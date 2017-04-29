using System;
using System.Collections.Generic;

namespace MuGame
{
	public struct SkillXmlData
	{
		public bool hitfall;

		public string eff_female;

		public uint id;

		public string skill_name;

		public string eff;

		public Dictionary<uint, SkillLvXmlData> lv;

		public int target_type;

		public bool useJump;

		public string jump_canying;

		public int targetNum;

		public SkillHitedXml hitxml;
	}
}
