using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public class GameEventDispatcher : IGameEventDispatcher
	{
		private Dictionary<uint, List<Action<GameEvent>>> _dispatcherMap;

		private Dictionary<uint, List<Action<GameEvent>>> _removeMap;

		private bool _processFlag = false;

		private gameEventDelegate eventDelegate
		{
			get
			{
				return CrossApp.singleton.getPlugin("gameEventDelegate") as gameEventDelegate;
			}
		}

		public GameEventDispatcher()
		{
			this._dispatcherMap = new Dictionary<uint, List<Action<GameEvent>>>();
			this._removeMap = new Dictionary<uint, List<Action<GameEvent>>>();
		}

		public bool dispatchEvent(GameEvent evt)
		{
			bool flag = !this._dispatcherMap.ContainsKey(evt.type);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				uint type = evt.type;
				List<Action<GameEvent>> list = null;
				bool flag2 = this._removeMap.Count > 0 && this._removeMap.ContainsKey(type);
				if (flag2)
				{
					list = this._removeMap[type];
				}
				this._processFlag = true;
				List<Action<GameEvent>> list2 = this._dispatcherMap[type];
				for (int i = 0; i < list2.Count; i++)
				{
					Action<GameEvent> action = list2[i];
					bool flag3 = list != null && list.IndexOf(action) >= 0;
					if (!flag3)
					{
						bool immediately = evt.immediately;
						if (immediately)
						{
							action(evt);
						}
						else
						{
							this.eventDelegate.addEventTask(evt, action);
						}
					}
				}
				bool gC_FLAG = evt.GC_FLAG;
				if (gC_FLAG)
				{
					GameEvent.free(evt);
				}
				this._processFlag = false;
				bool flag4 = list != null && list.Count > 0;
				if (flag4)
				{
					foreach (Action<GameEvent> current in list2)
					{
						this.removeListener(list, current);
					}
					this._removeMap.Clear();
				}
				result = true;
			}
			return result;
		}

		public void addEventListener(uint type, Action<GameEvent> listener)
		{
			bool flag = !this._dispatcherMap.ContainsKey(type);
			if (flag)
			{
				this._dispatcherMap[type] = new List<Action<GameEvent>>();
			}
			List<Action<GameEvent>> list = this._dispatcherMap[type];
			list.Add(listener);
		}

		public bool hasEventListener(uint type)
		{
			return this._dispatcherMap.ContainsKey(type);
		}

		public void removeEventListener(uint type, Action<GameEvent> listener)
		{
			bool flag = !this._dispatcherMap.ContainsKey(type);
			if (!flag)
			{
				bool processFlag = this._processFlag;
				if (processFlag)
				{
					bool flag2 = !this._removeMap.ContainsKey(type);
					if (flag2)
					{
						this._removeMap[type] = new List<Action<GameEvent>>();
					}
					List<Action<GameEvent>> list = this._removeMap[type];
					list.Add(listener);
				}
				else
				{
					List<Action<GameEvent>> listenerFuns = this._dispatcherMap[type];
					this.removeListener(listenerFuns, listener);
				}
			}
		}

		public void removeListener(List<Action<GameEvent>> listenerFuns, Action<GameEvent> listener)
		{
			bool flag = listenerFuns.Count <= 0;
			if (!flag)
			{
				for (int i = listenerFuns.Count - 1; i >= 0; i--)
				{
					bool flag2 = listenerFuns[i] != listener;
					if (!flag2)
					{
						listenerFuns.RemoveAt(i);
					}
				}
			}
		}

		public void removeAllListener()
		{
			this._removeMap.Clear();
			this._dispatcherMap.Clear();
		}
	}
}
