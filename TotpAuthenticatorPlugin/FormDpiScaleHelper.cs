/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 17:17
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Com.Spbdev.SecurityNote.TotpAuthenticator.Utils;

namespace Com.Spbdev.SecurityNote.TotpAuthenticator
{
	/// <summary>
	/// Description of FormDpiScaleHelper.
	/// </summary>
	public class FormDpiScaleHelper
	{
		#region 私有struct
		/// <summary>
		/// 距离上级容器的上下左右的距离
		/// </summary>
		private struct AnchorRect{
			public AnchorRect(int left, int right, int top, int bottom, int width, int height) {
				this.Left = left;
				this.Right = right;
				this.Top = top;
				this.Bottom = bottom;
				this.Width = width;
				this.Height = height;
			}
			public int Left;
			/// <summary>
			/// 到父容器右侧的距离，注意是相对父容器右侧而不是左侧
			/// </summary>
			public int Right;
			public int Top;
			/// <summary>
			/// 到父容器右侧的距离，注意是相对父容器右侧而不是左侧
			/// </summary>
			public int Bottom;
			public int Width;
			public int Height;
		}
		#endregion
		
		#region win32api DPI部分
		[StructLayout(LayoutKind.Sequential)]
		struct WndRect{
			public int left{
				get;
				set;
			}
			public int top{
				get;
				set;
			}
			public int right{
				get;
				set;
			}
			public int bottom{
				get;
				set;
			}
			public override string ToString()
			{
				return string.Format("[WndRect Left={0}, Right={1}, Top={2}, Bottom={3}]", left, right, top, bottom);
			}

		}
		#endregion
		
		#region 构造函数
		public FormDpiScaleHelper():this(true) {
			
		}
		public FormDpiScaleHelper(bool resizeForm) {
			this.resizeForm = resizeForm;
		}
		#endregion
		
		#region 成员/属性
		private Form form;
		private bool resizeForm;
		private uint formDpi;
		private IntPtr formLastMonitor = IntPtr.Zero;
		private Dictionary<Control, AnchorRect> formControlRects = new Dictionary<Control, AnchorRect>();
		
		private Font formMenuFont;
		private Font formFont;
		public Font FormFont{
			get{
				return formFont;
			}
			set{
				formFont = value;
			}
		}
		
		
		
		private float scaleFactor = 1f;
		public float ScaleFactor{
			get{
				return scaleFactor;
			}
		}
		#endregion
		
		#region 事件
		public event EventHandler FormScaled;
		protected void OnFormScaled(EventArgs e) {
			if(FormScaled != null) {
				FormScaled(this, e);
			}
		}
		#endregion
		
		
		#region 方法
		public void Start(Form form) {
			if(this.form == form) {
				return;
			}
			if(this.form != null) {
				throw new InvalidOperationException();
			}
			
			this.form = form;
			formFont = form.Font;
			formMenuFont = (form.MainMenuStrip != null ? form.MainMenuStrip.Font : SystemFonts.MenuFont);
			using(Graphics g = Graphics.FromHwnd(form.Handle)) {
				formDpi = (uint)g.DpiX;
			}
			SnapshotChildrenLocation(form);
			formLastMonitor = GetCurrentMonitor();
		}
		
		public void Stop() {
			if(this.form == null)
				return;
			
			formControlRects.Clear();
			this.form = null;
		}
		
		public void HandleWndProc(ref Message m) {
			if(m.Msg == 0x02E0) { //WM_DPICHANGED
				if(this.form == null) {
					m.Result = (IntPtr)1;
					return;
				}
				int _dpiY = (m.WParam.ToInt32() >> 16);
				int _dpiX = (m.WParam.ToInt32() & 0xFFFF);
				WndRect _rect = (WndRect)Marshal.PtrToStructure(m.LParam, typeof(WndRect));
				ScaleForm((uint)_dpiX, (uint)_dpiY, new Rectangle(_rect.left, _rect.top, _rect.right - _rect.left, _rect.bottom - _rect.top));
				m.Result = IntPtr.Zero;
			}
		}
		
