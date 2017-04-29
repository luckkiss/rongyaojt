using System;
using UnityEngine;

public class M10001_Dyzhs_Event : Monster_Base_Event
{
	private void onSFX(int id)
	{
		SceneFXMgr.Instantiate("FX/monsterSFX/10001/SFX_" + id.ToString(), base.transform.position, base.transform.rotation, 2f);
	}

	private void Bullet_1()
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

	private void Bullet_2()
	{
	}

	public void onBullet(int type)
	{
		if (type != 1)
		{
			if (type != 2)
			{
				Debug.Log("未知的子弹类型：" + type);
			}
			else
			{
				this.Bullet_2();
			}
		}
		else
		{
			this.Bullet_1();
		}
	}
}
