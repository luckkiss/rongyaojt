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
	internal class A3_Smithy : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly A3_Smithy.<>c <>9 = new A3_Smithy.<>c();

			public static Action<GameObject> <>9__61_1;

			public static EventTriggerListener.VoidDelegate <>9__61_8;

			public static Action<GameObject> <>9__61_9;

			public static Action<cd> <>9__75_0;

			internal void <init>b__61_1(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SMITHY);
			}

			internal void <init>b__61_8(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SMITHY);
			}

			internal void <init>b__61_9(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_WAREHOUSE, null, false);
			}

			internal void <Make>b__75_0(cd item)
			{
				int num = (int)(cd.secCD - cd.lastCD) / 100;
				item.txt.text = ((float)num / 10f).ToString();
			}
		}

		public readonly uint RANDOM_ITEM_ID = 99u;

		public readonly int USE_SCROLL = 15;

		private List<KeyValuePair<int, string>> listMainItem;

		private List<int> listCarr;

		private int currentSelectedCarr;

		public List<int> listPartIdx;

		private int currentSelectedPart;

		private int currentExpandCarr;

		private int targetNum = 1;

		private uint targetId;

		private bool isAccessible;

		private static A3_Smithy instance;

		private Dictionary<int, ExpandState> dicExpandState;

		private Text moneyNeed;

		private InputField textNum;

		private Dictionary<uint, GameObject> itemicon = new Dictionary<uint, GameObject>();

		private Dictionary<int, GameObject> dicMainObj;

		private Dictionary<int, SubHeadNode> dicSubHead;

		private List<MatInfo> targetMatList;

		private List<GameObject> matObjList = new List<GameObject>();

		private Slider sliderExp;

		private Transform choosePart;

		private Transform checkPart;

		private Transform checkRandomPart;

		private Transform mainPanel;

		private GameObject targetIcon;

		private GameObject randomItemIcon;

		private GameObject relearnTip;

		private Transform targetEqp;

		private Transform targetMatListObj;

		private Transform matIcon;

		private Transform mainItem;

		private Transform subItem;

		private Transform subHead;

		public Transform transEqpList;

		private Transform rlrnArmor;

		private Transform rlrnWeapon;

		private Transform rlrnJewelry;

		private Toggle tglRlrnArmor;

		private Toggle tglRlrnWeapon;

		private Toggle tglRlrnJewelry;

		private GridLayoutGroup itemParent;

		private Dropdown dropdownCarr;

		private Dropdown dropdownPart;

		private Text txtLv;

		private GameObject fxSmithyLvUp;

		private Dictionary<uint, a3_BagItemData> dic_BagItem_shll = new Dictionary<uint, a3_BagItemData>();

		private Dictionary<uint, GameObject> itemcon_chushou = new Dictionary<uint, GameObject>();

		private GridLayoutGroup item_Parent_chushou;

		private int GetMoneyNum = 0;

		private Text Money;

		private List<Variant> dic_Itemlist = new List<Variant>();

		private KeyValuePair<int, GameObject> curEqpInfo;

		private a3_BagItemData curBodyEqpInfo;

		private Toggle white;

		private Toggle green;

		private Toggle blue;

		private Toggle purple;

		private Toggle orange;

		private Dictionary<uint, a3_BagItemData> dic_BagItem = new Dictionary<uint, a3_BagItemData>();

		public int mojing_num;

		public int shengguanghuiji_num;

		public int mifageli_num;

		private int conIndex = 0;

		private Text mojing;

		private Text shengguanghuiji;

		private Text mifageli;

		private Dictionary<uint, GameObject> itemcon_fenjie = new Dictionary<uint, GameObject>();

		private GridLayoutGroup item_Parent_fenjie;

		private List<uint> dic_leftAllid = new List<uint>();

		public int CurrentSelectedCarr
		{
			get
			{
				return this.currentSelectedCarr;
			}
			protected set
			{
				this.currentSelectedCarr = ((value < 0) ? 0 : value);
				this.listMainItem.Clear();
				switch (this.currentSelectedCarr)
				{
				case 1:
					this.listMainItem.Add(new KeyValuePair<int, string>(2, "战士"));
					this.listMainItem.Add(new KeyValuePair<int, string>(15, "卷轴"));
					break;
				case 2:
					this.listMainItem.Add(new KeyValuePair<int, string>(3, "法师"));
					this.listMainItem.Add(new KeyValuePair<int, string>(15, "卷轴"));
					break;
				case 3:
					this.listMainItem.Add(new KeyValuePair<int, string>(5, "刺客"));
					this.listMainItem.Add(new KeyValuePair<int, string>(15, "卷轴"));
					break;
				case 4:
					this.listMainItem.Add(new KeyValuePair<int, string>(15, "卷轴"));
					break;
				default:
					this.listMainItem.Add(new KeyValuePair<int, string>(2, "战士"));
					this.listMainItem.Add(new KeyValuePair<int, string>(3, "法师"));
					this.listMainItem.Add(new KeyValuePair<int, string>(5, "刺客"));
					this.listMainItem.Add(new KeyValuePair<int, string>(15, "卷轴"));
					break;
				}
			}
		}

		private int CurrentSelectedPart
		{
			get
			{
				bool flag = this.currentSelectedPart != 0;
				int result;
				if (flag)
				{
					result = this.listPartIdx[this.currentSelectedPart];
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				this.currentSelectedPart = value;
			}
		}

		public static A3_Smithy Instance
		{
			get
			{
				A3_Smithy expr_06 = A3_Smithy.instance;
				bool flag = expr_06 != null && expr_06.isAccessible;
				A3_Smithy result;
				if (flag)
				{
					result = A3_Smithy.instance;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					bool flag2 = A3_Smithy.instance != null;
					if (flag2)
					{
						A3_Smithy.instance.isAccessible = false;
					}
				}
				else
				{
					A3_Smithy.instance = value;
					A3_Smithy.instance.isAccessible = true;
				}
			}
		}

		private bool IsMatEnough
		{
			get
			{
				return this.CheckMat();
			}
		}

		private bool IsRoomEnough
		{
			get
			{
				return ModelBase<a3_BagModel>.getInstance().getItems(false).Count + this.targetNum <= ModelBase<a3_BagModel>.getInstance().curi;
			}
		}

		private bool IsMoneyEnough
		{
			get
			{
				return (ulong)ModelBase<PlayerModel>.getInstance().money > (ulong)((long)((this.targetId == this.RANDOM_ITEM_ID) ? ModelBase<A3_SmithyModel>.getInstance().GetMoneyCostByScroll(this.targetNum) : ModelBase<A3_SmithyModel>.getInstance().GetMoneyCostById(this.targetId, this.targetNum)));
			}
		}

		public override void init()
		{
			base.init();
			A3_Smithy.instance = this;
			this.listMainItem = new List<KeyValuePair<int, string>>();
			this.listCarr = new List<int>
			{
				2,
				3,
				5
			};
			this.choosePart = base.transform.FindChild("choosePart");
			this.checkPart = base.transform.FindChild("check");
			this.checkRandomPart = base.transform.FindChild("checkSuggest");
			this.mainPanel = base.transform.FindChild("Main");
			this.relearnTip = base.transform.FindChild("RelearnTip").gameObject;
			this.sliderExp = base.transform.FindChild("Main/left_panel/wk/exp/Slider").GetComponent<Slider>();
			this.txtLv = base.transform.FindChild("Main/left_panel/wk/exp/title_lv/lv").GetComponent<Text>();
			new BaseButton(base.transform.FindChild("Main/left_panel/wk/btn_do"), 1, 1).onClick = delegate(GameObject go)
			{
				this.Make();
			};
			BaseButton arg_144_0 = new BaseButton(base.transform.FindChild("Main/left_panel/wk/close"), 1, 1);
			Action<GameObject> arg_144_1;
			if ((arg_144_1 = A3_Smithy.<>c.<>9__61_1) == null)
			{
				arg_144_1 = (A3_Smithy.<>c.<>9__61_1 = new Action<GameObject>(A3_Smithy.<>c.<>9.<init>b__61_1));
			}
			arg_144_0.onClick = arg_144_1;
			this.targetMatListObj = base.transform.FindChild("Main/left_panel/wk/mat/mat_list");
			this.targetEqp = base.transform.FindChild("Main/left_panel/wk/mat/target_eqp/icon");
			this.itemParent = base.transform.FindChild("Main/right_bag/item_scroll/scroll_view/contain").GetComponent<GridLayoutGroup>();
			this.textNum = base.transform.FindChild("Main/left_panel/wk/num/InputField").GetComponent<InputField>();
			this.textNum.onEndEdit.AddListener(delegate(string text)
			{
				bool flag = int.TryParse(text, out this.targetNum);
				if (flag)
				{
					bool flag2 = this.targetNum <= 0;
					if (flag2)
					{
						this.targetNum = 1;
						this.textNum.text = "1";
					}
					else
					{
						bool flag3 = this.targetNum > 100;
						if (flag3)
						{
							this.targetNum = 100;
							this.textNum.text = "100";
						}
					}
				}
				else
				{
					this.targetNum = 1;
					this.textNum.text = "1";
				}
				this.RefreshNumText();
			});
			this.moneyNeed = base.transform.FindChild("Main/left_panel/wk/btn_do/cost").GetComponent<Text>();
			new BaseButton(base.transform.FindChild("Main/left_panel/wk/num/num_sub"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = this.targetNum <= 1;
				if (flag)
				{
					this.textNum.text = "1";
				}
				else
				{
					InputField arg_42_0 = this.textNum;
					int num = this.targetNum - 1;
					this.targetNum = num;
					arg_42_0.text = num.ToString();
					this.RefreshNumText();
				}
			};
			new BaseButton(base.transform.FindChild("Main/left_panel/wk/num/num_add"), 1, 1).onClick = delegate(GameObject go)
			{
				InputField arg_37_0 = this.textNum;
				int num = this.targetNum + 1;
				this.targetNum = num;
				arg_37_0.text = ((num > 10) ? (this.targetNum = 10) : this.targetNum).ToString();
				this.RefreshNumText();
			};
			new BaseButton(base.transform.FindChild("Main/left_panel/wk/help"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("HelpTip").gameObject.SetActive(true);
			};
			new BaseButton(base.transform.FindChild("Main/left_panel/wk/relearn"), 1, 1).onClick = new Action<GameObject>(this.ShowRelearnWin);
			new BaseButton(base.transform.FindChild("RelearnTip/btn_useMoney"), 1, 1).onClick = new Action<GameObject>(this.OnRelearnByMoney);
			new BaseButton(base.transform.FindChild("RelearnTip/btn_useDiamond"), 1, 1).onClick = new Action<GameObject>(this.OnRelearnByDiamond);
			new BaseButton(base.transform.FindChild("HelpTip/closeArea"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("HelpTip").gameObject.SetActive(false);
			};
			new BaseButton(base.transform.FindChild("RelearnTip/closeArea"), 1, 1).onClick = delegate(GameObject go)
			{
				this.relearnTip.SetActive(false);
			};
			this.matIcon = base.transform.FindChild("template/mat_icon");
			this.mainItem = base.transform.FindChild("template/main");
			this.subItem = base.transform.FindChild("template/sub");
			this.subHead = base.transform.FindChild("template/subHead");
			this.randomItemIcon = base.transform.FindChild("template/randomItem").gameObject;
			(this.fxSmithyLvUp = base.transform.FindChild("Main/left_panel/wk/exp/lv_up_fx").gameObject).SetActive(false);
			this.transEqpList = base.transform.FindChild("Main/left_panel/wk/panelbody/scroll");
			this.dicMainObj = new Dictionary<int, GameObject>();
			this.dicExpandState = new Dictionary<int, ExpandState>();
			this.dicSubHead = new Dictionary<int, SubHeadNode>();
			this.dropdownCarr = base.transform.FindChild("Main/left_panel/wk/filter/dropdown_carr").GetComponent<Dropdown>();
			this.dropdownCarr.ClearOptions();
			List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
			list.Add(new Dropdown.OptionData("全部"));
			list.Add(new Dropdown.OptionData("战士"));
			list.Add(new Dropdown.OptionData("法师"));
			list.Add(new Dropdown.OptionData("刺客"));
			this.dropdownCarr.AddOptions(list);
			this.dropdownCarr.onValueChanged.AddListener(new UnityAction<int>(this.OnSelectCarr));
			this.dropdownPart = base.transform.FindChild("Main/left_panel/wk/filter/dropdown_part").GetComponent<Dropdown>();
			this.listPartIdx = new List<int>();
			this.dropdownPart.ClearOptions();
			this.dropdownPart.onValueChanged.AddListener(new UnityAction<int>(this.OnSelectPart));
			EventTriggerListener arg_511_0 = base.getEventTrigerByPath("ig_bg_bg");
			EventTriggerListener.VoidDelegate arg_511_1;
			if ((arg_511_1 = A3_Smithy.<>c.<>9__61_8) == null)
			{
				arg_511_1 = (A3_Smithy.<>c.<>9__61_8 = new EventTriggerListener.VoidDelegate(A3_Smithy.<>c.<>9.<init>b__61_8));
			}
			arg_511_0.onClick = arg_511_1;
			this.rlrnArmor = base.transform.FindChild("RelearnTip/relearn_select/learn_armor");
			this.rlrnJewelry = base.transform.FindChild("RelearnTip/relearn_select/learn_jew");
			this.rlrnWeapon = base.transform.FindChild("RelearnTip/relearn_select/learn_weapon");
			this.tglRlrnArmor = this.rlrnArmor.GetComponent<Toggle>();
			this.tglRlrnWeapon = this.rlrnWeapon.GetComponent<Toggle>();
			this.tglRlrnJewelry = this.rlrnJewelry.GetComponent<Toggle>();
			BaseButton arg_5C1_0 = new BaseButton(base.transform.FindChild("Main/right_bag/item_scroll/bag"), 1, 1);
			Action<GameObject> arg_5C1_1;
			if ((arg_5C1_1 = A3_Smithy.<>c.<>9__61_9) == null)
			{
				arg_5C1_1 = (A3_Smithy.<>c.<>9__61_9 = new Action<GameObject>(A3_Smithy.<>c.<>9.<init>b__61_9));
			}
			arg_5C1_0.onClick = arg_5C1_1;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("Main/right_bag/piliang_fenjie/info_bg/go"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.Sendproxy);
			this.item_Parent_fenjie = base.transform.FindChild("Main/right_bag/piliang_fenjie/scroll_view/contain").GetComponent<GridLayoutGroup>();
			new BaseButton(base.transform.FindChild("Main/right_bag/item_scroll/chushou"), 1, 1).onClick = new Action<GameObject>(this.onChushou);
			this.Money = base.getComponentByPath<Text>("Main/right_bag/piliang_chushou/money");
			this.item_Parent_chushou = base.transform.FindChild("Main/right_bag/piliang_chushou/info_bg/scroll_view/contain").GetComponent<GridLayoutGroup>();
			new BaseButton(base.transform.FindChild("Main/right_bag/piliang_chushou/close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.refresh_Sell();
			};
			new BaseButton(base.transform.FindChild("Main/right_bag/piliang_chushou/info_bg/go"), 1, 1).onClick = new Action<GameObject>(this.SellItem);
			this.mojing = base.getComponentByPath<Text>("Main/right_bag/piliang_fenjie/info_bg/mojing/num");
			this.shengguanghuiji = base.getComponentByPath<Text>("Main/right_bag/piliang_fenjie/info_bg/shenguang/num");
			this.mifageli = base.getComponentByPath<Text>("Main/right_bag/piliang_fenjie/info_bg/mifa/num");
			this.white = base.getComponentByPath<Toggle>("Main/right_bag/piliang_fenjie/info_bg/Toggle_all/Toggle_white");
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
			this.green = base.getComponentByPath<Toggle>("Main/right_bag/piliang_fenjie/info_bg/Toggle_all/Toggle_green");
			this.green.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					bool flag = !this.white.isOn;
					if (flag)
					{
						this.white.isOn = true;
					}
					this.EquipsSureSell(2);
					this.OnLoadItem_fenjie();
				}
				else
				{
					this.outItemCon_fenjie(2, 0u);
					this.EquipsNoSell(2);
				}
			});
			this.blue = base.getComponentByPath<Toggle>("Main/right_bag/piliang_fenjie/info_bg/Toggle_all/Toggle_blue");
			this.blue.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					bool flag = !this.green.isOn;
					if (flag)
					{
						this.green.isOn = true;
					}
					this.EquipsSureSell(3);
					this.OnLoadItem_fenjie();
				}
				else
				{
					this.outItemCon_fenjie(3, 0u);
					this.EquipsNoSell(3);
				}
			});
			this.purple = base.getComponentByPath<Toggle>("Main/right_bag/piliang_fenjie/info_bg/Toggle_all/Toggle_puple");
			this.purple.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					bool flag = !this.blue.isOn;
					if (flag)
					{
						this.blue.isOn = true;
					}
					this.EquipsSureSell(4);
					this.OnLoadItem_fenjie();
				}
				else
				{
					this.outItemCon_fenjie(4, 0u);
					this.EquipsNoSell(4);
				}
			});
			this.orange = base.getComponentByPath<Toggle>("Main/right_bag/piliang_fenjie/info_bg/Toggle_all/Toggle_orange");
			this.orange.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					bool flag = !this.purple.isOn;
					if (flag)
					{
						this.purple.isOn = true;
					}
					this.EquipsSureSell(5);
					this.OnLoadItem_fenjie();
				}
				else
				{
					this.outItemCon_fenjie(5, 0u);
					this.EquipsNoSell(5);
				}
			});
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("Main/right_bag/item_scroll/equip"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onEquipSell);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("Main/right_bag/piliang_fenjie/close"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onfenjieclose);
		}

		private void ShowRelearnWin(GameObject go)
		{
			this.relearnTip.SetActive(true);
			base.transform.FindChild("RelearnTip/btn_useMoney/Text").GetComponent<Text>().text = ModelBase<A3_SmithyModel>.getInstance().rlrnMoneyCost.ToString();
			base.transform.FindChild("RelearnTip/btn_useDiamond/Text").GetComponent<Text>().text = ModelBase<A3_SmithyModel>.getInstance().rlrnDiamondCost.ToString();
			this.rlrnArmor.gameObject.SetActive(true);
			this.rlrnJewelry.gameObject.SetActive(true);
			this.rlrnWeapon.gameObject.SetActive(true);
			switch (ModelBase<A3_SmithyModel>.getInstance().CurSmithyType)
			{
			case SMITHY_PART.ARMOR:
				this.rlrnArmor.gameObject.SetActive(false);
				break;
			case SMITHY_PART.WEAPON:
				this.rlrnWeapon.gameObject.SetActive(false);
				break;
			case SMITHY_PART.JEWELRY:
				this.rlrnJewelry.gameObject.SetActive(false);
				break;
			}
		}

		private void RefreshNumText()
		{
			for (int i = 0; i < this.matObjList.Count; i++)
			{
				this.matObjList[i].transform.FindChild("Text").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.targetMatList[i].tpid) + "/" + this.targetMatList[i].num * this.targetNum;
			}
			bool flag = this.targetId != this.RANDOM_ITEM_ID;
			if (flag)
			{
				this.moneyNeed.text = ModelBase<A3_SmithyModel>.getInstance().GetMoneyCostById(this.targetId, this.targetNum).ToString();
			}
			else
			{
				this.moneyNeed.text = ModelBase<A3_SmithyModel>.getInstance().GetMoneyCostByScroll(this.targetNum).ToString();
			}
		}

		public override void onShowed()
		{
			base.onShowed();
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_ITEM_CHANGE, new Action<GameEvent>(this.OnItemChange));
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_ITME_SELL, new Action<GameEvent>(this.onItemSell));
			BaseProxy<A3_SmithyProxy>.getInstance().addEventListener(A3_SmithyProxy.ON_SMITHYDATACHANGED, new Action<GameEvent>(this.OnSmithyDataChanged));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_EQUIP_PUTON, new Action<GameEvent>(this.onEquipOn));
			BaseProxy<A3_SmithyProxy>.getInstance().SendRefresh();
			InputField expr_89 = this.textNum;
			int.TryParse(((expr_89 != null) ? expr_89.text : null) ?? "1", out this.targetNum);
			this.CurrentSelectedCarr = 0;
			this.CurrentSelectedPart = 0;
			this.LoadItem();
			this.RefreshPanel();
			this.onOpenLockRec(null);
			A3_Smithy.Instance = this;
		}

		public override void onClosed()
		{
			BaseProxy<A3_SmithyProxy>.getInstance().removeEventListener(A3_SmithyProxy.ON_SMITHYDATACHANGED, new Action<GameEvent>(this.OnSmithyDataChanged));
			BaseProxy<BagProxy>.getInstance().removeEventListener(BagProxy.EVENT_ITME_SELL, new Action<GameEvent>(this.onItemSell));
			BaseProxy<BagProxy>.getInstance().removeEventListener(BagProxy.EVENT_ITEM_CHANGE, new Action<GameEvent>(this.OnItemChange));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_EQUIP_PUTON, new Action<GameEvent>(this.onEquipOn));
			this.OnSelectedTargetEqp(0u);
			this.CollapseItem(this.currentSelectedCarr);
			foreach (GameObject current in this.itemicon.Values)
			{
				UnityEngine.Object.Destroy(current);
			}
			this.itemicon.Clear();
			A3_Smithy.Instance = null;
			this.curBodyEqpInfo = default(a3_BagItemData);
			this.targetId = 0u;
			base.onClosed();
		}

		private void onEquipOn(GameEvent e)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
			UnityEngine.Object.DestroyImmediate(this.curEqpInfo.Value);
			int childCount = this.itemParent.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = this.itemParent.transform.GetChild(i);
				bool flag = child && child.childCount > 1;
				if (!flag)
				{
					bool flag2 = this.curBodyEqpInfo.id > 0u;
					if (flag2)
					{
						this.CreateItemIcon(this.curBodyEqpInfo, i);
					}
					break;
				}
			}
		}

		private void HideLvUp()
		{
			this.fxSmithyLvUp.SetActive(false);
		}

		private void ShowLvUp()
		{
			this.fxSmithyLvUp.SetActive(true);
		}

		private void DoLvUp()
		{
			base.Invoke("ShowLvUp", 0f);
			base.Invoke("HideLvUp", 3f);
		}

		private void OnSmithyDataChanged(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("exp");
			if (flag)
			{
				ModelBase<A3_SmithyModel>.getInstance().CurSmithyExp = data["exp"];
			}
			bool flag2 = data.ContainsKey("lvl");
			if (flag2)
			{
				int curSmithyLevel = ModelBase<A3_SmithyModel>.getInstance().CurSmithyLevel;
				ModelBase<A3_SmithyModel>.getInstance().CurSmithyLevel = data["lvl"];
				bool flag3 = curSmithyLevel != 0 && ModelBase<A3_SmithyModel>.getInstance().CurSmithyLevel != curSmithyLevel;
				if (flag3)
				{
					for (int i = 0; i < this.listCarr.Count; i++)
					{
						bool flag4 = this.dicExpandState.ContainsKey(this.listCarr[i]) && this.dicExpandState[this.listCarr[i]] != ExpandState.NotInitialized && this.dicSubHead.ContainsKey(this.listCarr[i]);
						if (flag4)
						{
							this.dicSubHead[this.listCarr[i]].ShowItemByPart(this.CurrentSelectedPart);
							this.dicSubHead[this.listCarr[i]].RectScroll.sizeDelta = new Vector2(this.dicSubHead[this.listCarr[i]].RectScroll.sizeDelta.x, this.dicSubHead[this.listCarr[i]].GetFixedHeight(this.CurrentSelectedPart));
							bool flag5 = this.listCarr[i] > 0;
							if (flag5)
							{
								this.dicSubHead[this.listCarr[i]].FixHeight();
							}
						}
					}
					this.DoLvUp();
				}
				bool flag6 = ModelBase<A3_SmithyModel>.getInstance().CurSmithyLevel == 0;
				if (flag6)
				{
					this.InitSmithy(0u);
					return;
				}
				float num = (float)ModelBase<A3_SmithyModel>.getInstance().CurSmithyExp;
				float num2 = (float)ModelBase<A3_SmithyModel>.getInstance().CalcMaxExp(ModelBase<A3_SmithyModel>.getInstance().CurSmithyLevel);
				this.sliderExp.value = num / num2;
				this.txtLv.text = ModelBase<A3_SmithyModel>.getInstance().CurSmithyLevel.ToString();
			}
			bool flag7 = data.ContainsKey("type");
			if (flag7)
			{
				ModelBase<A3_SmithyModel>.getInstance().CurSmithyType = (SMITHY_PART)data["type"]._int;
				this.InitSmithy(data["type"]._uint);
			}
		}

		private void InitSmithy(uint typeId)
		{
			List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
			bool flag = typeId != 0u && this.listPartIdx.Count > 1;
			if (!flag)
			{
				bool flag2 = this.dicMainObj.ContainsKey(this.USE_SCROLL);
				if (flag2)
				{
					this.dicMainObj[this.USE_SCROLL].transform.FindChild("img_expand").gameObject.SetActive(false);
					this.dicMainObj[this.USE_SCROLL].transform.FindChild("img_collapse").gameObject.SetActive(false);
				}
				switch (typeId)
				{
				case 0u:
					IL_AD:
					this.mainPanel.gameObject.SetActive(false);
					this.InitLearnPanel();
					goto IL_313;
				case 1u:
					list.Add(new Dropdown.OptionData("全部"));
					this.listPartIdx.Add(0);
					list.Add(new Dropdown.OptionData("头部"));
					this.listPartIdx.Add(1);
					list.Add(new Dropdown.OptionData("肩甲"));
					this.listPartIdx.Add(2);
					list.Add(new Dropdown.OptionData("铠甲"));
					this.listPartIdx.Add(3);
					list.Add(new Dropdown.OptionData("腿部"));
					this.listPartIdx.Add(4);
					list.Add(new Dropdown.OptionData("脚部"));
					this.listPartIdx.Add(5);
					SubHeadNode.ListPartIdx = this.listPartIdx;
					this.dropdownPart.AddOptions(list);
					this.choosePart.gameObject.SetActive(false);
					this.mainPanel.gameObject.SetActive(true);
					goto IL_313;
				case 2u:
					list.Add(new Dropdown.OptionData("全部"));
					this.listPartIdx.Add(0);
					list.Add(new Dropdown.OptionData("武器"));
					this.listPartIdx.Add(6);
					list.Add(new Dropdown.OptionData("副手"));
					this.listPartIdx.Add(7);
					SubHeadNode.ListPartIdx = this.listPartIdx;
					this.dropdownPart.AddOptions(list);
					this.choosePart.gameObject.SetActive(false);
					this.mainPanel.gameObject.SetActive(true);
					goto IL_313;
				case 3u:
					list.Add(new Dropdown.OptionData("全部"));
					this.listPartIdx.Add(0);
					list.Add(new Dropdown.OptionData("项链"));
					this.listPartIdx.Add(8);
					list.Add(new Dropdown.OptionData("指环"));
					this.listPartIdx.Add(9);
					list.Add(new Dropdown.OptionData("戒指"));
					this.listPartIdx.Add(10);
					SubHeadNode.ListPartIdx = this.listPartIdx;
					this.dropdownPart.AddOptions(list);
					this.choosePart.gameObject.SetActive(false);
					this.mainPanel.gameObject.SetActive(true);
					goto IL_313;
				}
				goto IL_AD;
				IL_313:
				this.RefreshPanel();
			}
		}

		private void InitLearnPanel()
		{
			this.choosePart.gameObject.SetActive(true);
			new BaseButton(this.choosePart.FindChild("border/learn_weapon"), 1, 1).onClick = delegate(GameObject go)
			{
				this.InitCheckPanel(2u);
			};
			new BaseButton(this.choosePart.FindChild("border/learn_armor"), 1, 1).onClick = delegate(GameObject go)
			{
				this.InitCheckPanel(1u);
			};
			new BaseButton(this.choosePart.FindChild("border/learn_accessories"), 1, 1).onClick = delegate(GameObject go)
			{
				this.InitCheckPanel(3u);
			};
			new BaseButton(this.choosePart.FindChild("border/learn_suggest"), 1, 1).onClick = delegate(GameObject go)
			{
				this.InitCheckRandomPanel();
			};
		}

		private void InitCheckPanel(uint partId)
		{
			this.choosePart.gameObject.SetActive(false);
			this.checkPart.gameObject.SetActive(true);
			switch (partId)
			{
			case 1u:
				this.checkPart.FindChild("border/Part").GetComponent<Text>().text = "武器";
				break;
			case 2u:
				this.checkPart.FindChild("border/Part").GetComponent<Text>().text = "防具";
				break;
			case 3u:
				this.checkPart.FindChild("border/Part").GetComponent<Text>().text = "首饰";
				break;
			}
			new BaseButton(this.checkPart.FindChild("border/OK"), 1, 1).onClick = delegate(GameObject _go)
			{
				this.checkPart.gameObject.SetActive(false);
				BaseProxy<A3_SmithyProxy>.getInstance().SendChooseLearn(partId);
				this.InitSmithy(partId);
			};
			new BaseButton(this.checkPart.FindChild("border/cancel"), 1, 1).onClick = delegate(GameObject _go)
			{
				this.choosePart.gameObject.SetActive(true);
				this.checkPart.gameObject.SetActive(false);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SMITHY);
			};
		}

		private void InitCheckRandomPanel()
		{
			this.choosePart.gameObject.SetActive(false);
			this.checkRandomPart.gameObject.SetActive(true);
			new BaseButton(this.checkRandomPart.FindChild("border/OK"), 1, 1).onClick = delegate(GameObject _go)
			{
				uint num = (uint)UnityEngine.Random.Range(1, 3);
				BaseProxy<A3_SmithyProxy>.getInstance().SendChooseLearn(num);
				this.InitSmithy(num);
				this.checkRandomPart.gameObject.SetActive(false);
			};
			new BaseButton(this.checkRandomPart.FindChild("border/cancel"), 1, 1).onClick = delegate(GameObject _go)
			{
				this.choosePart.gameObject.SetActive(true);
				this.checkRandomPart.gameObject.SetActive(false);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SMITHY);
			};
		}

		private void Make()
		{
			this.targetNum = 1;
			float sec = 3f;
			bool flag = !int.TryParse(this.textNum.text, out this.targetNum);
			if (flag)
			{
				this.textNum.text = "1";
			}
			bool flag2 = this.targetId <= 0u;
			if (flag2)
			{
				flytxt.instance.fly("请选择要打造的装备", 0, default(Color), null);
			}
			else
			{
				bool isMatEnough = this.IsMatEnough;
				if (isMatEnough)
				{
					bool isMoneyEnough = this.IsMoneyEnough;
					if (isMoneyEnough)
					{
						bool isRoomEnough = this.IsRoomEnough;
						if (isRoomEnough)
						{
							Action<cd> arg_B3_0;
							if ((arg_B3_0 = A3_Smithy.<>c.<>9__75_0) == null)
							{
								arg_B3_0 = (A3_Smithy.<>c.<>9__75_0 = new Action<cd>(A3_Smithy.<>c.<>9.<Make>b__75_0));
							}
							cd.updateHandle = arg_B3_0;
							cd.show(delegate
							{
								bool flag3 = this.targetId == this.RANDOM_ITEM_ID;
								if (flag3)
								{
									BaseProxy<A3_SmithyProxy>.getInstance().SendMakeByScroll(this.targetNum);
								}
								else
								{
									BaseProxy<A3_SmithyProxy>.getInstance().SendMake(this.targetId, 1u, this.targetNum);
								}
							}, sec, false, null, default(Vector3));
						}
						else
						{
							flytxt.instance.fly("背包空间不足", 0, default(Color), null);
						}
					}
					else
					{
						flytxt.instance.fly("金钱不足", 0, default(Color), null);
					}
				}
				else
				{
					flytxt.instance.fly("材料不足", 0, default(Color), null);
				}
			}
		}

		private bool CheckMat()
		{
			bool flag = this.targetId != this.RANDOM_ITEM_ID;
			bool result;
			if (flag)
			{
				for (int i = 0; i < this.targetMatList.Count; i++)
				{
					bool flag2 = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.targetMatList[i].tpid) < this.targetMatList[i].num * this.targetNum;
					if (flag2)
					{
						result = false;
						return result;
					}
				}
			}
			else
			{
				List<MatInfo> list = ModelBase<A3_SmithyModel>.getInstance().smithyInfoDicUseScroll[(int)ModelBase<A3_SmithyModel>.getInstance().CurSmithyType];
				for (int j = 0; j < list.Count; j++)
				{
					bool flag3 = list[j].num * this.targetNum > ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(list[j].tpid);
					if (flag3)
					{
						result = false;
						return result;
					}
				}
			}
			result = true;
			return result;
		}

		public void LoadItem()
		{
			int i = 0;
			Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
			List<uint> list = new List<uint>(items.Keys);
			while (i < items.Count)
			{
				this.CreateItemIcon(items[list[i]], i);
				i++;
			}
		}

		private void OnSelectedTargetEqp(uint id)
		{
			Transform expr_07 = this.targetMatListObj;
			for (int i = ((expr_07 != null) ? expr_07.childCount : 0) - 1; i > -1; i--)
			{
				UnityEngine.Object.DestroyImmediate(this.targetMatListObj.GetChild(i).gameObject);
			}
			this.matObjList.Clear();
			bool flag = this.targetIcon != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this.targetIcon);
			}
			this.targetId = id;
			bool flag2 = this.targetId == 0u;
			if (!flag2)
			{
				this.targetIcon = IconImageMgr.getInstance().createA3ItemIcon(ModelBase<a3_BagModel>.getInstance().getItemDataById(this.targetId), true, -1, 0.7f, false, -1, 0, false, false, false, -1, true, false);
				new BaseButton(this.targetIcon.GetComponentInChildren<Button>().transform, 1, 1).onClick = delegate(GameObject go)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(this.targetId);
					arrayList.Add(1);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
				};
				this.targetIcon.transform.SetParent(this.targetEqp, false);
				this.targetMatList = this.GetMatList(this.targetId);
				bool flag3 = this.textNum.text == "";
				if (flag3)
				{
					this.textNum.text = "1";
					this.targetNum = 1;
				}
				for (int j = 0; j < this.targetMatList.Count; j++)
				{
					a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(this.targetMatList[j].tpid);
					uint tpid = this.targetMatList[j].tpid;
					GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(itemDataById, true, -1, 0.6f, false, -1, 0, false, false, false, -1, false, false);
					Transform expr_1AA = gameObject.transform.FindChild("bicon");
					if (expr_1AA != null)
					{
						Image expr_1B5 = expr_1AA.GetComponent<Image>();
						if (expr_1B5 != null)
						{
							expr_1B5.gameObject.SetActive(true);
						}
					}
					new BaseButton(gameObject.transform.GetComponent<Button>().transform, 1, 1).onClick = delegate(GameObject go)
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add(tpid);
						arrayList.Add(1);
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
					};
					this.matIcon.name = "Item";
					Transform transform = UnityEngine.Object.Instantiate<Transform>(this.matIcon).transform;
					gameObject.transform.SetParent(transform, false);
					transform.transform.FindChild("Text").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.targetMatList[j].tpid) + "/" + this.targetMatList[j].num * this.targetNum;
					transform.SetParent(this.targetMatListObj, false);
					this.matObjList.Add(transform.gameObject);
				}
				this.moneyNeed.text = ModelBase<A3_SmithyModel>.getInstance().GetMoneyCostById(this.targetId, this.targetNum).ToString();
			}
		}

		private void OnSelectedScroll()
		{
			bool flag = this.targetNum == 0;
			if (flag)
			{
				this.targetNum = 1;
				this.textNum.text = "1";
			}
			for (int i = this.targetMatListObj.childCount - 1; i > -1; i--)
			{
				UnityEngine.Object.DestroyImmediate(this.targetMatListObj.GetChild(i).gameObject);
			}
			this.matObjList.Clear();
			bool flag2 = this.targetIcon != null;
			if (flag2)
			{
				UnityEngine.Object.DestroyImmediate(this.targetIcon);
			}
			this.targetId = this.RANDOM_ITEM_ID;
			this.targetIcon = UnityEngine.Object.Instantiate<GameObject>(this.randomItemIcon);
			this.targetIcon.transform.SetParent(this.targetEqp, false);
			this.targetMatList = ModelBase<A3_SmithyModel>.getInstance().GetMatListUseScroll();
			for (int j = 0; j < this.targetMatList.Count; j++)
			{
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(this.targetMatList[j].tpid);
				GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(itemDataById, true, -1, 0.6f, false, -1, 0, false, false, false, -1, false, false);
				Transform expr_11E = gameObject.transform.FindChild("bicon");
				if (expr_11E != null)
				{
					Image expr_129 = expr_11E.GetComponent<Image>();
					if (expr_129 != null)
					{
						expr_129.gameObject.SetActive(true);
					}
				}
				this.matIcon.name = "Item";
				Transform transform = UnityEngine.Object.Instantiate<Transform>(this.matIcon).transform;
				gameObject.transform.SetParent(transform, false);
				transform.transform.FindChild("Text").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.targetMatList[j].tpid) + "/" + this.targetMatList[j].num * this.targetNum;
				transform.SetParent(this.targetMatListObj, false);
				this.matObjList.Add(transform.gameObject);
			}
			this.moneyNeed.text = ModelBase<A3_SmithyModel>.getInstance().GetMoneyCostByScroll(this.targetNum).ToString();
		}

		private List<MatInfo> GetMatList(uint tpid)
		{
			return ModelBase<A3_SmithyModel>.getInstance().GetMatListById(tpid);
		}

		private void OnSelectCarr(int select)
		{
			this.CurrentSelectedCarr = select;
			this.RefreshPanel();
		}

		private void OnSelectPart(int select)
		{
			this.CurrentSelectedPart = select;
			bool flag = !this.dicSubHead.ContainsKey(this.currentExpandCarr) || this.dicExpandState[this.currentExpandCarr] == ExpandState.NotInitialized;
			if (!flag)
			{
				this.RefreshExpand();
				for (int i = 0; i < this.listMainItem.Count; i++)
				{
					bool flag2 = this.dicSubHead.ContainsKey(this.listMainItem[i].Key);
					if (flag2)
					{
						this.dicSubHead[this.listMainItem[i].Key].ShowItemByPart(this.CurrentSelectedPart);
					}
				}
				this.dicSubHead[this.currentExpandCarr].RectScroll.sizeDelta = new Vector2(this.dicSubHead[this.currentExpandCarr].RectScroll.sizeDelta.x, this.dicSubHead[this.currentExpandCarr].GetFixedHeight(this.CurrentSelectedPart));
				bool flag3 = this.currentExpandCarr > 0;
				if (flag3)
				{
					this.dicSubHead[this.currentExpandCarr].FixHeight();
				}
			}
		}

		private void RefreshExpand()
		{
			int i = 0;
			int curSmithyLevel = ModelBase<A3_SmithyModel>.getInstance().CurSmithyLevel;
			List<int> list = new List<int>(this.dicExpandState.Keys);
			while (i < this.dicExpandState.Count)
			{
				bool flag = !this.dicSubHead.ContainsKey(list[i]);
				if (!flag)
				{
					List<a3_EquipData> list2 = new List<a3_EquipData>();
					List<a3_EquipData> equipListByEquipType = ModelBase<a3_EquipModel>.getInstance().GetEquipListByEquipType(this.CurrentSelectedPart);
					for (int j = 0; j < equipListByEquipType.Count; j++)
					{
						a3_EquipData a3_EquipData = equipListByEquipType[j];
						bool flag2 = this.GetMatList(a3_EquipData.tpid.GetValueOrDefault(0u)).Count > 0;
						if (flag2)
						{
							list2.Add(equipListByEquipType[j]);
						}
					}
					for (int k = 0; k < list2.Count; k++)
					{
						a3_BagModel arg_E8_0 = ModelBase<a3_BagModel>.getInstance();
						a3_EquipData a3_EquipData = list2[k];
						a3_ItemData itemDataById = arg_E8_0.getItemDataById(a3_EquipData.tpid.Value);
						bool flag3 = itemDataById.job_limit != 1 && itemDataById.job_limit != list[i];
						if (!flag3)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.subItem.gameObject);
							gameObject.SetActive(itemDataById.equip_level <= ModelBase<A3_SmithyModel>.getInstance().smithyLevelInfoList[curSmithyLevel - 1].MaxAllowedSetLv);
							gameObject.transform.FindChild("Text").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getEquipName(itemDataById.tpid);
							this.dicSubHead[list[i]].Add(itemDataById.tpid, new KeyValuePair<int, GameObject>(itemDataById.equip_type, gameObject));
							uint id = itemDataById.tpid;
							new BaseButton(gameObject.transform, 1, 1).onClick = delegate(GameObject go)
							{
								this.OnSelectedTargetEqp(id);
							};
						}
					}
				}
				i++;
			}
		}

		private void RefreshPanel()
		{
			int i = 0;
			List<int> list = new List<int>(this.dicMainObj.Keys);
			while (i < this.dicMainObj.Count)
			{
				this.dicMainObj[list[i]].SetActive(false);
				bool flag = this.CurrentSelectedCarr != 0;
				if (flag)
				{
					bool flag2 = this.CurrentSelectedCarr != list[i] && this.dicSubHead.ContainsKey(list[i]);
					if (flag2)
					{
						this.dicSubHead[list[i]].HeadObj.SetActive(false);
						this.CollapseItem(list[i]);
					}
				}
				i++;
			}
			for (i = 0; i < this.listMainItem.Count; i++)
			{
				bool flag3 = !this.dicMainObj.ContainsKey(this.listMainItem[i].Key);
				if (flag3)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mainItem.gameObject);
					gameObject.transform.FindChild("Text").GetComponent<Text>().text = this.listMainItem[i].Value;
					gameObject.transform.SetParent(this.transEqpList, false);
					this.dicMainObj.Add(this.listMainItem[i].Key, gameObject);
					this.dicExpandState.Add(this.listMainItem[i].Key, ExpandState.NotInitialized);
					int selectedCarr = this.listMainItem[i].Key;
					bool flag4 = this.listMainItem[i].Key == this.USE_SCROLL;
					if (flag4)
					{
						new BaseButton(gameObject.transform, 1, 1).onClick = delegate(GameObject go)
						{
							this.OnSelectedScroll();
						};
					}
					else
					{
						new BaseButton(gameObject.transform, 1, 1).onClick = delegate(GameObject go)
						{
							this.OnExpandItemClick(selectedCarr);
						};
					}
				}
				this.dicMainObj[this.listMainItem[i].Key].SetActive(true);
			}
		}

		private void OnExpandItemClick(int selectedCarr)
		{
			ExpandState expandState = this.dicExpandState[selectedCarr];
			if (expandState != ExpandState.Expanded)
			{
				this.ExpandItem(selectedCarr);
				int i = 0;
				List<int> list = new List<int>(this.dicExpandState.Keys);
				while (i < list.Count)
				{
					bool flag = list[i] != selectedCarr && this.dicExpandState[list[i]] == ExpandState.Expanded;
					if (flag)
					{
						this.CollapseItem(list[i]);
					}
					i++;
				}
				base.Invoke("FixContentHeight", 0.2f);
			}
			else
			{
				this.CollapseItem(selectedCarr);
			}
		}

		private void FixContentHeight()
		{
			this.dicSubHead[this.currentExpandCarr].FixContentHeight();
		}

		private void CollapseItem(int selectedCarr)
		{
			bool flag = selectedCarr == 0;
			if (!flag)
			{
				this.dicExpandState[selectedCarr] = ExpandState.Collaspsed;
				this.dicMainObj[selectedCarr].transform.FindChild("img_expand").gameObject.SetActive(false);
				this.dicSubHead[selectedCarr].HeadObj.SetActive(false);
			}
		}

		private void ExpandItem(int selectedCarr)
		{
			this.currentExpandCarr = selectedCarr;
			this.dicMainObj[selectedCarr].transform.FindChild("img_expand").gameObject.SetActive(true);
			bool flag = !this.dicExpandState.ContainsKey(selectedCarr) || this.dicExpandState[selectedCarr] == ExpandState.NotInitialized;
			if (flag)
			{
				this.ExpandInit(selectedCarr);
			}
			else
			{
				this.dicSubHead[selectedCarr].HeadObj.SetActive(true);
			}
			this.dicSubHead[selectedCarr].RectScroll.sizeDelta = new Vector2(this.dicSubHead[selectedCarr].RectScroll.sizeDelta.x, this.dicSubHead[selectedCarr].GetFixedHeight(this.CurrentSelectedPart));
			this.dicSubHead[selectedCarr].FixHeight();
			this.dicExpandState[this.currentExpandCarr] = ExpandState.Expanded;
		}

		private void ExpandInit(int selectedCarr)
		{
			int curSmithyLevel = ModelBase<A3_SmithyModel>.getInstance().CurSmithyLevel;
			List<a3_EquipData> list = new List<a3_EquipData>();
			List<a3_EquipData> equipListByEquipType = ModelBase<a3_EquipModel>.getInstance().GetEquipListByEquipType(this.CurrentSelectedPart);
			for (int i = 0; i < equipListByEquipType.Count; i++)
			{
				bool flag = this.CurrentSelectedPart == equipListByEquipType[i].eqp_type || (this.CurrentSelectedPart == 0 && this.listPartIdx.Contains(equipListByEquipType[i].eqp_type));
				if (flag)
				{
					a3_EquipData a3_EquipData = equipListByEquipType[i];
					bool flag2 = this.GetMatList(a3_EquipData.tpid.GetValueOrDefault(0u)).Count > 0;
					if (flag2)
					{
						list.Add(equipListByEquipType[i]);
					}
				}
			}
			SubHeadNode subHeadNode = new SubHeadNode(UnityEngine.Object.Instantiate<Transform>(this.subHead).gameObject);
			for (int j = 0; j < list.Count; j++)
			{
				a3_BagModel arg_108_0 = ModelBase<a3_BagModel>.getInstance();
				a3_EquipData a3_EquipData = list[j];
				a3_ItemData itemDataById = arg_108_0.getItemDataById(a3_EquipData.tpid.Value);
				bool flag3 = itemDataById.job_limit != 1 && itemDataById.job_limit != selectedCarr;
				if (!flag3)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.subItem.gameObject);
					gameObject.SetActive(itemDataById.equip_level <= ModelBase<A3_SmithyModel>.getInstance().smithyLevelInfoList[curSmithyLevel - 1].MaxAllowedSetLv);
					a3_ItemData itemDataById2 = ModelBase<a3_BagModel>.getInstance().getItemDataById(itemDataById.tpid);
					gameObject.transform.FindChild("Text").GetComponent<Text>().text = this.DyeEquipNameByQuality(itemDataById2.item_name, itemDataById2.quality);
					subHeadNode.Add(itemDataById.tpid, new KeyValuePair<int, GameObject>(itemDataById.equip_type, gameObject));
					uint id = itemDataById.tpid;
					new BaseButton(gameObject.transform, 1, 1).onClick = delegate(GameObject go)
					{
						this.OnSelectedTargetEqp(id);
					};
				}
			}
			subHeadNode.HeadObj.transform.SetParent(this.transEqpList, false);
			subHeadNode.HeadObj.transform.SetSiblingIndex(this.dicMainObj[selectedCarr].transform.GetSiblingIndex() + 1);
			bool flag4 = !this.dicSubHead.ContainsKey(selectedCarr);
			if (flag4)
			{
				this.dicSubHead.Add(selectedCarr, subHeadNode);
			}
		}

		private string DyeEquipNameByQuality(string eqpName, int quality)
		{
			bool flag = quality == 1;
			string result;
			if (flag)
			{
				result = "<color=#FFFFFF>" + eqpName + "</color>";
			}
			else
			{
				bool flag2 = quality == 2;
				if (flag2)
				{
					result = "<color=#6CE868>" + eqpName + "</color>";
				}
				else
				{
					bool flag3 = quality == 3;
					if (flag3)
					{
						result = "<color=#4E8BEE>" + eqpName + "</color>";
					}
					else
					{
						bool flag4 = quality == 4;
						if (flag4)
						{
							result = "<color=#7C48D5>" + eqpName + "</color>";
						}
						else
						{
							bool flag5 = quality == 5;
							if (flag5)
							{
								result = "<color=#FFD800>" + eqpName + "</color>";
							}
							else
							{
								bool flag6 = quality == 6;
								if (flag6)
								{
									result = "<color=#FFD800>" + eqpName + "</color>";
								}
								else
								{
									bool flag7 = quality == 7;
									if (flag7)
									{
										result = "<color=#FFD800>" + eqpName + "</color>";
									}
									else
									{
										result = "<color=#FFFFFF>" + eqpName + "</color>";
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		private void Clear()
		{
			this.dropdownPart.ClearOptions();
			List<int> expr_12 = SubHeadNode.ListPartIdx;
			if (expr_12 != null)
			{
				expr_12.Clear();
			}
			this.listPartIdx.Clear();
			for (int i = this.targetMatListObj.childCount; i > 0; i--)
			{
				UnityEngine.Object.DestroyImmediate(this.targetMatListObj.GetChild(i - 1).gameObject);
			}
			GameObject expr_63 = this.targetIcon;
			UnityEngine.Object.DestroyImmediate((expr_63 != null) ? expr_63.gameObject : null);
			for (int j = this.transEqpList.childCount; j > 0; j--)
			{
				UnityEngine.Object.DestroyImmediate(this.transEqpList.GetChild(j - 1).gameObject);
			}
			this.dicSubHead.Clear();
			this.dicMainObj.Clear();
			this.dicExpandState.Clear();
		}

		private void OnRelearnByMoney(GameObject go)
		{
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().gold < (ulong)((long)ModelBase<A3_SmithyModel>.getInstance().rlrnMoneyCost);
			if (flag)
			{
				flytxt.instance.fly("金币不足", 0, default(Color), null);
			}
			else
			{
				this.OnRelearn(1);
			}
		}

		private void OnRelearnByDiamond(GameObject go)
		{
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().money < (ulong)((long)ModelBase<A3_SmithyModel>.getInstance().rlrnDiamondCost);
			if (flag)
			{
				flytxt.instance.fly("钻石不足", 0, default(Color), null);
			}
			else
			{
				this.OnRelearn(2);
			}
		}

		private void OnRelearn(int costWay)
		{
			int num = 0;
			bool flag = this.tglRlrnArmor.gameObject.activeSelf && this.tglRlrnArmor.isOn;
			if (flag)
			{
				num = 1;
			}
			else
			{
				bool flag2 = this.tglRlrnWeapon.gameObject.activeSelf && this.tglRlrnWeapon.isOn;
				if (flag2)
				{
					num = 2;
				}
				else
				{
					bool flag3 = this.tglRlrnJewelry.gameObject.activeSelf && this.tglRlrnJewelry.isOn;
					if (flag3)
					{
						num = 3;
					}
				}
			}
			bool flag4 = num == 0;
			if (!flag4)
			{
				this.Clear();
				this.mainPanel.gameObject.SetActive(false);
				BaseProxy<A3_SmithyProxy>.getInstance().SendRelearn(num, costWay);
				this.relearnTip.SetActive(false);
			}
		}

		private void CreateItemIcon(a3_BagItemData data, int i)
		{
			GameObject icon = IconImageMgr.getInstance().createA3ItemIcon(data, true, data.num, 1f, false);
			icon.transform.SetParent(this.itemParent.transform.GetChild(i), false);
			this.itemicon[data.id] = icon;
			bool flag = data.num <= 1;
			if (flag)
			{
				icon.transform.FindChild("num").gameObject.SetActive(false);
			}
			BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.OnItemClick(icon, data.id);
			};
		}

		private void OnItemClick(GameObject go, uint id)
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
				this.curEqpInfo = new KeyValuePair<int, GameObject>(go.transform.GetSiblingIndex(), go);
				bool flag = ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(a3_BagItemData.confdata.equip_type);
				if (flag)
				{
					this.curBodyEqpInfo = ModelBase<a3_EquipModel>.getInstance().getEquipsByType()[a3_BagItemData.confdata.equip_type];
				}
				ArrayList arrayList = new ArrayList();
				arrayList.Add(a3_BagItemData);
				arrayList.Add(equip_tip_type.BagPick_tip);
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
					arrayList3.Add(equip_tip_type.Bag_tip);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMTIP, arrayList3, false);
				}
			}
		}

		private void OnItemChange(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("add");
			if (flag)
			{
				bool flag2 = ModelBase<a3_BagModel>.getInstance().getItems(false).Count <= ModelBase<a3_BagModel>.getInstance().curi;
				if (flag2)
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
						this.itemicon[key2].transform.FindChild("num").GetComponent<Text>().text = current2["cnt"]._str;
						bool flag6 = current2["cnt"]._int32 <= 1;
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
							GameObject gameObject2 = base.transform.FindChild("Main/right_bag/item_scroll/scroll_view/icon").gameObject;
							GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
							gameObject3.SetActive(true);
							gameObject3.transform.SetParent(this.itemParent.transform, false);
							gameObject3.transform.SetSiblingIndex(this.itemicon.Count + num);
						}
					}
				}
			}
			bool flag9 = this.targetId > 0u;
			if (flag9)
			{
				bool flag10 = this.targetId != this.RANDOM_ITEM_ID;
				if (flag10)
				{
					this.OnSelectedTargetEqp(this.targetId);
				}
				bool flag11 = this.targetId == this.RANDOM_ITEM_ID;
				if (flag11)
				{
					this.OnSelectedScroll();
				}
			}
		}

		private void onChushou(GameObject go)
		{
			base.transform.FindChild("Main/right_bag/piliang_chushou").gameObject.SetActive(true);
			this.Money.text = "0";
			this.SellPutin();
			this.clearCon();
			this.OnLoadTitm_chushou();
		}

		private void SellPutin()
		{
			foreach (uint current in ModelBase<a3_BagModel>.getInstance().getUnEquips().Keys)
			{
				uint tpid = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].tpid;
				bool flag = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid).quality <= 3;
				if (flag)
				{
					bool flag2 = this.dic_BagItem_shll.ContainsKey(current);
					if (flag2)
					{
						this.dic_BagItem_shll.Remove(current);
					}
					int num = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].num;
					this.dic_BagItem_shll[current] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current];
					this.ShowMoneyCount(ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].tpid, num, true);
				}
			}
			Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
			foreach (uint current2 in items.Keys)
			{
				uint tpid2 = items[current2].tpid;
				bool flag3 = items[current2].confdata.use_type == 2 || items[current2].confdata.use_type == 3;
				if (flag3)
				{
					SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + tpid2);
					bool flag4 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)sXML.getInt("use_limit"));
					if (flag4)
					{
						bool flag5 = ModelBase<PlayerModel>.getInstance().up_lvl == 1u;
						if (!flag5)
						{
							bool flag6 = ModelBase<PlayerModel>.getInstance().up_lvl == 3u;
							if (flag6)
							{
								bool flag7 = tpid2 == 1531u;
								if (flag7)
								{
									continue;
								}
								bool flag8 = tpid2 == 1532u;
								if (flag8)
								{
									continue;
								}
							}
							bool flag9 = this.dic_BagItem_shll.ContainsKey(current2);
							if (flag9)
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

		public void refresh_Sell()
		{
			base.transform.FindChild("Main/right_bag/piliang_chushou").gameObject.SetActive(false);
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
			base.transform.FindChild("Main/right_bag/piliang_fenjie").gameObject.SetActive(false);
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

		private void onfenjieclose(GameObject go)
		{
			this.dic_BagItem.Clear();
			this.clearCon_fenjie();
			this.conIndex = 0;
			this.mojing.text = string.Concat(0);
			this.shengguanghuiji.text = string.Concat(0);
			this.mifageli.text = string.Concat(0);
			base.transform.FindChild("Main/right_bag/piliang_fenjie").gameObject.SetActive(false);
		}

		private void onOpenLockRec(GameEvent e)
		{
			for (int i = 50; i < this.itemParent.transform.childCount; i++)
			{
				GameObject gameObject = this.itemParent.transform.GetChild(i).FindChild("lock").gameObject;
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

		private void onEquipSell(GameObject go)
		{
			this.white.isOn = false;
			this.green.isOn = false;
			this.blue.isOn = false;
			this.purple.isOn = false;
			this.orange.isOn = false;
			base.transform.FindChild("Main/right_bag/piliang_fenjie").gameObject.SetActive(true);
			this.mojing_num = 0;
			this.shengguanghuiji_num = 0;
			this.mifageli_num = 0;
			this.clearCon_fenjie();
			this.OnLoadItem_fenjie();
		}
	}
}
