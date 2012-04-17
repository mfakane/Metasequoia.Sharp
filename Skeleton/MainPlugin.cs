using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Metasequoia;
using Metasequoia.Sharp;

namespace Linearstar.Metaseq.Skeleton
{
	[DisplayName("Skeleton\tCopyright(C) 2012, mfakane")]
	[CommandPlugin(0xAB86CB1E, 0x62A49687, "スケルトン")]
	public class MainPlugin : CommandPlugin
	{
		const int MinimalBoneSize = 24;
		readonly Color highlightColor = new Color(1, 1, 0);
		readonly Color workingColor = new Color(77 / 255f, 191 / 255f, 217 / 255f);
		Metasequoia.Object bone;

		public MainPlugin()
		{
			SkeletonForm f = null;
			CreateBonePointInfo createBonePointInfo = null;
			Tuple<int, int>[] anchorSelect = null;

			this.Initialize += (sender, e) =>
			{
				f = new SkeletonForm();

				f.ModeChanged += (sender2, e2) =>
				{
					createBonePointInfo = null;
					Plugin.RefreshView();
				};
				f.CreateAnchor += (sender2, e2) => this.BeginCallback(_ =>
				{
					bone = _.Objects.FirstOrDefault(o => o.Name.StartsWith("bone"));
					CreateAnchor(_, f.AnchorMargin, f.SnapAnchorToParent, f.CreateSymmetricalAnchor);
				});
				f.FormClosing += (sender2, e2) =>
				{
					e2.Cancel = true;
					f.Hide();
				};
				f.VisibleChanged += (sender2, e2) =>
				{
					if (!f.Visible)
						this.WindowClose();
				};
			};
			this.Exit += (sender, e) => f.Dispose();
			this.Activate += (sender, e) =>
			{
				if (e.Value)
				{
					EnsureBoneObject(e.Document);

					if (bone == null)
					{
						bone = new Metasequoia.Object
						{
							Name = "bone",
							Color = new Color(1, 1, 0),
							ColorValid = true,
						};
						this.BeginCallback(_ => _.AddObject(bone));
					}

					f.Show(NativeWindow.FromHandle(Plugin.MainWindowHandle));
				}
				else
				{
					createBonePointInfo = null;
					f.Hide();
				}

				Plugin.RefreshView();
				e.Handled = e.Value;
			};
			this.IsActivated += (sender, e) => e.Handled = f.Visible;
			this.MaterialList += (sender, e) => f.OnMaterialChanged(e.Document);
			this.ObjectList += (sender, e) => f.OnObjectChanged(e.Document);
			this.Undo += (sender, e) =>
			{
				if (createBonePointInfo != null)
				{
					createBonePointInfo = null;
					e.Handled = true;
				}
			};
			this.RightButtonDown += (sender, e) =>
			{
				if (createBonePointInfo != null)
				{
					createBonePointInfo = null;
					this.RedrawAllScene();
				}
			};
			this.LeftButtonDown += (sender, e) =>
			{
				if (f.Visible)
				{
					switch (f.Mode)
					{
						case SkeletonMode.Bone:
							{
								var scr = new Point(e.X, e.Y, e.Scene.Convert3dToScreen(createBonePointInfo == null ? Point.Zero : createBonePointInfo.BeginWorld).Z);
								var hit = this.HitTest(e.Scene, new[] { e.X, e.Y }, HitType.Vertex);
								var pos = hit.HitType == HitType.Vertex
									? hit.HitPos
									: this.GetSnappedPos(e.Scene, e.Scene.ConvertScreenTo3d(ref scr), this.GetEditOption().SnapGrid);

								if (createBonePointInfo == null)
									createBonePointInfo = new CreateBonePointInfo
									{
										BeginWorld = pos,
										BeginVertexIndex = hit.HitType == HitType.Vertex && hit.ObjectIndex == bone.Index
											? hit.VertexIndex
											: -1,
									};
								else
								{
									createBonePointInfo.EndWorld = pos;
									createBonePointInfo.EndScreen = scr;
									createBonePointInfo.HasEnd = true;

									SetBoneSize(e.Scene, createBonePointInfo, new Point(e.X, e.Y, scr.Z), f.CreateRelativeBone);
								}

								this.RedrawAllScene();
								e.Handled = true;
							}

							break;
						case SkeletonMode.Anchor:
							anchorSelect = new[]
							{
								Tuple.Create(e.X, e.Y),
								Tuple.Create(e.X, e.Y),
							};
							e.Handled = true;

							break;
					}
				}
			};
			this.LeftButtonMove += (sender, e) =>
			{
				if (f.Visible)
					switch (f.Mode)
					{
						case SkeletonMode.Bone:
							if (f.CreateNormalBone &&
								createBonePointInfo != null &&
								createBonePointInfo.HasEnd)
							{
								SetBoneSize(e.Scene, createBonePointInfo, new Point(e.X, e.Y, 0), f.CreateRelativeBone);

								this.RedrawAllScene();
								e.Handled = true;
							}

							break;
						case SkeletonMode.Anchor:
							if (anchorSelect != null)
							{
								anchorSelect[1] = Tuple.Create(e.X, e.Y);
								this.RedrawScene(e.Scene);
								e.Handled = true;
							}

							break;
					}

			};
			this.LeftButtonUp += (sender, e) =>
			{
				if (f.Visible)
					switch (f.Mode)
					{
						case SkeletonMode.Bone:
							if (createBonePointInfo != null &&
								createBonePointInfo.HasEnd)
							{
								if (createBonePointInfo.HasSize || f.CreateRelativeBone)
								{
									EnsureBoneObject(e.Document);

									if (bone != null)
										this.BeginCallback(_ =>
										{
											createBonePointInfo = new CreateBonePointInfo
											{
												BeginVertexIndex = CreateBone
												(
													_,
													bone,
													createBonePointInfo,
													f.CreateNewMaterial,
													f.BoneName
												),
												BeginWorld = createBonePointInfo.EndWorld,
											};
											f.OnBoneCreated();
											this.UpdateUndo();
										});
								}
								else
									createBonePointInfo.HasEnd = false;

								this.RedrawAllScene();
								e.Handled = true;
							}

							break;
						case SkeletonMode.Anchor:
							if (anchorSelect != null)
							{
								var isDrag = Math.Abs(anchorSelect[1].Item1 - anchorSelect[0].Item1) > 4 || Math.Abs(anchorSelect[1].Item2 - anchorSelect[0].Item2) > 4;
								var isAdd = Control.ModifierKeys.HasFlag(Keys.Shift);
								var isRemove = Control.ModifierKeys.HasFlag(Keys.Control);
								
								if (!isAdd & !isRemove)
									e.Document.ClearSelect(Doc.ClearselectAll);

								if (isDrag)
								{
									var objIdx = e.Document.CurrentObjectIndex;
									var obj = e.Document.Objects[objIdx];
									var beginX = Math.Min(anchorSelect[0].Item1, anchorSelect[1].Item1);
									var beginY = Math.Min(anchorSelect[0].Item2, anchorSelect[1].Item2);
									var endX = Math.Max(anchorSelect[0].Item1, anchorSelect[1].Item1);
									var endY = Math.Max(anchorSelect[0].Item2, anchorSelect[1].Item2);

									foreach (var i in obj.Vertices)
									{
										var scr = e.Scene.Convert3dToScreen(i.Point);

										if (scr.X >= beginX && scr.X <= endX &&
											scr.Y >= beginY && scr.Y <= endY)
											i.IsSelected = !isRemove;
									}
								}
								else
								{
									var test = this.HitTest(e.Scene, new[] { anchorSelect[0].Item1, anchorSelect[0].Item2 }, HitType.Vertex | HitType.Face);

									if (test.HitType != HitType.None &&
										test.ObjectIndex == e.Document.CurrentObjectIndex)
										switch (test.HitType)
										{
											case HitType.Vertex:
												if (isRemove)
													e.Document.DeleteSelectVertex(test.ObjectIndex, test.VertexIndex);
												else
													e.Document.AddSelectVertex(test.ObjectIndex, test.VertexIndex);

												break;
											case HitType.Face:
												foreach (var i in e.Document.Objects[test.ObjectIndex].Faces[test.FaceIndex].Points)
													if (isRemove)
														e.Document.DeleteSelectVertex(test.ObjectIndex, i);
													else
														e.Document.AddSelectVertex(test.ObjectIndex, i);

												break;
										}
								}

								anchorSelect = null;
								this.RedrawAllScene();
								e.Handled = true;
							}

							break;
					}

			};
			this.Draw += (sender, e) =>
			{
				if (!f.Visible)
					return;

				switch (f.Mode)
				{
					case SkeletonMode.Bone:
						EnsureBoneObject(e.Document);

						if (bone != null)
							using (var c = this.CreateDrawingObject(e.Document, DrawObjectVisibility.Point | DrawObjectVisibility.Line))
							{
								c.Color = highlightColor;
								c.ColorValid = true;

								foreach (var i in bone.Faces)
									c.AddFace(i.Points.Select(_ => bone.Vertices[_].Point)
													  .Select(c.AddVertex));
							}

						if (createBonePointInfo != null)
							using (var c = this.CreateDrawingObject(e.Document, DrawObjectVisibility.Point | DrawObjectVisibility.Line))
							{
								var vertices = new Point?[]
								{
									createBonePointInfo.BeginWorld,
									createBonePointInfo.HasEnd ? createBonePointInfo.EndWorld : (Point?)null,
									createBonePointInfo.HasSize ? createBonePointInfo.SizeWorld : (Point?)null,
								}.Where(_ => _.HasValue).Select(_ => c.AddVertex(_.Value));

								c.Color = workingColor;
								c.ColorValid = true;
								c.AddFace(createBonePointInfo.Flip ? vertices.Reverse() : vertices);
							}

						break;
					case SkeletonMode.Anchor:
						if (anchorSelect != null)
							using (var c = this.CreateDrawingObject(e.Document, DrawObjectVisibility.Line))
							{
								var begin = anchorSelect[0];
								var end = anchorSelect[1];
								var z = e.Scene.Convert3dToScreen(Point.Zero).Z;
								var vertices = new[]
								{
									new Point(begin.Item1, begin.Item2, z),
									new Point(end.Item1, begin.Item2, z),
									new Point(end.Item1, end.Item2, z),
									new Point(begin.Item1, end.Item2, z),
								}
								.Select(_ => e.Scene.ConvertScreenTo3d(ref _))
								.Select(_ => c.AddVertex(_))
								.ToArray();

								c.Color = workingColor;
								c.ColorValid = true;
								c.AddFace(new[] { vertices[0], vertices[1], });
								c.AddFace(new[] { vertices[1], vertices[2], });
								c.AddFace(new[] { vertices[2], vertices[3], });
								c.AddFace(new[] { vertices[3], vertices[0], });
							}

						break;
				}
			};
		}

