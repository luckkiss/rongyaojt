using GameFramework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_getJewelryWay : Window
	{
		private BaseButton btn_close;

		private Text cs_mwlr;

		private Text cs_jjc;

		private Text cs_dgsl;

		private BaseButton mwlr_bt;

		private BaseButton jjc_bt;

		private BaseButton dgsl_bt;

		public static a3_getJewelryWay instance;

		public string closeWin = null;

		private bool towin = false;

		public override void init()
		{
			a3_getJewelryWay.instance = this;
			this.btn_close = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			this.btn_close.onClick = new Action<GameObject>(this.onBtnCloseClick);
			this.mwlr_bt = new BaseButton(base.getTransformByPath("cells/scroll/content/mwlr/go"), 1, 1);
			this.mwlr_bt.onClick = new Action<GameObject>(this.mwlr_go);
			this.jjc_bt = new BaseButton(base.getTransformByPath("cells/scroll/content/jjc/go"), 1, 1);
			this.jjc_bt.onClick = new Action<GameObject>(this.jjc_go);
			this.dgsl_bt = new BaseButton(base.getTransformByPath("cells/scroll/content/dgsl/go"), 1, 1);
			this.dgsl_bt.onClick = new Action<GameObject>(this.dgsl_go);
			this.cs_mwlr = base.getTransformByPath("cells/scroll/content/mwlr/name/dj").GetComponent<Text>();
			this.cs_jjc = base.getTransformByPath("cells/scroll/content/jjc/name/dj").GetComponent<Text>();
			this.cs_dgsl = base.getTransformByPath("cells/scroll/content/dgsl/name/dj").GetComponent<Text>();
		}

		public override void onShowed()
		{
			this.towin = false;
			bool flag = this.uiData != null && this.uiData.Count > 0;
			if (flag)
			{
				this.closeWin = (string)this.uiData[0];
			}
			base.transform.SetAsLastSibling();
			this.refresh();
		}

		public override void onClosed()
		{
			bool flag = !this.towin;
			if (flag)
			{
				this.closeWin = null;
			}
		}

		private void refresh()
		{
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.DEVIL_HUNTER, false);
			if (flag)
			{
				bool flag2 = ModelBase<A3_ActiveModel>.getInstance().mwlr_charges <= 0;
				if (flag2)
				{
					this.mwlr_bt.gameObject.GetComponent<Button>().interactable = false;
				}
				else
				{
					this.mwlr_bt.gameObject.GetComponent<Button>().interactable = true;
				}
				this.cs_mwlr.text = string.Concat(new object[]
				{
					"(",
					ModelBase<A3_ActiveModel>.getInstance().mwlr_charges,
					"/",
					10,
					")"
				});
			}
			else
			{
				base.transform.FindChild("cells/scroll/content/mwlr").gameObject.SetActive(false);
			}
			bool flag3 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.PVP_DUNGEON, false);
			if (flag3)
			{
				this.cs_jjc.text = string.Concat(new object[]
				{
					"(",
					ModelBase<A3_ActiveModel>.getInstance().callenge_cnt - ModelBase<A3_ActiveModel>.getInstance().pvpCount + ModelBase<A3_ActiveModel>.getInstance().buyCount,
					"/",
					ModelBase<A3_ActiveModel>.getInstance().callenge_cnt,
					")"
				});
				bool flag4 = ModelBase<A3_ActiveModel>.getInstance().callenge_cnt - ModelBase<A3_ActiveModel>.getInstance().pvpCount + ModelBase<A3_ActiveModel>.getInstance().buyCount <= 0;
				if (flag4)
				{
					this.jjc_bt.gameObject.GetComponent<Button>().interactable = false;
				}
				else
				{
					this.jjc_bt.gameObject.GetComponent<Button>().interactable = true;
				}
			}
			else
			{
				base.transform.FindChild("cells/scroll/content/jjc").gameObject.SetActive(false);
			}
			bool flag5 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.GLOBA_BOSS, false);
			if (flag5)
			{
				this.cs_dgsl.text = "";
			}
			else
			{
				base.transform.FindChild("cells/scroll/content/dgsl").gameObject.SetActive(false);
			}
		}

		public void setTxt_jjc(bool b)
		{
			this.cs_jjc.text = string.Concat(new object[]
			{
				"(",
				ModelBase<A3_ActiveModel>.getInstance().callenge_cnt - ModelBase<A3_ActiveModel>.getInstance().pvpCount + ModelBase<A3_ActiveModel>.getInstance().buyCount,
				"/",
				ModelBase<A3_ActiveModel>.getInstance().callenge_cnt,
				")"
			});
			if (b)
			{
				bool flag = ModelBase<A3_ActiveModel>.getInstance().callenge_cnt - ModelBase<A3_ActiveModel>.getInstance().pvpCount + ModelBase<A3_ActiveModel>.getInstance().buyCount <= 0;
				if (flag)
				{
					this.jjc_bt.gameObject.GetComponent<Button>().interactable = false;
				}
				else
				{
					this.jjc_bt.gameObject.GetComponent<Button>().interactable = true;
				}
			}
			else
			{
				this.jjc_bt.gameObject.GetComponent<Button>().interactable = false;
			}
		}

		public void onBtnCloseClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_GETJEWELRYWAY);
		}

		private void mwlr_go(GameObject go)
		{
			this.towin = true;
			ArrayList arrayList = new ArrayList();
			arrayList.Add("mwlr");
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_GETJEWELRYWAY);
			InterfaceMgr.getInstance().close(this.closeWin);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList, false);
		}

		private void jjc_go(GameObject go)
		{
			this.towin = true;
			ArrayList arrayList = new ArrayList();
			arrayList.Add("pvp");
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_GETJEWELRYWAY);
			InterfaceMgr.getInstance().close(this.closeWin);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList, false);
		}

		private void dgsl_go(GameObject go)
		{
			this.towin = true;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(ELITE_MONSTER_PAGE_IDX.BOSSPAGE);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_GETJEWELRYWAY);
			InterfaceMgr.getInstance().close(this.closeWin);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_ELITEMON, arrayList, false);
		}
	}
}
