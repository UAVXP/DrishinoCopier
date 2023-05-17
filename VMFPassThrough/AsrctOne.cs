using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

namespace VMFPassThrough
{
#if !PUBLIC
	class AsrctOne
	{
		internal static string MD5Checksum(string text)
		{
			if (text == "")
				return "";

			MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();

			byte[] bs = x.ComputeHash(Encoding.UTF8.GetBytes(text + saltySalt));
			StringBuilder s = new StringBuilder();
			foreach (byte b in bs)
			{
				//	s.Append(b.ToString("x2").ToLower());
				s.Append(b.ToString("x2"));
			}
			string result = s.ToString();
			return result;
		}



















































































































































































																																																																																																																																																																																			internal static string saltySalt = "m$&$lo1nupl^%0lz3l$&Oh#s@#$&";
	}
#endif
}
