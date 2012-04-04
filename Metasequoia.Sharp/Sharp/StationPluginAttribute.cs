namespace Metasequoia.Sharp
{
	public class StationPluginAttribute : PluginAttribute
	{
		public string DisplayName
		{
			get;
			private set;
		}

		protected StationPluginAttribute(PluginType kind, uint vendorId, uint productId, string displayName)
			: base(kind, vendorId, productId)
		{
			this.DisplayName = displayName;
		}

		public StationPluginAttribute(uint vendorId, uint productId, string displayName)
			: this(PluginType.Station, vendorId, productId, displayName)
		{
		}
	}
}
