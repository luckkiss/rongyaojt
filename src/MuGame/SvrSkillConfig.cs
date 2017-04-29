using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class SvrSkillConfig : configParser
	{
		public SvrSkillConfig(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrSkillConfig(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			conf["skill"] = GameTools.array2Map(conf["skill"], "id", 1u);
			conf["state"] = GameTools.array2Map(conf["state"], "id", 1u);
			foreach (Variant current in conf["skill"].Values)
			{
				bool flag = current.ContainsKey("lv");
				if (flag)
				{
					current["lv"] = GameTools.array2Map(current["lv"], "lv", 1u);
				}
			}
			return conf;
		}

		public Variant get_state_desc(uint state_id)
		{
			return this.m_conf["state"][state_id.ToString()];
		}

		public Variant get_bless_data(int blessid)
		{
			return this.m_conf["bstate"][blessid];
		}

		public Variant get_bless_by_tp(int stp)
		{
			Variant variant = new Variant();
			Variant result;
			using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator = this.m_conf["bstate"].Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string key = enumerator.Current;
					variant = this.m_conf["bstate"][key];
					bool flag = variant && variant.ContainsKey("eff");
					if (flag)
					{
						bool flag2 = stp == variant["eff"]["tp"];
						if (flag2)
						{
							result = variant;
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}

		public int get_bless_tp_by_id(int bid)
		{
			Variant variant = this.get_bless_data(bid);
			bool flag = variant && variant.ContainsKey("eff");
			int result;
			if (flag)
			{
				result = variant["eff"]["tp"]._int;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public Variant get_skill_conf(uint skill_id)
		{
			Variant variant = this.m_conf["skill"][skill_id.ToString()];
			bool flag = variant != null;
			if (flag)
			{
				bool flag2 = !variant.ContainsKey("act_c");
				if (flag2)
				{
					variant["lv"]["1"]["act_c"] = 0;
				}
				bool flag3 = !variant.ContainsKey("agry_c");
				if (flag3)
				{
					variant["lv"]["1"]["agry_c"] = 0;
				}
				bool flag4 = !variant.ContainsKey("hp_c");
				if (flag4)
				{
					variant["lv"]["1"]["hp_c"] = 0;
				}
			}
			return variant;
		}

		public Variant get_skill_lv_desc(uint skill_id, uint skill_lvl)
		{
			Variant variant = this.get_skill_conf(skill_id);
			bool flag = variant;
			Variant result;
			if (flag)
			{
				result = variant["lv"][skill_lvl];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant on_skill_data(ByteArray data)
		{
			return true;
		}

		private void _calc_skill_lvl_data()
		{
			Variant conf = this.m_conf;
			foreach (string current in conf["skill"].Keys)
			{
				Variant variant = conf["skill"][current];
				bool flag = !variant.ContainsKey("lv");
				if (!flag)
				{
					Variant variant2 = new Variant();
					foreach (string current2 in conf["lv"].Keys)
					{
						Variant variant3 = variant["lv"][current2];
						for (int i = 0; i < variant2.Count; i++)
						{
							bool flag2 = variant2[i] == null;
							if (!flag2)
							{
								bool flag3 = variant3["lv"] < variant2[i]["lv"];
								if (flag3)
								{
									break;
								}
							}
						}
					}
					Variant variant4 = new Variant();
					foreach (string current3 in variant2.Keys)
					{
						Variant variant5 = variant2[current3];
						bool flag4 = variant4 == null;
						if (flag4)
						{
							variant4 = variant5;
						}
						else
						{
							int j = variant4["lv"] + 1;
							int num = variant5["lv"] - variant4["lv"];
							while (j < variant5["lv"])
							{
								int num2 = j - variant4["lv"];
								Variant variant6 = new Variant();
								variant6["lv"] = j;
								bool flag5 = variant6.ContainsKey("hp_c");
								if (flag5)
								{
									variant6["hp_c"] = Convert.ToInt32(variant6["hp_c"] + (variant5["hp_c"] - variant6["hp_c"]) * num2 / num);
								}
								bool flag6 = variant6.ContainsKey("mp_c");
								if (flag6)
								{
									variant6["mp_c"] = Convert.ToInt32(variant6["mp_c"] + (variant5["mp_c"] - variant6["mp_c"]) * num2 / num);
								}
								bool flag7 = variant6.ContainsKey("agry_c");
								if (flag7)
								{
									variant6["agry_c"] = Convert.ToInt32(variant6["agry_c"] + (variant5["agry_c"] - variant6["agry_c"]) * num2 / num);
								}
								bool flag8 = variant6.ContainsKey("act_c");
								if (flag8)
								{
									variant6["act_c"] = Convert.ToInt32(variant6["act_c"] + (variant5["act_c"] - variant6["act_c"]) * num2 / num);
								}
								bool flag9 = variant6.ContainsKey("cd");
								if (flag9)
								{
									variant6["cd"] = Convert.ToInt32(variant6["cd"] + (variant5["cd"] - variant6["cd"]) * num2 / num);
								}
								bool flag10 = variant6.ContainsKey("lvexp");
								if (flag10)
								{
									variant6["lvexp"] = Convert.ToInt32(variant6["lvexp"] + (variant5["lvexp"] - variant6["lvexp"]) * num2 / num);
								}
								bool flag11 = variant6.ContainsKey("gld_cost");
								if (flag11)
								{
									variant6["gld_cost"] = Convert.ToInt32(variant6["gld_cost"] + (variant5["gld_cost"] - variant6["gld_cost"]) * num2 / num);
								}
								bool flag12 = variant6.ContainsKey("plv");
								if (flag12)
								{
									variant6["plv"] = Convert.ToInt32(variant6["plv"] + (variant5["plv"] - variant6["plv"]) * num2 / num);
								}
								bool flag13 = variant6.ContainsKey("teleport");
								if (flag13)
								{
									variant6["teleport"] = Convert.ToInt32(variant6["teleport"] + (variant5["teleport"] - variant6["teleport"]) * num2 / num);
								}
								bool flag14 = variant6.ContainsKey("upitmcnt");
								if (flag14)
								{
									variant6["upitmcnt"] = Convert.ToInt32(variant6["upitmcnt"] + (variant5["upitmcnt"] - variant6["upitmcnt"]) * num2 / num);
								}
								bool flag15 = variant6.ContainsKey("sres");
								if (flag15)
								{
									bool flag16 = variant6["sres"].ContainsKey("atk_dmg");
									if (flag16)
									{
										variant6["sres"]["atk_dmg"] = Convert.ToInt32(variant6["sres"]["atk_dmg"] + (variant5["sres"]["atk_dmg"] - variant6["sres"]["atk_dmg"]) * num2 / num);
									}
									bool flag17 = variant6["sres"].ContainsKey("matk_dmg");
									if (flag17)
									{
										variant6["sres"]["matk_dmg"] = Convert.ToInt32(variant6["sres"]["matk_dmg"] + (variant5["sres"]["matk_dmg"] - variant6["sres"]["matk_dmg"]) * num2 / num);
									}
									bool flag18 = variant6["sres"].ContainsKey("nag_dmg");
									if (flag18)
									{
										variant6["sres"]["nag_dmg"] = Convert.ToInt32(variant6["sres"]["nag_dmg"] + (variant5["sres"]["nag_dmg"] - variant6["sres"]["nag_dmg"]) * num2 / num);
									}
									bool flag19 = variant6["sres"].ContainsKey("pos_dmg");
									if (flag19)
									{
										variant6["sres"]["pos_dmg"] = Convert.ToInt32(variant6["sres"]["pos_dmg"] + (variant5["sres"]["pos_dmg"] - variant6["sres"]["pos_dmg"]) * num2 / num);
									}
									bool flag20 = variant6["sres"].ContainsKey("voi_dmg");
									if (flag20)
									{
										variant6["sres"]["voi_dmg"] = Convert.ToInt32(variant6["sres"]["voi_dmg"] + (variant5["sres"]["voi_dmg"] - variant6["sres"]["voi_dmg"]) * num2 / num);
									}
									bool flag21 = variant6["sres"].ContainsKey("poi_dmg");
									if (flag21)
									{
										variant6["sres"]["poi_dmg"] = Convert.ToInt32(variant6["sres"]["poi_dmg"] + (variant5["sres"]["poi_dmg"] - variant6["sres"]["poi_dmg"]) * num2 / num);
									}
									bool flag22 = variant6["sres"].ContainsKey("hp_dmg");
									if (flag22)
									{
										variant6["sres"]["hp_dmg"] = Convert.ToInt32(variant6["sres"]["hp_dmg"] + (variant5["sres"]["hp_dmg"] - variant6["sres"]["hp_dmg"]) * num2 / num);
									}
									bool flag23 = variant6["sres"].ContainsKey("mp_dmg");
									if (flag23)
									{
										variant6["sres"]["mp_dmg"] = Convert.ToInt32(variant6["sres"]["mp_dmg"] + (variant5["sres"]["mp_dmg"] - variant6["sres"]["mp_dmg"]) * num2 / num);
									}
									bool flag24 = variant6["sres"].ContainsKey("agry_dmg");
									if (flag24)
									{
										variant6["sres"]["agry_dmg"] = Convert.ToInt32(variant6["sres"]["agry_dmg"] + (variant5["sres"]["agry_dmg"] - variant6["sres"]["agry_dmg"]) * num2 / num);
									}
									bool flag25 = variant6["sres"].ContainsKey("state_tm");
									if (flag25)
									{
										variant6["sres"]["state_tm"] = Convert.ToInt32(variant6["sres"]["state_tm"] + (variant5["sres"]["state_tm"] - variant6["sres"]["state_tm"]) * num2 / num);
									}
									bool flag26 = variant6["sres"].ContainsKey("state_par");
									if (flag26)
									{
										variant6["sres"]["state_par"] = Convert.ToInt32(variant6["sres"]["state_par"] + (variant5["sres"]["state_par"] - variant6["sres"]["state_par"]) * num2 / num);
									}
								}
								this._calc_tres_lvl_conf_data(variant6, variant5, num2, num);
								bool flag27 = variant6.ContainsKey("fly");
								if (flag27)
								{
									bool flag28 = variant6["fly"].ContainsKey("speed");
									if (flag28)
									{
										variant6["fly"]["speed"] = Convert.ToInt32(variant6["fly"]["speed"] + (variant5["fly"]["speed"] - variant6["fly"]["speed"]) * num2 / num);
									}
									bool flag29 = variant6["fly"].ContainsKey("rang");
									if (flag29)
									{
										variant6["fly"]["rang"] = Convert.ToInt32(variant6["fly"]["rang"] + (variant5["fly"]["rang"] - variant6["fly"]["rang"]) * num2 / num);
									}
								}
								bool flag30 = variant6.ContainsKey("jump");
								if (flag30)
								{
									bool flag31 = variant6["jump"].ContainsKey("speed");
									if (flag31)
									{
										variant6["jump"]["speed"] = Convert.ToInt32(variant6["jump"]["speed"] + (variant5["jump"]["speed"] - variant6["jump"]["speed"]) * num2 / num);
									}
									bool flag32 = variant6["jump"].ContainsKey("rang");
									if (flag32)
									{
										variant6["jump"]["rang"] = Convert.ToInt32(variant6["jump"]["rang"] + (variant5["jump"]["rang"] - variant6["jump"]["rang"]) * num2 / num);
									}
									this._calc_tres_lvl_conf_data(variant6["jump"], variant5["jump"], num2, num);
								}
								variant["lv"][j] = variant6;
								j++;
							}
							variant4 = variant5;
						}
					}
					bool flag33 = this.m_conf.ContainsKey("clanskill");
					if (flag33)
					{
						Variant variant7 = new Variant();
						using (Dictionary<string, Variant>.KeyCollection.Enumerator enumerator4 = this.m_conf["clanskill"].Keys.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								Variant variant8 = enumerator4.Current;
								Variant variant9 = new Variant();
								using (Dictionary<string, Variant>.KeyCollection.Enumerator enumerator5 = variant8["lv"].Keys.GetEnumerator())
								{
									while (enumerator5.MoveNext())
									{
										Variant variant10 = enumerator5.Current;
										variant9[variant10["lv"]] = variant10;
									}
								}
								Variant variant11 = new Variant();
								variant11["id"] = variant8["id"];
								variant11["lv"] = variant9;
								variant7[variant8["id"]] = variant11;
							}
						}
						this.m_conf["clanskill"] = variant7;
					}
				}
			}
		}

		private void _calc_tres_lvl_conf_data(Variant cur_lv_conf, Variant lvconf, int cur_lv_dist, int lv_dist)
		{
			bool flag = cur_lv_conf.ContainsKey("tres");
			if (flag)
			{
				foreach (string current in cur_lv_conf["tres"].Keys)
				{
					Variant variant = cur_lv_conf["tres"][current];
					bool flag2 = variant.ContainsKey("atk_dmg");
					if (flag2)
					{
						variant["atk_dmg"] = Convert.ToInt32(variant["atk_dmg"] + (lvconf["tres"][current]["atk_dmg"] - variant["atk_dmg"]) * cur_lv_dist / lv_dist);
					}
					bool flag3 = variant.ContainsKey("matk_dmg");
					if (flag3)
					{
						variant["matk_dmg"] = Convert.ToInt32(variant["matk_dmg"] + (lvconf["tres"][current]["matk_dmg"] - variant["matk_dmg"]) * cur_lv_dist / lv_dist);
					}
					bool flag4 = variant.ContainsKey("nag_dmg");
					if (flag4)
					{
						variant["nag_dmg"] = Convert.ToInt32(variant["nag_dmg"] + (lvconf["tres"][current]["nag_dmg"] - variant["nag_dmg"]) * cur_lv_dist / lv_dist);
					}
					bool flag5 = variant.ContainsKey("pos_dmg");
					if (flag5)
					{
						variant["pos_dmg"] = Convert.ToInt32(variant["pos_dmg"] + (lvconf["tres"][current]["pos_dmg"] - variant["pos_dmg"]) * cur_lv_dist / lv_dist);
					}
					bool flag6 = variant.ContainsKey("voi_dmg");
					if (flag6)
					{
						variant["voi_dmg"] = Convert.ToInt32(variant["voi_dmg"] + (lvconf["tres"][current]["voi_dmg"] - variant["voi_dmg"]) * cur_lv_dist / lv_dist);
					}
					bool flag7 = variant.ContainsKey("poi_dmg");
					if (flag7)
					{
						variant["poi_dmg"] = Convert.ToInt32(variant["poi_dmg"] + (lvconf["tres"][current]["poi_dmg"] - variant["poi_dmg"]) * cur_lv_dist / lv_dist);
					}
					bool flag8 = variant.ContainsKey("hp_dmg");
					if (flag8)
					{
						variant["hp_dmg"] = Convert.ToInt32(variant["hp_dmg"] + (lvconf["tres"][current]["hp_dmg"] - variant["hp_dmg"]) * cur_lv_dist / lv_dist);
					}
					bool flag9 = variant.ContainsKey("mp_dmg");
					if (flag9)
					{
						variant["mp_dmg"] = Convert.ToInt32(variant["mp_dmg"] + (lvconf["tres"][current]["mp_dmg"] - variant["mp_dmg"]) * cur_lv_dist / lv_dist);
					}
					bool flag10 = variant.ContainsKey("agry_dmg");
					if (flag10)
					{
						variant["agry_dmg"] = Convert.ToInt32(variant["agry_dmg"] + (lvconf["tres"][current]["agry_dmg"] - variant["agry_dmg"]) * cur_lv_dist / lv_dist);
					}
					bool flag11 = variant.ContainsKey("state_tm");
					if (flag11)
					{
						variant["state_tm"] = Convert.ToInt32(variant["state_tm"] + (lvconf["tres"][current]["state_tm"] - variant["state_tm"]) * cur_lv_dist / lv_dist);
					}
					bool flag12 = variant.ContainsKey("state_par");
					if (flag12)
					{
						variant["state_par"] = Convert.ToInt32(variant["state_par"] + (lvconf["tres"][current]["state_par"] - variant["state_par"]) * cur_lv_dist / lv_dist);
					}
				}
			}
			bool flag13 = cur_lv_conf.ContainsKey("trang");
			if (flag13)
			{
				bool flag14 = cur_lv_conf["trang"].ContainsKey("cirang");
				if (flag14)
				{
					cur_lv_conf["trang"]["cirang"] = Convert.ToInt32(cur_lv_conf["trang"]["cirang"] + (lvconf["trang"]["cirang"] - cur_lv_conf["trang"]["cirang"]) * cur_lv_dist / lv_dist);
				}
				bool flag15 = cur_lv_conf["trang"].ContainsKey("fan");
				if (flag15)
				{
					bool flag16 = cur_lv_conf["trang"]["fan"].ContainsKey("angel");
					if (flag16)
					{
						cur_lv_conf["trang"]["fan"]["angel"] = Convert.ToInt32(cur_lv_conf["trang"]["fan"]["angel"] + (lvconf["trang"]["fan"]["angel"] - cur_lv_conf["trang"]["fan"]["angel"]) * cur_lv_dist / lv_dist);
					}
					bool flag17 = cur_lv_conf["trang"]["fan"].ContainsKey("rang");
					if (flag17)
					{
						cur_lv_conf["trang"]["fan"]["rang"] = Convert.ToInt32(cur_lv_conf["trang"]["fan"]["rang"] + (lvconf["trang"]["fan"]["rang"] - cur_lv_conf["trang"]["fan"]["rang"]) * cur_lv_dist / lv_dist);
					}
				}
				bool flag18 = cur_lv_conf["trang"].ContainsKey("maxi");
				if (flag18)
				{
					cur_lv_conf["trang"]["maxi"] = Convert.ToInt32(cur_lv_conf["trang"]["maxi"] + (lvconf["trang"]["maxi"] - cur_lv_conf["trang"]["maxi"]) * cur_lv_dist / lv_dist);
				}
			}
		}

		public Variant get_clanskill_conflist()
		{
			return this.m_conf["clanskill"];
		}

		public Variant get_clanskill_conf(uint skid)
		{
			bool flag = this.m_conf["clanskill"];
			Variant result;
			if (flag)
			{
				result = this.m_conf["clanskill"][skid];
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
