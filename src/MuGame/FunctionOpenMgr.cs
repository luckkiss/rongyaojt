using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class FunctionOpenMgr
	{
		public static int PK_MODEL = 1;

		public static int DAILY_TASK = 2;

		public static int AUCTION_GUILD = 3;

		public static int ACHIEVEMENT = 4;

		public static int SUMMON_MONSTER = 5;

		public static int SKILL = 6;

		public static int PET_SWING = 7;

		public static int PET = 8;

		public static int WING = 9;

		public static int ACTIVITES = 10;

		public static int WIND_THRONE = 11;

		public static int CHASTEN_JAIL = 12;

		public static int GOLD_DUNGEON = 13;

		public static int EXP_DUNGEON = 14;

		public static int SUMMON_PARK = 15;

		public static int GLOBA_BOSS = 16;

		public static int DEVIL_HUNTER = 17;

		public static int PVP_DUNGEON = 31;

		public static int FOR_CHEST = 32;

		public static int COUNTERPART = 33;

		public static int TEAMPART = 34;

		public static int LEGION = 39;

		public static int ENTRUST_TASK = 37;

		public static int AUTO_PLAY = 41;

		public static int EQP = 18;

		public static int EQP_ENHANCEMENT = 19;

		public static int EQP_REMOLD = 20;

		public static int EQP_INHERITANCE = 21;

		public static int EQP_LVUP = 22;

		public static int EQP_MOUNTING = 23;

		public static int EQP_ENCHANT = 24;

		public static int EQP_BSHC = 35;

		public static int EQP_BSXQ = 36;

		public static int SCREAMINGBOX = 25;

		public static int HUDUN = 30;

		public static int STAR_PIC = 42;

		public static int RUNE = 43;

		public static int JL_SL = 44;

		private static FunctionOpenMgr _instance;

		public Dictionary<int, FunctionItem> dItem = new Dictionary<int, FunctionItem>();

		public static FunctionOpenMgr instance
		{
			get
			{
				bool flag = FunctionOpenMgr._instance == null;
				if (flag)
				{
					FunctionOpenMgr.init();
				}
				return FunctionOpenMgr._instance;
			}
		}

		public static void init()
		{
			bool flag = FunctionOpenMgr._instance != null;
			if (!flag)
			{
				FunctionOpenMgr._instance = new FunctionOpenMgr();
				FunctionOpenMgr._instance.initMgr();
			}
		}

		public void initMgr()
		{
			int lvl = (int)ModelBase<PlayerModel>.getInstance().lvl;
			SXML sXML = XMLMgr.instance.GetSXML("func_open", "");
			List<SXML> nodeList = sXML.GetNodeList("func", "");
			foreach (SXML current in nodeList)
			{
				FunctionItem functionItem = new FunctionItem();
				functionItem.id = current.getInt("id");
				functionItem.type = current.getInt("type");
				functionItem.show = (current.getInt("show") == 1);
				functionItem.pos_x = current.getFloat("state_x");
				functionItem.pos_y = current.getFloat("state_y");
				bool flag = functionItem.type == 1;
				if (flag)
				{
					int @int = current.getInt("param1");
					functionItem.main_task_id = @int;
				}
				else
				{
					bool flag2 = functionItem.type == 2;
					if (flag2)
					{
						string[] array = current.getString("param1").Split(new char[]
						{
							','
						});
						functionItem.zhuan = int.Parse(array[0]);
						functionItem.lv = int.Parse(array[1]);
					}
					else
					{
						bool flag3 = functionItem.type == 3;
						if (flag3)
						{
							functionItem.legionlvl = current.getInt("param1");
						}
					}
				}
				this.dItem[functionItem.id] = functionItem;
			}
		}

		public void onLvUp(int up_lvl, int lvl, bool showAni = false)
		{
			foreach (FunctionItem current in this.dItem.Values)
			{
				current.checkOpen(up_lvl, lvl, 0, false, showAni);
			}
		}

		public void onFinshedMainTask(int maintaskid, bool finished = false, bool showAni = false)
		{
			foreach (FunctionItem current in this.dItem.Values)
			{
				current.checkOpen(0, 0, maintaskid, finished, showAni);
			}
		}

		public int getLvNeed(int id)
		{
			bool flag = !this.dItem.ContainsKey(id);
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				result = this.dItem[id].lv;
			}
			return result;
		}

		public bool Check(int id, bool notice = false)
		{
			bool flag = !this.dItem.ContainsKey(id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !this.dItem[id].Opened & notice;
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getCont("gameroom_unopen", null), 0, default(Color), null);
				}
				result = this.dItem[id].opened;
			}
			return result;
		}

		public bool checkLv(int id, bool notice = false)
		{
			bool flag = !this.dItem.ContainsKey(id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.dItem[id].Opened & notice;
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getCont("gameroom_unopen", null), 0, default(Color), null);
				}
				result = this.dItem[id].opened;
			}
			return result;
		}

		public bool checkLegion(int id, bool notice = false)
		{
			bool flag = !this.dItem.ContainsKey(id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.dItem[id].legionopen & notice;
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getCont("gameroom_unopen", null), 0, default(Color), null);
				}
				result = this.dItem[id].legionopen;
			}
			return result;
		}

		public void tryOpenFunction(int id)
		{
			bool flag = this.checkLv(id, false);
			if (flag)
			{
				this.dItem[id].doOpen(false);
			}
		}
	}
}
