/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 17:25
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Threading;
using System.Windows.Forms;

namespace Com.Spbdev.SecurityNote.TotpAuthenticator
{
	/// <summary>
	/// Description of StatusTextManager.
	/// </summary>
	public class StatusTextManager
	{
		class ResetContext{
			public ResetContext(int delayMilliseconds, long version) {
				this.DelayMilliseconds = delayMilliseconds;
				this.Version = version;
			}
			public int DelayMilliseconds;
			public long Version;
		}
		public StatusTextManager(ToolStripStatusLabel label)
		{
			this.label = label;
			this.rawText = this.label.Text;
		}
		
		private ToolStripStatusLabel label;
		private readonly String rawText;
		
		private long version = 0;
		
		public void SetText(String text) {
			if(label.Owner.InvokeRequired) {
				label.Owner.BeginInvoke(new Action<String>(SetText), text);
				return;
			}
			version++;
			label.Text = text;
		}
		
		public void ResetAfter(int delayMilliseconds) {
			version++;
			ThreadPool.QueueUserWorkItem(new WaitCallback(DoReset), new ResetContext(delayMilliseconds, version));
			
		}
		
		private void DoReset(object resetContext) {
			ResetContext _ctx = (ResetContext)resetContext;
			Thread.Sleep(_ctx.DelayMilliseconds);
			if(_ctx.Version < version)
				return;
			SetText(rawText);
		}
	}
}
