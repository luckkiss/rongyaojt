using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LGOthers : lgGDBase, IObjectPlugin
	{
		public static LGOthers instance;

		private Dictionary<uint, LGAvatarOther> _playersByIid;

		private Dictionary<uint, LGAvatarOther> _playersByCid;

		private Dictionary<uint, Variant> _playerShowInfosByIid;

		private bool _initFlag = false;

		private bool _mapChageFlag = false;

		public Dictionary<uint, LGAvatarOther> getPlayers
		{
			get
			{
				return this._playersByIid;
			}
		}

		private MgrPlayerInfo plyInfos
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).getObject("MgrPlayerInfo") as MgrPlayerInfo;
			}
		}

		public LGOthers(gameManager m) : base(m)
		{
			LGOthers.instance = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGOthers(m as gameManager);
		}

		public override void init()
		{
			this._playersByIid = new Dictionary<uint, LGAvatarOther>();
			this._playersByCid = new Dictionary<uint, LGAvatarOther>();
			this._playerShowInfosByIid = new Dictionary<uint, Variant>();
			this.g_mgr.g_netM.addEventListener(54u, new Action<GameEvent>(this.onPlayerEnterZone));
			this.g_mgr.g_netM.addEventListener(56u, new Action<GameEvent>(this.onSpriteLeaveZone));
			this.g_mgr.g_gameM.addEventListenerCL("LG_JOIN_WORLD", 3035u, new Action<GameEvent>(this.onMapchg));
			this.g_mgr.g_gameM.addEventListenerCL("LG_JOIN_WORLD", 3034u, new Action<GameEvent>(this.onMapchg));
		}

		private void onSpriteLeaveZone(GameEvent e)
		{
			Variant data = e.data;
			using (List<Variant>.Enumerator enumerator = data["iidary"]._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					uint key = enumerator.Current;
					bool flag = !this._playerShowInfosByIid.ContainsKey(key);
					if (!flag)
					{
						Variant variant = this._playerShowInfosByIid[key];
						uint @uint = variant["cid"]._uint;
						this._playerShowInfosByIid.Remove(key);
						bool flag2 = this._playersByIid.ContainsKey(key);
						if (flag2)
						{
							LGAvatarOther lGAvatarOther = this._playersByIid[key];
							this._playersByIid.Remove(key);
							this._playersByCid.Remove(@uint);
							lGAvatarOther.dispose();
						}
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
			foreach (LGAvatarOther current in this._playersByIid.Values)
			{
				this.g_mgr.g_processM.removeRender(current, false);
				current.dispose();
			}
			this._playersByIid.Clear();
			this._playersByCid.Clear();
			this._playerShowInfosByIid.Clear();
		}

		public void onMapchg(GameEvent e)
		{
			this.clear();
			this._mapChageFlag = true;
			this.createZoneSprites();
		}

		private void onPlayerEnterZone(GameEvent e)
		{
			bool playingPlot = GRMap.playingPlot;
			if (!playingPlot)
			{
				debug.Log("!!onPlayerEnterZone!! " + debug.count);
				Variant data = e.data;
				foreach (Variant current in data["pary"]._arr)
				{
					uint @uint = current["iid"]._uint;
					uint uint2 = current["cid"]._uint;
					bool flag = this._playerShowInfosByIid.ContainsKey(@uint);
					if (!flag)
					{
						this._playerShowInfosByIid[@uint] = current;
						bool initFlag = this._initFlag;
						if (initFlag)
						{
							this.createPlayer(current);
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
				foreach (Variant current in this._playerShowInfosByIid.Values)
				{
					this.createPlayer(current);
				}
			}
		}

		public LGAvatarGameInst get_player_by_cid(uint cid)
		{
			bool flag = !this._playersByCid.ContainsKey(cid);
			LGAvatarGameInst result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._playersByCid[cid];
			}
			return result;
		}

		public LGAvatarGameInst get_player_by_iid(uint iid)
		{
			bool flag = !this._playersByIid.ContainsKey(iid);
			LGAvatarGameInst result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._playersByIid[iid];
			}
			return result;
		}

		public LGAvatarOther getNearPlayer(int range = 1000)
		{
			bool flag = this._playersByIid == null || this._playersByIid.Values == null;
			LGAvatarOther result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LGAvatarOther lGAvatarOther = null;
				float num = 1000f;
				int num2 = lgSelfPlayer.instance.viewInfo["x"];
				int num3 = lgSelfPlayer.instance.viewInfo["y"];
				foreach (LGAvatarOther current in this._playersByIid.Values)
				{
					bool flag2 = current.IsDie();
					if (!flag2)
					{
						bool flag3 = current.IsCollect();
						if (!flag3)
						{
							float num4 = Math.Abs(current.x - (float)num2) + Math.Abs(current.y - (float)num3);
							bool flag4 = num4 > (float)range;
							if (!flag4)
							{
								bool flag5 = lGAvatarOther == null && !current.IsDie();
								if (flag5)
								{
									lGAvatarOther = current;
									num = num4;
								}
								else
								{
									bool flag6 = num > num4 && !current.IsDie();
									if (flag6)
									{
										lGAvatarOther = current;
										num = num4;
									}
								}
							}
						}
					}
				}
				result = lGAvatarOther;
			}
			return result;
		}

		private void createPlayer(Variant m)
		{
			uint @uint = m["iid"]._uint;
			uint uint2 = m["cid"]._uint;
			LGAvatarOther lGAvatarOther = new LGAvatarOther(this.g_mgr);
			this._playersByIid[@uint] = lGAvatarOther;
			this._playersByCid[uint2] = lGAvatarOther;
			lGAvatarOther.initData(m);
			lGAvatarOther.init();
			this.g_mgr.g_processM.addRender(lGAvatarOther, false);
		}

		public void disposeChar(uint iid)
		{
			LGAvatarOther lGAvatarOther = this._playersByIid[iid];
			lGAvatarOther.dispose();
		}
	}
}
