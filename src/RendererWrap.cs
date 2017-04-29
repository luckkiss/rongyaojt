using LuaInterface;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RendererWrap
{
	private static Type classType = typeof(Renderer);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetPropertyBlock", new LuaCSFunction(RendererWrap.SetPropertyBlock)),
			new LuaMethod("GetPropertyBlock", new LuaCSFunction(RendererWrap.GetPropertyBlock)),
			new LuaMethod("GetClosestReflectionProbes", new LuaCSFunction(RendererWrap.GetClosestReflectionProbes)),
			new LuaMethod("New", new LuaCSFunction(RendererWrap._CreateRenderer)),
			new LuaMethod("GetClassType", new LuaCSFunction(RendererWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(RendererWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("isPartOfStaticBatch", new LuaCSFunction(RendererWrap.get_isPartOfStaticBatch), null),
			new LuaField("worldToLocalMatrix", new LuaCSFunction(RendererWrap.get_worldToLocalMatrix), null),
			new LuaField("localToWorldMatrix", new LuaCSFunction(RendererWrap.get_localToWorldMatrix), null),
			new LuaField("enabled", new LuaCSFunction(RendererWrap.get_enabled), new LuaCSFunction(RendererWrap.set_enabled)),
			new LuaField("shadowCastingMode", new LuaCSFunction(RendererWrap.get_shadowCastingMode), new LuaCSFunction(RendererWrap.set_shadowCastingMode)),
			new LuaField("receiveShadows", new LuaCSFunction(RendererWrap.get_receiveShadows), new LuaCSFunction(RendererWrap.set_receiveShadows)),
			new LuaField("material", new LuaCSFunction(RendererWrap.get_material), new LuaCSFunction(RendererWrap.set_material)),
			new LuaField("sharedMaterial", new LuaCSFunction(RendererWrap.get_sharedMaterial), new LuaCSFunction(RendererWrap.set_sharedMaterial)),
			new LuaField("materials", new LuaCSFunction(RendererWrap.get_materials), new LuaCSFunction(RendererWrap.set_materials)),
			new LuaField("sharedMaterials", new LuaCSFunction(RendererWrap.get_sharedMaterials), new LuaCSFunction(RendererWrap.set_sharedMaterials)),
			new LuaField("bounds", new LuaCSFunction(RendererWrap.get_bounds), null),
			new LuaField("lightmapIndex", new LuaCSFunction(RendererWrap.get_lightmapIndex), new LuaCSFunction(RendererWrap.set_lightmapIndex)),
			new LuaField("realtimeLightmapIndex", new LuaCSFunction(RendererWrap.get_realtimeLightmapIndex), new LuaCSFunction(RendererWrap.set_realtimeLightmapIndex)),
			new LuaField("lightmapScaleOffset", new LuaCSFunction(RendererWrap.get_lightmapScaleOffset), new LuaCSFunction(RendererWrap.set_lightmapScaleOffset)),
			new LuaField("motionVectors", new LuaCSFunction(RendererWrap.get_motionVectors), new LuaCSFunction(RendererWrap.set_motionVectors)),
			new LuaField("realtimeLightmapScaleOffset", new LuaCSFunction(RendererWrap.get_realtimeLightmapScaleOffset), new LuaCSFunction(RendererWrap.set_realtimeLightmapScaleOffset)),
			new LuaField("isVisible", new LuaCSFunction(RendererWrap.get_isVisible), null),
			new LuaField("lightProbeUsage", new LuaCSFunction(RendererWrap.get_lightProbeUsage), new LuaCSFunction(RendererWrap.set_lightProbeUsage)),
			new LuaField("lightProbeProxyVolumeOverride", new LuaCSFunction(RendererWrap.get_lightProbeProxyVolumeOverride), new LuaCSFunction(RendererWrap.set_lightProbeProxyVolumeOverride)),
			new LuaField("probeAnchor", new LuaCSFunction(RendererWrap.get_probeAnchor), new LuaCSFunction(RendererWrap.set_probeAnchor)),
			new LuaField("reflectionProbeUsage", new LuaCSFunction(RendererWrap.get_reflectionProbeUsage), new LuaCSFunction(RendererWrap.set_reflectionProbeUsage)),
			new LuaField("sortingLayerName", new LuaCSFunction(RendererWrap.get_sortingLayerName), new LuaCSFunction(RendererWrap.set_sortingLayerName)),
			new LuaField("sortingLayerID", new LuaCSFunction(RendererWrap.get_sortingLayerID), new LuaCSFunction(RendererWrap.set_sortingLayerID)),
			new LuaField("sortingOrder", new LuaCSFunction(RendererWrap.get_sortingOrder), new LuaCSFunction(RendererWrap.set_sortingOrder))
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Renderer", typeof(Renderer), regs, fields, typeof(Component));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateRenderer(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Renderer obj = new Renderer();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Renderer.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, RendererWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPartOfStaticBatch(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPartOfStaticBatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPartOfStaticBatch on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.isPartOfStaticBatch);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldToLocalMatrix(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldToLocalMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldToLocalMatrix on a nil value");
			}
		}
		LuaScriptMgr.PushValue(L, renderer.worldToLocalMatrix);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localToWorldMatrix(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localToWorldMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localToWorldMatrix on a nil value");
			}
		}
		LuaScriptMgr.PushValue(L, renderer.localToWorldMatrix);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_enabled(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enabled on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.enabled);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowCastingMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowCastingMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowCastingMode on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.shadowCastingMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_receiveShadows(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name receiveShadows");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index receiveShadows on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.receiveShadows);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_material(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name material");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index material on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.material);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sharedMaterial(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sharedMaterial");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sharedMaterial on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.sharedMaterial);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_materials(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name materials");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index materials on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, renderer.materials);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sharedMaterials(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sharedMaterials");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sharedMaterials on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, renderer.sharedMaterials);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bounds(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bounds");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bounds on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.bounds);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lightmapIndex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightmapIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightmapIndex on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.lightmapIndex);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_realtimeLightmapIndex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name realtimeLightmapIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index realtimeLightmapIndex on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.realtimeLightmapIndex);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lightmapScaleOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightmapScaleOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightmapScaleOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.lightmapScaleOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_motionVectors(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name motionVectors");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index motionVectors on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.motionVectors);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_realtimeLightmapScaleOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name realtimeLightmapScaleOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index realtimeLightmapScaleOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.realtimeLightmapScaleOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isVisible(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isVisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isVisible on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.isVisible);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lightProbeUsage(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightProbeUsage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightProbeUsage on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.lightProbeUsage);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lightProbeProxyVolumeOverride(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightProbeProxyVolumeOverride");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightProbeProxyVolumeOverride on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.lightProbeProxyVolumeOverride);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_probeAnchor(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name probeAnchor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index probeAnchor on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.probeAnchor);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_reflectionProbeUsage(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reflectionProbeUsage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reflectionProbeUsage on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.reflectionProbeUsage);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sortingLayerName(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerName on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.sortingLayerName);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sortingLayerID(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerID on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.sortingLayerID);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sortingOrder(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingOrder on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.sortingOrder);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_enabled(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enabled on a nil value");
			}
		}
		renderer.enabled = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowCastingMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowCastingMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowCastingMode on a nil value");
			}
		}
		renderer.shadowCastingMode = (ShadowCastingMode)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(ShadowCastingMode)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_receiveShadows(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name receiveShadows");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index receiveShadows on a nil value");
			}
		}
		renderer.receiveShadows = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_material(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name material");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index material on a nil value");
			}
		}
		renderer.material = (Material)LuaScriptMgr.GetUnityObject(L, 3, typeof(Material));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sharedMaterial(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sharedMaterial");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sharedMaterial on a nil value");
			}
		}
		renderer.sharedMaterial = (Material)LuaScriptMgr.GetUnityObject(L, 3, typeof(Material));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_materials(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name materials");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index materials on a nil value");
			}
		}
		renderer.materials = LuaScriptMgr.GetArrayObject<Material>(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sharedMaterials(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sharedMaterials");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sharedMaterials on a nil value");
			}
		}
		renderer.sharedMaterials = LuaScriptMgr.GetArrayObject<Material>(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lightmapIndex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightmapIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightmapIndex on a nil value");
			}
		}
		renderer.lightmapIndex = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_realtimeLightmapIndex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name realtimeLightmapIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index realtimeLightmapIndex on a nil value");
			}
		}
		renderer.realtimeLightmapIndex = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lightmapScaleOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightmapScaleOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightmapScaleOffset on a nil value");
			}
		}
		renderer.lightmapScaleOffset = LuaScriptMgr.GetVector4(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_motionVectors(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name motionVectors");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index motionVectors on a nil value");
			}
		}
		renderer.motionVectors = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_realtimeLightmapScaleOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name realtimeLightmapScaleOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index realtimeLightmapScaleOffset on a nil value");
			}
		}
		renderer.realtimeLightmapScaleOffset = LuaScriptMgr.GetVector4(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lightProbeUsage(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightProbeUsage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightProbeUsage on a nil value");
			}
		}
		renderer.lightProbeUsage = (LightProbeUsage)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(LightProbeUsage)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lightProbeProxyVolumeOverride(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightProbeProxyVolumeOverride");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightProbeProxyVolumeOverride on a nil value");
			}
		}
		renderer.lightProbeProxyVolumeOverride = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_probeAnchor(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name probeAnchor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index probeAnchor on a nil value");
			}
		}
		renderer.probeAnchor = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_reflectionProbeUsage(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reflectionProbeUsage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reflectionProbeUsage on a nil value");
			}
		}
		renderer.reflectionProbeUsage = (ReflectionProbeUsage)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(ReflectionProbeUsage)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sortingLayerName(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerName on a nil value");
			}
		}
		renderer.sortingLayerName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sortingLayerID(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerID on a nil value");
			}
		}
		renderer.sortingLayerID = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sortingOrder(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingOrder on a nil value");
			}
		}
		renderer.sortingOrder = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetPropertyBlock(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Renderer renderer = (Renderer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Renderer");
		MaterialPropertyBlock propertyBlock = (MaterialPropertyBlock)LuaScriptMgr.GetNetObject(L, 2, typeof(MaterialPropertyBlock));
		renderer.SetPropertyBlock(propertyBlock);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPropertyBlock(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Renderer renderer = (Renderer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Renderer");
		MaterialPropertyBlock dest = (MaterialPropertyBlock)LuaScriptMgr.GetNetObject(L, 2, typeof(MaterialPropertyBlock));
		renderer.GetPropertyBlock(dest);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClosestReflectionProbes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Renderer renderer = (Renderer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Renderer");
		List<ReflectionProbeBlendInfo> result = (List<ReflectionProbeBlendInfo>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<ReflectionProbeBlendInfo>));
		renderer.GetClosestReflectionProbes(result);
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
