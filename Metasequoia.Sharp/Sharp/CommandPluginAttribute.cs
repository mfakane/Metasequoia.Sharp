namespace Metasequoia.Sharp
{
	public class CommandPluginAttribute : StationPluginAttribute
	{
		public CommandPluginAttribute(uint vendorId, uint productId, string displayName)
			: base(PluginType.Command, vendorId, productId, displayName)
		{
		}
	}
}
