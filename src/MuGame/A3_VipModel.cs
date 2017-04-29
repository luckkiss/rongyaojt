using System;
using System.Collections.Generic;

namespace MuGame
{
	public class A3_VipModel : ModelBase<A3_VipModel>
	{
		public List<uint> isGetVipGift = new List<uint>();

		public Dictionary<int, Dictionary<int, int>> giftdata = new Dictionary<int, Dictionary<int, int>>();

		private List<SXML> vipLevelXML;

		private List<SXML> vipGiftXML;

		private int level = 0;

		public Action OnLevelChange = null;

		private int exp = 0;

		public Action OnExpChange = null;

		private List<List<int>> allVipData = new List<List<int>>();

		private List<int> allVipGiftData = new List<int>();

		private Dictionary<int, List<int>> allVipPriData = new Dictionary<int, List<int>>();

		public List<SXML> VipLevelXML
		{
			get
			{
				bool flag = this.vipLevelXML == null;
				if (flag)
				{
					this.vipLevelXML = XMLMgr.instance.GetSXMLList("vip.viplevel", "");
				}
				return this.vipLevelXML;
			}
		}

		public List<SXML> VipGiftXML
		{
			get
			{
				bool flag = this.vipGiftXML == null;
				if (flag)
				{
					this.vipGiftXML = XMLMgr.instance.GetSXMLList("vip.gift", "");
				}
				return this.vipGiftXML;
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
					ProfessionRole expr_1C = SelfRole._inst;
					if (expr_1C != null)
					{
						expr_1C.refreshVipLvl((uint)this.level);
					}
					Action expr_34 = this.OnLevelChange;
					if (expr_34 != null)
					{
						expr_34();
					}
					InterfaceMgr.doCommandByLua("VipModel:getInstance().modLevel", "model/VipModel", new object[]
					{
						this.level
					});
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

		public A3_VipModel()
		{
			this.InitVipAttData();
			this.InitVipGiftData();
			this.ReadXml_gidtData();
		}

		public void viplvl_refresh()
		{
			ModelBase<A3_ActiveModel>.getInstance().buy_cnt = ModelBase<A3_VipModel>.getInstance().vip_exchange_num(7);
		}

		public int vip_exchange_num(int i)
		{
			int result = 0;
			SXML sXML = XMLMgr.instance.GetSXML("vip.viplevel", "vip_level==" + ModelBase<A3_VipModel>.getInstance().Level);
			bool flag = sXML != null;
			if (flag)
			{
				SXML node = sXML.GetNode("vt", "type==" + i);
				bool flag2 = node != null;
				if (flag2)
				{
					result = node.getInt("value");
				}
			}
			return result;
		}

		public int GetCurLevelMaxExp()
		{
			int result = 0;
			SXML sXML = this.VipLevelXML[this.Level];
			bool flag = sXML != null;
			if (flag)
			{
				result = sXML.getInt("vip_point");
			}
			return result;
		}

		public int GetNextLvlMaxExp()
		{
			bool flag = this.Level >= this.GetMaxVipLevel();
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				SXML sXML = this.VipLevelXML[this.Level + 1];
				result = sXML.getInt("vip_point");
			}
			return result;
		}

		public int GetMaxVipLevel()
		{
			return this.allVipData.Count - 1;
		}

		public int GetPriMum()
		{
			List<SXML> nodeList = this.VipLevelXML[0].GetNodeList("vt", "");
			return nodeList.Count;
		}

		public string Gettype_Name(int lvl, int type)
		{
			List<SXML> nodeList = this.VipLevelXML[lvl].GetNodeList("vt", "");
			string result = "";
			bool flag = nodeList != null;
			if (flag)
			{
				result = nodeList[type].getString("show_name");
			}
			return result;
		}

		public string GetValue(int lvl, int type)
		{
			List<SXML> nodeList = this.VipLevelXML[lvl].GetNodeList("vt", "");
			string result = "";
			bool flag = nodeList != null;
			if (flag)
			{
				result = nodeList[type].getString("value");
			}
			return result;
		}

		public int GetShowType(int type)
		{
			List<SXML> nodeList = this.VipLevelXML[0].GetNodeList("vt", "");
			int result = 0;
			bool flag = nodeList != null;
			if (flag)
			{
				result = nodeList[type].getInt("show_type");
			}
			return result;
		}

		public int GetType(int lvl, int type)
		{
			List<SXML> nodeList = this.VipLevelXML[lvl].GetNodeList("vt", "");
			int result = 0;
			bool flag = nodeList != null;
			if (flag)
			{
				result = nodeList[type].getInt("type");
			}
			return result;
		}

		private void InitVipAttData()
		{
			int count = this.VipLevelXML.Count;
			for (int i = 0; i < count; i++)
			{
				List<int> list = new List<int>();
				List<SXML> nodeList = this.VipLevelXML[i].GetNodeList("vt", "");
				bool flag = nodeList != null;
				if (flag)
				{
					for (int j = 0; j < nodeList.Count; j++)
					{
						list.Add(nodeList[j].getInt("value"));
					}
				}
				else
				{
					list.Add(-1);
				}
				this.allVipData.Add(list);
			}
		}

		private void InitVipGiftData()
		{
			int count = this.VipGiftXML.Count;
			for (int i = 0; i < count; i++)
			{
				List<SXML> nodeList = this.VipGiftXML[i].GetNodeList("item", "");
				bool flag = nodeList != null;
				if (flag)
				{
					this.allVipGiftData.Add(nodeList[0].getInt("item_id"));
				}
				else
				{
					this.allVipGiftData.Add(-1);
				}
			}
		}

		private void ReadXml_gidtData()
		{
			foreach (int current in this.allVipGiftData)
			{
				bool flag = current <= 0;
				if (!flag)
				{
					SXML sXML = XMLMgr.instance.GetSXML("itemdrop.itemdrop", "id==" + current);
					SXML node = sXML.GetNode("itempkg", "");
					List<SXML> nodeList = node.GetNodeList("group", "");
					Dictionary<int, int> dictionary = new Dictionary<int, int>();
					foreach (SXML current2 in nodeList)
					{
						int key = 0;
						int value = 0;
						SXML node2 = current2.GetNode("itm", "");
						SXML node3 = current2.GetNode("eqp", "");
						bool flag2 = node2 != null;
						if (flag2)
						{
							key = node2.getInt("itemid");
							value = node2.getInt("max");
						}
						bool flag3 = node3 != null;
						if (flag3)
						{
							key = node3.getInt("itemid");
							value = node3.getInt("max");
						}
						dictionary[key] = value;
					}
					this.giftdata[current] = dictionary;
				}
			}
		}

		private void InitVipPriData()
		{
		}

		public List<int> GetVipAttByLevel(int level)
		{
			List<int> list = new List<int>();
			return this.allVipData[level];
		}

		public int GetVipPoint(int level)
		{
			return 1;
		}

		public int GetVipGiftListByLevel(int level)
		{
			return this.allVipGiftData[level];
		}

		public string GetVipState()
		{
			string result = null;
			SXML sXML = XMLMgr.instance.GetSXML("vip.vip_state", "");
			bool flag = sXML != null;
			if (flag)
			{
				result = sXML.getString("state_str");
			}
			return result;
		}
	}
}
