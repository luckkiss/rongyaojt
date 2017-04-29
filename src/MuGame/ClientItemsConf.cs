using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientItemsConf : configParser
	{
		public static ClientItemsConf instace;

		private Variant _clientItem;

		public ClientItemsConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientItemsConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			ClientItemsConf.instace = this;
			bool flag = conf.ContainsKey("item");
			if (flag)
			{
				conf["item"] = GameTools.array2Map(conf["item"], "tpid", 1u);
			}
			bool flag2 = conf.ContainsKey("itemeqp");
			if (flag2)
			{
				conf["itemeqp"] = GameTools.array2Map(conf["itemeqp"], "tpid", 1u);
			}
			bool flag3 = conf.ContainsKey("itemqual");
			if (flag3)
			{
				conf["itemqual"] = GameTools.array2Map(conf["itemqual"], "qual", 1u);
			}
			bool flag4 = conf.ContainsKey("flvlmat");
			if (flag4)
			{
				Variant variant = new Variant();
				foreach (Variant current in conf["flvlmat"]._arr)
				{
					current["flvl"] = GameTools.array2Map(current["flvl"], "lvl", 1u);
					variant[current["id"]] = current;
				}
				conf["flvlmat"] = variant;
			}
			bool flag5 = conf.ContainsKey("flvlemt");
			if (flag5)
			{
				Variant variant2 = new Variant();
				foreach (Variant current2 in conf["flvlemt"]._arr)
				{
					Variant variant3 = new Variant();
					foreach (Variant current3 in current2["attachEffect"]._arr)
					{
						bool flag6 = variant3[current3["lvl"]] == null;
						if (flag6)
						{
							variant3[current3["lvl"]] = new Variant();
						}
						variant3[current3["lvl"]]._arr.Add(current3);
					}
					current2["attachEffect"] = variant3;
					variant2[current2["id"]] = current2;
				}
				conf["flvlemt"] = variant2;
			}
			bool flag7 = conf.ContainsKey("itemIcon");
			if (flag7)
			{
				conf["itemIcon"] = GameTools.array2Map(conf["itemIcon"], "tp", 1u);
			}
			bool flag8 = conf.ContainsKey("clientitem");
			if (flag8)
			{
				conf["clientitem"] = GameTools.array2Map(conf["clientitem"], "tp", 1u);
			}
			bool flag9 = conf.ContainsKey("itemeff");
			if (flag9)
			{
				conf["itemeff"] = GameTools.array2Map(conf["itemeff"], "tp", 1u);
			}
			bool flag10 = conf.ContainsKey("eqpeff");
			if (flag10)
			{
				conf["eqpeff"] = GameTools.array2Map(conf["eqpeff"], "tp", 1u);
			}
			return conf;
		}

		public Variant GetShow3dItem(int tpid)
		{
			bool flag = this.m_conf["item"].ContainsKey(tpid);
			Variant result;
			if (flag)
			{
				Variant variant = this.m_conf["item"][tpid]["show3D"];
				bool flag2 = variant != null;
				if (flag2)
				{
					result = variant[0];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetClientItem(int tpid)
		{
			bool flag = this._clientItem == null;
			if (flag)
			{
				this._clientItem = new Variant();
				Variant variant = this.m_conf["clientitem"][0]["item"];
				foreach (Variant current in variant._arr)
				{
					this._clientItem[current["tpid"]] = current;
				}
			}
			return this._clientItem[tpid];
		}

		public int GetEqppt(uint tpid)
		{
			int result = -1;
			Variant variant = this.m_conf["item"][tpid];
			bool flag = variant != null && variant.ContainsKey("eqppt");
			if (flag)
			{
				result = variant["eqppt"];
			}
			return result;
		}

		public string get_item_icon_url(uint tpid)
		{
			bool flag = this.m_conf["item"].ContainsKey(tpid.ToString());
			string result;
			if (flag)
			{
				result = this.m_conf["item"][tpid.ToString()]["icon"];
			}
			else
			{
				result = "";
			}
			return result;
		}

		public Variant get_item_conf(uint tpid)
		{
			return this.m_conf["item"][tpid.ToString()];
		}

		public string get_item_replace_icon_url(uint tpid, string stype)
		{
			bool flag = this.m_conf["item"].ContainsKey(tpid.ToString());
			string result;
			if (flag)
			{
				string text = this.m_conf["item"][tpid]["icon"];
				if (!(stype == "atf"))
				{
					if (!(stype == "jtx"))
					{
						if (!(stype == "ptx"))
						{
							result = text;
						}
						else
						{
							result = text.Replace(".png", this.m_conf["png2ptx"]);
						}
					}
					else
					{
						result = text.Replace(".png", this.m_conf["png2jtx"]);
					}
				}
				else
				{
					result = text.Replace(".png", this.m_conf["png2atf"]);
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string GetItemNameColorByQual(uint qual)
		{
			bool flag = this.m_conf["itemqual"].ContainsKey(qual.ToString());
			string result;
			if (flag)
			{
				result = this.m_conf["itemqual"][qual.ToString()]["color"];
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string GetItemIconBgByQual(uint qual)
		{
			bool flag = this.m_conf["itemqual"].ContainsKey(qual.ToString());
			string result;
			if (flag)
			{
				result = this.m_conf["itemqual"][qual.ToString()]["icon"];
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string GetItemIconBgByTp(uint tp)
		{
			bool flag = this.m_conf.ContainsKey("itemeff");
			string result;
			if (flag)
			{
				bool flag2 = this.m_conf["itemeff"].ContainsKey(tp.ToString());
				if (flag2)
				{
					result = this.m_conf["itemeff"][tp.ToString()]._str;
					return result;
				}
			}
			result = "";
			return result;
		}

		public Variant GetItemIconEffByTp(uint tp)
		{
			bool flag = base.conf.ContainsKey("itemeff") && base.conf["itemeff"].ContainsKey(tp.ToString());
			Variant result;
			if (flag)
			{
				result = this.m_conf["itemeff"][tp.ToString()];
			}
			else
			{
				result = "";
			}
			return result;
		}

		public Variant GetAppointIconEff(uint tp)
		{
			bool flag = this.m_conf.ContainsKey("eqpeff") && this.m_conf["eqpeff"].ContainsKey(tp.ToString());
			Variant result;
			if (flag)
			{
				result = this.m_conf["eqpeff"][tp.ToString()];
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string GetItemFlvlmat(uint tpid, int flvl)
		{
			Variant variant = this.m_conf["item"][tpid];
			bool flag = variant != null && variant.ContainsKey("flvlmat");
			string result;
			if (flag)
			{
				Variant variant2 = this.m_conf["flvlmat"][variant["flvlmat"]];
				bool flag2 = variant2 != null;
				if (flag2)
				{
					result = variant2["flvl"][flvl]._str;
					return result;
				}
			}
			result = "";
			return result;
		}

		public Variant GetItemFlvlemt(uint tpid, uint flvl)
		{
			Variant variant = this.m_conf["item"][tpid];
			bool flag = variant != null && variant.ContainsKey("flvlemt");
			Variant result;
			if (flag)
			{
				Variant variant2 = this.m_conf["flvlemt"][variant["flvlemt"]];
				bool flag2 = variant2 != null;
				if (flag2)
				{
					result = variant2["attachEffect"][flvl];
					return result;
				}
			}
			result = null;
			return result;
		}

		public uint GetItemGrade(uint tpid)
		{
			bool flag = this.m_conf["item"].ContainsKey((int)tpid);
			uint result;
			if (flag)
			{
				result = this.m_conf["item"][tpid]["grade"];
			}
			else
			{
				result = 0u;
			}
			return result;
		}

		public string GetItemEquipUrl(uint tpid)
		{
			bool flag = this.m_conf.ContainsKey("itemeqp") && this.m_conf["itemeqp"].ContainsKey((int)tpid);
			string result;
			if (flag)
			{
				result = this.m_conf["itemeqp"][tpid]["icon"];
			}
			else
			{
				result = "";
			}
			return result;
		}
	}
}
