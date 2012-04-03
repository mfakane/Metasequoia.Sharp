using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Metasequoia.Sharp
{
	/// <summary>
	/// プラグインの基底クラスです。
	/// プラグイン情報は DisplayNameAttribute および PluginAttribute 属性を使用し付加してください。
	/// </summary>
	public abstract class PluginBase : IPlugin
	{
		DisplayNameAttribute displayNameAttribute;
		PluginAttribute pluginAttribute;
		IPluginFeature[] features;

		IEnumerable<T> GetAttributes<T>()
			where T : Attribute
		{
			return this.GetType().GetCustomAttributes(typeof(T), true).Cast<T>();
		}

		DisplayNameAttribute DisplayNameAttribute
		{
			get
			{
				return displayNameAttribute ?? (displayNameAttribute = GetAttributes<DisplayNameAttribute>().Last());
			}
		}

		PluginAttribute PluginAttribute
		{
			get
			{
				return pluginAttribute ?? (pluginAttribute = GetAttributes<PluginAttribute>().Last());
			}
		}

		IPluginFeature[] Features
		{
			get
			{
				return features ?? (features = GetFeatures().ToArray());
			}
		}

		protected abstract IEnumerable<IPluginFeature> GetFeatures();

		#region Events

		public event EventHandler<MetasequoiaEventArgs> RaiseEvent;
		public event EventHandler<MetasequoiaEventArgs> Initialize;
		public event EventHandler<MetasequoiaEventArgs> Exit;
		public event EventHandler<BooleanEventArgs> Activate;
		public event EventHandler<MetasequoiaEventArgs> IsActivated;
		public event EventHandler<BooleanEventArgs> Minimize;
		public event EventHandler<UserMessageEventArgs> UserMessage;
		public event EventHandler<DrawEventArgs> Draw;
		public event EventHandler<MouseEventArgs> LeftButtonDown;
		public event EventHandler<MouseEventArgs> LeftButtonMove;
		public event EventHandler<MouseEventArgs> LeftButtonUp;
		public event EventHandler<MouseEventArgs> MiddleButtonDown;
		public event EventHandler<MouseEventArgs> MiddleButtonMove;
		public event EventHandler<MouseEventArgs> MiddleButtonUp;
		public event EventHandler<MouseEventArgs> RightButtonDown;
		public event EventHandler<MouseEventArgs> RightButtonMove;
		public event EventHandler<MouseEventArgs> RightButtonUp;
		public event EventHandler<MouseEventArgs> MouseWheel;
		public event EventHandler<MouseEventArgs> MouseMove;
		public event EventHandler<KeyEventArgs> KeyDown;
		public event EventHandler<KeyEventArgs> KeyUp;
		public event EventHandler<DocumentEventArgs> NewDocument;
		public event EventHandler<MetasequoiaEventArgs> EndDocument;
		public event EventHandler<DocumentEventArgs> SaveDocument;
		public event EventHandler<UndoEventArgs> Undo;
		public event EventHandler<UndoEventArgs> Redo;
		public event EventHandler<UndoEventArgs> UndoUpdated;
		public event EventHandler<MetasequoiaEventArgs> ObjectModified;
		public event EventHandler<MetasequoiaEventArgs> ObjectSelected;
		public event EventHandler<MetasequoiaEventArgs> ObjectList;
		public event EventHandler<MetasequoiaEventArgs> MaterialModified;
		public event EventHandler<MetasequoiaEventArgs> MaterialList;
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

		void Raise<T>(EventHandler<T> handler, T e)
			where T: EventArgs
		{
			if (handler != null)
				handler(this, e);
		}

		#endregion
		#region IPlugin

		void IPlugin.GetPluginId(ref uint product, ref uint id)
		{
			product = this.PluginAttribute.VendorId;
			id = this.PluginAttribute.ProductId;
		}

		string IPlugin.GetPluginName()
		{
			return this.DisplayNameAttribute.DisplayName;
		}

		PluginType IPlugin.GetPluginType()
		{
			return this.PluginAttribute.Kind;
		}

		bool IPlugin.ImportFile(int index, string filename, Document doc)
		{
			return this.Features[index].ProcessMain(doc, filename);
		}

		bool IPlugin.ExportFile(int index, string filename, Document doc)
		{
			return this.Features[index].ProcessMain(doc, filename);
		}

		string IPlugin.EnumFileType(int index)
		{
			if (index >= this.Features.Length)
				return null;
			else
				return this.Features[index].EnumFileType();
		}

		string IPlugin.EnumFileExt(int index)
		{
			if (index >= this.Features.Length)
				return null;
			else
				return this.Features[index].EnumFileExt();
		}

		string IPlugin.EnumString(int index)
		{
			if (index >= this.Features.Length)
				return null;
			else
				return this.Features[index].EnumString();
		}

		bool IPlugin.Create(int index, Document doc)
		{
			return this.Features[index].ProcessMain(doc, null);
		}

		bool IPlugin.ModifyObject(int index, Document doc)
		{
			return this.Features[index].ProcessMain(doc, null);
		}

		bool IPlugin.ModifySelect(int index, Document doc)
		{
			return this.Features[index].ProcessMain(doc, null);
		}

		bool IPlugin.OnEvent(Document doc, Event eventType, IntPtr option)
		{
			var e = new MetasequoiaEventArgs(doc, eventType, option);

			OnRaiseEvent(e);

			return e.Handled;
		}

		#endregion
	}
}
