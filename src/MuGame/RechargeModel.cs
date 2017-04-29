using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class RechargeModel : ModelBase<RechargeModel>
	{
		public Dictionary<int, rechargeData> rechargeMenu;

		public RechargeModel()
		{
			this.init();
		}

		public rechargeData getRechargeDataById(int id)
		{
			bool flag = this.rechargeMenu == null;
			if (flag)
			{
				this.init();
			}
			bool flag2 = this.rechargeMenu.ContainsKey(id);
			rechargeData result;
			if (flag2)
			{
				result = this.rechargeMenu[id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public void init()
		{
			this.rechargeMenu = new Dictionary<int, rechargeData>();
			SXML sXML = XMLMgr.instance.GetSXML("recharge", "");
			List<SXML> nodeList = sXML.GetNodeList("recharge", "");
			bool flag = nodeList == null;
			if (!flag)
			{
				foreach (SXML current in nodeList)
				{
					rechargeData rechargeData = new rechargeData();
					rechargeData.id = current.getInt("id");
					rechargeData.name = current.getString("name");
					rechargeData.golden = current.getString("golden");
					rechargeData.golden_value = current.getString("golden_value");
					rechargeData.days = current.getString("days");
					rechargeData.daynum = current.getString("daynum");
					rechargeData.first_double = current.getInt("first_double");
					rechargeData.desc = current.getString("desc");
					rechargeData.payid = current.getString("payid");
					this.rechargeMenu[rechargeData.id] = rechargeData;
				}
			}
		}
	}
}
