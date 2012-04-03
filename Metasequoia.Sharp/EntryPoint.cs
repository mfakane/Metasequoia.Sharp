using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Metasequoia
{
	class EntryPoint
	{
		static IPlugin instance;

		static IPlugin Instance
		{
			get
			{
				if (instance == null)
				{
					var type = Assembly.GetExecutingAssembly()
									   .GetExportedTypes()
									   .Where(_ => _.IsClass && !_.IsAbstract)
									   .Where(_ => typeof(IPlugin).IsAssignableFrom(_))
									   .Last();

					instance = (IPlugin)type.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
				}

				return instance;
			}
		}

		[DllExport("MQCheckVersion", CallingConvention.Cdecl)]
		public static uint MQCheckVersion(uint exeVersion)
		{
			return exeVersion >= Plugin.Version ? Plugin.Version : 0u;
		}

		[DllExport("MQInit", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static bool MQInit([MarshalAs(UnmanagedType.LPStr)] string exeName)
		{
			return NativeMethods.Initialize();
		}

		[DllExport("MQGetPlugInID", CallingConvention.Cdecl)]
		public static void MQGetPlugInID(ref uint product, ref uint id)
		{
			Instance.GetPluginId(ref product, ref id);
		}

		[DllExport("MQGetPlugInName", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static string MQGetPlugInName()
		{
			return Instance.GetPluginName();
		}

		[DllExport("MQGetPlugInType", CallingConvention.Cdecl)]
		public static PluginType MQGetPlugInType()
		{
			return Instance.GetPluginType();
		}

		[DllExport("MQImportFile", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static bool MQImportFile(int index, [MarshalAs(UnmanagedType.LPStr)] string filename, IntPtr doc)
		{
			return Instance.ImportFile(index, filename, doc);
		}

		[DllExport("MQExportFile", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static bool MQExportFile(int index, [MarshalAs(UnmanagedType.LPStr)] string filename, IntPtr doc)
		{
			return Instance.ExportFile(index, filename, doc);
		}

		[DllExport("EnumFileType", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static string EnumFileType(int index)
		{
			return Instance.EnumFileType(index);
		}

		[DllExport("EnumFileExt", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static string EnumFileExt(int index)
		{
			return Instance.EnumFileExt(index);
		}

		[DllExport("MQEnumString", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static string MQEnumString(int index)
		{
			return Instance.EnumString(index);
		}

		[DllExport("MQCreate", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static bool MQCreate(int index, IntPtr doc)
		{
			return Instance.Create(index, doc);
		}

		[DllExport("MQModifyObject", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static bool MQModifyObject(int index, IntPtr doc)
		{
			return Instance.ModifyObject(index, doc);
		}

		[DllExport("MQModifySelect", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static bool MQModifySelect(int index, IntPtr doc)
		{
			return Instance.ModifySelect(index, doc);
		}

		[DllExport("MQOnEvent", CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static bool MQOnEvent(IntPtr doc, Event eventType, /* void* */ IntPtr option)
		{
			return Instance.OnEvent(doc, eventType, option);
		}
	}
}
