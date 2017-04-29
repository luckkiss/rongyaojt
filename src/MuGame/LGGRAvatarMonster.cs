using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LGGRAvatarMonster : LGGRAvatar, IObjectPlugin
	{
		private uint _iid = 0u;

		private LGAvatarMonster gameCtrl
		{
			get
			{
				return this.m_gameCtrl as LGAvatarMonster;
			}
		}

		public LGGRAvatarMonster(muGRClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGRAvatarMonster(m as muGRClient);
		}

		protected override void initData(GameEvent e)
		{
			Variant data = e.data;
			this._iid = data["iid"];
			uint @uint = data["mid"]._uint;
			bool flag = data.ContainsKey("sex");
			if (flag)
			{
				base.avatarid = this.m_mgr.getAvatarId(data["sex"], 1u);
			}
			else
			{
				base.avatarid = this.m_mgr.getMonAvatarId(@uint);
			}
			Variant entityConf = this.m_mgr.getEntityConf(base.avatarid);
			bool flag2 = entityConf == null;
			if (flag2)
			{
			}
			Variant variant = new Variant();
			variant["isCreateName"] = true;
			bool flag3 = data.ContainsKey("titleConf");
			if (flag3)
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
			variant["max_mp"] = this.gameCtrl.max_mp;
			variant["hp"] = this.gameCtrl.hp;
			variant["mp"] = this.gameCtrl.mp;
			bool flag4 = data.ContainsKey("ori");
			if (flag4)
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
