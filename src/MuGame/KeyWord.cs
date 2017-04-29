using System;
using System.Collections.Generic;

namespace MuGame
{
	public class KeyWord
	{
		public static KeyWord instance = new KeyWord();

		private static string[] l;

		public static string filter(string str)
		{
			return KeyWord.instance.Filter(str);
		}

		public static bool isContain(string str)
		{
			return KeyWord.instance.IsContain(str);
		}

		public void init()
		{
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("keyword.k", "");
			KeyWord.l = new string[sXMLList.Count];
			int num = 0;
			foreach (SXML current in sXMLList)
			{
				KeyWord.l[num] = current.getString("s");
				num++;
			}
		}

		public string Filter(string str)
		{
			bool flag = KeyWord.l == null;
			if (flag)
			{
				this.init();
			}
			string[] value = str.Split(KeyWord.l, StringSplitOptions.None);
			str = string.Join("*", value);
			return str;
		}

		public bool IsContain(string str)
		{
			bool flag = KeyWord.l == null;
			if (flag)
			{
				this.init();
			}
			string[] array = str.Split(KeyWord.l, StringSplitOptions.None);
			return array.Length > 1;
		}
	}
}
