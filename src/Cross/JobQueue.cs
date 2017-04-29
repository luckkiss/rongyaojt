using System;
using System.Collections.Generic;

namespace Cross
{
	public class JobQueue
	{
		protected LinkedList<Job> m_queque = new LinkedList<Job>();

		protected Action m_onFin = null;

		public Action onFin
		{
			get
			{
				return this.m_onFin;
			}
			set
			{
				this.m_onFin = value;
			}
		}

		public JobQueue()
		{
		}

		public JobQueue(Job[] jobs)
		{
			for (int i = 0; i < jobs.Length; i++)
			{
				jobs[i].owner = this;
				this.m_queque.AddLast(jobs[i]);
			}
		}

		public void addJob(Job job)
		{
			job.owner = this;
			this.m_queque.AddLast(job);
		}

		public void execute()
		{
			bool flag = this.m_queque.Count == 0;
			if (flag)
			{
				bool flag2 = this.m_onFin != null;
				if (flag2)
				{
					this.m_onFin();
				}
			}
			else
			{
				Job value = this.m_queque.First.Value;
				this.m_queque.RemoveFirst();
				value.execute();
			}
		}

		public void doNext()
		{
			this.execute();
		}
	}
}
