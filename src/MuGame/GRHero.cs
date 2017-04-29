using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class GRHero : GRAvatar
	{
		public GRHero(muGRClient ctrl) : base(ctrl)
		{
		}

		public new static IObjectPlugin create(IClientBase m)
		{
			return new GRHero(m as muGRClient);
		}

		protected override void onLoadFin()
		{
			SkAniMeshImpl skAniMeshImpl = (this.m_gr as GRCharacter3D).skmesh as SkAniMeshImpl;
			bool isUserOwnHero = (this.lgAvatar as LGAvatarHero).isUserOwnHero;
			if (isUserOwnHero)
			{
				skAniMeshImpl.addScript(this._fileName + "(Clone)", "FightAniUserTempSC");
			}
			else
			{
				skAniMeshImpl.addScript(this._fileName + "(Clone)", "FightAniTempSC");
			}
			base.onLoadFin();
		}
	}
}
