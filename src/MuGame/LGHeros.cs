using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LGHeros : lgGDBase, IObjectPlugin
	{
		public static LGHeros instacne;

		private Dictionary<uint, LGAvatarHero> _heros;

		private Dictionary<uint, Variant> _heroInfos;

		private Dictionary<uint, Variant> _heroWaitCreateInfos = new Dictionary<uint, Variant>();

		private bool _initFlag = false;

		private bool _mapChageFlag = false;

		private int herologhtIdx = 0;

		private lgSelfPlayer lgMainPlayer
		{
			get
			{
				return this.g_mgr.getObject("LG_MAIN_PLAY") as lgSelfPlayer;
			}
		}

		public LGHeros(gameManager m) : base(m)
		{
			LGHeros.instacne = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGHeros(m as gameManager);
		}

		public Dictionary<uint, LGAvatarHero> getHeros()
		{
			return this._heros;
		}

		public LGAvatarHero getHeroById(uint id)
		{
			bool flag = !this._heros.ContainsKey(id);
			LGAvatarHero result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._heros[id];
			}
			return result;
		}

		public override void init()
		{
			this._heros = new Dictionary<uint, LGAvatarHero>();
			this._heroInfos = new Dictionary<uint, Variant>();
			this.g_mgr.g_netM.addEventListener(56u, new Action<GameEvent>(this.onSpriteLeaveZone));
			this.g_mgr.g_gameM.addEventListenerCL("LG_JOIN_WORLD", 3035u, new Action<GameEvent>(this.onMapchg));
			this.g_mgr.g_gameM.addEventListenerCL("LG_JOIN_WORLD", 3034u, new Action<GameEvent>(this.onMapchg));
			this.g_mgr.g_netM.addEventListener(20u, new Action<GameEvent>(this.onRespawn));
			this.g_mgr.g_processM.addProcess(new processStruct(new Action<float>(this.update), "LGHeros", false, false), false);
		}

		private void update(float tmSlice)
		{
			bool flag = this._heroWaitCreateInfos.Count <= 0;
			if (!flag)
			{
				using (Dictionary<uint, Variant>.ValueCollection.Enumerator enumerator = this._heroWaitCreateInfos.Values.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						Variant current = enumerator.Current;
						this.createHero(current);
						uint @uint = current["iid"]._uint;
						this._heroWaitCreateInfos.Remove(@uint);
					}
				}
			}
		}

		private void onMapchgBegin(GameEvent e)
		{
			this._mapChageFlag = false;
			this._initFlag = false;
			this.clear();
		}

		public void clear()
		{
			debug.Log("dele!!!!!!!!!!!!!!!!!!!!!!!!  " + debug.count);
			foreach (LGAvatarHero current in this._heros.Values)
			{
				this.g_mgr.g_processM.removeRender(current, false);
				current.dispose();
			}
			this._heros.Clear();
			this._heroInfos.Clear();
		}

		public void onMapchg(GameEvent e)
		{
			this._mapChageFlag = true;
			this.createZoneSprites();
		}

		private void onSpriteLeaveZone(GameEvent e)
		{
			Variant data = e.data;
			using (List<Variant>.Enumerator enumerator = data["iidary"]._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					uint key = enumerator.Current;
					bool flag = !this._heroInfos.ContainsKey(key);
					if (!flag)
					{
						this._heroInfos.Remove(key);
						bool flag2 = this._heroWaitCreateInfos.ContainsKey(key);
						if (flag2)
						{
							this._heroWaitCreateInfos.Remove(key);
						}
						bool flag3 = !this._heros.ContainsKey(key);
						if (!flag3)
						{
							LGAvatarHero lGAvatarHero = this._heros[key];
							this._heros.Remove(key);
							lGAvatarHero.dispose();
						}
					}
				}
			}
		}

		private void onRespawn(GameEvent e)
		{
			Variant data = e.data;
			bool flag = !data.ContainsKey("iid");
			if (!flag)
			{
				uint @uint = data["iid"]._uint;
				bool flag2 = !this._heros.ContainsKey(@uint);
				if (!flag2)
				{
					LGAvatarHero lGAvatarHero = this._heros[@uint];
					lGAvatarHero.Respawn(data);
				}
			}
		}

		public void onHeroEnterZone(Variant m)
		{
			bool playingPlot = GRMap.playingPlot;
			if (!playingPlot)
			{
				uint @uint = m["iid"]._uint;
				bool flag = this._heros.ContainsKey(@uint);
				if (!flag)
				{
					this._heroInfos[@uint] = m;
					bool initFlag = this._initFlag;
					if (initFlag)
					{
						this.createHero(m);
					}
				}
			}
		}

		private void createZoneSprites()
		{
			bool flag = !this._mapChageFlag;
			if (!flag)
			{
				this._initFlag = true;
				foreach (Variant current in this._heroInfos.Values)
				{
					this.createHero(current);
				}
				this._mapChageFlag = false;
			}
		}

		private void addCreateHero(Variant m)
		{
			uint @uint = m["iid"]._uint;
			this._heroWaitCreateInfos[@uint] = m;
		}

		public void createHero(Variant m)
		{
			int @int = m["mid"]._int;
			uint @uint = m["iid"]._uint;
			Variant conf = MonsterConfig.instance.conf;
			Variant variant = conf["monsters"][string.Concat(@int)];
			bool flag = variant == null;
			if (flag)
			{
				GameTools.PrintError(" hero[ " + @int + " ] no conf ERR!");
			}
			else
			{
				LGAvatarHero lGAvatarHero = new LGAvatarHero(this.g_mgr);
				this._heros[@uint] = lGAvatarHero;
				bool flag2 = m["owner_cid"] == ModelBase<PlayerModel>.getInstance().cid;
				if (flag2)
				{
					lGAvatarHero.addEffect("heroenterworld" + this.herologhtIdx, "hero_enterscence", true);
				}
				lGAvatarHero.initData(m);
				lGAvatarHero.init();
				this.g_mgr.g_processM.addRender(lGAvatarHero, false);
			}
		}

		public LGAvatarHero get_hero_by_iid(uint iid)
		{
			bool flag = !this._heros.ContainsKey(iid);
			LGAvatarHero result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._heros[iid];
			}
			return result;
		}

		public LGAvatarHero get_hero_by_mid(uint mid)
		{
			bool flag = this._heros == null || this._heros.Values == null;
			LGAvatarHero result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (LGAvatarHero current in this._heros.Values)
				{
					bool flag2 = current.getMid() != mid;
					if (!flag2)
					{
						result = current;
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public LGAvatarHero getNearHero()
		{
			bool flag = this._heros == null || this._heros.Values == null;
			LGAvatarHero result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LGAvatarHero lGAvatarHero = null;
				float num = 1000f;
				int num2 = this.lgMainPlayer.viewInfo["x"];
				int num3 = this.lgMainPlayer.viewInfo["y"];
				foreach (LGAvatarHero current in this._heros.Values)
				{
					bool flag2 = current.IsDie();
					if (!flag2)
					{
						bool flag3 = current.IsCollect();
						if (!flag3)
						{
							float num4 = Math.Abs(current.x - (float)num2) + Math.Abs(current.y - (float)num3);
							bool flag4 = num4 > 1000f;
							if (!flag4)
							{
								bool flag5 = lGAvatarHero == null;
								if (flag5)
								{
									lGAvatarHero = current;
									num = num4;
								}
								else
								{
									bool flag6 = num > num4;
									if (flag6)
									{
										lGAvatarHero = current;
										num = num4;
									}
								}
							}
						}
					}
				}
				result = lGAvatarHero;
			}
			return result;
		}
	}
}
