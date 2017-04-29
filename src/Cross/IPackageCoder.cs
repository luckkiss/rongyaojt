using System;

namespace Cross
{
	public interface IPackageCoder
	{
		bool InitRPCPackageDescribe(Variant par);

		ByteArray PackRPCPackage(uint cmdID, Variant par);

		Variant UnpackRPCPackage(byte[] bytes, int length);

		ByteArray PackTypePackage(uint cmdID, Variant par);

		Variant UnpackTypePackage(byte[] bytes, int length);

		ByteArray PackFullTypePackage(uint cmdID, Variant par);

		Variant UnpackFullTypePackage(byte[] bytes, int length);
	}
}
