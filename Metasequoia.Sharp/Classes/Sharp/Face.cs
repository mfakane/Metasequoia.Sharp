
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
				var rt = new int[this.PointCount];

				this.Object.GetFacePointArray(this.Index, rt);

				return rt;
			}
		}

		public Coordinate[] Coordinates
		{
			get
			{
				var rt = new Coordinate[this.PointCount];

				this.Object.GetFaceCoordinateArray(this.Index, rt);

				return rt;
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
	}
}
