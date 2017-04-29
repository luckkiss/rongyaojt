using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRole : BaseRole
{
	private Vector3 bornPos;

	private bool canTestDis;

	public int monsterid;

	protected float m_fThinkTime = 0.25f;

	protected float m_fFreezeTime = 0f;

	protected float m_fFreezeSpeed = 0f;

	protected bool m_bdoFreeze = false;

	public bool isBoos = false;

	protected float m_fAttackAngle = 16f;

	public bool issummon = false;

	public int summonid = 0;

	public bool isDart = false;

	public int dartid = 0;

	public uint masterid = 0u;

	protected SkinnedMeshRenderer m_MonBody;

	protected SkinnedMeshRenderer m_MonBody1;

	protected Color m_Main_Color;

	protected Color m_Rim_Color;

	protected float m_Rim_Width;

	private float m_fhitFlash_time = 0f;

	private bool m_bhitFlashGoUp = false;

	private bool m_bDoHitFlash = false;

	private bool m_bchangeDeadMtl = false;

	private bool m_bchangeLiveMtl = false;

	public int born_type = 0;

	private float m_dead_Burn = 0f;

	private float m_live_Burn = 1f;

	private Material m_mat;

	public bool m_remove_after_dead = false;

	private float height;

	private float radius;

	private bool isDartRole = false;

	public string ownerName;

	public Vector3 BornPos
	{
		get
		{
			return this.bornPos;
		}
	}

	public bool hasBorned
	{
		get
		{
			return this.m_eThinkType != AI_TYPE.MAIT_BORN;
		}
	}

	public virtual void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f)
	{
		base.Init(prefab_path, layer, pos, roatate, false);
		this.m_unLegionID = 1u;
		this.m_curGameObj.SetActive(true);
		MonHurtPoint monHurtPoint = this.m_curPhy.gameObject.AddComponent<MonHurtPoint>();
		monHurtPoint.m_monRole = this;
		base.setNavLay(NavmeshUtils.allARE);
		bool flag = this is MonsterPlayer;
		if (flag)
		{
			PlayerNameUIMgr.getInstance().show(this);
		}
		Transform transform = this.m_curModel.FindChild("body");
		bool flag2 = transform != null;
		if (flag2)
		{
			this.m_MonBody = transform.GetComponent<SkinnedMeshRenderer>();
			bool flag3 = this.m_MonBody.material.HasProperty(EnumShader.SPI_COLOR);
			if (flag3)
			{
				this.m_Main_Color = this.m_MonBody.material.GetColor(EnumShader.SPI_COLOR);
			}
			bool flag4 = this.m_MonBody.material.HasProperty(EnumShader.SPI_RIMCOLOR);
			if (flag4)
			{
				this.m_Rim_Color = this.m_MonBody.material.GetColor(EnumShader.SPI_RIMCOLOR);
			}
			bool flag5 = this.m_MonBody.material.HasProperty(EnumShader.SPI_RIMWIDTH);
			if (flag5)
			{
				this.m_Rim_Width = this.m_MonBody.material.GetFloat(EnumShader.SPI_RIMWIDTH);
			}
			this.m_mat = this.m_MonBody.material;
		}
		bool flag6 = this.m_MonBody == null;
		if (flag6)
		{
			this.m_MonBody = U3DAPI.DEF_SKINNEDMESHRENDERER;
		}
		Transform transform2 = this.m_curModel.FindChild("body1");
		bool flag7 = transform2 != null;
		if (flag7)
		{
			this.m_MonBody1 = transform2.GetComponent<SkinnedMeshRenderer>();
		}
		bool flag8 = this.tempXMl != null && this.tempXMl.getInt("born_eff") > 0;
		if (flag8)
		{
			this.onBornStart(this.tempXMl.getInt("born_eff"));
		}
		this.height = this.m_curModel.FindChild("physics").GetComponent<CapsuleCollider>().height;
		this.radius = this.m_curModel.FindChild("physics").GetComponent<CapsuleCollider>().radius;
	}

	public virtual void onClick()
	{
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

	public override void onServerHurt(int damage, int hp, bool dead, BaseRole frm = null, int isCrit = -1, bool miss = false, bool stagger = false)
	{
		bool isDead = this.isDead;
		if (!isDead)
		{
			base.curhp = hp;
			if (dead)
			{
				this.onDead();
			}
			bool flag = !miss;
			if (flag)
			{
				PlayerNameUIMgr.getInstance().refreshHp(this, base.curhp, base.maxHp);
				bool flag2 = this is MS0000 && (long)(this as MS0000).owner_cid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid);
				if (flag2)
				{
					bool flag3 = a3_herohead.instance;
					if (flag3)
					{
						a3_herohead.instance.refresh_sumHp(base.curhp, base.maxHp);
					}
				}
			}
			bool flag4 = frm != null && (SelfRole._inst.m_unIID == frm.m_unIID || (frm is MS0000 && (long)((MS0000)frm).owner_cid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid)));
			if (flag4)
			{
				FightText.CurrentCauseRole = frm;
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

	public void setHitFlash(HitData hd)
	{
		bool flag = !(this is MonsterPlayer);
		if (flag)
		{
			this.m_bhitFlashGoUp = true;
			this.m_bDoHitFlash = true;
			this.m_MonBody.material.SetColor(EnumShader.SPI_COLOR, hd.m_Color_Main);
			this.m_MonBody.material.SetColor(EnumShader.SPI_RIMCOLOR, hd.m_Color_Rim);
			bool flag2 = this.m_MonBody1 != null;
			if (flag2)
			{
				this.m_MonBody1.material.SetColor(EnumShader.SPI_COLOR, hd.m_Color_Main);
				this.m_MonBody1.material.SetColor(EnumShader.SPI_RIMCOLOR, hd.m_Color_Rim);
			}
		}
	}

	public void AI_Sick()
	{
		base.SetDestPos(SelfRole._inst.m_curModel.position);
		bool flag = this.m_moveAgent.remainingDistance > this.m_fNavStoppingDis;
		if (!flag)
		{
			this.m_eThinkType = AI_TYPE.MAIT_ATTACK;
			this.m_curAni.SetBool(EnumAni.ANI_RUN, false);
			this.m_moveAgent.updateRotation = false;
		}
	}

	protected virtual void EnterAttackState()
	{
		this.m_curAni.SetBool(EnumAni.ANI_ATTACK, true);
	}

	protected virtual void LeaveAttackState()
	{
		this.m_curAni.SetBool(EnumAni.ANI_ATTACK, false);
	}

	public void AI_Attack(float delta_time)
	{
		bool flag = !this.attackAvailable;
		if (!flag)
		{
			Vector3 to = SelfRole._inst.m_curModel.position - this.m_curModel.position;
			bool flag2 = to.magnitude > 4f;
			if (flag2)
			{
				this.m_eThinkType = AI_TYPE.MAIT_SICK;
				this.m_curAni.SetBool(EnumAni.ANI_RUN, true);
			}
			else
			{
				Vector3 forward = this.m_curModel.forward;
				forward.y = 0f;
				float num = Vector3.Angle(forward, to);
				bool flag3 = true;
				bool flag4 = num < this.m_fAttackAngle;
				if (flag4)
				{
					this.EnterAttackState();
					this.m_fAttackCount = 1f;
					flag3 = false;
				}
				bool flag5 = flag3;
				if (flag5)
				{
					Vector3 right = this.m_curModel.right;
					right.y = 0f;
					this.LeaveAttackState();
					num = Vector3.Angle(right, to);
					bool flag6 = num < 90f;
					if (flag6)
					{
						this.m_curModel.transform.Rotate(Vector3.up, 256f * delta_time);
					}
					else
					{
						this.m_curModel.transform.Rotate(Vector3.up, -256f * delta_time);
					}
				}
			}
		}
	}

	public HitData BuildBullet(uint skillid, float t, GameObject original, Vector3 position, Quaternion rotation)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(original, position, rotation) as GameObject;
		gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
		HitData hitData = gameObject.gameObject.AddComponent<HitData>();
		hitData.m_vBornerPos = this.m_curModel.position;
		hitData.m_CastRole = this;
		hitData.m_ePK_Type = PK_TYPE.PK_LEGION;
		hitData.m_unPK_Param = this.m_unLegionID;
		hitData.m_unSkillID = skillid;
		hitData.m_nDamage = 100;
		hitData.m_nHitType = 0;
		gameObject.layer = EnumLayer.LM_BT_FIGHT;
		UnityEngine.Object.Destroy(gameObject, t);
		bool flag = this.m_fDisposeTime < t;
		if (flag)
		{
			this.m_fDisposeTime = t;
		}
		return hitData;
	}

	public HitData longBullet(uint skillid, float t, GameObject original, Transform linker)
	{
		HitData hitData = linker.gameObject.AddComponent<HitData>();
		hitData.m_hdRootObj = original;
		hitData.m_vBornerPos = this.m_curModel.position;
		hitData.m_CastRole = this;
		hitData.m_ePK_Type = PK_TYPE.PK_LEGION;
		hitData.m_unPK_Param = this.m_unLegionID;
		hitData.m_unSkillID = skillid;
		hitData.m_nDamage = 100;
		hitData.m_nHitType = 0;
		linker.gameObject.layer = EnumLayer.LM_BT_FIGHT;
		UnityEngine.Object.Destroy(original, t);
		bool flag = this.m_fDisposeTime < t;
		if (flag)
		{
			this.m_fDisposeTime = t;
		}
		return hitData;
	}

	private void onfly(float s)
	{
	}

	public void onHurt(HitData hd)
	{
		bool isDead = this.isDead;
		if (!isDead)
		{
			this.setHitFlash(hd);
			bool flag = hd.m_nHurtFX > 0 && hd.m_nHurtFX < 10;
			if (flag)
			{
				bool flag2 = hd.m_nHurtFX == 6;
				if (flag2)
				{
					Vector3 zero = Vector3.zero;
					zero.y += this.headOffset.y;
					GameObject gameObject = UnityEngine.Object.Instantiate(SceneTFX.m_HFX_Prefabs[hd.m_nHurtFX], zero, this.m_curModel.rotation) as GameObject;
					gameObject.transform.SetParent(this.m_curModel, false);
					UnityEngine.Object.Destroy(gameObject, 2f);
				}
				else
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate(SceneTFX.m_HFX_Prefabs[hd.m_nHurtFX], this.m_curModel.position, this.m_curModel.rotation) as GameObject;
					gameObject2.transform.SetParent(U3DAPI.FX_POOL_TF, false);
					UnityEngine.Object.Destroy(gameObject2, 2f);
				}
			}
			bool flag3 = hd.m_nDamage == 0;
			if (!flag3)
			{
				base.curhp -= hd.m_nDamage;
				bool flag4 = base.curhp < 0;
				if (flag4)
				{
					base.curhp = 0;
				}
				PlayerNameUIMgr.getInstance().refreshHp(this, base.curhp, base.maxHp);
				bool flag5 = this is MS0000 && (long)(this as MS0000).owner_cid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid);
				if (flag5)
				{
					bool flag6 = a3_herohead.instance;
					if (flag6)
					{
						a3_herohead.instance.refresh_sumHp(base.curhp, base.maxHp);
					}
				}
				bool flag7 = SelfRole._inst.m_unIID == hd.m_CastRole.m_unIID;
				if (flag7)
				{
					FightText.play(FightText.userText, this.lastHeadPos, hd.m_nDamage, false, -1);
				}
				bool flag8 = base.curhp == 0;
				if (flag8)
				{
					this.m_curPhy.gameObject.SetActive(false);
					this.onDead();
				}
				else
				{
					this.PlayHurtFront();
				}
			}
		}
	}

	public virtual void onDeadEnd()
	{
		bool flag = this.m_MonBody != null;
		if (flag)
		{
			SkinnedMeshRenderer[] componentsInChildren = this.m_MonBody.GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Material material = UnityEngine.Object.Instantiate<Material>(gameST.DEAD_MTL);
				Texture texture = componentsInChildren[i].material.GetTexture(gameST.MTL_Main_Tex);
				material.SetTexture(gameST.MTL_Dead_Tex, texture);
				componentsInChildren[i].material = material;
			}
			this.m_bchangeDeadMtl = true;
		}
	}

	public virtual void onBornStart(int type)
	{
		bool flag = this.m_MonBody != null;
		if (flag)
		{
			Material original = U3DAPI.U3DResLoad<Material>("mtl/born_mtl" + type);
			SkinnedMeshRenderer[] componentsInChildren = this.m_MonBody.GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Material material = UnityEngine.Object.Instantiate<Material>(original);
				Texture texture = componentsInChildren[i].material.GetTexture(gameST.MTL_Main_Tex);
				material.SetTexture(gameST.MTL_Dead_Tex, texture);
				componentsInChildren[i].material = material;
			}
			this.m_MonBody.material.SetFloat(gameST.DEAD_MT_AMOUNT, this.m_live_Burn);
			this.m_bchangeLiveMtl = true;
		}
	}

	public void onResetMTL()
	{
		bool flag = this.m_MonBody != null;
		if (flag)
		{
			SkinnedMeshRenderer[] componentsInChildren = this.m_MonBody.GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].material = this.m_mat;
			}
		}
		Color arg_48_0 = this.m_Main_Color;
		bool flag2 = true;
		if (flag2)
		{
			this.m_MonBody.material.SetColor(EnumShader.SPI_COLOR, this.m_Main_Color);
		}
		else
		{
			this.m_MonBody.material.SetColor(EnumShader.SPI_COLOR, Color.white);
		}
		Color arg_8F_0 = this.m_Rim_Color;
		bool flag3 = true;
		if (flag3)
		{
			this.m_MonBody.material.SetColor(EnumShader.SPI_RIMCOLOR, this.m_Rim_Color);
		}
		else
		{
			this.m_MonBody.material.SetColor(EnumShader.SPI_RIMCOLOR, Color.black);
		}
		this.m_MonBody.material.SetFloat(EnumShader.SPI_RIMWIDTH, this.m_Rim_Width);
	}

	public void onDead()
	{
		bool flag = !this.isDead;
		if (flag)
		{
			this.isDead = true;
			bool isfake = this.isfake;
			if (isfake)
			{
				int random = ConfigUtil.getRandom(1, 3);
				bool flag2 = random > 0;
				if (flag2)
				{
					List<DropItemdta> list = new List<DropItemdta>();
					for (int i = 0; i < random; i++)
					{
						list.Add(new DropItemdta
						{
							dpid = 0u,
							count = ConfigUtil.getRandom(1, 10)
						});
					}
				}
			}
			bool flag3 = this.m_curAni;
			if (flag3)
			{
				this.m_curAni.enabled = true;
				this.m_curAni.SetBool(EnumAni.ANI_B_DIE, true);
			}
			bool flag4 = this.m_curPhy;
			if (flag4)
			{
				this.m_curPhy.gameObject.SetActive(false);
			}
			bool flag5 = this.m_moveAgent;
			if (flag5)
			{
				this.m_moveAgent.baseOffset = 0f;
				this.m_moveAgent.enabled = false;
			}
			PlayerNameUIMgr.getInstance().hide(this);
			bool flag6 = MapModel.getInstance().curLevelId > 0u;
			if (flag6)
			{
				bool flag7 = MapModel.getInstance().dFbDta.ContainsKey((int)MapModel.getInstance().curLevelId);
				if (flag7)
				{
					MapData expr_15D = MapModel.getInstance().dFbDta[(int)MapModel.getInstance().curLevelId];
					int kmNum = expr_15D.kmNum;
					expr_15D.kmNum = kmNum + 1;
				}
			}
			bool flag8 = GameRoomMgr.getInstance().curRoom != null;
			if (flag8)
			{
				GameRoomMgr.getInstance().curRoom.onMonsterDied(this);
			}
		}
	}

	public void onBorned()
	{
		PlayerNameUIMgr.getInstance().show(this);
		bool flag = this.m_eThinkType > AI_TYPE.MAIT_NONE;
		if (flag)
		{
			this.m_eThinkType = AI_TYPE.MAIT_SICK;
		}
		this.canbehurt = true;
		bool flag2 = this.m_moveAgent != null;
		if (flag2)
		{
			this.m_moveAgent.enabled = true;
		}
		this.bornPos = this.m_moveAgent.transform.position;
	}

	public void SleepAI(float time)
	{
		this.m_fThinkTime = time;
	}

	public void FreezeAni(float time, float speed)
	{
		this.m_fFreezeTime = time;
		this.m_fFreezeSpeed = speed;
		this.m_bdoFreeze = true;
	}

	public override void PlaySkill(int id)
	{
		bool flag = this.m_curSkillId != 0;
		if (!flag)
		{
			this.m_fAttackCount = 0.5f;
			bool flag2 = id == 1;
			if (flag2)
			{
				this.EnterAttackState();
			}
			else
			{
				this.m_curSkillId = id;
				this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, id);
			}
		}
	}

	protected override void onRefreshViewType()
	{
		bool flag = this.viewType == BaseRole.VIEW_TYPE_ALL && this.isfake && this.m_eThinkType == AI_TYPE.MAIT_NONE;
		if (flag)
		{
			this.m_eThinkType = AI_TYPE.MAIT_BORN;
			this.m_moveAgent.baseOffset = 0f;
			this.m_moveAgent.enabled = false;
			this.canbehurt = false;
		}
		bool flag2 = this.m_moveAgent;
		if (flag2)
		{
			this.m_moveAgent.radius = this.radius * 0.8f;
			this.m_moveAgent.height = this.height;
		}
	}

	public new virtual void FrameMove(float delta_time)
	{
		base.FrameMove(delta_time);
		bool disposed = this.disposed;
		if (!disposed)
		{
			bool bchangeDeadMtl = this.m_bchangeDeadMtl;
			if (bchangeDeadMtl)
			{
				bool flag = this.m_dead_Burn < 1f;
				if (flag)
				{
					this.m_dead_Burn += delta_time * 0.75f;
					this.m_MonBody.material.SetFloat(gameST.DEAD_MT_AMOUNT, this.m_dead_Burn);
				}
				else
				{
					this.m_remove_after_dead = true;
				}
			}
			bool bchangeLiveMtl = this.m_bchangeLiveMtl;
			if (bchangeLiveMtl)
			{
				bool flag2 = this.m_live_Burn > 0f;
				if (flag2)
				{
					this.m_live_Burn -= delta_time * 0.75f;
					this.m_MonBody.material.SetFloat(gameST.DEAD_MT_AMOUNT, this.m_live_Burn);
				}
				else
				{
					this.m_bchangeLiveMtl = false;
					this.onResetMTL();
				}
			}
			bool bDoHitFlash = this.m_bDoHitFlash;
			if (bDoHitFlash)
			{
				bool bhitFlashGoUp = this.m_bhitFlashGoUp;
				if (bhitFlashGoUp)
				{
					this.m_fhitFlash_time += delta_time * 10f;
					bool flag3 = this.m_fhitFlash_time > 0.25f;
					if (flag3)
					{
						this.m_fhitFlash_time = 0.25f;
						this.m_bhitFlashGoUp = false;
					}
					this.m_MonBody.material.SetFloat(EnumShader.SPI_RIMWIDTH, this.m_fhitFlash_time * 6f);
				}
				else
				{
					bool flag4 = this.m_nSkillSP_fb == -31;
					if (flag4)
					{
						this.m_MonBody.material.SetFloat(EnumShader.SPI_RIMWIDTH, 1.5f);
					}
					else
					{
						this.m_fhitFlash_time -= delta_time;
						bool flag5 = this.m_fhitFlash_time <= 0f;
						if (flag5)
						{
							this.m_fhitFlash_time = 0f;
							this.m_bDoHitFlash = false;
							Color arg_1C9_0 = this.m_Main_Color;
							bool flag6 = true;
							if (flag6)
							{
								this.m_MonBody.material.SetColor(EnumShader.SPI_COLOR, this.m_Main_Color);
							}
							else
							{
								this.m_MonBody.material.SetColor(EnumShader.SPI_COLOR, Color.white);
							}
							Color arg_210_0 = this.m_Rim_Color;
							bool flag7 = true;
							if (flag7)
							{
								this.m_MonBody.material.SetColor(EnumShader.SPI_RIMCOLOR, this.m_Rim_Color);
							}
							else
							{
								this.m_MonBody.material.SetColor(EnumShader.SPI_RIMCOLOR, Color.black);
							}
							this.m_MonBody.material.SetFloat(EnumShader.SPI_RIMWIDTH, this.m_Rim_Width);
							bool flag8 = this.m_MonBody1 != null;
							if (flag8)
							{
								this.m_MonBody1.material.SetColor(EnumShader.SPI_COLOR, this.m_Main_Color);
								this.m_MonBody1.material.SetColor(EnumShader.SPI_RIMCOLOR, this.m_Rim_Color);
								this.m_MonBody1.material.SetFloat(EnumShader.SPI_RIMWIDTH, this.m_Rim_Width);
							}
						}
						else
						{
							this.m_MonBody.material.SetFloat(EnumShader.SPI_RIMWIDTH, this.m_fhitFlash_time * 6f);
							bool flag9 = this.m_MonBody1 != null;
							if (flag9)
							{
								this.m_MonBody1.material.SetFloat(EnumShader.SPI_RIMWIDTH, this.m_fhitFlash_time * 6f);
							}
						}
					}
				}
			}
			bool flag10 = this.m_fAttackCount > 0f;
			if (flag10)
			{
				this.m_fAttackCount -= delta_time;
				bool flag11 = this.m_fAttackCount <= 0f;
				if (flag11)
				{
					this.m_curSkillId = 0;
					this.LeaveAttackState();
					this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, 0);
				}
			}
			bool flag12 = this.m_moveAgent != null && this.m_moveAgent.enabled && this.m_moveAgent.isOnNavMesh;
			if (flag12)
			{
				bool flag13 = this.isDartRole;
				if (flag13)
				{
					return;
				}
				bool flag14 = this.m_moveAgent.remainingDistance > 1f;
				if (flag14)
				{
					bool flag15 = this.canTestDis;
					if (flag15)
					{
						this.m_moveAgent.avoidancePriority = 50 - (int)this.m_moveAgent.remainingDistance;
						this.canTestDis = false;
					}
				}
				else
				{
					this.m_moveAgent.avoidancePriority = 10;
					this.canTestDis = true;
				}
				M00000_Default_Event expr_442 = this.m_curModel.GetComponent<M00000_Default_Event>();
				bool flag16 = ((expr_442 != null) ? expr_442.m_monRole : null) is MDC000;
				if (flag16)
				{
					this.m_moveAgent.avoidancePriority = 0;
					this.isDartRole = true;
					Debug.Log("isDartRole");
				}
			}
			bool isDead = this.isDead;
			if (!isDead)
			{
				this.m_fFreezeTime -= delta_time;
				bool flag17 = this.m_fFreezeTime > 0f;
				if (flag17)
				{
					this.m_curAni.speed = this.m_fFreezeSpeed;
				}
				else
				{
					bool flag18 = this.m_fFreezeTime < 0f && this.m_bdoFreeze;
					if (flag18)
					{
						this.m_fFreezeTime = 0f;
						this.m_curAni.speed = 1f;
						this.m_bdoFreeze = false;
					}
				}
				bool flag19 = this.m_eThinkType == AI_TYPE.MAIT_NONE;
				if (!flag19)
				{
					bool bHide_state = SelfRole._inst.m_bHide_state;
					if (!bHide_state)
					{
						this.m_fThinkTime -= delta_time;
						bool flag20 = this.m_fThinkTime <= 0f;
						if (flag20)
						{
							this.m_fThinkTime = 0.05f;
							switch (this.m_eThinkType)
							{
							case AI_TYPE.MAIT_SICK:
								this.AI_Sick();
								break;
							case AI_TYPE.MAIT_ATTACK:
								this.AI_Attack(delta_time);
								break;
							}
						}
					}
				}
			}
		}
	}

	public void PlayHurtFront()
	{
		bool flag = this.m_moveAgent != null && this.m_moveAgent.remainingDistance < 1f;
		if (flag)
		{
			this.m_curAni.SetTrigger(EnumAni.ANI_HURT_FRONT);
		}
	}

	public void PlayHurtBack()
	{
		this.m_curAni.SetTrigger(EnumAni.ANI_HURT_BACK);
	}

	public void PlayFallFront()
	{
		this.m_curAni.SetTrigger(EnumAni.ANI_FALL_FRONT);
	}

	public void PlayFallBack()
	{
		this.m_curAni.SetTrigger(EnumAni.ANI_FALL_BACK);
	}
}
