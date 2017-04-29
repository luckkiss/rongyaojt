using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class ResetLvLAwardModel : ModelBase<ResetLvLAwardModel>
	{
		private List<ResetLvLAwardData> resetLvLAwardList;

		public List<ResetLvLAwardData> getAwardListById(uint carr)
		{
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl_item.item", "id==" + carr.ToString());
			bool flag = sXML == null;
			List<ResetLvLAwardData> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				this.resetLvLAwardList = new List<ResetLvLAwardData>();
				this.resetLvLAwardList.Clear();
				List<SXML> nodeList = sXML.GetNodeList("award", null);
				for (int i = 0; i < nodeList.Count; i++)
				{
					ResetLvLAwardData resetLvLAwardData = new ResetLvLAwardData();
					resetLvLAwardData.name = nodeList[i].getString("item_name");
					resetLvLAwardData.icon = nodeList[i].getString("icon_file");
					this.resetLvLAwardList.Add(resetLvLAwardData);
				}
				result = this.resetLvLAwardList;
			}
			return result;
		}
	}
}
