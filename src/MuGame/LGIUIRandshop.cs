using Cross;
using System;

namespace MuGame
{
	public interface LGIUIRandshop
	{
		void addNewBuyLog(Variant logList);

		void resetOnceRefresh(Variant data);

		void resetBatchRefresh(Variant data);

		void setFreeRefresh(Variant data);
	}
}
