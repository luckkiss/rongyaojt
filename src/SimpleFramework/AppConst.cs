using System;
using UnityEngine;

namespace SimpleFramework
{
	public class AppConst
	{
		public const bool ExampleMode = true;

		public const bool AutoWrapMode = true;

		public const int TimerInterval = 1;

		public const int GameFrameRate = 30;

		public const bool UsePbc = true;

		public const bool UseLpeg = true;

		public const bool UsePbLua = true;

		public const bool UseCJson = true;

		public const bool UseSproto = true;

		public const bool LuaEncode = true;

		public const string ExtName = ".assetbundle";

		public const string AssetDirname = "StreamingAssets";

		public static bool DebugMode = false;

		public static bool UpdateMode = true;

		public static string AppName = "mu";

		public static string AppPrefix = AppConst.AppName + "_";

		public static string WebUrl = "http://10.1.8.60/muAsset/";

		public static string UserId = string.Empty;

		public static int SocketPort = 0;

		public static string SocketAddress = string.Empty;

		public static string LuaBasePath
		{
			get
			{
				return Application.dataPath + "/uLua/Source/";
			}
		}

		public static string LuaWrapPath
		{
			get
			{
				return AppConst.LuaBasePath + "LuaWrap/";
			}
		}
	}
}
