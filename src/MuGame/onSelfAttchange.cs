using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onSelfAttchange : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 32u;
			}
		}

		public static onSelfAttchange create()
		{
			return new onSelfAttchange();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData.ContainsKey("hexpadd");
			if (flag)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5005u, this, this.msgData["hexpadd"], false));
			}
			bool flag2 = this.msgData.ContainsKey("clangadd");
			if (flag2)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5006u, this, this.msgData["clangadd"], false));
			}
			bool flag3 = this.msgData.ContainsKey("clang");
			if (flag3)
			{
				Variant variant = new Variant();
				variant["clang"] = this.msgData["clang"];
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5007u, this, variant, false));
			}
			bool flag4 = this.msgData.ContainsKey("batptadd");
			if (flag4)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5008u, this, this.msgData["batptadd"], false));
			}
			bool flag5 = this.msgData.ContainsKey("nobptadd");
			if (flag5)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5009u, this, this.msgData["nobptadd"], false));
			}
			bool flag6 = this.msgData.ContainsKey("carrlvl");
			if (flag6)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5010u, this, this.msgData["carrlvl"], false));
			}
			bool flag7 = this.msgData.ContainsKey("prizelvl");
			if (flag7)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5011u, this, this.msgData["prizelvl"], false));
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5020u, this, null, false));
			}
			bool flag8 = this.msgData.ContainsKey("soulptadd");
			if (flag8)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5012u, this, this.msgData["soulptadd"], false));
			}
			bool flag9 = this.msgData.ContainsKey("shopptadd");
			if (flag9)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5013u, this, this.msgData["shopptadd"], false));
			}
			bool flag10 = this.msgData.ContainsKey("lotexptadd");
			if (flag10)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5014u, this, this.msgData["lotexptadd"], false));
			}
			bool flag11 = this.msgData.ContainsKey("tcyb_lott_cost");
			if (flag11)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5015u, this, this.msgData["tcyb_lott_cost"], false));
				Variant variant2 = new Variant();
				variant2["usetp"] = 3;
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5017u, this, variant2, false));
			}
			bool flag12 = this.msgData.ContainsKey("tcyb_lott");
			if (flag12)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5016u, this, this.msgData["tcyb_lott"], false));
				Variant variant3 = new Variant();
				variant3["usetp"] = 2;
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5017u, this, variant3, false));
			}
			bool flag13 = this.msgData.ContainsKey("lvlshare");
			if (flag13)
			{
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_levelsCT.RefreshLvlShare(this.msgData["lvlshare"]);
			}
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(32u, this, this.msgData, false));
			((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_selfPlayer.on_self_attchange(this.msgData);
		}
	}
}
