/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/14
 * 时间: 16:31
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;
using Com.Spbdev.SecurityNote.Model;
using Com.Spbdev.SecurityNote.Utils;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm(string[] args)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.Icon = Properties.Resource.App;
			titleFormat = base.Text;
			base.Text = String.Format(titleFormat, "无标题");
			noteItems[txtContent] = new FileItem();
			if(Environment.OSVersion.Version.Major >= 6) {
				base.Font = SystemFonts.IconTitleFont;
			}
			
			if(args != null && args.Length > 0) {
				foreach(String item in args) {
					try{
						switch(item.Trim()) {
							case ApplicationConfig.ShowFileAssociateArg:
								toShowFileAssociateDialog = true;
								break;
							default:
								if(fileToOpen == null && !String.IsNullOrEmpty(item) && File.Exists(item)) {
									fileToOpen = item;
								}
								break;
						}
					}
					catch(Exception ex) {
						Debug.WriteLine(ex.ToString());
					}
				}
				
			}
			pluginManager.LoadPlugins(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins"));
		}
		
		#region 字段
		private const string OpenFileHistoryTagPrefix = "FileHistory:";
		
		private String fileToOpen;
		private bool toShowFileAssociateDialog = false;
		private String titleFormat;
		private Dictionary<Control, FileItem> noteItems = new Dictionary<Control, FileItem>();//为多标签页做准备
		private HashAlgorithm hashAlg = SHA256.Create();
		private ApplicationConfigSerializer configSerializer;
		private FileHistoryManager fileHistory = new FileHistoryManager();
		private Stack<ToolStripMenuItem> openFileHistoryMenuItems = new Stack<ToolStripMenuItem>();
		private FormAbout formAbout;
		private FormPassword formPassword = new FormPassword();
		private FormOptions formOptions;
		private FormTextFind formFind;
		private FormDpiScaleHelper dpiScaler = new FormDpiScaleHelper();
		private PluginManager pluginManager = new PluginManager();
		#endregion
		
		#region 方法
		protected override void WndProc(ref Message m)
		{
			dpiScaler.HandleWndProc(ref m);
			base.WndProc(ref m);
		}
		
		private void BindingConfig() {
			this.Size = configSerializer.ApplicationConfig.WindowSize;
			this.WindowState = configSerializer.ApplicationConfig.WindowState;
			txtContent.Font = configSerializer.ApplicationConfig.DisplayFont; //formScaler用
			
			//this.DataBindings.Add(new Binding("Size", configSerializer.ApplicationConfig, "WindowSize", false, DataSourceUpdateMode.OnPropertyChanged));
			//txtContent.DataBindings.Add("Font", configSerializer.ApplicationConfig, "DisplayFont");
			txtContent.DataBindings.Add("WordWrap", configSerializer.ApplicationConfig, "WordWrap");
		}
		
		private bool CheckEditorSave() {
			if(noteItems[txtContent].Edited) {
				FileInfo _fileInfo = noteItems[txtContent].File;
				String filename = (_fileInfo == null ? "无标题" : Path.GetFileName(_fileInfo.FullName));
				DialogResult _dr = MessageBox.Show(this, "是否将更改保存到 " + filename, Application.ProductName, MessageBoxButtons.YesNoCancel);
				if(_dr == DialogResult.Cancel) {
					return false;
				}
				if(_dr == DialogResult.Yes){
					MenuItem_FileSave.PerformClick();
				}
			}
			return true;
		}
		
		private void Find() {
			if(formFind == null || formFind.IsDisposed) {
				formFind = new FormTextFind();
				formFind.HideOnClosing = true;
			}
			formFind.TargetTextBox = txtContent;
			formFind.Show(this);
		}
		
		private void OpenFile(String filepath) {
			formPassword.Text = String.Format("打开 {0}", Path.GetFileName(filepath));
			while(true) {
				if(formPassword.ShowDialog(this) == DialogResult.OK) {
					noteItems[txtContent].Password = hashAlg.ComputeHash(ApplicationConfig.FileEncoding.GetBytes(formPassword.Password));
				}
				else {
					return;
				}
				try {
					noteItems[txtContent].Load(new System.IO.FileInfo(filepath));
					txtContent.Text = noteItems[txtContent].Content;
					noteItems[txtContent].Edited = false; //刚打开，尚未编辑
					txtContent.SelectionStart = 0;
					txtContent.SelectionLength = 0;
					txtContent.Select();
					fileHistory.Add(filepath);
				}
				catch(SecurityNoteException sne) {
					MessageBox.Show("打开文件失败:" + sne.Message);
					continue;
				}
				catch(Exception ex){
					noteItems[txtContent].Reset();
					MessageBox.Show("打开文件失败：" + ex.ToString());
					noteItems[txtContent].Reset();
					txtContent.Clear();
					return;
				}
				break;
			}
			UpdateFormCaption();
		}
		
		private void SaveFile(String filepath) {
			noteItems[txtContent].Content = txtContent.Text;
			if(noteItems[txtContent].Password == null) {
				formPassword.Text = String.Format("保存 {0}", Path.GetFileName(filepath ?? noteItems[txtContent].File.FullName));
				if(formPassword.ShowDialog(this) == DialogResult.OK) {
					noteItems[txtContent].Password = hashAlg.ComputeHash(ApplicationConfig.FileEncoding.GetBytes(formPassword.Password));
				}
				else {
					return;
				}
			}
			if(filepath == null) {
				noteItems[txtContent].Save();
			}
			else {
				noteItems[txtContent].Save(new FileInfo(filepath));
				fileHistory.Add(filepath);
			}
			UpdateFormCaption();
		}
		
		private void ShowAssociateDialog() {
			if(formOptions == null){
				formOptions = new FormOptions();
			}
			formOptions.ApplicationConfig = configSerializer.ApplicationConfig;
			formOptions.ShowDialog(this);
		}
		
		private void UpdateFormCaption() {
			FileInfo _fileInfo = noteItems[txtContent].File;
			String filename = (_fileInfo == null ? "无标题" : Path.GetFileName(_fileInfo.FullName));
			base.Text = (noteItems[txtContent].Edited ? "*" : "") + String.Format(titleFormat, filename);
		}
		
		private void UpdateUIFont() {
			txtContent.Font = new Font(configSerializer.ApplicationConfig.DisplayFont.FontFamily, configSerializer.ApplicationConfig.DisplayFont.Size * dpiScaler.ScaleFactor, GraphicsUnit.Point);
			
		}

		#endregion
		
		#region 事件处理
		
		void FormScaler_FormScaled(object sender, EventArgs e) {
			UpdateUIFont();
		}

		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if(!CheckEditorSave()) {
				e.Cancel = true;
				return;
			}
			pluginManager.OnApplicationClosing();
			if(WindowState == FormWindowState.Normal) {
				configSerializer.ApplicationConfig.WindowSize = new Size(Convert.ToInt32(this.Width / dpiScaler.ScaleFactor), Convert.ToInt32(this.Height / dpiScaler.ScaleFactor));
			}
			if(WindowState != FormWindowState.Minimized) {
				configSerializer.ApplicationConfig.WindowState = this.WindowState;
			}
			
			dpiScaler.Stop();
			fileHistory.SaveToConfig();
			configSerializer.SaveConfig();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			fileHistory.LoadFromConfig();
			configSerializer = new ApplicationConfigSerializer(this);
			configSerializer.LoadConfig();
			BindingConfig();
			if(fileToOpen != null) {
				OpenFile(fileToOpen);
			}
			if(toShowFileAssociateDialog) {
				toShowFileAssociateDialog = false;
				ShowAssociateDialog();
			}
			dpiScaler.Start(this);
			dpiScaler.FormScaled += FormScaler_FormScaled;
			pluginManager.OnApplicationStart(this, menuStrip1);
		}
		
		void MainFormDragEnter(object sender, DragEventArgs e)
		{
			if(e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
			}
		}
		void MainFormDragDrop(object sender, DragEventArgs e)
		{
			string[] _files = e.Data.GetData(DataFormats.FileDrop) as string[];
			if(_files != null && _files.Length > 0) {
				OpenFile(_files[0]);
			}
		}
	
		void MenuItem_FileDropDownOpening(object sender, EventArgs e)
		{
			//控制设置密码菜单是否可见
			MenuItem_FileSetPwd.Visible = (noteItems[txtContent].File != null);
			
			//动态添加文件打开历史记录
			ToolStripMenuItem _menu;
			String filename;
			int k;
			int _idx;
			for(k = MenuItem_File.DropDownItems.Count - 1; k >= 0; k--) {
				ToolStripItem _item = MenuItem_File.DropDownItems[k];
				if(_item.Tag != null && Convert.ToString(_item.Tag).StartsWith(OpenFileHistoryTagPrefix, StringComparison.Ordinal)) {
					MenuItem_File.DropDownItems.RemoveAt(k);
					_menu = _item as ToolStripMenuItem;
					if(_menu != null) {
						_menu.Click -= MenuItem_FileOpenHistory;
						openFileHistoryMenuItems.Push(_menu);
					}
				}
			}
			_idx = MenuItem_File.DropDownItems.IndexOf(MenuItem_FileHistoryClear);
			if(_idx > 0 && fileHistory.All.Length > 0) {
				for(k = 0; k < fileHistory.All.Length; k++) {
					filename = fileHistory.All[k];
					if(openFileHistoryMenuItems.Count > 0) {
						_menu = openFileHistoryMenuItems.Pop();
					}
					else {
						_menu = new ToolStripMenuItem();
					}
					_menu.Click += MenuItem_FileOpenHistory;
					_menu.Tag = OpenFileHistoryTagPrefix + filename;
					_menu.Text = Convert.ToString(k + 1) + " " + StringUtils.ToDisplayPath(filename, 30);
					MenuItem_File.DropDownItems.Insert(_idx, _menu);
					_idx++;
				}
				//添加分隔线
				ToolStripSeparator _sep = new ToolStripSeparator();
				_sep.Tag = OpenFileHistoryTagPrefix;
				MenuItem_File.DropDownItems.Insert(_idx, _sep);
			}
			MenuItem_FileHistoryClear.Enabled = (fileHistory.All.Length > 0);
		}
		
		void MenuItem_FileNewClick(object sender, EventArgs e)
		{
			if(!CheckEditorSave()) {
				return;
			}
			noteItems[txtContent].Reset();
			txtContent.Clear();
		}
		
		void MenuItem_FileOpenHistory(object sender, EventArgs e) {
			if(!CheckEditorSave()) {
				return;
			}
			
			ToolStripMenuItem _item = sender as ToolStripMenuItem;
			if(_item != null && _item.Tag != null) {
				String _str = Convert.ToString(_item.Tag);
				if(_str.StartsWith(OpenFileHistoryTagPrefix, StringComparison.Ordinal)) {
					_str = _str.Substring(OpenFileHistoryTagPrefix.Length);
					OpenFile(_str);
				}
			}
		}
		
		void MenuItem_FileOpenClick(object sender, EventArgs e) {
			if(!CheckEditorSave()) {
				return;
			}
			String _filename = null;
			if(openFileDlg.ShowDialog(this) == DialogResult.OK) {
				_filename = openFileDlg.FileName;
			}
			if(_filename != null) {
				OpenFile(_filename);//会弹出输入密码对话框，所以放到打开文件对话框之外
			}
		}
		
		void MenuItem_FileSetPwdClick(object sender, EventArgs e)
		{
			byte[] _oldPwd = noteItems[txtContent].Password;
			noteItems[txtContent].Password = null;
			MenuItem_FileSave.PerformClick();
			if(noteItems[txtContent].Password != null) {
				MessageBox.Show(this, "设置密码成功！");
			}
			else {
				noteItems[txtContent].Password = _oldPwd;
			}
		}
		
		void MenuItem_FileSaveClick(object sender, EventArgs e)
		{
			String _filename = null;
			if(noteItems[txtContent].File == null) {
				if(this.saveFileDlg.ShowDialog(this) != DialogResult.OK) {
					return;
				}
				_filename = saveFileDlg.FileName;
			}
			SaveFile(_filename);
		}
		
		void MenuItem_FileSaveAsClick(object sender, EventArgs e)
		{
			if(this.saveFileDlg.ShowDialog(this) == DialogResult.OK) {
				SaveFile(saveFileDlg.FileName);
			}
		}
		
		void MenuItem_FileHistoryClearClick(object sender, EventArgs e)
		{
			fileHistory.Clear();
		}
		
		void MenuItem_FileExitClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void MenuItem_EditSelectAllClick(object sender, EventArgs e)
		{
			txtContent.SelectAll();
		}
	
		void MenuItem_EditCopyClick(object sender, EventArgs e)
		{
			txtContent.Copy();
		}
		
		void MenuItem_EditCutClick(object sender, EventArgs e)
		{
			txtContent.Cut();
		}
		void MenuItem_EditPasteClick(object sender, EventArgs e)
		{
			txtContent.Paste();
		}
		
		void MenuItem_EditDropDownOpening(object sender, EventArgs e)
		{
			MenuItem_EditSelectAll.Enabled = txtContent.TextLength >  0;
			MenuItem_EditCopy.Enabled = MenuItem_EditCut.Enabled = txtContent.SelectionLength > 0;
			MenuItem_EditPaste.Enabled = Clipboard.ContainsText();
		}
		
		void MenuItem_EditFindClick(object sender, EventArgs e)
		{
			Find();
		}
		
		void MenuItem_EditFindPrevClick(object sender, EventArgs e)
		{
			if(formFind == null || formFind.IsDisposed || String.IsNullOrEmpty(formFind.FindText)) {
				Find();
				return;
			}
			formFind.FindPrev();
		}
		void MenuItem_EditFindNextClick(object sender, EventArgs e)
		{
			if(formFind == null || formFind.IsDisposed || String.IsNullOrEmpty(formFind.FindText)) {
				Find();
				return;
			}
			formFind.FindNext();
		}
		
		void MenuItem_FormatFontClick(object sender, EventArgs e)
		{
			fontDlg.Font = configSerializer.ApplicationConfig.DisplayFont;
			if(fontDlg.ShowDialog(this) == DialogResult.OK) {
				configSerializer.ApplicationConfig.DisplayFont = fontDlg.Font;
				UpdateUIFont();
			}
		}
		
		void MenuItem_FormatWrapClick(object sender, EventArgs e)
		{
			configSerializer.ApplicationConfig.WordWrap = !configSerializer.ApplicationConfig.WordWrap;
			//txtContent.WordWrap = !txtContent.WordWrap;
		}
		
		void MenuItem_FormatDropDownOpening(object sender, EventArgs e)
		{
			MenuItem_FormatWrap.Checked = txtContent.WordWrap;
		}
		
		void MenuItem_HelpAboutClick(object sender, EventArgs e)
		{
			if(formAbout == null) {
				formAbout = new FormAbout();
				formAbout.Owner = this;
			}
			formAbout.Show();
			formAbout.Location = new Point(this.Left + this.Width / 2 - formAbout.Width / 2, this.Top + this.Height / 2 - formAbout.Height / 2);
		}
		
		void MenuItem_ToolAssociateClick(object sender, EventArgs e)
		{
			ShowAssociateDialog();
		}
		
		void TxtContentTextChanged(object sender, EventArgs e)
		{
			noteItems[txtContent].Edited = true;
			UpdateFormCaption();
			pluginManager.OnContentChanged(sender, txtContent.Text);
		}

		#endregion
	}
}
