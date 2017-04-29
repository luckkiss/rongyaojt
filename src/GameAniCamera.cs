using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameAniCamera : MonoBehaviour
{
	private bool hasAddEndEvent = false;

	public float speed = 10f;

	public bool uiactive = false;

	public Animator ani;

	public Dictionary<string, GameObject> dEvt = new Dictionary<string, GameObject>();

	private void Start()
	{
		this.ani = base.gameObject.GetComponent<Animator>();
		this.ani.enabled = false;
		base.gameObject.SetActive(false);
	}

	public void stopAni()
	{
		NbDoItems.cacheCameraAni = this.ani;
		this.ani.speed = 0f;
	}

	public void onSpeedEnd(float endSpeed)
	{
		SceneCamera.setCamChangeToMainCam(endSpeed, new Action(this.dispose));
	}

	public void onEnd()
	{
		SceneCamera.setCamChangeToMainCam(this.speed, new Action(this.dispose));
	}

	public void onLoginEnd()
	{
		SceneCamera.ResetAfterLoginCam();
	}

	private void dispose()
	{
		this.ani = null;
		bool flag = this.uiactive;
		if (flag)
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
		}
		NpcRole[] componentsInChildren = base.transform.GetComponentsInChildren<NpcRole>();
		NpcRole[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			NpcRole npcRole = array[i];
			npcRole.dispose();
		}
		UnityEngine.Object.Destroy(base.transform.gameObject);
	}

	public void doit(string id)
	{
		bool flag = this.dEvt.ContainsKey(id);
		if (flag)
		{
			GameObject gameObject = this.dEvt[id];
			TriggerHanldePoint component = gameObject.GetComponent<TriggerHanldePoint>();
			this.dEvt.Remove(id);
			bool flag2 = component != null;
			if (flag2)
			{
				component.onTriggerHanlde();
			}
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public void shake(string pram)
	{
		string[] array = pram.Split(new char[]
		{
			','
		});
		bool flag = array.Length < 3;
		if (!flag)
		{
			SceneCamera.cameraShake(float.Parse(array[0]), int.Parse(array[1]), float.Parse(array[2]));
		}
	}
}
