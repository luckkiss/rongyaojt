using System;
using UnityEngine;

public class BtWallHurtPoint : MonoBehaviour
{
	public void OnTriggerEnter(Collider other)
	{
		HitData component = other.gameObject.GetComponent<HitData>();
		bool flag = component == null;
		if (!flag)
		{
			bool bOnlyHit = component.m_bOnlyHit;
			if (bOnlyHit)
			{
				other.enabled = false;
			}
			bool flag2 = component.m_unSkillID == 3006u;
			if (flag2)
			{
				component.HitAndStop(-1, true);
			}
			else
			{
				component.HitAndStop(-1, false);
			}
		}
	}
}
