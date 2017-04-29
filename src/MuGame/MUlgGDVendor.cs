using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class MUlgGDVendor : lgGDBase
	{
		private InGameItemMsgs _vendormsg = null;

		private MULGIUIVendor _uiVendor;

		private Variant _curVer = new Variant();

		private Variant _selfVendorData = new Variant();

		private Variant _verdorData = new Variant();

		private int _msgNum = 0;

		private int _curMsg = 1;

		private MULGIUIVendor uiVendor
		{
			get
			{
				bool flag = !Convert.ToBoolean(this._uiVendor);
				if (flag)
				{
					this._uiVendor = ((this.g_mgr.g_uiM as muUIClient).getLGUI("stall") as MULGIUIVendor);
				}
				return this._uiVendor;
			}
		}

		public MUlgGDVendor(gameManager m) : base(m)
		{
			this._curVer["1"] = 0;
			this._curVer["2"] = 0;
			this._curVer["3"] = 0;
			this._curVer["4"] = 0;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new MUlgGDVendor(m as gameManager);
		}

		public override void init()
		{
		}

		public void GetVendorInfo(int auctp)
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			variant["auctp"] = auctp;
			variant["ver"] = this._curVer[auctp];
			this._vendormsg.get_auc_info(variant);
		}

		public void AddPrice(int itmid, uint yb_bid, uint gld_bid, int auctp)
		{
			Variant variant = new Variant();
			variant["tp"] = 3;
			variant["auctp"] = auctp;
			variant["itmid"] = itmid;
			bool flag = yb_bid > 0u;
			if (flag)
			{
				variant["yb_bid"] = yb_bid;
			}
			else
			{
				variant["gld_bid"] = gld_bid;
			}
			this._vendormsg.get_auc_info(variant);
		}

		public void AddPriceRes(Variant data)
		{
			foreach (Variant current in this._selfVendorData._arr)
			{
				Variant variant = current["itms"];
				foreach (Variant current2 in variant._arr)
				{
					bool flag = current2["itm"]["id"] == data["itmid"];
					if (flag)
					{
						bool flag2 = data.ContainsKey("yb_bid");
						if (flag2)
						{
							bool flag3 = !current2.ContainsKey("yb_bid");
							if (flag3)
							{
								current2["yb_bid"] = 0;
							}
							Variant variant2 = current2;
							variant2["yb_bid"] = variant2["yb_bid"] + data["yb_bid"]._int;
						}
						bool flag4 = data.ContainsKey("gld_bid");
						if (flag4)
						{
							bool flag5 = !current2.ContainsKey("gld_bid");
							if (flag5)
							{
								current2["gld_bid"] = 0;
							}
							Variant variant2 = current2;
							variant2["gld_bid"] = variant2["gld_bid"] + data["gld_bid"]._int;
						}
						break;
					}
				}
			}
			bool flag6 = Convert.ToBoolean(this.uiVendor);
			if (flag6)
			{
				this.uiVendor.AddPriceRes(data);
			}
		}

		public void AddVendorItem(Variant data)
		{
			this._vendormsg.add_auc_itm(data);
		}

		public void AddVendorItemRes(Variant data, Array itmArr)
		{
			bool flag = this._selfVendorData[data["auctp"]];
			if (flag)
			{
				Variant variant = this._selfVendorData[data["auctp"]]["itms"];
				foreach (Variant variant2 in itmArr)
				{
					bool flag2 = variant2["id"] == data["itmid"];
					if (flag2)
					{
						data["itm"] = variant2;
						variant._arr.Add(data);
						break;
					}
				}
			}
			bool flag3 = Convert.ToBoolean(this.uiVendor);
			if (flag3)
			{
				this.uiVendor.AddVendorItemRes(data);
			}
		}

		public void ReaddVendorItem(Variant data)
		{
			foreach (Variant current in this._selfVendorData._arr)
			{
				Variant variant = current["itms"];
				foreach (Variant current2 in variant._arr)
				{
					bool flag = current2["itm"]["id"] == data["itmid"];
					if (flag)
					{
						current2["tm"] = data["tm"];
						this.uiVendor.AddVendorItemRes(data);
						break;
					}
				}
			}
		}

		public void RemoveAucItem(uint itmid, int auctp)
		{
			this._vendormsg.rmv_auc_itm(itmid, auctp);
		}

		public void RemoveAucItemRes(Variant data)
		{
			Variant variant = this.RemoveVendorItem(data["itmid"]);
			bool flag = variant;
			if (flag)
			{
				(this.g_mgr as muLGClient).g_itemsCT.pkg_add_items(variant, 0);
			}
		}

		public void GetPlyAuclist(int cid, int auctp)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			variant["auctp"] = auctp;
			this._vendormsg.get_ply_auc_list(variant);
		}

		public void GetPlyAuclistRes(Variant data)
		{
			this._selfVendorData[data["auctp"]] = data;
			bool flag = Convert.ToBoolean(this.uiVendor);
			if (flag)
			{
				this.uiVendor.GetPlyAuclistRes(data);
			}
		}

		public Variant GetSelfVendor(int auctp)
		{
			return this._selfVendorData[auctp];
		}

		public Variant GetVendorItem(int id)
		{
			Variant result = new Variant();
			foreach (Variant current in this._selfVendorData._arr)
			{
				Variant variant = current["itms"];
				foreach (Variant current2 in variant._arr)
				{
					bool flag = current2["itm"]["id"]._int == id;
					if (flag)
					{
						result = current2["itm"];
						break;
					}
				}
			}
			return result;
		}

		public Variant RemoveVendorItem(int id)
		{
			Variant variant = new Variant();
			foreach (Variant current in this._selfVendorData._arr)
			{
				Variant variant2 = current["itms"];
				for (int i = 0; i < variant2.Length; i++)
				{
					bool flag = variant2[i]["itm"]["id"] == id;
					if (flag)
					{
						variant = variant2[i]["itm"];
						variant2._arr.RemoveAt(i);
						bool flag2 = Convert.ToBoolean(this.uiVendor);
						if (flag2)
						{
							this.uiVendor.RemoveVendorItemRes(variant);
						}
						break;
					}
				}
			}
			return variant;
		}

		public void BuyAucItem(uint cid, uint itemid, int auctp)
		{
			this._vendormsg.buy_auc_itm(cid, itemid, auctp);
		}

		public void BuyAucItemRes(Variant data)
		{
			Variant data2 = new Variant();
			bool flag = this._verdorData[data["auctp"]];
			if (flag)
			{
				Variant variant = this._verdorData[data["auctp"]]["itms"];
				for (int i = 0; i < variant.Length; i++)
				{
					bool flag2 = data["itmid"] == variant[i]["itm"]["id"];
					if (flag2)
					{
						data2 = variant[i]["itm"];
						variant._arr.RemoveAt(i);
						break;
					}
				}
			}
			bool flag3 = Convert.ToBoolean(this.uiVendor);
			if (flag3)
			{
				this.uiVendor.BuyAucItemRes(data2);
			}
		}

		public Variant GetAucItem(int id)
		{
			Variant result;
			foreach (Variant current in this._verdorData._arr)
			{
				Variant variant = current["itms"];
				foreach (Variant current2 in variant._arr)
				{
					bool flag = current2["itm"]["id"] == id;
					if (flag)
					{
						result = current2["itm"];
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public void GetAucinfoRes(Variant data)
		{
			this._curVer[data["auctp"]] = data["ver"];
			this._verdorData[data["auctp"]] = data;
			bool flag = Convert.ToBoolean(this.uiVendor);
			if (flag)
			{
				this.uiVendor.GetAucinfoRes(data);
			}
		}

		public Variant GetVerdorItems(int auctp)
		{
			Variant result = new Variant();
			bool flag = this._verdorData[auctp];
			if (flag)
			{
				result = this._verdorData[auctp]["itms"];
			}
			return result;
		}

		public Variant GetVerdorData(int auctp)
		{
			return this._verdorData[auctp];
		}

		public void FetchMoneyRes()
		{
			bool flag = Convert.ToBoolean(this.uiVendor);
			if (flag)
			{
				this.uiVendor.FetchMoneyRes();
			}
		}

		public void GetAllAcuInfo()
		{
			uint level = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.level;
			bool flag = level > 10u;
			if (flag)
			{
				this._msgNum = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.GetAucNum();
			}
		}

		private void msgProcess(float tm)
		{
			bool flag = this._curMsg >= this._msgNum;
			if (flag)
			{
				this._curMsg = 1;
				this._msgNum = 0;
			}
			else
			{
				this.GetVendorInfo(this._curMsg);
				this._curMsg++;
			}
		}
	}
}
