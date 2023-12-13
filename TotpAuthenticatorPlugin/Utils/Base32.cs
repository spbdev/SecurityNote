/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 17:16
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace Com.Spbdev.SecurityNote.TotpAuthenticator.Utils
{
	/// <summary>
	/// Description of Base32.
	/// </summary>
	public static class Base32
	{
		public static byte[] ToBytes(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException("input");
			}

			input = input.TrimEnd('=');
			int byteCount = input.Length * 5 / 8;
			byte[] returnArray = new byte[byteCount];

			byte curByte = 0, bitsRemaining = 8;
			int mask = 0, arrayIndex = 0;

			foreach (char c in input)
			{
				int cValue = CharToValue(c);

				if (bitsRemaining > 5)
				{
					mask = cValue << (bitsRemaining - 5);
					curByte = (byte)(curByte | mask);
					bitsRemaining -= 5;
				}
				else
				{
					mask = cValue >> (5 - bitsRemaining);
					curByte = (byte)(curByte | mask);
					returnArray[arrayIndex++] = curByte;
					curByte = unchecked((byte)(cValue << (3 + bitsRemaining)));
					bitsRemaining += 3;
				}
			}

			if (arrayIndex != byteCount)
			{
				returnArray[arrayIndex] = curByte;
			}

			return returnArray;
		}

		public static string ToString(byte[] input)
		{
			if (input == null || input.Length == 0)
			{
				throw new ArgumentNullException("input");
			}

			int charCount = (int)Math.Ceiling(input.Length / 5d) * 8;
			char[] returnArray = new char[charCount];

			byte nextChar = 0, bitsRemaining = 5;
			int arrayIndex = 0;

			foreach (byte b in input)
			{
				nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
				returnArray[arrayIndex++] = ValueToChar(nextChar);

				if (bitsRemaining < 4)
				{
					nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
					returnArray[arrayIndex++] = ValueToChar(nextChar);
					if(bitsRemaining == 3) {
						bitsRemaining = 5;
						nextChar = 0;
						continue;
					}
					bitsRemaining += 5;
				}

				bitsRemaining -= 3;
				nextChar = (byte)((b << bitsRemaining) & 31);
			}

			if (arrayIndex != charCount)
			{
				returnArray[arrayIndex++] = ValueToChar(nextChar);
				while (arrayIndex != charCount) returnArray[arrayIndex++] = '=';
			}

			return new string(returnArray);
		}

		private static int CharToValue(char c)
		{
			var value = (int)c;

			if (value < 91 && value > 64)
			{
				return value - 65;
			}
			if (value < 56 && value > 49)
			{
				return value - 24;
			}
			if (value < 123 && value > 96) //兼容小写字母
			{
				return value - 97;
			}

			throw new ArgumentException("Character is not a Base32 character.", "c");
		}

		/// <summary>
		/// 将值转换为可见字符，包含26个大写字母和数字2-7。
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		private static char ValueToChar(byte b)
		{
			if (b < 26)
			{
				return (char)(b + 65);
			}

			if (b < 32)
			{
				return (char)(b + 24);
			}

			throw new ArgumentException("Byte is not a Base32 value.", "b");
		}
	}
}
