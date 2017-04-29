using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_warehouse : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_warehouse.<>c <>9 = new a3_warehouse.<>c();

			public static Action<GameObject> <>9__21_2;

			internal void <init>b__21_2(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
			}
		}

		private GameObject itemListView;

		private GameObject houseListView;

		private GridLayoutGroup item_Parent;

		private GridLayoutGroup house_Parent;

		private ScrollControler scrollControler;

		private Dictionary<uint, GameObject> itemicon = new Dictionary<uint, GameObject>();

		private Dictionary<uint, GameObject> houseicon = new Dictionary<uint, GameObject>();

		private Scrollbar open_bar;

		private int cur_num = 1;

		private bool isbag_open = false;

		private int open_choose_tag = 1;

		private bool is_auto = false;

		private Text money;

		private Text gold;

		private Text coin;

		private Text textPageIndex_right;

		private Text textPageIndex_left;

		private int pageIndex_right = 1;

		private int maxPageNum_right = 6;

		private int pageIndex_left = 1;

		private int maxPageNum_left = 6;

		private uint nextid = 0u;

		private uint nextid1 = 0u;

		private bool needEvent = true;

		public override void init()
		{
			this.money = base.transform.FindChild("money_bg/money").GetComponent<Text>();
			this.gold = base.transform.FindChild("gem_bg/stone").GetComponent<Text>();
			this.coin = base.transform.FindChild("bdgem_bg/bindstone").GetComponent<Text>();
			this.textPageIndex_right = base.getComponentByPath<Text>("page_right/Text");
			this.textPageIndex_left = base.getComponentByPath<Text>("page_left/Text");
			this.itemListView = base.transform.FindChild("bag_scroll/scroll_view/contain").gameObject;
			this.item_Parent = this.itemListView.GetComponent<GridLayoutGroup>();
			this.houseListView = base.transform.FindChild("house_scroll/scroll_view/contain").gameObject;
			this.house_Parent = this.houseListView.GetComponent<GridLayoutGroup>();
			BaseButton baseButton = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onclose);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("close_btn"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onclose);
			new BaseButton(base.transform.FindChild("money_bg/money/add_money"), 1, 1).onClick = delegate(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EXCHANGE, null, false);
				a3_exchange.Instance.transform.SetSiblingIndex(base.transform.GetSiblingIndex() + 1);
			};
			new BaseButton(base.transform.FindChild("gem_bg/stone/add_stone"), 1, 1).onClick = delegate(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				a3_Recharge expr_18 = a3_Recharge.Instance;
				if (expr_18 != null)
				{
					expr_18.transform.SetSiblingIndex(base.transform.GetSiblingIndex() + 1);
				}
			};
			BaseButton arg_1AA_0 = new BaseButton(base.transform.FindChild("bdgem_bg/bindstone/add_bangstone"), 1, 1);
			Action<GameObject> arg_1AA_1;
			if ((arg_1AA_1 = a3_warehouse.<>c.<>9__21_2) == null)
			{
				arg_1AA_1 = (a3_warehouse.<>c.<>9__21_2 = new Action<GameObject>(a3_warehouse.<>c.<>9.<init>b__21_2));
			}
			arg_1AA_0.onClick = arg_1AA_1;
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("panel_open/open"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onOpenLock);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("panel_open/close"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onCloseOpen);
			this.scrollControler = new ScrollControler();
			ScrollRect component = base.transform.FindChild("bag_scroll/scroll_view").GetComponent<ScrollRect>();
			this.scrollControler.create(component, 4);
			this.open_bar = base.transform.FindChild("panel_open/Scrollbar").GetComponent<Scrollbar>();
			this.open_bar.onValueChanged.AddListener(new UnityAction<float>(this.onNumChange));
			for (int i = 50; i < this.itemListView.transform.childCount; i++)
			{
				bool flag = i >= ModelBase<a3_BagModel>.getInstance().curi;
				if (flag)
				{
					GameObject lockig = this.itemListView.transform.GetChild(i).FindChild("lock").gameObject;
					lockig.SetActive(true);
					int tag = i + 1;
					BaseButton baseButton5 = new BaseButton(lockig.transform, 1, 1);
					baseButton5.onClick = delegate(GameObject go)
					{
						this.onClickOpenBagLock(lockig, tag);
					};
				}
			}
			for (int j = 10; j < this.houseListView.transform.childCount; j++)
			{
				bool flag2 = j >= ModelBase<a3_BagModel>.getInstance().house_curi;
				if (flag2)
				{
					GameObject lockig = this.houseListView.transform.GetChild(j).FindChild("lock").gameObject;
					lockig.SetActive(true);
					int tag = j + 1;
					BaseButton baseButton6 = new BaseButton(lockig.transform, 1, 1);
					baseButton6.onClick = delegate(GameObject go)
					{
						this.onClickOpenHouseLock(lockig, tag);
					};
				}
			}
			for (int k = 1; k <= 2; k++)
			{
				Toggle component2 = base.transform.FindChild("panel_open/open_choose/Toggle" + k).GetComponent<Toggle>();
				int tag = k;
				component2.onValueChanged.AddListener(delegate(bool isOn)
				{
					this.open_choose_tag = tag;
					this.checkNumChange();
				});
			}
			Toggle component3 = base.transform.FindChild("auto").GetComponent<Toggle>();
			component3.onValueChanged.AddListener(delegate(bool isOn)
			{
				this.is_auto = isOn;
			});
			new BaseButton(base.transform.FindChild("page_right/right"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag3 = this.pageIndex_right < this.maxPageNum_right;
				if (flag3)
				{
					this.pageIndex_right++;
				}
				this.show_page_right();
			};
			new BaseButton(base.transform.FindChild("page_right/left"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag3 = this.pageIndex_right > 1;
				if (flag3)
				{
					this.pageIndex_right--;
				}
				this.show_page_right();
			};
			new BaseButton(base.transform.FindChild("page_left/right"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag3 = this.pageIndex_left < this.maxPageNum_left;
				if (flag3)
				{
					this.pageIndex_left++;
				}
				this.show_page_left();
			};
			new BaseButton(base.transform.FindChild("page_left/left"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag3 = this.pageIndex_left > 1;
				if (flag3)
				{
					this.pageIndex_left--;
				}
				this.show_page_left();
			};
			base.InvokeRepeating("OnShowAchievementPage_right", 0f, 0.3f);
			base.InvokeRepeating("OnShowAchievementPage_left", 0f, 0.3f);
		}

		private void OnShowAchievementPage_right()
		{
			float y = this.itemListView.GetComponent<RectTransform>().anchoredPosition.y;
			float y2 = base.transform.FindChild("bag_scroll/scroll_view/icon").GetComponent<RectTransform>().sizeDelta.y;
			bool flag = y < y2;
			if (flag)
			{
				this.pageIndex_right = 1;
				this.textPageIndex_right.text = 1 + "/" + this.maxPageNum_right;
			}
			else
			{
				for (int i = 2; i <= this.maxPageNum_right; i++)
				{
					bool flag2 = y >= y2 && y >= 5f * y2 * (float)i - 8f * y2 && y < 5f * y2 * (float)(i + 1) - 8f * y2;
					if (flag2)
					{
						this.pageIndex_right = i;
						this.textPageIndex_right.text = i + "/" + this.maxPageNum_right;
					}
				}
			}
		}

		private void OnShowAchievementPage_left()
		{
			float y = this.houseListView.GetComponent<RectTransform>().anchoredPosition.y;
			float y2 = base.transform.FindChild("house_scroll/scroll_view/icon").GetComponent<RectTransform>().sizeDelta.y;
			bool flag = y < y2;
			if (flag)
			{
				this.pageIndex_left = 1;
				this.textPageIndex_left.text = 1 + "/" + this.maxPageNum_left;
			}
			else
			{
				for (int i = 2; i <= this.maxPageNum_left; i++)
				{
					bool flag2 = y >= y2 && y >= 4f * y2 * (float)i - 6f * y2 && y < 4f * y2 * (float)(i + 1) - 6f * y2;
					if (flag2)
					{
						this.pageIndex_left = i;
						this.textPageIndex_left.text = i + "/" + this.maxPageNum_left;
					}
				}
			}
		}

		private void show_page_left()
		{
			float num = this.houseListView.GetComponent<GridLayoutGroup>().cellSize.y + this.houseListView.GetComponent<GridLayoutGroup>().spacing.y;
			this.textPageIndex_left.text = this.pageIndex_left + "/" + this.maxPageNum_left;
			this.houseListView.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, (float)((this.pageIndex_left - 1) * 5) * num, 0f);
		}

		private void show_page_right()
		{
			float y = base.transform.FindChild("bag_scroll/scroll_view/icon").GetComponent<RectTransform>().sizeDelta.y;
			this.textPageIndex_right.text = this.pageIndex_right + "/" + this.maxPageNum_right;
			this.itemListView.GetComponent<RectTransform>().anchoredPosition = new Vector3(2.5f, (float)((this.pageIndex_right - 1) * 5) * y, 0f);
		}

		public override void onShowed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			GRMap.GAME_CAMERA.SetActive(false);
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_ITEM_CHANGE, new Action<GameEvent>(this.onItemChange));
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_OPEN_BAGLOCK, new Action<GameEvent>(this.onOpenBagLockRec));
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_OPEN_HOUSELOCK, new Action<GameEvent>(this.onOpenHouseLockRec));
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_ROOM_TURN, new Action<GameEvent>(this.onRoomTurn));
			UIClient.instance.addEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			this.onOpenBagLockRec(null);
			this.onOpenHouseLockRec(null);
			this.refreshGold();
			this.refreshGift();
			this.refreshMoney();
			this.onLoadItem();
			this.open_choose_tag = 1;
			for (int i = 1; i <= 2; i++)
			{
				Toggle component = base.transform.FindChild("panel_open/open_choose/Toggle" + i).GetComponent<Toggle>();
				bool flag = i == this.open_choose_tag;
				if (flag)
				{
					component.isOn = true;
				}
				else
				{
					component.isOn = false;
				}
			}
			base.transform.FindChild("auto").GetComponent<Toggle>().isOn = false;
			this.is_auto = false;
		}

		public void refreshMoney()
		{
			this.money.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().money);
		}

		public void refreshGold()
		{
			this.gold.text = ModelBase<PlayerModel>.getInstance().gold.ToString();
		}

		public void refreshGift()
		{
			this.coin.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
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

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
			BaseProxy<BagProxy>.getInstance().removeEventListener(BagProxy.EVENT_ITEM_CHANGE, new Action<GameEvent>(this.onItemChange));
			BaseProxy<BagProxy>.getInstance().removeEventListener(BagProxy.EVENT_OPEN_BAGLOCK, new Action<GameEvent>(this.onOpenBagLockRec));
			BaseProxy<BagProxy>.getInstance().removeEventListener(BagProxy.EVENT_OPEN_HOUSELOCK, new Action<GameEvent>(this.onOpenHouseLockRec));
			BaseProxy<BagProxy>.getInstance().removeEventListener(BagProxy.EVENT_ROOM_TURN, new Action<GameEvent>(this.onRoomTurn));
			UIClient.instance.removeEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			foreach (GameObject current in this.itemicon.Values)
			{
				UnityEngine.Object.Destroy(current);
			}
			this.itemicon.Clear();
			foreach (GameObject current2 in this.houseicon.Values)
			{
				UnityEngine.Object.Destroy(current2);
			}
			this.houseicon.Clear();
			bool flag = a3_bag.indtans;
			if (flag)
			{
				bool isbagToCK = a3_bag.indtans.isbagToCK;
				if (isbagToCK)
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_BAG, null, false);
					a3_bag.indtans.isbagToCK = false;
				}
			}
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_WAREHOUSE);
			A3_Smithy expr_16 = A3_Smithy.Instance;
			bool flag = expr_16 != null && expr_16.gameObject.activeSelf;
			if (flag)
			{
			}
		}

		public void onLoadItem()
		{
			Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
			int num = 0;
			foreach (a3_BagItemData current in items.Values)
			{
				this.CreateItemIcon(current, this.item_Parent.transform.GetChild(num), false);
				num++;
			}
			Dictionary<uint, a3_BagItemData> houseItems = ModelBase<a3_BagModel>.getInstance().getHouseItems();
			int num2 = 0;
			foreach (a3_BagItemData current2 in houseItems.Values)
			{
				this.CreateItemIcon(current2, this.house_Parent.transform.GetChild(num2), true);
				num2++;
			}
		}

		private void onItemChange(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("add");
			if (flag)
			{
				foreach (Variant current in data["add"]._arr)
				{
					uint key = current["id"];
					bool flag2 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(key);
					if (flag2)
					{
						bool flag3 = this.item_Parent.transform.GetChild(this.itemicon.Count).childCount > 1;
						if (flag3)
						{
							for (int i = 0; i < this.item_Parent.transform.GetChild(this.itemicon.Count).childCount; i++)
							{
								bool flag4 = this.item_Parent.transform.GetChild(this.itemicon.Count).GetChild(i).name == "lock";
								if (!flag4)
								{
									UnityEngine.Object.Destroy(this.item_Parent.transform.GetChild(this.itemicon.Count).GetChild(i).gameObject);
								}
							}
						}
						a3_BagItemData data2 = ModelBase<a3_BagModel>.getInstance().getItems(false)[key];
						this.CreateItemIcon(data2, this.item_Parent.transform.GetChild(this.itemicon.Count), false);
					}
				}
			}
			bool flag5 = data.ContainsKey("modcnts");
			if (flag5)
			{
				foreach (Variant current2 in data["modcnts"]._arr)
				{
					uint key2 = current2["id"];
					bool flag6 = this.itemicon.ContainsKey(key2);
					if (flag6)
					{
						this.itemicon[key2].transform.FindChild("num").GetComponent<Text>().text = current2["cnt"];
						bool flag7 = current2["cnt"] <= 1;
						if (flag7)
						{
							this.itemicon[key2].transform.FindChild("num").gameObject.SetActive(false);
						}
						else
						{
							this.itemicon[key2].transform.FindChild("num").gameObject.SetActive(true);
						}
					}
				}
			}
			bool flag8 = data.ContainsKey("rmvids");
			if (flag8)
			{
				using (List<Variant>.Enumerator enumerator3 = data["rmvids"]._arr.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						uint num = enumerator3.Current;
						uint key3 = num;
						bool flag9 = this.itemicon.ContainsKey(key3);
						if (flag9)
						{
							GameObject gameObject = this.itemicon[key3].transform.parent.gameObject;
							UnityEngine.Object.Destroy(gameObject);
							this.itemicon.Remove(key3);
							GameObject gameObject2 = base.transform.FindChild("bag_scroll/scroll_view/icon").gameObject;
							GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
							gameObject3.SetActive(true);
							gameObject3.transform.SetParent(this.item_Parent.transform, false);
							gameObject3.transform.SetSiblingIndex(this.itemicon.Count + 1);
						}
					}
				}
			}
		}

		private void onRoomTurn(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("add");
			if (flag)
			{
				foreach (Variant current in data["add"]._arr)
				{
					uint key = current["id"];
					bool flag2 = ModelBase<a3_BagModel>.getInstance().getHouseItems().ContainsKey(key);
					if (flag2)
					{
						a3_BagItemData data2 = ModelBase<a3_BagModel>.getInstance().getHouseItems()[key];
						this.CreateItemIcon(data2, this.house_Parent.transform.GetChild(this.houseicon.Count), true);
					}
				}
			}
			bool flag3 = data.ContainsKey("modcnts");
			if (flag3)
			{
				foreach (Variant current2 in data["modcnts"]._arr)
				{
					uint key2 = current2["id"];
					bool flag4 = this.houseicon.ContainsKey(key2);
					if (flag4)
					{
						this.houseicon[key2].transform.FindChild("num").GetComponent<Text>().text = current2["cnt"];
						bool flag5 = current2["cnt"] <= 1;
						if (flag5)
						{
							this.houseicon[key2].transform.FindChild("num").gameObject.SetActive(false);
						}
						else
						{
							this.houseicon[key2].transform.FindChild("num").gameObject.SetActive(true);
						}
					}
				}
			}
			bool flag6 = data.ContainsKey("rmvids");
			if (flag6)
			{
				using (List<Variant>.Enumerator enumerator3 = data["rmvids"]._arr.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						uint num = enumerator3.Current;
						uint key3 = num;
						bool flag7 = this.houseicon.ContainsKey(key3);
						if (flag7)
						{
							GameObject gameObject = this.houseicon[key3].transform.parent.gameObject;
							UnityEngine.Object.Destroy(gameObject);
							this.houseicon.Remove(key3);
							GameObject gameObject2 = base.transform.FindChild("house_scroll/scroll_view/icon").gameObject;
							GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
							gameObject3.SetActive(true);
							gameObject3.transform.SetParent(this.house_Parent.transform, false);
							gameObject3.transform.SetSiblingIndex(this.houseicon.Count + 1);
						}
					}
				}
			}
		}

		private void onOpenBagLockRec(GameEvent e)
		{
			for (int i = 50; i < this.itemListView.transform.childCount; i++)
			{
				GameObject gameObject = this.itemListView.transform.GetChild(i).FindChild("lock").gameObject;
				bool flag = i >= ModelBase<a3_BagModel>.getInstance().curi;
				if (flag)
				{
					gameObject.SetActive(true);
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
		}

		private void onOpenHouseLockRec(GameEvent e)
		{
			for (int i = 10; i < this.houseListView.transform.childCount; i++)
			{
				GameObject gameObject = this.houseListView.transform.GetChild(i).FindChild("lock").gameObject;
				bool flag = i >= ModelBase<a3_BagModel>.getInstance().house_curi;
				if (flag)
				{
					gameObject.SetActive(true);
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
		}

		private void CreateItemIcon(a3_BagItemData data, Transform parent, bool ishouse = false)
		{
			GameObject icon = IconImageMgr.getInstance().createA3ItemIcon(data, true, data.num, 1f, false);
			icon.transform.SetParent(parent, false);
			bool ishouse2 = ishouse;
			if (ishouse2)
			{
				this.houseicon[data.id] = icon;
			}
			else
			{
				this.itemicon[data.id] = icon;
			}
			bool flag = data.num <= 1;
			if (flag)
			{
				icon.transform.FindChild("num").gameObject.SetActive(false);
			}
			BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.onItemClick(icon, data.id, ishouse);
			};
		}

		private void onItemClick(GameObject go, uint id, bool ishouse)
		{
			if (ishouse)
			{
				a3_BagItemData a3_BagItemData = ModelBase<a3_BagModel>.getInstance().getHouseItems()[id];
				bool flag = this.is_auto;
				if (flag)
				{
					bool flag2 = this.nextid != id;
					if (flag2)
					{
						BaseProxy<BagProxy>.getInstance().sendRoomItems(false, a3_BagItemData.id, a3_BagItemData.num);
						this.nextid = id;
					}
				}
				else
				{
					bool isEquip = a3_BagItemData.isEquip;
					if (isEquip)
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add(a3_BagItemData);
						arrayList.Add(equip_tip_type.HouseOut_tip);
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
					}
					else
					{
						bool isSummon = a3_BagItemData.isSummon;
						if (isSummon)
						{
							ArrayList arrayList2 = new ArrayList();
							arrayList2.Add(a3_BagItemData);
							InterfaceMgr.getInstance().open(InterfaceMgr.A3TIPS_SUMMON, arrayList2, false);
						}
						else
						{
							ArrayList arrayList3 = new ArrayList();
							arrayList3.Add(a3_BagItemData);
							arrayList3.Add(equip_tip_type.HouseOut_tip);
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMTIP, arrayList3, false);
						}
					}
				}
			}
			else
			{
				a3_BagItemData a3_BagItemData = ModelBase<a3_BagModel>.getInstance().getItems(false)[id];
				bool flag3 = this.is_auto;
				if (flag3)
				{
					bool flag4 = this.nextid1 != id;
					if (flag4)
					{
						BaseProxy<BagProxy>.getInstance().sendRoomItems(true, a3_BagItemData.id, a3_BagItemData.num);
						this.nextid1 = id;
					}
				}
				else
				{
					bool isNew = a3_BagItemData.isNew;
					if (isNew)
					{
						a3_BagItemData.isNew = false;
						ModelBase<a3_BagModel>.getInstance().addItem(a3_BagItemData);
						this.itemicon[id].transform.FindChild("iconborder/is_new").gameObject.SetActive(false);
					}
					bool isEquip2 = a3_BagItemData.isEquip;
					if (isEquip2)
					{
						ArrayList arrayList4 = new ArrayList();
						arrayList4.Add(a3_BagItemData);
						arrayList4.Add(equip_tip_type.HouseIn_tip);
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList4, false);
					}
					else
					{
						bool isSummon2 = a3_BagItemData.isSummon;
						if (isSummon2)
						{
							ArrayList arrayList5 = new ArrayList();
							arrayList5.Add(a3_BagItemData);
							InterfaceMgr.getInstance().open(InterfaceMgr.A3TIPS_SUMMON, arrayList5, false);
						}
						else
						{
							ArrayList arrayList6 = new ArrayList();
							arrayList6.Add(a3_BagItemData);
							arrayList6.Add(equip_tip_type.HouseIn_tip);
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMTIP, arrayList6, false);
						}
					}
				}
			}
		}

		private void onClickOpenBagLock(GameObject go, int tag)
		{
			this.isbag_open = true;
			base.transform.FindChild("panel_open").gameObject.SetActive(true);
			this.cur_num = tag - ModelBase<a3_BagModel>.getInstance().curi;
			this.needEvent = false;
			this.open_bar.value = (float)this.cur_num / (float)(150 - ModelBase<a3_BagModel>.getInstance().curi);
			this.checkNumChange();
		}

		private void onClickOpenHouseLock(GameObject go, int tag)
		{
			this.isbag_open = false;
			base.transform.FindChild("panel_open").gameObject.SetActive(true);
			this.cur_num = tag - ModelBase<a3_BagModel>.getInstance().house_curi;
			this.needEvent = false;
			this.open_bar.value = (float)this.cur_num / (float)(150 - ModelBase<a3_BagModel>.getInstance().house_curi);
			this.checkNumChange();
		}

		private void onNumChange(float rate)
		{
			bool flag = !this.needEvent;
			if (flag)
			{
				this.needEvent = true;
			}
			else
			{
				bool flag2 = this.isbag_open;
				if (flag2)
				{
					this.cur_num = (int)Math.Floor((double)(rate * (float)(150 - ModelBase<a3_BagModel>.getInstance().curi)));
				}
				else
				{
					this.cur_num = (int)Math.Floor((double)(rate * (float)(150 - ModelBase<a3_BagModel>.getInstance().house_curi)));
				}
				bool flag3 = this.cur_num == 0;
				if (flag3)
				{
					this.cur_num = 1;
				}
				this.checkNumChange();
			}
		}

		private void checkNumChange()
		{
			string text = " ";
			base.transform.FindChild("panel_open/num").GetComponent<Text>().text = this.cur_num.ToString();
			int num = this.open_choose_tag;
			if (num != 1)
			{
				if (num == 2)
				{
					text = string.Format("消耗{0}个绑定砖石开启{1}个格子", 5 * this.cur_num, this.cur_num);
				}
			}
			else
			{
				text = string.Format("消耗{0}个砖石开启{1}个格子", 5 * this.cur_num, this.cur_num);
			}
			base.transform.FindChild("panel_open/desc").GetComponent<Text>().text = text;
		}

		private void onOpenLock(GameObject go)
		{
			base.transform.FindChild("panel_open").gameObject.SetActive(false);
			bool flag = this.isbag_open;
			if (flag)
			{
				bool flag2 = this.open_choose_tag == 1;
				if (flag2)
				{
					BaseProxy<BagProxy>.getInstance().sendOpenLock(2, this.cur_num, true);
				}
				else
				{
					BaseProxy<BagProxy>.getInstance().sendOpenLock(2, this.cur_num, false);
				}
			}
			else
			{
				bool flag3 = this.open_choose_tag == 1;
				if (flag3)
				{
					BaseProxy<BagProxy>.getInstance().sendOpenLock(3, this.cur_num, true);
				}
				else
				{
					BaseProxy<BagProxy>.getInstance().sendOpenLock(3, this.cur_num, false);
				}
			}
		}

		private void onCloseOpen(GameObject go)
		{
			base.transform.FindChild("panel_open").gameObject.SetActive(false);
		}
	}
}
