using DG.Tweening;
using MuGame;
using System;
using UnityEngine;

public class MS0000_Default_Event : Monster_Base_Event
{
	public string effect;

	public Vector3 vec;

	private void onSFX(string id)
	{
		SceneFXMgr.Instantiate("FX/zhsFX/slef_FX/" + id, base.transform.position, base.transform.rotation, 1.5f);
	}

	public void onBullet(int type)
	{
	}

	public void onSFX_EFF()
	{
		bool flag = this.effect == null || !MonsterMgr._inst.dMonEff.ContainsKey(this.effect);
		if (!flag)
		{
			MonEffData monEffData = MonsterMgr._inst.dMonEff[this.effect];
			Quaternion rotation = base.transform.rotation;
			Quaternion rotation2 = default(Quaternion);
			rotation2.y = monEffData.rotation / 90f;
			rotation2.w = 1f;
			rotation.y += monEffData.rotation / 90f;
			Vector3 position = base.transform.position + rotation2 * base.transform.forward * monEffData.f;
			position.y += monEffData.y;
			bool romote = monEffData.romote;
			if (romote)
			{
				bool flag2 = this.m_monRole.m_LockRole != null;
				if (flag2)
				{
					this.m_monRole.TurnToRole(this.m_monRole.m_LockRole, false);
					GameObject original = Resources.Load<GameObject>(monEffData.file);
					GameObject bult = UnityEngine.Object.Instantiate(original, position, rotation) as GameObject;
					bult.transform.SetParent(U3DAPI.FX_POOL_TF, false);
					Transform real_track = bult.transform.FindChild("t");
					bool flag3 = real_track != null;
					if (flag3)
					{
						bool flag4 = real_track.GetComponent<Animator>() != null;
						if (flag4)
						{
							real_track.GetComponent<Animator>().enabled = false;
						}
						real_track.gameObject.layer = EnumLayer.LM_BT_FIGHT;
						float num = Vector3.Distance(base.transform.position, this.m_monRole.m_LockRole.m_curModel.position);
						Vector3 position2 = this.m_monRole.m_LockRole.m_curModel.position;
						position2.y += this.m_monRole.m_LockRole.headOffset.y * 3f / 4f;
						Tweener t = bult.transform.DOLocalMove(position2, num * 0.03f / monEffData.speed, false);
						t.SetUpdate(true);
						switch (UnityEngine.Random.Range(0, 4))
						{
						case 1:
							t.SetEase(Ease.InQuad);
							break;
						case 2:
							t.SetEase(Ease.InCirc);
							break;
						case 3:
							t.SetEase(Ease.InCubic);
							break;
						case 4:
							t.SetEase(Ease.InExpo);
							break;
						}
						t.OnComplete(delegate
						{
							Transform transform = real_track.FindChild("f");
							bool flag6 = transform != null;
							if (flag6)
							{
								transform.GetComponent<Animator>().SetTrigger(EnumAni.ANI_T_FXDEAD);
							}
							UnityEngine.Object.Destroy(bult, 2f);
						});
					}
				}
			}
			else
			{
				bool lockpos = monEffData.Lockpos;
				if (lockpos)
				{
					SceneFXMgr.Instantiate(monEffData.file, this.vec, rotation, 4f);
				}
				else
				{
					SceneFXMgr.Instantiate(monEffData.file, position, rotation, 4f);
				}
			}
			bool flag5 = monEffData.sound != "null";
			if (flag5)
			{
				MediaClient.instance.PlaySoundUrl("audio/eff/" + monEffData.sound, false, null);
			}
		}
	}
}
