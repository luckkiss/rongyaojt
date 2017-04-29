using MuGame;
using System;
using UnityEngine;

public class P3Mage : ProfessionRole
{
	public static GameObject P3MAGE_SFX1;

	public static GameObject P3MAGE_SFX2;

	public static GameObject P3MAGE_SFX3;

	private GameObject m_SFX1;

	private GameObject m_SFX2;

	private GameObject m_SFX3;

	private TickItem process_3003;

	private float m_skill3003_time = 4f;

	private float m_cur3003_time = 0f;

	private int m_skill3003_num = 20;

	private int m_cur3003_num = 0;

	private Vector3 m_3003_pos;

	private Quaternion m_3003_rotation;

	private TickItem process_3008;

	private float m_cur3008_time = 0f;

	public new void Init(string name, int layer, Vector3 pos, bool isUser = false)
	{
		this.m_strAvatarPath = "profession/mage/";
		this.m_strEquipEffPath = "Fx/armourFX/mage/";
		base.roleName = name;
		base.setNavLay(NavmeshUtils.listARE[1]);
		base.Init("profession/mage_inst", layer, pos, isUser);
		P3Mage_Event p3Mage_Event = this.m_curModel.gameObject.AddComponent<P3Mage_Event>();
		p3Mage_Event.m_linkProfessionRole = this;
		this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 0f);
		base.set_weaponl(0, 0);
		base.set_body(0, 0);
	}

	public override void PlaySkill(int id)
	{
		bool flag = this.m_curSkillId != 0;
		if (!flag)
		{
			bool flag2 = 3003 == id && base.getShowSkillEff() != 3;
			if (flag2)
			{
				this.runSkill_3003();
			}
			bool flag3 = 3008 == id && base.getShowSkillEff() != 3;
			if (flag3)
			{
				this.runSkill_3008();
			}
			bool flag4 = 30081 == id;
			if (flag4)
			{
				this.removeSkill_30081();
			}
			bool isMain = this.m_isMain;
			if (isMain)
			{
				this.m_moveAgent.avoidancePriority = 50;
			}
			this.m_curSkillId = id;
			this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, id);
			this.m_fAttackCount = 1f;
			bool flag5 = 3010 == id;
			if (flag5)
			{
				this.m_fAttackCount = 0.5f;
			}
		}
	}

	private void runSkill_3003()
	{
		bool flag = this.process_3003 == null;
		if (flag)
		{
			this.process_3003 = new TickItem(new Action<float>(this.onUpdate_3003));
			TickMgr.instance.addTick(this.process_3003);
		}
		this.m_cur3003_time = 0f;
		this.m_cur3003_num = 0;
		this.m_3003_pos = this.m_curModel.transform.position;
		this.m_3003_rotation = this.m_curModel.transform.rotation;
		bool flag2 = this.m_SFX2 == null;
		if (flag2)
		{
			this.m_SFX2 = (UnityEngine.Object.Instantiate(P3Mage.P3MAGE_SFX2, this.m_3003_pos, this.m_3003_rotation) as GameObject);
			this.m_SFX2.transform.SetParent(U3DAPI.FX_POOL_TF, false);
		}
	}

	private void onUpdate_3003(float s)
	{
		bool flag = this.m_curModel == null;
		if (flag)
		{
			TickMgr.instance.removeTick(this.process_3003);
			this.process_3003 = null;
			this.m_SFX2.transform.FindChild("f").GetComponent<Animator>().SetTrigger(EnumAni.ANI_T_FXDEAD);
			UnityEngine.Object.Destroy(this.m_SFX2, 2f);
			this.m_SFX2 = null;
		}
		else
		{
			this.m_cur3003_time += s;
			float num = 0.5f + (float)this.m_cur3003_num * (this.m_skill3003_time - 1f) / (float)this.m_skill3003_num;
			bool flag2 = this.m_cur3003_time > num;
			if (flag2)
			{
				this.onBullet_3003(this.m_3003_pos, this.m_3003_rotation);
				this.m_cur3003_num++;
			}
			bool flag3 = this.m_cur3003_time > this.m_skill3003_time;
			if (flag3)
			{
				this.m_cur3003_time = 0f;
				this.m_cur3003_num = 0;
				TickMgr.instance.removeTick(this.process_3003);
				this.process_3003 = null;
				this.m_SFX2.transform.FindChild("f").GetComponent<Animator>().SetTrigger(EnumAni.ANI_T_FXDEAD);
				UnityEngine.Object.Destroy(this.m_SFX2, 2f);
				this.m_SFX2 = null;
			}
		}
	}

	public void onBullet_3003(Vector3 pos, Quaternion rotation)
	{
		pos.z += UnityEngine.Random.Range(-3f, 3f);
		pos.x += UnityEngine.Random.Range(-3f, 3f);
		pos.y = 16f + pos.y;
		GameObject gameObject = UnityEngine.Object.Instantiate(P3Mage_Event.MAGE_B3, pos, rotation) as GameObject;
		gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
		Transform transform = gameObject.transform.FindChild("t");
		bool flag = transform != null;
		if (flag)
		{
			HitData hitData = base.Link_PRBullet(3003u, 2f, gameObject, transform);
			hitData.m_nHurtSP_type = 11;
			hitData.m_nHurtSP_pow = 1;
			hitData.m_nDamage = 177;
			hitData.m_Color_Main = Color.blue;
			hitData.m_Color_Rim = Color.white;
			hitData.m_aniTrack = transform.GetComponent<Animator>();
			transform.gameObject.layer = EnumLayer.LM_BT_FIGHT;
			Transform transform2 = transform.FindChild("f");
			bool flag2 = transform2 != null;
			if (flag2)
			{
				hitData.m_aniFx = transform2.GetComponent<Animator>();
			}
		}
	}

	public void runSkill_3008()
	{
		bool flag = this.m_SFX1 == null;
		if (flag)
		{
			this.m_SFX1 = UnityEngine.Object.Instantiate<GameObject>(P3Mage.P3MAGE_SFX1);
			this.m_SFX1.transform.SetParent(this.m_curModel, false);
			this.m_SFX1.SetActive(true);
		}
		bool flag2 = this.process_3008 == null;
		if (flag2)
		{
			this.process_3008 = new TickItem(new Action<float>(this.onUpdate_3008));
			TickMgr.instance.addTick(this.process_3008);
		}
		this.m_cur3008_time = 0f;
	}

	private void removeSkill_30081()
	{
		bool flag = this.m_SFX1 != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.m_SFX1);
			this.m_cur3008_time = 0f;
			bool flag2 = this.process_3008 != null;
			if (flag2)
			{
				TickMgr.instance.removeTick(this.process_3008);
			}
			this.process_3008 = null;
			this.m_SFX1 = null;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P3Mage.P3MAGE_SFX3);
			gameObject.transform.SetParent(this.m_curModel, false);
			UnityEngine.Object.Destroy(gameObject, 1f);
		}
	}

	private void onUpdate_3008(float s)
	{
		bool flag = this.m_curModel == null;
		if (flag)
		{
			TickMgr.instance.removeTick(this.process_3008);
		}
		else
		{
			this.m_cur3008_time += s;
			bool flag2 = this.m_cur3008_time > 0.6f;
			if (flag2)
			{
				this.m_SFX1.SetActive(true);
				this.m_cur3008_time = 0f;
				TickMgr.instance.removeTick(this.process_3008);
				this.process_3008 = null;
			}
		}
	}
}
