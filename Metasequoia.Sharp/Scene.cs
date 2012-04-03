namespace Metasequoia
{
	partial class Scene
	{
		public Point Convert3dToScreen(Point p)
		{
			float w;

			return Convert3dToScreen(ref p, out w);
		}
	}
}
