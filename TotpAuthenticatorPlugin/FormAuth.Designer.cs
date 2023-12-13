/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 17:23
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace Com.Spbdev.SecurityNote.TotpAuthenticator
{
	partial class FormAuth
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Panel pnlCodes;
		private System.Windows.Forms.ContextMenuStrip ctxMenu;
		private System.Windows.Forms.ToolStripMenuItem CtxMM_Copy;
		
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
			this.components = new System.ComponentModel.Container();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlCodes = new System.Windows.Forms.Panel();
			this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CtxMM_Copy = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1.SuspendLayout();
			this.ctxMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 426);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(512, 25);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(492, 20);
			this.toolStripStatusLabel1.Spring = true;
			this.toolStripStatusLabel1.Text = "提示：双击验证码复制到剪贴板。";
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlCodes
			// 
			this.pnlCodes.AutoScroll = true;
			this.pnlCodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCodes.Location = new System.Drawing.Point(0, 0);
			this.pnlCodes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pnlCodes.Name = "pnlCodes";
			this.pnlCodes.Size = new System.Drawing.Size(512, 426);
			this.pnlCodes.TabIndex = 3;
			// 
			// ctxMenu
			// 
			this.ctxMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.CtxMM_Copy});
			this.ctxMenu.Name = "ctxMenu";
			this.ctxMenu.Size = new System.Drawing.Size(174, 28);
			// 
			// CtxMM_Copy
			// 
			this.CtxMM_Copy.Name = "CtxMM_Copy";
			this.CtxMM_Copy.Size = new System.Drawing.Size(173, 24);
			this.CtxMM_Copy.Text = "复制验证码(&C)";
			this.CtxMM_Copy.Click += new System.EventHandler(this.CtxMM_CopyClick);
			// 
			// FormAuth
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(512, 451);
			this.Controls.Add(this.pnlCodes);
			this.Controls.Add(this.statusStrip1);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.Name = "FormAuth";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "二次验证码";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.VisibleChanged += new System.EventHandler(this.FormAuthVisibleChanged);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ctxMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
