using Cross;
using System;

namespace MuGame
{
	public interface LGIUITrade
	{
		void showTrade(uint cid);

		void closeTrade();

		void addSelfItem(Variant obj);

		void addTargetItem(Variant obj);

		void setLockStatus(bool selfLocked, bool targerLocked);

		void setTradeDone(bool selfTradeDone, bool targerTradeDone);
	}
}
