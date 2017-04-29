using GameFramework;
using System;

namespace MuGame
{
	public class ModelBase<T> : GameEventDispatcher where T : class, new()
	{
		private static T _instance;

		public static T getInstance()
		{
			bool flag = ModelBase<T>._instance == null;
			if (flag)
			{
				bool flag2 = ModelBase<T>._instance == null;
				if (flag2)
				{
					ModelBase<T>._instance = Activator.CreateInstance<T>();
				}
			}
			return ModelBase<T>._instance;
		}
	}
}
