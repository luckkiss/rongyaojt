using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MuGame
{
	public class SvrLevelConfig : configParser
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly SvrLevelConfig.<>c <>9 = new SvrLevelConfig.<>c();

			public static Comparison<Variant> <>9__17_0;

			public static Comparison<Variant> <>9__23_0;

			internal int <get_levelmis_byChapter>b__17_0(Variant left, Variant right)
			{
				bool flag = left["tpid"] > right["tpid"];
				int result;
				if (flag)
				{
					result = 1;
				}
				else
				{
					bool flag2 = left["tpid"] == right["tpid"];
					if (flag2)
					{
						result = 0;
					}
					else
					{
						result = -1;
					}
				}
				return result;
			}

			internal int <GetMultiLevel>b__23_0(Variant left, Variant right)
			{
				bool flag = left["tpid"] > right["tpid"];
				int result;
				if (flag)
				{
					result = 1;
				}
				else
				{
					bool flag2 = left["tpid"] == right["tpid"];
					if (flag2)
					{
						result = 0;
					}
					else
					{
						result = -1;
					}
				}
				return result;
			}
		}

		public static SvrLevelConfig instacne;

		private Variant _levelAwdInfo = new Variant();

		private Variant _multiLevel;

		public SvrLevelConfig(ClientConfig m) : base(m)
		{
			SvrLevelConfig.instacne = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrLevelConfig(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("lvl");
			if (flag)
			{
				conf["lvl"] = conf["lvl"].convertToDct("tpid");
				foreach (Variant current in conf["lvl"].Values)
				{
					bool flag2 = current.ContainsKey("diff_lvl");
					if (flag2)
					{
						List<Variant> list = new List<Variant>();
						for (int i = 0; i < current["diff_lvl"].Length; i++)
						{
							int j = current["diff_lvl"][i]["lv"]._int;
							while (j >= list.Count)
							{
								list.Add(null);
							}
							list[j] = current["diff_lvl"][i];
						}
						current.RemoveKey("diff_lvl");
						current["diff_lvl"] = new Variant();
						current["diff_lvl"].setToArray();
						for (int k = 0; k < list.Count; k++)
						{
							current["diff_lvl"].pushBack(list[k]);
						}
					}
					bool flag3 = current.ContainsKey("pvp");
					if (flag3)
					{
						bool flag4 = current["pvp"][0].ContainsKey("side");
						if (flag4)
						{
							for (int l = 0; l < current["pvp"][0]["side"].Length; l++)
							{
								current["pvp"][0]["side"] = current["pvp"][0]["side"].convertToDct("id");
							}
						}
					}
					bool flag5 = current.ContainsKey("tmchk");
					if (flag5)
					{
						for (int m = 0; m < current["tmchk"].Count; m++)
						{
							bool flag6 = current["tmchk"][m].ContainsKey("dtb");
							if (flag6)
							{
								Variant variant = GameTools.split(current["tmchk"][m]["dtb"], ":", 0u);
								current["tmchk"][m]["dtb"] = GameTools.createGroup(new Variant[]
								{
									"h",
									variant[0],
									"min",
									variant[1],
									"s",
									variant[2]
								});
							}
							bool flag7 = current["tmchk"][m].ContainsKey("dte");
							if (flag7)
							{
								Variant variant2 = GameTools.split(current["tmchk"][m]["dte"], ":", 0u);
								current["tmchk"][m]["dte"] = GameTools.createGroup(new Variant[]
								{
									"h",
									variant2[0],
									"min",
									variant2[1],
									"s",
									variant2[2]
								});
							}
							bool flag8 = current["tmchk"][m].ContainsKey("tb");
							if (flag8)
							{
								Variant variant3 = GameTools.split(current["tmchk"][m]["tb"], " ", 1u);
								Variant variant4 = GameTools.split(variant3[0], "-", 0u);
								Variant variant5 = GameTools.split(variant3[1], ":", 0u);
								current["tmchk"][m]["tb"] = GameTools.createGroup(new Variant[]
								{
									"y",
									variant4[0],
									"mon",
									variant4[1],
									"d",
									variant4[2],
									"h",
									variant5[0],
									"min",
									variant5[1],
									"s",
									variant5[2]
								});
							}
							bool flag9 = current["tmchk"][m].ContainsKey("te");
							if (flag9)
							{
								Variant variant6 = GameTools.split(current["tmchk"][m]["te"], " ", 1u);
								Variant variant7 = GameTools.split(variant6[0], "-", 0u);
								Variant variant8 = GameTools.split(variant6[1], ":", 0u);
								current["tmchk"][m]["te"] = GameTools.createGroup(new Variant[]
								{
									"y",
									variant7[0],
									"mon",
									variant7[1],
									"d",
									variant7[2],
									"h",
									variant8[0],
									"min",
									variant8[1],
									"s",
									variant8[2]
								});
							}
						}
					}
				}
			}
			return conf;
		}

		public Variant get_level_data(uint ltpid)
		{
			return this.m_conf["lvl"][ltpid.ToString()];
		}

		public bool isLevel(int ltpid)
		{
			Variant variant = this.m_conf["lvl"];
			return variant.ContainsKey(ltpid.ToString());
		}

		public Variant get_clan_territory(uint id)
		{
			return this.m_conf["clan_territory"][id];
		}

		public Variant is_level_map(uint mid)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant variant = this.m_conf["lvl"];
				foreach (Variant current in variant.Values)
				{
					Variant variant2 = current["map"];
					uint @uint = variant2[0]["id"]._uint;
					bool flag2 = @uint == mid;
					if (flag2)
					{
						result = true;
						return result;
					}
				}
				result = false;
			}
			return result;
		}

		public Variant getLevelAwd(uint ltpid)
		{
			bool flag = this._levelAwdInfo.ContainsKey("ltpid");
			Variant result;
			if (flag)
			{
				result = this._levelAwdInfo[ltpid];
			}
			else
			{
				foreach (Variant current in this.m_conf["lvl"]._arr)
				{
					bool flag2 = current["tpid"] == ltpid && current.ContainsKey("pvp") && current["pvp"][0].ContainsKey("rnk_awd");
					if (flag2)
					{
						this._levelAwdInfo[ltpid] = current;
						result = current;
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public bool IsLevelHasItemPrize(uint tpid)
		{
			bool result = false;
			Variant variant = this.m_conf["lvl"][tpid];
			bool flag = variant != null;
			if (flag)
			{
				Variant variant2 = variant["diff_lvl"];
				using (List<Variant>.Enumerator enumerator = variant2._arr.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						Variant current = enumerator.Current;
						result = current.ContainsKey("awd_itm");
					}
				}
			}
			return result;
		}

		public int get_levelmis_datalen()
		{
			int num = 0;
			bool flag = this.m_conf.ContainsKey("lvlmis");
			if (flag)
			{
				foreach (Variant current in this.m_conf["lvlmis"]._arr)
				{
					num++;
				}
			}
			return num;
		}

		public Variant get_levelmis_data(uint ltpid)
		{
			bool flag = this.m_conf.ContainsKey("lvlmis");
			Variant result;
			if (flag)
			{
				foreach (Variant current in this.m_conf["lvlmis"]._arr)
				{
					bool flag2 = current["tpid"] == ltpid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant Getlvlmis()
		{
			return this.m_conf["lvlmis"];
		}

		public uint GetlvlmisLine(uint misid)
		{
			bool flag = this.m_conf["lvlmis"].ContainsKey(misid.ToString());
			uint result;
			if (flag)
			{
				result = this.m_conf["lvlmis"][misid]["line"];
			}
			else
			{
				result = 0u;
			}
			return result;
		}

		public Variant get_levelmis_byPrelvlmis(uint ltpid)
		{
			bool flag = this.m_conf.ContainsKey("lvlmis");
			Variant result;
			if (flag)
			{
				foreach (Variant current in this.m_conf["lvlmis"]._arr)
				{
					bool flag2 = current["prelvlmis"] == ltpid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public uint get_passlvlmis_len(uint chapter)
		{
			uint num = 0u;
			bool flag = this.m_conf.ContainsKey("lvlmis");
			if (flag)
			{
				foreach (Variant current in this.m_conf["lvlmis"]._arr)
				{
					bool flag2 = current["chapter"] >= chapter;
					if (!flag2)
					{
						num += 1u;
					}
				}
			}
			return num;
		}

		public Variant get_levelmis_byChapter(uint chapter)
		{
			bool flag = this.m_conf.ContainsKey("lvlmis");
			Variant result;
			if (flag)
			{
				Variant variant = new Variant();
				foreach (Variant current in this.m_conf["lvlmis"]._arr)
				{
					bool flag2 = current["chapter"] == chapter;
					if (flag2)
					{
						variant._arr.Add(current);
					}
				}
				List<Variant> arg_AE_0 = variant._arr;
				Comparison<Variant> arg_AE_1;
				if ((arg_AE_1 = SvrLevelConfig.<>c.<>9__17_0) == null)
				{
					arg_AE_1 = (SvrLevelConfig.<>c.<>9__17_0 = new Comparison<Variant>(SvrLevelConfig.<>c.<>9.<get_levelmis_byChapter>b__17_0));
				}
				arg_AE_0.Sort(arg_AE_1);
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public uint GetLevelMisFloor(Variant lvlmis)
		{
			uint result = 0u;
			uint num = this.get_passlvlmis_len(lvlmis["chapter"]);
			Variant variant = this.get_levelmis_byChapter(lvlmis["chapter"]);
			for (int i = 0; i < variant.Length; i++)
			{
				Variant variant2 = variant[i];
				bool flag = variant2["tpid"] == lvlmis["tpid"];
				if (flag)
				{
					result = (uint)((ulong)num + (ulong)((long)i) + 1uL);
				}
			}
			return result;
		}

		public Variant get_carrchief(uint carr)
		{
			bool flag = this.m_conf == null || !this.m_conf.ContainsKey("carr_chief");
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = this.m_conf["carr_chief"];
				result = variant[carr];
			}
			return result;
		}

		public Variant get_arena_level(uint arenaid)
		{
			bool flag = this.m_conf == null || !this.m_conf.ContainsKey("arena");
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = this.m_conf["arena"];
				result = variant[arenaid];
			}
			return result;
		}

		public Variant get_arenaex_level(uint arenaexid)
		{
			bool flag = this.m_conf == null || !this.m_conf.ContainsKey("arenaex");
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = this.m_conf["arenaex"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current != null;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public Variant GetMultiLevel()
		{
			bool flag = this._multiLevel == null;
			if (flag)
			{
				this._multiLevel = new Variant();
				Variant variant = this.m_conf["lvl"];
				foreach (Variant current in variant.Values)
				{
					bool flag2 = current.ContainsKey("room");
					if (flag2)
					{
						this._multiLevel._arr.Add(current);
					}
				}
				List<Variant> arg_B0_0 = this._multiLevel._arr;
				Comparison<Variant> arg_B0_1;
				if ((arg_B0_1 = SvrLevelConfig.<>c.<>9__23_0) == null)
				{
					arg_B0_1 = (SvrLevelConfig.<>c.<>9__23_0 = new Comparison<Variant>(SvrLevelConfig.<>c.<>9.<GetMultiLevel>b__23_0));
				}
				arg_B0_0.Sort(arg_B0_1);
			}
			return this._multiLevel;
		}

		public Variant GetLevelMap(uint ltpid, uint diff)
		{
			Variant variant = this.m_conf["lvl"][ltpid];
			bool flag = variant != null && variant["diff_lvl"] != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in variant["diff_lvl"]._arr)
				{
					bool flag2 = current["lv"] == diff;
					if (flag2)
					{
						result = current["map"];
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetLevelMapById(uint ltpid, uint diff, uint mapid)
		{
			Variant variant = this.m_conf["lvl"][ltpid];
			bool flag = variant != null && variant["diff_lvl"] != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in variant["diff_lvl"]._arr)
				{
					bool flag2 = current["lv"] == diff;
					if (flag2)
					{
						foreach (Variant current2 in current["map"]._arr)
						{
							bool flag3 = current2["id"] == mapid;
							if (flag3)
							{
								result = current2;
								return result;
							}
						}
						break;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetLevelNextMapById(uint ltpid, uint diff, uint mapid)
		{
			Variant variant = this.m_conf["lvl"][ltpid];
			bool flag = variant != null && variant["diff_lvl"] != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in variant["diff_lvl"]._arr)
				{
					bool flag2 = current["lv"] == diff;
					if (flag2)
					{
						foreach (Variant current2 in current["map"]._arr)
						{
							bool flag3 = current2["dir_enter"] != null && current2["dir_enter"][0] != null && current2["dir_enter"][0]["km"] != null;
							if (flag3)
							{
								foreach (Variant current3 in current2["dir_enter"][0]["km"]._arr)
								{
									bool flag4 = current3["mapid"] == mapid;
									if (flag4)
									{
										result = current2;
										return result;
									}
								}
							}
						}
						break;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetDirEnterByMapid(uint ltpid, uint diff, uint mapid)
		{
			Variant levelNextMapById = this.GetLevelNextMapById(ltpid, diff, mapid);
			bool flag = levelNextMapById != null && levelNextMapById["dir_enter"] != null;
			Variant result;
			if (flag)
			{
				result = levelNextMapById["dir_enter"][0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetLevelDiffCostCh(uint ltpid, uint diff = 1u)
		{
			Variant variant = this.m_conf["lvl"][ltpid.ToString()];
			bool flag = variant != null && variant["diff_lvl"] != null;
			Variant result;
			if (flag)
			{
				Variant variant2 = new Variant();
				foreach (Variant current in variant["diff_lvl"]._arr)
				{
					bool flag2 = current == null;
					if (!flag2)
					{
						bool flag3 = current["lv"] == diff && current.ContainsKey("cost_ch") && current["cost_ch"] != null;
						if (flag3)
						{
							foreach (Variant current2 in current["cost_ch"]._arr)
							{
								variant2[current2["tp"]] = current2["cost"];
							}
							break;
						}
					}
				}
				result = variant2;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetLevelShareTpConf(uint ltpid, uint diff = 1u)
		{
			Variant levelDiffCostCh = this.GetLevelDiffCostCh(ltpid, diff);
			bool flag = levelDiffCostCh.Count != 0;
			Variant result;
			if (flag)
			{
				foreach (string current in levelDiffCostCh.Keys)
				{
					foreach (Variant current2 in levelDiffCostCh[current]._arr)
					{
						bool flag2 = current2["share"] != null;
						if (flag2)
						{
							result = current2["share"][0];
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}
	}
}
