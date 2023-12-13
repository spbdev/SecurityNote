/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/15
 * 时间: 14:32
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of SecurityNodeException.
	/// </summary>
	public class SecurityNoteException : ApplicationException
	{
		public SecurityNoteException() : base()
		{
		}
		
		public SecurityNoteException(String message) : base(message) {
			
		}
		
		public SecurityNoteException(String message, Exception innerException) : base(message, innerException) {
			
		}
	}
}
