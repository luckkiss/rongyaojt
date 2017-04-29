using System;
using System.Collections.Generic;

namespace Cross
{
	public class ProfilerManager
	{
		private static ProfilerManager _inst;

		protected Dictionary<string, Profiler> _profilerDict = new Dictionary<string, Profiler>();

		public static ProfilerManager inst
		{
			get
			{
				bool flag = ProfilerManager._inst == null;
				if (flag)
				{
					ProfilerManager._inst = new ProfilerManager();
				}
				return ProfilerManager._inst;
			}
		}

		public void clear()
		{
			this._profilerDict.Clear();
		}

		public void profilerMark(double procTm, string name, string subName = "")
		{
			bool flag = !this._profilerDict.ContainsKey(name);
			Profiler profiler;
			if (flag)
			{
				profiler = new Profiler();
				bool flag2 = subName != "";
				if (flag2)
				{
					profiler.name = name + "_" + subName;
				}
				else
				{
					profiler.name = name;
				}
				this._profilerDict[name] = profiler;
			}
			else
			{
				profiler = this._profilerDict[name];
			}
			profiler.onProc(procTm);
		}

		public string dumpProfile(string name = "")
		{
			string text = string.Empty;
			foreach (Profiler current in this._profilerDict.Values)
			{
				bool flag = name != "" && current.name != name;
				if (!flag)
				{
					text += current.dumpProfile();
				}
			}
			return text;
		}
	}
}
