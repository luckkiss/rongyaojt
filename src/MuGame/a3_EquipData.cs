using System;
using System.Collections.Generic;

namespace MuGame
{
	public struct a3_EquipData
	{
		public uint color;

		public int intensify_lv;

		public int add_level;

		public int add_exp;

		public int stage;

		public int blessing_lv;

		public int combpt;

		public int attribute;

		public int att_type;

		public int att_value;

		public int eqp_level;

		public int eqp_type;

		public Dictionary<int, int> subjoin_att;

		public Dictionary<int, int> gem_att;

		public Dictionary<int, int> baoshi;

		public uint? tpid;
	}
}
