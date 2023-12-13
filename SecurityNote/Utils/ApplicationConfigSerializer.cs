/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/17
 * 时间: 11:20
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Windows.Forms;

namespace Com.Spbdev.SecurityNote.Utils
{
	/// <summary>
	/// Description of ApplicationConfigSerializer.
	/// </summary>
	public class ApplicationConfigSerializer
	{
		public ApplicationConfigSerializer(Form owner)
		{
			this.owner = owner;
			configFileName = System.IO.Path.Combine(ApplicationConfig.ConfigDirectory, Application.ProductName + ".cfg");
		}
		
		private readonly Form owner;
		private readonly String configFileName;
		private ApplicationConfig applicationConfig;
		public ApplicationConfig ApplicationConfig{
			get{
				return applicationConfig;
			}
		}
		
		public void LoadConfig() {
			System.IO.FileStream _oStream = null;
			try
			{
				_oStream = new System.IO.FileStream(configFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
				System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				applicationConfig = formatter.Deserialize(_oStream) as ApplicationConfig;
			}
			catch {
				applicationConfig = new ApplicationConfig();
			}
			finally {
				if(_oStream != null) {
					_oStream.Close();
					_oStream = null;
				}
				if(applicationConfig.DisplayFont == null) {
					applicationConfig.DisplayFont = SystemFonts.IconTitleFont;
				}
				if(applicationConfig.WindowSize.IsEmpty || applicationConfig.WindowSize.Width < owner.MinimumSize.Width || applicationConfig.WindowSize.Height < owner.MinimumSize.Height) {
					applicationConfig.WindowSize = owner.Size;
				}
			}
		}
		
		public void SaveConfig() {
			string _sConfigFolder = Path.GetDirectoryName(configFileName);
			if (!Directory.Exists(_sConfigFolder)) {
				Directory.CreateDirectory(_sConfigFolder);
			}

			FileStream _oStream = null;
			try {
				_oStream = new FileStream(configFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
				_oStream.SetLength(0);
				System.Runtime.Serialization.IFormatter _oFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				_oFormatter.Serialize(_oStream, applicationConfig);
			}
			catch (Exception e) {
				Debug.WriteLine(String.Format("保存配置失败：{0}", e.Message));
				//MessageBox.Show(String.Format("保存配置失败：{0}", e.Message));
			}
			finally
			{
				if (_oStream != null) {
					_oStream.Flush();
					_oStream.Close();
					_oStream = null;
				}
			}
		}
	}
}
