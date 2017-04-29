using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public class timersManager
	{
		private Dictionary<long, ITimer> _timers = new Dictionary<long, ITimer>();

		private processManager g_processM
		{
			get
			{
				return CrossApp.singleton.getPlugin("processManager") as processManager;
			}
		}

		public long addTimer(int delayTime, Action<object> callBack, int cnt = 1, object data = null)
		{
			ITimer timer = timer.create(this, delayTime, callBack, cnt, data);
			this.g_processM.addProcess(timer, false);
			long id = timer.id;
			this._timers[id] = timer;
			timer.start();
			return id;
		}

		public void removeTimer(long id)
		{
			bool flag = !this._timers.ContainsKey(id);
			if (flag)
			{
				GameTools.PrintError("removeTimer id[" + id + "] err!");
			}
			else
			{
				ITimer p = this._timers[id];
				this.g_processM.removeProcess(p, false);
			}
		}
	}
}
