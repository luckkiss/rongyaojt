using DG.Tweening;
using System;
using UnityEngine;

public class P5Assassin_Event : Profession_Base_Event
{
	public static GameObject ASSASSIN_S1;

	public static GameObject ASSASSIN_S2;

	public string fx_5003_name = "skill_5003";

	private void onSFX(int id)
	{
		bool flag = this.m_linkProfessionRole.getShowSkillEff() == 3;
		if (!flag)
		{
			bool flag2 = id == 5003;
			if (flag2)
			{
				bool flag3 = base.transform.FindChild(this.fx_5003_name) != null;
				if (!flag3)
				{
					base.CancelInvoke("SFX_5003_hide");
					base.Invoke("SFX_5003_hide", 2.5f);
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P5Assassin.ASSASSIN_SFX1);
					UnityEngine.Object.Destroy(gameObject, 4f);
					this.fx_5003_name = gameObject.name;
					gameObject.transform.SetParent(base.transform, false);
				}
			}
			else
			{
				this.SFX_5003_hide();
				SceneFXMgr.Instantiate("FX/assa/SFX_" + id.ToString(), base.transform.position, base.transform.rotation, 4f);
			}
		}
	}

	public void SFX_5003_hide()
	{
		bool flag = base.transform.FindChild(this.fx_5003_name) != null;
		if (flag)
		{
			base.transform.FindChild(this.fx_5003_name).gameObject.SetActive(false);
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
				position.y += 1f;
				HitData hitData = this.m_linkProfessionRole.Build_PRBullet(5001u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position, base.transform.rotation);
				hitData.m_Color_Main = Color.gray;
				hitData.m_Color_Rim = Color.white;
				hitData.m_nHurtFX = 2;
			}
			bool flag3 = id == 2;
			if (flag3)
			{
				Vector3 position2 = base.transform.position + base.transform.forward * 1.5f;
				position2.y += 1f;
				HitData hitData2 = this.m_linkProfessionRole.Build_PRBullet(5001u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position2, base.transform.rotation);
				hitData2.m_Color_Main = Color.gray;
				hitData2.m_Color_Rim = Color.white;
				hitData2.m_nHurtSP_type = 1;
				hitData2.m_nHurtSP_pow = 2;
				hitData2.m_nHurtFX = 2;
			}
			bool flag4 = id == 3;
			if (flag4)
			{
				Vector3 position3 = base.transform.position + base.transform.forward * 2.5f;
				position3.y += 1f;
				HitData hitData3 = this.m_linkProfessionRole.Build_PRBullet(5001u, 0.125f, SceneTFX.m_Bullet_Prefabs[4], position3, base.transform.rotation);
				hitData3.m_Color_Main = Color.gray;
				hitData3.m_Color_Rim = Color.white;
				hitData3.m_nHurtFX = 2;
				hitData3.m_nLastHit = 1;
			}
			bool flag5 = id == 50021;
			if (flag5)
			{
				Vector3 position4 = base.transform.position + base.transform.forward * 1.5f;
				position4.y += 1f;
				HitData hitData4 = this.m_linkProfessionRole.Build_PRBullet(5002u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position4, base.transform.rotation);
				hitData4.m_nDamage = 58;
				hitData4.m_Color_Main = Color.gray;
				hitData4.m_Color_Rim = Color.white;
				hitData4.m_nHurtFX = 4;
			}
			bool flag6 = id == 50022;
			if (flag6)
			{
				Vector3 position5 = base.transform.position + base.transform.forward * 1.5f;
				position5.y += 1f;
				HitData hitData5 = this.m_linkProfessionRole.Build_PRBullet(5002u, 0.125f, SceneTFX.m_Bullet_Prefabs[5], position5, base.transform.rotation);
				hitData5.m_nDamage = 138;
				hitData5.m_Color_Main = Color.gray;
				hitData5.m_Color_Rim = Color.white;
				hitData5.m_nHurtFX = 5;
				hitData5.m_nHurtSP_type = 11;
				hitData5.m_nHurtSP_pow = 3;
				hitData5.m_nLastHit = 1;
			}
			bool flag7 = id == 5;
			if (flag7)
			{
				Vector3 position6 = base.transform.position;
				position6.y += 1f;
				HitData hitData6 = this.m_linkProfessionRole.Build_PRBullet(5003u, 0.125f, SceneTFX.m_Bullet_Prefabs[4], position6, base.transform.rotation);
				hitData6.m_nDamage = 57;
				hitData6.m_Color_Main = Color.blue;
				hitData6.m_Color_Rim = Color.cyan;
				hitData6.m_nHurtFX = 5;
			}
			bool flag8 = id == 50041;
			if (flag8)
			{
				Vector3 position7 = base.transform.position + base.transform.forward * 1.5f;
				position7.y += 1f;
				HitData hitData7 = this.m_linkProfessionRole.Build_PRBullet(5004u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position7, base.transform.rotation);
				hitData7.m_Color_Main = Color.gray;
				hitData7.m_Color_Rim = Color.white;
				hitData7.m_nHurtFX = 4;
			}
			bool flag9 = id == 50042;
			if (flag9)
			{
				Vector3 position8 = base.transform.position + base.transform.forward * 1.5f;
				position8.y += 1f;
				HitData hitData8 = this.m_linkProfessionRole.Build_PRBullet(5004u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position8, base.transform.rotation);
				hitData8.m_Color_Main = Color.gray;
				hitData8.m_Color_Rim = Color.white;
				hitData8.m_nLastHit = 1;
				hitData8.m_nHurtFX = 5;
			}
			bool flag10 = 131 == id;
			if (flag10)
			{
				bool flag11 = this.m_linkProfessionRole.m_LockRole != null;
				if (flag11)
				{
					bool flag12 = (this.m_linkProfessionRole.m_curModel.position - this.m_linkProfessionRole.m_LockRole.m_curModel.position).magnitude < 4f;
					if (flag12)
					{
						this.m_linkProfessionRole.m_curModel.position = this.m_linkProfessionRole.m_LockRole.m_curModel.position - this.m_linkProfessionRole.m_LockRole.m_curModel.forward * 4f;
						this.m_linkProfessionRole.m_curModel.forward = this.m_linkProfessionRole.m_LockRole.m_curModel.forward;
					}
				}
			}
			bool flag13 = 50071 == id;
			if (flag13)
			{
				Vector3 position9 = base.transform.position;
				HitData hitData9 = this.m_linkProfessionRole.Build_PRBullet(5007u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position9, base.transform.rotation);
				hitData9.m_Color_Main = Color.gray;
				hitData9.m_Color_Rim = Color.white;
				hitData9.m_nHurtFX = 5;
			}
			bool flag14 = 50072 == id;
			if (flag14)
			{
				Vector3 position10 = base.transform.position;
				HitData hitData10 = this.m_linkProfessionRole.Build_PRBullet(5007u, 0.125f, SceneTFX.m_Bullet_Prefabs[7], position10, base.transform.rotation);
				hitData10.m_Color_Main = Color.gray;
				hitData10.m_Color_Rim = Color.white;
				hitData10.m_nHurtFX = 1;
			}
			bool flag15 = 5006 == id;
			if (flag15)
			{
				Vector3 position11 = base.transform.position;
				HitData hitData11 = this.m_linkProfessionRole.Build_PRBullet(5006u, 1f, SceneTFX.m_Bullet_Prefabs[3], position11, base.transform.rotation);
				hitData11.m_Color_Main = Color.gray;
				hitData11.m_Color_Rim = Color.white;
				hitData11.m_nHurtFX = 2;
			}
			bool flag16 = 5009 == id;
			if (flag16)
			{
				bool flag17 = this.m_linkProfessionRole.m_LockRole != null;
				if (flag17)
				{
					this.m_linkProfessionRole.TurnToRole(this.m_linkProfessionRole.m_LockRole, true);
				}
				Vector3 position12 = base.transform.position;
				HitData hitData12 = this.m_linkProfessionRole.Build_PRBullet(5009u, 0.125f, SceneTFX.m_Bullet_Prefabs[7], position12, base.transform.rotation);
				hitData12.m_Color_Main = Color.gray;
				hitData12.m_Color_Rim = Color.white;
				hitData12.m_nHurtFX = 2;
			}
		}
	}

	public void onJump(int id)
	{
		bool flag = 5009 == id;
		if (flag)
		{
			bool flag2 = this.m_linkProfessionRole.m_LockRole != null;
			if (flag2)
			{
				NavMeshPath path = new NavMeshPath();
				NavMeshAgent component = base.transform.GetComponent<NavMeshAgent>();
				Vector3 position = base.transform.position;
				int num = 2;
				while (num <= 2 && num >= 0)
				{
					Vector3 vector = this.m_linkProfessionRole.m_LockRole.m_curModel.position + base.transform.forward * (float)num;
					bool flag3 = component.CalculatePath(vector, path);
					if (flag3)
					{
						float num2 = Vector3.Distance(position, vector);
						base.transform.DOJump(vector, 0.2f * num2 + vector.y, 1, 0.01f, false);
						break;
					}
					num--;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P5Assassin_Event.ASSASSIN_S1);
				gameObject.transform.SetParent(this.m_linkProfessionRole.m_LockRole.m_curModel, false);
				UnityEngine.Object.Destroy(gameObject, 3f);
			}
		}
		this.m_linkProfessionRole.ShowAll();
	}

	public new void onHide(int id)
	{
		bool flag = 5009 == id;
		if (flag)
		{
			bool flag2 = this.m_linkProfessionRole.m_LockRole != null;
			if (flag2)
			{
				Vector3 zero = Vector3.zero;
				zero.y += this.m_linkProfessionRole.m_LockRole.headOffset.y;
				GameObject gameObject = UnityEngine.Object.Instantiate(P5Assassin_Event.ASSASSIN_S2, zero, this.m_linkProfessionRole.m_LockRole.m_curModel.rotation) as GameObject;
				gameObject.transform.SetParent(this.m_linkProfessionRole.m_LockRole.m_curModel, false);
				UnityEngine.Object.Destroy(gameObject, 3f);
			}
		}
		base.onHide(id);
	}
}
