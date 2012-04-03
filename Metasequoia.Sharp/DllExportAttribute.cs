using System;

namespace System.Runtime.InteropServices
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	sealed class DllExportAttribute : Attribute
	{
		public DllExportAttribute(string entryPoint, CallingConvention callingConvention)
		{
			this.EntryPoint = entryPoint;
			this.CallingConvention = callingConvention;
		}

		public string EntryPoint
		{
			get;
			private set;
		}

		public CallingConvention CallingConvention
		{
			get;
			private set;
		}
	}
}
