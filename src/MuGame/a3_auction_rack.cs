using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_auction_rack : a3BaseAuction
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_auction_rack.<>c <>9 = new a3_auction_rack.<>c();

			public static Action<GameObject> <>9__12_1;

			internal void <init>b__12_1(GameObject go)
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

		private BaseButton btn_refresh;

		private BaseButton btn_putoff;

		private BaseButton btn_help;

		private Transform help;

		private Transform help_par;

		public a3_auction_rack(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			this._prefab = base.transform.FindChild("cells/scroll/0").gameObject;
			this._content = base.transform.FindChild("cells/scroll/content");
			this._glg = this._content.GetComponent<GridLayoutGroup>();
			this._select = base.transform.FindChild("cells/scroll/select").gameObject;
			this.help = base.transform.FindChild("help/panel_help");
			this.help_par = base.transform.FindChild("help");
			this.btn_refresh = new BaseButton(base.transform.FindChild("refresh"), 1, 1);
			this.btn_putoff = new BaseButton(base.transform.FindChild("putoff"), 1, 1);
			this.btn_help = new BaseButton(base.transform.FindChild("description"), 1, 1);
			this.btn_putoff.onClick = delegate(GameObject g)
			{
				BaseProxy<A3_AuctionProxy>.getInstance().SendPutOffMsg(this._selectId);
			};
			BaseButton arg_12F_0 = new BaseButton(base.transform.FindChild("fg/gold/add"), 1, 1);
			Action<GameObject> arg_12F_1;
			if ((arg_12F_1 = a3_auction_rack.<>c.<>9__12_1) == null)
			{
				arg_12F_1 = (a3_auction_rack.<>c.<>9__12_1 = new Action<GameObject>(a3_auction_rack.<>c.<>9.<init>b__12_1));
			}
			arg_12F_0.onClick = arg_12F_1;
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
		}

		public override void onShowed()
		{
			this.btn_putoff.interactable = false;
			this._selectId = 0u;
			this._select.SetActive(false);
			base.transform.FindChild("fg/gold/Text").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().gold.ToString();
			base.transform.FindChild("count/count").GetComponent<Text>().text = "(" + 0 + ")";
			this._content.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
			BaseProxy<A3_AuctionProxy>.getInstance().addEventListener(A3_AuctionProxy.EVENT_LOADMYSHELF, new Action<GameEvent>(this.Event_LoadMyRack));
			BaseProxy<A3_AuctionProxy>.getInstance().addEventListener(A3_AuctionProxy.EVENT_PUTOFFSUCCESS, new Action<GameEvent>(this.Event_PutOffSuccess));
			BaseProxy<A3_AuctionProxy>.getInstance().SendMyRackMsg();
		}

		public void SetUIData(uint id, a3_BagItemData data)
		{
			bool flag = !this.gos.ContainsKey(id);
			if (!flag)
			{
				Image component = this.gos[id].transform.FindChild("icon").GetComponent<Image>();
				component.sprite = (Resources.Load(data.confdata.file, typeof(Sprite)) as Sprite);
				this.gos[id].transform.FindChild("name").GetComponent<Text>().text = data.confdata.item_name;
				this.gos[id].transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(data.confdata.quality);
				TimeSpan timeSpan = new TimeSpan(data.auctiondata.pro_tm, 0, 0);
				int b = (int)(timeSpan.TotalSeconds + (double)data.auctiondata.tm - (double)muNetCleint.instance.CurServerTimeStamp);
				timeSpan = new TimeSpan(0, 0, Mathf.Max(0, b));
				this.gos[id].transform.FindChild("time").GetComponent<Text>().text = string.Concat(new object[]
				{
					(int)timeSpan.TotalHours,
					"时",
					timeSpan.Minutes,
					"分",
					timeSpan.Seconds,
					"秒"
				});
				bool flag2 = data.confdata.equip_type < 1;
				if (flag2)
				{
					this.gos[id].transform.FindChild("num").gameObject.SetActive(true);
					this.gos[id].transform.FindChild("num").GetComponent<Text>().text = data.num.ToString();
				}
				bool flag3 = data.confdata.equip_type > 0;
				if (flag3)
				{
					this.gos[id].transform.FindChild("sell").GetComponent<Text>().text = data.auctiondata.cost + "钻";
				}
				else
				{
					this.gos[id].transform.FindChild("sell").GetComponent<Text>().text = data.auctiondata.cost / data.num + "钻/个";
				}
			}
		}

		public override void onClose()
		{
			BaseProxy<A3_AuctionProxy>.getInstance().removeEventListener(A3_AuctionProxy.EVENT_LOADMYSHELF, new Action<GameEvent>(this.Event_LoadMyRack));
			BaseProxy<A3_AuctionProxy>.getInstance().removeEventListener(A3_AuctionProxy.EVENT_PUTOFFSUCCESS, new Action<GameEvent>(this.Event_PutOffSuccess));
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

		private void Event_LoadMyRack(GameEvent e)
		{
			List<a3_BagItemData> list = new List<a3_BagItemData>(ModelBase<A3_AuctionModel>.getInstance().GetMyItems_up().Values);
			Transform transformByPath = base.getTransformByPath("cells/scroll/content");
			int childCount = transformByPath.childCount;
			for (int i = childCount; i > 0; i--)
			{
				UnityEngine.Object.DestroyImmediate(transformByPath.GetChild(i - 1).gameObject);
			}
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
					this.btn_putoff.interactable = true;
				};
				this.gos[current.id] = gameObject;
				this.SetUIData(current.id, current);
			}
			this._content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (this._glg.cellSize.y + 0.1f) * (float)list.Count);
			base.transform.FindChild("count/count").GetComponent<Text>().text = "(" + list.Count + ")";
		}

		private void Event_PutOffSuccess(GameEvent e)
		{
			this._selectId = 0u;
			this._select.transform.SetParent(base.transform);
			this._select.SetActive(false);
			base.transform.FindChild("fg/gold/Text").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().gold.ToString();
			this.btn_putoff.interactable = false;
			Variant data = e.data;
			bool flag = data.ContainsKey("auc_data");
			if (flag)
			{
				Variant variant = data["auc_data"];
				foreach (Variant current in variant._arr)
				{
					uint num = current["id"];
					bool flag2 = current["equip_type"] > 0;
					if (flag2)
					{
						flytxt.instance.fly("成功下架了一件" + current["name"], 0, default(Color), null);
					}
					else
					{
						flytxt.instance.fly("成功下架了" + current["num"] + "个" + current["name"], 0, default(Color), null);
					}
					bool flag3 = this.gos.ContainsKey(num);
					if (flag3)
					{
						foreach (KeyValuePair<uint, GameObject> current2 in this.gos)
						{
							bool flag4 = current2.Key != num;
							if (flag4)
							{
								UnityEngine.Object.Destroy(current2.Value);
							}
						}
					}
					this.gos.Remove(num);
				}
				bool flag5 = variant.Count < 1;
				if (flag5)
				{
					foreach (KeyValuePair<uint, GameObject> current3 in this.gos)
					{
						UnityEngine.Object.Destroy(current3.Value);
					}
					this.gos.Clear();
				}
			}
			bool flag6 = data.ContainsKey("get_list");
			if (flag6)
			{
				Variant variant2 = data["get_list"];
				foreach (Variant current4 in variant2._arr)
				{
					uint key = current4["id"];
					bool flag7 = current4["equip_type"] > 0;
					if (flag7)
					{
						flytxt.instance.fly("成功下架了一件" + current4["name"], 0, default(Color), null);
					}
					else
					{
						flytxt.instance.fly("成功下架了" + current4["num"] + "个" + current4["name"], 0, default(Color), null);
					}
					bool flag8 = this.gos.ContainsKey(key);
					if (flag8)
					{
						UnityEngine.Object.Destroy(this.gos[key]);
					}
					this.gos.Remove(key);
				}
			}
			this._content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (this._glg.cellSize.y + 0.1f) * (float)this.gos.Count);
			BaseProxy<A3_AuctionProxy>.getInstance().SendMyRackMsg();
		}
	}
}
