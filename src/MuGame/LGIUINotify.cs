using Cross;
using System;

namespace MuGame
{
	public interface LGIUINotify
	{
		void CheckToNotify(string type, Variant objData = null);

		void notifyFriendRequest(Variant objData = null, bool filter = true);

		void notifyTradeRequest(Variant objData = null, bool filter = true);

		void notifyInviteAddTeam(Variant objData = null, bool filter = true);

		void NotifyApplicationAddToTeam(Variant objData = null, bool filter = true);

		void notifyBackGift(Variant objData = null, bool filter = true);

		void notifyupLvlGift(Variant objData = null, bool filter = true);

		void notifytimeReminder(Variant objData = null, bool filter = true);

		void notifyAccountSafe(Variant objData = null, bool filter = true);

		void notifyActivity(Variant objData = null, bool filter = true);

		void notifyFlash(Variant objData = null, bool filter = true);

		void notifyPrivateMsg(Variant objData = null, bool filter = true);

		void AddNewItems(Variant items, bool filter = true);

		void notifyRecommendFriend(Variant objData = null, bool filter = true);

		void notifyNewMail(Variant objData = null, bool filter = true);
	}
}
