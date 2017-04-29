using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class lgGDClans : lgGDBase
	{
		protected double _clanlist_expire = 0.0;

		protected uint _clanlist_totalcnt = 0u;

		protected uint _clanplys_totalcnt = 0u;

		protected uint _clanplys_olcnt = 0u;

		public const int LEADER = 2;

		public const int PRESBYTER = 1;

		public const int MEMBER = 0;

		protected Variant _clan_baseinfo;

		protected Variant _clan_logs;

		protected Variant _clan_reqs;

		protected List<Variant> _clan_list = new List<Variant>();

		protected Variant _self_clan_info;

		protected Variant _clan_plys;

		protected Variant _clan_donates;

		protected Variant _clan_name;

		protected Variant _clan_info;

		protected Dictionary<int, List<Action<uint, Variant>>> _query_clinfo_cbs = new Dictionary<int, List<Action<uint, Variant>>>();

		protected Dictionary<string, List<Action<Variant>>> _query_clid_cbs = new Dictionary<string, List<Action<Variant>>>();

		private int _freshNum = 8;

		private bool _loadSelf = false;

		private Variant _changePlyClancInfo;

		private Variant _day_record = new Variant();

		public bool selfIsClanLeader
		{
			get
			{
				return this._self_clan_info != null && this._self_clan_info["clanc"]._int == 2;
			}
		}

		public bool selfIsClanPresbyter
		{
			get
			{
				return this._self_clan_info && this._self_clan_info["clanc"]._int == 1;
			}
		}

		protected LGIUIMainUI _lgMainUi
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			}
		}

		protected LGIUIMission _lgiui_mis
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("UI_MISSION") as LGIUIMission;
			}
		}

		public lgGDClans(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDClans(m as gameManager);
		}

		public override void init()
		{
			this._clan_name = new Variant();
			this._clan_info = new Variant();
			this._query_clinfo_cbs = new Dictionary<int, List<Action<uint, Variant>>>();
			this._query_clid_cbs = new Dictionary<string, List<Action<Variant>>>();
		}

		protected void _set_clan_name(uint clanid, string clannm)
		{
			this._clan_name[clannm] = clanid;
		}

		public void get_claninfo_by_clanid(uint clanid, Action<uint, Variant> on_fin)
		{
			bool flag = this._clan_info.ContainsKey((int)clanid);
			if (flag)
			{
				on_fin(clanid, this._clan_info[clanid]);
			}
			else
			{
				bool flag2 = !this._query_clinfo_cbs.ContainsKey((int)clanid);
				if (flag2)
				{
					this._query_clinfo_cbs[(int)clanid] = new List<Action<uint, Variant>>();
				}
				this._query_clinfo_cbs[(int)clanid].Add(on_fin);
				(this.g_mgr.g_netM as muNetCleint).igClanMsgs.query_clinfo(clanid, 0u);
			}
		}

		public void on_query_clinfo_res(Variant data)
		{
			this._clan_name[data["name"]] = data["id"];
			this._clan_info[data["id"]] = data;
			bool flag = this._query_clinfo_cbs.ContainsKey(data["id"]._int);
			if (flag)
			{
				List<Action<uint, Variant>> list = this._query_clinfo_cbs[data["id"]._int];
				for (int i = 0; i < list.Count; i++)
				{
					Action<uint, Variant> action = list[i];
					action(data["id"], data);
				}
				this._query_clinfo_cbs.Remove(data["id"]);
			}
		}

		public void get_clanid_by_clannm(string clannm, Action<Variant> on_fin)
		{
			bool flag = this._clan_name.ContainsKey(clannm);
			if (flag)
			{
				on_fin(this._clan_name[clannm]);
			}
			else
			{
				bool flag2 = !this._query_clid_cbs.ContainsKey(clannm);
				if (flag2)
				{
					this._query_clid_cbs[clannm] = null;
				}
				this._query_clid_cbs[clannm].Add(on_fin);
				(this.g_mgr.g_netM as muNetCleint).igClanMsgs.query_clan_id(clannm);
			}
		}

		public void on_query_clan_id_res(Variant data)
		{
			this._clan_name[data["name"]] = data["id"];
			bool flag = this._query_clid_cbs.ContainsKey(data["name"]);
			if (flag)
			{
				List<Action<Variant>> list = this._query_clid_cbs[data["name"]];
				for (int i = 0; i < list.Count; i++)
				{
					Action<Variant> action = list[i];
					action(this._clan_name[data["name"]]);
				}
				this._query_clid_cbs.Remove(data["name"]);
			}
		}

		public void get_clan_plys(uint begin_idx, uint end_idx)
		{
			(this.g_mgr.g_netM as muNetCleint).igClanMsgs.get_clanpls(begin_idx, end_idx);
		}

		public void get_all_clan_plys(uint begin_idx, uint end_idx)
		{
			uint num = Convert.ToUInt32(Math.Ceiling((end_idx - begin_idx) / (double)this._freshNum));
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				uint num3 = Convert.ToUInt32((long)((ulong)begin_idx + (ulong)((long)(this._freshNum * num2))));
				uint num4 = Convert.ToUInt32((long)((ulong)num3 + (ulong)((long)this._freshNum)));
				bool flag = num4 > end_idx;
				if (flag)
				{
					num4 = end_idx;
				}
				(this.g_mgr.g_netM as muNetCleint).igClanMsgs.get_clanpls(num3, num4);
				num2++;
			}
		}

		public void on_clan_plys_list(Variant data)
		{
			this._clanplys_totalcnt = data["total_cnt"];
			this._clanplys_olcnt = data["ol_cnt"];
			bool flag = this._clan_plys == null;
			if (flag)
			{
				this._clan_plys = new Variant();
			}
			for (int i = 0; i < data["pls"].Count; i++)
			{
				this._clan_plys[(i + data["begin_idx"]._int).ToString()] = data["pls"][i];
			}
			bool flag2 = data["begin_idx"] == 0;
			if (flag2)
			{
				this.setClanList();
			}
		}

		private void setClanList()
		{
			this._reset_selfName();
		}

		public void get_clan_list(uint begin_idx, uint end_idx)
		{
			uint num = Convert.ToUInt32((this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS);
			bool flag = this._clanlist_expire < num;
			if (flag)
			{
				this._clan_list = null;
			}
			bool flag2 = this._clan_list == null;
			if (flag2)
			{
				(this.g_mgr.g_netM as muNetCleint).igClanMsgs.get_clans_list(begin_idx, end_idx);
			}
			else
			{
				Variant variant = new Variant();
				int num2 = (int)begin_idx;
				while (num2 < this._clan_list.Count && (long)num2 < (long)((ulong)end_idx))
				{
					variant.pushBack(this._clan_list[num2]);
					num2++;
				}
			}
		}

		public void on_clan_list(Variant data)
		{
			this._clanlist_expire = Convert.ToUInt32((this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS + 300000L);
			this._clanlist_totalcnt = data["total_cnt"];
			for (int i = 0; i < data["list"].Count; i++)
			{
				this._clan_list = new List<Variant>();
				this._clan_list.Add(data["list"][i]);
			}
		}

		public void OnCreateClan(Variant data)
		{
		}

		public void set_clan_baseinfo(Variant data)
		{
			bool flag = this._clan_baseinfo != null;
			if (flag)
			{
				GameTools.assignProp(this._clan_baseinfo, data);
			}
			else
			{
				this._clan_baseinfo = data;
			}
			bool flag2 = this._clan_baseinfo.ContainsKey("day_record");
			if (flag2)
			{
				this._day_record = this._clan_baseinfo["day_record"];
			}
		}

		public void add_clan_req(Variant data)
		{
			bool flag = this._clan_reqs == null;
			if (flag)
			{
				this._clan_reqs = data;
			}
			else
			{
				this._clan_reqs.pushBack(data);
			}
			data["tm"] = (this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L;
			this._lgMainUi.add_clan_req(data);
		}

		public void remove_clan_req(uint cid)
		{
			bool flag = this._clan_reqs == null;
			if (!flag)
			{
				for (int i = 0; i < this._clan_reqs.Count; i++)
				{
					bool flag2 = this._clan_reqs[i]["cid"] == cid;
					if (flag2)
					{
						this._clan_reqs._arr.RemoveAt(i);
						break;
					}
				}
			}
		}

		public void add_clan_ply(Variant data)
		{
			bool flag = data["cid"] != (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["cid"];
			if (flag)
			{
				string text = LanguagePack.getLanguageText("faction", "pl_join_clan");
				text = DebugTrace.Printf(text, new string[]
				{
					data["name"]
				});
				this._lgMainUi.systemmsg(text, 1024u);
				text = LanguagePack.getLanguageText("faction", "join_clan");
				this._lgMainUi.ShowClanEvent(DebugTrace.Printf(text, new string[]
				{
					data["name"]
				}), "welcome", data["name"], 4);
			}
			bool flag2 = this._clan_baseinfo != null && this._clan_baseinfo["plycnt"] != null;
			if (flag2)
			{
				Variant expr_FF = this._clan_baseinfo;
				Variant val = expr_FF["plycnt"];
				expr_FF["plycnt"] = val + 1;
			}
			bool flag3 = data["cid"] != (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["cid"];
			if (flag3)
			{
				bool flag4 = this._clan_plys == null;
				if (flag4)
				{
					this._clan_plys = data;
				}
				else
				{
					this._clan_plys.pushBack(data);
				}
			}
		}

		public void pl_leave_clan(Variant data)
		{
			bool flag = this._clan_baseinfo != null && this._clan_baseinfo["plycnt"] != null;
			if (flag)
			{
				Variant expr_2A = this._clan_baseinfo;
				Variant val = expr_2A["plycnt"];
				expr_2A["plycnt"] = val - 1;
			}
			bool flag2 = this._clan_plys != null;
			if (flag2)
			{
				for (int i = 0; i < this._clan_plys.Count; i++)
				{
					Variant variant = this._clan_plys["0"];
					bool flag3 = variant["cid"] == data["cid"];
					if (flag3)
					{
						this._clan_plys._arr.RemoveAt(i);
						break;
					}
				}
			}
			bool flag4 = data["cid"] == (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["cid"];
			if (flag4)
			{
				(this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["clanid"] = 0;
				this._clan_baseinfo = null;
				this.clan_dismiss();
			}
			bool flag5 = !data.ContainsKey("name");
			if (flag5)
			{
				data["name"] = "";
			}
		}

		public void set_clan_self_info(Variant data)
		{
			this._loadSelf = false;
			bool flag = this._self_clan_info == null;
			if (flag)
			{
				this._self_clan_info = data;
			}
			else
			{
				using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator = data.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						this._self_clan_info[key] = data[key];
					}
				}
			}
			this._clanlist_expire = 0.0;
			bool flag2 = this._clan_baseinfo == null;
			if (flag2)
			{
				(this.g_mgr.g_netM as muNetCleint).igClanMsgs.get_clan_base_info();
			}
			(this.g_mgr.g_gameM as muLGClient).g_generalCT.clang = this._self_clan_info["clang"];
			(this.g_mgr.g_gameM as muLGClient).g_generalCT.cur_clanagld = this._self_clan_info["cur_clanagld"];
			(this.g_mgr.g_gameM as muLGClient).g_generalCT.cur_clanayb = this._self_clan_info["cur_clanayb"];
		}

		public Variant GetSelfInfo()
		{
			bool loadSelf = this._loadSelf;
			Variant result;
			if (loadSelf)
			{
				result = null;
			}
			else
			{
				bool flag = this._self_clan_info == null;
				if (flag)
				{
					this._loadSelf = true;
					(this.g_mgr.g_netM as muNetCleint).igClanMsgs.get_clan_selfinfo();
				}
				result = this._self_clan_info;
			}
			return result;
		}

		public void self_info_change(Variant data)
		{
			bool flag = this._self_clan_info != null;
			if (flag)
			{
				using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator = data.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						this._self_clan_info[key] = data[key];
					}
				}
			}
		}

		public void clan_info_change(Variant data)
		{
			bool flag = this._clan_baseinfo != null;
			if (flag)
			{
				bool flag2 = data != null;
				if (flag2)
				{
					this._deleteCostItem(this._clan_baseinfo["itms_donate"], data["cost_itms"]);
				}
				bool flag3 = data != null && data["day_record"] != null;
				if (flag3)
				{
					this.SetClanDayRecord(data["day_record"]);
				}
				bool flag4 = data.ContainsKey("lvl") && data.ContainsKey("clan_pt");
				if (flag4)
				{
					(this.g_mgr.g_gameM as muLGClient).g_generalCT.clan_pt -= data["clan_pt"];
				}
				else
				{
					bool flag5 = data.ContainsKey("clan_pt");
					if (flag5)
					{
						(this.g_mgr.g_gameM as muLGClient).g_generalCT.clan_pt = data["clan_pt"];
					}
				}
				foreach (string current in data.Keys)
				{
					bool flag6 = current == "clan_pt";
					if (flag6)
					{
						this._clan_baseinfo["clan_pt"] = (this.g_mgr.g_gameM as muLGClient).g_generalCT.clan_pt;
					}
					else
					{
						bool flag7 = current != "day_record";
						if (flag7)
						{
							this._clan_baseinfo[current] = data[current];
						}
					}
				}
			}
		}

		public void change_ply_clanc(Variant data)
		{
			bool flag = this._clan_baseinfo == null;
			if (flag)
			{
				this._changePlyClancInfo = data;
				(this.g_mgr.g_netM as muNetCleint).igClanMsgs.get_clan_base_info();
			}
			else
			{
				Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
				bool flag2 = data["cid"] == mainPlayerInfo["cid"];
				string text = "";
				bool flag3 = data["clanc"] == 2;
				if (flag3)
				{
					int @int = data["atp"]._int;
					bool flag4 = @int == 1;
					if (flag4)
					{
						string text2 = LanguagePack.getLanguageText("faction", "fire_leader");
						text2 = DebugTrace.Printf(text2, new string[]
						{
							data["name"]._str,
							this._clan_baseinfo["clname"]._str
						});
						this._lgMainUi.ShowClanEvent(text2, "", "", 4);
					}
					else
					{
						string text2 = LanguagePack.getLanguageText("faction", "change_leader");
						text2 = DebugTrace.Printf(text2, new string[]
						{
							this._clan_baseinfo["name"]._str,
							data["name"]._str
						});
						this._lgMainUi.ShowClanEvent(text2, "", "", 4);
						bool flag5 = flag2;
						if (flag5)
						{
							text = DebugTrace.Printf(LanguagePack.getLanguageText("faction", "change_leader_getter"), new string[]
							{
								this._clan_baseinfo["name"]._str
							});
						}
						else
						{
							bool flag6 = this._clan_info["pcid"] == mainPlayerInfo["cid"];
							if (flag6)
							{
								text = DebugTrace.Printf(LanguagePack.getLanguageText("faction", "change_leader_setter"), new string[]
								{
									data["name"]._str
								});
							}
							else
							{
								text = DebugTrace.Printf(LanguagePack.getLanguageText("faction", "change_leader_other"), new string[]
								{
									this._clan_baseinfo["name"]._str,
									data["name"]._str
								});
							}
						}
					}
					this._clan_info["pcid"] = data["cid"];
					this._clan_info["name"] = data["name"];
				}
				else
				{
					bool flag7 = data["clanc"] == 1;
					if (flag7)
					{
						string text2 = LanguagePack.getLanguageText("faction", "change_presbyter");
						text2 = DebugTrace.Printf(text2, new string[]
						{
							this._clan_baseinfo["name"]._str,
							data["name"]._str
						});
						this._lgMainUi.ShowClanEvent(text2, "", "", 4);
						bool flag8 = flag2;
						if (flag8)
						{
							text = DebugTrace.Printf(LanguagePack.getLanguageText("faction", "change_presbyter_getter"), new string[]
							{
								this._clan_baseinfo["name"]._str
							});
						}
						else
						{
							bool flag9 = this._clan_info["pcid"] == mainPlayerInfo["cid"];
							if (flag9)
							{
								text = DebugTrace.Printf(LanguagePack.getLanguageText("faction", "change_presbyter_setter"), new string[]
								{
									data["name"]._str
								});
							}
							else
							{
								text = DebugTrace.Printf(LanguagePack.getLanguageText("faction", "change_presbyter_other"), new string[]
								{
									this._clan_baseinfo["name"]._str,
									data["name"]._str
								});
							}
						}
					}
					else
					{
						bool flag10 = data.ContainsKey("oldclanc");
						if (flag10)
						{
							string text2 = LanguagePack.getLanguageText("faction", "change_member");
							text2 = DebugTrace.Printf(text2, new string[]
							{
								this._clan_baseinfo["name"]._str,
								data["name"]._str
							});
							this._lgMainUi.ShowClanEvent(text2, "", "", 4);
							bool flag11 = flag2;
							if (flag11)
							{
								text = DebugTrace.Printf(LanguagePack.getLanguageText("faction", "change_member_getter"), new string[]
								{
									this._clan_baseinfo["name"]._str
								});
							}
							else
							{
								bool flag12 = this._clan_info["pcid"] == mainPlayerInfo["cid"];
								if (flag12)
								{
									text = DebugTrace.Printf(LanguagePack.getLanguageText("faction", "change_member_setter"), new string[]
									{
										data["name"]._str
									});
								}
								else
								{
									text = DebugTrace.Printf(LanguagePack.getLanguageText("faction", "change_member_other"), new string[]
									{
										this._clan_baseinfo["name"]._str,
										data["name"]._str
									});
								}
							}
						}
					}
				}
				bool flag13 = text != "";
				if (flag13)
				{
					Variant variant = new Variant();
					variant.setToArray();
					variant.pushBack(text);
					this._lgMainUi.systemmsg(variant, 1024u);
				}
				bool flag14 = this._clan_plys != null;
				if (flag14)
				{
					for (int i = 0; i < this._clan_plys.Count; i++)
					{
						Variant variant2 = this._clan_plys[i];
						bool flag15 = variant2["cid"] == data["cid"];
						if (flag15)
						{
							variant2["clanc"] = data["clanc"];
							bool flag16 = data["clanc"] >= 2;
							if (flag16)
							{
								bool flag17 = this._clan_baseinfo != null;
								if (flag17)
								{
									this._clan_baseinfo["pcid"] = data["cid"];
									this._clan_baseinfo["name"] = data["name"];
								}
							}
							break;
						}
					}
				}
				bool flag18 = this._self_clan_info != null;
				if (flag18)
				{
					this._self_clan_info["clanc"] = data["clanc"];
				}
			}
		}

		public void clan_dismiss()
		{
			this._clan_baseinfo = null;
			this._clan_logs = null;
			this._clan_reqs = null;
			this._clan_list = null;
			this._self_clan_info = null;
			this._clan_plys = null;
			this._clan_donates = null;
			this._day_record = null;
			(this.g_mgr.g_gameM as muLGClient).g_generalCT.clan_pt = 0;
		}

		public void get_clan_info_res(Variant data)
		{
			switch (data["tp"]._int)
			{
			case 1:
			{
				this._clan_baseinfo = data["info"];
				this._reset_selfName();
				(this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["clanid"] = data["info"]["id"];
				bool flag = this._clan_baseinfo.ContainsKey("day_record");
				if (flag)
				{
					this._day_record = this._clan_baseinfo["day_record"];
				}
				bool flag2 = this._clan_baseinfo.ContainsKey("clan_pt");
				if (flag2)
				{
					(this.g_mgr.g_gameM as muLGClient).g_generalCT.clan_pt = this._clan_baseinfo["clan_pt"];
				}
				bool flag3 = this._changePlyClancInfo != null;
				if (flag3)
				{
					this.change_ply_clanc(this._changePlyClancInfo);
					this._changePlyClancInfo = null;
				}
				break;
			}
			case 2:
				this._clan_donates = data["info"];
				break;
			case 3:
				this._clan_logs = data["info"];
				break;
			case 4:
				this._clan_reqs = data["info"];
				this._lgMainUi.set_clan_reqs(this._clan_reqs);
				break;
			}
		}

		public void GetClanReqs(bool normal = true)
		{
			if (normal)
			{
				(this.g_mgr.g_netM as muNetCleint).igClanMsgs.get_clan_reqs();
			}
			else
			{
				bool flag = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["clanid"] != 0;
				if (flag)
				{
					(this.g_mgr.g_netM as muNetCleint).igClanMsgs.get_clan_reqs();
				}
			}
		}

		protected void _showMsg(string str, uint type = 8u)
		{
			bool flag = this._lgMainUi != null;
			if (flag)
			{
			}
		}

		public void JoinClan(uint clanid)
		{
			bool flag = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["clanid"] != 0;
			if (flag)
			{
				string languageText = LanguagePack.getLanguageText("ServerErrorCode", "-2139");
				this._showMsg(languageText, 1024u);
			}
			else
			{
				bool flag2 = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["level"] < (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.get_game_general_data("join_clan_lvl");
				if (!flag2)
				{
					(this.g_mgr.g_netM as muNetCleint).igClanMsgs.join_clan_req(clanid);
				}
			}
		}

		public void ChangeClanDirectJoin(int flag)
		{
			(this.g_mgr.g_netM as muNetCleint).igClanMsgs.ChangeClanDirectJoin(flag);
		}

		public Variant GetClanPlys()
		{
			return this._clan_plys;
		}

		public uint GetClanPlysNum()
		{
			return this._clan_baseinfo["plycnt"];
		}

		public Variant GetClanBaseInfo()
		{
			return this._clan_baseinfo;
		}

		public void Refresh_clan_baseinfo()
		{
			this._reset_selfName();
		}

		private void _reset_selfName()
		{
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			bool flag = this._clan_baseinfo != null && this._clan_baseinfo["id"] == mainPlayerInfo["clanid"];
			if (flag)
			{
				bool flag2 = this._clan_plys != null;
				if (flag2)
				{
					foreach (Variant current in this._clan_plys.Values)
					{
						bool flag3 = current["cid"] == mainPlayerInfo["cid"];
						if (flag3)
						{
							current["name"] = mainPlayerInfo["name"];
							break;
						}
					}
				}
				bool flag4 = this._clan_baseinfo["pcid"] == mainPlayerInfo["cid"];
				if (flag4)
				{
					this._clan_baseinfo["name"] = mainPlayerInfo["name"];
				}
			}
		}

		public void clan_donate(int gld = -1, int yb = -1, Variant itms = null)
		{
			(this.g_mgr.g_netM as muNetCleint).igClanMsgs.clan_donate(gld, yb, itms);
		}

		public void set_items_donate(Variant items)
		{
			this._clan_baseinfo["itms_donate"] = items;
		}

		private void _deleteCostItem(Variant oldItems, Variant costItems)
		{
			bool flag = oldItems != null && costItems != null;
			if (flag)
			{
				foreach (Variant current in costItems.Values)
				{
					for (int i = 0; i < oldItems.Count; i++)
					{
						Variant variant = oldItems[i];
						bool flag2 = variant["tpid"] == current["tpid"];
						if (flag2)
						{
							Variant variant2 = variant;
							variant2["cnt"] = variant2["cnt"] - current["cnt"];
							bool flag3 = variant["cnt"] <= 0;
							if (flag3)
							{
								oldItems._arr.RemoveAt(i);
							}
						}
					}
				}
			}
		}

		public void SetClanDayRecord(Variant data)
		{
			bool flag = this._day_record["d"] == data["d"] && this._day_record["mon"] == data["mon"];
			if (flag)
			{
				foreach (string current in data.Keys)
				{
					this._day_record[current] = data[current];
				}
			}
			else
			{
				this._day_record = data;
			}
			bool flag2 = this._clan_baseinfo;
			if (flag2)
			{
				this._clan_baseinfo["day_record"] = this._day_record;
			}
		}

		public Variant GetClanDayRecord()
		{
			return this._day_record;
		}
	}
}
