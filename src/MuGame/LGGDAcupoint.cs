using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LGGDAcupoint : lgGDBase
	{
		private Variant _meris_info = null;

		private bool _isLoad = false;

		private InGameAcupointMsgs _acupointMsg;

		public Variant meris_info
		{
			get
			{
				return this._meris_info;
			}
		}

		public LGGDAcupoint(gameManager m) : base(m)
		{
			this._acupointMsg = (this.g_mgr.g_netM as muNetCleint).igAcupointMsgs;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGDAcupoint(m as gameManager);
		}

		public override void init()
		{
		}

		public bool is_load_data()
		{
			return this._isLoad;
		}

		public void init_meri_data()
		{
			bool flag = !this._isLoad;
			if (flag)
			{
				this._meris_info = new Variant();
				Variant variant = new Variant();
				bool flag2 = variant;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						this._meris_info[current]["id"] = new Variant();
						this._meris_info[current]["id"]["mdata"] = current;
						this._meris_info[current]["id"]["adata"] = null;
						this._meris_info[current]["id"]["isopen"] = false;
					}
				}
				this.requets_open_meri();
				this._isLoad = true;
			}
		}

		public Variant get_meri_info_by_meirid(int meirid)
		{
			return this._meris_info ? this._meris_info[meirid] : null;
		}

		public bool is_open_meri(int meriid)
		{
			Variant variant = this.get_meri_info_by_meirid(meriid);
			return variant && variant["isopen"];
		}

		public Variant get_open_meri()
		{
			Variant variant = new Variant();
			foreach (Variant current in this._meris_info._arr)
			{
				bool flag = current["isopen"];
				if (flag)
				{
					variant["meo"].pushBack(current);
				}
			}
			return variant;
		}

		private void set_meri_open(int meriid)
		{
			Variant variant = this.get_meri_info_by_meirid(meriid);
			bool flag = variant;
			if (flag)
			{
				variant["isopen"] = true;
			}
		}

		private void add_aucp_data(int meriid, Variant aucparr)
		{
			Variant variant = this.get_meri_info_by_meirid(meriid);
			bool flag = variant == null;
			if (!flag)
			{
				Variant variant2 = variant["adata"];
				bool flag2 = variant2 == null;
				if (flag2)
				{
					variant2 = new Variant();
				}
				foreach (Variant current in aucparr._arr)
				{
					bool flag3 = false;
					foreach (Variant current2 in variant2._arr)
					{
						bool flag4 = current["aid"] == current2["aid"];
						if (flag4)
						{
							flag3 = true;
							break;
						}
					}
					bool flag5 = flag3;
					if (!flag5)
					{
						variant2["a_t"].pushBack(current);
					}
				}
				variant["adata"] = variant2;
			}
		}

		public Variant get_meri_open_info(int meriid)
		{
			Variant variant = this.get_meri_info_by_meirid(meriid);
			bool flag = variant == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant2 = variant["mdata"];
				Variant variant3 = new Variant();
				using (List<Variant>.Enumerator enumerator = variant2._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						bool flag2 = text == "name" || text == "id" || text == "acup";
						if (!flag2)
						{
							bool isArr = variant2[text].isArr;
							if (isArr)
							{
								Variant variant4 = new Variant();
								variant4["name"] = text;
								variant4["mes"] = variant2[text];
								variant3.pushBack(variant4);
							}
							else
							{
								Variant variant5 = new Variant();
								variant5["name"] = text;
								variant5["mes"] = variant2[text].ToString();
								variant3.pushBack(variant5);
							}
						}
					}
				}
				result = variant3;
			}
			return result;
		}

		public Variant get_aucp_info(int meriid, int aucpid)
		{
			Variant variant = this.get_meri_info_by_meirid(meriid);
			bool flag = variant && variant["adata"];
			Variant result;
			if (flag)
			{
				foreach (Variant current in variant["adata"]._arr)
				{
					bool flag2 = current["aid"] == aucpid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public bool is_meri_crossed(int meriid)
		{
			Variant variant = this.get_meri_info_by_meirid(meriid);
			Variant variant2 = this.get_aucparr_by_meriid(meriid);
			bool flag = variant == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = variant["adata"] == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					Variant variant3 = variant["adata "];
					bool flag3 = variant3 == null || variant3.Count < variant2.Count - 1;
					result = !flag3;
				}
			}
			return result;
		}

		public Variant get_no_open_aucp_info(int meriid, int aucpid, int apt = 0)
		{
			Variant variant = this.get_aucp_by_meriid(meriid, aucpid);
			Variant variant2 = new Variant();
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					string languageText = LanguagePack.getLanguageText("att_mod", text);
					bool flag = languageText == "" || languageText == "undefine";
					if (!flag)
					{
						bool isArr = variant[text].isArr;
						if (isArr)
						{
							Variant value = this.create_acup_att(variant[text], (double)apt);
							Variant variant3 = new Variant();
							variant3["name"] = text;
							variant3["res"] = value;
							variant3["apt"] = apt;
							variant2.pushBack(variant3);
						}
						else
						{
							double val = (double)(variant[text] + variant[text] * apt / 100);
							Variant variant4 = new Variant();
							variant4["name"] = text;
							variant4["res"] = val;
							variant4["apt"] = apt;
							variant2.pushBack(variant4);
						}
					}
				}
			}
			return variant2;
		}

		public Variant get_open_aucp_info(int meriid, int aucpid, int addapt = 0)
		{
			Variant variant = this.get_aucp_by_meriid(meriid, aucpid);
			Variant variant2 = this.get_aucp_info(meriid, aucpid);
			bool flag = variant2 == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant3 = new Variant();
				int num = variant2["apt"] + addapt;
				using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						string languageText = LanguagePack.getLanguageText("att_mod", text);
						bool flag2 = languageText == "" || languageText == "undefine";
						if (!flag2)
						{
							bool isArr = variant[text].isArr;
							if (isArr)
							{
								Variant value = this.create_acup_att(variant[text], (double)num);
								Variant variant4 = new Variant();
								variant4["name"] = text;
								variant4["res"] = value;
								variant4["apt"] = num;
								variant3.pushBack(variant4);
							}
							else
							{
								double val = (double)(variant[text] + variant[text] * num / 100);
								Variant variant5 = new Variant();
								variant5["name"] = text;
								variant5["res"] = val;
								variant5["apt"] = num;
								variant3.pushBack(variant5);
							}
						}
					}
				}
				result = variant3;
			}
			return result;
		}

		private Variant create_acup_att(Variant aobj, double apt)
		{
			bool isArr = aobj.isArr;
			Variant result;
			if (isArr)
			{
				Variant variant = new Variant();
				foreach (Variant current in aobj._arr)
				{
					Variant variant2 = new Variant();
					using (List<Variant>.Enumerator enumerator2 = current._arr.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string text = enumerator2.Current;
							bool flag = text == "per" || text == "red" || text == "redtm";
							if (flag)
							{
								double val = current[text] + current[text] * apt / 100.0;
								variant2[text] = val;
							}
							else
							{
								variant2[text] = current[text];
							}
						}
					}
					variant.pushBack(variant2);
				}
				result = variant;
			}
			else
			{
				result = aobj;
			}
			return result;
		}

		private Variant get_aucparr_by_meriid(int meriid)
		{
			Variant variant = this.get_meri_info_by_meirid(meriid);
			return (variant && variant["mdata"]) ? variant["mdata"]["acup"] : null;
		}

		private Variant get_aucp_by_meriid(int meriid, int aucpid)
		{
			Variant variant = this.get_aucparr_by_meriid(meriid);
			bool flag = variant;
			Variant result;
			if (flag)
			{
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current && current["aid"] == aucpid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public void change_aucpup(Variant data)
		{
			Variant variant = this.get_aucp_info(data["meriid"], data["aid"]);
			bool flag = variant == null;
			if (!flag)
			{
				using (List<Variant>.Enumerator enumerator = data._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						bool flag2 = text == "meriid";
						if (!flag2)
						{
							bool flag3 = text == "alvl";
							if (flag3)
							{
							}
							variant[text] = data[text];
						}
					}
				}
			}
		}

		public bool is_aucp_max(int meriid, int aid)
		{
			int num = 0;
			Variant variant = this.get_aucp_info(meriid, aid);
			return variant && variant["alvl"] >= num;
		}

		public Variant get_need_info(int meriid, int aid)
		{
			Variant variant = this.get_aucp_by_meriid(meriid, aid);
			Variant variant2 = this.get_aucp_info(meriid, aid);
			Variant variant3 = new Variant();
			bool flag = variant3 == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = variant["gld"];
				int num2 = variant3["gld_per"];
				int val = num * num2 / 100;
				int num3 = variant["up_meript"];
				int num4 = variant3["meript_per"];
				int val2 = num3 * num4 / 100;
				Variant variant4 = new Variant();
				variant4["needtb"] = val;
				variant4["needPoints"] = val2;
				result = variant4;
			}
			return result;
		}

		public bool is_open_aucp_by_itemid(int tpid)
		{
			Variant variant = this.get_meri_by_itemid(tpid);
			bool flag = variant == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant variant2 = this.get_aucp_info(variant["meriid"], variant["aid"]);
				bool flag2 = variant2 == null;
				result = !flag2;
			}
			return result;
		}

		public Variant get_meri_by_itemid(int tpid)
		{
			Variant variant = new Variant();
			bool flag = variant == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !variant["conf"]["acupbk"];
				if (flag2)
				{
					result = null;
				}
				else
				{
					Variant variant2 = variant["conf"]["acupbk"][0];
					bool flag3 = !variant2["ap"];
					if (flag3)
					{
						result = null;
					}
					else
					{
						int val = variant2["meriid"];
						Variant variant3 = variant2["ap"][0];
						bool flag4 = variant3 == null;
						if (flag4)
						{
							result = null;
						}
						else
						{
							int val2 = variant3["aid"];
							Variant variant4 = new Variant();
							variant4["meriid"] = val;
							variant4["aid"] = val2;
							result = variant4;
						}
					}
				}
			}
			return result;
		}

		public void get_meri_info_res(Variant data)
		{
			this.add_aucp_data(data["meriid"], data["acup"]);
		}

		public void acupupcd_chang(Variant data)
		{
		}

		public void get_meris_res(Variant data)
		{
			int @int = data["tp"]._int;
			if (@int != 1)
			{
				if (@int == 2)
				{
					LGIUIStarmap lGIUIStarmap = new Variant() as LGIUIStarmap;
					lGIUIStarmap.OnMeriptChange();
				}
			}
			else
			{
				Variant variant = data["meris"];
				for (int i = 0; i < variant.Count; i++)
				{
					int num = variant[i]["meriid"];
					this.get_mes_meri(num);
					this.set_meri_open(num);
				}
			}
		}

		public void meri_activate(Variant data)
		{
			this.set_meri_open(data["meriid"]);
			Variant variant = this.get_meri_info_by_meirid(data["meriid"]);
			bool flag = variant == null;
			if (!flag)
			{
				LGIUIStarmap lGIUIStarmap = new Variant() as LGIUIStarmap;
				lGIUIStarmap.OnMerisRes();
			}
		}

		public void acup_activate(Variant data)
		{
			Variant variant = new Variant();
			variant["aid"] = data["aid"];
			variant["alvl"] = data["alvl"];
			variant["apt"] = data["apt"];
			this.add_aucp_data(data["meriid"], variant);
			Variant variant2 = this.get_meri_info_by_meirid(data["meriid"]);
			LGIUIStarmap lGIUIStarmap = new Variant() as LGIUIStarmap;
			lGIUIStarmap.onAcupupcdChange();
		}

		public void acupup_res(Variant data)
		{
			this.change_aucpup(data);
		}

		public void start_acupup(uint meriid, uint aid)
		{
			this._acupointMsg.start_acupup(meriid, aid);
		}

		public void quick_fin_cd(float tm)
		{
			this._acupointMsg.quick_fin_cd(tm);
		}

		public void expend_cdcnt()
		{
			this._acupointMsg.expend_cdcnt();
		}

		public void get_mes_meri(int meriid)
		{
			this._acupointMsg.get_mes_meri(meriid);
		}

		public void requets_open_meri()
		{
			this._acupointMsg.requets_open_meri();
		}

		public void requestOpenMeri(int meriid = 0, int aid = 0)
		{
			bool flag = meriid != 0;
			if (flag)
			{
				bool flag2 = aid != 0;
				if (flag2)
				{
					this._acupointMsg.OpenMeri(meriid, aid);
				}
				else
				{
					this._acupointMsg.OpenAcup(meriid);
				}
			}
		}
	}
}