		void EnsureBoneObject(Document doc)
		{
			bone = doc.Objects.FirstOrDefault(_ => _.Name.StartsWith("bone"));
		}

		void CreateAnchor(Document doc, float margin, bool snap, bool autoMirror)
		{
			if (doc.CurrentObjectIndex == -1 ||
				doc.CurrentMaterialIndex == -1)
				return;

			var targetObject = doc.Objects[doc.CurrentObjectIndex];
			var targetBoneFace = bone == null ? null : bone.Faces.FirstOrDefault(_ => _.Material == doc.CurrentMaterialIndex);
			var targetBone = targetBoneFace == null ? null : new Bone(doc, targetBoneFace);
			var beginToEnd = targetBone == null ? Point.Zero : targetBone.End.Point - targetBone.Begin.Point;
			var unit = new Point(0, 1, 0);
			var axis = Point.GetCrossProduct(unit, beginToEnd).Normalize();
			var angle = Point.GetCrossingAngle(unit, beginToEnd);

			if (axis == Point.Zero)
				axis = new Point(0, 1, 0);

			var targetBoneMatrix = targetBone == null || !snap
				? Matrix.Identity
				: Matrix.CreateFromAxisAngle(axis, angle) * Matrix.CreateTranslation(targetBone.Begin.Point);
			var targetBoneInverseMatrix = targetBone == null || !snap
				? Matrix.Identity
				: Matrix.Invert(targetBoneMatrix);
			var anchorName = "anchor|" + targetObject.Name;

			while (doc.Objects.Any(_ => _.Name == anchorName))
				anchorName = anchorName.Replace("|", "_|");

			var vertices = targetObject.Vertices
									   .Where(_ => _.IsSelected)
									   .Select(_ => Point.Transform(_.Point, targetBoneInverseMatrix))
									   .ToArray();
			var min = vertices.Aggregate(vertices.First(), (x, y) => new Point(Math.Min(x.X, y.X), Math.Min(x.Y, y.Y), Math.Min(x.Z, y.Z))) - margin;
			var max = vertices.Aggregate(vertices.First(), (x, y) => new Point(Math.Max(x.X, y.X), Math.Max(x.Y, y.Y), Math.Max(x.Z, y.Z))) + margin;
			var obj = new Metasequoia.Object
			{
				Name = anchorName,
			};
			var newVertices = new[]
			{
				new Point(min.X, max.Y, max.Z),
				new Point(min.X, min.Y, max.Z),
				new Point(max.X, max.Y, max.Z),
				new Point(max.X, min.Y, max.Z),
				new Point(max.X, max.Y, min.Z),
				new Point(max.X, min.Y, min.Z),
				new Point(min.X, max.Y, min.Z),
				new Point(min.X, min.Y, min.Z),
			}.Select(_ => Point.Transform(_, targetBoneMatrix)).Select(obj.AddVertex).ToArray();

			foreach (var i in new[]
			{
				new[] { 0, 2, 3, 1 },
				new[] { 2, 4, 5, 3 },
				new[] { 4, 6, 7, 5 },
				new[] { 6, 0, 1, 7 },
				new[] { 6, 4, 2, 0 },
				new[] { 1, 3, 5, 7 },
			})
				obj.Faces[obj.AddFace(i.Select(_ => newVertices[_]))].Material = doc.CurrentMaterialIndex;

			if (autoMirror && doc.Materials[doc.CurrentMaterialIndex].Name.EndsWith("[]"))
			{
				obj.MirrorType = ObjectMirror.Normal;
				obj.MirrorAxis = ObjectMirrorAxis.X;
			}

			doc.AddObject(obj);
		}

