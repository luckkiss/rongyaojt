using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class A3_AuctionProxy : BaseProxy<A3_AuctionProxy>
	{
		public static uint EVENT_LOADALL = 60u;

		public static uint EVENT_SELLSCUCCESS = 1u;

		public static uint EVENT_LOADMYSHELF = 0u;

		public static uint EVENT_PUTOFFSUCCESS = 2u;

		public static uint EVENT_BUYSUCCESS = 3u;

		public static uint EVENT_MYGET = 10u;

		public static uint EVENT_GETMYGET = 4u;

		public static uint EVENT_NEWGET = 99u;

		public A3_AuctionProxy()
		{
			this.addProxyListener(117u, new Action<Variant>(this.AuctionOP));
		}

		public void SendMyRackMsg()
		{
			Variant variant = new Variant();
			variant["op"] = 0;
			this.sendRPC(117u, variant);
		}

		public void SendPutOnMsg(uint id, uint puttm, uint yb, uint num)
		{
			Variant variant = new Variant();
			variant["op"] = 1;
			variant["id"] = id;
			variant["puttm"] = puttm;
			variant["yb"] = yb;
			variant["num"] = num;
			this.sendRPC(117u, variant);
		}

		public void SendPutOffMsg(uint id)
		{
			Variant variant = new Variant();
			variant["op"] = 2;
			variant["id"] = id;
			this.sendRPC(117u, variant);
		}

		public void SendBuyMsg(uint id, uint cid, uint num)
		{
			Variant variant = new Variant();
			variant["op"] = 3;
			variant["id"] = id;
			variant["cid"] = cid;
			variant["num"] = num;
			this.sendRPC(117u, variant);
		}

		public void SendGetMsg(uint id)
		{
			Variant variant = new Variant();
			variant["op"] = 4;
			variant["id"] = id;
			this.sendRPC(117u, variant);
		}

		public void SendGetAllMsg()
		{
			Variant variant = new Variant();
			variant["op"] = 5;
			this.sendRPC(117u, variant);
		}

		public void SendSearchMsg(uint row = 0u, uint up_cost = 0u, uint job = 0u, uint equip_type = 0u, uint stage = 0u, uint quality = 0u, string name = null)
		{
			Variant variant = new Variant();
			variant["op"] = 6;
			variant["row"] = row * 8u;
			variant["up_cost"] = up_cost;
			bool flag = job > 0u;
			if (flag)
			{
				variant["job"] = job;
			}
			bool flag2 = equip_type > 0u;
			if (flag2)
			{
				variant["equip_type"] = equip_type - 1u;
			}
			bool flag3 = stage > 0u;
			if (flag3)
			{
				variant["stage"] = stage - 1u;
			}
			bool flag4 = quality > 0u;
			if (flag4)
			{
				variant["quality"] = quality;
			}
			bool flag5 = name != null;
			if (flag5)
			{
				variant["name"] = name;
			}
			this.sendRPC(117u, variant);
		}

		private void AuctionOP(Variant data)
		{
			debug.Log(data.dump());
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			Variant variant = new Variant();
			switch (num)
			{
			case 0:
				ModelBase<A3_AuctionModel>.getInstance().AddMyItem(data);
				base.dispatchEvent(GameEvent.Create(A3_AuctionProxy.EVENT_LOADMYSHELF, this, data, false));
				base.dispatchEvent(GameEvent.Create(A3_AuctionProxy.EVENT_MYGET, this, data, false));
				break;
			case 1:
				base.dispatchEvent(GameEvent.Create(A3_AuctionProxy.EVENT_SELLSCUCCESS, this, data, false));
				break;
			case 2:
				ModelBase<A3_AuctionModel>.getInstance().UpToDown(data);
				base.dispatchEvent(GameEvent.Create(A3_AuctionProxy.EVENT_PUTOFFSUCCESS, this, data, false));
				break;
			case 3:
				ModelBase<A3_AuctionModel>.getInstance().AddMyItem(data);
				base.dispatchEvent(GameEvent.Create(A3_AuctionProxy.EVENT_BUYSUCCESS, this, data, false));
				break;
			case 4:
			{
				uint key = data["auc_id"];
				bool flag2 = ModelBase<A3_AuctionModel>.getInstance().GetMyItems_down().ContainsKey(key);
				if (flag2)
				{
					a3_BagItemData a3_BagItemData = ModelBase<A3_AuctionModel>.getInstance().GetMyItems_down()[key];
					bool flag3 = a3_BagItemData.auctiondata.get_type != 3;
					if (flag3)
					{
						flytxt.instance.fly(string.Concat(new object[]
						{
							"领取了",
							ModelBase<A3_AuctionModel>.getInstance().myitems_down[key].num,
							"个",
							ModelBase<A3_AuctionModel>.getInstance().myitems_down[key].confdata.item_name
						}), 0, default(Color), null);
					}
					else
					{
						flytxt.instance.fly("领取了" + (int)((float)ModelBase<A3_AuctionModel>.getInstance().myitems_down[key].auctiondata.cost * 0.8f) + "钻石", 0, default(Color), null);
					}
					ModelBase<A3_AuctionModel>.getInstance().GetMyItems_down().Remove(key);
				}
				base.dispatchEvent(GameEvent.Create(A3_AuctionProxy.EVENT_GETMYGET, this, data, false));
				break;
			}
			case 6:
				ModelBase<A3_AuctionModel>.getInstance().Clear();
				variant = data["auc_data"];
				foreach (Variant current in variant._arr)
				{
					ModelBase<A3_AuctionModel>.getInstance().AddItem(current);
				}
				base.dispatchEvent(GameEvent.Create(A3_AuctionProxy.EVENT_LOADALL, this, data, false));
				break;
			}
			Variant variant2 = new Variant();
			bool flag4 = ModelBase<A3_AuctionModel>.getInstance().GetMyItems_down().Count > 0;
			if (flag4)
			{
				variant2["new"] = true;
			}
			else
			{
				variant2["new"] = false;
			}
			base.dispatchEvent(GameEvent.Create(A3_AuctionProxy.EVENT_NEWGET, this, variant2, false));
		}
	}
}
