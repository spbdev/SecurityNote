/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 17:23
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Com.Spbdev.SecurityNote.TotpAuthenticator.Controls;
using Com.Spbdev.SecurityNote.TotpAuthenticator.Utils;

namespace Com.Spbdev.SecurityNote.TotpAuthenticator
{
	/// <summary>
	/// Description of FormAuth.
	/// </summary>
	public partial class FormAuth : Form
	{
		public FormAuth()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			if(Environment.OSVersion.Version.Major >= 6) {
				base.Font = SystemFonts.IconTitleFont;
			}
			statusTextManager = new StatusTextManager(toolStripStatusLabel1);
			if(this.Owner != null) {
				this.Icon = this.Owner.Icon;
			}
		}
		
		#region Fields
		private FormDpiScaleHelper dpiScaler = new FormDpiScaleHelper();
		private List<AuthenticatorItem> authItems = new List<AuthenticatorItem>();
		private StatusTextManager statusTextManager;
		private long dataVersion = 0L;
		private bool threadAborting = false;
		private bool threadRunning = false;
		#endregion
		
		#region 方法
		public void Init(Dictionary<String, String> items) {
			threadAborting = false;
			dataVersion++;
			ClearAuthItems();
			
			if(items != null && items.Count > 0) {
				float _scaleFactor;
				using(Graphics g = Graphics.FromHwnd(this.Handle)) {
					_scaleFactor = g.DpiX / 96f;
				}
				foreach(KeyValuePair<String, String> pair in items) {
					AddAuthItem(pair.Key, pair.Value, _scaleFactor);
				}
				if(authItems.Count > 0) {
					StartThread();
				}
			}
		}
		#endregion
		
		#region 私有方法
		protected override void WndProc(ref Message m)
		{
			dpiScaler.HandleWndProc(ref m);
			base.WndProc(ref m);
		}
		
		private AuthenticatorItem AddAuthItem(String name, String secret, float scaleFactor) {
			AuthenticatorItem _authItem = new AuthenticatorItem();
			authItems.Add(_authItem);
			_authItem.Secret = secret;
			_authItem.Authenticator = new Authenticator(Base32.ToBytes(secret), 30);
			_authItem.Control = new AuthCodeViewControl();
			_authItem.Control.Height = (int)(_authItem.Control.Height * scaleFactor);
			_authItem.Control.Dock = DockStyle.Top;
			_authItem.Control.Cursor = Cursors.Hand;
			_authItem.Control.Font = this.Font;
			_authItem.Control.Padding = new Padding(6, 3, 6, 3);
			_authItem.Control.AuthDesc = name;
			_authItem.Control.AuthCode = "";
			pnlCodes.Controls.Add(_authItem.Control);
			pnlCodes.Controls.SetChildIndex(_authItem.Control, 0);
			_authItem.Control.DoubleClick += AuthItemControl_DoubleClick;
			_authItem.Control.MouseDown += AuthItemControl_MouseDown;
			return _authItem;
		}
		
		private void ClearAuthItems() {
			while(authItems.Count > 0) {
				AuthenticatorItem _authItem = authItems[0];
				authItems.RemoveAt(0);
				_authItem.Control.DoubleClick -= AuthItemControl_DoubleClick;
				_authItem.Control.MouseDown -= AuthItemControl_MouseDown;
				pnlCodes.Controls.Remove(_authItem.Control);
				_authItem.Control.Dispose();
				_authItem.Control = null;
			}
		}
		
		private void DoGeneration(object state) {
			long _lastExpireSeconds = 0L;
			long _lastDataVersion = 0L;
			bool _update = false;
			try{
				while(authItems.Count > 0) {
					if(threadAborting) {
						threadAborting = false;
						break;
					}
					_update = (_lastDataVersion != dataVersion || _lastExpireSeconds < authItems[0].Authenticator.EXPIRE_SECONDS);
					_lastExpireSeconds = authItems[0].Authenticator.EXPIRE_SECONDS;
					_lastDataVersion = dataVersion;
					
					foreach(AuthenticatorItem item in authItems) {
						DoUpdateControlUI(item.Control, item.Authenticator.EXPIRE_SECONDS, _update ? item.Authenticator.GenerateCode() : null);
					}
					System.Threading.Thread.Sleep(1000);
				}
			}
			finally{
				threadRunning = false;
			}
		}
		private void DoUpdateControlUI(AuthCodeViewControl control, long expireSeconds, String code) {
			if(control.Disposing || control.Parent == null)
				return;
			if(control.InvokeRequired) {
				control.BeginInvoke(new Action<AuthCodeViewControl, long, String>(DoUpdateControlUI), control, expireSeconds, code);
				return;
			}
			control.ExpireRemainPercent = (int)(expireSeconds * 100d / 30);
			if(code != null) {
				control.AuthCode = code;
			}
		}
		private void DoCopy(AuthCodeViewControl control) {
			Clipboard.SetText(control.AuthCode);
			statusTextManager.SetText(String.Format("已复制 {0} 到剪贴板！", control.AuthCode));
			statusTextManager.ResetAfter(2000);
		}
		private void StartThread() {
			if(threadRunning)
				return;
			ThreadPool.QueueUserWorkItem(new WaitCallback(DoGeneration), null);
			threadRunning = true;
		}
		#endregion
		
		#region 事件处理
		void AuthItemControl_DoubleClick(object sender, EventArgs e)
		{
			AuthCodeViewControl _authCtrl = sender as AuthCodeViewControl;
			if(_authCtrl != null) {
				DoCopy(_authCtrl);
			}
		}

		void AuthItemControl_MouseDown(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right) {
				((AuthCodeViewControl)sender).ContextMenuStrip = ctxMenu;
			}
		}

		void CtxMM_CopyClick(object sender, EventArgs e)
		{
			AuthCodeViewControl _authCtrl = ctxMenu.SourceControl as AuthCodeViewControl;
			if(_authCtrl != null) {
				DoCopy(_authCtrl);
			}
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			dpiScaler.Start(this);
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			dpiScaler.Stop();
		}
		void FormAuthVisibleChanged(object sender, EventArgs e)
		{
			if(!this.Visible) {
				threadAborting = true;
			}
		}
		#endregion
	}
}
