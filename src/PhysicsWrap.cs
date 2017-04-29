using LuaInterface;
using System;
using UnityEngine;

public class PhysicsWrap
{
	private static Type classType = typeof(Physics);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Raycast", new LuaCSFunction(PhysicsWrap.Raycast)),
			new LuaMethod("RaycastAll", new LuaCSFunction(PhysicsWrap.RaycastAll)),
			new LuaMethod("RaycastNonAlloc", new LuaCSFunction(PhysicsWrap.RaycastNonAlloc)),
			new LuaMethod("Linecast", new LuaCSFunction(PhysicsWrap.Linecast)),
			new LuaMethod("OverlapSphere", new LuaCSFunction(PhysicsWrap.OverlapSphere)),
			new LuaMethod("OverlapSphereNonAlloc", new LuaCSFunction(PhysicsWrap.OverlapSphereNonAlloc)),
			new LuaMethod("OverlapCapsule", new LuaCSFunction(PhysicsWrap.OverlapCapsule)),
			new LuaMethod("OverlapCapsuleNonAlloc", new LuaCSFunction(PhysicsWrap.OverlapCapsuleNonAlloc)),
			new LuaMethod("CapsuleCast", new LuaCSFunction(PhysicsWrap.CapsuleCast)),
			new LuaMethod("SphereCast", new LuaCSFunction(PhysicsWrap.SphereCast)),
			new LuaMethod("CapsuleCastAll", new LuaCSFunction(PhysicsWrap.CapsuleCastAll)),
			new LuaMethod("CapsuleCastNonAlloc", new LuaCSFunction(PhysicsWrap.CapsuleCastNonAlloc)),
			new LuaMethod("SphereCastAll", new LuaCSFunction(PhysicsWrap.SphereCastAll)),
			new LuaMethod("SphereCastNonAlloc", new LuaCSFunction(PhysicsWrap.SphereCastNonAlloc)),
			new LuaMethod("CheckSphere", new LuaCSFunction(PhysicsWrap.CheckSphere)),
			new LuaMethod("CheckCapsule", new LuaCSFunction(PhysicsWrap.CheckCapsule)),
			new LuaMethod("CheckBox", new LuaCSFunction(PhysicsWrap.CheckBox)),
			new LuaMethod("OverlapBox", new LuaCSFunction(PhysicsWrap.OverlapBox)),
			new LuaMethod("OverlapBoxNonAlloc", new LuaCSFunction(PhysicsWrap.OverlapBoxNonAlloc)),
			new LuaMethod("BoxCastAll", new LuaCSFunction(PhysicsWrap.BoxCastAll)),
			new LuaMethod("BoxCastNonAlloc", new LuaCSFunction(PhysicsWrap.BoxCastNonAlloc)),
			new LuaMethod("BoxCast", new LuaCSFunction(PhysicsWrap.BoxCast)),
			new LuaMethod("IgnoreCollision", new LuaCSFunction(PhysicsWrap.IgnoreCollision)),
			new LuaMethod("IgnoreLayerCollision", new LuaCSFunction(PhysicsWrap.IgnoreLayerCollision)),
			new LuaMethod("GetIgnoreLayerCollision", new LuaCSFunction(PhysicsWrap.GetIgnoreLayerCollision)),
			new LuaMethod("New", new LuaCSFunction(PhysicsWrap._CreatePhysics)),
			new LuaMethod("GetClassType", new LuaCSFunction(PhysicsWrap.GetClassType))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("IgnoreRaycastLayer", new LuaCSFunction(PhysicsWrap.get_IgnoreRaycastLayer), null),
			new LuaField("DefaultRaycastLayers", new LuaCSFunction(PhysicsWrap.get_DefaultRaycastLayers), null),
			new LuaField("AllLayers", new LuaCSFunction(PhysicsWrap.get_AllLayers), null),
			new LuaField("gravity", new LuaCSFunction(PhysicsWrap.get_gravity), new LuaCSFunction(PhysicsWrap.set_gravity)),
			new LuaField("defaultContactOffset", new LuaCSFunction(PhysicsWrap.get_defaultContactOffset), new LuaCSFunction(PhysicsWrap.set_defaultContactOffset)),
			new LuaField("bounceThreshold", new LuaCSFunction(PhysicsWrap.get_bounceThreshold), new LuaCSFunction(PhysicsWrap.set_bounceThreshold)),
			new LuaField("defaultSolverIterations", new LuaCSFunction(PhysicsWrap.get_defaultSolverIterations), new LuaCSFunction(PhysicsWrap.set_defaultSolverIterations)),
			new LuaField("defaultSolverVelocityIterations", new LuaCSFunction(PhysicsWrap.get_defaultSolverVelocityIterations), new LuaCSFunction(PhysicsWrap.set_defaultSolverVelocityIterations)),
			new LuaField("sleepThreshold", new LuaCSFunction(PhysicsWrap.get_sleepThreshold), new LuaCSFunction(PhysicsWrap.set_sleepThreshold)),
			new LuaField("queriesHitTriggers", new LuaCSFunction(PhysicsWrap.get_queriesHitTriggers), new LuaCSFunction(PhysicsWrap.set_queriesHitTriggers))
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Physics", typeof(Physics), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreatePhysics(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Physics o = new Physics();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, PhysicsWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_IgnoreRaycastLayer(IntPtr L)
	{
		LuaScriptMgr.Push(L, 4);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_DefaultRaycastLayers(IntPtr L)
	{
		LuaScriptMgr.Push(L, -5);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_AllLayers(IntPtr L)
	{
		LuaScriptMgr.Push(L, -1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_gravity(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.gravity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_defaultContactOffset(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.defaultContactOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bounceThreshold(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.bounceThreshold);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_defaultSolverIterations(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.defaultSolverIterations);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_defaultSolverVelocityIterations(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.defaultSolverVelocityIterations);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sleepThreshold(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.sleepThreshold);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_queriesHitTriggers(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.queriesHitTriggers);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_gravity(IntPtr L)
	{
		Physics.gravity = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_defaultContactOffset(IntPtr L)
	{
		Physics.defaultContactOffset = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bounceThreshold(IntPtr L)
	{
		Physics.bounceThreshold = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_defaultSolverIterations(IntPtr L)
	{
		Physics.defaultSolverIterations = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_defaultSolverVelocityIterations(IntPtr L)
	{
		Physics.defaultSolverVelocityIterations = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sleepThreshold(IntPtr L)
	{
		Physics.sleepThreshold = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_queriesHitTriggers(IntPtr L)
	{
		Physics.queriesHitTriggers = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Raycast(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			Ray ray = LuaScriptMgr.GetRay(L, 1);
			bool b = Physics.Raycast(ray);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null))
		{
			Ray ray2 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit hit;
			bool b2 = Physics.Raycast(ray2, out hit);
			LuaScriptMgr.Push(L, b2);
			LuaScriptMgr.Push(L, hit);
			return 2;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable)))
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			bool b3 = Physics.Raycast(vector, vector2);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float)))
		{
			Ray ray3 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 2);
			bool b4 = Physics.Raycast(ray3, maxDistance);
			LuaScriptMgr.Push(L, b4);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int)))
		{
			Ray ray4 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 2);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 3);
			bool b5 = Physics.Raycast(ray4, maxDistance2, layerMask);
			LuaScriptMgr.Push(L, b5);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit hit2;
			bool b6 = Physics.Raycast(vector3, vector4, out hit2);
			LuaScriptMgr.Push(L, b6);
			LuaScriptMgr.Push(L, hit2);
			return 2;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 3);
			bool b7 = Physics.Raycast(vector5, vector6, maxDistance3);
			LuaScriptMgr.Push(L, b7);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null, typeof(float)))
		{
			Ray ray5 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 3);
			RaycastHit hit3;
			bool b8 = Physics.Raycast(ray5, out hit3, maxDistance4);
			LuaScriptMgr.Push(L, b8);
			LuaScriptMgr.Push(L, hit3);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray6 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 2);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 3);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 4));
			bool b9 = Physics.Raycast(ray6, maxDistance5, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b9);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance6 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 4);
			bool b10 = Physics.Raycast(vector7, vector8, maxDistance6, layerMask3);
			LuaScriptMgr.Push(L, b10);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null, typeof(float)))
		{
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance7 = (float)LuaDLL.lua_tonumber(L, 4);
			RaycastHit hit4;
			bool b11 = Physics.Raycast(vector9, vector10, out hit4, maxDistance7);
			LuaScriptMgr.Push(L, b11);
			LuaScriptMgr.Push(L, hit4);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Ray ray7 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance8 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask4 = (int)LuaDLL.lua_tonumber(L, 4);
			RaycastHit hit5;
			bool b12 = Physics.Raycast(ray7, out hit5, maxDistance8, layerMask4);
			LuaScriptMgr.Push(L, b12);
			LuaScriptMgr.Push(L, hit5);
			return 2;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance9 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask5 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 5));
			bool b13 = Physics.Raycast(vector11, vector12, maxDistance9, layerMask5, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, b13);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Vector3 vector13 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector14 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance10 = (float)LuaDLL.lua_tonumber(L, 4);
			int layerMask6 = (int)LuaDLL.lua_tonumber(L, 5);
			RaycastHit hit6;
			bool b14 = Physics.Raycast(vector13, vector14, out hit6, maxDistance10, layerMask6);
			LuaScriptMgr.Push(L, b14);
			LuaScriptMgr.Push(L, hit6);
			return 2;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null, typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray8 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance11 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask7 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction3 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 5));
			RaycastHit hit7;
			bool b15 = Physics.Raycast(ray8, out hit7, maxDistance11, layerMask7, queryTriggerInteraction3);
			LuaScriptMgr.Push(L, b15);
			LuaScriptMgr.Push(L, hit7);
			return 2;
		}
		if (num == 6)
		{
			Vector3 vector15 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector16 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance12 = (float)LuaScriptMgr.GetNumber(L, 4);
			int layerMask8 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction queryTriggerInteraction4 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction)));
			RaycastHit hit8;
			bool b16 = Physics.Raycast(vector15, vector16, out hit8, maxDistance12, layerMask8, queryTriggerInteraction4);
			LuaScriptMgr.Push(L, b16);
			LuaScriptMgr.Push(L, hit8);
			return 2;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.Raycast");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RaycastAll(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			Ray ray = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] o = Physics.RaycastAll(ray);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable)))
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit[] o2 = Physics.RaycastAll(vector, vector2);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float)))
		{
			Ray ray2 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit[] o3 = Physics.RaycastAll(ray2, maxDistance);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float)))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 3);
			RaycastHit[] o4 = Physics.RaycastAll(vector3, vector4, maxDistance2);
			LuaScriptMgr.PushArray(L, o4);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int)))
		{
			Ray ray3 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 2);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 3);
			RaycastHit[] o5 = Physics.RaycastAll(ray3, maxDistance3, layerMask);
			LuaScriptMgr.PushArray(L, o5);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 3);
			int layermask = (int)LuaDLL.lua_tonumber(L, 4);
			RaycastHit[] o6 = Physics.RaycastAll(vector5, vector6, maxDistance4, layermask);
			LuaScriptMgr.PushArray(L, o6);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray4 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 2);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 3);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 4));
			RaycastHit[] o7 = Physics.RaycastAll(ray4, maxDistance5, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o7);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance6 = (float)LuaScriptMgr.GetNumber(L, 3);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction)));
			RaycastHit[] o8 = Physics.RaycastAll(vector7, vector8, maxDistance6, layermask2, queryTriggerInteraction2);
			LuaScriptMgr.PushArray(L, o8);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.RaycastAll");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RaycastNonAlloc(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			Ray ray = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] arrayObject = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 2);
			int d = Physics.RaycastNonAlloc(ray, arrayObject);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(RaycastHit[])))
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit[] arrayObject2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			int d2 = Physics.RaycastNonAlloc(vector, vector2, arrayObject2);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(RaycastHit[]), typeof(float)))
		{
			Ray ray2 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] arrayObject3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 2);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 3);
			int d3 = Physics.RaycastNonAlloc(ray2, arrayObject3, maxDistance);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(RaycastHit[]), typeof(float)))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit[] arrayObject4 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 4);
			int d4 = Physics.RaycastNonAlloc(vector3, vector4, arrayObject4, maxDistance2);
			LuaScriptMgr.Push(L, d4);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(RaycastHit[]), typeof(float), typeof(int)))
		{
			Ray ray3 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] arrayObject5 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 2);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 4);
			int d5 = Physics.RaycastNonAlloc(ray3, arrayObject5, maxDistance3, layerMask);
			LuaScriptMgr.Push(L, d5);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(RaycastHit[]), typeof(float), typeof(int)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit[] arrayObject6 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 4);
			int layermask = (int)LuaDLL.lua_tonumber(L, 5);
			int d6 = Physics.RaycastNonAlloc(vector5, vector6, arrayObject6, maxDistance4, layermask);
			LuaScriptMgr.Push(L, d6);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(RaycastHit[]), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray4 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] arrayObject7 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 2);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 5));
			int d7 = Physics.RaycastNonAlloc(ray4, arrayObject7, maxDistance5, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, d7);
			return 1;
		}
		if (num == 6)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit[] arrayObject8 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float maxDistance6 = (float)LuaScriptMgr.GetNumber(L, 4);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction)));
			int d8 = Physics.RaycastNonAlloc(vector7, vector8, arrayObject8, maxDistance6, layermask2, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, d8);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.RaycastNonAlloc");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Linecast(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			bool b = Physics.Linecast(vector, vector2);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit hit;
			bool b2 = Physics.Linecast(vector3, vector4, out hit);
			LuaScriptMgr.Push(L, b2);
			LuaScriptMgr.Push(L, hit);
			return 2;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(int)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 3);
			bool b3 = Physics.Linecast(vector5, vector6, layerMask);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null, typeof(int)))
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 4);
			RaycastHit hit2;
			bool b4 = Physics.Linecast(vector7, vector8, out hit2, layerMask2);
			LuaScriptMgr.Push(L, b4);
			LuaScriptMgr.Push(L, hit2);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 2);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 3);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 4));
			bool b5 = Physics.Linecast(vector9, vector10, layerMask3, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b5);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 2);
			int layerMask4 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction)));
			RaycastHit hit3;
			bool b6 = Physics.Linecast(vector11, vector12, out hit3, layerMask4, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, b6);
			LuaScriptMgr.Push(L, hit3);
			return 2;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.Linecast");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapSphere(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			float radius = (float)LuaScriptMgr.GetNumber(L, 2);
			Collider[] o = Physics.OverlapSphere(vector, radius);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		if (num == 3)
		{
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 1);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 2);
			int layerMask = (int)LuaScriptMgr.GetNumber(L, 3);
			Collider[] o2 = Physics.OverlapSphere(vector2, radius2, layerMask);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		if (num == 4)
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 2);
			int layerMask2 = (int)LuaScriptMgr.GetNumber(L, 3);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 4, typeof(QueryTriggerInteraction)));
			Collider[] o3 = Physics.OverlapSphere(vector3, radius3, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapSphere");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapSphereNonAlloc(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			float radius = (float)LuaScriptMgr.GetNumber(L, 2);
			Collider[] arrayObject = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			int d = Physics.OverlapSphereNonAlloc(vector, radius, arrayObject);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 4)
		{
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 1);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 2);
			Collider[] arrayObject2 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			int layerMask = (int)LuaScriptMgr.GetNumber(L, 4);
			int d2 = Physics.OverlapSphereNonAlloc(vector2, radius2, arrayObject2, layerMask);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 2);
			Collider[] arrayObject3 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			int layerMask2 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction)));
			int d3 = Physics.OverlapSphereNonAlloc(vector3, radius3, arrayObject3, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapSphereNonAlloc");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapCapsule(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			float radius = (float)LuaScriptMgr.GetNumber(L, 3);
			Collider[] o = Physics.OverlapCapsule(vector, vector2, radius);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		if (num == 4)
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 3);
			int layerMask = (int)LuaScriptMgr.GetNumber(L, 4);
			Collider[] o2 = Physics.OverlapCapsule(vector3, vector4, radius2, layerMask);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 3);
			int layerMask2 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction)));
			Collider[] o3 = Physics.OverlapCapsule(vector5, vector6, radius3, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapCapsule");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapCapsuleNonAlloc(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 4)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			float radius = (float)LuaScriptMgr.GetNumber(L, 3);
			Collider[] arrayObject = LuaScriptMgr.GetArrayObject<Collider>(L, 4);
			int d = Physics.OverlapCapsuleNonAlloc(vector, vector2, radius, arrayObject);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Collider[] arrayObject2 = LuaScriptMgr.GetArrayObject<Collider>(L, 4);
			int layerMask = (int)LuaScriptMgr.GetNumber(L, 5);
			int d2 = Physics.OverlapCapsuleNonAlloc(vector3, vector4, radius2, arrayObject2, layerMask);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		if (num == 6)
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 3);
			Collider[] arrayObject3 = LuaScriptMgr.GetArrayObject<Collider>(L, 4);
			int layerMask2 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction)));
			int d3 = Physics.OverlapCapsuleNonAlloc(vector5, vector6, radius3, arrayObject3, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapCapsuleNonAlloc");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CapsuleCast(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 4)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			float radius = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 4);
			bool b = Physics.CapsuleCast(vector, vector2, radius, vector3);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), null))
		{
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 2);
			float radius2 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit hit;
			bool b2 = Physics.CapsuleCast(vector4, vector5, radius2, vector6, out hit);
			LuaScriptMgr.Push(L, b2);
			LuaScriptMgr.Push(L, hit);
			return 2;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float)))
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			float radius3 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 5);
			bool b3 = Physics.CapsuleCast(vector7, vector8, radius3, vector9, maxDistance);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float)))
		{
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 2);
			float radius4 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 6);
			RaycastHit hit2;
			bool b4 = Physics.CapsuleCast(vector10, vector11, radius4, vector12, out hit2, maxDistance2);
			LuaScriptMgr.Push(L, b4);
			LuaScriptMgr.Push(L, hit2);
			return 2;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 vector13 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector14 = LuaScriptMgr.GetVector3(L, 2);
			float radius5 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector15 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 5);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 6);
			bool b5 = Physics.CapsuleCast(vector13, vector14, radius5, vector15, maxDistance3, layerMask);
			LuaScriptMgr.Push(L, b5);
			return 1;
		}
		if (num == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 vector16 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector17 = LuaScriptMgr.GetVector3(L, 2);
			float radius6 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector18 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 5);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 6);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 7));
			bool b6 = Physics.CapsuleCast(vector16, vector17, radius6, vector18, maxDistance4, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b6);
			return 1;
		}
		if (num == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Vector3 vector19 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector20 = LuaScriptMgr.GetVector3(L, 2);
			float radius7 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector21 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 6);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 7);
			RaycastHit hit3;
			bool b7 = Physics.CapsuleCast(vector19, vector20, radius7, vector21, out hit3, maxDistance5, layerMask3);
			LuaScriptMgr.Push(L, b7);
			LuaScriptMgr.Push(L, hit3);
			return 2;
		}
		if (num == 8)
		{
			Vector3 vector22 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector23 = LuaScriptMgr.GetVector3(L, 2);
			float radius8 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector24 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance6 = (float)LuaScriptMgr.GetNumber(L, 6);
			int layerMask4 = (int)LuaScriptMgr.GetNumber(L, 7);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 8, typeof(QueryTriggerInteraction)));
			RaycastHit hit4;
			bool b8 = Physics.CapsuleCast(vector22, vector23, radius8, vector24, out hit4, maxDistance6, layerMask4, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, b8);
			LuaScriptMgr.Push(L, hit4);
			return 2;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CapsuleCast");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SphereCast(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			Ray ray = LuaScriptMgr.GetRay(L, 1);
			float radius = (float)LuaScriptMgr.GetNumber(L, 2);
			bool b = Physics.SphereCast(ray, radius);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float)))
		{
			Ray ray2 = LuaScriptMgr.GetRay(L, 1);
			float radius2 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 3);
			bool b2 = Physics.SphereCast(ray2, radius2, maxDistance);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null))
		{
			Ray ray3 = LuaScriptMgr.GetRay(L, 1);
			float radius3 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit hit;
			bool b3 = Physics.SphereCast(ray3, radius3, out hit);
			LuaScriptMgr.Push(L, b3);
			LuaScriptMgr.Push(L, hit);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), null))
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			float radius4 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit hit2;
			bool b4 = Physics.SphereCast(vector, radius4, vector2, out hit2);
			LuaScriptMgr.Push(L, b4);
			LuaScriptMgr.Push(L, hit2);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int)))
		{
			Ray ray4 = LuaScriptMgr.GetRay(L, 1);
			float radius5 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 4);
			bool b5 = Physics.SphereCast(ray4, radius5, maxDistance2, layerMask);
			LuaScriptMgr.Push(L, b5);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null, typeof(float)))
		{
			Ray ray5 = LuaScriptMgr.GetRay(L, 1);
			float radius6 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 4);
			RaycastHit hit3;
			bool b6 = Physics.SphereCast(ray5, radius6, out hit3, maxDistance3);
			LuaScriptMgr.Push(L, b6);
			LuaScriptMgr.Push(L, hit3);
			return 2;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null, typeof(float), typeof(int)))
		{
			Ray ray6 = LuaScriptMgr.GetRay(L, 1);
			float radius7 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 4);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 5);
			RaycastHit hit4;
			bool b7 = Physics.SphereCast(ray6, radius7, out hit4, maxDistance4, layerMask2);
			LuaScriptMgr.Push(L, b7);
			LuaScriptMgr.Push(L, hit4);
			return 2;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray7 = LuaScriptMgr.GetRay(L, 1);
			float radius8 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 5));
			bool b8 = Physics.SphereCast(ray7, radius8, maxDistance5, layerMask3, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b8);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float)))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			float radius9 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance6 = (float)LuaDLL.lua_tonumber(L, 5);
			RaycastHit hit5;
			bool b9 = Physics.SphereCast(vector3, radius9, vector4, out hit5, maxDistance6);
			LuaScriptMgr.Push(L, b9);
			LuaScriptMgr.Push(L, hit5);
			return 2;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			float radius10 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance7 = (float)LuaDLL.lua_tonumber(L, 5);
			int layerMask4 = (int)LuaDLL.lua_tonumber(L, 6);
			RaycastHit hit6;
			bool b10 = Physics.SphereCast(vector5, radius10, vector6, out hit6, maxDistance7, layerMask4);
			LuaScriptMgr.Push(L, b10);
			LuaScriptMgr.Push(L, hit6);
			return 2;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null, typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray8 = LuaScriptMgr.GetRay(L, 1);
			float radius11 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance8 = (float)LuaDLL.lua_tonumber(L, 4);
			int layerMask5 = (int)LuaDLL.lua_tonumber(L, 5);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 6));
			RaycastHit hit7;
			bool b11 = Physics.SphereCast(ray8, radius11, out hit7, maxDistance8, layerMask5, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, b11);
			LuaScriptMgr.Push(L, hit7);
			return 2;
		}
		if (num == 7)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			float radius12 = (float)LuaScriptMgr.GetNumber(L, 2);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance9 = (float)LuaScriptMgr.GetNumber(L, 5);
			int layerMask6 = (int)LuaScriptMgr.GetNumber(L, 6);
			QueryTriggerInteraction queryTriggerInteraction3 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 7, typeof(QueryTriggerInteraction)));
			RaycastHit hit8;
			bool b12 = Physics.SphereCast(vector7, radius12, vector8, out hit8, maxDistance9, layerMask6, queryTriggerInteraction3);
			LuaScriptMgr.Push(L, b12);
			LuaScriptMgr.Push(L, hit8);
			return 2;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.SphereCast");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CapsuleCastAll(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 4)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			float radius = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] o = Physics.CapsuleCastAll(vector, vector2, radius, vector3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 2);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance = (float)LuaScriptMgr.GetNumber(L, 5);
			RaycastHit[] o2 = Physics.CapsuleCastAll(vector4, vector5, radius2, vector6, maxDistance);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		if (num == 6)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance2 = (float)LuaScriptMgr.GetNumber(L, 5);
			int layermask = (int)LuaScriptMgr.GetNumber(L, 6);
			RaycastHit[] o3 = Physics.CapsuleCastAll(vector7, vector8, radius3, vector9, maxDistance2, layermask);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		if (num == 7)
		{
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 2);
			float radius4 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance3 = (float)LuaScriptMgr.GetNumber(L, 5);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 6);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 7, typeof(QueryTriggerInteraction)));
			RaycastHit[] o4 = Physics.CapsuleCastAll(vector10, vector11, radius4, vector12, maxDistance3, layermask2, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o4);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CapsuleCastAll");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CapsuleCastNonAlloc(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 5)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			float radius = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] arrayObject = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 5);
			int d = Physics.CapsuleCastNonAlloc(vector, vector2, radius, vector3, arrayObject);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 6)
		{
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 2);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] arrayObject2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 5);
			float maxDistance = (float)LuaScriptMgr.GetNumber(L, 6);
			int d2 = Physics.CapsuleCastNonAlloc(vector4, vector5, radius2, vector6, arrayObject2, maxDistance);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		if (num == 7)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] arrayObject3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 5);
			float maxDistance2 = (float)LuaScriptMgr.GetNumber(L, 6);
			int layermask = (int)LuaScriptMgr.GetNumber(L, 7);
			int d3 = Physics.CapsuleCastNonAlloc(vector7, vector8, radius3, vector9, arrayObject3, maxDistance2, layermask);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		if (num == 8)
		{
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 2);
			float radius4 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] arrayObject4 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 5);
			float maxDistance3 = (float)LuaScriptMgr.GetNumber(L, 6);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 7);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 8, typeof(QueryTriggerInteraction)));
			int d4 = Physics.CapsuleCastNonAlloc(vector10, vector11, radius4, vector12, arrayObject4, maxDistance3, layermask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, d4);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CapsuleCastNonAlloc");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SphereCastAll(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			Ray ray = LuaScriptMgr.GetRay(L, 1);
			float radius = (float)LuaScriptMgr.GetNumber(L, 2);
			RaycastHit[] o = Physics.SphereCastAll(ray, radius);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float)))
		{
			Ray ray2 = LuaScriptMgr.GetRay(L, 1);
			float radius2 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 3);
			RaycastHit[] o2 = Physics.SphereCastAll(ray2, radius2, maxDistance);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable)))
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			float radius3 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] o3 = Physics.SphereCastAll(vector, radius3, vector2);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int)))
		{
			Ray ray3 = LuaScriptMgr.GetRay(L, 1);
			float radius4 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 4);
			RaycastHit[] o4 = Physics.SphereCastAll(ray3, radius4, maxDistance2, layerMask);
			LuaScriptMgr.PushArray(L, o4);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float)))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			float radius5 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 4);
			RaycastHit[] o5 = Physics.SphereCastAll(vector3, radius5, vector4, maxDistance3);
			LuaScriptMgr.PushArray(L, o5);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			float radius6 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 4);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 5);
			RaycastHit[] o6 = Physics.SphereCastAll(vector5, radius6, vector6, maxDistance4, layerMask2);
			LuaScriptMgr.PushArray(L, o6);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray4 = LuaScriptMgr.GetRay(L, 1);
			float radius7 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 5));
			RaycastHit[] o7 = Physics.SphereCastAll(ray4, radius7, maxDistance5, layerMask3, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o7);
			return 1;
		}
		if (num == 6)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			float radius8 = (float)LuaScriptMgr.GetNumber(L, 2);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance6 = (float)LuaScriptMgr.GetNumber(L, 4);
			int layerMask4 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction)));
			RaycastHit[] o8 = Physics.SphereCastAll(vector7, radius8, vector8, maxDistance6, layerMask4, queryTriggerInteraction2);
			LuaScriptMgr.PushArray(L, o8);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.SphereCastAll");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SphereCastNonAlloc(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3)
		{
			Ray ray = LuaScriptMgr.GetRay(L, 1);
			float radius = (float)LuaScriptMgr.GetNumber(L, 2);
			RaycastHit[] arrayObject = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			int d = Physics.SphereCastNonAlloc(ray, radius, arrayObject);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(RaycastHit[]), typeof(float)))
		{
			Ray ray2 = LuaScriptMgr.GetRay(L, 1);
			float radius2 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit[] arrayObject2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 4);
			int d2 = Physics.SphereCastNonAlloc(ray2, radius2, arrayObject2, maxDistance);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(RaycastHit[])))
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			float radius3 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] arrayObject3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			int d3 = Physics.SphereCastNonAlloc(vector, radius3, vector2, arrayObject3);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(RaycastHit[]), typeof(float), typeof(int)))
		{
			Ray ray3 = LuaScriptMgr.GetRay(L, 1);
			float radius4 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit[] arrayObject4 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 4);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 5);
			int d4 = Physics.SphereCastNonAlloc(ray3, radius4, arrayObject4, maxDistance2, layerMask);
			LuaScriptMgr.Push(L, d4);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(RaycastHit[]), typeof(float)))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			float radius5 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] arrayObject5 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 5);
			int d5 = Physics.SphereCastNonAlloc(vector3, radius5, vector4, arrayObject5, maxDistance3);
			LuaScriptMgr.Push(L, d5);
			return 1;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(RaycastHit[]), typeof(float), typeof(int)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			float radius6 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] arrayObject6 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 5);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 6);
			int d6 = Physics.SphereCastNonAlloc(vector5, radius6, vector6, arrayObject6, maxDistance4, layerMask2);
			LuaScriptMgr.Push(L, d6);
			return 1;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(RaycastHit[]), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray4 = LuaScriptMgr.GetRay(L, 1);
			float radius7 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit[] arrayObject7 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 4);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 5);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 6));
			int d7 = Physics.SphereCastNonAlloc(ray4, radius7, arrayObject7, maxDistance5, layerMask3, queryTriggerInteraction);
			LuaScriptMgr.Push(L, d7);
			return 1;
		}
		if (num == 7)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			float radius8 = (float)LuaScriptMgr.GetNumber(L, 2);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] arrayObject8 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			float maxDistance6 = (float)LuaScriptMgr.GetNumber(L, 5);
			int layerMask4 = (int)LuaScriptMgr.GetNumber(L, 6);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 7, typeof(QueryTriggerInteraction)));
			int d8 = Physics.SphereCastNonAlloc(vector7, radius8, vector8, arrayObject8, maxDistance6, layerMask4, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, d8);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.SphereCastNonAlloc");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CheckSphere(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			float radius = (float)LuaScriptMgr.GetNumber(L, 2);
			bool b = Physics.CheckSphere(vector, radius);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 3)
		{
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 1);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 2);
			int layerMask = (int)LuaScriptMgr.GetNumber(L, 3);
			bool b2 = Physics.CheckSphere(vector2, radius2, layerMask);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		if (num == 4)
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 2);
			int layerMask2 = (int)LuaScriptMgr.GetNumber(L, 3);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 4, typeof(QueryTriggerInteraction)));
			bool b3 = Physics.CheckSphere(vector3, radius3, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CheckSphere");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CheckCapsule(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			float radius = (float)LuaScriptMgr.GetNumber(L, 3);
			bool b = Physics.CheckCapsule(vector, vector2, radius);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 4)
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 3);
			int layermask = (int)LuaScriptMgr.GetNumber(L, 4);
			bool b2 = Physics.CheckCapsule(vector3, vector4, radius2, layermask);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 3);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction)));
			bool b3 = Physics.CheckCapsule(vector5, vector6, radius3, layermask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CheckCapsule");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CheckBox(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			bool b = Physics.CheckBox(vector, vector2);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 3)
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion quaternion = LuaScriptMgr.GetQuaternion(L, 3);
			bool b2 = Physics.CheckBox(vector3, vector4, quaternion);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		if (num == 4)
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion quaternion2 = LuaScriptMgr.GetQuaternion(L, 3);
			int layermask = (int)LuaScriptMgr.GetNumber(L, 4);
			bool b3 = Physics.CheckBox(vector5, vector6, quaternion2, layermask);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion quaternion3 = LuaScriptMgr.GetQuaternion(L, 3);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction)));
			bool b4 = Physics.CheckBox(vector7, vector8, quaternion3, layermask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b4);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CheckBox");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapBox(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			Collider[] o = Physics.OverlapBox(vector, vector2);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		if (num == 3)
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion quaternion = LuaScriptMgr.GetQuaternion(L, 3);
			Collider[] o2 = Physics.OverlapBox(vector3, vector4, quaternion);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		if (num == 4)
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion quaternion2 = LuaScriptMgr.GetQuaternion(L, 3);
			int layerMask = (int)LuaScriptMgr.GetNumber(L, 4);
			Collider[] o3 = Physics.OverlapBox(vector5, vector6, quaternion2, layerMask);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion quaternion3 = LuaScriptMgr.GetQuaternion(L, 3);
			int layerMask2 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction)));
			Collider[] o4 = Physics.OverlapBox(vector7, vector8, quaternion3, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o4);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapBox");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapBoxNonAlloc(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			Collider[] arrayObject = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			int d = Physics.OverlapBoxNonAlloc(vector, vector2, arrayObject);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 4)
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			Collider[] arrayObject2 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			Quaternion quaternion = LuaScriptMgr.GetQuaternion(L, 4);
			int d2 = Physics.OverlapBoxNonAlloc(vector3, vector4, arrayObject2, quaternion);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			Collider[] arrayObject3 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			Quaternion quaternion2 = LuaScriptMgr.GetQuaternion(L, 4);
			int layerMask = (int)LuaScriptMgr.GetNumber(L, 5);
			int d3 = Physics.OverlapBoxNonAlloc(vector5, vector6, arrayObject3, quaternion2, layerMask);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		if (num == 6)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			Collider[] arrayObject4 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			Quaternion quaternion3 = LuaScriptMgr.GetQuaternion(L, 4);
			int layerMask2 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction)));
			int d4 = Physics.OverlapBoxNonAlloc(vector7, vector8, arrayObject4, quaternion3, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, d4);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapBoxNonAlloc");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int BoxCastAll(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] o = Physics.BoxCastAll(vector, vector2, vector3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		if (num == 4)
		{
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion = LuaScriptMgr.GetQuaternion(L, 4);
			RaycastHit[] o2 = Physics.BoxCastAll(vector4, vector5, vector6, quaternion);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion2 = LuaScriptMgr.GetQuaternion(L, 4);
			float maxDistance = (float)LuaScriptMgr.GetNumber(L, 5);
			RaycastHit[] o3 = Physics.BoxCastAll(vector7, vector8, vector9, quaternion2, maxDistance);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		if (num == 6)
		{
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion3 = LuaScriptMgr.GetQuaternion(L, 4);
			float maxDistance2 = (float)LuaScriptMgr.GetNumber(L, 5);
			int layermask = (int)LuaScriptMgr.GetNumber(L, 6);
			RaycastHit[] o4 = Physics.BoxCastAll(vector10, vector11, vector12, quaternion3, maxDistance2, layermask);
			LuaScriptMgr.PushArray(L, o4);
			return 1;
		}
		if (num == 7)
		{
			Vector3 vector13 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector14 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector15 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion4 = LuaScriptMgr.GetQuaternion(L, 4);
			float maxDistance3 = (float)LuaScriptMgr.GetNumber(L, 5);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 6);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 7, typeof(QueryTriggerInteraction)));
			RaycastHit[] o5 = Physics.BoxCastAll(vector13, vector14, vector15, quaternion4, maxDistance3, layermask2, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o5);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.BoxCastAll");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int BoxCastNonAlloc(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 4)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] arrayObject = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			int d = Physics.BoxCastNonAlloc(vector, vector2, vector3, arrayObject);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] arrayObject2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			Quaternion quaternion = LuaScriptMgr.GetQuaternion(L, 5);
			int d2 = Physics.BoxCastNonAlloc(vector4, vector5, vector6, arrayObject2, quaternion);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		if (num == 6)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] arrayObject3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			Quaternion quaternion2 = LuaScriptMgr.GetQuaternion(L, 5);
			float maxDistance = (float)LuaScriptMgr.GetNumber(L, 6);
			int d3 = Physics.BoxCastNonAlloc(vector7, vector8, vector9, arrayObject3, quaternion2, maxDistance);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		if (num == 7)
		{
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] arrayObject4 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			Quaternion quaternion3 = LuaScriptMgr.GetQuaternion(L, 5);
			float maxDistance2 = (float)LuaScriptMgr.GetNumber(L, 6);
			int layermask = (int)LuaScriptMgr.GetNumber(L, 7);
			int d4 = Physics.BoxCastNonAlloc(vector10, vector11, vector12, arrayObject4, quaternion3, maxDistance2, layermask);
			LuaScriptMgr.Push(L, d4);
			return 1;
		}
		if (num == 8)
		{
			Vector3 vector13 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector14 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector15 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] arrayObject5 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			Quaternion quaternion4 = LuaScriptMgr.GetQuaternion(L, 5);
			float maxDistance3 = (float)LuaScriptMgr.GetNumber(L, 6);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 7);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 8, typeof(QueryTriggerInteraction)));
			int d5 = Physics.BoxCastNonAlloc(vector13, vector14, vector15, arrayObject5, quaternion4, maxDistance3, layermask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, d5);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.BoxCastNonAlloc");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int BoxCast(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3)
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 3);
			bool b = Physics.BoxCast(vector, vector2, vector3);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), null))
		{
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit hit;
			bool b2 = Physics.BoxCast(vector4, vector5, vector6, out hit);
			LuaScriptMgr.Push(L, b2);
			LuaScriptMgr.Push(L, hit);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(LuaTable)))
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion = LuaScriptMgr.GetQuaternion(L, 4);
			bool b3 = Physics.BoxCast(vector7, vector8, vector9, quaternion);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(float)))
		{
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion2 = LuaScriptMgr.GetQuaternion(L, 4);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 5);
			bool b4 = Physics.BoxCast(vector10, vector11, vector12, quaternion2, maxDistance);
			LuaScriptMgr.Push(L, b4);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), null, typeof(LuaTable)))
		{
			Vector3 vector13 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector14 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector15 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion3 = LuaScriptMgr.GetQuaternion(L, 5);
			RaycastHit hit2;
			bool b5 = Physics.BoxCast(vector13, vector14, vector15, out hit2, quaternion3);
			LuaScriptMgr.Push(L, b5);
			LuaScriptMgr.Push(L, hit2);
			return 2;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), null, typeof(LuaTable), typeof(float)))
		{
			Vector3 vector16 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector17 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector18 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion4 = LuaScriptMgr.GetQuaternion(L, 5);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 6);
			RaycastHit hit3;
			bool b6 = Physics.BoxCast(vector16, vector17, vector18, out hit3, quaternion4, maxDistance2);
			LuaScriptMgr.Push(L, b6);
			LuaScriptMgr.Push(L, hit3);
			return 2;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 vector19 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector20 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector21 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion5 = LuaScriptMgr.GetQuaternion(L, 4);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 5);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 6);
			bool b7 = Physics.BoxCast(vector19, vector20, vector21, quaternion5, maxDistance3, layerMask);
			LuaScriptMgr.Push(L, b7);
			return 1;
		}
		if (num == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 vector22 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector23 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector24 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion6 = LuaScriptMgr.GetQuaternion(L, 4);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 5);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 6);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)((int)LuaScriptMgr.GetLuaObject(L, 7));
			bool b8 = Physics.BoxCast(vector22, vector23, vector24, quaternion6, maxDistance4, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b8);
			return 1;
		}
		if (num == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), null, typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 vector25 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector26 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector27 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion7 = LuaScriptMgr.GetQuaternion(L, 5);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 6);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 7);
			RaycastHit hit4;
			bool b9 = Physics.BoxCast(vector25, vector26, vector27, out hit4, quaternion7, maxDistance5, layerMask3);
			LuaScriptMgr.Push(L, b9);
			LuaScriptMgr.Push(L, hit4);
			return 2;
		}
		if (num == 8)
		{
			Vector3 vector28 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector29 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector30 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion quaternion8 = LuaScriptMgr.GetQuaternion(L, 5);
			float maxDistance6 = (float)LuaScriptMgr.GetNumber(L, 6);
			int layerMask4 = (int)LuaScriptMgr.GetNumber(L, 7);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)((int)LuaScriptMgr.GetNetObject(L, 8, typeof(QueryTriggerInteraction)));
			RaycastHit hit5;
			bool b10 = Physics.BoxCast(vector28, vector29, vector30, out hit5, quaternion8, maxDistance6, layerMask4, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, b10);
			LuaScriptMgr.Push(L, hit5);
			return 2;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.BoxCast");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IgnoreCollision(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			Collider collider = (Collider)LuaScriptMgr.GetUnityObject(L, 1, typeof(Collider));
			Collider collider2 = (Collider)LuaScriptMgr.GetUnityObject(L, 2, typeof(Collider));
			Physics.IgnoreCollision(collider, collider2);
			return 0;
		}
		if (num == 3)
		{
			Collider collider3 = (Collider)LuaScriptMgr.GetUnityObject(L, 1, typeof(Collider));
			Collider collider4 = (Collider)LuaScriptMgr.GetUnityObject(L, 2, typeof(Collider));
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			Physics.IgnoreCollision(collider3, collider4, boolean);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.IgnoreCollision");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IgnoreLayerCollision(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			int layer = (int)LuaScriptMgr.GetNumber(L, 1);
			int layer2 = (int)LuaScriptMgr.GetNumber(L, 2);
			Physics.IgnoreLayerCollision(layer, layer2);
			return 0;
		}
		if (num == 3)
		{
			int layer3 = (int)LuaScriptMgr.GetNumber(L, 1);
			int layer4 = (int)LuaScriptMgr.GetNumber(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			Physics.IgnoreLayerCollision(layer3, layer4, boolean);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.IgnoreLayerCollision");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetIgnoreLayerCollision(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		int layer = (int)LuaScriptMgr.GetNumber(L, 1);
		int layer2 = (int)LuaScriptMgr.GetNumber(L, 2);
		bool ignoreLayerCollision = Physics.GetIgnoreLayerCollision(layer, layer2);
		LuaScriptMgr.Push(L, ignoreLayerCollision);
		return 1;
	}
}
