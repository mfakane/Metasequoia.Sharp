namespace Metasequoia.Sharp
{
	public class UndoEventArgs : MetasequoiaEventArgs
	{
		public int State
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "state", 0);
			}
		}

		public int Size
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "size", 0);
			}
		}

		public UndoEventArgs(MetasequoiaEventArgs e)
			: base(e)
		{
		}
	}
}
