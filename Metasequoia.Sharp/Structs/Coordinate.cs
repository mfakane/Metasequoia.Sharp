namespace Metasequoia
{
	partial struct Coordinate
	{
		public static Coordinate Zero = new Coordinate();
		public static Coordinate One = new Coordinate(1, 1);

		public Coordinate(float u, float v)
		{
			this.U = u;
			this.V = v;
		}

		public static Coordinate operator +(Coordinate self)
		{
			return self;
		}

		public static Coordinate operator -(Coordinate self)
		{
			return new Coordinate(-self.U, -self.V);
		}

		public static Coordinate operator +(Coordinate a, Coordinate b)
		{
			return new Coordinate(a.U + b.U, a.V + b.V);
		}

		public static Coordinate operator -(Coordinate a, Coordinate b)
		{
			return new Coordinate(a.U - b.U, a.V - b.V);
		}

		public static Coordinate operator +(Coordinate a, float b)
		{
			return new Coordinate(a.U + b, a.V + b);
		}

		public static Coordinate operator -(Coordinate a, float b)
		{
			return new Coordinate(a.U - b, a.V - b);
		}

		public static Coordinate operator -(float a, Coordinate b)
		{
			return new Coordinate(a - b.U, a - b.U);
		}

		public static Coordinate operator *(Coordinate a, float b)
		{
			return new Coordinate(a.U * b, a.V * b);
		}

		public static Coordinate operator /(Coordinate a, float b)
		{
			return new Coordinate(a.U / b, a.V / b);
		}

		public static bool operator ==(Coordinate a, Coordinate b)
		{
			return a.U == b.U
				&& a.V == b.V;
		}

		public static bool operator !=(Coordinate a, Coordinate b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			return obj is Coordinate
				&& (Coordinate)obj == this;
		}

		public override int GetHashCode()
		{
			return typeof(Coordinate).GetHashCode() ^ this.U.GetHashCode() ^ this.V.GetHashCode();
		}
	}
}
