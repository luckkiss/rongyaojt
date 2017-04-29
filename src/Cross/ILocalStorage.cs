using System;

namespace Cross
{
	public interface ILocalStorage
	{
		void writeByteArray(string key, byte[] data);

		byte[] readByteArray(string key);

		void writeString(string key, string data);

		string readString(string key);

		void writeInt(string key, int data);

		int readInt(string key);

		void writeFloat(string key, float data);

		float readFloat(string key);

		void deleteAll();
	}
}
