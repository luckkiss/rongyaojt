using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class awardCenterItem4Recharge : BaseAwardCenter
	{
		public Transform root;

		private Text txtNum;

		private Text txtInfo;

		private Transform imgChecked;

		private BaseButton btnGet;

		public WelfareModel.itemWelfareData m_iwd;

		public awardCenterItem4Recharge(Transform trans, WelfareModel.itemWelfareData iwd) : base(trans)
		{
			this.root = trans;
			this.m_iwd = iwd;
			this.txtInfo = base.getComponentByPath<Text>("txtInfor");
			this.imgChecked = base.getGameObjectByPath("state/imgChecked").transform;
			this.btnGet = new BaseButton(this.root.transform.FindChild("state/btnGet"), 1, 1);
			this.btnGet.onClick = new Action<GameObject>(this.onBtnGetClick);
			this.txtInfo.text = string.Format(iwd.desc, iwd.cumulateNum, iwd.worth);
			bool flag = iwd.zhuan > ModelBase<PlayerModel>.getInstance().up_lvl;
			if (flag)
			{
				this.btnGet.interactable = false;
				this.btnGet.transform.FindChild("Text").GetComponent<Text>().text = "未达成";
			}
			else
			{
				this.btnGet.transform.FindChild("Text").GetComponent<Text>().text = "领取";
				this.btnGet.interactable = true;
			}
		}

		public void SetInfo(a3_ItemData id)
		{
			this.txtInfo.text = id.desc;
			this.root.gameObject.SetActive(true);
		}

		public void Checked()
		{
			this.btnGet.gameObject.SetActive(false);
			this.imgChecked.gameObject.SetActive(true);
			this.root.SetAsLastSibling();
		}

		public void CanNotCheck()
		{
			this.btnGet.interactable = false;
			this.btnGet.transform.FindChild("Text").GetComponent<Text>().text = "未达成";
		}

		public void CanCheck()
		{
			this.btnGet.interactable = true;
			this.btnGet.transform.FindChild("Text").GetComponent<Text>().text = "领取";
		}

		private void onBtnGetClick(GameObject go)
		{
			BaseProxy<welfareProxy>.getInstance().sendWelfare(welfareProxy.ActiveType.accumulateRecharge, this.m_iwd.id);
		}
	}
}
