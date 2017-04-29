using System;
using System.Collections.Generic;

namespace Cross
{
	public class LanguagePack
	{
		public static LanguagePack inst = new LanguagePack();

		protected Dictionary<string, StringTable> m_lPakMap = new Dictionary<string, StringTable>();

		public static string getLanguageText(string group, string text)
		{
			StringTable languagePack = LanguagePack.inst.getLanguagePack(group);
			bool flag = languagePack == null;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				result = languagePack.getString(text);
			}
			return result;
		}

		public LanguagePack()
		{
			this.m_lPakMap = new Dictionary<string, StringTable>();
		}

		public void regLanguagePack(string group, StringTable pack)
		{
			this.m_lPakMap[group] = pack;
		}

		public StringTable getLanguagePack(string group)
		{
			bool flag = !this.m_lPakMap.ContainsKey(group);
			StringTable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_lPakMap[group];
			}
			return result;
		}

		public void loadExtendFile(string file, Action onFin)
		{
			ConfigManager configManager = CrossApp.singleton.getPlugin("conf") as ConfigManager;
			configManager.loadExtendConfig(file, delegate(Variant conf)
			{
				this._formatLangPackConf(conf);
				onFin();
			});
		}

		protected void _formatLangPackConf(Variant conf)
		{
			foreach (Variant current in conf["group"]._arr)
			{
				bool flag = !current.ContainsKey("id");
				if (flag)
				{
					DebugTrace.print("ss");
				}
				bool flag2 = this.m_lPakMap.ContainsKey(current["id"]._str);
				StringTable stringTable;
				if (flag2)
				{
					stringTable = this.m_lPakMap[current["id"]._str];
				}
				else
				{
					stringTable = null;
				}
				bool flag3 = stringTable == null;
				if (flag3)
				{
					stringTable = new StringTable();
					this.m_lPakMap[current["id"]._str] = stringTable;
				}
				bool flag4 = current.ContainsKey("p");
				if (flag4)
				{
					stringTable.loadStrings(current["p"]._arr);
				}
			}
		}
	}
}
