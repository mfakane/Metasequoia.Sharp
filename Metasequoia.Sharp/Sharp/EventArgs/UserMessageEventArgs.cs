namespace Metasequoia.Sharp
{
	public class UserMessageEventArgs : MetasequoiaEventArgs
	{
		public uint VendorId
		{
			get
			{
				return (uint)MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "src_product", 0);
			}
		}

		public uint ProductId
		{
			get
			{
				return (uint)MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "src_id", 0);
			}
		}

		public unsafe string Description
		{
			get
			{
				return new string((char*)MetasequoiaEventArgs.ExtractEventOption(this.Option, "description"));
			}
		}

		public unsafe int Result
		{
			get
			{
				return *(int*)MetasequoiaEventArgs.ExtractEventOption(this.Option, "result");
			}
			set
			{
				*(int*)MetasequoiaEventArgs.ExtractEventOption(this.Option, "result") = value;
			}
		}

		public UserMessageEventArgs(MetasequoiaEventArgs e)
			: base(e)
		{
		}
	}
}
