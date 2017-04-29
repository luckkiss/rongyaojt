using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class lgGDTrade : lgGDBase
	{
		protected uint _traderCid;

		protected string _traderName;

		protected bool _selfLocked;

		protected bool _targetLocked;

		protected bool _selfTradeDone;

		protected bool _targetTradeDone;

		public uint traderCid
		{
			get
			{
				return this._traderCid;
			}
		}

		public lgGDTrade(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDTrade(m as gameManager);
		}

		public override void init()
		{
		}

		public void setTradeReqData(Variant msgData)
		{
			this._traderCid = msgData["cid"];
			this._traderName = msgData["name"];
			LGIUINotify lGIUINotify = (this.g_mgr.g_uiM as muUIClient).getLGUI("notify") as LGIUINotify;
			lGIUINotify.notifyTradeRequest(msgData, true);
			LGIUIMainUI lGIUIMainUI = (this.g_mgr.g_uiM as muUIClient).getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			lGIUIMainUI.systemmsg(new Variant(this._traderName + LanguagePack.getLanguageText("trade", "request")), LGUIConstant.SYSMSG_TYPE_RB1);
		}

		public void startTrade(Variant msgData)
		{
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			LGIUITrade lGIUITrade = (this.g_mgr.g_uiM as muUIClient).getLGUI("trade") as LGIUITrade;
			bool flag = msgData["cid"] == mainPlayerInfo["cid"];
			if (flag)
			{
				lGIUITrade.showTrade(this._traderCid);
			}
			else
			{
				lGIUITrade.showTrade(msgData["cid"]);
			}
		}

		public void addItem(Variant msgData)
		{
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			LGIUITrade lGIUITrade = (this.g_mgr.g_uiM as muUIClient).getLGUI("trade") as LGIUITrade;
			bool flag = msgData["cid"] == mainPlayerInfo["cid"];
			if (flag)
			{
				lGIUITrade.addSelfItem(msgData);
			}
			else
			{
				lGIUITrade.addTargetItem(msgData);
			}
		}

		public void lockTrade(Variant msgData)
		{
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			LGIUITrade lGIUITrade = (this.g_mgr.g_uiM as muUIClient).getLGUI("trade") as LGIUITrade;
			bool flag = msgData["cid"] == mainPlayerInfo["cid"];
			if (flag)
			{
				this._selfLocked = msgData["locked"];
				lGIUITrade.setLockStatus(this._selfLocked, this._targetLocked);
			}
			else
			{
				this._targetLocked = msgData["locked"];
				lGIUITrade.setLockStatus(this._selfLocked, this._targetLocked);
			}
		}

		public void tradeDone(Variant msgData)
		{
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			LGIUITrade lGIUITrade = (this.g_mgr.g_uiM as muUIClient).getLGUI("trade") as LGIUITrade;
			bool flag = msgData["cid"] == mainPlayerInfo["cid"];
			if (flag)
			{
				this._selfTradeDone = true;
				lGIUITrade.setTradeDone(this._selfTradeDone, this._targetTradeDone);
				bool targetTradeDone = this._targetTradeDone;
				if (targetTradeDone)
				{
					this.initStatus();
				}
			}
			else
			{
				this._targetTradeDone = true;
				lGIUITrade.setTradeDone(this._selfTradeDone, this._targetTradeDone);
				bool selfTradeDone = this._selfTradeDone;
				if (selfTradeDone)
				{
					this.initStatus();
				}
			}
			bool flag2 = this._selfTradeDone && this._targetTradeDone;
			if (flag2)
			{
			}
		}

		public void initStatus()
		{
			this._selfLocked = false;
			this._targetLocked = false;
			this._selfTradeDone = false;
			this._targetTradeDone = false;
		}
	}
}
