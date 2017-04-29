using DG.Tweening;
using System;
using UnityEngine;

public class P3Mage_Event : Profession_Base_Event
{
	public static GameObject MAGE_B1;

	public static GameObject MAGE_B2;

	public static GameObject MAGE_B3;

	public static GameObject MAGE_B4_1;

	public static GameObject MAGE_B4_2;

	public static GameObject MAGE_B6;

	public static GameObject MAGE_S3002;

	public static GameObject MAGE_S3002_2;

	public static GameObject MAGE_S3011;

	public static GameObject MAGE_S3011_1;

	private int cur_3002_num = 0;

	public Vector3 cur_3002_pos;

	public Vector3 cur_3002_forward;

	private int cur_3007_2_time = 0;

	private int cur_3007_1_time = 0;

	private void onSFX(int id)
	{
		bool flag = this.m_linkProfessionRole.getShowSkillEff() == 3;
		if (!flag)
		{
			SceneFXMgr.Instantiate("FX/mage/SFX_" + id.ToString(), base.transform.position, base.transform.rotation, 6f);
		}
	}

	public new void onBullet(int id)
	{
		bool flag = this.m_linkProfessionRole.getShowSkillEff() == 3;
		if (!flag)
		{
			bool flag2 = 3006 == id;
			if (flag2)
			{
				bool flag3 = this.m_linkProfessionRole.m_LockRole == null;
				if (flag3)
				{
					return;
				}
				Vector3 position = this.m_linkProfessionRole.m_LockRole.m_curModel.position;
				position.z += UnityEngine.Random.Range(-2f, 2f);
				position.x += UnityEngine.Random.Range(-2f, 2f);
				position.y += 8f;
				GameObject gameObject = UnityEngine.Object.Instantiate(P3Mage_Event.MAGE_B6, position, base.transform.rotation) as GameObject;
				gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
				Transform transform = gameObject.transform.FindChild("t");
				bool flag4 = transform != null;
				if (flag4)
				{
					HitData hitData = this.m_linkProfessionRole.Link_PRBullet(3006u, 3f, gameObject, transform);
					hitData.m_nHurtSP_type = 11;
					hitData.m_nHurtSP_pow = 1;
					hitData.m_bOnlyHit = false;
					hitData.m_Color_Main = Color.red;
					hitData.m_Color_Rim = Color.white;
					hitData.m_aniTrack = transform.GetComponent<Animator>();
					transform.gameObject.layer = EnumLayer.LM_BT_FIGHT;
					Transform transform2 = transform.FindChild("f");
					bool flag5 = transform2 != null;
					if (flag5)
					{
						hitData.m_aniFx = transform2.GetComponent<Animator>();
					}
				}
			}
			bool flag6 = id == 2;
			if (flag6)
			{
				Vector3 vector = base.transform.position;
				vector.y += 1f + UnityEngine.Random.Range(0f, 1f);
				bool flag7 = UnityEngine.Random.Range(0, 16) > 7;
				if (flag7)
				{
					vector += base.transform.right * (0.5f + UnityEngine.Random.Range(0f, 1f));
				}
				else
				{
					vector -= base.transform.right * (0.5f + UnityEngine.Random.Range(0f, 1f));
				}
				GameObject gameObject2 = UnityEngine.Object.Instantiate(P3Mage_Event.MAGE_B2, vector, base.transform.rotation) as GameObject;
				gameObject2.transform.SetParent(U3DAPI.FX_POOL_TF, false);
				Transform transform3 = gameObject2.transform.FindChild("t");
				bool flag8 = transform3 != null;
				if (flag8)
				{
					HitData hitData2 = this.m_linkProfessionRole.Link_PRBullet(3005u, 2f, gameObject2, transform3);
					hitData2.m_nHurtSP_type = 11;
					hitData2.m_nHurtSP_pow = 1;
					hitData2.m_aniTrack = transform3.GetComponent<Animator>();
					transform3.gameObject.layer = EnumLayer.LM_BT_FIGHT;
					Transform transform4 = transform3.FindChild("f");
					bool flag9 = transform4 != null;
					if (flag9)
					{
						hitData2.m_aniFx = transform4.GetComponent<Animator>();
					}
				}
			}
			bool flag10 = 3002 == id;
			if (flag10)
			{
				this.cur_3002_num = 0;
				this.cur_3002_forward = base.transform.forward;
				this.cur_3002_pos = base.transform.position;
				base.CancelInvoke("skill_3002");
				base.InvokeRepeating("skill_3002", 0f, 0.2f);
			}
			bool flag11 = 3011 == id;
			if (flag11)
			{
				bool flag12 = this.m_linkProfessionRole == null;
				if (flag12)
				{
					return;
				}
				bool flag13 = this.m_linkProfessionRole.m_LockRole == null || this.m_linkProfessionRole.m_LockRole.m_curModel == null;
				if (flag13)
				{
					return;
				}
				Vector3 position2 = base.transform.position;
				position2.y += this.m_linkProfessionRole.headOffset.y / 2f;
				Vector3 ball_30111_pos = this.m_linkProfessionRole.m_LockRole.m_curModel.position;
				float y = ball_30111_pos.y + this.m_linkProfessionRole.m_LockRole.headOffset.y / 2f;
				float num = Vector3.Distance(base.transform.position, this.m_linkProfessionRole.m_LockRole.m_curModel.position);
				this.m_linkProfessionRole.TurnToRole(this.m_linkProfessionRole.m_LockRole, false);
				for (int i = 0; i < 6; i++)
				{
					float num2 = (float)UnityEngine.Random.Range(-1, 1);
					float num3 = UnityEngine.Random.Range(-0.5f, 1f);
					float num4 = (float)UnityEngine.Random.Range(-1, 1);
					GameObject fx_inst = UnityEngine.Object.Instantiate(P3Mage_Event.MAGE_S3011, new Vector3(position2.x + num2, position2.y + num3, position2.z + num4), base.transform.rotation) as GameObject;
					fx_inst.transform.SetParent(U3DAPI.FX_POOL_TF, false);
					UnityEngine.Object.Destroy(fx_inst, 4f);
					Transform ball = fx_inst.transform;
					float num5 = 0.35f + num * 0.01f;
					bool flag14 = num5 > 3f;
					if (flag14)
					{
						num5 = 3f;
					}
					Tweener t = ball.DOLocalMove(new Vector3(ball_30111_pos.x, y, ball_30111_pos.z), num5, false).SetDelay(0.1f * (float)i);
					t.SetUpdate(true);
					switch (UnityEngine.Random.Range(0, 5))
					{
					case 1:
						t.SetEase(Ease.OutQuad);
						break;
					case 2:
						t.SetEase(Ease.OutCirc);
						break;
					case 3:
						t.SetEase(Ease.OutCubic);
						break;
					case 4:
						t.SetEase(Ease.OutExpo);
						break;
					case 5:
						t.SetEase(Ease.OutElastic);
						break;
					}
					t.OnComplete(delegate
					{
						ball.gameObject.SetActive(false);
						GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(P3Mage_Event.MAGE_S3011_1);
						gameObject3.transform.SetParent(U3DAPI.FX_POOL_TF, false);
						gameObject3.transform.localPosition = ball.transform.localPosition;
						UnityEngine.Object.Destroy(gameObject3, 2f);
						UnityEngine.Object.Destroy(fx_inst, 1f);
						HitData hitData7 = this.m_linkProfessionRole.Build_PRBullet(3004u, 0.125f, SceneTFX.m_Bullet_Prefabs[1], ball_30111_pos, this.transform.rotation);
						hitData7.m_nDamage = 288;
					});
				}
			}
			bool flag15 = this.m_linkProfessionRole.getShowSkillEff() == 2;
			if (!flag15)
			{
				bool flag16 = id == 300111 || id == 300112 || id == 300121 || id == 300122 || id == 30013;
				if (flag16)
				{
					float d = 1f;
					int num6 = 1;
					if (id <= 300111)
					{
						if (id != 30013)
						{
							if (id == 300111)
							{
								d = 2f;
								num6 = 4;
							}
						}
						else
						{
							d = 3f;
							num6 = 7;
						}
					}
					else if (id != 300112)
					{
						if (id != 300121)
						{
							if (id == 300122)
							{
								d = 8f;
								num6 = 5;
							}
						}
						else
						{
							d = 2f;
							num6 = 5;
						}
					}
					else
					{
						d = 8f;
						num6 = 4;
					}
					Vector3 position3 = base.transform.position + base.transform.forward * d;
					HitData hitData3 = this.m_linkProfessionRole.Build_PRBullet(3001u, 0.125f, SceneTFX.m_Bullet_Prefabs[num6], position3, base.transform.rotation);
					hitData3.m_nDamage = 288;
					hitData3.m_nHurtFX = 8;
					hitData3.m_Color_Main = Color.blue;
					hitData3.m_Color_Rim = Color.white;
				}
				bool flag17 = id == 101;
				if (flag17)
				{
					Vector3 position4 = base.transform.position;
					HitData hitData4 = this.m_linkProfessionRole.Build_PRBullet(3004u, 0.125f, SceneTFX.m_Bullet_Prefabs[7], position4, base.transform.rotation);
					hitData4.m_nDamage = 288;
					hitData4.m_Color_Main = Color.blue;
					hitData4.m_Color_Rim = Color.cyan;
					hitData4.m_nHurtFX = 1;
					hitData4.m_nHurtSP_type = 11;
					hitData4.m_nHurtSP_pow = 2;
				}
				bool flag18 = 3009 == id;
				if (flag18)
				{
					Vector3 position5 = base.transform.position + base.transform.forward * 3.5f;
					HitData hitData5 = this.m_linkProfessionRole.Build_PRBullet(3009u, 0.125f, SceneTFX.m_Bullet_Prefabs[7], position5, base.transform.rotation);
					hitData5.m_nDamage = 288;
					hitData5.m_Color_Main = Color.white;
					hitData5.m_Color_Rim = new Color(0.02f, 0.73f, 0.92f, 0.51f);
					hitData5.m_nHurtSP_type = 31;
					hitData5.m_nHurtSP_pow = 2;
				}
				bool flag19 = 30071 == id;
				if (flag19)
				{
					Vector3 vector2 = base.transform.position + base.transform.forward * 3.5f;
					HitData hitData6 = this.m_linkProfessionRole.Build_PRBullet(3007u, 3.5f, SceneTFX.m_Bullet_Prefabs[8], vector2, base.transform.rotation);
					hitData6.m_nDamage = 0;
					hitData6.m_Color_Main = Color.gray;
					hitData6.m_Color_Rim = Color.white;
					vector2.y += 1f;
					hitData6.m_nHurtSP_pos = vector2;
					hitData6.m_nHurtSP_type = 14;
					hitData6.m_nHurtSP_pow = 4;
					hitData6.m_hurtNum = 0;
				}
				bool flag20 = 30072 == id;
				if (flag20)
				{
					this.cur_3007_1_time = 0;
					base.CancelInvoke("skill_3007_1");
					base.InvokeRepeating("skill_3007_1", 0f, 0.4f);
				}
				bool flag21 = 30073 == id;
				if (flag21)
				{
					this.cur_3007_2_time = 0;
					base.CancelInvoke("skill_3007_2");
					base.InvokeRepeating("skill_3007_2", 0f, 0.4f);
				}
			}
		}
	}

