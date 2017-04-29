using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class HurtData
	{
		public List<float> lRandom = new List<float>
		{
			1.05f,
			1.01f,
			0.96f,
			1.02f,
			0.95f,
			1.03f,
			0.99f,
			1f
		};

		public int maxComboNum;

		public float timer;

		public BaseRole _ava;

		public hurtInfo _hurtD;

		public int singleAttack;

		public int comboNum = 0;

		public bool criatk = false;

		public bool useCombo = false;

		private bool inited = false;

		private int idx = 0;

		public HurtData(BaseRole ava, hurtInfo hurtD, bool combo)
		{
			this._ava = ava;
			this._hurtD = hurtD;
			this.useCombo = combo;
		}

		public void playRest()
		{
			bool flag = this.comboNum == 0;
			if (!flag)
			{
				this._hurtD.dmg = (int)((float)this._hurtD.dmg * (float)this.comboNum);
				bool flag2 = this._hurtD.isdie && !this._hurtD.isdie;
				if (flag2)
				{
				}
				this.comboNum = 0;
			}
		}

		public bool play(int max)
		{
			bool flag = !this.inited;
			if (flag)
			{
				this.inited = true;
				this.maxComboNum = max;
			}
			bool flag2 = this.maxComboNum >= 1 && this.comboNum == 0;
			if (flag2)
			{
				this.comboNum = this.maxComboNum;
			}
			bool flag3 = this.useCombo;
			if (flag3)
			{
				combo_txt.play();
			}
			bool flag4 = this.comboNum > 0;
			bool result;
			if (flag4)
			{
				this.doHurt();
				this.comboNum--;
				bool flag5 = this.comboNum == 0;
				result = flag5;
			}
			else
			{
				this.doHurt();
				result = true;
			}
			return result;
		}

		private void doHurt()
		{
			bool flag = this.comboNum > 0;
			if (flag)
			{
				this.idx++;
				bool flag2 = this.idx >= this.lRandom.Count;
				if (flag2)
				{
					this.idx = 0;
				}
			}
		}
	}
}
