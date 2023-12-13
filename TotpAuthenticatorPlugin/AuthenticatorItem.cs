/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 17:15
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using Com.Spbdev.SecurityNote.TotpAuthenticator.Controls;

namespace Com.Spbdev.SecurityNote.TotpAuthenticator
{
	/// <summary>
	/// Description of AuthenticatorItem.
	/// </summary>
	public class AuthenticatorItem
	{
		public AuthenticatorItem()
		{
		}
		
		public String Secret;
		public AuthCodeViewControl Control;
		public Authenticator Authenticator;
	}
}
