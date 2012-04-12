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

		public override string ToString()
		{
			return string.Format("{{H:{0} P:{1} B:{2}}}", this.Head, this.Pitch, this.Bank);
		}
	}
}
