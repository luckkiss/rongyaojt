using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class muGRClient : GRClient
	{
		private SvrItemConfig svrItemConf
		{
			get
			{
				return base.g_gameConfM.getObject("SvrItem") as SvrItemConfig;
			}
		}

		private SvrNPCConfig svrNpcConf
		{
			get
			{
				return base.g_gameConfM.getObject("SvrNPC") as SvrNPCConfig;
			}
		}

		private SvrMonsterConfig svrMonConf
		{
			get
			{
				return base.g_gameConfM.getObject("SvrMonsters") as SvrMonsterConfig;
			}
		}

		private LGMap lgmap
		{
			get
			{
				return base.g_gameM.getObject("LG_MAP") as LGMap;
			}
		}

		public muGRClient(gameMain m) : base(m)
		{
		}

		protected override void onInit()
		{
			base.regCreator("SCENE_MAP_CTRL", new Func<IClientBase, IObjectPlugin>(LGGRMap.create));
			base.regCreator("SCENE_MAP_DRAW", new Func<IClientBase, IObjectPlugin>(GRMap.create));
			base.regCreator("SCENE_CAMERA_CTRL", new Func<IClientBase, IObjectPlugin>(LGGRCamera.create));
			base.regCreator("SCENE_CAMERA_DRAW", new Func<IClientBase, IObjectPlugin>(GRCamera.create));
			base.regCreator("SCENE_MAIN_PLAY_CTRL", new Func<IClientBase, IObjectPlugin>(LGGRAvatarMain.create));
			base.regCreator("SCENE_MAIN_PLAY_DRAW", new Func<IClientBase, IObjectPlugin>(GRUsrplayerAvatar.create));
			base.regCreator("SCENE_NPC_CTRL", new Func<IClientBase, IObjectPlugin>(sceneCtrlAvatarNpc.create));
			base.regCreator("SCENE_NPC_DRAW", new Func<IClientBase, IObjectPlugin>(GRAvatar.create));
			base.regCreator("SCENE_OTHER_PLAY_CTRL", new Func<IClientBase, IObjectPlugin>(LGGRAvatarOtherPlayer.create));
			base.regCreator("SCENE_OTHER_PLAY_DRAW", new Func<IClientBase, IObjectPlugin>(GrPlayerAvatar.create));
			base.regCreator("SCENE_MONSTER_CTRL", new Func<IClientBase, IObjectPlugin>(LGGRAvatarMonster.create));
			base.regCreator("SCENE_MONSTER_DRAW", new Func<IClientBase, IObjectPlugin>(GrMonsterAvatar.create));
			base.regCreator("SCENE_HERO_CTRL", new Func<IClientBase, IObjectPlugin>(LGGRAvatarHero.create));
			base.regCreator("SCENE_HERO_DRAW", new Func<IClientBase, IObjectPlugin>(GRHero.create));
			base.addEventListener(2183u, new Action<GameEvent>(this.onCreateMainPlay));
			base.addEventListener(2180u, new Action<GameEvent>(this.onCreateMainCamera));
			base.addEventListener(2181u, new Action<GameEvent>(this.onCreateMap));
			base.addEventListener(2182u, new Action<GameEvent>(this.onCreateNPC));
			base.addEventListener(2184u, new Action<GameEvent>(this.onCreateMonster));
			base.addEventListener(2186u, new Action<GameEvent>(this.onCreateHero));
			base.addEventListener(2185u, new Action<GameEvent>(this.onCreateOtherPlayer));
		}

		public IGREffectParticles createEffect(string effID, float x, float y, bool loop = false)
		{
			IGREffectParticles iGREffectParticles = base.createEffect(effID);
			bool flag = iGREffectParticles == null;
			IGREffectParticles result;
			if (flag)
			{
				result = null;
			}
			else
			{
				iGREffectParticles.loop = loop;
				iGREffectParticles.x = (float)GameTools.inst.pixelToUnit((double)x);
				iGREffectParticles.z = (float)GameTools.inst.pixelToUnit((double)y);
				iGREffectParticles.y = this.getZ(x, y);
				result = iGREffectParticles;
			}
			return result;
		}

		private void onCreateMainPlay(GameEvent e)
		{
			base.createMainAvatarObject(e.target as lgGDBase);
		}

		private void onCreateMainCamera(GameEvent e)
		{
			base.createCamera(e.target as lgGDBase);
		}

		private void onCreateMap(GameEvent e)
		{
			base.createMapObject(e.target as lgGDBase);
		}

		private void onCreateNPC(GameEvent e)
		{
			base.createNPC(e.target as lgGDBase);
		}

		private void onCreateMonster(GameEvent e)
		{
			base.createMonster(e.target as lgGDBase);
		}

		private void onCreateHero(GameEvent e)
		{
			base.createHero(e.target as lgGDBase);
		}

		private void onCreateOtherPlayer(GameEvent e)
		{
			base.createOtherPlayer(e.target as lgGDBase);
		}

		public override string getNPCAvatar(uint npcid)
		{
			Variant variant = this.svrNpcConf.get_npc_data((int)npcid);
			bool flag = variant == null;
			string result;
			if (flag)
			{
				result = "1070";
			}
			else
			{
				result = variant["obj"]._str;
			}
			return result;
		}

		public override string getAvatarId(uint sex, uint carr)
		{
			bool flag = sex == 0u;
			string result;
			if (flag)
			{
				result = "0";
			}
			else
			{
				result = "1";
			}
			return result;
		}

		public override string getMonAvatarId(uint mid)
		{
			Variant monster = MonsterConfig.instance.getMonster(string.Concat(mid));
			bool flag = monster == null || !monster.ContainsKey("obj");
			string result;
			if (flag)
			{
				result = "1070";
			}
			else
			{
				result = monster["obj"]._str;
			}
			return result;
		}

		public override string getHeroAvararId(uint hid)
		{
			SXML sXML = XMLMgr.instance.GetSXML("hero.hero", "id==" + hid);
			uint @uint = sXML.getUint("obj");
			bool flag = sXML == null;
			string result;
			if (flag)
			{
				result = "1010";
			}
			else
			{
				result = @uint.ToString();
			}
			return result;
		}

		public override avatarInfo getEqpAvatarInfo(uint tpid, Variant eqp)
		{
			Variant variant = this.svrItemConf.get_item_conf(tpid);
			bool flag = variant == null;
			avatarInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new avatarInfo
				{
					iid = eqp["id"]._uint,
					tpid = tpid,
					avatarid = variant["conf"]["avatar"]._str,
					pos = variant["conf"]["pos"]._int
				};
			}
			return result;
		}

		public override float getZ(float x, float y)
		{
			return ScenceUtils.getGroundHight(x, y);
		}
	}
}
