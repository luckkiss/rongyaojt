using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class MapModel
	{
		public Dictionary<int, MapData> dFbDta = new Dictionary<int, MapData>();

		public Dictionary<int, Dictionary<int, List<MapData>>> lData = new Dictionary<int, Dictionary<int, List<MapData>>>();

		public Dictionary<string, MapFogXml> dFogXmls = new Dictionary<string, MapFogXml>();

		public int energy;

		public float beginTimer;

		public bool inited = false;

		public uint curLevelId;

		public uint curDiff;

		public Dictionary<string, int> lItemUsed;

		private SXML instancelvlxml;

		public int maxId0 = 0;

		public int maxId1 = 0;

		public int maxStageId0 = 1;

		public int maxStageId1 = 1;

		private static MapModel _instance;

		public MapModel()
		{
			this.instancelvlxml = XMLMgr.instance.GetSXML("instancelvl", "");
			this.show_instanceLvl(103u);
		}

		public MapData get3Starmap()
		{
			MapData result;
			foreach (MapData current in this.dFbDta.Values)
			{
				bool flag = current.starNum == 3;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant getCurLevelVar()
		{
			bool flag = this.curLevelId == 0u;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = SvrLevelConfig.instacne.get_level_data(this.curLevelId);
			}
			return result;
		}

		public bool CheckAutoPlay()
		{
			bool flag = this.curLevelId == 0u;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant expr_22 = SvrLevelConfig.instacne.get_level_data(this.curLevelId);
				result = (expr_22 != null && expr_22.ContainsKey("auto_play"));
			}
			return result;
		}

		public bool containerID(int mapid)
		{
			return this.dFbDta.ContainsKey(mapid);
		}

		public float getMaxCD()
		{
			int num = 480 - VipMgr.getValue(VipMgr.TILI_RESTORE_TIME);
			return (float)num;
		}

		public MapData getMapDta(int id)
		{
			bool flag = this.dFbDta.ContainsKey(id);
			MapData result;
			if (flag)
			{
				result = this.dFbDta[id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public void AddMapDta(int id, MapData data)
		{
			this.dFbDta[id] = data;
		}

		public void setLastMapId(int mapType, int mapid)
		{
			bool flag = mapType == 0;
			if (flag)
			{
				bool flag2 = mapid <= this.maxId0;
				if (flag2)
				{
					return;
				}
				this.maxId0 = mapid;
			}
			else
			{
				bool flag3 = mapType == 1;
				if (flag3)
				{
					bool flag4 = mapid <= this.maxId1;
					if (flag4)
					{
						return;
					}
					this.maxId1 = mapid;
				}
			}
			int num = this.dFbDta[mapid].stage_group;
			Dictionary<int, List<MapData>> dictionary = this.lData[mapType + 1];
			int count = dictionary.Count;
			foreach (List<MapData> current in dictionary.Values)
			{
				bool flag5 = current.Count > 0;
				if (flag5)
				{
					MapData mapData = current[current.Count - 1];
					bool flag6 = mapData.id == mapid && mapData.stage_group < count;
					if (flag6)
					{
						num = mapData.stage_group + 1;
					}
				}
			}
			bool flag7 = mapType == 0;
			if (flag7)
			{
				this.maxStageId0 = num;
			}
			else
			{
				bool flag8 = mapType == 1;
				if (flag8)
				{
					this.maxStageId1 = num;
				}
			}
		}

		public List<MapData> getFbListByGroup(int stagegroupId, int uiGroupId)
		{
			return this.lData[stagegroupId][uiGroupId];
		}

		public static MapModel getInstance()
		{
			bool flag = MapModel._instance == null;
			if (flag)
			{
				MapModel._instance = new MapModel();
			}
			return MapModel._instance;
		}

		public int show_instanceLvl(uint levelid)
		{
			int result = 0;
			Variant variant = SvrLevelConfig.instacne.get_level_data(levelid);
			bool flag = variant != null;
			if (flag)
			{
				int num = variant["lmtp"];
				SXML node = this.instancelvlxml.GetNode("zhuan", "zhuan==" + ModelBase<PlayerModel>.getInstance().up_lvl);
				List<SXML> nodeList = node.GetNodeList("att", "lv==" + ModelBase<PlayerModel>.getInstance().lvl);
				foreach (SXML current in nodeList)
				{
					bool flag2 = current.getInt("lmtp") == num;
					if (flag2)
					{
						result = current.getInt("show_lv");
						break;
					}
				}
			}
			return result;
		}
	}
}
