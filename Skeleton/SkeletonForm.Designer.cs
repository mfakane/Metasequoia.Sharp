namespace Linearstar.Metaseq.Skeleton
{
	partial class SkeletonForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.boneImageList = new System.Windows.Forms.ImageList(this.components);
			this.mainTabControl = new System.Windows.Forms.TabControl();
			this.boneTabPage = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.boneNameLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.normalBoneRadioButton = new System.Windows.Forms.RadioButton();
			this.relativeBoneRadioButton = new System.Windows.Forms.RadioButton();
			this.boneNameTextBox = new System.Windows.Forms.TextBox();
			this.createNewMaterialCheckBox = new System.Windows.Forms.CheckBox();
			this.anchorTabPage = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.createSymmetricalAnchorCheckBox = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.anchorTargetLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.snapAnchorToParentCheckBox = new System.Windows.Forms.CheckBox();
			this.anchorBoneNameLabel = new System.Windows.Forms.Label();
			this.anchorBoneNameValueLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.createAnchorButton = new System.Windows.Forms.Button();
			this.anchorMarginNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.mainTabControl.SuspendLayout();
			this.boneTabPage.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.anchorTabPage.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.anchorMarginNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// boneImageList
			// 
			this.boneImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.boneImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.boneImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// mainTabControl
			// 
			this.mainTabControl.Controls.Add(this.boneTabPage);
			this.mainTabControl.Controls.Add(this.anchorTabPage);
			this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabControl.Location = new System.Drawing.Point(0, 0);
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.SelectedIndex = 0;
			this.mainTabControl.Size = new System.Drawing.Size(193, 194);
			this.mainTabControl.TabIndex = 0;
			this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
			// 
			// boneTabPage
			// 
			this.boneTabPage.Controls.Add(this.tableLayoutPanel1);
			this.boneTabPage.Location = new System.Drawing.Point(4, 22);
			this.boneTabPage.Name = "boneTabPage";
			this.boneTabPage.Padding = new System.Windows.Forms.Padding(8);
			this.boneTabPage.Size = new System.Drawing.Size(185, 168);
			this.boneTabPage.TabIndex = 0;
			this.boneTabPage.Text = "ボーン";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.boneNameLabel, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.boneNameTextBox, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.createNewMaterialCheckBox, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 8);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(169, 152);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// boneNameLabel
			// 
			this.boneNameLabel.AutoSize = true;
			this.boneNameLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boneNameLabel.Location = new System.Drawing.Point(16, 51);
			this.boneNameLabel.Margin = new System.Windows.Forms.Padding(16, 3, 0, 0);
			this.boneNameLabel.Name = "boneNameLabel";
			this.boneNameLabel.Size = new System.Drawing.Size(48, 12);
			this.boneNameLabel.TabIndex = 2;
			this.boneNameLabel.Text = "ボーン名:";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.normalBoneRadioButton, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.relativeBoneRadioButton, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(169, 23);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// normalBoneRadioButton
			// 
			this.normalBoneRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.normalBoneRadioButton.AutoSize = true;
			this.normalBoneRadioButton.Checked = true;
			this.normalBoneRadioButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.normalBoneRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.normalBoneRadioButton.Location = new System.Drawing.Point(0, 0);
			this.normalBoneRadioButton.Margin = new System.Windows.Forms.Padding(0);
			this.normalBoneRadioButton.MinimumSize = new System.Drawing.Size(0, 23);
			this.normalBoneRadioButton.Name = "normalBoneRadioButton";
			this.normalBoneRadioButton.Size = new System.Drawing.Size(84, 23);
			this.normalBoneRadioButton.TabIndex = 0;
			this.normalBoneRadioButton.TabStop = true;
			this.normalBoneRadioButton.Text = "通常";
			this.normalBoneRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.normalBoneRadioButton.UseVisualStyleBackColor = true;
			// 
			// relativeBoneRadioButton
			// 
			this.relativeBoneRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.relativeBoneRadioButton.AutoSize = true;
			this.relativeBoneRadioButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.relativeBoneRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.relativeBoneRadioButton.Location = new System.Drawing.Point(84, 0);
			this.relativeBoneRadioButton.Margin = new System.Windows.Forms.Padding(0);
			this.relativeBoneRadioButton.MinimumSize = new System.Drawing.Size(0, 23);
			this.relativeBoneRadioButton.Name = "relativeBoneRadioButton";
			this.relativeBoneRadioButton.Size = new System.Drawing.Size(85, 23);
			this.relativeBoneRadioButton.TabIndex = 1;
			this.relativeBoneRadioButton.Text = "浮動";
			this.relativeBoneRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.relativeBoneRadioButton.UseVisualStyleBackColor = true;
			// 
			// boneNameTextBox
			// 
			this.boneNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.boneNameTextBox.Location = new System.Drawing.Point(64, 48);
			this.boneNameTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
			this.boneNameTextBox.Name = "boneNameTextBox";
			this.boneNameTextBox.Size = new System.Drawing.Size(105, 19);
			this.boneNameTextBox.TabIndex = 3;
			this.boneNameTextBox.Text = "センター";
			// 
			// createNewMaterialCheckBox
			// 
			this.createNewMaterialCheckBox.AutoSize = true;
			this.createNewMaterialCheckBox.Checked = true;
			this.createNewMaterialCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel1.SetColumnSpan(this.createNewMaterialCheckBox, 2);
			this.createNewMaterialCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.createNewMaterialCheckBox.Location = new System.Drawing.Point(0, 31);
			this.createNewMaterialCheckBox.Margin = new System.Windows.Forms.Padding(0);
			this.createNewMaterialCheckBox.Name = "createNewMaterialCheckBox";
			this.createNewMaterialCheckBox.Size = new System.Drawing.Size(118, 17);
			this.createNewMaterialCheckBox.TabIndex = 1;
			this.createNewMaterialCheckBox.Text = "新しい材質を作成";
			this.createNewMaterialCheckBox.UseVisualStyleBackColor = true;
			this.createNewMaterialCheckBox.CheckedChanged += new System.EventHandler(this.createNewMaterialCheckBox_CheckedChanged);
			// 
			// anchorTabPage
			// 
			this.anchorTabPage.Controls.Add(this.tableLayoutPanel3);
			this.anchorTabPage.Location = new System.Drawing.Point(4, 22);
			this.anchorTabPage.Name = "anchorTabPage";
			this.anchorTabPage.Padding = new System.Windows.Forms.Padding(8);
			this.anchorTabPage.Size = new System.Drawing.Size(167, 168);
			this.anchorTabPage.TabIndex = 3;
			this.anchorTabPage.Text = "アンカー";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.createSymmetricalAnchorCheckBox, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.anchorTargetLabel, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.snapAnchorToParentCheckBox, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.anchorBoneNameLabel, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.anchorBoneNameValueLabel, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 5);
			this.tableLayoutPanel3.Controls.Add(this.anchorMarginNumericUpDown, 1, 2);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(8, 8);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 6;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(151, 152);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// createSymmetricalAnchorCheckBox
			// 
			this.createSymmetricalAnchorCheckBox.AutoSize = true;
			this.createSymmetricalAnchorCheckBox.Checked = true;
			this.createSymmetricalAnchorCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel3.SetColumnSpan(this.createSymmetricalAnchorCheckBox, 2);
			this.createSymmetricalAnchorCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.createSymmetricalAnchorCheckBox.Location = new System.Drawing.Point(0, 77);
			this.createSymmetricalAnchorCheckBox.Margin = new System.Windows.Forms.Padding(0);
			this.createSymmetricalAnchorCheckBox.Name = "createSymmetricalAnchorCheckBox";
			this.createSymmetricalAnchorCheckBox.Size = new System.Drawing.Size(149, 17);
			this.createSymmetricalAnchorCheckBox.TabIndex = 7;
			this.createSymmetricalAnchorCheckBox.Text = "対称アンカーの自動作成";
			this.createSymmetricalAnchorCheckBox.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(0, 36);
			this.label2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "マージン:";
			// 
			// anchorTargetLabel
			// 
			this.anchorTargetLabel.AutoSize = true;
			this.anchorTargetLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.anchorTargetLabel.Location = new System.Drawing.Point(60, 18);
			this.anchorTargetLabel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.anchorTargetLabel.Name = "anchorTargetLabel";
			this.anchorTargetLabel.Size = new System.Drawing.Size(32, 12);
			this.anchorTargetLabel.TabIndex = 3;
			this.anchorTargetLabel.Text = "(なし)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(0, 18);
			this.label1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "ターゲット:";
			// 
			// snapAnchorToParentCheckBox
			// 
			this.snapAnchorToParentCheckBox.AutoSize = true;
			this.snapAnchorToParentCheckBox.Checked = true;
			this.snapAnchorToParentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel3.SetColumnSpan(this.snapAnchorToParentCheckBox, 2);
			this.snapAnchorToParentCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.snapAnchorToParentCheckBox.Location = new System.Drawing.Point(0, 60);
			this.snapAnchorToParentCheckBox.Margin = new System.Windows.Forms.Padding(0);
			this.snapAnchorToParentCheckBox.Name = "snapAnchorToParentCheckBox";
			this.snapAnchorToParentCheckBox.Size = new System.Drawing.Size(109, 17);
			this.snapAnchorToParentCheckBox.TabIndex = 6;
			this.snapAnchorToParentCheckBox.Text = "ボーンの軸に沿う";
			this.snapAnchorToParentCheckBox.UseVisualStyleBackColor = true;
			// 
			// anchorBoneNameLabel
			// 
			this.anchorBoneNameLabel.AutoSize = true;
			this.anchorBoneNameLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.anchorBoneNameLabel.Location = new System.Drawing.Point(0, 3);
			this.anchorBoneNameLabel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.anchorBoneNameLabel.Name = "anchorBoneNameLabel";
			this.anchorBoneNameLabel.Size = new System.Drawing.Size(60, 12);
			this.anchorBoneNameLabel.TabIndex = 0;
			this.anchorBoneNameLabel.Text = "関連ボーン:";
			// 
			// anchorBoneNameValueLabel
			// 
			this.anchorBoneNameValueLabel.AutoSize = true;
			this.anchorBoneNameValueLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.anchorBoneNameValueLabel.Location = new System.Drawing.Point(60, 3);
			this.anchorBoneNameValueLabel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.anchorBoneNameValueLabel.Name = "anchorBoneNameValueLabel";
			this.anchorBoneNameValueLabel.Size = new System.Drawing.Size(32, 12);
			this.anchorBoneNameValueLabel.TabIndex = 1;
			this.anchorBoneNameValueLabel.Text = "(なし)";
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.AutoSize = true;
			this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel4.ColumnCount = 3;
			this.tableLayoutPanel3.SetColumnSpan(this.tableLayoutPanel4, 2);
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.Controls.Add(this.createAnchorButton, 1, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 129);
			this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(151, 23);
			this.tableLayoutPanel4.TabIndex = 5;
			// 
			// createAnchorButton
			// 
			this.createAnchorButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.createAnchorButton.Location = new System.Drawing.Point(38, 0);
			this.createAnchorButton.Margin = new System.Windows.Forms.Padding(0);
			this.createAnchorButton.Name = "createAnchorButton";
			this.createAnchorButton.Size = new System.Drawing.Size(75, 23);
			this.createAnchorButton.TabIndex = 0;
			this.createAnchorButton.Text = "作成";
			this.createAnchorButton.UseVisualStyleBackColor = true;
			this.createAnchorButton.Click += new System.EventHandler(this.createAnchorButton_Click);
			// 
			// anchorMarginNumericUpDown
			// 
			this.anchorMarginNumericUpDown.DecimalPlaces = 2;
			this.anchorMarginNumericUpDown.Location = new System.Drawing.Point(60, 33);
			this.anchorMarginNumericUpDown.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
			this.anchorMarginNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
			this.anchorMarginNumericUpDown.Name = "anchorMarginNumericUpDown";
			this.anchorMarginNumericUpDown.Size = new System.Drawing.Size(81, 19);
			this.anchorMarginNumericUpDown.TabIndex = 5;
			this.anchorMarginNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			// 
			// SkeletonForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(193, 194);
			this.Controls.Add(this.mainTabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SkeletonForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "スケルトン";
			this.mainTabControl.ResumeLayout(false);
			this.boneTabPage.ResumeLayout(false);
			this.boneTabPage.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.anchorTabPage.ResumeLayout(false);
			this.anchorTabPage.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.anchorMarginNumericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl mainTabControl;
		private System.Windows.Forms.TabPage boneTabPage;
		private System.Windows.Forms.ImageList boneImageList;
		private System.Windows.Forms.TabPage anchorTabPage;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.RadioButton normalBoneRadioButton;
		private System.Windows.Forms.RadioButton relativeBoneRadioButton;
		private System.Windows.Forms.Label boneNameLabel;
		private System.Windows.Forms.TextBox boneNameTextBox;
		private System.Windows.Forms.CheckBox createNewMaterialCheckBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label anchorBoneNameLabel;
		private System.Windows.Forms.Label anchorBoneNameValueLabel;
		private System.Windows.Forms.CheckBox snapAnchorToParentCheckBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Button createAnchorButton;
		private System.Windows.Forms.Label anchorTargetLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown anchorMarginNumericUpDown;
		private System.Windows.Forms.CheckBox createSymmetricalAnchorCheckBox;
	}
}