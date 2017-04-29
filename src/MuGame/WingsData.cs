using System;

namespace MuGame
{
	public class WingsData
	{
		public int stage;

		public uint IconID;

		public string spriteFile;

		public string wingName;

		public uint stageCostGold;

		public uint stageCrystalMin;

		public uint stageCrystalMax;

		public uint stageCrystalStep;

		public uint stageRateMin;

		public uint stageRateMax;

		public bool isUnlock(int curStage)
		{
			bool result = false;
			bool flag = curStage >= this.stage;
			if (flag)
			{
				result = true;
			}
			return result;
		}
	}
}
