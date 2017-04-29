using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class HudunModel : ModelBase<HudunModel>
	{
		private a3_hudun hudun = a3_hudun._instance;

		private a3_BagModel bagModel = ModelBase<a3_BagModel>.getInstance();

		public uint auto_time = 60u;

		public uint noAttackTime = 60u;

		public bool isNoAttack = true;

		public bool is_auto = true;

		public Dictionary<int, hudunData> hdData = new Dictionary<int, hudunData>();

		private int nowCount = 0;

		public Action OnnowCountChange = null;

		private int level = 0;

		public Action OnLevelChange = null;

		public int NowCount
		{
			get
			{
				return this.nowCount;
			}
			set
			{
				bool flag = this.nowCount == value;
				if (!flag)
				{
					this.nowCount = value;
					bool flag2 = this.OnnowCountChange != null;
					if (flag2)
					{
						this.OnnowCountChange();
					}
					bool flag3 = a3_herohead.instance;
					if (flag3)
					{
					}
				}
			}
		}

		public int Level
		{
			get
			{
				return this.level;
			}
			set
			{
				bool flag = this.level == value;
				if (!flag)
				{
					this.level = value;
					bool flag2 = this.OnLevelChange != null;
					if (flag2)
					{
						this.OnLevelChange();
					}
					bool flag3 = a3_herohead.instance;
					if (flag3)
					{
					}
				}
			}
		}

		public HudunModel()
		{
			this.Readxml();
		}

		public bool OnMjCountOk_auto(int needcount)
		{
			return needcount <= this.bagModel.getItemNumByTpid(1540u);
		}

		public bool CheckLevelupAvailable()
		{
			bool flag = this.Level < hudunData.max_level;
			bool result;
			if (flag)
			{
				bool flag2 = this.GetNeedMjMun(this.Level + 1) <= ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(1540u) && (long)this.GetNeedZhuan(this.Level + 1) <= (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) && (long)this.GetNeedLevel(this.Level + 1) <= (long)((ulong)ModelBase<PlayerModel>.getInstance().lvl);
				if (flag2)
				{
					result = true;
					return result;
				}
			}
			bool flag3 = this.Level > 0 && this.NowCount < this.GetMaxCount(this.Level);
			if (flag3)
			{
				bool flag4 = this.isNoAttack;
				if (flag4)
				{
					bool flag5 = this.GetNeedAddCount(this.Level) <= ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(1540u);
					if (flag5)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		private void Readxml()
		{
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("pvpsheild.sheild", "");
			hudunData.max_level = sXMLList.Count;
			foreach (SXML current in sXMLList)
			{
				hudunData hudunData = new hudunData();
				hudunData.hdLvl = current.getInt("lv");
				hudunData.raise_zhuan = current.getInt("raise_zhuan");
				hudunData.raise_lv = current.getInt("raise_lv");
				hudunData.needcount = current.getInt("item_cost");
				hudunData.addcount = current.getInt("item_refill");
				hudunData.energy = current.getInt("energy");
				this.hdData[hudunData.hdLvl] = hudunData;
			}
		}

		public int GetMaxCount(int hdlvl)
		{
			return this.hdData[hdlvl].energy;
		}

		public int GetNeedMjMun(int hdlvl)
		{
			return this.hdData[hdlvl].needcount;
		}

		public int GetNeedAddCount(int hdlvl)
		{
			return this.hdData[hdlvl].addcount;
		}

		public int GetNeedLevel(int hdlvl)
		{
			return this.hdData[hdlvl].raise_lv;
		}

		public int GetNeedZhuan(int hdlvl)
		{
			return this.hdData[hdlvl].raise_zhuan;
		}
	}
}
