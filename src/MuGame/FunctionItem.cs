using System;
using System.Collections.Generic;

namespace MuGame
{
	public class FunctionItem
	{
		private static FunctionItem instance;

		public List<int> haveOpenId = new List<int>();

		public int id;

		public int type;

		public int main_task_id;

		public int zhuan;

		public int lv;

		public int legionlvl;

		public bool show;

		public float pos_x;

		public float pos_y;

		public int func_id;

		public int icon;

		public string name;

		public string des;

		public int grade;

		public int level;

		public bool opened = false;

		public static FunctionItem Instance
		{
			get
			{
				bool flag = FunctionItem.instance == null;
				if (flag)
				{
					FunctionItem.instance = new FunctionItem();
				}
				return FunctionItem.instance;
			}
			set
			{
				FunctionItem.instance = value;
			}
		}

		public bool Opened
		{
			get
			{
				return this.checkOpen((int)ModelBase<PlayerModel>.getInstance().up_lvl, (int)ModelBase<PlayerModel>.getInstance().lvl, ModelBase<A3_TaskModel>.getInstance().main_task_id, false, false);
			}
		}

		public bool legionopen
		{
			get
			{
				return this.checklegion(ModelBase<A3_LegionModel>.getInstance().myLegion.lvl);
			}
		}

