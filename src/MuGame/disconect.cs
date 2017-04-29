using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class disconect : Window
	{
		public static int ERROR_TYPE_DISCONNECT = 0;

		public static int ERROR_TYPE_SOCKET = 1;

		public static int ERROR_TYPE_SERVER = 2;

		public static int ERROR_TYPE_NETWORK = 3;

		public static bool showLine = false;

		public static bool needResetMusic = false;

		private GameObject goInfo;

		private GameObject goLoading;

		private Text txt;

		public static disconect instance;

		private int timers;

		public override bool showBG
		{
			get
			{
				return true;
			}
		}

		public override void init()
		{
			this.goInfo = base.getGameObjectByPath("info");
			this.goLoading = base.getGameObjectByPath("loading");
			this.txt = base.getComponentByPath<Text>("info/txt");
			base.getEventTrigerByPath("info/bt").onClick = new EventTriggerListener.VoidDelegate(this.onCLick);
		}

		public override void onShowed()
		{
			base.transform.SetAsLastSibling();
			disconect.instance = this;
			NetClient.instance.addEventListener(3013u, new Action<GameEvent>(this.onError));
			this.setErrorType(disconect.ERROR_TYPE_DISCONNECT);
			base.onShowed();
			bool flag = a3_wing_skin.instance != null;
			if (flag)
			{
				a3_wing_skin.instance.wingAvatar.SetActive(false);
			}
			bool flag2 = a3_summon.instan && a3_summon.instan.m_selectedSummon;
			if (flag2)
			{
				a3_summon.instan.m_selectedSummon.SetActive(false);
			}
			NewbieModel.getInstance().hide();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_DIS_CONECT);
		}

		public override void onClosed()
		{
			disconect.instance = null;
			NetClient.instance.removeEventListener(3013u, new Action<GameEvent>(this.onError));
			base.onClosed();
		}

		public void onError(GameEvent e)
		{
			this.setErrorType(disconect.ERROR_TYPE_SOCKET);
		}

		public void setErrorType(int type)
		{
			bool flag = muNetCleint.instance.CurServerTimeStamp - this.timers < 3;
			if (flag)
			{
				DelayDoManager.singleton.AddDelayDo(delegate
				{
					bool flag2 = disconect.instance != null;
					if (flag2)
					{
						this.refreshText(type);
					}
				}, 3, 0u);
			}
			else
			{
				this.refreshText(type);
			}
		}

		public void refreshText(int type)
		{
			this.goLoading.SetActive(false);
			this.goInfo.SetActive(true);
			this.txt.text = StringUtils.formatText(ContMgr.getCont("disconect_" + type, null));
		}

		public override void dispose()
		{
			disconect.instance = null;
			base.getComponentByPath<Button>("bt").onClick.RemoveAllListeners();
			NetClient.instance.removeEventListener(3013u, new Action<GameEvent>(this.onError));
			base.dispose();
		}

		private void onCLick(GameObject go)
		{
			bool mUSIC_ON = GlobleSetting.MUSIC_ON;
			if (mUSIC_ON)
			{
				disconect.needResetMusic = true;
			}
			this.goLoading.SetActive(true);
			this.goInfo.SetActive(false);
			this.timers = muNetCleint.instance.CurServerTimeStamp;
			LGPlatInfo.inst.relogin();
			MapModel.getInstance().curLevelId = 0u;
		}
	}
}
