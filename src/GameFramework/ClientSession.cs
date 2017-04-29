using Cross;
using System;

namespace GameFramework
{
	public class ClientSession : Session
	{
		private NetClient _netClient;

		public NetClient g_mgr
		{
			get
			{
				return this._netClient;
			}
		}

		public ClientSession(IConnection c, NetManager mgr) : base(c, mgr)
		{
			this.type = 1;
		}

		public void setNetClient(NetClient cl)
		{
			this._netClient = cl;
		}

		public override void onConnect()
		{
			this._netClient.onConnect();
		}

		public override void onConnectFailed()
		{
			this._netClient.onConnectFailed();
		}

		public override void onConnectionClose()
		{
			this._netClient.onConnectionClose();
		}

		public override void onError(string msg)
		{
			this._netClient.onError(msg);
		}

		public override void onLogin(Variant msg)
		{
			this._netClient.onLogin(msg);
		}

		public override void onServerVersionRecv()
		{
			this._netClient.onServerVersionRecv();
		}

		public override void onHBSend()
		{
			this._netClient.onHBSend();
		}

		public override void onHBRecv()
		{
			this._netClient.onHBRecv();
		}
	}
}
