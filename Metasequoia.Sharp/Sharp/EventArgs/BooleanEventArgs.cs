namespace Metasequoia.Sharp
{
	public class BooleanEventArgs : MetasequoiaEventArgs
	{
		public unsafe bool Value
		{
			get
			{
				return *(bool*)this.Option;
			}
		}

		public BooleanEventArgs(MetasequoiaEventArgs e)
			: base(e)
		{
		}
	}
}
