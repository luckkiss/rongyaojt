using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class lgGDGeneral : lgGDBase
	{
		protected int _skillExp;

		protected bool _prepo = false;

		protected Variant _clientConf = null;

		protected Variant netData;

		protected string _msgStr = null;

		private LGIUIMainUI _lgiuiMainUI;

		private LGIUIShop _lgiuiShop;

		private LGIUIItems _lgiuiItems;

		private LGIUILottery _lgiuiLottery;

		private LGIUISkill _lguiSkill = null;

		public Variant _netData
		{
			get
			{
				bool flag = this.netData == null;
				if (flag)
				{
					this.netData = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.m_data;
				}
				return this.netData;
			}
		}

		public int yb
		{
			get
			{
				return this._netData["yb"];
			}
			set
			{
				this._netData["yb"] = value;
				bool flag = this.ui_items != null;
				if (flag)
				{
					this.ui_items.pkg_set_yb(this._netData["yb"]);
				}
				bool flag2 = this.ui_shop != null;
				if (flag2)
				{
					this.ui_shop.setCurYb(this._netData["yb"]);
				}
				bool flag3 = this.ui_lottery != null;
				if (flag3)
				{
					this.ui_lottery.setCurYb(this._netData["yb"]);
				}
			}
		}

		public int gold
		{
			get
			{
				return this._netData["gold"];
			}
			set
			{
				this._netData["gold"] = value;
				this.ui_items.pkg_set_gold(this._netData["gold"]);
			}
		}

		public int ybpt
		{
			get
			{
				return this._netData["ybpt"];
			}
			set
			{
				this._netData["ybpt"] = value;
			}
		}

		public int ybcharge
		{
			get
			{
				return this._netData["ybcharge"];
			}
			set
			{
				this._netData["ybcharge"] = value;
			}
		}

		public int bndyb
		{
			get
			{
				return this._netData.ContainsKey("bndyb") ? this._netData["bndyb"]._int : 0;
			}
			set
			{
				this._netData["bndyb"] = value;
				this.ui_shop.setCurBndyb(this._netData["bndyb"]);
				this.lgMainui.setCurBndyb(this._netData["bndyb"]);
				this.ui_items.pkg_set_bdyb(this._netData["bndyb"]);
			}
		}

		public int hexp
		{
			get
			{
				return this._netData["hexp"];
			}
			set
			{
				this._netData["hexp"] = value;
			}
		}

		public int skillExp
		{
			get
			{
				return this._skillExp;
			}
			set
			{
				this._skillExp = value;
			}
		}

		public bool prepo
		{
			get
			{
				return this._prepo;
			}
			set
			{
				this._prepo = value;
			}
		}

		public int noblv
		{
			get
			{
				return this._netData["noblv"];
			}
			set
			{
				this._netData["noblv"] = value;
			}
		}

		public int nobpt
		{
			get
			{
				return this._netData["nobpt"];
			}
			set
			{
				int num = value - this._netData["nobpt"]._int;
				bool flag = num > 0;
				if (flag)
				{
					this.lgMainui.AddAttShow("nobpt", num);
				}
				this._netData["nobpt"] = value;
			}
		}

		public int batpt
		{
			get
			{
				return this._netData["batpt"];
			}
			set
			{
				this._netData["batpt"] = value;
			}
		}

		public int clang
		{
			get
			{
				return this._netData["clang"];
			}
			set
			{
				this._netData["clang"] = value;
			}
		}

		public int clana
		{
			get
			{
				return this._netData["clana"];
			}
			set
			{
				this._netData["clana"] = value;
			}
		}

		public int cur_clanagld
		{
			get
			{
				return this._netData["cur_clanagld"];
			}
			set
			{
				this._netData["cur_clanagld"] = value;
			}
		}

		public int cur_clanayb
		{
			get
			{
				return this._netData["cur_clanayb"];
			}
			set
			{
				this._netData["cur_clanayb"] = value;
			}
		}

		public int clan_pt
		{
			get
			{
				return this._netData["clan_pt"];
			}
			set
			{
				this._netData["clan_pt"] = value;
			}
		}

		private LGIUIMainUI lgMainui
		{
			get
			{
				bool flag = this._lgiuiMainUI == null;
				if (flag)
				{
					this._lgiuiMainUI = (this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI);
				}
				return this._lgiuiMainUI;
			}
		}

		private LGIUIShop ui_shop
		{
			get
			{
				bool flag = this._lgiuiShop == null;
				if (flag)
				{
					this._lgiuiShop = (this.g_mgr.g_uiM.getLGUI("mdlg_shop") as LGIUIShop);
				}
				return this._lgiuiShop;
			}
		}

		private LGIUIItems ui_items
		{
			get
			{
				bool flag = this._lgiuiItems == null;
				if (flag)
				{
					this._lgiuiItems = (this.g_mgr.g_uiM.getLGUI("LGUIItemImpl") as LGIUIItems);
				}
				return this._lgiuiItems;
			}
		}

		private LGIUILottery ui_lottery
		{
			get
			{
				bool flag = this._lgiuiLottery == null;
				if (flag)
				{
					this._lgiuiLottery = (this.g_mgr.g_uiM.getLGUI("mdlg_lottery") as LGIUILottery);
				}
				return this._lgiuiLottery;
			}
		}

		public int meript
		{
			get
			{
				return this._netData["meript"];
			}
			set
			{
				int num = value - this._netData["meript"];
				bool flag = num > 0 && this._netData.ContainsKey("meript");
				if (flag)
				{
					this.lgMainui.AddAttShow("meript", num);
				}
				this._netData["meript"] = value;
			}
		}

		public int soulpt
		{
			get
			{
				return this._netData["soulpt"];
			}
			set
			{
				this._netData["soulpt"] = value;
				LGIUIVeapon lGIUIVeapon = this.g_mgr.g_uiM.getLGUI("mdlg_veapon") as LGIUIVeapon;
				bool flag = lGIUIVeapon != null;
				if (flag)
				{
					lGIUIVeapon.SetSoulpt();
				}
			}
		}

		public int shoppt
		{
			get
			{
				return this._netData["shoppt"];
			}
			set
			{
				this._netData["shoppt"] = value;
				LGIUIPointShop lGIUIPointShop = this.g_mgr.g_uiM.getLGUI("mdlg_veapon") as LGIUIPointShop;
				bool flag = lGIUIPointShop != null;
				if (flag)
				{
					lGIUIPointShop.ptChange(this._netData["shoppt"]);
				}
			}
		}

		public int lotexpt
		{
			get
			{
				return this._netData["lotexpt"];
			}
			set
			{
				this._netData["lotexpt"] = value;
				LGIUILotteryShop lGIUILotteryShop = this.g_mgr.g_uiM.getLGUI("UI_LOTTERY_SHOP") as LGIUILotteryShop;
				bool flag = lGIUILotteryShop != null;
				if (flag)
				{
					lGIUILotteryShop.ptChange(this._netData["lotexpt"]);
				}
				LGIUILottery lGIUILottery = this.g_mgr.g_uiM.getLGUI("UI_LOTTERY") as LGIUILottery;
			}
		}

		public int lottery_c_cnt
		{
			get
			{
				return 0;
			}
		}

		public int tcyb_lott
		{
			get
			{
				return this._netData["tcyb_lott"];
			}
			set
			{
				this._netData["tcyb_lott"] = value;
			}
		}

		public int lottery_u_cnt_new
		{
			get
			{
				return 0;
			}
		}

		public int tcyb_lott_cost
		{
			get
			{
				return this._netData["tcyb_lott_cost"];
			}
			set
			{
				this._netData["tcyb_lott_cost"] = value;
			}
		}

		private LGIUISkill lguiSkill
		{
			get
			{
				bool flag = this._lguiSkill == null;
				if (flag)
				{
				}
				return this._lguiSkill;
			}
		}

		public lgGDGeneral(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDGeneral(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListener(37u, new Action<GameEvent>(this.modpkgspcres));
			this.g_mgr.g_netM.addEventListener(62u, new Action<GameEvent>(this.buyitemres));
			this.g_mgr.g_netM.addEventListener(63u, new Action<GameEvent>(this.sellitemres));
			this.g_mgr.g_netM.addEventListener(64u, new Action<GameEvent>(this.buysolditemres));
			this.g_mgr.g_netM.addEventListener(65u, new Action<GameEvent>(this.onGetItemsRes));
			this.g_mgr.g_netM.addEventListener(75u, new Action<GameEvent>(this.itemsChange));
		}

		private void onPkStateChange(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("pk_state");
			if (flag)
			{
				this.PKStateChange(data["pk_state"]._int);
			}
			else
			{
				this.lgMainui.show_defense(data);
			}
		}

		private void onClientConfig(GameEvent e)
		{
			Variant data = e.data;
			this.SetClientConf(data);
		}

		private void modpkgspcres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["yb_cost"]._int > 0;
			if (flag)
			{
				this.sub_yb(data["yb_cost"], true);
			}
		}

		private void buyitemres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				bool flag2 = data.ContainsKey("yb");
				if (flag2)
				{
					this.sub_yb(data["yb"], true);
				}
				bool flag3 = data.ContainsKey("gld");
				if (flag3)
				{
					this.sub_gold(data["gld"]);
				}
				bool flag4 = data.ContainsKey("bndyb");
				if (flag4)
				{
					this.sub_bndyb(data["bndyb"]);
				}
				bool flag5 = data.ContainsKey("hexp");
				if (flag5)
				{
					this.mode_hexp(-data["hexp"]);
				}
				bool flag6 = data.ContainsKey("ybpt");
				if (flag6)
				{
					this.sub_ybpt(data["ybpt"]);
				}
				bool flag7 = data.ContainsKey("clang") && data.ContainsKey("itms");
				if (flag7)
				{
					this.sub_clang(data["clang"]);
				}
				bool flag8 = data.ContainsKey("shoppt");
				if (flag8)
				{
					this.mode_shoppt(-data["shoppt"]);
				}
				bool flag9 = data.ContainsKey("lotexpt");
				if (flag9)
				{
					this.mode_lotexpt(-data["lotexpt"]);
				}
			}
		}

		private void sellitemres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				this.add_gold(data["earn"]._uint);
			}
		}

		private void buysolditemres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				this.sub_gold(data["cost"]);
			}
		}

		private void onGetItemsRes(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("prepo");
			if (flag)
			{
				this.prepo = data["prepo"];
			}
		}

		private void itemsChange(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("gold");
			if (flag)
			{
				bool flag2 = data["gold"] > 0;
				if (flag2)
				{
					this.add_gold(data["gold"]);
				}
				else
				{
					this.sub_gold((uint)Math.Abs(data["gold"]._int));
				}
			}
			bool flag3 = data.ContainsKey("yb");
			if (flag3)
			{
				bool flag4 = data["yb"]._int > 0;
				if (flag4)
				{
					this.add_yb(data["yb"]);
				}
				else
				{
					this.sub_yb(-data["yb"]._int, true);
				}
			}
			bool flag5 = data.ContainsKey("ybpt");
			if (flag5)
			{
				bool flag6 = data["ybpt"] > 0;
				if (flag6)
				{
					this.add_ybpt(data["ybpt"]);
				}
				else
				{
					this.sub_ybpt((uint)Math.Abs(data["ybpt"]._int));
				}
			}
			bool flag7 = data.ContainsKey("ybcharge");
			if (flag7)
			{
				this.ybcharge = data["ybcharge"];
			}
			bool flag8 = data.ContainsKey("bndyb");
			if (flag8)
			{
				bool flag9 = data["bndyb"] > 0;
				if (flag9)
				{
					this.add_bndyb(data["bndyb"]);
				}
				else
				{
					this.sub_bndyb((uint)Math.Abs(data["bndyb"]._int));
				}
			}
			bool flag10 = data.ContainsKey("clang");
			if (flag10)
			{
				this.mode_clang(data["clang"]);
			}
		}

		public void add_yb(int value)
		{
			this.yb = this._netData["yb"] + value;
			this._msgStr = LanguagePack.getLanguageText("LGUIItemImpl", "getDiamond");
			this._msgStr = DebugTrace.Printf(this._msgStr, new string[]
			{
				value.ToString()
			});
			this.lgMainui.systemmsg(this._msgStr, 12u);
			bool flag = this.lguiSkill != null;
			if (flag)
			{
				this.lguiSkill.ChangeGold();
			}
		}

		public void sub_yb(int value, bool isXf = true)
		{
			this.yb = this._netData["yb"]._int - value;
			this._msgStr = LanguagePack.getLanguageText("LGUIItemImpl", "loseDiamond");
			this._msgStr = DebugTrace.Printf(this._msgStr, new string[]
			{
				value.ToString()
			});
			this.lgMainui.systemmsg(this._msgStr, 4u);
			bool flag = this.lguiSkill != null;
			if (flag)
			{
				this.lguiSkill.ChangeGold();
			}
			if (isXf)
			{
			}
		}

		public void add_gold(uint value)
		{
			this.gold = this._netData["gold"]._int + (int)value;
			this._msgStr = LanguagePack.getLanguageText("LGUIItemImpl", "getGold");
			this._msgStr = DebugTrace.Printf(this._msgStr, new string[]
			{
				value.ToString()
			});
			this.lgMainui.systemmsg(this._msgStr, 12u);
			this.lgMainui.AddAttShow("gold", (int)value);
			bool flag = this.lguiSkill != null;
			if (flag)
			{
				this.lguiSkill.ChangeGold();
			}
		}

		public void sub_gold(uint value)
		{
			this.gold = this._netData["gold"]._int - (int)value;
			this._msgStr = LanguagePack.getLanguageText("LGUIItemImpl", "loseGold");
			this._msgStr = DebugTrace.Printf(this._msgStr, new string[]
			{
				value.ToString()
			});
			this.lgMainui.systemmsg(this._msgStr, 4u);
			bool flag = this.lguiSkill != null;
			if (flag)
			{
				this.lguiSkill.ChangeGold();
			}
		}

		public void add_ybpt(uint value)
		{
			this.ybpt = this._netData["ybpt"]._int + (int)value;
		}

		public void sub_ybpt(uint value)
		{
			this.ybpt = this._netData["ybpt"]._int - (int)value;
		}

		public void add_ybcharge(uint value)
		{
			this.ybcharge = this._netData["ybcharge"]._int + (int)value;
		}

		public void sub_ybcharge(uint value)
		{
			this.ybcharge = this._netData["ybcharge"]._int - (int)value;
		}

		public void add_bndyb(uint value)
		{
			this.bndyb = this._netData["bndyb"]._int + (int)value;
			this._msgStr = LanguagePack.getLanguageText("LGUIItemImpl", "getGift");
			this._msgStr = DebugTrace.Printf(this._msgStr, new string[]
			{
				value.ToString()
			});
			this.lgMainui.systemmsg(this._msgStr, 12u);
		}

		public void sub_bndyb(uint value)
		{
			this.bndyb = this._netData["bndyb"]._int - (int)value;
			this._msgStr = LanguagePack.getLanguageText("LGUIItemImpl", "loseGift");
			this._msgStr = DebugTrace.Printf(this._msgStr, new string[]
			{
				value.ToString()
			});
			this.lgMainui.systemmsg(this._msgStr, 4u);
		}

		public void mode_hexp(int value)
		{
		}

		public void addSkillExp(int v)
		{
			this._skillExp += v;
		}

		public void mode_nobpt(int value)
		{
		}

		public void mode_batpt(int value)
		{
			Variant variant = this._netData;
			variant["batpt"] = variant["batpt"] + value;
		}

		public void mode_clang(int value)
		{
			Variant variant = this._netData;
			variant["clang"] = variant["clang"] + value;
		}

		public void sub_clang(int value)
		{
			Variant variant = this._netData;
			variant["clang"] = variant["clang"] - value;
		}

		public void mode_clana(int value)
		{
			Variant variant = this._netData;
			variant["clana"] = variant["clana"] + value;
		}

		public void sub_clana(int value)
		{
			Variant variant = this._netData;
			variant["clana"] = variant["clana"] - value;
		}

		public void add_cur_clanagld(int value)
		{
			bool flag = this._netData["cur_clanagld"] == null;
			if (flag)
			{
				this._netData["cur_clanagld"] = 0;
			}
			Variant variant = this._netData;
			variant["cur_clanagld"] = variant["cur_clanagld"] + value;
		}

		public void sub_cur_clanagld(int value)
		{
			bool flag = this._netData["cur_clanagld"] == null;
			if (flag)
			{
				this._netData["cur_clanagld"] = 0;
			}
			Variant variant = this._netData;
			variant["cur_clanagld"] = variant["cur_clanagld"] - value;
		}

		public void add_cur_clanayb(int value)
		{
			Variant variant = this._netData;
			variant["cur_clanayb"] = variant["cur_clanayb"] + value;
		}

		public void sub_cur_clanayb(int value)
		{
			Variant variant = this._netData;
			variant["cur_clanayb"] = variant["cur_clanayb"] - value;
		}

		public void mode_clanpt(int value)
		{
			bool flag = this._netData["clan_pt"] >= 0;
			if (flag)
			{
				Variant variant = this._netData;
				variant["clan_pt"] = variant["clan_pt"] + value;
			}
			else
			{
				this._netData["clan_pt"] = value;
			}
			bool flag2 = value > 0;
			if (flag2)
			{
				this._msgStr = LanguagePack.getLanguageText("LGUIItemImpl", "getClanPt");
			}
			else
			{
				this._msgStr = LanguagePack.getLanguageText("LGUIItemImpl", "loseClanPt");
				value = -value;
			}
			this._msgStr = DebugTrace.Printf(this._msgStr, new string[]
			{
				value.ToString()
			});
			this.lgMainui.systemmsg(this._msgStr, 12u);
		}

		public void SetClientConf(Variant data)
		{
			this._clientConf = data;
			this.lgMainui.setClientConfig(this._clientConf);
		}

		public Variant getClientConf()
		{
			bool flag = this._clientConf == null;
			if (flag)
			{
				BaseProxy<GeneralProxy>.getInstance().sendGetClientConfig();
			}
			return this._clientConf;
		}

		public void SendPKState(int state)
		{
			Variant data = GameTools.createGroup(new Variant[]
			{
				"tp",
				1,
				"pk_state",
				state
			});
			(this.g_mgr.g_netM as muNetCleint).igGenMsg.SendPKState(data);
		}

		public void PKStateChange(int pk_state)
		{
			this.lgMainui.ChangePKState(pk_state);
		}

		public void CarrlvlChange(int carrlvl)
		{
			bool isup = false;
			bool flag = carrlvl > this._netData["carrlvl"];
			if (flag)
			{
				isup = true;
			}
			this._netData["carrlvl"] = carrlvl;
			bool flag2 = this.lgMainui != null;
			if (flag2)
			{
				this.lgMainui.CarrlvlChange(carrlvl, isup);
			}
		}

		public void change_meript(int value)
		{
			int meript = this.meript;
			int num = value - meript;
			bool flag = num > 0;
			if (flag)
			{
				this._msgStr = LanguagePack.getLanguageText("LGUIItemImpl", "getStarSoul");
			}
			else
			{
				bool flag2 = num < 0;
				if (flag2)
				{
					this._msgStr = LanguagePack.getLanguageText("LGUIItemImpl", "loseStarSoul");
					num = -num;
				}
			}
			this._msgStr = DebugTrace.Printf(this._msgStr, new string[]
			{
				num.ToString()
			});
			this.lgMainui.systemmsg(this._msgStr, 12u);
			this.meript = value;
		}

		public void mode_shoppt(int val)
		{
			this.shoppt += val;
		}

		public void mode_lotexpt(int val)
		{
			this.lotexpt += val;
		}

		public void mode_tcyb_lott(int val)
		{
			this.tcyb_lott += val;
		}

		public void mode_tcyb_lott_cost(int val)
		{
			this.tcyb_lott_cost += val;
		}
	}
}
