/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/22
 * 时间: 13:47
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;
using Com.Spbdev.SecurityNote.Utils;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of FormOptions.
	/// </summary>
	public partial class FormOptions : Form
	{
		public FormOptions()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			if(Environment.OSVersion.Version.Major >= 6) {
				base.Font = SystemFonts.IconTitleFont;
			}
		}
		
		#region 属性/字段
		private FormDpiScaleHelper dpiScaler = new FormDpiScaleHelper();
		
		private ApplicationConfig applicationConfig;
		public ApplicationConfig ApplicationConfig{
			get {
				return applicationConfig;
			}
			set{
				if(applicationConfig != value) {
					applicationConfig = value;
					UpdateUI();
				}
			}
		}
		#endregion
		
		#region 方法
		protected override void WndProc(ref Message m)
		{
			dpiScaler.HandleWndProc(ref m);
			base.WndProc(ref m);
		}
		
		private bool IsAdministrator() {
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
			return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
		}
		
		private void UpdateUI() {
			cbxEnableFileAssociate.Checked = applicationConfig != null && applicationConfig.FileAssociate;
		}
		#endregion
		
		#region 事件处理
		void BtnOKClick(object sender, EventArgs e)
		{
			if(!IsAdministrator()) { //当前不是管理员权限
				if(MessageBox.Show(this, "该操作需要管理员权限；点击“确定”立即以管理员身份重新启动应用", "重新启动应用", MessageBoxButtons.OKCancel) == DialogResult.OK) {
					ProcessStartInfo startInfo = new ProcessStartInfo();
					startInfo.FileName = Application.ExecutablePath;
					startInfo.Arguments = ApplicationConfig.ShowFileAssociateArg; //重启后展示文件关联界面
					//设置启动动作,确保以管理员身份运行
					startInfo.Verb = "runas";
					try {
						Process.Start(startInfo);
						Application.Exit();
					}
					catch(Exception ex) {
						Debug.WriteLine(ex.ToString());
					}
				}
				return;
			}
			try{
				if(cbxEnableFileAssociate.Checked) {
					RegistryUtils.FileAssociate(new string[]{".snt"});
				}
				else {
					RegistryUtils.ClearFileAssociate();
				}
				if(applicationConfig != null) {
					applicationConfig.FileAssociate = cbxEnableFileAssociate.Checked;
				}
			}
			catch(Exception ex) {
				MessageBox.Show(this, String.Format("建立文件关联失败：{0}", ex.Message));
			}
		}
		void FormOptionsVisibleChanged(object sender, EventArgs e) {
			if(this.Visible) {
				UpdateUI();
			}
		}
		
		void FormOptionsLoad(object sender, EventArgs e) {
			dpiScaler.Start(this);
		}
		
		void FormOptionsFormClosing(object sender, FormClosingEventArgs e) {
			dpiScaler.Stop();
		}
		#endregion
	}
}
