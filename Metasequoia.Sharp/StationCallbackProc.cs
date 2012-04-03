using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Metasequoia
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public delegate bool StationCallbackProc(/* Document */ IntPtr doc, /* void* */ IntPtr option);
}
