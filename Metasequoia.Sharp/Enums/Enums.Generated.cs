using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Metasequoia
{
	partial class Plugin
	{
		/// <summary>
		/// MQPLUGIN_VERSION
		/// </summary>
		public const int Version = 0x0249;
	}

	public enum PluginType
	{
		/// <summary>
		/// MQPLUGIN_TYPE_IMPORT
		/// </summary>
		Import = 1,
		/// <summary>
		/// MQPLUGIN_TYPE_EXPORT
		/// </summary>
		Export = 2,
		/// <summary>
		/// MQPLUGIN_TYPE_CREATE
		/// </summary>
		Create = 3,
		/// <summary>
		/// MQPLUGIN_TYPE_OBJECT
		/// </summary>
		Object = 4,
		/// <summary>
		/// MQPLUGIN_TYPE_SELECT
		/// </summary>
		Select = 5,
		/// <summary>
		/// MQPLUGIN_TYPE_STATION
		/// </summary>
		Station = 6,
		/// <summary>
		/// MQPLUGIN_TYPE_COMMAND
		/// </summary>
		Command = 7,
	}

	public enum Event
	{
		/// <summary>
		/// MQEVENT_INITIALIZE
		/// </summary>
		Initialize = 1,
		/// <summary>
		/// MQEVENT_EXIT
		/// </summary>
		Exit = 2,
		/// <summary>
		/// MQEVENT_ACTIVATE
		/// </summary>
		Activate = 100,
		/// <summary>
		/// MQEVENT_IS_ACTIVATED
		/// </summary>
		IsActivated = 101,
		/// <summary>
		/// MQEVENT_MINIMIZE
		/// </summary>
		Minimize = 102,
		/// <summary>
		/// MQEVENT_USER_MESSAGE
		/// </summary>
		UserMessage = 103,
		/// <summary>
		/// MQEVENT_DRAW
		/// </summary>
		Draw = 110,
		/// <summary>
		/// MQEVENT_LBUTTON_DOWN
		/// </summary>
		LbuttonDown = 120,
		/// <summary>
		/// MQEVENT_LBUTTON_MOVE
		/// </summary>
		LbuttonMove = 121,
		/// <summary>
		/// MQEVENT_LBUTTON_UP
		/// </summary>
		LbuttonUp = 122,
		/// <summary>
		/// MQEVENT_MBUTTON_DOWN
		/// </summary>
		MbuttonDown = 123,
		/// <summary>
		/// MQEVENT_MBUTTON_MOVE
		/// </summary>
		MbuttonMove = 124,
		/// <summary>
		/// MQEVENT_MBUTTON_UP
		/// </summary>
		MbuttonUp = 125,
		/// <summary>
		/// MQEVENT_RBUTTON_DOWN
		/// </summary>
		RbuttonDown = 126,
		/// <summary>
		/// MQEVENT_RBUTTON_MOVE
		/// </summary>
		RbuttonMove = 127,
		/// <summary>
		/// MQEVENT_RBUTTON_UP
		/// </summary>
		RbuttonUp = 128,
		/// <summary>
		/// MQEVENT_MOUSE_MOVE
		/// </summary>
		MouseMove = 129,
		/// <summary>
		/// MQEVENT_MOUSE_WHEEL
		/// </summary>
		MouseWheel = 130,
		/// <summary>
		/// MQEVENT_KEY_DOWN
		/// </summary>
		KeyDown = 140,
		/// <summary>
		/// MQEVENT_KEY_UP
		/// </summary>
		KeyUp = 141,
		/// <summary>
		/// MQEVENT_NEW_DOCUMENT
		/// </summary>
		NewDocument = 200,
		/// <summary>
		/// MQEVENT_END_DOCUMENT
		/// </summary>
		EndDocument = 201,
		/// <summary>
		/// MQEVENT_SAVE_DOCUMENT
		/// </summary>
		SaveDocument = 202,
		/// <summary>
		/// MQEVENT_UNDO
		/// </summary>
		Undo = 210,
		/// <summary>
		/// MQEVENT_REDO
		/// </summary>
		Redo = 211,
		/// <summary>
		/// MQEVENT_UNDO_UPDATED
		/// </summary>
		UndoUpdated = 212,
		/// <summary>
		/// MQEVENT_OBJECT_LIST
		/// </summary>
		ObjectList = 220,
		/// <summary>
		/// MQEVENT_OBJECT_MODIFIED
		/// </summary>
		ObjectModified = 221,
		/// <summary>
		/// MQEVENT_OBJECT_SELECTED
		/// </summary>
		ObjectSelected = 222,
		/// <summary>
		/// MQEVENT_MATERIAL_LIST
		/// </summary>
		MaterialList = 230,
		/// <summary>
		/// MQEVENT_MATERIAL_MODIFIED
		/// </summary>
		MaterialModified = 231,
		/// <summary>
		/// MQEVENT_SCENE
		/// </summary>
		Scene = 240,
	}

	public enum Message
	{
		/// <summary>
		/// MQMESSAGE_ACTIVATE
		/// </summary>
		Activate = 100,
		/// <summary>
		/// MQMESSAGE_USER_MESSAGE
		/// </summary>
		UserMessage = 101,
		/// <summary>
		/// MQMESSAGE_NEW_DRAW_OBJECT
		/// </summary>
		NewDrawObject = 200,
		/// <summary>
		/// MQMESSAGE_NEW_DRAW_MATERIAL
		/// </summary>
		NewDrawMaterial = 201,
		/// <summary>
		/// MQMESSAGE_DELETE_DRAW_OBJECT
		/// </summary>
		DeleteDrawObject = 210,
		/// <summary>
		/// MQMESSAGE_DELETE_DRAW_MATERIAL
		/// </summary>
		DeleteDrawMaterial = 211,
		/// <summary>
		/// MQMESSAGE_GET_UNDO_STATE
		/// </summary>
		GetUndoState = 300,
		/// <summary>
		/// MQMESSAGE_UPDATE_UNDO
		/// </summary>
		UpdateUndo = 301,
		/// <summary>
		/// MQMESSAGE_REDRAW_SCENE
		/// </summary>
		RedrawScene = 400,
		/// <summary>
		/// MQMESSAGE_REDRAW_ALL_SCENE
		/// </summary>
		RedrawAllScene = 401,
		/// <summary>
		/// MQMESSAGE_GET_SCENE_OPTION
		/// </summary>
		GetSceneOption = 410,
		/// <summary>
		/// MQMESSAGE_HIT_TEST
		/// </summary>
		HitTest = 411,
		/// <summary>
		/// MQMESSAGE_GET_EDIT_OPTION
		/// </summary>
		GetEditOption = 500,
		/// <summary>
		/// MQMESSAGE_GET_SNAPPED_POS
		/// </summary>
		GetSnappedPos = 501,
		/// <summary>
		/// MQMESSAGE_GET_RESOURCE_CURSOR
		/// </summary>
		GetResourceCursor = 502,
		/// <summary>
		/// MQMESSAGE_SET_MOUSE_CURSOR
		/// </summary>
		SetMouseCursor = 603,
		/// <summary>
		/// MQMESSAGE_SET_STATUS_STRING
		/// </summary>
		SetStatusString = 604,
	}

	public enum FileType
	{
		/// <summary>
		/// MQFILE_TYPE_LEFT
		/// </summary>
		Left = 0,
		/// <summary>
		/// MQFILE_TYPE_RIGHT
		/// </summary>
		Right = 1,
		/// <summary>
		/// MQFILE_TYPE_UP
		/// </summary>
		Up = 2,
		/// <summary>
		/// MQFILE_TYPE_DOWN
		/// </summary>
		Down = 3,
		/// <summary>
		/// MQFILE_TYPE_FRONT
		/// </summary>
		Front = 4,
		/// <summary>
		/// MQFILE_TYPE_BACK
		/// </summary>
		Back = 5,
	}

	public enum Folder
	{
		/// <summary>
		/// MQFOLDER_ROOT
		/// </summary>
		Root = 1,
		/// <summary>
		/// MQFOLDER_METASEQ_EXE
		/// </summary>
		MetaseqExe = 2,
		/// <summary>
		/// MQFOLDER_METASEQ_INI
		/// </summary>
		MetaseqIni = 3,
		/// <summary>
		/// MQFOLDER_DATA
		/// </summary>
		Data = 4,
		/// <summary>
		/// MQFOLDER_PLUGINS
		/// </summary>
		Plugins = 5,
	}

	public enum Doc
	{
		/// <summary>
		/// MQDOC_CLEARSELECT_VERTEX
		/// </summary>
		ClearselectVertex = 1,
		/// <summary>
		/// MQDOC_CLEARSELECT_LINE
		/// </summary>
		ClearselectLine = 2,
		/// <summary>
		/// MQDOC_CLEARSELECT_FACE
		/// </summary>
		ClearselectFace = 4,
		/// <summary>
		/// MQDOC_CLEARSELECT_ALL
		/// </summary>
		ClearselectAll = 7,
	}

	public enum Mapping
	{
		/// <summary>
		/// MQMAPPING_TEXTURE
		/// </summary>
		Texture = 1,
		/// <summary>
		/// MQMAPPING_ALPHA
		/// </summary>
		Alpha = 2,
		/// <summary>
		/// MQMAPPING_BUMP
		/// </summary>
		Bump = 3,
	}

	public enum MQScene
	{
		/// <summary>
		/// MQSCENE_GET_CAMERA_POS
		/// </summary>
		GetCameraPos = 0x101,
		/// <summary>
		/// MQSCENE_GET_CAMERA_ANGLE
		/// </summary>
		GetCameraAngle = 0x102,
		/// <summary>
		/// MQSCENE_GET_LOOK_AT_POS
		/// </summary>
		GetLookAtPos = 0x103,
		/// <summary>
		/// MQSCENE_GET_ROTATION_CENTER
		/// </summary>
		GetRotationCenter = 0x104,
		/// <summary>
		/// MQSCENE_GET_FOV
		/// </summary>
		GetFov = 0x105,
		/// <summary>
		/// MQSCENE_GET_DIRECTIONAL_LIGHT
		/// </summary>
		GetDirectionalLight = 0x106,
		/// <summary>
		/// MQSCENE_GET_AMBIENT_COLOR
		/// </summary>
		GetAmbientColor = 0x107,
		/// <summary>
		/// MQSCENE_SET_CAMERA_POS
		/// </summary>
		SetCameraPos = 0x201,
		/// <summary>
		/// MQSCENE_SET_CAMERA_ANGLE
		/// </summary>
		SetCameraAngle = 0x202,
		/// <summary>
		/// MQSCENE_SET_LOOK_AT_POS
		/// </summary>
		SetLookAtPos = 0x203,
		/// <summary>
		/// MQSCENE_SET_ROTATION_CENTER
		/// </summary>
		SetRotationCenter = 0x204,
		/// <summary>
		/// MQSCENE_SET_FOV
		/// </summary>
		SetFov = 0x205,
		/// <summary>
		/// MQSCENE_SET_DIRECTIONAL_LIGHT
		/// </summary>
		SetDirectionalLight = 0x206,
		/// <summary>
		/// MQSCENE_SET_AMBIENT_COLOR
		/// </summary>
		SetAmbientColor = 0x207,
		/// <summary>
		/// MQSCENE_CONVERT_3D_TO_SCREEN
		/// </summary>
		Convert3dToScreen = 0x300,
		/// <summary>
		/// MQSCENE_CONVERT_SCREEN_TO_3D
		/// </summary>
		ConvertScreenTo3d = 0x301,
	}

	[Flags]
	public enum ObjectFreeze
	{
		/// <summary>
		/// MQOBJECT_FREEZE_PATCH
		/// </summary>
		Patch = 0x00000001,
		/// <summary>
		/// MQOBJECT_FREEZE_MIRROR
		/// </summary>
		Mirror = 0x00000002,
		/// <summary>
		/// MQOBJECT_FREEZE_LATHE
		/// </summary>
		Lathe = 0x00000004,
		/// <summary>
		/// MQOBJECT_FREEZE_ALL
		/// </summary>
		All = 0x7,
	}

	public enum ObjectPatch
	{
		/// <summary>
		/// MQOBJECT_PATCH_MAX
		/// </summary>
		Max = 3,
		/// <summary>
		/// MQOBJECT_PATCH_NONE
		/// </summary>
		None = 0,
		/// <summary>
		/// MQOBJECT_PATCH_SPLINE1
		/// </summary>
		Spline1 = 1,
		/// <summary>
		/// MQOBJECT_PATCH_SPLINE2
		/// </summary>
		Spline2 = 2,
		/// <summary>
		/// MQOBJECT_PATCH_CATMULL
		/// </summary>
		Catmull = 3,
	}

	public enum ObjectShade
	{
		/// <summary>
		/// MQOBJECT_SHADE_MAX
		/// </summary>
		Max = 1,
		/// <summary>
		/// MQOBJECT_SHADE_FLAT
		/// </summary>
		Flat = 0,
		/// <summary>
		/// MQOBJECT_SHADE_GOURAUD
		/// </summary>
		Gouraud = 1,
	}

	public enum ObjectMirror
	{
		/// <summary>
		/// MQOBJECT_MIRROR_MAX
		/// </summary>
		Max = 2,
		/// <summary>
		/// MQOBJECT_MIRROR_NONE
		/// </summary>
		None = 0,
		/// <summary>
		/// MQOBJECT_MIRROR_NORMAL
		/// </summary>
		Normal = 1,
		/// <summary>
		/// MQOBJECT_MIRROR_JOIN
		/// </summary>
		Join = 2,
	}

	[Flags]
	public enum ObjectMirrorAxis
	{
		/// <summary>
		/// MQOBJECT_MIRROR_AXIS_X
		/// </summary>
		X = 1,
		/// <summary>
		/// MQOBJECT_MIRROR_AXIS_Y
		/// </summary>
		Y = 2,
		/// <summary>
		/// MQOBJECT_MIRROR_AXIS_Z
		/// </summary>
		Z = 4,
	}

	public enum ObjectLathe
	{
		/// <summary>
		/// MQOBJECT_LATHE_MAX
		/// </summary>
		Max = 3,
		/// <summary>
		/// MQOBJECT_LATHE_NONE
		/// </summary>
		None = 0,
		/// <summary>
		/// MQOBJECT_LATHE_FRONT
		/// </summary>
		Front = 1,
		/// <summary>
		/// MQOBJECT_LATHE_BACK
		/// </summary>
		Back = 2,
		/// <summary>
		/// MQOBJECT_LATHE_BOTH
		/// </summary>
		Both = 3,
	}

	[Flags]
	public enum ObjectLatheAxis
	{
		/// <summary>
		/// MQOBJECT_LATHE_X
		/// </summary>
		X = 0,
		/// <summary>
		/// MQOBJECT_LATHE_Y
		/// </summary>
		Y = 1,
		/// <summary>
		/// MQOBJECT_LATHE_Z
		/// </summary>
		Z = 2,
	}

	public enum MaterialShader
	{
		/// <summary>
		/// MQMATERIAL_SHADER_CLASSIC
		/// </summary>
		Classic = 0,
		/// <summary>
		/// MQMATERIAL_SHADER_CONSTANT
		/// </summary>
		Constant = 1,
		/// <summary>
		/// MQMATERIAL_SHADER_LAMBERT
		/// </summary>
		Lambert = 2,
		/// <summary>
		/// MQMATERIAL_SHADER_PHONG
		/// </summary>
		Phong = 3,
		/// <summary>
		/// MQMATERIAL_SHADER_BLINN
		/// </summary>
		Blinn = 4,
	}

	public enum MaterialVertexcolor
	{
		/// <summary>
		/// MQMATERIAL_VERTEXCOLOR_DISABLE
		/// </summary>
		Disable = 0,
		/// <summary>
		/// MQMATERIAL_VERTEXCOLOR_DIFFUSE
		/// </summary>
		Diffuse = 1,
	}

	public enum MaterialProjection
	{
		/// <summary>
		/// MQMATERIAL_PROJECTION_UV
		/// </summary>
		Uv = 0,
		/// <summary>
		/// MQMATERIAL_PROJECTION_FLAT
		/// </summary>
		Flat = 1,
		/// <summary>
		/// MQMATERIAL_PROJECTION_CYLINDER
		/// </summary>
		Cylinder = 2,
		/// <summary>
		/// MQMATERIAL_PROJECTION_SPHERE
		/// </summary>
		Sphere = 3,
	}

	public enum ObjId
	{
		/// <summary>
		/// MQOBJ_ID_DEPTH
		/// </summary>
		Depth = 0x101,
		/// <summary>
		/// MQOBJ_ID_FOLDING
		/// </summary>
		Folding = 0x102,
		/// <summary>
		/// MQOBJ_ID_LOCKING
		/// </summary>
		Locking = 0x103,
		/// <summary>
		/// MQOBJ_ID_UNIQUE_ID
		/// </summary>
		UniqueId = 0x104,
		/// <summary>
		/// MQOBJ_ID_COLOR
		/// </summary>
		Color = 0x201,
		/// <summary>
		/// MQOBJ_ID_COLOR_VALID
		/// </summary>
		ColorValid = 0x201,
		/// <summary>
		/// MQOBJ_ID_SCALING
		/// </summary>
		Scaling = 0x301,
		/// <summary>
		/// MQOBJ_ID_ROTATION
		/// </summary>
		Rotation = 0x302,
		/// <summary>
		/// MQOBJ_ID_TRANSLATION
		/// </summary>
		Translation = 0x303,
		/// <summary>
		/// MQOBJ_ID_LOCAL_MATRIX
		/// </summary>
		LocalMatrix = 0x304,
		/// <summary>
		/// MQOBJ_ID_LOCAL_INVERSE_MATRIX
		/// </summary>
		LocalInverseMatrix = 0x305,
	}

	public enum MatId
	{
		/// <summary>
		/// MQMAT_ID_SHADER
		/// </summary>
		Shader = 0x101,
		/// <summary>
		/// MQMAT_ID_VERTEXCOLOR
		/// </summary>
		Vertexcolor = 0x102,
		/// <summary>
		/// MQMAT_ID_UNIQUE_ID
		/// </summary>
		UniqueId = 0x103,
		/// <summary>
		/// MQMAT_ID_MAPPROJ
		/// </summary>
		Mapproj = 0x301,
		/// <summary>
		/// MQMAT_ID_MAPPROJ_POSITION
		/// </summary>
		MapprojPosition = 0x302,
		/// <summary>
		/// MQMAT_ID_MAPPROJ_SCALING
		/// </summary>
		MapprojScaling = 0x303,
		/// <summary>
		/// MQMAT_ID_MAPPROJ_ANGLE
		/// </summary>
		MapprojAngle = 0x304,
	}

	public enum MQMatrix
	{
		/// <summary>
		/// MQMATRIX_GET_SCALING
		/// </summary>
		GetScaling = 0x101,
		/// <summary>
		/// MQMATRIX_GET_ROTATION
		/// </summary>
		GetRotation = 0x102,
		/// <summary>
		/// MQMATRIX_GET_TRANSLATION
		/// </summary>
		GetTranslation = 0x103,
		/// <summary>
		/// MQMATRIX_GET_INVERSE_TRANSFORM
		/// </summary>
		GetInverseTransform = 0x105,
		/// <summary>
		/// MQMATRIX_SET_TRANSFORM
		/// </summary>
		SetTransform = 0x204,
		/// <summary>
		/// MQMATRIX_SET_INVERSE_TRANSFORM
		/// </summary>
		SetInverseTransform = 0x205,
	}

	public enum Xmlelem
	{
		/// <summary>
		/// MQXMLELEM_ADD_CHILD_ELEMENT
		/// </summary>
		AddChildElement = 0x101,
		/// <summary>
		/// MQXMLELEM_REMOVE_CHILD_ELEMENT
		/// </summary>
		RemoveChildElement = 0x102,
		/// <summary>
		/// MQXMLELEM_FIRST_CHILD_ELEMENT
		/// </summary>
		FirstChildElement = 0x201,
		/// <summary>
		/// MQXMLELEM_NEXT_CHILD_ELEMENT
		/// </summary>
		NextChildElement = 0x202,
		/// <summary>
		/// MQXMLELEM_GET_PARENT_ELEMENT
		/// </summary>
		GetParentElement = 0x203,
		/// <summary>
		/// MQXMLELEM_GET_NAME
		/// </summary>
		GetName = 0x301,
		/// <summary>
		/// MQXMLELEM_GET_TEXT
		/// </summary>
		GetText = 0x302,
		/// <summary>
		/// MQXMLELEM_GET_ATTRIBUTE
		/// </summary>
		GetAttribute = 0x303,
		/// <summary>
		/// MQXMLELEM_SET_TEXT
		/// </summary>
		SetText = 0x402,
		/// <summary>
		/// MQXMLELEM_SET_ATTRIBUTE
		/// </summary>
		SetAttribute = 0x403,
	}

}

