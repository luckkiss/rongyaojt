using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class GameRoomMgr
	{
		public Dictionary<uint, BaseRoomItem> dRooms;

		public BaseRoomItem curRoom;

		private static GameRoomMgr _instance;

		public GameRoomMgr()
		{
			this.dRooms = new Dictionary<uint, BaseRoomItem>();
			this.dRooms[0u] = new BaseRoomItem();
			this.dRooms[9999999u] = new PlotRoom();
			this.dRooms[3334u] = new ExpRoom();
			this.dRooms[3335u] = new MoneyRoom();
			this.dRooms[3339u] = new FSWZRoom();
			this.dRooms[3338u] = new MLZDRoom();
			this.dRooms[3340u] = new ZHSLYRoom();
			this.dRooms[3341u] = new PVPRoom();
			this.dRooms[3342u] = new WdsyRoom();
			this.dRooms[3344u] = new DragonRoom();
			this.dRooms[3345u] = new TlfbRoom109();
			this.dRooms[3346u] = new TlfbRoom110();
			this.dRooms[3347u] = new TlfbRoom111();
		}

		public bool checkCityRoom()
		{
			return this.curRoom == this.dRooms[0u];
		}

		public void onChangeLevel(Variant svrconf, Variant cursvrmsg)
		{
			bool flag = this.curRoom != null;
			if (flag)
			{
				this.curRoom.onEnd();
				this.curRoom = null;
			}
			uint key = svrconf["id"];
			bool flag2 = this.dRooms.ContainsKey(key);
			if (flag2)
			{
				this.curRoom = this.dRooms[key];
			}
			else
			{
				bool flag3 = MapModel.getInstance().curLevelId > 0u;
				if (flag3)
				{
					this.curRoom = this.dRooms[9999999u];
				}
				else
				{
					this.curRoom = this.dRooms[0u];
				}
			}
			this.curRoom.onStart(svrconf);
			bool flag4 = cursvrmsg != null && cursvrmsg.ContainsKey("dpitms");
			if (flag4)
			{
				List<Variant> arr = cursvrmsg["dpitms"]._arr;
				Dictionary<string, List<DropItemdta>> dictionary = new Dictionary<string, List<DropItemdta>>();
				long curServerTimeStampMS = NetClient.instance.CurServerTimeStampMS;
				foreach (Variant current in arr)
				{
					int num = current["x"];
					int num2 = current["y"];
					string key2 = num + "+" + num2;
					bool flag5 = !dictionary.ContainsKey(key2);
					if (flag5)
					{
						dictionary[key2] = new List<DropItemdta>();
					}
					DropItemdta dropItemdta = new DropItemdta();
					dropItemdta.init(current, curServerTimeStampMS);
					dropItemdta.x = num;
					dropItemdta.y = num2;
					dictionary[key2].Add(dropItemdta);
				}
				foreach (List<DropItemdta> current2 in dictionary.Values)
				{
					Vector3 pos = new Vector3((float)current2[0].x / 53.333f, 0f, (float)current2[0].y / 53.333f);
					BaseRoomItem.instance.showDropItem(pos, current2, false);
				}
			}
		}

		public bool onLevelFinish(Variant msgData)
		{
			bool flag = MapModel.getInstance().curLevelId > 0u;
			if (flag)
			{
				bool flag2 = MapModel.getInstance().getMapDta((int)MapModel.getInstance().curLevelId) != null;
				if (flag2)
				{
					MapData mapDta = MapModel.getInstance().getMapDta((int)MapModel.getInstance().curLevelId);
					bool flag3 = msgData.ContainsKey("kill_m");
					if (flag3)
					{
						Variant variant = msgData["kill_m"];
						int num = 0;
						foreach (Variant current in variant._arr)
						{
							bool flag4 = current.ContainsKey("cnt");
							if (flag4)
							{
							}
							num += current["cnt"];
						}
						mapDta.kmNum = num;
					}
				}
			}
			bool flag5 = this.curRoom != null;
			return flag5 && this.curRoom.onLevelFinish(msgData);
		}

		public bool onPrizeFinish(Variant msgData)
		{
			bool flag = this.curRoom != null;
			return flag && this.curRoom.onPrizeFinish(msgData);
		}

		public bool onLevelStatusChanges(Variant msgData)
		{
			bool flag = this.curRoom != null;
			return flag && this.curRoom.onLevel_Status_Changes(msgData);
		}

		public void onGetMapMoney(int money)
		{
			bool flag = this.curRoom != null;
			if (flag)
			{
				this.curRoom.onGetMapMoney(money);
			}
		}

		public static GameRoomMgr getInstance()
		{
			bool flag = GameRoomMgr._instance == null;
			if (flag)
			{
				GameRoomMgr._instance = new GameRoomMgr();
			}
			return GameRoomMgr._instance;
		}
	}
}
