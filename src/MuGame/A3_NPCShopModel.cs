using System;
using System.Collections.Generic;
using System.Linq;

namespace MuGame
{
	internal class A3_NPCShopModel : ModelBase<A3_NPCShopModel>
	{
		public int alltimes;

		public Dictionary<uint, uint> float_list = new Dictionary<uint, uint>();

		public Dictionary<uint, uint> float_list_last = new Dictionary<uint, uint>();

		public Dictionary<uint, uint> float_list_num = new Dictionary<uint, uint>();

		public Dictionary<uint, uint> all_float = new Dictionary<uint, uint>();

		public Dictionary<uint, uint> limit_num = new Dictionary<uint, uint>();

		public List<SXML> listNPCShop = new List<SXML>();

		public Dictionary<int, NpcShopData> local_dicNpcShop;

		public Dictionary<int, NpcShopData> price = new Dictionary<int, NpcShopData>();

		public A3_NPCShopModel()
		{
			this.local_dicNpcShop = new Dictionary<int, NpcShopData>();
			this.ReadXML();
		}

		private void ReadXML()
		{
			SXML sXML = XMLMgr.instance.GetSXML("npc_shop", "");
			List<SXML> nodeList = XMLMgr.instance.GetSXML("npc_shop", "").GetNodeList("npc_shop", "");
			bool flag = nodeList != null;
			if (flag)
			{
				for (int i = 0; i < nodeList.Count; i++)
				{
					NpcShopData npcShopData = new NpcShopData();
					npcShopData.shop_id = nodeList[i].getUint("shop_id");
					npcShopData.npc_id = nodeList[i].getInt("npc_id");
					npcShopData.shop_name = nodeList[i].getString("shop_name");
					string[] array = nodeList[i].getString("float_list").Split(new char[]
					{
						','
					});
					string[] array2 = nodeList[i].getString("goods_list").Split(new char[]
					{
						','
					});
					npcShopData.dicFloatList = new Dictionary<uint, uint>();
					npcShopData.dicGoodsList = new Dictionary<uint, uint>();
					for (int j = 0; j < array.Length; j++)
					{
						uint num;
						bool flag2 = uint.TryParse(array[j], out num);
						if (flag2)
						{
							npcShopData.dicFloatList.Add(num, sXML.GetNode("float_list", "id==" + num).getUint("item_id"));
						}
					}
					for (int k = 0; k < array2.Length; k++)
					{
						uint num;
						bool flag3 = uint.TryParse(array2[k], out num);
						if (flag3)
						{
							npcShopData.dicGoodsList.Add(num, sXML.GetNode("goods_list", "id==" + num).getUint("item_id"));
						}
					}
					npcShopData.mapId = XMLMgr.instance.GetSXML("npcs", "").GetNode("npc", "id==" + npcShopData.npc_id).getInt("map_id");
					this.local_dicNpcShop.Add(npcShopData.npc_id, npcShopData);
				}
			}
		}

		public NpcShopData GetDataByItemId(uint itemId)
		{
			List<int> list = this.local_dicNpcShop.Keys.ToList<int>();
			NpcShopData result;
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < this.local_dicNpcShop[list[i]].dicFloatList.Count; j++)
				{
					bool flag = this.local_dicNpcShop[list[i]].dicFloatList.ContainsValue(itemId);
					if (flag)
					{
						result = this.local_dicNpcShop[list[i]];
						return result;
					}
				}
			}
			result = null;
			return result;
		}
	}
}
