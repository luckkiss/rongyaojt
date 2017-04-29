using Cross;
using GameFramework;
using MuGame.Qsmy.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MuGame
{
	public class GRMap : GRBaseImpls
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly GRMap.<>c <>9 = new GRMap.<>c();

			public static Action<IURLReq, string> <>9__40_1;

			public static Action <>9__49_0;

			internal void <REV_SOUNDRES_LINKER>b__40_1(IURLReq req, string err)
			{
				Debug.Log("加载剧情声音失败 " + req.url);
			}

			internal void <a3_scene_loaded>b__49_0()
			{
				float y = 0f;
				bool flag = GRMap.curSvrConf["id"] == 10;
				if (flag)
				{
					y = 18f;
				}
				SelfRole._inst.setPos(new Vector3(ModelBase<PlayerModel>.getInstance().mapBeginX, y, ModelBase<PlayerModel>.getInstance().mapBeginY));
				bool flag2 = ModelBase<PlayerModel>.getInstance().mapBeginroatate > 0f;
				if (flag2)
				{
					SelfRole._inst.setRoleRoatate(ModelBase<PlayerModel>.getInstance().mapBeginroatate);
					ModelBase<PlayerModel>.getInstance().mapBeginroatate = 0f;
				}
				bool flag3 = MapModel.getInstance().CheckAutoPlay();
				if (flag3)
				{
					SelfRole.fsm.StartAutofight();
				}
			}
		}

		public static GRMap instance;

		public static bool haveListener = false;

		public static bool loading = true;

		private static bool firstInGame = true;

		private Variant fly_array = new Variant();

		public static GameObject GAME_CAMERA;

		public static Camera GAME_CAM_CAMERA;

		public static float M_FToCameraNearStep = 0f;

		public static Vector3 M_VGame_Cam_FARpos = default(Vector3);

		public static Vector3 M_VGame_Cam_FARrot = default(Vector3);

		public static GameObject GAME_CAM_CUR = new GameObject();

		public static int LEVEL_PLOT_ID = 0;

		private uint m_nCurMapSceneSettingID;

		public int m_nCurMapID;

		private AssetBundle m_curAssetBundlePlot = null;

		private AssetBundle m_curAssetBundleScene = null;

		private static List<AudioClip> CUR_PLOT_SOUNDS = new List<AudioClip>();

		private static Action S_PLOT_PLAYOVER_CB = null;

		public static Variant curSvrMsg;

		public static Variant curSvrConf;

		public static bool playingPlot = false;

		private static bool sdk_sendroleLogin = true;

		public static int changeMapTimeSt = 0;

		private int m_nScene_Loaded_CallNextFrame = 0;

		private static bool initedDontDestory = false;

		private List<int> showid = new List<int>();

		private bool fristloaded = true;

		public static Action CUR_POLTOVER_CB = null;

		private Variant changeMapData;

		public IGRMap m_map
		{
			get
			{
				return this.m_gr as IGRMap;
			}
			set
			{
				this.m_gr = value;
			}
		}

		private lgSelfPlayer lgMainPlayer
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).getObject("LG_MAIN_PLAY") as lgSelfPlayer;
			}
		}

		public GRMap(muGRClient m) : base(m)
		{
			GRMap.instance = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new GRMap(m as muGRClient);
		}

		public override void init()
		{
		}

		protected override void onSetSceneCtrl()
		{
			bool flag = GRMap.haveListener;
			if (!flag)
			{
				this.m_ctrl.addEventListener(2100u, new Action<GameEvent>(this.setData));
				this.m_ctrl.addEventListener(2160u, new Action<GameEvent>(this.onChangeMap));
				GRMap.haveListener = true;
			}
		}

		protected override void onSetGraphImpl()
		{
		}

		private void setData(GameEvent e)
		{
			bool flag = TriggerHanldePoint.lGo != null;
			if (flag)
			{
				TriggerHanldePoint.lGo.Clear();
				TriggerHanldePoint.lGo = null;
			}
			Variant data = e.data;
			Variant svrConf = data["conf"];
			this.setData(data["mapid"]._uint, svrConf, data["localConf"]);
		}

		public static void SetPoltOver_EnterLevel(Action plot_over_callback)
		{
			GRMap.S_PLOT_PLAYOVER_CB = plot_over_callback;
		}

		public void refreshLightMap()
		{
		}

		private void setData(uint scene_setting_mapid, Variant svrConf, Variant localConf_TO_DEL)
		{
			GRMap.changeMapTimeSt = NetClient.instance.CurServerTimeStamp;
			GRMap.curSvrConf = svrConf;
			InterfaceMgr.doCommandByLua("MapModel:getInstance().getmapinfo", "model/MapModel", new object[]
			{
				GRMap.curSvrConf
			});
			debug.Log("C#1::::" + svrConf.dump());
			Resources.UnloadUnusedAssets();
			GC.Collect();
			bool flag = GRMap.sdk_sendroleLogin;
			if (flag)
			{
				GRMap.sdk_sendroleLogin = false;
			}
			MouseClickMgr.init();
			int @int = svrConf["id"]._int;
			this.m_nCurMapSceneSettingID = scene_setting_mapid;
			this.m_nCurMapID = @int;
			bool flag2 = false;
			int num = -1;
			for (int i = 0; i < ModelBase<AutoPlayModel>.getInstance().autoplayCfg4FB.Count; i++)
			{
				bool flag3 = ModelBase<AutoPlayModel>.getInstance().autoplayCfg4FB[i].map.Contains(this.m_nCurMapID);
				if (flag3)
				{
					flag2 = true;
					num = i;
					break;
				}
			}
			bool flag4 = flag2;
			if (flag4)
			{
				bool flag5 = num != -1;
				if (flag5)
				{
					StateInit.Instance.Distance = ModelBase<AutoPlayModel>.getInstance().autoplayCfg4FB[num].Distance;
					StateInit.Instance.PickDistance = ModelBase<AutoPlayModel>.getInstance().autoplayCfg4FB[num].DistancePick;
				}
			}
			else
			{
				StateInit.Instance.Distance = StateInit.Instance.DistanceNormal;
				StateInit.Instance.PickDistance = StateInit.Instance.PickDistanceNormal;
			}
			InterfaceMgr.getInstance().closeAllWin("");
			bool flag6 = BaseProxy<MapProxy>.getInstance().openWin != null && BaseProxy<MapProxy>.getInstance().openWin != "";
			if (flag6)
			{
				bool flag7 = BaseProxy<MapProxy>.getInstance().Win_uiData != null && BaseProxy<MapProxy>.getInstance().Win_uiData != "";
				if (flag7)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(BaseProxy<MapProxy>.getInstance().Win_uiData);
					InterfaceMgr.getInstance().open(BaseProxy<MapProxy>.getInstance().openWin, arrayList, false);
				}
				else
				{
					InterfaceMgr.getInstance().open(BaseProxy<MapProxy>.getInstance().openWin, null, false);
				}
				BaseProxy<MapProxy>.getInstance().openWin = null;
				BaseProxy<MapProxy>.getInstance().Win_uiData = null;
			}
			combo_txt.clear();
			GRMap.LEVEL_PLOT_ID = 0;
			this.REV_RES_LIST_OK();
			this.REV_PLOT_PLAY_OVER();
		}

		private static void REV_CHARRES_LINKER_GO(GameObject[] linker, PLOT_CHARRES_TYPE res_type, string str_id, Dictionary<string, AnimationClip> anim_loop, string first_anim)
		{
			string res_url = "null";
			switch (res_type)
			{
			case PLOT_CHARRES_TYPE.PCRT_HERO:
				res_url = string.Concat(new string[]
				{
					"QSMY/character/hero/",
					str_id,
					"/",
					str_id,
					".res"
				});
				break;
			case PLOT_CHARRES_TYPE.PCRT_MONSTER:
				res_url = string.Concat(new string[]
				{
					"QSMY/character/monster/",
					str_id,
					"/",
					str_id,
					".res"
				});
				break;
			case PLOT_CHARRES_TYPE.PCRT_NPC:
				res_url = string.Concat(new string[]
				{
					"QSMY/character/npc/",
					str_id,
					"/",
					str_id,
					".res"
				});
				break;
			case PLOT_CHARRES_TYPE.PCRT_MOUNT:
				res_url = string.Concat(new string[]
				{
					"QSMY/character/mount/",
					str_id,
					"/",
					str_id,
					".res"
				});
				break;
			case PLOT_CHARRES_TYPE.PCRT_AVATAR:
				res_url = "QSMY/character/avatar/" + str_id + ".res";
				break;
			}
			IAsset asset = os.asset.getAsset<IAssetMesh>(res_url, delegate(IAsset ast)
			{
				for (int i = 0; i < linker.Length; i++)
				{
					GameObject gameObject = linker[i];
					bool flag = gameObject == null;
					if (!flag)
					{
						GameObject assetObj = (ast as AssetMeshImpl).assetObj;
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(assetObj);
						gameObject2.transform.SetParent(gameObject.transform, false);
						Animation animation = gameObject2.GetComponent<Animation>();
						bool flag2 = animation == null;
						if (flag2)
						{
							animation = gameObject2.AddComponent<Animation>();
						}
						bool flag3 = animation != null && anim_loop != null;
						if (flag3)
						{
							foreach (string current in anim_loop.Keys)
							{
								animation.AddClip(anim_loop[current], current);
							}
							animation.Play(first_anim);
							animation.wrapMode = WrapMode.Loop;
						}
					}
				}
			}, null, delegate(IAsset ast, string err)
			{
				Debug.Log("加载剧情Res失败" + res_url);
			});
			(asset as AssetImpl).loadImpl(false);
		}

		public static void REV_CHARRES_LINKER(GameObject[] linker, PLOT_CHARRES_TYPE res_type, int id, string[] anim_list)
		{
			string str_id = id.ToString();
			int nanim_count = anim_list.Length;
			bool flag = nanim_count == 0;
			if (flag)
			{
				GRMap.REV_CHARRES_LINKER_GO(linker, res_type, str_id, null, null);
			}
			else
			{
				Dictionary<string, AnimationClip> anim_loop = new Dictionary<string, AnimationClip>();
				string strfirst_anim = null;
				for (int i = 0; i < anim_list.Length; i++)
				{
					string curanim = anim_list[i];
					bool flag2 = i == 0;
					if (flag2)
					{
						strfirst_anim = curanim;
					}
					string anim_url = "null";
					switch (res_type)
					{
					case PLOT_CHARRES_TYPE.PCRT_HERO:
						anim_url = "QSMY/character/hero/" + str_id + "/" + curanim;
						break;
					case PLOT_CHARRES_TYPE.PCRT_MONSTER:
						anim_url = "QSMY/character/monster/" + str_id + "/" + curanim;
						break;
					case PLOT_CHARRES_TYPE.PCRT_NPC:
						anim_url = "QSMY/character/npc/" + str_id + "/" + curanim;
						break;
					case PLOT_CHARRES_TYPE.PCRT_MOUNT:
						anim_url = "QSMY/character/mount/" + str_id + "/" + curanim;
						break;
					}
					IAsset asset = os.asset.getAsset<IAssetSkAnimation>(anim_url, delegate(IAsset ast)
					{
						int nanim_count = nanim_count;
						nanim_count--;
						anim_loop[curanim] = (ast as AssetSkAnimationImpl).anim;
						Debug.Log(nanim_count + "成功加载了动作 " + anim_url);
						bool flag3 = nanim_count == 0;
						if (flag3)
						{
							GRMap.REV_CHARRES_LINKER_GO(linker, res_type, str_id, anim_loop, strfirst_anim);
						}
					}, null, delegate(IAsset ast, string err)
					{
						Debug.Log("加载动作失败 " + anim_url);
						int nanim_count = nanim_count;
						nanim_count--;
					});
					(asset as AssetImpl).loadImpl(false);
				}
			}
		}

		public static void REV_FXRES_LINKER(GameObject[] linker, string fx_file)
		{
			IAsset asset = os.asset.getAsset<IAssetParticles>(fx_file, delegate(IAsset ast)
			{
				for (int i = 0; i < linker.Length; i++)
				{
					GameObject gameObject = linker[i];
					GameObject assetObj = (ast as AssetParticlesImpl).assetObj;
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(assetObj);
					gameObject2.transform.SetParent(gameObject.transform, false);
					gameObject.SetActive(false);
				}
			}, null, delegate(IAsset ast, string err)
			{
				Debug.Log("加载特效失败" + fx_file);
			});
			(asset as AssetImpl).loadImpl(false);
		}

		public static void ClearWaitPlotSound()
		{
			GRMap.CUR_PLOT_SOUNDS.Clear();
		}

		public static bool HasNoWaitPlotSound()
		{
			bool result = true;
			for (int i = 0; i < GRMap.CUR_PLOT_SOUNDS.Count; i++)
			{
				bool flag = !GRMap.CUR_PLOT_SOUNDS[i].isReadyToPlay;
				if (flag)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		public static void REV_SOUNDRES_LINKER(GameObject linker, int id)
		{
			Debug.Log("加载剧情声音");
			string str = id.ToString();
			URLReqImpl arg_77_0 = new URLReqImpl
			{
				dataFormat = "assetbundle",
				url = "media/plot/" + str + ".snd"
			};
			Action<IURLReq, object> arg_77_1 = delegate(IURLReq req, object ret)
			{
				AudioSource audioSource = linker.GetComponent<AudioSource>();
				bool flag = audioSource == null;
				if (flag)
				{
					audioSource = linker.AddComponent<AudioSource>();
				}
				AudioClip audioClip = ret as AudioClip;
				audioSource.clip = audioClip;
				GRMap.CUR_PLOT_SOUNDS.Add(audioClip);
			};
			Action<IURLReq, float> arg_77_2 = null;
			Action<IURLReq, string> arg_77_3;
			if ((arg_77_3 = GRMap.<>c.<>9__40_1) == null)
			{
				arg_77_3 = (GRMap.<>c.<>9__40_1 = new Action<IURLReq, string>(GRMap.<>c.<>9.<REV_SOUNDRES_LINKER>b__40_1));
			}
			arg_77_0.load(arg_77_1, arg_77_2, arg_77_3);
		}

		public static void REV_ZIMU_TEXT(string zimu)
		{
			bool flag = zimu != null;
			if (flag)
			{
				storydialog.show(zimu);
			}
			else
			{
				storydialog.show("");
			}
		}

		public static void REV_PLOT_UI(string plot_ui)
		{
			plot_linkui.show(plot_ui);
		}

		public static void DontDestroyBaseGameObject()
		{
			bool flag = !GRMap.initedDontDestory;
			if (flag)
			{
				UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
				UnityEngine.Object[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					UnityEngine.Object mono = array2[i];
					Application.DontDestroyOnLoad(mono);
				}
				GRMap.initedDontDestory = true;
			}
		}

		public void REV_RES_LIST_OK()
		{
			bool flag = GameRoomMgr.getInstance().curRoom != null;
			if (flag)
			{
				GameRoomMgr.getInstance().curRoom.clear();
			}
			MediaClient.getInstance().clearMusic();
			NpcMgr.instance.clear();
			MonsterMgr._inst.clear();
			OtherPlayerMgr._inst.clear();
			LGUIMainUIImpl_NEED_REMOVE.CHECK_MAPLOADING_LAYER = true;
			bool flag2 = this.m_curAssetBundleScene != null;
			if (flag2)
			{
				this.m_curAssetBundleScene.Unload(false);
				this.m_curAssetBundleScene = null;
			}
			GRMap.loading = true;
			MediaClient.getInstance().clearMusic();
			GRMap.DontDestroyBaseGameObject();
			GameEventTrigger.clear();
			Debug.Log("开始加载地图");
			SceneManager.LoadScene(GRMap.curSvrConf["name"]._str);
			this.m_nScene_Loaded_CallNextFrame = 2;
			bool flag3 = BaseRoomItem.instance != null;
			if (flag3)
			{
				BaseRoomItem.instance.clear();
			}
			bool flag4 = login.instance == null;
			if (flag4)
			{
				this.refreshLightMap();
			}
			this.m_nCurMapSceneSettingID = 0u;
		}

		private void a3_scene_loaded()
		{
			bool flag = !GRMap.loading;
			if (!flag)
			{
				MediaClient.instance.clearMusic();
				SceneCamera.Init();
				bool flag2 = SelfRole._inst != null;
				if (flag2)
				{
					SelfRole._inst.dispose();
				}
				Debug.Log("初始化角色");
				SelfRole.Init();
				SelfRole._inst.m_unIID = ModelBase<PlayerModel>.getInstance().iid;
				SelfRole._inst.m_curModel.position = ModelBase<PlayerModel>.getInstance().enter_map_pos;
				SelfRole._inst.setNavLay(NavmeshUtils.listARE[1]);
				Time.fixedDeltaTime = 0.02f;
				GameObject gameObject = GameObject.Find("SVR_DATA");
				bool flag3 = gameObject != null;
				if (flag3)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				DoAfterMgr arg_DB_0 = DoAfterMgr.instacne;
				Action arg_DB_1;
				if ((arg_DB_1 = GRMap.<>c.<>9__49_0) == null)
				{
					arg_DB_1 = (GRMap.<>c.<>9__49_0 = new Action(GRMap.<>c.<>9.<a3_scene_loaded>b__49_0));
				}
				arg_DB_0.addAfterRender(arg_DB_1);
				GameRoomMgr.getInstance().onChangeLevel(GRMap.curSvrConf, GRMap.curSvrMsg);
				GRMap.loading = false;
				OtherPlayerMgr._inst.onMapLoaded();
				MonsterMgr._inst.onMapLoaded();
				BaseProxy<MapProxy>.getInstance().changingMap = false;
				bool flag4 = !this.fristloaded;
				if (flag4)
				{
					bool flag5 = this.showid.Contains(GRMap.curSvrConf["id"]);
					if (flag5)
					{
						InterfaceMgr.getInstance().close(InterfaceMgr.A3_PKMAPUI);
					}
					else
					{
						bool flag6 = GRMap.curSvrConf.ContainsKey("pk_hint") && GRMap.curSvrConf["pk_hint"] == 1;
						if (flag6)
						{
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_PKMAPUI, null, false);
							this.showid.Add(GRMap.curSvrConf["id"]);
						}
						else
						{
							InterfaceMgr.getInstance().close(InterfaceMgr.A3_PKMAPUI);
						}
					}
				}
				bool flag7 = GRMap.curSvrConf.ContainsKey("pk_lock");
				if (flag7)
				{
					InterfaceMgr.doCommandByLua("herohead2.refreskCanPk", "ui/interfaces/floatui/herohead2", new object[]
					{
						false
					});
				}
				else
				{
					InterfaceMgr.doCommandByLua("herohead2.refreskCanPk", "ui/interfaces/floatui/herohead2", new object[]
					{
						true
					});
				}
				bool flag8 = skillbar.instance != null;
				if (flag8)
				{
					bool flag9 = GRMap.curSvrConf["id"] == 10;
					if (flag9)
					{
						skillbar.instance.ShowCombatUI(false);
					}
					else
					{
						skillbar.instance.ShowCombatUI(true);
					}
				}
				bool flag10 = a3_liteMinimap.instance;
				if (flag10)
				{
					a3_liteMinimap.instance.refreshMapname();
				}
				InterfaceMgr.doCommandByLua("a3_litemap.refreshMapname", "ui/interfaces/floatui/a3_litemap", null);
				bool flag11 = skillbar.instance != null;
				if (flag11)
				{
					skillbar.instance.refreshAllSkills(SelfRole.s_bStandaloneScene ? 0 : -1);
				}
				bool flag12 = GRMap.curSvrConf.ContainsKey("music");
				if (flag12)
				{
					MediaClient.getInstance().PlayMusicUrl("audio/map/" + GRMap.curSvrConf["music"], null, true);
				}
				this.fristloaded = false;
			}
		}

		private void doLogin()
		{
			login.instance.onBeginLoading(delegate
			{
				this.DoPlotPlayOver();
			});
		}

		private void DoPlotPlayOver()
		{
			Globle.setTimeScale(1f);
			plot_linkui.ClearAll();
			InterfaceMgr.getInstance().delclose(InterfaceMgr.PLOT_LINKUI);
			GRMap.M_VGame_Cam_FARpos = GRMap.GAME_CAM_CUR.transform.localPosition;
			GRMap.M_VGame_Cam_FARrot = GRMap.GAME_CAM_CUR.transform.localEulerAngles;
			UiEventCenter.getInstance().onMapChanged();
			LGMap lGMap = GRClient.instance.g_gameM.getObject("LG_MAP") as LGMap;
			lGMap.playMapMusic(disconect.needResetMusic);
			disconect.needResetMusic = false;
		}

		private void REV_PLOT_PLAY_OVER()
		{
			GRMap.playingPlot = false;
			bool flag = GRMap.S_PLOT_PLAYOVER_CB != null;
			if (flag)
			{
				bool flag2 = login.instance;
				if (flag2)
				{
					GRMap.CUR_POLTOVER_CB = new Action(this.doLogin);
				}
				else
				{
					GRMap.CUR_POLTOVER_CB = new Action(this.DoPlotPlayOver);
				}
				GRMap.S_PLOT_PLAYOVER_CB();
				GRMap.S_PLOT_PLAYOVER_CB = null;
			}
			else
			{
				bool flag3 = login.instance;
				if (flag3)
				{
					this.doLogin();
				}
				else
				{
					this.DoPlotPlayOver();
				}
			}
		}

		private void onChangeMap(GameEvent e)
		{
			base.deleteEffects();
			bool flag = this.changeMapData != null && loading_cloud.instance != null;
			if (!flag)
			{
				debug.Log("onChangeMap:e.data::::::" + e.data);
				this.changeMapData = e.data;
				bool flag2 = loading_cloud.instance == null;
				if (flag2)
				{
					loading_cloud.showhandle = new Action(this.doChangeMap);
					InterfaceMgr.getInstance().open(InterfaceMgr.LOADING_CLOUD, null, false);
				}
				else
				{
					this.doChangeMap();
				}
			}
		}

		private void doChangeMap()
		{
			lgSelfPlayer.instance.doSkillPreload();
			GameCameraMgr.getInstance().clearCurCamera();
			bool flag = this.changeMapData != null;
			if (flag)
			{
				debug.Log(this.changeMapData.dump());
			}
			this.setData(this.changeMapData["mapid"]._uint, this.changeMapData["conf"], this.changeMapData["localConf"]);
			this.m_ctrl.dispatchEvent(GameEvent.Create(2165u, null, null, false));
			this.changeMapData = null;
		}

		private void onViewSizeChange(GameEvent e)
		{
			Variant data = e.data;
		}

		private void onLinkAdd(GameEvent e)
		{
			Variant data = e.data;
		}

		private void upDateView(GameEvent e)
		{
		}

		public override void dispose()
		{
		}

		private void onAddEff(GameEvent e)
		{
			Variant data = e.data;
			string str = data["effid"]._str;
			float @float = data["x"]._float;
			float float2 = data["y"]._float;
			bool loop = false;
			float num = 0f;
			bool flag = data.ContainsKey("angle");
			if (flag)
			{
				num = data["angle"]._float;
			}
			bool flag2 = data.ContainsKey("loop");
			if (flag2)
			{
				loop = data["loop"]._bool;
			}
			float y = this.g_mgr.getZ(@float, float2);
			IGREffectParticles iGREffectParticles = base.addEffect(str, (float)GameTools.inst.pixelToUnit((double)@float), y, (float)GameTools.inst.pixelToUnit((double)float2), false);
			bool flag3 = iGREffectParticles != null;
			if (flag3)
			{
				bool flag4 = num != 0f;
				if (flag4)
				{
					iGREffectParticles.rotY = (float)((double)(num * 180f) / 3.1415926535897931);
				}
				iGREffectParticles.loop = loop;
				iGREffectParticles.play();
			}
		}

		private void onAddFlyEff(GameEvent e)
		{
			Variant data = e.data;
			bool flag = !data.ContainsKey("fly_eff");
			if (!flag)
			{
				Vec2 vec = data["effPoint"]._val as Vec2;
				string str = data["fly_eff"]._str;
				float x = vec.x;
				float y = vec.y;
				bool loop = false;
				float num = 0f;
				bool flag2 = data.ContainsKey("angle");
				if (flag2)
				{
					num = data["angle"]._float;
				}
				bool flag3 = data.ContainsKey("loop");
				if (flag3)
				{
					loop = data["loop"]._bool;
				}
				float y2 = this.g_mgr.getZ(x, y);
				IGREffectParticles iGREffectParticles = base.addEffect(str, (float)GameTools.inst.pixelToUnit((double)x), y2, (float)GameTools.inst.pixelToUnit((double)y), false);
				bool flag4 = iGREffectParticles != null;
				if (flag4)
				{
					bool flag5 = num != 0f;
					if (flag5)
					{
						iGREffectParticles.rotY = (float)((double)(num * 180f) / 3.1415926535897931);
					}
					iGREffectParticles.loop = loop;
					iGREffectParticles.play();
				}
				Variant variant = data.clone();
				variant["fly_eff"]._val = iGREffectParticles;
				this.fly_array._arr.Add(variant);
			}
		}

		public override void updateProcess(float tmSlice)
		{
			bool flag = this.m_nScene_Loaded_CallNextFrame > 0;
			if (flag)
			{
				this.m_nScene_Loaded_CallNextFrame--;
				bool flag2 = this.m_nScene_Loaded_CallNextFrame == 0;
				if (flag2)
				{
					bool flag3 = debug.instance.async == null;
					if (flag3)
					{
						this.a3_scene_loaded();
					}
					else
					{
						this.m_nScene_Loaded_CallNextFrame = 1;
					}
				}
			}
			bool flag4 = GRMap.GAME_CAMERA == null;
			if (flag4)
			{
				GRMap.GAME_CAMERA = GameObject.Find("GAME_CAMERA");
				bool flag5 = GRMap.GAME_CAMERA == null;
				if (!flag5)
				{
					GRMap.GAME_CAM_CUR = GRMap.GAME_CAMERA.transform.GetChild(0).gameObject;
					GRMap.GAME_CAM_CAMERA = GRMap.GAME_CAM_CUR.GetComponent<Camera>();
					LGCamera.instance.updateMainPlayerPos(true);
					LGNpcs.instance.onMapchg();
				}
			}
		}
	}
}
