using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class GRUsrplayerAvatar : GrPlayerAvatar
	{
		public GRUsrplayerAvatar(muGRClient ctrl) : base(ctrl)
		{
		}

		public new static IObjectPlugin create(IClientBase m)
		{
			return new GRUsrplayerAvatar(m as muGRClient);
		}

		protected override void onLoadFin()
		{
			SkAniMeshImpl skAniMeshImpl = (this.m_gr as GRCharacter3D).skmesh as SkAniMeshImpl;
			skAniMeshImpl.addScript("player(Clone)", "FightAniUserTempSC");
		}
	}
}
