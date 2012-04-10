using System;

namespace Metasequoia
{
	[Flags]
	public enum DrawObjectVisibility
	{
		/// <summary>
		/// 頂点を表示
		/// </summary>
		Point = 1,
		/// <summary>
		/// ラインを表示
		/// </summary>
		Line = 2,
		/// <summary>
		/// 面を表示
		/// </summary>
		Face = 4,
	}
}
