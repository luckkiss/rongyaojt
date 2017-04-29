using LuaInterface;
using System;
using UnityEngine.Events;

public class UnityEngine_Events_UnityEventWrap
{
	private static Type classType = typeof(UnityEvent);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("AddListener", new LuaCSFunction(UnityEngine_Events_UnityEventWrap.AddListener)),
			new LuaMethod("RemoveListener", new LuaCSFunction(UnityEngine_Events_UnityEventWrap.RemoveListener)),
			new LuaMethod("Invoke", new LuaCSFunction(UnityEngine_Events_UnityEventWrap.Invoke)),
			new LuaMethod("New", new LuaCSFunction(UnityEngine_Events_UnityEventWrap._CreateUnityEngine_Events_UnityEvent)),
			new LuaMethod("GetClassType", new LuaCSFunction(UnityEngine_Events_UnityEventWrap.GetClassType))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Events.UnityEvent", typeof(UnityEvent), regs, fields, typeof(UnityEventBase));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Events_UnityEvent(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			UnityEvent o = new UnityEvent();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.Events.UnityEvent.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, UnityEngine_Events_UnityEventWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEvent unityEvent = (UnityEvent)LuaScriptMgr.GetNetObjectSelf(L, 1, "UnityEngine.Events.UnityEvent");
		LuaTypes luaTypes = LuaDLL.lua_type(L, 2);
		UnityAction call;
		if (luaTypes != LuaTypes.LUA_TFUNCTION)
		{
			call = (UnityAction)LuaScriptMgr.GetNetObject(L, 2, typeof(UnityAction));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			call = delegate
			{
				func.Call();
			};
		}
		unityEvent.AddListener(call);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEvent unityEvent = (UnityEvent)LuaScriptMgr.GetNetObjectSelf(L, 1, "UnityEngine.Events.UnityEvent");
		LuaTypes luaTypes = LuaDLL.lua_type(L, 2);
		UnityAction call;
		if (luaTypes != LuaTypes.LUA_TFUNCTION)
		{
			call = (UnityAction)LuaScriptMgr.GetNetObject(L, 2, typeof(UnityAction));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			call = delegate
			{
				func.Call();
			};
		}
		unityEvent.RemoveListener(call);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Invoke(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEvent unityEvent = (UnityEvent)LuaScriptMgr.GetNetObjectSelf(L, 1, "UnityEngine.Events.UnityEvent");
		unityEvent.Invoke();
		return 0;
	}
}