	private void skill_3002()
	{
		this.cur_3002_num++;
		Vector3 vector = this.cur_3002_pos + this.cur_3002_forward * 2f * (float)this.cur_3002_num;
		float num = UnityEngine.Random.Range(0f, 1f);
		float num2 = UnityEngine.Random.Range(0f, 1f);
		vector.x += num;
		vector.z += num2;
		GameObject gameObject = UnityEngine.Object.Instantiate(P3Mage_Event.MAGE_S3002, vector, base.transform.rotation) as GameObject;
		bool flag = SceneCamera.m_nSkillEff_Level > 1;
		if (flag)
		{
			Transform transform = gameObject.transform.FindChild("hide");
			bool flag2 = transform != null;
			if (flag2)
			{
				transform.gameObject.SetActive(false);
			}
		}
		gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
		UnityEngine.Object.Destroy(gameObject, 1.5f);
		HitData hitData = this.m_linkProfessionRole.Build_PRBullet(3002u, 1f, SceneTFX.m_Bullet_Prefabs[3], vector, base.transform.rotation);
		hitData.m_nDamage = 1088;
		hitData.m_Color_Main = Color.gray;
		hitData.m_Color_Rim = Color.white;
		hitData.m_nHurtFX = 1;
		bool flag3 = this.cur_3002_num >= 6;
		if (flag3)
		{
			base.CancelInvoke("skill_3002");
		}
	}

