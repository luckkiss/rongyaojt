using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class GetRankInfo : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 254u;
			}
		}

		public static GetRankInfo create()
		{
			return new GetRankInfo();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(254u, this, this.msgData, false));
			lgGDRank g_RankCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_RankCT;
			g_RankCT.setRankData(this.msgData);
		}
	}
}
