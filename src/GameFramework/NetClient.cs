using Cross;
using System;

namespace GameFramework
{
	public abstract class NetClient : clientBase, IProcess
	{
		public static NetClient instance;

		private ClientSession _session;

		private IConnection _conn;

		private Variant connectData;

		private bool _pause = false;

		private bool _destory = false;

		private string _processName = "";

		private NetManager _netM
		{
			get
			{
				return CrossApp.singleton.getPlugin("net") as NetManager;
			}
		}

		public bool isConnect
		{
			get
			{
				bool flag = this._conn == null;
				return !flag && this._conn.isConnect;
			}
		}

		public int CurServerTimeStamp
		{
			get
			{
				return this._conn.CurServerTimeStamp;
			}
		}

		public int curServerPing
		{
			get
			{
				return this._conn.Latency;
			}
		}

		public long CurServerTimeStampMS
		{
			get
			{
				return this._conn.CurServerTimeStampMS;
			}
		}

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
				return "NetClient";
			}
			set
			{
				this._processName = value;
			}
		}

		public NetClient(gameMain m) : base(m)
		{
			NetClient.instance = this;
		}

		public override void init()
		{
			this._conn = new ConnectionImpl();
			this._session = new ClientSession(this._conn, this._netM);
			this._session.setNetClient(this);
			base.g_processM.addProcess(this, false);
			this.onInit();
		}

		public void regRPCProcesser(uint msgID, NetManager.RPCProcCreator procCrtFunc)
		{
			this._netM.regRPCProcesser(msgID, procCrtFunc);
		}

		public void regTpkgProcesser(uint msgID, NetManager.TPKGProcCreator procCrtFunc)
		{
			this._netM.regTpkgProcesser(msgID, procCrtFunc);
		}

		protected abstract void onInit();

		public void onHBSend()
		{
		}

		public void onHBRecv()
		{
		}

		public void onError(string msg)
		{
			base.dispatchEvent(GameEvent.Create(3013u, this, null, false));
		}

		public void onConnect()
		{
			base.dispatchEvent(GameEvent.Create(3012u, this, null, false));
		}

		public void onServerVersionRecv()
		{
			base.dispatchEvent(GameEvent.Create(3014u, this, null, false));
		}

		public void onConnectionClose()
		{
			base.dispatchEvent(GameEvent.Create(3015u, this, null, false));
		}

		public void onConnectFailed()
		{
			base.dispatchEvent(GameEvent.Create(3016u, this, null, false));
		}

		public void onLogin(Variant data)
		{
			base.dispatchEvent(GameEvent.Create(3021u, this, data, false));
		}

		public void sendRpc(uint cmd, Variant msg)
		{
			bool flag = cmd != 9u;
			if (flag)
			{
				GameTools.PrintDetial(string.Concat(new object[]
				{
					"sendRpc cmd[",
					cmd,
					"] msg:",
					msg.dump()
				}));
			}
			this._conn.PSendRPC(cmd, msg);
		}

		public void sendTpkg(uint cmd, Variant msg)
		{
			this._conn.PSendTPKG(cmd, msg);
		}

		public bool connect(string addr, int port, uint uid, string token, uint client, Variant sinfo)
		{
			this.connectData = new Variant();
			this.connectData["addr"] = addr;
			this.connectData["port"] = port;
			this.connectData["uid"] = uid;
			this.connectData["token"] = token;
			this.connectData["client"] = client;
			this.connectData["sinfo"] = sinfo;
			return this._session.connect(addr, port, uid, token, client);
		}

		public void reConnect(Variant v = null)
		{
			bool flag = v != null;
			if (flag)
			{
				this.connectData = v;
			}
			bool flag2 = this.connectData == null;
			if (!flag2)
			{
				this.connect(this.connectData["addr"], this.connectData["port"], this.connectData["uid"], this.connectData["token"], this.connectData["client"], this.connectData["sinfo"]);
			}
		}

		public void reqServerVersion()
		{
			this._conn.RequestServerVersion();
		}

		public Variant getServerVersionInfo()
		{
			return this._conn.ConfigVersions;
		}

		public string rpcProfileDump()
		{
			return this._session.rpcProfileDump();
		}

		public IURLReq getUrlImpl()
		{
			return new URLReqImpl();
		}

		public void disconnect()
		{
			this._conn.Disconnect();
		}

		public void updateProcess(float tmSlice)
		{
			this._conn.onProcess();
		}
	}
}
