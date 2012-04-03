namespace Metasequoia
{
	partial struct Color
	{
		public Color(float r, float g, float b)
		{
			this.R = r;
			this.G = g;
			this.B = b;
		}

		public static Color operator +(Color self)
		{
			return self;
		}

		public static Color operator -(Color self)
		{
			return new Color(-self.R, -self.G, -self.B);
		}

		public static Color operator +(Color a, Color b)
		{
			return new Color(a.R + b.R, a.G + b.G, a.B + b.B);
		}

		public static Color operator -(Color a, Color b)
		{
			return new Color(a.R - b.R, a.G - b.G, a.B - b.B);
		}

		public static Color operator +(Color a, float b)
		{
			return new Color(a.R + b, a.G + b, a.B + b);
		}

		public static Color operator -(Color a, float b)
		{
			return new Color(a.R - b, a.G - b, a.B - b);
		}

		public static Color operator -(float a, Color b)
		{
			return new Color(a - b.R, a - b.R, a - b.B);
		}

		public static Color operator *(Color a, float b)
		{
			return new Color(a.R * b, a.G * b, a.B * b);
		}

		public static Color operator /(Color a, float b)
		{
			return new Color(a.R / b, a.G / b, a.B / b);
		}

		public static bool operator ==(Color a, Color b)
		{
			return a.R == b.R
				&& a.G == b.G
				&& a.B == b.B;
		}

		public static bool operator !=(Color a, Color b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			return obj is Color
				&& (Color)obj == this;
		}

		public override int GetHashCode()
		{
			return typeof(Color).GetHashCode() ^ this.R.GetHashCode() ^ this.G.GetHashCode() ^ this.B.GetHashCode();
		}
	}
}
