using MuGame;
using System;
using UnityEngine;

public class P5Assassin : ProfessionRole
{
	public static GameObject ASSASSIN_SFX1;

	public static GameObject ASSASSIN_SFX2;

	public static GameObject ASSASSIN_SFX3;

	private float m_skill5010_time = 60f;

	public new void Init(string name, int layer, Vector3 pos, bool isUser = false)
	{
		this.m_strAvatarPath = "profession/assa/";
		this.m_strEquipEffPath = "Fx/armourFX/assa/";
		base.roleName = name;
		base.Init("profession/assa_inst", layer, pos, isUser);
		base.setNavLay(NavmeshUtils.listARE[1]);
		P5Assassin_Event p5Assassin_Event = this.m_curModel.gameObject.AddComponent<P5Assassin_Event>();
		p5Assassin_Event.m_linkProfessionRole = this;
		this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 0f);
		base.set_weaponr(0, 0);
		base.set_weaponl(0, 0);
		base.set_body(0, 0);
		this.m_skill5010_time = ModelBase<Skill_a3Model>.getInstance().skilldic[5010].eff_last;
	}

	public override void PlaySkill(int id)
	{
		bool flag = this.m_curSkillId != 0;
		if (!flag)
		{
			bool flag2 = 5005 == id && base.getShowSkillEff() != 3;
			if (flag2)
			{
				this.runSkill_5005();
			}
			bool flag3 = 5010 == id && base.getShowSkillEff() != 3;
			if (flag3)
			{
				this.runSkill_5010();
			}
			bool flag4 = 5003 == this.m_curAni.GetInteger(EnumAni.ANI_I_SKILL);
			if (!flag4)
			{
				this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, id);
				this.m_curSkillId = id;
				bool isMain = this.m_isMain;
				if (isMain)
				{
					this.m_moveAgent.avoidancePriority = 50;
				}
				this.m_fAttackCount = 1f;
				bool flag5 = 5003 == id;
				if (flag5)
				{
					this.m_fAttackCount = 2.5f;
					bool flag6 = !this.m_isMain;
					if (flag6)
					{
						this.m_moveAgent.updateRotation = true;
						this.m_moveAgent.updatePosition = true;
						this.m_fSkillShowTime = 2.5f;
						this.m_moveAgent.speed = 5f;
					}
				}
				bool flag7 = 5006 == id || 5005 == id || 5010 == id;
				if (flag7)
				{
					this.m_fAttackCount = 0.5f;
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
			bool flag2 = 5003 == this.m_curAni.GetInteger(EnumAni.ANI_I_SKILL);
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

	public void runSkill_5005()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P5Assassin.ASSASSIN_SFX2);
		gameObject.transform.SetParent(this.m_curModel, false);
		UnityEngine.Object.Destroy(gameObject, 10f);
	}

	public void runSkill_5010()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P5Assassin.ASSASSIN_SFX3);
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(P5Assassin.ASSASSIN_SFX3);
		gameObject.transform.SetParent(this.m_curModel.transform.FindChild("Weapon_L"), false);
		gameObject2.transform.SetParent(this.m_curModel.transform.FindChild("Weapon_R"), false);
		UnityEngine.Object.Destroy(gameObject, this.m_skill5010_time);
		UnityEngine.Object.Destroy(gameObject2, this.m_skill5010_time);
	}
}
