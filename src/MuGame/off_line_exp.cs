using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class off_line_exp : Window
	{
		public static off_line_exp instance;

		private BaseButton btn1;

		private BaseButton btn2;

		private BaseButton btn3;

		private BaseButton btn4;

		private BaseButton closeBtn1;

		private BaseButton left_click;

		private BaseButton right_click;

		private Text Text_time;

		private Text Text_exp;

		private GameObject recharge;

		private int currentType = 0;

		private List<BaseButton> lBtn = new List<BaseButton>();

		private List<BaseButton> lBtn2 = new List<BaseButton>();

		public List<a3_BagItemData> offline_item = new List<a3_BagItemData>();

		public bool offline;

		private OffLineModel offLineModel;

		private GameObject eqp_icon;

		private Transform contain;

		public Toggle fenjie;

		public int mojing_num;

		public int shengguanghuiji_num;

		public int mifageli_num;

		private Image timeSlider;

		private Transform TipOne;

		public override void init()
		{
			base.init();
			off_line_exp.instance = this;
			this.contain = base.getGameObjectByPath("equp/scroll/contain").transform;
			this.eqp_icon = base.getGameObjectByPath("icon");
			this.left_click = new BaseButton(base.transform.FindChild("r_l_btn/left"), 1, 1);
			this.right_click = new BaseButton(base.transform.FindChild("r_l_btn/right"), 1, 1);
			this.Text_exp = base.transform.FindChild("exp_bg/exp").gameObject.GetComponent<Text>();
			this.fenjie = base.getComponentByPath<Toggle>("Toggle_fenjie");
			this.btn1 = new BaseButton(base.transform.FindChild("exp/btn1/btn"), 1, 1);
			this.btn2 = new BaseButton(base.transform.FindChild("exp/btn2/btn"), 1, 1);
			this.btn3 = new BaseButton(base.transform.FindChild("exp/btn3/btn"), 1, 1);
			this.btn4 = new BaseButton(base.transform.FindChild("exp/btn4/btn"), 1, 1);
			this.closeBtn1 = new BaseButton(base.transform.FindChild("closeBtn"), 1, 1);
			this.Text_time = base.getComponentByPath<Text>("state/Text_time");
			this.timeSlider = base.transform.FindChild("state/time_Slider/Fill Area/Fill").GetComponent<Image>();
			this.timeSlider.type = Image.Type.Filled;
			this.timeSlider.fillMethod = Image.FillMethod.Vertical;
			this.timeSlider.fillOrigin = 0;
			this.vip_getexp_btn();
			this.btn1.onClick = delegate(GameObject go)
			{
				this.OnClickToGetExp(1);
			};
			this.btn2.onClick = delegate(GameObject go)
			{
				this.OnClickToGetExp(2);
			};
			this.btn3.onClick = delegate(GameObject go)
			{
				this.OnClickToGetExp(3);
			};
			this.btn4.onClick = delegate(GameObject go)
			{
				this.OnClickToGetExp(4);
			};
			this.left_click.onClick = delegate(GameObject go)
			{
				this.OnClick_left();
			};
			this.right_click.onClick = delegate(GameObject go)
			{
				this.OnClick_right();
			};
			this.closeBtn1.onClick = delegate(GameObject go)
			{
				this.OnClickToClose();
			};
			this.recharge = base.transform.FindChild("recharge").gameObject;
			this.lBtn.Add(this.btn1);
			this.lBtn.Add(this.btn2);
			this.lBtn.Add(this.btn3);
			this.lBtn.Add(this.btn4);
			this.lBtn2.Add(this.closeBtn1);
			this.lBtn2.Add(this.left_click);
			this.lBtn2.Add(this.right_click);
			this.offLineModel = ModelBase<OffLineModel>.getInstance();
		}

		private void outItemCon_fenjie()
		{
		}

		private void vip_getexp_btn()
		{
			bool flag = ModelBase<A3_VipModel>.getInstance().Level < this.vip_lite(3);
			if (flag)
			{
				this.btn3.transform.FindChild("image1").gameObject.SetActive(false);
				this.btn3.transform.FindChild("image2").gameObject.SetActive(true);
				this.btn3.transform.FindChild("image2/Text").GetComponent<Text>().text = "VIP" + this.vip_lite(3) + "开启";
			}
			else
			{
				this.btn3.transform.FindChild("image2").gameObject.SetActive(false);
				this.btn3.transform.FindChild("image1").gameObject.SetActive(true);
				this.btn3.transform.FindChild("image1/Text").GetComponent<Text>().text = "领取";
			}
			bool flag2 = ModelBase<A3_VipModel>.getInstance().Level < this.vip_lite(4);
			if (flag2)
			{
				this.btn4.transform.FindChild("image1").gameObject.SetActive(false);
				this.btn4.transform.FindChild("image2").gameObject.SetActive(true);
				this.btn4.transform.FindChild("image2/Text").GetComponent<Text>().text = "VIP" + this.vip_lite(4) + "开启";
			}
			else
			{
				this.btn4.transform.FindChild("image2").gameObject.SetActive(false);
				this.btn4.transform.FindChild("image1").gameObject.SetActive(true);
				this.btn4.transform.FindChild("image1/Text").GetComponent<Text>().text = "领取";
			}
		}

		private void OnClick_right()
		{
			float x = this.eqp_icon.GetComponent<RectTransform>().sizeDelta.x;
			float y = this.contain.GetComponent<RectTransform>().anchoredPosition.y;
			float x2 = this.contain.GetComponent<RectTransform>().anchoredPosition.x;
			float num = this.eqp_icon.GetComponent<RectTransform>().sizeDelta.x * (float)Mathf.CeilToInt((float)BaseProxy<OffLineExpProxy>.getInstance().eqp.Count / 4f);
			float num2 = -(num - this.eqp_icon.GetComponent<RectTransform>().sizeDelta.x * 4f) + 10f;
			bool flag = this.contain.GetComponent<RectTransform>().anchoredPosition.x <= num2;
			if (!flag)
			{
				this.contain.GetComponent<RectTransform>().anchoredPosition = new Vector2(x2 - x * 8f, y);
			}
		}

		private void OnClick_left()
		{
			float x = this.eqp_icon.GetComponent<RectTransform>().sizeDelta.x;
			float y = this.contain.GetComponent<RectTransform>().anchoredPosition.y;
			float x2 = this.contain.GetComponent<RectTransform>().anchoredPosition.x;
			float num = this.eqp_icon.GetComponent<RectTransform>().sizeDelta.x * (float)Mathf.CeilToInt((float)BaseProxy<OffLineExpProxy>.getInstance().eqp.Count / 4f);
			bool flag = this.contain.GetComponent<RectTransform>().anchoredPosition.x >= num - this.eqp_icon.GetComponent<RectTransform>().sizeDelta.x * 4f - 10f;
			if (!flag)
			{
				this.contain.GetComponent<RectTransform>().anchoredPosition = new Vector2(x2 + x * 8f, y);
			}
		}

		public override void onShowed()
		{
			bool flag = ModelBase<PlayerModel>.getInstance().last_time == 0 && BaseProxy<OffLineExpProxy>.getInstance().eqp.Count == 0;
			if (flag)
			{
				base.transform.FindChild("equp/image_con").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("equp/image_con").gameObject.SetActive(false);
			}
			this.show_eqp();
			for (int i = 0; i < this.lBtn.Count; i++)
			{
				this.lBtn[i].addEvent();
				Text component = this.lBtn[i].transform.FindChild("Text_exp").GetComponent<Text>();
				component.text = "经验+" + this.offLineModel.BaseExp * (i + 1);
			}
			base.onShowed();
			this.vip_getexp_btn();
			BaseProxy<OffLineExpProxy>.getInstance().addEventListener(OffLineExpProxy.EVENT_OFFLINE_EXP_GET, new Action<GameEvent>(this.doGetExp));
			this.OnCostTextChange();
			this.OnTitleChange();
			OffLineModel expr_11B = this.offLineModel;
			expr_11B.OnOffLineTimeChange = (Action)Delegate.Combine(expr_11B.OnOffLineTimeChange, new Action(this.OnTitleChange));
			OffLineModel expr_142 = this.offLineModel;
			expr_142.OnBaseExpChange = (Action)Delegate.Combine(expr_142.OnBaseExpChange, new Action(this.OnCostTextChange));
		}

		private void show_eqp()
		{
			bool flag = BaseProxy<OffLineExpProxy>.getInstance().eqp.Count <= 16;
			if (flag)
			{
				this.contain.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
				this.contain.GetComponent<GridLayoutGroup>().constraintCount = 8;
			}
			else
			{
				this.contain.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedRowCount;
				this.contain.GetComponent<GridLayoutGroup>().constraintCount = 2;
			}
			using (List<a3_BagItemData>.Enumerator enumerator = BaseProxy<OffLineExpProxy>.getInstance().eqp.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					a3_BagItemData d = enumerator.Current;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.eqp_icon);
					gameObject.SetActive(true);
					gameObject.transform.SetParent(this.contain.transform, false);
					gameObject.transform.FindChild("equp").GetComponent<Image>().sprite = (Resources.Load("icon/item/" + d.tpid, typeof(Sprite)) as Sprite);
					Transform arg_131_0 = gameObject.transform;
					object arg_12C_0 = "quality_bg/";
					a3_ItemData confdata = d.confdata;
					arg_131_0.FindChild(arg_12C_0 + confdata.equip_level).gameObject.SetActive(true);
					BaseButton baseButton = new BaseButton(gameObject.transform.FindChild("equp").transform, 1, 1);
					baseButton.onClick = delegate(GameObject goo)
					{
						ArrayList arrayList = new ArrayList();
						a3_BagItemData d = d;
						arrayList.Add(d);
						arrayList.Add(equip_tip_type.Comon_tip);
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
					};
				}
			}
			bool flag2 = BaseProxy<OffLineExpProxy>.getInstance().eqp.Count < 16;
			if (flag2)
			{
				for (int i = 0; i < 16 - BaseProxy<OffLineExpProxy>.getInstance().eqp.Count; i++)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.eqp_icon);
					gameObject2.SetActive(true);
					gameObject2.transform.SetParent(this.contain.transform, false);
					gameObject2.transform.FindChild("equp").gameObject.SetActive(false);
				}
			}
			else
			{
				bool flag3 = BaseProxy<OffLineExpProxy>.getInstance().eqp.Count % 2 != 0;
				if (flag3)
				{
					GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.eqp_icon);
					gameObject3.SetActive(true);
					gameObject3.transform.SetParent(this.contain.transform, false);
					gameObject3.transform.FindChild("equp").gameObject.SetActive(false);
				}
			}
			this.contain.GetComponent<RectTransform>().sizeDelta = new Vector2((this.eqp_icon.GetComponent<RectTransform>().sizeDelta.x + 2.5f) * (float)Mathf.CeilToInt((float)BaseProxy<OffLineExpProxy>.getInstance().eqp.Count / 2f), this.eqp_icon.GetComponent<RectTransform>().sizeDelta.x * 2f);
		}

		public override void onClosed()
		{
			foreach (BaseButton current in this.lBtn)
			{
				current.removeAllListener();
			}
			base.onClosed();
			BaseProxy<OffLineExpProxy>.getInstance().removeEventListener(OffLineExpProxy.EVENT_OFFLINE_EXP_GET, new Action<GameEvent>(this.doGetExp));
			OffLineModel expr_64 = this.offLineModel;
			expr_64.OnOffLineTimeChange = (Action)Delegate.Remove(expr_64.OnOffLineTimeChange, new Action(this.OnTitleChange));
			OffLineModel expr_8B = this.offLineModel;
			expr_8B.OnBaseExpChange = (Action)Delegate.Remove(expr_8B.OnBaseExpChange, new Action(this.OnCostTextChange));
		}

		private void OnClickToClose()
		{
			bool flag = this.contain.transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < this.contain.transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.contain.transform.GetChild(i).gameObject);
				}
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.OFFLINEEXP);
		}

		private void OnTitleChange()
		{
			string item = (this.offLineModel.OffLineTime % 3600 / 60).ToString();
			string item2 = (this.offLineModel.OffLineTime / 3600).ToString();
			this.Text_time.text = ContMgr.getCont("off_line_exp_time", new List<string>
			{
				item2,
				item
			});
			this.timeSlider.fillAmount = (float)ModelBase<OffLineModel>.getInstance().OffLineTime / 86400f;
			this.Text_exp.text = "EXP " + this.offLineModel.BaseExp;
		}

		private void OnCostTextChange()
		{
			for (int i = 1; i < this.lBtn.Count; i++)
			{
				Text component = this.lBtn[i].transform.FindChild("Text_cost").GetComponent<Text>();
				component.text = ((int)this.offLineModel.GetCost(i + 1)).ToString();
			}
		}

		private int vip_lite(int type)
		{
			SXML node = ModelBase<OffLineModel>.getInstance().OffLineXML.GetNode("rate_type", "type==" + type.ToString());
			return node.getInt("vip_level");
		}

		private void OnClickToGetExp(int type)
		{
			A3_VipModel a3_VipModel = ModelBase<A3_VipModel>.getInstance();
			bool flag = !this.offLineModel.CanGetExp;
			if (flag)
			{
				flytxt.instance.fly(ContMgr.getCont("off_line_empty", null), 0, default(Color), null);
				InterfaceMgr.getInstance().close(InterfaceMgr.OFFLINEEXP);
				a3_expbar.instance.getGameObjectByPath("operator/LightTips/btnAuto_off_line_exp").SetActive(false);
			}
			else
			{
				this.offline_item.Clear();
				this.offline = true;
				switch (type)
				{
				case 1:
				{
					bool isOn = this.fenjie.isOn;
					if (isOn)
					{
						BaseProxy<OffLineExpProxy>.getInstance().sendType(1, true);
					}
					else
					{
						BaseProxy<OffLineExpProxy>.getInstance().sendType(1, false);
					}
					this.currentType = 1;
					break;
				}
				case 2:
				{
					bool flag2 = ModelBase<PlayerModel>.getInstance().money < ModelBase<OffLineModel>.getInstance().GetCost(2);
					if (flag2)
					{
						flytxt.instance.fly(ContMgr.getCont("off_line_exp_money", null), 0, default(Color), null);
					}
					else
					{
						bool isOn2 = this.fenjie.isOn;
						if (isOn2)
						{
							BaseProxy<OffLineExpProxy>.getInstance().sendType(2, true);
						}
						else
						{
							BaseProxy<OffLineExpProxy>.getInstance().sendType(2, false);
						}
					}
					this.currentType = 2;
					break;
				}
				case 3:
				{
					bool flag3 = ModelBase<PlayerModel>.getInstance().gold < ModelBase<OffLineModel>.getInstance().GetCost(3);
					if (flag3)
					{
						flytxt.instance.fly(ContMgr.getCont("off_line_exp_gold", null), 0, default(Color), null);
					}
					else
					{
						bool isOn3 = this.fenjie.isOn;
						if (isOn3)
						{
							BaseProxy<OffLineExpProxy>.getInstance().sendType(3, true);
						}
						else
						{
							BaseProxy<OffLineExpProxy>.getInstance().sendType(3, false);
						}
					}
					this.currentType = 3;
					break;
				}
				case 4:
				{
					bool flag4 = ModelBase<PlayerModel>.getInstance().gold < ModelBase<OffLineModel>.getInstance().GetCost(4);
					if (flag4)
					{
						flytxt.instance.fly(ContMgr.getCont("off_line_exp_gold", null), 0, default(Color), null);
					}
					else
					{
						bool isOn4 = this.fenjie.isOn;
						if (isOn4)
						{
							BaseProxy<OffLineExpProxy>.getInstance().sendType(4, true);
						}
						else
						{
							BaseProxy<OffLineExpProxy>.getInstance().sendType(4, false);
						}
					}
					this.currentType = 4;
					break;
				}
				}
			}
		}

		private void doGetExp(GameEvent e)
		{
			off_line_exp expr_06 = off_line_exp.instance;
			bool flag = expr_06 != null && expr_06.offline;
			if (flag)
			{
				bool flag2 = off_line_exp.instance.offline_item != null;
				if (flag2)
				{
					foreach (a3_BagItemData current in off_line_exp.instance.offline_item)
					{
						a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(current.tpid);
						GameObject showIcon = IconImageMgr.getInstance().createA3ItemIconTip(current.tpid, false, current.num, 1f, false, -1, 0, false, false, false);
						flytxt.instance.fly(null, 6, default(Color), showIcon);
					}
				}
				off_line_exp.instance.offline = false;
				off_line_exp.instance.offline_item.Clear();
			}
			Variant data = e.data;
			int num = data["res"];
			this.offLineModel.OffLineTime = 0;
			this.offLineModel.BaseExp = 0;
			debug.Log("离线经验的服务器反馈" + e.data.dump());
			InterfaceMgr.getInstance().close(InterfaceMgr.OFFLINEEXP);
		}
	}
}
