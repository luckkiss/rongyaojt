using LuaInterface;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class UnityEngine_Events_UnityEventBaseWrap
{
	private static Type classType = typeof(UnityEventBase);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetPersistentEventCount", new LuaCSFunction(UnityEngine_Events_UnityEventBaseWrap.GetPersistentEventCount)),
			new LuaMethod("GetPersistentTarget", new LuaCSFunction(UnityEngine_Events_UnityEventBaseWrap.GetPersistentTarget)),
			new LuaMethod("GetPersistentMethodName", new LuaCSFunction(UnityEngine_Events_UnityEventBaseWrap.GetPersistentMethodName)),
			new LuaMethod("SetPersistentListenerState", new LuaCSFunction(UnityEngine_Events_UnityEventBaseWrap.SetPersistentListenerState)),
			new LuaMethod("RemoveAllListeners", new LuaCSFunction(UnityEngine_Events_UnityEventBaseWrap.RemoveAllListeners)),
			new LuaMethod("ToString", new LuaCSFunction(UnityEngine_Events_UnityEventBaseWrap.ToString)),
			new LuaMethod("GetValidMethodInfo", new LuaCSFunction(UnityEngine_Events_UnityEventBaseWrap.GetValidMethodInfo)),
			new LuaMethod("New", new LuaCSFunction(UnityEngine_Events_UnityEventBaseWrap._CreateUnityEngine_Events_UnityEventBase)),
			new LuaMethod("GetClassType", new LuaCSFunction(UnityEngine_Events_UnityEventBaseWrap.GetClassType)),
			new LuaMethod("__tostring", new LuaCSFunction(UnityEngine_Events_UnityEventBaseWrap.Lua_ToString))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Events.UnityEventBase", typeof(UnityEventBase), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Events_UnityEventBase(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.Events.UnityEventBase class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, UnityEngine_Events_UnityEventBaseWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_ToString(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		if (luaObject != null)
		{
			LuaScriptMgr.Push(L, luaObject.ToString());
		}
		else
		{
			LuaScriptMgr.Push(L, "Table: UnityEngine.Events.UnityEventBase");
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPersistentEventCount(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEventBase unityEventBase = (UnityEventBase)LuaScriptMgr.GetNetObjectSelf(L, 1, "UnityEngine.Events.UnityEventBase");
		int persistentEventCount = unityEventBase.GetPersistentEventCount();
		LuaScriptMgr.Push(L, persistentEventCount);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPersistentTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEventBase unityEventBase = (UnityEventBase)LuaScriptMgr.GetNetObjectSelf(L, 1, "UnityEngine.Events.UnityEventBase");
		int index = (int)LuaScriptMgr.GetNumber(L, 2);
		UnityEngine.Object persistentTarget = unityEventBase.GetPersistentTarget(index);
		LuaScriptMgr.Push(L, persistentTarget);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPersistentMethodName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEventBase unityEventBase = (UnityEventBase)LuaScriptMgr.GetNetObjectSelf(L, 1, "UnityEngine.Events.UnityEventBase");
		int index = (int)LuaScriptMgr.GetNumber(L, 2);
		string persistentMethodName = unityEventBase.GetPersistentMethodName(index);
		LuaScriptMgr.Push(L, persistentMethodName);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetPersistentListenerState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UnityEventBase unityEventBase = (UnityEventBase)LuaScriptMgr.GetNetObjectSelf(L, 1, "UnityEngine.Events.UnityEventBase");
		int index = (int)LuaScriptMgr.GetNumber(L, 2);
		UnityEventCallState state = (UnityEventCallState)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(UnityEventCallState)));
		unityEventBase.SetPersistentListenerState(index, state);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveAllListeners(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEventBase unityEventBase = (UnityEventBase)LuaScriptMgr.GetNetObjectSelf(L, 1, "UnityEngine.Events.UnityEventBase");
		unityEventBase.RemoveAllListeners();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEventBase unityEventBase = (UnityEventBase)LuaScriptMgr.GetNetObjectSelf(L, 1, "UnityEngine.Events.UnityEventBase");
		string str = unityEventBase.ToString();
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetValidMethodInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		object varObject = LuaScriptMgr.GetVarObject(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		Type[] arrayObject = LuaScriptMgr.GetArrayObject<Type>(L, 3);
		MethodInfo validMethodInfo = UnityEventBase.GetValidMethodInfo(varObject, luaString, arrayObject);
		LuaScriptMgr.PushObject(L, validMethodInfo);
		return 1;
	}
}
