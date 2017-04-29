using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class RechargePanel : BaseAwardCenter
	{
		private Transform root;

		private Transform itemsParent;

		private Dictionary<uint, awardCenterItem4Recharge> rechargeAwardsDic;

		private string strDesc = "累积充值{0}钻石可获得价值{1}的大礼包一个";

		public RechargePanel(Transform trans) : base(trans)
		{
			this.root = trans;
			this.rechargeAwardsDic = new Dictionary<uint, awardCenterItem4Recharge>();
			this.itemsParent = this.root.FindChild("awardItems/content");
			List<WelfareModel.itemWelfareData> cumulateRechargeAward = ModelBase<WelfareModel>.getInstance().getCumulateRechargeAward();
			foreach (WelfareModel.itemWelfareData current in cumulateRechargeAward)
			{
				WelfareModel.itemWelfareData iwd = current;
				iwd.desc = this.strDesc;
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(current.itemId);
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
				awardCenterItem4Recharge value = new awardCenterItem4Recharge(gameObject.transform, iwd);
				this.rechargeAwardsDic.Add(current.id, value);
			}
			this.itemsParent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)(100 * (this.rechargeAwardsDic.Count + 2)));
			BaseProxy<welfareProxy>.getInstance().addEventListener(welfareProxy.ACCUMULATERECHARGE, new Action<GameEvent>(this.onAccumulateRecharge));
		}

		public override void onShowed()
		{
			bool flag = BaseProxy<welfareProxy>.getInstance().leijichongzhi == null;
			if (!flag)
			{
				List<uint> list = new List<uint>();
				list.Clear();
				foreach (Variant current in BaseProxy<welfareProxy>.getInstance().leijichongzhi)
				{
					uint num = current;
					list.Add(num);
					bool flag2 = this.rechargeAwardsDic.ContainsKey(num);
					if (flag2)
					{
						this.rechargeAwardsDic[num].transform.FindChild("state/imgChecked").gameObject.SetActive(true);
						this.rechargeAwardsDic[num].transform.FindChild("state/btnGet").gameObject.SetActive(false);
						this.rechargeAwardsDic[num].transform.SetAsLastSibling();
					}
				}
				foreach (KeyValuePair<uint, awardCenterItem4Recharge> current2 in this.rechargeAwardsDic)
				{
					bool flag3 = list.Contains(current2.Key);
					if (!flag3)
					{
						bool flag4 = current2.Value.m_iwd.cumulateNum > welfareProxy.totalRecharge;
						if (flag4)
						{
							this.rechargeAwardsDic[current2.Key].CanNotCheck();
						}
						else
						{
							this.rechargeAwardsDic[current2.Key].CanCheck();
						}
					}
				}
			}
		}

		public override void onClose()
		{
		}

		private void onAccumulateRecharge(GameEvent e)
		{
			uint key = e.data["gift_id"];
			bool flag = this.rechargeAwardsDic.ContainsKey(key);
			if (flag)
			{
				this.rechargeAwardsDic[key].Checked();
			}
		}
	}
}
