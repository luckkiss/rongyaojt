using Cross;
using MuGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace SimpleFramework.Manager
{
	public class GameManager : LuaBehaviour
	{
		public LuaScriptMgr uluaMgr;

		private List<string> downloadFiles = new List<string>();

		private GameObject obj_update;

		private UpdateScrollbar updateUi;

		private bool waitupdating = true;

		private void Awake()
		{
			this.Init();
		}

		private void Init()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.obj_update = GameObject.Find("canvas_main/updateui");
			LoaderBehavior.DATA_PATH = Util.DataPath;
			UnityEngine.Debug.Log("!!!!!!!!!!!!!!!!!!!!Util.DataPath:" + Util.DataPath);
			this.obj_update.SetActive(true);
			this.updateUi = this.obj_update.AddComponent<UpdateScrollbar>();
			this.updateUi.init();
			this.updateUi.clickHandle = new Action(this.load);
			this.CheckExtractResource();
		}

		public void CheckExtractResource()
		{
			if (AppConst.DebugMode && !Application.isMobilePlatform)
			{
				this.updateUi.cur = 0;
				this.OnResourceInited();
				return;
			}
			if (Globle.QSMY_CLIENT_VER < Globle.QSMY_CLIENT_LOW_VER)
			{
				this.updateUi.showComfirm("客户端版本过低，请重新下载最新的客户端", new Action(this.onUpdateNewClient));
				return;
			}
			bool flag = Directory.Exists(Util.DataPath) && Directory.Exists(Util.DataPath + "lua/") && File.Exists(Util.DataPath + "files.txt");
			int @int = PlayerPrefs.GetInt("inited");
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"???????????CheckExtractResource::",
				flag,
				" ",
				@int,
				"  ",
				Application.isMobilePlatform
			}));
			if (flag && @int == 1)
			{
				base.StartCoroutine(this.OnUpdateResource());
				return;
			}
			PlayerPrefs.SetInt("inited", 0);
			base.StartCoroutine(this.OnExtractResource());
		}

		[DebuggerHidden]
		private IEnumerator OnExtractResource()
		{
			GameManager.<OnExtractResource>c__Iterator1 <OnExtractResource>c__Iterator = new GameManager.<OnExtractResource>c__Iterator1();
			<OnExtractResource>c__Iterator.<>f__this = this;
			return <OnExtractResource>c__Iterator;
		}

		public void load()
		{
			base.StartCoroutine(this.OnUpdateResource());
		}

		[DebuggerHidden]
		private IEnumerator OnUpdateResource()
		{
			GameManager.<OnUpdateResource>c__Iterator2 <OnUpdateResource>c__Iterator = new GameManager.<OnUpdateResource>c__Iterator2();
			<OnUpdateResource>c__Iterator.<>f__this = this;
			return <OnUpdateResource>c__Iterator;
		}

		public void onLoadingError(string error)
		{
			if (!Application.isMobilePlatform || AppConst.DebugMode)
			{
				this.updateUi.cur = 0;
				this.OnResourceInited();
			}
			else
			{
				this.OnUpdateFailed(error);
			}
		}

		public void onUpdateClick()
		{
			if (!Util.IsWifi)
			{
				this.updateUi.showComfirm(ContMgr.getOutGameCont("wifi", new string[0]), new Action(this.onWifiClick));
				return;
			}
			this.waitupdating = false;
		}

		public void onUpdateNewClient()
		{
		}

		public void onWifiClick()
		{
			this.waitupdating = false;
		}

		private void OnUpdateFailed(string file)
		{
			string text = "更新失败!>" + file;
			this.updateUi.onfail(file);
		}

		private bool IsDownOK(string file)
		{
			return this.downloadFiles.Contains(file);
		}

		private void BeginDownload(string url, string file)
		{
			object[] collection = new object[]
			{
				url,
				file
			};
			ThreadEvent threadEvent = new ThreadEvent();
			threadEvent.Key = "UpdateDownload";
			threadEvent.evParams.AddRange(collection);
			base.ThreadManager.AddEvent(threadEvent, new Action<NotiData>(this.OnThreadCompleted));
		}

		private void OnThreadCompleted(NotiData data)
		{
			string evName = data.evName;
			if (evName != null)
			{
				if (GameManager.<>f__switch$map0 == null)
				{
					GameManager.<>f__switch$map0 = new Dictionary<string, int>(2)
					{
						{
							"UpdateExtract",
							0
						},
						{
							"UpdateDownload",
							1
						}
					};
				}
				int num;
				if (GameManager.<>f__switch$map0.TryGetValue(evName, out num))
				{
					if (num != 0)
					{
						if (num == 1)
						{
							this.downloadFiles.Add(data.evParam.ToString());
						}
					}
				}
			}
		}

		public void OnResourceInited()
		{
			this.updateUi.loadCompleted(new Action(this.resourceLoadedOverhandle));
		}

		private void resourceLoadedOverhandle()
		{
			base.ResManager.Initialize();
			base.LuaManager.Start();
			base.LuaManager.DoFile("Logic/Network");
			base.LuaManager.DoFile("Logic/GameManager");
			LuaBehaviour.initialize = true;
			base.CallMethod("OnInitOK", new object[0]);
			Main.instance.begin();
			UnityEngine.Object.Destroy(this.updateUi);
			this.updateUi = null;
			UnityEngine.Object.Destroy(this.obj_update);
			this.obj_update = null;
		}

		private void Update()
		{
			if (base.LuaManager != null && LuaBehaviour.initialize)
			{
				base.LuaManager.Update();
			}
		}

		private void LateUpdate()
		{
			if (base.LuaManager != null && LuaBehaviour.initialize)
			{
				base.LuaManager.LateUpate();
			}
		}

		private void FixedUpdate()
		{
			if (base.LuaManager != null && LuaBehaviour.initialize)
			{
				base.LuaManager.FixedUpdate();
			}
		}

		private void OnDestroy()
		{
			if (base.LuaManager != null)
			{
				base.LuaManager.Destroy();
				base.LuaManager = null;
			}
			UnityEngine.Debug.Log("~GameManager was destroyed");
		}
	}
}
