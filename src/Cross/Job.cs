using System;

namespace Cross
{
	public abstract class Job
	{
		protected JobQueue m_queue;

		public JobQueue owner
		{
			set
			{
				this.m_queue = value;
			}
		}

		public abstract void execute();

		protected void doNext()
		{
			bool flag = this.m_queue != null;
			if (flag)
			{
				this.m_queue.doNext();
			}
		}
	}
}
