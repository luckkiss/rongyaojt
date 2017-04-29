using Cross;
using MuGame;
using System;
using UnityEngine;

public class BaseRole : INameObj
{
	public static int VIEW_TYPE_NONE = 0;

	public static int VIEW_TYPE_NAV = 1;

	public static int VIEW_TYPE_ALL = 2;

	public BaseRole m_LockRole;

	public float m_LockDis = 13f;

	public int m_circle_type = -1;

	public float m_circle_scale = 1f;

	public bool isfake = false;

	public uint m_unIID = 0u;

	public string _strIID = "";

	public uint m_unCID = 0u;

	public PK_TYPE m_ePK_Type = PK_TYPE.PK_PKALL;

	public uint m_unPK_Param = 0u;

	public uint m_unTeamID = 0u;

	public uint m_unLegionID = 0u;

	protected bool m_bFlyMonster = false;

	public float m_fSkillShowTime;

	protected float m_fDisposeTime = 0f;

	protected GameObject m_curGameObj;

	public Transform m_curModel;

	public Transform m_curPhy;

	public Animator m_curAni;

	public NavMeshAgent m_moveAgent;

	public float m_fAttackCount;

	public int m_curSkillId;

	public Transform m_LeftHand;

	public Transform m_RightHand;

	public Transform m_LeftFoot;

	public Transform m_RightFoot;

	public int m_nSPWeight = 1;

	public int m_nSPLevel = 1;

	public int m_nSkillSP_up;

	public int m_nSkillSP_fb;

	public float m_fSkillSPup_Value;

	public float m_fSkillSPfb_Value;

	public Vector3 m_vSkillSP_dir;

	public bool m_isMain = false;

	protected AI_TYPE m_eThinkType = AI_TYPE.MAIT_NONE;

	public Vector3 headOffset;

	public Vector3 headOffset_half;

	private string _roleName = "";

	public SXML tempXMl;

	private int _curhp = 100;

	private int _maxHp = 100;

	public bool isDead = false;

	public bool canbehurt = false;

	private int _title_id = 0;

	private bool _isactive = true;

	private int _rednm = 0;

	public uint _hidbacktime = 0u;

	protected int m_nNavPriority = 50;

	protected float m_fNavSpeed = 0.125f;

	protected float m_fNavStoppingDis = 1.5f;

	public int viewType = 0;

	public bool m_bHide_state;

	public float m_fHideTime = 0f;

	public RoleItemData m_roleDta = new RoleItemData();

	public bool viewInScene = false;

	public int m_layer;

	public bool disposed = false;

	protected bool attackAvailable = true;

	public static GameObject TEMP_SHADOW = Resources.Load("FX/monsterSFX/com_monster/fx_com_shader") as GameObject;

	public int lastHeadPosTick;

	public Vector3 lastHeadPos;

	private Vector3 old_move_pos;

	private int checkMoveTick;

	private int m_nAnimActiveCount;

	private Vector3 initPos;

	public string strIID
	{
		get
		{
			bool flag = this._strIID == "";
			if (flag)
			{
				this._strIID = (this.isfake ? ("fake" + this.m_unIID) : this.m_unIID.ToString());
			}
			return this._strIID;
		}
	}

	public string roleName
	{
		get
		{
			return this._roleName;
		}
		set
		{
			this._roleName = value;
		}
	}

	public int curhp
	{
		get
		{
			return this._curhp;
		}
		set
		{
			this._curhp = value;
		}
	}

	public int maxHp
	{
		get
		{
			return this._maxHp;
		}
		set
		{
			this._maxHp = value;
		}
	}

	public int title_id
	{
		get
		{
			return this._title_id;
		}
		set
		{
			this._title_id = value;
		}
	}

	public bool isactive
	{
		get
		{
			return this.isactive;
		}
		set
		{
			this._isactive = value;
			bool flag = !this._isactive;
			if (flag)
			{
				this._title_id = 0;
			}
		}
	}

	public int rednm
	{
		get
		{
			return this._rednm;
		}
		set
		{
			this._rednm = value;
		}
	}

