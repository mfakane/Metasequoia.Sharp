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
				return Process.GetCurrentProcess().MainWindowHandle;
			}
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

				plugin.GetPluginId(ref info.Product, ref info.ID);

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

		static byte[] Get932(string value)
		{
			return Encoding.GetEncoding(932).GetBytes(value);
		}
	}
}