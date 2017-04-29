using System;

namespace MuGame
{
	internal class StateDie : StateBase
	{
		public static StateDie Instance = new StateDie();

		private bool hasSend = false;

		public override void Enter()
		{
			this.hasSend = false;
		}

		public override void Execute(float delta_time)
		{
			bool flag = !SelfRole._inst.isDead;
			if (flag)
			{
				SelfRole.fsm.ChangeState(StateIdle.Instance);
			}
		}

		public override void Exit()
		{
			this.hasSend = true;
		}
	}
}
