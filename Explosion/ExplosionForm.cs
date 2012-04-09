using System;
using System.Drawing;
using System.Windows.Forms;

namespace Linearstar.Metaseq.Explosion
{
	public partial class ExplosionForm : Form
	{
		public Tuple<float, float> Power
		{
			get
			{
				return Tuple.Create((float)powerBeginNumericUpDown.Value, (float)powerEndNumericUpDown.Value);
			}
			set
			{
				powerBeginNumericUpDown.Value = (decimal)value.Item1;
				powerEndNumericUpDown.Value = (decimal)value.Item2;
			}
		}

		public Tuple<float, float> Scaling
		{
			get
			{
				return Tuple.Create((float)scaleBeginNumericUpDown.Value, (float)scaleEndNumericUpDown.Value);
			}
			set
			{
				scaleBeginNumericUpDown.Value = (decimal)value.Item1;
				scaleEndNumericUpDown.Value = (decimal)value.Item2;
			}
		}

		public Tuple<float, float> Rotation
		{
			get
			{
				return Tuple.Create((float)rotateBeginNumericUpDown.Value, (float)rotateEndNumericUpDown.Value);
			}
			set
			{
				rotateBeginNumericUpDown.Value = (decimal)value.Item1;
				rotateEndNumericUpDown.Value = (decimal)value.Item2;
			}
		}

		public int Seed
		{
			get
			{
				return (int)seedNumericUpDown.Value;
			}
			set
			{
				seedNumericUpDown.Value = value;
			}
		}

		public ExplosionForm()
		{
			InitializeComponent();
			this.Font = SystemFonts.MessageBoxFont;
		}

		void powerBeginNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			powerEndNumericUpDown.Minimum = powerBeginNumericUpDown.Value;
		}

		void scaleBeginNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			scaleEndNumericUpDown.Minimum = scaleBeginNumericUpDown.Value;
		}

		void rotateBeginNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			rotateEndNumericUpDown.Minimum = rotateBeginNumericUpDown.Value;
		}

		void powerBeginNumericUpDown_Enter(object sender, EventArgs e)
		{
			((NumericUpDown)sender).Select(0, ((NumericUpDown)sender).Value.ToString("0.00").Length);
		}

		void seedNumericUpDown_Enter(object sender, EventArgs e)
		{
			((NumericUpDown)sender).Select(0, ((NumericUpDown)sender).Value.ToString().Length);
		}
	}
}
