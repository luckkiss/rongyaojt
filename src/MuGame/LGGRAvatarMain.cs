using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LGGRAvatarMain : LGGRAvatar, IObjectPlugin
	{
		private uint _iid = 0u;

		private lgSelfPlayer gameCtrl
		{
			get
			{
				return this.m_gameCtrl as lgSelfPlayer;
			}
		}

		public LGGRAvatarMain(muGRClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGRAvatarMain(m as muGRClient);
		}

		protected override void onSetGameCtrl()
		{
			base.onSetGameCtrl();
		}

		protected override void initData(GameEvent e)
		{
			Variant data = e.data;
			this._iid = data["iid"];
			base.avatarid = this.m_mgr.getAvatarId(data["sex"], data["carr"]);
			Variant entityConf = this.m_mgr.getEntityConf(base.avatarid);
			Variant variant = new Variant();
			variant["iid"] = this._iid;
			bool flag = data.ContainsKey("titleConf");
			if (flag)
			{
				variant["titleConf"] = data["titleConf"];
			}
			else
			{
				variant["titleConf"] = this.getTitleConf("name", 0, GameTools.createGroup(new Variant[]
				{
					"text",
					data["name"]
				}));
			}
			variant["name"] = data["name"];
			variant["equip"] = data["equip"];
			variant["avatarConf"] = entityConf;
			variant["max_hp"] = ModelBase<PlayerModel>.getInstance().max_hp;
			variant["hp"] = ModelBase<PlayerModel>.getInstance().hp;
			variant["ori"] = 0;
			variant["ani"] = "idle";
			variant["loop"] = true;
			this.setPos((this.m_gameCtrl as LGAvatar).x, (this.m_gameCtrl as LGAvatar).y);
			Vec3 poss = base.getPoss();
			variant["x"] = poss.x;
			variant["y"] = poss.y;
			variant["z"] = poss.z;
			variant["isMain"] = true;
			base.dispatchEvent(GameEvent.Createimmedi(2100u, this, variant, false));
		}

		public override void setPos(float x, float y)
		{
			base.setPos(x, y);
		}

		protected override bool isRender()
		{
			return this._iid > 0u;
		}
	}
}
