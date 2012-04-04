using System;

namespace Metasequoia.Sharp
{
	public class DrawingObject : Object
	{
		readonly Action<DrawingObject> deleteDrawingObject;

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

		internal DrawingObject(StationPlugin plugin, Action<DrawingObject> deleteDrawingObject, bool instant, IntPtr ptr)
			: base(ptr)
		{
			this.StationPlugin = plugin;
			this.deleteDrawingObject = deleteDrawingObject;
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
				deleteDrawingObject(this);
			}
		}
	}
}
