using System.Runtime.InteropServices;

namespace Metasequoia
{
	public struct SceneOption
	{
		[MarshalAs(UnmanagedType.Bool)]
		public bool ShowVertex;
		[MarshalAs(UnmanagedType.Bool)]
		public bool ShowLine;
		[MarshalAs(UnmanagedType.Bool)]
		public bool ShowFace;
		[MarshalAs(UnmanagedType.Bool)]
		public bool FrontOnly;
		[MarshalAs(UnmanagedType.Bool)]
		public bool ShowBackgroundImage;
	}
}
