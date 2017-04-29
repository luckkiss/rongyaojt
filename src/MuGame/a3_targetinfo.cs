using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_targetinfo : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_targetinfo.<>c <>9 = new a3_targetinfo.<>c();

			public static Action<GameObject> <>9__8_0;

			internal void <init>b__8_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_TARGETINFO);
				bool flag = a3_ranking.isshow && a3_ranking.isshow.Toback;
				if (flag)
				{
					a3_ranking.isshow.Toback.SetActive(true);
					a3_ranking.isshow.Toback = null;
				}
				bool showFriend = ModelBase<PlayerModel>.getInstance().showFriend;
				if (showFriend)
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, null, false);
					ModelBase<PlayerModel>.getInstance().showFriend = false;
				}
			}
		}

		private GameObject m_Obj;

		private GameObject m_SelfObj;

		private GameObject scene_Camera;

		private GameObject scene_Obj;

		private ProfessionAvatar m_proAvatar;

		private Dictionary<int, Image> icon_ani = new Dictionary<int, Image>();

		public static a3_targetinfo instan;

		public static a3_targetinfo isshow;

		private new string name = "";

		private int carr = 0;

		private int lvl = 0;

		private int zhuan = 0;

		private int combpt = 0;

		private int wing_lvl = 0;

		private int wing_stage = 0;

		private int show_wing = 0;

		private int pet_type = 0;

		private string clan_name = "";

		private int jjclvl = 0;

		private int jjcfec = 0;

		private int summon_combpt = 0;

		private Dictionary<int, a3_BagItemData> Equips = new Dictionary<int, a3_BagItemData>();

		public Dictionary<int, a3_BagItemData> active_eqp = new Dictionary<int, a3_BagItemData>();

		private Dictionary<int, GameObject> equipicon = new Dictionary<int, GameObject>();

		public override void init()
		{
			a3_targetinfo.instan = this;
			BaseButton arg_38_0 = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			Action<GameObject> arg_38_1;
			if ((arg_38_1 = a3_targetinfo.<>c.<>9__8_0) == null)
			{
				arg_38_1 = (a3_targetinfo.<>c.<>9__8_0 = new Action<GameObject>(a3_targetinfo.<>c.<>9.<init>b__8_0));
			}
			arg_38_0.onClick = arg_38_1;
			for (int i = 1; i <= 10; i++)
			{
				this.icon_ani[i] = base.transform.FindChild("ig_bg1/ain" + i).GetComponent<Image>();
			}
			base.getEventTrigerByPath("avatar_touch").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
		}

		public override void onShowed()
		{
			a3_targetinfo.isshow = this;
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_LOOKFRIEND, new Action<GameEvent>(this.GetInfo));
			bool flag = this.uiData != null && this.uiData.Count > 0;
			uint cid;
			if (flag)
			{
				cid = (uint)this.uiData[0];
			}
			else
			{
				cid = SelfRole._inst.m_LockRole.m_unCID;
			}
			BaseProxy<FriendProxy>.getInstance().sendgetplayerinfo(cid);
			base.transform.SetAsLastSibling();
			this.create_scene();
			GRMap.GAME_CAMERA.SetActive(false);
			base.transform.FindChild("ig_bg_bg").gameObject.SetActive(false);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
		}

		public override void onClosed()
		{
			a3_targetinfo.isshow = null;
			BaseProxy<FriendProxy>.getInstance().removeEventListener(FriendProxy.EVENT_LOOKFRIEND, new Action<GameEvent>(this.GetInfo));
			this.disposeAvatar();
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
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
			itemFriendPrefab expr_BA = itemFriendPrefab.instance;
			bool flag2 = expr_BA != null && expr_BA.watch_avt;
			if (flag2)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(1);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, arrayList, false);
				itemFriendPrefab.instance.watch_avt = false;
			}
			itemNearbyListPrefab expr_106 = itemNearbyListPrefab.instance;
			bool flag3 = expr_106 != null && expr_106.watch_avt;
			if (flag3)
			{
				ArrayList arrayList2 = new ArrayList();
				arrayList2.Add(1);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, arrayList2, false);
				itemNearbyListPrefab.instance.watch_avt = false;
			}
		}

		private void GetInfo(GameEvent e)
		{
			Variant data = e.data;
			this.name = data["name"];
			this.carr = data["carr"];
			this.lvl = data["lvl"];
			this.zhuan = data["zhuan"];
			this.combpt = data["combpt"];
			this.wing_lvl = data["wing_lvl"];
			bool flag = data.ContainsKey("wing_stage");
			if (flag)
			{
				this.wing_stage = data["wing_stage"];
			}
			bool flag2 = data.ContainsKey("show_wing");
			if (flag2)
			{
				this.show_wing = data["show_wing"];
			}
			bool flag3 = data.ContainsKey("pet_type");
			if (flag3)
			{
				this.pet_type = data["pet_type"];
			}
			bool flag4 = data.ContainsKey("clan_name");
			if (flag4)
			{
				this.clan_name = data["clan_name"];
			}
			bool flag5 = data.ContainsKey("summon_combpt");
			if (flag5)
			{
				this.summon_combpt = data["summon_combpt"];
			}
			bool flag6 = data.ContainsKey("grade");
			if (flag6)
			{
				this.jjclvl = data["grade"];
			}
			bool flag7 = data.ContainsKey("score");
			if (flag7)
			{
				this.jjcfec = data["score"];
			}
			else
			{
				this.clan_name = "";
			}
			string path = "";
			switch (this.carr)
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
			Image component = base.transform.FindChild("playerInfo/panel_attr/hero_ig/ig").GetComponent<Image>();
			component.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			base.transform.FindChild("playerInfo/panel_attr/name").GetComponent<Text>().text = this.name;
			base.transform.FindChild("playerInfo/panel_attr/lv").GetComponent<Text>().text = string.Concat(new object[]
			{
				"Lv",
				this.lvl,
				"（",
				this.zhuan,
				"转）"
			});
			base.getTransformByPath("fighting/value").GetComponent<Text>().text = this.combpt.ToString();
			bool flag8 = this.jjclvl > 0;
			if (flag8)
			{
				SXML sXML = XMLMgr.instance.GetSXML("jjc.reward", "grade==" + this.jjclvl);
				string @string = sXML.getString("name");
				base.transform.FindChild("playerInfo/panel_attr/attr_scroll/scroll/contain/1/value").GetComponent<Text>().text = @string;
			}
			else
			{
				base.transform.FindChild("playerInfo/panel_attr/attr_scroll/scroll/contain/1/value").GetComponent<Text>().text = "暂无段位";
			}
			bool flag9 = this.jjclvl < 10;
			if (flag9)
			{
				base.transform.FindChild("playerInfo/panel_attr/attr_scroll/scroll/contain/1/icon").GetComponent<Image>().sprite = (Resources.Load("icon/rank/00" + this.jjclvl, typeof(Sprite)) as Sprite);
			}
			else
			{
				base.transform.FindChild("playerInfo/panel_attr/attr_scroll/scroll/contain/1/icon").GetComponent<Image>().sprite = (Resources.Load("icon/rank/0" + this.jjclvl, typeof(Sprite)) as Sprite);
			}
			base.transform.FindChild("playerInfo/panel_attr/attr_scroll/scroll/contain/2/value").GetComponent<Text>().text = this.summon_combpt.ToString();
			base.transform.FindChild("playerInfo/panel_attr/attr_scroll/scroll/contain/3/value").GetComponent<Text>().text = string.Concat(new object[]
			{
				this.wing_stage,
				"阶",
				this.wing_lvl,
				"级"
			});
			bool flag10 = data.ContainsKey("min_attack");
			if (flag10)
			{
				base.transform.FindChild("playerInfo/panel_attr/att/atk/value").GetComponent<Text>().text = data["min_attack"] + "-" + data["max_attack"];
				base.transform.FindChild("playerInfo/panel_attr/att/hp/value").GetComponent<Text>().text = data["max_hp"];
				base.transform.FindChild("playerInfo/panel_attr/att/phydef/value").GetComponent<Text>().text = data["physics_def"];
				base.transform.FindChild("playerInfo/panel_attr/att/manadef/value").GetComponent<Text>().text = data["magic_def"];
			}
			bool flag11 = this.clan_name == "";
			if (flag11)
			{
				base.transform.FindChild("playerInfo/panel_attr/team").gameObject.SetActive(false);
				base.transform.FindChild("playerInfo/panel_attr/no_team").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("playerInfo/panel_attr/team").gameObject.SetActive(true);
				base.transform.FindChild("playerInfo/panel_attr/team/team_name").GetComponent<Text>().text = this.clan_name;
				base.transform.FindChild("playerInfo/panel_attr/no_team").gameObject.SetActive(false);
			}
			Variant variant = data["equipments"];
			this.Equips.Clear();
			this.active_eqp.Clear();
			foreach (Variant current in variant._arr)
			{
				a3_BagItemData a3_BagItemData = default(a3_BagItemData);
				a3_BagItemData.confdata.equip_type = current["part_id"];
				Variant variant2 = current["eqpinfo"];
				a3_BagItemData.id = variant2["id"];
				a3_BagItemData.tpid = variant2["tpid"];
				a3_BagItemData.confdata = ModelBase<a3_BagModel>.getInstance().getItemDataById(a3_BagItemData.tpid);
				a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData, variant2);
				this.Equips[a3_BagItemData.confdata.equip_type] = a3_BagItemData;
			}
			foreach (a3_BagItemData current2 in this.Equips.Values)
			{
				bool flag12 = this.isactive_eqp(current2);
				if (flag12)
				{
					this.active_eqp[current2.confdata.equip_type] = current2;
				}
			}
			this.initEquipIcon();
			this.createAvatar();
			this.setAni();
			this.SetAni_Color();
		}

		public bool isactive_eqp(a3_BagItemData data)
		{
			bool flag = data.equipdata.attribute == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int key = ModelBase<a3_EquipModel>.getInstance().eqp_type_act[data.confdata.equip_type];
				bool flag2 = !this.Equips.ContainsKey(key);
				if (flag2)
				{
					result = false;
				}
				else
				{
					int num = ModelBase<a3_EquipModel>.getInstance().eqp_att_act[data.equipdata.attribute];
					bool flag3 = this.Equips[key].equipdata.attribute == num;
					result = flag3;
				}
			}
			return result;
		}

		public void initEquipIcon()
		{
			this.equipicon.Clear();
			Dictionary<int, a3_BagItemData> equips = this.Equips;
			foreach (int current in equips.Keys)
			{
				a3_BagItemData data = equips[current];
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
			icon.transform.FindChild("iconborder").gameObject.SetActive(false);
			this.equipicon[data.confdata.equip_type] = icon;
			icon.transform.GetComponent<Image>().color = new Vector4(0f, 0f, 0f, 0f);
			BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.onEquipClick(icon, data);
			};
		}

		private void setAni()
		{
			foreach (int current in this.icon_ani.Keys)
			{
				bool flag = this.active_eqp.ContainsKey(current);
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
			foreach (int current in this.Equips.Keys)
			{
				Color color = default(Color);
				switch (this.Equips[current].equipdata.attribute)
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

		private void onEquipClick(GameObject go, a3_BagItemData one)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(one);
			arrayList.Add(equip_tip_type.tip_ForLook);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
		}

		public void createAvatar()
		{
			bool flag = this.m_SelfObj == null;
			if (flag)
			{
				bool flag2 = this.carr == 2;
				string avatar_path;
				string equipEff_path;
				if (flag2)
				{
					GameObject original = Resources.Load<GameObject>("profession/avatar_ui/warrior_avatar");
					this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
					avatar_path = "profession/warrior/";
					equipEff_path = "Fx/armourFX/warrior/";
				}
				else
				{
					bool flag3 = this.carr == 3;
					if (flag3)
					{
						GameObject original = Resources.Load<GameObject>("profession/avatar_ui/mage_avatar");
						this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 167f, 0f, 0f)) as GameObject);
						avatar_path = "profession/mage/";
						equipEff_path = "Fx/armourFX/mage/";
					}
					else
					{
						bool flag4 = this.carr == 5;
						if (!flag4)
						{
							return;
						}
						GameObject original = Resources.Load<GameObject>("profession/avatar_ui/assa_avatar");
						this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
						avatar_path = "profession/assa/";
						equipEff_path = "Fx/armourFX/assa/";
					}
				}
				Transform[] componentsInChildren = this.m_SelfObj.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					transform.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
				}
				Transform transform2 = this.m_SelfObj.transform.FindChild("model");
				bool flag5 = this.carr == 3;
				if (flag5)
				{
					Transform parent = transform2.FindChild("R_Finger1");
					GameObject original = Resources.Load<GameObject>("profession/avatar_ui/mage_r_finger_fire");
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					gameObject.transform.SetParent(parent, false);
				}
				int id = 0;
				int fxlevel = 0;
				uint equip_color = 0u;
				bool flag6 = this.Equips.ContainsKey(3);
				if (flag6)
				{
					id = (int)this.Equips[3].tpid;
					fxlevel = this.Equips[3].equipdata.stage;
					equip_color = this.Equips[3].equipdata.color;
				}
				int id2 = 0;
				int fxlevel2 = 0;
				int id3 = 0;
				int fxlevel3 = 0;
				bool flag7 = this.Equips.ContainsKey(6);
				if (flag7)
				{
					switch (this.carr)
					{
					case 2:
						id3 = (int)this.Equips[6].tpid;
						fxlevel3 = this.Equips[6].equipdata.stage;
						break;
					case 3:
						id2 = (int)this.Equips[6].tpid;
						fxlevel2 = this.Equips[6].equipdata.stage;
						break;
					case 5:
						id2 = (int)this.Equips[6].tpid;
						fxlevel2 = this.Equips[6].equipdata.stage;
						id3 = (int)this.Equips[6].tpid;
						fxlevel3 = this.Equips[6].equipdata.stage;
						break;
					}
				}
				this.m_proAvatar = new ProfessionAvatar();
				this.m_proAvatar.Init(avatar_path, "h_", EnumLayer.LM_ROLE_INVISIBLE, EnumMaterial.EMT_EQUIP_H, transform2, equipEff_path);
				bool flag8 = this.active_eqp.Count >= 10;
				if (flag8)
				{
					this.m_proAvatar.set_equip_eff(id, true);
				}
				this.m_proAvatar.set_body(id, fxlevel);
				this.m_proAvatar.set_weaponl(id2, fxlevel2);
				this.m_proAvatar.set_weaponr(id3, fxlevel3);
				this.m_proAvatar.set_wing(this.show_wing, this.show_wing);
				this.m_proAvatar.set_equip_color(equip_color);
				bool flag9 = this.m_proAvatar != null;
				if (flag9)
				{
					this.m_proAvatar.FrameMove();
				}
			}
		}

		public void create_scene()
		{
			GameObject original = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera");
			this.scene_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
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
			bool flag3 = this.scene_Camera != null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.scene_Camera);
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

		public void setavt()
		{
			bool flag = this.m_Obj != null && !this.m_Obj.activeSelf;
			if (flag)
			{
				this.m_Obj.SetActive(true);
			}
		}

		private void SetStar(Transform starRoot, int num)
		{
			int num2 = 0;
			Transform[] componentsInChildren = starRoot.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent != null && transform.parent.parent == starRoot.transform;
				if (flag)
				{
					bool flag2 = num2 < num;
					if (flag2)
					{
						transform.gameObject.SetActive(true);
						num2++;
					}
					else
					{
						transform.gameObject.SetActive(false);
					}
				}
			}
		}
	}
}
