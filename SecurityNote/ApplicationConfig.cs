/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/14
 * 时间: 17:07
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of ApplicationConfig.
	/// </summary>
	[Serializable]
	public class ApplicationConfig : INotifyPropertyChanged
	{
		public ApplicationConfig() {
			
		}
		
		public const string ShowFileAssociateArg = "-associate";
		public const int FileVersion = 1;
		public const ulong CrcPoly = 0x1EDC6F41;
		public static readonly Encoding FileEncoding = Encoding.UTF8;
		
		private static string configDirectory;
		public static String ConfigDirectory{
			get{
				if(configDirectory == null) {
					configDirectory = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), Application.CompanyName);
					configDirectory = Path.Combine(configDirectory, Application.ProductName);
					if(!Directory.Exists(configDirectory)) {
						Directory.CreateDirectory(configDirectory);
					}
				}
				return configDirectory;
			}
		}
		
		private bool fileAssociate;
		public bool FileAssociate{
			get{
				return fileAssociate;
			}
			set {
				if(fileAssociate != value) {
					fileAssociate = value;
					OnPropertyChanged(new PropertyChangedEventArgs("FileAssociate"));
				}
			}
		}
		
		private Font displayFont;
		public Font DisplayFont{
			get {
				return displayFont;
			}
			set {
				displayFont = value;
				OnPropertyChanged(new PropertyChangedEventArgs("DisplayFont"));
			}
		}
		
		private bool wordWrap;
		public bool WordWrap{
			get {
				return wordWrap;
			}
			set {
				if(wordWrap != value) {
					wordWrap = value;
					OnPropertyChanged(new PropertyChangedEventArgs("WordWrap"));
				}
			}
		}
		
		private FormWindowState windowState;
		public FormWindowState WindowState{
			get {
				return windowState;
			}
			set {
				if(windowState != value) {
					windowState = value;
					OnPropertyChanged(new PropertyChangedEventArgs("WindowState"));
				}
			}
		}
		
		private Size windowSize;
		public Size WindowSize{
			get {
				return windowSize;
			}
			set {
				if(windowSize != value) {
					windowSize = value;
					OnPropertyChanged(new PropertyChangedEventArgs("WindowSize"));
				}
			}
		}
		
		
		#region INotifyPropertyChanged implementation
		[field:NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(PropertyChangedEventArgs e){
			if(PropertyChanged != null) {
				PropertyChanged(this, e);
			}
		}
		#endregion
	}
}
