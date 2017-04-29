using System;

namespace Cross
{
	public class MsgProcesser
	{
		public uint msgId;

		public Variant msgData;

		public Session session;

		public virtual int priority
		{
			get
			{
				return 0;
			}
		}

		public virtual bool immiProc
		{
			get
			{
				return true;
			}
		}

		public virtual bool discardable
		{
			get
			{
				return false;
			}
		}

		public virtual bool orderStrict
		{
			get
			{
				return true;
			}
		}

		public virtual uint msgID
		{
			get
			{
				return 0u;
			}
		}

		public virtual uint msgType
		{
			get
			{
				return 0u;
			}
		}

		public void Process()
		{
			double num = (double)CCTime.getTickMillisec();
			this._onProcess();
			double procTm = (double)CCTime.getTickMillisec() - num;
			_MsgProfilerMgr.inst.getMsgProfiler(this.msgType, this.msgID).onProc(procTm);
		}

		protected virtual void _onProcess()
		{
		}

		public virtual void Dispose()
		{
			this.msgData = null;
			this.session = null;
		}
	}
}
