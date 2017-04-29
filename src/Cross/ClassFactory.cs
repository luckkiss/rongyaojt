using System;
using System.Collections.Generic;

namespace Cross
{
	public class ClassFactory
	{
		public delegate object CreateFunc();

		protected Dictionary<string, ClassFactory.CreateFunc> _classes;

		public ClassFactory()
		{
			this._classes = new Dictionary<string, ClassFactory.CreateFunc>();
		}

		public void regClass(string name, ClassFactory.CreateFunc func)
		{
			this._classes[name] = func;
		}

		public void unregClass(string name)
		{
			bool flag = this._classes.ContainsKey(name);
			if (flag)
			{
				this._classes.Remove(name);
			}
		}

		public object createClassInst(string name)
		{
			bool flag = this._classes.ContainsKey(name);
			object result;
			if (flag)
			{
				result = this._classes[name]();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
