using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

namespace Metasequoia
{
	partial class Document
	{
		public static Document Instance
		{
			get;
			private set;
		}

		partial void Initialize()
		{
			Instance = this;
			this.Objects = new ReadOnlyIndexer<Object>(this.GetObject, () => this.ObjectCount);
			this.Materials = new ReadOnlyIndexer<Material>(this.GetMaterial, () => this.MaterialCount);
		}

		public ReadOnlyIndexer<Object> Objects
		{
			get;
			private set;
		}

		public ReadOnlyIndexer<Material> Materials
		{
			get;
			private set;
		}

		public string GetUnusedObjectName()
		{
			return GetUnusedObjectName(null);
		}

		public string GetUnusedObjectName(string basename)
		{
			var sb = new StringBuilder(Object.NameBufferLength);

			this.GetUnusedObjectName(sb, sb.Capacity, basename);

			return sb.ToString();
		}

		public string GetUnusedMaterialName()
		{
			return GetUnusedMaterialName(null);
		}

		public string GetUnusedMaterialName(string basename)
		{
			var sb = new StringBuilder(Material.NameBufferLength);

			this.GetUnusedMaterialName(sb, sb.Capacity, basename);

			return sb.ToString();
		}

		public string FindMappingFile(string filename, Mapping mapType)
		{
			var sb = new StringBuilder(Material.TextureNameBufferLength);

			this.FindMappingFile(sb, filename, mapType);

			return sb.ToString();
		}

		partial void BeforeAddObject(Object obj)
		{
			obj.deletable = false;
		}

		partial void BeforeInsertObject(Object obj, Object before)
		{
			obj.deletable = false;
		}

		partial void BeforeAddMaterial(Material mat)
		{
			mat.deletable = false;
		}

		#region Interop

		/// <summary>
		/// BOOL GetMappingImageSize(const char *filename, DWORD map_type, int&amp; width, int&amp; height)
		/// </summary>
		public unsafe bool GetMappingImageSize(string filename, Mapping mapType, out int width, out int height)
		{
			var array = new IntPtr[5];
			int bpp;
			IntPtr buffer;

			fixed (int* widthp = &width)
			fixed (int* heightp = &height)
			{
				array[0] = (IntPtr)widthp;
				array[1] = (IntPtr)heightp;
				array[2] = (IntPtr)(&bpp);
				array[3] = IntPtr.Zero;
				array[4] = (IntPtr)(&buffer);

				if (!NativeMethods.MQDoc_GetMappingImage(this, filename, (uint)mapType, array))
				{
					width = height = 0;
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// int CreateUserData(DWORD productID, DWORD pluginID, const char *identifier, int userdata_type, int bytes_per_object)
		/// </summary>
		public int CreateUserData(uint productID, uint pluginID, string identifier, int userdata_type, int bytes_per_object)
		{
			BeforeCreateUserData(productID, pluginID, identifier, userdata_type, bytes_per_object);

			var info = new UserDataInfo
			{
				dwSize = (uint)Marshal.SizeOf(typeof(UserDataInfo)),
				ProductID = productID,
				PluginID = pluginID,
				Identifier = Plugin.Get932(identifier).Take(15).Concat(new byte[] { 0 }).ToArray(),
				UserdataType = userdata_type,
				BytesPerElement = bytes_per_object,
				Create = true,
			};

			return NativeMethods.MQDoc_CreateUserData(this, ref info);
		}

		partial void BeforeCreateUserData(uint productID, uint pluginID, string identifier, int userdata_type, int bytes_per_object);
		/// <summary>
		/// int FindUserData(DWORD productID, DWORD pluginID, const char *identifier, int userdata_type)
		/// </summary>
		public int FindUserData(uint productID, uint pluginID, string identifier, int userdata_type)
		{
			BeforeFindUserData(productID, pluginID, identifier, userdata_type);

			var info = new UserDataInfo
			{
				dwSize = (uint)Marshal.SizeOf(typeof(UserDataInfo)),
				ProductID = productID,
				PluginID = pluginID,
				Identifier = Plugin.Get932(identifier).Take(15).Concat(new byte[] { 0 }).ToArray(),
				UserdataType = userdata_type,
				BytesPerElement = 0,
			};

			return NativeMethods.MQDoc_CreateUserData(this, ref info);
		}
		partial void BeforeFindUserData(uint productID, uint pluginID, string identifier, int userdata_type);

		#endregion
	}
}
