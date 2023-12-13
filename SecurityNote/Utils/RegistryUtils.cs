/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/22
 * 时间: 9:22
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace Com.Spbdev.SecurityNote.Utils
{
	/// <summary>
	/// Description of RegistryUtils.
	/// </summary>
	public class RegistryUtils
	{
		public RegistryUtils()
		{
		}
		
		private const char spliterString = ',';
		private const string regApplicationName = "SecurityNote";
		public static void FileAssociate(String[] fileExtensions) {
			ClearFileAssociate();
			
			RegistryKey _rk;
			foreach(String item in fileExtensions) {
				String _fe = (!item.StartsWith(".", StringComparison.Ordinal) ? "." + item : item);
				_rk = Registry.ClassesRoot.CreateSubKey(_fe);
				_rk.SetValue("", regApplicationName);
			}
			_rk = Registry.ClassesRoot.CreateSubKey(regApplicationName);
			_rk.SetValue("filetypes", String.Join(new string(spliterString, 1), fileExtensions));
			_rk = _rk.CreateSubKey("DefaultIcon");
			_rk.SetValue("", "\"" + Process.GetCurrentProcess().MainModule.FileName + "\",1");
			_rk = Registry.ClassesRoot.CreateSubKey(regApplicationName + "\\shell\\open\\command");
			_rk.SetValue("", "\"" + Process.GetCurrentProcess().MainModule.FileName + "\" \"%1\"");
		}
		
		public static void ClearFileAssociate() {
			RegistryKey _rk = Registry.ClassesRoot.OpenSubKey(regApplicationName);
			if(_rk == null)
				return;
			String _fileTypes = _rk.GetValue("filetypes") as String;
			if(String.IsNullOrEmpty(_fileTypes))
				return;
			string[] _fileTypeArr = _fileTypes.Split(spliterString);
			foreach(String item in _fileTypeArr) {
				_rk = Registry.ClassesRoot.OpenSubKey(item, true);
				if(_rk != null && Convert.ToString(_rk.GetValue("")) == regApplicationName) {
					_rk.DeleteValue(""); //清理掉默认值
					if(_rk.GetValueNames().Length == 0 && _rk.GetSubKeyNames().Length == 0) {
						Registry.ClassesRoot.DeleteSubKey(item);
					}
				}
			}
			
			Registry.ClassesRoot.DeleteSubKeyTree(regApplicationName);
		}
	}
}
