using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Metasequoia
{
	/// <summary>
	/// Metasequoia プラグイン API への直接的なアクセスを提供します。
	/// </summary>
	public static class NativeMethods
	{
		[SuppressUnmanagedCodeSecurity, DllImport("kernel32", CharSet = CharSet.Ansi)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
		[SuppressUnmanagedCodeSecurity, DllImport("kernel32", CharSet = CharSet.Ansi)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

		static T GetDelegate<T>(IntPtr hModule, string entryPoint)
		{
			var proc = GetProcAddress(hModule, entryPoint);

			if (proc == IntPtr.Zero)
				throw new EntryPointNotFoundException(entryPoint);

			return (T)(object)Marshal.GetDelegateForFunctionPointer(proc, typeof(T));
		}

		internal static bool Initialize()
		{
			try
			{
				var hModule = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
	
				MQ_GetWindowHandle = GetDelegate<MQ_GetWindowHandleDelegate>(hModule, "MQ_GetWindowHandle");
				MQ_CreateObject = GetDelegate<MQ_CreateObjectDelegate>(hModule, "MQ_CreateObject");
				MQ_CreateMaterial = GetDelegate<MQ_CreateMaterialDelegate>(hModule, "MQ_CreateMaterial");
				MQ_ShowFileDialog = GetDelegate<MQ_ShowFileDialogDelegate>(hModule, "MQ_ShowFileDialog");
				MQ_ImportAxis = GetDelegate<MQ_ImportAxisDelegate>(hModule, "MQ_ImportAxis");
				MQ_ExportAxis = GetDelegate<MQ_ExportAxisDelegate>(hModule, "MQ_ExportAxis");
				MQ_LoadImage = GetDelegate<MQ_LoadImageDelegate>(hModule, "MQ_LoadImage");
				MQ_GetSystemPath = GetDelegate<MQ_GetSystemPathDelegate>(hModule, "MQ_GetSystemPath");
				MQ_RefreshView = GetDelegate<MQ_RefreshViewDelegate>(hModule, "MQ_RefreshView");
				MQ_StationCallback = GetDelegate<MQ_StationCallbackDelegate>(hModule, "MQ_StationCallback");
				MQ_SendMessage = GetDelegate<MQ_SendMessageDelegate>(hModule, "MQ_SendMessage");
				MQDoc_GetObjectCount = GetDelegate<MQDoc_GetObjectCountDelegate>(hModule, "MQDoc_GetObjectCount");
				MQDoc_GetObject = GetDelegate<MQDoc_GetObjectDelegate>(hModule, "MQDoc_GetObject");
				MQDoc_GetCurrentObjectIndex = GetDelegate<MQDoc_GetCurrentObjectIndexDelegate>(hModule, "MQDoc_GetCurrentObjectIndex");
				MQDoc_SetCurrentObjectIndex = GetDelegate<MQDoc_SetCurrentObjectIndexDelegate>(hModule, "MQDoc_SetCurrentObjectIndex");
				MQDoc_AddObject = GetDelegate<MQDoc_AddObjectDelegate>(hModule, "MQDoc_AddObject");
				MQDoc_DeleteObject = GetDelegate<MQDoc_DeleteObjectDelegate>(hModule, "MQDoc_DeleteObject");
				MQDoc_GetObjectIndex = GetDelegate<MQDoc_GetObjectIndexDelegate>(hModule, "MQDoc_GetObjectIndex");
				MQDoc_GetMaterialCount = GetDelegate<MQDoc_GetMaterialCountDelegate>(hModule, "MQDoc_GetMaterialCount");
				MQDoc_GetMaterial = GetDelegate<MQDoc_GetMaterialDelegate>(hModule, "MQDoc_GetMaterial");
				MQDoc_GetCurrentMaterialIndex = GetDelegate<MQDoc_GetCurrentMaterialIndexDelegate>(hModule, "MQDoc_GetCurrentMaterialIndex");
				MQDoc_SetCurrentMaterialIndex = GetDelegate<MQDoc_SetCurrentMaterialIndexDelegate>(hModule, "MQDoc_SetCurrentMaterialIndex");
				MQDoc_AddMaterial = GetDelegate<MQDoc_AddMaterialDelegate>(hModule, "MQDoc_AddMaterial");
				MQDoc_DeleteMaterial = GetDelegate<MQDoc_DeleteMaterialDelegate>(hModule, "MQDoc_DeleteMaterial");
				MQDoc_FindMappingFile = GetDelegate<MQDoc_FindMappingFileDelegate>(hModule, "MQDoc_FindMappingFile");
				MQDoc_Compact = GetDelegate<MQDoc_CompactDelegate>(hModule, "MQDoc_Compact");
				MQDoc_ClearSelect = GetDelegate<MQDoc_ClearSelectDelegate>(hModule, "MQDoc_ClearSelect");
				MQDoc_AddSelectVertex = GetDelegate<MQDoc_AddSelectVertexDelegate>(hModule, "MQDoc_AddSelectVertex");
				MQDoc_DeleteSelectVertex = GetDelegate<MQDoc_DeleteSelectVertexDelegate>(hModule, "MQDoc_DeleteSelectVertex");
				MQDoc_IsSelectVertex = GetDelegate<MQDoc_IsSelectVertexDelegate>(hModule, "MQDoc_IsSelectVertex");
				MQDoc_AddSelectLine = GetDelegate<MQDoc_AddSelectLineDelegate>(hModule, "MQDoc_AddSelectLine");
				MQDoc_DeleteSelectLine = GetDelegate<MQDoc_DeleteSelectLineDelegate>(hModule, "MQDoc_DeleteSelectLine");
				MQDoc_IsSelectLine = GetDelegate<MQDoc_IsSelectLineDelegate>(hModule, "MQDoc_IsSelectLine");
				MQDoc_AddSelectFace = GetDelegate<MQDoc_AddSelectFaceDelegate>(hModule, "MQDoc_AddSelectFace");
				MQDoc_DeleteSelectFace = GetDelegate<MQDoc_DeleteSelectFaceDelegate>(hModule, "MQDoc_DeleteSelectFace");
				MQDoc_IsSelectFace = GetDelegate<MQDoc_IsSelectFaceDelegate>(hModule, "MQDoc_IsSelectFace");
				MQDoc_AddSelectUVVertex = GetDelegate<MQDoc_AddSelectUVVertexDelegate>(hModule, "MQDoc_AddSelectUVVertex");
				MQDoc_DeleteSelectUVVertex = GetDelegate<MQDoc_DeleteSelectUVVertexDelegate>(hModule, "MQDoc_DeleteSelectUVVertex");
				MQDoc_IsSelectUVVertex = GetDelegate<MQDoc_IsSelectUVVertexDelegate>(hModule, "MQDoc_IsSelectUVVertex");
				MQDoc_GetScene = GetDelegate<MQDoc_GetSceneDelegate>(hModule, "MQDoc_GetScene");
				MQDoc_GetParentObject = GetDelegate<MQDoc_GetParentObjectDelegate>(hModule, "MQDoc_GetParentObject");
				MQDoc_GetChildObjectCount = GetDelegate<MQDoc_GetChildObjectCountDelegate>(hModule, "MQDoc_GetChildObjectCount");
				MQDoc_GetChildObject = GetDelegate<MQDoc_GetChildObjectDelegate>(hModule, "MQDoc_GetChildObject");
				MQDoc_GetGlobalMatrix = GetDelegate<MQDoc_GetGlobalMatrixDelegate>(hModule, "MQDoc_GetGlobalMatrix");
				MQDoc_GetGlobalInverseMatrix = GetDelegate<MQDoc_GetGlobalInverseMatrixDelegate>(hModule, "MQDoc_GetGlobalInverseMatrix");
				MQDoc_InsertObject = GetDelegate<MQDoc_InsertObjectDelegate>(hModule, "MQDoc_InsertObject");
				MQScene_InitSize = GetDelegate<MQScene_InitSizeDelegate>(hModule, "MQScene_InitSize");
				MQScene_GetProjMatrix = GetDelegate<MQScene_GetProjMatrixDelegate>(hModule, "MQScene_GetProjMatrix");
				MQScene_GetViewMatrix = GetDelegate<MQScene_GetViewMatrixDelegate>(hModule, "MQScene_GetViewMatrix");
				MQScene_FloatValue = GetDelegate<MQScene_FloatValueDelegate>(hModule, "MQScene_FloatValue");
				MQScene_GetVisibleFace = GetDelegate<MQScene_GetVisibleFaceDelegate>(hModule, "MQScene_GetVisibleFace");
				MQObj_Delete = GetDelegate<MQObj_DeleteDelegate>(hModule, "MQObj_Delete");
				MQObj_Clone = GetDelegate<MQObj_CloneDelegate>(hModule, "MQObj_Clone");
				MQObj_Merge = GetDelegate<MQObj_MergeDelegate>(hModule, "MQObj_Merge");
				MQObj_Freeze = GetDelegate<MQObj_FreezeDelegate>(hModule, "MQObj_Freeze");
				MQObj_GetName = GetDelegate<MQObj_GetNameDelegate>(hModule, "MQObj_GetName");
				MQObj_GetVertexCount = GetDelegate<MQObj_GetVertexCountDelegate>(hModule, "MQObj_GetVertexCount");
				MQObj_GetVertex = GetDelegate<MQObj_GetVertexDelegate>(hModule, "MQObj_GetVertex");
				MQObj_SetVertex = GetDelegate<MQObj_SetVertexDelegate>(hModule, "MQObj_SetVertex");
				MQObj_GetVertexArray = GetDelegate<MQObj_GetVertexArrayDelegate>(hModule, "MQObj_GetVertexArray");
				MQObj_GetFaceCount = GetDelegate<MQObj_GetFaceCountDelegate>(hModule, "MQObj_GetFaceCount");
				MQObj_GetFacePointCount = GetDelegate<MQObj_GetFacePointCountDelegate>(hModule, "MQObj_GetFacePointCount");
				MQObj_GetFacePointArray = GetDelegate<MQObj_GetFacePointArrayDelegate>(hModule, "MQObj_GetFacePointArray");
				MQObj_GetFaceCoordinateArray = GetDelegate<MQObj_GetFaceCoordinateArrayDelegate>(hModule, "MQObj_GetFaceCoordinateArray");
				MQObj_GetFaceMaterial = GetDelegate<MQObj_GetFaceMaterialDelegate>(hModule, "MQObj_GetFaceMaterial");
				MQObj_GetFaceUniqueID = GetDelegate<MQObj_GetFaceUniqueIDDelegate>(hModule, "MQObj_GetFaceUniqueID");
				MQObj_GetFaceIndexFromUniqueID = GetDelegate<MQObj_GetFaceIndexFromUniqueIDDelegate>(hModule, "MQObj_GetFaceIndexFromUniqueID");
				MQObj_SetName = GetDelegate<MQObj_SetNameDelegate>(hModule, "MQObj_SetName");
				MQObj_AddVertex = GetDelegate<MQObj_AddVertexDelegate>(hModule, "MQObj_AddVertex");
				MQObj_DeleteVertex = GetDelegate<MQObj_DeleteVertexDelegate>(hModule, "MQObj_DeleteVertex");
				MQObj_GetVertexRefCount = GetDelegate<MQObj_GetVertexRefCountDelegate>(hModule, "MQObj_GetVertexRefCount");
				MQObj_GetVertexUniqueID = GetDelegate<MQObj_GetVertexUniqueIDDelegate>(hModule, "MQObj_GetVertexUniqueID");
				MQObj_GetVertexIndexFromUniqueID = GetDelegate<MQObj_GetVertexIndexFromUniqueIDDelegate>(hModule, "MQObj_GetVertexIndexFromUniqueID");
				MQObj_GetVertexWeight = GetDelegate<MQObj_GetVertexWeightDelegate>(hModule, "MQObj_GetVertexWeight");
				MQObj_SetVertexWeight = GetDelegate<MQObj_SetVertexWeightDelegate>(hModule, "MQObj_SetVertexWeight");
				MQObj_CopyVertexAttribute = GetDelegate<MQObj_CopyVertexAttributeDelegate>(hModule, "MQObj_CopyVertexAttribute");
				MQObj_AddFace = GetDelegate<MQObj_AddFaceDelegate>(hModule, "MQObj_AddFace");
				MQObj_DeleteFace = GetDelegate<MQObj_DeleteFaceDelegate>(hModule, "MQObj_DeleteFace");
				MQObj_InvertFace = GetDelegate<MQObj_InvertFaceDelegate>(hModule, "MQObj_InvertFace");
				MQObj_SetFaceMaterial = GetDelegate<MQObj_SetFaceMaterialDelegate>(hModule, "MQObj_SetFaceMaterial");
				MQObj_SetFaceCoordinateArray = GetDelegate<MQObj_SetFaceCoordinateArrayDelegate>(hModule, "MQObj_SetFaceCoordinateArray");
				MQObj_GetFaceVertexColor = GetDelegate<MQObj_GetFaceVertexColorDelegate>(hModule, "MQObj_GetFaceVertexColor");
				MQObj_SetFaceVertexColor = GetDelegate<MQObj_SetFaceVertexColorDelegate>(hModule, "MQObj_SetFaceVertexColor");
				MQObj_OptimizeVertex = GetDelegate<MQObj_OptimizeVertexDelegate>(hModule, "MQObj_OptimizeVertex");
				MQObj_Compact = GetDelegate<MQObj_CompactDelegate>(hModule, "MQObj_Compact");
				MQObj_GetVisible = GetDelegate<MQObj_GetVisibleDelegate>(hModule, "MQObj_GetVisible");
				MQObj_SetVisible = GetDelegate<MQObj_SetVisibleDelegate>(hModule, "MQObj_SetVisible");
				MQObj_GetPatchType = GetDelegate<MQObj_GetPatchTypeDelegate>(hModule, "MQObj_GetPatchType");
				MQObj_SetPatchType = GetDelegate<MQObj_SetPatchTypeDelegate>(hModule, "MQObj_SetPatchType");
				MQObj_GetPatchSegment = GetDelegate<MQObj_GetPatchSegmentDelegate>(hModule, "MQObj_GetPatchSegment");
				MQObj_SetPatchSegment = GetDelegate<MQObj_SetPatchSegmentDelegate>(hModule, "MQObj_SetPatchSegment");
				MQObj_GetShading = GetDelegate<MQObj_GetShadingDelegate>(hModule, "MQObj_GetShading");
				MQObj_SetShading = GetDelegate<MQObj_SetShadingDelegate>(hModule, "MQObj_SetShading");
				MQObj_GetSmoothAngle = GetDelegate<MQObj_GetSmoothAngleDelegate>(hModule, "MQObj_GetSmoothAngle");
				MQObj_SetSmoothAngle = GetDelegate<MQObj_SetSmoothAngleDelegate>(hModule, "MQObj_SetSmoothAngle");
				MQObj_GetMirrorType = GetDelegate<MQObj_GetMirrorTypeDelegate>(hModule, "MQObj_GetMirrorType");
				MQObj_SetMirrorType = GetDelegate<MQObj_SetMirrorTypeDelegate>(hModule, "MQObj_SetMirrorType");
				MQObj_GetMirrorAxis = GetDelegate<MQObj_GetMirrorAxisDelegate>(hModule, "MQObj_GetMirrorAxis");
				MQObj_SetMirrorAxis = GetDelegate<MQObj_SetMirrorAxisDelegate>(hModule, "MQObj_SetMirrorAxis");
				MQObj_GetMirrorDistance = GetDelegate<MQObj_GetMirrorDistanceDelegate>(hModule, "MQObj_GetMirrorDistance");
				MQObj_SetMirrorDistance = GetDelegate<MQObj_SetMirrorDistanceDelegate>(hModule, "MQObj_SetMirrorDistance");
				MQObj_GetLatheType = GetDelegate<MQObj_GetLatheTypeDelegate>(hModule, "MQObj_GetLatheType");
				MQObj_SetLatheType = GetDelegate<MQObj_SetLatheTypeDelegate>(hModule, "MQObj_SetLatheType");
				MQObj_GetLatheAxis = GetDelegate<MQObj_GetLatheAxisDelegate>(hModule, "MQObj_GetLatheAxis");
				MQObj_SetLatheAxis = GetDelegate<MQObj_SetLatheAxisDelegate>(hModule, "MQObj_SetLatheAxis");
				MQObj_GetLatheSegment = GetDelegate<MQObj_GetLatheSegmentDelegate>(hModule, "MQObj_GetLatheSegment");
				MQObj_SetLatheSegment = GetDelegate<MQObj_SetLatheSegmentDelegate>(hModule, "MQObj_SetLatheSegment");
				MQObj_GetIntValue = GetDelegate<MQObj_GetIntValueDelegate>(hModule, "MQObj_GetIntValue");
				MQObj_GetFloatArray = GetDelegate<MQObj_GetFloatArrayDelegate>(hModule, "MQObj_GetFloatArray");
				MQObj_SetIntValue = GetDelegate<MQObj_SetIntValueDelegate>(hModule, "MQObj_SetIntValue");
				MQObj_SetFloatArray = GetDelegate<MQObj_SetFloatArrayDelegate>(hModule, "MQObj_SetFloatArray");
				MQMat_Delete = GetDelegate<MQMat_DeleteDelegate>(hModule, "MQMat_Delete");
				MQMat_GetIntValue = GetDelegate<MQMat_GetIntValueDelegate>(hModule, "MQMat_GetIntValue");
				MQMat_GetFloatArray = GetDelegate<MQMat_GetFloatArrayDelegate>(hModule, "MQMat_GetFloatArray");
				MQMat_GetName = GetDelegate<MQMat_GetNameDelegate>(hModule, "MQMat_GetName");
				MQMat_GetColor = GetDelegate<MQMat_GetColorDelegate>(hModule, "MQMat_GetColor");
				MQMat_GetAlpha = GetDelegate<MQMat_GetAlphaDelegate>(hModule, "MQMat_GetAlpha");
				MQMat_GetDiffuse = GetDelegate<MQMat_GetDiffuseDelegate>(hModule, "MQMat_GetDiffuse");
				MQMat_GetAmbient = GetDelegate<MQMat_GetAmbientDelegate>(hModule, "MQMat_GetAmbient");
				MQMat_GetEmission = GetDelegate<MQMat_GetEmissionDelegate>(hModule, "MQMat_GetEmission");
				MQMat_GetSpecular = GetDelegate<MQMat_GetSpecularDelegate>(hModule, "MQMat_GetSpecular");
				MQMat_GetPower = GetDelegate<MQMat_GetPowerDelegate>(hModule, "MQMat_GetPower");
				MQMat_GetTextureName = GetDelegate<MQMat_GetTextureNameDelegate>(hModule, "MQMat_GetTextureName");
				MQMat_GetAlphaName = GetDelegate<MQMat_GetAlphaNameDelegate>(hModule, "MQMat_GetAlphaName");
				MQMat_GetBumpName = GetDelegate<MQMat_GetBumpNameDelegate>(hModule, "MQMat_GetBumpName");
				MQMat_SetIntValue = GetDelegate<MQMat_SetIntValueDelegate>(hModule, "MQMat_SetIntValue");
				MQMat_SetFloatArray = GetDelegate<MQMat_SetFloatArrayDelegate>(hModule, "MQMat_SetFloatArray");
				MQMat_SetName = GetDelegate<MQMat_SetNameDelegate>(hModule, "MQMat_SetName");
				MQMat_SetColor = GetDelegate<MQMat_SetColorDelegate>(hModule, "MQMat_SetColor");
				MQMat_SetAlpha = GetDelegate<MQMat_SetAlphaDelegate>(hModule, "MQMat_SetAlpha");
				MQMat_SetDiffuse = GetDelegate<MQMat_SetDiffuseDelegate>(hModule, "MQMat_SetDiffuse");
				MQMat_SetAmbient = GetDelegate<MQMat_SetAmbientDelegate>(hModule, "MQMat_SetAmbient");
				MQMat_SetEmission = GetDelegate<MQMat_SetEmissionDelegate>(hModule, "MQMat_SetEmission");
				MQMat_SetSpecular = GetDelegate<MQMat_SetSpecularDelegate>(hModule, "MQMat_SetSpecular");
				MQMat_SetPower = GetDelegate<MQMat_SetPowerDelegate>(hModule, "MQMat_SetPower");
				MQMat_SetTextureName = GetDelegate<MQMat_SetTextureNameDelegate>(hModule, "MQMat_SetTextureName");
				MQMat_SetAlphaName = GetDelegate<MQMat_SetAlphaNameDelegate>(hModule, "MQMat_SetAlphaName");
				MQMat_SetBumpName = GetDelegate<MQMat_SetBumpNameDelegate>(hModule, "MQMat_SetBumpName");
				MQMatrix_FloatValue = GetDelegate<MQMatrix_FloatValueDelegate>(hModule, "MQMatrix_FloatValue");
				MQXmlElem_Value = GetDelegate<MQXmlElem_ValueDelegate>(hModule, "MQXmlElem_Value");
				
				return true;
			}
			catch (EntryPointNotFoundException)
			{
				return false;
			}
		}
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate IntPtr MQ_GetWindowHandleDelegate();
		public static MQ_GetWindowHandleDelegate MQ_GetWindowHandle;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate /* Object */ IntPtr MQ_CreateObjectDelegate();
		public static MQ_CreateObjectDelegate MQ_CreateObject;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate /* Material */ IntPtr MQ_CreateMaterialDelegate();
		public static MQ_CreateMaterialDelegate MQ_CreateMaterial;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQ_ShowFileDialogDelegate([MarshalAs(UnmanagedType.LPStr)] string title, ref FileDialogInfo info);
		public static MQ_ShowFileDialogDelegate MQ_ShowFileDialog;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQ_ImportAxisDelegate(ref FileDialogInfo info, ref Point pts, int pts_count);
		public static MQ_ImportAxisDelegate MQ_ImportAxis;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQ_ExportAxisDelegate(ref FileDialogInfo info, ref Point pts, int pts_count);
		public static MQ_ExportAxisDelegate MQ_ExportAxis;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQ_LoadImageDelegate([MarshalAs(UnmanagedType.LPStr)] string filename, out IntPtr header, out IntPtr buffer, uint reserved);
		public static MQ_LoadImageDelegate MQ_LoadImage;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQ_GetSystemPathDelegate([MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int type);
		public static MQ_GetSystemPathDelegate MQ_GetSystemPath;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQ_RefreshViewDelegate(/* void* */ IntPtr reserved);
		public static MQ_RefreshViewDelegate MQ_RefreshView;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQ_StationCallbackDelegate(StationCallbackProc proc, /* void* */ IntPtr option);
		public static MQ_StationCallbackDelegate MQ_StationCallback;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQ_SendMessageDelegate(int message_type, ref SendMessageInfo info);
		public static MQ_SendMessageDelegate MQ_SendMessage;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQDoc_GetObjectCountDelegate(/* Document */ IntPtr doc);
		public static MQDoc_GetObjectCountDelegate MQDoc_GetObjectCount;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate /* Object */ IntPtr MQDoc_GetObjectDelegate(/* Document */ IntPtr doc, int index);
		public static MQDoc_GetObjectDelegate MQDoc_GetObject;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQDoc_GetCurrentObjectIndexDelegate(/* Document */ IntPtr doc);
		public static MQDoc_GetCurrentObjectIndexDelegate MQDoc_GetCurrentObjectIndex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQDoc_SetCurrentObjectIndexDelegate(/* Document */ IntPtr doc, int index);
		public static MQDoc_SetCurrentObjectIndexDelegate MQDoc_SetCurrentObjectIndex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQDoc_AddObjectDelegate(/* Document */ IntPtr doc, /* Object */ IntPtr obj);
		public static MQDoc_AddObjectDelegate MQDoc_AddObject;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQDoc_DeleteObjectDelegate(/* Document */ IntPtr doc, int index);
		public static MQDoc_DeleteObjectDelegate MQDoc_DeleteObject;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQDoc_GetObjectIndexDelegate(/* Document */ IntPtr doc, /* Object */ IntPtr obj);
		public static MQDoc_GetObjectIndexDelegate MQDoc_GetObjectIndex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQDoc_GetMaterialCountDelegate(/* Document */ IntPtr doc);
		public static MQDoc_GetMaterialCountDelegate MQDoc_GetMaterialCount;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate /* Material */ IntPtr MQDoc_GetMaterialDelegate(/* Document */ IntPtr doc, int material);
		public static MQDoc_GetMaterialDelegate MQDoc_GetMaterial;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQDoc_GetCurrentMaterialIndexDelegate(/* Document */ IntPtr doc);
		public static MQDoc_GetCurrentMaterialIndexDelegate MQDoc_GetCurrentMaterialIndex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQDoc_SetCurrentMaterialIndexDelegate(/* Document */ IntPtr doc, int index);
		public static MQDoc_SetCurrentMaterialIndexDelegate MQDoc_SetCurrentMaterialIndex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQDoc_AddMaterialDelegate(/* Document */ IntPtr doc, /* Material */ IntPtr mat);
		public static MQDoc_AddMaterialDelegate MQDoc_AddMaterial;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQDoc_DeleteMaterialDelegate(/* Document */ IntPtr doc, int index);
		public static MQDoc_DeleteMaterialDelegate MQDoc_DeleteMaterial;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_FindMappingFileDelegate(/* Document */ IntPtr doc, [MarshalAs(UnmanagedType.LPStr)] StringBuilder out_path, [MarshalAs(UnmanagedType.LPStr)] string filename, uint map_type);
		public static MQDoc_FindMappingFileDelegate MQDoc_FindMappingFile;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQDoc_CompactDelegate(/* Document */ IntPtr doc);
		public static MQDoc_CompactDelegate MQDoc_Compact;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQDoc_ClearSelectDelegate(/* Document */ IntPtr doc, uint flag);
		public static MQDoc_ClearSelectDelegate MQDoc_ClearSelect;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_AddSelectVertexDelegate(/* Document */ IntPtr doc, int objindex, int vertindex);
		public static MQDoc_AddSelectVertexDelegate MQDoc_AddSelectVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_DeleteSelectVertexDelegate(/* Document */ IntPtr doc, int objindex, int vertindex);
		public static MQDoc_DeleteSelectVertexDelegate MQDoc_DeleteSelectVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_IsSelectVertexDelegate(/* Document */ IntPtr doc, int objindex, int vertindex);
		public static MQDoc_IsSelectVertexDelegate MQDoc_IsSelectVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_AddSelectLineDelegate(/* Document */ IntPtr doc, int objindex, int faceindex, int lineindex);
		public static MQDoc_AddSelectLineDelegate MQDoc_AddSelectLine;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_DeleteSelectLineDelegate(/* Document */ IntPtr doc, int objindex, int faceindex, int lineindex);
		public static MQDoc_DeleteSelectLineDelegate MQDoc_DeleteSelectLine;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_IsSelectLineDelegate(/* Document */ IntPtr doc, int objindex, int faceindex, int lineindex);
		public static MQDoc_IsSelectLineDelegate MQDoc_IsSelectLine;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_AddSelectFaceDelegate(/* Document */ IntPtr doc, int objindex, int faceindex);
		public static MQDoc_AddSelectFaceDelegate MQDoc_AddSelectFace;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_DeleteSelectFaceDelegate(/* Document */ IntPtr doc, int objindex, int faceindex);
		public static MQDoc_DeleteSelectFaceDelegate MQDoc_DeleteSelectFace;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_IsSelectFaceDelegate(/* Document */ IntPtr doc, int objindex, int faceindex);
		public static MQDoc_IsSelectFaceDelegate MQDoc_IsSelectFace;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_AddSelectUVVertexDelegate(/* Document */ IntPtr doc, int objindex, int faceindex, int vertindex);
		public static MQDoc_AddSelectUVVertexDelegate MQDoc_AddSelectUVVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_DeleteSelectUVVertexDelegate(/* Document */ IntPtr doc, int objindex, int faceindex, int vertindex);
		public static MQDoc_DeleteSelectUVVertexDelegate MQDoc_DeleteSelectUVVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQDoc_IsSelectUVVertexDelegate(/* Document */ IntPtr doc, int objindex, int faceindex, int vertindex);
		public static MQDoc_IsSelectUVVertexDelegate MQDoc_IsSelectUVVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate /* Scene */ IntPtr MQDoc_GetSceneDelegate(/* Document */ IntPtr doc, int index);
		public static MQDoc_GetSceneDelegate MQDoc_GetScene;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate /* Object */ IntPtr MQDoc_GetParentObjectDelegate(/* Document */ IntPtr doc, /* Object */ IntPtr obj);
		public static MQDoc_GetParentObjectDelegate MQDoc_GetParentObject;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQDoc_GetChildObjectCountDelegate(/* Document */ IntPtr doc, /* Object */ IntPtr obj);
		public static MQDoc_GetChildObjectCountDelegate MQDoc_GetChildObjectCount;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate /* Object */ IntPtr MQDoc_GetChildObjectDelegate(/* Document */ IntPtr doc, /* Object */ IntPtr obj, int index);
		public static MQDoc_GetChildObjectDelegate MQDoc_GetChildObject;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQDoc_GetGlobalMatrixDelegate(/* Document */ IntPtr doc, /* Object */ IntPtr obj, float[] matrix);
		public static MQDoc_GetGlobalMatrixDelegate MQDoc_GetGlobalMatrix;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQDoc_GetGlobalInverseMatrixDelegate(/* Document */ IntPtr doc, /* Object */ IntPtr obj, float[] matrix);
		public static MQDoc_GetGlobalInverseMatrixDelegate MQDoc_GetGlobalInverseMatrix;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQDoc_InsertObjectDelegate(/* Document */ IntPtr doc, /* Object */ IntPtr obj, /* Object */ IntPtr before);
		public static MQDoc_InsertObjectDelegate MQDoc_InsertObject;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQScene_InitSizeDelegate(/* Scene */ IntPtr scene, int width, int height);
		public static MQScene_InitSizeDelegate MQScene_InitSize;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQScene_GetProjMatrixDelegate(/* Scene */ IntPtr scene, float[] matrix);
		public static MQScene_GetProjMatrixDelegate MQScene_GetProjMatrix;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQScene_GetViewMatrixDelegate(/* Scene */ IntPtr scene, float[] matrix);
		public static MQScene_GetViewMatrixDelegate MQScene_GetViewMatrix;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQScene_FloatValueDelegate(/* Scene */ IntPtr scene, int type_id, float[] values);
		public static MQScene_FloatValueDelegate MQScene_FloatValue;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQScene_GetVisibleFaceDelegate(/* Scene */ IntPtr scene, /* Object */ IntPtr obj, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Bool)] bool[] visible);
		public static MQScene_GetVisibleFaceDelegate MQScene_GetVisibleFace;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_DeleteDelegate(/* Object */ IntPtr obj);
		public static MQObj_DeleteDelegate MQObj_Delete;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate /* Object */ IntPtr MQObj_CloneDelegate(/* Object */ IntPtr obj);
		public static MQObj_CloneDelegate MQObj_Clone;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_MergeDelegate(/* Object */ IntPtr dest, /* Object */ IntPtr source);
		public static MQObj_MergeDelegate MQObj_Merge;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_FreezeDelegate(/* Object */ IntPtr obj, uint flag);
		public static MQObj_FreezeDelegate MQObj_Freeze;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_GetNameDelegate(/* Object */ IntPtr obj, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);
		public static MQObj_GetNameDelegate MQObj_GetName;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetVertexCountDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetVertexCountDelegate MQObj_GetVertexCount;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_GetVertexDelegate(/* Object */ IntPtr obj, int index, ref Point pts);
		public static MQObj_GetVertexDelegate MQObj_GetVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetVertexDelegate(/* Object */ IntPtr obj, int index, ref Point pts);
		public static MQObj_SetVertexDelegate MQObj_SetVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_GetVertexArrayDelegate(/* Object */ IntPtr obj, Point[] ptsarray);
		public static MQObj_GetVertexArrayDelegate MQObj_GetVertexArray;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetFaceCountDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetFaceCountDelegate MQObj_GetFaceCount;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetFacePointCountDelegate(/* Object */ IntPtr obj, int face);
		public static MQObj_GetFacePointCountDelegate MQObj_GetFacePointCount;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_GetFacePointArrayDelegate(/* Object */ IntPtr obj, int face, int[] vertex);
		public static MQObj_GetFacePointArrayDelegate MQObj_GetFacePointArray;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_GetFaceCoordinateArrayDelegate(/* Object */ IntPtr obj, int face, Coordinate[] uvarray);
		public static MQObj_GetFaceCoordinateArrayDelegate MQObj_GetFaceCoordinateArray;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetFaceMaterialDelegate(/* Object */ IntPtr obj, int face);
		public static MQObj_GetFaceMaterialDelegate MQObj_GetFaceMaterial;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate uint MQObj_GetFaceUniqueIDDelegate(/* Object */ IntPtr obj, int face);
		public static MQObj_GetFaceUniqueIDDelegate MQObj_GetFaceUniqueID;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetFaceIndexFromUniqueIDDelegate(/* Object */ IntPtr obj, uint unique_id);
		public static MQObj_GetFaceIndexFromUniqueIDDelegate MQObj_GetFaceIndexFromUniqueID;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetNameDelegate(/* Object */ IntPtr obj, [MarshalAs(UnmanagedType.LPStr)] string buffer);
		public static MQObj_SetNameDelegate MQObj_SetName;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_AddVertexDelegate(/* Object */ IntPtr obj, ref Point p);
		public static MQObj_AddVertexDelegate MQObj_AddVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQObj_DeleteVertexDelegate(/* Object */ IntPtr obj, int index, [MarshalAs(UnmanagedType.Bool)] bool del_vert);
		public static MQObj_DeleteVertexDelegate MQObj_DeleteVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetVertexRefCountDelegate(/* Object */ IntPtr obj, int index);
		public static MQObj_GetVertexRefCountDelegate MQObj_GetVertexRefCount;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate uint MQObj_GetVertexUniqueIDDelegate(/* Object */ IntPtr obj, int index);
		public static MQObj_GetVertexUniqueIDDelegate MQObj_GetVertexUniqueID;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetVertexIndexFromUniqueIDDelegate(/* Object */ IntPtr obj, uint unique_id);
		public static MQObj_GetVertexIndexFromUniqueIDDelegate MQObj_GetVertexIndexFromUniqueID;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate float MQObj_GetVertexWeightDelegate(/* Object */ IntPtr obj, int index);
		public static MQObj_GetVertexWeightDelegate MQObj_GetVertexWeight;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetVertexWeightDelegate(/* Object */ IntPtr obj, int index, float value);
		public static MQObj_SetVertexWeightDelegate MQObj_SetVertexWeight;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_CopyVertexAttributeDelegate(/* Object */ IntPtr obj, int vert1, /* Object */ IntPtr obj2, int vert2);
		public static MQObj_CopyVertexAttributeDelegate MQObj_CopyVertexAttribute;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_AddFaceDelegate(/* Object */ IntPtr obj, int count, int[] index);
		public static MQObj_AddFaceDelegate MQObj_AddFace;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQObj_DeleteFaceDelegate(/* Object */ IntPtr obj, int index, [MarshalAs(UnmanagedType.Bool)] bool del_vert);
		public static MQObj_DeleteFaceDelegate MQObj_DeleteFace;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool MQObj_InvertFaceDelegate(/* Object */ IntPtr obj, int index);
		public static MQObj_InvertFaceDelegate MQObj_InvertFace;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetFaceMaterialDelegate(/* Object */ IntPtr obj, int face, int material);
		public static MQObj_SetFaceMaterialDelegate MQObj_SetFaceMaterial;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetFaceCoordinateArrayDelegate(/* Object */ IntPtr obj, int face, Coordinate[] uvarray);
		public static MQObj_SetFaceCoordinateArrayDelegate MQObj_SetFaceCoordinateArray;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate uint MQObj_GetFaceVertexColorDelegate(/* Object */ IntPtr obj, int face, int vertex);
		public static MQObj_GetFaceVertexColorDelegate MQObj_GetFaceVertexColor;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetFaceVertexColorDelegate(/* Object */ IntPtr obj, int face, int vertex, uint color);
		public static MQObj_SetFaceVertexColorDelegate MQObj_SetFaceVertexColor;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_OptimizeVertexDelegate(/* Object */ IntPtr obj, float distance, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1)] bool[] apply);
		public static MQObj_OptimizeVertexDelegate MQObj_OptimizeVertex;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_CompactDelegate(/* Object */ IntPtr obj);
		public static MQObj_CompactDelegate MQObj_Compact;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate uint MQObj_GetVisibleDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetVisibleDelegate MQObj_GetVisible;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetVisibleDelegate(/* Object */ IntPtr obj, uint visible);
		public static MQObj_SetVisibleDelegate MQObj_SetVisible;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate uint MQObj_GetPatchTypeDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetPatchTypeDelegate MQObj_GetPatchType;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetPatchTypeDelegate(/* Object */ IntPtr obj, uint type);
		public static MQObj_SetPatchTypeDelegate MQObj_SetPatchType;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetPatchSegmentDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetPatchSegmentDelegate MQObj_GetPatchSegment;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetPatchSegmentDelegate(/* Object */ IntPtr obj, int segment);
		public static MQObj_SetPatchSegmentDelegate MQObj_SetPatchSegment;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetShadingDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetShadingDelegate MQObj_GetShading;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetShadingDelegate(/* Object */ IntPtr obj, int type);
		public static MQObj_SetShadingDelegate MQObj_SetShading;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate float MQObj_GetSmoothAngleDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetSmoothAngleDelegate MQObj_GetSmoothAngle;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetSmoothAngleDelegate(/* Object */ IntPtr obj, float degree);
		public static MQObj_SetSmoothAngleDelegate MQObj_SetSmoothAngle;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetMirrorTypeDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetMirrorTypeDelegate MQObj_GetMirrorType;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetMirrorTypeDelegate(/* Object */ IntPtr obj, int type);
		public static MQObj_SetMirrorTypeDelegate MQObj_SetMirrorType;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate uint MQObj_GetMirrorAxisDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetMirrorAxisDelegate MQObj_GetMirrorAxis;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetMirrorAxisDelegate(/* Object */ IntPtr obj, uint axis);
		public static MQObj_SetMirrorAxisDelegate MQObj_SetMirrorAxis;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate float MQObj_GetMirrorDistanceDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetMirrorDistanceDelegate MQObj_GetMirrorDistance;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetMirrorDistanceDelegate(/* Object */ IntPtr obj, float dis);
		public static MQObj_SetMirrorDistanceDelegate MQObj_SetMirrorDistance;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetLatheTypeDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetLatheTypeDelegate MQObj_GetLatheType;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetLatheTypeDelegate(/* Object */ IntPtr obj, int type);
		public static MQObj_SetLatheTypeDelegate MQObj_SetLatheType;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate uint MQObj_GetLatheAxisDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetLatheAxisDelegate MQObj_GetLatheAxis;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetLatheAxisDelegate(/* Object */ IntPtr obj, uint axis);
		public static MQObj_SetLatheAxisDelegate MQObj_SetLatheAxis;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetLatheSegmentDelegate(/* Object */ IntPtr obj);
		public static MQObj_GetLatheSegmentDelegate MQObj_GetLatheSegment;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetLatheSegmentDelegate(/* Object */ IntPtr obj, int segment);
		public static MQObj_SetLatheSegmentDelegate MQObj_SetLatheSegment;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQObj_GetIntValueDelegate(/* Object */ IntPtr obj, int type_id);
		public static MQObj_GetIntValueDelegate MQObj_GetIntValue;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_GetFloatArrayDelegate(/* Object */ IntPtr obj, int type_id, float[] array);
		public static MQObj_GetFloatArrayDelegate MQObj_GetFloatArray;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetIntValueDelegate(/* Object */ IntPtr obj, int type_id, int value);
		public static MQObj_SetIntValueDelegate MQObj_SetIntValue;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQObj_SetFloatArrayDelegate(/* Object */ IntPtr obj, int type_id, float[] array);
		public static MQObj_SetFloatArrayDelegate MQObj_SetFloatArray;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_DeleteDelegate(/* Material */ IntPtr mat);
		public static MQMat_DeleteDelegate MQMat_Delete;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate int MQMat_GetIntValueDelegate(/* Material */ IntPtr mat, int type_id);
		public static MQMat_GetIntValueDelegate MQMat_GetIntValue;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_GetFloatArrayDelegate(/* Material */ IntPtr mat, int type_id, float[] array);
		public static MQMat_GetFloatArrayDelegate MQMat_GetFloatArray;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_GetNameDelegate(/* Material */ IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);
		public static MQMat_GetNameDelegate MQMat_GetName;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_GetColorDelegate(/* Material */ IntPtr mat, out Color color);
		public static MQMat_GetColorDelegate MQMat_GetColor;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate float MQMat_GetAlphaDelegate(/* Material */ IntPtr mat);
		public static MQMat_GetAlphaDelegate MQMat_GetAlpha;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate float MQMat_GetDiffuseDelegate(/* Material */ IntPtr mat);
		public static MQMat_GetDiffuseDelegate MQMat_GetDiffuse;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate float MQMat_GetAmbientDelegate(/* Material */ IntPtr mat);
		public static MQMat_GetAmbientDelegate MQMat_GetAmbient;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate float MQMat_GetEmissionDelegate(/* Material */ IntPtr mat);
		public static MQMat_GetEmissionDelegate MQMat_GetEmission;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate float MQMat_GetSpecularDelegate(/* Material */ IntPtr mat);
		public static MQMat_GetSpecularDelegate MQMat_GetSpecular;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate float MQMat_GetPowerDelegate(/* Material */ IntPtr mat);
		public static MQMat_GetPowerDelegate MQMat_GetPower;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_GetTextureNameDelegate(/* Material */ IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);
		public static MQMat_GetTextureNameDelegate MQMat_GetTextureName;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_GetAlphaNameDelegate(/* Material */ IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);
		public static MQMat_GetAlphaNameDelegate MQMat_GetAlphaName;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_GetBumpNameDelegate(/* Material */ IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);
		public static MQMat_GetBumpNameDelegate MQMat_GetBumpName;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetIntValueDelegate(/* Material */ IntPtr mat, int type_id, int value);
		public static MQMat_SetIntValueDelegate MQMat_SetIntValue;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetFloatArrayDelegate(/* Material */ IntPtr mat, int type_id, float[] array);
		public static MQMat_SetFloatArrayDelegate MQMat_SetFloatArray;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetNameDelegate(/* Material */ IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] string name);
		public static MQMat_SetNameDelegate MQMat_SetName;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetColorDelegate(/* Material */ IntPtr mat, out Color color);
		public static MQMat_SetColorDelegate MQMat_SetColor;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetAlphaDelegate(/* Material */ IntPtr mat, float value);
		public static MQMat_SetAlphaDelegate MQMat_SetAlpha;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetDiffuseDelegate(/* Material */ IntPtr mat, float value);
		public static MQMat_SetDiffuseDelegate MQMat_SetDiffuse;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetAmbientDelegate(/* Material */ IntPtr mat, float value);
		public static MQMat_SetAmbientDelegate MQMat_SetAmbient;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetEmissionDelegate(/* Material */ IntPtr mat, float value);
		public static MQMat_SetEmissionDelegate MQMat_SetEmission;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetSpecularDelegate(/* Material */ IntPtr mat, float value);
		public static MQMat_SetSpecularDelegate MQMat_SetSpecular;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetPowerDelegate(/* Material */ IntPtr mat, float value);
		public static MQMat_SetPowerDelegate MQMat_SetPower;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetTextureNameDelegate(/* Material */ IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] string name);
		public static MQMat_SetTextureNameDelegate MQMat_SetTextureName;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetAlphaNameDelegate(/* Material */ IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] string name);
		public static MQMat_SetAlphaNameDelegate MQMat_SetAlphaName;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMat_SetBumpNameDelegate(/* Material */ IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] string name);
		public static MQMat_SetBumpNameDelegate MQMat_SetBumpName;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQMatrix_FloatValueDelegate(float[] mtx, int type_id, float[] values);
		public static MQMatrix_FloatValueDelegate MQMatrix_FloatValue;

		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public delegate void MQXmlElem_ValueDelegate(/* XmlElement */ IntPtr elem, int type_id, /* void* */ IntPtr values);
		public static MQXmlElem_ValueDelegate MQXmlElem_Value;

	}
}