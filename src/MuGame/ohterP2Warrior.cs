using System;
using UnityEngine;

namespace MuGame
{
	internal class ohterP2Warrior : MonsterPlayer
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
			base.roleName = name;
			this.m_fNavStoppingDis = 0.125f;
			base.Init("profession/warrior_inst", layer, pos, isUser);
			M0x000_Role_Event m0x000_Role_Event = this.m_curModel.gameObject.AddComponent<M0x000_Role_Event>();
			m0x000_Role_Event.m_monRole = this;
			MonHurtPoint monHurtPoint = this.m_curPhy.gameObject.AddComponent<MonHurtPoint>();
			monHurtPoint.m_monRole = this;
			base.setNavLay(NavmeshUtils.listARE[1]);
			this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 0f);
			base.set_body(1, 1);
			base.set_weaponr(1, 1);
		}

		public override void PlaySkill(int id)
		{
			bool isMain = this.m_isMain;
			if (isMain)
			{
				this.m_moveAgent.avoidancePriority = 50;
			}
			this.m_curSkillId = id;
			bool flag = 2003 == this.m_curAni.GetInteger(EnumAni.ANI_I_SKILL);
			if (!flag)
			{
				bool flag2 = 2005 == id;
				if (flag2)
				{
					this.runSkill_2005();
				}
				bool flag3 = 2010 == id;
				if (flag3)
				{
					this.runSkill_2010();
				}
				bool flag4 = 2009 == id;
				if (flag4)
				{
					this.runSkill_2009();
				}
				bool flag5 = id == 2006;
				if (flag5)
				{
					this.runSkill_2006();
				}
				this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, id);
				bool flag6 = 2003 == id;
				if (flag6)
				{
					this.m_fAttackCount = 3.5f;
					bool flag7 = this.m_moveAgent;
					if (flag7)
					{
						this.m_moveAgent.updateRotation = true;
						this.m_moveAgent.updatePosition = true;
						this.m_fSkillShowTime = 3.5f;
						this.m_moveAgent.speed = 3f;
					}
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ohterP2Warrior.WARRIOR_SFX1);
					UnityEngine.Object.Destroy(gameObject, 3.5f);
					gameObject.transform.SetParent(this.m_curModel, false);
				}
				else
				{
					this.m_fAttackCount = 1f;
				}
			}
		}

		public void runSkill_2005()
		{
			bool flag = this.m_SFX1 == null;
			if (flag)
			{
				this.m_SFX1 = UnityEngine.Object.Instantiate<GameObject>(ohterP2Warrior.WARRIOR_SFX2);
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
				this.m_SFX2 = UnityEngine.Object.Instantiate<GameObject>(ohterP2Warrior.WARRIOR_SFX2);
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
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ohterP2Warrior.WARRIOR_SFX3);
			UnityEngine.Object.Destroy(gameObject, 3.5f);
			gameObject.transform.SetParent(this.m_curModel, false);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(ohterP2Warrior.WARRIOR_SFX4);
			UnityEngine.Object.Destroy(gameObject2, 3.5f);
			bool flag = this.m_curModel.FindChild("Spine") != null;
			if (flag)
			{
				gameObject2.transform.SetParent(this.m_curModel.FindChild("Spine"), false);
			}
		}

		public void runSkill_2006()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ohterP2Warrior.WARRIOR_SFX5);
			UnityEngine.Object.Destroy(gameObject, 2f);
			gameObject.transform.SetParent(this.m_curModel, false);
		}
	}
}
