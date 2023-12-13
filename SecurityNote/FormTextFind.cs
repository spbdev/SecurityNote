/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/7/2
 * 时间: 17:12
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of FormTextFind.
	/// </summary>
	public partial class FormTextFind : Form
	{
		public FormTextFind()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			if(Environment.OSVersion.Version.Major >= 6) {
				base.Font = SystemFonts.IconTitleFont;
			}
		}
		#region 字段、属性
		private FormDpiScaleHelper dpiScaler = new FormDpiScaleHelper();
		
		private string findText; //暂存上一次进行查找的关键字
		public string FindText{
			get{
				return findText;
			}
		}

		public bool HideOnClosing {
			get;
			set;
		}
		
		public TextBoxBase TargetTextBox{
			get;
			set;
		}
		
		#endregion
		
		#region 方法
		
		public void FindNext() {
			if(findText.Length == 0)
				return;
			if(TargetTextBox == null)
				return;
			int _idx = TargetTextBox.Text.IndexOf(findText, TargetTextBox.SelectionStart + TargetTextBox.SelectionLength, cbxCaseSensitive.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
			ApplyFind(_idx, findText);
		}
		
		public void FindPrev() {
			if(findText.Length == 0)
				return;
			if(TargetTextBox == null)
				return;
			
			int _idx = TargetTextBox.SelectionStart == 0 ? -1 : TargetTextBox.Text.LastIndexOf(findText, TargetTextBox.SelectionStart - 1, cbxCaseSensitive.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
			ApplyFind(_idx, findText);
		}
		
		protected override void WndProc(ref Message m)
		{
			dpiScaler.HandleWndProc(ref m);
			base.WndProc(ref m);
		}
		
		private void ApplyFind(int index, String key) {
			if(index > -1) {
				TargetTextBox.SelectionStart = index;
				TargetTextBox.SelectionLength = key.Length;
				try{
					TargetTextBox.ScrollToCaret();
				}
				catch{
				}
			}
			else {
				MessageBox.Show(String.Format("找不到\"{0}\"", key));
			}
		}
		#endregion
		
		#region 事件处理
		void BtnOKClick(object sender, EventArgs e)
		{
			findText = txtKey.Text;
			if(rdoDown.Checked){
				FindNext();
			}
			else {
				FindPrev();
			}
		}
		
		void BtnCancelClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void FormTextSearchVisibleChanged(object sender, EventArgs e)
		{
			if(TargetTextBox == null)
				return;
			String _key = TargetTextBox.SelectedText;
			if(_key.Length > 0) {
				String[] _lines = _key.Split('\r', '\n');
				foreach(String item in _lines) {
					if(item.Length > 0) {
						findText = item;
						break;
					}
				}
			}
		}
		void FormTextFindFormClosing(object sender, FormClosingEventArgs e)
		{
			if(HideOnClosing && e.CloseReason == CloseReason.UserClosing) {
				e.Cancel = true;
				this.Hide();
				return;
			}
			dpiScaler.Stop();
		}
		void FormTextFindLoad(object sender, EventArgs e)
		{
			dpiScaler.Start(this);
		}
		#endregion
	}
}
