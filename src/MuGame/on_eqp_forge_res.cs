using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_eqp_forge_res : RPCMsgProcesser
	{
		private lgGDItems _lgGDItems;

		private lgSelfPlayer _selfPlayer;

		public override uint msgID
		{
			get
			{
				return 72u;
			}
		}

		private lgGDItems gdItem
		{
			get
			{
				bool flag = this._lgGDItems == null;
				if (flag)
				{
					this._lgGDItems = ((this.session as ClientSession).g_mgr.g_gameM.getObject("LG_ITEMS") as lgGDItems);
				}
				return this._lgGDItems;
			}
		}

		private lgSelfPlayer selfPlayer
		{
			get
			{
				bool flag = this._selfPlayer == null;
				if (flag)
				{
					this._selfPlayer = ((this.session as ClientSession).g_mgr.g_gameM.getObject("LG_MAIN_PLAY") as lgSelfPlayer);
				}
				return this._selfPlayer;
			}
		}

		public static on_eqp_forge_res create()
		{
			return new on_eqp_forge_res();
		}

		protected override void _onProcess()
		{
			Variant variant = new Variant();
			bool flag = !this.msgData.ContainsKey("tp");
			if (!flag)
			{
				int @int = this.msgData["tp"]._int;
				if (@int != 1)
				{
					if (@int == 2)
					{
						bool flag2 = this.msgData.ContainsKey("succ");
						if (flag2)
						{
							bool flag3 = !this.gdItem.pkg_mod_item_data(this.msgData["id"], this.msgData["succ"]);
							if (flag3)
							{
							}
						}
						else
						{
							bool flag4 = this.msgData.ContainsKey("failed");
							if (flag4)
							{
								bool flag5 = !this.gdItem.pkg_mod_item_data(this.msgData["id"], this.msgData["failed"]);
								if (flag5)
								{
								}
							}
						}
					}
				}
				else
				{
					bool flag6 = this.msgData.ContainsKey("succ");
					if (flag6)
					{
						bool flag7 = !this.gdItem.pkg_mod_item_data(this.msgData["id"], this.msgData["succ"]);
						if (flag7)
						{
						}
					}
					else
					{
						bool flag8 = this.msgData.ContainsKey("failed");
						if (flag8)
						{
							bool flag9 = !this.gdItem.pkg_mod_item_data(this.msgData["id"], this.msgData["failed"]);
							if (flag9)
							{
							}
						}
					}
				}
				this.gdItem.ForgeBack(this.msgData["tp"], this.msgData.ContainsKey("succ"));
			}
		}
	}
}
