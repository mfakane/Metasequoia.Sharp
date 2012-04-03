using System.Text;

namespace Metasequoia
{
	partial class Document
	{
		public static Document Instance
		{
			get;
			private set;
		}

		partial void Initialize()
		{
			Instance = this;
			this.Objects = new ReadOnlyIndexer<Object>(this.GetObject, () => this.ObjectCount);
			this.Materials = new ReadOnlyIndexer<Material>(this.GetMaterial, () => this.MaterialCount);
		}

		public ReadOnlyIndexer<Object> Objects
		{
			get;
			private set;
		}

		public ReadOnlyIndexer<Material> Materials
		{
			get;
			private set;
		}

		public string FindMappingFile(string filename, Mapping mapType)
		{
			var sb = new StringBuilder(Material.TextureNameBufferLength);

			this.FindMappingFile(sb, filename, mapType);

			return sb.ToString();
		}
	}
}
