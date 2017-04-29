using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class a3_achievement : Window
	{
		private AchiveSkin honur;

		private AchiveSkin rank;

		private AchiveSkin currentAchieve = null;

		private TabControl tc;

		public static a3_achievement instance;

		public override void init()
		{
			this.honur = new a3_honor(this, base.transform.FindChild("contents/a3_honor"));
			this.rank = new a3_rank(this, base.transform.FindChild("contents/a3_rank"));
			this.tc = new TabControl();
			a3_achievement.instance = this;
			this.tc.onClickHanle = delegate(TabControl t)
			{
				int seletedIndex = t.getSeletedIndex();
				int num = 0;
				Transform transform = base.transform.FindChild("contents");
				Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform2 = componentsInChildren[i];
					bool flag = transform2.parent == transform;
					if (flag)
					{
						bool flag2 = num == seletedIndex;
						if (flag2)
						{
							transform2.gameObject.SetActive(true);
						}
						else
						{
							transform2.gameObject.SetActive(false);
						}
						num++;
					}
				}
				bool flag3 = this.currentAchieve != null;
				if (flag3)
				{
					this.currentAchieve.onClosed();
				}
				int num2 = seletedIndex;
				if (num2 != 0)
				{
					if (num2 == 1)
					{
						this.currentAchieve = this.rank;
					}
				}
				else
				{
					this.currentAchieve = this.honur;
				}
				bool flag4 = this.currentAchieve != null;
				if (flag4)
				{
					this.currentAchieve.onShowed();
				}
			};
			this.tc.create(base.transform.FindChild("tabs").gameObject, base.gameObject, 0, 0, false);
		}

		public override void onShowed()
		{
			BaseProxy<A3_RankProxy>.getInstance().sendProxy(1u, -1, false);
			bool flag = this.uiData == null;
			if (flag)
			{
				this.tc.setSelectedIndex(0, true);
			}
			else
			{
				int index = (int)this.uiData[0];
				this.tc.setSelectedIndex(index, true);
			}
			GRMap.GAME_CAMERA.SetActive(false);
		}

		public override void onClosed()
		{
			bool flag = this.currentAchieve != null;
			if (flag)
			{
				this.currentAchieve.onClosed();
			}
			GRMap.GAME_CAMERA.SetActive(true);
		}
	}
}
