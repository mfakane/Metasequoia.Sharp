
namespace Metasequoia.Sharp
{
	public class KeyEventArgs : MouseEventArgs
	{
		public int Key
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "key", 0);
			}
		}

		public KeyEventArgs(MetasequoiaEventArgs e)
			: base(e)
		{
		}
	}
}