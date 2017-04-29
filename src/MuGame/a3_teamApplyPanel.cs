using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_teamApplyPanel : Window
	{
		private class itemTeamApplyPanel
		{
			private Text txtInfor;

			private ItemTeamData m_itd;

			private GameObject m_go;

			private Text txtCD;

			private string strCD = "({0})";

			private float cdTime = 8f;

			private TickItem showTime;

			public itemTeamApplyPanel(Transform trans, ItemTeamData itd)
			{
				this.m_go = UnityEngine.Object.Instantiate<GameObject>(trans.gameObject);
				this.m_go.SetActive(true);
				this.m_go.transform.SetParent(trans.parent);
				this.m_go.transform.localScale = Vector3.one;
				this.m_go.transform.localPosition = Vector3.zero;
				this.m_go.transform.SetAsFirstSibling();
				this.txtInfor = this.m_go.transform.FindChild("body/Text").GetComponent<Text>();
				this.txtCD = this.m_go.transform.FindChild("body/txtCD").GetComponent<Text>();
				this.txtCD.text = string.Format(this.strCD, this.cdTime);
				this.showTime = new TickItem(new Action<float>(this.onUpdates));
				TickMgr.instance.addTick(this.showTime);
				BaseButton baseButton = new BaseButton(this.m_go.transform.FindChild("title/btnClose"), 1, 1);
				BaseButton baseButton2 = new BaseButton(this.m_go.transform.FindChild("bottom/btnOk"), 1, 1);
				BaseButton baseButton3 = new BaseButton(this.m_go.transform.FindChild("bottom/btnCancle"), 1, 1);
				baseButton3.onClick = new Action<GameObject>(this.onBtnCancleClick);
				baseButton.onClick = new Action<GameObject>(this.onBtnCloseClick);
				baseButton2.onClick = new Action<GameObject>(this.onBtnOKClick);
				this.m_itd = itd;
				string name = itd.name;
				string professional = ModelBase<A3_TeamModel>.getInstance().getProfessional(itd.carr);
				uint zhuan = itd.zhuan;
				uint lvl = itd.lvl;
				this.txtInfor.text = string.Format(this.txtInfor.text, new object[]
				{
					itd.name,
					professional,
					zhuan,
					lvl
				});
				trans.transform.SetAsLastSibling();
			}

			private void onUpdates(float s)
			{
				this.cdTime -= s;
				bool flag = this.cdTime <= 0f;
				if (flag)
				{
					TickMgr.instance.removeTick(this.showTime);
					UnityEngine.Object.Destroy(this.m_go);
				}
				else
				{
					this.txtCD.text = string.Format(this.strCD, (int)this.cdTime);
				}
			}

			private void onBtnCloseClick(GameObject go)
			{
				TickMgr.instance.removeTick(this.showTime);
				BaseProxy<TeamProxy>.getInstance().SendAffirmApply(this.m_itd.cid, false);
				UnityEngine.Object.Destroy(this.m_go);
			}

			private void onBtnCancleClick(GameObject go)
			{
				TickMgr.instance.removeTick(this.showTime);
				BaseProxy<TeamProxy>.getInstance().SendAffirmApply(this.m_itd.cid, false);
				UnityEngine.Object.Destroy(this.m_go);
			}

			private void onBtnOKClick(GameObject go)
			{
				TickMgr.instance.removeTick(this.showTime);
				BaseProxy<TeamProxy>.getInstance().SendAffirmApply(this.m_itd.cid, true);
				UnityEngine.Object.Destroy(this.m_go);
			}
		}

		private Transform itemApplyPanelFrefab;

		public static a3_teamApplyPanel mInstance;

		public override bool showBG
		{
			get
			{
				return false;
			}
		}

		public override void init()
		{
			a3_teamApplyPanel.mInstance = this;
			base.gameObject.SetActive(false);
			this.itemApplyPanelFrefab = base.transform.FindChild("body");
		}

		public void Show(ItemTeamData itd)
		{
			base.gameObject.SetActive(true);
			base.transform.SetAsLastSibling();
			new a3_teamApplyPanel.itemTeamApplyPanel(this.itemApplyPanelFrefab, itd);
		}

		public override void onClosed()
		{
		}
	}
}