		static void SetBoneSize(Scene scene, CreateBonePointInfo pointInfo, Point currentScreen, bool isRelativeBone)
		{
			if (isRelativeBone)
				pointInfo.HasSize = false;
			else
			{
				var beginScreen = scene.Convert3dToScreen(pointInfo.BeginWorld);
				var endScreen = pointInfo.EndScreen;
				var currentToEndScreenSize = (endScreen - currentScreen).GetSizeSquared();
				var beginToEndScreen = endScreen - beginScreen;
				var size = (float)Math.Sqrt(Math.Min
				(
					Math.Max(currentToEndScreenSize, MinimalBoneSize * MinimalBoneSize),
					beginToEndScreen.GetSizeSquared()
				));
				var flip = Point.GetInnerProduct(new Point(-beginToEndScreen.Y, beginToEndScreen.X, 0), currentScreen - beginScreen) < 1;
				var rad = Math.Atan2(beginToEndScreen.Y, beginToEndScreen.X) + MathHelper.PiOver2 * (flip ? -1 : 1);
				var scr = new Point(beginScreen.X + (float)Math.Cos(rad) * size, beginScreen.Y + (float)Math.Sin(rad) * size, beginScreen.Z);

				pointInfo.Flip = flip;
				pointInfo.SizeWorld = scene.ConvertScreenTo3d(ref scr);
				pointInfo.HasSize = true;
			}
		}

