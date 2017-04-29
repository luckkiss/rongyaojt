using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class PayPanel : BaseAwardCenter
	{
		private Transform root;

		private Transform itemsParent;

		private Dictionary<uint, awardCenterItem4Consumption> consumptionAwardsDic;

		private string strDesc = "累积消费{0}钻石可获得{1}一个";

		public PayPanel(Transform trans) : base(trans)
		{
			this.root = trans;
			this.consumptionAwardsDic = new Dictionary<uint, awardCenterItem4Consumption>();
			this.itemsParent = this.root.FindChild("awardItems/content");
			List<WelfareModel.itemWelfareData> cumulateConsumption = ModelBase<WelfareModel>.getInstance().getCumulateConsumption();
			foreach (WelfareModel.itemWelfareData current in cumulateConsumption)
			{
				WelfareModel.itemWelfareData iwd = current;
				iwd.desc = this.strDesc;
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(current.itemId);
				iwd.awardName = itemDataById.item_name;
				GameObject gameObject = IconImageMgr.getInstance().creatItemAwardCenterIcon(itemDataById);
				gameObject.name = "itemWelfare";
				gameObject.transform.SetParent(this.itemsParent);
				gameObject.transform.localScale = Vector3.one;
				uint id = current.itemId;
				new BaseButton(gameObject.transform.FindChild("icon"), 1, 1).onClick = delegate(GameObject oo)
				{
					bool flag = a3_awardCenter.instan;
					if (flag)
					{
						a3_awardCenter.instan.showtip(id);
					}
				};
				awardCenterItem4Consumption value = new awardCenterItem4Consumption(gameObject.transform, iwd);
				this.consumptionAwardsDic.Add(current.id, value);
			}
			this.itemsParent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)(100 * (this.consumptionAwardsDic.Count + 2)));
			BaseProxy<welfareProxy>.getInstance().addEventListener(welfareProxy.ACCUMULATECONSUMPTION, new Action<GameEvent>(this.onAccumulateConsumption));
		}

		public override void onShowed()
		{
			bool flag = BaseProxy<welfareProxy>.getInstance().leijixiaofei == null;
			if (!flag)
			{
				List<uint> list = new List<uint>();
				list.Clear();
				foreach (Variant current in BaseProxy<welfareProxy>.getInstance().leijixiaofei)
				{
					uint num = current;
					list.Add(num);
					bool flag2 = this.consumptionAwardsDic.ContainsKey(num);
					if (flag2)
					{
						this.consumptionAwardsDic[num].transform.FindChild("state/imgChecked").gameObject.SetActive(true);
						this.consumptionAwardsDic[num].transform.FindChild("state/btnGet").gameObject.SetActive(false);
						this.consumptionAwardsDic[num].transform.SetAsLastSibling();
					}
				}
				foreach (KeyValuePair<uint, awardCenterItem4Consumption> current2 in this.consumptionAwardsDic)
				{
					bool flag3 = list.Contains(current2.Key);
					if (!flag3)
					{
						bool flag4 = current2.Value.m_iwd.cumulateNum > welfareProxy.totalXiaofei;
						if (flag4)
						{
							this.consumptionAwardsDic[current2.Key].CanNotCheck();
						}
						else
						{
							this.consumptionAwardsDic[current2.Key].CanCheck();
						}
					}
				}
			}
		}

		public override void onClose()
		{
		}

		private void onAccumulateConsumption(GameEvent e)
		{
			uint key = e.data["gift_id"];
			bool flag = this.consumptionAwardsDic.ContainsKey(key);
			if (flag)
			{
				this.consumptionAwardsDic[key].Checked();
			}
		}
	}
}
