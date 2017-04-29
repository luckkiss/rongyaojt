using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class BaseProxy<T> : GameEventDispatcher where T : class, new()
	{
		protected SessionFuncMgr sessionFuncMgr;

		protected NetClient netClient;

		private static T _instance;

		public BaseProxy()
		{
			this.sessionFuncMgr = SessionFuncMgr.instance;
			this.netClient = NetClient.instance;
		}

		protected virtual void addProxyListener(uint id, Action<Variant> handle)
		{
			bool flag = this.sessionFuncMgr == null;
			if (!flag)
			{
				this.sessionFuncMgr.addFunc(id, handle, true);
			}
		}

		protected virtual void sendRPC(uint cmd, Variant v = null)
		{
			bool flag = v == null;
			if (flag)
			{
				v = new Variant();
			}
			this.netClient.sendRpc(cmd, v);
		}

		protected virtual void sendTPKG(uint cmd, Variant v)
		{
			this.netClient.sendTpkg(cmd, v);
		}

		public static T getInstance()
		{
			bool flag = BaseProxy<T>._instance == null;
			if (flag)
			{
				bool flag2 = BaseProxy<T>._instance == null;
				if (flag2)
				{
					BaseProxy<T>._instance = Activator.CreateInstance<T>();
				}
			}
			return BaseProxy<T>._instance;
		}
	}
}
