using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MuGame
{
	public class missionInfo : LGDataBase
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly missionInfo.<>c <>9 = new missionInfo.<>c();

			public static Comparison<Variant> <>9__59_0;

			internal int <acceptable_refault>b__59_0(Variant left, Variant right)
			{
				bool flag = left["misline"] > right["misline"];
				int result;
				if (flag)
				{
					result = 1;
				}
				else
				{
					bool flag2 = left["misline"] == right["misline"];
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

		private Variant _line_data = new Variant();

		private Variant _no_line_data = new Variant();

		private Variant _playerMis = new Variant();

		private Variant _acceptable = new Variant();

		private Variant _mis_qa_arr = new Variant();

		private Variant _mis_uitm_arr = new Variant();

		private Variant _mis_operate_arr = new Variant();

		private Variant _mis_cgoal_arr = new Variant();

		private Variant _misAction = new Variant();

		private uint _current_map_id = 0u;

		private int _mlineawd = 0;

		private Variant _rmisConfData = new Variant();

		private Variant _playerRmis = new Variant();

		private Variant _rmis_share = new Variant();

		private int _freeInsure = 0;

		private Variant _appawdRmis;

		private bool _reSetPlaymis = false;

		private bool _reSetShare = false;

		private Variant _fingmis;

		private Variant _finvips;

		private Variant _killmons = new Variant();

		public Variant misacept
		{
			get
			{
				return this._playerMis;
			}
		}

		public Variant no_line_data
		{
			get
			{
				return this._no_line_data;
			}
		}

		public Variant acceptableMis
		{
			get
			{
				return this._acceptable;
			}
		}

		public missionInfo(muNetCleint m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new missionInfo(m as muNetCleint);
		}

		public override void init()
		{
		}

		private void onJoinWorldRes(GameEvent e)
		{
			Variant misacept = (base.g_mgr as muNetCleint).joinWorldInfoInst.misacept;
			this.setAcceptMis(misacept);
			this.read_current_map_mis_line(0u);
			this.initRmisData();
			this.InitPlayerRmisData();
		}

		private void onFinedMisStateRes(GameEvent e)
		{
			Variant data = e.data;
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
						Variant variant2 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
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
			this.setNpcMis();
		}

		private void onDataMisModityRes(GameEvent e)
		{
		}

		private void onMisLineStateRes(GameEvent e)
		{
			Variant variant = e.data["misline"];
			for (int i = 0; i < variant.Count; i++)
			{
				Variant variant2 = variant[i];
				Variant variant3 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(variant2["curmis"]._int);
				bool flag = variant3 == null;
				if (flag)
				{
					int lastComMis = this.getLastComMis(variant2);
					this._line_data[variant2["lineid"]._str] = lastComMis;
				}
				else
				{
					this._line_data[variant2["lineid"]._str] = variant2["curmis"];
				}
			}
			this.acceptable_refault();
			this.setNpcMis();
		}

		private int getLastComMis(Variant line)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_by_line(line["lineid"]._int);
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

		private void onLvlMisPrizeRes(GameEvent e)
		{
		}

		private void onAbordMisRes(GameEvent e)
		{
			Variant data = e.data;
			int num = 0;
			bool flag = data.ContainsKey("res");
			if (flag)
			{
				num = data["res"];
			}
			bool flag2 = num == 1;
			if (flag2)
			{
				int num2 = data["misid"];
				foreach (Variant current in this._playerMis._arr)
				{
					bool flag3 = current != null && current["misid"] == num2;
					if (flag3)
					{
						this._playerMis._arr.Remove(current);
						break;
					}
				}
			}
		}

		private void onAcceptMisRes(GameEvent e)
		{
			Variant data = e.data;
			int misid = data["misid"];
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			this.init_single_localmis(misid, true);
			bool flag = this.addPlayerMiss(data);
			if (flag)
			{
				this.delete_accept_mis(misid);
			}
			this.missionChange(misid);
			string misGoalDesc = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.getMisGoalDesc(misid);
			bool flag2 = misid.ToString() != misGoalDesc;
			if (flag2)
			{
			}
			int misType = this.getMisType(misid);
			bool flag3 = 4 == misType || 5 == misType || 7 == misType;
			if (flag3)
			{
				string languageText = LanguagePack.getLanguageText("missionMsg", "accept");
				string languageText2 = LanguagePack.getLanguageText("misName", misid.ToString());
			}
			bool flag4 = variant.ContainsKey("rmis");
			if (flag4)
			{
				Variant rmisDesc = this.GetRmisDesc(variant["rmis"]._int);
				bool flag5 = rmisDesc != null;
				if (flag5)
				{
					bool flag6 = 1 == rmisDesc["type"]._int;
					if (!flag6)
					{
						bool flag7 = 2 == rmisDesc["type"];
						if (flag7)
						{
							this.onAcceptRmisMis(rmisDesc["id"]);
						}
					}
				}
			}
			Variant variant2 = new Variant();
			variant2["name"] = "mdlg_npcDialog";
			base.g_mgr.g_uiM.dispatchEvent(GameEvent.Create(4002u, this, variant2, false));
		}

		private void onCommitMisRes(GameEvent e)
		{
			Variant data = e.data;
			int num = data["misid"];
			bool flag = this.isNewPlayermis(num);
			if (flag)
			{
				Variant misAward = this.getMisAward(num, -1);
				bool flag2 = misAward != null;
				if (flag2)
				{
					bool flag3 = misAward.ContainsKey("eqp") && misAward["eqp"][0] != null;
					if (!flag3)
					{
						bool flag4 = misAward.ContainsKey("itm") && misAward["itm"][0] != null;
						if (flag4)
						{
							Variant variant = misAward["itm"][0];
						}
					}
				}
			}
			Variant variant2 = this._playerMis[num.ToString()];
			bool flag5 = variant2 != null;
			if (flag5)
			{
				Variant variant3 = variant2["configdata"];
				Variant variant4 = variant3["goal"];
				bool flag6 = variant4.ContainsKey("lvl_score_awd");
				if (flag6)
				{
				}
				bool flag7 = variant3.ContainsKey("dalyrep") && variant3["dalyrep"]._int > 0;
				if (flag7)
				{
					bool flag8 = !this._no_line_data.ContainsKey(num.ToString());
					if (flag8)
					{
						this._no_line_data[num.ToString()] = variant3["dalyrep"];
					}
					Variant arg_17C_0 = this._no_line_data;
					string key = num.ToString();
					Variant val = arg_17C_0[key];
					arg_17C_0[key] = val - 1;
				}
				Variant variant5 = variant3["misline"];
				bool flag9 = variant5 != null;
				if (flag9)
				{
					this._line_data[variant5.ToString()] = num;
					this._line_refresh_acceptable(variant5._int);
				}
				this.preMisComplete(num);
				this.deletePlayerMis(num);
			}
			this.onCompleteRmis(num);
			this.missionChange(num);
			Variant variant6 = new Variant();
			variant6["name"] = "mdlg_npcDialog";
			base.g_mgr.g_uiM.dispatchEvent(GameEvent.Create(4002u, this, variant6, false));
			bool flag10 = this.is_main_mis(num);
			if (flag10)
			{
			}
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
				Variant missions = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_missions();
				foreach (Variant current in missions.Values)
				{
					Variant variant4 = current["accept"];
					bool flag2 = variant4.ContainsKey("attchk");
					if (flag2)
					{
					}
					this.add_mis_info(variant2, variant, current);
					this.add_daymis_info(variant3, current);
				}
				bool flag3 = variant.Count > 0;
				if (flag3)
				{
					(base.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetMisLineState(variant);
				}
				bool flag4 = variant2.Count > 0;
				if (flag4)
				{
					(base.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetFinedMisState(variant2);
				}
				bool flag5 = variant3.Count > 0;
				if (flag5)
				{
					(base.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetFinedMisState(variant3);
				}
			}
		}

		public string get_mis_name(int mis_id)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mis_id);
			string text = "";
			bool flag = variant == null;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				bool flag2 = variant.ContainsKey("rmis") && !this.is_acceptable_mis(variant);
				if (flag2)
				{
					text = LanguagePack.getLanguageText("rmisName", variant["rmis"]._str);
					result = text;
				}
				else
				{
					text = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.getMisName(mis_id);
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
						Variant conf = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mid);
						Variant missionGoal = this.getMissionGoal(conf, 0);
						int num3 = this.get_mon_id_by_killmonitm(missionGoal["kilmonitm"], uitem["tpid"]._int);
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
							uitem["pos"][0]["mpid"],
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
							text = string.Concat(new string[]
							{
								"buyitem_",
								uitem["npcid"]._str,
								"_",
								mid.ToString(),
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
								Variant variant2 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMarketConf.get_game_market_sell_data_by_tpid(tpid);
								bool flag8 = variant2 != null;
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
				text = text + "_" + uitem["level_id"];
			}
			return text;
		}

		public Variant get_npc_misacept(int npcid)
		{
			Variant variant = new Variant();
			foreach (Variant current in this._playerMis.Values)
			{
				Variant variant2 = current["configdata"];
				bool flag = variant2 == null;
				if (!flag)
				{
					bool flag2 = variant2["awards"][0]["npc"]._int == npcid;
					if (flag2)
					{
						variant.pushBack(variant2["id"]);
					}
				}
			}
			return variant;
		}

		public Variant get_npc_acceptable_mis(int npcid)
		{
			Variant variant = null;
			Variant variant2 = new Variant();
			for (int i = 0; i < this.acceptableMis.Count; i++)
			{
				Variant variant3 = this.acceptableMis[i];
				bool flag = variant3 == null;
				if (!flag)
				{
					bool flag2 = variant3["accept"][0]["npc"]._int == npcid;
					if (flag2)
					{
						bool flag3 = variant3.ContainsKey("rmis");
						if (flag3)
						{
							bool flag4 = variant == null;
							if (flag4)
							{
								variant = this.GetPlayerAcceptedRmis();
							}
							bool flag5 = missionInfo.IsArrayHasValue(variant, variant3["rmis"]) || !this.IsRmisCanAccept(variant3["rmis"]);
							if (flag5)
							{
								goto IL_CD;
							}
						}
						variant2.pushBack(variant3["id"]);
					}
				}
				IL_CD:;
			}
			return variant2;
		}

		public Variant getMissionGoal(Variant conf, int goalid = 0)
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
					Variant mainPlayerInfo = (base.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
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

		public int getMisType(int misid)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
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
			foreach (string current in this._playerMis.Keys)
			{
				bool flag = num > this._playerMis[current]["misid"]._int;
				if (flag)
				{
					num = this._playerMis[current]["misid"]._int;
				}
			}
			int result = 0;
			Variant variant = this._playerMis[num.ToString()];
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
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mis_id);
			bool flag = variant == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = this.is_acceptable_mis(variant);
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
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mis_id);
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

		public int getKillmonCnt(int misid)
		{
			int num = 0;
			Variant variant = null;
			Variant variant2 = null;
			bool flag = this._playerMis[misid.ToString()] != null;
			if (flag)
			{
				Variant variant3 = this._playerMis[misid.ToString()]["goal"];
				variant = this._playerMis[misid.ToString()]["configdata"];
				variant2 = this._playerMis[misid.ToString()]["data"];
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

		public void missionChange(int misid)
		{
			bool flag = misid > 0;
			if (flag)
			{
				this.refreshMisNpc(misid);
			}
			else
			{
				this.setNpcMis();
			}
			Variant variant = new Variant();
			variant["misid"] = misid;
			base.dispatchEvent(GameEvent.Create(3051u, this, variant, false));
		}

		private void init_local_mis()
		{
			bool flag = this._playerMis.Count <= 0;
			if (!flag)
			{
				foreach (Variant current in this._playerMis.Values)
				{
					bool flag2 = current == null;
					if (!flag2)
					{
						DebugTrace.dumpObj(current);
						int misid = current["misid"];
						this.init_single_localmis(misid, false);
					}
				}
			}
		}

		private void init_single_localmis(int misid, bool newAccept = false)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = variant == null;
			if (!flag)
			{
				Variant missionGoal = this.getMissionGoal(variant, 0);
				bool flag2 = missionGoal.ContainsKey("qa");
				if (flag2)
				{
					Variant variant2 = new Variant();
					variant2["id"] = misid;
					variant2["complete"] = false;
					this._mis_qa_arr[missionGoal["qa"]["qamis"].ToString()] = variant2;
				}
				bool flag3 = missionGoal.ContainsKey("uitm");
				if (flag3)
				{
					Variant variant3 = new Variant();
					variant3["id"] = misid;
					variant3["complete"] = false;
				}
				bool flag4 = missionGoal.ContainsKey("clientgoal");
				if (flag4)
				{
				}
				bool flag5 = newAccept && missionGoal.ContainsKey("operate");
				if (flag5)
				{
					Variant variant4 = new Variant();
					variant4["id"] = misid;
					variant4["complete"] = false;
					this._mis_operate_arr[missionGoal["operate"]] = variant4;
				}
			}
		}

		private void clearLocalMis(int misid)
		{
			bool flag = this._playerMis == null || this._playerMis.Count <= 0;
			if (!flag)
			{
				foreach (Variant current in this._playerMis._arr)
				{
					bool flag2 = current["misid"] != misid;
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

		public Variant get_mis_cgoal_arr()
		{
			return this._mis_cgoal_arr;
		}

		public Variant get_mis_qa(int id)
		{
			return this._mis_qa_arr[id.ToString()];
		}

		public void MisDataChange(string type, Variant data)
		{
			bool flag = data.ContainsKey("fincnt");
			if (flag)
			{
				int num = data["fincnt"];
			}
			if (!(type == "rmis"))
			{
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
				bool flag9 = "rmis" == data["type"];
				if (flag9)
				{
				}
			}
		}

		public Variant getAcceptMisInfo(int misid)
		{
			return this._playerMis[misid.ToString()];
		}

		public void setAcceptMis(Variant data)
		{
			foreach (Variant current in data._arr)
			{
				this.addPlayerMiss(current);
			}
			this.init_local_mis();
		}

		public int get_line_proc(int misline)
		{
			return this._line_data[misline.ToString()];
		}

		public Variant get_allLine_proc()
		{
			return this._line_data;
		}

		public Variant getMisGoalAction(int misid)
		{
			return this._misAction[misid.ToString()];
		}

		private void deletePlayerMis(int misid)
		{
			Variant variant = this._playerMis[misid.ToString()];
			bool flag = variant != null;
			if (flag)
			{
				this._playerMis.RemoveKey(misid.ToString());
				this.clearLocalMis(misid);
			}
		}

		private bool addPlayerMiss(Variant data)
		{
			int num = data["misid"];
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(num);
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
				variant2["goal"] = this.getMissionGoal(variant, goalid);
				this._playerMis[num.ToString()] = variant2;
				this._updataAcceptMisState(variant2, false);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		private void _updataAcceptMisState(Variant acceptObj, bool flag = false)
		{
			Variant variant = acceptObj["goal"];
			int num = acceptObj["misid"];
			bool flag2 = true;
			Variant variant2 = acceptObj["data"];
			Variant variant3 = acceptObj["configdata"];
			bool flag3 = variant.ContainsKey("microclient") && variant["microclient"] == 1;
			if (flag3)
			{
			}
			bool flag4 = variant.ContainsKey("uitm");
			if (flag4)
			{
			}
			bool flag5 = variant.ContainsKey("clientgoal");
			if (flag5)
			{
				Variant variant4 = this._mis_cgoal_arr[variant["clientgoal"]];
				flag2 = (variant4 != null && variant4["complete"]._bool);
			}
			bool flag6 = variant.ContainsKey("operate");
			if (flag6)
			{
				Variant variant5 = this._mis_operate_arr[variant["operate"]];
				bool flag7 = variant5 == null || variant5["complete"]._bool;
				flag2 = flag7;
			}
			bool flag8 = variant.ContainsKey("qa");
			if (flag8)
			{
				Variant variant6 = variant["qa"];
				int num2 = variant6["qamis"];
				bool flag9 = this._mis_qa_arr.ContainsKey(num2.ToString());
				if (flag9)
				{
					Variant variant7 = this._mis_qa_arr[num2.ToString()];
					bool flag10 = !variant7["complete"];
					if (flag10)
					{
						flag2 = false;
					}
				}
			}
			bool flag11 = variant.ContainsKey("colmon");
			if (flag11)
			{
			}
			bool flag12 = variant.ContainsKey("colitm");
			if (flag12)
			{
			}
			bool flag13 = variant.ContainsKey("ownitm");
			if (flag13)
			{
			}
			bool flag14 = variant.ContainsKey("kilmon");
			if (flag14)
			{
				Variant variant8 = variant["kilmon"];
				for (int i = 0; i < variant8.Count; i++)
				{
					Variant variant9 = variant8[i];
					Variant variant10 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMonsterConf.get_monster_data(variant9["monid"]._int);
					bool flag15 = variant10 == null;
					if (flag15)
					{
						flag2 = false;
					}
					else
					{
						int num3 = 0;
						float num4 = 100f;
						bool flag16 = 5 == this.getMisType(num);
						if (flag16)
						{
							Variant appawdRmis = this.GetAppawdRmis(num);
							Variant playerRmisInfo = this.GetPlayerRmisInfo(variant3["rmis"]._int);
							bool flag17 = appawdRmis != null;
							if (flag17)
							{
								Variant variant11 = appawdRmis["goal"];
								Variant variant12 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_appgoal(variant11["id"]._int);
								Variant objectBykeyValue = this.getObjectBykeyValue(variant12["qual_grp"], "qual", variant11["qual"]);
								num4 = (float)(100 + objectBykeyValue["per"]);
							}
							else
							{
								bool flag18 = playerRmisInfo != null && playerRmisInfo["misid"] == num;
								if (flag18)
								{
									Variant variant11 = playerRmisInfo["appgoal"];
									Variant variant12 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_appgoal(variant11["appgoal"]._int);
									int val = 1;
									bool flag19 = variant11["qual"] != 0;
									if (flag19)
									{
										val = variant11["qual"];
									}
									Variant objectBykeyValue = this.getObjectBykeyValue(variant12["qual_grp"], "qual", val);
									num4 = (float)(100 + objectBykeyValue["per"]);
								}
							}
						}
						bool flag20 = acceptObj != null && variant2 != null;
						if (flag20)
						{
							Variant variant13 = variant2["km"];
							bool flag21 = variant13 == null;
							if (flag21)
							{
								goto IL_49C;
							}
							for (int j = 0; j < variant13.Count; j++)
							{
								Variant variant14 = variant13[j];
								bool flag22 = variant14["monid"]._int == variant9["monid"]._int;
								if (flag22)
								{
									num3 = variant14["cnt"];
									break;
								}
							}
						}
						bool flag23 = num3 < (int)(variant9["cnt"] * num4 / 100f);
						if (flag23)
						{
							flag2 = false;
						}
					}
					IL_49C:;
				}
			}
			bool flag24 = variant.ContainsKey("kilmon_map");
			if (flag24)
			{
				Variant variant15 = variant["kilmon_map"];
				int i = 0;
				while (i < variant15.Count)
				{
					Variant variant16 = variant15[i];
					int num5 = 0;
					float num6 = 100f;
					bool flag25 = 5 == this.getMisType(num);
					if (flag25)
					{
						Variant appawdRmis2 = this.GetAppawdRmis(num);
						Variant playerRmisInfo2 = this.GetPlayerRmisInfo(variant3["rmis"]._int);
						bool flag26 = appawdRmis2;
						if (flag26)
						{
							Variant variant11 = appawdRmis2["goal"];
							Variant variant12 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_appgoal(variant11["id"]._int);
							Variant objectBykeyValue2 = this.getObjectBykeyValue(variant12["qual_grp"], "qual", variant11["qual"]);
							num6 = (float)(100 + objectBykeyValue2["per"]);
						}
						else
						{
							bool flag27 = playerRmisInfo2 != null && playerRmisInfo2["misid"] == num;
							if (flag27)
							{
								Variant variant11 = playerRmisInfo2["appgoal"];
								Variant variant12 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_appgoal(variant11["appgoal"]._int);
								int val2 = 1;
								bool flag28 = variant11["qual"] != 0;
								if (flag28)
								{
									val2 = variant11["qual"];
								}
								Variant objectBykeyValue2 = this.getObjectBykeyValue(variant12["qual_grp"], "qual", val2);
								num6 = (float)(100 + objectBykeyValue2["per"]);
							}
						}
					}
					bool flag29 = acceptObj != null && variant2 != null;
					if (!flag29)
					{
						goto IL_719;
					}
					Variant variant17 = variant2["km_map"];
					bool flag30 = variant17 == null;
					if (!flag30)
					{
						for (i = 0; i < variant17.Count; i++)
						{
							Variant variant18 = variant17[i];
							bool flag31 = variant18["mapid"]._int == variant16["mapid"]._int;
							if (flag31)
							{
								num5 = variant18["cnt"];
								break;
							}
						}
						goto IL_719;
					}
					IL_73C:
					i++;
					continue;
					IL_719:
					int killmonCnt = this.getKillmonCnt(num);
					bool flag32 = (float)num5 < (float)killmonCnt * num6 / 100f;
					if (flag32)
					{
						flag2 = false;
					}
					goto IL_73C;
				}
			}
			bool flag33 = variant.ContainsKey("pzckp");
			if (flag33)
			{
				Variant variant19 = variant["pzckp"];
				bool flag34 = variant2 != null && variant2.ContainsKey("pzckp");
				if (flag34)
				{
					bool flag35 = variant2["pzckp"] == 1;
					flag2 = flag35;
				}
			}
			bool flag36 = variant.ContainsKey("pzkp");
			if (flag36)
			{
				Variant variant20 = variant["pzkp"];
				int num7 = 0;
				bool flag37 = variant2 != null && variant2.ContainsKey("pzkp");
				if (flag37)
				{
					num7 = variant2["pzkp"];
				}
				bool flag38 = num7 < variant20[0]["cnt"];
				if (flag38)
				{
					flag2 = false;
				}
			}
			bool flag39 = variant.ContainsKey("kp");
			if (flag39)
			{
				Variant variant21 = variant["kp"];
				int num8 = 0;
				bool flag40 = variant2 && variant2.ContainsKey("kp");
				if (flag40)
				{
					num8 = variant2["kp"];
				}
				bool flag41 = num8 < variant21[0]["cnt"];
				if (flag41)
				{
					flag2 = false;
				}
			}
			bool flag42 = variant.ContainsKey("joinclan");
			if (flag42)
			{
				Variant mainPlayerInfo = (base.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
				bool flag43 = mainPlayerInfo.ContainsKey("clanid") && mainPlayerInfo["clanid"] > 0;
				flag2 = flag43;
			}
			bool flag44 = variant.ContainsKey("attchk");
			if (flag44)
			{
			}
			bool flag45 = variant.ContainsKey("enterlvl");
			if (flag45)
			{
			}
			bool flag46 = variant.ContainsKey("eqpchk");
			if (flag46)
			{
			}
			bool flag47 = variant.ContainsKey("finlvlmis");
			if (flag47)
			{
			}
			bool flag48 = variant.ContainsKey("finlvldiff");
			if (flag48)
			{
			}
			bool flag49 = variant.ContainsKey("meri");
			if (flag49)
			{
			}
			bool flag50 = variant.ContainsKey("action");
			if (flag50)
			{
				flag2 = false;
				Variant variant22 = variant["action"];
				Variant variant23 = this._misAction[num.ToString()];
				foreach (Variant current in variant22._arr)
				{
					bool flag51 = variant23 == null || variant23[current["id"]] != null;
					if (flag51)
					{
						flag2 = false;
						break;
					}
					flag2 = true;
				}
			}
			bool flag52 = variant.ContainsKey("ownskil");
			if (flag52)
			{
			}
			bool flag53 = variant.ContainsKey("lvl_score_awd");
			if (flag53)
			{
			}
			acceptObj["isComplete"] = flag2;
			bool flag54 = flag & flag2;
			if (flag54)
			{
			}
		}

		private void _line_refresh_acceptable(int lineid)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_by_line(lineid);
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
			bool flag = this.is_acceptable_mis(misData);
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
			this._line_refresh_acceptable(1);
			Variant missions = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_missions();
			foreach (Variant current in missions.Values)
			{
				bool flag = this.is_acceptable_mis(current);
				if (flag)
				{
					bool flag2 = !this._is_in_acceptable(current["id"]);
					if (flag2)
					{
						this._acceptable.pushBack(current);
					}
				}
			}
			List<Variant> arg_C7_0 = this._acceptable._arr;
			Comparison<Variant> arg_C7_1;
			if ((arg_C7_1 = missionInfo.<>c.<>9__59_0) == null)
			{
				arg_C7_1 = (missionInfo.<>c.<>9__59_0 = new Comparison<Variant>(missionInfo.<>c.<>9.<acceptable_refault>b__59_0));
			}
			arg_C7_0.Sort(arg_C7_1);
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

		public void setNpcMis()
		{
		}

		public void refreshMisNpc(int misid)
		{
		}

		public void updateNpcsMisState(Variant npcs)
		{
		}

		public void updateNpcMisState(Variant npc)
		{
		}

		private void _updateNpcMisState(LGAvatarNpc npc)
		{
		}

		private void _addMisTop(int type, LGAvatarNpc npc)
		{
		}

		public int getNpcMissionTopState(int npcid)
		{
			return 0;
		}

		public bool is_accepted_mis(int misid)
		{
			return this._playerMis[misid.ToString()] != null;
		}

		public bool is_acceptable_mis(Variant misData)
		{
			bool flag = misData == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int num = misData["id"];
				bool flag2 = this.is_accepted_mis(num);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = misData.ContainsKey("misline") && (!misData.ContainsKey("dalyrep") || misData["dalyrep"] <= 0 || misData["misline"] == 1);
					if (flag3)
					{
						bool flag4 = !misData.ContainsKey("rmis");
						if (flag4)
						{
							bool flag5 = this._line_data.ContainsKey(misData["misline"]._str);
							if (flag5)
							{
								bool flag6 = this._line_data[misData["misline"].ToString()] >= num;
								if (flag6)
								{
									result = false;
									return result;
								}
							}
						}
					}
					Variant variant = misData["accept"];
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
						Variant variant2 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(num2);
						bool flag9 = variant2 != null && !this._line_data.ContainsKey(variant2["misline"].ToString());
						if (flag9)
						{
							result = false;
						}
						else
						{
							bool flag10 = variant2 != null && this._line_data[variant2["misline"].ToString()] < num2;
							if (flag10)
							{
								result = false;
							}
							else
							{
								bool flag11 = misData["misline"] == 1;
								if (flag11)
								{
									bool flag12 = num2 == 0;
									if (flag12)
									{
										bool flag13 = variant.ContainsKey("show_premis");
										if (flag13)
										{
											int num3 = variant["show_premis"];
											Variant variant3 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(num3);
											bool flag14 = variant3 != null && this._line_data[variant3["misline"].ToString()] >= num3;
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
									Variant variant6 = variant["clan"];
									bool flag17 = variant6 != null;
									if (flag17)
									{
									}
									bool flag18 = this._no_line_data.ContainsKey(num.ToString());
									if (flag18)
									{
										bool flag19 = !misData.ContainsKey("rmis");
										if (flag19)
										{
											int num4 = this._no_line_data[num.ToString()];
											bool flag20 = num4 <= 0;
											if (flag20)
											{
												result = false;
												return result;
											}
										}
									}
									bool flag21 = misData.ContainsKey("tmchk");
									if (flag21)
									{
										bool flag22 = !this.is_in_open_tm(num);
										if (flag22)
										{
											result = false;
											return result;
										}
									}
									Variant missionGoal = this.getMissionGoal(misData, 0);
									bool flag23 = missionGoal.ContainsKey("microclient");
									if (flag23)
									{
									}
									bool flag24 = this.is_dalyrep_mis(misData);
									if (flag24)
									{
										bool flag25 = !this._no_line_data.ContainsKey(num.ToString());
										if (flag25)
										{
											result = true;
											return result;
										}
										int num5 = this._no_line_data[num.ToString()];
										bool flag26 = num5 <= 0;
										if (flag26)
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
			return result;
		}

		public bool is_accepted_rmis(int tp)
		{
			foreach (Variant current in this._playerMis._arr)
			{
				bool flag = current == null;
				if (!flag)
				{
					Variant variant = current["configdata"];
					bool flag2 = variant != null && variant.ContainsKey("rmis");
					if (flag2)
					{
					}
				}
			}
			return false;
		}

		public bool is_dalyrep_mis(Variant misData)
		{
			bool flag = misData == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = misData.ContainsKey("dalyrep") && misData["dalyrep"] > 0;
				result = flag2;
			}
			return result;
		}

		public bool is_in_open_tm(int misid)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
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
					long curServerTimeStampMS = (base.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS;
					result = false;
				}
			}
			return result;
		}

		public bool is_mis_complete(int misid)
		{
			Variant variant = this._playerMis[misid.ToString()];
			return variant != null && variant["isComplete"]._bool;
		}

		public bool is_mis_has_complete(int misid)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
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
						Variant variant2 = this._playerMis[misid.ToString()];
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
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			bool flag = !variant;
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
			return this._no_line_data.ContainsKey(mis["id"].ToString());
		}

		public bool is_main_mis(int misid)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_by_line(1);
			bool flag = variant != null && variant.Count > 0;
			return flag && variant[misid] != null;
		}

		public bool is_mis_goal(int misid)
		{
			Variant variant = this._playerMis[misid.ToString()];
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

		public bool isNewPlayermis(int misid)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			return variant != null && variant.ContainsKey("spcmis") && variant["spcmis"] == 1;
		}

		public bool is_cant_abord_mis(int misid)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
			return variant != null && variant.ContainsKey("cant_abord") && variant["cant_abord"] == 1;
		}

		public int get_mon_id_by_killmonitm(Variant kilmonitm, int tpid)
		{
			int result;
			for (int i = 0; i < kilmonitm.Count; i++)
			{
				Variant variant = kilmonitm[i];
				bool flag = variant["tpid"] == tpid;
				if (flag)
				{
					result = variant["monids"][0];
					return result;
				}
			}
			result = 0;
			return result;
		}

		private void add_daymis_info(Variant dayarr, Variant mis)
		{
			bool flag = mis == null;
			if (!flag)
			{
				bool flag2 = this.is_dalyrep_mis(mis);
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

		public Variant getMisAward(int misid, int carr = -1)
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
			Variant variant4 = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
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
					bool flag3 = this._playerMis[misid];
					Variant variant6;
					if (flag3)
					{
						variant6 = this._playerMis[misid.ToString()]["goal"];
					}
					else
					{
						variant6 = this.getMissionGoal(variant4, 0);
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
						bool flag7 = current2["carrid"]._int > 0;
						if (flag7)
						{
							bool flag8 = carr < 0;
							int num8;
							if (flag8)
							{
								Variant mainPlayerInfo = (base.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
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
			foreach (Variant current in this._playerMis.Values)
			{
				bool flag = current["misid"]._int == misid;
				if (flag)
				{
					bool flag2 = !current["isComplete"]._bool;
					if (flag2)
					{
						this._updataAcceptMisState(current, false);
					}
					break;
				}
			}
		}

		public bool is_attchk_commit_mis(int misid)
		{
			bool result = true;
			Variant mainPlayerInfo = (base.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
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

		public void on_item_use(int item_id)
		{
			bool flag = !this._mis_uitm_arr.ContainsKey(item_id.ToString()) || this._mis_uitm_arr[item_id.ToString()]["complete"];
			if (!flag)
			{
				this._mis_uitm_arr[item_id.ToString()]["complete"] = true;
				int misid = this._mis_uitm_arr[item_id.ToString()]["id"];
				Variant variant = this._playerMis[misid.ToString()];
				bool flag2 = variant != null;
				if (flag2)
				{
					variant["isComplete"] = true;
				}
				this.missionChange(misid);
			}
		}

		public void OnOperateComplete(string type)
		{
			Variant variant = this._mis_operate_arr[type];
			bool flag = variant != null;
			if (flag)
			{
				variant["complete"] = true;
				int misid = variant["id"];
				Variant variant2 = this._playerMis[misid.ToString()];
				bool flag2 = variant2 != null;
				if (flag2)
				{
					variant2["isComplete"] = true;
				}
				this.missionChange(misid);
			}
		}

		public void on_qa_answer(int qa_id)
		{
			bool flag = !this._mis_qa_arr.ContainsKey(qa_id.ToString()) || this._mis_qa_arr[qa_id.ToString()]["complete"];
			if (!flag)
			{
				this._mis_qa_arr[qa_id.ToString()]["complete"] = true;
				int misid = this._mis_qa_arr[qa_id.ToString()]["id"];
				Variant variant = this._playerMis[misid.ToString()];
				bool flag2 = variant != null;
				if (flag2)
				{
					variant["isComplete"] = true;
				}
				this.missionChange(misid);
			}
		}

		public void accpet_move(int mid, bool ignore_level = false)
		{
			Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(mid);
			bool flag = variant == null;
			if (!flag)
			{
				bool flag2 = variant != null && variant.ContainsKey("goal");
				if (flag2)
				{
					Variant missionGoal = this.getMissionGoal(variant, 0);
					bool flag3 = missionGoal.ContainsKey("kilmon");
					if (flag3)
					{
						Variant variant2 = missionGoal["kilmon"][0];
						bool flag4 = variant2 != null;
						if (flag4)
						{
							string text = "mon_" + variant2["monid"] + "_";
							bool flag5 = variant2.ContainsKey("pos");
							if (flag5)
							{
								Variant variant3 = variant2["pos"][0];
								text = string.Concat(new string[]
								{
									text,
									variant3["mpid"],
									"_",
									variant3["x"],
									"_",
									variant3["y"]
								});
							}
							else
							{
								text += "0_0_0";
							}
							int @int = variant2["level_id"]._int;
							bool flag6 = @int != 0;
							if (flag6)
							{
							}
							text = string.Concat(new object[]
							{
								text,
								"_",
								@int,
								"_",
								mid
							});
						}
					}
					bool flag7 = missionGoal.ContainsKey("ownitm");
					if (flag7)
					{
						Variant variant2 = missionGoal["ownitm"][0];
						bool flag8 = variant2 != null;
						if (flag8)
						{
							string text2 = this.get_need_item_str(variant2, variant["id"]._int);
						}
					}
					bool flag9 = missionGoal.ContainsKey("colitm");
					if (flag9)
					{
						Variant variant2 = missionGoal["colitm"][0];
						bool flag10 = variant2 != null;
						if (flag10)
						{
							string text3 = this.get_need_item_str(variant2, variant["id"]._int);
						}
					}
					bool flag11 = missionGoal.ContainsKey("enterlvl");
					if (flag11)
					{
						string text4 = "enterlvl_" + missionGoal["enterlvl"]._str;
					}
					bool flag12 = missionGoal.ContainsKey("jcamp");
					if (flag12)
					{
					}
					bool flag13 = missionGoal.ContainsKey("qa");
					if (flag13)
					{
						this.to_npc_open_mis(mid, missionGoal["qa"]["npc"]._int, false);
					}
					else
					{
						bool flag14 = missionGoal.ContainsKey("talknpc");
						if (flag14)
						{
							bool flag15 = this.is_mis_complete(mid);
							if (flag15)
							{
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
		}

		private void findway_process()
		{
		}

		public void auto_to_npc_open_mis(int mid, int npc_id)
		{
		}

		public void to_npc_open_mis(int mid, int npc_id, bool ignore_level = false)
		{
		}

		private void requestRmisInfo(int id)
		{
			bool flag = id != 0;
			if (flag)
			{
				(base.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetRmisInfo(id);
			}
		}

		public void initRmisData()
		{
			Variant rmiss = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_rmiss();
			bool flag = rmiss != null;
			if (flag)
			{
				for (int i = 0; i < rmiss.Count; i++)
				{
					Variant variant = rmiss[i];
					Variant variant2 = this._rmisConfData[variant["id"]._str];
					bool flag2 = variant2 == null;
					if (flag2)
					{
						this._rmisConfData[variant["id"]._str] = variant;
					}
					else
					{
						foreach (string current in variant.Keys)
						{
							variant2[current] = variant[current];
						}
					}
				}
			}
		}

		public void InitPlayerRmisData()
		{
			Variant mainPlayerInfo = (base.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			this._reSetPlaymis = true;
			this._reSetShare = true;
			foreach (string current in this._rmisConfData.Keys)
			{
				Variant variant = this._rmisConfData[current];
				this.requestRmisInfo(variant["id"]._int);
			}
			Variant playerAcceptedRmis = this.GetPlayerAcceptedRmis();
			for (int i = 0; i < playerAcceptedRmis.Count; i++)
			{
				this.requestRmisInfo(playerAcceptedRmis[i]._int);
			}
			(base.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetAppawd();
			(base.g_mgr.g_netM as muNetCleint).igMissionMsgs.GetRmisShareInfo();
		}

		public static bool IsArrayHasValue(Variant data, int value)
		{
			bool flag = data.Count <= 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (Variant current in data._arr)
				{
					bool flag2 = value == current._int;
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

		public Variant GetPlayerAcceptedRmis()
		{
			Variant variant = new Variant();
			bool flag = this.misacept.Count > 0;
			if (flag)
			{
				foreach (Variant current in this.misacept.Values)
				{
					Variant variant2 = current["configdata"];
					bool flag2 = variant2 == null;
					if (!flag2)
					{
						bool flag3 = variant2.ContainsKey("rmis");
						if (flag3)
						{
							variant.pushBack(variant2["rmis"]);
						}
					}
				}
			}
			return variant;
		}

		public Variant GetRmisActivities()
		{
			Variant variant = new Variant();
			foreach (Variant current in this._rmisConfData._arr)
			{
				bool flag = current.ContainsKey("part_type") && (current["part_type"] & 8) > 0;
				if (flag)
				{
					variant.pushBack(current);
				}
			}
			return variant;
		}

		public bool IsRmisCanAccept(int rmisId)
		{
			bool result = false;
			Variant variant = this._playerRmis[rmisId.ToString()];
			bool flag = variant != null;
			if (flag)
			{
				Variant variant2 = this._rmisConfData[rmisId.ToString()];
				int num = 0;
				bool flag2 = variant2.ContainsKey("rmis_share");
				if (flag2)
				{
					Variant variant3 = this._rmis_share[variant2["rmis_share"]];
					bool flag3 = variant3 != null;
					if (flag3)
					{
						num = variant3["cnt"];
					}
					else
					{
						Variant rmisShare = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.GetRmisShare(variant2["rmis_share"]);
						bool flag4 = rmisShare != null;
						if (flag4)
						{
							num = rmisShare["dalycnt"];
						}
					}
				}
				else
				{
					num = variant["cnt"];
				}
				bool flag5 = num > 0;
				if (flag5)
				{
					Variant variant4 = this._rmisConfData[rmisId.ToString()];
					Variant variant5 = variant4["clan"];
					result = (variant5 == null);
				}
			}
			return result;
		}

		public int GetLeftFreeInsure()
		{
			return this._freeInsure;
		}

		public int GetPlayerRmisCurrQualMis(int rmisId)
		{
			int result = 0;
			Variant variant = this._playerRmis[rmisId.ToString()];
			bool flag = variant != null;
			if (flag)
			{
				result = variant["misids"][0];
			}
			return result;
		}

		public Variant GetRmisQualIcon(int rmisid)
		{
			Variant variant = null;
			Variant variant2 = this._rmisConfData[rmisid.ToString()];
			bool flag = variant2 != null;
			if (flag)
			{
				variant = new Variant();
				foreach (Variant current in variant2["rqual"].Values)
				{
					foreach (Variant current2 in current["qual_grp"].Values)
					{
						variant.pushBack("rmis_icon_" + current2["qual"]);
					}
				}
			}
			return variant;
		}

		public Variant GetPlayerRmisInfo(int rmisid)
		{
			Variant variant = null;
			Variant variant2 = null;
			Variant variant3 = this._rmisConfData[rmisid.ToString()];
			Variant variant4 = this._playerRmis[rmisid.ToString()];
			bool flag = variant3 != null && variant4 != null;
			if (flag)
			{
				int @int = variant3["type"]._int;
				if (@int != 1)
				{
					if (@int == 2)
					{
						variant = new Variant();
						variant["misid"] = variant4["misids"][0];
						bool flag2 = variant3.ContainsKey("rmis_share");
						if (flag2)
						{
							Variant variant5 = this._rmis_share[variant3["rmis_share"]];
							bool flag3 = variant5 == null;
							if (flag3)
							{
								variant5 = new Variant();
								Variant rmisShare = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.GetRmisShare(variant3["rmis_share"]);
								bool flag4 = rmisShare != null;
								if (flag4)
								{
									variant5["cnt"] = rmisShare["dalycnt"];
									variant5["fincnt"] = 0;
									variant5["dalyawd"] = false;
								}
								this._rmis_share[variant3["rmis_share"]] = variant5;
							}
							variant["cnt"] = variant5["cnt"];
							variant["fincnt"] = variant5["fincnt"];
							variant["dalyawd"] = variant5["dalyawd"];
						}
						else
						{
							variant["cnt"] = variant4["cnt"];
							variant["fincnt"] = variant4["fincnt"];
							variant["dalyawd"] = variant4["dalyawd"];
						}
						Variant variant6 = new Variant();
						Variant variant7 = new Variant();
						foreach (Variant current in variant4["rqual"]._arr)
						{
							Variant objectBykeyValue = this.getObjectBykeyValue(variant3["rqual"], "id", current["id"]._int);
							foreach (Variant current2 in objectBykeyValue["qual_grp"]._arr)
							{
								bool flag5 = current2 != null && current2["qual"]._int == current["qual"]._int;
								if (flag5)
								{
									variant2 = current2;
									break;
								}
							}
							bool flag6 = objectBykeyValue.ContainsKey("reflushmis");
							if (flag6)
							{
								variant7 = objectBykeyValue;
								foreach (string current3 in variant2.Keys)
								{
									variant7[current3] = variant2[current3];
								}
							}
							else
							{
								variant6 = objectBykeyValue;
								foreach (string current4 in variant2.Keys)
								{
									variant6[current4] = variant2[current4];
								}
							}
						}
						variant["appawd"] = variant6;
						variant["appgoal"] = variant7;
						variant["type"] = variant3["type"];
					}
				}
				else
				{
					variant = new Variant();
					bool flag7 = variant3.ContainsKey("rmis_share");
					if (flag7)
					{
						Variant variant5 = this._rmis_share[variant3["rmis_share"]];
						bool flag8 = variant5 == null;
						if (flag8)
						{
							variant5 = new Variant();
							Variant rmisShare = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.GetRmisShare(variant3["rmis_share"]);
							bool flag9 = rmisShare != null;
							if (flag9)
							{
								variant5["cnt"] = rmisShare["dalycnt"];
								variant5["fincnt"] = 0;
							}
							this._rmis_share[variant3["rmis_share"]] = variant5;
						}
						variant["cnt"] = variant5["cnt"];
						variant["fincnt"] = variant5["fincnt"];
					}
					else
					{
						variant["cnt"] = variant4["cnt"];
						variant["fincnt"] = variant4["fincnt"];
					}
					variant["misid"] = variant4["misids"][0];
					variant["tm"] = variant4["rtm"];
					Variant variant8 = variant4["rqual"][0];
					variant["rqualid"] = variant8["id"];
					variant["failcnt"] = variant8["failcnt"];
					variant["freecnt"] = variant8["freecnt"];
					variant["qual"] = variant8["qual"];
					Variant variant9 = variant3["rqual"][variant8["id"]];
					variant2 = variant9["qual_grp"][variant8["qual"]];
					variant["yb"] = variant2["ryb"];
					variant["percent"] = variant2["uprate"];
					bool flag10 = variant8["failcnt"] > variant2["failcnt"];
					if (flag10)
					{
						variant["addper"] = (variant8["failcnt"] - variant2["failcnt"]) * variant2["failper"];
					}
					else
					{
						variant["addper"] = 0;
					}
					variant["type"] = variant3["type"];
				}
			}
			return variant;
		}

		public Variant getObjectBykeyValue(Variant data, string key, Variant value)
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

		public Variant GetRmisDesc(int rmisid)
		{
			return this._rmisConfData[rmisid.ToString()];
		}

		public void onCompleteRmis(int misid)
		{
			bool flag = this._appawdRmis != null;
			if (flag)
			{
				bool flag2 = this._appawdRmis[misid.ToString()] != null;
				if (flag2)
				{
					this._appawdRmis[misid.ToString()] = null;
				}
				Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mission_conf(misid);
				Variant variant2 = this._playerRmis[variant["rmis"].ToString()];
				bool flag3 = variant != null && variant2 != null;
				if (flag3)
				{
					Variant variant3 = this._rmisConfData[variant["rmis"]._str];
					bool flag4 = variant3.ContainsKey("rmis_share");
					if (flag4)
					{
						Variant variant4 = this._rmis_share[variant3["rmis_share"]];
						bool flag5 = variant4;
						if (flag5)
						{
							Variant expr_E6 = variant4;
							Variant val = expr_E6["fincnt"];
							expr_E6["fincnt"] = val + 1;
						}
					}
					else
					{
						Variant expr_12D = this._playerRmis[variant["rmis"].ToString()];
						Variant val = expr_12D["fincnt"];
						expr_12D["fincnt"] = val + 1;
					}
				}
			}
		}

		public void onAcceptRmisMis(int misid)
		{
			Variant variant = this._rmisConfData[misid.ToString()];
			bool flag = variant.ContainsKey("rmis_share");
			if (flag)
			{
				Variant variant2 = this._rmis_share[variant["rmis_share"]];
				bool flag2 = variant2;
				if (flag2)
				{
					Variant expr_47 = variant2;
					Variant val = expr_47["cnt"];
					expr_47["cnt"] = val - 1;
				}
			}
		}

		public int GetRmisAppwdExp(int rmisid, int misid)
		{
			Variant rmisDesc = this.GetRmisDesc(rmisid);
			bool flag = rmisDesc != null && rmisDesc.ContainsKey("appawd");
			int result;
			if (flag)
			{
				Variant appawdRmis = this.GetAppawdRmis(misid);
				bool flag2 = appawdRmis != null;
				if (flag2)
				{
					Variant variant = (base.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.get_mis_appawd(rmisDesc["appawd"]);
					bool flag3 = variant != null;
					if (flag3)
					{
						Variant variant2 = variant["qual_grp"];
						int @int = appawdRmis["qual"]._int;
						bool flag4 = @int > 0 && variant2 != null && variant2.Count >= @int - 1;
						if (flag4)
						{
							result = variant2[@int - 1]["awdper"];
							return result;
						}
					}
				}
			}
			result = 0;
			return result;
		}

		public Variant GetAppawdRmis(int misid)
		{
			return (this._appawdRmis != null) ? this._appawdRmis[misid.ToString()] : null;
		}

		public void onRmisInfoRes(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data == null;
			if (!flag)
			{
				int num = 0;
				bool flag2 = data.ContainsKey("id");
				if (flag2)
				{
					num = data["id"];
				}
				bool flag3 = false;
				Variant variant = this._rmisConfData[num.ToString()];
				Variant rmis_fin = (base.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.rmis_fin;
				switch (data["tp"]._int)
				{
				case 1:
				{
					bool reSetPlaymis = this._reSetPlaymis;
					if (reSetPlaymis)
					{
						this._reSetPlaymis = false;
						this._playerRmis = new Variant();
					}
					Variant variant2 = this._playerRmis[num.ToString()];
					this._playerRmis[num.ToString()] = data;
					bool flag4 = variant2 == null;
					if (flag4)
					{
						this.missionChange(0);
					}
					else
					{
						int @int = variant["type"]._int;
						if (@int != 1)
						{
							if (@int != 2)
							{
							}
						}
					}
					break;
				}
				case 2:
				{
					Variant variant2 = this._playerRmis[num.ToString()];
					bool flag5 = variant2 != null;
					if (flag5)
					{
						foreach (string current in data.Keys)
						{
							bool flag6 = current != "rqual";
							if (flag6)
							{
								variant2[current] = data[current];
							}
						}
						foreach (Variant current2 in variant2["rqual"].Values)
						{
							Variant variant3 = data["rqual"];
							bool flag7 = current2["id"] == variant3["id"];
							if (flag7)
							{
								foreach (string current3 in variant3.Keys)
								{
									current2[current3] = variant3[current3];
								}
							}
						}
						int int2 = variant["type"]._int;
						if (int2 != 1)
						{
							if (int2 != 2)
							{
							}
						}
					}
					break;
				}
				case 8:
				{
					bool flag8 = data.ContainsKey("mis_append") && data["mis_append"].Count > 0;
					if (flag8)
					{
						foreach (Variant current4 in data["mis_append"]._arr)
						{
							int num2 = current4["misid"];
							bool flag9 = this._appawdRmis == null;
							if (flag9)
							{
								this._appawdRmis = new Variant();
							}
							this._appawdRmis[num2.ToString()] = current4;
						}
					}
					break;
				}
				case 9:
				{
					bool flag10 = this._playerRmis[num.ToString()] == null;
					if (flag10)
					{
						this._playerRmis[num.ToString()] = new Variant();
					}
					Variant variant2 = this._playerRmis[num.ToString()];
					foreach (string current5 in data.Keys)
					{
						variant2[current5] = data[current5];
					}
					int int3 = variant["type"]._int;
					if (int3 != 1)
					{
						if (int3 != 2)
						{
						}
					}
					break;
				}
				case 10:
				{
					bool flag11 = this._playerRmis[num.ToString()] == null;
					if (flag11)
					{
						this._playerRmis[num.ToString()] = new Variant();
					}
					Variant variant2 = this._playerRmis[num.ToString()];
					foreach (Variant current6 in rmis_fin.Values)
					{
						bool flag12 = current6["rid"] == num;
						if (flag12)
						{
							Variant expr_4D0 = current6;
							Variant val = expr_4D0["fcnt"];
							expr_4D0["fcnt"] = val + 1;
							flag3 = true;
							break;
						}
					}
					bool flag13 = !flag3 && rmis_fin;
					if (flag13)
					{
						Variant variant4 = new Variant();
						variant4["rid"] = num;
						variant4["fcnt"] = 1;
						rmis_fin.pushBack(variant4);
					}
					variant2["dalyawd"] = data["dalyawd"];
					int int4 = variant["type"]._int;
					if (int4 != 1)
					{
						if (int4 != 2)
						{
						}
					}
					break;
				}
				case 11:
				{
					bool reSetShare = this._reSetShare;
					if (reSetShare)
					{
						this._reSetShare = false;
						this._rmis_share = new Variant();
					}
					foreach (Variant current7 in rmis_fin._arr)
					{
						bool flag14 = current7 != null && current7["rid"] == num;
						if (flag14)
						{
							Variant expr_609 = current7;
							Variant val = expr_609["fcnt"];
							expr_609["fcnt"] = val + 1;
							flag3 = true;
							break;
						}
					}
					bool flag15 = !flag3 && rmis_fin != null;
					if (flag15)
					{
						Variant variant4 = new Variant();
						variant4["rid"] = num;
						variant4["fcnt"] = 1;
						rmis_fin.pushBack(variant4);
					}
					Variant variant5 = data["rmis_share"];
					bool flag16 = variant5.Count > 0;
					if (flag16)
					{
						foreach (Variant current8 in variant5._arr)
						{
							this._rmis_share[current8["id"]] = current8;
							foreach (Variant current9 in this._rmisConfData.Values)
							{
								bool flag17 = current9.ContainsKey("rmis_share") && current9["rmis_share"] == current8["id"];
								if (flag17)
								{
									int int5 = current9["type"]._int;
									if (int5 != 1)
									{
										if (int5 != 2)
										{
										}
									}
									break;
								}
							}
						}
					}
					break;
				}
				}
			}
		}

		public void onGmisInfoRes(GameEvent e)
		{
			Variant data = e.data;
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

		public void onGmisAwdRes(GameEvent e)
		{
			Variant data = e.data;
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
	}
}
