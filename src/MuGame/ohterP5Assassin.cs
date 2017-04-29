using System;
using UnityEngine;

namespace MuGame
{
	internal class ohterP5Assassin : MonsterPlayer
	{
		public static GameObject ASSASSIN_SFX1;

		public static GameObject ASSASSIN_SFX2;

		public static GameObject ASSASSIN_SFX3;

		public new void Init(string name, int layer, Vector3 pos, bool isUser = false)
		{
			this.m_strAvatarPath = "profession/assa/";
			base.roleName = name;
			base.Init("profession/assa_inst", layer, pos, isUser);
			M0x000_Role_Event m0x000_Role_Event = this.m_curModel.gameObject.AddComponent<M0x000_Role_Event>();
			m0x000_Role_Event.m_monRole = this;
			MonHurtPoint monHurtPoint = this.m_curPhy.gameObject.AddComponent<MonHurtPoint>();
			monHurtPoint.m_monRole = this;
			base.setNavLay(NavmeshUtils.listARE[1]);
			this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 0f);
			base.set_weaponr(0, 0);
			base.set_weaponl(0, 0);
			base.set_body(0, 0);
		}

		public override void PlaySkill(int id)
		{
			bool flag = 5005 == id;
			if (flag)
			{
				this.runSkill_5005();
			}
			bool flag2 = 5010 == id;
			if (flag2)
			{
				this.runSkill_5010();
			}
			bool flag3 = 5003 == this.m_curAni.GetInteger(EnumAni.ANI_I_SKILL);
			if (!flag3)
			{
				this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, id);
				this.m_curSkillId = id;
				bool isMain = this.m_isMain;
				if (isMain)
				{
					this.m_moveAgent.avoidancePriority = 50;
				}
				this.m_fAttackCount = 1f;
				bool flag4 = 5003 == id;
				if (flag4)
				{
					this.m_fAttackCount = 2.5f;
					bool flag5 = !this.m_isMain;
					if (flag5)
					{
						this.m_moveAgent.updateRotation = true;
						this.m_moveAgent.updatePosition = true;
						this.m_fSkillShowTime = 2.5f;
						this.m_moveAgent.speed = 5f;
					}
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ohterP5Assassin.ASSASSIN_SFX1);
					UnityEngine.Object.Destroy(gameObject, 2.5f);
					gameObject.transform.SetParent(this.m_curModel, false);
				}
				bool flag6 = 5006 == id || 5005 == id || 5010 == id;
				if (flag6)
				{
					this.m_fAttackCount = 0.5f;
				}
			}
		}

		public void runSkill_5005()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ohterP5Assassin.ASSASSIN_SFX2);
			gameObject.transform.SetParent(this.m_curModel, false);
			UnityEngine.Object.Destroy(gameObject, 10f);
		}

		public void runSkill_5010()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ohterP5Assassin.ASSASSIN_SFX3);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(ohterP5Assassin.ASSASSIN_SFX3);
			gameObject.transform.SetParent(this.m_curModel.transform.FindChild("Weapon_L"), false);
			gameObject2.transform.SetParent(this.m_curModel.transform.FindChild("Weapon_R"), false);
			UnityEngine.Object.Destroy(gameObject, 60f);
			UnityEngine.Object.Destroy(gameObject2, 60f);
		}
	}
}
