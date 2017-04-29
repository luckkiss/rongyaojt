using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class damageShowStruct : ShowStruct
	{
		public delegate bool fun();

		public damageShowStruct(IUIBaseControl disp, IUIContainer parent)
		{
			this._disp = disp;
			this._parent = parent;
		}

		public override bool Update()
		{
			damageShowStruct.fun fun = null;
			int @int = base.userdata["conf"]["tp"]._int;
			if (@int != 0)
			{
				if (@int == 1)
				{
					fun = new damageShowStruct.fun(this.updateFortp2);
				}
			}
			else
			{
				fun = new damageShowStruct.fun(this.updateFortp1);
			}
			return fun();
		}

		private bool updateFortp2()
		{
			float num = (float)GameTools.getTimer();
			Variant variant = base.userdata["conf"];
			float num2 = num - base.userdata["stm"];
			bool flag = num >= base.userdata["etm"];
			bool result;
			if (flag)
			{
				bool flag2 = this._isadd && this._parent != null && this._disp != null;
				if (flag2)
				{
					this._parent.removeChild(this._disp, false);
				}
				result = false;
			}
			else
			{
				int num3 = variant["distance"];
				float num4 = (float)(variant["ttm"] - variant["stop_ttm"]);
				bool flag3 = num2 < variant["stop_s"];
				float num5;
				float val;
				float val2;
				if (flag3)
				{
					num5 = variant["stop_s"];
					val = base.userdata["stx"];
					val2 = base.userdata["sty"];
				}
				else
				{
					bool flag4 = num2 > (float)(variant["stop_s"]._int + variant["stop_ttm"]._int);
					if (flag4)
					{
						float num6 = num2 - variant["stop_ttm"] - variant["stop_s"];
						num5 = num4 - variant["stop_s"];
						val = (float)(num3 * base.userdata["dri"]["x"]) * (variant["stop_s"] / num4) + base.userdata["stx"];
						val2 = (float)(num3 * base.userdata["dri"]["y"]) * (variant["stop_s"] / num4) + base.userdata["sty"];
					}
					else
					{
						num5 = 0f;
						val = (float)(num3 * base.userdata["dri"]["x"]) * (variant["stop_s"] / num4) + base.userdata["stx"];
						val2 = (float)(num3 * base.userdata["dri"]["y"]) * (variant["stop_s"] / num4) + base.userdata["sty"];
					}
				}
				float val3 = (float)(num3 * base.userdata["dri"]["x"]) * (num5 / num4);
				float val4 = (float)(num3 * base.userdata["dri"]["y"]) * (num5 / num4);
				Random random = new Random();
				num5 += (float)(random.Next(0, 1) * 50);
				Variant variant2 = GameTools.createGroup(new Variant[]
				{
					"duration",
					num5,
					"change",
					val3,
					"begin",
					val
				});
				variant2 = GameTools.createGroup(new Variant[]
				{
					"duration",
					num5,
					"change",
					val4,
					"begin",
					val2
				});
				bool flag5 = variant["ttm"] - num2 < variant["alphatm"];
				if (flag5)
				{
					this._disp.alpha = (variant["ttm"] - num2) / variant["alphatm"];
				}
				bool flag6 = !this._isadd && this._parent != null && this._disp != null;
				if (flag6)
				{
					this._parent.addChild(this._disp);
					this._isadd = true;
				}
				result = true;
			}
			return result;
		}

		private bool updateFortp1()
		{
			Variant userdata = base.userdata;
			Variant variant = base.userdata["conf"];
			float num = (float)GameTools.getTimer();
			float num2 = num - userdata["stm"];
			bool flag = num >= base.userdata["etm"];
			bool result;
			if (flag)
			{
				bool flag2 = this._isadd && this._parent != null && this._disp != null;
				if (flag2)
				{
					this._parent.removeChild(this._disp, false);
				}
				result = false;
			}
			else
			{
				bool flag3 = userdata["criatk"];
				float num3;
				if (flag3)
				{
					bool flag4 = num2 < variant["scaletm_b"];
					if (flag4)
					{
						num3 = num2 / variant["scaletm_b"] + 1f;
					}
					else
					{
						bool flag5 = num2 < (float)(variant["scaletm_s"]._int + variant["scaletm_b"]._int);
						if (flag5)
						{
							num3 = 2f - (num2 - (float)variant["scaletm_b"]._int) / (float)variant["scaletm_s"]._int;
						}
						else
						{
							num3 = 1f;
							userdata["criatk"] = false;
						}
					}
					this._disp.x = userdata["stx"] - (num3 - 1f) * this._disp.width / 4f;
				}
				num3 = num2 / variant["ttm"] - 1f;
				this._disp.y = userdata["sty"] - 120f * (num3 * num3 * num3 + 1f);
				bool flag6 = variant["ttm"] - num2 < variant["alphatm"];
				if (flag6)
				{
					this._disp.alpha = (variant["ttm"] - num2) / variant["alphatm"];
				}
				bool flag7 = !this._isadd && this._parent != null && this._disp != null;
				if (flag7)
				{
					this._parent.addChild(this._disp);
					this._isadd = true;
				}
				result = true;
			}
			return result;
		}
	}
}
