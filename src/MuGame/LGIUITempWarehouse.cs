using Cross;
using System;

namespace MuGame
{
	public interface LGIUITempWarehouse
	{
		void temp_set_items(Variant items);

		void temp_add_items(Variant items);

		void temp_rmv_items(Variant item_ids);

		void temp_mod_item_data(uint item_id, Variant item);

		void PackageFull();
	}
}
