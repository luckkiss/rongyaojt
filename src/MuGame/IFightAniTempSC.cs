using System;

namespace MuGame
{
	public interface IFightAniTempSC
	{
		void onAttackPoint(int skillid);

		void onAttackBegin(int num);

		void onAttackShake_time_num_str(string pram);

		void onAttack_sound(int id);
	}
}
