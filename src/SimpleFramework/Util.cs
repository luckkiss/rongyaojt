using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SimpleFramework
{
	public class Util
	{
		private static List<string> luaPaths = new List<string>();

		public static string DataPath
		{
			get
			{
				string text = AppConst.AppName.ToLower();
				if (Application.isMobilePlatform)
				{
					return Application.persistentDataPath + "/" + text + "/";
				}
				if (AppConst.DebugMode)
				{
					return Application.dataPath + "/StreamingAssets/";
				}
				return "c:/" + text + "/";
			}
		}

		public static bool NetAvailable
		{
			get
			{
				return Application.internetReachability != NetworkReachability.NotReachable;
			}
		}

		public static bool IsWifi
		{
			get
			{
				return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
			}
		}

		public static bool isLogin
		{
			get
			{
				return Application.loadedLevelName.CompareTo("login") == 0;
			}
		}

		public static bool isMain
		{
			get
			{
				return Application.loadedLevelName.CompareTo("main") == 0;
			}
		}

		public static bool isFight
		{
			get
			{
				return Application.loadedLevelName.CompareTo("fight") == 0;
			}
		}

		public static bool isApplePlatform
		{
			get
			{
				return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
			}
		}

		public static int Int(object o)
		{
			return Convert.ToInt32(o);
		}

		public static float Float(object o)
		{
			return (float)Math.Round((double)Convert.ToSingle(o), 2);
		}

		public static long Long(object o)
		{
			return Convert.ToInt64(o);
		}

		public static int Random(int min, int max)
		{
			return UnityEngine.Random.Range(min, max);
		}

		public static float Random(float min, float max)
		{
			return UnityEngine.Random.Range(min, max);
		}

		public static string Uid(string uid)
		{
			int num = uid.LastIndexOf('_');
			return uid.Remove(0, num + 1);
		}

		public static long GetTime()
		{
			long arg_27_0 = DateTime.UtcNow.Ticks;
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
			TimeSpan timeSpan = new TimeSpan(arg_27_0 - dateTime.Ticks);
			return (long)timeSpan.TotalMilliseconds;
		}

		public static T Get<T>(GameObject go, string subnode) where T : Component
		{
			if (go != null)
			{
				Transform transform = go.transform.FindChild(subnode);
				if (transform != null)
				{
					return transform.GetComponent<T>();
				}
			}
			return (T)((object)null);
		}

		public static T Get<T>(Transform go, string subnode) where T : Component
		{
			if (go != null)
			{
				Transform transform = go.FindChild(subnode);
				if (transform != null)
				{
					return transform.GetComponent<T>();
				}
			}
			return (T)((object)null);
		}

		public static T Get<T>(Component go, string subnode) where T : Component
		{
			return go.transform.FindChild(subnode).GetComponent<T>();
		}

		public static T Add<T>(GameObject go) where T : Component
		{
			if (go != null)
			{
				T[] components = go.GetComponents<T>();
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i] != null)
					{
						UnityEngine.Object.Destroy(components[i]);
					}
				}
				return go.gameObject.AddComponent<T>();
			}
			return (T)((object)null);
		}

		public static T Add<T>(Transform go) where T : Component
		{
			return Util.Add<T>(go.gameObject);
		}

		public static GameObject Child(GameObject go, string subnode)
		{
			return Util.Child(go.transform, subnode);
		}

		public static GameObject Child(Transform go, string subnode)
		{
			Transform transform = go.FindChild(subnode);
			if (transform == null)
			{
				return null;
			}
			return transform.gameObject;
		}

		public static GameObject Peer(GameObject go, string subnode)
		{
			return Util.Peer(go.transform, subnode);
		}

		public static GameObject Peer(Transform go, string subnode)
		{
			Transform transform = go.parent.FindChild(subnode);
			if (transform == null)
			{
				return null;
			}
			return transform.gameObject;
		}

		public static void Vibrate()
		{
		}

		public static string Encode(string message)
		{
			byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(message);
			return Convert.ToBase64String(bytes);
		}

		public static string Decode(string message)
		{
			byte[] bytes = Convert.FromBase64String(message);
			return Encoding.GetEncoding("utf-8").GetString(bytes);
		}

		public static bool IsNumeric(string str)
		{
			if (str == null || str.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < str.Length; i++)
			{
				if (!char.IsNumber(str[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static string HashToMD5Hex(string sourceStr)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(sourceStr);
			string result;
			using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
			{
				byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		public static string md5(string source)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.UTF8.GetBytes(source);
			byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes, 0, bytes.Length);
			mD5CryptoServiceProvider.Clear();
			string text = string.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				text += Convert.ToString(array[i], 16).PadLeft(2, '0');
			}
			return text.PadLeft(32, '0');
		}

		public static string md5file(string file)
		{
			string result;
			try
			{
				FileStream fileStream = new FileStream(file, FileMode.Open);
				MD5 mD = new MD5CryptoServiceProvider();
				byte[] array = mD.ComputeHash(fileStream);
				fileStream.Close();
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				result = stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception("md5file() fail, error:" + ex.Message);
			}
			return result;
		}

		public static long sizeFile(string file)
		{
			long result;
			try
			{
				FileStream fileStream = new FileStream(file, FileMode.Open);
				MD5 mD = new MD5CryptoServiceProvider();
				long length = fileStream.Length;
				fileStream.Close();
				result = length;
			}
			catch (Exception ex)
			{
				throw new Exception("sizeFile() fail, error:" + ex.Message);
			}
			return result;
		}

		public static void ClearChild(Transform go)
		{
			if (go == null)
			{
				return;
			}
			for (int i = go.childCount - 1; i >= 0; i--)
			{
				UnityEngine.Object.Destroy(go.GetChild(i).gameObject);
			}
		}

		public static string GetKey(string key)
		{
			return AppConst.AppPrefix + AppConst.UserId + "_" + key;
		}

		public static int GetInt(string key)
		{
			string key2 = Util.GetKey(key);
			return PlayerPrefs.GetInt(key2);
		}

		public static bool HasKey(string key)
		{
			string key2 = Util.GetKey(key);
			return PlayerPrefs.HasKey(key2);
		}

		public static void SetInt(string key, int value)
		{
			string key2 = Util.GetKey(key);
			PlayerPrefs.DeleteKey(key2);
			PlayerPrefs.SetInt(key2, value);
		}

		public static string GetString(string key)
		{
			string key2 = Util.GetKey(key);
			return PlayerPrefs.GetString(key2);
		}

		public static void SetString(string key, string value)
		{
			string key2 = Util.GetKey(key);
			PlayerPrefs.DeleteKey(key2);
			PlayerPrefs.SetString(key2, value);
		}

		public static void RemoveData(string key)
		{
			string key2 = Util.GetKey(key);
			PlayerPrefs.DeleteKey(key2);
		}

		public static void ClearMemory()
		{
			GC.Collect();
			Resources.UnloadUnusedAssets();
			LuaScriptMgr manager = AppFacade.Instance.GetManager<LuaScriptMgr>("LuaScriptMgr");
			if (manager != null && manager.lua != null)
			{
				manager.LuaGC(new string[0]);
			}
		}

		public static bool IsNumber(string strNumber)
		{
			Regex regex = new Regex("[^0-9]");
			return !regex.IsMatch(strNumber);
		}

		public static string GetRelativePath()
		{
			if (Application.isEditor)
			{
				return "file://" + Environment.CurrentDirectory.Replace("\\", "/") + "/Assets/StreamingAssets/";
			}
			if (Application.isMobilePlatform || Application.isConsolePlatform)
			{
				return "file:///" + Util.DataPath;
			}
			return "file://" + Application.streamingAssetsPath + "/";
		}

		public static bool isInPhone()
		{
			return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
		}

		public static string GetFileText(string path)
		{
			return File.ReadAllText(path);
		}

		public static string AppContentPath()
		{
			string result = string.Empty;
			switch (Application.platform)
			{
			case RuntimePlatform.IPhonePlayer:
				result = Application.dataPath + "/Raw/";
				return result;
			case RuntimePlatform.Android:
				result = "jar:file://" + Application.dataPath + "!/assets/";
				return result;
			}
			result = Application.dataPath + "/StreamingAssets/";
			return result;
		}

		public static string LuaPath(string name)
		{
			string dataPath = Util.DataPath;
			string text = name.ToLower();
			if (text.EndsWith(".lua"))
			{
				int length = name.LastIndexOf('.');
				name = name.Substring(0, length);
			}
			name = name.Replace('.', '/');
			if (Util.luaPaths.Count == 0)
			{
				Util.AddLuaPath(dataPath + "lua/");
			}
			return Util.SearchLuaPath(name + ".lua");
		}

		public static string SearchLuaPath(string fileName)
		{
			string text = fileName;
			for (int i = 0; i < Util.luaPaths.Count; i++)
			{
				text = Util.luaPaths[i] + fileName;
				if (File.Exists(text))
				{
					return text;
				}
			}
			return text;
		}

		public static void AddLuaPath(string path)
		{
			if (!Util.luaPaths.Contains(path))
			{
				if (!path.EndsWith("/"))
				{
					path += "/";
				}
				Util.luaPaths.Add(path);
			}
		}

		public static void RemoveLuaPath(string path)
		{
			Util.luaPaths.Remove(path);
		}

		public static void Log(string str)
		{
			Debug.Log(str);
		}

		public static void LogWarning(string str)
		{
			Debug.LogWarning(str);
		}

		public static void LogError(string str)
		{
			Debug.LogError(str);
		}

		public static int CheckRuntimeFile()
		{
			if (!Application.isEditor)
			{
				return 0;
			}
			string text = Application.dataPath + "/StreamingAssets/";
			if (!Directory.Exists(text))
			{
				return -1;
			}
			string[] files = Directory.GetFiles(text);
			if (files.Length == 0)
			{
				return -1;
			}
			if (!File.Exists(text + "files.txt"))
			{
				return -1;
			}
			string luaWrapPath = AppConst.LuaWrapPath;
			if (!Directory.Exists(luaWrapPath))
			{
				return -2;
			}
			string[] files2 = Directory.GetFiles(luaWrapPath);
			if (files2.Length == 0)
			{
				return -2;
			}
			return 0;
		}

		public static object[] CallMethod(string module, string func, params object[] args)
		{
			LuaScriptMgr manager = AppFacade.Instance.GetManager<LuaScriptMgr>("LuaScriptMgr");
			if (manager == null)
			{
				return null;
			}
			string text = module + "." + func;
			text = text.Replace("(Clone)", string.Empty);
			return manager.CallLuaFunction(text, args);
		}

		public static bool CheckEnvironment()
		{
			return true;
		}

		public static void del(string dir)
		{
			if (Directory.Exists(dir))
			{
				Util.DeleteFolder(dir);
			}
			else if (File.Exists(dir))
			{
				File.Delete(dir);
			}
		}

		public static void DeleteFolder(string dir)
		{
			if (!Directory.Exists(dir))
			{
				return;
			}
			string[] fileSystemEntries = Directory.GetFileSystemEntries(dir);
			string[] array = fileSystemEntries;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				Debug.Log(text);
				if (File.Exists(text))
				{
					FileInfo fileInfo = new FileInfo(text);
					if (fileInfo.Attributes.ToString().IndexOf("ReadOnly") != -1)
					{
						fileInfo.Attributes = FileAttributes.Normal;
					}
					File.Delete(text);
				}
				else
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(text);
					DirectoryInfo[] directories = directoryInfo.GetDirectories();
					if (directories.Length != 0)
					{
						Util.DeleteFolder(directoryInfo.FullName);
					}
					FileInfo[] files = directoryInfo.GetFiles();
					if (files.Length != 0)
					{
						Util.DeleteFolder(directoryInfo.FullName);
					}
					Directory.Delete(text);
				}
			}
		}

		public static void MoveDirectory(string sourcePath, string destinationPath)
		{
			Util.CopyDirectory(sourcePath, destinationPath, string.Empty, string.Empty);
			Util.DeleteFolder(sourcePath);
		}

		public static void CopyDirectory(string sourcePath, string destinationPath, string filter = "", string unFilter = "")
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);
			Directory.CreateDirectory(destinationPath);
			FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
			for (int i = 0; i < fileSystemInfos.Length; i++)
			{
				FileSystemInfo fileSystemInfo = fileSystemInfos[i];
				string text = Path.Combine(destinationPath, fileSystemInfo.Name);
				if (fileSystemInfo is FileInfo)
				{
					if (!(unFilter != string.Empty) || !fileSystemInfo.FullName.EndsWith(unFilter))
					{
						if (filter == string.Empty || fileSystemInfo.FullName.EndsWith(filter))
						{
							File.Copy(fileSystemInfo.FullName, text, true);
						}
					}
				}
				else
				{
					Directory.CreateDirectory(text);
					Util.CopyDirectory(fileSystemInfo.FullName, text, string.Empty, string.Empty);
				}
			}
		}
	}
}
