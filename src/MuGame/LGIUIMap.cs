using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public interface LGIUIMap
	{
		void NewAchieve(Variant data);

		void updateNpcMisState(LGAvatarBase c);

		void addCharacter(LGAvatarBase c);

		void removeCharacter(int iid);

		void changeMap(Variant mapData);

		void updatePath(Variant path);

		void removePath();

		void stopMove();

		void updateMove(LGAvatarBase c, Variant data = null);

		void Respawn(LGAvatarBase c);
	}
}
