using System;

namespace Metasequoia.Sharp
{
	[Flags]
	public enum MouseButtonFlags : uint
	{
		Left = 0x1,
		Right = 0x2,
		Shift = 0x4,
		Control = 0x8,
		Middle = 0x10,
		XButton1 = 0x20,
		XButton2 = 0x40,
	}
}
