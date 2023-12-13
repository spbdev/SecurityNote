/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/14
 * 时间: 16:31
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.ThreadException += Application_ThreadException;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(args));
		}
		
		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
			try{
				using(StreamWriter sw = new StreamWriter(Path.Combine(ApplicationConfig.ConfigDirectory, "error.log"), true, Encoding.Default)) {
					sw.WriteLine(String.Format("{0:yyyy-MM-dd HH:mm:ss} {1}", DateTime.Now, e.Exception.Message));
					sw.WriteLine(e.Exception.StackTrace);
				}
			}
			catch {
				//Do Nothing
			}
			MessageBox.Show(String.Format("发生未处理的异常：{0}", e.Exception.Message), "异常");
		}
	}
}
