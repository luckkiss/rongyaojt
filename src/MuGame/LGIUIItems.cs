using Cross;
using System;

namespace MuGame
{
	public interface LGIUIItems
	{
		void pkg_set_items(Variant items);

		void pkg_add_items(Variant items, int flag = 0);

		void pkg_rmv_items(Variant item_ids);

		void pkg_mod_item_data(uint item_id, Variant item);

		void pkg_set_grid(uint count);

		void pkg_set_yb(uint value);

		void pkg_set_gold(uint value);

		void pkg_set_bdyb(uint value);

		void SelfAddEquip(Variant eqps);

		void SelfRmvEquip(Variant eqps);

		void equipDataChange(Variant itmData, Variant oldData);

		void openUseitemPrompt(Variant itemData);

		void UseMlineAward();

		void UseItemError(int err);

		void UseItemSuccess(int id, int cdtp, float cdtm);

		void ShowBuyItemRemind(bool flag, Variant data);

		void RefreshPackage();

		void SetMyGrade(Variant data);

		void SetLotteryFirst(Variant items, int flag = 0, Action<Variant, int> fun = null);
	}
}
