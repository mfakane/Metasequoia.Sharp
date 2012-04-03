namespace Metasequoia.Sharp
{
	/// <summary>
	/// 基本的なプラグイン機能を表します。
	/// 表示名は DisplayNameAttribute を使用し付加してください。
	/// </summary>
	public abstract class BasicPluginFeature : PluginFeatureBase
	{
		protected abstract bool Main(Document doc);

		protected override bool Main(Document doc, string filename)
		{
			return Main(doc);
		}
	}
}
