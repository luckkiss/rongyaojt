using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class SvrMeriConfig : configParser
	{
		private Variant meri_arr1 = new Variant();

		private Variant meri_arr2 = new Variant();

		private Variant meri_arr3 = new Variant();

		private Variant meri_arr4 = new Variant();

		private Variant meri_arr5 = new Variant();

		public SvrMeriConfig(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrMeriConfig(m as ClientConfig);
		}

		private Variant get_carr_meri_arr(int carr)
		{
			bool flag = carr == 1;
			Variant result;
			if (flag)
			{
				result = this.meri_arr1;
			}
			else
			{
				bool flag2 = carr == 2;
				if (flag2)
				{
					result = this.meri_arr2;
				}
				else
				{
					bool flag3 = carr == 3;
					if (flag3)
					{
						result = this.meri_arr3;
					}
					else
					{
						bool flag4 = carr == 4;
						if (flag4)
						{
							result = this.meri_arr4;
						}
						else
						{
							bool flag5 = carr == 5;
							if (flag5)
							{
								result = this.meri_arr5;
							}
							else
							{
								result = null;
							}
						}
					}
				}
			}
			return result;
		}

		public Variant get_meri_data_carr(int carr)
		{
			Variant variant = this.get_carr_meri_arr(carr);
			bool flag = variant.Count == 0;
			Variant result;
			if (flag)
			{
				Variant variant2 = this.get_carr_meri(carr);
				Variant meri_data = this.get_meri_data();
				Variant variant3 = new Variant();
				variant3 = meri_data.clone();
				variant = variant3;
				bool flag2 = carr == 1;
				if (flag2)
				{
					this.meri_arr1 = variant;
				}
				else
				{
					bool flag3 = carr == 2;
					if (flag3)
					{
						this.meri_arr2 = variant;
					}
					else
					{
						bool flag4 = carr == 3;
						if (flag4)
						{
							this.meri_arr3 = variant;
						}
						else
						{
							bool flag5 = carr == 4;
							if (flag5)
							{
								this.meri_arr4 = variant;
							}
							else
							{
								bool flag6 = carr == 5;
								if (flag6)
								{
									this.meri_arr5 = variant;
								}
							}
						}
					}
				}
				bool flag7 = variant2 == null || variant2["meri"] == null;
				if (flag7)
				{
					result = variant;
					return result;
				}
				using (List<Variant>.Enumerator enumerator = variant2["meri"]._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						bool flag8 = variant2["meri"][key] == null || variant2["meri"][key]["acup"] == null;
						if (!flag8)
						{
							Variant variant4 = variant2["meri"][key]["acup"];
							Variant variant5 = variant[key]["acup"];
							using (List<Variant>.Enumerator enumerator2 = variant4._arr.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string key2 = enumerator2.Current;
									variant5[key2] = variant4[key2];
								}
							}
						}
					}
				}
			}
			result = variant;
			return result;
		}

		public Variant Clone()
		{
			return null;
		}

		public Variant get_meri_data()
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this.m_conf["meri"] == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = this.m_conf["meri"];
				}
			}
			return result;
		}

		public Variant get_meri_carr_data_by_id(int merid, int carr)
		{
			Variant variant = this.get_meri_data_carr(carr);
			Variant result;
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string key = enumerator.Current;
					Variant variant2 = variant[key];
					bool flag = variant2["id"] == merid;
					if (flag)
					{
						result = variant2;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant get_meri_cdcnt(int cnt)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this.m_conf["cdcnt"] == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Variant variant = this.m_conf["cdcnt"];
					using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string key = enumerator.Current;
							Variant variant2 = variant[key];
							bool flag3 = variant2["cnt"] == cnt;
							if (flag3)
							{
								result = variant2;
								return result;
							}
						}
					}
					result = null;
				}
			}
			return result;
		}

		public int get_meri_cdcnt_cnt()
		{
			bool flag = this.m_conf == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = this.m_conf["cdcnt"] == null;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					Variant variant = this.m_conf["cdcnt"];
					int num = 0;
					using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							num++;
						}
					}
					result = num;
				}
			}
			return result;
		}

		public Variant get_meri_data_by_id(int merid)
		{
			Variant meri_data = this.get_meri_data();
			Variant variant = new Variant();
			Variant result;
			using (List<Variant>.Enumerator enumerator = meri_data._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string key = enumerator.Current;
					variant = meri_data[key];
					bool flag = variant["id"] == merid;
					if (flag)
					{
						result = variant;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant get_carr_meri(int carr)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = this.m_conf["carr"];
				result = variant[carr];
			}
			return result;
		}

		public Variant get_acup_lvl(int lvl)
		{
			bool flag = this.m_conf["acup_lvl"] == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = this.m_conf["acup_lvl"];
				bool flag2 = lvl >= variant.Count;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = variant[lvl];
				}
			}
			return result;
		}

		public int get_stage_apt(int stage)
		{
			int num = 1;
			int num2 = 0;
			bool flag2;
			do
			{
				Variant variant = this.get_acup_lvl(++num);
				bool flag = variant == null;
				if (flag)
				{
					break;
				}
				num2 += variant["att_per_min"];
				flag2 = (num2 / 100 > stage);
			}
			while (!flag2);
			return num2;
		}

		public int get_total_apt_by_level(int level)
		{
			int num = 0;
			bool flag = this.m_conf["acup_lvl"] != null;
			if (flag)
			{
				Variant variant = this.m_conf["acup_lvl"];
				bool flag2 = level > variant.Count;
				if (flag2)
				{
					level = variant.Count;
				}
				for (int i = 1; i < level; i++)
				{
					Variant variant2 = variant[i];
					num += variant2["att_per_min"]._int;
				}
			}
			return num;
		}

		public int get_add_apt_by_level(int level)
		{
			Variant variant = this.get_acup_lvl(level);
			bool flag = variant == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = variant["att_per_min"];
			}
			return result;
		}

		public int get_acup_level_by_apt(int apt)
		{
			int num = 1;
			while (apt > 0)
			{
				Variant variant = this.get_acup_lvl(num);
				bool flag = variant == null;
				if (flag)
				{
					break;
				}
				num++;
				apt -= variant["att_per_min"];
			}
			return num;
		}

		public int max_acup_pt()
		{
			bool flag = this.m_conf["acup_lvl"] == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num = 0;
				Variant variant = this.m_conf["acup_lvl"];
				for (int i = 1; i < variant["length"]; i++)
				{
					num += variant[i]["att_per_min"];
				}
				result = num;
			}
			return result;
		}

		public int max_acup_lvl()
		{
			bool flag = this.m_conf["acup_lvl"] == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				Variant variant = this.m_conf["acup_lvl"];
				Variant variant2 = variant[variant.Count - 1];
				bool flag2 = variant2 == null;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					result = variant2["lvl"];
				}
			}
			return result;
		}

		public Variant get_acup_by_meri(int merid, int aid)
		{
			Variant variant = this.get_meri_data_by_id(merid);
			bool flag = variant == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant2 = variant["acup"];
				Variant variant3 = null;
				using (List<Variant>.Enumerator enumerator = variant2._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						variant3 = variant2[key];
						bool flag2 = variant3["aid"] == aid;
						if (flag2)
						{
							result = variant3;
							return result;
						}
					}
				}
				result = variant3;
			}
			return result;
		}

		public Variant get_carr_acup_by_meri(int merid, int aid, int carr)
		{
			Variant variant = this.get_meri_carr_data_by_id(merid, carr);
			bool flag = variant == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant2 = variant["acup"];
				bool flag2 = aid >= variant2.Count;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Variant variant3 = null;
					using (List<Variant>.Enumerator enumerator = variant2._arr.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string key = enumerator.Current;
							variant3 = variant2[key];
							bool flag3 = variant3["aid"] == aid;
							if (flag3)
							{
								result = variant3;
								return result;
							}
						}
					}
					result = variant3;
				}
			}
			return result;
		}
	}
}
