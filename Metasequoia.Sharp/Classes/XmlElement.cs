using System;
using System.Collections.Generic;
using System.Text;

namespace Metasequoia
{
	public class XmlElement
	{
		public IntPtr Handle
		{
			get;
			private set;
		}

		XmlElement(IntPtr ptr)
		{
			this.Handle = ptr;
		}

		public static implicit operator IntPtr(XmlElement self)
		{
			return self.Handle;
		}

		public static implicit operator XmlElement(IntPtr self)
		{
			return new XmlElement(self);
		}

		public unsafe XmlElement AddChildElement(string name)
		{
			var val = ToUtf8(name);

			fixed (byte* valptr = val)
			{
				var ptr = new void*[]
				{
					valptr,
					null,
				};

				fixed (void* ptrptr = ptr)
					NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.AddChildElement, (IntPtr)ptrptr);

				return (IntPtr)ptr[1];
			}
		}

		public unsafe bool RemoveChildElement(XmlElement child)
		{
			var rt = 0;
			var ptr = new void*[]
			{
				(void*)(IntPtr)child,
				&rt,
			};

			fixed (void* ptrptr = ptr)
				NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.RemoveChildElement, (IntPtr)ptrptr);

			return rt != 0;
		}

		public unsafe XmlElement FirstChildElement()
		{
			var ptr = new void*[]
			{
				null,
				null
			};

			fixed (void* ptrptr = ptr)
				NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.FirstChildElement, (IntPtr)ptrptr);

			return (IntPtr)ptr[1];
		}

		public unsafe XmlElement FirstChildElement(string name)
		{
			var val = ToUtf8(name);

			fixed (byte* valptr = val)
			{
				var ptr = new void*[]
				{
					valptr,
					null,
				};

				fixed (void* ptrptr = ptr)
					NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.FirstChildElement, (IntPtr)ptrptr);

				return (IntPtr)ptr[1];
			}
		}

		public unsafe XmlElement NextChildElement(XmlElement child)
		{
			var ptr = new void*[]
			{
				null,
				(void*)(IntPtr)child,
				null,
			};

			fixed (void* ptrptr = ptr)
				NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.NextChildElement, (IntPtr)ptrptr);

			return (IntPtr)ptr[2];
		}

		public unsafe XmlElement NextChildElement(string name, XmlElement child)
		{
			var val = ToUtf8(name);

			fixed (byte* valptr = val)
			{
				var ptr = new void*[]
				{
					valptr,
					(void*)(IntPtr)child,
					null,
				};

				fixed (void* ptrptr = ptr)
					NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.NextChildElement, (IntPtr)ptrptr);

				return (IntPtr)ptr[2];
			}
		}

		public unsafe XmlElement GetParentElement()
		{
			var ptr = new void*[]
			{
				null,
			};

			fixed (void* ptrptr = ptr)
				NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.GetParentElement, (IntPtr)ptrptr);

			return (IntPtr)ptr[0];
		}

		public unsafe string GetName()
		{
			var ptr = new void*[]
			{
				null,
			};

			fixed (void* ptrptr = ptr)
				NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.GetName, (IntPtr)ptrptr);

			var bytePtr = (byte*)ptr[0];
			var bytes = new List<byte>();

			for (int i = 0; ; i++)
			{
				var b = bytePtr[i];

				if (b == 0)
					break;
				else
					bytes.Add(b);
			}

			return Encoding.UTF8.GetString(bytes.ToArray());
		}

		public unsafe string GetText()
		{
			var ptr = new void*[]
			{
				null,
			};

			fixed (void* ptrptr = ptr)
				NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.GetText, (IntPtr)ptrptr);

			if (ptr[0] == null)
				return null;

			var bytePtr = (byte*)ptr[0];
			var bytes = new List<byte>();

			for (int i = 0; ; i++)
			{
				var b = bytePtr[i];

				if (b == 0)
					break;
				else
					bytes.Add(b);
			}

			return Encoding.UTF8.GetString(bytes.ToArray());
		}

		public unsafe string GetAttribute(string name)
		{
			var val = ToUtf8(name);

			fixed (byte* valptr = val)
			{
				var ptr = new void*[]
				{
					valptr,
					null,
				};

				fixed (void* ptrptr = ptr)
					NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.GetAttribute, (IntPtr)ptrptr);

				var bytePtr = (byte*)ptr[1];
				var bytes = new List<byte>();

				for (int i = 0; ; i++)
				{
					var b = bytePtr[i];

					if (b == 0)
						break;
					else
						bytes.Add(b);
				}

				return Encoding.UTF8.GetString(bytes.ToArray());
			}
		}

		public unsafe void SetText(string text)
		{
			var val = ToUtf8(text);

			fixed (byte* valptr = val)
			{
				var ptr = new void*[]
				{
					valptr,
				};

				fixed (void* ptrptr = ptr)
					NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.SetText, (IntPtr)ptrptr);
			}
		}

		public unsafe void SetAttribute(string name, string value)
		{
			var nameVal = ToUtf8(name);
			var valueVal = ToUtf8(value);

			fixed (byte* nameValPtr = nameVal)
			fixed (byte* valueValPtr = valueVal)
			{
				var ptr = new void*[]
				{
					nameValPtr,
					valueValPtr,
				};

				fixed (void* ptrptr = ptr)
					NativeMethods.MQXmlElem_Value(this, (int)Xmlelem.SetAttribute, (IntPtr)ptrptr);
			}
		}

		static byte[] ToUtf8(string value)
		{
			return Encoding.UTF8.GetBytes(value);
		}
	}
}
