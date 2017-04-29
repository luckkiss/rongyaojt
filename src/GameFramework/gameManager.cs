using System;

namespace GameFramework
{
	public abstract class gameManager : clientBase
	{
		public timersManager timers = new timersManager();

		public gameManager(gameMain m) : base(m)
		{
		}

		public override void init()
		{
			GMCommand.inst.setNetManager(base.g_netM);
			base.g_processM.addRender(new processStruct(new Action<float>(this.update), "timersManager", false, false), false);
			this.onInit();
		}

		protected abstract void onInit();

		private void update(float tmSlice)
		{
		}
	}
}
