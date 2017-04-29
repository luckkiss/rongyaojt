using Cross;
using System;

namespace MuGame
{
	public interface LGIUIMultilevel
	{
		void OnTeamListBack(Variant rooms);

		void LeaveRoomBack(Variant roomInfo, bool isself);

		void JoinRoomBack(Variant roomInfo, bool isself);

		void RoomInfoChange(Variant roomInfo);

		void FlushRoomMateInfo(Variant data);

		void refreshTowerRecord(Variant data, Variant lvlData = null);
	}
}
