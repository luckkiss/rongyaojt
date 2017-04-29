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
	internal class a3_summon : Window
	{
		public enum e_Page
		{
			info,
			grade,
			equip,
			skill,
			identify
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_summon.<>c <>9 = new a3_summon.<>c();

			public static Action<GameObject> <>9__40_2;

			public static Action<GameObject> <>9__124_0;

			public static Action<GameObject> <>9__124_1;

			internal void <init>b__40_2(GameObject GameObject)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SUMMON);
			}

			internal void <Jitan_refresh>b__124_0(GameObject go)
			{
				flytxt.instance.fly("召唤失败，没有足够的灵魂碎片", 0, default(Color), null);
			}

			internal void <Jitan_refresh>b__124_1(GameObject go)
			{
				flytxt.instance.fly("召唤失败，没有足够的灵魂碎片", 0, default(Color), null);
			}
		}

		public a3_summon.e_Page curstate = a3_summon.e_Page.info;

		private TabControl m_tabs;

		private bool isPointDown = false;

		private float lastInvokeTime;

		private float interval = 0.2f;

		private Action m_OnLongpress;

		private ScrollControler m_summonListScroll;

		private Transform selectframe;

		private List<Transform> m_contents = new List<Transform>();

		public GameObject m_selectedSummon;

		private a3_BagItemData m_selectedData;

		private GameObject m_avatar_Camera;

		private Dictionary<uint, GameObject> m_summonList_obj = new Dictionary<uint, GameObject>();

		private BaseButton feedexptab;

		private BaseButton feedsmtab;

		private GameObject feedpan;

		private GameObject feedpan2;

		private GameObject lookobj;

		private GameObject useTip;

		private ScrollControler m_identifyListScroll;

		private List<GameObject> identcells = new List<GameObject>();

		private Dictionary<uint, GameObject> m_identifyList = new Dictionary<uint, GameObject>();

		private Transform m_toIdentRoot;

		private Dictionary<GameObject, uint> m_toidentifyList = new Dictionary<GameObject, uint>();

		private List<GameObject> m_toItem = new List<GameObject>();

		public int identicost = 0;

		private a3_BagItemData _integrationSummon;

		private GameObject _integrationSummonObj;

		private GameObject _itIcon;

		private int selectbookid;

		private int m_selectskill;

		private new Text name;

		private Text level;

		private Text need;

		private Text des;

		private Transform content_skill;

		private Transform tra_jitan;

		private TabControl jitan_tabc;

		public static a3_summon instan;

		private int open_view = 0;

		private bool isAdd = false;

		private bool isReduce = false;

		private float addTime = 0.5f;

		private float rateTime = 0f;

		private Dictionary<uint, GameObject> food_obj = new Dictionary<uint, GameObject>();

		private uint curFood_tpid = 0u;

		private Dictionary<uint, GameObject> sm_obj = new Dictionary<uint, GameObject>();

		private int cur_num;

		private Dictionary<int, GameObject> skillbook = new Dictionary<int, GameObject>();

		private int slectKey = 0;

		private List<GameObject> jitanlist = new List<GameObject>();

		public override void init()
		{
			a3_summon.instan = this;
			this.lookobj = base.transform.FindChild("contents/0/avalook").gameObject;
			this.feedpan = base.transform.FindChild("contents/0/feedpan").gameObject;
			this.feedpan2 = base.transform.FindChild("contents/0/feedpan2").gameObject;
			new BaseButton(this.feedpan.transform.FindChild("tach"), 1, 1).onClick = delegate(GameObject go)
			{
				this.feedpan.SetActive(false);
			};
			new BaseButton(this.feedpan2.transform.FindChild("tach"), 1, 1).onClick = delegate(GameObject go)
			{
				this.feedpan2.SetActive(false);
			};
			this.name = base.getComponentByPath<Text>("contents/3/right/name/Text");
			this.level = base.getComponentByPath<Text>("contents/3/right/level/Text");
			this.need = base.getComponentByPath<Text>("contents/3/right/need/Text");
			this.des = base.getComponentByPath<Text>("contents/3/right/des/Text");
			Transform transform = base.transform.FindChild("contents").transform;
			Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform2 = componentsInChildren[i];
				bool flag = transform2.parent == transform;
				if (flag)
				{
					this.m_contents.Add(transform2);
				}
			}
			BaseButton arg_18A_0 = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			Action<GameObject> arg_18A_1;
			if ((arg_18A_1 = a3_summon.<>c.<>9__40_2) == null)
			{
				arg_18A_1 = (a3_summon.<>c.<>9__40_2 = new Action<GameObject>(a3_summon.<>c.<>9.<init>b__40_2));
			}
			arg_18A_0.onClick = arg_18A_1;
			this.m_summonListScroll = new ScrollControler();
			ScrollRect component = base.transform.FindChild("summonlist/summons/scroll").GetComponent<ScrollRect>();
			this.m_summonListScroll.create(component, 4);
			this.m_identifyListScroll = new ScrollControler();
			this.m_tabs = new TabControl();
			this.m_tabs.onClickHanle = delegate(TabControl t)
			{
				this.SetPages((a3_summon.e_Page)t.getSeletedIndex());
			};
			this.m_tabs.create(base.getGameObjectByPath("tabs"), base.gameObject, 0, 0, false);
			BaseButton baseButton = new BaseButton(base.transform.FindChild("contents/0/actionbtns/1"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.Send_PutSummonInBag);
			this.selectframe = base.transform.FindChild("summonlist/summons/scroll/frame");
			this.content_skill = base.getTransformByPath("contents/3/right/0/list/scroll/content");
			this.useTip = base.transform.FindChild("contents/0/useTip").gameObject;
			EventTriggerListener.Get(this.useTip.transform.FindChild("info/bodyNum/btn_add").gameObject).onDown = delegate(GameObject go)
			{
				this.isAdd = true;
				this.rateTime = 0f;
				this.addTime = 0.5f;
			};
			EventTriggerListener.Get(this.useTip.transform.FindChild("info/bodyNum/btn_add").gameObject).onExit = delegate(GameObject go)
			{
				this.isAdd = false;
			};
			EventTriggerListener.Get(this.useTip.transform.FindChild("info/bodyNum/btn_reduce").gameObject).onDown = delegate(GameObject go)
			{
				this.isReduce = true;
				this.rateTime = 0f;
				this.addTime = 0.5f;
			};
			EventTriggerListener.Get(this.useTip.transform.FindChild("info/bodyNum/btn_reduce").gameObject).onExit = delegate(GameObject go)
			{
				this.isReduce = false;
			};
			new BaseButton(this.useTip.transform.FindChild("info/bodyNum/btn_add"), 1, 1).onClick = new Action<GameObject>(this.onadd);
			new BaseButton(this.useTip.transform.FindChild("info/bodyNum/btn_reduce"), 1, 1).onClick = new Action<GameObject>(this.onreduce);
			new BaseButton(this.useTip.transform.FindChild("info/bodyNum/max"), 1, 1).onClick = new Action<GameObject>(this.onmax);
			new BaseButton(this.useTip.transform.FindChild("info/bodyNum/min"), 1, 1).onClick = new Action<GameObject>(this.onmin);
			new BaseButton(this.useTip.transform.FindChild("touch"), 1, 1).onClick = delegate(GameObject go)
			{
				this.useTip.SetActive(false);
				bool flag2 = this.m_selectedSummon && !this.m_selectedSummon.activeSelf;
				if (flag2)
				{
					this.m_selectedSummon.SetActive(true);
				}
				this.curFood_tpid = 0u;
			};
			this.feedexptab = new BaseButton(base.transform.FindChild("contents/0/feed/tabs/jy"), 1, 1);
			EventTriggerListener.Get(base.transform.FindChild("contents/0/feed/tabs/jy").gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Event_UIFeedEXPClicked);
			EventTriggerListener.Get(base.transform.FindChild("contents/0/left/exp/add").gameObject).onClick = delegate(GameObject go)
			{
				this.feedpan.SetActive(!this.feedpan.activeSelf);
				this.feedpan2.SetActive(false);
				bool activeSelf = this.feedpan.activeSelf;
				if (activeSelf)
				{
					this.Event_UIFeedEXPClicked(go);
				}
			};
			EventTriggerListener.Get(base.transform.FindChild("contents/0/left/lifespan/add").gameObject).onClick = delegate(GameObject go)
			{
				this.feedpan2.SetActive(!this.feedpan2.activeSelf);
				this.feedpan.SetActive(false);
				bool activeSelf = this.feedpan2.activeSelf;
				if (activeSelf)
				{
					this.Event_UIFeedSMClicked(go);
				}
			};
			this.feedsmtab = new BaseButton(base.transform.FindChild("contents/0/feed/tabs/sm"), 1, 1);
			EventTriggerListener.Get(base.transform.FindChild("contents/0/feed/tabs/sm").gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Event_UIFeedSMClicked);
			new BaseButton(base.transform.FindChild("contents/0/actionbtns/2"), 1, 1).onClick = new Action<GameObject>(this.Event_UIGoAttackOrBack);
			new BaseButton(base.transform.FindChild("contents/1/right/frame"), 1, 1).onClick = new Action<GameObject>(this.Event_UIIntegrationList);
			new BaseButton(base.transform.FindChild("contents/1/integration/Panel/btn_close"), 1, 1).onClick = delegate(GameObject g)
			{
				base.transform.FindChild("contents/1/integration/").gameObject.SetActive(false);
				Transform transform3 = base.transform.FindChild("contents/1/integration/Panel/summons/scroll/content");
				Transform[] componentsInChildren2 = transform3.GetComponentsInChildren<Transform>(true);
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Transform transform4 = componentsInChildren2[j];
					bool flag2 = transform4 != null && transform4.parent == transform3.transform;
					if (flag2)
					{
						UnityEngine.Object.DestroyImmediate(transform4.gameObject);
					}
				}
			};
			new BaseButton(base.transform.FindChild("contents/1/actionbtns/0"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag2 = this.m_selectedData.summondata.id > 0 && this._integrationSummon.summondata.id > 0;
				if (flag2)
				{
					bool flag3 = this.m_selectedData.summondata.level < 60;
					if (flag3)
					{
						flytxt.instance.fly(" 主召唤兽等级不满足要求", 0, default(Color), null);
					}
					else
					{
						bool flag4 = (long)this._integrationSummon.summondata.id == (long)((ulong)ModelBase<A3_SummonModel>.getInstance().nowShowAttackID);
						if (flag4)
						{
							flytxt.instance.fly("辅召唤兽已出战", 0, default(Color), null);
						}
						else
						{
							this.Send_Integration(this.m_selectedData.summondata.id, this._integrationSummon.summondata.id);
						}
					}
				}
			};
			new BaseButton(base.transform.FindChild("contents/0/left/look"), 1, 1).onClick = delegate(GameObject go)
			{
				this.lookobj.SetActive(true);
				bool flag2 = this.m_selectedSummon != null;
				if (flag2)
				{
					this.m_selectedSummon.transform.position = new Vector3(-152.93f, 0.77f, 0f);
					this.m_selectedSummon.transform.localScale = new Vector3(2f, 2f, 2f);
					this.m_selectedSummon.transform.localRotation = Quaternion.identity;
					bool flag3 = this.m_selectedData.id > 0u;
					if (flag3)
					{
						base.transform.FindChild("contents/0/avalook/name/Text").GetComponent<Text>().text = this.m_selectedData.summondata.name;
					}
				}
			};
			new BaseButton(base.transform.FindChild("contents/0/avalook/btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.lookobj.SetActive(false);
				bool flag2 = this.m_selectedSummon != null;
				if (flag2)
				{
					this.m_selectedSummon.transform.position = new Vector3(-152.83f, 1.24f, 0f);
					this.m_selectedSummon.transform.localRotation = Quaternion.identity;
					this.m_selectedSummon.transform.localScale = new Vector3(1f, 1f, 1f);
					base.transform.FindChild("contents/0/avalook/name/Text").GetComponent<Text>().text = "";
				}
			};
			EventTriggerListener expr_606 = base.getEventTrigerByPath("contents/0/avalook/avatar_touch");
			expr_606.onDrag = (EventTriggerListener.VectorDelegate)Delegate.Combine(expr_606.onDrag, new EventTriggerListener.VectorDelegate(this.SetAvatarDrag));
			new BaseButton(base.transform.FindChild("btn_sub_jitan"), 1, 1).onClick = new Action<GameObject>(this.ShowJitan);
			new BaseButton(base.transform.FindChild("sub_jitan/Panel/btn_close"), 1, 1).onClick = new Action<GameObject>(this.CloseJitan);
			this.tra_jitan = base.transform.FindChild("sub_jitan");
			this.jitan_tabc = new TabControl();
			this.jitan_tabc.create(base.transform.FindChild("sub_jitan/Panel/tabs").gameObject, base.transform.FindChild("sub_jitan/Panel/contents").gameObject, 0, 0, false);
			this.jitan_tabc.onClickHanle = new Action<TabControl>(this.jitan_onswitch);
			this.Jitan_init();
			new BaseButton(base.getTransformByPath("contents/3/right/0/btn"), 1, 1).onClick = new Action<GameObject>(this.skill_study);
			new BaseButton(base.getTransformByPath("contents/3/right/1/btn"), 1, 1).onClick = new Action<GameObject>(this.skill_forget);
		}

		public override void onShowed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			GRMap.GAME_CAMERA.SetActive(false);
			this.open_view = 0;
			bool flag = this.uiData != null && this.uiData.Count > 0;
			if (flag)
			{
				this.open_view = (int)this.uiData[0];
			}
			this.selectframe.gameObject.SetActive(false);
			this.selectbookid = 0;
			this.SetEvent(true);
			this.ShowSummonList();
			base.transform.SetAsLastSibling();
			this.identicost = 0;
			this.ShowGrade();
			this.feedpan.SetActive(false);
			this.feedpan2.SetActive(false);
			base.transform.FindChild("contents/0/feed").gameObject.SetActive(false);
			this.useTip.SetActive(false);
			this.ShowSelectSummon();
			this.m_tabs.setSelectedIndex(0, false);
			bool flag2 = this.open_view != 0;
			if (flag2)
			{
				int num = this.open_view;
				if (num != 4)
				{
					if (num == 5)
					{
						this.ShowJitan(null);
					}
				}
				else
				{
					this.m_tabs.setSelectedIndex(3, false);
				}
				this.open_view = 0;
			}
			else
			{
				bool flag3 = ModelBase<A3_SummonModel>.getInstance().GetSummons().Count <= 0;
				if (flag3)
				{
					this.ShowJitan(null);
				}
			}
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
			this.selectbookid = 0;
			this.lookobj.SetActive(false);
			this.SetEvent(false);
			this.SetDisposeAvatar();
			foreach (GameObject current in this.m_identifyList.Values)
			{
				UnityEngine.Object.Destroy(current);
			}
			this.m_identifyList.Clear();
			foreach (GameObject current2 in this.m_toItem)
			{
				UnityEngine.Object.Destroy(current2);
			}
			this.m_toItem.Clear();
			this.m_identifyList.Clear();
			this.identicost = 0;
			this.ClearSummonList();
			this.m_selectedData = default(a3_BagItemData);
			this._integrationSummon = default(a3_BagItemData);
			bool flag = this._integrationSummonObj != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this._integrationSummonObj);
			}
			bool flag2 = this._itIcon != null;
			if (flag2)
			{
				UnityEngine.Object.DestroyImmediate(this._itIcon);
			}
		}

		private void Update()
		{
			bool flag = this.isAdd || this.isReduce;
			if (flag)
			{
				this.addTime -= Time.deltaTime;
				bool flag2 = this.addTime < 0f;
				if (flag2)
				{
					this.rateTime += 0.05f;
					this.addTime = 0.5f - this.rateTime;
					bool flag3 = this.isAdd;
					if (flag3)
					{
						this.onadd(null);
					}
					bool flag4 = this.isReduce;
					if (flag4)
					{
						this.onreduce(null);
					}
				}
			}
		}

		private void Event_S2CTEST(GameEvent evt)
		{
			Debug.Log("测试代码执行");
			Variant data = evt.data;
			int num = 0;
			Variant variant = new Variant();
			bool flag = data.ContainsKey("summon");
			if (flag)
			{
				variant = data["summon"];
				num = variant["id"];
			}
			bool flag2 = num <= 0 && data.ContainsKey("summon_id");
			if (flag2)
			{
				num = data["summon_id"];
			}
			int num2 = -1;
			bool flag3 = data.ContainsKey("tp");
			if (flag3)
			{
				num2 = data["tp"];
			}
			this.RefreshSummonList((uint)num);
			switch (num2)
			{
			}
			bool flag4 = this.m_summonList_obj.ContainsKey((uint)num) && num == (int)this.m_selectedData.id;
			if (flag4)
			{
				bool flag5 = this.curstate == a3_summon.e_Page.info;
				if (flag5)
				{
					this.ShowSelectSummon();
				}
				else
				{
					bool flag6 = this.curstate == a3_summon.e_Page.grade;
					if (!flag6)
					{
						bool flag7 = this.curstate == a3_summon.e_Page.skill;
						if (flag7)
						{
							this.ShowSummonSkills();
							this.ShowSkillBooks();
						}
					}
				}
			}
		}

		private void onChuzhan(GameEvent evt)
		{
			Variant data = evt.data;
			bool flag = data.ContainsKey("summon_id");
			if (flag)
			{
				this.RefreshSummonList(data["summon_id"]);
				this.refsumInfo(data["summon_id"]);
			}
		}

		private void onXiuxi(GameEvent evt)
		{
			Variant data = evt.data;
			bool flag = data.ContainsKey("summon_id");
			if (flag)
			{
				this.RefreshSummonList(data["summon_id"]);
				this.refsumInfo(data["summon_id"]);
			}
		}

		private void Event_S2CGetIndentifyMsg(GameEvent evt)
		{
			bool flag = !this.tra_jitan.gameObject.activeSelf;
			if (!flag)
			{
				this.Jitan_refresh(0);
				bool flag2 = ModelBase<A3_SummonModel>.getInstance().GetSummons().Count > 50;
				if (!flag2)
				{
					this.RefreshSummonList();
				}
			}
		}

		private void Event_S2CPutSummonInBag(GameEvent evt)
		{
			Variant data = evt.data;
			int key = data["summon_id"];
			bool flag = this.m_summonList_obj.ContainsKey((uint)key);
			if (flag)
			{
				bool flag2 = this.m_summonList_obj[(uint)key].transform.FindChild("frame") != null;
				if (flag2)
				{
					this.selectframe.gameObject.SetActive(false);
					this.selectframe.SetParent(base.transform.FindChild("summonlist/summons/scroll"));
				}
				UnityEngine.Object.Destroy(this.m_summonList_obj[(uint)key].gameObject);
				this.m_summonList_obj.Remove((uint)key);
				this.m_selectedData = default(a3_BagItemData);
			}
			bool flag3 = this.curstate == a3_summon.e_Page.info;
			if (flag3)
			{
				this.ShowSelectSummon();
			}
			Dictionary<uint, a3_BagItemData> summons = ModelBase<A3_SummonModel>.getInstance().GetSummons();
			this.refsumCount();
			Transform transform = base.transform.FindChild("summonlist/summons/scroll/content").transform;
			float y = transform.GetComponent<GridLayoutGroup>().cellSize.y;
			RectTransform component = transform.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(0f, (float)summons.Count * y);
			this.setSumConVec();
		}

		private void Event_S2CFeedEXP(GameEvent evt)
		{
			Variant data = evt.data;
			int num = 0;
			Variant variant = new Variant();
			bool flag = data.ContainsKey("summon");
			if (flag)
			{
				variant = data["summon"];
				num = variant["id"];
				this.RefreshSummonList((uint)num);
				bool flag2 = this.m_selectedData.summondata.id == num;
				if (flag2)
				{
					this.refsumInfo((uint)num);
				}
			}
			else
			{
				bool flag3 = data.ContainsKey("summon_id");
				if (flag3)
				{
					num = data["summon_id"];
				}
			}
			bool flag4 = this.m_selectedData.summondata.id == num;
			if (flag4)
			{
				this.setexp((uint)num);
			}
		}

		private void Event_S2CFeedSM(GameEvent evt)
		{
			Variant data = evt.data;
			bool flag = data.ContainsKey("summon_id");
			if (flag)
			{
				int num = data["summon_id"];
				bool flag2 = this.m_selectedData.summondata.id == num;
				if (flag2)
				{
					this.refsumInfo((uint)num);
				}
			}
		}

		private void Event_S2CIntegration(GameEvent evt)
		{
			this._integrationSummon = default(a3_BagItemData);
			bool flag = this._integrationSummonObj != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this._integrationSummonObj);
			}
			bool flag2 = this._itIcon != null;
			if (flag2)
			{
				UnityEngine.Object.DestroyImmediate(this._itIcon);
			}
			Variant data = evt.data;
			bool flag3 = data.ContainsKey("summon");
			if (flag3)
			{
				Variant variant = data["summon"];
				uint key = variant["id"];
				this.m_selectedData = ModelBase<A3_SummonModel>.getInstance().GetSummons()[key];
			}
			this.ShowGrade();
			this.RefreshSummonList();
			this.setSumConVec();
		}

		private void onstudySkill(GameEvent e)
		{
			Variant data = e.data;
			this.m_selectskill = data["skill_id"];
			this.m_selectedData = ModelBase<A3_SummonModel>.getInstance().GetSummons()[data["summon_id"]];
			this.selectbookid = 0;
			this.ShowSummonSkills();
			this.ShowSkillBooks();
		}

		private void onforgetSkill(GameEvent e)
		{
			Variant data = e.data;
			bool flag = this.m_selectedData.summondata.id != data["summon_id"];
			if (!flag)
			{
				this.m_selectedData = ModelBase<A3_SummonModel>.getInstance().GetSummons()[data["summon_id"]];
				this.m_selectskill = -1;
				this.ShowSummonSkills();
			}
		}

		private void Send_PutSummonInBag(GameObject obj)
		{
			bool flag = (long)this.m_selectedData.summondata.id != (long)((ulong)ModelBase<A3_SummonModel>.getInstance().nowShowAttackID);
			if (flag)
			{
				BaseProxy<A3_SummonProxy>.getInstance().sendPutSummonInBag((int)this.m_selectedData.id);
			}
			else
			{
				flytxt.instance.fly("该召唤兽已出战", 0, default(Color), null);
			}
		}

		private void Send_IndentifyMsg(GameObject obj)
		{
			List<uint> list = new List<uint>();
			foreach (uint current in this.m_toidentifyList.Values)
			{
				bool flag = current > 0u;
				if (flag)
				{
					list.Add(current);
				}
			}
			bool flag2 = list.Count > 0;
			if (flag2)
			{
				BaseProxy<A3_SummonProxy>.getInstance().sendIdentifySummons(list);
			}
		}

		private void Send_FeedExp(int tpid, int num)
		{
			BaseProxy<A3_SummonProxy>.getInstance().sendFeedExp((int)this.m_selectedData.id, tpid, num);
		}

		private void Send_FeedSM(int tpid, int num)
		{
			BaseProxy<A3_SummonProxy>.getInstance().sendFeedSM((int)this.m_selectedData.id, tpid, num);
		}

		private void Send_Integration(int ida, int idb)
		{
			BaseProxy<A3_SummonProxy>.getInstance().sendIntegration(ida, idb);
		}

		private void Event_UIOneKeyPutIdentBaby(GameObject obj)
		{
			int num = 0;
			List<a3_BagItemData> list = new List<a3_BagItemData>();
			foreach (uint current in this.m_identifyList.Keys)
			{
				bool flag = num++ < this.m_toidentifyList.Count;
				if (flag)
				{
					a3_BagItemData item = ModelBase<a3_BagModel>.getInstance().getItems(false)[current];
					list.Add(item);
				}
			}
			foreach (a3_BagItemData current2 in list)
			{
				this.Event_UIIdentifyLeftBtnClicked(current2);
			}
		}

		private void Event_UIIdentifyLeftBtnClicked(a3_BagItemData data)
		{
			Debug.Log(data.confdata.item_name + "Clicked");
			bool flag = !base.transform.FindChild("contents/4/right/down/cost").gameObject.activeSelf;
			if (!flag)
			{
				GameObject emptyToidentCell = this.GetEmptyToidentCell();
				bool flag2 = emptyToidentCell != null;
				if (flag2)
				{
					GameObject gameObject = this.SetIcon(data, emptyToidentCell.transform, -1);
					this.m_toidentifyList[emptyToidentCell] = data.id;
					this.m_identifyList[data.id].transform.parent.SetAsLastSibling();
					UnityEngine.Object.Destroy(this.m_identifyList[data.id]);
					this.m_identifyList.Remove(data.id);
					this.m_toItem.Add(gameObject);
					BaseButton baseButton = new BaseButton(gameObject.transform, 1, 1);
					List<a3_BagItemData> datas = new List<a3_BagItemData>();
					datas.Add(data);
					baseButton.onClick = delegate(GameObject go)
					{
						this.Event_UIIdentifyRightBtnClicked(datas[0], go);
					};
				}
				this.identicost += ModelBase<A3_SummonModel>.getInstance().appraiseCost;
				base.transform.FindChild("contents/4/right/down/cost/1/Text").GetComponent<Text>().text = "*" + this.identicost;
			}
		}

		private void Event_UIIdentifyRightBtnClicked(a3_BagItemData data, GameObject go)
		{
			Debug.Log(data.confdata.item_name + "Clicked");
			GameObject gameObject = this.SetIcon(data, this.identcells[0].transform.parent.GetChild(this.m_identifyList.Count), -1);
			this.m_identifyList[data.id] = gameObject;
			go.transform.parent.SetAsLastSibling();
			UnityEngine.Object.Destroy(go);
			this.m_toidentifyList[go.transform.parent.gameObject] = 0u;
			this.m_toidentifyList.Remove(go);
			this.m_toItem.Remove(go);
			BaseButton baseButton = new BaseButton(gameObject.transform, 1, 1);
			List<a3_BagItemData> datas = new List<a3_BagItemData>();
			datas.Add(data);
			baseButton.onClick = delegate(GameObject goo)
			{
				this.Event_UIIdentifyLeftBtnClicked(datas[0]);
			};
			this.identicost -= ModelBase<A3_SummonModel>.getInstance().appraiseCost;
			base.transform.FindChild("contents/4/right/down/cost/1/Text").GetComponent<Text>().text = "*" + this.identicost;
		}

		private void Event_UIFeedClicked(GameObject obj)
		{
			Transform transform = base.transform.FindChild("contents/0/feed");
			transform.gameObject.SetActive(!transform.gameObject.activeSelf);
			this.Event_UIFeedEXPClicked(null);
		}

		public void Event_UIFeedEXPClicked(GameObject obj)
		{
			this.useTip.SetActive(false);
			bool flag = this.m_selectedSummon && !this.m_selectedSummon.activeSelf;
			if (flag)
			{
				this.m_selectedSummon.SetActive(true);
			}
			Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
			GameObject gameObject = base.transform.FindChild("contents/0/feedpan/feeditems/scroll/0").gameObject;
			Transform parent = base.transform.FindChild("contents/0/feedpan/feeditems/scroll/content");
			bool flag2 = this.food_obj.Count <= 0;
			if (flag2)
			{
				foreach (int current in ModelBase<A3_SummonModel>.getInstance().feedexplist)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(parent);
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.SetActive(true);
					a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)current);
					GameObject gameObject3 = IconImageMgr.getInstance().createA3ItemIcon(itemDataById, false, -1, 1f, false, -1, 0, false, false, false, -1, false, false);
					gameObject3.transform.SetParent(gameObject2.transform.FindChild("icon"), false);
					gameObject3.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
					uint typeid = (uint)current;
					new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject goo)
					{
						bool flag4 = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(typeid) > 0;
						if (flag4)
						{
							this.curFood_tpid = typeid;
							this.setUseTip();
						}
						else
						{
							flytxt.instance.fly("道具不足", 0, default(Color), null);
						}
					};
					Transform transform = gameObject2.transform.FindChild("Text");
					bool flag3 = transform != null;
					if (flag3)
					{
						Text component = transform.GetComponent<Text>();
						component.text = string.Concat(new object[]
						{
							itemDataById.item_name,
							"\n可以增加",
							itemDataById.main_effect,
							"经验值"
						});
					}
					this.food_obj[(uint)current] = gameObject3;
				}
			}
			foreach (uint current2 in this.food_obj.Keys)
			{
				this.food_obj[current2].transform.FindChild("num").GetComponent<Text>().text = string.Concat(ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(current2));
				this.food_obj[current2].transform.FindChild("num").gameObject.SetActive(true);
			}
		}

		public void Event_UIFeedSMClicked(GameObject obj)
		{
			this.useTip.SetActive(false);
			bool flag = this.m_selectedSummon && !this.m_selectedSummon.activeSelf;
			if (flag)
			{
				this.m_selectedSummon.SetActive(true);
			}
			Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
			GameObject gameObject = base.transform.FindChild("contents/0/feedpan2/feeditems/scroll/0").gameObject;
			Transform parent = base.transform.FindChild("contents/0/feedpan2/feeditems/scroll/content");
			bool flag2 = this.sm_obj.Count <= 0;
			if (flag2)
			{
				foreach (int current in ModelBase<A3_SummonModel>.getInstance().feedsmlist)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(parent);
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.SetActive(true);
					a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)current);
					GameObject gameObject3 = IconImageMgr.getInstance().createA3ItemIcon(itemDataById, false, -1, 1f, false, -1, 0, false, false, false, -1, false, false);
					gameObject3.transform.SetParent(gameObject2.transform.FindChild("icon"), false);
					gameObject3.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
					uint typeid = (uint)current;
					new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject goo)
					{
						bool flag4 = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(typeid) > 0;
						if (flag4)
						{
							this.curFood_tpid = typeid;
							this.setUseTip();
						}
						else
						{
							flytxt.instance.fly("道具不足", 0, default(Color), null);
							bool flag5 = XMLMgr.instance.GetSXML("item.item", "id==" + typeid).GetNode("drop_info", "") == null;
							if (!flag5)
							{
								ArrayList arrayList = new ArrayList();
								arrayList.Add(ModelBase<a3_BagModel>.getInstance().getItemDataById(typeid));
								arrayList.Add(this.uiName);
								bool flag6 = this.m_selectedSummon != null;
								if (flag6)
								{
									arrayList.Add(this.m_selectedSummon);
								}
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMLACK, arrayList, false);
							}
						}
					};
					Transform transform = gameObject2.transform.FindChild("Text");
					bool flag3 = transform != null;
					if (flag3)
					{
						Text component = transform.GetComponent<Text>();
						component.text = string.Concat(new object[]
						{
							itemDataById.item_name,
							"\n可以增加",
							itemDataById.main_effect,
							"寿命"
						});
					}
					this.sm_obj[(uint)current] = gameObject3;
				}
			}
			foreach (uint current2 in this.sm_obj.Keys)
			{
				this.sm_obj[current2].transform.FindChild("num").GetComponent<Text>().text = string.Concat(ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(current2));
				this.sm_obj[current2].transform.FindChild("num").gameObject.SetActive(true);
			}
		}

		private void setUseTip()
		{
			this.useTip.SetActive(true);
			bool flag = this.m_selectedSummon && this.m_selectedSummon.activeSelf;
			if (flag)
			{
				this.m_selectedSummon.SetActive(false);
			}
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(this.curFood_tpid);
			Transform transform = this.useTip.transform.FindChild("info");
			transform.FindChild("name").GetComponent<Text>().text = itemDataById.item_name;
			transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(itemDataById.quality);
			transform.FindChild("desc").GetComponent<Text>().text = StringUtils.formatText(itemDataById.desc);
			bool flag2 = itemDataById.use_limit > 0;
			if (flag2)
			{
				transform.FindChild("lv").GetComponent<Text>().text = string.Concat(new object[]
				{
					itemDataById.use_limit,
					"转",
					itemDataById.use_lv,
					"级"
				});
			}
			else
			{
				transform.FindChild("lv").GetComponent<Text>().text = "无限制";
			}
			Transform transform2 = transform.FindChild("icon");
			bool flag3 = transform2.childCount > 0;
			if (flag3)
			{
				UnityEngine.Object.Destroy(transform2.GetChild(0).gameObject);
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(itemDataById, false, -1, 1f, false, -1, 0, false, false, false, -1, false, false);
			gameObject.transform.SetParent(transform2, false);
			this.cur_num = 1;
			this.refreshNum();
			new BaseButton(this.useTip.transform.FindChild("info/use"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag4 = ModelBase<A3_SummonModel>.getInstance().feedexplist.Contains((int)this.curFood_tpid);
				if (flag4)
				{
					this.Send_FeedExp((int)this.curFood_tpid, this.cur_num);
				}
				else
				{
					bool flag5 = ModelBase<A3_SummonModel>.getInstance().feedsmlist.Contains((int)this.curFood_tpid);
					if (flag5)
					{
						this.Send_FeedSM((int)this.curFood_tpid, this.cur_num);
					}
				}
				this.feedpan.SetActive(false);
				this.feedpan2.SetActive(false);
				this.useTip.SetActive(false);
				bool flag6 = this.m_selectedSummon && !this.m_selectedSummon.activeSelf;
				if (flag6)
				{
					this.m_selectedSummon.SetActive(true);
				}
			};
		}

		private void refreshNum()
		{
			bool flag = this.cur_num == 0;
			if (flag)
			{
				this.cur_num = 1;
			}
			this.useTip.transform.FindChild("info/bodyNum/donum").GetComponent<Text>().text = this.cur_num.ToString();
			this.useTip.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (ModelBase<a3_BagModel>.getInstance().getItemDataById(this.curFood_tpid).main_effect * this.cur_num).ToString();
			string text = "";
			bool flag2 = ModelBase<A3_SummonModel>.getInstance().feedexplist.Contains((int)this.curFood_tpid);
			if (flag2)
			{
				text = "增加经验";
			}
			else
			{
				bool flag3 = ModelBase<A3_SummonModel>.getInstance().feedsmlist.Contains((int)this.curFood_tpid);
				if (flag3)
				{
					text = "增加寿命";
				}
			}
			this.useTip.transform.FindChild("info/bodyNum/res").GetComponent<Text>().text = text;
		}

		private void onadd(GameObject go)
		{
			this.cur_num++;
			bool flag = this.cur_num >= ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.curFood_tpid);
			if (flag)
			{
				this.cur_num = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.curFood_tpid);
			}
			this.refreshNum();
		}

		private void onreduce(GameObject go)
		{
			this.cur_num--;
			bool flag = this.cur_num < 1;
			if (flag)
			{
				this.cur_num = 1;
			}
			this.refreshNum();
		}

		private void onmin(GameObject obj)
		{
			this.cur_num = 1;
			this.refreshNum();
		}

		private void onmax(GameObject obj)
		{
			this.cur_num = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.curFood_tpid);
			this.refreshNum();
		}

		private void Event_UIGoAttackOrBack(GameObject obj)
		{
			bool flag = this.m_selectedData.id == 0u;
			if (!flag)
			{
				bool flag2 = this.m_selectedData.id == ModelBase<A3_SummonModel>.getInstance().nowShowAttackID;
				if (flag2)
				{
					BaseProxy<A3_SummonProxy>.getInstance().sendGoBack((int)this.m_selectedData.id);
				}
				else
				{
					bool flag3 = ModelBase<A3_SummonModel>.getInstance().getSumCds().ContainsKey((int)this.m_selectedData.id);
					if (flag3)
					{
						flytxt.instance.fly("召唤兽出战cd中", 0, default(Color), null);
					}
					else
					{
						BaseProxy<A3_SummonProxy>.getInstance().sendGoAttack((int)this.m_selectedData.id);
					}
				}
			}
		}

		private void Event_UIIntegrationList(GameObject obj)
		{
			base.transform.FindChild("contents/1/integration").gameObject.SetActive(true);
			Transform transform = base.transform.FindChild("contents/1/integration/Panel/summons/scroll/content");
			transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, transform.GetComponent<RectTransform>().anchoredPosition.y);
			GameObject gameObject = base.transform.FindChild("contents/1/integration/Panel/summons/scroll/0").gameObject;
			List<a3_BagItemData> list = new List<a3_BagItemData>();
			Dictionary<uint, a3_BagItemData> summons = ModelBase<A3_SummonModel>.getInstance().GetSummons();
			foreach (a3_BagItemData current in summons.Values)
			{
				bool flag = current.summondata.level >= 60 && current.summondata.blood == this.m_selectedData.summondata.blood && current.summondata.id != this.m_selectedData.summondata.id;
				if (flag)
				{
					list.Add(current);
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(transform);
					gameObject2.gameObject.SetActive(true);
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.transform.localPosition = Vector3.zero;
					gameObject2.transform.FindChild("name").GetComponent<Text>().text = "名称：" + current.summondata.name;
					gameObject2.transform.FindChild("lv").GetComponent<Text>().text = "等级：" + current.summondata.level;
					gameObject2.transform.FindChild("blood").GetComponent<Text>().text = "血脉：" + ((current.summondata.blood > 1) ? "光" : "暗");
					string arg_218_0 = current.summondata.isSpecial ? "变异" : "";
					gameObject2.transform.FindChild("grade").GetComponent<Text>().text = "类型：" + ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(current.summondata.grade);
					gameObject2.transform.FindChild("speciality").GetComponent<Text>().text = "专精：" + ModelBase<A3_SummonModel>.getInstance().IntNaturalToStr(current.summondata.naturaltype);
					this.SetStar(gameObject2.transform.FindChild("stars"), current.summondata.star);
					int tpid = current.summondata.tpid;
					SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + tpid);
					gameObject2.transform.FindChild("icon").GetComponent<Image>().sprite = (Resources.Load("icon/item/" + sXML.getString("icon_file"), typeof(Sprite)) as Sprite);
					List<a3_BagItemData> temp = new List<a3_BagItemData>();
					temp.Add(current);
					new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject gobtn)
					{
						bool flag2 = this._integrationSummonObj != null;
						if (flag2)
						{
							UnityEngine.Object.DestroyImmediate(this._integrationSummonObj);
						}
						bool flag3 = this._itIcon != null;
						if (flag3)
						{
							UnityEngine.Object.DestroyImmediate(this._itIcon);
						}
						this.transform.FindChild("contents/1/right/frame/add").gameObject.SetActive(false);
						this._integrationSummonObj = this.SetIcon(temp[0], this.transform.FindChild("contents/1/right/frame"), -1);
						this._integrationSummon = temp[0];
						Transform transform2 = this.transform.FindChild("contents/1/integration/Panel/summons/scroll/content");
						this.transform.FindChild("contents/1/integration/").gameObject.SetActive(false);
						Transform[] componentsInChildren = transform2.GetComponentsInChildren<Transform>(true);
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							Transform transform3 = componentsInChildren[i];
							bool flag4 = transform3.parent == transform2.transform;
							if (flag4)
							{
								UnityEngine.Object.Destroy(transform3.gameObject);
							}
						}
						this.ShowGrade();
					};
				}
			}
			float x = transform.GetComponent<GridLayoutGroup>().cellSize.x;
			float x2 = transform.GetComponent<GridLayoutGroup>().spacing.x;
			transform.GetComponent<RectTransform>().sizeDelta = new Vector2((float)list.Count * x + (float)(list.Count - 1) * x2, 0f);
		}

		private void ClearSummonList()
		{
			this.selectframe.gameObject.SetActive(false);
			this.selectframe.SetParent(base.transform.FindChild("summonlist/summons/scroll"));
			foreach (GameObject current in this.m_summonList_obj.Values)
			{
				UnityEngine.Object.DestroyImmediate(current.gameObject);
			}
			this.m_summonList_obj.Clear();
		}

		private void ClearSkillBooks()
		{
			Transform[] componentsInChildren = this.content_skill.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent == this.content_skill.transform;
				if (flag)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
		}

		private void ShowSummonList()
		{
			Dictionary<uint, a3_BagItemData> summons = ModelBase<A3_SummonModel>.getInstance().GetSummons();
			CanvasGroup component = base.transform.FindChild("contents/0").GetComponent<CanvasGroup>();
			bool flag = component != null;
			if (flag)
			{
				bool flag2 = summons.Count > 0;
				if (flag2)
				{
					component.alpha = 1f;
				}
				else
				{
					component.alpha = 0f;
				}
			}
			GameObject gameObject = base.transform.FindChild("summonlist/summons/scroll/0").gameObject;
			Transform content = base.transform.FindChild("summonlist/summons/scroll/content");
			foreach (a3_BagItemData current in summons.Values)
			{
				this.SetNewInList(current, content, gameObject);
			}
			this.refsumCount();
			Transform transform = base.transform.FindChild("summonlist/summons/scroll/content").transform;
			float y = transform.GetComponent<GridLayoutGroup>().cellSize.y;
			RectTransform component2 = transform.GetComponent<RectTransform>();
			component2.sizeDelta = new Vector2(0f, (float)summons.Count * y);
		}

		private void refsumCount()
		{
			base.transform.FindChild("summonlist/num").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().GetSummons().Count + "/50";
		}

		private void ShowSelectSummon()
		{
			Dictionary<uint, a3_BagItemData> summons = ModelBase<A3_SummonModel>.getInstance().GetSummons();
			bool flag = summons.Count > 0;
			if (flag)
			{
				List<a3_BagItemData> list = new List<a3_BagItemData>(summons.Values);
				bool flag2 = this.m_selectedData.summondata.id == 0;
				if (flag2)
				{
					this.m_selectedData = list[0];
				}
				else
				{
					this.m_selectedData = summons[(uint)this.m_selectedData.summondata.id];
				}
				this.refsumInfo((uint)this.m_selectedData.summondata.id);
				this.SetCreateAvatar(this.m_selectedData.summondata.objid.ToString(), this.m_selectedData.tpid);
				base.transform.FindChild("contents/0").gameObject.SetActive(true);
			}
			else
			{
				this.SetDisposeAvatar();
				base.transform.FindChild("contents/0").gameObject.SetActive(false);
			}
		}

		private void ShowSummonInfoTip(a3_BagItemData data)
		{
			Debug.Log("Info: " + data.confdata.item_name);
			ArrayList arrayList = new ArrayList();
			arrayList.Add(data);
			arrayList.Add(1);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3TIPS_SUMMON, arrayList, false);
		}

		private void ShowIdentifyList()
		{
			this.identcells.Clear();
			GameObject gameObject = base.transform.FindChild("contents/4/summons/scroll/left").gameObject;
			Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent.gameObject == gameObject;
				if (flag)
				{
					this.identcells.Add(transform.gameObject);
				}
			}
			this.m_toidentifyList.Clear();
			this.m_toIdentRoot = base.transform.FindChild("contents/4/right/up");
			Transform[] componentsInChildren2 = this.m_toIdentRoot.GetComponentsInChildren<Transform>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform2 = componentsInChildren2[j];
				bool flag2 = transform2.parent.gameObject == this.m_toIdentRoot.gameObject;
				if (flag2)
				{
					this.m_toidentifyList[transform2.gameObject] = 0u;
				}
			}
			int num = 0;
			Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
			foreach (a3_BagItemData current in items.Values)
			{
				bool flag3 = !this.m_identifyList.ContainsKey(current.id);
				if (flag3)
				{
					bool flag4 = current.isSummon && ModelBase<A3_SummonModel>.getInstance().IsBaby(current);
					if (flag4)
					{
						GameObject gameObject2 = this.SetIcon(current, this.identcells[num].transform, -1);
						this.m_identifyList[current.id] = gameObject2;
						BaseButton baseButton = new BaseButton(gameObject2.transform, 1, 1);
						List<a3_BagItemData> datas = new List<a3_BagItemData>();
						datas.Add(current);
						baseButton.onClick = delegate(GameObject go)
						{
							this.Event_UIIdentifyLeftBtnClicked(datas[0]);
						};
						num++;
					}
				}
			}
		}

		private void ShowGrade()
		{
			bool flag = this.m_selectedData.id <= 0u;
			if (!flag)
			{
				this.m_selectedData = ModelBase<A3_SummonModel>.getInstance().GetSummons()[(uint)this.m_selectedData.summondata.id];
				base.transform.FindChild("contents/1").GetComponent<CanvasGroup>().alpha = 1f;
				Transform transform = base.transform.FindChild("contents/1/left/icon");
				bool flag2 = transform.childCount > 0;
				if (flag2)
				{
					for (int i = 0; i < transform.childCount; i++)
					{
						UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
					}
				}
				this.SetIcon(this.m_selectedData, transform, -1);
				base.transform.FindChild("contents/1/left/info/0/text").GetComponent<Text>().text = this.m_selectedData.summondata.name;
				base.transform.FindChild("contents/1/left/info/1/text").GetComponent<Text>().text = this.m_selectedData.summondata.level + "级";
				base.transform.FindChild("contents/1/left/info/2/text").GetComponent<Text>().text = ((this.m_selectedData.summondata.blood > 1) ? "光" : "暗");
				base.transform.FindChild("contents/1/left/info/3/text").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(this.m_selectedData.summondata.grade);
				bool flag3 = this._integrationSummon.summondata.id <= 0;
				if (flag3)
				{
					bool flag4 = this._integrationSummonObj != null;
					if (flag4)
					{
						UnityEngine.Object.DestroyImmediate(this._integrationSummonObj);
					}
					base.transform.FindChild("contents/1/right/frame/add").gameObject.SetActive(true);
					base.transform.FindChild("contents/1/right/alert").gameObject.SetActive(true);
					base.transform.FindChild("contents/1/right/info").gameObject.SetActive(false);
					base.transform.FindChild("contents/1/right/mix").gameObject.SetActive(false);
					bool flag5 = this.m_selectedData.id > 0u;
					if (flag5)
					{
						base.transform.FindChild("contents/1/left/atts").gameObject.SetActive(true);
						base.transform.FindChild("contents/1/left/evaluation").gameObject.SetActive(true);
					}
				}
				else
				{
					base.transform.FindChild("contents/1/right/frame/add").gameObject.SetActive(false);
					base.transform.FindChild("contents/1/right/alert").gameObject.SetActive(false);
					base.transform.FindChild("contents/1/right/info").gameObject.SetActive(true);
					base.transform.FindChild("contents/1/right/mix").gameObject.SetActive(true);
					base.transform.FindChild("contents/1/left/atts").gameObject.SetActive(true);
					base.transform.FindChild("contents/1/left/evaluation").gameObject.SetActive(true);
				}
				base.transform.FindChild("contents/1/right/info/0/text").GetComponent<Text>().text = this._integrationSummon.summondata.name;
				base.transform.FindChild("contents/1/right/info/1/text").GetComponent<Text>().text = this._integrationSummon.summondata.level + "级";
				base.transform.FindChild("contents/1/right/info/2/text").GetComponent<Text>().text = ((this._integrationSummon.summondata.blood > 1) ? "光" : "暗");
				base.transform.FindChild("contents/1/right/info/3/text").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(this._integrationSummon.summondata.grade);
				Vector4 talentTypeMax = ModelBase<A3_SummonModel>.getInstance().GetTalentTypeMax(this.m_selectedData.summondata.talent_type);
				float num = (float)this.m_selectedData.summondata.attNatural / talentTypeMax.x;
				float num2 = (float)this.m_selectedData.summondata.defNatural / talentTypeMax.y;
				float num3 = (float)this.m_selectedData.summondata.agiNatural / talentTypeMax.z;
				float num4 = (float)this.m_selectedData.summondata.conNatural / talentTypeMax.w;
				base.transform.FindChild("contents/1/left/atts/0/frame/true").GetComponent<Image>().fillAmount = num;
				base.transform.FindChild("contents/1/left/atts/1/frame/true").GetComponent<Image>().fillAmount = num2;
				base.transform.FindChild("contents/1/left/atts/2/frame/true").GetComponent<Image>().fillAmount = num3;
				base.transform.FindChild("contents/1/left/atts/3/frame/true").GetComponent<Image>().fillAmount = num4;
				base.transform.FindChild("contents/1/left/atts/0/frame/fake").GetComponent<Image>().fillAmount = 0f;
				base.transform.FindChild("contents/1/left/atts/1/frame/fake").GetComponent<Image>().fillAmount = 0f;
				base.transform.FindChild("contents/1/left/atts/2/frame/fake").GetComponent<Image>().fillAmount = 0f;
				base.transform.FindChild("contents/1/left/atts/3/frame/fake").GetComponent<Image>().fillAmount = 0f;
				base.transform.FindChild("contents/1/left/atts/0/exp").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.m_selectedData.summondata.attNatural,
					"(+",
					0,
					")"
				});
				base.transform.FindChild("contents/1/left/atts/1/exp").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.m_selectedData.summondata.defNatural,
					"(+",
					0,
					")"
				});
				base.transform.FindChild("contents/1/left/atts/2/exp").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.m_selectedData.summondata.agiNatural,
					"(+",
					0,
					")"
				});
				base.transform.FindChild("contents/1/left/atts/3/exp").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.m_selectedData.summondata.conNatural,
					"(+",
					0,
					")"
				});
				bool flag6 = num >= 1f;
				if (flag6)
				{
					base.transform.FindChild("contents/1/left/atts/0/star/light").gameObject.SetActive(true);
				}
				else
				{
					base.transform.FindChild("contents/1/left/atts/0/star/light").gameObject.SetActive(false);
				}
				bool flag7 = num2 >= 1f;
				if (flag7)
				{
					base.transform.FindChild("contents/1/left/atts/1/star/light").gameObject.SetActive(true);
				}
				else
				{
					base.transform.FindChild("contents/1/left/atts/1/star/light").gameObject.SetActive(false);
				}
				bool flag8 = num3 >= 1f;
				if (flag8)
				{
					base.transform.FindChild("contents/1/left/atts/2/star/light").gameObject.SetActive(true);
				}
				else
				{
					base.transform.FindChild("contents/1/left/atts/2/star/light").gameObject.SetActive(false);
				}
				bool flag9 = num4 >= 1f;
				if (flag9)
				{
					base.transform.FindChild("contents/1/left/atts/3/star/light").gameObject.SetActive(true);
				}
				else
				{
					base.transform.FindChild("contents/1/left/atts/3/star/light").gameObject.SetActive(false);
				}
				bool flag10 = this._integrationSummon.summondata.naturaltype == 1;
				if (flag10)
				{
					float num5 = (float)(this._integrationSummon.summondata.attNatural - this.m_selectedData.summondata.attNatural);
					num5 = Mathf.Max(30f, num5 / 1000f * (float)this.m_selectedData.summondata.luck);
					num5 = (float)Mathf.CeilToInt(num5);
					num5 = Mathf.Min(num5, talentTypeMax.x - (float)this.m_selectedData.summondata.attNatural);
					bool flag11 = num5 >= talentTypeMax.x - (float)this.m_selectedData.summondata.attNatural;
					if (flag11)
					{
						base.transform.FindChild("contents/1/left/atts/0/star/light").gameObject.SetActive(true);
					}
					base.transform.FindChild("contents/1/left/atts/0/frame/fake").GetComponent<Image>().fillAmount = (num5 + (float)this.m_selectedData.summondata.attNatural) / talentTypeMax.x;
					base.transform.FindChild("contents/1/left/atts/0/exp").GetComponent<Text>().text = string.Concat(new object[]
					{
						this.m_selectedData.summondata.attNatural,
						"(+",
						num5,
						")"
					});
				}
				else
				{
					bool flag12 = this._integrationSummon.summondata.naturaltype == 2;
					if (flag12)
					{
						float num5 = (float)(this._integrationSummon.summondata.defNatural - this.m_selectedData.summondata.defNatural);
						num5 = Mathf.Max(30f, num5 / 1000f * (float)this.m_selectedData.summondata.luck);
						num5 = (float)Mathf.CeilToInt(num5);
						num5 = Mathf.Min(num5, talentTypeMax.y - (float)this.m_selectedData.summondata.defNatural);
						bool flag13 = num5 >= talentTypeMax.y - (float)this.m_selectedData.summondata.defNatural;
						if (flag13)
						{
							base.transform.FindChild("contents/1/left/atts/1/star/light").gameObject.SetActive(true);
						}
						base.transform.FindChild("contents/1/left/atts/1/frame/fake").GetComponent<Image>().fillAmount = (num5 + (float)this.m_selectedData.summondata.defNatural) / talentTypeMax.y;
						base.transform.FindChild("contents/1/left/atts/1/exp").GetComponent<Text>().text = string.Concat(new object[]
						{
							this.m_selectedData.summondata.defNatural,
							"(+",
							num5,
							")"
						});
					}
					else
					{
						bool flag14 = this._integrationSummon.summondata.naturaltype == 3;
						if (flag14)
						{
							float num5 = (float)(this._integrationSummon.summondata.agiNatural - this.m_selectedData.summondata.agiNatural);
							num5 = Mathf.Max(30f, num5 / 1000f * (float)this.m_selectedData.summondata.luck);
							num5 = (float)Mathf.CeilToInt(num5);
							num5 = Mathf.Min(num5, talentTypeMax.z - (float)this.m_selectedData.summondata.agiNatural);
							bool flag15 = num5 >= talentTypeMax.z - (float)this.m_selectedData.summondata.agiNatural;
							if (flag15)
							{
								base.transform.FindChild("contents/1/left/atts/2/star/light").gameObject.SetActive(true);
							}
							base.transform.FindChild("contents/1/left/atts/2/frame/fake").GetComponent<Image>().fillAmount = (num5 + (float)this.m_selectedData.summondata.agiNatural) / talentTypeMax.z;
							base.transform.FindChild("contents/1/left/atts/2/exp").GetComponent<Text>().text = string.Concat(new object[]
							{
								this.m_selectedData.summondata.agiNatural,
								"(+",
								num5,
								")"
							});
						}
						else
						{
							bool flag16 = this._integrationSummon.summondata.naturaltype == 4;
							if (flag16)
							{
								float num5 = (float)(this._integrationSummon.summondata.conNatural - this.m_selectedData.summondata.conNatural);
								num5 = Mathf.Max(30f, num5 / 1000f * (float)this.m_selectedData.summondata.luck);
								num5 = (float)Mathf.CeilToInt(num5);
								bool flag17 = num5 >= talentTypeMax.w - (float)this.m_selectedData.summondata.conNatural;
								if (flag17)
								{
									base.transform.FindChild("contents/1/left/atts/3/star/light").gameObject.SetActive(true);
								}
								num5 = Mathf.Min(num5, talentTypeMax.w - (float)this.m_selectedData.summondata.conNatural);
								base.transform.FindChild("contents/1/left/atts/3/frame/fake").GetComponent<Image>().fillAmount = (num5 + (float)this.m_selectedData.summondata.conNatural) / talentTypeMax.w;
								base.transform.FindChild("contents/1/left/atts/3/exp").GetComponent<Text>().text = string.Concat(new object[]
								{
									this.m_selectedData.summondata.conNatural,
									"(+",
									num5,
									")"
								});
							}
						}
					}
				}
				this.SetStar(base.transform.FindChild("contents/1/left/evaluation/stars/layout"), this.m_selectedData.summondata.star);
				this.refmoney();
			}
		}

		public void ShowSkillBooks()
		{
			this.ClearSkillBooks();
			this.skillbook.Clear();
			Transform transformByPath = base.getTransformByPath("contents/3/right/0/list/scroll/0");
			foreach (a3_BagItemData current in ModelBase<a3_BagModel>.getInstance().getItems(false).Values)
			{
				bool flag = current.confdata.use_type == 18;
				if (flag)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transformByPath.gameObject);
					gameObject.transform.SetParent(this.content_skill, false);
					gameObject.SetActive(true);
					GameObject gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(current, false, current.num, 1f, false);
					gameObject2.transform.SetParent(gameObject.transform, false);
					List<a3_BagItemData> tt = new List<a3_BagItemData>();
					tt.Add(current);
					UnityEngine.Object arg_F4_0 = gameObject2;
					uint num = current.confdata.tpid;
					arg_F4_0.name = num.ToString();
					UnityEngine.Object arg_10B_0 = gameObject;
					num = current.id;
					arg_10B_0.name = num.ToString();
					new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject go)
					{
						this.skillbookclicked(tt[0].confdata);
						for (int i = 0; i < this.content_skill.childCount; i++)
						{
							this.content_skill.GetChild(i).FindChild("select").gameObject.SetActive(false);
						}
						go.transform.parent.FindChild("select").gameObject.SetActive(true);
						go.transform.SetAsFirstSibling();
					};
					this.skillbook[(int)tt[0].tpid] = gameObject;
				}
			}
			foreach (int current2 in this.skillbook.Keys)
			{
				bool flag2 = current2 == this.selectbookid;
				if (flag2)
				{
					this.skillbook[current2].transform.FindChild("select").gameObject.SetActive(true);
				}
				else
				{
					this.skillbook[current2].transform.FindChild("select").gameObject.SetActive(false);
				}
			}
		}

		private void ShowSummonSkills()
		{
			bool flag = this.m_selectedData.summondata.id <= 0;
			if (!flag)
			{
				base.transform.FindChild("contents/3").GetComponent<CanvasGroup>().alpha = 1f;
				Transform transform = base.transform.FindChild("contents/3/list/scroll/content");
				Transform transform2 = base.transform.FindChild("contents/3/list/scroll/0");
				Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform3 = componentsInChildren[i];
					bool flag2 = transform3.parent == transform.transform;
					if (flag2)
					{
						UnityEngine.Object.Destroy(transform3.gameObject);
					}
				}
				for (int j = 0; j < this.m_selectedData.summondata.skillNum; j++)
				{
					GameObject go = UnityEngine.Object.Instantiate<GameObject>(transform2.gameObject);
					go.transform.SetParent(transform);
					go.transform.localScale = Vector3.one;
					go.SetActive(true);
					bool flag3 = this.m_selectedData.summondata.skills != null && this.m_selectedData.summondata.skills.ContainsKey(j + 1);
					if (flag3)
					{
						go.transform.FindChild("0").gameObject.SetActive(false);
						go.transform.FindChild("1").gameObject.SetActive(true);
						SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + this.m_selectedData.summondata.skills[j + 1]);
						SXML sXML2 = XMLMgr.instance.GetSXML("item.item", "id==" + sXML.getInt("bood_item"));
						go.transform.FindChild("1/name").GetComponent<Text>().text = sXML.getString("name");
						go.transform.FindChild("1/lv").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().IntLvlToStr(sXML2.getInt("skill_level"));
						go.transform.FindChild("1/icon").GetComponent<Image>().sprite = (Resources.Load("icon/smskill/" + sXML.getInt("icon"), typeof(Sprite)) as Sprite);
						go.name = this.m_selectedData.summondata.skills[j + 1].ToString();
					}
					else
					{
						go.transform.FindChild("0").gameObject.SetActive(true);
						go.transform.FindChild("1").gameObject.SetActive(false);
						go.name = "-1";
					}
					go.transform.FindChild("select").gameObject.SetActive(false);
					int tag = j + 1;
					new BaseButton(go.transform, 1, 1).onClick = delegate(GameObject goo)
					{
						this.slectKey = tag;
						bool flag6 = this.m_selectedData.summondata.skills.ContainsKey(this.slectKey);
						if (flag6)
						{
							this.m_selectskill = this.m_selectedData.summondata.skills[this.slectKey];
						}
						else
						{
							this.m_selectskill = -1;
						}
						this.showSkillORbook();
						this.skill_show(go);
					};
					bool flag4 = tag == this.slectKey;
					if (flag4)
					{
						this.skill_show(go);
						bool flag5 = this.m_selectedData.summondata.skills.ContainsKey(this.slectKey);
						if (flag5)
						{
							this.m_selectskill = this.m_selectedData.summondata.skills[this.slectKey];
						}
						else
						{
							this.m_selectskill = -1;
						}
						this.showSkillORbook();
					}
				}
			}
		}

		private void showSkillORbook()
		{
			bool flag = this.m_selectskill < 0;
			if (flag)
			{
				base.transform.FindChild("contents/3/right/0").gameObject.SetActive(true);
				base.transform.FindChild("contents/3/right/1").gameObject.SetActive(false);
				bool flag2 = this.selectbookid <= 0;
				if (flag2)
				{
					this.name.text = "";
					this.level.text = "";
					this.need.text = "";
					this.des.text = "";
				}
				else
				{
					SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + this.selectbookid);
					this.name.text = sXML.getString("item_name");
					this.level.text = ModelBase<A3_SummonModel>.getInstance().IntLvlToStr(sXML.getInt("skill_level"));
					this.need.text = ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(sXML.getInt("skill_level"));
					this.des.text = sXML.getString("desc");
				}
			}
			else
			{
				SXML sXML2 = XMLMgr.instance.GetSXML("skill.skill", "id==" + this.m_selectskill);
				bool flag3 = sXML2 != null;
				if (flag3)
				{
					SXML sXML3 = XMLMgr.instance.GetSXML("item.item", "id==" + sXML2.getInt("bood_item"));
					this.name.text = sXML2.getString("name");
					this.level.text = ModelBase<A3_SummonModel>.getInstance().IntLvlToStr(sXML3.getInt("skill_level"));
					this.need.text = ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(sXML3.getInt("skill_level"));
					this.des.text = sXML2.getString("descr1");
				}
				else
				{
					this.name.text = "";
					this.level.text = "";
					this.need.text = "";
					this.des.text = "";
				}
			}
		}

		private void skill_show(GameObject go)
		{
			Transform[] componentsInChildren = go.transform.parent.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent == go.transform.parent;
				if (flag)
				{
					transform.FindChild("select").gameObject.SetActive(false);
				}
			}
			go.transform.FindChild("select").gameObject.SetActive(true);
			int num = int.Parse(go.name);
			bool flag2 = num < 0;
			if (flag2)
			{
				base.transform.FindChild("contents/3/right/0").gameObject.SetActive(true);
				base.transform.FindChild("contents/3/right/1").gameObject.SetActive(false);
			}
			else
			{
				base.transform.FindChild("contents/3/right/0").gameObject.SetActive(false);
				base.transform.FindChild("contents/3/right/1").gameObject.SetActive(true);
			}
		}

		private void skillbookclicked(a3_ItemData data)
		{
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + data.tpid);
			this.name.text = data.item_name;
			this.level.text = ModelBase<A3_SummonModel>.getInstance().IntLvlToStr(sXML.getInt("skill_level"));
			this.need.text = ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(sXML.getInt("skill_level"));
			this.des.text = data.desc;
			this.selectbookid = (int)data.tpid;
		}

		private void skill_study(GameObject go)
		{
			bool flag = this.selectbookid != 0;
			if (flag)
			{
				bool flag2 = (long)this.m_selectedData.summondata.id == (long)((ulong)ModelBase<A3_SummonModel>.getInstance().nowShowAttackID);
				if (flag2)
				{
					flytxt.instance.fly("该召唤兽已出战", 0, default(Color), null);
				}
				else
				{
					BaseProxy<A3_SummonProxy>.getInstance().sendOPSkill(2, this.m_selectedData.summondata.id, this.selectbookid, this.slectKey);
				}
			}
			else
			{
				flytxt.instance.fly("请选择技能书", 0, default(Color), null);
			}
		}

		private void skill_forget(GameObject go)
		{
			bool flag = this.m_selectskill != 0;
			if (flag)
			{
				bool flag2 = (long)this.m_selectedData.summondata.id == (long)((ulong)ModelBase<A3_SummonModel>.getInstance().nowShowAttackID);
				if (flag2)
				{
					flytxt.instance.fly("该召唤兽已出战", 0, default(Color), null);
				}
				else
				{
					BaseProxy<A3_SummonProxy>.getInstance().sendOPSkill(1, this.m_selectedData.summondata.id, this.m_selectskill, this.slectKey);
				}
			}
			else
			{
				flytxt.instance.fly("请选择技能", 0, default(Color), null);
			}
		}

		private void ShowSkillList()
		{
			bool flag = this.m_selectedData.id <= 0u;
			if (flag)
			{
			}
		}

		private void RefreshSummonList()
		{
			this.ClearSummonList();
			this.ShowSummonList();
		}

		private void refmoney()
		{
			Text component = base.transform.FindChild("contents/1/actionbtns/0/money").GetComponent<Text>();
			component.text = XMLMgr.instance.GetSXML("callbeast.callbeast", "id==" + this.m_selectedData.tpid).getInt("combining_cost").ToString();
		}

		private void RefreshSummonList(uint id)
		{
			this.m_summonList_obj[id].transform.FindChild("name").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().GetSummons()[id].summondata.name;
			this.m_summonList_obj[id].transform.FindChild("lv").GetComponent<Text>().text = "LV." + ModelBase<A3_SummonModel>.getInstance().GetSummons()[id].summondata.level;
			this.m_summonList_obj[id].transform.FindChild("grade").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(ModelBase<A3_SummonModel>.getInstance().GetSummons()[id].summondata.grade);
			this.m_summonList_obj[id].transform.FindChild("attritype").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().IntNaturalToStr(ModelBase<A3_SummonModel>.getInstance().GetSummons()[id].summondata.naturaltype);
			this.m_summonList_obj[id].transform.FindChild("blood").GetComponent<Text>().text = ((ModelBase<A3_SummonModel>.getInstance().GetSummons()[id].summondata.blood > 1) ? "光" : "暗");
			foreach (uint current in this.m_summonList_obj.Keys)
			{
				bool flag = current == ModelBase<A3_SummonModel>.getInstance().nowShowAttackID;
				if (flag)
				{
					this.m_summonList_obj[current].transform.FindChild("fighting").gameObject.SetActive(true);
				}
				else
				{
					this.m_summonList_obj[current].transform.FindChild("fighting").gameObject.SetActive(false);
				}
			}
		}

		private void RefreshIdentifyList(GameEvent e)
		{
			this.onClosed();
			this.onShowed();
		}

		private void setSumConVec()
		{
			RectTransform component = base.transform.FindChild("summonlist/summons/scroll/content").GetComponent<RectTransform>();
			component.anchoredPosition = new Vector2(component.anchoredPosition.x, 0f);
		}

		private void SetNewInList(a3_BagItemData data, Transform content, GameObject go)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(go);
			gameObject.transform.SetParent(content.transform);
			gameObject.SetActive(true);
			gameObject.transform.localScale = Vector3.one;
			Transform parent = gameObject.transform.FindChild("bg");
			GameObject gameObject2 = this.SetIcon(data, parent, -1);
			Text component = gameObject.transform.FindChild("name").GetComponent<Text>();
			component.text = data.summondata.name;
			Text component2 = gameObject.transform.FindChild("lv").GetComponent<Text>();
			component2.text = "LV." + data.summondata.level;
			gameObject.transform.FindChild("attritype").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().IntNaturalToStr(data.summondata.naturaltype);
			gameObject.transform.FindChild("blood").GetComponent<Text>().text = ((data.summondata.blood > 1) ? "光" : "暗");
			bool flag = data.summondata.id == (int)ModelBase<A3_SummonModel>.getInstance().nowShowAttackID;
			if (!flag)
			{
				gameObject.transform.FindChild("fighting").gameObject.SetActive(false);
			}
			Text component3 = gameObject.transform.FindChild("grade").GetComponent<Text>();
			string arg_1BC_0 = data.summondata.isSpecial ? "变异" : "";
			component3.text = ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(data.summondata.grade);
			BaseButton baseButton = new BaseButton(gameObject2.transform, 1, 1);
			List<a3_BagItemData> datas = new List<a3_BagItemData>();
			datas.Add(data);
			baseButton.onClick = delegate(GameObject obj)
			{
				a3_BagItemData a3_BagItemData = ModelBase<A3_SummonModel>.getInstance().GetSummons()[(uint)data.summondata.id];
				bool flag3 = this.m_selectedSummon != null;
				if (flag3)
				{
					this.m_selectedSummon.SetActive(false);
				}
				ArrayList arrayList = new ArrayList();
				arrayList.Add(a3_BagItemData);
				arrayList.Add(1);
				arrayList.Add(new Action(this.<SetNewInList>b__105_1));
				InterfaceMgr.getInstance().open(InterfaceMgr.A3TIPS_SUMMON, arrayList, false);
			};
			new BaseButton(gameObject.transform, 1, 1).onClick = delegate(GameObject g)
			{
				int id = this.m_selectedData.summondata.id;
				this.selectframe.gameObject.SetActive(true);
				this.selectframe.SetParent(g.transform);
				this.selectframe.SetAsFirstSibling();
				this.selectframe.localPosition = Vector3.zero;
				this.m_selectedData = datas[0];
				bool flag3 = this.m_tabs.getSeletedIndex() == 0;
				if (flag3)
				{
					bool flag4 = id != this.m_selectedData.summondata.id;
					if (flag4)
					{
						this.refsumInfo((uint)this.m_selectedData.summondata.id);
						this.SetCreateAvatar(this.m_selectedData.summondata.objid.ToString(), this.m_selectedData.tpid);
					}
					this.feedpan.SetActive(false);
					this.feedpan2.SetActive(false);
				}
				else
				{
					bool flag5 = this.m_tabs.getSeletedIndex() == 1;
					if (flag5)
					{
						this._integrationSummon = default(a3_BagItemData);
						bool flag6 = this._integrationSummonObj != null;
						if (flag6)
						{
							UnityEngine.Object.DestroyImmediate(this._integrationSummonObj);
						}
						bool flag7 = this._itIcon != null;
						if (flag7)
						{
							UnityEngine.Object.DestroyImmediate(this._itIcon);
						}
						this.ShowGrade();
					}
					else
					{
						bool flag8 = this.m_tabs.getSeletedIndex() == 3;
						if (flag8)
						{
							this.slectKey = 1;
							this.ShowSummonSkills();
							this.showSkillORbook();
						}
					}
				}
			};
			bool flag2 = this.m_selectedData.summondata.id == data.summondata.id;
			if (flag2)
			{
				this.selectframe.gameObject.SetActive(true);
				this.selectframe.SetParent(gameObject.transform);
				this.selectframe.SetAsFirstSibling();
				this.selectframe.localPosition = Vector3.zero;
			}
			this.m_summonList_obj[data.id] = gameObject;
		}

		private void SetOpenShowTab()
		{
			bool flag = this.uiData != null && this.uiData.Count != 0;
			if (flag)
			{
				Dictionary<uint, a3_BagItemData> summons = ModelBase<A3_SummonModel>.getInstance().GetSummons();
				bool flag2 = summons.Count > 0;
				if (flag2)
				{
					List<a3_BagItemData> list = new List<a3_BagItemData>(summons.Values);
					bool flag3 = this.m_selectedData.summondata.id == 0;
					if (flag3)
					{
						this.m_selectedData = list[0];
					}
				}
				int index = (int)this.uiData[0];
				this.m_tabs.setSelectedIndex(index, false);
			}
			else
			{
				this.ShowSelectSummon();
				this.m_tabs.setSelectedIndex(0, false);
			}
		}

		private void refsumInfo(uint id)
		{
			bool flag = !ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(id);
			if (!flag)
			{
				a3_BagItemData a3_BagItemData = ModelBase<A3_SummonModel>.getInstance().GetSummons()[id];
				bool flag2 = this.m_summonList_obj.ContainsKey(a3_BagItemData.id);
				if (flag2)
				{
					this.selectframe.gameObject.SetActive(true);
					this.selectframe.SetParent(this.m_summonList_obj[a3_BagItemData.id].transform);
					this.selectframe.SetAsFirstSibling();
					this.selectframe.localPosition = Vector3.zero;
				}
				Text component = base.transform.FindChild("contents/0/right/up/name/Text").GetComponent<Text>();
				component.text = ModelBase<A3_SummonModel>.getInstance().IntNaturalToStr(a3_BagItemData.summondata.naturaltype);
				base.transform.FindChild("contents/0/right/up/att/Text").GetComponent<Text>().text = a3_BagItemData.summondata.attNatural.ToString();
				base.transform.FindChild("contents/0/right/up/def/Text").GetComponent<Text>().text = a3_BagItemData.summondata.defNatural.ToString();
				base.transform.FindChild("contents/0/right/up/agi/Text").GetComponent<Text>().text = a3_BagItemData.summondata.agiNatural.ToString();
				base.transform.FindChild("contents/0/right/up/con/Text").GetComponent<Text>().text = a3_BagItemData.summondata.conNatural.ToString();
				Text component2 = base.transform.FindChild("contents/0/right/up/lv/Text").GetComponent<Text>();
				component2.text = a3_BagItemData.summondata.level.ToString();
				Text component3 = base.transform.FindChild("contents/0/right/up/longlife/Text").GetComponent<Text>();
				component3.text = a3_BagItemData.summondata.lifespan.ToString();
				Text component4 = base.transform.FindChild("contents/0/right/up/blood/Text").GetComponent<Text>();
				component4.text = ((a3_BagItemData.summondata.blood > 1) ? "光" : "暗");
				Text component5 = base.transform.FindChild("contents/0/right/up/luck/Text").GetComponent<Text>();
				component5.text = a3_BagItemData.summondata.luck.ToString();
				Text component6 = base.transform.FindChild("contents/0/right/up/life/Text").GetComponent<Text>();
				component6.text = a3_BagItemData.summondata.maxhp.ToString();
				Text component7 = base.transform.FindChild("contents/0/right/up/phyatk/Text").GetComponent<Text>();
				component7.text = a3_BagItemData.summondata.min_attack.ToString() + "~" + a3_BagItemData.summondata.max_attack.ToString();
				Text component8 = base.transform.FindChild("contents/0/right/up/manaatk/Text").GetComponent<Text>();
				component8.text = a3_BagItemData.summondata.magic_dmg_red.ToString();
				Text component9 = base.transform.FindChild("contents/0/right/up/phydef/Text").GetComponent<Text>();
				component9.text = a3_BagItemData.summondata.physics_def.ToString();
				Text component10 = base.transform.FindChild("contents/0/right/up/manadef/Text").GetComponent<Text>();
				component10.text = a3_BagItemData.summondata.magic_def.ToString();
				Text component11 = base.transform.FindChild("contents/0/right/up/hit/Text").GetComponent<Text>();
				component11.text = a3_BagItemData.summondata.physics_dmg_red.ToString();
				Text component12 = base.transform.FindChild("contents/0/right/up/dodge/Text").GetComponent<Text>();
				component12.text = a3_BagItemData.summondata.dodge.ToString();
				Text component13 = base.transform.FindChild("contents/0/right/up/crit/Text").GetComponent<Text>();
				component13.text = a3_BagItemData.summondata.double_damage_rate.ToString();
				base.transform.FindChild("contents/0/right/up/fatal_damage/Text").GetComponent<Text>().text = a3_BagItemData.summondata.fatal_damage.ToString();
				base.transform.FindChild("contents/0/right/up/hitit/Text").GetComponent<Text>().text = a3_BagItemData.summondata.hit.ToString();
				base.transform.FindChild("contents/0/right/up/reflect_crit_rate/Text").GetComponent<Text>().text = a3_BagItemData.summondata.reflect_crit_rate.ToString();
				Text component14 = base.transform.FindChild("contents/0/left/name/Image/Text").GetComponent<Text>();
				component14.text = a3_BagItemData.summondata.name;
				component14.color = Globle.getColorByQuality(a3_BagItemData.summondata.grade + 1);
				Transform starRoot = base.transform.FindChild("contents/0/left/name/evaluation/stars");
				this.SetStar(starRoot, a3_BagItemData.summondata.star);
				Text component15 = base.transform.FindChild("contents/0/left/power/num").GetComponent<Text>();
				component15.text = a3_BagItemData.summondata.power.ToString();
				Transform transform = base.transform.FindChild("contents/0/left/skills");
				for (int i = 0; i < transform.childCount; i++)
				{
					transform.GetChild(i).FindChild("lock").gameObject.SetActive(true);
					transform.GetChild(i).FindChild("icon").gameObject.SetActive(false);
					bool flag3 = a3_BagItemData.summondata.skills.ContainsKey(i + 1);
					if (flag3)
					{
						SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + a3_BagItemData.summondata.skills[i + 1]);
						transform.GetChild(i).FindChild("icon/icon").GetComponent<Image>().sprite = (Resources.Load("icon/smskill/" + sXML.getInt("icon"), typeof(Sprite)) as Sprite);
						transform.GetChild(i).FindChild("icon").gameObject.SetActive(true);
					}
				}
				for (int j = 0; j < a3_BagItemData.summondata.skillNum; j++)
				{
					transform.GetChild(j).FindChild("lock").gameObject.SetActive(false);
				}
				this.setexp(id);
				Image component16 = base.transform.FindChild("contents/0/left/lifespan/slider").GetComponent<Image>();
				Text component17 = base.transform.FindChild("contents/0/left/lifespan/Text").GetComponent<Text>();
				component17.text = a3_BagItemData.summondata.lifespan + "/" + 100;
				component16.fillAmount = (float)a3_BagItemData.summondata.lifespan / 100f;
				bool flag4 = a3_BagItemData.summondata.id == (int)ModelBase<A3_SummonModel>.getInstance().nowShowAttackID;
				if (flag4)
				{
					Text component18 = base.transform.FindChild("contents/0/actionbtns/2/Text").GetComponent<Text>();
					component18.text = "休息";
				}
				else
				{
					Text component19 = base.transform.FindChild("contents/0/actionbtns/2/Text").GetComponent<Text>();
					component19.text = "出战";
				}
			}
		}

		private void SetInfo()
		{
			this.m_selectedData = ModelBase<A3_SummonModel>.getInstance().GetSummons()[(uint)this.m_selectedData.summondata.id];
			bool flag = this.m_summonList_obj.ContainsKey(this.m_selectedData.id);
			if (flag)
			{
				this.selectframe.gameObject.SetActive(true);
				this.selectframe.SetParent(this.m_summonList_obj[this.m_selectedData.id].transform);
				this.selectframe.SetAsFirstSibling();
				this.selectframe.localPosition = Vector3.zero;
			}
			Text component = base.transform.FindChild("contents/0/right/up/name/Text").GetComponent<Text>();
			component.text = ModelBase<A3_SummonModel>.getInstance().IntNaturalToStr(this.m_selectedData.summondata.naturaltype);
			base.transform.FindChild("contents/0/right/up/att/Text").GetComponent<Text>().text = this.m_selectedData.summondata.attNatural.ToString();
			base.transform.FindChild("contents/0/right/up/def/Text").GetComponent<Text>().text = this.m_selectedData.summondata.defNatural.ToString();
			base.transform.FindChild("contents/0/right/up/agi/Text").GetComponent<Text>().text = this.m_selectedData.summondata.agiNatural.ToString();
			base.transform.FindChild("contents/0/right/up/con/Text").GetComponent<Text>().text = this.m_selectedData.summondata.conNatural.ToString();
			Text component2 = base.transform.FindChild("contents/0/right/up/lv/Text").GetComponent<Text>();
			component2.text = this.m_selectedData.summondata.level.ToString();
			Text component3 = base.transform.FindChild("contents/0/right/up/longlife/Text").GetComponent<Text>();
			component3.text = this.m_selectedData.summondata.lifespan.ToString();
			Text component4 = base.transform.FindChild("contents/0/right/up/blood/Text").GetComponent<Text>();
			component4.text = ((this.m_selectedData.summondata.blood > 1) ? "光" : "暗");
			Text component5 = base.transform.FindChild("contents/0/right/up/luck/Text").GetComponent<Text>();
			component5.text = this.m_selectedData.summondata.luck.ToString();
			Text component6 = base.transform.FindChild("contents/0/right/up/life/Text").GetComponent<Text>();
			component6.text = this.m_selectedData.summondata.maxhp.ToString();
			Text component7 = base.transform.FindChild("contents/0/right/up/phyatk/Text").GetComponent<Text>();
			component7.text = this.m_selectedData.summondata.min_attack.ToString() + "~" + this.m_selectedData.summondata.max_attack.ToString();
			Text component8 = base.transform.FindChild("contents/0/right/up/manaatk/Text").GetComponent<Text>();
			component8.text = this.m_selectedData.summondata.magic_dmg_red.ToString();
			Text component9 = base.transform.FindChild("contents/0/right/up/phydef/Text").GetComponent<Text>();
			component9.text = this.m_selectedData.summondata.physics_def.ToString();
			Text component10 = base.transform.FindChild("contents/0/right/up/manadef/Text").GetComponent<Text>();
			component10.text = this.m_selectedData.summondata.magic_def.ToString();
			Text component11 = base.transform.FindChild("contents/0/right/up/hit/Text").GetComponent<Text>();
			component11.text = this.m_selectedData.summondata.physics_dmg_red.ToString();
			Text component12 = base.transform.FindChild("contents/0/right/up/dodge/Text").GetComponent<Text>();
			component12.text = this.m_selectedData.summondata.dodge.ToString();
			Text component13 = base.transform.FindChild("contents/0/right/up/crit/Text").GetComponent<Text>();
			component13.text = this.m_selectedData.summondata.double_damage_rate.ToString();
			base.transform.FindChild("contents/0/right/up/fatal_damage/Text").GetComponent<Text>().text = this.m_selectedData.summondata.fatal_damage.ToString();
			base.transform.FindChild("contents/0/right/up/hitit/Text").GetComponent<Text>().text = this.m_selectedData.summondata.hit.ToString();
			base.transform.FindChild("contents/0/right/up/reflect_crit_rate/Text").GetComponent<Text>().text = this.m_selectedData.summondata.reflect_crit_rate.ToString();
			Text component14 = base.transform.FindChild("contents/0/left/name/Image/Text").GetComponent<Text>();
			component14.text = this.m_selectedData.summondata.name;
			component14.color = Globle.getColorByQuality(this.m_selectedData.summondata.grade + 1);
			Transform starRoot = base.transform.FindChild("contents/0/left/name/evaluation/stars");
			this.SetStar(starRoot, this.m_selectedData.summondata.star);
			Text component15 = base.transform.FindChild("contents/0/left/power/num").GetComponent<Text>();
			component15.text = this.m_selectedData.summondata.power.ToString();
			Transform transform = base.transform.FindChild("contents/0/left/skills");
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).FindChild("lock").gameObject.SetActive(true);
				transform.GetChild(i).FindChild("icon").gameObject.SetActive(false);
				bool flag2 = this.m_selectedData.summondata.skills.ContainsKey(i + 1);
				if (flag2)
				{
					SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + this.m_selectedData.summondata.skills[i + 1]);
					transform.GetChild(i).FindChild("icon/icon").GetComponent<Image>().sprite = (Resources.Load("icon/smskill/" + sXML.getInt("icon"), typeof(Sprite)) as Sprite);
					transform.GetChild(i).FindChild("icon").gameObject.SetActive(true);
				}
			}
			for (int j = 0; j < this.m_selectedData.summondata.skillNum; j++)
			{
				transform.GetChild(j).FindChild("lock").gameObject.SetActive(false);
			}
			Image component16 = base.transform.FindChild("contents/0/left/lifespan/slider").GetComponent<Image>();
			Text component17 = base.transform.FindChild("contents/0/left/lifespan/Text").GetComponent<Text>();
			component17.text = this.m_selectedData.summondata.lifespan + "/" + 100;
			component16.fillAmount = (float)this.m_selectedData.summondata.lifespan / 100f;
			bool flag3 = this.m_selectedData.summondata.id == (int)ModelBase<A3_SummonModel>.getInstance().nowShowAttackID;
			if (flag3)
			{
				Text component18 = base.transform.FindChild("contents/0/actionbtns/2/Text").GetComponent<Text>();
				component18.text = "休息";
			}
			else
			{
				Text component19 = base.transform.FindChild("contents/0/actionbtns/2/Text").GetComponent<Text>();
				component19.text = "出战";
			}
		}

		private void setexp(uint id)
		{
			bool flag = !ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(id);
			if (!flag)
			{
				a3_BagItemData a3_BagItemData = ModelBase<A3_SummonModel>.getInstance().GetSummons()[id];
				Text component = base.transform.FindChild("contents/0/left/exp/Text").GetComponent<Text>();
				SXML attributeXml = ModelBase<A3_SummonModel>.getInstance().GetAttributeXml(a3_BagItemData.summondata.level);
				int @int = attributeXml.getInt("exp");
				component.text = a3_BagItemData.summondata.currentexp + "/" + @int;
				Image component2 = base.transform.FindChild("contents/0/left/exp/slider").GetComponent<Image>();
				component2.fillAmount = (float)a3_BagItemData.summondata.currentexp / (float)@int;
			}
		}

		private void SetStar(Transform starRoot, int num)
		{
			int num2 = 0;
			Transform[] componentsInChildren = starRoot.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent != null && transform.parent.parent == starRoot.transform;
				if (flag)
				{
					bool flag2 = num2 < num;
					if (flag2)
					{
						transform.gameObject.SetActive(true);
						num2++;
					}
					else
					{
						transform.gameObject.SetActive(false);
					}
				}
			}
		}

		private void SetEvent(bool TrueOrFalse)
		{
			if (TrueOrFalse)
			{
				BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_SHOWIDENTIFYANSWER, new Action<GameEvent>(this.Event_S2CGetIndentifyMsg));
				BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_PUTSUMMONINBAG, new Action<GameEvent>(this.Event_S2CPutSummonInBag));
				BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_UPDATE, new Action<GameEvent>(this.Event_S2CTEST));
				BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_CHUZHAN, new Action<GameEvent>(this.onChuzhan));
				BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_XUEXI, new Action<GameEvent>(this.onstudySkill));
				BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_FORGET, new Action<GameEvent>(this.onforgetSkill));
				BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_XIUXI, new Action<GameEvent>(this.onXiuxi));
				BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_FEEDEXP, new Action<GameEvent>(this.Event_S2CFeedEXP));
				BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_FEEDSM, new Action<GameEvent>(this.Event_S2CFeedSM));
				BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_INTEGRATION, new Action<GameEvent>(this.Event_S2CIntegration));
			}
			else
			{
				BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_SHOWIDENTIFYANSWER, new Action<GameEvent>(this.Event_S2CGetIndentifyMsg));
				BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_PUTSUMMONINBAG, new Action<GameEvent>(this.Event_S2CPutSummonInBag));
				BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_UPDATE, new Action<GameEvent>(this.Event_S2CTEST));
				BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_CHUZHAN, new Action<GameEvent>(this.onChuzhan));
				BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_XUEXI, new Action<GameEvent>(this.onstudySkill));
				BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_XIUXI, new Action<GameEvent>(this.onXiuxi));
				BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_FORGET, new Action<GameEvent>(this.onforgetSkill));
				BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_FEEDEXP, new Action<GameEvent>(this.Event_S2CFeedEXP));
				BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_FEEDSM, new Action<GameEvent>(this.Event_S2CFeedSM));
				BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_INTEGRATION, new Action<GameEvent>(this.Event_S2CIntegration));
			}
		}

		private void SetCreateAvatar(string name, uint typeid)
		{
			bool flag = this.m_selectedSummon != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this.m_selectedSummon);
				UnityEngine.Object.DestroyImmediate(this.m_avatar_Camera);
			}
			GameObject gameObject = Resources.Load<GameObject>("monster/" + name);
			this.m_selectedSummon = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-152.927f, 0.778f, 0f), Quaternion.identity) as GameObject);
			Transform[] componentsInChildren = this.m_selectedSummon.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = EnumLayer.LM_FX;
			}
			Transform transform2 = this.m_selectedSummon.transform.FindChild("model");
			Animator component = transform2.GetComponent<Animator>();
			component.cullingMode = AnimatorCullingMode.AlwaysAnimate;
			transform2.gameObject.AddComponent<Summon_Base_Event>();
			GameObject original = Resources.Load<GameObject>("profession/avatar_ui/roleinfo_ui_camera");
			this.m_avatar_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
			Camera componentInChildren = this.m_avatar_Camera.GetComponentInChildren<Camera>();
			SXML sXML = XMLMgr.instance.GetSXML("callbeast", "");
			SXML node = sXML.GetNode("callbeast", "id==" + typeid);
			int @int = node.getInt("mid");
			SXML sXML2 = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + @int);
			transform2.Rotate(Vector3.up, (float)(270 - sXML2.getInt("smshow_face")));
			bool flag2 = componentInChildren != null;
			if (flag2)
			{
				float orthographicSize = componentInChildren.orthographicSize * 1920f / 1080f * (float)Screen.height / (float)Screen.width;
				componentInChildren.orthographicSize = orthographicSize;
			}
			transform2.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
			Transform[] componentsInChildren2 = gameObject.GetComponentsInChildren<Transform>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform3 = componentsInChildren2[j];
				transform3.gameObject.layer = EnumLayer.LM_FX;
			}
		}

		private void SetDisposeAvatar()
		{
			bool flag = this.m_selectedSummon != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this.m_selectedSummon);
				UnityEngine.Object.DestroyImmediate(this.m_avatar_Camera);
			}
		}

		private void SetPages(a3_summon.e_Page page)
		{
			this.lookobj.SetActive(false);
			this.curstate = page;
			foreach (Transform current in this.m_contents)
			{
				current.gameObject.SetActive(false);
			}
			this.m_contents[(int)page].gameObject.SetActive(true);
			this.SetDisposeAvatar();
			switch (page)
			{
			case a3_summon.e_Page.info:
				this.feedpan.SetActive(false);
				this.feedpan2.SetActive(false);
				this.ShowSelectSummon();
				break;
			case a3_summon.e_Page.grade:
			{
				this._integrationSummon = default(a3_BagItemData);
				bool flag = this._integrationSummonObj != null;
				if (flag)
				{
					UnityEngine.Object.DestroyImmediate(this._integrationSummonObj);
				}
				bool flag2 = this._itIcon != null;
				if (flag2)
				{
					UnityEngine.Object.DestroyImmediate(this._itIcon);
				}
				this.ShowGrade();
				break;
			}
			case a3_summon.e_Page.skill:
			{
				this.selectbookid = 0;
				this.slectKey = 1;
				this.ShowSummonSkills();
				this.ShowSkillBooks();
				this.showSkillORbook();
				bool flag3 = this.m_selectedData.summondata.id <= 0;
				if (flag3)
				{
					base.transform.FindChild("contents/3").GetComponent<CanvasGroup>().alpha = 0f;
				}
				else
				{
					base.transform.FindChild("contents/3").GetComponent<CanvasGroup>().alpha = 1f;
				}
				break;
			}
			case a3_summon.e_Page.identify:
				foreach (GameObject current2 in this.m_identifyList.Values)
				{
					UnityEngine.Object.Destroy(current2);
				}
				this.m_identifyList.Clear();
				foreach (GameObject current3 in this.m_toItem)
				{
					UnityEngine.Object.Destroy(current3);
				}
				this.m_toItem.Clear();
				this.m_identifyList.Clear();
				break;
			}
		}

		private void SetAvatarDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.m_selectedSummon != null;
			if (flag)
			{
				this.m_selectedSummon.transform.Rotate(Vector3.up, -delta.x);
			}
		}

		private GameObject SetIcon(a3_BagItemData data, Transform parent, int num = -1)
		{
			bool flag = data.summondata.grade == 0;
			if (flag)
			{
				data.confdata.borderfile = "icon/itemborder/b039_0" + data.confdata.quality;
			}
			else
			{
				data.confdata.borderfile = "icon/itemborder/b039_0" + (data.summondata.grade + 1);
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data.confdata, false, num, 1f, false, -1, 0, false, false, false, -1, false, false);
			gameObject.transform.SetParent(parent, false);
			gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0f);
			return gameObject;
		}

		private GameObject GetEmptyToidentCell()
		{
			this.m_toIdentRoot = base.transform.FindChild("contents/4/right/up");
			Transform[] componentsInChildren = this.m_toIdentRoot.GetComponentsInChildren<Transform>(true);
			GameObject result;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent.gameObject == this.m_toIdentRoot.gameObject;
				if (flag)
				{
					bool flag2 = this.m_toidentifyList[transform.gameObject] == 0u;
					if (flag2)
					{
						result = transform.gameObject;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		private void ShowJitan(GameObject go)
		{
			this.tra_jitan.gameObject.SetActive(true);
			this.jitan_tabc.setSelectedIndex(0, true);
			this.SetDisposeAvatar();
		}

		private void CloseJitan(GameObject go)
		{
			bool flag = ModelBase<A3_SummonModel>.getInstance().GetSummons().Count <= 0;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SUMMON);
			}
			else
			{
				this.tra_jitan.gameObject.SetActive(false);
				this.SetPages(this.curstate);
			}
		}

		private void jitan_onswitch(TabControl tc)
		{
			int seletedIndex = tc.getSeletedIndex();
			Transform transform = base.transform.FindChild("sub_jitan/Panel/contents");
			for (int i = 0; i < transform.childCount; i++)
			{
				bool flag = i == seletedIndex;
				if (flag)
				{
					transform.GetChild(i).gameObject.SetActive(true);
				}
				else
				{
					transform.GetChild(i).gameObject.SetActive(false);
				}
			}
			this.Jitan_refresh(seletedIndex);
		}

		private void Jitan_init()
		{
			this.jitanlist.Clear();
			SXML sXML = XMLMgr.instance.GetSXML("callbeast", "");
			List<SXML> nodeList = sXML.GetNodeList("callbeast", "type==" + 1);
			List<SXML> nodeList2 = sXML.GetNodeList("callbeast", "type==" + 2);
			Transform transform = base.transform.FindChild("sub_jitan/Panel/contents/putong/scroll/0");
			Transform parent = base.transform.FindChild("sub_jitan/Panel/contents/putong/scroll/content");
			Transform transform2 = base.transform.FindChild("sub_jitan/Panel/contents/jingying/scroll/0");
			Transform parent2 = base.transform.FindChild("sub_jitan/Panel/contents/jingying/scroll/content");
			Transform transform3 = base.transform.FindChild("sub_jitan/Panel/contents/lingzhu/scroll/0");
			Transform parent3 = base.transform.FindChild("sub_jitan/Panel/contents/lingzhu/scroll/content");
			Transform transform4 = base.transform.FindChild("sub_jitan/Panel/contents/bianyi/scroll/0");
			Transform parent4 = base.transform.FindChild("sub_jitan/Panel/contents/bianyi/scroll/content");
			Transform transform5 = base.transform.FindChild("sub_jitan/Panel/contents/kezhaohuan/scroll/0");
			Transform transform6 = base.transform.FindChild("sub_jitan/Panel/contents/kezhaohuan/scroll/content");
			int i = 0;
			while (i < nodeList.Count)
			{
				SXML sXML2 = nodeList[i];
				bool flag = sXML2.getInt("quality") == 1;
				if (flag)
				{
					bool flag2 = sXML2.getInt("exchange") != 1;
					if (!flag2)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
						this.Jitan_set0(gameObject.transform, nodeList[i]);
						gameObject.transform.SetParent(parent);
						gameObject.transform.localScale = Vector3.one;
						gameObject.SetActive(true);
						gameObject.name = nodeList[i].getString("id");
						new BaseButton(gameObject.transform.FindChild("btn_hc"), 1, 1).onClick = new Action<GameObject>(this.jitan_hc_onclick);
						this.jitanlist.Add(gameObject);
					}
				}
				else
				{
					bool flag3 = sXML2.getInt("quality") == 2;
					if (flag3)
					{
						bool flag4 = sXML2.getInt("exchange") != 1;
						if (!flag4)
						{
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(transform2.gameObject);
							this.Jitan_set0(gameObject2.transform, nodeList[i]);
							gameObject2.transform.SetParent(parent2);
							gameObject2.transform.localScale = Vector3.one;
							gameObject2.SetActive(true);
							gameObject2.name = nodeList[i].getString("id");
							new BaseButton(gameObject2.transform.FindChild("btn_hc"), 1, 1).onClick = new Action<GameObject>(this.jitan_hc_onclick);
							this.jitanlist.Add(gameObject2);
						}
					}
					else
					{
						bool flag5 = sXML2.getInt("quality") == 3;
						if (flag5)
						{
							bool flag6 = sXML2.getInt("exchange") != 1;
							if (!flag6)
							{
								GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(transform3.gameObject);
								this.Jitan_set0(gameObject3.transform, nodeList[i]);
								gameObject3.transform.SetParent(parent3);
								gameObject3.transform.localScale = Vector3.one;
								gameObject3.SetActive(true);
								gameObject3.name = nodeList[i].getString("id");
								new BaseButton(gameObject3.transform.FindChild("btn_hc"), 1, 1).onClick = new Action<GameObject>(this.jitan_hc_onclick);
								this.jitanlist.Add(gameObject3);
							}
						}
					}
				}
				IL_3B2:
				i++;
				continue;
				goto IL_3B2;
			}
			for (int j = 0; j < nodeList2.Count; j++)
			{
				bool flag7 = nodeList2[j].getInt("exchange") != 1;
				if (!flag7)
				{
					GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(transform4.gameObject);
					this.Jitan_set0(gameObject4.transform, nodeList2[j]);
					gameObject4.transform.SetParent(parent4);
					gameObject4.transform.localScale = Vector3.one;
					gameObject4.SetActive(true);
					gameObject4.name = nodeList2[j].getString("id");
					new BaseButton(gameObject4.transform.FindChild("btn_hc"), 1, 1).onClick = new Action<GameObject>(this.jitan_hc_onclick);
					this.jitanlist.Add(gameObject4);
				}
			}
		}

		private void Jitan_set0(Transform go, SXML xml)
		{
			int @int = xml.getInt("id");
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)@int);
			go.FindChild("icon").GetComponent<Image>().sprite = (Resources.Load(itemDataById.file, typeof(Sprite)) as Sprite);
			go.FindChild("name").GetComponent<Text>().text = xml.getString("name");
			go.FindChild("type").GetComponent<Text>().text = "类型：" + ((xml.getInt("type") > 1) ? "变异" : "普通");
			go.FindChild("lv").GetComponent<Text>().text = "特长：" + ModelBase<A3_SummonModel>.getInstance().IntNaturalToStr(xml.getInt("speciality"));
			go.FindChild("grade").GetComponent<Text>().text = "品质：" + ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(xml.getInt("quality"));
		}

		private void Jitan_refresh(int i)
		{
			Transform transform = base.transform.FindChild("sub_jitan/Panel/contents/kezhaohuan/scroll/content");
			Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>(true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				Transform transform2 = componentsInChildren[j];
				bool flag = transform2.parent == transform.transform;
				if (flag)
				{
					UnityEngine.Object.Destroy(transform2.gameObject);
				}
			}
			List<GameObject> list = new List<GameObject>();
			List<GameObject> list2 = new List<GameObject>();
			foreach (GameObject current in this.jitanlist)
			{
				int num = int.Parse(current.name);
				SXML sXML = XMLMgr.instance.GetSXML("callbeast.callbeast", "id==" + num);
				int @int = sXML.getInt("need_item");
				int int2 = sXML.getInt("need_num");
				int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)@int);
				current.transform.FindChild("num").GetComponent<Text>().text = itemNumByTpid + "/" + int2;
				bool flag2 = itemNumByTpid >= int2;
				if (flag2)
				{
					current.transform.FindChild("btn_hc").GetComponent<Button>().interactable = true;
					current.transform.FindChild("can").gameObject.SetActive(true);
					list.Add(current);
				}
				else
				{
					current.transform.FindChild("btn_hc").GetComponent<Button>().interactable = false;
					current.transform.FindChild("can").gameObject.SetActive(false);
					BaseButton arg_1DC_0 = new BaseButton(current.transform.FindChild("btn_hc"), 1, 1);
					Action<GameObject> arg_1DC_1;
					if ((arg_1DC_1 = a3_summon.<>c.<>9__124_0) == null)
					{
						arg_1DC_1 = (a3_summon.<>c.<>9__124_0 = new Action<GameObject>(a3_summon.<>c.<>9.<Jitan_refresh>b__124_0));
					}
					arg_1DC_0.onClickFalse = arg_1DC_1;
					list2.Add(current);
				}
			}
			foreach (GameObject current2 in list)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(current2.gameObject);
				gameObject.transform.SetParent(transform);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = current2.name;
				new BaseButton(gameObject.transform.FindChild("btn_hc"), 1, 1).onClick = new Action<GameObject>(this.jitan_hc_onclick);
			}
			list.Clear();
			foreach (GameObject current3 in list2)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(current3.gameObject);
				gameObject2.transform.SetParent(transform);
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.transform.name = current3.name;
				BaseButton arg_33E_0 = new BaseButton(gameObject2.transform.FindChild("btn_hc"), 1, 1);
				Action<GameObject> arg_33E_1;
				if ((arg_33E_1 = a3_summon.<>c.<>9__124_1) == null)
				{
					arg_33E_1 = (a3_summon.<>c.<>9__124_1 = new Action<GameObject>(a3_summon.<>c.<>9.<Jitan_refresh>b__124_1));
				}
				arg_33E_0.onClickFalse = arg_33E_1;
			}
			list2.Clear();
		}

		private void jitan_hc_onclick(GameObject go)
		{
			uint tpid = (uint)int.Parse(go.transform.parent.name);
			BaseProxy<A3_SummonProxy>.getInstance().sendZHSummon(tpid);
		}
	}
}
