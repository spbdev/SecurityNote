/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/17
 * 时间: 9:40
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace Com.Spbdev.SecurityNote
{
	partial class FormAbout
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblProductVal;
		private System.Windows.Forms.Label lblAuthorVal;
		private System.Windows.Forms.Label lblCopyrightVal;
		
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
			this.label2 = new System.Windows.Forms.Label();
			this.lblProductVal = new System.Windows.Forms.Label();
			this.lblAuthorVal = new System.Windows.Forms.Label();
			this.lblCopyrightVal = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(123, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Author：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(123, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "CopyRight：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblProductVal
			// 
			this.lblProductVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.lblProductVal.Location = new System.Drawing.Point(12, 6);
			this.lblProductVal.Name = "lblProductVal";
			this.lblProductVal.Size = new System.Drawing.Size(433, 38);
			this.lblProductVal.TabIndex = 2;
			this.lblProductVal.Text = "SecurityNote v1.00";
			this.lblProductVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblAuthorVal
			// 
			this.lblAuthorVal.Location = new System.Drawing.Point(141, 55);
			this.lblAuthorVal.Name = "lblAuthorVal";
			this.lblAuthorVal.Size = new System.Drawing.Size(303, 23);
			this.lblAuthorVal.TabIndex = 3;
			this.lblAuthorVal.Text = "spbdev";
			this.lblAuthorVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCopyrightVal
			// 
			this.lblCopyrightVal.Location = new System.Drawing.Point(141, 88);
			this.lblCopyrightVal.Name = "lblCopyrightVal";
			this.lblCopyrightVal.Size = new System.Drawing.Size(303, 23);
			this.lblCopyrightVal.TabIndex = 4;
			this.lblCopyrightVal.Text = "ixff.tech {year}";
			this.lblCopyrightVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 130);
			this.Controls.Add(this.lblCopyrightVal);
			this.Controls.Add(this.lblAuthorVal);
			this.Controls.Add(this.lblProductVal);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAbout";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "关于";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAboutFormClosing);
			this.Load += new System.EventHandler(this.FormAboutLoad);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAboutKeyDown);
			this.ResumeLayout(false);

		}
	}
}
