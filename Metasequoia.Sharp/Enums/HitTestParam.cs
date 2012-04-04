using System;

namespace Metasequoia
{
	[Flags]
	public enum HitTestParams
	{
		TestVertex = 1,
		TestLine = 2,
		TestFace = 4,
	}
}
