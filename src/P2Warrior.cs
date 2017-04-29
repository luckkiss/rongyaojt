using MuGame;
using System;
using UnityEngine;

public class P2Warrior : ProfessionRole
{
	public static GameObject WARRIOR_SFX1;

	public static GameObject WARRIOR_SFX2;

	public static GameObject WARRIOR_SFX3;

	public static GameObject WARRIOR_SFX4;

	public static GameObject WARRIOR_SFX5;

	private GameObject m_SFX1;

	private GameObject m_SFX2;

	private TickItem process_2005;

	private float m_skill2005_time = 10f;

	private float m_cur2005_time = 0f;

	private TickItem process_2010;

	private float m_skill2010_time = 10f;

	private float m_cur2010_time = 0f;

	public new void Init(string name, int layer, Vector3 pos, bool isUser = false)
	{
		this.m_strAvatarPath = "profession/warrior/";
		this.m_strEquipEffPath = "Fx/armourFX/warrior/";
		base.roleName = name;
		this.m_fNavStoppingDis = 0.125f;
		base.Init("profession/warrior_inst", layer, pos, isUser);
		base.setNavLay(NavmeshUtils.listARE[1]);
		P2Warrior_Event p2Warrior_Event = this.m_curModel.gameObject.AddComponent<P2Warrior_Event>();
		p2Warrior_Event.m_linkProfessionRole = this;
		this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 0f);
		base.set_body(0, 0);
		base.set_weaponr(0, 0);
		this.m_skill2005_time = ModelBase<Skill_a3Model>.getInstance().skilldic[2005].eff_last;
		this.m_skill2010_time = ModelBase<Skill_a3Model>.getInstance().skilldic[2010].eff_last;
	}

	public override void PlaySkill(int id)
	{
		bool flag = this.m_curSkillId != 0;
		if (!flag)
		{
			bool isMain = this.m_isMain;
			if (isMain)
			{
				this.m_moveAgent.avoidancePriority = 50;
			}
			this.m_curSkillId = id;
			bool flag2 = 2003 == this.m_curAni.GetInteger(EnumAni.ANI_I_SKILL);
			if (!flag2)
			{
				bool flag3 = 2005 == id && base.getShowSkillEff() != 3;
				if (flag3)
				{
					this.runSkill_2005();
				}
				bool flag4 = 2010 == id && base.getShowSkillEff() != 3;
				if (flag4)
				{
					this.runSkill_2010();
				}
				bool flag5 = 2009 == id && base.getShowSkillEff() != 3;
				if (flag5)
				{
					this.runSkill_2009();
				}
				bool flag6 = id == 2006 && base.getShowSkillEff() != 3;
				if (flag6)
				{
					this.runSkill_2006();
				}
				this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, id);
				bool flag7 = 2003 == id;
				if (flag7)
				{
					this.m_fAttackCount = 3.5f;
					bool flag8 = this.m_moveAgent;
					if (flag8)
					{
						this.m_moveAgent.updateRotation = true;
						this.m_moveAgent.updatePosition = true;
						this.m_fSkillShowTime = 3.5f;
						this.m_moveAgent.speed = 3f;
					}
				}
				else
				{
					this.m_fAttackCount = 1f;
				}
			}
		}
	}

	public override void StartMove(float joy_x, float joy_y)
	{
		bool flag = !base.canMove;
		if (!flag)
		{
			this.moving = true;
			bool flag2 = 2003 == this.m_curAni.GetInteger(EnumAni.ANI_I_SKILL);
			if (flag2)
			{
				float num = 0.06f;
				float x = (SceneCamera.m_right.x * joy_x + SceneCamera.m_forward.x * joy_y) * num;
				float z = (SceneCamera.m_right.y * joy_x + SceneCamera.m_forward.y * joy_y) * num;
				Vector3 b = new Vector3(x, 0f, z);
				Vector3 position = this.m_curModel.position + b;
				this.m_curModel.position = position;
			}
			else
			{
				bool flag3 = this.m_fSkillShowTime > 0f;
				if (!flag3)
				{
					float x2 = SceneCamera.m_right.x * joy_x + SceneCamera.m_forward.x * joy_y;
					float z2 = SceneCamera.m_right.y * joy_x + SceneCamera.m_forward.y * joy_y;
					Vector3 forward = new Vector3(x2, 0f, z2);
					this.m_curModel.forward = forward;
					this.m_curAni.SetBool(EnumAni.ANI_RUN, true);
				}
			}
		}
	}

	public void runSkill_2005()
	{
		bool flag = this.m_SFX1 == null;
		if (flag)
		{
			this.m_SFX1 = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX2);
			this.m_SFX1.transform.SetParent(this.m_curModel, false);
			this.m_SFX1.SetActive(false);
		}
		bool flag2 = this.process_2005 == null;
		if (flag2)
		{
			this.process_2005 = new TickItem(new Action<float>(this.onUpdate_2005));
			TickMgr.instance.addTick(this.process_2005);
		}
		this.m_cur2005_time = 0f;
	}

	private void onUpdate_2005(float s)
	{
		bool flag = this.m_curModel == null;
		if (flag)
		{
			TickMgr.instance.removeTick(this.process_2005);
		}
		else
		{
			this.m_cur2005_time += s;
			bool flag2 = this.m_cur2005_time > 1f;
			if (flag2)
			{
				this.m_SFX1.SetActive(true);
			}
			bool flag3 = this.m_cur2005_time > this.m_skill2005_time;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.m_SFX1);
				this.m_cur2005_time = 0f;
				TickMgr.instance.removeTick(this.process_2005);
				this.process_2005 = null;
				this.m_SFX1 = null;
			}
		}
	}

	public void runSkill_2010()
	{
		bool flag = this.m_SFX2 == null;
		if (flag)
		{
			this.m_SFX2 = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX2);
			this.m_SFX2.transform.SetParent(this.m_curModel, false);
			this.m_SFX2.SetActive(false);
		}
		bool flag2 = this.process_2010 == null;
		if (flag2)
		{
			this.process_2010 = new TickItem(new Action<float>(this.onUpdate_2010));
			TickMgr.instance.addTick(this.process_2010);
		}
		this.m_cur2010_time = 0f;
	}

	private void onUpdate_2010(float s)
	{
		bool flag = this.m_curModel == null;
		if (flag)
		{
			TickMgr.instance.removeTick(this.process_2010);
		}
		else
		{
			this.m_cur2010_time += s;
			bool flag2 = this.m_cur2010_time > 1f;
			if (flag2)
			{
				this.m_SFX2.SetActive(true);
			}
			bool flag3 = this.m_cur2010_time > this.m_skill2010_time;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.m_SFX2);
				this.m_cur2010_time = 0f;
				TickMgr.instance.removeTick(this.process_2010);
				this.process_2010 = null;
				this.m_SFX2 = null;
			}
		}
	}

	public void runSkill_2009()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX3);
		UnityEngine.Object.Destroy(gameObject, 3.5f);
		gameObject.transform.SetParent(this.m_curModel, false);
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX4);
		UnityEngine.Object.Destroy(gameObject2, 3.5f);
		bool flag = this.m_curModel.FindChild("Spine") != null;
		if (flag)
		{
			gameObject2.transform.SetParent(this.m_curModel.FindChild("Spine"), false);
		}
	}

	public void runSkill_2006()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX5);
		UnityEngine.Object.Destroy(gameObject, 2f);
		gameObject.transform.SetParent(this.m_curModel, false);
	}
}
