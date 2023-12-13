/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/24
 * 时间: 9:27
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace Com.Spbdev.SecurityNote.TotpAuthenticator.Utils
{
	/// <summary>
	/// Description of KeyValueParser.
	/// </summary>
	public static class KeyValueParser
	{
		public const string KeyValueSeparator = ": ";
		public static readonly string[] CommentPrefixes = new string[]{"#", "//"};
		
		public static Dictionary<String, String> Parse(string text, bool throwOnBadFormart) {
			Dictionary<String, String> _result = new Dictionary<string, string>();
			Parse(text, throwOnBadFormart, _result);
			return _result;
		}
		
		public static void Parse(string text, bool throwOnBadFormart, Dictionary<String, String> fillToDict) {
			if(text == null)
				throw new ArgumentNullException("text");
			string _key, _value;
			bool _hasInvalidateChar;
			using(StringReader sr = new StringReader(text)) {
				String _line;
				int _idx;
				while((_line = sr.ReadLine()) != null) {
					_line = _line.Trim();
					if(_line.StartsWith("#", StringComparison.Ordinal) || _line.StartsWith("//", StringComparison.Ordinal)) {
						continue;
					}
					_idx = _line.LastIndexOf(KeyValueSeparator, StringComparison.Ordinal);
					if(_idx > 0) {
						_key = _line.Substring(0, _idx).Trim();
						_value = _line.Substring(_idx + 1).Trim();
						if(_key.Length == 0 || _value.Length == 0) {
							if(throwOnBadFormart)
								throw new ArgumentException("Argument text contains bad format line");
							continue;
						}
						_hasInvalidateChar = false;
						foreach(char c in _value) {
							if(!(c >= 'A' && c <= 'Z') && !(c >= 'a' && c <= 'z') && !(c >= '2' && c <= '7')) {
								_hasInvalidateChar = true;
								break;
							}
						}
						if(_hasInvalidateChar) {
							if(throwOnBadFormart)
								throw new ArgumentException("Argument text contains bad format line");
							continue;
						}
						fillToDict[_key] = _value;
					}
				}
			}
		}
	}
}
