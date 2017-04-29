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
	internal class a3_auction_sell : a3BaseAuction
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_auction_sell.<>c <>9 = new a3_auction_sell.<>c();

			public static Action<GameObject> <>9__23_1;

			public static Action<GameObject> <>9__23_2;

			internal void <init>b__23_1(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				bool flag = a3_Recharge.Instance != null;
				if (flag)
				{
					a3_Recharge.Instance.transform.SetAsLastSibling();
				}
			}

			internal void <init>b__23_2(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EXCHANGE, null, false);
				bool flag = a3_exchange.Instance != null;
				if (flag)
				{
					a3_exchange.Instance.transform.SetAsLastSibling();
				}
			}
		}

		private Transform _content;

		private GameObject _select;

		private uint _selectId;

		private GameObject _selectIdIcon;

		private List<GameObject> ir = new List<GameObject>();

		private Dictionary<uint, GameObject> _gos = new Dictionary<uint, GameObject>();

		private int minPrice;

		private BaseButton sell;

		private InputField input;

		private Transform help;

		private Transform help_par;

		private BaseButton btn_help;

		private Text reducePrice;

		private int needPriceTime = 1;

		private int num;

		private int maxNum;

		private int inputNum;

		private InputField inputf;

		private int selectMax;

		private int selectPrice;

		public static a3_auction_sell instans;

		private List<SXML> listxml = new List<SXML>();

		public a3_auction_sell(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			a3_auction_sell.instans = this;
			this.btn_help = new BaseButton(base.transform.FindChild("description"), 1, 1);
			this._content = base.transform.FindChild("cells/scroll/content");
			this._select = base.transform.FindChild("cells/scroll/bagselect").gameObject;
			this.input = base.transform.FindChild("price/1/InputField").GetComponent<InputField>();
			this.help = base.transform.FindChild("help/panel_help");
			this.help_par = base.transform.FindChild("help");
			this.reducePrice = base.transform.FindChild("price/reduce/Image/reducePrice").GetComponent<Text>();
			Transform[] componentsInChildren = this._content.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent == this._content;
				if (flag)
				{
					this.ir.Add(transform.gameObject);
				}
			}
			this.sell = new BaseButton(base.transform.FindChild("sell"), 1, 1);
			this.sell.onClick = delegate(GameObject g)
			{
				bool flag3 = this._selectId <= 0u || this._selectIdIcon == null;
				if (!flag3)
				{
					int num = int.Parse(this.input.text) * this.num;
					bool flag4 = this.input.text != null && this.input.text != "";
					if (flag4)
					{
						num = int.Parse(this.input.text) * this.num;
					}
					else
					{
						this.input.text = (this.minPrice / this.num).ToString();
					}
					int puttm = 12;
					bool isOn = base.transform.FindChild("time/1/tgroup/1").GetComponent<Toggle>().isOn;
					bool isOn2 = base.transform.FindChild("time/1/tgroup/2").GetComponent<Toggle>().isOn;
					bool flag5 = isOn;
					if (flag5)
					{
						puttm = 24;
					}
					else
					{
						bool flag6 = isOn2;
						if (flag6)
						{
							puttm = 48;
						}
					}
					num = int.Parse(this.input.text) * this.num;
					bool flag7 = num < this.minPrice * this.num;
					if (flag7)
					{
						flytxt.instance.fly("售价不合理，设置价格小于最低价格！", 0, default(Color), null);
					}
					else
					{
						BaseProxy<A3_AuctionProxy>.getInstance().SendPutOnMsg(this._selectId, (uint)puttm, (uint)num, (uint)this.num);
						this.reducePrice.text = "";
						this.inputf.text = 1.ToString();
						this.num = 1;
					}
				}
			};
			BaseButton arg_16D_0 = new BaseButton(base.transform.FindChild("diamond/add"), 1, 1);
			Action<GameObject> arg_16D_1;
			if ((arg_16D_1 = a3_auction_sell.<>c.<>9__23_1) == null)
			{
				arg_16D_1 = (a3_auction_sell.<>c.<>9__23_1 = new Action<GameObject>(a3_auction_sell.<>c.<>9.<init>b__23_1));
			}
			arg_16D_0.onClick = arg_16D_1;
			BaseButton arg_1A9_0 = new BaseButton(base.transform.FindChild("gold/add"), 1, 1);
			Action<GameObject> arg_1A9_1;
			if ((arg_1A9_1 = a3_auction_sell.<>c.<>9__23_2) == null)
			{
				arg_1A9_1 = (a3_auction_sell.<>c.<>9__23_2 = new Action<GameObject>(a3_auction_sell.<>c.<>9.<init>b__23_2));
			}
			arg_1A9_0.onClick = arg_1A9_1;
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
				bool flag3 = int.TryParse(this.inputf.text, out num);
				if (flag3)
				{
				}
				bool flag4 = num - 1 < 1;
				if (!flag4)
				{
					this.inputf.text = (num - 1).ToString();
					bool flag5 = int.TryParse(this.inputf.text, out this.num);
					if (flag5)
					{
					}
					base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (this.num * this.selectPrice).ToString();
				}
			};
			new BaseButton(base.getTransformByPath("buynum/bg/contain/btn_add"), 1, 1).onClick = delegate(GameObject go)
			{
				int num;
				bool flag3 = int.TryParse(this.inputf.text, out num);
				if (flag3)
				{
				}
				bool flag4 = num + 1 > this.selectMax;
				if (!flag4)
				{
					this.inputf.text = (num + 1).ToString();
					bool flag5 = int.TryParse(this.inputf.text, out this.num);
					if (flag5)
					{
					}
					base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (this.num * this.selectPrice).ToString();
				}
			};
			new BaseButton(base.getTransformByPath("buynum/bg/contain/min"), 1, 1).onClick = delegate(GameObject go)
			{
				this.inputf.text = 1.ToString();
				bool flag3 = int.TryParse(this.inputf.text, out this.num);
				if (flag3)
				{
				}
				base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (this.num * this.selectPrice).ToString();
			};
			new BaseButton(base.getTransformByPath("buynum/bg/contain/max"), 1, 1).onClick = delegate(GameObject go)
			{
				this.inputf.text = this.selectMax.ToString();
				bool flag3 = int.TryParse(this.inputf.text, out this.num);
				if (flag3)
				{
				}
				base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (this.num * this.selectPrice).ToString();
			};
			new BaseButton(base.transform.FindChild("buynum/btn"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("buynum").SetActive(false);
			};
			new BaseButton(base.main.etra.transform.FindChild("info/put"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag3 = ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId].confdata.equip_type > 0;
				if (flag3)
				{
					bool flag4 = this._selectId != 0u && ModelBase<a3_BagModel>.getInstance().isWorked(ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId]);
					if (flag4)
					{
						a3_BagItemData info = ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId];
						this.SetInfo(info);
					}
					else
					{
						flytxt.instance.fly("该物品不可交易！", 0, default(Color), null);
						base.main.etra.gameObject.SetActive(false);
					}
				}
			};
			new BaseButton(base.main.djtip.transform.FindChild("info/put"), 1, 1).onClick = delegate(GameObject go)
			{
				this.inputf.text = 1.ToString();
				this.listxml.Clear();
				this.listxml = XMLMgr.instance.GetSXMLList("item.item", "id==" + ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId].confdata.tpid);
				base.getTransformByPath("buynum/bg/contain/des_bg/Text").GetComponent<Text>().text = StringUtils.formatText(this.listxml[0].getString("desc"));
				base.getTransformByPath("buynum/bg/contain/name").GetComponent<Text>().text = this.listxml[0].getString("item_name");
				base.getTransformByPath("buynum/bg/contain/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + this.listxml[0].getString("icon_file"));
				base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId].auctiondata.cost / this.maxNum).ToString();
				this.selectPrice = ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId].auctiondata.cost / this.maxNum;
				this.selectMax = this.maxNum;
				base.getGameObjectByPath("buynum").SetActive(true);
				base.main.djtip.gameObject.SetActive(false);
			};
			this.input.onValueChanged.AddListener(delegate(string str)
			{
				bool flag3 = int.TryParse(str, out this.inputNum) && this.inputNum < this.minPrice;
				if (flag3)
				{
					str = this.minPrice.ToString();
				}
				bool flag4 = int.TryParse(str, out this.inputNum);
				if (flag4)
				{
					this.reducePrice.text = string.Concat((int)((float)(this.inputNum * this.num) * 0.2f));
				}
				base.transform.FindChild("price/3/Image/Text").GetComponent<Text>().text = (int.Parse(this.input.text) * this.num).ToString();
			});
			this.inputf = base.getTransformByPath("buynum/bg/contain/bug/InputField").GetComponent<InputField>();
			this.inputf.text = 1.ToString();
			bool flag2 = int.TryParse(this.inputf.text, out this.num);
			if (flag2)
			{
			}
			new BaseButton(base.getTransformByPath("buynum/bg/ok"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag3 = int.TryParse(this.inputf.text, out this.num);
				if (flag3)
				{
				}
				bool flag4 = ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId].confdata.equip_type < 1;
				if (flag4)
				{
					a3_BagItemData info = ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId];
					this.SetInfo(info);
				}
				else
				{
					bool flag5 = this._selectId != 0u && ModelBase<a3_BagModel>.getInstance().isWorked(ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId]);
					if (flag5)
					{
						a3_BagItemData info2 = ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId];
						this.SetInfo(info2);
					}
					else
					{
						flytxt.instance.fly("该物品不可交易！", 0, default(Color), null);
						base.main.etra.gameObject.SetActive(false);
					}
				}
				base.getGameObjectByPath("buynum").SetActive(false);
			};
			this.inputf.onValueChanged.AddListener(delegate(string ss)
			{
				bool flag3 = int.Parse(this.inputf.text) > this.selectMax;
				if (flag3)
				{
					this.inputf.text = this.selectMax.ToString();
					this.num = this.selectMax;
				}
				else
				{
					this.num = int.Parse(this.inputf.text);
				}
			});
			base.transform.FindChild("time/1/tgroup/0").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool check)
			{
				if (check)
				{
					this.needPriceTime = 1;
					bool flag3 = this._selectId > 0u;
					if (flag3)
					{
						a3_BagItemData needPrice = ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId];
						this.SetNeedPrice(needPrice);
					}
				}
			});
			base.transform.FindChild("time/1/tgroup/1").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool check)
			{
				if (check)
				{
					this.needPriceTime = 2;
					bool flag3 = this._selectId > 0u;
					if (flag3)
					{
						a3_BagItemData needPrice = ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId];
						this.SetNeedPrice(needPrice);
					}
				}
			});
			base.transform.FindChild("time/1/tgroup/2").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool check)
			{
				if (check)
				{
					this.needPriceTime = 3;
					bool flag3 = this._selectId > 0u;
					if (flag3)
					{
						a3_BagItemData needPrice = ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId];
						this.SetNeedPrice(needPrice);
					}
				}
			});
		}

		public override void onShowed()
		{
			this.needPriceTime = 1;
			base.main.etra.gameObject.SetActive(false);
			UIClient.instance.addEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			BaseProxy<A3_AuctionProxy>.getInstance().addEventListener(A3_AuctionProxy.EVENT_SELLSCUCCESS, new Action<GameEvent>(this.Event_OnSellDone));
			base.transform.FindChild("time/price/1/Image/Text").GetComponent<Text>().text = "0";
			this.BeginShow();
			this.refreshMoney();
			this.refreshGold();
		}

		private void justShowBuynum()
		{
			this.inputf.text = 1.ToString();
			this.listxml.Clear();
			this.listxml = XMLMgr.instance.GetSXMLList("item.item", "id==" + ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId].confdata.tpid);
			base.getTransformByPath("buynum/bg/contain/des_bg/Text").GetComponent<Text>().text = StringUtils.formatText(this.listxml[0].getString("desc"));
			base.getTransformByPath("buynum/bg/contain/name").GetComponent<Text>().text = this.listxml[0].getString("item_name");
			base.getTransformByPath("buynum/bg/contain/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + this.listxml[0].getString("icon_file"));
			base.getTransformByPath("buynum/bg/contain/paymoney/needMoney").GetComponent<Text>().text = (ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId].auctiondata.cost / this.maxNum).ToString();
			this.selectPrice = ModelBase<a3_BagModel>.getInstance().getItems(false)[this._selectId].auctiondata.cost / this.maxNum;
			this.selectMax = this.maxNum;
			base.getGameObjectByPath("buynum").SetActive(true);
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
		}

		public void refreshMoney()
		{
			Text component = base.transform.FindChild("gold/Text").GetComponent<Text>();
			component.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().money);
		}

		public void refreshGold()
		{
			Text component = base.transform.FindChild("diamond/Text").GetComponent<Text>();
			component.text = ModelBase<PlayerModel>.getInstance().gold.ToString();
		}

		public override void onClose()
		{
			base.main.etra.gameObject.SetActive(false);
			UIClient.instance.removeEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			BaseProxy<A3_AuctionProxy>.getInstance().removeEventListener(A3_AuctionProxy.EVENT_SELLSCUCCESS, new Action<GameEvent>(this.Event_OnSellDone));
			this.EndClear();
		}

		private void BeginShow()
		{
			this.input.text = "";
			this.reducePrice.text = "";
			this.sell.interactable = false;
			this.minPrice = 0;
			base.transform.FindChild("sellobj/name").GetComponent<Text>().text = "";
			base.transform.FindChild("sellobj/lv").GetComponent<Text>().text = "";
			base.transform.FindChild("price/2/Image/Text").GetComponent<Text>().text = "";
			this._selectId = 0u;
			this._select.SetActive(false);
			for (int i = 50; i < this.ir.Count; i++)
			{
				GameObject gameObject = this.ir[i].transform.FindChild("lock").gameObject;
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
			List<SXML> list = new List<SXML>();
			List<a3_BagItemData> list2 = new List<a3_BagItemData>();
			SXML sXML = XMLMgr.instance.GetSXML("item", "");
			list = sXML.GetNodeList("item", "");
			foreach (a3_BagItemData current in ModelBase<a3_BagModel>.getInstance().getItems(false).Values)
			{
				list2.Add(current);
			}
			List<a3_BagItemData> list3 = new List<a3_BagItemData>();
			for (int j = 0; j < list2.Count; j++)
			{
				for (int k = 0; k < list.Count; k++)
				{
					bool flag2 = (ulong)list2[j].tpid == (ulong)((long)list[k].getInt("id")) && (list[k].getInt("trade") > 0 || (list2[j].isEquip && ModelBase<a3_BagModel>.getInstance().isWorked(list2[j])));
					if (flag2)
					{
						list3.Add(list2[j]);
						break;
					}
				}
			}
			for (int l = 0; l < list3.Count; l++)
			{
				GameObject gameObject2 = this.SetIcon(list3[l]);
				bool flag3 = this.ir.Count >= l;
				if (flag3)
				{
					gameObject2.transform.SetParent(this.ir[l].transform);
				}
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localScale = Vector3.one;
				this._gos[list3[l].id] = gameObject2;
				List<a3_BagItemData> tp = new List<a3_BagItemData>();
				tp.Add(list3[l]);
				new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject g)
				{
					bool flag4 = tp[0].confdata.equip_type < 1;
					if (flag4)
					{
						this.ShowDJTip(tp[0]);
					}
					else
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add(tp[0]);
						arrayList.Add(equip_tip_type.tip_forAuc_put);
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
					}
				};
			}
		}

		private void EndClear()
		{
			bool flag = this._selectIdIcon != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this._selectIdIcon);
			}
			this._selectId = 0u;
			this._select.transform.SetParent(base.transform);
			List<GameObject> list = new List<GameObject>(this._gos.Values);
			for (int i = 0; i < list.Count; i++)
			{
				UnityEngine.Object.Destroy(list[i]);
			}
			this._gos.Clear();
		}

		private void ShowEPTip(a3_BagItemData data)
		{
			this.maxNum = data.num;
			this._selectId = data.id;
			base.main.etra.gameObject.SetActive(true);
			base.main.ShowEPTip(data, true, false);
		}

		private void ShowDJTip(a3_BagItemData data)
		{
			this.maxNum = data.num;
			this._selectId = data.id;
			this.justShowBuynum();
		}

		public void SetInfo(a3_BagItemData data)
		{
			SXML sXML = XMLMgr.instance.GetSXML("item", "");
			List<SXML> list = new List<SXML>();
			list = sXML.GetNodeList("item", "");
			base.main.etra.gameObject.SetActive(false);
			bool flag = this._selectIdIcon != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this._selectIdIcon);
			}
			bool flag2 = !this._gos.ContainsKey(data.id);
			if (!flag2)
			{
				GameObject gameObject = this._gos[data.id];
				this._select.transform.SetParent(gameObject.transform);
				this._select.transform.localPosition = Vector3.zero;
				this._select.SetActive(true);
				this._select.transform.SetSiblingIndex(1);
				this._selectId = data.id;
				this._selectIdIcon = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				this._selectIdIcon.transform.FindChild("bagselect").gameObject.SetActive(false);
				this._selectIdIcon.transform.SetParent(base.transform.FindChild("sellobj/Image"));
				this._selectIdIcon.transform.localPosition = Vector3.zero;
				this._selectIdIcon.transform.localScale = Vector3.one;
				bool flag3 = data.confdata.equip_type < 1;
				if (flag3)
				{
					this._selectIdIcon.transform.FindChild("num").gameObject.SetActive(true);
					this._selectIdIcon.transform.FindChild("num").GetComponent<Text>().text = this.num.ToString();
				}
				base.transform.FindChild("sellobj/name").GetComponent<Text>().text = data.confdata.item_name;
				base.transform.FindChild("sellobj/name").GetComponent<Text>().color = Globle.getColorByQuality(data.confdata.quality);
				bool flag4 = data.confdata.equip_type < 1;
				if (flag4)
				{
					base.transform.FindChild("sellobj/lv").GetComponent<Text>().text = "材料";
					for (int i = 0; i < list.Count; i++)
					{
						bool flag5 = (long)list[i].getInt("id") == (long)((ulong)data.tpid);
						if (flag5)
						{
							this.minPrice = list[i].getInt("trade");
							this.input.text = this.minPrice.ToString();
							break;
						}
					}
				}
				else
				{
					this.num = 1;
					base.transform.FindChild("sellobj/lv").GetComponent<Text>().text = string.Concat(new object[]
					{
						"+",
						data.equipdata.intensify_lv,
						"追",
						data.equipdata.add_level
					});
					this.minPrice = Mathf.Max(1, data.equipdata.combpt / 100) + data.equipdata.intensify_lv * data.equipdata.intensify_lv * 50 + data.equipdata.stage * data.equipdata.stage * 100;
					foreach (int current in data.equipdata.gem_att.Values)
					{
						this.minPrice += current;
					}
					this.input.text = this.minPrice.ToString();
					this.reducePrice.text = string.Concat((int)((float)this.minPrice * 0.2f));
					this.minPrice += data.equipdata.add_level * data.equipdata.add_level;
				}
				bool flag6 = data.confdata.equip_type < 1;
				if (flag6)
				{
					base.transform.FindChild("price/2/Image/Text").GetComponent<Text>().text = this.minPrice + "/个";
					this.reducePrice.text = string.Concat((int)((float)(this.num * this.minPrice) * 0.2f));
				}
				else
				{
					base.transform.FindChild("price/2/Image/Text").GetComponent<Text>().text = this.num * this.minPrice + "钻";
				}
				bool flag7 = data.confdata.equip_type > 0;
				if (flag7)
				{
					int num = (int)(data.tpid / 10u % 10u);
					num = (num + 1) * 10000;
					bool flag8 = data.confdata.equip_type == 8 || data.confdata.equip_type == 9 || data.confdata.equip_type == 10;
					if (flag8)
					{
						num = 100000;
					}
					num *= this.needPriceTime;
					base.transform.FindChild("time/price/1/Image/Text").GetComponent<Text>().text = num.ToString();
				}
				else
				{
					int num2 = 100;
					num2 = data.confdata.quality * num2 * this.num * this.needPriceTime;
					base.transform.FindChild("time/price/1/Image/Text").GetComponent<Text>().text = num2.ToString();
				}
				this.sell.interactable = true;
			}
		}

		private GameObject SetIcon(a3_BagItemData data)
		{
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data.confdata, true, -1, 1f, false, -1, 0, false, false, false, -1, false, false);
			bool flag = data.confdata.equip_type < 1;
			if (flag)
			{
				gameObject.transform.FindChild("num").gameObject.SetActive(true);
				gameObject.transform.FindChild("num").GetComponent<Text>().text = data.num.ToString();
			}
			gameObject.transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(false);
			gameObject.transform.FindChild("iconborder/equip_self").gameObject.SetActive(false);
			return gameObject;
		}

		private void SetNeedPrice(a3_BagItemData data)
		{
			bool flag = data.confdata.equip_type > 0;
			if (flag)
			{
				int num = (int)(data.tpid / 10u % 10u);
				num = (num + 1) * 10000;
				bool flag2 = data.confdata.equip_type == 8 || data.confdata.equip_type == 9 || data.confdata.equip_type == 10;
				if (flag2)
				{
					num = 100000;
				}
				num *= this.needPriceTime;
				base.transform.FindChild("time/price/1/Image/Text").GetComponent<Text>().text = num.ToString();
			}
			else
			{
				int num2 = 100;
				num2 = data.confdata.quality * num2 * this.num * this.needPriceTime;
				base.transform.FindChild("time/price/1/Image/Text").GetComponent<Text>().text = num2.ToString();
			}
		}

		private void Event_OnSellDone(GameEvent e)
		{
			this.EndClear();
			this.BeginShow();
			flytxt.instance.fly("成功出售了" + e.data["auc_data"]._arr[0]["name"] + "*" + e.data["auc_data"]._arr[0]["num"], 0, default(Color), null);
		}
	}
}
