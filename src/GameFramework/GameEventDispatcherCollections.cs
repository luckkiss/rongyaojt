using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public class GameEventDispatcherCollections : GameEventDispatcher, IGameEventDispatcherCollections
	{
		private Dictionary<string, IGameEventDispatcher> _dispatcherMap = new Dictionary<string, IGameEventDispatcher>();

		public void regEventDispatcher(string objectName, IGameEventDispatcher ed)
		{
			bool flag = this._dispatcherMap.ContainsKey(objectName);
			if (flag)
			{
				DebugTrace.print("ERR regEventDispatcher [" + objectName + "] exsit!");
			}
			else
			{
				bool flag2 = ed == null;
				if (flag2)
				{
					DebugTrace.print("ERR regEventDispatcher [" + objectName + "] null!");
				}
				else
				{
					this._dispatcherMap[objectName] = ed;
				}
			}
		}

		public bool dispatchEventCL(string objectName, GameEvent evt)
		{
			bool flag = !this._dispatcherMap.ContainsKey(objectName);
			bool result;
			if (flag)
			{
				DebugTrace.print("ERR dispatchEventCL [" + objectName + "] not exsit!");
				result = false;
			}
			else
			{
				IGameEventDispatcher gameEventDispatcher = this._dispatcherMap[objectName];
				gameEventDispatcher.dispatchEvent(evt);
				result = true;
			}
			return result;
		}

		public void addEventListenerCL(string objectName, uint type, Action<GameEvent> listener)
		{
			bool flag = !this._dispatcherMap.ContainsKey(objectName);
			if (flag)
			{
				DebugTrace.print("ERR addEventListenerCL [" + objectName + "] not exsit!");
			}
			else
			{
				IGameEventDispatcher gameEventDispatcher = this._dispatcherMap[objectName];
				gameEventDispatcher.addEventListener(type, listener);
			}
		}

		public bool hasEventListenerCL(string objectName, uint type)
		{
			bool flag = !this._dispatcherMap.ContainsKey(objectName);
			bool result;
			if (flag)
			{
				DebugTrace.print("ERR hasEventListenerCL [" + objectName + "] not exsit!");
				result = false;
			}
			else
			{
				IGameEventDispatcher gameEventDispatcher = this._dispatcherMap[objectName];
				result = gameEventDispatcher.hasEventListener(type);
			}
			return result;
		}

		public void removeEventListenerCL(string objectName, uint type, Action<GameEvent> listener)
		{
			bool flag = !this._dispatcherMap.ContainsKey(objectName);
			if (flag)
			{
				DebugTrace.print("ERR removeEventListenerCL [" + objectName + "] not exsit!");
			}
			else
			{
				IGameEventDispatcher gameEventDispatcher = this._dispatcherMap[objectName];
				gameEventDispatcher.removeEventListener(type, listener);
			}
		}
	}
}
