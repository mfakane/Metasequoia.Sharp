using System;

namespace Metasequoia.Sharp
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public class PluginAttribute : Attribute
	{
		public uint VendorId
		{
			get;
			set;
		}

		public uint ProductId
		{
			get;
			set;
		}

		public virtual PluginType Kind
		{
			get;
			set;
		}

		public PluginAttribute(PluginType kind, uint vendorId, uint productId)
		{
			this.Kind = kind;
			this.VendorId = vendorId;
			this.ProductId = productId;
		}
	}
}
