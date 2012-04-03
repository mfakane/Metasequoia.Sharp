using System;
using Metasequoia.Sharp;

namespace Metasequoia
{
	/// <summary>
	/// MQObjNormal
	/// </summary>
	public class ObjectNormal
	{
		Point[] normal;

		public Point this[int faceIndex, int ptIndex]
		{
			get
			{
				return normal[faceIndex * 4 + ptIndex];
			}
		}

		public ObjectNormal(Object obj)
		{
			var faceCount = obj.FaceCount;
			var vertCount = obj.VertexCount;
			var faceN = new Point[faceCount];
			var vi = new int[4];

			normal = new Point[faceCount * 4];

			for (int i = 0; i < faceCount; i++)
			{
				var count = obj.GetFacePointCount(i);

				switch (count)
				{
					case 3:
						obj.GetFacePointArray(i, vi);
						faceN[i] = Point.GetNormal
						(
							obj.GetVertex(vi[0]),
							obj.GetVertex(vi[1]),
							obj.GetVertex(vi[2])
						);

						break;
					case 4:
						obj.GetFacePointArray(i, vi);
						faceN[i] = Point.GetNormal
						(
							obj.GetVertex(vi[0]),
							obj.GetVertex(vi[1]),
							obj.GetVertex(vi[2]),
							obj.GetVertex(vi[3])
						);

						break;
				}
			}

			switch (obj.Shading)
			{
				case ObjectShade.Flat:
					for (int i = 0; i < faceCount; i++)
					{
						var count = obj.GetFacePointCount(i);

						for (int j = 0; j < count; j++)
							normal[i * 4 + j] = faceN[i];
					}

					break;
				case ObjectShade.Gouraud:
					{
						var facet = Math.Cos(MathHelper.ToRadians(obj.SmoothAngle));
						var vtbl = new GouraudHash[faceCount][];
						var hash = new GouraudHash[vertCount + faceCount * 4];
						var hashCount = vertCount;

						for (var i = 0; i < faceCount; i++)
						{
							var cvt = vtbl[i];
							var count = obj.GetFacePointCount(i);

							if (cvt == null)
								cvt = vtbl[i] = new GouraudHash[4];

							if (count < 3)
								continue;

							obj.GetFacePointArray(i, vi);

							for (int j = 0; j < count; j++)
							{
								var chs = hash[vi[j]] ?? (hash[vi[j]] = new GouraudHash());

								if (chs.Count == 0)
								{
									chs.NV = faceN[i];
									chs.Count++;
									cvt[j] = chs;

									continue;
								}

								var pa = faceN[i];
								var da = pa.GetSizeSquared();

								for (; ; chs = chs.Next)
								{
									var c = 0f;

									if (da > 0)
									{
										var pb = chs.NV;
										var db = pb.GetSizeSquared();

										if (db > 0)
											c = Point.GetInnerProduct(pa, pb) / (float)Math.Sqrt(da * db);
									}

									if (c >= facet)
									{
										chs.NV += pa;
										chs.Count++;
										cvt[j] = chs;
									}

									if (chs.Next == null)
									{
										cvt[j] = chs.Next = hash[hashCount++] ?? (hash[hashCount] = new GouraudHash());
										chs = chs.Next;
										chs.NV = pa;
										chs.Count = 1;
										chs.Next = null;

										break;
									}
								}
							}
						}

						for (int i = 0; i < hashCount; i++)
						{
							var chs = hash[i] ?? (hash[i] = new GouraudHash());

							if (chs.Count > 1)
								chs.NV = chs.NV.Normalize();
						}

						for (int i = 0; i < faceCount; i++)
						{
							var cvt = vtbl[i];
							var count = obj.GetFacePointCount(i);

							if (count < 3)
								continue;

							for (int j = 0; j < count; j++)
								normal[i * 4 + j] = cvt[j].NV;
						}
					}

					break;
			}
		}

		class GouraudHash
		{
			public GouraudHash Next
			{
				get;
				set;
			}

			public Point NV
			{
				get;
				set;
			}

			public int Count
			{
				get;
				set;
			}
		}
	}
}
