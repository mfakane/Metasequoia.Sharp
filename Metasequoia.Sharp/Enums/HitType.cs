using System;

namespace Metasequoia
{
	[Flags]
	public enum HitType
	{
		None,
		Vertex,
		Line,
		Face = 4,
	}
}
