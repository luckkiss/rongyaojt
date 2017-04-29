using Cross;
using System;

namespace MuGame
{
	public interface LGIUIShop
	{
		void set_dbmkt_items(Variant items);

		void setCurYb(uint value);

		void setCurBndyb(uint value);

		void VipLevelChange();

		void OnResetlvl();
	}
}
