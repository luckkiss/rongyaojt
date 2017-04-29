using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class AttShowStruct : ShowStruct
	{
		private float _first_tm = 0f;

		private float _total_tm = 1100f;

		private int _up_tm = 300;

		private int _down_tm = 600;

		private float _bn = 80f;

		private float _cn = 100f;

		private int _scale = 500;

		private float _wait_tm = 0f;

		private float _wait_curtm = 0f;

		private int _tp = 0;

		private int _last_update_tm = 0;

		private Vec2 _relative_pos;

		public float wait_tm
		{
			get
			{
				return this._wait_tm;
			}
			set
			{
				this._wait_tm = value;
			}
		}

		public AttShowStruct(IUIBaseControl disp, IUIContainer parent)
		{
			this._disp = disp;
			this._parent = parent;
		}

		public void SetRelativePos(Vec2 p)
		{
			this._relative_pos = p;
			this._disp.x = p.x;
			this._disp.y = p.y;
		}

		public override bool Update()
		{
			float num = (float)GameTools.getTimer();
			int num2 = (int)(num - (float)this._last_update_tm);
			float num3 = (float)GameTools.getTimer();
			bool flag = this._wait_curtm == 0f;
			bool result;
			if (flag)
			{
				this._wait_curtm = num3;
				result = true;
			}
			else
			{
				bool flag2 = this._wait_tm > num3 - this._wait_curtm;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = this._first_tm == 0f;
					if (flag3)
					{
						this._first_tm = num;
					}
					else
					{
						float num4 = num - this._first_tm;
						bool flag4 = !this._isadd && this._parent != null && this._disp != null;
						if (flag4)
						{
							this._parent.addChild(this._disp);
							this._isadd = true;
						}
						bool flag5 = num4 < this._total_tm;
						if (!flag5)
						{
							bool flag6 = this._parent != null && this._disp != null;
							if (flag6)
							{
								this._parent.removeChild(this._disp, false);
							}
							result = false;
							return result;
						}
						float num5 = num4 / (float)this._up_tm;
						bool flag7 = this._tp == 0;
						if (flag7)
						{
							bool flag8 = num4 < (float)this._up_tm;
							if (flag8)
							{
								num5 *= num5 - 2f;
								this._disp.y = this._relative_pos.y - this._cn * 2f / 3f + this._cn * 1f / 3f * num5;
								num5 = num4 / (float)(this._up_tm + this._down_tm);
								num5 *= num5 - 2f;
								this._disp.x = this._relative_pos.x + 70f + this._bn * num5;
							}
							else
							{
								bool flag9 = num4 > (float)(this._up_tm + this._down_tm);
								if (flag9)
								{
									float num6 = (float)(this._up_tm + this._down_tm);
									this._disp.alpha = (float)(1.0 - 0.5 * (double)num4 / (double)this._total_tm);
									this._disp.y = this._relative_pos.y + this._bn * (num4 - num6) / (this._total_tm - num6);
								}
								else
								{
									num5 = num4 / (float)(this._up_tm + this._down_tm);
									num5 *= num5 - 2f;
									this._disp.x = this._relative_pos.x + 70f + this._bn * num5;
									num5 = (num4 - (float)this._up_tm) / (float)this._down_tm;
									num5 *= num5 - 2f;
									this._disp.y = this._relative_pos.y - this._cn - this._cn * num5;
								}
							}
							bool flag10 = num4 < (float)this._scale;
							if (flag10)
							{
								num5 = (float)(0.7 + 0.3 * (double)(num4 / (float)this._scale));
							}
						}
					}
					this._last_update_tm = (int)num;
					result = true;
				}
			}
			return result;
		}
	}
}
