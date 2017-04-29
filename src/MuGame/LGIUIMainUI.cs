using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public interface LGIUIMainUI
	{
		void show_all();

		void onChatMsg(Variant data);

		void setChaInfo(Variant data);

		void sethp(int cur, uint max);

		void setmp(int cur, uint max);

		void setProtect(uint cur, uint max);

		void setCurLevel(Variant data);

		void setCurExp(uint exp, int val = 0);

		void setCurYb(uint value);

		void setCurBndyb(uint value);

		void setCurGold(uint value);

		void setActivities(Variant data);

		void systemmsg(Variant msgs, uint type = 4u);

		void output_server_err(int errcode);

		void output_client_err(int errcode);

		void pkg_set_items(Variant items);

		void pkg_add_items(Variant items, int flag = 0);

		void pkg_rmv_items(Variant item);

		void pkg_mod_item_data(uint item_id, Variant item, int flag = 0);

		void refreshSkillList(Variant skillList);

		void addNewSkill(Variant skill);

		void setClientConfig(Variant clientConf);

		void updateNpcMisState(LGAvatarBase c);

		void addCharacter(LGAvatarBase c);

		void removeCharacter(LGAvatarBase c);

		void changeMap(Variant mapData);

		void updatePath(Variant path);

		void removePath();

		void stopMove();

		void updateMove(LGAvatarBase c);

		void Respawn(LGAvatarBase c);

		void StateChange(Variant states);

		void setCharVipInfo(bool isvip);

		void VipChange(int vip);

		void setAIState(uint state);

		void AIStop();

		void Restart();

		void set_clan_reqs(Variant reqs);

		void add_clan_req(Variant data);

		void ChangePKState(int state);

		void PczoneChange(bool b);

		void setNextOlAward(Variant data);

		void NobptChange(int val);

		void CarrlvlChange(int carrlvl, bool isup);

		void BlessChange(Variant arr);

		void AddAttShow(string att, int value);

		void AddAttShows(Variant arr);

		void OnConLost();

		void showItemTips(Variant data);

		void AutoGame();

		void RefreshRecMisAwardPan();

		void AddMlineawd();

		void MisAwdOver();

		void ShowLevelUpAni();

		void ShowMapMsg(int mapid);

		void ShowMsgBoxStr(Variant data);

		void ShowClanEvent(string text, string fun = "", string arg1 = "", int tp = 0);

		void OpenInterface(string typename, bool isopen = true);

		void openSystem(string typename, bool isopen = true);

		void RemoveStateShow(int id, string name);

		void OnMissionRes();

		void show_defense(Variant data);

		void updateFullBtn(bool isfull);

		void addTeamMateToMap(Variant teammate = null);

		void ColdDown(Variant data, Dictionary<uint, Variant> cds);

		void ItemColdDown(Dictionary<uint, Variant> cds);

		void RideLevelUp();

		void RideQualUp();

		void AddScreenShake(string name);

		void RefreshMonthRewardInvest(Variant data);

		void RefreshUplvlRewardInvest(Variant data);

		void EnterMultilvl();

		void LeaveMultilvl();
	}
}
