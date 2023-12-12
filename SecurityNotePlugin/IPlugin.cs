/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 14:41
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Windows.Forms;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of IPlugin.
	/// </summary>
	public interface IPlugin
	{
		string Name{
			get;
		}
		
		string Description{
			get;
		}
		
		void OnApplicationStart(object owner, MenuStrip menu);
		
		void OnApplicationClosing();
		
		void OnFileLoad(string filepath);
		
		void OnContentChanged(object textEditor, string content);
	}
}
