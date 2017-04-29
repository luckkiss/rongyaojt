using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LGMap : lgGDBase, IObjectPlugin
	{
		private uint _tile_size = 0u;

		private uint _globaMapW = 0u;

		private uint _globaMapH = 0u;

		private uint _currMapid = 0u;

		private mapCalc _mapCalc;

		protected Variant _tmpLinks = new Variant();

		public uint m_unMapWidth = 0u;

		public uint m_unMapHeight = 0u;

		public Variant _viewInfo = new Variant();

		public uint curMapId
		{
			get
			{
				return this._currMapid;
			}
		}

		public uint curMapWidth
		{
			get
			{
				Variant variant = this.currMapSvrConf();
				bool flag = variant != null;
				uint result;
				if (flag)
				{
					uint @uint = variant["tile_size"]._uint;
					uint num = variant["width"]._uint * this._tile_size;
					result = num;
				}
				else
				{
					result = 0u;
				}
				return result;
			}
		}

		public uint curMapHeight
		{
			get
			{
				Variant variant = this.currMapSvrConf();
				bool flag = variant != null;
				uint result;
				if (flag)
				{
					uint @uint = variant["tile_size"]._uint;
					uint num = variant["height"]._uint * this._tile_size;
					result = num;
				}
				else
				{
					result = 0u;
				}
				return result;
			}
		}

		public Variant tmpLinks
		{
			get
			{
				return this._tmpLinks;
			}
		}

		private SvrMapConfig svrMapConfig
		{
			get
			{
				return this.g_mgr.g_gameConfM.getObject("SvrMap") as SvrMapConfig;
			}
		}

		private InGameMapMsgs msgMap
		{
			get
			{
				return this.g_mgr.g_netM.getObject("MSG_MAP") as InGameMapMsgs;
			}
		}

		private lgSelfPlayer selfPlayer
		{
			get
			{
				return this.g_mgr.getObject("LG_MAIN_PLAY") as lgSelfPlayer;
			}
		}

		private LGOthers otherPlayers
		{
			get
			{
				return this.g_mgr.getObject("LG_OTHER_PLAYERS") as LGOthers;
			}
		}

		private LGMonsters monsters
		{
			get
			{
				return this.g_mgr.getObject("LG_MONSTERS") as LGMonsters;
			}
		}

		private LGNpcs lgNpcs
		{
			get
			{
				return this.g_mgr.getObject("LG_NPCS") as LGNpcs;
			}
		}

		private MediaClient m_media
		{
			get
			{
				return MediaClient.getInstance();
			}
		}

		public LGMap(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGMap(m as gameManager);
		}

		public override void init()
		{
			this._mapCalc = new mapCalc(this);
			this.g_mgr.g_gameM.addEventListenerCL("LG_JOIN_WORLD", 3034u, new Action<GameEvent>(this.onJoinWorld));
			this.g_mgr.g_gameM.addEventListenerCL("LG_JOIN_WORLD", 3035u, new Action<GameEvent>(this.onChangeMap));
			this.g_mgr.addEventListener(2166u, new Action<GameEvent>(this.onAddFlyEff));
		}

		public Variant getViewInfo()
		{
			return this._viewInfo;
		}

		public bool showFlag()
		{
			return this._currMapid > 0u;
		}

		private void onChangeMap(GameEvent e)
		{
			Variant data = this.refreshMapInfo();
			base.dispatchEvent(GameEvent.Create(2160u, this, data, false));
		}

		private void onJoinWorld(GameEvent e)
		{
			this.trySetDrawBase();
		}

		private void onAddFlyEff(GameEvent e)
		{
			base.dispatchEvent(GameEvent.Createimmedi(2166u, this, e.data, false));
		}

		public void EnterStandalone()
		{
			this.g_mgr.g_sceneM.dispatchEvent(GameEvent.Createimmedi(2181u, this, null, false));
			SvrMapConfig svrMapConfig = GRClient.instance.g_gameConfM.getObject("SvrMap") as SvrMapConfig;
			uint num = 3333u;
			Variant singleMapConf = svrMapConfig.getSingleMapConf(num);
			Variant variant = new Variant();
			variant["conf"] = singleMapConf;
			variant["mapid"] = num;
			variant["tmpLinks"] = new Variant();
			base.dispatchEvent(GameEvent.Create(2161u, this, variant, false));
		}

		public LGAvatar get_player_by_cid(uint cid)
		{
			bool flag = cid == this.selfPlayer.getCid();
			LGAvatar result;
			if (flag)
			{
				result = this.selfPlayer;
			}
			else
			{
				result = this.otherPlayers.get_player_by_cid(cid);
			}
			return result;
		}

		public LGAvatar get_Character_by_iid(uint iid)
		{
			return this.selfPlayer.get_Character_by_iid(iid);
		}

		public LGAvatar get_mon_by_mid(uint mid)
		{
			return this.monsters.get_mon_by_mid(mid);
		}

		public LGAvatar get_NPC(uint nid)
		{
			return this.lgNpcs.getNpc((int)nid);
		}

		private void trySetDrawBase()
		{
			bool flag = GRMap.instance != null;
			if (flag)
			{
				base.dispatchEvent(GameEvent.Create(2181u, this, null, false));
			}
			else
			{
				this.g_mgr.g_sceneM.dispatchEvent(GameEvent.Createimmedi(2181u, this, null, false));
			}
			Variant data = this.refreshMapInfo();
			base.dispatchEvent(GameEvent.Create(2161u, this, data, false));
		}

		private void refreshCameraParma()
		{
		}

		private Variant currMapLocalConf()
		{
			bool flag = this._currMapid <= 0u;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.g_mgr.g_sceneM.getMapConf(this._currMapid.ToString());
			}
			return result;
		}

		private Variant currMapSvrConf()
		{
			bool flag = this._currMapid <= 0u;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant singleMapConf = this.svrMapConfig.getSingleMapConf(this._currMapid);
				result = singleMapConf;
			}
			return result;
		}

		private Variant refreshMapInfo()
		{
			joinWorldInfo joinWorldInfo = this.g_mgr.g_netM.getObject("DATA_JOIN_WORLD") as joinWorldInfo;
			Variant mainPlayerInfo = joinWorldInfo.mainPlayerInfo;
			this._currMapid = joinWorldInfo.mapid;
			Variant value = this.currMapLocalConf();
			Variant variant = this.currMapSvrConf();
			bool flag = variant != null;
			if (flag)
			{
				this._tile_size = variant["tile_size"];
				this.m_unMapWidth = variant["width"];
				this.m_unMapHeight = variant["height"];
				this._globaMapW = variant["width"] * this._tile_size;
				this._globaMapH = variant["height"] * this._tile_size;
			}
			Variant variant2 = new Variant();
			Variant variant3 = new Variant();
			variant2["param"] = variant3;
			variant3["width"] = this._globaMapW;
			variant3["height"] = this._globaMapH;
			Variant variant4 = new Variant();
			variant4["localConf"] = value;
			variant4["conf"] = variant;
			variant4["mapid"] = this._currMapid;
			variant4["tmpLinks"] = this._tmpLinks;
			return variant4;
		}

		private void formatXY(int x, int y)
		{
		}

		public bool IsWalkAble(int gx, int gy)
		{
			return this._mapCalc.IsWalkAble((float)gx, (float)gy);
		}

		public List<GridST> findPath(Vec2 start, Vec2 end)
		{
			return this._mapCalc.findPath(start, end);
		}

		public List<uint> getMapPath(uint curMapId, uint dest_map_id)
		{
			return this._mapCalc.get_map_path(curMapId, dest_map_id);
		}

		public Vec2 getPPosByGPos(float gridX, float gridY)
		{
			return new Vec2((float)((int)(gridX * 32f + 16f)), (float)((int)(gridY * 32f + 16f)));
		}

		public Vec2 getGPosByPPos(float pixelX, float pixelY)
		{
			return new Vec2((float)((int)(pixelX / 32f)), (float)((int)(pixelY / 32f)));
		}

		public float getFarthestDistance(float stPixelX, float stPixelY, float ori)
		{
			Vec2 farthestGPosByOri = this.getFarthestGPosByOri(stPixelX, stPixelY, ori, 707f);
			int num = (int)(farthestGPosByOri.x * 32f - stPixelX);
			int num2 = (int)(farthestGPosByOri.y * 32f - stPixelY);
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		public Vec2 getFarthestGPosByOri(float stPixelX, float stPixelY, float radian, float gezi_distance = 707f)
		{
			Vec2 gPosByPPos = this.getGPosByPPos(stPixelX, stPixelY);
			float num = gPosByPPos.x;
			float num2 = gPosByPPos.y;
			float num3 = (float)Math.Cos((double)radian);
			float num4 = (float)Math.Sin((double)radian);
			float num5 = num4 / num3;
			float num6 = (float)((num3 >= 0f) ? 1 : -1);
			float num7 = (float)((num4 >= 0f) ? 1 : -1);
			float num8 = Math.Abs(num5);
			bool flag = (double)Math.Abs(num8 - 1f) >= 0.01;
			bool flag2 = num8 > 1f;
			float num9 = num;
			float num10 = num2;
			float num11 = num6;
			float num12 = num7;
			float num13 = num5;
			bool flag3 = flag2;
			if (flag3)
			{
				num9 = num2;
				num10 = num;
				num11 = num7;
				num12 = num6;
				num13 = 1f / num5;
			}
			float num14 = 0f;
			float num15 = 0f;
			while (true)
			{
				bool flag4 = gezi_distance < 707f && gezi_distance <= Math.Abs(num14);
				if (flag4)
				{
					break;
				}
				num14 += num11;
				num8 = (float)((int)(num14 * num13));
				bool flag5 = flag;
				if (flag5)
				{
					bool flag6 = num8 != num15;
					if (flag6)
					{
						bool flag7 = num8 - num15 != num12;
						if (flag7)
						{
						}
						num15 = num8;
						num14 -= num11;
					}
				}
				else
				{
					num15 += num12;
				}
				bool flag8 = flag2;
				if (flag8)
				{
					bool flag9 = !this.IsWalkAble((int)(num10 + num15), (int)(num9 + num14));
					if (flag9)
					{
						break;
					}
					num = num10 + num15;
					num2 = num9 + num14;
				}
				else
				{
					bool flag10 = !this.IsWalkAble((int)(num9 + num14), (int)(num10 + num15));
					if (flag10)
					{
						break;
					}
					num2 = num10 + num15;
					num = num9 + num14;
				}
			}
			return new Vec2(num, num2);
		}

		public void AddTempLinks(List<Variant> linkDatas)
		{
			long curServerTimeStampMS = this.g_mgr.g_netM.CurServerTimeStampMS;
			Variant variant = new Variant();
			foreach (Variant current in linkDatas)
			{
				current["goto"] = current["gto"];
				current.RemoveKey("gto");
				Variant variant2 = new Variant();
				variant2["data"] = current;
				variant2["ctm"] = curServerTimeStampMS;
				this._tmpLinks.pushBack(variant2);
				variant.pushBack(variant2);
			}
			base.dispatchEvent(GameEvent.Create(2163u, this, variant, false));
		}

		public Variant get_map_link(uint mapid)
		{
			Variant variant = this.currMapSvrConf();
			bool flag = !variant.ContainsKey("l");
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<Variant> arr = variant["l"]._arr;
				for (int i = 0; i < arr.Count; i++)
				{
					Variant variant2 = arr[i];
					bool flag2 = variant2["gto"]._uint == mapid;
					if (flag2)
					{
						result = variant2;
						return result;
					}
				}
				bool flag3 = this._tmpLinks.Values != null;
				if (flag3)
				{
					foreach (Variant current in this._tmpLinks.Values)
					{
						bool flag4 = current["data"]["goto"]._uint == mapid;
						if (flag4)
						{
							result = current["data"];
							return result;
						}
					}
				}
				result = null;
			}
			return result;
		}

		public Variant get_map_link(int grid_x, int grid_y)
		{
			Variant variant = this.currMapSvrConf();
			bool flag = !variant.ContainsKey("l");
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<Variant> arr = variant["l"]._arr;
				for (int i = 0; i < arr.Count; i++)
				{
					int num = arr[i]["x"];
					int num2 = arr[i]["y"];
					bool flag2 = (long)Math.Abs(num - grid_x) <= 5L && (long)Math.Abs(num2 - grid_y) <= 5L;
					if (flag2)
					{
						result = arr[i];
						return result;
					}
				}
				bool flag3 = this._tmpLinks.Values != null;
				if (flag3)
				{
					foreach (Variant current in this._tmpLinks.Values)
					{
						int @int = current["data"]["x"]._int;
						int int2 = current["data"]["y"]._int;
						bool flag4 = (long)Math.Abs(@int - grid_x) < 5L && (long)Math.Abs(int2 - grid_y) < 5L;
						if (flag4)
						{
							result = current["data"];
							return result;
						}
					}
				}
				result = null;
			}
			return result;
		}

		public int pixelToGridSize(float psize)
		{
			return (int)(psize / 32f);
		}

		public float gridSizeToPixel(int gridSize)
		{
			return (float)((long)gridSize * 32L);
		}

		public void beginChangeMap(uint mapid)
		{
			this.msgMap.change_map(mapid);
		}

		public void playMapMusic(bool force = false)
		{
			Variant gMapInfo = GRClient.instance.g_sceneM.getGMapInfo(this.curMapId.ToString());
			bool flag = gMapInfo != null;
			if (flag)
			{
			}
		}

		public int GetPKState()
		{
			Variant variant = this.currMapSvrConf();
			return (variant != null && variant.ContainsKey("pk")) ? variant["pk"]._int : 0;
		}
	}
}
