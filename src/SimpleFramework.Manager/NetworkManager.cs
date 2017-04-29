using Cross;
using GameFramework;
using LuaInterface;
using System;
using System.Collections.Generic;

namespace SimpleFramework.Manager
{
	public class NetworkManager : View
	{
		protected SessionFuncMgr sessionFuncMgr;

		protected NetClient netClient;

		public Variant a;

		protected Dictionary<uint, LuaFunction> poolRpc = new Dictionary<uint, LuaFunction>();

		private void init()
		{
			this.sessionFuncMgr = SessionFuncMgr.instance;
			this.netClient = NetClient.instance;
			this.sessionFuncMgr._luaFunc = new SessionFuncMgr.luaDelegate(this.proxyHandle);
		}

		public void addProxyListener(uint id, LuaFunction handle)
		{
			if (this.netClient == null)
			{
				this.init();
			}
			if (this.sessionFuncMgr == null)
			{
				return;
			}
			this.poolRpc[id] = handle;
		}

		private bool proxyHandle(uint id, Variant v)
		{
			if (!this.poolRpc.ContainsKey(id))
			{
				return false;
			}
			this.poolRpc[id].CallParams(new object[]
			{
				v
			});
			return true;
		}

		public void sendRPC(uint cmd, Variant v = null)
		{
			if (v == null)
			{
				v = new Variant();
			}
			this.netClient.sendRpc(cmd, v);
		}

		public Variant newVariant()
		{
			return new Variant();
		}

		public Variant writeInt(Variant v, string name, int num)
		{
			v[name] = num;
			return v;
		}

		public int getInt(Variant v, string name)
		{
			return v[name]._int;
		}
	}
}
