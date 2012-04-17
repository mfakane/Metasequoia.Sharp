using System;
using System.Collections.Generic;
using System.Linq;
using Metasequoia;
using Metasequoia.Sharp;

namespace Linearstar.Metaseq.Skeleton
{
	class Bone
	{
		public Bone(Document doc, Face face)
		{
			if (face.PointCount != 3)
				throw new ArgumentException("face.PointCount must be 3");

			var vertices = face.Points.Select(_ => face.Object.Vertices[_]).ToArray();

			// 辺の長さ算出、長さ順
			var lengths = vertices.Select((_, idx) =>
			{
				var nextIdx = (idx + 1) % vertices.Length;
				var next = vertices[nextIdx];
				var length = (next.Point - _.Point).GetSizeSquared();

				return new
				{
					FirstIndex = idx,
					NextIndex = nextIdx,
					FirstVertex = _,
					NextVertex = next,
					Length = length
				};
			})
								  .OrderBy(_ => _.Length)
								  .ToArray();
			var middle = lengths[1];	// 二番目に長い辺がボーンの軸
			var shortest = lengths[0];	// 一番短い辺も取る

			// ボーン軸になる辺のどちらかの頂点が一番短い辺と頂点を共有しているかを調べる
			// 頂点を共有してる方がボーンの始点
			if (middle.FirstIndex == shortest.FirstIndex ||
				middle.FirstIndex == shortest.NextIndex)
			{
				this.Begin = middle.FirstVertex;
				this.End = middle.NextVertex;
				this.Helper = shortest.FirstIndex == middle.FirstIndex ? shortest.NextVertex : shortest.FirstVertex;
			}
			else
			{
				this.Begin = middle.NextVertex;
				this.End = middle.FirstVertex;
				this.Helper = shortest.FirstIndex == middle.NextIndex ? shortest.NextVertex : shortest.FirstVertex;
			}

			this.Face = face;
			this.Material = this.Face.Material == -1 ? null : doc.Materials[this.Face.Material];
		}

		public Material Material
		{
			get;
			private set;
		}

		public string Name
		{
			get
			{
				return this.Material == null ? null : this.Material.Name;
			}
			set
			{
				if (this.Material != null)
					this.Material.Name = value;
			}
		}

		public Face Face
		{
			get;
			private set;
		}

		public Vertex Begin
		{
			get;
			private set;
		}

		public Vertex End
		{
			get;
			private set;
		}

		public Vertex Helper
		{
			get;
			private set;
		}
	}
}
