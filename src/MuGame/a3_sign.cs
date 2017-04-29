using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_sign : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_sign.<>c <>9 = new a3_sign.<>c();

			public static Action<GameObject> <>9__28_1;

			public static Action<GameObject> <>9__28_3;

			public static Action<GameObject> <>9__28_4;

			public static Action<GameObject> <>9__28_5;

			internal void <init>b__28_1(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SIGN);
			}

			internal void <init>b__28_3(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EXCHANGE, null, false);
				bool flag = a3_exchange.Instance != null;
				if (flag)
				{
					a3_exchange.Instance.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__28_4(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				bool flag = a3_Recharge.Instance != null;
				if (flag)
				{
					a3_Recharge.Instance.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__28_5(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACHIEVEMENT, null, false);
				bool flag = a3_achievement.instance != null;
				if (flag)
				{
					a3_achievement.instance.transform.SetAsLastSibling();
				}
			}
		}

		private int thisday = DateTime.Now.Day;

		private int year = DateTime.Now.Year;

		private int month = DateTime.Now.Month;

		private int weekinone;

		private int thismonthcout;

		private int repairsigncount = 0;

		private Text timeover;

		private List<int> list = new List<int>();

		private List<int> lst;

		private GameObject[] risen_rewards;

		private GameObject risen_reward;

		private GameObject thisday_panel;

		private Text repairsignglod;

		private Text yesallglod;

		private GameObject signbtn_panel;

		private Button allrepairsignbtn;

		private Button signbtn;

		private GameObject buy_cardbtn;

		private GameObject busign_bg;

		private Dictionary<int, GameObject> dic_obj;

		private Dictionary<int, GameObject> robj;

		private GameObject image_day;

		private GameObject contain;

		private Text thismonth;

		private Text addsigns;

		private int addsigns_num;

		private bool thisdayissign = false;

		private Transform libg;

		private int years;

		private int months;

		private int days;

		private string timeover_str = "";

		private List<int> last_day = new List<int>();

		private int day = -1;

		private int itemid;

		public override void init()
		{
			this.libg = base.transform.FindChild("panel_top/libg");
			this.dic_obj = new Dictionary<int, GameObject>();
			this.robj = new Dictionary<int, GameObject>();
			this.image_day = base.transform.FindChild("panel_centre/image").gameObject;
			this.contain = base.transform.FindChild("panel_centre/contain").gameObject;
			this.signbtn_panel = base.transform.FindChild("panel_down/signbtn").gameObject;
			this.buy_cardbtn = base.transform.FindChild("panel_down/buy_cardbtn").gameObject;
			this.thisday_panel = base.transform.FindChild("panel_down/thisday_reward/group").gameObject;
			this.timeover = base.transform.FindChild("panel_down/signbtn/time").GetComponent<Text>();
			this.allrepairsignbtn = base.transform.FindChild("panel_down/signbtn/allrepairsign").GetComponent<Button>();
			this.repairsignglod = base.transform.FindChild("panel_down/signbtn/allrepairsign/gold").GetComponent<Text>();
			this.yesallglod = base.transform.FindChild("busign_bg/Image/yes/gold").GetComponent<Text>();
			this.signbtn = base.transform.FindChild("panel_down/signbtn/sign").GetComponent<Button>();
			this.busign_bg = base.transform.FindChild("busign_bg").gameObject;
			this.risen_reward = base.transform.FindChild("panel_top/contain").gameObject;
			this.risen_rewards = new GameObject[5];
			for (int i = 0; i < this.risen_reward.transform.childCount; i++)
			{
				this.risen_rewards[i] = this.risen_reward.transform.GetChild(i).gameObject;
				this.risen_rewards[i].name = (i + 1).ToString();
				EventTriggerListener.Get(this.risen_rewards[i]).onClick = delegate(GameObject go)
				{
					this.onenter(go, go.GetComponent<RectTransform>().position);
				};
			}
			new BaseButton(base.transform.FindChild("cumulative_reward/touch"), 1, 1).onClick = new Action<GameObject>(this.onexit);
			this.lst = new List<int>();
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("signup_a3.total", "");
			foreach (SXML current in sXMLList)
			{
				this.lst.Add(current.getInt("total"));
			}
			this.thismonth = base.transform.FindChild("bg/month").GetComponent<Text>();
			this.addsigns = base.transform.FindChild("bg/Text").GetComponent<Text>();
			this.addsigns.text = string.Format("<color=#B3EE3A>{0}</color> 天", 0);
			BaseButton baseButton = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onClose);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("panel_down/signbtn/allrepairsign"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onAllrepairsign);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("panel_down/signbtn/sign"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onsign);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("busign_bg/Image/yes"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onbusign);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("busign_bg/Image/no"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onbusigns);
			BaseButton arg_3EE_0 = new BaseButton(base.getTransformByPath("panel_down/buy_cardbtn"), 1, 1);
			Action<GameObject> arg_3EE_1;
			if ((arg_3EE_1 = a3_sign.<>c.<>9__28_1) == null)
			{
				arg_3EE_1 = (a3_sign.<>c.<>9__28_1 = new Action<GameObject>(a3_sign.<>c.<>9.<init>b__28_1));
			}
			arg_3EE_0.onClick = arg_3EE_1;
			new BaseButton(base.getTransformByPath("desc/touch"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("desc").SetActive(false);
			};
			BaseButton arg_449_0 = new BaseButton(base.getTransformByPath("top/jingbi/Image"), 1, 1);
			Action<GameObject> arg_449_1;
			if ((arg_449_1 = a3_sign.<>c.<>9__28_3) == null)
			{
				arg_449_1 = (a3_sign.<>c.<>9__28_3 = new Action<GameObject>(a3_sign.<>c.<>9.<init>b__28_3));
			}
			arg_449_0.onClick = arg_449_1;
			BaseButton arg_480_0 = new BaseButton(base.getTransformByPath("top/zuanshi/Image"), 1, 1);
			Action<GameObject> arg_480_1;
			if ((arg_480_1 = a3_sign.<>c.<>9__28_4) == null)
			{
				arg_480_1 = (a3_sign.<>c.<>9__28_4 = new Action<GameObject>(a3_sign.<>c.<>9.<init>b__28_4));
			}
			arg_480_0.onClick = arg_480_1;
			BaseButton arg_4B7_0 = new BaseButton(base.getTransformByPath("top/bangzuan/Image"), 1, 1);
			Action<GameObject> arg_4B7_1;
			if ((arg_4B7_1 = a3_sign.<>c.<>9__28_5) == null)
			{
				arg_4B7_1 = (a3_sign.<>c.<>9__28_5 = new Action<GameObject>(a3_sign.<>c.<>9.<init>b__28_5));
			}
			arg_4B7_0.onClick = arg_4B7_1;
		}

		public override void onShowed()
		{
			this.thismonth.text = this.month + "月";
			BaseProxy<A3_signProxy>.getInstance().sendproxy(1, 0);
			BaseProxy<A3_signProxy>.getInstance().addEventListener(A3_signProxy.SIGNINFO, new Action<GameEvent>(this.refreshSign));
			BaseProxy<A3_signProxy>.getInstance().addEventListener(A3_signProxy.AllREPARISIGN, new Action<GameEvent>(this.allrepairsign));
			BaseProxy<A3_signProxy>.getInstance().addEventListener(A3_signProxy.SIGNorREPAIR, new Action<GameEvent>(this.singorrepair));
			BaseProxy<A3_signProxy>.getInstance().addEventListener(A3_signProxy.ACCUMULATE, new Action<GameEvent>(this.accumulate));
			BaseProxy<A3_signProxy>.getInstance().addEventListener(A3_signProxy.SIGNINFO_YUEKA, new Action<GameEvent>(this.timeovers));
			GRMap.GAME_CAMERA.SetActive(false);
			UIClient.instance.addEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			this.refreshMoney();
			this.refreshGold();
			this.refreshGift();
			this.createSign();
		}

		public override void onClosed()
		{
			this.addsigns_num = 0;
			this.thisdayissign = false;
			UIClient.instance.removeEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			BaseProxy<SignProxy>.getInstance().removeEventListener(A3_signProxy.SIGNINFO, new Action<GameEvent>(this.refreshSign));
			BaseProxy<A3_signProxy>.getInstance().removeEventListener(A3_signProxy.AllREPARISIGN, new Action<GameEvent>(this.allrepairsign));
			BaseProxy<A3_signProxy>.getInstance().removeEventListener(A3_signProxy.SIGNorREPAIR, new Action<GameEvent>(this.singorrepair));
			BaseProxy<A3_signProxy>.getInstance().removeEventListener(A3_signProxy.ACCUMULATE, new Action<GameEvent>(this.accumulate));
			BaseProxy<A3_signProxy>.getInstance().removeEventListener(A3_signProxy.SIGNINFO_YUEKA, new Action<GameEvent>(this.timeovers));
			GRMap.GAME_CAMERA.SetActive(true);
		}

		public void refreshMoney()
		{
			Text component = base.transform.FindChild("top/jingbi/image/num").GetComponent<Text>();
			component.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().money);
		}

		public void refreshGold()
		{
			Text component = base.transform.FindChild("top/zuanshi/image/num").GetComponent<Text>();
			component.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().gold);
		}

		public void refreshGift()
		{
			Text component = base.transform.FindChild("top/bangzuan/image/num").GetComponent<Text>();
			component.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
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

		public string ConvertStringToDateTime(long timeStamp)
		{
			return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds((double)timeStamp).ToString("yyyy-MM-dd");
		}

		private void timeovers(GameEvent e)
		{
			this.timeoverss(e.data["yueka"]);
		}

		private void timeoverss(int num)
		{
			switch (num)
			{
			case 0:
				this.timeover.text = "请先购买月卡";
				break;
			case 1:
				this.timeover.text = this.timeover_str;
				break;
			case 2:
				this.timeover.text = "永久有效";
				break;
			}
		}

		private void refreshSign(GameEvent e)
		{
			int length = e.data["qd_days"].Length;
			for (int i = 0; i < length; i++)
			{
				this.last_day.Add(e.data["qd_days"]._arr[i]);
			}
			this.buy_cardbtn.SetActive(false);
			this.signbtn_panel.SetActive(true);
			this.repairsigncount = this.thisday - length - 1;
			this.timeover.text = this.ConvertStringToDateTime(e.data["yueka_tm"]);
			this.timeover_str = this.timeover.text;
			this.timeoverss(e.data["yueka"]._int);
			this.addsigns.text = string.Format("<color=#B3EE3A>{0}</color> 天", e.data["qd_days"].Length);
			this.addsigns_num = e.data["qd_days"].Length;
			this.dic_obj[this.thisday + this.weekinone - 1].transform.FindChild("button").gameObject.SetActive(true);
			BaseButton baseButton = new BaseButton(this.dic_obj[this.thisday + this.weekinone - 1].transform.FindChild("button"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onsign);
			bool flag = e.data["qd_days"].Length > 0;
			if (flag)
			{
				this.dic_obj[this.thisday + this.weekinone - 1].transform.FindChild("thisday").gameObject.SetActive(true);
				this.signbtn.interactable = true;
				using (List<Variant>.Enumerator enumerator = e.data["qd_days"]._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						this.dic_obj[num + this.weekinone - 1].transform.FindChild("over").gameObject.SetActive(true);
						this.dic_obj[num + this.weekinone - 1].transform.FindChild(string.Concat(num + this.weekinone - 1)).gameObject.SetActive(false);
						bool flag2 = num == this.thisday;
						if (flag2)
						{
							this.thisdayissign = true;
							this.signbtn.interactable = false;
							this.repairsigncount = this.thisday - length;
							this.dic_obj[this.thisday + this.weekinone - 1].transform.FindChild("button").gameObject.SetActive(false);
						}
						bool flag3 = this.list.Contains(num + this.weekinone - 1);
						if (flag3)
						{
							this.list.Remove(num + this.weekinone - 1);
						}
					}
				}
				for (int j = 0; j < this.list.Count; j++)
				{
					bool flag4 = this.list[j] == this.thisday + this.weekinone - 1;
					if (!flag4)
					{
						this.dic_obj[this.list[j]].transform.FindChild("over").gameObject.SetActive(false);
						this.dic_obj[this.list[j]].transform.FindChild(string.Concat(this.list[j])).gameObject.SetActive(true);
					}
				}
				bool flag5 = this.repairsigncount == 0;
				if (flag5)
				{
					this.allrepairsignbtn.interactable = false;
					this.repairsignglod.text = string.Concat(0);
					this.yesallglod.text = string.Concat(0);
				}
				else
				{
					this.allrepairsignbtn.interactable = true;
					this.repairsignglod.text = (this.repairsigncount * 5).ToString();
					this.yesallglod.text = (this.repairsigncount * 5).ToString();
				}
			}
			else
			{
				this.signbtn.gameObject.SetActive(true);
				bool flag6 = e.data.ContainsKey("yueka_tm");
				if (flag6)
				{
					bool flag7 = this.days == this.thisday;
					if (flag7)
					{
						this.signbtn.interactable = true;
					}
				}
				foreach (int current in this.dic_obj.Keys)
				{
					this.dic_obj[current].transform.FindChild("over").gameObject.SetActive(false);
				}
				this.allrepairsignbtn.interactable = true;
				this.repairsignglod.text = (this.repairsigncount * 5).ToString();
				this.yesallglod.text = (this.repairsigncount * 5).ToString();
				foreach (int current2 in this.dic_obj.Keys)
				{
					bool flag8 = current2 < this.thisday + this.weekinone - 1;
					if (flag8)
					{
						this.dic_obj[current2].transform.FindChild(string.Concat(current2)).gameObject.SetActive(true);
					}
					else
					{
						bool flag9 = current2 == this.thisday + this.weekinone - 1;
						if (flag9)
						{
							this.dic_obj[current2].transform.FindChild("thisday").gameObject.SetActive(true);
						}
					}
				}
			}
			for (int k = 0; k < this.risen_rewards.Length; k++)
			{
				this.risen_rewards[k].transform.FindChild("over").gameObject.SetActive(false);
				this.risen_rewards[k].transform.FindChild("can").gameObject.SetActive(false);
				this.risen_rewards[k].transform.FindChild("image").gameObject.SetActive(true);
				bool flag10 = k < 4;
				if (flag10)
				{
					base.transform.FindChild(string.Concat(new object[]
					{
						"bg/line/",
						k,
						"/",
						k
					})).gameObject.SetActive(false);
				}
			}
			bool flag11 = e.data["count_type"].Length > 0;
			if (flag11)
			{
				using (List<Variant>.Enumerator enumerator4 = e.data["count_type"]._arr.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						int num2 = enumerator4.Current;
						this.risen_rewards[num2 / 5 - 1].transform.FindChild("over").gameObject.SetActive(true);
						this.risen_rewards[num2 / 5 - 1].transform.FindChild("image").gameObject.SetActive(false);
						this.risen_rewards[num2 / 5 - 1].transform.FindChild("can").gameObject.SetActive(false);
						bool flag12 = num2 / 5 - 1 < 4;
						if (flag12)
						{
							base.transform.FindChild(string.Concat(new object[]
							{
								"bg/line/",
								num2 / 5 - 1,
								"/",
								num2 / 5 - 1
							})).gameObject.SetActive(true);
						}
					}
				}
			}
			bool flag13 = !this.thisdayissign;
			if (flag13)
			{
				this.leijicanImahe(this.addsigns_num);
			}
		}

		private void allrepairsign(GameEvent e)
		{
			bool flag = e.data["fillsign_all"].Length > 0;
			if (flag)
			{
				foreach (Variant current in e.data["fillsign_all"]._arr)
				{
					this.dic_obj[current + this.weekinone - 1].transform.FindChild("over").gameObject.SetActive(true);
					this.dic_obj[current + this.weekinone - 1].transform.FindChild(string.Concat(current + this.weekinone - 1)).gameObject.SetActive(false);
					List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("signup_a3.signup", "signup_times==" + current._int);
					bool flag2 = sXMLList.Count > 0;
					if (flag2)
					{
						int @int = sXMLList[0].GetNodeList("item", "")[0].getInt("item_id");
						int int2 = sXMLList[0].GetNodeList("item", "")[0].getInt("num");
						List<SXML> sXMLList2 = XMLMgr.instance.GetSXMLList("item.item", "id==" + @int);
						string @string = sXMLList2[0].getString("item_name");
						flytxt.instance.fly(string.Concat(new object[]
						{
							"获得了",
							@string,
							"*",
							int2
						}), 0, default(Color), null);
					}
				}
			}
			this.addsigns_num += e.data["fillsign_all"].Length;
			this.addsigns.text = string.Format("<color=#B3EE3A>{0}</color> 天", this.addsigns_num);
			bool activeSelf = this.busign_bg.activeSelf;
			if (activeSelf)
			{
				this.busign_bg.SetActive(false);
			}
			this.allrepairsignbtn.interactable = false;
			this.repairsignglod.text = string.Concat(0);
			this.yesallglod.text = string.Concat(0);
			bool flag3 = !this.thisdayissign;
			if (flag3)
			{
				this.leijicanImahe(this.addsigns_num);
			}
		}

		private void singorrepair(GameEvent e)
		{
			this.dic_obj[e.data["daysign"] + this.weekinone - 1].transform.FindChild(string.Concat(e.data["daysign"] + this.weekinone - 1)).gameObject.SetActive(false);
			this.dic_obj[e.data["daysign"] + this.weekinone - 1].transform.FindChild("over").gameObject.SetActive(true);
			bool flag = e.data["daysign"] == this.thisday;
			if (flag)
			{
				this.signbtn.interactable = false;
				this.thisdayissign = true;
			}
			else
			{
				bool flag2 = this.list.Contains(e.data["daysign"] + this.weekinone - 1);
				if (flag2)
				{
					this.list.Remove(e.data["daysign"] + this.weekinone - 1);
				}
				this.repairsigncount--;
				this.repairsignglod.text = (this.repairsigncount * 5).ToString();
				this.yesallglod.text = (this.repairsigncount * 5).ToString();
			}
			bool flag3 = this.repairsigncount == 0;
			if (flag3)
			{
				this.allrepairsignbtn.interactable = false;
				this.repairsignglod.text = string.Concat(0);
				this.yesallglod.text = string.Concat(0);
			}
			bool activeSelf = this.busign_bg.activeSelf;
			if (activeSelf)
			{
				this.busign_bg.SetActive(false);
			}
			this.day = -1;
			this.addsigns_num++;
			this.addsigns.text = string.Format("<color=#B3EE3A>{0}</color> 天", this.addsigns_num);
			bool flag4 = !this.thisdayissign;
			if (flag4)
			{
				this.leijicanImahe(this.addsigns_num);
			}
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("signup_a3.signup", "signup_times==" + e.data["daysign"]);
			bool flag5 = sXMLList.Count > 0;
			if (flag5)
			{
				int @int = sXMLList[0].GetNodeList("item", "")[0].getInt("item_id");
				int int2 = sXMLList[0].GetNodeList("item", "")[0].getInt("num");
				List<SXML> sXMLList2 = XMLMgr.instance.GetSXMLList("item.item", "id==" + @int);
				string @string = sXMLList2[0].getString("item_name");
				flytxt.instance.fly(string.Concat(new object[]
				{
					"获得了",
					@string,
					"*",
					int2
				}), 0, default(Color), null);
			}
		}

		private void accumulate(GameEvent e)
		{
			bool flag = e.data["total_signup"].Length > 0;
			if (flag)
			{
				using (List<Variant>.Enumerator enumerator = e.data["total_signup"]._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						this.risen_rewards[num / 5 - 1].transform.FindChild("over").gameObject.SetActive(true);
						this.risen_rewards[num / 5 - 1].transform.FindChild("image").gameObject.SetActive(false);
						this.risen_rewards[num / 5 - 1].transform.FindChild("can").gameObject.SetActive(false);
						bool flag2 = num / 5 - 1 < 4;
						if (flag2)
						{
							base.transform.FindChild(string.Concat(new object[]
							{
								"bg/line/",
								num / 5 - 1,
								"/",
								num / 5 - 1
							})).gameObject.SetActive(true);
						}
						List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("signup_a3.total", "total==" + num);
						for (int i = 0; i < sXMLList[0].GetNodeList("item", "").Count; i++)
						{
							int @int = sXMLList[0].GetNodeList("item", "")[i].getInt("item_id");
							int int2 = sXMLList[0].GetNodeList("item", "")[i].getInt("num");
							List<SXML> sXMLList2 = XMLMgr.instance.GetSXMLList("item.item", "id==" + @int);
							string @string = sXMLList2[0].getString("item_name");
							flytxt.instance.fly(string.Concat(new object[]
							{
								"获得了累积签到奖励",
								@string,
								"*",
								int2
							}), 0, default(Color), null);
						}
					}
				}
			}
		}

		private void leijicanImahe(int nums)
		{
			for (int i = 0; i < this.lst.Count; i++)
			{
				bool flag = this.lst[i] == nums + 1;
				if (flag)
				{
					this.risen_rewards[i].transform.FindChild("can").gameObject.SetActive(true);
					this.risen_rewards[i].transform.FindChild("over").gameObject.SetActive(false);
					this.risen_rewards[i].transform.FindChild("image").gameObject.SetActive(false);
					BaseButton baseButton = new BaseButton(this.risen_rewards[i].transform.FindChild("can").transform, 1, 1);
					baseButton.onClick = new Action<GameObject>(this.onsign);
					bool flag2 = i < 4;
					if (flag2)
					{
						base.transform.FindChild(string.Concat(new object[]
						{
							"bg/line/",
							i,
							"/",
							i
						})).gameObject.SetActive(false);
					}
					break;
				}
			}
		}

		private void createSign()
		{
			this.onexit(null);
			this.buy_cardbtn.SetActive(true);
			this.signbtn_panel.SetActive(false);
			this.thismonthcout = DateTime.DaysInMonth(this.year, this.month);
			int num4 = (this.thisday + 2 * this.month + 3 * (this.month + 1) / 5 + this.year + this.year / 4 - this.year / 100 + this.year / 400) % 7 + 1;
			bool flag = base.transform.FindChild("panel_down/thisday_reward/group").transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < this.thisday_panel.transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.thisday_panel.transform.GetChild(i).gameObject);
				}
			}
			this.showreward(this.thisday, this.thisday_panel);
			for (int j = 0; j < this.risen_rewards.Length; j++)
			{
				this.risen_rewards[j].transform.FindChild("Text").GetComponent<Text>().text = "累计<color=#f5ff55>" + this.lst[j] + "</color>天";
			}
			int num2 = 2;
			DateTime dateTime = default(DateTime);
			switch (DateTime.Parse(string.Concat(new object[]
			{
				this.year,
				"/",
				this.month,
				"/",
				1
			})).DayOfWeek)
			{
			case DayOfWeek.Sunday:
				num2 = 7;
				break;
			case DayOfWeek.Monday:
				num2 = 1;
				break;
			case DayOfWeek.Tuesday:
				num2 = 2;
				break;
			case DayOfWeek.Wednesday:
				num2 = 3;
				break;
			case DayOfWeek.Thursday:
				num2 = 4;
				break;
			case DayOfWeek.Friday:
				num2 = 5;
				break;
			case DayOfWeek.Saturday:
				num2 = 6;
				break;
			}
			this.weekinone = num2;
			bool flag2 = this.month - 1 == 0;
			int num3;
			if (flag2)
			{
				num3 = DateTime.DaysInMonth(this.year - 1, 12);
			}
			else
			{
				num3 = DateTime.DaysInMonth(this.year, this.month - 1);
			}
			for (int k = 1; k <= 42; k++)
			{
				int num = k;
				bool flag3 = this.robj.ContainsKey(k);
				GameObject gameObject;
				if (flag3)
				{
					gameObject = this.robj[k];
				}
				else
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image_day);
				}
				gameObject.SetActive(true);
				gameObject.transform.SetParent(this.contain.transform, false);
				gameObject.name = string.Concat(k);
				Transform transform = gameObject.transform.FindChild("bu");
				bool flag4 = transform != null;
				if (flag4)
				{
					transform.name = k.ToString();
				}
				bool flag5 = k < num2;
				if (flag5)
				{
					gameObject.transform.FindChild("nub").gameObject.SetActive(false);
					gameObject.transform.FindChild("old_nub").gameObject.SetActive(true);
					gameObject.transform.FindChild("old_nub").GetComponent<Text>().text = (num3 - num2 + k + 1).ToString();
				}
				bool flag6 = k >= num2 && k <= this.thismonthcout + num2 - 1;
				if (flag6)
				{
					gameObject.transform.FindChild("nub").gameObject.SetActive(true);
					gameObject.transform.FindChild("old_nub").gameObject.SetActive(false);
					gameObject.transform.FindChild("nub").GetComponent<Text>().text = (k - num2 + 1).ToString();
				}
				bool flag7 = k > this.thismonthcout + num2 - 1;
				if (flag7)
				{
					gameObject.transform.FindChild("nub").gameObject.SetActive(false);
					gameObject.transform.FindChild("old_nub").gameObject.SetActive(true);
					gameObject.transform.FindChild("old_nub").GetComponent<Text>().text = (k - (this.thismonthcout + num2 - 1)).ToString();
				}
				bool flag8 = k < this.thisday + this.weekinone - 1;
				if (flag8)
				{
					gameObject.transform.FindChild("old").gameObject.SetActive(true);
				}
				bool flag9 = k == this.thisday + this.weekinone - 1;
				if (flag9)
				{
					gameObject.transform.FindChild("thisday").gameObject.SetActive(true);
				}
				bool flag10 = k > this.thisday + this.weekinone - 1;
				if (flag10)
				{
					gameObject.transform.FindChild("new").gameObject.SetActive(true);
				}
				BaseButton baseButton = new BaseButton(gameObject.transform.FindChild(string.Concat(k)), 1, 1);
				baseButton.onClick = delegate(GameObject go)
				{
					this.day = num - this.weekinone + 1;
					this.deldereward();
					this.showreward(this.day, this.busign_bg.transform.FindChild("Image/hscr/scroll/content").gameObject);
					this.yesallglod.text = string.Concat(5);
					this.busign_bg.transform.FindChild("left").gameObject.SetActive(false);
					this.busign_bg.transform.FindChild("right").gameObject.SetActive(false);
					this.busign_bg.SetActive(true);
				};
				this.robj[k] = gameObject;
				bool flag11 = k >= num2 && k <= this.thismonthcout + num2 - 1;
				if (flag11)
				{
					this.dic_obj[k] = gameObject;
				}
				bool flag12 = k >= num2 && k < this.thisday + num2 - 1;
				if (flag12)
				{
					this.list.Add(k);
				}
			}
		}

		private void showreward(int days, GameObject obj)
		{
			Transform transform = base.transform.FindChild("cumulative_reward/iconbg");
			SXML sXML = XMLMgr.instance.GetSXML("signup_a3.signup", "signup_times==" + days);
			int @int = sXML.getInt("gemnum");
			List<SXML> nodeList = sXML.GetNodeList("item", "");
			SXML xmlitem;
			string limit;
			Action<GameObject> <>9__0;
			foreach (SXML current in nodeList)
			{
				GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon((uint)current.getInt("item_id"), false, current.getInt("num"), 1f, true, -1, 0, false, false, false, false);
				bool flag = current.getInt("num") <= 1;
				if (flag)
				{
					gameObject.transform.FindChild("num").gameObject.SetActive(false);
				}
				this.itemid = current.getInt("item_id");
				gameObject.transform.FindChild("iconborder").localScale = new Vector3(1f, 1f, 0f);
				Image component = gameObject.GetComponent<Image>();
				bool flag2 = component != null;
				if (flag2)
				{
					component.enabled = false;
				}
				gameObject.transform.SetParent(obj.transform, false);
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
				gameObject2.transform.SetParent(gameObject.transform);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.SetActive(true);
				gameObject2.transform.SetAsFirstSibling();
				BaseButton arg_1D9_0 = new BaseButton(gameObject.transform, 1, 1);
				Action<GameObject> arg_1D9_1;
				if ((arg_1D9_1 = <>9__0) == null)
				{
					arg_1D9_1 = (<>9__0 = delegate(GameObject go)
					{
						xmlitem = XMLMgr.instance.GetSXML("item.item", "id==" + this.itemid);
						this.getTransformByPath("desc/bg/itemdesc").GetComponent<Text>().text = StringUtils.formatText(xmlitem.getString("desc"));
						this.getTransformByPath("desc/bg/name/name").GetComponent<Text>().text = xmlitem.getString("item_name");
						this.getTransformByPath("desc/bg/iconbg/icon").GetComponent<Image>().sprite = (Resources.Load("icon/item/" + this.itemid, typeof(Sprite)) as Sprite);
						bool flag4 = xmlitem.getInt("use_limit") > 0;
						if (flag4)
						{
							limit = xmlitem.getInt("use_limit") + "转";
						}
						else
						{
							limit = "无限制";
						}
						this.getTransformByPath("desc/bg/name/dengji").GetComponent<Text>().text = limit;
						this.getGameObjectByPath("desc").SetActive(true);
					});
				}
				arg_1D9_0.onClick = arg_1D9_1;
			}
			GridLayoutGroup component2 = obj.GetComponent<GridLayoutGroup>();
			obj.GetComponent<RectTransform>().sizeDelta = new Vector2((component2.cellSize.x + 0.1f) * (float)Mathf.Max(nodeList.Count, 6), 102f);
			bool flag3 = nodeList.Count > 6;
			if (flag3)
			{
				component2.childAlignment = TextAnchor.MiddleLeft;
			}
			else
			{
				component2.childAlignment = TextAnchor.MiddleCenter;
			}
		}

		private void deldereward()
		{
			bool flag = this.busign_bg.transform.FindChild("Image/hscr/scroll/content").transform.childCount > 0;
			if (flag)
			{
				int childCount = this.busign_bg.transform.FindChild("Image/hscr/scroll/content").transform.childCount;
				for (int i = childCount; i > 0; i--)
				{
					UnityEngine.Object.DestroyImmediate(this.busign_bg.transform.FindChild("Image/hscr/scroll/content").transform.GetChild(i - 1).gameObject);
				}
			}
		}

		private void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SIGN);
		}

		private void onAllrepairsign(GameObject go)
		{
			this.yesallglod.text = (this.repairsigncount * 5).ToString();
			this.deldereward();
			int num = 0;
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int i = 0; i < this.list.Count; i++)
			{
				SXML sXML = XMLMgr.instance.GetSXML("signup_a3.signup", "signup_times==" + (this.list[i] - 2));
				num += sXML.getInt("gemnum");
				List<SXML> nodeList = sXML.GetNodeList("item", "");
				foreach (SXML current in nodeList)
				{
					bool flag = dictionary.ContainsKey(current.getInt("item_id"));
					if (flag)
					{
						Dictionary<int, int> dictionary2 = dictionary;
						int @int = current.getInt("item_id");
						dictionary2[@int] += current.getInt("num");
					}
					else
					{
						dictionary[current.getInt("item_id")] = current.getInt("num");
					}
				}
			}
			this.busign_bg.transform.FindChild("Image/panel/Image/num").GetComponent<Text>().text = num.ToString();
			Transform transform = base.transform.FindChild("cumulative_reward/iconbg");
			foreach (int current2 in dictionary.Keys)
			{
				GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon((uint)current2, false, dictionary[current2], 1f, true, -1, 0, false, false, false, false);
				bool flag2 = dictionary[current2] <= 1;
				if (flag2)
				{
					gameObject.transform.FindChild("num").gameObject.SetActive(false);
				}
				gameObject.transform.FindChild("iconborder").localScale = new Vector3(1f, 1f, 0f);
				Image component = gameObject.GetComponent<Image>();
				bool flag3 = component != null;
				if (flag3)
				{
					component.enabled = false;
				}
				gameObject.transform.SetParent(this.busign_bg.transform.FindChild("Image/hscr/scroll/content").transform, false);
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
				gameObject2.transform.SetParent(gameObject.transform);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.SetActive(true);
				gameObject2.transform.SetAsFirstSibling();
			}
			GameObject gameObject3 = this.busign_bg.transform.FindChild("Image/hscr/scroll/content").gameObject;
			bool flag4 = this.list.Count <= 5;
			if (flag4)
			{
				this.busign_bg.transform.FindChild("left").gameObject.SetActive(false);
				this.busign_bg.transform.FindChild("right").gameObject.SetActive(false);
			}
			else
			{
				this.busign_bg.transform.FindChild("left").gameObject.SetActive(true);
				this.busign_bg.transform.FindChild("right").gameObject.SetActive(true);
			}
			GridLayoutGroup component2 = gameObject3.GetComponent<GridLayoutGroup>();
			gameObject3.GetComponent<RectTransform>().sizeDelta = new Vector2((component2.cellSize.x + 0.1f) * (float)Mathf.Max(dictionary.Count, 6), 102f);
			bool flag5 = dictionary.Count > 6;
			if (flag5)
			{
				component2.childAlignment = TextAnchor.MiddleLeft;
			}
			else
			{
				component2.childAlignment = TextAnchor.MiddleCenter;
			}
			this.busign_bg.SetActive(true);
		}

		private void onsign(GameObject go)
		{
			BaseProxy<A3_signProxy>.getInstance().sendproxy(2, 0);
		}

		private void onbusign(GameObject go)
		{
			bool flag = this.day == -1;
			if (flag)
			{
				BaseProxy<A3_signProxy>.getInstance().sendproxy(4, 0);
			}
			else
			{
				BaseProxy<A3_signProxy>.getInstance().sendproxy(3, this.day);
			}
		}

		private void onbusigns(GameObject go)
		{
			this.busign_bg.SetActive(false);
		}

		private void onenter(GameObject go, Vector3 pos)
		{
			this.libg.gameObject.SetActive(true);
			this.libg.SetParent(go.transform);
			this.libg.localPosition = new Vector3(-4.5f, 4f, 0f);
			this.libg.SetAsFirstSibling();
			Transform transform = base.transform.FindChild("cumulative_reward/bg/cumulative_reward");
			Transform transform2 = base.transform.FindChild("cumulative_reward/iconbg");
			GridLayoutGroup component = transform.GetComponent<GridLayoutGroup>();
			RectTransform componentByPath = base.getComponentByPath<RectTransform>("cumulative_reward/bg");
			bool flag = transform.transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < transform.transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(transform.transform.GetChild(i).gameObject);
				}
			}
			SXML sXML = XMLMgr.instance.GetSXML("signup_a3.total", "total==" + int.Parse(go.name) * 5);
			List<SXML> nodeList = sXML.GetNodeList("item", "");
			int itemid2;
			SXML xmlitem;
			string limit;
			Action<GameObject> <>9__0;
			foreach (SXML current in nodeList)
			{
				GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon((uint)current.getInt("item_id"), false, current.getInt("num"), 1f, true, -1, 0, false, false, false, false);
				bool flag2 = current.getInt("num") <= 1;
				if (flag2)
				{
					gameObject.transform.FindChild("num").gameObject.SetActive(false);
				}
				gameObject.transform.FindChild("iconborder").localScale = new Vector3(1f, 1f, 0f);
				Image component2 = gameObject.GetComponent<Image>();
				bool flag3 = component2 != null;
				if (flag3)
				{
					component2.enabled = false;
				}
				gameObject.transform.SetParent(transform.transform, false);
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(transform2.gameObject);
				gameObject2.transform.SetParent(gameObject.transform);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.SetActive(true);
				gameObject2.transform.SetAsFirstSibling();
				BaseButton arg_28E_0 = new BaseButton(gameObject.transform, 1, 1);
				Action<GameObject> arg_28E_1;
				if ((arg_28E_1 = <>9__0) == null)
				{
					arg_28E_1 = (<>9__0 = delegate(GameObject gos)
					{
						itemid2 = int.Parse(gos.transform.FindChild("icon").GetComponent<Image>().sprite.name);
						xmlitem = XMLMgr.instance.GetSXML("item.item", "id==" + itemid2);
						this.getTransformByPath("desc/bg/itemdesc").GetComponent<Text>().text = xmlitem.getString("desc");
						this.getTransformByPath("desc/bg/name/name").GetComponent<Text>().text = xmlitem.getString("item_name");
						this.getTransformByPath("desc/bg/iconbg/icon").GetComponent<Image>().sprite = (Resources.Load("icon/item/" + itemid2, typeof(Sprite)) as Sprite);
						bool flag6 = xmlitem.getInt("use_limit") > 0;
						if (flag6)
						{
							limit = xmlitem.getInt("use_limit") + "转";
						}
						else
						{
							limit = "无限制";
						}
						this.getTransformByPath("desc/bg/name/dengji").GetComponent<Text>().text = limit;
						this.getGameObjectByPath("desc").SetActive(true);
					});
				}
				arg_28E_0.onClick = arg_28E_1;
			}
			base.transform.FindChild("cumulative_reward").gameObject.SetActive(true);
			componentByPath.sizeDelta = new Vector2(componentByPath.sizeDelta.x, (component.cellSize.y + component.spacing.y + 10f) * (float)(Mathf.Max(1, nodeList.Count - 1) / component.constraintCount + 1) + 20f);
			Vector3 vector = pos;
			bool flag4 = vector.y > (float)(Screen.height / 2);
			if (flag4)
			{
				pos.y -= component.cellSize.y * (float)(Mathf.Max(1, nodeList.Count - 1) / component.constraintCount) * Baselayer.uiRatio;
			}
			else
			{
				pos.y += component.cellSize.y * (float)(Mathf.Max(1, nodeList.Count - 1) / component.constraintCount) * Baselayer.uiRatio;
			}
			bool flag5 = vector.x > (float)(Screen.width / 2);
			if (flag5)
			{
				pos.x -= 230f * Baselayer.uiRatio;
			}
			else
			{
				pos.x += 230f * Baselayer.uiRatio;
			}
			base.transform.FindChild("cumulative_reward").transform.position = pos + new Vector3(30f, 0f, 0f);
		}

		private void onexit(GameObject go)
		{
			base.transform.FindChild("cumulative_reward").gameObject.SetActive(false);
			Transform parent = base.transform.FindChild("panel_top");
			this.libg.SetParent(parent);
			this.libg.gameObject.SetActive(false);
		}
	}
}
