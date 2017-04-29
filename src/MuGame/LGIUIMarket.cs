using Cross;
using System;

namespace MuGame
{
	public interface LGIUIMarket
	{
		void refreshItemList(Variant itemList);

		void setIncome(uint income);

		void refreshAutoBuyList(Variant itemList);

		void openMarket();
	}
}
