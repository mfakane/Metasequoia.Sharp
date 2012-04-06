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
	}
}
