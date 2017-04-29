using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_active : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_active.<>c <>9 = new a3_active.<>c();

			public static Action<GameEvent> <>9__6_1;

			internal void <init>b__6_1(GameEvent e)
			{
				bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.DEVIL_HUNTER, false) && a3_active_mwlr_kill.Instance.Count != 0;
				if (flag)
				{
					BaseProxy<A3_ActiveProxy>.getInstance().SendGiveUpHunt();
				}
			}
		}

		private a3BaseActive _currentActive = null;

		private Dictionary<string, a3BaseActive> _activies = new Dictionary<string, a3BaseActive>();

		public static a3_active instance;

		public static a3_active onshow;

		private bool Toclose = false;

		public bool map_light;

		public string openView = null;

		public string pastopen = null;

		private int Time_bt = 0;

		public override void init()
		{
			this._activies["mlzd"] = new a3_active_mlzd(this, "contents/mlzd");
			this._activies["summonpark"] = new a3_active_zhsly(this, "contents/summonpark");
			this._activies["mwlr"] = new a3_active_mwlr(this, "contents/mwlr");
			this._activies["pvp"] = new a3_active_pvp(this, "contents/pvp");
			this._activies["forchest"] = new a3_active_forchest(this, "contents/forchest");
			this._activies["findbtu"] = new a3_active_findbtu(this, "contents/findbtu");
			this.InitLayout();
			new BaseButton(base.getTransformByPath("btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.Toclose = true;
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACTIVE);
			};
			this.CheckLock();
			a3_active.instance = this;
			GameEventDispatcher arg_10A_0 = BaseProxy<TeamProxy>.getInstance();
			uint arg_10A_1 = TeamProxy.EVENT_LEAVETEAM;
			Action<GameEvent> arg_10A_2;
			if ((arg_10A_2 = a3_active.<>c.<>9__6_1) == null)
			{
				arg_10A_2 = (a3_active.<>c.<>9__6_1 = new Action<GameEvent>(a3_active.<>c.<>9.<init>b__6_1));
			}
			arg_10A_0.addEventListener(arg_10A_1, arg_10A_2);
		}

		public override void onShowed()
		{
			BaseProxy<A3_ActiveProxy>.getInstance().SendGetHuntInfo();
			bool flag = this.uiData != null;
			if (flag)
			{
				string tabname = (string)this.uiData[0];
				this.ShowTabContent(tabname);
			}
			else
			{
				bool flag2 = this._currentActive != null;
				if (flag2)
				{
					this._currentActive.onShowed();
				}
			}
			GRMap.GAME_CAMERA.SetActive(false);
			RectTransform componentByPath = base.getComponentByPath<RectTransform>("scroll_view/contain");
			componentByPath.anchoredPosition = new Vector2(componentByPath.anchoredPosition.x, 0f);
			GridLayoutGroup component = componentByPath.transform.GetComponent<GridLayoutGroup>();
			componentByPath.sizeDelta = new Vector2(componentByPath.sizeDelta.x, (float)componentByPath.transform.childCount * component.cellSize.y);
			a3_active.onshow = this;
			for (int i = 0; i < componentByPath.transform.childCount; i++)
			{
				bool activeSelf = componentByPath.transform.GetChild(i).gameObject.activeSelf;
				if (activeSelf)
				{
					this.openView = componentByPath.transform.GetChild(i).gameObject.name;
					break;
				}
			}
			bool flag3 = this.uiData == null && this.openView != null && this.pastopen == null;
			if (flag3)
			{
				this.ShowTabContent(this.openView);
				this.openView = null;
			}
			else
			{
				bool flag4 = this.uiData == null && this.pastopen != null;
				if (flag4)
				{
					this.ShowTabContent(this.pastopen);
				}
			}
		}

		public override void onClosed()
		{
			bool flag = a3_active_mwlr.instance != null;
			if (flag)
			{
				a3_active_mwlr.instance.getTransformByPath("timer/timerRec").GetComponent<Text>().text = "00:00:00";
			}
			bool flag2 = this._currentActive != null;
			if (flag2)
			{
				this._currentActive.onClose();
			}
			GRMap.GAME_CAMERA.SetActive(true);
			bool flag3 = a3_active_mwlr_kill.Instance.MHInfo.gameObject && a3_active_mwlr_kill.Instance.NewAction;
			if (flag3)
			{
				bool newAction = a3_active_mwlr_kill.Instance.NewAction;
				if (newAction)
				{
					Animator expr_91 = a3_active_mwlr_kill.Instance.MHAnimator;
					if (expr_91 != null)
					{
						expr_91.SetTrigger("start");
					}
				}
				a3_active_mwlr_kill.Instance.Reset();
				a3_active_mwlr_kill.Instance.NewAction = false;
			}
			else
			{
				bool mwlr_giveup = ModelBase<A3_ActiveModel>.getInstance().mwlr_giveup;
				if (mwlr_giveup)
				{
					a3_active_mwlr_kill.Instance.Clear();
				}
			}
			a3_active.onshow = null;
			bool flag4 = a3_itemLack.intans && a3_itemLack.intans.closewindow != null;
			if (flag4)
			{
				bool toclose = this.Toclose;
				if (toclose)
				{
					InterfaceMgr.getInstance().open(a3_itemLack.intans.closewindow, null, false);
					a3_itemLack.intans.closewindow = null;
					this.Toclose = false;
				}
				else
				{
					a3_itemLack.intans.closewindow = null;
				}
			}
			else
			{
				bool flag5 = a3_getJewelryWay.instance && a3_getJewelryWay.instance.closeWin != null && this.Toclose;
				if (flag5)
				{
					InterfaceMgr.getInstance().open(a3_getJewelryWay.instance.closeWin, null, false);
					a3_getJewelryWay.instance.closeWin = null;
				}
			}
		}

		public void CheckLock()
		{
			base.getGameObjectByPath("scroll_view/contain/mlzd").gameObject.SetActive(false);
			base.getGameObjectByPath("scroll_view/contain/summonpark").gameObject.SetActive(false);
			base.getGameObjectByPath("scroll_view/contain/mwlr").gameObject.SetActive(false);
			base.getGameObjectByPath("scroll_view/contain/pvp").gameObject.SetActive(false);
			base.getGameObjectByPath("scroll_view/contain/forchest").gameObject.SetActive(false);
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.CHASTEN_JAIL, false);
			if (flag)
			{
				this.OpenMLZD();
			}
			bool flag2 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.SUMMON_PARK, false);
			if (flag2)
			{
				this.OpenSummon();
			}
			bool flag3 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.DEVIL_HUNTER, false);
			if (flag3)
			{
				this.OpenMWLR();
			}
			bool flag4 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.PVP_DUNGEON, false);
			if (flag4)
			{
				this.OpenPVP();
			}
			bool flag5 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.FOR_CHEST, false);
			if (flag5)
			{
				this.OpenForChest();
			}
		}

		public void OpenMLZD()
		{
			base.getGameObjectByPath("scroll_view/contain/mlzd").gameObject.SetActive(true);
		}

		public void OpenSummon()
		{
			base.getGameObjectByPath("scroll_view/contain/summonpark").gameObject.SetActive(true);
		}

		public void OpenMWLR()
		{
			base.getGameObjectByPath("scroll_view/contain/mwlr").gameObject.SetActive(true);
		}

		public void OpenPVP()
		{
			base.getGameObjectByPath("scroll_view/contain/pvp").gameObject.SetActive(true);
		}

		public void OpenForChest()
		{
			base.getGameObjectByPath("scroll_view/contain/forchest").gameObject.SetActive(true);
		}

		private void Update()
		{
			bool flag = a3_active_mwlr.instance != null;
			if (flag)
			{
				a3_active_mwlr.instance.Re_Time();
			}
		}

		public void Runtimer_bt(int time)
		{
			this.Time_bt = time;
			base.CancelInvoke("runTime_bt");
			base.InvokeRepeating("runTime_bt", 0f, 1f);
		}

		private void runTime_bt()
		{
			this.Time_bt--;
			bool flag = a3_active_findbtu.instans != null;
			if (flag)
			{
				a3_active_findbtu.instans.showtime(this.Time_bt);
				bool flag2 = this.Time_bt <= 0;
				if (flag2)
				{
					this.Time_bt = 0;
					base.CancelInvoke("runTime_bt");
					BaseProxy<FindBestoProxy>.getInstance().getinfo();
				}
			}
			else
			{
				base.CancelInvoke("runTime_bt");
			}
		}

		private void InitLayout()
		{
			Transform transform = base.getGameObjectByPath("contents").transform;
			Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform2 = componentsInChildren[i];
				bool flag = transform2.parent == transform;
				if (flag)
				{
					transform2.gameObject.SetActive(false);
				}
			}
			GameObject gameObjectByPath = base.getGameObjectByPath("scroll_view/contain");
			Transform[] componentsInChildren2 = gameObjectByPath.GetComponentsInChildren<Transform>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform3 = componentsInChildren2[j];
				bool flag2 = transform3.parent == gameObjectByPath.transform;
				if (flag2)
				{
					transform3.GetComponent<Button>().interactable = true;
					new BaseButton(transform3.transform, 1, 1).onClick = new Action<GameObject>(this.Tab_click);
				}
			}
		}

		private void ShowTabContent(string tabname)
		{
			Transform transform = base.getGameObjectByPath("contents").transform;
			Transform transform2 = transform.FindChild(tabname);
			this.pastopen = tabname;
			bool flag = transform2 != null;
			if (flag)
			{
				this.Tab_click(transform2.gameObject);
			}
		}

		private void Tab_click(GameObject go)
		{
			bool flag = !this._activies.ContainsKey(go.name);
			if (!flag)
			{
				this.pastopen = go.name;
				Transform transform = base.getGameObjectByPath("contents").transform;
				Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform2 = componentsInChildren[i];
					bool flag2 = transform2.parent == transform;
					if (flag2)
					{
						transform2.gameObject.SetActive(false);
					}
				}
				Transform transform3 = transform.FindChild(go.name);
				bool flag3 = transform3 != null;
				if (flag3)
				{
					transform3.gameObject.SetActive(true);
				}
				GameObject gameObjectByPath = base.getGameObjectByPath("scroll_view/contain");
				Transform[] componentsInChildren2 = gameObjectByPath.GetComponentsInChildren<Transform>(true);
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Transform transform4 = componentsInChildren2[j];
					bool flag4 = transform4.parent == gameObjectByPath.transform;
					if (flag4)
					{
						transform4.GetComponent<Button>().interactable = true;
					}
				}
				Button component = go.GetComponent<Button>();
				bool flag5 = component == null;
				if (flag5)
				{
					component = gameObjectByPath.transform.FindChild(go.name).GetComponent<Button>();
				}
				bool flag6 = component != null;
				if (flag6)
				{
					component.interactable = false;
				}
				bool flag7 = this._currentActive != null;
				if (flag7)
				{
					this._currentActive.onClose();
				}
				bool flag8 = this._activies.ContainsKey(go.name);
				if (flag8)
				{
					this._currentActive = this._activies[go.name];
					this._currentActive.onShowed();
				}
			}
		}
	}
}
