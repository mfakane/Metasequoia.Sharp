using System;
using System.Text;

namespace Metasequoia.Sharp
{
	public abstract class CommandPlugin : StationPlugin
	{
		/// <summary>
		/// 左ボタンが押されたときに呼び出されます。
		/// プラグイン独自の動作を行った場合には TRUE を、
		/// 独自処理を行わず標準動作を行わせる場合には FALSE を戻り値として返します。
		/// MQCommandPlugin::OnLeftButtonDown
		/// </summary>
		public event EventHandler<MouseEventArgs> LeftButtonDown;
		/// <summary>
		/// 左ボタンが押されながらマウスが移動したときに呼び出されます。
		/// MQCommandPlugin::OnLeftButtonMove
		/// </summary>
		public event EventHandler<MouseEventArgs> LeftButtonMove;
		/// <summary>
		/// 左ボタンが離されたときに呼び出されます。
		/// MQCommandPlugin::OnLeftButtonUp
		/// </summary>
		public event EventHandler<MouseEventArgs> LeftButtonUp;
		/// <summary>
		/// 中ボタンが押されたときに呼び出されます。
		/// MQCommandPlugin::OnMiddleButtonDown
		/// </summary>
		public event EventHandler<MouseEventArgs> MiddleButtonDown;
		/// <summary>
		/// 中ボタンが押されながらマウスが移動したときに呼び出されます。
		/// MQCommandPlugin::OnMiddleButtonMove
		/// </summary>
		public event EventHandler<MouseEventArgs> MiddleButtonMove;
		/// <summary>
		/// 中ボタンが離されたときに呼び出されます。
		/// MQCommandPlugin::OnMiddleButtonUp
		/// </summary>
		public event EventHandler<MouseEventArgs> MiddleButtonUp;
		/// <summary>
		/// 右ボタンが押されたときに呼び出されます。
		/// MQCommandPlugin::OnRightButtonDown
		/// </summary>
		public event EventHandler<MouseEventArgs> RightButtonDown;
		/// <summary>
		/// 右ボタンが押されながらマウスが移動したときに呼び出されます。
		/// MQCommandPlugin::OnRightButtonMove
		/// </summary>
		public event EventHandler<MouseEventArgs> RightButtonMove;
		/// <summary>
		/// 右ボタンが離されたときに呼び出されます。
		/// MQCommandPlugin::OnRightButtonUp
		/// </summary>
		public event EventHandler<MouseEventArgs> RightButtonUp;
		/// <summary>
		/// マウスのホイールが回転したときに呼び出されます。
		/// ホイールの回転量は e.Wheel に WHEEL_DELTA の倍数または約数で入力されます。
		/// MQCommandPlugin::OnMouseWheel
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseWheel;
		/// <summary>
		/// マウスが移動したときに呼び出されます。
		/// MQCommandPlugin::OnMouseMove
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseMove;
		/// <summary>
		/// キーが押されたときに呼び出されます。
		/// MQCommandPlugin::OnKeyDown
		/// </summary>
		public event EventHandler<KeyEventArgs> KeyDown;
		/// <summary>
		/// キーが押されたときに呼び出されます。
		/// MQCommandPlugin::OnKeyUp
		/// </summary>
		public event EventHandler<KeyEventArgs> KeyUp;

		protected override void OnRaiseEvent(MetasequoiaEventArgs e)
		{
			base.OnRaiseEvent(e);

			switch (e.EventType)
			{
				case Event.LbuttonDown:
					Raise(LeftButtonDown, new MouseEventArgs(e));

					break;
				case Event.LbuttonMove:
					Raise(LeftButtonMove, new MouseEventArgs(e));

					break;
				case Event.LbuttonUp:
					Raise(LeftButtonUp, new MouseEventArgs(e));

					break;
				case Event.MbuttonDown:
					Raise(MiddleButtonDown, new MouseEventArgs(e));

					break;
				case Event.MbuttonMove:
					Raise(MiddleButtonMove, new MouseEventArgs(e));

					break;
				case Event.MbuttonUp:
					Raise(MiddleButtonUp, new MouseEventArgs(e));

					break;
				case Event.RbuttonDown:
					Raise(RightButtonDown, new MouseEventArgs(e));

					break;
				case Event.RbuttonMove:
					Raise(RightButtonMove, new MouseEventArgs(e));

					break;
				case Event.RbuttonUp:
					Raise(RightButtonUp, new MouseEventArgs(e));

					break;
				case Event.MouseMove:
					Raise(MouseMove, new MouseEventArgs(e));

					break;
				case Event.MouseWheel:
					Raise(MouseWheel, new MouseEventArgs(e));

					break;
				case Event.KeyDown:
					Raise(KeyDown, new KeyEventArgs(e));

					break;
				case Event.KeyUp:
					Raise(KeyUp, new KeyEventArgs(e));

					break;
			}
		}

