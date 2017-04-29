using System;

namespace Cross
{
	public class MsgProcdures
	{
		protected Session _session;

		public Session session
		{
			get
			{
				return this._session;
			}
		}

		public MsgProcdures(Session s)
		{
			this._session = s;
		}

		public virtual void regMsgs(NetManager mgr)
		{
		}
	}
}
