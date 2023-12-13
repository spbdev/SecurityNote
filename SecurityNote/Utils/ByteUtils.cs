/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/16
 * 时间: 15:57
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace Com.Spbdev.SecurityNote.Utils
{
	/// <summary>
	/// Description of ByteUtils.
	/// </summary>
	public static class ByteUtils
	{
		public static byte[] ToBytes(uint value) {
			byte[] _buffer = BitConverter.GetBytes(value);
			if(BitConverter.IsLittleEndian) {
				Array.Reverse(_buffer);
			}
			return _buffer;
		}
		
		public static bool Equals(byte[] a, byte[] b) {
			if(a == b)
				return true;
			if(a == null || b == null)
				return false;
			if(a.Length != b.Length)
				return false;
			for(int k=0;k<a.Length;k++) {
				if(a[k] != b[k])
					return false;
			}
			return true;
		}
	}
}
