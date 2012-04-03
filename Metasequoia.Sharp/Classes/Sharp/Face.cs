
namespace Metasequoia.Sharp
{
	public class Face
	{
		Face(Object obj, uint uniqueId)
		{
			this.Object = obj;
			this.UniqueId = uniqueId;
			this.VertexColor = new ReadWriteIndexer<uint>
			(
				_ => this.Object.GetFaceVertexColor(this.Index, _),
				(_, value) => this.Object.SetFaceVertexColor(this.Index, _, value),
				() => this.PointCount
			);
			this.IsLineSelected = new ReadWriteIndexer<bool>
			(
				_ => Document.Instance.IsSelectLine(this.Object.Index, this.Index, _),
				(_, value) =>
				{
					if (value)
						Document.Instance.AddSelectLine(this.Object.Index, this.Index, _);
					else
						Document.Instance.DeleteSelectLine(this.Object.Index, this.Index, _);
				},
				() => this.PointCount == 2 ? 1 : this.PointCount
			);
			this.IsUVVertexSelected = new ReadWriteIndexer<bool>
			(
				_ => Document.Instance.IsSelectUVVertex(this.Object.Index, this.Index, _),
				(_, value) =>
				{
					if (value)
						Document.Instance.AddSelectUVVertex(this.Object.Index, this.Index, _);
					else
						Document.Instance.DeleteSelectUVVertex(this.Object.Index, this.Index, _);
				},
				() => this.PointCount
			);
		}

		public static Face FromObjectAndId(Object obj, uint uniqueId)
		{
			return new Face(obj, uniqueId);
		}

		public Object Object
		{
			get;
			private set;
		}

		public uint UniqueId
		{
			get;
			private set;
		}

		public int Index
		{
			get
			{
				return this.Object.GetFaceIndexFromUniqueId(this.UniqueId);
			}
		}

		public int PointCount
		{
			get
			{
				return this.Object.GetFacePointCount(this.Index);
			}
		}

		public int[] Points
		{
			get
			{
				return this.Object.GetFacePointArray(this.Index);
			}
		}

		public Coordinate[] Coordinates
		{
			get
			{
				return this.Object.GetFaceCoordinateArray(this.Index);
			}
			set
			{
				this.Object.SetFaceCoordinateArray(this.Index, value);
			}
		}

		public ReadWriteIndexer<uint> VertexColor
		{
			get;
			private set;
		}

		public int Material
		{
			get
			{
				return this.Object.GetFaceMaterial(this.Index);
			}
			set
			{
				this.Object.SetFaceMaterial(this.Index, value);
			}
		}

		public bool IsSelected
		{
			get
			{
				return Document.Instance.IsSelectFace(this.Object.Index, this.Index);
			}
			set
			{
				if (value)
					Document.Instance.AddSelectFace(this.Object.Index, this.Index);
				else
					Document.Instance.DeleteSelectFace(this.Object.Index, this.Index);
			}
		}

		public ReadWriteIndexer<bool> IsLineSelected
		{
			get;
			private set;
		}

		public ReadWriteIndexer<bool> IsUVVertexSelected
		{
			get;
			private set;
		}

		public Point GetNormal()
		{
			var pt = this.Points;

			switch (pt.Length)
			{
				case 3:
					return Point.GetNormal
					(
						this.Object.GetVertex(pt[0]),
						this.Object.GetVertex(pt[1]),
						this.Object.GetVertex(pt[2])
					);
				case 4:
					return Point.GetNormal
					(
						this.Object.GetVertex(pt[0]),
						this.Object.GetVertex(pt[1]),
						this.Object.GetVertex(pt[2]),
						this.Object.GetVertex(pt[3])
					);
				default:
					return Point.Zero;
			}
		}

		public bool Invert()
		{
			return this.Object.InvertFace(this.Index);
		}

		public bool Delete()
		{
			return Delete(true);
		}

		public bool Delete(bool deleteVertex)
		{
			return this.Object.DeleteFace(this.Index, deleteVertex);
		}

		public bool IsFrontFace(Scene scene)
		{
			return this.Object.IsFrontFace(scene, this.Index);
		}

		public int SearchInvertedFace()
		{
			return this.Object.SearchInvertedFace(this.Index);
		}

		public int SearchInvertedFace(int begin, int end)
		{
			return this.Object.SearchInvertedFace(this.Index, begin, end);
		}
	}
}
