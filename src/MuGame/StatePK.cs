using System;
using UnityEngine;

namespace MuGame
{
	internal class StatePK : StateBase
	{
		public static StatePK Instance = new StatePK();

		private Vector3 startPos;

		private float timer = 0f;

		public ProfessionRole Enemy;

		private bool TargetLost
		{
			get;
			set;
		}

		public override void Enter()
		{
			this.startPos = SelfRole._inst.m_curModel.position;
			SelfRole._inst.m_LockRole = this.Enemy;
		}

		public override void Execute(float delta_time)
		{
			this.timer += delta_time;
			bool flag = this.timer < 0.1f;
			if (!flag)
			{
				this.timer -= 1f;
				bool flag2 = this.TargetLost || Vector3.Distance(this.startPos, SelfRole._inst.m_curModel.position) > StateInit.Instance.PKDistance || this.Enemy.isDead;
				if (flag2)
				{
					Vector3 nearestWayPoint = StateInit.Instance.GetNearestWayPoint();
					StateAutoMoveToPos.Instance.pos = nearestWayPoint;
					StateAutoMoveToPos.Instance.stopdistance = 2f;
					SelfRole.fsm.ChangeState(StateAutoMoveToPos.Instance);
				}
				else
				{
					int skillCanUse = StateInit.Instance.GetSkillCanUse();
					skill_a3Data skill_a3Data = null;
					ModelBase<Skill_a3Model>.getInstance().skilldic.TryGetValue(skillCanUse, out skill_a3Data);
					float num = (float)skill_a3Data.range / 53.333f;
					this.TargetLost = (this.Enemy.m_curModel == null);
					float num2 = Vector3.Distance(this.Enemy.m_curModel.position, SelfRole._inst.m_curModel.position);
					bool flag3 = num2 > num;
					if (flag3)
					{
						SelfRole._inst.m_moveAgent.destination = this.Enemy.m_curModel.position;
						SelfRole._inst.m_moveAgent.stoppingDistance = num - 0.125f;
						SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, true);
					}
					else
					{
						SelfRole._inst.m_moveAgent.Stop();
						SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, false);
						SelfRole._inst.TurnToRole(this.Enemy, true);
						bool flag4 = skillbar.instance.playSkillById(skillCanUse, false);
						bool flag5 = flag4 && StateInit.Instance.PreferedSkill == skillCanUse;
						if (flag5)
						{
							StateInit.Instance.PreferedSkill = -1;
						}
					}
				}
			}
		}

		public override void Exit()
		{
			SelfRole._inst.m_moveAgent.Stop();
			SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, false);
		}

		public StatePK()
		{
			this.<TargetLost>k__BackingField = false;
			base..ctor();
		}
	}
}
