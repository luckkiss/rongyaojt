using System;
using System.Collections.Generic;

namespace MuGame
{
	public class ItemTeamMemberData
	{
		public uint leaderCid;

		public bool cofirmed;

		public uint teamId;

		public uint totalCount;

		public uint idxBegin;

		public bool dirJoin;

		public bool membInv;

		public bool meIsCaptain;

		public uint removedIndex;

		public uint ltpid;

		public uint ldiff;

		public List<ItemTeamData> itemTeamDataList;

		public ItemTeamMemberData()
		{
			this.itemTeamDataList = new List<ItemTeamData>();
		}

		public bool IsInMyTeam(string name)
		{
			bool result;
			for (int i = 0; i < this.itemTeamDataList.Count; i++)
			{
				bool flag = this.itemTeamDataList[i].name.Equals(name);
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
	}
}
