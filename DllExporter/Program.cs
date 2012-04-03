using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace DllExporter
{
	class Program
	{
		static void Main(string[] args)
		{
			var is64 = false;
			var isDebug = false;
			var input = "";
			var output = "";
			var il = "";

			foreach (var i in args)
				if (i.StartsWith("/"))
				{
					var sl = i.Split(new[] { ':' }, 2);

					switch (sl.First().ToLower())
					{
						case "x64":
							is64 = true;

							break;
						case "debug":
							is64 = true;

							break;
						case "out":
							output = sl.Last();

							break;
						case "il":
							il = sl.Last();

							break;
					}
				}
				else
					input = i;

			if (string.IsNullOrEmpty(input))
			{
				Console.WriteLine("Usage: DllExporter [/x64] [/debug] [/il:filename] [/out:filename] <assembly>");

				return;
			}

			if (!File.Exists(input))
			{
				Console.Error.WriteLine("DllExporter: input file " + input + " not found");

				return;
			}

			if (string.IsNullOrEmpty(output))
				output = input;
			else if (!Path.IsPathRooted(output) && Path.IsPathRooted(input))
				output = Path.Combine(Path.GetDirectoryName(input), output);

			const string attributeName = "System.Runtime.InteropServices.DllExportAttribute";
			var corFlags = new Regex(@"^\.corflags\s(.+)$", RegexOptions.Compiled | RegexOptions.Multiline);
			var method = new Regex(@"\.method\s+([\w\s]+?)\s+(?<marshal>marshal\([\w\s]+?\)\s+)?(?<name>.+?)\(", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.RightToLeft);
			var dllExport = new Regex(@"(?<indent>\s*)\.custom instance void " + Regex.Escape(attributeName) + @"\:\:\.ctor\((?:.|\s)*?\)\s+=\s+\{(?<args>(?:.|\s)+?)\}", RegexOptions.Compiled | RegexOptions.Multiline);
			var dllExportString = new Regex(@"string\(\'(.+?)\'\)", RegexOptions.Compiled);
			var dllExportInt32 = new Regex(@"int32\((.+?)\)", RegexOptions.Compiled);
			var replaceMethods = new List<Tuple<string, string>>();

			var ilcode = Disassemble(input);
			var exportIndex = 1;

			ilcode = corFlags.Replace(ilcode, is64
				? ".corflags 0x00000008    // 64BITREQUIRED"
				: ".corflags 0x00000002    // 32BITREQUIRED");
			ilcode = dllExport.Replace(ilcode, m =>
			{
				var content = m.Groups["args"].Value;
				var indent = m.Groups["indent"].Value;
				var entryPoint = dllExportString.Match(content).Groups[1].Value;
				var callingConvention = GetCallingConvention((CallingConvention)int.Parse(dllExportInt32.Match(content).Groups[1].Value));
				var mm = method.Match(ilcode, m.Index);

				replaceMethods.Add(Tuple.Create(mm.Value, mm.Value.Insert((mm.Groups["marshal"].Success ? mm.Groups["marshal"] : mm.Groups["name"]).Index - mm.Index, string.Format("modopt([mscorlib]{0}) ", callingConvention))));

				Console.WriteLine("Exporting {0} at {1}", entryPoint, exportIndex);

				return string.Format("{0}.export [{1}] as {2}", indent, exportIndex++, entryPoint);
			});
			ilcode = replaceMethods.Aggregate(ilcode, (x, y) => x.Replace(y.Item1, y.Item2));

			if (!string.IsNullOrEmpty(il))
				File.WriteAllText(il, ilcode);

			Assemble(ilcode, output, is64, isDebug);
		}

		static string GetCallingConvention(CallingConvention callingConvention)
		{
			switch (callingConvention)
			{
				case CallingConvention.Cdecl:
					return typeof(CallConvCdecl).FullName;
				case CallingConvention.FastCall:
					return typeof(CallConvFastcall).FullName;
				case CallingConvention.StdCall:
				case CallingConvention.Winapi:
					return typeof(CallConvStdcall).FullName;
				case CallingConvention.ThisCall:
					return typeof(CallConvThiscall).FullName;
				default:
					throw new ArgumentException("invalid calling convention");
			}
		}

		static void Assemble(string il, string output, bool is64, bool isDebug)
		{
			var temp = Path.GetTempFileName();

			File.WriteAllText(temp, il);

			try
			{
				using (var p = new Process
				{
					StartInfo =
					{
						FileName = GetAssembler(),
						Arguments = string.Join(" ", new[]
						{
							"/nologo",
							"/quiet",
							"/dll",
							is64 ? "/x64" : "",
							isDebug ? "/debug" : "/debug=opt /optimize",
							"/pdb",
							"\"/out:" + output + "\"",
							temp,
						}),
						UseShellExecute = false,
						CreateNoWindow = false,
						RedirectStandardOutput = true,
					},
				})
				{
					p.Start();
					p.WaitForExit();

					Console.Write(p.StandardOutput.ReadToEnd());

					if (p.ExitCode != 0)
						throw new Exception("Failed to assemble. Error code: " + p.ExitCode);
				}
			}
			finally
			{
				File.Delete(temp);
			}
		}

		static string Disassemble(string input)
		{
			var temp = Path.GetTempFileName();

			try
			{
				using (var p = new Process
				{
					StartInfo =
					{
						FileName = GetDisassembler(),
						Arguments = string.Join(" ", new[]
						{
							"/linenum",
							"/caverbal",
							"/unicode",
							"/nobar",
							"/typelist",
							"\"/out=" + temp + "\"",
							input,
						}),
						UseShellExecute = false,
						CreateNoWindow = false,
						RedirectStandardOutput = true,
					},
				})
				{
					try
					{
						p.Start();
					}
					catch (Win32Exception ex)
					{
						if (ex.NativeErrorCode == 3)
						{
							p.StartInfo.FileName = p.StartInfo.FileName.Replace(@"\Program Files\", @"\Program Files (x86)\");
							p.Start();
						}
						else
							throw;
					}

					p.WaitForExit();
					Console.Write(p.StandardOutput.ReadToEnd());

					if (p.ExitCode == 0)
						return File.ReadAllText(temp);
					else
						throw new Exception("Failed to disassemble. Error code: " + p.ExitCode);
				}
			}
			finally
			{
				File.Delete(temp);
			}
		}

		static string GetAssembler()
		{
			return Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "ilasm.exe");
		}

		static string GetDisassembler()
		{
			const string registryPath = @"SOFTWARE\Microsoft\Microsoft SDKs\Windows";

			using (var key = Registry.LocalMachine.OpenSubKey(registryPath) ?? Registry.CurrentUser.OpenSubKey(registryPath))
				return Path.Combine((string)key.GetValue("CurrentInstallFolder"), @"Bin\ildasm.exe");
		}
	}
}
