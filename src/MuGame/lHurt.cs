using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class lHurt
	{
		public List<HurtData> l;

		private int _maxComboNum = 0;

		public float lastTimer;

		public int maxComboNum
		{
			get
			{
				return this._maxComboNum;
			}
			set
			{
				this.lastTimer = Time.time;
				this._maxComboNum = value;
				foreach (HurtData current in this.l)
				{
					bool flag = current.maxComboNum == 0;
					if (flag)
					{
						current.maxComboNum = this._maxComboNum;
					}
				}
			}
		}

		public int Count
		{
			get
			{
				return this.l.Count;
			}
		}

		public lHurt()
		{
			this.l = new List<HurtData>();
		}

		public void add(HurtData d)
		{
			float time = Time.time;
			bool flag = (double)(time - this.lastTimer) > 0.7;
			if (flag)
			{
				this._maxComboNum = 0;
			}
			d.timer = time;
			d.maxComboNum = this._maxComboNum;
			this.l.Add(d);
		}
	}
}
