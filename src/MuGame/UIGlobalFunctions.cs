using System;

namespace MuGame
{
	public class UIGlobalFunctions
	{
		public static string FormatStr(string str, string format, string clkFun = null, string clkPar = null, string style = "")
		{
			bool flag = str == "";
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = "";
				string text2 = (style == "") ? "" : (" style=\"" + style + "\"");
				bool flag2 = clkFun == null && clkPar == null;
				if (flag2)
				{
					text = string.Concat(new string[]
					{
						text,
						"<txt text=\"",
						str,
						"\" format=\"",
						format,
						"\"",
						text2,
						"/>"
					});
				}
				else
				{
					bool flag3 = clkFun != null && clkPar == null;
					if (flag3)
					{
						text = string.Concat(new string[]
						{
							text,
							"<txt text=\"",
							str,
							"\" format=\"",
							format,
							"\"",
							text2,
							" onclick=\"",
							clkFun,
							"\"/>"
						});
					}
					else
					{
						bool flag4 = clkFun != null && clkPar != null;
						if (flag4)
						{
							text = string.Concat(new string[]
							{
								text,
								"<txt text=\"",
								str,
								"\" format=\"",
								format,
								"\"",
								text2,
								" onclick=\"",
								clkFun,
								"\" clickpar=\"",
								clkPar,
								"\"/>"
							});
						}
					}
				}
				result = text;
			}
			return result;
		}

		public static string GetFinFormatStr(string str)
		{
			string str2 = "<supertext>";
			str2 += str;
			return str2 + "</supertext>";
		}

		public static string clearSuperTxtStr()
		{
			return UIGlobalFunctions.GetFinFormatStr("");
		}

		public static string GetSuperHtmlStr(string str, string format, string clkFun = null, string clkPar = null)
		{
			string str2 = UIGlobalFunctions.FormatStr(str, format, clkFun, clkPar, "");
			return UIGlobalFunctions.GetFinFormatStr(str2);
		}
	}
}
