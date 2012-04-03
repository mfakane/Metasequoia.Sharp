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
