using SimpleFramework;
using SimpleFramework.Manager;
using System;
using UnityEngine;

public class StartUpCommand : ControllerCommand
{
	public override void Execute(IMessage message)
	{
		if (!Util.CheckEnvironment())
		{
			return;
		}
		GameObject gameObject = GameObject.Find("GlobalGenerator");
		if (gameObject != null)
		{
			AppView appView = gameObject.AddComponent<AppView>();
		}
		AppFacade.Instance.AddManager("LuaScriptMgr", new LuaScriptMgr());
		AppFacade.Instance.AddManager<InterfaceMgr>("InterfaceMgr");
		AppFacade.Instance.AddManager<PanelManager>("PanelManager");
		AppFacade.Instance.AddManager<TimerManager>("TimeManager");
		AppFacade.Instance.AddManager<NetworkManager>("NetworkManager");
		AppFacade.Instance.AddManager<ResourceManager>("ResourceManager");
		AppFacade.Instance.AddManager<ThreadManager>("ThreadManager");
		AppFacade.Instance.AddManager<GameManager>("GameManager");
		Debug.Log("SimpleFramework StartUp-------->>>>>");
	}
}
