using System;
using System.Linq;
using System.Text;

namespace Metasequoia
{
	/// <summary>
	/// Material
	/// </summary>
	partial class Material : IDisposable
	{
		internal bool deletable;

		/// <summary>
		/// Name プロパティで MQMaterial::GetName() に対して使用するバッファサイズを取得または設定します。
		/// </summary>
		public static int NameBufferLength
		{
			get;
			set;
		}

		/// <summary>
		/// TextureName, AlphaName, BumpName プロパティで MQMaterial::GetTextureName() 等に対して使用するバッファサイズを取得または設定します。
		/// </summary>
		public static int TextureNameBufferLength
		{
			get;
			set;
		}

		static Material()
		{
			NameBufferLength = 256;
			TextureNameBufferLength = 260;
		}

		/// <summary>
		/// MQMaterial MQ_CreateMaterial()
		/// </summary>
		public Material()
			: this(NativeMethods.MQ_CreateMaterial())
		{
			deletable = true;
		}

		/// <summary>
		/// マテリアルのインデックスを取得します。
		/// </summary>
		public virtual int Index
		{
			get
			{
				var rt = Document.Instance.Materials.Select((_, idx) => Tuple.Create(_, idx))
													.Where(_ => _.Item1 != null && _.Item1.UniqueId == this.UniqueId)
													.FirstOrDefault();

				return rt == null ? -1 : rt.Item2;
			}
		}

		/// <summary>
		/// マテリアルが選択されているかどうかを取得または設定します。
		/// </summary>
		public bool IsSelected
		{
			get
			{
				return Document.Instance.CurrentMaterialIndex == this.Index;
			}
			set
			{
				Document.Instance.CurrentMaterialIndex = this.Index;
			}
		}

		/// <summary>
		/// マテリアルの名前を取得します。
		/// MQMaterial::GetName(char* buffer, int size)
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
		/// 模様マッピングのファイル名を取得または設定します。
		/// </summary>
		public string TextureName
		{
			get
			{
				var rt = new StringBuilder(TextureNameBufferLength);

				this.GetTextureName(rt, rt.Capacity);

				return rt.ToString();
			}
			set
			{
				this.SetTextureName(value);
			}
		}

		/// <summary>
		/// 透明マッピングのファイル名を取得または設定します。
		/// </summary>
		public string AlphaName
		{
			get
			{
				var rt = new StringBuilder(TextureNameBufferLength);

				this.GetAlphaName(rt, rt.Capacity);

				return rt.ToString();
			}
			set
			{
				this.SetAlphaName(value);
			}
		}

		/// <summary>
		/// 凸凹マッピングのファイル名を取得または設定します。
		/// </summary>
		public string BumpName
		{
			get
			{
				var rt = new StringBuilder(TextureNameBufferLength);

				this.GetBumpName(rt, rt.Capacity);

				return rt.ToString();
			}
			set
			{
				this.SetBumpName(value);
			}
		}

		/// <summary>
		/// void MQDocument::DeleteMaterial(int index);
		/// </summary>
		public void Delete()
		{
			Document.Instance.DeleteMaterial(this.Index);
		}

		/// <summary>
		/// マテリアルクラスを消滅させます。
		/// if (MQMaterial::GetUniqueID() == 0 &amp;&amp; _IS_CREATED_BY_USER_) MQMaterial::DeleteThis()
		/// </summary>
		public virtual void Dispose()
		{
			if (deletable && this.UniqueId == 0)
				NativeMethods.MQMat_Delete(this);

			GC.SuppressFinalize(this);
		}

		~Material()
		{
			Dispose();
		}
	}
}
