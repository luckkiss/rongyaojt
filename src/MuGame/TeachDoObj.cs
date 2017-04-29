using System;

namespace MuGame
{
	public struct TeachDoObj
	{
		public Action<object[], Action> _doFun;

		public Action forcedo;

		public int _pramNum;

		public object[] _param;
	}
}
