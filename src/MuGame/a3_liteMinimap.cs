using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_liteMinimap : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_liteMinimap.<>c <>9 = new a3_liteMinimap.<>c();

			public static Action<GameObject> <>9__56_0;

			public static Action<GameObject> <>9__152_1;

			internal void <init>b__56_0(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ROLE, null, false);
				a3_role.ForceIndex = 1;
			}

			internal void <onTab1>b__152_1(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SPEEDTEAM, null, false);
			}
		}

		public static a3_liteMinimap instance;

		public Transform transItemCon;

		public GameObject goUser;

		public GameObject goTempEnemy;

		public GameObject goTempBoss;

		public GameObject goTempNpc;

		public GameObject goTempJy;

		public GameObject goTempOther;

		public GameObject noNet;

		public GameObject goUpwardArrow;

		public GameObject goDownwardArrow;

		public ScrollRect scrlrectTaskPanel;

		public Transform batt;

		public GameObject net1;

		public GameObject net2;

		public GameObject net3;

		public GameObject chongDian;

		public GameObject dianLiang;

		public Transform transTask;

		private Animator taskAnim;

		private BaseButton btnTask;

		private BaseButton btnTask_close;

		public Text time1;

		public Text net;

		private string strPos = "({0},{1})";

		private BaseButton btSee;

		private GameObject imgSee;

		private float dianLiangNew;

		private float notic_v;

		private int notic_i;

		private TickItem tick;

		private Transform taskPanel;

		private Transform teamPanel;

		private bool notice_w;

		private bool isTaskBtnShow = false;

		private TabControl tabCtrl1;

		private int oldtab;

		private GameObject equip_no;

		private Text accentExp_text;

		private Image accentExp_Image;

		private Image Godicon;

		private Text YGname;

		public GameObject taskinfo;

		public TaskData task_id;

		private GameObject fun_open;

		private Text fun_des;

		private Image fun_icon;

		public int fun_i = 1;

		public int func_id;

		public int grade;

		public int level;

		public int currentTopShowSiblingIndex = 0;

		public GameObject godlight;

		public bool notice_ison;

		private GameObject CangBaoTu;

		public int notice_i;

		private bool showDrawAvaiable = false;

		private long nowtime = 0L;

		private List<int> achi = new List<int>();

		private int timesDraw = 0;

		private float timer = 0f;

		public Camera m_minimap_camara;

		public static float usrAnglesOffset = 0f;

		public static GameObject camGo;

		private bool _miniMapActive = true;

		private Dictionary<string, GameObject> dMonInMinimap = new Dictionary<string, GameObject>();

		private Dictionary<int, GameObject> dicTaskPage = new Dictionary<int, GameObject>();

		private GameObject has_guide_show = null;

		private int select;

		private int oldtab1 = 0;

		public uint active_leftTm = 0u;

		private int notice_tt;

		public bool miniMapActive
		{
			get
			{
				return this._miniMapActive;
			}
			set
			{
				bool flag = this._miniMapActive == value;
				if (!flag)
				{
					this._miniMapActive = value;
					bool miniMapActive = this._miniMapActive;
					if (miniMapActive)
					{
						TickMgr.instance.addTick(this.tick);
					}
					else
					{
						TickMgr.instance.removeTick(this.tick);
					}
					this.goUser.SetActive(this._miniMapActive);
					foreach (GameObject current in this.dMonInMinimap.Values)
					{
						current.SetActive(this._miniMapActive);
					}
				}
			}
		}

		public override void init()
		{
			base.alain();
			this.time1 = base.getComponentByPath<Text>("mobile/time");
			this.time1.text = "";
			this.net = base.getComponentByPath<Text>("mobile/net/wifi or 4G");
			this.net.text = "";
			this.noNet = base.getGameObjectByPath("mobile/net/no_net");
			this.noNet.SetActive(false);
			this.batt = base.getComponentByPath<Transform>("mobile/battry/battry1");
			this.net1 = base.getGameObjectByPath("mobile/net/net_1");
			this.net2 = base.getGameObjectByPath("mobile/net/net_2");
			this.net3 = base.getGameObjectByPath("mobile/net/net_3");
			this.dianLiang = base.getGameObjectByPath("mobile/battry");
			this.chongDian = base.getGameObjectByPath("mobile/battry/chongdian");
			this.net1.SetActive(false);
			this.net2.SetActive(true);
			this.net3.SetActive(false);
			this.fun_open = base.transform.FindChild("fun_open").gameObject;
			this.fun_des = this.fun_open.transform.FindChild("fun_des").GetComponent<Text>();
			this.fun_icon = this.fun_open.transform.FindChild("fun_icon").GetComponent<Image>();
			this.fun_open.gameObject.SetActive(true);
			ModelBase<notice_model>.getInstance().xml_time();
			this.CangBaoTu = base.transform.FindChild("notice").gameObject;
			this.CangBaoTu.SetActive(false);
			new BaseButton(this.CangBaoTu.transform.FindChild("bg"), 1, 1).onClick = new Action<GameObject>(this.join_tips);
			this.btSee = new BaseButton(base.getTransformByPath("normal/minimap/see"), 1, 1);
			this.imgSee = base.getGameObjectByPath("normal/minimap/see/Image");
			this.btSee.onClick = new Action<GameObject>(this.onSeeClick);
			this.taskinfo = base.transform.FindChild("taskinfo").gameObject;
			this.chongDian.SetActive(false);
			this.dianLiang.SetActive(false);
			this.accentExp_text = base.transform.FindChild("taskinfo/bar/jindu").gameObject.GetComponent<Text>();
			this.accentExp_Image = base.transform.FindChild("taskinfo/bar/bar").gameObject.GetComponent<Image>();
			this.Godicon = base.transform.FindChild("taskinfo/bar/icon/icon").gameObject.GetComponent<Image>();
			this.YGname = base.transform.FindChild("taskinfo/bar/name").gameObject.GetComponent<Text>();
			new BaseButton(base.transform.FindChild("taskinfo/bar/open"), 1, 1).onClick = new Action<GameObject>(this.onYGfb);
			BaseButton baseButton = new BaseButton(base.transform.FindChild("normal/minimap/see"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onSee);
			this.equip_no = base.transform.FindChild("equip_no").gameObject;
			BaseButton arg_35B_0 = new BaseButton(this.equip_no.transform, 1, 1);
			Action<GameObject> arg_35B_1;
			if ((arg_35B_1 = a3_liteMinimap.<>c.<>9__56_0) == null)
			{
				arg_35B_1 = (a3_liteMinimap.<>c.<>9__56_0 = new Action<GameObject>(a3_liteMinimap.<>c.<>9.<init>b__56_0));
			}
			arg_35B_0.onClick = arg_35B_1;
			new BaseButton(base.getTransformByPath("goonDart"), 1, 1).onClick = delegate(GameObject go)
			{
				flytxt.instance.fly("已继续押镖", 0, default(Color), null);
				BaseProxy<a3_dartproxy>.getInstance().gotoDart = true;
				base.getGameObjectByPath("goonDart").SetActive(false);
			};
			this.equip_no.SetActive(false);
			this.tick = new TickItem(new Action<float>(this.onUpdate));
			this.transItemCon = base.getTransformByPath("normal/minimap/itemcon");
			this.goUser = base.getGameObjectByPath("normal/minimap/itemcon/tempU");
			this.goTempEnemy = base.getGameObjectByPath("normal/minimap/tempE");
			this.goTempBoss = base.getGameObjectByPath("normal/minimap/tempboss");
			this.goTempNpc = base.getGameObjectByPath("normal/minimap/tempNpc");
			this.goTempJy = base.getGameObjectByPath("normal/minimap/tempjy");
			this.goTempOther = base.getGameObjectByPath("normal/minimap/tempOther");
			this.goTempNpc.SetActive(false);
			this.goTempJy.SetActive(false);
			this.goTempOther.SetActive(false);
			this.goTempBoss.SetActive(false);
			this.goTempEnemy.SetActive(false);
			a3_liteMinimap.instance = this;
			this.refreshMapname();
			this.transTask = base.getTransformByPath("taskinfo");
			this.btnTask = new BaseButton(this.transTask.FindChild("title/btnshow"), 1, 1);
			this.btnTask.onClick = new Action<GameObject>(this.OnTaskClickShow);
			this.btnTask_close = new BaseButton(this.transTask.FindChild("title/btnshow_close"), 1, 1);
			this.btnTask_close.onClick = new Action<GameObject>(this.OnTaskClickClose);
			this.taskAnim = this.transTask.GetComponent<Animator>();
			this.transTask.FindChild("skin/view").GetComponent<ScrollRect>().onValueChanged.AddListener(delegate(Vector2 any)
			{
				this.CheckArrow();
			});
			this.CheckLock();
			this.CheckLock4Screamingbox();
			this.CheckFirstRecharge();
			this.taskPanel = base.transform.FindChild("taskinfo/skin/view/con");
			this.teamPanel = base.transform.FindChild("taskinfo/skin/team");
			this.scrlrectTaskPanel = base.transform.FindChild("taskinfo/skin/view").GetComponent<ScrollRect>();
			this.goUpwardArrow = base.transform.FindChild("taskinfo/skin/view/Head").gameObject;
			this.goDownwardArrow = base.transform.FindChild("taskinfo/skin/view/Tail").gameObject;
			this.isTaskBtnShow = false;
			this.OnTaskClickShow(null);
			this.oldtab = 1;
			this.tabCtrl1 = new TabControl();
			this.tabCtrl1.onClickHanle = new Action<TabControl>(this.onTab1);
			this.tabCtrl1.create(base.getGameObjectByPath("taskinfo/title/panelTab1"), base.gameObject, 0, 0, true);
			this.showActiveIcon(BaseProxy<GeneralProxy>.getInstance().active_open, BaseProxy<GeneralProxy>.getInstance().active_left_tm);
			base.InvokeRepeating("setTextPos", 0f, 3f);
			bool flag = Application.platform == RuntimePlatform.Android;
			if (flag)
			{
				base.InvokeRepeating("BatteryValue", 0f, 8f);
			}
			base.InvokeRepeating("notice_show", 0f, 1f);
		}

		private void join_tips(GameObject obj)
		{
			switch (this.notice_i)
			{
			case 1:
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add("findbtu");
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList, false);
				break;
			}
			case 2:
			{
				ArrayList arrayList2 = new ArrayList();
				arrayList2.Add("pvp");
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList2, false);
				break;
			}
			case 3:
			{
				ArrayList arrayList3 = new ArrayList();
				arrayList3.Add(1);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ELITEMON, arrayList3, false);
				break;
			}
			case 4:
			{
				ArrayList arrayList4 = new ArrayList();
				arrayList4.Add("forchest");
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList4, false);
				break;
			}
			}
			bool activeSelf = this.CangBaoTu.activeSelf;
			if (activeSelf)
			{
				this.CangBaoTu.SetActive(false);
			}
		}

		private void CheckArrow()
		{
			bool flag = this.taskPanel.childCount > 3;
			if (flag)
			{
				bool flag2 = this.taskPanel.GetChild(0).transform.position.y <= this.goUpwardArrow.transform.position.y;
				if (flag2)
				{
					this.goUpwardArrow.SetActive(false);
					this.goDownwardArrow.SetActive(true);
				}
				else
				{
					bool flag3 = this.taskPanel.GetChild(this.taskPanel.childCount - 1).transform.position.y >= this.goDownwardArrow.transform.position.y;
					if (flag3)
					{
						this.goUpwardArrow.SetActive(true);
						this.goDownwardArrow.SetActive(false);
					}
					else
					{
						this.goUpwardArrow.SetActive(true);
						this.goDownwardArrow.SetActive(true);
					}
				}
			}
			else
			{
				this.goUpwardArrow.SetActive(false);
				this.goDownwardArrow.SetActive(false);
			}
		}

		private void OnItemChanged(GameEvent e)
		{
			int i = 0;
			Dictionary<int, TaskData> dicTaskData = ModelBase<A3_TaskModel>.getInstance().GetDicTaskData();
			List<int> list = new List<int>(dicTaskData.Keys);
			while (i < list.Count)
			{
				bool flag = dicTaskData[list[i]].targetType == TaskTargetType.GET_ITEM_GIVEN;
				if (flag)
				{
					GameObject gameObject = this.dicTaskPage[list[i]];
					Text component = gameObject.transform.FindChild("desc").GetComponent<Text>();
					string text = ModelBase<A3_TaskModel>.getInstance().GetTaskDesc(list[i], dicTaskData[list[i]].isComplete) + this.GetCountStr(list[i]);
					component.text = text;
				}
				i++;
			}
		}

		public void function_open(int i)
		{
			int lvl = (int)ModelBase<PlayerModel>.getInstance().lvl;
			int up_lvl = (int)ModelBase<PlayerModel>.getInstance().up_lvl;
			SXML sXML = XMLMgr.instance.GetSXML("func_open.func_pre.order", "id==" + i);
			bool flag = sXML == null;
			if (flag)
			{
				this.fun_open.gameObject.SetActive(false);
			}
			else
			{
				this.fun_des.text = sXML.getString("des");
				this.fun_icon.sprite = (Resources.Load("icon/func_open/" + sXML.getInt("icon"), typeof(Sprite)) as Sprite);
				this.func_id = sXML.getInt("func_id");
				bool flag2 = this.func_id == 0;
				if (flag2)
				{
					this.grade = sXML.getInt("grade");
					this.level = sXML.getInt("level");
					bool flag3 = lvl * 100 + up_lvl >= this.grade * 100 + this.level;
					if (flag3)
					{
						this.function_open(i + 1);
					}
				}
				bool flag4 = FunctionItem.Instance != null;
				if (flag4)
				{
					bool flag5 = FunctionOpenMgr.instance.dItem.Keys.Contains(this.func_id) && this.func_id != 0;
					if (flag5)
					{
						bool opened = FunctionOpenMgr.instance.dItem[this.func_id].opened;
						if (opened)
						{
							this.function_open(i + 1);
						}
					}
				}
			}
		}

		public void Update()
		{
			bool flag = SelfRole._inst != null;
			if (flag)
			{
				bool flag2 = SelfRole._inst.m_curModel != null;
				if (flag2)
				{
					bool flag3 = SelfRole.fsm.currentState == StateIdle.Instance;
					if (flag3)
					{
						worldmap.Desmapimg();
					}
				}
			}
			bool flag4 = DateTime.Now.Minute > 9;
			if (flag4)
			{
				this.time1.text = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
			}
			else
			{
				this.time1.text = DateTime.Now.Hour + ":0" + DateTime.Now.Minute;
			}
			bool flag5 = Application.internetReachability == NetworkReachability.NotReachable;
			if (flag5)
			{
				this.net.text = "";
				this.noNet.SetActive(true);
			}
			else
			{
				bool flag6 = Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork;
				if (flag6)
				{
					this.net.text = "4G";
					this.noNet.SetActive(false);
				}
				else
				{
					this.noNet.SetActive(false);
					this.net.text = "WIFI";
				}
			}
			bool flag7 = muNetCleint.instance.curServerPing < 60;
			if (flag7)
			{
				this.net3.SetActive(true);
				this.net2.SetActive(false);
				this.net1.SetActive(false);
			}
			else
			{
				bool flag8 = muNetCleint.instance.curServerPing < 200;
				if (flag8)
				{
					this.net3.SetActive(false);
					this.net2.SetActive(true);
					this.net1.SetActive(false);
				}
				else
				{
					this.net3.SetActive(false);
					this.net2.SetActive(false);
					this.net1.SetActive(true);
				}
			}
			bool flag9 = BaseProxy<TeamProxy>.getInstance() != null;
			if (flag9)
			{
				bool flag10 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
				if (flag10)
				{
					bool flag11 = !base.getGameObjectByPath("taskinfo/title/Team_Num").gameObject.activeInHierarchy;
					if (flag11)
					{
						base.getGameObjectByPath("taskinfo/title/Team_Num").SetActive(true);
					}
					int count = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
					base.getGameObjectByPath("taskinfo/title/Team_Num/text").GetComponent<Text>().text = count.ToString();
				}
				else
				{
					bool activeInHierarchy = base.getGameObjectByPath("taskinfo/title/Team_Num").gameObject.activeInHierarchy;
					if (activeInHierarchy)
					{
						base.getGameObjectByPath("taskinfo/title/Team_Num").SetActive(false);
					}
				}
			}
			bool flag12 = this.tabCtrl1.getSeletedIndex() == 1 && base.getGameObjectByPath("taskinfo/bar").activeSelf;
			if (flag12)
			{
				base.getGameObjectByPath("taskinfo/bar").SetActive(false);
			}
		}

		private void notice_show()
		{
			bool flag = !this.notice_w;
			if (flag)
			{
				for (int i = 0; i < ModelBase<notice_model>.getInstance().notice.Count; i++)
				{
					foreach (float current in ModelBase<notice_model>.getInstance().notice[i].time.Keys)
					{
						this.notice_ison = ((long)(ModelBase<notice_model>.getInstance().notice[i].zhuan * 100 + ModelBase<notice_model>.getInstance().notice[i].level) > (long)((ulong)(ModelBase<PlayerModel>.getInstance().up_lvl * 100u + ModelBase<PlayerModel>.getInstance().lvl)));
						int num = (int)(current * 60f);
						int num2 = (int)(ModelBase<notice_model>.getInstance().notice[i].time[current] * 60f);
						bool flag2 = DateTime.Now.Minute + DateTime.Now.Hour * 60 >= num && DateTime.Now.Minute + DateTime.Now.Hour * 60 <= num2;
						if (flag2)
						{
							bool flag3 = !this.CangBaoTu.activeSelf && !this.notice_ison;
							if (flag3)
							{
								this.CangBaoTu.SetActive(true);
								this.notice_i = ModelBase<notice_model>.getInstance().notice[i].id;
								this.CangBaoTu.transform.FindChild("des").GetComponent<Text>().text = ModelBase<notice_model>.getInstance().notice[i].des;
								Image component = this.CangBaoTu.transform.FindChild("icon").GetComponent<Image>();
								component.sprite = (Resources.Load("icon/notice/" + ModelBase<notice_model>.getInstance().notice[i].icon, typeof(Sprite)) as Sprite);
								this.notice_tt = DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 3600 + ModelBase<notice_model>.getInstance().notice[i].last;
								this.notice_w = true;
								this.notic_v = current;
								this.notic_i = i;
							}
						}
					}
				}
			}
			else
			{
				bool flag4 = ModelBase<notice_model>.getInstance().notice[this.notic_i].time[this.notic_v] != 0f;
				if (flag4)
				{
					ModelBase<notice_model>.getInstance().notice[this.notic_i].time[this.notic_v] = 0f;
				}
				bool flag5 = this.notice_tt == DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 3600;
				if (flag5)
				{
					bool activeSelf = this.CangBaoTu.activeSelf;
					if (activeSelf)
					{
						this.CangBaoTu.SetActive(false);
					}
					this.notice_w = false;
				}
			}
		}

		private int GetBattery()
		{
			bool flag = Application.platform == RuntimePlatform.Android;
			int result;
			if (flag)
			{
				try
				{
					string s = File.ReadAllText("/sys/class/power_supply/battery/capacity");
					result = int.Parse(s);
					return result;
				}
				catch (Exception)
				{
					Debug.LogError("获取不到电量");
				}
			}
			result = 100;
			return result;
		}

		public static int BatteryState()
		{
			return a3_liteMinimap.CallStatic("GetBattery", "BatteryState", new object[0]);
		}

		public static int CallStatic(string className, string methodName, params object[] args)
		{
			int result;
			try
			{
				string className2 = "com.example.asgardgame.androidnative.GetBattery";
				AndroidJavaObject androidJavaObject = new AndroidJavaObject(className2, new object[0]);
				int num = androidJavaObject.CallStatic<int>(methodName, args);
				result = num;
				return result;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
			}
			result = -1;
			return result;
		}

		public static int BatteryLevel()
		{
			return a3_liteMinimap.CallStatic("GetBattery", "BatteryLevel", new object[0]);
		}

		private void BatteryValue()
		{
			this.dianLiangNew = this.batt.localScale.x;
			int num = a3_liteMinimap.BatteryLevel();
			bool flag = num != -1;
			if (flag)
			{
				this.batt.localScale = new Vector3((float)num / 100f, 1f, 0f);
				this.dianLiang.SetActive(true);
			}
			else
			{
				this.dianLiang.SetActive(false);
			}
			bool flag2 = a3_liteMinimap.BatteryState() == 1;
			if (flag2)
			{
				bool flag3 = !this.chongDian.activeSelf;
				if (flag3)
				{
					this.chongDian.SetActive(true);
				}
			}
			else
			{
				bool activeSelf = this.chongDian.activeSelf;
				if (activeSelf)
				{
					this.chongDian.SetActive(false);
				}
			}
		}

		private void onSeeClick(GameObject go)
		{
			bool active = this.imgSee.active;
			if (active)
			{
				this.imgSee.SetActive(false);
			}
			else
			{
				this.imgSee.SetActive(true);
			}
		}

		public override void onShowed()
		{
			this.function_open(this.fun_i);
			this.initm_minimap_camara();
			TickMgr.instance.addTick(this.tick);
			a3_liteMinimap.camGo.SetActive(false);
			BaseProxy<A3_TaskProxy>.getInstance().addEventListener(1u, new Action<GameEvent>(this.OnSubmitTask));
			BaseProxy<A3_TaskProxy>.getInstance().addEventListener(3u, new Action<GameEvent>(this.OnRefreshTask));
			BaseProxy<A3_TaskProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(this.OnAddNewTask));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_LEAVETEAM, new Action<GameEvent>(this.onLeaveTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_DISSOLVETEAM, new Action<GameEvent>(this.onLeaveTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_CREATETEAM, new Action<GameEvent>(this.onCreatTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_AFFIRMINVITE, new Action<GameEvent>(this.onBeInvite));
			BaseProxy<welfareProxy>.getInstance().addEventListener(welfareProxy.SHOWFIRSTRECHARGE, new Action<GameEvent>(this.onShowFirstRecharge));
			ModelBase<PlayerModel>.getInstance().addEventListener(PlayerModel.ON_ATTR_CHANGE, new Action<GameEvent>(this.refreshEquip));
			BaseProxy<ResetLvLProxy>.getInstance().resetLvL();
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.ONEQUIP, new Action<GameEvent>(this.refreshEquip));
			BaseProxy<LotteryProxy>.getInstance().addEventListener(LotteryProxy.LOADLOTTERY, new Action<GameEvent>(this.ShowFreeDrawAvaible));
			BaseProxy<A3_RankProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(this.showAchi));
			ModelBase<A3_TaskModel>.getInstance().addEventListener(1u, new Action<GameEvent>(this.OnTopShowSiblingIndexSub));
			BaseProxy<TaskProxy>.getInstance().addEventListener(1u, new Action<GameEvent>(this.CheckLock));
			BaseProxy<PlayerInfoProxy>.getInstance().addEventListener(PlayerInfoProxy.EVENT_SELF_ON_LV_CHANGE, new Action<GameEvent>(this.CheckLock));
			BaseProxy<A3_signProxy>.getInstance().addEventListener(A3_signProxy.SIGNorREPAIR, new Action<GameEvent>(this.singorrepair));
			BaseProxy<A3_signProxy>.getInstance().addEventListener(A3_signProxy.SIGNINFO, new Action<GameEvent>(this.refreshSign));
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_ITEM_CHANGE, new Action<GameEvent>(this.OnItemChanged));
			BaseProxy<a3_dartproxy>.getInstance().addEventListener(4u, new Action<GameEvent>(this.dartHP));
			this.OnTaskInfoChange();
			this.refreshEquip(null);
			this.refreshYGexp();
			this.petRenew();
		}

		private void showAchi(GameEvent e)
		{
			long num = NetClient.instance.CurServerTimeStampMS - this.nowtime;
			bool flag = e.data.ContainsKey("changed");
			if (flag)
			{
				List<SXML> list = new List<SXML>();
				this.nowtime = NetClient.instance.CurServerTimeStampMS;
				bool flag2 = num < 100L || num == NetClient.instance.CurServerTimeStampMS;
				if (flag2)
				{
					this.achi.Add(e.data["changed"][0]["id"]);
					this.achi.Sort();
					base.StartCoroutine(this.wait(list, this.nowtime, this.achi));
				}
				else
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(base.getGameObjectByPath("achievement"));
					gameObject.transform.SetParent(base.getTransformByPath("achi"));
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
					this.achi.Clear();
					list = XMLMgr.instance.GetSXMLList("achievement.achievement", "achievement_id==" + e.data["changed"][0]["id"]);
					gameObject.transform.FindChild("Text").GetComponent<Text>().text = list[0].getString("name");
					gameObject.transform.FindChild("achipnt/Text").GetComponent<Text>().text = list[0].getInt("point").ToString();
					gameObject.SetActive(true);
					gameObject.name = list[0].getString("name");
					UnityEngine.Object.Destroy(gameObject, 1f);
				}
			}
		}

		private void petRenew()
		{
			bool showrenew = A3_PetModel.showrenew;
			if (showrenew)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_PET_RENEW, null, false);
				a3_pet_renew expr_22 = a3_pet_renew.instance;
				if (expr_22 != null)
				{
					expr_22.transform.SetAsLastSibling();
				}
			}
		}

		private IEnumerator wait(List<SXML> xml, long nowtime, List<int> achis)
		{
			int num;
			for (int i = 0; i < achis.Count; i = num + 1)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.getGameObjectByPath("achievement"));
				gameObject.transform.SetParent(this.getTransformByPath("achi"));
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				xml = XMLMgr.instance.GetSXMLList("achievement.achievement", "achievement_id==" + achis[i]);
				bool flag = xml.Count > 0;
				if (flag)
				{
					gameObject.transform.FindChild("Text").GetComponent<Text>().text = xml[0].getString("name");
					gameObject.transform.FindChild("achipnt/Text").GetComponent<Text>().text = xml[0].getInt("point").ToString();
					gameObject.name = xml[0].getString("name");
					gameObject.SetActive(true);
				}
				yield return new WaitForSeconds(1f);
				UnityEngine.Object.Destroy(gameObject.gameObject, 0.5f);
				gameObject = null;
				num = i;
			}
			achis.Clear();
			yield break;
		}

		private void dartHP(GameEvent e)
		{
			Variant singleMapConf = SvrMapConfig.instance.getSingleMapConf(e.data["map_id"]);
			base.getTransformByPath("goonDart/map_name").GetComponent<Text>().text = singleMapConf["map_name"];
			base.getTransformByPath("goonDart/dartHP").GetComponent<Text>().text = "血量：" + e.data["hp_per"] + "%";
		}

		private void singorrepair(GameEvent e)
		{
		}

		private void refreshSign(GameEvent e)
		{
		}

		private void CheckLock(GameEvent e)
		{
			this.CheckLock();
		}

		private void ShowFreeDrawAvaible(GameEvent e)
		{
		}

		public void initm_minimap_camara()
		{
			a3_liteMinimap.camGo = GameObject.Find("camera_minimap(Clone)");
			bool flag = this.m_minimap_camara == null;
			if (flag)
			{
				GameObject original = Resources.Load("camera/camera_minimap") as GameObject;
				a3_liteMinimap.camGo = UnityEngine.Object.Instantiate<GameObject>(original);
				Application.DontDestroyOnLoad(a3_liteMinimap.camGo);
			}
			this.m_minimap_camara = a3_liteMinimap.camGo.transform.FindChild("camera").GetComponent<Camera>();
			RectTransform component = GameObject.Find("camcon").GetComponent<RectTransform>();
			Vector3 position = component.position;
			float x = (Baselayer.uiWidth + component.anchoredPosition.x - component.rect.width) / Baselayer.uiWidth;
			float y = (Baselayer.uiHeight + component.anchoredPosition.y - component.rect.height) / Baselayer.uiHeight;
			Vector3 vector = InterfaceMgr.ui_Camera_cam.WorldToScreenPoint(position);
			this.m_minimap_camara.rect = new UnityEngine.Rect(x, y, component.sizeDelta.x / Baselayer.uiWidth, component.sizeDelta.y / Baselayer.uiHeight);
			this.refreshMiniCam();
		}

		public void refreshMiniCam()
		{
			bool flag = this.m_minimap_camara == null || SceneCamera.m_curCamGo == null;
			if (!flag)
			{
				float num = SceneCamera.m_curCamGo.transform.transform.eulerAngles.y % 360f;
				Vector3 eulerAngles = this.m_minimap_camara.transform.eulerAngles;
				bool flag2 = num < 90f;
				if (flag2)
				{
					a3_liteMinimap.usrAnglesOffset = (eulerAngles.y = 90f);
				}
				else
				{
					bool flag3 = num < 180f;
					if (flag3)
					{
						a3_liteMinimap.usrAnglesOffset = (eulerAngles.y = 180f);
					}
					else
					{
						bool flag4 = num < 270f;
						if (flag4)
						{
							a3_liteMinimap.usrAnglesOffset = (eulerAngles.y = 270f);
						}
						else
						{
							a3_liteMinimap.usrAnglesOffset = (eulerAngles.y = 0f);
						}
					}
				}
				this.m_minimap_camara.transform.eulerAngles = eulerAngles;
			}
		}

		public void refreshMapname()
		{
		}

		public void refreshYGexp()
		{
			bool flag = ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id < 0;
			if (!flag)
			{
				debug.Log("这里" + ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id);
				bool flag2 = ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id > 9;
				if (flag2)
				{
					base.transform.FindChild("taskinfo/bar").gameObject.SetActive(false);
					base.transform.FindChild("taskinfo/skin/view").GetComponent<RectTransform>().anchoredPosition = base.transform.FindChild("taskinfo/pos0").GetComponent<RectTransform>().anchoredPosition;
				}
				else
				{
					base.transform.FindChild("taskinfo/bar").gameObject.SetActive(true);
					base.transform.FindChild("taskinfo/skin/view").GetComponent<RectTransform>().anchoredPosition = base.transform.FindChild("taskinfo/pos1").GetComponent<RectTransform>().anchoredPosition;
					this.YGname.text = ModelBase<a3_ygyiwuModel>.getInstance().GetYiWu_God(ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id).name;
					int needexp = ModelBase<a3_ygyiwuModel>.getInstance().GetYiWu_God(ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id).needexp;
					this.accentExp_Image.fillAmount = (float)ModelBase<PlayerModel>.getInstance().accent_exp / (float)needexp;
					this.Godicon.sprite = (Resources.Load("icon/ar_smallicon/" + ModelBase<a3_ygyiwuModel>.getInstance().GetYiWu_God(ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id).iconid, typeof(Sprite)) as Sprite);
					bool flag3 = ModelBase<PlayerModel>.getInstance().accent_exp >= needexp;
					if (flag3)
					{
						this.accentExp_text.text = "点击挑战神王";
					}
					else
					{
						float num = (float)ModelBase<PlayerModel>.getInstance().accent_exp / (float)needexp;
						this.accentExp_text.text = num * 100f + "%";
					}
				}
			}
		}

		private void onYGfb(GameObject go)
		{
			bool flag = ModelBase<PlayerModel>.getInstance().accent_exp >= ModelBase<a3_ygyiwuModel>.getInstance().GetYiWu_God(ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id).needexp;
			if (flag)
			{
				MsgBoxMgr.getInstance().showTask_fb_confirm(ModelBase<a3_ygyiwuModel>.getInstance().GetYiWu_God(ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id).fbBox_title, ModelBase<a3_ygyiwuModel>.getInstance().GetYiWu_God(ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id).fbBox_dec, true, ModelBase<a3_ygyiwuModel>.getInstance().GetYiWu_God(ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id).need_zdl, delegate
				{
					this.toYGfb();
				}, null);
			}
			else
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_YGYIWU, null, false);
			}
		}

		private void onCreatTeam(GameEvent e)
		{
			base.getGameObjectByPath("taskinfo/skin/team/createam").SetActive(false);
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.teamPanel);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_CURRENTTEAMINFO, arrayList, false);
			bool flag = this.select == 0;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_CURRENTTEAMINFO);
			}
		}

		private void onBeInvite(GameEvent e)
		{
			base.getGameObjectByPath("taskinfo/skin/team/createam").SetActive(false);
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.teamPanel);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_CURRENTTEAMINFO, arrayList, false);
			bool flag = this.select == 0;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_CURRENTTEAMINFO);
			}
		}

		private void toYGfb()
		{
			Debug.Log("Enter");
			Variant variant = new Variant();
			variant["mapid"] = 3334;
			variant["npcid"] = 0;
			variant["ltpid"] = ModelBase<a3_ygyiwuModel>.getInstance().nowGodFB_id;
			variant["diff_lvl"] = 1;
			BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
		}

		private void setTextPos()
		{
			bool flag = SelfRole._inst == null || SelfRole._inst.m_curModel == null;
			if (!flag)
			{
				string text = string.Format(this.strPos, (int)SelfRole._inst.m_curModel.position.x, (int)SelfRole._inst.m_curModel.position.z);
				InterfaceMgr.doCommandByLua("a3_litemap.setTextPos", "ui/interfaces/floatui/a3_litemap", new object[]
				{
					text
				});
			}
		}

		public void refreshByUIState()
		{
			bool flag = MapModel.getInstance().curLevelId > 0u;
			if (flag)
			{
				bool flag2 = GameRoomMgr.getInstance().curRoom is PlotRoom;
				if (flag2)
				{
					base.transform.FindChild("taskinfo/bar").gameObject.SetActive(false);
				}
				bool flag3 = !(GameRoomMgr.getInstance().curRoom is PlotRoom);
				if (flag3)
				{
					base.transform.FindChild("taskinfo").gameObject.SetActive(false);
				}
				base.transform.FindChild("taskinfo/bar").gameObject.SetActive(false);
				float x = base.transform.FindChild("taskinfo/skin").GetComponent<RectTransform>().anchoredPosition.x;
				base.transform.FindChild("taskinfo/skin").GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 48f);
			}
			else
			{
				base.transform.FindChild("taskinfo").gameObject.SetActive(true);
				bool flag4 = base.transform.FindChild("taskinfo/title/panelTab1/btn_equiped").GetComponent<Button>().interactable && ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id <= 9;
				if (flag4)
				{
					base.transform.FindChild("taskinfo/bar").gameObject.SetActive(true);
				}
				float x2 = base.transform.FindChild("taskinfo/skin").GetComponent<RectTransform>().anchoredPosition.x;
				base.transform.FindChild("taskinfo/skin").GetComponent<RectTransform>().anchoredPosition = new Vector2(x2, 0f);
				this.isTaskBtnShow = false;
				this.OnTaskClickShow(null);
				this.refreshYGexp();
			}
		}

		public void CheckLock()
		{
		}

		public void OpenActive()
		{
		}

		public void OpenFB()
		{
		}

		public void removeRoleInMiniMap(string iid)
		{
			bool flag = this.dMonInMinimap.ContainsKey(iid);
			if (flag)
			{
				GameObject obj = this.dMonInMinimap[iid];
				this.dMonInMinimap.Remove(iid);
				UnityEngine.Object.Destroy(obj);
			}
		}

		public void onWorldMap(GameObject go)
		{
			bool flag = GRMap.curSvrConf.ContainsKey("maptype") && GRMap.curSvrConf["maptype"] > 0;
			if (flag)
			{
				flytxt.instance.fly(ContMgr.getCont("worldmap_cantopen", null), 0, default(Color), null);
			}
			else
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.WORLD_MAP, null, false);
			}
		}

		private void onBtnEnterLottery(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_LOTTERY, null, false);
		}

		private void onBtnAwardCenterClick(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_AWARDCENTER, null, false);
		}

		private void onBtnFBClick(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, null, false);
		}

		public override void onClosed()
		{
			BaseProxy<LotteryProxy>.getInstance().removeEventListener(LotteryProxy.LOTTERYTIP_FREEDRAW, new Action<GameEvent>(this.ShowFreeDrawAvaible));
			BaseProxy<A3_TaskProxy>.getInstance().removeEventListener(1u, new Action<GameEvent>(this.OnSubmitTask));
			BaseProxy<A3_TaskProxy>.getInstance().removeEventListener(3u, new Action<GameEvent>(this.OnRefreshTask));
			BaseProxy<A3_TaskProxy>.getInstance().removeEventListener(2u, new Action<GameEvent>(this.OnAddNewTask));
			BaseProxy<TeamProxy>.getInstance().removeEventListener(TeamProxy.EVENT_LEAVETEAM, new Action<GameEvent>(this.onLeaveTeam));
			BaseProxy<TeamProxy>.getInstance().removeEventListener(TeamProxy.EVENT_DISSOLVETEAM, new Action<GameEvent>(this.onLeaveTeam));
			BaseProxy<welfareProxy>.getInstance().removeEventListener(welfareProxy.SHOWFIRSTRECHARGE, new Action<GameEvent>(this.onShowFirstRecharge));
			TickMgr.instance.removeTick(this.tick);
			BaseProxy<A3_TaskProxy>.getInstance().removeEventListener(1u, new Action<GameEvent>(this.CheckLock));
			BaseProxy<PlayerInfoProxy>.getInstance().removeEventListener(PlayerInfoProxy.EVENT_SELF_ON_LV_CHANGE, new Action<GameEvent>(this.CheckLock));
			ModelBase<A3_TaskModel>.getInstance().removeEventListener(1u, new Action<GameEvent>(this.OnTopShowSiblingIndexSub));
		}

		private void onActive(GameObject go)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add("pvp");
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList, false);
		}

		private void onBtnFirstRechargeClick(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_FIRESTRECHARGEAWARD, null, false);
		}

		private void onBtnShopClick(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.SHOP_A3, null, false);
		}

		private void onSee(GameObject go)
		{
		}

		private void onMoneyDraw(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_EXCHANGE, null, false);
		}

		private void onAutoPlay(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_AUTOPLAY2, null, false);
		}

		private void onBtnMonthCardClick(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_SIGN, null, false);
		}

		private void onBtnCsethClick(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE_GODLIGHT, null, false);
		}

		private void onranking(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_RANKING, null, false);
		}

		public void clear()
		{
			foreach (GameObject current in this.dMonInMinimap.Values)
			{
				UnityEngine.Object.Destroy(current);
			}
			this.dMonInMinimap.Clear();
		}

		private void onUpdate(float s)
		{
			bool loading = GRMap.loading;
			if (!loading)
			{
				bool flag = a3_liteMinimap.camGo == null || SelfRole._inst == null || SelfRole._inst.m_curModel == null;
				if (!flag)
				{
					a3_liteMinimap.camGo.transform.position = SelfRole._inst.m_curModel.position;
					Dictionary<uint, ProfessionRole> mapOtherPlayer = OtherPlayerMgr._inst.m_mapOtherPlayer;
					this.goUser.transform.localEulerAngles = new Vector3(0f, 0f, a3_liteMinimap.usrAnglesOffset - SelfRole._inst.m_curModel.eulerAngles.y);
				}
			}
		}

		public void InitTaskInfo()
		{
			this.OnTaskInfoChange();
		}

		private void OnTaskInfoChange()
		{
			Transform container = this.transTask.FindChild("skin/view/con");
			GameObject gameObject = this.transTask.FindChild("skin/pageTemp").gameObject;
			A3_TaskModel a3_TaskModel = ModelBase<A3_TaskModel>.getInstance();
			Dictionary<int, TaskData> dicTaskData = a3_TaskModel.GetDicTaskData();
			foreach (GameObject current in this.dicTaskPage.Values)
			{
				UnityEngine.Object.Destroy(current);
			}
			this.dicTaskPage.Clear();
			foreach (TaskData current2 in dicTaskData.Values)
			{
				this.OnCreateTaskPage(current2, gameObject, container);
			}
		}

		public void RefreshTaskPage(int taskId)
		{
			bool flag = this.dicTaskPage.ContainsKey(taskId);
			if (flag)
			{
				this.dicTaskPage[taskId].transform.SetSiblingIndex(this.currentTopShowSiblingIndex);
			}
			RectTransform component = this.transTask.FindChild("skin/view/con").GetComponent<RectTransform>();
			component.anchoredPosition = new Vector2(component.anchoredPosition.x, 0f);
			bool flag2 = this.dicTaskPage.Count > 3;
			if (flag2)
			{
				this.goDownwardArrow.SetActive(true);
			}
			else
			{
				this.goDownwardArrow.SetActive(false);
			}
		}

		private void OnCreateTaskPage(TaskData data, GameObject pageTemp, Transform container)
		{
			A3_TaskModel a3_TaskModel = ModelBase<A3_TaskModel>.getInstance();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(pageTemp);
			int taskT = (int)data.taskT;
			Text component = gameObject.transform.FindChild("name").GetComponent<Text>();
			Text component2 = gameObject.transform.FindChild("name/count").GetComponent<Text>();
			Text component3 = gameObject.transform.FindChild("name/title").GetComponent<Text>();
			Text component4 = gameObject.transform.FindChild("desc").GetComponent<Text>();
			GameObject gameObject2 = gameObject.transform.FindChild("guide_task_info").gameObject;
			component.text = data.taskName;
			string text = a3_TaskModel.GetTaskDesc(data.taskId, data.isComplete) + this.GetCountStr(data.taskId);
			component4.text = text;
			bool guide = data.guide;
			if (guide)
			{
				this.has_guide_show = gameObject.transform.FindChild("guide_task_info").gameObject;
				bool flag = !MsgBoxMgr.isShow_guide;
				if (flag)
				{
					gameObject2.SetActive(true);
				}
			}
			else
			{
				gameObject2.SetActive(false);
				this.has_guide_show = null;
			}
			component3.text = a3_TaskModel.GetTaskTypeStr(data.taskT);
			bool flag2 = data.taskT == TaskType.MAIN;
			if (flag2)
			{
				bool flag3 = container.FindChild("page_main") != null;
				if (flag3)
				{
					UnityEngine.Object.Destroy(container.FindChild("page_main").gameObject);
				}
			}
			gameObject.transform.SetParent(container, false);
			gameObject.SetActive(true);
			this.dicTaskPage[data.taskId] = gameObject;
			bool flag4 = data.taskT == TaskType.MAIN;
			if (flag4)
			{
				gameObject.name = "page_main";
				gameObject.transform.SetAsFirstSibling();
			}
			else
			{
				gameObject.transform.SetSiblingIndex(taskT);
			}
			bool flag5 = data.taskT == TaskType.CLAN;
			if (flag5)
			{
				component2.gameObject.SetActive(true);
				component2.text = string.Format("({0}/10)", 1 + data.taskCount);
			}
			BaseButton baseButton = new BaseButton(gameObject.transform, 1, 1);
			int taskid = data.taskId;
			baseButton.onClick = delegate(GameObject <obj>)
			{
				a3_task_auto.instance.stopAuto = false;
				this.OnTaskInfoClick(taskid);
				bool flag6 = a3_expbar.instance != null;
				if (flag6)
				{
					a3_expbar.instance.On_Btn_Down();
				}
				InterfaceMgr.doCommandByLua("a3_litemap_btns.setToggle", "ui/interfaces/floatui/a3_litemap_btns", new object[]
				{
					true
				});
			};
		}

		public void SetTopShow(int taskId)
		{
			bool flag = !this.dicTaskPage.ContainsKey(taskId);
			if (!flag)
			{
				this.dicTaskPage[taskId].transform.SetSiblingIndex(this.currentTopShowSiblingIndex);
				this.currentTopShowSiblingIndex++;
			}
		}

		public void setGuide()
		{
			bool isShow_guide = MsgBoxMgr.isShow_guide;
			if (isShow_guide)
			{
				bool flag = this.has_guide_show != null;
				if (flag)
				{
					this.has_guide_show.SetActive(false);
				}
			}
			else
			{
				bool flag2 = this.has_guide_show != null;
				if (flag2)
				{
					this.has_guide_show.SetActive(true);
				}
			}
		}

		public Transform GetEntrustTaskPage()
		{
			List<int> list = new List<int>(this.dicTaskPage.Keys);
			Transform result;
			for (int i = 0; i < list.Count; i++)
			{
				TaskData expr_28 = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(list[i]);
				TaskType taskType = (expr_28 != null) ? expr_28.taskT : TaskType.NULL;
				bool flag = taskType == TaskType.ENTRUST;
				if (flag)
				{
					result = this.dicTaskPage[list[i]].transform;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Transform GetClanTaskPage()
		{
			List<int> list = new List<int>(this.dicTaskPage.Keys);
			Transform result;
			for (int i = 0; i < list.Count; i++)
			{
				TaskData expr_28 = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(list[i]);
				TaskType taskType = (expr_28 != null) ? expr_28.taskT : TaskType.NULL;
				bool flag = taskType == TaskType.CLAN;
				if (flag)
				{
					result = this.dicTaskPage[list[i]].transform;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Transform GetTaskPage(int taskId)
		{
			List<int> list = new List<int>(this.dicTaskPage.Keys);
			Transform result;
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = list[i] == taskId;
				if (flag)
				{
					result = this.dicTaskPage[list[i]].transform;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Transform GetEntrustTaskPage(out int taskId)
		{
			List<int> list = new List<int>(this.dicTaskPage.Keys);
			taskId = -1;
			Transform result;
			for (int i = 0; i < list.Count; i++)
			{
				TaskData expr_2B = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(list[i]);
				TaskType taskType = (expr_2B != null) ? expr_2B.taskT : TaskType.NULL;
				bool flag = taskType == TaskType.ENTRUST;
				if (flag)
				{
					taskId = list[i];
					result = this.dicTaskPage[list[i]].transform;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Transform GetClanTaskPage(out int taskId)
		{
			List<int> list = new List<int>(this.dicTaskPage.Keys);
			taskId = -1;
			Transform result;
			for (int i = 0; i < list.Count; i++)
			{
				TaskData expr_2B = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(list[i]);
				TaskType taskType = (expr_2B != null) ? expr_2B.taskT : TaskType.NULL;
				bool flag = taskType == TaskType.CLAN;
				if (flag)
				{
					taskId = list[i];
					result = this.dicTaskPage[list[i]].transform;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Transform GetDailyTaskPage(out int taskId)
		{
			List<int> list = new List<int>(this.dicTaskPage.Keys);
			taskId = -1;
			Transform result;
			for (int i = 0; i < list.Count; i++)
			{
				TaskData expr_2B = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(list[i]);
				TaskType taskType = (expr_2B != null) ? expr_2B.taskT : TaskType.NULL;
				bool flag = taskType == TaskType.DAILY;
				if (flag)
				{
					taskId = list[i];
					result = this.dicTaskPage[list[i]].transform;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Transform GetDailyTaskPage()
		{
			List<int> list = new List<int>(this.dicTaskPage.Keys);
			Transform result;
			for (int i = 0; i < list.Count; i++)
			{
				TaskData expr_28 = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(list[i]);
				TaskType taskType = (expr_28 != null) ? expr_28.taskT : TaskType.NULL;
				bool flag = taskType == TaskType.DAILY;
				if (flag)
				{
					result = this.dicTaskPage[list[i]].transform;
					return result;
				}
			}
			result = null;
			return result;
		}

		private void OnTaskInfoClick(int id)
		{
			A3_TaskModel a3_TaskModel = ModelBase<A3_TaskModel>.getInstance();
			a3_TaskModel.curTask = a3_TaskModel.GetTaskDataById(id);
			this.task_id = a3_TaskModel.curTask;
			bool flag = a3_TaskModel.curTask.taskT != TaskType.MAIN && a3_TaskModel.curTask.isComplete;
			if (flag)
			{
				int completeWay = a3_TaskModel.curTask.completeWay;
				if (completeWay != 1)
				{
					if (completeWay == 2)
					{
						this.OnOpenTaskWindow(a3_TaskModel.curTask.taskId);
					}
				}
				else
				{
					this.OnAutoMove(a3_TaskModel.curTask);
				}
			}
			else
			{
				this.OnAutoMove(a3_TaskModel.curTask);
			}
		}

		private void OnOpenTaskWindow(int taskId)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(taskId);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_TASK, arrayList, false);
		}

		private void OnRefreshTask(GameEvent e)
		{
			A3_TaskModel a3_TaskModel = ModelBase<A3_TaskModel>.getInstance();
			Variant data = e.data;
			List<Variant> arr = data["change_task"]._arr;
			foreach (Variant current in arr)
			{
				int num = current["id"];
				TaskData taskDataById = a3_TaskModel.GetTaskDataById(num);
				GameObject gameObject = this.dicTaskPage[num];
				Text component = gameObject.transform.FindChild("desc").GetComponent<Text>();
				string text = a3_TaskModel.GetTaskDesc(num, taskDataById.isComplete) + this.GetCountStr(num);
				component.text = text;
				bool flag = taskDataById.isComplete && !SelfRole.s_bInTransmit && taskDataById.targetType != TaskTargetType.GETEXP;
				if (flag)
				{
					SelfRole.fsm.Stop();
					a3_task_auto.instance.RunTask(taskDataById, true, false);
				}
			}
			bool flag2 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.COUNTERPART, false);
			if (flag2)
			{
				this.OpenFB();
			}
		}

		private void OnAutoMove(TaskData taskData)
		{
			worldmap.Desmapimg();
			SelfRole.fsm.Stop();
			a3_task_auto.instance.RunTask(taskData, false, true);
		}

		public void SetTaskInfoVisible(bool visible)
		{
			base.transform.FindChild("taskinfo").gameObject.SetActive(visible);
			if (visible)
			{
				this.OnTaskClickShow(null);
			}
			else
			{
				this.OnTaskClickClose(null);
			}
		}

		private void OnTaskClickShow(GameObject go)
		{
			bool flag = this.isTaskBtnShow;
			if (!flag)
			{
				this.isTaskBtnShow = true;
				this.taskAnim.SetBool("onoff", true);
				this.btnTask_close.gameObject.SetActive(true);
				this.btnTask.gameObject.SetActive(false);
			}
		}

		private void OnTaskClickClose(GameObject go)
		{
			bool flag = !this.isTaskBtnShow;
			if (!flag)
			{
				this.isTaskBtnShow = false;
				this.taskAnim.SetBool("onoff", false);
				this.btnTask_close.gameObject.SetActive(false);
				this.btnTask.gameObject.SetActive(true);
			}
		}

		private string GetCountStr(int id)
		{
			A3_TaskModel a3_TaskModel = ModelBase<A3_TaskModel>.getInstance();
			TaskData taskDataById = a3_TaskModel.GetTaskDataById(id);
			string result = string.Empty;
			bool flag = !taskDataById.isComplete;
			if (flag)
			{
				int taskRate = taskDataById.taskRate;
				int completion = taskDataById.completion;
				bool flag2 = taskDataById.targetType == TaskTargetType.GET_ITEM_GIVEN;
				if (flag2)
				{
					int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)taskDataById.completionAim);
					int num = completion - taskRate;
					bool flag3 = num > 0;
					if (flag3)
					{
						result = string.Concat(new object[]
						{
							"(",
							itemNumByTpid,
							"/",
							num,
							")"
						});
					}
					else
					{
						result = string.Concat(new object[]
						{
							"(",
							taskRate,
							"/",
							completion,
							")"
						});
					}
				}
				else
				{
					result = string.Concat(new object[]
					{
						"(",
						taskRate,
						"/",
						completion,
						")"
					});
				}
			}
			return result;
		}

		private void OnAddNewTask(GameEvent e)
		{
			Transform container = this.transTask.FindChild("skin/view/con");
			GameObject gameObject = this.transTask.FindChild("skin/pageTemp").gameObject;
			List<TaskData> listAddTask = ModelBase<A3_TaskModel>.getInstance().listAddTask;
			foreach (TaskData current in listAddTask)
			{
				bool flag = current.taskT == TaskType.DAILY;
				if (flag)
				{
					int num;
					Transform expr_63 = this.GetDailyTaskPage(out num);
					UnityEngine.Object.Destroy((expr_63 != null) ? expr_63.gameObject : null);
					bool flag2 = num != -1;
					if (flag2)
					{
						this.dicTaskPage.Remove(num);
						ModelBase<A3_TaskModel>.getInstance().GetDicTaskData().Remove(num);
					}
				}
				else
				{
					bool flag3 = current.taskT == TaskType.ENTRUST;
					if (flag3)
					{
						int num;
						Transform expr_C4 = this.GetEntrustTaskPage(out num);
						UnityEngine.Object.Destroy((expr_C4 != null) ? expr_C4.gameObject : null);
						bool flag4 = num != -1;
						if (flag4)
						{
							this.dicTaskPage.Remove(num);
							ModelBase<A3_TaskModel>.getInstance().GetDicTaskData().Remove(num);
						}
					}
					else
					{
						bool flag5 = current.taskT == TaskType.CLAN;
						if (flag5)
						{
							int num;
							Transform expr_125 = this.GetClanTaskPage(out num);
							UnityEngine.Object.Destroy((expr_125 != null) ? expr_125.gameObject : null);
							bool flag6 = num != -1;
							if (flag6)
							{
								this.dicTaskPage.Remove(num);
								ModelBase<A3_TaskModel>.getInstance().GetDicTaskData().Remove(num);
							}
						}
						else
						{
							bool flag7 = this.dicTaskPage.ContainsKey(current.taskId);
							if (flag7)
							{
								UnityEngine.Object.Destroy(this.dicTaskPage[current.taskId]);
							}
						}
					}
				}
				bool flag8 = current.taskId > 0;
				if (flag8)
				{
					this.OnCreateTaskPage(current, gameObject, container);
				}
			}
			bool flag9 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.COUNTERPART, false);
			if (flag9)
			{
				this.OpenFB();
			}
		}

		private void OnSubmitTask(GameEvent e)
		{
			this.function_open(this.fun_i);
			int key = e.data["id"];
			bool flag = this.dicTaskPage.ContainsKey(key);
			if (flag)
			{
				UnityEngine.Object.Destroy(this.dicTaskPage[key]);
				this.dicTaskPage.Remove(key);
			}
		}

		private void OnTopShowSiblingIndexSub(GameEvent e)
		{
			bool flag = this.currentTopShowSiblingIndex > 0;
			if (flag)
			{
				this.currentTopShowSiblingIndex--;
			}
		}

		public void jf()
		{
		}

		public void SubmitTask(int taskId)
		{
			bool flag = this.dicTaskPage.ContainsKey(taskId);
			if (flag)
			{
				UnityEngine.Object.Destroy(this.dicTaskPage[taskId]);
				this.dicTaskPage.Remove(taskId);
			}
		}

		private void onLeaveTeam(GameEvent e)
		{
			this.tabCtrl1.setSelectedIndex(1, false);
		}

		private void onTab1(TabControl t)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_CURRENTTEAMINFO);
			this.taskPanel.gameObject.SetActive(false);
			bool flag = t.getSeletedIndex() == 0;
			if (flag)
			{
				this.taskPanel.gameObject.SetActive(true);
				base.getGameObjectByPath("taskinfo/skin/team/createam").SetActive(false);
				bool flag2 = this.oldtab1 == 1;
				if (flag2)
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_TASK, null, false);
					this.oldtab1 = 0;
				}
				else
				{
					this.oldtab1 = 1;
				}
				this.select = 0;
				bool flag3 = ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id <= 9;
				if (flag3)
				{
					base.getGameObjectByPath("taskinfo/bar").SetActive(true);
				}
				else
				{
					base.getGameObjectByPath("taskinfo/bar").SetActive(false);
				}
				this.oldtab = 1;
				bool flag4 = GameRoomMgr.getInstance().curRoom is PlotRoom;
				if (flag4)
				{
					base.transform.FindChild("taskinfo/bar").gameObject.SetActive(false);
				}
				this.oldtab1 = 1;
				this.CheckArrow();
			}
			else
			{
				this.select = 1;
				this.oldtab1 = 0;
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.teamPanel);
				bool flag5 = !a3_currentTeamPanel.leave;
				if (flag5)
				{
					bool flag6 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null && this.oldtab != 3;
					if (flag6)
					{
						base.getGameObjectByPath("taskinfo/skin/team/createam").SetActive(false);
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_CURRENTTEAMINFO, arrayList, false);
						ArrayList arrayList2 = new ArrayList();
						arrayList2.Add(2);
						bool flag7 = this.oldtab == 1;
						if (flag7)
						{
							this.oldtab = 2;
						}
						else
						{
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, arrayList2, false);
						}
					}
					else
					{
						bool flag8 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
						if (flag8)
						{
							base.getGameObjectByPath("taskinfo/skin/team/createam").SetActive(false);
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_CURRENTTEAMINFO, arrayList, false);
						}
						else
						{
							base.getGameObjectByPath("taskinfo/skin/team/createam").SetActive(true);
							new BaseButton(base.getTransformByPath("taskinfo/skin/team/createam/go"), 1, 1).onClick = delegate(GameObject go)
							{
								BaseProxy<TeamProxy>.getInstance().SendCreateTeam(0);
								base.getGameObjectByPath("taskinfo/skin/team/createam").SetActive(false);
							};
							BaseButton arg_282_0 = new BaseButton(base.getTransformByPath("taskinfo/skin/team/createam/speedteam"), 1, 1);
							Action<GameObject> arg_282_1;
							if ((arg_282_1 = a3_liteMinimap.<>c.<>9__152_1) == null)
							{
								arg_282_1 = (a3_liteMinimap.<>c.<>9__152_1 = new Action<GameObject>(a3_liteMinimap.<>c.<>9.<onTab1>b__152_1));
							}
							arg_282_0.onClick = arg_282_1;
						}
						ArrayList arrayList3 = new ArrayList();
						arrayList3.Add(2);
						bool flag9 = this.oldtab == 1;
						if (flag9)
						{
							this.oldtab = 2;
						}
						else
						{
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, arrayList3, false);
						}
					}
				}
				else
				{
					base.getGameObjectByPath("taskinfo/skin/team/createam").SetActive(true);
				}
				base.getGameObjectByPath("taskinfo/bar").SetActive(false);
				this.scrlrectTaskPanel.StopMovement();
				this.goUpwardArrow.SetActive(false);
				this.goDownwardArrow.SetActive(false);
			}
		}

		private void onShowFirstRecharge(GameEvent e)
		{
		}

		private void onSuccessGetFirstRechargeAward(GameEvent e)
		{
		}

		public void CheckLock4Screamingbox()
		{
		}

		public void OpenMH()
		{
		}

		public void CheckFirstRecharge()
		{
			BaseProxy<welfareProxy>.getInstance().sendWelfare(welfareProxy.ActiveType.selfWelfareInfo, 4294967295u);
			BaseProxy<welfareProxy>.getInstance().addEventListener(welfareProxy.SUCCESSGETFIRSTRECHARGEAWARD, new Action<GameEvent>(this.onSuccessGetFirstRechargeAward));
		}

		public void updateUICseth()
		{
		}

		public void showActiveIcon(bool open, uint time)
		{
		}

		private void activeTimeGo()
		{
		}

		public void refreshEquip(GameEvent e)
		{
			Dictionary<uint, a3_BagItemData> equips = ModelBase<a3_EquipModel>.getInstance().getEquips();
			bool flag = false;
			foreach (uint current in equips.Keys)
			{
				bool flag2 = !ModelBase<a3_EquipModel>.getInstance().checkCanEquip(equips[current].confdata, equips[current].equipdata.stage, equips[current].equipdata.blessing_lv);
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			bool flag3 = flag;
			if (flag3)
			{
				this.equip_no.SetActive(true);
			}
			else
			{
				this.equip_no.SetActive(false);
			}
		}
	}
}
