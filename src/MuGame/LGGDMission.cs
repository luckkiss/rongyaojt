using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LGGDMission : lgGDBase
	{
		private Variant _line_data = new Variant();

		private Variant _no_line_data = new Variant();

		private Variant _playerMiss = new Variant();

		private Variant _acceptable = new Variant();

		private uint _current_map_id = 0u;

		private Variant _mis_uitm_arr = new Variant();

		private Variant _mis_operate_arr = new Variant();

		private Variant _mis_qa_arr = new Variant();

		private Variant _mis_cgoal_arr = new Variant();

		private Variant _findwayobj = null;

		private Variant _misAction = new Variant();

		private bool _addfavorite = false;

		private Variant _checks_arr;

		private int _killmon_map_id = 0;

		private Vec2 _killmon_pos = null;

		private Variant _killmon_mons = null;

		private Vec2 _enter_level_pos = null;

		private int _enter_level_id = 0;

		private int _enter_level_tp = 0;

		protected int _mlineawd = 0;

		private Variant _fingmis;

		private Variant _finvips;

		private Variant _killmons = new Variant();

		private LGIUIMission uiMis
		{
			get
			{
				return null;
			}
		}

		private LGIUIMainUI lguiMain
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			}
		}

		public Variant misacept
		{
			get
			{
				return this._playerMiss;
			}
		}

		public Variant no_line_data
		{
			get
			{
				return this._no_line_data;
			}
		}

		public Variant autocomit_mission
		{
			get
			{
				Variant variant = new Variant();
				int num = 0;
				Variant autocomit_mis = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_autocomit_mis();
				foreach (string current in autocomit_mis.Keys)
				{
					Variant variant2 = autocomit_mis[current];
					foreach (string current2 in variant2.Keys)
					{
						Variant variant3 = variant2[current2];
						int mid = variant3["data"]["id"];
						bool flag = num >= int.Parse(current) && num <= variant3["mx"]._int;
						if (flag)
						{
							bool flag2 = this.isautocomitshow(mid);
							if (flag2)
							{
								variant.pushBack(variant3["data"]);
							}
						}
					}
				}
				return variant;
			}
		}

		public Variant acceptable_mis
		{
			get
			{
				return this._acceptable;
			}
		}

		public Variant activity_mis
		{
			get
			{
				Variant variant = new Variant();
				bool flag = this._acceptable.Count == 0;
				Variant result;
				if (flag)
				{
					result = variant;
				}
				else
				{
					foreach (Variant current in this._acceptable._arr)
					{
						bool flag2 = current == null;
						if (!flag2)
						{
							bool flag3 = current["misline"] == 0;
							if (flag3)
							{
								variant.pushBack(current);
							}
						}
					}
					result = variant;
				}
				return result;
			}
		}

		private MediaClient m_media
		{
			get
			{
				return this.g_mgr.getObject("LG_MEDIA") as MediaClient;
			}
		}

		private lgSelfPlayer selfPlayer
		{
			get
			{
				return this.g_mgr.getObject("LG_MAIN_PLAY") as lgSelfPlayer;
			}
		}

		public LGGDMission(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGDMission(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListener(43u, new Action<GameEvent>(this.onGmisAwdRes));
			this.g_mgr.g_netM.addEventListener(110u, new Action<GameEvent>(this.onAcceptMisRes));
			this.g_mgr.g_netM.addEventListener(111u, new Action<GameEvent>(this.onCommitMisRes));
			this.g_mgr.g_netM.addEventListener(112u, new Action<GameEvent>(this.onFinedMisStateRes));
			this.g_mgr.g_netM.addEventListener(113u, new Action<GameEvent>(this.onDataMisModityRes));
			this.g_mgr.g_netM.addEventListener(114u, new Action<GameEvent>(this.onMisLineStateRes));
			this.g_mgr.g_netM.addEventListener(115u, new Action<GameEvent>(this.onAbordMisRes));
		}

		public void onJoinWorldRes(GameEvent e)
		{
		}

		public void onGmisInfoRes(GameEvent e)
		{
			Variant data = e.data;
			this.on_get_gmis(data);
		}

		public void onGmisAwdRes(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("res") && data["res"]._int < 0;
			if (!flag)
			{
				this.on_get_gmisawd(data);
			}
		}

		public void onAcceptMisRes(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("res") && data["res"]._int < 0;
			if (flag)
			{
				this.lguiMain.output_server_err(data["res"]._int);
			}
			else
			{
				this.accept_mis_res(data);
			}
		}

		public void onCommitMisRes(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("res") && data["res"]._int < 0;
			if (flag)
			{
				this.lguiMain.output_server_err(data["res"]._int);
			}
			else
			{
				this.commit_mis(data);
			}
		}

		public void onFinedMisStateRes(GameEvent e)
		{
			Variant data = e.data;
			this.get_fined_mis_state_res(data);
		}

		public void onDataMisModityRes(GameEvent e)
		{
			Variant data = e.data;
			this.mis_data_modify(data);
		}

		public void onMisLineStateRes(GameEvent e)
		{
			Variant data = e.data;
			this.get_mis_line_state_res(data);
		}

		public void onAbordMisRes(GameEvent e)
		{
			Variant data = e.data;
			this.abord_mis(data);
		}

		public void onLvlMisPrizeRes(GameEvent e)
		{
			Variant data = e.data;
			this.lvlMisChange();
		}

		public Variant get_accept_mis_info(int misid)
		{
			return this._playerMiss[misid.ToString()];
		}

		public int GetMainMisId()
		{
			bool flag = this._playerMiss.Count > 0;
			int result;
			if (flag)
			{
				foreach (Variant current in this._playerMiss.Values)
				{
					bool flag2 = current != null && current["configdata"]["misline"]._int == 1;
					if (flag2)
					{
						result = current["misid"];
						return result;
					}
				}
			}
			bool flag3 = this._acceptable.Count > 0;
			if (flag3)
			{
				foreach (Variant current2 in this._acceptable._arr)
				{
					bool flag4 = current2 != null && current2["misline"]._int == 1;
					if (flag4)
					{
						result = current2["id"];
						return result;
					}
				}
			}
			result = 0;
			return result;
		}

		public void set_accepted_mis(Variant misacept)
		{
			foreach (Variant current in misacept._arr)
			{
				this.addPlayerMiss(current);
			}
			this.init_local_miss();
		}

		public int get_line_proc(int misline)
		{
			return this._line_data[misline.ToString()];
		}

		private bool addPlayerMiss(Variant data)
		{
			int num = data["misid"];
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(num);
			bool flag = variant != null;
			bool result;
			if (flag)
			{
				bool flag2 = data.ContainsKey("action");
				if (flag2)
				{
					this._misAction[num.ToString()] = data["action"];
				}
				Variant variant2 = new Variant();
				variant2["misid"] = num;
				variant2["data"] = data;
				variant2["configdata"] = variant;
				variant2["isComplete"] = false;
				int goalid = 0;
				bool flag3 = data.ContainsKey("goalid");
				if (flag3)
				{
					goalid = data["goalid"];
				}
				variant2["goal"] = this.get_mission_goal(variant, goalid);
				this._playerMiss[num.ToString()] = variant2;
				this.updataAcceptMisState(variant2, false);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		private void updataAcceptMisState(Variant acceptObj, bool flag = false)
		{
			Variant variant = acceptObj["goal"];
			int num = acceptObj["misid"];
			bool flag2 = true;
			Variant variant2 = acceptObj["data"];
			Variant variant3 = acceptObj["configdata"];
			int num2 = 0;
			bool flag3 = variant.ContainsKey("jcamp");
			if (flag3)
			{
			}
			bool flag4 = variant.ContainsKey("microclient") && variant["microclient"] == 1;
			if (flag4)
			{
				flag2 = true;
			}
			bool flag5 = variant.ContainsKey("addfav");
			if (flag5)
			{
				bool addfavorite = this._addfavorite;
				flag2 = addfavorite;
			}
			bool flag6 = variant.ContainsKey("uitm");
			if (flag6)
			{
				uint tpid = variant["uitm"];
				bool flag7 = this._mis_uitm_arr.ContainsKey(tpid.ToString());
				if (flag7)
				{
					uint num3 = (this.g_mgr as muLGClient).g_itemsCT.pkg_get_item_count_bytpid(tpid);
					bool flag8 = num3 <= 0u;
					if (flag8)
					{
						this._mis_uitm_arr[tpid.ToString()]["complete"] = true;
					}
					bool flag9 = !this._mis_uitm_arr[tpid.ToString()]["complete"];
					flag2 = !flag9;
				}
			}
			bool flag10 = variant.ContainsKey("clientgoal");
			if (flag10)
			{
				Variant variant4 = this._mis_cgoal_arr[variant["clientgoal"]];
				flag2 = (variant4 != null && variant4["complete"]._bool);
			}
			bool flag11 = variant.ContainsKey("operate");
			if (flag11)
			{
				Variant variant5 = this._mis_operate_arr[variant["operate"]];
				bool flag12 = variant5 == null || variant5["complete"]._bool;
				flag2 = flag12;
			}
			bool flag13 = variant.ContainsKey("qa");
			if (flag13)
			{
				Variant variant6 = variant["qa"];
				int num4 = variant6["qamis"];
				bool flag14 = this._mis_qa_arr.ContainsKey(num4.ToString());
				if (flag14)
				{
					Variant variant7 = this._mis_qa_arr[num4.ToString()];
					bool flag15 = !variant7["complete"];
					if (flag15)
					{
						flag2 = false;
					}
				}
			}
			bool flag16 = variant.ContainsKey("colmon");
			if (flag16)
			{
				Variant variant8 = variant["colmon"];
				for (int i = 0; i < variant8.Count; i++)
				{
					Variant variant9 = variant8[i];
					string text = this.get_need_item_str(variant9, num);
					bool flag17 = acceptObj != null && variant2 != null && variant2.ContainsKey("colm");
					if (flag17)
					{
						num2 = variant2["colm"][i]["cnt"];
					}
					bool flag18 = num2 < variant9["cnt"]._int;
					if (flag18)
					{
						flag2 = false;
					}
				}
			}
			bool flag19 = variant.ContainsKey("colitm");
			if (flag19)
			{
				Variant variant10 = variant["colitm"];
				for (int j = 0; j < variant10.Count; j++)
				{
					Variant variant11 = variant10[j];
					Variant variant12 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(variant11["tpid"]._uint);
					bool flag20 = variant12 == null;
					if (flag20)
					{
						flag2 = false;
					}
					else
					{
						int num5 = (int)(this.g_mgr as muLGClient).g_itemsCT.pkg_get_item_count_bytpid(variant11["tpid"]._uint);
						bool flag21 = num5 < variant11["cnt"]._int;
						if (flag21)
						{
							flag2 = false;
						}
					}
				}
			}
			bool flag22 = variant.ContainsKey("ownitm");
			if (flag22)
			{
				Variant variant13 = variant["ownitm"];
				for (int j = 0; j < variant13.Count; j++)
				{
					Variant variant14 = variant13[j];
					Variant variant15 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(variant14["tpid"]._uint);
					bool flag23 = variant15 == null;
					if (flag23)
					{
						flag2 = false;
					}
					else
					{
						int num5 = (int)(this.g_mgr as muLGClient).g_itemsCT.pkg_get_item_count_bytpid(variant14["tpid"]._uint);
						bool flag24 = num5 < variant14["cnt"];
						if (flag24)
						{
							flag2 = false;
						}
					}
				}
			}
			bool flag25 = variant.ContainsKey("kilmon");
			if (flag25)
			{
				Variant variant16 = variant["kilmon"];
				for (int j = 0; j < variant16.Count; j++)
				{
					Variant variant17 = variant16[j];
					Variant variant18 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMonsterConf.get_monster_data(variant17["monid"]._int);
					bool flag26 = variant18 == null;
					if (flag26)
					{
						flag2 = false;
					}
					else
					{
						int num6 = 0;
						float num7 = 100f;
						bool flag27 = 5 == this.get_mis_type(num);
						if (flag27)
						{
							Variant appawdRmis = (this.g_mgr as muLGClient).g_rmissCT.GetAppawdRmis(num);
							Variant playerRmisInfo = (this.g_mgr as muLGClient).g_rmissCT.GetPlayerRmisInfo(variant3["rmis"]._int);
							bool flag28 = appawdRmis != null;
							if (flag28)
							{
								Variant variant19 = appawdRmis["goal"];
								Variant variant20 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_appgoal(variant19["id"]._int);
								Variant objectBykeyValue = LGGDMission.getObjectBykeyValue(variant20["qual_grp"], "qual", variant19["qual"]);
								num7 = (float)(100 + objectBykeyValue["per"]);
							}
							else
							{
								bool flag29 = playerRmisInfo != null && playerRmisInfo["misid"] == num;
								if (flag29)
								{
									Variant variant19 = playerRmisInfo["appgoal"];
									Variant variant20 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_appgoal(variant19["appgoal"]._int);
									int val = 1;
									bool flag30 = variant19["qual"] != 0;
									if (flag30)
									{
										val = variant19["qual"];
									}
									Variant objectBykeyValue = LGGDMission.getObjectBykeyValue(variant20["qual_grp"], "qual", val);
									num7 = (float)(100 + objectBykeyValue["per"]);
								}
							}
						}
						bool flag31 = acceptObj != null && variant2 != null;
						if (flag31)
						{
							Variant variant21 = variant2["km"];
							bool flag32 = variant21 == null;
							if (flag32)
							{
								goto IL_7E3;
							}
							for (int k = 0; k < variant21.Count; k++)
							{
								Variant variant22 = variant21[k];
								bool flag33 = variant22["monid"]._int == variant17["monid"]._int;
								if (flag33)
								{
									num6 = variant22["cnt"];
									break;
								}
							}
						}
						bool flag34 = num6 < (int)(variant17["cnt"] * num7 / 100f);
						if (flag34)
						{
							flag2 = false;
						}
					}
					IL_7E3:;
				}
			}
			bool flag35 = variant.ContainsKey("pzckp");
			if (flag35)
			{
				Variant variant23 = variant["pzckp"];
				bool flag36 = acceptObj != null && variant2 != null && variant2.ContainsKey("pzckp");
				if (flag36)
				{
					bool flag37 = variant2["pzckp"] == 1;
					flag2 = flag37;
				}
			}
			bool flag38 = variant.ContainsKey("pzkp");
			if (flag38)
			{
				Variant variant24 = variant["pzkp"];
				int num8 = 0;
				bool flag39 = acceptObj != null && variant2 != null && variant2.ContainsKey("pzkp");
				if (flag39)
				{
					num8 = variant2["pzkp"];
				}
				bool flag40 = num8 < variant24[0]["cnt"];
				if (flag40)
				{
					flag2 = false;
				}
			}
			bool flag41 = variant.ContainsKey("kp");
			if (flag41)
			{
				Variant variant25 = variant["kp"];
				int num9 = 0;
				bool flag42 = acceptObj != null && variant2 != null && variant2.ContainsKey("kp");
				if (flag42)
				{
					num9 = variant2["kp"];
				}
				bool flag43 = num9 < variant25[0]["cnt"];
				if (flag43)
				{
					flag2 = false;
				}
			}
			bool flag44 = variant.ContainsKey("joinclan");
			if (flag44)
			{
				Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
				bool flag45 = mainPlayerInfo.ContainsKey("clanid") && mainPlayerInfo["clanid"] > 0;
				flag2 = flag45;
			}
			bool flag46 = variant.ContainsKey("attchk");
			if (flag46)
			{
				foreach (Variant current in variant["attchk"]._arr)
				{
					Variant variant26 = new Variant();
					bool flag47 = current["name"]._str == "lotcnt";
					if (flag47)
					{
						Variant variant27 = new Variant();
						foreach (string current2 in current.Keys)
						{
							bool flag48 = current2 == "name";
							if (flag48)
							{
								variant27["name"] = "freelot";
							}
							else
							{
								variant27[current2] = current[current2];
							}
						}
						Variant variant28 = new Variant();
						variant28["c"] = variant27;
						variant26["attchk"] = variant28;
					}
					else
					{
						Variant variant29 = new Variant();
						variant29["attchk"] = current;
						variant26["attchk"] = variant29;
					}
				}
			}
			bool flag49 = variant.ContainsKey("enterlvl");
			if (flag49)
			{
				uint num10 = variant["enterlvl"];
				Variant variant30 = (this.g_mgr.g_gameConfM as muCLientConfig).svrLevelConf.get_level_data(num10);
				bool flag50 = !(this.g_mgr as muLGClient).g_levelsCT.has_enter_lvl((int)num10);
				if (flag50)
				{
					flag2 = false;
				}
			}
			bool flag51 = variant.ContainsKey("eqpchk");
			if (flag51)
			{
			}
			bool flag52 = variant.ContainsKey("finlvlmis");
			if (flag52)
			{
				flag2 = false;
				Variant lvlmis_data = (this.g_mgr as muLGClient).g_levelsCT.get_lvlmis_data();
				bool flag53 = lvlmis_data && lvlmis_data.ContainsKey("misline");
				if (flag53)
				{
					int num11 = (int)(this.g_mgr.g_gameConfM as muCLientConfig).svrLevelConf.GetlvlmisLine(variant["finlvlmis"]._uint);
					foreach (Variant current3 in lvlmis_data["misline"]._arr)
					{
						bool flag54 = current3["lineid"]._int == num11;
						if (flag54)
						{
							bool flag55 = current3["lmisid"]._int >= variant["finlvlmis"]._int;
							if (flag55)
							{
								flag2 = true;
								break;
							}
						}
					}
				}
			}
			bool flag56 = variant.ContainsKey("meri");
			if (flag56)
			{
				foreach (Variant current4 in variant["meri"]._arr)
				{
					Variant variant31 = (this.g_mgr as muLGClient).g_acupointCT.get_aucp_info(current4["id"]._int, current4["aid"]._int);
					bool flag57 = !current4.ContainsKey("alvl") || !variant31 || variant31["alvl"]._int >= current4["lvl"]._int;
					bool flag58 = variant31 == null || variant31["aid"]._int < current4["aid"]._int || !flag57;
					if (flag58)
					{
						flag2 = false;
						break;
					}
				}
			}
			acceptObj["isComplete"] = flag2;
			bool flag59 = flag & flag2;
			if (flag59)
			{
				this.uiMis.OpenMissionGuide(acceptObj["misid"]);
			}
		}

		private void addUpdateMis(Variant acceptMis)
		{
			bool flag = this._checks_arr == null;
			if (flag)
			{
				this._checks_arr = new Variant();
			}
			this._checks_arr.pushBack(acceptMis);
		}

		public void DelayCheckMis()
		{
			foreach (Variant current in this._checks_arr._arr)
			{
				this.updataAcceptMisState(current, false);
				this.mission_change(current["misid"]._int);
			}
			bool flag = this._checks_arr != null;
			if (flag)
			{
				this._checks_arr = new Variant();
			}
		}

		private void deletePlayerMiss(int misid)
		{
			Variant variant = this._playerMiss[misid.ToString()];
			bool flag = variant != null;
			if (flag)
			{
				this._playerMiss.RemoveKey(misid.ToString());
				this.clear_local_mis(misid);
				this.acceptable_reflesh_one_mission(variant["configdata"], false);
			}
		}

		public int get_auto_mis_have_count(int mid)
		{
			return this._no_line_data[mid.ToString()];
		}

		private void delete_follow_mis_cnt(int mis_id)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mis_id);
			bool flag = variant == null;
			if (!flag)
			{
				bool flag2 = !variant.ContainsKey("goal");
				if (!flag2)
				{
					Variant variant2 = this.get_mission_goal(variant, 0);
					bool flag3 = !variant2.ContainsKey("follow");
					if (!flag3)
					{
						bool flag4 = this.isdalyrep_mis(variant) && variant["dalyrep"]._int > 0;
						if (flag4)
						{
							bool flag5 = this._no_line_data.ContainsKey(mis_id);
							if (flag5)
							{
								Variant arg_A8_0 = this._no_line_data;
								string key = mis_id.ToString();
								Variant val = arg_A8_0[key];
								arg_A8_0[key] = val - 1;
							}
						}
					}
				}
			}
		}

		public void set_mis_leftcnt(int misid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = !this._no_line_data.ContainsKey(misid) && this.isdalyrep_mis(variant);
			if (flag)
			{
				bool flag2 = variant["dalyrep"]._int >= 1;
				if (flag2)
				{
					this._no_line_data[misid.ToString()] = variant["dalyrep"]._int;
				}
			}
		}

		private void init_local_miss()
		{
			bool flag = this._playerMiss.Count <= 0;
			if (!flag)
			{
				foreach (Variant current in this._playerMiss.Values)
				{
					bool flag2 = current == null;
					if (!flag2)
					{
						int mis_id = current["misid"];
						this.init_single_local_mis(mis_id, false);
					}
				}
			}
		}

		private void clear_local_mis(int mis_id)
		{
			bool flag = this._playerMiss == null || this._playerMiss.Count <= 0;
			if (!flag)
			{
				foreach (Variant current in this._playerMiss._arr)
				{
					bool flag2 = current["misid"] != mis_id;
					if (!flag2)
					{
						Variant variant = current["goal"];
						bool flag3 = variant.ContainsKey("qa");
						if (flag3)
						{
							Variant variant2 = variant["qa"];
							this._mis_qa_arr.RemoveKey(variant2["qamis"].ToString());
						}
						bool flag4 = variant.ContainsKey("uitm");
						if (flag4)
						{
							Variant variant3 = variant["uitm"];
							this._mis_uitm_arr.RemoveKey(variant3.ToString());
						}
						bool flag5 = variant.ContainsKey("clientgoal");
						if (flag5)
						{
						}
						bool flag6 = variant.ContainsKey("operate");
						if (flag6)
						{
							this._mis_operate_arr.RemoveKey(variant["operate"].ToString());
						}
						break;
					}
				}
			}
		}

		private void init_single_local_mis(int mis_id, bool newAccept = false)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mis_id);
			bool flag = variant == null;
			if (!flag)
			{
				Variant variant2 = this.get_mission_goal(variant, 0);
				bool flag2 = variant2.ContainsKey("qa");
				if (flag2)
				{
					Variant variant3 = new Variant();
					variant3["id"] = mis_id;
					variant3["complete"] = false;
					this._mis_qa_arr[variant2["qa"]["qamis"].ToString()] = variant3;
				}
				bool flag3 = variant2.ContainsKey("uitm");
				if (flag3)
				{
					Variant variant4 = new Variant();
					variant4["id"] = mis_id;
					variant4["complete"] = false;
				}
				bool flag4 = variant2.ContainsKey("clientgoal");
				if (flag4)
				{
					int num = variant2["clientgoal"];
					Variant cgoals = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCgoals(num);
					foreach (Variant current in cgoals._arr)
					{
						current["fincnt"] = 0;
					}
					Variant variant5 = new Variant();
					variant5["cgoals"] = cgoals;
					variant5["complete"] = false;
					this._mis_cgoal_arr[num] = variant5;
					foreach (Variant current2 in cgoals._arr)
					{
						bool flag5 = current2 != null && "rmis" == current2["type"]._str;
						if (flag5)
						{
							Variant dalyrepTypeRmis = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetDalyrepTypeRmis(current2["par"]._uint);
							foreach (string current3 in dalyrepTypeRmis.Keys)
							{
								int num2 = int.Parse(current3);
								Variant playerRmisInfo = (this.g_mgr as muLGClient).g_rmissCT.GetPlayerRmisInfo(num2);
								bool flag6 = playerRmisInfo != null;
								if (flag6)
								{
									Variant variant6 = new Variant();
									variant6["type"] = "rmis";
									variant6["par"] = current2["par"];
									variant6["fincnt"] = playerRmisInfo["fincnt"];
									variant6["rmisid"] = num2;
									this.reflushCgoalsData(variant6);
								}
							}
						}
					}
				}
				bool flag7 = newAccept && variant2.ContainsKey("operate");
				if (flag7)
				{
					Variant variant7 = new Variant();
					variant7["id"] = mis_id;
					variant7["complete"] = false;
					this._mis_operate_arr[variant2["operate"]] = variant7;
				}
			}
		}

		public Variant get_mis_uitm_arr()
		{
			return this._mis_uitm_arr;
		}

		public Variant get_mis_cgoal_arr()
		{
			return this._mis_cgoal_arr;
		}

		public void MisDataChange(string type, Variant data)
		{
			bool flag = data.ContainsKey("fincnt");
			int val;
			if (flag)
			{
				val = data["fincnt"];
			}
			else
			{
				val = 1;
			}
			if (type == "rmis")
			{
				int dalyrepRmisType = (int)(this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetDalyrepRmisType(data["id"]._uint);
				Variant variant = new Variant();
				variant["type"] = "rmis";
				variant["par"] = dalyrepRmisType;
				variant["fincnt"] = val;
				variant["rmisid"] = data["id"];
				this.reflushCgoalsData(variant);
			}
		}

		protected void reflushCgoalsData(Variant data)
		{
			bool flag = false;
			foreach (Variant current in this._mis_cgoal_arr.Values)
			{
				foreach (Variant current2 in current["cgoals"].Values)
				{
					bool flag2 = data["type"] == current2["type"];
					if (flag2)
					{
						bool flag3 = "rmis" == data["type"]._str && data["par"] == current2["par"];
						if (flag3)
						{
							current2["fincnt"]._int += data["fincnt"]._int;
							flag = true;
							break;
						}
					}
				}
				bool flag4 = flag;
				if (flag4)
				{
					bool flag5 = true;
					foreach (Variant current3 in current["cgoals"].Values)
					{
						bool flag6 = current3["fincnt"] < current3["cnt"];
						if (flag6)
						{
							flag5 = false;
							break;
						}
					}
					bool flag7 = flag5;
					if (flag7)
					{
						current["complete"] = true;
					}
					break;
				}
			}
			bool flag8 = flag;
			if (flag8)
			{
				base.dispatchEvent(GameEvent.Create(3304u, this, null, false));
				bool flag9 = "rmis" == data["type"];
				if (flag9)
				{
					this.player_rmis_change(data["rmisid"]._int);
				}
			}
		}

		public Variant get_mis_operate(string id)
		{
			return this._mis_operate_arr[id];
		}

		public Variant get_mis_qa(int id)
		{
			return this._mis_qa_arr[id.ToString()];
		}

		private bool isautocomitshow(int mid)
		{
			return this._no_line_data[mid.ToString()] == null || this._no_line_data[mid.ToString()]._int > 0;
		}

		private void line_acceptable_reflesh(int lineid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_by_line(lineid);
			bool flag = variant == null || variant.Count == 0;
			if (!flag)
			{
				foreach (Variant current in variant.Values)
				{
					this.acceptable_reflesh_one_mission(current, true);
				}
			}
		}

		private void acceptable_reflesh_one_mission(Variant misData, bool adjustState = false)
		{
			bool flag = this.is_accept_able_mis(misData);
			if (flag)
			{
				bool flag2 = !this._is_in_acceptable(misData["id"]);
				if (flag2)
				{
					this._acceptable.pushBack(misData);
				}
			}
		}

		private bool _is_in_acceptable(int misid)
		{
			bool flag = this._acceptable.Count == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (Variant current in this._acceptable._arr)
				{
					bool flag2 = current == null;
					if (!flag2)
					{
						bool flag3 = current["id"] == misid;
						if (flag3)
						{
							result = true;
							return result;
						}
					}
				}
				result = false;
			}
			return result;
		}

		public void acceptable_refault()
		{
			this._acceptable = new Variant();
			this.line_acceptable_reflesh(1);
			Variant missions = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_missions();
			foreach (Variant current in missions.Values)
			{
				bool flag = this.is_accept_able_mis(current);
				if (flag)
				{
					bool flag2 = !this._is_in_acceptable(current["id"]);
					if (flag2)
					{
						this._acceptable.pushBack(current);
					}
				}
			}
			GameTools.Sort(this._acceptable, "misline", 16u);
			base.dispatchEvent(GameEvent.Create(3304u, this, null, false));
		}

		private void delete_accept_mis(int misid)
		{
			for (int i = 0; i < this._acceptable.Count; i++)
			{
				Variant variant = this._acceptable[i];
				bool flag = misid == variant["id"];
				if (flag)
				{
					this._acceptable._arr.RemoveAt(i);
					break;
				}
			}
		}

		public void set_npcmis()
		{
			this.UpdateNpcsMisState((this.g_mgr as muLGClient).g_npcsCT.getNpcs());
		}

		public void UpdateNpcsMisState(Dictionary<string, LGAvatarNpc> npcs)
		{
			bool flag = npcs == null || npcs.Count <= 0;
			if (!flag)
			{
				foreach (LGAvatarNpc current in npcs.Values)
				{
					this.updateNpcMisStateImpl(current);
				}
			}
		}

		public void UpdateNpcMisState(int npcid)
		{
			LGAvatarNpc npc = (this.g_mgr as muLGClient).g_npcsCT.getNpc(npcid);
			bool flag = npc != null;
			if (flag)
			{
				this.updateNpcMisStateImpl(npc);
			}
		}

		private void updateNpcMisStateImpl(LGAvatarNpc npc)
		{
		}

		private void add_mis_top(int str, LGAvatarNpc mn)
		{
		}

		public int get_npc_mision_top_state(int npcid)
		{
			Variant variant = this.get_npc_misacept(npcid);
			bool flag = false;
			int result;
			for (int i = 0; i < variant.Count; i++)
			{
				Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(variant[i]._int);
				bool flag2 = variant2 == null;
				if (!flag2)
				{
					bool flag3 = this.is_mis_complete(variant2["id"]._int);
					if (flag3)
					{
						result = 3;
						return result;
					}
					flag = true;
				}
			}
			Variant variant3 = this.get_npc_acceptable_mis(npcid);
			bool flag4 = variant3.Count > 0;
			if (flag4)
			{
				result = 1;
				return result;
			}
			result = (flag ? 2 : 0);
			return result;
		}

		private void set_onenpc_topima(int misid, bool isover = false)
		{
			bool flag = misid == 0;
			if (flag)
			{
				this.set_npcmis();
			}
			else
			{
				this.refresh_mis_npc(misid);
			}
		}

		private void refresh_mis_npc(int misid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = variant == null;
			if (!flag)
			{
				this.UpdateNpcMisState(variant["awards"]["npc"]._int);
				this.UpdateNpcMisState(variant["accept"]["npc"]._int);
			}
		}

		public void PlayerInfoChanged()
		{
			this.acceptable_refault();
			this.set_npcmis();
		}

		public void use_item(int item_id)
		{
			foreach (Variant current in this._playerMiss.Values)
			{
				bool flag = current == null;
				if (!flag)
				{
					Variant variant = current["configdata"];
					Variant variant2 = current["goal"];
					bool flag2 = variant == null || variant2 == null;
					if (!flag2)
					{
						bool flag3 = variant2.ContainsKey("uitm") && variant2["uitm"]._int == item_id;
						if (flag3)
						{
							current["isComplete"] = true;
							this.auto_to_npc_open_mis(current["misid"]._int, variant["awards"]["npc"]._int);
							break;
						}
					}
				}
			}
		}

		public void player_camp_change()
		{
			int num = 0;
			foreach (Variant current in this._playerMiss.Values)
			{
				Variant variant = current["goal"];
				bool flag = variant == null;
				if (!flag)
				{
					bool flag2 = !variant.ContainsKey("jcamp");
					if (!flag2)
					{
						num = current["misid"];
						Variant variant2 = current["configdata"];
						this.updataAcceptMisState(current, false);
						bool flag3 = this.is_mis_complete(num);
						if (flag3)
						{
							this.auto_to_npc_open_mis(num, variant2["awards"]["npc"]._int);
							break;
						}
					}
				}
			}
			bool flag4 = num > 0;
			if (flag4)
			{
				this.mission_change(num);
			}
		}

		public void player_rmis_change(int rmisid)
		{
			int num = 0;
			bool flag = this._playerMiss.Count > 0;
			if (flag)
			{
				foreach (Variant current in this._playerMiss.Values)
				{
					this.updataAcceptMisState(current, false);
					Variant variant = current["goal"];
					bool flag2 = variant == null;
					if (!flag2)
					{
						num = current["misid"];
						Variant variant2 = current["configdata"];
						bool flag3 = variant.ContainsKey("clientgoal");
						if (flag3)
						{
							bool flag4 = this.is_mis_complete(num);
							if (flag4)
							{
								this.auto_to_npc_open_mis(num, variant2["awards"]["npc"]._int);
								break;
							}
						}
					}
				}
			}
			bool flag5 = num > 0;
			if (flag5)
			{
				this.mission_change(num);
			}
		}

		public void player_change()
		{
			int num = 0;
			foreach (Variant current in this._playerMiss.Values)
			{
				Variant variant = current["goal"];
				bool flag = variant == null;
				if (!flag)
				{
					bool flag2 = !variant.ContainsKey("attchk");
					if (!flag2)
					{
						num = current["misid"];
						Variant variant2 = current["configdata"];
						this.updataAcceptMisState(current, false);
						bool flag3 = this.is_mis_complete(num);
						if (flag3)
						{
							this.auto_to_npc_open_mis(num, variant2["awards"]["npc"]._int);
							break;
						}
					}
				}
			}
			bool flag4 = num > 0;
			if (flag4)
			{
				this.mission_change(num);
			}
		}

		public void PlayerLevelChange()
		{
			foreach (Variant current in this._playerMiss.Values)
			{
				Variant variant = current["goal"];
				bool flag = variant == null;
				if (!flag)
				{
					bool flag2 = !variant.ContainsKey("attchk");
					if (!flag2)
					{
						Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
						foreach (Variant current2 in variant["attchk"]._arr)
						{
							bool flag3 = current2["name"] == "level";
							if (flag3)
							{
								current["isComplete"] = (mainPlayerInfo["level"]._int >= current2["min"]._int);
								int misid = current["misid"];
								this.refresh_mis_npc(misid);
								break;
							}
						}
					}
				}
			}
		}

		public void equip_change(Variant data)
		{
			foreach (Variant current in this._playerMiss.Values)
			{
				Variant variant = current["goal"];
				bool flag = current == null || variant == null;
				if (!flag)
				{
					bool flag2 = variant.ContainsKey("eqpchk");
					if (flag2)
					{
						foreach (string current2 in variant["eqpchk"].Keys)
						{
							Variant variant2 = variant["eqpchk"][current2];
							bool flag3 = variant2["name"]._str == data["tp"]._str;
							if (flag3)
							{
								int num = current["misid"];
								Variant variant3 = current["configdata"];
								this.updataAcceptMisState(current, false);
								this.mission_change(num);
								bool flag4 = this.is_mis_complete(num);
								if (flag4)
								{
									this.auto_to_npc_open_mis(num, variant3["awards"]["npc"]._int);
								}
								break;
							}
						}
					}
				}
			}
		}

		public void RefreshItems(Variant items)
		{
			foreach (Variant current in items._arr)
			{
				this.item_add(current["tpid"]._int);
			}
		}

		public void item_add(int item_id)
		{
			Variant variant = null;
			int num = 0;
			foreach (Variant current in this._playerMiss.Values)
			{
				Variant variant2 = current["configdata"];
				Variant variant3 = current["goal"];
				bool flag = variant2 == null || variant3 == null;
				if (!flag)
				{
					bool flag2 = variant3.ContainsKey("ownitm");
					if (flag2)
					{
						variant = variant3["ownitm"];
					}
					else
					{
						bool flag3 = variant3.ContainsKey("colitm");
						if (flag3)
						{
							variant = variant3["colitm"];
						}
						else
						{
							bool flag4 = variant3.ContainsKey("kilmonitm");
							if (flag4)
							{
								variant = variant3["kilmonitm"];
							}
						}
					}
					bool flag5 = variant == null || current["misid"]._int != variant2["id"]._int;
					if (!flag5)
					{
						bool flag6 = false;
						foreach (string current2 in variant.Keys)
						{
							bool flag7 = variant[current2]["tpid"]._int == item_id;
							if (flag7)
							{
								flag6 = true;
								break;
							}
						}
						bool flag8 = flag6;
						if (flag8)
						{
							num = current["misid"];
							this.updataAcceptMisState(current, false);
							break;
						}
					}
				}
			}
			bool flag9 = num == 0;
			if (!flag9)
			{
				this.mission_change(num);
				bool flag10 = this.is_mis_complete(num);
				if (flag10)
				{
					Variant variant4 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(num);
					this.auto_to_npc_open_mis(num, variant4["awards"]["npc"]._int);
				}
			}
		}

		public void clan_info_change()
		{
			foreach (Variant current in this._playerMiss.Values)
			{
				Variant variant = current["goal"];
				bool flag = variant == null;
				if (!flag)
				{
					bool flag2 = !variant.ContainsKey("joinclan");
					if (!flag2)
					{
						int misid = current["misid"];
						this.updataAcceptMisState(current, false);
						this.mission_change(misid);
					}
				}
			}
		}

		public void lvlMisChange()
		{
			bool flag = this._playerMiss.Count > 0;
			if (flag)
			{
				foreach (Variant current in this._playerMiss.Values)
				{
					Variant variant = current["goal"];
					bool flag2 = variant == null;
					if (!flag2)
					{
						bool flag3 = !variant.ContainsKey("finlvlmis");
						if (!flag3)
						{
							Variant variant2 = current["configdata"];
							int num = current["misid"];
							this.updataAcceptMisState(current, false);
							this.mission_change(num);
							bool flag4 = this.is_mis_complete(num);
							if (flag4)
							{
								this.auto_to_npc_open_mis(num, variant2["awards"]["npc"]._int);
								break;
							}
						}
					}
				}
			}
		}

		public void mission_change(int misid)
		{
			bool flag = misid > 0;
			if (flag)
			{
				Variant variant = this._playerMiss[misid.ToString()];
				this.refresh_mis_npc(misid);
			}
			else
			{
				this.set_npcmis();
			}
			this.check_follow();
			Variant variant2 = new Variant();
			variant2["misid"] = misid;
			base.dispatchEvent(GameEvent.Create(3304u, this, variant2, false));
		}

		public void player_meri_change()
		{
			int num = 0;
			foreach (Variant current in this._playerMiss.Values)
			{
				Variant variant = current["goal"];
				bool flag = variant == null;
				if (!flag)
				{
					bool flag2 = !variant.ContainsKey("meri");
					if (!flag2)
					{
						Variant variant2 = current["configdata"];
						num = current["misid"];
						this.updataAcceptMisState(current, false);
						bool flag3 = this.is_mis_complete(num);
						if (flag3)
						{
							this.auto_to_npc_open_mis(num, variant2["awards"]["npc"]._int);
							break;
						}
					}
				}
			}
			bool flag4 = num > 0;
			if (flag4)
			{
				this.mission_change(num);
			}
		}

		public int getKillmonCnt(int misid)
		{
			int num = 0;
			Variant variant = null;
			Variant variant2 = null;
			bool flag = this._playerMiss[misid.ToString()] != null;
			if (flag)
			{
				Variant variant3 = this._playerMiss[misid.ToString()]["goal"];
				variant = this._playerMiss[misid.ToString()]["configdata"];
				variant2 = this._playerMiss[misid.ToString()]["data"];
				bool flag2 = variant3.ContainsKey("kilmon_map");
				if (flag2)
				{
					num = variant3["kilmon_map"][0]["cnt"];
				}
				else
				{
					bool flag3 = variant3.ContainsKey("kilmon");
					if (flag3)
					{
						num = variant3["kilmon"][0]["cnt"];
					}
				}
			}
			bool flag4 = variant != null && variant.ContainsKey("goaladdition_daly");
			if (flag4)
			{
				int num2 = variant["dalyrep"];
				Variant variant4 = variant["goaladdition_daly"][0];
				float num3 = variant4["coefficient_a"];
				float num4 = variant4["coefficient_b"];
				float num5 = variant4["coefficient_c"];
				float num6 = variant4["fix"];
				int num7 = num2 - variant2["cntleft"];
				num = (int)(num3 * (float)num7 * (float)num * (float)num + num4 * (float)num + num5);
				bool flag5 = (float)num > num6;
				if (flag5)
				{
					num -= (int)((float)num % num6);
				}
			}
			return num;
		}

		public void set_enter_level_pos(Vec2 p)
		{
			this._enter_level_pos = p;
		}

		public void enter_level_call(int level_id, int tp, int misid = 0)
		{
			this._enter_level_tp = tp;
			bool flag = level_id > 0;
			if (flag)
			{
				this._enter_level_id = level_id;
				bool in_level = (this.g_mgr as muLGClient).g_levelsCT.in_level;
				if (!in_level)
				{
					Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrLevelConf.get_level_data((uint)this._enter_level_id);
					int levelEntryNpc = (this.g_mgr.g_gameConfM as muCLientConfig).svrNpcConf.getLevelEntryNpc(this._enter_level_id);
					Variant variant2 = new Variant();
					variant2.pushBack(levelEntryNpc);
					bool flag2 = variant2 == null || variant2.Count <= 0;
					if (!flag2)
					{
						int num = -1;
						for (int i = 0; i < variant2.Count; i++)
						{
							int num2 = variant2[i];
							int num3 = (int)(this.g_mgr.g_gameConfM as muCLientConfig).svrMapsConf.get_npc_map_id((uint)num2);
							bool flag3 = (long)num3 == (long)((ulong)(this.g_mgr as muLGClient).g_mapCT.curMapId);
							if (flag3)
							{
								num = num2;
								break;
							}
						}
						bool flag4 = num < 0;
						if (flag4)
						{
							for (int i = 0; i < variant2.Count; i++)
							{
								int num2 = variant2[i];
								int num3 = (int)(this.g_mgr.g_gameConfM as muCLientConfig).svrMapsConf.get_npc_map_id((uint)num2);
								bool flag5 = num3 > 0;
								if (flag5)
								{
									num = num2;
									break;
								}
							}
						}
						bool flag6 = num < 0;
						if (!flag6)
						{
							bool flag7 = 4 == this._enter_level_tp;
							if (flag7)
							{
							}
						}
					}
				}
			}
			else
			{
				this.process_enter_level_end();
			}
		}

		private void process_enter_level_end()
		{
			bool flag = this._enter_level_tp == 1;
			if (flag)
			{
				this._move_to_monster();
			}
			else
			{
				bool flag2 = this._enter_level_tp == 4;
				if (flag2)
				{
					bool flag3 = this._enter_level_pos != null;
					if (flag3)
					{
					}
				}
			}
			this._enter_level_tp = 0;
		}

		private void _move_to_monster()
		{
		}

		public void enter_level(int lvl_id)
		{
			bool flag = this._enter_level_tp == 4;
			if (flag)
			{
				bool in_level = (this.g_mgr as muLGClient).g_levelsCT.in_level;
				if (in_level)
				{
					bool flag2 = this._enter_level_id != (this.g_mgr as muLGClient).g_levelsCT.current_lvl;
					if (!flag2)
					{
						Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrLevelConf.get_level_data((uint)this._enter_level_id);
						bool flag3 = variant["map"][0]["id"]._uint != (this.g_mgr as muLGClient).g_mapCT.curMapId;
						if (!flag3)
						{
							this.process_enter_level_end();
							this._enter_level_id = 0;
						}
					}
				}
			}
		}

		public void on_change_map()
		{
			bool flag = this._enter_level_tp == 4;
			if (flag)
			{
				bool in_level = (this.g_mgr as muLGClient).g_levelsCT.in_level;
				if (in_level)
				{
					bool flag2 = this._enter_level_id != (this.g_mgr as muLGClient).g_levelsCT.current_lvl;
					if (!flag2)
					{
						Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrLevelConf.get_level_data((uint)this._enter_level_id);
						bool flag3 = variant["map"][0]["id"]._uint != (this.g_mgr as muLGClient).g_mapCT.curMapId;
						if (!flag3)
						{
							this.process_enter_level_end();
							this._enter_level_id = 0;
						}
					}
				}
			}
		}

		public void on_item_use(int item_id)
		{
			bool flag = !this._mis_uitm_arr.ContainsKey(item_id.ToString()) || this._mis_uitm_arr[item_id.ToString()]["complete"];
			if (!flag)
			{
				this._mis_uitm_arr[item_id.ToString()]["complete"] = true;
				int misid = this._mis_uitm_arr[item_id.ToString()]["id"];
				Variant variant = this._playerMiss[misid.ToString()];
				bool flag2 = variant != null;
				if (flag2)
				{
					variant["isComplete"] = true;
				}
				this.mission_change(misid);
			}
		}

		public void OnOperateComplete(string type)
		{
			Variant variant = this._mis_operate_arr[type];
			bool flag = variant != null;
			if (flag)
			{
				variant["complete"] = true;
				int num = variant["id"];
				Variant variant2 = this._playerMiss[num.ToString()];
				bool flag2 = variant2 != null;
				if (flag2)
				{
					variant2["isComplete"] = true;
				}
				this.mission_change(num);
				bool flag3 = type == "getNpcBuff";
				if (flag3)
				{
					bool flag4 = this.is_mis_complete(num);
					if (flag4)
					{
						bool flag5 = this.IsMisCanAutocommit(num);
						if (flag5)
						{
							Variant variant3 = new Variant();
							variant3["misid"] = num;
							this.commit_mis(variant3);
						}
						else
						{
							this.to_npc_open_mis(num, variant2["configdata"]["awards"]["npc"]._int, false);
						}
					}
					else
					{
						bool flag6 = variant2 != null;
						if (flag6)
						{
							this.to_npc_open_mis(num, variant2["goal"]["talknpc"]._int, false);
						}
					}
				}
			}
		}

		public void on_qa_answer(int qa_id)
		{
			bool flag = !this._mis_qa_arr.ContainsKey(qa_id.ToString()) || this._mis_qa_arr[qa_id.ToString()]["complete"];
			if (!flag)
			{
				this._mis_qa_arr[qa_id.ToString()]["complete"] = true;
				int misid = this._mis_qa_arr[qa_id.ToString()]["id"];
				Variant variant = this._playerMiss[misid.ToString()];
				bool flag2 = variant != null;
				if (flag2)
				{
					variant["isComplete"] = true;
				}
				this.mission_change(misid);
			}
		}

		public void accpet_move(int mid, bool ignore_level = false)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mid);
			bool flag = variant == null;
			if (!flag)
			{
				bool flag2 = variant != null && variant.ContainsKey("goal");
				if (flag2)
				{
					Variant variant2 = this.get_mission_goal(variant, 0);
					bool flag3 = variant2.ContainsKey("kilmon");
					if (flag3)
					{
						Variant variant3 = variant2["kilmon"][0];
						bool flag4 = variant3 != null;
						if (flag4)
						{
							string text = "mon_" + variant3["monid"]._str + "_";
							bool flag5 = variant3.ContainsKey("pos");
							if (flag5)
							{
								Variant variant4 = variant3["pos"][0];
								text = string.Concat(new string[]
								{
									text,
									variant4["mpid"],
									"_",
									variant4["x"],
									"_",
									variant4["y"]
								});
							}
							else
							{
								text += "0_0_0";
							}
							int num = variant3["level_id"];
							bool flag6 = num != 0;
							if (flag6)
							{
							}
							text = string.Concat(new object[]
							{
								text,
								"_",
								num,
								"_",
								mid
							});
							this.uiMis.accept_move_execute_link_event(text);
						}
					}
					bool flag7 = variant2.ContainsKey("ownitm");
					if (flag7)
					{
						Variant variant3 = variant2["ownitm"][0];
						bool flag8 = variant3 != null;
						if (flag8)
						{
							string str = this.get_need_item_str(variant3, variant["id"]._int);
							this.uiMis.accept_move_execute_link_event(str);
						}
					}
					bool flag9 = variant2.ContainsKey("colitm");
					if (flag9)
					{
						Variant variant3 = variant2["colitm"][0];
						bool flag10 = variant3 != null;
						if (flag10)
						{
							string str2 = this.get_need_item_str(variant3, variant["id"]._int);
							this.uiMis.accept_move_execute_link_event(str2);
						}
					}
					bool flag11 = variant2.ContainsKey("enterlvl");
					if (flag11)
					{
						string str3 = "enterlvl_" + variant2["enterlvl"]._str;
						this.uiMis.accept_move_execute_link_event(str3);
					}
					bool flag12 = variant2.ContainsKey("jcamp");
					if (flag12)
					{
						this.uiMis.accept_move_execute_link_event("openui_jcamp");
					}
					bool flag13 = variant2.ContainsKey("qa");
					if (flag13)
					{
						this.to_npc_open_mis(mid, variant2["qa"]["npc"]._int, false);
					}
					else
					{
						bool flag14 = variant2.ContainsKey("talknpc");
						if (flag14)
						{
							bool flag15 = this.is_mis_complete(mid);
							if (flag15)
							{
								this.uiMis.to_find_npc(mid, variant2["talknpc"]._int);
							}
						}
					}
				}
			}
		}

		private void open_findway(Variant fobj)
		{
		}

		public void close_findway()
		{
			this._findwayobj["isfind"] = false;
			this._findwayobj["sttm"] = GameTools.getTimer();
			this._findwayobj["mid"] = 0;
			this._findwayobj["npcid"] = 0;
		}

		private void findway_process()
		{
			bool flag = !this._findwayobj["isfind"]._bool;
			if (!flag)
			{
				long timer = GameTools.getTimer();
				bool flag2 = (double)timer - this._findwayobj["sttm"]._double >= this._findwayobj["stoptm"]._double;
				if (flag2)
				{
					this.close_findway();
				}
			}
		}

		public void auto_to_npc_open_mis(int mid, int npc_id)
		{
			Variant variant = new Variant();
			variant["mid"] = mid;
			variant["npcid"] = npc_id;
			this.open_findway(variant);
		}

		public void to_npc_open_mis(int mid, int npc_id, bool ignore_level = false)
		{
		}

		public void process(float tm)
		{
			this.findway_process();
		}

		public Variant get_only_accept()
		{
			int num = 0;
			Variant variant = null;
			Variant result;
			foreach (Variant current in this._playerMiss.Values)
			{
				bool flag = current != null;
				if (flag)
				{
					num++;
					bool flag2 = num > 1;
					if (flag2)
					{
						result = null;
						return result;
					}
					variant = current;
				}
			}
			result = variant;
			return result;
		}

		public void monster_event(Variant re)
		{
			int val = re[1];
			int killmon_map_id = re[2];
			int num = re[3];
			int num2 = re[4];
			int num3 = re[5];
			int misid = re[6];
			bool in_level = (this.g_mgr as muLGClient).g_levelsCT.in_level;
			if (in_level)
			{
				bool flag = (this.g_mgr as muLGClient).g_levelsCT.current_lvl == num3;
				if (flag)
				{
					this._killmon_map_id = killmon_map_id;
					this._killmon_pos = new Vec2((float)num, (float)num2);
					this._killmon_mons.pushBack(val);
					this._move_to_monster();
					return;
				}
			}
			this._killmon_map_id = killmon_map_id;
			this._killmon_pos = new Vec2((float)num, (float)num2);
			this._killmon_mons.pushBack(val);
			this.enter_level_call(num3, 1, misid);
		}

		public void npc_event(Variant re)
		{
			int num = re[1];
			int num2 = re[2];
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrNpcConf.get_npc_data(num);
			bool flag = variant == null;
			if (!flag)
			{
				int num3 = 0;
				bool flag2 = variant.ContainsKey("level");
				if (flag2)
				{
					num3 = variant["level"][0]["id"];
				}
				bool in_level = (this.g_mgr as muLGClient).g_levelsCT.in_level;
				if (in_level)
				{
					bool flag3 = variant.ContainsKey("level");
					if (!flag3)
					{
						bool flag4 = !variant.ContainsKey("level");
						if (flag4)
						{
							bool flag5 = !this.is_accepted_mis(num2) || this.is_mis_complete(num2);
							if (flag5)
							{
								this.to_npc_open_mis(num2, num, true);
							}
							else
							{
								this.accpet_move(num2, true);
							}
						}
						return;
					}
					Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrLevelConf.get_level_data((uint)num3);
					bool flag6 = (this.g_mgr as muLGClient).g_levelsCT.current_lvl == num3;
					if (flag6)
					{
						bool flag7 = !this.is_accepted_mis(num2) || this.is_mis_complete(num2);
						if (flag7)
						{
							this.to_npc_open_mis(num2, num, true);
						}
						else
						{
							this.accpet_move(num2, false);
						}
						return;
					}
				}
				bool flag8 = variant.ContainsKey("level");
				if (flag8)
				{
					this.enter_level_call(num3, 1, num2);
				}
				else
				{
					bool flag9 = !this.is_accepted_mis(num2) || this.is_mis_complete(num2);
					if (flag9)
					{
						this.to_npc_open_mis(num2, num, false);
					}
					else
					{
						this.to_npc_open_mis(num2, num, false);
					}
				}
			}
		}

		private string get_type_by_id(int typeid)
		{
			return LanguagePack.getLanguageText("mission_manager", "mis_type_" + typeid.ToString());
		}

		public string get_need_item_str(Variant uitem, int mid)
		{
			string text = "";
			bool flag = uitem.ContainsKey("open_ui");
			if (flag)
			{
				string str = uitem["open_ui"];
				text = "openui_" + str;
			}
			else
			{
				bool flag2 = uitem.ContainsKey("collect");
				if (flag2)
				{
					Variant variant = uitem["collect"][0];
					int num = variant["mpid"];
					int num2 = variant["areaid"];
				}
				else
				{
					bool flag3 = uitem.ContainsKey("pos");
					if (flag3)
					{
						Variant conf = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mid);
						Variant variant2 = this.get_mission_goal(conf, 0);
						uint num3 = this.get_mon_id_by_killmonitm(variant2["kilmonitm"], uitem["tpid"]._int);
						int num4 = 0;
						bool flag4 = uitem.ContainsKey("level_id");
						if (flag4)
						{
							num4 = uitem["level_id"];
						}
						text = string.Concat(new string[]
						{
							"mon_",
							num3.ToString(),
							"_",
							uitem["pos"][0]["mpid"]._str,
							"_",
							uitem["pos"][0]["x"],
							"_",
							uitem["pos"][0]["y"],
							"_",
							num4.ToString(),
							"_",
							mid.ToString()
						});
					}
					else
					{
						bool flag5 = uitem.ContainsKey("npcid");
						if (flag5)
						{
							text = string.Concat(new object[]
							{
								"buyitem_",
								uitem["npcid"]._str,
								"_",
								mid,
								"_",
								uitem["tpid"]._str
							});
						}
						else
						{
							bool flag6 = uitem.ContainsKey("tpid");
							if (flag6)
							{
								bool flag7 = uitem.ContainsKey("ttpid");
								int tpid;
								if (flag7)
								{
									tpid = uitem["ttpid"];
								}
								else
								{
									tpid = uitem["tpid"];
								}
								Variant variant3 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMarketConf.get_game_market_sell_data_by_tpid(tpid);
								bool flag8 = variant3 != null;
								if (flag8)
								{
									text = "shop_" + tpid.ToString();
								}
							}
						}
					}
				}
			}
			bool flag9 = uitem.ContainsKey("level_id");
			if (flag9)
			{
				text = text + "_" + uitem["level_id"]._str;
			}
			return text;
		}

		public bool is_accepted_mis(int mis_id)
		{
			return this._playerMiss[mis_id.ToString()] != null;
		}

		public bool isPlayerAcceptedRmis(uint tp = 1u)
		{
			bool result;
			foreach (Variant current in this._playerMiss.Values)
			{
				Variant variant = current["configdata"];
				bool flag = variant != null && variant.ContainsKey("rmis");
				if (flag)
				{
					Variant rmisDesc = (this.g_mgr as muLGClient).g_rmissCT.GetRmisDesc(variant["rmis"]._int);
					bool flag2 = rmisDesc != null && rmisDesc["type"]._uint == tp;
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

		public bool isdalyrep_mis(Variant mis_data)
		{
			bool flag = mis_data == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = mis_data.ContainsKey("dalyrep") && mis_data["dalyrep"]._int > 0;
				result = flag2;
			}
			return result;
		}

		public bool is_accept_able_mis(Variant mis_data)
		{
			bool flag = mis_data == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int num = mis_data["id"];
				bool flag2 = this.is_accepted_mis(num);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = mis_data.ContainsKey("misline") && (!mis_data.ContainsKey("dalyrep") || mis_data["dalyrep"]._int <= 0 || mis_data["misline"]._int == 1);
					if (flag3)
					{
						bool flag4 = !mis_data.ContainsKey("rmis");
						if (flag4)
						{
							bool flag5 = this._line_data.ContainsKey(mis_data["misline"]._str);
							if (flag5)
							{
								bool flag6 = this._line_data[mis_data["misline"]._str]._int >= num;
								if (flag6)
								{
									result = false;
									return result;
								}
							}
						}
					}
					Variant variant = mis_data["accept"];
					bool flag7 = variant.ContainsKey("unaccept_able") && variant["unaccept_able"];
					if (flag7)
					{
						result = false;
					}
					else
					{
						int num2 = 0;
						bool flag8 = variant.ContainsKey("premis") && variant["premis"]._str != "";
						if (flag8)
						{
							num2 = variant["premis"]._int;
						}
						Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(num2);
						bool flag9 = variant2 != null && !this._line_data.ContainsKey(variant2["misline"]._str);
						if (flag9)
						{
							result = false;
						}
						else
						{
							bool flag10 = variant2 != null && this._line_data[variant2["misline"]._str] < num2;
							if (flag10)
							{
								result = false;
							}
							else
							{
								bool flag11 = mis_data["misline"] == 1;
								if (flag11)
								{
									bool flag12 = num2 == 0;
									if (flag12)
									{
										bool flag13 = variant.ContainsKey("show_premis");
										if (flag13)
										{
											int num3 = variant["show_premis"];
											Variant variant3 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(num3);
											bool flag14 = variant3 != null && this._line_data[variant3["misline"]._str] >= num3;
											if (flag14)
											{
												result = true;
												return result;
											}
										}
										result = false;
									}
									else
									{
										bool flag15 = variant.ContainsKey("attchk");
										if (flag15)
										{
											Variant variant4 = variant["attchk"];
											for (int i = 0; i < variant4.Count; i++)
											{
												Variant variant5 = variant4[i];
												string a = variant5["name"];
												bool flag16 = a == "carr";
												if (flag16)
												{
												}
											}
										}
										result = true;
									}
								}
								else
								{
									Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
									bool flag17 = !ConfigUtil.attchk(variant["attchk"], mainPlayerInfo);
									if (flag17)
									{
										result = false;
									}
									else
									{
										Variant variant6 = variant["clan"];
										bool flag18 = variant6 != null;
										if (flag18)
										{
										}
										bool flag19 = this._no_line_data.ContainsKey(num.ToString());
										if (flag19)
										{
											bool flag20 = !mis_data.ContainsKey("rmis");
											if (flag20)
											{
												int num4 = this._no_line_data[num.ToString()];
												bool flag21 = num4 <= 0;
												if (flag21)
												{
													result = false;
													return result;
												}
											}
										}
										bool flag22 = mis_data.ContainsKey("tmchk");
										if (flag22)
										{
											bool flag23 = !this.is_in_open_tm(num);
											if (flag23)
											{
												result = false;
												return result;
											}
										}
										Variant variant7 = this.get_mission_goal(mis_data, 0);
										bool flag24 = variant7.ContainsKey("microclient");
										if (flag24)
										{
										}
										bool flag25 = this.isdalyrep_mis(mis_data);
										if (flag25)
										{
											bool flag26 = !this._no_line_data.ContainsKey(num.ToString());
											if (flag26)
											{
												result = true;
												return result;
											}
											int num5 = this._no_line_data[num.ToString()];
											bool flag27 = num5 <= 0;
											if (flag27)
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
					}
				}
			}
			return result;
		}

		public bool is_in_open_tm(int misid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = !variant.ContainsKey("tmchk");
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = !variant["tmchk"].ContainsKey("tb");
				if (flag2)
				{
					result = true;
				}
				else
				{
					long curServerTimeStampMS = (this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS;
					result = ConfigUtil.check_tm((double)curServerTimeStampMS, variant["tmchk"], 0.0, 0.0);
				}
			}
			return result;
		}

		public bool is_mis_complete(int misid)
		{
			Variant variant = this._playerMiss[misid.ToString()];
			return variant != null && variant["isComplete"]._bool;
		}

		public bool is_mis_has_complete(int misid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = variant == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int num = variant["misline"];
				bool flag2 = num <= 0;
				if (flag2)
				{
					result = this._no_line_data.ContainsKey(misid.ToString());
				}
				else
				{
					bool flag3 = variant.ContainsKey("rmis");
					if (flag3)
					{
						result = false;
					}
					else
					{
						Variant variant2 = this._playerMiss[misid.ToString()];
						bool flag4 = variant.ContainsKey("dalyrep") && variant["dalyrep"] > 0;
						if (flag4)
						{
							Variant variant3 = variant2["data"];
							bool flag5 = variant2 != null && variant3.ContainsKey("cntleft") && variant3["cntleft"] >= 0;
							if (flag5)
							{
								result = false;
								return result;
							}
							foreach (Variant current in this._acceptable._arr)
							{
								bool flag6 = current == null;
								if (!flag6)
								{
									bool flag7 = current["id"] == misid;
									if (flag7)
									{
										result = false;
										return result;
									}
								}
							}
						}
						result = (this._line_data.ContainsKey(num.ToString()) && this._line_data[num.ToString()] >= misid);
					}
				}
			}
			return result;
		}

		public bool IsMisCanAutocommit(int misid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = variant == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant variant2 = null;
				bool flag2 = variant2 != null;
				if (flag2)
				{
					bool flag3 = !variant2.ContainsKey("misid") || (variant2["misid"] > 0 && variant2["misid"] < misid);
					if (flag3)
					{
						result = false;
						return result;
					}
				}
				bool flag4 = variant.ContainsKey("goaladdition_daly");
				if (flag4)
				{
					result = false;
				}
				else
				{
					Variant variant3 = variant["awards"];
					bool flag5 = !variant3.ContainsKey("npc") || variant3["npc"] <= 0;
					result = (flag5 && !variant.ContainsKey("rmis"));
				}
			}
			return result;
		}

		public bool is_in_no_line_data(Variant mis)
		{
			return this._no_line_data.ContainsKey(mis["id"]._str);
		}

		public bool is_main_mis(int misid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_by_line(1);
			bool flag = variant != null && variant.Count > 0;
			return flag && variant[misid] != null;
		}

		public bool is_mis_goal(int misid)
		{
			Variant variant = this._playerMiss[misid.ToString()];
			bool flag = variant == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant variant2 = variant["configdata"];
				Variant variant3 = variant["goal"];
				Variant variant4 = variant3["kilmon"];
				Variant variant5 = variant["data"];
				bool flag2 = variant5 != null && variant5.ContainsKey("km");
				if (flag2)
				{
					foreach (Variant current in variant5["km"]._arr)
					{
						for (int i = 0; i < variant4.Count; i++)
						{
							Variant variant6 = variant4[i];
							bool flag3 = current["monid"] == variant6["monid"];
							if (flag3)
							{
								bool flag4 = current["cnt"] < variant6["cnt"];
								if (flag4)
								{
									result = false;
									return result;
								}
							}
						}
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public bool isnewPlayermis(int misid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			return variant != null && variant.ContainsKey("spcmis") && variant["spcmis"] == 1;
		}

		public bool is_cant_abord_mis(int misid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			return variant != null && variant.ContainsKey("cant_abord") && variant["cant_abord"] == 1;
		}

		public void read_current_map_mis_line(uint mapid)
		{
			bool flag = this._current_map_id > 0u;
			if (!flag)
			{
				this._current_map_id = mapid;
				Variant variant = new Variant();
				Variant variant2 = new Variant();
				Variant variant3 = new Variant();
				Variant missions = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_missions();
				Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
				foreach (Variant current in missions.Values)
				{
					Variant variant4 = current["accept"];
					bool flag2 = variant4.ContainsKey("attchk");
					if (flag2)
					{
						Variant attchk = variant4["attchk"];
						bool flag3 = !ConfigUtil.attchk(attchk, mainPlayerInfo);
						if (flag3)
						{
							continue;
						}
					}
					this.add_mis_info(variant2, variant, current);
					this.add_daymis_info(variant3, current);
				}
				bool flag4 = variant.Count > 0;
				if (flag4)
				{
					(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetMisLineState(variant);
				}
				bool flag5 = variant2.Count > 0;
				if (flag5)
				{
					(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetFinedMisState(variant2);
				}
				bool flag6 = variant3.Count > 0;
				if (flag6)
				{
					(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetFinedMisState(variant3);
				}
			}
		}

		public void read_autocomit_mis()
		{
			Variant autocomit_mis = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_autocomit_mis();
			Variant variant = null;
			foreach (string current in autocomit_mis.Keys)
			{
				Variant variant2 = autocomit_mis[current];
				variant = new Variant();
				foreach (string current2 in variant2.Keys)
				{
					Variant variant3 = variant2[current2]["data"];
					variant.pushBack(variant3["id"]);
					this._no_line_data[variant3["id"]._str] = variant3["dalyrep"];
				}
				(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetFinedMisState(variant);
			}
		}

		public void AutocomitMis(int misid, bool double_awd = false)
		{
			(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.AutoComitMis(misid, double_awd);
		}

		public void AutodoublecomitMis(int misid)
		{
			this.AutocomitMis(misid, true);
		}

		public void AbordMis(int misid)
		{
			(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.AbordMis(misid);
		}

		private void get_mis_line_state_res(Variant data)
		{
			Variant variant = data["misline"];
			for (int i = 0; i < variant.Count; i++)
			{
				Variant variant2 = variant[i];
				Variant variant3 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(variant2["curmis"]._int);
				bool flag = variant3 == null;
				if (flag)
				{
					int lastComMis = this.getLastComMis(variant2);
					this._line_data[variant2["lineid"]._int.ToString()] = lastComMis;
				}
				else
				{
					this._line_data[variant2["lineid"]._int.ToString()] = variant2["curmis"];
				}
			}
			this._can_show_cache_warning();
			this.reflush_option_limit();
			this.acceptable_refault();
			this.set_npcmis();
		}

		private int getLastComMis(Variant line)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_by_line(line["lineid"]._int);
			int num = 0;
			bool flag = variant == null;
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				foreach (Variant current in variant.Values)
				{
					bool flag2 = current["id"] < line["curmis"];
					if (flag2)
					{
						bool flag3 = num < current["id"];
						if (flag3)
						{
							num = current["id"];
						}
					}
				}
				result = num;
			}
			return result;
		}

		public void accept_mis_res(Variant data)
		{
			int num = data["misid"];
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(num);
			this.init_single_local_mis(num, true);
			bool flag = this.addPlayerMiss(data);
			if (flag)
			{
				this.delete_accept_mis(num);
			}
			this.mission_change(num);
			string misGoalDesc = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.getMisGoalDesc(num);
			bool flag2 = num.ToString() != misGoalDesc;
			if (flag2)
			{
				Variant variant2 = new Variant();
				variant2.pushBack(misGoalDesc);
				this.lguiMain.systemmsg(variant2, 16u);
			}
			int num2 = this.get_mis_type(num);
			bool flag3 = 4 == num2 || 5 == num2;
			if (flag3)
			{
				string text = LanguagePack.getLanguageText("missionMsg", "accept");
				string languageText = LanguagePack.getLanguageText("misName", num.ToString());
				text = DebugTrace.Printf(text, new string[]
				{
					languageText
				});
				Variant variant3 = new Variant();
				variant3.pushBack(text);
				this.lguiMain.systemmsg(variant3, 4u);
			}
			bool flag4 = variant.ContainsKey("rmis");
			if (flag4)
			{
				Variant rmisDesc = (this.g_mgr as muLGClient).g_rmissCT.GetRmisDesc(variant["rmis"]._int);
				bool flag5 = rmisDesc != null;
				if (flag5)
				{
					bool flag6 = 1u == rmisDesc["type"]._uint;
					if (flag6)
					{
						(this.g_mgr as muLGClient).g_rmissCT.onAcceptRmisMis(rmisDesc["id"]._int);
					}
					else
					{
						bool flag7 = 2u == rmisDesc["type"]._uint;
						if (flag7)
						{
							(this.g_mgr as muLGClient).g_rmissCT.onAcceptRmisMis(rmisDesc["id"]._int);
							this.uiMis.PlayerRmisAccept(rmisDesc["id"]._uint);
						}
					}
				}
			}
			base.dispatchEvent(GameEvent.Create(3302u, this, data, false));
		}

		public void commit_mis(Variant data)
		{
			int num = data["misid"];
			bool flag = this.isnewPlayermis(num);
			if (flag)
			{
				Variant variant = this.get_mis_award(num, -1);
				bool flag2 = variant != null;
				if (flag2)
				{
					bool flag3 = variant.ContainsKey("eqp") && variant["eqp"][0] != null;
					if (flag3)
					{
						base.dispatchEvent(GameEvent.Create(4611u, this, variant["eqp"][0], false));
					}
					else
					{
						bool flag4 = variant.ContainsKey("itm") && variant["itm"][0] != null;
						if (flag4)
						{
							Variant variant2 = variant["itm"][0];
							uint item_id = variant2["id"];
							bool flag5 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.is_equip(item_id) || (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.is_uitem(item_id);
							if (flag5)
							{
								base.dispatchEvent(GameEvent.Create(4611u, this, variant2, false));
							}
						}
					}
				}
			}
			Variant variant3 = this._playerMiss[num.ToString()];
			bool flag6 = variant3 != null;
			if (flag6)
			{
				Variant variant4 = variant3["configdata"];
				Variant variant5 = variant4["goal"];
				bool flag7 = variant5.ContainsKey("lvl_score_awd");
				if (flag7)
				{
				}
				bool flag8 = variant4.ContainsKey("dalyrep") && variant4["dalyrep"]._int > 0;
				if (flag8)
				{
					bool flag9 = !this._no_line_data.ContainsKey(num.ToString());
					if (flag9)
					{
						this._no_line_data[num.ToString()] = variant4["dalyrep"];
					}
					Variant arg_20F_0 = this._no_line_data;
					string key = num.ToString();
					Variant val = arg_20F_0[key];
					arg_20F_0[key] = val - 1;
				}
				Variant variant6 = variant4["misline"];
				bool flag10 = variant6 != null;
				if (flag10)
				{
					this._line_data[variant6._str] = num;
					this.line_acceptable_reflesh(variant6._int);
				}
				this.preMisComplete(num);
				this.deletePlayerMiss(num);
			}
			(this.g_mgr as muLGClient).g_rmissCT.onCompleteMis(num);
			this.mission_change(num);
			base.dispatchEvent(GameEvent.Create(3303u, this, data, false));
			LGIUISystemOpen lGIUISystemOpen = this.g_mgr.g_uiM.getLGUI("LGUISystemOpenImpl") as LGIUISystemOpen;
			bool flag11 = lGIUISystemOpen != null;
			if (flag11)
			{
				lGIUISystemOpen.OnSelfCpMission((uint)num);
			}
			string text = LanguagePack.getLanguageText("missionMsg", "commit");
			string languageText = LanguagePack.getLanguageText("misName", num.ToString());
			text = DebugTrace.Printf(text, new string[]
			{
				languageText
			});
			Variant variant7 = new Variant();
			variant7.pushBack(text);
			this.lguiMain.systemmsg(variant7, 4u);
		}

		public void abord_mis(Variant data)
		{
			int @int = data._int;
			Variant variant = this._playerMiss[@int.ToString()];
			bool flag = variant != null;
			if (flag)
			{
				this.deletePlayerMiss(@int);
			}
			this.delete_follow_mis_cnt(@int);
			this.mission_change(@int);
		}

		public void mis_data_modify(Variant data)
		{
			int misid = data["misid"];
			Variant variant = this._playerMiss[misid.ToString()];
			Variant variant2 = null;
			bool flag = variant == null;
			if (flag)
			{
				this.addPlayerMiss(data);
			}
			else
			{
				variant2 = variant["data"];
				foreach (string current in data.Keys)
				{
					variant["data"][current] = data[current];
				}
				bool flag2 = data.ContainsKey("colmid") && data.ContainsKey("cnt");
				if (flag2)
				{
					bool flag3 = variant2.ContainsKey("colm");
					if (flag3)
					{
						foreach (Variant current2 in variant2["colm"]._arr)
						{
							bool flag4 = current2["monid"]._int == data["colmid"]._int;
							if (flag4)
							{
								current2["cnt"] = data["cnt"];
								this.updataAcceptMisState(variant, true);
								this.mission_change(misid);
								break;
							}
						}
					}
				}
			}
			bool flag5 = this.is_mis_complete(misid);
			if (flag5)
			{
				bool flag6 = variant2 != null && variant2.ContainsKey("fin_desc");
				if (flag6)
				{
				}
				base.dispatchEvent(GameEvent.Create(3305u, this, data, false));
			}
		}

		public void get_fined_mis_state_res(Variant data)
		{
			bool flag = data.ContainsKey("misfined");
			if (flag)
			{
				Variant variant = data["misfined"];
				bool flag2 = variant != null;
				if (flag2)
				{
					for (int i = 0; i < variant.Count; i++)
					{
						Variant variant2 = variant[i];
						bool flag3 = variant2.ContainsKey("cntleft");
						if (flag3)
						{
							this._no_line_data[variant2["misid"]._str] = variant2["cntleft"];
						}
						else
						{
							this._no_line_data[variant2["misid"]._str] = 0;
						}
					}
				}
			}
			bool flag4 = data.ContainsKey("unfined");
			if (flag4)
			{
				Variant variant3 = data["unfined"];
				bool flag5 = variant3 != null;
				if (flag5)
				{
					for (int i = 0; i < variant3.Count; i++)
					{
						int misid = variant3[i];
						Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
						bool flag6 = variant2 == null;
						if (!flag6)
						{
							bool flag7 = variant2.ContainsKey("rep") && variant2["rep"] > 0;
							if (flag7)
							{
								this._no_line_data[misid.ToString()] = variant2["rep"];
							}
							else
							{
								bool flag8 = variant2.ContainsKey("dalyrep");
								if (flag8)
								{
									this._no_line_data[misid.ToString()] = variant2["dalyrep"];
								}
								else
								{
									this._no_line_data[misid.ToString()] = 1;
								}
							}
						}
					}
				}
			}
			this.acceptable_refault();
			this.set_npcmis();
		}

		private void monster_kill(int mid)
		{
			foreach (Variant current in this._playerMiss.Values)
			{
				Variant variant = current["data"];
				bool flag = !variant.ContainsKey("km");
				if (!flag)
				{
					Variant variant2 = variant["km"];
					foreach (Variant current2 in variant2._arr)
					{
						bool flag2 = current2["monid"]._int == mid;
						if (flag2)
						{
							int num = variant["misid"];
						}
					}
				}
			}
		}

		private void reflush_option_limit()
		{
		}

		private void _can_show_cache_warning()
		{
		}

		public uint get_mon_id_by_killmonitm(Variant kilmonitm, int tpid)
		{
			uint result;
			for (int i = 0; i < kilmonitm.Count; i++)
			{
				Variant variant = kilmonitm[i];
				bool flag = variant["tpid"]._int == tpid;
				if (flag)
				{
					result = variant["monids"][0];
					return result;
				}
			}
			result = 0u;
			return result;
		}

		public void check_follow()
		{
		}

		public void mis_kilmon_modify(Variant data)
		{
		}

		public void AutoFirstMission()
		{
			Variant variant = null;
			bool flag = variant != null;
			if (flag)
			{
				bool flag2 = this._playerMiss[variant.ToString()] != null;
				if (flag2)
				{
					this.accpet_move(variant._int, false);
				}
			}
		}

		private void add_daymis_info(Variant dayarr, Variant mis)
		{
			bool flag = mis == null;
			if (!flag)
			{
				bool flag2 = this.isdalyrep_mis(mis);
				if (flag2)
				{
					bool flag3 = this._no_line_data.ContainsKey(mis["id"]._str);
					if (!flag3)
					{
						dayarr.pushBack(mis["id"]);
					}
				}
			}
		}

		private void add_mis_info(Variant no_line_arr, Variant line_arr, Variant mis)
		{
			bool flag = mis == null;
			if (!flag)
			{
				int num = mis["misline"];
				bool flag2 = num <= 0;
				if (flag2)
				{
					bool flag3 = this._no_line_data.ContainsKey(mis["id"]._str);
					if (flag3)
					{
						return;
					}
					no_line_arr.pushBack(mis["id"]);
				}
				bool flag4 = this._line_data.ContainsKey(num.ToString());
				if (!flag4)
				{
					bool flag5 = false;
					for (int i = 0; i < line_arr.Count; i++)
					{
						bool flag6 = line_arr[i] == num;
						if (flag6)
						{
							flag5 = true;
							break;
						}
					}
					bool flag7 = !flag5;
					if (flag7)
					{
						line_arr.pushBack(num);
					}
				}
			}
		}

		public Variant get_npc_misacept(int npcid)
		{
			Variant variant = new Variant();
			bool flag = this._playerMiss.Count > 0;
			if (flag)
			{
				foreach (Variant current in this._playerMiss.Values)
				{
					Variant variant2 = current["configdata"];
					bool flag2 = variant2 == null;
					if (!flag2)
					{
						bool flag3 = variant2["awards"]["npc"]._int == npcid;
						if (flag3)
						{
							variant.pushBack(variant2["id"]);
						}
					}
				}
			}
			return variant;
		}

		public Variant get_npc_acceptable_mis(int npcid)
		{
			Variant variant = null;
			Variant variant2 = new Variant();
			for (int i = 0; i < this.acceptable_mis.Count; i++)
			{
				Variant variant3 = this.acceptable_mis[i];
				bool flag = variant3 == null;
				if (!flag)
				{
					bool flag2 = variant3["accept"]["npc"]._int == npcid;
					if (flag2)
					{
						bool flag3 = variant3.ContainsKey("rmis");
						if (flag3)
						{
							bool flag4 = variant == null;
							if (flag4)
							{
								variant = (this.g_mgr as muLGClient).g_rmissCT.GetPlayerAcceptedRmis();
							}
							bool flag5 = LGGDRmission.IsArrayHasValue(variant, variant3["rmis"]._int) || !(this.g_mgr as muLGClient).g_rmissCT.IsRmisCanAccept(variant3["rmis"]);
							if (flag5)
							{
								goto IL_E8;
							}
						}
						variant2.pushBack(variant3["id"]);
					}
				}
				IL_E8:;
			}
			return variant2;
		}

		public Variant get_mission_goal(Variant conf, int goalid = 0)
		{
			bool flag = conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = conf.ContainsKey("carr_gaol");
				if (flag2)
				{
					Variant variant = conf["carr_goal"];
					Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
					bool flag3 = variant.ContainsKey(mainPlayerInfo["carr"]._int);
					if (flag3)
					{
						result = variant;
						return result;
					}
				}
				result = conf["goal"][0];
			}
			return result;
		}

		public Variant get_kill_mon(int misid)
		{
			Variant variant = new Variant();
			Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = variant2 == null;
			Variant result;
			if (flag)
			{
				result = variant;
			}
			else
			{
				Variant variant3 = this.get_mission_goal(variant2, 0);
				bool flag2 = variant3.ContainsKey("colitm");
				if (flag2)
				{
					Variant variant4 = variant3["colitm"];
					foreach (Variant current in variant4._arr)
					{
						bool flag3 = current.ContainsKey("pos");
						if (flag3)
						{
							uint val = this.get_mon_id_by_killmonitm(variant3["kilmonitm"], current["tpid"]._int);
							variant.pushBack(val);
						}
					}
				}
				else
				{
					bool flag4 = variant3.ContainsKey("ownitm");
					if (flag4)
					{
						Variant variant5 = variant3["ownitm"];
						foreach (Variant current2 in variant5._arr)
						{
							bool flag5 = current2.ContainsKey("pos");
							if (flag5)
							{
								uint val2 = this.get_mon_id_by_killmonitm(variant3["kilmonitm"], current2["tpid"]._int);
								variant.pushBack(val2);
							}
						}
					}
					else
					{
						bool flag6 = variant3.ContainsKey("kilmon");
						if (flag6)
						{
							Variant variant6 = variant3["kilmon"];
							foreach (Variant current3 in variant6._arr)
							{
								variant.pushBack(current3["monid"]);
							}
						}
					}
				}
				result = variant;
			}
			return result;
		}

		public int get_mis_type(int misid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = variant == null;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				int num = variant["misline"];
				bool flag2 = num == 1;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					bool flag3 = variant.ContainsKey("autocomit_yb") && variant["autocomit_yb"] > 0;
					if (flag3)
					{
						result = 5;
					}
					else
					{
						bool flag4 = variant.ContainsKey("goaladdition_daly");
						if (flag4)
						{
							result = 7;
						}
						else
						{
							bool flag5 = (variant.ContainsKey("rmis") && variant["rmis"] > 0) || (variant.ContainsKey("dalyrep") && variant["dalyrep"] > 0);
							if (flag5)
							{
								result = 4;
							}
							else
							{
								result = 2;
							}
						}
					}
				}
			}
			return result;
		}

		public int get_first_beginner_misaccet()
		{
			int num = 0;
			foreach (string current in this._playerMiss.Keys)
			{
				bool flag = num > this._playerMiss[current]["misid"]._int;
				if (flag)
				{
					num = this._playerMiss[current]["misid"]._int;
				}
			}
			int result = 0;
			Variant variant = this._playerMiss[num.ToString()];
			bool flag2 = variant != null && variant["configdata"] != null;
			if (flag2)
			{
				Variant variant2 = variant["configdata"];
				bool flag3 = 1 == variant2["misline"];
				if (flag3)
				{
					result = variant2["id"];
				}
			}
			else
			{
				Variant variant2 = this._acceptable[0];
				bool flag4 = variant2 != null && 1 == variant2["misline"];
				if (flag4)
				{
					result = variant2["id"];
				}
			}
			return result;
		}

		public int get_mis_state(int mis_id)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mis_id);
			bool flag = variant == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = this.is_accept_able_mis(variant);
				if (flag2)
				{
					result = 1;
				}
				else
				{
					result = (this.is_mis_complete(mis_id) ? 3 : 2);
				}
			}
			return result;
		}

		public int get_mis_flag(int mis_id)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mis_id);
			bool flag = variant == null;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				bool flag2 = this.is_accepted_mis(mis_id);
				if (flag2)
				{
					bool flag3 = this.is_mis_complete(mis_id);
					bool flag4 = flag3;
					if (flag4)
					{
						result = 4;
					}
					else
					{
						result = 2;
					}
				}
				else
				{
					bool flag5 = variant["misline"]._int <= 0;
					if (flag5)
					{
						bool flag6 = this._no_line_data.ContainsKey(mis_id.ToString());
						if (flag6)
						{
							result = 8;
						}
						else
						{
							result = 1;
						}
					}
					else
					{
						bool flag7 = !this._line_data.ContainsKey(variant["misline"]._str) || this._line_data[variant["misline"]._str]._int < mis_id;
						if (flag7)
						{
							result = 1;
						}
						else
						{
							result = 8;
						}
					}
				}
			}
			return result;
		}

		public Variant get_mis_award(int misid, int carr = -1)
		{
			int num = 0;
			int num2 = 0;
			int val = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			Variant variant3 = new Variant();
			Variant variant4 = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = variant4 == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant5 = new Variant();
				bool flag2 = variant4.ContainsKey("gawards") && variant4["gawards"] != null;
				if (flag2)
				{
					bool flag3 = this._playerMiss[misid.ToString()] != null;
					Variant variant6;
					if (flag3)
					{
						variant6 = this._playerMiss[misid.ToString()]["goal"];
					}
					else
					{
						variant6 = this.get_mission_goal(variant4, 0);
					}
					foreach (Variant current in variant4["gawards"]._arr)
					{
						bool flag4 = current["gid"] == variant6["id"];
						if (flag4)
						{
							variant5 = current;
							break;
						}
					}
				}
				else
				{
					variant5 = variant4["awards"];
				}
				Variant variant7 = variant5["award"];
				bool flag5 = variant7 == null;
				if (flag5)
				{
					result = null;
				}
				else
				{
					foreach (Variant current2 in variant7._arr)
					{
						bool flag6 = true;
						bool flag7 = current2.ContainsKey("carrid") && current2["carrid"]._int > 0;
						if (flag7)
						{
							bool flag8 = carr < 0;
							int num8;
							if (flag8)
							{
								Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
								num8 = mainPlayerInfo["carr"];
							}
							else
							{
								num8 = carr;
							}
							bool flag9 = num8 != current2["carrid"]._int;
							if (flag9)
							{
								flag6 = false;
							}
						}
						bool flag10 = !flag6;
						if (!flag10)
						{
							bool flag11 = current2.ContainsKey("exp");
							if (flag11)
							{
								num += current2["exp"]._int;
							}
							bool flag12 = current2.ContainsKey("skexp");
							if (flag12)
							{
								num2 += current2["skexp"]._int;
							}
							bool flag13 = current2.ContainsKey("gld");
							if (flag13)
							{
								num3 += current2["gld"]._int;
							}
							bool flag14 = current2.ContainsKey("clan");
							if (flag14)
							{
								Variant variant8 = current2["clan"];
								for (int i = 0; i < variant8.Count; i++)
								{
									num4 += variant8[i]["clana"]._int;
									num5 += variant8[i]["clang"]._int;
									num6 += variant8[i]["gold"]._int;
									num7 += variant8[i]["yb"]._int;
								}
							}
							bool flag15 = current2.ContainsKey("achive");
							if (flag15)
							{
								variant.pushBack(current2["achive"]);
							}
							bool flag16 = current2.ContainsKey("eqp");
							if (flag16)
							{
								for (int j = 0; j < current2["eqp"].Count; j++)
								{
									variant2.pushBack(current2["eqp"][j]);
								}
							}
							bool flag17 = current2.ContainsKey("itm");
							if (flag17)
							{
								for (int k = 0; k < current2["itm"].Count; k++)
								{
									variant3.pushBack(current2["itm"][k]);
								}
							}
							bool flag18 = current2.ContainsKey("meript");
							if (flag18)
							{
								val = current2["meript"];
							}
						}
					}
					Variant variant9 = new Variant();
					variant9["exp"] = num;
					variant9["skexp"] = num2;
					variant9["gld"] = num3;
					variant9["clana"] = num4;
					variant9["clang"] = num5;
					variant9["achives"] = variant;
					variant9["eqp"] = variant2;
					variant9["itm"] = variant3;
					variant9["meript"] = val;
					variant9["clangld"] = num6;
					variant9["clanyb"] = num7;
					result = variant9;
				}
			}
			return result;
		}

		public string get_mis_name(int mis_id)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mis_id);
			string text = "";
			bool flag = variant == null;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				bool flag2 = variant.ContainsKey("rmis") && !this.is_accept_able_mis(variant);
				if (flag2)
				{
					text = LanguagePack.getLanguageText("rmisName", variant["rmis"]._str);
					result = text;
				}
				else
				{
					text = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.getMisName(mis_id);
					string text2 = text;
					bool flag3 = variant["misline"] > 0;
					if (flag3)
					{
						result = text2;
					}
					else
					{
						bool flag4 = (!variant.ContainsKey("rep") || variant["rep"] <= 0) && (!variant.ContainsKey("dalyrep") || variant["dalyrep"] <= 0);
						if (flag4)
						{
							result = text2;
						}
						else
						{
							bool flag5 = this._no_line_data.ContainsKey(mis_id);
							int num;
							if (flag5)
							{
								num = this._no_line_data[mis_id];
							}
							else
							{
								num = variant["dalyrep"];
							}
							text2 = string.Concat(new string[]
							{
								text2,
								"(",
								(variant["dalyrep"]._int - num).ToString(),
								"/",
								variant["dalyrep"]._str,
								")"
							});
							result = text2;
						}
					}
				}
			}
			return result;
		}

		public void SetMlineawd(int misid)
		{
			this._mlineawd = misid;
		}

		public void AddMlineawd(int misid)
		{
			this._mlineawd = misid;
		}

		public int GetMlineawd()
		{
			return this._mlineawd;
		}

		private void preMisComplete(int misid)
		{
		}

		public void UpdateRmis(int misid)
		{
			foreach (Variant current in this._playerMiss.Values)
			{
				bool flag = current["misid"]._int == misid;
				if (flag)
				{
					bool flag2 = !current["isComplete"]._bool;
					if (flag2)
					{
						this.updataAcceptMisState(current, false);
					}
					break;
				}
			}
		}

		public bool is_attchk_commit_mis(int misid)
		{
			bool result = true;
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = variant != null && variant.ContainsKey("goal");
			if (flag)
			{
				Variant variant2 = variant["goal"]["attchk"];
				bool flag2 = variant2 != null;
				if (flag2)
				{
					foreach (Variant current in variant2._arr)
					{
						bool flag3 = current["name"] == "level" && mainPlayerInfo["level"]._int < current["min"]._int;
						if (flag3)
						{
							result = false;
							break;
						}
					}
				}
			}
			return result;
		}

		public Variant GetFinGmis()
		{
			return this._fingmis;
		}

		public Variant GetFinVips()
		{
			return this._finvips;
		}

		public Variant GetGmisKillmons()
		{
			return this._killmons;
		}

		public void GetGmisInfo()
		{
			(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetGmisInfo();
		}

		public void GetGmisAwd(int gmisid, bool vip = false)
		{
			Variant variant = new Variant();
			variant["gmisid"] = gmisid;
			variant["vip"] = vip;
			(this.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetGmisAwd(variant);
		}

		public void on_get_gmis(Variant data)
		{
			int @int = data["tp"]._int;
			if (@int != 1)
			{
				if (@int == 2)
				{
					bool flag = false;
					foreach (Variant current in this._killmons._arr)
					{
						bool flag2 = current["gid"] == data["gid"];
						if (flag2)
						{
							flag = true;
							foreach (Variant current2 in data["km"]._arr)
							{
								bool flag3 = false;
								foreach (Variant current3 in current["km"]._arr)
								{
									bool flag4 = current3["monid"] == current2["monid"];
									if (flag4)
									{
										flag3 = true;
										Variant variant = current3;
										variant["cnt"] = variant["cnt"] + current2["cnt"]._int;
										break;
									}
								}
								bool flag5 = !flag3;
								if (flag5)
								{
									current["km"].pushBack(current2);
								}
							}
							break;
						}
					}
					bool flag6 = !flag;
					if (flag6)
					{
						Variant variant2 = new Variant();
						variant2["gid"] = data["gid"];
						variant2["km"] = data["km"];
						this._killmons.pushBack(variant2);
					}
				}
			}
			else
			{
				this._fingmis = data["fin_gmis"];
				this._finvips = data["fin_vip"];
				bool flag7 = data.ContainsKey("killmons") && data["killmons"] != null;
				if (flag7)
				{
					this._killmons = data["killmons"];
				}
			}
		}

		public void on_get_gmisawd(Variant data)
		{
			bool flag = data.ContainsKey("gmisid");
			if (flag)
			{
				this._fingmis.pushBack(data["gmisid"]);
			}
			bool flag2 = data.ContainsKey("vip");
			if (flag2)
			{
				this._finvips.pushBack(data["vip"]);
			}
		}

		public static Variant getObjectBykeyValue(Variant data, string key, Variant value)
		{
			bool flag = data == null || data.Count <= 0;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (Variant current in data._arr)
				{
					bool flag2 = current != null && current[key]._int == value._int;
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
	}
}
