using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class TodayRechargePanel : BaseAwardCenter
	{
		private Transform root;

		private Transform itemsParent;

		private Dictionary<uint, awardCenterItem4todayRecharge> todayRechargeAwardsDic;

		private string strDesc = "今日累积充值{0}钻石可获得{1}一个";

		public TodayRechargePanel(Transform trans) : base(trans)
		{
			this.root = trans;
			this.todayRechargeAwardsDic = new Dictionary<uint, awardCenterItem4todayRecharge>();
			this.itemsParent = this.root.FindChild("awardItems/content");
			List<WelfareModel.itemWelfareData> dailyRecharge = ModelBase<WelfareModel>.getInstance().getDailyRecharge();
			foreach (WelfareModel.itemWelfareData current in dailyRecharge)
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
				awardCenterItem4todayRecharge value = new awardCenterItem4todayRecharge(gameObject.transform, iwd);
				this.todayRechargeAwardsDic.Add(current.id, value);
			}
			this.itemsParent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)(100 * (this.todayRechargeAwardsDic.Count + 2)));
			BaseProxy<welfareProxy>.getInstance().addEventListener(welfareProxy.ACCUMULATETODAYRECHARGE, new Action<GameEvent>(this.onAccumulateTodayRecharge));
		}

		public override void onShowed()
		{
			bool flag = BaseProxy<welfareProxy>.getInstance().dailyGift == null;
			if (!flag)
			{
				List<uint> list = new List<uint>();
				list.Clear();
				foreach (Variant current in BaseProxy<welfareProxy>.getInstance().dailyGift)
				{
					uint num = current;
					list.Add(num);
					bool flag2 = this.todayRechargeAwardsDic.ContainsKey(num);
					if (flag2)
					{
						this.todayRechargeAwardsDic[num].transform.FindChild("state/imgChecked").gameObject.SetActive(true);
						this.todayRechargeAwardsDic[num].transform.FindChild("state/btnGet").gameObject.SetActive(false);
						this.todayRechargeAwardsDic[num].transform.SetAsLastSibling();
					}
				}
				foreach (KeyValuePair<uint, awardCenterItem4todayRecharge> current2 in this.todayRechargeAwardsDic)
				{
					bool flag3 = list.Contains(current2.Key);
					if (!flag3)
					{
						bool flag4 = current2.Value.m_iwd.cumulateNum > welfareProxy.todayTotal_recharge;
						if (flag4)
						{
							this.todayRechargeAwardsDic[current2.Key].CanNotCheck();
						}
						else
						{
							this.todayRechargeAwardsDic[current2.Key].CanCheck();
						}
					}
				}
			}
		}

		private void onAccumulateTodayRecharge(GameEvent e)
		{
			uint key = e.data["gift_id"];
			bool flag = this.todayRechargeAwardsDic.ContainsKey(key);
			if (flag)
			{
				this.todayRechargeAwardsDic[key].Checked();
			}
		}

		public override void onClose()
		{
		}
	}
}
