using LuaInterface;
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class RenderSettingsWrap
{
	private static Type classType = typeof(RenderSettings);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", new LuaCSFunction(RenderSettingsWrap._CreateRenderSettings)),
			new LuaMethod("GetClassType", new LuaCSFunction(RenderSettingsWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(RenderSettingsWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("fog", new LuaCSFunction(RenderSettingsWrap.get_fog), new LuaCSFunction(RenderSettingsWrap.set_fog)),
			new LuaField("fogMode", new LuaCSFunction(RenderSettingsWrap.get_fogMode), new LuaCSFunction(RenderSettingsWrap.set_fogMode)),
			new LuaField("fogColor", new LuaCSFunction(RenderSettingsWrap.get_fogColor), new LuaCSFunction(RenderSettingsWrap.set_fogColor)),
			new LuaField("fogDensity", new LuaCSFunction(RenderSettingsWrap.get_fogDensity), new LuaCSFunction(RenderSettingsWrap.set_fogDensity)),
			new LuaField("fogStartDistance", new LuaCSFunction(RenderSettingsWrap.get_fogStartDistance), new LuaCSFunction(RenderSettingsWrap.set_fogStartDistance)),
			new LuaField("fogEndDistance", new LuaCSFunction(RenderSettingsWrap.get_fogEndDistance), new LuaCSFunction(RenderSettingsWrap.set_fogEndDistance)),
			new LuaField("ambientMode", new LuaCSFunction(RenderSettingsWrap.get_ambientMode), new LuaCSFunction(RenderSettingsWrap.set_ambientMode)),
			new LuaField("ambientSkyColor", new LuaCSFunction(RenderSettingsWrap.get_ambientSkyColor), new LuaCSFunction(RenderSettingsWrap.set_ambientSkyColor)),
			new LuaField("ambientEquatorColor", new LuaCSFunction(RenderSettingsWrap.get_ambientEquatorColor), new LuaCSFunction(RenderSettingsWrap.set_ambientEquatorColor)),
			new LuaField("ambientGroundColor", new LuaCSFunction(RenderSettingsWrap.get_ambientGroundColor), new LuaCSFunction(RenderSettingsWrap.set_ambientGroundColor)),
			new LuaField("ambientLight", new LuaCSFunction(RenderSettingsWrap.get_ambientLight), new LuaCSFunction(RenderSettingsWrap.set_ambientLight)),
			new LuaField("ambientIntensity", new LuaCSFunction(RenderSettingsWrap.get_ambientIntensity), new LuaCSFunction(RenderSettingsWrap.set_ambientIntensity)),
			new LuaField("ambientProbe", new LuaCSFunction(RenderSettingsWrap.get_ambientProbe), new LuaCSFunction(RenderSettingsWrap.set_ambientProbe)),
			new LuaField("reflectionIntensity", new LuaCSFunction(RenderSettingsWrap.get_reflectionIntensity), new LuaCSFunction(RenderSettingsWrap.set_reflectionIntensity)),
			new LuaField("reflectionBounces", new LuaCSFunction(RenderSettingsWrap.get_reflectionBounces), new LuaCSFunction(RenderSettingsWrap.set_reflectionBounces)),
			new LuaField("haloStrength", new LuaCSFunction(RenderSettingsWrap.get_haloStrength), new LuaCSFunction(RenderSettingsWrap.set_haloStrength)),
			new LuaField("flareStrength", new LuaCSFunction(RenderSettingsWrap.get_flareStrength), new LuaCSFunction(RenderSettingsWrap.set_flareStrength)),
			new LuaField("flareFadeSpeed", new LuaCSFunction(RenderSettingsWrap.get_flareFadeSpeed), new LuaCSFunction(RenderSettingsWrap.set_flareFadeSpeed)),
			new LuaField("skybox", new LuaCSFunction(RenderSettingsWrap.get_skybox), new LuaCSFunction(RenderSettingsWrap.set_skybox)),
			new LuaField("defaultReflectionMode", new LuaCSFunction(RenderSettingsWrap.get_defaultReflectionMode), new LuaCSFunction(RenderSettingsWrap.set_defaultReflectionMode)),
			new LuaField("defaultReflectionResolution", new LuaCSFunction(RenderSettingsWrap.get_defaultReflectionResolution), new LuaCSFunction(RenderSettingsWrap.set_defaultReflectionResolution)),
			new LuaField("customReflection", new LuaCSFunction(RenderSettingsWrap.get_customReflection), new LuaCSFunction(RenderSettingsWrap.set_customReflection))
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.RenderSettings", typeof(RenderSettings), regs, fields, typeof(UnityEngine.Object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateRenderSettings(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			RenderSettings obj = new RenderSettings();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: RenderSettings.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettingsWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fog(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.fog);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fogMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.fogMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fogColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.fogColor);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fogDensity(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.fogDensity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fogStartDistance(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.fogStartDistance);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fogEndDistance(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.fogEndDistance);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.ambientMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientSkyColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.ambientSkyColor);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientEquatorColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.ambientEquatorColor);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientGroundColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.ambientGroundColor);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientLight(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.ambientLight);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientIntensity(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.ambientIntensity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientProbe(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, RenderSettings.ambientProbe);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_reflectionIntensity(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.reflectionIntensity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_reflectionBounces(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.reflectionBounces);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_haloStrength(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.haloStrength);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_flareStrength(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.flareStrength);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_flareFadeSpeed(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.flareFadeSpeed);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_skybox(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.skybox);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_defaultReflectionMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.defaultReflectionMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_defaultReflectionResolution(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.defaultReflectionResolution);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_customReflection(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderSettings.customReflection);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fog(IntPtr L)
	{
		RenderSettings.fog = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fogMode(IntPtr L)
	{
		RenderSettings.fogMode = (FogMode)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(FogMode)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fogColor(IntPtr L)
	{
		RenderSettings.fogColor = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fogDensity(IntPtr L)
	{
		RenderSettings.fogDensity = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fogStartDistance(IntPtr L)
	{
		RenderSettings.fogStartDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fogEndDistance(IntPtr L)
	{
		RenderSettings.fogEndDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientMode(IntPtr L)
	{
		RenderSettings.ambientMode = (AmbientMode)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(AmbientMode)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientSkyColor(IntPtr L)
	{
		RenderSettings.ambientSkyColor = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientEquatorColor(IntPtr L)
	{
		RenderSettings.ambientEquatorColor = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientGroundColor(IntPtr L)
	{
		RenderSettings.ambientGroundColor = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientLight(IntPtr L)
	{
		RenderSettings.ambientLight = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientIntensity(IntPtr L)
	{
		RenderSettings.ambientIntensity = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientProbe(IntPtr L)
	{
		RenderSettings.ambientProbe = (SphericalHarmonicsL2)LuaScriptMgr.GetNetObject(L, 3, typeof(SphericalHarmonicsL2));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_reflectionIntensity(IntPtr L)
	{
		RenderSettings.reflectionIntensity = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_reflectionBounces(IntPtr L)
	{
		RenderSettings.reflectionBounces = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_haloStrength(IntPtr L)
	{
		RenderSettings.haloStrength = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_flareStrength(IntPtr L)
	{
		RenderSettings.flareStrength = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_flareFadeSpeed(IntPtr L)
	{
		RenderSettings.flareFadeSpeed = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_skybox(IntPtr L)
	{
		RenderSettings.skybox = (Material)LuaScriptMgr.GetUnityObject(L, 3, typeof(Material));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_defaultReflectionMode(IntPtr L)
	{
		RenderSettings.defaultReflectionMode = (DefaultReflectionMode)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(DefaultReflectionMode)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_defaultReflectionResolution(IntPtr L)
	{
		RenderSettings.defaultReflectionResolution = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_customReflection(IntPtr L)
	{
		RenderSettings.customReflection = (Cubemap)LuaScriptMgr.GetUnityObject(L, 3, typeof(Cubemap));
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
