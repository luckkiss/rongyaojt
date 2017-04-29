using Cross;
using GameFramework;
using MuGame;
using MuGame.role;
using System;
using System.Collections;
using UnityEngine;

public class ProfessionRole : BaseRole
{
	public static GameObject ROLE_LVUP_FX;

	public int zhuan = 0;

	public int Pk_state;

	public bool m_bUserSelf = false;

	public bool m_invisiable = false;

	public int lvl = 0;

	public int combpt = 0;

	public string clanName;

	public int m_nKeepSkillCount = 0;

	public PlayerData playerDta;

	protected new PLAYER_AI_TYPE m_eThinkType = PLAYER_AI_TYPE.PAIT_IDLE;

	public string m_strAvatarPath;

	public string m_strEquipEffPath;

	private AnimationState m_Wing_Animstate;

	private float m_fWingTime = 0f;

	private int m_petID = -1;

	private int m_petStage = -1;

	private PetBird m_myPetBird = null;

	public int mapCount = 0;

	public int serial = 0;

	protected ProfessionAvatar m_proAvatar;

	public static bool ismyself = false;

	public RoleStateHelper m_rshelper;

	public Variant detailInfo;

	protected int m_testAvatar;

	protected int m_testPro;

	public bool can_buff_move;

	public bool can_buff_skill;

	public bool can_buff_ani;

	public bool moving;

	public float AttackCount
	{
		get
		{
			return this.m_fAttackCount;
		}
		set
		{
			this.m_fAttackCount = value;
			bool flag = this.m_fAttackCount <= 0f;
			if (flag)
			{
				this.m_rshelper.SetNavMeshInfo(20, 1E-05f, 1E-05f);
			}
		}
	}

	public int mapId
	{
		get
		{
			return GRMap.instance.m_nCurMapID;
		}
	}

	public REDNAME_TYPE NameType
	{
		get;
		set;
	}

	public bool invisible
	{
		get
		{
			return this.m_invisiable;
		}
		set
		{
			this.m_invisiable = value;
			bool isMain = this.m_isMain;
			if (isMain)
			{
				bool invisiable = this.m_invisiable;
				if (invisiable)
				{
					this.m_proAvatar.push_fx(1, false);
					this.m_bHide_state = true;
				}
				else
				{
					this.LeaveHide();
				}
			}
			else
			{
				bool flag = !this.m_invisiable;
				if (flag)
				{
					this.ShowAll();
				}
				else
				{
					this.HideAll();
				}
			}
		}
	}

