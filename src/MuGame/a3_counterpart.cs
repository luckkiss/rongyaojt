using Cross;
using DG.Tweening;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_counterpart : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_counterpart.<>c <>9 = new a3_counterpart.<>c();

			public static Action<GameObject> <>9__50_6;

			public static Action<GameObject> <>9__50_7;

			public static Action<GameObject> <>9__50_8;

			public static Action<GameObject> <>9__98_0;

			public static Action<GameObject> <>9__98_1;

			public static Action<GameObject> <>9__99_0;

			public static Action<GameObject> <>9__100_0;

			internal void <init>b__50_6(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EXCHANGE, null, false);
				bool flag = a3_exchange.Instance != null;
				if (flag)
				{
					a3_exchange.Instance.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__50_7(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				bool flag = a3_Recharge.Instance != null;
				if (flag)
				{
					a3_Recharge.Instance.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__50_8(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACHIEVEMENT, null, false);
				bool flag = a3_achievement.instance != null;
				if (flag)
				{
					a3_achievement.instance.transform.SetAsLastSibling();
				}
			}

			internal void <OpenFB>b__98_0(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_32", null), 0, default(Color), null);
			}

			internal void <OpenFB>b__98_1(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_11", null), 0, default(Color), null);
			}

			internal void <OpenFSWZ>b__99_0(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_11", null), 0, default(Color), null);
			}

			internal void <OpenGold>b__100_0(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_13", null), 0, default(Color), null);
			}
		}

		private Dictionary<string, a3BaseActive> _activies = new Dictionary<string, a3BaseActive>();

		public static a3_counterpart instance;

		public GameObject choicePanel;

		public GameObject yesNo;

		public GameObject less5;

		public GameObject yaoQing;

		private BaseButton enterbtn1;

		private BaseButton enterbtn2;

		private BaseButton enterbtn3;

		private BaseButton enterbtn4;

		private BaseButton enterbtn5;

		private BaseButton enterbtn6;

		private BaseButton enterbtn7;

		private BaseButton left;

		private BaseButton right;

		private BaseButton left1;

		private BaseButton right1;

		public Text txtName;

		public Text txtLvl;

		public Text txtPro;

		private Toggle toggle;

		public static int nummeb;

		public Dictionary<uint, itemFriendData> RecommendListDic = new Dictionary<uint, itemFriendData>();

		public TabControl tab;

		private GameObject single;

		private GameObject multiPlayer;

		public static int lvl = 0;

		public uint begin_index = 0u;

		public uint end_index = 20u;

		public List<uint> kout = new List<uint>();

		public List<uint> canin = new List<uint>();

		public Dictionary<uint, bool> readyGofb = new Dictionary<uint, bool>();

		private int mode = 0;

		private int diff = 0;

		private Transform tran;

		private Transform tran1;

		public Transform currentTeam;

		public Transform peopleLess;

		private Transform yao;

		private Transform friends;

		private Transform objects;

		private Transform tenSlider;

		private Sprite icon2;

		private Sprite icon3;

		private Sprite icon5;

		public uint readyLtpid;

		public uint readyLdiff;

		private bool needTrues = false;

		private List<a3_currentTeamPanel.ItemMemberObj> itemMemberObjList;

		private Dictionary<uint, string> ltemObjList = new Dictionary<uint, string>();

		private Tween tween = null;

		private Sprite icon;

		private string diffStr = "";

		private bool yesReady = false;

		private uint leader_cid;

		private string str = "currentTeam/currentTeamPanel/right/bottom/btnStart";

		private string str1 = "multiPlayer/contain/exp/enter/Text";

		private string str2 = "multiPlayer/contain/gold/enter/Text";

		private string str3 = "multiPlayer/contain/material/enter/Text";

		private string str4 = "multiPlayer/contain/ghost/enter/Text";

		private bool Toclose = false;

		public override void init()
		{
			a3_counterpart.instance = this;
			this._activies["exp"] = new a3_counterpart_exp(this, "contents/exp");
			this._activies["gold"] = new a3_counterpart_gold(this, "contents/gold");
			this._activies["material"] = new a3_counterpart_mate(this, "contents/material");
			this._activies["mexp"] = new a3_counterpart_multi_exp(this, "multiConDiff/exp");
			this._activies["mgold"] = new a3_counterpart_multi_gold(this, "multiConDiff/gold");
			this._activies["mmaterial"] = new a3_counterpart_multi_mate(this, "multiConDiff/material");
			this._activies["ghost"] = new a3_counterpart_multi_ghost(this, "multiConDiff/ghost");
			this.enterbtn1 = new BaseButton(base.getTransformByPath("scroll_view_fuben/contain/gold/enter"), 1, 1);
			this.enterbtn2 = new BaseButton(base.getTransformByPath("scroll_view_fuben/contain/exp/enter"), 1, 1);
			this.enterbtn3 = new BaseButton(base.getTransformByPath("scroll_view_fuben/contain/material/enter"), 1, 1);
			this.enterbtn4 = new BaseButton(base.getTransformByPath("multiPlayer/contain/gold/enter"), 1, 1);
			this.enterbtn5 = new BaseButton(base.getTransformByPath("multiPlayer/contain/exp/enter"), 1, 1);
			this.enterbtn6 = new BaseButton(base.getTransformByPath("multiPlayer/contain/material/enter"), 1, 1);
			this.enterbtn7 = new BaseButton(base.getTransformByPath("multiPlayer/contain/ghost/enter"), 1, 1);
			this.icon2 = Resources.Load<Sprite>("icon/team/warrior_team");
			this.icon3 = Resources.Load<Sprite>("icon/team/mage_team");
			this.icon5 = Resources.Load<Sprite>("icon/team/assassin_team");
			this.right = new BaseButton(base.getTransformByPath("right"), 1, 1);
			this.left = new BaseButton(base.getTransformByPath("left"), 1, 1);
			this.right1 = new BaseButton(base.getTransformByPath("right1"), 1, 1);
			this.left1 = new BaseButton(base.getTransformByPath("left1"), 1, 1);
			this.currentTeam = base.getTransformByPath("currentTeam/currentTeamPanel");
			this.tenSlider = base.getTransformByPath("ready/yesorno/slider/fill");
			this.peopleLess = base.getTransformByPath("peopelLess");
			this.itemMemberObjList = new List<a3_currentTeamPanel.ItemMemberObj>();
			this.ltemObjList = new Dictionary<uint, string>();
			this.objects = this.currentTeam.FindChild("right/contains");
			this.tenSlider.localScale = new Vector3(0f, 1f, 1f);
			for (int i = 0; i < this.objects.childCount; i++)
			{
				Transform child = this.objects.GetChild(i);
				a3_currentTeamPanel.ItemMemberObj item = new a3_currentTeamPanel.ItemMemberObj(child);
				this.itemMemberObjList.Add(item);
				new BaseButton(base.getTransformByPath("currentTeam/currentTeamPanel/right/contains/" + i + "/empty/btnInvite"), 1, 1).onClick = delegate(GameObject go)
				{
					BaseProxy<FriendProxy>.getInstance().sendOnlineRecommend();
					this.yaoQing.SetActive(true);
				};
			}
			BaseProxy<TeamProxy>.getInstance().SendGetMapTeam(this.begin_index, this.end_index);
			this.single = base.getGameObjectByPath("scroll_view_fuben");
			this.multiPlayer = base.getGameObjectByPath("multiPlayer");
			this.yesNo = base.getGameObjectByPath("team");
			this.less5 = base.getGameObjectByPath("teamLess5");
			this.yaoQing = base.getGameObjectByPath("yaoqing");
			this.yao = base.getTransformByPath("yaoqing/main/scroll/containts");
			this.friends = base.getTransformByPath("haoyou/main/scroll/containts");
			this.tran = base.getTransformByPath("scroll_view_fuben/contain");
			Tween tween;
			this.right.onClick = delegate(GameObject go)
			{
				bool flag = this.tran.localPosition.x < (float)((this.getTransformByPath("scroll_view_fuben/contain").childCount - 3) * 300);
				if (!flag)
				{
					tween = this.tran.DOLocalMoveX(this.tran.localPosition.x - 10f, 0.5f, false);
				}
			};
			this.left.onClick = delegate(GameObject go)
			{
				bool flag = this.tran.localPosition.x > (float)((this.getTransformByPath("scroll_view_fuben/contain").childCount - 3) * 300);
				if (!flag)
				{
					tween = this.tran.DOLocalMoveX(this.tran.localPosition.x + 10f, 0.5f, false);
				}
			};
			this.tran1 = base.getTransformByPath("multiPlayer/contain");
			Tween tween1;
			this.right1.onClick = delegate(GameObject go)
			{
				bool flag = this.tran1.localPosition.x < (float)((this.getTransformByPath("multiPlayer/contain").childCount - 3) * 300);
				if (!flag)
				{
					tween1 = this.tran1.DOLocalMoveX(this.tran1.localPosition.x - 10f, 0.5f, false);
				}
			};
			this.left1.onClick = delegate(GameObject go)
			{
				bool flag = this.tran1.localPosition.x > (float)((this.getTransformByPath("multiPlayer/contain").childCount - 3) * 300);
				if (!flag)
				{
					tween1 = this.tran1.DOLocalMoveX(this.tran1.localPosition.x + 10f, 0.5f, false);
				}
			};
			new BaseButton(base.getTransformByPath("btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.Toclose = true;
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_COUNTERPART);
				a3_currentTeamPanel.leave = false;
			};
			BaseButton arg_467_0 = new BaseButton(base.getTransformByPath("jingbi/Image"), 1, 1);
			Action<GameObject> arg_467_1;
			if ((arg_467_1 = a3_counterpart.<>c.<>9__50_6) == null)
			{
				arg_467_1 = (a3_counterpart.<>c.<>9__50_6 = new Action<GameObject>(a3_counterpart.<>c.<>9.<init>b__50_6));
			}
			arg_467_0.onClick = arg_467_1;
			BaseButton arg_49E_0 = new BaseButton(base.getTransformByPath("zuanshi/Image"), 1, 1);
			Action<GameObject> arg_49E_1;
			if ((arg_49E_1 = a3_counterpart.<>c.<>9__50_7) == null)
			{
				arg_49E_1 = (a3_counterpart.<>c.<>9__50_7 = new Action<GameObject>(a3_counterpart.<>c.<>9.<init>b__50_7));
			}
			arg_49E_0.onClick = arg_49E_1;
			BaseButton arg_4D5_0 = new BaseButton(base.getTransformByPath("bangzuan/Image"), 1, 1);
			Action<GameObject> arg_4D5_1;
			if ((arg_4D5_1 = a3_counterpart.<>c.<>9__50_8) == null)
			{
				arg_4D5_1 = (a3_counterpart.<>c.<>9__50_8 = new Action<GameObject>(a3_counterpart.<>c.<>9.<init>b__50_8));
			}
			arg_4D5_0.onClick = arg_4D5_1;
			new BaseButton(base.getTransformByPath("scroll_view_fuben/contain/gold/enter"), 1, 1).onClick = delegate(GameObject go)
			{
				this.choicePanel = base.getGameObjectByPath("contents/gold");
				this.choicePanel.SetActive(true);
			};
			new BaseButton(base.getTransformByPath("contents/gold/btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.choicePanel.SetActive(false);
			};
			new BaseButton(base.getTransformByPath("scroll_view_fuben/contain/exp/enter"), 1, 1).onClick = delegate(GameObject go)
			{
				this.choicePanel = base.getGameObjectByPath("contents/exp");
				this.choicePanel.SetActive(true);
			};
			new BaseButton(base.getTransformByPath("contents/exp/btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.choicePanel.SetActive(false);
			};
			new BaseButton(base.getTransformByPath("scroll_view_fuben/contain/material/enter"), 1, 1).onClick = delegate(GameObject go)
			{
				this.choicePanel = base.getGameObjectByPath("contents/material");
				this.choicePanel.SetActive(true);
			};
			new BaseButton(base.getTransformByPath("contents/material/btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.choicePanel.SetActive(false);
			};
			new BaseButton(base.getTransformByPath("multiPlayer/contain/exp/enter"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null && BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid != 108u;
				if (flag)
				{
					bool flag2 = !BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
					if (flag2)
					{
						flytxt.instance.fly(ContMgr.getCont("counterpart_1", null), 0, default(Color), null);
						return;
					}
					BaseProxy<TeamProxy>.getInstance().sendobject_change(108);
				}
				this.choicePanel = base.getGameObjectByPath("multiConDiff/exp");
				this.mode = 2;
				Variant variant2 = SvrLevelConfig.instacne.get_level_data(108u);
				this.currentTeam.FindChild("title").GetComponent<Text>().text = variant2["name"];
				this.cTeam();
			};
			new BaseButton(base.getTransformByPath("multiPlayer/contain/gold/enter"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null && BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid != 109u;
				if (flag)
				{
					bool flag2 = !BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
					if (flag2)
					{
						flytxt.instance.fly(ContMgr.getCont("counterpart_1", null), 0, default(Color), null);
						return;
					}
					BaseProxy<TeamProxy>.getInstance().sendobject_change(109);
				}
				this.choicePanel = base.getGameObjectByPath("multiConDiff/gold");
				this.mode = 1;
				Variant variant2 = SvrLevelConfig.instacne.get_level_data(109u);
				this.currentTeam.FindChild("title").GetComponent<Text>().text = variant2["name"];
				this.cTeam();
			};
			new BaseButton(base.getTransformByPath("multiPlayer/contain/material/enter"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null && BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid != 110u;
				if (flag)
				{
					bool flag2 = !BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
					if (flag2)
					{
						flytxt.instance.fly(ContMgr.getCont("counterpart_1", null), 0, default(Color), null);
						return;
					}
					BaseProxy<TeamProxy>.getInstance().sendobject_change(110);
				}
				this.choicePanel = base.getGameObjectByPath("multiConDiff/material");
				this.mode = 3;
				Variant variant2 = SvrLevelConfig.instacne.get_level_data(110u);
				this.currentTeam.FindChild("title").GetComponent<Text>().text = variant2["name"];
				this.cTeam();
			};
			new BaseButton(base.getTransformByPath("multiPlayer/contain/ghost/enter"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null && BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid != 111u;
				if (flag)
				{
					bool flag2 = !BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
					if (flag2)
					{
						flytxt.instance.fly(ContMgr.getCont("counterpart_1", null), 0, default(Color), null);
						return;
					}
					BaseProxy<TeamProxy>.getInstance().sendobject_change(111);
				}
				this.choicePanel = base.getGameObjectByPath("multiConDiff/ghost");
				this.mode = 4;
				Variant variant2 = SvrLevelConfig.instacne.get_level_data(111u);
				this.currentTeam.FindChild("title").GetComponent<Text>().text = variant2["name"];
				this.cTeam();
			};
			new BaseButton(base.getTransformByPath("multiConDiff/gold/btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.choicePanel.SetActive(false);
			};
			new BaseButton(base.getTransformByPath("multiConDiff/exp/btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.choicePanel.SetActive(false);
			};
			new BaseButton(base.getTransformByPath("multiConDiff/material/btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.choicePanel.SetActive(false);
			};
			new BaseButton(base.getTransformByPath("multiConDiff/ghost/btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.choicePanel.SetActive(false);
			};
			new BaseButton(base.getTransformByPath("team/yesorno/shd"), 1, 1).onClick = delegate(GameObject go)
			{
				this.onBtnCreateClick(go);
				base.getGameObjectByPath("currentTeam").SetActive(true);
				base.getGameObjectByPath("team").SetActive(false);
			};
			new BaseButton(base.getTransformByPath("team/yesorno/zid"), 1, 1).onClick = delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(0);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SPEEDTEAM, arrayList, false);
				base.getGameObjectByPath("team").SetActive(false);
			};
			new BaseButton(base.getTransformByPath("teamLess5/yesorno/yes"), 1, 1).onClick = delegate(GameObject go)
			{
				this.less5.SetActive(false);
				BaseProxy<FriendProxy>.getInstance().sendOnlineRecommend();
				this.yaoQing.SetActive(true);
			};
			new BaseButton(base.getTransformByPath("team/yesorno/no"), 1, 1).onClick = delegate(GameObject go)
			{
				this.yesNo.SetActive(false);
			};
			new BaseButton(base.getTransformByPath("teamLess5/yesorno/no"), 1, 1).onClick = delegate(GameObject go)
			{
				this.less5.SetActive(false);
			};
			new BaseButton(base.getTransformByPath("yaoqing/main/title/title/close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.yaoQing.SetActive(false);
				for (int j = 0; j < this.yao.childCount; j++)
				{
					UnityEngine.Object.Destroy(this.yao.GetChild(j).gameObject);
				}
			};
			bool isons = false;
			bool isonline = false;
			new BaseButton(base.getTransformByPath("yaoqing/bottom/btnSelectAll"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = this.yao != null && this.yao.childCount > 0 && !isonline;
				if (flag)
				{
					for (int j = 0; j < this.yao.childCount; j++)
					{
						Transform child2 = this.yao.GetChild(j);
						child2.FindChild("Toggle").GetComponent<Toggle>().isOn = true;
					}
					isonline = true;
					this.getTransformByPath("yaoqing/bottom/btnSelectAll/Text").GetComponent<Text>().text = "取消";
				}
				else
				{
					bool flag2 = (this.yao != null && this.yao.childCount > 0) & isonline;
					if (flag2)
					{
						for (int k = 0; k < this.yao.childCount; k++)
						{
							Transform child3 = this.yao.GetChild(k);
							child3.FindChild("Toggle").GetComponent<Toggle>().isOn = false;
						}
						isonline = false;
						this.getTransformByPath("yaoqing/bottom/btnSelectAll/Text").GetComponent<Text>().text = "全选";
					}
				}
			};
			new BaseButton(base.getTransformByPath("yaoqing/bottom/btnInvite"), 1, 1).onClick = delegate(GameObject go)
			{
				for (int j = 0; j < this.yao.childCount; j++)
				{
					Transform child2 = this.yao.GetChild(j);
					foreach (KeyValuePair<uint, string> current in this.ltemObjList)
					{
						current.Value.Equals(child2.FindChild("texts/txtNickName").GetComponent<Text>().text);
						bool flag = current.Value == child2.FindChild("texts/txtNickName").GetComponent<Text>().text && child2.FindChild("Toggle").GetComponent<Toggle>().isOn;
						if (flag)
						{
							uint key = current.Key;
							BaseProxy<TeamProxy>.getInstance().SendInvite(key);
						}
					}
				}
				this.yaoQing.SetActive(false);
				for (int k = 0; k < this.yao.childCount; k++)
				{
					UnityEngine.Object.Destroy(this.yao.GetChild(k).gameObject);
				}
			};
			new BaseButton(base.getTransformByPath("haoyou/bottom/btnSelectAll"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = this.friends != null && this.friends.childCount > 0 && !isons;
				if (flag)
				{
					for (int j = 0; j < this.friends.childCount; j++)
					{
						Transform child2 = this.friends.GetChild(j);
						child2.FindChild("Toggle").GetComponent<Toggle>().isOn = true;
					}
					isons = true;
					this.getTransformByPath("haoyou/bottom/btnSelectAll/Text").GetComponent<Text>().text = "取消";
				}
				else
				{
					bool flag2 = (this.friends != null && this.friends.childCount > 0) & isons;
					if (flag2)
					{
						for (int k = 0; k < this.friends.childCount; k++)
						{
							Transform child3 = this.friends.GetChild(k);
							child3.FindChild("Toggle").GetComponent<Toggle>().isOn = false;
						}
						isons = false;
						this.getTransformByPath("haoyou/bottom/btnSelectAll/Text").GetComponent<Text>().text = "全选";
					}
				}
			};
			new BaseButton(base.getTransformByPath("haoyou/bottom/btnInvite"), 1, 1).onClick = delegate(GameObject go)
			{
				for (int j = 0; j < this.friends.childCount; j++)
				{
					Transform child2 = this.friends.GetChild(j);
					foreach (KeyValuePair<uint, string> current in this.ltemObjList)
					{
						current.Value.Equals(child2.FindChild("texts/txtNickName").GetComponent<Text>().text);
						bool flag = current.Value == child2.FindChild("texts/txtNickName").GetComponent<Text>().text && child2.FindChild("Toggle").GetComponent<Toggle>().isOn;
						if (flag)
						{
							uint key = current.Key;
							BaseProxy<TeamProxy>.getInstance().SendInvite(key);
						}
					}
				}
				base.getGameObjectByPath("haoyou").SetActive(false);
				for (int k = 0; k < this.friends.childCount; k++)
				{
					UnityEngine.Object.Destroy(this.friends.GetChild(k).gameObject);
				}
			};
			new BaseButton(base.getTransformByPath("currentTeam/currentTeamPanel/right/bottom/btnStart"), 1, 1).onClick = delegate(GameObject go)
			{
				bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
				if (meIsCaptain)
				{
					bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count < 0;
					if (flag)
					{
						base.getGameObjectByPath("teamLess5").SetActive(true);
					}
					else
					{
						base.getGameObjectByPath("teamLess5").SetActive(false);
						base.getGameObjectByPath("currentTeam").SetActive(false);
						switch (this.mode)
						{
						case 1:
							base.getGameObjectByPath("multiConDiff/gold").SetActive(true);
							break;
						case 2:
							base.getGameObjectByPath("multiConDiff/exp").SetActive(true);
							break;
						case 3:
							base.getGameObjectByPath("multiConDiff/material").SetActive(true);
							break;
						case 4:
							base.getGameObjectByPath("multiConDiff/ghost").SetActive(true);
							break;
						}
					}
				}
				else
				{
					flytxt.instance.fly("等待中", 0, default(Color), null);
				}
			};
			new BaseButton(base.getTransformByPath("currentTeam/currentTeamPanel/right/bottom/friend"), 1, 1).onClick = delegate(GameObject go)
			{
				BaseProxy<FriendProxy>.getInstance().sendfriendlist(FriendProxy.FriendType.FRIEND);
				base.getGameObjectByPath("haoyou").SetActive(true);
			};
			new BaseButton(base.getTransformByPath("haoyou/main/title/title/close"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("haoyou").SetActive(false);
				for (int j = 0; j < this.friends.childCount; j++)
				{
					UnityEngine.Object.Destroy(this.friends.GetChild(j).gameObject);
				}
			};
			new BaseButton(base.getTransformByPath("currentTeam/currentTeamPanel/right/bottom/btnQuitTeam"), 1, 1).onClick = delegate(GameObject go)
			{
				this.onBtnQuitClick(go);
				base.getGameObjectByPath("currentTeam").SetActive(false);
			};
			new BaseButton(base.getTransformByPath("currentTeam/currentTeamPanel/close"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("currentTeam").SetActive(false);
			};
			new BaseButton(base.getTransformByPath("peopelLess/yesorno/yes"), 1, 1).onClick = delegate(GameObject go)
			{
				for (int j = 0; j < this.kout.Count; j++)
				{
					BaseProxy<TeamProxy>.getInstance().SendKickOut(this.kout[j]);
				}
				this.peopleLess.gameObject.SetActive(false);
				base.getGameObjectByPath("currentTeam").SetActive(true);
			};
			new BaseButton(base.getTransformByPath("peopelLess/yesorno/no"), 1, 1).onClick = delegate(GameObject go)
			{
				this.peopleLess.gameObject.SetActive(false);
				base.getGameObjectByPath("currentTeam").SetActive(true);
			};
			new BaseButton(base.getTransformByPath("ready/yesorno/yes"), 1, 1).onClick = delegate(GameObject go)
			{
				this.yesReady = true;
				BaseProxy<TeamProxy>.getInstance().SendReady(true, this.readyLtpid, this.readyLdiff);
				base.getGameObjectByPath("currentTeam").SetActive(true);
				Transform transformByPath = base.getTransformByPath("ready/yesorno/show");
				bool flag = !BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
				if (flag)
				{
					base.getButtonByPath("ready/yesorno/yes").interactable = false;
					for (int j = 0; j < transformByPath.childCount; j++)
					{
						bool flag2 = !transformByPath.GetChild(j).transform.FindChild("yes").gameObject.activeSelf && !this.needTrues;
						if (flag2)
						{
							transformByPath.GetChild(j).transform.FindChild("yes").gameObject.SetActive(true);
							transformByPath.GetChild(j).transform.FindChild("name").GetComponent<Text>().text = "自己";
							this.needTrues = true;
							break;
						}
					}
				}
			};
			new BaseButton(base.getTransformByPath("ready/yesorno/no"), 1, 1).onClick = delegate(GameObject go)
			{
				BaseProxy<TeamProxy>.getInstance().SendReady(false, this.readyLtpid, this.readyLdiff);
				this.sendfalse(true);
				this.tenSlider.DOKill(false);
				this.tenSeconed(false);
			};
			bool bein = ModelBase<A3_TeamModel>.getInstance().bein;
			if (bein)
			{
				Variant variant = SvrLevelConfig.instacne.get_level_data(ModelBase<A3_TeamModel>.getInstance().ltpids);
				this.currentTeam.FindChild("title").GetComponent<Text>().text = variant["name"];
				base.getGameObjectByPath("currentTeam").SetActive(true);
			}
			this.tab = new TabControl();
			this.tab.onClickHanle = new Action<TabControl>(this.ontab);
			this.tab.create(base.getGameObjectByPath("tabs"), base.gameObject, 0, 0, false);
			this.CheckLock();
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_NEWMEMBERJOIN, new Action<GameEvent>(this.onNewMemberJoin));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_KICKOUT, new Action<GameEvent>(this.onKickOut));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_CHANGECAPTAIN, new Action<GameEvent>(this.changeCap));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_CREATETEAM, new Action<GameEvent>(this.onCreatTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_LEAVETEAM, new Action<GameEvent>(this.onLeaveTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_NOTICEHAVEMEMBERLEAVE, new Action<GameEvent>(this.noticeHaveLeave));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_AFFIRMINVITE, new Action<GameEvent>(this.noticeInvite));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_TEAMOBJECT_CHANGE, new Action<GameEvent>(this.changeOBJ));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_TEAM_READY, new Action<GameEvent>(this.readyGo));
			this.changePos();
			this.butoText();
		}

		private void tenSeconed(bool comp)
		{
			if (comp)
			{
				this.tween = this.tenSlider.DOScaleX(1f, 9f);
				this.tween.OnComplete(delegate
				{
					this.OnComplete();
				});
			}
			else
			{
				this.tween = this.tenSlider.DOScaleX(0f, 0.01f);
				this.tween.OnComplete(delegate
				{
					this.OnComp();
				});
			}
		}

		private void OnComplete()
		{
		}

		private void OnComp()
		{
		}

		private void sendfalse(bool fly)
		{
			this.tenSlider.localScale = new Vector3(0f, 1f, 1f);
			base.getGameObjectByPath("ready").SetActive(false);
			base.getGameObjectByPath("currentTeam").SetActive(true);
			Transform transformByPath = base.getTransformByPath("ready/yesorno/show");
			for (int i = 1; i < transformByPath.childCount; i++)
			{
				bool activeSelf = transformByPath.GetChild(i).transform.FindChild("yes").gameObject.activeSelf;
				if (activeSelf)
				{
					transformByPath.GetChild(i).transform.FindChild("yes").gameObject.SetActive(false);
					transformByPath.GetChild(i).transform.FindChild("name").GetComponent<Text>().text = string.Empty;
				}
			}
			this.yesReady = false;
			this.readyLtpid = 0u;
			this.readyLdiff = 0u;
			this.diffStr = "";
			this.readyGofb.Clear();
			this.needTrues = false;
			if (fly)
			{
				flytxt.instance.fly("10秒内有人没有确认前往", 0, default(Color), null);
			}
		}

		public void tenSen()
		{
			this.tenSlider.DOKill(false);
			this.tenSeconed(true);
		}

		private void onLeaveTeam(GameEvent e)
		{
			base.getButtonByPath("ready/yesorno/yes").interactable = true;
			base.getGameObjectByPath("ready").SetActive(false);
			base.getGameObjectByPath("currentTeam").SetActive(false);
			this.yesReady = false;
			ModelBase<A3_TeamModel>.getInstance().cidName.Clear();
			this.tenSlider.DOKill(false);
			this.tenSeconed(false);
			this.butoText();
			this.changePos();
		}

		private void onCreatTeam(GameEvent e)
		{
			ModelBase<A3_TeamModel>.getInstance().cidName.Clear();
			bool flag = e.data["ltpid"] == 108 || e.data["ltpid"] == 109 || e.data["ltpid"] == 110 || e.data["ltpid"] == 111;
			if (flag)
			{
				this.butoText();
				this.changePos();
				bool flag2 = !ModelBase<A3_TeamModel>.getInstance().cidName.ContainsKey(ModelBase<PlayerModel>.getInstance().cid);
				if (flag2)
				{
					ModelBase<A3_TeamModel>.getInstance().cidName.Add(ModelBase<PlayerModel>.getInstance().cid, ModelBase<PlayerModel>.getInstance().name);
				}
				this.tenSlider.DOKill(false);
				this.tenSeconed(false);
			}
		}

		private void changeOBJ(GameEvent e)
		{
			switch (e.data["ltpid"]._int)
			{
			case 108:
				BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid = 108u;
				this.butoText();
				this.changePos();
				break;
			case 109:
				BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid = 109u;
				this.butoText();
				this.changePos();
				break;
			case 110:
				BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid = 110u;
				this.butoText();
				this.changePos();
				break;
			case 111:
				BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid = 111u;
				this.butoText();
				this.changePos();
				break;
			default:
				this.butoText();
				this.changePos();
				break;
			}
			Variant variant = SvrLevelConfig.instacne.get_level_data(e.data["ltpid"]);
			bool flag = variant != null;
			if (flag)
			{
				this.currentTeam.FindChild("title").GetComponent<Text>().text = variant["name"];
			}
		}

		private void readyGo(GameEvent e)
		{
			this.changePos();
			this.butoText();
			Transform transformByPath = base.getTransformByPath("ready/yesorno/show");
			bool flag = e.data["ready"];
			if (flag)
			{
				base.getGameObjectByPath("ready").SetActive(true);
				base.getGameObjectByPath("currentTeam").SetActive(true);
				this.readyLdiff = e.data["ldiff"];
				this.readyLtpid = e.data["ltpid"];
				uint num = e.data["cid"];
				base.getGameObjectByPath("ready").SetActive(true);
				bool flag2 = false;
				bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
				if (meIsCaptain)
				{
					this.yesReady = true;
					transformByPath.FindChild("0/name").GetComponent<Text>().text = "自己";
				}
				else
				{
					transformByPath.FindChild("0/name").GetComponent<Text>().text = ModelBase<A3_TeamModel>.getInstance().cidName[BaseProxy<TeamProxy>.getInstance().MyTeamData.leaderCid];
				}
				bool flag3 = num == BaseProxy<TeamProxy>.getInstance().MyTeamData.leaderCid;
				if (flag3)
				{
					this.tenSlider.DOKill(false);
					this.tenSeconed(true);
				}
				bool flag4 = this.yesReady;
				if (flag4)
				{
					base.getButtonByPath("ready/yesorno/yes").interactable = false;
				}
				else
				{
					base.getButtonByPath("ready/yesorno/yes").interactable = true;
				}
				bool flag5 = num != BaseProxy<TeamProxy>.getInstance().MyTeamData.leaderCid && !this.readyGofb.ContainsKey(num);
				if (flag5)
				{
					for (int i = 0; i < transformByPath.childCount; i++)
					{
						bool flag6 = !transformByPath.GetChild(i).transform.FindChild("yes").gameObject.activeSelf && !flag2;
						if (flag6)
						{
							transformByPath.GetChild(i).transform.FindChild("yes").gameObject.SetActive(true);
							transformByPath.GetChild(i).transform.FindChild("name").GetComponent<Text>().text = ModelBase<A3_TeamModel>.getInstance().cidName[num];
							flag2 = true;
							bool flag7 = !this.readyGofb.ContainsKey(num);
							if (flag7)
							{
								this.readyGofb.Add(num, e.data["ready"]);
							}
						}
					}
				}
				switch (this.readyLdiff)
				{
				case 1u:
					this.diffStr = "简单";
					break;
				case 2u:
					this.diffStr = "普通";
					break;
				case 3u:
					this.diffStr = "困难";
					break;
				case 4u:
					this.diffStr = "地狱";
					break;
				}
				Variant variant = SvrLevelConfig.instacne.get_level_data(this.readyLtpid);
				base.getTransformByPath("ready/yesorno/Text/name").GetComponent<Text>().text = variant["name"] + "--" + this.diffStr;
				bool flag8 = this.readyGofb.Count == BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count - 1;
				if (flag8)
				{
					this.tenSlider.DOKill(false);
					this.tenSeconed(false);
					Variant variant2 = new Variant();
					variant2["npcid"] = 0;
					variant2["ltpid"] = this.readyLtpid;
					variant2["diff_lvl"] = this.readyLdiff;
					a3_counterpart.lvl = variant2["diff_lvl"];
					BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant2);
					base.getGameObjectByPath("ready").SetActive(false);
					base.getGameObjectByPath("currentTeam").SetActive(false);
					for (int j = 1; j < transformByPath.childCount; j++)
					{
						bool activeSelf = transformByPath.GetChild(j).transform.FindChild("yes").gameObject.activeSelf;
						if (activeSelf)
						{
							transformByPath.GetChild(j).transform.FindChild("yes").gameObject.SetActive(false);
							transformByPath.GetChild(j).transform.FindChild("name").GetComponent<Text>().text = string.Empty;
						}
					}
					this.readyLtpid = 0u;
					this.readyLdiff = 0u;
					this.diffStr = "";
					this.readyGofb.Clear();
					this.needTrues = false;
				}
			}
			else
			{
				this.sendfalse(true);
				this.tenSlider.DOKill(false);
				this.tenSeconed(false);
			}
		}

		private void onKickOut(GameEvent e)
		{
			this.changePos();
			this.butoText();
			ModelBase<A3_TeamModel>.getInstance().cidName.Remove(e.data["cid"]);
			base.getGameObjectByPath("ready").SetActive(false);
			base.getGameObjectByPath("currentTeam").SetActive(false);
		}

		private void changeCap(GameEvent e)
		{
			this.changePos();
			this.butoText();
		}

		private void noticeHaveLeave(GameEvent e)
		{
			this.butoText();
			this.changePos();
			ModelBase<A3_TeamModel>.getInstance().cidName.Remove(e.data["cid"]);
		}

		private void noticeInvite(GameEvent e)
		{
			bool flag = e.data["ltpid"] == 108 || e.data["ltpid"] == 109 || e.data["ltpid"] == 110 || e.data["ltpid"] == 111;
			if (flag)
			{
				this.leader_cid = e.data["leader_cid"]._uint;
				this.changePos();
				this.butoText();
				this.yesReady = false;
				foreach (ItemTeamData current in BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList)
				{
					bool flag2 = !ModelBase<A3_TeamModel>.getInstance().cidName.ContainsKey(current.cid);
					if (flag2)
					{
						ModelBase<A3_TeamModel>.getInstance().cidName.Add(current.cid, current.name);
					}
				}
				bool flag3 = !ModelBase<A3_TeamModel>.getInstance().cidName.ContainsKey(ModelBase<PlayerModel>.getInstance().cid);
				if (flag3)
				{
					ModelBase<A3_TeamModel>.getInstance().cidName.Add(ModelBase<PlayerModel>.getInstance().cid, ModelBase<PlayerModel>.getInstance().name);
				}
				Variant variant = SvrLevelConfig.instacne.get_level_data(e.data["ltpid"]);
				bool flag4 = variant != null;
				if (flag4)
				{
					this.currentTeam.FindChild("title").GetComponent<Text>().text = variant["name"];
				}
			}
		}

		private void onFriendList(GameEvent e)
		{
			Dictionary<uint, itemFriendData> friendDataList = BaseProxy<FriendProxy>.getInstance().FriendDataList;
			this.listFriend();
		}

		private void onGetTeamListInfo(GameEvent e)
		{
			ItemTeamMemberData mapItemTeamData = BaseProxy<TeamProxy>.getInstance().mapItemTeamData;
			this.changePos();
			this.butoText();
		}

		private void onNewMemberJoin(GameEvent e)
		{
			Variant data = e.data;
			uint num = data["cid"];
			string text = data["name"];
			uint num2 = data["lvl"];
			uint zhuan = data["zhuan"];
			uint carr = data["carr"];
			uint combpt = data["combpt"];
			ItemTeamData itemTeamData = new ItemTeamData();
			itemTeamData.cid = num;
			itemTeamData.name = text;
			itemTeamData.lvl = num2;
			itemTeamData.zhuan = zhuan;
			itemTeamData.carr = carr;
			itemTeamData.combpt = (int)combpt;
			itemTeamData.isCaptain = false;
			itemTeamData.online = true;
			itemTeamData.teamId = BaseProxy<TeamProxy>.getInstance().MyTeamData.teamId;
			this.changePos();
			this.butoText();
			bool flag = !ModelBase<A3_TeamModel>.getInstance().cidName.ContainsKey(data["cid"]);
			if (flag)
			{
				ModelBase<A3_TeamModel>.getInstance().cidName.Add(num, text);
			}
		}

		private void commandList(GameEvent e)
		{
			List<itemFriendData> recommendDataList = BaseProxy<FriendProxy>.getInstance().RecommendDataList;
			recommendDataList.Sort(new Comparison<itemFriendData>(this.SortItemFriendData));
			this.listOnline();
			this.changePos();
			this.butoText();
		}

		private void onMoneyChange(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("money");
			if (flag)
			{
				this.refreshMoney();
			}
			bool flag2 = data.ContainsKey("yb");
			if (flag2)
			{
				this.refreshGold();
			}
			bool flag3 = data.ContainsKey("bndyb");
			if (flag3)
			{
				this.refreshGift();
			}
		}

		private void teamList()
		{
			ItemTeamMemberData mapItemTeamData = BaseProxy<TeamProxy>.getInstance().mapItemTeamData;
			foreach (ItemTeamData current in mapItemTeamData.itemTeamDataList)
			{
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData == null;
				if (flag)
				{
					BaseProxy<TeamProxy>.getInstance().SendApplyJoinTeam(current.teamId);
				}
			}
		}

		private void listFriend()
		{
			Transform transform = base.transform.FindChild("haoyou/itemReserFriend");
			Dictionary<uint, itemFriendData> friendDataList = BaseProxy<FriendProxy>.getInstance().FriendDataList;
			RectTransform component = base.getTransformByPath("haoyou/main/scroll/containts").GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component.sizeDelta.x, (float)friendDataList.Count * 60f);
			foreach (KeyValuePair<uint, itemFriendData> current in friendDataList)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
				Transform transform2 = gameObject.transform;
				gameObject.SetActive(true);
				transform2.SetParent(base.getTransformByPath("haoyou/main/scroll/containts"));
				this.txtName = transform2.transform.FindChild("texts/txtNickName").GetComponent<Text>();
				this.txtLvl = transform2.transform.FindChild("texts/txtLevel").GetComponent<Text>();
				this.txtPro = transform2.transform.FindChild("texts/txtProfessional").GetComponent<Text>();
				this.set(current.Value);
				gameObject.transform.localScale = Vector3.one;
				this.toggle = transform2.transform.FindChild("Toggle").GetComponent<Toggle>();
				bool flag = this.ltemObjList.ContainsKey(current.Value.cid);
				if (!flag)
				{
					this.ltemObjList.Add(current.Value.cid, current.Value.name);
				}
			}
		}

		public void changePos()
		{
			bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null && (BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid == 108u || BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid == 109u || BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid == 110u || BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid == 111u);
			if (flag)
			{
				a3_counterpart.nummeb = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
				int count = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
				int num = 1;
				bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
				if (meIsCaptain)
				{
					switch (ModelBase<PlayerModel>.getInstance().profession)
					{
					case 2:
						this.icon = this.icon2;
						break;
					case 3:
						this.icon = this.icon3;
						break;
					case 5:
						this.icon = this.icon5;
						break;
					}
					this.objects.FindChild("0/noEmpty/texts/icon/leader").GetComponent<Image>().sprite = this.icon;
					this.objects.FindChild("0/noEmpty/btnRemoveTeam").gameObject.SetActive(false);
					for (int i = 1; i < count; i++)
					{
						this.objects.FindChild(i + "/noEmpty/btnRemoveTeam").gameObject.SetActive(true);
					}
					for (int j = count; j < 5; j++)
					{
						this.objects.FindChild(j + "/empty/btnInvite").gameObject.SetActive(true);
					}
				}
				else
				{
					for (int k = 0; k < count; k++)
					{
						this.objects.FindChild(k + "/noEmpty/btnRemoveTeam").gameObject.SetActive(false);
					}
					for (int l = count; l < 5; l++)
					{
						this.objects.FindChild(l + "/empty/btnInvite").gameObject.SetActive(false);
					}
				}
				bool flag2 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null && BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count > 0;
				if (flag2)
				{
					int count2 = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
					for (int m = 0; m < count2; m++)
					{
						this.objects.FindChild(m + "/noEmpty").gameObject.SetActive(true);
						this.objects.FindChild(m + "/empty").gameObject.SetActive(false);
					}
					for (int n = count2; n < 5; n++)
					{
						this.objects.FindChild(n + "/noEmpty").gameObject.SetActive(false);
						this.objects.FindChild(n + "/empty").gameObject.SetActive(true);
					}
				}
				using (List<ItemTeamData>.Enumerator enumerator = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ItemTeamData item = enumerator.Current;
						switch (item.carr)
						{
						case 2u:
							this.icon = this.icon2;
							break;
						case 3u:
							this.icon = this.icon3;
							break;
						case 5u:
							this.icon = this.icon5;
							break;
						}
						IL_3E6:
						bool isCaptain = item.isCaptain;
						if (isCaptain)
						{
							this.objects.FindChild("0/noEmpty/iconCaptain").gameObject.SetActive(true);
							this.objects.FindChild("0/noEmpty/texts/icon/leader").GetComponent<Image>().sprite = this.icon;
							this.objects.FindChild("0/noEmpty/texts/txtName/Text").GetComponent<Text>().text = item.name;
							this.objects.FindChild("0/noEmpty/texts/txtLvl/Text").GetComponent<Text>().text = string.Concat(new object[]
							{
								item.zhuan,
								"转",
								item.lvl,
								"级"
							});
							this.objects.FindChild("0/noEmpty/texts/txtCombat/Text").GetComponent<Text>().text = item.combpt.ToString();
						}
						else
						{
							bool flag3 = num < count;
							if (flag3)
							{
								this.objects.FindChild(num + "/noEmpty/iconCaptain").gameObject.SetActive(false);
								this.objects.FindChild(num + "/noEmpty/texts/icon/leader").GetComponent<Image>().sprite = this.icon;
								this.objects.FindChild(num + "/noEmpty/texts/txtName/Text").GetComponent<Text>().text = item.name;
								this.objects.FindChild(num + "/noEmpty/texts/txtLvl/Text").GetComponent<Text>().text = string.Concat(new object[]
								{
									item.zhuan,
									"转",
									item.lvl,
									"级"
								});
								this.objects.FindChild(num + "/noEmpty/texts/txtCombat/Text").GetComponent<Text>().text = item.combpt.ToString();
								new BaseButton(this.objects.FindChild(num + "/noEmpty/btnRemoveTeam"), 1, 1).onClick = delegate(GameObject go)
								{
									BaseProxy<TeamProxy>.getInstance().SendKickOut(item.cid);
									flytxt.instance.fly(item.name + "已被请离队伍", 0, default(Color), null);
								};
								num++;
							}
						}
						continue;
						goto IL_3E6;
					}
				}
			}
		}

		private void cTeam()
		{
			this.changePos();
			bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag)
			{
				base.getGameObjectByPath("team").SetActive(false);
				base.getGameObjectByPath("currentTeam").SetActive(true);
			}
			else
			{
				base.getGameObjectByPath("currentTeam").SetActive(false);
				base.getGameObjectByPath("multiConDiff/gold").SetActive(false);
				base.getGameObjectByPath("team").SetActive(true);
			}
		}

		private void butoText()
		{
			bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag)
			{
				bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
				if (meIsCaptain)
				{
					base.getTransformByPath(this.str).FindChild("Text").GetComponent<Text>().text = "开始";
				}
				else
				{
					base.getTransformByPath(this.str).FindChild("Text").GetComponent<Text>().text = "准备";
				}
				base.getTransformByPath(this.str4).GetComponent<Text>().text = "我的队伍";
				base.getTransformByPath(this.str2).GetComponent<Text>().text = "我的队伍";
				base.getTransformByPath(this.str3).GetComponent<Text>().text = "我的队伍";
				base.getTransformByPath(this.str1).GetComponent<Text>().text = "我的队伍";
			}
			else
			{
				base.getTransformByPath(this.str1).GetComponent<Text>().text = "立即组队";
				base.getTransformByPath(this.str2).GetComponent<Text>().text = "立即组队";
				base.getTransformByPath(this.str3).GetComponent<Text>().text = "立即组队";
				base.getTransformByPath(this.str4).GetComponent<Text>().text = "立即组队";
			}
		}

		public override void onShowed()
		{
			this.Toclose = false;
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_RECOMMEND, new Action<GameEvent>(this.commandList));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_TEAMLISTINFO, new Action<GameEvent>(this.onGetTeamListInfo));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_FRIENDLIST, new Action<GameEvent>(this.onFriendList));
			UIClient.instance.addEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			this.refreshMoney();
			this.refreshGold();
			this.refreshGift();
			this.refreshGoldTimes();
			this.refreshExpTimes();
			this.refreshGhostTimes();
			this.refreshMateTimes();
			this.CheckLock();
			this.OpenGold();
			this.OpenFSWZ();
			bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag)
			{
				this.butoText();
				this.changePos();
			}
			else
			{
				bool flag2 = BaseProxy<TeamProxy>.getInstance().MyTeamData == null;
				if (flag2)
				{
					this.changePos();
					this.butoText();
				}
			}
			bool flag3 = this.uiData != null;
			if (flag3)
			{
				int idx = (int)this.uiData[0];
				this.changetab(idx);
			}
			bool flag4 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag4)
			{
				a3_counterpart.nummeb = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count;
				base.getGameObjectByPath("ready").SetActive(false);
			}
			bool flag5 = GRMap.GAME_CAMERA != null;
			if (flag5)
			{
				GRMap.GAME_CAMERA.SetActive(false);
			}
		}

		public override void onClosed()
		{
			this.refreshGoldTimes();
			this.refreshExpTimes();
			this.refreshGhostTimes();
			this.refreshMateTimes();
			BaseProxy<FriendProxy>.getInstance().removeEventListener(FriendProxy.EVENT_RECOMMEND, new Action<GameEvent>(this.commandList));
			BaseProxy<TeamProxy>.getInstance().removeEventListener(TeamProxy.EVENT_TEAMLISTINFO, new Action<GameEvent>(this.onGetTeamListInfo));
			BaseProxy<FriendProxy>.getInstance().removeEventListener(FriendProxy.EVENT_FRIENDLIST, new Action<GameEvent>(this.onFriendList));
			UIClient.instance.removeEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			Transform transformByPath = base.getTransformByPath("ready/yesorno/show");
			for (int i = 1; i < transformByPath.childCount; i++)
			{
				bool activeSelf = transformByPath.GetChild(i).transform.FindChild("yes").gameObject.activeSelf;
				if (activeSelf)
				{
					transformByPath.GetChild(i).transform.FindChild("yes").gameObject.SetActive(false);
					transformByPath.GetChild(i).transform.FindChild("name").GetComponent<Text>().text = string.Empty;
				}
			}
			base.getGameObjectByPath("currentTeam").SetActive(false);
			this.needTrues = false;
			GRMap.GAME_CAMERA.SetActive(true);
			InterfaceMgr.getInstance().itemToWin(this.Toclose, this.uiName);
		}

		private void Update()
		{
			bool flag = this.choicePanel != null && this.choicePanel.activeSelf;
			if (flag)
			{
				bool flag2 = a3_counterpart_gold.open || a3_counterpart_exp.open || a3_counterpart_mate.open || a3_counterpart_multi_gold.open || a3_counterpart_multi_exp.open || a3_counterpart_multi_mate.open || a3_counterpart_multi_ghost.open;
				if (flag2)
				{
					this.choicePanel.SetActive(false);
				}
			}
			bool flag3 = this.choicePanel != null && !this.choicePanel.activeSelf;
			if (flag3)
			{
				a3_counterpart_gold.open = false;
				a3_counterpart_exp.open = false;
				a3_counterpart_mate.open = false;
				a3_counterpart_multi_gold.open = false;
				a3_counterpart_multi_exp.open = false;
				a3_counterpart_multi_mate.open = false;
				a3_counterpart_multi_ghost.open = false;
			}
			bool flag4 = this.tenSlider.localScale.x == 1f;
			if (flag4)
			{
				this.tenSlider.localScale = new Vector3(0f, 1f, 1f);
				BaseProxy<TeamProxy>.getInstance().SendReady(false, this.readyLtpid, this.readyLdiff);
				this.sendfalse(false);
			}
		}

		public void ontab(TabControl t)
		{
			int seletedIndex = t.getSeletedIndex();
			this.changetab(seletedIndex);
		}

		private void changetab(int idx)
		{
			bool flag = idx == 0;
			if (flag)
			{
				base.getGameObjectByPath("tabs/single").GetComponent<Button>().interactable = true;
				base.getGameObjectByPath("tabs/multiplayer").GetComponent<Button>().interactable = false;
				this.multiPlayer.SetActive(false);
				this.single.SetActive(true);
				this.right1.gameObject.SetActive(false);
				this.left1.gameObject.SetActive(false);
				this.right.gameObject.SetActive(true);
				this.left.gameObject.SetActive(true);
			}
			else
			{
				base.getGameObjectByPath("tabs/single").GetComponent<Button>().interactable = false;
				base.getGameObjectByPath("tabs/multiplayer").GetComponent<Button>().interactable = true;
				this.single.SetActive(false);
				this.multiPlayer.SetActive(true);
				this.right.gameObject.SetActive(false);
				this.left.gameObject.SetActive(false);
				this.right1.gameObject.SetActive(true);
				this.left1.gameObject.SetActive(true);
			}
		}

		public void refreshGoldTimes()
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(102u);
			int num = variant["daily_cnt"];
			int num2 = 0;
			bool flag = MapModel.getInstance().dFbDta.ContainsKey(102);
			if (flag)
			{
				num2 = Mathf.Min(MapModel.getInstance().dFbDta[102].cycleCount, num);
			}
			Variant variant2 = SvrLevelConfig.instacne.get_level_data(109u);
			int num3 = variant2["daily_cnt"];
			int num4 = 0;
			bool flag2 = MapModel.getInstance().dFbDta.ContainsKey(109);
			if (flag2)
			{
				num4 = Mathf.Min(MapModel.getInstance().dFbDta[109].cycleCount, num3);
			}
			base.getTransformByPath("scroll_view_fuben/contain/gold/cue/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num - num2,
				"/",
				num,
				"</color>"
			});
			base.getTransformByPath("multiPlayer/contain/gold/cue/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num3 - num4,
				"/",
				num3,
				"</color>"
			});
			bool flag3 = num - num2 <= 0;
			if (flag3)
			{
				this.enterbtn1.interactable = false;
			}
			else
			{
				this.enterbtn1.interactable = true;
			}
			bool flag4 = num3 - num4 <= 0;
			if (flag4)
			{
				this.enterbtn4.interactable = false;
			}
			else
			{
				this.enterbtn4.interactable = true;
			}
		}

		public void refreshExpTimes()
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(101u);
			int num = variant["daily_cnt"];
			int num2 = 0;
			bool flag = MapModel.getInstance().dFbDta.ContainsKey(101);
			if (flag)
			{
				num2 = Mathf.Min(MapModel.getInstance().dFbDta[101].cycleCount, num);
			}
			Variant variant2 = SvrLevelConfig.instacne.get_level_data(108u);
			int num3 = variant2["daily_cnt"];
			int num4 = 0;
			bool flag2 = MapModel.getInstance().dFbDta.ContainsKey(108);
			if (flag2)
			{
				num4 = Mathf.Min(MapModel.getInstance().dFbDta[108].cycleCount, num3);
			}
			base.getTransformByPath("multiConDiff/exp/choiceDef/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num3 - num4,
				"/",
				num3,
				"</color>"
			});
			base.getTransformByPath("multiPlayer/contain/exp/cue/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num3 - num4,
				"/",
				num3,
				"</color>"
			});
			base.getTransformByPath("scroll_view_fuben/contain/exp/cue/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num - num2,
				"/",
				num,
				"</color>"
			});
			bool flag3 = num - num2 <= 0;
			if (flag3)
			{
				this.enterbtn2.interactable = false;
			}
			else
			{
				this.enterbtn2.interactable = true;
			}
			bool flag4 = num3 - num4 <= 0;
			if (flag4)
			{
				this.enterbtn5.interactable = false;
			}
			else
			{
				this.enterbtn5.interactable = true;
			}
		}

		public void refreshMateTimes()
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(103u);
			int num = variant["daily_cnt"];
			int num2 = 0;
			bool flag = MapModel.getInstance().dFbDta.ContainsKey(103);
			if (flag)
			{
				num2 = Mathf.Min(MapModel.getInstance().dFbDta[103].cycleCount, num);
			}
			Variant variant2 = SvrLevelConfig.instacne.get_level_data(110u);
			int num3 = variant2["daily_cnt"];
			int num4 = 0;
			bool flag2 = MapModel.getInstance().dFbDta.ContainsKey(110);
			if (flag2)
			{
				num4 = Mathf.Min(MapModel.getInstance().dFbDta[110].cycleCount, num3);
			}
			base.getTransformByPath("multiConDiff/material/choiceDef/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num3 - num4,
				"/",
				num3,
				"</color>"
			});
			base.getTransformByPath("multiPlayer/contain/material/cue/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num3 - num4,
				"/",
				num3,
				"</color>"
			});
			base.getTransformByPath("scroll_view_fuben/contain/material/cue/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num - num2,
				"/",
				num,
				"</color>"
			});
			bool flag3 = num - num2 <= 0;
			if (flag3)
			{
				this.enterbtn3.interactable = false;
			}
			else
			{
				this.enterbtn3.interactable = true;
			}
			bool flag4 = num3 - num4 <= 0;
			if (flag4)
			{
				this.enterbtn6.interactable = false;
			}
			else
			{
				this.enterbtn6.interactable = true;
			}
		}

		public void refreshGhostTimes()
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(111u);
			int num = variant["daily_cnt"];
			int num2 = 0;
			bool flag = MapModel.getInstance().dFbDta.ContainsKey(111);
			if (flag)
			{
				num2 = Mathf.Min(MapModel.getInstance().dFbDta[111].cycleCount, num);
			}
			base.getTransformByPath("multiConDiff/ghost/choiceDef/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num - num2,
				"/",
				num,
				"</color>"
			});
			base.getTransformByPath("multiPlayer/contain/ghost/cue/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num - num2,
				"/",
				num,
				"</color>"
			});
			bool flag2 = num - num2 <= 0;
			if (flag2)
			{
				this.enterbtn7.interactable = false;
			}
			else
			{
				this.enterbtn7.interactable = true;
			}
		}

		public void refreshMoney()
		{
			Text component = base.transform.FindChild("jingbi/image/num").GetComponent<Text>();
			component.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().money);
		}

		public void refreshGold()
		{
			Text component = base.transform.FindChild("zuanshi/image/num").GetComponent<Text>();
			component.text = ModelBase<PlayerModel>.getInstance().gold.ToString();
		}

		public void refreshGift()
		{
			Text component = base.transform.FindChild("bangzuan/image/num").GetComponent<Text>();
			component.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
		}

		public void CheckLock()
		{
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.TEAMPART, false);
			if (flag)
			{
				base.getGameObjectByPath("lock").SetActive(false);
			}
			else
			{
				this.OpenFB();
			}
		}

		public void OpenFB()
		{
			base.getGameObjectByPath("lock").SetActive(true);
			base.getGameObjectByPath("scroll_view_fuben/contain/material/lock").SetActive(true);
			BaseButton arg_56_0 = new BaseButton(base.getTransformByPath("lock"), 1, 1);
			Action<GameObject> arg_56_1;
			if ((arg_56_1 = a3_counterpart.<>c.<>9__98_0) == null)
			{
				arg_56_1 = (a3_counterpart.<>c.<>9__98_0 = new Action<GameObject>(a3_counterpart.<>c.<>9.<OpenFB>b__98_0));
			}
			arg_56_0.onClick = arg_56_1;
			BaseButton arg_8D_0 = new BaseButton(base.getTransformByPath("scroll_view_fuben/contain/material/lock"), 1, 1);
			Action<GameObject> arg_8D_1;
			if ((arg_8D_1 = a3_counterpart.<>c.<>9__98_1) == null)
			{
				arg_8D_1 = (a3_counterpart.<>c.<>9__98_1 = new Action<GameObject>(a3_counterpart.<>c.<>9.<OpenFB>b__98_1));
			}
			arg_8D_0.onClick = arg_8D_1;
		}

		public void OpenFSWZ()
		{
			bool flag = !FunctionOpenMgr.instance.Check(FunctionOpenMgr.WIND_THRONE, false);
			if (flag)
			{
				base.getGameObjectByPath("scroll_view_fuben/contain/material/lock").SetActive(true);
				BaseButton arg_5C_0 = new BaseButton(base.getTransformByPath("scroll_view_fuben/contain/material/lock"), 1, 1);
				Action<GameObject> arg_5C_1;
				if ((arg_5C_1 = a3_counterpart.<>c.<>9__99_0) == null)
				{
					arg_5C_1 = (a3_counterpart.<>c.<>9__99_0 = new Action<GameObject>(a3_counterpart.<>c.<>9.<OpenFSWZ>b__99_0));
				}
				arg_5C_0.onClick = arg_5C_1;
			}
			else
			{
				base.getGameObjectByPath("scroll_view_fuben/contain/material/lock").SetActive(false);
			}
		}

		public void OpenGold()
		{
			bool flag = !FunctionOpenMgr.instance.Check(FunctionOpenMgr.GOLD_DUNGEON, false);
			if (flag)
			{
				base.getGameObjectByPath("scroll_view_fuben/contain/gold/lock").SetActive(true);
				BaseButton arg_5C_0 = new BaseButton(base.getTransformByPath("scroll_view_fuben/contain/gold/lock"), 1, 1);
				Action<GameObject> arg_5C_1;
				if ((arg_5C_1 = a3_counterpart.<>c.<>9__100_0) == null)
				{
					arg_5C_1 = (a3_counterpart.<>c.<>9__100_0 = new Action<GameObject>(a3_counterpart.<>c.<>9.<OpenGold>b__100_0));
				}
				arg_5C_0.onClick = arg_5C_1;
			}
			else
			{
				base.getGameObjectByPath("scroll_view_fuben/contain/gold/lock").SetActive(false);
			}
		}

		private void onBtnCreateClick(GameObject go)
		{
			int v = 0;
			switch (this.mode)
			{
			case 1:
				v = 109;
				break;
			case 2:
				v = 108;
				break;
			case 3:
				v = 110;
				break;
			case 4:
				v = 111;
				break;
			}
			bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag)
			{
				BaseProxy<TeamProxy>.getInstance().sendobject_change(v);
			}
			else
			{
				BaseProxy<TeamProxy>.getInstance().SendCreateTeam(v);
			}
		}

		private void onBtnQuitClick(GameObject go)
		{
			for (int i = 0; i < 5; i++)
			{
				this.objects.FindChild(i + "/noEmpty").gameObject.SetActive(false);
				this.objects.FindChild(i + "/empty").gameObject.SetActive(true);
			}
			a3_currentTeamPanel.leave = true;
			bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count == 1;
			if (flag)
			{
				uint teamId = BaseProxy<TeamProxy>.getInstance().MyTeamData.teamId;
				BaseProxy<TeamProxy>.getInstance().SendDissolve(teamId);
			}
			else
			{
				BaseProxy<TeamProxy>.getInstance().SendLeaveTeam(ModelBase<PlayerModel>.getInstance().cid);
			}
			BaseProxy<TeamProxy>.getInstance().SendGetMapTeam(this.begin_index, this.end_index);
		}

		private void listOnline()
		{
			Transform transform = base.transform.FindChild("yaoqing/itemReserFriend");
			List<itemFriendData> recommendDataList = BaseProxy<FriendProxy>.getInstance().RecommendDataList;
			RectTransform component = base.getTransformByPath("yaoqing/main/scroll/containts").GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component.sizeDelta.x, (float)recommendDataList.Count * 60f);
			foreach (itemFriendData current in recommendDataList)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
				Transform transform2 = gameObject.transform;
				gameObject.SetActive(true);
				transform2.SetParent(base.getTransformByPath("yaoqing/main/scroll/containts"));
				this.txtName = transform2.transform.FindChild("texts/txtNickName").GetComponent<Text>();
				this.txtLvl = transform2.transform.FindChild("texts/txtLevel").GetComponent<Text>();
				this.txtPro = transform2.transform.FindChild("texts/txtProfessional").GetComponent<Text>();
				this.set(current);
				gameObject.transform.localScale = Vector3.one;
				this.toggle = transform2.transform.FindChild("Toggle").GetComponent<Toggle>();
				bool flag = this.ltemObjList.ContainsKey(current.cid);
				if (!flag)
				{
					this.ltemObjList.Add(current.cid, current.name);
				}
			}
		}

		private void set(itemFriendData item)
		{
			this.txtName.text = item.name;
			this.txtLvl.text = string.Concat(new object[]
			{
				item.zhuan,
				"转",
				item.lvl,
				"级"
			});
			switch (item.carr)
			{
			case 2u:
				this.txtPro.text = "战士";
				break;
			case 3u:
				this.txtPro.text = "法师";
				break;
			case 5u:
				this.txtPro.text = "暗影";
				break;
			}
		}

		private int SortItemFriendData(itemFriendData a1, itemFriendData a2)
		{
			bool flag = a1.online.CompareTo(a2.online) != 0;
			int result;
			if (flag)
			{
				result = -a1.online.CompareTo(a2.online);
			}
			else
			{
				bool flag2 = a1.zhuan.CompareTo(a2.zhuan) != 0;
				if (flag2)
				{
					result = -a1.zhuan.CompareTo(a2.zhuan);
				}
				else
				{
					bool flag3 = a1.lvl.CompareTo(a2.lvl) != 0;
					if (flag3)
					{
						result = -a1.lvl.CompareTo(a2.lvl);
					}
					else
					{
						bool flag4 = a1.combpt.CompareTo(a2.combpt) != 0;
						if (flag4)
						{
							result = -a1.combpt.CompareTo(a2.combpt);
						}
						else
						{
							result = 1;
						}
					}
				}
			}
			return result;
		}
	}
}
