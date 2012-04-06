using System.Collections.Generic;
using System.ComponentModel;
using Metasequoia;
using Metasequoia.Sharp;

namespace Linearstar.Metaseq.Explosion
{
	[DisplayName("Explosion\tCopyright(C) 2012, mfakane")]
	[Plugin(PluginType.Select, 0xAB86CB1E, 0xAC5C35F1)]
	public class MainPlugin : PluginBase
	{
		protected override IEnumerable<IPluginFeature> GetFeatures()
		{
			return new []
			{
				new ExplosionFeature(),
			};
		}
	}
}
