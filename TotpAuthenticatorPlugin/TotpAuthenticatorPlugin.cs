/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 16:10
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Com.Spbdev.SecurityNote.TotpAuthenticator.Utils;

namespace Com.Spbdev.SecurityNote.TotpAuthenticator
{
	/// <summary>
	/// Description of TotpAuthenticatorPlugin.
	/// </summary>
	public class TotpAuthenticatorPlugin : IPlugin
	{
		public TotpAuthenticatorPlugin()
		{
		}

		private ToolStripMenuItem menuItem;
		private FormAuth formAuth;
		private Form formMain;
		private string fileContent;
		private Dictionary<String, String> secretDict = new Dictionary<string, string>();
		
		#region IPlugin implementation

		public void OnApplicationStart(object owner, MenuStrip menu)
		{
			formMain = owner as Form;
			foreach(ToolStripMenuItem m in menu.Items) {
				if(m.Name == "MenuItem_Tool") {
					if(menuItem == null) {
						menuItem = new ToolStripMenuItem("二次验证码", null, MenuItemTotpShow_Click, "MenuItem_totp_show");
						menuItem.ShortcutKeys = Keys.F5;
					}
					m.DropDownItems.Add(menuItem);
					m.DropDownOpening += MenuPlugins_DropDownOpening;
				}
			}
		}

		public void OnApplicationClosing()
		{
			if(formAuth != null) {
				formAuth.Dispose();
				formAuth = null;
			}
		}

		public void OnFileLoad(string filepath)
		{
			
		}

		public void OnContentChanged(object textEditor, string content)
		{
			this.fileContent = content;
			secretDict.Clear();
			if(menuItem != null) {
				menuItem.Enabled = true; //否则F5无法触发
			}
		}

		public string Name {
			get {
				return "TotpAuth";
			}
		}

		public string Description {
			get {
				return "二次验证码";
			}
		}

		#endregion
		
		private bool UpdateMenuItemState() {
			if(!String.IsNullOrEmpty(this.fileContent)) {
				KeyValueParser.Parse(this.fileContent, false, secretDict);
				if(secretDict.Count > 0) {
					menuItem.Enabled = true;
					return true;
				}
			}
			menuItem.Enabled = false;
			return false;
		}
		
		private void MenuItemTotpShow_Click(object sender, EventArgs e) {
			if(secretDict.Count == 0 && !UpdateMenuItemState()) {
				return;
			}
			if(formAuth == null) {
				formAuth = new FormAuth();
				if(formMain != null) {
					formAuth.Icon = formMain.Icon;
				}
			}
			formAuth.Init(secretDict);
			formAuth.ShowDialog(formMain);
		}

		void MenuPlugins_DropDownOpening(object sender, EventArgs e)
		{
			UpdateMenuItemState();
		}
	}
}
