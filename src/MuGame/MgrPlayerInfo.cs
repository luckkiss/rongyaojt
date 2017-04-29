using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class MgrPlayerInfo : lgGDBase
	{
		protected Dictionary<uint, List<Action<Variant>>> _playerShowInfoCBs = null;

		protected Dictionary<uint, List<Action<Variant>>> _playerDetailInfoCBs = null;

		protected Dictionary<uint, Action<Variant>> _playerDressChangeCBs = null;

		protected Dictionary<string, List<Action<string, uint>>> _playerCidCBs = null;

		protected Dictionary<uint, List<Action<uint, Variant>>> _playerQueryInfoCBs = null;

		protected Variant _playerInfos = null;

		protected Variant _playercid = null;

		protected Dictionary<uint, List<Action<uint, string>>> _playerNameCBs = null;

		protected Variant _playInfoCache = null;

		protected Dictionary<string, List<Action<string, Variant>>> _playerInfoNameCBs = null;

		protected Variant _playerInfoNameCache = null;

		private InGamePalyerInfoMsgs igPlyMsg
		{
			get
			{
				return (this.g_mgr.g_netM as muNetCleint).getObject("MSG_PALYER_INFO") as InGamePalyerInfoMsgs;
			}
		}

		private muLGClient muLgClt
		{
			get
			{
				return this.g_mgr.g_gameM as muLGClient;
			}
		}

		private LGIUIMainUI mainui
		{
			get
			{
				return (this.g_mgr.g_uiM as muUIClient).getObject("main") as LGIUIMainUI;
			}
		}

		private LGOthers lgothers
		{
			get
			{
				return this.g_mgr.g_gameM.getObject("LG_OTHER_PLAYERS") as LGOthers;
			}
		}

		private LGMonsters lgmonsters
		{
			get
			{
				return this.g_mgr.g_gameM.getObject("LG_MONSTERS") as LGMonsters;
			}
		}

		public MgrPlayerInfo(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new MgrPlayerInfo(m as gameManager);
		}

		public override void init()
		{
			this._playerShowInfoCBs = new Dictionary<uint, List<Action<Variant>>>();
			this._playerDetailInfoCBs = new Dictionary<uint, List<Action<Variant>>>();
			this._playerDressChangeCBs = new Dictionary<uint, Action<Variant>>();
			this._playerInfos = new Variant();
			this._playercid = new Variant();
			this._playerCidCBs = new Dictionary<string, List<Action<string, uint>>>();
			this._playerQueryInfoCBs = new Dictionary<uint, List<Action<uint, Variant>>>();
			this._playerNameCBs = new Dictionary<uint, List<Action<uint, string>>>();
			this._playInfoCache = new Variant();
			this._playerInfoNameCBs = new Dictionary<string, List<Action<string, Variant>>>();
			this._playerInfoNameCache = new Variant();
			this.g_mgr.g_netM.addEventListener(51u, new Action<GameEvent>(this.on_player_show_info));
			this.g_mgr.g_netM.addEventListener(52u, new Action<GameEvent>(this.on_player_detail_info));
			this.g_mgr.g_netM.addEventListener(78u, new Action<GameEvent>(this.on_view_avatar_change));
		}

		public void checkShowInfoRev(uint cid, uint rev, int iid)
		{
			bool flag = this._playerInfos.ContainsKey((int)cid) && this._playerInfos[cid]["appendShowInfo"];
			if (flag)
			{
				bool flag2 = this._playerInfos[cid]["data"]["rev"] != rev || this._playerInfos[cid]["data"]["iid"] != iid;
				if (flag2)
				{
					this._playerInfos.RemoveKey("cid");
				}
			}
		}

		public void get_player_showinfo(uint cid, Action<Variant> onFin)
		{
			bool flag = this._playerInfos.ContainsKey((int)cid) && this._playerInfos[cid]["appendShowInfo"];
			if (flag)
			{
				onFin(this._playerInfos[cid]["data"]);
			}
			else
			{
				bool flag2 = !this._playerShowInfoCBs.ContainsKey(cid);
				if (flag2)
				{
					this._playerShowInfoCBs[cid] = new List<Action<Variant>>();
					this._playerShowInfoCBs[cid].Add(onFin);
					Variant variant = new Variant();
					variant.setToArray();
					variant.pushBack(cid);
					this.igPlyMsg.PlayerShowInfo(variant);
				}
				else
				{
					bool flag3 = false;
					foreach (Action<Variant> current in this._playerShowInfoCBs[cid])
					{
						bool flag4 = current == onFin;
						if (flag4)
						{
							flag3 = true;
							break;
						}
					}
					bool flag5 = !flag3;
					if (flag5)
					{
						this._playerShowInfoCBs[cid].Add(onFin);
					}
				}
			}
		}

		public void addActionDressChange(uint cid, Action<Variant> onFin)
		{
			this._playerDressChangeCBs[cid] = onFin;
		}

		public void get_player_detailinfo(uint cid, Action<Variant> onFin)
		{
			bool flag = this._playerInfos.ContainsKey((int)cid) && this._playerInfos[cid]["appendDetailInfo"];
			if (flag)
			{
				onFin(this._playerInfos[cid]["data"]);
			}
			else
			{
				bool flag2 = !this._playerDetailInfoCBs.ContainsKey(cid);
				if (flag2)
				{
					this._playerDetailInfoCBs[cid] = new List<Action<Variant>>();
					this._playerDetailInfoCBs[cid].Add(onFin);
					this.igPlyMsg.PlayerDetailInfo(cid);
				}
				else
				{
					this._playerDetailInfoCBs[cid].Add(onFin);
				}
			}
		}

		public Variant get_playerinfo_ifexist(uint cid)
		{
			bool flag = this._playerInfos.ContainsKey((int)cid);
			Variant result;
			if (flag)
			{
				result = this._playerInfos[cid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_player_detailinfo_ifexist(uint cid)
		{
			bool flag = !this._playerInfos.ContainsKey((int)cid);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !this._playerInfos[cid]["appendDetailInfo"];
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = this._playerInfos[cid]["data"];
				}
			}
			return result;
		}

		public void on_player_show_info(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data == null || !data.ContainsKey("pary");
			if (flag)
			{
				bool flag2 = data != null;
				if (flag2)
				{
					GameTools.PrintError("on_player_show_info dump:" + data.dump());
				}
				else
				{
					GameTools.PrintError("on_player_show_info data null!");
				}
			}
			else
			{
				for (int i = 0; i < data["pary"].Length; i++)
				{
					Variant variant = data["pary"]._arr[i];
					for (int j = 0; j < this._playerShowInfoCBs[variant["cid"]._uint].Count; j++)
					{
						Action<Variant> action = this._playerShowInfoCBs[variant["cid"]._uint][j];
						action(data);
					}
					this._playerShowInfoCBs.Remove(variant["cid"]._uint);
					this._playercid[variant["name"]._str] = variant["cid"];
				}
			}
		}

		public void on_view_avatar_change(GameEvent e)
		{
			Variant data = e.data;
			uint @uint = data["cid"]._uint;
			bool flag = this._playerDressChangeCBs.ContainsKey(@uint);
			if (flag)
			{
				this._playerDressChangeCBs[@uint](data);
			}
		}

		public void on_player_detail_info(GameEvent e)
		{
			Variant data = e.data;
			for (int i = 0; i < this._playerDetailInfoCBs[data["cid"]._uint].Count; i++)
			{
				Action<Variant> action = this._playerDetailInfoCBs[data["cid"]._uint][i];
				action(data);
			}
			this._playerDetailInfoCBs.Remove(data["cid"]._uint);
			this._playercid[data["name"]._str] = data["cid"];
		}

		public Variant removePlayerInfo(uint cid)
		{
			Variant result = null;
			bool flag = this._playerInfos.ContainsKey((int)cid);
			if (flag)
			{
				result = this._playerInfos[cid];
				this._playerInfos.RemoveKey(cid.ToString());
			}
			return result;
		}

		public void get_player_cid_by_name(string nm, bool ol, Action<string, uint> onFin)
		{
			bool flag = this._playercid.ContainsKey(nm);
			if (flag)
			{
				onFin(nm, this._playercid[nm]);
			}
			else
			{
				bool flag2 = !this._playerCidCBs.ContainsKey(nm);
				if (flag2)
				{
					this._playerCidCBs[nm] = new List<Action<string, uint>>();
					this._playerCidCBs[nm].Add(onFin);
					this.igPlyMsg.on_get_user_cid_res(nm, ol, 0u);
				}
				else
				{
					this._playerCidCBs[nm].Add(onFin);
				}
			}
		}

		public void get_player_info_by_name(string nm, bool ol, Action<string, Variant> onFin)
		{
			bool flag = this._playerInfoNameCBs.ContainsKey(nm);
			if (flag)
			{
				onFin(nm, this._playerInfoNameCache[nm]);
			}
			else
			{
				bool flag2 = !this._playerInfoNameCBs.ContainsKey(nm);
				if (flag2)
				{
					this._playerInfoNameCBs[nm] = new List<Action<string, Variant>>();
					this._playerInfoNameCBs[nm].Add(onFin);
					this.igPlyMsg.on_get_user_cid_res(nm, ol, 0u);
				}
				else
				{
					this._playerInfoNameCBs[nm].Add(onFin);
				}
			}
		}

		public void on_get_user_cid_res(Variant data)
		{
			bool flag = data["cid"] > 0;
			if (flag)
			{
				this._playercid[data["name"]] = data["cid"];
			}
			this._playerInfoNameCache[data["name"]] = data;
			List<Action<string, uint>> list = this._playerCidCBs[data["name"]._str];
			bool flag2 = list.Count != 0;
			if (flag2)
			{
				for (int i = 0; i < list.Count; i++)
				{
					Action<string, uint> action = list[i];
					action(data["name"], data["cid"]);
				}
				this._playerCidCBs.Remove(data["name"]);
			}
			List<Action<string, Variant>> list2 = new List<Action<string, Variant>>();
			bool flag3 = this._playerInfoNameCBs.Count != 0;
			if (flag3)
			{
				list2 = this._playerInfoNameCBs[data["name"]._str];
			}
			bool flag4 = list2.Count != 0;
			if (flag4)
			{
				for (int i = 0; i < list2.Count; i++)
				{
					Action<string, Variant> action2 = list2[i];
					action2(data["name"], data);
				}
				this._playerInfoNameCBs.Remove(data["name"]);
			}
		}

		public void query_ply_info(uint cid, Action<uint, Variant> onFin, int tp = -1, bool bslvl = false)
		{
			bool flag = !this._playerQueryInfoCBs.ContainsKey(cid);
			if (flag)
			{
				this._playerQueryInfoCBs[cid] = new List<Action<uint, Variant>>();
				this._playerQueryInfoCBs[cid].Add(onFin);
				Variant variant = GameTools.createGroup(new Variant[]
				{
					"cid",
					cid
				});
				bool flag2 = tp > 0;
				if (flag2)
				{
					variant["tp"] = tp;
				}
				if (bslvl)
				{
					variant["bslvl"] = bslvl;
				}
				this.igPlyMsg.on_query_ply_info_res(variant);
			}
			else
			{
				bool flag3 = false;
				foreach (Action<uint, Variant> current in this._playerQueryInfoCBs[cid])
				{
					bool flag4 = current == onFin;
					if (flag4)
					{
						flag3 = true;
						break;
					}
				}
				bool flag5 = !flag3;
				if (flag5)
				{
					this._playerQueryInfoCBs[cid].Add(onFin);
				}
			}
		}

		public void on_query_ply_info_res(Variant data)
		{
			this._playercid[data["name"]] = data["cid"];
			float val = (float)(GameTools.getTimer() + 300000L);
			this._playInfoCache[data["cid"]] = GameTools.createGroup(new Variant[]
			{
				"tm",
				val,
				"data",
				data
			});
			List<Action<uint, Variant>> list = this._playerQueryInfoCBs[data["cid"]._uint];
			bool flag = list.Count != 0;
			if (flag)
			{
				for (int i = 0; i < list.Count; i++)
				{
					Action<uint, Variant> action = list[i];
					action(data["cid"], data);
				}
				this._playerQueryInfoCBs.Remove(data["cid"]);
			}
			List<Action<uint, string>> list2 = this._playerNameCBs.ContainsKey(data["cid"]._uint) ? this._playerNameCBs[data["cid"]._uint] : null;
			bool flag2 = list2 != null && list2.Count != 0;
			if (flag2)
			{
				for (int j = 0; j < list2.Count; j++)
				{
					Action<uint, string> action2 = list2[j];
					action2(data["cid"]._uint, data["name"]._str);
				}
				this._playerNameCBs.Remove(data["cid"]._uint);
			}
		}

		public void on_attchange(Variant msgData)
		{
		}

		public void get_player_name_by_cid(uint cid, Action<uint, string> onFin)
		{
			foreach (string current in this._playercid.Keys)
			{
				bool flag = this._playercid[current] == cid;
				if (flag)
				{
					onFin(cid, current);
					return;
				}
			}
			bool flag2 = !this._playerNameCBs.ContainsKey(cid);
			if (flag2)
			{
				this._playerNameCBs[cid] = new List<Action<uint, string>>();
				this._playerNameCBs[cid].Add(onFin);
				Variant data = GameTools.createGroup(new Variant[]
				{
					"cid",
					cid
				});
				this.igPlyMsg.on_query_ply_info_res(data);
			}
			else
			{
				this._playerNameCBs[cid].Add(onFin);
			}
		}

		public void GetPlayerInfoByCid(uint cid, Action<uint, Variant> onfin, int tp = -1, bool bslvl = false)
		{
			float num = (float)GameTools.getTimer();
			bool flag = this._playInfoCache.ContainsKey((int)cid);
			if (flag)
			{
				bool flag2 = num > this._playInfoCache[cid]["tm"];
				if (flag2)
				{
					this._playInfoCache.RemoveKey(cid.ToString());
				}
				else
				{
					bool flag3 = tp == -1 || (tp == 1 && this._playInfoCache[cid]["data"]["equip"]);
					if (flag3)
					{
						onfin(cid, this._playInfoCache[cid]["data"]);
						return;
					}
				}
			}
			this.query_ply_info(cid, onfin, tp, bslvl);
		}
	}
}
