using DG.Tweening;
using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Base_Event : MonoBehaviour
{
	public MonsterRole m_monRole;

	public static Dictionary<string, GameObject> bultList = new Dictionary<string, GameObject>();

	public void onSAI(float time)
	{
		this.m_monRole.SleepAI(time);
	}

	public void onSK(string param)
	{
		string[] array = param.Split(new char[]
		{
			','
		});
		bool flag = array.Length < 5;
		if (!flag)
		{
			this.m_monRole.FreezeAni(float.Parse(array[0]), float.Parse(array[1]));
			GameObject gameObject = Resources.Load<GameObject>("FX/monsterSFX/com_yujing/" + int.Parse(array[2]).ToString());
			bool flag2 = gameObject != null;
			if (flag2)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, base.transform.rotation) as GameObject;
				float num = float.Parse(array[4]);
				gameObject2.transform.localScale = new Vector3(num, num, num);
				gameObject2.transform.SetParent(U3DAPI.FX_POOL_TF, false);
				UnityEngine.Object.Destroy(gameObject2, float.Parse(array[0]));
			}
			int num2 = int.Parse(array[3]);
			bool flag3 = num2 > 0;
			if (flag3)
			{
				GameObject original = Resources.Load<GameObject>("FX/monsterSFX/com/FX_monster_" + num2.ToString());
				GameObject gameObject3 = UnityEngine.Object.Instantiate(original, base.transform.position, base.transform.rotation) as GameObject;
				gameObject3.transform.SetParent(U3DAPI.FX_POOL_TF, false);
				UnityEngine.Object.Destroy(gameObject3, float.Parse(array[0]));
			}
		}
	}

	private void onTFX(int id)
	{
		bool flag = id < 100;
		if (flag)
		{
			bool flag2 = id >= 11 && id <= 20;
			Vector3 position;
			if (flag2)
			{
				position = this.m_monRole.m_LeftFoot.position;
			}
			else
			{
				bool flag3 = id >= 21 && id <= 30;
				if (flag3)
				{
					position = this.m_monRole.m_RightFoot.position;
				}
				else
				{
					bool flag4 = id >= 31 && id <= 40;
					if (flag4)
					{
						position = this.m_monRole.m_LeftHand.position;
					}
					else
					{
						bool flag5 = id >= 41 && id <= 50;
						if (flag5)
						{
							position = this.m_monRole.m_RightHand.position;
						}
						else
						{
							position = base.transform.position;
						}
					}
				}
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(SceneTFX.m_TFX_Prefabs[id % 10], position, base.transform.rotation) as GameObject;
			gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
			UnityEngine.Object.Destroy(gameObject, 1f);
		}
	}

	private void onNewbie(string str)
	{
		NewbieTeachMgr.getInstance().add(str, -1);
	}

	private void onFX(int id)
	{
		SceneFXMgr.Instantiate("FX/FX_" + id.ToString(), base.transform.position, base.transform.rotation, 2f);
	}

	public void onSFX_EFF(string id)
	{
		bool flag = !MonsterMgr._inst.dMonEff.ContainsKey(id);
		if (!flag)
		{
			MonEffData monEffData = MonsterMgr._inst.dMonEff[id];
			Quaternion rotation = base.transform.rotation;
			Quaternion rotation2 = Quaternion.Euler(0f, monEffData.rotation, 0f);
			Vector3 position = base.transform.position + rotation2 * base.transform.forward * monEffData.f;
			position.y += monEffData.y;
			bool romote = monEffData.romote;
			if (romote)
			{
				bool flag2 = this.m_monRole.m_LockRole != null && this.m_monRole.m_LockRole.m_curModel != null;
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
							bool flag6 = bult.transform != null && bult.transform.FindChild("t") != null;
							if (flag6)
							{
								Transform transform = real_track.FindChild("f");
								bool flag7 = transform != null;
								if (flag7)
								{
									transform.GetComponent<Animator>().SetTrigger(EnumAni.ANI_T_FXDEAD);
								}
								UnityEngine.Object.Destroy(bult, 2f);
							}
						});
					}
				}
			}
			else
			{
				bool lockpos = monEffData.Lockpos;
				if (lockpos)
				{
					SceneFXMgr.Instantiate(monEffData.file, this.m_monRole.m_LockRole.m_curModel.position, rotation, 4f);
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

	public void onSound(string path)
	{
		bool flag = this.m_monRole != null;
		if (flag)
		{
			MediaClient.instance.PlaySoundUrl("audio/monster/" + path, false, null);
		}
	}

	public void onJump(float x, float y)
	{
		float x2 = x / 53.333f;
		float z = y / 53.333f;
		Vector3 position = new Vector3(x2, base.transform.position.y, z);
		bool flag = GameRoomMgr.getInstance().curRoom == GameRoomMgr.getInstance().dRooms[3342u];
		if (!flag)
		{
			NavMeshHit navMeshHit;
			NavMesh.SamplePosition(position, out navMeshHit, 100f, this.m_monRole.m_layer);
			position = navMeshHit.position;
		}
		float num = Vector3.Distance(base.transform.position, position);
		this.m_monRole.SetDestPos(position);
		base.transform.DOJump(position, 0.2f * num + position.y, 1, 0.01f, false);
	}

	public void onShake(string param)
	{
		bool flag = this.m_monRole == null;
		if (!flag)
		{
			string[] array = param.Split(new char[]
			{
				','
			});
			bool flag2 = array.Length < 3;
			if (!flag2)
			{
				SceneCamera.cameraShake(float.Parse(array[0]), int.Parse(array[1]), float.Parse(array[2]));
			}
		}
	}

	public void onBorned()
	{
		this.m_monRole.onBorned();
	}

	public void onDeadEnd()
	{
		this.m_monRole.onDeadEnd();
	}

	public void onShow(int id)
	{
	}
}
