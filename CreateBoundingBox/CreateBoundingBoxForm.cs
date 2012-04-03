using System;
using System.Drawing;
using System.Windows.Forms;

namespace Linearstar.Metaseq.CreateBoundingBox
{
	public partial class CreateBoundingBoxForm : Form
	{
		public bool CreateAtNewObject
		{
			get
			{
				return newObjectRadioButton.Checked;
			}
			set
			{
				newObjectRadioButton.Checked = value;
			}
		}

		public bool CreateAtCurrentObject
		{
			get
			{
				return currentObjectRadioButton.Checked;
			}
			set
			{
				currentObjectRadioButton.Checked = value;
			}
		}

		public float CreateMargin
		{
			get
			{
				return (float)marginNumericUpDown.Value;
			}
			set
			{
				marginNumericUpDown.Value = (decimal)value;
			}
		}

		public CreateBoundingBoxForm()
		{
			InitializeComponent();
			this.Font = SystemFonts.MessageBoxFont;
		}

		void marginNumericUpDown_Enter(object sender, EventArgs e)
		{
			marginNumericUpDown.Select(0, marginNumericUpDown.Value.ToString("0.00").Length);
		}
	}
}
