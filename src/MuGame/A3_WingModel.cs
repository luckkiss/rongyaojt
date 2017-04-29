using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class A3_WingModel : ModelBase<A3_WingModel>
	{
		private SXML wingXML;

		public int lastStage = 0;

		private int stage = 0;

		public bool stageUp = false;

		public int ShowStage = 0;

		public int LastShowState = 0;

		public int lastLevel = 0;

		private int level = 0;

		public int lastExp = 0;

		public int Exp = 0;

		public Dictionary<int, WingsData> dicWingsData;

		private Dictionary<string, uint> wingUpItem;

		public SXML WingXML
		{
			get
			{
				SXML arg_3C_0;
				if ((arg_3C_0 = this.wingXML) == null)
				{
					arg_3C_0 = (this.wingXML = XMLMgr.instance.GetSXML("wings.wing", "career==" + ModelBase<PlayerModel>.getInstance().profession));
				}
				return arg_3C_0;
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
				bool flag = value == this.stage;
				if (!flag)
				{
					this.stage = value;
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
				bool flag = value == this.level;
				if (!flag)
				{
					this.level = value;
				}
			}
		}

		public A3_WingModel()
		{
			this.GetAllWingsXMLData();
		}

		public void GetAllWingsXMLData()
		{
			this.dicWingsData = new Dictionary<int, WingsData>();
			bool flag = this.wingUpItem == null;
			if (flag)
			{
				Dictionary<string, uint> expr_1F = new Dictionary<string, uint>();
				expr_1F["levelItemId"] = XMLMgr.instance.GetSXML("wings.level_item", "").getUint("item_id");
				expr_1F["stageItemId"] = XMLMgr.instance.GetSXML("wings.stage_item", "").getUint("item_id");
				this.wingUpItem = expr_1F;
			}
			List<SXML> nodeList = this.WingXML.GetNodeList("wing_stage", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				WingsData wingsData = new WingsData();
				wingsData.stage = nodeList[i].getInt("stage_id");
				wingsData.spriteFile = "icon/wing/" + nodeList[i].getString("icon");
				wingsData.stageCostGold = nodeList[i].getUint("cost_gold");
				wingsData.stageCrystalMin = nodeList[i].getUint("crystal_min");
				wingsData.stageCrystalMax = nodeList[i].getUint("crystal_max");
				wingsData.stageCrystalStep = nodeList[i].getUint("crystal_step");
				wingsData.stageRateMin = nodeList[i].getUint("rate_min");
				wingsData.stageRateMax = nodeList[i].getUint("rate_max");
				wingsData.wingName = nodeList[i].getString("name");
				this.dicWingsData[wingsData.stage] = wingsData;
			}
		}

		private SXML GetLevelXML(int stage, int level)
		{
			SXML expr_20 = this.WingXML.GetNode("wing_stage", "stage_id==" + stage);
			return (expr_20 != null) ? expr_20.GetNode("wing_level", "level_id==" + level) : null;
		}

		public uint GetLevelUpMaxExp(int stage, int level)
		{
			SXML expr_08 = this.GetLevelXML(stage, level);
			return (expr_08 != null) ? expr_08.getUint("exp") : 0u;
		}

		public uint GetStageUpCostItemSum(int stage)
		{
			SXML expr_20 = this.WingXML.GetNode("wing_stage", "stage_id==" + stage);
			return (expr_20 != null) ? expr_20.getUint("crystal_min") : 0u;
		}

		public uint GetLevelUpCostItemSum(int stage, int level)
		{
			SXML expr_08 = this.GetLevelXML(stage, level);
			return (expr_08 != null) ? expr_08.getUint("cost_item_num") : 0u;
		}

		public int GetLevelUpCost(int stage, int level)
		{
			SXML expr_08 = this.GetLevelXML(stage, level);
			return (expr_08 != null) ? expr_08.getInt("cost_gold") : 0;
		}

		public int GetXmlMaxStage()
		{
			return this.dicWingsData.Count;
		}

		public int GetStageMaxLevel(int stage)
		{
			SXML expr_20 = this.WingXML.GetNode("wing_stage", "stage_id==" + stage);
			return ((expr_20 != null) ? expr_20.GetNodeList("wing_level", "").Count : 1) - 1;
		}

		public int GetStageUpCost(int stage)
		{
			SXML expr_20 = this.WingXML.GetNode("wing_stage", "stage_id==" + stage);
			return (expr_20 != null) ? expr_20.getInt("cost_gold") : -1;
		}

		public bool CheckLevelupAvailable()
		{
			return (this.stage < this.GetXmlMaxStage() && this.level == this.GetStageMaxLevel(this.stage)) ? ((ulong)ModelBase<PlayerModel>.getInstance().money >= (ulong)((long)this.GetStageUpCost(this.stage + 1)) && (long)ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.wingUpItem["stageItemId"]) >= (long)((ulong)this.GetStageUpCostItemSum(this.stage + 1))) : (this.level < this.GetStageMaxLevel(this.stage) && (ulong)ModelBase<PlayerModel>.getInstance().money >= (ulong)((long)this.GetLevelUpCost(this.stage, this.level + 1)) && (long)ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.wingUpItem["levelItemId"]) >= (long)((ulong)this.GetLevelUpCostItemSum(this.stage, this.level + 1)));
		}

		public void OnEquipModelChange()
		{
			int showStage = this.ShowStage;
			int showStage2 = this.ShowStage;
			SelfRole._inst.m_roleDta.m_WindID = showStage;
			SelfRole._inst.m_roleDta.m_WingFXID = showStage2;
			SelfRole._inst.set_wing(showStage, showStage2);
		}

		public void InitWingInfo(Variant data)
		{
			this.Stage = data["stage"];
			this.lastExp = (this.Exp = data["exp"]);
			this.lastLevel = (this.level = data["level"]);
		}

		public void SetLevelExp(Variant data)
		{
			this.lastExp = this.Exp;
			this.Exp = data["exp"];
			this.lastLevel = this.Level;
			this.Level = data["level"];
		}

		public void SetStageInfo(Variant data)
		{
			bool flag = data.ContainsKey("stage");
			if (flag)
			{
				this.lastLevel = this.Level;
				this.Level = 0;
				this.lastStage = this.Stage;
				this.Stage = data["stage"];
				this.ShowStage = data["show_stage"];
				this.stageUp = true;
			}
			else
			{
				this.stageUp = false;
			}
		}

		public void SetShowStage(Variant data)
		{
			this.ShowStage = data["show_stage"];
			this.OnEquipModelChange();
			bool flag = a3_wing_skin.instance != null;
			if (flag)
			{
				a3_wing_skin.instance.OnSetIconBGImage(this.ShowStage);
			}
		}
	}
}
