using MuGame;
using System;
using UnityEngine;

public class GameAniZhuanShengCamera : MonoBehaviour
{
	public void onEnd()
	{
		SceneCamera.ResetAfterZhuanShengCam();
	}

	public void onZhuanSheng1()
	{
		NpcMgr.instance.getRole(1013).playSkill();
	}

	public void onZhuanSheng2()
	{
		SelfRole._inst.m_curAni.SetTrigger("zhuansheng");
	}
}
