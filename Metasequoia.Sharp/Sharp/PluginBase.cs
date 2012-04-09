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

		public Setting OpenSetting()
		{
			return Plugin.OpenSetting(this);
		}

		#region IPlugin

		void IPlugin.GetPluginId(out uint product, out uint id)
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
			throw new NotImplementedException();
		}

		#endregion
	}
}
