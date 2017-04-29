using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class LGGDLevels : lgGDBase
	{
		private Variant _curr_lvl_conf;

		private Variant _cur_level;

		private bool _is_fin = false;

		private int _curlay = 0;

		protected int _lvlsideid;

		private uint _enter_lvl_npc = 0u;

		private uint _current_diff = 0u;

		private int curmapid;

		private Variant _lvl_pvpinfo_board = null;

		private Variant _lvl_clanter_score = null;

		private Variant _transdata = null;

		private Variant _mapMonObj = new Variant();

		private Variant _kumiteply = new Variant();

		private Variant _kumrs;

		private Variant _kumre;

		private Variant _curr_round_info = new Variant();

		private Variant _sideclan;

		private Variant _kmtrigs = null;

		private Variant _matchLvlInfo;

		protected Variant _associate_lvls = new Variant();

		protected Variant _entercds = new Variant();

		private Variant _lvl_infos = new Variant();

		private Variant _no_entry_lvl = new Variant();

		private Variant _enters = new Variant();

		private int _curltpid = 0;

		private Variant _awdData;

		private Variant _lvlprizes = new Variant();

		private Variant _hadCost;

		private Dictionary<uint, Variant> _clanTerritorys = new Dictionary<uint, Variant>();

		private Variant _loadTerState = new Variant();

		private Action<Variant> _clanInfoCall;

		private Variant _carrchief_info = new Variant();

		private Variant _carrchief_info_cb = new Variant();

		private Variant _carrchief_npc_id = new Variant();

		private Variant _lvlmisData;

		private Variant _record = new Variant();

		private Variant _loading = new Variant();

		private int _curWaitTm;

		private Variant _lvlshare = new Variant();

		public bool in_level
		{
			get
			{
				return this._cur_level != null;
			}
		}

		public int current_lvl
		{
			get
			{
				return this._curltpid;
			}
		}

		public uint current_diff
		{
			get
			{
				return this._current_diff;
			}
		}

		private InGameLevelMsgs igLevelMsg
		{
			get
			{
				return (this.g_mgr.g_netM as muNetCleint).getObject("MSG_LEVEL") as InGameLevelMsgs;
			}
		}

		private InGameMissionMsgs igMissionMsg
		{
			get
			{
				return this.muNClt.getObject("MSG_MISSION") as InGameMissionMsgs;
			}
		}

		private muCLientConfig muCCfg
		{
			get
			{
				return this.g_mgr.g_gameConfM as muCLientConfig;
			}
		}

		private muNetCleint muNClt
		{
			get
			{
				return this.g_mgr.g_netM as muNetCleint;
			}
		}

		private muLGClient muLgClt
		{
			get
			{
				return this.g_mgr.g_gameM as muLGClient;
			}
		}

		private LGIUILevel uiLevel
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("UI_LEVEL") as LGIUILevel;
			}
		}

		private LGIUIMainUI lgmainui
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			}
		}

		private LGIUINpcDialog ui_npcDialog
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUINpcDialogImpl") as LGIUINpcDialog;
			}
		}

		private LGIUIScriptEnter ui_enter
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("scriptEnter") as LGIUIScriptEnter;
			}
		}

		private LGIUIMission mission
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("UI_MISSION") as LGIUIMission;
			}
		}

		private LGIUIScriptActivity ui_scriptAct
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("scriptActivity") as LGIUIScriptActivity;
			}
		}

		public LGGDLevels(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGDLevels(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListener(5040u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(233u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(234u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(236u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(237u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(238u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(239u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(240u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(241u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(242u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(243u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(244u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(245u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(246u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(247u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(248u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(249u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(250u, new Action<GameEvent>(this.switchFunc));
			this.g_mgr.g_netM.addEventListener(230u, new Action<GameEvent>(this.switchFunc));
		}

		private void switchFunc(GameEvent e)
		{
			Variant variant = e.data;
			bool flag = variant.ContainsKey("case");
			if (flag)
			{
				string str = variant["case"]._str;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(str);
				if (num <= 2550941093u)
				{
					if (num <= 2164309073u)
					{
						if (num <= 579178283u)
						{
							if (num != 205695533u)
							{
								if (num == 579178283u)
								{
									if (str == "on_lvl_err_msg")
									{
										this.on_lvl_err_msg(variant["data"]);
										goto IL_4A4;
									}
								}
							}
							else if (str == "enter_scritp")
							{
								variant = variant["data"];
								this.enter_scritp(variant["ltpid"], variant["npcid"], variant["diff"], false, 0);
								goto IL_4A4;
							}
						}
						else if (num != 2000863509u)
						{
							if (num == 2164309073u)
							{
								if (str == "on_leave_lvl")
								{
									this.on_leave_lvl();
									goto IL_4A4;
								}
							}
						}
						else if (str == "on_create_lvl_res")
						{
							this.on_create_lvl_res(variant["data"]);
							goto IL_4A4;
						}
					}
					else if (num <= 2242945449u)
					{
						if (num != 2190674961u)
						{
							if (num == 2242945449u)
							{
								if (str == "get_associate_lvls_res")
								{
									this.get_associate_lvls_res(variant["data"]);
									goto IL_4A4;
								}
							}
						}
						else if (str == "lvl_pvpinfo_board_msg")
						{
							this.lvl_pvpinfo_board_msg(variant["data"]);
							goto IL_4A4;
						}
					}
					else if (num != 2394357983u)
					{
						if (num != 2423944544u)
						{
							if (num == 2550941093u)
							{
								if (str == "mod_lvl_selfpvpinfo")
								{
									this.mod_lvl_selfpvpinfo(variant["data"]);
									goto IL_4A4;
								}
							}
						}
						else if (str == "on_lvl_broadcast_res")
						{
							this.on_lvl_broadcast_res(variant["data"]);
							goto IL_4A4;
						}
					}
					else if (str == "on_close_lvl_res")
					{
						this.on_close_lvl_res(variant["data"]);
						goto IL_4A4;
					}
				}
				else if (num <= 3209656004u)
				{
					if (num <= 3101052162u)
					{
						if (num != 2558103454u)
						{
							if (num == 3101052162u)
							{
								if (str == "on_lvl_km")
								{
									this.on_lvl_km(variant["data"]);
									goto IL_4A4;
								}
							}
						}
						else if (str == "SetKillMonTrigs")
						{
							this.SetKillMonTrigs(variant["data"]);
							goto IL_4A4;
						}
					}
					else if (num != 3185258305u)
					{
						if (num == 3209656004u)
						{
							if (str == "on_battle_do_res")
							{
								this.on_battle_do_res(variant["data"]);
								goto IL_4A4;
							}
						}
					}
					else if (str == "on_clanter_res")
					{
						this.on_clanter_res(variant["data"]);
						goto IL_4A4;
					}
				}
				else if (num <= 3777560420u)
				{
					if (num != 3645987490u)
					{
						if (num == 3777560420u)
						{
							if (str == "on_lvl_res")
							{
								this.on_lvl_res(variant["data"]);
								goto IL_4A4;
							}
						}
					}
					else if (str == "on_lvl_side_info")
					{
						this.on_lvl_side_info(variant["data"]);
						goto IL_4A4;
					}
				}
				else if (num != 3888840524u)
				{
					if (num != 3918221701u)
					{
						if (num == 4030461539u)
						{
							if (str == "on_check_in_lvl_res")
							{
								this.on_check_in_lvl_res(variant["data"]);
								goto IL_4A4;
							}
						}
					}
					else if (str == "on_enter_lvl_res")
					{
						this.on_enter_lvl_res(variant["data"]);
						goto IL_4A4;
					}
				}
				else if (str == "get_lvlmis_res")
				{
					this.get_lvlmis_res(variant["data"]);
					goto IL_4A4;
				}
				GameTools.PrintNotice("switchFunc defanult");
				IL_4A4:;
			}
			else
			{
				GameTools.PrintNotice("switchFunc no case");
			}
		}

		public bool IsNeedLvl(Variant tpids)
		{
			return this.in_level && tpids != null && tpids._arr.IndexOf(this._curltpid) >= 0;
		}

		public bool InMultiLvl()
		{
			bool flag = this._cur_level != null;
			bool flag2 = flag;
			if (flag2)
			{
				flag = false;
				Variant multiLevel = this.muCCfg.svrLevelConf.GetMultiLevel();
				foreach (Variant current in multiLevel._arr)
				{
					bool flag3 = current["tpid"] == this._curltpid;
					if (flag3)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		public bool in_travel_script()
		{
			bool flag = this._cur_level != null;
			return flag && this.muCCfg.svrLevelConf.get_levelmis_data(this._cur_level["ltpid"]) != null;
		}

		public Variant get_curr_lvl_info()
		{
			return this._cur_level;
		}

		public Variant get_curr_lvl_conf()
		{
			return this._curr_lvl_conf;
		}

		public int get_lvlsideid()
		{
			return this._lvlsideid;
		}

		public int GetKillCount()
		{
			int num = 0;
			bool flag = this._cur_level.ContainsKey("score_km");
			if (flag)
			{
				foreach (Variant current in this._cur_level["score_km"]._arr)
				{
					Variant val = this.muCCfg.svrMonsterConf.get_monster_data(current["mid"]);
					bool flag2 = val;
					if (flag2)
					{
						num += current["cnt"];
					}
				}
			}
			return num;
		}

		public bool is_currlvl_ignore_team()
		{
			bool flag = this._curr_lvl_conf;
			bool result;
			if (flag)
			{
				bool flag2 = this._curr_lvl_conf.ContainsKey("pvp");
				if (flag2)
				{
					result = (this._curr_lvl_conf["pvp"][0]["ignore_team"] == 1);
					return result;
				}
			}
			result = false;
			return result;
		}

		public bool is_currlvl_map_ignore_side(int mapid)
		{
			bool flag = this._curr_lvl_conf;
			bool result;
			if (flag)
			{
				Variant variant = this._curr_lvl_conf["map"];
				bool flag2 = variant != null;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag3 = mapid == current["id"];
						if (flag3)
						{
							result = (current["ignore_side"] == 1);
							return result;
						}
					}
				}
			}
			result = false;
			return result;
		}

		public bool is_currlvl_ignore_clan()
		{
			bool flag = this._curr_lvl_conf;
			bool result;
			if (flag)
			{
				bool flag2 = this._curr_lvl_conf.ContainsKey("pvp");
				if (flag2)
				{
					result = (this._curr_lvl_conf["pvp"][0]["ignore_clan"] == 1);
					return result;
				}
			}
			result = false;
			return result;
		}

		public uint get_lvl_max_difflvl(Variant lvl_conf)
		{
			uint num = 1u;
			Variant variant = lvl_conf["diff_lvl"];
			bool flag = variant != null;
			if (flag)
			{
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["lv"] > num;
					if (flag2)
					{
						num = current["lv"];
					}
				}
			}
			return num;
		}

		public void enter_scritp(uint ltpid, int npcid = 0, int diff = 1, bool inroom = false, int cost_tp = 0)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrLevelConf.get_level_data(ltpid);
			bool flag = variant == null;
			if (!flag)
			{
				this._enter_lvl_npc = (uint)npcid;
				bool flag2 = variant.ContainsKey("cltid");
				if (!flag2)
				{
					bool flag3 = variant.ContainsKey("cltwarid");
					if (!flag3)
					{
						bool flag4 = variant.ContainsKey("slvl_diff");
						if (flag4)
						{
							this.check_in_slvl_diff(variant["slvl_diff"]._uint, (uint)diff, (uint)cost_tp);
						}
						else
						{
							bool flag5 = variant["lctp"] == 1;
							if (flag5)
							{
								Variant variant2 = this.get_lvl_llid(ltpid);
								bool flag6 = variant2 == null;
								if (flag6)
								{
									this.create_lvl(ltpid, (uint)diff, (uint)cost_tp);
								}
								else
								{
									this.enter_lvl(variant2["llid"], variant2["ltpid"], 0u, false, 0u);
								}
							}
							else
							{
								bool flag7 = variant["lctp"] == 2;
								if (flag7)
								{
									bool flag8 = variant.ContainsKey("carrchief");
									if (flag8)
									{
										this.check_in_carrchief(variant["carrchief"]);
									}
									else
									{
										this.check_in_lvl(ltpid);
									}
								}
							}
						}
					}
				}
			}
		}

		private void check_in_slvl_diff(uint slvl_diff, uint diff_lvl, uint cost_tp)
		{
			this.igLevelMsg.check_in_lvl(GameTools.createGroup(new Variant[]
			{
				"slvl_diff",
				slvl_diff,
				"diff_lvl",
				diff_lvl,
				"cost_tp",
				cost_tp
			}));
		}

		private void check_in_lvl(uint ltpid)
		{
			this.igLevelMsg.check_in_lvl(GameTools.createGroup(new Variant[]
			{
				"ltpid",
				ltpid
			}));
		}

		private void check_in_carrchief(uint carrid)
		{
			this.igLevelMsg.check_in_lvl(GameTools.createGroup(new Variant[]
			{
				"carrc",
				carrid
			}));
		}

		public void check_in_arena(uint arenaid, bool chlvl, bool cancel = false)
		{
			this.igLevelMsg.check_in_lvl(GameTools.createGroup(new Variant[]
			{
				"arenaid",
				arenaid,
				"chlvl",
				chlvl,
				"cancel",
				cancel
			}));
		}

		public void check_in_arenaex(uint arenaexid, bool cancel = false)
		{
			this.igLevelMsg.check_in_lvl(GameTools.createGroup(new Variant[]
			{
				"arenaexid",
				arenaexid,
				"cancel",
				cancel
			}));
		}

		private void check_in_clte(uint clteid, uint lvltpid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrLevelConf.get_clan_territory(clteid);
			bool flag = variant;
			if (flag)
			{
				string str = variant["tp"]._str;
				if (!(str == "1"))
				{
					if (str == "2")
					{
						bool flag2 = variant["warlvl"]["tpid"] == lvltpid;
						if (flag2)
						{
							float num = (float)this.muNClt.CurServerTimeStampMS;
							bool flag3 = ConfigUtil.check_tm((double)num, variant["warlvl"]["tmchk"][0], 0.0, 0.0);
							if (flag3)
							{
								this.igLevelMsg.check_in_lvl(GameTools.createGroup(new Variant[]
								{
									"clteid",
									clteid,
									"war",
									true
								}));
							}
							else
							{
								this.on_lvl_err_msg(GameTools.createGroup(new Variant[]
								{
									"res",
									-411
								}));
							}
						}
						else
						{
							Variant variant2 = null;
							bool flag4 = variant2 && variant2["clanid"] == this.IsClanHasClte(clteid);
							if (flag4)
							{
								this.igLevelMsg.check_in_lvl(GameTools.createGroup(new Variant[]
								{
									"clteid",
									clteid,
									"war",
									false
								}));
							}
						}
					}
				}
				else
				{
					this.igLevelMsg.check_in_lvl(GameTools.createGroup(new Variant[]
					{
						"clteid",
						clteid,
						"war",
						false
					}));
				}
			}
		}

		public void on_check_in_lvl_res(Variant data)
		{
		}

		private void create_lvl(uint ltpid, uint diff_lvl, uint cost_tp = 0u)
		{
			Variant variant = new Variant();
			variant["npcid"] = this._enter_lvl_npc;
			variant["ltpid"] = ltpid;
			variant["diff_lvl"] = diff_lvl;
			bool flag = cost_tp > 0u;
			if (flag)
			{
				variant["cost_tp"] = cost_tp;
			}
			this.igLevelMsg.create_lvl(variant);
		}

		public void Create_script(uint ltpid, int npcid = 0, int diff_lvl = 1, uint cost_tp = 0u)
		{
			Variant variant = new Variant();
			variant["npcid"] = npcid;
			variant["ltpid"] = ltpid;
			variant["diff_lvl"] = diff_lvl;
			bool flag = cost_tp > 0u;
			if (flag)
			{
				variant["cost_tp"] = cost_tp;
			}
			Variant variant2 = new Variant();
			variant2["npcid"] = npcid;
			variant2["ltpid"] = ltpid;
			variant2["diff_lvl"] = diff_lvl;
			this.igLevelMsg.create_lvl(variant2);
		}

		public void on_create_lvl_res(Variant data)
		{
			LGUIMainUIImpl_NEED_REMOVE.TO_LEVEL = true;
			this._add_lvl_llid(data);
			bool flag = data["lmtp"] != 2;
			if (flag)
			{
				bool flag2 = data["creator_cid"] != null;
				if (flag2)
				{
					uint llid = data.ContainsKey("llid") ? data["llid"]._uint : 0u;
					uint diff_lvl = data.ContainsKey("diff_lvl") ? data["diff_lvl"]._uint : 1u;
					uint cost_tp = data.ContainsKey("cost_tp") ? data["cost_tp"]._uint : 0u;
					Variant variant = SvrLevelConfig.instacne.get_level_data(data["ltpid"]);
					this.curmapid = variant["map"][0]["id"];
					this.enter_lvl(llid, data["ltpid"]._uint, diff_lvl, false, cost_tp);
				}
				else
				{
					bool flag3 = data["lmtp"] == 4;
					if (flag3)
					{
						int num = 0;
						bool flag4 = num == data["ltpid"];
						if (flag4)
						{
							this.enter_lvl(data["llid"], data["ltpid"]._uint, 0u, false, 0u);
						}
					}
				}
			}
			else
			{
				uint llid2 = data.ContainsKey("llid") ? data["llid"]._uint : 0u;
				uint diff_lvl2 = data.ContainsKey("diff_lvl") ? data["diff_lvl"]._uint : 1u;
				uint cost_tp2 = data.ContainsKey("cost_tp") ? data["cost_tp"]._uint : 0u;
				Variant variant2 = SvrLevelConfig.instacne.get_level_data(data["ltpid"]);
				this.curmapid = variant2["map"][0]["id"];
				this.enter_lvl(llid2, data["ltpid"]._uint, diff_lvl2, false, cost_tp2);
			}
			this.g_mgr.dispatchEvent(GameEvent.Create(5039u, this, GameTools.CreateSwitchData("OnCreateLevel", data), false));
		}

		private void enter_lvl(uint llid, uint ltpid, uint diff_lvl = 0u, bool bslvl = false, uint cost_tp = 0u)
		{
			Variant variant = GameTools.createGroup(new Variant[]
			{
				"tp",
				1,
				"npcid",
				this._enter_lvl_npc,
				"llid",
				llid,
				"bslvl",
				bslvl
			});
			bool flag = diff_lvl > 0u;
			if (flag)
			{
				variant["diff_lvl"] = diff_lvl;
			}
			bool flag2 = cost_tp > 0u;
			if (flag2)
			{
				variant["cost_tp"] = cost_tp;
			}
			variant["mapid"] = this.curmapid;
			variant["ltpid"] = ltpid;
			this.curmapid = 0;
			this.igLevelMsg.enter_lvl(variant);
		}

		public void changeLvl()
		{
			Variant data = GameTools.createGroup(new Variant[]
			{
				"tp",
				2
			});
			this.igLevelMsg.enter_lvl(data);
		}

		public void on_enter_lvl_res(Variant data)
		{
			ModelBase<PlayerModel>.getInstance().inFb = true;
			this._cur_level = data;
			this._current_diff = data["diff_lvl"];
			Variant variant = this.muCCfg.svrLevelConf.get_level_data(this._cur_level["ltpid"]);
			this._curr_lvl_conf = variant;
			this._curltpid = this._cur_level["ltpid"];
			debug.Log(string.Concat(new object[]
			{
				"!!enter_lvl_res!!  _curltpid:",
				this._curltpid,
				" ",
				debug.count
			}));
			bool flag = this._lvlsideid != 0;
			if (flag)
			{
				bool flag2 = data.ContainsKey("bslvl");
				if (flag2)
				{
				}
			}
			string str = this.muCCfg.localGeneral.GetCommonConf("enterDontStop")._str;
			Variant variant2 = GameTools.split(str, ",", 1u);
			bool flag3 = variant2._arr.IndexOf(this._curltpid) < 0;
			if (flag3)
			{
			}
			this._update_level_ctnleft(variant["tpid"], data.ContainsKey("cntleft") ? data["cntleft"]._int : 0);
			uint num = 0u;
			while ((ulong)num < (ulong)((long)this._associate_lvls.Count))
			{
				Variant variant3 = this._associate_lvls[(int)num];
				bool flag4 = variant3["llid"]._int == this._cur_level["llid"]._int;
				if (flag4)
				{
					variant3["end_tm"] = data["end_tm"];
					break;
				}
				num += 1u;
			}
			bool flag5 = data.ContainsKey("cur_round");
			if (flag5)
			{
				this._curr_round_info["cur_round"] = data["cur_round"];
			}
			bool flag6 = data.ContainsKey("round_tm");
			if (flag6)
			{
				this._curr_round_info["round_tm"] = data["round_tm"];
			}
			bool flag7 = data.ContainsKey("preptm");
			if (flag7)
			{
				this._curr_round_info["preptm"] = data["preptm"];
			}
			bool flag8 = data.ContainsKey("ghostcnt");
			if (flag8)
			{
				this._curr_round_info["ghostcnt"] = data["ghostcnt"];
			}
			bool flag9 = data.ContainsKey("sideclan");
			if (flag9)
			{
				this._set_sideclan(data["sideclan"]);
				data.RemoveKey("sideclan");
			}
			bool flag10 = this._curr_lvl_conf.ContainsKey("pvp");
			if (flag10)
			{
				bool flag11 = this._curr_lvl_conf.ContainsKey("arenaid") || this._curr_lvl_conf.ContainsKey("arenaexid");
				if (flag11)
				{
					bool flag12 = data["bslvl"];
					if (flag12)
					{
						this._curWaitTm = 10;
						this._rmvCarrLimitState();
					}
				}
			}
			bool flag13 = data.ContainsKey("kumiteply");
			if (flag13)
			{
				this._set_kumite_plys(data["kumiteply"]);
				data.RemoveKey("kumiteply");
			}
			bool flag14 = data.ContainsKey("cltwar");
			if (flag14)
			{
				this._lvl_clanter_score = data["cltwar"];
				data.RemoveKey("cltwar");
			}
			bool flag15 = this.uiLevel != null;
			if (flag15)
			{
				this.uiLevel.OnEnterLevel(variant, data);
			}
			bool flag16 = data.ContainsKey("kprec");
			if (flag16)
			{
				this._cur_level["kprec"] = data["kprec"];
			}
			else
			{
				bool flag17 = this._cur_level != null && this._curr_lvl_conf["lptp"] == 2;
				if (flag17)
				{
					this._cur_level["kprec"] = GameTools.createGroup(new Variant[]
					{
						"kp",
						0,
						"dc",
						0,
						"ckp",
						0,
						"mdc",
						0,
						"mkp",
						0
					});
				}
			}
			bool flag18 = this._curr_lvl_conf.ContainsKey("map_lay");
			if (flag18)
			{
				this._curlay = 0;
			}
			bool flag19 = this._enters._arr.IndexOf(data["ltpid"]) == -1;
			if (flag19)
			{
				this._enters._arr.Add(data["ltpid"]);
			}
			bool flag20 = this.InMultiLvl();
			if (flag20)
			{
				this.lgmainui.EnterMultilvl();
			}
		}

		private void _rmvCarrLimitState()
		{
			uint carr = 0u;
			Variant variant = null;
			foreach (Variant current in variant._arr)
			{
				bool flag = this.muCCfg.svrGeneralConf.CarrBStateLimit(carr, current["id"]);
				if (flag)
				{
				}
			}
			Variant variant2 = new Variant();
			bool flag2 = variant2.Count > 0;
			if (flag2)
			{
			}
		}

		public void leave_lvl()
		{
			bool flag = this._cur_level == null;
			if (!flag)
			{
				this.igLevelMsg.leave_lvl();
			}
		}

		public void on_leave_lvl()
		{
			ModelBase<PlayerModel>.getInstance().inFb = false;
			bool flag = this._lvlsideid != 0;
			if (flag)
			{
				this._lvlsideid = 0;
			}
			Variant multiLevel = this.muCCfg.svrLevelConf.GetMultiLevel();
			foreach (Variant current in multiLevel._arr)
			{
				bool flag2 = current["tpid"] == this._curltpid;
				if (flag2)
				{
					this.lgmainui.LeaveMultilvl();
					for (int i = 0; i < this._associate_lvls.Count; i++)
					{
						Variant variant = this._associate_lvls[i];
						bool flag3 = variant["llid"] == this._cur_level["llid"];
						if (flag3)
						{
							this._associate_lvls._arr.RemoveAt(i);
							break;
						}
					}
					break;
				}
			}
			this._cur_level = null;
			string str = this.muCCfg.localGeneral.GetCommonConf("leaveDontStop");
			Variant variant2 = GameTools.split(str, ",", 1u);
			bool flag4 = variant2._arr.IndexOf(this._curltpid) < 0;
			if (flag4)
			{
			}
			bool flag5 = this.uiLevel != null;
			if (flag5)
			{
				this.uiLevel.OnLeaveLevel(this._curr_lvl_conf, this._is_fin);
			}
			this._curr_lvl_conf = null;
			this._kumiteply._arr.Clear();
			this._kumrs = null;
			this._kumre = null;
			this._curr_round_info = new Variant();
			this._sideclan = null;
			this._kmtrigs = null;
			this._matchLvlInfo = null;
			this._hadCost = null;
			this._is_fin = false;
		}

		public void on_lvl_fin(Variant data)
		{
			this._is_fin = true;
			this._rmv_lvl_llid(this._cur_level["iid"]);
			this.SetKillMonTrigs(null);
			this._lvl_fin(data);
		}

		private void _lvl_fin(Variant data)
		{
			Variant curr_lvl_conf = this._curr_lvl_conf;
			bool flag = false;
			uint num = 0u;
			for (int i = 0; i < this._associate_lvls.Count; i++)
			{
				Variant variant = this._associate_lvls[i];
				bool flag2 = variant["llid"] == this._cur_level["llid"];
				if (flag2)
				{
					this._associate_lvls._arr.RemoveAt(i);
					break;
				}
			}
			bool flag3 = data.ContainsKey("self_win") && data["self_win"];
			if (flag3)
			{
				flag = true;
			}
			bool flag4 = data.ContainsKey("winplycids");
			if (flag4)
			{
				using (List<Variant>.Enumerator enumerator = data["winplycids"]._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						uint num2 = enumerator.Current;
						bool flag5 = num2 == num;
						if (flag5)
						{
							flag = true;
							break;
						}
					}
				}
			}
			else
			{
				bool flag6 = data["win"] == 0;
				if (flag6)
				{
					flag = true;
				}
				else
				{
					bool flag7 = data["win"] > 0;
					if (flag7)
					{
						Variant variant2 = curr_lvl_conf["pvp"][0];
						bool flag8 = variant2.ContainsKey("side") || (variant2.ContainsKey("death_match") && variant2["death_match"][0].ContainsKey("side")) || (variant2.ContainsKey("cltwar") && variant2["cltwar"][0].ContainsKey("side")) || (variant2.ContainsKey("clcqwar") && variant2["clcqwar"][0].ContainsKey("side"));
						if (flag8)
						{
							flag = (data["win"] == this._lvlsideid);
						}
						else
						{
							flag = (num == data["win"]);
						}
					}
				}
			}
			bool flag9 = flag;
			if (flag9)
			{
				bool flag10 = curr_lvl_conf["lptp"] == 1;
				if (flag10)
				{
					Variant variant3 = null;
					bool flag11 = data.ContainsKey("ply_res");
					if (flag11)
					{
						foreach (Variant current in data["ply_res"]._arr)
						{
							bool flag12 = num == current["cid"];
							if (flag12)
							{
								variant3 = current;
								break;
							}
						}
					}
					bool flag13 = variant3 != null;
					if (flag13)
					{
						this._update_lvlinfo_on_lvlfin(curr_lvl_conf["tpid"], variant3);
						bool flag14 = variant3.ContainsKey("achives");
						if (flag14)
						{
							foreach (Variant current2 in variant3["achives"]._arr)
							{
							}
						}
						bool flag15 = variant3["has_prz"];
						if (flag15)
						{
							this._add_lvlprize(GameTools.createGroup(new Variant[]
							{
								"ltpid",
								this._cur_level["ltpid"],
								"diff_lvl",
								this._cur_level["diff_lvl"]
							}));
						}
					}
				}
				else
				{
					bool flag16 = curr_lvl_conf["lptp"] == 2;
					if (flag16)
					{
						this._add_lvlprize(GameTools.createGroup(new Variant[]
						{
							"ltpid",
							this._cur_level["ltpid"],
							"diff_lvl",
							this._cur_level["diff_lvl"]
						}));
					}
				}
			}
			bool flag17 = this.uiLevel != null;
			if (flag17)
			{
				data["real_win"] = flag;
				this.uiLevel.OnLevelFinish(this._curr_lvl_conf, data);
			}
		}

		public void on_close_lvl_res(Variant data)
		{
			this._rmv_lvl_llid(data["llid"]);
		}

		public void on_lvl_err_msg(Variant data)
		{
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
		}

		public void on_lvl_broadcast_res(Variant data)
		{
			bool flag = this.uiLevel != null;
			if (flag)
			{
				this.uiLevel.OnLevelBroadcast(data);
			}
		}

		public Variant get_lvl_pvpinfo_board_buffer()
		{
			return this._lvl_pvpinfo_board;
		}

		public Variant get_self_lvl_killInfo()
		{
			bool flag = this._lvl_pvpinfo_board != null;
			Variant result;
			if (flag)
			{
				uint num = 0u;
				Variant variant = this._lvl_pvpinfo_board["infos"];
				uint num2 = 0u;
				while ((ulong)num2 < (ulong)((long)variant.Length))
				{
					Variant variant2 = variant[num2];
					bool flag2 = num == variant2["cid"];
					if (flag2)
					{
						result = GameTools.createGroup(new Variant[]
						{
							"rank",
							num2,
							"info",
							variant2
						});
						return result;
					}
					num2 += 1u;
				}
			}
			result = null;
			return result;
		}

		public Variant get_clanter_score_buffer()
		{
			return this._lvl_clanter_score;
		}

		public void get_lvl_pvpinfo_board()
		{
			this.igLevelMsg.get_lvl_pvpinfo_board(GameTools.createGroup(new Variant[]
			{
				"tp",
				1
			}));
		}

		private void get_clanter_score()
		{
			this.igLevelMsg.get_lvl_pvpinfo_board(GameTools.createGroup(new Variant[]
			{
				"tp",
				2
			}));
		}

		public void get_lvl_pvpinfo_side()
		{
			this.igLevelMsg.get_lvl_pvpinfo_board(GameTools.createGroup(new Variant[]
			{
				"tp",
				3
			}));
		}

		public void get_lvl_towerinfo(uint mapid, uint mid, Variant transdata)
		{
			this._transdata = transdata;
			this.igLevelMsg.get_lvl_pvpinfo_board(GameTools.createGroup(new Variant[]
			{
				"tp",
				4,
				"mapid",
				mapid,
				"mid",
				mid
			}));
		}

		public void lvl_pvpinfo_board_msg(Variant data)
		{
			bool flag = data["tp"] == 1;
			if (flag)
			{
				this._lvl_pvpinfo_board = data;
				bool flag2 = this.uiLevel != null;
				if (flag2)
				{
					this.uiLevel.UpdateLevelPvpinfo(data);
				}
			}
			else
			{
				bool flag3 = data["tp"] == 2;
				if (flag3)
				{
					foreach (string current in data.Keys)
					{
						this._lvl_clanter_score[current] = data[current];
					}
				}
				else
				{
					bool flag4 = data["tp"] == 4;
					if (flag4)
					{
						bool flag5 = data && data.ContainsKey("mapid");
						if (flag5)
						{
							this._mapMonObj[data["mapid"]] = data["cntleft"];
						}
						this.ui_npcDialog.MisInfoBack(this._transdata, data);
						bool flag6 = this._transdata;
						if (flag6)
						{
							this._transdata = null;
						}
					}
				}
			}
		}

		public int GetMonCntLeft(int mapid)
		{
			return this._mapMonObj[mapid];
		}

		public void on_lvl_km(Variant data)
		{
			Variant curr_lvl_conf = this._curr_lvl_conf;
			uint num = 0u;
			bool flag = !curr_lvl_conf.ContainsKey("show_course") || curr_lvl_conf["show_course"]._bool;
			if (flag)
			{
				bool flag2 = data.ContainsKey("mid");
				if (flag2)
				{
					num = data["mid"];
					bool flag3 = this._cur_level.ContainsKey("km") && data["sideid"] == this._lvlsideid;
					if (flag3)
					{
						foreach (Variant current in this._cur_level["km"]._arr)
						{
							bool flag4 = current["mid"] == num;
							if (flag4)
							{
								bool flag5 = current["cntleft"] > 0;
								if (flag5)
								{
									Variant expr_F8 = current["cntleft"];
									int @int = expr_F8._int;
									expr_F8._int = @int - 1;
									break;
								}
							}
						}
					}
					bool flag6 = this._cur_level.ContainsKey("sidekm");
					if (flag6)
					{
						foreach (Variant current2 in this._cur_level["sidekm"]._arr)
						{
							bool flag7 = current2["sideid"] == data["sideid"];
							if (flag7)
							{
								foreach (Variant current3 in current2["km"]._arr)
								{
									bool flag8 = current3["mid"] == num;
									if (flag8)
									{
										bool flag9 = current3["cntleft"] > 0;
										if (flag9)
										{
											Variant expr_1F9 = current3["cntleft"];
											int @int = expr_1F9._int;
											expr_1F9._int = @int - 1;
											break;
										}
									}
								}
								break;
							}
						}
					}
					bool flag10 = this.uiLevel != null;
					if (flag10)
					{
						this.uiLevel.OnKillCourseMon(data);
					}
				}
				bool flag11 = data.ContainsKey("pt") && this._cur_level.ContainsKey("sidept");
				if (flag11)
				{
					this.get_lvl_pvpinfo_board();
					Variant variant = this._cur_level["sidept"];
					foreach (Variant current4 in this._cur_level["sidept"]._arr)
					{
						bool flag12 = current4["sideid"] == data["sideid"];
						if (flag12)
						{
							current4["pt"] = data["pt"];
							bool flag13 = this.uiLevel != null;
							if (flag13)
							{
								this.uiLevel.OnUpdateSidept(data);
							}
							break;
						}
					}
				}
			}
			bool flag14 = curr_lvl_conf.ContainsKey("pvp");
			if (flag14)
			{
				Variant variant2 = curr_lvl_conf["pvp"][0];
				bool flag15 = curr_lvl_conf.ContainsKey("cltwarid") && num == variant2["cltwar"][0]["tar_mid"];
				if (flag15)
				{
					Variant expr_3BA = this._lvl_clanter_score["cntleft"];
					int @int = expr_3BA._int;
					expr_3BA._int = @int - 1;
				}
			}
		}

		public void LevelKillMonster(Variant data)
		{
			bool flag = this._cur_level != null;
			if (flag)
			{
				bool flag2 = this._cur_level.ContainsKey("score_km");
				if (flag2)
				{
					bool flag3 = false;
					foreach (Variant current in this._cur_level["score_km"]._arr)
					{
						bool flag4 = data["mid"] == current["mid"];
						if (flag4)
						{
							Variant expr_80 = current["cnt"];
							int @int = expr_80._int;
							expr_80._int = @int + 1;
							flag3 = true;
						}
					}
					bool flag5 = !flag3;
					if (flag5)
					{
						this._cur_level["score_km"]._arr.Add(GameTools.createGroup(new Variant[]
						{
							"mid",
							data["mid"],
							"cnt",
							1
						}));
					}
					this.uiLevel.LevelKill(null);
				}
			}
		}

		public void mod_lvl_selfpvpinfo(Variant data)
		{
			bool flag = data.ContainsKey("kp");
			if (flag)
			{
				this._update_kprec_kp(data["kp"]);
			}
			bool flag2 = data.ContainsKey("dc");
			if (flag2)
			{
				this._update_kprec_dc(data["dc"]);
			}
			bool flag3 = this._curr_lvl_conf && this._curr_lvl_conf.ContainsKey("map_lay");
			Variant variant;
			if (flag3)
			{
				variant = this._curr_lvl_conf["map_lay"][this._lvlsideid]["m"];
			}
			else
			{
				variant = new Variant();
			}
			bool flag4 = data.ContainsKey("mdc");
			if (flag4)
			{
				bool flag5 = this._cur_level;
				if (flag5)
				{
					this._cur_level["kprec"]["mdc"] = data["mdc"];
					bool flag6 = this._curlay > 0 && this._cur_level["kprec"]["mdc"] >= variant[this._curlay]["mdc_exit"];
					if (flag6)
					{
						this._curlay--;
						this.changeLvl();
					}
				}
			}
			bool flag7 = data.ContainsKey("mkp");
			if (flag7)
			{
				bool flag8 = this._cur_level;
				if (flag8)
				{
					this._cur_level["kprec"]["mkp"] = data["mkp"];
					bool flag9 = this._curlay < variant.Length - 1 && this._cur_level["kprec"]["mkp"] >= variant[this._curlay]["mkp_enter"];
					if (flag9)
					{
						this._curlay++;
						this.changeLvl();
					}
				}
			}
			bool flag10 = this.uiLevel != null;
			if (flag10)
			{
				this.uiLevel.UpdateSelfPvpinfo(data);
			}
		}

		public Variant GetCurrRoundInfo()
		{
			return this._curr_round_info;
		}

		public Variant get_kumiteply()
		{
			return this._kumiteply;
		}

		public Variant get_kumiteply_ply(uint cid)
		{
			bool flag = this._kumiteply.Length > 0;
			Variant result;
			if (flag)
			{
				foreach (Variant current in this._kumiteply._arr)
				{
					bool flag2 = current["cid"] == cid;
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

		public void on_self_die(Variant dispacher, Variant par)
		{
			bool flag = this._curr_round_info.ContainsKey("ghostcnt");
			if (flag)
			{
				Variant expr_26 = this._curr_round_info["ghostcnt"];
				int @int = expr_26._int;
				expr_26._int = @int - 1;
			}
		}

		public void on_lvl_side_info(Variant data)
		{
			this._lvlsideid = data["lvlsideid"]._int;
			bool flag = data.ContainsKey("sideclan");
			if (flag)
			{
				this._set_sideclan(data["sideclan"]);
			}
			bool flag2 = data.ContainsKey("kumiteply");
			if (flag2)
			{
				this._set_kumite_plys(data["kumiteply"]);
			}
			bool flag3 = data.ContainsKey("kumrs");
			if (flag3)
			{
				this._kumrs = data["kumrs"];
				this._curr_round_info["preptm"] = this._kumrs["preptm"];
				this._curr_round_info["round_tm"] = this._kumrs["rndtm"];
			}
			bool flag4 = data.ContainsKey("kumre");
			if (flag4)
			{
				this._kumre = data["kumre"];
			}
			bool flag5 = data.ContainsKey("round_change");
			if (flag5)
			{
				Variant variant = data["round_change"];
				this._curr_round_info["cur_round"] = variant["cur_round"];
				this._curr_round_info["round_tm"] = variant["round_tm"];
				this._curr_round_info["preptm"] = variant["preptm"];
				this._curr_round_info["ghostcnt"] = variant["ghostcnt"];
				Variant value = variant["winplycids"];
				this._curr_round_info["winplycids"] = value;
				bool flag6 = this.uiLevel != null;
				if (flag6)
				{
					this.uiLevel.OnLevelRoundChange(variant);
				}
			}
		}

		private void _set_kumite_plys(Variant plys)
		{
			bool flag = this._kumiteply.Length > 0;
			if (flag)
			{
				int num = this._kumiteply.Length - 1;
				foreach (Variant current in plys._arr)
				{
					bool flag2 = false;
					for (uint num2 = (uint)num; num2 >= 0u; num2 -= 1u)
					{
						Variant variant = this._kumiteply[(int)num2];
						bool flag3 = current["cid"] == variant["cid"];
						if (flag3)
						{
							variant["stat"] = current["stat"];
							flag2 = true;
							break;
						}
					}
					bool flag4 = !flag2;
					if (flag4)
					{
						this._kumiteply._arr.Add(current);
					}
				}
			}
			else
			{
				this._kumiteply = plys;
			}
		}

		public Variant get_sideclan(int sideid)
		{
			bool flag = this._sideclan;
			Variant result;
			if (flag)
			{
				result = this._sideclan[sideid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		private void _set_sideclan(Variant sideclan)
		{
			bool flag = !this._sideclan;
			if (flag)
			{
				this._sideclan = new Variant();
			}
			foreach (Variant current in sideclan._arr)
			{
				this._sideclan[current["sideid"]] = current;
			}
		}

		public Variant get_kmtrigs()
		{
			return this._kmtrigs;
		}

		public void SetKillMonTrigs(Variant kmtrigs)
		{
			bool flag = this._kmtrigs == null;
			if (flag)
			{
				this._kmtrigs = kmtrigs;
			}
			else
			{
				for (int i = this._kmtrigs.Length - 1; i >= 0; i--)
				{
					bool flag2 = this._kmtrigs[i]["cnt"] <= this._kmtrigs[i]["kmcnt"];
					if (flag2)
					{
						this._kmtrigs._arr.RemoveAt(i);
					}
				}
				foreach (Variant current in kmtrigs._arr)
				{
					bool flag3 = false;
					foreach (Variant current2 in this._kmtrigs._arr)
					{
						bool flag4 = true;
						foreach (string current3 in current.Keys)
						{
							bool flag5 = current[current3] != current2[current3];
							if (flag5)
							{
								flag4 = false;
								break;
							}
						}
						bool flag6 = flag4;
						if (flag6)
						{
							flag3 = true;
							break;
						}
					}
					bool flag7 = !flag3;
					if (flag7)
					{
						this._kmtrigs._arr.Add(current);
					}
				}
			}
			bool flag8 = this.uiLevel != null;
			if (flag8)
			{
				this.uiLevel.UpdateKmtimgs();
			}
		}

		public void OnKillTrigMon(uint km_mid)
		{
			bool flag = this._kmtrigs == null;
			if (!flag)
			{
				foreach (Variant current in this._kmtrigs._arr)
				{
					bool flag2 = current["mid"] == km_mid;
					if (flag2)
					{
						bool flag3 = current["kmcnt"] >= current["cnt"];
						if (flag3)
						{
							current["kmcnt"] = current["cnt"];
						}
						else
						{
							Variant expr_9D = current["kmcnt"];
							int @int = expr_9D._int + 1;
							expr_9D._int = @int;
						}
					}
				}
				bool flag4 = this.uiLevel != null;
				if (flag4)
				{
					this.uiLevel.UpdateKmtimgs();
				}
			}
		}

		public void SetTmtrigArr(Array trigsArr)
		{
			bool flag = this.uiLevel != null;
			if (flag)
			{
			}
		}

		private void _update_kprec_ckp(uint ckp)
		{
			this._cur_level["kprec"]["ckp"] = ckp;
		}

		private void _update_kprec_dc(uint dc)
		{
			this._cur_level["kprec"]["dc"] = dc;
		}

		private void _update_kprec_kp(uint kp)
		{
			bool flag = this._cur_level["kprec"];
			if (flag)
			{
				int num = (int)(kp - (uint)this._cur_level["kprec"]["kp"]);
				this._cur_level["kprec"]["kp"] = kp;
			}
		}

		public int get_level_cds(uint cdtp)
		{
			int result;
			foreach (Variant current in this._entercds._arr)
			{
				bool flag = current["cdtp"] == cdtp;
				if (flag)
				{
					result = current["cdtm"];
					return result;
				}
			}
			result = 0;
			return result;
		}

		public void on_ply_teamid_chang()
		{
			this.clear_associate_by_lmtp(2u);
			this.get_associate_lvls(2u, true);
		}

		public void get_associate_lvls(uint lmtp = 0u, bool entercd = true)
		{
			this.igLevelMsg.get_associate_lvls(GameTools.createGroup(new Variant[]
			{
				"lmtp",
				0,
				"entercd",
				entercd
			}));
		}

		public void get_associate_lvls_res(Variant data)
		{
			bool flag = data.ContainsKey("lvls");
			if (flag)
			{
				this._associate_lvls = data["lvls"];
			}
			bool flag2 = data.ContainsKey("entercds");
			if (flag2)
			{
				this._entercds = data["entercds"];
			}
			bool flag3 = this.ui_enter != null;
			if (flag3)
			{
				this.ui_enter.UpdateScriptInfo(null);
			}
		}

		public int get_arena_llid(int arenaid)
		{
			int result;
			foreach (Variant current in this._associate_lvls._arr)
			{
				Variant variant = this.muCCfg.svrLevelConf.get_level_data(current["ltpid"]);
				bool flag = variant.ContainsKey("arenaid") && variant["arenaid"] == arenaid;
				if (flag)
				{
					result = current["llid"];
					return result;
				}
			}
			result = 0;
			return result;
		}

		private Variant _get_lvl_llid(uint ltpid)
		{
			bool flag = this._associate_lvls.Length > 0;
			Variant result;
			if (flag)
			{
				float num = (float)(this.muNClt.CurServerTimeStampMS / 1000L);
				for (int i = this._associate_lvls.Length - 1; i >= 0; i--)
				{
					Variant variant = this._associate_lvls[i];
					bool flag2 = (float)(variant["end_tm"] + 120000) < num;
					if (flag2)
					{
						this._associate_lvls._arr.RemoveAt(i);
					}
					else
					{
						bool flag3 = variant["ltpid"] == ltpid;
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

		private void _add_lvl_llid(Variant data)
		{
			bool flag = data.ContainsKey("llid");
			if (flag)
			{
				Variant variant = new Variant();
				uint num = 0u;
				while ((ulong)num < (ulong)((long)this._associate_lvls.Count))
				{
					variant = this._associate_lvls[(int)num];
					bool flag2 = variant["llid"] == data["llid"];
					if (flag2)
					{
						this._associate_lvls._arr.RemoveAt((int)num);
						break;
					}
					num += 1u;
				}
				Variant variant2 = new Variant();
				variant2["llid"] = data["llid"];
				variant2["diff_lvl"] = 1;
				variant2["ltpid"] = data["ltpid"];
				Variant variant3 = variant2;
				bool flag3 = data.ContainsKey("diff_lvl");
				if (flag3)
				{
					variant3["diff_lvl"] = data["diff_lvl"];
				}
				bool flag4 = data["cost_tp"] != null;
				if (flag4)
				{
					variant3["cost_tp"] = data["cost_tp"];
				}
				int lvlArenaID = this.GetLvlArenaID(data["ltpid"]);
				int lvlArenaexID = this.GetLvlArenaexID(data["ltpid"]);
				bool flag5 = lvlArenaID != 0 || lvlArenaexID != 0;
				if (flag5)
				{
					uint num2 = 0u;
					while ((ulong)num2 < (ulong)((long)this._associate_lvls.Count))
					{
						variant = this._associate_lvls[(int)num2];
						bool flag6 = variant["ltpid"] == data["ltpid"];
						if (flag6)
						{
							this._associate_lvls._arr.RemoveAt((int)num2);
						}
						num2 += 1u;
					}
					Variant variant4 = this.muCCfg.svrLevelConf.get_arena_level((uint)lvlArenaID);
					Variant variant5 = this.muCCfg.svrLevelConf.get_arenaex_level((uint)lvlArenaexID);
					bool flag7 = variant4 != null;
					Variant variant6;
					if (flag7)
					{
						variant6 = variant4;
					}
					else
					{
						variant6 = variant5;
					}
					bool flag8 = true;
					bool flag9 = variant6 != null & flag8;
					if (flag9)
					{
					}
				}
				this._associate_lvls._arr.Add(variant3);
			}
		}

		private void _rmv_lvl_llid(uint llid)
		{
			for (int i = 0; i < this._associate_lvls.Count; i++)
			{
				Variant variant = this._associate_lvls[i];
				bool flag = variant["llid"] == llid;
				if (flag)
				{
					this._associate_lvls._arr.RemoveAt(i);
					break;
				}
			}
		}

		private void clear_associate_by_lmtp(uint lmtp)
		{
			for (int i = this._associate_lvls.Count - 1; i >= 0; i--)
			{
				Variant variant = this._associate_lvls[i];
				Variant variant2 = this.muCCfg.svrLevelConf.get_level_data(variant["ltpid"]);
				bool flag = variant2["lmtp"] == lmtp;
				if (flag)
				{
					this._associate_lvls._arr.RemoveAt(i);
				}
			}
		}

		public Variant get_lvl_cnt(uint tpid)
		{
			return this._lvl_infos[tpid];
		}

		public Variant get_lvlinfos()
		{
			return this._lvl_infos;
		}

		public Variant get_entered_lvls()
		{
			return this._enters;
		}

		public int get_lvl_left_cnt(uint tpid)
		{
			int result = -1;
			Variant variant = this._lvl_infos[tpid];
			bool flag = variant;
			if (flag)
			{
				bool flag2 = variant.ContainsKey("cntleft");
				if (flag2)
				{
					result = variant["cntleft"];
				}
			}
			else
			{
				Variant variant2 = this.muCCfg.svrLevelConf.get_level_data(tpid);
				bool flag3 = variant2["dalyrep"] > 0;
				if (flag3)
				{
					result = variant2["dalyrep"];
				}
			}
			return result;
		}

		public bool has_enter_lvl(int tpid)
		{
			bool flag = this._lvl_infos.ContainsKey(tpid);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this._no_entry_lvl.ContainsKey(tpid);
				if (flag2)
				{
					result = false;
				}
				else
				{
					this._no_entry_lvl[tpid] = tpid;
					this.igLevelMsg.get_lvl_cnt_info(GameTools.createGroup(new Variant[]
					{
						"ltpid",
						tpid
					}));
					result = false;
				}
			}
			return result;
		}

		public void get_ply_lvlinfo()
		{
			this._lvl_infos = new Variant();
			this.igLevelMsg.get_lvl_cnt_info(new Variant());
		}

		public void get_lvl_info_res(Variant data)
		{
		}

		public double GetLvlEnterTm(uint tpid)
		{
			bool flag = this._no_entry_lvl.ContainsKey((int)tpid);
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				bool flag2 = this._lvl_infos.ContainsKey((int)tpid);
				if (flag2)
				{
					bool flag3 = this._lvl_infos[tpid].ContainsKey("lastetm");
					if (flag3)
					{
						result = this._lvl_infos[tpid]["lastetm"]._double;
					}
					else
					{
						result = 0.0;
					}
				}
				else
				{
					this._no_entry_lvl[tpid] = tpid;
					this.igLevelMsg.get_lvl_cnt_info(GameTools.createGroup(new Variant[]
					{
						"ltpid",
						tpid
					}));
					result = -1.0;
				}
			}
			return result;
		}

		private void _update_level_ctnleft(uint tpid, int cntleft)
		{
			Variant variant = this._lvl_infos[tpid];
			bool flag = variant != null;
			if (flag)
			{
				variant["cntleft"] = cntleft;
			}
			else
			{
				float val = (float)this.muNClt.CurServerTimeStampMS;
				this._lvl_infos[tpid] = GameTools.createGroup(new Variant[]
				{
					"ltpid",
					tpid,
					"cntleft",
					cntleft,
					"diff_lvl",
					1,
					"score",
					0,
					"lastetm",
					val
				});
			}
		}

		private void _update_lvlinfo_on_lvlfin(uint tpid, Variant self_res)
		{
			Variant variant = this._lvl_infos[tpid];
			bool flag = variant;
			if (flag)
			{
				bool flag2 = variant["score"] < self_res["score"]._int;
				if (flag2)
				{
					variant["score"] = self_res["score"];
				}
				bool flag3 = variant.ContainsKey("fcnt");
				if (flag3)
				{
					Variant expr_7B = variant["fcnt"];
					int @int = expr_7B._int;
					expr_7B._int = @int + 1;
				}
				else
				{
					variant["fcnt"]._int = 1;
				}
			}
			else
			{
				variant = GameTools.createGroup(new Variant[]
				{
					"ltpid",
					tpid,
					"diff_lvl",
					1,
					"score",
					self_res["score"]._int,
					"fcnt",
					1
				});
				this._lvl_infos[tpid] = variant;
				Variant variant2 = this.muCCfg.svrLevelConf.get_level_data(tpid);
				bool flag4 = variant2["dalyrep"] > 0;
				if (flag4)
				{
					variant["cntleft"] = variant2["dalyrep"] - 1;
				}
			}
			bool flag5 = self_res.ContainsKey("diff_lvl");
			if (flag5)
			{
				variant["diff_lvl"] = self_res["diff_lvl"];
			}
			bool flag6 = self_res.ContainsKey("fin_diff");
			if (flag6)
			{
				variant["fin_diff"] = self_res["fin_diff"];
			}
		}

		public void get_curr_lvl_prize()
		{
			bool flag = this._curltpid > 0;
			if (flag)
			{
				this.igLevelMsg.get_lvl_prize(GameTools.createGroup(new Variant[]
				{
					"ltpid",
					this._curltpid
				}));
			}
		}

		public void lvl_get_prize_res(Variant data)
		{
			this._remove_prize(data["ltpid"], this._current_diff);
			this._awdData = data;
		}

		private void _onGetLevelAwd()
		{
			bool flag = this.uiLevel != null;
			if (flag)
			{
				this.uiLevel.OnGetLevelAwd(this._awdData);
			}
		}

		public Variant GetPrize()
		{
			return this._lvlprizes;
		}

		private void _add_lvlprize(Variant prize)
		{
			this._lvlprizes._arr.Add(prize);
		}

		private void _remove_prize(uint ltpid, uint diff_lvl)
		{
			for (int i = 0; i < this._lvlprizes.Length; i++)
			{
				Variant variant = this._lvlprizes[i];
				bool flag = variant["ltpid"] == ltpid;
				if (flag)
				{
					bool flag2 = diff_lvl == 0u || variant["diff_lvl"] == diff_lvl;
					if (flag2)
					{
						this._lvlprizes._arr.RemoveAt(i);
						break;
					}
				}
			}
		}

		public void on_lvl_res(Variant data)
		{
			switch (data["tp"]._int)
			{
			case 1:
			{
				this._update_lvlinfo_on_lvlfin(data["ltpid"], GameTools.createGroup(new Variant[]
				{
					"score",
					0
				}));
				bool flag = this.muCCfg.svrLevelConf.IsLevelHasItemPrize(data["ltpid"]);
				if (flag)
				{
					this._add_lvlprize(GameTools.createGroup(new Variant[]
					{
						"ltpid",
						data["ltpid"],
						"diff_lvl",
						data["diff_lvl"]
					}));
				}
				break;
			}
			case 2:
				this.getRecordRes(data);
				break;
			case 8:
			{
				bool flag2 = this.uiLevel != null;
				if (flag2)
				{
					this.uiLevel.AddCityWarCost(data);
				}
				break;
			}
			case 9:
			{
				bool flag3 = data.ContainsKey("cost_gold");
				if (flag3)
				{
				}
				bool flag4 = data.ContainsKey("cost_yb");
				if (flag4)
				{
				}
				bool flag5 = this.uiLevel != null;
				if (flag5)
				{
					this.uiLevel.OnBuyLevelBuffRes(data);
				}
				break;
			}
			}
		}

		public void LvlTmCost(Variant data)
		{
			bool flag = this._hadCost == null;
			if (flag)
			{
				this._hadCost = GameTools.createGroup(new Variant[]
				{
					"yb",
					0,
					"gold",
					0
				});
			}
			bool flag2 = data.ContainsKey("yb");
			if (flag2)
			{
				Variant hadCost = this._hadCost;
				hadCost["yb"] = hadCost["yb"] + -data["yb"];
			}
			bool flag3 = data.ContainsKey("gold");
			if (flag3)
			{
				Variant hadCost = this._hadCost;
				hadCost["gold"] = hadCost["gold"] + -data["gold"];
			}
			bool flag4 = this.uiLevel != null;
			if (flag4)
			{
				this.uiLevel.AddLvlCost();
			}
		}

		public Variant GetCurLvlCost()
		{
			return this._hadCost;
		}

		public uint IsClanHasClte(uint clteid)
		{
			Variant variant = this._clanTerritorys[clteid];
			return (variant != null) ? variant["clanid"]._uint : 0u;
		}

		public bool IsCltNotOwn(uint clteid)
		{
			bool flag = this._clanTerritorys != null && this._clanTerritorys[clteid] != null;
			bool result;
			if (flag)
			{
				Variant variant = this._clanTerritorys[clteid];
				result = (variant["clanid"]._int == 0);
			}
			else
			{
				result = false;
			}
			return result;
		}

		public void GetChanTerDalyAwd(uint clteid)
		{
		}

		public void OnClanError(int clanid)
		{
			bool flag = this._clanTerritorys.Count != 0;
			if (flag)
			{
				foreach (Variant current in this._clanTerritorys.Values)
				{
					bool flag2 = current["clanid"] == clanid;
					if (flag2)
					{
						current["clanid"] = 0;
						break;
					}
				}
			}
		}

		private void initClanTerritory(Variant data)
		{
			data.RemoveKey("tp");
			this._loadTerState.RemoveKey(data["clteid"]);
			this._clanTerritorys[data["clteid"]] = data;
			bool flag = this.uiLevel != null;
			if (flag)
			{
				this.uiLevel.OnClanTerrInfoChange(data["0clteid"]);
			}
		}

		private bool adjustClanTerrInfo(Variant data)
		{
			Variant variant = this._clanTerritorys[data["clteid"]];
			bool flag = variant != null;
			bool result;
			if (flag)
			{
				data.RemoveKey("tp");
				foreach (string current in data.Keys)
				{
					variant[current] = data[current];
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		private bool setClanTerrBuildHPInfo(Variant data)
		{
			Variant variant = this._clanTerritorys[data["clteid"]];
			bool flag = variant != null;
			bool result;
			if (flag)
			{
				variant["buildhp"] = data;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		private Variant getClanTerrBuildHPInfo(uint clteid)
		{
			Variant variant = this._clanTerritorys[clteid];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant["buildhp"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		private bool updateClanTerrBuildHP(Variant data)
		{
			Variant clanTerrBuildHPInfo = this.getClanTerrBuildHPInfo(data["clteid"]);
			bool flag = clanTerrBuildHPInfo;
			bool result;
			if (flag)
			{
				foreach (Variant current in clanTerrBuildHPInfo["mon_hp_pers"]._arr)
				{
					bool flag2 = current["mapid"] == data["mapid"];
					if (flag2)
					{
						foreach (Variant current2 in current["hp_pers"]._arr)
						{
							bool flag3 = current2["mid"] == data["mid"];
							if (flag3)
							{
								current2["hp_per"] = 100;
								result = true;
								return result;
							}
						}
						break;
					}
				}
			}
			result = false;
			return result;
		}

		private bool setClanTerrReqs(Variant data)
		{
			Variant variant = this._clanTerritorys[data["clteid"]];
			bool flag = variant;
			bool result;
			if (flag)
			{
				variant["war_reqs"] = data["war_reqs"];
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		private Variant getClanTerrReqs(uint clteid)
		{
			Variant variant = this._clanTerritorys[clteid];
			bool flag = variant;
			Variant result;
			if (flag)
			{
				result = variant["war_reqs"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		private bool addClanTerrReqs(Variant data)
		{
			Variant clanTerrReqs = this.getClanTerrReqs(data["clteid"]);
			bool flag = clanTerrReqs;
			bool result;
			if (flag)
			{
				clanTerrReqs._arr.Add(data["clanid"]);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public void on_clanter_res(Variant data)
		{
			switch (data["tp"]._int)
			{
			case 1:
				this.initClanTerritory(data);
				break;
			case 2:
			{
				uint @uint = data["awdtm"]._uint;
				bool flag = @uint > 0u;
				if (flag)
				{
					this.igLevelMsg.get_clanter_info(GameTools.createGroup(new Variant[]
					{
						"tp",
						2,
						"clteid",
						data["clteid"]
					}));
				}
				break;
			}
			case 3:
			{
				bool flag2 = this.adjustClanTerrInfo(data);
				if (flag2)
				{
					bool flag3 = this.uiLevel != null;
					if (flag3)
					{
						this.uiLevel.OnClanTerrInfoChange(data["clteid"]);
					}
				}
				break;
			}
			case 5:
			{
				bool flag4 = this.setClanTerrBuildHPInfo(data);
				if (flag4)
				{
					bool flag5 = this.uiLevel != null;
					if (flag5)
					{
						this.uiLevel.UpdateAllClanTerrBuildHP(data["clteid"]);
					}
				}
				break;
			}
			case 6:
			{
				bool flag6 = 1 == data["res"] || -1461 == data["res"];
				if (flag6)
				{
					bool flag7 = this.updateClanTerrBuildHP(data);
					if (flag7)
					{
						bool flag8 = this.uiLevel != null;
						if (flag8)
						{
							this.uiLevel.UpdateClanTerrBuildHP(data["clteid"], data);
						}
					}
				}
				break;
			}
			case 7:
			{
				bool flag9 = this.setClanTerrReqs(data);
				if (flag9)
				{
					bool flag10 = this.uiLevel != null;
					if (flag10)
					{
						this.uiLevel.UpdateClanTerrReqInfo(data["clteid"], data["war_reqs"]);
					}
				}
				break;
			}
			case 8:
			{
				bool flag11 = this.addClanTerrReqs(data);
				if (flag11)
				{
					bool flag12 = this.uiLevel != null;
					if (flag12)
					{
						this.uiLevel.AddClanTerrReq(data["clteid"], data["clanid"]);
					}
				}
				break;
			}
			}
			bool flag13 = this._clanInfoCall != null;
			if (flag13)
			{
				this._clanInfoCall(data);
				this._clanInfoCall = null;
			}
		}

		public Variant GetClanTerrInfo(uint clteid, Action<Variant> onfin = null)
		{
			Variant variant = this._clanTerritorys[clteid];
			bool flag = variant == null;
			Variant result;
			if (flag)
			{
				bool flag2 = this._loadTerState.ContainsKey((int)clteid);
				if (flag2)
				{
					result = null;
					return result;
				}
				bool flag3 = onfin != null;
				if (flag3)
				{
					this._clanInfoCall = onfin;
				}
				this._loadTerState[clteid] = true;
				this.igLevelMsg.get_clanter_info(GameTools.createGroup(new Variant[]
				{
					"tp",
					1,
					"clteid",
					clteid
				}));
			}
			else
			{
				bool flag4 = onfin != null;
				if (flag4)
				{
					onfin(variant);
				}
				this._clanInfoCall = null;
			}
			result = variant;
			return result;
		}

		public void GetClanTerrAwd(int clteid)
		{
			this.igLevelMsg.get_clanter_info(GameTools.createGroup(new Variant[]
			{
				"tp",
				4,
				"clteid",
				clteid
			}));
		}

		public Variant GetClanTerrBuildHPInfo(uint clteid)
		{
			Variant clanTerrBuildHPInfo = this.getClanTerrBuildHPInfo(clteid);
			bool flag = clanTerrBuildHPInfo;
			Variant result;
			if (flag)
			{
				result = clanTerrBuildHPInfo;
			}
			else
			{
				this.igLevelMsg.get_clanter_info(GameTools.createGroup(new Variant[]
				{
					"tp",
					5,
					"clteid",
					clteid
				}));
				result = null;
			}
			return result;
		}

		public void RepairClanTerrBuild(uint clteid, uint mapid, uint mid)
		{
			this.igLevelMsg.get_clanter_info(GameTools.createGroup(new Variant[]
			{
				"tp",
				6,
				"clteid",
				clteid,
				"mapid",
				mapid,
				"mid",
				mid
			}));
		}

		public Variant GetClanTerrRequests(int clteid)
		{
			Variant clanTerrReqs = this.getClanTerrReqs((uint)clteid);
			bool flag = clanTerrReqs != null;
			Variant result;
			if (flag)
			{
				result = clanTerrReqs;
			}
			else
			{
				this.igLevelMsg.get_clanter_info(GameTools.createGroup(new Variant[]
				{
					"tp",
					7,
					"clteid",
					clteid
				}));
				result = null;
			}
			return result;
		}

		public void RequestClanTerr(int clteid)
		{
			this.igLevelMsg.get_clanter_info(GameTools.createGroup(new Variant[]
			{
				"tp",
				8,
				"clteid",
				clteid
			}));
		}

		public bool is_city_war(uint clteid, uint lvltpid)
		{
			Variant variant = this.muCCfg.svrLevelConf.get_clan_territory(clteid);
			bool flag = variant != null;
			bool result;
			if (flag)
			{
				bool flag2 = variant["tp"] == 2;
				if (flag2)
				{
					bool flag3 = variant["warlvl"]["tpid"] == lvltpid;
					if (flag3)
					{
						float num = (float)this.muNClt.CurServerTimeStampMS;
						bool flag4 = ConfigUtil.check_tm((double)num, variant["warlvl"]["tmchk"], 0.0, 0.0);
						if (flag4)
						{
							result = true;
							return result;
						}
					}
				}
			}
			result = false;
			return result;
		}

		public void clear_carrchief_npc_data(uint ltpid)
		{
			Variant variant = this.muCCfg.svrLevelConf.get_level_data(ltpid);
			int num = variant["carrchief"];
			bool flag = this._carrchief_info.ContainsKey(num);
			if (flag)
			{
				this._carrchief_info.ContainsKey(num);
				this.get_carrchief_info((uint)num);
			}
		}

		public void get_carrchief_npc_data(uint carr, Action<Variant> on_fin)
		{
		}

		public Variant GetCarrchiefData(uint carr)
		{
			Variant variant = this._carrchief_info[carr];
			bool flag = variant == null;
			Variant result;
			if (flag)
			{
				this._carrchief_info[carr] = new Variant();
				this.get_carrchief_info(carr);
				result = null;
			}
			else
			{
				result = variant;
			}
			return result;
		}

		public void OnMapChangedFin()
		{
			this._carrchief_info_cb._arr.Clear();
		}

		public int get_carrchief_npc_id(uint carr)
		{
			bool flag = this._carrchief_npc_id.ContainsKey((int)carr);
			int result;
			if (flag)
			{
				result = this._carrchief_npc_id[carr];
			}
			else
			{
				int num = this.muCCfg.svrNpcConf.get_carrchief_npc((int)carr);
				this._carrchief_npc_id[carr] = num;
				result = num;
			}
			return result;
		}

		public void on_npc_shop(Variant data)
		{
			debug.Log("NPCSHOP============" + data.dump());
		}

		public void get_carrchief_info(uint carr)
		{
			this.igLevelMsg.get_carrchief_info(GameTools.createGroup(new Variant[]
			{
				"tp",
				1,
				"carr",
				carr
			}));
		}

		public void get_carrchief_award()
		{
			this.igLevelMsg.get_carrchief_info(GameTools.createGroup(new Variant[]
			{
				"tp",
				2
			}));
		}

		public int get_lvl_residue_cnt(uint tpid)
		{
			int num = 0;
			Variant variant = this.get_lvl_cnt(tpid);
			Variant variant2 = this.muCCfg.svrLevelConf.get_level_data(tpid);
			bool flag = variant != null;
			if (flag)
			{
				bool flag2 = variant.ContainsKey("cntleft");
				if (flag2)
				{
					num = variant["cntleft"]._int;
					bool flag3 = num == 0 && variant2 != null && variant2["dalyrep"] == 0;
					if (flag3)
					{
						num = -100;
					}
				}
				else
				{
					num = -100;
				}
			}
			else
			{
				bool flag4 = variant2 != null;
				if (flag4)
				{
					bool flag5 = variant2.ContainsKey("dalyrep") && variant2["dalyrep"] > 0;
					if (flag5)
					{
						num = variant2["dalyrep"];
					}
					else
					{
						num = -100;
					}
				}
			}
			return num;
		}

		public Variant get_lvl_llid(uint ltpid)
		{
			bool flag = this._associate_lvls.Length == 0;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				float num = (float)((this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L);
				uint num2 = 0u;
				while ((ulong)num2 < (ulong)((long)this._associate_lvls.Count))
				{
					Variant variant = this._associate_lvls[(int)num2];
					float num3 = variant.ContainsKey("end_tm") ? variant["end_tm"]._float : 0f;
					bool flag2 = 0f != num3 && num3 + 120000f < num;
					if (flag2)
					{
						this._associate_lvls._arr.RemoveAt((int)num2);
						num2 -= 1u;
					}
					else
					{
						bool flag3 = variant["ltpid"] == ltpid;
						if (flag3)
						{
							result = variant;
							return result;
						}
					}
					num2 += 1u;
				}
				result = null;
			}
			return result;
		}

		public Variant GetCitywarInfo()
		{
			bool flag = this._curr_lvl_conf && this._curr_lvl_conf.ContainsKey("pvp");
			Variant result;
			if (flag)
			{
				Variant variant = this._curr_lvl_conf["pvp"];
				bool flag2 = variant && variant[0] && variant[0].ContainsKey("cltwar");
				if (flag2)
				{
					result = variant[0]["cltwar"][0];
					return result;
				}
			}
			result = null;
			return result;
		}

		public void GetLvlBuff(int id, int cost_tp)
		{
			this.igLevelMsg.get_lvl_info(GameTools.createGroup(new Variant[]
			{
				"tp",
				9,
				"id",
				id,
				"cost_tp",
				cost_tp
			}));
		}

		public Variant GetRecord(uint fintp, int begin_idx, int end_idx)
		{
			string key = string.Concat(new object[]
			{
				fintp,
				"_",
				begin_idx,
				"_",
				end_idx
			});
			float num = (float)this.muNClt.CurServerTimeStampMS;
			bool flag = !this._loading.ContainsKey(key);
			if (flag)
			{
				this._loading[key] = 0;
			}
			bool flag2 = this._loading[key] < num;
			Variant result;
			if (flag2)
			{
				this._loading[key] = num + 60000f;
				this.GetLvlmisRecord(fintp, begin_idx, end_idx);
				result = null;
			}
			else
			{
				Variant variant = this._record[fintp];
				bool flag3 = variant == null;
				if (flag3)
				{
					this._record[fintp] = new Variant();
					result = null;
				}
				else
				{
					Variant variant2 = new Variant();
					for (int i = begin_idx; i < end_idx; i++)
					{
						variant2._arr.Add(variant[i]);
					}
					result = variant2;
				}
			}
			return result;
		}

		private void getRecordRes(Variant data)
		{
			int idx = data["fintp"];
			bool flag = this._record[idx];
			Variant variant;
			if (flag)
			{
				variant = this._record[idx];
			}
			else
			{
				variant = new Variant();
			}
			for (int i = 0; i < data["infos"].Length; i++)
			{
				variant[i + data["begin_idx"]] = data["infos"][i];
			}
			this._record[idx] = variant;
			this.ui_scriptAct.refreshTowerRecord(variant, null);
		}

		private void GetLvlmisRecord(uint fintp, int begin_idx, int end_idx)
		{
			this.igLevelMsg.get_lvl_info(GameTools.createGroup(new Variant[]
			{
				"tp",
				2,
				"fintp",
				fintp,
				"begin_idx",
				begin_idx,
				"end_idx",
				end_idx
			}));
		}

		public void GetLvlmisPrize(int lmisid)
		{
			this.igMissionMsg.GetLvlmisPrize(lmisid);
		}

		private void GetLvlmisInfo()
		{
			this.igLevelMsg.GetLvlmisInfo();
		}

		public void lvlmis_changed(Variant data)
		{
			int val = data["lmisid"];
			int @int = data["tp"]._int;
			if (@int != 1)
			{
				if (@int != 2)
				{
				}
			}
			else
			{
				Variant variant = this._lvlmisData["misline"];
				Variant variant2 = this.muCCfg.svrLevelConf.Getlvlmis();
				int num = 0;
				foreach (string current in variant2.Keys)
				{
					bool flag = current == data["lmisid"];
					if (flag)
					{
						num = variant2[current]["line"];
						break;
					}
				}
				bool flag2 = false;
				foreach (Variant current2 in variant._arr)
				{
					bool flag3 = num == current2["lineid"];
					if (flag3)
					{
						current2["lmisid"] = val;
						flag2 = true;
						break;
					}
				}
				bool flag4 = !flag2;
				if (flag4)
				{
					variant._arr.Add(GameTools.createGroup(new Variant[]
					{
						"lienid",
						num,
						"lmisid",
						val
					}));
				}
				this.ui_scriptAct.refreshTowerPage();
			}
		}

		public void get_lvlmis_res(Variant data)
		{
			this._lvlmisData = data;
			this.muLgClt.g_missionCT.lvlMisChange();
		}

		public Variant get_lvlmis_data()
		{
			bool flag = this._lvlmisData == null;
			if (flag)
			{
				this._lvlmisData = new Variant();
				this.GetLvlmisInfo();
			}
			return this._lvlmisData;
		}

		public void GetTerritory()
		{
			uint num = 0u;
			bool flag = num > 0u;
			if (flag)
			{
			}
			Variant variant = this.muCCfg.svrLevelConf.get_clan_territory(2u);
			bool flag2 = variant != null;
			if (flag2)
			{
				this.igLevelMsg.get_clanter_info(GameTools.createGroup(new Variant[]
				{
					"tp",
					1,
					"clteid",
					variant["id"]
				}));
			}
		}

		public bool IsWaitingStart()
		{
			return this.in_level && this._curWaitTm != 0;
		}

		public void SetCurWaitTm(int tm)
		{
			this._curWaitTm = tm;
		}

		public void EnterArenaLvl(Variant data)
		{
			this._enter_lvl_npc = 0u;
			bool flag = data.ContainsKey("arenaid") && data["arenaid"];
			if (flag)
			{
				Variant variant = this.muCCfg.svrLevelConf.get_arena_level(data["arenaid"]);
				bool flag2 = variant && variant["battle_lvl"];
				if (flag2)
				{
					int llid = data.ContainsKey("llid") ? data["llid"]._int : this.get_arena_llid(data["arenaid"]);
					uint ltpid = 0u;
					this.enter_lvl((uint)llid, ltpid, 0u, true, 0u);
				}
			}
			else
			{
				bool flag3 = data.ContainsKey("arenaexid") && data["arenaexid"];
				if (flag3)
				{
				}
			}
		}

		public int GetLvlArenaID(uint ltpid)
		{
			bool flag = ltpid > 0u;
			int result;
			if (flag)
			{
				Variant variant = muCLientConfig.instance.svrLevelConf.get_level_data(ltpid);
				bool flag2 = variant.ContainsKey("arenaid");
				if (flag2)
				{
					result = variant["arenaid"];
					return result;
				}
			}
			result = 0;
			return result;
		}

		public int GetLvlArenaexID(uint ltpid)
		{
			bool flag = ltpid > 0u;
			int result;
			if (flag)
			{
				Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrLevelConf.get_level_data(ltpid);
				bool flag2 = variant.ContainsKey("arenaexid");
				if (flag2)
				{
					result = variant["arenaexid"];
					return result;
				}
			}
			result = 0;
			return result;
		}

		public bool IsInBattleSrvLvl()
		{
			bool flag = this.in_level && this._curltpid != 0;
			bool result;
			if (flag)
			{
				int lvlArenaID = this.GetLvlArenaID((uint)this._curltpid);
				bool flag2 = lvlArenaID != 0;
				if (flag2)
				{
					Variant variant = this.muCCfg.svrLevelConf.get_arena_level((uint)lvlArenaID);
					bool flag3 = variant.ContainsKey("battle_lvl");
					if (flag3)
					{
						result = variant["battle_lvl"];
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public bool IsInMoreBattleSrvLvl()
		{
			bool flag = this._curltpid == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant variant = this.muCCfg.svrLevelConf.get_level_data((uint)this._curltpid);
				bool flag2 = !variant;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = variant.ContainsKey("arenaexid");
					result = flag3;
				}
			}
			return result;
		}

		public void on_battle_do_res(Variant data)
		{
			int @int = data["tp"]._int;
			if (@int == 5)
			{
				bool flag = data.ContainsKey("pinfo");
				if (flag)
				{
				}
			}
		}

		public void battle_do()
		{
		}

		public void SetLvlShare(Variant lvlshare)
		{
			this._lvlshare = lvlshare;
		}

		public Variant GetLvlShareByTp(uint tp)
		{
			Variant result;
			foreach (Variant current in this._lvlshare._arr)
			{
				bool flag = current["tp"] == tp;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public void RefreshLvlShare(Variant data)
		{
			bool flag = false;
			foreach (Variant current in this._lvlshare._arr)
			{
				bool flag2 = current["tp"] == data["tp"];
				if (flag2)
				{
					flag = true;
					GameTools.assignProp(current, data);
					break;
				}
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this._lvlshare._arr.Add(data);
			}
		}
	}
}
