using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class SvrItemConfig : configParser
	{
		public static SvrItemConfig instance;

		protected Variant _item_data;

		protected Variant _fb_awd;

		protected bool _hasSetFbAwd = false;

		protected Variant _forgeRateItm = null;

		public SvrItemConfig(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrItemConfig(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			SvrItemConfig.instance = this;
			bool flag = conf.ContainsKey("forge_att_lvl");
			if (flag)
			{
				Variant variant = new Variant();
				foreach (Variant current in conf["forge_att_lvl"]._arr)
				{
					variant[current["name"]] = current;
				}
				conf["forge_att_lvl"] = variant;
			}
			bool flag2 = conf.ContainsKey("itm_merge_grp");
			if (flag2)
			{
				Variant variant2 = new Variant();
				foreach (Variant current2 in conf["itm_merge_grp"]._arr)
				{
					variant2[current2["id"]._int] = current2;
				}
				conf["itm_merge_grp"] = variant2;
			}
			bool flag3 = conf.ContainsKey("uitem");
			if (flag3)
			{
				conf["uitem"] = conf["uitem"].convertToDct("id");
			}
			bool flag4 = conf.ContainsKey("item");
			if (flag4)
			{
				conf["item"] = conf["item"].convertToDct("id");
			}
			bool flag5 = conf.ContainsKey("equip");
			if (flag5)
			{
				conf["equip"] = conf["equip"].convertToDct("id");
			}
			bool flag6 = conf.ContainsKey("stone");
			if (flag6)
			{
				conf["stone"] = conf["stone"].convertToDct("id");
			}
			bool flag7 = conf.ContainsKey("ex_att_grp");
			if (flag7)
			{
				conf["ex_att_grp"] = conf["ex_att_grp"].convertToDct("id");
			}
			return conf;
		}

		public Variant get_item_conf(uint tpid)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this.m_conf["uitem"].ContainsKey(tpid.ToString());
				if (flag2)
				{
					result = GameTools.createGroup(new Variant[]
					{
						"tp",
						1,
						"conf",
						this.m_conf["uitem"][tpid.ToString()]
					});
				}
				else
				{
					bool flag3 = this.m_conf["item"].ContainsKey(tpid.ToString());
					if (flag3)
					{
						result = GameTools.createGroup(new Variant[]
						{
							"tp",
							2,
							"conf",
							this.m_conf["item"][tpid.ToString()]
						});
					}
					else
					{
						bool flag4 = this.m_conf["equip"].ContainsKey(tpid.ToString());
						if (flag4)
						{
							result = GameTools.createGroup(new Variant[]
							{
								"tp",
								3,
								"conf",
								this.m_conf["equip"][tpid.ToString()]
							});
						}
						else
						{
							bool flag5 = this.m_conf["stone"].ContainsKey(tpid.ToString());
							if (flag5)
							{
								result = GameTools.createGroup(new Variant[]
								{
									"tp",
									4,
									"conf",
									this.m_conf["stone"][tpid.ToString()]
								});
							}
							else
							{
								result = null;
							}
						}
					}
				}
			}
			return result;
		}

		public Variant get_firstvip_awd()
		{
			bool hasSetFbAwd = this._hasSetFbAwd;
			Variant result;
			if (hasSetFbAwd)
			{
				result = this._fb_awd;
			}
			else
			{
				foreach (Variant current in this.m_conf["uitem"].Values)
				{
					bool flag = current["buyvip"] != null;
					if (flag)
					{
						bool flag2 = current["buyvip"][0] != null;
						if (flag2)
						{
							bool flag3 = current["buyvip"][0]["fb_awd"] != null;
							if (flag3)
							{
								this._hasSetFbAwd = true;
								this._fb_awd = current["buyvip"][0]["fb_awd"][0]["itm"];
								result = this._fb_awd;
								return result;
							}
						}
					}
				}
				this._hasSetFbAwd = true;
				result = null;
			}
			return result;
		}

		public int get_vip_lvl(int tpid)
		{
			int result;
			for (int i = 0; i < this.m_conf["uitem"].Count; i++)
			{
				bool flag = tpid == this.m_conf["uitem"][i]["tpid"];
				if (flag)
				{
					Variant variant = this.m_conf["uitem"][tpid];
					bool flag2 = variant["buyvip"][0] != null;
					if (flag2)
					{
						result = variant["buyvip"][0]["lvl"];
						return result;
					}
				}
			}
			result = 0;
			return result;
		}

		public bool on_item_data(ByteArray data)
		{
			return true;
		}

		public Variant Get_ex_att_grp(uint id)
		{
			return this.m_conf["ex_att_grp"][id];
		}

		public Variant getAllItemData()
		{
			this._item_data = new Variant();
			this._item_data.pushBack(this.m_conf["uitem"]);
			this._item_data.pushBack(this.m_conf["item"]);
			this._item_data.pushBack(this.m_conf["equip"]);
			this._item_data.pushBack(this.m_conf["stone"]);
			return this._item_data;
		}

		public Variant getComposeItemData(uint tpid)
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				result = this.m_conf["itm_merge_grp"][(int)tpid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant getComposeItemDataArray(Variant recipes)
		{
			Variant variant = new Variant();
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				using (List<Variant>.Enumerator enumerator = recipes._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int idx = enumerator.Current;
						variant.pushBack(this.m_conf["itm_merge_grp"][idx]);
					}
				}
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_attchk_adjust()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				result = this.m_conf["attchk_adjust"][0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_sup_frg_att()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				result = this.m_conf["sup_frg_att"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_sup_frg_att_lvl_by_id(string id)
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				bool flag2 = this.m_conf["sup_frg_att_lvl"].ContainsKey(id);
				if (flag2)
				{
					result = this.m_conf["sup_frg_att_lvl"][id]["att_lvl"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant get_veri_exatt_grp_by_id(int id)
		{
			bool flag = this.m_conf && this.m_conf.ContainsKey("veri_exatt_grp");
			Variant result;
			if (flag)
			{
				result = this.m_conf["veri_exatt_grp"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_sup_frg_att_grp_by_id(int id)
		{
			bool flag = this.m_conf && this.m_conf.ContainsKey("sup_frg_att_grp");
			Variant result;
			if (flag)
			{
				result = this.m_conf["sup_frg_att_grp"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_ex_att()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				bool flag2 = this.m_conf.ContainsKey("ex_att");
				if (flag2)
				{
					result = this.m_conf["ex_att"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant get_add_att()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				bool flag2 = this.m_conf.ContainsKey("add_att");
				if (flag2)
				{
					result = this.m_conf["add_att"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant get_add_att_grp()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				bool flag2 = this.m_conf.ContainsKey("add_att_grp");
				if (flag2)
				{
					result = this.m_conf["add_att_grp"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant get_flag_ex_att()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				bool flag2 = this.m_conf.ContainsKey("flag_ex_att");
				if (flag2)
				{
					result = this.m_conf["flag_ex_att"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant get_flag_ex_grp()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				bool flag2 = this.m_conf.ContainsKey("flag_ex_grp");
				if (flag2)
				{
					result = this.m_conf["flag_ex_grp"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetForgeAttLvlById(string id)
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				result = this.m_conf["forge_att_lvl"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetForgeAttLvl()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				result = this.m_conf["forge_att_lvl"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetSupfrgattgrpById(int id)
		{
			bool flag = this.m_conf != null && this.m_conf.ContainsKey("sup_frg_att_grp");
			Variant result;
			if (flag)
			{
				result = this.m_conf["sup_frg_att_grp"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetLuckAtt()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				bool flag2 = this.m_conf.ContainsKey("luck_att");
				if (flag2)
				{
					result = this.m_conf["luck_att"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetSuitConf(uint suitid)
		{
			Variant result;
			for (int i = 0; i < this.m_conf["suit"].Count; i++)
			{
				bool flag = suitid == this.m_conf["suit"][i]["suitid"]._uint;
				if (flag)
				{
					result = this.m_conf["suit"][i];
					return result;
				}
			}
			result = null;
			return result;
		}

		public bool is_uitem(uint item_id)
		{
			return this.m_conf["uitem"].ContainsKey(item_id.ToString());
		}

		public bool is_equip(uint item_id)
		{
			return this.m_conf["equip"].ContainsKey(item_id.ToString());
		}

		public Variant get_attchk_adjust_by_id(string id)
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				Variant variant = this.m_conf["attchk_adjust"][0];
				result = variant[id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_forge_lvl_conf(uint lvl)
		{
			Variant result;
			for (int i = 0; i < this.m_conf["forge_lvl"].Count; i++)
			{
				bool flag = (ulong)lvl == (ulong)((long)this.m_conf["forge_lvl"][i]["lvl"]._int);
				if (flag)
				{
					result = this.m_conf["forge_lvl"][i];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant get_forge_att_lvl()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				result = this.m_conf["forge_att_lvl"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_forge_att_lvl_by_id(string id)
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				result = this.m_conf["forge_att_lvl"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetForgeLvlByGrp(int grpid)
		{
			return this.m_conf["eqp_forge_grp"][grpid];
		}

		public Variant GetForgeGrp()
		{
			return this.m_conf["eqp_forge_grp"];
		}

		public int MaxgetForgetFpLvl()
		{
			bool flag = this.m_conf.ContainsKey("add_att_lvl");
			int result;
			if (flag)
			{
				Variant variant = this.m_conf["add_att_lvl"];
				result = variant[variant.Count - 1]["lvl"];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public Variant GetForgeRate()
		{
			bool flag = this._forgeRateItm == null;
			if (flag)
			{
				this._forgeRateItm = new Variant();
				Variant variant = this.m_conf["item"];
				foreach (Variant current in variant.Values)
				{
					bool flag2 = current.ContainsKey("frgrate");
					if (flag2)
					{
						this._forgeRateItm.pushBack(current);
					}
				}
			}
			return this._forgeRateItm;
		}

		public Variant get_add_att_lvl()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				result = this.m_conf["add_att_lvl"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_sup_frg_lvl()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				result = this.m_conf["sup_frg_lvl"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int get_forge_max_lvl()
		{
			int count = this.m_conf["forge_lvl"].Count;
			return this.m_conf["forge_lvl"][count - 1]["lvl"];
		}

		public Variant get_fly_item_tpid()
		{
			Variant variant = new Variant();
			foreach (string current in this.m_conf.Keys)
			{
				Variant variant2 = this.m_conf[current];
				bool flag = variant2.Keys == null;
				if (flag)
				{
					for (int i = 0; i < variant2.Length; i++)
					{
						Variant variant3 = variant2[i];
						bool flag2 = variant3 != null && variant3.ContainsKey("trans") && variant3["trans"] == 1;
						if (flag2)
						{
							variant.pushBack(variant3["id"]);
						}
					}
				}
				else
				{
					foreach (string current2 in variant2.Keys)
					{
						Variant variant4 = variant2[current2];
						bool flag3 = variant4 != null && variant4.ContainsKey("trans") && variant4["trans"]._str == "1";
						if (flag3)
						{
							variant.pushBack(variant4["id"]);
						}
					}
				}
			}
			return variant;
		}

		public uint get_specialitm(string special)
		{
			bool flag = this.m_conf == null;
			uint result;
			if (flag)
			{
				result = 0u;
			}
			else
			{
				foreach (string current in this.m_conf["key"].Keys)
				{
					Variant variant = this.m_conf[current];
					foreach (string current2 in variant.Keys)
					{
						Variant variant2 = variant[current2];
						bool flag2 = variant2.ContainsKey(special);
						if (flag2)
						{
							result = variant2[special];
							return result;
						}
					}
				}
				result = 0u;
			}
			return result;
		}

		public Variant get_itm_decomp_grp()
		{
			return this.m_conf["itm_decomp_grp"];
		}

		public Variant get_itm_merge_grp_by_id(int id)
		{
			bool flag = this.m_conf.ContainsKey("itm_merge_grp");
			Variant result;
			if (flag)
			{
				result = this.m_conf["itm_merge_grp"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_sell_eqp_att_by_id(int id)
		{
			bool flag = this.m_conf.ContainsKey("sell_eqp_att");
			Variant result;
			if (flag)
			{
				result = this.m_conf["sell_eqp_att"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_mkt_eqp_att_by_id(int id)
		{
			bool flag = this.m_conf.ContainsKey("mkt_eqp_att");
			Variant result;
			if (flag)
			{
				result = this.m_conf["mkt_eqp_att"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_make_att(int id)
		{
			bool flag = this.m_conf.ContainsKey("make_att");
			Variant result;
			if (flag)
			{
				result = this.m_conf["make_att"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_rstore_eqp_att_by_id(int id)
		{
			bool flag = this.m_conf.ContainsKey("rstore_eqp_att");
			Variant result;
			if (flag)
			{
				result = this.m_conf["rstore_eqp_att"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public bool CheckItmAtt(Variant eqp, Variant chk_confs)
		{
			bool result;
			foreach (Variant current in chk_confs._arr)
			{
				int num = 0;
				bool flag = !eqp.ContainsKey(current["name"]._str);
				if (flag)
				{
					bool flag2 = current["name"] == "exatt_cnt";
					if (flag2)
					{
						Variant variant = this.DecodeExatt(eqp["exatt"], eqp);
						num = variant.Count;
					}
					else
					{
						bool flag3 = current["name"] == "grade";
						if (flag3)
						{
							Variant variant2 = this.get_item_conf(eqp["tpid"]);
							bool flag4 = variant2 != null && variant2["conf"];
							if (flag4)
							{
								num = variant2["conf"]["grade"];
							}
						}
						else
						{
							bool flag5 = current["fun"] != "notmatch";
							if (flag5)
							{
								result = false;
								return result;
							}
							continue;
						}
					}
				}
				else
				{
					num = eqp[current["name"]];
				}
				bool flag6 = "equal" == current["fun"];
				if (flag6)
				{
					bool flag7 = current["val"] != num;
					if (flag7)
					{
						result = false;
						return result;
					}
				}
				else
				{
					bool flag8 = "min" == current["fun"];
					if (flag8)
					{
						bool flag9 = current["val"] > num;
						if (flag9)
						{
							result = false;
							return result;
						}
					}
					else
					{
						bool flag10 = "max" == current["fun"];
						if (flag10)
						{
							bool flag11 = current["val"] < num;
							if (flag11)
							{
								result = false;
								return result;
							}
						}
						else
						{
							bool flag12 = "match" == current["fun"];
							if (flag12)
							{
								bool flag13 = false;
								using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator2 = current["val"].Values.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										int num2 = enumerator2.Current;
										bool flag14 = num2 == num;
										if (flag14)
										{
											flag13 = true;
											break;
										}
									}
								}
								bool flag15 = !flag13;
								if (flag15)
								{
									result = false;
									return result;
								}
							}
							else
							{
								bool flag16 = "notmatch" == current["fun"];
								if (!flag16)
								{
									result = false;
									return result;
								}
								bool flag17 = true;
								using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator3 = current["val"].Values.GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										int num3 = enumerator3.Current;
										bool flag18 = num3 == num;
										if (flag18)
										{
											flag17 = false;
											break;
										}
									}
								}
								bool flag19 = !flag17;
								if (flag19)
								{
									result = false;
									return result;
								}
							}
						}
					}
				}
			}
			result = true;
			return result;
		}

		public Variant DecodeSupfrgatt(int values)
		{
			Variant variant = new Variant();
			for (int i = 0; i < 3; i++)
			{
				int val = values >> i * 10 + 4 & 63;
				int val2 = values >> i * 10 & 15;
				Variant variant2 = new Variant();
				variant2["id"] = val;
				variant2["lvl"] = val2;
				variant._arr.Add(variant2);
			}
			return variant;
		}

		public Variant DecodeExAddatt(int values)
		{
			bool flag = values > 0;
			Variant result;
			if (flag)
			{
				Variant variant = new Variant();
				for (int i = 0; i < 2; i++)
				{
					int val = values >> i * 8 & 15;
					int num = values >> i * 8 + 4 & 15;
					bool flag2 = num > 0;
					if (flag2)
					{
						Variant variant2 = new Variant();
						variant2["id"] = num;
						variant2["lvl"] = val;
						variant._arr.Add(variant2);
					}
				}
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant DecodeAddatt(int values)
		{
			bool flag = values > 0;
			Variant result;
			if (flag)
			{
				Variant variant = new Variant();
				for (int i = 0; i < 2; i++)
				{
					int val = values >> i * 8 & 15;
					int num = values >> i * 8 + 4 & 15;
					bool flag2 = num > 0;
					if (flag2)
					{
						Variant variant2 = new Variant();
						variant2["id"] = num;
						variant2["lvl"] = val;
						variant._arr.Add(variant2);
					}
				}
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant DecodeExatt(int values, Variant data)
		{
			Variant variant = new Variant();
			Variant variant2 = this.m_conf["ex_att"];
			int num = 0;
			int num2 = 0;
			bool flag = data != null;
			if (flag)
			{
				bool flag2 = data.ContainsKey("exlevel1");
				if (flag2)
				{
					num = data["exlevel1"];
				}
				bool flag3 = data.ContainsKey("exlevel2");
				if (flag3)
				{
					num2 = data["exlevel2"];
				}
			}
			bool flag4 = variant2 != null;
			if (flag4)
			{
				for (int i = 0; i < variant2.Count; i++)
				{
					int num3 = -1;
					int num4 = 0;
					Variant variant3 = variant2[i];
					Variant variant4 = new Variant();
					variant4["name"] = variant3["att"][0]["name"];
					variant4["val"] = variant3["att"][0]["val"];
					variant4["lvl"] = num3;
					Variant variant5 = variant3["att"][0]["lvs"];
					bool flag5 = i < 8;
					if (flag5)
					{
						num3 = (num >> 4 * i & 15);
					}
					else
					{
						num3 = (num2 >> 4 * (i - 8) & 15);
					}
					bool flag6 = variant5 != null;
					if (flag6)
					{
						foreach (Variant current in variant5.Values)
						{
							bool flag7 = current["lv"] == num3;
							if (flag7)
							{
								variant4["val"] = current["val"];
								break;
							}
						}
					}
					bool flag8 = variant3["id"] <= values;
					if (flag8)
					{
						bool flag9 = (values & variant3["id"]) == variant3["id"]._int;
						if (flag9)
						{
							num4 += (variant3.ContainsKey("yb") ? variant3["yb"]._int : 0);
							Variant variant6 = new Variant();
							variant6["att"] = variant4;
							variant6["id"] = variant3["id"];
							variant6["yb"] = num4;
							variant._arr.Add(variant6);
						}
					}
				}
			}
			return variant;
		}

		public Variant GetRideEquipPos()
		{
			bool flag = this.m_conf.ContainsKey("ride_eqp_pos");
			Variant result;
			if (flag)
			{
				result = this.m_conf["ride_eqp_pos"][0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetWingEquipPos()
		{
			bool flag = this.m_conf.ContainsKey("wing_eqp_pos");
			Variant result;
			if (flag)
			{
				result = this.m_conf["wing_eqp_pos"][0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetEqpSmelt()
		{
			return this.m_conf["eqp_smelt"];
		}

		public int GetItemGrade(uint tpid)
		{
			bool flag = this.m_conf["uitem"].ContainsKey("tpid");
			int result;
			if (flag)
			{
				result = -this.m_conf["uitem"][tpid]["grade"]._int;
			}
			else
			{
				bool flag2 = this.m_conf["item"].ContainsKey("tpid");
				if (flag2)
				{
					result = this.m_conf["item"][tpid]["grade"]._int;
				}
				else
				{
					bool flag3 = this.m_conf["equip"].ContainsKey("tpid");
					if (flag3)
					{
						result = this.m_conf["equip"][tpid]["grade"]._int;
					}
					else
					{
						bool flag4 = this.m_conf["stone"].ContainsKey("tpid");
						if (flag4)
						{
							result = this.m_conf["stone"][tpid]["grade"]._int;
						}
						else
						{
							result = 0;
						}
					}
				}
			}
			return result;
		}

		public Variant GetSkilItemBySkid(uint skid)
		{
			Variant result;
			foreach (Variant current in this.m_conf["uitem"].Values)
			{
				Variant variant = current["skillbk"];
				foreach (Variant current2 in variant.Values)
				{
					Variant variant2 = current2["skill"];
					bool flag = variant2 != null && variant2[0]["skid"] && variant2[0]["skid"] == skid;
					if (flag)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant get_rare_att()
		{
			bool flag = this.m_conf != null;
			Variant result;
			if (flag)
			{
				bool flag2 = this.m_conf.ContainsKey("rare_att");
				if (flag2)
				{
					result = this.m_conf["rare_att"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant Get_rare_att_grp(uint id)
		{
			return this.m_conf["rare_att_grp"][id];
		}

		public Variant DecodeRareExatt(int values, Variant data)
		{
			Variant variant = new Variant();
			Variant variant2 = this.m_conf["rare_att"];
			bool flag = variant2 != null;
			if (flag)
			{
				for (int i = 0; i < variant2.Count; i++)
				{
					int num = 0;
					Variant variant3 = variant2[i];
					bool flag2 = variant3["id"] <= values;
					if (flag2)
					{
						bool flag3 = (values & variant3["id"]) == variant3["id"]._int;
						if (flag3)
						{
							num += variant3["yb"];
							Variant variant4 = new Variant();
							variant4["att"] = variant3["att"][0];
							variant4["id"] = variant3["id"];
							variant4["yb"] = num;
							variant._arr.Add(variant4);
						}
					}
				}
			}
			return variant;
		}
	}
}
