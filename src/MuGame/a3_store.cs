using GameFramework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_store : Window
	{
		public static uint itm_tpid = 0u;

		private static uint min_num = 1u;

		private static uint max_num = 10000u;

		private Slider slider;

		private a3_ItemData itmdata;

		public override void init()
		{
			new BaseButton(base.transform.FindChild("addBtn"), 1, 1).onClick = new Action<GameObject>(this.OnAddButton);
			new BaseButton(base.transform.FindChild("subBtn"), 1, 1).onClick = new Action<GameObject>(this.OnSubButton);
			new BaseButton(base.transform.FindChild("buyBtn"), 1, 1).onClick = new Action<GameObject>(this.OnBuy);
			this.slider = base.getComponentByPath<Slider>("slider");
			this.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnSliderChange));
			base.getEventTrigerByPath("ig_bg_bg").onClick = new EventTriggerListener.VoidDelegate(this.onClose);
		}

		public override void onShowed()
		{
			a3_BagModel instance = ModelBase<a3_BagModel>.getInstance();
			this.itmdata = instance.getItemDataById(a3_store.itm_tpid);
			Image componentByPath = base.getComponentByPath<Image>("iconimage/icon");
			componentByPath.sprite = (Resources.Load(this.itmdata.file, typeof(Sprite)) as Sprite);
			Image componentByPath2 = base.getComponentByPath<Image>("iconimage/iconborder");
			componentByPath2.sprite = (Resources.Load(this.itmdata.borderfile, typeof(Sprite)) as Sprite);
			base.getComponentByPath<Text>("nimg/name").text = this.itmdata.item_name;
			base.getComponentByPath<Text>("desc").text = this.itmdata.desc;
			this.slider.minValue = a3_store.min_num;
			this.slider.maxValue = a3_store.max_num;
			uint num = (uint)this.slider.value;
			base.getComponentByPath<Text>("num").text = num.ToString();
			long num2 = (long)((ulong)num * (ulong)((long)this.itmdata.on_sale));
			base.getComponentByPath<Text>("money").text = num2.ToString();
		}

		private void OnSubButton(GameObject go)
		{
			uint num = (uint)this.slider.value;
			bool flag = num > a3_store.min_num;
			if (flag)
			{
				num -= 1u;
			}
			this.slider.value = num;
		}

		private void OnAddButton(GameObject go)
		{
			uint num = (uint)this.slider.value;
			bool flag = num < a3_store.max_num;
			if (flag)
			{
				num += 1u;
			}
			this.slider.value = num;
		}

		private void OnBuy(GameObject go)
		{
			bool flag = this.itmdata.on_sale <= 0;
			if (!flag)
			{
				PlayerModel instance = ModelBase<PlayerModel>.getInstance();
				bool flag2 = instance.money < this.slider.value * (float)this.itmdata.on_sale;
				if (flag2)
				{
					flytxt.instance.fly("金币不足!", 0, default(Color), null);
				}
				else
				{
					BaseProxy<Shop_a3Proxy>.getInstance().BuyStoreItems(this.itmdata.tpid, (uint)this.slider.value);
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_STORE);
				}
			}
		}

		private void OnSliderChange(float v)
		{
			uint num = (uint)v;
			base.getComponentByPath<Text>("num").text = num.ToString();
			long num2 = (long)((ulong)num * (ulong)((long)this.itmdata.on_sale));
			base.getComponentByPath<Text>("money").text = num2.ToString();
		}

		private void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_STORE);
		}
	}
}
