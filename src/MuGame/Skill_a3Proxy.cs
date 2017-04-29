using Cross;
using GameFramework;
using MuGame.Qsmy.model;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class Skill_a3Proxy : BaseProxy<Skill_a3Proxy>
	{
		public static uint EUNRINFOS = 1u;

		public static uint RUNEINFOS = 0u;

		public static uint RUNERESEARCH = 2u;

		public static uint RUNEADDSPEED = 3u;

		public static uint RUNERESEARCHOVER = 4u;

		public static uint SKILLINFO = 5u;

		public static uint SKILLUP = 6u;

		public static uint SKILLUPINFO = 7u;

		public Skill_a3Proxy()
		{
			this.addProxyListener(86u, new Action<Variant>(this.onLoadSkill));
			this.addProxyListener(42u, new Action<Variant>(this.onLoadRune));
		}

		public void sendProxy(int skill_id, List<int> groups)
		{
			Variant variant = new Variant();
			bool flag = groups != null;
			if (flag)
			{
				variant["skill_groups"] = new Variant();
				for (int i = 0; i < groups.Count; i++)
				{
					variant["skill_groups"].pushBack(groups[i]);
				}
			}
			else
			{
				variant["skill_id"] = skill_id;
			}
			this.sendRPC(86u, variant);
		}

		public void sendSkillsneed(Variant skills)
		{
			Variant variant = new Variant();
			variant["skills"] = skills;
			this.sendRPC(86u, variant);
		}

		public void onLoadSkill(Variant data)
		{
			int num = data["res"];
			bool flag = data["res"] == 1;
			if (flag)
			{
				debug.Log("a3技能升级：" + data.dump());
				ModelBase<Skill_a3Model>.getInstance().skillinfos(data["skid"], data["sklvl"]);
				bool flag2 = skill_a3._instance != null;
				if (flag2)
				{
					skill_a3._instance.uprefreshskillinfo(data["skid"], data["sklvl"]);
					skill_a3._instance.showCanStudy();
					skill_a3._instance.showLevelupImage();
				}
				base.dispatchEvent(GameEvent.Create(Skill_a3Proxy.SKILLINFO, this, data, false));
				int num2 = -1;
				int num3 = 0;
				int[] skills = ModelBase<AutoPlayModel>.getInstance().Skills;
				for (int i = 0; i < skills.Length; i++)
				{
					int num4 = skills[i];
					bool flag3 = num4 == 0;
					if (flag3)
					{
						bool flag4 = false;
						int[] skills2 = ModelBase<AutoPlayModel>.getInstance().Skills;
						for (int j = 0; j < skills2.Length; j++)
						{
							int num5 = skills2[j];
							bool flag5 = num5 == data["skid"];
							if (flag5)
							{
								flag4 = true;
							}
						}
						bool flag6 = !flag4;
						if (flag6)
						{
							num2 = num3;
							break;
						}
					}
					num3++;
				}
				bool flag7 = num2 >= 0 && ModelBase<Skill_a3Model>.getInstance().skilldic.ContainsKey(data["skid"]) && ModelBase<Skill_a3Model>.getInstance().skilldic[data["skid"]].skillType2 != 1;
				if (flag7)
				{
					ModelBase<AutoPlayModel>.getInstance().Skills[num2] = data["skid"];
					ModelBase<AutoPlayModel>.getInstance().WriteLocalData();
				}
				bool flag8 = !ModelBase<Skill_a3Model>.getInstance().skillid_have.Contains(data["skid"]);
				if (flag8)
				{
					ModelBase<Skill_a3Model>.getInstance().skillid_have.Add(data["skid"]);
				}
				bool flag9 = data.ContainsKey("sklvl") && data["sklvl"] == 1;
				if (flag9)
				{
					bool flag10 = a3_skillopen.instance != null;
					if (flag10)
					{
						a3_skillopen.instance.open_id = data["skid"];
						a3_skillopen.instance.refreshInfo();
					}
					bool flag11 = ModelBase<Skill_a3Model>.getInstance().skillid_have.Count != 2;
					if (flag11)
					{
						int num6 = -1;
						int num7 = -1;
						for (int k = 0; k < ModelBase<Skill_a3Model>.getInstance().idsgroupone.Length; k++)
						{
							bool flag12 = ModelBase<Skill_a3Model>.getInstance().idsgroupone[k] <= 0;
							if (flag12)
							{
								num6 = k;
								break;
							}
						}
						bool flag13 = num6 < 0;
						if (flag13)
						{
							for (int l = 0; l < ModelBase<Skill_a3Model>.getInstance().idsgrouptwo.Length; l++)
							{
								bool flag14 = ModelBase<Skill_a3Model>.getInstance().idsgrouptwo[l] <= 0;
								if (flag14)
								{
									num7 = l;
									break;
								}
							}
						}
						bool flag15 = ModelBase<Skill_a3Model>.getInstance().skilldic.ContainsKey(data["skid"]);
						if (flag15)
						{
							bool flag16 = num6 >= 0;
							if (flag16)
							{
								ModelBase<Skill_a3Model>.getInstance().idsgroupone[num6] = data["skid"];
								bool flag17 = skillbar.instance != null && skillbar.instance.skillsetIdx == 1;
								if (flag17)
								{
									skillbar.instance.refreSkill(num6 + 1, data["skid"]);
								}
							}
							else
							{
								bool flag18 = num7 >= 0;
								if (flag18)
								{
									ModelBase<Skill_a3Model>.getInstance().idsgrouptwo[num7] = data["skid"];
									bool flag19 = skillbar.instance != null && skillbar.instance.skillsetIdx == 2;
									if (flag19)
									{
										skillbar.instance.refreSkill(num7 + 1, data["skid"]);
									}
								}
							}
							bool flag20 = skill_a3._instance != null;
							if (flag20)
							{
								skill_a3._instance.openrefreshskillinfo();
								int arg_485_0 = (num6 >= 0) ? 1 : 2;
							}
						}
					}
				}
			}
			else
			{
				bool flag21 = data["res"] == 2;
				if (flag21)
				{
					debug.Log("a3技能组合：" + data.dump());
					ModelBase<Skill_a3Model>.getInstance().skillGroups(data["skill_groups"]._arr);
				}
				else
				{
					bool flag22 = data["res"] == 4;
					if (flag22)
					{
						debug.Log("升级：" + data.dump());
						base.dispatchEvent(GameEvent.Create(Skill_a3Proxy.SKILLUPINFO, this, data, false));
					}
					else
					{
						bool flag23 = data["res"] < 0;
						if (flag23)
						{
						}
					}
				}
			}
		}

		public void sendProxys(int res, int id = 0, bool isfree = false)
		{
			Variant variant = new Variant();
			variant["op"] = res;
			switch (res)
			{
			case 2:
				variant["rune_id"] = id;
				break;
			case 3:
				variant["rune_id"] = id;
				variant["free"] = isfree;
				break;
			}
			this.sendRPC(42u, variant);
		}

		public void onLoadRune(Variant data)
		{
			switch (data["res"])
			{
			case 1:
				debug.Log("符文的信息：" + data.dump());
				base.dispatchEvent(GameEvent.Create(Skill_a3Proxy.RUNEINFOS, this, data, false));
				break;
			case 2:
			{
				debug.Log("符文开始研究结果：" + data.dump());
				ModelBase<Skill_a3Model>.getInstance().Reshreinfos(data["id"], -1, data["upgrade_count_down"]);
				base.dispatchEvent(GameEvent.Create(Skill_a3Proxy.RUNERESEARCH, this, data, false));
				bool flag = data["upgrade_count_down"] == 0;
				if (flag)
				{
					bool flag2 = a3_runeopen.instance != null;
					if (flag2)
					{
						a3_runeopen.instance.open_id = data["id"];
						a3_runeopen.instance.refreshInfo();
					}
				}
				break;
			}
			case 3:
				debug.Log("符文加速结果：" + data.dump());
				ModelBase<Skill_a3Model>.getInstance().Reshreinfos(data["id"], data["lvl"], 0);
				base.dispatchEvent(GameEvent.Create(Skill_a3Proxy.RUNEADDSPEED, this, data, false));
				break;
			case 4:
				debug.Log("升级完成给客户端数据：" + data.dump());
				ModelBase<Skill_a3Model>.getInstance().Reshreinfos(data["id"], data["lvl"], 0);
				base.dispatchEvent(GameEvent.Create(Skill_a3Proxy.RUNERESEARCHOVER, this, data, false));
				break;
			default:
				Globle.err_output(data["res"]);
				break;
			}
		}
	}
}
