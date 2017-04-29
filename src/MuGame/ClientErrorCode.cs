using Cross;
using System;

namespace MuGame
{
	public class ClientErrorCode
	{
		public const int CHATMSG_1 = -1001;

		public const int CHATMSG_2 = -1002;

		public const int CHATMSG_3 = -1003;

		public const int CHATMSG_4 = -1004;

		public const int CHATMSG_5 = -1005;

		public const int NULL_MSG = -1006;

		public const int NO_SPEAKER = -1007;

		public const int NO_USE_ITEM = -2001;

		public const int NO_SELECT_ITEM = -2002;

		public const int ITEM_IN_CD = -2003;

		public const int ITEM_NO_SPLIT = -2004;

		public const int WAREHOUSE_IS_FULL = -2005;

		public const int ITEM_NO_DELET = -2006;

		public const int NO_WAREHOUSE = -2007;

		public const int NO_TRANS_IN_LEVEL = -2008;

		public const int PACKAGE_IS_FULL = -2009;

		public const int ITEM_NO_SELL = -2010;

		public const int NO_ADD_ITEM_IN_BATCHSELL = -2011;

		public const int OPEN_PREPO_SUCCESS = -2012;

		public const int JOIN_CLAN_FAIL = -2020;

		public const int CREAT_CLAN_FAIL = -2021;

		public const int CLANNAME_PUT_ERROR = -2022;

		public const int MESG_TOO_LOOG = -2023;

		public const int CHANGGE_NOTICE_FAIL = -2024;

		public const int NO_CLAN_LEADER = -2025;

		public const int NO_FIRE_CLAN_LEADER = -2026;

		public const int JOIN_TEAM_FAIL = -2040;

		public const int HAS_MAST_QUAL = -2060;

		public const int NO_ENOUGH_GOLD = -2061;

		public static string error_string(int errcode)
		{
			return LanguagePack.getLanguageText("clientError", errcode.ToString());
		}
	}
}
