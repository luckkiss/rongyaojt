using Cross;
using System;

namespace MuGame
{
	public interface LGIUILevelHall
	{
		void refreshTowerRecord(Array data, Variant lvlData = null);

		void refreshTowerPage();

		void ResetRadioOrder();
	}
}
