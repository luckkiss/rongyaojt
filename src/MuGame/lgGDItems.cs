using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class lgGDItems : lgGDBase
	{
		protected joinWorldInfo m_player = null;

		protected const int CAN_PICK = 0;

		protected const int NOT_PICK = 1;

		protected const int IS_IN_PICK = 2;

		protected const int _soundDelay = 1;

		protected uint _pkg_maxi;

		protected uint _repo_maxi;

		protected uint _pshop_maxi;

		protected const uint _dropItem_max = 1023u;

		protected Variant _pkg_items;

		protected Variant _repo_items;

		protected Variant _temp_items;

		protected Variant _sold_items;

		protected Variant _pshop_items;

		protected Variant _dbmkt_items;

		protected Variant _item_cds = null;

		protected Variant _mergeInfo;

		protected Variant _mergeInfoCBs;

		protected Variant _show_item_data = new Variant();

		protected Variant _show_pet_data = new Variant();

		protected Variant _drop_items = new Variant();

		private Variant _autoBuy;

		private Variant _tm_prompt_items = new Variant();

		private Variant _expireItems = new Variant();

		private Variant _needCheckPosConf = new Variant();

		private Action<Variant> _splitBack;

		private Variant _itmMerge;

		private Variant _posArr = new Variant();

		protected LGIUIItems _itemUI;

		private double _lastFreshTm = 0.0;

		private Variant _otherAucList = new Variant();

		protected Variant _smeltData;

		protected LGIUIItems _ui_items;

		protected LGIUIShop _ui_shop;

		private LGIUITempWarehouse _tempWarehouse;

		protected LGIUIMessageBox _ui_msgbox;

		private InGameItemMsgs igItemMsg
		{
			get
			{
				return (this.g_mgr.g_netM as muNetCleint).getObject("MSG_ITEM") as InGameItemMsgs;
			}
		}

		protected LGIUIMainUI mainui
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			}
		}

		protected LGIUIItems ui_items
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("LGUIItemImpl") as LGIUIItems;
			}
		}

		protected LGIUITempWarehouse tempWarehouse
		{
			get
			{
				bool flag = this._tempWarehouse == null;
				if (flag)
				{
					this._tempWarehouse = (this.g_mgr.g_uiM.getLGUI("LGUITempWarehouse") as LGIUITempWarehouse);
				}
				return this._tempWarehouse;
			}
		}

		protected LGIUIMessageBox ui_msgbox
		{
			get
			{
				bool flag = this._ui_msgbox == null;
				if (flag)
				{
					this._ui_msgbox = (this.g_mgr.g_uiM.getLGUI("msgbox") as LGIUIMessageBox);
				}
				return this._ui_msgbox;
			}
		}

		protected LGIUIShop ui_shop
		{
			get
			{
				bool flag = this._ui_shop == null;
				if (flag)
				{
					this._ui_shop = (this.g_mgr.g_uiM.getLGUI("mdlg_shop") as LGIUIShop);
				}
				return this._ui_shop;
			}
		}

		protected LGIUIWarehouse warehouse
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("mdlg_warehouse") as LGIUIWarehouse;
			}
		}

		protected LGIUIItems itmui
		{
			get
			{
				bool flag = this._itemUI != null;
				if (flag)
				{
					this._itemUI = ((this.g_mgr.g_uiM as muUIClient).getLGUI("items") as LGIUIItems);
				}
				return this._itemUI;
			}
		}

		public lgGDItems(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDItems(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListener(75u, new Action<GameEvent>(this.itemsChange));
			this.g_mgr.g_netM.addEventListener(201u, new Action<GameEvent>(this.rmvaucitmres));
			this.g_mgr.g_netM.addEventListener(202u, new Action<GameEvent>(this.buyaucitmres));
			this.g_mgr.g_netM.addEventListener(203u, new Action<GameEvent>(this.getplyauclistres));
			this.g_mgr.g_netM.addEventListener(204u, new Action<GameEvent>(this.fetchaucmoneyres));
			this.g_mgr.g_netM.addEventListener(205u, new Action<GameEvent>(this.getaucinfores));
			this.g_mgr.g_netM.addEventListener(33u, new Action<GameEvent>(this.transrepoitmres));
			this.g_mgr.g_netM.addEventListener(37u, new Action<GameEvent>(this.modpkgspcres));
			this.g_mgr.g_netM.addEventListener(64u, new Action<GameEvent>(this.buysolditemres));
			this.g_mgr.g_netM.addEventListener(67u, new Action<GameEvent>(this.combineitemres));
			this.g_mgr.g_netM.addEventListener(72u, new Action<GameEvent>(this.eqpforgeres));
			this.g_mgr.g_netM.addEventListener(73u, new Action<GameEvent>(this.embedstoneres));
			this.g_mgr.g_netM.addEventListener(74u, new Action<GameEvent>(this.removestoneres));
			this.g_mgr.g_netM.addEventListener(94u, new Action<GameEvent>(this.on_get_dbmkt_itm));
			this.g_mgr.g_netM.addEventListener(104u, new Action<GameEvent>(this.get_tips_item_data));
		}

		private void addaucitmres(GameEvent e)
		{
			Variant variant = new Variant();
			Variant data = e.data;
			variant._arr.Add(data["itmid"]);
			lgGDGeneral g_generalCT = (this.g_mgr.g_gameM as muLGClient).g_generalCT;
			bool flag = data.ContainsKey("gld_cost");
			if (flag)
			{
				g_generalCT.sub_gold(data["yb_cost"]._uint);
				this.set_gold((uint)g_generalCT.gold);
			}
			bool flag2 = data.ContainsKey("yb_cost");
			if (flag2)
			{
				g_generalCT.sub_yb(data["yb_cost"]._int, true);
				this.set_yb((uint)g_generalCT.yb);
			}
			bool flag3 = data["auctp"] == 0;
			if (flag3)
			{
				bool flag4 = data["readd"] != null;
				if (flag4)
				{
					this.pshop_updexpire_items(variant);
				}
				else
				{
					this.pshop_add_items(data);
				}
			}
		}

		private void rmvaucitmres(GameEvent e)
		{
			Variant data = e.data;
			Variant variant = new Variant();
			variant._arr.Add(data["itmid"]);
			bool flag = data["auctp"] != null;
			if (flag)
			{
				bool flag2 = data["auctp"]._int == 0;
				if (flag2)
				{
					Variant items = this.pshop_remove_items(variant);
					this.pkg_add_items(items, 0);
				}
			}
		}

		private void buyaucitmres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["auctp"] == 0;
			if (flag)
			{
				Variant variant = new Variant();
				variant._arr.Add(data["itmid"]);
				lgGDMarket g_marketCT = (this.g_mgr.g_gameM as muLGClient).g_marketCT;
				Variant variant2 = this.pshop_remove_items(variant);
				bool flag2 = variant2 == null;
				if (flag2)
				{
					variant2 = g_marketCT.rmvMarketItems(variant);
					g_marketCT.getMarketData(null);
				}
				bool flag3 = variant2 == null;
				if (flag3)
				{
					variant2 = this.GetFromOtherList(data["cid"], variant);
				}
				bool flag4 = variant2 == null;
				if (flag4)
				{
					variant2 = g_marketCT.rmvHadBuyItem(variant);
				}
				bool flag5 = variant2 != null;
				if (flag5)
				{
					this.pkg_add_items(variant2, 101);
				}
			}
			else
			{
				MUlgGDVendor g_vendorCT = (this.g_mgr.g_gameM as muLGClient).g_vendorCT;
				Variant aucItem = g_vendorCT.GetAucItem(data["itmid"]);
				bool flag6 = aucItem != null;
				if (flag6)
				{
					Variant variant3 = new Variant();
					variant3.pushBack(aucItem);
					this.pkg_add_items(variant3, 0);
				}
				g_vendorCT.BuyAucItemRes(data);
			}
			lgGDGeneral g_generalCT = (this.g_mgr.g_gameM as muLGClient).g_generalCT;
			bool flag7 = data.ContainsKey("gld_cost");
			if (flag7)
			{
				g_generalCT.sub_gold(data["gld_cost"]._uint);
				this.set_gold((uint)g_generalCT.gold);
			}
			bool flag8 = data.ContainsKey("yb_cost");
			if (flag8)
			{
				g_generalCT.sub_yb(data["yb_cost"]._int, false);
				this.set_yb((uint)g_generalCT.yb);
			}
		}

		private void getplyauclistres(GameEvent e)
		{
			Variant data = e.data;
			lgGDMarket g_marketCT = (this.g_mgr.g_gameM as muLGClient).g_marketCT;
			lgGDItems g_itemsCT = (this.g_mgr.g_gameM as muLGClient).g_itemsCT;
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			bool flag = data.ContainsKey("auctp") && data["auctp"]._int == 0;
			if (flag)
			{
				bool flag2 = data["cid"] != null;
				if (flag2)
				{
					bool flag3 = data["cid"] == mainPlayerInfo["cid"];
					if (flag3)
					{
						this.pshop_set_items(data);
						g_marketCT.setIncome(data["yb"]._uint);
					}
					this.GetPlyAucListRes(data);
				}
			}
			else
			{
				bool flag4 = data.ContainsKey("auctp") && data["auctp"] != null;
				if (flag4)
				{
					(this.g_mgr.g_gameM as muLGClient).g_vendorCT.GetPlyAuclistRes(data);
				}
				else
				{
					bool flag5 = !data.ContainsKey("cid");
					if (flag5)
					{
						g_marketCT.setMarketData(data);
					}
					else
					{
						bool flag6 = data["cid"]._str == mainPlayerInfo["cid"]._str;
						if (flag6)
						{
							g_itemsCT.pshop_set_items(data);
						}
					}
				}
			}
		}

		private void fetchaucmoneyres(GameEvent e)
		{
			Variant data = e.data;
			lgGDGeneral g_generalCT = (this.g_mgr.g_gameM as muLGClient).g_generalCT;
			bool flag = data["gld_add"];
			if (flag)
			{
				g_generalCT.add_gold(data["gld_add"]);
			}
			bool flag2 = data["yb_add"];
			if (flag2)
			{
				g_generalCT.add_yb(data["yb_add"]);
			}
			this.set_gold((uint)g_generalCT.gold);
			this.set_yb((uint)g_generalCT.yb);
			lgGDMarket g_marketCT = (this.g_mgr.g_gameM as muLGClient).g_marketCT;
			g_marketCT.setIncome(0u);
			(this.g_mgr.g_gameM as muLGClient).g_vendorCT.FetchMoneyRes();
		}

		private void getaucinfores(GameEvent e)
		{
			Variant data = e.data;
			bool flag = 1 == data["tp"];
			if (flag)
			{
				this.get_auc_info_res(data["itms"]);
			}
			else
			{
				bool flag2 = 2 == data["tp"];
				if (flag2)
				{
					(this.g_mgr.g_gameM as muLGClient).g_vendorCT.GetAucinfoRes(data);
				}
				else
				{
					bool flag3 = 3 == data["tp"];
					if (flag3)
					{
						lgGDGeneral g_generalCT = (this.g_mgr.g_gameM as muLGClient).g_generalCT;
						bool flag4 = data.ContainsKey("gld_bid");
						if (flag4)
						{
							g_generalCT.sub_gold(data["gld_bid"]._uint);
							this.set_gold((uint)g_generalCT.gold);
						}
						bool flag5 = data.ContainsKey("yb_bid");
						if (flag5)
						{
							g_generalCT.sub_yb(data["yb_bid"]._int, false);
							this.set_yb((uint)g_generalCT.yb);
						}
						(this.g_mgr.g_gameM as muLGClient).g_vendorCT.AddPriceRes(data);
					}
				}
			}
		}

		private void transrepoitmres(GameEvent e)
		{
			Variant data = e.data;
			uint val = data["item_id"];
			bool flag = data.ContainsKey("repotp") && data["repotp"] == 1;
			if (flag)
			{
				Variant items = this.temp_remove_items(GameTools.createArray(new Variant[]
				{
					val
				}));
				this.pkg_add_items(items, 101);
			}
			else
			{
				bool flag2 = data["direct"] == 0;
				if (flag2)
				{
					Variant items = this.repo_remove_items(GameTools.createArray(new Variant[]
					{
						val
					}));
					this.pkg_add_items(items, 0);
				}
				else
				{
					bool flag3 = data["direct"] == 1;
					if (flag3)
					{
						Variant items = this.pkg_remove_items(GameTools.createArray(new Variant[]
						{
							val
						}));
						this.repo_add_items(items);
					}
				}
			}
		}

		private void modpkgspcres(GameEvent e)
		{
			Variant data = e.data;
			uint @uint = data["pkg_tp"]._uint;
			uint uint2 = data["maxi_add"]._uint;
			bool flag = @uint == 0u;
			if (flag)
			{
				this.pkg_add_grid(uint2);
			}
			else
			{
				bool flag2 = @uint == 1u;
				if (flag2)
				{
					this.repo_add_grid(uint2);
				}
				else
				{
					bool flag3 = @uint == 2u;
					if (flag3)
					{
						this.pshop_add_grid(uint2);
					}
				}
			}
		}

		private void buyitemres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				bool flag2 = data.ContainsKey("gift_cid") && data["gift_cid"] > 0;
				if (flag2)
				{
				}
				bool flag3 = data.ContainsKey("hexp");
				if (flag3)
				{
					this.RefreshBuyHexpItms(data["itms"]);
				}
				bool flag4 = data.ContainsKey("itms");
				if (flag4)
				{
					this.pkg_add_items(data["itms"], 101);
				}
				bool flag5 = data.ContainsKey("clang") && data.ContainsKey("itms");
				if (flag5)
				{
					this.RefreshBuyDmktItms(data["itms"]);
				}
			}
		}

		private void sellitemres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				Variant items = this.pkg_remove_items(GameTools.createArray(new Variant[]
				{
					data["id"]
				}));
				this.sold_add_items(items);
			}
		}

		private void buysolditemres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				Variant items = this.sold_remove_items(GameTools.createArray(new Variant[]
				{
					data["id"]
				}));
				this.pkg_add_items(items, 0);
			}
		}

		private void splititemres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				Variant variant = data["to"];
				Variant variant2 = data["frm"];
				this.pkg_mod_item_cnt(variant2["id"], variant2["cnt"], 0);
				Variant variant3 = this.pkg_get_item_by_id(GameTools.createArray(new Variant[]
				{
					variant2["id"]
				}));
				Variant variant4 = new Variant();
				variant4["cnt"] = variant["cnt"];
				variant4["id"] = variant["id"];
				this.pkg_add_items(GameTools.createArray(new Variant[]
				{
					variant4
				}), 0);
			}
		}

		private void combineitemres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				Variant variant = data["to"];
				Variant variant2 = data["frm"];
				this.pkg_mod_item_cnt(variant["id"], variant["cnt"], 0);
				bool flag2 = variant2["left"]._int > 0;
				if (flag2)
				{
					this.pkg_mod_item_cnt(variant2["id"], variant2["left"], 0);
				}
				else
				{
					this.pkg_remove_items(GameTools.createArray(new Variant[]
					{
						variant2["id"]
					}));
				}
			}
		}

		private void useuitemres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				Variant variant = this.pkg_get_item_by_id(data["id"]);
				Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(variant["tpid"]);
				this.touch_cd(variant2["conf"]["cdtp"], (double)(data["cdtm"] / 10));
				bool flag2 = data["cnt"]._int > 0;
				if (flag2)
				{
					this.pkg_mod_item_cnt(data["id"], data["cnt"], 0);
				}
				else
				{
					this.pkg_remove_items(GameTools.createArray(new Variant[]
					{
						data["id"]
					}));
				}
				bool flag3 = data.ContainsKey("pkgs");
				if (flag3)
				{
					this.PkgsUseItem(data);
				}
			}
		}

		private void changeeqpres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				bool flag2 = data.ContainsKey("rmv");
				if (flag2)
				{
					Variant variant = new Variant();
					this.pkg_add_items(variant, 100);
				}
				bool flag3 = data.ContainsKey("add");
				if (flag3)
				{
					Variant variant = this.pkg_remove_items(GameTools.createArray(new Variant[]
					{
						data["add"]["id"]
					}));
					this.AddExprieItems(variant);
				}
			}
			else
			{
				bool flag4 = data["res"] == -1201;
				if (flag4)
				{
				}
			}
		}

		private void deleteitemres(GameEvent e)
		{
			Variant data = e.data;
			this.pkg_remove_items(GameTools.createArray(new Variant[]
			{
				data["id"]
			}));
		}

		private void eqpforgeres(GameEvent e)
		{
			Variant data = e.data;
			Variant variant = new Variant();
			switch (data["tp"]._int)
			{
			case 1:
			{
				bool flag = data.ContainsKey("succ");
				if (flag)
				{
					bool flag2 = !this.pkg_mod_item_data(data["id"], data["succ"]);
					if (flag2)
					{
					}
					variant[0] = LanguagePack.getLanguageText("smithy", "smithy_succ");
				}
				else
				{
					bool flag3 = data.ContainsKey("failed");
					if (flag3)
					{
						bool flag4 = !this.pkg_mod_item_data(data["id"], data["failed"]);
						if (flag4)
						{
						}
						variant[0] = LanguagePack.getLanguageText("smithy", "smithy_fail");
					}
				}
				break;
			}
			case 2:
			{
				bool flag5 = data.ContainsKey("succ");
				if (flag5)
				{
					bool flag6 = !this.pkg_mod_item_data(data["id"], data["succ"]);
					if (flag6)
					{
					}
					variant[0] = LanguagePack.getLanguageText("smithy", "smithy_succ");
				}
				else
				{
					bool flag7 = data.ContainsKey("failed");
					if (flag7)
					{
						bool flag8 = !this.pkg_mod_item_data(data["id"], data["failed"]);
						if (flag8)
						{
						}
						variant[0] = LanguagePack.getLanguageText("smithy", "smithy_fail");
					}
				}
				break;
			}
			case 3:
			{
				bool flag9 = data.ContainsKey("succ");
				if (flag9)
				{
					bool flag10 = !this.pkg_mod_item_data(data["id"], data["succ"]);
					if (flag10)
					{
					}
					variant[0] = LanguagePack.getLanguageText("smithy", "smithy_succ");
				}
				else
				{
					bool flag11 = data.ContainsKey("failed");
					if (flag11)
					{
						bool flag12 = !this.pkg_mod_item_data(data["id"], data["failed"]);
						if (flag12)
						{
						}
						variant[0] = LanguagePack.getLanguageText("smithy", "smithy_fail");
					}
				}
				break;
			}
			case 4:
			{
				bool flag13 = !this.pkg_mod_item_data(data["id"], data);
				if (flag13)
				{
				}
				break;
			}
			case 7:
				this.ResFlagexEqp(data);
				break;
			case 9:
			{
				bool flag14 = data.ContainsKey("succ");
				if (flag14)
				{
					bool flag15 = !this.pkg_mod_item_data(data["id"], data["succ"]);
					if (flag15)
					{
					}
					variant[0] = LanguagePack.getLanguageText("smithy", "smithy_succ");
				}
				else
				{
					bool flag16 = data.ContainsKey("failed");
					if (flag16)
					{
						bool flag17 = !this.pkg_mod_item_data(data["id"], data["failed"]);
						if (flag17)
						{
						}
						variant[0] = LanguagePack.getLanguageText("smithy", "smithy_fail");
					}
				}
				break;
			}
			}
			this.ForgeBack(data["tp"], data.ContainsKey("succ"));
		}

		private void embedstoneres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				this.embed_stone_res(data);
			}
		}

		private void removestoneres(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"] == 1;
			if (flag)
			{
				this.remove_stone_res(data);
			}
		}

		private void itemdroped(GameEvent e)
		{
		}

		public Variant GetPkgItems()
		{
			return this._pkg_items;
		}

		public Variant GetEqp()
		{
			return this._pkg_items["eqp"];
		}

		public int GetItemCount(uint tpid)
		{
			int num = 0;
			bool flag = this._pkg_items != null;
			if (flag)
			{
				foreach (Variant current in this._pkg_items["ci"]._arr)
				{
					bool flag2 = current != null && tpid == current["tpid"];
					if (flag2)
					{
						num += (current.ContainsKey("cnt") ? current["cnt"]._int : 1);
					}
				}
				foreach (Variant current2 in this._pkg_items["nci"]._arr)
				{
					bool flag3 = current2 != null && tpid == current2["tpid"];
					if (flag3)
					{
						num += (current2.ContainsKey("cnt") ? current2["cnt"]._int : 1);
					}
				}
				foreach (Variant current3 in this._pkg_items["eqp"]._arr)
				{
					bool flag4 = current3 != null && tpid == current3["tpid"];
					if (flag4)
					{
						num += (current3.ContainsKey("cnt") ? current3["cnt"]._int : 1);
					}
				}
			}
			return num;
		}

		public Variant GetItemDataBytpid(uint tpid)
		{
			bool flag = this._pkg_items != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in this._pkg_items["ci"]._arr)
				{
					bool flag2 = current != null && tpid == current["tpid"];
					if (flag2)
					{
						result = current;
						return result;
					}
				}
				foreach (Variant current2 in this._pkg_items["nci"]._arr)
				{
					bool flag3 = current2 != null && tpid == current2["tpid"];
					if (flag3)
					{
						result = current2;
						return result;
					}
				}
				foreach (Variant current3 in this._pkg_items["eqp"]._arr)
				{
					bool flag4 = current3 != null && tpid == current3["tpid"];
					if (flag4)
					{
						result = current3;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetAllItemDataBytpid(uint tpid)
		{
			bool flag = this._pkg_items != null;
			Variant result;
			if (flag)
			{
				Variant variant = new Variant();
				foreach (Variant current in this._pkg_items["ci"].Values)
				{
					bool flag2 = current != null && tpid == current["tpid"];
					if (flag2)
					{
						variant._arr.Add(current);
					}
				}
				foreach (Variant current2 in this._pkg_items["nci"].Values)
				{
					bool flag3 = current2 != null && tpid == current2["tpid"];
					if (flag3)
					{
						variant._arr.Add(current2);
					}
				}
				foreach (Variant current3 in this._pkg_items["eqp"].Values)
				{
					bool flag4 = current3 != null && tpid == current3["tpid"];
					if (flag4)
					{
						variant._arr.Add(current3);
					}
				}
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public uint pkg_get_item_count_bytpid(uint tpid)
		{
			bool flag = this._pkg_items == null;
			uint result;
			if (flag)
			{
				result = 0u;
			}
			else
			{
				uint num = 0u;
				bool flag2 = this._pkg_items.ContainsKey("ci");
				if (flag2)
				{
					for (int i = 0; i < this._pkg_items["ci"].Count; i++)
					{
						Variant variant = this._pkg_items["ci"][i];
						bool flag3 = variant["tpid"] == tpid;
						if (flag3)
						{
							num += variant["cnt"]._uint;
						}
					}
				}
				bool flag4 = this._pkg_items.ContainsKey("nci");
				if (flag4)
				{
					for (int i = 0; i < this._pkg_items["nci"].Count; i++)
					{
						Variant variant = this._pkg_items["nci"][i];
						bool flag5 = variant["tpid"] == tpid;
						if (flag5)
						{
							num += variant["cnt"]._uint;
						}
					}
				}
				bool flag6 = this._pkg_items.ContainsKey("eqp");
				if (flag6)
				{
					for (int i = 0; i < this._pkg_items["eqp"].Count; i++)
					{
						Variant variant = this._pkg_items["eqp"][i];
						bool flag7 = variant["tpid"] == tpid;
						if (flag7)
						{
							num += variant["cnt"]._uint;
						}
					}
				}
				result = num;
			}
			return result;
		}

		public Variant GetItemDataById(uint id)
		{
			bool flag = this._pkg_items != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in this._pkg_items["ci"].Values)
				{
					bool flag2 = current != null && id == current["id"];
					if (flag2)
					{
						result = current;
						return result;
					}
				}
				foreach (Variant current2 in this._pkg_items["nci"].Values)
				{
					bool flag3 = current2 != null && id == current2["id"];
					if (flag3)
					{
						result = current2;
						return result;
					}
				}
				foreach (Variant current3 in this._pkg_items["eqp"].Values)
				{
					bool flag4 = current3 != null && id == current3["id"];
					if (flag4)
					{
						result = current3;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetSelfEquipById(uint id)
		{
			Variant mainPlayerInfo = this.m_player.mainPlayerInfo;
			Variant variant = mainPlayerInfo["equip"];
			Variant result;
			foreach (Variant current in variant.Values)
			{
				bool flag = current["id"] == id;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetItemById(uint id)
		{
			Variant variant = this.GetItemDataById(id);
			bool flag = variant == null;
			if (flag)
			{
				variant = this.GetSelfEquipById(id);
			}
			return variant;
		}

		public Variant get_item_by_Features(string Features, uint tp = 0u, string features2 = "", uint tpid = 0u)
		{
			Variant variant = this._pkg_items["ci"];
			Variant variant2 = this._pkg_items["nci"];
			for (int i = 0; i < variant2.Count; i++)
			{
				variant[i + variant.Count] = variant2[i];
			}
			Variant variant3 = variant;
			Variant result;
			for (int j = 0; j < variant3.Count; j++)
			{
				Variant variant4 = variant3[j];
				Variant variant5 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(variant4["tpid"]._uint);
				bool flag = tpid != 0u && variant4["tpid"]._uint != tpid;
				if (!flag)
				{
					bool flag2 = variant5 == null;
					if (!flag2)
					{
						bool flag3 = tp != 0u && variant5["tp"]._uint != tp;
						if (!flag3)
						{
							bool flag4 = variant5["conf"].ContainsKey("Features");
							if (flag4)
							{
								bool flag5 = features2 != "" && !variant5["conf"].ContainsKey("features2");
								if (!flag5)
								{
									result = variant4;
									return result;
								}
							}
						}
					}
				}
			}
			result = null;
			return result;
		}

		public int get_empty_grids()
		{
			return (int)(this._pkg_maxi - (uint)this.pkg_occupy_grid());
		}

		public bool UseTransItemCheck(int tpid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf((uint)tpid);
			bool flag = variant == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = variant["conf"].ContainsKey("trans");
				if (flag2)
				{
				}
				result = true;
			}
			return result;
		}

		public bool useitem(uint tpid)
		{
			Variant itemDataBytpid = this.GetItemDataBytpid(tpid);
			bool flag = itemDataBytpid == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.UseItemIsCd(tpid);
				if (flag2)
				{
					result = false;
				}
				else
				{
					Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(tpid);
					this._realUseItem(itemDataBytpid["id"], tpid, 0u);
					result = true;
				}
			}
			return result;
		}

		public int BuyItem(int tpid, uint cnt, bool flag)
		{
			bool flag2 = (this.g_mgr.g_gameM as muLGClient).g_levelsCT.IsInBattleSrvLvl();
			int result;
			if (flag2)
			{
				string languageText = LanguagePack.getLanguageText("crosswar", "no_buyitem");
				result = 0;
			}
			else
			{
				this._autoBuy = new Variant();
				Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf((uint)tpid);
				Variant variant2 = variant["conf"];
				double num = Math.Ceiling(cnt / variant2["mul"]._double);
				bool flag3 = num > (double)this.get_empty_grids();
				if (flag3)
				{
					result = this.toShowHint(flag, tpid);
				}
				else
				{
					int npcid = 0;
					bool flag4 = variant2.ContainsKey("yb");
					if (flag4)
					{
						bool flag5 = (long)(this.g_mgr as muLGClient).g_generalCT.yb < (long)variant2["yb"]._int * (long)((ulong)cnt);
						if (flag5)
						{
							result = this.toShowHint(flag, tpid);
							return result;
						}
						this._autoBuy["yb"] = variant2["yb"] * cnt;
						npcid = 0;
					}
					else
					{
						bool flag6 = variant2.ContainsKey("gold");
						if (flag6)
						{
							bool flag7 = (long)(this.g_mgr as muLGClient).g_generalCT.gold < (long)variant2["gold"]._int * (long)((ulong)cnt);
							if (flag7)
							{
								result = this.toShowHint(flag, tpid);
								return result;
							}
							this._autoBuy["gold"] = variant2["gold"] * cnt;
							npcid = 1;
						}
					}
					this._autoBuy["tpid"] = tpid;
					this._autoBuy["cnt"] = cnt;
					this.igItemMsg.buy_item((uint)npcid, (uint)tpid, cnt, 1u, -1);
					result = 0;
				}
			}
			return result;
		}

		public bool UseItemIsCd(uint tpid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(tpid);
			bool flag = variant == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant itemCDData = this.GetItemCDData(variant["conf"].ContainsKey("") ? variant["conf"]["cdtp"]._uint : 0u);
				double num = (double)((this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L);
				bool flag2 = itemCDData != null;
				if (flag2)
				{
					double num2 = (num - itemCDData["start_tm"]._uint) / itemCDData["cd_tm"]._uint;
					bool flag3 = num2 < 1.0;
					if (flag3)
					{
						result = true;
						return result;
					}
				}
				result = false;
			}
			return result;
		}

		public double GetCdEndTm(int tpid)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf((uint)tpid);
			Variant itemCDData = this.GetItemCDData(variant["conf"]["cdtp"]._uint);
			bool flag = itemCDData;
			double result;
			if (flag)
			{
				result = itemCDData["start_tm"]._double + itemCDData["cd_tm"]._double;
			}
			else
			{
				result = 0.0;
			}
			return result;
		}

		public Variant pkg_remove_items(Variant ids)
		{
			Variant variant = new Variant();
			this.remove_item(ids);
			foreach (Variant current in ids._arr)
			{
				uint @uint = current._uint;
				variant.pushBack(this._remove_item(this._pkg_items, @uint));
			}
			this.ui_items.pkg_rmv_items(ids);
			return variant;
		}

		public void pkg_mod_item_cnt(uint id, uint cnt, int flag = 0)
		{
			Variant variant = this.pkg_get_item_by_id(id);
			bool flag2 = variant != null;
			if (flag2)
			{
				variant["cnt"] = cnt;
			}
			this.ui_items.pkg_mod_item_data(id, variant);
		}

		public void pkg_mod_item_att(uint id, uint flvl, uint fp)
		{
			Variant variant = this.pkg_get_item_by_id(id);
			bool flag = variant == null;
			if (!flag)
			{
				variant["flvl"] = flvl;
				variant["fp"] = fp;
				this.ui_items.pkg_mod_item_data(id, variant);
			}
		}

		public bool SetItemStn(Variant stn, uint id)
		{
			Variant itemById = this.GetItemById(id);
			bool flag = itemById != null;
			bool result;
			if (flag)
			{
				itemById["stn"] = stn;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool pkg_mod_item_data(uint id, Variant data)
		{
			Variant variant = this.pkg_get_item_by_id(id);
			bool flag = variant == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				variant.mergeFrom(data);
				this.ui_items.pkg_mod_item_data(id, variant);
				bool flag2 = data.ContainsKey("flag") && 12 == data["flag"];
				if (flag2)
				{
					this.ui_items.UseMlineAward();
				}
				result = true;
			}
			return result;
		}

		public uint pkg_get_grid()
		{
			return this._pkg_maxi;
		}

		public int pkg_occupy_grid()
		{
			int num = 0;
			List<Variant> arr = this._pkg_items["ci"]._arr;
			bool flag = arr != null;
			if (flag)
			{
				num += arr.Count;
			}
			arr = this._pkg_items["nci"]._arr;
			bool flag2 = arr != null;
			if (flag2)
			{
				num += arr.Count;
			}
			arr = this._pkg_items["eqp"]._arr;
			bool flag3 = arr != null;
			if (flag3)
			{
				num += arr.Count;
			}
			return num;
		}

		public Variant pkg_get_item_by_id(uint id)
		{
			bool flag = this._pkg_items == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this._pkg_items.ContainsKey("ci");
				if (flag2)
				{
					for (int i = 0; i < this._pkg_items["ci"].Count; i++)
					{
						Variant variant = this._pkg_items["ci"][i];
						bool flag3 = variant["id"] == id;
						if (flag3)
						{
							result = variant;
							return result;
						}
					}
				}
				bool flag4 = this._pkg_items.ContainsKey("nci");
				if (flag4)
				{
					for (int i = 0; i < this._pkg_items["nci"].Count; i++)
					{
						Variant variant = this._pkg_items["nci"][i];
						bool flag5 = variant["id"] == id;
						if (flag5)
						{
							result = variant;
							return result;
						}
					}
				}
				bool flag6 = this._pkg_items.ContainsKey("eqp");
				if (flag6)
				{
					for (int i = 0; i < this._pkg_items["eqp"].Count; i++)
					{
						Variant variant = this._pkg_items["eqp"][i];
						bool flag7 = variant["id"] == id;
						if (flag7)
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

		public uint pshop_get_grid()
		{
			return this._pshop_maxi;
		}

		public Variant GetFromOtherList(int cid, Variant itmids)
		{
			Variant variant = this._otherAucList[cid];
			Variant variant2 = new Variant();
			bool flag = variant;
			if (flag)
			{
				Variant variant3 = variant["itms"];
				foreach (int current in itmids.IntKeys)
				{
					foreach (Variant current2 in variant3.Values)
					{
						bool flag2 = current == current2["itm"]["id"];
						if (flag2)
						{
							variant2.pushBack(current2["itm"]);
							break;
						}
					}
				}
			}
			bool flag3 = variant2.Length == 0;
			if (flag3)
			{
				variant2 = null;
			}
			return variant2;
		}

		public Variant get_dbmkt_itm()
		{
			return this._dbmkt_items;
		}

		public Variant pshop_get_items()
		{
			return this._pshop_items;
		}

		public Variant GetItemCDData(uint cdType)
		{
			bool flag = this._item_cds == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = this._item_cds[cdType];
				bool flag2 = variant != null;
				if (flag2)
				{
					result = variant;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public Variant getMergeInfoReArr()
		{
			bool flag = this._mergeInfo == null;
			if (flag)
			{
			}
			return this._mergeInfo;
		}

		public bool check_eqp_useless(Variant eqpData)
		{
			bool flag = eqpData == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(eqpData["tpid"]._uint);
				bool flag2 = variant == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = eqpData.ContainsKey("dura");
					if (flag3)
					{
						bool flag4 = eqpData["dura"] == 0 && variant["conf"]["dura"] > 0;
						if (flag4)
						{
							result = false;
							return result;
						}
					}
					bool flag5 = variant["conf"].ContainsKey("attchk");
					if (flag5)
					{
						Variant value = this.get_attchkData(eqpData, variant["conf"]["attchk"]);
						Variant variant2 = new Variant();
						variant2["attchk"] = value;
					}
					result = true;
				}
			}
			return result;
		}

		public bool CheckTimeOut(Variant item)
		{
			bool flag = item.ContainsKey("expire") && item["expire"] > 0;
			bool result;
			if (flag)
			{
				double num = (double)((this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L);
				bool flag2 = item["expire"] < num;
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			result = true;
			return result;
		}

		public Variant get_attchkData(Variant data, Variant attchk)
		{
			return null;
		}

		public int att_need(int val, Variant adjust, string type)
		{
			int num = 0;
			bool flag = adjust[type];
			int result;
			if (flag)
			{
				bool flag2 = adjust[type]["mul"] > 0 || adjust[type]["add"] > 0;
				if (flag2)
				{
					num = val * adjust[type]["mul"] / 100 + adjust[type]["add"];
				}
				result = num;
			}
			else
			{
				result = val;
			}
			return result;
		}

		public Variant get_adjustData(Variant data, Variant conf = null)
		{
			Variant attchk_adjust = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_attchk_adjust();
			bool flag = attchk_adjust == null || data == null;
			Variant result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				Variant variant = new Variant();
				Variant variant2 = null;
				bool flag2 = conf == null;
				if (flag2)
				{
					Variant variant3 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(data["tpid"]._uint);
					conf = variant3["conf"];
				}
				bool flag3 = conf.ContainsKey("lv") && attchk_adjust.ContainsKey("lv");
				if (flag3)
				{
					foreach (Variant current in attchk_adjust["lv"].Values)
					{
						bool flag4 = variant.ContainsKey(current["name"]._str);
						if (flag4)
						{
							variant2 = variant[current["name"]];
						}
						else
						{
							variant2["mul"] = 0;
							variant2["add"] = 0;
							variant[current["name"]] = variant2;
						}
						bool flag5 = current.ContainsKey("mul");
						if (flag5)
						{
							Variant variant4 = variant[current["name"]];
							variant4["mul"] = variant4["mul"] + current["mul"] * conf["lv"];
						}
						bool flag6 = current.ContainsKey("add");
						if (flag6)
						{
							Variant variant4 = variant[current["name"]];
							variant4["add"] = variant4["add"] + current["add"]._int;
						}
					}
				}
				bool flag7 = data.ContainsKey("flvl") && data["flvl"] > 0;
				if (flag7)
				{
					bool flag8 = attchk_adjust.ContainsKey("flvl");
					if (flag8)
					{
						foreach (Variant current2 in attchk_adjust["flvl"].Values)
						{
							bool flag9 = variant.ContainsKey(current2["name"]._str);
							if (flag9)
							{
								variant2 = variant[current2["name"]];
							}
							else
							{
								variant2["mul"] = 0;
								variant2["add"] = 0;
								variant[current2["name"]] = variant2;
							}
							bool flag10 = current2.ContainsKey("mul");
							if (flag10)
							{
								Variant variant4 = variant[current2["name"]];
								variant4["mul"] = variant4["mul"] + current2["mul"] * data["flvl"];
							}
							bool flag11 = current2.ContainsKey("add");
							if (flag11)
							{
								Variant variant4 = variant[current2["name"]];
								variant4["add"] = variant4["add"] + current2["add"]._int;
							}
						}
					}
				}
				bool flag12 = attchk_adjust.ContainsKey("exatt");
				if (flag12)
				{
					foreach (Variant current3 in attchk_adjust["exatt"].Values)
					{
						bool flag13 = variant.ContainsKey(current3["name"]._int);
						if (flag13)
						{
							variant2 = variant[current3["name"]];
						}
						else
						{
							variant2["mul"] = 0;
							variant2["add"] = 0;
							variant[current3["name"]] = variant2;
						}
						bool flag14 = current3.ContainsKey("mul");
						if (flag14)
						{
							Variant variant4 = variant[current3["name"]];
							variant4["mul"] = variant4["mul"] + current3["mul"]._int;
						}
						bool flag15 = current3.ContainsKey("add");
						if (flag15)
						{
							Variant variant4 = variant[current3["name"]];
							variant4["add"] = variant4["add"] + current3["add"]._int;
						}
					}
				}
				bool flag16 = data.ContainsKey("fpt") && data["fpt"] > 0;
				if (flag16)
				{
					bool flag17 = attchk_adjust.ContainsKey("supfrg");
					if (flag17)
					{
						Variant variant5 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.DecodeSupfrgatt(data["fpt"]);
						foreach (Variant current4 in variant5.Values)
						{
							int idx = current4["id"];
							int fptlvl = current4["lvl"];
							Variant sup_frg_att = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_sup_frg_att();
							bool flag18 = sup_frg_att[idx];
							if (flag18)
							{
								Variant variant6 = sup_frg_att[idx];
								foreach (Variant current5 in attchk_adjust["supfrg"].Values)
								{
									bool flag19 = variant6["att"][0]["name"] != current5["supatt"];
									if (!flag19)
									{
										bool flag20 = variant.ContainsKey(current5["name"]._str);
										if (flag20)
										{
											variant2 = variant[current5["name"]];
										}
										else
										{
											variant2["mul"] = 0;
											variant2["add"] = 0;
											variant[current5["name"]] = variant2;
										}
										Variant variant4 = variant[current5["name"]];
										variant4["add"] = variant4["add"] + (variant6["att"][0]["val"] + this.get_sup_frg_add(current5["supatt"], fptlvl));
									}
								}
							}
						}
					}
				}
				result = variant;
			}
			return result;
		}

		public uint count_exatt(Variant data)
		{
			uint num = data["veriex_cnt"];
			for (uint num2 = 32768u; num2 > 0u; num2 /= 2u)
			{
				bool flag = (data["exatt"] & num2) == num2;
				if (flag)
				{
					num += 1u;
				}
			}
			return num;
		}

		public int get_sup_frg_add(string name, int fptlvl)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_sup_frg_att_lvl_by_id(name);
			int result = 0;
			bool flag = variant != null;
			if (flag)
			{
				bool flag2 = fptlvl < variant.Count;
				if (flag2)
				{
					bool flag3 = variant[fptlvl];
					if (flag3)
					{
						result = variant[fptlvl]["add"];
					}
				}
			}
			return result;
		}

		public bool check_state(Variant data, Variant attchk)
		{
			return true;
		}

		public Variant get_drop_items()
		{
			return this._drop_items;
		}

		public bool CheckSuperposition(int tpid, int cnt = 1)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf((uint)tpid);
			bool flag = variant && variant["conf"]["mul"];
			bool result;
			if (flag)
			{
				int num = variant["conf"]["mul"];
				Variant variant2 = this._pkg_items["ci"];
				foreach (Variant current in variant2.Values)
				{
					bool flag2 = tpid == current["tpid"] && num >= current["cnt"] + cnt;
					if (flag2)
					{
						result = true;
						return result;
					}
				}
				Variant variant3 = this._pkg_items["nci"];
				foreach (Variant current2 in variant3.Values)
				{
					bool flag3 = tpid == current2["tpid"] && num >= current2["cnt"] + cnt;
					if (flag3)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public bool is_can_pick(Variant item)
		{
			return item["pickFlag"] == 0;
		}

		public bool pick_drop_item(uint id, uint pick_range = 0u)
		{
			Variant variant = this.get_drop_item_by_dpid(id);
			bool flag = variant != null;
			bool result;
			if (flag)
			{
				bool flag2 = !this.is_can_pick(variant);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = pick_range > 0u;
					if (flag3)
					{
						int value = variant["x"] - this.m_player.mainPlayerInfo["x"];
						int value2 = variant["y"] - this.m_player.mainPlayerInfo["y"];
						bool flag4 = (long)Math.Abs(value) >= (long)((ulong)pick_range) || (long)Math.Abs(value2) >= (long)((ulong)pick_range);
						if (flag4)
						{
							result = false;
							return result;
						}
					}
					bool flag5 = variant.ContainsKey("gold") && variant["gold"] > 0;
					if (flag5)
					{
						this.change_pick_flag(variant, 2, true);
					}
					this._playPickSound(variant);
					this.igItemMsg.pick_dpitem(id);
					result = true;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		public Variant get_drop_item_by_dpid(uint dpid)
		{
			Variant result;
			foreach (Variant current in this._drop_items.Values)
			{
				bool flag = dpid == current["dpid"];
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant FindNearDropItems(uint range, int maxCnt, Action filter = null)
		{
			Variant result = new Variant();
			bool flag = this._drop_items.Count > 0;
			if (flag)
			{
				for (int i = this._drop_items.Count; i > 0; i--)
				{
					Variant variant = this._drop_items[i - 1];
					bool flag2 = !variant["mapObj"];
					if (!flag2)
					{
						bool flag3 = !this.is_can_pick(variant);
						if (flag3)
						{
						}
					}
				}
			}
			return result;
		}

		public Variant get_need_rpdura_item()
		{
			Variant variant = new Variant();
			Variant variant2 = null;
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			bool flag = mainPlayerInfo != null;
			if (flag)
			{
				variant2 = mainPlayerInfo["equip"];
			}
			bool flag2 = variant2 == null;
			Variant result;
			if (flag2)
			{
				result = variant2;
			}
			else
			{
				for (int i = 0; i < variant2.Count; i++)
				{
					Variant variant3 = variant2[i];
					Variant variant4 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(variant3["tpid"]._uint);
					bool flag3 = variant4 == null || variant4["conf"].ContainsKey("cant_repair");
					if (!flag3)
					{
						bool flag4 = !variant4["conf"].ContainsKey("dura");
						if (!flag4)
						{
							bool flag5 = variant3["dura"] == variant4["conf"]["dura"];
							if (!flag5)
							{
								variant._arr.Add(variant3);
							}
						}
					}
				}
				result = variant;
			}
			return result;
		}

		public float GetRepaireCostMoney()
		{
			float num = 0f;
			Variant variant = new Variant();
			Variant need_rpdura_item = this.get_need_rpdura_item();
			bool flag = need_rpdura_item == null || need_rpdura_item.Count == 0;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float num2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.get_game_general_data("dura_repaire_per");
				for (int i = 0; i < need_rpdura_item.Count; i++)
				{
					Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(need_rpdura_item[i]["tpid"]._uint);
					uint @uint = variant2["conf"]["qual"]._uint;
					uint num3 = 1u;
					bool flag2 = variant2["conf"]["lv"]._uint > 1u;
					if (flag2)
					{
						num3 = variant2["conf"]["lv"]._uint;
					}
					uint num4 = variant2["conf"]["dura"];
					bool flag3 = num4 == 0u;
					uint num5;
					if (flag3)
					{
						num5 = 0u;
					}
					else
					{
						num5 = num4 - need_rpdura_item[i]["dura"];
					}
					num += num3 * @uint * @uint * num5 * num2 / 100f;
				}
				result = num;
			}
			return result;
		}

		public Variant get_show_item_data(int id)
		{
			return this._show_item_data[id];
		}

		public void BuyItemNormal(uint npcid, uint tpid, uint cnt, uint mktp, int gift_cid = -1)
		{
			bool flag = (this.g_mgr.g_gameM as muLGClient).g_levelsCT.IsInBattleSrvLvl();
			if (flag)
			{
				string languageText = LanguagePack.getLanguageText("crosswar", "no_buyitem");
			}
			else
			{
				this.igItemMsg.buy_item(npcid, tpid, cnt, mktp, gift_cid);
			}
		}

		public void UseItemById(int itemId, uint itemTpid, uint idx = 0u)
		{
			bool flag = this.UseItemIsCd(itemTpid);
			if (!flag)
			{
				this._realUseItem(itemId, itemTpid, idx);
			}
		}

		public void pkg_get_items()
		{
			bool flag = this._pkg_items == null;
			if (flag)
			{
				this.igItemMsg.get_items(0u);
			}
			else
			{
				this.pkg_set_items(this._pkg_items);
			}
		}

		public void pkg_add_items(Variant items, int flag = 0)
		{
			bool flag2 = this._pkg_items == null || items.Count == 0;
			if (!flag2)
			{
				this.AddExprieItems(items);
				foreach (Variant current in items._arr)
				{
					current["pick"] = true;
					Variant variant = this._get_item_data_array(this._pkg_items, current["tpid"]._uint);
					bool flag3 = variant != null;
					if (flag3)
					{
						variant._arr.Add(current);
					}
				}
				this.ui_items.pkg_add_items(items, flag);
				this.ui_items.SetLotteryFirst(items, flag, delegate(Variant data, int type)
				{
					this.mainui.pkg_add_items(data, type);
				});
				this.ui_msgbox.pkg_add_items(items);
				bool flag4 = (long)flag == 101L;
				if (flag4)
				{
					foreach (Variant current2 in items._arr)
					{
						bool flag5 = this._autoBuy != null && current2["tpid"] == this._autoBuy["tpid"];
						if (flag5)
						{
							this.ui_items.ShowBuyItemRemind(true, this._autoBuy);
							this._autoBuy = null;
						}
					}
				}
				bool flag6 = this._splitBack != null;
				if (flag6)
				{
					this._splitBack(items);
					this._splitBack = null;
				}
			}
		}

		public void ForgeBack(int type, bool succ)
		{
		}

		public void repo_set_items(Variant items)
		{
			this._repo_items = items;
			this._repo_maxi = items["maxi"];
			this.warehouse.repo_set_items(this._repo_items);
			this.warehouse.repo_set_grid((int)this._repo_maxi);
		}

		public void repo_get_items()
		{
			bool flag = this._repo_items == null;
			if (flag)
			{
				this.igItemMsg.get_items(2u);
			}
			else
			{
				this.repo_set_items(this._repo_items);
			}
		}

		public void repo_add_items(Variant items)
		{
			bool flag = this._repo_items == null;
			if (!flag)
			{
				for (int i = 0; i < items.Count; i++)
				{
					Variant variant = items[i];
					Variant variant2 = this._get_item_data_array(this._repo_items, variant["tpid"]);
					variant2._arr.Add(variant);
				}
				this.warehouse.repo_add_items(items);
			}
		}

		public void pkg_set_items(Variant items)
		{
			bool flag = items.ContainsKey("itemArr");
			if (flag)
			{
				foreach (Variant current in items["itemArr"]._arr)
				{
					bool isArr = current.isArr;
					if (isArr)
					{
						foreach (Variant current2 in current["item"]._arr)
						{
							current2["pick"] = true;
						}
					}
				}
			}
			this._pkg_items = items;
			this._pkg_maxi = items["maxi"];
			Variant items2 = this._getPkgItems(this._pkg_items);
			this.ui_items.pkg_set_grid(this._pkg_maxi);
			this.ui_items.pkg_set_items(items2);
			this.mainui.pkg_set_items(items2);
		}

		public Variant repo_remove_items(Variant ids)
		{
			Variant variant = new Variant();
			for (int i = 0; i < ids.Count; i++)
			{
				uint id = ids[i];
				variant._arr.Add(this._remove_item(this._repo_items, id));
			}
			this.warehouse.repo_rmv_items(ids);
			return variant;
		}

		public Variant _get_items_pos(uint pos)
		{
			Variant variant = new Variant();
			bool flag = this._pkg_items != null;
			if (flag)
			{
				foreach (Variant current in this._pkg_items["ci"]._arr)
				{
					Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]);
					bool flag2 = variant2 != null && variant2["conf"].ContainsKey("pos") && pos == variant2["conf"]["pos"];
					if (flag2)
					{
						variant._arr.Add(current);
					}
				}
				foreach (Variant current2 in this._pkg_items["nci"]._arr)
				{
					Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current2["tpid"]);
					bool flag3 = variant2 != null && variant2["conf"].ContainsKey("pos") && pos == variant2["conf"]["pos"];
					if (flag3)
					{
						variant._arr.Add(current2);
					}
				}
				foreach (Variant current3 in this._pkg_items["eqp"]._arr)
				{
					Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current3["tpid"]);
					bool flag4 = variant2 != null && variant2["conf"].ContainsKey("pos") && pos == variant2["conf"]["pos"];
					if (flag4)
					{
						variant._arr.Add(current3);
					}
				}
			}
			return variant;
		}

		public void repo_mod_item_cnt(uint id, uint cnt)
		{
			Variant variant = null;
			Variant variant2 = this._repo_items["itm"];
			for (int i = 0; i < variant2.Length; i++)
			{
				variant = variant2[i];
				bool flag = (long)variant["id"]._int == (long)((ulong)id);
				if (flag)
				{
					variant["cnt"] = cnt;
					break;
				}
			}
			this.warehouse.repo_mod_item_data((int)id, variant);
		}

		public void repo_add_grid(uint count)
		{
			this._repo_maxi += count;
			this.warehouse.repo_set_grid((int)this._repo_maxi);
		}

		public void temp_set_items(Variant items)
		{
			this._temp_items = items;
			this.tempWarehouse.temp_set_items(this._temp_items);
		}

		public void temp_get_items()
		{
			bool flag = this._temp_items == null;
			if (flag)
			{
				this.igItemMsg.get_items(3u);
			}
			else
			{
				this.temp_set_items(this._temp_items);
			}
		}

		public void temp_add_items(Variant items)
		{
			bool flag = this._temp_items == null;
			if (!flag)
			{
				for (int i = 0; i < items.Count; i++)
				{
					Variant variant = items[i];
					Variant variant2 = this._get_item_data_array(this._temp_items, variant["tpid"]._uint);
					variant2._arr.Add(variant);
				}
				this.tempWarehouse.temp_add_items(items);
			}
		}

		public Variant temp_remove_items(Variant ids)
		{
			Variant variant = new Variant();
			for (int i = 0; i < ids.Length; i++)
			{
				uint id = ids[i];
				variant.pushBack(this._remove_item(this._temp_items, id));
			}
			this.tempWarehouse.temp_rmv_items(ids);
			return variant;
		}

		public void on_package_full()
		{
			this.tempWarehouse.PackageFull();
		}

		public void temp_mod_item_cnt(uint id, uint cnt)
		{
			Variant variant = new Variant();
			Variant variant2 = this._temp_items["itm"];
			for (int i = 0; i < variant2.Length; i++)
			{
				variant = variant2[i];
				bool flag = (long)variant["id"]._int == (long)((ulong)id);
				if (flag)
				{
					variant["cnt"] = cnt;
					break;
				}
			}
			this.tempWarehouse.temp_mod_item_data(id, variant);
		}

		public void sold_set_items(Variant items)
		{
		}

		public void sold_get_items()
		{
			bool flag = this._sold_items == null;
			if (flag)
			{
				this.igItemMsg.get_items(1u);
			}
			else
			{
				this.sold_set_items(this._sold_items);
			}
		}

		public void sold_add_items(Variant items)
		{
		}

		public Variant sold_remove_items(Variant ids)
		{
			return null;
		}

		public void pshop_set_items(Variant items)
		{
		}

		public void SellItem(Variant data)
		{
		}

		public void get_auc_info_res(Variant arr)
		{
		}

		public void pshop_updexpire_items(Variant items)
		{
		}

		public void pshop_add_items(Variant msgData)
		{
		}

		public Variant pshop_remove_items(Variant ids)
		{
			Variant variant = null;
			Variant variant2 = null;
			for (int i = 0; i < ids.Length; i++)
			{
				uint num = ids[i];
				Variant variant3 = this._remove_item(this._pshop_items, num);
				bool flag = variant3;
				if (flag)
				{
					bool flag2 = variant == null;
					if (flag2)
					{
						variant = new Variant();
					}
					bool flag3 = variant2 == null;
					if (flag3)
					{
						variant2 = new Variant();
					}
					variant.pushBack(variant3);
					variant2.pushBack(num);
				}
			}
			return variant;
		}

		public void GetPlyAucList(Variant data, Action<Variant> finFun = null)
		{
		}

		public void pkg_add_grid(uint count)
		{
			this._pkg_maxi += count;
			this.ui_items.pkg_set_grid(this._pkg_maxi);
		}

		public void GetPlyAucListRes(Variant data)
		{
		}

		public void pshop_add_grid(uint count)
		{
		}

		public void on_get_dbmkt_itm(GameEvent e)
		{
			Variant data = e.data;
			this._dbmkt_items = data;
			bool flag = this.ui_shop == null;
			if (!flag)
			{
				this.ui_shop.set_dbmkt_items(this._dbmkt_items);
			}
		}

		public void touch_cd(uint cdtp, double cd_tm = -1.0)
		{
			bool flag = this._item_cds == null;
			if (flag)
			{
				this._item_cds = new Variant();
			}
			double val = (double)((this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L);
			double num = cd_tm;
			bool flag2 = num == -1.0;
			if (flag2)
			{
				bool flag3 = (ulong)cdtp < (ulong)((long)this._item_cds.Length) && this._item_cds[cdtp] != null && this._item_cds[cdtp]["cd_tm"]._double != -1.0;
				if (!flag3)
				{
					return;
				}
				num = this._item_cds[cdtp]["cd_tm"]._double;
			}
			bool flag4 = this._item_cds.ContainsKey(cdtp.ToString());
			if (flag4)
			{
				Variant variant = this._item_cds[cdtp];
				variant["start_tm"] = val;
				variant["cd_tm"] = num;
			}
			else
			{
				Variant variant2 = new Variant();
				variant2["cdtp"] = cdtp;
				variant2["start_tm"] = val;
				variant2["cd_tm"] = num;
				this._item_cds[cdtp] = variant2;
			}
			bool flag5 = cd_tm != -1.0;
			if (flag5)
			{
			}
		}

		public void set_gold(uint gold)
		{
			this.ui_items.pkg_set_gold(gold);
		}

		public void set_yb(uint yb)
		{
			this.ui_items.pkg_set_yb(yb);
		}

		public void getMergeInfo(Action<Variant> onFin)
		{
			bool flag = this._mergeInfo != null;
			if (!flag)
			{
				bool flag2 = this._mergeInfoCBs == null;
				if (flag2)
				{
				}
			}
		}

		public void setMergeInfo(Variant mergeInfo)
		{
			this._mergeInfo = mergeInfo;
			bool flag = this._mergeInfoCBs;
			if (flag)
			{
				for (int i = 0; i < this._mergeInfoCBs.Count; i++)
				{
				}
			}
			this._mergeInfoCBs = null;
		}

		public void InitDropItems(Variant dpitms)
		{
			this._drop_items = null;
			this._drop_items = new Variant();
			foreach (Variant current in dpitms.Values)
			{
				this.add_drop_item(current);
			}
		}

		public void add_drop_item(Variant data)
		{
			bool flag = this._drop_items.Count == 0;
			if (flag)
			{
			}
			data["pickFlag"] = 0;
			data["mapObj"] = false;
			data["dropSound"] = false;
		}

		public void remove_drop_item(uint dpid)
		{
			uint num = 0u;
			while ((ulong)num < (ulong)((long)this._drop_items.Count))
			{
				Variant variant = this._drop_items[num];
				bool flag = variant["dpid"] == dpid;
				if (flag)
				{
					this.onRmvDropItem(variant);
					break;
				}
				num += 1u;
			}
		}

		public void change_pick_flag(Variant item, int value, bool is_add = true)
		{
			if (is_add)
			{
				item["pickFlag"] = (item["pickFlag"] | value);
			}
			else
			{
				item["pickFlag"] = (item["pickFlag"] & ~value);
			}
		}

		public void pick_dropitem_res(Variant data)
		{
		}

		public void GetShowItem(uint cid, uint id)
		{
			this.igItemMsg.GetShowItem(cid, id);
		}

		public void get_tips_item_data(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"]._int == 1;
			if (flag)
			{
				Variant variant = new Variant();
				variant["data"] = data["itm"];
				variant["str"] = "";
				this.add_show_item_data(variant);
			}
		}

		public void add_show_item_data(Variant data)
		{
			bool flag = data == null || data["data"] == null;
			if (!flag)
			{
				this._show_item_data[data["data"]["id"]] = data;
			}
		}

		public void ResFlagexEqp(Variant data)
		{
		}

		public void SplitItm(uint id, uint cnt, paramStruct data)
		{
			this._splitBack = data.fun;
			this.igItemMsg.split_item(id, cnt);
		}

		public void OnItemMergeRes(Variant msgData)
		{
		}

		public void SetMergeRatept(Variant data)
		{
			this._itmMerge = data;
			bool flag = this._itmMerge == null;
			if (flag)
			{
				this._itmMerge = new Variant();
			}
		}

		public int GetMergeRptById(int id)
		{
			int result = 0;
			foreach (Variant current in this._itmMerge._arr)
			{
				bool flag = current["id"] == id;
				if (flag)
				{
					result = current["rpt"];
					break;
				}
			}
			return result;
		}

		public void embed_stone_res(Variant data)
		{
			bool flag = data.ContainsKey("succ") && data["succ"];
			if (flag)
			{
				Variant itemById = this.GetItemById(data["succ"]["eqpid"]);
				Variant variant = itemById["stn"];
				Variant variant2 = data["succ"]["stn"];
				for (int i = 0; i < variant2.Count; i++)
				{
					variant._arr.Add(variant2[i]);
				}
				this.SetItemStn(variant, data["succ"]["eqpid"]);
				string languageText = LanguagePack.getLanguageText("LGUIItemImpl", "inlaySuccess");
				bool flag2 = data.ContainsKey("gld_cost");
				if (flag2)
				{
					(this.g_mgr as muLGClient).g_generalCT.sub_gold(data["gld_cost"]);
				}
			}
			else
			{
				string languageText2 = LanguagePack.getLanguageText("LGUIItemImpl", "inlayDefeat");
			}
		}

		public void remove_stone_res(Variant data)
		{
			bool flag = data.ContainsKey("gld_cost");
			if (flag)
			{
				(this.g_mgr as muLGClient).g_generalCT.sub_gold(data["gld_cost"]);
			}
			Variant variant = null;
			Variant itemById = this.GetItemById(data["eqpid"]);
			uint num = 0u;
			while ((ulong)num < (ulong)((long)itemById["stn"].Count))
			{
				variant = itemById["stn"][num];
				bool flag2 = variant["id"] == data["stnid"];
				if (flag2)
				{
					break;
				}
				num += 1u;
			}
			Variant variant2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(variant["tpid"]._uint);
			bool flag3 = data.ContainsKey("succ") && data["succ"];
			if (flag3)
			{
				string languageText = LanguagePack.getLanguageText("LGUIItemImpl", "removeStoneSuccess");
				variant["cnt"] = 1;
			}
			else
			{
				string str = LanguagePack.getLanguageText("LGUIItemImpl", "stoneDebase");
				str = variant2["name"] + str;
			}
		}

		private void itemsChange(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("id");
			if (flag)
			{
				this.pkg_mod_item_data(data["id"], data);
			}
			bool flag2 = data.ContainsKey("flag");
			if (flag2)
			{
				int @int = data["flag"]._int;
				if (@int != 3)
				{
					if (@int == 8)
					{
						(this.g_mgr as muLGClient).g_levelsCT.LvlTmCost(data);
					}
				}
				else
				{
					this.DecompBack(data);
				}
			}
			bool flag3 = data.ContainsKey("add");
			if (flag3)
			{
				bool flag4 = !data.ContainsKey("tp");
				if (flag4)
				{
					this.pkg_add_items(data["add"], 101);
				}
				else
				{
					bool flag5 = data["tp"] == 3;
					if (flag5)
					{
						this.temp_add_items(data["add"]);
					}
				}
			}
			bool flag6 = data.ContainsKey("modcnts");
			if (flag6)
			{
				for (int i = 0; i < data["modcnts"].Count; i++)
				{
					Variant variant = data["modcnts"][i];
					bool flag7 = !data.ContainsKey("tp");
					if (flag7)
					{
						bool flag8 = data.ContainsKey("flag");
						if (flag8)
						{
							this.pkg_mod_item_cnt(variant["id"], variant["cnt"], data["flag"]);
						}
						else
						{
							this.pkg_mod_item_cnt(variant["id"], variant["cnt"], 0);
						}
					}
					else
					{
						bool flag9 = data["tp"]._int == 2;
						if (flag9)
						{
							this.repo_mod_item_cnt(variant["id"], variant["cnt"]);
						}
						else
						{
							bool flag10 = data["tp"]._int == 3;
							if (flag10)
							{
								this.temp_mod_item_cnt(variant["id"], variant["cnt"]);
							}
						}
					}
				}
			}
			bool flag11 = data.ContainsKey("modatts");
			if (flag11)
			{
				for (int i = 0; i < data["modatts"].Count; i++)
				{
					Variant variant2 = data["modatts"][i];
					this.pkg_mod_item_att(variant2["id"], variant2["flvl"], variant2["fp"]);
				}
			}
			bool flag12 = data.ContainsKey("rmvids");
			if (flag12)
			{
				bool flag13 = !data.ContainsKey("tp");
				if (flag13)
				{
					this.pkg_remove_items(data["rmvids"]);
				}
				else
				{
					bool flag14 = data["tp"]._int == 2;
					if (flag14)
					{
						this.repo_remove_items(data["rmvids"]);
					}
					else
					{
						bool flag15 = data["tp"]._int == 3;
						if (flag15)
						{
							this.temp_remove_items(data["rmvids"]);
						}
					}
				}
			}
		}

		public void DecompBack(Variant data)
		{
			bool flag = data.ContainsKey("add");
			if (flag)
			{
			}
		}

		public void ChangeEqp(bool add, uint id)
		{
			bool flag = (this.g_mgr.g_gameM as muLGClient).g_levelsCT.IsInBattleSrvLvl();
			if (flag)
			{
				string languageText = LanguagePack.getLanguageText("crosswar", "no_eqpchange");
			}
		}

		public void init_tm_prompt_itms(Variant obj)
		{
			bool flag = this._needCheckPosConf == null;
			if (flag)
			{
				string str = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("notifyCheck").ToString();
				Variant variant = GameTools.split(str, ",", 1u);
				this._needCheckPosConf = new Variant();
				using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator = variant.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string s = enumerator.Current;
						this._needCheckPosConf.pushBack(int.Parse(s));
					}
				}
			}
			bool flag2 = obj["eqp"] != null;
			if (flag2)
			{
				int count = this._tm_prompt_items.Count;
				foreach (Variant current in obj["eqp"].Values)
				{
					bool flag3 = current.ContainsKey("expire") && current["expire"];
					if (flag3)
					{
						this._tm_prompt_items._arr.Add(current);
					}
				}
				bool flag4 = count == 0 && this._tm_prompt_items.Count > 0;
				if (flag4)
				{
				}
			}
			Variant mainPlayerInfo = this.m_player.mainPlayerInfo;
			int num = mainPlayerInfo["cid"];
		}

		public void AddExprieItems(Variant arr)
		{
			foreach (Variant current in arr._arr)
			{
				bool flag = current.ContainsKey("expire") && current["expire"]._float > 0f;
				if (flag)
				{
				}
			}
		}

		public void remove_item(Variant arr)
		{
			foreach (Variant current in arr._arr)
			{
				for (int i = 0; i < this._tm_prompt_items.Length; i++)
				{
					bool flag = this._tm_prompt_items[i]["id"] == current;
					if (flag)
					{
						this._tm_prompt_items._arr.RemoveAt(i);
					}
				}
			}
		}

		public void OnExpireProcess(double tm)
		{
			bool flag = this._tm_prompt_items.Count == 0;
			if (!flag)
			{
				double num = (double)((this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L);
				bool flag2 = num < this._lastFreshTm;
				if (!flag2)
				{
					this._lastFreshTm = num + 1.0;
					Variant variant = new Variant();
					for (int i = 0; i < this._tm_prompt_items.Count; i++)
					{
						bool flag3 = this._tm_prompt_items[i]["expire"] < num;
						if (flag3)
						{
							bool flag4 = this.checkNeednotify(this._tm_prompt_items[i]);
							if (flag4)
							{
								variant._arr.Add(this._tm_prompt_items[i]);
								i--;
							}
						}
					}
					bool flag5 = variant.Count > 0;
					if (flag5)
					{
						uint num2 = 0u;
						while ((ulong)num2 < (ulong)((long)variant.Count))
						{
							Variant item = variant[num2];
							bool flag6 = this.isEquiped(item);
							if (flag6)
							{
							}
							this._expireItems._arr.Add(item);
							num2 += 1u;
						}
						this.itmui.RefreshPackage();
					}
				}
			}
		}

		public void DeleteTypeExprieItems(uint tpid)
		{
			bool flag = this._expireItems.Count > 0;
			if (flag)
			{
				Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(tpid);
				int num = variant["conf"]["pos"];
				for (int i = this._expireItems.Count - 1; i >= 0; i--)
				{
					Variant variant2 = this._expireItems[i];
					Variant variant3 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(variant2["tpid"]._uint);
					bool flag2 = variant3["conf"]["pos"] == num;
					if (flag2)
					{
						this.igItemMsg.delete_item(variant2["id"]);
						this._expireItems._arr.RemoveAt(i);
					}
				}
			}
		}

		private void onGetItemsRes(GameEvent e)
		{
			Variant data = e.data;
			switch (data["sold"]._uint)
			{
			case 0u:
				this.pkg_set_items(data["info"]);
				break;
			case 1u:
				this.sold_set_items(data["info"]);
				break;
			case 2u:
				this.repo_set_items(data["info"]);
				break;
			case 3u:
				this.temp_set_items(data["info"]);
				break;
			}
		}

		private int toShowHint(bool flag, int tpid)
		{
			int result;
			if (flag)
			{
				this.ui_items.ShowBuyItemRemind(false, tpid);
				result = 1;
			}
			else
			{
				result = 2;
			}
			return result;
		}

		protected bool checkNeednotify(Variant item)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(item["tpid"]._uint);
			int num = variant["conf"]["pos"];
			bool flag = this._needCheckPosConf._arr.IndexOf(num) == -1;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this._posArr.ContainsKey("expirePos");
				if (flag2)
				{
					result = this._posArr[num];
				}
				else
				{
					Variant mainPlayerInfo = this.m_player.mainPlayerInfo;
					Variant variant2 = mainPlayerInfo["equip"];
					for (int i = 0; i < this._pkg_items["eqp"].Count; i++)
					{
						variant2[i + variant2.Count] = this._pkg_items["eqp"][i];
					}
					Variant variant3 = variant2;
					double num2 = (double)((this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L);
					this._posArr[num] = true;
					foreach (Variant current in variant3.Values)
					{
						bool flag3 = current["id"] == item["id"] || (current.ContainsKey("expire") && current["expire"] < num2);
						if (!flag3)
						{
							Variant variant4 = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]._uint);
							bool flag4 = variant4 && variant4["conf"]["pos"] == num;
							if (flag4)
							{
								this._posArr[num] = false;
								break;
							}
						}
					}
					result = this._posArr[num];
				}
			}
			return result;
		}

		protected bool isEquiped(Variant item)
		{
			Variant mainPlayerInfo = this.m_player.mainPlayerInfo;
			Variant variant = mainPlayerInfo["equip"];
			bool result;
			foreach (Variant current in variant.Values)
			{
				bool flag = current["id"] == item["id"];
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public Variant _getPkgItems(Variant data)
		{
			Variant variant = new Variant();
			Variant variant2 = data["ci"];
			for (int i = 0; i < variant2.Count; i++)
			{
				Variant item = variant2[i];
				variant._arr.Add(item);
			}
			Variant variant3 = data["nci"];
			for (int i = 0; i < variant3.Count; i++)
			{
				Variant item = variant3[i];
				variant._arr.Add(item);
			}
			Variant variant4 = data["eqp"];
			for (int i = 0; i < variant4.Count; i++)
			{
				Variant item = variant4[i];
				variant._arr.Add(item);
			}
			return variant;
		}

		protected Variant _remove_item(Variant item_data, uint id)
		{
			bool flag = item_data == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = item_data == this._pkg_items;
				if (flag2)
				{
					bool flag3 = item_data.ContainsKey("ci");
					if (flag3)
					{
						List<Variant> arr = item_data["ci"]._arr;
						for (int i = 0; i < arr.Count; i++)
						{
							Variant variant = arr[i];
							bool flag4 = variant["id"] == id;
							if (flag4)
							{
								arr.RemoveAt(i);
								result = variant;
								return result;
							}
						}
					}
					bool flag5 = item_data.ContainsKey("nci");
					if (flag5)
					{
						List<Variant> arr = item_data["nci"]._arr;
						for (int i = 0; i < arr.Count; i++)
						{
							Variant variant = arr[i];
							bool flag6 = variant["id"] == id;
							if (flag6)
							{
								arr.RemoveAt(i);
								result = variant;
								return result;
							}
						}
					}
					bool flag7 = item_data.ContainsKey("eqp");
					if (flag7)
					{
						List<Variant> arr = item_data["eqp"]._arr;
						for (int i = 0; i < arr.Count; i++)
						{
							Variant variant = arr[i];
							bool flag8 = variant["id"] == id;
							if (flag8)
							{
								arr.RemoveAt(i);
								result = variant;
								return result;
							}
						}
					}
				}
				else
				{
					bool flag9 = item_data == this._pshop_items;
					if (flag9)
					{
						List<Variant> arr = item_data["itms"]._arr;
						bool flag10 = arr != null;
						if (flag10)
						{
							for (int i = 0; i < arr.Count; i++)
							{
								Variant variant = arr[i]["itm"];
								bool flag11 = variant["id"] == id;
								if (flag11)
								{
									arr.RemoveAt(i);
									result = variant;
									return result;
								}
							}
						}
					}
					else
					{
						List<Variant> arr = item_data["itm"]._arr;
						bool flag12 = arr != null;
						if (flag12)
						{
							for (int i = 0; i < arr.Count; i++)
							{
								Variant variant = arr[i];
								bool flag13 = variant["id"] == id;
								if (flag13)
								{
									arr.RemoveAt(i);
									result = variant;
									return result;
								}
							}
						}
					}
				}
				result = null;
			}
			return result;
		}

		protected Variant _get_item_data_array(Variant item_data, uint tpid)
		{
			bool flag = item_data != this._pkg_items;
			Variant result;
			if (flag)
			{
				result = item_data["itm"];
			}
			else
			{
				Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(tpid);
				bool flag2 = variant != null;
				if (flag2)
				{
					bool flag3 = variant["tp"] == 3;
					Variant variant2;
					if (flag3)
					{
						variant2 = item_data["eqp"];
					}
					else
					{
						bool flag4 = variant["conf"].ContainsKey("mul") && variant["conf"]["mul"]._int > 0;
						if (flag4)
						{
							variant2 = item_data["ci"];
						}
						else
						{
							variant2 = item_data["nci"];
						}
					}
					result = variant2;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		protected void _realUseItem(int itemId, uint tpid, uint idx = 0u)
		{
			Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(tpid);
			bool flag = (this.g_mgr.g_gameM as muLGClient).g_levelsCT.IsInBattleSrvLvl();
			if (flag)
			{
				bool flag2 = !variant["conf"].ContainsKey("state") && !variant["conf"].ContainsKey("bstate");
				if (flag2)
				{
					return;
				}
			}
			this.touch_cd(variant["conf"].ContainsKey("cdtp") ? variant["conf"]["cdtp"]._uint : 0u, -1.0);
			bool flag3 = variant != null && variant["conf"].ContainsKey("itempkg") && variant["conf"]["itempkg"].Count > 1;
			if (flag3)
			{
				this.igItemMsg.get_use_item((uint)itemId, idx);
			}
			else
			{
				this.igItemMsg.use_item((uint)itemId);
			}
		}

		private void change_pick_flag_by_id(uint dpid, int value, bool is_add = true)
		{
			foreach (Variant current in this._drop_items.Values)
			{
				bool flag = current["dpid"] == dpid;
				if (flag)
				{
					this.change_pick_flag(current, value, is_add);
					break;
				}
			}
		}

		protected void onRmvDropItem(Variant dItem)
		{
			bool flag = dItem["mapObj"];
			if (flag)
			{
			}
			bool flag2 = this._drop_items.Count == 0;
			if (flag2)
			{
			}
		}

		protected void processDropItem(double tmSlice)
		{
			for (int i = this._drop_items.Count - 1; i >= 0; i--)
			{
				Variant variant = this._drop_items[i];
				bool flag = variant["dropSound"];
				if (flag)
				{
					variant["dropSound"] = false;
					this._playDropSound(variant);
				}
			}
		}

		private void _playDropSound(Variant data)
		{
			bool flag = data.ContainsKey("gold");
			if (!flag)
			{
				bool flag2 = data.ContainsKey("eqp");
				if (flag2)
				{
					string text = data["eqp"]["id"];
				}
				else
				{
					bool flag3 = data.ContainsKey("itm");
					if (flag3)
					{
						string text = data["itm"]["id"];
					}
				}
			}
		}

		protected void _playPickSound(Variant data)
		{
			bool flag = data.ContainsKey("gold");
			if (!flag)
			{
				bool flag2 = data.ContainsKey("eqp");
				if (flag2)
				{
					string text = data["eqp"]["id"];
				}
				else
				{
					bool flag3 = data.ContainsKey("itm");
					if (flag3)
					{
						string text = data["itm"]["id"];
					}
				}
			}
		}

		private void _init_tm_prompt_equip_itms(int cid, Variant detail_info)
		{
			Variant arr = detail_info["equip"];
			this.AddExprieItems(arr);
		}

		private void _remove_stone(uint stnid)
		{
			Variant itemById = this.GetItemById(stnid);
			bool flag = itemById != null;
			if (flag)
			{
				bool flag2 = !itemById.ContainsKey("cnt");
				if (flag2)
				{
					itemById["cnt"] = 1;
				}
				Variant expr_39 = itemById;
				Variant value = expr_39["cnt"] - 1;
				expr_39["cnt"] = value;
				bool flag3 = itemById["cnt"] <= 0;
				if (!flag3)
				{
					this.pkg_mod_item_data(stnid, itemById);
				}
			}
		}

		public Variant GetSmeltData()
		{
			bool flag = this._smeltData == null;
			if (flag)
			{
				(this.g_mgr.g_netM as muNetCleint).igRandShopMsgs.SmeltItem(0u);
			}
			return this._smeltData;
		}

		public void OnSmeltItemRes(Variant data)
		{
			this._smeltData = data;
			LGIUISmelt lGIUISmelt = this.g_mgr.g_uiM.getLGUI("smelt") as LGIUISmelt;
			lGIUISmelt.GetSmeltInfoRes(this._smeltData);
		}

		public void OnGetSmeltAwdRes(Variant data)
		{
			this._smeltData["awdcnt"] = data["awdcnt"];
			LGIUISmelt lGIUISmelt = this.g_mgr.g_uiM.getLGUI("smelt") as LGIUISmelt;
			lGIUISmelt.GetAwdRes(this._smeltData);
		}

		public void RefreshBuyDmktItms(Variant items)
		{
		}

		public void RefreshBuyHexpItms(Variant items)
		{
		}

		public void PkgsUseItem(Variant data)
		{
			bool flag = data.ContainsKey("pkgs");
			if (flag)
			{
				foreach (Variant current in data["pkgs"].Values)
				{
					bool flag2 = current["name"]._str == "gold";
					if (flag2)
					{
						(this.g_mgr as muLGClient).g_generalCT.add_gold(current["val"]._uint);
					}
				}
			}
		}
	}
}
