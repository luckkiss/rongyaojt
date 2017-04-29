using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class GrMonsterAvatar : GRAvatar
	{
		public GrMonsterAvatar(muGRClient ctrl) : base(ctrl)
		{
		}

		public new static IObjectPlugin create(IClientBase m)
		{
			return new GrMonsterAvatar(m as muGRClient);
		}

		protected override void onLoadFin()
		{
			SkAniMeshImpl skAniMeshImpl = (this.m_gr as GRCharacter3D).skmesh as SkAniMeshImpl;
			skAniMeshImpl.addScript(this._fileName + "(Clone)", "FightAniTempSC");
		}
	}
}
