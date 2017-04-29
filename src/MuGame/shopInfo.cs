using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class shopInfo : LGDataBase
	{
		private Variant _common = new Variant();

		public Variant _godsDct = new Variant();

		public Variant _shopGods = new Variant();

		public Variant _tpid = new Variant();

		public Variant _cost = new Variant();

		public int _buyGodsId;

		public Variant shopGods
		{
			get
			{
				return this._shopGods;
			}
		}

		public shopInfo(muNetCleint m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new shopInfo(m as muNetCleint);
		}

		public override void init()
		{
			base.g_mgr.addEventListener(5004u, new Action<GameEvent>(this.onGetShopGodsRes));
			base.addEventListener(3061u, new Action<GameEvent>(this.chooseShop));
		}

		private void onGetShopGods(GameEvent e)
		{
		}

		private void onBuyItem(GameEvent e)
		{
			Variant data = e.data;
		}

		private void onGetShopGodsRes(GameEvent e)
		{
			this._godsDct = e.data;
			base.dispatchEvent(GameEvent.Create(3061u, this, null, false));
		}

		private void onBuyItemRes(GameEvent e)
		{
			Variant data = e.data;
			base.dispatchEvent(GameEvent.Create(3063u, this, data, false));
		}

		private void chooseShop(GameEvent e)
		{
			bool flag = e.data == null;
			if (!flag)
			{
				int @int = e.data._int;
				bool flag2 = 6 >= this._tpid.Count;
				if (flag2)
				{
					bool flag3 = this._godsDct["itms"].Count == 0;
					if (flag3)
					{
						this._tpid = (base.g_mgr.g_gameConfM as muCLientConfig).svrMarketConf.get_hot_items_tpid();
						this.getCommonGods();
						int[] array = new int[]
						{
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10,
							10
						};
						for (int i = 0; i < array.Length; i++)
						{
							this._cost[i] = array[i];
							this._godsDct["itms"][i] = this._tpid[i];
						}
						int j = 0;
						int num = 24;
						while (j < array.Length)
						{
							this._godsDct["itms"][num] = this._cost[j];
							j++;
							num++;
						}
					}
					else
					{
						for (int k = 0; k < 6; k++)
						{
							this._tpid[k] = this._godsDct["itms"][k]["tpid"];
							this._cost[k] = this._godsDct["itms"][k]["yb"];
						}
						this.getCommonGods();
						for (int l = 6; l < 24; l++)
						{
							this._cost[l] = 10;
						}
					}
				}
				bool flag4 = !this._godsDct["ybmkt"]["discnt"];
				if (flag4)
				{
					int val = 100;
					this._godsDct["ybmkt"]["discnt"] = val;
				}
				int num2 = 6 * @int;
				int num3 = 6 * (1 + @int);
				this._shopGods["tpid"] = new Variant();
				this._shopGods["img"] = new Variant();
				this._shopGods["name"] = new Variant();
				this._shopGods["cost"] = new Variant();
				this._shopGods["discnt"] = new Variant();
				this._shopGods["discnt"] = this._godsDct["ybmkt"]["discnt"];
				for (int m = num2; m < num3; m++)
				{
					this._shopGods["tpid"].pushBack(this._tpid[m]);
					string val2 = (base.g_mgr.g_gameConfM as muCLientConfig).localItems.get_item_icon_url(this._tpid[m]);
					this._shopGods["img"].pushBack(val2);
					string languageText = LanguagePack.getLanguageText("items_xml", Convert.ToString(this._tpid[m]._int));
					this._shopGods["name"].pushBack(languageText);
					this._shopGods["cost"].pushBack(this._cost[m]);
				}
				base.dispatchEvent(GameEvent.Create(3062u, this, this._shopGods, false));
			}
		}

		private void getCommonGods()
		{
			this._common = (base.g_mgr.g_gameConfM as muCLientConfig).svrMarketConf.get_common_items_tpid();
			int i = 0;
			int idx = 0;
			while (i < 18)
			{
				Random random = new Random();
				int count = this._common.Count;
				int minValue = 0;
				int num = 0;
				idx = random.Next(minValue, count);
				using (List<Variant>.Enumerator enumerator = this._tpid._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num2 = enumerator.Current;
						bool flag = this._common[idx] != num2;
						if (!flag)
						{
							break;
						}
						num++;
					}
				}
				bool flag2 = num == this._tpid.Length;
				if (flag2)
				{
					this._tpid.pushBack(this._common[idx]);
				}
				else
				{
					i--;
				}
				i++;
			}
		}

		public Variant getItemQG()
		{
			Variant variant = new Variant();
			return (base.g_mgr.g_gameConfM as muCLientConfig).svrMarketConf.get_hot_items();
		}

		public Variant getItemCY()
		{
			Variant variant = new Variant();
			return (base.g_mgr.g_gameConfM as muCLientConfig).svrMarketConf.get_sell_items();
		}

		public Variant getItemXH()
		{
			Variant variant = new Variant();
			return (base.g_mgr.g_gameConfM as muCLientConfig).svrMarketConf.get_bndsell_items();
		}

		public Variant getItemBZ()
		{
			Variant variant = new Variant();
			return (base.g_mgr.g_gameConfM as muCLientConfig).svrMarketConf.get_shopppt_items();
		}

		public void GetShopItem()
		{
			(base.g_mgr.g_netM as muNetCleint).igItemMsg.get_dbmkt_itm();
		}
	}
}
