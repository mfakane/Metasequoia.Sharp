using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Metasequoia;

namespace Linearstar.Metaseq.Skeleton
{
	partial class SkeletonForm : Form
	{
		public event EventHandler CreateAnchor;
		public event EventHandler ModeChanged;

		static Dictionary<string, string> nextBoneNames = new Dictionary<string, string>
		{
			{ "センター", "上半身" },
			{ "上半身", "上半身2" },
			{ "上半身2", "首" },
			{ "首", "頭" },
			{ "頭", "肩[]" },
			{ "肩[]", "腕[]" },
			{ "腕[]", "腕捩[]" },
			{ "腕捩[]", "ひじ[]" },
			{ "ひじ[]", "手捩[]" },
			{ "手捩[]", "手首[]" },
			{ "手首[]", "人指１[]" },
			{ "人指１[]", "人指２[]" },
			{ "人指２[]", "人指３[]" },
			{ "人指３[]", "中指１[]" },
			{ "中指２[]", "中指３[]" },
			{ "中指３[]", "薬指１[]" },
			{ "薬指１[]", "薬指２[]" },
			{ "薬指２[]", "薬指３[]" },
			{ "薬指３[]", "小指１[]" },
			{ "小指１[]", "小指２[]" },
			{ "小指２[]", "親指１[]" },
			{ "親指１[]", "親指２[]" },
			{ "下半身", "足[]" },
			{ "足[]", "ひざ[]" },
			{ "ひざ[]", "足首[]" },
			{ "足首[]", "足ＩＫ[]" },
			{ "足ＩＫ[]", "つま先ＩＫ[]" },
		};

		public bool CreateNormalBone
		{
			get
			{
				return normalBoneRadioButton.Checked;
			}
		}

		public bool CreateRelativeBone
		{
			get
			{
				return relativeBoneRadioButton.Checked;
			}
		}

		public bool CreateNewMaterial
		{
			get
			{
				return createNewMaterialCheckBox.Checked;
			}
		}

		public bool CreateSymmetricalAnchor
		{
			get
			{
				return createSymmetricalAnchorCheckBox.Checked;
			}
		}

		public bool SnapAnchorToParent
		{
			get
			{
				return snapAnchorToParentCheckBox.Checked;
			}
		}

		public string BoneName
		{
			get
			{
				return boneNameTextBox.Text;
			}
			set
			{
				boneNameTextBox.Text = value;
			}
		}

		public float AnchorMargin
		{
			get
			{
				return (float)anchorMarginNumericUpDown.Value;
			}
		}

		public SkeletonMode Mode
		{
			get
			{
				return (SkeletonMode)mainTabControl.SelectedIndex;
			}
		}

		public SkeletonForm()
		{
			InitializeComponent();
			this.Font = SystemFonts.MessageBoxFont;
		}

		public void OnBoneCreated()
		{
			if (this.CreateNormalBone)
				if (nextBoneNames.ContainsKey(this.BoneName))
					this.BoneName = nextBoneNames[this.BoneName];
				else
				{
					var numericSuffixMatch = Regex.Match(this.BoneName, "(.*)([0-9]+)$", RegexOptions.Compiled);

					if (numericSuffixMatch.Success)
						this.BoneName = numericSuffixMatch.Groups[1].Value + (int.Parse(numericSuffixMatch.Groups[2].Value) + 1);
					else
						this.BoneName += "1";
				}
		}

		public void OnMaterialChanged(Document doc)
		{
			anchorBoneNameValueLabel.Text = doc.CurrentMaterialIndex == -1
				? "(なし)"
				: doc.Materials[doc.CurrentMaterialIndex].Name;
		}

		public void OnObjectChanged(Document doc)
		{
			anchorTargetLabel.Text = doc.CurrentObjectIndex == -1
				? "(なし)"
				: doc.Objects[doc.CurrentObjectIndex].Name;
		}

		void createNewMaterialCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			boneNameLabel.Enabled =
				boneNameTextBox.Enabled = createNewMaterialCheckBox.Checked;
		}

		void createAnchorButton_Click(object sender, EventArgs e)
		{
			if (CreateAnchor != null)
				CreateAnchor(this, e);
		}

		void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ModeChanged != null)
				ModeChanged(this, e);
		}
	}
}
