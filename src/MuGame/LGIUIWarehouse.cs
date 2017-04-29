using Cross;
using System;

namespace MuGame
{
	public interface LGIUIWarehouse
	{
		void repo_set_items(Variant items);

		void repo_add_items(Variant items);

		void repo_rmv_items(Variant item_ids);

		void repo_mod_item_data(int item_id, Variant item);

		void repo_set_grid(int count);

		void pkg_set_yb(int value);

		void pkg_set_gold(int value);
	}
}
