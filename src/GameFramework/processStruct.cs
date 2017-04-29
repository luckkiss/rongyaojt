using System;

namespace GameFramework
{
	public class processStruct : IProcess
	{
		private bool _pause;

		private bool _destory;

		private string _processName;

		private Action<float> _update;

		public bool destroy
		{
			get
			{
				return this._destory;
			}
			set
			{
				this._destory = value;
			}
		}

		public bool pause
		{
			get
			{
				return this._pause;
			}
			set
			{
				this._pause = value;
			}
		}

		public string processName
		{
			get
			{
				return this._processName;
			}
			set
			{
				this._processName = value;
			}
		}

		public Action<float> update
		{
			set
			{
				this._update = value;
			}
		}

		public processStruct(Action<float> fun, string processName = "", bool pause = false, bool destory = false)
		{
			this._pause = pause;
			this._destory = destory;
			this._update = fun;
			this._processName = processName;
		}

		public static processStruct create(Action<float> fun = null, string processName = "", bool pause = false, bool destory = false)
		{
			return new processStruct(fun, processName, pause, destory);
		}

		public void updateProcess(float tmSlice)
		{
			this._update(tmSlice);
		}
	}
}
