using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_auction_get : a3BaseAuction
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_auction_get.<>c <>9 = new a3_auction_get.<>c();

			public static Action<GameObject> <>9__12_1;

			public static Action<GameObject> <>9__12_4;

			internal void <init>b__12_1(GameObject g)
			{
				BaseProxy<A3_AuctionProxy>.getInstance().SendGetAllMsg();
			}

			internal void <init>b__12_4(GameObject go)
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

		private BaseButton btn_get;

		private BaseButton btn_getall;

		private BaseButton btn_help;

		private Transform help;

		private Transform help_par;

		public a3_auction_get(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			this._prefab = base.transform.FindChild("cells/scroll/0").gameObject;
			this._content = base.transform.FindChild("cells/scroll/content");
			this.help = base.transform.FindChild("help/panel_help");
			this.help_par = base.transform.FindChild("help");
			this._glg = this._content.GetComponent<GridLayoutGroup>();
			this._select = base.transform.FindChild("cells/scroll/select").gameObject;
			this.btn_getall = new BaseButton(base.transform.FindChild("getall"), 1, 1);
			this.btn_get = new BaseButton(base.transform.FindChild("get"), 1, 1);
			this.btn_help = new BaseButton(base.transform.FindChild("description"), 1, 1);
			this.btn_get.onClick = delegate(GameObject g)
			{
				bool flag = this._selectId > 0u;
				if (flag)
				{
					BaseProxy<A3_AuctionProxy>.getInstance().SendGetMsg(this._selectId);
				}
			};
			BaseButton arg_11E_0 = this.btn_getall;
			Action<GameObject> arg_11E_1;
			if ((arg_11E_1 = a3_auction_get.<>c.<>9__12_1) == null)
			{
				arg_11E_1 = (a3_auction_get.<>c.<>9__12_1 = new Action<GameObject>(a3_auction_get.<>c.<>9.<init>b__12_1));
			}
			arg_11E_0.onClick = arg_11E_1;
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
			BaseButton arg_1AC_0 = new BaseButton(base.transform.FindChild("fg/gold/add"), 1, 1);
			Action<GameObject> arg_1AC_1;
			if ((arg_1AC_1 = a3_auction_get.<>c.<>9__12_4) == null)
			{
				arg_1AC_1 = (a3_auction_get.<>c.<>9__12_4 = new Action<GameObject>(a3_auction_get.<>c.<>9.<init>b__12_4));
			}
			arg_1AC_0.onClick = arg_1AC_1;
			this.btn_help.onClick = delegate(GameObject go)
			{
				this.help.gameObject.SetActive(true);
				this.help.SetParent(base.main.transform);
			};
			BaseProxy<A3_AuctionProxy>.getInstance().addEventListener(A3_AuctionProxy.EVENT_NEWGET, delegate(GameEvent e)
			{
				Variant data = e.data;
				bool flag = data["new"];
				bool flag2 = flag;
				if (flag2)
				{
					base.main.transform.FindChild("tabs/get/notice").gameObject.SetActive(true);
				}
				else
				{
					base.main.transform.FindChild("tabs/get/notice").gameObject.SetActive(false);
				}
			});
		}

		public override void onShowed()
		{
			this.btn_get.interactable = false;
			this._selectId = 0u;
			this._select.SetActive(false);
			base.transform.FindChild("fg/gold/Text").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().gold.ToString();
			base.transform.FindChild("count/count").GetComponent<Text>().text = "(" + 0 + ")";
			this._content.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
			BaseProxy<A3_AuctionProxy>.getInstance().addEventListener(A3_AuctionProxy.EVENT_MYGET, new Action<GameEvent>(this.Event_OnMyGet));
			BaseProxy<A3_AuctionProxy>.getInstance().addEventListener(A3_AuctionProxy.EVENT_GETMYGET, new Action<GameEvent>(this.Event_GetOneSuccess));
			BaseProxy<A3_AuctionProxy>.getInstance().SendMyRackMsg();
		}

		public void SetUIData(uint id, a3_BagItemData data)
		{
			bool flag = !this.gos.ContainsKey(id);
			if (!flag)
			{
				bool flag2 = data.auctiondata.get_type != 3;
				if (flag2)
				{
					Image component = this.gos[id].transform.FindChild("icon").GetComponent<Image>();
					component.sprite = (Resources.Load(data.confdata.file, typeof(Sprite)) as Sprite);
					this.gos[id].transform.FindChild("name").GetComponent<Text>().text = data.confdata.item_name;
					this.gos[id].transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(data.confdata.quality);
					this.gos[id].transform.FindChild("sell").GetComponent<Text>().text = data.auctiondata.cost.ToString();
				}
				else
				{
					this.gos[id].transform.FindChild("sell").GetComponent<Text>().text = string.Concat((int)((float)data.auctiondata.cost * 0.8f));
				}
				bool flag3 = data.confdata.equip_type < 1;
				if (flag3)
				{
					this.gos[id].transform.FindChild("num").gameObject.SetActive(true);
					this.gos[id].transform.FindChild("num").GetComponent<Text>().text = data.num.ToString();
				}
				TimeSpan timeSpan = new TimeSpan(720, 0, data.auctiondata.get_tm);
				bool flag4 = timeSpan.TotalSeconds < (double)muNetCleint.instance.CurServerTimeStamp;
				if (flag4)
				{
					int num = data.equipdata.combpt / 100 + data.equipdata.stage * 10;
					base.transform.FindChild("price/2/Image/Text").GetComponent<Text>().text = num + "钻/个";
					int num2 = (int)(data.tpid / 10u % 10u);
					num2 = (num2 + 1) * 10000;
					bool flag5 = data.confdata.equip_type == 8 || data.confdata.equip_type == 9 || data.confdata.equip_type == 10;
					if (flag5)
					{
						num2 = 100000;
					}
					num2 *= data.auctiondata.pro_tm / 12;
					num2 *= 10;
					this.gos[id].transform.FindChild("time").GetComponent<Text>().text = "已经过期\n保存费：" + num2;
				}
				else
				{
					timeSpan = new TimeSpan(0, 0, (int)(timeSpan.TotalSeconds - (double)muNetCleint.instance.CurServerTimeStamp));
					bool flag6 = timeSpan.TotalSeconds >= 86400.0;
					if (flag6)
					{
						this.gos[id].transform.FindChild("time").GetComponent<Text>().text = "剩余" + (int)timeSpan.TotalDays + "天";
					}
					else
					{
						this.gos[id].transform.FindChild("time").GetComponent<Text>().text = string.Concat(new object[]
						{
							"剩余",
							(int)timeSpan.TotalHours % 24,
							"时",
							timeSpan.Minutes,
							"分"
						});
					}
				}
				this.gos[id].transform.FindChild("tip").GetComponent<Text>().text = ModelBase<A3_AuctionModel>.getInstance().FromGetTypeToString(data.auctiondata.get_type);
			}
		}

		public override void onClose()
		{
			BaseProxy<A3_AuctionProxy>.getInstance().removeEventListener(A3_AuctionProxy.EVENT_MYGET, new Action<GameEvent>(this.Event_OnMyGet));
			BaseProxy<A3_AuctionProxy>.getInstance().removeEventListener(A3_AuctionProxy.EVENT_GETMYGET, new Action<GameEvent>(this.Event_GetOneSuccess));
			this._selectId = 0u;
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

		private void Event_OnMyGet(GameEvent e)
		{
			base.transform.FindChild("fg/gold/Text").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().gold.ToString();
			List<a3_BagItemData> list = new List<a3_BagItemData>(ModelBase<A3_AuctionModel>.getInstance().GetMyItems_down().Values);
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
					this.btn_get.interactable = true;
				};
				this.gos[current.id] = gameObject;
				this.SetUIData(current.id, current);
			}
			this._content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, this._glg.cellSize.y * (float)list.Count);
			base.transform.FindChild("count/count").GetComponent<Text>().text = "(" + list.Count + ")";
		}

		private void Event_GetOneSuccess(GameEvent e)
		{
			this._selectId = 0u;
			this._select.transform.SetParent(base.transform);
			this._select.SetActive(false);
			this.btn_get.interactable = false;
			base.transform.FindChild("fg/gold/Text").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().gold.ToString();
			Variant data = e.data;
			uint key = data["auc_id"];
			bool flag = this.gos.ContainsKey(key);
			if (flag)
			{
				UnityEngine.Object.Destroy(this.gos[key]);
			}
			this.gos.Remove(key);
			base.transform.FindChild("count/count").GetComponent<Text>().text = "(" + this.gos.Count + ")";
			this._content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (this._glg.cellSize.y + 0.1f) * (float)this.gos.Count);
		}
	}
}
