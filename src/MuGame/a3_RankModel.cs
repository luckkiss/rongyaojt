using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class a3_RankModel : ModelBase<a3_RankModel>
	{
		public static int now_id = 0;

		public static int nowexp = 0;

		public static bool nowisactive = true;

		public Dictionary<int, rankinfos> dicrankinfo;

		public a3_RankModel()
		{
			this.dicrankinfo = new Dictionary<int, rankinfos>();
			this.readxml();
		}

		private void readxml()
		{
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("achievement.title", "");
			foreach (SXML current in sXMLList)
			{
				rankinfos rankinfos = new rankinfos();
				rankinfos.name = current.getString("title_name");
				rankinfos.title_id = current.getInt("title_id");
				rankinfos.rankexp = current.getInt("para");
				foreach (SXML current2 in current.GetNodeList("nature", ""))
				{
					rankinfos.nature[current2.getUint("att_type")] = current2.getInt("att_value");
				}
				this.dicrankinfo[rankinfos.title_id] = rankinfos;
			}
		}

		public void refreinfo(int title_nowid, int exp, bool isavtive = true)
		{
			a3_RankModel.now_id = title_nowid;
			a3_RankModel.nowexp = exp;
			a3_RankModel.nowisactive = isavtive;
		}

		public bool CheckTitleLevelupAvailable()
		{
			return FunctionOpenMgr.instance.Check(FunctionOpenMgr.ACHIEVEMENT, false) && this.dicrankinfo.ContainsKey(a3_RankModel.now_id + 1) && (ulong)ModelBase<PlayerModel>.getInstance().ach_point > (ulong)((long)this.dicrankinfo[a3_RankModel.now_id + 1].rankexp);
		}
	}
}
