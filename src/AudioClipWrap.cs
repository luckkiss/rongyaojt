using LuaInterface;
using System;
using UnityEngine;

public class AudioClipWrap
{
	private static Type classType = typeof(AudioClip);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("LoadAudioData", new LuaCSFunction(AudioClipWrap.LoadAudioData)),
			new LuaMethod("UnloadAudioData", new LuaCSFunction(AudioClipWrap.UnloadAudioData)),
			new LuaMethod("GetData", new LuaCSFunction(AudioClipWrap.GetData)),
			new LuaMethod("SetData", new LuaCSFunction(AudioClipWrap.SetData)),
			new LuaMethod("Create", new LuaCSFunction(AudioClipWrap.Create)),
			new LuaMethod("New", new LuaCSFunction(AudioClipWrap._CreateAudioClip)),
			new LuaMethod("GetClassType", new LuaCSFunction(AudioClipWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(AudioClipWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("length", new LuaCSFunction(AudioClipWrap.get_length), null),
			new LuaField("samples", new LuaCSFunction(AudioClipWrap.get_samples), null),
			new LuaField("channels", new LuaCSFunction(AudioClipWrap.get_channels), null),
			new LuaField("frequency", new LuaCSFunction(AudioClipWrap.get_frequency), null),
			new LuaField("loadType", new LuaCSFunction(AudioClipWrap.get_loadType), null),
			new LuaField("preloadAudioData", new LuaCSFunction(AudioClipWrap.get_preloadAudioData), null),
			new LuaField("loadState", new LuaCSFunction(AudioClipWrap.get_loadState), null),
			new LuaField("loadInBackground", new LuaCSFunction(AudioClipWrap.get_loadInBackground), null)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.AudioClip", typeof(AudioClip), regs, fields, typeof(UnityEngine.Object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateAudioClip(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			AudioClip obj = new AudioClip();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AudioClip.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, AudioClipWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_length(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name length");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index length on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.length);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_samples(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name samples");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index samples on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.samples);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_channels(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name channels");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index channels on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.channels);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_frequency(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name frequency");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index frequency on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.frequency);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadType(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loadType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loadType on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.loadType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_preloadAudioData(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preloadAudioData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preloadAudioData on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.preloadAudioData);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadState(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loadState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loadState on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.loadState);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadInBackground(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loadInBackground");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loadInBackground on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.loadInBackground);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAudioData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioClip audioClip = (AudioClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioClip");
		bool b = audioClip.LoadAudioData();
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnloadAudioData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioClip audioClip = (AudioClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioClip");
		bool b = audioClip.UnloadAudioData();
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioClip audioClip = (AudioClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioClip");
		float[] arrayNumber = LuaScriptMgr.GetArrayNumber<float>(L, 2);
		int offsetSamples = (int)LuaScriptMgr.GetNumber(L, 3);
		bool data = audioClip.GetData(arrayNumber, offsetSamples);
		LuaScriptMgr.Push(L, data);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioClip audioClip = (AudioClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioClip");
		float[] arrayNumber = LuaScriptMgr.GetArrayNumber<float>(L, 2);
		int offsetSamples = (int)LuaScriptMgr.GetNumber(L, 3);
		bool b = audioClip.SetData(arrayNumber, offsetSamples);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Create(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 5)
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			int lengthSamples = (int)LuaScriptMgr.GetNumber(L, 2);
			int channels = (int)LuaScriptMgr.GetNumber(L, 3);
			int frequency = (int)LuaScriptMgr.GetNumber(L, 4);
			bool boolean = LuaScriptMgr.GetBoolean(L, 5);
			AudioClip obj = AudioClip.Create(luaString, lengthSamples, channels, frequency, boolean);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		if (num == 6)
		{
			string luaString2 = LuaScriptMgr.GetLuaString(L, 1);
			int lengthSamples2 = (int)LuaScriptMgr.GetNumber(L, 2);
			int channels2 = (int)LuaScriptMgr.GetNumber(L, 3);
			int frequency2 = (int)LuaScriptMgr.GetNumber(L, 4);
			bool boolean2 = LuaScriptMgr.GetBoolean(L, 5);
			LuaTypes luaTypes = LuaDLL.lua_type(L, 6);
			AudioClip.PCMReaderCallback pcmreadercallback;
			if (luaTypes != LuaTypes.LUA_TFUNCTION)
			{
				pcmreadercallback = (AudioClip.PCMReaderCallback)LuaScriptMgr.GetNetObject(L, 6, typeof(AudioClip.PCMReaderCallback));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 6);
				pcmreadercallback = delegate(float[] param0)
				{
					int oldTop = func.BeginPCall();
					LuaScriptMgr.PushArray(L, param0);
					func.PCall(oldTop, 1);
					func.EndPCall(oldTop);
				};
			}
			AudioClip obj2 = AudioClip.Create(luaString2, lengthSamples2, channels2, frequency2, boolean2, pcmreadercallback);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		if (num == 7)
		{
			string luaString3 = LuaScriptMgr.GetLuaString(L, 1);
			int lengthSamples3 = (int)LuaScriptMgr.GetNumber(L, 2);
			int channels3 = (int)LuaScriptMgr.GetNumber(L, 3);
			int frequency3 = (int)LuaScriptMgr.GetNumber(L, 4);
			bool boolean3 = LuaScriptMgr.GetBoolean(L, 5);
			LuaTypes luaTypes2 = LuaDLL.lua_type(L, 6);
			AudioClip.PCMReaderCallback pcmreadercallback2;
			if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
			{
				pcmreadercallback2 = (AudioClip.PCMReaderCallback)LuaScriptMgr.GetNetObject(L, 6, typeof(AudioClip.PCMReaderCallback));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 6);
				pcmreadercallback2 = delegate(float[] param0)
				{
					int oldTop = func.BeginPCall();
					LuaScriptMgr.PushArray(L, param0);
					func.PCall(oldTop, 1);
					func.EndPCall(oldTop);
				};
			}
			LuaTypes luaTypes3 = LuaDLL.lua_type(L, 7);
			AudioClip.PCMSetPositionCallback pcmsetpositioncallback;
			if (luaTypes3 != LuaTypes.LUA_TFUNCTION)
			{
				pcmsetpositioncallback = (AudioClip.PCMSetPositionCallback)LuaScriptMgr.GetNetObject(L, 7, typeof(AudioClip.PCMSetPositionCallback));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 7);
				pcmsetpositioncallback = delegate(int param0)
				{
					int oldTop = func.BeginPCall();
					LuaScriptMgr.Push(L, param0);
					func.PCall(oldTop, 1);
					func.EndPCall(oldTop);
				};
			}
			AudioClip obj3 = AudioClip.Create(luaString3, lengthSamples3, channels3, frequency3, boolean3, pcmreadercallback2, pcmsetpositioncallback);
			LuaScriptMgr.Push(L, obj3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AudioClip.Create");
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
