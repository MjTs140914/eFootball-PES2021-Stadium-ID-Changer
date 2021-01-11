using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eFootball_PES2021_Stadium_ID_Changer
{
	internal static class ByteToReplace
	{
		public static byte[] ArrayReplace(byte[] data, string find, string replacement, Encoding enc)
		{
			return ArrayReplace(data, enc.GetBytes(find), enc.GetBytes(replacement));
		}

		public static byte[] ArrayReplace(byte[] data, byte[] find, byte[] replacement)
		{
			int matchStart = -1;
			int matchLength = 0;

			using (var mem = new System.IO.MemoryStream())
			{
				int tempVar = data.Length;
				for (var index = 0; index < tempVar; index++)
				{
					if (data[index] == find[matchLength])
					{
						if (matchLength == 0)
						{
							matchStart = index;
						}
						matchLength += 1;
						if (matchLength == find.Length)
						{
							mem.Write(replacement, 0, replacement.Length);
							matchLength = 0;
						}
					}
					else
					{
						if (matchLength > 0)
						{
							mem.Write(data, matchStart, matchLength);
							matchLength = 0;
						}
						mem.WriteByte(data[index]);
					}
				}

				if (matchLength > 0)
				{
					mem.Write(data, data.Length - matchLength, matchLength);
				}

				byte[] retVal = new byte[mem.Length];
				mem.Position = 0;
				mem.Read(retVal, 0, retVal.Length);
				return retVal;
			}
		}
	}

}