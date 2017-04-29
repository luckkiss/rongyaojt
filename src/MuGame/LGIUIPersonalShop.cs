using Cross;
using System;

namespace MuGame
{
	public interface LGIUIPersonalShop
	{
		void pshop_set_items(Variant items);

		void pshop_add_items(Variant item);

		void pshop_rmv_items(Array item_ids);

		void pshop_set_grid(uint count);

		void SellItem(int tpid);
	}
}
