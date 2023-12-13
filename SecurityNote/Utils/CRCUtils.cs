/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2020/4/16
 * 时间: 14:08
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace Com.Spbdev.SecurityNote.Utils
{
	/// <summary>
	/// Description of CRCUtils.
	/// </summary>
	public static class CRCUtils
	{
		/// <summary>
		/// 根据多项式自动计算位宽，并按位宽生成二进制位全1的初始值和标志位
		/// </summary>
		/// <param name="data"></param>
		/// <param name="ulPoly">多项式 POLY</param>
		/// <returns></returns>
		public static ulong CRC(byte[] data, ulong ulPoly) {
			return CRC(data, ulPoly, ((int)Math.Log(ulPoly, 2) / 8 + 1) * 8);
		}
		
		/// <summary>
		/// 根据多项式、位宽计算CRC，初始值二进制位全为1，结果异或值取0。
		/// </summary>
		/// <param name="data"></param>
		/// <param name="ulPoly">多项式 POLY</param>
		/// <param name="width">位宽，一般取值8、16、32</param>
		/// <returns></returns>
		public static ulong CRC(byte[] data, ulong ulPoly, int width) {
			return CRC(data, ulPoly, width, 0);
		}
		
		/// <summary>
		/// 根据多项式、位宽、结果异或值计算CRC，其中初始值根据位宽生成，二进制位都是1。
		/// </summary>
		/// <param name="data"></param>
		/// <param name="ulPoly"></param>
		/// <param name="width"></param>
		/// <param name="ulXorOut"></param>
		/// <returns></returns>
		public static ulong CRC(byte[] data, ulong ulPoly, int width, ulong ulXorOut) {
			ulong _init = (ulong)Math.Pow(2, width) - 1;
			return CRC(data, ulPoly, _init, ulXorOut, _init);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data">要进行CRC计算的数据</param>
		/// <param name="ulPoly">多项式 POLY</param>
		/// <param name="ulInit">初始值 INIT</param>
		/// <param name="ulXorOut">结果异或值 XOROUT</param>
		/// <param name="ulMask">标志位，通常是所有二进制位都是1</param>
		/// <returns></returns>
		public static ulong CRC(byte[] data, ulong ulPoly, ulong ulInit, ulong ulXorOut, ulong ulMask)
		{
			ulong crc = ulInit;
			byte ucByte;
			int blk_len = data.Length;
			int i;
			bool iTopBitCRC;
			bool iTopBitByte;
			ulong ulTopBit;
			if (ulMask > 0xffff)
				ulTopBit = 0x80000000;
			else
				ulTopBit = ((ulMask + 1) >> 1);
			
			for (int j = 0; j < blk_len; j++)
			{
				ucByte = data[j];
				for (i = 0; i < 8; i++)
				{
					iTopBitCRC = (crc & ulTopBit) != 0;
					iTopBitByte = (ucByte & 0x80) != 0;
					if (iTopBitCRC != iTopBitByte)
					{
						crc = (crc << 1) ^ ulPoly;
					}
					else
					{
						crc = (crc << 1);
					}
					unchecked {
						ucByte <<= 1;
					}
				}
			}
			return (ulong)((crc ^ ulXorOut) & ulMask);
		}
	}
}
