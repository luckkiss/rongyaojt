using Cross;
using DG.Tweening;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_equip : Window
	{
		public Dictionary<uint, GameObject> equipicon = new Dictionary<uint, GameObject>();

		private TabControl tabCtrl1;

		private GameObject[] equipPanel;

		private GameObject[] infoPanel;

		private GameObject shellcon;

		private GameObject shellcon_baoshi;

		private uint[] intensify_need_id = new uint[3];

		private ScrollControler scrollControler1;

		private ScrollControler scrollControler2;

		private GameObject[] shell;

		private GameObject itemListView;

		private GridLayoutGroup item_Parent;

		private Image add_Change;

		private Text qhDashi_text;

		private GameObject QHDashi;

		private GameObject Inbaoshi;

		private GameObject canUp;

		private GameObject ismaxlvl;

		private Dictionary<a3_ItemData, int> Needobj = new Dictionary<a3_ItemData, int>();

		private Transform isthiseqp;

		private Text NextTet;

		private bool isAdd = false;

		private float addTime = 0.5f;

		private float rateTime = 0f;

		private int addType;

		public a3_BagItemData curChooseEquip;

		private int curChooseAttType;

		private int curChooseAttTag = 6;

		private int curChooseInheritTag = 1;

		private int curChooseInheritUseTag = 1;

		private uint curInheritId1 = 0u;

		private uint curInheritId2 = 0u;

		public uint curInheritId3 = 0u;

		private uint curBaoshiId = 0u;

		private int outKey;

		private uint hcbaoshiId = 0u;

		private uint hcid = 0u;

		private GameObject isthis;

		private Text hcMoney;

		private Text zxMoney;

		public static a3_equip instance;

		private int addTyp = 0;

		private int tabIdx;

		private List<GameObject> tab = new List<GameObject>();

		private Dictionary<int, GameObject> baoshi_con = new Dictionary<int, GameObject>();

		private Dictionary<int, GameObject> baoshi_con2 = new Dictionary<int, GameObject>();

		private GameObject geticon;

		private Animator ani_strength_2;

		private Animator ani_strength_3;

		private Animator ani_inherit;

		private Animator ani_add_att;

		private Animator ani_add_att1;

		private Animator ani_Advance_1;

		private Animator ani_Advance_2;

		private Animator ani_Advance_3;

		private Dictionary<int, GameObject> eqp_obj = new Dictionary<int, GameObject>();

		private float time = 0.2f;

		private string str = "";

		public int tabIndex
		{
			get
			{
				return this.tabIdx;
			}
			set
			{
				this.onTab(value);
			}
		}

		public int tabIndex0
		{
			get
			{
				return this.tabCtrl1.getSeletedIndex();
			}
			set
			{
				this.tabCtrl1.setSelectedIndex(value, false);
				this.onTab1(this.tabCtrl1);
			}
		}

		public override void init()
		{
			this.addIconHintImage();
			this.equipPanel = new GameObject[2];
			this.equipPanel[0] = base.transform.FindChild("panel_equiped").gameObject;
			this.equipPanel[1] = base.transform.FindChild("panel_unequiped").gameObject;
			this.itemListView = base.transform.FindChild("panel_unequiped/equip_info/bag_scroll/scroll_view/contain").gameObject;
			this.item_Parent = this.itemListView.GetComponent<GridLayoutGroup>();
			this.isthiseqp = this.equipPanel[0].transform.FindChild("equip_info/scrollview/this");
			this.infoPanel = new GameObject[8];
			this.infoPanel[0] = base.transform.FindChild("panel_tab1").gameObject;
			this.infoPanel[1] = base.transform.FindChild("panel_tab2").gameObject;
			this.infoPanel[2] = base.transform.FindChild("panel_tab3").gameObject;
			this.infoPanel[3] = base.transform.FindChild("panel_tab4").gameObject;
			this.infoPanel[4] = base.transform.FindChild("panel_tab5").gameObject;
			this.infoPanel[5] = base.transform.FindChild("panel_tab6").gameObject;
			this.infoPanel[6] = base.transform.FindChild("panel_tab7").gameObject;
			this.infoPanel[7] = base.transform.FindChild("panel_tab8").gameObject;
			this.tabCtrl1 = new TabControl();
			this.tabCtrl1.onClickHanle = new Action<TabControl>(this.onTab1);
			this.tabCtrl1.create(base.getGameObjectByPath("panelTab1"), base.gameObject, 0, 0, false);
			for (int i = 0; i < base.transform.FindChild("panelTab2/con").childCount; i++)
			{
				this.tab.Add(base.transform.FindChild("panelTab2/con").GetChild(i).gameObject);
			}
			for (int j = 0; j < this.tab.Count; j++)
			{
				int tag = j;
				new BaseButton(this.tab[j].transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.onTab(tag);
				};
			}
			this.shellcon = this.equipPanel[1].transform.FindChild("equip_info/bag_scroll/scroll_view/contain").gameObject;
			this.shellcon_baoshi = this.equipPanel[1].transform.FindChild("equip_info/bag_scroll_baoshi/scroll_view/contain").gameObject;
			this.scrollControler2 = new ScrollControler();
			ScrollRect component = this.equipPanel[1].transform.FindChild("equip_info/scroll").GetComponent<ScrollRect>();
			this.scrollControler2.create(component, 4);
			this.isthis = this.infoPanel[7].transform.FindChild("bg/isthis").gameObject;
			this.canUp = this.infoPanel[0].transform.FindChild("canUp").gameObject;
			this.ismaxlvl = this.infoPanel[0].transform.FindChild("maxlvl").gameObject;
			this.ani_strength_2 = this.infoPanel[0].transform.FindChild("ani_success").GetComponent<Animator>();
			this.ani_strength_3 = this.infoPanel[0].transform.FindChild("ani_fail").GetComponent<Animator>();
			this.ani_inherit = this.infoPanel[2].transform.FindChild("ani_cc").GetComponent<Animator>();
			this.ani_add_att = this.infoPanel[5].transform.FindChild("zj_levelUP").GetComponent<Animator>();
			this.ani_add_att1 = this.infoPanel[5].transform.FindChild("ani_zj").GetComponent<Animator>();
			this.ani_Advance_1 = this.infoPanel[3].transform.FindChild("ani_lightning").GetComponent<Animator>();
			this.ani_Advance_2 = this.infoPanel[3].transform.FindChild("ani_success").GetComponent<Animator>();
			this.ani_Advance_3 = this.infoPanel[3].transform.FindChild("ani_fail").GetComponent<Animator>();
			this.qhDashi_text = this.infoPanel[0].transform.FindChild("qhdashi/Text").GetComponent<Text>();
			this.QHDashi = base.transform.FindChild("qhdashi_info").gameObject;
			this.Inbaoshi = base.transform.FindChild("Inbaoshi").gameObject;
			BaseButton baseButton = new BaseButton(this.Inbaoshi.transform.FindChild("close"), 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.Inbaoshi.SetActive(false);
				this.curBaoshiId = 0u;
			};
			this.NextTet = this.infoPanel[0].transform.FindChild("NextTet/Text").GetComponent<Text>();
			this.add_Change = this.infoPanel[5].transform.FindChild("add_lvl").GetComponent<Image>();
			this.hcMoney = this.infoPanel[6].transform.FindChild("do/money").GetComponent<Text>();
			this.zxMoney = this.infoPanel[7].transform.FindChild("do/money").GetComponent<Text>();
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onclose);
			BaseButton baseButton3 = new BaseButton(this.infoPanel[0].transform.FindChild("btn_do"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onStrength);
			BaseButton baseButton4 = new BaseButton(this.infoPanel[1].transform.FindChild("btn_do"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onChange);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("change_panel/btn_yes"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onYesChange);
			BaseButton baseButton6 = new BaseButton(base.transform.FindChild("change_panel/btn_no"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onNoChange);
			BaseButton baseButton7 = new BaseButton(this.infoPanel[3].transform.FindChild("btn_do"), 1, 1);
			baseButton7.onClick = new Action<GameObject>(this.onAdvance);
			BaseButton baseButton8 = new BaseButton(this.infoPanel[5].transform.FindChild("btn_do"), 1, 1);
			baseButton8.onClick = new Action<GameObject>(this.onAddAttr);
			BaseButton baseButton9 = new BaseButton(this.infoPanel[2].transform.FindChild("btn_do"), 1, 1);
			baseButton9.onClick = new Action<GameObject>(this.onInherit);
			BaseButton baseButton10 = new BaseButton(this.infoPanel[0].transform.FindChild("qhdashi"), 1, 1);
			baseButton10.onClick = new Action<GameObject>(this.onOpenDashi);
			BaseButton baseButton11 = new BaseButton(this.infoPanel[6].transform.FindChild("do"), 1, 1);
			baseButton11.onClick = new Action<GameObject>(this.onHeCheng);
			new BaseButton(this.infoPanel[7].transform.FindChild("do"), 1, 1).onClick = new Action<GameObject>(this.onSendOut);
			new BaseButton(this.infoPanel[0].transform.FindChild("help_btn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.infoPanel[0].transform.FindChild("help").gameObject.SetActive(true);
			};
			new BaseButton(this.infoPanel[0].transform.FindChild("help/close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.infoPanel[0].transform.FindChild("help").gameObject.SetActive(false);
			};
			new BaseButton(this.infoPanel[7].transform.FindChild("help_btn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.infoPanel[7].transform.FindChild("help").gameObject.SetActive(true);
			};
			new BaseButton(this.infoPanel[7].transform.FindChild("help/close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.infoPanel[7].transform.FindChild("help").gameObject.SetActive(false);
			};
			for (int k = 0; k < 3; k++)
			{
				this.baoshi_con[k] = this.infoPanel[7].transform.FindChild("bg/icon" + k).gameObject;
			}
			for (int l = 0; l < 5; l++)
			{
				this.baoshi_con2[l] = this.infoPanel[6].transform.FindChild("bg/icon" + l).gameObject;
			}
			for (int m = 1; m <= 10; m++)
			{
				this.eqp_obj[m] = this.equipPanel[0].transform.FindChild("equip_info/scrollview/con/eqp" + m).gameObject;
			}
			this.geticon = this.infoPanel[6].transform.FindChild("bg/Geticon").gameObject;
			this.initUi();
			a3_equip.instance = this;
			this.CheckLock();
		}

		public override void onShowed()
		{
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_EQUIP_STRENGTH, new Action<GameEvent>(this.onEquipStrength));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_CHANGE_ATT, new Action<GameEvent>(this.onChangeAtt));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_DO_CHANGE_ATT, new Action<GameEvent>(this.onDoChangeAtt));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_EQUIP_ADVANCE, new Action<GameEvent>(this.onEquipAdvance));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_EQUIP_GEM_UP, new Action<GameEvent>(this.onEquipGem));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_EQUIP_ADDATTR, new Action<GameEvent>(this.onEquipAtt));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_EQUIP_INHERIT, new Action<GameEvent>(this.onEquipInherit));
			UIClient.instance.addEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_BAOSHI, new Action<GameEvent>(this.onEquipBaoshi));
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_BAOSHI_HC, new Action<GameEvent>(this.onBaoshi_hc));
			base.transform.SetAsLastSibling();
			this.shot_eqp();
			bool flag = GRMap.GAME_CAMERA != null;
			if (flag)
			{
				GRMap.GAME_CAMERA.SetActive(false);
			}
			this.initEquipPanel();
			this.initBaoshiPanel();
			this.refreshScrollRect();
			this.outKey = -1;
			this.hcbaoshiId = 0u;
			this.hcid = 0u;
			this.refMask();
			bool flag2 = this.uiData != null;
			if (flag2)
			{
				uint num = (uint)this.uiData[0];
				this.onClickEquip(this.equipicon[num], num);
			}
			else
			{
				bool flag3 = this.equipicon.Count > 0;
				if (flag3)
				{
					this.onClickEquip(this.equipicon[this.equipicon.Keys.First<uint>()], this.equipicon.Keys.First<uint>());
				}
			}
			this.refreshMoney();
			this.refreshGold();
			this.refreshGift();
			this.onTab(0);
			this.refreshQHdashi();
		}

		public override void onClosed()
		{
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_EQUIP_STRENGTH, new Action<GameEvent>(this.onEquipStrength));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_CHANGE_ATT, new Action<GameEvent>(this.onChangeAtt));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_DO_CHANGE_ATT, new Action<GameEvent>(this.onDoChangeAtt));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_EQUIP_ADVANCE, new Action<GameEvent>(this.onEquipAdvance));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_EQUIP_GEM_UP, new Action<GameEvent>(this.onEquipGem));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_EQUIP_ADDATTR, new Action<GameEvent>(this.onEquipAtt));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_EQUIP_INHERIT, new Action<GameEvent>(this.onEquipInherit));
			UIClient.instance.removeEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_BAOSHI, new Action<GameEvent>(this.onEquipBaoshi));
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_BAOSHI_HC, new Action<GameEvent>(this.onBaoshi_hc));
			bool flag = GRMap.GAME_CAMERA != null;
			if (flag)
			{
				GRMap.GAME_CAMERA.SetActive(true);
			}
			this.Inbaoshi.SetActive(false);
			this.reset();
			this.isAdd = false;
			this.curInheritId1 = 0u;
			this.curInheritId2 = 0u;
		}

		private void initUi()
		{
			SXML sXML = XMLMgr.instance.GetSXML("item.intensify", "intensify_level==1");
			bool flag = sXML != null;
			if (flag)
			{
				for (int i = 0; i < 3; i++)
				{
					this.intensify_need_id[i] = uint.Parse(sXML.getString("intensify_material" + (i + 1)).Split(new char[]
					{
						','
					})[0]);
				}
			}
			BaseButton baseButton = new BaseButton(this.infoPanel[2].transform.FindChild("btn_close1"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onInheritRemove1);
			BaseButton baseButton2 = new BaseButton(this.infoPanel[2].transform.FindChild("btn_close2"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onInheritRemove2);
			for (int j = 1; j <= 3; j++)
			{
				int tag = j;
				BaseButton baseButton3 = new BaseButton(this.infoPanel[4].transform.FindChild("stone" + j + "/btn_do"), 1, 1);
				EventTriggerListener.Get(baseButton3.gameObject).onDown = delegate(GameObject go)
				{
					this.onGemClick(tag);
				};
				EventTriggerListener.Get(baseButton3.gameObject).onExit = delegate(GameObject go)
				{
					this.onGemClickExit(tag);
				};
			}
		}

		private void onTab1(TabControl t)
		{
			this.equipPanel[0].SetActive(false);
			this.equipPanel[1].SetActive(false);
			bool flag = t.getSeletedIndex() == 0;
			if (flag)
			{
				this.equipPanel[0].SetActive(true);
			}
			else
			{
				this.equipPanel[1].SetActive(true);
			}
		}

		private void onTab(int tag)
		{
			bool flag = this.curChooseEquip.id <= 0u;
			if (!flag)
			{
				bool flag2 = this.curInheritId1 > 0u || this.curInheritId2 > 0u;
				if (flag2)
				{
					this.curInheritId1 = 0u;
					this.curInheritId2 = 0u;
					this.refreshUnEquipItem();
				}
				this.curInheritId3 = this.curChooseEquip.id;
				this.tabIdx = tag;
				this.infoPanel[0].SetActive(false);
				this.infoPanel[1].SetActive(false);
				this.infoPanel[2].SetActive(false);
				this.infoPanel[3].SetActive(false);
				this.infoPanel[4].SetActive(false);
				this.infoPanel[5].SetActive(false);
				this.infoPanel[6].SetActive(false);
				this.infoPanel[7].SetActive(false);
				for (int i = 0; i < this.tab.Count; i++)
				{
					this.tab[i].GetComponent<Button>().interactable = true;
				}
				this.tab[this.tabIdx].GetComponent<Button>().interactable = false;
				this.infoPanel[this.tabIdx].SetActive(true);
				switch (this.tabIdx)
				{
				case 0:
					this.infoPanel[0].transform.FindChild("help").gameObject.SetActive(false);
					this.refreshStrength(this.curChooseEquip);
					this.refreshEquipItem(EQUIP_SHOW_TYPE.SHOW_COMMON);
					this.refeqptext();
					break;
				case 1:
					this.refrenshChange(this.curChooseEquip);
					this.refreshEquipItem(EQUIP_SHOW_TYPE.SHOW_COMMON);
					this.refeqptext();
					break;
				case 2:
					this.refreshInherit();
					this.refreshEquipItem(EQUIP_SHOW_TYPE.SHOW_COMMON);
					this.refeqptext();
					break;
				case 3:
					this.refreshAdvance(this.curChooseEquip);
					this.refreshEquipItem(EQUIP_SHOW_TYPE.SHOW_COMMON);
					this.refeqptext();
					break;
				case 4:
					this.refreshGemAtt(this.curChooseEquip);
					this.refreshEquipItem(EQUIP_SHOW_TYPE.SHOW_COMMON);
					this.refeqptext();
					break;
				case 5:
					this.refreshAddAtt(this.curChooseEquip);
					this.refreshEquipItem(EQUIP_SHOW_TYPE.SHOW_COMMON);
					this.refeqptext();
					break;
				case 6:
					this.ref_hc_baoshiMoney();
					this.tabCtrl1.setSelectedIndex(1, false);
					this.onTab1(this.tabCtrl1);
					this.refreshEquipItem(EQUIP_SHOW_TYPE.SHOW_COMMON);
					this.refeqptext();
					break;
				case 7:
					this.infoPanel[7].transform.FindChild("help").gameObject.SetActive(false);
					this.refrenshChangeBaoshi();
					this.refreshEquipItem(EQUIP_SHOW_TYPE.SHOW_COMMON);
					this.refeqptext();
					break;
				}
				this.clear_baoshi();
				this.hcbaoshiId = 0u;
				this.hcid = 0u;
				this.Inbaoshi.SetActive(false);
				this.outKey = -1;
				this.refMask();
				this.refeqp();
			}
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIP);
		}

		private void Update()
		{
			bool flag = this.isAdd;
			if (flag)
			{
				this.addTime -= Time.deltaTime;
				bool flag2 = this.addTime < 0f;
				if (flag2)
				{
					this.time -= Time.deltaTime;
					bool flag3 = this.time < 0f;
					if (flag3)
					{
						this.rateTime += 0.05f;
						this.addTime = 0.5f - this.rateTime;
						this.sendGem(this.addType);
						this.time = 0.2f;
					}
				}
			}
		}

		private bool isCan(a3_BagItemData data1, a3_BagItemData data2)
		{
			bool result;
			foreach (int current in data2.equipdata.gem_att.Keys)
			{
				bool flag = data2.equipdata.gem_att[current] > data1.equipdata.gem_att[current];
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		private void rightInfo(a3_BagItemData data1, a3_BagItemData data2)
		{
			this.infoPanel[2].transform.FindChild("duibi/right").gameObject.SetActive(true);
			bool flag = data1.equipdata.intensify_lv <= data2.equipdata.intensify_lv;
			if (flag)
			{
				this.infoPanel[2].transform.FindChild("duibi/right/star/Text").GetComponent<Text>().text = " <color=#f90e0e>+" + data2.equipdata.intensify_lv + "</color>";
			}
			else
			{
				this.infoPanel[2].transform.FindChild("duibi/right/star/Text").GetComponent<Text>().text = "+" + data2.equipdata.intensify_lv;
			}
			bool flag2 = data1.equipdata.add_level < data2.equipdata.add_level;
			if (flag2)
			{
				this.infoPanel[2].transform.FindChild("duibi/right/4").GetComponent<Text>().text = " <color=#f90e0e>追加 + " + data2.equipdata.add_level + "</color>";
			}
			else
			{
				this.infoPanel[2].transform.FindChild("duibi/right/4").GetComponent<Text>().text = "追加 + " + data2.equipdata.add_level;
			}
			int num = 0;
			foreach (int current in data1.equipdata.gem_att.Keys)
			{
				num++;
				bool flag3 = data1.equipdata.gem_att[current] < data2.equipdata.gem_att[current];
				if (flag3)
				{
					this.infoPanel[2].transform.FindChild("duibi/right/" + num).GetComponent<Text>().text = " <color=#f90e0e>" + Globle.getAttrAddById(current, data2.equipdata.gem_att[current], true) + "</color>";
				}
				else
				{
					this.infoPanel[2].transform.FindChild("duibi/right/" + num).GetComponent<Text>().text = Globle.getAttrAddById(current, data2.equipdata.gem_att[current], true);
				}
			}
		}

		private void leftInfo_do(a3_BagItemData data)
		{
			this.infoPanel[2].transform.FindChild("duibi/left").gameObject.SetActive(true);
			this.infoPanel[2].transform.FindChild("duibi/left/star/Text").GetComponent<Text>().text = "+ 0";
			SXML sXML = XMLMgr.instance.GetSXML("item.gem", "item_id==" + data.tpid);
			SXML node = sXML.GetNode("gem_info", "stage_level==" + data.equipdata.stage);
			int num = 0;
			foreach (int current in data.equipdata.gem_att.Keys)
			{
				num++;
				Text component = this.infoPanel[2].transform.FindChild("duibi/left/" + num).GetComponent<Text>();
				component.text = Globle.getAttrAddById(current, 0, true);
			}
			this.infoPanel[2].transform.FindChild("duibi/left/4").GetComponent<Text>().text = "追加 + " + 0;
		}

		public void onOutEqp()
		{
			for (int i = 0; i < 3; i++)
			{
				this.infoPanel[7].transform.FindChild("bg/att" + i).gameObject.SetActive(false);
			}
			foreach (int current in this.baoshi_con.Keys)
			{
				int j = 0;
				while (j < this.baoshi_con[current].transform.childCount)
				{
					bool flag = this.baoshi_con[current].transform.GetChild(j).name == "local";
					if (flag)
					{
						this.baoshi_con[current].transform.GetChild(j).gameObject.SetActive(true);
					}
					else
					{
						bool flag2 = this.baoshi_con[current].transform.GetChild(j).name == "isthis";
						if (!flag2)
						{
							UnityEngine.Object.Destroy(this.baoshi_con[current].transform.GetChild(j).gameObject);
						}
					}
					IL_11A:
					j++;
					continue;
					goto IL_11A;
				}
			}
			Transform transform = this.infoPanel[7].transform.FindChild("bg/equipicon");
			bool flag3 = transform.childCount > 0;
			if (flag3)
			{
				for (int k = 0; k < transform.childCount; k++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(k).gameObject);
				}
			}
			this.isthis.gameObject.SetActive(false);
			this.equipPanel[1].transform.FindChild("equip_info/bag_scroll_baoshi").gameObject.SetActive(false);
			this.equipPanel[1].transform.FindChild("equip_info/bag_scroll").gameObject.SetActive(true);
			this.curInheritId3 = 0u;
			this.outKey = -1;
			this.refMask();
			this.ref_zx_baoshiMoney();
		}

		public void onClickEquip(GameObject go, uint id)
		{
			this.curChooseEquip = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id);
			this.refeqp_this();
			switch (this.tabIdx)
			{
			case 0:
				this.refreshStrength(this.curChooseEquip);
				break;
			case 1:
				this.refrenshChange(this.curChooseEquip);
				break;
			case 2:
			{
				bool flag = this.curInheritId1 > 0u && this.curInheritId2 > 0u;
				if (flag)
				{
					flytxt.instance.fly("已选", 0, default(Color), null);
				}
				else
				{
					bool flag2 = this.curInheritId1 > 0u;
					if (flag2)
					{
						bool flag3 = this.curInheritId1 != id;
						if (flag3)
						{
							bool flag4 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).confdata.equip_type != ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1).confdata.equip_type;
							if (flag4)
							{
								flytxt.instance.fly("只能传承相同部位的装备", 0, default(Color), null);
							}
							else
							{
								bool flag5 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).equipdata.stage > ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1).equipdata.stage;
								if (flag5)
								{
									flytxt.instance.fly("传承装备的精炼等级不能低于继承装备", 0, default(Color), null);
								}
								else
								{
									bool flag6 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).equipdata.stage == ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1).equipdata.stage && ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).equipdata.intensify_lv >= ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1).equipdata.intensify_lv;
									if (flag6)
									{
										flytxt.instance.fly("传承装备的强化需要高于继承装备", 0, default(Color), null);
									}
									else
									{
										bool flag7 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).equipdata.add_level > ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1).equipdata.add_level;
										if (flag7)
										{
											flytxt.instance.fly("传承装备的追加等级不能低于继承装备", 0, default(Color), null);
										}
										else
										{
											this.curInheritId2 = id;
											this.refreshInherit();
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag8 = this.curInheritId2 > 0u;
						if (flag8)
						{
							bool flag9 = this.curInheritId2 != id;
							if (flag9)
							{
								bool flag10 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).confdata.equip_type != ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId2).confdata.equip_type;
								if (flag10)
								{
									flytxt.instance.fly("只能传承相同部位的装备", 0, default(Color), null);
								}
								else
								{
									bool flag11 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).equipdata.stage > ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1).equipdata.stage;
									if (flag11)
									{
										flytxt.instance.fly("传承装备的精炼等级不能低于继承装备", 0, default(Color), null);
									}
									else
									{
										bool flag12 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).equipdata.stage == ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1).equipdata.stage && ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).equipdata.intensify_lv >= ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1).equipdata.intensify_lv;
										if (flag12)
										{
											flytxt.instance.fly("传承装备的强化需要高于继承装备", 0, default(Color), null);
										}
										else
										{
											bool flag13 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id).equipdata.add_level < ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId2).equipdata.add_level;
											if (flag13)
											{
												flytxt.instance.fly("传承装备的追加等级不能低于继承装备", 0, default(Color), null);
											}
											else
											{
												this.curInheritId1 = id;
												this.refreshInherit();
											}
										}
									}
								}
							}
						}
						else
						{
							this.curInheritId1 = id;
							this.refreshInherit();
						}
					}
				}
				this.refreshUnEquipItem();
				break;
			}
			case 3:
				this.refreshAdvance(this.curChooseEquip);
				break;
			case 4:
				this.refreshGemAtt(this.curChooseEquip);
				break;
			case 5:
				this.refreshAddAtt(this.curChooseEquip);
				break;
			case 7:
				this.curInheritId3 = id;
				this.refrenshChangeBaoshi();
				this.refeqp();
				break;
			}
			this.showIconHintImage();
		}

		private void refreshQHdashi()
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

		private void onOpenDashi(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_QHMASTER, null, false);
		}

		private void addtoget(a3_ItemData item)
		{
			bool flag = XMLMgr.instance.GetSXML("item.item", "id==" + item.tpid).GetNode("drop_info", "") == null;
			if (!flag)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(item);
				arrayList.Add(InterfaceMgr.A3_EQUIP);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMLACK, arrayList, false);
			}
		}

		public void fly(string txt, int tag)
		{
			switch (tag)
			{
			case 1:
			{
				GameObject gameObject = base.transform.FindChild("panel_tab5/flytext/txt_3_equ1").gameObject;
				GameObject txtclone = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				txtclone.gameObject.SetActive(true);
				txtclone.transform.FindChild("txt").GetComponent<Text>().text = txt;
				txtclone.transform.SetParent(gameObject.transform.parent, false);
				Tweener t = txtclone.transform.FindChild("txt").DOLocalMoveY(100f, 2.5f, false);
				t.OnComplete(delegate
				{
					UnityEngine.Object.Destroy(txtclone);
				});
				break;
			}
			case 2:
			{
				GameObject gameObject = base.transform.FindChild("panel_tab5/flytext/txt_3_equ2").gameObject;
				GameObject txtclone = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				txtclone.gameObject.SetActive(true);
				txtclone.transform.FindChild("txt").GetComponent<Text>().text = txt;
				txtclone.transform.SetParent(gameObject.transform.parent, false);
				Tweener t = txtclone.transform.FindChild("txt").DOLocalMoveY(100f, 2.5f, false);
				t.OnComplete(delegate
				{
					UnityEngine.Object.Destroy(txtclone);
				});
				break;
			}
			case 3:
			{
				GameObject gameObject = base.transform.FindChild("panel_tab5/flytext/txt_3_equ3").gameObject;
				GameObject txtclone = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				txtclone.gameObject.SetActive(true);
				txtclone.transform.FindChild("txt").GetComponent<Text>().text = txt;
				txtclone.transform.SetParent(gameObject.transform.parent, false);
				Tweener t = txtclone.transform.FindChild("txt").DOLocalMoveY(100f, 2.5f, false);
				t.OnComplete(delegate
				{
					UnityEngine.Object.Destroy(txtclone);
				});
				break;
			}
			}
		}

		private void onGemClick(int tag)
		{
			this.isAdd = true;
			this.rateTime = 0f;
			this.addTime = 0.5f;
			this.addTyp = tag;
			this.addType = tag;
			this.sendGem(tag);
		}

		private void sendGem(int tag)
		{
			SXML sXML = XMLMgr.instance.GetSXML("item.gem", "item_id==" + this.curChooseEquip.tpid);
			SXML node = sXML.GetNode("gem_info", "stage_level==" + this.curChooseEquip.equipdata.stage);
			List<SXML> nodeList = node.GetNodeList("gem_att", "");
			int @int = nodeList[tag - 1].getInt("att_type");
			BaseProxy<EquipProxy>.getInstance().sendGemUp(this.curChooseEquip.id, @int);
		}

		private void onGemClickExit(int tag)
		{
			this.isAdd = false;
		}

		private void onEquipStrength(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["success"];
			uint id = data["id"];
			this.curChooseEquip = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id);
			this.refreshStrength(this.curChooseEquip);
			bool flag2 = flag;
			if (flag2)
			{
				this.ani_strength_2.gameObject.SetActive(true);
				this.ani_strength_2.Play("ch_success", -1, 0f);
			}
			else
			{
				this.ani_strength_3.gameObject.SetActive(true);
				this.ani_strength_3.Play("ani_fail", -1, 0f);
			}
			this.refeqptext();
			this.refreshQHdashi();
			this.showIconHintImage();
		}

		private void onEquipAdvance(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["success"];
			bool flag2 = flag;
			if (flag2)
			{
				uint id = data["id"];
				this.curChooseEquip = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id);
				this.refreshAdvance(this.curChooseEquip);
				this.ani_Advance_1.gameObject.SetActive(true);
				this.ani_Advance_1.Play("jj_lightning", -1, 0f);
				this.refeqptext();
				this.ani_Advance_2.gameObject.SetActive(true);
				this.ani_Advance_2.Play("jj_success", -1, 0f);
			}
			else
			{
				this.ani_Advance_1.gameObject.SetActive(true);
				this.ani_Advance_1.Play("jj_lightning", -1, 0f);
				this.ani_Advance_3.gameObject.SetActive(true);
				this.ani_Advance_3.Play("jj_fail", -1, 0f);
				this.refreshAdvance(this.curChooseEquip);
			}
			this.showIconHintImage();
		}

		private void onEquipGem(GameEvent e)
		{
			Variant data = e.data;
			uint id = data["id"];
			this.curChooseEquip = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id);
			this.refreshGemAtt(this.curChooseEquip);
			Debug.Log(data.dump());
			this.fly("+" + data["att_value"], this.addTyp);
		}

		private void onEquipAtt(GameEvent e)
		{
			Variant data = e.data;
			uint id = data["id"];
			this.curChooseEquip = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id);
			this.refreshAddAtt(this.curChooseEquip);
			bool flag = data["leapfrog"];
			bool flag2 = flag;
			if (flag2)
			{
				this.ani_add_att.gameObject.SetActive(true);
				this.ani_add_att.Play("zj_levelup", -1, 0f);
			}
			bool flag3 = data["do_add_level_up"];
			if (flag3)
			{
				this.ani_add_att.gameObject.SetActive(true);
				this.ani_add_att.Play("zj_levelup", -1, 0f);
			}
			this.refeqptext();
			this.ani_add_att1.enabled = true;
			this.ani_add_att1.Play("equip_zj_head", -1, 0f);
			this.showIconHintImage();
		}

		private void onEquipInherit(GameEvent e)
		{
			Variant data = e.data;
			bool flag = ModelBase<a3_BagModel>.getInstance().getUnEquips().ContainsKey(this.curChooseEquip.id);
			if (flag)
			{
				this.curChooseEquip = ModelBase<a3_BagModel>.getInstance().getUnEquips()[this.curChooseEquip.id];
			}
			else
			{
				bool flag2 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(this.curChooseEquip.id);
				if (flag2)
				{
					this.curChooseEquip = ModelBase<a3_EquipModel>.getInstance().getEquips()[this.curChooseEquip.id];
				}
			}
			uint num = data["frm_eqpinfo"]["id"];
			uint num2 = data["to_eqpinfo"]["id"];
			this.curInheritId1 = 0u;
			this.curInheritId2 = 0u;
			this.refreshUnEquipItem();
			this.refreshInherit();
			this.refeqptext();
			this.ani_inherit.gameObject.SetActive(true);
			this.ani_inherit.Play("cc_arrow", -1, 0f);
			this.showIconHintImage();
		}

		private void onEquipBaoshi(GameEvent e)
		{
			bool flag = ModelBase<a3_BagModel>.getInstance().getUnEquips().ContainsKey(this.curChooseEquip.id);
			if (flag)
			{
				this.curChooseEquip = ModelBase<a3_BagModel>.getInstance().getUnEquips()[this.curChooseEquip.id];
			}
			else
			{
				bool flag2 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(this.curChooseEquip.id);
				if (flag2)
				{
					this.curChooseEquip = ModelBase<a3_EquipModel>.getInstance().getEquips()[this.curChooseEquip.id];
				}
			}
			this.refrenshChangeBaoshi();
			this.initBaoshiPanel();
			this.refeqptext();
			this.showIconHintImage();
		}

		private void onChangeAtt(GameEvent e)
		{
			Variant data = e.data;
			int num = data["type"];
			int num2 = data["value"];
			base.transform.FindChild("change_panel").gameObject.SetActive(true);
			string attrAddById = Globle.getAttrAddById(this.curChooseAttType, this.curChooseEquip.equipdata.subjoin_att[this.curChooseAttType], true);
			string attrAddById2 = Globle.getAttrAddById(num, num2, true);
			base.transform.FindChild("change_panel/old_type").GetComponent<Text>().text = attrAddById;
			base.transform.FindChild("change_panel/new_type").GetComponent<Text>().text = attrAddById2;
			SXML sXML = XMLMgr.instance.GetSXML("item.subjoin_att", "equip_level==" + this.curChooseEquip.confdata.equip_level);
			bool flag = sXML.GetNode("subjoin_att_info", "att_type==" + this.curChooseAttType).getInt("max") <= this.curChooseEquip.equipdata.subjoin_att[this.curChooseAttType];
			if (flag)
			{
				base.transform.FindChild("change_panel/old_max").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("change_panel/old_max").gameObject.SetActive(false);
			}
			bool flag2 = sXML.GetNode("subjoin_att_info", "att_type==" + num).getInt("max") <= num2;
			if (flag2)
			{
				base.transform.FindChild("change_panel/new_max").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("change_panel/new_max").gameObject.SetActive(false);
			}
			this.showIconHintImage();
		}

		private void onDoChangeAtt(GameEvent e)
		{
			Variant data = e.data;
			uint id = data["id"];
			this.curChooseEquip = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(id);
			this.refrenshChange(this.curChooseEquip);
			this.showIconHintImage();
		}

		public void refreshMoney()
		{
			Text component = base.transform.FindChild("top/money").GetComponent<Text>();
			component.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().money);
		}

		public void refreshGold()
		{
			Text component = base.transform.FindChild("top/stone").GetComponent<Text>();
			component.text = ModelBase<PlayerModel>.getInstance().gold.ToString();
		}

		public void refreshGift()
		{
			Text component = base.transform.FindChild("top/bindstone").GetComponent<Text>();
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

		public void refeqp()
		{
			bool flag = (this.curInheritId3 > 0u && this.tabIdx == 7) || this.tabIdx == 6;
			if (flag)
			{
				this.equipPanel[1].transform.FindChild("equip_info/bag_scroll_baoshi").gameObject.SetActive(true);
				this.equipPanel[1].transform.FindChild("equip_info/bag_scroll").gameObject.SetActive(false);
			}
			else
			{
				this.equipPanel[1].transform.FindChild("equip_info/bag_scroll_baoshi").gameObject.SetActive(false);
				this.equipPanel[1].transform.FindChild("equip_info/bag_scroll").gameObject.SetActive(true);
			}
		}

		private void shot_eqp()
		{
			for (int i = 1; i <= 10; i++)
			{
				this.equipPanel[0].transform.FindChild("equip_info/scrollview/con/eqp" + i).SetAsLastSibling();
			}
		}

		private GameObject createUnEquipItem(a3_BagItemData data)
		{
			GameObject gameObject = this.equipPanel[1].transform.FindChild("equip_info/scroll/item").gameObject;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject2.name = data.id.ToString();
			gameObject2.SetActive(true);
			gameObject2.transform.FindChild("name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getEquipNameInfo(data);
			Transform transform = gameObject2.transform.FindChild("icon");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				UnityEngine.Object.Destroy(transform.GetChild(0).gameObject);
			}
			GameObject gameObject3 = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 1f, false);
			gameObject3.transform.SetParent(transform, false);
			for (int i = 1; i <= 15; i++)
			{
				bool flag2 = i <= data.equipdata.intensify_lv;
				if (flag2)
				{
					gameObject2.transform.FindChild("stars/contain/star" + i).gameObject.SetActive(true);
				}
				else
				{
					gameObject2.transform.FindChild("stars/contain/star" + i).gameObject.SetActive(false);
				}
			}
			this.addEquipTipClick_bag(gameObject3, data);
			return gameObject2;
		}

		private void addEquipTipClick_bag(GameObject go, a3_BagItemData data)
		{
			go.transform.GetComponent<Button>().enabled = true;
			go.transform.GetComponent<Button>().onClick.AddListener(delegate
			{
				ArrayList arrayList = new ArrayList();
				a3_BagItemData equipByAll = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(data.id);
				arrayList.Add(equipByAll);
				arrayList.Add(equip_tip_type.Equip_tip);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
			});
		}

		private void addEquipclick(GameObject go, a3_BagItemData data)
		{
			go.transform.GetComponent<Button>().enabled = true;
			go.transform.GetComponent<Button>().onClick.AddListener(delegate
			{
				ArrayList arrayList = new ArrayList();
				a3_BagItemData equipByAll = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(data.id);
				arrayList.Add(equipByAll);
				arrayList.Add(equip_tip_type.tip_ForLook);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
			});
		}

		private void initEquipPanel()
		{
			this.equipicon.Clear();
			Dictionary<int, a3_BagItemData> equipsByType = ModelBase<a3_EquipModel>.getInstance().getEquipsByType();
			for (int i = 1; i <= 10; i++)
			{
				GameObject gameObject = this.equipPanel[0].transform.FindChild("equip_info/scrollview/con/eqp" + i).gameObject;
				bool flag = gameObject.transform.FindChild("equip").childCount > 0;
				if (flag)
				{
					UnityEngine.Object.Destroy(gameObject.transform.FindChild("equip").GetChild(0).gameObject);
				}
			}
			for (int j = 1; j <= 10; j++)
			{
				this.eqp_obj[j].transform.FindChild("has").gameObject.SetActive(false);
				this.eqp_obj[j].transform.FindChild("null").gameObject.SetActive(false);
				bool flag2 = equipsByType.ContainsKey(j);
				if (flag2)
				{
					this.eqp_obj[j].transform.FindChild("has").gameObject.SetActive(true);
					GameObject go = this.createEquipItem(equipsByType[j]);
					uint id = equipsByType[j].id;
					GameObject gameObject2 = this.equipPanel[0].transform.FindChild("equip_info/scrollview/con/eqp" + equipsByType[j].confdata.equip_type).gameObject;
					go.transform.SetParent(gameObject2.transform.FindChild("equip"), false);
					BaseButton baseButton = new BaseButton(this.eqp_obj[j].transform, 0, 1);
					baseButton.onClick = delegate(GameObject goo)
					{
						this.onClickEquip(go, id);
					};
					this.equipicon[id] = go;
					this.addEquipTipClick_bag(go, equipsByType[j]);
				}
				else
				{
					this.eqp_obj[j].transform.FindChild("null").gameObject.SetActive(true);
					BaseButton baseButton2 = new BaseButton(this.eqp_obj[j].transform, 0, 1);
					baseButton2.onClick = null;
					this.eqp_obj[j].transform.SetAsLastSibling();
				}
			}
			Dictionary<uint, a3_BagItemData> unEquips = ModelBase<a3_BagModel>.getInstance().getUnEquips();
			int num = 0;
			foreach (a3_BagItemData current in unEquips.Values)
			{
				GameObject gameObject3 = this.createUnEquipItem_bag(current);
				gameObject3.transform.SetParent(this.shellcon.transform.GetChild(num), false);
				num++;
				uint id2 = current.id;
				this.equipicon[id2] = gameObject3;
			}
			int count = unEquips.Count;
		}

		private string getEquipNameInfo(a3_BagItemData data)
		{
			string text = "";
			int quality = 1;
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + data.tpid);
			bool flag = sXML != null;
			if (flag)
			{
				text = sXML.getString("item_name");
				quality = sXML.getInt("quality");
			}
			string str = "";
			switch (data.equipdata.stage)
			{
			case 0:
				str = "普通的";
				break;
			case 1:
				str = "强化的";
				break;
			case 2:
				str = "打磨的";
				break;
			case 3:
				str = "优良的";
				break;
			case 4:
				str = "珍惜的";
				break;
			case 5:
				str = "祝福的";
				break;
			case 6:
				str = "完美的";
				break;
			case 7:
				str = "卓越的";
				break;
			case 8:
				str = "传说的";
				break;
			case 9:
				str = "神话的";
				break;
			case 10:
				str = "创世的";
				break;
			}
			text = str + text;
			string str2 = "";
			switch (data.equipdata.attribute)
			{
			case 1:
				str2 = "[风]";
				break;
			case 2:
				str2 = "[火]";
				break;
			case 3:
				str2 = "[光]";
				break;
			case 4:
				str2 = "[雷]";
				break;
			case 5:
				str2 = "[冰]";
				break;
			}
			text += str2;
			return Globle.getColorStrByQuality(text, quality);
		}

		private void refeqptext()
		{
			Dictionary<int, a3_BagItemData> equipsByType = ModelBase<a3_EquipModel>.getInstance().getEquipsByType();
			foreach (int current in equipsByType.Keys)
			{
				this.eqp_obj[current].transform.FindChild("has/lvl").GetComponent<Text>().text = string.Concat(new object[]
				{
					"精",
					equipsByType[current].equipdata.stage,
					"强",
					equipsByType[current].equipdata.intensify_lv
				});
				this.eqp_obj[current].transform.FindChild("has/zhui").GetComponent<Text>().text = "追" + equipsByType[current].equipdata.add_level;
				this.eqp_obj[current].transform.FindChild("has/name").GetComponent<Text>().text = this.getEquipNameInfo(equipsByType[current]);
				for (int i = 0; i < 3; i++)
				{
					this.eqp_obj[current].transform.FindChild("has/bs" + i).gameObject.SetActive(false);
				}
				foreach (int current2 in equipsByType[current].equipdata.baoshi.Keys)
				{
					this.eqp_obj[current].transform.FindChild("has/bs" + current2).gameObject.SetActive(true);
					bool flag = equipsByType[current].equipdata.baoshi[current2] > 0;
					if (flag)
					{
						SXML sXML = XMLMgr.instance.GetSXML("item", "");
						SXML node = sXML.GetNode("item", "id==" + equipsByType[current].equipdata.baoshi[current2]);
						string path = "icon/item/" + node.getString("icon_file");
						this.eqp_obj[current].transform.FindChild("has/bs" + current2).FindChild("icon").GetComponent<Image>().sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
						this.eqp_obj[current].transform.FindChild("has/bs" + current2).FindChild("icon").gameObject.SetActive(true);
					}
					else
					{
						this.eqp_obj[current].transform.FindChild("has/bs" + current2).FindChild("icon").gameObject.SetActive(false);
					}
				}
			}
		}

		private GameObject createEquipItem(a3_BagItemData data)
		{
			bool flag = data.confdata.equip_type != 8 && data.confdata.equip_type != 9 && data.confdata.equip_type != 10;
			GameObject gameObject;
			if (flag)
			{
				gameObject = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 1f, false);
			}
			else
			{
				gameObject = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 1f, false);
			}
			gameObject.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			return gameObject;
		}

		private void refreshEquipItem(EQUIP_SHOW_TYPE type)
		{
			Dictionary<uint, a3_BagItemData> equips = ModelBase<a3_EquipModel>.getInstance().getEquips();
			foreach (a3_BagItemData current in equips.Values)
			{
				bool flag = this.equipicon.ContainsKey(current.id);
				if (flag)
				{
					IconImageMgr.getInstance().refreshA3EquipIcon_byType(this.equipicon[current.id], current, type);
				}
			}
		}

		private void refreshUnEquipItem()
		{
			int num = 0;
			int num2 = 0;
			Dictionary<uint, a3_BagItemData> unEquips = ModelBase<a3_BagModel>.getInstance().getUnEquips();
			bool flag = this.curInheritId1 > 0u && this.curInheritId2 > 0u;
			if (!flag)
			{
				bool flag2 = this.curInheritId1 > 0u;
				if (flag2)
				{
					a3_BagItemData equipByAll = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1);
					foreach (a3_BagItemData current in unEquips.Values)
					{
						bool flag3 = current.confdata.job_limit == equipByAll.confdata.job_limit && current.confdata.equip_type == equipByAll.confdata.equip_type && current.id != equipByAll.id;
						if (flag3)
						{
							this.equipicon[current.id].SetActive(true);
							Transform child = this.shellcon.transform.GetChild(num2).GetChild(0);
							child.SetParent(this.equipicon[current.id].transform.parent, false);
							child.transform.localPosition = Vector3.zero;
							this.equipicon[current.id].transform.SetParent(this.shellcon.transform.GetChild(num2), false);
							num2++;
							num++;
						}
						else
						{
							this.equipicon[current.id].SetActive(false);
						}
					}
				}
				else
				{
					bool flag4 = this.curInheritId2 > 0u;
					if (flag4)
					{
						a3_BagItemData equipByAll2 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId2);
						foreach (a3_BagItemData current2 in unEquips.Values)
						{
							bool flag5 = current2.confdata.job_limit == equipByAll2.confdata.job_limit && current2.confdata.equip_type == equipByAll2.confdata.equip_type && current2.id != equipByAll2.id;
							if (flag5)
							{
								this.equipicon[current2.id].SetActive(true);
								Transform child2 = this.shellcon.transform.GetChild(num2).GetChild(0);
								child2.SetParent(this.equipicon[current2.id].transform.parent, false);
								child2.transform.localPosition = Vector3.zero;
								this.equipicon[current2.id].transform.SetParent(this.shellcon.transform.GetChild(num2), false);
								num2++;
								num++;
							}
							else
							{
								this.equipicon[current2.id].SetActive(false);
							}
						}
					}
					else
					{
						for (int i = 0; i < this.shellcon.transform.childCount; i++)
						{
							bool flag6 = this.shellcon.transform.GetChild(i).transform.childCount > 0;
							if (!flag6)
							{
								break;
							}
							this.shellcon.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
							num++;
						}
					}
				}
			}
		}

		private GameObject createUnEquipItem_bag(a3_BagItemData data)
		{
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 1f, false);
			gameObject.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			this.addEquipTipClick_bag(gameObject, data);
			return gameObject;
		}

		protected void refreshScrollRect()
		{
			int childCount = this.itemListView.transform.childCount;
			bool flag = childCount <= 0;
			if (!flag)
			{
				float y = this.item_Parent.cellSize.y;
				int num = (int)Math.Ceiling((double)childCount / 5.0);
				RectTransform component = this.itemListView.GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(0f, (float)num * y);
			}
		}

		private void reset()
		{
			for (int i = 0; i < this.shellcon.transform.childCount; i++)
			{
				bool flag = this.shellcon.transform.GetChild(i).transform.childCount > 0;
				if (!flag)
				{
					break;
				}
				UnityEngine.Object.Destroy(this.shellcon.transform.GetChild(i).transform.GetChild(0).gameObject);
			}
		}

		private void refeqp_this()
		{
			bool flag = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(this.curChooseEquip.id);
			if (flag)
			{
				this.isthiseqp.gameObject.SetActive(true);
				this.isthiseqp.SetParent(this.eqp_obj[this.curChooseEquip.confdata.equip_type].transform, false);
				this.isthiseqp.localPosition = Vector3.zero;
			}
			else
			{
				this.isthiseqp.gameObject.SetActive(false);
			}
		}

		private void setStrength_NextTet(a3_BagItemData equip_data)
		{
			this.NextTet.transform.parent.gameObject.SetActive(true);
			SXML node = XMLMgr.instance.GetSXML("item.intensify", "intensify_level==" + (equip_data.equipdata.intensify_lv + 1)).GetNode("intensify_info", "itemid==" + equip_data.tpid);
			string[] array = node.getString("intensify_att").Split(new char[]
			{
				','
			});
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + equip_data.tpid);
			int @int = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + equip_data.equipdata.stage).getInt("extra");
			bool flag = array.Length > 1;
			if (flag)
			{
				int num = int.Parse(node.getString("intensify_att").Split(new char[]
				{
					','
				})[0]) * @int / 100;
				int num2 = int.Parse(node.getString("intensify_att").Split(new char[]
				{
					','
				})[1]) * @int / 100;
				this.NextTet.text = string.Concat(new object[]
				{
					"攻击力 ",
					num,
					"-",
					num2
				});
			}
			else
			{
				int num = int.Parse(node.getString("intensify_att").Split(new char[]
				{
					','
				})[0]) * @int / 100;
				this.NextTet.text = Globle.getAttrNameById(sXML.getInt("att_type")) + " + " + num;
			}
		}

		private void onStrength(GameObject go)
		{
			bool flag = this.curChooseEquip.equipdata.intensify_lv < 15;
			if (flag)
			{
				bool flag2 = this.Needobj.Count > 0;
				if (flag2)
				{
					foreach (a3_ItemData current in this.Needobj.Keys)
					{
						this.addtoget(current);
					}
				}
				BaseProxy<EquipProxy>.getInstance().sendStrengthEquip(this.curChooseEquip.id);
			}
			else
			{
				flytxt.instance.fly("强化已至最高级！", 0, default(Color), null);
			}
		}

		private void refreshStrength(a3_BagItemData data)
		{
			this.Needobj.Clear();
			this.ani_strength_2.gameObject.SetActive(false);
			this.ani_strength_3.gameObject.SetActive(false);
			this.canUp.SetActive(false);
			this.ismaxlvl.SetActive(false);
			this.infoPanel[0].transform.FindChild("name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getEquipNameInfo(data);
			Transform transform = this.infoPanel[0].transform.FindChild("icon");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
				}
			}
			GameObject gameObject = this.createEquipItem(data);
			gameObject.transform.FindChild("iconborder").gameObject.SetActive(false);
			gameObject.transform.FindChild("stars").gameObject.SetActive(false);
			gameObject.transform.FindChild("shuxing").gameObject.SetActive(false);
			gameObject.transform.FindChild("inlvl").gameObject.SetActive(false);
			gameObject.transform.SetParent(transform, false);
			this.addEquipclick(gameObject, data);
			for (int j = 1; j <= 15; j++)
			{
				bool flag2 = j <= data.equipdata.intensify_lv;
				if (flag2)
				{
					this.infoPanel[0].transform.FindChild("stars/star" + j).gameObject.SetActive(true);
				}
				else
				{
					this.infoPanel[0].transform.FindChild("stars/star" + j).gameObject.SetActive(false);
				}
			}
			SXML sXML = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + data.equipdata.stage);
			this.infoPanel[0].transform.FindChild("value2").GetComponent<Text>().text = sXML.getString("intensify_rate") + "%";
			SXML sXML2 = XMLMgr.instance.GetSXML("item.intensify", "intensify_level==" + (data.equipdata.intensify_lv + 1));
			SXML sXML3 = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + data.equipdata.stage);
			bool flag3 = sXML2 != null;
			if (flag3)
			{
				for (int k = 0; k < 3; k++)
				{
					int num = int.Parse(sXML2.getString("intensify_material" + (k + 1)).Split(new char[]
					{
						','
					})[1]) + sXML3.getInt("i_extra");
					int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.intensify_need_id[k]);
					bool flag4 = itemNumByTpid >= num;
					string text;
					if (flag4)
					{
						text = string.Concat(new object[]
						{
							"<color=#ffffff>",
							itemNumByTpid,
							"</color>/",
							num
						});
					}
					else
					{
						text = string.Concat(new object[]
						{
							"<color=#f90e0e>",
							itemNumByTpid,
							"</color>/",
							num
						});
						this.Needobj[ModelBase<a3_BagModel>.getInstance().getItemDataById(this.intensify_need_id[k])] = num;
					}
					this.infoPanel[0].transform.FindChild("need_item/item" + (k + 1) + "/num").GetComponent<Text>().text = text;
				}
				this.infoPanel[0].transform.FindChild("btn_do/value1").GetComponent<Text>().text = sXML2.getString("intensify_money");
				this.infoPanel[0].transform.FindChild("btn_do").gameObject.SetActive(true);
				this.infoPanel[0].transform.FindChild("btn_goUP").gameObject.SetActive(false);
				this.setStrength_NextTet(data);
			}
			else
			{
				this.infoPanel[0].transform.FindChild("need_item/item1/num").GetComponent<Text>().text = "<color=#ffffff>" + ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.intensify_need_id[0]) + "</color>/0";
				bool flag5 = data.equipdata.stage < 10;
				if (flag5)
				{
					this.canUp.SetActive(true);
					this.infoPanel[0].transform.FindChild("btn_goUP/gotext").gameObject.SetActive(true);
					this.infoPanel[0].transform.FindChild("btn_goUP/maxtext").gameObject.SetActive(false);
					this.infoPanel[0].transform.FindChild("btn_goUP").GetComponent<Button>().interactable = true;
				}
				else
				{
					bool flag6 = data.equipdata.stage >= 10;
					if (flag6)
					{
						this.ismaxlvl.SetActive(true);
						this.infoPanel[0].transform.FindChild("btn_goUP/gotext").gameObject.SetActive(false);
						this.infoPanel[0].transform.FindChild("btn_goUP/maxtext").gameObject.SetActive(true);
						this.infoPanel[0].transform.FindChild("btn_goUP").GetComponent<Button>().interactable = false;
					}
				}
				this.NextTet.transform.parent.gameObject.SetActive(false);
				this.infoPanel[0].transform.FindChild("btn_do").gameObject.SetActive(false);
				this.infoPanel[0].transform.FindChild("btn_goUP").gameObject.SetActive(true);
				new BaseButton(this.infoPanel[0].transform.FindChild("btn_goUP"), 1, 1).onClick = delegate(GameObject go)
				{
					this.onTab(3);
				};
			}
		}

		private void refreshAdvance(a3_BagItemData data)
		{
			this.ani_Advance_1.gameObject.SetActive(false);
			this.ani_Advance_2.gameObject.SetActive(false);
			this.ani_Advance_3.gameObject.SetActive(false);
			this.Needobj.Clear();
			this.str = "";
			Transform transform = this.infoPanel[3].transform.FindChild("icon1");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
				}
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3EquipIcon(data, 1f, false);
			gameObject.transform.SetParent(transform, false);
			this.addEquipclick(gameObject, data);
			this.infoPanel[3].transform.FindChild("txt_lv").GetComponent<Text>().text = data.equipdata.stage + "阶";
			bool flag2 = data.equipdata.intensify_lv >= 15;
			if (flag2)
			{
				this.infoPanel[3].transform.FindChild("limit0/yes").gameObject.SetActive(true);
				this.infoPanel[3].transform.FindChild("limit0/no").gameObject.SetActive(false);
			}
			else
			{
				this.infoPanel[3].transform.FindChild("limit0/yes").gameObject.SetActive(false);
				this.infoPanel[3].transform.FindChild("limit0/no").gameObject.SetActive(true);
			}
			SXML sXML = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + data.equipdata.stage);
			SXML node = sXML.GetNode("stage_info", "itemid==" + data.tpid);
			List<SXML> nodeList = node.GetNodeList("stage_material", "");
			bool flag3 = nodeList != null;
			if (flag3)
			{
				string text = string.Concat(new object[]
				{
					"角色等级：",
					node.getInt("zhuan"),
					"转",
					node.getInt("level"),
					"级"
				});
				int @int = node.getInt("stage_money");
				this.infoPanel[3].transform.FindChild("limit1").GetComponent<Text>().text = text;
				bool flag4 = @int > 0;
				if (flag4)
				{
					this.infoPanel[3].transform.FindChild("btn_do/money").gameObject.SetActive(true);
					this.infoPanel[3].transform.FindChild("btn_do/maxlvl").gameObject.SetActive(false);
					this.infoPanel[3].transform.FindChild("btn_do/money").GetComponent<Text>().text = @int.ToString();
					this.infoPanel[3].transform.FindChild("btn_do").GetComponent<Button>().interactable = true;
				}
				else
				{
					this.infoPanel[3].transform.FindChild("btn_do/money").gameObject.SetActive(false);
					this.infoPanel[3].transform.FindChild("btn_do/maxlvl").gameObject.SetActive(true);
					this.infoPanel[3].transform.FindChild("btn_do").GetComponent<Button>().interactable = false;
				}
				bool flag5 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)node.getInt("zhuan"));
				if (flag5)
				{
					this.infoPanel[3].transform.FindChild("limit1/yes").gameObject.SetActive(true);
					this.infoPanel[3].transform.FindChild("limit1/no").gameObject.SetActive(false);
				}
				else
				{
					bool flag6 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)node.getInt("zhuan"));
					if (flag6)
					{
						bool flag7 = (ulong)ModelBase<PlayerModel>.getInstance().lvl >= (ulong)((long)node.getInt("level"));
						if (flag7)
						{
							this.infoPanel[3].transform.FindChild("limit1/yes").gameObject.SetActive(true);
							this.infoPanel[3].transform.FindChild("limit1/no").gameObject.SetActive(false);
						}
						else
						{
							this.infoPanel[3].transform.FindChild("limit1/yes").gameObject.SetActive(false);
							this.infoPanel[3].transform.FindChild("limit1/no").gameObject.SetActive(true);
						}
					}
					else
					{
						this.infoPanel[3].transform.FindChild("limit1/yes").gameObject.SetActive(false);
						this.infoPanel[3].transform.FindChild("limit1/no").gameObject.SetActive(true);
					}
				}
				bool flag8 = (ulong)ModelBase<PlayerModel>.getInstance().money < (ulong)((long)node.getInt("stage_money"));
				if (flag8)
				{
				}
				int num = 0;
				this.infoPanel[3].transform.FindChild("limit0").gameObject.SetActive(true);
				this.infoPanel[3].transform.FindChild("limit1").gameObject.SetActive(true);
				this.infoPanel[3].transform.FindChild("limit2").gameObject.SetActive(false);
				this.infoPanel[3].transform.FindChild("limit3").gameObject.SetActive(false);
				this.infoPanel[3].transform.FindChild("limit4").gameObject.SetActive(false);
				foreach (SXML current in nodeList)
				{
					string item_name = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)current.getInt("item")).item_name;
					this.Needobj[ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)current.getInt("item"))] = current.getInt("num");
					bool flag9 = num == 0;
					if (flag9)
					{
						this.infoPanel[3].transform.FindChild("limit2").gameObject.SetActive(true);
						string text2 = item_name + "：" + current.getInt("num");
						this.infoPanel[3].transform.FindChild("limit2").GetComponent<Text>().text = text2;
						bool flag10 = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)current.getInt("item")) >= current.getInt("num");
						if (flag10)
						{
							this.infoPanel[3].transform.FindChild("limit2/yes").gameObject.SetActive(true);
							this.infoPanel[3].transform.FindChild("limit2/no").gameObject.SetActive(false);
						}
						else
						{
							this.infoPanel[3].transform.FindChild("limit2/yes").gameObject.SetActive(false);
							this.infoPanel[3].transform.FindChild("limit2/no").gameObject.SetActive(true);
						}
					}
					bool flag11 = num == 1;
					if (flag11)
					{
						string text3 = item_name + "：" + current.getInt("num");
						bool flag12 = current.getInt("item") == 1541;
						if (flag12)
						{
							this.infoPanel[3].transform.FindChild("limit3").gameObject.SetActive(true);
							this.infoPanel[3].transform.FindChild("limit3").GetComponent<Text>().text = text3;
							bool flag13 = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)current.getInt("item")) >= current.getInt("num");
							if (flag13)
							{
								this.infoPanel[3].transform.FindChild("limit3/yes").gameObject.SetActive(true);
								this.infoPanel[3].transform.FindChild("limit3/no").gameObject.SetActive(false);
							}
							else
							{
								this.infoPanel[3].transform.FindChild("limit3/yes").gameObject.SetActive(false);
								this.infoPanel[3].transform.FindChild("limit3/no").gameObject.SetActive(true);
							}
						}
						else
						{
							this.infoPanel[3].transform.FindChild("limit4").gameObject.SetActive(true);
							this.infoPanel[3].transform.FindChild("limit4").GetComponent<Text>().text = text3;
							bool flag14 = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)current.getInt("item")) >= current.getInt("num");
							if (flag14)
							{
								this.infoPanel[3].transform.FindChild("limit4/yes").gameObject.SetActive(true);
								this.infoPanel[3].transform.FindChild("limit4/no").gameObject.SetActive(false);
							}
							else
							{
								this.infoPanel[3].transform.FindChild("limit4/yes").gameObject.SetActive(false);
								this.infoPanel[3].transform.FindChild("limit4/no").gameObject.SetActive(true);
							}
						}
					}
					num++;
				}
			}
			bool activeSelf = this.infoPanel[3].transform.FindChild("limit0/no").gameObject.activeSelf;
			if (activeSelf)
			{
				this.Needobj.Clear();
				this.infoPanel[3].transform.FindChild("needStrength").gameObject.SetActive(true);
				this.infoPanel[3].transform.FindChild("btn_do").gameObject.SetActive(false);
				new BaseButton(this.infoPanel[3].transform.FindChild("needStrength"), 1, 1).onClick = delegate(GameObject go)
				{
					this.onTab(0);
				};
			}
			else
			{
				this.infoPanel[3].transform.FindChild("needStrength").gameObject.SetActive(false);
				this.infoPanel[3].transform.FindChild("btn_do").gameObject.SetActive(true);
				bool activeSelf2 = this.infoPanel[3].transform.FindChild("limit1/no").gameObject.activeSelf;
				if (activeSelf2)
				{
					this.str = "角色等级不足";
					this.Needobj.Clear();
				}
				else
				{
					bool flag15 = this.infoPanel[3].transform.FindChild("limit2").gameObject.activeSelf && this.infoPanel[3].transform.FindChild("limit2/no").gameObject.activeSelf;
					if (flag15)
					{
						this.str = "魔晶数量不足";
					}
					else
					{
						bool flag16 = this.infoPanel[3].transform.FindChild("limit3").gameObject.activeSelf && this.infoPanel[3].transform.FindChild("limit3/no").gameObject.activeSelf;
						if (flag16)
						{
							this.str = "神光徽记数量不足";
						}
						else
						{
							bool flag17 = this.infoPanel[3].transform.FindChild("limit4").gameObject.activeSelf && this.infoPanel[3].transform.FindChild("limit4/no").gameObject.activeSelf;
							if (flag17)
							{
								this.str = "秘法颗粒数量不足";
							}
							else
							{
								bool flag18 = (ulong)ModelBase<PlayerModel>.getInstance().money < (ulong)((long)node.getInt("stage_money"));
								if (flag18)
								{
									this.str = "金币不足";
								}
							}
						}
					}
				}
			}
		}

		private void onAdvance(GameObject go)
		{
			bool flag = this.Needobj.Count > 0;
			if (flag)
			{
				foreach (a3_ItemData current in this.Needobj.Keys)
				{
					bool flag2 = this.Needobj[current] > ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(current.tpid);
					if (flag2)
					{
						this.addtoget(current);
						break;
					}
				}
			}
			bool flag3 = this.str != "";
			if (flag3)
			{
				flytxt.instance.fly(this.str, 0, default(Color), null);
			}
			else
			{
				BaseProxy<EquipProxy>.getInstance().sendAdvance(this.curChooseEquip.id);
			}
		}

		private void onChange(GameObject go)
		{
			bool flag = this.curChooseAttType != -1;
			if (flag)
			{
				BaseProxy<EquipProxy>.getInstance().sendChangeAtt(this.curChooseEquip.id, this.curChooseAttType);
			}
			else
			{
				flytxt.instance.fly("没有选中的重铸类型！", 0, default(Color), null);
			}
		}

		private void onYesChange(GameObject go)
		{
			BaseProxy<EquipProxy>.getInstance().sendDoChangeAtt(this.curChooseEquip.id, true);
			base.transform.FindChild("change_panel").gameObject.SetActive(false);
		}

		private void onNoChange(GameObject go)
		{
			BaseProxy<EquipProxy>.getInstance().sendDoChangeAtt(this.curChooseEquip.id, false);
			base.transform.FindChild("change_panel").gameObject.SetActive(false);
		}

		private void refreshGemAtt(a3_BagItemData data)
		{
			this.infoPanel[4].transform.FindChild("name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getEquipNameInfo(data);
			Transform transform = this.infoPanel[4].transform.FindChild("icon");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
				}
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3EquipIcon(data, 1f, false);
			gameObject.transform.SetParent(transform, false);
			this.addEquipclick(gameObject, data);
			SXML sXML = XMLMgr.instance.GetSXML("item.gem", "item_id==" + data.tpid);
			SXML node = sXML.GetNode("gem_info", "stage_level==" + data.equipdata.stage);
			int num = 0;
			foreach (int current in data.equipdata.gem_att.Keys)
			{
				num++;
				this.infoPanel[4].transform.FindChild("attr" + num).GetComponent<Text>().text = " +" + data.equipdata.gem_att[current];
			}
			List<SXML> nodeList = node.GetNodeList("gem_att", "");
			this.infoPanel[4].transform.FindChild("max1").GetComponent<Text>().text = "MAX +" + nodeList[0].getString("att_max");
			this.infoPanel[4].transform.FindChild("max2").GetComponent<Text>().text = "MAX +" + nodeList[1].getString("att_max");
			this.infoPanel[4].transform.FindChild("max3").GetComponent<Text>().text = "MAX +" + nodeList[2].getString("att_max");
			num = 0;
			foreach (SXML current2 in nodeList)
			{
				this.infoPanel[4].transform.FindChild("stone" + (num + 1) + "/desc").GetComponent<Text>().text = current2.getString("desc");
				Image component = this.infoPanel[4].transform.FindChild("icon" + (num + 1)).GetComponent<Image>();
				bool flag2 = component.transform.childCount > 0;
				if (flag2)
				{
					UnityEngine.Object.Destroy(component.transform.GetChild(0).gameObject);
				}
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(current2.getUint("need_itemid"));
				component.sprite = (Resources.Load(itemDataById.file, typeof(Sprite)) as Sprite);
				int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(current2.getUint("need_itemid"));
				bool flag3 = itemNumByTpid < current2.getInt("need_value");
				string text;
				if (flag3)
				{
					text = string.Concat(new object[]
					{
						"<color=#f90e0e>",
						itemNumByTpid,
						"</color>/",
						current2.getInt("need_value")
					});
				}
				else
				{
					text = string.Concat(new object[]
					{
						"<color=#ffffff>",
						itemNumByTpid,
						"</color>/",
						current2.getInt("need_value")
					});
				}
				this.infoPanel[4].transform.FindChild("stone" + (num + 1) + "/num").GetComponent<Text>().text = text;
				num++;
				float @float = current2.getFloat("att_max");
				this.infoPanel[4].transform.FindChild("bar" + num).GetComponent<Image>().fillAmount = (float)data.equipdata.gem_att[current2.getInt("att_type")] / @float;
				this.infoPanel[4].transform.FindChild("top" + num).transform.localPosition = new Vector3((float)(-370 + num * 170), (float)(185 * data.equipdata.gem_att[current2.getInt("att_type")]) / @float - 230f, 0f);
			}
		}

		private void refrenshChange(a3_BagItemData data)
		{
			this.infoPanel[1].transform.FindChild("name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getEquipNameInfo(data);
			Transform transform = this.infoPanel[1].transform.FindChild("icon");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
				}
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3EquipIcon(data, 1f, false);
			gameObject.transform.SetParent(transform, false);
			this.addEquipclick(gameObject, data);
			SXML sXML = XMLMgr.instance.GetSXML("item.subjoin_att", "equip_level==" + data.confdata.equip_level);
			this.infoPanel[1].transform.FindChild("btn_do/need_money").GetComponent<Text>().text = sXML.getString("recasting_money");
			int j = 0;
			this.curChooseAttType = -1;
			int num = this.curChooseAttTag;
			foreach (int current in data.equipdata.subjoin_att.Keys)
			{
				j++;
				Toggle component = this.infoPanel[1].transform.FindChild("attr_info/Toggle" + j).GetComponent<Toggle>();
				component.gameObject.SetActive(true);
				component.onValueChanged.RemoveAllListeners();
				int id = current;
				int tag = j;
				component.onValueChanged.AddListener(delegate(bool isOn)
				{
					this.curChooseAttType = id;
					this.curChooseAttTag = tag;
				});
				component.transform.FindChild("Label").GetComponent<Text>().text = Globle.getAttrAddById(current, data.equipdata.subjoin_att[current], true);
				SXML node = sXML.GetNode("subjoin_att_info", "att_type==" + current);
				bool flag2 = node.getInt("max") <= data.equipdata.subjoin_att[current];
				if (flag2)
				{
					component.transform.FindChild("Max").gameObject.SetActive(true);
				}
				else
				{
					component.transform.FindChild("Max").gameObject.SetActive(false);
				}
				bool flag3 = j == 1 && data.equipdata.subjoin_att.Count < num;
				if (flag3)
				{
					component.isOn = true;
					this.curChooseAttType = id;
					this.curChooseAttTag = 1;
				}
				else
				{
					bool flag4 = j == num;
					if (flag4)
					{
						component.isOn = true;
						this.curChooseAttType = id;
						this.curChooseAttTag = j;
					}
					else
					{
						component.isOn = false;
					}
				}
			}
			for (j++; j <= 5; j++)
			{
				Toggle component2 = this.infoPanel[1].transform.FindChild("attr_info/Toggle" + j).GetComponent<Toggle>();
				component2.gameObject.SetActive(false);
			}
		}

		private string getstr(int type)
		{
			string result = "";
			switch (type)
			{
			case 1:
				result = "【风】";
				break;
			case 2:
				result = "【火】";
				break;
			case 3:
				result = "【光】";
				break;
			case 4:
				result = "【雷】";
				break;
			case 5:
				result = "【冰】";
				break;
			}
			return result;
		}

		private void rightInfo_do(a3_BagItemData data1, a3_BagItemData data2)
		{
			this.infoPanel[2].transform.FindChild("duibi/right").gameObject.SetActive(true);
			this.infoPanel[2].transform.FindChild("duibi").gameObject.SetActive(true);
			this.infoPanel[2].transform.FindChild("duibi/right/1").GetComponent<Text>().text = string.Concat(new object[]
			{
				Globle.getColorStrByQuality(string.Concat(new object[]
				{
					data2.confdata.item_name,
					this.getstr(data2.equipdata.attribute),
					data2.equipdata.stage,
					"阶"
				}), data2.confdata.quality),
				"<color=#00FF00>（+",
				data1.equipdata.stage - data2.equipdata.stage,
				"阶）</color>"
			});
			bool flag = data1.equipdata.intensify_lv < data2.equipdata.intensify_lv;
			if (flag)
			{
				this.infoPanel[2].transform.FindChild("duibi/right/2").GetComponent<Text>().text = string.Concat(new object[]
				{
					"强化等级 + ",
					data2.equipdata.intensify_lv,
					"<color=#f90e0e>（-",
					data2.equipdata.intensify_lv - data1.equipdata.intensify_lv,
					"）</color>"
				});
			}
			else
			{
				this.infoPanel[2].transform.FindChild("duibi/right/2").GetComponent<Text>().text = string.Concat(new object[]
				{
					"强化等级 + ",
					data2.equipdata.intensify_lv,
					"<color=#00FF00>（+",
					data1.equipdata.intensify_lv - data2.equipdata.intensify_lv,
					"）</color>"
				});
			}
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + data2.tpid);
			int id = int.Parse(sXML.getString("add_atttype").Split(new char[]
			{
				','
			})[0]);
			int num = int.Parse(sXML.getString("add_atttype").Split(new char[]
			{
				','
			})[1]) * data2.equipdata.add_level;
			SXML sXML2 = XMLMgr.instance.GetSXML("item.item", "id==" + data1.tpid);
			int num2 = int.Parse(sXML2.getString("add_atttype").Split(new char[]
			{
				','
			})[1]) * data1.equipdata.add_level;
			int num3 = data2.confdata.add_basiclevel * data2.equipdata.stage * data2.confdata.equip_level;
			int num4 = int.Parse(sXML.getString("add_atttype").Split(new char[]
			{
				','
			})[1]) * num3;
			bool flag2 = data1.equipdata.add_level > num3;
			if (flag2)
			{
				this.infoPanel[2].transform.FindChild("duibi/right/3").GetComponent<Text>().text = string.Concat(new object[]
				{
					"追加 ",
					Globle.getAttrAddById(id, num, true),
					"<color=#00FF00>（+",
					num4 - num,
					"）</color>"
				});
			}
			else
			{
				this.infoPanel[2].transform.FindChild("duibi/right/3").GetComponent<Text>().text = string.Concat(new object[]
				{
					"追加 ",
					Globle.getAttrAddById(id, num, true),
					"<color=#00FF00>（+",
					num2 - num,
					"）</color>"
				});
			}
		}

		private void leftInfo(a3_BagItemData data)
		{
			this.infoPanel[2].transform.FindChild("duibi/left").gameObject.SetActive(true);
			this.infoPanel[2].transform.FindChild("duibi/left/1").GetComponent<Text>().text = string.Concat(new object[]
			{
				data.confdata.item_name,
				this.getstr(data.equipdata.attribute),
				data.equipdata.stage,
				"阶"
			});
			this.infoPanel[2].transform.FindChild("duibi/left/1").GetComponent<Text>().color = Globle.getColorByQuality(data.confdata.quality);
			this.infoPanel[2].transform.FindChild("duibi/left/2").GetComponent<Text>().text = "强化等级 + " + data.equipdata.intensify_lv;
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + data.tpid);
			int id = int.Parse(sXML.getString("add_atttype").Split(new char[]
			{
				','
			})[0]);
			int value = int.Parse(sXML.getString("add_atttype").Split(new char[]
			{
				','
			})[1]) * data.equipdata.add_level;
			this.infoPanel[2].transform.FindChild("duibi/left/3").GetComponent<Text>().text = "追加" + Globle.getAttrAddById(id, value, true);
		}

		private void refreshInherit()
		{
			this.ani_inherit.gameObject.SetActive(false);
			this.infoPanel[2].transform.FindChild("cctext").gameObject.SetActive(true);
			this.infoPanel[2].transform.FindChild("jctext").gameObject.SetActive(true);
			Transform transform = this.infoPanel[2].transform.FindChild("icon1");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				UnityEngine.Object.Destroy(transform.GetChild(0).gameObject);
			}
			Transform transform2 = this.infoPanel[2].transform.FindChild("icon2");
			bool flag2 = transform2.childCount > 0;
			if (flag2)
			{
				UnityEngine.Object.Destroy(transform2.GetChild(0).gameObject);
			}
			bool flag3 = this.curInheritId1 > 0u && this.curInheritId2 > 0u;
			if (flag3)
			{
				this.infoPanel[2].transform.FindChild("cctext").gameObject.SetActive(false);
				this.infoPanel[2].transform.FindChild("jctext").gameObject.SetActive(false);
				a3_BagItemData equipByAll = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1);
				GameObject gameObject = IconImageMgr.getInstance().createA3EquipIcon(equipByAll, 1f, false);
				gameObject.transform.SetParent(transform, false);
				this.addEquipclick(gameObject, equipByAll);
				a3_BagItemData equipByAll2 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId2);
				GameObject gameObject2 = IconImageMgr.getInstance().createA3EquipIcon(equipByAll2, 1f, false);
				gameObject2.transform.SetParent(transform2, false);
				this.addEquipclick(gameObject2, equipByAll2);
				this.infoPanel[2].transform.FindChild("btn_close1").gameObject.SetActive(true);
				this.infoPanel[2].transform.FindChild("btn_close2").gameObject.SetActive(true);
				this.infoPanel[2].transform.FindChild("btn_close1/name").GetComponent<Text>().text = equipByAll.confdata.item_name;
				this.infoPanel[2].transform.FindChild("btn_close2/name").GetComponent<Text>().text = equipByAll2.confdata.item_name;
				this.infoPanel[2].transform.FindChild("btn_do").GetComponent<Button>().interactable = true;
				SXML sXML = XMLMgr.instance.GetSXML("item.inheritance", "equip_stage==" + equipByAll.equipdata.stage);
				this.infoPanel[2].transform.FindChild("btn_do/need_money").GetComponent<Text>().text = sXML.getString("money");
				this.leftInfo(equipByAll);
				this.rightInfo_do(equipByAll, equipByAll2);
			}
			else
			{
				this.infoPanel[2].transform.FindChild("btn_do").GetComponent<Button>().interactable = false;
				this.infoPanel[2].transform.FindChild("btn_do/need_money").GetComponent<Text>().text = "0";
				bool flag4 = this.curInheritId1 > 0u;
				if (flag4)
				{
					this.infoPanel[2].transform.FindChild("cctext").gameObject.SetActive(false);
					a3_BagItemData equipByAll3 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId1);
					GameObject gameObject3 = IconImageMgr.getInstance().createA3EquipIcon(equipByAll3, 1f, false);
					gameObject3.transform.SetParent(transform, false);
					this.leftInfo(equipByAll3);
					this.addEquipclick(gameObject3, equipByAll3);
					this.infoPanel[2].transform.FindChild("btn_close1").gameObject.SetActive(true);
					this.infoPanel[2].transform.FindChild("btn_close1/name").GetComponent<Text>().text = equipByAll3.confdata.item_name;
				}
				else
				{
					bool flag5 = this.curInheritId2 > 0u;
					if (flag5)
					{
						this.infoPanel[2].transform.FindChild("jctext").gameObject.SetActive(false);
						this.infoPanel[2].transform.FindChild("duibi/right").gameObject.SetActive(false);
						a3_BagItemData equipByAll4 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId2);
						GameObject gameObject4 = IconImageMgr.getInstance().createA3EquipIcon(equipByAll4, 1f, false);
						gameObject4.transform.SetParent(transform2, false);
						this.addEquipclick(gameObject4, equipByAll4);
						this.infoPanel[2].transform.FindChild("btn_close2").gameObject.SetActive(true);
						this.infoPanel[2].transform.FindChild("btn_close2/name").GetComponent<Text>().text = equipByAll4.confdata.item_name;
					}
					else
					{
						this.infoPanel[2].transform.FindChild("btn_close1").gameObject.SetActive(false);
						this.infoPanel[2].transform.FindChild("btn_close2").gameObject.SetActive(false);
						this.infoPanel[2].transform.FindChild("duibi/left").gameObject.SetActive(false);
						this.infoPanel[2].transform.FindChild("duibi/right").gameObject.SetActive(false);
					}
				}
			}
		}

		private void onInheritRemove1(GameObject go)
		{
			this.curInheritId1 = 0u;
			Transform transform = this.infoPanel[2].transform.FindChild("icon1");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				UnityEngine.Object.Destroy(transform.GetChild(0).gameObject);
			}
			go.SetActive(false);
			this.infoPanel[2].transform.FindChild("btn_do").GetComponent<Button>().interactable = false;
			this.infoPanel[2].transform.FindChild("duibi/left").gameObject.SetActive(false);
			this.infoPanel[2].transform.FindChild("duibi/right").gameObject.SetActive(false);
			this.infoPanel[2].transform.FindChild("btn_do/need_money").GetComponent<Text>().text = "0";
			this.refreshUnEquipItem();
		}

		private void onInheritRemove2(GameObject go)
		{
			this.curInheritId2 = 0u;
			Transform transform = this.infoPanel[2].transform.FindChild("icon2");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				UnityEngine.Object.Destroy(transform.GetChild(0).gameObject);
			}
			go.SetActive(false);
			this.infoPanel[2].transform.FindChild("btn_do").GetComponent<Button>().interactable = false;
			this.infoPanel[2].transform.FindChild("duibi/right").gameObject.SetActive(false);
			this.infoPanel[2].transform.FindChild("btn_do/need_money").GetComponent<Text>().text = "0";
			this.refreshUnEquipItem();
		}

		private void onInherit(GameObject go)
		{
			bool flag = this.curChooseInheritUseTag == 1;
			if (flag)
			{
				BaseProxy<EquipProxy>.getInstance().sendInherit(this.curInheritId1, this.curInheritId2, this.curChooseInheritTag, false);
			}
			else
			{
				BaseProxy<EquipProxy>.getInstance().sendInherit(this.curInheritId1, this.curInheritId2, this.curChooseInheritTag, true);
			}
		}

		private void onAddAttr(GameObject go)
		{
			bool flag = this.Needobj.Count > 0;
			if (flag)
			{
				foreach (a3_ItemData current in this.Needobj.Keys)
				{
					this.addtoget(current);
				}
			}
			bool flag2 = this.curChooseEquip.equipdata.add_level < this.curChooseEquip.confdata.add_basiclevel * this.curChooseEquip.confdata.equip_level * this.curChooseEquip.equipdata.stage;
			if (flag2)
			{
				BaseProxy<EquipProxy>.getInstance().sendAddAttr(this.curChooseEquip.id);
			}
			else
			{
				bool flag3 = this.curChooseEquip.confdata.add_basiclevel * this.curChooseEquip.confdata.equip_level * this.curChooseEquip.equipdata.stage <= 0;
				if (flag3)
				{
					flytxt.instance.fly("未精炼装备不能追加", 0, default(Color), null);
				}
				else
				{
					bool flag4 = this.curChooseEquip.equipdata.add_level >= this.curChooseEquip.confdata.add_basiclevel * this.curChooseEquip.confdata.equip_level * 10;
					if (flag4)
					{
						flytxt.instance.fly("追加属性等级已达到最大", 0, default(Color), null);
					}
					else
					{
						flytxt.instance.fly("精练后可继续追加", 0, default(Color), null);
					}
				}
			}
		}

		private void refreshAddAtt(a3_BagItemData data)
		{
			this.Needobj.Clear();
			this.ani_add_att1.enabled = false;
			this.ani_add_att.gameObject.SetActive(false);
			this.infoPanel[5].transform.FindChild("name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getEquipNameInfo(data);
			Transform transform = this.infoPanel[5].transform.FindChild("icon");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
				}
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3EquipIcon(data, 1f, false);
			gameObject.transform.SetParent(transform, false);
			this.addEquipclick(gameObject, data);
			this.infoPanel[5].transform.FindChild("add_lv").GetComponent<Text>().text = string.Concat(new object[]
			{
				data.equipdata.add_level,
				"<color=#66FFFF>/",
				data.confdata.equip_level * data.equipdata.stage * 2,
				"</color>"
			});
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + data.tpid);
			int id = int.Parse(sXML.getString("add_atttype").Split(new char[]
			{
				','
			})[0]);
			int value = int.Parse(sXML.getString("add_atttype").Split(new char[]
			{
				','
			})[1]) * data.equipdata.add_level;
			this.infoPanel[5].transform.FindChild("add_value").GetComponent<Text>().text = "追加" + Globle.getAttrAddById(id, value, true);
			SXML sXML2 = XMLMgr.instance.GetSXML("item.add_att", "add_level==" + (data.equipdata.add_level + 1));
			uint tpid = data.tpid;
			int num = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid).add_basiclevel * data.equipdata.stage * data.confdata.equip_level;
			this.add_Change.fillAmount = (float)data.equipdata.add_level / (float)num;
			bool flag2 = sXML2 != null;
			if (flag2)
			{
				Image component = this.infoPanel[5].transform.FindChild("bar").GetComponent<Image>();
				component.fillAmount = (float)data.equipdata.add_exp / sXML2.getFloat("add_exp");
				uint @uint = sXML2.getUint("material_id");
				Transform transform2 = this.infoPanel[5].transform.FindChild("icon_need");
				bool flag3 = transform2.childCount > 0;
				if (flag3)
				{
					UnityEngine.Object.Destroy(transform2.GetChild(0).gameObject);
				}
				GameObject gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(@uint, false, -1, 1f, false, -1, 0, false, false, false, false);
				gameObject2.transform.SetParent(transform2, false);
				int @int = sXML2.getInt("material_num");
				Text component2 = this.infoPanel[5].transform.FindChild("need_num").GetComponent<Text>();
				bool flag4 = @int <= ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(@uint);
				if (flag4)
				{
					component2.color = Color.white;
				}
				else
				{
					component2.color = Color.red;
					this.Needobj[ModelBase<a3_BagModel>.getInstance().getItemDataById(@uint)] = @int;
				}
				component2.text = @int.ToString();
				this.infoPanel[5].transform.FindChild("btn_do/money").GetComponent<Text>().text = sXML2.getString("money");
			}
		}

		private void initBaoshiPanel()
		{
			for (int i = 0; i < this.shellcon_baoshi.transform.childCount; i++)
			{
				for (int j = 0; j < this.shellcon_baoshi.transform.GetChild(i).childCount; j++)
				{
					UnityEngine.Object.Destroy(this.shellcon_baoshi.transform.GetChild(i).GetChild(j).gameObject);
				}
				bool flag = this.shellcon_baoshi.transform.GetChild(i).childCount <= 0;
				if (flag)
				{
					break;
				}
			}
			Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
			int num = 0;
			using (Dictionary<uint, a3_BagItemData>.ValueCollection.Enumerator enumerator = items.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					a3_BagItemData one = enumerator.Current;
					bool flag2 = one.confdata.use_type == 19;
					if (flag2)
					{
						GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(one, true, one.num, 1f, false);
						gameObject.transform.SetParent(this.shellcon_baoshi.transform.GetChild(num), false);
						gameObject.transform.GetComponent<Button>().enabled = true;
						gameObject.transform.GetComponent<Button>().onClick.AddListener(delegate
						{
							this.curBaoshiId = one.tpid;
							this.Inbaoshi.SetActive(true);
							Transform transform = this.Inbaoshi.transform.FindChild("icon");
							for (int k = 0; k < transform.childCount; k++)
							{
								UnityEngine.Object.Destroy(transform.GetChild(k).gameObject);
							}
							this.Inbaoshi.transform.FindChild("name").GetComponent<Text>().text = one.confdata.item_name;
							this.Inbaoshi.transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(one.confdata.quality);
							this.Inbaoshi.transform.FindChild("dec").GetComponent<Text>().text = StringUtils.formatText(one.confdata.desc);
							GameObject gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(one, false, -1, 1f, false);
							gameObject2.transform.SetParent(transform, false);
							bool flag3 = this.tabIdx == 7;
							if (flag3)
							{
								this.Inbaoshi.transform.FindChild("do/Text").GetComponent<Text>().text = "镶嵌";
								new BaseButton(this.Inbaoshi.transform.FindChild("do"), 1, 1).onClick = new Action<GameObject>(this.<initBaoshiPanel>b__130_1);
							}
							else
							{
								bool flag4 = this.tabIdx == 6;
								if (flag4)
								{
									this.Inbaoshi.transform.FindChild("do/Text").GetComponent<Text>().text = "放入";
									new BaseButton(this.Inbaoshi.transform.FindChild("do"), 1, 1).onClick = delegate(GameObject go)
									{
										this.input_baoshi(one);
										this.Inbaoshi.SetActive(false);
										this.ref_hc_baoshiMoney();
									};
								}
							}
						});
						num++;
					}
				}
			}
		}

		private void input_baoshi(a3_BagItemData data)
		{
			this.clear_baoshi();
			SXML sXML = XMLMgr.instance.GetSXML("item", "");
			SXML node = sXML.GetNode("gem_intensify.gem", "item_id==" + data.tpid);
			uint @int = (uint)node.getInt("get_num");
			bool flag = @int <= 0u;
			if (flag)
			{
				flytxt.instance.fly("已达到最高等级", 0, default(Color), null);
			}
			else
			{
				bool flag2 = data.num < 5;
				if (flag2)
				{
					this.addtoget(data.confdata);
					flytxt.instance.fly("宝石数量不足！", 0, default(Color), null);
				}
				else
				{
					this.hcbaoshiId = data.tpid;
					this.hcid = data.id;
					for (int i = 0; i < this.baoshi_con2.Count; i++)
					{
						GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data, false, -1, 1f, false);
						gameObject.transform.SetParent(this.baoshi_con2[i].transform, false);
					}
					bool flag3 = @int > 0u;
					if (flag3)
					{
						GameObject gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(ModelBase<a3_BagModel>.getInstance().getItemDataById(@int), false, -1, 1f, false, -1, 0, false, false, false, -1, false, false);
						gameObject2.transform.FindChild("iconborder").gameObject.SetActive(false);
						gameObject2.transform.SetParent(this.geticon.transform, false);
					}
					else
					{
						this.hcbaoshiId = 0u;
						this.hcid = 0u;
					}
				}
			}
		}

		private void clear_baoshi()
		{
			for (int i = 0; i < this.baoshi_con2.Count; i++)
			{
				for (int j = 0; j < this.baoshi_con2[i].transform.childCount; j++)
				{
					UnityEngine.Object.Destroy(this.baoshi_con2[i].transform.GetChild(j).gameObject);
				}
			}
			for (int k = 0; k < this.geticon.transform.childCount; k++)
			{
				UnityEngine.Object.Destroy(this.geticon.transform.GetChild(k).gameObject);
			}
		}

		private void onHeCheng(GameObject go)
		{
			bool flag = this.hcbaoshiId > 0u;
			if (flag)
			{
				BaseProxy<EquipProxy>.getInstance().send_hcBaoshi(this.hcbaoshiId, 1u);
			}
			else
			{
				flytxt.instance.fly("请加入正确宝石！", 0, default(Color), null);
			}
		}

		private void onBaoshi_hc(GameEvent e)
		{
			Variant data = e.data;
			SXML sXML = XMLMgr.instance.GetSXML("item", "");
			SXML node = sXML.GetNode("gem_intensify.gem", "item_id==" + this.hcbaoshiId);
			uint @int = (uint)node.getInt("get_num");
			bool flag = @int > 0u;
			if (flag)
			{
				SXML node2 = sXML.GetNode("item", "id==" + @int);
				flytxt.instance.fly("获得" + node2.getString("item_name"), 0, default(Color), null);
			}
			bool flag2 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(this.hcid);
			if (flag2)
			{
				bool flag3 = ModelBase<a3_BagModel>.getInstance().getItems(false)[this.hcid].num >= 5;
				if (!flag3)
				{
					this.clear_baoshi();
					this.hcbaoshiId = 0u;
					this.hcid = 0u;
				}
			}
			else
			{
				this.clear_baoshi();
				this.hcbaoshiId = 0u;
				this.hcid = 0u;
			}
			this.initBaoshiPanel();
			this.ref_hc_baoshiMoney();
		}

		public void ref_hc_baoshiMoney()
		{
			bool flag = this.hcbaoshiId <= 0u;
			if (flag)
			{
				this.hcMoney.text = "0";
			}
			else
			{
				SXML sXML = XMLMgr.instance.GetSXML("item", "");
				SXML node = sXML.GetNode("gem_intensify.gem", "item_id==" + this.hcbaoshiId);
				this.hcMoney.text = node.getInt("money").ToString();
			}
		}

		public void ref_zx_baoshiMoney()
		{
			bool flag = this.outKey < 0 || this.outKey > 2;
			if (flag)
			{
				this.zxMoney.text = "0";
			}
			else
			{
				SXML sXML = XMLMgr.instance.GetSXML("item", "");
				a3_BagItemData equipByAll = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId3);
				SXML node = sXML.GetNode("gem_info", "item_id==" + equipByAll.equipdata.baoshi[this.outKey]);
				this.zxMoney.text = node.getInt("take_off_money").ToString();
			}
		}

		private void refrenshChangeBaoshi()
		{
			this.outKey = -1;
			this.refMask();
			this.ref_zx_baoshiMoney();
			foreach (int current in this.baoshi_con.Keys)
			{
				int i = 0;
				while (i < this.baoshi_con[current].transform.childCount)
				{
					bool flag = this.baoshi_con[current].transform.GetChild(i).name == "local";
					if (flag)
					{
						this.baoshi_con[current].transform.GetChild(i).gameObject.SetActive(true);
					}
					else
					{
						bool flag2 = this.baoshi_con[current].transform.GetChild(i).name == "isthis";
						if (!flag2)
						{
							UnityEngine.Object.Destroy(this.baoshi_con[current].transform.GetChild(i).gameObject);
						}
					}
					IL_EF:
					i++;
					continue;
					goto IL_EF;
				}
			}
			Transform transform = this.infoPanel[7].transform.FindChild("bg/equipicon");
			bool flag3 = transform.childCount > 0;
			if (flag3)
			{
				for (int j = 0; j < transform.childCount; j++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(j).gameObject);
				}
			}
			bool flag4 = this.curInheritId3 <= 0u;
			if (!flag4)
			{
				a3_BagItemData equipByAll = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.curInheritId3);
				GameObject gameObject = IconImageMgr.getInstance().createA3EquipIcon(equipByAll, 0.8f, false);
				gameObject.transform.SetParent(transform, false);
				this.addEquipTipClick_bag(gameObject, equipByAll);
				bool flag5 = equipByAll.equipdata.baoshi == null;
				if (!flag5)
				{
					for (int k = 0; k < 3; k++)
					{
						this.infoPanel[7].transform.FindChild("bg/att" + k).gameObject.SetActive(false);
					}
					foreach (int current2 in equipByAll.equipdata.baoshi.Keys)
					{
						bool flag6 = equipByAll.equipdata.baoshi[current2] == 0;
						if (flag6)
						{
							this.baoshi_con[current2].transform.FindChild("local").gameObject.SetActive(false);
							this.infoPanel[7].transform.FindChild("bg/att" + current2).gameObject.SetActive(true);
							this.infoPanel[7].transform.FindChild("bg/att" + current2 + "/icon").gameObject.SetActive(false);
							this.infoPanel[7].transform.FindChild("bg/att" + current2 + "/att").GetComponent<Text>().text = "可镶嵌";
						}
						bool flag7 = equipByAll.equipdata.baoshi[current2] > 0;
						if (flag7)
						{
							GameObject gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)equipByAll.equipdata.baoshi[current2]), false, -1, 0.7f, false, -1, 0, false, false, false, -1, false, false);
							this.baoshi_con[current2].transform.FindChild("local").gameObject.SetActive(false);
							gameObject2.transform.SetParent(this.baoshi_con[current2].transform, false);
							SXML sXML = XMLMgr.instance.GetSXML("item", "");
							SXML node = sXML.GetNode("item", "id==" + equipByAll.equipdata.baoshi[current2]);
							string path = "icon/item/" + node.getString("icon_file");
							this.infoPanel[7].transform.FindChild("bg/att" + current2 + "/icon").GetComponent<Image>().sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
							this.infoPanel[7].transform.FindChild("bg/att" + current2 + "/icon").gameObject.SetActive(true);
							SXML node2 = sXML.GetNode("gem_info", "item_id==" + equipByAll.equipdata.baoshi[current2]);
							List<SXML> nodeList = node2.GetNodeList("gem_add", "");
							int id = 0;
							int value = 0;
							foreach (SXML current3 in nodeList)
							{
								bool flag8 = current3.getInt("equip_type") == equipByAll.confdata.equip_type;
								if (flag8)
								{
									id = current3.getInt("att_type");
									value = current3.getInt("att_value");
									break;
								}
							}
							this.infoPanel[7].transform.FindChild("bg/att" + current2).gameObject.SetActive(true);
							this.infoPanel[7].transform.FindChild("bg/att" + current2 + "/att").GetComponent<Text>().text = Globle.getAttrAddById(id, value, true);
							int tag = current2;
							new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject go)
							{
								this.outKey = tag;
								this.refMask();
							};
						}
					}
				}
			}
		}

		private void refMask()
		{
			bool flag = this.outKey >= 0 && this.outKey <= 2;
			if (flag)
			{
				this.isthis.transform.SetParent(this.baoshi_con[this.outKey].transform, false);
				this.isthis.SetActive(true);
				this.isthis.transform.localPosition = Vector3.zero;
				this.ref_zx_baoshiMoney();
			}
			else
			{
				this.isthis.SetActive(false);
			}
		}

		private void onSendOut(GameObject go)
		{
			bool flag = this.outKey >= 0 && this.outKey <= 2;
			if (flag)
			{
				BaseProxy<EquipProxy>.getInstance().send_outBaoshi(this.curInheritId3, (uint)(this.outKey + 1));
				this.outKey = -1;
				this.refMask();
			}
			else
			{
				flytxt.instance.fly("请选择摘取宝石!", 0, default(Color), null);
			}
		}

		public void CheckLock()
		{
			base.transform.FindChild("panelTab2/con/0").gameObject.SetActive(false);
			base.transform.FindChild("panelTab2/con/1").gameObject.SetActive(false);
			base.transform.FindChild("panelTab2/con/2").gameObject.SetActive(false);
			base.transform.FindChild("panelTab2/con/3").gameObject.SetActive(false);
			base.transform.FindChild("panelTab2/con/4").gameObject.SetActive(false);
			base.transform.FindChild("panelTab2/con/5").gameObject.SetActive(false);
			base.transform.FindChild("panelTab2/con/6").gameObject.SetActive(false);
			base.transform.FindChild("panelTab2/con/7").gameObject.SetActive(false);
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_ENHANCEMENT, false);
			if (flag)
			{
				this.OpenQH();
			}
			bool flag2 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_REMOLD, false);
			if (flag2)
			{
				this.OpenCZ();
			}
			bool flag3 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_INHERITANCE, false);
			if (flag3)
			{
				this.OpenCC();
			}
			bool flag4 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_LVUP, false);
			if (flag4)
			{
				this.OpenJJ();
			}
			bool flag5 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_MOUNTING, false);
			if (flag5)
			{
				this.OpenBS();
			}
			bool flag6 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_ENCHANT, false);
			if (flag6)
			{
				this.OpenZJ();
			}
			bool flag7 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_BSHC, false);
			if (flag7)
			{
				this.OpenBSHC();
			}
			bool flag8 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP_BSXQ, false);
			if (flag8)
			{
				this.OpenBSXQ();
			}
		}

		public void OpenQH()
		{
			base.transform.FindChild("panelTab2/con/0").gameObject.SetActive(true);
		}

		public void OpenCZ()
		{
			base.transform.FindChild("panelTab2/con/1").gameObject.SetActive(true);
		}

		public void OpenCC()
		{
			base.transform.FindChild("panelTab2/con/2").gameObject.SetActive(true);
		}

		public void OpenJJ()
		{
			base.transform.FindChild("panelTab2/con/3").gameObject.SetActive(true);
		}

		public void OpenBS()
		{
			base.transform.FindChild("panelTab2/con/4").gameObject.SetActive(true);
		}

		public void OpenZJ()
		{
			base.transform.FindChild("panelTab2/con/5").gameObject.SetActive(true);
		}

		public void OpenBSHC()
		{
			base.transform.FindChild("panelTab2/con/6").gameObject.SetActive(true);
		}

		public void OpenBSXQ()
		{
			base.transform.FindChild("panelTab2/con/7").gameObject.SetActive(true);
		}

		private void addIconHintImage()
		{
			IconHintMgr.getInsatnce().addHint_equip(base.getTransformByPath("panelTab2/con/0"), IconHintMgr.TYPE_QIANGHUA);
			IconHintMgr.getInsatnce().addHint_equip(base.getTransformByPath("panelTab2/con/3"), IconHintMgr.TYPE_JINGLIAN);
			IconHintMgr.getInsatnce().addHint_equip(base.getTransformByPath("panelTab2/con/5"), IconHintMgr.TYPE_ZHUIJIA);
			IconHintMgr.getInsatnce().addHint_equip(base.getTransformByPath("panelTab2/con/7"), IconHintMgr.TYPE_XIANGQIAN);
		}

		private void showIconHintImage()
		{
			bool flag = this.check_QH();
			if (flag)
			{
				IconHintMgr.getInsatnce().showHint(IconHintMgr.TYPE_QIANGHUA, -1, -1, false);
			}
			else
			{
				IconHintMgr.getInsatnce().closeHint(IconHintMgr.TYPE_QIANGHUA, false, false);
			}
			bool flag2 = this.check_JL();
			if (flag2)
			{
				IconHintMgr.getInsatnce().showHint(IconHintMgr.TYPE_JINGLIAN, -1, -1, false);
			}
			else
			{
				IconHintMgr.getInsatnce().closeHint(IconHintMgr.TYPE_JINGLIAN, false, false);
			}
			bool flag3 = this.check_ZJ();
			if (flag3)
			{
				IconHintMgr.getInsatnce().showHint(IconHintMgr.TYPE_ZHUIJIA, -1, -1, false);
			}
			else
			{
				IconHintMgr.getInsatnce().closeHint(IconHintMgr.TYPE_ZHUIJIA, false, false);
			}
			bool flag4 = this.check_XQ();
			if (flag4)
			{
				IconHintMgr.getInsatnce().showHint(IconHintMgr.TYPE_XIANGQIAN, -1, -1, false);
			}
			else
			{
				IconHintMgr.getInsatnce().closeHint(IconHintMgr.TYPE_XIANGQIAN, false, false);
			}
		}

		private bool check_QH()
		{
			SXML sXML = XMLMgr.instance.GetSXML("item.intensify", "intensify_level==" + (this.curChooseEquip.equipdata.intensify_lv + 1));
			bool flag = sXML != null;
			bool result;
			if (flag)
			{
				bool flag2 = (long)sXML.getInt("intensify_money") > (long)((ulong)ModelBase<PlayerModel>.getInstance().money);
				if (flag2)
				{
					result = false;
				}
				else
				{
					string @string = sXML.getString("intensify_material1");
					string[] array = @string.Split(new char[]
					{
						','
					});
					int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(uint.Parse(array[0]));
					bool flag3 = itemNumByTpid >= int.Parse(array[1]);
					result = flag3;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		private bool check_JL()
		{
			bool flag = true;
			SXML sXML = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + this.curChooseEquip.equipdata.stage);
			SXML node = sXML.GetNode("stage_info", "itemid==" + this.curChooseEquip.tpid);
			bool flag2 = (long)node.getInt("stage_money") > (long)((ulong)ModelBase<PlayerModel>.getInstance().money);
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = this.curChooseEquip.equipdata.intensify_lv < 15;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = (long)node.getInt("zhuan") > (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) || ((long)node.getInt("zhuan") == (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) && (long)node.getInt("level") > (long)((ulong)ModelBase<PlayerModel>.getInstance().lvl));
					if (flag4)
					{
						result = false;
					}
					else
					{
						List<SXML> nodeList = node.GetNodeList("stage_material", "");
						foreach (SXML current in nodeList)
						{
							bool flag5 = current.getInt("num") <= 0;
							if (flag5)
							{
								result = false;
								return result;
							}
							int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(current.getUint("item"));
							bool flag6 = itemNumByTpid < current.getInt("num");
							if (flag6)
							{
								result = false;
								return result;
							}
						}
						result = flag;
					}
				}
			}
			return result;
		}

		private bool check_ZJ()
		{
			bool flag = true;
			bool flag2 = this.curChooseEquip.equipdata.stage <= 0;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = this.curChooseEquip.equipdata.add_level >= this.curChooseEquip.confdata.add_basiclevel * this.curChooseEquip.confdata.equip_level * this.curChooseEquip.equipdata.stage;
				if (flag3)
				{
					result = false;
				}
				else
				{
					SXML sXML = XMLMgr.instance.GetSXML("item.add_att", "add_level==" + (this.curChooseEquip.equipdata.add_level + 1));
					bool flag4 = sXML == null;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = (long)sXML.getInt("money") > (long)((ulong)ModelBase<PlayerModel>.getInstance().money);
						if (flag5)
						{
							result = false;
						}
						else
						{
							uint @uint = sXML.getUint("material_id");
							int @int = sXML.getInt("material_num");
							bool flag6 = @int > ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(@uint);
							result = (!flag6 && flag);
						}
					}
				}
			}
			return result;
		}

		private bool check_XQ()
		{
			bool flag = this.curChooseEquip.equipdata.baoshi.Count <= 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = false;
				foreach (int current in this.curChooseEquip.equipdata.baoshi.Keys)
				{
					bool flag3 = this.curChooseEquip.equipdata.baoshi[current] <= 0;
					if (flag3)
					{
						flag2 = true;
						break;
					}
				}
				bool flag4 = false;
				Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
				foreach (a3_BagItemData current2 in items.Values)
				{
					bool flag5 = current2.confdata.use_type == 19;
					if (flag5)
					{
						flag4 = true;
						break;
					}
				}
				result = (flag2 & flag4);
			}
			return result;
		}
	}
}
