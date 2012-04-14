namespace Metasequoia
{
	partial struct Angle
	{
		public static readonly Angle Zero = new Angle();

		public Angle(float head, float pitch, float bank)
		{
			this.Head = head;
			this.Pitch = pitch;
			this.Bank = bank;
		}

		public static Angle operator +(Angle a, Angle b)
		{
			return new Angle(a.Head + b.Head, a.Pitch + b.Bank, a.Bank + b.Bank);
		}

		public static Angle operator -(Angle a, Angle b)
		{
			return new Angle(a.Head - b.Head, a.Pitch - b.Bank, a.Bank - b.Bank);
		}

		public static Angle operator *(Angle a, float b)
		{
			return new Angle(a.Head * b, a.Pitch * b, a.Bank * b);
		}

		public static Angle operator /(Angle a, float b)
		{
			return new Angle(a.Head / b, a.Pitch / b, a.Bank / b);
		}

		public static Angle operator -(Angle self)
		{
			return new Angle(-self.Head, -self.Pitch, -self.Bank);
		}

		public override string ToString()
		{
			return string.Format("{{H:{0} P:{1} B:{2}}}", this.Head, this.Pitch, this.Bank);
		}
	}
}
