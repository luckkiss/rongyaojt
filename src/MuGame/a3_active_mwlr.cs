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
	internal class a3_active_mwlr : a3BaseActive
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_active_mwlr.<>c <>9 = new a3_active_mwlr.<>c();

			public static Action<GameObject> <>9__19_2;

			public static Action<GameObject> <>9__19_3;

			public static Action<GameObject> <>9__19_6;

			public static Action<GameEvent> <>9__19_7;

			public static Action <>9__27_0;

			public static Action <>9__29_1;

			internal void <init>b__19_2(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(2);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SPEEDTEAM, arrayList, false);
			}

			internal void <init>b__19_3(GameObject go)
			{
				BaseProxy<TeamProxy>.getInstance().SendCreateTeam(2);
			}

			internal void <init>b__19_6(GameObject go)
			{
				a3_active_mwlr_kill.Instance.NewAction = false;
				BaseProxy<A3_ActiveProxy>.getInstance().SendGiveUpHunt();
			}

			internal void <init>b__19_7(GameEvent e)
			{
				a3_active_mwlr_kill.Instance.ReloadData(ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info);
			}

			internal void <PlaySearchAni>b__27_0()
			{
				BaseProxy<A3_ActiveProxy>.getInstance().SendStartHunt();
			}

			internal void <Refresh>b__29_1()
			{
				bool mwlr_on = ModelBase<A3_ActiveModel>.getInstance().mwlr_on;
				if (mwlr_on)
				{
					bool flag = SelfRole._inst.m_moveAgent.remainingDistance < 1f;
					if (flag)
					{
						bool flag2 = !SelfRole.fsm.Autofighting;
						if (flag2)
						{
							SelfRole.fsm.StartAutofight();
							SelfRole.fsm.ChangeState(StateAttack.Instance);
						}
					}
				}
			}
		}

		public static a3_active_mwlr instance;

		private Text textTargetMonsterName;

		public BaseButton searchbtn;

		public BaseButton createTeamBtn;

		public BaseButton searchTeamBtn;

		private Transform search_ani;

		private GameObject I302;

		private GameObject goTotalLeftTime;

		private Image timebar;

		private float needtime;

		private GameObject createobj;

		private GameObject camobj;

		private Transform tfReward;

		private Transform tfRewardIcon;

		private Transform rewardItemTip;

		private Text rewardItemTip_itemName;

		private Text rewardItemTip_itemDesc;

		private Transform rewardItemTip_IconRoot;

		public a3_active_mwlr(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			a3_active_mwlr.instance = this;
			this.textTargetMonsterName = base.transform.FindChild("B/roleimg/Text").GetComponent<Text>();
			this.timebar = base.getComponentByPath<Image>("B/info/1/time/bar");
			this.search_ani = base.getTransformByPath("search_ani");
			this.I302 = base.getTransformByPath("B/info/3/S/1").gameObject;
			Transform transformByPath = base.getTransformByPath("B/roleimg/img");
			bool flag = transformByPath != null;
			if (flag)
			{
				transformByPath.gameObject.SetActive(false);
			}
			this.tfReward = base.transform.FindChild("A/reward/rewardScroll/rewardLayout");
			this.rewardItemTip = base.transform.FindChild("A/reward/rewardItemTip");
			this.rewardItemTip_itemName = this.rewardItemTip.FindChild("text_bg/nameBg/itemName").GetComponent<Text>();
			this.rewardItemTip_itemDesc = this.rewardItemTip.FindChild("text_bg/text").GetComponent<Text>();
			this.rewardItemTip_IconRoot = this.rewardItemTip.FindChild("text_bg/iconbg/icon");
			new BaseButton(this.rewardItemTip.FindChild("close_btn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.rewardItemTip.gameObject.SetActive(false);
			};
			this.tfRewardIcon = base.transform.FindChild("A/reward/template/icon");
			List<SXML> nodeList = XMLMgr.instance.GetSXML("monsterhunter", "").GetNodeList("item", "");
			bool flag2 = nodeList != null;
			if (flag2)
			{
				for (int i = 0; i < nodeList.Count; i++)
				{
					uint @uint = nodeList[i].getUint("item");
					GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(@uint, false, -1, 1f, false, -1, 0, false, false, true, true);
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.tfRewardIcon.gameObject);
					gameObject.transform.SetParent(gameObject2.transform, false);
					gameObject.transform.SetAsFirstSibling();
					this.CreateButtonShowReward(gameObject2, @uint);
					gameObject2.transform.SetParent(gameObject2.transform, false);
					gameObject2.transform.SetParent(this.tfReward, false);
				}
			}
			this.searchbtn = new BaseButton(base.getTransformByPath("A/searchbtn"), 1, 1);
			this.searchbtn.onClick = delegate(GameObject go)
			{
				bool flag3 = !ModelBase<PlayerModel>.getInstance().IsCaptainOrAlone;
				if (flag3)
				{
					flytxt.instance.fly(err_string.get_Err_String(-6600), 0, default(Color), null);
				}
				else
				{
					a3_active_mwlr_kill.Instance.Clear();
					a3_active_mwlr_kill.Instance.NewAction = true;
					a3_active_mwlr_kill.Instance.Reset();
					this.search_ani.gameObject.SetActive(true);
					this.PlaySearchAni();
				}
			};
			this.searchTeamBtn = new BaseButton(base.getTransformByPath("A/searchteambtn"), 1, 1);
			BaseButton arg_28E_0 = this.searchTeamBtn;
			Action<GameObject> arg_28E_1;
			if ((arg_28E_1 = a3_active_mwlr.<>c.<>9__19_2) == null)
			{
				arg_28E_1 = (a3_active_mwlr.<>c.<>9__19_2 = new Action<GameObject>(a3_active_mwlr.<>c.<>9.<init>b__19_2));
			}
			arg_28E_0.onClick = arg_28E_1;
			this.createTeamBtn = new BaseButton(base.getTransformByPath("A/createteambtn"), 1, 1);
			BaseButton arg_2D1_0 = this.createTeamBtn;
			Action<GameObject> arg_2D1_1;
			if ((arg_2D1_1 = a3_active_mwlr.<>c.<>9__19_3) == null)
			{
				arg_2D1_1 = (a3_active_mwlr.<>c.<>9__19_3 = new Action<GameObject>(a3_active_mwlr.<>c.<>9.<init>b__19_3));
			}
			arg_2D1_0.onClick = arg_2D1_1;
			new BaseButton(base.getTransformByPath("search_ani/panel1/bg_0"), 1, 1).onClick = delegate(GameObject go)
			{
				cd.hide();
				this.search_ani.gameObject.SetActive(false);
			};
			new BaseButton(base.getTransformByPath("search_ani/panel2/bg_0"), 1, 1).onClick = delegate(GameObject go)
			{
				this.search_ani.gameObject.SetActive(false);
			};
			BaseButton arg_350_0 = new BaseButton(base.getTransformByPath("B/info/1/giveup"), 1, 1);
			Action<GameObject> arg_350_1;
			if ((arg_350_1 = a3_active_mwlr.<>c.<>9__19_6) == null)
			{
				arg_350_1 = (a3_active_mwlr.<>c.<>9__19_6 = new Action<GameObject>(a3_active_mwlr.<>c.<>9.<init>b__19_6));
			}
			arg_350_0.onClick = arg_350_1;
			GameEventDispatcher arg_37F_0 = BaseProxy<A3_ActiveProxy>.getInstance();
			uint arg_37F_1 = A3_ActiveProxy.EVENT_MWLR_NEW;
			Action<GameEvent> arg_37F_2;
			if ((arg_37F_2 = a3_active_mwlr.<>c.<>9__19_7) == null)
			{
				arg_37F_2 = (a3_active_mwlr.<>c.<>9__19_7 = new Action<GameEvent>(a3_active_mwlr.<>c.<>9.<init>b__19_7));
			}
			arg_37F_0.addEventListener(arg_37F_1, arg_37F_2);
		}

		private void CreateButtonShowReward(GameObject rewardIcon, uint itemId)
		{
			SXML itemXml = ModelBase<a3_BagModel>.getInstance().getItemXml((int)itemId);
			string @string = itemXml.getString("desc");
			string string2 = itemXml.getString("item_name");
			int showType = itemXml.getInt("show_type");
			showType = ((showType == -1) ? 1 : showType);
			new BaseButton(rewardIcon.transform.FindChild("btn"), 1, 1).onClick = delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(itemId);
				arrayList.Add(showType);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
			};
		}

		public override void onShowed()
		{
			this.DestroyModel();
			a3_active_mwlr.instance = this;
			BaseProxy<A3_ActiveProxy>.getInstance().addEventListener(A3_ActiveProxy.EVENT_MLZDOPCUCCESS, new Action<GameEvent>(this.Refresh));
			this.search_ani.gameObject.SetActive(false);
			this.Refresh(null);
			bool activeSelf = base.getTransformByPath("A").gameObject.activeSelf;
			if (activeSelf)
			{
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
				if (flag)
				{
					this.searchbtn.gameObject.SetActive(true);
					this.searchTeamBtn.gameObject.SetActive(false);
					this.createTeamBtn.gameObject.SetActive(false);
				}
				else
				{
					this.searchbtn.gameObject.SetActive(false);
					this.searchTeamBtn.gameObject.SetActive(true);
					this.createTeamBtn.gameObject.SetActive(true);
				}
			}
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_CREATETEAM, new Action<GameEvent>(this.OnCreateTeamSuccess));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_LEAVETEAM, new Action<GameEvent>(this.OnLeaveTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_AFFIRMINVITE, new Action<GameEvent>(this.OnJoinTeamSuccess));
		}

		private void RefreshLeftTimes()
		{
		}

		public override void onClose()
		{
			this.DestroyModel();
			a3_active_mwlr.instance = null;
			this.needtime = 0f;
			BaseProxy<A3_ActiveProxy>.getInstance().removeEventListener(A3_ActiveProxy.EVENT_MLZDOPCUCCESS, new Action<GameEvent>(this.Refresh));
			BaseProxy<TeamProxy>.getInstance().removeEventListener(TeamProxy.EVENT_CREATETEAM, new Action<GameEvent>(this.OnCreateTeamSuccess));
			BaseProxy<TeamProxy>.getInstance().removeEventListener(TeamProxy.EVENT_LEAVETEAM, new Action<GameEvent>(this.OnLeaveTeam));
			BaseProxy<TeamProxy>.getInstance().removeEventListener(TeamProxy.EVENT_AFFIRMINVITE, new Action<GameEvent>(this.OnJoinTeamSuccess));
		}

		public void OnLeaveTeam(GameEvent e)
		{
			flytxt.instance.fly("你已离开队伍,魔物猎人结束", 0, default(Color), null);
			this.searchbtn.gameObject.SetActive(false);
			this.searchTeamBtn.gameObject.SetActive(true);
			this.createTeamBtn.gameObject.SetActive(true);
		}

		private void OnCreateTeamSuccess(GameEvent e)
		{
			bool flag = e.data.ContainsKey("ltpid") && e.data["ltpid"] == 2;
			if (flag)
			{
				flytxt.instance.fly("创建队伍成功！", 0, default(Color), null);
				this.searchbtn.gameObject.SetActive(true);
				this.searchTeamBtn.gameObject.SetActive(false);
				this.createTeamBtn.gameObject.SetActive(false);
			}
		}

		private void OnJoinTeamSuccess(GameEvent e)
		{
			flytxt.instance.fly("加入队伍成功！", 0, default(Color), null);
			this.searchbtn.gameObject.SetActive(true);
			this.searchTeamBtn.gameObject.SetActive(false);
			this.createTeamBtn.gameObject.SetActive(false);
		}

		private void PlaySearchAni()
		{
			this.search_ani.FindChild("panel1").gameObject.SetActive(true);
			this.search_ani.FindChild("panel2").gameObject.SetActive(false);
			cd.updateHandle = new Action<cd>(a3_active_mwlr.onCD);
			Action arg_84_0;
			if ((arg_84_0 = a3_active_mwlr.<>c.<>9__27_0) == null)
			{
				arg_84_0 = (a3_active_mwlr.<>c.<>9__27_0 = new Action(a3_active_mwlr.<>c.<>9.<PlaySearchAni>b__27_0));
			}
			cd.show(arg_84_0, 3f, false, null, new Vector3(67f, 32f, 0f));
		}

		public static void onCD(cd item)
		{
			int num = (int)(cd.secCD - cd.lastCD) / 100;
			item.txt.text = "";
		}

		private void Refresh(GameEvent e = null)
		{
			GameObject gameObject = this.search_ani.FindChild("panel1").gameObject;
			bool activeSelf = gameObject.activeSelf;
			if (activeSelf)
			{
				gameObject.SetActive(false);
			}
			bool flag = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info.Count > 0 && ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info[0]["target_mid"]._int > 0;
			if (flag)
			{
				a3_active_mwlr_kill.Instance.ReloadData(ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info);
				base.getTransformByPath("A").gameObject.SetActive(false);
				base.getTransformByPath("B").gameObject.SetActive(true);
				SXML node = XMLMgr.instance.GetSXML("monsters", "").GetNode("monsters", "id==" + ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info[0]["target_mid"]);
				this.search_ani.FindChild("panel2/name").GetComponent<Text>().text = node.getString("name");
				this.CreatModel(node.getString("obj"));
			}
			else
			{
				base.getTransformByPath("A").gameObject.SetActive(true);
				base.getTransformByPath("B").gameObject.SetActive(false);
				this.DestroyModel();
				this.searchbtn.interactable = (ModelBase<A3_ActiveModel>.getInstance().mwlr_charges > 0);
			}
			base.getTransformByPath("charges/num").GetComponent<Text>().text = ModelBase<A3_ActiveModel>.getInstance().mwlr_charges + "/" + 10;
			this.Re_Time();
			Transform transformByPath = base.getTransformByPath("B/info/4/0_map");
			Transform transformByPath2 = base.getTransformByPath("B/info/4/S");
			Transform[] componentsInChildren = transformByPath2.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag2 = transform.parent == transformByPath2;
				if (flag2)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			for (int j = 0; j < ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id.Count; j++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(transformByPath.gameObject);
				gameObject2.transform.SetParent(transformByPath2);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.transform.GetChild(0).gameObject.SetActive(ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info[j]["kill"]._bool);
				bool flag3 = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id[j] > 0;
				if (flag3)
				{
					Variant variant = SvrMapConfig.instance.mapConfs[(uint)ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id[j]];
					gameObject2.GetComponent<Text>().text = variant["map_name"];
				}
				gameObject2.gameObject.SetActive(true);
				List<int> t = new List<int>();
				t.Add(ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id[j]);
				new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject go)
				{
					SXML node2 = XMLMgr.instance.GetSXML("monsterhunter", "").GetNode("map", "mapid==" + t[0]);
					int @int = node2.getInt("point_id");
					ModelBase<A3_ActiveModel>.getInstance().mwlr_target_monId = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info[0]["target_mid"];
					ModelBase<A3_ActiveModel>.getInstance().mwlr_target_pos = new Vector3(ModelBase<A3_ActiveModel>.getInstance().mwlr_mons_pos[t[0]].x, 0f, ModelBase<A3_ActiveModel>.getInstance().mwlr_mons_pos[t[0]].z);
					ModelBase<A3_ActiveModel>.getInstance().mwlr_on = true;
					int arg_115_0 = node2.m_dAtttr["mapid"].intvalue;
					Vector3 arg_115_1 = ModelBase<A3_ActiveModel>.getInstance().mwlr_target_pos;
					Action arg_115_2;
					if ((arg_115_2 = a3_active_mwlr.<>c.<>9__29_1) == null)
					{
						arg_115_2 = (a3_active_mwlr.<>c.<>9__29_1 = new Action(a3_active_mwlr.<>c.<>9.<Refresh>b__29_1));
					}
					SelfRole.WalkToMap(arg_115_0, arg_115_1, arg_115_2, 0.3f);
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACTIVE);
				};
			}
			bool flag4 = ModelBase<A3_ActiveModel>.getInstance().mwlr_doubletime <= 0;
			if (flag4)
			{
				this.needtime = 0f;
			}
			else
			{
				this.needtime = (float)(ModelBase<A3_ActiveModel>.getInstance().mwlr_doubletime - muNetCleint.instance.CurServerTimeStamp);
			}
			bool flag5 = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info.Count > 0;
			if (flag5)
			{
				this.textTargetMonsterName.text = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info[0]["target_mid"]._str).getString("name");
			}
		}

		private void CreatModel(string obj)
		{
			bool flag = this.createobj != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this.createobj);
				UnityEngine.Object.DestroyImmediate(this.camobj);
			}
			GameObject gameObject = Resources.Load<GameObject>("monster/" + obj);
			this.createobj = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-153.43f, 1f, 0f), Quaternion.identity) as GameObject);
			Transform[] componentsInChildren = this.createobj.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = EnumLayer.LM_FX;
			}
			Transform transform2 = this.createobj.transform.FindChild("model");
			GameObject original = Resources.Load<GameObject>("profession/avatar_ui/roleinfo_ui_camera");
			this.camobj = UnityEngine.Object.Instantiate<GameObject>(original);
			Camera componentInChildren = this.camobj.GetComponentInChildren<Camera>();
			bool flag2 = componentInChildren != null;
			if (flag2)
			{
				float orthographicSize = componentInChildren.orthographicSize * 1920f / 1080f * (float)Screen.height / (float)Screen.width;
				componentInChildren.orthographicSize = orthographicSize;
			}
			transform2.gameObject.AddComponent<Summon_Base_Event>();
			transform2.Rotate(Vector3.up, 180f);
			transform2.transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
			Transform[] componentsInChildren2 = gameObject.GetComponentsInChildren<Transform>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform3 = componentsInChildren2[j];
				transform3.gameObject.layer = EnumLayer.LM_FX;
			}
		}

		private void DestroyModel()
		{
			bool flag = this.createobj != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this.createobj);
			}
			bool flag2 = this.camobj != null;
			if (flag2)
			{
				UnityEngine.Object.DestroyImmediate(this.camobj);
			}
		}

		public void Re_Time()
		{
			bool flag = ModelBase<A3_ActiveModel>.getInstance().mwlr_totaltime <= 0;
			if (flag)
			{
				base.getTransformByPath("timer/timerTotalLeftTime").GetComponent<Text>().text = "00:00:00";
				base.getTransformByPath("timer/timerRec").GetComponent<Text>().text = "00:00:00";
			}
			else
			{
				bool flag2 = ModelBase<A3_ActiveModel>.getInstance().mwlr_doubletime <= 0;
				if (flag2)
				{
					base.getTransformByPath("timer/timerRec").GetComponent<Text>().text = "00:00:00";
				}
				TimeSpan timeSpan = new TimeSpan(0, 0, Mathf.Max(0, ModelBase<A3_ActiveModel>.getInstance().mwlr_doubletime - muNetCleint.instance.CurServerTimeStamp));
				TimeSpan timeSpan2 = new TimeSpan(0, 0, Mathf.Max(0, ModelBase<A3_ActiveModel>.getInstance().mwlr_totaltime - muNetCleint.instance.CurServerTimeStamp));
				bool activeSelf = base.getTransformByPath("B").gameObject.activeSelf;
				if (activeSelf)
				{
					base.getTransformByPath("timer/timerRec").GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
					base.getTransformByPath("timer/timerTotalLeftTime").GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan2.Hours, timeSpan2.Minutes, timeSpan2.Seconds);
				}
				else
				{
					base.getTransformByPath("timer/timerRec").GetComponent<Text>().text = "00:00:00";
					base.getTransformByPath("timer/timerTotalLeftTime").GetComponent<Text>().text = "00:00:00";
				}
				this.timebar.fillAmount = (float)timeSpan.TotalSeconds / this.needtime;
				bool flag3 = ModelBase<A3_ActiveModel>.getInstance().mwlr_doubletime - muNetCleint.instance.CurServerTimeStamp > 0;
				if (flag3)
				{
					bool flag4 = !this.I302.activeSelf;
					if (flag4)
					{
						this.I302.SetActive(true);
					}
				}
				else
				{
					bool activeSelf2 = this.I302.activeSelf;
					if (activeSelf2)
					{
						this.I302.SetActive(false);
					}
				}
			}
		}
	}
}
