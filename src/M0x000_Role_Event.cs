using DG.Tweening;
using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class M0x000_Role_Event : Monster_Base_Event
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

	public static GameObject WARRIOR_B1;

	public static GameObject ASSASSIN_S1;

	public static GameObject ASSASSIN_S2;

	public string fx_2003_name = "skill_2003";

	public string fx_5003_name = "skill_5003";

	private int cur_3002_num = 0;

	public Vector3 cur_3002_pos;

	public Vector3 cur_3002_forward;

	public Vector3 ball_30111_pos;

	public float ball_30111_hight;

	public float ball_3011_dis;

	public List<Transform> m_cur_ball_30111 = new List<Transform>();

	private int cur_3007_2_time = 0;

	private int cur_3007_1_time = 0;

	private void onNT(float time)
	{
		this.m_monRole.m_fSkillShowTime = time;
	}

	private void onSFX(int id)
	{
		bool flag = this.m_monRole is M000P2 || this.m_monRole is ohterP2Warrior;
		if (flag)
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
				SceneFXMgr.Instantiate("FX/warrior/SFX_" + id.ToString(), base.transform.position, base.transform.rotation, 4f);
			}
		}
		else
		{
			bool flag4 = this.m_monRole is M000P3 || this.m_monRole is ohterP3Mage;
			if (flag4)
			{
				SceneFXMgr.Instantiate("FX/mage/SFX_" + id.ToString(), base.transform.position, base.transform.rotation, 6f);
			}
			else
			{
				bool flag5 = this.m_monRole is M000P5 || this.m_monRole is ohterP5Assassin;
				if (flag5)
				{
					bool flag6 = id == 5003;
					if (flag6)
					{
						bool flag7 = base.transform.FindChild(this.fx_5003_name) != null;
						if (!flag7)
						{
							base.CancelInvoke("SFX_5003_hide");
							base.Invoke("SFX_5003_hide", 2.5f);
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(P5Assassin.ASSASSIN_SFX1);
							UnityEngine.Object.Destroy(gameObject2, 4f);
							this.fx_5003_name = gameObject2.name;
							gameObject2.transform.SetParent(base.transform, false);
						}
					}
					else
					{
						SceneFXMgr.Instantiate("FX/assa/SFX_" + id.ToString(), base.transform.position, base.transform.rotation, 4f);
					}
				}
			}
		}
	}

	private void SFX_2003_hide()
	{
		bool flag = base.transform.FindChild(this.fx_2003_name) != null;
		if (flag)
		{
			base.transform.FindChild(this.fx_2003_name).gameObject.SetActive(false);
		}
	}

	private void SFX_5003_hide()
	{
		bool flag = base.transform.FindChild(this.fx_5003_name) != null;
		if (flag)
		{
			base.transform.FindChild(this.fx_5003_name).gameObject.SetActive(false);
		}
	}

	public void onWing(float time)
	{
	}

	private void Bullet_1()
	{
	}

	private void onND()
	{
	}

	private void onSound()
	{
	}

	public new void onShake(string param)
	{
	}

	private void Bullet_2()
	{
	}

	public void onJump(int id)
	{
		bool flag = 2009 == id;
		if (flag)
		{
			NavMeshPath path = new NavMeshPath();
			NavMeshAgent component = base.transform.GetComponent<NavMeshAgent>();
			Vector3 position = base.transform.position;
			bool flag2 = this.m_monRole.m_LockRole != null;
			if (flag2)
			{
				float num = Vector3.Distance(position, this.m_monRole.m_LockRole.m_curModel.position);
				int num2 = 1;
				while ((float)num2 < num)
				{
					Vector3 targetPosition = this.m_monRole.m_LockRole.m_curModel.position - base.transform.forward * (float)num2;
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
						Vector3 vector2 = this.m_monRole.m_LockRole.m_curModel.position - base.transform.forward * (float)num4;
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
			bool flag9 = this.m_monRole.m_LockRole != null;
			if (flag9)
			{
				float num6 = Vector3.Distance(position2, this.m_monRole.m_LockRole.m_curModel.position);
				int num7 = 1;
				while ((float)num7 < num6)
				{
					Vector3 targetPosition2 = this.m_monRole.m_LockRole.m_curModel.position - base.transform.forward * (float)num7;
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
						Vector3 vector5 = this.m_monRole.m_LockRole.m_curModel.position - base.transform.forward * (float)num9;
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
		bool flag15 = 3010 == id;
		if (flag15)
		{
			NavMeshPath path3 = new NavMeshPath();
			NavMeshAgent component3 = base.transform.GetComponent<NavMeshAgent>();
			Vector3 position3 = base.transform.position;
			int num11 = 10;
			for (int k = num11; k >= 0; k--)
			{
				Vector3 vector7 = position3 + base.transform.forward * (float)k;
				bool flag16 = component3.CalculatePath(vector7, path3);
				if (flag16)
				{
					float num12 = Vector3.Distance(position3, vector7);
					base.transform.DOJump(vector7, 0.2f * num12 + vector7.y, 1, 0.03f, false);
					break;
				}
			}
		}
		bool flag17 = 5009 == id;
		if (flag17)
		{
			bool flag18 = this.m_monRole.m_LockRole != null;
			if (flag18)
			{
				NavMeshPath path4 = new NavMeshPath();
				NavMeshAgent component4 = base.transform.GetComponent<NavMeshAgent>();
				Vector3 position4 = base.transform.position;
				int num13 = 2;
				while (num13 <= 2 && num13 >= 0)
				{
					Vector3 vector8 = this.m_monRole.m_LockRole.m_curModel.position + base.transform.forward * (float)num13;
					bool flag19 = component4.CalculatePath(vector8, path4);
					if (flag19)
					{
						float num14 = Vector3.Distance(position4, vector8);
						base.transform.DOJump(vector8, 0.2f * num14 + vector8.y, 1, 0.01f, false);
						break;
					}
					num13--;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(M0x000_Role_Event.ASSASSIN_S1);
				gameObject.transform.SetParent(this.m_monRole.m_LockRole.m_curModel, false);
				UnityEngine.Object.Destroy(gameObject, 3f);
			}
		}
	}

	public void onBullet(int id)
	{
		bool flag = this.m_monRole is M000P3 || this.m_monRole is ohterP3Mage;
		if (flag)
		{
			bool flag2 = id == 1;
			if (flag2)
			{
				Vector3 position = base.transform.position + base.transform.forward * 0.5f;
				position.y += 1.25f;
				GameObject gameObject = UnityEngine.Object.Instantiate(M0x000_Role_Event.MAGE_B1, position, base.transform.rotation) as GameObject;
				gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
				Transform transform = gameObject.transform.FindChild("t");
				bool flag3 = transform != null;
				if (flag3)
				{
					HitData hitData = this.m_monRole.Link_PRBullet(3001u, 2f, gameObject, transform);
					hitData.m_Color_Main = Color.gray;
					hitData.m_Color_Rim = Color.white;
					hitData.m_nDamage = 277;
					hitData.m_aniTrack = transform.GetComponent<Animator>();
					transform.gameObject.layer = EnumLayer.LM_BT_FIGHT;
					Transform transform2 = transform.FindChild("f");
					bool flag4 = transform2 != null;
					if (flag4)
					{
						hitData.m_aniFx = transform2.GetComponent<Animator>();
					}
				}
			}
			bool flag5 = id == 2;
			if (flag5)
			{
				Vector3 vector = base.transform.position;
				vector.y += 1f + UnityEngine.Random.Range(0f, 1f);
				bool flag6 = UnityEngine.Random.Range(0, 16) > 7;
				if (flag6)
				{
					vector += base.transform.right * (0.5f + UnityEngine.Random.Range(0f, 1f));
				}
				else
				{
					vector -= base.transform.right * (0.5f + UnityEngine.Random.Range(0f, 1f));
				}
				GameObject gameObject2 = UnityEngine.Object.Instantiate(M0x000_Role_Event.MAGE_B2, vector, base.transform.rotation) as GameObject;
				gameObject2.transform.SetParent(U3DAPI.FX_POOL_TF, false);
				Transform transform3 = gameObject2.transform.FindChild("t");
				bool flag7 = transform3 != null;
				if (flag7)
				{
					HitData hitData2 = this.m_monRole.Link_PRBullet(3005u, 2f, gameObject2, transform3);
					hitData2.m_nHurtSP_type = 11;
					hitData2.m_nHurtSP_pow = 1;
					hitData2.m_aniTrack = transform3.GetComponent<Animator>();
					transform3.gameObject.layer = EnumLayer.LM_BT_FIGHT;
					Transform transform4 = transform3.FindChild("f");
					bool flag8 = transform4 != null;
					if (flag8)
					{
						hitData2.m_aniFx = transform4.GetComponent<Animator>();
					}
				}
			}
			bool flag9 = id == 101;
			if (flag9)
			{
				Vector3 position2 = base.transform.position;
				HitData hitData3 = this.m_monRole.Build_PRBullet(3004u, 0.125f, SceneTFX.m_Bullet_Prefabs[7], position2, base.transform.rotation);
				hitData3.m_nDamage = 288;
				hitData3.m_Color_Main = Color.blue;
				hitData3.m_Color_Rim = Color.cyan;
				hitData3.m_nHurtFX = 1;
				hitData3.m_nHurtSP_type = 11;
				hitData3.m_nHurtSP_pow = 2;
			}
			bool flag10 = 3006 == id;
			if (flag10)
			{
				bool flag11 = this.m_monRole.m_LockRole == null;
				if (flag11)
				{
					return;
				}
				Vector3 position3 = this.m_monRole.m_LockRole.m_curModel.position;
				position3.z += UnityEngine.Random.Range(-2f, 2f);
				position3.x += UnityEngine.Random.Range(-2f, 2f);
				position3.y += 8f;
				GameObject gameObject3 = UnityEngine.Object.Instantiate(P3Mage_Event.MAGE_B6, position3, base.transform.rotation) as GameObject;
				gameObject3.transform.SetParent(U3DAPI.FX_POOL_TF, false);
				Transform transform5 = gameObject3.transform.FindChild("t");
				bool flag12 = transform5 != null;
				if (flag12)
				{
					HitData hitData4 = this.m_monRole.Link_PRBullet(3006u, 3f, gameObject3, transform5);
					hitData4.m_nHurtSP_type = 11;
					hitData4.m_nHurtSP_pow = 1;
					hitData4.m_bOnlyHit = false;
					hitData4.m_Color_Main = Color.blue;
					hitData4.m_Color_Rim = Color.white;
					hitData4.m_aniTrack = transform5.GetComponent<Animator>();
					transform5.gameObject.layer = EnumLayer.LM_BT_FIGHT;
					Transform transform6 = transform5.FindChild("f");
					bool flag13 = transform6 != null;
					if (flag13)
					{
						hitData4.m_aniFx = transform6.GetComponent<Animator>();
					}
				}
			}
			bool flag14 = 3002 == id;
			if (flag14)
			{
				this.cur_3002_num = 0;
				this.cur_3002_forward = base.transform.forward;
				this.cur_3002_pos = base.transform.position;
				base.CancelInvoke("skill_3002");
				base.InvokeRepeating("skill_3002", 0f, 0.2f);
			}
			bool flag15 = 3009 == id;
			if (flag15)
			{
				Vector3 position4 = base.transform.position + base.transform.forward * 3.5f;
				HitData hitData5 = this.m_monRole.Build_PRBullet(3009u, 0.125f, SceneTFX.m_Bullet_Prefabs[7], position4, base.transform.rotation);
				hitData5.m_nDamage = 288;
				hitData5.m_Color_Main = Color.white;
				hitData5.m_Color_Rim = new Color(0.02f, 0.73f, 0.92f, 0.51f);
				hitData5.m_nHurtSP_type = 31;
				hitData5.m_nHurtSP_pow = 2;
			}
			bool flag16 = 30071 == id;
			if (flag16)
			{
				Vector3 vector2 = base.transform.position + base.transform.forward * 7f;
				HitData hitData6 = this.m_monRole.Build_PRBullet(3007u, 3.5f, SceneTFX.m_Bullet_Prefabs[8], vector2, base.transform.rotation);
				hitData6.m_nDamage = 0;
				hitData6.m_Color_Main = Color.gray;
				hitData6.m_Color_Rim = Color.white;
				vector2.y += 1f;
				hitData6.m_nHurtSP_pos = vector2;
				hitData6.m_nHurtSP_type = 14;
				hitData6.m_nHurtSP_pow = 4;
				hitData6.m_hurtNum = 0;
			}
			bool flag17 = 30072 == id;
			if (flag17)
			{
				this.cur_3007_1_time = 0;
				base.CancelInvoke("skill_3007_1");
				base.InvokeRepeating("skill_3007_1", 0f, 0.4f);
			}
			bool flag18 = 30073 == id;
			if (flag18)
			{
				this.cur_3007_2_time = 0;
				base.CancelInvoke("skill_3007_2");
				base.InvokeRepeating("skill_3007_2", 0f, 0.4f);
			}
			bool flag19 = 30111 == id;
			if (flag19)
			{
				this.m_cur_ball_30111.Clear();
				bool flag20 = this.m_monRole == null;
				if (flag20)
				{
					return;
				}
				this.ball_30111_pos = this.m_monRole.m_LockRole.m_curModel.position;
				this.ball_30111_hight = this.m_monRole.m_LockRole.headOffset.y / 2f;
				this.ball_3011_dis = Vector3.Distance(base.transform.position, this.m_monRole.m_LockRole.m_curModel.position);
				GameObject gameObject4 = UnityEngine.Object.Instantiate(M0x000_Role_Event.MAGE_S3011, base.transform.position, base.transform.rotation) as GameObject;
				gameObject4.transform.SetParent(U3DAPI.FX_POOL_TF, false);
				for (int i = 1; i <= gameObject4.transform.FindChild("heiqiu").childCount; i++)
				{
					this.m_cur_ball_30111.Add(gameObject4.transform.FindChild("heiqiu/0" + i));
				}
				UnityEngine.Object.Destroy(gameObject4, 6f);
			}
			bool flag21 = 30112 == id;
			if (flag21)
			{
				bool flag22 = this.m_monRole.m_LockRole != null;
				if (flag22)
				{
					this.m_monRole.TurnToRole(this.m_monRole.m_LockRole, false);
				}
				bool flag23 = this.m_cur_ball_30111.Count == 0;
				if (!flag23)
				{
					for (int j = 0; j < this.m_cur_ball_30111.Count; j++)
					{
						Transform one = this.m_cur_ball_30111[j];
						one.GetComponent<Animator>().enabled = false;
						Transform ball = one.FindChild("top/heiqiu (1)");
						Vector3 localPosition = ball.localPosition;
						ball.transform.DOLocalMove(localPosition / 3f, 0.35f, false).SetDelay(0.1f * (float)(j + 1) / 2f);
						Tweener t = one.transform.DOLocalMove(new Vector3(0f, this.ball_30111_hight, this.ball_3011_dis), 0.35f + this.ball_3011_dis * 0.01f, false).SetDelay(0.1f * (float)j);
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
							GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(M0x000_Role_Event.MAGE_S3011_1);
							gameObject5.transform.SetParent(one, false);
							HitData hitData31 = this.m_monRole.Build_PRBullet(3004u, 0.125f, SceneTFX.m_Bullet_Prefabs[1], this.ball_30111_pos, this.transform.rotation);
							hitData31.m_nDamage = 288;
						});
						bool flag24 = j == this.m_cur_ball_30111.Count - 1;
						if (flag24)
						{
							this.m_cur_ball_30111.Clear();
						}
					}
				}
			}
		}
		else
		{
			bool flag25 = this.m_monRole is M000P2 || this.m_monRole is ohterP2Warrior;
			if (flag25)
			{
				bool flag26 = id == 1;
				if (flag26)
				{
					Vector3 position5 = base.transform.position + base.transform.forward * 1.5f;
					HitData hitData7 = this.m_monRole.Build_PRBullet(2001u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position5, base.transform.rotation);
					hitData7.m_nDamage = 108;
					hitData7.m_Color_Main = Color.gray;
					hitData7.m_Color_Rim = Color.white;
					hitData7.m_nHurtFX = 2;
				}
				bool flag27 = id == 2;
				if (flag27)
				{
					Vector3 position6 = base.transform.position + base.transform.forward * 1.5f;
					HitData hitData8 = this.m_monRole.Build_PRBullet(2001u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position6, base.transform.rotation);
					hitData8.m_nDamage = 128;
					hitData8.m_Color_Main = Color.gray;
					hitData8.m_Color_Rim = Color.white;
					hitData8.m_nHurtFX = 2;
				}
				bool flag28 = id == 3;
				if (flag28)
				{
					Vector3 position7 = base.transform.position + base.transform.forward * 2.5f;
					HitData hitData9 = this.m_monRole.Build_PRBullet(2001u, 0.125f, SceneTFX.m_Bullet_Prefabs[4], position7, base.transform.rotation);
					hitData9.m_nDamage = 188;
					hitData9.m_Color_Main = Color.gray;
					hitData9.m_Color_Rim = Color.white;
					hitData9.m_nHurtFX = 2;
					hitData9.m_nHurtSP_type = 11;
					hitData9.m_nHurtSP_pow = 3;
					hitData9.m_nLastHit = 1;
				}
				bool flag29 = id == 4;
				if (flag29)
				{
					Vector3 position8 = base.transform.position;
					HitData hitData10 = this.m_monRole.Build_PRBullet(2003u, 0.125f, SceneTFX.m_Bullet_Prefabs[4], position8, base.transform.rotation);
					hitData10.m_nDamage = 88;
					hitData10.m_Color_Main = Color.red;
					hitData10.m_Color_Rim = Color.red;
					hitData10.m_nHurtFX = 3;
				}
				bool flag30 = id == 21;
				if (flag30)
				{
					Vector3 position9 = base.transform.position + base.transform.forward * 4f;
					HitData hitData11 = this.m_monRole.Build_PRBullet(2004u, 0.2f, SceneTFX.m_Bullet_Prefabs[6], position9, base.transform.rotation);
					hitData11.m_nDamage = 388;
					hitData11.m_Color_Main = Color.red;
					hitData11.m_Color_Rim = Color.red;
					hitData11.m_nHurtSP_type = 1;
					hitData11.m_nHurtSP_pow = 4;
				}
				bool flag31 = 13 == id;
				if (flag31)
				{
					bool flag32 = this.m_monRole.m_LockRole != null;
					if (flag32)
					{
						Vector3 position10 = base.transform.position + base.transform.forward * 0.5f;
						position10.y += 1.25f;
						HitData bullet = this.m_monRole.Build_PRBullet(2004u, 0f, M0x000_Role_Event.WARRIOR_B1, position10, base.transform.rotation);
						FollowBullet_Mgr.AddBullet(this.m_monRole.m_LockRole, bullet, 0.8f);
					}
				}
				bool flag33 = 141 == id;
				if (flag33)
				{
					Vector3 position11 = base.transform.position + base.transform.forward * 3f;
					HitData hitData12 = this.m_monRole.Build_PRBullet(2002u, 0.3f, SceneTFX.m_Bullet_Prefabs[4], position11, base.transform.rotation);
					hitData12.m_nDamage = 108;
					hitData12.m_Color_Main = Color.gray;
					hitData12.m_Color_Rim = Color.white;
					hitData12.m_nHurtFX = 2;
				}
				bool flag34 = 142 == id;
				if (flag34)
				{
					Vector3 position12 = base.transform.position + base.transform.forward * 6f;
					HitData hitData13 = this.m_monRole.Build_PRBullet(2002u, 0.3f, SceneTFX.m_Bullet_Prefabs[4], position12, base.transform.rotation);
					hitData13.m_nDamage = 88;
					hitData13.m_Color_Main = Color.gray;
					hitData13.m_Color_Rim = Color.white;
					hitData13.m_nHurtFX = 2;
				}
				bool flag35 = 143 == id;
				if (flag35)
				{
					Vector3 position13 = base.transform.position + base.transform.forward * 4f;
					HitData hitData14 = this.m_monRole.Build_PRBullet(2002u, 0.2f, SceneTFX.m_Bullet_Prefabs[6], position13, base.transform.rotation);
					hitData14.m_nDamage = 188;
					hitData14.m_Color_Main = Color.gray;
					hitData14.m_Color_Rim = Color.white;
					hitData14.m_nHurtFX = 2;
					hitData14.m_nHurtSP_type = 21;
					hitData14.m_nHurtSP_pow = 4;
				}
				bool flag36 = 2009 == id;
				if (flag36)
				{
					Vector3 position14 = base.transform.position + base.transform.forward * 3f;
					HitData hitData15 = this.m_monRole.Build_PRBullet(2009u, 0.2f, SceneTFX.m_Bullet_Prefabs[5], position14, base.transform.rotation);
					hitData15.m_nDamage = 88;
					hitData15.m_Color_Main = Color.red;
					hitData15.m_Color_Rim = Color.red;
					hitData15.m_nHurtSP_type = 11;
					hitData15.m_nHurtSP_pow = 4;
					hitData15.m_nHurtFX = 6;
				}
				bool flag37 = 2006 == id;
				if (flag37)
				{
					Vector3 position15 = base.transform.position + base.transform.forward * 3f;
					HitData hitData16 = this.m_monRole.Build_PRBullet(2006u, 0.2f, SceneTFX.m_Bullet_Prefabs[6], position15, base.transform.rotation);
					hitData16.m_nDamage = 88;
					hitData16.m_Color_Main = Color.red;
					hitData16.m_Color_Rim = Color.red;
					hitData16.m_nHurtSP_type = 11;
					hitData16.m_nHurtSP_pow = 4;
					hitData16.m_nHurtFX = 6;
				}
				bool flag38 = 2007 == id;
				if (flag38)
				{
					Vector3 position16 = base.transform.position + base.transform.forward * 1f;
					HitData hitData17 = this.m_monRole.Build_PRBullet(2007u, 0.3f, SceneTFX.m_Bullet_Prefabs[4], position16, base.transform.rotation);
					hitData17.m_nDamage = 88;
					hitData17.m_Color_Main = Color.red;
					hitData17.m_Color_Rim = Color.red;
					hitData17.m_nHurtSP_type = 11;
					hitData17.m_nHurtSP_pow = 10;
					hitData17.m_nHurtFX = 3;
				}
			}
			else
			{
				bool flag39 = this.m_monRole is M000P5 || this.m_monRole is ohterP5Assassin;
				if (flag39)
				{
					bool flag40 = id == 1;
					if (flag40)
					{
						Vector3 position17 = base.transform.position + base.transform.forward * 1.5f;
						position17.y += 1f;
						HitData hitData18 = this.m_monRole.Build_PRBullet(5001u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position17, base.transform.rotation);
						hitData18.m_Color_Main = Color.gray;
						hitData18.m_Color_Rim = Color.white;
						hitData18.m_nHurtFX = 2;
					}
					bool flag41 = id == 2;
					if (flag41)
					{
						Vector3 position18 = base.transform.position + base.transform.forward * 1.5f;
						position18.y += 1f;
						HitData hitData19 = this.m_monRole.Build_PRBullet(5001u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position18, base.transform.rotation);
						hitData19.m_Color_Main = Color.gray;
						hitData19.m_Color_Rim = Color.white;
						hitData19.m_nHurtSP_type = 1;
						hitData19.m_nHurtSP_pow = 2;
						hitData19.m_nHurtFX = 2;
					}
					bool flag42 = id == 3;
					if (flag42)
					{
						Vector3 position19 = base.transform.position + base.transform.forward * 2.5f;
						position19.y += 1f;
						HitData hitData20 = this.m_monRole.Build_PRBullet(5001u, 0.125f, SceneTFX.m_Bullet_Prefabs[4], position19, base.transform.rotation);
						hitData20.m_Color_Main = Color.gray;
						hitData20.m_Color_Rim = Color.white;
						hitData20.m_nHurtFX = 2;
						hitData20.m_nLastHit = 1;
					}
					bool flag43 = id == 50021;
					if (flag43)
					{
						Vector3 position20 = base.transform.position + base.transform.forward * 1.5f;
						position20.y += 1f;
						HitData hitData21 = this.m_monRole.Build_PRBullet(5002u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position20, base.transform.rotation);
						hitData21.m_nDamage = 58;
						hitData21.m_Color_Main = Color.gray;
						hitData21.m_Color_Rim = Color.white;
						hitData21.m_nHurtFX = 4;
					}
					bool flag44 = id == 50022;
					if (flag44)
					{
						Vector3 position21 = base.transform.position + base.transform.forward * 1.5f;
						position21.y += 1f;
						HitData hitData22 = this.m_monRole.Build_PRBullet(5002u, 0.125f, SceneTFX.m_Bullet_Prefabs[5], position21, base.transform.rotation);
						hitData22.m_nDamage = 138;
						hitData22.m_Color_Main = Color.gray;
						hitData22.m_Color_Rim = Color.white;
						hitData22.m_nHurtFX = 5;
						hitData22.m_nHurtSP_type = 11;
						hitData22.m_nHurtSP_pow = 3;
						hitData22.m_nLastHit = 1;
					}
					bool flag45 = id == 5;
					if (flag45)
					{
						Vector3 position22 = base.transform.position;
						position22.y += 1f;
						HitData hitData23 = this.m_monRole.Build_PRBullet(5003u, 0.125f, SceneTFX.m_Bullet_Prefabs[4], position22, base.transform.rotation);
						hitData23.m_nDamage = 57;
						hitData23.m_Color_Main = Color.blue;
						hitData23.m_Color_Rim = Color.cyan;
						hitData23.m_nHurtFX = 5;
					}
					bool flag46 = id == 50041;
					if (flag46)
					{
						Vector3 position23 = base.transform.position + base.transform.forward * 1.5f;
						position23.y += 1f;
						HitData hitData24 = this.m_monRole.Build_PRBullet(5004u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position23, base.transform.rotation);
						hitData24.m_Color_Main = Color.gray;
						hitData24.m_Color_Rim = Color.white;
						hitData24.m_nHurtFX = 4;
					}
					bool flag47 = id == 50042;
					if (flag47)
					{
						Vector3 position24 = base.transform.position + base.transform.forward * 1.5f;
						position24.y += 1f;
						HitData hitData25 = this.m_monRole.Build_PRBullet(5004u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position24, base.transform.rotation);
						hitData25.m_Color_Main = Color.gray;
						hitData25.m_Color_Rim = Color.white;
						hitData25.m_nLastHit = 1;
						hitData25.m_nHurtFX = 5;
					}
					bool flag48 = 131 == id;
					if (flag48)
					{
						bool flag49 = this.m_monRole.m_LockRole != null;
						if (flag49)
						{
							bool flag50 = (this.m_monRole.m_curModel.position - this.m_monRole.m_LockRole.m_curModel.position).magnitude < 4f;
							if (flag50)
							{
								this.m_monRole.m_curModel.position = this.m_monRole.m_LockRole.m_curModel.position - this.m_monRole.m_LockRole.m_curModel.forward * 4f;
								this.m_monRole.m_curModel.forward = this.m_monRole.m_LockRole.m_curModel.forward;
							}
						}
					}
					bool flag51 = 50071 == id;
					if (flag51)
					{
						Vector3 position25 = base.transform.position;
						HitData hitData26 = this.m_monRole.Build_PRBullet(5007u, 0.125f, SceneTFX.m_Bullet_Prefabs[3], position25, base.transform.rotation);
						hitData26.m_Color_Main = Color.gray;
						hitData26.m_Color_Rim = Color.white;
						hitData26.m_nHurtFX = 5;
					}
					bool flag52 = 50072 == id;
					if (flag52)
					{
						Vector3 position26 = base.transform.position;
						HitData hitData27 = this.m_monRole.Build_PRBullet(5007u, 0.125f, SceneTFX.m_Bullet_Prefabs[7], position26, base.transform.rotation);
						hitData27.m_Color_Main = Color.gray;
						hitData27.m_Color_Rim = Color.white;
						hitData27.m_nHurtFX = 1;
					}
					bool flag53 = 5006 == id;
					if (flag53)
					{
						Vector3 position27 = base.transform.position;
						HitData hitData28 = this.m_monRole.Build_PRBullet(5006u, 1f, SceneTFX.m_Bullet_Prefabs[3], position27, base.transform.rotation);
						hitData28.m_Color_Main = Color.gray;
						hitData28.m_Color_Rim = Color.white;
						hitData28.m_nHurtFX = 2;
					}
					bool flag54 = 5009 == id;
					if (flag54)
					{
						bool flag55 = this.m_monRole.m_LockRole != null;
						if (flag55)
						{
							this.m_monRole.TurnToRole(this.m_monRole.m_LockRole, false);
						}
						Vector3 position28 = base.transform.position;
						HitData hitData29 = this.m_monRole.Build_PRBullet(5009u, 1f, SceneTFX.m_Bullet_Prefabs[3], position28, base.transform.rotation);
						hitData29.m_Color_Main = Color.gray;
						hitData29.m_Color_Rim = Color.white;
						hitData29.m_nHurtFX = 2;
					}
				}
				else
				{
					bool flag56 = 1 == id;
					if (flag56)
					{
						Vector3 position29 = base.transform.position + base.transform.forward * 2f;
						position29.y += 1f;
						HitData hitData30 = this.m_monRole.BuildBullet(1u, 0.125f, SceneTFX.m_Bullet_Prefabs[1], position29, base.transform.rotation);
						bool flag57 = !this.m_monRole.isfake;
						if (flag57)
						{
							hitData30.m_nDamage = 0;
						}
					}
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
		GameObject gameObject = UnityEngine.Object.Instantiate(M0x000_Role_Event.MAGE_S3002, vector, base.transform.rotation) as GameObject;
		gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
		UnityEngine.Object.Destroy(gameObject, 0.5f);
		HitData hitData = this.m_monRole.Build_PRBullet(3002u, 1f, SceneTFX.m_Bullet_Prefabs[3], vector, base.transform.rotation);
		hitData.m_nDamage = 1088;
		hitData.m_Color_Main = Color.gray;
		hitData.m_Color_Rim = Color.white;
		hitData.m_nHurtFX = 1;
		bool flag = this.cur_3002_num >= 6;
		if (flag)
		{
			base.CancelInvoke("skill_3002");
		}
	}

	private void skill_3007_1()
	{
		this.cur_3007_1_time++;
		Vector3 position = base.transform.position + base.transform.forward * 2f;
		HitData hitData = this.m_monRole.Build_PRBullet(3007u, 0.3f, SceneTFX.m_Bullet_Prefabs[6], position, base.transform.rotation);
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
		HitData hitData = this.m_monRole.Build_PRBullet(3007u, 0.3f, SceneTFX.m_Bullet_Prefabs[6], position, base.transform.rotation);
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

	public void onHide(int id)
	{
		bool flag = 5009 == id;
		if (flag)
		{
			bool flag2 = this.m_monRole.m_LockRole != null;
			if (flag2)
			{
				Vector3 zero = Vector3.zero;
				zero.y += this.m_monRole.m_LockRole.headOffset.y;
				GameObject gameObject = UnityEngine.Object.Instantiate(M0x000_Role_Event.ASSASSIN_S2, zero, this.m_monRole.m_LockRole.m_curModel.rotation) as GameObject;
				gameObject.transform.SetParent(this.m_monRole.m_LockRole.m_curModel, false);
				UnityEngine.Object.Destroy(gameObject, 3f);
			}
		}
	}
}
