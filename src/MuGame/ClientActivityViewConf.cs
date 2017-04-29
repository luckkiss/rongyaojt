using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class ClientActivityViewConf : configParser
	{
		public ClientActivityViewConf(ClientConfig m) : base(m)
		{
		}

		public static ClientActivityViewConf create(IClientBase m)
		{
			return new ClientActivityViewConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("activitys");
			if (flag)
			{
				Variant variant = conf["activitys"][0];
				bool flag2 = variant != null;
				if (flag2)
				{
					List<Variant> arr = variant["a"]._arr;
					Variant variant2 = new Variant();
					for (int i = 0; i < arr.Count; i++)
					{
						Variant variant3 = arr[i];
						Variant variant4 = this.transTmchks(variant3["tmchk"]);
						bool flag3 = variant4 != null;
						if (flag3)
						{
							variant3["tmchk"] = variant4;
						}
						variant2[variant3["id"]] = variant3;
						bool flag4 = variant3.ContainsKey("allday");
						if (flag4)
						{
							List<Variant> arr2 = variant3["allday"]._arr;
							bool flag5 = arr2.Count > 0;
							if (flag5)
							{
								variant3["allday"] = arr2[0]["value"];
							}
						}
					}
					conf["activitys"] = variant2;
				}
			}
			bool flag6 = conf.ContainsKey("days");
			if (flag6)
			{
				Variant variant5 = conf["days"][0];
				bool flag7 = variant5 != null;
				if (flag7)
				{
					List<Variant> arr = variant5["t"]._arr;
					Variant variant6 = new Variant();
					for (int j = 0; j < arr.Count; j++)
					{
						Variant variant7 = arr[j];
						Variant variant8 = GameTools.split(variant7["content"], ",", 1u);
						Variant variant9 = new Variant();
						for (int k = 0; k < variant8.Count; k++)
						{
							Variant variant10 = conf["activitys"][variant8[k]];
							bool flag8 = variant10 != null;
							if (flag8)
							{
								variant9._arr.Add(variant10);
							}
						}
						variant6[variant7["day"]] = variant9;
					}
					conf["days"] = variant6;
				}
			}
			bool flag9 = conf.ContainsKey("special");
			if (flag9)
			{
				Variant variant11 = conf["special"][0];
				bool flag10 = variant11 != null;
				if (flag10)
				{
					List<Variant> arr = variant11["broad"]._arr;
					Variant variant12 = new Variant();
					for (int l = 0; l < arr.Count; l++)
					{
						Variant variant13 = arr[l];
						variant13["tmchk"] = this.transTmchks(variant13["tmchk"]);
						variant12[variant13["bid"]] = variant13;
					}
					conf["special"] = variant12;
				}
			}
			bool flag11 = conf.ContainsKey("specialdays");
			if (flag11)
			{
				Variant variant14 = conf["specialdays"][0];
				bool flag12 = variant14 != null;
				if (flag12)
				{
					List<Variant> arr = variant14["t"]._arr;
					Variant variant15 = new Variant();
					for (int m = 0; m < arr.Count; m++)
					{
						Variant variant16 = arr[m];
						Variant variant8 = GameTools.split(variant16["content"], ",", 1u);
						Variant variant17 = new Variant();
						for (int k = 0; k < variant8.Count; k++)
						{
							Variant variant10 = conf["special"][variant8[k]];
							bool flag13 = variant10 != null;
							if (flag13)
							{
								variant17._arr.Add(variant10);
							}
						}
						variant15[variant16["day"]] = variant17;
					}
					conf["specialdays"] = variant15;
				}
			}
			return conf;
		}

		private Variant transTmchks(Variant tmchks)
		{
			Variant variant = new Variant();
			bool flag = tmchks != null;
			if (flag)
			{
				foreach (Variant current in tmchks._arr)
				{
					Variant variant2 = new Variant();
					bool flag2 = current.ContainsKey("dtb");
					if (flag2)
					{
						string str = current["dtb"]._str;
						Variant variant3 = new Variant();
						Variant variant4 = GameTools.split(str, ":", 1u);
						variant3["h"] = variant4[0];
						variant3["min"] = variant4[1];
						variant3["s"] = variant4[2];
						variant2["dtb"] = variant3;
					}
					bool flag3 = current.ContainsKey("dte");
					if (flag3)
					{
						string str2 = current["dte"]._str;
						Variant variant5 = GameTools.split(str2, ":", 1u);
						Variant variant6 = new Variant();
						variant6["h"] = variant5[0];
						variant6["min"] = variant5[1];
						variant6["s"] = variant5[2];
						variant2["dte"] = variant6;
					}
					bool flag4 = current.ContainsKey("optm");
					if (flag4)
					{
						variant2["optm"] = current["optm"];
					}
					bool flag5 = current.ContainsKey("cltm");
					if (flag5)
					{
						variant2["cltm"] = current["cltm"];
					}
					bool flag6 = current.ContainsKey("cb_optm");
					if (flag6)
					{
						variant2["cb_optm"] = current["cb_optm"];
					}
					bool flag7 = current.ContainsKey("cb_cltm");
					if (flag7)
					{
						variant2["cb_cltm"] = current["cb_cltm"];
					}
					bool flag8 = current.ContainsKey("tb");
					if (flag8)
					{
						variant2["tb"] = ConfigUtil.GetTmchkAbs(current["tb"]);
					}
					bool flag9 = current.ContainsKey("te");
					if (flag9)
					{
						variant2["te"] = ConfigUtil.GetTmchkAbs(current["te"]);
					}
					bool flag10 = current.ContainsKey("type");
					if (flag10)
					{
						variant2["type"] = current["type"];
					}
					bool flag11 = variant == null;
					if (flag11)
					{
						variant = new Variant();
					}
					variant._arr.Add(variant2);
				}
			}
			return variant;
		}

		public Variant GetActInfoById(uint id)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this.m_conf.ContainsKey("activitys");
				if (flag2)
				{
					result = this.m_conf["activitys"][id.ToString()];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public Variant get_day_data(int day)
		{
			bool flag = this.m_conf != null && this.m_conf.ContainsKey("days");
			Variant result;
			if (flag)
			{
				bool flag2 = day == 0;
				if (flag2)
				{
					day = 7;
				}
				Variant ori_arr = this.m_conf["days"][day.ToString()];
				Variant variant = new Variant();
				variant = this.get_all_act_arr(ori_arr);
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_specialday_data(int day)
		{
			bool flag = this.m_conf != null && this.m_conf.ContainsKey("specialdays");
			Variant result;
			if (flag)
			{
				bool flag2 = day == 0;
				if (flag2)
				{
					day = 7;
				}
				Variant variant = this.m_conf["specialdays"][day.ToString()];
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		protected Variant get_all_act_arr(Variant ori_arr)
		{
			Variant variant = new Variant();
			for (int i = 0; i < ori_arr.Count; i++)
			{
				Variant variant2 = ori_arr[i];
				bool flag = variant2.ContainsKey("allday") && variant2["allday"] > 0;
				if (flag)
				{
					Variant v = variant2.clone();
					variant.pushBack(v);
				}
				else
				{
					Variant variant3 = variant2["tmchk"];
					bool flag2 = variant3 != null;
					if (flag2)
					{
						for (int j = 0; j < variant3.Count; j++)
						{
							Variant variant4 = variant2.clone();
							Variant variant5 = variant3[j].clone();
							Variant variant6 = new Variant();
							Variant variant7 = new Variant();
							bool flag3 = variant5.ContainsKey("optm") && variant5["optm"] > 0;
							if (flag3)
							{
								variant7["optm"] = variant5["optm"];
							}
							bool flag4 = variant5.ContainsKey("cltm") && variant5["cltm"] > 0;
							if (flag4)
							{
								variant7["cltm"] = variant5["cltm"];
							}
							bool flag5 = variant5.ContainsKey("cb_optm") && variant5["cb_optm"] > 0;
							if (flag5)
							{
								variant7["cb_optm"] = variant5["cb_optm"];
							}
							bool flag6 = variant5.ContainsKey("cb_cltm") && variant5["cb_cltm"] > 0;
							if (flag6)
							{
								variant7["cb_cltm"] = variant5["cb_cltm"];
							}
							bool flag7 = variant5.ContainsKey("type") && variant5["type"] > 0;
							if (flag7)
							{
								variant7["type"] = variant5["type"];
							}
							variant7["dtb"] = variant5["dtb"];
							variant7["dte"] = variant5["dte"];
							bool flag8 = variant5.ContainsKey("tb");
							if (flag8)
							{
								variant7["tb"] = variant5["tb"];
								variant7["dtb"] = variant5["tb"];
							}
							bool flag9 = variant5.ContainsKey("te");
							if (flag9)
							{
								variant7["te"] = variant5["te"];
								variant7["dte"] = variant5["te"];
							}
							variant6._arr.Add(variant7);
							variant4["act_tmchk"] = variant6;
							variant._arr.Add(variant4);
						}
					}
				}
			}
			return variant;
		}
	}
}
