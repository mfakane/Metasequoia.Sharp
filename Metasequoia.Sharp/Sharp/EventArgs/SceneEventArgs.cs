namespace Metasequoia.Sharp
{
	public class SceneEventArgs : MetasequoiaEventArgs
	{
		public Scene Scene
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOption(this.Option, "scene");
			}
		}

		public SceneEventArgs(MetasequoiaEventArgs e)
			: base(e)
		{
		}
	}
}
