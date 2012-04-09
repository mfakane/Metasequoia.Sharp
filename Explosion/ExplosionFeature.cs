using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Metasequoia;
using Metasequoia.Sharp;

namespace Linearstar.Metaseq.Explosion
{
	[DisplayName("エクスプロード")]
	public class ExplosionFeature : BasicPluginFeature
	{
		static Tuple<float, float> power = Tuple.Create(10f, 10f);
		static Tuple<float, float> scale = Tuple.Create(1f, 1f);
		static Tuple<float, float> rotate = Tuple.Create(0f, 0f);
		static int seed;

		protected override bool Main(Document doc)
		{
			var owner = NativeWindow.FromHandle(Plugin.MainWindowHandle);

			using (var f = new ExplosionForm
			{
				Power = power,
				Scaling = scale,
				Rotation = rotate,
				Seed = seed,
			})
				if (f.ShowDialog(owner) == DialogResult.OK)
				{
					power = f.Power;
					scale = f.Scaling;
					rotate = f.Rotation;
					seed = f.Seed;
				}
				else
					return false;

			var r = new Random(seed);

			foreach (var i in doc.Objects.SelectMany(_ => _.Faces)
										 .Where(_ => _.IsSelected)
										 .ToArray())
			{
				var vertices = i.Points.Select(_ => i.Object.Vertices[_])
									   .Select(_ =>
									   {
										   var v = i.Object.Vertices[_.Object.AddVertex(_.Point)];

										   v.CopyAttributeFrom(_);

										   return v;
									   })
									   .ToArray();
				var face = i.Object.Faces[i.Object.AddFace(vertices.Select(_ => _.Index))];
				var normal = i.GetNormal();
				var center = vertices.Select(_ => _.Point).Aggregate((x, y) => x + y) / vertices.Length;
				var m = Matrix.Identity.SetTransform(new Point(Chaos(r, scale)), new Angle(Chaos(r, rotate), Chaos(r, rotate), Chaos(r, rotate)), center);
				var pow = Chaos(r, power);

				foreach (var j in vertices)
				{
					var pt = j.Point;

					pt = Point.Transform(pt - center, m);
					pt += normal * pow;
					j.Point = pt;
				}

				face.Material = i.Material;
				face.Coordinates = i.Coordinates;
				face.IsSelected = true;
				i.Delete();
			}

			return true;
		}

		static float Chaos(Random r, Tuple<float, float> range)
		{
			return (float)(range.Item1 + r.NextDouble() * (range.Item2 - range.Item1));
		}
	}
}
