using Cross;
using System;

namespace MuGame
{
	public interface LGIUIRideSkill
	{
		void RefreshActiveData();

		void RefreshRideActive(Variant data);

		void RefreshCurRideSkill(Variant data);

		void ItemChange();
	}
}
