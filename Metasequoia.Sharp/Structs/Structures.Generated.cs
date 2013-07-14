using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Metasequoia
{
	/// <summary>
	/// MQPoint
	/// </summary>
	public partial struct Point
	{
		/// <summary>
		/// float x
		/// </summary>
		public float X;
		/// <summary>
		/// float y
		/// </summary>
		public float Y;
		/// <summary>
		/// float z
		/// </summary>
		public float Z;
	}
	
	/// <summary>
	/// MQColor
	/// </summary>
	public partial struct Color
	{
		/// <summary>
		/// float r
		/// </summary>
		public float R;
		/// <summary>
		/// float g
		/// </summary>
		public float G;
		/// <summary>
		/// float b
		/// </summary>
		public float B;
	}
	
	/// <summary>
	/// MQCoordinate
	/// </summary>
	public partial struct Coordinate
	{
		/// <summary>
		/// float u
		/// </summary>
		public float U;
		/// <summary>
		/// float v
		/// </summary>
		public float V;
	}
	
	/// <summary>
	/// MQAngle
	/// </summary>
	public partial struct Angle
	{
		/// <summary>
		/// float head
		/// </summary>
		public float Head;
		/// <summary>
		/// float pitch
		/// </summary>
		public float Pitch;
		/// <summary>
		/// float bank
		/// </summary>
		public float Bank;
	}
	
	/// <summary>
	/// MQFileDialogInfo
	/// </summary>
	public partial struct FileDialogInfo
	{
		/// <summary>
		/// DWORD dwSize
		/// </summary>
		public uint dwSize;
		/// <summary>
		/// int type
		/// </summary>
		public int Type;
		/// <summary>
		/// float scale
		/// </summary>
		public float Scale;
		/// <summary>
		/// const char * softname
		/// </summary>
		[MarshalAs(UnmanagedType.LPStr)] public string Softname;
		/// <summary>
		/// int axis_x
		/// </summary>
		public int AxisX;
		/// <summary>
		/// int axis_y
		/// </summary>
		public int AxisY;
		/// <summary>
		/// int axis_z
		/// </summary>
		public int AxisZ;
	}
	
	/// <summary>
	/// MQUserDataInfo 
	/// </summary>
	public partial struct UserDataInfo 
	{
		/// <summary>
		/// DWORD dwSize
		/// </summary>
		public uint dwSize;
		/// <summary>
		/// DWORD productID
		/// </summary>
		public uint ProductID;
		/// <summary>
		/// DWORD pluginID
		/// </summary>
		public uint PluginID;
		/// <summary>
		/// char identifier
		/// </summary>
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public byte[] Identifier;
		/// <summary>
		/// int userdata_type
		/// </summary>
		public int UserdataType;
		/// <summary>
		/// int bytes_per_element
		/// </summary>
		public int BytesPerElement;
		/// <summary>
		/// bool create
		/// </summary>
		public bool Create;
	}
	
	/// <summary>
	/// MQSendMessageInfo 
	/// </summary>
	public partial struct SendMessageInfo 
	{
		/// <summary>
		/// DWORD Product
		/// </summary>
		public uint Product;
		/// <summary>
		/// DWORD ID
		/// </summary>
		public uint ID;
		/// <summary>
		/// int index
		/// </summary>
		public int Index;
		/// <summary>
		/// void * option
		/// </summary>
		public /* void* */ IntPtr Option;
	}
	
	/// <summary>
	/// MQSelectVertex
	/// </summary>
	public partial struct SelectVertex
	{
		/// <summary>
		/// int object
		/// </summary>
		public int Object;
		/// <summary>
		/// int vertex
		/// </summary>
		public int Vertex;
	}
	
	/// <summary>
	/// MQSelectLine
	/// </summary>
	public partial struct SelectLine
	{
		/// <summary>
		/// int object
		/// </summary>
		public int Object;
		/// <summary>
		/// int face
		/// </summary>
		public int Face;
		/// <summary>
		/// int line
		/// </summary>
		public int Line;
	}
	
	/// <summary>
	/// MQSelectFace
	/// </summary>
	public partial struct SelectFace
	{
		/// <summary>
		/// int object
		/// </summary>
		public int Object;
		/// <summary>
		/// int face
		/// </summary>
		public int Face;
	}
	
}

