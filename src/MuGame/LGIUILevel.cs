using Cross;
using System;

namespace MuGame
{
	public interface LGIUILevel
	{
		void OnCreateLevel(Variant data);

		void OnEnterLevel(Variant lvlConf, Variant data);

		void OnLevelFinish(Variant lvlConf, Variant data);

		void OnLeaveLevel(Variant lvlConf, bool isFin);

		void OnGetLevelAwd(Variant data);

		void OnMetchLevel(Variant data);

		void OnLevelRoundChange(Variant data);

		void OnLevelBroadcast(Variant data);

		void OnKillCourseMon(Variant data);

		void OnUpdateSidept(Variant data);

		void UpdateLevelPvpinfo(Variant data);

		void UpdateSelfPvpinfo(Variant data);

		void UpdateKmtimgs();

		void UpdateTmtimgs(Variant trigsArr);

		void AddCityWarCost(Variant data);

		void AddLvlCost();

		void OnBuyLevelBuffRes(Variant data);

		void UpdateSelfKPInfo(Variant data);

		void OnLevelError(Variant data);

		void OnClanTerrInfoChange(uint clteid);

		void UpdateAllClanTerrBuildHP(uint clteid);

		void UpdateClanTerrBuildHP(uint clteid, Variant data);

		void UpdateClanTerrReqInfo(uint clteid, Variant warReqs);

		void AddClanTerrReq(uint clteid, uint clanid);

		void LevelKill(Variant data);
	}
}
