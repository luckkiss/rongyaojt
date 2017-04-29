using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class lgGDMarket : lgGDBase
	{
		protected Variant _marketData;

		protected Variant _searchObj;

		protected int _next_send_tm = 0;

		protected int _send_load_tm = 1100;

		protected int _send_load_process_tm = 1200;

		protected Variant _need_buy_arr = new Variant();

		protected uint _oldTpid = 0u;

		protected int _lostTm = 300000;

		protected int _reqTm = 0;

		protected Variant _needBuyItems = new Variant();

		protected Variant _hadBuyItems = new Variant();

		protected bool _autoBuyQueryFlg = false;

		private Dictionary<string, processStruct> _processDict = new Dictionary<string, processStruct>();

		public lgGDMarket(gameManager m) : base(m)
		{
		}

		public static lgGDMarket create(IClientBase m)
		{
			return new lgGDMarket(m as gameManager);
		}

		public override void init()
		{
		}

		public void getMarketData(Variant pSearchObj = null)
		{
			bool flag = pSearchObj != null;
			if (flag)
			{
				this._searchObj = pSearchObj;
			}
			int num = (int)GameTools.getTimer();
			int num2 = num - this._next_send_tm;
			bool flag2 = num2 > this._send_load_tm;
			if (flag2)
			{
				this._next_send_tm = (int)GameTools.getTimer();
				DelayDoManager.singleton.AddDelayDo(new Action(this.delayGetMarketData), this._send_load_tm / 1000, 0u);
			}
		}

		private void delayGetMarketData()
		{
			bool flag = this._searchObj != null;
			if (flag)
			{
				(this.g_mgr.g_netM as muNetCleint).igItemMsg.get_ply_auc_list(this._searchObj);
			}
		}

		public void setMarketData(Variant msgData)
		{
			bool autoBuyQueryFlg = this._autoBuyQueryFlg;
			if (autoBuyQueryFlg)
			{
				this.getCanBuyItems(msgData["itms"]);
				this._autoBuyQueryFlg = false;
			}
			else
			{
				this._marketData = msgData["itms"];
				LGIUIMarket lGIUIMarket = this.g_mgr.g_uiM.getLGUI("UI_MARKET") as LGIUIMarket;
				lGIUIMarket.refreshItemList(this._marketData);
			}
		}

		public void setIncome(uint income)
		{
			LGIUIMarket lGIUIMarket = this.g_mgr.g_uiM.getLGUI("UI_MARKET") as LGIUIMarket;
			lGIUIMarket.setIncome(income);
		}

		public Variant rmvMarketItems(Variant itmArr)
		{
			bool flag = this._marketData == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = new Variant();
				for (int i = 0; i < itmArr.Length; i++)
				{
					Variant variant2 = this.rmvItemById(this._marketData, itmArr[i]);
					bool flag2 = variant2;
					if (flag2)
					{
						variant._arr.Add(variant2["itm"]);
					}
				}
				bool flag3 = variant.Length > 0;
				if (flag3)
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

		protected Variant rmvItemById(Variant items, uint itemid)
		{
			Variant result;
			for (int i = 0; i < items.Length; i++)
			{
				bool flag = items[i]["itm"]["id"]._uint == itemid;
				if (flag)
				{
					items._arr.RemoveAt(i);
					result = items;
					return result;
				}
			}
			result = null;
			return result;
		}

		protected bool isNeedBuyItem(uint tpid)
		{
			bool result;
			foreach (Variant current in this._need_buy_arr._arr)
			{
				bool flag = current != null && tpid == current["tpid"]._uint;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		protected Variant getBuyItemByTpid(uint tpid)
		{
			Variant result;
			foreach (Variant current in this._need_buy_arr._arr)
			{
				bool flag = tpid == current["tpid"]._uint;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public void addBuyItem(Variant itemData)
		{
			bool flag = !this.isNeedBuyItem(itemData["tpid"]._uint);
			if (flag)
			{
				this._need_buy_arr._arr.Add(itemData);
			}
		}

		public void deleteBuyItem(Variant itemData)
		{
			bool flag = this._need_buy_arr.Length > 0;
			if (flag)
			{
				for (int i = this._need_buy_arr.Length - 1; i >= 0; i--)
				{
					Variant variant = this._need_buy_arr[i];
					bool flag2 = itemData["tpid"] == variant["tpid"] && itemData["up_max"] == variant["up_max"];
					if (flag2)
					{
						this._need_buy_arr._arr.RemoveAt(i);
						return;
					}
				}
				bool flag3 = this._need_buy_arr.Length == 0;
				if (flag3)
				{
				}
			}
		}

		public Variant rmvHadBuyItem(Variant itmArr)
		{
			Variant variant = new Variant();
			for (int i = 0; i < itmArr.Length; i++)
			{
				Variant variant2 = this.rmvItemById(this._hadBuyItems, itmArr[i]);
				bool flag = variant2;
				if (flag)
				{
					this.refreshBuyList(variant2);
					variant._arr.Add(variant2);
				}
			}
			bool flag2 = itmArr.Length > 0;
			Variant result;
			if (flag2)
			{
				result = itmArr;
			}
			else
			{
				result = null;
			}
			return result;
		}

		protected void refreshBuyList(Variant itemInfo)
		{
			bool flag = this._needBuyItems != null && this._needBuyItems.Length > 0;
			if (flag)
			{
				Variant variant = itemInfo["itm"];
				for (int i = this._need_buy_arr.Length - 1; i >= 0; i--)
				{
					Variant variant2 = this._need_buy_arr[i];
					bool flag2 = variant["tpid"] == variant2["tpid"];
					if (flag2)
					{
						uint num = variant.ContainsKey("cnt") ? variant["cnt"]._uint : 1u;
						Variant variant3 = variant2;
						variant3["cnt"] = variant3["cnt"] - num;
						bool flag3 = variant2["cnt"] <= 0;
						if (flag3)
						{
							this._need_buy_arr._arr.RemoveAt(i);
						}
						else
						{
							this.gotoBuyItem();
						}
						LGIUIMarket lGIUIMarket = this.g_mgr.g_uiM.getLGUI("UI_MARKET") as LGIUIMarket;
						lGIUIMarket.refreshAutoBuyList(this._need_buy_arr);
						break;
					}
				}
				bool flag4 = this._need_buy_arr.Length == 0;
				if (flag4)
				{
				}
			}
		}

		protected void getCanBuyItems(Variant items)
		{
			bool flag = this._need_buy_arr.Length > 0;
			if (flag)
			{
				bool flag2 = items != null && items.Length > 0;
				if (flag2)
				{
					Variant buyItemByTpid = this.getBuyItemByTpid(this._oldTpid);
					bool flag3 = !buyItemByTpid;
					if (!flag3)
					{
						Variant variant = new Variant();
						for (int i = 0; i < items.Length; i++)
						{
							Variant variant2 = items[i];
							Variant variant3 = variant2["itm"];
							bool flag4 = this._oldTpid != variant3["tpid"]._uint;
							if (flag4)
							{
								return;
							}
							uint num = variant3.ContainsKey("cnt") ? variant3["cnt"]._uint : 1u;
							Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
							uint num2 = mainPlayerInfo["cid"];
							bool flag5 = num2 == variant2["cid"]._uint || buyItemByTpid["up_max"]._uint < variant2["yb"]._uint / num;
							if (!flag5)
							{
								variant._arr.Add(variant2);
							}
						}
						this._needBuyItems = variant;
						this.gotoBuyItem();
					}
				}
			}
		}

		protected void gotoBuyItem()
		{
			bool flag = this._needBuyItems.Length > 0;
			if (flag)
			{
				for (int i = 0; i < this._needBuyItems.Length; i++)
				{
					Variant variant = this._needBuyItems[i];
					Variant variant2 = variant["itm"];
					uint tpid = variant2["tpid"];
					Variant buyItemByTpid = this.getBuyItemByTpid(tpid);
					bool flag2 = buyItemByTpid == null;
					if (flag2)
					{
						break;
					}
					lgGDGeneral g_generalCT = (this.g_mgr.g_gameM as muLGClient).g_generalCT;
					int yb = g_generalCT.yb;
					uint num = variant2.ContainsKey("cnt") ? variant2["cnt"]._uint : 1u;
					int num2 = (int)(this.g_mgr.g_gameM as muLGClient).g_itemsCT.pkg_get_grid();
					int num3 = (this.g_mgr.g_gameM as muLGClient).g_itemsCT.pkg_occupy_grid();
					bool flag3 = yb < variant["yb"]._int || num2 - num3 < 1;
					if (flag3)
					{
						this._needBuyItems._arr.Clear();
						this.deleteBuyItem(buyItemByTpid);
						this._need_buy_arr = new Variant();
						LGIUIMarket lGIUIMarket = this.g_mgr.g_uiM.getLGUI("UI_MARKET") as LGIUIMarket;
						lGIUIMarket.refreshAutoBuyList(this._need_buy_arr);
					}
					else
					{
						bool flag4 = buyItemByTpid["cnt"]._uint >= num && buyItemByTpid["up_max"]._int >= variant["yb"] / variant2["cnt"];
						if (flag4)
						{
							this._hadBuyItems._arr.Add(variant);
							this._needBuyItems._arr.RemoveAt(i);
							(this.g_mgr.g_netM as muNetCleint).igItemMsg.buy_auc_itm(variant["cid"], variant2["id"], 0);
							break;
						}
					}
				}
			}
		}

		public void processFun(float tm)
		{
			bool flag = this._need_buy_arr.Length > 0;
			if (flag)
			{
				int num = (int)GameTools.getTimer();
				bool flag2 = num < this._reqTm;
				if (!flag2)
				{
					Variant variant = new Variant();
					bool flag3 = this._need_buy_arr.Length > 1;
					if (flag3)
					{
						for (int i = 0; i < this._need_buy_arr.Length; i++)
						{
							bool flag4 = this._oldTpid == this._need_buy_arr[i]["tpid"]._uint;
							if (flag4)
							{
								int num2 = i + 1;
								bool flag5 = num2 >= this._need_buy_arr.Length;
								if (flag5)
								{
									num2 = 0;
								}
								variant = this._need_buy_arr[num2];
								break;
							}
						}
					}
					else
					{
						variant = this._need_buy_arr[0];
					}
					this._oldTpid = variant["tpid"];
					int num3 = (int)GameTools.getTimer();
					int num4 = num3 - this._next_send_tm;
					bool flag6 = num4 > this._send_load_tm;
					if (flag6)
					{
						Variant variant2 = new Variant();
						variant2["tpids"] = variant["tpid"];
						variant2["row"] = 0;
						variant2["up_max"] = variant["up_max"];
						variant2["cnt"] = variant["cnt"];
						variant2["up_odr"] = true;
						(this.g_mgr.g_netM as muNetCleint).igItemMsg.get_ply_auc_list(variant2);
						this._autoBuyQueryFlg = true;
						this._reqTm = num3 + this._lostTm;
					}
					else
					{
						this._reqTm = num3 + this._send_load_tm;
					}
				}
			}
		}
	}
}
