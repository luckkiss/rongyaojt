using Cross;
using System;

namespace MuGame
{
	public interface LGIUILottery
	{
		void refreshLog(Variant data);

		void setCurYb(uint value);

		void pkg_add_items(Variant items);

		void pkg_rmv_items(Variant item_ids);

		void pkg_mod_item_data(uint item_id, Variant item);

		void PtChange(int v);
	}
}
