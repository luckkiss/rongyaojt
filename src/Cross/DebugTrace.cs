using System;
using System.Text.RegularExpressions;

namespace Cross
{
	public class DebugTrace
	{
		public static Action<string> print = null;

		public static Action<string> print1 = null;

		private const string BAD_VARIABLE_NUMBER = "The number of variables to be replaced and template holes don't match";

		private const string STRING_FORMATTER = "s";

		private const string FLOAT_FORMATER = "f";

		private const string INTEGER_FORMATER = "d";

		private const string OCTAL_FORMATER = "o";

		private const string HEXA_FORMATER = "x";

		private const string DATES_FORMATERS = "aAbBcDHIjmMpSUwWxXyYZ";

		private const string DATE_DAY_FORMATTER = "D";

		private const string DATE_FULLYEAR_FORMATTER = "Y";

		private const string DATE_YEAR_FORMATTER = "y";

		private const string DATE_MONTH_FORMATTER = "m";

		private const string DATE_HOUR24_FORMATTER = "H";

		private const string DATE_HOUR_FORMATTER = "I";

		private const string DATE_HOUR_AMPM_FORMATTER = "p";

		private const string DATE_MINUTES_FORMATTER = "M";

		private const string DATE_SECONDS_FORMATTER = "S";

		private const string DATE_TOLOCALE_FORMATTER = "c";

		private string version = "$Id$";

		private static void _trace(string msg)
		{
			bool flag = DebugTrace.print != null;
			if (flag)
			{
				DebugTrace.print(msg);
			}
		}

		public static void add(Define.DebugTrace type, string info)
		{
			string str = "[none]:";
			bool flag = type == Define.DebugTrace.DTT_SYS;
			if (flag)
			{
				str = "[sys]:";
			}
			else
			{
				bool flag2 = type == Define.DebugTrace.DTT_ERR;
				if (flag2)
				{
					str = "[err]:";
				}
				else
				{
					bool flag3 = type == Define.DebugTrace.DTT_DTL;
					if (flag3)
					{
						str = "[dtl]:";
					}
				}
			}
			DebugTrace._trace(str + info);
		}

		public static void dumpObj(object obj)
		{
			bool flag = obj is Variant;
			if (flag)
			{
				DebugTrace.print((obj as Variant).dump());
			}
			else
			{
				bool flag2 = obj is ByteArray;
				if (flag2)
				{
					DebugTrace.print((obj as ByteArray).dump());
				}
			}
		}

		public static string Printf(string raw, params string[] rest)
		{
			string text = "";
			Regex regex = new Regex("%s");
			for (int i = 0; i < rest.Length; i++)
			{
				text = regex.Replace(raw, rest[i], 1);
				raw = text;
			}
			return text;
		}

		private static string padString(string str, int paddingNum, string paddingChar = " ")
		{
			bool flag = paddingChar == null;
			string result;
			if (flag)
			{
				result = str;
			}
			else
			{
				Variant variant = new Variant();
				for (int i = 0; i < Math.Abs(paddingNum) - str.Length; i++)
				{
					variant._arr.Add(paddingChar);
				}
				bool flag2 = paddingNum < 0;
				if (flag2)
				{
					variant._arr.Insert(0, str);
				}
				else
				{
					variant._arr.Add(str);
				}
				result = "";
			}
			return result;
		}

		private static double truncateNumber(double raw, int decimals = 2)
		{
			Variant val = Math.Pow(10.0, (double)decimals);
			return Math.Round(raw * val) / val;
		}
	}
}
