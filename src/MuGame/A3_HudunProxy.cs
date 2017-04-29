using Cross;
using System;
using UnityEngine;

namespace MuGame
{
	internal class A3_HudunProxy : BaseProxy<A3_HudunProxy>
	{
		private HudunModel HudunModel = ModelBase<HudunModel>.getInstance();

		private bool isUplvl = false;

		private bool isshow = true;

		public A3_HudunProxy()
		{
			this.addProxyListener(116u, new Action<Variant>(this.onLoadinfo));
		}

		public void sendinfo(int val)
		{
			Variant variant = new Variant();
			variant["op"] = val;
			this.sendRPC(116u, variant);
		}

		public void sendinfo(int val, int auto)
		{
			Variant variant = new Variant();
			variant["op"] = val;
			variant["auto"] = auto;
			this.sendRPC(116u, variant);
		}

		private void onLoadinfo(Variant data)
		{
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(data["res"]);
			}
			else
			{
				Debug.Log(data.dump());
				bool flag2 = data["level"] > this.HudunModel.Level;
				if (flag2)
				{
					bool flag3 = a3_hudun._instance;
					if (flag3)
					{
						a3_hudun._instance.AniUpLvl();
					}
					this.isUplvl = true;
				}
				int nowCount = this.HudunModel.NowCount;
				this.HudunModel.Level = data["level"];
				this.HudunModel.NowCount = data["holy_shield"];
				this.isshow = true;
				bool flag4 = data["auto"] == 0;
				if (flag4)
				{
					this.HudunModel.is_auto = false;
				}
				bool flag5 = a3_hudun._instance;
				if (flag5)
				{
					bool flag6 = this.isUplvl;
					if (flag6)
					{
						a3_hudun._instance.updata_hd(0);
						this.isUplvl = false;
					}
					else
					{
						a3_hudun._instance.updata_hd(nowCount);
					}
				}
			}
		}

		public void Add_energy_auto()
		{
			bool is_auto = this.HudunModel.is_auto;
			if (is_auto)
			{
				bool flag = this.HudunModel.Level <= 0;
				if (!flag)
				{
					bool flag2 = this.HudunModel.NowCount >= this.HudunModel.GetMaxCount(this.HudunModel.Level);
					if (!flag2)
					{
						bool flag3 = this.HudunModel.OnMjCountOk_auto(this.HudunModel.hdData[this.HudunModel.Level].addcount);
						if (flag3)
						{
							this.sendinfo(2);
							flytxt.instance.fly("护盾自动充能成功！！", 1, default(Color), null);
						}
						else
						{
							bool flag4 = this.isshow;
							if (flag4)
							{
								flytxt.instance.fly("魔晶数量不足！！", 1, default(Color), null);
								this.isshow = false;
							}
						}
					}
				}
			}
		}
	}
}
