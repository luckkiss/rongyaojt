using System;

namespace MuGame
{
	internal class StateGlobal : StateBase
	{
		public static StateGlobal Instance = new StateGlobal();

		public override void Enter()
		{
		}

		public override void Execute(float delta_time)
		{
			bool isDead = SelfRole._inst.isDead;
			if (isDead)
			{
				SelfRole.fsm.ChangeState(StateDie.Instance);
			}
		}

		public override void Exit()
		{
		}
	}
}
