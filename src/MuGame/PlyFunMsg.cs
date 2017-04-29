using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class PlyFunMsg : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 255u;
			}
		}

		public static PlyFunMsg create()
		{
			return new PlyFunMsg();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(255u, this, this.msgData, false));
			switch (this.msgData["tp"]._int)
			{
			case 5:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5030u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.OnResetlvl(this.msgData);
				break;
			case 9:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5026u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.PasswordSafeDataRes(this.msgData);
				break;
			case 10:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5027u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.OnUpgradeNobRes(this.msgData);
				break;
			case 11:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5028u, this, this.msgData["merge_ids"], false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.setMergeInfo(this.msgData["merge_ids"]);
				break;
			case 12:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5029u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.RollptBack(this.msgData);
				break;
			case 13:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5034u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.OnInvestBack(this.msgData);
				break;
			case 15:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5031u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.UpgradeRideRes(this.msgData);
				break;
			case 16:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5032u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.RideChange(this.msgData);
				break;
			case 17:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5033u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.UpdatePlayerName(this.msgData);
				break;
			case 18:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5035u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).lgGD_Award.GetOlAwds(this.msgData);
				break;
			case 19:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5036u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.PkgsItemBack(this.msgData);
				break;
			case 20:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5037u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.RideQualAttActiveRes(this.msgData);
				break;
			case 21:
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5038u, this, this.msgData, false));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_plyfunCT.SelectRideSkillRes(this.msgData);
				break;
			}
		}
	}
}
