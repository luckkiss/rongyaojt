using System;

namespace Cross
{
	public class NetInterfaceImpl : INetInterface
	{
		public IConnection CreateConnection()
		{
			return new ConnectionImpl();
		}

		public IPackageCoder CreatePackageCoder()
		{
			return new PackageCoderImpl();
		}

		public IURLReq CreateURLReq(string url)
		{
			return new URLReqImpl();
		}

		public ByteArray SerializeObject(Variant par)
		{
			return PackageCoderImpl.SerializeObject(par);
		}

		public Variant UnserializeObject(byte[] bytes, int length)
		{
			return PackageCoderImpl.UnserializeObject(bytes, length);
		}
	}
}
