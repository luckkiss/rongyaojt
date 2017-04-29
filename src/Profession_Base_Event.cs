using MuGame;
using System;
using UnityEngine;

public class Profession_Base_Event : MonoBehaviour
{
	public ProfessionRole m_linkProfessionRole;

	public MonsterPlayer ohter_linkProfessionRole;

	private void onNT(float time)
	{
		bool flag = this.m_linkProfessionRole != null;
		if (flag)
		{
			this.m_linkProfessionRole.m_fSkillShowTime = time;
		}
		bool flag2 = this.ohter_linkProfessionRole != null;
		if (flag2)
		{
			this.ohter_linkProfessionRole.m_fSkillShowTime = time;
		}
	}

	private void onND(float time)
	{
		bool flag = this.m_linkProfessionRole != null;
		if (flag)
		{
			this.m_linkProfessionRole.m_fAttackCount = time;
		}
		bool flag2 = this.ohter_linkProfessionRole != null;
		if (flag2)
		{
			this.ohter_linkProfessionRole.m_fAttackCount = time;
		}
	}

	private void onSL()
	{
		bool flag = this.m_linkProfessionRole != null;
		if (flag)
		{
			this.m_linkProfessionRole.m_nKeepSkillCount++;
		}
		bool flag2 = this.ohter_linkProfessionRole != null;
		if (flag2)
		{
			this.ohter_linkProfessionRole.m_nKeepSkillCount++;
		}
	}

	public void onWing(float time)
	{
		bool flag = this.m_linkProfessionRole != null;
		if (flag)
		{
			this.m_linkProfessionRole.FlyWing(time);
		}
		bool flag2 = this.ohter_linkProfessionRole != null;
		if (flag2)
		{
			this.ohter_linkProfessionRole.FlyWing(time);
		}
	}

	private void onTFX(int id)
	{
		bool flag = id < 100;
		if (flag)
		{
			bool flag2 = this.m_linkProfessionRole.m_roleDta.m_WindID > 0;
			if (!flag2)
			{
				bool flag3 = id >= 11 && id <= 20;
				Vector3 position;
				if (flag3)
				{
					position = this.m_linkProfessionRole.m_LeftFoot.position;
				}
				else
				{
					bool flag4 = id >= 21 && id <= 30;
					if (flag4)
					{
						position = this.m_linkProfessionRole.m_RightFoot.position;
					}
					else
					{
						bool flag5 = id >= 31 && id <= 40;
						if (flag5)
						{
							position = this.m_linkProfessionRole.m_LeftHand.position;
						}
						else
						{
							bool flag6 = id >= 41 && id <= 50;
							if (flag6)
							{
								position = this.m_linkProfessionRole.m_RightHand.position;
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
	}

	private void onFX(int id)
	{
	}

	public void onBullet(int id)
	{
	}

	public void onShake(string param)
	{
		bool flag = this.m_linkProfessionRole != null;
		if (flag)
		{
			bool flag2 = !this.m_linkProfessionRole.m_isMain;
			if (flag2)
			{
				return;
			}
		}
		string[] array = param.Split(new char[]
		{
			','
		});
		bool flag3 = array.Length < 3;
		if (!flag3)
		{
			SceneCamera.cameraShake(float.Parse(array[0]), int.Parse(array[1]), float.Parse(array[2]));
		}
	}

	public void onSound(string path)
	{
		bool flag = this.m_linkProfessionRole != null;
		if (flag)
		{
			MediaClient.instance.PlaySoundUrl("audio/skill/" + path, false, null);
		}
	}

	public void onShow(int id)
	{
		bool flag = this.m_linkProfessionRole != null;
		if (flag)
		{
			this.m_linkProfessionRole.ShowAll();
		}
	}

	public void onHide(int id)
	{
		bool flag = this.m_linkProfessionRole != null;
		if (flag)
		{
			this.m_linkProfessionRole.HideAll();
		}
	}

	private void onSFX(int id)
	{
	}
}
