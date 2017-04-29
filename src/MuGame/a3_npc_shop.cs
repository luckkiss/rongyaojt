using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_npc_shop : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_npc_shop.<>c <>9 = new a3_npc_shop.<>c();

			public static Action<GameObject> <>9__17_0;

			internal void <init>b__17_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_NPC_SHOP);
			}
		}

		public static a3_npc_shop instance;

		private List<SXML> listNormal = new List<SXML>();

		private List<SXML> listChange = new List<SXML>();

		private List<SXML> listItem = new List<SXML>();

		private Transform contents;

		private int sizey;

		private float cellsizey;

		private int times;

		private int npc_shopid;

		private int goodsLength;

		private List<int> goodsID = new List<int>();

		private GameObject item;

		private GameObject itemclone;

		public int selectItemID;

		public int itemType;

		private GameObject selectItem;

		private GameObject selectIcon;

		private int haveTimes;

		private int min;

		private int sec;

		public override void init()
		{
			a3_npc_shop.instance = this;
			BaseButton arg_38_0 = new BaseButton(base.getTransformByPath("close"), 1, 1);
			Action<GameObject> arg_38_1;
			if ((arg_38_1 = a3_npc_shop.<>c.<>9__17_0) == null)
			{
				arg_38_1 = (a3_npc_shop.<>c.<>9__17_0 = new Action<GameObject>(a3_npc_shop.<>c.<>9.<init>b__17_0));
			}
			arg_38_0.onClick = arg_38_1;
			new BaseButton(base.getTransformByPath("buy"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = this.selectItemID == 0;
				if (flag)
				{
					flytxt.instance.fly("请先选择要购买的道具", 0, default(Color), null);
				}
				else
				{
					BaseProxy<A3_NPCShopProxy>.getInstance().sendBuy((uint)ModelBase<A3_NPCShopModel>.getInstance().listNPCShop[0].getInt("shop_id"), (uint)this.selectItemID, (uint)this.itemType, 1u);
				}
			};
			BaseProxy<A3_NPCShopProxy>.getInstance().addEventListener(A3_NPCShopProxy.EVENT_NPCSHOP_REFRESH, new Action<GameEvent>(this.onRefresh));
			BaseProxy<A3_NPCShopProxy>.getInstance().addEventListener(A3_NPCShopProxy.EVENT_NPCSHOP_BUY, new Action<GameEvent>(this.onBuy));
			BaseProxy<A3_NPCShopProxy>.getInstance().addEventListener(A3_NPCShopProxy.EVENT_NPCSHOP_TIME, new Action<GameEvent>(this.onShowFloat));
			this.item = base.getGameObjectByPath("panel_right/scroll_rect/changeItem");
			this.contents = base.getTransformByPath("panel_right/scroll_rect/contains");
			this.haveTimes = ModelBase<A3_NPCShopModel>.getInstance().alltimes - NetClient.instance.CurServerTimeStamp - 1;
			base.InvokeRepeating("time", 0f, 1f);
			this.change();
			this.cloneItem(this.goodsLength);
		}

		private void change()
		{
			base.getComponentByPath<Text>("title").text = ModelBase<A3_NPCShopModel>.getInstance().listNPCShop[0].getString("shop_name");
			string @string = ModelBase<A3_NPCShopModel>.getInstance().listNPCShop[0].getString("goods_list");
			string[] array = @string.Split(new char[]
			{
				','
			});
			this.goodsLength = array.Length;
			for (int i = 0; i < this.goodsLength; i++)
			{
				int num;
				bool flag = int.TryParse(array[i], out num);
				if (flag)
				{
					this.goodsID.Add(num);
				}
			}
			foreach (KeyValuePair<uint, uint> current in ModelBase<A3_NPCShopModel>.getInstance().float_list)
			{
				List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("npc_shop.float_list", "item_id==" + current.Key);
				this.goodsID.Add(sXMLList[0].getInt("id"));
			}
			this.goodsLength = this.goodsID.Count;
			this.cellsizey = this.contents.GetComponent<GridLayoutGroup>().cellSize.y;
			float y = this.contents.GetComponent<GridLayoutGroup>().spacing.y;
			this.sizey = this.goodsLength / 2 + this.goodsLength % 2;
			this.contents.GetComponent<RectTransform>().sizeDelta = new Vector2(this.contents.GetComponent<RectTransform>().sizeDelta.x, (float)this.sizey * (this.cellsizey + y));
		}

		public override void onShowed()
		{
			this.selectItemID = 0;
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			this.change();
			base.Invoke("ShowFirstItem", 0.2f);
		}

		private void ShowFirstItem()
		{
			bool flag = this.contents.childCount > 0;
			if (flag)
			{
				this.contents.GetChild(0).FindChild("bg/bg1/select").gameObject.SetActive(true);
				this.selectItemID = this.goodsID[0];
				bool flag2 = this.selectItemID < 5001;
				if (flag2)
				{
					this.itemType = 0;
				}
				else
				{
					this.itemType = 1;
				}
			}
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
		}

		private void time()
		{
			this.min = this.haveTimes / 60;
			this.sec = this.haveTimes % 60;
			bool flag = this.sec < 0 && this.min > 0;
			if (flag)
			{
				this.min--;
				this.sec = 59;
			}
			else
			{
				bool flag2 = this.min <= 0 && this.sec < 0;
				if (flag2)
				{
					this.min = 0;
					this.sec = 0;
				}
			}
			bool flag3 = this.min <= 0;
			if (flag3)
			{
				this.min = 0;
			}
			bool flag4 = this.haveTimes > 0;
			if (flag4)
			{
				bool flag5 = this.sec < 10 && this.min > 9;
				if (flag5)
				{
					base.getComponentByPath<Text>("timego").text = string.Concat(new object[]
					{
						ContMgr.getCont("npc_shop_1", null),
						this.min,
						":0",
						this.sec
					});
				}
				else
				{
					bool flag6 = this.sec > 9 && this.min > 9;
					if (flag6)
					{
						base.getComponentByPath<Text>("timego").text = string.Concat(new object[]
						{
							ContMgr.getCont("npc_shop_1", null),
							this.min,
							":",
							this.sec
						});
					}
					else
					{
						bool flag7 = this.sec > 9 && this.min < 10;
						if (flag7)
						{
							base.getComponentByPath<Text>("timego").text = string.Concat(new object[]
							{
								ContMgr.getCont("npc_shop_1", null),
								"0",
								this.min,
								":",
								this.sec
							});
						}
						else
						{
							base.getComponentByPath<Text>("timego").text = string.Concat(new object[]
							{
								ContMgr.getCont("npc_shop_1", null),
								"0",
								this.min,
								":0",
								this.sec
							});
						}
					}
				}
				this.haveTimes--;
			}
			else
			{
				this.haveTimes = 0;
				base.getComponentByPath<Text>("timego").text = ContMgr.getCont("npc_shop_2", null);
			}
		}

		private void cloneItem(int lenth)
		{
			for (int i = 0; i < lenth; i++)
			{
				this.itemclone = UnityEngine.Object.Instantiate<GameObject>(this.item);
				this.itemclone.transform.SetParent(this.contents);
				this.itemclone.transform.localScale = Vector3.one;
				this.itemclone.SetActive(true);
				this.itemclone.name = this.goodsID[i].ToString();
				Image component = this.itemclone.transform.FindChild("bg/bg1/icon").GetComponent<Image>();
				Text component2 = this.itemclone.transform.FindChild("bg/bg1/name").GetComponent<Text>();
				bool flag = this.goodsID[i] == this.selectItemID;
				if (flag)
				{
					this.itemclone.transform.FindChild("bg/bg1/select").gameObject.SetActive(true);
				}
				bool flag2 = this.goodsID[i] < 5001;
				int @int;
				int name;
				int shop_type;
				if (flag2)
				{
					this.itemclone.transform.FindChild("bg/bg1/four_icon/up").gameObject.SetActive(false);
					this.itemclone.transform.FindChild("bg/bg1/four_icon/down").gameObject.SetActive(false);
					this.itemclone.transform.FindChild("bg/bg1/four_icon/changeless").gameObject.SetActive(false);
					this.itemclone.transform.FindChild("bg/bg1/need").gameObject.SetActive(false);
					this.listNormal = XMLMgr.instance.GetSXMLList("npc_shop.goods_list", "id==" + this.goodsID[i]);
					this.itemclone.transform.FindChild("bg/bg1/limittext/limitnum").GetComponent<Text>().text = "无限量";
					this.itemclone.transform.FindChild("bg/bg1/price_now").GetComponent<Text>().text = this.listNormal[0].getInt("value").ToString();
					bool flag3 = this.listNormal[0].getInt("money_type") == 3;
					if (flag3)
					{
						this.itemclone.transform.FindChild("bg/bg1/four_icon/gold").gameObject.SetActive(false);
						this.itemclone.transform.FindChild("bg/bg1/four_icon/diamond").gameObject.SetActive(true);
					}
					else
					{
						this.itemclone.transform.FindChild("bg/bg1/four_icon/gold").gameObject.SetActive(true);
						this.itemclone.transform.FindChild("bg/bg1/four_icon/diamond").gameObject.SetActive(false);
					}
					component.sprite = Resources.Load<Sprite>("icon/item/" + this.listNormal[0].getInt("item_id"));
					@int = this.listNormal[0].getInt("item_id");
					name = this.goodsID[i];
					int int2 = this.listNormal[0].getInt("money_type");
					shop_type = 0;
				}
				else
				{
					this.listChange = XMLMgr.instance.GetSXMLList("npc_shop.float_list", "id==" + this.goodsID[i]);
					this.itemclone.transform.FindChild("bg/bg1/limittext/limitnum").GetComponent<Text>().text = ModelBase<A3_NPCShopModel>.getInstance().float_list_num[(uint)this.listChange[0].getInt("item_id")].ToString();
					this.itemclone.transform.FindChild("bg/bg1/price_now").GetComponent<Text>().text = ModelBase<A3_NPCShopModel>.getInstance().float_list[(uint)this.listChange[0].getInt("item_id")].ToString();
					this.itemclone.transform.FindChild("bg/bg1/need").GetComponent<Text>().text = ModelBase<A3_NPCShopModel>.getInstance().limit_num[(uint)this.listChange[0].getInt("item_id")].ToString();
					bool flag4 = this.listChange[0].getInt("money_type") == 3;
					if (flag4)
					{
						this.itemclone.transform.FindChild("bg/bg1/four_icon/gold").gameObject.SetActive(false);
						this.itemclone.transform.FindChild("bg/bg1/four_icon/diamond").gameObject.SetActive(true);
					}
					else
					{
						this.itemclone.transform.FindChild("bg/bg1/four_icon/gold").gameObject.SetActive(true);
						this.itemclone.transform.FindChild("bg/bg1/four_icon/diamond").gameObject.SetActive(false);
					}
					component.sprite = Resources.Load<Sprite>("icon/item/" + this.listChange[0].getInt("item_id"));
					@int = this.listChange[0].getInt("item_id");
					name = this.goodsID[i];
					int int2 = this.listChange[0].getInt("money_type");
					shop_type = 1;
					for (int j = 0; j < ModelBase<A3_NPCShopModel>.getInstance().price.Count; j++)
					{
						for (int k = 0; k < this.contents.childCount; k++)
						{
							bool flag5 = ModelBase<A3_NPCShopModel>.getInstance().price.ContainsKey(int.Parse(this.contents.GetChild(k).name));
							if (flag5)
							{
								int lastprice = ModelBase<A3_NPCShopModel>.getInstance().price[int.Parse(this.contents.GetChild(k).name)].lastprice;
								int nowprice = ModelBase<A3_NPCShopModel>.getInstance().price[int.Parse(this.contents.GetChild(k).name)].nowprice;
								bool flag6 = lastprice < nowprice;
								if (flag6)
								{
									this.contents.GetChild(k).FindChild("bg/bg1/four_icon/up").gameObject.SetActive(true);
									this.contents.GetChild(k).FindChild("bg/bg1/four_icon/down").gameObject.SetActive(false);
									this.contents.GetChild(k).FindChild("bg/bg1/four_icon/changeless").gameObject.SetActive(false);
								}
								else
								{
									bool flag7 = lastprice > nowprice;
									if (flag7)
									{
										this.contents.GetChild(k).FindChild("bg/bg1/four_icon/up").gameObject.SetActive(false);
										this.contents.GetChild(k).FindChild("bg/bg1/four_icon/changeless").gameObject.SetActive(false);
										this.contents.GetChild(k).FindChild("bg/bg1/four_icon/down").gameObject.SetActive(true);
									}
									else
									{
										this.contents.GetChild(k).FindChild("bg/bg1/four_icon/up").gameObject.SetActive(false);
										this.contents.GetChild(k).FindChild("bg/bg1/four_icon/down").gameObject.SetActive(false);
										this.contents.GetChild(k).FindChild("bg/bg1/four_icon/changeless").gameObject.SetActive(true);
									}
								}
							}
						}
					}
				}
				this.listItem = XMLMgr.instance.GetSXMLList("item.item", "id==" + @int);
				component2.text = this.listItem[0].getString("item_name");
				new BaseButton(this.itemclone.transform, 1, 1).onClick = delegate(GameObject go)
				{
					for (int l = 0; l < this.contents.childCount; l++)
					{
						this.selectItem = this.getGameObjectByPath("panel_right/scroll_rect/contains/" + this.contents.GetChild(l).name + "/bg/bg1/select");
						bool flag8 = int.Parse(this.contents.GetChild(l).name) == name;
						if (flag8)
						{
							this.selectItem.SetActive(true);
						}
						else
						{
							this.selectItem.SetActive(false);
						}
					}
					this.selectItemID = name;
					this.itemType = shop_type;
				};
				new BaseButton(this.itemclone.transform.FindChild("bg/bg1/icon"), 1, 1).onClick = delegate(GameObject go)
				{
					for (int l = 0; l < this.contents.childCount; l++)
					{
						this.selectItem = this.getGameObjectByPath("panel_right/scroll_rect/contains/" + this.contents.GetChild(l).name + "/bg/bg1/select");
						this.selectIcon = this.getGameObjectByPath("panel_right/scroll_rect/contains/" + this.contents.GetChild(l).name + "/bg/bg1/seicon");
						bool flag8 = int.Parse(this.contents.GetChild(l).name) == name;
						if (flag8)
						{
							this.selectItem.SetActive(true);
						}
						else
						{
							this.selectItem.SetActive(false);
						}
					}
					this.selectItemID = name;
				};
			}
		}

		private void onRefresh(GameEvent e)
		{
			BaseProxy<A3_NPCShopProxy>.getInstance().sendShowFloat((uint)ModelBase<A3_NPCShopModel>.getInstance().listNPCShop[0].getInt("shop_id"));
		}

		private void onBuy(GameEvent e)
		{
			int num = e.data["shop_idx"];
			int num2 = e.data["shop_num"];
			bool flag = e.data["limit_num"] != null;
			string @string;
			if (flag)
			{
				int num3 = e.data["limit_num"];
				int @int = this.listChange[0].getInt("item_id");
				List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + @int);
				@string = sXMLList[0].getString("item_name");
				flytxt.instance.fly(string.Concat(new object[]
				{
					"您成功购买了",
					@string,
					"*",
					num2
				}), 0, default(Color), null);
				BaseProxy<A3_NPCShopProxy>.getInstance().sendShowAll();
				for (int i = 0; i < this.contents.childCount; i++)
				{
					bool flag2 = num == int.Parse(this.contents.GetChild(i).name);
					if (flag2)
					{
						this.contents.GetChild(i).FindChild("bg/bg1/limittext/limitnum").GetComponent<Text>().text = num3.ToString();
						return;
					}
				}
			}
			else
			{
				int int2 = this.listNormal[0].getInt("item_id");
				List<SXML> sXMLList2 = XMLMgr.instance.GetSXMLList("item.item", "id==" + int2);
				@string = sXMLList2[0].getString("item_name");
			}
			flytxt.instance.fly(string.Concat(new object[]
			{
				"您成功购买了",
				@string,
				"*",
				num2
			}), 0, default(Color), null);
		}

		private void onShowFloat(GameEvent e)
		{
			this.haveTimes = ModelBase<A3_NPCShopModel>.getInstance().alltimes - NetClient.instance.CurServerTimeStamp - 1;
			string @string = ModelBase<A3_NPCShopModel>.getInstance().listNPCShop[0].getString("goods_list");
			string[] array = @string.Split(new char[]
			{
				','
			});
			int num = array.Length;
			this.goodsID.Clear();
			for (int i = this.contents.childCount; i > 0; i--)
			{
				UnityEngine.Object.DestroyImmediate(this.contents.GetChild(i - 1).gameObject);
			}
			for (int j = 0; j < num; j++)
			{
				int num2;
				bool flag = int.TryParse(array[j], out num2);
				if (flag)
				{
					this.goodsID.Add(num2);
				}
			}
			foreach (KeyValuePair<uint, uint> current in ModelBase<A3_NPCShopModel>.getInstance().float_list)
			{
				List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("npc_shop.float_list", "item_id==" + current.Key);
				this.goodsID.Add(sXMLList[0].getInt("id"));
			}
			num = this.goodsID.Count;
			this.cloneItem(num);
			for (int k = 0; k < ModelBase<A3_NPCShopModel>.getInstance().price.Count; k++)
			{
				for (int l = 0; l < this.contents.childCount; l++)
				{
					bool flag2 = ModelBase<A3_NPCShopModel>.getInstance().price.ContainsKey(int.Parse(this.contents.GetChild(l).name));
					if (flag2)
					{
						int lastprice = ModelBase<A3_NPCShopModel>.getInstance().price[int.Parse(this.contents.GetChild(l).name)].lastprice;
						int nowprice = ModelBase<A3_NPCShopModel>.getInstance().price[int.Parse(this.contents.GetChild(l).name)].nowprice;
						bool flag3 = lastprice < nowprice;
						if (flag3)
						{
							this.contents.GetChild(l).FindChild("bg/bg1/four_icon/up").gameObject.SetActive(true);
							this.contents.GetChild(l).FindChild("bg/bg1/four_icon/down").gameObject.SetActive(false);
							this.contents.GetChild(l).FindChild("bg/bg1/four_icon/changeless").gameObject.SetActive(false);
						}
						else
						{
							bool flag4 = lastprice > nowprice;
							if (flag4)
							{
								this.contents.GetChild(l).FindChild("bg/bg1/four_icon/up").gameObject.SetActive(false);
								this.contents.GetChild(l).FindChild("bg/bg1/four_icon/changeless").gameObject.SetActive(false);
								this.contents.GetChild(l).FindChild("bg/bg1/four_icon/down").gameObject.SetActive(true);
							}
							else
							{
								this.contents.GetChild(l).FindChild("bg/bg1/four_icon/up").gameObject.SetActive(false);
								this.contents.GetChild(l).FindChild("bg/bg1/four_icon/down").gameObject.SetActive(false);
								this.contents.GetChild(l).FindChild("bg/bg1/four_icon/changeless").gameObject.SetActive(true);
							}
						}
					}
				}
			}
		}
	}
}
