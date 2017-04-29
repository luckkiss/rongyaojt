using System;
using UnityEngine;

namespace MuGame
{
	internal class StateMove : StateBase
	{
		public static StateMove Instance = new StateMove();

		public float stop_distance = 0.125f;

		public override void Enter()
		{
			SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, true);
		}

		public override void Execute(float delta_time)
		{
			SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, true);
			BaseRole lockRole = SelfRole._inst.m_LockRole;
			bool flag = lockRole == null;
			if (flag)
			{
				SelfRole.fsm.ChangeState(StateIdle.Instance);
			}
			else
			{
				Vector3 position = lockRole.m_curModel.position;
				Vector3 position2 = SelfRole._inst.m_curModel.position;
				SelfRole._inst.TurnToPos(position);
				bool flag2 = Vector3.Distance(position, position2) < this.stop_distance;
				if (flag2)
				{
					SelfRole.fsm.ChangeState(StateAttack.Instance);
				}
				else
				{
					SelfRole._inst.m_rshelper.CheckMoveAgent(delta_time, 0.5f, true);
				}
			}
		}

		public override void Exit()
		{
			SelfRole._inst.StopMove();
		}
	}
}
