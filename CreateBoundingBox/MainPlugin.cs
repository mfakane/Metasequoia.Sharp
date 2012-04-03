using System.Collections.Generic;
using System.ComponentModel;
using Metasequoia;
using Metasequoia.Sharp;

namespace Linearstar.Metaseq.CreateBoundingBox
{
	[DisplayName("CreateBoundingBox\tCopyright(C) 2012, mfakane")]
	[Plugin(PluginType.Select, 0xAB86CB1E, 0x394449BD)]
	public class MainPlugin : PluginBase
	{
		protected override IEnumerable<IPluginFeature> GetFeatures()
		{
			return new[]
			{
				new CreateBoundingBoxFeature(),
			};
		}
	}
}