		/// <summary>
		/// アンドゥバッファを更新する
		/// void MQCommandPlugin::UpdateUndo()
		/// </summary>
		public unsafe void UpdateUndo()
		{
			var array = new void*[1];

			fixed (void** arrayPtr = array)
				SendMessage(Message.UpdateUndo, (IntPtr)arrayPtr);
		}

		/// <summary>
		/// シーンの再描画を予約する
		/// void MQCommandPlugin::RedrawScene(MQScene scene)
		/// </summary>
		/// <param name="scene">シーン</param>
		public unsafe void RedrawScene(Scene scene)
		{
			fixed (byte* sceneString = GetASCII("scene"))
			{
				var array = new void*[]
				{
					sceneString,
					(void*)scene.Handle,
					null,
				};

				fixed (void** arrayPtr = array)
					SendMessage(Message.RedrawScene, (IntPtr)arrayPtr);
			}
		}

		/// <summary>
		/// すべてのシーンの再描画を予約する
		/// void MQCommandPlugin::RedrawAllScene()
		/// </summary>
		public unsafe void RedrawAllScene()
		{
			var array = new void*[1];

			fixed (void** arrayPtr = array)
				SendMessage(Message.RedrawAllScene, (IntPtr)arrayPtr);
		}

		/// <summary>
		/// 編集オプションを取得
		/// void MQCommandPlugin::GetEditOption(EDIT_OPTION&amp; option)
		/// </summary>
		/// <returns>編集オプション</returns>
		public unsafe EditOption GetEditOption()
		{
			fixed (byte* editVertexString = GetASCII("edit_vertex"),
						 editLineString = GetASCII("edit_line"),
						 editFaceString = GetASCII("edit_face"),
						 selectRectString = GetASCII("select_rect"),
						 selectRopeString = GetASCII("select_rope"),
						 symmetryString = GetASCII("symmetry"),
						 symmetryDistanceString = GetASCII("symmetry_distance"),
						 curObjOnlyString = GetASCII("cur_obj_only"),
						 snapXString = GetASCII("snap_x"),
						 snapYString = GetASCII("snap_y"),
						 snapZString = GetASCII("snap_z"),
						 coordinateTypeString = GetASCII("coordinate_type"),
						 snapGridString = GetASCII("snap_grid"))
			{
				var option = new EditOption();
				var array = new void*[]
				{
					editVertexString,
					&option.EditVertex,
					editLineString,
					&option.EditLine,
					editFaceString,
					&option.EditFace,
					selectRectString,
					&option.SelectRect,
					selectRopeString,
					&option.SelectRope,
					symmetryString,
					&option.Symmetry,
					symmetryDistanceString,
					&option.SymmetryDistance,
					curObjOnlyString,
					&option.CurrentObjectOnly,
					snapXString,
					&option.SnapX,
					snapYString,
					&option.SnapY,
					snapZString,
					&option.SnapZ,
					coordinateTypeString,
					&option.CoordinateType,
					snapGridString,
					&option.SnapGrid,
					null,
				};

				fixed (void** arrayPtr = array)
					SendMessage(Message.GetEditOption, (IntPtr)arrayPtr);

				return option;
			}
		}

		/// <summary>
		/// スナップ位置を取得
		/// MQPoint MQCommandPlugin::GetSnappedPos(MQScene scene, const MQPoint&amp; p, SNAP_GRID_TYPE type)
		/// </summary>
		/// <param name="scene">シーン</param>
		/// <param name="p">ポイント</param>
		/// <param name="type">種類</param>
		/// <returns>スナップ位置</returns>
		public unsafe Point GetSnappedPos(Scene scene, Point p, SnapGridType type)
		{
			fixed (byte* sceneString = GetASCII("scene"),
						 typeString = GetASCII("type"),
						 posString = GetASCII("pos"),
						 resultString = GetASCII("result"))
			{
				var pos = new[]
				{
					p.X,
					p.Y,
					p.Z,
				};
				var result = new float[3];

				fixed (float* posPtr = pos,
							  resultPtr = result)
				{
					var array = new void*[]
					{
						sceneString,
						(void*)scene.Handle,
						typeString,
						&type,
						posString,
						posPtr,
						resultString,
						resultPtr,
						null,
					};

					fixed (void** arrayPtr = array)
						SendMessage(Message.GetSnappedPos, (IntPtr)arrayPtr);

					return new Point(result[0], result[1], result[2]);
				}
			}
		}

