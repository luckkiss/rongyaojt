using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public class GameEvent
	{
		protected static LinkedList<GameEvent> m_pool = new LinkedList<GameEvent>();

		private uint _type;

		private object _target;

		private object _data;

		private bool _GC_FLAG;

		private bool _immediately;

		public bool immediately
		{
			get
			{
				return this._immediately;
			}
		}

		public bool GC_FLAG
		{
			get
			{
				return this._GC_FLAG;
			}
		}

		public uint type
		{
			get
			{
				return this._type;
			}
		}

		public object target
		{
			get
			{
				return this._target;
			}
		}

		public Variant data
		{
			get
			{
				return this._data as Variant;
			}
		}

		public object orgdata
		{
			get
			{
				return this._data;
			}
		}

		public static GameEvent alloc(uint type, object target, object data, bool gcFlag = false, bool flag = true)
		{
			bool flag2 = GameEvent.m_pool.Count > 0;
			GameEvent result;
			if (flag2)
			{
				GameEvent value = GameEvent.m_pool.Last.Value;
				GameEvent.m_pool.RemoveLast();
				value._type = type;
				value._target = target;
				value._data = data;
				value._immediately = flag;
				value._GC_FLAG = gcFlag;
				result = value;
			}
			else
			{
				result = new GameEvent(type, target, data, flag, false);
			}
			return result;
		}

		public static void free(GameEvent evt)
		{
			bool flag = evt == null;
			if (!flag)
			{
				evt._type = 0u;
				evt._target = null;
				evt._data = null;
				evt._immediately = false;
				GameEvent.m_pool.AddLast(evt);
			}
		}

		public GameEvent(uint type, object target, object data, bool flag, bool gcFlag = false)
		{
			this._type = type;
			this._target = target;
			this._data = data;
			this._immediately = flag;
			this._GC_FLAG = gcFlag;
		}

		public static GameEvent Create(uint type, object target, object data, bool gcFlag = false)
		{
			return GameEvent.alloc(type, target, data, gcFlag, false);
		}

		public static GameEvent Createimmedi(uint type, object target, object data, bool gcFlag = false)
		{
			return GameEvent.alloc(type, target, data, gcFlag, true);
		}
	}
}
