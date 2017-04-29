using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientVipConf : configParser
	{
		public ClientVipConf(ClientConfig m) : base(m)
		{
		}

		public static ClientVipConf create(IClientBase m)
		{
			return new ClientVipConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("vipdata");
			if (flag)
			{
				Variant variant = conf["vipdata"][0];
				bool flag2 = variant != null;
				if (flag2)
				{
					Variant variant2 = variant["vip"];
					Variant variant3 = new Variant();
					foreach (Variant current in variant2._arr)
					{
						bool flag3 = current.ContainsKey("des");
						if (flag3)
						{
							Variant variant4 = current["des"][0];
							Variant value = variant4["item"];
							variant4[0] = value;
							conf["vipdata"]["des"] = variant4;
						}
						bool flag4 = current.ContainsKey("fun");
						if (flag4)
						{
							Variant variant5 = current["fun"][0];
							Variant variant6 = variant5["item"];
							foreach (Variant current2 in variant6._arr)
							{
								current2["item"] = GameTools.array2Map(current2["item"], "des", 1u);
							}
							variant5[0] = variant6;
							conf["vipdata"]["fun"] = variant5;
						}
						bool flag5 = current.ContainsKey("state");
						if (flag5)
						{
							Variant variant7 = current["state"][0];
							Variant variant8 = variant7["item"];
							foreach (Variant current3 in variant8._arr)
							{
								current3["item"] = GameTools.array2Map(current3["item"], "des", 1u);
							}
							variant7[0] = variant8;
							conf["vipdata"]["state"] = variant7;
						}
						variant3[current["id"]] = current;
					}
					conf["vipdata"] = variant3;
				}
			}
			return conf;
		}

		public Variant get_vip_description(int id = 1)
		{
			return base.conf["vipdata"][id.ToString()];
		}
	}
}