	private void skill_3007_1()
	{
		this.cur_3007_1_time++;
		Vector3 position = base.transform.position + base.transform.forward * 2f;
		HitData hitData = this.m_linkProfessionRole.Build_PRBullet(3007u, 0.3f, SceneTFX.m_Bullet_Prefabs[6], position, base.transform.rotation);
		hitData.m_nDamage = 88;
		hitData.m_Color_Main = Color.gray;
		hitData.m_Color_Rim = Color.white;
		bool flag = this.cur_3007_1_time >= 5;
		if (flag)
		{
			base.CancelInvoke("skill_3007_1");
		}
	}

	private void skill_3007_2()
	{
		this.cur_3007_2_time++;
		Vector3 position = base.transform.position + base.transform.forward * 2f;
		HitData hitData = this.m_linkProfessionRole.Build_PRBullet(3007u, 0.3f, SceneTFX.m_Bullet_Prefabs[6], position, base.transform.rotation);
		hitData.m_nDamage = 88;
		hitData.m_Color_Main = Color.gray;
		hitData.m_Color_Rim = Color.white;
		hitData.m_nLastHit = 1;
		bool flag = this.cur_3007_2_time >= 5;
		if (flag)
		{
			base.CancelInvoke("skill_3007_2");
		}
	}

	public void onJump(int id)
	{
		bool flag = 3010 == id;
		if (flag)
		{
			NavMeshPath path = new NavMeshPath();
			NavMeshAgent component = base.transform.GetComponent<NavMeshAgent>();
			Vector3 position = base.transform.position;
			int num = 10;
			for (int i = num; i >= 0; i--)
			{
				Vector3 vector = position + base.transform.forward * (float)i;
				bool flag2 = component.CalculatePath(vector, path);
				if (flag2)
				{
					float num2 = Vector3.Distance(position, vector);
					base.transform.DOJump(vector, 0.2f * num2 + vector.y, 1, 0.03f, false);
					break;
				}
			}
		}
	}
}
