using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_bag : Window
	{
		public static a3_bag Instance;

		private GameObject m_SelfObj;

		private GameObject scene_Camera;

		private ProfessionAvatar m_proAvatar;

		private GameObject scene_Obj;

		private GameObject itemListView;

		private GridLayoutGroup item_Parent;

		private GridLayoutGroup item_Parent_chushou;

		private GridLayoutGroup item_Parent_fenjie;

		private ScrollControler scrollControler;

		public static a3_bag indtans;

		public static a3_bag isshow;

		private Dictionary<uint, GameObject> itemicon = new Dictionary<uint, GameObject>();

		private Dictionary<int, GameObject> equipicon = new Dictionary<int, GameObject>();

		private Dictionary<uint, GameObject> itemcon_chushou = new Dictionary<uint, GameObject>();

		private Dictionary<uint, GameObject> itemcon_fenjie = new Dictionary<uint, GameObject>();

		private Dictionary<uint, int> cdtype = new Dictionary<uint, int>();

		private GameObject curitem;

		private Dictionary<int, Image> icon_ani = new Dictionary<int, Image>();

		private List<Sprite> ani = new List<Sprite>();

		public GameObject eqpView;

		private Toggle white;

		private Toggle green;

		private Toggle blue;

		private Toggle purple;

		private Toggle orange;

		private Scrollbar open_bar;

		private int cur_num = 1;

		private int open_choose_tag = 1;

		public int mojing_num;

		public int shengguanghuiji_num;

		public int mifageli_num;

		public int GetMoneyNum;

		public bool isbagToCK = false;

		private Text mojing;

		private Text shengguanghuiji;

		private Text mifageli;

		private Text Money;

		private int shellCount = 0;

		private Dictionary<uint, a3_BagItemData> dic_BagItem = new Dictionary<uint, a3_BagItemData>();

		private Dictionary<uint, a3_BagItemData> dic_BagItem_shll = new Dictionary<uint, a3_BagItemData>();

		public Dictionary<int, int> eqpatt = new Dictionary<int, int>();

		public Dictionary<int, int> Uneqpatt = new Dictionary<int, int>();

		private Text qhDashi_text;

		private Text LHlianjie_text;

		private bool Toclose = false;

		private int n = 0;

		private float waitTime = 0.5f;

		private bool isAtt = false;

		private bool needEvent = true;

		private int conIndex = 0;

		private List<Variant> dic_Itemlist = new List<Variant>();

		private List<uint> dic_leftAllid = new List<uint>();

		public override void init()
		{
			a3_bag.Instance = this;
			a3_bag.indtans = this;
			this.itemListView = base.transform.FindChild("item_scroll/scroll_view/contain").gameObject;
			this.item_Parent = this.itemListView.GetComponent<GridLayoutGroup>();
			this.item_Parent_chushou = base.transform.FindChild("piliang_chushou/info_bg/scroll_view/contain").GetComponent<GridLayoutGroup>();
			this.item_Parent_fenjie = base.transform.FindChild("piliang_fenjie/scroll_view/contain").GetComponent<GridLayoutGroup>();
			base.transform.FindChild("item_scroll/v2_open_bag").gameObject.SetActive(true);
			BaseButton baseButton = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onclose);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("piliang_fenjie/close"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onfenjieclose);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("piliang_chushou/close"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onchoushouclose);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("panel_open/close"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onCloseOpen);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("item_scroll/equip"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onEquipSell);
			BaseButton baseButton6 = new BaseButton(base.transform.FindChild("item_scroll/bag"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onZhengLi);
			BaseButton baseButton7 = new BaseButton(base.transform.FindChild("item_scroll/chushou"), 1, 1);
			baseButton7.onClick = new Action<GameObject>(this.onChushou);
			BaseButton baseButton8 = new BaseButton(base.transform.FindChild("ig_bg1/pet"), 1, 1);
			baseButton8.onClick = new Action<GameObject>(this.OnOpenPet);
			BaseButton baseButton9 = new BaseButton(base.transform.FindChild("info/info"), 1, 1);
			baseButton9.onClick = new Action<GameObject>(this.onInfo);
			BaseButton baseButton10 = new BaseButton(base.transform.FindChild("panel_open/open"), 1, 1);
			baseButton10.onClick = new Action<GameObject>(this.onOpenLock);
			BaseButton baseButton11 = new BaseButton(base.transform.FindChild("piliang_fenjie/info_bg/go"), 1, 1);
			baseButton11.onClick = new Action<GameObject>(this.Sendproxy);
			BaseButton baseButton12 = new BaseButton(base.transform.FindChild("piliang_chushou/info_bg/go"), 1, 1);
			baseButton12.onClick = new Action<GameObject>(this.SellItem);
			new BaseButton(base.transform.FindChild("item_scroll/equip/tishi"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("item_scroll/equip/tishi").gameObject.SetActive(false);
			};
			new BaseButton(base.transform.FindChild("QH_dashi/help"), 1, 1).onClick = delegate(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_QHMASTER, null, false);
				base.transform.FindChild("QH_dashi").gameObject.SetActive(false);
			};
			new BaseButton(base.transform.FindChild("LH_lianjie/help"), 1, 1).onClick = delegate(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_LHLIANJIE, null, false);
				base.transform.FindChild("LH_lianjie").gameObject.SetActive(false);
			};
			new BaseButton(base.transform.FindChild("QH_dashi/tach_close"), 1, 1).onClick = (new BaseButton(base.transform.FindChild("QH_dashi/close"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("QH_dashi").gameObject.SetActive(false);
			});
			new BaseButton(base.transform.FindChild("LH_lianjie/tach_close"), 1, 1).onClick = (new BaseButton(base.transform.FindChild("LH_lianjie/close"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("LH_lianjie").gameObject.SetActive(false);
			});
			this.mojing = base.getComponentByPath<Text>("piliang_fenjie/info_bg/mojing/num");
			this.shengguanghuiji = base.getComponentByPath<Text>("piliang_fenjie/info_bg/shenguang/num");
			this.mifageli = base.getComponentByPath<Text>("piliang_fenjie/info_bg/mifa/num");
			this.Money = base.getComponentByPath<Text>("piliang_chushou/money");
			this.qhDashi_text = base.transform.FindChild("qhdashi/Text").GetComponent<Text>();
			this.LHlianjie_text = base.transform.FindChild("LHlianjie/Text").GetComponent<Text>();
			BaseButton baseButton13 = new BaseButton(base.transform.FindChild("qhdashi"), 1, 1);
			baseButton13.onClick = new Action<GameObject>(this.onOpenDashi);
			new BaseButton(base.transform.FindChild("LHlianjie"), 1, 1).onClick = new Action<GameObject>(this.onLHlianjie);
			this.eqpView = base.transform.FindChild("item_scroll/scroll_view").gameObject;
			new BaseButton(base.transform.FindChild("ig_bg1/equip8"), 1, 1).onClick = (new BaseButton(base.transform.FindChild("ig_bg1/equip9"), 1, 1).onClick = (new BaseButton(base.transform.FindChild("ig_bg1/equip10"), 1, 1).onClick = new Action<GameObject>(this.openGetJewelry)));
			this.white = base.getComponentByPath<Toggle>("piliang_fenjie/info_bg/Toggle_all/Toggle_white");
			this.white.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.EquipsSureSell(1);
					this.OnLoadItem_fenjie();
				}
				else
				{
					this.outItemCon_fenjie(1, 0u);
					this.EquipsNoSell(1);
				}
			});
			this.green = base.getComponentByPath<Toggle>("piliang_fenjie/info_bg/Toggle_all/Toggle_green");
			this.green.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.EquipsSureSell(2);
					this.OnLoadItem_fenjie();
					bool flag2 = !this.white.isOn;
					if (flag2)
					{
						this.white.isOn = true;
					}
				}
				else
				{
					this.outItemCon_fenjie(2, 0u);
					this.EquipsNoSell(2);
				}
			});
			this.blue = base.getComponentByPath<Toggle>("piliang_fenjie/info_bg/Toggle_all/Toggle_blue");
			this.blue.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.EquipsSureSell(3);
					this.OnLoadItem_fenjie();
					bool flag2 = !this.green.isOn;
					if (flag2)
					{
						this.green.isOn = true;
					}
				}
				else
				{
					this.outItemCon_fenjie(3, 0u);
					this.EquipsNoSell(3);
				}
			});
			this.purple = base.getComponentByPath<Toggle>("piliang_fenjie/info_bg/Toggle_all/Toggle_puple");
			this.purple.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.EquipsSureSell(4);
					this.OnLoadItem_fenjie();
					bool flag2 = !this.blue.isOn;
					if (flag2)
					{
						this.blue.isOn = true;
					}
				}
				else
				{
					this.outItemCon_fenjie(4, 0u);
					this.EquipsNoSell(4);
				}
			});
			this.orange = base.getComponentByPath<Toggle>("piliang_fenjie/info_bg/Toggle_all/Toggle_orange");
			this.orange.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.EquipsSureSell(5);
					this.OnLoadItem_fenjie();
					bool flag2 = !this.purple.isOn;
					if (flag2)
					{
						this.purple.isOn = true;
					}
				}
				else
				{
					this.outItemCon_fenjie(5, 0u);
					this.EquipsNoSell(5);
				}
			});
			base.getEventTrigerByPath("avatar_touch").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			this.scrollControler = new ScrollControler();
			ScrollRect component = base.transform.FindChild("item_scroll/scroll_view").GetComponent<ScrollRect>();
			this.scrollControler.create(component, 4);
			string path = "";
			switch (ModelBase<PlayerModel>.getInstance().profession)
			{
			case 2:
				path = "icon/job_icon/2";
				break;
			case 3:
				path = "icon/job_icon/3";
				break;
			case 4:
				path = "icon/job_icon/4";
				break;
			case 5:
				path = "icon/job_icon/5";
				break;
			}
			Image component2 = base.transform.FindChild("info/icon").GetComponent<Image>();
			component2.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			for (int i = 0; i < this.itemListView.transform.childCount; i++)
			{
				bool flag = i >= ModelBase<a3_BagModel>.getInstance().curi;
				if (flag)
				{
					GameObject lockig = this.itemListView.transform.GetChild(i).FindChild("lock").gameObject;
					lockig.SetActive(true);
					int tag = i + 1;
					BaseButton baseButton14 = new BaseButton(lockig.transform, 1, 1);
					baseButton14.onClick = delegate(GameObject go)
					{
						this.onOpenLock(lockig, tag);
					};
				}
			}
			this.open_bar = base.transform.FindChild("panel_open/Scrollbar").GetComponent<Scrollbar>();
			this.open_bar.onValueChanged.AddListener(new UnityAction<float>(this.onNumChange));
			for (int j = 1; j <= 2; j++)
			{
				Toggle component3 = base.transform.FindChild("panel_open/open_choose/Toggle" + j).GetComponent<Toggle>();
				int tag = j;
				component3.onValueChanged.AddListener(delegate(bool isOn)
				{
					this.open_choose_tag = tag;
					this.checkNumChange();
				});
			}
			for (int k = 1; k <= 10; k++)
			{
				this.icon_ani[k] = base.transform.FindChild("ig_bg1/ain" + k).GetComponent<Image>();
			}
			for (int l = 1; l <= 15; l += 2)
			{
				Sprite item = Resources.Load("UI_eff_icon_001/icon_001_0" + l, typeof(Sprite)) as Sprite;
				this.ani.Add(item);
			}
		}

		public override void onShowed()
		{
			a3_bag.isshow = this;
			this.isbagToCK = false;
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_ITME_SELL, new Action<GameEvent>(this.onItemSell));
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_ITEM_CHANGE, new Action<GameEvent>(this.onItemChange));
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_OPEN_BAGLOCK, new Action<GameEvent>(this.onOpenLockRec));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_EQUIP_PUTON, new Action<GameEvent>(this.onEquipOn));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_EQUIP_PUTDOWN, new Action<GameEvent>(this.onEquipDown));
			UIClient.instance.addEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			ModelBase<PlayerModel>.getInstance().addEventListener(PlayerModel.ON_ATTR_CHANGE, new Action<GameEvent>(this.onAttrChange));
			this.onLoadItem();
			this.shellCount = base.transform.FindChild("item_scroll/scroll_view/contain").childCount;
			this.eqpView.SetActive(true);
			this.initEquipIcon();
			this.showfenjie_tishi();
			this.refreshMoney();
			this.refreshGold();
			this.refreshGift();
			this.onOpenLockRec(null);
			this.onAttrChange(null);
			base.transform.FindChild("info/name").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().name;
			base.transform.FindChild("info/lv").GetComponent<Text>().text = string.Concat(new object[]
			{
				"Lv",
				ModelBase<PlayerModel>.getInstance().lvl,
				"（",
				ModelBase<PlayerModel>.getInstance().up_lvl,
				"转）"
			});
			this.refreshQHdashi();
			this.refreshLHlianjie();
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
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			base.transform.FindChild("ig_bg_bg").gameObject.SetActive(false);
			this.SetAni_Color();
			bool flag2 = GRMap.GAME_CAMERA != null;
			if (flag2)
			{
				GRMap.GAME_CAMERA.SetActive(false);
			}
			this.createAvatar();
			this.createAvatar_body();
			this.setAni();
			BaseProxy<BagProxy>.getInstance().sendSellItems(4321u, 1);
			this.Toclose = false;
			bool flag3 = ModelBase<A3_VipModel>.getInstance().Level > 0;
			int num;
			if (flag3)
			{
				num = ModelBase<A3_VipModel>.getInstance().vip_exchange_num(14);
			}
			else
			{
				num = 0;
			}
			bool flag4 = num == 1;
			if (flag4)
			{
				base.transform.FindChild("item_scroll/v2_open_bag").gameObject.SetActive(false);
			}
			a3_lottery expr_2F3 = a3_lottery.mInstance;
			bool flag5 = expr_2F3 != null && expr_2F3.is_open;
			if (flag5)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_LOTTERY);
			}
		}

		public override void onClosed()
		{
			a3_bag.isshow = null;
			this.disposeAvatar();
			BaseProxy<BagProxy>.getInstance().removeEventListener(BagProxy.EVENT_ITME_SELL, new Action<GameEvent>(this.onItemSell));
			BaseProxy<BagProxy>.getInstance().removeEventListener(BagProxy.EVENT_ITEM_CHANGE, new Action<GameEvent>(this.onItemChange));
			BaseProxy<BagProxy>.getInstance().removeEventListener(BagProxy.EVENT_OPEN_BAGLOCK, new Action<GameEvent>(this.onOpenLockRec));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_EQUIP_PUTON, new Action<GameEvent>(this.onEquipOn));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_EQUIP_PUTDOWN, new Action<GameEvent>(this.onEquipDown));
			UIClient.instance.removeEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			ModelBase<PlayerModel>.getInstance().removeEventListener(PlayerModel.ON_ATTR_CHANGE, new Action<GameEvent>(this.onAttrChange));
			base.transform.FindChild("QH_dashi").gameObject.SetActive(false);
			base.transform.FindChild("LH_lianjie").gameObject.SetActive(false);
			foreach (GameObject current in this.itemicon.Values)
			{
				UnityEngine.Object.Destroy(current);
			}
			this.itemicon.Clear();
			this.cdtype.Clear();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			bool flag = GRMap.GAME_CAMERA != null;
			if (flag)
			{
				GRMap.GAME_CAMERA.SetActive(true);
			}
			InterfaceMgr.getInstance().itemToWin(this.Toclose, this.uiName);
			a3_lottery expr_1A8 = a3_lottery.mInstance;
			bool flag2 = expr_1A8 != null && expr_1A8.is_open;
			if (flag2)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_LOTTERY, null, false);
			}
		}

		private void Update()
		{
			bool flag = ModelBase<a3_BagModel>.getInstance().getItemCds().Count > 0;
			if (flag)
			{
				foreach (int current in ModelBase<a3_BagModel>.getInstance().getItemCds().Keys)
				{
					foreach (uint current2 in this.cdtype.Keys)
					{
						bool flag2 = current == this.cdtype[current2];
						if (flag2)
						{
							bool flag3 = ModelBase<a3_BagModel>.getInstance().getItemCds()[current] <= 0f;
							if (flag3)
							{
								this.itemicon[current2].transform.FindChild("cd").gameObject.SetActive(false);
								this.itemicon[current2].transform.FindChild("cd_bar").gameObject.SetActive(false);
							}
							else
							{
								this.itemicon[current2].transform.FindChild("cd").gameObject.SetActive(true);
								this.itemicon[current2].transform.FindChild("cd_bar").gameObject.SetActive(true);
								this.itemicon[current2].transform.FindChild("cd").GetComponent<Text>().text = ((int)ModelBase<a3_BagModel>.getInstance().getItemCds()[current]).ToString();
								bool flag4 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(current2);
								if (flag4)
								{
									this.itemicon[current2].transform.FindChild("cd_bar/cd_bar").GetComponent<Image>().fillAmount = ModelBase<a3_BagModel>.getInstance().getItemCds()[current] / ModelBase<a3_BagModel>.getInstance().getItems(false)[current2].confdata.cd_time;
								}
							}
						}
					}
				}
			}
		}

		private void showfenjie_tishi()
		{
			bool flag = ModelBase<a3_BagModel>.getInstance().curi - ModelBase<a3_BagModel>.getInstance().getItems(false).Count <= 5;
			if (flag)
			{
				base.transform.FindChild("item_scroll/equip/tishi").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("item_scroll/equip/tishi").gameObject.SetActive(false);
			}
		}

		private void setAni()
		{
			foreach (int current in this.icon_ani.Keys)
			{
				bool flag = ModelBase<a3_EquipModel>.getInstance().active_eqp.ContainsKey(current);
				if (flag)
				{
					this.icon_ani[current].gameObject.SetActive(true);
				}
				else
				{
					this.icon_ani[current].gameObject.SetActive(false);
				}
			}
		}

		public void SetAni_Color()
		{
			foreach (int current in ModelBase<a3_EquipModel>.getInstance().getEquipsByType().Keys)
			{
				Color color = default(Color);
				switch (ModelBase<a3_EquipModel>.getInstance().getEquipsByType()[current].equipdata.attribute)
				{
				case 1:
					color = new Color(0f, 0.47f, 0f);
					break;
				case 2:
					color = new Color(0.68f, 0.26f, 0.03f);
					break;
				case 3:
					color = new Color(0.76f, 0.86f, 0.33f);
					break;
				case 4:
					color = new Color(0.97f, 0.11f, 0.87f);
					break;
				case 5:
					color = new Color(0.17f, 0.18f, 0.57f);
					break;
				}
				this.icon_ani[current].GetComponent<Image>().color = color;
			}
		}

		public void refreshQHdashi()
		{
			Dictionary<uint, a3_BagItemData> equips = ModelBase<a3_EquipModel>.getInstance().getEquips();
			int num = 0;
			bool flag = true;
			bool flag2 = equips.Count < 10;
			if (flag2)
			{
				num = 0;
			}
			else
			{
				foreach (uint current in equips.Keys)
				{
					bool flag3 = flag;
					if (flag3)
					{
						num = equips[current].equipdata.intensify_lv;
						flag = false;
					}
					bool flag4 = equips[current].equipdata.intensify_lv < num;
					if (flag4)
					{
						num = equips[current].equipdata.intensify_lv;
					}
				}
			}
			this.qhDashi_text.text = num.ToString();
		}

		public void refreshLHlianjie()
		{
			this.LHlianjie_text.text = ModelBase<a3_EquipModel>.getInstance().active_eqp.Count.ToString();
		}

		private void onclose(GameObject go)
		{
			this.Toclose = true;
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_BAG);
		}

		protected void refreshScrollRect()
		{
			int childCount = this.itemListView.transform.childCount;
			bool flag = childCount <= 0;
			if (!flag)
			{
				float y = this.itemListView.GetComponent<GridLayoutGroup>().cellSize.y;
				float y2 = this.itemListView.GetComponent<GridLayoutGroup>().spacing.y;
				int num = (int)Math.Ceiling((double)childCount / 5.0);
				RectTransform component = this.itemListView.GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(0f, (float)num * (y + y2));
			}
		}

		public void initEquipIcon()
		{
			for (int i = 1; i <= 10; i++)
			{
				GameObject gameObject = base.transform.FindChild("ig_bg1/txt" + i).gameObject;
				bool flag = gameObject.transform.childCount > 0;
				if (flag)
				{
					UnityEngine.Object.Destroy(gameObject.transform.GetChild(0).gameObject);
				}
			}
			Dictionary<int, a3_BagItemData> equipsByType = ModelBase<a3_EquipModel>.getInstance().getEquipsByType();
			foreach (int current in equipsByType.Keys)
			{
				a3_BagItemData data = equipsByType[current];
				this.CreateEquipIcon(data);
			}
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

		private void onAttrChange(GameEvent e)
		{
			Text component = base.transform.FindChild("info/value1").GetComponent<Text>();
			component.text = ModelBase<PlayerModel>.getInstance().max_attack.ToString();
			Text component2 = base.transform.FindChild("info/value2").GetComponent<Text>();
			component2.text = ModelBase<PlayerModel>.getInstance().physics_def.ToString();
			Text component3 = base.transform.FindChild("info/value3").GetComponent<Text>();
			component3.text = ModelBase<PlayerModel>.getInstance().max_hp.ToString();
			this.refresh_equip();
		}

		public void refreshMoney()
		{
			Text component = base.transform.FindChild("item_scroll/money").GetComponent<Text>();
			component.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().money);
		}

		public void refreshGold()
		{
			Text component = base.transform.FindChild("item_scroll/stone").GetComponent<Text>();
			component.text = ModelBase<PlayerModel>.getInstance().gold.ToString();
		}

		public void refreshGift()
		{
			Text component = base.transform.FindChild("item_scroll/bindstone").GetComponent<Text>();
			component.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
		}

		public void onLoadItem()
		{
			Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(true);
			int num = 0;
			foreach (a3_BagItemData current in items.Values)
			{
				this.CreateItemIcon(current, num);
				num++;
			}
			this.refreshScrollRect();
		}

		private void onItemChange(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("add");
			if (flag)
			{
				bool flag2 = ModelBase<a3_BagModel>.getInstance().getItems(false).Count > ModelBase<a3_BagModel>.getInstance().curi;
				if (!flag2)
				{
					foreach (Variant current in data["add"]._arr)
					{
						uint key = current["id"];
						bool flag3 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(key);
						if (flag3)
						{
							a3_BagItemData data2 = ModelBase<a3_BagModel>.getInstance().getItems(false)[key];
							this.CreateItemIcon(data2, this.itemicon.Count);
						}
					}
				}
			}
			bool flag4 = data.ContainsKey("modcnts");
			if (flag4)
			{
				foreach (Variant current2 in data["modcnts"]._arr)
				{
					uint key2 = current2["id"];
					bool flag5 = this.itemicon.ContainsKey(key2);
					if (flag5)
					{
						this.itemicon[key2].transform.FindChild("num").GetComponent<Text>().text = current2["cnt"];
						bool flag6 = current2["cnt"] <= 1;
						if (flag6)
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
			bool flag7 = data.ContainsKey("rmvids");
			if (flag7)
			{
				int num = 0;
				using (List<Variant>.Enumerator enumerator3 = data["rmvids"]._arr.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						uint num2 = enumerator3.Current;
						uint key3 = num2;
						bool flag8 = this.itemicon.ContainsKey(key3);
						if (flag8)
						{
							num++;
							GameObject gameObject = this.itemicon[key3].transform.parent.gameObject;
							UnityEngine.Object.Destroy(gameObject);
							this.itemicon.Remove(key3);
							bool flag9 = this.cdtype.ContainsKey(key3);
							if (flag9)
							{
								this.cdtype.Remove(key3);
							}
							GameObject gameObject2 = base.transform.FindChild("item_scroll/scroll_view/icon").gameObject;
							GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
							gameObject3.SetActive(true);
							gameObject3.transform.SetParent(this.item_Parent.transform, false);
							gameObject3.transform.SetSiblingIndex(this.itemicon.Count + num);
						}
					}
				}
			}
		}

		public void ghuaneqp(a3_BagItemData add, a3_BagItemData rem)
		{
			GameObject gameObject = null;
			ModelBase<a3_BagModel>.getInstance().removeItem(rem.id);
			ModelBase<a3_BagModel>.getInstance().addItem(add);
			bool flag = this.itemicon.ContainsKey(rem.id);
			if (flag)
			{
				gameObject = this.itemicon[rem.id].transform.parent.gameObject;
				UnityEngine.Object.Destroy(gameObject.transform.FindChild("icon").gameObject);
				this.itemicon.Remove(rem.id);
			}
			bool flag2 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(add.id);
			if (flag2)
			{
				GameObject icon = IconImageMgr.getInstance().createA3ItemIcon(add, true, add.num, 1f, false);
				icon.transform.SetParent(gameObject.transform, false);
				this.itemicon[add.id] = icon;
				bool flag3 = add.num <= 1;
				if (flag3)
				{
					icon.transform.FindChild("num").gameObject.SetActive(false);
				}
				BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
				baseButton.onClick = delegate(GameObject go)
				{
					this.onItemClick(icon, add.id);
				};
			}
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				this.m_SelfObj.transform.Rotate(Vector3.up, -delta.x);
			}
		}

		public void refresh_equip()
		{
			Dictionary<uint, a3_BagItemData> equips = ModelBase<a3_EquipModel>.getInstance().getEquips();
			Dictionary<uint, a3_BagItemData> unEquips = ModelBase<a3_BagModel>.getInstance().getUnEquips();
			foreach (uint current in unEquips.Keys)
			{
				bool flag = !ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(unEquips[current].confdata);
				if (!flag)
				{
					bool flag2 = !ModelBase<a3_EquipModel>.getInstance().checkCanEquip(unEquips[current].confdata, unEquips[current].equipdata.stage, unEquips[current].equipdata.blessing_lv);
					if (flag2)
					{
						bool flag3 = this.itemicon.ContainsKey(current) && this.itemicon[current] != null;
						if (flag3)
						{
							this.itemicon[current].transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(true);
						}
					}
					else
					{
						bool flag4 = this.itemicon.ContainsKey(current) && this.itemicon[current] != null;
						if (flag4)
						{
							this.itemicon[current].transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(false);
						}
					}
				}
			}
			foreach (uint current2 in equips.Keys)
			{
				bool flag5 = !ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(equips[current2].confdata);
				if (!flag5)
				{
					bool flag6 = !ModelBase<a3_EquipModel>.getInstance().checkCanEquip(equips[current2].confdata, equips[current2].equipdata.stage, equips[current2].equipdata.blessing_lv);
					if (flag6)
					{
						bool flag7 = this.equipicon.ContainsKey(equips[current2].confdata.equip_type) && this.equipicon[equips[current2].confdata.equip_type] != null;
						if (flag7)
						{
							this.equipicon[equips[current2].confdata.equip_type].transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(true);
						}
					}
					else
					{
						bool flag8 = this.equipicon.ContainsKey(equips[current2].confdata.equip_type) && this.equipicon[equips[current2].confdata.equip_type] != null;
						if (flag8)
						{
							this.equipicon[equips[current2].confdata.equip_type].transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(false);
						}
					}
				}
			}
		}

		public void refreshMark(uint id)
		{
			bool ismark = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).ismark;
			if (ismark)
			{
				this.itemicon[id].transform.FindChild("iconborder/ismark").gameObject.SetActive(true);
			}
			else
			{
				this.itemicon[id].transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			}
		}

		public void refreshMarkRuneStones(uint id)
		{
			bool ismark = ModelBase<a3_BagModel>.getInstance().getItems(false)[id].ismark;
			if (ismark)
			{
				this.itemicon[id].transform.FindChild("iconborder/ismark").gameObject.SetActive(true);
			}
			else
			{
				this.itemicon[id].transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			}
		}

		private void onOpenLockRec(GameEvent e)
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
			this.showfenjie_tishi();
		}

		private void onItemSell(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("id");
			if (flag)
			{
				uint num = data["id"];
			}
			string text = data["earn"];
		}

		private void onEquipOn(GameEvent e)
		{
			Variant data = e.data;
			uint key = data["eqpinfo"]["id"];
			a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquips()[key];
			bool flag = a3_BagItemData.confdata.equip_type == 3 || a3_BagItemData.confdata.equip_type == 6;
			if (flag)
			{
				bool flag2 = this.m_SelfObj != null;
				if (flag2)
				{
					UnityEngine.Object.Destroy(this.m_SelfObj);
				}
				this.createAvatar_body();
			}
			else
			{
				this.seteqp_eff();
			}
			int equip_type = a3_BagItemData.confdata.equip_type;
			bool flag3 = this.equipicon.ContainsKey(equip_type);
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.equipicon[equip_type]);
				this.equipicon.Remove(equip_type);
			}
			this.CreateEquipIcon(a3_BagItemData);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
			Dictionary<uint, a3_BagItemData> unEquips = ModelBase<a3_BagModel>.getInstance().getUnEquips();
			foreach (a3_BagItemData current in unEquips.Values)
			{
				bool flag4 = current.confdata.equip_type == a3_BagItemData.confdata.equip_type;
				if (flag4)
				{
					bool flag5 = current.equipdata.combpt > a3_BagItemData.equipdata.combpt;
					if (flag5)
					{
						bool flag6 = this.itemicon.ContainsKey(current.id);
						if (flag6)
						{
							this.itemicon[current.id].transform.FindChild("iconborder/is_upequip").gameObject.SetActive(true);
						}
					}
					else
					{
						bool flag7 = this.itemicon.ContainsKey(current.id);
						if (flag7)
						{
							this.itemicon[current.id].transform.FindChild("iconborder/is_upequip").gameObject.SetActive(false);
						}
					}
				}
			}
			this.setAni();
			this.SetAni_Color();
		}

		public void refOneEquipIcon(uint id)
		{
			a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquips()[id];
			bool flag = a3_BagItemData.confdata.equip_type == 3 || a3_BagItemData.confdata.equip_type == 6;
			if (flag)
			{
				bool flag2 = this.m_SelfObj != null;
				if (flag2)
				{
					UnityEngine.Object.Destroy(this.m_SelfObj);
				}
				this.createAvatar_body();
			}
			int equip_type = a3_BagItemData.confdata.equip_type;
			bool flag3 = this.equipicon.ContainsKey(equip_type);
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.equipicon[equip_type]);
				this.equipicon.Remove(equip_type);
			}
			this.CreateEquipIcon(a3_BagItemData);
		}

		private void onEquipDown(GameEvent e)
		{
			Variant data = e.data;
			int num = data["part_id"];
			this.equipicon[num].transform.parent.GetComponent<Text>().enabled = true;
			UnityEngine.Object.Destroy(this.equipicon[num]);
			this.equipicon.Remove(num);
			bool flag = num == 3 || num == 6;
			if (flag)
			{
				bool flag2 = this.m_SelfObj != null;
				if (flag2)
				{
					UnityEngine.Object.Destroy(this.m_SelfObj);
				}
				this.createAvatar_body();
			}
			else
			{
				this.seteqp_eff();
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
			Dictionary<uint, a3_BagItemData> unEquips = ModelBase<a3_BagModel>.getInstance().getUnEquips();
			foreach (a3_BagItemData current in unEquips.Values)
			{
				bool flag3 = current.confdata.equip_type == num;
				if (flag3)
				{
					bool flag4 = this.itemicon.ContainsKey(current.id);
					if (flag4)
					{
						this.itemicon[current.id].transform.FindChild("iconborder/is_upequip").gameObject.SetActive(true);
					}
				}
				this.refreshMark(current.id);
			}
			this.refresh_equip();
			this.setAni();
		}

		private void CreateItemIcon(a3_BagItemData data, int i)
		{
			GameObject icon = IconImageMgr.getInstance().createA3ItemIcon(data, true, data.num, 1f, false);
			icon.transform.SetParent(this.item_Parent.transform.GetChild(i), false);
			this.itemicon[data.id] = icon;
			bool flag = data.confdata.cd_type > 0;
			if (flag)
			{
				this.cdtype[data.id] = data.confdata.cd_type;
			}
			bool flag2 = data.num <= 1;
			if (flag2)
			{
				icon.transform.FindChild("num").gameObject.SetActive(false);
			}
			BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.onItemClick(icon, data.id);
			};
		}

		private void CreateItemIcon_chushou(a3_BagItemData data, int i)
		{
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data, false, data.num, 1f, false);
			gameObject.transform.SetParent(this.item_Parent_chushou.transform.GetChild(i), false);
			this.itemcon_chushou[data.id] = gameObject;
			bool flag = data.num <= 1;
			if (flag)
			{
				gameObject.transform.FindChild("num").gameObject.SetActive(false);
			}
		}

		private void CreateItemIcon_fenjie(a3_BagItemData data, int i)
		{
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 1f, false);
			gameObject.transform.SetParent(this.item_Parent_fenjie.transform.GetChild(i), false);
			this.itemcon_fenjie[data.id] = gameObject;
			BaseButton baseButton = new BaseButton(gameObject.transform, 1, 1);
			uint id = data.id;
			baseButton.onClick = delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				a3_BagItemData a3_BagItemData = ModelBase<a3_BagModel>.getInstance().getItems(false)[id];
				arrayList.Add(a3_BagItemData);
				arrayList.Add(equip_tip_type.tip_forfenjie);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
			};
		}

		public void createAvatar()
		{
			bool flag = this.m_SelfObj == null;
			if (flag)
			{
				GameObject original = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera");
				this.scene_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
				original = Resources.Load<GameObject>("profession/avatar_ui/show_scene");
				this.scene_Obj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.49f, 15.1f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
				Transform[] componentsInChildren = this.scene_Obj.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					bool flag2 = transform.gameObject.name == "scene_ta";
					if (flag2)
					{
						transform.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
					}
					else
					{
						transform.gameObject.layer = EnumLayer.LM_FX;
					}
				}
			}
		}

		private void createAvatar_body()
		{
			bool flag = SelfRole._inst is P2Warrior;
			if (flag)
			{
				GameObject original = Resources.Load<GameObject>("profession/avatar_ui/warrior_avatar");
				this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
			}
			else
			{
				bool flag2 = SelfRole._inst is P3Mage;
				if (flag2)
				{
					GameObject original = Resources.Load<GameObject>("profession/avatar_ui/mage_avatar");
					this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 167f, 0f, 0f)) as GameObject);
				}
				else
				{
					bool flag3 = SelfRole._inst is P5Assassin;
					if (!flag3)
					{
						return;
					}
					GameObject original = Resources.Load<GameObject>("profession/avatar_ui/assa_avatar");
					this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
				}
			}
			Transform[] componentsInChildren = this.m_SelfObj.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
			}
			Transform transform2 = this.m_SelfObj.transform.FindChild("model");
			bool flag4 = SelfRole._inst is P3Mage;
			if (flag4)
			{
				Transform parent = transform2.FindChild("R_Finger1");
				GameObject original = Resources.Load<GameObject>("profession/avatar_ui/mage_r_finger_fire");
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
				gameObject.transform.SetParent(parent, false);
			}
			this.m_proAvatar = new ProfessionAvatar();
			this.m_proAvatar.Init(SelfRole._inst.m_strAvatarPath, "h_", EnumLayer.LM_ROLE_INVISIBLE, EnumMaterial.EMT_EQUIP_H, transform2, SelfRole._inst.m_strEquipEffPath);
			this.seteqp_eff();
			this.m_proAvatar.set_body(SelfRole._inst.get_bodyid(), SelfRole._inst.get_bodyfxid());
			this.m_proAvatar.set_weaponl(SelfRole._inst.get_weaponl_id(), SelfRole._inst.get_weaponl_fxid());
			this.m_proAvatar.set_weaponr(SelfRole._inst.get_weaponr_id(), SelfRole._inst.get_weaponr_fxid());
			this.m_proAvatar.set_wing(SelfRole._inst.get_wingid(), SelfRole._inst.get_windfxid());
			this.m_proAvatar.set_equip_color(SelfRole._inst.get_equip_colorid());
			bool flag5 = this.m_proAvatar != null;
			if (flag5)
			{
				this.m_proAvatar.FrameMove();
			}
		}

		private void seteqp_eff()
		{
			bool flag = this.m_proAvatar != null;
			if (flag)
			{
				this.m_proAvatar.clear_eff();
				bool flag2 = ModelBase<a3_EquipModel>.getInstance().active_eqp.Count >= 10;
				if (flag2)
				{
					this.m_proAvatar.set_equip_eff(ModelBase<a3_EquipModel>.getInstance().GetEqpIdbyType(3), true);
				}
			}
		}

		public void disposeAvatar()
		{
			this.m_proAvatar = null;
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_SelfObj);
			}
			bool flag2 = this.scene_Obj != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.scene_Obj);
			}
			bool flag3 = this.scene_Camera != null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.scene_Camera);
			}
		}

		private void OnOpenPet(GameObject go)
		{
			A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
			bool flag = !instance.hasPet();
			if (flag)
			{
				flytxt.instance.fly("宠物未开启,请提升角色等级", 0, default(Color), null);
			}
			else
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_BAG);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_YILING, null, false);
			}
		}

		private void onInfo(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_BAG);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_ROLE, null, false);
		}

		private void CreateEquipIcon(a3_BagItemData data)
		{
			bool flag = data.confdata.equip_type != 8 && data.confdata.equip_type != 9 && data.confdata.equip_type != 10;
			GameObject icon;
			if (flag)
			{
				icon = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 1f, false);
			}
			else
			{
				icon = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 1f, false);
			}
			IconImageMgr.getInstance().refreshA3EquipIcon_byType(icon, data, EQUIP_SHOW_TYPE.SHOW_INTENSIFYANDSTAGE);
			GameObject gameObject = base.transform.FindChild("ig_bg1/txt" + data.confdata.equip_type).gameObject;
			icon.transform.SetParent(gameObject.transform, false);
			gameObject.GetComponent<Text>().enabled = false;
			icon.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			icon.name = data.id.ToString();
			this.equipicon[data.confdata.equip_type] = icon;
			icon.transform.GetComponent<Image>().color = new Vector4(0f, 0f, 0f, 0f);
			BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.onEquipClick(icon, data.id);
			};
		}

		private void onItemClick(GameObject go, uint id)
		{
			a3_BagItemData a3_BagItemData = ModelBase<a3_BagModel>.getInstance().getItems(false)[id];
			bool isNew = a3_BagItemData.isNew;
			if (isNew)
			{
				a3_BagItemData.isNew = false;
				ModelBase<a3_BagModel>.getInstance().addItem(a3_BagItemData);
				this.itemicon[id].transform.FindChild("iconborder/is_new").gameObject.SetActive(false);
			}
			bool isEquip = a3_BagItemData.isEquip;
			if (isEquip)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(a3_BagItemData);
				arrayList.Add(equip_tip_type.BagPick_tip);
				this.curitem = this.itemicon[id];
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
			}
			else
			{
				bool isSummon = a3_BagItemData.isSummon;
				if (isSummon)
				{
					ArrayList arrayList2 = new ArrayList();
					arrayList2.Add(a3_BagItemData);
					this.curitem = this.itemicon[id];
					InterfaceMgr.getInstance().open(InterfaceMgr.A3TIPS_SUMMON, arrayList2, false);
				}
				else
				{
					bool isrunestone = a3_BagItemData.isrunestone;
					if (isrunestone)
					{
						ArrayList arrayList3 = new ArrayList();
						arrayList3.Add(a3_BagItemData);
						arrayList3.Add(runestone_tipstype.bag_tip);
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_RUNESTONETIP, arrayList3, false);
					}
					else
					{
						ArrayList arrayList4 = new ArrayList();
						arrayList4.Add(a3_BagItemData);
						arrayList4.Add(equip_tip_type.Bag_tip);
						this.curitem = this.itemicon[id];
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMTIP, arrayList4, false);
						a3_itemtip.closeWin = InterfaceMgr.A3_BAG;
					}
				}
			}
		}

		private void onEquipClick(GameObject go, uint id)
		{
			ArrayList arrayList = new ArrayList();
			a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquips()[id];
			arrayList.Add(a3_BagItemData);
			arrayList.Add(equip_tip_type.Bag_tip);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
		}

		private void onOpenLock(GameObject go, int tag)
		{
			base.transform.FindChild("panel_open").gameObject.SetActive(true);
			this.cur_num = tag - ModelBase<a3_BagModel>.getInstance().curi;
			this.needEvent = false;
			this.open_bar.value = (float)this.cur_num / (float)(150 - ModelBase<a3_BagModel>.getInstance().curi);
			this.checkNumChange();
		}

		private void onCloseOpen(GameObject go)
		{
			base.transform.FindChild("panel_open").gameObject.SetActive(false);
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
				this.cur_num = (int)Math.Floor((double)(rate * (float)(150 - ModelBase<a3_BagModel>.getInstance().curi)));
				bool flag2 = this.cur_num == 0;
				if (flag2)
				{
					this.cur_num = 1;
				}
				this.checkNumChange();
			}
		}

		private void checkNumChange()
		{
			base.transform.FindChild("panel_open/num").GetComponent<Text>().text = this.cur_num.ToString();
			string text = " ";
			int num = this.open_choose_tag;
			if (num != 1)
			{
				if (num == 2)
				{
					text = string.Format("消耗{0}个绑定钻石开启{1}个格子", 5 * this.cur_num, this.cur_num);
				}
			}
			else
			{
				text = string.Format("消耗{0}个钻石开启{1}个格子", 5 * this.cur_num, this.cur_num);
			}
			base.transform.FindChild("panel_open/desc").GetComponent<Text>().text = text;
		}

		private void onOpenLock(GameObject go)
		{
			base.transform.FindChild("panel_open").gameObject.SetActive(false);
			bool flag = this.open_choose_tag == 1;
			if (flag)
			{
				BaseProxy<BagProxy>.getInstance().sendOpenLock(2, this.cur_num, true);
			}
			else
			{
				BaseProxy<BagProxy>.getInstance().sendOpenLock(2, this.cur_num, false);
			}
		}

		public void EquipsSureSell(int quality = 0)
		{
			foreach (uint current in ModelBase<a3_BagModel>.getInstance().getUnEquips().Keys)
			{
				uint tpid = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].tpid;
				bool flag = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid).quality == quality;
				if (flag)
				{
					bool flag2 = !ModelBase<a3_BagModel>.getInstance().isWorked(ModelBase<a3_BagModel>.getInstance().getUnEquips()[current]);
					if (!flag2)
					{
						bool ismark = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(current).ismark;
						if (!ismark)
						{
							bool flag3 = this.dic_BagItem.ContainsKey(current);
							if (flag3)
							{
								this.dic_BagItem.Remove(current);
							}
							this.dic_BagItem[current] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current];
							this.showItemNum(ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].tpid, true);
						}
					}
				}
			}
		}

		private void erfershatt()
		{
		}

		private void SellPutin()
		{
			foreach (uint current in ModelBase<a3_BagModel>.getInstance().getUnEquips().Keys)
			{
				bool flag = ModelBase<a3_BagModel>.getInstance().HasBaoshi(ModelBase<a3_BagModel>.getInstance().getUnEquips()[current]);
				if (!flag)
				{
					bool ismark = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].ismark;
					if (!ismark)
					{
						uint tpid = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].tpid;
						bool flag2 = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid).quality <= 3;
						if (flag2)
						{
							bool flag3 = this.dic_BagItem_shll.ContainsKey(current);
							if (flag3)
							{
								this.dic_BagItem_shll.Remove(current);
							}
							int num = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].num;
							this.dic_BagItem_shll[current] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current];
							this.ShowMoneyCount(ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].tpid, num, true);
						}
					}
				}
			}
			Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
			foreach (uint current2 in items.Keys)
			{
				uint tpid2 = items[current2].tpid;
				bool flag4 = items[current2].confdata.use_type == 2 || items[current2].confdata.use_type == 3;
				if (flag4)
				{
					SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + tpid2);
					bool flag5 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)sXML.getInt("use_limit"));
					if (flag5)
					{
						bool flag6 = ModelBase<PlayerModel>.getInstance().up_lvl == 1u;
						if (!flag6)
						{
							bool flag7 = ModelBase<PlayerModel>.getInstance().up_lvl == 3u;
							if (flag7)
							{
								bool flag8 = tpid2 == 1531u;
								if (flag8)
								{
									continue;
								}
								bool flag9 = tpid2 == 1532u;
								if (flag9)
								{
									continue;
								}
							}
							bool flag10 = this.dic_BagItem_shll.ContainsKey(current2);
							if (flag10)
							{
								this.dic_BagItem_shll.Remove(current2);
							}
							int num2 = items[current2].num;
							this.dic_BagItem_shll[current2] = items[current2];
							this.ShowMoneyCount(items[current2].tpid, num2, true);
						}
					}
				}
			}
		}

		public void sellPutout(uint tpid, int num)
		{
		}

		public void OnLoadTitm_chushou()
		{
			this.itemcon_chushou.Clear();
			int num = 0;
			bool flag = this.dic_BagItem_shll.Count > 0;
			if (flag)
			{
				int num2 = 0;
				foreach (a3_BagItemData current in this.dic_BagItem_shll.Values)
				{
					this.CreateItemIcon_chushou(current, num2);
					num2++;
				}
				num = this.dic_BagItem_shll.Count / 6;
				bool flag2 = this.dic_BagItem_shll.Count % 6 > 0;
				if (flag2)
				{
					num++;
				}
			}
			RectTransform component = this.item_Parent_chushou.gameObject.GetComponent<RectTransform>();
			float y = this.item_Parent_chushou.cellSize.y;
			Vector2 sizeDelta = new Vector2(component.sizeDelta.x, y * (float)num);
			component.sizeDelta = sizeDelta;
		}

		private void setfenjieCon()
		{
			int num = 0;
			bool flag = this.dic_BagItem.Count > 0;
			if (flag)
			{
				num = this.itemcon_fenjie.Count / this.item_Parent_fenjie.constraintCount;
				bool flag2 = this.itemcon_fenjie.Count % this.item_Parent_fenjie.constraintCount > 0;
				if (flag2)
				{
					num++;
				}
			}
			RectTransform component = this.item_Parent_fenjie.gameObject.GetComponent<RectTransform>();
			float y = this.item_Parent_fenjie.cellSize.y;
			float y2 = this.item_Parent_fenjie.spacing.y;
			Vector2 sizeDelta = new Vector2(component.sizeDelta.x, (y + y2) * (float)num);
			component.sizeDelta = sizeDelta;
		}

		private void OnLoadItem_fenjie()
		{
			bool flag = this.dic_BagItem.Count > 0;
			if (flag)
			{
				foreach (uint current in this.dic_BagItem.Keys)
				{
					bool flag2 = this.itemcon_fenjie.ContainsKey(current);
					if (!flag2)
					{
						this.CreateItemIcon_fenjie(this.dic_BagItem[current], this.conIndex);
						this.conIndex++;
					}
				}
			}
			this.setfenjieCon();
		}

		public void outItemCon_fenjie(int type = -1, uint id = 0u)
		{
			GameObject gameObject = this.item_Parent_fenjie.transform.parent.FindChild("icon").gameObject;
			bool flag = type != -1;
			if (flag)
			{
				foreach (uint current in this.dic_BagItem.Keys)
				{
					bool flag2 = this.dic_BagItem[current].confdata.quality == type;
					if (flag2)
					{
						this.conIndex--;
						UnityEngine.Object.Destroy(this.itemcon_fenjie[current].transform.parent.gameObject);
						this.itemcon_fenjie.Remove(current);
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject).gameObject;
						gameObject2.transform.SetParent(this.item_Parent_fenjie.transform, false);
						gameObject2.SetActive(true);
						gameObject2.transform.SetAsLastSibling();
					}
				}
			}
			else
			{
				bool flag3 = id > 0u;
				if (flag3)
				{
					UnityEngine.Object.Destroy(this.itemcon_fenjie[id].transform.parent.gameObject);
					this.itemcon_fenjie.Remove(id);
					this.dic_BagItem.Remove(id);
					this.showItemNum(ModelBase<a3_BagModel>.getInstance().getUnEquips()[id].tpid, false);
					GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject).gameObject;
					gameObject3.transform.SetParent(this.item_Parent_fenjie.transform, false);
					gameObject3.SetActive(true);
					gameObject3.transform.SetAsLastSibling();
					this.conIndex--;
				}
			}
			this.setfenjieCon();
		}

		private void ShowMoneyCount(uint tpid, int num, bool add)
		{
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + tpid);
			if (add)
			{
				this.GetMoneyNum += sXML.getInt("value") * num;
			}
			else
			{
				this.GetMoneyNum -= sXML.getInt("value") * num;
			}
			this.Money.text = this.GetMoneyNum.ToString();
		}

		public void refresh_Sell()
		{
			base.transform.FindChild("piliang_chushou").gameObject.SetActive(false);
			this.dic_BagItem_shll.Clear();
			this.Money.text = string.Concat(0);
			this.GetMoneyNum = 0;
		}

		private void SellItem(GameObject go)
		{
			this.dic_Itemlist.Clear();
			foreach (uint current in this.dic_BagItem_shll.Keys)
			{
				Variant variant = new Variant();
				variant["id"] = current;
				variant["num"] = this.dic_BagItem_shll[current].num;
				this.dic_Itemlist.Add(variant);
			}
			BaseProxy<BagProxy>.getInstance().sendSellItems(this.dic_Itemlist);
		}

		public void EquipsNoSell(int quality = 0)
		{
			List<uint> list = new List<uint>();
			foreach (uint current in this.dic_BagItem.Keys)
			{
				bool flag = this.dic_BagItem[current].confdata.quality == quality;
				if (flag)
				{
					list.Add(current);
					this.showItemNum(ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].tpid, false);
				}
			}
			foreach (uint current2 in list)
			{
				this.dic_BagItem.Remove(current2);
			}
		}

		private void showItemNum(uint tpid, bool add)
		{
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + tpid);
			List<SXML> nodeList = sXML.GetNodeList("decompose", "");
			foreach (SXML current in nodeList)
			{
				switch (current.getInt("item"))
				{
				case 1540:
					if (add)
					{
						this.mojing_num += current.getInt("num");
					}
					else
					{
						this.mojing_num -= current.getInt("num");
					}
					this.mojing.text = this.mojing_num.ToString();
					break;
				case 1541:
					if (add)
					{
						this.shengguanghuiji_num += current.getInt("num");
					}
					else
					{
						this.shengguanghuiji_num -= current.getInt("num");
					}
					this.shengguanghuiji.text = this.shengguanghuiji_num.ToString();
					break;
				case 1542:
					if (add)
					{
						this.mifageli_num += current.getInt("num");
					}
					else
					{
						this.mifageli_num -= current.getInt("num");
					}
					this.mifageli.text = this.mifageli_num.ToString();
					break;
				}
			}
		}

		private void Sendproxy(GameObject go)
		{
			this.dic_leftAllid.Clear();
			foreach (uint current in this.dic_BagItem.Keys)
			{
				this.dic_leftAllid.Add(current);
			}
			BaseProxy<EquipProxy>.getInstance().sendsell(this.dic_leftAllid);
			this.onfenjieclose(null);
			base.transform.FindChild("piliang_fenjie").gameObject.SetActive(false);
		}

		public void refresh()
		{
			bool flag = this.mojing_num != 0 && this.shengguanghuiji_num == 0 && this.mifageli_num == 0;
			if (flag)
			{
				flytxt.instance.fly("获得" + this.mojing_num + "个魔晶", 0, default(Color), null);
			}
			bool flag2 = this.mojing_num == 0 && this.shengguanghuiji_num != 0 && this.mifageli_num == 0;
			if (flag2)
			{
				flytxt.instance.fly("获得" + this.shengguanghuiji_num + "个神光徽记", 0, default(Color), null);
			}
			bool flag3 = this.mojing_num == 0 && this.shengguanghuiji_num == 0 && this.mifageli_num != 0;
			if (flag3)
			{
				flytxt.instance.fly("获得" + this.mifageli_num + "个秘法颗粒", 0, default(Color), null);
			}
			bool flag4 = this.mojing_num != 0 && this.shengguanghuiji_num != 0 && this.mifageli_num == 0;
			if (flag4)
			{
				flytxt.instance.fly(string.Concat(new object[]
				{
					"获得",
					this.mojing_num,
					"个魔晶,",
					this.shengguanghuiji_num,
					"个神光徽记"
				}), 0, default(Color), null);
			}
			bool flag5 = this.mojing_num != 0 && this.shengguanghuiji_num == 0 && this.mifageli_num != 0;
			if (flag5)
			{
				flytxt.instance.fly(string.Concat(new object[]
				{
					"获得",
					this.mojing_num,
					"个魔晶,",
					this.mifageli_num,
					"个秘法颗粒"
				}), 0, default(Color), null);
			}
			bool flag6 = this.mojing_num == 0 && this.shengguanghuiji_num != 0 && this.mifageli_num != 0;
			if (flag6)
			{
				flytxt.instance.fly(string.Concat(new object[]
				{
					"获得",
					this.shengguanghuiji_num,
					"个神光徽记,",
					this.mifageli_num,
					"个秘法颗粒"
				}), 0, default(Color), null);
			}
			bool flag7 = this.mojing_num != 0 && this.shengguanghuiji_num != 0 && this.mifageli_num != 0;
			if (flag7)
			{
				flytxt.instance.fly(string.Concat(new object[]
				{
					"获得",
					this.mojing_num,
					"个魔晶",
					this.shengguanghuiji_num,
					"个神光徽记",
					this.mifageli_num,
					"个秘法颗粒"
				}), 0, default(Color), null);
			}
			this.dic_BagItem.Clear();
			this.clearCon_fenjie();
			this.conIndex = 0;
			this.mojing_num = 0;
			this.shengguanghuiji_num = 0;
			this.mifageli_num = 0;
			this.mojing.text = string.Concat(0);
			this.shengguanghuiji.text = string.Concat(0);
			this.mifageli.text = string.Concat(0);
			this.purple.isOn = false;
			this.blue.isOn = false;
			this.green.isOn = false;
			this.white.isOn = false;
			this.orange.isOn = false;
		}

		private void onEquipSell(GameObject go)
		{
			this.white.isOn = false;
			this.green.isOn = false;
			this.blue.isOn = false;
			this.purple.isOn = false;
			this.orange.isOn = false;
			base.transform.FindChild("piliang_fenjie").gameObject.SetActive(true);
			this.mojing_num = 0;
			this.shengguanghuiji_num = 0;
			this.mifageli_num = 0;
			this.clearCon_fenjie();
			this.OnLoadItem_fenjie();
			base.transform.FindChild("item_scroll/equip/tishi").gameObject.SetActive(false);
		}

		private void onfenjieclose(GameObject go)
		{
			this.dic_BagItem.Clear();
			this.clearCon_fenjie();
			this.conIndex = 0;
			this.mojing.text = string.Concat(0);
			this.shengguanghuiji.text = string.Concat(0);
			this.mifageli.text = string.Concat(0);
			base.transform.FindChild("piliang_fenjie").gameObject.SetActive(false);
		}

		private void onchoushouclose(GameObject go)
		{
			this.refresh_Sell();
		}

		private void onZhengLi(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_BAG);
			this.isbagToCK = true;
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_WAREHOUSE, null, false);
		}

		private void clearCon()
		{
			bool flag = this.itemcon_chushou.Count > 0;
			if (flag)
			{
				foreach (GameObject current in this.itemcon_chushou.Values)
				{
					UnityEngine.Object.Destroy(current);
				}
			}
		}

		private void clearCon_fenjie()
		{
			bool flag = this.itemcon_fenjie.Count > 0;
			if (flag)
			{
				foreach (GameObject current in this.itemcon_fenjie.Values)
				{
					UnityEngine.Object.Destroy(current);
				}
			}
			this.itemcon_fenjie.Clear();
		}

		private void onChushou(GameObject go)
		{
			base.transform.FindChild("piliang_chushou").gameObject.SetActive(true);
			this.SellPutin();
			this.clearCon();
			this.OnLoadTitm_chushou();
		}

		private void onOpenDashi(GameObject go)
		{
			base.transform.FindChild("QH_dashi").gameObject.SetActive(true);
			this.set_QH_att();
		}

		private void onLHlianjie(GameObject go)
		{
			base.transform.FindChild("LH_lianjie").gameObject.SetActive(true);
			this.set_LH_att();
		}

		private void openGetJewelry(GameObject go)
		{
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.GLOBA_BOSS, false);
			if (flag)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(InterfaceMgr.A3_BAG);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_GETJEWELRYWAY, arrayList, false);
			}
			else
			{
				flytxt.instance.fly("等级不够，首饰获得需要1转2级开启", 0, default(Color), null);
			}
		}

		private void set_QH_att()
		{
			Dictionary<uint, a3_BagItemData> equips = ModelBase<a3_EquipModel>.getInstance().getEquips();
			int num = 0;
			bool flag = true;
			bool flag2 = equips.Count < 10;
			if (flag2)
			{
				num = 0;
			}
			else
			{
				foreach (uint current in equips.Keys)
				{
					bool flag3 = flag;
					if (flag3)
					{
						num = equips[current].equipdata.intensify_lv;
						flag = false;
					}
					bool flag4 = equips[current].equipdata.intensify_lv < num;
					if (flag4)
					{
						num = equips[current].equipdata.intensify_lv;
					}
				}
			}
			base.transform.FindChild("QH_dashi/lvl").GetComponent<Text>().text = num.ToString();
			int num2 = 0;
			SXML sXML = XMLMgr.instance.GetSXML("intensifymaster.level", "lvl==" + ModelBase<PlayerModel>.getInstance().up_lvl);
			List<SXML> nodeList = sXML.GetNodeList("intensify", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				bool flag5 = num >= nodeList[i].getInt("qh") && nodeList.Count <= i + 1;
				if (flag5)
				{
					num2 = nodeList[i].getInt("qh");
					break;
				}
				bool flag6 = num < nodeList[i].getInt("qh") && i == 0;
				if (flag6)
				{
					num2 = 0;
					break;
				}
				bool flag7 = num >= nodeList[i].getInt("qh") && nodeList.Count > i + 1 && num < nodeList[i + 1].getInt("qh");
				if (flag7)
				{
					num2 = nodeList[i].getInt("qh");
					break;
				}
			}
			SXML node = sXML.GetNode("intensify", "qh==" + num2);
			bool flag8 = node == null;
			if (flag8)
			{
				base.transform.FindChild("QH_dashi/attVeiw").gameObject.SetActive(false);
				base.transform.FindChild("QH_dashi/tishi").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("QH_dashi/attVeiw").gameObject.SetActive(true);
				base.transform.FindChild("QH_dashi/tishi").gameObject.SetActive(false);
				List<SXML> nodeList2 = node.GetNodeList("att", "");
				GameObject gameObject = base.transform.FindChild("QH_dashi/attVeiw/info_item").gameObject;
				RectTransform component = base.transform.FindChild("QH_dashi/attVeiw/con").GetComponent<RectTransform>();
				for (int j = 0; j < component.childCount; j++)
				{
					UnityEngine.Object.Destroy(component.GetChild(j).gameObject);
				}
				foreach (SXML current2 in nodeList2)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					Text component2 = gameObject2.transform.FindChild("Text").GetComponent<Text>();
					component2.text = "+" + current2.getInt("value") + Globle.getAttrNameById(current2.getInt("type"));
					gameObject2.SetActive(true);
					gameObject2.transform.SetParent(component, false);
				}
			}
		}

		private void set_LH_att()
		{
			int count = ModelBase<a3_EquipModel>.getInstance().active_eqp.Count;
			base.transform.FindChild("LH_lianjie/lvl").GetComponent<Text>().text = count.ToString();
			SXML sXML = XMLMgr.instance.GetSXML("activate_fun.activate_num", "");
			SXML node = sXML.GetNode("num", "cout==" + count);
			bool flag = node == null;
			if (flag)
			{
				base.transform.FindChild("LH_lianjie/attVeiw").gameObject.SetActive(false);
				base.transform.FindChild("LH_lianjie/tishi").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("LH_lianjie/attVeiw").gameObject.SetActive(true);
				base.transform.FindChild("LH_lianjie/tishi").gameObject.SetActive(false);
				List<SXML> nodeList = node.GetNodeList("type", "");
				GameObject gameObject = base.transform.FindChild("LH_lianjie/attVeiw/info_item").gameObject;
				RectTransform component = base.transform.FindChild("LH_lianjie/attVeiw/con").GetComponent<RectTransform>();
				for (int i = 0; i < component.childCount; i++)
				{
					UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
				}
				foreach (SXML current in nodeList)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					Text component2 = gameObject2.transform.FindChild("Text").GetComponent<Text>();
					component2.text = Globle.getAttrNameById(current.getInt("att_type")) + "+" + current.getInt("att_value");
					gameObject2.SetActive(true);
					gameObject2.transform.SetParent(component, false);
				}
			}
		}
	}
}
