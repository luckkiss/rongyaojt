using LuaInterface;
using System;

public class DelegateFactoryWrap
{
	private static Type classType = typeof(DelegateFactory);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Action_GameObject", new LuaCSFunction(DelegateFactoryWrap.Action_GameObject)),
			new LuaMethod("Action", new LuaCSFunction(DelegateFactoryWrap.Action)),
			new LuaMethod("UnityEngine_Events_UnityAction", new LuaCSFunction(DelegateFactoryWrap.UnityEngine_Events_UnityAction)),
			new LuaMethod("System_Reflection_MemberFilter", new LuaCSFunction(DelegateFactoryWrap.System_Reflection_MemberFilter)),
			new LuaMethod("System_Reflection_TypeFilter", new LuaCSFunction(DelegateFactoryWrap.System_Reflection_TypeFilter)),
			new LuaMethod("RectTransform_ReapplyDrivenProperties", new LuaCSFunction(DelegateFactoryWrap.RectTransform_ReapplyDrivenProperties)),
			new LuaMethod("TestLuaDelegate_VoidDelegate", new LuaCSFunction(DelegateFactoryWrap.TestLuaDelegate_VoidDelegate)),
			new LuaMethod("Camera_CameraCallback", new LuaCSFunction(DelegateFactoryWrap.Camera_CameraCallback)),
			new LuaMethod("AudioClip_PCMReaderCallback", new LuaCSFunction(DelegateFactoryWrap.AudioClip_PCMReaderCallback)),
			new LuaMethod("AudioClip_PCMSetPositionCallback", new LuaCSFunction(DelegateFactoryWrap.AudioClip_PCMSetPositionCallback)),
			new LuaMethod("Application_LogCallback", new LuaCSFunction(DelegateFactoryWrap.Application_LogCallback)),
			new LuaMethod("Application_AdvertisingIdentifierCallback", new LuaCSFunction(DelegateFactoryWrap.Application_AdvertisingIdentifierCallback)),
			new LuaMethod("Clear", new LuaCSFunction(DelegateFactoryWrap.Clear)),
			new LuaMethod("New", new LuaCSFunction(DelegateFactoryWrap._CreateDelegateFactory)),
			new LuaMethod("GetClassType", new LuaCSFunction(DelegateFactoryWrap.GetClassType))
		};
		LuaScriptMgr.RegisterLib(L, "DelegateFactory", regs);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateDelegateFactory(IntPtr L)
	{
		LuaDLL.luaL_error(L, "DelegateFactory class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, DelegateFactoryWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Action_GameObject(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Action_GameObject(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Action(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Action(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnityEngine_Events_UnityAction(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UnityEngine_Events_UnityAction(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int System_Reflection_MemberFilter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.System_Reflection_MemberFilter(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int System_Reflection_TypeFilter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.System_Reflection_TypeFilter(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RectTransform_ReapplyDrivenProperties(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.RectTransform_ReapplyDrivenProperties(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TestLuaDelegate_VoidDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.TestLuaDelegate_VoidDelegate(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Camera_CameraCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Camera_CameraCallback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AudioClip_PCMReaderCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.AudioClip_PCMReaderCallback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AudioClip_PCMSetPositionCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.AudioClip_PCMSetPositionCallback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Application_LogCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Application_LogCallback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Application_AdvertisingIdentifierCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Application_AdvertisingIdentifierCallback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Clear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		DelegateFactory.Clear();
		return 0;
	}
}
