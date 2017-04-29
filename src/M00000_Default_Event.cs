using System;
using UnityEngine;

public class M00000_Default_Event : Monster_Base_Event
{
	private void onSFX(int id)
	{
		SceneFXMgr.Instantiate("FX/monsterSFX/com/FX_monster_" + id.ToString(), base.transform.position, base.transform.rotation, 6f);
	}

	private void Bullet_1()
	{
	}

	private void Bullet_2()
	{
	}

	public void onBullet(int type)
	{
		bool flag = 1 == type;
		if (flag)
		{
			Vector3 position = base.transform.position + base.transform.forward * 2f;
			position.y += 1f;
			bool isfake = this.m_monRole.isfake;
			if (isfake)
			{
				HitData hitData = this.m_monRole.BuildBullet(1u, 2f, SceneTFX.m_Bullet_Prefabs[1], position, base.transform.rotation);
			}
		}
	}
}
