using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Metasequoia;
using Metasequoia.Sharp;

namespace Linearstar.Metaseq.CreateBoundingBox
{
	[DisplayName("バウンディングボックス")]
	public class CreateBoundingBoxFeature : BasicPluginFeature
	{
		static bool createNewObject = true;
		static float margin = 0;

		protected override bool Main(Document doc)
		{
			var owner = NativeWindow.FromHandle(Plugin.MainWindowHandle);
			var vertices = doc.Objects.SelectMany(_ => _.Vertices)
									  .Where(_ => _.IsSelected)
									  .ToArray();

			if (vertices.Length == 0)
			{
				MessageBox.Show(owner, "対象の頂点がありません。", "バウンディングボックス", MessageBoxButtons.OK, MessageBoxIcon.Information);

				return false;
			}

			using (var f = new CreateBoundingBoxForm
			{
				CreateAtNewObject = createNewObject,
				CreateAtCurrentObject = !createNewObject,
				CreateMargin = margin,
			})
				if (f.ShowDialog(owner) == DialogResult.OK)
				{
					createNewObject = f.CreateAtNewObject;
					margin = f.CreateMargin;
				}
				else
					return false;

			var min = vertices.Aggregate(vertices.First().Point, (x, y) => new Point(Math.Min(x.X, y.Point.X), Math.Min(x.Y, y.Point.Y), Math.Min(x.Z, y.Point.Z))) - margin;
			var max = vertices.Aggregate(vertices.First().Point, (x, y) => new Point(Math.Max(x.X, y.Point.X), Math.Max(x.Y, y.Point.Y), Math.Max(x.Z, y.Point.Z))) + margin;
			var newVertices = new[]
			{
				new Point(min.X, max.Y, max.Z),
				new Point(min.X, min.Y, max.Z),
				new Point(max.X, max.Y, max.Z),
				new Point(max.X, min.Y, max.Z),
				new Point(max.X, max.Y, min.Z),
				new Point(max.X, min.Y, min.Z),
				new Point(min.X, max.Y, min.Z),
				new Point(min.X, min.Y, min.Z),
			};
			var newIndices = new[]
			{
				new[] { 0, 2, 3, 1 },
				new[] { 2, 4, 5, 3 },
				new[] { 4, 6, 7, 5 },
				new[] { 6, 0, 1, 7 },
				new[] { 6, 4, 2, 0 },
				new[] { 1, 3, 5, 7 },
			};
			var obj = doc.Objects[doc.CurrentObjectIndex];

			if (createNewObject || obj.Locking)
			{
				obj = new Metasequoia.Object();
				createNewObject = true;
			}

			var realVertices = newVertices.Select(obj.AddVertex).ToArray();
			var currentMaterial = doc.CurrentMaterialIndex;

			foreach (var i in newIndices)
				obj.Faces[obj.AddFace(i.Select(_ => realVertices[_]).ToArray())].Material = currentMaterial;

			if (createNewObject)
			{
				doc.AddObject(obj);
				obj.IsSelected = true;
			}

			return true;
		}
	}
}
