using Cross;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_counterpart_multi_mate : a3BaseActive
	{
		public static bool open = false;

		private int zhaun = 0;

		private int ji = 0;

		private int pzhuan = 0;

		private int pji = 0;

		private BaseButton enterbtn;

		private BaseButton enterbtn1;

		private BaseButton enterbtn2;

		private BaseButton enterbtn3;

		public a3_counterpart_multi_mate(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			this.refreshExp();
			this.enterbtn = new BaseButton(base.getTransformByPath("choiceDef/easy"), 1, 1);
			this.enterbtn1 = new BaseButton(base.getTransformByPath("choiceDef/normal"), 1, 1);
			this.enterbtn2 = new BaseButton(base.getTransformByPath("choiceDef/deffi"), 1, 1);
			this.enterbtn3 = new BaseButton(base.getTransformByPath("choiceDef/god"), 1, 1);
			this.pzhuan = (int)ModelBase<PlayerModel>.getInstance().up_lvl;
			this.pji = (int)ModelBase<PlayerModel>.getInstance().lvl;
			this.changeSth();
			this.enterbtn.onClick = delegate(GameObject go)
			{
				base.gameObject.SetActive(false);
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count < 2;
				if (flag)
				{
					Variant variant = new Variant();
					variant["npcid"] = 0;
					variant["ltpid"] = 110;
					variant["diff_lvl"] = 1;
					a3_counterpart.lvl = variant["diff_lvl"];
					BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
				}
				else
				{
					this.zhaunJi(1);
					a3_counterpart_multi_mate.open = true;
					bool flag2 = this.canINfb();
					if (flag2)
					{
						BaseProxy<TeamProxy>.getInstance().SendReady(true, 110u, 1u);
						a3_counterpart.instance.getTransformByPath("ready/yesorno/Text/name").GetComponent<Text>().text = "血之盛宴--简单";
						a3_counterpart.instance.tenSen();
						bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
						if (meIsCaptain)
						{
							a3_counterpart.instance.getTransformByPath("ready/yesorno/show/0/name").GetComponent<Text>().text = "自己";
						}
						a3_counterpart.instance.getGameObjectByPath("currentTeam").SetActive(true);
						a3_counterpart.instance.getGameObjectByPath("ready").SetActive(true);
						a3_counterpart.instance.getButtonByPath("ready/yesorno/yes").interactable = false;
					}
				}
			};
			this.enterbtn1.onClick = delegate(GameObject go)
			{
				base.gameObject.SetActive(false);
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count < 2;
				if (flag)
				{
					Variant variant = new Variant();
					variant["npcid"] = 0;
					variant["ltpid"] = 110;
					variant["diff_lvl"] = 2;
					a3_counterpart.lvl = variant["diff_lvl"];
					BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
				}
				else
				{
					this.zhaunJi(2);
					a3_counterpart_multi_mate.open = true;
					bool flag2 = this.canINfb();
					if (flag2)
					{
						BaseProxy<TeamProxy>.getInstance().SendReady(true, 110u, 2u);
						a3_counterpart.instance.getTransformByPath("ready/yesorno/Text/name").GetComponent<Text>().text = "血之盛宴--普通";
						a3_counterpart.instance.tenSen();
						bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
						if (meIsCaptain)
						{
							a3_counterpart.instance.getTransformByPath("ready/yesorno/show/0/name").GetComponent<Text>().text = "自己";
						}
						a3_counterpart.instance.getGameObjectByPath("currentTeam").SetActive(true);
						a3_counterpart.instance.getGameObjectByPath("ready").SetActive(true);
						a3_counterpart.instance.getButtonByPath("ready/yesorno/yes").interactable = false;
					}
				}
			};
			this.enterbtn2.onClick = delegate(GameObject go)
			{
				base.gameObject.SetActive(false);
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count < 2;
				if (flag)
				{
					Variant variant = new Variant();
					variant["npcid"] = 0;
					variant["ltpid"] = 110;
					variant["diff_lvl"] = 3;
					a3_counterpart.lvl = variant["diff_lvl"];
					BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
				}
				else
				{
					this.zhaunJi(3);
					a3_counterpart_multi_mate.open = true;
					bool flag2 = this.canINfb();
					if (flag2)
					{
						BaseProxy<TeamProxy>.getInstance().SendReady(true, 110u, 3u);
						a3_counterpart.instance.getTransformByPath("ready/yesorno/Text/name").GetComponent<Text>().text = "血之盛宴--困难";
						a3_counterpart.instance.tenSen();
						bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
						if (meIsCaptain)
						{
							a3_counterpart.instance.getTransformByPath("ready/yesorno/show/0/name").GetComponent<Text>().text = "自己";
						}
						a3_counterpart.instance.getGameObjectByPath("currentTeam").SetActive(true);
						a3_counterpart.instance.getGameObjectByPath("ready").SetActive(true);
						a3_counterpart.instance.getButtonByPath("ready/yesorno/yes").interactable = false;
					}
				}
			};
			this.enterbtn3.onClick = delegate(GameObject go)
			{
				base.gameObject.SetActive(false);
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count < 2;
				if (flag)
				{
					Variant variant = new Variant();
					variant["npcid"] = 0;
					variant["ltpid"] = 110;
					variant["diff_lvl"] = 4;
					a3_counterpart.lvl = variant["diff_lvl"];
					BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
				}
				else
				{
					this.zhaunJi(4);
					a3_counterpart_multi_mate.open = true;
					bool flag2 = this.canINfb();
					if (flag2)
					{
						BaseProxy<TeamProxy>.getInstance().SendReady(true, 110u, 4u);
						a3_counterpart.instance.getTransformByPath("ready/yesorno/Text/name").GetComponent<Text>().text = "血之盛宴--地狱";
						a3_counterpart.instance.tenSen();
						bool meIsCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
						if (meIsCaptain)
						{
							a3_counterpart.instance.getTransformByPath("ready/yesorno/show/0/name").GetComponent<Text>().text = "自己";
						}
						a3_counterpart.instance.getGameObjectByPath("currentTeam").SetActive(true);
						a3_counterpart.instance.getGameObjectByPath("ready").SetActive(true);
						a3_counterpart.instance.getButtonByPath("ready/yesorno/yes").interactable = false;
					}
				}
			};
		}

		private void refreshExp()
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(110u);
			int num = variant["daily_cnt"];
			int num2 = 0;
			bool flag = MapModel.getInstance().dFbDta.ContainsKey(110);
			if (flag)
			{
				num2 = Mathf.Min(MapModel.getInstance().dFbDta[110].cycleCount, num);
			}
			base.getTransformByPath("choiceDef/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"剩余次数： <color=#00ff00>",
				num - num2,
				"/",
				num,
				"</color>"
			});
		}

		private void zhaunJi(int difs)
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(110u);
			this.zhaun = variant["diff_lvl"][difs]["open_zhuan"];
			this.ji = variant["diff_lvl"][difs]["open_level"];
		}

		private void changeSth()
		{
			this.zhaunJi(2);
			bool flag = this.zhaun > this.pzhuan || (this.zhaun == this.pzhuan && this.pji < this.ji);
			if (flag)
			{
				base.getGameObjectByPath("choiceDef/normal/normalText").SetActive(true);
				base.getGameObjectByPath("choiceDef/normal/enter").SetActive(false);
				base.getTransformByPath("choiceDef/normal/normalText").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.zhaun,
					"转",
					this.ji,
					"级开放"
				});
				this.enterbtn1.interactable = false;
			}
			else
			{
				bool flag2 = this.zhaun < this.pzhuan || (this.zhaun == this.pzhuan && this.pji >= this.ji);
				if (flag2)
				{
					base.getGameObjectByPath("choiceDef/normal/normalText").SetActive(false);
					base.getGameObjectByPath("choiceDef/normal/enter").SetActive(true);
					this.enterbtn1.interactable = true;
				}
			}
			this.zhaunJi(3);
			bool flag3 = this.zhaun > this.pzhuan || (this.zhaun == this.pzhuan && this.pji < this.ji);
			if (flag3)
			{
				base.getGameObjectByPath("choiceDef/deffi/deffiText").SetActive(true);
				base.getGameObjectByPath("choiceDef/deffi/enter").SetActive(false);
				base.getTransformByPath("choiceDef/deffi/deffiText").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.zhaun,
					"转",
					this.ji,
					"级开放"
				});
				this.enterbtn2.interactable = false;
			}
			else
			{
				bool flag4 = this.zhaun < this.pzhuan || (this.zhaun == this.pzhuan && this.pji >= this.ji);
				if (flag4)
				{
					base.getGameObjectByPath("choiceDef/deffi/deffiText").SetActive(false);
					base.getGameObjectByPath("choiceDef/deffi/enter").SetActive(true);
					this.enterbtn2.interactable = true;
				}
			}
			this.zhaunJi(4);
			bool flag5 = this.zhaun > this.pzhuan || (this.zhaun == this.pzhuan && this.pji < this.ji);
			if (flag5)
			{
				base.getGameObjectByPath("choiceDef/god/godText").SetActive(true);
				base.getGameObjectByPath("choiceDef/god/enter").SetActive(false);
				base.getTransformByPath("choiceDef/god/godText").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.zhaun,
					"转",
					this.ji,
					"级开放"
				});
				this.enterbtn3.interactable = false;
			}
			else
			{
				bool flag6 = this.zhaun < this.pzhuan || (this.zhaun == this.pzhuan && this.pji >= this.ji);
				if (flag6)
				{
					base.getGameObjectByPath("choiceDef/god/godText").SetActive(false);
					base.getGameObjectByPath("choiceDef/god/enter").SetActive(true);
					this.enterbtn3.interactable = true;
				}
			}
		}

		private bool canINfb()
		{
			int num = 0;
			a3_counterpart.instance.kout.Clear();
			a3_counterpart.instance.canin.Clear();
			for (int i = 0; i < a3_counterpart.nummeb; i++)
			{
				int lvl = (int)BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[i].lvl;
				int zhuan = (int)BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[i].zhuan;
				bool flag = this.zhaun > zhuan || (this.zhaun == zhuan && lvl < this.ji);
				if (flag)
				{
					num++;
					a3_counterpart.instance.kout.Add(BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[i].cid);
					a3_counterpart.instance.canin.Add(BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[i].cid);
				}
			}
			bool flag2 = num > 0;
			bool result;
			if (flag2)
			{
				a3_counterpart.instance.peopleLess.FindChild("yesorno/Text").GetComponent<Text>().text = "队伍中有" + num + "个玩家等级不足，是否移除他们。";
				a3_counterpart.instance.peopleLess.gameObject.SetActive(true);
				a3_counterpart.instance.canin.Clear();
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		public override void onShowed()
		{
			this.changeSth();
		}
	}
}
