using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class lgGDRandshop : lgGDBase
	{
		protected int _maxid;

		protected int _freecnt;

		protected Variant _onceRefreshArr = new Variant();

		protected Variant _batchRefreshArr = new Variant();

		protected Variant _buyLogArr = new Variant();

		public int maxid
		{
			get
			{
				return this._maxid;
			}
		}

		public lgGDRandshop(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDRandshop(m as gameManager);
		}

		public override void init()
		{
		}

		public void setBuyLog(Variant msgData)
		{
			this._maxid = msgData["maxid"];
			Variant variant = msgData["ary"];
			for (int i = 0; i < variant.Count; i++)
			{
				this._buyLogArr._arr.Add(variant[i]);
			}
			LGIUIRandshop lGIUIRandshop = (this.g_mgr.g_uiM as muUIClient).getLGUI("LGUIRandshopImpl") as LGIUIRandshop;
			bool flag = lGIUIRandshop == null;
			if (!flag)
			{
				lGIUIRandshop.addNewBuyLog(variant);
			}
		}

		public void setRandShopData(Variant msgData)
		{
			LGIUIRandshop lGIUIRandshop = (this.g_mgr.g_uiM as muUIClient).getLGUI("LGUIRandshopImpl") as LGIUIRandshop;
			bool flag = lGIUIRandshop == null;
			if (!flag)
			{
				bool flag2 = msgData.ContainsKey("ary_expire");
				if (flag2)
				{
					lGIUIRandshop.setFreeRefresh(msgData);
				}
				bool flag3 = msgData["ary"] != null;
				if (flag3)
				{
					this._onceRefreshArr = msgData["ary"];
					lGIUIRandshop.resetOnceRefresh(this._onceRefreshArr);
				}
				bool flag4 = msgData["bary"] != null;
				if (flag4)
				{
					this._batchRefreshArr = msgData["bary"];
					lGIUIRandshop.resetBatchRefresh(this._batchRefreshArr);
				}
			}
		}

		public void setOnceRefreshData(Variant msgData)
		{
			for (int i = 0; i < this._onceRefreshArr.Length; i++)
			{
				bool flag = this._onceRefreshArr[i]["id"] == msgData["id"];
				if (flag)
				{
					this._onceRefreshArr[i]["cnt"] = this._onceRefreshArr[i] - msgData["cnt"];
					bool flag2 = this._onceRefreshArr[i]["cnt"] == 0;
					if (flag2)
					{
						this._onceRefreshArr._arr.RemoveAt(i);
					}
					break;
				}
			}
			LGIUIRandshop lGIUIRandshop = (this.g_mgr.g_uiM as muUIClient).getLGUI("randshop") as LGIUIRandshop;
			bool flag3 = lGIUIRandshop == null;
			if (!flag3)
			{
				lGIUIRandshop.resetOnceRefresh(this._onceRefreshArr);
			}
		}

		public void setBatchRefreshData(Variant msgData)
		{
			for (int i = 0; i < this._batchRefreshArr.Length; i++)
			{
				bool flag = this._batchRefreshArr[i]["id"] == msgData["id"];
				if (flag)
				{
					this._batchRefreshArr[i]["cnt"] = this._batchRefreshArr[i] - msgData["cnt"];
					bool flag2 = this._batchRefreshArr[i]["cnt"] == 0;
					if (flag2)
					{
						this._batchRefreshArr._arr.RemoveAt(i);
					}
					break;
				}
			}
			LGIUIRandshop lGIUIRandshop = (this.g_mgr.g_uiM as muUIClient).getLGUI("randshop") as LGIUIRandshop;
			bool flag3 = lGIUIRandshop == null;
			if (!flag3)
			{
				lGIUIRandshop.resetBatchRefresh(this._batchRefreshArr);
			}
		}
	}
}
