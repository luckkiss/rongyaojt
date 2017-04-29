using GameFramework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_hudun : Window
	{
		private BaseButton btnClose;

		private BaseButton btnUpLevel;

		private BaseButton btnAddEnergy;

		private Text huDunLevel;

		private Text huDunCount;

		private Text mjCount;

		public Toggle isAuto;

		private HudunModel HudunModel;

		public a3_BagModel bagModel;

		private PlayerModel playerModel;

		private A3_HudunProxy hudunProxy;

		public static a3_hudun _instance;

		public static a3_hudun isshow;

		private Animator ani;

		private Image bar;

		private bool isCan = false;

		private GameObject scene_Camera;

		private GameObject scene_Obj;

		private GameObject Shield_obj;

		private GameObject FX_open;

		private GameObject FX_open_Clon;

		private int speed = 0;

		private int oldCount = 0;

		private int newCount = 0;

		public override void init()
		{
			a3_hudun._instance = this;
			this.btnClose = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			this.btnClose.onClick = new Action<GameObject>(this.OnCLoseClick);
			this.btnUpLevel = new BaseButton(base.getTransformByPath("ig_bg1/qianghua"), 1, 1);
			this.btnUpLevel.onClick = new Action<GameObject>(this.UpLevel);
			this.btnAddEnergy = new BaseButton(base.getTransformByPath("ig_bg1/chongneng"), 1, 1);
			this.btnAddEnergy.onClick = new Action<GameObject>(this.AddEnergy);
			new BaseButton(base.getTransformByPath("ig_bg1/help"), 1, 1).onClick = new Action<GameObject>(this.onHelp);
			new BaseButton(base.getTransformByPath("ig_bg1/tishi/close"), 1, 1).onClick = new Action<GameObject>(this.close_tishi);
			this.huDunLevel = base.getComponentByPath<Text>("ig_bg1/top/topImage/Text");
			this.huDunCount = base.getComponentByPath<Text>("ig_bg1/top/shuzhi");
			this.mjCount = base.getComponentByPath<Text>("ig_bg1/mjText/count");
			this.isAuto = base.getComponentByPath<Toggle>("ig_bg1/Toggle");
			this.bar = base.getComponentByPath<Image>("ig_bg1/bar/bar_n");
			this.isAuto.onValueChanged.AddListener(new UnityAction<bool>(this.add_isAuto));
			this.HudunModel = ModelBase<HudunModel>.getInstance();
			this.playerModel = ModelBase<PlayerModel>.getInstance();
			this.hudunProxy = BaseProxy<A3_HudunProxy>.getInstance();
			this.bagModel = ModelBase<a3_BagModel>.getInstance();
			this.hudunProxy.sendinfo(0);
			base.transform.FindChild("ig_bg_bg").gameObject.SetActive(false);
			this.updata_hd(this.HudunModel.NowCount);
			base.init();
		}

		public override void onShowed()
		{
			a3_hudun.isshow = this;
			this.updata_hd(this.HudunModel.NowCount);
			this.showbtnUpLevel();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			base.onShowed();
			GRMap.GAME_CAMERA.SetActive(false);
			this.createSence();
			this.createShield();
		}

		public override void onClosed()
		{
			a3_hudun.isshow = null;
			this.disposeAvatar();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			base.onClosed();
			GRMap.GAME_CAMERA.SetActive(true);
		}

		private void add_isAuto(bool v)
		{
			bool isOn = this.isAuto.isOn;
			if (isOn)
			{
				this.HudunModel.is_auto = true;
				this.hudunProxy.sendinfo(3, 1);
				a3_herohead.instance.Add_energy_auto(this.HudunModel.auto_time, this.HudunModel.is_auto);
			}
			else
			{
				this.HudunModel.is_auto = false;
				this.hudunProxy.sendinfo(3, 0);
			}
		}

		private void showbtnUpLevel()
		{
			bool flag = this.HudunModel.Level >= this.HudunModel.hdData.Count;
			if (flag)
			{
				this.btnUpLevel.interactable = false;
			}
			else
			{
				this.btnUpLevel.interactable = ModelBase<HudunModel>.getInstance().CheckLevelupAvailable();
			}
		}

		private void UpLevel(GameObject go)
		{
			bool flag = this.OnMjCountOk(this.HudunModel.GetNeedMjMun(this.HudunModel.Level + 1));
			if (flag)
			{
				bool flag2 = this.HudunModel.Level >= this.HudunModel.hdData.Count;
				if (flag2)
				{
					flytxt.instance.fly("神圣护盾等级已达上限！！", 1, default(Color), null);
				}
				else
				{
					this.hudunProxy.sendinfo(1);
				}
			}
			else
			{
				flytxt.instance.fly("魔晶数量不足！！", 1, default(Color), null);
			}
		}

		public void AniUpLvl()
		{
			this.ani.SetBool("isopen", true);
			bool flag = this.FX_open_Clon != null;
			if (flag)
			{
				this.FX_open_Clon.SetActive(false);
			}
			this.FX_open_Clon = UnityEngine.Object.Instantiate<GameObject>(this.FX_open);
			this.FX_open_Clon.SetActive(true);
			this.FX_open_Clon.transform.SetParent(this.Shield_obj.transform, false);
			UnityEngine.Object.Destroy(this.FX_open_Clon, 2f);
			this.isCan = true;
		}

		private void Update()
		{
			bool flag = this.isCan;
			if (flag)
			{
				bool flag2 = this.ani.GetCurrentAnimatorStateInfo(0).IsName("open");
				if (flag2)
				{
					this.ani.SetBool("isopen", false);
					this.isCan = false;
				}
			}
			bool flag3 = this.HudunModel.Level != 0;
			if (flag3)
			{
				this.goUp();
			}
		}

		private void AddEnergy(GameObject go)
		{
			bool flag = !this.HudunModel.isNoAttack;
			if (flag)
			{
				flytxt.instance.fly("战斗中不能充能！！", 1, default(Color), null);
			}
			else
			{
				bool flag2 = this.HudunModel.Level <= 0;
				if (flag2)
				{
					flytxt.instance.fly("神圣护盾等级为零！！", 1, default(Color), null);
				}
				else
				{
					bool flag3 = this.HudunModel.NowCount >= this.HudunModel.GetMaxCount(this.HudunModel.Level);
					if (flag3)
					{
						flytxt.instance.fly("神圣护盾的能量已满！！", 1, default(Color), null);
					}
					else
					{
						bool flag4 = this.OnMjCountOk(this.HudunModel.hdData[this.HudunModel.Level].addcount);
						if (flag4)
						{
							this.hudunProxy.sendinfo(2);
						}
						else
						{
							flytxt.instance.fly("魔晶数量不足！！", 1, default(Color), null);
						}
					}
				}
			}
		}

		public void updata_hd(int oldcount)
		{
			this.showbtnUpLevel();
			bool flag = this.HudunModel.Level == 0;
			if (flag)
			{
				this.huDunCount.text = "0/0";
				this.bar.fillAmount = 0f;
			}
			else
			{
				this.huDunCount.text = this.HudunModel.NowCount.ToString() + "/" + this.HudunModel.GetMaxCount(this.HudunModel.Level).ToString();
				this.refbar(oldcount);
			}
			this.huDunLevel.text = this.HudunModel.Level.ToString();
			this.isAuto.isOn = this.HudunModel.is_auto;
			bool flag2 = this.HudunModel.Level >= this.HudunModel.hdData.Count;
			if (flag2)
			{
				this.mjCount.transform.parent.gameObject.SetActive(false);
			}
			else
			{
				this.mjCount.text = this.HudunModel.GetNeedMjMun(this.HudunModel.Level + 1).ToString();
			}
		}

		public void createSence()
		{
			GameObject original = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera");
			this.scene_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
			this.scene_Camera.transform.FindChild("Main_Avatar_Camera").GetComponent<Camera>().orthographicSize = 0.86f;
			original = Resources.Load<GameObject>("profession/avatar_ui/show_scene");
			this.scene_Obj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.49f, 15.1f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
			Transform[] componentsInChildren = this.scene_Obj.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.gameObject.name == "scene_ta";
				if (flag)
				{
					transform.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
				}
				else
				{
					transform.gameObject.layer = EnumLayer.LM_FX;
				}
			}
			this.scene_Obj.transform.FindChild("scene_ta").localPosition = new Vector3(-1.08f, 0.39f, -0.166f);
			this.scene_Obj.transform.FindChild("scene_ta").localScale = new Vector3(0.6f, 0.6f, 0.6f);
			this.scene_Obj.transform.FindChild("sc_tz_lg").localPosition = new Vector3(-1.08f, 0.39f, -0.166f);
			this.scene_Obj.transform.FindChild("sc_tz_lg").localScale = new Vector3(1f, 1f, 1f);
			this.scene_Obj.transform.FindChild("fx_sc").localPosition = new Vector3(-1.08f, 0.461f, -0.32f);
			this.scene_Obj.transform.FindChild("fx_sc").localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}

		public void createShield()
		{
			GameObject original = Resources.Load<GameObject>("npc/npc_shield");
			this.Shield_obj = (UnityEngine.Object.Instantiate(original, new Vector3(-76.3f, 0.195f, 15.266f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
			Transform[] componentsInChildren = this.Shield_obj.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = EnumLayer.LM_FX;
			}
			this.ani = this.Shield_obj.transform.FindChild("model/model").GetComponent<Animator>();
			this.FX_open = this.Shield_obj.transform.FindChild("FX_npc_shield_open").gameObject;
		}

		public void disposeAvatar()
		{
			this.ani = null;
			this.FX_open = null;
			this.FX_open_Clon = null;
			bool flag = this.Shield_obj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.Shield_obj);
			}
			bool flag2 = this.scene_Obj != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.scene_Obj);
			}
			bool flag3 = this.scene_Camera != null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.scene_Camera);
			}
		}

		private bool OnMjCountOk(int needcount)
		{
			return needcount <= this.bagModel.getItemNumByTpid(1540u);
		}

		public bool CheckMjCount(int needcount)
		{
			return this.OnMjCountOk(needcount);
		}

		public void refbar(int oldCount)
		{
			this.oldCount = oldCount;
			this.newCount = this.HudunModel.NowCount;
			this.speed = (int)Math.Ceiling((double)(this.newCount - oldCount) / 20.0);
		}

		private void goUp()
		{
			bool flag = this.newCount > this.oldCount;
			if (flag)
			{
				this.oldCount += this.speed;
				this.bar.fillAmount = (float)this.oldCount / (float)this.HudunModel.GetMaxCount(this.HudunModel.Level);
			}
			else
			{
				bool flag2 = this.newCount <= this.oldCount;
				if (flag2)
				{
					this.bar.fillAmount = (float)this.newCount / (float)this.HudunModel.GetMaxCount(this.HudunModel.Level);
				}
			}
		}

		private void OnCLoseClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_HUDUN);
		}

		public void onHelp(GameObject go)
		{
			base.transform.FindChild("ig_bg1/tishi").gameObject.SetActive(true);
		}

		public void close_tishi(GameObject go)
		{
			base.transform.FindChild("ig_bg1/tishi").gameObject.SetActive(false);
		}
	}
}
