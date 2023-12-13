/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/16
 * 时间: 10:39
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of FormPassword.
	/// </summary>
	public partial class FormPassword : Form
	{
		public FormPassword()
		{
			InitializeComponent();
			if(Environment.OSVersion.Version.Major >= 6) {
				base.Font = SystemFonts.IconTitleFont;
			}
		}
		
		#region 字段、属性
		private FormDpiScaleHelper dpiScaler = new FormDpiScaleHelper();
		
		public String Password{
			get{
				return txtPassword.Text;
			}
		}
		#endregion
		
		#region 方法
		protected override void WndProc(ref Message m)
		{
			dpiScaler.HandleWndProc(ref m);
			base.WndProc(ref m);
		}
		#endregion
		
		#region 事件处理
		void FormPasswordVisibleChanged(object sender, EventArgs e)
		{
			if(this.Visible) {
				txtPassword.Clear();
			}
		}
		
		void FormPasswordLoad(object sender, EventArgs e) {
			dpiScaler.Start(this);
		}
		
		void FormPasswordFormClosing(object sender, FormClosingEventArgs e) {
			dpiScaler.Stop();
		}
		
		void TxtPasswordTextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = (txtPassword.TextLength > 0);
		}
		
		#endregion
	}
}
