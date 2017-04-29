using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class SkillProxy : BaseProxy<SkillProxy>
	{
		public static uint EVENT_SKILL_UP = 0u;

		public void sendSkillUp(int id)
		{
			Variant variant = new Variant();
			variant["skill_id"] = id;
			this.sendRPC(87u, variant);
		}

		private void onSkillUp(Variant data)
		{
			int val = data["skid"];
			int val2 = data["sklvl"];
			Variant variant = new Variant();
			variant["skill_id"] = val;
			variant["skill_level"] = val2;
			ModelBase<SkillModel>.getInstance().changeSkillList(variant);
		}

		private void onSetUpSkill(Variant data)
		{
			int num = data["res"];
			bool flag = num != 1;
			if (flag)
			{
				Globle.err_output(num);
			}
			else
			{
				Variant variant = new Variant();
				variant["skill_id"] = data["skill_id"];
				variant["skill_level"] = data["skill_level"];
				ModelBase<SkillModel>.getInstance().changeSkillList(variant);
				base.dispatchEvent(GameEvent.Create(SkillProxy.EVENT_SKILL_UP, this, variant, false));
			}
		}
	}
}
