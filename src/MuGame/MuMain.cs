using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class MuMain : gameMain
	{
		protected override void onInit(Variant parma)
		{
			this.g_gameM.dispatchEventCL("LG_OUT_GAME", GameEvent.Create(3060u, this, null, false));
			connInfo connInfo = this.g_netM.getObject("DATA_CONN") as connInfo;
			connInfo.setInfo(parma);
		}

		protected override IClientBase createGameManager()
		{
			return new muLGClient(this);
		}

		protected override IClientBase createNetManager()
		{
			return new muNetCleint(this);
		}

		protected override IClientBase createSceneManager()
		{
			return new muGRClient(this);
		}

		protected override IClientBase createUiManager()
		{
			return new muUIClient(this);
		}

		protected override IClientBase createGameConfingManager()
		{
			return new muCLientConfig(this);
		}
	}
}
