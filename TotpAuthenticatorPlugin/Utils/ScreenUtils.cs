/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 17:19
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Com.Spbdev.SecurityNote.TotpAuthenticator.Utils
{
	/// <summary>
	/// Description of ScreenUtils.
	/// </summary>
	public static class ScreenUtils
	{
		[DllImport("User32.dll")]
		internal static extern IntPtr MonitorFromPoint([In]Point pt, [In]uint dwFlags);
	}
}