		public void ScaleForm(uint dpiX, uint dpiY, Rectangle rect) {
			if(formDpi <=0)
				return;
			float _scaleFactorOld = scaleFactor;
			scaleFactor = (float)dpiX / formDpi;
			if(Math.Abs(_scaleFactorOld - scaleFactor) > float.MinValue) {
				form.SuspendLayout();
				form.Font = new Font(formFont.FontFamily, (float)Math.Round(scaleFactor * formFont.Size), formFont.Unit);
				if(form.MainMenuStrip != null) {
					form.MainMenuStrip.Font = new Font(formMenuFont.FontFamily, (float)Math.Round(scaleFactor * formMenuFont.Size), formMenuFont.Unit);
				}
				if(resizeForm) {//直接设置Bounds而不是size+location，否则可能引起窗体反复收到WM_DPICHANGED消息
					form.Bounds = rect;
				}
				RestoreChildrenLocation(form, scaleFactor);
				form.ResumeLayout();
				OnFormScaled(EventArgs.Empty); //引发事件
			}
		}
		
		private IntPtr GetCurrentMonitor() {
			double _scaleFactor;
			if(resizeForm) {
				//当前的缩放比例，避免跨屏幕时因为窗口大小变化引起中心点变化，导致反复缩放
				_scaleFactor = scaleFactor > 0d ? scaleFactor * 1.01 : 1d * 0.99d;
			}
			else {
				_scaleFactor = 1d;
			}
			Point _p = new Point(form.Left + (int)(form.Width / 2.0 / _scaleFactor), form.Top + (int)(form.Height / 2.0 / _scaleFactor));
			return ScreenUtils.MonitorFromPoint(new Point((int)_p.X, (int)_p.Y), 2);
		}
		
		private void SnapshotChildrenLocation(Control c) {
			foreach(Control cc in c.Controls) {
				SnapshotChildrenLocation(cc); //递归
				if(cc is MenuStrip)//不保存菜单的位置信息
					continue;
				formControlRects[cc] = new AnchorRect(cc.Left, c.ClientSize.Width - cc.Right, cc.Top, c.ClientSize.Height - cc.Bottom, cc.Width, cc.Height);
			}
		}
		
		private void RestoreChildrenLocation(Control c, float factor) {
			int _left, _right, _top, _bottom;
			foreach(Control cc in c.Controls) {
				RestoreChildrenLocation(cc, factor); //递归
				if(formControlRects.ContainsKey(cc)) {
					if(cc.Dock == DockStyle.Fill)
						continue;
					
					_left = formControlRects[cc].Left;
					if((cc.Anchor & AnchorStyles.Left) == AnchorStyles.Left) {
						_left = (int)(_left * factor);
					}
					if((cc.Anchor & AnchorStyles.Right) == AnchorStyles.Right) {
						_right = (int)(cc.Parent.ClientSize.Width - formControlRects[cc].Right * factor);
						if((cc.Anchor & AnchorStyles.Left) != AnchorStyles.Left) {
							_left = _right - (int)(formControlRects[cc].Width * factor);
						}
					}
					else {
						_right = _left + (int)(formControlRects[cc].Width * factor);
					}
					_top = formControlRects[cc].Top;
					if((cc.Anchor & AnchorStyles.Top) == AnchorStyles.Top) {
						_top = (int)(_top * factor);
					}
					if((cc.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom) {
						_bottom = (int)(cc.Parent.ClientSize.Height - formControlRects[cc].Bottom * factor);
						if((cc.Anchor & AnchorStyles.Top) != AnchorStyles.Top) {
							_top = _bottom - (int)(formControlRects[cc].Height * factor);
						}
					}
					else {
						_bottom = _top + (int)(formControlRects[cc].Height * factor);
					}
					cc.Bounds = new Rectangle(_left, _top, _right - _left, _bottom - _top);
				}
			}
		}
		
		#endregion
	}
}
