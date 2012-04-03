namespace Metasequoia.Sharp
{
	public class MouseEventArgs : MetasequoiaEventArgs
	{
		public int X
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "mouse_pos_x", 0);
			}
		}

		public int Y
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "mouse_pos_y", 0);
			}
		}

		public int Wheel
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "mouse_wheel", 0);
			}
		}

		public MouseButtonFlags ButtonState
		{
			get
			{
				return (MouseButtonFlags)MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "button_state", 0);
			}
		}

		public Scene Scene
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOption(this.Option, "scene");
			}
		}

		public MouseEventArgs(MetasequoiaEventArgs e)
			: base(e)
		{
		}
	}
}