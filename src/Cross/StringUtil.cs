using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Cross
{
	public class StringUtil
	{
		public static Variant FormatStringType(string val)
		{
			bool success = Regex.Match(val, "^-?\\d+$").Success;
			Variant result;
			if (success)
			{
				result = new Variant(int.Parse(val));
			}
			else
			{
				bool success2 = Regex.Match(val, "^(-?\\d+)(\\.\\d+)?$").Success;
				if (success2)
				{
					result = new Variant(float.Parse(val));
				}
				else
				{
					bool success3 = Regex.Match(val, "^0[xX][A-Fa-f0-9]+$").Success;
					if (success3)
					{
						result = new Variant(Convert.ToInt32(val, 16));
					}
					else
					{
						bool flag = val == "true";
						if (flag)
						{
							result = new Variant(true);
						}
						else
						{
							bool flag2 = val == "false";
							if (flag2)
							{
								result = new Variant(false);
							}
							else
							{
								result = new Variant(val);
							}
						}
					}
				}
			}
			return result;
		}

		public static string make_version(uint ver)
		{
			return string.Concat(new object[]
			{
				ver >> 24,
				".",
				ver >> 16 & 255u,
				".",
				ver >> 8 & 255u,
				".",
				ver & 255u
			});
		}

		public static List<string> splitStr(string source, string semi)
		{
			List<string> list = new List<string>();
			int num = source.IndexOf(semi);
			bool flag = num >= 0;
			if (flag)
			{
				list.Insert(list.Count, source.Substring(0, num));
				list.Insert(list.Count, source.Substring(num + 1));
			}
			else
			{
				list.Insert(list.Count, source);
			}
			return list;
		}

		public static string read_NTSTR(ByteArray d, string cp)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.position = 0;
			sbyte b = d.readByte();
			while (b != 0 && d.bytesAvailable > 0)
			{
				byteArray.writeByte(b);
				b = d.readByte();
			}
			byteArray.position = 0;
			string result = byteArray.readUTF8Bytes(byteArray.length);
			byteArray.length = 0;
			return result;
		}

		public static string trim(string str)
		{
			bool flag = str == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				int num = 0;
				while (StringUtil.isWhitespace(StringUtil.getStrBool(str, num)))
				{
					num++;
				}
				int num2 = str.Length - 1;
				while (StringUtil.isWhitespace(StringUtil.getStrBool(str, num2)))
				{
					num2--;
				}
				bool flag2 = num2 >= num;
				if (flag2)
				{
					result = str.Substring(num + 1, num2 + 1 - num);
				}
				else
				{
					result = "";
				}
			}
			return result;
		}

		protected static string getStrBool(string str, int index)
		{
			string text = str.Substring(index, 1);
			bool flag = text == "\\";
			if (flag)
			{
				text = str.Substring(index, 2);
			}
			return text;
		}

		public static bool isWhitespace(string character)
		{
			return character == " " || character == "\t" || character == "\r" || character == "\n" || character == "\f";
		}
	}
}
