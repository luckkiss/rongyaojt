using LuaInterface;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWrap
{
	private static Type classType = typeof(Button);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnPointerClick", new LuaCSFunction(ButtonWrap.OnPointerClick)),
			new LuaMethod("OnSubmit", new LuaCSFunction(ButtonWrap.OnSubmit)),
			new LuaMethod("New", new LuaCSFunction(ButtonWrap._CreateButton)),
			new LuaMethod("GetClassType", new LuaCSFunction(ButtonWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(ButtonWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("onClick", new LuaCSFunction(ButtonWrap.get_onClick), new LuaCSFunction(ButtonWrap.set_onClick))
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.UI.Button", typeof(Button), regs, fields, typeof(Selectable));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateButton(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Button class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, ButtonWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_onClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Button button = (Button)luaObject;
		if (button == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}
		LuaScriptMgr.PushObject(L, button.onClick);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_onClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Button button = (Button)luaObject;
		if (button == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}
		button.onClick = (Button.ButtonClickedEvent)LuaScriptMgr.GetNetObject(L, 3, typeof(Button.ButtonClickedEvent));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OnPointerClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Button button = (Button)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Button");
		PointerEventData eventData = (PointerEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(PointerEventData));
		button.OnPointerClick(eventData);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OnSubmit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Button button = (Button)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Button");
		BaseEventData eventData = (BaseEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(BaseEventData));
		button.OnSubmit(eventData);
		return 0;
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
