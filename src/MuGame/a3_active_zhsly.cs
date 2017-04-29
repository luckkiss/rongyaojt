using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_active_zhsly : a3BaseActive
	{
		private BaseButton enterbtn;

		private Dictionary<uint, uint> diff_lvl_zhaun = new Dictionary<uint, uint>();

		private new Variant data = SvrLevelConfig.instacne.get_level_data(105u);

		private uint diff_lvl;

		public a3_active_zhsly(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			for (int i = 1; i < 5; i++)
			{
				bool flag = !this.diff_lvl_zhaun.ContainsKey((uint)i);
				if (flag)
				{
					this.diff_lvl_zhaun.Add((uint)i, this.data["diff_lvl"][i]["zhuan"]);
				}
			}
			this.enterbtn = new BaseButton(base.getTransformByPath("enter"), 1, 1);
			this.enterbtn.onClick = delegate(GameObject go)
			{
				Variant variant = new Variant();
				variant["mapid"] = 3340;
				variant["npcid"] = 0;
				variant["ltpid"] = 105;
				variant["diff_lvl"] = this.diff_lvl;
				BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
			};
		}

		public override void onShowed()
		{
			uint up_lvl = ModelBase<PlayerModel>.getInstance().up_lvl;
			foreach (KeyValuePair<uint, uint> current in this.diff_lvl_zhaun)
			{
				bool flag = current.Value == up_lvl;
				if (flag)
				{
					this.diff_lvl = current.Key;
				}
			}
			bool flag2 = up_lvl > this.diff_lvl_zhaun[4u];
			if (flag2)
			{
				this.diff_lvl = 4u;
			}
			int minutes = this.data["tm"];
			TimeSpan timeSpan = new TimeSpan(0, minutes, 0);
			base.getTransformByPath("cue/time").gameObject.SetActive(false);
			bool flag3 = MapModel.getInstance().dFbDta.ContainsKey(105);
			if (flag3)
			{
				MapData mapData = MapModel.getInstance().dFbDta[105];
				TimeSpan timeSpan2 = new TimeSpan(0, 0, mapData.limit_tm);
				base.getTransformByPath("cue/limit").GetComponent<Text>().text = string.Concat(new object[]
				{
					"剩余时间： ",
					timeSpan2.Hours,
					"时",
					timeSpan2.Minutes,
					"分",
					timeSpan2.Seconds,
					"秒"
				});
				bool flag4 = mapData.limit_tm <= 0;
				if (flag4)
				{
					this.enterbtn.interactable = false;
				}
				else
				{
					this.enterbtn.interactable = true;
				}
			}
			else
			{
				base.getTransformByPath("cue/limit").GetComponent<Text>().text = string.Concat(new object[]
				{
					"剩余时间： ",
					1,
					"时",
					0,
					"分",
					0,
					"秒"
				});
				this.enterbtn.interactable = true;
			}
			base.getTransformByPath("cue/reword").GetComponent<Text>().text = "副本奖励： <color=#ff9900>召唤兽宝宝</color>";
			this.RefreshLeftTimes();
		}

		private void RefreshLeftTimes()
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(105u);
			int b = variant["daily_cnt"];
			bool flag = MapModel.getInstance().dFbDta.ContainsKey(105);
			if (flag)
			{
				int num = Mathf.Min(MapModel.getInstance().dFbDta[105].cycleCount, b);
			}
			bool flag2 = MapModel.getInstance().dFbDta.ContainsKey(105);
			if (flag2)
			{
				MapData mapData = MapModel.getInstance().dFbDta[105];
				TimeSpan timeSpan = new TimeSpan(0, 0, mapData.limit_tm);
				base.getTransformByPath("cue/limit").GetComponent<Text>().text = string.Concat(new object[]
				{
					"剩余时间： ",
					timeSpan.Hours,
					"时",
					timeSpan.Minutes,
					"分",
					timeSpan.Seconds,
					"秒"
				});
				bool flag3 = mapData.limit_tm <= 0;
				if (flag3)
				{
					this.enterbtn.interactable = false;
				}
				else
				{
					this.enterbtn.interactable = true;
				}
			}
			else
			{
				base.getTransformByPath("cue/limit").GetComponent<Text>().text = string.Concat(new object[]
				{
					"剩余时间： ",
					1,
					"时",
					0,
					"分",
					0,
					"秒"
				});
				this.enterbtn.interactable = true;
			}
		}

		public override void onClose()
		{
		}
	}
}