		static int CreateBone(Document doc, Metasequoia.Object obj, CreateBonePointInfo info, bool newMaterial, string boneName)
		{
			var vertices = new[]
			{
				info.BeginVertexIndex != -1
					? info.BeginVertexIndex
					: obj.AddVertex(info.BeginWorld)
			}.Concat(new[]
			{
				info.EndWorld,
				info.HasSize ? info.SizeWorld : (Point?)null,
			}
			.Where(_ => _.HasValue)
			.Select(_ => obj.AddVertex(_.Value)))
			.ToArray();

			if (vertices.Length == 3 && info.Flip)
				vertices = vertices.Reverse().ToArray();

			var face = obj.AddFace(vertices);

			if (info.HasSize && newMaterial)
			{
				var r = new Random();
				var mat = doc.Materials.Where(_ => _.Name == boneName)
									   .Select(_ => (int?)_.Index)
									   .FirstOrDefault()
					?? doc.AddMaterial(new Material
					{
						Name = boneName,
						Alpha = (float)(0.5 + r.NextDouble() * 0.4),
						Color = new Color((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble()),
					});

				obj.SetFaceMaterial(face, mat);
				doc.CurrentMaterialIndex = mat;
			}
			else
				obj.SetFaceMaterial(face, doc.CurrentMaterialIndex);

			foreach (var i in vertices)
				doc.AddSelectVertex(obj.Index, i);

			for (int i = 0; i < (vertices.Length == 2 ? 1 : vertices.Length); i++)
				doc.AddSelectLine(obj.Index, face, i);

			doc.AddSelectFace(obj.Index, face);

			return vertices[1];
		}

		class CreateBonePointInfo
		{
			public int BeginVertexIndex
			{
				get;
				set;
			}

			public Point BeginWorld
			{
				get;
				set;
			}

			public Point EndWorld
			{
				get;
				set;
			}

			public Point EndScreen
			{
				get;
				set;
			}

			public Point SizeWorld
			{
				get;
				set;
			}

			public bool HasEnd
			{
				get;
				set;
			}

			public bool HasSize
			{
				get;
				set;
			}

			public bool Flip
			{
				get;
				set;
			}

			public CreateBonePointInfo()
			{
				this.BeginVertexIndex = -1;
			}
		}
	}
}