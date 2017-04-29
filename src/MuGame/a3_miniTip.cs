using GameFramework;
using System;

namespace MuGame
{
	internal class a3_miniTip : Window
	{
		public const int ITEM_ID = 0;

		public const int SHOW_TYPE = 1;

		public const int REWARD_DESC_TEXT = 2;

		private static a3_miniTip instance;

		public RewardItemTip rewardItemTip;

		public RewardEquipTip rewardEquipTip;

		public static a3_miniTip Instance
		{
			get
			{
				return a3_miniTip.instance;
			}
			set
			{
				a3_miniTip.instance = value;
			}
		}

		public override void init()
		{
			a3_miniTip.Instance = this;
			this.rewardItemTip = new RewardItemTip(base.transform.FindChild("rewardItemTip").gameObject);
			this.rewardEquipTip = new RewardEquipTip(base.transform.FindChild("rewardEqpTip").gameObject);
		}

		public override void onShowed()
		{
			switch ((int)this.uiData[1])
			{
			case -1:
			case 0:
			case 1:
			{
				IL_31:
				bool flag = this.uiData.Count >= 1;
				if (flag)
				{
					this.rewardItemTip.owner.SetActive(true);
					this.rewardItemTip.ShowItemTip((uint)this.uiData[0]);
				}
				return;
			}
			case 2:
			{
				bool flag2 = this.uiData.Count >= 1;
				if (flag2)
				{
					this.rewardEquipTip.owner.SetActive(true);
					this.rewardEquipTip.ShowEquipTip((uint)this.uiData[0]);
				}
				return;
			}
			case 3:
			{
				bool flag3 = this.uiData.Count >= 3;
				if (flag3)
				{
					this.rewardEquipTip.owner.SetActive(true);
					this.rewardEquipTip.ShowXMLCustomizedEquipTip((uint)this.uiData[0], new RewardDescText?((RewardDescText)this.uiData[2]));
				}
				return;
			}
			}
			goto IL_31;
		}

		public override void onClosed()
		{
			for (int i = 1; i < base.transform.childCount; i++)
			{
				base.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
}
