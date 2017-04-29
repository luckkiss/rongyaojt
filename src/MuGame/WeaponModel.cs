using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class WeaponModel : ModelBase<WeaponModel>
	{
		public static uint EVENT_INIT_INFO = 0u;

		public Dictionary<int, WeaponData> weaponFace;

		public Dictionary<int, cl_aryData> cl_arydata;

		public Dictionary<int, WeaponUpgradeData> upgradeData;

		public Dictionary<int, WeaponCastData> dicwcsdata;

		public int num = 0;

		public WeaponModel()
		{
			this.weaponFace = new Dictionary<int, WeaponData>();
			this.cl_arydata = new Dictionary<int, cl_aryData>();
			this.upgradeData = new Dictionary<int, WeaponUpgradeData>();
			this.dicwcsdata = new Dictionary<int, WeaponCastData>();
		}

		public void addData(WeaponData one)
		{
			SXML sXML = XMLMgr.instance.GetSXML("shadow_flare.shadow_flare", "id==" + one.id);
			bool flag = sXML != null;
			if (flag)
			{
				one.name = sXML.getString("name");
				one.attrId = sXML.getInt("attrId");
				one.levelList = new List<WeaponLevelData>();
				SXML node = sXML.GetNode("level", null);
				bool flag2 = node != null;
				if (flag2)
				{
					do
					{
						WeaponLevelData item = default(WeaponLevelData);
						item.id = node.getString("id");
						item.que = node.getString("que");
						one.levelList.Add(item);
					}
					while (node.nextOne());
				}
			}
			this.weaponFace.Add(one.id, one);
		}

		public void getWeaponUpgradeDataById(int id)
		{
			SXML sXML = XMLMgr.instance.GetSXML("shadow_flare.upgrade_exp", "id==" + id);
			WeaponUpgradeData weaponUpgradeData = default(WeaponUpgradeData);
			weaponUpgradeData.id = id;
			bool flag = sXML != null;
			if (flag)
			{
				weaponUpgradeData.expList = new List<UpgradeLevelData>();
				SXML node = sXML.GetNode("level", null);
				bool flag2 = node != null;
				if (flag2)
				{
					do
					{
						UpgradeLevelData item = default(UpgradeLevelData);
						item.id = node.getInt("id");
						item.exp = node.getInt("exp");
						weaponUpgradeData.expList.Add(item);
					}
					while (node.nextOne());
				}
			}
			this.upgradeData[weaponUpgradeData.id] = weaponUpgradeData;
		}

		public WeaponUnlockData getWeaponUnlockDataById(string id)
		{
			WeaponUnlockData result = default(WeaponUnlockData);
			result.id = id;
			SXML sXML = XMLMgr.instance.GetSXML("shadow_flare.unlock", "id==" + id);
			bool flag = sXML != null;
			if (flag)
			{
				result.unlock_lv = sXML.getString("unlock_lv");
			}
			return result;
		}

		public WeaponCastData getWeaponCastDataById(int id)
		{
			WeaponCastData weaponCastData = default(WeaponCastData);
			SXML sXML = XMLMgr.instance.GetSXML("cast_exp.", "shadow_flare==" + id);
			bool flag = sXML != null;
			if (flag)
			{
				do
				{
					weaponCastData.material_id = sXML.getInt("material_id");
					weaponCastData.cast_exp = sXML.getInt("cast_exp");
				}
				while (sXML.nextOne());
			}
			this.dicwcsdata[weaponCastData.material_id] = weaponCastData;
			return weaponCastData;
		}

		public void tianjiaData(cl_aryData one)
		{
			this.cl_arydata.Add(one.id, one);
		}
	}
}
