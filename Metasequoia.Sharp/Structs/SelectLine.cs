namespace Metasequoia
{
	partial struct SelectLine
	{
		public SelectLine(int objectIndex, int faceIndex, int lineIndex)
		{
			this.Object = objectIndex;
			this.Face = faceIndex;
			this.Line = lineIndex;
		}
	}
}
