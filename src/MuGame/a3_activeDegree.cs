using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_activeDegree : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_activeDegree.<>c <>9 = new a3_activeDegree.<>c();

			public static Action<GameObject> <>9__11_0;

			internal void <init>b__11_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACTIVEDEGREE);
			}
		}

		public static a3_activeDegree instance;

		public Image loadslider;

		public int active_num;

		private Transform icon;

		private Transform iconi;

		private Text coin;

		private Text diamond;

		private Text bangzuan;

		private GameObject pre;

		private Transform contain;

		private Dictionary<int, int> reward_geted = new Dictionary<int, int>();

		public override void init()
		{
			a3_activeDegree.instance = this;
			this.loadslider = base.transform.FindChild("reward/load/load_fill").GetComponent<Image>();
			this.icon = base.transform.FindChild("reward/icon_reward");
			this.pre = base.transform.FindChild("icon_go/scroll_rect/temprefab").gameObject;
			this.contain = base.transform.FindChild("icon_go/scroll_rect/contain");
			this.coin = base.transform.FindChild("coin/image/num").GetComponent<Text>();
			this.diamond = base.transform.FindChild("diamond/image/num").GetComponent<Text>();
			this.bangzuan = base.transform.FindChild("bangzuan/image/num").GetComponent<Text>();
			BaseButton arg_F0_0 = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			Action<GameObject> arg_F0_1;
			if ((arg_F0_1 = a3_activeDegree.<>c.<>9__11_0) == null)
			{
				arg_F0_1 = (a3_activeDegree.<>c.<>9__11_0 = new Action<GameObject>(a3_activeDegree.<>c.<>9.<init>b__11_0));
			}
			arg_F0_0.onClick = arg_F0_1;
			new BaseButton(base.transform.FindChild("coin/add"), 1, 1).onClick = delegate(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EXCHANGE, null, false);
				a3_exchange expr_18 = a3_exchange.Instance;
				if (expr_18 != null)
				{
					expr_18.transform.SetSiblingIndex(base.transform.GetSiblingIndex() + 1);
				}
			};
			new BaseButton(base.transform.FindChild("diamond/add"), 1, 1).onClick = delegate(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				a3_Recharge expr_18 = a3_Recharge.Instance;
				if (expr_18 != null)
				{
					expr_18.transform.SetSiblingIndex(base.transform.GetSiblingIndex() + 1);
				}
			};
			new BaseButton(base.transform.FindChild("bangzuan/add"), 1, 1).onClick = delegate(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				a3_Recharge.Instance.transform.SetSiblingIndex(base.transform.GetSiblingIndex() + 1);
			};
		}

		public override void onShowed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			GRMap.GAME_CAMERA.SetActive(false);
			a3_activeDegreeProxy expr_22 = BaseProxy<a3_activeDegreeProxy>.getInstance();
			if (expr_22 != null)
			{
				expr_22.SendGetPoint(1);
			}
			this.onActive_Load();
			this.refreshMoney();
			this.do_Active();
			this.onLoad_Change();
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
		}

		private void refreshMoney()
		{
			this.coin.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().money);
			this.diamond.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().gold);
			this.bangzuan.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
		}

		public void onActive_Load()
		{
			List<SXML> xmlreward = XMLMgr.instance.GetSXMLList("huoyue.reward", "");
			for (int i = 0; i < xmlreward.Count; i++)
			{
				this.icon.transform.FindChild(i + "/icon_over").gameObject.SetActive(false);
				this.icon.transform.FindChild(i + "/finsh").gameObject.SetActive(false);
				this.iconi = this.icon.transform.FindChild(i + "/icon");
				this.iconi.GetComponent<Image>().sprite = (Resources.Load(ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)xmlreward[i].getInt("item")).file, typeof(Sprite)) as Sprite);
				int index = i;
				bool flag = BaseProxy<a3_activeDegreeProxy>.getInstance().huoyue_point >= xmlreward[index].getInt("ac");
				if (flag)
				{
					bool flag2 = !BaseProxy<a3_activeDegreeProxy>.getInstance().point.Contains(xmlreward[index].getInt("ac"));
					if (flag2)
					{
						this.icon.transform.FindChild(i + "/icon_over").gameObject.SetActive(true);
					}
					else
					{
						this.icon.transform.FindChild(i + "/icon_over").gameObject.SetActive(false);
						this.icon.transform.FindChild(i + "/finsh").gameObject.SetActive(true);
					}
				}
				new BaseButton(base.transform.FindChild("reward/icon_reward/" + i), 1, 1).onClick = delegate(GameObject oo)
				{
					bool flag3 = BaseProxy<a3_activeDegreeProxy>.getInstance().huoyue_point >= xmlreward[index].getInt("ac");
					if (flag3)
					{
						bool flag4 = !BaseProxy<a3_activeDegreeProxy>.getInstance().point.Contains(xmlreward[index].getInt("ac"));
						if (flag4)
						{
							BaseProxy<a3_activeDegreeProxy>.getInstance().SendGetReward(2u, (uint)xmlreward[index].getInt("ac"));
							flytxt.instance.fly("成功领取活跃奖品！", 0, default(Color), null);
							BaseProxy<a3_activeDegreeProxy>.getInstance().SendGetPoint(1);
						}
						else
						{
							flytxt.instance.fly("已经领取过了。明天再来吧！", 0, default(Color), null);
						}
					}
					else
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add((uint)xmlreward[index].getInt("item"));
						arrayList.Add(1);
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
						a3_miniTip expr_13C = a3_miniTip.Instance;
						if (expr_13C != null)
						{
							expr_13C.transform.SetAsLastSibling();
						}
					}
				};
			}
		}

		public void do_Active()
		{
			this.Clear_con();
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("huoyue.active", "");
			for (int i = 0; i < sXMLList.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pre);
				gameObject.transform.SetParent(this.contain);
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				this.Set_Line(gameObject.transform, sXMLList[i]);
			}
		}

		private void Clear_con()
		{
			bool flag = this.contain.childCount == 0;
			if (!flag)
			{
				for (int i = 0; i < this.contain.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.contain.GetChild(i).gameObject);
				}
			}
		}

		private void Set_Line(Transform go, SXML xml)
		{
			new BaseButton(go.transform.FindChild("go_btn/Text"), 1, 1).onClick = delegate(GameObject oo)
			{
				this.btn_go(xml.getInt("id"));
			};
			int @int = xml.getInt("times");
			go.transform.FindChild("active_num").GetComponent<Text>().text = "活跃度：" + xml.getInt("ac_num");
			bool flag = xml.getInt("type") == 1;
			if (flag)
			{
				int lvl = (int)ModelBase<PlayerModel>.getInstance().lvl;
				int up_lvl = (int)ModelBase<PlayerModel>.getInstance().up_lvl;
				string @string = xml.getString("pram");
				string[] array = @string.Split(new char[]
				{
					','
				});
				int num = int.Parse(array[0]);
				int num2 = int.Parse(array[1]);
				bool flag2 = up_lvl * 100 + lvl >= num * 100 + num2;
				if (flag2)
				{
					go.transform.FindChild("go_btn/lock").gameObject.SetActive(false);
				}
				else
				{
					go.transform.FindChild("go_btn/lock").gameObject.SetActive(true);
				}
			}
			else
			{
				bool flag3 = xml.getInt("type") == 2;
				if (flag3)
				{
					int int2 = xml.getInt("pram");
					bool flag4 = int2 < ModelBase<A3_TaskModel>.getInstance().main_task_id;
					if (flag4)
					{
						go.transform.FindChild("go_btn/lock").gameObject.SetActive(false);
					}
					else
					{
						go.transform.FindChild("go_btn/lock").gameObject.SetActive(true);
					}
				}
			}
			bool flag5 = BaseProxy<a3_activeDegreeProxy>.getInstance().itd.ContainsKey((uint)xml.getInt("id"));
			if (flag5)
			{
				go.transform.FindChild("name_num").GetComponent<Text>().text = string.Concat(new object[]
				{
					xml.getString("des"),
					"(",
					BaseProxy<a3_activeDegreeProxy>.getInstance().itd[(uint)xml.getInt("id")].count,
					"/",
					xml.getInt("times"),
					")"
				});
				bool flag6 = (long)@int - (long)((ulong)BaseProxy<a3_activeDegreeProxy>.getInstance().itd[(uint)xml.getInt("id")].count) <= 0L;
				if (flag6)
				{
					go.transform.FindChild("finsh").gameObject.SetActive(true);
					go.transform.FindChild("get_reward").gameObject.SetActive(false);
					go.transform.FindChild("go_btn").gameObject.SetActive(false);
				}
				else
				{
					go.transform.FindChild("finsh").gameObject.SetActive(false);
					go.transform.FindChild("get_reward").gameObject.SetActive(false);
					go.transform.FindChild("go_btn").gameObject.SetActive(true);
				}
			}
			else
			{
				go.transform.FindChild("name_num").GetComponent<Text>().text = string.Concat(new object[]
				{
					xml.getString("des"),
					"(0/",
					xml.getInt("times"),
					")"
				});
				go.transform.FindChild("finsh").gameObject.SetActive(false);
				go.transform.FindChild("get_reward").gameObject.SetActive(false);
				go.transform.FindChild("go_btn").gameObject.SetActive(true);
			}
		}

		public void btn_go(int i)
		{
			switch (i)
			{
			case 1:
			{
				bool flag = GRMap.instance.m_nCurMapID != 10;
				if (flag)
				{
					flytxt.instance.fly("请先回到曙光城堡", 0, default(Color), null);
				}
				else
				{
					SelfRole.fsm.ChangeState(StateIdle.Instance);
					ArrayList arrayList = new ArrayList();
					arrayList.Add(1);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_AUCTION, arrayList, false);
				}
				break;
			}
			case 2:
			{
				ArrayList arrayList2 = new ArrayList();
				arrayList2.Add(0);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, arrayList2, false);
				break;
			}
			case 3:
			{
				ArrayList arrayList3 = new ArrayList();
				arrayList3.Add("mlzd");
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList3, false);
				break;
			}
			case 4:
			{
				ArrayList arrayList4 = new ArrayList();
				arrayList4.Add(0);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, arrayList4, false);
				break;
			}
			case 5:
			{
				ArrayList arrayList5 = new ArrayList();
				arrayList5.Add(0);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, arrayList5, false);
				break;
			}
			case 6:
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ELITEMON, null, false);
				break;
			case 7:
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIP, null, false);
				break;
			case 8:
			{
				ArrayList arrayList6 = new ArrayList();
				arrayList6.Add(1);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, arrayList6, false);
				break;
			}
			case 9:
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_LOTTERY, null, false);
				break;
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACTIVEDEGREE);
		}

		public void onLoad_Change()
		{
			base.transform.FindChild("reward/current/Text").GetComponent<Text>().text = BaseProxy<a3_activeDegreeProxy>.getInstance().huoyue_point.ToString();
			this.loadslider.fillAmount = (float)BaseProxy<a3_activeDegreeProxy>.getInstance().huoyue_point / 100f;
		}
	}
}
