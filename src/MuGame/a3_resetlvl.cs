using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_resetlvl : Window
	{
		public static a3_resetlvl _instance;

		private GameObject m_SelfObj;

		private ProfessionAvatar m_proAvatar;

		private BaseButton btn_close;

		private BaseButton btn_description;

		private BaseButton btn_reincarnation;

		private BaseButton btn_closeDesc;

		private GameObject resetlvlDesc;

		private BaseButton touchBG;

		private GameObject roleModle;

		private Text lab_fightingCapacityValue;

		private Text nextZhuanLvl;

		private Text lab_level;

		private Text lab_experience;

		private string statusPointStr;

		private Text lab_consumeGolds;

		private Image txt_currentZhuan;

		private Image txt_targetZhuan;

		private Text lab_waradEquip;

		private uint zhuan;

		private int profession;

		private Image image1;

		private Image image10;

		private Image image100;

		private Slider sliderExperience;

		public override void init()
		{
			a3_resetlvl._instance = this;
			base.getEventTrigerByPath("body/leftbody/role/TouchDrag").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			this.resetlvlDesc = base.transform.FindChild("resetlvlDesc").gameObject;
			this.touchBG = new BaseButton(base.transform.FindChild("resetlvlDesc/Touch"), 1, 1);
			this.btn_closeDesc = new BaseButton(base.transform.FindChild("resetlvlDesc/btn_close"), 1, 1);
			this.btn_closeDesc.onClick = new Action<GameObject>(this.onTouchBGClick);
			this.touchBG.onClick = new Action<GameObject>(this.onTouchBGClick);
			this.roleModle = base.transform.FindChild("roledummy").gameObject;
			this.lab_fightingCapacityValue = base.transform.FindChild("body/leftbody/bottom/value").gameObject.GetComponent<Text>();
			this.lab_level = base.transform.FindChild("body/leftbody/title/level/value").gameObject.GetComponent<Text>();
			this.lab_experience = base.transform.FindChild("body/rightbody/body/experience/value").gameObject.GetComponent<Text>();
			this.image1 = base.transform.FindChild("body/rightbody/body/ScrollRectPanel/GridLayoutPanel/waradProperty/num/1").GetComponent<Image>();
			this.image10 = base.transform.FindChild("body/rightbody/body/ScrollRectPanel/GridLayoutPanel/waradProperty/num/10").GetComponent<Image>();
			this.image100 = base.transform.FindChild("body/rightbody/body/ScrollRectPanel/GridLayoutPanel/waradProperty/num/100").GetComponent<Image>();
			this.lab_consumeGolds = base.transform.FindChild("bottom/btn_reincarnation/consumeGolds/value").gameObject.GetComponent<Text>();
			this.lab_waradEquip = base.transform.FindChild("body/rightbody/body/ScrollRectPanel/GridLayoutPanel/waradEquip/desc").GetComponent<Text>();
			this.sliderExperience = base.transform.FindChild("body/rightbody/body/experience").GetComponent<Slider>();
			this.txt_currentZhuan = base.transform.FindChild("body/rightbody/title/currentZhuan/txtZhuan").GetComponent<Image>();
			this.txt_targetZhuan = base.transform.FindChild("body/rightbody/title/targetZhuan/txtZhuan").GetComponent<Image>();
			this.nextZhuanLvl = base.transform.FindChild("body/rightbody/title/txtLvl").GetComponent<Text>();
			this.btn_close = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			this.btn_close.onClick = new Action<GameObject>(this.onBtnCloseClick);
			this.btn_description = new BaseButton(base.transform.FindChild("title/btn_description"), 1, 1);
			this.btn_description.onClick = new Action<GameObject>(this.onDescriptionClick);
			this.btn_reincarnation = new BaseButton(base.transform.FindChild("bottom/btn_reincarnation"), 1, 1);
			this.btn_reincarnation.onClick = new Action<GameObject>(this.onReincarnationClick);
		}

		public override void onShowed()
		{
			BaseProxy<ResetLvLProxy>.getInstance().addEventListener(ResetLvLProxy.EVENT_RESETLVL, new Action<GameEvent>(this.onResetLvLSucc));
			this.lab_fightingCapacityValue.text = ModelBase<PlayerModel>.getInstance().combpt.ToString();
			this.lab_level.text = string.Format("{0}转{1}级", ModelBase<PlayerModel>.getInstance().up_lvl.ToString(), ModelBase<PlayerModel>.getInstance().lvl.ToString());
			this.profession = ModelBase<PlayerModel>.getInstance().profession;
			this.zhuan = ModelBase<PlayerModel>.getInstance().up_lvl;
			uint lvl = ModelBase<PlayerModel>.getInstance().lvl;
			uint expByResetLvL = ModelBase<ResetLvLModel>.getInstance().getExpByResetLvL(this.profession, this.zhuan, lvl);
			uint num = (ModelBase<PlayerModel>.getInstance().exp > ModelBase<ResetLvLModel>.getInstance().getAllExpByZhuan(this.profession, this.zhuan)) ? ModelBase<ResetLvLModel>.getInstance().getAllExpByZhuan(this.profession, this.zhuan) : ModelBase<PlayerModel>.getInstance().exp;
			this.lab_experience.text = string.Format("{0}/{1}", num, ModelBase<ResetLvLModel>.getInstance().getAllExpByZhuan(this.profession, this.zhuan));
			this.sliderExperience.maxValue = ModelBase<ResetLvLModel>.getInstance().getAllExpByZhuan(this.profession, this.zhuan);
			this.sliderExperience.value = ModelBase<PlayerModel>.getInstance().exp;
			this.statusPointStr = ModelBase<ResetLvLModel>.getInstance().getAwardAttrPointByZhuan(this.profession, this.zhuan).ToString();
			this.createNum(uint.Parse(this.statusPointStr));
			this.lab_waradEquip.text = this.getAwardDescStr(this.zhuan);
			this.lab_consumeGolds.text = ModelBase<ResetLvLModel>.getInstance().getNeedGoldsByZhuan(this.profession, this.zhuan).ToString();
			this.lab_consumeGolds.color = this.getGoldsColor();
			this.txt_currentZhuan.sprite = Resources.Load<Sprite>("icon/resetlvl/" + this.zhuan);
			this.txt_targetZhuan.sprite = Resources.Load<Sprite>("icon/resetlvl/" + (this.zhuan + 1u));
			this.nextZhuanLvl.text = string.Format("{0}转{1}级", this.zhuan + 1u, ModelBase<ResetLvLModel>.getInstance().getNextLvLByZhuan(this.profession, this.zhuan, num).ToString());
			this.createAvatar();
			this.btn_description.addEvent();
			this.btn_reincarnation.addEvent();
			this.btn_close.addEvent();
			this.touchBG.addEvent();
		}

		public override void onClosed()
		{
			BaseProxy<ResetLvLProxy>.getInstance().removeEventListener(ResetLvLProxy.EVENT_RESETLVL, new Action<GameEvent>(this.onResetLvLSucc));
			this.btn_close.removeAllListener();
			this.btn_description.removeAllListener();
			this.btn_reincarnation.removeAllListener();
			this.touchBG.removeAllListener();
			this.disposeAvatar();
		}

		private void Update()
		{
			bool flag = this.m_proAvatar != null;
			if (flag)
			{
				this.m_proAvatar.FrameMove();
			}
		}

		private void onTouchBGClick(GameObject go)
		{
			bool activeSelf = this.resetlvlDesc.activeSelf;
			if (activeSelf)
			{
				this.resetlvlDesc.SetActive(false);
			}
		}

		private void onBtnCloseClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_RESETLVL);
		}

		private void onDescriptionClick(GameObject go)
		{
			bool flag = !this.resetlvlDesc.activeSelf;
			if (flag)
			{
				this.resetlvlDesc.SetActive(true);
			}
		}

		private void onReincarnationClick(GameObject go)
		{
			uint needGoldsByZhuan = ModelBase<ResetLvLModel>.getInstance().getNeedGoldsByZhuan(this.profession, this.zhuan);
			bool flag = needGoldsByZhuan > ModelBase<PlayerModel>.getInstance().money;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_RESETLVL);
				flytxt.instance.fly("金币不足", 0, default(Color), null);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_GETGOLDWAY, null, false);
			}
			else
			{
				BaseProxy<ResetLvLProxy>.getInstance().sendResetLvL();
			}
		}

		private void onResetLvLSucc(GameEvent e)
		{
			SceneCamera.CheckZhuanShengCam();
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_RESETLVL);
		}

		public void createAvatar()
		{
			bool flag = this.m_SelfObj == null;
			if (flag)
			{
				bool flag2 = SelfRole._inst is P2Warrior;
				GameObject original;
				if (flag2)
				{
					original = Resources.Load<GameObject>("profession/avatar_ui/warrior_avatar");
				}
				else
				{
					bool flag3 = SelfRole._inst is P3Mage;
					if (flag3)
					{
						original = Resources.Load<GameObject>("profession/avatar_ui/mage_avatar");
					}
					else
					{
						bool flag4 = SelfRole._inst is P5Assassin;
						if (!flag4)
						{
							return;
						}
						original = Resources.Load<GameObject>("profession/avatar_ui/assa_avatar");
					}
				}
				this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-128f, 0f, 0f), Quaternion.identity) as GameObject);
				Transform transform = this.m_SelfObj.transform.FindChild("model");
				this.m_SelfObj.transform.SetParent(this.roleModle.transform, true);
				Transform[] componentsInChildren = this.m_SelfObj.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform2 = componentsInChildren[i];
					transform2.gameObject.layer = EnumLayer.LM_FX;
				}
				this.m_SelfObj.transform.localPosition = Vector3.zero;
				this.m_SelfObj.transform.localScale = new Vector3(1f, 1f, 1f);
				this.m_SelfObj.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
				this.m_SelfObj.name = "UIAvatar";
				bool flag5 = SelfRole._inst is P3Mage;
				if (flag5)
				{
					Transform parent = transform.FindChild("R_Finger1");
					original = Resources.Load<GameObject>("profession/avatar_ui/mage_r_finger_fire");
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					gameObject.transform.SetParent(parent, false);
				}
				this.m_proAvatar = new ProfessionAvatar();
				this.m_proAvatar.Init(SelfRole._inst.m_strAvatarPath, "h_", EnumLayer.LM_FX, EnumMaterial.EMT_EQUIP_H, transform, SelfRole._inst.m_strEquipEffPath);
				bool flag6 = ModelBase<a3_EquipModel>.getInstance().active_eqp.Count >= 10;
				if (flag6)
				{
					this.m_proAvatar.set_equip_eff(ModelBase<a3_EquipModel>.getInstance().GetEqpIdbyType(3), true);
				}
				this.m_proAvatar.set_body(SelfRole._inst.get_bodyid(), SelfRole._inst.get_bodyfxid());
				this.m_proAvatar.set_weaponl(SelfRole._inst.get_weaponl_id(), SelfRole._inst.get_weaponl_fxid());
				this.m_proAvatar.set_weaponr(SelfRole._inst.get_weaponr_id(), SelfRole._inst.get_weaponr_fxid());
				this.m_proAvatar.set_wing(SelfRole._inst.get_wingid(), SelfRole._inst.get_windfxid());
				transform.Rotate(Vector3.up, 90f);
			}
		}

		public void disposeAvatar()
		{
			this.m_proAvatar = null;
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_SelfObj);
			}
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				this.m_SelfObj.transform.Rotate(Vector3.up, -delta.x);
			}
		}

		private void createNum(uint num)
		{
			uint num2;
			uint.TryParse(this.statusPointStr, out num2);
			switch (num.ToString().Length)
			{
			case 1:
			{
				this.image100.gameObject.SetActive(false);
				this.image10.gameObject.SetActive(false);
				this.image1.gameObject.SetActive(true);
				Sprite sprite = Resources.Load("icon/uplvl/num/" + num2.ToString(), typeof(Sprite)) as Sprite;
				this.image1.sprite = sprite;
				break;
			}
			case 2:
			{
				this.image100.gameObject.SetActive(false);
				this.image10.gameObject.SetActive(true);
				this.image1.gameObject.SetActive(true);
				Sprite sprite2 = Resources.Load("icon/uplvl/num/" + num2.ToString().ToCharArray()[1].ToString(), typeof(Sprite)) as Sprite;
				this.image1.sprite = sprite2;
				Sprite sprite3 = Resources.Load("icon/uplvl/num/" + num2.ToString().ToCharArray()[0].ToString(), typeof(Sprite)) as Sprite;
				this.image10.sprite = sprite3;
				break;
			}
			case 3:
			{
				this.image100.gameObject.SetActive(true);
				this.image10.gameObject.SetActive(true);
				this.image1.gameObject.SetActive(true);
				Sprite sprite4 = Resources.Load("icon/uplvl/num/" + num2.ToString().ToCharArray()[1].ToString(), typeof(Sprite)) as Sprite;
				this.image10.sprite = sprite4;
				Sprite sprite5 = Resources.Load("icon/uplvl/num/" + num2.ToString().ToCharArray()[0].ToString(), typeof(Sprite)) as Sprite;
				this.image100.sprite = sprite5;
				Sprite sprite6 = Resources.Load("icon/uplvl/num/" + num2.ToString().ToCharArray()[2].ToString(), typeof(Sprite)) as Sprite;
				this.image1.sprite = sprite6;
				break;
			}
			}
		}

		private string getAwardDescStr(uint carr)
		{
			string text = string.Empty;
			List<ResetLvLAwardData> awardListById = ModelBase<ResetLvLAwardModel>.getInstance().getAwardListById(carr);
			char[] array = awardListById[3].name.ToArray<char>();
			for (int i = 0; i < array.Length; i++)
			{
				text = text + array[i].ToString() + "\n";
			}
			return text;
		}

		private Color getGoldsColor()
		{
			uint needGoldsByZhuan = ModelBase<ResetLvLModel>.getInstance().getNeedGoldsByZhuan(this.profession, this.zhuan);
			return (needGoldsByZhuan > ModelBase<PlayerModel>.getInstance().money) ? Color.red : Color.green;
		}
	}
}
