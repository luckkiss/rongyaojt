using MuGame;
using System;
using UnityEngine;

public class M10003 : MonsterRole
{
	public Animator m_LH_Anim_Skill1;

	public Animator m_RH_Anim_Skill1;

	public override void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f)
	{
		this.m_bFlyMonster = true;
		this.m_fNavSpeed = 3f;
		this.m_fNavStoppingDis = 4f;
		this.m_fAttackAngle = 35f;
		this.m_nSPWeight = 64;
		this.m_nNavPriority = 49;
		bool flag = this.m_moveAgent != null;
		if (flag)
		{
			this.m_moveAgent.avoidancePriority = 49;
			this.m_moveAgent.speed = this.m_fNavSpeed;
			this.m_moveAgent.radius = 4f;
		}
		base.Init(prefab_path, layer, pos, roatate);
		M10003_Stl_Event m10003_Stl_Event = this.m_curModel.gameObject.AddComponent<M10003_Stl_Event>();
		m10003_Stl_Event.m_monRole = this;
		m10003_Stl_Event.m_StlRole = this;
		base.maxHp = (base.curhp = 2000);
		Transform transform = this.m_curModel.FindChild("Dummy001");
		Transform transform2 = this.m_curModel.FindChild("Bip001 HeadNub");
		bool flag2 = transform != null;
		if (flag2)
		{
			GameObject original = Resources.Load<GameObject>("bullet/10003/sbt1/s2");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject.transform.SetParent(transform, false);
			this.m_LH_Anim_Skill1 = gameObject.GetComponent<Animator>();
			Transform transform3 = gameObject.transform.FindChild("bt");
			bool flag3 = transform3 != null;
			if (flag3)
			{
				HitData hitData = transform3.gameObject.AddComponent<HitData>();
				hitData.m_CastRole = this;
				hitData.m_ePK_Type = PK_TYPE.PK_LEGION;
				hitData.m_unPK_Param = this.m_unLegionID;
				hitData.m_vBornerPos = this.m_curModel.position;
				hitData.m_nDamage = 888;
				hitData.m_nHitType = 0;
				hitData.m_bOnlyHit = false;
				transform3.gameObject.layer = EnumLayer.LM_BT_FIGHT;
			}
		}
		bool flag4 = transform2 != null && transform != null;
		if (flag4)
		{
			GameObject original2 = Resources.Load<GameObject>("bullet/10003/sbt1/s1");
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original2);
			gameObject2.transform.SetParent(transform2, false);
			this.m_RH_Anim_Skill1 = gameObject2.GetComponent<Animator>();
			Transform transform4 = gameObject2.transform.FindChild("bt");
			bool flag5 = transform4 != null;
			if (flag5)
			{
				HitData hitData2 = transform4.gameObject.AddComponent<HitData>();
				hitData2.m_CastRole = this;
				hitData2.m_ePK_Type = PK_TYPE.PK_LEGION;
				hitData2.m_unPK_Param = this.m_unLegionID;
				hitData2.m_vBornerPos = this.m_curModel.position;
				hitData2.m_nDamage = 888;
				hitData2.m_nHitType = 0;
				hitData2.m_bOnlyHit = false;
				transform4.gameObject.layer = EnumLayer.LM_BT_FIGHT;
			}
		}
		bool flag6 = this.m_LH_Anim_Skill1 == null;
		if (flag6)
		{
			this.m_LH_Anim_Skill1 = U3DAPI.DEF_ANIMATOR;
		}
		bool flag7 = this.m_RH_Anim_Skill1 == null;
		if (flag7)
		{
			this.m_RH_Anim_Skill1 = U3DAPI.DEF_ANIMATOR;
		}
		this.PlayFire();
	}

	protected override void EnterAttackState()
	{
		bool flag = UnityEngine.Random.Range(0, 10) < 3;
		if (flag)
		{
			this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, 1);
			this.m_curAni.SetBool(EnumAni.ANI_ATTACK, false);
		}
		else
		{
			this.m_curAni.SetBool(EnumAni.ANI_ATTACK, true);
			this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, 0);
		}
	}

	protected override void LeaveAttackState()
	{
		this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, 0);
		this.m_curAni.SetBool(EnumAni.ANI_ATTACK, false);
	}

	public void PlayFire()
	{
		this.m_LH_Anim_Skill1.SetTrigger(EnumAni.ANI_T_SBT_ATK);
		this.m_RH_Anim_Skill1.SetTrigger(EnumAni.ANI_T_SBT_ATK);
	}

	public override void onDeadEnd()
	{
		base.onDeadEnd();
		Debug.Log("成功过关");
	}
}
