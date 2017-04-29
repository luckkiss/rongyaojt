using System;

namespace Cross
{
	public interface IConnection
	{
		bool isLogin
		{
			get;
		}

		int Latency
		{
			get;
		}

		long CurServerTickTimeMS
		{
			get;
		}

		int CurServerTimeStamp
		{
			get;
		}

		long CurServerTimeStampMS
		{
			get;
		}

		uint ServerVersion
		{
			get;
		}

		uint ProtocalVersion
		{
			get;
		}

		Variant ConfigVersions
		{
			get;
		}

		uint HBInterval
		{
			get;
			set;
		}

		IConnectionEventHandler EventHandler
		{
			get;
			set;
		}

		bool isConnect
		{
			get;
		}

		void SetSec(ICrypt enc, ICrypt dec);

		void ClearSec();

		bool Connect(string addr, int port, uint uid, string token, uint client);

		bool Disconnect();

		void SetSHMSG(string msg, string addr, uint port);

		bool RequestServerVersion();

		uint PSendTPKG(uint cmdID, Variant par);

		uint PSendRPC(uint cmdID, Variant par);

		uint OSend(byte[] data, int len);

		uint OSend(byte[] data, int len, bool ignoreEnc);

		void onProcess();
	}
}
