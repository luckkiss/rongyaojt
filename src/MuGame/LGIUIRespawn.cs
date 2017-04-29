using Cross;
using System;

namespace MuGame
{
	public interface LGIUIRespawn
	{
		void OpenRespawnUI(uint resSec, Variant data);

		void CloseRespawnUI();

		bool isLeave_lvl();
	}
}
