using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public class processQueue
	{
		private List<IProcess> _processVec = new List<IProcess>();

		private string _debugFlag = "";

		private int _limittmMS = 10;

		public void setDebug(string val)
		{
			this._debugFlag = val;
		}

		public void setLimittm(int ms)
		{
			this._limittmMS = ms;
		}

		private void _onProcess(float tmSlice)
		{
			List<IProcess> list = new List<IProcess>();
			double num = (double)CCTime.getTickMillisec();
			for (int i = 0; i < this._processVec.Count; i++)
			{
				IProcess process = this._processVec[i];
				bool flag = process == null;
				if (flag)
				{
					GameTools.PrintError("processQueue onProcess null!");
				}
				else
				{
					bool pause = process.pause;
					if (!pause)
					{
						bool destroy = process.destroy;
						if (destroy)
						{
							list.Add(process);
						}
						else
						{
							double num2 = (double)CCTime.getTickMillisec();
							process.updateProcess(tmSlice);
							double procTm = (double)CCTime.getTickMillisec() - num2;
							ProfilerManager.inst.profilerMark(procTm, this._debugFlag, process.processName);
							bool flag2 = num - num2 > (double)this._limittmMS;
							if (flag2)
							{
								break;
							}
						}
					}
				}
			}
			foreach (IProcess current in list)
			{
				for (int i = this._processVec.Count - 1; i >= 0; i--)
				{
					IProcess process = this._processVec[i];
					bool flag3 = current != process;
					if (!flag3)
					{
						this._processVec.RemoveAt(i);
						break;
					}
				}
			}
		}

		public void process(float tmSlice)
		{
			this._onProcess(tmSlice);
		}

		public void addProcess(IProcess p)
		{
			bool flag = p == null;
			if (flag)
			{
				GameTools.PrintError("processQueue addProcess null!");
			}
			else
			{
				bool flag2 = p.processName == "";
				if (flag2)
				{
					GameTools.PrintError("processQueue addProcess processName null!");
				}
				bool destroy = p.destroy;
				if (destroy)
				{
					p.destroy = false;
				}
				this._processVec.Add(p);
			}
		}

		public void removeProcess(IProcess p)
		{
			p.destroy = true;
		}
	}
}
