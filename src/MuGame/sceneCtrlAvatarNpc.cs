using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class sceneCtrlAvatarNpc : LGGRAvatar, IObjectPlugin
	{
		private uint _npcid = 0u;

		private LGAvatarNpc gameCtrl
		{
			get
			{
				return this.m_gameCtrl as LGAvatarNpc;
			}
		}

		public sceneCtrlAvatarNpc(muGRClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new sceneCtrlAvatarNpc(m as muGRClient);
		}

		protected override void initData(GameEvent e)
		{
			Variant data = e.data;
			this._npcid = data["nid"];
			base.avatarid = this.m_mgr.getNPCAvatar(this._npcid);
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
			variant["avatarConf"] = entityConf;
			bool flag2 = data.ContainsKey("ori");
			if (flag2)
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

		protected override Variant getClickInfo()
		{
			Variant variant = new Variant();
			variant["npcid"] = this._npcid;
			return variant;
		}

		protected override bool isRender()
		{
			return false;
		}
	}
}
