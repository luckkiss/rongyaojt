using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HitData : MonoBehaviour
{
	public GameObject m_hdRootObj;

	public BaseRole m_CastRole;

	public Vector3 m_vBornerPos;

	public uint m_unSkillID = 0u;

	public int m_nDamage = 1;

	public int m_nLastHit = 0;

	public int m_nHittedCount = 3;

	public List<uint> m_unListRoleHitted;

	public List<uint> HittedisProfession;

	public List<uint> m_haveHittedList = new List<uint>();

	public int m_nHitType = 0;

	public PK_TYPE m_ePK_Type = PK_TYPE.PK_PKALL;

	public uint m_unPK_Param = 0u;

	public bool m_bSelfHit = false;

	public Animator m_aniTrack;

	public Animator m_aniFx;

	public bool m_bOnlyHit = false;

	public Color m_Color_Main = Color.gray;

	public Color m_Color_Rim = Color.red;

	public int m_nHurtFX = 0;

	public int m_nHurtSP_type = 0;

	public int m_nHurtSP_pow = 0;

	public Vector3 m_nHurtSP_pos;

	public int m_hurtNum = -1;

	public bool AddHittedRole(uint id, bool isProfession)
	{
		bool flag = this.m_unListRoleHitted == null;
		if (flag)
		{
			this.m_unListRoleHitted = new List<uint>();
			this.HittedisProfession = new List<uint>();
		}
		bool flag2 = this.m_hurtNum == -1;
		if (flag2)
		{
			this.m_hurtNum = ModelBase<Skill_a3Model>.getInstance().skilldic[(int)this.m_unSkillID].targetNum;
		}
		bool flag3 = this.m_hurtNum > 0;
		if (flag3)
		{
			if (isProfession)
			{
				this.HittedisProfession.Add(id);
			}
			this.m_unListRoleHitted.Add(id);
			this.m_hurtNum--;
			bool flag4 = this.m_hurtNum == 0;
			if (flag4)
			{
				bool flag5 = SelfRole._inst.m_LockRole != null;
				if (flag5)
				{
					bool flag6 = !this.m_unListRoleHitted.Contains(SelfRole._inst.m_LockRole.m_unIID);
					if (flag6)
					{
						float num = Vector3.Distance(SelfRole._inst.m_curModel.position, SelfRole._inst.m_LockRole.m_curModel.position);
						bool flag7 = num * 53.333f <= (float)ModelBase<Skill_a3Model>.getInstance().skilldic[(int)this.m_unSkillID].range;
						if (flag7)
						{
							uint item = this.m_unListRoleHitted[0];
							this.m_unListRoleHitted.Remove(item);
							this.m_unListRoleHitted.Add(SelfRole._inst.m_LockRole.m_unIID);
							if (isProfession)
							{
								this.HittedisProfession.Add(SelfRole._inst.m_LockRole.m_unIID);
							}
							bool flag8 = this.HittedisProfession.Contains(item);
							if (flag8)
							{
								this.HittedisProfession.Remove(item);
							}
						}
					}
					else
					{
						this.m_unListRoleHitted.Remove(SelfRole._inst.m_LockRole.m_unIID);
						this.m_unListRoleHitted.Add(SelfRole._inst.m_LockRole.m_unIID);
					}
				}
			}
		}
		return this.m_hurtNum >= this.m_unListRoleHitted.Count + this.HittedisProfession.Count;
	}

	public void HitAndStop(int ani = -1, bool shake = false)
	{
		bool flag = this.m_aniTrack != null;
		if (flag)
		{
			this.m_aniTrack.speed = 0f;
		}
		bool flag2 = this.m_aniFx != null;
		if (flag2)
		{
			bool flag3 = ani == -1;
			if (flag3)
			{
				this.m_aniFx.SetTrigger(EnumAni.ANI_T_FXDEAD);
			}
			else
			{
				this.m_aniFx.SetTrigger(ani);
			}
		}
		if (shake)
		{
			SceneCamera.cameraShake(0.4f, 20, 0.5f);
		}
	}

	private void Update()
	{
		bool flag = this.m_unListRoleHitted != null;
		if (flag)
		{
			PkmodelAdmin.RefreshList(this.m_unListRoleHitted, this.HittedisProfession);
			bool flag2 = this.m_unListRoleHitted != null;
			if (flag2)
			{
				int lockid = -1;
				bool flag3 = SelfRole._inst.m_LockRole != null && this.m_unListRoleHitted.Contains(SelfRole._inst.m_LockRole.m_unIID);
				if (flag3)
				{
					lockid = (int)SelfRole._inst.m_LockRole.m_unIID;
				}
				BaseProxy<BattleProxy>.getInstance().sendcast_target_skill(this.m_unSkillID, this.m_unListRoleHitted, this.m_nLastHit, lockid);
				this.m_unListRoleHitted = null;
				this.HittedisProfession = null;
			}
		}
	}
}
