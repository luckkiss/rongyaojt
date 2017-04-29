using Cross;
using System;

namespace MuGame
{
	internal class DisplayUtil
	{
		protected muUIClient _lgClient;

		public static DisplayUtil singleton;

		public DisplayUtil(muUIClient m)
		{
			this._lgClient = m;
			DisplayUtil.singleton = this;
		}

		public uint GetEqpColorUint(Variant data, Variant conf, bool drop = false)
		{
			uint num = 16777215u;
			if (drop)
			{
				num = 13421772u;
			}
			bool flag = data == null;
			uint result;
			if (flag)
			{
				result = num;
			}
			else
			{
				int num2 = 0;
				bool flag2 = data.ContainsKey("flvl");
				if (flag2)
				{
					num2 = data["flvl"];
					bool flag3 = num2 < 7;
					if (flag3)
					{
						num = 16777215u;
					}
					else
					{
						num = 16763929u;
					}
				}
				bool flag4 = data.ContainsKey("fp") && (data["fp"] | 61680) > 61680;
				if (flag4)
				{
					bool flag5 = num2 < 7;
					if (flag5)
					{
						num = 6730495u;
					}
				}
				bool flag6 = data.ContainsKey("flag") && data["flag"] > 0;
				if (flag6)
				{
					bool flag7 = 2 == data["flag"] && conf != null;
					if (flag7)
					{
						bool flag8 = conf.ContainsKey("skil") && num2 < 7;
						if (flag8)
						{
							num = 6730495u;
						}
					}
					else
					{
						bool flag9 = 1 == data["flag"];
						if (flag9)
						{
							num = 6730495u;
						}
					}
				}
				bool flag10 = this.IsExatt(data);
				if (flag10)
				{
					num = 1703808u;
				}
				bool flag11 = conf != null && conf.ContainsKey("suitid");
				if (flag11)
				{
					num = 65280u;
				}
				result = num;
			}
			return result;
		}

		public int CountExatt(Variant data)
		{
			int num = 0;
			bool flag = data.ContainsKey("veriex_cnt");
			if (flag)
			{
				num += data["veriex_cnt"];
			}
			bool flag2 = data.ContainsKey("exatt");
			if (flag2)
			{
				for (int i = 0; i < 32; i++)
				{
					bool flag3 = (1 << i & data["exatt"]) != 0;
					if (flag3)
					{
						num++;
					}
				}
			}
			else
			{
				bool flag4 = data.ContainsKey("exatt_show") && data["exatt_show"] != null;
				if (flag4)
				{
					num++;
				}
				else
				{
					bool flag5 = data.ContainsKey("make_att");
					if (flag5)
					{
						Variant variant = (this._lgClient.g_gameConfM as muCLientConfig).svrItemConf.get_make_att(data["make_att"]);
						bool flag6 = variant;
						if (flag6)
						{
							num += this.CountExatt(variant);
						}
					}
				}
			}
			bool flag7 = data.ContainsKey("ex_att_grp");
			if (flag7)
			{
				Variant variant2 = (this._lgClient.g_gameConfM as muCLientConfig).svrItemConf.Get_ex_att_grp(data["ex_att_grp"]);
				bool flag8 = variant2 != null;
				if (flag8)
				{
					num += variant2["mincnt"];
				}
				else
				{
					num++;
				}
			}
			return num;
		}

		public bool IsExatt(Variant data)
		{
			bool flag = data == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = data.ContainsKey("make_att") && data["make_att"] > 0;
				if (flag2)
				{
					Variant variant = (this._lgClient.g_gameConfM as muCLientConfig).svrItemConf.get_make_att(data["make_att"]);
					bool flag3 = variant != null;
					if (flag3)
					{
						bool flag4 = this.IsExatt(variant);
						if (flag4)
						{
							result = true;
							return result;
						}
					}
				}
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				bool flag5 = data.ContainsKey("exatt") && data["exatt"] != null;
				if (flag5)
				{
					int.TryParse(data["exatt"], out num);
				}
				bool flag6 = data.ContainsKey("ex_att_grp");
				if (flag6)
				{
					int.TryParse(data["ex_att_grp"], out num2);
				}
				bool flag7 = data.ContainsKey("veriex_cnt");
				if (flag7)
				{
					int.TryParse(data["veriex_cnt"], out num3);
				}
				result = (num > 0 || num2 > 0 || num3 > 0);
			}
			return result;
		}
	}
}
