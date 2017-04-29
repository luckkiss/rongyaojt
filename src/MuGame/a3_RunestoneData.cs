using System;
using System.Collections.Generic;

namespace MuGame
{
	public struct a3_RunestoneData
	{
		public int id;

		public string item_name;

		public int icon_file;

		public string desc;

		public int quality;

		public int use_type;

		public int stonelevel;

		public int name_type;

		public int position;

		public Dictionary<int, int> runeston_att;

		public List<a3_RunestonrnMaterial> compose_data;

		public List<a3_RunestonrnMaterial> decompose_data;

		public int stone_level
		{
			get
			{
				return (this.stonelevel < 1) ? 1 : this.stonelevel;
			}
			set
			{
				this.stonelevel = value;
			}
		}
	}
}
