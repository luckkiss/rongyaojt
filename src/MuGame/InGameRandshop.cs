using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameRandshop : MsgProcduresBase
	{
		private class on_item_msg_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 95u;
				}
			}

			private lgGDRandshop vlgGDRandshop
			{
				get
				{
					return ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_randshopCT;
				}
			}

			private lgGDItems lgGDitem
			{
				get
				{
					return ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT;
				}
			}

			private LGGDAward vlgGDAward
			{
				get
				{
					return ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).lgGD_Award;
				}
			}

			public static InGameRandshop.on_item_msg_res create()
			{
				return new InGameRandshop.on_item_msg_res();
			}

			protected override void _onProcess()
			{
				switch (this.msgData["tp"]._int)
				{
				case 2:
					this.vlgGDAward.OnGetInviteFriendData(this.msgData);
					break;
				case 3:
					this.vlgGDAward.OnGetFeedAwardData(this.msgData);
					break;
				case 4:
					this.vlgGDRandshop.setRandShopData(this.msgData);
					break;
				case 5:
				{
					bool flag = this.msgData["batch"] != null;
					if (flag)
					{
						this.vlgGDRandshop.setBatchRefreshData(this.msgData);
					}
					else
					{
						this.vlgGDRandshop.setOnceRefreshData(this.msgData);
					}
					break;
				}
				case 6:
					this.vlgGDRandshop.setBuyLog(this.msgData);
					break;
				case 13:
					this.lgGDitem.OnSmeltItemRes(this.msgData["eqp_smelt"]);
					break;
				case 14:
					this.lgGDitem.OnGetSmeltAwdRes(this.msgData);
					break;
				}
			}
		}

		public InGameRandshop(IClientBase m) : base(m)
		{
		}

		public static InGameRandshop create(IClientBase m)
		{
			return new InGameRandshop(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(95u, new NetManager.RPCProcCreator(InGameRandshop.on_item_msg_res.create));
		}

		public void SendGetRandShop()
		{
			Variant variant = new Variant();
			variant["tp"] = 8;
			base.sendRPC(95u, variant);
		}

		public void RefreshRandShop(bool useitm = true, bool free = true, uint batch = 0u)
		{
			bool flag = batch > 0u;
			if (flag)
			{
				Variant variant = new Variant();
				variant["tp"] = 9;
				variant["batch"] = batch;
				variant["useitm"] = useitm;
				variant["free"] = free;
				base.sendRPC(95u, variant);
			}
			else
			{
				Variant variant2 = new Variant();
				variant2["tp"] = 9;
				variant2["useitm"] = useitm;
				variant2["free"] = free;
				base.sendRPC(95u, variant2);
			}
		}

		public void BuyRandShopItem(bool batch, uint id, uint cnt, int exatt = 0, int exlevel1 = 0, int exlevel2 = 0)
		{
			Variant variant = new Variant();
			variant["tp"] = 10;
			variant["batch"] = batch;
			variant["id"] = id;
			variant["cnt"] = cnt;
			variant["exatt"] = exatt;
			variant["exlevel1"] = exlevel1;
			variant["exlevel2"] = exlevel2;
			base.sendRPC(95u, variant);
		}

		public void GetRandShopLog()
		{
			lgGDRandshop g_randshopCT = (this.g_mgr.g_gameM as muLGClient).g_randshopCT;
			base.sendRPC(95u, GameTools.createGroup(new Variant[]
			{
				"tp",
				11,
				"maxid",
				g_randshopCT.maxid
			}));
		}

		public void ExchangeItem(uint exid, uint itmid)
		{
			Variant variant = new Variant();
			variant["tp"] = 12;
			variant["exid"] = exid;
			variant["itmid"] = itmid;
			base.sendRPC(95u, variant);
		}

		public void SmeltItem(uint eqpid)
		{
			Variant variant = new Variant();
			variant["tp"] = 13;
			variant["eqpid"] = eqpid;
			base.sendRPC(95u, variant);
		}

		public void GetSmeltAwd()
		{
			Variant variant = new Variant();
			variant["tp"] = 14;
			base.sendRPC(95u, variant);
		}

		public void sendPvipMsg(Variant data)
		{
			base.sendRPC(95u, data);
		}

		public void GetInviteFriendsAward()
		{
			Variant variant = new Variant();
			variant["tp"] = 3;
			this.sendPvipMsg(variant);
		}

		public void GetFeedAwardData()
		{
			Variant variant = new Variant();
			variant["tp"] = 5;
			this.sendPvipMsg(variant);
		}

		public void GetDoPerdayAward()
		{
			Variant variant = new Variant();
			variant["tp"] = 6;
			this.sendPvipMsg(variant);
		}

		public void GetDaysAward(int gmisid)
		{
			Variant variant = new Variant();
			variant["tp"] = 7;
			variant["gmisid"] = gmisid;
			this.sendPvipMsg(variant);
		}

		public void GetFriendGlowAward(int id)
		{
			Variant variant = new Variant();
			variant["tp"] = 4;
			variant["ivt_lvlid"] = id;
			this.sendPvipMsg(variant);
		}

		public void getInviteFriendData()
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			this.sendPvipMsg(variant);
		}
	}
}
