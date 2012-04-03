using System;

namespace Metasequoia
{
	partial struct Point
	{
		public static Point Zero = new Point();
		public static Point One = new Point(1, 1, 1);

		public Point(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>
		/// ベクトルのサイズの ２ 乗
		/// float GetNorm(const MQPoint&amp; p)
		/// </summary>
		/// <returns>ベクトルのサイズの ２ 乗</returns>
		public float GetSizeSquared()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
		}

		/// <summary>
		/// ベクトルのサイズ
		/// float GetSize(const MQPoint&amp; p)
		/// </summary>
		/// <returns>ベクトルのサイズ</returns>
		public float GetSize()
		{
			return (float)Math.Sqrt(GetSizeSquared());
		}

		/// <summary>
		/// ベクトルの正規化
		/// MQPoint Normalize(const MQPoint&amp; p)
		/// </summary>
		/// <returns>ベクトルの正規化</returns>
		public Point Normalize()
		{
			var s = GetSize();

			return s == 0 ? Zero : this / s;
		}

		/// <summary>
		/// 3 点からなる面の法線を得る
		/// MQPoint GetNormal(const MQPoint&amp; p0, const MQPoint&amp; p1, const MQPoint&amp; p2)
		/// </summary>
		/// <param name="p0">点 1</param>
		/// <param name="p1">点 2</param>
		/// <param name="p2">点 3</param>
		/// <returns>面の法線</returns>
		public static Point GetNormal(Point p0, Point p1, Point p2)
		{
			var ep = GetCrossProduct(p1 - p2, p0 - p1);

			return ep == Zero
				? Zero
				: ep / ep.GetSize();
		}

		/// <summary>
		/// 4 点からなる面の法線を得る
		/// MQPoint GetQuadNormal(const MQPoint&amp; p0, const MQPoint&amp; p1, const MQPoint&amp; p2, const MQPoint&amp; p3)
		/// </summary>
		/// <param name="p0">点 1</param>
		/// <param name="p1">点 2</param>
		/// <param name="p2">点 3</param>
		/// <param name="p3">点 4</param>
		/// <returns>面の法線</returns>
		public static Point GetNormal(Point p0, Point p1, Point p2, Point p3)
		{
			var n1a = GetNormal(p0, p1, p2);
			var n1b = GetNormal(p0, p2, p3);
			var n2a = GetNormal(p1, p2, p3);
			var n2b = GetNormal(p1, p3, p0);

			return GetInnerProduct(n1a, n1b) > GetInnerProduct(n2a, n2b)
				? (n1a + n1b).Normalize()
				: (n2a + n2b).Normalize();
		}

		/// <summary>
		/// 2 ベクトルの交差する角度をラジアン単位の 0 から π までの値で得る
		/// float GetCrossingAngle(const MQPoint&amp; v1, const MQPoint&amp; v2)
		/// </summary>
		/// <returns>ベクトルの交差する角度</returns>
		public static float GetCrossingAngle(Point v1, Point v2)
		{
			var d = v1.GetSize() * v2.GetSize();

			if (d == 0)
				return 0;

			var c = GetInnerProduct(v1, v2) / d;

			if (c >= 1)
				return 0;
			else if (c <= -1)
				return (float)Math.PI;

			return (float)Math.Acos(c);
		}

		/// <summary>
		/// 内積の値を得る
		/// float GetInnerProduct(const MQPoint&amp; pa, const MQPoint&amp; pb)
		/// </summary>
		/// <param name="pa">ベクトル 1</param>
		/// <param name="pb">ベクトル 2</param>
		/// <returns>内積の値</returns>
		public static float GetInnerProduct(Point pa, Point pb)
		{
			return pa.X * pb.X + pa.Y * pb.Y + pa.Z * pb.Z;
		}

		/// <summary>
		/// 外積ベクトルを得る
		/// MQPoint GetCrossProduct(const MQPoint&amp; v1, const MQPoint&amp; v2)
		/// </summary>
		/// <param name="v1">ベクトル 1</param>
		/// <param name="v2">ベクトル 2</param>
		/// <returns>外積ベクトル</returns>
		public static Point GetCrossProduct(Point v1, Point v2)
		{
			return new Point
			(
				v1.Y * v2.Z - v1.Z * v2.Y,
				v1.Z * v2.X - v1.X * v2.Z,
				v1.X * v2.Y - v1.Y * v2.X
			);
		}

		public static Point operator +(Point self)
		{
			return self;
		}

		public static Point operator -(Point self)
		{
			return new Point(-self.X, -self.Y, -self.Z);
		}

		public static Point operator +(Point a, Point b)
		{
			return new Point(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Point operator -(Point a, Point b)
		{
			return new Point(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static Point operator +(Point a, float b)
		{
			return new Point(a.X + b, a.Y + b, a.Z + b);
		}

		public static Point operator -(Point a, float b)
		{
			return new Point(a.X - b, a.Y - b, a.Z - b);
		}

		public static Point operator -(float a, Point b)
		{
			return new Point(a - b.X, a - b.X, a - b.Z);
		}

		public static Point operator *(Point a, float b)
		{
			return new Point(a.X * b, a.Y * b, a.Z * b);
		}

		public static Point operator /(Point a, float b)
		{
			return new Point(a.X / b, a.Y / b, a.Z / b);
		}

		public static bool operator ==(Point a, Point b)
		{
			return a.X == b.X
				&& a.Y == b.Y
				&& a.Z == b.Z;
		}

		public static bool operator !=(Point a, Point b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			return obj is Point
				&& (Point)obj == this;
		}

		public override int GetHashCode()
		{
			return typeof(Point).GetHashCode() ^ this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
		}
	}
}
