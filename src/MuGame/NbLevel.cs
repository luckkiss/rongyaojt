using GameFramework;
using System;

namespace MuGame
{
	internal class NbLevel : NewbieTeachItem
	{
		public static NbLevel create(string[] arr)
		{
			return new NbLevel();
		}

		public override void addListener()
		{
			ModelBase<PlayerModel>.getInstance().addEventListener(PlayerModel.ON_LEVEL_CHANGED, new Action<GameEvent>(base.onHanlde));
		}

		public override void removeListener()
		{
			ModelBase<PlayerModel>.getInstance().removeEventListener(PlayerModel.ON_LEVEL_CHANGED, new Action<GameEvent>(base.onHanlde));
		}
	}
}
