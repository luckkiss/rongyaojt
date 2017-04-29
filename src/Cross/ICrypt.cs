using System;

namespace Cross
{
	public interface ICrypt
	{
		void Encrypt(uint seed, byte[] data, uint length);

		void Decrypt(uint seed, byte[] data, uint length);
	}
}
