using System;
using UnityEngine;

namespace MuGame
{
	internal class ohterP3Mage : MonsterPlayer
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
			base.roleName = name;
			base.setNavLay(NavmeshUtils.listARE[1]);
			base.Init("profession/mage_inst", layer, pos, isUser);
			M0x000_Role_Event m0x000_Role_Event = this.m_curModel.gameObject.AddComponent<M0x000_Role_Event>();
			m0x000_Role_Event.m_monRole = this;
			MonHurtPoint monHurtPoint = this.m_curPhy.gameObject.AddComponent<MonHurtPoint>();
			monHurtPoint.m_monRole = this;
			this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 0f);
			base.set_weaponl(0, 0);
			base.set_body(0, 0);
		}

		public override void PlaySkill(int id)
		{
			bool flag = 3008 == id;
			if (flag)
			{
				this.runSkill_3008();
			}
			bool flag2 = 30081 == id;
			if (flag2)
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
			bool flag3 = 3010 == id;
			if (flag3)
			{
				this.m_fAttackCount = 0.5f;
			}
		}

		public void runSkill_3008()
		{
			bool flag = this.m_SFX1 == null;
			if (flag)
			{
				this.m_SFX1 = UnityEngine.Object.Instantiate<GameObject>(ohterP3Mage.P3MAGE_SFX1);
				this.m_SFX1.transform.SetParent(this.m_curModel, false);
				this.m_SFX1.SetActive(false);
			}
			bool flag2 = this.process_3008 == null;
			if (flag2)
			{
				this.process_3008 = new TickItem(new Action<float>(this.onUpdate_3008));
				TickMgr.instance.addTick(this.process_3008);
			}
			this.m_cur3008_time = 0f;
		}

		public void removeSkill_30081()
		{
			bool flag = this.m_SFX1 != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_SFX1);
				this.m_cur3008_time = 0f;
				TickMgr.instance.removeTick(this.process_3008);
				this.process_3008 = null;
				this.m_SFX1 = null;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ohterP3Mage.P3MAGE_SFX3);
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
}
