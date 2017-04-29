using System;

namespace MuGame
{
	public interface LGIUISystemOpen
	{
		void OnSelfLevelUp(uint level);

		void OnSelfCpMission(uint misId);

		void ReflushOpenData();

		bool CanOpen(string name, bool isRemind = false);

		void OnResetlvl(int v);

		void OnCarrlvl(int v);
	}
}
