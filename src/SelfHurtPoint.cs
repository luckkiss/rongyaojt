using MuGame;
using System;
using UnityEngine;

public class SelfHurtPoint : BaseHurt
{
	public ProfessionRole m_selfRole;

	public void OnTriggerEnter(Collider other)
	{
		HitData component = other.gameObject.GetComponent<HitData>();
		bool flag = component == null;
		if (!flag)
		{
			bool flag2 = component.m_haveHittedList.Contains(this.m_selfRole.m_unIID);
			if (!flag2)
			{
				component.m_haveHittedList.Add(this.m_selfRole.m_unIID);
				bool flag3 = ModelBase<PlayerModel>.getInstance().up_lvl >= 1u;
				if (flag3)
				{
					bool flag4 = base.CanHited(this.m_selfRole, component);
					if (flag4)
					{
						bool bOnlyHit = component.m_bOnlyHit;
						if (bOnlyHit)
						{
							other.enabled = false;
						}
						bool flag5 = component.m_unSkillID == 3003u;
						if (flag5)
						{
							component.HitAndStop(EnumAni.ANI_T_FXDEAD1, false);
						}
						else
						{
							bool flag6 = component.m_unSkillID == 3006u;
							if (!flag6)
							{
								component.HitAndStop(-1, false);
							}
						}
						bool flag7 = component.m_CastRole != null && component.m_CastRole.isfake;
						if (flag7)
						{
							this.m_selfRole.onHurt(component);
						}
						this.m_selfRole.ShowHurtFX(component.m_nHurtFX);
						bool flag8 = component.m_CastRole is ProfessionRole && ModelBase<PlayerModel>.getInstance().now_pkState == 0;
						if (flag8)
						{
							bool flag9 = !component.m_CastRole.isDead || component.m_CastRole != null || !this.m_selfRole.isDead;
							if (flag9)
							{
								a3_expbar.instance.ShowAgainst(component.m_CastRole);
							}
						}
					}
				}
			}
		}
	}
}
