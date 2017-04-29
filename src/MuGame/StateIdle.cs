using System;
using UnityEngine;

namespace MuGame
{
	internal class StateIdle : StateBase
	{
		public static StateIdle Instance = new StateIdle();

		public override void Enter()
		{
			ProfessionRole expr_06 = SelfRole._inst;
			if (expr_06 != null)
			{
				NavMeshAgent expr_11 = expr_06.m_moveAgent;
				if (expr_11 != null)
				{
					expr_11.Stop();
				}
			}
		}

		public override void Execute(float delta_time)
		{
			bool flag = SelfRole.fsm.Autofighting && !SelfRole.fsm.IsPause;
			if (flag)
			{
				bool flag2 = (SelfRole._inst.m_LockRole != null && ModelBase<PlayerModel>.getInstance().pk_state != PK_TYPE.PK_PEACE) || (SelfRole._inst.m_LockRole is MonsterRole && ModelBase<PlayerModel>.getInstance().pk_state == PK_TYPE.PK_PEACE);
				if (flag2)
				{
					SelfRole.fsm.ChangeState(StateAttack.Instance);
				}
				else
				{
					SelfRole.fsm.ChangeState(StateSearch.Instance);
				}
			}
			bool flag3 = SelfRole.fsm.AutoCollect && !SelfRole.fsm.IsPause;
			if (flag3)
			{
				SelfRole.fsm.ChangeState(StateCollect.Instance);
			}
		}

		public override void Exit()
		{
		}
	}
}
