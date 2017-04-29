using Cross;
using DG.Tweening;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	public class login : LoadingUI
	{
		private Button bt;

		private Button btChoose;

		private Text txtServer;

		public static string m_QSMY_Update_url;

		private GameObject loadingserver;

		private string errorString = "";

		public ServerData curServerData;

		private Animation aniCam;

		public GameObject scene;

		private Image imageBg;

		private GameObject goUpdate;

		private Transform updateline;

		private Text updatTxt;

		private Text uidTxt;

		private GameObject noticePanel;

		private Text noticeTxt;

		private Text titleTxt;

		private GameObject btn_look;

		private Text waitTxt;

		public LoginMsgBox msg;

		public static login instance;

		public bool canEnter = false;

		private float toLineScale = 1f;

		private float curlineScale = 0f;

		private Color maskColor = new Color(1f, 1f, 1f, 1f);

		private int curState = 0;

		private int m_nSDK_ReloginCount = 0;

		private bool serverChanged = false;

		private Action _overhandle;

		private int lastIdx = -999;

		private void CheckUpdateVer()
		{
			this.changeState(0, 0);
		}

		public void onUpdateError(string str)
		{
			bool flag = this.errorString == "";
			if (flag)
			{
				this.errorString = str;
			}
		}

		public override void init()
		{
			base.getGameObjectByPath("login").SetActive(true);
			base.getGameObjectByPath("loading").SetActive(true);
			login.instance = this;
			this.txtServer = base.getComponentByPath<Text>("login/server/Text");
			this.bt = base.getComponentByPath<Button>("login/bt");
			this.btChoose = base.getComponentByPath<Button>("login/server");
			this.waitTxt = base.getComponentByPath<Text>("loading/txt_wait");
			this.waitTxt.gameObject.SetActive(false);
			this.imageBg = base.getComponentByPath<Image>("mask");
			this.imageBg.gameObject.SetActive(false);
			this.goUpdate = base.getGameObjectByPath("update");
			this.updateline = this.goUpdate.transform.FindChild("line");
			this.updatTxt = this.goUpdate.transform.FindChild("Text").GetComponent<Text>();
			this.uidTxt = base.getComponentByPath<Text>("login/uid/txt");
			this.noticePanel = base.getGameObjectByPath("notice");
			this.noticeTxt = this.noticePanel.transform.FindChild("str").GetComponent<Text>();
			this.titleTxt = this.noticePanel.transform.FindChild("title").GetComponent<Text>();
			this.btn_look = base.getGameObjectByPath("btn_look");
			this.btn_look.GetComponent<Button>().onClick.AddListener(new UnityAction(this.onOpenNotice));
			this.noticePanel.transform.FindChild("close").GetComponent<Button>().onClick.AddListener(new UnityAction(this.onCloseNotice));
			GameObject gameObjectByPath = base.getGameObjectByPath("msg");
			gameObjectByPath.SetActive(false);
			this.msg = new LoginMsgBox(gameObjectByPath.transform);
			this.bt.onClick.AddListener(new UnityAction(this.onEnterClick));
			this.btChoose.onClick.AddListener(new UnityAction(this.onChoose));
			this.loadingserver = base.getGameObjectByPath("loading_update");
			this.bt.interactable = (this.btChoose.interactable = false);
			this.goUpdate.SetActive(false);
			GlobleSetting.initSystemSetting();
			base.getGameObjectByPath("fg").SetActive(false);
			this.initScence();
			bool flag = Globle.QSMY_Platform_Index == ENUM_QSMY_PLATFORM.QSPF_LINKSDK;
			if (flag)
			{
				AndroidPlotformSDK.LOGIN_ACTION = new Action<Variant>(this.platform_Login);
				IOSPlatformSDK.LOGIN_ACTION = new Action<Variant>(this.platform_Login);
				AnyPlotformSDK.Call_Cmd("login", null, null, true);
			}
			else
			{
				this.changeState(-1, 0);
			}
		}

		private void initScence()
		{
			GameObject original = U3DAPI.U3DResLoad<GameObject>("ui_mesh/login/loginscene");
			this.scene = UnityEngine.Object.Instantiate<GameObject>(original);
			this.aniCam = this.scene.transform.FindChild("camera").GetComponent<Animation>();
			LightmapSettings.lightmaps = new LightmapData[]
			{
				new LightmapData
				{
					lightmapFar = Resources.Load("ui_mesh/login/scene/LightmapFar-0") as Texture2D
				}
			};
			InterfaceMgr.ui_Camera_cam.gameObject.SetActive(false);
		}

		private void platform_Login(Variant v_login)
		{
			Debug.Log("收到平台的登入数据 " + v_login.dump());
			bool flag = false;
			bool flag2 = v_login.ContainsKey("result");
			if (flag2)
			{
				Variant variant = v_login["data"];
				flag = true;
				bool flag3 = variant.ContainsKey("pid");
				if (flag3)
				{
					Globle.YR_srvlists__platform = variant["pid"]._str;
				}
				else
				{
					flag = false;
				}
				bool flag4 = variant.ContainsKey("uid");
				if (flag4)
				{
					Globle.YR_srvlists__platuid = variant["uid"]._str;
					this.uidTxt.text = variant["uid"]._str;
				}
				else
				{
					flag = false;
				}
				bool flag5 = variant.ContainsKey("avatar");
				if (flag5)
				{
					Globle.YR_srvlists__sign = variant["avatar"]._str;
				}
				else
				{
					flag = false;
				}
				bool flag6 = variant.ContainsKey("srvlists_url");
				if (flag6)
				{
					Globle.YR_srvlists__slurl = variant["srvlists_url"]._str;
				}
				else
				{
					flag = false;
				}
				bool flag7 = variant.ContainsKey("titles") && variant["titles"] != "";
				if (flag7)
				{
					string text = variant["content"];
					string text2 = variant["titles"];
					text = text.Replace('n', '\n');
					this.noticePanel.SetActive(true);
					this.btn_look.SetActive(true);
					this.noticeTxt.text = text;
					this.titleTxt.text = text2;
				}
			}
			Variant.free(v_login);
			v_login = null;
			bool flag8 = flag;
			if (flag8)
			{
				AndroidPlotformSDK.m_bLogined = true;
				IOSPlatformSDK.m_bLogined = true;
				this.changeState(-1, 0);
			}
			else
			{
				this.m_nSDK_ReloginCount = 30;
			}
		}

		private void Update()
		{
			bool flag = this.m_nSDK_ReloginCount > 0;
			if (flag)
			{
				this.m_nSDK_ReloginCount--;
				bool flag2 = this.m_nSDK_ReloginCount == 0;
				if (flag2)
				{
					AnyPlotformSDK.Call_Cmd("login", null, null, true);
				}
			}
			bool flag3 = this.goUpdate.active && this.curlineScale < 1f;
			if (flag3)
			{
				bool flag4 = this.curlineScale < this.toLineScale;
				if (flag4)
				{
					this.curlineScale += 0.01f;
					bool flag5 = this.curlineScale > 0.95f;
					if (flag5)
					{
						this.curlineScale = 1f;
					}
					this.updateline.localScale = new Vector3(this.curlineScale, 1f, 1f);
					bool flag6 = this.curlineScale == 1f;
					if (flag6)
					{
						bool flag7 = this.canEnter;
						if (flag7)
						{
							this.changeState(0, 0);
						}
						else
						{
							this.canEnter = true;
						}
					}
				}
			}
			bool flag8 = this.curState == 0;
			if (!flag8)
			{
				bool flag9 = this.curState == 1;
				if (flag9)
				{
					this.curState = 2;
				}
				else
				{
					bool flag10 = this.curState == 2;
					if (flag10)
					{
						this.aniCam.Play("cam_move");
						this.imageBg.gameObject.SetActive(true);
						this.maskColor.a = 0f;
						this.imageBg.color = this.maskColor;
						this.curState = 3;
					}
					else
					{
						bool flag11 = this.curState == 3;
						if (flag11)
						{
							float normalizedTime = this.aniCam["cam_move"].normalizedTime;
							bool flag12 = normalizedTime >= 1f;
							if (flag12)
							{
								bool flag13 = this._overhandle != null;
								if (flag13)
								{
									this._overhandle();
								}
								this._overhandle = null;
								this.curState = 4;
								this.maskColor.a = 1f;
								bool flag14 = this.scene != null;
								if (flag14)
								{
									UnityEngine.Object.Destroy(this.scene);
								}
								this.scene = null;
								bool flag15 = GRMap.instance != null;
								if (flag15)
								{
									GRMap.instance.refreshLightMap();
									(UIClient.instance.getLGUI("LGUIMainUIImpl") as LGIUIMainUI).show_all();
								}
							}
							else
							{
								this.maskColor.a = normalizedTime;
								this.imageBg.color = this.maskColor;
							}
						}
						else
						{
							bool flag16 = this.curState == 4;
							if (flag16)
							{
								bool flag17 = this.maskColor.a <= 0.1f;
								if (flag17)
								{
									this.curState = 5;
									InterfaceMgr.ui_Camera_cam.gameObject.SetActive(true);
									InterfaceMgr.ui_Camera_cam.gameObject.SetActive(true);
									InterfaceMgr.getInstance().destory(InterfaceMgr.SERVE_CHOOSE);
									InterfaceMgr.getInstance().destory(InterfaceMgr.LOGIN);
								}
								else
								{
									this.maskColor.a = this.maskColor.a - 0.05f;
									this.imageBg.color = this.maskColor;
								}
							}
						}
					}
				}
			}
		}

		public void showUILoading()
		{
			base.getGameObjectByPath("login").SetActive(false);
			base.getGameObjectByPath("loading").SetActive(true);
		}

		public void setServer(ServerData d)
		{
			this.serverChanged = true;
			this.curServerData = d;
			Globle.curServerD = d;
			this.txtServer.text = d.server_name;
			this.bt.interactable = true;
		}

		public void setServer(int sid)
		{
			bool flag = !Globle.dServer.ContainsKey(sid);
			if (!flag)
			{
				this.setServer(Globle.dServer[sid]);
			}
		}

		public void onBeginLoading(Action overhandle)
		{
			bool flag = this.curState != 0;
			if (!flag)
			{
				this._overhandle = overhandle;
				this.changeState(1, 0);
			}
		}

		public void setWaitingTxt(string txt)
		{
			this.waitTxt.text = txt;
			this.waitTxt.gameObject.SetActive(true);
		}

		public void changeState(int idx, int parm = 0)
		{
			bool flag = this.curState != 0;
			if (!flag)
			{
				this.lastIdx = idx;
				bool flag2 = idx == -1;
				if (flag2)
				{
					this.goUpdate.SetActive(false);
					base.getGameObjectByPath("login").SetActive(false);
					base.getGameObjectByPath("loading").SetActive(false);
					this.CheckUpdateVer();
				}
				else
				{
					bool flag3 = idx == 0;
					if (flag3)
					{
						LGPlatInfo.inst.loadServerList();
						bool active = this.goUpdate.active;
						if (active)
						{
							this.goUpdate.transform.DOKill(false);
							this.goUpdate.transform.DOScale(0f, 0.3f);
						}
						else
						{
							this.goUpdate.SetActive(false);
						}
					}
					else
					{
						bool flag4 = idx == 1;
						if (flag4)
						{
							base.getGameObjectByPath("login").SetActive(false);
							base.getGameObjectByPath("loading").SetActive(false);
							this.curState = 1;
						}
					}
				}
			}
		}

		private void onLoadSid(int sid)
		{
			bool flag = this.serverChanged;
			if (!flag)
			{
				this.setServer(sid);
			}
		}

		public void refresh()
		{
			bool active = this.goUpdate.active;
			if (active)
			{
				base.getGameObjectByPath("login").SetActive(true);
				base.getGameObjectByPath("login").transform.DOScale(0f, 0.3f).From<Tweener>();
			}
			else
			{
				base.getGameObjectByPath("login").SetActive(true);
			}
			this.btChoose.interactable = true;
			int num = 0;
			bool flag = PlayeLocalInfo.checkKey(PlayeLocalInfo.LOGIN_SERVER_SID);
			if (flag)
			{
				num = PlayeLocalInfo.loadInt(PlayeLocalInfo.LOGIN_SERVER_SID);
			}
			bool flag2 = num == 0;
			if (flag2)
			{
				bool flag3 = Globle.defServerId != 0;
				if (flag3)
				{
					num = Globle.defServerId;
				}
				else
				{
					bool flag4 = Globle.lServer.Count > 0;
					if (flag4)
					{
						num = Globle.lServer[0].sid;
					}
				}
			}
			this.setServer(num);
			this.serverChanged = false;
		}

		private void onEnterClick()
		{
			bool flag = this.curServerData == null;
			if (!flag)
			{
				this.bt.interactable = false;
				this.btn_look.SetActive(false);
				PlayeLocalInfo.saveInt(PlayeLocalInfo.LOGIN_SERVER_SID, this.curServerData.sid);
				LGPlatInfo.inst.sendLogin(this.curServerData.login_url);
			}
		}

		private void onCloseNotice()
		{
			this.noticePanel.SetActive(false);
		}

		private void onOpenNotice()
		{
			this.noticePanel.SetActive(true);
		}

		private void onChoose()
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.SERVE_CHOOSE, null, false);
		}

		public override void dispose()
		{
			bool flag = this.scene != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.scene);
			}
			this.scene = null;
			this.bt.onClick.RemoveAllListeners();
			this.btChoose.onClick.RemoveAllListeners();
			this.bt = null;
			this.btChoose = null;
			this.txtServer = null;
			this._overhandle = null;
			login.instance = null;
			base.dispose();
		}
	}
}
