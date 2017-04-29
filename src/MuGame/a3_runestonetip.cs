using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_runestonetip : Window
	{
		private BaseButton btn_sell;

		private BaseButton btn_dress;

		private BaseButton btn_out;

		private BaseButton btn_decompose;

		private BaseButton btn_down;

		private BaseButton btn_close;

		private BaseButton mask_btn;

		private BaseButton nomask_btn;

		private GameObject mask;

		private GameObject nomask;

		private GameObject icon_obj;

		private Text name_txt;

		private GameObject image;

		private GameObject contain;

		private a3_BagItemData itemdata = default(a3_BagItemData);

		private runestone_tipstype tip_type = runestone_tipstype.bag_tip;

		public static a3_runestonetip _instance;

		public override void init()
		{
			a3_runestonetip._instance = this;
			this.btn_close = new BaseButton(base.getTransformByPath("bg"), 1, 1);
			this.btn_close.onClick = new Action<GameObject>(this.closeOnclick);
			this.btn_sell = new BaseButton(base.getTransformByPath("info/btns/contain/sell"), 1, 1);
			this.btn_sell.onClick = new Action<GameObject>(this.btn_sell_Onclick);
			this.btn_dress = new BaseButton(base.getTransformByPath("info/btns/contain/do"), 1, 1);
			this.btn_dress.onClick = new Action<GameObject>(this.btn_dress_Onclick);
			this.btn_out = new BaseButton(base.getTransformByPath("info/btns/contain/output"), 1, 1);
			this.btn_out.onClick = new Action<GameObject>(this.btn_out_Onclick);
			this.btn_decompose = new BaseButton(base.getTransformByPath("info/btns/contain/fenjie"), 1, 1);
			this.btn_decompose.onClick = new Action<GameObject>(this.btn_decompose_Onclick);
			this.btn_down = new BaseButton(base.getTransformByPath("info/btns/contain/down"), 1, 1);
			this.btn_down.onClick = new Action<GameObject>(this.btn_down_Onclick);
			this.name_txt = base.getComponentByPath<Text>("info/name");
			this.image = base.getGameObjectByPath("info/scrollview/Image");
			this.contain = base.getGameObjectByPath("info/scrollview/contain");
			this.icon_obj = base.getGameObjectByPath("info/icon");
			this.mask_btn = new BaseButton(base.getTransformByPath("info/Mask"), 1, 1);
			this.mask_btn.onClick = new Action<GameObject>(this.maskOnclick);
			this.nomask_btn = new BaseButton(base.getTransformByPath("info/noMask"), 1, 1);
			this.nomask_btn.onClick = new Action<GameObject>(this.nomaskOnclick);
			this.mask = this.mask_btn.gameObject;
			this.nomask = this.nomask_btn.gameObject;
		}

		public override void onShowed()
		{
			bool flag = this.uiData != null;
			if (flag)
			{
				this.itemdata = (a3_BagItemData)this.uiData[0];
				this.tip_type = (runestone_tipstype)this.uiData[1];
			}
			this.btn_sell.gameObject.SetActive(false);
			this.btn_dress.gameObject.SetActive(false);
			this.btn_out.gameObject.SetActive(false);
			this.btn_decompose.gameObject.SetActive(false);
			this.btn_down.gameObject.SetActive(false);
			base.getGameObjectByPath("info/noMask").SetActive(true);
			base.getGameObjectByPath("info/Mask").SetActive(true);
			switch (this.tip_type)
			{
			case runestone_tipstype.bag_tip:
				this.btn_sell.gameObject.SetActive(true);
				this.btn_dress.gameObject.SetActive(true);
				this.btn_decompose.gameObject.SetActive(true);
				break;
			case runestone_tipstype.compose_tips:
				this.btn_decompose.gameObject.SetActive(true);
				break;
			case runestone_tipstype.dress_tip:
				this.btn_dress.gameObject.SetActive(true);
				this.btn_decompose.gameObject.SetActive(true);
				break;
			case runestone_tipstype.decompose_tip:
				this.btn_out.gameObject.SetActive(true);
				break;
			case runestone_tipstype.dressdown_tip:
				this.btn_down.gameObject.SetActive(true);
				base.getGameObjectByPath("info/noMask").SetActive(false);
				base.getGameObjectByPath("info/Mask").SetActive(false);
				break;
			}
			this.infos();
			base.gameObject.transform.SetAsLastSibling();
		}

		public override void onClosed()
		{
			for (int i = 0; i < this.contain.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.contain.transform.GetChild(i).gameObject);
			}
		}

		private void closeOnclick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_RUNESTONETIP);
		}

		private void infos()
		{
			bool flag = this.icon_obj.transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < this.icon_obj.transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.icon_obj.transform.GetChild(i).gameObject);
				}
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(this.itemdata, false, -1, 1f, false);
			gameObject.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			gameObject.transform.SetParent(this.icon_obj.transform);
			gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
			this.name_txt.text = ModelBase<a3_BagModel>.getInstance().getRunestoneDataByid((int)this.itemdata.tpid).item_name;
			Dictionary<int, int> runeston_att = this.itemdata.runestonedata.runeston_att;
			foreach (int current in runeston_att.Keys)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.image);
				gameObject2.SetActive(true);
				gameObject2.transform.SetParent(this.contain.transform);
				gameObject2.GetComponent<Text>().text = Globle.getAttrNameById(current) + ":" + runeston_att[current];
			}
			RectTransform component = this.contain.GetComponent<RectTransform>();
			RectTransform component2 = this.image.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component.sizeDelta.x, component2.sizeDelta.y * (float)runeston_att.Count);
			this.nomask.SetActive(!this.itemdata.ismark);
			this.mask.SetActive(this.itemdata.ismark);
		}

		private void maskOnclick(GameObject go)
		{
			BaseProxy<a3_RuneStoneProxy>.getInstance().sendporxy(7, (int)this.itemdata.id, 0, null);
			this.mask.SetActive(false);
			this.nomask.SetActive(true);
		}

		private void nomaskOnclick(GameObject go)
		{
			BaseProxy<a3_RuneStoneProxy>.getInstance().sendporxy(7, (int)this.itemdata.id, 0, null);
			this.mask.SetActive(true);
			this.nomask.SetActive(false);
			flytxt.instance.fly("被锁定的装备不会被分解", 0, default(Color), null);
		}

		private void btn_sell_Onclick(GameObject go)
		{
			BaseProxy<BagProxy>.getInstance().sendSellItems(this.itemdata.id, 1);
			this.closeOnclick(go);
		}

		private void btn_dress_Onclick(GameObject go)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.itemdata);
			this.closeOnclick(go);
			bool flag = this.tip_type == runestone_tipstype.bag_tip;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_BAG);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RUNESTONE, null, false);
			}
			else
			{
				bool flag2 = this.tip_type == runestone_tipstype.dress_tip;
				if (flag2)
				{
					BaseProxy<a3_RuneStoneProxy>.getInstance().sendporxy(1, (int)this.itemdata.id, 0, null);
				}
			}
		}

		private void btn_out_Onclick(GameObject go)
		{
			this.closeOnclick(go);
			bool flag = a3_runestone._instance != null;
			if (flag)
			{
				a3_runestone._instance.destoryIcon(this.itemdata.id);
			}
		}

		private void btn_decompose_Onclick(GameObject go)
		{
			BaseProxy<a3_RuneStoneProxy>.getInstance().sendporxy(3, (int)this.itemdata.id, 0, null);
			this.closeOnclick(go);
		}

		private void btn_down_Onclick(GameObject go)
		{
			BaseProxy<a3_RuneStoneProxy>.getInstance().sendporxy(1, (int)this.itemdata.id, 0, null);
			this.closeOnclick(go);
		}
	}
}
