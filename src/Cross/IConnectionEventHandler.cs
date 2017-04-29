using System;

namespace Cross
{
	public interface IConnectionEventHandler
	{
		void onConnect();

		void onConnectFailed();

		void onConnectionClose();

		void onError(string msg);

		void onLogin(Variant msg);

		void onServerVersionRecv();

		void onHBSend();

		void onHBRecv();

		void onRPC(uint cmdID, string cmdName, Variant par);

		void onTPKG(uint cmdID, Variant par);

		void onFullTPKG(uint cmdID, Variant par);
	}
}
