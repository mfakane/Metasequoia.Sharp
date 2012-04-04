using System.Runtime.InteropServices;

namespace Metasequoia
{
	public struct EditOption
	{
		[MarshalAs(UnmanagedType.Bool)]
		public bool EditVertex;
		[MarshalAs(UnmanagedType.Bool)]
		public bool EditLine;
		[MarshalAs(UnmanagedType.Bool)]
		public bool EditFace;
		[MarshalAs(UnmanagedType.Bool)]
		public bool SelectRect;
		[MarshalAs(UnmanagedType.Bool)]
		public bool SelectRope;
		[MarshalAs(UnmanagedType.Bool)]
		public bool SnapX;
		[MarshalAs(UnmanagedType.Bool)]
		public bool SnapY;
		[MarshalAs(UnmanagedType.Bool)]
		public bool SnapZ;
		public CoordinateType CoordinateType;
		public SnapGridType SnapGrid;
		[MarshalAs(UnmanagedType.Bool)]
		public bool Symmetry;
		public float SymmetryDistance;
		[MarshalAs(UnmanagedType.Bool)]
		public bool CurrentObjectOnly;
	}
}
