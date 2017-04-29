using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class lgGDCloseServer : lgGDBase
	{
		protected LGIUIMainUI _ogMain;

		public lgGDCloseServer(gameManager m) : base(m)
		{
			this._ogMain = ((this.g_mgr.g_uiM as muUIClient).getLGUI("LGUIMainUIImpl") as LGIUIMainUI);
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDCloseServer(m as gameManager);
		}

		public override void init()
		{
		}

		public void onCloseServer(Variant msgData)
		{
			bool flag = Convert.ToBoolean(this._ogMain);
			if (flag)
			{
				this._ogMain.onChatMsg(msgData);
			}
		}
	}
}
