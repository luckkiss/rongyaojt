using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public abstract class GRClient : clientBase
	{
		private GRWorld3D _world;

		private Variant _size = new Variant();

		public static GRClient instance;

		private Dictionary<string, Variant> _mapInfos = null;

		private GraphManager GraphMgr
		{
			get
			{
				return CrossApp.singleton.getPlugin("graph") as GraphManager;
			}
		}

		public Variant getMapInfo
		{
			get
			{
				return this.GraphMgr.MapInfo;
			}
		}

		public GRWorld3D world
		{
			get
			{
				bool flag = this._world != null;
				GRWorld3D result;
				if (flag)
				{
					result = this._world;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public GRClient(gameMain m) : base(m)
		{
			GRClient.instance = this;
		}

		public Variant getMapConf(string mapid)
		{
			Variant mapConf = this.GraphMgr.getMapConf(mapid);
			bool flag = mapConf == null;
			if (flag)
			{
				DebugTrace.print("  >>> ERR!  getMapConf mapid[" + mapid + "] null!");
			}
			return mapConf;
		}

		public Variant getGMapInfo(string id)
		{
			bool flag = this._mapInfos == null;
			if (flag)
			{
				this._mapInfos = new Dictionary<string, Variant>();
				foreach (Variant current in this.GraphMgr.MapInfo._arr)
				{
					bool flag2 = current.ContainsKey("camera");
					if (flag2)
					{
						current["camera"] = current["camera"]._arr[0];
					}
					this._mapInfos[current["id"]._str] = current;
				}
			}
			bool flag3 = !this._mapInfos.ContainsKey(id);
			Variant result;
			if (flag3)
			{
				result = null;
			}
			else
			{
				result = this._mapInfos[id];
			}
			return result;
		}

		public Variant getGMapCameraInfo(string id)
		{
			Variant gMapInfo = this.getGMapInfo(id);
			bool flag = gMapInfo == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !gMapInfo.ContainsKey("camera");
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = gMapInfo["camera"];
				}
			}
			return result;
		}

		public Variant getEntityConf(string avatarid)
		{
			Variant characterConf = this.GraphMgr.getCharacterConf(avatarid);
			bool flag = characterConf == null;
			if (flag)
			{
				DebugTrace.print("  >>> ERR!  getCharacterConf avatarid[" + avatarid + "] null!");
			}
			return characterConf;
		}

		public Variant getAvatarConf(string chaid, string avaID, bool isleft = false)
		{
			if (isleft)
			{
				avaID += "L";
			}
			Variant avatarConf = this.GraphMgr.getAvatarConf(chaid, avaID);
			bool flag = avatarConf == null;
			if (flag)
			{
				DebugTrace.print(string.Concat(new string[]
				{
					"  >>> ERR!  getCharacterConf  chaid[",
					chaid,
					"] avaID[",
					avaID,
					"] null!"
				}));
			}
			return avatarConf;
		}

		public Variant getEffectConf(string effid)
		{
			Variant effectConf = this.GraphMgr.getEffectConf(effid);
			bool flag = effectConf == null;
			if (flag)
			{
				DebugTrace.print("  >>> ERR!  getEffectConf  effid[" + effid + "] null!");
			}
			return effectConf;
		}

		public override void init()
		{
			this._size.setToDct();
			this._size["w"] = 1400;
			this._size["h"] = 800;
			this._world = new GRWorld3D("mainWorld", this.GraphMgr);
			this.onInit();
		}

		protected abstract void onInit();

		public Variant getSize()
		{
			return this._size;
		}

		public void setScreenSize(int w, int h)
		{
			this._size["w"] = w;
			this._size["h"] = h;
		}

		public virtual string getNPCAvatar(uint npcid)
		{
			return "105";
		}

		public virtual string getAvatarId(uint sex, uint carr)
		{
			return "107";
		}

		public virtual string getMonAvatarId(uint mid)
		{
			return "106";
		}

		public virtual string getHeroAvararId(uint mid)
		{
			return "10001";
		}

		public virtual avatarInfo getEqpAvatarInfo(uint tpid, Variant eqp)
		{
			return null;
		}

		public abstract float getZ(float x, float y);

		public Vec3 trans3DPos(float x, float y)
		{
			return new Vec3
			{
				x = x,
				y = y,
				z = this.getZ(x, y)
			};
		}

		public void createMapObject(lgGDBase mapCtrl)
		{
			this.createSceneObjectPair(mapCtrl, "SCENE_MAP_CTRL", "SCENE_MAP_DRAW");
		}

		public void createMainAvatarObject(lgGDBase avatarCtrl)
		{
			this.createSceneObjectPair(avatarCtrl, "SCENE_MAIN_PLAY_CTRL", "SCENE_MAIN_PLAY_DRAW");
		}

		public void createCamera(lgGDBase cameraCtrl)
		{
			this.createSceneObjectPair(cameraCtrl, "SCENE_CAMERA_CTRL", "SCENE_CAMERA_DRAW");
		}

		public void createOtherPlayer(lgGDBase otherPlayerCtrl)
		{
			this.createSceneObjectPair(otherPlayerCtrl, "SCENE_OTHER_PLAY_CTRL", "SCENE_OTHER_PLAY_DRAW");
		}

		public void createMonster(lgGDBase monsterCtrl)
		{
			this.createSceneObjectPair(monsterCtrl, "SCENE_MONSTER_CTRL", "SCENE_MONSTER_DRAW");
		}

		public void createHero(lgGDBase heroCtrl)
		{
			this.createSceneObjectPair(heroCtrl, "SCENE_HERO_CTRL", "SCENE_HERO_DRAW");
		}

		public void createNPC(lgGDBase npcCtrl)
		{
			this.createSceneObjectPair(npcCtrl, "SCENE_NPC_CTRL", "SCENE_NPC_DRAW");
		}

		private void createSceneObjectPair(lgGDBase mapCtrl, string ctrlName, string drawName)
		{
			LGGRBaseImpls lGGRBaseImpls = base.createInst(ctrlName, false) as LGGRBaseImpls;
			GRBaseImpls gRBaseImpls = base.createInst(drawName, false) as GRBaseImpls;
			bool flag = lGGRBaseImpls == null;
			if (flag)
			{
				DebugTrace.print("GRClient create ctrlName[" + ctrlName + "] null!");
			}
			else
			{
				bool flag2 = gRBaseImpls == null;
				if (flag2)
				{
					DebugTrace.print("GRClient create drawName[" + drawName + "] null!");
				}
				else
				{
					mapCtrl.initGr(gRBaseImpls, lGGRBaseImpls);
					gRBaseImpls.initLg(mapCtrl);
					lGGRBaseImpls.init();
					gRBaseImpls.init();
					lGGRBaseImpls.setGameCtrl(mapCtrl);
					lGGRBaseImpls.setDrawBase(gRBaseImpls);
					gRBaseImpls.setSceneCtrl(lGGRBaseImpls);
					bool flag3 = "SCENE_MAIN_PLAY_CTRL" == ctrlName;
					if (flag3)
					{
						base.g_processM.addRender(lGGRBaseImpls, true);
						base.g_processM.addRender(gRBaseImpls, true);
					}
					else
					{
						base.g_processM.addRender(lGGRBaseImpls, false);
						base.g_processM.addRender(gRBaseImpls, false);
					}
				}
			}
		}

		public IGREffectParticles createEffect(string id)
		{
			IGREffectParticles iGREffectParticles = this._world.createEntity(Define.GREntityType.EFFECT_PARTICLE, id) as IGREffectParticles;
			bool flag = id.Length < 20;
			IGREffectParticles result;
			string assetPath;
			if (flag)
			{
				Variant effectConf = this.GraphMgr.getEffectConf(id);
				bool flag2 = effectConf == null;
				if (flag2)
				{
					GameTools.PrintError("createEffect[" + id + "] no conf ERR!");
					result = null;
					return result;
				}
				assetPath = effectConf["file"]._str;
			}
			else
			{
				assetPath = id;
			}
			iGREffectParticles.asset = os.asset.getAsset<IAssetParticles>(assetPath);
			bool flag3 = iGREffectParticles == null;
			if (flag3)
			{
				GameTools.PrintError("createEffect[" + id + "] ERR!");
				result = null;
			}
			else
			{
				result = iGREffectParticles;
			}
			return result;
		}

		public IGRMap createGraphMap(string mapid)
		{
			return this._world.createMap(mapid);
		}

		public GRCamera3D getGraphCamera()
		{
			return this._world.cam;
		}

		public GRCharacter3D createGraphChar(Variant conf)
		{
			GRCharacter3D gRCharacter3D = this._world.createEntity(Define.GREntityType.CHARACTER) as GRCharacter3D;
			gRCharacter3D.load(conf, null, null);
			return gRCharacter3D;
		}

		public void deleteEntity(IGREntity ent)
		{
			this._world.deleteEntity(ent);
		}
	}
}
