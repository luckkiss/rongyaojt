using GameFramework;
using System;

namespace MuGame
{
	public class muUIClient : UIClient
	{
		public LGUIMainUIImpl_NEED_REMOVE lguiMainUIImpl
		{
			get
			{
				return base.getLGUI("LGUIMainUIImpl") as LGUIMainUIImpl_NEED_REMOVE;
			}
		}

		private MediaClient m_media
		{
			get
			{
				return MediaClient.getInstance();
			}
		}

		public muUIClient(gameMain m) : base(m)
		{
		}

		protected override void onInit()
		{
			base.regCreatorLGUI("LGUIMainUIImpl", new Func<IClientBase, IObjectPlugin>(LGUIMainUIImpl_NEED_REMOVE.create));
			base.addEventListener(4020u, new Action<GameEvent>(this.onTryEnterGame));
			(base.g_gameM.getObject("LG_JOIN_WORLD") as LGJoinWorld).addEventListener(3034u, new Action<GameEvent>(this.onEnterGame));
			new UIUtility(this);
			new UILinksManager(this);
			new DisplayUtil(this);
			new GuideManager(this);
		}

		private void backgroundClick()
		{
			base.regBackgroundClick("mdlg_clan");
			base.regBackgroundClick("mdlg_clan_bigDialog");
			base.regBackgroundClick("mdlg_clan_join_list");
			base.regBackgroundClick("mdlg_clan_member_list");
			base.regBackgroundClick("mdlg_clan_skill");
			base.regBackgroundClick("mdlg_clan_smallDialog");
			base.regBackgroundClick("mdlg_clan_view_list");
		}

		public override bool showLoading(BaseLGUI ui)
		{
			return this.m_backgroundClickUIS.ContainsKey(ui.uiName);
		}

		public override void onLoadingUI(BaseLGUI ui)
		{
		}

		public override void onLoadingUIEnd(BaseLGUI ui)
		{
		}

		private void onTryEnterGame(GameEvent e)
		{
			bool flag = login.instance == null;
			if (flag)
			{
				debug.Log("打开loading ！！！ login==null");
			}
			else
			{
				debug.Log("打开loading ！！！ login!=null");
			}
			bool flag2 = !LGPlatInfo.relogined;
			if (flag2)
			{
				debug.Log("打开loading ！！！ LGPlatInfo.relogined==false");
			}
			else
			{
				debug.Log("打开loading ！！！ LGPlatInfo.relogined==true");
			}
			bool flag3 = login.instance == null && !LGPlatInfo.relogined;
			if (flag3)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.MAP_LOADING, null, false);
			}
		}

		private void onEnterGame(GameEvent e)
		{
		}

		protected override void onPlaySound(GameEvent e)
		{
			MediaClient.getInstance();
		}
	}
}
