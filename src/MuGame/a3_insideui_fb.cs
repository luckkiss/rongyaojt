using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_insideui_fb : FloatUi
	{
		public enum e_room
		{
			Normal,
			Exp,
			Money,
			Cailiao,
			MLZD,
			ZHSLY,
			PVP,
			WDSY,
			DRAGON,
			TLFB109,
			TLFB110,
			TLFB111
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_insideui_fb.<>c <>9 = new a3_insideui_fb.<>c();

			public static Action<GameObject> <>9__95_0;

			internal void <OnLvFinish>b__95_0(GameObject go)
			{
				BaseProxy<LevelProxy>.getInstance().sendLeave_lvl();
			}
		}

		private a3_insideui_fb.e_room eroom = a3_insideui_fb.e_room.Normal;

		public Transform exp_tra;

		public Transform money_tra;

		public Transform cailiao_tra;

		public Transform dragon_tra;

		public Text dragon_txt;

		public Transform mlzd_tra;

		public Transform zhsly_tra;

		public Transform wdsy_tra;

		public Transform tlfb109_tra;

		public Transform tlfb110_tra;

		public Transform tlfb111_tra;

		public Transform pvp_tra;

		private Variant enterdata;

		private Text normalExitTime;

		private Text exitTime;

		private Image exitTime_bar;

		private Text fin_exittime;

		public double close_time;

		private float TotalSec;

		private float blesstime = 0f;

		private int addexp = 0;

		public int addmoney = 0;

		private Transform fb_cast;

		private Transform broad;

		public Transform normal;

		public Transform btn_quit;

		public Transform exittime;

		public Transform light_biu;

		private TabControl tabCtrl1;

		private TabControl tabCtrl109;

		private TabControl tabCtrl110;

		private TabControl tabCtrl111;

		private Transform teamPanel;

		private Transform teamPanel109;

		private Transform teamPanel110;

		private Transform teamPanel111;

		public static a3_insideui_fb instance;

		public GameObject enter_pic1;

		public GameObject enter_pic2;

		private Transform bg1;

		private GameObject pic_icon;

		private GameObject pic_icon1;

		private Text text1;

		private Text text2;

		private Variant data;

		private Variant data109;

		private Variant data110;

		private Variant data111;

		private Text text11;

		private Text text21;

		public int doors = 0;

		public int needkill = 0;

		public bool fb_bgset;

		public static BaseRoomItem room;

		private Text closeTime;

		public static double begintime;

		private double endtime;

		private double closetime;

		private int wait_time = 0;

		public int km_count = 0;

		private int mid;

		private Dictionary<int, Variant> phase = new Dictionary<int, Variant>();

		private Dictionary<int, Dictionary<string, string>> phaseChild = new Dictionary<int, Dictionary<string, string>>();

		public static void ShowInUI(a3_insideui_fb.e_room room)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_INSIDEUI_FB, new ArrayList
			{
				room
			}, false);
		}

		public static void CloseInUI()
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_INSIDEUI_FB);
		}

		public override void init()
		{
			this.enter_pic1 = base.transform.FindChild("enter_pic").gameObject;
			this.enter_pic2 = base.transform.FindChild("enter_pic1").gameObject;
			this.bg1 = this.enter_pic1.transform.FindChild("bg1");
			this.pic_icon = this.enter_pic1.transform.FindChild("ar_result/icon").gameObject;
			this.pic_icon1 = this.enter_pic2.transform.FindChild("ar_result/icon").gameObject;
			this.text1 = this.enter_pic1.transform.FindChild("ar_result/Text1").GetComponent<Text>();
			this.text2 = this.enter_pic1.transform.FindChild("ar_result/Text2").GetComponent<Text>();
			this.text11 = this.enter_pic2.transform.FindChild("ar_result/Text1").GetComponent<Text>();
			this.text21 = this.enter_pic2.transform.FindChild("ar_result/Text2").GetComponent<Text>();
			this.light_biu = base.getTransformByPath("guide_task_info");
			this.exittime = base.getTransformByPath("time");
			this.fin_exittime = base.transform.FindChild("time/Text").GetComponent<Text>();
			this.btn_quit = base.getTransformByPath("btn_quit");
			this.normal = base.getTransformByPath("normal");
			this.broad = base.getTransformByPath("normal/broadcast/broad");
			this.fb_cast = base.getTransformByPath("normal/broadcast/cast");
			Transform transform = base.transform.FindChild("normal/info");
			this.exp_tra = base.transform.FindChild("exp");
			this.money_tra = base.transform.FindChild("money");
			this.cailiao_tra = base.transform.FindChild("cailiao");
			this.mlzd_tra = base.transform.FindChild("mlzd");
			this.zhsly_tra = base.transform.FindChild("zhsly");
			this.wdsy_tra = base.transform.FindChild("wdsy");
			this.tlfb109_tra = base.transform.FindChild("tlfb109");
			this.tlfb110_tra = base.transform.FindChild("tlfb110");
			this.tlfb111_tra = base.transform.FindChild("tlfb111");
			this.pvp_tra = base.transform.FindChild("pvp");
			this.dragon_tra = base.transform.FindChild("dragon");
			this.dragon_txt = this.dragon_tra.FindChild("info/info_desc/Text").GetComponent<Text>();
			this.enter_pic1.SetActive(false);
			this.fb_bgset = false;
			this.enter_pic2.SetActive(false);
			this.normal.gameObject.SetActive(true);
			this.btn_quit.gameObject.SetActive(false);
			this.exittime.gameObject.SetActive(false);
			this.light_biu.gameObject.SetActive(false);
			base.transform.FindChild("btn").gameObject.SetActive(false);
			this.data = SvrLevelConfig.instacne.get_level_data(108u);
			this.data109 = SvrLevelConfig.instacne.get_level_data(109u);
			this.data110 = SvrLevelConfig.instacne.get_level_data(110u);
			this.data111 = SvrLevelConfig.instacne.get_level_data(111u);
			new BaseButton(this.bg1, 1, 1).onClick = delegate(GameObject go)
			{
				this.enter_pic1.SetActive(false);
				this.fb_bgset = false;
				this.enter_pic2.SetActive(true);
				base.transform.FindChild("btn").gameObject.SetActive(false);
				base.transform.FindChild("normal").gameObject.SetActive(true);
				this.sett_f(true);
			};
			new BaseButton(base.transform.FindChild("btn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.enter_pic1.SetActive(false);
				this.fb_bgset = false;
				this.enter_pic2.SetActive(true);
				base.transform.FindChild("btn").gameObject.SetActive(false);
				base.transform.FindChild("normal").gameObject.SetActive(true);
				this.sett_f(true);
			};
			new BaseButton(base.transform.FindChild("normal/btn_quitfb"), 1, 1).onClick = delegate(GameObject go)
			{
				uint curLevelId = MapModel.getInstance().curLevelId;
				string str = string.Empty;
				switch (curLevelId)
				{
				case 101u:
					str = ContMgr.getCont("fb_quit_hint_13", null);
					break;
				case 102u:
					str = ContMgr.getCont("fb_quit_hint_8", null);
					break;
				case 103u:
					str = ContMgr.getCont("fb_quit_hint_12", null);
					break;
				case 104u:
					str = ContMgr.getCont("fb_quit_hint_10", null);
					break;
				case 105u:
					str = ContMgr.getCont("fb_quit_hint_11", null);
					break;
				case 106u:
					str = ContMgr.getCont("fb_quit_hint_14", null);
					break;
				case 107u:
					str = ContMgr.getCont("fb_quit_hint_5", null);
					break;
				case 108u:
				case 109u:
				case 110u:
				case 111u:
					str = ContMgr.getCont("fb_quit_hint_2", null);
					break;
				default:
					str = ContMgr.getCont("fb_quit_hint_1", null);
					break;
				}
				MsgBoxMgr.getInstance().showConfirm(str, delegate
				{
					BaseProxy<LevelProxy>.getInstance().sendLeave_lvl();
					bool activeInHierarchy = this.enter_pic2.activeInHierarchy;
					if (activeInHierarchy)
					{
						this.enter_pic2.gameObject.SetActive(false);
					}
					bool flag3 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
					if (flag3)
					{
						BaseProxy<TeamProxy>.getInstance().SendLeaveTeam(ModelBase<PlayerModel>.getInstance().cid);
						bool flag4 = BaseProxy<TeamProxy>.getInstance().hasEventListener(237u);
						if (flag4)
						{
							BaseProxy<TeamProxy>.getInstance().removeEventListener(237u, new Action<GameEvent>(WdsyOpenDoor.instance.killNum));
						}
						this.doors = 0;
						this.needkill = 0;
						a3_liteMinimap.instance.getGameObjectByPath("taskinfo/bar").SetActive(false);
					}
				}, null, 0);
			};
			new BaseButton(this.btn_quit, 1, 1).onClick = delegate(GameObject go)
			{
				BaseProxy<LevelProxy>.getInstance().sendLeave_lvl();
				bool flag3 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
				if (flag3)
				{
					a3_liteMinimap.instance.getGameObjectByPath("taskinfo/bar").SetActive(false);
					BaseProxy<TeamProxy>.getInstance().SendLeaveTeam(ModelBase<PlayerModel>.getInstance().cid);
					bool flag4 = BaseProxy<TeamProxy>.getInstance().hasEventListener(237u);
					if (flag4)
					{
						BaseProxy<TeamProxy>.getInstance().removeEventListener(237u, new Action<GameEvent>(WdsyOpenDoor.instance.killNum));
					}
					this.doors = 0;
					this.needkill = 0;
				}
			};
			this.normalExitTime = base.transform.FindChild("normal/btn_quitfb/Text").GetComponent<Text>();
			Transform[] componentsInChildren = base.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform2 = componentsInChildren[i];
				bool flag = transform2.name == "btn_blessing";
				if (flag)
				{
					new BaseButton(transform2, 1, 1).onClick = delegate(GameObject go)
					{
						bool flag3 = this.blesstime == 0f;
						if (flag3)
						{
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_BLESSING, null, false);
						}
						else
						{
							flytxt.instance.fly("已经获得鼓舞状态", 0, default(Color), null);
						}
					};
				}
				bool flag2 = transform2.name == "blesson";
				if (flag2)
				{
					transform2.gameObject.SetActive(false);
				}
			}
			this.teamPanel = base.transform.FindChild("wdsy/team");
			this.tabCtrl1 = new TabControl();
			this.tabCtrl1.onClickHanle = new Action<TabControl>(this.onTab108);
			this.tabCtrl1.create(base.getGameObjectByPath("wdsy/title/panelTab1"), base.gameObject, 0, 0, true);
			this.teamPanel109 = base.transform.FindChild("tlfb109/team");
			this.tabCtrl109 = new TabControl();
			this.tabCtrl109.onClickHanle = new Action<TabControl>(this.onTab109);
			this.tabCtrl109.create(base.getGameObjectByPath("tlfb109/title/panelTab1"), base.gameObject, 0, 0, true);
			this.teamPanel110 = base.transform.FindChild("tlfb110/team");
			this.tabCtrl110 = new TabControl();
			this.tabCtrl110.onClickHanle = new Action<TabControl>(this.onTab110);
			this.tabCtrl110.create(base.getGameObjectByPath("tlfb110/title/panelTab1"), base.gameObject, 0, 0, true);
			this.teamPanel111 = base.transform.FindChild("tlfb111/team");
			this.tabCtrl111 = new TabControl();
			this.tabCtrl111.onClickHanle = new Action<TabControl>(this.onTab111);
			this.tabCtrl111.create(base.getGameObjectByPath("tlfb111/title/panelTab1"), base.gameObject, 0, 0, true);
		}

		private void onTab108(TabControl t)
		{
			bool flag = t.getSeletedIndex() == 0;
			if (flag)
			{
				base.getGameObjectByPath("wdsy/info_title").SetActive(true);
				base.getGameObjectByPath("wdsy/info").SetActive(true);
				base.getGameObjectByPath("wdsy/icon").SetActive(true);
				base.getGameObjectByPath("wdsy/team").SetActive(false);
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.teamPanel);
				base.getGameObjectByPath("wdsy/info_title").SetActive(false);
				base.getGameObjectByPath("wdsy/info").SetActive(false);
				base.getGameObjectByPath("wdsy/icon").SetActive(false);
				base.getGameObjectByPath("wdsy/team").SetActive(true);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_CURRENTTEAMINFO, arrayList, false);
			}
		}

		private void onTab109(TabControl t)
		{
			bool flag = t.getSeletedIndex() == 0;
			if (flag)
			{
				base.getGameObjectByPath("tlfb109/info_title").SetActive(true);
				base.getGameObjectByPath("tlfb109/info").SetActive(true);
				base.getGameObjectByPath("tlfb109/icon").SetActive(true);
				base.getGameObjectByPath("tlfb109/team").SetActive(false);
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.teamPanel109);
				base.getGameObjectByPath("tlfb109/info_title").SetActive(false);
				base.getGameObjectByPath("tlfb109/info").SetActive(false);
				base.getGameObjectByPath("tlfb109/icon").SetActive(false);
				base.getGameObjectByPath("tlfb109/team").SetActive(true);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_CURRENTTEAMINFO, arrayList, false);
			}
		}

		private void onTab110(TabControl t)
		{
			bool flag = t.getSeletedIndex() == 0;
			if (flag)
			{
				base.getGameObjectByPath("tlfb110/info_title").SetActive(true);
				base.getGameObjectByPath("tlfb110/info").SetActive(true);
				base.getGameObjectByPath("tlfb110/icon").SetActive(true);
				base.getGameObjectByPath("tlfb110/team").SetActive(false);
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.teamPanel110);
				base.getGameObjectByPath("tlfb110/info_title").SetActive(false);
				base.getGameObjectByPath("tlfb110/info").SetActive(false);
				base.getGameObjectByPath("tlfb110/icon").SetActive(false);
				base.getGameObjectByPath("tlfb110/team").SetActive(true);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_CURRENTTEAMINFO, arrayList, false);
			}
		}

		private void onTab111(TabControl t)
		{
			bool flag = t.getSeletedIndex() == 0;
			if (flag)
			{
				base.getGameObjectByPath("tlfb111/info_title").SetActive(true);
				base.getGameObjectByPath("tlfb111/info").SetActive(true);
				base.getGameObjectByPath("tlfb111/icon").SetActive(true);
				base.getGameObjectByPath("tlfb111/team").SetActive(false);
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.teamPanel111);
				base.getGameObjectByPath("tlfb111/info_title").SetActive(false);
				base.getGameObjectByPath("tlfb111/info").SetActive(false);
				base.getGameObjectByPath("tlfb111/icon").SetActive(false);
				base.getGameObjectByPath("tlfb111/team").SetActive(true);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_CURRENTTEAMINFO, arrayList, false);
			}
		}

		public override void onShowed()
		{
			a3_insideui_fb.instance = this;
			this.enterdata = muLGClient.instance.g_levelsCT.get_curr_lvl_info();
			bool flag = this.enterdata == null || !this.enterdata.ContainsKey("end_tm");
			if (flag)
			{
				throw new Exception("进入副本失败！");
			}
			bool flag2 = this.enterdata != null;
			if (flag2)
			{
				this.endtime = this.enterdata["end_tm"];
			}
			base.transform.FindChild("normal/btn_quitfb").gameObject.SetActive(true);
			this.normal.gameObject.SetActive(true);
			this.exp_tra.gameObject.SetActive(false);
			this.money_tra.gameObject.SetActive(false);
			this.cailiao_tra.gameObject.SetActive(false);
			this.mlzd_tra.gameObject.SetActive(false);
			this.zhsly_tra.gameObject.SetActive(false);
			this.wdsy_tra.gameObject.SetActive(false);
			this.tlfb109_tra.gameObject.SetActive(false);
			this.tlfb110_tra.gameObject.SetActive(false);
			this.tlfb111_tra.gameObject.SetActive(false);
			this.btn_quit.gameObject.SetActive(false);
			this.exittime.gameObject.SetActive(false);
			this.light_biu.gameObject.SetActive(false);
			this.pvp_tra.FindChild("stear").gameObject.SetActive(false);
			this.pvp_tra.FindChild("time").gameObject.SetActive(false);
			this.eroom = a3_insideui_fb.e_room.Normal;
			bool flag3 = this.uiData == null || this.uiData.Count < 1;
			if (flag3)
			{
				throw new Exception("副本界面没有指明类型!");
			}
			this.eroom = (a3_insideui_fb.e_room)this.uiData[0];
			bool flag4 = MapModel.getInstance().curLevelId == 108u;
			if (flag4)
			{
				this.readLevel(0, this.data);
			}
			bool flag5 = MapModel.getInstance().curLevelId == 109u;
			if (flag5)
			{
				this.readLevel(0, this.data109);
			}
			bool flag6 = MapModel.getInstance().curLevelId == 110u;
			if (flag6)
			{
				this.readLevel(0, this.data110);
			}
			bool flag7 = MapModel.getInstance().curLevelId == 111u;
			if (flag7)
			{
				this.readLevel(0, this.data111);
			}
			switch (this.eroom)
			{
			case a3_insideui_fb.e_room.Exp:
				this.exp_tra.gameObject.SetActive(true);
				break;
			case a3_insideui_fb.e_room.Money:
				this.money_tra.gameObject.SetActive(true);
				break;
			case a3_insideui_fb.e_room.Cailiao:
				this.cailiao_tra.gameObject.SetActive(true);
				break;
			case a3_insideui_fb.e_room.MLZD:
				this.mlzd_tra.gameObject.SetActive(true);
				break;
			case a3_insideui_fb.e_room.ZHSLY:
				this.zhsly_tra.gameObject.SetActive(true);
				break;
			case a3_insideui_fb.e_room.PVP:
				this.pvp_tra.gameObject.SetActive(true);
				this.waitTime();
				break;
			case a3_insideui_fb.e_room.WDSY:
				this.wdsy_tra.gameObject.SetActive(true);
				break;
			case a3_insideui_fb.e_room.DRAGON:
				this.dragon_tra.gameObject.SetActive(true);
				break;
			case a3_insideui_fb.e_room.TLFB109:
				this.tlfb109_tra.gameObject.SetActive(true);
				break;
			case a3_insideui_fb.e_room.TLFB110:
				this.tlfb110_tra.gameObject.SetActive(true);
				break;
			case a3_insideui_fb.e_room.TLFB111:
				this.tlfb111_tra.gameObject.SetActive(true);
				break;
			}
			bool flag8 = this.GetNowTran() != null;
			if (flag8)
			{
				this.exitTime = this.GetNowTran().FindChild("info/info_time/time_text").GetComponent<Text>();
				this.exitTime_bar = this.GetNowTran().FindChild("info/info_time/time_bar").GetComponent<Image>();
			}
			this.normalExitTime.text = "未开始";
			bool flag9 = this.exitTime != null;
			if (flag9)
			{
				this.exitTime.text = "未开始";
			}
			bool flag10 = this.exitTime_bar != null;
			if (flag10)
			{
				this.exitTime_bar.fillAmount = 1f;
			}
			this.RefreshExitTime();
			this.TotalSec = (float)Mathf.Max((int)(this.endtime - (double)muNetCleint.instance.CurServerTimeStamp), 0);
			base.CancelInvoke("RefreshExitTime");
			bool flag11 = this.TotalSec > 0f;
			if (flag11)
			{
				base.InvokeRepeating("RefreshExitTime", 0f, 1f);
			}
			this.addexp = 0;
			this.addmoney = 0;
			this.blesstime = 0f;
			this.SetKillNum(0, -1);
			this.SetInfExp(0);
			this.SetInfBo(0);
			this.SetInfKill(0);
			this.SetInfMoney(0);
			bool flag12 = this.enterdata.ContainsKey("energy");
			if (flag12)
			{
				this.SetInfBaozang(this.enterdata["energy"]);
			}
			else
			{
				this.SetInfBaozang(0);
			}
			this.SetInfBoss(0);
			BaseProxy<A3_ActiveProxy>.getInstance().addEventListener(A3_ActiveProxy.EVENT_ONBLESS, new Action<GameEvent>(this.OnBless));
			base.transform.SetAsFirstSibling();
			bool open_pic = BaseProxy<LevelProxy>.getInstance().open_pic;
			if (open_pic)
			{
				this.enter_pic1.SetActive(true);
				this.fb_bgset = true;
				base.transform.FindChild("btn").gameObject.SetActive(true);
				bool flag13 = ModelBase<PlayerModel>.getInstance().profession == 2;
				if (flag13)
				{
					this.pic_icon.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/ar/" + BaseProxy<LevelProxy>.getInstance().codes[0]);
				}
				bool flag14 = ModelBase<PlayerModel>.getInstance().profession == 3;
				if (flag14)
				{
					this.pic_icon.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/ar/" + BaseProxy<LevelProxy>.getInstance().codes[1]);
				}
				bool flag15 = ModelBase<PlayerModel>.getInstance().profession == 5;
				if (flag15)
				{
					this.pic_icon.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/ar/" + BaseProxy<LevelProxy>.getInstance().codes[2]);
				}
				this.enter_pic();
				this.enter_pic2_show();
				a3_liteMinimap.instance.taskinfo.SetActive(false);
				base.transform.FindChild("normal").gameObject.SetActive(false);
				this.sett_f(false);
				BaseProxy<LevelProxy>.getInstance().open_pic = false;
			}
		}

		private void waitTime()
		{
			this.wait_time = 6;
			SelfRole._inst.can_buff_move = false;
			SelfRole._inst.can_buff_skill = false;
			base.CancelInvoke("pvpTimeGo");
			base.InvokeRepeating("pvpTimeGo", 0f, 1f);
		}

		private void pvpTimeGo()
		{
			bool flag = this.wait_time <= -1;
			if (flag)
			{
				this.pvp_tra.gameObject.SetActive(false);
				base.CancelInvoke("pvpTimeGo");
			}
			else
			{
				bool flag2 = this.wait_time <= 0;
				if (flag2)
				{
					SelfRole._inst.can_buff_move = true;
					SelfRole._inst.can_buff_skill = true;
					this.pvp_tra.FindChild("stear").gameObject.SetActive(true);
					this.pvp_tra.FindChild("time").gameObject.SetActive(false);
					bool flag3 = !SelfRole._inst.isDead;
					if (flag3)
					{
						BaseProxy<MoveProxy>.getInstance().sendVec();
					}
				}
				else
				{
					bool flag4 = this.wait_time <= 5;
					if (flag4)
					{
						this.pvp_tra.FindChild("stear").gameObject.SetActive(false);
						this.pvp_tra.FindChild("time").gameObject.SetActive(true);
						this.pvp_tra.FindChild("time").GetComponent<Image>().sprite = (Resources.Load("icon/countdown/countdown" + this.wait_time, typeof(Sprite)) as Sprite);
					}
					else
					{
						bool flag5 = this.wait_time > 5;
						if (flag5)
						{
							this.pvp_tra.FindChild("stear").gameObject.SetActive(false);
							this.pvp_tra.FindChild("time").gameObject.SetActive(false);
						}
					}
				}
				this.wait_time--;
			}
		}

		private void sett_f(bool b)
		{
			skillbar.instance.transform.FindChild("combat").gameObject.SetActive(b);
			joystick.instance.transform.FindChild("Image").GetComponent<Image>().enabled = b;
			joystick.instance.transform.FindChild("stick").GetComponent<Image>().enabled = b;
			a3_expbar.instance.transform.FindChild("lt_temp").gameObject.SetActive(b);
			a3_expbar.instance.transform.FindChild("operator").gameObject.SetActive(b);
			a3_liteMinimap.instance.transform.FindChild("fun_open").gameObject.SetActive(b);
		}

		private void enter_pic2_show()
		{
			bool flag = ModelBase<PlayerModel>.getInstance().profession == 2;
			if (flag)
			{
				this.pic_icon1.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/ar/" + BaseProxy<LevelProxy>.getInstance().codes[0]);
			}
			bool flag2 = ModelBase<PlayerModel>.getInstance().profession == 3;
			if (flag2)
			{
				this.pic_icon1.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/ar/" + BaseProxy<LevelProxy>.getInstance().codes[1]);
			}
			bool flag3 = ModelBase<PlayerModel>.getInstance().profession == 5;
			if (flag3)
			{
				this.pic_icon1.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/ar/" + BaseProxy<LevelProxy>.getInstance().codes[2]);
			}
		}

		public void enter_pic()
		{
			List<SXML> list = null;
			bool flag = list == null;
			if (flag)
			{
				list = XMLMgr.instance.GetSXMLList("accent_relic.relic", "");
				for (int i = 0; i < list.Count; i++)
				{
					bool flag2 = list[i].getInt("carr") == ModelBase<PlayerModel>.getInstance().profession && list[i].getString("type") == BaseProxy<LevelProxy>.getInstance().codess[0].ToString();
					if (flag2)
					{
						List<SXML> nodeList = list[i].GetNodeList("relic_god", "id==" + BaseProxy<LevelProxy>.getInstance().codess[1].ToString());
						foreach (SXML current in nodeList)
						{
							this.text1.text = current.getString("des1");
							this.text2.text = current.getString("des2");
							this.text11.text = current.getString("des1");
							this.text21.text = current.getString("des2");
						}
					}
				}
			}
		}

		public override void onClosed()
		{
			this.EndBless();
			a3_insideui_fb.instance = null;
			a3_insideui_fb.room = null;
			this.eroom = a3_insideui_fb.e_room.Normal;
			BaseProxy<A3_ActiveProxy>.getInstance().removeEventListener(A3_ActiveProxy.EVENT_ONBLESS, new Action<GameEvent>(this.OnBless));
		}

		private void Update()
		{
			bool activeInHierarchy = this.exittime.gameObject.activeInHierarchy;
			if (activeInHierarchy)
			{
				double num = this.close_time - (double)muNetCleint.instance.CurServerTimeStamp;
				this.fin_exittime.text = "<color=#ff0000>(" + (int)num + ")</color>";
				bool flag = num <= 0.0;
				if (flag)
				{
					BaseProxy<LevelProxy>.getInstance().sendLeave_lvl();
					bool flag2 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
					if (flag2)
					{
						BaseProxy<TeamProxy>.getInstance().SendLeaveTeam(ModelBase<PlayerModel>.getInstance().cid);
						this.doors = 0;
						this.needkill = 0;
						bool flag3 = BaseProxy<TeamProxy>.getInstance().hasEventListener(237u);
						if (flag3)
						{
							BaseProxy<TeamProxy>.getInstance().removeEventListener(237u, new Action<GameEvent>(WdsyOpenDoor.instance.killNum));
						}
					}
				}
			}
		}

		private void RefreshExitTime()
		{
			this.normalExitTime.text = Globle.formatTime(Mathf.Max((int)(this.endtime - (double)muNetCleint.instance.CurServerTimeStamp), 0), true);
			bool flag = this.exitTime != null;
			if (flag)
			{
				this.exitTime.text = Globle.formatTime(Mathf.Max((int)(this.endtime - (double)muNetCleint.instance.CurServerTimeStamp), 0), true);
			}
			bool flag2 = this.exitTime_bar != null;
			if (flag2)
			{
				this.exitTime_bar.fillAmount = Mathf.Max((float)(this.endtime - (double)muNetCleint.instance.CurServerTimeStamp), 0f) / this.TotalSec;
			}
			bool flag3 = this.eroom == a3_insideui_fb.e_room.DRAGON;
			if (flag3)
			{
				int num = (int)(this.endtime - (double)muNetCleint.instance.CurServerTimeStamp) / 60;
				string currentDragonName = ModelBase<A3_SlayDragonModel>.getInstance().GetCurrentDragonName();
				bool flag4 = num >= ModelBase<A3_SlayDragonModel>.getInstance().GetKillingTime();
				if (flag4)
				{
					int num2 = (int)(this.endtime - (double)muNetCleint.instance.CurServerTimeStamp - (double)(60 * ModelBase<A3_SlayDragonModel>.getInstance().GetKillingTime()));
					bool flag5 = num2 > 0;
					if (flag5)
					{
						this.dragon_txt.text = string.Format("{0}将于<color=#00ff00>{1}</color>秒后出现，请各位勇士做好准备", currentDragonName, num2);
					}
				}
				else
				{
					int num2 = (int)(this.endtime - (double)muNetCleint.instance.CurServerTimeStamp);
					bool flag6 = num2 > 0;
					if (flag6)
					{
						string arg = string.Format("{0:D2}:{1:D2}", num2 / 60, num2 % 60);
						this.dragon_txt.text = string.Format("{0}已经刷新，请各位勇士前往击杀，{0}将于<color=#00ff00>{1}</color>后逃离巨龙深渊！", currentDragonName, arg);
					}
				}
			}
		}

		private void readLevel(int door, Variant datas = null)
		{
			int lvl = a3_counterpart.lvl;
			this.phase.Clear();
			this.phaseChild.Clear();
			for (int i = 0; i < datas["diff_lvl"][lvl]["phase"]._arr.Count; i++)
			{
				bool flag = !this.phase.ContainsKey(i);
				if (flag)
				{
					this.phase.Add(i, datas["diff_lvl"][lvl]["phase"][i]);
				}
			}
			foreach (KeyValuePair<int, Variant> current in this.phase)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("p", current.Value["p"]._str);
				dictionary.Add("des", current.Value["des"]._str);
				dictionary.Add("target", current.Value["target"]._str);
				dictionary.Add("num", current.Value["num"]._str);
				bool flag2 = !this.phaseChild.ContainsKey(current.Key);
				if (flag2)
				{
					this.phaseChild.Add(current.Key, dictionary);
				}
			}
			Transform transform = this.GetNowTran().FindChild("info/info_killnums/mid");
			bool flag3 = transform != null;
			if (flag3)
			{
				transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_9", new string[]
				{
					this.phaseChild[door]["des"]
				});
			}
			Transform transform2 = this.GetNowTran().FindChild("info/info_killnums/Text");
			bool flag4 = transform2 != null;
			if (flag4)
			{
				transform2.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
				{
					this.km_count.ToString()
				}) + "/" + this.phaseChild[door]["num"];
			}
		}

		public void SetInfKill(int i)
		{
			bool flag = this.GetNowTran() == null;
			if (!flag)
			{
				Transform transform = this.GetNowTran().FindChild("info/info_killnum/Text");
				bool flag2 = transform != null;
				if (flag2)
				{
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_1", new List<string>
					{
						i.ToString()
					});
				}
			}
		}

		public void SetLvl(int lvl)
		{
			bool flag = this.GetNowTran() == null;
			if (!flag)
			{
				Transform transform = this.GetNowTran().FindChild("icon/rank");
				bool flag2 = transform == null;
				if (!flag2)
				{
					if (lvl == 1)
					{
						transform.GetComponent<Text>().text = "";
					}
				}
			}
		}

		private void bjm()
		{
		}

		public void SetInfKillPgs(int i, int max)
		{
			bool flag = this.GetNowTran() == null;
			if (!flag)
			{
				Transform transform = this.GetNowTran().FindChild("info/info_killnum/Text");
				bool flag2 = transform == null;
				if (!flag2)
				{
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_2", new List<string>
					{
						i.ToString(),
						max.ToString()
					});
				}
			}
		}

		public void SetInfBaozang(int i)
		{
			bool flag = this.GetNowTran() == null;
			if (!flag)
			{
				Transform transform = this.GetNowTran().FindChild("info/info_baozang/Text");
				bool flag2 = transform == null;
				if (!flag2)
				{
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_6", new List<string>
					{
						i.ToString()
					});
				}
			}
		}

		public void refre_lvl()
		{
			for (int i = 0; i < 10; i++)
			{
			}
		}

		public void SetInfExp(int i)
		{
			bool flag = this.GetNowTran() == null;
			if (!flag)
			{
				Transform transform = this.GetNowTran().FindChild("info/info_exp/Text");
				bool flag2 = transform == null;
				if (!flag2)
				{
					this.addexp += i;
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_3", new List<string>
					{
						this.addexp.ToString()
					});
				}
			}
		}

		public void SetInfBo(int i)
		{
			int num = 0;
			bool flag = this.GetNowTran() == null;
			if (!flag)
			{
				Variant variant = SvrLevelConfig.instacne.get_level_data(101u);
				int diff = a3_counterpart_exp.diff;
				bool flag2 = diff > 0;
				if (flag2)
				{
					num = variant["diff_lvl"][diff]["bo_all"];
				}
				Transform transform = this.GetNowTran().FindChild("info/info_boshu/Text");
				bool flag3 = transform == null;
				if (!flag3)
				{
					bool flag4 = num > 0;
					if (flag4)
					{
						transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_4", new List<string>
						{
							i.ToString(),
							num.ToString()
						});
					}
				}
			}
		}

		public void SetInfMoney(int i)
		{
			bool flag = this.GetNowTran() == null;
			if (!flag)
			{
				Transform transform = this.GetNowTran().FindChild("info/info_money/Text");
				bool flag2 = transform == null;
				if (!flag2)
				{
					this.addmoney += i;
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_5", new List<string>
					{
						this.addmoney.ToString()
					});
				}
			}
		}

		public void SetInfBoss(int i)
		{
			bool flag = this.GetNowTran() == null;
			if (!flag)
			{
				Transform transform = this.GetNowTran().FindChild("info/info_boss/Text");
				bool flag2 = transform == null;
				if (!flag2)
				{
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_7", new List<string>
					{
						i.ToString()
					});
				}
			}
		}

		public void SetBroadCast(Variant data)
		{
			string cont = ContMgr.getCont(data["msg"], null);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.fb_cast.gameObject);
			gameObject.transform.FindChild("Text").GetComponent<Text>().text = cont;
			gameObject.transform.SetParent(this.broad);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.SetActive(true);
			UnityEngine.Object.Destroy(gameObject, 4f);
		}

		public Transform GetNowTran()
		{
			Transform result;
			switch (this.eroom)
			{
			case a3_insideui_fb.e_room.Exp:
				result = this.exp_tra;
				return result;
			case a3_insideui_fb.e_room.Money:
				result = this.money_tra;
				return result;
			case a3_insideui_fb.e_room.Cailiao:
				result = this.cailiao_tra;
				return result;
			case a3_insideui_fb.e_room.MLZD:
				result = this.mlzd_tra;
				return result;
			case a3_insideui_fb.e_room.ZHSLY:
				result = this.zhsly_tra;
				return result;
			case a3_insideui_fb.e_room.WDSY:
				result = this.wdsy_tra;
				return result;
			case a3_insideui_fb.e_room.DRAGON:
				result = this.dragon_tra;
				return result;
			case a3_insideui_fb.e_room.TLFB109:
				result = this.tlfb109_tra;
				return result;
			case a3_insideui_fb.e_room.TLFB110:
				result = this.tlfb110_tra;
				return result;
			case a3_insideui_fb.e_room.TLFB111:
				result = this.tlfb111_tra;
				return result;
			}
			result = null;
			return result;
		}

		public void setAct()
		{
			base.transform.FindChild("normal/btn_quitfb").gameObject.SetActive(false);
			this.exp_tra.gameObject.SetActive(false);
			this.money_tra.gameObject.SetActive(false);
			this.cailiao_tra.gameObject.SetActive(false);
			this.mlzd_tra.gameObject.SetActive(false);
			this.zhsly_tra.gameObject.SetActive(false);
			this.wdsy_tra.gameObject.SetActive(false);
			this.tlfb109_tra.gameObject.SetActive(false);
			this.tlfb110_tra.gameObject.SetActive(false);
			this.tlfb111_tra.gameObject.SetActive(false);
			this.normal.gameObject.SetActive(false);
			this.btn_quit.gameObject.SetActive(true);
		}

		public void OnLvFinish(Variant data)
		{
			bool flag = data.ContainsKey("close_tm");
			if (flag)
			{
				double num = data["close_tm"];
				this.closetime = num;
			}
			base.transform.FindChild("normal/level_finish").gameObject.SetActive(true);
			BaseButton arg_7C_0 = new BaseButton(base.transform.FindChild("normal/level_finish/bt"), 1, 1);
			Action<GameObject> arg_7C_1;
			if ((arg_7C_1 = a3_insideui_fb.<>c.<>9__95_0) == null)
			{
				arg_7C_1 = (a3_insideui_fb.<>c.<>9__95_0 = new Action<GameObject>(a3_insideui_fb.<>c.<>9.<OnLvFinish>b__95_0));
			}
			arg_7C_0.onClick = arg_7C_1;
		}

		private void ShowBless()
		{
		}

		public void SetKillNum(int i, int max = -1)
		{
			bool flag = max > 0;
			if (flag)
			{
				this.SetInfKillPgs(i, max);
			}
			else
			{
				this.SetInfKill(i);
			}
		}

		public void SetGoldLeft(int i)
		{
			this.SetInfBaozang(i);
		}

		public void OnBless(GameEvent e)
		{
			bool flag = this.GetNowTran() == null;
			if (!flag)
			{
				Transform transform = this.GetNowTran().FindChild("btn_blessing/blesson");
				bool flag2 = transform == null;
				if (!flag2)
				{
					transform.gameObject.SetActive(true);
					this.blesstime = 100f;
				}
			}
		}

		public void EndBless()
		{
			bool flag = this.GetNowTran() == null;
			if (!flag)
			{
				Transform transform = this.GetNowTran().FindChild("btn_blessing/blesson");
				bool flag2 = transform == null;
				if (!flag2)
				{
					transform.gameObject.SetActive(false);
				}
			}
		}
	}
}
