using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GameFramework
{
	public class StringUtils
	{
		public static string unicodeToStr(string str)
		{
			string text = "";
			bool flag = !string.IsNullOrEmpty(str);
			if (flag)
			{
				for (int i = 0; i < str.Length; i++)
				{
					string text2 = str[i].ToString();
					bool flag2 = Regex.IsMatch(text2, "u") && i + 5 <= str.Length;
					if (flag2)
					{
						text2 = str.Substring(i + 1, 4);
						text += ((char)int.Parse(text2, NumberStyles.HexNumber)).ToString();
						i += 4;
					}
					else
					{
						text += str[i].ToString();
					}
				}
			}
			return text;
		}

		public static string FilterSpecial(string str)
		{
			bool flag = str == "";
			string result;
			if (flag)
			{
				result = str;
			}
			else
			{
				str = str.Replace("'", "");
				str = str.Replace("<", "");
				str = str.Replace(">", "");
				str = str.Replace("%", "");
				str = str.Replace("'delete", "");
				str = str.Replace("''", "");
				str = str.Replace("\\", "");
				str = str.Replace(",", "");
				str = str.Replace(".", "");
				str = str.Replace(">=", "");
				str = str.Replace("=<", "");
				str = str.Replace("-", "");
				str = str.Replace("_", "");
				str = str.Replace(";", "");
				str = str.Replace("||", "");
				str = str.Replace("[", "");
				str = str.Replace("]", "");
				str = str.Replace("&", "");
				str = str.Replace("#", "");
				str = str.Replace("/", "");
				str = str.Replace("-", "");
				str = str.Replace("|", "");
				str = str.Replace("?", "");
				str = str.Replace(">?", "");
				str = str.Replace("?<", "");
				str = str.Replace(" ", "");
				result = str;
			}
			return result;
		}

		public static string formatText(string str)
		{
			string[] value = str.Split(new string[]
			{
				"{n}"
			}, StringSplitOptions.None);
			str = string.Join("\n", value);
			value = str.Split(new char[]
			{
				'{'
			});
			str = string.Join("<", value);
			value = str.Split(new char[]
			{
				'}'
			});
			str = string.Join(">", value);
			return str;
		}

		public static bool isValidName(string name)
		{
			bool result = true;
			string text = "\\/:*?\"<>|!@#$%^&*()_+={}[]<>\t\0' ";
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < text.Length; i++)
				{
					bool flag2 = name.Contains(text[i].ToString());
					if (flag2)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}
	}
}
