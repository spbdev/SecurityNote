/*
 * 由SharpDevelop创建。
 * 用户: trg
 * 日期: 2023/8/23
 * 时间: 17:15
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Security.Cryptography;

namespace Com.Spbdev.SecurityNote.TotpAuthenticator
{
	/// <summary>
	/// Description of Authenticator.
	/// </summary>
	public class Authenticator
	{
		/// <summary>
		/// 初始化验证码生成规则
		/// </summary>
		/// <param name="secret">秘钥(Base32解码后的数据)</param>
		/// <param name="duration">验证码间隔多久刷新一次（默认30秒和google同步）</param>
		public Authenticator(byte[] secret, long duration)
		{
			this.secretData = secret;
			this.DURATION_TIME = duration;
		}

		private readonly DateTime BaseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		/// <summary>
		/// 间隔时间
		/// </summary>
		private long DURATION_TIME { get; set; }

		/// <summary>
		/// 迭代次数
		/// </summary>
		private long COUNTER
		{
			get
			{
				return (long)(DateTime.UtcNow - BaseTime).TotalSeconds / DURATION_TIME;
			}
		}

		/// <summary>
		/// 秘钥
		/// </summary>
		private byte[] secretData;
		
		/// <summary>
		/// 到期秒数
		/// </summary>
		public long EXPIRE_SECONDS
		{
			get
			{
				return (DURATION_TIME - (long)(DateTime.UtcNow - BaseTime).TotalSeconds % DURATION_TIME);
			}
		}

		/// <summary>
		/// 生成认证码
		/// </summary>
		/// <returns>返回验证码</returns>
		public string GenerateCode()
		{
			return GenerateHashedCode(secretData, COUNTER);
		}

		/// <summary>
		/// 按照次数生成哈希编码
		/// </summary>
		/// <param name="secret">秘钥</param>
		/// <param name="iterationNumber">迭代次数</param>
		/// <param name="digits">生成位数</param>
		/// <returns>返回验证码</returns>
		private string GenerateHashedCode(byte[] secret, long iterationNumber, int digits = 6)
		{
			byte[] counter = BitConverter.GetBytes(iterationNumber);

			if (BitConverter.IsLittleEndian)
				Array.Reverse(counter);

			HMACSHA1 hmac = new HMACSHA1(secret, true);

			byte[] hash = hmac.ComputeHash(counter);

			int offset = hash[hash.Length - 1] & 0xf;

			int binary =
				((hash[offset] & 0x7f) << 24)
				| ((hash[offset + 1] & 0xff) << 16)
				| ((hash[offset + 2] & 0xff) << 8)
				| (hash[offset + 3] & 0xff);

			int password = binary % (int)Math.Pow(10, digits); // 6 digits

			return password.ToString(new string('0', digits));
		}
	}
}
