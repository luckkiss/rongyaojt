using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class lgGDVIP : lgGDBase
	{
		private Variant skill_list = new Variant();

		protected Variant _vipData;

		protected uint _vipLevel = 0u;

		protected List<Action<Variant>> _vipresFunList = new List<Action<Variant>>();

		protected bool _hasGetAwd = false;

		protected uint _fbvip_awd = 0u;

		private bool _resetVipData;

		private bool _hasAwd = true;

		public const int NO_GIFT = 2;

		public const int GETED_GIFT = 1;

		public const int GET_GIFT = 0;

		private InGamePlyFunMsgs igPlyFunMsg
		{
			get
			{
				return (this.g_mgr.g_netM as muNetCleint).getObject("MSG_PLY_FUN") as InGamePlyFunMsgs;
			}
		}

		private InGameVIPMsgs igVIPMsg
		{
			get
			{
				return (this.g_mgr.g_netM as muNetCleint).getObject("MSG_VIP") as InGameVIPMsgs;
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

		private muLGClient muLGClt
		{
			get
			{
				return this.g_mgr.g_gameM as muLGClient;
			}
		}

		private LGIUIMainUI uiMain
		{
			get
			{
				return (this.g_mgr.g_uiM as muUIClient).getObject("LGUIMainUIImpl") as LGIUIMainUI;
			}
		}

		private LGIUIWelfare _lgui_welfare
		{
			get
			{
				return (this.g_mgr.g_uiM as muUIClient).getObject("welfare") as LGIUIWelfare;
			}
		}

		private LGIUIWorldline line
		{
			get
			{
				return (this.g_mgr.g_uiM as muUIClient).getObject("worldline") as LGIUIWorldline;
			}
		}

		private LGIUIShop shop
		{
			get
			{
				return (this.g_mgr.g_uiM as muUIClient).getObject("shop") as LGIUIShop;
			}
		}

		private LGIUIPvip pvip
		{
			get
			{
				return (this.g_mgr.g_uiM as muUIClient).getObject("pvip") as LGIUIPvip;
			}
		}

		private LGIUIVip lgiui_Vip
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUIVipImpl") as LGIUIVip;
			}
		}

		private LGIUIVipReward lgiui_VipReward
		{
			get
			{
				return (this.g_mgr.g_uiM as muUIClient).getObject("vipreward") as LGIUIVipReward;
			}
		}

		private LGIUIMission mission
		{
			get
			{
				return null;
			}
		}

		private LGIUIPvipPower pvipPower
		{
			get
			{
				return (this.g_mgr.g_uiM as muUIClient).getObject("pvippower") as LGIUIPvipPower;
			}
		}

		public bool hasGetAwd
		{
			get
			{
				return this._hasGetAwd;
			}
		}

		public int expireTime
		{
			get
			{
				bool flag = this._vipData["vip_data"] == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					bool flag2 = this._vipData["vip_data"]["expire"] == null;
					if (flag2)
					{
						result = 0;
					}
					else
					{
						bool flag3 = this._vipData["vip"]._int == 0;
						if (flag3)
						{
							result = 0;
						}
						else
						{
							int num = (int)this.muNClt.CurServerTimeStampMS / 1000;
							int num2 = this._vipData["vip_data"]["expire"]._int - num;
							bool flag4 = num2 < 0;
							if (flag4)
							{
								result = 0;
							}
							else
							{
								result = num2;
							}
						}
					}
				}
				return result;
			}
		}

		public int expireDay
		{
			get
			{
				return this.expireTime / 86400;
			}
		}

		public int lvlcnt
		{
			get
			{
				bool flag = this._vipData.ContainsKey("lvlcnt") && this._vipData["lvlcnt"] != null;
				int result;
				if (flag)
				{
					result = this._vipData["lvlcnt"]["lvlcnt"]._int;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		public lgGDVIP(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDVIP(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListener(46u, new Action<GameEvent>(this.set_vipData));
		}

		public bool HasAwd()
		{
			return this._hasAwd;
		}

		public void set_vipData(GameEvent e)
		{
		}

		protected void update_lvlcnt(int ltpid, int lvlcnt)
		{
			bool flag = !this._vipData.ContainsKey("vip_data") || this._vipData["vip_data"] == null;
			if (flag)
			{
				this._vipData["vip_data"] = new Variant();
			}
			bool flag2 = this._vipData["vip_data"]["lvlcnt"] == null;
			if (flag2)
			{
				this._vipData["vip_data"]["lvlcnt"] = GameTools.createGroup(new Variant[]
				{
					"ltpid",
					ltpid,
					"cnt",
					lvlcnt
				});
			}
			else
			{
				foreach (Variant current in this._vipData["vip_data"]["lvlcnt"]._arr)
				{
					bool flag3 = current["ltpid"]._int == ltpid;
					if (flag3)
					{
						current["cnt"] = lvlcnt;
						return;
					}
				}
				this._vipData["vip_data"]["lvlcnt"]._arr.Add(GameTools.createGroup(new Variant[]
				{
					"",
					ltpid,
					"cnt",
					lvlcnt
				}));
			}
		}

		private void sendToGetVip()
		{
			this._resetVipData = true;
			this.igVIPMsg.get_vip(0u, 0u);
		}

		public void getVipData(uint awdtype = 0u, int lvl = 0, Action<Variant> fun = null)
		{
			bool flag = awdtype == 0u;
			if (flag)
			{
				bool flag2 = this._vipData == null;
				if (flag2)
				{
					bool flag3 = fun != null;
					if (flag3)
					{
						this._vipresFunList.Add(fun);
					}
					this.igVIPMsg.get_vip(awdtype, (uint)lvl);
				}
				else
				{
					bool flag4 = fun != null;
					if (flag4)
					{
						fun(this._vipData);
					}
				}
			}
			else
			{
				this.igVIPMsg.get_vip(awdtype, (uint)lvl);
			}
		}

		public bool isVip()
		{
			return this._vipLevel > 0u;
		}

		public void setVip(uint viplvl)
		{
			this._vipLevel = viplvl;
		}

		public int GetVIPlvl()
		{
			return (int)this._vipLevel;
		}

		public uint getLtpcntByID(uint ltpid)
		{
			bool flag = this._vipData != null && this._vipData["vip_data"] != null;
			uint result;
			if (flag)
			{
				bool flag2 = this._vipData["vip_data"]["lvlcnt"] != null;
				if (flag2)
				{
					for (int i = 0; i < this._vipData["vip_data"]["lvlcnt"].Count; i++)
					{
						bool flag3 = (long)this._vipData["vip_data"]["lvlcnt"][i]["ltpid"]._int == (long)((ulong)ltpid);
						if (flag3)
						{
							result = this._vipData["vip_data"]["lvlcnt"][i]["cnt"]._uint;
							return result;
						}
					}
				}
			}
			result = 0u;
			return result;
		}

		public uint getRespawn()
		{
			bool flag = this._vipData["vip_data"] != null;
			uint result;
			if (flag)
			{
				bool flag2 = this._vipData["vip_data"]["respawn"] != null;
				if (flag2)
				{
					result = this._vipData["vip_data"]["respawn"]._uint;
					return result;
				}
			}
			result = 0u;
			return result;
		}

		public Variant get_pvip_data()
		{
			bool flag = this.is_pvip();
			Variant result;
			if (flag)
			{
				result = this._vipData["pvip_data"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int GetFirstCanGet()
		{
			Variant pvip_growawd = this.muCCfg.svrGeneralConf.get_pvip_growawd();
			int num = 0;
			int result;
			foreach (Variant current in pvip_growawd._arr)
			{
				bool flag = !this.is_pvip_forGrowGiftGet(current["lmisid"]._uint);
				if (flag)
				{
					result = num;
					return result;
				}
				num++;
			}
			result = 0;
			return result;
		}

		public int GetGrowLength()
		{
			bool flag = this._vipData.ContainsKey("pvip_data");
			int result;
			if (flag)
			{
				Variant variant = this._vipData["pvip_data"]["lmawd_ids"];
				bool flag2 = variant != null;
				if (flag2)
				{
					result = variant.Length;
					return result;
				}
			}
			result = 0;
			return result;
		}

		public bool is_pvip()
		{
			return this.get_pvipLvl() > 0u || this.is_pvip_forYear();
		}

		public uint get_pvipLvl()
		{
			return (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["pvip"]._uint;
		}

		public bool is_pvip_forYear()
		{
			return (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["ypvip"]._bool;
		}

		public bool is_pvip_forNewGiftGet()
		{
			return this._vipData.ContainsKey("pvip_data") && this._vipData["pvip_data"]["newfetch"]._int > 0;
		}

		public bool is_pvip_forYearGiftGet()
		{
			return this._vipData.ContainsKey("pvip_data") && this._vipData["pvip_data"]["ydfetch"]._int > 0;
		}

		public bool is_pvip_forDayGiftGet()
		{
			return this._vipData.ContainsKey("pvip_data") && this._vipData["pvip_data"]["dfetch"] > 0;
		}

		public bool is_pvip_forGrowGiftCanGet()
		{
			Variant pvip_growawd = this.muCCfg.svrGeneralConf.get_pvip_growawd();
			bool result;
			foreach (Variant current in pvip_growawd._arr)
			{
				bool flag = !this.is_pvip_forGrowGiftGet(current["lmisid"]._uint);
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public bool is_pvip_forGrowGiftGet(uint lmawd_id)
		{
			return this._is_pvip_forGrowGiftGet(lmawd_id) == 0;
		}

		public int _is_pvip_forGrowGiftGet(uint lmawd_id)
		{
			bool flag = this._vipData.ContainsKey("pvip_data");
			int result;
			if (flag)
			{
				Variant variant = this._vipData["pvip_data"]["lmawd_ids"];
				Variant pvip_growawd = this.muCCfg.svrGeneralConf.get_pvip_growawd();
				bool flag2 = pvip_growawd;
				if (flag2)
				{
					Variant variant2 = pvip_growawd[lmawd_id]["attchk"];
				}
				Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
				bool flag3 = variant != null;
				if (flag3)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag4 = current._uint == lmawd_id;
						if (flag4)
						{
							result = 1;
							return result;
						}
					}
				}
			}
			result = 0;
			return result;
		}

		public void PvipPowerAddRes(Variant data)
		{
			this.pvipPower.RefreshInfo();
		}

		public void PvipPowerGetAwdRes(Variant data)
		{
			bool flag = data["res"];
			if (flag)
			{
				Variant powerConf = this.muCCfg.svrGeneralConf.GetPowerConf(data["id"]);
				bool flag2 = powerConf;
				if (flag2)
				{
					int arg_63_0 = powerConf.ContainsKey("pt_cost") ? (-powerConf["pt_cost"]) : 0;
					this.pvipPower.RefreshInfo();
				}
			}
		}

		public void PvipPowerTmChangeRes(Variant data)
		{
			this.pvipPower.UpdateTime();
		}

		public void get_pvip_prizeForDay()
		{
			this.igVIPMsg.send_get_pvip(2);
		}

		public void get_pvip_prizeForYear()
		{
			this.igVIPMsg.send_get_pvip(3);
		}

		public void get_pvip_prizeForNew()
		{
			this.igVIPMsg.send_get_pvip(4);
		}

		public void get_pvip_prizeForGrow(uint lmawd_id)
		{
			this.igVIPMsg.GetPvipPrizeForGrow(lmawd_id);
		}

		public void send_get_vip(int awdtp = 0)
		{
			this.igVIPMsg.SendGetVip(awdtp);
		}

		public void GetPvipPowerAwd(uint awdid)
		{
			this.igPlyFunMsg.GetPvipPowerAwd(awdid);
		}
	}
}
