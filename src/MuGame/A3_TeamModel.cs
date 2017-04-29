using System;
using System.Collections.Generic;

namespace MuGame
{
	public class A3_TeamModel : ModelBase<A3_TeamModel>
	{
		public ItemTeamMemberData AffirmInviteData;

		public ItemTeamData NewMemberJoinData;

		public Dictionary<uint, string> cidName = new Dictionary<uint, string>();

		public Dictionary<uint, string> cidNameElse = new Dictionary<uint, string>();

		public bool bein = false;

		public uint ltpids;

		public string getProfessional(uint carr)
		{
			string result = string.Empty;
			switch (carr)
			{
			case 1u:
				result = "全职业";
				break;
			case 2u:
				result = "战士";
				break;
			case 3u:
				result = "法师";
				break;
			case 5u:
				result = "刺客";
				break;
			}
			return result;
		}

		public bool Limit_Change_Teammubiao(int obj)
		{
			int num = 0;
			switch (obj)
			{
			case 0:
				num = 5;
				break;
			case 1:
				num = 4;
				break;
			case 2:
				num = 1;
				break;
			case 3:
				num = 2;
				break;
			case 4:
				num = 3;
				break;
			}
			int num2 = (int)(ModelBase<PlayerModel>.getInstance().up_lvl * 100u + ModelBase<PlayerModel>.getInstance().lvl);
			SXML sXML = XMLMgr.instance.GetSXML("func_open.team_lv_limit", "id==" + num);
			int num3 = sXML.getInt("zhuan") * 100 + sXML.getInt("lv");
			return num2 >= num3;
		}
	}
}
