using System;
using System.Collections.Generic;
using System.Text;
using Metasequoia.Sharp;

namespace Metasequoia
{
	/// <summary>
	/// MQObject
	/// </summary>
	partial class Object : IDisposable
	{
		/// <summary>
		/// Name プロパティで MQObject::GetName() に対して使用するバッファサイズを取得または設定します。
		/// </summary>
		public static int NameBufferLength
		{
			get;
			set;
		}

		static Object()
		{
			NameBufferLength = 256;
		}

		/// <summary>
		/// MQObject MQ_CreateObject()
		/// </summary>
		public Object()
			: this(NativeMethods.MQ_CreateObject())
		{
		}

		partial void Initialize()
		{
			this.Vertices = new ReadOnlyIndexer<Vertex>(_ =>
			{
				var uniqueId = this.GetVertexUniqueId(_);

				return uniqueId == 0 ? null : Vertex.FromObjectAndId(this, uniqueId);
			}, () => this.VertexCount);
			this.Faces = new ReadOnlyIndexer<Face>(_ =>
			{
				var uniqueId = this.GetFaceUniqueId(_);

				return uniqueId == 0 ? null : Face.FromObjectAndId(this, uniqueId);
			}, () => this.FaceCount);
			this.Children = new ReadOnlyIndexer<Object>(_ => Document.Instance.GetChildObject(this, _), () => Document.Instance.GetChildObjectCount(this));
		}

		/// <summary>
		/// オブジェクトのインデックスを取得します。
		/// </summary>
		public int Index
		{
			get
			{
				return Document.Instance.GetObjectIndex(this);
			}
		}

		/// <summary>
		/// オブジェクトが選択されているかどうかを取得または設定します。
		/// </summary>
		public bool IsSelected
		{
			get
			{
				return Document.Instance.CurrentObjectIndex == this.Index;
			}
			set
			{
				Document.Instance.CurrentObjectIndex = this.Index;
			}
		}

		/// <summary>
		/// 頂点を取得します。
		/// </summary>
		public ReadOnlyIndexer<Vertex> Vertices
		{
			get;
			private set;
		}

		/// <summary>
		/// 面を取得します。
		/// </summary>
		public ReadOnlyIndexer<Face> Faces
		{
			get;
			private set;
		}

		/// <summary>
		/// 親オブジェクトを取得します。
		/// </summary>
		public Object Parent
		{
			get
			{
				return Document.Instance.GetParentObject(this);
			}
		}

		/// <summary>
		/// 子オブジェクトを取得します。
		/// </summary>
		public ReadOnlyIndexer<Object> Children
		{
			get;
			private set;
		}

		public int AddFace(IEnumerable<int> vertices)
		{
			return AddFace(vertices);
		}

		public int AddFace(int[] vertices)
		{
			return this.AddFace(vertices.Length, vertices);
		}

		public bool DeleteFace(int index)
		{
			return this.DeleteFace(index, true);
		}

		/// <summary>
		/// オブジェクトの名前を取得します。
		/// MQObject::GetName(char* buffer, int size)
		/// </summary>
		public string Name
		{
			get
			{
				var rt = new StringBuilder(NameBufferLength);

				this.GetName(rt, rt.Capacity);

				return rt.ToString();
			}
			set
			{
				this.SetName(value);
			}
		}

		/// <summary>
		/// void MQDocument::DeleteObject(int index);
		/// </summary>
		public void Delete()
		{
			Document.Instance.DeleteObject(this.Index);
		}

		/// <summary>
		/// オブジェクトクラスを消滅させます。
		/// if (MQObject::GetUniqueID() == 0) MQObject::DeleteThis()
		/// </summary>
		public void Dispose()
		{
			if (this.UniqueId == 0)
				NativeMethods.MQObj_Delete(this);

			GC.SuppressFinalize(this);
		}

		~Object()
		{
			Dispose();
		}
	}
}