	public bool isPlayingSkill
	{
		get
		{
			bool flag = !this.can_buff_skill;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this.m_curAni != null;
				result = (flag2 && this.m_curAni.GetInteger(EnumAni.ANI_I_SKILL) != 0 && this.m_fAttackCount > 0f);
			}
			return result;
		}
	}

	public bool isCanPlayingSkill
	{
		get
		{
			return !this.isPlayingSkill && (this.m_curAni.GetBool(EnumAni.ANI_RUN) || this.m_curAni);
		}
	}

	public bool canMove
	{
		get
		{
			return !BaseProxy<MapProxy>.getInstance().changingMap && this.can_buff_move;
		}
	}

	public void Init(string prefab_path, int layer, Vector3 pos, bool isUser = false)
	{
		base.Init(prefab_path, layer, pos, 0f, isUser);
		this.m_unLegionID = 2u;
		bool bUserSelf = this.m_bUserSelf;
		if (bUserSelf)
		{
			SelfHurtPoint selfHurtPoint = this.m_curPhy.gameObject.AddComponent<SelfHurtPoint>();
			selfHurtPoint.m_selfRole = this;
			ProfessionRole.ismyself = true;
		}
		else
		{
			OtherHurtPoint otherHurtPoint = this.m_curPhy.gameObject.AddComponent<OtherHurtPoint>();
			otherHurtPoint.m_otherRole = this;
			ProfessionRole.ismyself = false;
		}
		PlayerNameUIMgr.getInstance().show(this);
		base.curhp = (base.maxHp = 2000);
		this.m_proAvatar = new ProfessionAvatar();
		this.m_proAvatar.Init(this.m_strAvatarPath, "l_", this.m_curGameObj.layer, EnumMaterial.EMT_EQUIP_L, this.m_curModel, this.m_strEquipEffPath);
		bool flag = this.m_isMain && this.viewType != BaseRole.VIEW_TYPE_ALL;
		if (flag)
		{
			base.refreshViewType(BaseRole.VIEW_TYPE_ALL);
		}
		this.m_rshelper = new RoleStateHelper(this);
		bool isMain = this.m_isMain;
		if (isMain)
		{
			this.mapCount = (int)ModelBase<PlayerModel>.getInstance().treasure_num;
			this.refreshmapCount((int)ModelBase<PlayerModel>.getInstance().treasure_num);
			this.serial = ModelBase<PlayerModel>.getInstance().serial;
			this.refreshserialCount(ModelBase<PlayerModel>.getInstance().serial);
			this.refreshVipLvl((uint)ModelBase<A3_VipModel>.getInstance().Level);
		}
		bool flag2 = this.m_moveAgent != null;
		if (flag2)
		{
			this.m_rshelper.SetNavMeshInfo(20, 1E-05f, 1E-05f);
		}
	}

	public void ChangePetAvatar(int petID, int petStage)
	{
		bool flag = petID == this.m_petID && petStage == this.m_petStage;
		if (!flag)
		{
			this.m_petID = petID;
			this.m_petStage = petStage;
			bool flag2 = this.m_myPetBird != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.m_myPetBird.gameObject);
				this.m_myPetBird = null;
			}
			Transform transform = this.m_curModel.FindChild("birdstop");
			string petAvatar = ModelBase<A3_PetModel>.getInstance().GetPetAvatar(this.m_petID, 0);
			bool flag3 = petAvatar == "";
			if (!flag3)
			{
				GameObject gameObject = Resources.Load<GameObject>("profession/" + petAvatar);
				GameObject gameObject2 = Resources.Load<GameObject>("profession/birdpath");
				bool flag4 = gameObject == null || gameObject2 == null;
				if (!flag4)
				{
					GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject, transform.position, Quaternion.identity) as GameObject;
					GameObject gameObject4 = UnityEngine.Object.Instantiate(gameObject2, transform.position, Quaternion.identity) as GameObject;
					bool flag5 = gameObject3 == null || gameObject4 == null;
					if (!flag5)
					{
						gameObject4.transform.parent = transform;
						gameObject3.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
						this.m_myPetBird = gameObject3.AddComponent<PetBird>();
						this.m_myPetBird.Path = gameObject4;
					}
				}
			}
		}
	}

	protected override void onRefreshViewType()
	{
		bool flag = !this.m_isMain && this.m_moveAgent != null && this.m_moveAgent.enabled;
		if (flag)
		{
			this.m_rshelper.SetNavMeshInfo(20, 1E-05f, 1E-05f);
		}
		bool flag2 = this.viewType == BaseRole.VIEW_TYPE_ALL;
		if (flag2)
		{
			this.set_weaponl(this.m_roleDta.m_Weapon_LID, this.m_roleDta.m_Weapon_LFXID);
			this.set_weaponr(this.m_roleDta.m_Weapon_RID, this.m_roleDta.m_Weapon_RFXID);
			this.set_wing(this.m_roleDta.m_WindID, this.m_roleDta.m_WingFXID);
			this.set_body(this.m_roleDta.m_BodyID, this.m_roleDta.m_BodyFXID);
			this.clear_eff();
			bool add_eff = this.m_roleDta.add_eff;
			if (add_eff)
			{
				this.set_equip_eff(this.m_roleDta.m_BodyID, false);
			}
			this.rebind_ani();
			this.set_equip_color(this.m_roleDta.m_EquipColorID);
			bool isDead = this.isDead;
			if (isDead)
			{
				this.onDead(true, null);
			}
		}
		else
		{
			bool flag3 = this.viewType == BaseRole.VIEW_TYPE_NAV;
			if (flag3)
			{
				this.m_proAvatar.set_weaponl(-1, 0);
				this.m_proAvatar.set_weaponr(-1, 0);
				this.m_proAvatar.set_wing(-1, 0);
				this.m_proAvatar.set_body(-1, 0);
				this.m_proAvatar.set_equip_color(this.m_roleDta.m_EquipColorID);
			}
		}
	}

	public override void refreshViewData(Variant v)
	{
		bool flag = !this.m_isMain && this.m_moveAgent != null && this.m_moveAgent.enabled;
		if (flag)
		{
			this.m_moveAgent.avoidancePriority = 48;
		}
		this.detailInfo = v;
		int num = v["carr"];
		bool flag2 = v.ContainsKey("activate_count");
		if (flag2)
		{
			this.m_roleDta.add_eff = false;
			bool flag3 = v["activate_count"] >= 10;
			if (flag3)
			{
				this.m_roleDta.add_eff = true;
			}
		}
		bool flag4 = v.ContainsKey("show_eqp");
		if (flag4)
		{
			this.m_roleDta.m_BodyID = 0;
			this.m_roleDta.m_BodyFXID = 0;
			this.m_roleDta.m_EquipColorID = 0u;
			this.m_roleDta.m_Weapon_LID = 0;
			this.m_roleDta.m_Weapon_LFXID = 0;
			this.m_roleDta.m_Weapon_RID = 0;
			this.m_roleDta.m_Weapon_RFXID = 0;
			foreach (Variant current in v["show_eqp"]._arr)
			{
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(current["tpid"]);
				bool flag5 = itemDataById.equip_type == 3;
				if (flag5)
				{
					int tpid = (int)itemDataById.tpid;
					int bodyFXID = current["stage"];
					this.m_roleDta.m_BodyID = tpid;
					this.m_roleDta.m_BodyFXID = bodyFXID;
					uint equipColorID = 0u;
					bool flag6 = current.ContainsKey("colour");
					if (flag6)
					{
						equipColorID = current["colour"];
					}
					this.m_roleDta.m_EquipColorID = equipColorID;
				}
				bool flag7 = itemDataById.equip_type == 6;
				if (flag7)
				{
					int tpid2 = (int)itemDataById.tpid;
					int num2 = current["stage"];
					switch (num)
					{
					case 2:
						this.m_roleDta.m_Weapon_RID = tpid2;
						this.m_roleDta.m_Weapon_RFXID = num2;
						break;
					case 3:
						this.m_roleDta.m_Weapon_LID = tpid2;
						this.m_roleDta.m_Weapon_LFXID = num2;
						break;
					case 5:
						this.m_roleDta.m_Weapon_LID = tpid2;
						this.m_roleDta.m_Weapon_LFXID = num2;
						this.m_roleDta.m_Weapon_RID = tpid2;
						this.m_roleDta.m_Weapon_RFXID = num2;
						break;
					}
				}
			}
		}
		bool flag8 = v.ContainsKey("wing");
		if (flag8)
		{
			this.m_roleDta.m_WindID = v["wing"]["show_stage"];
		}
		bool flag9 = v.ContainsKey("ach_title");
		if (flag9)
		{
			base.title_id = v["ach_title"];
			base.isactive = v["title_display"]._bool;
			PlayerNameUIMgr.getInstance().refreshTitlelv(this, base.title_id);
		}
		bool flag10 = v.ContainsKey("serial_kp");
		if (flag10)
		{
			PlayerNameUIMgr.getInstance().refreserialCount(this, v["serial_kp"]);
		}
		bool flag11 = v.ContainsKey("rednm");
		if (flag11)
		{
			base.rednm = v["rednm"];
			PlayerNameUIMgr.getInstance().refreshNameColor(this, base.rednm);
			bool flag12 = SelfRole._inst != null && SelfRole._inst.m_LockRole != null && SelfRole._inst.m_LockRole.m_unIID == v["iid"];
			if (flag12)
			{
				PkmodelAdmin.RefreshShow(SelfRole._inst.m_LockRole, false, true);
			}
		}
		bool flag13 = v.ContainsKey("strike_back_tm");
		if (flag13)
		{
			base.hidbacktime = v["strike_back_tm"];
			PlayerNameUIMgr.getInstance().refresHitback(this, (int)(base.hidbacktime - (uint)NetClient.instance.CurServerTimeStamp), false);
		}
		bool flag14 = v.ContainsKey("lvl");
		if (flag14)
		{
			this.lvl = v["lvl"];
		}
		bool flag15 = v.ContainsKey("combpt");
		if (flag15)
		{
			this.combpt = v["combpt"];
		}
		bool flag16 = v.ContainsKey("clname");
		if (flag16)
		{
			this.clanName = v["clname"];
		}
		ArrayList arrayList = new ArrayList();
		arrayList.Add(this.m_unCID);
		arrayList.Add(this.combpt);
		bool flag17 = BaseProxy<FriendProxy>.getInstance() != null;
		if (flag17)
		{
			BaseProxy<FriendProxy>.getInstance().reFreshProfessionInfo(arrayList);
		}
		bool flag18 = OtherPlayerMgr._inst.VIEW_PLAYER_TYPE == 1 || this.m_isMain;
		if (flag18)
		{
			base.refreshViewType(BaseRole.VIEW_TYPE_ALL);
		}
	}

	public void refreshViewData1(Variant v)
	{
		int num = v["carr"];
		bool flag = v.ContainsKey("eqp");
		if (flag)
		{
			this.m_roleDta.m_BodyID = 0;
			this.m_roleDta.m_BodyFXID = 0;
			this.m_roleDta.m_EquipColorID = 0u;
			this.m_roleDta.m_Weapon_LID = 0;
			this.m_roleDta.m_Weapon_LFXID = 0;
			this.m_roleDta.m_Weapon_RID = 0;
			this.m_roleDta.m_Weapon_RFXID = 0;
			foreach (Variant current in v["eqp"]._arr)
			{
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(current["tpid"]);
				bool flag2 = itemDataById.equip_type == 3;
				if (flag2)
				{
					int tpid = (int)itemDataById.tpid;
					int bodyFXID = current["intensify"];
					this.m_roleDta.m_BodyID = tpid;
					this.m_roleDta.m_BodyFXID = bodyFXID;
					uint equipColorID = 0u;
					bool flag3 = current.ContainsKey("colour");
					if (flag3)
					{
						equipColorID = current["colour"];
					}
					this.m_roleDta.m_EquipColorID = equipColorID;
				}
				bool flag4 = itemDataById.equip_type == 6;
				if (flag4)
				{
					int tpid2 = (int)itemDataById.tpid;
					int num2 = current["intensify"];
					switch (num)
					{
					case 2:
						this.m_roleDta.m_Weapon_RID = tpid2;
						this.m_roleDta.m_Weapon_RFXID = num2;
						break;
					case 3:
						this.m_roleDta.m_Weapon_LID = tpid2;
						this.m_roleDta.m_Weapon_LFXID = num2;
						break;
					case 5:
						this.m_roleDta.m_Weapon_LID = tpid2;
						this.m_roleDta.m_Weapon_LFXID = num2;
						this.m_roleDta.m_Weapon_RID = tpid2;
						this.m_roleDta.m_Weapon_RFXID = num2;
						break;
					}
				}
			}
		}
		bool flag5 = v.ContainsKey("wing");
		if (flag5)
		{
			this.m_roleDta.m_WindID = v["wing"];
		}
		bool flag6 = v.ContainsKey("ach_title");
		if (flag6)
		{
			base.title_id = v["ach_title"];
			base.isactive = v["title_display"]._bool;
			PlayerNameUIMgr.getInstance().refreshTitlelv(this, base.title_id);
		}
		bool flag7 = v.ContainsKey("lvl");
		if (flag7)
		{
			this.lvl = v["lvl"];
		}
		bool flag8 = v.ContainsKey("combpt");
		if (flag8)
		{
			this.combpt = v["combpt"];
		}
		bool flag9 = v.ContainsKey("clname");
		if (flag9)
		{
			this.clanName = v["clname"];
		}
		ArrayList arrayList = new ArrayList();
		arrayList.Add(this.m_unCID);
		arrayList.Add(this.combpt);
		bool flag10 = BaseProxy<FriendProxy>.getInstance() != null;
		if (flag10)
		{
			BaseProxy<FriendProxy>.getInstance().reFreshProfessionInfo(arrayList);
		}
		bool flag11 = OtherPlayerMgr._inst.VIEW_PLAYER_TYPE == 1 || this.m_isMain;
		if (flag11)
		{
			base.refreshViewType(BaseRole.VIEW_TYPE_ALL);
		}
	}

	public new void dispose()
	{
		base.dispose();
		UnityEngine.Object.Destroy(this.m_curGameObj);
		bool flag = this.m_myPetBird != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.m_myPetBird.gameObject);
		}
	}

	public int get_weaponl_id()
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		int weapon_LID;
		if (flag)
		{
			weapon_LID = this.m_roleDta.m_Weapon_LID;
		}
		else
		{
			weapon_LID = this.m_proAvatar.m_Weapon_LID;
		}
		return weapon_LID;
	}

	public int get_weaponl_fxid()
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		int result;
		if (flag)
		{
			result = this.m_roleDta.m_Weapon_LFXID;
		}
		else
		{
			result = this.m_proAvatar.m_Weapon_LFXLV;
		}
		return result;
	}

	public void set_weaponl(int id, int fxlevel)
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		if (flag)
		{
			this.m_roleDta.m_Weapon_LID = id;
			this.m_roleDta.m_Weapon_LFXID = fxlevel;
		}
		else
		{
			this.m_proAvatar.set_weaponl(id, fxlevel);
			bool flag2 = this.invisible && this.m_isMain;
			if (flag2)
			{
				this.m_proAvatar.push_fx(1, true);
			}
		}
	}

	public int get_weaponr_id()
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		int weapon_RID;
		if (flag)
		{
			weapon_RID = this.m_roleDta.m_Weapon_RID;
		}
		else
		{
			weapon_RID = this.m_proAvatar.m_Weapon_RID;
		}
		return weapon_RID;
	}

	public int get_weaponr_fxid()
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		int result;
		if (flag)
		{
			result = this.m_roleDta.m_Weapon_RFXID;
		}
		else
		{
			result = this.m_proAvatar.m_Weapon_RFXLV;
		}
		return result;
	}

	public void set_weaponr(int id, int fxlevel)
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		if (flag)
		{
			this.m_roleDta.m_Weapon_RID = id;
			this.m_roleDta.m_Weapon_RFXID = fxlevel;
		}
		else
		{
			this.m_proAvatar.set_weaponr(id, fxlevel);
			bool flag2 = this.invisible && this.m_isMain;
			if (flag2)
			{
				this.m_proAvatar.push_fx(1, true);
			}
		}
	}

	public uint get_equip_colorid()
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		uint equipColorID;
		if (flag)
		{
			equipColorID = this.m_roleDta.m_EquipColorID;
		}
		else
		{
			equipColorID = this.m_proAvatar.m_EquipColorID;
		}
		return equipColorID;
	}

	public int get_wingid()
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		int windID;
		if (flag)
		{
			windID = this.m_roleDta.m_WindID;
		}
		else
		{
			windID = this.m_proAvatar.m_WindID;
		}
		return windID;
	}

	public int get_windfxid()
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		int result;
		if (flag)
		{
			result = this.m_roleDta.m_WingFXID;
		}
		else
		{
			result = this.m_proAvatar.m_Wing_FXLV;
		}
		return result;
	}

	public void set_wing(int id, int fxlevel)
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		if (flag)
		{
			this.m_roleDta.m_WindID = id;
			this.m_roleDta.m_WingFXID = fxlevel;
		}
		else
		{
			this.m_proAvatar.set_wing(id, fxlevel);
			bool flag2 = this.m_proAvatar.m_WindID > 0 && this.m_proAvatar.m_WingObj != null;
			if (flag2)
			{
				Animation component = this.m_proAvatar.m_WingObj.GetComponent<Animation>();
				bool flag3 = component != null;
				if (flag3)
				{
					this.m_Wing_Animstate = component["wg"];
				}
				this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 1f);
			}
			else
			{
				this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 0f);
			}
			bool flag4 = this.invisible && this.m_isMain;
			if (flag4)
			{
				this.m_proAvatar.push_fx(1, true);
			}
		}
	}

	public int get_bodyid()
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		int bodyID;
		if (flag)
		{
			bodyID = this.m_roleDta.m_BodyID;
		}
		else
		{
			bodyID = this.m_proAvatar.m_BodyID;
		}
		return bodyID;
	}

	public int get_bodyfxid()
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		int result;
		if (flag)
		{
			result = this.m_roleDta.m_BodyFXID;
		}
		else
		{
			result = this.m_proAvatar.m_BodyFXLV;
		}
		return result;
	}

	public void set_body(int id, int fxlevel)
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		if (flag)
		{
			this.m_roleDta.m_BodyID = id;
			this.m_roleDta.m_BodyFXID = fxlevel;
		}
		else
		{
			this.m_proAvatar.set_body(id, fxlevel);
			bool flag2 = this.invisible && this.m_isMain;
			if (flag2)
			{
				this.m_proAvatar.push_fx(1, true);
			}
		}
	}

	public void clear_eff()
	{
		this.m_proAvatar.clear_eff();
	}

	public void set_equip_eff(int id, bool high)
	{
		this.m_proAvatar.set_equip_eff(id, high);
	}

	public void set_equip_color(uint id)
	{
		bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
		if (flag)
		{
			this.m_roleDta.m_EquipColorID = id;
		}
		else
		{
			this.m_proAvatar.set_equip_color(id);
		}
	}

	public void FlyWing(float time)
	{
		bool flag = this.m_Wing_Animstate != null;
		if (flag)
		{
			this.m_Wing_Animstate.speed = 4f;
			this.m_fWingTime = time;
		}
	}

	public int getShowSkillEff()
	{
		bool flag = SceneCamera.m_nSkillEff_Level == 3 && !this.m_isMain;
		int result;
		if (flag)
		{
			result = 3;
		}
		else
		{
			bool flag2 = SceneCamera.m_nSkillEff_Level == 2 && !this.m_isMain;
			if (flag2)
			{
				result = 2;
			}
			else
			{
				result = 1;
			}
		}
		return result;
	}

	public void rebind_ani()
	{
		this.m_curAni.Rebind();
		bool flag = this.m_roleDta.m_WindID > 0;
		if (flag)
		{
			this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 1f);
		}
		else
		{
			this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 0f);
		}
	}

	public HitData Link_PRBullet(uint skillid, float t, GameObject root, Transform linker)
	{
		HitData hitData = linker.gameObject.AddComponent<HitData>();
		hitData.m_hdRootObj = root;
		hitData.m_CastRole = this;
		hitData.m_vBornerPos = this.m_curModel.position;
		hitData.m_ePK_Type = this.m_ePK_Type;
		switch (this.m_ePK_Type)
		{
		case PK_TYPE.PK_PKALL:
			hitData.m_unPK_Param = this.m_unCID;
			break;
		case PK_TYPE.PK_TEAM:
			hitData.m_unPK_Param = this.m_unTeamID;
			break;
		case PK_TYPE.PK_LEGION:
			hitData.m_unPK_Param = this.m_unLegionID;
			break;
		}
		hitData.m_unSkillID = skillid;
		hitData.m_nDamage = 100;
		hitData.m_nHitType = 0;
		linker.gameObject.layer = EnumLayer.LM_BT_FIGHT;
		UnityEngine.Object.Destroy(root, t);
		bool flag = this.m_fDisposeTime < t;
		if (flag)
		{
			this.m_fDisposeTime = t;
		}
		return hitData;
	}

	public HitData Build_PRBullet(uint skillid, float t, GameObject original, Vector3 position, Quaternion rotation)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(original, position, rotation) as GameObject;
		gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
		HitData hitData = gameObject.gameObject.AddComponent<HitData>();
		hitData.m_hdRootObj = gameObject;
		hitData.m_CastRole = this;
		hitData.m_vBornerPos = this.m_curModel.position;
		hitData.m_ePK_Type = this.m_ePK_Type;
		switch (this.m_ePK_Type)
		{
		case PK_TYPE.PK_PKALL:
			hitData.m_unPK_Param = this.m_unCID;
			break;
		case PK_TYPE.PK_TEAM:
			hitData.m_unPK_Param = this.m_unTeamID;
			break;
		case PK_TYPE.PK_LEGION:
			hitData.m_unPK_Param = this.m_unLegionID;
			break;
		}
		hitData.m_unSkillID = skillid;
		hitData.m_nDamage = 100;
		hitData.m_nHitType = 0;
		gameObject.layer = EnumLayer.LM_BT_FIGHT;
		bool flag = t > 0f;
		if (flag)
		{
			UnityEngine.Object.Destroy(gameObject, t);
		}
		bool flag2 = this.m_fDisposeTime < t;
		if (flag2)
		{
			this.m_fDisposeTime = t;
		}
		return hitData;
	}

	public void LeaveHide()
	{
		bool bHide_state = this.m_bHide_state;
		if (bHide_state)
		{
			this.m_bHide_state = false;
			this.m_proAvatar.pop_fx();
			this.m_fHideTime = 0f;
		}
	}

	public void HideAll()
	{
		PlayerNameUIMgr.getInstance().hide(this);
		Transform[] componentsInChildren = this.m_curGameObj.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
		}
		bool flag = this.m_myPetBird;
		if (flag)
		{
			Transform[] componentsInChildren2 = this.m_myPetBird.gameObject.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform2 = componentsInChildren2[j];
				transform2.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
			}
		}
	}

	public void ShowAll()
	{
		PlayerNameUIMgr.getInstance().show(this);
		this.refreshmapCount(this.mapCount);
		this.refreshserialCount(this.serial);
		this.m_curModel.gameObject.layer = this.m_layer;
		Transform[] componentsInChildren = this.m_curGameObj.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = this.m_layer;
			bool flag = transform.name == "physics";
			if (flag)
			{
				bool isMain = this.m_isMain;
				if (isMain)
				{
					transform.gameObject.layer = EnumLayer.LM_BT_SELF;
				}
				else
				{
					transform.gameObject.layer = EnumLayer.LM_BT_FIGHT;
				}
			}
		}
		bool flag2 = this.m_myPetBird;
		if (flag2)
		{
			Transform[] componentsInChildren2 = this.m_myPetBird.gameObject.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform2 = componentsInChildren2[j];
				transform2.gameObject.layer = EnumLayer.LM_MONSTER;
			}
		}
	}

	public override void FrameMove(float delta_time)
	{
		base.FrameMove(delta_time);
		bool bHide_state = this.m_bHide_state;
		if (bHide_state)
		{
			bool s_bStandaloneScene = SelfRole.s_bStandaloneScene;
			if (s_bStandaloneScene)
			{
				this.m_fHideTime -= delta_time;
				bool flag = this.m_fHideTime <= 0f;
				if (flag)
				{
					this.LeaveHide();
				}
			}
		}
		bool flag2 = this.m_Wing_Animstate != null;
		if (flag2)
		{
			bool flag3 = this.m_fWingTime > 0f;
			if (flag3)
			{
				this.m_fWingTime -= delta_time;
				bool flag4 = this.m_fWingTime <= 0f;
				if (flag4)
				{
					this.m_Wing_Animstate.speed = 1f;
				}
			}
		}
		bool flag5 = this.AttackCount > 0f;
		if (flag5)
		{
			this.AttackCount -= delta_time;
			bool flag6 = this.AttackCount <= 0f;
			if (flag6)
			{
				bool flag7 = this.m_curSkillId == 2003;
				if (flag7)
				{
					this.m_moveAgent.speed = 0.125f;
				}
				this.m_curSkillId = 0;
				this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, 0);
				bool flag8 = this is P2Warrior;
				if (flag8)
				{
					this.m_curModel.gameObject.GetComponent<P2Warrior_Event>().SFX_2003_hide();
				}
				bool flag9 = this is P5Assassin;
				if (flag9)
				{
					this.m_curModel.gameObject.GetComponent<P5Assassin_Event>().SFX_5003_hide();
				}
			}
		}
		bool isPlayingSkill = this.isPlayingSkill;
		if (isPlayingSkill)
		{
			this.m_rshelper.SetNavMeshInfo(20, 0.5f, 2f);
		}
		else
		{
			this.m_rshelper.SetNavMeshInfo(20, 1E-05f, 1E-05f);
		}
	}

	public void PlayHurt()
	{
		this.m_curAni.SetTrigger(EnumAni.ANI_HURT_FRONT);
	}

	public void onDead(bool force = false, BaseRole frm = null)
	{
		bool flag = !this.isDead | force;
		if (flag)
		{
			this.isDead = true;
			bool flag2 = this.m_curAni;
			if (flag2)
			{
				this.m_curAni.enabled = true;
				this.m_curAni.SetBool(EnumAni.ANI_B_DIE, true);
			}
			bool flag3 = this.m_curPhy;
			if (flag3)
			{
				this.m_curPhy.gameObject.SetActive(false);
			}
			bool flag4 = this.m_moveAgent;
			if (flag4)
			{
				this.m_moveAgent.enabled = false;
				this.m_moveAgent.updateRotation = false;
			}
			bool bUserSelf = this.m_bUserSelf;
			if (bUserSelf)
			{
				bool flag5 = GRMap.curSvrConf.ContainsKey("revive");
				if (flag5)
				{
					bool flag6 = GRMap.curSvrConf["revive"] > 0;
					ArrayList arrayList = new ArrayList();
					arrayList.Add(frm);
					bool flag7 = flag6;
					if (flag7)
					{
						InterfaceMgr.getInstance().open(InterfaceMgr.A3_RELIVE, arrayList, false);
					}
				}
			}
		}
	}

	public void onRelive(int max_hp)
	{
		this.isDead = false;
		bool flag = this.m_curAni;
		if (flag)
		{
			this.m_curAni.enabled = true;
			this.m_curAni.SetBool(EnumAni.ANI_B_DIE, false);
		}
		bool flag2 = this.m_curPhy;
		if (flag2)
		{
			this.m_curPhy.gameObject.SetActive(true);
		}
		bool flag3 = this.m_moveAgent;
		if (flag3)
		{
			this.m_moveAgent.enabled = true;
			this.m_moveAgent.updateRotation = true;
			this.m_moveAgent.updatePosition = true;
		}
		base.maxHp = max_hp;
		base.curhp = base.maxHp;
		PlayerNameUIMgr.getInstance().refreshHp(this, base.curhp, base.maxHp);
		bool isMain = this.m_isMain;
		if (isMain)
		{
			ModelBase<PlayerModel>.getInstance().modHp(base.curhp);
		}
		bool flag4 = this.m_curModel != null;
		if (flag4)
		{
			GameObject original = Resources.Load<GameObject>("FX/comFX/fx_player_common/FX_com_fuhuo");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject.transform.SetParent(this.m_curModel, false);
			UnityEngine.Object.Destroy(gameObject, 4f);
		}
	}

	public override void PlaySkill(int id)
	{
	}

	public void PlayJump()
	{
	}

	public override void onServerHurt(int damage, int hp, bool dead, BaseRole frm = null, int isCrit = -1, bool miss = false, bool stagger = false)
	{
		bool isDead = this.isDead;
		if (!isDead)
		{
			base.curhp = hp;
			if (stagger)
			{
				this.PlayHurt();
			}
			if (dead)
			{
				this.onDead(false, frm);
			}
			bool flag = damage > 0;
			if (flag)
			{
				bool flag2 = !miss;
				if (flag2)
				{
					PlayerNameUIMgr.getInstance().refreshHp(this, base.curhp, base.maxHp);
				}
				bool flag3 = frm != null;
				if (flag3)
				{
					FightText.CurrentCauseRole = frm;
					bool flag4 = this == SelfRole._inst && frm != null;
					if (flag4)
					{
						FightText.play(FightText.ENEMY_TEXT, base.getHeadPos(), damage, false, -1);
					}
					else
					{
						bool flag5 = SelfRole._inst.m_unIID == frm.m_unIID;
						if (flag5)
						{
							switch (isCrit)
							{
							case -1:
								FightText.play(FightText.userText, base.getHeadPos(), damage, false, -1);
								break;
							case 0:
								break;
							case 1:
								FightText.play(FightText.userText, base.getHeadPos(), damage, false, -1);
								break;
							case 2:
								FightText.play(FightText.IMG_TEXT, base.getHeadPos(), damage, false, isCrit);
								break;
							case 3:
								FightText.play(FightText.userText, base.getHeadPos(), damage, false, -1);
								break;
							case 4:
								FightText.play(FightText.userText, base.getHeadPos(), damage, false, -1);
								break;
							case 5:
								FightText.play(FightText.userText, base.getHeadPos(), damage, false, -1);
								break;
							default:
								FightText.play(FightText.IMG_TEXT, base.getHeadPos(), damage, false, isCrit);
								break;
							}
						}
					}
				}
			}
			bool flag6 = frm != null;
			if (flag6)
			{
				bool flag7 = frm is ProfessionRole;
				if (flag7)
				{
					ProfessionRole professionRole = frm as ProfessionRole;
					bool invisible = professionRole.invisible;
					if (invisible)
					{
						professionRole.invisible = false;
					}
				}
			}
		}
	}

	public void ShowHurtFX(int id)
	{
		bool flag = id > 0 && id < 10;
		if (flag)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(SceneTFX.m_HFX_Prefabs[id], this.m_curModel.position, this.m_curModel.rotation) as GameObject;
			gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
			UnityEngine.Object.Destroy(gameObject, 2f);
		}
	}

	public void ShowLvUpFx()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ProfessionRole.ROLE_LVUP_FX);
		gameObject.transform.SetParent(this.m_curModel, false);
		UnityEngine.Object.Destroy(gameObject, 4f);
	}

	public void onHurt(HitData hd)
	{
		bool isDead = this.isDead;
		if (!isDead)
		{
			bool flag = hd.m_nDamage == 0;
			if (!flag)
			{
				base.curhp -= hd.m_nDamage;
				bool flag2 = base.curhp < 0;
				if (flag2)
				{
					base.curhp = 0;
				}
				bool flag3 = hd.m_nDamage >= 100;
				if (flag3)
				{
				}
				bool flag4 = base.curhp == 0;
				if (flag4)
				{
				}
				PlayerNameUIMgr.getInstance().refreshHp(this, base.curhp, base.maxHp);
				bool flag5 = this == SelfRole._inst || SelfRole._inst.m_unIID == hd.m_CastRole.m_unIID;
				if (flag5)
				{
					FightText.play(FightText.ENEMY_TEXT, this.lastHeadPos, hd.m_nDamage, false, -1);
				}
				bool autofighting = SelfRole.fsm.Autofighting;
				if (autofighting)
				{
					bool flag6 = hd.m_CastRole is ProfessionRole;
					if (flag6)
					{
						ProfessionRole professionRole = (ProfessionRole)hd.m_CastRole;
						switch (ModelBase<PlayerModel>.getInstance().pk_state)
						{
						case PK_TYPE.PK_PKALL:
							SelfRole.UnderPlayerAttack = true;
							SelfRole._inst.m_LockRole = hd.m_CastRole;
							SelfRole.LastAttackPlayer = professionRole;
							goto IL_1B1;
						case PK_TYPE.PK_TEAM:
						{
							bool flag7 = professionRole.m_unTeamID != ModelBase<PlayerModel>.getInstance().teamid || !ModelBase<PlayerModel>.getInstance().IsInATeam;
							if (flag7)
							{
								SelfRole.UnderPlayerAttack = true;
								SelfRole._inst.m_LockRole = hd.m_CastRole;
								SelfRole.LastAttackPlayer = professionRole;
							}
							else
							{
								SelfRole.LastAttackPlayer = null;
								SelfRole.UnderPlayerAttack = false;
							}
							goto IL_1B1;
						}
						}
						SelfRole.UnderPlayerAttack = false;
						SelfRole.LastAttackPlayer = null;
						IL_1B1:;
					}
				}
			}
		}
	}

	public virtual void StartMove(float joy_x, float joy_y)
	{
		bool flag = !this.canMove;
		if (!flag)
		{
			bool isDead = this.isDead;
			if (!isDead)
			{
				bool flag2 = this.m_fSkillShowTime > 0f;
				if (!flag2)
				{
					float x = SceneCamera.m_right.x * joy_x + SceneCamera.m_forward.x * joy_y;
					float z = SceneCamera.m_right.y * joy_x + SceneCamera.m_forward.y * joy_y;
					Vector3 forward = new Vector3(x, 0f, z);
					this.m_curModel.forward = forward;
					this.moving = true;
					this.m_curAni.SetBool(EnumAni.ANI_RUN, true);
				}
			}
		}
	}

	public void MoveToTest()
	{
		bool flag = !this.canMove;
		if (!flag)
		{
			float num = 169.6f + UnityEngine.Random.Range(-7f, 7f);
			float num2 = 98f + UnityEngine.Random.Range(-7f, 7f);
			Vector3 forward = new Vector3(num - this.m_curModel.position.x, 0f, num2 - this.m_curModel.position.z);
			this.m_curModel.forward = forward;
			this.m_curAni.SetBool(EnumAni.ANI_RUN, true);
		}
	}

	public void StopMove()
	{
		bool flag = this.disposed || this.m_curAni == null;
		if (!flag)
		{
			this.moving = false;
			this.m_curAni.SetBool(EnumAni.ANI_RUN, false);
		}
	}

	public void refreshtitle(int titile_id)
	{
		PlayerNameUIMgr.getInstance().refreshTitlelv(this, titile_id);
	}

	public void refreshmapCount(int count)
	{
		PlayerNameUIMgr.getInstance().refreshmapCount(this, count, this.m_isMain);
	}

	public void refreshserialCount(int count)
	{
		PlayerNameUIMgr.getInstance().refreserialCount(this, count);
	}

	public void refreshVipLvl(uint lvl)
	{
		PlayerNameUIMgr.getInstance().refreshVipLv(this, lvl);
	}

	public void refreshnamecolor(int rednm)
	{
		PlayerNameUIMgr.getInstance().refreshNameColor(this, rednm);
	}

	public ProfessionRole()
	{
		this.<NameType>k__BackingField = REDNAME_TYPE.RNT_NORMAL;
		this.m_testAvatar = 0;
		this.m_testPro = 1;
		this.can_buff_move = true;
		this.can_buff_skill = true;
		this.can_buff_ani = true;
		base..ctor();
	}
}
