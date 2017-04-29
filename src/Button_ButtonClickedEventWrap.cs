using LuaInterface;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class Button_ButtonClickedEventWrap
{
	private static Type classType = typeof(Button.ButtonClickedEvent);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", new LuaCSFunction(Button_ButtonClickedEventWrap._CreateButton_ButtonClickedEvent)),
			new LuaMethod("GetClassType", new LuaCSFunction(Button_ButtonClickedEventWrap.GetClassType))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "UnityEngine.UI.Button.ButtonClickedEvent", typeof(Button.ButtonClickedEvent), regs, fields, typeof(UnityEvent));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateButton_ButtonClickedEvent(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Button.ButtonClickedEvent o = new Button.ButtonClickedEvent();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Button.ButtonClickedEvent.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, Button_ButtonClickedEventWrap.classType);
		return 1;
	}
}
