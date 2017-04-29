using Cross;
using DG.Tweening;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_role : Window
	{
		private GameObject m_SelfObj;

		private GameObject m_Self_Camera;

		private GameObject scene_Camera;

		private GameObject scene_Obj;

		public IMesh m_functionbar_scene;

		private ProfessionAvatar m_proAvatar;

		private RawImage roleAvatar;

		private RenderTexture roleTexture;

		private AsyncOperation async = null;

		private TabControl tabCtrl;

		private GameObject[] rolePanel;

		private ScrollControler scrollControler;

		private Dictionary<int, Text> attr_text = new Dictionary<int, Text>();

		private Dictionary<int, Image> icon_ani = new Dictionary<int, Image>();

		private bool isAdd = false;

		private bool isReduce = false;

		private float addTime = 0.5f;

		private float rateTime = 0f;

		private int addType;

		private int left_pt_num = 0;

		private Transform container;

		private List<Vector3> listPos = new List<Vector3>();

		public List<Transform> listPage = new List<Transform>();

		private List<GameObject> listIcon = new List<GameObject>();

		private bool isCanAdd = false;

		private Text isCanAddText;

		private int maxCount;

		private static int forceIndex;

		public static a3_role instan;

		public static a3_role isshow;

		private Dictionary<int, Button> btn_pt_add = new Dictionary<int, Button>();

		private Dictionary<int, Button> btn_pt_reduce = new Dictionary<int, Button>();

		private Dictionary<int, int> cur_att_pt = new Dictionary<int, int>();

		private Dictionary<int, int> true_att_pt = new Dictionary<int, int>();

		private Dictionary<int, int> base_att_pt = new Dictionary<int, int>();

		private BaseButton dye_use;

		private BaseButton dye_backinfo;

		private BaseButton btn_dye;

		private Dictionary<uint, GameObject> dyes = new Dictionary<uint, GameObject>();

		private SXML dedey = null;

		private uint seleltColorId;

		private int index = 0;

		public Action OnRunStateChange = null;

		private bool canRun = true;

		private Dictionary<int, GameObject> equipicon = new Dictionary<int, GameObject>();

		private Vector3 upVec = default(Vector3);

		public static int ForceIndex
		{
			get
			{
				int result = a3_role.forceIndex;
				a3_role.forceIndex = -1;
				return result;
			}
			set
			{
				a3_role.forceIndex = value;
				a3_role expr_0C = a3_role.instan;
				if (expr_0C != null)
				{
					expr_0C.tabCtrl.setSelectedIndex(a3_role.forceIndex, false);
				}
			}
		}

		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				bool flag = this.index == value;
				if (!flag)
				{
					this.index = value % this.maxCount;
					this.SetSibling();
				}
			}
		}

		public bool CanRun
		{
			get
			{
				return this.canRun;
			}
			set
			{
				this.canRun = value;
				bool flag = this.OnRunStateChange != null;
				if (flag)
				{
					this.OnRunStateChange();
				}
			}
		}

		private void SetSibling()
		{
			this.listPage[this.Index].SetAsLastSibling();
		}

		public override void init()
		{
			a3_role.instan = this;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onclose);
			this.scrollControler = new ScrollControler();
			ScrollRect component = base.transform.FindChild("playerInfo/contents/panel_attr/attr_scroll/scroll").GetComponent<ScrollRect>();
			this.scrollControler.create(component, 4);
			this.rolePanel = new GameObject[2];
			this.rolePanel[0] = base.transform.FindChild("playerInfo/contents/panel_attr").gameObject;
			this.rolePanel[1] = base.transform.FindChild("playerInfo/contents/panel_add").gameObject;
			this.roleAvatar = base.transform.FindChild("avatar/RawImage").GetComponent<RawImage>();
			this.isCanAddText = this.rolePanel[1].transform.FindChild("btn_add_do/Text").GetComponent<Text>();
			BaseButton baseButton2 = new BaseButton(this.rolePanel[1].transform.FindChild("btn_add_do"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onbtnAdd);
			BaseButton baseButton3 = new BaseButton(this.rolePanel[1].transform.FindChild("btn_clear"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onbtnClear);
			BaseButton baseButton4 = new BaseButton(this.rolePanel[1].transform.FindChild("tishi/can/yes"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.on_yesClear);
			BaseButton baseButton5 = new BaseButton(this.rolePanel[1].transform.FindChild("tishi/can/no"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.on_noClear);
			BaseButton baseButton6 = new BaseButton(this.rolePanel[1].transform.FindChild("tishi/no/yes"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onClealback);
			BaseButton baseButton7 = new BaseButton(base.transform.FindChild("btn_bag"), 1, 1);
			baseButton7.onClick = new Action<GameObject>(this.onbtnBag);
			BaseButton baseButton8 = new BaseButton(base.transform.FindChild("dye/up"), 1, 1);
			baseButton8.onClick = new Action<GameObject>(this.doUp);
			BaseButton baseButton9 = new BaseButton(base.transform.FindChild("dye/down"), 1, 1);
			baseButton9.onClick = new Action<GameObject>(this.doDown);
			base.getEventTrigerByPath("dye").onDrag = new EventTriggerListener.VectorDelegate(this.onDragIcon);
			this.tabCtrl = new TabControl();
			this.tabCtrl.onClickHanle = new Action<TabControl>(this.onTab);
			this.tabCtrl.create(base.getGameObjectByPath("playerInfo/panelTab"), base.gameObject, 0, 0, false);
			bool flag = a3_role.forceIndex != -1;
			if (flag)
			{
				this.tabCtrl.setSelectedIndex(a3_role.ForceIndex, false);
			}
			bool flag2 = Baselayer.cemaraRectTran == null;
			if (flag2)
			{
				Baselayer.cemaraRectTran = GameObject.Find("canvas").GetComponent<RectTransform>();
			}
			RectTransform cemaraRectTran = Baselayer.cemaraRectTran;
			RectTransform component2 = base.transform.FindChild("ig_bg1").GetComponent<RectTransform>();
			RectTransform component3 = base.GetComponent<RectTransform>();
			component2.sizeDelta = new Vector2(cemaraRectTran.rect.width, cemaraRectTran.rect.height);
			component3.sizeDelta = new Vector2(cemaraRectTran.rect.width, cemaraRectTran.rect.height);
			base.getEventTrigerByPath("avatar/avatar_touch").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			string path = "";
			switch (ModelBase<PlayerModel>.getInstance().profession)
			{
			case 2:
				path = "icon/job_icon/h2";
				break;
			case 3:
				path = "icon/job_icon/h3";
				break;
			case 4:
				path = "icon/job_icon/h4";
				break;
			case 5:
				path = "icon/job_icon/h5";
				break;
			}
			Image component4 = base.transform.FindChild("playerInfo/contents/panel_attr/hero_ig/ig").GetComponent<Image>();
			component4.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			this.btn_dye = new BaseButton(base.transform.FindChild("btn_dye"), 1, 1);
			this.btn_dye.onClick = delegate(GameObject go)
			{
				go.transform.FindChild("isthis").gameObject.SetActive(true);
				this.dye_backinfo.transform.FindChild("isthis").gameObject.SetActive(false);
				this.showDye();
			};
			this.dye_backinfo = new BaseButton(base.transform.FindChild("backinfo"), 1, 1);
			this.dye_backinfo.onClick = delegate(GameObject go)
			{
				go.transform.FindChild("isthis").gameObject.SetActive(true);
				this.btn_dye.transform.FindChild("isthis").gameObject.SetActive(false);
				this.hideDye();
			};
			for (int i = 1; i <= 10; i++)
			{
				this.icon_ani[i] = base.transform.FindChild("ig_bg1/ain" + i).GetComponent<Image>();
			}
			this.initAttr();
			this.initPointAttr();
			this.initDye();
		}

		public override void onShowed()
		{
			a3_role.isshow = this;
			this.refreshAttr();
			this.refreshAttPoint();
			ModelBase<PlayerModel>.getInstance().addEventListener(PlayerModel.ON_ATTR_CHANGE, new Action<GameEvent>(this.onAttrChange));
			BaseProxy<PlayerInfoProxy>.getInstance().addEventListener(PlayerInfoProxy.EVENT_ADD_POINT, new Action<GameEvent>(this.onAddPt));
			BaseProxy<A3_WingProxy>.getInstance().addEventListener(3u, new Action<GameEvent>(this.OnShowStageChange));
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_USE_DYE, new Action<GameEvent>(this.refreshDye));
			this.onAttrChange(null);
			bool flag = this.uiData != null;
			if (flag)
			{
				int num = (int)this.uiData[0];
				this.tabCtrl.setSelectedIndex(num, true);
			}
			this.initEquipIcon();
			this.dye_backinfo.transform.FindChild("isthis").gameObject.SetActive(true);
			this.btn_dye.transform.FindChild("isthis").gameObject.SetActive(false);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			base.transform.FindChild("ig_bg_bg").gameObject.SetActive(false);
			GRMap.GAME_CAMERA.SetActive(false);
			this.createAvatar();
			this.refreshEquip();
			this.setAni();
			this.SetAni_Color();
		}

		public override void onClosed()
		{
			a3_role.isshow = null;
			this.disposeAvatar();
			ModelBase<PlayerModel>.getInstance().removeEventListener(PlayerModel.ON_ATTR_CHANGE, new Action<GameEvent>(this.onAttrChange));
			BaseProxy<PlayerInfoProxy>.getInstance().removeEventListener(PlayerInfoProxy.EVENT_ADD_POINT, new Action<GameEvent>(this.onAddPt));
			BaseProxy<A3_WingProxy>.getInstance().removeEventListener(3u, new Action<GameEvent>(this.OnShowStageChange));
			BaseProxy<A3_WingProxy>.getInstance().removeEventListener(BagProxy.EVENT_USE_DYE, new Action<GameEvent>(this.refreshDye));
			this.hideDye();
			this.rolePanel[1].transform.FindChild("tishi").gameObject.SetActive(false);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			this.isAdd = false;
			this.isReduce = false;
			GRMap.GAME_CAMERA.SetActive(true);
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				this.m_SelfObj.transform.Rotate(Vector3.up, -delta.x);
			}
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
					this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
				}
				else
				{
					bool flag3 = SelfRole._inst is P3Mage;
					if (flag3)
					{
						original = Resources.Load<GameObject>("profession/avatar_ui/mage_avatar");
						this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 167f, 0f, 0f)) as GameObject);
					}
					else
					{
						bool flag4 = SelfRole._inst is P5Assassin;
						if (!flag4)
						{
							return;
						}
						original = Resources.Load<GameObject>("profession/avatar_ui/assa_avatar");
						this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
					}
				}
				Transform[] componentsInChildren = this.m_SelfObj.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					transform.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
				}
				Transform transform2 = this.m_SelfObj.transform.FindChild("model");
				bool flag5 = SelfRole._inst is P3Mage;
				if (flag5)
				{
					Transform parent = transform2.FindChild("R_Finger1");
					original = Resources.Load<GameObject>("profession/avatar_ui/mage_r_finger_fire");
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					gameObject.transform.SetParent(parent, false);
				}
				this.m_proAvatar = new ProfessionAvatar();
				this.m_proAvatar.Init(SelfRole._inst.m_strAvatarPath, "h_", EnumLayer.LM_ROLE_INVISIBLE, EnumMaterial.EMT_EQUIP_H, transform2, SelfRole._inst.m_strEquipEffPath);
				bool flag6 = ModelBase<a3_EquipModel>.getInstance().active_eqp.Count >= 10;
				if (flag6)
				{
					this.m_proAvatar.set_equip_eff(ModelBase<a3_EquipModel>.getInstance().GetEqpIdbyType(3), true);
				}
				this.m_proAvatar.set_body(SelfRole._inst.get_bodyid(), SelfRole._inst.get_bodyfxid());
				this.m_proAvatar.set_weaponl(SelfRole._inst.get_weaponl_id(), SelfRole._inst.get_weaponl_fxid());
				this.m_proAvatar.set_weaponr(SelfRole._inst.get_weaponr_id(), SelfRole._inst.get_weaponr_fxid());
				this.m_proAvatar.set_wing(SelfRole._inst.get_wingid(), SelfRole._inst.get_windfxid());
				this.m_proAvatar.set_equip_color(SelfRole._inst.get_equip_colorid());
				original = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera");
				this.scene_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
				original = Resources.Load<GameObject>("profession/avatar_ui/show_scene");
				this.scene_Obj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.49f, 15.1f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
				Transform[] componentsInChildren2 = this.scene_Obj.GetComponentsInChildren<Transform>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Transform transform3 = componentsInChildren2[j];
					bool flag7 = transform3.gameObject.name == "scene_ta";
					if (flag7)
					{
						transform3.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
					}
					else
					{
						transform3.gameObject.layer = EnumLayer.LM_FX;
					}
				}
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
			bool flag2 = this.scene_Obj != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.scene_Obj);
			}
			bool flag3 = this.m_Self_Camera != null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.m_Self_Camera);
			}
			bool flag4 = this.scene_Camera != null;
			if (flag4)
			{
				UnityEngine.Object.Destroy(this.scene_Camera);
			}
		}

		private void Update()
		{
			bool flag = this.isAdd || this.isReduce;
			if (flag)
			{
				this.addTime -= Time.deltaTime;
				bool flag2 = this.addTime < 0f;
				if (flag2)
				{
					this.rateTime += 0.05f;
					this.addTime = 0.5f - this.rateTime;
					bool flag3 = this.cur_att_pt.ContainsKey(this.addType);
					if (flag3)
					{
						bool flag4 = this.isAdd;
						if (flag4)
						{
							Dictionary<int, int> arg_97_0 = this.cur_att_pt;
							int num = this.addType;
							int num2 = arg_97_0[num];
							arg_97_0[num] = num2 + 1;
							this.left_pt_num--;
						}
						bool flag5 = this.isReduce;
						if (flag5)
						{
							Dictionary<int, int> arg_D7_0 = this.cur_att_pt;
							int num2 = this.addType;
							int num = arg_D7_0[num2];
							arg_D7_0[num2] = num - 1;
							this.left_pt_num++;
						}
						this.rolePanel[1].transform.FindChild("num").GetComponent<Text>().text = this.left_pt_num.ToString();
						this.rolePanel[1].transform.FindChild("btn_" + this.addType + "/value").GetComponent<Text>().text = this.cur_att_pt[this.addType].ToString();
						this.checkLeftPtNum();
					}
				}
			}
			bool flag6 = this.m_proAvatar != null;
			if (flag6)
			{
				this.m_proAvatar.FrameMove();
			}
		}

		private void checkLeftPtNum()
		{
			bool flag = this.left_pt_num <= 0;
			if (flag)
			{
				this.left_pt_num = 0;
				foreach (Button current in this.btn_pt_add.Values)
				{
					current.interactable = false;
					this.isAdd = false;
				}
			}
			else
			{
				foreach (Button current2 in this.btn_pt_add.Values)
				{
					current2.interactable = true;
				}
			}
			bool flag2 = this.left_pt_num < ModelBase<PlayerModel>.getInstance().pt_att;
			if (flag2)
			{
				foreach (int current3 in this.cur_att_pt.Keys)
				{
					bool flag3 = this.cur_att_pt[current3] > 0;
					if (flag3)
					{
						this.btn_pt_reduce[current3].interactable = true;
					}
					else
					{
						this.btn_pt_reduce[current3].interactable = false;
					}
				}
				this.isCanAdd = true;
				this.isCanAddText.text = "确认";
			}
			else
			{
				this.isReduce = false;
				foreach (int current4 in this.cur_att_pt.Keys)
				{
					this.btn_pt_reduce[current4].interactable = false;
				}
				this.isCanAdd = false;
				this.isCanAddText.text = "推荐加点";
			}
			bool flag4 = this.cur_att_pt.ContainsKey(this.addType);
			if (flag4)
			{
				bool flag5 = this.cur_att_pt[this.addType] <= 0;
				if (flag5)
				{
					this.isReduce = false;
				}
			}
		}

		private void refreshAttr()
		{
			base.transform.FindChild("playerInfo/contents/panel_attr/name").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().name;
			base.transform.FindChild("playerInfo/contents/panel_attr/lv").GetComponent<Text>().text = string.Concat(new object[]
			{
				"Lv",
				ModelBase<PlayerModel>.getInstance().lvl,
				"（",
				ModelBase<PlayerModel>.getInstance().up_lvl,
				"转）"
			});
			bool flag = ModelBase<A3_LegionModel>.getInstance().myLegion.clname == null;
			if (flag)
			{
				base.transform.FindChild("playerInfo/contents/panel_attr/team").gameObject.SetActive(false);
				base.transform.FindChild("playerInfo/contents/panel_attr/no_team").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("playerInfo/contents/panel_attr/team").gameObject.SetActive(true);
				base.transform.FindChild("playerInfo/contents/panel_attr/team/team_name").GetComponent<Text>().text = ModelBase<A3_LegionModel>.getInstance().myLegion.clname;
				base.transform.FindChild("playerInfo/contents/panel_attr/no_team").gameObject.SetActive(false);
			}
		}

		private void refreshAttPoint()
		{
			bool flag = ModelBase<PlayerModel>.getInstance().profession == 3;
			if (flag)
			{
				this.cur_att_pt[2] = 0;
				this.true_att_pt[2] = ModelBase<PlayerModel>.getInstance().pt_intept;
			}
			else
			{
				this.cur_att_pt[3] = 0;
				this.true_att_pt[3] = ModelBase<PlayerModel>.getInstance().pt_agipt;
			}
			this.cur_att_pt[1] = 0;
			this.cur_att_pt[4] = 0;
			this.cur_att_pt[5] = 0;
			this.true_att_pt[1] = ModelBase<PlayerModel>.getInstance().pt_strpt;
			this.true_att_pt[4] = ModelBase<PlayerModel>.getInstance().pt_conpt;
			this.true_att_pt[5] = ModelBase<PlayerModel>.getInstance().pt_wispt;
			foreach (int current in this.cur_att_pt.Keys)
			{
				this.rolePanel[1].transform.FindChild("btn_" + current + "/value").GetComponent<Text>().text = this.cur_att_pt[current].ToString();
				this.rolePanel[1].transform.FindChild("btn_" + current + "/value_all").GetComponent<Text>().text = (this.true_att_pt[current] + this.base_att_pt[current]).ToString();
			}
			this.left_pt_num = ModelBase<PlayerModel>.getInstance().pt_att;
			this.rolePanel[1].transform.FindChild("num").GetComponent<Text>().text = this.left_pt_num.ToString();
			this.checkLeftPtNum();
		}

		private void refreshAttPointAuto()
		{
			bool flag = ModelBase<PlayerModel>.getInstance().profession == 2;
			if (flag)
			{
				int[] addtype = new int[]
				{
					4,
					3,
					1,
					5
				};
				int[] tem = new int[]
				{
					3,
					3,
					2,
					2
				};
				this.addPointAuto(this.left_pt_num, addtype, tem);
			}
			bool flag2 = ModelBase<PlayerModel>.getInstance().profession == 3;
			if (flag2)
			{
				int[] addtype2 = new int[]
				{
					2,
					1,
					4,
					5
				};
				int[] tem2 = new int[]
				{
					3,
					2,
					3,
					2
				};
				this.addPointAuto(this.left_pt_num, addtype2, tem2);
			}
			bool flag3 = ModelBase<PlayerModel>.getInstance().profession == 5;
			if (flag3)
			{
				int[] addtype3 = new int[]
				{
					3,
					4,
					1,
					5
				};
				int[] tem3 = new int[]
				{
					3,
					3,
					2,
					2
				};
				this.addPointAuto(this.left_pt_num, addtype3, tem3);
			}
		}

		private void addPointAuto(int left_num, int[] addtype, int[] tem)
		{
			int[] array = new int[4];
			int num = 0;
			for (int i = 0; i < tem.Length; i++)
			{
				num += tem[i];
			}
			int num2 = (int)Math.Floor((double)left_num / (double)num);
			int num3 = left_num % num;
			for (int j = 0; j < 4; j++)
			{
				array[j] = tem[j] * num2;
			}
			bool flag = num3 > 0;
			if (flag)
			{
				int num4 = num3;
				for (int k = 0; k < 4; k++)
				{
					bool flag2 = num4 >= tem[k];
					if (flag2)
					{
						array[k] += tem[k];
						num4 -= tem[k];
					}
					else
					{
						array[k] += num4;
						num4 = 0;
					}
					bool flag3 = num4 <= 0;
					if (flag3)
					{
						break;
					}
				}
			}
			for (int l = 0; l < 4; l++)
			{
				this.cur_att_pt[addtype[l]] = array[l];
			}
			foreach (int current in this.cur_att_pt.Keys)
			{
				this.rolePanel[1].transform.FindChild("btn_" + current + "/value").GetComponent<Text>().text = this.cur_att_pt[current].ToString();
			}
			this.left_pt_num = 0;
			this.rolePanel[1].transform.FindChild("num").GetComponent<Text>().text = this.left_pt_num.ToString();
			this.checkLeftPtNum();
		}

		private IEnumerator LoadScene()
		{
			this.async = Application.LoadLevelAsync("show_scene");
			yield return this.async;
			yield break;
		}

		private void onTab(TabControl t)
		{
			this.rolePanel[0].SetActive(false);
			this.rolePanel[1].SetActive(false);
			bool flag = t.getSeletedIndex() == 0;
			if (flag)
			{
				this.rolePanel[0].SetActive(true);
				this.refreshAttr();
			}
			else
			{
				this.rolePanel[1].SetActive(true);
			}
		}

		public void initEquipIcon()
		{
			for (int i = 1; i <= 10; i++)
			{
				GameObject gameObject = base.transform.FindChild("ig_bg1/txt" + i).gameObject;
				gameObject.GetComponent<Text>().enabled = true;
				bool flag = gameObject.transform.childCount > 0;
				if (flag)
				{
					UnityEngine.Object.Destroy(gameObject.transform.GetChild(0).gameObject);
				}
			}
			this.equipicon.Clear();
			Dictionary<int, a3_BagItemData> equipsByType = ModelBase<a3_EquipModel>.getInstance().getEquipsByType();
			foreach (int current in equipsByType.Keys)
			{
				a3_BagItemData data = equipsByType[current];
				this.CreateEquipIcon(data);
			}
		}

		private void CreateEquipIcon(a3_BagItemData data)
		{
			GameObject icon = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 1f, false);
			IconImageMgr.getInstance().refreshA3EquipIcon_byType(icon, data, EQUIP_SHOW_TYPE.SHOW_INTENSIFYANDSTAGE);
			GameObject gameObject = base.transform.FindChild("ig_bg1/txt" + data.confdata.equip_type).gameObject;
			icon.transform.SetParent(gameObject.transform, false);
			gameObject.GetComponent<Text>().enabled = false;
			icon.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			this.equipicon[data.confdata.equip_type] = icon;
			icon.transform.GetComponent<Image>().color = new Vector4(0f, 0f, 0f, 0f);
			BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.onEquipClick(icon, data.id);
			};
		}

		private void onEquipClick(GameObject go, uint id)
		{
			ArrayList arrayList = new ArrayList();
			a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquips()[id];
			arrayList.Add(a3_BagItemData);
			arrayList.Add(equip_tip_type.tip_ForLook);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
		}

		private void setAni()
		{
			foreach (int current in this.icon_ani.Keys)
			{
				bool flag = ModelBase<a3_EquipModel>.getInstance().active_eqp.ContainsKey(current);
				if (flag)
				{
					this.icon_ani[current].gameObject.SetActive(true);
				}
				else
				{
					this.icon_ani[current].gameObject.SetActive(false);
				}
			}
		}

		public void SetAni_Color()
		{
			foreach (int current in ModelBase<a3_EquipModel>.getInstance().getEquipsByType().Keys)
			{
				Color color = default(Color);
				switch (ModelBase<a3_EquipModel>.getInstance().getEquipsByType()[current].equipdata.attribute)
				{
				case 1:
					color = new Color(0f, 0.47f, 0f);
					break;
				case 2:
					color = new Color(0.68f, 0.26f, 0.03f);
					break;
				case 3:
					color = new Color(0.76f, 0.86f, 0.33f);
					break;
				case 4:
					color = new Color(0.97f, 0.11f, 0.87f);
					break;
				case 5:
					color = new Color(0.17f, 0.18f, 0.57f);
					break;
				}
				this.icon_ani[current].GetComponent<Image>().color = color;
			}
		}

		private void initAttr()
		{
			GameObject gameObject = base.transform.FindChild("playerInfo/contents/panel_attr/attr_scroll/scroll/item").gameObject;
			GameObject gameObject2 = base.transform.FindChild("playerInfo/contents/panel_attr/attr_scroll/scroll/contain").gameObject;
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl", "");
			string @string = sXML.GetNode("att_show", "").getString("att_type");
			string[] array = @string.Split(new char[]
			{
				','
			});
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				num++;
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject3.SetActive(true);
				Text component = gameObject3.transform.FindChild("name").GetComponent<Text>();
				Text component2 = gameObject3.transform.FindChild("value").GetComponent<Text>();
				int num2 = int.Parse(array[i]);
				component.text = Globle.getAttrNameById(num2) + "：";
				bool flag = num2 == 5;
				if (flag)
				{
					component.text = "攻击力：";
					component2.text = ModelBase<PlayerModel>.getInstance().attr_list[38u] + "-" + ModelBase<PlayerModel>.getInstance().attr_list[5u];
				}
				else
				{
					bool flag2 = num2 == 17 || num2 == 19 || num2 == 20 || num2 == 24 || num2 == 25 || num2 == 29 || num2 == 30 || num2 == 31 || num2 == 32 || num2 == 33 || num2 == 35 || num2 == 36 || num2 == 37 || num2 == 39 || num2 == 40 || num2 == 17 || num2 == 41;
					if (flag2)
					{
						component2.text = "+" + (float)ModelBase<PlayerModel>.getInstance().attr_list[(uint)num2] / 10f + "%";
					}
					else
					{
						component2.text = "+" + ModelBase<PlayerModel>.getInstance().attr_list[(uint)num2].ToString();
					}
				}
				bool flag3 = num % 2 != 0;
				if (flag3)
				{
					gameObject3.transform.FindChild("ig_bg").gameObject.SetActive(false);
				}
				gameObject3.transform.SetParent(gameObject2.transform, false);
				this.attr_text[num2] = component2;
			}
			float y = gameObject2.transform.GetComponent<GridLayoutGroup>().cellSize.y;
			RectTransform component3 = gameObject2.GetComponent<RectTransform>();
			component3.sizeDelta = new Vector2(0f, (float)this.attr_text.Count * y);
		}

		private void initPointAttr()
		{
			for (int i = 1; i <= 5; i++)
			{
				GameObject gameObject = this.rolePanel[1].transform.FindChild("btn_" + i).gameObject;
				bool flag = ModelBase<PlayerModel>.getInstance().profession == 3 && i == 3;
				if (flag)
				{
					gameObject.SetActive(false);
				}
				else
				{
					bool flag2 = ModelBase<PlayerModel>.getInstance().profession != 3 && i == 2;
					if (flag2)
					{
						gameObject.SetActive(false);
					}
					else
					{
						Button component = gameObject.transform.FindChild("btn_add").GetComponent<Button>();
						Button component2 = gameObject.transform.FindChild("btn_reduce").GetComponent<Button>();
						int type = i;
						EventTriggerListener.Get(component.gameObject).onDown = delegate(GameObject go)
						{
							this.onClickAdd(go, type);
						};
						EventTriggerListener.Get(component.gameObject).onExit = delegate(GameObject go)
						{
							this.onClickAddExit(go, type);
						};
						EventTriggerListener.Get(component2.gameObject).onDown = delegate(GameObject go)
						{
							this.onClickReduce(go, type);
						};
						EventTriggerListener.Get(component2.gameObject).onExit = delegate(GameObject go)
						{
							this.onClickReduceExit(go, type);
						};
						this.btn_pt_add[type] = component;
						this.btn_pt_reduce[type] = component2;
					}
				}
			}
			SXML sXML = XMLMgr.instance.GetSXML("creat_character.character", "job_type==" + ModelBase<PlayerModel>.getInstance().profession);
			List<SXML> nodeList = sXML.GetNodeList("character", "");
			foreach (SXML current in nodeList)
			{
				int @int = current.getInt("att_type");
				switch (@int)
				{
				case 1:
					this.base_att_pt[1] = current.getInt("att_value");
					break;
				case 2:
					this.base_att_pt[3] = current.getInt("att_value");
					break;
				case 3:
					this.base_att_pt[4] = current.getInt("att_value");
					break;
				case 4:
					this.base_att_pt[2] = current.getInt("att_value");
					break;
				default:
					if (@int == 34)
					{
						this.base_att_pt[5] = current.getInt("att_value");
					}
					break;
				}
			}
		}

		private void onClickAdd(GameObject go, int type)
		{
			bool flag = !go.GetComponent<Button>().interactable;
			if (!flag)
			{
				this.isAdd = true;
				this.addType = type;
				this.rateTime = 0f;
				this.addTime = 0.5f;
				bool flag2 = this.cur_att_pt.ContainsKey(this.addType);
				if (flag2)
				{
					Dictionary<int, int> arg_62_0 = this.cur_att_pt;
					int key = this.addType;
					int num = arg_62_0[key];
					arg_62_0[key] = num + 1;
					this.rolePanel[1].transform.FindChild("btn_" + type + "/value").GetComponent<Text>().text = this.cur_att_pt[this.addType].ToString();
					this.left_pt_num--;
					this.rolePanel[1].transform.FindChild("num").GetComponent<Text>().text = this.left_pt_num.ToString();
					this.checkLeftPtNum();
				}
			}
		}

		private void onClickAddExit(GameObject go, int type)
		{
			this.isAdd = false;
		}

		private void onClickReduce(GameObject go, int type)
		{
			bool flag = !go.GetComponent<Button>().interactable;
			if (!flag)
			{
				this.isReduce = true;
				this.addType = type;
				this.rateTime = 0f;
				this.addTime = 0.5f;
				bool flag2 = this.cur_att_pt.ContainsKey(this.addType);
				if (flag2)
				{
					Dictionary<int, int> arg_62_0 = this.cur_att_pt;
					int key = this.addType;
					int num = arg_62_0[key];
					arg_62_0[key] = num - 1;
					this.rolePanel[1].transform.FindChild("btn_" + type + "/value").GetComponent<Text>().text = this.cur_att_pt[this.addType].ToString();
					this.left_pt_num++;
					this.rolePanel[1].transform.FindChild("num").GetComponent<Text>().text = this.left_pt_num.ToString();
					this.checkLeftPtNum();
				}
			}
		}

		private void onClickReduceExit(GameObject go, int type)
		{
			this.isReduce = false;
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ROLE);
		}

		private void onbtnAdd(GameObject go)
		{
			bool flag = this.isCanAdd;
			if (flag)
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				foreach (int current in this.cur_att_pt.Keys)
				{
					bool flag2 = this.cur_att_pt[current] > 0;
					if (flag2)
					{
						dictionary[current] = this.cur_att_pt[current];
					}
				}
				bool flag3 = dictionary.Count > 0;
				if (flag3)
				{
					BaseProxy<PlayerInfoProxy>.getInstance().sendAddPoint(0, dictionary);
				}
			}
			else
			{
				this.refreshAttPointAuto();
			}
		}

		private void onbtnClear(GameObject go)
		{
			this.rolePanel[1].transform.FindChild("tishi").gameObject.SetActive(true);
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl", "");
			string @string = sXML.GetNode("points_reset", "").getString("cost");
			bool flag = ModelBase<PlayerModel>.getInstance().up_lvl == 0u && ModelBase<PlayerModel>.getInstance().lvl <= 80u;
			if (flag)
			{
				this.rolePanel[1].transform.FindChild("tishi/text").GetComponent<Text>().text = "1转后开启次功能！";
				this.rolePanel[1].transform.FindChild("tishi/no").gameObject.SetActive(true);
				this.rolePanel[1].transform.FindChild("tishi/can").gameObject.SetActive(false);
			}
			else
			{
				this.rolePanel[1].transform.FindChild("tishi/text").GetComponent<Text>().text = "需要花费" + @string + "个钻石，是否继续？";
				this.rolePanel[1].transform.FindChild("tishi/no").gameObject.SetActive(false);
				this.rolePanel[1].transform.FindChild("tishi/can").gameObject.SetActive(true);
			}
		}

		private void on_yesClear(GameObject go)
		{
			BaseProxy<PlayerInfoProxy>.getInstance().sendAddPoint(1, null);
			this.rolePanel[1].transform.FindChild("tishi").gameObject.SetActive(false);
		}

		private void onClealback(GameObject go)
		{
			this.rolePanel[1].transform.FindChild("tishi").gameObject.SetActive(false);
		}

		private void on_noClear(GameObject go)
		{
			this.rolePanel[1].transform.FindChild("tishi").gameObject.SetActive(false);
		}

		private void onbtnBag(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ROLE);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_BAG, null, false);
		}

		private void onAttrChange(GameEvent e)
		{
			foreach (int current in this.attr_text.Keys)
			{
				bool flag = current == 5;
				if (flag)
				{
					this.attr_text[current].text = ModelBase<PlayerModel>.getInstance().attr_list[38u] + "-" + ModelBase<PlayerModel>.getInstance().attr_list[5u];
				}
				else
				{
					bool flag2 = current == 17 || current == 19 || current == 20 || current == 24 || current == 25 || current == 29 || current == 30 || current == 31 || current == 32 || current == 33 || current == 35 || current == 36 || current == 37 || current == 39 || current == 40 || current == 17 || current == 41;
					if (flag2)
					{
						this.attr_text[current].text = "+" + (float)ModelBase<PlayerModel>.getInstance().attr_list[(uint)current] / 10f + "%";
					}
					else
					{
						this.attr_text[current].text = "+" + ModelBase<PlayerModel>.getInstance().attr_list[(uint)current].ToString();
					}
				}
			}
			base.transform.FindChild("fighting/value").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().combpt.ToString();
			this.refresh_equip();
			this.refreshEquip();
		}

		private void onAddPt(GameEvent e)
		{
			this.refreshAttPoint();
		}

		private void initDye()
		{
			this.dye_use = new BaseButton(base.transform.FindChild("dye/use"), 1, 1);
			SXML sXML = XMLMgr.instance.GetSXML("item", "");
			this.dye_use.onClick = delegate(GameObject go)
			{
				int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.seleltColorId);
				bool flag5 = itemNumByTpid <= 0;
				if (flag5)
				{
					a3_BagItemData a3_BagItemData = default(a3_BagItemData);
					a3_BagItemData.confdata = ModelBase<a3_BagModel>.getInstance().getItemDataById(this.seleltColorId);
					ArrayList arrayList = new ArrayList();
					arrayList.Add(a3_BagItemData);
					arrayList.Add(a3_dyetip.eType.buy);
					arrayList.Add(new Action(delegate
					{
						bool flag12 = this.m_SelfObj != null;
						if (flag12)
						{
							this.m_SelfObj.SetActive(true);
						}
					}));
					this.m_SelfObj.SetActive(false);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_DYETIP, arrayList, false);
				}
				else
				{
					uint num2 = this.seleltColorId;
					bool flag6 = ModelBase<a3_BagModel>.getInstance().getItemDataById(this.seleltColorId).use_type == 17;
					if (flag6)
					{
						num2 = 0u;
					}
					bool flag7 = SelfRole._inst.get_equip_colorid() == num2;
					if (flag7)
					{
						bool flag8 = num2 != 0u && ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(3);
						if (flag8)
						{
							flytxt.instance.fly("使用失败，颜色相同，无须重复使用哦！", 0, default(Color), null);
						}
						else
						{
							bool flag9 = ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(3);
							if (flag9)
							{
								flytxt.instance.fly("使用失败，装备没有被过染色哦！", 0, default(Color), null);
							}
						}
					}
					else
					{
						a3_BagItemData a3_BagItemData2 = default(a3_BagItemData);
						foreach (a3_BagItemData current2 in ModelBase<a3_BagModel>.getInstance().getItems(false).Values)
						{
							bool flag10 = current2.tpid == this.seleltColorId;
							if (flag10)
							{
								a3_BagItemData2 = current2;
							}
						}
						bool flag11 = a3_BagItemData2.id != 0u && ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(3);
						if (flag11)
						{
							BaseProxy<BagProxy>.getInstance().sendUseItems(a3_BagItemData2.id, 1);
						}
					}
				}
			};
			new BaseButton(base.transform.FindChild("dye/dye_help"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("dye/dye_help/panel_help").gameObject.SetActive(true);
				this.m_SelfObj.SetActive(false);
			};
			new BaseButton(base.transform.FindChild("dye/dye_help/panel_help/closeBtn"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("dye/dye_help/panel_help").gameObject.SetActive(false);
				this.m_SelfObj.SetActive(true);
			};
			new BaseButton(base.transform.FindChild("dye/dye_help/panel_help/bg_0"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("dye/dye_help/panel_help").gameObject.SetActive(false);
				this.m_SelfObj.SetActive(true);
			};
			this.container = base.transform.FindChild("dye/Revolve/Panel");
			for (int i = 0; i < this.container.childCount; i++)
			{
				Transform child = this.container.GetChild(i);
				this.listPos.Add(child.localPosition);
				this.listPage.Add(child);
			}
			Transform parent = base.transform.FindChild("dye/Revolve/con");
			GameObject gameObject = base.transform.FindChild("dye/Revolve/0").gameObject;
			int num = 0;
			foreach (SXML current in sXML.GetNodeList("item", ""))
			{
				bool flag = current.getInt("use_type") == 16 || current.getInt("use_type") == 17;
				if (flag)
				{
					num++;
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(parent);
					a3_BagItemData data = default(a3_BagItemData);
					data.confdata.file = "icon/item/" + current.getString("icon_file");
					data.confdata.borderfile = "icon/itemborder/b039_0" + current.getInt("quality");
					GameObject gameObject3 = IconImageMgr.getInstance().createA3ItemIcon(data, false, -1, 1.5f, false);
					gameObject3.transform.SetParent(gameObject2.transform.FindChild("ico"));
					gameObject3.name = "dyeicon";
					gameObject3.GetComponent<Image>().enabled = false;
					gameObject3.transform.FindChild("iconborder").gameObject.SetActive(false);
					gameObject3.transform.FindChild("iconbor").gameObject.SetActive(false);
					gameObject2.transform.localScale = Vector3.one;
					gameObject3.transform.localPosition = Vector3.zero;
					List<SXML> temp = new List<SXML>();
					temp.Add(current);
					new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject sego)
					{
						bool flag5 = !ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(3);
						if (!flag5)
						{
							bool flag6 = temp[0].getInt("use_type") == 16 && ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(3);
							if (flag6)
							{
								this.m_proAvatar.set_equip_color(temp[0].getUint("id"));
							}
							else
							{
								this.m_proAvatar.set_equip_color(0u);
							}
							this.seleltColorId = temp[0].getUint("id");
							this.dye_use.interactable = true;
							foreach (GameObject current2 in this.dyes.Values)
							{
								Transform transform = current2.transform.FindChild("select");
								bool flag7 = current2 == sego;
								if (flag7)
								{
									transform.gameObject.SetActive(true);
								}
								else
								{
									transform.gameObject.SetActive(false);
								}
							}
						}
					};
					gameObject2.SetActive(true);
					this.dyes[current.getUint("id")] = gameObject2;
					this.listIcon.Add(gameObject2);
				}
			}
			bool flag2 = this.listIcon.Count <= this.listPage.Count;
			if (flag2)
			{
				for (int j = 0; j < this.listIcon.Count; j++)
				{
					this.listIcon[j].transform.SetParent(this.container);
					this.listIcon[j].transform.localPosition = this.listPage[j].localPosition;
				}
			}
			else
			{
				bool flag3 = this.listIcon.Count > this.listPage.Count;
				if (flag3)
				{
					for (int k = 0; k < this.listIcon.Count; k++)
					{
						this.listIcon[k].transform.SetParent(this.container);
						bool flag4 = k < this.listPage.Count;
						if (flag4)
						{
							this.listIcon[k].transform.localPosition = this.listPage[k].localPosition;
						}
						else
						{
							this.listIcon[k].transform.localPosition = new Vector3(500f, 0f, 0f);
						}
					}
				}
			}
		}

		public void refresh_equip()
		{
			Dictionary<uint, a3_BagItemData> equips = ModelBase<a3_EquipModel>.getInstance().getEquips();
			foreach (uint current in equips.Keys)
			{
				bool flag = !ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(equips[current].confdata);
				if (!flag)
				{
					bool flag2 = !ModelBase<a3_EquipModel>.getInstance().checkCanEquip(equips[current].confdata, equips[current].equipdata.stage, equips[current].equipdata.blessing_lv);
					if (flag2)
					{
						bool flag3 = this.equipicon.ContainsKey(equips[current].confdata.equip_type) && this.equipicon[equips[current].confdata.equip_type] != null;
						if (flag3)
						{
							this.equipicon[equips[current].confdata.equip_type].transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(true);
						}
					}
					else
					{
						bool flag4 = this.equipicon.ContainsKey(equips[current].confdata.equip_type) && this.equipicon[equips[current].confdata.equip_type] != null;
						if (flag4)
						{
							this.equipicon[equips[current].confdata.equip_type].transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(false);
						}
					}
				}
			}
		}

		public void refreshEquip()
		{
			Dictionary<uint, a3_BagItemData> equips = ModelBase<a3_EquipModel>.getInstance().getEquips();
			bool flag = false;
			foreach (uint current in equips.Keys)
			{
				bool flag2 = !ModelBase<a3_EquipModel>.getInstance().checkCanEquip(equips[current].confdata, equips[current].equipdata.stage, equips[current].equipdata.blessing_lv);
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			bool flag3 = flag;
			if (flag3)
			{
				base.transform.FindChild("equip_no").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("equip_no").gameObject.SetActive(false);
			}
		}

		private void showDye()
		{
			this.dye_use.interactable = false;
			base.transform.FindChild("playerInfo").gameObject.SetActive(false);
			base.transform.FindChild("dye").gameObject.SetActive(true);
			foreach (GameObject current in this.dyes.Values)
			{
				Transform transform = current.transform.FindChild("select");
				transform.gameObject.SetActive(false);
			}
			this.refreshDye(null);
		}

		private void hideDye()
		{
			base.transform.FindChild("dye").gameObject.SetActive(false);
			base.transform.FindChild("playerInfo").gameObject.SetActive(true);
		}

		private void doUp(GameObject go)
		{
			this.goUp();
		}

		private void doDown(GameObject go)
		{
			this.goDown();
		}

		private void goUp()
		{
			bool flag = !this.CanRun;
			if (!flag)
			{
				this.CanRun = false;
				Tween t = null;
				this.upVec = this.listIcon[this.listIcon.Count - 1].transform.localPosition;
				for (int i = 0; i < this.listIcon.Count; i++)
				{
					Vector3 localPosition = this.listIcon[i].transform.localPosition;
					t = this.listIcon[i].transform.DOLocalMove(this.upVec, 1f, false);
					bool flag2 = this.listIcon[i].transform.localPosition.y >= 280f || this.listIcon[i].transform.localPosition.x >= 100f;
					if (flag2)
					{
						this.listIcon[i].gameObject.SetActive(false);
					}
					else
					{
						this.listIcon[i].gameObject.SetActive(true);
					}
					this.upVec = localPosition;
				}
				t.OnComplete(delegate
				{
					this.OnComplete();
				});
			}
		}

		private void goDown()
		{
			bool flag = !this.CanRun;
			if (!flag)
			{
				this.CanRun = false;
				Tween t = null;
				this.upVec = this.listIcon[0].transform.localPosition;
				for (int i = this.listIcon.Count - 1; i >= 0; i--)
				{
					Vector3 localPosition = this.listIcon[i].transform.localPosition;
					t = this.listIcon[i].transform.DOLocalMove(this.upVec, 1f, false);
					bool flag2 = this.listIcon[i].transform.localPosition.y <= -280f || this.listIcon[i].transform.localPosition.x >= 100f;
					if (flag2)
					{
						this.listIcon[i].gameObject.SetActive(false);
					}
					else
					{
						this.listIcon[i].gameObject.SetActive(true);
					}
					this.upVec = localPosition;
				}
				t.OnComplete(delegate
				{
					this.OnComplete();
				});
			}
		}

		private void OnComplete()
		{
			this.CanRun = true;
		}

		private void onDragIcon(GameObject go, Vector2 delta)
		{
			float y = delta.y;
			bool flag = y > 0f;
			if (flag)
			{
				this.goUp();
			}
			else
			{
				bool flag2 = y < 0f;
				if (flag2)
				{
					this.goDown();
				}
			}
		}

		public void refreshDye(GameEvent e)
		{
			foreach (uint current in this.dyes.Keys)
			{
				int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(current);
				bool flag = itemNumByTpid >= 0;
				if (flag)
				{
					int count = this.dyes.Keys.Count;
					Transform transform = this.dyes[current].transform.FindChild("mun");
					transform.GetComponent<Text>().text = itemNumByTpid.ToString();
				}
			}
		}

		private void OnShowStageChange(GameEvent e)
		{
			this.m_proAvatar.set_wing(SelfRole._inst.get_wingid(), SelfRole._inst.get_windfxid());
		}
	}
}
