using System;
using UnityEngine;

namespace MuGame
{
	internal class StateCollect : StateBase
	{
		public static StateCollect Instance = new StateCollect();

		private CollectRole collTarget = null;

		public bool collecting;

		public override void Enter()
		{
			this.collecting = false;
		}

		public override void Execute(float delta_time)
		{
			bool flag = this.collTarget == null;
			if (flag)
			{
				foreach (MonsterRole current in MonsterMgr._inst.m_mapMonster.Values)
				{
					bool flag2 = current is CollectRole && ModelBase<A3_TaskModel>.getInstance().IfCurrentCollectItem(current.monsterid);
					if (flag2)
					{
						CollectRole collectRole = current as CollectRole;
						bool flag3 = !collectRole.becollected;
						if (flag3)
						{
							this.collTarget = collectRole;
							break;
						}
					}
				}
				bool flag4 = this.collTarget != null;
				if (flag4)
				{
					SelfRole._inst.TurnToPos(this.collTarget.m_curModel.transform.position);
					SelfRole._inst.SetDestPos(this.collTarget.m_curModel.transform.position);
					bool flag5 = !SelfRole._inst.m_moveAgent.hasPath;
					if (flag5)
					{
						SelfRole._inst.m_moveAgent.ResetPath();
						SelfRole._inst.m_moveAgent.Stop();
						return;
					}
					SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, true);
				}
			}
			bool flag6 = this.collTarget == null;
			if (flag6)
			{
				SelfRole._inst.m_moveAgent.ResetPath();
				SelfRole._inst.m_moveAgent.Stop();
				SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, false);
			}
			else
			{
				bool flag7 = !this.collecting;
				if (flag7)
				{
					bool flag8 = Vector3.Distance(this.collTarget.m_curModel.transform.position, SelfRole._inst.m_curModel.transform.position) <= 2f;
					if (flag8)
					{
						this.collTarget.onClick();
						this.collecting = true;
					}
				}
			}
		}

		public override void Exit()
		{
			SelfRole._inst.m_moveAgent.ResetPath();
			SelfRole._inst.m_moveAgent.Stop();
			SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, false);
			this.collecting = false;
			SelfRole.fsm.AutoCollect = false;
			this.collTarget = null;
		}
	}
}
