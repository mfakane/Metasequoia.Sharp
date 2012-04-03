namespace Metasequoia.Sharp
{
	public class Vertex
	{
		Vertex(Object obj, uint uniqueId)
		{
			this.Object = obj;
			this.UniqueId = uniqueId;
		}

		public static Vertex FromObjectAndId(Object obj, uint uniqueId)
		{
			return new Vertex(obj, uniqueId);
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
				return this.Object.GetVertexIndexFromUniqueId(this.UniqueId);
			}
		}

		public Point Point
		{
			get
			{
				return this.Object.GetVertex(this.Index);
			}
			set
			{
				this.Object.SetVertex(this.Index, value);
			}
		}

		public float Weight
		{
			get
			{
				return this.Object.GetVertexWeight(this.Index);
			}
			set
			{
				this.Object.SetVertexWeight(this.Index, value);
			}
		}

		public int ReferenceCount
		{
			get
			{
				return this.Object.GetVertexRefCount(this.Index);
			}
		}

		public bool IsSelected
		{
			get
			{
				return Document.Instance.IsSelectVertex(this.Object.Index, this.Index);
			}
			set
			{
				if (value)
					Document.Instance.AddSelectVertex(this.Object.Index, this.Index);
				else
					Document.Instance.DeleteSelectVertex(this.Object.Index, this.Index);
			}
		}

		public void CopyAttributeFrom(Vertex fromVertex)
		{
			CopyAttributeFrom(fromVertex.Object, fromVertex.Index);
		}

		public void CopyAttributeFrom(Object fromObject, int fromVertex)
		{
			this.Object.CopyVertexAttribute(this.Index, fromObject, fromVertex);
		}

		public bool Delete()
		{
			return this.Object.DeleteVertex(this.Index);
		}
	}
}
