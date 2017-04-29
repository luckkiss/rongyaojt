using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LGGRAvatarOtherPlayer : LGGRAvatar, IObjectPlugin
	{
		private uint _iid = 0u;

		private LGAvatarOther gameCtrl
		{
			get
			{
				return this.m_gameCtrl as LGAvatarOther;
			}
		}

		public LGGRAvatarOtherPlayer(muGRClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGRAvatarOtherPlayer(m as muGRClient);
		}

		protected override void initData(GameEvent e)
		{
			Variant data = e.data;
			this._iid = data["iid"];
			uint sex = data["sex"];
			uint carr = data["carr"];
			base.avatarid = this.m_mgr.getAvatarId(sex, carr);
			Variant entityConf = this.m_mgr.getEntityConf(base.avatarid);
			Variant variant = new Variant();
			variant["isCreateName"] = true;
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
			variant["max_hp"] = this.gameCtrl.max_hp;
			variant["max_mp"] = this.gameCtrl.max_mp;
			variant["hp"] = this.gameCtrl.hp;
			variant["mp"] = this.gameCtrl.mp;
			variant["ori"] = 0;
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
