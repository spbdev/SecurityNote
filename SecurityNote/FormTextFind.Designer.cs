/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/7/2
 * 时间: 17:12
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace Com.Spbdev.SecurityNote
{
	partial class FormTextFind
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtKey;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.CheckBox cbxCaseSensitive;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdoUp;
		private System.Windows.Forms.RadioButton rdoDown;
		private System.Windows.Forms.Button btnCancel;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtKey = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.cbxCaseSensitive = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdoDown = new System.Windows.Forms.RadioButton();
			this.rdoUp = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "查找内容：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtKey
			// 
			this.txtKey.Location = new System.Drawing.Point(105, 15);
			this.txtKey.Name = "txtKey";
			this.txtKey.Size = new System.Drawing.Size(220, 21);
			this.txtKey.TabIndex = 1;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(338, 13);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(104, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "查找下一个";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
			// 
			// cbxCaseSensitive
			// 
			this.cbxCaseSensitive.Location = new System.Drawing.Point(13, 60);
			this.cbxCaseSensitive.Name = "cbxCaseSensitive";
			this.cbxCaseSensitive.Size = new System.Drawing.Size(104, 24);
			this.cbxCaseSensitive.TabIndex = 3;
			this.cbxCaseSensitive.Text = "区分大小写";
			this.cbxCaseSensitive.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdoDown);
			this.groupBox1.Controls.Add(this.rdoUp);
			this.groupBox1.Location = new System.Drawing.Point(124, 42);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 51);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "方向";
			// 
			// rdoDown
			// 
			this.rdoDown.Checked = true;
			this.rdoDown.Location = new System.Drawing.Point(119, 17);
			this.rdoDown.Name = "rdoDown";
			this.rdoDown.Size = new System.Drawing.Size(75, 24);
			this.rdoDown.TabIndex = 1;
			this.rdoDown.TabStop = true;
			this.rdoDown.Text = "向下";
			this.rdoDown.UseVisualStyleBackColor = true;
			// 
			// rdoUp
			// 
			this.rdoUp.Location = new System.Drawing.Point(24, 17);
			this.rdoUp.Name = "rdoUp";
			this.rdoUp.Size = new System.Drawing.Size(75, 24);
			this.rdoUp.TabIndex = 0;
			this.rdoUp.Text = "向上";
			this.rdoUp.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(338, 48);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(104, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// FormTextFind
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(454, 104);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.cbxCaseSensitive);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtKey);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormTextFind";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "查找";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTextFindFormClosing);
			this.Load += new System.EventHandler(this.FormTextFindLoad);
			this.VisibleChanged += new System.EventHandler(this.FormTextSearchVisibleChanged);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
