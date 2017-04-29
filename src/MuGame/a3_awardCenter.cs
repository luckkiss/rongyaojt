using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_awardCenter : Window
	{
		private TabControl _tabControl;

		private Transform _itemAward;

		private BaseAwardCenter _current;

		private BaseAwardCenter _lvlUpAwardPanel;

		private BaseAwardCenter _rechargePanel;

		private BaseAwardCenter _payPanel;

		private BaseAwardCenter _todayRechargePanel;

		private Transform _content;

		public GameObject tip;

		public static a3_awardCenter instan;

		public override void init()
		{
			this._content = base.transform.FindChild("body/right");
			this._itemAward = base.transform.FindChild("temp/itemAward");
			this.tip = base.transform.FindChild("tip").gameObject;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("title/btnClose"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnCloseClick);
			this._tabControl = new TabControl();
			this._tabControl.onClickHanle = new Action<TabControl>(this.OnSwitch);
			this._tabControl.create(base.getGameObjectByPath("body/left"), base.gameObject, 0, 0, false);
		}

		public override void onShowed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			this.tip.SetActive(false);
			a3_awardCenter.instan = this;
			BaseProxy<welfareProxy>.getInstance().sendWelfare(welfareProxy.ActiveType.selfWelfareInfo, 4294967295u);
			bool flag = this._current != null;
			if (flag)
			{
				this._current.onShowed();
			}
			else
			{
				this._tabControl.setSelectedIndex(0, false);
				this.OnSwitch(this._tabControl);
			}
			GRMap.GAME_CAMERA.SetActive(false);
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
			a3_awardCenter.instan = null;
		}

		public void showtip(uint id)
		{
			this.tip.SetActive(true);
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(id);
			this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().text = itemDataById.item_name;
			this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().color = Globle.getColorByQuality(itemDataById.quality);
			bool flag = itemDataById.use_limit <= 0;
			if (flag)
			{
				this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = "无限制";
			}
			else
			{
				this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = itemDataById.use_limit + "转";
			}
			this.tip.transform.FindChild("text_bg/text").GetComponent<Text>().text = StringUtils.formatText(itemDataById.desc);
			this.tip.transform.FindChild("text_bg/iconbg/icon").GetComponent<Image>().sprite = (Resources.Load(itemDataById.file, typeof(Sprite)) as Sprite);
			new BaseButton(this.tip.transform.FindChild("close_btn"), 1, 1).onClick = delegate(GameObject oo)
			{
				this.tip.SetActive(false);
			};
		}

		public void OnSwitch(TabControl tabc)
		{
			int seletedIndex = tabc.getSeletedIndex();
			bool flag = this._current != null;
			if (flag)
			{
				this._current.onClose();
				this._current.gameObject.SetActive(false);
			}
			bool flag2 = seletedIndex == 0;
			if (flag2)
			{
				bool flag3 = this._lvlUpAwardPanel == null;
				if (flag3)
				{
					Transform trans = base.transform.FindChild("temp/lvlAwardPanel");
					this._lvlUpAwardPanel = new LvlUpAwardPanel(trans);
					this._lvlUpAwardPanel.setPerent(this._content);
				}
				this._current = this._lvlUpAwardPanel;
			}
			bool flag4 = seletedIndex == 1;
			if (flag4)
			{
				bool flag5 = this._rechargePanel == null;
				if (flag5)
				{
					Transform trans2 = base.transform.FindChild("temp/rechargePanel");
					this._rechargePanel = new RechargePanel(trans2);
					this._rechargePanel.setPerent(this._content);
				}
				this._current = this._rechargePanel;
			}
			bool flag6 = seletedIndex == 2;
			if (flag6)
			{
				bool flag7 = this._payPanel == null;
				if (flag7)
				{
					Transform trans3 = base.transform.FindChild("temp/payPanel");
					this._payPanel = new PayPanel(trans3);
					this._payPanel.setPerent(this._content);
				}
				this._current = this._payPanel;
			}
			bool flag8 = seletedIndex == 3;
			if (flag8)
			{
				bool flag9 = this._todayRechargePanel == null;
				if (flag9)
				{
					Transform trans4 = base.transform.FindChild("temp/toDayRechargePanel");
					this._todayRechargePanel = new TodayRechargePanel(trans4);
					this._todayRechargePanel.setPerent(this._content);
				}
				this._current = this._todayRechargePanel;
			}
			this._current.onShowed();
			this._current.gameObject.SetActive(true);
		}

		private void onBtnCloseClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_AWARDCENTER);
		}
	}
}
