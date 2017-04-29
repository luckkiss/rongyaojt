using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LGAvatarHero : LGAvatarGameInst
	{
		public int ownid;

		public bool isUserOwnHero = false;

		private SvrMonsterConfig svrMonsterConf
		{
			get
			{
				return (this.g_mgr.g_gameConfM as muCLientConfig).svrMonsterConf;
			}
		}

		private ClientGeneralConf localGeneral
		{
			get
			{
				return (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral;
			}
		}

		protected new MgrPlayerInfo playerInfos
		{
			get
			{
				return this.g_mgr.getObject("MgrPlayerInfo") as MgrPlayerInfo;
			}
		}

		public override string processName
		{
			get
			{
				return "LGAvatarMonster";
			}
			set
			{
				this._processName = value;
			}
		}

		public LGAvatarHero(gameManager m) : base(m)
		{
		}

		public override uint getIid()
		{
			return this.viewInfo["iid"]._uint;
		}

		public override uint getMid()
		{
			return this.viewInfo["mid"]._uint;
		}

		public void initData(Variant info)
		{
			this.viewInfo = info;
			int @int = info["mid"]._int;
			this.ownid = this.viewInfo["owner_cid"];
			this.isUserOwnHero = ((long)this.ownid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid));
			GRClient.instance.createHero(this);
			bool flag = this.viewInfo.ContainsKey("moving");
			if (flag)
			{
				base.addMoving(this.viewInfo["moving"]);
			}
			bool flag2 = this.viewInfo.ContainsKey("atking");
			if (flag2)
			{
				base.addAttack(this.viewInfo["atking"]["tar_iid"]._uint, null);
			}
			Variant monOriConf = this.localGeneral.GetMonOriConf(@int);
			bool flag3 = monOriConf != null;
			if (flag3)
			{
				this.viewInfo["ori"] = (float)monOriConf["octOri"]._int * 45f;
			}
			this.viewInfo["ori"] = 90f;
			base.dispatchEvent(GameEvent.Create(2100u, this, this.viewInfo, false));
		}

		private void _getOwnerInfoBack(uint cid, Variant data)
		{
			bool destroy = base.destroy;
			if (!destroy)
			{
				Variant ownerMonConf = this.localGeneral.GetOwnerMonConf(this.m_conf["mid"]);
				string languageText = LanguagePack.getLanguageText("monName", this.m_conf["mid"]._str);
				string languageText2 = LanguagePack.getLanguageText("common", "belongsto");
				this.viewInfo["name"] = DebugTrace.Printf(languageText2, new string[]
				{
					data["name"]._str,
					languageText
				});
				this.viewInfo["titleConf"] = base.getTitleConf("name", ownerMonConf["showtp"]._int, GameTools.createGroup(new Variant[]
				{
					"text",
					this.viewInfo["name"]
				}));
				base.dispatchEvent(GameEvent.Create(2100u, this, this.viewInfo, false));
			}
		}

		protected override void onMovePosSingleReach(GameEvent e)
		{
			base.setMeshAni("idle", 0);
		}

		protected override void playAttackAni(bool criatk = false)
		{
			bool flag = this.m_ani == "run" || this.m_ani == "idle";
			if (flag)
			{
				base.playAttackAni(criatk);
				Variant monster = MonsterConfig.instance.getMonster(this.viewInfo["mid"]._str);
				bool flag2 = monster.ContainsKey("atk_effect");
				if (flag2)
				{
					MapEffMgr.getInstance().play(monster["atk_effect"], base.pGameobject.transform, 450f - this.lg_ori_angle, 0f);
				}
			}
		}

		public bool IsBoss()
		{
			return false;
		}

		public override void Die(Variant data)
		{
			base.Die(data);
		}
	}
}
