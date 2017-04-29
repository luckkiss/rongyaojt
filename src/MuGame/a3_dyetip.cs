using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_dyetip : Window
	{
		public enum eType
		{
			info,
			buy
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_dyetip.<>c <>9 = new a3_dyetip.<>c();

			public static Action<GameObject> <>9__18_0;

			internal void <initItemBuy>b__18_0(GameObject go)
			{
				flytxt.instance.fly("钻石不足，请先充值！", 0, default(Color), null);
			}
		}

		private a3_dyetip.eType _type = a3_dyetip.eType.info;

		private a3_BagItemData item_data;

		private Action backEvent = null;

		private Text totalMoney = null;

		private int buynum;

		private int maxnum;

		private BaseButton bs_bt1;

		private BaseButton bs_bt2;

		private BaseButton bs_buy;

		private Scrollbar bar = null;

		private shopDatas sd = null;

		private InputField Inputbuy_num;

		private Text buy_text;

		public override void init()
		{
			BaseButton baseButton = new BaseButton(base.transform.FindChild("touch"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onclose);
			this.Inputbuy_num = base.transform.FindChild("buy/bg/contain/bug/InputField").GetComponent<InputField>();
			this.buy_text = this.Inputbuy_num.transform.FindChild("Text").GetComponent<Text>();
			this.totalMoney = base.transform.FindChild("buy/bg/contain/paymoney/money").GetComponent<Text>();
			this.bar = base.transform.FindChild("buy/bg/contain/Scrollbar").GetComponent<Scrollbar>();
			this.bs_bt1 = new BaseButton(base.transform.FindChild("buy/bg/contain/btn_reduce"), 1, 1);
			this.bs_bt2 = new BaseButton(base.transform.FindChild("buy/bg/contain/btn_add"), 1, 1);
			this.bs_buy = new BaseButton(base.transform.FindChild("buy/bg/Button").transform, 1, 1);
			this.bs_bt1.__listener.onDown = delegate(GameObject go)
			{
				int a = this.buynum - 1;
				this.buynum = a;
				this.buynum = Mathf.Max(a, 0);
				this.bar.value = (float)this.buynum / (float)this.maxnum;
				this.buy_text.text = this.buynum.ToString();
				bool flag = this.buynum == 0;
				if (flag)
				{
					this.totalMoney.text = "0";
				}
			};
			this.bs_bt2.__listener.onDown = delegate(GameObject go)
			{
				int a = this.buynum + 1;
				this.buynum = a;
				this.buynum = Mathf.Max(a, 0);
				this.bar.value = (float)this.buynum / (float)this.maxnum;
				this.buy_text.text = this.buynum.ToString();
			};
			this.bar.onValueChanged.AddListener(delegate(float f)
			{
				this.buynum = (int)((float)this.maxnum * this.bar.value);
				this.totalMoney.text = (this.buynum * this.sd.value).ToString();
				this.buy_text.text = this.buynum.ToString();
				this.Inputbuy_num.text = this.buynum.ToString();
			});
			this.Inputbuy_num.onValueChange.AddListener(delegate(string s)
			{
				int.TryParse(s, out this.buynum);
				this.bar.value = (float)this.buynum / (float)this.maxnum;
				this.totalMoney.text = (this.buynum * this.sd.value).ToString();
				this.buy_text.text = this.buynum.ToString();
			});
		}

		public override void onShowed()
		{
			this.buynum = 0;
			this.bar.value = 0f;
			this.backEvent = null;
			base.transform.SetAsLastSibling();
			this.item_data = (a3_BagItemData)this.uiData[0];
			this._type = (a3_dyetip.eType)this.uiData[1];
			this.backEvent = (Action)this.uiData[2];
			Transform transform = base.transform.FindChild("info");
			Transform transform2 = base.transform.FindChild("buy");
			bool flag = this._type == a3_dyetip.eType.info;
			if (flag)
			{
				transform.gameObject.SetActive(true);
				transform2.gameObject.SetActive(false);
				this.initItemInfo();
			}
			else
			{
				bool flag2 = this._type == a3_dyetip.eType.buy;
				if (flag2)
				{
					transform.gameObject.SetActive(false);
					transform2.gameObject.SetActive(true);
					this.initItemBuy();
				}
			}
			this.bs_bt1.interactable = true;
			this.bs_bt2.interactable = true;
			this.bs_buy.interactable = true;
			this.buy_text.text = "1";
		}

		public override void onClosed()
		{
			bool flag = this.backEvent != null;
			if (flag)
			{
				this.backEvent();
			}
		}

		private void initItemInfo()
		{
			Transform transform = base.transform.FindChild("info");
			transform.FindChild("name").GetComponent<Text>().text = this.item_data.confdata.item_name;
			transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(this.item_data.confdata.quality);
			transform.FindChild("desc").GetComponent<Text>().text = this.item_data.confdata.desc;
			bool flag = this.item_data.confdata.use_limit > 0;
			if (flag)
			{
				transform.FindChild("lv").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.item_data.confdata.use_limit,
					"转",
					this.item_data.confdata.use_lv,
					"级"
				});
			}
			else
			{
				transform.FindChild("lv").GetComponent<Text>().text = "无限制";
			}
			Transform transform2 = transform.FindChild("icon");
			bool flag2 = transform2.childCount > 0;
			if (flag2)
			{
				UnityEngine.Object.Destroy(transform2.GetChild(0).gameObject);
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(this.item_data, false, -1, 1f, false);
			gameObject.transform.SetParent(transform2, false);
			base.transform.FindChild("info/donum").GetComponent<Text>().text = this.item_data.num.ToString();
		}

		private void initItemBuy()
		{
			Transform transform = base.transform.FindChild("buy");
			Text component = transform.transform.FindChild("bg/contain/name").GetComponent<Text>();
			Text component2 = transform.transform.FindChild("bg/contain/des_bg/Text").GetComponent<Text>();
			GameObject gameObject = transform.transform.FindChild("bg/contain/icon").gameObject;
			bool flag = gameObject.transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(gameObject.transform.GetChild(i).gameObject);
				}
			}
			GameObject gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(this.item_data, false, -1, 1f, false);
			gameObject2.transform.SetParent(gameObject.transform, false);
			component.text = ModelBase<a3_BagModel>.getInstance().getItemDataById(this.item_data.confdata.tpid).item_name;
			int quality = ModelBase<a3_BagModel>.getInstance().getItemDataById(this.item_data.confdata.tpid).quality;
			component.color = Globle.getColorByQuality(quality);
			component2.text = ModelBase<a3_BagModel>.getInstance().getItemDataById(this.item_data.confdata.tpid).desc;
			foreach (shopDatas current in ModelBase<Shop_a3Model>.getInstance().itemsdic.Values)
			{
				bool flag2 = current.itemid == (int)this.item_data.confdata.tpid;
				if (flag2)
				{
					this.sd = current;
				}
			}
			this.bar.numberOfSteps = (int)(ModelBase<PlayerModel>.getInstance().gold / (uint)this.sd.value);
			this.maxnum = this.bar.numberOfSteps;
			bool flag3 = this.maxnum <= 0;
			if (flag3)
			{
				this.bs_bt1.interactable = false;
				this.bs_bt2.interactable = false;
				this.bs_buy.interactable = false;
				BaseButton arg_246_0 = this.bs_buy;
				Action<GameObject> arg_246_1;
				if ((arg_246_1 = a3_dyetip.<>c.<>9__18_0) == null)
				{
					arg_246_1 = (a3_dyetip.<>c.<>9__18_0 = new Action<GameObject>(a3_dyetip.<>c.<>9.<initItemBuy>b__18_0));
				}
				arg_246_0.onClick = arg_246_1;
			}
			else
			{
				this.buynum = 1;
				this.totalMoney.text = (this.buynum * this.sd.value).ToString();
				this.bar.value = (float)this.buynum / (float)this.maxnum;
				this.bs_buy.onClick = delegate(GameObject goo)
				{
					bool flag4 = this.sd == null;
					if (!flag4)
					{
						bool flag5 = this.buynum >= 1;
						if (flag5)
						{
							BaseProxy<Shop_a3Proxy>.getInstance().sendinfo(2, this.sd.id, this.buynum, -1);
						}
					}
				};
			}
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_DYETIP);
		}
	}
}
