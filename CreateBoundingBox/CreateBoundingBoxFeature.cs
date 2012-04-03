using System;
using System.ComponentModel;
using System.Linq;
using Metasequoia;
using Metasequoia.Sharp;

namespace Linearstar.Metaseq.CreateBoundingBox
{
	[DisplayName("バウンディングボックス")]
	public class CreateBoundingBoxFeature : BasicPluginFeature
	{
		protected override bool Main(Document doc)
		{
			var vertices = doc.Objects.SelectMany(_ => _.Vertices)
									  .Where(_ => _.IsSelected)
									  .ToArray();

			if (vertices.Length < 3)
				return false;

			var min = vertices.Aggregate(vertices.First().Point, (x, y) => new Point(Math.Min(x.X, y.Point.X), Math.Min(x.Y, y.Point.Y), Math.Min(x.Z, y.Point.Z)));
			var max = vertices.Aggregate(vertices.First().Point, (x, y) => new Point(Math.Max(x.X, y.Point.X), Math.Max(x.Y, y.Point.Y), Math.Max(x.Z, y.Point.Z)));
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
			var obj = new Metasequoia.Object();
			var realVertices = newVertices.Select(obj.AddVertex).ToArray();
			var currentMaterial = doc.CurrentMaterialIndex;

			foreach (var i in newIndices)
				obj.Faces[obj.AddFace(i.Select(_ => realVertices[_]).ToArray())].Material = currentMaterial;

			doc.AddObject(obj);
			obj.IsSelected = true;

			return true;
		}
	}
}
