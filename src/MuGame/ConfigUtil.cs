using Cross;
using GameFramework;
using System;
using System.Timers;

namespace MuGame
{
	public class ConfigUtil
	{
		private static Random _random;

		public static Random random
		{
			get
			{
				bool flag = ConfigUtil._random == null;
				if (flag)
				{
					ConfigUtil._random = new Random();
				}
				return ConfigUtil._random;
			}
		}

		public static Type getType(string name)
		{
			return Type.GetType(name);
		}

		public static void SetTimeout(double interval, Action action)
		{
			Timer timer = new Timer(interval);
			timer.Elapsed += delegate(object sender, ElapsedEventArgs e)
			{
				timer.Enabled = false;
				action();
			};
			timer.Enabled = true;
		}

		public static bool attchk(Variant attchk, Variant atts)
		{
			bool flag = attchk == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = atts == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					for (int i = 0; i < attchk.Count; i++)
					{
						Variant variant = attchk[i];
						bool flag3 = variant["name"]._str == "carr";
						if (flag3)
						{
							int num = 8;
							bool flag4 = false;
							int num2 = variant["and"];
							for (int j = 0; j < num; j++)
							{
								bool flag5 = (num2 >> j * 4 & 1) != 0;
								if (flag5)
								{
									int num3 = num2 >> j * 4 + 1 & 7;
									int num4 = j + 1;
									bool flag6 = atts["carr"]._int == num4 && atts.ContainsKey("carrlvl") && atts["carrlvl"]._int >= num3;
									if (flag6)
									{
										flag4 = true;
										break;
									}
								}
							}
							bool flag7 = !flag4;
							if (flag7)
							{
								result = false;
								return result;
							}
						}
						else
						{
							bool flag8 = variant.ContainsKey("equal");
							if (flag8)
							{
								bool flag9 = atts.ContainsKey(variant["name"]._str);
								if (flag9)
								{
									bool flag10 = atts[variant["name"]]._int != variant["equal"]._int;
									if (flag10)
									{
										result = false;
										return result;
									}
								}
							}
							else
							{
								bool flag11 = variant.ContainsKey("min");
								if (flag11)
								{
									bool flag12 = atts.ContainsKey(variant["name"]._str);
									if (flag12)
									{
										bool flag13 = atts[variant["name"]]._int < variant["min"]._int;
										if (flag13)
										{
											result = false;
											return result;
										}
									}
								}
								bool flag14 = variant.ContainsKey("max");
								if (flag14)
								{
									bool flag15 = atts.ContainsKey(variant["name"]._str);
									if (flag15)
									{
										bool flag16 = atts[variant["name"]]._int > variant["max"]._int;
										if (flag16)
										{
											result = false;
											return result;
										}
									}
								}
							}
						}
					}
					result = true;
				}
			}
			return result;
		}

		public static Variant get_att(Variant attchk, Variant atts)
		{
			Variant result;
			for (int i = 0; i < attchk.Count; i++)
			{
				Variant variant = attchk[i]["attchk"][0];
				bool flag = variant.ContainsKey("equal");
				if (flag)
				{
					bool flag2 = atts.ContainsKey("name") && atts["name"] == variant["name"];
					if (flag2)
					{
						bool flag3 = atts["equal"]._int == variant["equal"]._int;
						if (flag3)
						{
							result = attchk[i];
							return result;
						}
					}
				}
				else
				{
					bool flag4 = atts.ContainsKey("min") && atts["name"] == variant["name"];
					if (flag4)
					{
						bool flag5 = atts.ContainsKey(variant["name"]._str);
						if (flag5)
						{
							bool flag6 = atts["min"]._int > variant["min"]._int;
							if (flag6)
							{
								result = attchk[i];
								return result;
							}
						}
					}
					bool flag7 = variant.ContainsKey("max");
					if (flag7)
					{
						bool flag8 = atts.ContainsKey("name") && atts["name"] == variant["name"];
						if (flag8)
						{
							bool flag9 = atts["max"]._int < variant["max"]._int;
							if (flag9)
							{
								result = attchk[i];
								return result;
							}
						}
					}
				}
			}
			result = null;
			return result;
		}

		public static bool check_tm(double tm_now, Variant tmchk, double firstopentm = 0.0, double cbtm = 0.0)
		{
			bool flag = tmchk == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool isArr = tmchk.isArr;
				if (isArr)
				{
					foreach (Variant current in tmchk._arr)
					{
						bool flag2 = ConfigUtil.check_tm_impl(tm_now, current, firstopentm, cbtm);
						if (flag2)
						{
							result = true;
							return result;
						}
					}
					result = false;
				}
				else
				{
					result = ConfigUtil.check_tm_impl(tm_now, tmchk, firstopentm, cbtm);
				}
			}
			return result;
		}

		private static bool check_tm_impl(double tm_now, Variant tmchk, double firstopentm = 0.0, double cbtm = 0.0)
		{
			bool flag = cbtm > 0.0;
			bool result;
			if (flag)
			{
				bool flag2 = tmchk.ContainsKey("cb_optm");
				if (flag2)
				{
					bool flag3 = tm_now / 1000.0 - cbtm < (double)(tmchk["cb_optm"]._int * 86400);
					if (flag3)
					{
						result = false;
						return result;
					}
				}
				bool flag4 = tmchk.ContainsKey("cb_cltm");
				if (flag4)
				{
					bool flag5 = tm_now / 1000.0 - cbtm > (double)((tmchk["cb_cltm"]._int + 1) * 86400);
					if (flag5)
					{
						result = false;
						return result;
					}
				}
			}
			else
			{
				bool flag6 = tmchk.ContainsKey("cb_optm") || tmchk.ContainsKey("cb_cltm");
				if (flag6)
				{
					result = false;
					return result;
				}
			}
			bool flag7 = firstopentm > 0.0;
			if (flag7)
			{
				bool flag8 = tmchk.ContainsKey("optm");
				if (flag8)
				{
					bool flag9 = tm_now / 1000.0 - firstopentm < (double)(tmchk["optm"]._int * 86400);
					if (flag9)
					{
						result = false;
						return result;
					}
				}
				bool flag10 = tmchk.ContainsKey("cltm");
				if (flag10)
				{
					bool flag11 = tm_now / 1000.0 - firstopentm > (double)((tmchk["cltm"]._int + 1) * 86400);
					if (flag11)
					{
						result = false;
						return result;
					}
				}
			}
			bool flag12 = tmchk.ContainsKey("tb");
			if (flag12)
			{
				Variant variant = tmchk["tb"];
				TZDate tZDate = TZDate.createByYMDHMS(variant["y"], variant["mon"]._int - 1, variant["d"], variant["h"], variant["min"], variant["s"], 0);
				double time = tZDate.time;
				bool flag13 = tm_now < time;
				if (flag13)
				{
					result = false;
					return result;
				}
			}
			bool flag14 = tmchk.ContainsKey("te");
			if (flag14)
			{
				Variant variant2 = tmchk["te"];
				TZDate tZDate2 = TZDate.createByYMDHMS(variant2["y"], variant2["mon"] - 1, variant2["d"], variant2["h"], variant2["min"], variant2["s"], 0);
				double time2 = tZDate2.time;
				bool flag15 = tm_now > time2;
				if (flag15)
				{
					result = false;
					return result;
				}
			}
			bool flag16 = tmchk.ContainsKey("dtb") && tmchk.ContainsKey("dte");
			if (flag16)
			{
				TZDate tZDate3 = new TZDate(tm_now);
				int num = tZDate3.getDay();
				bool flag17 = num == 0;
				if (flag17)
				{
					num = 7;
				}
				bool flag18 = tmchk.ContainsKey("wtb") && tmchk.ContainsKey("wte");
				if (flag18)
				{
					int num2 = tmchk["wtb"];
					int num3 = tmchk["wte"] - 1;
					bool flag19 = num > num3 || num < num2;
					if (flag19)
					{
						result = false;
						return result;
					}
				}
				else
				{
					bool flag20 = tmchk.ContainsKey("wd");
					if (flag20)
					{
						string str = tmchk["wd"];
						Variant variant3 = GameTools.split(str, ",", 1u);
						bool flag21 = false;
						for (int i = 0; i < variant3.Count; i++)
						{
							int num4 = variant3[i];
							bool flag22 = num4 == num;
							if (flag22)
							{
								flag21 = true;
								break;
							}
						}
						bool flag23 = !flag21;
						if (flag23)
						{
							result = false;
							return result;
						}
					}
				}
				TZDate tZDate4 = new TZDate(tm_now);
				TZDate tZDate5 = new TZDate(tm_now);
				Variant variant4 = tmchk["dtb"];
				Variant variant5 = tmchk["dte"];
				tZDate4.setHours(variant4["h"], variant4["min"], variant4["s"], 0);
				tZDate5.setHours(variant5["h"], variant5["min"], variant5["s"], 0);
				double time = tZDate4.time;
				double time2 = tZDate5.time;
				bool flag24 = tm_now < time || tm_now > time2;
				if (flag24)
				{
					result = false;
					return result;
				}
			}
			result = true;
			return result;
		}

		public static double next_start_tm(double tm_now, Variant tmchk, double firstopentm)
		{
			bool flag = ConfigUtil.check_tm(tm_now, tmchk, firstopentm, 0.0);
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				bool isArr = tmchk.isArr;
				if (isArr)
				{
					double num = 0.0;
					foreach (Variant current in tmchk._arr)
					{
						double num2 = ConfigUtil.next_start(tm_now, current, firstopentm);
						bool flag2 = num2 > 0.0;
						if (flag2)
						{
							bool flag3 = 0.0 == num;
							if (flag3)
							{
								num = num2;
							}
							else
							{
								bool flag4 = num > num2;
								if (flag4)
								{
									num = num2;
								}
							}
						}
					}
					result = num;
				}
				else
				{
					result = ConfigUtil.next_start(tm_now, tmchk, firstopentm);
				}
			}
			return result;
		}

		private static double next_start(double tm_now, Variant tmchk, double firstopentm = 0.0)
		{
			bool flag = false;
			bool flag2 = firstopentm > 0.0 && tmchk.ContainsKey("optm");
			if (flag2)
			{
				bool flag3 = tm_now / 1000.0 - firstopentm < (double)(tmchk["optm"]._int * 86400);
				if (flag3)
				{
					tm_now = (double)(tmchk["optm"]._int * 86400 * 1000) + firstopentm * 1000.0;
					flag = true;
				}
			}
			bool flag4 = tmchk.ContainsKey("tb") && tmchk.ContainsKey("te");
			double result;
			if (flag4)
			{
				Variant variant = tmchk["tb"];
				Variant variant2 = tmchk["te"];
				TZDate tZDate = TZDate.createByYMDHMS(variant["y"], variant["mon"]._int - 1, variant["d"], variant["h"], variant["min"], variant["s"], 0);
				double time = tZDate.time;
				TZDate tZDate2 = TZDate.createByYMDHMS(variant2["y"], variant2["mon"] - 1, variant2["d"], variant2["h"], variant2["min"], variant2["s"], 0);
				double time2 = tZDate2.time;
				bool flag5 = time < tm_now;
				if (flag5)
				{
					result = 0.0;
				}
				else
				{
					result = time;
				}
			}
			else
			{
				bool flag6 = tmchk.ContainsKey("dtb") && tmchk.ContainsKey("dte");
				if (flag6)
				{
					bool flag7 = false;
					TZDate tZDate3 = new TZDate(tm_now);
					int num = tZDate3.getDay();
					bool flag8 = num == 0;
					if (flag8)
					{
						num = 7;
					}
					Variant variant3 = tmchk["dtb"];
					Variant variant4 = tmchk["dte"];
					int num2 = 0;
					int num3 = 0;
					bool flag9 = tmchk.ContainsKey("wtb") && tmchk.ContainsKey("wte");
					if (flag9)
					{
						num2 = tmchk["wtb"];
						num3 = tmchk["wte"] - 1;
						flag7 = true;
					}
					else
					{
						bool flag10 = tmchk.ContainsKey("wd");
						if (flag10)
						{
							string str = tmchk["wd"];
							Variant variant5 = GameTools.split(str, ",", 1u);
						}
					}
					TZDate tZDate4 = new TZDate(tm_now);
					TZDate tZDate5 = new TZDate(tm_now);
					tZDate4.setHours(variant3["h"], variant3["min"], variant3["s"], 0);
					tZDate5.setHours(variant4["h"], variant4["min"], variant4["s"], 0);
					double time = tZDate4.time;
					double time2 = tZDate5.time;
					bool flag11 = flag7 && (num > num3 || num < num2);
					bool flag12;
					if (flag11)
					{
						flag12 = true;
					}
					else
					{
						bool flag13 = tmchk.ContainsKey("wd");
						if (flag13)
						{
							string str = tmchk["wd"];
							Variant variant5 = GameTools.split(str, ",", 1u);
							flag12 = true;
							for (int i = 0; i < variant5.Count; i++)
							{
								int num4 = variant5[i];
								bool flag14 = num4 == num;
								if (flag14)
								{
									bool flag15 = tm_now > time && tm_now < time2;
									if (flag15)
									{
										flag12 = false;
									}
									break;
								}
							}
						}
						else
						{
							bool flag16 = tm_now < time;
							flag12 = flag16;
						}
					}
					bool flag17 = flag12 | flag;
					if (flag17)
					{
						bool flag18 = tmchk.ContainsKey("wd");
						if (flag18)
						{
							string str = tmchk["wd"];
							Variant variant5 = GameTools.split(str, ",", 1u);
							int num5 = -1;
							int num6 = -1;
							for (int j = 0; j < variant5.Count; j++)
							{
								int num7 = variant5[j];
								bool flag19 = num6 < 0 || num7 < num6;
								if (flag19)
								{
									num6 = num7;
								}
								bool flag20 = num > num7 || (num == num7 && tm_now > time2);
								if (!flag20)
								{
									bool flag21 = num5 < 0 || num7 < num5;
									if (flag21)
									{
										num5 = num7;
									}
								}
							}
							bool flag22 = num5 < 0;
							if (flag22)
							{
								result = time + (double)(86400000 * (7 - num + num6));
							}
							else
							{
								result = time + (double)(86400000 * (num5 - num));
							}
						}
						else
						{
							bool flag23 = !flag7;
							if (flag23)
							{
								result = time;
							}
							else
							{
								bool flag24 = num < num2 || num > num3;
								if (flag24)
								{
									int num8 = num2 - num;
									bool flag25 = num8 < 0;
									if (flag25)
									{
										num8 += 7;
									}
									result = time + (double)(86400000 * num8);
								}
								else
								{
									result = time;
								}
							}
						}
					}
					else
					{
						bool flag26 = tmchk.ContainsKey("wd");
						if (flag26)
						{
							result = 0.0;
						}
						else
						{
							bool flag27 = !flag7;
							if (flag27)
							{
								result = time + 86400000.0;
							}
							else
							{
								num++;
								bool flag28 = num > 6;
								if (flag28)
								{
									num = 0;
								}
								bool flag29 = num < num2 || num > num3;
								if (flag29)
								{
									int num8 = num2 - num;
									bool flag30 = num8 < 0;
									if (flag30)
									{
										num8 += 7;
									}
									result = time + (double)(86400000 * num8);
								}
								else
								{
									result = time + 86400000.0;
								}
							}
						}
					}
				}
				else
				{
					result = 0.0;
				}
			}
			return result;
		}

		public static double next_end_tm(double tm_now, Variant tmchk, double firstracttmt = 0.0, double combracttm = 0.0)
		{
			bool flag = !ConfigUtil.check_tm(tm_now, tmchk, firstracttmt, combracttm);
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				double num = 0.0;
				bool flag2 = tmchk.ContainsKey("tb") && tmchk.ContainsKey("te");
				if (flag2)
				{
					Variant variant = tmchk["te"];
					TZDate tZDate = TZDate.createByYMDHMS(variant["y"], variant["mon"] - 1, variant["d"], variant["h"], variant["min"], variant["s"], 0);
					num = tZDate.time;
				}
				else
				{
					bool flag3 = tmchk.ContainsKey("dtb") && tmchk.ContainsKey("dte");
					if (flag3)
					{
						Variant variant2 = tmchk["dte"];
						TZDate tZDate2 = TZDate.createByYMDHMS((int)tm_now, 0, 0, 0, 0, 0, 0);
						tZDate2.setHours(variant2["h"], variant2["min"], variant2["s"], 0);
						num = tZDate2.time;
					}
				}
				result = num;
			}
			return result;
		}

		public static Variant GetTodayActiveTime(double tm_now, Variant tmchk, double firstracttmt, double combracttm)
		{
			Variant variant = new Variant();
			variant["begin"] = 0;
			variant["end"] = 0;
			bool flag = true;
			bool flag2 = tmchk.ContainsKey("tb") && tmchk.ContainsKey("te");
			Variant result;
			if (flag2)
			{
				Variant variant2 = tmchk["tb"];
				Variant variant3 = tmchk["te"];
				TZDate tZDate = TZDate.createByYMDHMS(variant2["y"], variant2["mon"] - 1, variant2["d"], variant2["h"], variant2["min"], variant2["s"], 0);
				variant["begin"] = tZDate.time;
				tZDate.setHours(0, 0, 0, 0);
				bool flag3 = tm_now < tZDate.time;
				if (flag3)
				{
					variant["begin"] = 0;
				}
				tZDate = TZDate.createByYMDHMS(variant3["y"], variant3["mon"] - 1, variant3["d"], variant3["h"], variant3["min"], variant3["s"], 0);
				variant["end"] = tZDate.time;
				tZDate.setHours(23, 59, 59, 0);
				bool flag4 = tm_now >= tZDate.time;
				if (flag4)
				{
					variant["begin"] = 0;
				}
				result = variant;
			}
			else
			{
				bool flag5 = tmchk.ContainsKey("dtb") && tmchk.ContainsKey("dte");
				if (flag5)
				{
					TZDate tZDate = new TZDate(tm_now);
					int num = tZDate.getDay();
					bool flag6 = num == 0;
					if (flag6)
					{
						num = 7;
					}
					bool flag7 = tmchk.ContainsKey("wtb") && tmchk.ContainsKey("wte");
					if (flag7)
					{
						flag = (num >= tmchk["wtb"]._int && num <= tmchk["wte"]._int - 1);
					}
					else
					{
						bool flag8 = tmchk.ContainsKey("wd");
						if (flag8)
						{
							flag = false;
							Variant variant4 = GameTools.split(tmchk["wd"]._str, ",", 1u);
							for (int i = 0; i < variant4.Count; i++)
							{
								bool flag9 = variant4[i]._int == num;
								if (flag9)
								{
									flag = true;
									break;
								}
							}
						}
					}
					bool flag10 = flag;
					if (flag10)
					{
						bool flag11 = tmchk.ContainsKey("optm");
						if (flag11)
						{
							bool flag12 = tm_now / 1000.0 - firstracttmt < (double)(tmchk["optm"]._int * 86400);
							if (flag12)
							{
								flag = false;
							}
						}
						bool flag13 = tmchk.ContainsKey("cltm");
						if (flag13)
						{
							bool flag14 = tm_now / 1000.0 - firstracttmt > (double)((tmchk["cltm"]._int + 1) * 86400);
							if (flag14)
							{
								flag = false;
							}
						}
						bool flag15 = tmchk.ContainsKey("cb_optm");
						if (flag15)
						{
							bool flag16 = tm_now / 1000.0 - combracttm < (double)(tmchk["cb_optm"]._int * 86400);
							if (flag16)
							{
								flag = false;
							}
						}
						bool flag17 = tmchk.ContainsKey("cb_cltm");
						if (flag17)
						{
							bool flag18 = tm_now / 1000.0 - combracttm > (double)((tmchk["cb_cltm"]._int + 1) * 86400);
							if (flag18)
							{
								flag = false;
							}
						}
					}
					bool flag19 = flag;
					if (flag19)
					{
						Variant variant5 = tmchk["dtb"];
						Variant variant6 = tmchk["dte"];
						tZDate.setHours(variant5["h"], variant5["min"], variant5["s"], 0);
						variant["begin"] = tZDate.time;
						tZDate.setHours(variant6["h"], variant6["min"], variant6["s"], 0);
						variant["end"] = tZDate.time;
					}
				}
				else
				{
					bool flag20 = (tmchk.ContainsKey("optm") && tmchk.ContainsKey("cltm")) || (tmchk.ContainsKey("cb_optm") && tmchk.ContainsKey("cb_cltm"));
					if (flag20)
					{
						bool flag21 = tmchk.ContainsKey("optm");
						if (flag21)
						{
							bool flag22 = tm_now / 1000.0 - firstracttmt >= (double)(tmchk["optm"]._int * 86400) && tm_now / 1000.0 - firstracttmt < (double)((tmchk["cltm"]._int + 1) * 86400);
							if (flag22)
							{
								variant["begin"] = firstracttmt + (double)(tmchk["optm"]._int * 86400);
								variant["end"] = firstracttmt + (double)((tmchk["cltm"]._int + 1) * 86400);
							}
						}
						else
						{
							bool flag23 = tmchk.ContainsKey("cb_optm");
							if (flag23)
							{
								bool flag24 = tm_now / 1000.0 - combracttm >= (double)(tmchk["cb_optm"]._int * 86400) && tm_now / 1000.0 - combracttm < (double)((tmchk["cb_cltm"]._int + 1) * 86400);
								if (flag24)
								{
									variant["begin"] = firstracttmt + (double)(tmchk["cb_optm"]._int * 86400);
									variant["end"] = firstracttmt + (double)((tmchk["cb_cltm"]._int + 1) * 86400);
								}
							}
						}
					}
				}
				result = variant;
			}
			return result;
		}

		public static double GetRadianByOctori(int ori)
		{
			double num = 0.78539816339744828;
			switch (ori)
			{
			case 0:
				num *= 4.0;
				break;
			case 1:
				num *= 5.0;
				break;
			case 2:
				num *= 6.0;
				break;
			case 3:
				num *= 7.0;
				break;
			case 4:
				num = 0.0;
				break;
			case 5:
				num *= 1.0;
				break;
			case 6:
				num *= 2.0;
				break;
			case 7:
				num *= 3.0;
				break;
			}
			return num - 0.0;
		}

		public static Variant GetTmchkAbs(string date)
		{
			bool flag = "" == date || date == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = GameTools.split(date, " ", 1u);
				Variant variant2 = GameTools.split(variant[0], "-", 1u);
				Variant variant3 = GameTools.split(variant[1], ":", 1u);
				Variant variant4 = new Variant();
				variant4["y"] = variant2[0];
				variant4["mon"] = variant2[1];
				variant4["d"] = variant2[2];
				variant4["h"] = variant3[0];
				variant4["min"] = variant3[1];
				variant4["s"] = variant3[2];
				result = variant4;
			}
			return result;
		}

		public static int getRandom(int sInt, int eInt)
		{
			bool flag = sInt > eInt || eInt < 0;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				result = (int)(ConfigUtil.random.NextDouble() * (double)(eInt - sInt + 1)) + sInt;
			}
			return result;
		}

		public static string get_time(int sec)
		{
			int num = sec / 3600;
			int num2 = (sec - num * 3600) / 60;
			int num3 = sec - num * 3600 - num2 * 60;
			string str = "";
			bool flag = num < 10;
			if (flag)
			{
				str += "0";
			}
			str += num.ToString();
			str += ":";
			bool flag2 = num2 < 10;
			if (flag2)
			{
				str += "0";
			}
			str += num2.ToString();
			str += ":";
			bool flag3 = num3 < 10;
			if (flag3)
			{
				str += "0";
			}
			return str + num3.ToString();
		}

		public static bool check_crttm(float tm_now, Variant crttmchk, Variant self)
		{
			bool flag = crttmchk == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool isArr = crttmchk.isArr;
				if (isArr)
				{
					foreach (Variant current in crttmchk._arr)
					{
						bool flag2 = ConfigUtil.check_crttm_impl(tm_now, current, self);
						if (flag2)
						{
							result = true;
							return result;
						}
					}
					result = false;
				}
				else
				{
					result = ConfigUtil.check_crttm_impl(tm_now, crttmchk, self);
				}
			}
			return result;
		}

		public static bool check_crttm_impl(float tm_now, Variant crttmchk, Variant self)
		{
			bool flag = crttmchk.ContainsKey("optm");
			bool result;
			if (flag)
			{
				bool flag2 = tm_now / 1000f - (float)self["crttm"]._int < (float)(crttmchk["optm"]._int * 86400);
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			bool flag3 = crttmchk.ContainsKey("cltm");
			if (flag3)
			{
				bool flag4 = tm_now / 1000f - (float)self["crttm"]._int > (float)((crttmchk["cltm"]._int + 1) * 86400);
				if (flag4)
				{
					result = false;
					return result;
				}
			}
			result = true;
			return result;
		}

		public static Variant get_tmchk_time(double tm_now, Variant tmchk, double firstopentm = 0.0, double cbtm = 0.0)
		{
			double val = 0.0;
			double val2 = 0.0;
			bool flag = firstopentm > 0.0;
			if (flag)
			{
				bool flag2 = tmchk.ContainsKey("optm");
				if (flag2)
				{
					val = (firstopentm + (double)(tmchk["optm"]._int * 86400)) * 1000.0;
				}
				bool flag3 = tmchk.ContainsKey("cltm");
				if (flag3)
				{
					val2 = (firstopentm + (double)((tmchk["cltm"]._int + 1) * 86400)) * 1000.0;
				}
			}
			else
			{
				bool flag4 = cbtm > 0.0;
				if (flag4)
				{
					bool flag5 = tmchk.ContainsKey("cb_optm");
					if (flag5)
					{
						val = (cbtm + (double)(tmchk["cb_optm"]._int * 86400)) * 1000.0;
					}
					bool flag6 = tmchk.ContainsKey("cb_cltm");
					if (flag6)
					{
						val2 = (cbtm + (double)((tmchk["cb_cltm"]._int + 1) * 86400)) * 1000.0;
					}
				}
			}
			bool flag7 = tmchk.ContainsKey("tb");
			if (flag7)
			{
				Variant variant = tmchk["tb"];
				TZDate tZDate = TZDate.createByYMDHMS(variant["y"], variant["mon"]._int - 1, variant["d"], variant["h"], variant["min"], variant["s"], 0);
				val = tZDate.time;
			}
			bool flag8 = tmchk.ContainsKey("te");
			if (flag8)
			{
				Variant variant2 = tmchk["te"];
				TZDate tZDate2 = TZDate.createByYMDHMS(variant2["y"], variant2["mon"]._int - 1, variant2["d"], variant2["h"], variant2["min"], variant2["s"], 0);
				val2 = tZDate2.time;
			}
			bool flag9 = tmchk.ContainsKey("dtb") && tmchk.ContainsKey("dte");
			if (flag9)
			{
				TZDate tZDate3 = new TZDate(tm_now);
				TZDate tZDate4 = new TZDate(tm_now);
				Variant variant3 = tmchk["dtb"];
				Variant variant4 = tmchk["dte"];
				tZDate3.setHours(variant3["h"], variant3["min"], variant3["s"], 0);
				tZDate4.setHours(variant4["h"], variant4["min"], variant4["s"], 0);
				val = tZDate3.time;
				val2 = tZDate4.time;
			}
			Variant variant5 = new Variant();
			variant5["stm"] = val;
			variant5["etm"] = val2;
			return variant5;
		}

		public static bool check_lott_tm(float now_tm, int usetp, Variant lottm = null, Variant costLottm = null)
		{
			bool flag = usetp == 2 && lottm != null;
			bool result;
			if (flag)
			{
				bool flag2 = now_tm > (float)(lottm["sttm"]._int * 1000) && now_tm < (float)(lottm["edtm"]._int * 1000);
				result = flag2;
			}
			else
			{
				bool flag3 = usetp == 3 && costLottm != null;
				if (flag3)
				{
					bool flag4 = now_tm > (float)(costLottm["sttm"]._int * 1000) && now_tm < (float)(costLottm["edtm"]._int * 1000);
					result = flag4;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}
	}
}
