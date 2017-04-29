using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class checks
	{
		private lgSelfPlayer _selfPlayer = null;

		private gameManager g_mgr;

		private Dictionary<string, Func<Variant, bool>> checkMethod;

		private Dictionary<string, Func<lgSelfPlayer, Variant, gameManager, bool>> propertyMethod;

		private Variant equip_data
		{
			get
			{
				return this._selfPlayer.data["eqp"];
			}
		}

		public checks()
		{
			this.checkMethod = new Dictionary<string, Func<Variant, bool>>();
			this.checkMethod.Add("check_complex_flvl", new Func<Variant, bool>(this.check_complex_flvl));
			this.checkMethod.Add("check_flvl", new Func<Variant, bool>(this.check_flvl));
			this.checkMethod.Add("check_stns", new Func<Variant, bool>(this.check_stns));
			this.checkMethod.Add("check_bcnt", new Func<Variant, bool>(this.check_bcnt));
			this.checkMethod.Add("check_kcnt", new Func<Variant, bool>(this.check_kcnt));
			this.checkMethod.Add("check_have", new Func<Variant, bool>(this.check_have));
			this.checkMethod.Add("check_ware", new Func<Variant, bool>(this.check_ware));
			this.propertyMethod = new Dictionary<string, Func<lgSelfPlayer, Variant, gameManager, bool>>();
			this.propertyMethod.Add("attchk", new Func<lgSelfPlayer, Variant, gameManager, bool>(this.attchk));
			this.propertyMethod.Add("eqpchk", new Func<lgSelfPlayer, Variant, gameManager, bool>(this.eqpchk));
		}

		public bool isPropertyMethod(string name)
		{
			return this.propertyMethod.ContainsKey(name);
		}

		public Func<lgSelfPlayer, Variant, gameManager, bool> getCheckMethod(string name)
		{
			return this.isPropertyMethod(name) ? this.propertyMethod[name] : null;
		}

		public bool attchk(lgSelfPlayer self, Variant attchk, gameManager mgr = null)
		{
			Variant variant = self.get_value(attchk["name"]);
			bool flag = attchk["name"] == "carr";
			bool result;
			if (flag)
			{
				result = lgSelfPlayer.check_carr(attchk["and"], self.data);
			}
			else
			{
				bool flag2 = attchk["name"] == "autofinlvl";
				if (flag2)
				{
					result = (variant != null);
				}
				else
				{
					bool flag3 = attchk.ContainsKey("have");
					if (flag3)
					{
						bool isArr = variant.isArr;
						if (isArr)
						{
							bool flag4 = variant._arr.IndexOf(attchk["have"]) != -1;
							if (flag4)
							{
								result = true;
								return result;
							}
						}
						else
						{
							bool flag5 = variant == attchk["have"];
							if (flag5)
							{
								result = true;
								return result;
							}
						}
					}
					bool isArr2 = variant.isArr;
					if (isArr2)
					{
						result = false;
					}
					else
					{
						bool flag6 = attchk.ContainsKey("equal");
						if (flag6)
						{
							bool flag7 = variant == attchk["equal"];
							result = flag7;
						}
						else
						{
							bool flag8 = attchk.ContainsKey("min") || attchk.ContainsKey("max");
							if (flag8)
							{
								bool flag9 = attchk.ContainsKey("min");
								if (flag9)
								{
									bool flag10 = variant < attchk["min"];
									if (flag10)
									{
										result = false;
										return result;
									}
								}
								bool flag11 = attchk.ContainsKey("max");
								if (flag11)
								{
									bool flag12 = variant > attchk["max"];
									if (flag12)
									{
										result = false;
										return result;
									}
								}
								result = true;
							}
							else
							{
								result = false;
							}
						}
					}
				}
			}
			return result;
		}

		public bool eqpchk(lgSelfPlayer self, Variant attchk, gameManager mgr)
		{
			bool flag = this._selfPlayer == null;
			if (flag)
			{
				this._selfPlayer = self;
			}
			bool flag2 = this.g_mgr == null;
			if (flag2)
			{
				this.g_mgr = mgr;
			}
			string str = attchk["name"];
			string key = "check_" + str;
			bool flag3 = !this.checkMethod.ContainsKey(key);
			return flag3 || this.checkMethod[key](attchk);
		}

		private bool check_pos_qual_body(Variant eqpchk, string chkTP)
		{
			int cid = this._selfPlayer.cid;
			Variant data = this._selfPlayer.data;
			Variant variant = null;
			bool flag = data != null;
			if (flag)
			{
				variant = data["equip"];
			}
			bool flag2 = variant == null;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				foreach (Variant current in variant._arr)
				{
					Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]);
					bool flag3 = variant2 == null;
					if (!flag3)
					{
						bool flag4 = variant2["conf"][chkTP] == eqpchk[chkTP];
						if (flag4)
						{
							result = true;
							return result;
						}
					}
				}
				result = false;
			}
			return result;
		}

		private bool check_pos_qual_items(Variant eqpchk, string chkTP)
		{
			Variant variant = (this.g_mgr.g_gameM as muLGClient).g_itemsCT.GetPkgItems();
			bool flag = variant == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				variant = variant["eqp"];
				foreach (Variant current in variant._arr)
				{
					Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]);
					bool flag2 = variant2 == null;
					if (!flag2)
					{
						bool flag3 = variant2["conf"][chkTP] == eqpchk[chkTP];
						if (flag3)
						{
							result = true;
							return result;
						}
					}
				}
				result = false;
			}
			return result;
		}

		private bool check_complex_flvl(Variant eqpchk)
		{
			bool flag = false;
			int num = this._selfPlayer.data["cid"];
			Variant data = this._selfPlayer.data;
			Variant variant = null;
			bool flag2 = data != null;
			if (flag2)
			{
				variant = data["equip"];
			}
			foreach (Variant current in variant._arr)
			{
				bool flag3 = !current.ContainsKey("flvl");
				if (!flag3)
				{
					bool flag4 = current["flvl"] >= eqpchk["flvl"];
					if (flag4)
					{
						int tpid = current["tpid"];
						flag = true;
						bool flag5 = eqpchk.ContainsKey("pos");
						if (flag5)
						{
							flag = false;
							bool flag6 = this.check_qual_items_by_tpid(tpid, eqpchk, "pos") || this.check_qual_body_by_tpid(tpid, eqpchk, "pos");
							if (flag6)
							{
								flag = true;
								break;
							}
						}
					}
				}
			}
			bool flag7 = !flag;
			if (flag7)
			{
				Variant variant2 = (this.g_mgr.g_gameM as muLGClient).g_itemsCT.GetPkgItems();
				bool flag8 = variant2 != null;
				if (flag8)
				{
					variant2 = variant2["eqp"];
					foreach (Variant current2 in variant2._arr)
					{
						bool flag9 = !current2.ContainsKey("flvl");
						if (!flag9)
						{
							bool flag10 = current2["flvl"] >= eqpchk["flvl"];
							if (flag10)
							{
								int tpid = current2["tpid"];
								flag = true;
								bool flag11 = eqpchk.ContainsKey("pos");
								if (flag11)
								{
									flag = false;
									bool flag12 = this.check_qual_items_by_tpid(tpid, eqpchk, "pos") || this.check_qual_body_by_tpid(tpid, eqpchk, "pos");
									if (flag12)
									{
										flag = true;
										break;
									}
								}
							}
						}
					}
				}
			}
			return flag;
		}

		private bool check_qual_body_by_tpid(int tpid, Variant eqpchk, string chkTP)
		{
			Variant data = this._selfPlayer.data;
			Variant variant = null;
			bool flag = data != null;
			if (flag)
			{
				variant = data["equip"];
			}
			bool flag2 = variant == null;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				foreach (Variant current in variant._arr)
				{
					Variant variant2 = null;
					bool flag3 = current["tpid"] == tpid;
					if (flag3)
					{
						variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]);
					}
					bool flag4 = variant2 == null;
					if (!flag4)
					{
						bool flag5 = variant2["conf"][chkTP] == eqpchk[chkTP];
						if (flag5)
						{
							result = true;
							return result;
						}
					}
				}
				result = false;
			}
			return result;
		}

		private bool check_qual_items_by_tpid(int tpid, Variant eqpchk, string chkTP)
		{
			Variant variant = (this.g_mgr.g_gameM as muLGClient).g_itemsCT.GetPkgItems();
			bool flag = variant == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				variant = variant["eqp"];
				foreach (Variant current in variant._arr)
				{
					Variant variant2 = null;
					bool flag2 = current["tpid"] == tpid;
					if (flag2)
					{
						variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]);
					}
					bool flag3 = variant2 == null;
					if (!flag3)
					{
						bool flag4 = variant2["conf"][chkTP] == eqpchk[chkTP];
						if (flag4)
						{
							result = true;
							return result;
						}
					}
				}
				result = false;
			}
			return result;
		}

		private bool check_prop(string prop, uint value)
		{
			bool result;
			foreach (Variant current in this.equip_data._arr)
			{
				bool flag = !current.ContainsKey(prop);
				if (!flag)
				{
					bool flag2 = current[prop] >= value;
					if (flag2)
					{
						result = true;
						return result;
					}
				}
			}
			Variant variant = (this.g_mgr.g_gameM as muLGClient).g_itemsCT.GetPkgItems();
			bool flag3 = variant != null;
			if (flag3)
			{
				variant = variant["eqp"];
				foreach (Variant current2 in variant._arr)
				{
					bool flag4 = !current2.ContainsKey(prop);
					if (!flag4)
					{
						bool flag5 = current2[prop] >= value;
						if (flag5)
						{
							result = true;
							return result;
						}
					}
				}
			}
			result = false;
			return result;
		}

		public bool check_flvl(Variant eqpchk)
		{
			bool flag = eqpchk.ContainsKey("pos");
			bool result;
			if (flag)
			{
				result = this.check_complex_flvl(eqpchk);
			}
			else
			{
				result = this.check_prop("flvl", eqpchk["flvl"]);
			}
			return result;
		}

		public bool check_stns(Variant eqpchk)
		{
			bool result;
			foreach (Variant current in this.equip_data._arr)
			{
				bool flag = !current.ContainsKey("stn");
				if (!flag)
				{
					bool flag2 = current["stn"].Count >= eqpchk["cnt"];
					if (flag2)
					{
						result = true;
						return result;
					}
				}
			}
			Variant variant = (this.g_mgr.g_gameM as muLGClient).g_itemsCT.GetPkgItems();
			bool flag3 = variant != null;
			if (flag3)
			{
				variant = variant["eqp"];
				foreach (Variant current2 in variant._arr)
				{
					bool flag4 = !current2.ContainsKey("stn");
					if (!flag4)
					{
						bool flag5 = current2["stn"].Count >= eqpchk["cnt"];
						if (flag5)
						{
							result = true;
							return result;
						}
					}
				}
			}
			result = false;
			return result;
		}

		public bool check_bcnt(Variant eqpchk)
		{
			return this.check_prop("bcnt", eqpchk["cnt"]);
		}

		public bool check_kcnt(Variant eqpchk)
		{
			return this.check_prop("kcnt", eqpchk["cnt"]);
		}

		public bool check_have(Variant eqpchk)
		{
			bool flag = eqpchk.ContainsKey("qual");
			bool result;
			if (flag)
			{
				bool flag2 = !this.check_pos_qual_items(eqpchk, "qual") && !this.check_pos_qual_body(eqpchk, "qual");
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			bool flag3 = eqpchk.ContainsKey("pos");
			if (flag3)
			{
				bool flag4 = !this.check_pos_qual_items(eqpchk, "pos") && !this.check_pos_qual_body(eqpchk, "pos");
				if (flag4)
				{
					result = false;
					return result;
				}
			}
			foreach (Variant current in this.equip_data._arr)
			{
				Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]);
				bool flag5 = variant == null || !variant["conf"].ContainsKey("pos");
				if (!flag5)
				{
					bool flag6 = variant["conf"]["pos"] == eqpchk["pos"];
					if (flag6)
					{
						bool flag7 = variant["conf"]["qual"] >= eqpchk["qual"];
						if (flag7)
						{
							result = true;
							return result;
						}
					}
				}
			}
			Variant variant2 = (this.g_mgr.g_gameM as muLGClient).g_itemsCT.GetPkgItems();
			bool flag8 = variant2 != null;
			if (flag8)
			{
				variant2 = variant2["eqp"];
				foreach (Variant current2 in variant2._arr)
				{
					Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current2["tpid"]);
					bool flag9 = variant == null || !variant["conf"].ContainsKey("pos");
					if (!flag9)
					{
						bool flag10 = variant["conf"]["pos"] == eqpchk["pos"];
						if (flag10)
						{
							bool flag11 = variant["conf"]["qual"] >= eqpchk["qual"];
							if (flag11)
							{
								result = true;
								return result;
							}
						}
					}
				}
			}
			result = false;
			return result;
		}

		public bool check_ware(Variant eqpchk)
		{
			return this.check_pos_qual_body(eqpchk, "qual");
		}
	}
}
