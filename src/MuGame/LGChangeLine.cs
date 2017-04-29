using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class LGChangeLine : lgGDBase
	{
		public LGChangeLine(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGChangeLine(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListenerCL("MSG_CHANGELINE", 7u, new Action<GameEvent>(this.onChangeLine));
		}

		private void onChangeLine(GameEvent e)
		{
			Variant data = e.data;
		}
	}
}
