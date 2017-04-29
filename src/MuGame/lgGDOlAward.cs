using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class lgGDOlAward : lgGDBase
	{
		protected Variant _olAwardInfo;

		private Variant _chaOflTmData;

		public lgGDOlAward(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDOlAward(m as gameManager);
		}

		public override void init()
		{
		}

		public void setOlAwardInfo(Variant msgData)
		{
		}

		public Variant getOlAwardInfo()
		{
			return this._olAwardInfo;
		}

		public Variant GetChaOflTmData()
		{
			return this._chaOflTmData;
		}

		public void GetChaOLTm()
		{
			(this.g_mgr.g_netM as muNetCleint).igOlAwardMsgs.GetChaOLTm();
		}

		public void GetChaOflTmAwd(uint ofl_tm, uint tp)
		{
			(this.g_mgr.g_netM as muNetCleint).igOlAwardMsgs.GetChaOflTmAwd(ofl_tm, tp);
		}

		public void on_get_ol_tm_res(Variant msgData)
		{
			bool flag = msgData != null;
			if (flag)
			{
				bool flag2 = this._chaOflTmData == null;
				if (flag2)
				{
					this._chaOflTmData = msgData;
				}
				else
				{
					foreach (string current in msgData.Keys)
					{
						this._chaOflTmData[current] = msgData[current];
					}
				}
				LGIUIWelfare lGIUIWelfare = (this.g_mgr.g_uiM as muUIClient).getLGUI("welfare") as LGIUIWelfare;
				bool flag3 = lGIUIWelfare.isOpen();
				if (flag3)
				{
					lGIUIWelfare.RefOffLineExpList();
				}
				bool flag4 = !lGIUIWelfare.isOpen();
				if (flag4)
				{
					bool flag5 = this._chaOflTmData["ofla_tm"] >= 60;
					if (flag5)
					{
						lGIUIWelfare.OpenWelfare("OFFLINE_EXP");
					}
				}
			}
		}
	}
}