		public void doOpen(bool showAni = false)
		{
			bool flag = this.opened;
			if (!flag)
			{
				bool flag2 = this.haveOpenId.Contains(this.id);
				if (!flag2)
				{
					bool flag3 = showAni && this.show;
					if (flag3)
					{
						bool flag4 = a3_funcopen.instance != null;
						if (flag4)
						{
							a3_funcopen.instance.refreshInfo(this.id, this.pos_x, this.pos_y);
						}
					}
					this.haveOpenId.Add(this.id);
					switch (this.id)
					{
					case 1:
						InterfaceMgr.doCommandByLua("herohead2.OpenPk", "ui/interfaces/floatui/herohead2", new object[0]);
						break;
					case 2:
					{
						bool flag5 = a3_task.instance;
						if (flag5)
						{
							a3_task.instance.OpenDailyTask();
						}
						break;
					}
					case 3:
					{
						bool flag6 = a3_expbar.instance;
						if (flag6)
						{
							a3_expbar.instance.OpenAuction();
						}
						break;
					}
					case 4:
					{
						bool flag7 = a3_expbar.instance;
						if (flag7)
						{
							a3_expbar.instance.OpenAchievement();
						}
						break;
					}
					case 5:
					{
						bool flag8 = a3_expbar.instance;
						if (flag8)
						{
							a3_expbar.instance.OpenSummon();
						}
						break;
					}
					case 6:
					{
						bool flag9 = a3_expbar.instance;
						if (flag9)
						{
							a3_expbar.instance.OpenSkill();
						}
						break;
					}
					case 7:
					{
						bool flag10 = a3_expbar.instance;
						if (flag10)
						{
							a3_expbar.instance.OpenSWING_PET();
						}
						break;
					}
					case 8:
					{
						bool flag11 = a3_expbar.instance;
						if (flag11)
						{
							a3_expbar.instance.OpenPET();
						}
						break;
					}
					case 9:
					{
						bool flag12 = a3_yiling.instance;
						if (flag12)
						{
							a3_yiling.instance.OpenWing();
						}
						break;
					}
					case 10:
						InterfaceMgr.doCommandByLua("a3_litemap.OpenActive", "ui/interfaces/floatui/a3_litemap", new object[0]);
						break;
					case 12:
					{
						bool flag13 = a3_active.instance;
						if (flag13)
						{
							a3_active.instance.OpenMWLR();
						}
						break;
					}
					case 15:
					{
						bool flag14 = a3_active.instance;
						if (flag14)
						{
							a3_active.instance.OpenSummon();
						}
						break;
					}
					case 16:
						InterfaceMgr.doCommandByLua("a3_litemap.openElite", "ui/interfaces/floatui/a3_litemap", new object[0]);
						break;
					case 17:
					{
						bool flag15 = a3_active.instance;
						if (flag15)
						{
							a3_active.instance.OpenMWLR();
						}
						break;
					}
					case 18:
					{
						bool flag16 = a3_expbar.instance;
						if (flag16)
						{
							a3_expbar.instance.OpenEQP();
						}
						break;
					}
					case 19:
					{
						bool flag17 = a3_equip.instance;
						if (flag17)
						{
							a3_equip.instance.OpenQH();
						}
						break;
					}
					case 20:
					{
						bool flag18 = a3_equip.instance;
						if (flag18)
						{
							a3_equip.instance.OpenCZ();
						}
						break;
					}
					case 21:
					{
						bool flag19 = a3_equip.instance;
						if (flag19)
						{
							a3_equip.instance.OpenCC();
						}
						break;
					}
					case 22:
					{
						bool flag20 = a3_equip.instance;
						if (flag20)
						{
							a3_equip.instance.OpenJJ();
						}
						break;
					}
					case 23:
					{
						bool flag21 = a3_equip.instance;
						if (flag21)
						{
							a3_equip.instance.OpenBS();
						}
						break;
					}
					case 24:
					{
						bool flag22 = a3_equip.instance;
						if (flag22)
						{
							a3_equip.instance.OpenZJ();
						}
						break;
					}
					case 25:
						InterfaceMgr.doCommandByLua("a3_litemap_btns.OpenMH", "ui/interfaces/floatui/a3_litemap_btns", new object[0]);
						break;
					case 30:
					{
						bool flag23 = a3_expbar.instance;
						if (flag23)
						{
							a3_expbar.instance.OpenHUDUN();
						}
						break;
					}
					case 33:
						InterfaceMgr.doCommandByLua("a3_litemap.OpenFB", "ui/interfaces/floatui/a3_litemap", new object[0]);
						break;
					case 35:
					{
						bool flag24 = a3_equip.instance;
						if (flag24)
						{
							a3_equip.instance.OpenBSHC();
						}
						break;
					}
					case 36:
					{
						bool flag25 = a3_equip.instance;
						if (flag25)
						{
							a3_equip.instance.OpenBSXQ();
						}
						break;
					}
					case 43:
					{
						bool flag26 = a3_equip.instance;
						if (flag26)
						{
							a3_equip.instance.OpenBSXQ();
						}
						break;
					}
					case 44:
					{
						bool flag27 = a3_equip.instance;
						if (flag27)
						{
							a3_equip.instance.OpenBSXQ();
						}
						break;
					}
					}
				}
			}
		}

		public bool checkOpen(int zhuanshen, int lvl, int maintaskid, bool finished = false, bool showAni = false)
		{
			bool flag = this.opened;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this.type == 2;
				if (flag2)
				{
					bool flag3 = zhuanshen > this.zhuan;
					if (flag3)
					{
						this.doOpen(showAni);
						this.opened = true;
						result = true;
						return result;
					}
					bool flag4 = zhuanshen == this.zhuan;
					if (flag4)
					{
						bool flag5 = lvl >= this.lv;
						if (flag5)
						{
							this.doOpen(showAni);
							this.opened = true;
							result = true;
							return result;
						}
					}
				}
				else
				{
					bool flag6 = this.type == 1;
					if (flag6)
					{
						bool flag7 = maintaskid > this.main_task_id || (maintaskid == this.main_task_id & finished);
						if (flag7)
						{
							this.doOpen(showAni);
							this.opened = true;
							result = true;
							return result;
						}
					}
				}
				result = false;
			}
			return result;
		}

		public bool checklegion(int legionlvls)
		{
			bool flag = this.type == 3;
			bool result;
			if (flag)
			{
				bool flag2 = legionlvls >= this.legionlvl;
				result = flag2;
			}
			else
			{
				result = false;
			}
			return result;
		}
	}
}
