/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/14
 * 时间: 17:10
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using System.Security.Cryptography;
using Com.Spbdev.SecurityNote.Utils;

namespace Com.Spbdev.SecurityNote.Model
{
	/// <summary>
	/// 文件结构对象
	/// </summary>
	public class FileItem
	{
		public FileItem()
		{
		}
		
		/// <summary>
		/// 文件对象
		/// </summary>
		private FileInfo file;
		public FileInfo File {
			get{
				return file;
			}
		}
		/// <summary>
		/// 是否编辑后需要保存
		/// </summary>
		public bool Edited = false;
		
		private SecurityDataHandler dataHandler;
		
		private byte[] password;
		public byte[] Password{
			get{
				return password;
			}
			set{
				if(password != value) {
					password = value;
					dataHandler = (password == null ? null : new SecurityDataHandler(password));
				}
			}
		}
		
		/// <summary>
		/// 文件条目内容
		/// </summary>
		public NoteEntity FileEntity = new NoteEntity();
		
		/// <summary>
		/// 解码后的文本内容
		/// </summary>
		public String Content;
		
		public void Load(FileInfo file) {
			this.file = file;
			using(FileStream fs = this.file.Open(FileMode.Open, FileAccess.Read)) {
				this.FileEntity.Load(fs);
				try{
					byte[] _decryptedData = dataHandler.Decrypt(this.FileEntity.Data);
					byte[] _crcData = ByteUtils.ToBytes((uint)CRCUtils.CRC(_decryptedData, ApplicationConfig.CrcPoly));
					if(!ByteUtils.Equals(this.FileEntity.CRC, _crcData)) {
						this.Reset();
						throw new SecurityNoteException("密码不正确或文档损坏!");
					}
					this.Content = ApplicationConfig.FileEncoding.GetString(_decryptedData);
				}
				catch(CryptographicException ce) {
					throw new SecurityNoteException("密码不正确!", ce);
				}
			}
		}
		
		public void Reset() {
			this.Content = null;
			this.file = null;
			this.Password = null;
		}
		
		public void Save(FileInfo file) {
			if(file != null) {
				this.file = file;
			}
			using(FileStream fs = this.file.Open(FileMode.Create, FileAccess.Write)) {
				byte[] _rawData = ApplicationConfig.FileEncoding.GetBytes(this.Content);
				this.FileEntity.CRC = ByteUtils.ToBytes((uint)CRCUtils.CRC(_rawData, ApplicationConfig.CrcPoly));
				this.FileEntity.Data = dataHandler.Encrypt(_rawData);
				FileEntity.Save(fs);
			}
			this.Edited = false;
		}
		
		public void Save() {
			Save(file);
		}
	}
}
