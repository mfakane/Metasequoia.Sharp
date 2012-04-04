using System;

namespace Metasequoia
{
	[Flags]
	public enum SnapGridType
	{
		/// <summary>
		/// X 軸に吸着
		/// </summary>
		X = 1,
		/// <summary>
		/// Y 軸に吸着
		/// </summary>
		Y = 2,
		/// <summary>
		/// Z 軸に吸着
		/// </summary>
		Z = 4,
	}
}
