using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class SvrMarketConfig : configParser
	{
		protected Variant m_item = new Variant();

		private Variant _sellItem = new Variant();

		public SvrMarketConfig(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrMarketConfig(m as ClientConfig);
		}

		protected override void onData()
		{
			this.m_item = this.m_conf.clone();
			bool flag = this.m_conf["hot"] != null;
			if (flag)
			{
				Variant variant = this.m_conf["hot"];
				this.m_conf.RemoveKey("hot");
				this.m_conf["hot"] = new Variant();
				for (int i = 0; i < variant.Count; i++)
				{
					this.m_conf["hot"][variant[i]["item_id"]._str] = variant[i];
				}
			}
			bool flag2 = this.m_conf["sell"] != null;
			if (flag2)
			{
				Variant variant2 = this.m_conf["sell"];
				this.m_conf.RemoveKey("sell");
				this.m_conf["sell"] = new Variant();
				for (int j = 0; j < variant2.Count; j++)
				{
					this.m_conf["sell"][variant2[j]["item_id"]._str] = variant2[j];
				}
			}
			bool flag3 = this.m_conf["hsell"] != null;
			if (flag3)
			{
				Variant variant3 = this.m_conf["hsell"];
				this.m_conf.RemoveKey("hsell");
				this.m_conf["hsell"] = new Variant();
				for (int k = 0; k < variant3.Count; k++)
				{
					this.m_conf["hsell"][variant3[k]["item_id"]._str] = variant3[k];
				}
			}
			bool flag4 = this.m_conf["bndsell"] != null;
			if (flag4)
			{
				Variant variant4 = this.m_conf["bndsell"];
				this.m_conf.RemoveKey("bndsell");
				this.m_conf["bndsell"] = new Variant();
				for (int l = 0; l < variant4.Count; l++)
				{
					this.m_conf["bndsell"][variant4[l]["item_id"]._str] = variant4[l];
				}
			}
			bool flag5 = this.m_conf["shoppt"] != null;
			if (flag5)
			{
				Variant variant5 = this.m_conf["shoppt"];
				this.m_conf.RemoveKey("shoppt");
				this.m_conf["shoppt"] = new Variant();
				for (int m = 0; m < variant5.Count; m++)
				{
					this.m_conf["hot"][variant5[m]["item_id"]._str] = variant5[m];
				}
			}
			bool flag6 = this.m_conf["lottery"] != null;
			if (flag6)
			{
				Variant variant6 = this.m_conf["lottery"];
				this.m_conf.RemoveKey("lottery");
				this.m_conf["lottery"] = new Variant();
				for (int n = 0; n < variant6.Count; n++)
				{
					this.m_conf["hot"][variant6[n]["item_id"]._str] = variant6[n];
				}
			}
		}

		public Variant get_hot_items_tpid()
		{
			Variant variant = new Variant();
			for (int i = 0; i < 6; i++)
			{
				variant.pushBack(this.m_item["hot"][i]["item_id"]);
			}
			return variant;
		}

		public Variant get_common_items_tpid()
		{
			Variant variant = new Variant();
			for (int i = 0; i < this.m_item["sell"].Count; i++)
			{
				variant.pushBack(this.m_item["sell"][i]["item_id"]);
			}
			return variant;
		}

		public Variant get_hot_items()
		{
			return this.m_conf["hot"];
		}

		public Variant get_sell_items()
		{
			return this.m_conf["sell"];
		}

		public Variant get_hsell_items()
		{
			return this.m_conf["hsell"];
		}

		public Variant get_bndsell_items()
		{
			return this.m_conf["bndsell"];
		}

		public Variant get_shopppt_items()
		{
			return this.m_conf["shoppt"];
		}

		public Variant get_lottery_items()
		{
			return this.m_conf["lottery"];
		}

		public int IsMultiType(int tpid)
		{
			bool flag = this._sellItem.Count == 0;
			if (flag)
			{
				this._sellItem = new Variant();
				foreach (Variant current in this.m_conf["sell"].Values)
				{
					this._sellItem[current["item_id"]._int.ToString()] = 1;
				}
				bool flag2 = this.m_conf.ContainsKey("bndsell");
				if (flag2)
				{
					foreach (Variant current2 in this.m_conf["bndsell"].Values)
					{
						bool flag3 = this._sellItem.ContainsKey(current2["item_id"]._int.ToString());
						if (flag3)
						{
							Variant sellItem = this._sellItem;
							string key = current2["item_id"]._int.ToString();
							sellItem[key] |= 16;
						}
					}
				}
				bool flag4 = this.m_conf.ContainsKey("shoppt");
				if (flag4)
				{
					foreach (Variant current3 in this.m_conf["shoppt"].Values)
					{
						Variant sellItem = this._sellItem;
						string key = current3["item_id"]._int.ToString();
						sellItem[key] |= 256;
					}
				}
			}
			bool flag5 = this._sellItem.ContainsKey(tpid.ToString());
			int result;
			if (flag5)
			{
				result = this._sellItem[tpid.ToString()];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public Variant get_market_sellitem_by_tpid(int tpid)
		{
			bool flag = this.m_conf == null || this.m_conf["sell"] == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = new Variant();
				for (int i = 0; i < this.m_item["sell"].Count; i++)
				{
					variant = this.m_item["sell"][i];
					bool flag2 = variant == null;
					if (!flag2)
					{
						bool flag3 = variant["item_id"] == tpid;
						if (flag3)
						{
							result = variant;
							return result;
						}
					}
				}
				result = null;
			}
			return result;
		}

		public Variant get_game_market_sell_data_by_tpid(int tpid)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string[] array = new string[]
				{
					"sell",
					"bndsell",
					"hsell",
					"ptsell",
					"petptsell",
					"shoppt",
					"lottery"
				};
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string key = array2[i];
					bool flag2 = this.m_conf[key] != null && this.m_conf[key].ContainsKey(tpid.ToString());
					if (flag2)
					{
						result = this.m_conf[key][tpid.ToString()];
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public Variant GetMaketConfig()
		{
			return this.m_conf;
		}

		public Variant Getptsell()
		{
			return this.m_conf["ptsell"];
		}

		public Variant get_market_Psellitem_by_tpid(int tpid)
		{
			bool flag = this.m_item && this.m_item["psell"];
			Variant result;
			if (flag)
			{
				using (List<Variant>.Enumerator enumerator = this.m_item["psell"]._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int idx = enumerator.Current;
						bool flag2 = this.m_item["psell"][idx]["item_id"] == tpid;
						if (flag2)
						{
							result = this.m_item["psell"][idx];
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}

		public Variant get_market_bnd_by_tpid(int tpid)
		{
			bool flag = this.m_item && this.m_item["bndsell"];
			Variant result;
			if (flag)
			{
				using (List<Variant>.Enumerator enumerator = this.m_item["bndsell"]._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int idx = enumerator.Current;
						bool flag2 = this.m_item["bndsell"][idx]["item_id"] == tpid;
						if (flag2)
						{
							result = this.m_item["bndsell"][idx];
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}

		public Variant get_clan_items()
		{
			return this.m_conf["clan"];
		}
	}
}
