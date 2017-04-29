using DG.Tweening;
using System;
using UnityEngine;

public class P2Warrior_Event : Profession_Base_Event
{
	public static GameObject WARRIOR_B1;

	public string fx_2003_name = "skill_2003";

	private void onSFX(int id)
	{
		bool flag = this.m_linkProfessionRole.getShowSkillEff() == 3;
		if (!flag)
		{
			bool flag2 = id == 2003;
			if (flag2)
			{
				bool flag3 = base.transform.FindChild(this.fx_2003_name) != null;
				if (!flag3)
				{
					base.CancelInvoke("SFX_2003_hide");
					base.Invoke("SFX_2003_hide", 3.5f);
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX1);
					UnityEngine.Object.Destroy(gameObject, 5f);
					this.fx_2003_name = gameObject.name;
					gameObject.transform.SetParent(base.transform, false);
				}
			}
			else
			{
				this.SFX_2003_hide();
				SceneFXMgr.Instantiate("FX/warrior/SFX_" + id.ToString(), base.transform.position, base.transform.rotation, 4f);
			}
		}
	}

	public void SFX_2003_hide()
	{
		bool flag = base.transform.FindChild(this.fx_2003_name) != null;
		if (flag)
		{
			base.transform.FindChild(this.fx_2003_name).gameObject.SetActive(false);
		}
	}

	public new void onBullet(int id)
	{
		bool flag = this.m_linkProfessionRole.getShowSkillEff() == 3 || this.m_linkProfessionRole.getShowSkillEff() == 2;
		if (!flag)
		{
			bool flag2 = id == 1;
			if (flag2)
			{
				Vector3 position = base.transform.position + base.transform.forward * 1.5f;
				HitData hitData = this.m_linkProfessionRole.Build_PRBullet(2001u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position, base.transform.rotation);
				hitData.m_nDamage = 108;
				hitData.m_Color_Main = Color.gray;
				hitData.m_Color_Rim = Color.white;
				hitData.m_nHurtFX = 2;
			}
			bool flag3 = id == 2;
			if (flag3)
			{
				Vector3 position2 = base.transform.position + base.transform.forward * 1.5f;
				HitData hitData2 = this.m_linkProfessionRole.Build_PRBullet(2001u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position2, base.transform.rotation);
				hitData2.m_nDamage = 128;
				hitData2.m_Color_Main = Color.gray;
				hitData2.m_Color_Rim = Color.white;
				hitData2.m_nHurtFX = 2;
			}
			bool flag4 = id == 3;
			if (flag4)
			{
				Vector3 position3 = base.transform.position + base.transform.forward * 2.5f;
				HitData hitData3 = this.m_linkProfessionRole.Build_PRBullet(2001u, 0.125f, SceneTFX.m_Bullet_Prefabs[4], position3, base.transform.rotation);
				hitData3.m_nDamage = 188;
				hitData3.m_Color_Main = Color.gray;
				hitData3.m_Color_Rim = Color.white;
				hitData3.m_nHurtFX = 2;
				hitData3.m_nHurtSP_type = 11;
				hitData3.m_nHurtSP_pow = 3;
				hitData3.m_nLastHit = 1;
			}
			bool flag5 = id == 4;
			if (flag5)
			{
				Vector3 position4 = base.transform.position;
				HitData hitData4 = this.m_linkProfessionRole.Build_PRBullet(2003u, 0.125f, SceneTFX.m_Bullet_Prefabs[4], position4, base.transform.rotation);
				hitData4.m_nDamage = 88;
				hitData4.m_Color_Main = Color.red;
				hitData4.m_Color_Rim = Color.red;
				hitData4.m_nHurtFX = 3;
			}
			bool flag6 = id == 21;
			if (flag6)
			{
				Vector3 position5 = base.transform.position + base.transform.forward * 4f;
				HitData hitData5 = this.m_linkProfessionRole.Build_PRBullet(2004u, 0.2f, SceneTFX.m_Bullet_Prefabs[6], position5, base.transform.rotation);
				hitData5.m_nDamage = 388;
				hitData5.m_Color_Main = Color.red;
				hitData5.m_Color_Rim = Color.red;
				hitData5.m_nHurtSP_type = 1;
				hitData5.m_nHurtSP_pow = 4;
			}
			bool flag7 = 13 == id;
			if (flag7)
			{
				bool flag8 = this.m_linkProfessionRole.m_LockRole != null;
				if (flag8)
				{
					Vector3 position6 = base.transform.position + base.transform.forward * 0.5f;
					position6.y += 1.25f;
					HitData bullet = this.m_linkProfessionRole.Build_PRBullet(2004u, 0f, P2Warrior_Event.WARRIOR_B1, position6, base.transform.rotation);
					FollowBullet_Mgr.AddBullet(this.m_linkProfessionRole.m_LockRole, bullet, 0.8f);
				}
			}
			bool flag9 = 141 == id;
			if (flag9)
			{
				Vector3 position7 = base.transform.position + base.transform.forward * 3f;
				HitData hitData6 = this.m_linkProfessionRole.Build_PRBullet(2002u, 0.3f, SceneTFX.m_Bullet_Prefabs[4], position7, base.transform.rotation);
				hitData6.m_nDamage = 108;
				hitData6.m_Color_Main = Color.gray;
				hitData6.m_Color_Rim = Color.white;
				hitData6.m_nHurtFX = 2;
			}
			bool flag10 = 142 == id;
			if (flag10)
			{
				Vector3 position8 = base.transform.position + base.transform.forward * 6f;
				HitData hitData7 = this.m_linkProfessionRole.Build_PRBullet(2002u, 0.3f, SceneTFX.m_Bullet_Prefabs[4], position8, base.transform.rotation);
				hitData7.m_nDamage = 88;
				hitData7.m_Color_Main = Color.gray;
				hitData7.m_Color_Rim = Color.white;
				hitData7.m_nHurtFX = 2;
			}
			bool flag11 = 143 == id;
			if (flag11)
			{
				Vector3 position9 = base.transform.position + base.transform.forward * 4f;
				HitData hitData8 = this.m_linkProfessionRole.Build_PRBullet(2002u, 0.2f, SceneTFX.m_Bullet_Prefabs[6], position9, base.transform.rotation);
				hitData8.m_nDamage = 188;
				hitData8.m_Color_Main = Color.gray;
				hitData8.m_Color_Rim = Color.white;
				hitData8.m_nHurtFX = 2;
				hitData8.m_nHurtSP_type = 21;
				hitData8.m_nHurtSP_pow = 4;
			}
			bool flag12 = 2009 == id;
			if (flag12)
			{
				Vector3 position10 = base.transform.position + base.transform.forward * 3f;
				HitData hitData9 = this.m_linkProfessionRole.Build_PRBullet(2009u, 0.2f, SceneTFX.m_Bullet_Prefabs[5], position10, base.transform.rotation);
				hitData9.m_nDamage = 88;
				hitData9.m_Color_Main = Color.red;
				hitData9.m_Color_Rim = Color.red;
				hitData9.m_nHurtSP_type = 11;
				hitData9.m_nHurtSP_pow = 4;
				hitData9.m_nHurtFX = 6;
			}
			bool flag13 = 2006 == id;
			if (flag13)
			{
				Vector3 position11 = base.transform.position + base.transform.forward * 3f;
				HitData hitData10 = this.m_linkProfessionRole.Build_PRBullet(2006u, 0.2f, SceneTFX.m_Bullet_Prefabs[6], position11, base.transform.rotation);
				hitData10.m_nDamage = 88;
				hitData10.m_Color_Main = Color.red;
				hitData10.m_Color_Rim = Color.red;
				hitData10.m_nHurtSP_type = 11;
				hitData10.m_nHurtSP_pow = 4;
				hitData10.m_nHurtFX = 6;
			}
			bool flag14 = 2007 == id;
			if (flag14)
			{
				Vector3 position12 = base.transform.position + base.transform.forward * 1f;
				HitData hitData11 = this.m_linkProfessionRole.Build_PRBullet(2007u, 0.3f, SceneTFX.m_Bullet_Prefabs[4], position12, base.transform.rotation);
				hitData11.m_nDamage = 88;
				hitData11.m_Color_Main = Color.red;
				hitData11.m_Color_Rim = Color.red;
				hitData11.m_nHurtSP_type = 11;
				hitData11.m_nHurtSP_pow = 10;
				hitData11.m_nHurtFX = 3;
			}
		}
	}

	public void onJump(int id)
	{
		bool flag = 2009 == id;
		if (flag)
		{
			NavMeshPath path = new NavMeshPath();
			NavMeshAgent component = base.transform.GetComponent<NavMeshAgent>();
			Vector3 position = base.transform.position;
			bool flag2 = this.m_linkProfessionRole.m_LockRole != null;
			if (flag2)
			{
				float num = Vector3.Distance(position, this.m_linkProfessionRole.m_LockRole.m_curModel.position);
				int num2 = 1;
				while ((float)num2 < num)
				{
					Vector3 targetPosition = this.m_linkProfessionRole.m_LockRole.m_curModel.position - base.transform.forward * (float)num2;
					targetPosition = base.transform.position + base.transform.forward * (float)num2;
					bool flag3 = component.CalculatePath(targetPosition, path);
					if (!flag3)
					{
						break;
					}
					num2++;
				}
				bool flag4 = (float)num2 < num;
				if (flag4)
				{
					Vector3 vector = position + base.transform.forward * (float)num2;
					float num3 = Vector3.Distance(position, vector);
					base.transform.DOJump(vector, 0.2f * num3, 1, 0.07f * num3, false);
				}
				else
				{
					int num4 = 3;
					while (num4 <= 3 && num4 >= 0)
					{
						Vector3 vector2 = this.m_linkProfessionRole.m_LockRole.m_curModel.position - base.transform.forward * (float)num4;
						bool flag5 = component.CalculatePath(vector2, path);
						if (flag5)
						{
							float num5 = Vector3.Distance(position, vector2);
							base.transform.DOJump(vector2, 0.2f * num5 + vector2.y, 1, 0.07f * num5, false);
							break;
						}
						num4--;
					}
				}
			}
			else
			{
				int i = 1;
				Vector3 vector3 = base.transform.position + base.transform.forward * (float)i;
				while (i < 10)
				{
					vector3 = base.transform.position + base.transform.forward * (float)i;
					bool flag6 = component.CalculatePath(vector3, path);
					if (!flag6)
					{
						break;
					}
					i++;
				}
				bool flag7 = i > 3;
				if (flag7)
				{
					base.transform.DOJump(vector3, 0.2f * (float)i + vector3.y, 1, 0.07f * (float)i, false);
				}
			}
		}
		bool flag8 = id == 2006;
		if (flag8)
		{
			NavMeshPath path2 = new NavMeshPath();
			NavMeshAgent component2 = base.transform.GetComponent<NavMeshAgent>();
			Vector3 position2 = base.transform.position;
			bool flag9 = this.m_linkProfessionRole.m_LockRole != null;
			if (flag9)
			{
				float num6 = Vector3.Distance(position2, this.m_linkProfessionRole.m_LockRole.m_curModel.position);
				int num7 = 1;
				while ((float)num7 < num6)
				{
					Vector3 targetPosition2 = this.m_linkProfessionRole.m_LockRole.m_curModel.position - base.transform.forward * (float)num7;
					targetPosition2 = base.transform.position + base.transform.forward * (float)num7;
					bool flag10 = component2.CalculatePath(targetPosition2, path2);
					if (!flag10)
					{
						break;
					}
					num7++;
				}
				bool flag11 = (float)num7 < num6;
				if (flag11)
				{
					Vector3 vector4 = position2 + base.transform.forward * (float)num7;
					float num8 = Vector3.Distance(position2, vector4);
					base.transform.DOJump(vector4, position2.y, 1, 0.07f * num8, false);
				}
				else
				{
					int num9 = 3;
					while (num9 <= 3 && num9 >= 0)
					{
						Vector3 vector5 = this.m_linkProfessionRole.m_LockRole.m_curModel.position - base.transform.forward * (float)num9;
						bool flag12 = component2.CalculatePath(vector5, path2);
						if (flag12)
						{
							float num10 = Vector3.Distance(position2, vector5);
							base.transform.DOJump(vector5, vector5.y, 1, 0.07f * num10, false);
							break;
						}
						num9--;
					}
				}
			}
			else
			{
				int j = 1;
				Vector3 vector6 = base.transform.position + base.transform.forward * (float)j;
				while (j < 10)
				{
					vector6 = base.transform.position + base.transform.forward * (float)j;
					bool flag13 = component2.CalculatePath(vector6, path2);
					if (!flag13)
					{
						break;
					}
					j++;
				}
				bool flag14 = j > 3;
				if (flag14)
				{
					base.transform.DOJump(vector6, vector6.y, 1, 0.07f * (float)j, false);
				}
			}
		}
	}
}
