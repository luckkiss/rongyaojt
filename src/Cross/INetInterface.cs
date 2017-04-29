using System;

namespace Cross
{
	public interface INetInterface
	{
		IConnection CreateConnection();

		IPackageCoder CreatePackageCoder();

		IURLReq CreateURLReq(string url);

		ByteArray SerializeObject(Variant par);

		Variant UnserializeObject(byte[] bytes, int length);
	}
}