	public uint hidbacktime
	{
		get
		{
			return this._hidbacktime;
		}
		set
		{
			this._hidbacktime = value;
		}
	}

	public bool m_isMarked
	{
		get;
		set;
	}

	public float scale
	{
		get
		{
			return this.m_roleDta.scale.x;
		}
		set
		{
			this.m_roleDta.scale = new Vector3(value, value, value);
			bool flag = this.m_curGameObj != null;
			if (flag)
			{
				this.m_curModel.transform.localScale = this.m_roleDta.scale;
			}
			bool flag2 = this.m_moveAgent != null && this.m_moveAgent.enabled;
			if (flag2)
			{
				this.m_moveAgent.radius = 0.5f * value;
			}
		}
	}

	protected void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f, bool isMain = false)
	{
		GameObject original = Resources.Load<GameObject>(prefab_path);
		this.m_curGameObj = UnityEngine.Object.Instantiate<GameObject>(original);
		this.m_layer = layer;
		Transform[] componentsInChildren = this.m_curGameObj.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = layer;
		}
		this.m_isMain = isMain;
		this.m_curModel = this.m_curGameObj.transform.FindChild("model");
		this.m_curPhy = this.m_curModel.transform.FindChild("physics");
		try
		{
			this.m_curPhy.gameObject.layer = EnumLayer.LM_BT_FIGHT;
		}
		catch (Exception var_5_A3)
		{
		}
		RoleItemData arg_BB_0 = this.m_roleDta;
		this.initPos = pos;
		arg_BB_0.pos = pos;
		this.m_roleDta.rotate = roatate;
		this.m_curAni = this.m_curModel.GetComponent<Animator>();
		CapsuleCollider component = this.m_curPhy.GetComponent<CapsuleCollider>();
		this.headOffset = component.center;
		this.headOffset.y = this.headOffset.y + component.height / 2f;
		this.headOffset_half = component.center;
		this.m_LeftHand = this.m_curModel.FindChild("L_Finger1");
		this.m_RightHand = this.m_curModel.FindChild("R_Finger1");
		this.m_LeftFoot = this.m_curModel.FindChild("L_Toe0");
		this.m_RightFoot = this.m_curModel.FindChild("R_Toe0");
		bool flag = this.m_LeftHand == null;
		if (flag)
		{
			this.m_LeftHand = U3DAPI.DEF_TRANSFORM;
		}
		bool flag2 = this.m_RightHand == null;
		if (flag2)
		{
			this.m_RightHand = U3DAPI.DEF_TRANSFORM;
		}
		bool flag3 = this.m_LeftFoot == null;
		if (flag3)
		{
			this.m_LeftFoot = U3DAPI.DEF_TRANSFORM;
		}
		bool flag4 = this.m_RightFoot == null;
		if (flag4)
		{
			this.m_RightFoot = U3DAPI.DEF_TRANSFORM;
		}
		this.refreshViewType(this.viewType);
		bool flag5 = layer != EnumLayer.LM_SELFROLE && !(this is CollectRole);
		if (flag5)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(BaseRole.TEMP_SHADOW);
			gameObject.transform.SetParent(this.m_curModel.transform, false);
			Quaternion localRotation = this.m_curModel.transform.localRotation;
			gameObject.transform.localRotation = Quaternion.Inverse(localRotation);
		}
	}

	public void initNavMesh()
	{
		bool flag = this.m_moveAgent != null;
		if (flag)
		{
			this.m_moveAgent.enabled = true;
		}
		else
		{
			this.m_moveAgent = this.m_curModel.GetComponent<NavMeshAgent>();
			bool flag2 = this.m_moveAgent == null;
			if (flag2)
			{
				this.m_moveAgent = this.m_curModel.gameObject.AddComponent<NavMeshAgent>();
			}
			this.m_moveAgent.stoppingDistance = this.m_fNavStoppingDis;
			this.m_moveAgent.speed = this.m_fNavSpeed;
			this.m_moveAgent.avoidancePriority = this.m_nNavPriority;
			this.m_moveAgent.angularSpeed = 360f;
		}
	}

	public void refreshViewType(int type = 0)
	{
		bool flag = this.m_curModel == null;
		if (flag)
		{
			this.viewType = type;
		}
		else
		{
			bool flag2 = type == BaseRole.VIEW_TYPE_NAV;
			if (flag2)
			{
				bool flag3 = this.m_isMain && type == BaseRole.VIEW_TYPE_NONE;
				if (flag3)
				{
					this.refreshViewType(BaseRole.VIEW_TYPE_ALL);
					return;
				}
				this.viewType = BaseRole.VIEW_TYPE_NAV;
				this.initNavMesh();
				this.setNavLay(this.m_roleDta.wallkableMask);
				this.setPos(this.m_roleDta.pos);
				this.m_moveAgent.speed = 8f;
			}
			else
			{
				bool flag4 = type == BaseRole.VIEW_TYPE_ALL;
				if (flag4)
				{
					this.initNavMesh();
					this.viewType = BaseRole.VIEW_TYPE_ALL;
					this.setNavLay(this.m_roleDta.wallkableMask);
					this.setPos(this.m_roleDta.pos);
					this.viewInScene = true;
					bool flag5 = !this.isfake;
					if (flag5)
					{
						this.m_curAni.SetBool(EnumAni.ANI_B_BORNED, true);
						this.canbehurt = true;
					}
					bool flag6 = this.m_roleDta.rotate != 0f;
					if (flag6)
					{
						this.m_curModel.eulerAngles = new Vector3(this.m_curModel.eulerAngles.x, this.m_roleDta.rotate, this.m_curModel.eulerAngles.z);
					}
					this.scale = this.m_roleDta.scale.x;
				}
				else
				{
					this.viewType = BaseRole.VIEW_TYPE_NONE;
					bool flag7 = this.m_moveAgent;
					if (flag7)
					{
						this.m_moveAgent.baseOffset = 0f;
						this.m_moveAgent.enabled = false;
					}
				}
			}
			this.onRefreshViewType();
		}
	}

	protected virtual void onRefreshViewType()
	{
	}

	public void setNavLay(int idx)
	{
		bool flag = this.m_moveAgent == null || !this.m_moveAgent.enabled;
		if (flag)
		{
			this.m_roleDta.wallkableMask = idx;
		}
		else
		{
			this.m_moveAgent.walkableMask = idx;
		}
	}

	public Vector3 getHeadPos()
	{
		bool flag = this.m_curPhy == null || SceneCamera.m_curCamera == null;
		Vector3 result;
		if (flag)
		{
			result = Vector3.zero;
		}
		else
		{
			bool flag2 = SelfRole._inst.m_curPhy.position.x > this.m_curPhy.position.x;
			float num;
			if (flag2)
			{
				num = SelfRole._inst.m_curPhy.position.x - this.m_curPhy.position.x;
			}
			else
			{
				num = this.m_curPhy.position.x - SelfRole._inst.m_curPhy.position.x;
			}
			bool flag3 = SelfRole._inst.m_curPhy.position.y > this.m_curPhy.position.y;
			float num2;
			if (flag3)
			{
				num2 = SelfRole._inst.m_curPhy.position.y - this.m_curPhy.position.y;
			}
			else
			{
				num2 = this.m_curPhy.position.y - SelfRole._inst.m_curPhy.position.y;
			}
			float num3 = num + num2;
			bool flag4 = num3 > 10f;
			if (flag4)
			{
				result = Vector3.zero;
			}
			else
			{
				int tickNum = TickMgr.tickNum;
				bool flag5 = this.lastHeadPosTick == tickNum;
				if (flag5)
				{
					result = this.lastHeadPos;
				}
				else
				{
					this.lastHeadPosTick = tickNum;
					Vector3 vector = this.m_curPhy.position + this.headOffset;
					vector = SceneCamera.m_curCamera.WorldToScreenPoint(vector);
					vector *= SceneCamera.m_fGameScreenPow;
					vector.z = 0f;
					this.lastHeadPos = vector;
					result = vector;
				}
			}
		}
		return result;
	}

	public virtual void refreshViewData(Variant v)
	{
	}

	public void TurnToPos(Vector3 pos)
	{
		bool flag = pos != Vector3.zero;
		if (flag)
		{
			float x = pos.x - this.m_curModel.position.x;
			float z = pos.z - this.m_curModel.position.z;
			Vector3 vector = new Vector3(x, 0f, z);
			bool flag2 = vector != Vector3.zero;
			if (flag2)
			{
				this.m_curModel.forward = vector;
			}
		}
	}

	public void setRoleRoatate(float r)
	{
		Vector3 eulerAngles = this.m_curModel.eulerAngles;
		eulerAngles.y = r;
		this.m_curModel.eulerAngles = eulerAngles;
	}

	public void TurnToRole(BaseRole r, bool ismyself)
	{
		if (ismyself)
		{
			bool flag = PkmodelAdmin.RefreshLockRoleTransform(r) == null;
			if (flag)
			{
				return;
			}
			bool flag2 = r == null;
			if (flag2)
			{
				return;
			}
		}
		bool flag3 = r.m_curModel != null;
		if (flag3)
		{
			Vector3 position = r.m_curModel.position;
			this.TurnToPos(position);
		}
	}

	public virtual void onServerHurt(int damage, int hp, bool dead, BaseRole frm = null, int isCrit = -1, bool miss = false, bool stagger = false)
	{
	}

	public virtual void PlaySkill(int id)
	{
	}

	public void OtherSkillShow()
	{
		bool flag = this.m_moveAgent != null;
		if (flag)
		{
			this.m_moveAgent.updateRotation = false;
		}
		this.m_fSkillShowTime = 1.4f;
	}

	public void modHp(int hp)
	{
		this.curhp = hp;
		PlayerNameUIMgr.getInstance().refreshHp(this, this.curhp, this.maxHp);
	}

	public void pos_correct(Vector3 pos)
	{
		bool flag = Vector3.Distance(pos, this.m_curModel.position) > 15f;
		if (flag)
		{
			this.m_curModel.GetComponent<NavMeshAgent>().enabled = false;
			this.m_curModel.position = pos;
			this.m_roleDta.pos = pos;
			this.m_curModel.GetComponent<NavMeshAgent>().enabled = true;
		}
		else
		{
			this.SetDestPos(pos);
		}
	}

	public void SetDestPos(Vector3 pos)
	{
		bool flag = Vector3.Distance(this.m_roleDta.pos, this.m_curModel.position) < 1.5f;
		this.m_roleDta.pos = pos;
		bool flag2 = this.viewType == BaseRole.VIEW_TYPE_NONE || this.m_moveAgent == null;
		if (!flag2)
		{
			bool flag3 = this.isfake && this.m_eThinkType == AI_TYPE.MAIT_BORN;
			if (flag3)
			{
				this.m_roleDta.pos = pos;
			}
			else
			{
				bool flag4 = !this.isDead;
				if (flag4)
				{
					bool flag5 = this.m_moveAgent && this.m_moveAgent.enabled && this.m_moveAgent.isOnNavMesh;
					if (flag5)
					{
						NavMeshPath navMeshPath = new NavMeshPath();
						this.m_moveAgent.CalculatePath(this.m_roleDta.pos, navMeshPath);
						bool flag6 = navMeshPath.corners.Length > 1;
						if (flag6)
						{
							this.TurnToPos(navMeshPath.corners[1]);
						}
						bool isMain = this.m_isMain;
						if (isMain)
						{
							NavMeshHit navMeshHit;
							NavMesh.SamplePosition(pos, out navMeshHit, 100f, this.m_layer);
							this.m_moveAgent.SetDestination(navMeshHit.position);
							this.m_moveAgent.CalculatePath(navMeshHit.position, navMeshPath);
							bool flag7 = worldmap.instance != null;
							if (flag7)
							{
								worldmap.instance.DrawMapImage(navMeshPath);
							}
							else
							{
								worldmap.Desmapimg();
							}
						}
						else
						{
							this.m_moveAgent.SetDestination(pos);
						}
						bool flag8 = this.m_fSkillShowTime > 0f;
						if (flag8)
						{
						}
					}
				}
			}
		}
	}

	public void setPos(Vector3 vec)
	{
		bool flag = this.viewType == BaseRole.VIEW_TYPE_NONE;
		if (flag)
		{
			this.m_roleDta.pos = vec;
		}
		else
		{
			NavMeshHit navMeshHit;
			bool flag2 = NavMesh.SamplePosition(vec, out navMeshHit, 100f, NavmeshUtils.allARE);
			if (flag2)
			{
				bool flag3 = this.m_moveAgent;
				if (flag3)
				{
					this.m_moveAgent.enabled = false;
					this.m_moveAgent.transform.position = navMeshHit.position;
					this.m_moveAgent.enabled = true;
				}
			}
		}
	}

	public virtual void dispose()
	{
		this.disposed = true;
		PlayerNameUIMgr.getInstance().hide(this);
		UnityEngine.Object.Destroy(this.m_curGameObj);
		bool flag = SelfRole._inst.m_LockRole == this;
		if (flag)
		{
			SelfRole._inst.m_LockRole = null;
		}
	}

	public virtual void FrameMove(float delta_time)
	{
		bool flag = this.m_curPhy == null || this.isDead;
		if (!flag)
		{
			bool flag2 = this.m_nAnimActiveCount < 0;
			if (flag2)
			{
				Animator component = this.m_curModel.GetComponent<Animator>();
				bool flag3 = component != null;
				if (flag3)
				{
					bool flag4 = this.m_nAnimActiveCount == -2;
					if (flag4)
					{
					}
					bool flag5 = this.m_nAnimActiveCount == -1;
					if (flag5)
					{
						component.Rebind();
						bool flag6 = this is ProfessionRole || this is MonsterPlayer;
						if (flag6)
						{
							bool flag7 = this.m_roleDta.m_WindID > 0;
							if (flag7)
							{
								component.SetFloat(EnumAni.ANI_F_FLY, 1f);
							}
							else
							{
								component.SetFloat(EnumAni.ANI_F_FLY, 0f);
							}
						}
					}
				}
				this.m_nAnimActiveCount++;
			}
			bool isMain = this.m_isMain;
			if (isMain)
			{
				bool flag8 = this.m_fSkillShowTime > 0f;
				if (flag8)
				{
					this.m_fSkillShowTime -= delta_time;
				}
			}
			else
			{
				bool flag9 = this.m_nSkillSP_up == 1;
				if (flag9)
				{
					bool flag10 = this.m_fSkillSPup_Value > 0f;
					if (flag10)
					{
						this.m_moveAgent.baseOffset += this.m_fSkillSPup_Value * 0.5f * (float)this.m_nSPLevel;
						this.m_fSkillSPup_Value -= delta_time;
					}
					else
					{
						this.m_moveAgent.baseOffset += this.m_fSkillSPup_Value * 0.5f * (float)this.m_nSPLevel;
						this.m_fSkillSPup_Value -= delta_time;
						bool flag11 = this.m_moveAgent.baseOffset <= 0f;
						if (flag11)
						{
							this.m_moveAgent.baseOffset = 0f;
							this.m_nSkillSP_up = 0;
						}
					}
				}
				bool flag12 = this.m_nSkillSP_fb != 0;
				if (flag12)
				{
					bool flag13 = this.m_nSkillSP_fb == -21;
					if (flag13)
					{
						bool flag14 = this.m_fSkillSPfb_Value > 0f;
						if (flag14)
						{
							Vector3 a = this.m_curModel.position - this.m_vSkillSP_dir;
							a.Normalize();
							this.m_curModel.position -= a * (float)this.m_nSPLevel * this.m_fSkillSPfb_Value;
							this.m_fSkillSPfb_Value -= delta_time;
							bool flag15 = this.m_fSkillSPfb_Value <= 0f;
							if (flag15)
							{
								this.m_nSkillSP_fb = 0;
							}
						}
					}
					else
					{
						bool flag16 = this.m_nSkillSP_fb == -31;
						if (flag16)
						{
							bool flag17 = this.m_fSkillSPfb_Value > 0f;
							if (flag17)
							{
								this.m_curAni.enabled = false;
								this.m_fSkillSPfb_Value -= delta_time;
							}
							bool flag18 = this.m_fSkillSPfb_Value <= 0f;
							if (flag18)
							{
								this.m_curAni.enabled = true;
								this.m_nSkillSP_fb = 0;
							}
						}
						else
						{
							bool flag19 = this.m_nSkillSP_fb == -41;
							if (flag19)
							{
								bool flag20 = this.m_fSkillSPfb_Value > 0f;
								if (flag20)
								{
									Vector3 a2 = this.m_curModel.position - this.m_vSkillSP_dir;
									a2.Normalize();
									this.m_curModel.position -= a2 * delta_time * 3f;
									this.m_fSkillSPfb_Value -= delta_time;
									bool flag21 = this.m_fSkillSPfb_Value <= 0f;
									if (flag21)
									{
										this.m_nSkillSP_fb = 0;
									}
								}
							}
							else
							{
								bool flag22 = this.m_fSkillSPfb_Value > 0f;
								if (flag22)
								{
									this.m_curModel.position += this.m_vSkillSP_dir * (float)this.m_nSPLevel * this.m_fSkillSPfb_Value * (float)this.m_nSkillSP_fb;
									this.m_fSkillSPfb_Value -= delta_time;
									bool flag23 = this.m_fSkillSPfb_Value <= 0f;
									if (flag23)
									{
										this.m_nSkillSP_fb = 0;
									}
								}
							}
						}
					}
				}
				bool flag24 = this.m_fSkillShowTime > 0f;
				if (flag24)
				{
					this.m_moveAgent.updateRotation = false;
					this.m_fSkillShowTime -= delta_time;
					this.m_curAni.SetBool(EnumAni.ANI_RUN, false);
				}
				else
				{
					this.m_moveAgent.updateRotation = true;
					bool flag25 = !this.m_moveAgent.enabled;
					if (!flag25)
					{
						bool flag26 = !this.m_moveAgent.isOnNavMesh;
						if (!flag26)
						{
							bool flag27 = this.viewType == BaseRole.VIEW_TYPE_ALL && this.m_eThinkType == AI_TYPE.MAIT_NONE;
							if (flag27)
							{
								float num = Vector3.Distance(this.m_moveAgent.destination.ConvertToGamePosition(), this.m_curModel.transform.position.ConvertToGamePosition());
								bool flag28 = !this.m_bFlyMonster && this.m_moveAgent.speed > 0.125f;
								if (flag28)
								{
									this.m_moveAgent.speed = 0.125f;
								}
								bool flag29 = num > 0.93f;
								if (flag29)
								{
									bool flag30 = this.m_moveAgent != null && !this.m_moveAgent.updateRotation;
									if (flag30)
									{
										this.m_moveAgent.updateRotation = true;
										bool flag31 = !this.m_bFlyMonster;
										if (flag31)
										{
											this.m_moveAgent.speed = 0.125f;
										}
									}
									this.m_curAni.SetBool(EnumAni.ANI_RUN, true);
									bool flag32 = !this.m_isMain;
									if (flag32)
									{
										this.SetDestPos(this.m_moveAgent.destination);
									}
								}
								else
								{
									this.m_curAni.SetBool(EnumAni.ANI_RUN, false);
								}
							}
						}
					}
				}
			}
		}
	}

	public BaseRole()
	{
		this.<m_isMarked>k__BackingField = false;
		this.lastHeadPosTick = 0;
		this.lastHeadPos = Vector3.zero;
		this.old_move_pos = Vector3.zero;
		this.checkMoveTick = 0;
		this.m_nAnimActiveCount = -3;
		base..ctor();
	}
}
