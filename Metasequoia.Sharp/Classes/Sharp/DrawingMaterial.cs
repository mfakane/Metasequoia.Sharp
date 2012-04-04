using System;

namespace Metasequoia.Sharp
{
	public class DrawingMaterial : Material
	{
		readonly Action<DrawingMaterial> deleteDrawingMaterial;
		int index;

		public StationPlugin StationPlugin
		{
			get;
			private set;
		}

		public bool IsInstant
		{
			get;
			private set;
		}

		public override int Index
		{
			get
			{
				return index;
			}
		}

		internal DrawingMaterial(StationPlugin plugin, int index, Action<DrawingMaterial> deleteDrawingMaterial, bool instant, IntPtr ptr)
			: base(ptr)
		{
			this.StationPlugin = plugin;
			this.index = index;
			this.deleteDrawingMaterial = deleteDrawingMaterial;
			this.IsInstant = instant;

			if (!this.IsInstant)
				this.StationPlugin.EndDocument += DeleteOnEndDocument;
		}

		void DeleteOnEndDocument(object sender, MetasequoiaEventArgs e)
		{
			Dispose();
		}

		public override void Dispose()
		{
			if (!this.IsInstant)
			{
				this.StationPlugin.EndDocument -= DeleteOnEndDocument;
				deleteDrawingMaterial(this);
			}
		}
	}
}
