using LuaInterface;
using System;
using UnityEngine;

public class AssetBundleWrap
{
	private static Type classType = typeof(AssetBundle);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("LoadFromFileAsync", new LuaCSFunction(AssetBundleWrap.LoadFromFileAsync)),
			new LuaMethod("LoadFromFile", new LuaCSFunction(AssetBundleWrap.LoadFromFile)),
			new LuaMethod("LoadFromMemoryAsync", new LuaCSFunction(AssetBundleWrap.LoadFromMemoryAsync)),
			new LuaMethod("LoadFromMemory", new LuaCSFunction(AssetBundleWrap.LoadFromMemory)),
			new LuaMethod("Contains", new LuaCSFunction(AssetBundleWrap.Contains)),
			new LuaMethod("LoadAsset", new LuaCSFunction(AssetBundleWrap.LoadAsset)),
			new LuaMethod("LoadAssetAsync", new LuaCSFunction(AssetBundleWrap.LoadAssetAsync)),
			new LuaMethod("LoadAssetWithSubAssets", new LuaCSFunction(AssetBundleWrap.LoadAssetWithSubAssets)),
			new LuaMethod("LoadAssetWithSubAssetsAsync", new LuaCSFunction(AssetBundleWrap.LoadAssetWithSubAssetsAsync)),
			new LuaMethod("LoadAllAssets", new LuaCSFunction(AssetBundleWrap.LoadAllAssets)),
			new LuaMethod("LoadAllAssetsAsync", new LuaCSFunction(AssetBundleWrap.LoadAllAssetsAsync)),
			new LuaMethod("Unload", new LuaCSFunction(AssetBundleWrap.Unload)),
			new LuaMethod("GetAllAssetNames", new LuaCSFunction(AssetBundleWrap.GetAllAssetNames)),
			new LuaMethod("GetAllScenePaths", new LuaCSFunction(AssetBundleWrap.GetAllScenePaths)),
			new LuaMethod("New", new LuaCSFunction(AssetBundleWrap._CreateAssetBundle)),
			new LuaMethod("GetClassType", new LuaCSFunction(AssetBundleWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(AssetBundleWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("mainAsset", new LuaCSFunction(AssetBundleWrap.get_mainAsset), null),
			new LuaField("isStreamedSceneAssetBundle", new LuaCSFunction(AssetBundleWrap.get_isStreamedSceneAssetBundle), null)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.AssetBundle", typeof(AssetBundle), regs, fields, typeof(UnityEngine.Object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateAssetBundle(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			AssetBundle obj = new AssetBundle();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, AssetBundleWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mainAsset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AssetBundle assetBundle = (AssetBundle)luaObject;
		if (assetBundle == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainAsset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainAsset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, assetBundle.mainAsset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isStreamedSceneAssetBundle(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AssetBundle assetBundle = (AssetBundle)luaObject;
		if (assetBundle == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isStreamedSceneAssetBundle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isStreamedSceneAssetBundle on a nil value");
			}
		}
		LuaScriptMgr.Push(L, assetBundle.isStreamedSceneAssetBundle);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadFromFileAsync(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			AssetBundleCreateRequest o = AssetBundle.LoadFromFileAsync(luaString);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		if (num == 2)
		{
			string luaString2 = LuaScriptMgr.GetLuaString(L, 1);
			uint crc = (uint)LuaScriptMgr.GetNumber(L, 2);
			AssetBundleCreateRequest o2 = AssetBundle.LoadFromFileAsync(luaString2, crc);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		if (num == 3)
		{
			string luaString3 = LuaScriptMgr.GetLuaString(L, 1);
			uint crc2 = (uint)LuaScriptMgr.GetNumber(L, 2);
			ulong offset = (ulong)LuaScriptMgr.GetNumber(L, 3);
			AssetBundleCreateRequest o3 = AssetBundle.LoadFromFileAsync(luaString3, crc2, offset);
			LuaScriptMgr.PushObject(L, o3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadFromFileAsync");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadFromFile(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			AssetBundle obj = AssetBundle.LoadFromFile(luaString);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		if (num == 2)
		{
			string luaString2 = LuaScriptMgr.GetLuaString(L, 1);
			uint crc = (uint)LuaScriptMgr.GetNumber(L, 2);
			AssetBundle obj2 = AssetBundle.LoadFromFile(luaString2, crc);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		if (num == 3)
		{
			string luaString3 = LuaScriptMgr.GetLuaString(L, 1);
			uint crc2 = (uint)LuaScriptMgr.GetNumber(L, 2);
			ulong offset = (ulong)LuaScriptMgr.GetNumber(L, 3);
			AssetBundle obj3 = AssetBundle.LoadFromFile(luaString3, crc2, offset);
			LuaScriptMgr.Push(L, obj3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadFromFile");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadFromMemoryAsync(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			byte[] arrayNumber = LuaScriptMgr.GetArrayNumber<byte>(L, 1);
			AssetBundleCreateRequest o = AssetBundle.LoadFromMemoryAsync(arrayNumber);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		if (num == 2)
		{
			byte[] arrayNumber2 = LuaScriptMgr.GetArrayNumber<byte>(L, 1);
			uint crc = (uint)LuaScriptMgr.GetNumber(L, 2);
			AssetBundleCreateRequest o2 = AssetBundle.LoadFromMemoryAsync(arrayNumber2, crc);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadFromMemoryAsync");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadFromMemory(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			byte[] arrayNumber = LuaScriptMgr.GetArrayNumber<byte>(L, 1);
			AssetBundle obj = AssetBundle.LoadFromMemory(arrayNumber);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		if (num == 2)
		{
			byte[] arrayNumber2 = LuaScriptMgr.GetArrayNumber<byte>(L, 1);
			uint crc = (uint)LuaScriptMgr.GetNumber(L, 2);
			AssetBundle obj2 = AssetBundle.LoadFromMemory(arrayNumber2, crc);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadFromMemory");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Contains(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		bool b = assetBundle.Contains(luaString);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAsset(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			UnityEngine.Object obj = assetBundle.LoadAsset(luaString);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		if (num == 3)
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 3);
			UnityEngine.Object obj2 = assetBundle2.LoadAsset(luaString2, typeObject);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAsset");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAssetAsync(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			AssetBundleRequest o = assetBundle.LoadAssetAsync(luaString);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		if (num == 3)
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 3);
			AssetBundleRequest o2 = assetBundle2.LoadAssetAsync(luaString2, typeObject);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAssetAsync");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAssetWithSubAssets(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			UnityEngine.Object[] o = assetBundle.LoadAssetWithSubAssets(luaString);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		if (num == 3)
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 3);
			UnityEngine.Object[] o2 = assetBundle2.LoadAssetWithSubAssets(luaString2, typeObject);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAssetWithSubAssets");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAssetWithSubAssetsAsync(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			AssetBundleRequest o = assetBundle.LoadAssetWithSubAssetsAsync(luaString);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		if (num == 3)
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 3);
			AssetBundleRequest o2 = assetBundle2.LoadAssetWithSubAssetsAsync(luaString2, typeObject);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAssetWithSubAssetsAsync");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAllAssets(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			UnityEngine.Object[] o = assetBundle.LoadAllAssets();
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		if (num == 2)
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			UnityEngine.Object[] o2 = assetBundle2.LoadAllAssets(typeObject);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAllAssets");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAllAssetsAsync(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			AssetBundleRequest o = assetBundle.LoadAllAssetsAsync();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		if (num == 2)
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			AssetBundleRequest o2 = assetBundle2.LoadAllAssetsAsync(typeObject);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAllAssetsAsync");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Unload(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
		bool boolean = LuaScriptMgr.GetBoolean(L, 2);
		assetBundle.Unload(boolean);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAllAssetNames(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
		string[] allAssetNames = assetBundle.GetAllAssetNames();
		LuaScriptMgr.PushArray(L, allAssetNames);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAllScenePaths(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
		string[] allScenePaths = assetBundle.GetAllScenePaths();
		LuaScriptMgr.PushArray(L, allScenePaths);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEngine.Object x = LuaScriptMgr.GetLuaObject(L, 1) as UnityEngine.Object;
		UnityEngine.Object y = LuaScriptMgr.GetLuaObject(L, 2) as UnityEngine.Object;
		bool b = x == y;
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
