using System;

namespace MuGame
{
	public interface LGIUISystem
	{
		void InitSystemSet();

		void OnStartAuto();

		void OnLearnSkill(uint sid);

		void use_skill_suss(int sid);

		void OnSelfRespawn();

		void OnPlayerDie();

		void SetAutoStates();
	}
}
