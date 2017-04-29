using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_Recharge : Window
	{
		private BaseButton btnClose;

		private BaseButton btnVipPri;

		public static a3_Recharge Instance;

		private Text Vip_lvl;

		private Text NeedCount;

		private Text ToNextVip;

		private Text bindDiamond;

		private Text Diamond;

		private GameObject toptext;

		private GameObject isManLvl;

		private Dictionary<int, GameObject> retra = new Dictionary<int, GameObject>();

		private Image ExpImg;

		public override void init()
		{
			a3_Recharge.Instance = this;
			this.isManLvl = base.transform.FindChild("topCon/isMaxLvl").gameObject;
			this.btnClose = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			this.btnClose.onClick = new Action<GameObject>(this.OnCLoseClick);
			this.btnVipPri = new BaseButton(base.getTransformByPath("btn_VipPri"), 1, 1);
			this.btnVipPri.onClick = new Action<GameObject>(this.OnVipPri);
			this.Vip_lvl = base.getComponentByPath<Text>("topCon/Image_level/Text");
			this.NeedCount = base.getComponentByPath<Text>("topCon/Text_bg_1/Text_num");
			this.ToNextVip = base.getComponentByPath<Text>("topCon/Text_level");
			this.toptext = base.transform.FindChild("topCon/Text_bg_1").gameObject;
			this.bindDiamond = base.getComponentByPath<Text>("bindDiamond/Text");
			this.Diamond = base.getComponentByPath<Text>("Diamond/Text");
			this.ExpImg = base.getComponentByPath<Image>("topCon/Image_exp");
			this.recharge_Refresh();
		}

		public override void onShowed()
		{
			GRMap.GAME_CAMERA.SetActive(false);
			UIClient.instance.addEventListener(9005u, new Action<GameEvent>(this.Refresh_Diamond));
			A3_VipModel expr_2E = ModelBase<A3_VipModel>.getInstance();
			expr_2E.OnExpChange = (Action)Delegate.Combine(expr_2E.OnExpChange, new Action(this.OnExpChange));
			A3_VipModel expr_54 = ModelBase<A3_VipModel>.getInstance();
			expr_54.OnLevelChange = (Action)Delegate.Combine(expr_54.OnLevelChange, new Action(this.OnVipLevelChange));
			this.OnVipLevelChange();
			this.OnExpChange();
			this.Refresh_Diamond(null);
			bool flag = a3_relive.instans;
			if (flag)
			{
				base.transform.SetAsLastSibling();
				a3_relive.instans.FX.SetActive(false);
			}
		}

		public override void onClosed()
		{
			GRMap.GAME_CAMERA.SetActive(true);
			UIClient.instance.removeEventListener(9005u, new Action<GameEvent>(this.Refresh_Diamond));
			A3_VipModel expr_2E = ModelBase<A3_VipModel>.getInstance();
			expr_2E.OnExpChange = (Action)Delegate.Remove(expr_2E.OnExpChange, new Action(this.OnExpChange));
			A3_VipModel expr_54 = ModelBase<A3_VipModel>.getInstance();
			expr_54.OnLevelChange = (Action)Delegate.Remove(expr_54.OnLevelChange, new Action(this.OnVipLevelChange));
			bool flag = a3_relive.instans;
			if (flag)
			{
				a3_relive.instans.FX.SetActive(true);
			}
		}

		private void recharge_Refresh()
		{
			bool flag = this.retra.Count > 0;
			if (!flag)
			{
				GameObject gameObject = base.transform.FindChild("buy_bg/item").gameObject;
				RectTransform component = base.transform.FindChild("buy_bg/scrollview/con").GetComponent<RectTransform>();
				Dictionary<int, rechargeData> dictionary = new Dictionary<int, rechargeData>();
				dictionary = ModelBase<RechargeModel>.getInstance().rechargeMenu;
				foreach (int current in dictionary.Keys)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.SetActive(true);
					gameObject2.transform.SetParent(component, false);
					Text component2 = gameObject2.transform.FindChild("name").GetComponent<Text>();
					component2.text = dictionary[current].name;
					Text component3 = gameObject2.transform.FindChild("money/Text").GetComponent<Text>();
					component3.text = "￥" + dictionary[current].golden;
					Text component4 = gameObject2.transform.FindChild("item_text").GetComponent<Text>();
					component4.text = StringUtils.formatText(dictionary[current].desc);
					bool flag2 = dictionary[current].first_double > 0;
					if (flag2)
					{
						gameObject2.transform.FindChild("double").gameObject.SetActive(true);
					}
					else
					{
						gameObject2.transform.FindChild("double").gameObject.SetActive(false);
					}
					gameObject2.transform.FindChild("icon_di/icon_Img").GetComponent<Image>().sprite = (Resources.Load("icon/recharge/" + dictionary[current].id.ToString(), typeof(Sprite)) as Sprite);
					rechargeData dta = dictionary[current];
					BaseButton baseButton = new BaseButton(gameObject2.transform.FindChild("money"), 1, 1);
					baseButton.onClick = delegate(GameObject go)
					{
						this.onEnsure(dta);
					};
					this.retra[current] = gameObject2;
				}
				float x = component.GetComponent<GridLayoutGroup>().cellSize.x;
				Vector2 sizeDelta = new Vector2((float)dictionary.Count * x, component.sizeDelta.y);
				component.sizeDelta = sizeDelta;
			}
		}

		private void onEnsure(rechargeData dta)
		{
			GameSdkMgr.Pay(dta);
		}

		private void OnExpChange()
		{
			int nextLvlMaxExp = ModelBase<A3_VipModel>.getInstance().GetNextLvlMaxExp();
			int num = nextLvlMaxExp - ModelBase<A3_VipModel>.getInstance().Exp;
			this.NeedCount.text = num.ToString();
			bool flag = nextLvlMaxExp > 0;
			if (flag)
			{
				this.ExpImg.fillAmount = (float)ModelBase<A3_VipModel>.getInstance().Exp / (float)nextLvlMaxExp;
			}
			else
			{
				this.ExpImg.fillAmount = 1f;
			}
		}

		private void OnVipLevelChange()
		{
			int level = ModelBase<A3_VipModel>.getInstance().Level;
			this.Vip_lvl.text = level.ToString();
			bool flag = level >= ModelBase<A3_VipModel>.getInstance().GetMaxVipLevel();
			if (flag)
			{
				this.ToNextVip.gameObject.SetActive(false);
				this.toptext.SetActive(false);
				this.isManLvl.SetActive(true);
			}
			else
			{
				this.ToNextVip.text = "VIP" + (level + 1);
				this.ToNextVip.gameObject.SetActive(true);
				this.toptext.SetActive(true);
				this.isManLvl.SetActive(false);
			}
		}

		private void Refresh_Diamond(GameEvent e)
		{
			this.bindDiamond.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
			this.Diamond.text = ModelBase<PlayerModel>.getInstance().gold.ToString();
		}

		private void OnCLoseClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_RECHARGE);
		}

		private void OnVipPri(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_RECHARGE);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_VIP, null, false);
		}
	}
}
