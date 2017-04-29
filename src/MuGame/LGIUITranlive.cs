using Cross;
using System;

namespace MuGame
{
	public interface LGIUITranlive
	{
		void OnResetlvl(Variant data);

		void RollptBack(Variant data);

		void leftPointChange();

		void levelUP();

		void itemChange(Variant item);
	}
}
