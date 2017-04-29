using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class UIUtility
	{
		protected muUIClient _uiClient;

		public static UIUtility singleton;

		private string _newline = "{br}";

		private const string _strNewLineForTF = "\n";

		private Variant _market_data = null;

		private Variant _market_data_sell;

		private uint _trans_item_tpid = 0u;

		private string t_noTransItem = "";

		private Variant _transData = new Variant();

		private const string F_PREFIX = "localflag_";

		private const string F_PREFIX_GAME = "localflag_game";

		private string _char_key;

		private string _defaultStr = "c=txt";

		private string _dafaultNumber = "c=yellow";

		private Variant _entities = null;

		private const int FLAG_LUCK = 1;

		private const double ratio = 0.001;

		private string _fuzzyMap;

		public UIUtility(muUIClient m)
		{
			this._uiClient = m;
			UIUtility.singleton = this;
		}

		public bool IsWalkAble(int gx, int gy)
		{
			return (this._uiClient.g_gameM.getObject("LG_MAP") as LGMap).IsWalkAble(gx, gy);
		}

		public string GetHtmlText(string grp, string key)
		{
			string languageText = LanguagePack.getLanguageText(grp, key);
			return (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.TransToSuperText(languageText);
		}

		public string TransToHtmlText(string str)
		{
			str = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.TransToSuperText(str);
			return str;
		}

		public Variant GetAttchkData(Variant data, Variant attchk)
		{
			Variant variant = new Variant();
			Variant adjustData = this.getAdjustData(data, null);
			foreach (Variant current in attchk._arr)
			{
				Variant variant2 = new Variant();
				foreach (string current2 in current.Keys)
				{
					variant2[current2] = current[current2];
				}
				bool flag = "carr" != current["name"];
				if (flag)
				{
					variant2["min"] = this.attNeed(current["min"], adjustData, current["name"]);
				}
				variant._arr.Add(variant2);
			}
			return variant;
		}

		private int attNeed(int val, Variant adjust, string type)
		{
			int num = 0;
			bool flag = adjust[type] != null;
			int result;
			if (flag)
			{
				bool flag2 = adjust[type]["mul"] > 0 || adjust[type]["add"] > 0;
				if (flag2)
				{
					num = val * adjust[type]["mul"]._int / 100 + adjust[type]["add"]._int;
				}
				result = num;
			}
			else
			{
				result = val;
			}
			return result;
		}

		private Variant getAdjustData(Variant data, Variant conf = null)
		{
			SvrItemConfig svrItemConf = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf;
			Variant attchk_adjust = svrItemConf.get_attchk_adjust();
			bool flag = attchk_adjust == null;
			Variant result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				Variant variant = new Variant();
				bool flag2 = conf == null;
				if (flag2)
				{
					Variant variant2 = svrItemConf.get_item_conf(data["tpid"]);
					conf = variant2["conf"];
				}
				bool flag3 = conf.ContainsKey("lv") && attchk_adjust.ContainsKey("lv");
				if (flag3)
				{
					foreach (Variant current in attchk_adjust["lv"]._arr)
					{
						bool flag4 = variant.ContainsKey(current["name"]._str);
						if (flag4)
						{
							Variant value = variant[current["name"]];
						}
						else
						{
							Variant value = GameTools.createGroup(new Variant[]
							{
								"mul",
								0,
								"add",
								0
							});
							variant[current["name"]] = value;
						}
						bool flag5 = current.ContainsKey("mul");
						if (flag5)
						{
							Variant variant3 = variant[current["name"]];
							variant3["mul"] = variant3["mul"] + current["mul"] * conf["lv"];
						}
						bool flag6 = current.ContainsKey("add");
						if (flag6)
						{
							Variant variant3 = variant[current["name"]];
							variant3["add"] = variant3["add"] + current["add"]._int;
						}
					}
				}
				bool flag7 = data.ContainsKey("flvl") && data["flvl"] > 0;
				if (flag7)
				{
					bool flag8 = attchk_adjust.ContainsKey("flvl");
					if (flag8)
					{
						foreach (Variant current2 in attchk_adjust["flvl"]._arr)
						{
							bool flag9 = variant.ContainsKey(current2["name"]._str);
							if (flag9)
							{
								Variant value = variant[current2["name"]];
							}
							else
							{
								Variant value = GameTools.createGroup(new Variant[]
								{
									"mul",
									0,
									"add",
									0
								});
								variant[current2["name"]] = value;
							}
							bool flag10 = current2.ContainsKey("mul");
							if (flag10)
							{
								Variant variant3 = variant[current2["name"]];
								variant3["mul"] = variant3["mul"] + current2["mul"] * data["flvl"];
							}
							bool flag11 = current2.ContainsKey("add");
							if (flag11)
							{
								Variant variant3 = variant[current2["name"]];
								variant3["add"] = variant3["add"] + current2["add"]._int;
							}
						}
					}
				}
				bool flag12 = this.IsExatt(data);
				if (flag12)
				{
					bool flag13 = attchk_adjust.ContainsKey("exatt");
					if (flag13)
					{
						foreach (Variant current3 in attchk_adjust["exatt"]._arr)
						{
							bool flag14 = variant.ContainsKey(current3["name"]._str);
							if (flag14)
							{
								Variant value = variant[current3["name"]];
							}
							else
							{
								Variant value = GameTools.createGroup(new Variant[]
								{
									"mul",
									0,
									"add",
									0
								});
								variant[current3["name"]] = value;
							}
							bool flag15 = current3.ContainsKey("mul");
							if (flag15)
							{
								Variant variant3 = variant[current3["name"]];
								variant3["mul"] = variant3["mul"] + current3["mul"]._int;
							}
							bool flag16 = current3.ContainsKey("add");
							if (flag16)
							{
								Variant variant3 = variant[current3["name"]];
								variant3["add"] = variant3["add"] + current3["add"]._int;
							}
						}
					}
				}
				bool flag17 = data.ContainsKey("fpt") && data["fpt"] > 0;
				if (flag17)
				{
					bool flag18 = attchk_adjust.ContainsKey("supfrg");
					if (flag18)
					{
						Variant variant4 = svrItemConf.DecodeSupfrgatt(data["fpt"]._int);
						foreach (Variant current4 in variant4._arr)
						{
							int idx = current4["id"];
							int fptlvl = current4["lvl"];
							Variant sup_frg_att = svrItemConf.get_sup_frg_att();
							bool flag19 = sup_frg_att[idx] != null;
							if (flag19)
							{
								Variant variant5 = sup_frg_att[idx];
								foreach (Variant current5 in attchk_adjust["supfrg"]._arr)
								{
									bool flag20 = variant5["att"][0]["name"] != current5["supatt"];
									if (!flag20)
									{
										bool flag21 = variant.ContainsKey(current5["name"]._int);
										if (flag21)
										{
											Variant value = variant[current5["name"]];
										}
										else
										{
											Variant value = GameTools.createGroup(new Variant[]
											{
												"mul",
												0,
												"add",
												0
											});
											variant[current5["name"]] = value;
										}
										Variant variant3 = variant[current5["name"]];
										variant3["add"] = variant3["add"] + (variant5["att"][0]["val"] + this.GetSupfrgAdd(current5["supatt"], fptlvl));
									}
								}
							}
						}
					}
				}
				result = variant;
			}
			return result;
		}

		public int GetSupfrgAdd(string name, int fptlvl)
		{
			Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_sup_frg_att_lvl_by_id(name);
			int result = 0;
			bool flag = variant != null;
			if (flag)
			{
				bool flag2 = fptlvl < variant.Count;
				if (flag2)
				{
					bool flag3 = variant[fptlvl] != null;
					if (flag3)
					{
						result = variant[fptlvl]["add"]._int;
					}
				}
			}
			return result;
		}

		public int ExattAdd(int val, int lv, string name = "")
		{
			bool flag = "eqp_def" == name;
			int result;
			if (flag)
			{
				result = val * 12 / lv + lv / 5 + 4;
			}
			else
			{
				result = val * 25 / lv + 5;
			}
			return result;
		}

		public int GetForgeAttLvlAdd(string name, int flvl)
		{
			Variant forgeAttLvlById = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.GetForgeAttLvlById(name);
			int result = 0;
			bool flag = forgeAttLvlById != null && flvl > 0;
			if (flag)
			{
				result = forgeAttLvlById["att_lvl"][flvl]["add"]._int;
			}
			return result;
		}

		public Variant GetExattById(int values)
		{
			Variant ex_att = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_ex_att();
			Variant variant = new Variant();
			for (int i = 0; i < ex_att.Count; i++)
			{
				Variant variant2 = ex_att[i];
				bool flag = variant2["id"] <= values;
				if (flag)
				{
					bool flag2 = (values & variant2["id"]) == variant2["id"];
					if (flag2)
					{
						variant["att"] = variant2["att"][0];
						variant["id"] = variant2["id"];
						break;
					}
				}
			}
			return variant;
		}

		public string DecodeCarr(int carr, Variant chkData, bool sup = true)
		{
			int @int = chkData["carr"]._int;
			int int2 = chkData["carrlvl"]._int;
			string text = "";
			int num = 8;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				bool flag = (carr >> i * 4 & 1) != 0;
				if (flag)
				{
					int num3 = i + 1;
					int num4 = carr >> i * 4 + 1 & 7;
					string str = LanguagePack.getLanguageText("carr", string.Concat(new object[]
					{
						"carr_",
						num3,
						"_",
						num4
					}));
					bool flag2 = num2 > 0;
					string str2;
					if (flag2)
					{
						str2 = ";";
					}
					else
					{
						str2 = "";
					}
					if (sup)
					{
						bool flag3 = @int == num3 && int2 >= num4;
						if (flag3)
						{
							str = "<txt text='" + str + str2 + "' color='0xffffff'/>";
						}
						else
						{
							str = "<txt text='" + str + str2 + "' color='0xff0000'/>";
						}
					}
					text += str;
					num2++;
				}
			}
			return text;
		}

		public bool CheckCarr(uint carr, Variant chkData)
		{
			int @int = chkData["carr"]._int;
			int int2 = chkData["carrlvl"]._int;
			int num = 8;
			bool result;
			for (int i = 0; i < num; i++)
			{
				bool flag = (carr >> i * 4 & 1u) > 0u;
				if (flag)
				{
					uint num2 = carr >> i * 4 + 1 & 7u;
					int num3 = i + 1;
					bool flag2 = @int == num3 && (long)int2 >= (long)((ulong)num2);
					if (flag2)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public bool IsExatt(Variant data)
		{
			return DisplayUtil.singleton.IsExatt(data);
		}

		public int isQual(Variant o)
		{
			bool flag = !UIUtility.singleton.IsExatt(o);
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(o["tpid"]._uint);
				bool flag2 = variant == null;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					bool flag3 = variant["conf"]["pos"]._int == 11;
					if (flag3)
					{
						result = 0;
					}
					else
					{
						bool flag4 = variant["conf"]["qual"]._int == 2 || variant["conf"]["qual"]._int == 3;
						if (flag4)
						{
							result = 2;
						}
						else
						{
							bool flag5 = variant["conf"]["qual"]._int == 4;
							if (flag5)
							{
								result = 3;
							}
							else
							{
								bool flag6 = variant["conf"]["qual"]._int == 5;
								if (flag6)
								{
									result = 4;
								}
								else
								{
									result = 0;
								}
							}
						}
					}
				}
			}
			return result;
		}

		public bool CheckAllChk(Variant eqp, Variant chkData)
		{
			Variant variant = new Variant();
			SvrItemConfig svrItemConf = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf;
			bool flag = chkData.ContainsKey("attchk");
			bool result;
			if (flag)
			{
				bool flag2 = !svrItemConf.CheckItmAtt(eqp, chkData["attchk"]);
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			variant = svrItemConf.get_item_conf(eqp["tpid"]._uint);
			bool flag3 = variant == null;
			if (flag3)
			{
				result = false;
			}
			else
			{
				bool flag4 = chkData.ContainsKey("cfgchk");
				if (flag4)
				{
					bool flag5 = !svrItemConf.CheckItmAtt(variant["conf"], chkData["cfgchk"]);
					if (flag5)
					{
						result = false;
						return result;
					}
				}
				bool flag6 = chkData.ContainsKey("n_match_1");
				if (flag6)
				{
					Variant variant2 = chkData["n_match_1"][0];
					bool flag7 = false;
					foreach (Variant current in variant2["match_chk"]._arr)
					{
						bool flag8 = current.ContainsKey("attchk");
						if (flag8)
						{
							bool flag9 = !svrItemConf.CheckItmAtt(eqp, current["attchk"]);
							if (flag9)
							{
								continue;
							}
						}
						bool flag10 = current.ContainsKey("cfgchk");
						if (flag10)
						{
							bool flag11 = !svrItemConf.CheckItmAtt(variant["conf"], current["cfgchk"]);
							if (flag11)
							{
								continue;
							}
						}
						flag7 = true;
						break;
					}
					bool flag12 = !flag7;
					if (flag12)
					{
						result = false;
						return result;
					}
				}
				result = true;
			}
			return result;
		}

		public bool CheckIsSuitStone(Variant item)
		{
			string str = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("isSuitStone")._str;
			Variant variant = GameTools.split(str, ",", 1u);
			bool result;
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num = enumerator.Current;
					bool flag = item["tpid"] == num;
					if (flag)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public bool CheckCanForge(Variant data)
		{
			bool flag = data == null || data["cantfrg"] != null || 10 == data["pos"] || 12 == data["pos"];
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = data.ContainsKey("eqp_def") || data.ContainsKey("eqp_atk_min") || data.ContainsKey("eqp_atk_max") || data.ContainsKey("eqp_matk_min") || data.ContainsKey("eqp_matk_max");
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = data.ContainsKey("forge_att");
					if (flag3)
					{
						foreach (Variant current in data["forge_att"]._arr)
						{
							bool flag4 = current.ContainsKey("eqp_def") || current.ContainsKey("eqp_atk_min") || current.ContainsKey("eqp_atk_max");
							if (flag4)
							{
								result = true;
								return result;
							}
						}
					}
					result = false;
				}
			}
			return result;
		}

		public int GetMulattAdd(Variant data)
		{
			Variant mainPlayerInfo = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			int @int = data["val"]._int;
			return mainPlayerInfo[data["mulatt"]] * @int;
		}

		public int GetExattAdd(Variant obj)
		{
			bool flag = obj == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num = (int)obj["att"]["val"]._float;
				bool flag2 = obj["att"].ContainsKey("mulatt");
				if (flag2)
				{
					Variant mainPlayerInfo = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
					bool flag3 = mainPlayerInfo != null;
					if (flag3)
					{
						num = mainPlayerInfo[obj["att"]["mulatt"]] * num;
					}
				}
				result = num;
			}
			return result;
		}

		public string GetExattStr(int exid, string name, float add)
		{
			string languageText = LanguagePack.getLanguageText("equip", "exatt");
			string text = LanguagePack.getLanguageText("equip", "ext_" + exid);
			bool flag = add < 0f;
			if (flag)
			{
				add = -add;
			}
			text = DebugTrace.Printf(text, new string[]
			{
				add.ToString()
			});
			string str = text + languageText;
			Variant mainPlayerInfo = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			Variant attShowVal = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetAttShowVal();
			Variant baseAtt = (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.GetBaseAtt(mainPlayerInfo["carr"]._int);
			Variant carrlvl = (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.GetCarrlvl(mainPlayerInfo["carr"]._int);
			string text2 = "";
			string text3 = "";
			bool flag2 = baseAtt != null && attShowVal != null && attShowVal._arr.IndexOf(name) >= 0;
			if (flag2)
			{
				int num = 0;
				bool flag3 = name == "hp_mul";
				if (flag3)
				{
					num = (int)((float)(baseAtt["hp"] + baseAtt["conhp"] * mainPlayerInfo["con"] + baseAtt["lvlhp"] * (mainPlayerInfo["level"] - 1)) * add / 100f);
					text3 = num.ToString();
				}
				else
				{
					bool flag4 = name == "matk_mul";
					if (flag4)
					{
						int num2 = 0;
						int num3 = 0;
						foreach (Variant current in carrlvl["att_matk"]._arr)
						{
							num2 += mainPlayerInfo[current["name"]] / current["min"];
							num3 += mainPlayerInfo[current["name"]] / current["max"];
						}
						num = (int)((float)num2 * add / 100f);
						text3 = num + "~" + (float)num3 * add / 100f;
					}
					else
					{
						bool flag5 = name == "atk_mul";
						if (flag5)
						{
							int num4 = 0;
							int num5 = 0;
							foreach (Variant current2 in carrlvl["att_atk"]._arr)
							{
								num4 += mainPlayerInfo[current2["name"]] / current2["min"];
								num5 += mainPlayerInfo[current2["name"]] / current2["max"];
							}
							num = (int)((float)num4 * add / 100f);
							text3 = num + "~" + (float)num5 * add / 100f;
						}
					}
				}
				bool flag6 = num > 0;
				if (flag6)
				{
					text2 = LanguagePack.getLanguageText("att_mod_name", name);
					text2 = string.Concat(new string[]
					{
						"(",
						text2,
						"+",
						text3,
						")"
					});
				}
			}
			return str + text2;
		}

		public string GetRareattStr(int rareid, string name, float add)
		{
			string languageText = LanguagePack.getLanguageText("equip", "rareatt");
			string text = LanguagePack.getLanguageText("equip", "raret_" + rareid);
			bool flag = add < 0f;
			if (flag)
			{
				add = -add;
			}
			text = DebugTrace.Printf(text, new string[]
			{
				add.ToString()
			});
			string str = text + languageText;
			Variant mainPlayerInfo = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			Variant attShowVal = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetAttShowVal();
			Variant baseAtt = (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.GetBaseAtt(mainPlayerInfo["carr"]._int);
			Variant carrlvl = (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.GetCarrlvl(mainPlayerInfo["carr"]._int);
			string text2 = "";
			string text3 = "";
			bool flag2 = baseAtt && attShowVal && attShowVal._arr.IndexOf(name) >= 0;
			if (flag2)
			{
				int num = 0;
				bool flag3 = name == "hp_mul";
				if (flag3)
				{
					num = (int)((float)(baseAtt["hp"] + baseAtt["conhp"] * mainPlayerInfo["con"] + baseAtt["lvlhp"] * (mainPlayerInfo["level"] - 1)) * add / 100f);
					text3 = num.ToString();
				}
				else
				{
					bool flag4 = name == "matk_mul";
					if (flag4)
					{
						int num2 = 0;
						int num3 = 0;
						foreach (Variant current in carrlvl["att_matk"]._arr)
						{
							num2 += mainPlayerInfo[current["name"]] / current["min"];
							num3 += mainPlayerInfo[current["name"]] / current["max"];
						}
						num = (int)((float)num2 * add / 100f);
						text3 = num + "~" + (float)num3 * add / 100f;
					}
					else
					{
						bool flag5 = name == "atk_mul";
						if (flag5)
						{
							int num4 = 0;
							int num5 = 0;
							foreach (Variant current2 in carrlvl["att_atk"]._arr)
							{
								num4 += mainPlayerInfo[current2["name"]] / current2["min"];
								num5 += mainPlayerInfo[current2["name"]] / current2["max"];
							}
							num = (int)((float)num4 * add / 100f);
							text3 = num + "~" + (float)num5 * add / 100f;
						}
					}
				}
				bool flag6 = num > 0;
				if (flag6)
				{
					text2 = LanguagePack.getLanguageText("att_mod_name", name);
					text2 = string.Concat(new string[]
					{
						"(",
						text2,
						"+",
						text3,
						")"
					});
				}
			}
			return str + text2;
		}

		public string GetLuckStr(string name, float add)
		{
			string languageText = LanguagePack.getLanguageText("equip", "lucky");
			string text = LanguagePack.getLanguageText("att_mod", name);
			text = DebugTrace.Printf(text, new string[]
			{
				add.ToString()
			});
			return text + languageText;
		}

		public string GetFpStr(int fpid, int fplvl, float add)
		{
			string text = LanguagePack.getLanguageText("equip", "add");
			text = DebugTrace.Printf(text, new string[]
			{
				fplvl.ToString()
			});
			string text2 = LanguagePack.getLanguageText("equip", "add_" + fpid);
			bool flag = add < 0f;
			if (flag)
			{
				add = -add;
			}
			text2 = DebugTrace.Printf(text2, new string[]
			{
				add.ToString()
			});
			return text + text2;
		}

		public string GetFlagStr(int fpid, int fplvl, float add)
		{
			string text = LanguagePack.getLanguageText("equip", "flagadd");
			text = DebugTrace.Printf(text, new string[]
			{
				fplvl.ToString()
			});
			string text2 = LanguagePack.getLanguageText("equip", "flagadd_" + fpid);
			bool flag = add < 0f;
			if (flag)
			{
				add = -add;
			}
			text2 = DebugTrace.Printf(text2, new string[]
			{
				add.ToString()
			});
			return text + text2;
		}

		public string GetFptStr(int fptid, float add, Variant data = null)
		{
			string text = "";
			string text2 = LanguagePack.getLanguageText("equip", "supftp");
			string text3 = LanguagePack.getLanguageText("equip", "advanced_" + fptid);
			bool flag = add < 0f;
			if (flag)
			{
				add = -add;
			}
			bool flag2 = data != null && data.ContainsKey("fptlvl");
			if (flag2)
			{
				text = data["fptlvl"];
			}
			text2 = DebugTrace.Printf(text2, new string[]
			{
				text
			});
			text3 = DebugTrace.Printf(text3, new string[]
			{
				add.ToString()
			});
			return text2 + text3;
		}

		public Variant GetFptEmptyStrData(Variant data, Variant conf, int fptgroup_id)
		{
			bool flag = data == null || conf == null;
			Variant result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string val = "";
				Variant variant = new Variant();
				uint colorByType = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetColorByType("fpt");
				bool flag2 = conf.ContainsKey("sup_frg_att_grp");
				if (flag2)
				{
					int @int = conf["sup_frg_att_grp"]._int;
					variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.GetSupfrgattgrpById(@int);
				}
				bool flag3 = variant.Values != null && variant["group"][fptgroup_id] != null;
				if (flag3)
				{
					bool flag4 = this.CheckAllChk(data, variant);
					if (flag4)
					{
						int int2 = variant["group"][fptgroup_id]["act_flvl"]._int;
						string text = LanguagePack.getLanguageText("equip", "flvl_fpt");
						bool flag5 = data["flvl"] < int2;
						if (flag5)
						{
							text = DebugTrace.Printf(text, new string[]
							{
								"+" + int2
							});
							val = text;
							colorByType = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetColorByType("sup_frg_att_grp");
						}
						else
						{
							text = DebugTrace.Printf(text, new string[]
							{
								""
							});
							val = text;
						}
					}
				}
				Variant variant2 = new Variant();
				variant2["str"] = val;
				variant2["color"] = colorByType;
				result = variant2;
			}
			return result;
		}

		public string GetSuitInfo(Variant data, Variant conf, string color = "0xffffff")
		{
			string text = "";
			uint @uint = conf["suitid"]._uint;
			bool flag = @uint == 0u;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				Variant suitConf = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.GetSuitConf(@uint);
				bool flag2 = suitConf == null;
				if (flag2)
				{
					result = text;
				}
				else
				{
					string confNomalStr = this.GetConfNomalStr(suitConf["desc"]);
					text = string.Concat(new string[]
					{
						text,
						"<txt text=\"",
						confNomalStr,
						"\" size='12' color='",
						color,
						"' bold='true'/>",
						this._newline
					});
					uint num = 0u;
					Variant mainPlayerInfo = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
					bool flag3 = mainPlayerInfo == null || mainPlayerInfo["equip"] == null;
					if (flag3)
					{
						result = text;
					}
					else
					{
						Variant variant = mainPlayerInfo["equip"];
						string str = suitConf["ids"]._str;
						Variant variant2 = GameTools.split(str, ",", 1u);
						Variant variant3 = new Variant();
						uint num2 = 0u;
						while ((ulong)num2 < (ulong)((long)variant2.Count))
						{
							bool flag4 = variant2[(int)num2]._str == "";
							if (flag4)
							{
								break;
							}
							uint num3 = variant2[(int)num2];
							Variant variant4 = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(num3);
							bool flag5 = variant4 == null;
							if (!flag5)
							{
								string text2 = this.GetPosName(variant4["conf"]);
								string text3 = "0x777777";
								foreach (Variant current in variant._arr)
								{
									bool flag6 = current["tpid"] == num3 && variant3._arr.IndexOf(current["id"]) == -1;
									if (flag6)
									{
										num += 1u;
										text3 = "0xffffff";
										variant3._arr.Add(current["id"]);
										break;
									}
								}
								text2 += " ";
								text = string.Concat(new string[]
								{
									text,
									"<txt text='",
									text2,
									"' size='12' color='",
									text3,
									"'/>"
								});
							}
							num2 += 1u;
						}
						text += this._newline;
						Variant variant5 = new Variant();
						variant5 = suitConf["att"];
						uint num4 = 0u;
						while ((ulong)num4 < (ulong)((long)variant5.Count))
						{
							Variant variant6 = variant5[(int)num4];
							string text4 = "";
							Variant variant7 = new Variant();
							foreach (string current2 in variant6.Keys)
							{
								string suitString = this.getSuitString(current2, variant6[current2]);
								bool flag7 = suitString == "";
								if (!flag7)
								{
									bool flag8 = current2.IndexOf("min") >= 0;
									if (flag8)
									{
										variant7[0] = suitString;
									}
									else
									{
										bool flag9 = current2.IndexOf("max") >= 0;
										if (flag9)
										{
											variant7[1] = "," + suitString;
										}
										else
										{
											bool flag10 = text4 != "";
											if (flag10)
											{
												text4 += ",";
											}
											text4 += suitString;
										}
									}
								}
							}
							bool flag11 = variant7.Count > 0;
							if (flag11)
							{
								using (List<Variant>.Enumerator enumerator3 = variant7._arr.GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										string str2 = enumerator3.Current;
										text4 += str2;
									}
								}
							}
							bool flag12 = (long)variant6.Count > (long)((ulong)num);
							if (flag12)
							{
								text4 = string.Concat(new object[]
								{
									"<txt text=\"(",
									variant6.Count,
									")",
									text4,
									"\" size='12' color='0x777777'/>"
								});
							}
							else
							{
								text4 = string.Concat(new object[]
								{
									"<txt text=\"(",
									variant6.Count,
									")",
									text4,
									"\" size='12' color='0x66ff00'/>"
								});
							}
							text = text + text4 + "<br/>";
							num4 += 1u;
						}
						bool flag13 = text != "";
						if (flag13)
						{
							text = this._newline + text;
						}
						result = text;
					}
				}
			}
			return result;
		}

		private string getSuitString(string att, Variant value)
		{
			string languageText = LanguagePack.getLanguageText("equip", "allstates");
			string languageText2 = LanguagePack.getLanguageText("equip", "allskills");
			string text = LanguagePack.getLanguageText("att_mod", att);
			bool flag = text == "" || text == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				Variant variant = new Variant();
				Variant variant2 = new Variant();
				bool flag2 = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.IsThousand(att);
				if (flag2)
				{
					float num = (float)((double)value * 0.1);
					text = string.Format(text, num);
				}
				else
				{
					text = string.Format(text, value);
				}
				result = text;
			}
			return result;
		}

		public string GetEqpColor(Variant data, Variant conf = null)
		{
			string text = "0xffffff";
			bool flag = data == null;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				bool flag2 = conf == null;
				if (flag2)
				{
					Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(data["tpid"]._uint);
					bool flag3 = variant == null || variant["conf"] == null;
					if (flag3)
					{
						result = text;
						return result;
					}
					conf = variant["conf"];
				}
				int num = 0;
				bool flag4 = data.ContainsKey("flvl");
				if (flag4)
				{
					num = data["flvl"]._int;
					bool flag5 = num >= 7;
					if (flag5)
					{
						text = "0xffcc19";
					}
				}
				bool flag6 = data.ContainsKey("fp") && (data["fp"] | 61680) > 61680;
				if (flag6)
				{
					bool flag7 = num < 7;
					if (flag7)
					{
						text = "0x66b2ff";
					}
				}
				bool flag8 = data.ContainsKey("flag") && data["flag"] > 0;
				if (flag8)
				{
					bool flag9 = 2 == data["flag"]._int && conf != null;
					if (flag9)
					{
						bool flag10 = conf.ContainsKey("skil") && num < 7;
						if (flag10)
						{
							text = "0x66b2ff";
						}
					}
					else
					{
						bool flag11 = 1 == data["flag"];
						if (flag11)
						{
							text = "0x66b2ff";
						}
					}
				}
				bool flag12 = conf != null && conf.ContainsKey("suitid");
				if (flag12)
				{
					text = "0xffff00";
				}
				result = text;
			}
			return result;
		}

		public uint byQualGetColot(Variant o)
		{
			int num = UIUtility.singleton.isQual(o);
			bool flag = num == 1;
			uint result;
			if (flag)
			{
				result = 16777215u;
			}
			else
			{
				bool flag2 = num == 2;
				if (flag2)
				{
					result = 3059767u;
				}
				else
				{
					bool flag3 = num == 3;
					if (flag3)
					{
						result = 2715608u;
					}
					else
					{
						bool flag4 = num == 4;
						if (flag4)
						{
							result = 12077780u;
						}
						else
						{
							result = 16777215u;
						}
					}
				}
			}
			return result;
		}

		public uint GetEqpColorUint(Variant data, Variant conf = null)
		{
			bool flag = data == null;
			uint result;
			if (flag)
			{
				result = 16777215u;
			}
			else
			{
				bool flag2 = conf == null;
				if (flag2)
				{
					bool flag3 = data.ContainsKey("data") && data["item_id"] != null;
					int tpid;
					if (flag3)
					{
						tpid = data["item_id"];
					}
					else
					{
						tpid = (data.ContainsKey("tpid") ? data["tpid"]._int : 0);
					}
					Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf((uint)tpid);
					bool flag4 = variant == null || variant["conf"] == null;
					if (flag4)
					{
						result = 16777215u;
						return result;
					}
					conf = variant["conf"];
				}
				result = DisplayUtil.singleton.GetEqpColorUint(data, conf, false);
			}
			return result;
		}

		public string GetPosName(Variant data)
		{
			string text = "";
			bool flag = data.ContainsKey("pos");
			if (flag)
			{
				bool flag2 = data.ContainsKey("subtp");
				if (flag2)
				{
					text = LanguagePack.getLanguageText("pos", "p" + data["pos"] + "_st" + data["subtp"]);
				}
				else
				{
					bool flag3 = data.ContainsKey("takepos") && data["pos"] == 6;
					if (flag3)
					{
						text += LanguagePack.getLanguageText("pos", "takepos" + data["takepos"]);
					}
					text += LanguagePack.getLanguageText("pos", "p" + data["pos"]);
				}
			}
			return text;
		}

		public string getItemAttchkStr(Variant attchk)
		{
			string text = "";
			bool flag = attchk == null;
			if (flag)
			{
				text = LanguagePack.getLanguageText("equip", "noLimit");
			}
			else
			{
				for (int i = 0; i < attchk.Count; i++)
				{
					Variant attchk2 = attchk[i];
					text += this.getAttchkStr(attchk2);
				}
			}
			return text;
		}

		private string getAttchkStr(Variant attchk)
		{
			string text = attchk["name"];
			string text2 = "";
			int num = 0;
			Variant mainPlayerInfo = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			bool flag = mainPlayerInfo != null;
			string result;
			if (flag)
			{
				bool flag2 = text == "carr";
				if (flag2)
				{
					string languageText = LanguagePack.getLanguageText("equip", "state_name_carr");
					text2 = text2 + "<txt text='" + languageText + "' color='0xffffff'/>";
					Variant variant = new Variant();
					variant["carr"] = mainPlayerInfo["carr"];
					variant["carrlvl"] = mainPlayerInfo["carrlvl"];
					text2 += this.DecodeCarr(attchk["and"]._int, variant, true);
					text2 += "<br/>";
					result = text2;
					return result;
				}
				bool flag3 = mainPlayerInfo.ContainsKey(text);
				if (flag3)
				{
					num = mainPlayerInfo[text];
					text2 = LanguagePack.getLanguageText("equip", "state_name_" + text);
				}
			}
			string text3 = "";
			bool flag4 = attchk.ContainsKey("min") && attchk.ContainsKey("max");
			if (flag4)
			{
				bool arg_172_0 = num >= attchk["min"] && num <= attchk["max"];
				text3 = attchk["min"] + "~" + attchk["max"];
			}
			else
			{
				bool flag5 = attchk.ContainsKey("min");
				if (flag5)
				{
					bool flag6 = num >= attchk["min"];
					text3 = attchk["min"]._str;
				}
				else
				{
					bool flag7 = attchk.ContainsKey("equal");
					if (flag7)
					{
						bool flag6 = num == attchk["equal"];
						text3 = LanguagePack.getLanguageText("equip", "state_type_equal");
						text3 = DebugTrace.Printf(text3, new string[]
						{
							attchk["equal"]._str
						});
					}
					else
					{
						bool flag8 = attchk.ContainsKey("max");
						if (flag8)
						{
							text3 = LanguagePack.getLanguageText("equip", "state_type_max");
							text3 = DebugTrace.Printf(text3, new string[]
							{
								attchk["min"]._str
							});
							bool flag6 = num <= attchk["min"];
						}
					}
				}
			}
			bool flag9 = text == "carrlvl";
			if (flag9)
			{
				text2 = DebugTrace.Printf(text2, new string[]
				{
					text3
				});
			}
			else
			{
				text2 += text3;
			}
			result = text2;
			return result;
		}

		public string GetItemDesc(Variant data)
		{
			Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(data["tpid"]._uint);
			bool flag = variant == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = "";
				string languageText = LanguagePack.getLanguageText("equip", "noLimit");
				bool flag2 = data.ContainsKey("bnd") && data["bnd"]._bool;
				if (flag2)
				{
					bool flag3 = data.ContainsKey("pick") && data["pick"];
					string languageText2;
					if (flag3)
					{
						languageText2 = LanguagePack.getLanguageText("equip", "bond");
					}
					else
					{
						languageText2 = LanguagePack.getLanguageText("equip", "pickbond");
					}
					text = text + "<br/>" + languageText2;
				}
				string itemAttchkStr = this.getItemAttchkStr(variant["conf"]["attchk"]);
				bool flag4 = itemAttchkStr != languageText;
				if (flag4)
				{
					bool flag5 = itemAttchkStr != "";
					if (flag5)
					{
						text = text + "<br/>" + itemAttchkStr;
					}
				}
				bool flag6 = 1 == variant["tp"] || 2 == variant["tp"];
				if (flag6)
				{
					string itemCost = this.getItemCost(variant["conf"]["cost"]);
					bool flag7 = itemCost != "";
					if (flag7)
					{
						text = text + "<br/>" + itemCost;
					}
				}
				result = text;
			}
			return result;
		}

		private string getItemCost(Variant costa)
		{
			bool flag = costa == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				Variant variant = costa[0];
				string text = "";
				string str = "";
				Variant variant2 = new Variant();
				string languageText = LanguagePack.getLanguageText("equip", "t_yb");
				string languageText2 = LanguagePack.getLanguageText("equip", "t_points");
				string languageText3 = LanguagePack.getLanguageText("equip", "t_useCost");
				bool flag2 = variant.ContainsKey("yb") || variant.ContainsKey("ybpt");
				if (flag2)
				{
					string text2 = "";
					string text3 = "";
					bool flag3 = true;
					bool flag4 = variant.ContainsKey("yb");
					if (flag4)
					{
						uint @uint = variant["yb"]._uint;
					}
					bool flag5 = variant.ContainsKey("ybpt");
					if (flag5)
					{
						uint uint2 = variant["ybpt"]._uint;
					}
					bool flag6 = flag3;
					if (flag6)
					{
						text = text + "<txt text='" + languageText3 + "'/>";
					}
					else
					{
						text = text + "<txt text='" + languageText3 + "' size='12' color='0xff2828'/>";
					}
					bool flag7 = text2 != "";
					if (flag7)
					{
						text += text2;
					}
					bool flag8 = text3 != "";
					if (flag8)
					{
						bool flag9 = text2 != "";
						if (flag9)
						{
							text += "<txt text='，'/>";
						}
						text += text3;
					}
					bool flag10 = text != "";
					if (flag10)
					{
						text += this._newline;
					}
				}
				bool flag11 = variant.ContainsKey("itmtpid");
				if (flag11)
				{
					uint uint3 = variant["itmtpid"]._uint;
					Variant variant3 = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(uint3);
					Variant variant4 = variant3["conf"];
					bool flag12 = variant4 != null;
					if (flag12)
					{
						str = languageText3 + LanguagePack.getLanguageText("items_xml", uint3.ToString());
					}
					uint num = (this._uiClient.g_gameM as muLGClient).g_itemsCT.pkg_get_item_count_bytpid(uint3);
					bool flag13 = num <= 0u;
					if (flag13)
					{
						text = text + "<txt text='" + str + "' size='12' color='0xff2828'/>";
					}
					else
					{
						text = text + "<txt text='" + str + "'/>";
					}
				}
				bool flag14 = text != "";
				if (flag14)
				{
					text += this._newline;
				}
				result = text;
			}
			return result;
		}

		public int GetForgeAttMaxlvl(string name)
		{
			Variant forgeAttLvlById = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.GetForgeAttLvlById(name);
			int result = 0;
			bool flag = forgeAttLvlById != null;
			if (flag)
			{
				result = forgeAttLvlById["att_lvl"].Count - 1;
			}
			return result;
		}

		public string strGetSkillDescText(Variant data, Variant conf = null)
		{
			string text = "";
			uint @uint = data["skid"]._uint;
			bool flag = @uint == 0u;
			string result;
			if (flag)
			{
				string languageText = LanguagePack.getLanguageText("skill", "normalAttack");
				text += languageText;
				result = text;
			}
			else
			{
				string languageText2 = LanguagePack.getLanguageText("equip", "equipNeed");
				string languageText3 = LanguagePack.getLanguageText("skill", "powerTime");
				bool flag2 = conf == null;
				if (flag2)
				{
					conf = (this._uiClient.g_gameConfM as muCLientConfig).svrSkillConf.get_skill_conf(@uint);
					bool flag3 = conf == null;
					if (flag3)
					{
						result = "";
						return result;
					}
				}
				Variant lv = conf["lv"][data["sklvl"]];
				bool flag4 = data.ContainsKey("eqptp") && data["eqptp"] != 0;
				if (flag4)
				{
					Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(data["eqptp"]._uint);
					bool flag5 = variant != null;
					if (flag5)
					{
						text = text + languageText2 + LanguagePack.getLanguageText("items_xml", data["eqptp"]);
					}
				}
				string str = this.strGetSkillCostText(lv);
				text += str;
				string text2 = this.strGetSkillResultText(lv, data["spt"]);
				bool flag6 = text2 != "";
				if (flag6)
				{
					text += "\n";
					text += text2;
				}
				result = text;
			}
			return result;
		}

		public string GetContentSkill(Variant data, int lvl, Variant conf = null)
		{
			string text = "";
			uint @uint = data["skid"]._uint;
			bool flag = @uint == 0u;
			string result;
			if (flag)
			{
				result = LanguagePack.getLanguageText("skill", "normalAttack");
			}
			else
			{
				string languageText = LanguagePack.getLanguageText("equip", "equipNeed");
				bool flag2 = conf == null;
				if (flag2)
				{
					conf = (this._uiClient.g_gameConfM as muCLientConfig).svrSkillConf.get_skill_conf(@uint);
					bool flag3 = conf == null;
					if (flag3)
					{
						result = "";
						return result;
					}
				}
				Variant lv = conf["lv"][lvl];
				bool flag4 = data.ContainsKey("eqptp") && data["eqptp"] != 0;
				if (flag4)
				{
					Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(data["eqptp"]._uint);
					bool flag5 = variant != null;
					if (flag5)
					{
						text = text + this._newline + languageText + LanguagePack.getLanguageText("items_xml", data["eqptp"]);
						text += this._newline;
					}
				}
				string skillCost = this.GetSkillCost(lv);
				text = text + this._newline + skillCost;
				bool flag6 = lvl >= 1;
				if (flag6)
				{
					string skillCooldown = this.GetSkillCooldown(lvl, conf);
					text += skillCooldown;
				}
				string skillResult = this.getSkillResult(lv, data["spt"]);
				bool flag7 = skillResult != "";
				if (flag7)
				{
					text = text + this._newline + skillResult;
				}
				result = text;
			}
			return result;
		}

		public string GetSkillCooldown(int lvl, Variant conf)
		{
			float num = (float)((conf["lv"][lvl.ToString()].ContainsKey("cd_spec") && conf["lv"][lvl.ToString()]["cd_spec"] != null) ? (conf["lv"][lvl.ToString()]["cd_spec"] / 10) : (conf["lv"][lvl.ToString()]["cd"] / 10));
			return DebugTrace.Printf(LanguagePack.getLanguageText("skill", "cooldown"), new string[]
			{
				num.ToString()
			});
		}

		public string GetSkillDesc(Variant conf)
		{
			string text = this.GetConfNomalStr(conf["desc"]);
			text = ((text != null) ? text : conf["desc"]._str);
			return DebugTrace.Printf(LanguagePack.getLanguageText("skill", "skilldesc"), new string[]
			{
				text
			});
		}

		public string GetSkillCost(Variant lv)
		{
			bool flag = lv == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = lv["act_c"] <= 0 && lv["agry_c"] <= 0 && lv["hp_c"] == 0 && lv["mp_c"] == 0;
				if (flag2)
				{
					result = "";
				}
				else
				{
					string text = "";
					bool flag3 = lv["act_c"] > 0;
					if (flag3)
					{
						text = DebugTrace.Printf(LanguagePack.getLanguageText("skill", "movePoint"), new string[]
						{
							lv["act_c"]._str
						});
					}
					bool flag4 = lv["agry_c"] > 0;
					if (flag4)
					{
						text = DebugTrace.Printf(LanguagePack.getLanguageText("skill", "angerPoint"), new string[]
						{
							lv["agry_c"]._str
						});
					}
					bool flag5 = lv["hp_c"] > 0;
					if (flag5)
					{
						text = DebugTrace.Printf(LanguagePack.getLanguageText("skill", "strengthPoint"), new string[]
						{
							lv["hp_c"]._str
						});
					}
					bool flag6 = lv["mp_c"] > 0;
					if (flag6)
					{
						text = DebugTrace.Printf(LanguagePack.getLanguageText("skill", "powerPoint"), new string[]
						{
							lv["mp_c"]._str
						});
					}
					result = DebugTrace.Printf(LanguagePack.getLanguageText("skill", "cost"), new string[]
					{
						text
					});
				}
			}
			return result;
		}

		private string strGetSkillCostText(Variant lv)
		{
			string languageText = LanguagePack.getLanguageText("skill", "movePoint");
			string languageText2 = LanguagePack.getLanguageText("skill", "angerPoint");
			string languageText3 = LanguagePack.getLanguageText("skill", "strengthPoint");
			string languageText4 = LanguagePack.getLanguageText("skill", "powerPoint");
			bool flag = lv == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = lv["act_c"] <= 0 && lv["agry_c"] <= 0 && lv["hp_c"] == 0 && lv["mp_c"] == 0;
				if (flag2)
				{
					result = "";
				}
				else
				{
					string arg = "";
					bool flag3 = lv["act_c"] > 0;
					if (flag3)
					{
						arg = string.Format(languageText, lv["act_c"]);
					}
					bool flag4 = lv["agry_c"] > 0;
					if (flag4)
					{
						arg = string.Format(languageText2, lv["agry_c"]);
					}
					bool flag5 = lv["hp_c"] > 0;
					if (flag5)
					{
						arg = string.Format(languageText3, lv["hp_c"]);
					}
					bool flag6 = lv["mp_c"] > 0;
					if (flag6)
					{
						arg = string.Format(languageText4, lv["mp_c"]);
					}
					result = string.Format(LanguagePack.getLanguageText("skill", "cost"), arg);
				}
			}
			return result;
		}

		private string strGetSkillResultText(Variant lv, float spt)
		{
			string languageText = LanguagePack.getLanguageText("skill", "tomyself");
			string languageText2 = LanguagePack.getLanguageText("skill", "jump");
			string languageText3 = LanguagePack.getLanguageText("skill", "start_jump");
			string languageText4 = LanguagePack.getLanguageText("skill", "end_jump");
			string languageText5 = LanguagePack.getLanguageText("skill", "toTarget");
			string languageText6 = LanguagePack.getLanguageText("skill", "all");
			string languageText7 = LanguagePack.getLanguageText("skill", "toTargetAround");
			string languageText8 = LanguagePack.getLanguageText("skill", "entries");
			bool flag = lv == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = "";
				bool flag2 = lv.ContainsKey("sres");
				if (flag2)
				{
					text += languageText;
					text += this.strGetDmgStrText(lv["sres"], spt);
				}
				bool flag3 = lv.ContainsKey("teleport");
				if (flag3)
				{
					string languageText9 = LanguagePack.getLanguageText("skill", "teleport");
					text += languageText9;
				}
				bool flag4 = lv.ContainsKey("jump");
				if (flag4)
				{
					text += languageText2;
				}
				bool flag5 = lv.ContainsKey("tres");
				if (flag5)
				{
					bool flag6 = lv.ContainsKey("jump");
					if (flag6)
					{
						text += languageText3;
					}
					bool flag7 = lv.ContainsKey("trang");
					if (flag7)
					{
						Variant variant = lv["trang"];
						bool flag8 = variant.ContainsKey("cirang") && variant["cirang"] > 0;
						if (flag8)
						{
							string languageText10 = LanguagePack.getLanguageText("skill", "circle");
							text += string.Format(languageText10, variant["cirang"]);
						}
						bool flag9 = variant.ContainsKey("fan") && variant["fan"];
						if (flag9)
						{
							string languageText11 = LanguagePack.getLanguageText("skill", "fan_shaped");
							text += string.Format(languageText11, variant["fan"]["rang"]);
						}
						bool flag10 = variant.ContainsKey("ray") && variant["ray"];
						if (flag10)
						{
							string languageText12 = LanguagePack.getLanguageText("skill", "ray");
							text += string.Format(languageText12, variant["ray"]["dist"]);
						}
						bool flag11 = lv["trang"].ContainsKey("maxi") && lv["trang"]["maxi"] != 0;
						if (flag11)
						{
							text += string.Format(languageText8, lv["trang"]["maxi"]);
						}
						text += "\n";
					}
					uint num = 0u;
					while ((ulong)num < (ulong)((long)lv["tres"].Count))
					{
						text += this.strGetDmgStrText(lv["tres"][num], spt);
						num += 1u;
					}
				}
				bool flag12 = lv.ContainsKey("jump") && lv["jump"].ContainsKey("tres") && lv["jump"]["tres"].Count > 0;
				if (flag12)
				{
					text = text + this._newline + languageText4;
					bool flag13 = lv["jump"].ContainsKey("trang");
					if (flag13)
					{
						text += string.Format(languageText7, lv["jump"]["trang"]["cirang"]);
						bool flag14 = !lv["jump"]["trang"].ContainsKey("maxi") || lv["jump"]["trang"]["maxi"] == 0;
						if (flag14)
						{
							text += languageText6;
						}
						else
						{
							text += string.Format(languageText8, lv["jump"]["trang"]["maxi"]);
						}
					}
					uint num2 = 0u;
					while ((ulong)num2 < (ulong)((long)lv["jump"]["tres"].Count))
					{
						text += this.strGetDmgStrText(lv["jump"]["tres"][num2], spt);
						num2 += 1u;
					}
				}
				result = text;
			}
			return result;
		}

		private string getSkillResult(Variant lv, float spt)
		{
			string languageText = LanguagePack.getLanguageText("skill", "tomyself");
			string languageText2 = LanguagePack.getLanguageText("skill", "jump");
			string languageText3 = LanguagePack.getLanguageText("skill", "start_jump");
			string languageText4 = LanguagePack.getLanguageText("skill", "end_jump");
			string languageText5 = LanguagePack.getLanguageText("skill", "toTarget");
			string languageText6 = LanguagePack.getLanguageText("skill", "all");
			string languageText7 = LanguagePack.getLanguageText("skill", "toTargetAround");
			string languageText8 = LanguagePack.getLanguageText("skill", "entries");
			bool flag = lv == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = "";
				bool flag2 = lv.ContainsKey("sres");
				if (flag2)
				{
					text = text + "<txt text=\"" + languageText + "\" size='12' color='0xffffff'/>";
					text += this.getDmgStr(lv["sres"], spt);
				}
				bool flag3 = lv.ContainsKey("teleport");
				if (flag3)
				{
					string languageText9 = LanguagePack.getLanguageText("skill", "teleport");
					text = text + "<txt text=\"" + languageText9 + "\" size='12' color='0x489df4'/>";
					text += this._newline;
				}
				bool flag4 = lv.ContainsKey("jump");
				if (flag4)
				{
					text = text + "<txt text=\"" + languageText2 + "\" size='12' color='0x489df4'/>";
					text += this._newline;
				}
				bool flag5 = lv.ContainsKey("tres");
				if (flag5)
				{
					bool flag6 = lv.ContainsKey("jump");
					if (flag6)
					{
						text += this._newline;
						text = text + "<txt text=\"" + languageText3 + "\"/>";
					}
					bool flag7 = lv.ContainsKey("trang");
					if (flag7)
					{
						Variant variant = lv["trang"];
						bool flag8 = variant.ContainsKey("cirang") && variant["cirang"] > 0;
						if (flag8)
						{
							string languageText10 = LanguagePack.getLanguageText("skill", "circle");
							text = text + "<txt text=\"" + string.Format(languageText10, variant["cirang"]) + "\" size='12' color='0x489df4'/>";
						}
						bool flag9 = variant.ContainsKey("fan") && variant["fan"];
						if (flag9)
						{
							string languageText11 = LanguagePack.getLanguageText("skill", "fan_shaped");
							text = text + "<txt text=\"" + string.Format(languageText11, variant["fan"]["rang"]) + "\" size='12' color='0x489df4'/>";
						}
						bool flag10 = variant.ContainsKey("ray") && variant["ray"];
						if (flag10)
						{
							string languageText12 = LanguagePack.getLanguageText("skill", "ray");
							text = text + "<txt text=\"" + string.Format(languageText12, variant["ray"]["dist"]) + "\" size='12' color='0x489df4'/>";
						}
						bool flag11 = lv["trang"].ContainsKey("maxi") && lv["trang"]["maxi"] != 0;
						if (flag11)
						{
							text = text + "<txt text=\"" + string.Format(languageText8, lv["trang"]["maxi"]) + "\" size='12' color='0x489df4'/>";
						}
					}
					bool flag12 = text != "";
					if (flag12)
					{
						text = text + this._newline + this._newline;
					}
					uint num = 0u;
					while ((ulong)num < (ulong)((long)lv["tres"].Count))
					{
						bool flag13 = num > 0u;
						if (flag13)
						{
							text += this._newline;
						}
						text += this.getDmgStr(lv["tres"][num], spt);
						num += 1u;
					}
				}
				bool flag14 = lv.ContainsKey("jump") && lv["jump"].ContainsKey("tres") && lv["jump"]["tres"].Count > 0;
				if (flag14)
				{
					text = text + this._newline + languageText4;
					bool flag15 = lv["jump"].ContainsKey("trang");
					if (flag15)
					{
						text += string.Format(languageText7, lv["jump"]["trang"]["cirang"]);
						bool flag16 = !lv["jump"]["trang"].ContainsKey("maxi") || lv["jump"]["trang"]["maxi"] == 0;
						if (flag16)
						{
							text += languageText6;
						}
						else
						{
							text += string.Format(languageText8, lv["jump"]["trang"]["maxi"]);
						}
					}
					uint num2 = 0u;
					while ((ulong)num2 < (ulong)((long)lv["jump"]["tres"].Count))
					{
						text += this.getDmgStr(lv["jump"]["tres"][num2], spt);
						num2 += 1u;
					}
				}
				result = text;
			}
			return result;
		}

		private string getDmgStr(Variant res, float spt)
		{
			string languageText = LanguagePack.getLanguageText("skill", "me");
			string languageText2 = LanguagePack.getLanguageText("skill", "ally");
			string languageText3 = LanguagePack.getLanguageText("skill", "enemy");
			string languageText4 = LanguagePack.getLanguageText("skill", "neutral");
			string languageText5 = LanguagePack.getLanguageText("skill", "allPerson");
			string text = "";
			bool flag = res.ContainsKey("aff");
			if (flag)
			{
				bool flag2 = (res["aff"] & 1) == 1;
				if (flag2)
				{
					text = text + "<txt text=\"" + languageText + "\" size='12' color='0x489df4'/>";
				}
				bool flag3 = (res["aff"] & 2) == 2;
				if (flag3)
				{
					text = text + "<txt text=\"" + languageText2 + "\" size='12' color='0x489df4'/>";
				}
				bool flag4 = (res["aff"] & 4) == 4;
				if (flag4)
				{
					text = text + "<txt text=\"" + languageText3 + "\" size='12' color='0x489df4'/>";
				}
				bool flag5 = (res["aff"] & 8) == 8;
				if (flag5)
				{
					text = text + "<txt text=\"" + languageText4 + "\" size='12' color='0x489df4'/>";
				}
				text += this._newline;
			}
			string text2 = "";
			string languageText6 = LanguagePack.getLanguageText("skill", "outPower");
			string languageText7 = LanguagePack.getLanguageText("skill", "insidePower");
			string languageText8 = LanguagePack.getLanguageText("skill", "pwoerType1");
			string languageText9 = LanguagePack.getLanguageText("skill", "pwoerType2");
			string languageText10 = LanguagePack.getLanguageText("skill", "pwoerType3");
			string languageText11 = LanguagePack.getLanguageText("skill", "pwoerType4");
			string languageText12 = LanguagePack.getLanguageText("skill", "attackHurt");
			string languageText13 = LanguagePack.getLanguageText("skill", "immediacyHurt");
			string languageText14 = LanguagePack.getLanguageText("skill", "addStrength");
			string languageText15 = LanguagePack.getLanguageText("skill", "t_ahurt");
			string languageText16 = LanguagePack.getLanguageText("skill", "t_bhurt");
			string languageText17 = LanguagePack.getLanguageText("skill", "t_baseHt_hurt");
			bool flag6 = res.ContainsKey("noratk") && res["noratk"] == 1;
			string text3;
			if (flag6)
			{
				text3 = languageText15;
			}
			else
			{
				text3 = languageText16;
			}
			bool flag7 = res.ContainsKey("dmg_min") && res.ContainsKey("dmg_max");
			if (flag7)
			{
				bool flag8 = res["dmg_min"] == 0 && res["dmg_max"] == 0;
				if (flag8)
				{
					text2 += " ";
				}
				bool flag9 = res["dmg_max"] > 0;
				if (flag9)
				{
					text2 = text2 + "<txt text=\"" + string.Format(languageText17, res["dmg_min"], res["dmg_max"]) + "\" color='0xffffff'/>";
				}
			}
			bool flag10 = res.ContainsKey("nag_dmg") && res["nag_dmg"] > 0;
			if (flag10)
			{
				bool flag11 = text2 != "";
				if (flag11)
				{
					text2 += this._newline;
				}
				text2 = string.Concat(new string[]
				{
					text2,
					text3,
					languageText8,
					languageText12,
					this.skillDmgStr(res["nag_dmg"], spt),
					" "
				});
			}
			bool flag12 = res.ContainsKey("pos_dmg") && res["pos_dmg"] > 0;
			if (flag12)
			{
				bool flag13 = text2 != "";
				if (flag13)
				{
					text2 += this._newline;
				}
				text2 = string.Concat(new string[]
				{
					text2,
					text3,
					languageText9,
					languageText12,
					this.skillDmgStr(res["pos_dmg"], spt),
					" "
				});
			}
			bool flag14 = res.ContainsKey("voi_dmg") && res["voi_dmg"] > 0;
			if (flag14)
			{
				bool flag15 = text2 != "";
				if (flag15)
				{
					text2 += this._newline;
				}
				text2 = string.Concat(new string[]
				{
					text2,
					text3,
					languageText10,
					languageText12,
					this.skillDmgStr(res["voi_dmg"], spt),
					" "
				});
			}
			bool flag16 = res.ContainsKey("poi_dmg") && res["poi_dmg"] > 0;
			if (flag16)
			{
				bool flag17 = text2 != "";
				if (flag17)
				{
					text2 += this._newline;
				}
				text2 = string.Concat(new string[]
				{
					text2,
					text3,
					languageText11,
					languageText12,
					this.skillDmgStr(res["poi_dmg"], spt),
					" "
				});
			}
			string languageText18 = LanguagePack.getLanguageText("skill", "t_baseHt_NeiGong");
			string languageText19 = LanguagePack.getLanguageText("skill", "t_baseHt_WaiGong");
			bool flag18 = text2 != "";
			if (flag18)
			{
				float num = res["noratk"]._float * 100f;
				bool flag19 = num > 0f;
				if (flag19)
				{
					string text4 = num.ToString() + "%";
					bool flag20 = res.ContainsKey("plyatk") && res["plyatk"] > 0;
					if (flag20)
					{
						text = string.Concat(new string[]
						{
							text,
							"<txt text=\"",
							text4,
							languageText19,
							"\" color='0xffffff'/>"
						});
					}
					else
					{
						int @int = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["carr"]._int;
						Variant carrlvl = (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.GetCarrlvl(@int);
						uint num2 = carrlvl["baseatt"][0]["atktp"];
						bool flag21 = 2u == num2;
						if (flag21)
						{
							text = string.Concat(new string[]
							{
								text,
								"<txt text=\"",
								text4,
								languageText18,
								"\" color='0xffffff'/>"
							});
						}
						else
						{
							text = string.Concat(new string[]
							{
								text,
								"<txt text=\"",
								text4,
								languageText19,
								"\" color='0xffffff'/>"
							});
						}
					}
				}
			}
			bool flag22 = res.ContainsKey("hp_dmg") && res["hp_dmg"] != 0;
			if (flag22)
			{
				bool flag23 = text2 != "";
				if (flag23)
				{
					text2 += this._newline;
				}
				bool flag24 = res["hp_dmg"] > 0;
				if (flag24)
				{
					text2 = string.Concat(new string[]
					{
						text2,
						"<txt text=\"",
						languageText13,
						"\"/>",
						this.skillDmgStr(res["hp_dmg"], spt)
					});
				}
				else
				{
					text2 = string.Concat(new string[]
					{
						text2,
						"<txt text=\"",
						languageText14,
						"\"/>",
						this.skillDmgStr((float)Math.Abs(res["hp_dmg"]), spt)
					});
				}
			}
			bool flag25 = text2 != "";
			if (flag25)
			{
				text = text + text2 + this._newline;
			}
			string languageText20 = LanguagePack.getLanguageText("skill", "reducePowerValue");
			string languageText21 = LanguagePack.getLanguageText("skill", "reduceAngerValue");
			string languageText22 = LanguagePack.getLanguageText("skill", "targetEffectPercent");
			string languageText23 = LanguagePack.getLanguageText("skill", "continuanceSecond");
			string languageText24 = LanguagePack.getLanguageText("skill", "removeAllStateVerso");
			string languageText25 = LanguagePack.getLanguageText("skill", "removeAllStateFace");
			string languageText26 = LanguagePack.getLanguageText("skill", "removeOneStateVersoRandom");
			string languageText27 = LanguagePack.getLanguageText("skill", "removeOneStateFaceRandom");
			bool flag26 = res.ContainsKey("mp_dmg") && res["mp_dmg"] > 0;
			if (flag26)
			{
				text = string.Concat(new string[]
				{
					text,
					"<txt text=\"",
					languageText20,
					"\"/>",
					this.skillDmgStr(res["mp_dmg"], spt)
				});
				text += this._newline;
			}
			bool flag27 = res.ContainsKey("agry_dmg") && res["agry_dmg"] > 0;
			if (flag27)
			{
				text = string.Concat(new string[]
				{
					text,
					"<txt text=\"",
					languageText21,
					"\"/>",
					this.skillDmgStr(res["agry_dmg"], spt)
				});
				text += this._newline;
			}
			bool flag28 = res.ContainsKey("tar_state") && res["tar_state"] != 0;
			if (flag28)
			{
				Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrSkillConf.get_state_desc(res["tar_state"]);
				string languageText28 = LanguagePack.getLanguageText("buffStateName", variant["name"]);
				text = text + "<txt text=\"" + string.Format(languageText22, languageText28) + "\"/>";
			}
			bool flag29 = res.ContainsKey("state_tm") && res["state_tm"] > 0;
			if (flag29)
			{
				bool flag30 = res["state_tm"] > 600;
				if (flag30)
				{
					text = text + "<txt text=\"" + string.Format(languageText23, this.GetTimeYdms((uint)(res["state_tm"] / 10), null)) + "\"/>";
				}
				else
				{
					string languageText29 = LanguagePack.getLanguageText("time", "sec");
					text = string.Concat(new string[]
					{
						text,
						"<txt text=\"",
						string.Format(languageText23, (float)(res["state_tm"] * 0.1)),
						languageText29,
						"\"/>"
					});
				}
				text += this._newline;
			}
			bool flag31 = res.ContainsKey("rmv_stat");
			if (flag31)
			{
				bool flag32 = res["rmv_stat"] == 1;
				if (flag32)
				{
					text = text + "<txt text=\"" + languageText24 + "\"/>";
					text += this._newline;
				}
				else
				{
					bool flag33 = res["rmv_stat"] == 2;
					if (flag33)
					{
						text = text + "<txt text=\"" + languageText25 + "\"/>";
						text += this._newline;
					}
				}
			}
			bool flag34 = res.ContainsKey("rmv_1stat");
			if (flag34)
			{
				bool flag35 = res["rmv_1stat"] == 1;
				if (flag35)
				{
					text = text + "<txt text=\"" + languageText26 + "\"/>";
					text += "<br/>";
				}
				else
				{
					bool flag36 = res["rmv_1stat"] == 2;
					if (flag36)
					{
						text = text + "<txt text=\"" + languageText27 + "\"/>";
						text += "<br/>";
					}
				}
			}
			return text;
		}

		private string strGetDmgStrText(Variant res, float spt)
		{
			string languageText = LanguagePack.getLanguageText("skill", "me");
			string languageText2 = LanguagePack.getLanguageText("skill", "ally");
			string languageText3 = LanguagePack.getLanguageText("skill", "enemy");
			string languageText4 = LanguagePack.getLanguageText("skill", "neutral");
			string languageText5 = LanguagePack.getLanguageText("skill", "allPerson");
			string text = "";
			bool flag = res.ContainsKey("aff");
			if (flag)
			{
				bool flag2 = (res["aff"] & 1) == 1;
				if (flag2)
				{
					text += languageText;
				}
				bool flag3 = (res["aff"] & 2) == 2;
				if (flag3)
				{
					text += languageText2;
				}
				bool flag4 = (res["aff"] & 4) == 4;
				if (flag4)
				{
					text += languageText3;
				}
				bool flag5 = (res["aff"] & 8) == 8;
				if (flag5)
				{
					text += languageText4;
				}
				text += "\n";
			}
			string text2 = "";
			string languageText6 = LanguagePack.getLanguageText("skill", "outPower");
			string languageText7 = LanguagePack.getLanguageText("skill", "insidePower");
			string languageText8 = LanguagePack.getLanguageText("skill", "pwoerType1");
			string languageText9 = LanguagePack.getLanguageText("skill", "pwoerType2");
			string languageText10 = LanguagePack.getLanguageText("skill", "pwoerType3");
			string languageText11 = LanguagePack.getLanguageText("skill", "pwoerType4");
			string languageText12 = LanguagePack.getLanguageText("skill", "attackHurt");
			string languageText13 = LanguagePack.getLanguageText("skill", "immediacyHurt");
			string languageText14 = LanguagePack.getLanguageText("skill", "addStrength");
			string languageText15 = LanguagePack.getLanguageText("skill", "t_ahurt");
			string languageText16 = LanguagePack.getLanguageText("skill", "t_bhurt");
			string languageText17 = LanguagePack.getLanguageText("skill", "t_baseHt_hurt");
			bool flag6 = res.ContainsKey("noratk") && res["noratk"] == 1;
			string text3;
			if (flag6)
			{
				text3 = languageText15;
			}
			else
			{
				text3 = languageText16;
			}
			bool flag7 = res.ContainsKey("dmg_min") && res.ContainsKey("dmg_max");
			if (flag7)
			{
				bool flag8 = res["dmg_min"] == 0 && res["dmg_max"] == 0;
				if (flag8)
				{
					text2 += " ";
				}
				bool flag9 = res["dmg_max"] > 0;
				if (flag9)
				{
					text2 += string.Format(languageText17, res["dmg_min"], res["dmg_max"]);
				}
			}
			bool flag10 = res.ContainsKey("nag_dmg") && res["nag_dmg"] > 0;
			if (flag10)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					text3,
					languageText8,
					languageText12,
					this.strSkillDmgStrText(res["nag_dmg"], spt),
					" "
				});
			}
			bool flag11 = res.ContainsKey("pos_dmg") && res["pos_dmg"] > 0;
			if (flag11)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					text3,
					languageText9,
					languageText12,
					this.strSkillDmgStrText(res["pos_dmg"], spt),
					" "
				});
			}
			bool flag12 = res.ContainsKey("voi_dmg") && res["voi_dmg"] > 0;
			if (flag12)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					text3,
					languageText10,
					languageText12,
					this.strSkillDmgStrText(res["voi_dmg"], spt),
					" "
				});
			}
			bool flag13 = res.ContainsKey("poi_dmg") && res["poi_dmg"] > 0;
			if (flag13)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					text3,
					languageText11,
					languageText12,
					this.strSkillDmgStrText(res["poi_dmg"], spt),
					" "
				});
			}
			string languageText18 = LanguagePack.getLanguageText("skill", "t_baseHt_NeiGong");
			string languageText19 = LanguagePack.getLanguageText("skill", "t_baseHt_WaiGong");
			bool flag14 = text2 != "";
			if (flag14)
			{
				float num = res["noratk"];
				bool flag15 = num > 0f;
				if (flag15)
				{
					string str = (num * 100f).ToString() + "%";
					bool flag16 = res.ContainsKey("plyatk") && res["plyatk"] > 0;
					if (flag16)
					{
						text = text + str + languageText19;
					}
					else
					{
						int @int = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["carr"]._int;
						Variant carrlvl = (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.GetCarrlvl(@int);
						uint @uint = carrlvl["baseatt"][0]["atktp"]._uint;
						bool flag17 = 2u == @uint;
						if (flag17)
						{
							text = text + str + languageText18;
						}
						else
						{
							text = text + str + languageText19;
						}
					}
				}
			}
			bool flag18 = res.ContainsKey("hp_dmg") && res["hp_dmg"] != 0;
			if (flag18)
			{
				bool flag19 = res["hp_dmg"] > 0;
				if (flag19)
				{
					text2 = text2 + languageText13 + this.strSkillDmgStrText(res["hp_dmg"], spt);
				}
				else
				{
					text2 = text2 + languageText14 + this.strSkillDmgStrText((float)Math.Abs(res["hp_dmg"]), spt);
				}
			}
			bool flag20 = text2 != "";
			if (flag20)
			{
				text += text2;
			}
			string languageText20 = LanguagePack.getLanguageText("skill", "reducePowerValue");
			string languageText21 = LanguagePack.getLanguageText("skill", "reduceAngerValue");
			string languageText22 = LanguagePack.getLanguageText("skill", "targetEffectPercent");
			string languageText23 = LanguagePack.getLanguageText("skill", "continuanceSecond");
			string languageText24 = LanguagePack.getLanguageText("skill", "removeAllStateVerso");
			string languageText25 = LanguagePack.getLanguageText("skill", "removeAllStateFace");
			string languageText26 = LanguagePack.getLanguageText("skill", "removeOneStateVersoRandom");
			string languageText27 = LanguagePack.getLanguageText("skill", "removeOneStateFaceRandom");
			bool flag21 = res.ContainsKey("mp_dmg") && res["mp_dmg"] > 0;
			if (flag21)
			{
				text = text + languageText20 + this.strSkillDmgStrText(res["mp_dmg"], spt);
			}
			bool flag22 = res.ContainsKey("agry_dmg") && res["agry_dmg"] > 0;
			if (flag22)
			{
				text = text + languageText21 + this.strSkillDmgStrText(res["agry_dmg"], spt);
			}
			bool flag23 = res.ContainsKey("tar_state") && res["tar_state"] != 0;
			if (flag23)
			{
				Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrSkillConf.get_state_desc(res["tar_state"]);
				string languageText28 = LanguagePack.getLanguageText("buffStateName", variant["name"]);
				text += string.Format(languageText22, languageText28);
			}
			bool flag24 = res.ContainsKey("state_tm") && res["state_tm"] > 0;
			if (flag24)
			{
				bool flag25 = res["state_tm"] > 600;
				if (flag25)
				{
					text += string.Format(languageText23, this.GetTimeYdms((uint)(res["state_tm"] / 10), null));
				}
				else
				{
					string languageText29 = LanguagePack.getLanguageText("time", "sec");
					text = text + string.Format(languageText23, (float)(res["state_tm"] * 0.1)) + languageText29;
				}
			}
			bool flag26 = res.ContainsKey("rmv_stat");
			if (flag26)
			{
				bool flag27 = res["rmv_stat"] == 1;
				if (flag27)
				{
					text += languageText24;
				}
				else
				{
					bool flag28 = res["rmv_stat"] == 2;
					if (flag28)
					{
						text += languageText25;
					}
				}
			}
			bool flag29 = res.ContainsKey("rmv_1stat");
			if (flag29)
			{
				bool flag30 = res["rmv_1stat"] == 1;
				if (flag30)
				{
					text += languageText26;
				}
				else
				{
					bool flag31 = res["rmv_1stat"] == 2;
					if (flag31)
					{
						text += languageText27;
					}
				}
			}
			return text;
		}

		private string skillDmgStr(float _base_dmg, float spt)
		{
			string text = ((int)_base_dmg).ToString();
			int num = (int)((double)(_base_dmg * spt) * 0.001);
			bool flag = num > 0;
			if (flag)
			{
				text = string.Concat(new object[]
				{
					text,
					"<txt text=\"(+",
					num,
					")\" color='0xacea54'/>"
				});
			}
			return text;
		}

		private string strSkillDmgStrText(float _base_dmg, float spt)
		{
			string text = ((int)_base_dmg).ToString();
			int num = (int)((double)(_base_dmg * spt) * 0.001);
			bool flag = num > 0;
			if (flag)
			{
				text += num;
			}
			return text;
		}

		public string GetBlessDesc(Variant bless)
		{
			string languageText = LanguagePack.getLanguageText("UITileBuff", "recoveryStrength");
			string languageText2 = LanguagePack.getLanguageText("UITileBuff", "recoveryPower");
			string languageText3 = LanguagePack.getLanguageText("UITileBuff", "doubleExp");
			string languageText4 = LanguagePack.getLanguageText("UITileBuff", "perSencond");
			string languageText5 = LanguagePack.getLanguageText("UITileBuff", "memory");
			string text = "";
			Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrSkillConf.get_bless_data(bless["id"]);
			bool flag = variant.ContainsKey("eff");
			if (flag)
			{
				Variant variant2 = variant["eff"];
				float @float = variant2["par"]._float;
				int num = variant2["tp"];
				string str = "";
				bool flag2 = num == 1;
				if (flag2)
				{
					str = string.Format(languageText, variant2["add"]);
				}
				else
				{
					bool flag3 = num == 2;
					if (flag3)
					{
						str = string.Format(languageText2, variant2["add"]);
					}
					else
					{
						bool flag4 = num == 3;
						if (flag4)
						{
							int @int = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["exp"]._int;
							str = string.Format(languageText3, @float);
						}
					}
				}
				bool flag5 = num != 3;
				if (flag5)
				{
					bool flag6 = variant2.ContainsKey("ticktm");
					if (flag6)
					{
						text = text + "<txt text=\"" + string.Format(languageText4, variant2["ticktm"]) + "\"color='0x5ceb77' size='12'/>";
					}
					text = text + "<txt text=\"" + str + "\" color='0x5ceb77' size='12'/><br/>";
					text = string.Concat(new string[]
					{
						text,
						"<txt text=\"",
						languageText5,
						bless["par"],
						"/",
						variant2["maxpar"],
						"\" color='0x5ceb77' size='12'/><br/>"
					});
				}
				else
				{
					text = text + "<txt text=\"" + str + "\" color='0x5ceb77' size='12'/><br/>";
				}
			}
			return text;
		}

		public string GetConfigStr(string str)
		{
			str = str.Substring(1, str.Length - 2);
			Variant variant = GameTools.split(str, "##", 1u);
			return this.GetHtmlText(variant[0], variant[1]);
		}

		public string GetConfNomalStr(string str)
		{
			str = str.Substring(1, str.Length - 2);
			Variant variant = GameTools.split(str, "##", 1u);
			string result = "";
			bool flag = variant.Length > 1;
			if (flag)
			{
				result = LanguagePack.getLanguageText(variant[0], variant[1]);
			}
			return result;
		}

		public static int get_day(int sec)
		{
			return sec / 86400;
		}

		public static string get_time(uint sec)
		{
			uint num = sec / 3600u;
			uint num2 = (sec - num * 3600u) / 60u;
			uint num3 = sec - num * 3600u - num2 * 60u;
			string str = "";
			bool flag = num < 10u;
			if (flag)
			{
				str += "0";
			}
			str += num.ToString();
			str += ":";
			bool flag2 = num2 < 10u;
			if (flag2)
			{
				str += "0";
			}
			str += num2.ToString();
			str += ":";
			bool flag3 = num3 < 10u;
			if (flag3)
			{
				str += "0";
			}
			return str + num3.ToString();
		}

		public string GetTimeYdms(uint sec, Variant data = null)
		{
			uint num = sec / 86400u;
			uint num2 = (sec - num * 86400u) / 3600u;
			uint num3 = (sec - num * 86400u - num2 * 3600u) / 60u;
			uint num4 = sec - num * 86400u - num2 * 3600u - num3 * 60u;
			string text = "";
			string languageText = LanguagePack.getLanguageText("time", "day");
			string languageText2 = LanguagePack.getLanguageText("time", "hour");
			string languageText3 = LanguagePack.getLanguageText("time", "min");
			string languageText4 = LanguagePack.getLanguageText("time", "sec");
			bool flag = num > 0u;
			if (flag)
			{
				text = text + num.ToString() + languageText;
			}
			bool flag2 = num2 > 0u;
			if (flag2)
			{
				text = text + num2.ToString() + languageText2;
			}
			bool flag3 = num3 > 0u;
			if (flag3)
			{
				text = text + num3.ToString() + languageText3;
			}
			bool flag4 = data != null && data.ContainsKey("offline");
			string result;
			if (flag4)
			{
				text = string.Concat(new string[]
				{
					num.ToString(),
					languageText,
					num2.ToString(),
					languageText2,
					num3.ToString(),
					languageText3
				});
				result = text;
			}
			else
			{
				bool flag5 = num4 > 0u;
				if (flag5)
				{
					text = text + num4.ToString() + languageText4;
				}
				result = text;
			}
			return result;
		}

		public string GetDateBySec(TZDate date)
		{
			string text = "";
			int num = date.month + 1;
			bool flag = num < 10;
			if (flag)
			{
				text += "0";
			}
			text += num;
			text += "/";
			float num2 = (float)date.date;
			bool flag2 = num2 < 10f;
			if (flag2)
			{
				text += "0";
			}
			text += num2;
			text += " ";
			float num3 = (float)date.hours;
			bool flag3 = num3 < 10f;
			if (flag3)
			{
				text += "0";
			}
			text += num3;
			text += ":";
			float num4 = (float)date.minutes;
			bool flag4 = num4 < 10f;
			if (flag4)
			{
				text += "0";
			}
			return text + num4;
		}

		private Variant _get_sell_data()
		{
			Variant variant = new Variant();
			Variant variant2 = this._market_data["sell"];
			foreach (Variant current in variant2.Values)
			{
				bool flag = current == null;
				if (!flag)
				{
					Variant variant3 = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["item_id"]);
					bool flag2 = variant3 == null;
					if (!flag2)
					{
						variant._arr.Add(current);
					}
				}
			}
			return variant;
		}

		public Variant get_market_data_sell()
		{
			bool flag = this._market_data_sell == null || this._market_data_sell.Count == 0;
			if (flag)
			{
				bool flag2 = this._market_data == null;
				if (flag2)
				{
					this._market_data = (this._uiClient.g_gameConfM as muCLientConfig).svrMarketConf.GetMaketConfig();
				}
				this._market_data_sell = new Variant();
				Variant variant = this._get_sell_data();
				for (int i = 0; i < variant.Count; i++)
				{
					Variant variant2 = new Variant();
					bool flag3 = variant2 == null;
					if (!flag3)
					{
						variant2 = variant[i];
						this._market_data_sell[variant2["item_id"]._int.ToString()] = variant2;
					}
				}
			}
			return this._market_data_sell;
		}

		public Variant GetMarketSellItemByProp(string prop)
		{
			Variant market_data_sell = this.get_market_data_sell();
			bool flag = market_data_sell != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in market_data_sell.Values)
				{
					Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["item_id"]._uint);
					bool flag2 = variant != null;
					if (flag2)
					{
						bool flag3 = variant["conf"].ContainsKey(prop);
						if (flag3)
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

		public int GetNeedGridCount(int tpid, uint cnt)
		{
			uint num = 1u;
			Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf((uint)tpid);
			bool flag = variant != null && variant["conf"] != null;
			if (flag)
			{
				num = variant["conf"]["mul"];
				bool flag2 = num == 0u;
				if (flag2)
				{
					num = 1u;
				}
			}
			return (int)Math.Ceiling((double)(cnt / num));
		}

		public bool verify(string name, uint max_length = 0u, uint tp = 0u)
		{
			bool flag = name.Length == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = max_length <= 0u;
				if (flag2)
				{
					result = false;
				}
				else
				{
					ByteArray byteArray = new ByteArray();
					bool flag3 = (long)byteArray.length > (long)((ulong)max_length);
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = name.IndexOf("regEx") >= 0;
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = tp > 0u;
							if (flag5)
							{
								bool flag6 = name != "";
								if (flag6)
								{
									result = false;
									return result;
								}
							}
							result = true;
						}
					}
				}
			}
			return result;
		}

		public static int mbcs_length(string text)
		{
			int num = 0;
			for (int i = 0; i < text.Length; i++)
			{
				int num2 = (int)text[i];
				bool flag = num2 > 255;
				if (flag)
				{
					num += 2;
				}
				else
				{
					num++;
				}
			}
			return num;
		}

		public bool CanTrade()
		{
			return this.CanGoldTrade() || this.CanYBTrade();
		}

		public bool CanGoldTrade()
		{
			return (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.get_game_general_data("trade_gld") != 0f;
		}

		public bool CanYBTrade()
		{
			return (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("ybtrade") == 1u;
		}

		public Variant GetNameByLvl(int lvl = 0)
		{
			Variant variant = new Variant();
			bool flag = lvl == 0;
			if (flag)
			{
				lvl = (this._uiClient.g_gameM as muLGClient).g_generalCT.noblv;
			}
			Variant nobByid = (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.GetNobByid(lvl);
			bool flag2 = lvl == 0 || nobByid == null;
			Variant result;
			if (flag2)
			{
				variant["name"] = LanguagePack.getLanguageText("nobility_name", "define");
				variant["fmt"] = 16777215;
				result = variant;
			}
			else
			{
				Variant nobilityColor = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetNobilityColor(lvl);
				string confNomalStr = this.GetConfNomalStr(nobByid["name"]._str);
				string str = nobByid["star"]._str;
				bool flag3 = nobilityColor != null;
				if (flag3)
				{
					string text = confNomalStr + str;
					bool flag4 = !nobilityColor["star"];
					if (flag4)
					{
						text += LanguagePack.getLanguageText("nobility_str", "star");
					}
					variant["name"] = text;
					variant["fmt"] = nobilityColor["color"]._str;
				}
				else
				{
					variant["fmt"] = 16777215;
					variant["name"] = confNomalStr + str + LanguagePack.getLanguageText("nobility_str", "star");
				}
				result = variant;
			}
			return result;
		}

		public void goto_map(uint mid, uint tx, uint ty, bool ask = true, Variant data = null)
		{
			LGUIMainUIImpl_NEED_REMOVE lGUIMainUIImpl_NEED_REMOVE = this._uiClient.getLGUI("LGUIMainUIImpl") as LGUIMainUIImpl_NEED_REMOVE;
			Variant variant = new Variant();
			variant["tp"] = 1;
			variant["map_id"] = mid;
			variant["x"] = tx;
			variant["y"] = ty;
			variant["itmtpid"] = 0;
			bool flag = !this.is_self_can_free_trans();
			if (!flag)
			{
				this._goto_map_need(variant, ask, data);
			}
		}

		public void TrantoNPC(uint npcid, uint tx, uint ty, bool ask = true, Variant data = null)
		{
			string languageText = LanguagePack.getLanguageText("mission_manager", "t_cantTrans");
		}

		public void TrantoMis(uint misid, string goal, uint tx, uint ty, bool ask = true, Variant data = null)
		{
			string languageText = LanguagePack.getLanguageText("mission_manager", "t_cantTrans");
		}

		public Variant GetMisGoalPos(Variant goal)
		{
			string val = "talknpc";
			Variant variant = new Variant();
			int val2 = 0;
			int val3 = 0;
			int @int = goal["talknpc"]._int;
			bool flag = goal.ContainsKey("kilmon");
			if (flag)
			{
				val = "kilmon";
				Variant variant2 = goal["kilmon"];
				for (int i = 0; i < variant2.Count; i++)
				{
					Variant variant3 = variant2[i];
					bool flag2 = variant3.ContainsKey("pos");
					if (flag2)
					{
						val2 = variant3["pos"][0]["x"];
						val3 = variant3["pos"][0]["y"];
						break;
					}
				}
			}
			else
			{
				bool flag3 = goal.ContainsKey("colitm");
				if (flag3)
				{
					val = "colitm";
					Variant variant4 = goal["colitm"];
					for (int i = 0; i < variant4.Count; i++)
					{
						Variant variant5 = variant4[i];
						bool flag4 = variant5.ContainsKey("pos");
						if (flag4)
						{
							val2 = variant5["pos"][0]["x"];
							val3 = variant5["pos"][0]["y"];
							break;
						}
					}
				}
				else
				{
					bool flag5 = goal.ContainsKey("ownitm");
					if (flag5)
					{
						val = "ownitm";
						Variant variant6 = goal["ownitm"];
						for (int i = 0; i < variant6.Count; i++)
						{
							Variant variant7 = variant6[i];
							bool flag6 = variant7.ContainsKey("pos");
							if (flag6)
							{
								val2 = variant7["pos"][0]["x"];
								val3 = variant7["pos"][0]["y"];
								break;
							}
						}
					}
					else
					{
						bool flag7 = goal.ContainsKey("colmon");
						if (flag7)
						{
							val = "colmon";
							Variant variant8 = goal["colmon"];
							for (int i = 0; i < variant8.Count; i++)
							{
								Variant variant9 = variant8[i];
								bool flag8 = variant9.ContainsKey("pos");
								if (flag8)
								{
									val2 = variant9["pos"][0]["x"];
									val3 = variant9["pos"][0]["y"];
									break;
								}
							}
						}
						else
						{
							bool flag9 = @int > 0;
							if (flag9)
							{
								Variant variant10 = (this._uiClient.g_gameConfM as muCLientConfig).svrMapsConf.get_npc_info((uint)@int);
								val2 = variant10["x"] + 2;
								val3 = variant10["y"] + 2;
							}
						}
					}
				}
			}
			variant["goal"] = val;
			variant["npcid"] = @int;
			variant["posX"] = val2;
			variant["posY"] = val3;
			return variant;
		}

		public bool is_self_can_free_trans()
		{
			return true;
		}

		public int get_fly_item_cnt()
		{
			int num = 0;
			Variant fly_item_tpid = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_fly_item_tpid();
			using (List<Variant>.Enumerator enumerator = fly_item_tpid._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int tpid = enumerator.Current;
					int itemCount = (this._uiClient.g_gameM as muLGClient).g_itemsCT.GetItemCount((uint)tpid);
					num += itemCount;
				}
			}
			return num;
		}

		private void _goto_map_need(Variant info, bool ask, Variant data)
		{
			this._transData["info"] = info;
			this._transData["data"] = data;
			if (ask)
			{
				this._goto_map_with_ask(info, data);
			}
			else
			{
				this._goto_map_no_ask(info, data);
			}
		}

		private void _goto_map_no_ask(Variant info, Variant data)
		{
			bool flag = this._trans_item_tpid == 0u;
			if (flag)
			{
				this._get_trans_item_tpid();
			}
			this._confirmTransMap();
		}

		private void _goto_map_with_ask(Variant info, Variant data)
		{
			int num = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["cid"];
			string text = LanguagePack.getLanguageText("mission_manager", "trans_notice");
			string languageText = LanguagePack.getLanguageText("mission_manager", "chkBox_label");
			bool flag = this._trans_item_tpid == 0u;
			if (flag)
			{
				this._get_trans_item_tpid();
			}
			Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(this._trans_item_tpid);
			int fly_item_cnt = this.get_fly_item_cnt();
			this.t_noTransItem = fly_item_cnt.ToString();
			bool flag2 = this.t_noTransItem == "";
			if (flag2)
			{
				this.t_noTransItem = LanguagePack.getLanguageText("mission_manager", "t_noTransItem");
			}
			text = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.TransToSuperText(text);
			string languageText2 = LanguagePack.getLanguageText("items_xml", this._trans_item_tpid.ToString());
			text = DebugTrace.Printf(text, new string[]
			{
				languageText2,
				fly_item_cnt.ToString()
			});
			bool flag3 = fly_item_cnt > 0 && this.IsSaveFlag("noticewhB6");
			if (flag3)
			{
				this._confirmTransMap();
			}
		}

		private void _checkTransBack(bool isCheck)
		{
			this.SaveFlag("noticewhB6", true);
		}

		private void _confirmTransMap()
		{
			this._goto_map(this._transData["info"], this._transData["data"]);
		}

		private void _buyTransItemBack()
		{
			this._confirmTransMap();
		}

		private void _goto_map(Variant info, Variant data)
		{
		}

		private void _get_trans_item_tpid()
		{
			bool flag = this._trans_item_tpid > 0u;
			if (!flag)
			{
				uint trans_item_tpid = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.get_itemId_byFeatures("trans");
				this._trans_item_tpid = trans_item_tpid;
			}
		}

		protected void _showFastBuy()
		{
		}

		public bool can_goto_special_map(uint map_id)
		{
			bool result = true;
			ClientGeneralConf localGeneral = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral;
			Variant mapLimit = localGeneral.GetMapLimit((int)map_id);
			bool flag = mapLimit != null;
			if (flag)
			{
				bool flag2 = mapLimit["type"] == 0;
				if (flag2)
				{
					uint num = 11u;
					Variant variant = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["equip"];
					bool flag3 = variant != null;
					if (flag3)
					{
						bool flag4 = false;
						foreach (Variant current in variant.Values)
						{
							bool flag5 = !(this._uiClient.g_gameM as muLGClient).g_itemsCT.CheckTimeOut(current);
							if (!flag5)
							{
								string str = mapLimit["need"];
								Variant variant2 = GameTools.split(str, ",", 1u);
								using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator2 = variant2.Values.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										int num2 = enumerator2.Current;
										bool flag6 = current["tpid"] == num2;
										if (flag6)
										{
											flag4 = true;
										}
									}
								}
								Variant variant3 = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]._uint);
								bool flag7 = variant3 != null && variant3["conf"] != null && variant3["conf"]["pos"] == num;
								if (flag7)
								{
									flag4 = true;
								}
								bool flag8 = flag4;
								if (flag8)
								{
									break;
								}
							}
						}
						result = flag4;
					}
				}
				else
				{
					bool flag9 = mapLimit["type"] == 1;
					if (flag9)
					{
					}
				}
			}
			return result;
		}

		public Variant GetAwardItems(Variant awardData)
		{
			bool flag = awardData == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = new Variant();
				Variant variant2 = awardData["eqp"];
				Variant mainPlayerInfo = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
				bool flag2 = variant2 != null && variant2.Count > 0;
				if (flag2)
				{
					foreach (Variant current in variant2._arr)
					{
						bool flag3 = current.ContainsKey("attchk");
						if (flag3)
						{
							bool flag4 = !this.is_attchk(current["attchk"], mainPlayerInfo, null);
							if (flag4)
							{
								continue;
							}
						}
						bool flag5 = !current.ContainsKey("tpid");
						if (flag5)
						{
							Variant variant3 = current["id"];
							bool flag6 = variant3 != null;
							if (flag6)
							{
								current["tpid"] = variant3;
							}
						}
						bool flag7 = current.ContainsKey("expire");
						if (flag7)
						{
							current["client_time"] = current["expire"];
						}
						variant._arr.Add(current);
					}
				}
				variant2 = awardData["itm"];
				bool flag8 = variant2 != null && variant2.Count > 0;
				if (flag8)
				{
					foreach (Variant current2 in variant2._arr)
					{
						bool flag9 = current2.ContainsKey("attchk");
						if (flag9)
						{
							bool flag10 = !this.is_attchk(current2["attchk"], mainPlayerInfo, null);
							if (flag10)
							{
								continue;
							}
						}
						bool flag11 = !current2.ContainsKey("tpid");
						if (flag11)
						{
							Variant variant3 = current2["id"];
							bool flag12 = variant3 != null;
							if (flag12)
							{
								current2["tpid"] = variant3;
							}
						}
						bool flag13 = current2.ContainsKey("expire");
						if (flag13)
						{
							current2["client_time"] = current2["expire"];
						}
						variant._arr.Add(current2);
					}
				}
				bool flag14 = awardData.ContainsKey("exp") && awardData["exp"] > 0;
				if (flag14)
				{
					Variant variant4 = new Variant();
					variant4["exp"] = awardData["exp"];
					variant._arr.Add(variant4);
				}
				bool flag15 = awardData.ContainsKey("gld") && awardData["gld"] > 0;
				if (flag15)
				{
					Variant variant5 = new Variant();
					variant5["gold"] = awardData["gld"];
					variant._arr.Add(variant5);
				}
				bool flag16 = awardData.ContainsKey("yb") && awardData["yb"] > 0;
				if (flag16)
				{
					Variant variant6 = new Variant();
					variant6["yb"] = awardData["yb"];
					variant._arr.Add(variant6);
				}
				bool flag17 = awardData.ContainsKey("bndyb") && awardData["bndyb"] > 0;
				if (flag17)
				{
					Variant variant7 = new Variant();
					variant7["bndyb"] = awardData["bndyb"];
					variant._arr.Add(variant7);
				}
				bool flag18 = awardData.ContainsKey("meript") && awardData["meript"] > 0;
				if (flag18)
				{
					Variant variant8 = new Variant();
					variant8["meript"] = awardData["meript"];
					variant._arr.Add(variant8);
				}
				bool flag19 = awardData.ContainsKey("skexp") && awardData["skexp"] > 0;
				if (flag19)
				{
					Variant variant9 = new Variant();
					variant9["skexp"] = awardData["skexp"];
					variant._arr.Add(variant9);
				}
				bool flag20 = awardData.ContainsKey("nobpt") && awardData["nobpt"] > 0;
				if (flag20)
				{
					Variant variant10 = new Variant();
					variant10["nobpt"] = awardData["nobpt"];
					variant._arr.Add(variant10);
				}
				bool flag21 = awardData.ContainsKey("clang") && awardData["clang"] > 0;
				if (flag21)
				{
					Variant variant11 = new Variant();
					variant11["clang"] = awardData["clang"];
					variant._arr.Add(variant11);
				}
				bool flag22 = awardData.ContainsKey("clana") && awardData["clana"] > 0;
				if (flag22)
				{
					Variant variant12 = new Variant();
					variant12["clana"] = awardData["clana"];
					variant._arr.Add(variant12);
				}
				bool flag23 = awardData.ContainsKey("clangld") && awardData["clangld"] > 0;
				if (flag23)
				{
					Variant variant13 = new Variant();
					variant13["clangld"] = awardData["clangld"];
					variant._arr.Add(variant13);
				}
				bool flag24 = awardData.ContainsKey("clanyb") && awardData["clanyb"] > 0;
				if (flag24)
				{
					Variant variant14 = new Variant();
					variant14["clanyb"] = awardData["clanyb"];
					variant._arr.Add(variant14);
				}
				result = variant;
			}
			return result;
		}

		public void AssignItemCnt(Variant obj, Variant prop)
		{
			bool flag = obj.ContainsKey("tpid");
			if (flag)
			{
				foreach (string current in prop.Keys)
				{
					bool flag2 = !obj.ContainsKey(current);
					if (!flag2)
					{
						obj[current] = prop[current];
					}
				}
			}
			else
			{
				bool flag3 = prop.ContainsKey("cnt");
				if (flag3)
				{
					bool flag4 = obj.ContainsKey("gld");
					if (flag4)
					{
						obj["gld"] = prop["cnt"];
					}
					else
					{
						bool flag5 = obj.ContainsKey("gold");
						if (flag5)
						{
							obj["gold"] = prop["cnt"];
						}
						else
						{
							bool flag6 = obj.ContainsKey("exp");
							if (flag6)
							{
								obj["exp"] = prop["cnt"];
							}
							else
							{
								bool flag7 = obj.ContainsKey("bndyb");
								if (flag7)
								{
									obj["bndyb"] = prop["cnt"];
								}
								else
								{
									bool flag8 = obj.ContainsKey("skexp");
									if (flag8)
									{
										obj["skexp"] = prop["cnt"];
									}
									else
									{
										bool flag9 = obj.ContainsKey("nobpt");
										if (flag9)
										{
											obj["nobpt"] = prop["cnt"];
										}
										else
										{
											bool flag10 = obj.ContainsKey("meript");
											if (flag10)
											{
												obj["meript"] = prop["cnt"];
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		public bool IsLvlClosed(Variant tmchks)
		{
			bool flag = tmchks != null;
			bool result;
			if (flag)
			{
				bool flag2 = true;
				float num = (float)(this._uiClient.g_netM as muNetCleint).CurServerTimeStampMS;
				float firstracttmt = (this._uiClient.g_gameM as muLGClient).g_activityCT.firstracttmt;
				float combracttm = (this._uiClient.g_gameM as muLGClient).g_activityCT.combracttm;
				foreach (Variant current in tmchks._arr)
				{
					Variant todayActiveTime = ConfigUtil.GetTodayActiveTime((double)num, current, (double)firstracttmt, (double)combracttm);
					bool flag3 = todayActiveTime["begin"]._float > 0f;
					if (flag3)
					{
						flag2 = false;
						break;
					}
				}
				result = flag2;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool IsWorShipOpenTime()
		{
			Variant worshipData = (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.GetWorshipData();
			bool flag = worshipData && worshipData.ContainsKey("tmchk");
			bool result;
			if (flag)
			{
				float num = (float)(this._uiClient.g_netM as muNetCleint).CurServerTimeStampMS;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private Variant getCharFlagData()
		{
			bool flag = this._char_key == null;
			if (flag)
			{
				this._char_key = "localflag_" + (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["cid"];
			}
			return new Variant();
		}

		public bool IsSaveFlag(string name)
		{
			Variant charFlagData = this.getCharFlagData();
			return charFlagData.ContainsKey(name);
		}

		public void SaveFlag(string name, bool data = true)
		{
			Variant charFlagData = this.getCharFlagData();
			charFlagData[name] = data;
		}

		public Variant GetFlag(string name)
		{
			Variant charFlagData = this.getCharFlagData();
			bool flag = charFlagData[name] != null;
			Variant result;
			if (flag)
			{
				result = charFlagData[name];
			}
			else
			{
				result = "false";
			}
			return result;
		}

		public void DelFlag(string name)
		{
		}

		public void FlushFlag()
		{
		}

		private Variant getGameFlagData()
		{
			return 0;
		}

		public bool IsSaveGameFlag(string name)
		{
			Variant gameFlagData = this.getGameFlagData();
			return gameFlagData.ContainsKey(name);
		}

		public void SaveGameFlag(string name, bool data = true)
		{
			Variant gameFlagData = this.getGameFlagData();
			gameFlagData[name] = data;
		}

		public void DelGameFlag(string name)
		{
		}

		public Variant GetGameFlag(string name)
		{
			Variant gameFlagData = this.getGameFlagData();
			return gameFlagData[name];
		}

		public void FlushGameFlag()
		{
		}

		public void go_to_charge(int payType = 0)
		{
		}

		public Action HoldData(Variant data, Action<Variant> fun)
		{
			return delegate
			{
				fun(data);
			};
		}

		public Action<bool> HoldData_arg1_bool(Variant data, Action<Variant, bool> fun)
		{
			return delegate(bool arg1)
			{
				fun(data, arg1);
			};
		}

		public Action<uint> HoldData_arg1_uint(Variant data, Action<Variant, uint> fun)
		{
			return delegate(uint arg1)
			{
				fun(data, arg1);
			};
		}

		public Action<string> HoldData_arg1_string(Variant data, Action<Variant, string> fun)
		{
			return delegate(string arg1)
			{
				fun(data, arg1);
			};
		}

		public Action<Variant> HoldData_arg1_var(Variant data, Action<Variant, Variant> fun)
		{
			return delegate(Variant arg1)
			{
				fun(data, arg1);
			};
		}

		public Action<bool, Variant> HoldData_arg2_bool(Variant data, Action<Variant, bool, Variant> fun)
		{
			return delegate(bool arg1, Variant arg2)
			{
				fun(data, arg1, arg2);
			};
		}

		public Action<uint, Variant> HoldData_arg2_uint(Variant data, Action<Variant, uint, Variant> fun)
		{
			return delegate(uint arg1, Variant arg2)
			{
				fun(data, arg1, arg2);
			};
		}

		public Action<string, Variant> HoldData_arg2_string(Variant data, Action<Variant, string, Variant> fun)
		{
			return delegate(string arg1, Variant arg2)
			{
				fun(data, arg1, arg2);
			};
		}

		public Action<string, Variant> HoldData_arg2_val(Variant data, Action<Variant, string, Variant> fun)
		{
			return delegate(string arg1, Variant arg2)
			{
				fun(data, arg1, arg2);
			};
		}

		public bool CanChangePK()
		{
			Variant mainPlayerInfo = (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			int num = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("pklimit");
			bool flag = mainPlayerInfo["level"] < num;
			bool result;
			if (flag)
			{
				string text = LanguagePack.getLanguageText("pkstate", "cantpklevel");
				text = DebugTrace.Printf(text, new string[]
				{
					num.ToString()
				});
				this.showMessage(text, 4u);
				result = false;
			}
			else
			{
				bool @bool = mainPlayerInfo["in_pczone"]._bool;
				if (@bool)
				{
					string languageText = LanguagePack.getLanguageText("pkstate", "cantpkzone");
					this.showMessage(languageText, 4u);
					result = false;
				}
				else
				{
					int pKState = (this._uiClient.g_gameM as muLGClient).g_mapCT.GetPKState();
					bool flag2 = pKState == 0;
					if (flag2)
					{
						string languageText2 = LanguagePack.getLanguageText("pkstate", "cantpkmap");
						this.showMessage(languageText2, 4u);
						result = false;
					}
					else
					{
						bool flag3 = this.IsSideMode();
						if (flag3)
						{
							string languageText3 = LanguagePack.getLanguageText("pkstate", "cantpkside");
							this.showMessage(languageText3, 4u);
							result = false;
						}
						else
						{
							result = true;
						}
					}
				}
			}
			return result;
		}

		public bool IsSideMode()
		{
			string str = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("campid")._str;
			Variant variant = GameTools.split(str, ",", 1u);
			return false;
		}

		private void showMessage(string str, uint type = 2u)
		{
			LGIUIMainUI lGIUIMainUI = this._uiClient.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			lGIUIMainUI.systemmsg(str, type);
		}

		public string GetAwardItemsText(Variant data, Variant show)
		{
			bool @bool = show["flag"]._bool;
			bool bool2 = show["short"]._bool;
			uint num = 0u;
			bool flag = show.ContainsKey("cString");
			string arg;
			if (flag)
			{
				arg = show["cString"];
			}
			else
			{
				arg = this._defaultStr;
			}
			bool flag2 = show.ContainsKey("cNumber");
			string arg2;
			if (flag2)
			{
				arg2 = show["cNumber"];
			}
			else
			{
				arg2 = this._dafaultNumber;
			}
			string str = "";
			bool flag3 = show.ContainsKey("prefab");
			if (flag3)
			{
				str = show["prefab"] + "_";
			}
			string text = "";
			string str2 = "{br}";
			bool flag4 = show.ContainsKey("changeline");
			if (flag4)
			{
				str2 = show["changeline"];
			}
			bool flag5 = (data.ContainsKey("exp") && data["exp"] > 0u) & @bool;
			if (flag5)
			{
				string languageText = LanguagePack.getLanguageText("UI_Class_Utility", str + "exp");
				bool flag6 = bool2;
				if (flag6)
				{
					string text2 = (data["exp"] * 0.0001).ToString() + "w";
					text = text + string.Format(languageText, arg, arg2, text2) + str2;
				}
				else
				{
					text = text + string.Format(languageText, arg, arg2, data["exp"]) + str2;
				}
			}
			bool flag7 = data.ContainsKey("yb") && data["yb"] > 0u;
			if (flag7)
			{
				string languageText2 = LanguagePack.getLanguageText("UI_Class_Utility", str + "t_yb");
				text = text + string.Format(languageText2, arg, arg2, data["yb"]) + str2;
			}
			bool flag8 = data.ContainsKey("bndyb") && data["bndyb"] > 0u;
			if (flag8)
			{
				string languageText3 = LanguagePack.getLanguageText("UI_Class_Utility", str + "bndyb");
				text = text + string.Format(languageText3, arg, arg2, data["bndyb"]) + str2;
			}
			bool flag9 = (data.ContainsKey("gld") && data["gld"] != 0u) || (data.ContainsKey("gld_cost") && data["gld_cost"] > 0u);
			if (flag9)
			{
				string languageText4 = LanguagePack.getLanguageText("UI_Class_Utility", str + "gold");
				bool flag10 = data.ContainsKey("gld");
				if (flag10)
				{
					text = text + string.Format(languageText4, arg, arg2, data["gld"]) + str2;
				}
				else
				{
					text = text + string.Format(languageText4, arg, arg2, data["gld_cost"]) + str2;
				}
			}
			bool flag11 = data.ContainsKey("meript") && data["meript"] > 0u;
			if (flag11)
			{
				string languageText5 = LanguagePack.getLanguageText("UI_Class_Utility", str + "t_meript");
				text = text + string.Format(languageText5, arg, arg2, data["meript"]) + str2;
			}
			bool flag12 = data.ContainsKey("skept") && data["skept"] > 0u;
			if (flag12)
			{
				string languageText6 = LanguagePack.getLanguageText("UI_Class_Utility", str + "t_skept");
				text = text + string.Format(languageText6, arg, arg2, data["skept"]) + str2;
			}
			bool flag13 = data.ContainsKey("nobpt") && data["nobpt"] > 0u;
			if (flag13)
			{
				string languageText7 = LanguagePack.getLanguageText("UI_Class_Utility", str + "t_nobpt");
				text = text + string.Format(languageText7, arg, arg2, data["nobpt"]) + str2;
			}
			bool flag14 = data.ContainsKey("clang") && data["clang"] > 0u;
			if (flag14)
			{
				string languageText8 = LanguagePack.getLanguageText("UI_Class_Utility", str + "t_clang");
				text = text + string.Format(languageText8, arg, arg2, data["clang"]) + str2;
			}
			bool flag15 = data.ContainsKey("clangld") && data["clangld"] > 0u;
			if (flag15)
			{
				Variant variant = new Variant();
				variant["awd"] = true;
				num += this.GetClanpt(data["clangld"], 0u, variant);
			}
			bool flag16 = data.ContainsKey("clanyb") && data["clanyb"] > 0u;
			if (flag16)
			{
				Variant variant2 = new Variant();
				variant2["awd"] = true;
				num += this.GetClanpt(0u, data["clanyb"], variant2);
			}
			bool flag17 = data.ContainsKey("clangld") || data.ContainsKey("clanyb");
			if (flag17)
			{
				string languageText9 = LanguagePack.getLanguageText("UI_Class_Utility", str + "t_clanpt");
				text = text + string.Format(languageText9, arg, arg2, num) + str2;
			}
			bool flag18 = data.ContainsKey("hexp");
			if (flag18)
			{
				string languageText10 = LanguagePack.getLanguageText("UI_Class_Utility", str + "hexp");
				text = text + string.Format(languageText10, arg, arg2, data["hexp"]) + str2;
			}
			string languageText11 = LanguagePack.getLanguageText("UI_Class_Utility", str + "t_item");
			bool flag19 = data.ContainsKey("eqp");
			if (flag19)
			{
				Variant variant3 = data["eqp"];
				uint num2 = 0u;
				while ((ulong)num2 < (ulong)((long)variant3.Count))
				{
					Variant variant4 = variant3[num2];
					uint num3 = variant4["id"];
					variant4["tpid"] = num3;
					uint num4 = variant4["flvl"];
					Variant variant5 = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(num3);
					bool flag20 = variant5 == null;
					if (!flag20)
					{
						string itemNameColorByQual = (this._uiClient.g_gameConfM as muCLientConfig).localItems.GetItemNameColorByQual(variant5["conf"]["qual"]);
						string text2 = string.Concat(new string[]
						{
							"{c=",
							itemNameColorByQual,
							"}",
							LanguagePack.getLanguageText("items_xml", variant5["conf"]["id"]),
							"(",
							num4.ToString(),
							"){end}"
						});
						text = text + string.Format(languageText11, arg, text2) + str2;
					}
					num2 += 1u;
				}
			}
			bool flag21 = data.ContainsKey("itm") || data.ContainsKey("upitm");
			if (flag21)
			{
				Variant variant6 = new Variant();
				bool flag22 = data.ContainsKey("itm");
				if (flag22)
				{
					variant6 = data["itm"];
				}
				else
				{
					Variant variant7 = new Variant();
					variant7["id "] = data["upitm"];
					variant7["cnt"] = data["upitmcnt"];
					variant6 = variant7;
				}
				uint num2 = 0u;
				while ((ulong)num2 < (ulong)((long)variant6.Count))
				{
					Variant variant4 = variant6[num2];
					uint num3 = variant4["id"];
					Variant variant5 = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(num3);
					bool flag23 = variant5 == null;
					if (!flag23)
					{
						string itemNameColorByQual = (this._uiClient.g_gameConfM as muCLientConfig).localItems.GetItemNameColorByQual(variant5["conf"]["qual"]);
						string text2 = string.Concat(new string[]
						{
							"{c=",
							itemNameColorByQual,
							"}",
							LanguagePack.getLanguageText("items_xml", variant5["conf"]["id"]),
							"*",
							variant4["cnt"]._uint.ToString(),
							"{end}"
						});
						text = text + string.Format(languageText11, arg, text2) + str2;
					}
					num2 += 1u;
				}
			}
			bool flag24 = show["trans"] == null || !show["trans"];
			if (flag24)
			{
				text = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.TransToSuperText(text);
			}
			return text;
		}

		public string GetPosTransEvent(Variant data)
		{
			string text = "";
			bool flag = data["type"] == "transpos";
			if (flag)
			{
				text = string.Concat(new string[]
				{
					"transpos_",
					data["goal"],
					"_",
					data["x"],
					"_",
					data["y"],
					"_",
					data["misid"]
				});
			}
			else
			{
				bool flag2 = data["type"] == "transnpc";
				if (flag2)
				{
					text = "transnpc_" + data["npcid"];
				}
			}
			bool flag3 = data["map_id"] != 0 && !(this._uiClient.g_gameConfM as muCLientConfig).svrLevelConf.is_level_map(data["map_id"]);
			string result;
			if (flag3)
			{
				string imgRes = (this._uiClient.g_gameConfM as muCLientConfig).localImgRes.GetImgRes("fly");
				Variant commonConf = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("flyEvtStr");
				string text2 = "";
				bool flag4 = commonConf;
				if (flag4)
				{
					text2 = commonConf.ToString();
				}
				result = string.Concat(new string[]
				{
					"<imgfile width='16' height='16' src='",
					imgRes,
					"' onclick='",
					data["event"],
					"' clickpar='",
					text,
					"' ",
					text2,
					" />"
				});
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string translateEntity(string str)
		{
			bool flag = this._entities == null;
			if (flag)
			{
				this._entities = new Variant();
				this._entities.pushBack(GameTools.createGroup(new Variant[]
				{
					"regex",
					"&",
					"xml",
					"&amp;"
				}));
				this._entities.pushBack(GameTools.createGroup(new Variant[]
				{
					"regex",
					"<",
					"xml",
					"&lt;"
				}));
				this._entities.pushBack(GameTools.createGroup(new Variant[]
				{
					"regex",
					">",
					"xml",
					"&gt;"
				}));
				this._entities.pushBack(GameTools.createGroup(new Variant[]
				{
					"regex",
					"'",
					"xml",
					"&apos;"
				}));
				this._entities.pushBack(GameTools.createGroup(new Variant[]
				{
					"regex",
					"/\"",
					"xml",
					"&quot;"
				}));
			}
			bool flag2 = str.Length == 0;
			string result;
			if (flag2)
			{
				result = str;
			}
			else
			{
				foreach (Variant current in this._entities._arr)
				{
					str = str.Replace(current["regex"], current["xml"]);
				}
				result = str;
			}
			return result;
		}

		public bool is_attchk(Variant attchk, Variant atts, Variant meri = null)
		{
			bool flag = attchk == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = atts == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					foreach (Variant current in attchk._arr)
					{
						bool flag3 = !atts.ContainsKey(current["name"]._str) && !current.ContainsKey("have");
						if (flag3)
						{
							result = false;
							return result;
						}
						bool flag4 = current.ContainsKey("min");
						if (flag4)
						{
							bool flag5 = atts[current["name"]] < current["min"];
							if (flag5)
							{
								result = false;
								return result;
							}
						}
						bool flag6 = current.ContainsKey("max");
						if (flag6)
						{
							bool flag7 = atts[current["name"]] > current["max"];
							if (flag7)
							{
								result = false;
								return result;
							}
						}
						bool flag8 = current.ContainsKey("equal");
						if (flag8)
						{
							bool flag9 = atts[current["name"]] != current["equal"];
							if (flag9)
							{
								result = false;
								return result;
							}
						}
						bool flag10 = current.ContainsKey("and");
						if (flag10)
						{
							bool flag11 = "carr" == current["name"];
							if (flag11)
							{
								result = this.check_carr(current["and"], atts);
								return result;
							}
							bool flag12 = (atts[current["name"]] & current["and"]._int) != atts[current["name"]];
							if (flag12)
							{
								result = false;
								return result;
							}
						}
						bool flag13 = current.ContainsKey("have");
						if (flag13)
						{
							bool flag14 = "meri" == current["name"];
							if (flag14)
							{
								int num = (current["have"] & 15) - 1;
								bool flag15 = num >= 0;
								if (flag15)
								{
									bool flag16 = !meri.ContainsKey(num);
									if (flag16)
									{
										result = false;
										return result;
									}
								}
								int num2 = (current["have"] >> 8 & 15) - 1;
								bool flag17 = num2 >= 0;
								if (flag17)
								{
									Variant variant = meri[num]["adata"];
									bool flag18 = !variant.ContainsKey(num2);
									if (flag18)
									{
										result = false;
										return result;
									}
									int num3 = current["have"] >> 16 & 15;
									bool flag19 = num3 > 0;
									if (flag19)
									{
										bool flag20 = num3 > variant[num2]["alvl"];
										if (flag20)
										{
											result = false;
											return result;
										}
									}
								}
							}
							else
							{
								bool flag21 = "achive" == current["name"];
								if (flag21)
								{
									Variant variant2 = atts["achs"];
									bool flag22 = variant2 == null || variant2._arr.IndexOf(current["have"]) == -1;
									if (flag22)
									{
										result = false;
										return result;
									}
								}
							}
						}
					}
					result = true;
				}
			}
			return result;
		}

		public bool to_check_carr(uint carr, Variant atts)
		{
			int @int = atts["carrlvl"]._int;
			int num = this.check_only_carr(carr, atts);
			bool flag = num != -1;
			bool result;
			if (flag)
			{
				int num2 = (int)(carr >> num * 4 + 1 & 7u);
				bool flag2 = @int >= num2;
				if (flag2)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public bool check_carr(uint carr, Variant atts)
		{
			int num = atts["carrlvl"];
			int num2 = this.check_only_carr(carr, atts);
			bool flag = num2 != -1;
			bool result;
			if (flag)
			{
				int num3 = (int)(carr >> num2 * 4 + 1 & 7u);
				bool flag2 = num >= num3;
				if (flag2)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public int check_only_carr(uint carr, Variant atts)
		{
			int num = atts["carr"];
			int num2 = 8;
			int result;
			for (int i = 0; i < num2; i++)
			{
				bool flag = (carr >> i * 4 & 1u) > 0u;
				if (flag)
				{
					int num3 = i + 1;
					bool flag2 = num == num3;
					if (flag2)
					{
						result = i;
						return result;
					}
				}
			}
			result = -1;
			return result;
		}

		public void SetIdxVisable(Variant ctrls, int idx, bool vis, float width = 0f, float height = 0f)
		{
			int count = ctrls.Count;
			bool flag = count <= 0;
			if (!flag)
			{
				bool flag2 = idx < 0 || idx >= count;
				if (!flag2)
				{
					ctrls[idx]["visible"] = vis;
					int num = (width > 0f) ? ctrls[0]["x"] : ctrls[0]["y"];
					for (int i = 0; i < ctrls.Count; i++)
					{
					}
				}
			}
		}

		public string get_type_by_tpid(uint tpid)
		{
			string text = "";
			Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(tpid);
			bool flag = variant == null || variant["conf"] == null;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				Variant variant2 = variant["conf"];
				bool flag2 = variant2["pos"] == 11;
				if (flag2)
				{
					text = "wing";
				}
				else
				{
					bool flag3 = variant2["pos"] == 10;
					if (flag3)
					{
						text = "riding";
					}
					else
					{
						bool flag4 = variant2["pos"] == 12;
						if (flag4)
						{
							text = "guard";
						}
						else
						{
							bool flag5 = variant2["pos"] == 13;
							if (flag5)
							{
								text = "pet";
							}
						}
					}
				}
				result = text;
			}
			return result;
		}

		public string strGetFormatTime(TZDate d)
		{
			bool flag = d == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = d.fullYear.ToString();
				string text2 = (d.month + 1).ToString();
				string text3 = d.date.ToString();
				bool flag2 = d.hours < 10;
				string text4;
				if (flag2)
				{
					text4 = "0" + d.hours.ToString();
				}
				else
				{
					text4 = d.hours.ToString();
				}
				bool flag3 = d.minutes < 10;
				string text5;
				if (flag3)
				{
					text5 = "0" + d.minutes.ToString();
				}
				else
				{
					text5 = d.minutes.ToString();
				}
				bool flag4 = d.seconds < 10;
				string text6;
				if (flag4)
				{
					text6 = "0" + d.seconds.ToString();
				}
				else
				{
					text6 = d.seconds.ToString();
				}
				string text7 = string.Concat(new string[]
				{
					text,
					"-",
					text2,
					"-",
					text3,
					" ",
					text4,
					":",
					text5,
					":",
					text6
				});
				result = text7;
			}
			return result;
		}

		public void showItemTileTips(Variant data, Vec2 point, bool flag = false)
		{
		}

		public Variant GetShowArrByType(Variant arr, Variant before, Dictionary<string, Func<bool>> checkShowFun = null)
		{
			return null;
		}

		private int checkBeforeOpenSet(string name, Variant before)
		{
			int result;
			foreach (Variant current in before._arr)
			{
				bool flag = current["typename"] == name;
				if (flag)
				{
					result = (current["flag"] ? 3 : 2);
					return result;
				}
			}
			result = 1;
			return result;
		}

		protected Variant getIconArr(Variant arr)
		{
			Variant sysOpenConf = (this._uiClient.g_gameConfM as muCLientConfig).localSystemOpen.GetSysOpenConf();
			Variant variant = new Variant();
			foreach (Variant current in sysOpenConf.Values)
			{
				using (List<Variant>.Enumerator enumerator2 = arr._arr.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						int num = enumerator2.Current;
						bool flag = current["btnpos"] == num;
						if (flag)
						{
							variant._arr.Add(current);
							break;
						}
					}
				}
			}
			return variant;
		}

		public Variant HandleIconShow(Variant allArr, Variant showArr, string typename, bool isopen, Variant data = null)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			bool flag = data != null && data.ContainsKey("ref");
			if (flag)
			{
				variant2 = data["ref"];
			}
			for (int i = 0; i < allArr.Count; i++)
			{
				Variant variant3 = allArr[i];
				bool flag2 = variant3 == null;
				if (!flag2)
				{
					bool flag3 = variant3["oid"] == typename;
					if (flag3)
					{
						int num = showArr._arr.IndexOf(variant3);
						if (isopen)
						{
							bool flag4 = num == -1;
							if (!flag4)
							{
								break;
							}
							bool flag5 = variant2 != null;
							if (flag5)
							{
								Variant variant4 = new Variant();
								variant4["data"] = variant3;
								variant4["todo"] = "add";
								variant4["id"] = typename;
								variant2._arr.Add(variant4);
							}
							showArr._arr.Add(variant3);
						}
						else
						{
							bool flag6 = num != -1;
							if (!flag6)
							{
								break;
							}
							bool flag7 = variant2 != null;
							if (flag7)
							{
								Variant variant5 = new Variant();
								variant5["id"] = typename;
								variant5["todo"] = "remove";
								variant2._arr.Add(variant5);
							}
							showArr._arr.RemoveAt(num);
						}
						foreach (Variant current in showArr._arr)
						{
							bool flag8 = current["idx"] < variant3["idx"];
							if (flag8)
							{
								bool flag9 = current["btnpos"] != variant3["btnpos"];
								if (!flag9)
								{
									variant._arr.Add(current);
								}
							}
						}
					}
				}
			}
			return variant;
		}

		public bool CostCheck(Variant cost)
		{
			bool flag = cost.ContainsKey("itm");
			bool result;
			if (flag)
			{
				Variant variant = cost["itm"];
				foreach (Variant current in variant.Values)
				{
					bool flag2 = current.ContainsKey("tpid");
					int tpid;
					if (flag2)
					{
						tpid = current["tpid"];
					}
					else
					{
						tpid = current["id"];
					}
					int itemCount = (this._uiClient.g_gameM as muLGClient).g_itemsCT.GetItemCount((uint)tpid);
					bool flag3 = itemCount < current["cnt"];
					if (flag3)
					{
						result = false;
						return result;
					}
				}
			}
			bool flag4 = cost.ContainsKey("gld") || cost.ContainsKey("gold");
			if (flag4)
			{
				bool flag5 = cost.ContainsKey("gld");
				if (flag5)
				{
					int num = cost["gld"];
				}
				else
				{
					int num = cost["gold"];
				}
			}
			bool flag6 = cost.ContainsKey("yb");
			if (flag6)
			{
				int num = cost["yb"];
			}
			result = true;
			return result;
		}

		public int GetCombpt(Variant item, Variant conf = null)
		{
			Variant combptConf = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetCombptConf();
			bool flag = item == null || combptConf == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				uint tpid = 0u;
				bool flag2 = item.ContainsKey("tpid");
				if (flag2)
				{
					tpid = item["tpid"];
				}
				else
				{
					bool flag3 = item.ContainsKey("id");
					if (flag3)
					{
						tpid = item["id"];
					}
				}
				bool flag4 = conf == null;
				Variant variant2;
				if (flag4)
				{
					Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(tpid);
					variant2 = variant["conf"];
				}
				else
				{
					variant2 = conf;
				}
				bool flag5 = variant2 == null;
				if (flag5)
				{
					result = 0;
				}
				else
				{
					int num = 0;
					int num2 = 0;
					bool flag6 = item.ContainsKey("flvl");
					if (flag6)
					{
						num2 = item["flvl"];
					}
					bool flag7 = variant2.ContainsKey("forge_lvl_att");
					if (flag7)
					{
						Variant variant3 = variant2["forge_lvl_att"][1];
						bool flag8 = variant3 != null && variant3.ContainsKey("lvl") && num2 >= variant3["lvl"];
						if (flag8)
						{
							foreach (string current in variant3.Keys)
							{
								bool flag9 = combptConf.ContainsKey(current);
								if (flag9)
								{
									int num3 = variant3[current];
									num += (int)((double)(combptConf[current] * num3) * 0.001);
								}
							}
						}
					}
					bool flag10 = variant2.ContainsKey("forge_att");
					if (flag10)
					{
						Variant variant4 = variant2["forge_att"][0];
						foreach (string current2 in variant4.Keys)
						{
							int num3 = variant4[current2] + this.GetForgeAttLvlAdd(current2, num2);
							num += (int)((double)((combptConf.ContainsKey(current2) ? combptConf[current2]._int : 0) * num3) * 0.001);
						}
					}
					int num4 = 0;
					bool flag11 = this.IsExatt(item);
					foreach (string current3 in variant2.Keys)
					{
						bool isNumber = variant2[current3].isNumber;
						if (isNumber)
						{
							bool flag12 = combptConf.ContainsKey(current3);
							if (flag12)
							{
								int num3 = this.GetForgeAttLvlAdd(current3, num2);
								num3 += variant2[current3];
								bool flag13 = flag11;
								if (flag13)
								{
									bool flag14 = variant2.ContainsKey("lv");
									int lv;
									if (flag14)
									{
										lv = variant2["lv"];
									}
									else
									{
										lv = 1;
									}
									num4 = this.ExattAdd(variant2[current3], lv, current3);
								}
								num3 += num4;
								num += (int)((double)(combptConf[current3] * num3) * 0.001);
							}
						}
					}
					Variant variant5 = new Variant();
					variant5.pushBack(1);
					bool flag15 = item.ContainsKey("flag");
					if (flag15)
					{
						for (int i = 0; i < variant5.Length; i++)
						{
							int num5 = variant5[i];
							bool flag16 = num5 <= item["flag"];
							if (flag16)
							{
								bool flag17 = (item["flag"] & num5) == num5;
								if (flag17)
								{
									bool flag18 = 1 == num5;
									if (flag18)
									{
										Variant luckAtt = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.GetLuckAtt();
										bool flag19 = luckAtt != null;
										if (flag19)
										{
											Variant variant6 = luckAtt[0]["att"];
											foreach (Variant current4 in variant6._arr)
											{
												int num3 = current4["val"];
												num += (int)((double)((combptConf.ContainsKey(current4["name"]._str) ? combptConf[current4["name"]._str]._int : 0) * num3) * 0.001);
											}
										}
									}
								}
							}
						}
					}
					bool flag20 = item.ContainsKey("fp");
					if (flag20)
					{
						Variant variant7 = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.DecodeAddatt(item["fp"]._int);
						Variant add_att = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_add_att();
						bool flag21 = variant7 != null && add_att != null;
						if (flag21)
						{
							foreach (Variant current5 in variant7._arr)
							{
								bool flag22 = current5["id"] <= 0;
								if (!flag22)
								{
									foreach (Variant current6 in add_att._arr)
									{
										bool flag23 = current5["id"] == current6["id"];
										if (flag23)
										{
											Variant key = current6["att"][0]["name"];
											int num3 = current6["att"][0]["add"] * current5["lvl"] + current6["att"][0]["base"];
											num += (int)((double)(combptConf[key] * num3) * 0.001);
											break;
										}
									}
								}
							}
						}
					}
					bool flag24 = item.ContainsKey("fpt");
					if (flag24)
					{
						int num6 = item["fpt"];
						bool flag25 = num6 > 0;
						if (flag25)
						{
							Variant variant8 = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.DecodeSupfrgatt(num6);
							Variant sup_frg_att = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_sup_frg_att();
							for (int j = 0; j < variant8.Count; j++)
							{
								Variant variant9 = variant8[j];
								bool flag26 = variant9["id"] > 0;
								if (flag26)
								{
									int idx = variant9["id"];
									int num7 = variant9["lvl"];
									bool flag27 = sup_frg_att[idx] != null;
									if (flag27)
									{
										Variant variant10 = sup_frg_att[idx]["att"][0]["name"];
										int num8 = sup_frg_att[idx]["att"][0]["val"];
										int num3 = 0;
										bool flag28 = num7 == 0;
										if (flag28)
										{
											num3 = num8;
										}
										else
										{
											bool flag29 = num7 > 0;
											if (flag29)
											{
												num3 = num8 + this.GetSupfrgAdd(variant10, num7);
											}
										}
										num += (int)((double)((combptConf.ContainsKey(variant10._str) ? combptConf[variant10._str]._int : 0) * num3) * 0.001);
									}
								}
							}
						}
					}
					Variant variant11 = new Variant();
					Variant variant12 = new Variant();
					variant12.pushBack("normal_att");
					variant12.pushBack("unstack_att");
					using (List<Variant>.Enumerator enumerator7 = variant12._arr.GetEnumerator())
					{
						while (enumerator7.MoveNext())
						{
							string key2 = enumerator7.Current;
							bool flag30 = variant2.ContainsKey(key2);
							if (flag30)
							{
								Variant variant13 = variant2[key2][0];
								foreach (string current7 in variant13.Keys)
								{
									bool flag31 = variant11.ContainsKey(current7);
									if (flag31)
									{
										Variant variant14 = variant11;
										string key3 = current7;
										variant14[key3] += variant13[current7]._int;
									}
									else
									{
										variant11[current7] = variant13[current7];
									}
								}
							}
						}
					}
					bool flag32 = variant11 != null && variant11.Keys != null;
					if (flag32)
					{
						foreach (string current8 in variant11.Keys)
						{
							int num3 = variant11[current8];
							num += (int)((double)((combptConf.ContainsKey(current8) ? combptConf[current8]._int : 0) * num3) * 0.001);
						}
					}
					bool flag33 = item.ContainsKey("exatt") && item["exatt"] > 0;
					if (flag33)
					{
						int exattCombpt = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetExattCombpt(item["exatt"], 1);
						num += (int)((double)exattCombpt * 0.001);
					}
					result = num;
				}
			}
			return result;
		}

		public string CheckEquip(Variant equip, Variant detail_info, Variant conf, int eqpCpt = 0, int curCpt = 0)
		{
			string text = "";
			bool flag = equip.ContainsKey("expire") && equip["expire"] > 0;
			string result;
			if (flag)
			{
				float num = (float)((this._uiClient.g_netM as muNetCleint).CurServerTimeStampMS / 1000L);
				bool flag2 = equip["expire"] < num;
				if (flag2)
				{
					text = "";
					result = text;
					return result;
				}
			}
			bool flag3 = conf.ContainsKey("attchk");
			if (flag3)
			{
				foreach (Variant current in conf["attchk"]._arr)
				{
					bool flag4 = current.ContainsKey("and");
					if (flag4)
					{
						bool flag5 = "carr" == current["name"];
						if (flag5)
						{
							int num2 = this.check_only_carr(current["and"], detail_info);
							bool flag6 = num2 == -1;
							if (flag6)
							{
								result = text;
								return result;
							}
						}
					}
				}
			}
			Variant currentEquip = this.GetCurrentEquip(conf, detail_info);
			bool flag7 = currentEquip.Count == 0;
			if (flag7)
			{
				text = (this._uiClient.g_gameConfM as muCLientConfig).localImgRes.GetImgRes("grade_up");
				result = text;
			}
			else
			{
				int num3 = curCpt;
				bool flag8 = curCpt <= 0;
				if (flag8)
				{
					foreach (Variant current2 in currentEquip._arr)
					{
						int combpt = this.GetCombpt(current2, null);
						bool flag9 = conf["pos"] == 6 && conf.ContainsKey("takepos") && conf["takepos"] == 2;
						if (flag9)
						{
							num3 += combpt;
						}
						else
						{
							bool flag10 = num3 == 0 || num3 > combpt;
							if (flag10)
							{
								num3 = combpt;
							}
						}
					}
				}
				int num4 = eqpCpt;
				bool flag11 = eqpCpt <= 0;
				if (flag11)
				{
					num4 = this.GetCombpt(equip, null);
				}
				bool flag12 = num3 < num4;
				if (flag12)
				{
					text = (this._uiClient.g_gameConfM as muCLientConfig).localImgRes.GetImgRes("grade_up");
				}
				else
				{
					bool flag13 = num3 > num4;
					if (flag13)
					{
						text = (this._uiClient.g_gameConfM as muCLientConfig).localImgRes.GetImgRes("grade_down");
					}
				}
				result = text;
			}
			return result;
		}

		public Variant GetCurrentEquip(Variant c_conf, Variant detail_info)
		{
			Variant variant = new Variant();
			bool flag = !detail_info.ContainsKey("equip") || detail_info["equip"].Count <= 0;
			Variant result;
			if (flag)
			{
				result = variant;
			}
			else
			{
				int num = c_conf["pos"];
				Variant variant2 = detail_info["equip"];
				bool flag2 = false;
				bool flag3 = variant2 != null;
				if (flag3)
				{
					foreach (Variant current in variant2._arr)
					{
						Variant variant3 = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]._uint);
						bool flag4 = variant3 == null;
						if (!flag4)
						{
							bool flag5 = variant3["conf"]["pos"] == num;
							if (flag5)
							{
								variant._arr.Add(current);
								bool flag6 = num == 6;
								if (flag6)
								{
									bool flag7 = c_conf != null && c_conf.ContainsKey("posuniq") && c_conf["posuniq"] != 0;
									if (flag7)
									{
										bool flag8 = c_conf["posuniq"] == variant3["conf"]["posuniq"];
										if (flag8)
										{
											flag2 = true;
											variant = null;
											variant._arr.Add(current);
											break;
										}
									}
								}
								bool flag9 = num != 9 && num != 6;
								if (flag9)
								{
									break;
								}
								bool flag10 = variant.Length == 2;
								if (flag10)
								{
									break;
								}
							}
						}
					}
				}
				bool flag11 = num == 6 && !flag2 && variant.Length < 2;
				if (flag11)
				{
					bool flag12 = c_conf.ContainsKey("takepos") && c_conf["takepos"]._int == 1;
					if (flag12)
					{
						bool flag13 = variant.Length == 1 && variant[0].ContainsKey("takepos") && variant[0]["takepos"] == 1;
						if (flag13)
						{
							variant = null;
						}
					}
				}
				result = variant;
			}
			return result;
		}

		public bool CheckEqp(Variant data, Variant needEqps)
		{
			int num = 0;
			bool result;
			foreach (Variant current in needEqps._arr)
			{
				bool flag = true;
				bool flag2 = true;
				bool flag3 = true;
				bool flag4 = data.ContainsKey("attchk");
				if (flag4)
				{
					flag = false;
					foreach (Variant current2 in data["attchk"]._arr)
					{
						string b = current2["fun"];
						string text = current2["name"];
						int num2 = current2["val"];
						int num3 = 0;
						bool flag5 = "fp" == text;
						if (flag5)
						{
							bool flag6 = current["data"].ContainsKey("fp") && current["data"]["fp"] > 0;
							if (flag6)
							{
								Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.DecodeAddatt(current["data"]["fp"]._int);
								num3 = variant[0]["lvl"];
							}
						}
						else
						{
							bool flag7 = "id" == text;
							if (flag7)
							{
								num3 = current["data"]["tpid"];
							}
							else
							{
								bool flag8 = current["data"].ContainsKey(text) && current["data"][text] > 0;
								if (flag8)
								{
									num3 = current["data"][text];
								}
							}
						}
						bool flag9 = "min" == b;
						if (flag9)
						{
							bool flag10 = num3 >= num2;
							if (!flag10)
							{
								result = false;
								return result;
							}
							flag = true;
						}
						else
						{
							bool flag11 = "max" == b;
							if (flag11)
							{
								bool flag12 = num3 <= num2;
								if (!flag12)
								{
									result = false;
									return result;
								}
								flag = true;
							}
							else
							{
								bool flag13 = "equal" == b;
								if (flag13)
								{
									bool flag14 = num3 == num2;
									if (!flag14)
									{
										result = false;
										return result;
									}
									flag = true;
								}
							}
						}
					}
				}
				bool flag15 = data.ContainsKey("cfgchk");
				if (flag15)
				{
					flag3 = false;
					foreach (Variant current3 in data["cfgchk"]._arr)
					{
						string b = current3["fun"];
						string text = current3["name"];
						int num2 = current3["val"];
						int num3 = 0;
						bool flag16 = "id" == text;
						if (flag16)
						{
							num3 = current["data"]["tpid"];
						}
						else
						{
							bool flag17 = current["data"].ContainsKey(text) && current["data"][text] > 0;
							if (flag17)
							{
								num3 = current["data"][text];
							}
						}
						bool flag18 = "equal" == b;
						if (flag18)
						{
							bool flag19 = num3 == num2;
							if (flag19)
							{
								flag3 = true;
								break;
							}
						}
						else
						{
							bool flag20 = "min" == b;
							if (flag20)
							{
								bool flag21 = num3 >= num2;
								if (!flag21)
								{
									result = false;
									return result;
								}
								flag3 = true;
							}
							else
							{
								bool flag22 = "max" == b;
								if (flag22)
								{
									bool flag23 = num3 <= num2;
									if (!flag23)
									{
										result = false;
										return result;
									}
									flag3 = true;
								}
							}
						}
					}
				}
				bool flag24 = data.ContainsKey("n_match_1");
				if (flag24)
				{
					flag2 = false;
					bool flag25 = data["n_match_1"][0].ContainsKey("match_chk");
					if (flag25)
					{
						Variant variant2 = data["n_match_1"][0]["match_chk"];
						foreach (Variant current4 in variant2._arr)
						{
							string b = current4["cfgchk"][0]["fun"];
							string text = current4["cfgchk"][0]["name"];
							int num2 = current4["cfgchk"][0]["val"];
							int num3 = 0;
							bool flag26 = "id" == text;
							if (flag26)
							{
								num3 = current["data"]["tpid"];
							}
							else
							{
								bool flag27 = current["data"].ContainsKey(text) && current["data"][text] > 0;
								if (flag27)
								{
									num3 = current["data"][text];
								}
							}
							bool flag28 = "equal" == b;
							if (flag28)
							{
								bool flag29 = num3 == num2;
								if (flag29)
								{
									flag2 = true;
									break;
								}
							}
							else
							{
								bool flag30 = "min" == b;
								if (flag30)
								{
									bool flag31 = num3 >= num2;
									if (!flag31)
									{
										result = false;
										return result;
									}
									flag2 = true;
								}
								else
								{
									bool flag32 = "max" == b;
									if (flag32)
									{
										bool flag33 = num3 <= num2;
										if (!flag33)
										{
											result = false;
											return result;
										}
										flag2 = true;
									}
								}
							}
						}
					}
				}
				bool flag34 = flag & flag2 & flag3;
				if (flag34)
				{
					num++;
				}
				bool flag35 = num == data["cnt"];
				if (flag35)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public void GotoChargePvip()
		{
		}

		public void GotoChargeYearPvip()
		{
		}

		public void BuyPvip()
		{
		}

		public uint getMountCombpt(Variant mountAtt)
		{
			uint num = 0u;
			bool flag = mountAtt == null;
			uint result;
			if (flag)
			{
				result = num;
			}
			else
			{
				Variant combptConf = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetCombptConf();
				foreach (string current in mountAtt.Keys)
				{
					bool flag2 = !combptConf.ContainsKey(current);
					if (flag2)
					{
						bool flag3 = "atk_rate_mul" == current;
						if (flag3)
						{
							num += (uint)((double)(mountAtt[current] * combptConf["atk_rate"]) / 1000.0);
						}
						bool flag4 = "miss_rate_mul" == current;
						if (flag4)
						{
							num += (uint)((double)(mountAtt[current] * combptConf["miss_rate"]) / 1000.0);
						}
					}
					else
					{
						num += (uint)((double)(mountAtt[current] * combptConf[current]) / 1000.0);
					}
				}
				result = num;
			}
			return result;
		}

		public bool CheckName(string name)
		{
			bool flag = name == "";
			bool result;
			if (flag)
			{
				string languageText = LanguagePack.getLanguageText("UI_Character_create", "inputname");
				result = false;
			}
			else
			{
				bool flag2 = name.IndexOf(" ") > -1;
				if (flag2)
				{
					string languageText = LanguagePack.getLanguageText("UI_Character_create", "no_space");
					result = false;
				}
				else
				{
					bool flag3 = !this.isNameValidLength(name);
					if (flag3)
					{
						string languageText = LanguagePack.getLanguageText("UI_Character_create", "sixchar");
						result = false;
					}
					else
					{
						bool flag4 = !this.isNameCharValid(name);
						if (flag4)
						{
							string languageText = LanguagePack.getLanguageText("UI_Character_create", "validchar");
							result = false;
						}
						else
						{
							bool flag5 = this.HasShieldWord();
							if (flag5)
							{
								Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).localShield.isHaveWord(name);
								bool flag6 = variant[0];
								if (flag6)
								{
									string languageText = LanguagePack.getLanguageText("UI_Character_create", "invalidchar");
									result = false;
									return result;
								}
							}
							result = true;
						}
					}
				}
			}
			return result;
		}

		private bool isNameValidLength(string name)
		{
			bool result = false;
			ByteArray byteArray = new ByteArray();
			return result;
		}

		private bool isNameCharValid(string name)
		{
			return true;
		}

		public bool HasShieldWord()
		{
			return false;
		}

		public void RecommendAuto(int mapid)
		{
			int id = 20000 + mapid;
			ClientGeneralConf localGeneral = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral;
			Variant randPosConfById = localGeneral.getRandPosConfById(id);
			bool flag = randPosConfById != null;
			if (flag)
			{
				Variant variant = randPosConfById["pos"];
				int @int = variant["length"]._int;
				Random random = new Random();
				double num = random.NextDouble();
				int idx = (int)Math.Floor(num * (double)@int);
				Variant variant2 = variant[idx];
			}
		}

		public string getFuzzyMapUrl(uint mapid)
		{
			bool flag = this._fuzzyMap == null;
			if (flag)
			{
				this._fuzzyMap = (this._uiClient.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("fuzzy")._str;
			}
			return string.Format(this._fuzzyMap, mapid);
		}

		public Variant GetEqpExattCnt(Variant equips, Variant eqppos)
		{
			int num = 0;
			foreach (Variant current in equips.Values)
			{
				Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]._uint);
				int val = variant["conf"]["pos"];
				int num2 = eqppos._arr.IndexOf(val);
				bool flag = num2 == -1;
				if (!flag)
				{
					bool flag2 = current.ContainsKey("exatt") && current["exatt"] > 0;
					if (flag2)
					{
						Variant variant2 = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.DecodeExatt(current["exatt"]._int, current);
						num += variant2.Count;
					}
				}
			}
			Variant variant3 = new Variant();
			variant3["cnt "] = num;
			return variant3;
		}

		public Variant GetEqpFlvlCnt(Variant equips, Variant eqppos)
		{
			int num = 0;
			foreach (Variant current in equips.Values)
			{
				Variant variant = (this._uiClient.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]._uint);
				int @int = variant["conf"]["pos"]._int;
				int num2 = eqppos._arr.IndexOf(@int);
				bool flag = num2 == -1;
				if (!flag)
				{
					bool flag2 = current.ContainsKey("flvl") && current["flvl"] > 0;
					if (flag2)
					{
						num += current["flvl"];
					}
				}
			}
			Variant variant2 = new Variant();
			variant2["flvl "] = num;
			return variant2;
		}

		public uint GetClanpt(uint clangld, uint clangyb, Variant data = null)
		{
			uint num = 0u;
			float num2 = (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.get_game_general_data("yb_clan_point_per");
			float num3 = (this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.get_game_general_data("gld_clan_point_per");
			uint num4 = (uint)(this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.get_game_general_data("clan_day_mission_maxyb");
			uint num5 = (uint)(this._uiClient.g_gameConfM as muCLientConfig).svrGeneralConf.get_game_general_data("clan_day_mission_maxgold");
			bool flag = data != null;
			if (flag)
			{
				bool flag2 = data.ContainsKey("awd");
				if (flag2)
				{
					num += (uint)Math.Round((double)(num3 * clangld / 1000f));
					num += (uint)(num2 * clangyb / 1000f);
				}
				else
				{
					bool flag3 = data.ContainsKey("max");
					if (flag3)
					{
						num += (uint)Math.Round((double)(num3 * num5 / 1000f));
						num += (uint)(num2 * num4 / 1000f);
					}
				}
			}
			else
			{
				num += (uint)Math.Round((double)(num3 * clangld / 1000f));
				num += (uint)(num2 * clangyb / 1000f);
			}
			return num;
		}
	}
}
