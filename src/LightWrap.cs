using LuaInterface;
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class LightWrap
{
	private static Type classType = typeof(Light);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("AddCommandBuffer", new LuaCSFunction(LightWrap.AddCommandBuffer)),
			new LuaMethod("RemoveCommandBuffer", new LuaCSFunction(LightWrap.RemoveCommandBuffer)),
			new LuaMethod("RemoveCommandBuffers", new LuaCSFunction(LightWrap.RemoveCommandBuffers)),
			new LuaMethod("RemoveAllCommandBuffers", new LuaCSFunction(LightWrap.RemoveAllCommandBuffers)),
			new LuaMethod("GetCommandBuffers", new LuaCSFunction(LightWrap.GetCommandBuffers)),
			new LuaMethod("GetLights", new LuaCSFunction(LightWrap.GetLights)),
			new LuaMethod("New", new LuaCSFunction(LightWrap._CreateLight)),
			new LuaMethod("GetClassType", new LuaCSFunction(LightWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(LightWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("type", new LuaCSFunction(LightWrap.get_type), new LuaCSFunction(LightWrap.set_type)),
			new LuaField("color", new LuaCSFunction(LightWrap.get_color), new LuaCSFunction(LightWrap.set_color)),
			new LuaField("intensity", new LuaCSFunction(LightWrap.get_intensity), new LuaCSFunction(LightWrap.set_intensity)),
			new LuaField("bounceIntensity", new LuaCSFunction(LightWrap.get_bounceIntensity), new LuaCSFunction(LightWrap.set_bounceIntensity)),
			new LuaField("shadows", new LuaCSFunction(LightWrap.get_shadows), new LuaCSFunction(LightWrap.set_shadows)),
			new LuaField("shadowStrength", new LuaCSFunction(LightWrap.get_shadowStrength), new LuaCSFunction(LightWrap.set_shadowStrength)),
			new LuaField("shadowResolution", new LuaCSFunction(LightWrap.get_shadowResolution), new LuaCSFunction(LightWrap.set_shadowResolution)),
			new LuaField("shadowCustomResolution", new LuaCSFunction(LightWrap.get_shadowCustomResolution), new LuaCSFunction(LightWrap.set_shadowCustomResolution)),
			new LuaField("shadowBias", new LuaCSFunction(LightWrap.get_shadowBias), new LuaCSFunction(LightWrap.set_shadowBias)),
			new LuaField("shadowNormalBias", new LuaCSFunction(LightWrap.get_shadowNormalBias), new LuaCSFunction(LightWrap.set_shadowNormalBias)),
			new LuaField("shadowNearPlane", new LuaCSFunction(LightWrap.get_shadowNearPlane), new LuaCSFunction(LightWrap.set_shadowNearPlane)),
			new LuaField("range", new LuaCSFunction(LightWrap.get_range), new LuaCSFunction(LightWrap.set_range)),
			new LuaField("spotAngle", new LuaCSFunction(LightWrap.get_spotAngle), new LuaCSFunction(LightWrap.set_spotAngle)),
			new LuaField("cookieSize", new LuaCSFunction(LightWrap.get_cookieSize), new LuaCSFunction(LightWrap.set_cookieSize)),
			new LuaField("cookie", new LuaCSFunction(LightWrap.get_cookie), new LuaCSFunction(LightWrap.set_cookie)),
			new LuaField("flare", new LuaCSFunction(LightWrap.get_flare), new LuaCSFunction(LightWrap.set_flare)),
			new LuaField("renderMode", new LuaCSFunction(LightWrap.get_renderMode), new LuaCSFunction(LightWrap.set_renderMode)),
			new LuaField("bakedIndex", new LuaCSFunction(LightWrap.get_bakedIndex), new LuaCSFunction(LightWrap.set_bakedIndex)),
			new LuaField("isBaked", new LuaCSFunction(LightWrap.get_isBaked), null),
			new LuaField("cullingMask", new LuaCSFunction(LightWrap.get_cullingMask), new LuaCSFunction(LightWrap.set_cullingMask)),
			new LuaField("commandBufferCount", new LuaCSFunction(LightWrap.get_commandBufferCount), null)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Light", typeof(Light), regs, fields, typeof(Behaviour));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateLight(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Light obj = new Light();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Light.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, LightWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_type(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.type);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_color(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name color");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index color on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.color);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_intensity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name intensity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index intensity on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.intensity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bounceIntensity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bounceIntensity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bounceIntensity on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.bounceIntensity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadows(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadows");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadows on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.shadows);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowStrength(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowStrength");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowStrength on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.shadowStrength);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowResolution(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowResolution");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowResolution on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.shadowResolution);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowCustomResolution(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowCustomResolution");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowCustomResolution on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.shadowCustomResolution);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowBias(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowBias");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowBias on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.shadowBias);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowNormalBias(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowNormalBias");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowNormalBias on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.shadowNormalBias);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowNearPlane(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowNearPlane");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowNearPlane on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.shadowNearPlane);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_range(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name range");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index range on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.range);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_spotAngle(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spotAngle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spotAngle on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.spotAngle);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cookieSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cookieSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cookieSize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.cookieSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cookie(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cookie");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cookie on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.cookie);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_flare(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name flare");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index flare on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.flare);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_renderMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderMode on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.renderMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bakedIndex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bakedIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bakedIndex on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.bakedIndex);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isBaked(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isBaked");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isBaked on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.isBaked);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cullingMask(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullingMask");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullingMask on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.cullingMask);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_commandBufferCount(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name commandBufferCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index commandBufferCount on a nil value");
			}
		}
		LuaScriptMgr.Push(L, light.commandBufferCount);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_type(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}
		light.type = (LightType)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(LightType)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_color(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name color");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index color on a nil value");
			}
		}
		light.color = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_intensity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name intensity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index intensity on a nil value");
			}
		}
		light.intensity = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bounceIntensity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bounceIntensity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bounceIntensity on a nil value");
			}
		}
		light.bounceIntensity = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadows(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadows");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadows on a nil value");
			}
		}
		light.shadows = (LightShadows)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(LightShadows)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowStrength(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowStrength");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowStrength on a nil value");
			}
		}
		light.shadowStrength = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowResolution(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowResolution");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowResolution on a nil value");
			}
		}
		light.shadowResolution = (LightShadowResolution)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(LightShadowResolution)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowCustomResolution(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowCustomResolution");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowCustomResolution on a nil value");
			}
		}
		light.shadowCustomResolution = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowBias(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowBias");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowBias on a nil value");
			}
		}
		light.shadowBias = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowNormalBias(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowNormalBias");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowNormalBias on a nil value");
			}
		}
		light.shadowNormalBias = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowNearPlane(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowNearPlane");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowNearPlane on a nil value");
			}
		}
		light.shadowNearPlane = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_range(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name range");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index range on a nil value");
			}
		}
		light.range = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_spotAngle(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spotAngle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spotAngle on a nil value");
			}
		}
		light.spotAngle = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cookieSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cookieSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cookieSize on a nil value");
			}
		}
		light.cookieSize = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cookie(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cookie");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cookie on a nil value");
			}
		}
		light.cookie = (Texture)LuaScriptMgr.GetUnityObject(L, 3, typeof(Texture));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_flare(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name flare");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index flare on a nil value");
			}
		}
		light.flare = (Flare)LuaScriptMgr.GetUnityObject(L, 3, typeof(Flare));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_renderMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderMode on a nil value");
			}
		}
		light.renderMode = (LightRenderMode)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(LightRenderMode)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bakedIndex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bakedIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bakedIndex on a nil value");
			}
		}
		light.bakedIndex = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cullingMask(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Light light = (Light)luaObject;
		if (light == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullingMask");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullingMask on a nil value");
			}
		}
		light.cullingMask = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddCommandBuffer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Light light = (Light)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Light");
		LightEvent evt = (LightEvent)((int)LuaScriptMgr.GetNetObject(L, 2, typeof(LightEvent)));
		CommandBuffer buffer = (CommandBuffer)LuaScriptMgr.GetNetObject(L, 3, typeof(CommandBuffer));
		light.AddCommandBuffer(evt, buffer);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveCommandBuffer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Light light = (Light)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Light");
		LightEvent evt = (LightEvent)((int)LuaScriptMgr.GetNetObject(L, 2, typeof(LightEvent)));
		CommandBuffer buffer = (CommandBuffer)LuaScriptMgr.GetNetObject(L, 3, typeof(CommandBuffer));
		light.RemoveCommandBuffer(evt, buffer);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveCommandBuffers(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Light light = (Light)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Light");
		LightEvent evt = (LightEvent)((int)LuaScriptMgr.GetNetObject(L, 2, typeof(LightEvent)));
		light.RemoveCommandBuffers(evt);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveAllCommandBuffers(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Light light = (Light)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Light");
		light.RemoveAllCommandBuffers();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetCommandBuffers(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Light light = (Light)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Light");
		LightEvent evt = (LightEvent)((int)LuaScriptMgr.GetNetObject(L, 2, typeof(LightEvent)));
		CommandBuffer[] commandBuffers = light.GetCommandBuffers(evt);
		LuaScriptMgr.PushArray(L, commandBuffers);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetLights(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LightType type = (LightType)((int)LuaScriptMgr.GetNetObject(L, 1, typeof(LightType)));
		int layer = (int)LuaScriptMgr.GetNumber(L, 2);
		Light[] lights = Light.GetLights(type, layer);
		LuaScriptMgr.PushArray(L, lights);
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
