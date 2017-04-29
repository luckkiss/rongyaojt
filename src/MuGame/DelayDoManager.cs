using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class DelayDoManager
	{
		private muLGClient g_mgr;

		public static DelayDoManager singleton;

		private Variant _delayDos = new Variant();

		public const uint CF_NONE = 0u;

		public const uint CF_CHANGE_MAP = 1u;

		public const uint CF_SETSYSTEM = 2u;

		public DelayDoManager(muLGClient m)
		{
			DelayDoManager.singleton = this;
			this.g_mgr = m;
		}

		public void AddDelayDo(Action fun, int tm, uint clearFlag = 0u)
		{
			long num = this.g_mgr.timers.addTimer(tm, delegate(object data)
			{
				fun();
			}, 1, null);
			this._delayDos.pushBack(GameTools.createGroup(new object[]
			{
				"fun",
				fun,
				"id",
				num,
				"cf",
				clearFlag
			}));
		}

		public void RmvDelayDo(Action fun)
		{
			for (int i = this._delayDos.Count - 1; i >= 0; i--)
			{
				Variant variant = this._delayDos[i];
				bool flag = variant["fun"]._val as Action == fun;
				if (flag)
				{
					long id = variant["id"];
					this._delayDos._arr.RemoveAt(i);
					this.g_mgr.timers.removeTimer(id);
				}
			}
		}

		public void ClearDelayDoByFlag(uint cf)
		{
			bool flag = this._delayDos.Count == 0;
			if (!flag)
			{
				bool flag2 = cf == 0u;
				if (flag2)
				{
					this._delayDos = new Variant();
				}
				else
				{
					for (int i = this._delayDos.Count - 1; i >= 0; i--)
					{
						Variant variant = this._delayDos[i];
						bool flag3 = (variant["cf"] & cf) > 0u;
						if (flag3)
						{
							long id = variant["id"];
							this._delayDos._arr.RemoveAt(i);
							this.g_mgr.timers.removeTimer(id);
						}
					}
				}
			}
		}
	}
}
