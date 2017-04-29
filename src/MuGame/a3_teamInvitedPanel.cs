using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_teamInvitedPanel : Window
	{
		private Text txtInfor;

		private ItemTeamData m_itd;

		private Text txtCD;

		private string strCD = "({0})";

		private float cdTime = 30f;

		private TickItem showTime;

		public static a3_teamInvitedPanel mInstance;

		public override bool showBG
		{
			get
			{
				return false;
			}
		}

		public override void init()
		{
			a3_teamInvitedPanel.mInstance = this;
			this.txtInfor = base.transform.FindChild("body/body/Text").GetComponent<Text>();
			this.txtCD = base.transform.FindChild("body/body/txtCD").GetComponent<Text>();
			BaseButton baseButton = new BaseButton(base.transform.FindChild("body/title/btnClose"), 1, 1);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("body/bottom/btnOk"), 1, 1);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("body/bottom/btnCancle"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onBtnCancleClick);
			baseButton.onClick = new Action<GameObject>(this.onBtnCloseClick);
			baseButton2.onClick = new Action<GameObject>(this.onBtnOKClick);
		}

		public override void onShowed()
		{
			bool flag = this.uiData != null;
			if (flag)
			{
				this.cdTime = 30f;
				this.txtCD.text = string.Format(this.strCD, this.cdTime);
				this.showTime = new TickItem(new Action<float>(this.onUpdates));
				TickMgr.instance.addTick(this.showTime);
				ItemTeamData itemTeamData = (ItemTeamData)this.uiData[0];
				this.m_itd = itemTeamData;
				string name = itemTeamData.name;
				string professional = ModelBase<A3_TeamModel>.getInstance().getProfessional(itemTeamData.carr);
				uint zhuan = itemTeamData.zhuan;
				uint lvl = itemTeamData.lvl;
				this.txtInfor.text = string.Format(this.txtInfor.text, new object[]
				{
					name,
					professional,
					zhuan,
					lvl
				});
			}
			base.transform.SetAsLastSibling();
		}

		public override void onClosed()
		{
			bool flag = this.showTime != null;
			if (flag)
			{
				TickMgr.instance.removeTick(this.showTime);
			}
		}

		private void onUpdates(float s)
		{
			this.cdTime -= s;
			bool flag = this.cdTime <= 0f;
			if (flag)
			{
				TickMgr.instance.removeTick(this.showTime);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_TEAMINVITEDPANEL);
			}
			else
			{
				this.txtCD.text = string.Format(this.strCD, (int)this.cdTime);
			}
		}

		private void onBtnCloseClick(GameObject go)
		{
			TickMgr.instance.removeTick(this.showTime);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_TEAMINVITEDPANEL);
		}

		private void onBtnCancleClick(GameObject go)
		{
			BaseProxy<TeamProxy>.getInstance().SendAffirmInvite(this.m_itd.cid, this.m_itd.teamId, false);
			TickMgr.instance.removeTick(this.showTime);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_TEAMINVITEDPANEL);
		}

		private void onBtnOKClick(GameObject go)
		{
			a3_expbar.instance.showBtnTeamTips(false);
			TickMgr.instance.removeTick(this.showTime);
			BaseProxy<TeamProxy>.getInstance().SendAffirmInvite(this.m_itd.cid, this.m_itd.teamId, true);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_TEAMINVITEDPANEL);
		}
	}
}
