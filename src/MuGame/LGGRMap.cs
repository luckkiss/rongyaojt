using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LGGRMap : LGGRBaseImpls, IObjectPlugin
	{
		private bool _mapSpriteCreatFlag = false;

		public static LGGRMap instance;

		public static bool haveListener = false;

		private Variant flyEff = new Variant();

		private LGMap gameCtrl
		{
			get
			{
				return this.m_gameCtrl as LGMap;
			}
		}

		public LGGRMap(muGRClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			bool flag = LGGRMap.instance == null;
			if (flag)
			{
				LGGRMap.instance = new LGGRMap(m as muGRClient);
			}
			return LGGRMap.instance;
		}

		protected override void onSetGameCtrl()
		{
			bool flag = LGGRMap.haveListener;
			if (!flag)
			{
				this.gameCtrl.addEventListener(2161u, new Action<GameEvent>(this.onDataInit));
				this.gameCtrl.addEventListener(2162u, new Action<GameEvent>(this.onViewSizeChange));
				this.gameCtrl.addEventListener(2160u, new Action<GameEvent>(this.onChangeMap));
				this.gameCtrl.addEventListener(2163u, new Action<GameEvent>(this.onLinkAdd));
				this.gameCtrl.addEventListener(2166u, new Action<GameEvent>(this.onAddFlyEff));
				LGGRMap.haveListener = true;
			}
		}

		protected override void onSetDrawBase()
		{
			base.addEventListener(2165u, new Action<GameEvent>(this.onLoadReady));
		}

		private void onLoadReady(GameEvent e)
		{
			base.g_mgr.dispatchEvent(e);
		}

		private void onDataInit(GameEvent e)
		{
			base.dispatchEvent(GameEvent.Create(2100u, this, e.data, false));
		}

		private void onChangeMap(GameEvent e)
		{
			base.dispatchEvent(GameEvent.Create(2160u, this, e.data, false));
		}

		private void onLinkAdd(GameEvent e)
		{
			base.dispatchEvent(GameEvent.Create(2163u, this, e.data, false));
		}

		private void onAddFlyEff(GameEvent e)
		{
			this.flyEff.pushBack(e.data);
			base.dispatchEvent(GameEvent.Create(2166u, this, e.data, false));
		}

		public override void updateProcess(float tmSlice)
		{
		}

		protected bool isRender()
		{
			return this._mapSpriteCreatFlag && this.gameCtrl != null && this.gameCtrl.showFlag();
		}

		public void onViewSizeChange(GameEvent e)
		{
			base.dispatchEvent(GameEvent.Createimmedi(2207u, this, e.data, false));
		}

		private void ondispose(GameEvent e)
		{
			base.dispose();
		}
	}
}
