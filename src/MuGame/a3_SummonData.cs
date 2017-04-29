using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public struct a3_SummonData : IComparable<a3_SummonData>
	{
		public int id;

		public int tpid;

		public string name;

		public int currentexp;

		public int currenthp;

		public int maxhp;

		public int grade;

		public int naturaltype;

		public int level;

		public int lifespan;

		public int blood;

		public int luck;

		public int power;

		public int talent_type;

		public int attNatural;

		public int defNatural;

		public int agiNatural;

		public int conNatural;

		public int star;

		public int max_attack;

		public int min_attack;

		public int physics_def;

		public int magic_def;

		public int physics_dmg_red;

		public int magic_dmg_red;

		public int dodge;

		public int hit;

		public int double_damage_rate;

		public int fatal_damage;

		public int reflect_crit_rate;

		public int skillNum;

		public Dictionary<int, int> skills;

		public Dictionary<int, int> equips;

		public bool isSpecial;

		public int objid;

		public int status;

		public int CompareTo(a3_SummonData other)
		{
			bool flag = this.Equals(other);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num = this.isSpecial.CompareTo(other.isSpecial) * 8 + this.grade.CompareTo(other.grade) * -4 + this.level.CompareTo(other.level) * -2 + this.name.CompareTo(other.name);
				result = ((num == 0) ? 0 : Mathf.Clamp(num, -1, 1));
			}
			return result;
		}
	}
}
