using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Metasequoia.Sharp
{
	/// <summary>
	/// プラグイン機能の基底クラスです。
	/// 表示名は DisplayNameAttribute を使用し付加してください。
	/// </summary>
	public abstract class PluginFeatureBase : IPluginFeature
	{
		DisplayNameAttribute displayNameAttribute;

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

		public PluginBase Owner
		{
			get
			{
				return ((IPluginFeature)this).Plugin as PluginBase;
			}
		}

		protected abstract bool Main(Document doc, string filename);

		IPlugin IPluginFeature.Plugin
		{
			get;
			set;
		}

		string IPluginFeature.EnumString()
		{
			return this.DisplayNameAttribute.DisplayName;
		}

		string IPluginFeature.EnumFileType()
		{
			return this.DisplayNameAttribute.DisplayName;
		}

		string IPluginFeature.EnumFileExt()
		{
			if (this.DisplayNameAttribute.DisplayName.Contains('(') &&
				this.DisplayNameAttribute.DisplayName.EndsWith(")"))
				return this.DisplayNameAttribute.DisplayName.Substring(this.DisplayNameAttribute.DisplayName.IndexOf('(')).Trim('(', ')').TrimStart('*', '.');
			else
				return null;
		}

		bool IPluginFeature.ProcessMain(Document doc, string filename)
		{
			return Main(doc, filename);
		}
	}
}
