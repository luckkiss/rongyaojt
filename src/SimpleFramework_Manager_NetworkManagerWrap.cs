using Cross;
using LuaInterface;
using SimpleFramework.Manager;
using System;
using UnityEngine;

public class SimpleFramework_Manager_NetworkManagerWrap
{
	private static Type classType = typeof(NetworkManager);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("addProxyListener", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.addProxyListener)),
			new LuaMethod("sendRPC", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.sendRPC)),
			new LuaMethod("newVariant", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.newVariant)),
			new LuaMethod("writeInt", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.writeInt)),
			new LuaMethod("getInt", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.getInt)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap._CreateSimpleFramework_Manager_NetworkManager)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("a", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.get_a), new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.set_a))
		};
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.Manager.NetworkManager", typeof(NetworkManager), regs, fields, typeof(View));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_Manager_NetworkManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.Manager.NetworkManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_Manager_NetworkManagerWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_a(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager networkManager = (NetworkManager)luaObject;
		if (networkManager == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name a");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index a on a nil value");
			}
		}
		LuaScriptMgr.PushObject(L, networkManager.a);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_a(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager networkManager = (NetworkManager)luaObject;
		if (networkManager == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name a");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index a on a nil value");
			}
		}
		networkManager.a = (Variant)LuaScriptMgr.GetNetObject(L, 3, typeof(Variant));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int addProxyListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NetworkManager networkManager = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.NetworkManager");
		uint id = (uint)LuaScriptMgr.GetNumber(L, 2);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 3);
		networkManager.addProxyListener(id, luaFunction);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int sendRPC(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NetworkManager networkManager = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.NetworkManager");
		uint cmd = (uint)LuaScriptMgr.GetNumber(L, 2);
		Variant v = (Variant)LuaScriptMgr.GetNetObject(L, 3, typeof(Variant));
		networkManager.sendRPC(cmd, v);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int newVariant(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager networkManager = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.NetworkManager");
		Variant o = networkManager.newVariant();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int writeInt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		NetworkManager networkManager = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.NetworkManager");
		Variant v = (Variant)LuaScriptMgr.GetNetObject(L, 2, typeof(Variant));
		string luaString = LuaScriptMgr.GetLuaString(L, 3);
		int num = (int)LuaScriptMgr.GetNumber(L, 4);
		Variant o = networkManager.writeInt(v, luaString, num);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int getInt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NetworkManager networkManager = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.NetworkManager");
		Variant v = (Variant)LuaScriptMgr.GetNetObject(L, 2, typeof(Variant));
		string luaString = LuaScriptMgr.GetLuaString(L, 3);
		int @int = networkManager.getInt(v, luaString);
		LuaScriptMgr.Push(L, @int);
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
