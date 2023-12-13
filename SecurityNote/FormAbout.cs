/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/17
 * 时间: 9:40
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of FormAbout.
	/// </summary>
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			if(Environment.OSVersion.Version.Major >= 6) {
				base.Font = SystemFonts.IconTitleFont;
			}
			base.Text = "关于" + Application.ProductName;
			lblProductVal.Font = new Font(lblProductVal.Font.FontFamily, 1.25f * lblProductVal.Font.Size, FontStyle.Regular, GraphicsUnit.Point);
			lblCopyrightVal.Text = lblCopyrightVal.Text.Replace("{year}", DateTime.Now.Year.ToString());
		}
		private FormDpiScaleHelper dpiScaler = new FormDpiScaleHelper();
		
		protected override void WndProc(ref Message m)
		{
			dpiScaler.HandleWndProc(ref m);
			base.WndProc(ref m);
		}
		
		void FormAboutLoad(object sender, EventArgs e)
		{
			lblProductVal.Text = Application.ProductName + " v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
			dpiScaler.Start(this);
		}
		void FormAboutKeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape) {
				this.Hide();
			}
		}
		void FormAboutFormClosing(object sender, FormClosingEventArgs e)
		{
			if(e.CloseReason == CloseReason.UserClosing) {
				e.Cancel = true;
				this.Hide();
				return;
			}
			dpiScaler.Stop();
		}
	}
}
