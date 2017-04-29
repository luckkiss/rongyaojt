using LuaInterface;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFramework
{
	public class LuaBehaviour : View
	{
		private string data;

		protected static bool initialize;

		private Dictionary<GameObject, LuaFunction> buttons = new Dictionary<GameObject, LuaFunction>();

		private void Awake()
		{
			this.CallMethod("Awake", new object[]
			{
				base.gameObject
			});
		}

		protected void Start()
		{
			if (base.LuaManager != null && LuaBehaviour.initialize)
			{
				LuaState lua = base.LuaManager.lua;
				lua[base.name + ".transform"] = base.transform;
				lua[base.name + ".gameObject"] = base.gameObject;
			}
			this.CallMethod("Start", new object[0]);
		}

		protected void OnClick()
		{
			this.CallMethod("OnClick", new object[0]);
		}

		protected void OnClickEvent(GameObject go)
		{
			this.CallMethod("OnClick", new object[]
			{
				go
			});
		}

		public void AddClick(GameObject go, LuaFunction luafunc)
		{
			if (go == null || luafunc == null)
			{
				return;
			}
			Button button = go.GetComponent<Button>();
			if (button == null)
			{
				button = go.AddComponent<Button>();
			}
			this.buttons.Add(go, luafunc);
			button.onClick.AddListener(delegate
			{
				luafunc.Call(new object[]
				{
					go
				});
			});
		}

		public void RemoveClick(GameObject go)
		{
			if (go == null)
			{
				return;
			}
			LuaFunction luaFunction = null;
			if (this.buttons.TryGetValue(go, out luaFunction))
			{
				luaFunction.Dispose();
				luaFunction = null;
				this.buttons.Remove(go);
			}
		}

		public void ClearClick()
		{
			foreach (KeyValuePair<GameObject, LuaFunction> current in this.buttons)
			{
				if (current.Value != null)
				{
					current.Value.Dispose();
				}
			}
			this.buttons.Clear();
		}

		protected object[] CallMethod(string func, params object[] args)
		{
			if (!LuaBehaviour.initialize)
			{
				return null;
			}
			return Util.CallMethod(base.name, func, args);
		}

		private void OnDestroy()
		{
			this.ClearClick();
			base.LuaManager = null;
			Util.ClearMemory();
			Debug.Log("~" + base.name + " was destroy!");
		}
	}
}
