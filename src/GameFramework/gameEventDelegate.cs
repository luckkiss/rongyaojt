using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public class gameEventDelegate : BaseCrossPlugin
	{
		private List<taskEvent> _taskList = new List<taskEvent>();

		public override string pluginName
		{
			get
			{
				return "gameEventDelegate";
			}
		}

		public void addEventTask(GameEvent evt, Action<GameEvent> listenerFun)
		{
			taskEvent taskEvent = taskEvent.alloc();
			taskEvent.evt = evt;
			taskEvent.listenerFun = listenerFun;
			this._taskList.Add(taskEvent);
		}

		public override void onProcess(float tmSlice)
		{
			this._process_updata(tmSlice);
		}

		private void _process_updata(float tmSlice)
		{
			while (this._taskList.Count > 0)
			{
				taskEvent taskEvent = this._taskList[0];
				this._taskList.RemoveAt(0);
				double num = (double)CCTime.getTickMillisec();
				taskEvent.listenerFun(taskEvent.evt);
				double procTm = (double)CCTime.getTickMillisec() - num;
				ProfilerManager.inst.profilerMark(procTm, "gameEventDelegate", taskEvent.evt.type.ToString());
				taskEvent.free(taskEvent);
			}
		}
	}
}
