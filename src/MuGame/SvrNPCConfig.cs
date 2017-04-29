using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class SvrNPCConfig : configParser
	{
		public static SvrNPCConfig instance;

		public SvrNPCConfig(ClientConfig m) : base(m)
		{
			SvrNPCConfig.instance = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrNPCConfig(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf != null;
			if (flag)
			{
				Variant variant = conf["npc"];
				bool flag2 = variant != null;
				if (flag2)
				{
					variant = GameTools.array2Map(variant, "id", 1u);
				}
				foreach (string current in variant.Keys)
				{
					bool flag3 = variant[current] != null && variant[current].ContainsKey("lentry");
					if (flag3)
					{
						Variant value = GameTools.split(variant[current]["lentry"]._str, ",", 0u);
						variant[current]["lentry"] = value;
					}
				}
				bool flag4 = conf.ContainsKey("state_grp");
				if (flag4)
				{
					conf["state_grp"] = GameTools.array2Map(conf["state_grp"], "id", 1u);
				}
				bool flag5 = conf.ContainsKey("dialog");
				if (flag5)
				{
					conf["dialog"] = GameTools.array2Map(conf["dialog"], "did", 1u);
				}
				conf["npc"] = variant;
			}
			return base._formatConfig(conf);
		}

		public Variant get_npc_data(int npcid)
		{
			bool flag = this.m_conf == null || !this.m_conf["npc"].ContainsKey(npcid.ToString());
			Variant result;
			if (flag)
			{
				GameTools.PrintError("get_npc_data [" + npcid + "] Config Err!");
				result = null;
			}
			else
			{
				Variant variant = this.m_conf["npc"][npcid.ToString()];
				bool flag2 = variant == null;
				if (flag2)
				{
					GameTools.PrintError("get_npc_data npc[" + npcid + "] not exist Err!");
					result = null;
				}
				else
				{
					bool flag3 = variant.ContainsKey("name");
					if (flag3)
					{
						variant["name"] = LanguagePack.getLanguageText("npcName", npcid.ToString());
					}
					result = variant;
				}
			}
			return result;
		}

		public int get_carrchief_npc(int carr)
		{
			int result;
			foreach (string current in this.m_conf["npc"].Keys)
			{
				Variant variant = this.m_conf["npc"][current];
				bool flag = variant.ContainsKey("carrchief") && variant["carrchief"]._int == carr;
				if (flag)
				{
					result = variant["id"]._int;
					return result;
				}
			}
			result = 0;
			return result;
		}

		public Variant get_sell_item(int siid)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_conf["sell_item"][siid];
			}
			return result;
		}

		public string get_sell_type(int stid)
		{
			bool flag = this.m_conf == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = this.m_conf["sell_type"][stid]["name"]._str;
			}
			return result;
		}

		public string get_dialog(int did)
		{
			bool flag = this.m_conf == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = this.m_conf.ContainsKey("dialog");
				bool flag3 = this.m_conf["dialog"].ContainsKey(did.ToString());
				Variant variant = this.m_conf["dialog"][did.ToString()];
				bool flag4 = !this.m_conf.ContainsKey("dialog") || !this.m_conf["dialog"].ContainsKey(did.ToString());
				if (flag4)
				{
					result = "";
				}
				else
				{
					result = this.m_conf["dialog"][did.ToString()]["content"];
				}
			}
			return result;
		}

		public int getLevelEntryNpc(int lvlid)
		{
			int result;
			foreach (string current in this.m_conf["npc"].Keys)
			{
				Variant variant = this.m_conf["npc"][current];
				bool flag = !variant.ContainsKey("lentry");
				if (!flag)
				{
					Variant variant2 = variant["lentry"];
					for (int i = 0; i < variant2.Count; i++)
					{
						bool flag2 = variant2[i]._int == lvlid;
						if (flag2)
						{
							result = variant["id"]._int;
							return result;
						}
					}
				}
			}
			result = 0;
			return result;
		}

		public Variant getNpcConf()
		{
			return this.m_conf;
		}
	}
}
