/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2019/9/9
 * 时间: 13:40
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of FileHistoryManager.
	/// </summary>
	public class FileHistoryManager
	{
		public FileHistoryManager()
		{
			configFileName = Path.Combine(ApplicationConfig.ConfigDirectory, "OpenFileHistory.cfg");
		}
		
		private readonly string configFileName;
		private List<String> all = new List<string>();
		public String[] All {
			get{
				return all.ToArray();
			}
		}
		
		private int capacity = 10;
		public int Capacity{
			get{
				return capacity;
			}
			set {
				if(capacity != value) {
					if(value < 1){
						throw new ArgumentOutOfRangeException();
					}
					capacity = value;
					EnsureCapacity();
				}
			}
		}
		public void Add(String item) {
			Remove(item);
			all.Insert(0, item);
			EnsureCapacity();
		}
		
		public void Clear() {
			all.Clear();
		}
		
		public bool Remove(String item) {
			int _idx = all.IndexOf(item);
			if(_idx < 0) {
				return false;
			}
			all.RemoveAt(_idx);
			return true;
		}
		
		
		public void LoadFromConfig() {
			String _line;
			using(FileStream _fs = new FileStream(configFileName, FileMode.OpenOrCreate, FileAccess.Read)) {
				using(StreamReader _sr = new StreamReader(_fs, Encoding.UTF8, true)) {
					while((_line = _sr.ReadLine()) != null) {
						if(!String.IsNullOrEmpty(_line)) {
							all.Add(_line);
						}
					}
				}
			}
		}
		
		public void SaveToConfig() {
			using(FileStream _fs = new FileStream(configFileName, FileMode.Create, FileAccess.Write)) {
				using(StreamWriter _sw = new StreamWriter(_fs, Encoding.UTF8)) {
					foreach(String item in all) {
						_sw.WriteLine(item);
					}
				}
			}
		}
		
		private void EnsureCapacity() {
			while(all.Count > capacity) {
				all.RemoveAt(all.Count - 1);
			}
		}
	}
}
