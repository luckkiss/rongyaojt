using MuGame;
using System;
using UnityEngine;

public class MonHurtPoint : BaseHurt
{
	public MonsterRole m_monRole;

	private static uint lastUsedSkillId = 1u;

	private static int lastUsedSkillTargetNum = 0;

	private static float lastUseSkillTime;

	private static float SkillCheckTimeSpan = 0.05f;

	private static skill_a3Data skillData;

	public void OnTriggerEnter(Collider other)
	{
		bool flag = this.m_monRole.m_layer == EnumLayer.LM_COLLECT;
		if (!flag)
		{
			HitData component = other.gameObject.GetComponent<HitData>();
			bool flag2 = component == null;
			if (!flag2)
			{
				bool flag3 = component.m_CastRole is MonsterRole;
				if (!flag3)
				{
					bool flag4 = component.m_CastRole == SelfRole._inst && this.m_monRole is MS0000 && (long)((MS0000)this.m_monRole).owner_cid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid);
					if (!flag4)
					{
						bool flag5 = !this.m_monRole.canbehurt;
						if (!flag5)
						{
							bool flag6 = component.m_haveHittedList.Contains(this.m_monRole.m_unIID);
							if (!flag6)
							{
								component.m_haveHittedList.Add(this.m_monRole.m_unIID);
								bool flag7 = base.CanHited(this.m_monRole, component);
								if (flag7)
								{
									this.MonsterHurtEffectWork(other, component);
								}
							}
						}
					}
				}
			}
		}
	}

	private void MonsterHurtEffectWork(Collider other, HitData hd)
	{
		bool flag = !this.m_monRole.isfake;
		if (flag)
		{
			hd.m_nDamage = 0;
		}
		bool flag2 = hd.m_CastRole == SelfRole._inst && !this.m_monRole.isfake;
		if (flag2)
		{
			bool flag3 = hd.AddHittedRole(this.m_monRole.m_unIID, false);
			if (flag3)
			{
				this.m_monRole.setHitFlash(hd);
				bool flag4 = hd.m_nHurtFX > 0 && hd.m_nHurtFX < 10;
				if (flag4)
				{
					bool flag5 = hd.m_nHurtFX == 6;
					if (flag5)
					{
						Vector3 zero = Vector3.zero;
						zero.y += this.m_monRole.headOffset.y;
						GameObject gameObject = UnityEngine.Object.Instantiate(SceneTFX.m_HFX_Prefabs[hd.m_nHurtFX], zero, this.m_monRole.m_curModel.rotation) as GameObject;
						gameObject.transform.SetParent(this.m_monRole.m_curModel, false);
						UnityEngine.Object.Destroy(gameObject, 2f);
					}
					else
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate(SceneTFX.m_HFX_Prefabs[hd.m_nHurtFX], this.m_monRole.m_curModel.position, this.m_monRole.m_curModel.rotation) as GameObject;
						gameObject2.transform.SetParent(U3DAPI.FX_POOL_TF, false);
						UnityEngine.Object.Destroy(gameObject2, 2f);
					}
				}
				this.m_monRole.PlayHurtFront();
			}
		}
		else
		{
			this.m_monRole.onHurt(hd);
		}
		bool bOnlyHit = hd.m_bOnlyHit;
		if (bOnlyHit)
		{
			other.enabled = false;
		}
		bool flag6 = hd.m_unSkillID == 3003u;
		if (flag6)
		{
			hd.HitAndStop(EnumAni.ANI_T_FXDEAD1, false);
		}
		else
		{
			bool flag7 = hd.m_unSkillID == 3006u;
			if (!flag7)
			{
				hd.HitAndStop(-1, false);
			}
		}
		bool flag8 = hd.m_nHurtSP_type > 0;
		if (flag8)
		{
			bool flag9 = hd.m_nHurtSP_pow > this.m_monRole.m_nSPWeight * 3;
			if (flag9)
			{
				this.m_monRole.m_nSPLevel = 3;
			}
			else
			{
				bool flag10 = hd.m_nHurtSP_pow > this.m_monRole.m_nSPWeight * 2;
				if (flag10)
				{
					this.m_monRole.m_nSPLevel = 2;
				}
				else
				{
					bool flag11 = hd.m_nHurtSP_pow > this.m_monRole.m_nSPWeight;
					if (flag11)
					{
						this.m_monRole.m_nSPLevel = 1;
					}
					else
					{
						this.m_monRole.m_nSPLevel = 0;
					}
				}
			}
			bool flag12 = this.m_monRole.m_nSPLevel > 0;
			if (flag12)
			{
				bool flag13 = !this.m_monRole.isBoos;
				if (flag13)
				{
					bool flag14 = 1 == hd.m_nHurtSP_type || 13 == hd.m_nHurtSP_type;
					if (flag14)
					{
						this.m_monRole.m_fSkillSPup_Value = 0.25f;
						this.m_monRole.m_nSkillSP_up = 1;
					}
					bool flag15 = 11 == hd.m_nHurtSP_type || 13 == hd.m_nHurtSP_type;
					if (flag15)
					{
						this.m_monRole.m_fSkillSPfb_Value = 0.1f;
						this.m_monRole.m_nSkillSP_fb = 1;
						this.m_monRole.m_vSkillSP_dir = hd.m_CastRole.m_curModel.forward;
					}
					bool flag16 = 12 == hd.m_nHurtSP_type || 14 == hd.m_nHurtSP_type;
					if (flag16)
					{
						this.m_monRole.m_fSkillSPfb_Value = 2.5f;
						this.m_monRole.m_nSkillSP_fb = -41;
						this.m_monRole.m_vSkillSP_dir = hd.m_nHurtSP_pos;
					}
					bool flag17 = 21 == hd.m_nHurtSP_type;
					if (flag17)
					{
						this.m_monRole.m_fSkillSPfb_Value = 0.5f;
						this.m_monRole.m_nSkillSP_fb = -21;
						this.m_monRole.m_vSkillSP_dir = hd.m_CastRole.m_curModel.position;
					}
					bool flag18 = 31 == hd.m_nHurtSP_type;
					if (flag18)
					{
						this.m_monRole.setHitFlash(hd);
						this.m_monRole.m_fSkillSPfb_Value = 2.5f;
						this.m_monRole.m_nSkillSP_fb = -31;
						this.m_monRole.m_vSkillSP_dir = hd.m_CastRole.m_curModel.position;
					}
				}
			}
		}
	}
}
