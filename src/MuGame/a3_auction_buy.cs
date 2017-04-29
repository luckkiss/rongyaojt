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
	internal class a3_auction_buy : a3BaseAuction
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_auction_buy.<>c <>9 = new a3_auction_buy.<>c();

			public static Action<GameObject> <>9__25_4;

			internal void <init>b__25_4(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
			}
		}

		private GameObject _prefab;

		private Transform _content;

		private GridLayoutGroup _glg;

		private GameObject _select;

		private Dictionary<uint, GameObject> gos = new Dictionary<uint, GameObject>();

		private uint _selectId;

		private int _page_index;

		private int _page_max;

		private int _goods_count;

		private BaseButton btn_refresh;

		private BaseButton btn_buy;

		private BaseButton btn_help;

		private BaseButton btn_left;

		private BaseButton btn_right;

		private Dictionary<string, uint> selectSearch = new Dictionary<string, uint>();

		private Transform help;

		private Transform help_par;

		private string searchStr;

		private InputField inputf;

		private int num;

		private int maxNum;

		private List<SXML> listxml = new List<SXML>();

		private int selectMax;

		private int selectPrice;

		public a3_auction_buy(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			Transform transform = base.main.etra.FindChild("info");
			Transform transform2 = base.main.djtip.FindChild("info");
			this.searchStr = null;
			this._prefab = base.transform.FindChild("cells/scroll/0").gameObject;
			this._content = base.transform.FindChild("cells/scroll/content");
			this._glg = this._content.GetComponent<GridLayoutGroup>();
			this._select = base.transform.FindChild("cells/scroll/select").gameObject;
			this.help = base.transform.FindChild("help/panel_help");
			this.help_par = base.transform.FindChild("help");
			this.btn_refresh = new BaseButton(base.transform.FindChild("refresh"), 1, 1);
			this.btn_buy = new BaseButton(base.transform.FindChild("buy"), 1, 1);
			this.btn_help = new BaseButton(base.transform.FindChild("description"), 1, 1);
			this.btn_left = new BaseButton(base.transform.FindChild("count/left"), 1, 1);
			this.btn_right = new BaseButton(base.transform.FindChild("count/right"), 1, 1);
			this.btn_buy.onClick = delegate(GameObject go)
			{
				bool flag4 = this._selectId != 0u && ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].confdata.equip_type > 0;
				if (flag4)
				{
					uint cid = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].auctiondata.cid;
					BaseProxy<A3_AuctionProxy>.getInstance().SendBuyMsg(this._selectId, cid, (uint)this.maxNum);
				}
				else
				{
					bool flag5 = this._selectId != 0u && ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].confdata.equip_type < 1;
					if (flag5)
					{
						uint cid2 = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].auctiondata.cid;
						bool flag6 = cid2 == ModelBase<PlayerModel>.getInstance().cid;
						if (flag6)
						{
							flytxt.instance.fly(ContMgr.getCont("auction_tag0", null), 0, default(Color), null);
						}
						else
						{
							this.listxml.Clear();
							this.listxml = XMLMgr.instance.GetSXMLList("item.item", "id==" + ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].confdata.tpid);
							base.getTransformByPath("buynum/bg/contain/des_bg/Text").GetComponent<Text>().text = StringUtils.formatText(this.listxml[0].getString("desc"));
							base.getTransformByPath("buynum/bg/contain/name").GetComponent<Text>().text = this.listxml[0].getString("item_name");
							base.getTransformByPath("buynum/bg/contain/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + this.listxml[0].getString("icon_file"));
							base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].auctiondata.cost / this.maxNum).ToString();
							this.selectPrice = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].auctiondata.cost / this.maxNum;
							this.selectMax = this.maxNum;
							base.getGameObjectByPath("buynum").SetActive(true);
						}
					}
				}
			};
			this.btn_left.onClick = delegate(GameObject go)
			{
				this._page_index = Mathf.Max(0, this._page_index - 1);
				this.SendSearch((uint)this._page_index);
				this._content.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
			};
			this.btn_right.onClick = delegate(GameObject go)
			{
				this._page_index = Mathf.Min(this._page_max, this._page_index + 1);
				this.SendSearch((uint)this._page_index);
				this._content.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
			};
			this.btn_refresh.onClick = delegate(GameObject go)
			{
				this._page_index = 0;
				this.SendSearch((uint)this._page_index);
				this._content.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
			};
			BaseButton arg_1E4_0 = new BaseButton(base.transform.FindChild("fg/gold/add"), 1, 1);
			Action<GameObject> arg_1E4_1;
			if ((arg_1E4_1 = a3_auction_buy.<>c.<>9__25_4) == null)
			{
				arg_1E4_1 = (a3_auction_buy.<>c.<>9__25_4 = new Action<GameObject>(a3_auction_buy.<>c.<>9.<init>b__25_4));
			}
			arg_1E4_0.onClick = arg_1E4_1;
			new BaseButton(base.transform.FindChild("help/panel_help/closeBtn"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.help.gameObject.SetActive(false);
				this.help.SetParent(this.help_par);
			};
			new BaseButton(base.transform.FindChild("help/panel_help/bg_0"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.help.gameObject.SetActive(false);
				this.help.SetParent(this.help_par);
			};
			this.btn_help.onClick = delegate(GameObject go)
			{
				this.help.gameObject.SetActive(true);
				this.help.SetParent(base.main.transform);
			};
			new BaseButton(base.getTransformByPath("buynum/bg/contain/btn_reduce"), 1, 1).onClick = delegate(GameObject go)
			{
				int num;
				bool flag4 = int.TryParse(this.inputf.text, out num);
				if (flag4)
				{
				}
				bool flag5 = num - 1 < 1;
				if (!flag5)
				{
					this.inputf.text = (num - 1).ToString();
					bool flag6 = int.TryParse(this.inputf.text, out this.num);
					if (flag6)
					{
					}
					base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (this.num * this.selectPrice).ToString();
				}
			};
			new BaseButton(base.getTransformByPath("buynum/bg/contain/btn_add"), 1, 1).onClick = delegate(GameObject go)
			{
				int num;
				bool flag4 = int.TryParse(this.inputf.text, out num);
				if (flag4)
				{
				}
				bool flag5 = num + 1 > this.selectMax;
				if (!flag5)
				{
					this.inputf.text = (num + 1).ToString();
					bool flag6 = int.TryParse(this.inputf.text, out this.num);
					if (flag6)
					{
					}
					base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (this.num * this.selectPrice).ToString();
				}
			};
			new BaseButton(base.getTransformByPath("buynum/bg/contain/min"), 1, 1).onClick = delegate(GameObject go)
			{
				this.inputf.text = 1.ToString();
				bool flag4 = int.TryParse(this.inputf.text, out this.num);
				if (flag4)
				{
				}
				base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (this.num * this.selectPrice).ToString();
			};
			new BaseButton(base.getTransformByPath("buynum/bg/contain/max"), 1, 1).onClick = delegate(GameObject go)
			{
				this.inputf.text = this.selectMax.ToString();
				bool flag4 = int.TryParse(this.inputf.text, out this.num);
				if (flag4)
				{
				}
				base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (this.num * this.selectPrice).ToString();
			};
			BaseProxy<A3_AuctionProxy>.getInstance().addEventListener(A3_AuctionProxy.EVENT_LOADALL, new Action<GameEvent>(this.Event_OnLoadAll));
			Transform transform3 = base.transform.FindChild("filt");
			Button[] componentsInChildren = transform3.GetComponentsInChildren<Button>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				a3_auction_buy.<>c__DisplayClass25_1 <>c__DisplayClass25_ = new a3_auction_buy.<>c__DisplayClass25_1();
				<>c__DisplayClass25_.<>4__this = this;
				<>c__DisplayClass25_.v = componentsInChildren[i];
				bool flag = <>c__DisplayClass25_.v.transform.parent == transform3;
				if (flag)
				{
					new BaseButton(<>c__DisplayClass25_.v.transform, 1, 1).onClick = new Action<GameObject>(this.UI_Filt);
					Transform transform4 = base.transform.FindChild("filt/box/" + <>c__DisplayClass25_.v.name + "/0");
					bool flag2 = transform4 != null;
					if (flag2)
					{
						Button[] componentsInChildren2 = transform4.GetComponentsInChildren<Button>(true);
						for (int j = 0; j < componentsInChildren2.Length; j++)
						{
							a3_auction_buy.<>c__DisplayClass25_2 <>c__DisplayClass25_2 = new a3_auction_buy.<>c__DisplayClass25_2();
							<>c__DisplayClass25_2.CS$<>8__locals1 = <>c__DisplayClass25_;
							<>c__DisplayClass25_2.bt = componentsInChildren2[j];
							List<string> temp = new List<string>();
							temp.Add(<>c__DisplayClass25_2.CS$<>8__locals1.v.name);
							new BaseButton(<>c__DisplayClass25_2.bt.transform, 1, 1).onClick = delegate(GameObject go)
							{
								<>c__DisplayClass25_2.CS$<>8__locals1.<>4__this.UI_SelectFilt(temp[0], int.Parse(go.name));
								bool flag4 = <>c__DisplayClass25_2.CS$<>8__locals1.v.name == "part" && <>c__DisplayClass25_2.bt.name == "1";
								if (flag4)
								{
									<>c__DisplayClass25_2.CS$<>8__locals1.<>4__this.getTransformByPath("filt/level").GetComponent<Button>().interactable = false;
									<>c__DisplayClass25_2.CS$<>8__locals1.<>4__this.getTransformByPath("filt/crr").GetComponent<Button>().interactable = false;
								}
								else
								{
									<>c__DisplayClass25_2.CS$<>8__locals1.<>4__this.getTransformByPath("filt/level").GetComponent<Button>().interactable = true;
									<>c__DisplayClass25_2.CS$<>8__locals1.<>4__this.getTransformByPath("filt/crr").GetComponent<Button>().interactable = true;
								}
							};
						}
					}
				}
			}
			new BaseButton(base.transform.FindChild("buynum/btn"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("buynum").SetActive(false);
				this.inputf.text = 1.ToString();
			};
			this.inputf = base.getTransformByPath("buynum/bg/contain/bug/InputField").GetComponent<InputField>();
			this.inputf.text = 1.ToString();
			bool flag3 = int.TryParse(this.inputf.text, out this.num);
			if (flag3)
			{
			}
			new BaseButton(base.getTransformByPath("buynum/bg/ok"), 1, 1).onClick = delegate(GameObject go)
			{
				uint cid = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].auctiondata.cid;
				BaseProxy<A3_AuctionProxy>.getInstance().SendBuyMsg(this._selectId, cid, (uint)this.num);
				base.getGameObjectByPath("buynum").SetActive(false);
				this.inputf.text = 1.ToString();
			};
			this.inputf.onValueChanged.AddListener(delegate(string ss)
			{
				bool flag4 = int.Parse(this.inputf.text) > this.selectMax;
				if (flag4)
				{
					this.inputf.text = this.selectMax.ToString();
					this.num = this.selectMax;
				}
				else
				{
					this.num = int.Parse(this.inputf.text);
				}
				base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (this.num * this.selectPrice).ToString();
			});
			new BaseButton(base.transform.FindChild("fg/search/btn_search"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.searchStr = base.transform.FindChild("fg/search/InputField").GetComponent<InputField>().text;
				this.SendSearch((uint)this._page_index);
			};
			base.transform.FindChild("fg/search/InputField").GetComponent<InputField>().onValueChanged.AddListener(delegate(string ss)
			{
				bool flag4 = ss == "";
				if (flag4)
				{
					this.searchStr = "";
					this.SendSearch((uint)this._page_index);
				}
			});
			new BaseButton(base.main.etra.transform.FindChild("info/buy"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				bool flag4 = this._selectId != 0u && ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].confdata.equip_type > 0;
				if (flag4)
				{
					uint cid = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].auctiondata.cid;
					BaseProxy<A3_AuctionProxy>.getInstance().SendBuyMsg(this._selectId, cid, (uint)this.maxNum);
				}
				base.main.etra.gameObject.SetActive(false);
			};
			new BaseButton(base.main.djtip.transform.FindChild("info/buy"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.inputf.text = 1.ToString();
				this.listxml.Clear();
				this.listxml = XMLMgr.instance.GetSXMLList("item.item", "id==" + ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].confdata.tpid);
				base.getTransformByPath("buynum/bg/contain/des_bg/Text").GetComponent<Text>().text = StringUtils.formatText(this.listxml[0].getString("desc"));
				base.getTransformByPath("buynum/bg/contain/name").GetComponent<Text>().text = this.listxml[0].getString("item_name");
				base.getTransformByPath("buynum/bg/contain/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + this.listxml[0].getString("icon_file"));
				base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].auctiondata.cost / this.maxNum).ToString();
				this.selectPrice = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].auctiondata.cost / this.maxNum;
				this.selectMax = this.maxNum;
				base.getGameObjectByPath("buynum").SetActive(true);
				base.main.djtip.gameObject.SetActive(false);
			};
		}

		public override void onShowed()
		{
			this.searchStr = null;
			this._page_index = 0;
			this._page_max = 0;
			this._goods_count = 0;
			this.btn_left.interactable = false;
			this.btn_right.interactable = false;
			this.selectSearch.Clear();
			this.selectSearch["updown"] = 0u;
			this.UI_SelectFiltName("part", 0);
			this.UI_SelectFiltName("grade", 0);
			this.UI_SelectFiltName("level", 0);
			this.UI_SelectFiltName("crr", 0);
			Transform transform = base.transform.FindChild("filt/price/sellsortup");
			Transform transform2 = base.transform.FindChild("filt/price/sellsortdown");
			transform.gameObject.SetActive(true);
			transform2.gameObject.SetActive(false);
			base.transform.FindChild("fg/gold/Text").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().gold.ToString();
			base.transform.FindChild("fg/search/InputField").GetComponent<InputField>().text = "";
			this.btn_buy.interactable = false;
			this._selectId = 0u;
			this._select.SetActive(false);
			BaseProxy<A3_AuctionProxy>.getInstance().addEventListener(A3_AuctionProxy.EVENT_LOADALL, new Action<GameEvent>(this.Event_OnLoadAll));
			BaseProxy<A3_AuctionProxy>.getInstance().addEventListener(A3_AuctionProxy.EVENT_BUYSUCCESS, new Action<GameEvent>(this.Event_BuySuccess));
			BaseProxy<A3_AuctionProxy>.getInstance().SendSearchMsg(0u, 0u, 0u, 0u, 0u, 0u, null);
			base.transform.FindChild("count/count").GetComponent<Text>().text = string.Concat(new object[]
			{
				"(",
				0,
				" / ",
				0,
				")"
			});
			this._content.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
		}

		public void SetUIData(uint id, a3_BagItemData data)
		{
			bool flag = !this.gos.ContainsKey(id);
			if (!flag)
			{
				Image component = this.gos[id].transform.FindChild("icon").GetComponent<Image>();
				component.sprite = (Resources.Load(data.confdata.file, typeof(Sprite)) as Sprite);
				ArrayList al = new ArrayList();
				al.Add(data);
				al.Add(this.gos[id]);
				new BaseButton(component.transform, 1, 1).onClick = delegate(GameObject gg)
				{
					this._select.transform.SetParent(((GameObject)al[1]).transform);
					this._select.transform.localPosition = Vector3.zero;
					this._select.SetActive(true);
					this._select.transform.SetSiblingIndex(1);
					this._selectId = ((a3_BagItemData)al[0]).id;
					this.btn_buy.interactable = true;
					this.maxNum = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].num;
					bool flag7 = data.confdata.equip_type < 1;
					if (flag7)
					{
						this.justShowBuynum();
					}
					else
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add((a3_BagItemData)al[0]);
						arrayList.Add(equip_tip_type.tip_forAuc_buy);
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
					}
				};
				bool flag2 = data.confdata.job_limit == ModelBase<PlayerModel>.getInstance().profession;
				if (flag2)
				{
					bool flag3 = ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(data.confdata.equip_type);
					if (flag3)
					{
						a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquipsByType()[data.confdata.equip_type];
						bool flag4 = a3_BagItemData.equipdata.combpt > data.equipdata.combpt;
						if (flag4)
						{
							this.gos[id].transform.FindChild("arrow").gameObject.SetActive(false);
						}
						else
						{
							this.gos[id].transform.FindChild("arrow").gameObject.SetActive(true);
						}
					}
					else
					{
						this.gos[id].transform.FindChild("arrow").gameObject.SetActive(true);
					}
				}
				this.gos[id].transform.FindChild("name").GetComponent<Text>().text = data.confdata.item_name;
				this.gos[id].transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(data.confdata.quality);
				this.gos[id].transform.FindChild("lv").GetComponent<Text>().text = string.Concat(new object[]
				{
					"+",
					data.equipdata.intensify_lv,
					"追 ",
					data.equipdata.add_level
				});
				this.gos[id].transform.FindChild("seller").GetComponent<Text>().text = data.auctiondata.seller;
				this.gos[id].transform.FindChild("grade").GetComponent<Text>().text = data.equipdata.stage + " 阶";
				this.gos[id].transform.FindChild("addlv").GetComponent<Text>().text = "追 " + data.equipdata.add_level;
				bool flag5 = data.confdata.equip_type < 1;
				if (flag5)
				{
					this.gos[id].transform.FindChild("num").gameObject.SetActive(true);
					this.gos[id].transform.FindChild("num").GetComponent<Text>().text = data.num.ToString();
				}
				this.gos[id].transform.FindChild("pro/war").gameObject.SetActive(false);
				this.gos[id].transform.FindChild("pro/mage").gameObject.SetActive(false);
				this.gos[id].transform.FindChild("pro/ass").gameObject.SetActive(false);
				switch (data.confdata.job_limit)
				{
				case 2:
					this.gos[id].transform.FindChild("pro/war").gameObject.SetActive(true);
					break;
				case 3:
					this.gos[id].transform.FindChild("pro/mage").gameObject.SetActive(true);
					break;
				case 4:
					this.gos[id].transform.FindChild("pro/war").gameObject.SetActive(true);
					break;
				case 5:
					this.gos[id].transform.FindChild("pro/ass").gameObject.SetActive(true);
					break;
				}
				bool flag6 = data.confdata.equip_type > 0;
				if (flag6)
				{
					this.gos[id].transform.FindChild("sell").GetComponent<Text>().text = data.auctiondata.cost + "钻";
				}
				else
				{
					this.gos[id].transform.FindChild("sell").GetComponent<Text>().text = data.auctiondata.cost / ModelBase<A3_AuctionModel>.getInstance().GetItems()[id].num + "钻/个";
				}
			}
		}

		private void justShowBuynum()
		{
			this.inputf.text = 1.ToString();
			this.listxml.Clear();
			this.listxml = XMLMgr.instance.GetSXMLList("item.item", "id==" + ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].confdata.tpid);
			base.getTransformByPath("buynum/bg/contain/des_bg/Text").GetComponent<Text>().text = StringUtils.formatText(this.listxml[0].getString("desc"));
			base.getTransformByPath("buynum/bg/contain/name").GetComponent<Text>().text = this.listxml[0].getString("item_name");
			base.getTransformByPath("buynum/bg/contain/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + this.listxml[0].getString("icon_file"));
			base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].auctiondata.cost / this.maxNum).ToString();
			this.selectPrice = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].auctiondata.cost / this.maxNum;
			this.selectMax = this.maxNum;
			base.getGameObjectByPath("buynum").SetActive(true);
		}

		private void Event_OnLoadAll(GameEvent e)
		{
			this._page_max = 0;
			bool flag = this.gos.Count > 0;
			if (flag)
			{
				this._select.transform.SetParent(base.transform);
				Transform[] componentsInChildren = this._content.GetComponentsInChildren<Transform>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					bool flag2 = transform.parent == this._content;
					if (flag2)
					{
						UnityEngine.Object.Destroy(transform.gameObject);
					}
				}
				this.gos.Clear();
			}
			Variant data = e.data;
			bool flag3 = data.ContainsKey("count");
			if (flag3)
			{
				this._goods_count = data["count"];
			}
			this._page_max = Mathf.CeilToInt((float)this._goods_count / 8f) - 1;
			this._page_max = Mathf.Max(0, this._page_max);
			this._page_index = Mathf.Min(this._page_index, this._page_max);
			base.transform.FindChild("count/count").GetComponent<Text>().text = string.Concat(new object[]
			{
				"(",
				this._page_index + 1,
				" / ",
				this._page_max + 1,
				")"
			});
			bool flag4 = this._page_max == this._page_index;
			if (flag4)
			{
				this.btn_right.interactable = false;
			}
			else
			{
				this.btn_right.interactable = true;
			}
			bool flag5 = this._page_index > 0;
			if (flag5)
			{
				this.btn_left.interactable = true;
			}
			else
			{
				this.btn_left.interactable = false;
			}
			this._selectId = 0u;
			this._select.transform.SetParent(base.transform);
			this._select.SetActive(false);
			List<a3_BagItemData> list = new List<a3_BagItemData>(ModelBase<A3_AuctionModel>.getInstance().GetItems().Values);
			foreach (a3_BagItemData current in list)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._prefab);
				gameObject.transform.SetParent(this._content);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.SetActive(true);
				List<a3_BagItemData> tp = new List<a3_BagItemData>();
				tp.Add(current);
				new BaseButton(gameObject.transform, 1, 1).onClick = delegate(GameObject g)
				{
					this._select.transform.SetParent(g.transform);
					this._select.transform.localPosition = Vector3.zero;
					this._select.SetActive(true);
					this._select.transform.SetSiblingIndex(1);
					this._selectId = tp[0].id;
					this.maxNum = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this._selectId].num;
					this.btn_buy.interactable = true;
				};
				this.gos[current.id] = gameObject;
				this.SetUIData(current.id, current);
			}
			this._content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (this._glg.cellSize.y + 0.1f) * (float)list.Count);
			this.btn_buy.interactable = false;
		}

		private void Event_BuySuccess(GameEvent e)
		{
			this._selectId = 0u;
			this._select.transform.SetParent(base.transform);
			this._select.SetActive(false);
			this.btn_buy.interactable = false;
			base.transform.FindChild("fg/gold/Text").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().gold.ToString();
			this._page_index = 0;
			this.SendSearch((uint)this._page_index);
			this._content.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
			Variant data = e.data;
			Variant variant = data["get_list"];
			foreach (Variant current in variant._arr)
			{
				int num = current["cid"];
				uint key = current["id"];
				bool flag = this.gos.ContainsKey(key);
				if (flag)
				{
					UnityEngine.Object.Destroy(this.gos[key]);
					this.gos.Remove(key);
				}
				bool flag2 = (long)num != (long)((ulong)ModelBase<PlayerModel>.getInstance().cid);
				if (flag2)
				{
					bool flag3 = current["equip_type"] > 0;
					if (flag3)
					{
						bool flag4 = current.ContainsKey("name");
						if (flag4)
						{
							flytxt.instance.fly("成功购买了1件" + current["name"] + "!", 0, default(Color), null);
						}
					}
					else
					{
						bool flag5 = current.ContainsKey("name");
						if (flag5)
						{
							flytxt.instance.fly(string.Concat(new string[]
							{
								"成功购买了",
								current["num"],
								"个",
								current["name"],
								"!"
							}), 0, default(Color), null);
						}
					}
				}
				else
				{
					bool flag6 = current.ContainsKey("name");
					if (flag6)
					{
						flytxt.instance.fly("成功拍卖了" + current["name"] + "!", 0, default(Color), null);
					}
				}
			}
			bool activeSelf = base.main.etra.gameObject.activeSelf;
			if (activeSelf)
			{
				base.main.etra.gameObject.SetActive(false);
			}
		}

		public override void onClose()
		{
			this.searchStr = null;
			this._selectId = 0u;
			BaseProxy<A3_AuctionProxy>.getInstance().removeEventListener(A3_AuctionProxy.EVENT_LOADALL, new Action<GameEvent>(this.Event_OnLoadAll));
			BaseProxy<A3_AuctionProxy>.getInstance().removeEventListener(A3_AuctionProxy.EVENT_BUYSUCCESS, new Action<GameEvent>(this.Event_BuySuccess));
			this._select.transform.SetParent(base.transform);
			Transform[] componentsInChildren = this._content.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent == this._content;
				if (flag)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			this.gos.Clear();
		}

		private void UI_Filt(GameObject go)
		{
			Transform transform = base.transform.FindChild("filt/box/" + go.name);
			bool flag = transform != null;
			if (flag)
			{
				bool flag2 = !transform.gameObject.activeSelf;
				if (flag2)
				{
					Transform y = base.transform.FindChild("filt/box/");
					Transform[] componentsInChildren = base.transform.FindChild("filt/box/").GetComponentsInChildren<Transform>(true);
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						Transform transform2 = componentsInChildren[i];
						bool flag3 = transform2.parent == y;
						if (flag3)
						{
							bool activeSelf = transform2.gameObject.activeSelf;
							if (activeSelf)
							{
								transform2.gameObject.SetActive(false);
							}
						}
					}
				}
				transform.gameObject.SetActive(!transform.gameObject.activeSelf);
			}
			bool flag4 = go.name == "price";
			if (flag4)
			{
				Transform transform3 = go.transform.FindChild("sellsortup");
				Transform transform4 = go.transform.FindChild("sellsortdown");
				transform3.gameObject.SetActive(!transform3.gameObject.activeSelf);
				transform4.gameObject.SetActive(!transform4.gameObject.activeSelf);
				this.selectSearch["updown"] = (transform3.gameObject.activeSelf ? 0u : 1u);
				this._page_index = 0;
				this.SendSearch((uint)this._page_index);
			}
		}

		private void UI_SelectFilt(string type, int id)
		{
			this.selectSearch[type] = (uint)id;
			Transform transform = base.transform.FindChild("filt/box/" + type);
			bool flag = transform != null;
			if (flag)
			{
				transform.gameObject.SetActive(false);
			}
			this.UI_SelectFiltName(type, id);
			this._page_index = 0;
			this.SendSearch((uint)this._page_index);
		}

		private void UI_SelectFiltName(string type, int id)
		{
			this.selectSearch[type] = (uint)id;
			string text = base.transform.FindChild(string.Concat(new object[]
			{
				"filt/box/",
				type,
				"/0/",
				id,
				"/Text"
			})).GetComponent<Text>().text;
			base.transform.FindChild("filt/" + type + "/Text").GetComponent<Text>().text = text;
		}

		private void SendSearch(uint page)
		{
			bool flag = !this.selectSearch.ContainsKey("updown");
			if (flag)
			{
				this.selectSearch["updown"] = 0u;
			}
			bool flag2 = !this.selectSearch.ContainsKey("crr");
			if (flag2)
			{
				this.selectSearch["crr"] = 0u;
			}
			bool flag3 = !this.selectSearch.ContainsKey("part");
			if (flag3)
			{
				this.selectSearch["part"] = 0u;
			}
			bool flag4 = !this.selectSearch.ContainsKey("level");
			if (flag4)
			{
				this.selectSearch["level"] = 0u;
			}
			bool flag5 = !this.selectSearch.ContainsKey("grade");
			if (flag5)
			{
				this.selectSearch["grade"] = 0u;
			}
			BaseProxy<A3_AuctionProxy>.getInstance().SendSearchMsg(page, this.selectSearch["updown"], this.selectSearch["crr"], this.selectSearch["part"], this.selectSearch["level"], this.selectSearch["grade"], this.searchStr);
		}
	}
}
