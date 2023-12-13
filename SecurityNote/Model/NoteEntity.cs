/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/15
 * 时间: 11:25
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using System.Linq;

namespace Com.Spbdev.SecurityNote.Model
{
	/// <summary>
	/// Description of FileEntity.
	/// </summary>
	public class NoteEntity
	{
		public NoteEntity()
		{
		}
		
		public static readonly byte[] FileTypeTag = new byte[]{0xBF, 0xD3, 0xD4, 0xD8, 0x54};
		public static readonly byte[] FileVersionTag = new byte[]{0x02};
		public static readonly byte[] CRCValueTag = new byte[]{0x02};
		public static readonly byte[] FileDataTag = new byte[]{0x04};
		
		private int fileVersion = 1;
		public int FileVersion{
			get{
				return fileVersion;
			}
			set{
				if(value < 0)
					throw new ArgumentOutOfRangeException();
				fileVersion = value;
			}
		}
		
		public byte[] CRC;
		/// <summary>
		/// 文件数据
		/// </summary>
		public byte[] Data;
		
		public void Load(Stream stream) {
			ReadTag(stream, FileTypeTag);
			//总的数据长度
			int _totalLength = ReadLength(stream);
			_totalLength += (int)stream.Position;
			if(stream.CanSeek && stream.Length < _totalLength) {
				throw new SecurityNoteException("Bad file length");
			}
			//读取文件格式版本号
			fileVersion = ReadVersion(stream);
			if(fileVersion > ApplicationConfig.FileVersion) {
				throw new SecurityNoteException("Bad file version");
			}
			//读取加密前的数据CRC值
			this.CRC = ReadCrc(stream);
			//读取加密数据
			this.Data = ReadData(stream);
			if(stream.Position != _totalLength) {
				throw new SecurityNoteException("Bad file length");
			}
		}
		
		public void Save(Stream stream) {
			int _totalDataLen = Data.Length; //数据部分的字节数
			//加上数据TLV部分的Length字节数
			_totalDataLen += CalculateLengthByteCount(Data.Length);
			//加上数据部分的Tag字节数
			_totalDataLen += FileDataTag.Length;
			
			//加上Version的Value部分的字节数，占用4字节即可
			_totalDataLen += 4;
			//加上Version的Length字节数
			_totalDataLen += 1;
			//加上Version的Tag字节数
			_totalDataLen += FileVersionTag.Length;
			
			//加上CRC的字节数，占用4直接
			_totalDataLen += 4;
			//加上CRC的Length字节数
			_totalDataLen += 1;
			//加上CRC的Tag字节数
			_totalDataLen += CRCValueTag.Length;
			
			//写入tag
			stream.Write(FileTypeTag, 0, FileTypeTag.Length);
			//写入length
			WriteLength(stream, _totalDataLen);
			
			//写入Version
			stream.Write(FileVersionTag, 0, FileVersionTag.Length);
			WriteLength(stream, 4);
			WriteInt32(stream, fileVersion);
			
			//写入CRC
			stream.Write(CRCValueTag, 0, CRCValueTag.Length);
			WriteLength(stream, CRC.Length);
			stream.Write(CRC, 0, CRC.Length);
			
			//写入Data
			stream.Write(FileDataTag, 0, FileDataTag.Length);
			WriteLength(stream, Data.Length);
			stream.Write(Data, 0, Data.Length);
		}
		
		private int CalculateLengthByteCount(int length) {
			int _len = length;
			int _byteCount = 1;
			if(_len <= 127) {
				return _byteCount;
			}
			//return 1 + (int)Math.Floor(Math.Log(length, 256)) + 1; //对数方式计算
			_byteCount++;
			while((_len = (_len >> 8)) > 0) { //右移方式计算
				_byteCount++;
			}
			return _byteCount;
		}
		
		private byte[] ReadBytes(Stream stream, int count) {
			byte[] _data = new byte[count];
			if(count > 0) {
				int _readCount = stream.Read(_data, 0, count);
				if(_readCount < count) {
					throw new SecurityNoteException("End of stream");
				}
			}
			return _data;
		}
		
		private byte[] ReadCrc(Stream stream) {
			ReadTag(stream, CRCValueTag);
			int _len = ReadLength(stream);
			return ReadBytes(stream, _len);
		}
		
		private byte[] ReadData(Stream stream) {
			ReadTag(stream, FileDataTag);
			int _len = ReadLength(stream);
			return ReadBytes(stream, _len);
		}
		
		private int ReadLength(Stream stream) {
			int _byteVal = stream.ReadByte();
			if(_byteVal == -1) {
				throw new EndOfStreamException();
			}
			if(_byteVal < 0x80) { //单字节长度
				return _byteVal;
			}
			int _bCount = _byteVal - 0x80; //需要继续读取的字节数
			if(_bCount > 4) { //最多4字节用于表达长度，可支持2GB的文件
				throw new SecurityNoteException("Bad file format");
			}
			return ReadNumber(stream, _bCount);
		}
		
		private int ReadNumber(Stream stream, int byteCount) {
			int _result = 0;
			int _byteVal;
			for(int k = byteCount; k > 0; k--) {
				_byteVal = stream.ReadByte();
				if(_byteVal == -1) {
					throw new EndOfStreamException();
				}
				_result = (_result | (_byteVal << ((k - 1) * 8)));
			}
			return _result;
		}
		
		private void ReadTag(Stream stream, byte[] header) {
			int _byteVal;
			for(int k=0;k<header.Length;k++) {
				_byteVal = stream.ReadByte();
				if(_byteVal == -1) {
					throw new EndOfStreamException();
				}
				if(header[k] != _byteVal) {
					throw new SecurityNoteException("Bad file format");
				}
			}
		}
		
		/// <summary>
		/// 读取文件版本号
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		private int ReadVersion(Stream stream) {
			ReadTag(stream, FileVersionTag);
			int _len = ReadLength(stream);
			return ReadNumber(stream, _len);
		}
		
		private void WriteInt32(Stream stream, int value) {
			for(int k = 0; k < 4; k++) {
				stream.WriteByte(unchecked((byte)(value >> ((3 - k) * 8))));
			}
		}
		
		private void WriteLength(Stream stream, int length) {
			int _byteCount = CalculateLengthByteCount(length);
			if(_byteCount == 1) {
				stream.WriteByte((byte)length);
			}
			else {
				stream.WriteByte(unchecked((byte)(0x80 | (_byteCount - 1))));
				for(int k = _byteCount - 2; k >= 0; k--) {
					stream.WriteByte(unchecked((byte)(length >> (k * 8))));
				}
			}
		}
	}
}
