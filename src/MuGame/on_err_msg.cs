using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_err_msg : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 252u;
			}
		}

		public static on_err_msg create()
		{
			return new on_err_msg();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["res"] == -1204;
			if (!flag)
			{
				bool flag2 = -208 == this.msgData["res"];
				if (flag2)
				{
					LGIUIRespawn lGIUIRespawn = (this.session as ClientSession).g_mgr.g_uiM.getLGUI("LGUIRespawn") as LGIUIRespawn;
					lGIUIRespawn.CloseRespawnUI();
				}
				else
				{
					bool flag3 = -253 == this.msgData["res"];
					if (!flag3)
					{
						bool flag4 = -1808 == this.msgData["res"];
						if (!flag4)
						{
							bool flag5 = -218 == this.msgData["res"];
							if (!flag5)
							{
								bool flag6 = -1217 == this.msgData["res"];
								if (!flag6)
								{
									bool flag7 = -1204 == this.msgData["res"];
									if (!flag7)
									{
										bool flag8 = -1101 == this.msgData["res"];
										if (flag8)
										{
											((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.on_package_full();
										}
										else
										{
											bool flag9 = -616 == this.msgData["res"];
											if (flag9)
											{
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
