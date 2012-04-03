namespace Metasequoia.Sharp
{
	public class DrawEventArgs : MetasequoiaEventArgs
	{
		public int Width
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "width", 1);
			}
		}

		public int Height
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "height", 1);
			}
		}

		public Scene Scene
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOption(this.Option, "scene");
			}
		}

		public DrawEventArgs(MetasequoiaEventArgs e)
			: base(e)
		{
		}
	}
}
