using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LGAvatarMonster : LGAvatarGameInst
	{
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

		public LGAvatarMonster(gameManager m) : base(m)
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
			bool flag = !this.viewInfo.ContainsKey("name");
			if (flag)
			{
				this.viewInfo["name"] = MonsterConfig.instance.getMonster(string.Concat(@int))["name"];
			}
			base.setConfig(MonsterConfig.instance.getMonster(string.Concat(@int)));
			this.g_mgr.g_sceneM.dispatchEvent(GameEvent.Createimmedi(2184u, this, null, false));
			bool flag2 = this.viewInfo.ContainsKey("moving");
			if (flag2)
			{
				base.addMoving(this.viewInfo["moving"]);
			}
			bool flag3 = this.viewInfo.ContainsKey("atking");
			if (flag3)
			{
				base.addAttack(this.viewInfo["atking"]["tar_iid"]._uint, null);
			}
			Variant monOriConf = this.localGeneral.GetMonOriConf(@int);
			bool flag4 = monOriConf != null;
			if (flag4)
			{
				this.viewInfo["ori"] = (float)monOriConf["octOri"]._int * 45f;
			}
			this.viewInfo["ori"] = 90f;
			bool flag5 = this.data.ContainsKey("dressments");
			if (flag5)
			{
				this.grAvatar.m_nSex = this.data["sex"]._int;
				List<Variant> arr = this.data["dressments"]._arr;
				foreach (Variant current in arr)
				{
					int int2 = current["dressid"]._int;
					int num = int2 % 1000 - 1;
					bool flag6 = num >= 0 && num < this.grAvatar.m_nOtherDress.Length;
					if (flag6)
					{
						this.grAvatar.m_nOtherDress[num] = int2;
					}
				}
			}
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

		protected override void onClick(GameEvent e)
		{
			base.onClick(e);
			GameTools.PrintNotice(string.Concat(new object[]
			{
				"monster click mid[",
				this.getMid(),
				"] iid[",
				this.getIid(),
				"]!"
			}));
			base.selfPlayer.onSelectMonster(this);
		}

		protected override void playDeadSound()
		{
			Variant monster = MonsterConfig.instance.getMonster(this.viewInfo["mid"] ?? "");
			bool flag = monster.ContainsKey("dead_music");
			if (flag)
			{
				MediaClient.instance.PlaySoundUrl("media/dead/" + monster["dead_music"], false, null);
			}
		}

		protected override void playAttackAni(bool criatk = false)
		{
			base.playAttackAni(criatk);
			Variant monster = MonsterConfig.instance.getMonster(this.viewInfo["mid"]);
			bool flag = monster.ContainsKey("atk_effect");
			if (flag)
			{
				MapEffMgr.getInstance().play(monster["atk_effect"], base.pGameobject.transform, 450f - this.lg_ori_angle, 0f);
			}
		}

		public override void Die(Variant data)
		{
			base.Die(data);
		}

		public bool IsBoss()
		{
			return false;
		}
	}
}
