using System;

namespace GameFramework
{
	public struct notifyStruct
	{
		public string text;

		public Action onOk;

		public Action onCancel;
	}
}
