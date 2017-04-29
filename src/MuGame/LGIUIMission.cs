using Cross;
using System;

namespace MuGame
{
	public interface LGIUIMission
	{
		void setMissions(int misid = 0);

		void OnEnterLevel(Variant levelInfo);

		void showMisTrack();

		void PlayerRmisChanged(uint rmisid);

		void PlayerRmisAccept(uint rmisid);

		void MissionChange(int type, int misid);

		void PlayerRmisChangedCarry(uint rmisid);

		void changeMap(Variant mapData);

		void RefreshMisTrack(string refreshType = "");

		void to_find_npc(int mid, int npc_id);

		void accept_move_execute_link_event(string str);

		void OpenMissionGuide(uint misid);
	}
}
