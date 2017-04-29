using System;
using UnityEngine;

namespace MuGame
{
	internal class M10002_Srg_Event : Monster_Base_Event
	{
		public void onBullet(int type)
		{
			Vector3 position = base.transform.position + base.transform.forward * 2f;
			position.y += 1f;
			bool isfake = this.m_monRole.isfake;
			if (isfake)
			{
				HitData hitData = this.m_monRole.BuildBullet(1u, 0.125f, SceneTFX.m_Bullet_Prefabs[1], position, base.transform.rotation);
				hitData.m_nDamage = 1;
			}
		}
	}
}
