using System;
using UnityEngine;

namespace Cross
{
	public class LocalStorageImpl : ILocalStorage
	{
		public void writeByteArray(string key, byte[] data)
		{
			string value = Convert.ToBase64String(data, 0, data.Length);
			PlayerPrefs.SetString(key, value);
		}

		public byte[] readByteArray(string key)
		{
			string @string = PlayerPrefs.GetString(key);
			return Convert.FromBase64String(@string);
		}

		public void writeString(string key, string data)
		{
			PlayerPrefs.SetString(key, data);
		}

		public string readString(string key)
		{
			return PlayerPrefs.GetString(key);
		}

		public void writeInt(string key, int data)
		{
			PlayerPrefs.SetInt(key, data);
		}

		public int readInt(string key)
		{
			return PlayerPrefs.GetInt(key);
		}

		public void writeFloat(string key, float data)
		{
			PlayerPrefs.SetFloat(key, data);
		}

		public float readFloat(string key)
		{
			return PlayerPrefs.GetFloat(key);
		}

		public void deleteAll()
		{
			PlayerPrefs.DeleteAll();
		}
	}
}
