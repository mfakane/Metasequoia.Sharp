namespace Metasequoia.Sharp
{
	/// <summary>
	/// プラグイン機能を表します。
	/// </summary>
	public interface IPluginFeature
	{
		IPlugin Plugin
		{
			get;
			set;
		}

		string EnumString();
		string EnumFileType();
		string EnumFileExt();
		bool ProcessMain(Document doc, string filename);
	}
}
