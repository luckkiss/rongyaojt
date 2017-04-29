using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_new_pet : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_new_pet.<>c <>9 = new a3_new_pet.<>c();

			public static Action<GameObject> <>9__26_0;

			public static Action<GameObject> <>9__26_3;

			public static Action<GameObject> <>9__26_4;

			public static Action<GameObject> <>9__26_5;

			public static Action<GameObject> <>9__26_12;

			internal void <init>b__26_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_NEW_PET);
			}

			internal void <init>b__26_3(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EXCHANGE, null, false);
				bool flag = a3_exchange.Instance != null;
				if (flag)
				{
					a3_exchange.Instance.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__26_4(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				bool flag = a3_Recharge.Instance != null;
				if (flag)
				{
					a3_Recharge.Instance.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__26_5(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACHIEVEMENT, null, false);
				bool flag = a3_achievement.instance != null;
				if (flag)
				{
					a3_achievement.instance.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__26_12(GameObject go)
			{
				flytxt.instance.fly("这个宠物还没有解锁", 0, default(Color), null);
			}
		}

		public static a3_new_pet instance;

		private int petNum = 0;

		private List<SXML> list;

		private SXML xml;

		private GameObject iconTemp;

		private GameObject goIcon;

		private GameObject sliderGo;

		private Transform cont;

		private float sizeY;

		public int petid = 2;

		private long year = 0L;

		private long month = 0L;

		private long day = 0L;

		private long hour = 0L;

		private long min = 0L;

		private long sen = 0L;

		private long all = 0L;

		public bool first = false;

		public long lastTime = 0L;

		public long addTime = 0L;

		public long last_times = 0L;

		public long feedPetTime;

		private petGO petgo;

		private Text timeGo;

		public bool openEveryLogin = false;

		public GameObject pet_avater;

		public override void init()
		{
			a3_new_pet.instance = this;
			bool flag = ModelBase<PlayerModel>.getInstance().last_time == -1;
			if (flag)
			{
				this.lastTime = -1L;
				base.getGameObjectByPath("timeBuy/buyIt").SetActive(false);
			}
			else
			{
				bool flag2 = ModelBase<PlayerModel>.getInstance().last_time == 0;
				if (flag2)
				{
					this.lastTime = 0L;
				}
				else
				{
					bool flag3 = ModelBase<PlayerModel>.getInstance().last_time > 0;
					if (flag3)
					{
						this.lastTime = (long)(ModelBase<PlayerModel>.getInstance().last_time - muNetCleint.instance.CurServerTimeStamp);
					}
					else
					{
						this.lastTime = 0L;
					}
				}
			}
			this.first = ModelBase<PlayerModel>.getInstance().first;
			debug.Log(string.Concat(new object[]
			{
				"first:",
				this.first.ToString(),
				"  last_time:",
				this.lastTime
			}));
			this.petgo = new petGO();
			this.timeGo = base.transform.FindChild("timeBuy/Text").GetComponent<Text>();
			this.sliderGo = base.getGameObjectByPath("timeBuy/expbar/slider");
			bool flag4 = this.first;
			if (flag4)
			{
				base.getGameObjectByPath("buyshow/scollBuy/content/buy0").SetActive(true);
			}
			else
			{
				base.getGameObjectByPath("buyshow/scollBuy/content/buy0").SetActive(false);
			}
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_GET_PET, new Action<GameEvent>(this.getPet));
			bool flag5 = this.lastTime <= -1L;
			if (flag5)
			{
				this.lastTime = -1L;
				base.getGameObjectByPath("timeBuy/buyIt").SetActive(false);
			}
			this.timeSet(this.lastTime);
			base.InvokeRepeating("time", 0f, 1f);
			BaseButton arg_1E7_0 = new BaseButton(base.getTransformByPath("title/btn_close"), 1, 1);
			Action<GameObject> arg_1E7_1;
			if ((arg_1E7_1 = a3_new_pet.<>c.<>9__26_0) == null)
			{
				arg_1E7_1 = (a3_new_pet.<>c.<>9__26_0 = new Action<GameObject>(a3_new_pet.<>c.<>9.<init>b__26_0));
			}
			arg_1E7_0.onClick = arg_1E7_1;
			BaseButton baseButton = new BaseButton(base.getTransformByPath("buyshow/touchClose"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.close);
			new BaseButton(base.getTransformByPath("showSth/help"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("help").SetActive(true);
			};
			new BaseButton(base.getTransformByPath("help/btn"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("help").SetActive(false);
			};
			BaseButton arg_28C_0 = new BaseButton(base.getTransformByPath("top/jingbi/Image"), 1, 1);
			Action<GameObject> arg_28C_1;
			if ((arg_28C_1 = a3_new_pet.<>c.<>9__26_3) == null)
			{
				arg_28C_1 = (a3_new_pet.<>c.<>9__26_3 = new Action<GameObject>(a3_new_pet.<>c.<>9.<init>b__26_3));
			}
			arg_28C_0.onClick = arg_28C_1;
			BaseButton arg_2C3_0 = new BaseButton(base.getTransformByPath("top/zuanshi/Image"), 1, 1);
			Action<GameObject> arg_2C3_1;
			if ((arg_2C3_1 = a3_new_pet.<>c.<>9__26_4) == null)
			{
				arg_2C3_1 = (a3_new_pet.<>c.<>9__26_4 = new Action<GameObject>(a3_new_pet.<>c.<>9.<init>b__26_4));
			}
			arg_2C3_0.onClick = arg_2C3_1;
			BaseButton arg_2FA_0 = new BaseButton(base.getTransformByPath("top/bangzuan/Image"), 1, 1);
			Action<GameObject> arg_2FA_1;
			if ((arg_2FA_1 = a3_new_pet.<>c.<>9__26_5) == null)
			{
				arg_2FA_1 = (a3_new_pet.<>c.<>9__26_5 = new Action<GameObject>(a3_new_pet.<>c.<>9.<init>b__26_5));
			}
			arg_2FA_0.onClick = arg_2FA_1;
			new BaseButton(base.getTransformByPath("timeBuy/buyIt"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag7 = this.all <= -1L;
				if (flag7)
				{
					flytxt.instance.fly("您的宠物饲料已是永久", 0, default(Color), null);
				}
				else
				{
					base.getGameObjectByPath("buyshow").SetActive(true);
				}
			};
			new BaseButton(base.getTransformByPath("buyshow/scollBuy/content/buy0/image"), 1, 1).onClick = delegate(GameObject go)
			{
				BaseProxy<A3_PetProxy>.getInstance().SendTime(1);
				base.getGameObjectByPath("buyshow").SetActive(false);
				base.getGameObjectByPath("buyshow/scollBuy/content/buy0").SetActive(false);
			};
			new BaseButton(base.getTransformByPath("buyshow/scollBuy/content/buy1/image"), 1, 1).onClick = delegate(GameObject go)
			{
				BaseProxy<A3_PetProxy>.getInstance().SendTime(2);
				base.getGameObjectByPath("buyshow").SetActive(false);
			};
			new BaseButton(base.getTransformByPath("buyshow/scollBuy/content/buy2/image"), 1, 1).onClick = delegate(GameObject go)
			{
				BaseProxy<A3_PetProxy>.getInstance().SendTime(3);
				base.getGameObjectByPath("buyshow").SetActive(false);
			};
			new BaseButton(base.getTransformByPath("buyshow/scollBuy/content/buy3/image"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("timeBuy/buyIt").SetActive(false);
				BaseProxy<A3_PetProxy>.getInstance().SendTime(4);
				List<SXML> nodeList = this.xml.GetNodeList("buy", "");
				int @int = nodeList[3].getInt("zuan");
				bool flag7 = (long)@int >= (long)((ulong)ModelBase<PlayerModel>.getInstance().gold);
				if (flag7)
				{
					base.getGameObjectByPath("buyshow").SetActive(false);
				}
			};
			this.iconTemp = base.getGameObjectByPath("icon_temp");
			this.xml = XMLMgr.instance.GetSXML("newpet", "");
			this.list = this.xml.GetNodeList("pet", "");
			this.petNum = this.list.Count;
			this.cont = base.getTransformByPath("scoll/cont");
			this.sizeY = this.cont.GetComponent<GridLayoutGroup>().cellSize.y;
			this.cont.GetComponent<RectTransform>().sizeDelta = new Vector2(130f, this.sizeY * (float)this.petNum);
			base.transform.FindChild("ig_bg_bg").gameObject.SetActive(false);
			base.getEventTrigerByPath("rotate").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			A3_PetModel a3_PetModel = ModelBase<A3_PetModel>.getInstance();
			base.getTransformByPath("shuxing/exp/Text").GetComponent<Text>().text = "经验上升 " + this.list[0].getInt("exp") + "%";
			base.getTransformByPath("shuxing/gold/Text").GetComponent<Text>().text = "金币上升 " + this.list[0].getInt("gold") + "%";
			base.getTransformByPath("shuxing/equip/Text").GetComponent<Text>().text = "装备掉率 " + this.list[0].getInt("arm") + "%";
			base.getTransformByPath("shuxing/mate/Text").GetComponent<Text>().text = "材料掉率 " + this.list[0].getInt("mat") + "%";
			foreach (SXML current in this.list)
			{
				this.goIcon = UnityEngine.Object.Instantiate<GameObject>(this.iconTemp);
				this.goIcon.transform.SetParent(base.getTransformByPath("scoll/cont"));
				this.goIcon.name = current.getString("mod");
				this.goIcon.SetActive(true);
				this.goIcon.transform.localScale = Vector3.one;
				string path = "scoll/cont/" + this.goIcon.name + "/icon_bg/icon";
				base.transform.FindChild("scoll/cont/" + this.list[0].getString("mod") + "/icon_bg/image_on").gameObject.SetActive(true);
				base.getTransformByPath(path).GetComponent<Image>().sprite = (Resources.Load("icon/pet/" + this.goIcon.name, typeof(Sprite)) as Sprite);
				new BaseButton(base.transform.FindChild("scoll/cont/" + this.goIcon.name + "/icon_bg/icon"), 1, 1).onClick = delegate(GameObject go)
				{
					string name = go.transform.parent.parent.name;
					for (int i = 0; i < this.list.Count; i++)
					{
						bool flag7 = name == this.list[i].getString("mod");
						if (flag7)
						{
							int @int = this.list[i].getInt("exp");
							int int2 = this.list[i].getInt("gold");
							int int3 = this.list[i].getInt("arm");
							int int4 = this.list[i].getInt("mat");
							string a = name;
							if (!(a == "eagle"))
							{
								if (!(a == "yingwu"))
								{
									if (a == "yaque")
									{
										this.petid = 3;
									}
								}
								else
								{
									this.petid = 5;
								}
							}
							else
							{
								this.petid = 2;
							}
							base.getTransformByPath("shuxing/exp/Text").GetComponent<Text>().text = "经验上升 " + @int + "%";
							base.getTransformByPath("shuxing/gold/Text").GetComponent<Text>().text = "金币上升 " + int2 + "%";
							base.getTransformByPath("shuxing/equip/Text").GetComponent<Text>().text = "装备掉率 " + int3 + "%";
							base.getTransformByPath("shuxing/mate/Text").GetComponent<Text>().text = "材料掉率 " + int4 + "%";
							base.transform.FindChild("scoll/cont/" + name + "/icon_bg/image_on").gameObject.SetActive(true);
							BaseProxy<A3_PetProxy>.getInstance().SendPetId(this.petid);
						}
						else
						{
							base.transform.FindChild("scoll/cont/" + this.list[i].getString("mod") + "/icon_bg/image_on").gameObject.SetActive(false);
						}
					}
					this.petgo.createAvatar();
					this.petgo.creatPetAvatar(this.petid);
					debug.Log(this.feedPetTime + ":::::::::::::");
					bool flag8 = (this.feedPetTime == 0L || this.feedPetTime - (long)muNetCleint.instance.CurServerTimeStamp < 1L) && this.lastTime == 0L;
					if (flag8)
					{
						bool flag9 = this.feedPetTime == -1L;
						if (flag9)
						{
							SelfRole._inst.ChangePetAvatar(this.petid, 0);
						}
						else
						{
							flytxt.instance.fly(ContMgr.getCont("pet_buy_feed", null), 0, default(Color), null);
						}
					}
					else
					{
						SelfRole._inst.ChangePetAvatar(this.petid, 0);
					}
				};
				BaseButton arg_740_0 = new BaseButton(base.transform.FindChild("scoll/cont/" + this.goIcon.name + "/lock"), 1, 1);
				Action<GameObject> arg_740_1;
				if ((arg_740_1 = a3_new_pet.<>c.<>9__26_12) == null)
				{
					arg_740_1 = (a3_new_pet.<>c.<>9__26_12 = new Action<GameObject>(a3_new_pet.<>c.<>9.<init>b__26_12));
				}
				arg_740_0.onClick = arg_740_1;
			}
			this.getPet0();
			bool flag6 = a3_everydayLogin.instans != null && a3_everydayLogin.instans.open;
			if (flag6)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_EVERYDAYLOGIN);
				this.openEveryLogin = true;
			}
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

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.pet_avater != null;
			if (flag)
			{
				this.pet_avater.transform.Rotate(Vector3.up, -delta.x);
			}
		}

		private void showPet(GameEvent e)
		{
			SelfRole._inst.ChangePetAvatar(this.petid, 0);
		}

		private void feedPet(GameEvent e)
		{
			this.feedPetTime = e.data["pet_food_last_time"];
		}

		private void getlastTime(GameEvent e)
		{
			this.addTime = (long)(e.data["pet_food_last_time"] - muNetCleint.instance.CurServerTimeStamp);
			bool flag = e.data["pet_food_last_time"] == -1;
			if (flag)
			{
				this.addTime = -1L;
			}
			bool flag2 = this.addTime == -1L;
			if (flag2)
			{
				this.timeGo.text = ContMgr.getCont("pet_forever", null);
			}
			bool flag3 = e.data.ContainsKey("first_pet_food");
			if (flag3)
			{
				this.first = e.data["first_pet_food"];
			}
			this.timeSet(this.addTime);
		}

		private void getPet(GameEvent e)
		{
			ModelBase<A3_PetModel>.getInstance().petId = e.data["pet"]["id_arr"]._arr;
			int i = 0;
			while (i < ModelBase<A3_PetModel>.getInstance().petId.Count)
			{
				int num = ModelBase<A3_PetModel>.getInstance().petId[i];
				string str = "";
				switch (num)
				{
				case 2:
					str = "eagle";
					break;
				case 3:
					str = "yaque";
					break;
				case 5:
					str = "yingwu";
					break;
				}
				IL_84:
				base.transform.FindChild("scoll/cont/" + str + "/lock").gameObject.SetActive(false);
				base.transform.FindChild("scoll/cont/" + str + "/lock/image_lock").gameObject.SetActive(false);
				i++;
				continue;
				goto IL_84;
			}
		}

		private void havePet(GameEvent e)
		{
		}

		private void getPet0()
		{
			bool flag = ModelBase<A3_PetModel>.getInstance().petId == null;
			if (!flag)
			{
				int i = 0;
				while (i < ModelBase<A3_PetModel>.getInstance().petId.Count)
				{
					int num = ModelBase<A3_PetModel>.getInstance().petId[i];
					string str = "";
					switch (num)
					{
					case 2:
						str = "eagle";
						break;
					case 3:
						str = "yaque";
						break;
					case 5:
						str = "yingwu";
						break;
					}
					IL_74:
					base.transform.FindChild("scoll/cont/" + str + "/lock").gameObject.SetActive(false);
					base.transform.FindChild("scoll/cont/" + str + "/lock/image_lock").gameObject.SetActive(false);
					i++;
					continue;
					goto IL_74;
				}
			}
		}

		public override void onShowed()
		{
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_GET_LAST_TIME, new Action<GameEvent>(this.getlastTime));
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_SHOW_PET, new Action<GameEvent>(this.showPet));
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_FEED_PET, new Action<GameEvent>(this.feedPet));
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_HAVE_PET, new Action<GameEvent>(this.havePet));
			UIClient.instance.addEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			this.refreshMoney();
			this.refreshGold();
			this.refreshGift();
			this.petgo.createAvatar();
			this.petgo.creatPetAvatar(this.petid);
			this.petgo.canshow = true;
			bool showbuy = A3_PetModel.showbuy;
			if (showbuy)
			{
				base.getGameObjectByPath("buyshow").SetActive(true);
			}
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			GRMap.GAME_CAMERA.SetActive(false);
		}

		public override void onClosed()
		{
			BaseProxy<A3_PetProxy>.getInstance().removeEventListener(A3_PetProxy.EVENT_GET_LAST_TIME, new Action<GameEvent>(this.getlastTime));
			BaseProxy<A3_PetProxy>.getInstance().removeEventListener(A3_PetProxy.EVENT_SHOW_PET, new Action<GameEvent>(this.showPet));
			BaseProxy<A3_PetProxy>.getInstance().removeEventListener(A3_PetProxy.EVENT_FEED_PET, new Action<GameEvent>(this.feedPet));
			BaseProxy<A3_PetProxy>.getInstance().removeEventListener(A3_PetProxy.EVENT_HAVE_PET, new Action<GameEvent>(this.havePet));
			UIClient.instance.removeEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			A3_PetModel.showbuy = false;
			this.petgo.canshow = false;
			this.petgo.disposeAvatar();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
			bool flag = this.openEveryLogin;
			if (flag)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EVERYDAYLOGIN, null, false);
				this.openEveryLogin = false;
			}
		}

		private void timeSet(long add)
		{
			this.last_times = add;
			debug.Log("宠物饲料总时间" + this.last_times.ToString() + "秒");
			this.year = this.last_times / 31104000L;
			this.month = this.last_times / 2592000L - this.year * 12L;
			this.day = this.last_times / 86400L - this.month * 30L - this.year * 12L * 30L;
			this.hour = this.last_times / 3600L - this.day * 24L - this.month * 30L * 24L - this.year * 12L * 30L * 24L;
			this.min = this.last_times / 60L - this.hour * 60L - this.day * 24L * 60L - this.month * 30L * 24L * 60L - this.year * 12L * 30L * 24L * 60L;
			this.sen = this.last_times - this.min * 60L - this.hour * 3600L - this.day * 24L * 3600L - this.month * 30L * 24L * 3600L - this.year * 12L * 30L * 24L * 3600L;
			this.all = this.last_times;
			bool flag = add == -1L || this.lastTime == -1L;
			if (flag)
			{
				this.all = -1L;
			}
		}

		private void time()
		{
			bool flag = this.year <= 0L;
			if (flag)
			{
				this.year = 0L;
			}
			bool flag2 = this.month <= 0L && this.day <= 0L && this.hour <= 0L && this.min <= 0L && this.sen <= 0L;
			if (flag2)
			{
				bool flag3 = this.year > 0L;
				if (flag3)
				{
					this.month = 11L;
					this.day = 29L;
					this.hour = 23L;
					this.min = 59L;
					this.sen = 59L;
					this.year -= 1L;
				}
				else
				{
					this.month = 0L;
				}
			}
			bool flag4 = this.day <= 0L && this.hour <= 0L && this.min <= 0L && this.sen <= 0L;
			if (flag4)
			{
				bool flag5 = this.month > 0L;
				if (flag5)
				{
					this.day = 29L;
					this.hour = 23L;
					this.min = 59L;
					this.sen = 59L;
					this.month -= 1L;
				}
				else
				{
					this.day = 0L;
				}
			}
			bool flag6 = this.hour <= 0L && this.min <= 0L && this.sen <= 0L;
			if (flag6)
			{
				bool flag7 = this.day > 0L;
				if (flag7)
				{
					this.hour = 23L;
					this.min = 59L;
					this.sen = 59L;
					this.day -= 1L;
				}
				else
				{
					this.hour = 0L;
				}
			}
			bool flag8 = this.min <= 0L && this.sen <= 0L;
			if (flag8)
			{
				bool flag9 = this.hour > 0L;
				if (flag9)
				{
					this.min = 59L;
					this.sen = 59L;
					this.hour -= 1L;
				}
				else
				{
					this.min = 0L;
				}
			}
			bool flag10 = this.sen <= 0L;
			if (flag10)
			{
				bool flag11 = this.min > 0L;
				if (flag11)
				{
					this.sen = 59L;
					this.min -= 1L;
				}
				else
				{
					this.sen = 0L;
				}
			}
			bool flag12 = this.all == 0L;
			if (flag12)
			{
				this.timeGo.text = ContMgr.getCont("pet_invalid", null);
				this.all = 0L;
			}
			else
			{
				bool flag13 = this.all == -1L;
				if (flag13)
				{
					this.timeGo.text = ContMgr.getCont("pet_forever", null);
				}
				else
				{
					this.timeGo.text = string.Concat(new object[]
					{
						"剩余时间:",
						this.year,
						"年",
						this.month,
						"月",
						this.day,
						"天",
						this.hour,
						"时",
						this.min,
						"分",
						this.sen,
						"秒"
					});
					this.sen -= 1L;
					this.all -= 1L;
				}
			}
		}

		private void close(GameObject go)
		{
			base.getGameObjectByPath("buyshow").SetActive(false);
		}
	}
}
