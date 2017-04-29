using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class FollowBullet_Mgr
{
	public static uint m_unFollowBulletMakeID = 0u;

	public static Dictionary<uint, OneFollowBlt> m_mapFollowBullet = new Dictionary<uint, OneFollowBlt>();

	public static void AddBullet(BaseRole locker, HitData bullet, float time)
	{
		FollowBullet_Mgr.m_unFollowBulletMakeID += 1u;
		OneFollowBlt oneFollowBlt = new OneFollowBlt();
		oneFollowBlt.id = FollowBullet_Mgr.m_unFollowBulletMakeID;
		oneFollowBlt.beginpos = bullet.transform.position;
		oneFollowBlt.locker = locker;
		oneFollowBlt.blt_hd = bullet;
		oneFollowBlt.costtime = 0f;
		oneFollowBlt.maxtime = time;
		Transform transform = bullet.transform.FindChild("t");
		bool flag = transform != null;
		if (flag)
		{
			oneFollowBlt.aniTrack = transform.GetComponent<Animator>();
			transform.gameObject.layer = EnumLayer.LM_BT_FIGHT;
			Transform transform2 = transform.FindChild("f");
			bool flag2 = transform2 != null;
			if (flag2)
			{
				oneFollowBlt.aniFx = transform2.GetComponent<Animator>();
			}
		}
		FollowBullet_Mgr.m_mapFollowBullet.Add(FollowBullet_Mgr.m_unFollowBulletMakeID, oneFollowBlt);
	}

	public static void clear()
	{
		FollowBullet_Mgr.m_mapFollowBullet.Clear();
	}

	public static void FrameMove(float fdt)
	{
		List<uint> list = new List<uint>();
		foreach (OneFollowBlt current in FollowBullet_Mgr.m_mapFollowBullet.Values)
		{
			try
			{
				float num = current.costtime / current.maxtime;
				current.costtime += fdt;
				bool flag = num > 1f;
				if (flag)
				{
					bool flag2 = current.aniTrack != null;
					if (flag2)
					{
						current.aniTrack.speed = 0f;
					}
					bool flag3 = current.aniFx != null;
					if (flag3)
					{
						current.aniFx.SetTrigger(EnumAni.ANI_T_FXDEAD);
					}
					UnityEngine.Object.Destroy(current.blt_hd.m_hdRootObj, 1f);
					list.Add(current.id);
					bool flag4 = current.locker is MonsterRole;
					if (flag4)
					{
						MonsterRole monsterRole = current.locker as MonsterRole;
						bool isfake = monsterRole.isfake;
						if (isfake)
						{
							monsterRole.onHurt(current.blt_hd);
						}
						else
						{
							bool flag5 = current.blt_hd.m_CastRole == SelfRole._inst;
							if (flag5)
							{
								List<uint> list2 = new List<uint>();
								list2.Add(monsterRole.m_unIID);
								int lockid = -1;
								bool flag6 = SelfRole._inst.m_LockRole != null && SelfRole._inst.m_LockRole.m_unIID == monsterRole.m_unIID;
								if (flag6)
								{
									lockid = (int)SelfRole._inst.m_LockRole.m_unIID;
								}
								BaseProxy<BattleProxy>.getInstance().sendcast_target_skill(current.blt_hd.m_unSkillID, list2, 0, lockid);
							}
						}
					}
				}
				else
				{
					Vector3 position = current.locker.m_curModel.position;
					position.y += 1.5f;
					Vector3 position2 = current.beginpos + (position - current.beginpos) * num;
					current.blt_hd.m_hdRootObj.transform.position = position2;
				}
			}
			catch (Exception var_16_1FB)
			{
				list.Add(current.id);
				break;
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			FollowBullet_Mgr.m_mapFollowBullet.Remove(list[i]);
		}
	}
}
