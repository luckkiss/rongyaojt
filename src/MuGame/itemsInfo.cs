using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class itemsInfo : LGDataBase
	{
		private const uint SOLD_TP_PACKAGE = 0u;

		private const uint SOLD_TP_SELLED = 1u;

		private const uint SOLD_TP_STORAGE = 2u;

		private const uint SOLD_TP_STORAGE_TEMP = 3u;

		private Variant _itemInfo;

		private Variant _packageItems = new Variant();

		private Variant _packageItemsMap = new Variant();

		private bool _packageItemsRefreshFlag = true;

		private Variant itemInfo
		{
			get
			{
				bool flag = this._itemInfo == null;
				Variant result;
				if (flag)
				{
					this._itemInfo = new Variant();
					this._itemInfo = 2;
					Variant variant = new Variant();
					variant["sold"] = 0;
					result = null;
				}
				else
				{
					bool flag2 = this._itemInfo.isInteger && this._itemInfo == 2;
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = this._itemInfo;
					}
				}
				return result;
			}
		}

		public Variant packageItems
		{
			get
			{
				bool flag = this.itemInfo == null;
				Variant result;
				if (flag)
				{
					result = null;
				}
				else
				{
					this.refreshPackageItems();
					result = this._packageItems;
				}
				return result;
			}
		}

		public bool isPrepoAct
		{
			get
			{
				bool flag = this.itemInfo == null;
				return !flag && this.itemInfo["prepo"]._bool;
			}
		}

		public uint packageMaxGrid
		{
			get
			{
				bool flag = this.itemInfo == null;
				uint result;
				if (flag)
				{
					result = 0u;
				}
				else
				{
					result = this.itemInfo["info"]["maxi"]._uint;
				}
				return result;
			}
		}

		public itemsInfo(muNetCleint m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new itemsInfo(m as muNetCleint);
		}

		public override void init()
		{
		}

		private void refreshPackageItems()
		{
			bool flag = !this._packageItemsRefreshFlag;
			if (!flag)
			{
				this._packageItemsRefreshFlag = false;
				this._packageItems._arr.Clear();
				this._packageItems._arr.AddRange(this.itemInfo["info"]["ci"]._arr);
				this._packageItems._arr.AddRange(this.itemInfo["info"]["nci"]._arr);
				this._packageItems._arr.AddRange(this.itemInfo["info"]["eqp"]._arr);
				foreach (Variant current in this._packageItems._arr)
				{
					this._packageItemsMap[current["id"]._uint] = current;
				}
			}
		}

		private void onItemRes(GameEvent e)
		{
		}

		private void onItemChange(GameEvent e)
		{
			this._packageItemsRefreshFlag = true;
			Variant data = e.data;
			bool flag = data.ContainsKey("id");
			if (!flag)
			{
				bool flag2 = data.ContainsKey("add");
				if (flag2)
				{
					foreach (Variant current in data["add"]._arr)
					{
						this._packageItems._arr.Add(current);
						this._packageItemsMap[current["id"]._uint] = current;
						base.dispatchEvent(GameEvent.Create(3036u, this, current, false));
					}
				}
				else
				{
					bool flag3 = data.ContainsKey("rmv");
					if (flag3)
					{
						foreach (Variant current2 in data["rmv"]._arr)
						{
							this._packageItems._arr.Remove(current2);
							this._packageItemsMap._dct.Remove(current2["id"]._uint.ToString());
							base.dispatchEvent(GameEvent.Create(3038u, this, current2, false));
						}
					}
					else
					{
						bool flag4 = data.ContainsKey("mod");
						if (flag4)
						{
							foreach (Variant current3 in data["mod"]._arr)
							{
								uint @uint = current3["id"]._uint;
								bool flag5 = !this._packageItemsMap.ContainsKey(@uint.ToString());
								if (!flag5)
								{
									Variant variant = this._packageItemsMap[@uint];
									foreach (string current4 in current3.Keys)
									{
										variant[current4] = current3[current4];
									}
									base.dispatchEvent(GameEvent.Create(3037u, this, current3, false));
								}
							}
						}
					}
				}
			}
		}
	}
}
