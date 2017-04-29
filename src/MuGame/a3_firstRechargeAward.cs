using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_firstRechargeAward : Window
	{
		private BaseButton btnRecharge;

		private Transform content2d;

		private GameObject m_EquipObj;

		private GameObject m_Self_Camera;

		private bool Toclose = false;

		public override void init()
		{
			this.btnRecharge = new BaseButton(base.transform.FindChild("body/btnRecharge"), 1, 1);
			this.content2d = base.transform.FindChild("body/awardItems/content");
			this.btnRecharge.onClick = new Action<GameObject>(this.onBtnRechargeClick);
			BaseButton baseButton = new BaseButton(base.transform.FindChild("title/btnClose"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnCloseClick);
			this.createItem();
			this.createEquip();
			base.getEventTrigerByPath("body/left/avatar_touch").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			this.createPic();
			BaseProxy<welfareProxy>.getInstance().addEventListener(welfareProxy.SUCCESSGETFIRSTRECHARGEAWARD, new Action<GameEvent>(this.onSuccessGetFirstRechargeAward));
		}

		public override void onShowed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			this.Toclose = false;
			base.transform.FindChild("tip").gameObject.SetActive(false);
			bool flag = welfareProxy.totalRecharge <= 0u;
			if (flag)
			{
				this.btnRecharge.transform.FindChild("Text").GetComponent<Text>().text = "立即充值";
			}
			else
			{
				bool flag2 = welfareProxy.firstRecharge <= 0u;
				if (flag2)
				{
					this.btnRecharge.transform.FindChild("Text").GetComponent<Text>().text = "未领取";
					this.btnRecharge.removeAllListener();
					this.btnRecharge.addEvent();
					this.btnRecharge.onClick = new Action<GameObject>(this.onBtnDrawClick);
				}
				else
				{
					this.btnRecharge.transform.FindChild("Text").GetComponent<Text>().text = "已领取";
					this.btnRecharge.removeAllListener();
				}
			}
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			this.disposeAvatar();
			InterfaceMgr.getInstance().itemToWin(this.Toclose, this.uiName);
		}

		private void onBtnRechargeClick(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_FIRESTRECHARGEAWARD);
		}

		private void onBtnDrawClick(GameObject go)
		{
			BaseProxy<welfareProxy>.getInstance().sendWelfare(welfareProxy.ActiveType.firstRechange, 4294967295u);
		}

		private void onSuccessGetFirstRechargeAward(GameEvent e)
		{
			this.btnRecharge.transform.FindChild("Text").GetComponent<Text>().text = "已领取";
			this.btnRecharge.removeAllListener();
		}

		private void onBtnCloseClick(GameObject go)
		{
			this.Toclose = true;
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_FIRESTRECHARGEAWARD);
		}

		private void createItem()
		{
			Dictionary<a3_ItemData, uint> firstChargeDataList = ModelBase<WelfareModel>.getInstance().getFirstChargeDataList();
			using (Dictionary<a3_ItemData, uint>.Enumerator enumerator = firstChargeDataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<a3_ItemData, uint> item = enumerator.Current;
					a3_ItemData key = item.Key;
					bool flag = item.Value == 0u;
					if (flag)
					{
						GameObject gameObject = base.transform.FindChild("body/awardItems/item").gameObject;
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
						gameObject2.SetActive(true);
						gameObject2.transform.SetParent(this.content2d, false);
						GameObject gameObject3 = IconImageMgr.getInstance().createA3ItemIcon(key, true, -1, 0.9f, false, -1, 0, false, false, false, -1, false, false);
						gameObject3.transform.SetParent(gameObject2.transform, false);
						new BaseButton(gameObject3.transform, 1, 1).onClick = delegate(GameObject go)
						{
							this.setTip(item.Key, false);
						};
						gameObject3.gameObject.SetActive(true);
					}
				}
			}
		}

		private void createEquip()
		{
			Dictionary<a3_ItemData, uint> firstChargeDataList = ModelBase<WelfareModel>.getInstance().getFirstChargeDataList();
			using (Dictionary<a3_ItemData, uint>.Enumerator enumerator = firstChargeDataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<a3_ItemData, uint> item = enumerator.Current;
					a3_ItemData key = item.Key;
					bool flag = item.Value > 0u;
					if (flag)
					{
						bool flag2 = item.Value == (uint)ModelBase<PlayerModel>.getInstance().profession;
						if (flag2)
						{
							GameObject gameObject = base.transform.FindChild("body/awardItems/equip").gameObject;
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
							gameObject2.SetActive(true);
							gameObject2.transform.SetParent(this.content2d, false);
							GameObject gameObject3 = IconImageMgr.getInstance().createA3ItemIcon(key, true, -1, 1f, false, -1, 0, false, false, false, -1, false, false);
							gameObject3.transform.FindChild("iconborder").gameObject.SetActive(false);
							gameObject3.transform.SetParent(gameObject2.transform, false);
							gameObject3.gameObject.SetActive(true);
							new BaseButton(gameObject3.transform, 1, 1).onClick = delegate(GameObject go)
							{
								this.setTip(item.Key, true);
							};
						}
					}
				}
			}
		}

		private void setTip(a3_ItemData item, bool Eqp = false)
		{
			base.transform.FindChild("tip").gameObject.SetActive(true);
			base.transform.FindChild("tip/text_bg/name/namebg").GetComponent<Text>().text = item.item_name;
			base.transform.FindChild("tip/text_bg/name/namebg").GetComponent<Text>().color = Globle.getColorByQuality(item.quality);
			if (Eqp)
			{
				base.transform.FindChild("tip/text_bg/name/lite").GetComponent<Text>().text = "使用职业：";
				switch (item.job_limit)
				{
				case 1:
					base.transform.FindChild("tip/text_bg/name/dengji").GetComponent<Text>().text = "无限制";
					break;
				case 2:
					base.transform.FindChild("tip/text_bg/name/dengji").GetComponent<Text>().text = "狂战士";
					break;
				case 3:
					base.transform.FindChild("tip/text_bg/name/dengji").GetComponent<Text>().text = "法师";
					break;
				case 5:
					base.transform.FindChild("tip/text_bg/name/dengji").GetComponent<Text>().text = "暗影";
					break;
				}
			}
			else
			{
				base.transform.FindChild("tip/text_bg/name/lite").GetComponent<Text>().text = "使用等级：";
				bool flag = item.use_limit <= 0;
				if (flag)
				{
					base.transform.FindChild("tip/text_bg/name/dengji").GetComponent<Text>().text = "无限制";
				}
				else
				{
					base.transform.FindChild("tip/text_bg/name/dengji").GetComponent<Text>().text = item.use_limit + "转";
				}
			}
			base.transform.FindChild("tip/text_bg/text").GetComponent<Text>().text = StringUtils.formatText(item.desc);
			base.transform.FindChild("tip/text_bg/iconbg/icon").GetComponent<Image>().sprite = (Resources.Load(item.file, typeof(Sprite)) as Sprite);
			new BaseButton(base.transform.FindChild("tip/close_btn"), 1, 1).onClick = delegate(GameObject oo)
			{
				base.transform.FindChild("tip").gameObject.SetActive(false);
			};
		}

		public void createAvatar(int id, int profession)
		{
			bool flag = this.m_EquipObj == null;
			if (flag)
			{
				GameObject gameObject = Resources.Load<GameObject>("profession/" + this.getProfession(profession) + "/weaponl_l_" + id.ToString());
				bool flag2 = gameObject == null;
				if (flag2)
				{
					gameObject = Resources.Load<GameObject>("profession/" + this.getProfession(profession) + "/weaponr_l_" + id.ToString());
				}
				bool flag3 = gameObject == null;
				if (flag3)
				{
					Debug.Log("无法找到模型文件.");
				}
				else
				{
					this.m_EquipObj = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-128f, 0f, 0f), Quaternion.identity) as GameObject);
					Transform[] componentsInChildren = this.m_EquipObj.GetComponentsInChildren<Transform>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						Transform transform = componentsInChildren[i];
						transform.gameObject.layer = EnumLayer.LM_FX;
					}
					this.m_EquipObj.transform.localPosition = new Vector3(0f, 10f, 0f);
					this.m_EquipObj.transform.localScale = new Vector3(1f, 1f, 1f);
					this.m_EquipObj.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
					this.m_EquipObj.name = "UIEquip";
					gameObject = Resources.Load<GameObject>("profession/avatar_ui/ui_FirstRechargeCamera");
					this.m_Self_Camera = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					Camera componentInChildren = this.m_Self_Camera.GetComponentInChildren<Camera>();
				}
			}
		}

		private void createPic()
		{
			bool flag = SelfRole._inst is P5Assassin;
			if (flag)
			{
				base.transform.FindChild("body/eqp").GetComponent<Image>().sprite = (Resources.Load("icon/eqp_cw/assa", typeof(Sprite)) as Sprite);
			}
			else
			{
				bool flag2 = SelfRole._inst is P2Warrior;
				if (flag2)
				{
					base.transform.FindChild("body/eqp").GetComponent<Image>().sprite = (Resources.Load("icon/eqp_cw/warrior", typeof(Sprite)) as Sprite);
				}
				else
				{
					bool flag3 = SelfRole._inst is P3Mage;
					if (flag3)
					{
						base.transform.FindChild("body/eqp").GetComponent<Image>().sprite = (Resources.Load("icon/eqp_cw/mage", typeof(Sprite)) as Sprite);
					}
				}
			}
		}

		private string getProfession(int profession)
		{
			string result = string.Empty;
			switch (profession)
			{
			case 2:
				result = "warrior";
				break;
			case 3:
				result = "mage";
				break;
			case 5:
				result = "assa";
				break;
			}
			return result;
		}

		public void disposeAvatar()
		{
			bool flag = this.m_EquipObj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_EquipObj);
			}
			bool flag2 = this.m_Self_Camera != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.m_Self_Camera);
			}
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.m_EquipObj != null;
			if (flag)
			{
				this.m_EquipObj.transform.Rotate(Vector3.forward, -delta.x);
			}
		}
	}
}
