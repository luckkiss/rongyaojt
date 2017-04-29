using Cross;
using GameFramework;
using MuGame;
using SimpleFramework;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class Main : MonoBehaviour
{
	private static osImpl m_os;

	public bool m_bforceOnlyU3DAssets = true;

	public bool debugMode = true;

	public int m_nTestMonsterID;

	public int m_QSMY_ver = 1511131004;

	public string m_QSMY_Update_url = "?";

	public string m_debug_SvrList_Platform = "lan";

	public string m_debug_SvrList_Platuid = "11111452";

	public ENUM_QSMY_PLATFORM CurPlatform;

	public ENUM_SDK_PLATFORM CurSDKPlatform = ENUM_SDK_PLATFORM.QISJ_QUICK;

	public int CurClientVer = 2;

	public static Main instance;

	private bool m_bCGPlayOver;

	private bool m_sdkLogined;

	private string androidJavaClass = "com.unity3d.player.UnityPlayer";

	private string androidJavaObject = "currentActivity";

	private void Start()
	{
		Main.instance = this;
		AppConst.AppName = "mu_" + this.m_QSMY_ver;
		Baselayer.setDesignContentScale(1136, 640);
		AndroidPlotformSDK.ANDROID_PLOTFORM_SDK_CALL = new Action<string>(this.AndroidJavaMethodCall);
		AndroidPlotformSDK.ANDROID_PLOTFORM_SDK_INFO_CALL = new Action<string, string>(this.AndroidJavaMethodInfoCall);
		GameSdkMgr.init();
		AnyPlotformSDK.InitSDK();
		AppConst.DebugMode = this.debugMode;
		Screen.sleepTimeout = -1;
		AssetManagerImpl.UPDATE_DOWN_PATH = Application.persistentDataPath + "/OutAssets/v/" + this.m_QSMY_ver.ToString() + "/";
		AssetManagerImpl.preparePath(AssetManagerImpl.UPDATE_DOWN_PATH);
		UnityEngine.Debug.Log(AssetManagerImpl.UPDATE_DOWN_PATH);
		DoAfterMgr.init();
		if (GameObject.Find("Sequence") == null)
		{
			GameObject gameObject = Resources.Load("qsmy_uiRoot") as GameObject;
			if (gameObject != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			}
			GameObject gameObject3 = Resources.Load("qsmy_EventSystem") as GameObject;
			if (gameObject3 != null)
			{
				GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(gameObject3);
			}
		}
		Globle.DebugMode = ((!this.debugMode) ? 1 : 2);
		if (this.CurPlatform != ENUM_QSMY_PLATFORM.QSPF_None)
		{
			Globle.DebugMode = 0;
		}
		Globle.QSMY_Platform_Index = this.CurPlatform;
		Globle.QSMY_SDK_Index = this.CurSDKPlatform;
		Globle.QSMY_CLIENT_VER = this.CurClientVer;
		Globle.WebUrl = AppConst.WebUrl;
		Globle.QSMY_game_ver = this.m_QSMY_ver.ToString();
		Globle.m_nTestMonsterID = this.m_nTestMonsterID;
		Globle.YR_srvlists__platform = this.m_debug_SvrList_Platform;
		Globle.YR_srvlists__platuid = this.m_debug_SvrList_Platuid;
		if (this.CurPlatform > ENUM_QSMY_PLATFORM.QSPF_None)
		{
			if (this.m_sdkLogined)
			{
				this.InitGameMangager();
			}
			else
			{
				base.CancelInvoke("invoke_initGame");
				base.InvokeRepeating("invoke_initGame", 0f, 0.3f);
			}
		}
		else
		{
			this.InitGameMangager();
		}
	}

	private void invoke_initGame()
	{
		if (this.m_sdkLogined)
		{
			UnityEngine.Debug.Log("进入InitGameMangager()");
			this.InitGameMangager();
			base.CancelInvoke("invoke_initGame");
		}
	}

	public void begin()
	{
		Globle.game_CrossMono = new gameST();
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"Screen.width::",
			Screen.width,
			" ",
			Screen.height
		}));
		Main.instance = this;
		Screen.SetResolution(Screen.width, Screen.height, true);
		Main.m_os = new osImpl();
		Main.m_os.init(base.gameObject, Screen.width, Screen.height);
		if (this.m_bforceOnlyU3DAssets)
		{
			os.asset.async = false;
		}
		Globle.Lan = "zh_cn";
		if (GameObject.Find("Sequence") != null)
		{
			this.CG_PlayOver();
		}
		else
		{
			PlayeLocalInfo.saveInt("cg_" + this.m_QSMY_ver, 1);
			base.StartCoroutine(this.PlayVideoCoroutine("qsmy_cg.mp4"));
		}
	}

	[DebuggerHidden]
	private IEnumerator PlayVideoCoroutine(string videoPath)
	{
		Main.<PlayVideoCoroutine>c__Iterator0 <PlayVideoCoroutine>c__Iterator = new Main.<PlayVideoCoroutine>c__Iterator0();
		<PlayVideoCoroutine>c__Iterator.<>f__this = this;
		return <PlayVideoCoroutine>c__Iterator;
	}

	private void InitGameMangager()
	{
		string name = "GameManager";
		GameObject gameObject = GameObject.Find(name);
		if (gameObject == null)
		{
			gameObject = new GameObject(name);
			gameObject.name = name;
			AppFacade.Instance.StartUp();
		}
	}

	private void platform_Call(string cmd)
	{
		AnyPlotformSDK.Call_Cmd(cmd, null, null, true);
	}

	private void CG_PlayOver()
	{
		Globle.A3_DEMO = true;
		Application.targetFrameRate = 60;
		this.CheckDeviceInfo();
		bool flag = true;
		if (Application.platform == RuntimePlatform.Android && SystemInfo.graphicsDeviceVersion.IndexOf("OpenGL ES 3.0") < 0)
		{
			flag = false;
		}
		if (flag)
		{
			FastBloom.FAST_BLOOMSHADER = Resources.Load<Shader>("ieshader/MobileBloom");
			DepthOfField34.DOF_BLUR_SHADER = Resources.Load<Shader>("ieshader/SeparableWeightedBlurDof34");
			DepthOfField34.DOF_SHADER = Resources.Load<Shader>("ieshader/DepthOfField34");
			DepthOfField34.BOKEN_SHADER = Resources.Load<Shader>("ieshader/Bokeh34");
		}
		if (PlotMain._inst.m_bUntestPlot)
		{
			this.CheckUpdateVer();
		}
	}

	private void CheckUpdateVer()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			MsgBoxMgr.getInstance().showConfirm("无法连接网络", new UnityAction(this.closeApp), null, 0);
		}
		else
		{
			login.m_QSMY_Update_url = this.m_QSMY_Update_url + this.m_QSMY_ver.ToString() + "/OutAssets/";
			this.showLoginUI();
		}
	}

	private void closeApp()
	{
		AnyPlotformSDK.Call_Cmd("close", null, null, true);
	}

	private void showLoginUI()
	{
		UnityEngine.Debug.Log("进入了新的选服务器界面");
		InterfaceMgr.getInstance().open(InterfaceMgr.LOGIN, null, false);
		if (Globle.DebugMode > 0)
		{
			GameObject gameObject = GameObject.Find("loadingLayer");
			GameObject gameObject2 = Resources.Load("login_loacal") as GameObject;
			if (gameObject && gameObject2)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
				gameObject3.transform.SetParent(gameObject.transform, false);
				gameObject3.SetActive(true);
				if (gameObject3.GetComponent<login_loacal>() == null)
				{
					gameObject3.AddComponent<login_loacal>();
				}
			}
		}
	}

	private void CheckDeviceInfo()
	{
		try
		{
			string message = string.Concat(new object[]
			{
				"\tTitle:当前系统基础信息：\n设备模型：",
				SystemInfo.deviceModel,
				"\n设备名称：",
				SystemInfo.deviceName,
				"\n设备类型：",
				SystemInfo.deviceType,
				"\n设备唯一标识符：",
				SystemInfo.deviceUniqueIdentifier,
				"\n显卡标识符：",
				SystemInfo.graphicsDeviceID,
				"\n显卡设备名称：",
				SystemInfo.graphicsDeviceName,
				"\n显卡厂商：",
				SystemInfo.graphicsDeviceVendor,
				"\n显卡厂商ID:",
				SystemInfo.graphicsDeviceVendorID,
				"\n显卡支持版本:",
				SystemInfo.graphicsDeviceVersion,
				"\n显存（M）：",
				SystemInfo.graphicsMemorySize,
				"\n显卡像素填充率(百万像素/秒)，-1未知填充率：",
				SystemInfo.graphicsPixelFillrate,
				"\n显卡支持Shader层级：",
				SystemInfo.graphicsShaderLevel,
				"\n支持最大图片尺寸：",
				SystemInfo.maxTextureSize,
				"\nnpotSupport：",
				SystemInfo.npotSupport,
				"\n操作系统：",
				SystemInfo.operatingSystem,
				"\nCPU处理核数：",
				SystemInfo.processorCount,
				"\nCPU类型：",
				SystemInfo.processorType
			});
			UnityEngine.Debug.Log(message);
			message = string.Concat(new object[]
			{
				"\nsupportedRenderTargetCount：",
				SystemInfo.supportedRenderTargetCount,
				"\nsupports3DTextures：",
				SystemInfo.supports3DTextures,
				"\nsupportsAccelerometer：",
				SystemInfo.supportsAccelerometer,
				"\nsupportsComputeShaders：",
				SystemInfo.supportsComputeShaders,
				"\nsupportsGyroscope：",
				SystemInfo.supportsGyroscope,
				"\nsupportsImageEffects：",
				SystemInfo.supportsImageEffects,
				"\nsupportsInstancing：",
				SystemInfo.supportsInstancing,
				"\nsupportsLocationService：",
				SystemInfo.supportsLocationService,
				"\nsupportsRenderTextures：",
				SystemInfo.supportsRenderTextures,
				"\nsupportsRenderToCubemap：",
				SystemInfo.supportsRenderToCubemap,
				"\nsupportsShadows：",
				SystemInfo.supportsShadows,
				"\nsupportsSparseTextures：",
				SystemInfo.supportsSparseTextures,
				"\nsupportsStencil：",
				SystemInfo.supportsStencil,
				"\nsupportsVertexPrograms：",
				SystemInfo.supportsVertexPrograms,
				"\nsupportsVibration：",
				SystemInfo.supportsVibration,
				"\n内存大小：",
				SystemInfo.systemMemorySize
			});
			UnityEngine.Debug.Log(message);
		}
		catch (Exception var_1_2EA)
		{
		}
	}

	public void initParam(uint uid, string tkn, serverData sd)
	{
		Globle.game_CrossMono = new gameST();
		if (Globle.DebugMode == 2)
		{
			Globle.setDebugServerD(sd.sid, "http://" + sd.ip + "/do.php");
		}
		Globle.game_CrossMono.init("http://10.1.8.76/do.php", sd.ip, sd.sid, sd.port, uid, sd.clnt, tkn, "main");
	}

	private void LateUpdate()
	{
		DoAfterMgr.instacne.onAfterRender();
		if (TickMgr.instance != null)
		{
			TickMgr.instance.updateAfterRender();
		}
	}

	private void Update()
	{
		AnyPlotformSDK.FrameMove();
		if (this.m_bCGPlayOver)
		{
			this.m_bCGPlayOver = false;
			this.CG_PlayOver();
		}
		if (Application.platform != RuntimePlatform.Android || Input.GetKeyUp(KeyCode.Escape))
		{
		}
		float deltaTime = Time.deltaTime;
		SceneCamera.FrameMove();
		MonsterMgr._inst.FrameMove(deltaTime);
		OtherPlayerMgr._inst.FrameMove(deltaTime);
		FollowBullet_Mgr.FrameMove(deltaTime);
		SelfRole.FrameMove(deltaTime);
		if (TickMgr.instance != null)
		{
			TickMgr.instance.update(deltaTime);
		}
	}

	private void AndroidJavaMethodCall(string androidJavaMethod)
	{
		UnityEngine.Debug.Log("AndroidJavaMethodCall::" + androidJavaMethod);
		AndroidJavaClass androidJavaClass = new AndroidJavaClass(this.androidJavaClass);
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>(this.androidJavaObject);
		@static.Call(androidJavaMethod, new object[0]);
	}

	private void AndroidJavaMethodInfoCall(string androidJavaMethod, string infoJsonString)
	{
		UnityEngine.Debug.Log("AndroidJavaMethodInfoCall::" + androidJavaMethod + " " + infoJsonString);
		AndroidJavaClass androidJavaClass = new AndroidJavaClass(this.androidJavaClass);
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>(this.androidJavaObject);
		@static.Call(androidJavaMethod, new object[]
		{
			infoJsonString
		});
	}

	private void AndroidJavaReceiveString(string jsonString)
	{
		UnityEngine.Debug.Log("AndroidJavaReceiveString::" + jsonString);
		Variant variant = JsonManager.StringToVariant(jsonString, true);
		if (variant.ContainsKey("result") && variant["result"]._int == 1 && !this.debugMode)
		{
			if (variant.ContainsKey("data"))
			{
				Variant variant2 = variant["data"];
				if (variant2.ContainsKey("versions"))
				{
					Globle.VER = variant2["versions"];
					Globle.QSMY_game_ver = Globle.VER.ToString();
				}
				if (variant2.ContainsKey("resourceurl"))
				{
					AppConst.WebUrl = (Globle.WebUrl = variant2["resourceurl"]);
				}
				if (variant2.ContainsKey("clientversions"))
				{
					int qSMY_CLIENT_LOW_VER = variant2["clientversions"];
					Globle.QSMY_CLIENT_LOW_VER = qSMY_CLIENT_LOW_VER;
				}
			}
			this.m_sdkLogined = true;
			UnityEngine.Debug.Log("m_sdkLogined = true");
		}
		AnyPlotformSDK.Cmd_CallBack(variant);
	}

	private void IOSSDKMessage(string jsonString)
	{
		UnityEngine.Debug.Log("IOS SDK Message::" + jsonString);
		Variant v = JsonManager.StringToVariant(jsonString, true);
		AnyPlotformSDK.Cmd_CallBack(v);
	}

	private void OnApplicationFocus(bool isFocus)
	{
		UnityEngine.Debug.Log("--------OnApplicationFocus---" + isFocus);
		Globle.OnApplicationFocus(isFocus);
	}
}
