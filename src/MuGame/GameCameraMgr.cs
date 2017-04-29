using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class GameCameraMgr
	{
		private processStruct process;

		private Transform transUser;

		private Transform curTransCamera;

		private GameObject curGoCamera;

		private CameraAniTempCS curScCamera;

		private Animator animator;

		private static GameCameraMgr instance;

		public void useCamera(string id)
		{
			bool flag = this.transUser == null;
			if (flag)
			{
				this.transUser = FightAniUserTempSC.goUser.transform;
			}
			bool flag2 = this.process == null;
			if (flag2)
			{
				this.process = new processStruct(new Action<float>(this.onUpdate), "GameCameraMgr", false, false);
			}
			this.clearCurCamera();
			GameObject gameObject = U3DAPI.U3DResLoad<GameObject>(id);
			bool flag3 = gameObject == null;
			if (!flag3)
			{
				this.curGoCamera = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				this.curGoCamera.transform.SetParent(this.transUser, false);
				this.curScCamera = this.curGoCamera.transform.FindChild("Camera").GetComponent<CameraAniTempCS>();
				bool flag4 = this.curScCamera == null;
				if (flag4)
				{
					this.curScCamera = this.curGoCamera.transform.FindChild("Camera").gameObject.AddComponent<CameraAniTempCS>();
				}
				this.curTransCamera = this.curGoCamera.transform.FindChild("Camera");
				this.animator = this.curGoCamera.transform.FindChild("Camera").GetComponent<Animator>();
				this.animator.speed = 1f;
				GRMap.GAME_CAMERA.SetActive(false);
				(CrossApp.singleton.getPlugin("processManager") as processManager).addProcess(this.process, false);
			}
		}

		public void stop()
		{
			bool flag = this.curScCamera == null;
			if (!flag)
			{
				this.animator.speed = 0f;
			}
		}

		private void onUpdate(float s)
		{
			bool flag = this.curScCamera == null;
			if (!flag)
			{
				bool stop = this.curScCamera.stop;
				if (!stop)
				{
					bool lookatUser = this.curScCamera.lookatUser;
					if (lookatUser)
					{
						this.curTransCamera.LookAt(this.transUser);
					}
					bool flag2 = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
					if (flag2)
					{
						this.clearCurCamera();
					}
				}
			}
		}

		public void clearCurCamera()
		{
			bool flag = this.curGoCamera == null;
			if (!flag)
			{
				this.curScCamera.clearAllPrelab();
				GRMap.GAME_CAMERA.SetActive(true);
				UnityEngine.Object.Destroy(this.curGoCamera);
				this.curGoCamera = null;
				this.curScCamera = null;
				this.animator = null;
				Globle.setTimeScale(1f);
			}
		}

		public static GameCameraMgr getInstance()
		{
			bool flag = GameCameraMgr.instance == null;
			if (flag)
			{
				GameCameraMgr.instance = new GameCameraMgr();
			}
			return GameCameraMgr.instance;
		}
	}
}
