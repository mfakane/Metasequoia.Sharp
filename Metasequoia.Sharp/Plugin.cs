using System;
using System.Diagnostics;
using System.Text;

namespace Metasequoia
{
	public static partial class Plugin
	{
		public static IntPtr MainWindowHandle
		{
			get
			{
				return NativeMethods.MQ_GetWindowHandle();
			}
		}

		public static void RefreshView()
		{
			NativeMethods.MQ_RefreshView(IntPtr.Zero);
		}

		public static string GetSystemPath(Folder folder)
		{
			const int MaxPath = 260;
			var sb = new StringBuilder(MaxPath);

			if (NativeMethods.MQ_GetSystemPath(sb, (int)folder))
				return sb.ToString();
			else
				return null;
		}

		public static Setting OpenSetting(IPlugin plugin)
		{
			uint product;
			uint id;

			plugin.GetPluginId(out product, out id);

			return new Setting(Plugin.GetSystemPath(Folder.MetaseqIni), string.Format("Plugin.{0:X8}:{1:X8}", product, id));
		}

		public static unsafe int SendUserMessage(IPlugin plugin, Document doc, uint target_product, uint target_id, string description, IntPtr message)
		{
			var info = new SendMessageInfo();
			var result = 0;

			fixed (byte* documentString = GetASCII("document"),
						 targetProductString = GetASCII("target_product"),
						 targetIdString = GetASCII("target_id"),
						 descriptionString = GetASCII("description"),
						 messageString = GetASCII("message"),
						 resultString = GetASCII("result"),
						 descriptionPtr = Get932(description))
			{
				var array = new void*[]
				{
					documentString,
					(void*)(IntPtr)doc,
					targetProductString,
					&target_product,
					targetIdString,
					&target_id,
					descriptionString,
					descriptionPtr,
					messageString,
					(void*)message,
					resultString,
					&result,
					null,
				};

				plugin.GetPluginId(out info.Product, out info.ID);

				fixed (void** arrayPtr = array)
				{
					info.Option = (IntPtr)arrayPtr;

					NativeMethods.MQ_SendMessage((int)Message.UserMessage, ref info);
				}
			}

			return result;
		}

		static byte[] GetASCII(string value)
		{
			return Encoding.ASCII.GetBytes(value);
		}

		internal static byte[] Get932(string value)
		{
			return Encoding.GetEncoding(932).GetBytes(value);
		}
	}
}