using LuaInterface;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public static class DelegateFactory
{
	private delegate Delegate DelegateValue(LuaFunction func);

	private static Dictionary<Type, DelegateFactory.DelegateValue> dict = new Dictionary<Type, DelegateFactory.DelegateValue>();

	[NoToLua]
	public static void Register(IntPtr L)
	{
		DelegateFactory.dict.Add(typeof(Action<GameObject>), new DelegateFactory.DelegateValue(DelegateFactory.Action_GameObject));
		DelegateFactory.dict.Add(typeof(Action), new DelegateFactory.DelegateValue(DelegateFactory.Action));
		DelegateFactory.dict.Add(typeof(UnityAction), new DelegateFactory.DelegateValue(DelegateFactory.UnityEngine_Events_UnityAction));
		DelegateFactory.dict.Add(typeof(MemberFilter), new DelegateFactory.DelegateValue(DelegateFactory.System_Reflection_MemberFilter));
		DelegateFactory.dict.Add(typeof(TypeFilter), new DelegateFactory.DelegateValue(DelegateFactory.System_Reflection_TypeFilter));
		DelegateFactory.dict.Add(typeof(RectTransform.ReapplyDrivenProperties), new DelegateFactory.DelegateValue(DelegateFactory.RectTransform_ReapplyDrivenProperties));
		DelegateFactory.dict.Add(typeof(TestLuaDelegate.VoidDelegate), new DelegateFactory.DelegateValue(DelegateFactory.TestLuaDelegate_VoidDelegate));
		DelegateFactory.dict.Add(typeof(Camera.CameraCallback), new DelegateFactory.DelegateValue(DelegateFactory.Camera_CameraCallback));
		DelegateFactory.dict.Add(typeof(AudioClip.PCMReaderCallback), new DelegateFactory.DelegateValue(DelegateFactory.AudioClip_PCMReaderCallback));
		DelegateFactory.dict.Add(typeof(AudioClip.PCMSetPositionCallback), new DelegateFactory.DelegateValue(DelegateFactory.AudioClip_PCMSetPositionCallback));
		DelegateFactory.dict.Add(typeof(Application.LogCallback), new DelegateFactory.DelegateValue(DelegateFactory.Application_LogCallback));
		DelegateFactory.dict.Add(typeof(Application.AdvertisingIdentifierCallback), new DelegateFactory.DelegateValue(DelegateFactory.Application_AdvertisingIdentifierCallback));
	}

	[NoToLua]
	public static Delegate CreateDelegate(Type t, LuaFunction func)
	{
		DelegateFactory.DelegateValue delegateValue = null;
		if (!DelegateFactory.dict.TryGetValue(t, out delegateValue))
		{
			Debugger.LogError("Delegate {0} not register", new object[]
			{
				t.FullName
			});
			return null;
		}
		return delegateValue(func);
	}

	public static Delegate Action_GameObject(LuaFunction func)
	{
		return new Action<GameObject>(delegate(GameObject param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate Action(LuaFunction func)
	{
		return new Action(delegate
		{
			func.Call();
		});
	}

	public static Delegate UnityEngine_Events_UnityAction(LuaFunction func)
	{
		return new UnityAction(delegate
		{
			func.Call();
		});
	}

	public static Delegate System_Reflection_MemberFilter(LuaFunction func)
	{
		return new MemberFilter(delegate(MemberInfo param0, object param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.PushObject(luaState, param0);
			LuaScriptMgr.PushVarObject(luaState, param1);
			func.PCall(oldTop, 2);
			object[] array = func.PopValues(oldTop);
			func.EndPCall(oldTop);
			return (bool)array[0];
		});
	}

	public static Delegate System_Reflection_TypeFilter(LuaFunction func)
	{
		return new TypeFilter(delegate(Type param0, object param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.PushVarObject(luaState, param1);
			func.PCall(oldTop, 2);
			object[] array = func.PopValues(oldTop);
			func.EndPCall(oldTop);
			return (bool)array[0];
		});
	}

	public static Delegate RectTransform_ReapplyDrivenProperties(LuaFunction func)
	{
		return new RectTransform.ReapplyDrivenProperties(delegate(RectTransform param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate TestLuaDelegate_VoidDelegate(LuaFunction func)
	{
		return new TestLuaDelegate.VoidDelegate(delegate(GameObject param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate Camera_CameraCallback(LuaFunction func)
	{
		return new Camera.CameraCallback(delegate(Camera param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate AudioClip_PCMReaderCallback(LuaFunction func)
	{
		return new AudioClip.PCMReaderCallback(delegate(float[] param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.PushArray(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate AudioClip_PCMSetPositionCallback(LuaFunction func)
	{
		return new AudioClip.PCMSetPositionCallback(delegate(int param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate Application_LogCallback(LuaFunction func)
	{
		return new Application.LogCallback(delegate(string param0, string param1, LogType param2)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.Push(luaState, param1);
			LuaScriptMgr.Push(luaState, param2);
			func.PCall(oldTop, 3);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate Application_AdvertisingIdentifierCallback(LuaFunction func)
	{
		return new Application.AdvertisingIdentifierCallback(delegate(string param0, bool param1, string param2)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.Push(luaState, param1);
			LuaScriptMgr.Push(luaState, param2);
			func.PCall(oldTop, 3);
			func.EndPCall(oldTop);
		});
	}

	public static void Clear()
	{
		DelegateFactory.dict.Clear();
	}
}
