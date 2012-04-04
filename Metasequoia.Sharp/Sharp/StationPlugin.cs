using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metasequoia.Sharp
{
	public abstract class StationPlugin : PluginBase, IPlugin
	{
		public event EventHandler<MetasequoiaEventArgs> RaiseEvent;
		/// <summary>
		/// プラグインの初期化
		/// </summary>
		public event EventHandler<MetasequoiaEventArgs> Initialize;
		/// <summary>
		/// プラグインの終了
		/// </summary>
		public event EventHandler<MetasequoiaEventArgs> Exit;
		/// <summary>
		/// 表示・非表示切り替え要求
		/// </summary>
		public event EventHandler<BooleanEventArgs> Activate;
		/// <summary>
		/// 表示・非表示状態の返答
		/// MQCommandPlugin::IsActivated
		/// </summary>
		public event EventHandler<MetasequoiaEventArgs> IsActivated;
		/// <summary>
		/// ウインドウの最小化への返答
		/// MQStationPlugin::OnMinimize
		/// </summary>
		public event EventHandler<BooleanEventArgs> Minimize;
		/// <summary>
		/// MQBasePlugin::SendUserMessage() で他のプラグインからメッセージが送出された時に呼び出されます。
		/// MQStationPlugin::OnReceiveUserMessage
		/// </summary>
		public event EventHandler<UserMessageEventArgs> UserMessage;
		/// <summary>
		/// 描画時に呼び出されます。
		/// MQStationPlugin::OnDraw
		/// </summary>
		public event EventHandler<DrawEventArgs> Draw;
		/// <summary>
		/// ドキュメント初期化時に呼び出されます。
		/// MQStationPlugin::OnNewDocument
		/// </summary>
		public event EventHandler<DocumentEventArgs> NewDocument;
		/// <summary>
		/// ドキュメント終了時に呼び出されます。
		/// MQStationPlugin::OnEndDocument
		/// </summary>
		public event EventHandler<MetasequoiaEventArgs> EndDocument;
		/// <summary>
		/// MQO ファイルが保存される前に呼び出されます。
		/// MQStationPlugin::OnSaveDocument
		/// </summary>
		public event EventHandler<DocumentEventArgs> SaveDocument;
		/// <summary>
		/// アンドゥ実行時に呼び出されます。
		/// MQStationPlugin::OnUndo
		/// </summary>
		public event EventHandler<UndoEventArgs> Undo;
		/// <summary>
		/// リドゥ実行時に呼び出されます。
		/// MQStationPlugin::OnRedo
		/// </summary>
		public event EventHandler<UndoEventArgs> Redo;
		/// <summary>
		/// アンドゥの状態が更新されたときに呼び出されます。
		/// この関数内ではオブジェクトの内容を更に変更してはいけません。
		/// MQStationPlugin::OnUpdateUndo
		/// </summary>
		public event EventHandler<UndoEventArgs> UndoUpdated;
		/// <summary>
		/// オブジェクトが編集された時に呼び出されます。
		/// この関数内ではオブジェクトの内容を更に変更してはいけません。
		/// ※アンドゥが更新される前に呼び出されます。
		/// MQStationPlugin::OnObjectModified
		/// </summary>
		public event EventHandler<MetasequoiaEventArgs> ObjectModified;
		/// <summary>
		/// オブジェクトの頂点・辺・面・UVが選択された時に呼び出されます。
		/// この関数内ではオブジェクトの内容・選択状態を更に変更してはいけません。
		/// ※アンドゥが更新される前に呼び出されます。
		/// MQStationPlugin::OnObjectSelected
		/// </summary>
		public event EventHandler<MetasequoiaEventArgs> ObjectSelected;
		/// <summary>
		/// カレントオブジェクトの変更や、オブジェクトの追加・削除が行われた時に呼び出されます。
		/// この関数内ではオブジェクトの内容・選択状態を更に変更してはいけません。
		/// ※アンドゥが更新される前に呼び出されます。
		/// MQStationPlugin::OnUpdateObjectList
		/// </summary>
		public event EventHandler<MetasequoiaEventArgs> ObjectList;
		/// <summary>
		/// マテリアル内のパラメータが変更された時に呼び出されます。
		/// この関数内ではオブジェクト・マテリアルの内容を更に変更してはいけません。
		/// ※アンドゥが更新される前に呼び出されます。
		/// MQStationPlugin::OnMaterialModified
		/// </summary>
		public event EventHandler<MetasequoiaEventArgs> MaterialModified;
		/// <summary>
		/// カレントマテリアルの変更や、マテリアルの追加・削除が行われた時に呼び出されます。
		/// この関数内ではマテリアルの内容を更に変更してはいけません。
		/// ※アンドゥが更新される前に呼び出されます。
		/// MQStationPlugin::OnUpdateMaterialList
		/// </summary>
		public event EventHandler<MetasequoiaEventArgs> MaterialList;
		/// <summary>
		/// カメラの向きやズームなど、シーン情報が変更された時に呼び出されます。
		/// この関数内ではシーンを更に変更してはいけません。
		/// MQStationPlugin::OnMaterialModified
		/// </summary>
		public event EventHandler<SceneEventArgs> Scene;

		protected virtual void OnRaiseEvent(MetasequoiaEventArgs e)
		{
			Raise(RaiseEvent, e);

			switch (e.EventType)
			{
				case Event.Initialize:
					Raise(Initialize, e);

					break;
				case Event.Exit:
					Raise(Exit, e);
					e.Handled = true;

					break;
				case Event.Activate:
					Raise(Activate, new BooleanEventArgs(e));

					break;
				case Event.IsActivated:
					e.Handled = true;
					Raise(IsActivated, e);

					break;
				case Event.Minimize:
					Raise(Minimize, new BooleanEventArgs(e));

					break;
				case Event.UserMessage:
					Raise(UserMessage, new UserMessageEventArgs(e));

					break;
				case Event.Draw:
					Raise(Draw, new DrawEventArgs(e));

					break;
				case Event.NewDocument:
					Raise(NewDocument, new DocumentEventArgs(e));
					e.Handled = true;

					break;
				case Event.EndDocument:
					Raise(EndDocument, e);
					e.Handled = true;

					break;
				case Event.SaveDocument:
					var de = new DocumentEventArgs(e);

					Raise(SaveDocument, de);

					if (de.SaveUniqueId)
						unsafe
						{
							var saveUid = (int*)MetasequoiaEventArgs.ExtractEventOption(de.Option, "save_uid");

							if (saveUid != null)
								*saveUid = 1;
						}

					e.Handled = true;

					break;
				case Event.Undo:
					Raise(Undo, new UndoEventArgs(e));

					break;
				case Event.Redo:
					Raise(Redo, new UndoEventArgs(e));

					break;
				case Event.UndoUpdated:
					Raise(UndoUpdated, new UndoEventArgs(e));
					e.Handled = true;

					break;
				case Event.ObjectList:
					Raise(ObjectList, e);
					e.Handled = true;

					break;
				case Event.ObjectModified:
					Raise(ObjectModified, e);
					e.Handled = true;

					break;
				case Event.ObjectSelected:
					Raise(ObjectSelected, e);
					e.Handled = true;

					break;
				case Event.MaterialList:
					Raise(MaterialList, e);
					e.Handled = true;

					break;
				case Event.MaterialModified:
					Raise(MaterialModified, e);
					e.Handled = true;

					break;
				case Event.Scene:
					Raise(Scene, new SceneEventArgs(e));
					e.Handled = true;

					break;
			}
		}

		internal void Raise<T>(EventHandler<T> handler, T e)
			where T : EventArgs
		{
			if (handler != null)
				handler(this, e);
		}

		protected override IEnumerable<IPluginFeature> GetFeatures()
		{
			var attr = this.GetType().GetCustomAttributes(typeof(StationPluginAttribute), true);

			return new[]
			{
				new DummyFeature(attr.Cast<StationPluginAttribute>().Last().DisplayName),
			};
		}

		bool IPlugin.OnEvent(Document doc, Event eventType, IntPtr option)
		{
			var e = new MetasequoiaEventArgs(doc, eventType, option);

			OnRaiseEvent(e);

			return e.Handled;
		}

		protected bool SendMessage(Message message)
		{
			return SendMessage(message, IntPtr.Zero);
		}

		protected bool SendMessage(Message message, IntPtr option)
		{
			var info = new SendMessageInfo
			{
				Option = option,
			};

			((IPlugin)this).GetPluginId(ref info.Product, ref info.ID);

			return NativeMethods.MQ_SendMessage((int)message, ref info);
		}

		internal static byte[] GetASCII(string value)
		{
			return Encoding.ASCII.GetBytes(value);
		}

		/// <summary>
		/// プラグインが管理しているウインドウが閉じられたときにこの関数を呼び出しすと、
		/// 本体側に通知されメニュー上のチェックが外れるようになります。
		/// void MQStationPlugin::WindowClose()
		/// </summary>
		protected unsafe void WindowClose()
		{
			var flag = false;

			SendMessage(Message.Activate, (IntPtr)(void*)&flag);
		}

		/// <summary>
		/// Windows メッセージに対する応答をプラグイン内で処理する場合、
		/// この関数を呼び出すと必要な初期化処理が行われた後に
		/// proc が呼び出され、MQDocument に対する処理を行うことができます。
		/// void MQStationPlugin::BeginCallback(void *option)
		/// </summary>
		/// <param name="proc">proc</param>
		/// <param name="option">オプション</param>
		protected void BeginCallback(StationCallbackProc proc, IntPtr option)
		{
			NativeMethods.MQ_StationCallback(proc, option);
		}

		/// <summary>
		/// OnDraw() 時に描画するオブジェクトを追加します。
		/// instant に TRUE を指定して作成されたオブジェクトは描画が完了すると自動的に破棄されます。
		/// instant に FALSE を指定して作成したオブジェクトは、不要になった時点で Dispose() で削除してください。
		/// MQObject MQStationPlugin::CreateDrawingObject(MQDocument doc, DRAW_OBJECT_VIISIBILITY visibility, BOOL instant)
		/// </summary>
		/// <param name="doc">ドキュメント</param>
		/// <param name="visibility">可視フラグ</param>
		/// <param name="instant">自動削除するかどうか</param>
		/// <returns>描画用オブジェクト</returns>
		protected unsafe DrawingObject CreateDrawingObject(Document doc, DrawObjectVisibility visibility, bool instant)
		{
			var instantInt = instant ? 1 : 0;

			fixed (byte* documentString = GetASCII("document"),
						 visibilityString = GetASCII("visibility"),
						 instantString = GetASCII("instant"),
						 resultString = GetASCII("result"))
			{
				var array = new void*[]
				{
					documentString,
					(void*)doc.Handle,
					visibilityString,
					&visibility,
					instantString,
					&instantInt,
					resultString,
					null,
					null
				};

				fixed (void** option = array)
					SendMessage(Message.NewDrawObject, (IntPtr)option);

				return new DrawingObject(this, _ => DeleteDrawingObject(doc, _), instant, (IntPtr)array[7]);
			}
		}

		unsafe void DeleteDrawingObject(Document doc, DrawingObject obj)
		{
			fixed (byte* documentString = GetASCII("document"),
						 objectString = GetASCII("object"))
			{
				var array = new void*[]
				{
					documentString,
					(void*)doc.Handle,
					objectString,
					(void*)obj.Handle,
					null
				};

				fixed (void** option = array)
					SendMessage(Message.DeleteDrawObject, (IntPtr)option);
			}
		}

		/// <summary>
		/// OnDraw() 時の CreateDrawingObject() に作成したオブジェクト描画に使用するマテリアルを追加します。
		/// instant に TRUE を指定して作成されたマテリアルは描画が完了すると自動的に破棄されます。
		/// instant に FALSE を指定して作成したマテリアルは、不要になった時点で Dispose() で削除してください。
		/// MQMaterial MQStationPlugin::CreateDrawingMaterial(MQDocument doc, int&amp; index, BOOL instant)
		/// </summary>
		/// <param name="doc">ドキュメント</param>
		/// <param name="instant">自動削除するかどうか</param>
		/// <returns>描画用オブジェクト</returns>
		protected unsafe DrawingMaterial CreateDrawingMaterial(Document doc, bool instant)
		{
			var instantInt = instant ? 1 : 0;
			var index = -1;

			fixed (byte* documentString = GetASCII("document"),
						 indexString = GetASCII("index"),
						 instantString = GetASCII("instant"),
						 resultString = GetASCII("result"))
			{
				var array = new void*[]
				{
					documentString,
					(void*)doc.Handle,
					indexString,
					&index,
					instantString,
					&instantInt,
					resultString,
					null,
					null
				};

				fixed (void** option = array)
					SendMessage(Message.NewDrawMaterial, (IntPtr)option);

				return new DrawingMaterial(this, index, _ => DeleteDrawingMaterial(doc, _), instant, (IntPtr)array[7]);
			}
		}

		unsafe void DeleteDrawingMaterial(Document doc, DrawingMaterial mat)
		{
			fixed (byte* documentString = GetASCII("document"),
						 materialString = GetASCII("material"))
			{
				var array = new void*[]
				{
					documentString,
					(void*)doc.Handle,
					materialString,
					(void*)mat.Handle,
					null
				};

				fixed (void** option = array)
					SendMessage(Message.DeleteDrawMaterial, (IntPtr)option);
			}
		}

		/// <summary>
		/// 現在のアンドゥの状態カウンタを取得します。
		/// int MQStationPlugin::GetCurrentUndoState(MQDocument doc)
		/// </summary>
		/// <param name="doc">ドキュメント</param>
		/// <returns>状態カウンタ</returns>
		protected unsafe int GetCurrentUndoState(Document doc)
		{
			fixed (byte* documentString = GetASCII("document"),
						 resultString = GetASCII("result"))
			{
				var state = -1;
				var array = new void*[]
				{
					documentString,
					(void*)doc.Handle,
					resultString,
					&state,
					null
				};

				fixed (void** option = array)
					SendMessage(Message.GetUndoState, (IntPtr)option);

				return state;
			}
		}

		/// <summary>
		/// シーンの表示オプションを取得
		/// void MQStationPlugin::GetSceneOption(MQScene scene, SCENE_OPTION&amp; option)
		/// </summary>
		/// <param name="doc">ドキュメント</param>
		/// <returns>表示オプション</returns>
		protected unsafe SceneOption GetSceneOption(Document doc)
		{
			fixed (byte* documentString = GetASCII("document"),
						 showVertexString = GetASCII("show_vertex"),
						 showLineString = GetASCII("show_line"),
						 showFaceString = GetASCII("show_face"),
						 frontOnlyString = GetASCII("front_only"),
						 showBkImgString = GetASCII("show_bkimg"))
			{
				var rt = new SceneOption();
				var array = new void*[]
				{
					documentString,
					(void*)doc.Handle,
					showVertexString,
					&rt.ShowVertex,
					showLineString,
					&rt.ShowLine,
					showFaceString,
					&rt.ShowFace,
					frontOnlyString,
					&rt.FrontOnly,
					showBkImgString,
					&rt.ShowBackgroundImage,
					null
				};

				fixed (void** option = array)
					SendMessage(Message.GetSceneOption, (IntPtr)option);

				return rt;
			}
		}

		class DummyFeature : IPluginFeature
		{
			readonly string enumString;

			public DummyFeature(string enumString)
			{
				this.enumString = enumString;
			}

			IPlugin IPluginFeature.Plugin
			{
				get;
				set;
			}

			string IPluginFeature.EnumString()
			{
				return enumString;
			}

			string IPluginFeature.EnumFileType()
			{
				throw new NotSupportedException();
			}

			string IPluginFeature.EnumFileExt()
			{
				throw new NotSupportedException();
			}

			bool IPluginFeature.ProcessMain(Document doc, string filename)
			{
				throw new NotSupportedException();
			}
		}
	}
}
