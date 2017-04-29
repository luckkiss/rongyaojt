using Cross;
using System;

namespace MuGame
{
	public interface LGIUISkill
	{
		void refreshSkillList(Variant skillList);

		void addNewSkill(Variant skill);

		void setSkillUp(Variant skillData);

		void SetSkilExp(int value);

		void ChangeGold();

		void SetSkillitems();
	}
}
