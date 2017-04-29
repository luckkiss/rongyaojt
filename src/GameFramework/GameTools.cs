using Cross;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace GameFramework
{
	public class GameTools
	{
		public const uint CASEINSENSITIVE = 1u;

		public const uint DESCENDING = 2u;

		public const uint UNIQUESORT = 4u;

		public const uint RETURNINDEXEDARRAY = 8u;

		public const uint NUMERIC = 16u;

		private static Random _random;

		private static GameTools _t;

		public const uint ClassArray = 0u;

		public const uint ClassDictionary = 1u;

		public static GameTools inst
		{
			get
			{
				bool flag = GameTools._t == null;
				if (flag)
				{
					GameTools._t = new GameTools();
				}
				return GameTools._t;
			}
		}

		public static Random randomInst
		{
			get
			{
				bool flag = GameTools._random == null;
				if (flag)
				{
					GameTools._random = new Random();
				}
				return GameTools._random;
			}
		}

		public double pixelToUnit(double val)
		{
			return val / 53.333332061767578;
		}

		public double unitTopixel(double val)
		{
			return val * 53.333332061767578;
		}

		public static Variant array2Map(Variant ary, string keyName, uint type = 1u)
		{
			Variant variant = new Variant();
			bool flag = ary != null && ary._arr != null;
			Variant result;
			if (flag)
			{
				bool flag2 = type == 0u;
				if (flag2)
				{
					foreach (Variant current in ary._arr)
					{
						bool flag3 = current.ContainsKey(keyName);
						if (flag3)
						{
							variant[int.Parse(current[keyName]._str)] = current;
						}
					}
					result = variant;
					return result;
				}
				bool flag4 = 1u == type;
				if (flag4)
				{
					foreach (Variant current2 in ary._arr)
					{
						variant[current2[keyName]] = current2;
					}
					result = variant;
					return result;
				}
			}
			result = ary;
			return result;
		}

		public static Variant mergeSimpleObject(Variant src, Variant dest, bool clone = false, bool cuseSrc = true)
		{
			Variant variant = new Variant();
			Variant variant2 = src;
			bool flag = !cuseSrc;
			if (flag)
			{
				variant2 = dest;
			}
			if (clone)
			{
				bool isArr = src.isArr;
				if (isArr)
				{
					using (List<Variant>.Enumerator enumerator = src._arr.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string key = enumerator.Current;
							bool flag2 = dest.ContainsKey(key);
							if (flag2)
							{
								bool flag3 = (dest[key].isArr || dest[key].isDct) && (src[key].isArr || src[key].isDct);
								if (flag3)
								{
									variant[key] = GameTools.mergeSimpleObject(src[key], dest[key], false, true);
								}
								else
								{
									variant[key] = variant2[key].clone();
								}
							}
							else
							{
								variant[key] = src[key].clone();
							}
						}
					}
				}
				bool isDct = src.isDct;
				if (isDct)
				{
					foreach (string current in src.Keys)
					{
						bool flag4 = dest.ContainsKey(current);
						if (flag4)
						{
							bool flag5 = (dest[current].isArr || dest[current].isDct) && (src[current].isArr || src[current].isDct);
							if (flag5)
							{
								variant[current] = GameTools.mergeSimpleObject(src[current], dest[current], false, true);
							}
							else
							{
								variant[current] = variant2[current].clone();
							}
						}
						else
						{
							variant[current] = src[current].clone();
						}
					}
				}
				bool flag6 = dest.Count > 0;
				if (flag6)
				{
					foreach (string current2 in dest.Keys)
					{
						bool flag7 = variant.ContainsKey(current2);
						if (!flag7)
						{
							variant[current2] = dest[current2].clone();
						}
					}
				}
			}
			else
			{
				bool isDct2 = src.isDct;
				if (isDct2)
				{
					foreach (string current3 in src.Keys)
					{
						bool flag8 = dest.ContainsKey(current3);
						if (flag8)
						{
							bool flag9 = (dest[current3].isArr || dest[current3].isDct) && (src[current3].isArr || src[current3].isDct);
							if (flag9)
							{
								variant[current3] = GameTools.mergeSimpleObject(src[current3], dest[current3], false, true);
							}
							else
							{
								variant[current3] = variant2[current3];
							}
						}
						else
						{
							variant[current3] = src[current3];
						}
					}
				}
				bool flag10 = dest.Count > 0;
				if (flag10)
				{
					foreach (string current4 in dest.Keys)
					{
						bool flag11 = variant.ContainsKey(current4);
						if (!flag11)
						{
							variant[current4] = dest[current4];
						}
					}
				}
			}
			return variant;
		}

		public static Variant split(string str, string substr, uint type = 1u)
		{
			Variant variant = new Variant();
			string[] separator = new string[]
			{
				substr
			};
			string[] array = str.Split(separator, StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (type != 0u)
				{
					if (type == 1u)
					{
						variant._arr.Add(text);
					}
				}
				else
				{
					bool flag = text == "";
					if (flag)
					{
						variant._arr.Add(0);
					}
					else
					{
						variant._arr.Add(int.Parse(text));
					}
				}
			}
			return variant;
		}

		public static void PrintCrash(string msg)
		{
			DebugTrace.add(Define.DebugTrace.DTT_ERR, " < =========================== Crash  =========================== > \n" + msg);
		}

		public static void PrintError(string msg)
		{
			DebugTrace.add(Define.DebugTrace.DTT_ERR, " < =========================== ERROR  =========================== > \n" + msg);
		}

		public static void PrintDetial(string msg)
		{
			DebugTrace.add(Define.DebugTrace.DTT_DTL, msg);
		}

		public static void PrintSystem(string msg)
		{
			DebugTrace.add(Define.DebugTrace.DTT_SYS, " < =========================== SYSTEM  =========================== > \n" + msg);
		}

		public static void PrintNotice(string msg)
		{
			DebugTrace.add(Define.DebugTrace.DTT_SYS, " < =========================== Notice  =========================== > \n" + msg);
		}

		public static void PrintProfile(string msg)
		{
			DebugTrace.add(Define.DebugTrace.DTT_SYS, " < =========================== Profile  =========================== > \n" + msg);
		}

		public static Variant GetTmchkAbs(string date)
		{
			bool flag = "" == date || date == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = GameTools.split(date, " ", 1u);
				Variant variant2 = GameTools.split(variant[0], "-", 1u);
				Variant variant3 = GameTools.split(variant[1], ":", 1u);
				Variant variant4 = new Variant();
				variant4["y"] = variant2[0];
				variant4["mon"] = variant2[1];
				variant4["d"] = variant2[2];
				variant4["h"] = variant3[0];
				variant4["min"] = variant3[1];
				variant4["s"] = variant3[2];
				result = variant4;
			}
			return result;
		}

		public static string GetMethodInfo(Variant data = null)
		{
			string str = "";
			StackTrace stackTrace = new StackTrace(true);
			MethodBase method = stackTrace.GetFrame(1).GetMethod();
			str = str + method.DeclaringType.FullName + ".";
			str = str + method.Name + ":";
			bool flag = data != null;
			if (flag)
			{
				str += data.dump();
			}
			return str + "\n";
		}

		public static Variant CreateSwitchData(string caseStr, Variant data)
		{
			return GameTools.createGroup(new Variant[]
			{
				"case",
				caseStr,
				"data",
				data
			});
		}

		public static Variant createGroup(params Variant[] vals)
		{
			Variant variant = new Variant();
			for (int i = 0; i < vals.Length; i += 2)
			{
				bool flag = vals[i] != null && vals[i + 1] != null;
				if (flag)
				{
					variant[vals[i]._str] = vals[i + 1];
				}
			}
			return variant;
		}

		public static Variant createGroup(params object[] vals)
		{
			Variant variant = new Variant();
			for (int i = 0; i < vals.Length; i += 2)
			{
				bool flag = vals[i] != null && vals[i + 1] != null;
				if (flag)
				{
					variant[(string)vals[i]] = new Variant();
					variant[(string)vals[i]]._val = vals[i + 1];
				}
			}
			return variant;
		}

		public static Variant createArray(params Variant[] vals)
		{
			Variant variant = new Variant();
			for (int i = 0; i < vals.Length; i++)
			{
				variant.pushBack(vals[i]);
			}
			return variant;
		}

		public static long getTimer()
		{
			return CCTime.getCurTimestampMS();
		}

		public static void assignProp(Variant obj, Variant prop)
		{
			foreach (string current in prop.Keys)
			{
				bool flag = !obj.ContainsKey(current);
				if (!flag)
				{
					obj[current] = prop[current];
				}
			}
		}

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
			return GameTools.GetFinFormatStr("");
		}

		public static string GetSuperHtmlStr(string str, string format, string clkFun = null, string clkPar = null)
		{
			string str2 = GameTools.FormatStr(str, format, clkFun, clkPar, "");
			return GameTools.GetFinFormatStr(str2);
		}

		public static int get_day(int sec)
		{
			return sec / 86400;
		}

		public static T pop<T>(List<T> arr)
		{
			bool flag = arr == null || arr.Count <= 0;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				int index = arr.Count - 1;
				T t = arr[index];
				arr.RemoveAt(index);
				result = t;
			}
			return result;
		}

		public static Comparison<Variant> sortFun(string key, uint option = 16u)
		{
			return delegate(Variant left, Variant right)
			{
				int num = 0;
				int num2 = -1;
				bool flag = left.ContainsKey(key) && right.ContainsKey(key);
				if (flag)
				{
					bool flag2 = left[key].isStr && !int.TryParse(left[key]._str, out num2);
					if (flag2)
					{
						num = left[key]._str.CompareTo(right[key]);
					}
					else
					{
						bool flag3 = left[key] > right[key];
						if (flag3)
						{
							num = 1;
						}
						else
						{
							bool flag4 = left[key] == right[key];
							if (flag4)
							{
								num = 0;
							}
							else
							{
								num = -1;
							}
						}
					}
				}
				else
				{
					bool flag5 = !left.ContainsKey(key) && !right.ContainsKey(key);
					if (flag5)
					{
						num = 0;
					}
					bool flag6 = !left.ContainsKey(key);
					if (flag6)
					{
						num = -1;
					}
					bool flag7 = !right.ContainsKey(key);
					if (flag7)
					{
						num = 1;
					}
				}
				bool flag8 = option == 2u;
				if (flag8)
				{
					num = -num;
				}
				return num;
			};
		}

		public static void Sort(Variant data, string key, uint option = 16u)
		{
			data._arr.Sort(GameTools.sortFun(key, option));
		}

		public static void Sort(Variant data, List<Variant> keys = null, List<Variant> options = null)
		{
			bool flag = keys == null;
			if (flag)
			{
				data._arr.Sort();
			}
			else
			{
				data._arr.Sort((Variant left, Variant right) => GameTools.sortConparison(left, right, keys, options));
			}
		}

		private static int sortConparison(Variant left, Variant right, List<Variant> keys, List<Variant> opts)
		{
			int num = 0;
			bool flag = keys != null && keys.Count > 0;
			if (flag)
			{
				int num2 = -1;
				bool flag2 = left.ContainsKey(keys[0]._str) && right.ContainsKey(keys[0]._str);
				if (flag2)
				{
					bool flag3 = left[keys[0]].isStr && !int.TryParse(left[keys[0]]._str, out num2);
					if (flag3)
					{
						num = left[keys[0]]._str.CompareTo(right[keys[0]]);
					}
					else
					{
						bool flag4 = left[keys[0]] > right[keys[0]];
						if (flag4)
						{
							num = 1;
						}
						else
						{
							bool flag5 = left[keys[0]] == right[keys[0]];
							if (flag5)
							{
								num = 0;
							}
							else
							{
								num = -1;
							}
						}
					}
				}
				else
				{
					bool flag6 = !left.ContainsKey(keys[0]._str) && !right.ContainsKey(keys[0]._str);
					if (flag6)
					{
						num = 0;
					}
					bool flag7 = !left.ContainsKey(keys[0]._str);
					if (flag7)
					{
						num = -1;
					}
					bool flag8 = !right.ContainsKey(keys[0]._str);
					if (flag8)
					{
						num = 1;
					}
				}
				bool flag9 = num == 0;
				if (flag9)
				{
					List<Variant> opts2 = null;
					bool flag10 = opts != null && opts.Count > 1;
					if (flag10)
					{
						opts2 = opts.GetRange(1, opts.Count - 1);
					}
					num = GameTools.sortConparison(left, right, keys.GetRange(1, keys.Count - 1), opts2);
				}
			}
			bool flag11 = opts != null && opts.Count > 0;
			if (flag11)
			{
				uint @uint = opts[0]._uint;
				if (@uint == 2u)
				{
					num = -num;
				}
			}
			return num;
		}

		public static string get_time_ydms(uint sec, bool symbol = false)
		{
			uint num = sec / 86400u;
			uint num2 = (sec - num * 86400u) / 3600u;
			uint num3 = (sec - num * 86400u - num2 * 3600u) / 60u;
			uint num4 = sec - num * 86400u - num2 * 3600u - num3 * 60u;
			string text = "";
			string str = ":";
			string str2 = ":";
			string str3 = ":";
			string str4 = "";
			bool flag = !symbol;
			if (flag)
			{
				str = LanguagePack.getLanguageText("time", "day");
				str2 = LanguagePack.getLanguageText("time", "hour");
				str3 = LanguagePack.getLanguageText("time", "min");
				str4 = LanguagePack.getLanguageText("time", "sec");
			}
			bool flag2 = num > 0u;
			if (flag2)
			{
				text = text + num.ToString() + str;
			}
			bool flag3 = num2 > 0u;
			if (flag3)
			{
				text = text + num2.ToString() + str2;
			}
			bool flag4 = num3 > 0u;
			if (flag4)
			{
				text = text + num3.ToString() + str3;
			}
			bool flag5 = num4 > 0u;
			if (flag5)
			{
				text = text + num4.ToString() + str4;
			}
			return text;
		}

		public static Variant getByKeyVal(List<Variant> list, string key, uint val)
		{
			Variant result;
			for (int i = 0; i < list.Count; i++)
			{
				Variant variant = list[i];
				bool flag = variant[key] == val;
				if (flag)
				{
					result = variant;
					return result;
				}
			}
			result = null;
			return result;
		}

		public static Variant getByKeyVal(List<Variant> list, string key, int val)
		{
			Variant result;
			for (int i = 0; i < list.Count; i++)
			{
				Variant variant = list[i];
				bool flag = variant[key] == val;
				if (flag)
				{
					result = variant;
					return result;
				}
			}
			result = null;
			return result;
		}

		public static Variant getByKeyVal(List<Variant> list, string key, string val)
		{
			Variant result;
			for (int i = 0; i < list.Count; i++)
			{
				Variant variant = list[i];
				bool flag = variant[key] == val;
				if (flag)
				{
					result = variant;
					return result;
				}
			}
			result = null;
			return result;
		}

		public static Variant shift(ref Variant arr)
		{
			Variant result = arr[0];
			arr._arr.RemoveAt(0);
			return result;
		}

		public static void unshift(ref Variant arr, Variant obj)
		{
			Variant variant = new Variant();
			variant._arr.Add(obj);
			variant._arr.AddRange(arr._arr);
			arr = variant;
		}

		public static int random(int a, int b)
		{
			return a + GameTools.randomInst.Next(0, b - a);
		}

		public static int random(int a)
		{
			return GameTools.randomInst.Next(0, a);
		}
	}
}
