using Cross;
using System;

namespace MuGame
{
	internal class HeroUtils
	{
		public static Variant getHeroVar(int hid, uint iid, float x, float y)
		{
			Variant variant = new Variant();
			variant["hid"] = hid;
			variant["iid"] = iid;
			variant["x"] = x;
			variant["y"] = y;
			variant["level"] = 1;
			variant["speed"] = 10;
			variant["hp"] = 10000;
			variant["max_hp"] = 10000;
			return variant;
		}
	}
}
