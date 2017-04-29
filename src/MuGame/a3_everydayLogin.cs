using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_everydayLogin : Window
	{
		private class itemAward : Skin
		{
			public uint _dayNum;

			public uint _dayCount;

			private Transform _root;

			private Transform _toggleImage;

			private Transform canget_obj;

			private Text _txtDay;

			private BaseButton _btnGet;

			private bool _isChecked = false;

			private string _name;

			private uint _num;

			private List<string> strDay = new List<string>
			{
				"一",
				"二",
				"三",
				"四",
				"五",
				"六",
				"七"
			};

			public itemAward(a3_everydayLogin.itemAwardData iad) : base(iad.trSelf)
			{
				Transform trSelf = iad.trSelf;
				this._root = iad.trSelf;
				this._root.SetParent(iad.parent);
				this._root.localScale = Vector3.one;
				this._toggleImage = base.getTransformByPath("btnGet/toggleImage");
				Transform transformByPath = base.getTransformByPath("btnGet/parent");
				this.canget_obj = base.getTransformByPath("btnGet/canget");
				this._dayNum = iad.dayNum;
				this._dayCount = iad.dayCount;
				this._toggleImage.gameObject.SetActive(iad.isChecked);
				bool flag = this._dayNum == this._dayCount;
				if (flag)
				{
					this.canget_obj.gameObject.SetActive(true);
					base.transform.FindChild("btnGet/this").gameObject.SetActive(true);
					new BaseButton(this.canget_obj.transform, 1, 1).onClick = new Action<GameObject>(this.onBtnGetClick);
					base.transform.FindChild("icon").gameObject.SetActive(true);
					base.transform.FindChild("iconh").gameObject.SetActive(false);
				}
				else
				{
					this.canget_obj.gameObject.SetActive(false);
					base.transform.FindChild("btnGet/this").gameObject.SetActive(false);
					base.transform.FindChild("icon").gameObject.SetActive(false);
					base.transform.FindChild("iconh").gameObject.SetActive(true);
				}
				this._name = iad.name;
				this._num = iad.awardnum;
				BaseProxy<welfareProxy>.getInstance().addEventListener(welfareProxy.SHOWDAILYRECHARGE, new Action<GameEvent>(this.onShowDaily));
			}

			private void onShowDaily(GameEvent e)
			{
				Variant data = e.data;
				uint num = data["day_count"];
				bool flag = num == this._dayNum + 1u;
				if (flag)
				{
					this._isChecked = true;
					this._toggleImage.gameObject.SetActive(true);
					string cont = ContMgr.getCont("everDayLogin_flytxt0", new List<string>
					{
						this._name
					});
					flytxt.instance.fly(cont + this._num, 0, default(Color), null);
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_EVERYDAYLOGIN);
				}
			}

			private void onBtnGetClick(GameObject go)
			{
				bool flag = this._dayNum < this._dayCount;
				if (flag)
				{
					flytxt.instance.fly("奖励已领取.", 0, default(Color), null);
				}
				else
				{
					bool flag2 = this._dayNum > this._dayCount;
					if (flag2)
					{
						flytxt.instance.fly("日期未到无法领取.", 0, default(Color), null);
					}
					else
					{
						bool isChecked = this._isChecked;
						if (isChecked)
						{
							flytxt.instance.fly("奖励已领取.", 0, default(Color), null);
						}
						else
						{
							bool flag3 = a3_new_pet.instance != null;
							if (flag3)
							{
								a3_new_pet.instance.openEveryLogin = false;
							}
							a3_everydayLogin.instans.open = false;
							BaseProxy<welfareProxy>.getInstance().sendWelfare(welfareProxy.ActiveType.accumulateLogin, 4294967295u);
							a3_expbar.instance.getGameObjectByPath("operator/LightTips/everyDayLogin").SetActive(false);
						}
					}
				}
			}

			private void onThisClick(GameObject go)
			{
				bool flag = a3_everydayLogin.instans.thisobj != null;
				if (flag)
				{
					a3_everydayLogin.instans.thisobj.SetActive(false);
				}
				bool flag2 = a3_everydayLogin.instans.thisGetbtn != null;
				if (flag2)
				{
					a3_everydayLogin.instans.thisGetbtn.SetActive(false);
				}
				else
				{
					this.canget_obj.gameObject.SetActive(false);
				}
				a3_everydayLogin.instans.thisobj = go.transform.FindChild("this").gameObject;
			}
		}

		private struct itemAwardData
		{
			public Transform parent;

			public Transform trSelf;

			public Transform trAward;

			public uint dayNum;

			public uint dayCount;

			public bool isChecked;

			public bool canGet;

			public string name;

			public uint awardnum;
		}

		private Transform _content;

		private Transform _itemAwardPrefab;

		private uint day_count;

		private Transform _toggleImage;

		private Transform _canget;

		public static a3_everydayLogin instans;

		public GameObject thisobj = null;

		public GameObject thisGetbtn = null;

		public bool open = true;

		public override void init()
		{
			a3_everydayLogin.instans = this;
			new BaseButton(base.transform.FindChild("title/bgClose"), 1, 1).onClick = new Action<GameObject>(this.onBtnClose);
			this._content = base.transform.FindChild("body/awardItems/content");
			this._itemAwardPrefab = base.transform.FindChild("temp/itemAward");
			BaseButton baseButton = new BaseButton(base.transform.FindChild("title/btnClose"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnClose);
			this._toggleImage = base.transform.FindChild("body/btnGet/toggleImage");
			this._toggleImage.gameObject.SetActive(false);
			BaseProxy<welfareProxy>.getInstance().addEventListener(welfareProxy.SHOWDAILYRECHARGE, new Action<GameEvent>(this.onShowDaily));
			bool flag = a3_pet_renew.instance != null;
			if (flag)
			{
				base.transform.SetSiblingIndex(base.transform.GetSiblingIndex() - 1);
			}
		}

		private void onBtnClose(GameObject go)
		{
			bool flag = a3_new_pet.instance != null;
			if (flag)
			{
				a3_new_pet.instance.openEveryLogin = false;
			}
			this.open = false;
			InterfaceMgr.getInstance().floatUI.localScale = Vector3.one;
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EVERYDAYLOGIN);
		}

		public override void onShowed()
		{
			bool flag = this.uiData != null;
			if (flag)
			{
				InterfaceMgr.getInstance().floatUI.localScale = Vector3.zero;
				uint num = (uint)this.uiData[0];
				this.day_count = (uint)this.uiData[1];
				this.createAward(this.day_count);
			}
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().floatUI.localScale = Vector3.one;
		}

		private void createAward(uint dayCount)
		{
			List<WelfareModel.itemWelfareData> dailyLogin = ModelBase<WelfareModel>.getInstance().getDailyLogin();
			for (int i = 0; i < dailyLogin.Count; i++)
			{
				bool flag = i < 7;
				if (flag)
				{
					WelfareModel.itemWelfareData itemWelfareData = dailyLogin[i];
					a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(itemWelfareData.itemId);
					bool isChecked = false;
					bool canGet = false;
					bool flag2 = (long)i < (long)((ulong)dayCount);
					if (flag2)
					{
						isChecked = true;
					}
					bool flag3 = (long)i == (long)((ulong)dayCount);
					if (flag3)
					{
						canGet = true;
					}
					GameObject gameObject = base.transform.FindChild("body/awardItems/content/itemAward" + (i + 1)).gameObject;
					a3_everydayLogin.itemAward itemAward = new a3_everydayLogin.itemAward(new a3_everydayLogin.itemAwardData
					{
						parent = this._content,
						trSelf = gameObject.transform,
						dayNum = (uint)i,
						dayCount = dayCount,
						isChecked = isChecked,
						canGet = canGet,
						name = itemDataById.item_name,
						awardnum = itemWelfareData.num
					});
				}
			}
		}

		public void onBtnGetClick(GameObject go)
		{
			bool flag = 7u != this.day_count + 1u;
			if (flag)
			{
				flytxt.instance.fly("日期未到无法领取.", 0, default(Color), null);
			}
			else
			{
				bool flag2 = 7u == this.day_count;
				if (flag2)
				{
					flytxt.instance.fly("奖励已领取.", 0, default(Color), null);
				}
				else
				{
					bool flag3 = a3_new_pet.instance != null;
					if (flag3)
					{
						a3_new_pet.instance.openEveryLogin = false;
					}
					this.open = false;
					BaseProxy<welfareProxy>.getInstance().sendWelfare(welfareProxy.ActiveType.accumulateLogin, 4294967295u);
				}
			}
		}

		private void onShowDaily(GameEvent e)
		{
			Variant data = e.data;
			uint num = data["day_count"];
			bool flag = num == 7u;
			if (flag)
			{
				this._toggleImage.gameObject.SetActive(true);
				this._canget.gameObject.SetActive(false);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_EVERYDAYLOGIN);
			}
		}
	}
}
