using LuaInterface;
using SimpleFramework.Manager;
using System;
using UnityEngine;

public class SimpleFramework_Manager_ResourceManagerWrap
{
	private static Type classType = typeof(ResourceManager);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap.Initialize)),
			new LuaMethod("LoadAsset", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap.LoadAsset)),
			new LuaMethod("LoadSpriteAsset", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap.LoadSpriteAsset)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap._CreateSimpleFramework_Manager_ResourceManager)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.Manager.ResourceManager", typeof(ResourceManager), regs, fields, typeof(View));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_Manager_ResourceManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.Manager.ResourceManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_Manager_ResourceManagerWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ResourceManager resourceManager = (ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.ResourceManager");
		resourceManager.Initialize();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAsset(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3)
		{
			ResourceManager resourceManager = (ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.ResourceManager");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			string luaString2 = LuaScriptMgr.GetLuaString(L, 3);
			GameObject obj = resourceManager.LoadAsset(luaString, luaString2);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		if (num == 4)
		{
			ResourceManager resourceManager2 = (ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.ResourceManager");
			string luaString3 = LuaScriptMgr.GetLuaString(L, 2);
			string luaString4 = LuaScriptMgr.GetLuaString(L, 3);
			LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 4);
			resourceManager2.LoadAsset(luaString3, luaString4, luaFunction);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: SimpleFramework.Manager.ResourceManager.LoadAsset");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadSpriteAsset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		ResourceManager resourceManager = (ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.ResourceManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		string luaString2 = LuaScriptMgr.GetLuaString(L, 3);
		Sprite obj = resourceManager.LoadSpriteAsset(luaString, luaString2);
		LuaScriptMgr.Push(L, obj);
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
