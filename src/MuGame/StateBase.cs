using System;

namespace MuGame
{
	public abstract class StateBase
	{
		public abstract void Enter();

		public abstract void Execute(float delta_time);

		public abstract void Exit();
	}
}
