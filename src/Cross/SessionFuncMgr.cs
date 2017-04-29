using System;
using System.Collections.Generic;

namespace Cross
{
	public class SessionFuncMgr
	{
		public delegate bool luaDelegate(uint id, Variant v);

		private Dictionary<uint, Action<Variant>> _Func = new Dictionary<uint, Action<Variant>>();

		private Dictionary<uint, Action<Variant>> _TpkgFunc = new Dictionary<uint, Action<Variant>>();

		public SessionFuncMgr.luaDelegate _luaFunc;

		public SessionFuncMgr.luaDelegate _luaTpkgFunc;

		public static SessionFuncMgr instance;

		public void addFunc(uint id, Action<Variant> func, bool isRpc = true)
		{
			Dictionary<uint, Action<Variant>> dictionary = isRpc ? this._Func : this._TpkgFunc;
			bool flag = dictionary.ContainsKey(id);
			if (!flag)
			{
				dictionary[id] = func;
			}
		}

		public void delFunc(uint id, bool isRpc = true)
		{
			Dictionary<uint, Action<Variant>> dictionary = isRpc ? this._Func : this._TpkgFunc;
			bool flag = dictionary.ContainsKey(id);
			if (!flag)
			{
				dictionary.Remove(id);
			}
		}

		public void editFunc(uint id, Action<Variant> func, bool isRpc = true)
		{
			Dictionary<uint, Action<Variant>> dictionary = isRpc ? this._Func : this._TpkgFunc;
			dictionary[id] = func;
		}

		public bool onRpc(uint id, Variant data)
		{
			bool result = false;
			bool flag = this._Func.ContainsKey(id);
			if (flag)
			{
				this._Func[id](data);
				result = true;
			}
			bool flag2 = this._luaFunc != null;
			if (flag2)
			{
				bool flag3 = this._luaFunc(id, data);
				if (flag3)
				{
					result = true;
				}
			}
			return result;
		}

		public bool onTpkg(uint id, Variant data)
		{
			bool result = false;
			bool flag = this._TpkgFunc.ContainsKey(id);
			if (flag)
			{
				this._TpkgFunc[id](data);
				result = true;
			}
			bool flag2 = this._luaTpkgFunc != null;
			if (flag2)
			{
				bool flag3 = this._luaTpkgFunc(id, data);
				if (flag3)
				{
					result = true;
				}
			}
			return result;
		}

		public static void init()
		{
			SessionFuncMgr.instance = new SessionFuncMgr();
		}
	}
}
