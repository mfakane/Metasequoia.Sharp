namespace Metasequoia.Sharp
{
	public class UndoEventArgs : MetasequoiaEventArgs
	{
		/// <summary>
		/// アンドゥの状態カウンタ
		/// undo_state
		/// </summary>
		public int State
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "state", 0);
			}
		}

		/// <summary>
		/// アンドゥの実行可能回数
		/// undo_size
		/// </summary>
		public int Size
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "size", -1);
			}
		}

		public UndoEventArgs(MetasequoiaEventArgs e)
			: base(e)
		{
		}
	}
}
