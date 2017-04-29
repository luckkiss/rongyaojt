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
	internal class shop_a3 : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly shop_a3.<>c <>9 = new shop_a3.<>c();

			public static Action<GameObject> <>9__51_0;

			public static Action<GameObject> <>9__51_1;

			public static Action<GameObject> <>9__51_2;

			public static Action<GameObject> <>9__51_5;

			public static Action<GameObject> <>9__86_0;

			internal void <init>b__51_0(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EXCHANGE, null, false);
				a3_exchange expr_18 = a3_exchange.Instance;
				if (expr_18 != null)
				{
					expr_18.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__51_1(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				a3_Recharge expr_18 = a3_Recharge.Instance;
				if (expr_18 != null)
				{
					expr_18.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__51_2(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				bool flag = a3_Recharge.Instance != null;
				if (flag)
				{
					a3_Recharge.Instance.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__51_5(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(0);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, arrayList, false);
				a3_shejiao expr_2B = a3_shejiao.instance;
				if (expr_2B != null)
				{
					expr_2B.transform.SetAsLastSibling();
				}
			}

			internal void <Refresh>b__86_0(GameObject go)
			{
				flytxt.instance.fly("已经达到今日购买上限，明天再来吧！", 1, default(Color), null);
			}
		}

		private bool AddorMin = true;

		private int nowstate = 5;

		private CanvasGroup bg;

		private CanvasGroup bg_image;

		private Dictionary<int, shopDatas> dic = new Dictionary<int, shopDatas>();

		private List<shopDatas> lins_data = new List<shopDatas>();

		private List<shopDatas> shop_data = new List<shopDatas>();

		public static int now_id = 0;

		private Dictionary<int, GameObject> havePurchase = new Dictionary<int, GameObject>();

		private Dictionary<int, GameObject> limitedobj = new Dictionary<int, GameObject>();

		private Dictionary<int, limitedinmfos> limitedinmfo = new Dictionary<int, limitedinmfos>();

		private Dictionary<int, Queue<int>> limitedActivity = new Dictionary<int, Queue<int>>();

		private GameObject contain;

		private GameObject nullitems;

		private GameObject image_goodsorbundinggem;

		private GameObject image_packs;

		private GameObject image_limitedactive;

		private GameObject buy_success;

		private GameObject max;

		private GameObject min;

		private Text itiemd_time;

		private Text money;

		private Text gold;

		private Text coin;

		private Text textPageIndex;

		public GameObject objsurebuy;

		public GameObject desc;

		public GameObject close_btn;

		private Text surebuy_name;

		private Text surebuy_des;

		private InputField Inputbuy_num;

		private Text buy_num;

		private Text needbuymoney;

		private Scrollbar bar;

		private GameObject bar_Handle;

		private GameObject icon_small;

		private Text text1;

		private Text text_name;

		private Text text_dengji;

		public TabControl tab;

		public static shop_a3 _instance;

		private ScrollControler scrollControer;

		private GameObject[] btn;

		private BaseButton chongzhi;

		private Dictionary<Transform, BaseButton> tab_btns = new Dictionary<Transform, BaseButton>();

		private int maxNum = 8;

		private int pageIndex = 1;

		private int selectType = 1;

		private int maxPageNum = 1;

		private int btni = 0;

		public int selectnum = 0;

		private bool Toclose = false;

		private int itenmoneys = 0;

		private int maxnum = 999;

		private bool canbuy = false;

		private int times = -1;

		private float reduceoradd;

		private int nownum;

		public override void init()
		{
			shop_a3._instance = this;
			this.textPageIndex = base.getComponentByPath<Text>("panel_right/Image1/Text");
			this.bg_image = base.getGameObjectByPath("panel_down/image").GetComponent<CanvasGroup>();
			this.contain = base.transform.FindChild("panel_right/scroll_rect/contain").gameObject;
			this.image_goodsorbundinggem = base.transform.FindChild("panel_right/scroll_rect/Image_goodsorbundinggem").gameObject;
			this.image_packs = base.transform.FindChild("panel_right/scroll_rect/Image_packs").gameObject;
			this.nullitems = base.transform.FindChild("panel_right/null_image").gameObject;
			this.image_limitedactive = base.transform.FindChild("panel_right/scroll_rect/Image_limitedactive").gameObject;
			this.itiemd_time = base.transform.FindChild("panel_right/Image/Text").GetComponent<Text>();
			this.money = base.transform.FindChild("panel_down/gems/image/num").GetComponent<Text>();
			this.gold = base.transform.FindChild("panel_down/gemss/image/num").GetComponent<Text>();
			this.coin = base.transform.FindChild("panel_down/gemsss/image/num").GetComponent<Text>();
			this.objsurebuy = base.transform.FindChild("objdes").gameObject;
			this.surebuy_name = this.objsurebuy.transform.FindChild("bg/contain/name").GetComponent<Text>();
			this.surebuy_des = this.objsurebuy.transform.FindChild("bg/contain/des_bg/Text").GetComponent<Text>();
			this.Inputbuy_num = this.objsurebuy.transform.FindChild("bg/contain/bug/InputField").GetComponent<InputField>();
			this.buy_num = this.Inputbuy_num.transform.FindChild("Text").GetComponent<Text>();
			this.bar = this.objsurebuy.transform.FindChild("bg/contain/Scrollbar").GetComponent<Scrollbar>();
			this.needbuymoney = base.transform.FindChild("objdes/bg/contain/paymoney/money").GetComponent<Text>();
			this.buy_success = base.transform.FindChild("buy_success").gameObject;
			this.bar_Handle = this.objsurebuy.transform.FindChild("bg/contain/Scrollbar/Sliding Area/Handle").gameObject;
			this.close_btn = base.transform.FindChild("close_desc").gameObject;
			this.desc = base.transform.FindChild("close_desc/text_bg").gameObject;
			this.icon_small = this.desc.transform.FindChild("iconbg/icon").gameObject;
			this.text1 = this.desc.transform.FindChild("text").GetComponent<Text>();
			this.text_name = this.desc.transform.FindChild("name/namebg").GetComponent<Text>();
			this.text_dengji = this.desc.transform.FindChild("name/dengji").GetComponent<Text>();
			this.tab = new TabControl();
			this.tab.onClickHanle = new Action<TabControl>(this.tabhandel);
			this.selectnum = ModelBase<Shop_a3Model>.getInstance().selectnum;
			this.tab.create(base.getGameObjectByPath("panel_left/content"), base.gameObject, this.selectnum, 0, true);
			BaseButton arg_375_0 = new BaseButton(base.transform.FindChild("panel_down/gemsss/Image"), 1, 1);
			Action<GameObject> arg_375_1;
			if ((arg_375_1 = shop_a3.<>c.<>9__51_0) == null)
			{
				arg_375_1 = (shop_a3.<>c.<>9__51_0 = new Action<GameObject>(shop_a3.<>c.<>9.<init>b__51_0));
			}
			arg_375_0.onClick = arg_375_1;
			BaseButton arg_3B1_0 = new BaseButton(base.transform.FindChild("panel_down/gems/Image"), 1, 1);
			Action<GameObject> arg_3B1_1;
			if ((arg_3B1_1 = shop_a3.<>c.<>9__51_1) == null)
			{
				arg_3B1_1 = (shop_a3.<>c.<>9__51_1 = new Action<GameObject>(shop_a3.<>c.<>9.<init>b__51_1));
			}
			arg_3B1_0.onClick = arg_3B1_1;
			BaseButton arg_3ED_0 = new BaseButton(base.transform.FindChild("panel_down/addmoney/text"), 1, 1);
			Action<GameObject> arg_3ED_1;
			if ((arg_3ED_1 = shop_a3.<>c.<>9__51_2) == null)
			{
				arg_3ED_1 = (shop_a3.<>c.<>9__51_2 = new Action<GameObject>(shop_a3.<>c.<>9.<init>b__51_2));
			}
			arg_3ED_0.onClick = arg_3ED_1;
			new BaseButton(base.transform.FindChild("panel_right/Image1/right"), 1, 1).onClick = delegate(GameObject go)
			{
				this.pageIndex++;
				int num = this.btni + 1;
				bool flag3 = num > 4 && this.pageIndex >= this.maxPageNum + 1;
				if (flag3)
				{
					this.pageIndex = this.maxPageNum;
				}
				bool flag4 = num >= 5;
				if (flag4)
				{
				}
				bool flag5 = this.pageIndex > this.maxPageNum;
				if (flag5)
				{
					bool flag6 = this.shop_data.Count % this.maxNum != 0;
					if (flag6)
					{
						bool flag7 = this.pageIndex > this.shop_data.Count / this.maxNum + 1;
						if (flag7)
						{
							return;
						}
					}
					bool flag8 = this.shop_data.Count % this.maxNum == 0;
					if (flag8)
					{
						bool flag9 = this.pageIndex > this.shop_data.Count / this.maxNum;
						if (flag9)
						{
							return;
						}
					}
				}
				this.OnShowSelect(this.pageIndex);
			};
			new BaseButton(base.transform.FindChild("panel_right/Image1/left"), 1, 1).onClick = delegate(GameObject go)
			{
				this.pageIndex--;
				int num = this.btni - 1;
				bool flag3 = num < 0 && this.pageIndex <= 0;
				if (!flag3)
				{
					bool flag4 = num < 0;
					if (flag4)
					{
					}
					bool flag5 = this.pageIndex <= 0;
					if (flag5)
					{
						this.pageIndex = this.maxPageNum;
					}
					this.OnShowSelect(this.pageIndex);
				}
			};
			BaseButton arg_476_0 = new BaseButton(base.getTransformByPath("contribute/goto"), 1, 1);
			Action<GameObject> arg_476_1;
			if ((arg_476_1 = shop_a3.<>c.<>9__51_5) == null)
			{
				arg_476_1 = (shop_a3.<>c.<>9__51_5 = new Action<GameObject>(shop_a3.<>c.<>9.<init>b__51_5));
			}
			arg_476_0.onClick = arg_476_1;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("objdes/bg/contain/btn_reduce"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onLefts);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("objdes/bg/contain/btn_add"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onRights);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("objdes/bg/contain/max"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onmax);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("objdes/bg/contain/min"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onmin);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("close"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onClose);
			BaseButton baseButton6 = new BaseButton(base.transform.FindChild("objdes/btn"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onClose1);
			BaseButton baseButton7 = new BaseButton(base.transform.FindChild("close_desc/close_btn"), 1, 1);
			baseButton7.onClick = new Action<GameObject>(this.close_desc);
			BaseButton baseButton8 = new BaseButton(this.buy_success.transform.FindChild("bg/Button"), 1, 1);
			baseButton8.onClick = new Action<GameObject>(this.onClose3);
			this.chongzhi = new BaseButton(base.transform.FindChild("chongzhibtn"), 1, 1);
			this.chongzhi.onClick = new Action<GameObject>(this.OnChongzhi);
			BaseProxy<Shop_a3Proxy>.getInstance().sendinfo(0, 0, 0, -1);
			base.InvokeRepeating("OnShowAchievementPage", 0f, 0.3f);
			foreach (KeyValuePair<int, shopDatas> current in ModelBase<Shop_a3Model>.getInstance().itemsdic)
			{
				this.dic.Add(current.Key, current.Value);
			}
			bool flag = ModelBase<A3_LegionModel>.getInstance().myLegion.clname == null;
			if (flag)
			{
				base.getGameObjectByPath("panel_left/content/5").SetActive(false);
			}
			else
			{
				bool flag2 = FunctionOpenMgr.instance.checkLegion(FunctionOpenMgr.LEGION, false);
				if (flag2)
				{
					base.getGameObjectByPath("panel_left/content/5").SetActive(true);
				}
				else
				{
					base.getGameObjectByPath("panel_left/content/5").SetActive(false);
				}
			}
			switch (this.selectnum)
			{
			case 0:
				this.tab1();
				break;
			case 1:
				this.tab2();
				break;
			case 2:
				this.tab3();
				break;
			case 3:
				this.tab4();
				break;
			case 4:
				this.tab5();
				break;
			case 5:
				this.tab6();
				break;
			}
			base.getComponentByPath<Text>("contribute/num").text = ModelBase<A3_LegionModel>.getInstance().donate.ToString();
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(this.haveClan));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(22u, new Action<GameEvent>(this.deleteClan));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(9u, new Action<GameEvent>(this.deleteClan));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(1u, new Action<GameEvent>(this.changeDonate));
			BaseProxy<Shop_a3Proxy>.getInstance().addEventListener(Shop_a3Proxy.DONATECHANGE, new Action<GameEvent>(this.changeDonate));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(1u, new Action<GameEvent>(this.RefreshInfo));
		}

		private void tabhandel(TabControl t)
		{
			this.selectnum = t.getSeletedIndex();
			switch (this.selectnum)
			{
			case 0:
				this.tab1();
				break;
			case 1:
				this.tab2();
				break;
			case 2:
				this.tab3();
				break;
			case 3:
				this.tab4();
				break;
			case 4:
				this.tab5();
				break;
			case 5:
				this.tab6();
				break;
			}
		}

		private void tab6()
		{
			BaseProxy<Shop_a3Proxy>.getInstance().addEventListener(Shop_a3Proxy.LIMITED, new Action<GameEvent>(this.onLimited));
			BaseProxy<Shop_a3Proxy>.getInstance().addEventListener(Shop_a3Proxy.CHANGELIMITED, new Action<GameEvent>(this.onChangeLimited));
			BaseProxy<Shop_a3Proxy>.getInstance().addEventListener(Shop_a3Proxy.DELETELIMITED, new Action<GameEvent>(this.onDeleteLimited));
			BaseProxy<Shop_a3Proxy>.getInstance().sendinfo(1, 0, 0, -1);
			this.onShowlimitedactive();
			this.OnShowSelect(1);
		}

		private void tab1()
		{
			this.onShowpacks();
			this.OnShowSelect(1);
		}

		private void tab2()
		{
			this.onShowgoods();
			this.OnShowSelect(1);
		}

		private void tab3()
		{
			this.onShowbundinggem();
			this.OnShowSelect(1);
		}

		private void tab4()
		{
			this.onSummonTab();
			this.OnShowSelect(1);
		}

		private void tab5()
		{
			this.onLegion();
			this.OnShowSelect(1);
		}

		private void close_desc(GameObject obj)
		{
			this.close_btn.SetActive(false);
		}

		private void haveClan(GameEvent e)
		{
			bool flag = FunctionOpenMgr.instance.checkLegion(FunctionOpenMgr.LEGION, false);
			if (flag)
			{
				base.getGameObjectByPath("panel_left/content/5").SetActive(true);
			}
			else
			{
				base.getGameObjectByPath("panel_left/content/5").SetActive(false);
			}
		}

		private void deleteClan(GameEvent e)
		{
			base.getGameObjectByPath("panel_left/content/5").SetActive(false);
			this.tab.setSelectedIndex(0, false);
		}

		private void RefreshInfo(GameEvent e)
		{
			bool flag = FunctionOpenMgr.instance.checkLegion(FunctionOpenMgr.LEGION, false);
			if (flag)
			{
				base.getGameObjectByPath("panel_left/content/5").SetActive(true);
			}
			else
			{
				base.getGameObjectByPath("panel_left/content/5").SetActive(false);
			}
		}

		private void changeDonate(GameEvent e)
		{
			this.onLegion();
			bool flag = e.data.ContainsKey("donate");
			if (flag)
			{
				ModelBase<A3_LegionModel>.getInstance().donate = e.data["donate"];
			}
			base.getComponentByPath<Text>("contribute/num").text = ModelBase<A3_LegionModel>.getInstance().donate.ToString();
		}

		private void OnShowAchievementPage()
		{
			float y = this.contain.GetComponent<RectTransform>().anchoredPosition.y;
			float y2 = this.image_goodsorbundinggem.GetComponent<RectTransform>().sizeDelta.y;
			bool flag = y < y2;
			if (flag)
			{
				this.pageIndex = 1;
				this.textPageIndex.text = 1 + "/" + this.maxPageNum;
			}
			else
			{
				for (int i = 2; i <= this.maxPageNum; i++)
				{
					bool flag2 = y >= y2 && y >= 4f * y2 * (float)i - 7f * y2 - 20f && y < 4f * y2 * (float)(i + 1) - 7f * y2 - 20f;
					if (flag2)
					{
						this.pageIndex = i;
						this.textPageIndex.text = i + "/" + this.maxPageNum;
					}
				}
			}
		}

		private void OnShowSelect(int ss)
		{
			float y = this.image_goodsorbundinggem.GetComponent<RectTransform>().sizeDelta.y;
			this.pageIndex = ss;
			this.maxNum = 8;
			bool flag = this.shop_data.Count % this.maxNum != 0 || this.shop_data.Count == 0;
			if (flag)
			{
				this.maxPageNum = this.shop_data.Count / this.maxNum + 1;
			}
			else
			{
				this.maxPageNum = this.shop_data.Count / this.maxNum;
			}
			this.textPageIndex.text = this.pageIndex + "/" + this.maxPageNum;
			this.contain.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (this.contain.GetComponent<GridLayoutGroup>().cellSize.y + 2f) * (float)Mathf.CeilToInt((float)this.shop_data.Count / 2f));
			int num = this.shop_data.Count % this.maxNum;
			int num2 = this.shop_data.Count / this.maxNum;
			bool flag2 = this.maxPageNum > 1;
			if (flag2)
			{
				bool flag3 = num % 2 != 0;
				if (flag3)
				{
					num++;
				}
				bool flag4 = num == 0 || this.pageIndex < this.maxPageNum;
				if (flag4)
				{
					this.contain.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, (float)(this.pageIndex - 1) * (4f * y + 8f), 0f);
				}
				else
				{
					bool flag5 = this.pageIndex >= 2;
					if (flag5)
					{
						this.contain.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, (float)(this.pageIndex - 2) * (4f * y + 8f) + (float)(num / 2) * (y + 2f), 0f);
					}
				}
			}
			bool flag6 = this.maxPageNum <= 1;
			if (flag6)
			{
				this.contain.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f, 0f);
			}
		}

		public void setopen(int id)
		{
			SXML sXML = XMLMgr.instance.GetSXML("golden_shop.golden_shop", "id==" + id);
			bool flag = sXML == null;
			if (!flag)
			{
				int @int = sXML.getInt("type");
				int int2 = sXML.getInt("group");
				switch (@int)
				{
				case 1:
					this.objsurebuy.SetActive(true);
					this.tab.setSelectedIndex(1, false);
					this.surebuy(id, this.dic[id].itemid, 0, this.dic[id].money_type, this.dic[id].value, 2, -1);
					break;
				case 2:
					this.objsurebuy.SetActive(true);
					this.tab.setSelectedIndex(0, false);
					this.surebuy(id, this.dic[id].itemid, 0, this.dic[id].money_type, this.dic[id].value, 2, -1);
					break;
				case 3:
					this.objsurebuy.SetActive(true);
					this.tab.setSelectedIndex(2, false);
					this.surebuy(id, this.dic[id].itemid, 0, this.dic[id].money_type, this.dic[id].value, 2, -1);
					break;
				case 4:
					this.objsurebuy.SetActive(true);
					this.tab.setSelectedIndex(4, false);
					this.surebuy(id, this.dic[id].itemid, 0, this.dic[id].money_type, this.dic[id].value, 2, -1);
					break;
				case 5:
					this.objsurebuy.SetActive(true);
					this.tab.setSelectedIndex(3, false);
					this.surebuy(id, this.dic[id].itemid, 0, this.dic[id].money_type, this.dic[id].value, 2, -1);
					break;
				case 6:
					this.objsurebuy.SetActive(true);
					this.tab.setSelectedIndex(5, false);
					this.surebuy(id, this.dic[id].itemid, 0, this.dic[id].money_type, this.dic[id].value, 2, -1);
					break;
				}
				float num = this.contain.GetComponent<GridLayoutGroup>().cellSize.y + this.contain.GetComponent<GridLayoutGroup>().spacing.y;
				this.contain.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, num * 4f * (float)(int2 - 1), 0f);
			}
		}

		public override void onShowed()
		{
			this.Toclose = false;
			bool flag = !FunctionOpenMgr.instance.checkLegion(FunctionOpenMgr.LEGION, false);
			if (flag)
			{
				this.tab.setSelectedIndex(0, false);
			}
			this.refreshGold();
			this.refreshGift();
			this.refreshCoin();
			this.OnShowSelect(1);
			this.bg_image.interactable = false;
			this.bg_image.blocksRaycasts = false;
			this.selectnum = ModelBase<Shop_a3Model>.getInstance().selectnum;
			bool flag2 = this.uiData != null;
			if (flag2)
			{
				int id = (int)this.uiData[0];
				this.setopen(id);
			}
			GRMap.GAME_CAMERA.SetActive(false);
		}

		public override void onClosed()
		{
			BaseProxy<Shop_a3Proxy>.getInstance().removeEventListener(Shop_a3Proxy.DELETELIMITED, new Action<GameEvent>(this.onDeleteLimited));
			BaseProxy<Shop_a3Proxy>.getInstance().removeEventListener(Shop_a3Proxy.CHANGELIMITED, new Action<GameEvent>(this.onChangeLimited));
			BaseProxy<Shop_a3Proxy>.getInstance().removeEventListener(Shop_a3Proxy.LIMITED, new Action<GameEvent>(this.onLimited));
			GRMap.GAME_CAMERA.SetActive(true);
			base.transform.FindChild("close_desc").gameObject.SetActive(false);
			base.transform.FindChild("objdes").gameObject.SetActive(false);
			base.transform.FindChild("buy_success").gameObject.SetActive(false);
			InterfaceMgr.getInstance().itemToWin(this.Toclose, this.uiName);
		}

		private void onShowgoods()
		{
			this.btni = 2;
			base.getGameObjectByPath("panel_down").SetActive(true);
			base.getGameObjectByPath("contribute").SetActive(false);
			this.shop_data.Clear();
			bool flag = this.nowstate == 0;
			if (!flag)
			{
				this.deletecontain();
				foreach (int current in this.dic.Keys)
				{
					bool flag2 = this.dic[current].type == 1;
					if (flag2)
					{
						this.shop_data.Add(this.dic[current]);
						int key = this.dic[current].itemid;
						int shop_id = this.dic[current].id;
						int money_type = this.dic[current].money_type;
						int item_money = this.dic[current].value;
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image_goodsorbundinggem);
						gameObject.SetActive(true);
						gameObject.transform.SetParent(this.contain.transform, false);
						gameObject.transform.FindChild("bg/name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic[current].itemid).item_name;
						int color = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)key).quality;
						gameObject.transform.FindChild("bg/name").GetComponent<Text>().color = Globle.getColorByQuality(color);
						gameObject.transform.FindChild("bg/Image/price").GetComponent<Text>().text = this.dic[current].value.ToString();
						GameObject gameObject2 = gameObject.transform.FindChild("bg/icon").gameObject;
						string file = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic[current].itemid).file;
						gameObject2.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
						bool flag3 = money_type == 3;
						if (flag3)
						{
							gameObject.transform.FindChild("bg/Image/gold").gameObject.SetActive(true);
							gameObject.transform.FindChild("bg/Image/bangdingbaoshi").gameObject.SetActive(false);
							gameObject.transform.FindChild("bg/Image/contribute").gameObject.SetActive(false);
						}
						else
						{
							gameObject.transform.FindChild("bg/Image/gold").gameObject.SetActive(false);
							gameObject.transform.FindChild("bg/Image/bangdingbaoshi").gameObject.SetActive(true);
							gameObject.transform.FindChild("bg/Image/contribute").gameObject.SetActive(false);
						}
						gameObject.transform.FindChild("bg/surplus_num").gameObject.SetActive(false);
						gameObject.transform.FindChild("bg/Image/money").gameObject.SetActive(false);
						BaseButton baseButton = new BaseButton(gameObject.transform.FindChild("bg/btn").transform, 1, 1);
						baseButton.onClick = delegate(GameObject goo)
						{
							this.objsurebuy.SetActive(true);
							this.surebuy(shop_id, key, 0, money_type, item_money, 2, -1);
						};
						new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject gos)
						{
							this.icon_small.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
							this.close_btn.SetActive(true);
							List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + key);
							foreach (SXML current2 in sXMLList)
							{
								string @string = current2.getString("desc");
								this.text1.text = StringUtils.formatText(@string);
								this.text_name.text = current2.getString("item_name");
								int @int = current2.getInt("use_limit");
								bool flag4 = @int == 0;
								if (flag4)
								{
									this.text_dengji.text = "无限制";
								}
								else
								{
									this.text_dengji.text = current2.getString("use_limit") + "转";
								}
								this.text_name.color = Globle.getColorByQuality(color);
							}
						};
					}
				}
				this.nowstate = 0;
				this.refreshcontain(this.contain, this.image_goodsorbundinggem);
			}
		}

		private void onSummonTab()
		{
			this.btni = 4;
			this.shop_data.Clear();
			base.getGameObjectByPath("panel_down").SetActive(true);
			base.getGameObjectByPath("contribute").SetActive(false);
			bool flag = this.nowstate == 1;
			if (!flag)
			{
				this.deletecontain();
				foreach (int current in this.dic.Keys)
				{
					bool flag2 = this.dic[current].type == 5;
					if (flag2)
					{
						this.shop_data.Add(this.dic[current]);
						int key = this.dic[current].itemid;
						int shop_id = this.dic[current].id;
						int money_type = this.dic[current].money_type;
						int item_money = this.dic[current].value;
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image_goodsorbundinggem);
						gameObject.SetActive(true);
						gameObject.transform.SetParent(this.contain.transform, false);
						gameObject.transform.FindChild("bg/name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic[current].itemid).item_name;
						int color = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)key).quality;
						gameObject.transform.FindChild("bg/name").GetComponent<Text>().color = Globle.getColorByQuality(color);
						gameObject.transform.FindChild("bg/Image/price").GetComponent<Text>().text = this.dic[current].value.ToString();
						GameObject gameObject2 = gameObject.transform.FindChild("bg/icon").gameObject;
						string file = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic[current].itemid).file;
						gameObject2.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
						gameObject.transform.FindChild("bg/Image/gold").gameObject.SetActive(false);
						gameObject.transform.FindChild("bg/Image/bangdingbaoshi").gameObject.SetActive(false);
						gameObject.transform.FindChild("bg/Image/money").gameObject.SetActive(true);
						gameObject.transform.FindChild("bg/Image/contribute").gameObject.SetActive(false);
						gameObject.transform.FindChild("bg/surplus_num").gameObject.SetActive(false);
						BaseButton baseButton = new BaseButton(gameObject.transform.FindChild("bg/btn").transform, 1, 1);
						baseButton.onClick = delegate(GameObject goo)
						{
							this.objsurebuy.SetActive(true);
							this.surebuy(shop_id, key, 0, money_type, item_money, 5, -1);
						};
						new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject gos)
						{
							this.icon_small.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
							this.close_btn.SetActive(true);
							List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + key);
							foreach (SXML current2 in sXMLList)
							{
								this.text1.text = current2.getString("desc");
								this.text_name.text = current2.getString("item_name");
								this.text_dengji.text = current2.getString("use_limit");
								int @int = current2.getInt("use_limit");
								bool flag3 = @int == 0;
								if (flag3)
								{
									this.text_dengji.text = "无限制";
								}
								else
								{
									this.text_dengji.text = current2.getString("use_limit") + "转";
								}
								this.text_name.color = Globle.getColorByQuality(color);
							}
						};
					}
				}
				this.nowstate = 1;
				this.refreshcontain(this.contain, this.image_goodsorbundinggem);
			}
		}

		private void onShowpacks()
		{
			this.btni = 1;
			base.getGameObjectByPath("panel_down").SetActive(true);
			base.getGameObjectByPath("contribute").SetActive(false);
			this.shop_data.Clear();
			this.deletecontain();
			foreach (int current in this.dic.Keys)
			{
				bool flag = this.dic[current].type == 2;
				if (flag)
				{
					this.shop_data.Add(this.dic[current]);
					int key = this.dic[current].itemid;
					int shop_id = this.dic[current].id;
					int money_type = this.dic[current].money_type;
					int item_money = this.dic[current].value;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image_goodsorbundinggem);
					gameObject.SetActive(true);
					gameObject.transform.SetParent(this.contain.transform, false);
					gameObject.transform.FindChild("bg/name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic[current].itemid).item_name;
					int color = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)key).quality;
					gameObject.transform.FindChild("bg/name").GetComponent<Text>().color = Globle.getColorByQuality(color);
					gameObject.transform.FindChild("bg/Image/price").GetComponent<Text>().text = this.dic[current].value.ToString();
					GameObject gameObject2 = gameObject.transform.FindChild("bg/icon").gameObject;
					string file = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic[current].itemid).file;
					gameObject2.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
					gameObject.transform.FindChild("bg/Image/gold").gameObject.SetActive(true);
					gameObject.transform.FindChild("bg/Image/bangdingbaoshi").gameObject.SetActive(false);
					gameObject.transform.FindChild("bg/Image/money").gameObject.SetActive(false);
					gameObject.transform.FindChild("bg/Image/contribute").gameObject.SetActive(false);
					gameObject.transform.FindChild("bg/surplus_num").gameObject.SetActive(false);
					BaseButton baseButton = new BaseButton(gameObject.transform.FindChild("bg/btn").transform, 1, 1);
					baseButton.onClick = delegate(GameObject goo)
					{
						this.objsurebuy.SetActive(true);
						this.surebuy(shop_id, key, ModelBase<Shop_a3Model>.getInstance().itemsdic[shop_id].limiteD, money_type, item_money, 2, -1);
					};
					new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject gos)
					{
						this.icon_small.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
						this.close_btn.SetActive(true);
						List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + key);
						foreach (SXML current2 in sXMLList)
						{
							this.text1.text = current2.getString("desc");
							this.text_name.text = current2.getString("item_name");
							this.text_dengji.text = current2.getString("use_limit");
							int @int = current2.getInt("use_limit");
							bool flag2 = @int == 0;
							if (flag2)
							{
								this.text_dengji.text = "无限制";
							}
							else
							{
								this.text_dengji.text = current2.getString("use_limit") + "转";
							}
							this.text_name.color = Globle.getColorByQuality(color);
						}
					};
				}
			}
			this.nowstate = -1;
			this.refreshcontain(this.contain, this.image_goodsorbundinggem);
		}

		private void onLegion()
		{
			this.shop_data.Clear();
			base.getGameObjectByPath("panel_down").SetActive(false);
			base.getGameObjectByPath("contribute").SetActive(true);
			this.deletecontain();
			using (Dictionary<int, shopDatas>.KeyCollection.Enumerator enumerator = this.dic.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int i = enumerator.Current;
					bool flag = this.dic[i].type == 6;
					if (flag)
					{
						this.shop_data.Add(this.dic[i]);
						int key = this.dic[i].itemid;
						int shop_id = this.dic[i].id;
						int money_type = this.dic[i].money_type;
						int item_money = this.dic[i].value;
						string itemName = this.dic[i].itemName;
						int limiteNum = this.dic[i].limiteNum;
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image_goodsorbundinggem);
						gameObject.SetActive(true);
						gameObject.name = shop_id.ToString();
						gameObject.transform.SetParent(this.contain.transform, false);
						gameObject.transform.FindChild("bg/name").GetComponent<Text>().text = itemName;
						int color = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)key).quality;
						gameObject.transform.FindChild("bg/name").GetComponent<Text>().color = Globle.getColorByQuality(color);
						gameObject.transform.FindChild("bg/Image/price").GetComponent<Text>().text = this.dic[i].value.ToString();
						string text = string.Concat(new object[]
						{
							"兑换次数(",
							ModelBase<Shop_a3Model>.getInstance().itemsdic[i].limiteD,
							"/",
							limiteNum,
							")"
						});
						gameObject.transform.FindChild("bg/surplus_num").GetComponent<Text>().text = text;
						bool flag2 = money_type == 5;
						if (flag2)
						{
							gameObject.transform.FindChild("bg/Image/gold").gameObject.SetActive(false);
							gameObject.transform.FindChild("bg/Image/bangdingbaoshi").gameObject.SetActive(false);
							gameObject.transform.FindChild("bg/Image/contribute").gameObject.SetActive(true);
						}
						GameObject gameObject2 = gameObject.transform.FindChild("bg/icon").gameObject;
						string file = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic[i].itemid).file;
						gameObject2.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
						gameObject.transform.FindChild("bg/surplus_num").gameObject.SetActive(true);
						this.havePurchase[i] = gameObject;
						new BaseButton(gameObject.transform.FindChild("bg/btn").transform, 1, 1).onClick = delegate(GameObject goo)
						{
							bool flag4 = this.dic[i].limiteD == 0;
							if (flag4)
							{
								flytxt.instance.fly("已经达到今日购买上限，明天再来吧！", 1, default(Color), null);
							}
							else
							{
								this.objsurebuy.SetActive(true);
								MonoBehaviour.print("shop_id:" + shop_id);
								this.surebuy(shop_id, key, ModelBase<Shop_a3Model>.getInstance().itemsdic[shop_id].limiteD, money_type, item_money, 6, -1);
							}
						};
						new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject gos)
						{
							this.icon_small.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
							this.close_btn.SetActive(true);
							List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + key);
							foreach (SXML current in sXMLList)
							{
								string @string = current.getString("desc");
								this.text1.text = StringUtils.formatText(@string);
								this.text_name.text = current.getString("item_name");
								this.text_dengji.text = current.getString("use_limit");
								int @int = current.getInt("use_limit");
								bool flag4 = @int == 0;
								if (flag4)
								{
									this.text_dengji.text = "无限制";
								}
								else
								{
									this.text_dengji.text = current.getString("use_limit") + "转";
								}
								this.text_name.color = Globle.getColorByQuality(color);
							}
						};
					}
				}
			}
			int num = 0;
			for (int j = 0; j < this.contain.transform.childCount; j++)
			{
				int key2 = int.Parse(this.contain.transform.GetChild(num).name);
				bool flag3 = ModelBase<Shop_a3Model>.getInstance().itemsdic[key2].limiteD == 0;
				if (flag3)
				{
					this.contain.transform.GetChild(num).transform.SetAsLastSibling();
				}
				else
				{
					num++;
				}
			}
			this.nowstate = 5;
		}

		private void onShowbundinggem()
		{
			this.btni = 3;
			this.shop_data.Clear();
			base.getGameObjectByPath("panel_down").SetActive(true);
			base.getGameObjectByPath("contribute").SetActive(false);
			bool flag = this.nowstate == 3;
			if (!flag)
			{
				this.deletecontain();
				using (Dictionary<int, shopDatas>.KeyCollection.Enumerator enumerator = this.dic.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int i = enumerator.Current;
						bool flag2 = this.dic[i].type == 3;
						if (flag2)
						{
							this.shop_data.Add(this.dic[i]);
							int key = this.dic[i].itemid;
							int shop_id = this.dic[i].id;
							int money_type = this.dic[i].money_type;
							int item_money = this.dic[i].value;
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image_goodsorbundinggem);
							gameObject.SetActive(true);
							gameObject.transform.SetParent(this.contain.transform, false);
							gameObject.transform.FindChild("bg/name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic[i].itemid).item_name;
							int color = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)key).quality;
							gameObject.transform.FindChild("bg/name").GetComponent<Text>().color = Globle.getColorByQuality(color);
							gameObject.transform.FindChild("bg/Image/price").GetComponent<Text>().text = this.dic[i].value.ToString();
							gameObject.transform.FindChild("bg/surplus_num").GetComponent<Text>().text = string.Concat(new object[]
							{
								"今日限购(",
								ModelBase<Shop_a3Model>.getInstance().itemsdic[i].limiteD,
								"/",
								ModelBase<Shop_a3Model>.getInstance().itemsdic[i].limiteNum,
								")"
							});
							bool flag3 = this.dic[i].limiteD == 0;
							if (flag3)
							{
								gameObject.transform.FindChild("buy_over").gameObject.SetActive(false);
							}
							bool flag4 = money_type == 3;
							if (flag4)
							{
								gameObject.transform.FindChild("bg/Image/gold").gameObject.SetActive(true);
								gameObject.transform.FindChild("bg/Image/bangdingbaoshi").gameObject.SetActive(false);
								gameObject.transform.FindChild("bg/Image/contribute").gameObject.SetActive(false);
							}
							else
							{
								gameObject.transform.FindChild("bg/Image/gold").gameObject.SetActive(false);
								gameObject.transform.FindChild("bg/Image/bangdingbaoshi").gameObject.SetActive(true);
								gameObject.transform.FindChild("bg/Image/contribute").gameObject.SetActive(false);
							}
							GameObject gameObject2 = gameObject.transform.FindChild("bg/icon").gameObject;
							string file = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic[i].itemid).file;
							gameObject2.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
							gameObject.transform.FindChild("bg/surplus_num").gameObject.SetActive(true);
							gameObject.transform.FindChild("bg/Image/money").gameObject.SetActive(false);
							BaseButton baseButton = new BaseButton(gameObject.transform.FindChild("bg/btn").transform, 1, 1);
							baseButton.onClick = delegate(GameObject goo)
							{
								bool flag5 = this.dic[i].limiteD == 0;
								if (flag5)
								{
									flytxt.instance.fly("已经达到今日购买上限，明天再来吧！", 1, default(Color), null);
								}
								else
								{
									this.objsurebuy.SetActive(true);
									MonoBehaviour.print("shop_id:" + shop_id);
									this.surebuy(shop_id, key, ModelBase<Shop_a3Model>.getInstance().itemsdic[shop_id].limiteD, money_type, item_money, 2, -1);
								}
							};
							this.havePurchase[i] = gameObject;
							new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject gos)
							{
								this.icon_small.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
								this.close_btn.SetActive(true);
								List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + key);
								foreach (SXML current in sXMLList)
								{
									string @string = current.getString("desc");
									this.text1.text = StringUtils.formatText(@string);
									this.text_name.text = current.getString("item_name");
									this.text_dengji.text = current.getString("use_limit");
									int @int = current.getInt("use_limit");
									bool flag5 = @int == 0;
									if (flag5)
									{
										this.text_dengji.text = "无限制";
									}
									else
									{
										this.text_dengji.text = current.getString("use_limit") + "转";
									}
									this.text_name.color = Globle.getColorByQuality(color);
								}
							};
						}
					}
				}
				this.nowstate = 3;
				this.refreshcontain(this.contain, this.image_goodsorbundinggem);
			}
		}

		private void onShowlimitedactive()
		{
			this.btni = 0;
			this.shop_data.Clear();
			base.getGameObjectByPath("panel_down").SetActive(true);
			base.getGameObjectByPath("contribute").SetActive(false);
			bool flag = this.nowstate == 2;
			if (!flag)
			{
				BaseProxy<Shop_a3Proxy>.getInstance().sendinfo(1, 0, 0, -1);
				this.nowstate = 2;
			}
		}

		private void onChangeLimited(GameEvent e)
		{
			this.CreateLimitedShopItem(e.data);
		}

		private void onDeleteLimited(GameEvent e)
		{
			int activityId = e.data["id"];
			this.RemoveLimitedItemsByActivityId(activityId);
		}

		private void RemoveLimitedItemsByActivityId(int activityId)
		{
			bool flag = this.limitedActivity.ContainsKey(activityId);
			if (flag)
			{
				for (int i = this.limitedActivity[activityId].Count; i > 0; i--)
				{
					int itemIId = this.limitedActivity[activityId].Dequeue();
					UnityEngine.Object.Destroy(this.limitedobj[this.GetLimitedObjIndex(activityId, itemIId)]);
					this.limitedobj.Remove(this.GetLimitedObjIndex(activityId, itemIId));
				}
			}
		}

		private void onLimited(GameEvent e)
		{
			this.deletecontain();
			bool flag = e.data["discounts"].Length > 0;
			if (flag)
			{
				this.nullitems.SetActive(false);
				int i = 0;
				List<Variant> list = new List<Variant>(e.data["discounts"]._arr);
				while (i < list.Count)
				{
					this.CreateLimitedShopItem(list[i]);
					i++;
				}
			}
			else
			{
				this.nullitems.SetActive(true);
			}
		}

		private void CreateLimitedShopItem(Variant v)
		{
			this.btni = 0;
			this.shop_data.Clear();
			bool flag = v["start_time"] != null;
			if (flag)
			{
				this.itiemd_time.text = Globle.getStrTime(v["end_time"], false, false) + "截止";
			}
			int activityId = v["id"];
			string str = v["name"]._str;
			string str2 = v["msg"]._str;
			List<Variant> list = new List<Variant>(v["store"]._arr);
			bool flag2 = !this.limitedActivity.ContainsKey(activityId);
			if (flag2)
			{
				this.limitedActivity.Add(activityId, new Queue<int>());
			}
			else
			{
				this.RemoveLimitedItemsByActivityId(activityId);
				this.limitedActivity[activityId].Clear();
			}
			shopDatas shopDatas = new shopDatas();
			for (int i = 0; i < list.Count; i++)
			{
				int item_iid = list[i]["id"]._int;
				int item_id = list[i]["tpid"]._int;
				int purchase_num2 = list[i]["cnt"];
				shopDatas.id = item_iid;
				this.shop_data.Add(shopDatas);
				int purchase_num = purchase_num2;
				bool flag3 = list[i].ContainsKey("left_num");
				if (flag3)
				{
					purchase_num = list[i]["left_num"];
				}
				int money_type = list[i]["tp"];
				int price = list[i]["cost"];
				uint @uint = list[i]["discount"]._uint;
				int num = (int)Math.Round((double)((float)price / (@uint / 100f)), MidpointRounding.AwayFromZero);
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image_limitedactive);
				gameObject.SetActive(true);
				gameObject.transform.SetParent(this.contain.transform, false);
				gameObject.transform.FindChild("bg/bg1/name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)item_id).item_name;
				int color = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)item_id).quality;
				gameObject.transform.FindChild("bg/bg1/name").GetComponent<Text>().color = Globle.getColorByQuality(color);
				gameObject.transform.FindChild("bg/bg1/price_old").GetComponent<Text>().text = "原价：" + num;
				gameObject.transform.FindChild("bg/bg1/price_now").GetComponent<Text>().text = "现价：" + price;
				gameObject.transform.FindChild("bg/bg1/Image_btn/price").GetComponent<Text>().text = price.ToString();
				gameObject.transform.FindChild("bg/bg1/remain_num/num").GetComponent<Text>().text = "剩余：" + purchase_num;
				gameObject.transform.FindChild("bg/bg1/Image/Text").GetComponent<Text>().text = "折扣" + @uint + "%";
				GameObject gameObject2 = gameObject.transform.FindChild("bg/bg1/icon").gameObject;
				string file = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)item_id).file;
				gameObject2.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
				bool flag4 = money_type == 3;
				if (flag4)
				{
					gameObject.transform.FindChild("bg/bg1/Image_btn/gold").gameObject.SetActive(true);
					gameObject.transform.FindChild("bg/bg1/Image_btn/bangdingbaoshi").gameObject.SetActive(false);
				}
				else
				{
					gameObject.transform.FindChild("bg/bg1/Image_btn/gold").gameObject.SetActive(false);
					gameObject.transform.FindChild("bg/bg1/Image_btn/bangdingbaoshi").gameObject.SetActive(true);
				}
				BaseButton baseButton = new BaseButton(gameObject.transform.FindChild("bg/bg1/btn"), 1, 1);
				baseButton.onClick = delegate(GameObject go)
				{
					bool flag5 = purchase_num == 0;
					if (flag5)
					{
						flytxt.instance.fly("限时抢购剩余次数不足", 1, default(Color), null);
					}
					else
					{
						this.objsurebuy.SetActive(true);
						this.surebuy(activityId, item_id, purchase_num, money_type, price, 3, item_iid);
					}
				};
				this.limitedobj[this.GetLimitedObjIndex(activityId, item_iid)] = gameObject;
				this.limitedActivity[activityId].Enqueue(item_iid);
				new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject gos)
				{
					this.icon_small.GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
					this.close_btn.SetActive(true);
					List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + item_id);
					foreach (SXML current in sXMLList)
					{
						string @string = current.getString("desc");
						this.text1.text = StringUtils.formatText(@string);
						this.text_name.text = current.getString("item_name");
						int @int = current.getInt("use_limit");
						bool flag5 = @int == 0;
						if (flag5)
						{
							this.text_dengji.text = "无限制";
						}
						else
						{
							this.text_dengji.text = current.getString("use_limit") + "转";
						}
						this.text_name.color = Globle.getColorByQuality(color);
					}
				};
			}
			this.nowstate = 2;
		}

		private void surebuy(int shop_id, int item_id, int max_num, int montytype, int itemmoney, int type, int item_iid = -1)
		{
			this.bar.value = 0f;
			this.canbuy = true;
			GameObject gameObject = this.objsurebuy.transform.FindChild("bg/contain/icon").gameObject;
			bool flag = gameObject.transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(gameObject.transform.GetChild(i).gameObject);
				}
			}
			GameObject gameObject2 = IconImageMgr.getInstance().createA3ItemIcon((uint)item_id, false, -1, 1f, true, -1, 0, false, false, false, false);
			gameObject2.transform.SetParent(gameObject.transform, false);
			this.surebuy_name.text = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)item_id).item_name;
			int quality = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)item_id).quality;
			this.surebuy_name.color = Globle.getColorByQuality(quality);
			string str = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)item_id).desc;
			this.surebuy_des.text = StringUtils.formatText(str);
			bool flag2 = max_num == 0;
			if (flag2)
			{
				max_num = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)item_id).maxnum;
			}
			this.objsurebuy.transform.FindChild("bg/contain/paymoney/moneyIco").gameObject.SetActive(false);
			bool flag3 = montytype == 3;
			if (flag3)
			{
				this.objsurebuy.transform.FindChild("bg/contain/paymoney/gold").gameObject.SetActive(true);
				this.objsurebuy.transform.FindChild("bg/contain/paymoney/bangdingbaoshi").gameObject.SetActive(false);
				this.objsurebuy.transform.FindChild("bg/contain/paymoney/contribute").gameObject.SetActive(false);
				bool flag4 = max_num > 0;
				if (flag4)
				{
					bool flag5 = (ulong)ModelBase<PlayerModel>.getInstance().gold >= (ulong)((long)(max_num * itemmoney));
					if (flag5)
					{
						this.maxnum = max_num;
					}
					else
					{
						this.maxnum = (int)(ModelBase<PlayerModel>.getInstance().gold / (uint)itemmoney);
					}
				}
				else
				{
					this.maxnum = (int)(ModelBase<PlayerModel>.getInstance().gold / (uint)itemmoney);
					bool flag6 = this.maxnum > 999;
					if (flag6)
					{
						this.maxnum = 999;
					}
				}
			}
			else
			{
				bool flag7 = montytype == 4;
				if (flag7)
				{
					this.objsurebuy.transform.FindChild("bg/contain/paymoney/gold").gameObject.SetActive(false);
					this.objsurebuy.transform.FindChild("bg/contain/paymoney/contribute").gameObject.SetActive(false);
					this.objsurebuy.transform.FindChild("bg/contain/paymoney/bangdingbaoshi").gameObject.SetActive(true);
					bool flag8 = max_num > 0;
					if (flag8)
					{
						bool flag9 = (ulong)ModelBase<PlayerModel>.getInstance().gift >= (ulong)((long)(max_num * itemmoney));
						if (flag9)
						{
							this.maxnum = max_num;
						}
						else
						{
							this.maxnum = (int)(ModelBase<PlayerModel>.getInstance().gift / (uint)itemmoney);
						}
					}
					else
					{
						this.maxnum = (int)(ModelBase<PlayerModel>.getInstance().gift / (uint)itemmoney);
						bool flag10 = this.maxnum > 999;
						if (flag10)
						{
							this.maxnum = 999;
						}
					}
				}
				else
				{
					bool flag11 = montytype == 2;
					if (flag11)
					{
						this.objsurebuy.transform.FindChild("bg/contain/paymoney/gold").gameObject.SetActive(false);
						this.objsurebuy.transform.FindChild("bg/contain/paymoney/bangdingbaoshi").gameObject.SetActive(false);
						this.objsurebuy.transform.FindChild("bg/contain/paymoney/contribute").gameObject.SetActive(false);
						this.objsurebuy.transform.FindChild("bg/contain/paymoney/moneyIco").gameObject.SetActive(true);
						bool flag12 = max_num > 0;
						if (flag12)
						{
							bool flag13 = (ulong)ModelBase<PlayerModel>.getInstance().money >= (ulong)((long)(max_num * itemmoney));
							if (flag13)
							{
								this.maxnum = max_num;
							}
							else
							{
								this.maxnum = (int)(ModelBase<PlayerModel>.getInstance().money / (uint)itemmoney);
							}
						}
						else
						{
							this.maxnum = (int)(ModelBase<PlayerModel>.getInstance().money / (uint)itemmoney);
							bool flag14 = this.maxnum > 999;
							if (flag14)
							{
								this.maxnum = 999;
							}
						}
					}
					else
					{
						bool flag15 = montytype == 5;
						if (flag15)
						{
							this.objsurebuy.transform.FindChild("bg/contain/paymoney/gold").gameObject.SetActive(false);
							this.objsurebuy.transform.FindChild("bg/contain/paymoney/bangdingbaoshi").gameObject.SetActive(false);
							this.objsurebuy.transform.FindChild("bg/contain/paymoney/moneyIco").gameObject.SetActive(false);
							this.objsurebuy.transform.FindChild("bg/contain/paymoney/contribute").gameObject.SetActive(true);
							bool flag16 = max_num > 0;
							if (flag16)
							{
								bool flag17 = ModelBase<A3_LegionModel>.getInstance().donate >= max_num * itemmoney;
								if (flag17)
								{
									this.maxnum = max_num;
								}
								else
								{
									this.maxnum = ModelBase<A3_LegionModel>.getInstance().donate / itemmoney;
								}
							}
							else
							{
								this.maxnum = ModelBase<A3_LegionModel>.getInstance().donate / itemmoney;
								bool flag18 = this.maxnum > 999;
								if (flag18)
								{
									this.maxnum = 999;
								}
							}
						}
					}
				}
			}
			this.itenmoneys = itemmoney;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("objdes/bg/Button").transform, 1, 1);
			baseButton.onClick = delegate(GameObject goo)
			{
				bool flag19 = int.Parse(this.Inputbuy_num.text) <= 0;
				if (flag19)
				{
					bool flag20 = montytype == 3;
					if (flag20)
					{
						flytxt.instance.fly("宝石不足！！", 1, default(Color), null);
					}
					else
					{
						bool flag21 = montytype == 4;
						if (flag21)
						{
							flytxt.instance.fly("绑定宝石不足！！", 1, default(Color), null);
						}
						else
						{
							bool flag22 = montytype == 2;
							if (flag22)
							{
								flytxt.instance.fly("金币不足！！", 1, default(Color), null);
							}
							else
							{
								bool flag23 = montytype == 5;
								if (flag23)
								{
									flytxt.instance.fly("贡献值不足！！", 1, default(Color), null);
								}
							}
						}
					}
				}
				else
				{
					bool flag24 = type == 3;
					if (flag24)
					{
						BaseProxy<Shop_a3Proxy>.getInstance().sendinfo(type, shop_id, int.Parse(this.Inputbuy_num.text), item_iid);
						limitedinmfos limitedinmfos = new limitedinmfos();
						limitedinmfos.item_id = item_id;
						limitedinmfos.buy_num = int.Parse(this.Inputbuy_num.text);
						MonoBehaviour.print("我购买的数量是：" + limitedinmfos.buy_num);
						this.limitedinmfo[item_iid] = limitedinmfos;
					}
					else
					{
						BaseProxy<Shop_a3Proxy>.getInstance().sendinfo(type, shop_id, int.Parse(this.Inputbuy_num.text), -1);
					}
				}
			};
		}

		private int GetLimitedObjIndex(int activityId, int itemIId)
		{
			return activityId << 9 + itemIId;
		}

		public void Refresh(int id, int num)
		{
			bool flag = this.havePurchase.ContainsKey(id);
			if (flag)
			{
				string text = string.Concat(new object[]
				{
					"今日限购(",
					this.dic[id].limiteD,
					"/",
					ModelBase<Shop_a3Model>.getInstance().itemsdic[id].limiteNum,
					")"
				});
				this.havePurchase[id].transform.FindChild("bg/surplus_num").GetComponent<Text>().text = text;
			}
			bool flag2 = this.dic[id].limiteD == 0;
			if (flag2)
			{
				this.havePurchase[id].transform.FindChild("buy_over").gameObject.SetActive(false);
				BaseButton arg_11A_0 = new BaseButton(this.havePurchase[id].transform.FindChild("bg/btn"), 1, 1);
				Action<GameObject> arg_11A_1;
				if ((arg_11A_1 = shop_a3.<>c.<>9__86_0) == null)
				{
					arg_11A_1 = (shop_a3.<>c.<>9__86_0 = new Action<GameObject>(shop_a3.<>c.<>9.<Refresh>b__86_0));
				}
				arg_11A_0.onClick = arg_11A_1;
			}
			this.objsurebuy.SetActive(false);
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon((uint)this.dic[id].itemid, false, -1, 1f, true, -1, 0, false, false, false, false);
			string txt = string.Concat(new object[]
			{
				"你购买了",
				ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic[id].itemid).item_name,
				"*",
				num
			});
			flytxt.instance.fly(txt, 0, default(Color), null);
			this.refreshGold();
			this.refreshGift();
			this.refreshCoin();
		}

		public void Refresh_limited(int activityId, int id, int num)
		{
			this.limitedobj[this.GetLimitedObjIndex(activityId, id)].transform.FindChild("bg/bg1/remain_num/num").GetComponent<Text>().text = "剩余：" + num;
			bool flag = num == 0;
			if (flag)
			{
				this.limitedobj[this.GetLimitedObjIndex(activityId, id)].transform.FindChild("buy_over").gameObject.SetActive(true);
			}
			this.objsurebuy.SetActive(false);
			string txt = string.Concat(new object[]
			{
				"你购买了",
				ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.limitedinmfo[id].item_id).item_name,
				"*",
				this.limitedinmfo[id].buy_num
			});
			flytxt.instance.fly(txt, 0, default(Color), null);
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon((uint)this.limitedinmfo[id].item_id, false, -1, 1f, true, -1, 0, false, false, false, false);
			this.objsurebuy.SetActive(false);
			this.refreshGold();
			this.refreshGift();
			this.refreshCoin();
		}

		public void timeCountdown(int time)
		{
			this.times = time;
			base.InvokeRepeating("Countdown", 0f, 1f);
		}

		private void Countdown()
		{
			this.times--;
			bool flag = this.times <= 0;
			if (flag)
			{
				BaseProxy<Shop_a3Proxy>.getInstance().sendinfo(1, 0, 0, -1);
				this.times = 86400;
			}
		}

		private void Update()
		{
			bool flag = this.canbuy;
			if (flag)
			{
				bool flag2 = this.Inputbuy_num.text != null;
				if (flag2)
				{
					int num;
					bool flag3 = int.TryParse(this.Inputbuy_num.text, out num);
					if (!flag3)
					{
						this.Inputbuy_num.text = "1";
						return;
					}
					int num2 = num;
					this.Inputbuy_num.text = num2.ToString();
				}
				bool flag4 = Convert.ToInt32(this.Inputbuy_num.text) > this.maxnum;
				if (flag4)
				{
					this.Inputbuy_num.text = this.maxnum.ToString();
				}
				bool flag5 = Convert.ToInt32(this.Inputbuy_num.text) > 999;
				if (flag5)
				{
					this.Inputbuy_num.text = Convert.ToString(999);
				}
				this.reduceoradd = 1f / ((float)this.maxnum + 1f);
				this.needbuymoney.text = (int.Parse(this.Inputbuy_num.text) * this.itenmoneys).ToString();
				bool isFocused = this.Inputbuy_num.isFocused;
				if (isFocused)
				{
					this.bar.value = float.Parse(this.Inputbuy_num.text) / (float)this.maxnum;
				}
				else
				{
					bool flag6 = this.maxnum > 0;
					if (flag6)
					{
						bool flag7 = this.bar.value == 0f;
						if (flag7)
						{
							this.Inputbuy_num.text = Convert.ToString(1);
						}
						else
						{
							bool flag8 = this.bar.value == 1f;
							if (flag8)
							{
								this.Inputbuy_num.text = Convert.ToString(this.maxnum);
							}
							else
							{
								this.Inputbuy_num.text = ((int)Mathf.Ceil(this.bar.value * (float)this.maxnum)).ToString();
							}
						}
					}
					else
					{
						this.bar.value = 1f;
					}
				}
				bool flag9 = this.maxnum == 1;
				if (flag9)
				{
					this.bar.value = 1f;
				}
			}
		}

		public void refreinfo(int title_nowid)
		{
			shop_a3.now_id = title_nowid;
		}

		private void onLefts(GameObject go)
		{
			MonoBehaviour.print("reduceoradd是：" + this.reduceoradd);
			this.bar.value -= this.reduceoradd;
		}

		private void onRights(GameObject go)
		{
			bool flag = this.bar.value == 0f;
			if (flag)
			{
				this.bar.value += this.reduceoradd;
			}
			this.bar.value += this.reduceoradd;
		}

		private void onmin(GameObject go)
		{
			this.bar.value = 0f;
		}

		private void onmax(GameObject go)
		{
			this.bar.value = 1f;
		}

		private void deletecontain()
		{
			this.nullitems.SetActive(false);
			this.nownum = this.contain.transform.childCount;
			bool flag = this.nownum > 0;
			if (flag)
			{
				for (int i = this.nownum; i > 0; i--)
				{
					UnityEngine.Object.DestroyImmediate(this.contain.transform.GetChild(i - 1).gameObject);
				}
			}
			this.itiemd_time.text = "";
		}

		private void refreshcontain(GameObject contain, GameObject go)
		{
		}

		private void onClose(GameObject go)
		{
			this.Toclose = true;
			InterfaceMgr.getInstance().close(InterfaceMgr.SHOP_A3);
		}

		private void onClose1(GameObject go)
		{
			this.canbuy = false;
			this.objsurebuy.SetActive(false);
		}

		private void onClose3(GameObject go)
		{
			this.buy_success.SetActive(false);
		}

		public void refreshGold()
		{
			this.money.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().gold);
		}

		public void refreshGift()
		{
			this.gold.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
		}

		public void refreshCoin()
		{
			this.coin.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().money);
		}

		private void OnChongzhi(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
			InterfaceMgr.getInstance().close(InterfaceMgr.SHOP_A3);
		}
	}
}
