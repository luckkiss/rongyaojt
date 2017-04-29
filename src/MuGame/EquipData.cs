using System;
using System.Collections.Generic;

namespace MuGame
{
	public struct EquipData
	{
		public int id;

		public int tpid;

		public bool equiped;

		public int lv;

		public int addexp;

		public int addlv;

		public int stage;

		public int blessing_lv;

		public int combpt;

		public Dictionary<int, int> aad_att;

		public EquipConf equipConf;
	}
}
