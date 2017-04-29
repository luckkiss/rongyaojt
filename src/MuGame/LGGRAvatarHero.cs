using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LGGRAvatarHero : LGGRAvatar, IObjectPlugin
	{
		private uint _iid = 0u;

		private LGAvatarHero gameCtrl
		{
			get
			{
				return this.m_gameCtrl as LGAvatarHero;
			}
		}

		public LGGRAvatarHero(muGRClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGRAvatarHero(m as muGRClient);
		}

		protected override void initData(GameEvent e)
		{
			Variant data = e.data;
			this._iid = data["iid"];
			uint @uint = data["mid"]._uint;
			base.avatarid = this.m_mgr.getHeroAvararId(@uint);
			Variant entityConf = this.m_mgr.getEntityConf(base.avatarid);
			bool flag = entityConf == null;
			if (flag)
			{
			}
			Variant variant = new Variant();
			variant["isCreateName"] = true;
			bool flag2 = data.ContainsKey("titleConf");
			if (flag2)
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
			variant["equip"] = null;
			variant["avatarConf"] = entityConf;
			variant["max_hp"] = this.gameCtrl.max_hp;
			variant["hp"] = this.gameCtrl.hp;
			variant["mp"] = this.gameCtrl.mp;
			bool flag3 = data.ContainsKey("ori");
			if (flag3)
			{
				variant["ori"] = data["ori"];
			}
			else
			{
				variant["ori"] = 0;
			}
			variant["ani"] = "idle";
			variant["loop"] = true;
			this.setPos((this.m_gameCtrl as LGAvatar).x, (this.m_gameCtrl as LGAvatar).y);
			variant["x"] = this.m_x;
			variant["y"] = this.m_z;
			variant["z"] = this.m_y;
			base.dispatchEvent(GameEvent.Create(2100u, this, variant, false));
		}

		protected override bool isRender()
		{
			return this._iid > 0u;
		}
	}
}
