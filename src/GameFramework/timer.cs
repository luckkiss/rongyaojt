using System;

namespace GameFramework
{
	internal class timer : ITimer, IProcess
	{
		private timersManager _mgr = null;

		private bool _destroy = false;

		private bool _pause = true;

		private string _processName;

		private int _delayTime = 0;

		private int _cnt = 0;

		private Action<object> _callBack = null;

		private object _userData = null;

		private float _overTime = 0f;

		private float _overCnt = 0f;

		private long _id = 0L;

		private static long _idCount = 0L;

		public long id
		{
			get
			{
				return this._id;
			}
		}

		public bool destroy
		{
			get
			{
				return this._destroy;
			}
			set
			{
				this._destroy = value;
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

		public static timer create(timersManager mgr, int delayTime, Action<object> callBack, int cnt = 1, object data = null)
		{
			return new timer(mgr, delayTime, callBack, cnt, data);
		}

		public timer(timersManager mgr, int delayTime, Action<object> callBack, int cnt, object data)
		{
			this._mgr = mgr;
			timer._idCount += 1L;
			this._id = timer._idCount;
			this._delayTime = delayTime;
			this._cnt = cnt;
			this._userData = data;
			this._callBack = callBack;
		}

		public void start()
		{
			this.pause = false;
		}

		public void updateProcess(float tmSlice)
		{
			this._overTime += tmSlice;
			bool flag = this._overTime >= (float)this._delayTime;
			if (flag)
			{
				this._overCnt += 1f;
				this._overTime = 0f;
				this._callBack(this._userData);
			}
			bool flag2 = this._cnt != 0 && this._overCnt >= (float)this._cnt;
			if (flag2)
			{
				this._mgr.removeTimer(this.id);
			}
		}
	}
}
