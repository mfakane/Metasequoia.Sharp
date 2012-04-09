using System;

namespace Metasequoia
{
	/// <summary>
	/// プラグインを表します。
	/// </summary>
	public interface IPlugin
	{
		void GetPluginId(out uint product, out uint id);
		string GetPluginName();
		PluginType GetPluginType();
		bool ImportFile(int index, string filename, Document doc);
		bool ExportFile(int index, string filename, Document doc);
		string EnumFileType(int index);
		string EnumFileExt(int index);
		string EnumString(int index);
		bool Create(int index, Document doc);
		bool ModifyObject(int index, Document doc);
		bool ModifySelect(int index, Document doc);
		bool OnEvent(Document doc, Event eventType, IntPtr option);
	}
}