		/// <summary>
		/// シーン内の指定された位置にあるものを検知
		/// bool MQCommandPlugin::HitTest(MQScene scene, POINT p, HIT_TEST_PARAM&amp; param)
		/// </summary>
		/// <param name="scene">シーン</param>
		/// <param name="p">位置</param>
		/// <param name="testType">種類</param>
		/// <returns>結果</returns>
		public unsafe HitTestResult HitTest(Scene scene, int[] p, HitType testType)
		{
			var rt = new HitTestResult();

			fixed (byte* sceneString = GetASCII("scene"),
						 xString = GetASCII("x"),
						 yString = GetASCII("y"),
						 testTypeString = GetASCII("test_type"),
						 hitTypeString = GetASCII("hit_type"),
						 hitPosString = GetASCII("hit_pos"),
						 hitObjectString = GetASCII("hit_object"),
						 hitVertexString = GetASCII("hit_vertex"),
						 hitLineString = GetASCII("hit_line"),
						 hitFaceString = GetASCII("hit_face"))
			fixed (int* point = p)
			{
				var array = new void*[]
				{
					sceneString,
					(void*)scene.Handle,
					xString,
					&point[0],
					yString,
					&point[1],
					testTypeString,
					&testType,
					hitTypeString,
					&rt.HitType,
					hitPosString,
					&rt.HitPos,
					hitObjectString,
					&rt.ObjectIndex,
					hitVertexString,
					&rt.VertexIndex,
					hitLineString,
					&rt.LineIndex,
					hitFaceString,
					&rt.FaceIndex,
					null,
				};

				fixed (void** arrayPtr = array)
					SendMessage(Message.HitTest, (IntPtr)arrayPtr);

				return rt;
			}
		}

		/// <summary>
		/// 標準マウスカーソルを取得
		/// HCURSOR MQCommandPlugin::GetResourceCursor(MQCURSOR_TYPE cursor_type)
		/// </summary>
		/// <param name="cursorType">種類</param>
		/// <returns>カーソルハンドル</returns>
		public unsafe IntPtr GetResourceCursor(CursorType cursorType)
		{
			fixed (byte* indexString = GetASCII("index"),
						 resultString = GetASCII("result"))
			{
				var array = new void*[]
				{
					indexString,
					&cursorType,
					resultString,
					null,
					null,
				};

				fixed (void** arrayPtr = array)
					SendMessage(Message.GetResourceCursor, (IntPtr)arrayPtr);

				return (IntPtr)array[3];
			}
		}

		/// <summary>
		/// マウスカーソルを設定
		/// void MQCommandPlugin::SetMouseCursor(HCURSOR cursor)
		/// </summary>
		/// <param name="cursor">カーソルハンドル</param>
		public unsafe void SetMouseCursor(IntPtr cursor)
		{
			fixed (byte* cursorString = GetASCII("cursor"))
			{
				var array = new void*[]
				{
					cursorString,
					(void*)cursor,
					null,
				};

				fixed (void** arrayPtr = array)
					SendMessage(Message.SetMouseCursor, (IntPtr)arrayPtr);
			}
		}

		/// <summary>
		/// ステータスバーの文字列を設定
		/// void MQCommandPlugin::SetStatusString(const char *str)
		/// </summary>
		/// <param name="str">文字列</param>
		public unsafe void SetStatusString(string str)
		{
			var bytes = Encoding.GetEncoding(932).GetBytes(str);

			fixed (byte* stringString = GetASCII("string"),
						 bytesPtr = bytes)
			{
				var array = new void*[]
				{
					stringString,
					bytesPtr,
					null,
				};

				fixed (void** arrayPtr = array)
					SendMessage(Message.SetStatusString, (IntPtr)arrayPtr);
			}
		}
	}
}