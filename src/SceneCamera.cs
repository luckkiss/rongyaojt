using DG.Tweening;
using GameFramework;
using MuGame;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class SceneCamera
{
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		public static readonly SceneCamera.<>c <>9 = new SceneCamera.<>c();

		public static TweenCallback <>9__35_0;

		internal void <setCamChangeToMainCam>b__35_0()
		{
			SceneCamera.lockType = 101;
		}
	}

	public static GameObject m_curGameObj;

	public static GameObject m_curSceneObj;

	public static Transform m_curLoginCamObj;

	public static bool m_isFirstLogin = false;

	public static Transform m_curTrrigerCamObj;

	public static Transform m_curZhuanShengCamObj;

	public static GameObject m_curCamGo;

	public static Camera m_curCamera;

	public static Vector2 m_forward;

	public static Vector2 m_right;

	private static Vector3 beginRoate;

	private static Vector3 beginPos;

	public static int m_nLightGQ_Level = 1;

	public static int m_nShadowGQ_Level = 1;

	public static int m_nSceneGQ_Level = 1;

	public static float m_fScreenGQ_Level = 0f;

	public static int m_nSkillEff_Level = 2;

	private static RenderTexture m_curGameScreenTX;

	public static float m_fGameScreenPow = 1f;

	public static int lockType = 1;

	private static Action toMainFinHandle;

	private static GameObject curCam;

	private static GameObject curMiniMapCanvas;

	private static Rect CutRect = new Rect(0f, 0f, 1f, 1f);

	public static void Init()
	{
		SceneCamera.m_curGameObj = GameObject.Find("GAME_CAMERA");
		SceneCamera.m_curSceneObj = GameObject.Find("game_scene");
		bool flag = SceneCamera.m_curGameObj == null;
		if (!flag)
		{
			bool flag2 = SceneCamera.m_curSceneObj == null;
			if (flag2)
			{
				Debug.LogError("不存在对象--->game_scene");
			}
			else
			{
				SceneCamera.m_curLoginCamObj = SceneCamera.m_curSceneObj.transform.FindChild("camera_zc_ani");
				bool flag3 = SceneCamera.m_curLoginCamObj != null && SceneCamera.m_curLoginCamObj.GetComponent<GameAniLoginCamera>() == null;
				if (flag3)
				{
					SceneCamera.m_curLoginCamObj.gameObject.AddComponent<GameAniLoginCamera>();
					SceneCamera.m_curLoginCamObj.gameObject.SetActive(false);
				}
			}
			Transform transform = SceneCamera.m_curGameObj.transform.FindChild("myCamera");
			SceneCamera.m_curCamGo = transform.gameObject;
			SceneCamera.beginRoate = transform.localEulerAngles;
			SceneCamera.beginPos = transform.localPosition;
			Vector3 forward = transform.forward;
			SceneCamera.m_forward = new Vector2(forward.x, forward.z);
			SceneCamera.m_forward.Normalize();
			Vector3 right = transform.right;
			SceneCamera.m_right = new Vector2(right.x, right.z);
			SceneCamera.m_right.Normalize();
			SceneCamera.m_curCamera = transform.GetComponent<Camera>();
			SceneCamera.SetGameLight(SceneCamera.m_nLightGQ_Level);
			SceneCamera.SetGameShadow(SceneCamera.m_nShadowGQ_Level);
			SceneCamera.SetGameScene(SceneCamera.m_nSceneGQ_Level);
			SceneCamera.SetGameScreenPow(SceneCamera.m_fScreenGQ_Level);
			SceneTFX.InitScene();
			SceneCamera.Set_Sound_Effect(MediaClient.getInstance()._soundVolume);
		}
	}

	public static void Set_Sound_Effect(float v = 1f)
	{
		bool flag = v > 1f;
		if (flag)
		{
			v = 1f;
		}
		bool flag2 = v < 0f;
		if (flag2)
		{
			v = 0f;
		}
		bool flag3 = GRMap.instance.m_nCurMapID == 10;
		if (flag3)
		{
			SceneCamera.m_curSceneObj.transform.FindChild("lv3/Reverb Zone").GetComponent<AudioSource>().volume = v;
		}
	}

	public static void SetGameShadow(int lv)
	{
		SceneCamera.m_nShadowGQ_Level = lv;
		bool active = true;
		bool flag = lv > 1;
		if (flag)
		{
			active = false;
		}
		Transform transform = SceneCamera.m_curGameObj.transform.FindChild("Shadow_Camera");
		bool flag2 = transform != null;
		if (flag2)
		{
			transform.gameObject.SetActive(active);
		}
	}

	public static void SetGameLight(int lv)
	{
		SceneCamera.m_nLightGQ_Level = lv;
		bool active = true;
		bool flag = lv > 1;
		if (flag)
		{
			active = false;
		}
		Transform transform = SceneCamera.m_curGameObj.transform.FindChild("SL_scene");
		bool flag2 = transform != null;
		if (flag2)
		{
			transform.gameObject.SetActive(active);
		}
		Transform transform2 = SceneCamera.m_curGameObj.transform.FindChild("SL_monster");
		bool flag3 = transform2 != null;
		if (flag3)
		{
			transform2.gameObject.SetActive(active);
		}
		Transform transform3 = SceneCamera.m_curGameObj.transform.FindChild("DL_other");
		bool flag4 = transform3 != null;
		if (flag4)
		{
			transform3.gameObject.SetActive(active);
		}
	}

	public static void SetGameScene(int lv)
	{
		SceneCamera.m_nSceneGQ_Level = lv;
		bool flag = SceneCamera.m_curSceneObj != null;
		if (flag)
		{
			bool active = true;
			bool active2 = true;
			bool flag2 = lv == 2;
			if (flag2)
			{
				active2 = false;
			}
			bool flag3 = lv == 3;
			if (flag3)
			{
				active = false;
				active2 = false;
			}
			Transform transform = SceneCamera.m_curSceneObj.transform.FindChild("lv2");
			bool flag4 = transform != null;
			if (flag4)
			{
				transform.gameObject.SetActive(active);
			}
			Transform transform2 = SceneCamera.m_curSceneObj.transform.FindChild("lv3");
			bool flag5 = transform2 != null;
			if (flag5)
			{
				transform2.gameObject.SetActive(active2);
			}
		}
	}

	public static void SetSikillEff(int lv)
	{
		SceneCamera.m_nSkillEff_Level = lv;
	}

	public static void CheckLoginCam()
	{
		bool flag = SceneCamera.m_curLoginCamObj != null && SceneCamera.m_isFirstLogin;
		if (flag)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.RETURN_BT, null, false);
			SceneCamera.m_curLoginCamObj.gameObject.SetActive(true);
			SceneCamera.m_curGameObj.SetActive(false);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_HIDE_ALL);
			NpcMgr.instance.can_touch = false;
		}
		else
		{
			bool flag2 = SceneCamera.m_curLoginCamObj != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(SceneCamera.m_curLoginCamObj.gameObject);
			}
			SceneCamera.m_curLoginCamObj = null;
		}
	}

	public static void ResetAfterLoginCam()
	{
		InterfaceMgr.getInstance().destory(InterfaceMgr.RETURN_BT);
		UnityEngine.Object.Destroy(SceneCamera.m_curLoginCamObj.gameObject);
		SceneCamera.m_curGameObj.SetActive(true);
		InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
		NpcMgr.instance.can_touch = true;
		bool flag = GameObject.Find("a3_everydayLogin(Clone)") == null;
		if (flag)
		{
			UiEventCenter.getInstance().onWinClosed("a3_everydayLogin");
		}
	}

	public static void CheckTrrigerCam(int camid)
	{
		DoAfterMgr.instacne.addAfterRender(delegate
		{
			bool flag = SceneCamera.m_curSceneObj != null;
			if (flag)
			{
				SceneCamera.m_curTrrigerCamObj = SceneCamera.m_curSceneObj.transform.FindChild("cam_" + camid);
				bool flag2 = SceneCamera.m_curTrrigerCamObj == null;
				if (!flag2)
				{
					bool flag3 = SceneCamera.m_curTrrigerCamObj.GetComponent<GameAniTrrigerCamera>() == null;
					if (flag3)
					{
						SceneCamera.m_curTrrigerCamObj.gameObject.AddComponent<GameAniTrrigerCamera>();
					}
					SceneCamera.m_curTrrigerCamObj.gameObject.SetActive(true);
					SceneCamera.m_curGameObj.SetActive(false);
					InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_HIDE_ALL);
					NpcMgr.instance.can_touch = false;
					bool flag4 = joystick.instance != null;
					if (flag4)
					{
						joystick.instance.OnDragOut(null);
					}
				}
			}
		});
	}

	public static void ResetAfterTrrigerCam()
	{
		bool flag = SceneCamera.m_curTrrigerCamObj != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(SceneCamera.m_curTrrigerCamObj.gameObject);
		}
		SceneCamera.m_curGameObj.SetActive(true);
		InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
		NpcMgr.instance.can_touch = true;
		SceneCamera.m_curTrrigerCamObj = null;
	}

	public static void CheckZhuanShengCam()
	{
		bool flag = SceneCamera.m_curSceneObj != null;
		if (flag)
		{
			SceneCamera.m_curZhuanShengCamObj = SceneCamera.m_curSceneObj.transform.FindChild("cam_1000");
			bool flag2 = SceneCamera.m_curZhuanShengCamObj == null;
			if (!flag2)
			{
				bool flag3 = SceneCamera.m_curZhuanShengCamObj.GetComponent<GameAniZhuanShengCamera>() == null;
				if (flag3)
				{
					SceneCamera.m_curZhuanShengCamObj.gameObject.AddComponent<GameAniZhuanShengCamera>();
				}
				SceneCamera.m_curZhuanShengCamObj.gameObject.SetActive(true);
				SceneCamera.m_curGameObj.SetActive(false);
				NpcMgr.instance.can_touch = false;
				bool flag4 = joystick.instance != null && SelfRole._inst != null;
				if (flag4)
				{
					joystick.instance.OnDragOut(null);
				}
				SelfRole._inst.setPos(new Vector3(53f, 17.2f, 29f));
				SelfRole._inst.TurnToPos(new Vector3(54f, 17.2f, 24f));
				a3_task_auto.instance.stopAuto = true;
				InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_ZHUANSHENG_ANI);
			}
		}
	}

	public static void ResetAfterZhuanShengCam()
	{
		bool flag = SceneCamera.m_curZhuanShengCamObj != null;
		if (flag)
		{
			SceneCamera.m_curZhuanShengCamObj.gameObject.SetActive(false);
		}
		SceneCamera.m_curGameObj.SetActive(true);
		InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
		NpcMgr.instance.can_touch = true;
		a3_task_auto.instance.stopAuto = false;
		SceneCamera.m_curZhuanShengCamObj = null;
		InterfaceMgr.getInstance().open(InterfaceMgr.A3_RESETLVLSUCCESS, null, false);
	}

	public static void SetGameScreenPow(float pow)
	{
		SceneCamera.m_fScreenGQ_Level = pow;
		bool flag = pow > 0f;
		if (flag)
		{
			SceneCamera.m_fGameScreenPow = pow + 1f;
		}
		else
		{
			SceneCamera.m_fGameScreenPow = 1f;
		}
		bool flag2 = SceneCamera.m_curGameScreenTX != null;
		if (flag2)
		{
			SceneCamera.m_curGameScreenTX.Release();
			SceneCamera.m_curGameScreenTX = null;
			SceneCamera.m_curCamera.targetTexture = null;
			InterfaceMgr.getInstance().linkGameScreen(null);
			InterfaceMgr.getInstance().hideGameScreen();
		}
	}

	public static void setCamChangeToMainCam(float speed = 10f, Action Fin = null)
	{
		bool flag = SceneCamera.curCam == null;
		if (!flag)
		{
			Animator component = SceneCamera.curCam.transform.parent.GetComponent<Animator>();
			bool flag2 = component;
			if (flag2)
			{
				component.enabled = false;
			}
			SceneCamera.toMainFinHandle = Fin;
			bool flag3 = speed <= 0f;
			if (flag3)
			{
				SceneCamera.lockType = 101;
			}
			else
			{
				float duration = Vector3.Distance(SceneCamera.m_curGameObj.transform.position, SceneCamera.curCam.transform.position) / speed;
				SceneCamera.curCam.transform.DOMove(SelfRole._inst.m_curModel.position + SceneCamera.beginPos, duration, false);
				Tweener arg_E9_0 = SceneCamera.curCam.transform.DOLocalRotate(SceneCamera.beginRoate, duration, RotateMode.Fast);
				TweenCallback arg_E9_1;
				if ((arg_E9_1 = SceneCamera.<>c.<>9__35_0) == null)
				{
					arg_E9_1 = (SceneCamera.<>c.<>9__35_0 = new TweenCallback(SceneCamera.<>c.<>9.<setCamChangeToMainCam>b__35_0));
				}
				arg_E9_0.OnComplete(arg_E9_1);
			}
		}
	}

	public static void endStory(float speed)
	{
		bool flag = SceneCamera.curCam == null;
		if (!flag)
		{
			GameAniCamera component = SceneCamera.curCam.transform.parent.GetComponent<GameAniCamera>();
			bool flag2 = component == null;
			if (!flag2)
			{
				component.onSpeedEnd(speed);
			}
		}
	}

	public static void changeAniCamera(GameObject cam, float speed = 10f)
	{
		bool flag = SceneCamera.curCam != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(SceneCamera.curCam.gameObject);
		}
		PlayerNameUIMgr.getInstance().hideAll();
		SceneCamera.curCam = cam;
		SceneCamera.lockType = 0;
		Animator curcamani = SceneCamera.curCam.transform.parent.GetComponent<Animator>();
		SceneCamera.m_curGameObj.SetActive(false);
		SceneCamera.curCam.transform.parent.gameObject.SetActive(true);
		float duration = Vector3.Distance(SceneCamera.m_curGameObj.transform.position, SceneCamera.curCam.transform.position) / speed;
		SceneCamera.curCam.transform.DOMove(SelfRole._inst.m_curModel.position + SceneCamera.beginPos, duration, false).From<Tweener>().SetEase(Ease.Linear);
		SceneCamera.curCam.transform.DOLocalRotate(SceneCamera.beginRoate, duration, RotateMode.Fast).From<Tweener>().SetEase(Ease.Linear).OnComplete(delegate
		{
			curcamani.enabled = true;
		});
	}

	public static void cameraShake(float time, int count, float str)
	{
		bool flag = SceneCamera.curCam != null;
		if (flag)
		{
			Transform transform = SceneCamera.curCam.transform.FindChild("cam");
			bool flag2 = transform != null;
			if (flag2)
			{
				transform.DOShakePosition(time, str, count, 90f, false);
			}
		}
		else
		{
			bool flag3 = SceneCamera.m_curGameObj != null;
			if (flag3)
			{
				SceneCamera.m_curCamGo.transform.DOShakePosition(time, str, count, 90f, false).OnComplete(new TweenCallback(SceneCamera.resetMainCamPos));
			}
		}
	}

	public static void resetMainCamPos()
	{
		SceneCamera.m_curCamGo.transform.localPosition = SceneCamera.beginPos;
	}

	public static void lockTOPlayer()
	{
		SceneCamera.m_curGameObj.transform.DOMove(SelfRole._inst.m_curModel.position, 3f, false);
		SceneCamera.m_curCamGo.transform.DOLocalRotate(SceneCamera.beginRoate, 3f, RotateMode.Fast);
		SceneCamera.m_curCamGo.transform.DOLocalMove(SceneCamera.beginPos, 3f, false);
	}

	public static void FrameMove()
	{
		bool flag = SceneCamera.m_curGameObj == null;
		if (!flag)
		{
			bool flag2 = SceneCamera.m_curGameScreenTX != null;
			if (flag2)
			{
				bool active = SceneCamera.m_curGameObj.active;
				if (active)
				{
					InterfaceMgr.getInstance().showGameScreen();
				}
				else
				{
					InterfaceMgr.getInstance().hideGameScreen();
				}
			}
			bool flag3 = SceneCamera.lockType == 1;
			if (flag3)
			{
				Vector3 b = (SceneCamera.m_curGameObj.transform.position - SelfRole._inst.m_curModel.position) * 0.125f;
				SceneCamera.m_curGameObj.transform.position = SelfRole._inst.m_curModel.position + b;
			}
			else
			{
				bool flag4 = SceneCamera.lockType == 101;
				if (flag4)
				{
					bool flag5 = SceneCamera.toMainFinHandle != null;
					if (flag5)
					{
						SceneCamera.toMainFinHandle();
					}
					SceneCamera.curCam = null;
					SceneCamera.m_curGameObj.SetActive(true);
					SceneCamera.lockType = 1;
					PlayerNameUIMgr.getInstance().showAll();
				}
			}
		}
	}

	public static void refreshMiniMapCanvas()
	{
		GameObject camGo = a3_liteMinimap.camGo;
		SceneCamera.curMiniMapCanvas = GameObject.Find("MINIMAP_CANVAS");
		bool flag = SceneCamera.curMiniMapCanvas == null;
		if (!flag)
		{
			Shader shader = Shader.Find("UI/Unlit/Transparent");
			Texture2D texture2D = Resources.Load("map/map" + GRMap.curSvrConf["id"], typeof(Texture2D)) as Texture2D;
			bool flag2 = texture2D != null;
			if (flag2)
			{
				SceneCamera.curMiniMapCanvas.GetComponent<Renderer>().material.SetTexture("_MainTex", texture2D);
				SceneCamera.curMiniMapCanvas.GetComponent<Renderer>().material.shader = shader;
			}
			else
			{
				Transform transform = (camGo != null) ? camGo.transform.FindChild("camera") : null;
				GameObject gameObject = new GameObject();
				Camera camera = gameObject.AddComponent<Camera>();
				camera.backgroundColor = new Color(0f, 0f, 0f);
				camera.orthographic = true;
				camera.orthographicSize = SceneCamera.curMiniMapCanvas.transform.localScale.x * 0.5f;
				Vector3 position = SceneCamera.curMiniMapCanvas.transform.position;
				position.y = 50f;
				gameObject.transform.position = position;
				bool flag3 = transform != null;
				if (flag3)
				{
					gameObject.transform.rotation = transform.rotation;
				}
				camera.cullingMask = (1 << EnumLayer.LM_SCENE_NORMAL) + (1 << EnumLayer.LM_SCENE_SHADOW) + (1 << EnumLayer.LM_FX);
				RenderTexture renderTexture = new RenderTexture(512, 512, 2);
				camera.pixelRect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
				camera.targetTexture = renderTexture;
				Texture2D texture2D2 = new Texture2D((int)(512f * SceneCamera.CutRect.width), (int)(512f * SceneCamera.CutRect.height), TextureFormat.RGB24, false);
				camera.Render();
				RenderTexture.active = renderTexture;
				texture2D2.ReadPixels(new Rect(512f * SceneCamera.CutRect.x, 512f * SceneCamera.CutRect.y, 512f * SceneCamera.CutRect.width, 512f * SceneCamera.CutRect.height), 0, 0);
				texture2D2.Apply();
				SceneCamera.curMiniMapCanvas.GetComponent<Renderer>().material.SetTexture("_MainTex", texture2D2);
				shader = Shader.Find("UI/Unlit/Transparent");
				SceneCamera.curMiniMapCanvas.GetComponent<Renderer>().material.shader = shader;
				camera.targetTexture = null;
				RenderTexture.active = null;
				UnityEngine.Object.Destroy(renderTexture);
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public static Vector2 getPosOnMiniMap(float picscale = 1f)
	{
		Vector3 position = SelfRole._inst.m_curModel.position;
		float num = SceneCamera.curMiniMapCanvas.transform.localScale.x / 1024f;
		Vector3 localPosition = SceneCamera.curMiniMapCanvas.transform.localPosition;
		Vector2 a = new Vector2(-((position.x - localPosition.x) / num), -((position.z - localPosition.z) / num));
		return a * picscale;
	}

	public static Vector2 getTeamPosOnMinMap(int x, int y, float picscale = 1f)
	{
		Vector3 vector = new Vector3((float)x, 0f, (float)y);
		float num = SceneCamera.curMiniMapCanvas.transform.localScale.x / 1024f;
		Vector3 localPosition = SceneCamera.curMiniMapCanvas.transform.localPosition;
		Vector2 a = new Vector2(-((vector.x - localPosition.x) / num), -((vector.z - localPosition.z) / num));
		return a * picscale;
	}

	public static Vector2 getPosOnMiniMapNMA(Vector3 v3, float picscale = 0.45f)
	{
		float num = SceneCamera.curMiniMapCanvas.transform.localScale.x / 1024f;
		Vector3 localPosition = SceneCamera.curMiniMapCanvas.transform.localPosition;
		Vector2 a = new Vector2(-((v3.x - localPosition.x) / num), -((v3.z - localPosition.z) / num));
		return a * picscale;
	}

	public static Vector2 getPosOnMiniMap(Vector3 worldpos)
	{
		float num = SceneCamera.curMiniMapCanvas.transform.localScale.x / 1024f;
		Vector3 localPosition = SceneCamera.curMiniMapCanvas.transform.localPosition;
		Vector2 result = new Vector2(-((worldpos.x - localPosition.x) / num), -((worldpos.z - localPosition.z) / num));
		return result;
	}

	public static float getLengthOnMinMap(float worldLength)
	{
		float num = SceneCamera.curMiniMapCanvas.transform.localScale.x / 1024f;
		return worldLength / num;
	}

	public static Vector3 getPosByMiniMap(Vector2 pos, float picscale, float distenc = 20f)
	{
		bool flag = SceneCamera.curMiniMapCanvas == null;
		Vector3 result;
		if (flag)
		{
			result = Vector3.zero;
		}
		else
		{
			Vector3 localPosition = SceneCamera.curMiniMapCanvas.transform.localPosition;
			Vector3 vector = pos / picscale;
			float num = SceneCamera.curMiniMapCanvas.transform.localScale.x / 1024f;
			vector = new Vector3(-vector.x * num + localPosition.x, 10f, -vector.y * num + localPosition.z);
			NavMeshHit navMeshHit;
			NavMesh.SamplePosition(vector, out navMeshHit, distenc, NavmeshUtils.allARE);
			result = navMeshHit.position;
		}
		return result;
	}
}
