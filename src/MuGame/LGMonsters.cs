using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class LGMonsters : lgGDBase, IObjectPlugin
	{
		public static LGMonsters instacne;

		private Dictionary<uint, LGAvatarMonster> _mons;

		private Dictionary<uint, Variant> _monInfos;

		private Dictionary<uint, Variant> _monWaitCreateInfos = new Dictionary<uint, Variant>();

		private bool _initFlag = false;

		private bool _mapChageFlag = false;

		private lgSelfPlayer lgMainPlayer
		{
			get
			{
				return this.g_mgr.getObject("LG_MAIN_PLAY") as lgSelfPlayer;
			}
		}

		public LGMonsters(gameManager m) : base(m)
		{
			LGMonsters.instacne = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGMonsters(m as gameManager);
		}

		public Dictionary<uint, LGAvatarMonster> getMons()
		{
			return this._mons;
		}

		public LGAvatarMonster getMonsterById(uint id)
		{
			bool flag = !this._mons.ContainsKey(id);
			LGAvatarMonster result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._mons[id];
			}
			return result;
		}

		public override void init()
		{
			this._mons = new Dictionary<uint, LGAvatarMonster>();
			this._monInfos = new Dictionary<uint, Variant>();
			this.g_mgr.g_netM.addEventListener(55u, new Action<GameEvent>(this.onMonsterEnterZone));
			this.g_mgr.g_netM.addEventListener(56u, new Action<GameEvent>(this.onSpriteLeaveZone));
			this.g_mgr.g_gameM.addEventListenerCL("LG_JOIN_WORLD", 3035u, new Action<GameEvent>(this.onMapchg));
			this.g_mgr.g_gameM.addEventListenerCL("LG_JOIN_WORLD", 3034u, new Action<GameEvent>(this.onMapchg));
			this.g_mgr.g_netM.addEventListener(20u, new Action<GameEvent>(this.onRespawn));
			this.g_mgr.g_processM.addProcess(new processStruct(new Action<float>(this.update), "LGMonsters", false, false), false);
		}

		private void update(float tmSlice)
		{
			bool flag = this._monWaitCreateInfos.Count <= 0;
			if (!flag)
			{
				using (Dictionary<uint, Variant>.ValueCollection.Enumerator enumerator = this._monWaitCreateInfos.Values.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						Variant current = enumerator.Current;
						this.createMon(current);
						uint @uint = current["iid"]._uint;
						this._monWaitCreateInfos.Remove(@uint);
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
			foreach (LGAvatarMonster current in this._mons.Values)
			{
				this.g_mgr.g_processM.removeRender(current, false);
				current.dispose();
			}
			this._mons.Clear();
			this._monInfos.Clear();
			LGHeros.instacne.clear();
		}

		public void onMapchg(GameEvent e)
		{
			this.clear();
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
					bool flag = !this._monInfos.ContainsKey(key);
					if (!flag)
					{
						this._monInfos.Remove(key);
						bool flag2 = this._monWaitCreateInfos.ContainsKey(key);
						if (flag2)
						{
							this._monWaitCreateInfos.Remove(key);
						}
						bool flag3 = !this._mons.ContainsKey(key);
						if (!flag3)
						{
							LGAvatarMonster lGAvatarMonster = this._mons[key];
							this._mons.Remove(key);
							lGAvatarMonster.dispose();
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
				bool flag2 = !this._mons.ContainsKey(@uint);
				if (!flag2)
				{
					LGAvatarMonster lGAvatarMonster = this._mons[@uint];
					lGAvatarMonster.Respawn(data);
				}
			}
		}

		private void onMonsterEnterZone(GameEvent e)
		{
			Variant data = e.data;
			foreach (Variant current in data["monsters"]._arr)
			{
				uint @uint = current["iid"]._uint;
				bool flag = this._mons.ContainsKey(@uint);
				if (!flag)
				{
					bool flag2 = current.ContainsKey("owner_cid");
					if (flag2)
					{
						LGHeros.instacne.onHeroEnterZone(current);
					}
					else
					{
						this._monInfos[@uint] = current;
						bool initFlag = this._initFlag;
						if (initFlag)
						{
							this.createMon(current);
						}
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
				foreach (Variant current in this._monInfos.Values)
				{
					this.createMon(current);
				}
				this._mapChageFlag = false;
			}
		}

		private void addCreateMon(Variant m)
		{
			uint @uint = m["iid"]._uint;
			this._monWaitCreateInfos[@uint] = m;
		}

		public void createMon(Variant m)
		{
			int @int = m["mid"]._int;
			uint @uint = m["iid"]._uint;
			Variant conf = MonsterConfig.instance.conf;
			Variant variant = conf["monsters"][string.Concat(@int)];
			bool flag = variant == null;
			if (flag)
			{
				GameTools.PrintError(" mon[ " + @int + " ] no conf ERR!");
			}
			else
			{
				m["x"] = m["x"] / 53.333f;
				m["y"] = m["y"] / 53.333f;
				LGAvatarMonster lGAvatarMonster = new LGAvatarMonster(this.g_mgr);
				this._mons[@uint] = lGAvatarMonster;
				lGAvatarMonster.initData(m);
				lGAvatarMonster.init();
				this.g_mgr.g_processM.addRender(lGAvatarMonster, false);
			}
		}

		public LGAvatarMonster get_mon_by_iid(uint iid)
		{
			bool flag = !this._mons.ContainsKey(iid);
			LGAvatarMonster result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._mons[iid];
			}
			return result;
		}

		public LGAvatarMonster get_mon_by_mid(uint mid)
		{
			bool flag = this._mons == null || this._mons.Values == null;
			LGAvatarMonster result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (LGAvatarMonster current in this._mons.Values)
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

		public LGAvatarMonster getNearMon(int range = 1000)
		{
			bool flag = this._mons == null || this._mons.Values == null;
			LGAvatarMonster result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LGAvatarMonster lGAvatarMonster = null;
				float num = 1000f;
				Vector3 unityPos = this.lgMainPlayer.unityPos;
				float x = unityPos.x;
				float z = unityPos.z;
				foreach (LGAvatarMonster current in this._mons.Values)
				{
					bool flag2 = current.IsDie();
					if (!flag2)
					{
						bool flag3 = current.IsCollect();
						if (!flag3)
						{
							Vector3 unityPos2 = current.unityPos;
							float num2 = Math.Abs(unityPos2.x - x) + Math.Abs(unityPos2.y - z);
							bool flag4 = num2 > (float)range;
							if (!flag4)
							{
								bool flag5 = lGAvatarMonster == null && !current.IsDie();
								if (flag5)
								{
									lGAvatarMonster = current;
									num = num2;
								}
								else
								{
									bool flag6 = num > num2 && !current.IsDie();
									if (flag6)
									{
										lGAvatarMonster = current;
										num = num2;
									}
								}
							}
						}
					}
				}
				result = lGAvatarMonster;
			}
			return result;
		}
	}
}
