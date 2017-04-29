using System;

namespace MuGame
{
	internal class FindBestoModel : ModelBase<FindBestoModel>
	{
		public bool Canfly = true;

		public string nofly_txt = "当前状态不可传送";

		public uint waitTime = 15u;
	}
}
