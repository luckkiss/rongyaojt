using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class A3_PetModel : ModelBase<A3_PetModel>
	{
		private SXML petXML = null;

		public static bool showrenew = false;

		private uint tpid = 0u;

		public List<Variant> petId;

		public Action OnTpidChange = null;

		public static bool showbuy = false;

		public static uint curPetid;

		private int stage = -1;

		public Action OnStageChange = null;

		private int level = 0;

		public Action OnLevelChange = null;

		private int exp = 0;

		public Action OnExpChange = null;

		private int starvation = 0;

		public Action OnStarvationChange = null;

		private bool auto_buy = false;

		public Action Onauto_buyChange = null;

		private bool auto_feed = false;

		public Action OnAutoFeedChange = null;

		public bool first = false;

		public long getTime = 0L;

		private bool needHint = true;

		private uint feedid = 0u;

		public SXML PetXML
		{
			get
			{
				bool flag = this.petXML == null;
				if (flag)
				{
					this.petXML = XMLMgr.instance.GetSXML("pet", "");
				}
				return this.petXML;
			}
		}

		public uint Tpid
		{
			get
			{
				return this.tpid;
			}
			set
			{
				bool flag = this.tpid == value;
				if (!flag)
				{
					this.tpid = value;
					bool flag2 = this.OnTpidChange != null;
					if (flag2)
					{
						this.OnTpidChange();
					}
				}
			}
		}

		public int Stage
		{
			get
			{
				return this.stage;
			}
			set
			{
				bool flag = this.stage == value;
				if (!flag)
				{
					this.stage = value;
					bool flag2 = this.OnStageChange != null;
					if (flag2)
					{
						this.OnStageChange();
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
				}
			}
		}

		public int Exp
		{
			get
			{
				return this.exp;
			}
			set
			{
				bool flag = this.exp == value;
				if (!flag)
				{
					this.exp = value;
					bool flag2 = this.OnExpChange != null;
					if (flag2)
					{
						this.OnExpChange();
					}
				}
			}
		}

		public int Starvation
		{
			get
			{
				return this.starvation;
			}
			set
			{
				bool flag = value == 0;
				if (flag)
				{
					string cont = ContMgr.getCont("pet_invalid", null);
					bool flag2 = flytxt.instance != null;
					if (flag2)
					{
						flytxt.instance.fly(cont, 0, default(Color), null);
					}
				}
				bool flag3 = this.starvation == value;
				if (!flag3)
				{
					this.starvation = value;
					bool flag4 = this.OnStarvationChange != null;
					if (flag4)
					{
						this.OnStarvationChange();
					}
				}
			}
		}

		public bool Auto_buy
		{
			get
			{
				return this.auto_buy;
			}
			set
			{
				bool flag = this.auto_buy == value;
				if (!flag)
				{
					this.auto_buy = value;
					bool flag2 = this.Onauto_buyChange != null;
					if (flag2)
					{
						this.Onauto_buyChange();
					}
				}
			}
		}

		public bool Auto_feed
		{
			get
			{
				return this.auto_feed;
			}
			set
			{
				bool flag = this.auto_feed == value;
				if (!flag)
				{
					this.auto_feed = value;
					bool flag2 = this.OnAutoFeedChange != null;
					if (flag2)
					{
						this.OnAutoFeedChange();
					}
				}
			}
		}

		public int StageMaxLvl
		{
			get
			{
				return this.CurrentStageConf().getInt("to_next_stage_level");
			}
		}

		public A3_PetModel()
		{
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_USE_PETFEED, new Action<GameEvent>(this.OnMonitorFeed));
		}

		public bool hasPet()
		{
			return this.Tpid > 0u;
		}

		public int GetMaxExp()
		{
			return this.CurrentLevelConf().getInt("exp");
		}

		public int GetBlessCost()
		{
			return this.CurrentLevelConf().getInt("cost_item_num");
		}

		public uint GetFeedItemTpid()
		{
			SXML node = this.PetXML.GetNode("feed_item", "");
			return node.getUint("item_id");
		}

		public uint GetLevelItemTpid()
		{
			SXML node = this.PetXML.GetNode("level_item", "");
			return node.getUint("item_id");
		}

		public uint GetStageItemTpid()
		{
			SXML node = this.PetXML.GetNode("stage_item", "");
			return node.getUint("item_id");
		}

		public string GetPetAvatar(int id, int stageid)
		{
			string result;
			try
			{
				SXML node = this.PetXML.GetNode("pet", "id==" + id);
				SXML node2 = node.GetNode("stage", "stage==" + stageid);
				result = node2.getString("avatar");
			}
			catch (Exception var_3_4D)
			{
				result = "";
			}
			return result;
		}

		public string GetPetIcon()
		{
			return this.CurrentStageConf().getString("icon");
		}

		public SXML CurrentStageConf()
		{
			SXML node = this.PetXML.GetNode("pet", "id==" + this.Tpid);
			return (node != null) ? node.GetNode("stage", "stage==" + this.Stage) : null;
		}

		public SXML NextStageConf()
		{
			SXML node = this.PetXML.GetNode("pet", "id==" + this.Tpid);
			SXML node2 = node.GetNode("stage", "stage==" + (this.Stage + 1));
			bool flag = node2 == null;
			SXML result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = node2;
			}
			return result;
		}

		public SXML CurrentLevelConf()
		{
			SXML expr_07 = this.CurrentStageConf();
			return (expr_07 != null) ? expr_07.GetNode("lvl", "level==" + this.Level) : null;
		}

		public SXML NextLevelConf()
		{
			bool flag = this.Level == this.StageMaxLvl;
			SXML result;
			if (flag)
			{
				SXML sXML = this.NextStageConf();
				bool flag2 = sXML == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = sXML.GetNode("lvl", "level==1");
				}
			}
			else
			{
				result = this.CurrentStageConf().GetNode("lvl", "level==" + (this.Level + 1));
			}
			return result;
		}

		private void OnMonitorFeed(GameEvent e)
		{
			bool flag = this.feedid == 0u;
			if (flag)
			{
				this.feedid = this.GetFeedItemTpid();
			}
			int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.feedid);
			bool flag2 = itemNumByTpid > 0;
			if (flag2)
			{
				this.needHint = true;
			}
			else
			{
				bool flag3 = this.needHint && this.Auto_buy;
				if (flag3)
				{
					this.AutoBuy();
				}
				this.needHint = false;
			}
		}

		public void AutoBuy()
		{
			uint num = 99u;
			uint feedItemTpid = this.GetFeedItemTpid();
			a3_BagModel instance = ModelBase<a3_BagModel>.getInstance();
			a3_ItemData itemDataById = instance.getItemDataById(feedItemTpid);
			long num2 = (long)((ulong)num * (ulong)((long)itemDataById.on_sale));
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().money < (ulong)num2;
			if (flag)
			{
				int num3 = (int)(ModelBase<PlayerModel>.getInstance().money / (uint)itemDataById.on_sale);
				bool flag2 = num3 > 0;
				if (flag2)
				{
					BaseProxy<Shop_a3Proxy>.getInstance().BuyStoreItems(feedItemTpid, (uint)num3);
					flytxt.instance.fly(ContMgr.getCont("petmodel_addfood", new List<string>
					{
						num3.ToString(),
						itemDataById.item_name.ToString()
					}), 0, default(Color), null);
				}
				else
				{
					bool flag3 = num3 <= 0;
					if (flag3)
					{
						flytxt.instance.fly(ContMgr.getCont("petmodel_nofood", null), 0, default(Color), null);
					}
				}
			}
			else
			{
				BaseProxy<Shop_a3Proxy>.getInstance().BuyStoreItems(feedItemTpid, num);
				flytxt.instance.fly(ContMgr.getCont("petmodel_addfood", new List<string>
				{
					num.ToString(),
					itemDataById.item_name.ToString()
				}), 0, default(Color), null);
			}
		}

		public bool CheckLevelupAvaiable()
		{
			bool flag = !FunctionOpenMgr.instance.Check(FunctionOpenMgr.PET, false);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.CurrentLevelConf().getInt("level") >= this.StageMaxLvl;
				if (flag2)
				{
					result = (this.NextStageConf() != null && ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.GetStageItemTpid()) >= this.NextStageConf().getInt("crystal"));
				}
				else
				{
					result = (ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.GetLevelItemTpid()) >= this.NextLevelConf().getInt("cost_item_num"));
				}
			}
			return result;
		}
	}
}
