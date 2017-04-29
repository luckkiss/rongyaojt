using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class lgGDLottery : lgGDBase
	{
		protected Variant logs = new Variant();

		protected Variant turnLogs = new Variant();

		protected bool _selfHadLog = false;

		protected bool _isFirstLog = true;

		private LGIUIMainUI _lguiMain;

		private LGIUILuckyDraw _lguiLuckyDraw;

		public bool IsFirstLog
		{
			get
			{
				return this._isFirstLog;
			}
		}

		protected LGIUIMainUI _lgMainUi
		{
			get
			{
				bool flag = this._lguiMain == null;
				if (flag)
				{
					this._lguiMain = ((this.g_mgr.g_uiM as muUIClient).getLGUI("LGUIMainUIImpl") as LGIUIMainUI);
				}
				return this._lguiMain;
			}
		}

		protected LGIUILuckyDraw _lgui_luckydraw
		{
			get
			{
				bool flag = this._lguiLuckyDraw == null;
				if (flag)
				{
					this._lguiLuckyDraw = ((this.g_mgr.g_uiM as muUIClient).getLGUI("LGUILuckyDraw") as LGIUILuckyDraw);
				}
				return this._lguiLuckyDraw;
			}
		}

		public lgGDLottery(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDLottery(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListener(162u, new Action<GameEvent>(this.switchFunc));
		}

		private void switchFunc(GameEvent e)
		{
			Variant variant = e.data;
			bool flag = variant.ContainsKey("case");
			if (flag)
			{
				string str = variant["case"]._str;
				if (!(str == "setSelfLogData"))
				{
					GameTools.PrintNotice("switchFunc defanult");
				}
				else
				{
					variant = variant["data"];
					this.setSelfLogData(variant);
				}
			}
			else
			{
				GameTools.PrintNotice("switchFunc no case");
			}
		}

		public void setSelfLogData(Variant msgData)
		{
			bool isFirstLog = this._isFirstLog;
			if (isFirstLog)
			{
				this._isFirstLog = false;
			}
			this._selfHadLog = true;
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			bool flag = msgData["logs"].Length > 0;
			if (flag)
			{
				variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.get_lottery(msgData["logs"][0]["lolvl"]);
			}
			bool flag2 = variant2 != null;
			if (flag2)
			{
				bool flag3 = variant2.ContainsKey("usetp");
				if (flag3)
				{
					int num = variant2["usetp"];
				}
				variant = variant2["itm"];
			}
			Variant variant3 = msgData["logs"];
			for (int i = 0; i < variant3.Length; i++)
			{
				Variant variant4 = variant3[i];
				this.logs._arr.Add(variant4);
				bool flag4 = this.logs.Length > 20;
				if (flag4)
				{
					this.logs._arr.RemoveAt(0);
				}
				foreach (Variant current in variant._arr)
				{
					bool flag5 = current["id"] == variant4["tpid"];
					if (flag5)
					{
						bool flag6 = !variant4.ContainsKey("exatt") && current.ContainsKey("make_att");
						if (flag6)
						{
							variant4["make_att"] = current["make_att"];
						}
						bool flag7 = current.ContainsKey("broadcast");
						if (flag7)
						{
							this.turnLogs._arr.Insert(0, variant4);
						}
						break;
					}
				}
				bool flag8 = this.turnLogs.Length > 10;
				if (flag8)
				{
					this.turnLogs._arr.RemoveAt(this.turnLogs.Length - 1);
				}
			}
			Variant variant5 = new Variant();
			variant5["logs"] = this.logs;
			this._lgui_luckydraw.refreshLog(variant5);
			LGIUILottery lGIUILottery = (this.g_mgr.g_uiM as muUIClient).getLGUI("mdlg_lottery") as LGIUILottery;
			bool flag9 = lGIUILottery == null;
			if (!flag9)
			{
				Variant variant6 = new Variant();
				variant6["logs"] = this.logs;
				lGIUILottery.refreshLog(variant6);
			}
		}

		public void setOtherLogData(Variant msgData)
		{
			Variant variant = new Variant();
			Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.get_lottery(10);
			bool flag = variant2;
			if (flag)
			{
				variant = variant2["itm"];
			}
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			Variant variant3 = msgData["logs"];
			for (int i = 0; i < variant3.Length; i++)
			{
				Variant variant4 = variant3[i];
				bool flag2 = mainPlayerInfo["name"] == variant4["name"] && this._selfHadLog;
				if (!flag2)
				{
					this.logs._arr.Add(variant4);
					bool flag3 = this.logs.Length > 20;
					if (flag3)
					{
						this.logs._arr.RemoveAt(0);
					}
					foreach (Variant current in variant._arr)
					{
						bool flag4 = current["tpid"] == variant4["tpid"] && current.ContainsKey("broadcast");
						if (flag4)
						{
							this.turnLogs._arr.Add(variant4);
							break;
						}
					}
					bool flag5 = this.turnLogs.Length > 10;
					if (flag5)
					{
						this.turnLogs._arr.RemoveAt(0);
					}
				}
			}
			Variant variant5 = new Variant();
			variant5["curid"] = msgData["curid"];
			variant5["logs"] = this.logs;
			this._lgui_luckydraw.refreshLog(variant5);
			LGIUILottery lGIUILottery = (this.g_mgr.g_uiM as muUIClient).getLGUI("lottery") as LGIUILottery;
			bool flag6 = lGIUILottery == null;
			if (!flag6)
			{
				Variant variant6 = new Variant();
				variant6["curid"] = msgData["curid"];
				variant6["logs"] = this.logs;
				lGIUILottery.refreshLog(variant6);
			}
		}

		public void lottery(int level, int cnt)
		{
			int usetp = 0;
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.get_lottery(level);
			Variant variant2 = variant["lot_cnt"];
			bool flag = variant2[0].ContainsKey("itm2yb");
			if (flag)
			{
				lgGDItems g_itemsCT = (this.g_mgr as muLGClient).g_itemsCT;
				uint tpid = variant2[0]["itm2yb"];
				uint num = g_itemsCT.pkg_get_item_count_bytpid(tpid);
				bool flag2 = num > 0u;
				if (flag2)
				{
					usetp = 1;
				}
			}
			else
			{
				usetp = 0;
				bool flag3 = !this.is_need_yb(level, cnt);
				if (flag3)
				{
					return;
				}
			}
			(this.g_mgr.g_netM as muNetCleint).igLotteryMsgs.lottery((uint)level, cnt, usetp);
		}

		private bool is_need_yb(int tp, int cnt)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.get_lottery(tp);
			Variant variant2 = variant["lot_cnt"];
			Variant variant3 = null;
			foreach (string current in variant2.Keys)
			{
				variant3 = variant2[current];
				bool flag = variant3["cnt"] == cnt;
				if (flag)
				{
					break;
				}
			}
			bool flag2 = variant3 == null;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				lgGDGeneral g_generalCT = (this.g_mgr as muLGClient).g_generalCT;
				bool flag3 = variant3["ybcost"] > g_generalCT.yb;
				if (flag3)
				{
					string languageText = LanguagePack.getLanguageText("UI_Class_npc_carry", "t_moneyNotEnough");
					this._lgMainUi.systemmsg(languageText, 1024u);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		public void RefreshLotCnt(Variant data)
		{
			bool flag = data.ContainsKey("usetp");
			if (flag)
			{
				this._lgui_luckydraw.RefreshLotCnt(data);
			}
		}
	}
}
