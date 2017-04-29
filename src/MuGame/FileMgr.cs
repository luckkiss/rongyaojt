using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MuGame
{
	internal class FileMgr
	{
		public static string TYPE_MAIL = "mail";

		public static string TYPE_CHAT = "chat";

		public static string TYPE_NEWBIE = "newbie";

		public static string TYPE_DRESS = "dress";

		public static string TYPE_AUTO = "auto";

		public const string SUFFIX = ".txt";

		private const string PREFIX = "file://";

		private const string FORMAT = ".unity3d";

		public static string RESROOT = Application.persistentDataPath + "/";

		public static void saveString(string type, string fileName, string cacheStr)
		{
			string path = string.Concat(new object[]
			{
				FileMgr.RESROOT,
				ModelBase<PlayerModel>.getInstance().cid,
				"_",
				type,
				"_",
				fileName
			});
			FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
			StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.Flush();
			streamWriter.BaseStream.Seek(0L, SeekOrigin.Begin);
			streamWriter.Write(cacheStr);
			streamWriter.Close();
		}

		public static string loadString(string type, string fileName)
		{
			string result;
			try
			{
				string path = string.Concat(new object[]
				{
					FileMgr.RESROOT,
					ModelBase<PlayerModel>.getInstance().cid,
					"_",
					type,
					"_",
					fileName
				});
				bool flag = !File.Exists(path);
				if (flag)
				{
					result = "";
					return result;
				}
				FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, (int)fileStream.Length);
				fileStream.Close();
				List<string> list = new List<string>();
				result = Encoding.UTF8.GetString(array);
				return result;
			}
			catch (Exception ex)
			{
				Debug.LogWarning(ex.Message);
			}
			result = "";
			return result;
		}

		public static void removeFile(string type, string fileName)
		{
			try
			{
				string path = string.Concat(new object[]
				{
					FileMgr.RESROOT,
					ModelBase<PlayerModel>.getInstance().cid,
					"_",
					type,
					"_",
					fileName
				});
				bool flag = !File.Exists(path);
				if (!flag)
				{
					File.Delete(path);
				}
			}
			catch (Exception ex)
			{
				Debug.LogWarning(ex.Message);
			}
		}

		public static string GetStreamingAssetsPath(string p_filename)
		{
			string result = "";
			bool flag = Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
			if (flag)
			{
				result = string.Concat(new string[]
				{
					"file://",
					Application.streamingAssetsPath,
					"/",
					p_filename,
					".unity3d"
				});
			}
			else
			{
				bool flag2 = Application.platform == RuntimePlatform.Android;
				if (flag2)
				{
					result = Application.streamingAssetsPath + "/" + p_filename + ".unity3d";
				}
				else
				{
					bool flag3 = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.IPhonePlayer;
					if (flag3)
					{
						result = string.Concat(new string[]
						{
							"file://",
							Application.streamingAssetsPath,
							"/",
							p_filename,
							".unity3d"
						});
					}
				}
			}
			return result;
		}

		public static string GetOSDataPath(string p_filename)
		{
			string result = "";
			bool flag = Application.platform == RuntimePlatform.OSXEditor;
			if (flag)
			{
				result = Application.persistentDataPath + p_filename;
			}
			bool flag2 = Application.platform == RuntimePlatform.IPhonePlayer;
			if (flag2)
			{
				result = FileMgr.RESROOT + p_filename;
			}
			bool flag3 = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
			if (flag3)
			{
				result = Application.dataPath + "/cache/" + p_filename;
			}
			bool flag4 = Application.platform == RuntimePlatform.Android;
			if (flag4)
			{
				result = FileMgr.RESROOT + p_filename;
			}
			return result;
		}

		public static string GetURLPath(string p_filename, bool needPreFix, bool needFormat)
		{
			string text = "";
			bool flag = Application.platform == RuntimePlatform.OSXEditor;
			if (flag)
			{
				text = Application.persistentDataPath + "/" + p_filename;
			}
			bool flag2 = Application.platform == RuntimePlatform.IPhonePlayer;
			if (flag2)
			{
				text = FileMgr.RESROOT + p_filename;
			}
			bool flag3 = Application.platform == RuntimePlatform.WindowsEditor;
			if (flag3)
			{
				text = Application.dataPath + "/cache/" + p_filename;
			}
			bool flag4 = Application.platform == RuntimePlatform.WindowsPlayer;
			if (flag4)
			{
				text = Application.dataPath + "/cache/" + p_filename;
			}
			bool flag5 = Application.platform == RuntimePlatform.Android;
			if (flag5)
			{
				text = FileMgr.RESROOT + p_filename;
			}
			if (needPreFix)
			{
				text = "file://" + text;
			}
			if (needFormat)
			{
				text += ".unity3d";
			}
			return text;
		}

		public static string getFileName(string path)
		{
			string[] array = path.Split(new char[]
			{
				'/'
			});
			bool flag = array.Length != 0;
			string result;
			if (flag)
			{
				result = array[array.Length - 1];
			}
			else
			{
				result = "";
			}
			return result;
		}

		public static string getFileDir(string path)
		{
			path = path.Replace("\\", "/");
			path = path.Substring(0, path.LastIndexOf("/"));
			return path;
		}

		public static void CreateDirIfNotExists(string path)
		{
			string fileDir = FileMgr.getFileDir(path);
			bool flag = !Directory.Exists(fileDir);
			if (flag)
			{
				Directory.CreateDirectory(fileDir);
			}
		}
	}
}
