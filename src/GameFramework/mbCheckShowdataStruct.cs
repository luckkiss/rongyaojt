using Cross;
using System;

namespace GameFramework
{
	public struct mbCheckShowdataStruct
	{
		public Action lfCallBack;

		public Action rtCallBack;

		public Action confirmCallBack;

		public Action closeFun;

		public Action<bool> setCheck;

		public string lineAlign;

		public string ok;

		public string leftStr;

		public string rightStr;

		public string checkLabel;

		public string title;

		public Variant msgArr;
	}
}
