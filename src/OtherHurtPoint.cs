using System;
using UnityEngine;

public class OtherHurtPoint : BaseHurt
{
	public ProfessionRole m_otherRole;

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log("和其他玩家 发生 碰撞 " + other.name);
		HitData component = other.gameObject.GetComponent<HitData>();
		bool flag = component == null;
		if (!flag)
		{
			bool flag2 = component.m_haveHittedList.Contains(this.m_otherRole.m_unIID);
			if (!flag2)
			{
				component.m_haveHittedList.Add(this.m_otherRole.m_unIID);
				bool flag3 = OtherPlayerMgr._inst.m_mapOtherPlayer[this.m_otherRole.m_unIID].zhuan >= 1;
				if (flag3)
				{
					bool flag4 = base.CanHited(this.m_otherRole, component);
					if (flag4)
					{
						other.enabled = false;
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
						bool flag7 = component.m_CastRole == SelfRole._inst;
						if (flag7)
						{
							component.AddHittedRole(this.m_otherRole.m_unIID, true);
						}
						else
						{
							this.m_otherRole.onHurt(component);
						}
						this.m_otherRole.ShowHurtFX(component.m_nHurtFX);
					}
				}
			}
		}
	}
}
