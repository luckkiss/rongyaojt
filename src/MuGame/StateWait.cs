using System;

namespace MuGame
{
	internal class StateWait : StateBase
	{
		public static StateWait Instance = new StateWait();

		public float wait_time = 0f;

		private float timer = 0f;

		public override void Enter()
		{
			this.timer = 0f;
		}

		public override void Execute(float delta_time)
		{
			this.timer += delta_time;
			bool flag = this.timer < this.wait_time;
			if (!flag)
			{
				SelfRole.fsm.RevertToPreviousState();
			}
		}

		public override void Exit()
		{
			this.timer = 0f;
		}
	}
}
