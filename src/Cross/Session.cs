using System;
using System.Collections.Generic;

namespace Cross
{
	public class Session : IConnectionEventHandler
	{
		protected NetManager _netMgr;

		protected IConnection _conn;

		protected List<MsgProcesser> _MsgVec;

		public int type = 0;

		public IConnection connection
		{
			get
			{
				return this._conn;
			}
		}

		public Session(IConnection c, NetManager mgr)
		{
			SessionFuncMgr.init();
			this._conn = c;
			bool flag = this._conn == null;
			if (flag)
			{
				this._conn = os.net.CreateConnection();
			}
			this._conn.EventHandler = this;
			this._netMgr = mgr;
			this._MsgVec = new List<MsgProcesser>();
		}

		public void setSec(ICrypt enc, ICrypt dec)
		{
			this._conn.SetSec(enc, dec);
		}

		public virtual bool connect(string ip, int port, uint uid, string token, uint client)
		{
			return this._conn.Connect(ip, port, uid, token, client);
		}

		public virtual bool disconnect()
		{
			return this._conn.Disconnect();
		}

		public virtual uint sendRPC(uint cmd, Variant v)
		{
			return this._conn.PSendRPC(cmd, v);
		}

		public virtual uint sendTPKG(uint cmd, Variant v)
		{
			return this._conn.PSendTPKG(cmd, v);
		}

		public virtual void process(float tmSlice)
		{
			this._conn.onProcess();
			long tickMillisec = CCTime.getTickMillisec();
			while (this._MsgVec.Count > 0)
			{
				MsgProcesser msgProcesser = ArrayUtil.arrayPopFront<MsgProcesser>(this._MsgVec);
				msgProcesser.Process();
				msgProcesser.Dispose();
				this._netMgr.deleteMSGProcesser(msgProcesser);
				long tickMillisec2 = CCTime.getTickMillisec();
				bool flag = tickMillisec2 - tickMillisec > 10L;
				if (flag)
				{
					break;
				}
			}
		}

		public virtual void onConnect()
		{
		}

		public virtual void onConnectFailed()
		{
		}

		public virtual void onConnectionClose()
		{
		}

		public virtual void onError(string msg)
		{
		}

		public virtual void onLogin(Variant msg)
		{
		}

		public virtual void onServerVersionRecv()
		{
		}

		public virtual void onHBSend()
		{
		}

		public virtual void onHBRecv()
		{
		}

		public virtual void onRPC(uint cmdID, string cmdName, Variant par)
		{
			bool flag = cmdID != 9u;
			if (flag)
			{
				DebugTrace.add(Define.DebugTrace.DTT_DTL, string.Concat(new object[]
				{
					"cvcon_onrpc with cmd[",
					cmdID,
					"][",
					cmdName.Replace("\0", ""),
					"]"
				}));
				DebugTrace.dumpObj(par);
			}
			bool flag2 = this.type == 1;
			if (flag2)
			{
				bool flag3 = !SessionFuncMgr.instance.onRpc(cmdID, par);
				if (flag3)
				{
					this.onRPCMsgProcesser(cmdID, cmdName, par);
				}
			}
			else
			{
				this.onRPCMsgProcesser(cmdID, cmdName, par);
			}
		}

		public virtual void onRPCMsgProcesser(uint cmdID, string cmdName, Variant par)
		{
			MsgProcesser msgProcesser = this._netMgr.createRPCProcesser(cmdID, this, par);
			bool flag = msgProcesser == null;
			if (flag)
			{
				DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc msg id[" + cmdID + "] without processer");
			}
			else
			{
				bool immiProc = msgProcesser.immiProc;
				if (immiProc)
				{
					msgProcesser.Process();
					msgProcesser.Dispose();
					this._netMgr.deleteMSGProcesser(msgProcesser);
				}
				else
				{
					this._MsgVec.Add(msgProcesser);
				}
			}
		}

		public virtual void onTPKG(uint cmdID, Variant par)
		{
			DebugTrace.add(Define.DebugTrace.DTT_DTL, "OnTPKG with cmd[" + cmdID + "]");
			DebugTrace.dumpObj(par);
			bool flag = this.type == 1;
			if (flag)
			{
				bool flag2 = !SessionFuncMgr.instance.onTpkg(cmdID, par);
				if (flag2)
				{
					this.onTPKGMsgProcesser(cmdID, par);
				}
			}
			else
			{
				this.onTPKGMsgProcesser(cmdID, par);
			}
		}

		public virtual void onTPKGMsgProcesser(uint cmdID, Variant par)
		{
			MsgProcesser msgProcesser = this._netMgr.createTpkgProcesser(cmdID, this, par);
			bool flag = msgProcesser == null;
			if (flag)
			{
				DebugTrace.add(Define.DebugTrace.DTT_ERR, "tpkg msg id[" + cmdID + "] without processer");
			}
			else
			{
				bool immiProc = msgProcesser.immiProc;
				if (immiProc)
				{
					msgProcesser.Process();
					msgProcesser.Dispose();
					this._netMgr.deleteMSGProcesser(msgProcesser);
				}
				else
				{
					this._MsgVec.Add(msgProcesser);
				}
			}
		}

		public virtual void onFullTPKG(uint cmdID, Variant par)
		{
		}

		public string rpcProfileDump()
		{
			return _MsgProfilerMgr.inst.dumpProfile(2u);
		}
	}
}
