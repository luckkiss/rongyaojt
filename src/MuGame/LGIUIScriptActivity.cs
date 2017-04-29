using Cross;
using System;

namespace MuGame
{
	public interface LGIUIScriptActivity
	{
		void refreshTowerRecord(Variant data, Variant lvlData = null);

		void refreshTowerPage();
	}
}
