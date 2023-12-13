/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/16
 * 时间: 10:16
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Security.Cryptography;

namespace Com.Spbdev.SecurityNote
{
	/// <summary>
	/// Description of SecurityDataHandler.
	/// </summary>
	public class SecurityDataHandler
	{
		public SecurityDataHandler(byte[] keys) {
			this.keys = keys;
		}
		
		private byte[] keys;
		private ICryptoTransform encryptor;
		private ICryptoTransform decryptor;
		private void Init() {
			if(encryptor == null) {
				Rijndael _aes = Rijndael.Create();
				_aes.Key = this.keys;
				_aes.IV = new byte[16];
				_aes.Mode = CipherMode.CBC;
				_aes.Padding = PaddingMode.ISO10126;
				encryptor = _aes.CreateEncryptor();
				decryptor = _aes.CreateDecryptor();
			}
		}
		
		public byte[] Encrypt(byte[] data) {
			return Encrypt(data, 0, data.Length);
		}
		public byte[] Encrypt(byte[] data, int offset, int count) {
			Init();
			return encryptor.TransformFinalBlock(data, offset, count);
		}
		
		public byte[] Decrypt(byte[] data) {
			return Decrypt(data, 0, data.Length);
		}
		public byte[] Decrypt(byte[] data, int offset, int count) {
			Init();
			return decryptor.TransformFinalBlock(data, offset, count);
		}
	}
}
