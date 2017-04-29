using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class SvrGeneralConfig : configParser
	{
		protected Variant _dayAwdArr;

		private Variant _need_line_map;

		private Variant _wareExattData;

		private Variant _wareFlvlData;

		private Variant _sortExattData;

		private Variant _sortFlvlData;

		private Variant _pmkt_yb;

		private Variant _qualAttObj;

		private Variant combptConf;

		private Variant _gmis_killmon;

		public Variant getmap_need_lvl
		{
			get
			{
				Variant variant = new Variant();
				for (int i = 0; i < base.conf["map_need_lvl"].Count; i++)
				{
					variant[base.conf["map_need_lvl"][i]["mapid"]._int] = base.conf["map_need_lvl"][i]["lvl"]._int;
				}
				return variant;
			}
		}

		public Variant ybract
		{
			get
			{
				return base.conf["ybract"];
			}
		}

		public Variant awdact
		{
			get
			{
				return base.conf["awdact"];
			}
		}

		public Variant rankact
		{
			get
			{
				return base.conf["rankact"];
			}
		}

		public Variant ol_award
		{
			get
			{
				return base.conf["ol_award"];
			}
		}

		public Variant lvlprizes
		{
			get
			{
				return base.conf["lvlprize"];
			}
		}

		public SvrGeneralConfig(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrGeneralConfig(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("vip");
			if (flag)
			{
				foreach (Variant current in conf["vip"]._arr)
				{
					bool flag2 = current.ContainsKey("item_grp");
					if (flag2)
					{
						foreach (Variant current2 in current["item_grp"]._arr)
						{
							bool flag3 = current2.ContainsKey("ids");
							if (flag3)
							{
								current2["ids"] = GameTools.split(current2["ids"]._str, ",", 0u);
							}
						}
					}
				}
			}
			bool flag4 = conf.ContainsKey("awdact");
			if (flag4)
			{
				foreach (Variant current3 in conf["awdact"]._arr)
				{
					foreach (Variant current4 in current3["target"]._arr)
					{
						current4["awd"] = current4["awd"].convertToDct("awdid");
					}
				}
			}
			bool flag5 = conf.ContainsKey("carr");
			if (flag5)
			{
				conf["carr"] = GameTools.array2Map(conf["carr"], "id", 1u);
			}
			return base._formatConfig(conf);
		}

		public Variant get_vip_fb_awd(int viplv)
		{
			bool flag = base.conf.ContainsKey("vip_fb_awd");
			Variant result;
			if (flag)
			{
				result = base.conf["vip_fb_awd"]["viplv"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_vip_info(int level)
		{
			Variant variant = base.conf["vip"];
			bool flag = level < variant.Count;
			Variant result;
			if (flag)
			{
				result = variant[level];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_pvip_dayawd_byPlvl(uint Plvl)
		{
			Variant variant = base.conf["pvip_dayawd"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["pvip"]._uint == Plvl;
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

		public Variant get_pvip_dayawd()
		{
			return base.conf["pvip_dayawd"];
		}

		public Variant get_pvip_growawd()
		{
			return base.conf["pvip_lmawd"];
		}

		public Variant get_pvip_power()
		{
			bool flag = base.conf != null;
			Variant result;
			if (flag)
			{
				bool flag2 = base.conf.ContainsKey("pvip_power");
				if (flag2)
				{
					result = base.conf["pvip_power"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant get_pvip()
		{
			return base.conf["pvip"];
		}

		public Variant GetPowerConf(uint id)
		{
			bool flag = base.conf != null;
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["pvip_power"];
				bool flag2 = variant != null;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag3 = current["id"]._uint == id;
						if (flag3)
						{
							result = current;
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}

		public Variant get_lottery(int lv)
		{
			Variant variant = base.conf["lottery"];
			bool flag = variant == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				for (int i = 0; i < variant.Length; i++)
				{
					bool flag2 = variant[i]["lvl"]._int == lv;
					if (flag2)
					{
						result = variant[i];
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public float get_game_general_data(string name)
		{
			bool flag = base.conf["game"][0].ContainsKey(name);
			float result;
			if (flag)
			{
				result = base.conf["game"][0][name]._float;
			}
			else
			{
				result = float.NaN;
			}
			return result;
		}

		public Variant get_game_general_object(string name)
		{
			bool flag = base.conf["game"].ContainsKey(name);
			Variant result;
			if (flag)
			{
				result = base.conf["game"][name];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetAucInfo(int tp)
		{
			bool flag = base.conf.ContainsKey("auc");
			Variant result;
			if (flag)
			{
				result = base.conf["auc"][tp.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int GetAucNum()
		{
			int num = 0;
			bool flag = base.conf.ContainsKey("auc");
			if (flag)
			{
				foreach (Variant current in base.conf["auc"]._arr)
				{
					bool flag2 = current["stall_cnt"];
					if (flag2)
					{
						num++;
					}
				}
			}
			return num;
		}

		public Variant GetNobilityData()
		{
			return base.conf["nobility"];
		}

		public Variant GetNobilityAwd()
		{
			bool flag = base.conf == null || !base.conf.ContainsKey("nobawd");
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = base.conf["nobawd"];
			}
			return result;
		}

		public Variant GetNobByid(int lvl)
		{
			Variant variant = base.conf["nobility"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[lvl - 1];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public string GetNobName(int lvl)
		{
			Variant variant = base.conf["nobility"];
			bool flag = variant != null && variant.ContainsKey(lvl.ToString());
			string result;
			if (flag)
			{
				result = variant[lvl.ToString()]["name"];
			}
			else
			{
				result = "";
			}
			return result;
		}

		public Variant GetAchiveData()
		{
			return base.conf["achive"];
		}

		public Variant GetAchieve(int id)
		{
			return base.conf["achive"][id.ToString()];
		}

		public uint get_clan_general_data(int lvl, string name)
		{
			bool flag = !base.conf["clan"].ContainsKey(lvl.ToString());
			uint result;
			if (flag)
			{
				result = 0u;
			}
			else
			{
				Variant variant = base.conf["clan"][lvl.ToString()];
				result = variant[name]._uint;
			}
			return result;
		}

		public Variant get_clanlvl_data(uint lvl)
		{
			bool flag = base.conf.ContainsKey("clan");
			Variant result;
			if (flag)
			{
				result = base.conf["clan"][lvl.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetCarrlvl(int carr)
		{
			return base.conf["carr"][carr.ToString()];
		}

		public Variant GetResetlvl()
		{
			return base.conf["resetlvl"];
		}

		public Variant get_attpt_roll()
		{
			return base.conf["attpt_roll"];
		}

		public Variant GetVipData()
		{
			return base.conf["vip"];
		}

		public Variant getMapex(uint mpid)
		{
			bool flag = !base.conf.ContainsKey("mapex");
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = base.conf["mapex"];
				bool flag2 = !variant.ContainsKey(mpid.ToString());
				if (flag2)
				{
					result = null;
				}
				else
				{
					Variant variant2 = variant[mpid.ToString()];
					result = variant2;
				}
			}
			return result;
		}

		public Variant GetSigninawd()
		{
			return base.conf["signin_awd"];
		}

		public Variant GetClogawd()
		{
			return base.conf["clogawd"];
		}

		public Variant GetConfByTypeId(string type, int id)
		{
			bool flag = base.conf != null && base.conf.ContainsKey(type);
			Variant result;
			if (flag)
			{
				Variant variant = base.conf[type];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["id"]._int == id;
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

		public Variant GetWorshipData()
		{
			return base.conf["worship"];
		}

		public Variant GetAchieveGoal(uint achieveID, int carr)
		{
			bool flag = base.conf["achieve"] != null;
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["achieve"][achieveID.ToString()];
				bool flag2 = variant != null;
				if (flag2)
				{
					bool flag3 = carr > 0 && variant["carr_goal"] != null;
					if (flag3)
					{
						Variant variant2 = variant["carr_goal"];
						bool flag4 = variant2.ContainsKey(carr.ToString());
						if (flag4)
						{
							result = variant2[carr.ToString()];
							return result;
						}
					}
					result = variant["goal"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public int GetSkillDmg(int carr, int inte)
		{
			int num = 100;
			int num2 = 2147483647;
			Variant carrlvl = this.GetCarrlvl(carr);
			bool flag = carrlvl != null;
			if (flag)
			{
				Variant variant = carrlvl["baseatt"][0];
				bool flag2 = variant.ContainsKey("inte2atk");
				if (flag2)
				{
					num2 = variant["inte2atk"]._int;
				}
			}
			bool flag3 = carr == 1 || carr == 4;
			if (flag3)
			{
				num = 200;
			}
			return num + inte / (inte + num2) * 100;
		}

		public Variant GetDefSkills(int carr)
		{
			Variant carrlvl = this.GetCarrlvl(carr);
			bool flag = carrlvl != null && carrlvl.ContainsKey("defskil");
			Variant result;
			if (flag)
			{
				GameTools.Sort(carrlvl["defskil"], "id", 16u);
				result = carrlvl["defskil"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetDefSkillData(int carr, uint sid)
		{
			Variant defSkills = this.GetDefSkills(carr);
			bool flag = defSkills != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in defSkills._arr)
				{
					bool flag2 = current["id"]._uint == sid;
					if (flag2)
					{
						bool flag3 = !current.ContainsKey("sklvl");
						if (flag3)
						{
							current["sklvl"] = current["lvl"];
						}
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public bool IsDefSkill(int carr, uint sid)
		{
			Variant defSkillData = this.GetDefSkillData(carr, sid);
			return defSkillData != null;
		}

		public Variant GetFestivalData()
		{
			return base.conf["festact"];
		}

		public Variant GetFestivalDataById(int id)
		{
			bool flag = base.conf.ContainsKey("festact");
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["festact"];
				bool flag2 = variant.ContainsKey(id.ToString());
				if (flag2)
				{
					result = variant[id.ToString()];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetOnlinePrize(int aid)
		{
			bool flag = base.conf["ol_award"] != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in base.conf["ol_award"]._arr)
				{
					bool flag2 = current["aid"]._int == aid;
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

		public Variant get_entermaplvl(uint mapid)
		{
			bool flag = base.conf.ContainsKey("map_need_lvl");
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["map_need_lvl"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["mapid"]._uint == mapid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			Variant variant2 = new Variant();
			variant2["lvl"] = 1;
			result = variant2;
			return result;
		}

		public Variant get_day_awd()
		{
			bool flag = this._dayAwdArr == null;
			if (flag)
			{
				this._dayAwdArr = new Variant();
				Variant variant = base.conf["day_awd"];
				foreach (Variant current in variant._arr)
				{
					this._dayAwdArr._arr.Add(current);
				}
			}
			return this._dayAwdArr;
		}

		public Variant getExchangeInfo()
		{
			return base.conf["ltryptexchg"];
		}

		public Variant GetMicroloadAwd()
		{
			bool flag = base.conf.ContainsKey("once_awd");
			Variant result;
			if (flag)
			{
				foreach (Variant current in base.conf["once_awd"]._arr)
				{
					bool flag2 = current["id"]._int == 1;
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

		public int GetMapNeedLine(uint mapid)
		{
			bool flag = this._need_line_map == null;
			if (flag)
			{
				this._need_line_map = new Variant();
				bool flag2 = base.conf.ContainsKey("need_line_map");
				if (flag2)
				{
					foreach (Variant current in base.conf["need_line_map"]._arr)
					{
						this._need_line_map[current["mpid"]] = current["line"];
					}
				}
			}
			bool flag3 = this._need_line_map.ContainsKey(mapid.ToString());
			int result;
			if (flag3)
			{
				result = this._need_line_map[mapid.ToString()];
			}
			else
			{
				result = -1;
			}
			return result;
		}

		public Variant GetRanShopData()
		{
			return base.conf["randstore"][0];
		}

		public Variant GetRanShopByLevel(uint level)
		{
			Variant variant = base.conf["randstore"][0];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				Variant variant2 = variant["rs"];
				foreach (Variant current in variant["rs"]._arr)
				{
					bool flag2 = (long)current["minlvl"]._int <= (long)((ulong)level) && (long)current["maxlvl"]._int >= (long)((ulong)level);
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

		public Variant GetRanShopItemCost(uint id)
		{
			Variant variant = base.conf["randstore"][0];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				Variant variant2 = variant["itm"];
				bool flag2 = variant2 != null;
				if (flag2)
				{
					foreach (Variant current in variant2._arr)
					{
						bool flag3 = current["id"]._uint == id;
						if (flag3)
						{
							result = current;
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetWareExattData()
		{
			bool flag = this._wareExattData == null;
			if (flag)
			{
				this._wareExattData = new Variant();
				Variant variant = base.conf["ware_eqp_act"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["tp"]._int == 2;
					if (flag2)
					{
						this._wareExattData._arr.Add(current);
					}
				}
			}
			bool flag3 = this._sortExattData == null;
			if (flag3)
			{
				this._sortExattData = new Variant();
				foreach (Variant current2 in this._wareExattData._arr)
				{
					Variant variant2 = new Variant();
					variant2["cnt"] = current2["eqpchk"][0]["cnt"];
					variant2["data"] = current2;
					this._sortExattData._arr.Add(variant2);
				}
				this._sortExattData._arr.Sort();
			}
			return this._sortExattData;
		}

		public Variant GetWareFlvlData()
		{
			bool flag = this._wareFlvlData == null;
			if (flag)
			{
				this._wareFlvlData = new Variant();
				Variant variant = base.conf["ware_eqp_act"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["tp"] == 1;
					if (flag2)
					{
						this._wareFlvlData._arr.Add(current);
					}
				}
			}
			bool flag3 = this._sortFlvlData == null;
			if (flag3)
			{
				this._sortFlvlData = new Variant();
				foreach (Variant current2 in this._wareFlvlData._arr)
				{
					Variant variant2 = new Variant();
					variant2["flvl"] = current2["eqpchk"][0]["flvl"];
					variant2["data"] = current2;
					this._sortFlvlData._arr.Add(variant2);
				}
				this._sortFlvlData._arr.Sort();
			}
			return this._sortFlvlData;
		}

		public int GetWareExattDataLvl(int cnt)
		{
			bool flag = this._sortExattData != null;
			int result;
			if (flag)
			{
				for (int i = 0; i < this._sortExattData.Count; i++)
				{
					bool flag2 = this._sortExattData[i]["cnt"] == cnt;
					if (flag2)
					{
						int num = i + 1;
						bool flag3 = num > this._sortExattData.Count - 1;
						if (flag3)
						{
							num = this._sortExattData.Count - 1;
						}
						result = num;
						return result;
					}
				}
			}
			result = 0;
			return result;
		}

		public int GetWareFlvlDataLvl(int flvl)
		{
			bool flag = this._sortFlvlData != null;
			int result;
			if (flag)
			{
				for (int i = 0; i < this._sortFlvlData.Count; i++)
				{
					bool flag2 = this._sortFlvlData[i]["flvl"] == flvl;
					if (flag2)
					{
						int num = i + 1;
						bool flag3 = num > this._sortFlvlData.Count - 1;
						if (flag3)
						{
							num = this._sortFlvlData.Count - 1;
						}
						result = num;
						return result;
					}
				}
			}
			result = 0;
			return result;
		}

		public float get_lvl_pvpinfo_get_tm()
		{
			return base.conf["game"]["lvl_pvpinfo_get_tm"]._float;
		}

		public Variant get_pmkt_yb()
		{
			bool flag = this._pmkt_yb == null;
			if (flag)
			{
				this._pmkt_yb = new Variant();
				bool flag2 = base.conf.ContainsKey("pmkt_yb");
				if (flag2)
				{
					foreach (Variant current in base.conf["pmkt_yb"]._arr)
					{
						this._pmkt_yb._arr.Add(current);
					}
				}
				this._pmkt_yb._arr.Sort();
			}
			return this._pmkt_yb;
		}

		public Variant GetDailyShare()
		{
			return base.conf["divt_awd"];
		}

		public Variant GetAcuShare()
		{
			return base.conf["ivtlvl_awd"];
		}

		public Variant getMountCulSConf()
		{
			return base.conf["mount"];
		}

		public int GetMountAvatar(int qual)
		{
			bool flag = base.conf.ContainsKey("mount");
			int result;
			if (flag)
			{
				Variant variant = base.conf["mount"]["qual"];
				bool flag2 = variant != null;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag3 = current["val"] == qual;
						if (flag3)
						{
							result = current["mid"]._int;
							return result;
						}
					}
				}
			}
			result = 0;
			return result;
		}

		public Variant GetMountLvlAtt(uint lvl, Variant mount)
		{
			Variant result = new Variant();
			Variant variant = mount;
			bool flag = variant == null;
			if (flag)
			{
				variant = base.conf["mount"];
			}
			bool flag2 = variant != null && variant.ContainsKey("lvl");
			if (flag2)
			{
				Variant variant2 = variant["lvl"];
				foreach (Variant current in variant2._arr)
				{
					bool flag3 = current.ContainsKey("val") && current["val"]._uint == lvl;
					if (flag3)
					{
						result = current["att"][0];
						break;
					}
				}
			}
			return result;
		}

		public Variant GetMountLvl(uint lvl)
		{
			bool flag = base.conf.ContainsKey("mount") && base.conf["mount"].ContainsKey("lvl");
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["mount"]["lvl"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current.ContainsKey("val") && current["val"]._uint == lvl;
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

		public Variant GetMountQualLvl(int lvl)
		{
			bool flag = base.conf.ContainsKey("mount");
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["mount"];
				int @int = variant["qual_section"]._int;
				bool flag2 = variant.ContainsKey("lvl");
				if (flag2)
				{
					int num = lvl / @int;
					int num2 = lvl % @int;
					bool flag3 = num2 == 0;
					if (flag3)
					{
						bool flag4 = num != 0;
						if (flag4)
						{
							num--;
						}
						num2 = @int;
					}
					Variant variant2 = base.conf["mount"]["lvl"];
					foreach (Variant current in variant2._arr)
					{
						bool flag5 = current.ContainsKey("val") && current["val"]._int == lvl;
						if (flag5)
						{
							Variant variant3 = new Variant();
							variant3["qual"] = num;
							variant3["lvl"] = num2;
							variant3["data"] = current;
							result = variant3;
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetMountQualAtt(uint qual)
		{
			bool flag = this._qualAttObj == null;
			if (flag)
			{
				this._qualAttObj = new Variant();
			}
			bool flag2 = base.conf.ContainsKey("mount") && !this._qualAttObj.ContainsKey("qual");
			if (flag2)
			{
				Variant variant = base.conf["mount"];
				int @int = variant["qual_section"]._int;
				bool flag3 = variant.ContainsKey("qual");
				if (flag3)
				{
					Variant variant2 = variant["qual"];
					foreach (Variant current in variant2._arr)
					{
						bool flag4 = current["val"] == qual;
						if (flag4)
						{
							this._qualAttObj["qual"] = current;
						}
					}
				}
			}
			return this._qualAttObj["qual"];
		}

		public Variant GetCombptConf()
		{
			bool flag = base.conf.ContainsKey("combpt");
			if (flag)
			{
				bool flag2 = this.combptConf == null;
				if (flag2)
				{
					this.combptConf = new Variant();
					foreach (Variant current in base.conf["combpt"]._arr)
					{
						this.combptConf[current["attname"]] = current["per"];
					}
				}
			}
			return this.combptConf;
		}

		public Variant GetRoomInfo()
		{
			return base.conf["room"];
		}

		public Variant GetGmisConf()
		{
			return base.conf["gmis"];
		}

		public Variant GetGmisKillmon()
		{
			bool flag = this._gmis_killmon == null;
			if (flag)
			{
				this._gmis_killmon = new Variant();
				foreach (Variant current in base.conf["gmis"]._arr)
				{
					Variant variant = new Variant();
					foreach (Variant current2 in current["goal"]._arr)
					{
						bool flag2 = !current2.ContainsKey("kilmon");
						if (!flag2)
						{
							foreach (Variant current3 in current2["kilmon"]._arr)
							{
								bool flag3 = variant._str.IndexOf(current3["monid"]._str) == -1;
								if (flag3)
								{
									variant._arr.Add(current3["monid"]);
								}
							}
						}
					}
					Variant variant2 = new Variant();
					variant2["id"] = current["id"];
					variant2["km"] = variant;
					this._gmis_killmon._arr.Add(variant2);
				}
			}
			return this._gmis_killmon;
		}

		public Variant GetGmisConfById(uint id)
		{
			bool flag = base.conf.ContainsKey("gmis");
			Variant result;
			if (flag)
			{
				bool flag2 = base.conf["gmis"].ContainsKey(id.ToString());
				if (flag2)
				{
					result = base.conf["gmis"][id.ToString()]._uint;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetGmisAwdById(uint id)
		{
			bool flag = base.conf.ContainsKey("gmis") && base.conf["gmis"][id.ToString()] != null;
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["gmis"][id.ToString()];
				Variant value = new Variant();
				Variant value2 = new Variant();
				bool flag2 = variant["awd"] != null;
				if (flag2)
				{
					value = variant["awd"][0];
				}
				bool flag3 = variant["vipawd"] != null;
				if (flag3)
				{
					value2 = variant["vipawd"][0];
				}
				Variant variant2 = new Variant();
				variant2["id"] = id;
				variant2["awd"] = value;
				variant2["vipawd"] = value2;
				result = variant2;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetGmisGoalById(uint id)
		{
			bool flag = base.conf.ContainsKey("gmis") && base.conf["gmis"].ContainsKey(id.ToString()) && base.conf["gmis"][id.ToString()] != null;
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["gmis"][id.ToString()];
				result = variant["goal"][0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetMonthInvest()
		{
			bool flag = base.conf.ContainsKey("monthinvest");
			Variant result;
			if (flag)
			{
				result = base.conf["monthinvest"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetUplvlInvest()
		{
			bool flag = base.conf.ContainsKey("uplvlinvest");
			Variant result;
			if (flag)
			{
				result = base.conf["uplvlinvest"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetArenaCwinAwd()
		{
			bool flag = base.conf.ContainsKey("arena_cwin_awd");
			Variant result;
			if (flag)
			{
				result = base.conf["arena_cwin_awd"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public bool CarrBStateLimit(uint carr, uint bstid)
		{
			bool flag = base.conf.ContainsKey("state_limit");
			bool result;
			if (flag)
			{
				Variant variant = base.conf["state_limit"];
				bool flag2 = variant != null;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag3 = current["carr"]._uint == carr;
						if (flag3)
						{
							Variant variant2 = current["bstate"];
							foreach (Variant current2 in variant2._arr)
							{
								bool flag4 = current2._uint == bstid;
								if (flag4)
								{
									result = true;
									return result;
								}
							}
							break;
						}
					}
				}
			}
			result = false;
			return result;
		}

		public bool CarrStateLimit(uint carr, uint stid)
		{
			bool flag = base.conf.ContainsKey("state_limit");
			bool result;
			if (flag)
			{
				Variant variant = base.conf["state_limit"];
				bool flag2 = variant != null;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag3 = current["carr"]._uint == carr;
						if (flag3)
						{
							Variant variant2 = current["state"];
							foreach (Variant current2 in variant2._arr)
							{
								bool flag4 = current2._uint == stid;
								if (flag4)
								{
									result = true;
									return result;
								}
							}
							break;
						}
					}
				}
			}
			result = false;
			return result;
		}

		public uint GetLevelShareRep(uint tp)
		{
			bool flag = base.conf.ContainsKey("level_share");
			uint result;
			if (flag)
			{
				foreach (Variant current in base.conf["level_share"]._arr)
				{
					bool flag2 = current["tp"]._uint == tp;
					if (flag2)
					{
						bool flag3 = current.ContainsKey("dailyrep");
						if (flag3)
						{
							result = current["dailyrep"]._uint;
							return result;
						}
						bool flag4 = current.ContainsKey("rep");
						if (flag4)
						{
							result = current["rep"]._uint;
							return result;
						}
					}
				}
			}
			result = 0u;
			return result;
		}

		public Variant GetOflExp()
		{
			return base.conf["ofl_exp"];
		}

		public Variant GetRedPaperSvrConf()
		{
			bool flag = base.conf.ContainsKey("red_paper") && base.conf["red_paper"] != null;
			Variant result;
			if (flag)
			{
				result = base.conf["red_paper"][0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetBaseAtt(int carr)
		{
			Variant variant = base.conf["carr"][carr.ToString()];
			bool flag = variant != null && variant["baseatt"] != null;
			Variant result;
			if (flag)
			{
				result = variant["baseatt"][0];
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
