using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LotteryModel : ModelBase<LotteryModel>
	{
		public Dictionary<uint, lotterydata> dataybs = new Dictionary<uint, lotterydata>();

		public List<itemLotteryAwardData> lotteryAwardItems = new List<itemLotteryAwardData>();

		public static int iceOnceCost = 50;

		public bool isNewBie = false;

		public LotteryModel()
		{
			this.ReadLotteryXMLYB();
		}

		public void ReadLotteryXMLYB()
		{
			this.lotteryAwardItems.Clear();
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("lottery.item", null);
			for (int i = 0; i < sXMLList.Count; i++)
			{
				uint @uint = sXMLList[i].getUint("type");
				List<SXML> nodeList = sXMLList[i].GetNodeList("item", "");
				lotterydata lotterydata = new lotterydata();
				lotterydata.type = @uint;
				bool flag = this.dataybs.ContainsKey(@uint);
				if (flag)
				{
					this.dataybs[@uint] = lotterydata;
				}
				else
				{
					this.dataybs.Add(@uint, lotterydata);
				}
				lotterydata.lotteryAwardItems = new List<itemLotteryAwardData>();
				for (int j = 0; j < nodeList.Count; j++)
				{
					itemLotteryAwardData itemLotteryAwardData = new itemLotteryAwardData();
					itemLotteryAwardData.rootType = @uint;
					itemLotteryAwardData.id = nodeList[j].getUint("id");
					itemLotteryAwardData.itemType = nodeList[j].getUint("item_type");
					itemLotteryAwardData.itemId = nodeList[j].getUint("item_id");
					itemLotteryAwardData.num = nodeList[j].getUint("num");
					itemLotteryAwardData.itemName = nodeList[j].getString("item_name");
					itemLotteryAwardData.cost = nodeList[j].getUint("cost");
					itemLotteryAwardData.stage = nodeList[j].getUint("stage");
					itemLotteryAwardData.intensify = nodeList[j].getUint("intensify");
					this.lotteryAwardItems.Add(itemLotteryAwardData);
					lotterydata.lotteryAwardItems.Add(itemLotteryAwardData);
				}
			}
		}

		public string getAwardTypeId(uint id)
		{
			string result;
			for (int i = 0; i < this.lotteryAwardItems.Count; i++)
			{
				bool flag = id == this.lotteryAwardItems[i].id;
				if (flag)
				{
					bool flag2 = this.lotteryAwardItems[i].itemType == 2u;
					if (flag2)
					{
						result = "件";
					}
					else
					{
						bool flag3 = this.lotteryAwardItems[i].itemType == 1u;
						if (!flag3)
						{
							goto IL_64;
						}
						result = "个";
					}
					return result;
				}
				IL_64:;
			}
			result = string.Empty;
			return result;
		}

		public int getAwardNumById(uint id)
		{
			int result;
			for (int i = 0; i < this.lotteryAwardItems.Count; i++)
			{
				bool flag = id == this.lotteryAwardItems[i].id;
				if (flag)
				{
					result = (int)this.lotteryAwardItems[i].num;
					return result;
				}
			}
			result = -1;
			return result;
		}

		public uint getAwardItemIdById(uint id)
		{
			uint result;
			for (int i = 0; i < this.lotteryAwardItems.Count; i++)
			{
				bool flag = id == this.lotteryAwardItems[i].id;
				if (flag)
				{
					result = this.lotteryAwardItems[i].itemId;
					return result;
				}
			}
			result = 0u;
			return result;
		}

		public float getAwardItemAnimTimeSpan()
		{
			return XMLMgr.instance.GetSXML("lottery.distance", "").getFloat("val");
		}
	}
}
