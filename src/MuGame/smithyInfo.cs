using GameFramework;
using System;

namespace MuGame
{
	public class smithyInfo : LGDataBase
	{
		public smithyInfo(muNetCleint m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new smithyInfo(m as muNetCleint);
		}

		public override void init()
		{
		}
	}
	internal struct SmithyInfo
	{
		public int Level;

		public int ExpToNextLevel;

		public int MaxAllowedSetLv;
	}
}
