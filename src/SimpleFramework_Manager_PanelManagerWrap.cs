using DG.Tweening;
using GameFramework;
using LuaInterface;
using MuGame;
using SimpleFramework;
using SimpleFramework.Manager;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SimpleFramework_Manager_PanelManagerWrap
{
	private static Type classType = typeof(PanelManager);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ui_unshow", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.ui_unshow)),
			new LuaMethod("open", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.open)),
			new LuaMethod("CreateUI_Layer", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.CreateUI_Layer)),
			new LuaMethod("newGameobject", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.newGameobject)),
			new LuaMethod("onSound", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.onSound)),
			new LuaMethod("newImage", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.newImage)),
			new LuaMethod("GAME_CAMERA", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.GAME_CAMERA)),
			new LuaMethod("newScrollControler", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.newScrollControler)),
			new LuaMethod("newcellSize", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.newcellSize)),
			new LuaMethod("new_Split", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.new_Split)),
			new LuaMethod("newTabControler", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.newTabControler)),
			new LuaMethod("resLoad", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.resLoad)),
			new LuaMethod("resPicLoad", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.resPicLoad)),
			new LuaMethod("xmlMgr", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.xmlMgr)),
			new LuaMethod("getKeyWord", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.getKeyWord)),
			new LuaMethod("domoveX", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.domoveX)),
			new LuaMethod("domoveY", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.domoveY)),
			new LuaMethod("doScaleX", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.doScaleX)),
			new LuaMethod("doScaleY", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.doScaleY)),
			new LuaMethod("doScale", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.doScale)),
			new LuaMethod("doRotate", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.doRotate)),
			new LuaMethod("killTween", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.killTween)),
			new LuaMethod("getCont", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.getCont)),
			new LuaMethod("getError", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.getError)),
			new LuaMethod("openByC", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.openByC)),
			new LuaMethod("closeByC", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.closeByC)),
			new LuaMethod("changeStateByC", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.changeStateByC)),
			new LuaMethod("addBehaviour", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.addBehaviour)),
			new LuaMethod("getEventTrigger", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.getEventTrigger)),
			new LuaMethod("sliderOnValueChanged", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.sliderOnValueChanged)),
			new LuaMethod("doByC", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.doByC)),
			new LuaMethod("tween", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.tween)),
			new LuaMethod("functionOpenMgr", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.functionOpenMgr)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap._CreateSimpleFramework_Manager_PanelManager)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.Manager.PanelManager", typeof(PanelManager), regs, fields, typeof(View));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_Manager_PanelManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.Manager.PanelManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_Manager_PanelManagerWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ui_unshow(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		panelManager.ui_unshow();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int open(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		panelManager.open(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CreateUI_Layer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		int type = (int)LuaScriptMgr.GetNumber(L, 3);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 4);
		panelManager.CreateUI_Layer(luaString, type, luaFunction);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int newGameobject(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		GameObject obj = panelManager.newGameobject(luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int onSound(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		panelManager.onSound(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int newImage(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		GameObject go = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		Image obj = panelManager.newImage(go);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GAME_CAMERA(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		bool boolean = LuaScriptMgr.GetBoolean(L, 2);
		panelManager.GAME_CAMERA(boolean);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int newScrollControler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		ScrollControler o = panelManager.newScrollControler(trans);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int newcellSize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		float x = (float)LuaScriptMgr.GetNumber(L, 3);
		float y = (float)LuaScriptMgr.GetNumber(L, 4);
		panelManager.newcellSize(trans, x, y);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int new_Split(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		string[] o = panelManager.new_Split(luaString);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int newTabControler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		Transform main = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 4);
		TabControl o = panelManager.newTabControler(trans, main, luaFunction);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int resLoad(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		string luaString2 = LuaScriptMgr.GetLuaString(L, 3);
		GameObject obj = panelManager.resLoad(luaString, luaString2);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int resPicLoad(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		string luaString2 = LuaScriptMgr.GetLuaString(L, 3);
		Sprite obj = panelManager.resPicLoad(luaString, luaString2);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int xmlMgr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		XMLMgr o = panelManager.xmlMgr();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int getKeyWord(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		KeyWord keyWord = panelManager.getKeyWord();
		LuaScriptMgr.PushObject(L, keyWord);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int domoveX(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		float value = (float)LuaScriptMgr.GetNumber(L, 3);
		float duration = (float)LuaScriptMgr.GetNumber(L, 4);
		object varObject = LuaScriptMgr.GetVarObject(L, 5);
		Tween o = panelManager.domoveX(trans, value, duration, varObject);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int domoveY(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		float value = (float)LuaScriptMgr.GetNumber(L, 3);
		float duration = (float)LuaScriptMgr.GetNumber(L, 4);
		object varObject = LuaScriptMgr.GetVarObject(L, 5);
		Tween o = panelManager.domoveY(trans, value, duration, varObject);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int doScaleX(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		float value = (float)LuaScriptMgr.GetNumber(L, 3);
		float duration = (float)LuaScriptMgr.GetNumber(L, 4);
		object varObject = LuaScriptMgr.GetVarObject(L, 5);
		Tween o = panelManager.doScaleX(trans, value, duration, varObject);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int doScaleY(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		float value = (float)LuaScriptMgr.GetNumber(L, 3);
		float duration = (float)LuaScriptMgr.GetNumber(L, 4);
		object varObject = LuaScriptMgr.GetVarObject(L, 5);
		Tween o = panelManager.doScaleY(trans, value, duration, varObject);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int doScale(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		Vector3 vector = LuaScriptMgr.GetVector3(L, 3);
		float duration = (float)LuaScriptMgr.GetNumber(L, 4);
		object varObject = LuaScriptMgr.GetVarObject(L, 5);
		Tween o = panelManager.doScale(trans, vector, duration, varObject);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int doRotate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		Vector3 vector = LuaScriptMgr.GetVector3(L, 3);
		float duration = (float)LuaScriptMgr.GetNumber(L, 4);
		object varObject = LuaScriptMgr.GetVarObject(L, 5);
		Tween o = panelManager.doRotate(trans, vector, duration, varObject);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int killTween(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		panelManager.killTween(trans);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int getCont(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		LuaTable luaTable = LuaScriptMgr.GetLuaTable(L, 3);
		string cont = panelManager.getCont(luaString, luaTable);
		LuaScriptMgr.Push(L, cont);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int getError(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		string error = panelManager.getError(luaString);
		LuaScriptMgr.Push(L, error);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int openByC(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		panelManager.openByC(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int closeByC(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		panelManager.closeByC(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int changeStateByC(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		int state = (int)LuaScriptMgr.GetNumber(L, 2);
		panelManager.changeStateByC(state);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int addBehaviour(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		GameObject go = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		string luaString = LuaScriptMgr.GetLuaString(L, 3);
		GameObject obj = panelManager.addBehaviour(go, luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int getEventTrigger(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		GameObject go = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		EventTriggerListenerLua eventTrigger = panelManager.getEventTrigger(go);
		LuaScriptMgr.Push(L, eventTrigger);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int sliderOnValueChanged(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Slider go = (Slider)LuaScriptMgr.GetUnityObject(L, 2, typeof(Slider));
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 3);
		panelManager.sliderOnValueChanged(go, luaFunction);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int doByC(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		object[] paramsObject = LuaScriptMgr.GetParamsObject(L, 3, num - 2);
		panelManager.doByC(luaString, paramsObject);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int tween(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		string luaString = LuaScriptMgr.GetLuaString(L, 3);
		LuaTable luaTable = LuaScriptMgr.GetLuaTable(L, 4);
		panelManager.tween(trans, luaString, luaTable);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int functionOpenMgr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		FunctionOpenMgr o = panelManager.functionOpenMgr();
		LuaScriptMgr.PushObject(L, o);
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
