/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 17:12
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Com.Spbdev.SecurityNote.TotpAuthenticator.Controls
{
	/// <summary>
	/// Description of AuthCodeViewControl.
	/// </summary>
	public class AuthCodeViewControl : UserControl
	{
		public AuthCodeViewControl()
		{
			base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
		}
		
		private readonly Color[] expireColors = new Color[]{Color.Crimson, Color.Crimson, Color.Red, Color.Red, Color.DeepPink, Color.MediumVioletRed, Color.BlueViolet, Color.SlateBlue, Color.Blue};
		private Rectangle codeRect;
		private Rectangle descRect;
		private Rectangle progressRect;
		private Font authCodeFont;
		private Font authDescFont;
		private int authCodeFontSize;
		private int authDescFontSize;
		private bool measureDirty = false;
		private bool mouseEnter = false;
		
		private String authCode;
		public String AuthCode {
			get {
				return authCode;
			}
			set {
				if(authCode != value) {
					authCode = value;
					measureDirty = true;
					base.Invalidate();
				}
			}
		}
		
		
		
		private String authDesc;
		public String AuthDesc {
			get {
				return authDesc;
			}
			set {
				if(authDesc != value) {
					authDesc = value;
					measureDirty = true;
					base.Invalidate();
				}
			}
		}
		
		private int expireRemainPercent;
		public int ExpireRemainPercent {
			get{
				return expireRemainPercent;
			}
			set {
				expireRemainPercent = value;
				measureDirty = true;
				base.Invalidate();
			}
		}
		
		
		private Size defaultSize = new Size(300, 60);
		protected override Size DefaultSize {
			get {
				return defaultSize;
			}
		}
		
		private void MesureString() {
			if(!measureDirty)
				return;
			measureDirty = false;
			authCodeFontSize = Math.Max(14, 28 * (this.Height * 4 / defaultSize.Height) / 4);
			authDescFontSize = Math.Max(12, 12 * (this.Height * 4 / defaultSize.Height) / 4);
			if(authCodeFont == null || (int)authCodeFont.Size != authDescFontSize) {
				authCodeFont = new Font(FontFamily.GenericSerif, authCodeFontSize, FontStyle.Bold, GraphicsUnit.Pixel);
			}
			if(authDescFont == null || (int)authDescFont.Size != authCodeFontSize) {
				authDescFont = new Font(FontFamily.GenericSerif, authDescFontSize, FontStyle.Regular, GraphicsUnit.Pixel);
			}
			//progressRect = new Rectangle(Padding.Left, this.Height - Padding.Bottom - 2, this.Width - Padding.Horizontal, 2);
			using(Graphics g = this.CreateGraphics()) {
				Size _size = Size.Ceiling(g.MeasureString(AuthCode, authCodeFont, (SizeF)this.Size, StringFormat.GenericDefault));
				codeRect = new Rectangle(Padding.Left, (int)((this.Height - _size.Height) / 3f), _size.Width, _size.Height);
				_size = Size.Ceiling(g.MeasureString(AuthDesc, authDescFont, (SizeF)this.Size, StringFormat.GenericDefault));
				descRect = new Rectangle(Padding.Left + 3, this.Height - Padding.Bottom - 2 - _size.Height, _size.Width, _size.Height);
			}
			int _progressSize = (this.Height - Padding.Vertical) / 2;
			progressRect = new Rectangle(this.Width - Padding.Right - _progressSize, (this.Height - _progressSize) / 2, _progressSize, _progressSize);
			
		}
		
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			measureDirty = true;
		}
		
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			if(!mouseEnter) {
				mouseEnter = true;
				base.Invalidate();
			}
		}
		
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if(mouseEnter) {
				mouseEnter = false;
				base.Invalidate();
			}
		}
		
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			measureDirty = true;
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			int _visualWidth = this.Width - Padding.Horizontal;
			if(_visualWidth <=0)
				return;
			MesureString();
			using(SolidBrush _brush = new SolidBrush(SystemColors.Highlight)) {
				e.Graphics.DrawString(AuthCode, authCodeFont, mouseEnter ? Brushes.Crimson : _brush, codeRect);
			
				_brush.Color = this.ForeColor;
				e.Graphics.DrawString(AuthDesc, authDescFont, _brush, descRect);
			}
			
			using(Pen _pen = new Pen(SystemColors.ControlLight, 3f)) {
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				e.Graphics.DrawArc(_pen, progressRect, 0f, 360f);
				e.Graphics.DrawLine(_pen, Padding.Left, this.Height - 0.5f, this.Width - Padding.Right, this.Height - 0.5f);
				if(expireRemainPercent < 25) {
					_pen.Color = expireColors[Math.Max(0, Math.Min(expireColors.Length - 1,  expireColors.Length * expireRemainPercent / 25))];
				}
				else {
					_pen.Color = Color.ForestGreen;
				}
				e.Graphics.DrawArc(_pen, progressRect, 3.6f * (100 - expireRemainPercent) - 90f, 3.6f * expireRemainPercent);
			}
		}
	}
}
