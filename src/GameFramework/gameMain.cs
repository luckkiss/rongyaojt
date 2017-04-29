using Cross;
using System;
using UnityEngine;

namespace GameFramework
{
	public abstract class gameMain
	{
		public IClientBase g_gameM;

		public IClientBase g_netM;

		public IClientBase g_sceneM;

		public IClientBase g_uiM;

		public IClientBase g_gameConfM;

		public gameMain()
		{
		}

		public void init(Variant parma)
		{
			bool flag = CrossApp.singleton == null;
			if (flag)
			{
				Debug.Log("gameMain.............1");
				new CrossApp(true);
				Debug.Log("gameMain.............1-1");
				CrossApp.singleton.regPlugin(new gameEventDelegate());
				Debug.Log("gameMain.............1-2");
				CrossApp.singleton.regPlugin(new processManager());
				Debug.Log("gameMain.............1-3");
				CrossApp.singleton.init();
				Debug.Log("gameMain.............1-4");
				this.g_gameM = this.createGameManager();
				Debug.Log("gameMain.............1-5");
				this.g_netM = this.createNetManager();
				Debug.Log("gameMain.............1-6");
				this.g_sceneM = this.createSceneManager();
				Debug.Log("gameMain.............1-7");
				this.g_uiM = this.createUiManager();
				Debug.Log("gameMain.............1-8");
				this.g_gameConfM = this.createGameConfingManager();
				Debug.Log("gameMain.............1-9");
				this.g_netM.init();
				Debug.Log("gameMain.............1-10");
				this.g_gameM.init();
				Debug.Log("gameMain.............1-11");
				this.g_gameConfM.init();
				Debug.Log("gameMain.............1-12");
				this.g_sceneM.init();
				Debug.Log("gameMain.............1-13");
				this.g_uiM.init();
				Debug.Log("gameMain.............1-14");
			}
			Debug.Log("gameMain.............2");
			this.onInit(parma);
			Debug.Log("gameMain.............2-ee");
		}

		protected abstract void onInit(Variant parma);

		protected abstract IClientBase createGameManager();

		protected abstract IClientBase createNetManager();

		protected abstract IClientBase createSceneManager();

		protected abstract IClientBase createUiManager();

		protected abstract IClientBase createGameConfingManager();
	}
}
