using System;

namespace Metasequoia.Sharp
{
	public class MetasequoiaEventArgs : EventArgs
	{
		MetasequoiaEventArgs createdFrom;
		bool handled;

		public Event EventType
		{
			get;
			private set;
		}

		public IntPtr Option
		{
			get;
			private set;
		}

		public Document Document
		{
			get;
			private set;
		}

		public bool Handled
		{
			get
			{
				return handled;
			}
			set
			{
				handled = value;

				if (createdFrom != null)
					createdFrom.Handled = value;
			}
		}

		public MetasequoiaEventArgs(Document document, Event eventType, IntPtr option)
		{
			this.Document = document;
			this.EventType = eventType;
			this.Option = option;
		}

		public MetasequoiaEventArgs(MetasequoiaEventArgs e)
			: this(e.Document, e.EventType, e.Option)
		{
			createdFrom = e;
		}

		public static unsafe IntPtr ExtractEventOption(IntPtr option, string name)
		{
			if (option != IntPtr.Zero)
			{
				var arr = (void**)option;

				for (int i = 0; arr[i] != null; i += 2)
					if (new string((char*)arr[i]) == name)
						return (IntPtr)arr[i + 1];
			}

			return IntPtr.Zero;
		}

		public static unsafe bool ExtractEventOptionBool(IntPtr option, string name, bool defaultValue)
		{
			var ptr = ExtractEventOption(option, name);

			return ptr == IntPtr.Zero ? defaultValue : *(int*)ptr != 0;
		}

		public static unsafe int ExtractEventOptionInt(IntPtr option, string name, int defaultValue)
		{
			var ptr = ExtractEventOption(option, name);

			return ptr == IntPtr.Zero ? defaultValue : *(int*)ptr;
		}

		public static unsafe float ExtractEventOptionFloat(IntPtr option, string name, float defaultValue)
		{
			var ptr = ExtractEventOption(option, name);

			return ptr == IntPtr.Zero ? defaultValue : *(float*)ptr;
		}
	}
}
