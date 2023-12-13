/*
 * 由SharpDevelop创建。
 * 用户: niu
 * 日期: 2019/9/9
 * 时间: 21:16
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;

namespace Com.Spbdev.SecurityNote.Utils
{
	/// <summary>
	/// Description of StringUtils.
	/// </summary>
	public static class StringUtils
	{
		/// <summary>
		/// 返回为用于显示的、短路径，路径中间部分用省略号代替
		/// </summary>
		/// <param name="path"></param>
		/// <param name="maxLength"></param>
		/// <returns></returns>
		public static string ToDisplayPath(String path, int maxLength)
		{
			int _left; //path左侧多少个字符用于显示
			int _leftIdx; //对path按\符号拆分成段后，path左侧用于显示的段的最大索引
			int _right; //path右侧（末尾）多少个字符用于显示，类似_left
			int _rightIdx; //对path按\符号拆分成段后，path右侧用于显示的段的最小索引
			
			if(maxLength < 10) {
				throw new ArgumentOutOfRangeException();
			}
			if(String.IsNullOrEmpty(path) || path.Length <= maxLength)
				return path;
			
			String[] _parts = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
			if(_parts.Length < 2) {
				return "";
			}
			
			_leftIdx = 0;
			_rightIdx = _parts.Length - 1;
			_left = _parts[_leftIdx].Length + 1;
			_right = _parts[_rightIdx].Length + 1;
			while(_left + _right < maxLength && _leftIdx < _rightIdx - 1) {
				if(_left < _right) {
					_leftIdx ++;
					_left += _parts[_leftIdx].Length + 1;
				}
				else {
					_rightIdx--;
					_right += _parts[_rightIdx].Length + 1;
				}
			}
			if(_left + _right > maxLength) {
				if(maxLength - _left < _left) { //裁切right后，right的长度小于left，就裁切left
					_left = Math.Max(4, maxLength - _right);
				}
				if(_left + _right > maxLength) {
					_right = Math.Max(4, maxLength - _left);
				}
			}
			
			return path.Substring(0, _left) + " ... " + path.Substring(path.Length - _right);
		}
	}
}
