using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class LGGRAvatar : LGGRBaseImpls
	{
		private string _ani = "";

		private float _ori;

		private string _avatarid = "";

		private bool _visible = true;

		private float _moveScale = 0.8f;

		private float _moveScaleZ = 0.6f;

		protected string avatarid
		{
			get
			{
				return this._avatarid;
			}
			set
			{
				this._avatarid = value;
			}
		}

		protected joinWorldInfo selfinfo
		{
			get
			{
				return base.g_mgr.g_netM.getObject("DATA_JOIN_WORLD") as joinWorldInfo;
			}
		}

		public LGGRAvatar(muGRClient m) : base(m)
		{
		}

		public override void init()
		{
			base.init();
		}

		protected override void onSetGameCtrl()
		{
			this.m_gameCtrl.addEventListener(2100u, new Action<GameEvent>(this.initData));
			this.m_gameCtrl.addEventListener(2205u, new Action<GameEvent>(this.ondispose));
		}

		protected virtual void initData(GameEvent e)
		{
		}

		protected override void onSetDrawBase()
		{
			this.m_drawBase.addEventListener(2280u, new Action<GameEvent>(this.onClick));
		}

		private void RMVListener()
		{
			this.m_gameCtrl.removeEventListener(2100u, new Action<GameEvent>(this.initData));
			this.m_gameCtrl.removeEventListener(2205u, new Action<GameEvent>(this.ondispose));
			this.m_drawBase.removeEventListener(2280u, new Action<GameEvent>(this.onClick));
		}

		protected Vec2 getRealPos()
		{
			return new Vec2((this.m_gameCtrl as LGAvatar).x, (this.m_gameCtrl as LGAvatar).y);
		}

		protected string getAni()
		{
			bool flag = (int)(this.m_x * 100f) != (int)(this.m_xMoveto * 100f) || (int)(this.m_y * 100f) != (int)(this.m_yMoveto * 100f);
			string result;
			if (flag)
			{
				result = (this.m_gameCtrl as LGAvatar).getMoveAni();
			}
			else
			{
				result = (this.m_gameCtrl as LGAvatar).getAni();
			}
			return result;
		}

		protected bool visibleFlag()
		{
			return (this.m_gameCtrl as LGAvatar).visibleFlag();
		}

		private void onClick(GameEvent e)
		{
			Variant clickInfo = this.getClickInfo();
			this.m_gameCtrl.dispatchEvent(GameEvent.Createimmedi(2280u, this, clickInfo, false));
		}

		protected virtual Variant getClickInfo()
		{
			return null;
		}

		private void ondispose(GameEvent e)
		{
			base.dispose();
		}

		protected override void onDispose()
		{
			this.RMVListener();
		}

		private float getScaleVal(float fval, float tval)
		{
			return (1f - this._moveScale) * fval + this._moveScale * tval;
		}

		private float getScaleValZ(float fval, float tval)
		{
			return (1f - this._moveScaleZ) * fval + this._moveScaleZ * tval;
		}

		public void updateToTerrainZ()
		{
			Vec2 realPos = this.getRealPos();
			this.setPos(realPos.x, realPos.y);
			bool flag = this.m_x == this.m_xMoveto && this.m_y == this.m_yMoveto && this.m_z == this.m_zMoveto;
			if (!flag)
			{
				base.setPosX(this.m_xMoveto);
				base.setPosY(this.m_yMoveto);
				base.setPosZ(this.m_zMoveto);
			}
		}

		private void updatePos()
		{
			Vec2 realPos = this.getRealPos();
			this.setPos(realPos.x, realPos.y);
			bool flag = this.m_x == this.m_xMoveto && this.m_y == this.m_yMoveto && this.m_z == this.m_zMoveto;
			if (!flag)
			{
				base.setPosX(this.m_xMoveto);
				base.setPosY(this.m_yMoveto);
				float num = this.m_zMoveto - this.m_z;
				float num2 = this.m_zMoveto;
				bool flag2 = Math.Abs(num) < 0.6f;
				if (flag2)
				{
					num2 = this.m_z + num * 0.125f;
					bool flag3 = Math.Abs(this.m_zMoveto - num2) < 0.03125f;
					if (flag3)
					{
						num2 = this.m_zMoveto;
					}
				}
				base.setPosZ(num2);
				base.dispatchEvent(GameEvent.Createimmedi(2206u, this, null, false));
			}
		}

		private void updateInfo()
		{
			bool flag = this._visible != this.visibleFlag();
			if (flag)
			{
				this._visible = this.visibleFlag();
				Variant variant = new Variant();
				variant["visible"] = this.visibleFlag();
				base.dispatchEvent(GameEvent.Createimmedi(2200u, this, variant, false));
			}
		}

		protected virtual bool isRender()
		{
			return false;
		}

		protected virtual Variant getTitleConf(string tp, int showtp = 0, Variant showInfo = null)
		{
			return GameTools.createGroup(new Variant[]
			{
				"tp",
				tp,
				"showtp",
				showtp,
				"showInfo",
				showInfo
			});
		}
	}
}
