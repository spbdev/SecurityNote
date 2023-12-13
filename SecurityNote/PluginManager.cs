/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 16:57
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of PluginManager.
	/// </summary>
	public class PluginManager : IPlugin
	{
		public PluginManager()
		{
		}
		
		private List<IPlugin> plugins = new List<IPlugin>();
		public List<IPlugin> Plugins {
			get{
				return plugins;
			}
		}
		
		public int LoadPlugins(string pluginDir) {
			Assembly _assembly;
			Type[] _types;
			IPlugin _plugin;
			Type _iPluginType = typeof(IPlugin);
			if(Directory.Exists(pluginDir)) {
				string[] _files = Directory.GetFiles(pluginDir, "*.dll", SearchOption.TopDirectoryOnly);
				foreach(string f in _files) {
					try {
						_assembly = Assembly.LoadFile(f);
						_types = _assembly.GetTypes();
						foreach (Type t in _types) {
							if (t.IsClass && t.IsPublic && !t.IsAbstract && _iPluginType.IsAssignableFrom(t)) {
								_plugin = (IPlugin)Activator.CreateInstance(t);
								plugins.Add(_plugin);
							}
						}
					} catch (Exception) {
					}
				}
			}
			return plugins.Count;
		}
		
		
		#region IPlugin implementation
		public void OnApplicationStart(object owner, System.Windows.Forms.MenuStrip menu)
		{
			foreach(IPlugin p in plugins) {
				p.OnApplicationStart(owner, menu);
			}
		}
		public void OnApplicationClosing()
		{
			foreach(IPlugin p in plugins) {
				p.OnApplicationClosing();
			}
		}
		public void OnFileLoad(string filepath)
		{
			foreach(IPlugin p in plugins) {
				p.OnFileLoad(filepath);
			}
		}
		public void OnContentChanged(object textEditor, string content)
		{
			foreach(IPlugin p in plugins) {
				p.OnContentChanged(textEditor, content);
			}
		}
		public string Name {
			get {
				return this.GetType().Name;
			}
		}
		public string Description {
			get {
				return this.Name;
			}
		}
		#endregion
	}
}
