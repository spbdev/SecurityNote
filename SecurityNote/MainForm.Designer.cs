/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/14
 * 时间: 16:31
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace Com.Spbdev.SecurityNote
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_File;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_FileOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_FileExit;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_Help;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_HelpAbout;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_EditCopy;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_EditCut;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_EditPaste;
		private System.Windows.Forms.TextBox txtContent;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_FileSave;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_FileSaveAs;
		private System.Windows.Forms.OpenFileDialog openFileDlg;
		private System.Windows.Forms.SaveFileDialog saveFileDlg;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_EditSelectAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_FileSetPwd;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_Format;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_FormatFont;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_FormatWrap;
		private System.Windows.Forms.FontDialog fontDlg;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_FileHistoryClear;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_Tool;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_ToolAssociate;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_FileNew;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_EditFind;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_EditFindPrev;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_EditFindNext;
		
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
		protected void InitializeComponent() {
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.MenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_FileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_FileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_FileSetPwd = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_FileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_FileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuItem_FileHistoryClear = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuItem_FileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_EditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuItem_EditCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_EditCut = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_EditPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuItem_EditFind = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_EditFindPrev = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_EditFindNext = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_Format = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_FormatFont = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_FormatWrap = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_Tool = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_ToolAssociate = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.txtContent = new System.Windows.Forms.TextBox();
			this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
			this.fontDlg = new System.Windows.Forms.FontDialog();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.MenuItem_File,
			this.MenuItem_Edit,
			this.MenuItem_Format,
			this.MenuItem_Tool,
			this.MenuItem_Help});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(779, 28);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// MenuItem_File
			// 
			this.MenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.MenuItem_FileNew,
			this.MenuItem_FileOpen,
			this.MenuItem_FileSetPwd,
			this.MenuItem_FileSave,
			this.MenuItem_FileSaveAs,
			this.toolStripMenuItem3,
			this.MenuItem_FileHistoryClear,
			this.toolStripMenuItem1,
			this.MenuItem_FileExit});
			this.MenuItem_File.Name = "MenuItem_File";
			this.MenuItem_File.Size = new System.Drawing.Size(69, 24);
			this.MenuItem_File.Text = "文件(&F)";
			this.MenuItem_File.DropDownOpening += new System.EventHandler(this.MenuItem_FileDropDownOpening);
			// 
			// MenuItem_FileNew
			// 
			this.MenuItem_FileNew.Name = "MenuItem_FileNew";
			this.MenuItem_FileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.MenuItem_FileNew.Size = new System.Drawing.Size(249, 26);
			this.MenuItem_FileNew.Text = "新建(&N)";
			this.MenuItem_FileNew.Click += new System.EventHandler(this.MenuItem_FileNewClick);
			// 
			// MenuItem_FileOpen
			// 
			this.MenuItem_FileOpen.Name = "MenuItem_FileOpen";
			this.MenuItem_FileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.MenuItem_FileOpen.Size = new System.Drawing.Size(249, 26);
			this.MenuItem_FileOpen.Text = "打开(&O)...";
			this.MenuItem_FileOpen.Click += new System.EventHandler(this.MenuItem_FileOpenClick);
			// 
			// MenuItem_FileSetPwd
			// 
			this.MenuItem_FileSetPwd.Name = "MenuItem_FileSetPwd";
			this.MenuItem_FileSetPwd.Size = new System.Drawing.Size(249, 26);
			this.MenuItem_FileSetPwd.Text = "设置密码(&M)...";
			this.MenuItem_FileSetPwd.Click += new System.EventHandler(this.MenuItem_FileSetPwdClick);
			// 
			// MenuItem_FileSave
			// 
			this.MenuItem_FileSave.Name = "MenuItem_FileSave";
			this.MenuItem_FileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.MenuItem_FileSave.Size = new System.Drawing.Size(249, 26);
			this.MenuItem_FileSave.Text = "保存(&S)";
			this.MenuItem_FileSave.Click += new System.EventHandler(this.MenuItem_FileSaveClick);
			// 
			// MenuItem_FileSaveAs
			// 
			this.MenuItem_FileSaveAs.Name = "MenuItem_FileSaveAs";
			this.MenuItem_FileSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
			| System.Windows.Forms.Keys.S)));
			this.MenuItem_FileSaveAs.Size = new System.Drawing.Size(249, 26);
			this.MenuItem_FileSaveAs.Text = "另存为(&A)...";
			this.MenuItem_FileSaveAs.Click += new System.EventHandler(this.MenuItem_FileSaveAsClick);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(246, 6);
			// 
			// MenuItem_FileHistoryClear
			// 
			this.MenuItem_FileHistoryClear.Name = "MenuItem_FileHistoryClear";
			this.MenuItem_FileHistoryClear.Size = new System.Drawing.Size(249, 26);
			this.MenuItem_FileHistoryClear.Text = "清除文件列表(&C)";
			this.MenuItem_FileHistoryClear.Click += new System.EventHandler(this.MenuItem_FileHistoryClearClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(246, 6);
			// 
			// MenuItem_FileExit
			// 
			this.MenuItem_FileExit.Name = "MenuItem_FileExit";
			this.MenuItem_FileExit.Size = new System.Drawing.Size(249, 26);
			this.MenuItem_FileExit.Text = "退出(&X)";
			this.MenuItem_FileExit.Click += new System.EventHandler(this.MenuItem_FileExitClick);
			// 
			// MenuItem_Edit
			// 
			this.MenuItem_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.MenuItem_EditSelectAll,
			this.toolStripMenuItem2,
			this.MenuItem_EditCopy,
			this.MenuItem_EditCut,
			this.MenuItem_EditPaste,
			this.toolStripMenuItem4,
			this.MenuItem_EditFind,
			this.MenuItem_EditFindPrev,
			this.MenuItem_EditFindNext});
			this.MenuItem_Edit.Name = "MenuItem_Edit";
			this.MenuItem_Edit.Size = new System.Drawing.Size(69, 24);
			this.MenuItem_Edit.Text = "编辑(&E)";
			this.MenuItem_Edit.DropDownOpening += new System.EventHandler(this.MenuItem_EditDropDownOpening);
			// 
			// MenuItem_EditSelectAll
			// 
			this.MenuItem_EditSelectAll.Name = "MenuItem_EditSelectAll";
			this.MenuItem_EditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.MenuItem_EditSelectAll.Size = new System.Drawing.Size(229, 26);
			this.MenuItem_EditSelectAll.Text = "全选(&A)";
			this.MenuItem_EditSelectAll.Click += new System.EventHandler(this.MenuItem_EditSelectAllClick);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(226, 6);
			// 
			// MenuItem_EditCopy
			// 
			this.MenuItem_EditCopy.Name = "MenuItem_EditCopy";
			this.MenuItem_EditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.MenuItem_EditCopy.Size = new System.Drawing.Size(229, 26);
			this.MenuItem_EditCopy.Text = "复制(&C)";
			this.MenuItem_EditCopy.Click += new System.EventHandler(this.MenuItem_EditCopyClick);
			// 
			// MenuItem_EditCut
			// 
			this.MenuItem_EditCut.Name = "MenuItem_EditCut";
			this.MenuItem_EditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.MenuItem_EditCut.Size = new System.Drawing.Size(229, 26);
			this.MenuItem_EditCut.Text = "剪切(&X)";
			this.MenuItem_EditCut.Click += new System.EventHandler(this.MenuItem_EditCutClick);
			// 
			// MenuItem_EditPaste
			// 
			this.MenuItem_EditPaste.Name = "MenuItem_EditPaste";
			this.MenuItem_EditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.MenuItem_EditPaste.Size = new System.Drawing.Size(229, 26);
			this.MenuItem_EditPaste.Text = "粘贴(&P)";
			this.MenuItem_EditPaste.Click += new System.EventHandler(this.MenuItem_EditPasteClick);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(226, 6);
			// 
			// MenuItem_EditFind
			// 
			this.MenuItem_EditFind.Name = "MenuItem_EditFind";
			this.MenuItem_EditFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.MenuItem_EditFind.Size = new System.Drawing.Size(229, 26);
			this.MenuItem_EditFind.Text = "查找(&F)";
			this.MenuItem_EditFind.Click += new System.EventHandler(this.MenuItem_EditFindClick);
			// 
			// MenuItem_EditFindPrev
			// 
			this.MenuItem_EditFindPrev.Name = "MenuItem_EditFindPrev";
			this.MenuItem_EditFindPrev.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
			this.MenuItem_EditFindPrev.Size = new System.Drawing.Size(229, 26);
			this.MenuItem_EditFindPrev.Text = "查找上一个";
			this.MenuItem_EditFindPrev.Click += new System.EventHandler(this.MenuItem_EditFindPrevClick);
			// 
			// MenuItem_EditFindNext
			// 
			this.MenuItem_EditFindNext.Name = "MenuItem_EditFindNext";
			this.MenuItem_EditFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.MenuItem_EditFindNext.Size = new System.Drawing.Size(229, 26);
			this.MenuItem_EditFindNext.Text = "查找下一个";
			this.MenuItem_EditFindNext.Click += new System.EventHandler(this.MenuItem_EditFindNextClick);
			// 
			// MenuItem_Format
			// 
			this.MenuItem_Format.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.MenuItem_FormatFont,
			this.MenuItem_FormatWrap});
			this.MenuItem_Format.Name = "MenuItem_Format";
			this.MenuItem_Format.Size = new System.Drawing.Size(71, 24);
			this.MenuItem_Format.Text = "格式(&V)";
			this.MenuItem_Format.DropDownOpening += new System.EventHandler(this.MenuItem_FormatDropDownOpening);
			// 
			// MenuItem_FormatFont
			// 
			this.MenuItem_FormatFont.Name = "MenuItem_FormatFont";
			this.MenuItem_FormatFont.Size = new System.Drawing.Size(169, 26);
			this.MenuItem_FormatFont.Text = "字体(&F)";
			this.MenuItem_FormatFont.Click += new System.EventHandler(this.MenuItem_FormatFontClick);
			// 
			// MenuItem_FormatWrap
			// 
			this.MenuItem_FormatWrap.Name = "MenuItem_FormatWrap";
			this.MenuItem_FormatWrap.Size = new System.Drawing.Size(169, 26);
			this.MenuItem_FormatWrap.Text = "自动换行(&W)";
			this.MenuItem_FormatWrap.Click += new System.EventHandler(this.MenuItem_FormatWrapClick);
			// 
			// MenuItem_Tool
			// 
			this.MenuItem_Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.MenuItem_ToolAssociate});
			this.MenuItem_Tool.Name = "MenuItem_Tool";
			this.MenuItem_Tool.Size = new System.Drawing.Size(70, 24);
			this.MenuItem_Tool.Text = "工具(&T)";
			// 
			// MenuItem_ToolAssociate
			// 
			this.MenuItem_ToolAssociate.Name = "MenuItem_ToolAssociate";
			this.MenuItem_ToolAssociate.Size = new System.Drawing.Size(156, 26);
			this.MenuItem_ToolAssociate.Text = "文件关联...";
			this.MenuItem_ToolAssociate.Click += new System.EventHandler(this.MenuItem_ToolAssociateClick);
			// 
			// MenuItem_Help
			// 
			this.MenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.MenuItem_HelpAbout});
			this.MenuItem_Help.Name = "MenuItem_Help";
			this.MenuItem_Help.Size = new System.Drawing.Size(73, 24);
			this.MenuItem_Help.Text = "帮助(&H)";
			// 
			// MenuItem_HelpAbout
			// 
			this.MenuItem_HelpAbout.Name = "MenuItem_HelpAbout";
			this.MenuItem_HelpAbout.Size = new System.Drawing.Size(147, 26);
			this.MenuItem_HelpAbout.Text = "关于(&A)...";
			this.MenuItem_HelpAbout.Click += new System.EventHandler(this.MenuItem_HelpAboutClick);
			// 
			// txtContent
			// 
			this.txtContent.AcceptsTab = true;
			this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtContent.HideSelection = false;
			this.txtContent.Location = new System.Drawing.Point(0, 28);
			this.txtContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtContent.MaxLength = 0;
			this.txtContent.Multiline = true;
			this.txtContent.Name = "txtContent";
			this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtContent.Size = new System.Drawing.Size(779, 424);
			this.txtContent.TabIndex = 1;
			this.txtContent.WordWrap = false;
			this.txtContent.TextChanged += new System.EventHandler(this.TxtContentTextChanged);
			// 
			// openFileDlg
			// 
			this.openFileDlg.Filter = "Security Node(*.snt)|*.snt|所有文件(*.*)|*.*";
			// 
			// saveFileDlg
			// 
			this.saveFileDlg.Filter = "Security Node(*.snt)|*.snt|所有文件(*.*)|*.*";
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(779, 452);
			this.Controls.Add(this.txtContent);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "MainForm";
			this.Text = "{0} - 便笺";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainFormDragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainFormDragEnter);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
