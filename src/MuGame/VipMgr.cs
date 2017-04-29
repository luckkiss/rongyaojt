using System;

namespace MuGame
{
	internal class VipMgr
	{
		public static int BUY_TILI_COUNT = 0;

		public static int BUY_EXP_STAGE_COUNT = 1;

		public static int REFRESH_MSHOP_COUNT = 2;

		public static int USE_TILI_S_COUNT = 3;

		public static int USE_TILI_M_COUNT = 4;

		public static int USE_TILI_L_COUNT = 5;

		public static int BUY_PK_STAGE_COUNT = 6;

		public static int TILI_RESTORE_TIME = 7;

		public static int LEAVE_EXP_MUL = 8;

		public static int PLOT_STAGE_RESET = 9;

		public static int MASTER_STAGE_RESET = 10;

		public static int ONE_KEY_CLEAN = 11;

		public static int MSHOP_SELL_ZONE = 12;

		public static int BATTLE_HERO_COUNT = 13;

		public static int LUCKDRAW_SALE_OFF = 14;

		public static int AUTO_FIGHT = 15;

		public static int FAMILY_TREASURE = 16;

		public static int getValue(int id)
		{
			int vip = (int)ModelBase<PlayerModel>.getInstance().vip;
			bool flag = vip == 0 || !ModelBase<PlayerModel>.getInstance().isvipActive;
			int @int;
			if (flag)
			{
				SXML sXML = XMLMgr.instance.GetSXML("vip.viplevel", "vip_level==0");
				@int = sXML.GetNode("vt", "type==" + id).getInt("value");
			}
			else
			{
				SXML sXML = XMLMgr.instance.GetSXML("vip.viplevel", "vip_level==" + ModelBase<PlayerModel>.getInstance().vip);
				@int = sXML.GetNode("vt", "type==" + id).getInt("value");
			}
			return @int;
		}
	}
}
