using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace MuGame
{
	public class ContMgr
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly ContMgr.<>c <>9 = new ContMgr.<>c();

			public static Action<SXML> <>9__2_0;

			internal void <init>b__2_0(SXML x)
			{
				string text = x.getString("c");
				text = StringUtils.formatText(text);
				ContMgr.dText[x.getString("i")] = text;
			}
		}

		public static Dictionary<string, string> dText;

		public static Dictionary<string, string> dError;

		private static Dictionary<string, string> dOutGameText;

		public static void init()
		{
			ContMgr.dText = new Dictionary<string, string>();
			SXML sXML = XMLMgr.instance.GetSXML("zh", "");
			SXML arg_40_0 = sXML;
			Action<SXML> arg_40_1;
			if ((arg_40_1 = ContMgr.<>c.<>9__2_0) == null)
			{
				arg_40_1 = (ContMgr.<>c.<>9__2_0 = new Action<SXML>(ContMgr.<>c.<>9.<init>b__2_0));
			}
			arg_40_0.forEach(arg_40_1);
			ContMgr.dError = new Dictionary<string, string>();
			sXML = XMLMgr.instance.GetSXML("errorcode.l", "");
			string text = sXML.getString("c");
			text = text.Replace(" ", "");
			text = text.Replace("//", "");
			string[] array = text.Split(new char[]
			{
				'\n'
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text2 = array2[i];
				bool flag = text2 == "" || text2 == "\t";
				if (!flag)
				{
					string[] array3 = text2.Split(new char[]
					{
						','
					});
					string[] array4 = array3[0].Split(new char[]
					{
						'='
					});
					ContMgr.dError[array4[1]] = array3[1];
				}
			}
		}

		public static void initOutGame()
		{
			ContMgr.dOutGameText = new Dictionary<string, string>();
			string[] array = File.ReadAllLines(LoaderBehavior.DATA_PATH + "OutAssets/outGameTxt.txt");
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'|'
				});
				bool flag = array2.Length != 2;
				if (!flag)
				{
					ContMgr.dOutGameText[array2[0]] = array2[1];
					debug.Log(array2[0] + "==" + array2[1]);
				}
			}
		}

		public static void initOutGame(string path)
		{
			ContMgr.dOutGameText = new Dictionary<string, string>();
			string[] array = path.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'|'
				});
				bool flag = array2.Length != 2;
				if (!flag)
				{
					ContMgr.dOutGameText[array2[0]] = array2[1];
				}
			}
		}

		public static string getOutGameCont(string id, params string[] vals)
		{
			bool flag = ContMgr.dOutGameText == null;
			if (flag)
			{
				ContMgr.initOutGame();
			}
			bool flag2 = !ContMgr.dOutGameText.ContainsKey(id);
			string result;
			if (flag2)
			{
				result = id;
			}
			else
			{
				string text = ContMgr.dOutGameText[id];
				int num = 0;
				for (int i = 0; i < vals.Length; i++)
				{
					string separator = vals[i];
					string[] separator2 = new string[]
					{
						"<" + num + ">"
					};
					string[] value = text.Split(separator2, StringSplitOptions.None);
					text = string.Join(separator, value);
					num++;
				}
				string[] separator3 = new string[]
				{
					"<n>"
				};
				string[] value2 = text.Split(separator3, StringSplitOptions.None);
				text = string.Join("\n", value2);
				result = text;
			}
			return result;
		}

		public static string getError(string id)
		{
			bool flag = ContMgr.dError == null;
			if (flag)
			{
				ContMgr.init();
			}
			bool flag2 = !ContMgr.dError.ContainsKey(id);
			string result;
			if (flag2)
			{
				result = id;
			}
			else
			{
				result = ContMgr.dError[id];
			}
			return result;
		}

		public static string getCont(string id, params string[] vals)
		{
			bool flag = ContMgr.dText == null;
			if (flag)
			{
				ContMgr.init();
			}
			bool flag2 = !ContMgr.dText.ContainsKey(id);
			string result;
			if (flag2)
			{
				result = id;
			}
			else
			{
				string text = ContMgr.dText[id];
				int num = 0;
				for (int i = 0; i < vals.Length; i++)
				{
					string separator = vals[i];
					string[] separator2 = new string[]
					{
						"<" + num + ">"
					};
					string[] value = text.Split(separator2, StringSplitOptions.None);
					text = string.Join(separator, value);
					num++;
				}
				result = text;
			}
			return result;
		}

		public static string getCont(string id, List<string> pram = null)
		{
			bool flag = ContMgr.dText == null;
			if (flag)
			{
				ContMgr.init();
			}
			bool flag2 = !ContMgr.dText.ContainsKey(id);
			string result;
			if (flag2)
			{
				result = id;
			}
			else
			{
				string text = ContMgr.dText[id];
				bool flag3 = pram != null;
				if (flag3)
				{
					int num = 0;
					foreach (string current in pram)
					{
						string[] separator = new string[]
						{
							"<" + num + ">"
						};
						string[] value = text.Split(separator, StringSplitOptions.None);
						text = string.Join(current, value);
						num++;
					}
				}
				result = text;
			}
			return result;
		}
	}
}
