using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LGGRCamera : LGGRBaseImpls, IObjectPlugin
	{
		private lgSelfPlayer selfPlayer
		{
			get
			{
				return base.g_mgr.g_gameM.getObject("LG_MAIN_PLAY") as lgSelfPlayer;
			}
		}

		private LGCamera gameCtrl
		{
			get
			{
				return this.m_gameCtrl as LGCamera;
			}
		}

		public LGGRCamera(muGRClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGRCamera(m as muGRClient);
		}

		private void updateMainPlayerPos(GameEvent e)
		{
			base.dispatchEvent(GameEvent.Createimmedi(2206u, this, e.orgdata, false));
		}

		private void obj_mask(GameEvent e)
		{
			base.dispatchEvent(GameEvent.Create(2226u, this, e.orgdata, false));
		}

		protected override void onSetGameCtrl()
		{
			this.gameCtrl.addEventListener(2205u, new Action<GameEvent>(this.ondispose));
			this.gameCtrl.addEventListener(2170u, new Action<GameEvent>(this.onDataInit));
			this.gameCtrl.addEventListener(2206u, new Action<GameEvent>(this.updateMainPlayerPos));
			this.gameCtrl.addEventListener(2226u, new Action<GameEvent>(this.obj_mask));
		}

		protected override void onSetDrawBase()
		{
		}

		private void onDataInit(GameEvent e)
		{
			Vec3 data = base.g_mgr.trans3DPos(this.selfPlayer.x, this.selfPlayer.y);
			base.dispatchEvent(GameEvent.Create(2100u, this, data, false));
		}

		private void ondispose(GameEvent e)
		{
			base.dispose();
		}
	}
}
