using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class LGUIMainUIImpl_NEED_REMOVE : BaseLGUI, LGIUIMainUI
	{
		public delegate bool fun();

		public static LGUIMainUIImpl_NEED_REMOVE instance;

		private Dictionary<string, processStruct> m_process = new Dictionary<string, processStruct>();

		protected const uint UI_LOADING_COUNT = 20u;

		public const int NO_STATE = 0;

		public const int COLLECT = 1;

		public const int FINDWAY = 2;

		public const int AUTO_GAME = 3;

		public const int RECOVER = 4;

		protected int _CurAIState = -1;

		private bool _isFirstOpenMainUI = true;

		private float _turns_Interval = 1000f;

		private float _cur_truns_tm = 0f;

		protected Variant _pkgItems = null;

		protected Variant _skillArr = null;

		protected Variant _clientConf = null;

		protected bool _isFirstInitShortcutbtns = true;

		protected float _saveQuickbarTm = 0f;

		protected bool _needSave = false;

		private const uint CF_FAST_SKILL = 1u;

		private const uint CF_FUNCTION = 2u;

		private const uint CF_FUNCTBAR1 = 4u;

		private const uint CF_MAINCHAT = 8u;

		private const uint CF_OPTION = 16u;

		private uint _startOpenFlag = 15u;

		public BaseLGUI lgchat;

		private uint uiLoadedCnt = 0u;

		private bool _flag = true;

		private Variant _shakeArr = new Variant();

		private bool _isSetChainfo = false;

		private bool _isSethpmp = false;

		private bool _isSetfunbar1 = false;

		private Variant _vipData;

		private bool _isMovie = false;

		protected Vec2 _Pos = null;

		private int _curExp = 0;

		private int _maxExp = 0;

		private bool _hadExpPro = false;

		private bool _isfirstAct = true;

		private Variant _allCanvasArr;

		private Variant _needShowArr = new Variant();

		private Dictionary<string, Action> _showFun = new Dictionary<string, Action>();

		private Dictionary<string, Func<bool>> _checkFun = new Dictionary<string, Func<bool>>();

		private Variant _income_act_arr = new Variant();

		private Variant _old_act_arr = new Variant();

		private Variant _beforeOpenArr = new Variant();

		private Variant _tmpdata = new Variant();

		private bool _isinferfaceshow = true;

		private Variant _monthMsgData;

		private Variant _uplvlMsgData;

		private Variant _monthSvrCon;

		private Variant _retBndybArr;

		private Variant _noretBndybArr;

		protected uint _hasTodayReward = 0u;

		protected uint _hasOnlineReward = 0u;

		protected uint _hasUpLevelReward = 0u;

		protected float _oneDay = 86400f;

		protected bool _isFirstOpenFunctionbar1 = true;

		protected Variant _allFunCanvasArr = new Variant();

		protected Variant _allPkgFunCanvasArr = new Variant();

		protected Variant _needShowFunCanvasArr = new Variant();

		protected Variant _needShowPkgFunCanvasArr = new Variant();

		private bool _iconInit = false;

		private bool _hasIcon = false;

		protected int _Length = 50;

		private Variant _ignoreError;

		private bool bl = true;

		private Variant _clkCastskill;

		private bool _needrefresh = true;

		public static bool CHECK_MAPLOADING_LAYER = false;

		public static bool TRY_SHOW_MAP_OBJ = false;

		public static bool TO_LEVEL = false;

		private static float waitShowMapObj = 0f;

		private float recordTimer = 0f;

		public static bool FIRST_IN_GAME = true;

		private float refreshTime = 2000f;

		private string _mapNameImgPath;

		private bool _isChange = false;

		private int _curline = 1;

		private bool _isFirstSet = true;

		private Dictionary<string, LGAvatarNpc> _curNpcs;

		private bool _isFirstOpenUILoading = true;

		private int _loadUICount = 0;

		private Variant _loadUIIdArr = null;

		private bool _isFirstOpenUIDiscon = true;

		private Variant _disconDesc = null;

		protected Variant _selfStates = null;

		private int _curVip = 0;

		private int _vipstateDescNum = 6;

		private int long1 = 160;

		private bool _is_move = false;

		private float _v = 0.15f;

		private int _start_move_tm = 0;

		private Vec2 _start_pos = new Vec2();

		private IUIText _tf = null;

		protected Variant _attAnis = new Variant();

		protected List<Variant> _waitFlyItms = new List<Variant>();

		protected float _nextFlyTm = 0f;

		private Variant _curFallItemData;

		protected int _nextFallTm = 0;

		protected uint _curFallCnt = 0u;

		private Variant _goldAniArr = new Variant();

		private Vec2 _aniEndPos;

		private Vec2 _endFlyPos;

		private bool _hadNotPro = false;

		private int _expStartTm = 0;

		private List<IUIBaseControl> _expCtrlArr = new List<IUIBaseControl>();

		private float _frame_Y = 0f;

		private int _crossWidth;

		private int _crossHieght;

		private Variant _changeAttArr = new Variant();

		private Variant _showing_arr = new Variant();

		private Variant _attSprArr = new Variant();

		private float _last_show_tm = 0f;

		private float _show_interval = 300f;

		private float _one_total_tm = 2000f;

		private bool _isFirstOpenRecMisAwardPan = true;

		private int _curMlineMisID;

		private Variant _chapterAward;

		private int _showType = 0;

		private bool _curChapterMissionIsOver = false;

		private bool _isRealOpenMisAward = false;

		private int _awardChapter = -1;

		private int _lastChapterMisSchedule = -1;

		private int _curChapterMisSchedule = -1;

		private Variant _awardData;

		private bool _awdLineOver = false;

		private bool _curMlineOver = false;

		private Variant _carrInfo = new Variant();

		private bool _isGet = false;

		private List<damageShowStruct> _damageArr = new List<damageShowStruct>();

		private Variant _displayStyle = new Variant();

		private Variant _funsArr;

		private Variant _curShowArr;

		private bool _isfstOpen = true;

		private Variant _nextFunsArr;

		private Variant _nextShowArr;

		private bool _isNextFirstOpen = true;

		private bool _isfstOpenMCt = true;

		private IUIBaseControl _stopDrag = null;

		protected Variant _curSelectPackageData = null;

		protected Variant _showItems = null;

		protected uint _itemsCountMax = 0u;

		protected uint _itemsCountCur = 0u;

		protected uint _curPageIdx = 1u;

		protected uint _maxPageIdx = 5u;

		protected uint _curLableIdx = 0u;

		protected uint _curPageMaxNum = 36u;

		protected Variant _curPagePackageItems = null;

		protected int _curPageType = 0;

		protected Variant _lockArr = null;

		protected Variant _blockArr = null;

		private lgSelfPlayer lgMainPlayer
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).getObject("LG_MAIN_PLAY") as lgSelfPlayer;
			}
		}

		public bool isShowAll
		{
			get
			{
				return true;
			}
		}

		protected lgGDPlyFun lgGD_PlyFun
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_plyfunCT;
			}
		}

		public bool isDisconOpen
		{
			get
			{
				return false;
			}
		}

		public bool isRealOpenMisAward
		{
			set
			{
				this._isRealOpenMisAward = value;
				this._lastChapterMisSchedule = -1;
			}
		}

		public LGUIMainUIImpl_NEED_REMOVE(muUIClient m) : base(m)
		{
			LGUIMainUIImpl_NEED_REMOVE.instance = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGUIMainUIImpl_NEED_REMOVE(m as muUIClient);
		}

		public override void init()
		{
		}

		protected override void _regEventListener()
		{
		}

		protected override void _unRegEventListener()
		{
		}

		protected override void onOpen(Variant data)
		{
			base.onOpen(data);
		}

		private void openUIFin(string flag)
		{
		}

		public void set_clan_reqs(Variant reqs)
		{
		}

		public void add_clan_req(Variant data)
		{
		}

		public void onChatMsg(Variant data)
		{
		}

		public void add_world_mes(string msg)
		{
		}

		private void initBoard()
		{
			NewbieModel.getInstance();
			NewbieModel.getInstance().initNewbieData();
		}

		public void show_all()
		{
			bool isFirstOpenMainUI = this._isFirstOpenMainUI;
			if (isFirstOpenMainUI)
			{
				this._isFirstOpenMainUI = false;
				DelayDoManager.singleton.AddDelayDo(new Action(this.initBoard), 1, 0u);
				muLGClient.instance.addEventListenerCL("LG_JOIN_WORLD", 3035u, new Action<GameEvent>(this.onChangeMap));
				FunctionOpenMgr.init();
				InterfaceMgr.getInstance().initFirstUi();
				this.setInit();
				this._editionInit();
				this.isRealOpenMisAward = true;
				processStruct processStruct = processStruct.create(null, "", false, false);
				processStruct.update = new Action<float>(this.onProcess);
				this.m_process.Add("onProcess", processStruct);
				this.g_mgr.addProcess(processStruct);
			}
		}

		private void setUILoadedCount(string uiname)
		{
			this.uiLoadedCnt += 1u;
			maploading.instance.setPercent(this.uiLoadedCnt, 20f);
			maploading.instance.setTips("Loading ui " + uiname);
			bool flag = this.uiLoadedCnt >= 20u;
			if (flag)
			{
				this.uiLoadedCnt = 0u;
			}
		}

		private void _editionInit()
		{
		}

		public bool IsPlatActive(string key)
		{
			return false;
		}

		public void RegPlatNotActive(string key)
		{
		}

		public void RegPlatActive(string key)
		{
		}

		public void setSkillCount(int count)
		{
		}

		public void setCharVipInfo(bool isvip)
		{
		}

		public void VipChange(int vip)
		{
			bool flag = vip > 0;
			if (flag)
			{
				Vec2 vec = new Vec2((float)(CrossApp.singleton.width / 2 - 200), (float)(CrossApp.singleton.height / 2));
				IUIImageBox img = this.addScreenImg("viptxt", new Vec2(vec.x, vec.y + 80f));
				this.shwoScreenEff("firework", vec, new Action<IUIImageBox>(this.removeScreenImg), img);
			}
		}

		private IUIImageBox addScreenImg(string id, Vec2 pos)
		{
			return null;
		}

		private void removeScreenImg(IUIImageBox screenImg)
		{
		}

		public void showTipsByMis(int misid)
		{
			bool flag = this._flag;
			if (flag)
			{
				string misName = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.getMisName(misid);
				string text = text = "<txt text=\"" + misName + "\"color='0xfee569' size='14'/><br/>";
				string languageText = LanguagePack.getLanguageText("UI_Class_mission_track", "fin_mis_awd");
				text = text + languageText + "<br/>";
				Variant variant = (this.g_mgr.g_gameM as muLGClient).g_missionCT.get_accept_mis_info(misid);
				Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
				bool flag2 = variant.ContainsKey("configdata");
				if (flag2)
				{
					Variant variant2 = variant["configdata"];
					bool flag3 = variant2.ContainsKey("awards");
					if (flag3)
					{
						Variant variant3 = variant2["awards"];
						bool flag4 = variant3.ContainsKey("award");
						if (flag4)
						{
							foreach (Variant current in variant3["award"]._arr)
							{
								bool flag5 = current["carrid"] == 0;
								if (flag5)
								{
									bool flag6 = current.ContainsKey("exp");
									if (flag6)
									{
										int num = current["exp"];
										languageText = LanguagePack.getLanguageText("items_xml", "5202");
										text += "<img width='18' height='18'assettype='bmp' assetname='res_img_comimg_tips_01' assetblock='block_01'/>";
										text = string.Concat(new object[]
										{
											text,
											"<txt text=\"",
											languageText,
											"：",
											num,
											"\"/><br/>"
										});
									}
									bool flag7 = current.ContainsKey("gld");
									if (flag7)
									{
										int num2 = current["gld"];
										languageText = LanguagePack.getLanguageText("items_xml", "5201");
										text += "<img width='18' height='18'assettype='bmp' assetname='res_img_comimg_tips_01' assetblock='block_01'/>";
										text = string.Concat(new object[]
										{
											text,
											"<txt text=\"",
											languageText,
											"：",
											num2,
											"\"color='0xfee569' size='12'/><br/>"
										});
									}
								}
								bool flag8 = current["carrid"] == mainPlayerInfo["carr"];
								if (flag8)
								{
									bool flag9 = current.ContainsKey("itm");
									if (flag9)
									{
										Variant variant4 = current["itm"];
										foreach (Variant current2 in variant4._arr)
										{
											Variant variant5 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current2["id"]);
											languageText = LanguagePack.getLanguageText("items_xml", variant5["conf"]["id"]);
											text += "<img width='18' height='18'assettype='bmp' assetname='res_img_comimg_tips_01' assetblock='block_01'/>";
											text = string.Concat(new string[]
											{
												text,
												"<txt text=\"",
												languageText,
												"*",
												current2["cnt"],
												"\" size='12'/><br/>"
											});
										}
									}
									bool flag10 = current.ContainsKey("eqp");
									if (flag10)
									{
										Variant variant6 = current["eqp"];
										foreach (Variant current3 in variant6._arr)
										{
											Variant variant7 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current3["id"]);
											languageText = LanguagePack.getLanguageText("items_xml", variant7["conf"]["id"]);
											text += "<img width='18' height='18'assettype='bmp' assetname='res_img_comimg_tips_01' assetblock='block_01'/>";
											text = text + "<txt text=\"" + languageText + "\" size='12'/><br/>";
										}
									}
								}
							}
						}
					}
				}
				this._flag = !this._flag;
			}
		}

		private void shwoScreenEff(string id, Vec2 pos, Action<IUIImageBox> finFun, IUIImageBox img)
		{
		}

		protected void mini_mapCallBack(string type, bool data)
		{
		}

		public void AddScreenShake(string name)
		{
			Variant variant = GameTools.createGroup(new Variant[]
			{
				"name",
				name,
				"tmStr",
				GameTools.getTimer()
			});
			variant = this.formPlayData(variant);
			this._shakeArr._arr.Add(variant);
			bool flag = !this.m_process.ContainsKey("processShake");
			if (flag)
			{
				processStruct processStruct = processStruct.create(null, "", false, false);
				processStruct.update = new Action<float>(this.processShake);
				this.m_process.Add("processShake", processStruct);
			}
			this.g_mgr.addProcess(this.m_process["processShake"]);
		}

		private Variant formPlayData(Variant playData)
		{
			string id = (playData["name"] != null) ? playData["name"]._str : "";
			Variant shakeConf = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetShakeConf(id);
			bool flag = shakeConf != null && shakeConf["p"] != null;
			if (flag)
			{
				Variant variant = shakeConf["p"][0];
				foreach (string current in variant.Keys)
				{
					bool flag2 = !playData.ContainsKey(current);
					if (flag2)
					{
						string a = current;
						if (!(a == "ampy"))
						{
							if (!(a == "ampx"))
							{
								if (!(a == "times"))
								{
									if (!(a == "cycle"))
									{
										if (a == "tmPlay")
										{
											playData[current] = variant[current]._float;
										}
									}
									else
									{
										playData[current] = variant[current]._float;
									}
								}
								else
								{
									playData[current] = variant[current]._float;
								}
							}
							else
							{
								playData[current] = variant[current]._float;
							}
						}
						else
						{
							playData[current] = variant[current]._float;
						}
					}
				}
			}
			bool flag3 = playData["tmPlay"] == null;
			if (flag3)
			{
				playData["tmPlay"] = 1500;
			}
			return playData;
		}

		private void processShake(float tm1)
		{
			for (int i = this._shakeArr.Length - 1; i >= 0; i--)
			{
				Variant variant = this._shakeArr[i];
				float num = variant["tmStr"];
				float num2 = variant["ampx"];
				float num3 = variant["ampy"];
				float num4 = variant["cycle"];
				float num5 = variant["times"];
				float num6 = (float)this.g_mgr.g_netM.CurServerTimeStampMS;
				bool flag = num6 - num > num4 * num5;
				if (flag)
				{
					this._shakeArr._arr.RemoveAt(i);
				}
			}
			bool flag2 = this._shakeArr.Length <= 0;
			if (flag2)
			{
				this.g_mgr.removeProcess(this.m_process["processShake"]);
			}
		}

		private float ContrailStraightChange(float s, float tmPlay, float tmRun)
		{
			bool flag = tmRun > tmPlay;
			if (flag)
			{
				tmRun = tmPlay;
			}
			float num = 2f * s / tmPlay;
			float num2 = -num / tmPlay;
			return num * tmRun + num2 * tmRun * tmRun / 2f;
		}

		private float ContrailShake(int tm, int tmStr, int amp, int cycle, int times)
		{
			int num = amp - (int)this.ContrailStraightChange((float)amp, (float)(cycle * times), (float)((tm - tmStr) % (cycle * times)));
			return (float)((double)num * Math.Sin(6.2831853071795862 * (double)((tm - tmStr) % cycle) / (double)cycle));
		}

		private void _onOpenMlineAwardBack()
		{
		}

		private void _showDPBack(string str, int x, int y)
		{
		}

		private void _openTipsBack(string str, int x, int y)
		{
		}

		private void _closeTipsBack()
		{
		}

		private void setInit()
		{
		}

		private void onOpenPkPanel()
		{
		}

		public void ChangePKState(int state)
		{
		}

		public void PczoneChange(bool flag)
		{
		}

		private void _moveToBack(Variant moveData)
		{
		}

		private void _closeMsgBoxBack()
		{
		}

		private void _errBack(int err)
		{
		}

		private void _showRdmsgBox(string type)
		{
			bool flag = type == "open";
			if (!flag)
			{
				bool flag2 = type == "close";
				if (flag2)
				{
				}
			}
		}

		private void openFunBtns(Variant data)
		{
		}

		private void _clkCurBtnBack(int channleType, int x, int y)
		{
			this.openFunBtns(GameTools.createGroup(new Variant[]
			{
				"type",
				"chatList",
				"x",
				x,
				"y",
				y - 120,
				"width",
				50
			}));
		}

		private void _executeLinkEventBack(string linkStr, int x, int y)
		{
			Variant variant = GameTools.split(linkStr, "_", 1u);
			string a = variant[0];
			bool flag = a == "clkWorldName";
			if (flag)
			{
				string val = variant[1];
				uint val2 = variant[2];
				Variant variant2 = GameTools.createGroup(new Variant[]
				{
					"name",
					val,
					"cid",
					val2
				});
				this.openFunBtns(GameTools.createGroup(new Variant[]
				{
					"type",
					"clkWorldName",
					"x",
					x,
					"y",
					y - 133,
					"width",
					70,
					"data",
					variant2
				}));
			}
		}

		public void chatSetOnClkIdx(int idx)
		{
		}

		public bool CheckIsSend(int type)
		{
			return false;
		}

		public void RemoveStateShow(int id, string name)
		{
			bool flag = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.IsNeedStateDisTip(id);
			if (flag)
			{
				string text = LanguagePack.getLanguageText("UITileBuff", "stateDisStr");
				text = string.Format(text, UIUtility.singleton.GetConfNomalStr(name));
				this.systemmsg(text, 4u);
			}
		}

		protected void _applyBack(string type)
		{
			UILinksManager.singleton.UILinkEvent(type);
		}

		protected void _openBroadcastBack()
		{
		}

		private void _openChatfaceBack(int x, int y, int type = 0)
		{
		}

		public void AddChatFace(string faceName)
		{
		}

		public void sentChatMsg(uint chatType, string dataStr, uint cid = 0u, bool withtid = false, uint backshowtp = 0u)
		{
			(this.g_mgr.g_gameM as muLGClient).lgGD_Chat.chat_msg(chatType, dataStr, cid, withtid, backshowtp);
		}

		private void _enterChatBack(Variant data)
		{
			uint chatType = data["chatType"];
			string dataStr = data["dataStr"];
			uint cid = data["cid"];
			bool withtid = data["withtid"];
			uint backshowtp = data["backshowtp"];
			this.sentChatMsg(chatType, dataStr, cid, withtid, backshowtp);
		}

		private void _getCidBack(string name)
		{
			(this.g_mgr.g_gameM as muLGClient).g_MgrPlayerInfoCT.get_player_cid_by_name(name, false, new Action<string, uint>(this.getCidFin));
		}

		private void _buyItemBack(uint tpid, int cnt)
		{
		}

		public void getCidFin(string nm, uint cid)
		{
		}

		private void setDetaiInfo()
		{
			bool flag = !this._isSetChainfo || !this._isSethpmp || !this._isSetfunbar1;
			if (!flag)
			{
				Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
				this.setChaInfo(mainPlayerInfo);
				this._getVipInfo(0);
			}
		}

		public void setChaInfo(Variant data)
		{
		}

		private void setlvl(Variant data)
		{
		}

		protected void _getVipInfo(int lvl = 0)
		{
			(this.g_mgr.g_gameM as muLGClient).g_vipCT.getVipData(0u, lvl, new Action<Variant>(this._onGetVipData));
		}

		protected void _onGetVipData(Variant vipData)
		{
			this._vipData = vipData;
		}

		public Variant getVipData()
		{
			return this._vipData;
		}

		private void setHead(int carr, int sex)
		{
		}

		private void _chainfoCallBack(string msg, Variant data)
		{
			bool flag = "onStrong" == msg;
			if (!flag)
			{
				bool flag2 = "outSceenBtn" == msg;
				if (!flag2)
				{
					bool flag3 = "clkHead" == msg;
					if (!flag3)
					{
						bool flag4 = "overFull" == msg;
						if (flag4)
						{
							string str = LanguagePack.getLanguageText("LGUIMainUIImpl", "overFull");
							str = UIUtility.singleton.TransToHtmlText(str);
						}
						else
						{
							bool flag5 = "overRecove" == msg;
							if (flag5)
							{
								string str2 = LanguagePack.getLanguageText("LGUIMainUIImpl", "overRecove");
								str2 = UIUtility.singleton.TransToHtmlText(str2);
							}
							else
							{
								bool flag6 = "clkFull" == msg;
								if (!flag6)
								{
									bool flag7 = "clkRecove" == msg;
									if (!flag7)
									{
										bool flag8 = "clkMovie" == msg;
										if (flag8)
										{
											this._isMovie = !this._isMovie;
											this.changeMovie();
										}
										else
										{
											bool flag9 = "clkFanPage" == msg;
											if (flag9)
											{
												this.clkFanPage();
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void _autoRtnCallBack(string msg, Variant data = null)
		{
			string languageText = LanguagePack.getLanguageText("autoHpMp", "auto");
			if (!(msg == "hp"))
			{
				if (!(msg == "mp"))
				{
					if (!(msg == "init"))
					{
						if (!(msg == "over"))
						{
							if (!(msg == "move"))
							{
								if (!(msg == "out"))
								{
								}
							}
							else
							{
								string text = UIUtility.singleton.TransToHtmlText(string.Format(languageText, data["s"], data["value"]._int));
							}
						}
						else
						{
							string text = UIUtility.singleton.TransToHtmlText(string.Format(languageText, data["s"], data["value"]._int));
						}
					}
				}
			}
		}

		public void SetAutoValue(string tp, float value)
		{
		}

		private void clkFanPage()
		{
			string str = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("fanPageUrl")._str;
			UILinksManager.singleton.UILinkEvent(str);
		}

		public bool IsMovie()
		{
			return this._isMovie;
		}

		private void changeMovie()
		{
		}

		public void updateFullBtn(bool isfull)
		{
		}

		protected void initSelfPos()
		{
			this._Pos = new Vec2((this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["x"], (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["y"]);
		}

		public void AutoGame()
		{
			bool in_level = (this.g_mgr.g_gameM as muLGClient).g_levelsCT.in_level;
			if (in_level)
			{
				int current_lvl = (this.g_mgr.g_gameM as muLGClient).g_levelsCT.current_lvl;
				int current_diff = (int)(this.g_mgr.g_gameM as muLGClient).g_levelsCT.current_diff;
				Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.get_lvl_node(current_lvl, current_diff);
			}
		}

		public void setCurLevel(Variant data)
		{
		}

		public void setCurExp(uint exp, int val = 0)
		{
		}

		public void AddAchieveFly(string name, string img, Vec2 stPos)
		{
			Vec2 vec = this.get_pos_byid(name);
		}

		public void setCurYb(uint value)
		{
		}

		public void setCurBndyb(uint value)
		{
		}

		public void setCurGold(uint value)
		{
		}

		public void sethp(int cur, uint max)
		{
		}

		public void setmp(int cur, uint max)
		{
		}

		public void setProtect(uint cur, uint max)
		{
		}

		public void setActivities(Variant data)
		{
		}

		private void onOpenBack()
		{
		}

		private void CheckShow()
		{
		}

		private bool checkMobilegift()
		{
			return false;
		}

		private bool checkMicroload()
		{
			return false;
		}

		private bool checkHfActivity()
		{
			return false;
		}

		private bool checkBfActivity()
		{
			return false;
		}

		private bool checkOtherBfActivity()
		{
			return false;
		}

		private void showHfActivity()
		{
		}

		private void showBfActivity()
		{
		}

		private void showOtherBfActivity()
		{
		}

		private bool checkTransfer()
		{
			return this.lgGD_PlyFun.CanShowIcon();
		}

		private void onShowActivity()
		{
		}

		private void showLuckyDraw()
		{
		}

		private void onAchievement()
		{
			int canAwdNum = (this.g_mgr.g_gameM as muLGClient).g_achieveCT.GetCanAwdNum();
			bool flag = canAwdNum > 0;
			if (flag)
			{
			}
		}

		private void onMicroload()
		{
		}

		private void onChargeBack()
		{
		}

		private void onLottery()
		{
		}

		private void onWeekGoal()
		{
		}

		private void onGrowthFund()
		{
			this.lgGD_PlyFun.GetMonthInvestInfo();
			this.lgGD_PlyFun.GetUplvlInvestInfo();
		}

		private void onExpDouble()
		{
		}

		private void onEvtBack(string type, Variant data)
		{
		}

		private Variant getDataByName(string name)
		{
			Variant result;
			foreach (Variant current in this._income_act_arr._arr)
			{
				bool flag = current["data"]["name"] == name;
				if (flag)
				{
					result = current["data"];
					return result;
				}
			}
			result = null;
			return result;
		}

		private void onProcessAct(float tm)
		{
		}

		private bool _is_all_include()
		{
			int num = 0;
			for (int i = 0; i < this._income_act_arr.Length; i++)
			{
				for (int j = 0; j < this._old_act_arr.Length; j++)
				{
					Variant variant = this._old_act_arr[j];
					Variant variant2 = this._income_act_arr[i];
					bool flag = variant["data"]["name"] == variant2["data"]["name"] && variant["start_tm"]._float == variant2["start_tm"]._float;
					if (flag)
					{
						num++;
					}
				}
			}
			return num == this._old_act_arr.Length;
		}

		private void set_current_act_info()
		{
		}

		public void OpenInterface(string typename, bool isopen = true)
		{
		}

		public void openSystem(string typename, bool isopen)
		{
		}

		private void sortMainIcon()
		{
		}

		public void showBoomAni(string name)
		{
		}

		public void StopMainIconEff(string name)
		{
		}

		protected void _showActs()
		{
		}

		private int checkBeforeOpenSet(string name)
		{
			int result;
			foreach (Variant current in this._beforeOpenArr._arr)
			{
				bool flag = current["typename"] == name;
				if (flag)
				{
					result = ((current["flag"] != null) ? 3 : 2);
					return result;
				}
			}
			result = 1;
			return result;
		}

		public void NorOptionShow(bool isshow)
		{
		}

		public Vec2 get_pos_byid(string name_id)
		{
			return this._get_showbtn_pos(name_id);
		}

		public void interfaceShow(bool flag)
		{
		}

		public bool isInterfaceShow()
		{
			return this._isinferfaceshow;
		}

		private Vec2 _get_showbtn_pos(string name_id)
		{
			return null;
		}

		public IUIBaseControl GetActUIById(string id)
		{
			return null;
		}

		public void SetIconCount(string id, string num)
		{
		}

		public void setOnlineRewardCountdownTextAndColor(string str)
		{
		}

		public void setOnlineRewardVisible(bool b)
		{
		}

		public void SetGrowthFundRewardAvailable(string str)
		{
		}

		public void SetExpDoubleTime(string str)
		{
		}

		public void SetCrossWarTime(string str)
		{
		}

		public void RefreshMonthRewardInvest(Variant data)
		{
			bool flag = this.lgGD_PlyFun.IsMonthInvesting();
			if (flag)
			{
				this._monthMsgData = data;
			}
			this._setFundRewardInfo();
		}

		public void RefreshUplvlRewardInvest(Variant data)
		{
			bool flag = this.lgGD_PlyFun.IsUplvlInvesting();
			if (flag)
			{
				this._uplvlMsgData = data;
			}
			this._setFundRewardInfo();
		}

		protected void _setFundRewardInfo()
		{
			bool flag = this._monthMsgData == null || this._uplvlMsgData == null;
			if (!flag)
			{
				this._monthSvrCon = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.GetMonthInvest();
				int num = (int)((float)((this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L - this._monthMsgData["start_time"]) / this._oneDay + 1f);
				bool flag2 = this._monthSvrCon != null && num >= this._monthSvrCon[0]["retdays"];
				if (flag2)
				{
					bool flag3 = num > this._monthSvrCon[0]["retdays"] && num < this._monthSvrCon[0]["retdays"] + 7 && this._monthMsgData["addret_isget"] == 0;
					if (flag3)
					{
						this._hasOnlineReward = 1u;
					}
					else
					{
						this._hasOnlineReward = 0u;
					}
				}
				else
				{
					bool flag4 = num > this._monthMsgData["retcnt"];
					if (flag4)
					{
						this._hasTodayReward = 1u;
					}
					else
					{
						this._hasTodayReward = 0u;
					}
				}
				this._retBndybArr = this._uplvlMsgData["retbndyb"];
				this._sortArr(this._retBndybArr);
				Variant variant = this._uplvlMsgData["noretbndyb"];
				this._sortArr(variant);
				this._noretBndybArr = new Variant();
				for (int i = 0; i < variant.Length; i++)
				{
					bool flag5 = this._retBndybArr[i]["bndyb"] > 0 && this._retBndybArr[i]["level"] == variant[i]["level"];
					if (flag5)
					{
						bool flag6 = variant[i]["bndyb"] > 0;
						if (flag6)
						{
							int val = variant[i]["bndyb"];
							this._noretBndybArr._arr.Add(GameTools.createGroup(new Variant[]
							{
								"bndyb",
								val,
								"level",
								variant[i]["level"],
								"isAdd",
								true
							}));
						}
					}
					else
					{
						this._noretBndybArr._arr.Add(variant[i]);
					}
				}
				int @int = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.m_data["level"]._int;
				bool flag7 = this._noretBndybArr.Length > 0;
				if (flag7)
				{
					bool flag8 = this._noretBndybArr[0]["level"] > @int;
					if (flag8)
					{
						this._hasUpLevelReward = 0u;
					}
					else
					{
						this._hasUpLevelReward = 1u;
					}
				}
				else
				{
					this._hasUpLevelReward = 0u;
				}
				this.SetGrowthFundRewardAvailable(((int)(this._hasTodayReward + this._hasOnlineReward + this._hasUpLevelReward)).ToString());
			}
		}

		protected void _sortArr(Variant arr)
		{
			Variant variant = new Variant();
			for (int i = 0; i < arr.Length - 1; i++)
			{
				bool flag = true;
				for (int j = 0; j < arr.Length - 1 - i; j++)
				{
					bool flag2 = arr[j]["level"] > arr[j + 1]["level"];
					if (flag2)
					{
						Variant value = arr[j];
						arr[j] = arr[j + 1];
						arr[j + 1] = value;
						flag = false;
					}
				}
				bool flag3 = flag;
				if (flag3)
				{
					break;
				}
			}
		}

		public void OnMissionRes()
		{
			bool flag = this._iconInit && !this._hasIcon;
			if (flag)
			{
			}
			this._showFunbar1();
		}

		protected void functionbar1EvtCB(string msg, Variant data)
		{
		}

		protected void _showFunbar1()
		{
		}

		public Vec2 getOptionPos(string name)
		{
			return null;
		}

		private bool needIgnoreError(int err)
		{
			bool flag = this._ignoreError == null;
			if (flag)
			{
				string str = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("ignoreErr")._str;
				this._ignoreError = GameTools.split(str, ",", 1u);
			}
			bool result;
			using (List<Variant>.Enumerator enumerator = this._ignoreError._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num = enumerator.Current;
					bool flag2 = num == Math.Abs(err);
					if (flag2)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public void output_server_err(int errcode)
		{
			bool flag = this.needIgnoreError(errcode);
			if (!flag)
			{
				string languageText = LanguagePack.getLanguageText("ServerErrorCode", errcode.ToString());
				bool flag2 = languageText == errcode.ToString();
				bool flag3 = !flag2;
				if (flag3)
				{
					this.systemmsg(languageText, 1024u);
				}
			}
		}

		public void output_client_err(int errcode)
		{
			string languageText = LanguagePack.getLanguageText("clientError", errcode.ToString());
			this.systemmsg(languageText, 8u);
		}

		public void systemmsg(Variant msgs, uint type = 4u)
		{
		}

		public void pkg_set_items(Variant items)
		{
			this._pkgItems = items;
			bool flag = this.isLoadShortcutData();
			if (flag)
			{
				this.setShortcutInfo();
			}
		}

		public void pkg_add_items(Variant items, int flag = 0)
		{
		}

		public void pkg_rmv_items(Variant item)
		{
		}

		public void pkg_mod_item_data(uint item_id, Variant item, int flag = 0)
		{
			bool flag2 = flag > 1;
			if (flag2)
			{
				this.AddFlyItm(item, null);
			}
		}

		private void rbmsgShowTxt_1(Variant msgs)
		{
		}

		public void ShowMapMsg(int mapid)
		{
			Variant mapLimit = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetMapLimit(mapid);
			bool flag = mapLimit != null;
			if (flag)
			{
				string text = LanguagePack.getLanguageText("mission_manager", "maplimit_" + mapid);
				text = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.TransToSuperText(text);
				this.ShowMsgBoxStr(GameTools.createGroup(new Variant[]
				{
					"text",
					text,
					"type",
					"superText"
				}));
			}
		}

		public void ShowMsgBoxStr(Variant data)
		{
		}

		public void CloseMsgBox()
		{
		}

		public void setChatFocus()
		{
		}

		public void setWisperName(string name)
		{
		}

		public void show_defense(Variant data)
		{
		}

		public void SaveQuickbar()
		{
		}

		public string GetShortcutTipBySkid(uint skid)
		{
			return "";
		}

		public void CastSlotByKey(uint key, bool click = false)
		{
		}

		public void ChangeFastSkill(Variant data)
		{
		}

		private void useSkill(uint skid, uint sklvl)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrSkillConf.get_skill_conf(skid);
			LGAvatarGameInst nearMon = (this.g_mgr.g_gameM as muLGClient).g_monstersCT.getNearMon(1000);
			bool flag = skid > 0u;
			if (flag)
			{
				uint @uint = variant["aff"]._uint;
				uint uint2 = variant["tar_tp"]._uint;
				bool flag2 = uint2 == 0u;
				if (flag2)
				{
					bool flag3 = nearMon == null && (@uint & 1u) == 1u;
					if (flag3)
					{
						this.lgMainPlayer.attack(this.lgMainPlayer, false, skid);
					}
					else
					{
						this.lgMainPlayer.attack(nearMon, false, skid);
					}
				}
				else
				{
					bool flag4 = nearMon == null && uint2 == 1u && (@uint & 1u) == 1u;
					if (flag4)
					{
						this.lgMainPlayer.attack(this.lgMainPlayer, false, skid);
					}
					else
					{
						this.lgMainPlayer.attack(nearMon, false, skid);
					}
				}
			}
			else
			{
				Variant carrDefSkill = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCarrDefSkill(this.lgMainPlayer.data["carr"]._int);
				bool flag5 = carrDefSkill != null;
				if (flag5)
				{
					float num = (float)this.g_mgr.g_netM.CurServerTimeStampMS;
					bool flag6 = this._cur_truns_tm + this._turns_Interval < num;
					if (flag6)
					{
						this.lgMainPlayer.nextSkillTurn = 0;
					}
					skid = carrDefSkill[this.lgMainPlayer.nextSkillTurn]._uint;
					this._cur_truns_tm = num;
				}
				this.lgMainPlayer.attack(nearMon, false, skid);
			}
		}

		private void attackNearTar()
		{
			LGAvatarMonster lGAvatarMonster = this.lgMainPlayer.getSelectTarget();
			bool flag = lGAvatarMonster != null;
			if (!flag)
			{
				lGAvatarMonster = (this.g_mgr.g_gameM as muLGClient).g_monstersCT.getNearMon(1000);
				bool flag2 = lGAvatarMonster != null;
				if (flag2)
				{
					this.lgMainPlayer.stop();
					this.lgMainPlayer.onSelectMonster(lGAvatarMonster);
				}
			}
		}

		private void openShortcutbtnsCallBack()
		{
		}

		private void reqSkillInfo()
		{
			this._clientConf = (this.g_mgr.g_gameM as muLGClient).g_generalCT.getClientConf();
		}

		protected void setShortcutInfo()
		{
		}

		protected bool isLoadShortcutData()
		{
			return this._skillArr != null && this._pkgItems != null && this._clientConf != null;
		}

		private void onProcessShortcut()
		{
		}

		public void ColdDown(Variant data, Dictionary<uint, Variant> cds)
		{
		}

		public void ItemColdDown(Dictionary<uint, Variant> cds)
		{
		}

		public void ShowSkills(bool show)
		{
		}

		public void refreshSkillList(Variant skillList)
		{
			bool flag = this._skillArr == null;
			if (flag)
			{
				this._skillArr = new Variant();
			}
			bool flag2 = skillList != null && skillList.Count > 0;
			if (flag2)
			{
				foreach (Variant current in skillList.Values)
				{
					this._skillArr.pushBack(current);
				}
			}
			bool flag3 = this.isLoadShortcutData();
			if (flag3)
			{
				this.setShortcutInfo();
			}
		}

		public void addNewSkill(Variant skill)
		{
		}

		public void setClientConfig(Variant clientConf)
		{
			this._clientConf = clientConf;
			bool flag = this.isLoadShortcutData();
			if (flag)
			{
				this.setShortcutInfo();
			}
		}

		public void OpenSkillset(Variant data)
		{
		}

		public void CloseSkillset()
		{
		}

		private void onOpenSkillset()
		{
		}

		private void skillsetEvtCB(string msg, Variant data)
		{
		}

		public void onProcess(float tmSlice)
		{
			bool flag = this.recordTimer > 0f;
			if (flag)
			{
				bool flag2 = Time.time > this.recordTimer;
				if (flag2)
				{
					this.recordTimer = 0f;
				}
			}
			bool flag3 = !this.isShowAll;
			if (flag3)
			{
				GameTools.PrintNotice(" !isShowAll ");
			}
			else
			{
				bool tRY_SHOW_MAP_OBJ = LGUIMainUIImpl_NEED_REMOVE.TRY_SHOW_MAP_OBJ;
				if (tRY_SHOW_MAP_OBJ)
				{
					bool tO_LEVEL = LGUIMainUIImpl_NEED_REMOVE.TO_LEVEL;
					if (tO_LEVEL)
					{
						LGUIMainUIImpl_NEED_REMOVE.TRY_SHOW_MAP_OBJ = false;
					}
					else
					{
						LGUIMainUIImpl_NEED_REMOVE.waitShowMapObj += 1f;
						bool flag4 = LGUIMainUIImpl_NEED_REMOVE.waitShowMapObj < 5f;
						if (flag4)
						{
							return;
						}
						LGUIMainUIImpl_NEED_REMOVE.TRY_SHOW_MAP_OBJ = false;
						LGUIMainUIImpl_NEED_REMOVE.waitShowMapObj = 0f;
						BaseProxy<MapProxy>.getInstance().sendShowMapObj();
					}
				}
				bool flag5 = LGUIMainUIImpl_NEED_REMOVE.CHECK_MAPLOADING_LAYER && LoaderBehavior.ms_HasAllLoaded;
				if (flag5)
				{
					LGUIMainUIImpl_NEED_REMOVE.CHECK_MAPLOADING_LAYER = false;
					bool fIRST_IN_GAME = LGUIMainUIImpl_NEED_REMOVE.FIRST_IN_GAME;
					if (fIRST_IN_GAME)
					{
						LGUIMainUIImpl_NEED_REMOVE.FIRST_IN_GAME = false;
						bool flag6 = login.instance;
						if (!flag6)
						{
							bool flag7 = maploading.instance != null;
							if (flag7)
							{
								maploading.instance.closeLoadWait(0.2f);
							}
							bool flag8 = a3_mapname.instance;
							if (flag8)
							{
								a3_mapname.instance.refreshInfo();
							}
						}
						this.recordTimer = Time.time + 300f;
					}
					bool flag9 = GRMap.HasNoWaitPlotSound();
					if (flag9)
					{
						GRMap.ClearWaitPlotSound();
						bool flag10 = loading_cloud.instance == null;
						if (flag10)
						{
							gameST.REQ_PLAY_PLOT();
						}
						else
						{
							loading_cloud.instance.hide(gameST.REQ_PLAY_PLOT);
						}
					}
				}
			}
		}

		protected void chainOpen(bool flag)
		{
			if (flag)
			{
				processStruct processStruct = processStruct.create(null, "", false, false);
				processStruct.update = new Action<float>(this.serverTimePro);
				this.m_process.Add("serverTimePro", processStruct);
			}
			this._isSetChainfo = true;
			this.setDetaiInfo();
			this.setAccount();
		}

		public void setAccount()
		{
		}

		protected void onClkAccount()
		{
		}

		protected void serverTimePro(float tm)
		{
		}

		private string str_add(string str)
		{
			bool flag = str.Length == 1;
			if (flag)
			{
				str = "0" + str;
			}
			return str;
		}

		public void DoGMCommand(Variant cmd)
		{
		}

		public Variant GetGmCommand(string msg)
		{
			bool flag = msg.Length < 3;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string a = msg.Substring(0, 1);
				bool flag2 = a != "\\";
				if (flag2)
				{
					result = null;
				}
				else
				{
					string text = msg.Substring(1, msg.Length - 1);
					int num = text.IndexOf(" ");
					string val = text.Substring(0, num);
					string val2 = text.Substring(num + 1);
					result = GameTools.createGroup(new Variant[]
					{
						"cmd",
						val,
						"param",
						val2
					});
				}
			}
			return result;
		}

		public bool IsGM()
		{
			Variant data = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.m_data;
			bool flag = data.ContainsKey("gm") && data["gm"] != null;
			return flag && data["gm"]._bool;
		}

		public void Respawn(LGAvatarBase c)
		{
		}

		public void StateChange(Variant states)
		{
		}

		public void BlessChange(Variant arr)
		{
		}

		public void updateMove(LGAvatarBase c)
		{
		}

		public void updatePath(Variant pathArr)
		{
		}

		public void removePath()
		{
		}

		public void ChangeCharacterData(LGAvatarBase c, bool team = false)
		{
		}

		public void stopMove()
		{
		}

		public void updateNpcMisState(LGAvatarBase npc)
		{
		}

		public void addCharacter(LGAvatarBase c)
		{
		}

		public void removeCharacter(LGAvatarBase c)
		{
		}

		protected void onChangeMap(GameEvent e)
		{
		}

		public void changeMap(Variant map)
		{
		}

		public void mapNameCB()
		{
		}

		private void _getMapNamePathById(int mapid)
		{
			string text = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.get_mapname_icon((uint)mapid);
			bool flag = this._mapNameImgPath == text;
			if (!flag)
			{
				this._mapNameImgPath = text;
				bool flag2 = this._mapNameImgPath == "";
				if (!flag2)
				{
					this._isChange = true;
				}
			}
		}

		public void SetCurLine(Variant data)
		{
		}

		private void showMapLine()
		{
			bool isFirstSet = this._isFirstSet;
			if (isFirstSet)
			{
				this._isFirstSet = false;
				Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
				this.changeMap(mainPlayerInfo);
			}
			else
			{
				this.SetCurLine(GameTools.createGroup(new Variant[]
				{
					"line",
					(this.g_mgr.g_gameM as muLGClient).g_worldlineCT.curline
				}));
			}
		}

		protected void _setMapNpcs(int mpid)
		{
		}

		protected void _clearMapNpcs()
		{
		}

		protected void _setMapLink()
		{
		}

		protected void _setWorldBoss(uint mpid)
		{
		}

		protected void _goToMapBack(Variant data)
		{
			UIUtility.singleton.goto_map(data["mapid"], data["tx"], data["ty"], true, null);
		}

		protected void _clkTileBack(Variant data)
		{
		}

		protected void _recAutoBack(int mapid)
		{
			int id = 20000 + mapid;
			ClientGeneralConf localGeneral = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral;
			Variant randPosConfById = localGeneral.getRandPosConfById(id);
			Variant arr = randPosConfById["pos"];
			Variant ranPos = this.getRanPos(arr);
		}

		private Variant getRanPos(Variant arr)
		{
			int length = arr.Length;
			System.Random random = new System.Random();
			int idx = (int)Math.Floor((double)(random.Next(0, 1) * length));
			return arr[idx];
		}

		public void OpenUIloading(string str)
		{
			bool flag = this._loadUIIdArr == null;
			if (flag)
			{
				this._loadUIIdArr = new Variant();
			}
			this._loadUIIdArr._arr.Add(str);
			this._loadUICount++;
			bool isFirstOpenUILoading = this._isFirstOpenUILoading;
			if (isFirstOpenUILoading)
			{
				this._isFirstOpenUILoading = false;
			}
		}

		public void CloseUIloading(string str)
		{
			bool flag = false;
			int num = 0;
			foreach (Variant current in this._loadUIIdArr._arr)
			{
				bool flag2 = current == str;
				if (flag2)
				{
					this._loadUICount--;
					flag = true;
					this._loadUIIdArr._arr.RemoveAt(num);
				}
				num++;
			}
			bool flag3 = this._loadUICount == 0 & flag;
			if (flag3)
			{
			}
		}

		public void OnConLost()
		{
		}

		private void _setDisconDesc()
		{
			bool flag = this._disconDesc == null;
			if (flag)
			{
				this._disconDesc._arr.Add(LanguagePack.getLanguageText("disconnect", "disconnect_0"));
				this._disconDesc._arr.Add(LanguagePack.getLanguageText("disconnect", "disconnect_1"));
				this._disconDesc._arr.Add(LanguagePack.getLanguageText("disconnect", "disconnect_2"));
				this._disconDesc._arr.Add(LanguagePack.getLanguageText("disconnect", "disconnect_3"));
				this._disconDesc._arr.Add(LanguagePack.getLanguageText("disconnect", "disconnect_4"));
				this._disconDesc._arr.Add(LanguagePack.getLanguageText("disconnect", "disconnect_5"));
				this._disconDesc._arr.Add(LanguagePack.getLanguageText("disconnect", "disconnect_6"));
			}
		}

		private Variant _getDisconDesc()
		{
			Variant variant = new Variant();
			string val = "";
			for (int i = 0; i < this._disconDesc.Length; i++)
			{
				variant._arr.Add(val);
			}
			return variant;
		}

		private void _openDisconCB(Variant tp)
		{
		}

		private void _disconCB()
		{
			this.CloseDisConnect();
		}

		public void CloseDisConnect()
		{
		}

		public void SetCrossWarEnterCountDown(string str)
		{
		}

		private void _setSelfStates(Variant states)
		{
		}

		protected void _setVipState(bool isvip)
		{
			Variant value = new Variant();
			Variant data = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.m_data;
			bool flag = this._selfStates != null && this._selfStates.Length > 0;
			if (flag)
			{
				bool flag2 = !isvip;
				if (flag2)
				{
					bool flag3 = "vipState" == this._selfStates[0]["id"];
					if (flag3)
					{
						this._selfStates._arr.RemoveAt(0);
					}
				}
				else
				{
					bool flag4 = "vipState" != this._selfStates[0]["id"];
					if (flag4)
					{
						value = this._getVipState();
						this._selfStates[0] = value;
					}
					else
					{
						int num = data["vip"];
						bool flag5 = num > this._curVip;
						if (flag5)
						{
							this._curVip = num;
							value = this._getVipState();
							this._selfStates[0] = value;
						}
					}
				}
			}
			else
			{
				this._selfStates = new Variant();
				if (isvip)
				{
					this._curVip = data["vip"];
					value = this._getVipState();
					this._selfStates[0] = value;
				}
			}
		}

		private Variant _getVipState()
		{
			string val = "vipState";
			string languageText = LanguagePack.getLanguageText("vipstate", "vipstate_name");
			Variant variant = new Variant();
			Variant data = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.m_data;
			Variant vipStateData = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.getVipStateData(data["vip"]);
			for (int i = 0; i < this._vipstateDescNum; i++)
			{
				string languageText2 = LanguagePack.getLanguageText("vipstate", "vipstate" + data["vip"] + "_" + i.ToString());
				variant._arr.Add(languageText2);
			}
			return GameTools.createGroup(new Variant[]
			{
				"id",
				val,
				"vipDesc",
				variant,
				"icon",
				(vipStateData != null) ? vipStateData["icon"]._str : "",
				"name",
				languageText
			});
		}

		private bool _isVip()
		{
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			return mainPlayerInfo["vip"] > 0;
		}

		public void AIStop()
		{
		}

		public void setAIState(uint state)
		{
		}

		public void Restart()
		{
		}

		public int GetCurAIState()
		{
			return this._CurAIState;
		}

		private void set_tf_text(string mes)
		{
		}

		public bool InAutoGame()
		{
			return this._CurAIState == 2 || this._CurAIState == 3;
		}

		public void BeginStopAIPrompt()
		{
			string text = null;
			bool flag = this._CurAIState == 2;
			if (flag)
			{
				text = LanguagePack.getLanguageText("stop_move", "stop_findway");
			}
			else
			{
				bool flag2 = this._CurAIState == 3;
				if (flag2)
				{
					text = LanguagePack.getLanguageText("stop_move", "stop_auto_game");
				}
			}
			bool flag3 = text != null;
			if (flag3)
			{
				this.set_tf_text(text);
				this.open_move();
			}
		}

		public void EndStopAIPrompt()
		{
			bool is_move = this._is_move;
			if (is_move)
			{
				this._is_move = false;
				bool flag = this._tf != null;
				if (flag)
				{
					this._tf.visible = false;
				}
			}
		}

		private void open_move()
		{
			this._is_move = true;
			this._start_move_tm = (int)GameTools.getTimer();
		}

		private void move()
		{
			bool flag = this._is_move && this._tf != null;
			if (flag)
			{
				float num = (float)(GameTools.getTimer() - (long)this._start_move_tm);
				float num2 = num * this._v;
				this._tf.y = this._start_pos.y - num2;
				bool flag2 = num2 >= (float)this.long1;
				if (flag2)
				{
					this.EndStopAIPrompt();
				}
			}
		}

		public void setNextOlAward(Variant data)
		{
			bool flag = data != null;
			if (flag)
			{
				Variant onlinePrize = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.GetOnlinePrize(data["aid"]);
				bool flag2 = onlinePrize != null;
				if (flag2)
				{
					foreach (Variant current in onlinePrize["group"]._arr)
					{
						bool flag3 = current["gid"] == data["gid"];
						if (flag3)
						{
							break;
						}
					}
				}
			}
		}

		public void AddTextMoveAni(string id, string txt, Vec2 b, Vec2 e, uint tm)
		{
		}

		public void AddImgMoveAni(string id, string img, Vec2 b, Vec2 e, uint tm, Action fun)
		{
			bool flag = this._attAnis.ContainsKey(id);
			if (flag)
			{
			}
		}

		private void onAttAniEnd(DispAttAni ani)
		{
		}

		public void AddFlyItm(Variant itms, Variant data = null)
		{
			Vec2 vec = null;
			Vec2 vec2 = null;
			Vec2 vec3 = new Vec2((float)CrossApp.singleton.width, (float)CrossApp.singleton.height);
			bool flag = data != null;
			if (flag)
			{
				vec2 = (data["begin"]._val as Vec2);
				vec = (data["end"]._val as Vec2);
			}
			bool flag2 = vec2 == null;
			if (flag2)
			{
				vec2 = new Vec2(vec3.x / 2f, vec3.y / 2f - 130f);
			}
			bool flag3 = vec == null;
			if (flag3)
			{
				vec = new Vec2(vec2.x - 24f, vec2.y - 24f);
			}
			ClientItemsConf localItems = (this.g_mgr.g_gameConfM as muCLientConfig).localItems;
			foreach (Variant current in itms._arr)
			{
				Variant item = new Variant();
				string text = localItems.get_item_icon_url(current["tpid"]);
				item = GameTools.createGroup(new object[]
				{
					"img",
					text,
					"b",
					vec2,
					"e",
					vec,
					"aniEnd",
					this._aniEndPos
				});
				this._waitFlyItms.Add(item);
			}
			this._aniEndPos = null;
		}

		protected void onProcessItmFly()
		{
			float num = (float)this.g_mgr.g_netM.CurServerTimeStampMS;
			bool flag = this._waitFlyItms.Count > 0 && this._nextFlyTm <= num;
			if (flag)
			{
				this._nextFlyTm = num + 400f;
				this.playItmFlyAni(this._waitFlyItms[0]);
				this._waitFlyItms.RemoveAt(0);
			}
		}

		protected void playItmFlyAni(Variant data)
		{
		}

		public void FallItem(Variant data)
		{
			bool flag = data;
			if (flag)
			{
				this._curFallItemData = data;
				processStruct processStruct = processStruct.create(null, "", false, false);
				processStruct.update = new Action<float>(this.onProcessGoldFall);
				this.m_process.Add("onProcessGoldFall", processStruct);
			}
		}

		protected void onProcessGoldFall(float tm)
		{
			int num = (int)GameTools.getTimer();
			bool flag = this._nextFallTm <= num;
			if (flag)
			{
				this._nextFallTm = num + this._curFallItemData["space"];
				this.playItmFallDown();
			}
		}

		private void playItmFallDown()
		{
			string text = "itmfall" + GameTools.getTimer().ToString();
			System.Random random = new System.Random();
			float x = (float)(CrossApp.singleton.width * random.Next(0, 1));
			Vec2 vec = new Vec2(x, 0f);
			Vec2 vec2 = new Vec2(x, (float)CrossApp.singleton.height);
			float num = (float)(this._curFallItemData["scalemax"] * random.Next(0, 1));
		}

		private void itmFlyStep2(DispAttAni attAni)
		{
			IUIBaseControl baseControl = attAni.baseControl;
			baseControl.alpha = 1f;
			attAni.RemoveAttAni("scaleX");
			attAni.RemoveAttAni("scaleY");
			attAni.RemoveAttAni("alpha");
			Vec2 vec = attAni.userdata["e"]._val as Vec2;
			Vec2 packageBtnPos = this.GetPackageBtnPos();
			attAni.AddAttAni(new Func<Variant, double, double>(Algorithm.TweenLine), "x", vec.x, packageBtnPos.x, 1300f, 1);
			attAni.AddAttAni(new Func<Variant, double, double>(Algorithm.TweenQuadCircular), "y", vec.y, packageBtnPos.y, 1300f, 1);
			attAni.userdata["e"]._val = packageBtnPos;
			attAni.SetFinishFun(new Action<DispAttAni>(this.itmFlyStep3));
			attAni.Play();
		}

		private void itmFlyStep3(DispAttAni attAni)
		{
			Vec2 vec = attAni.userdata["e"]._val as Vec2;
			Vec2 e = new Vec2(vec.x - 24f, vec.y - 24f);
			attAni.AddMoveAni(new Func<Variant, double, double>(Algorithm.TweenLine), vec, e, 400f);
			attAni.AddSizeAni(new Func<Variant, double, double>(Algorithm.TweenLine), 1f, 2f, 400f, 1);
			attAni.AddAttAni(new Func<Variant, double, double>(Algorithm.TweenLine), "alpha", 1f, 0f, 400f, 1);
			attAni.SetFinishFun(new Action<DispAttAni>(this.onAttAniEnd));
			attAni.Play();
		}

		public Vec2 GetPackageBtnPos()
		{
			return null;
		}

		public void SetFlyItmEndPos(Vec2 e)
		{
			this._aniEndPos = e;
		}

		public void NobptChange(int val)
		{
			int @int = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("nobptfly")._int;
			bool flag = val > @int;
			if (flag)
			{
				bool flag2 = !this._hadNotPro;
				if (flag2)
				{
					this._hadNotPro = true;
					processStruct processStruct = processStruct.create(null, "", false, false);
					processStruct.update = new Action<float>(this.nobptProcess);
					this.m_process.Add("nobptProcess", processStruct);
				}
				this.addNobptFly();
			}
		}

		private void addNobptFly()
		{
		}

		private void playNobptFly()
		{
		}

		private void nobptProcess(float tm)
		{
		}

		private void addExpFly()
		{
			Variant variant = null;
			bool flag = variant == null;
			if (!flag)
			{
				int num = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["x"];
				int num2 = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["y"];
			}
		}

		private void playExpFly()
		{
		}

		private void expProcess(float tm)
		{
			bool flag = this._expCtrlArr.Count > 0;
			if (flag)
			{
				int num = (int)GameTools.getTimer();
				bool flag2 = this._expCtrlArr.Count > 10 || num - this._expStartTm > 5000;
				if (flag2)
				{
				}
			}
		}

		private void freeExpCtrl()
		{
			foreach (IUIBaseControl current in this._expCtrlArr)
			{
			}
			this._expCtrlArr.Clear();
			this._expStartTm = 0;
		}

		public void clkLink(string str)
		{
			bool flag = str == "";
			if (!flag)
			{
				Variant variant = GameTools.split(str, "_", 1u);
				string text = variant[0];
				if (!(str == "worldBoss"))
				{
					if (!(str == "warinfo"))
					{
						if (!(str == "smithy"))
						{
						}
					}
				}
				string str2 = variant[0]._str;
				if (!(str2 == "openui"))
				{
					if (!(str2 == "npc"))
					{
						if (!(str2 == "transpos"))
						{
							if (!(str2 == "bLink"))
							{
								if (!(str2 == "gaming"))
								{
									if (str2 == "item")
									{
										this.openTips(variant);
									}
								}
								else
								{
									this.openGaming(variant);
								}
							}
							else
							{
								this.bLinkOpen(variant);
							}
						}
						else
						{
							this.gotoTranspos(variant);
						}
					}
					else
					{
						this.transNpc(variant);
					}
				}
				else
				{
					this.openui(variant);
				}
			}
		}

		private void openui(Variant re)
		{
		}

		private void transNpc(Variant re)
		{
			uint num = re[1];
			uint val = (this.g_mgr.g_gameConfM as muCLientConfig).svrMapsConf.get_npc_map_id(num);
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMapsConf.get_npc_info(num);
			bool flag = variant == null;
			if (flag)
			{
				num = this._get_real_npc_trans_id(num);
				variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrMapsConf.get_npc_info(num);
			}
			bool flag2 = variant == null;
			if (!flag2)
			{
				UIUtility.singleton.TrantoNPC(variant["id"], (uint)(variant["x"] + 2), (uint)(variant["y"] + 2), true, GameTools.createGroup(new Variant[]
				{
					"map",
					val,
					"npc",
					num
				}));
			}
		}

		private void gotoTranspos(Variant re)
		{
			bool in_level = (this.g_mgr.g_gameM as muLGClient).g_levelsCT.in_level;
			if (in_level)
			{
				string languageText = LanguagePack.getLanguageText("showmessage", "untrans");
				this.systemmsg(languageText, 8u);
			}
			else
			{
				uint mid = re[1];
				uint tx = re[2];
				uint ty = re[3];
				UIUtility.singleton.goto_map(mid, tx, ty, true, null);
			}
		}

		private void bLinkOpen(Variant re)
		{
			string str = re[1]._str;
			if (str == "openui")
			{
				string val = re[2];
				string val2 = re[3];
				string val3 = re[4];
				this.openui(new Variant
				{
					_arr = 
					{
						"openui",
						val,
						val2,
						val3
					}
				});
			}
		}

		private void openTips(Variant re)
		{
			uint id = re[1];
			uint num = re[2];
			uint cid = re[3];
			Variant variant = (this.g_mgr.g_gameM as muLGClient).g_itemsCT.get_show_item_data((int)id);
			bool flag = variant == null;
			if (flag)
			{
				(this.g_mgr.g_gameM as muLGClient).g_itemsCT.GetShowItem(cid, id);
			}
			else
			{
				Variant data = variant["data"];
				this.showItemTips(data);
			}
		}

		public void showItemTips(Variant data)
		{
		}

		private void openGaming(Variant re)
		{
		}

		private uint _get_real_npc_trans_id(uint npc_id)
		{
			uint result = 0u;
			uint num = (this.g_mgr.g_gameConfM as muCLientConfig).svrMapsConf.get_npc_map_id(npc_id);
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrNpcConf.get_npc_data((int)npc_id);
			bool flag = num > 0u;
			if (flag)
			{
				bool flag2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrLevelConf.is_level_map(num) && variant.ContainsKey("level");
				if (flag2)
				{
					int lvlid = variant["level"][0]["id"];
					result = (uint)(this.g_mgr.g_gameConfM as muCLientConfig).svrNpcConf.getLevelEntryNpc(lvlid);
				}
				else
				{
					result = npc_id;
				}
			}
			return result;
		}

		public void CarrlvlChange(int carrlvl, bool isup)
		{
			if (isup)
			{
				Vec2 vec = new Vec2((float)(CrossApp.singleton.width / 2 - 200), (float)(CrossApp.singleton.height / 2));
				IUIImageBox img = this.addScreenImg("transtxt", new Vec2(vec.x, vec.y + 80f));
				this.shwoScreenEff("firework", vec, new Action<IUIImageBox>(this.removeScreenImg), img);
			}
		}

		public void AddScreenEff(IUIBaseControl ctrl)
		{
		}

		public void RemoveScreenEff(IUIBaseControl ctrl)
		{
		}

		private void set_pos_flag(int val)
		{
			switch (val)
			{
			}
		}

		public void SendWorldMsg(string str, string type = "")
		{
		}

		private void sizeChange()
		{
			this._isSethpmp = true;
			this.setDetaiInfo();
			this.refresh_ui_Pos(true);
		}

		public void refresh_ui_Pos(bool sizechange = true)
		{
		}

		public void setFcmShow(bool isShow)
		{
		}

		protected void _openFcmPanel()
		{
		}

		public void chatAppendInputText(string str)
		{
		}

		public void ShowLevelUpAni()
		{
		}

		public void AddAttShow(string att, int value)
		{
			int num = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.IsAttChangeShow(att);
			bool flag = num >= 0;
			if (flag)
			{
				string languageText = LanguagePack.getLanguageText("att_mod_name", att);
				bool flag2 = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.IsThousand(att);
				bool flag3 = "atkcdtm" == att;
				if (flag3)
				{
					value = -value;
				}
				bool flag4 = flag2;
				string text;
				if (flag4)
				{
					text = value / 10 + "%";
				}
				else
				{
					text = value.ToString();
				}
				bool flag5 = value > 0;
				if (flag5)
				{
					text = "+" + text;
				}
				string text2 = languageText + " " + text;
				bool flag6 = num == 1;
				if (!flag6)
				{
					bool flag7 = value < 0;
					if (flag7)
					{
					}
				}
			}
		}

		public void AddAttShows(Variant arr)
		{
			this._changeAttArr = arr;
			foreach (Variant current in arr._arr)
			{
				this.AddAttShow(current["key"], current["val"]);
			}
		}

		public Variant GetChangeAtt()
		{
			return this._changeAttArr;
		}

		private void attProcess(float tm)
		{
			for (int i = this._damageArr.Count - 1; i >= 0; i--)
			{
				this._damageArr.RemoveAt(i);
			}
			bool flag = this._attSprArr.Length > 0;
			if (flag)
			{
				int num = (int)GameTools.getTimer();
				int length = this._attSprArr.Length;
				float num2 = (float)length * this._show_interval;
				bool flag2 = num2 > this._one_total_tm;
				if (flag2)
				{
					int num3 = 0;
					for (int j = 0; j < this._attSprArr.Length; j++)
					{
						int num4 = (int)(this._one_total_tm / (float)length) * num3;
						this._attSprArr._arr.RemoveAt(j);
						num3++;
						j--;
					}
				}
				else
				{
					int num4 = (int)this._show_interval;
					bool flag3 = length > 0;
					if (flag3)
					{
						bool flag4 = (float)num - this._last_show_tm > this._show_interval;
						if (flag4)
						{
							this._attSprArr._arr.RemoveAt(0);
							this._last_show_tm = (float)num;
						}
					}
				}
			}
			bool flag5 = this._showing_arr.Length > 0;
			if (flag5)
			{
			}
			bool flag6 = this._attSprArr.Length == 0 && this._damageArr.Count == 0 && this._showing_arr.Length == 0;
			if (flag6)
			{
				this.m_process.Remove("attProcess");
			}
		}

		private void setVis(ShowStruct ast, int wat_tm)
		{
			int num = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["x"];
			int num2 = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["y"];
		}

		public void MisAwdOver()
		{
			this._awdLineOver = true;
		}

		public void RefreshRecMisAwardPan()
		{
			bool awdLineOver = this._awdLineOver;
			if (awdLineOver)
			{
				bool isOpen = base.isOpen;
				if (isOpen)
				{
				}
			}
			else
			{
				this.initMisAwardPanShow();
				bool flag = this._curChapterMissionIsOver && !this._awardData;
				if (!flag)
				{
					this.RealOpenRecMisAwardPan();
				}
			}
		}

		public void AddMlineawd()
		{
			this.CloseRecMisAwardPan();
			this.RefreshRecMisAwardPan();
			Vec2 vec = new Vec2((float)(CrossApp.singleton.width / 2 - 200), (float)(CrossApp.singleton.height / 2));
			IUIImageBox img = this.addScreenImg("mainlinetxt", new Vec2(vec.x, vec.y + 80f));
			this.shwoScreenEff("firework", vec, new Action<IUIImageBox>(this.removeScreenImg), img);
		}

		private void RealOpenRecMisAwardPan()
		{
			bool flag = this._isRealOpenMisAward && this._showType == 1;
			if (flag)
			{
				bool isOpen = base.isOpen;
				if (isOpen)
				{
					this.setMisAwardPanInfo(true);
				}
				else
				{
					this.OpenRecMisAwardPan();
				}
			}
			else
			{
				bool isOpen2 = base.isOpen;
				if (isOpen2)
				{
					this.CloseRecMisAwardPan();
				}
			}
		}

		private void OpenRecMisAwardPan()
		{
		}

		public void CloseRecMisAwardPan()
		{
			this._curChapterMisSchedule = -1;
		}

		public void ReplayMisAwardEff()
		{
		}

		protected void initMisAwardPanShow()
		{
			this._showType = 2;
			this._curMlineMisID = (this.g_mgr.g_gameM as muLGClient).g_missionCT.GetMainMisId();
			int mlineawd = (this.g_mgr.g_gameM as muLGClient).g_missionCT.GetMlineawd();
			bool flag = mlineawd > 0;
			if (flag)
			{
				int awardChapter = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.GetAwardChapter(mlineawd);
				bool flag2 = this._awardChapter != awardChapter;
				if (flag2)
				{
					this._awardChapter = awardChapter;
					this._lastChapterMisSchedule = -1;
				}
			}
			int num = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["level"];
			bool flag3 = this._chapterAward != null;
			if (flag3)
			{
				bool flag4 = this._curMlineMisID > this._chapterAward["awdmis"] || (num > 0 && num > this._chapterAward["level"]);
				if (flag4)
				{
					this.setMisAwardPanInfo(false);
					this.CloseRecMisAwardPan();
					this._isGet = true;
				}
			}
			bool flag5 = mlineawd == 0;
			if (!flag5)
			{
				Variant curMlineAwd = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.GetCurMlineAwd(mlineawd);
				bool flag6 = curMlineAwd != null;
				if (flag6)
				{
					bool flag7 = this._curMlineMisID >= curMlineAwd["misid"] || (num > 0 && num >= curMlineAwd["level"]);
					if (flag7)
					{
						this._chapterAward = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetMlineAwardInfoByMisid(curMlineAwd["misid"], curMlineAwd["level"], (uint)mlineawd);
					}
					else
					{
						this._chapterAward = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetMlineAwardInfoByMisid(this._curMlineMisID, num, (uint)mlineawd);
					}
				}
				else
				{
					this._chapterAward = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetMlineAwardInfoByMisid(this._curMlineMisID, num, (uint)mlineawd);
				}
				bool flag8 = this._chapterAward == null;
				if (!flag8)
				{
					int carr = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["carr"];
					this._awardData = (this.g_mgr.g_gameConfM as muCLientConfig).svrMisConf.GetMlineawdByItm(this._chapterAward["awdmis"], carr);
					int length = this._chapterAward["misid"].Length;
					bool flag9 = (this.g_mgr.g_gameM as muLGClient).g_missionCT.is_mis_has_complete(this._curMlineMisID);
					bool flag10 = (this.g_mgr.g_gameM as muLGClient).g_missionCT.is_accepted_mis(this._curMlineMisID);
					for (int i = 0; i < this._chapterAward["misid"].Length; i++)
					{
						bool flag11 = (num > 0 && num >= this._chapterAward["level"]) || (flag9 && this._curMlineMisID == this._chapterAward["misid"][length - 1]);
						if (flag11)
						{
							this._curChapterMisSchedule = i + 1;
							this._curMlineOver = true;
							break;
						}
						bool flag12 = (num > 0 && num >= this._chapterAward["level"]) || this._curMlineMisID > this._chapterAward["misid"][length - 1];
						if (flag12)
						{
							this._curMlineOver = true;
						}
						else
						{
							this._curMlineOver = false;
						}
						bool flag13 = flag10 && this._curMlineMisID == this._chapterAward["misid"][0];
						if (flag13)
						{
							this._curChapterMisSchedule = 0;
							break;
						}
						bool flag14 = this._chapterAward["misid"][i] < this._curMlineMisID;
						if (flag14)
						{
							this._curChapterMisSchedule = i + 1;
						}
					}
					bool flag15 = this._curChapterMisSchedule != this._chapterAward["misid"].Length;
					if (flag15)
					{
						this._curChapterMissionIsOver = false;
					}
					else
					{
						this._curChapterMissionIsOver = true;
					}
					bool flag16 = this._lastChapterMisSchedule != this._curChapterMisSchedule;
					if (flag16)
					{
						this._lastChapterMisSchedule = this._curChapterMisSchedule;
						bool flag17 = this._curChapterMisSchedule >= 0;
						if (flag17)
						{
							this._showType = 1;
						}
					}
					bool flag18 = num > 0 && num >= this._chapterAward["startlvl"];
					if (flag18)
					{
						bool flag19 = this._curChapterMisSchedule < 0;
						if (flag19)
						{
							this._curChapterMisSchedule = 0;
						}
						this._showType = 1;
					}
				}
			}
		}

		protected void _openMisAwardPanInfoCB()
		{
		}

		protected void setMisAwardPanInfo(bool fly = false)
		{
		}

		private void addMisAwardFly()
		{
			int num = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["x"];
			int num2 = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["y"];
			string text = "misawardfly" + GameTools.getTimer().ToString();
		}

		public Variant getItemdata()
		{
			Variant variant = new Variant();
			bool flag = this._awardData["itm"] != null;
			if (flag)
			{
				variant["tpid"] = this._awardData["itm"][0]["id"];
			}
			else
			{
				bool flag2 = this._awardData["eqp"] != null;
				if (flag2)
				{
					variant["tpid"] = this._awardData["eqp"][0]["id"];
				}
			}
			variant["isuse"] = this._chapterAward["isuse"];
			return variant;
		}

		private void _openMsgBoxAward()
		{
			bool flag = this._awardData != null;
			if (flag)
			{
				bool isGet = this._isGet;
				if (isGet)
				{
					this._isGet = false;
				}
				else
				{
					bool flag2 = this._curChapterMisSchedule == this._chapterAward["misid"].Length;
					bool flag3 = flag2;
					if (flag3)
					{
						(this.g_mgr.g_gameM as muLGClient).lgGD_Award.GetMlineMisPrize(this._awardData["misid"]);
					}
				}
			}
		}

		private void _receiveJudge()
		{
		}

		protected void OnMouseMove(Cross.Event evtPar)
		{
		}

		protected void OnMouseOver(Cross.Event evtPar)
		{
		}

		protected void OnMouseOut(Cross.Event evtPar)
		{
		}

		protected void showTips(Cross.Event evtObj)
		{
			bool flag = this._awardData;
			if (flag)
			{
				int tpid = 0;
				bool flag2 = this._awardData["itm"] != null;
				if (flag2)
				{
					tpid = this._awardData["itm"][0]["id"];
				}
				else
				{
					bool flag3 = this._awardData["eqp"];
					if (flag3)
					{
						tpid = this._awardData["eqp"][0]["id"];
					}
				}
				Variant val = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf((uint)tpid);
				bool flag4 = val;
				if (flag4)
				{
					string str = LanguagePack.getLanguageText("mlineAwardTips", this._carrInfo["tips"]);
					str = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.TransToSuperText(str);
				}
			}
			else
			{
				string str = LanguagePack.getLanguageText("mlineAwardTips", this._carrInfo["tips"]);
				str = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.TransToSuperText(str);
			}
		}

		protected void closeTips()
		{
		}

		protected void _misAwardPanCallBack(string msg)
		{
			if (msg == "receiveAward")
			{
				this._receiveJudge();
			}
		}

		public void addTeamMateToMap(Variant teammate = null)
		{
			bool flag = teammate == null;
			if (flag)
			{
			}
			bool flag2 = teammate != null;
			if (flag2)
			{
				foreach (Variant current in teammate._arr)
				{
					bool flag3 = !current || current["cid"] == (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo["cid"];
					if (flag3)
					{
					}
				}
			}
		}

		public void SetHideOtherFilter(bool b)
		{
		}

		private void _delayCloseBubble()
		{
		}

		protected void chatCallBack(string type, Variant data)
		{
		}

		public Vec2 GetChatRadioPos(int idx)
		{
			return null;
		}

		public void ChangeShowType(int tp, bool isAdd)
		{
		}

		public void ShowClanEvent(string text, string fun = "", string arg1 = "", int tp = 0)
		{
		}

		public void SendClanMsg(string text)
		{
		}

		public Variant GetFace(string content, string format)
		{
			return null;
		}

		public void ResetlvlBack()
		{
			Vec2 vec = new Vec2((float)(CrossApp.singleton.width / 2 - 200), (float)(CrossApp.singleton.height / 2));
			IUIImageBox img = this.addScreenImg("tranlive", new Vec2(vec.x, vec.y + 80f));
			this.shwoScreenEff("firework", vec, new Action<IUIImageBox>(this.removeScreenImg), img);
		}

		protected void createDynamicAni(Variant info)
		{
		}

		private Variant GetDisplayStyle(string id)
		{
			return this._displayStyle[id];
		}

		protected void _adjustShowInfo(Variant showConf, Variant showInfo)
		{
			bool flag = showConf.ContainsKey("sptm");
			if (flag)
			{
				showInfo["sptm"] = showConf["sptm"];
			}
			bool flag2 = showConf.ContainsKey("res");
			if (flag2)
			{
				showInfo["res"] = showConf["res"];
			}
			bool flag3 = showConf.ContainsKey("imageNum");
			if (flag3)
			{
				showInfo["imageNum"] = showConf["imageNum"];
			}
			bool flag4 = showConf.ContainsKey("style");
			if (flag4)
			{
				showInfo["style"] = this.GetDisplayStyle(showConf["style"]);
			}
			bool flag5 = showConf.ContainsKey("tp");
			if (flag5)
			{
				showInfo["tp"] = showConf["tp"];
			}
			bool flag6 = showInfo.ContainsKey("text");
			if (flag6)
			{
				Variant variant = new Variant();
				bool flag7 = showConf.ContainsKey("fmt");
				if (flag7)
				{
					variant = GameTools.mergeSimpleObject(showConf["fmt"][0], variant, false, true);
				}
				bool flag8 = showInfo.ContainsKey("fmt");
				if (flag8)
				{
					variant = GameTools.mergeSimpleObject(showInfo["fmt"][0], variant, false, true);
				}
				showInfo["fmt"] = variant;
			}
			bool flag9 = showConf.ContainsKey("g9");
			if (flag9)
			{
				showInfo["g9"] = showConf["g9"][0];
			}
			bool flag10 = showConf.ContainsKey("imgprop");
			if (flag10)
			{
				showInfo["imgprop"] = showConf["imgprop"][0];
			}
			bool flag11 = showConf.ContainsKey("textprop");
			if (flag11)
			{
				showInfo["textprop"] = showConf["textprop"][0];
			}
			bool flag12 = showConf.ContainsKey("tm");
			if (flag12)
			{
				showInfo["tm"] = showConf["tm"];
			}
			bool flag13 = showConf.ContainsKey("uv");
			if (flag13)
			{
				showInfo["uv"] = showConf["uv"][0];
			}
			bool flag14 = showConf.ContainsKey("playtm");
			if (flag14)
			{
				showInfo["playtm"] = showConf["playtm"];
			}
			bool flag15 = showConf.ContainsKey("width");
			if (flag15)
			{
				showInfo["width"] = showConf["width"];
			}
			bool flag16 = showConf.ContainsKey("height");
			if (flag16)
			{
				showInfo["height"] = showConf["height"];
			}
		}

		public void RideLevelUp()
		{
			Vec2 pos = new Vec2((float)(CrossApp.singleton.width / 2 - 200), (float)(CrossApp.singleton.height / 2));
			this.shwoScreenEff("rideLevelUp", pos, null, null);
		}

		public void RideQualUp()
		{
			Vec2 pos = new Vec2((float)(CrossApp.singleton.width / 2 - 200), (float)(CrossApp.singleton.height / 2));
			this.shwoScreenEff("rideQualUp", pos, null, null);
		}

		public void ChangeOptionState(bool needopen)
		{
		}

		public void AddFunOpen(Variant data)
		{
		}

		public Vec2 getOptionEndPos(string oid)
		{
			return null;
		}

		protected void initFuncPanelBtns()
		{
		}

		private void optionPanelCallback(string msg, Variant data)
		{
			if (!(msg == "clk"))
			{
				if (msg == "_onOpen")
				{
					bool isfstOpen = this._isfstOpen;
					if (isfstOpen)
					{
						this._isfstOpen = false;
						this.initFuncPanelBtns();
						this.openUIFin("open_function");
					}
				}
			}
			else
			{
				UILinksManager.singleton.OpenUI(GameTools.createGroup(new Variant[]
				{
					"par",
					data._str,
					"type",
					"main"
				}));
				this.StopMainIconEff(data._str);
			}
		}

		public void OpenNextPanel()
		{
		}

		public void AddNextFunOpen(Variant data)
		{
		}

		public Vec2 getNextEndPos(string oid)
		{
			return null;
		}

		protected void initNextPanelBtns()
		{
		}

		private void nextPanelCallback(string msg, Variant data = null)
		{
		}

		private void mainChatEventCallBack(string msg)
		{
		}

		public void MainChatHide(bool flag)
		{
		}

		private void chatMsgShow(string str)
		{
		}

		public void CombptChange()
		{
		}

		public void EnterMultilvl()
		{
		}

		public void LeaveMultilvl()
		{
		}

		public void initStopDrag()
		{
		}

		public void StopUIDrag()
		{
			bool flag = this._stopDrag == null;
			if (flag)
			{
				this.initStopDrag();
			}
			this._stopDrag.startDrag(false, null);
			this._stopDrag.stopDrag();
		}

		public void onSlide(int flag)
		{
		}

		protected void clearPackageSelectData()
		{
			bool flag = this._curSelectPackageData != null;
			if (flag)
			{
				this._curSelectPackageData = null;
			}
		}

		protected void packageCallBack(string msg, Variant data)
		{
			if (!(msg == "clickLeftBtn"))
			{
				if (!(msg == "clickRightBtn"))
				{
					if (msg == "radioBtn")
					{
						bool flag = this._curLableIdx == data["labelIdx"];
						if (!flag)
						{
							this._curLableIdx = data["labelIdx"];
							this._curPageIdx = 1u;
							this.clearPackageSelectData();
							this.setPackageTileList();
						}
					}
				}
				else
				{
					bool flag2 = this._curPageIdx >= this._maxPageIdx;
					if (!flag2)
					{
						this.clearPackageSelectData();
						this._curPageIdx += 1u;
						this.setPackageTileList();
					}
				}
			}
			else
			{
				bool flag3 = this._curPageIdx > 1u;
				if (flag3)
				{
					this.clearPackageSelectData();
					this._curPageIdx -= 1u;
					this.setPackageTileList();
				}
			}
		}

		protected void setPackageInfo()
		{
			this.setPackageTileList();
		}

		public void setPackageTileList()
		{
		}

		public Variant getCurLabelItems(Variant items, int label)
		{
			Variant variant = new Variant();
			for (int i = 0; i < items.Length; i++)
			{
				Variant variant2 = items[i];
				Variant obj = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(variant2["tpid"]);
				bool flag = this.is_this_kind(variant2, label, obj);
				if (flag)
				{
					variant._arr.Add(variant2);
				}
			}
			return variant;
		}

		protected bool is_this_kind(Variant data, int kind, Variant obj)
		{
			bool flag = data == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = obj == null || obj["conf"] == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					Variant variant = obj["conf"];
					switch (kind)
					{
					case 0:
						result = true;
						return result;
					case 1:
					{
						bool flag3 = variant["type"] == 1;
						if (flag3)
						{
							result = true;
							return result;
						}
						break;
					}
					case 2:
					{
						bool flag4 = variant["type"] == 16;
						if (flag4)
						{
							result = true;
							return result;
						}
						break;
					}
					case 3:
					{
						bool flag5 = variant["type"] == 2;
						if (flag5)
						{
							result = true;
							return result;
						}
						break;
					}
					case 4:
					{
						bool flag6 = variant["type"] != 1 && variant["type"] != 16 && variant["type"] != 2 && variant["type"] != 70001 && variant["type"] != 70002;
						if (flag6)
						{
							result = true;
							return result;
						}
						break;
					}
					}
					result = false;
				}
			}
			return result;
		}

		public Variant getCurPageItems(Variant items, uint curPage, uint maxPage, uint curItemsCount, uint maxItemsCount, uint curPageMaxNum)
		{
			bool flag = this._lockArr == null || this._blockArr == null;
			if (flag)
			{
				this._lockArr = new Variant();
				this._blockArr = new Variant();
				for (int i = 0; i < 36; i++)
				{
					this._lockArr._arr.Add(GameTools.createGroup(new Variant[]
					{
						"lock",
						""
					}));
					this._blockArr._arr.Add(GameTools.createGroup(new Variant[]
					{
						"nolock",
						""
					}));
				}
			}
			int num = (int)curItemsCount;
			bool flag2 = (long)items.Length < (long)((ulong)curItemsCount);
			if (flag2)
			{
				num = items.Length;
			}
			uint num2 = (curPage - 1u) * curPageMaxNum;
			uint num3 = curPage * curPageMaxNum;
			uint val = 0u;
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			maxItemsCount = num3;
			bool flag3 = (ulong)num3 <= (ulong)((long)num);
			if (flag3)
			{
				val = 0u;
				variant._arr.AddRange(items._arr.GetRange((int)num2, (int)(num3 - num2)));
			}
			else
			{
				bool flag4 = (ulong)num2 >= (ulong)((long)num) && num3 <= maxItemsCount;
				if (flag4)
				{
					val = 2u;
					variant = this._blockArr;
				}
				else
				{
					bool flag5 = (ulong)num2 < (ulong)((long)num) && num3 <= maxItemsCount;
					if (flag5)
					{
						val = 4u;
						variant._arr.AddRange(items._arr.GetRange((int)num2, num - (int)num2));
						variant._arr.AddRange(this._blockArr._arr.GetRange(0, (int)((ulong)num3 - (ulong)((long)num))));
					}
				}
			}
			bool flag6 = (long)variant.Length > (long)((ulong)curPageMaxNum);
			if (flag6)
			{
				variant._arr.AddRange(items._arr.GetRange(0, (int)curPageMaxNum));
			}
			else
			{
				variant2 = variant;
			}
			return GameTools.createGroup(new Variant[]
			{
				"items",
				variant2,
				"curType",
				val
			});
		}

		public void RefreshPackage()
		{
		}

		public void openPackageCallBack()
		{
		}

		public void hideMain(bool flag)
		{
		}
	}
}
