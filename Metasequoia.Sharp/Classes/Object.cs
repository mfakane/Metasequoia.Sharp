using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Metasequoia.Sharp;

namespace Metasequoia
{
	/// <summary>
	/// MQObject
	/// </summary>
	partial class Object : IDisposable
	{
		internal bool deletable;

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
			deletable = true;
		}

		partial void Initialize()
		{
			this.Vertices = new ReadOnlyIndexer<Vertex>(_ =>
			{
				var refCount = this.GetVertexRefCount(_);

				return refCount == 0 ? null : Vertex.FromObjectAndIndex(this, _);
			}, () => this.VertexCount);
			this.Faces = new ReadOnlyIndexer<Face>(_ =>
			{
				var pointCount = this.GetFacePointCount(_);

				return pointCount == 0 ? null : Face.FromObjectAndIndex(this, _);
			}, () => this.FaceCount);
			this.Children = new ReadOnlyIndexer<Object>(_ => Document.Instance.GetChildObject(this, _), () => Document.Instance.GetChildObjectCount(this));
		}

		/// <summary>
		/// 反対向きの面が存在するかどうかを調べる。
		/// 存在していればその面のインデックスを、なければ -1 を返す
		/// int SearchInvertedFace(MQObject obj, int faceindex, int start, int end)
		/// </summary>
		/// <param name="faceindex">調べる面</param>
		/// <returns>存在していればその面のインデックス、なければ -1</returns>
		public int SearchInvertedFace(int faceindex)
		{
			return SearchInvertedFace(faceindex, -1, -1);
		}

		/// <summary>
		/// 反対向きの面が存在するかどうかを調べる。
		/// 存在していればその面のインデックスを、なければ -1 を返す
		/// int SearchInvertedFace(MQObject obj, int faceindex, int start, int end)
		/// </summary>
		/// <param name="faceindex">調べる面</param>
		/// <param name="start">調べ始めるインデックス</param>
		/// <param name="end">調べ終わるインデックス</param>
		/// <returns>存在していればその面のインデックス、なければ -1</returns>
		public int SearchInvertedFace(int faceindex, int start, int end)
		{
			var faceCount = this.FaceCount;

			if (faceindex >= faceCount)
				return -1;

			var ptCount = this.GetFacePointCount(faceindex);
			var cvidx = new int[ptCount];
			var dvidx = new int[ptCount];

			if (ptCount == 0)
				return -1;

			this.GetFacePointArray(faceindex, dvidx);

			if (start < 0)
				start = 0;

			if (end < 0)
				end = faceCount - 1;

			for (int i = start; i <= end; i++)
			{
				if (this.GetFacePointCount(i) != ptCount)
					continue;

				this.GetFacePointArray(i, cvidx);

				for (int j = 0; j < ptCount; j++)
				{
					if (cvidx[j] != dvidx[0])
						continue;

					var k = 0;

					for (k = 0; k < ptCount; k++)
						if (cvidx[(j + k) % ptCount] != dvidx[ptCount])
							break;

					if (k == ptCount)
						return i;
				}
			}

			return -1;
		}

		/// <summary>
		/// 面の向きが表かどうか調べる
		/// bool IsFrontFace(MQScene scene, MQObject obj, int face_index)
		/// </summary>
		/// <param name="scene">シーン</param>
		/// <param name="faceIndex">面</param>
		/// <returns>向きが表かどうか</returns>
		public bool IsFrontFace(Scene scene, int faceIndex)
		{
			var num = this.GetFacePointCount(faceIndex);
			var vertIndex = new int[num];

			this.GetFacePointArray(faceIndex, vertIndex);

			var sp = new Point[num];

			for (int i = 0; i < num; i++)
			{
				sp[i] = scene.Convert3dToScreen(this.GetVertex(vertIndex[i]));

				if (sp[i].Z <= 0)
					return false;
			}

			if (num >= 3)
			{
				if ((sp[1].X - sp[0].X) * (sp[2].Y - sp[1].Y) - (sp[1].Y - sp[0].Y) * (sp[2].X - sp[1].X) < 0)
					return true;
				else if (num >= 4)
					if ((sp[2].X - sp[0].X) * (sp[3].Y - sp[2].Y) - (sp[2].Y - sp[0].Y) * (sp[3].X - sp[2].X) < 0)
						return true;
			}
			else if (num > 0)
				return true;

			return false;
		}

		public int[] GetFacePointArray(int face)
		{
			var arr = new int[this.GetFacePointCount(face)];

			this.GetFacePointArray(face, arr);

			return arr;
		}

		public Coordinate[] GetFaceCoordinateArray(int face)
		{
			var arr = new Coordinate[this.GetFacePointCount(face)];

			this.GetFaceCoordinateArray(face, arr);

			return arr;
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
			return AddFace(vertices.ToArray());
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
		/// if (MQObject::GetUniqueID() == 0 &amp;&amp; _IS_CREATED_BY_USER_) MQObject::DeleteThis()
		/// </summary>
		public virtual void Dispose()
		{
			if (this.UniqueId == 0 && deletable)
				NativeMethods.MQObj_Delete(this);

			GC.SuppressFinalize(this);
		}

		~Object()
		{
			Dispose();
		}
	}
}
