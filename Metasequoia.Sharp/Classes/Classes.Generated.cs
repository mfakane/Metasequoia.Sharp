using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Metasequoia
{
	/// <summary>
	/// MQDocument
	/// </summary>
	public partial class Document
	{
		/// <summary>
		/// このオブジェクトの基になるネイティブなポインタを取得します。
		/// </summary>
		public IntPtr Handle
		{
			get;
			private set;
		}

		protected Document(IntPtr ptr)
		{
			this.Handle = ptr;
			Initialize();
		}

		public static implicit operator Document(IntPtr self)
		{
			return self == IntPtr.Zero ? null : new Document(self);
		}

		public static implicit operator IntPtr(Document self)
		{
			return self.Handle;
		}

		partial void Initialize();

		/// <summary>
		/// int GetObjectCount()
		/// </summary>
		public int ObjectCount
		{
			get
			{
				return NativeMethods.MQDoc_GetObjectCount(this);
			}
		}

		/// <summary>
		/// MQObject GetObject(int index)
		/// </summary>
		public Object GetObject(int index)
		{
			return NativeMethods.MQDoc_GetObject(this, index);
		}

		/// <summary>
		/// int GetCurrentObjectIndex()
		/// </summary>
		public int CurrentObjectIndex
		{
			get
			{
				return NativeMethods.MQDoc_GetCurrentObjectIndex(this);
			}
			set
			{
				var index = value;
				NativeMethods.MQDoc_SetCurrentObjectIndex(this, index);
			}
		}

		/// <summary>
		/// int AddObject(MQObject obj)
		/// </summary>
		public int AddObject(Object obj)
		{
			return NativeMethods.MQDoc_AddObject(this, obj);
		}

		/// <summary>
		/// void DeleteObject(int index)
		/// </summary>
		public void DeleteObject(int index)
		{
			NativeMethods.MQDoc_DeleteObject(this, index);
		}

		/// <summary>
		/// int GetObjectIndex(MQObject obj)
		/// </summary>
		public int GetObjectIndex(Object obj)
		{
			return NativeMethods.MQDoc_GetObjectIndex(this, obj);
		}

		/// <summary>
		/// int GetMaterialCount()
		/// </summary>
		public int MaterialCount
		{
			get
			{
				return NativeMethods.MQDoc_GetMaterialCount(this);
			}
		}

		/// <summary>
		/// MQMaterial GetMaterial(int material)
		/// </summary>
		public Material GetMaterial(int material)
		{
			return NativeMethods.MQDoc_GetMaterial(this, material);
		}

		/// <summary>
		/// int GetCurrentMaterialIndex()
		/// </summary>
		public int CurrentMaterialIndex
		{
			get
			{
				return NativeMethods.MQDoc_GetCurrentMaterialIndex(this);
			}
			set
			{
				var index = value;
				NativeMethods.MQDoc_SetCurrentMaterialIndex(this, index);
			}
		}

		/// <summary>
		/// int AddMaterial(MQMaterial mat)
		/// </summary>
		public int AddMaterial(Material mat)
		{
			return NativeMethods.MQDoc_AddMaterial(this, mat);
		}

		/// <summary>
		/// void DeleteMaterial(int index)
		/// </summary>
		public void DeleteMaterial(int index)
		{
			NativeMethods.MQDoc_DeleteMaterial(this, index);
		}

		/// <summary>
		/// void Compact()
		/// </summary>
		public void Compact()
		{
			NativeMethods.MQDoc_Compact(this);
		}

		/// <summary>
		/// void ClearSelect(DWORD flag)
		/// </summary>
		public void ClearSelect(uint flag)
		{
			NativeMethods.MQDoc_ClearSelect(this, flag);
		}

		/// <summary>
		/// BOOL AddSelectVertex(int objindex, int vertindex)
		/// </summary>
		public bool AddSelectVertex(int objectIndex, int vertexIndex)
		{
			return NativeMethods.MQDoc_AddSelectVertex(this, objectIndex, vertexIndex);
		}

		/// <summary>
		/// BOOL DeleteSelectVertex(int objindex, int vertindex)
		/// </summary>
		public bool DeleteSelectVertex(int objectIndex, int vertexIndex)
		{
			return NativeMethods.MQDoc_DeleteSelectVertex(this, objectIndex, vertexIndex);
		}

		/// <summary>
		/// BOOL IsSelectVertex(int objindex, int vertindex)
		/// </summary>
		public bool IsSelectVertex(int objectIndex, int vertexIndex)
		{
			return NativeMethods.MQDoc_IsSelectVertex(this, objectIndex, vertexIndex);
		}

		/// <summary>
		/// BOOL AddSelectVertex(MQSelectVertex sel)
		/// </summary>
		public bool AddSelectVertex(SelectVertex sel)
		{
			return NativeMethods.MQDoc_AddSelectVertex(this, sel.Object, sel.Vertex);
		}

		/// <summary>
		/// BOOL DeleteSelectVertex(MQSelectVertex sel)
		/// </summary>
		public bool DeleteSelectVertex(SelectVertex sel)
		{
			return NativeMethods.MQDoc_DeleteSelectVertex(this, sel.Object, sel.Vertex);
		}

		/// <summary>
		/// BOOL IsSelectVertex(MQSelectVertex sel)
		/// </summary>
		public bool IsSelectVertex(SelectVertex sel)
		{
			return NativeMethods.MQDoc_IsSelectVertex(this, sel.Object, sel.Vertex);
		}

		/// <summary>
		/// BOOL AddSelectLine(int objindex, int faceindex, int lineindex)
		/// </summary>
		public bool AddSelectLine(int objectIndex, int faceIndex, int lineIndex)
		{
			return NativeMethods.MQDoc_AddSelectLine(this, objectIndex, faceIndex, lineIndex);
		}

		/// <summary>
		/// BOOL DeleteSelectLine(int objindex, int faceindex, int lineindex)
		/// </summary>
		public bool DeleteSelectLine(int objectIndex, int faceIndex, int lineIndex)
		{
			return NativeMethods.MQDoc_DeleteSelectLine(this, objectIndex, faceIndex, lineIndex);
		}

		/// <summary>
		/// BOOL IsSelectLine(int objindex, int faceindex, int lineindex)
		/// </summary>
		public bool IsSelectLine(int objectIndex, int faceIndex, int lineIndex)
		{
			return NativeMethods.MQDoc_IsSelectLine(this, objectIndex, faceIndex, lineIndex);
		}

		/// <summary>
		/// BOOL AddSelectLine(MQSelectLine sel)
		/// </summary>
		public bool AddSelectLine(SelectLine sel)
		{
			return NativeMethods.MQDoc_AddSelectLine(this, sel.Object, sel.Face, sel.Line);
		}

		/// <summary>
		/// BOOL DeleteSelectLine(MQSelectLine sel)
		/// </summary>
		public bool DeleteSelectLine(SelectLine sel)
		{
			return NativeMethods.MQDoc_DeleteSelectLine(this, sel.Object, sel.Face, sel.Line);
		}

		/// <summary>
		/// BOOL IsSelectLine(MQSelectLine sel)
		/// </summary>
		public bool IsSelectLine(SelectLine sel)
		{
			return NativeMethods.MQDoc_IsSelectLine(this, sel.Object, sel.Face, sel.Line);
		}

		/// <summary>
		/// BOOL AddSelectFace(int objindex, int faceindex)
		/// </summary>
		public bool AddSelectFace(int objectIndex, int faceIndex)
		{
			return NativeMethods.MQDoc_AddSelectFace(this, objectIndex, faceIndex);
		}

		/// <summary>
		/// BOOL DeleteSelectFace(int objindex, int faceindex)
		/// </summary>
		public bool DeleteSelectFace(int objectIndex, int faceIndex)
		{
			return NativeMethods.MQDoc_DeleteSelectFace(this, objectIndex, faceIndex);
		}

		/// <summary>
		/// BOOL IsSelectFace(int objindex, int faceindex)
		/// </summary>
		public bool IsSelectFace(int objectIndex, int faceIndex)
		{
			return NativeMethods.MQDoc_IsSelectFace(this, objectIndex, faceIndex);
		}

		/// <summary>
		/// BOOL AddSelectFace(MQSelectFace sel)
		/// </summary>
		public bool AddSelectFace(SelectFace sel)
		{
			return NativeMethods.MQDoc_AddSelectFace(this, sel.Object, sel.Face);
		}

		/// <summary>
		/// BOOL DeleteSelectFace(MQSelectFace sel)
		/// </summary>
		public bool DeleteSelectFace(SelectFace sel)
		{
			return NativeMethods.MQDoc_DeleteSelectFace(this, sel.Object, sel.Face);
		}

		/// <summary>
		/// BOOL IsSelectFace(MQSelectFace sel)
		/// </summary>
		public bool IsSelectFace(SelectFace sel)
		{
			return NativeMethods.MQDoc_IsSelectFace(this, sel.Object, sel.Face);
		}

		/// <summary>
		/// BOOL AddSelectUVVertex(int objindex, int faceindex, int vertindex)
		/// </summary>
		public bool AddSelectUVVertex(int objectIndex, int faceIndex, int vertexIndex)
		{
			return NativeMethods.MQDoc_AddSelectUVVertex(this, objectIndex, faceIndex, vertexIndex);
		}

		/// <summary>
		/// BOOL DeleteSelectUVVertex(int objindex, int faceindex, int vertindex)
		/// </summary>
		public bool DeleteSelectUVVertex(int objectIndex, int faceIndex, int vertexIndex)
		{
			return NativeMethods.MQDoc_DeleteSelectUVVertex(this, objectIndex, faceIndex, vertexIndex);
		}

		/// <summary>
		/// BOOL IsSelectUVVertex(int objindex, int faceindex, int vertindex)
		/// </summary>
		public bool IsSelectUVVertex(int objectIndex, int faceIndex, int vertexIndex)
		{
			return NativeMethods.MQDoc_IsSelectUVVertex(this, objectIndex, faceIndex, vertexIndex);
		}

		/// <summary>
		/// BOOL FindMappingFile(char *out_path, const char *filename, DWORD map_type)
		/// </summary>
		public bool FindMappingFile(StringBuilder outPath, string filename, Mapping mapType)
		{
			return NativeMethods.MQDoc_FindMappingFile(this, outPath, filename, (uint)mapType);
		}

		/// <summary>
		/// MQScene GetScene(int index)
		/// </summary>
		public Scene GetScene(int index)
		{
			return NativeMethods.MQDoc_GetScene(this, index);
		}

		/// <summary>
		/// MQObject GetParentObject(MQObject obj)
		/// </summary>
		public Object GetParentObject(Object obj)
		{
			return NativeMethods.MQDoc_GetParentObject(this, obj);
		}

		/// <summary>
		/// int GetChildObjectCount(MQObject obj)
		/// </summary>
		public int GetChildObjectCount(Object obj)
		{
			return NativeMethods.MQDoc_GetChildObjectCount(this, obj);
		}

		/// <summary>
		/// MQObject GetChildObject(MQObject obj, int index)
		/// </summary>
		public Object GetChildObject(Object obj, int index)
		{
			return NativeMethods.MQDoc_GetChildObject(this, obj, index);
		}

		/// <summary>
		/// void GetGlobalMatrix(MQObject obj, MQMatrix&amp; mtx)
		/// </summary>
		public void GetGlobalMatrix(Object obj, ref Matrix mtx)
		{
			var t = new float[16];
			NativeMethods.MQDoc_GetGlobalMatrix(this, obj, t);
			mtx = new Matrix(t);
		}

		/// <summary>
		/// void GetGlobalInverseMatrix(MQObject obj, MQMatrix&amp; mtx)
		/// </summary>
		public void GetGlobalInverseMatrix(Object obj, ref Matrix mtx)
		{
			var t = new float[16];
			NativeMethods.MQDoc_GetGlobalInverseMatrix(this, obj, t);
			mtx = new Matrix(t);
		}

		/// <summary>
		/// int InsertObject(MQObject obj, MQObject before)
		/// </summary>
		public int InsertObject(Object obj, Object before)
		{
			return NativeMethods.MQDoc_InsertObject(this, obj, before);
		}

	}

	/// <summary>
	/// MQScene
	/// </summary>
	public partial class Scene
	{
		/// <summary>
		/// このオブジェクトの基になるネイティブなポインタを取得します。
		/// </summary>
		public IntPtr Handle
		{
			get;
			private set;
		}

		protected Scene(IntPtr ptr)
		{
			this.Handle = ptr;
			Initialize();
		}

		public static implicit operator Scene(IntPtr self)
		{
			return self == IntPtr.Zero ? null : new Scene(self);
		}

		public static implicit operator IntPtr(Scene self)
		{
			return self.Handle;
		}

		partial void Initialize();

		/// <summary>
		/// void InitSize(int width, int height)
		/// </summary>
		public void InitSize(int width, int height)
		{
			NativeMethods.MQScene_InitSize(this, width, height);
		}

		/// <summary>
		/// void GetProjMatrix(float *matrix)
		/// </summary>
		public void GetProjMatrix(float[] matrix)
		{
			NativeMethods.MQScene_GetProjMatrix(this, matrix);
		}

		/// <summary>
		/// void GetViewMatrix(float *matrix)
		/// </summary>
		public void GetViewMatrix(float[] matrix)
		{
			NativeMethods.MQScene_GetViewMatrix(this, matrix);
		}

		/// <summary>
		/// MQPoint GetCameraPosition()
		/// </summary>
		public Point CameraPosition
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.GetCameraPos,val);
				return new Point(val[0],val[1],val[2]);
			}
			set
			{
				var p = value;
				var val = new float[3];
				val[0]=p.X;
				val[1]=p.Y;
				val[2]=p.Z;
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.SetCameraPos,val);
			}
		}

		/// <summary>
		/// MQAngle GetCameraAngle()
		/// </summary>
		public Angle CameraAngle
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.GetCameraAngle,val);
				return new Angle(val[0],val[1],val[2]);
			}
			set
			{
				var angle = value;
				var val = new float[3];
				val[0]=angle.Head;
				val[1]=angle.Pitch;
				val[2]=angle.Bank;
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.SetCameraAngle,val);
			}
		}

		/// <summary>
		/// MQPoint GetLookAtPosition()
		/// </summary>
		public Point LookAtPosition
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.GetLookAtPos,val);
				return new Point(val[0],val[1],val[2]);
			}
			set
			{
				var p = value;
				var val = new float[6];
				val[0]=p.X;
				val[1]=p.Y;
				val[2]=p.Z;
				val[3]=0;
				val[4]=1;
				val[5]=0;
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.SetLookAtPos,val);
			}
		}

		/// <summary>
		/// MQPoint GetRotationCenter()
		/// </summary>
		public Point RotationCenter
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.GetRotationCenter,val);
				return new Point(val[0],val[1],val[2]);
			}
			set
			{
				var p = value;
				var val = new float[3];
				val[0]=p.X;
				val[1]=p.Y;
				val[2]=p.Z;
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.SetRotationCenter,val);
			}
		}

		/// <summary>
		/// float GetFOV()
		/// </summary>
		public float FOV
		{
			get
			{
				var val = new float[0];
				NativeMethods.MQScene_FloatValue(this, (int)MQScene.GetFov, val);
				return val[0];
			}
			set
			{
				var fov = value;
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.SetFov,new[] { fov });
			}
		}

		/// <summary>
		/// MQPoint GetGlobalDirectionalLight()
		/// </summary>
		public Point GlobalDirectionalLight
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.GetDirectionalLight,val);
				return new Point(val[0],val[1],val[2]);
			}
			set
			{
				var vec = value;
				var val = new float[3];
				val[0]=vec.X;
				val[1]=vec.Y;
				val[2]=vec.Z;
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.SetDirectionalLight,val);
			}
		}

		/// <summary>
		/// MQColor GetGlobalAmbientColor()
		/// </summary>
		public Color GlobalAmbientColor
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.GetAmbientColor,val);
				return new Color(val[0],val[1],val[2]);
			}
			set
			{
				var amb = value;
				var val = new float[3];
				val[0]=amb.R;
				val[1]=amb.G;
				val[2]=amb.B;
				NativeMethods.MQScene_FloatValue(this,(int)MQScene.SetAmbientColor,val);
			}
		}

		/// <summary>
		/// MQPoint Convert3DToScreen(const MQPoint&amp; p, float *out_w)
		/// </summary>
		public Point Convert3dToScreen(ref Point p, out float w)
		{
			var val = new float[7];
	val[0]=p.X;
			val[1]=p.Y;
			val[2]=p.Z;
	NativeMethods.MQScene_FloatValue(this,(int)MQScene.Convert3dToScreen,val);
	w = val[6];
	return new Point(val[3],val[4],val[5]);
		}

		/// <summary>
		/// MQPoint ConvertScreenTo3D(const MQPoint&amp; p)
		/// </summary>
		public Point ConvertScreenTo3d(ref Point p)
		{
			var val = new float[6];
	val[0]=p.X;
			val[1]=p.Y;
			val[2]=p.Z;
	NativeMethods.MQScene_FloatValue(this,(int)MQScene.ConvertScreenTo3d,val);
	return new Point(val[3],val[4],val[5]);
		}

		/// <summary>
		/// BOOL GetVisibleFace(MQObject obj, BOOL *visible)
		/// </summary>
		public bool GetVisibleFace(Object obj, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Bool)] bool[] visible)
		{
			return NativeMethods.MQScene_GetVisibleFace(this,obj,visible);
		}

	}

	/// <summary>
	/// MQObject
	/// </summary>
	public partial class Object
	{
		/// <summary>
		/// このオブジェクトの基になるネイティブなポインタを取得します。
		/// </summary>
		public IntPtr Handle
		{
			get;
			private set;
		}

		protected Object(IntPtr ptr)
		{
			this.Handle = ptr;
			Initialize();
		}

		public static implicit operator Object(IntPtr self)
		{
			return self == IntPtr.Zero ? null : new Object(self);
		}

		public static implicit operator IntPtr(Object self)
		{
			return self.Handle;
		}

		partial void Initialize();

		/// <summary>
		/// MQObject Clone()
		/// </summary>
		public Object Clone()
		{
			return NativeMethods.MQObj_Clone(this);
		}

		/// <summary>
		/// void Merge(MQObject source)
		/// </summary>
		public void Merge(Object source)
		{
			NativeMethods.MQObj_Merge(this, source);
		}

		/// <summary>
		/// void Freeze(DWORD flag)
		/// </summary>
		public void Freeze(ObjectFreeze flag)
		{
			NativeMethods.MQObj_Freeze(this, (uint)flag);
		}

		/// <summary>
		/// void GetName(char *buffer, int size)
		/// </summary>
		public void GetName(StringBuilder buffer, int size)
		{
			NativeMethods.MQObj_GetName(this, buffer, size);
		}

		/// <summary>
		/// int GetVertexCount()
		/// </summary>
		public int VertexCount
		{
			get
			{
				return NativeMethods.MQObj_GetVertexCount(this);
			}
		}

		/// <summary>
		/// MQPoint GetVertex(int index)
		/// </summary>
		public Point GetVertex(int index)
		{
			var pts = new Point();
			NativeMethods.MQObj_GetVertex(this, index, ref pts);
			return pts;
		}

		/// <summary>
		/// void SetVertex(int index, MQPoint pts)
		/// </summary>
		public void SetVertex(int index, Point pts)
		{
			NativeMethods.MQObj_SetVertex(this, index, ref pts);
		}

		/// <summary>
		/// void GetVertexArray(MQPoint *ptsarray)
		/// </summary>
		public void GetVertexArray(Point[] ptsarray)
		{
			NativeMethods.MQObj_GetVertexArray(this, ptsarray);
		}

		/// <summary>
		/// int AddVertex(MQPoint p)
		/// </summary>
		public int AddVertex(Point p)
		{
			return NativeMethods.MQObj_AddVertex(this, ref p);
		}

		/// <summary>
		/// BOOL DeleteVertex(int index)
		/// </summary>
		public bool DeleteVertex(int index)
		{
			return NativeMethods.MQObj_DeleteVertex(this, index, true);
		}

		/// <summary>
		/// int GetVertexRefCount(int index)
		/// </summary>
		public int GetVertexRefCount(int index)
		{
			return NativeMethods.MQObj_GetVertexRefCount(this, index);
		}

		/// <summary>
		/// UINT GetVertexUniqueID(int index)
		/// </summary>
		public uint GetVertexUniqueId(int index)
		{
			return NativeMethods.MQObj_GetVertexUniqueID(this, index);
		}

		/// <summary>
		/// int GetVertexIndexFromUniqueID(UINT unique_id)
		/// </summary>
		public int GetVertexIndexFromUniqueId(uint unique_id)
		{
			return NativeMethods.MQObj_GetVertexIndexFromUniqueID(this, unique_id);
		}

		/// <summary>
		/// float GetVertexWeight(int index)
		/// </summary>
		public float GetVertexWeight(int index)
		{
			return NativeMethods.MQObj_GetVertexWeight(this, index);
		}

		/// <summary>
		/// void SetVertexWeight(int index, float value)
		/// </summary>
		public void SetVertexWeight(int index, float value)
		{
			NativeMethods.MQObj_SetVertexWeight(this, index, value);
		}

		/// <summary>
		/// void CopyVertexAttribute(int vert1, MQObject obj2, int vert2)
		/// </summary>
		public void CopyVertexAttribute(int vert1, Object obj2, int vert2)
		{
			NativeMethods.MQObj_CopyVertexAttribute(this, vert1, obj2, vert2);
		}

		/// <summary>
		/// int GetFaceCount()
		/// </summary>
		public int FaceCount
		{
			get
			{
				return NativeMethods.MQObj_GetFaceCount(this);
			}
		}

		/// <summary>
		/// int GetFacePointCount(int face)
		/// </summary>
		public int GetFacePointCount(int face)
		{
			return NativeMethods.MQObj_GetFacePointCount(this, face);
		}

		/// <summary>
		/// void GetFacePointArray(int face, int *vertex)
		/// </summary>
		public void GetFacePointArray(int face, int[] vertex)
		{
			NativeMethods.MQObj_GetFacePointArray(this, face, vertex);
		}

		/// <summary>
		/// void GetFaceCoordinateArray(int face, MQCoordinate *uvarray)
		/// </summary>
		public void GetFaceCoordinateArray(int face, Coordinate[] uvarray)
		{
			NativeMethods.MQObj_GetFaceCoordinateArray(this, face, uvarray);
		}

		/// <summary>
		/// int GetFaceMaterial(int face)
		/// </summary>
		public int GetFaceMaterial(int face)
		{
			return NativeMethods.MQObj_GetFaceMaterial(this, face);
		}

		/// <summary>
		/// UINT GetFaceUniqueID(int face)
		/// </summary>
		public uint GetFaceUniqueId(int face)
		{
			return NativeMethods.MQObj_GetFaceUniqueID(this, face);
		}

		/// <summary>
		/// int GetFaceIndexFromUniqueID(UINT unique_id)
		/// </summary>
		public int GetFaceIndexFromUniqueId(uint unique_id)
		{
			return NativeMethods.MQObj_GetFaceIndexFromUniqueID(this, unique_id);
		}

		/// <summary>
		/// void SetName(const char *buffer)
		/// </summary>
		public void SetName(string buffer)
		{
			NativeMethods.MQObj_SetName(this, buffer);
		}

		/// <summary>
		/// int AddFace(int count, int *index)
		/// </summary>
		public int AddFace(int count, int[] index)
		{
			return NativeMethods.MQObj_AddFace(this, count, index);
		}

		/// <summary>
		/// BOOL DeleteFace(int index, bool delete_vertex)
		/// </summary>
		public bool DeleteFace(int index, bool deleteVertex)
		{
			return NativeMethods.MQObj_DeleteFace(this, index, deleteVertex);
		}

		/// <summary>
		/// BOOL InvertFace(int index)
		/// </summary>
		public bool InvertFace(int index)
		{
			return NativeMethods.MQObj_InvertFace(this, index);
		}

		/// <summary>
		/// void SetFaceMaterial(int face, int material)
		/// </summary>
		public void SetFaceMaterial(int face, int material)
		{
			NativeMethods.MQObj_SetFaceMaterial(this, face, material);
		}

		/// <summary>
		/// void SetFaceCoordinateArray(int face, MQCoordinate *uvarray)
		/// </summary>
		public void SetFaceCoordinateArray(int face, Coordinate[] uvarray)
		{
			NativeMethods.MQObj_SetFaceCoordinateArray(this, face, uvarray);
		}

		/// <summary>
		/// DWORD GetFaceVertexColor(int face, int vertex)
		/// </summary>
		public uint GetFaceVertexColor(int face, int vertex)
		{
			return NativeMethods.MQObj_GetFaceVertexColor(this, face, vertex);
		}

		/// <summary>
		/// void SetFaceVertexColor(int face, int vertex, DWORD color)
		/// </summary>
		public void SetFaceVertexColor(int face, int vertex, uint color)
		{
			NativeMethods.MQObj_SetFaceVertexColor(this, face, vertex, color);
		}

		/// <summary>
		/// void OptimizeVertex(float distance, MQBool *apply)
		/// </summary>
		public void OptimizeVertex(float distance, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1)] bool[] apply)
		{
			NativeMethods.MQObj_OptimizeVertex(this, distance, apply);
		}

		/// <summary>
		/// void Compact()
		/// </summary>
		public void Compact()
		{
			NativeMethods.MQObj_Compact(this);
		}

		/// <summary>
		/// DWORD GetVisible()
		/// </summary>
		public uint Visible
		{
			get
			{
				return NativeMethods.MQObj_GetVisible(this);
			}
			set
			{
				var visible = value;
				NativeMethods.MQObj_SetVisible(this, visible);
			}
		}

		/// <summary>
		/// DWORD GetPatchType()
		/// </summary>
		public ObjectPatch PatchType
		{
			get
			{
				return (ObjectPatch)NativeMethods.MQObj_GetPatchType(this);
			}
			set
			{
				var type = value;
				NativeMethods.MQObj_SetPatchType(this, (uint)type);
			}
		}

		/// <summary>
		/// int GetPatchSegment()
		/// </summary>
		public int PatchSegment
		{
			get
			{
				return NativeMethods.MQObj_GetPatchSegment(this);
			}
			set
			{
				var segment = value;
				NativeMethods.MQObj_SetPatchSegment(this, segment);
			}
		}

		/// <summary>
		/// int GetShading()
		/// </summary>
		public ObjectShade Shading
		{
			get
			{
				return (ObjectShade)NativeMethods.MQObj_GetShading(this);
			}
			set
			{
				var type = value;
				NativeMethods.MQObj_SetShading(this, (int)type);
			}
		}

		/// <summary>
		/// float GetSmoothAngle()
		/// </summary>
		public float SmoothAngle
		{
			get
			{
				return NativeMethods.MQObj_GetSmoothAngle(this);
			}
			set
			{
				var degree = value;
				NativeMethods.MQObj_SetSmoothAngle(this, degree);
			}
		}

		/// <summary>
		/// int GetMirrorType()
		/// </summary>
		public ObjectMirror MirrorType
		{
			get
			{
				return (ObjectMirror)NativeMethods.MQObj_GetMirrorType(this);
			}
			set
			{
				var type = value;
				NativeMethods.MQObj_SetMirrorType(this, (int)type);
			}
		}

		/// <summary>
		/// DWORD GetMirrorAxis()
		/// </summary>
		public ObjectMirrorAxis MirrorAxis
		{
			get
			{
				return (ObjectMirrorAxis)NativeMethods.MQObj_GetMirrorAxis(this);
			}
			set
			{
				var axis = value;
				NativeMethods.MQObj_SetMirrorAxis(this, (uint)axis);
			}
		}

		/// <summary>
		/// float GetMirrorDistance()
		/// </summary>
		public float MirrorDistance
		{
			get
			{
				return NativeMethods.MQObj_GetMirrorDistance(this);
			}
			set
			{
				var dis = value;
				NativeMethods.MQObj_SetMirrorDistance(this, dis);
			}
		}

		/// <summary>
		/// int GetLatheType()
		/// </summary>
		public ObjectLathe LatheType
		{
			get
			{
				return (ObjectLathe)NativeMethods.MQObj_GetLatheType(this);
			}
			set
			{
				var type = value;
				NativeMethods.MQObj_SetLatheType(this, (int)type);
			}
		}

		/// <summary>
		/// DWORD GetLatheAxis()
		/// </summary>
		public ObjectLatheAxis LatheAxis
		{
			get
			{
				return (ObjectLatheAxis)NativeMethods.MQObj_GetLatheAxis(this);
			}
			set
			{
				var axis = value;
				NativeMethods.MQObj_SetLatheAxis(this, (uint)axis);
			}
		}

		/// <summary>
		/// int GetLatheSegment()
		/// </summary>
		public int LatheSegment
		{
			get
			{
				return NativeMethods.MQObj_GetLatheSegment(this);
			}
			set
			{
				var segment = value;
				NativeMethods.MQObj_SetLatheSegment(this, segment);
			}
		}

		/// <summary>
		/// UINT GetUniqueID()
		/// </summary>
		public uint UniqueId
		{
			get
			{
				return (uint)NativeMethods.MQObj_GetIntValue(this, (int)ObjId.UniqueId);
			}
		}

		/// <summary>
		/// int GetDepth()
		/// </summary>
		public int Depth
		{
			get
			{
				return NativeMethods.MQObj_GetIntValue(this, (int)ObjId.Depth);
			}
			set
			{
				var depth = value;
				NativeMethods.MQObj_SetIntValue(this, (int)ObjId.Depth, depth);
			}
		}

		/// <summary>
		/// BOOL GetFolding()
		/// </summary>
		public bool Folding
		{
			get
			{
				return NativeMethods.MQObj_GetIntValue(this, (int)ObjId.Folding) != 0;
			}
			set
			{
				var flag = value;
				NativeMethods.MQObj_SetIntValue(this, (int)ObjId.Folding, flag ? 1 : 0);
			}
		}

		/// <summary>
		/// BOOL GetLocking()
		/// </summary>
		public bool Locking
		{
			get
			{
				return NativeMethods.MQObj_GetIntValue(this, (int)ObjId.Locking) != 0;
			}
			set
			{
				var flag = value;
				NativeMethods.MQObj_SetIntValue(this, (int)ObjId.Locking, flag ? 1 : 0);
			}
		}

		/// <summary>
		/// MQColor GetColor()
		/// </summary>
		public Color Color
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQObj_GetFloatArray(this, (int)ObjId.Color, val);
				return new Color(val[0],val[1],val[2]);
			}
			set
			{
				var color = value;
				var val = new float[3];
				val[0]=color.R;
				val[1]=color.G;
				val[2]=color.B;
				NativeMethods.MQObj_SetFloatArray(this, (int)ObjId.Color, val);
			}
		}

		/// <summary>
		/// BOOL GetColorValid()
		/// </summary>
		public bool ColorValid
		{
			get
			{
				return NativeMethods.MQObj_GetIntValue(this, (int)ObjId.ColorValid) != 0;
			}
			set
			{
				var flag = value;
				NativeMethods.MQObj_SetIntValue(this, (int)ObjId.ColorValid, flag ? 1 : 0);
			}
		}

		/// <summary>
		/// MQPoint GetScaling()
		/// </summary>
		public Point Scaling
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQObj_GetFloatArray(this, (int)ObjId.Scaling, val);
				return new Point(val[0],val[1],val[2]);
			}
			set
			{
				var scale = value;
				var val = new float[3];
				val[0]=scale.X;
				val[1]=scale.Y;
				val[2]=scale.Z;
				NativeMethods.MQObj_SetFloatArray(this, (int)ObjId.Scaling, val);
			}
		}

		/// <summary>
		/// MQAngle GetRotation()
		/// </summary>
		public Angle Rotation
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQObj_GetFloatArray(this, (int)ObjId.Rotation, val);
				return new Angle(val[0],val[1],val[2]);
			}
			set
			{
				var angle = value;
				var val = new float[3];
				val[0]=angle.Head;
				val[1]=angle.Pitch;
				val[2]=angle.Bank;
				NativeMethods.MQObj_SetFloatArray(this, (int)ObjId.Rotation, val);
			}
		}

		/// <summary>
		/// MQPoint GetTranslation()
		/// </summary>
		public Point Translation
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQObj_GetFloatArray(this, (int)ObjId.Translation, val);
				return new Point(val[0],val[1],val[2]);
			}
			set
			{
				var trans = value;
				var val = new float[3];
				val[0]=trans.X;
				val[1]=trans.Y;
				val[2]=trans.Z;
				NativeMethods.MQObj_SetFloatArray(this, (int)ObjId.Translation, val);
			}
		}

		/// <summary>
		/// MQMatrix GetLocalMatrix()
		/// </summary>
		public Matrix LocalMatrix
		{
			get
			{
				var mtx = new Matrix();
				var t = new float[16];
				NativeMethods.MQObj_GetFloatArray(this, (int)ObjId.LocalMatrix, t);
				mtx = new Matrix(t);
				return mtx;
			}
			set
			{
				var mtx = value;
				NativeMethods.MQObj_SetFloatArray(this, (int)ObjId.LocalMatrix, mtx.ToArray());;
			}
		}

		/// <summary>
		/// MQMatrix GetLocalInverseMatrix()
		/// </summary>
		public Matrix LocalInverseMatrix
		{
			get
			{
				var mtx = new Matrix();
			var t = new float[16];
			NativeMethods.MQObj_GetFloatArray(this, (int)ObjId.LocalInverseMatrix, t);
			mtx = new Matrix(t);
			return mtx;
			}
		}

	}

	/// <summary>
	/// MQMaterial
	/// </summary>
	public partial class Material
	{
		/// <summary>
		/// このオブジェクトの基になるネイティブなポインタを取得します。
		/// </summary>
		public IntPtr Handle
		{
			get;
			private set;
		}

		protected Material(IntPtr ptr)
		{
			this.Handle = ptr;
			Initialize();
		}

		public static implicit operator Material(IntPtr self)
		{
			return self == IntPtr.Zero ? null : new Material(self);
		}

		public static implicit operator IntPtr(Material self)
		{
			return self.Handle;
		}

		partial void Initialize();

		/// <summary>
		/// void GetName(char *buffer, int size)
		/// </summary>
		public void GetName(StringBuilder buffer, int size)
		{
			NativeMethods.MQMat_GetName(this, buffer, size);
		}

		/// <summary>
		/// UINT GetUniqueID()
		/// </summary>
		public uint UniqueId
		{
			get
			{
				return (uint)NativeMethods.MQMat_GetIntValue(this, (int)MatId.UniqueId);
			}
		}

		/// <summary>
		/// int GetShader()
		/// </summary>
		public MaterialShader Shader
		{
			get
			{
				return (MaterialShader)NativeMethods.MQMat_GetIntValue(this, (int)MatId.Shader);
			}
			set
			{
				var shader = value;
				NativeMethods.MQMat_SetIntValue(this, (int)MatId.Shader, (int)shader);
			}
		}

		/// <summary>
		/// int GetVertexColor()
		/// </summary>
		public MaterialVertexcolor VertexColor
		{
			get
			{
				return (MaterialVertexcolor)NativeMethods.MQMat_GetIntValue(this, (int)MatId.Vertexcolor);
			}
			set
			{
				NativeMethods.MQMat_SetIntValue(this, (int)MatId.Vertexcolor, (int)value);
			}
		}

		/// <summary>
		/// MQColor GetColor()
		/// </summary>
		public Color Color
		{
			get
			{
				var color = new Color();
				NativeMethods.MQMat_GetColor(this, out color);
				return color;
			}
			set
			{
				var color = value;
				NativeMethods.MQMat_SetColor(this, out color);
			}
		}

		/// <summary>
		/// float GetAlpha()
		/// </summary>
		public float Alpha
		{
			get
			{
				return NativeMethods.MQMat_GetAlpha(this);
			}
			set
			{
				NativeMethods.MQMat_SetAlpha(this, value);
			}
		}

		/// <summary>
		/// float GetDiffuse()
		/// </summary>
		public float Diffuse
		{
			get
			{
				return NativeMethods.MQMat_GetDiffuse(this);
			}
			set
			{
				NativeMethods.MQMat_SetDiffuse(this, value);
			}
		}

		/// <summary>
		/// float GetAmbient()
		/// </summary>
		public float Ambient
		{
			get
			{
				return NativeMethods.MQMat_GetAmbient(this);
			}
			set
			{
				NativeMethods.MQMat_SetAmbient(this, value);
			}
		}

		/// <summary>
		/// float GetEmission()
		/// </summary>
		public float Emission
		{
			get
			{
				return NativeMethods.MQMat_GetEmission(this);
			}
			set
			{
				NativeMethods.MQMat_SetEmission(this, value);
			}
		}

		/// <summary>
		/// float GetSpecular()
		/// </summary>
		public float Specular
		{
			get
			{
				return NativeMethods.MQMat_GetSpecular(this);
			}
			set
			{
				NativeMethods.MQMat_SetSpecular(this, value);
			}
		}

		/// <summary>
		/// float GetPower()
		/// </summary>
		public float Power
		{
			get
			{
				return NativeMethods.MQMat_GetPower(this);
			}
			set
			{
				NativeMethods.MQMat_SetPower(this, value);
			}
		}

		/// <summary>
		/// int GetMappingType()
		/// </summary>
		public MaterialProjection MappingType
		{
			get
			{
				return (MaterialProjection)NativeMethods.MQMat_GetIntValue(this, (int)MatId.Mapproj);
			}
			set
			{
				var type = value;
				NativeMethods.MQMat_SetIntValue(this, (int)MatId.Mapproj, (int)type);
			}
		}

		/// <summary>
		/// MQPoint GetMappingPosition()
		/// </summary>
		public Point MappingPosition
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQMat_GetFloatArray(this, (int)MatId.MapprojPosition, val);
				return new Point(val[0], val[1], val[2]);
			}
			set
			{
				var pos = value;
				var val = new float[3];
				val[0]=pos.X;
				val[1]=pos.Y;
				val[2]=pos.Z;
				NativeMethods.MQMat_SetFloatArray(this, (int)MatId.MapprojPosition, val);
			}
		}

		/// <summary>
		/// MQPoint GetMappingScaling()
		/// </summary>
		public Point MappingScaling
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQMat_GetFloatArray(this, (int)MatId.MapprojScaling,  val);
				return new Point(val[0], val[1], val[2]);
			}
			set
			{
				var scale = value;
				var val = new float[3];
				val[0]=scale.X;
				val[1]=scale.Y;
				val[2]=scale.Z;
				NativeMethods.MQMat_SetFloatArray(this, (int)MatId.MapprojScaling, val);
			}
		}

		/// <summary>
		/// MQAngle GetMappingAngle()
		/// </summary>
		public Angle MappingAngle
		{
			get
			{
				var val = new float[3];
				NativeMethods.MQMat_GetFloatArray(this, (int)MatId.MapprojAngle,    val);
				return new Angle(val[0], val[1], val[2]);
			}
			set
			{
				var angle = value;
				var val = new float[3];
				val[0]=angle.Head;
				val[1]=angle.Pitch;
				val[2]=angle.Bank;
				NativeMethods.MQMat_SetFloatArray(this, (int)MatId.MapprojAngle, val);
			}
		}

		/// <summary>
		/// void GetTextureName(char *buffer, int size)
		/// </summary>
		public void GetTextureName(StringBuilder buffer, int size)
		{
			NativeMethods.MQMat_GetTextureName(this, buffer, size);
		}

		/// <summary>
		/// void GetAlphaName(char *buffer, int size)
		/// </summary>
		public void GetAlphaName(StringBuilder buffer, int size)
		{
			NativeMethods.MQMat_GetAlphaName(this, buffer, size);
		}

		/// <summary>
		/// void GetBumpName(char *buffer, int size)
		/// </summary>
		public void GetBumpName(StringBuilder buffer, int size)
		{
			NativeMethods.MQMat_GetBumpName(this, buffer, size);
		}

		/// <summary>
		/// void SetName(const char *name)
		/// </summary>
		public void SetName(string name)
		{
			NativeMethods.MQMat_SetName(this, name);
		}

		/// <summary>
		/// void SetTextureName(const char *name)
		/// </summary>
		public void SetTextureName(string name)
		{
			NativeMethods.MQMat_SetTextureName(this, name);
		}

		/// <summary>
		/// void SetAlphaName(const char *name)
		/// </summary>
		public void SetAlphaName(string name)
		{
			NativeMethods.MQMat_SetAlphaName(this, name);
		}

		/// <summary>
		/// void SetBumpName(const char *name)
		/// </summary>
		public void SetBumpName(string name)
		{
			NativeMethods.MQMat_SetBumpName(this, name);
		}

	}

}

