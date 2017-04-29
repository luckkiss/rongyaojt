using LuaInterface;
using System;
using UnityEngine;

public class RectTransformWrap
{
	private static Type classType = typeof(RectTransform);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetLocalCorners", new LuaCSFunction(RectTransformWrap.GetLocalCorners)),
			new LuaMethod("GetWorldCorners", new LuaCSFunction(RectTransformWrap.GetWorldCorners)),
			new LuaMethod("SetInsetAndSizeFromParentEdge", new LuaCSFunction(RectTransformWrap.SetInsetAndSizeFromParentEdge)),
			new LuaMethod("SetSizeWithCurrentAnchors", new LuaCSFunction(RectTransformWrap.SetSizeWithCurrentAnchors)),
			new LuaMethod("New", new LuaCSFunction(RectTransformWrap._CreateRectTransform)),
			new LuaMethod("GetClassType", new LuaCSFunction(RectTransformWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(RectTransformWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("rect", new LuaCSFunction(RectTransformWrap.get_rect), null),
			new LuaField("anchorMin", new LuaCSFunction(RectTransformWrap.get_anchorMin), new LuaCSFunction(RectTransformWrap.set_anchorMin)),
			new LuaField("anchorMax", new LuaCSFunction(RectTransformWrap.get_anchorMax), new LuaCSFunction(RectTransformWrap.set_anchorMax)),
			new LuaField("anchoredPosition3D", new LuaCSFunction(RectTransformWrap.get_anchoredPosition3D), new LuaCSFunction(RectTransformWrap.set_anchoredPosition3D)),
			new LuaField("anchoredPosition", new LuaCSFunction(RectTransformWrap.get_anchoredPosition), new LuaCSFunction(RectTransformWrap.set_anchoredPosition)),
			new LuaField("sizeDelta", new LuaCSFunction(RectTransformWrap.get_sizeDelta), new LuaCSFunction(RectTransformWrap.set_sizeDelta)),
			new LuaField("pivot", new LuaCSFunction(RectTransformWrap.get_pivot), new LuaCSFunction(RectTransformWrap.set_pivot)),
			new LuaField("offsetMin", new LuaCSFunction(RectTransformWrap.get_offsetMin), new LuaCSFunction(RectTransformWrap.set_offsetMin)),
			new LuaField("offsetMax", new LuaCSFunction(RectTransformWrap.get_offsetMax), new LuaCSFunction(RectTransformWrap.set_offsetMax))
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.RectTransform", typeof(RectTransform), regs, fields, typeof(Transform));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateRectTransform(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			RectTransform obj = new RectTransform();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: RectTransform.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, RectTransformWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rect(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rect on a nil value");
			}
		}
		LuaScriptMgr.PushValue(L, rectTransform.rect);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anchorMin(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchorMin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchorMin on a nil value");
			}
		}
		LuaScriptMgr.Push(L, rectTransform.anchorMin);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anchorMax(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchorMax");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchorMax on a nil value");
			}
		}
		LuaScriptMgr.Push(L, rectTransform.anchorMax);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anchoredPosition3D(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchoredPosition3D");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchoredPosition3D on a nil value");
			}
		}
		LuaScriptMgr.Push(L, rectTransform.anchoredPosition3D);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anchoredPosition(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchoredPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchoredPosition on a nil value");
			}
		}
		LuaScriptMgr.Push(L, rectTransform.anchoredPosition);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sizeDelta(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sizeDelta");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sizeDelta on a nil value");
			}
		}
		LuaScriptMgr.Push(L, rectTransform.sizeDelta);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pivot(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pivot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pivot on a nil value");
			}
		}
		LuaScriptMgr.Push(L, rectTransform.pivot);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_offsetMin(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetMin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetMin on a nil value");
			}
		}
		LuaScriptMgr.Push(L, rectTransform.offsetMin);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_offsetMax(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetMax");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetMax on a nil value");
			}
		}
		LuaScriptMgr.Push(L, rectTransform.offsetMax);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anchorMin(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchorMin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchorMin on a nil value");
			}
		}
		rectTransform.anchorMin = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anchorMax(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchorMax");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchorMax on a nil value");
			}
		}
		rectTransform.anchorMax = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anchoredPosition3D(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchoredPosition3D");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchoredPosition3D on a nil value");
			}
		}
		rectTransform.anchoredPosition3D = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anchoredPosition(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchoredPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchoredPosition on a nil value");
			}
		}
		rectTransform.anchoredPosition = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sizeDelta(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sizeDelta");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sizeDelta on a nil value");
			}
		}
		rectTransform.sizeDelta = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_pivot(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pivot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pivot on a nil value");
			}
		}
		rectTransform.pivot = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_offsetMin(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetMin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetMin on a nil value");
			}
		}
		rectTransform.offsetMin = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_offsetMax(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RectTransform rectTransform = (RectTransform)luaObject;
		if (rectTransform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetMax");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetMax on a nil value");
			}
		}
		rectTransform.offsetMax = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetLocalCorners(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		RectTransform rectTransform = (RectTransform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RectTransform");
		Vector3[] arrayObject = LuaScriptMgr.GetArrayObject<Vector3>(L, 2);
		rectTransform.GetLocalCorners(arrayObject);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetWorldCorners(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		RectTransform rectTransform = (RectTransform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RectTransform");
		Vector3[] arrayObject = LuaScriptMgr.GetArrayObject<Vector3>(L, 2);
		rectTransform.GetWorldCorners(arrayObject);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetInsetAndSizeFromParentEdge(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		RectTransform rectTransform = (RectTransform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RectTransform");
		RectTransform.Edge edge = (RectTransform.Edge)((int)LuaScriptMgr.GetNetObject(L, 2, typeof(RectTransform.Edge)));
		float inset = (float)LuaScriptMgr.GetNumber(L, 3);
		float size = (float)LuaScriptMgr.GetNumber(L, 4);
		rectTransform.SetInsetAndSizeFromParentEdge(edge, inset, size);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetSizeWithCurrentAnchors(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		RectTransform rectTransform = (RectTransform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RectTransform");
		RectTransform.Axis axis = (RectTransform.Axis)((int)LuaScriptMgr.GetNetObject(L, 2, typeof(RectTransform.Axis)));
		float size = (float)LuaScriptMgr.GetNumber(L, 3);
		rectTransform.SetSizeWithCurrentAnchors(axis, size);
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
