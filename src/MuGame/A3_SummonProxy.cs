using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class A3_SummonProxy : BaseProxy<A3_SummonProxy>
	{
		public static uint EVENT_LOADALL = 0u;

		public static uint EVENT_SHOWIDENTIFYANSWER = 1u;

		public static uint EVENT_PUTSUMMONINBAG = 51u;

		public static uint EVENT_USESUMMONFROMBAG = 52u;

		public static uint EVENT_UPDATE = 111u;

		public static uint EVENT_STUDY = 8u;

		public static uint EVENT_FEEDEXP = 31u;

		public static uint EVENT_FEEDSM = 38u;

		public static uint EVENT_CHUZHAN = 33u;

		public static uint EVENT_XIUXI = 34u;

		public static uint EVENT_XUEXI = 35u;

		public static uint EVENT_FORGET = 36u;

		public static uint EVENT_SUMINFO = 37u;

		public static uint EVENT_INTEGRATION = 32u;

		public A3_SummonProxy()
		{
			this.addProxyListener(79u, new Action<Variant>(this.SummonOP));
		}

		public void sendLoadSummons()
		{
			Variant variant = new Variant();
			variant["tp"] = 0;
			this.sendRPC(79u, variant);
		}

		public void getsummonInfo(uint id)
		{
			Variant variant = new Variant();
			variant["tp"] = 9;
			variant["id"] = id;
			this.sendRPC(79u, variant);
		}

		public void sendIdentifySummons(List<uint> idlist)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			foreach (uint current in idlist)
			{
				Variant variant3 = new Variant();
				variant3["id"] = current;
				int val = (int)current;
				variant2._arr.Add(val);
			}
			variant["ids"] = variant2;
			variant["tp"] = 1;
			this.sendRPC(79u, variant);
		}

		public void sendZHSummon(uint tpid)
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			variant["tpid"] = tpid;
			bool flag = ModelBase<A3_SummonModel>.getInstance().GetSummons().Count >= 50;
			if (flag)
			{
				flytxt.instance.fly("列表已满，召唤兽已自动收入背包", 0, default(Color), null);
			}
			this.sendRPC(79u, variant);
			debug.Log(variant.dump());
		}

		public void sendOPSkill(int op, int summonid, int skillid, int index)
		{
			Variant variant = new Variant();
			variant["tp"] = 4;
			variant["op"] = op;
			variant["id"] = summonid;
			variant["skill_id"] = skillid;
			variant["index"] = index;
			this.sendRPC(79u, variant);
			debug.Log(variant.dump());
		}

		public void sendPutSummonInBag(int id)
		{
			Variant variant = new Variant();
			variant["tp"] = 5;
			variant["op"] = 1;
			variant["id"] = id;
			this.sendRPC(79u, variant);
		}

		public void sendUseSummonFromBag(int id)
		{
			Variant variant = new Variant();
			variant["tp"] = 5;
			variant["op"] = 2;
			variant["id"] = id;
			this.sendRPC(79u, variant);
		}

		public void sendFeedExp(int summonid, int feeditemid, int num)
		{
			Variant variant = new Variant();
			variant["tp"] = 3;
			variant["op"] = 1;
			variant["id"] = summonid;
			variant["feed_id"] = feeditemid;
			variant["num"] = num;
			debug.Log("召唤兽经验" + variant.dump());
			this.sendRPC(79u, variant);
		}

		public void sendFeedSM(int summonid, int feeditemid, int num)
		{
			Variant variant = new Variant();
			variant["tp"] = 3;
			variant["op"] = 2;
			variant["id"] = summonid;
			variant["feed_id"] = feeditemid;
			variant["num"] = num;
			this.sendRPC(79u, variant);
		}

		public void sendIntegration(int mainid, int otherid)
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			variant["id"] = mainid;
			variant["blend_id"] = otherid;
			this.sendRPC(79u, variant);
		}

		public void sendGoAttack(int summonid)
		{
			Variant variant = new Variant();
			variant["tp"] = 6;
			variant["op"] = 1;
			variant["id"] = summonid;
			this.sendRPC(79u, variant);
		}

		public void sendGoBack(int summonid)
		{
			Variant variant = new Variant();
			variant["tp"] = 6;
			variant["op"] = 2;
			variant["id"] = summonid;
			this.sendRPC(79u, variant);
		}

		private void SummonOP(Variant data)
		{
			debug.Log("召唤兽信息" + data.dump());
			int num = -1;
			Variant variant = new Variant();
			uint num2 = 0u;
			bool flag = data.ContainsKey("tp");
			if (flag)
			{
				num = data["tp"];
				bool flag2 = num < 0;
				if (flag2)
				{
					Globle.err_output(num);
					return;
				}
			}
			bool flag3 = data.ContainsKey("summon");
			if (flag3)
			{
			}
			switch (num)
			{
			case 0:
			{
				variant = data["summons"];
				bool flag4 = variant != null;
				if (flag4)
				{
					foreach (Variant current in variant._arr)
					{
						ModelBase<A3_SummonModel>.getInstance().AddSummon(current);
					}
				}
				bool flag5 = data.ContainsKey("summon_on");
				if (flag5)
				{
					Variant variant2 = data["summon_on"];
					ModelBase<A3_SummonModel>.getInstance().nowShowAttackID = variant2["id"];
					ModelBase<A3_SummonModel>.getInstance().nowShowAttackModel = variant2["att_type"];
				}
				ModelBase<A3_SummonModel>.getInstance().SortSummon();
				break;
			}
			case 1:
			{
				bool flag6 = data.ContainsKey("summons");
				if (flag6)
				{
					variant = data["summons"];
					foreach (Variant current2 in variant._arr)
					{
						ModelBase<A3_SummonModel>.getInstance().AddSummon(current2);
					}
					ModelBase<A3_SummonModel>.getInstance().SortSummon();
				}
				base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_SHOWIDENTIFYANSWER, this, data, false));
				break;
			}
			case 2:
			{
				int id = data["summon_id"];
				ModelBase<A3_SummonModel>.getInstance().RemoveSummon(id);
				base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_PUTSUMMONINBAG, this, data, false));
				break;
			}
			case 3:
				variant = data["summon"];
				ModelBase<A3_SummonModel>.getInstance().AddSummon(variant);
				ModelBase<A3_SummonModel>.getInstance().SortSummon();
				base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_USESUMMONFROMBAG, this, data, false));
				break;
			case 4:
			{
				num2 = data["summon_id"];
				bool flag7 = ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(num2);
				if (flag7)
				{
					ModelBase<A3_SummonModel>.getInstance().GetSummons().Remove(num2);
				}
				bool flag8 = data.ContainsKey("summon");
				if (flag8)
				{
					variant = data["summon"];
					uint key = variant["id"];
					bool flag9 = ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(key);
					if (flag9)
					{
						ModelBase<A3_SummonModel>.getInstance().GetSummons().Remove(key);
					}
					ModelBase<A3_SummonModel>.getInstance().AddSummon(variant);
					ModelBase<A3_SummonModel>.getInstance().SortSummon();
				}
				base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_INTEGRATION, this, data, false));
				break;
			}
			case 5:
			{
				bool flag10 = data.ContainsKey("summon_id");
				if (flag10)
				{
					num2 = data["summon_id"];
					bool flag11 = ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(num2);
					if (flag11)
					{
						bool flag12 = data.ContainsKey("add_exp");
						if (flag12)
						{
							int currentexp = data["add_exp"];
							a3_BagItemData value = ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2];
							value.summondata.currentexp = currentexp;
							ModelBase<A3_SummonModel>.getInstance().GetSummons().Remove(num2);
							ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2] = value;
							ModelBase<A3_SummonModel>.getInstance().SortSummon();
						}
					}
				}
				else
				{
					bool flag13 = data.ContainsKey("summon");
					if (flag13)
					{
						variant = data["summon"];
						uint key2 = variant["id"];
						bool flag14 = ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(key2);
						if (flag14)
						{
							ModelBase<A3_SummonModel>.getInstance().GetSummons().Remove(key2);
						}
						ModelBase<A3_SummonModel>.getInstance().AddSummon(variant);
						ModelBase<A3_SummonModel>.getInstance().SortSummon();
					}
				}
				base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_FEEDEXP, this, data, false));
				bool flag15 = a3_summon.instan;
				if (flag15)
				{
					a3_summon.instan.Event_UIFeedEXPClicked(null);
				}
				break;
			}
			case 6:
			{
				bool flag16 = data.ContainsKey("summon_id");
				if (flag16)
				{
					num2 = data["summon_id"];
					bool flag17 = ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(num2);
					if (flag17)
					{
						bool flag18 = data.ContainsKey("summon_life");
						if (flag18)
						{
							int lifespan = data["summon_life"];
							a3_BagItemData value2 = ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2];
							value2.summondata.lifespan = lifespan;
							ModelBase<A3_SummonModel>.getInstance().GetSummons().Remove(num2);
							ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2] = value2;
							ModelBase<A3_SummonModel>.getInstance().SortSummon();
						}
					}
					base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_FEEDSM, this, data, false));
					bool flag19 = a3_summon.instan;
					if (flag19)
					{
						a3_summon.instan.Event_UIFeedSMClicked(null);
					}
					bool flag20 = data["summon_id"] == ModelBase<A3_SummonModel>.getInstance().nowShowAttackID || data["summon_id"] == ModelBase<A3_SummonModel>.getInstance().lastatkID;
					if (flag20)
					{
						bool flag21 = a3_herohead.instance;
						if (flag21)
						{
							a3_herohead.instance.refresh_sumbar();
						}
					}
				}
				break;
			}
			case 7:
			{
				bool flag22 = data.ContainsKey("summon_id");
				if (flag22)
				{
					num2 = data["summon_id"];
				}
				bool flag23 = ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(num2);
				if (flag23)
				{
					int value3 = data["skill_id"];
					int key3 = data["index"];
					a3_BagItemData a3_BagItemData = ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2];
					bool flag24 = a3_BagItemData.summondata.skills.ContainsKey(key3);
					if (flag24)
					{
						a3_BagItemData.summondata.skills.Remove(key3);
					}
					else
					{
						a3_BagItemData.summondata.skills[key3] = value3;
					}
					ModelBase<A3_SummonModel>.getInstance().GetSummons().Remove(num2);
					ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2] = a3_BagItemData;
					ModelBase<A3_SummonModel>.getInstance().SortSummon();
				}
				base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_FORGET, this, data, false));
				break;
			}
			case 8:
			{
				bool flag25 = data["skill_id"] == 0;
				if (flag25)
				{
					flytxt.instance.fly("学习失败！", 0, default(Color), null);
					bool flag26 = a3_summon.instan;
					if (flag26)
					{
						a3_summon.instan.ShowSkillBooks();
					}
				}
				else
				{
					bool flag27 = data.ContainsKey("summon_id");
					if (flag27)
					{
						num2 = data["summon_id"];
					}
					bool flag28 = ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(num2);
					if (flag28)
					{
						int value4 = data["skill_id"];
						int key4 = data["index"];
						a3_BagItemData a3_BagItemData2 = ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2];
						bool flag29 = a3_BagItemData2.summondata.skills.ContainsKey(key4);
						if (flag29)
						{
							a3_BagItemData2.summondata.skills.Remove(key4);
						}
						else
						{
							a3_BagItemData2.summondata.skills[key4] = value4;
						}
						ModelBase<A3_SummonModel>.getInstance().GetSummons().Remove(num2);
						ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2] = a3_BagItemData2;
						ModelBase<A3_SummonModel>.getInstance().SortSummon();
					}
					base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_XUEXI, this, data, false));
				}
				break;
			}
			case 9:
			{
				num2 = data["summon_id"];
				ModelBase<A3_SummonModel>.getInstance().nowShowAttackID = num2;
				bool flag30 = ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(num2);
				if (flag30)
				{
					ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2].summondata.status = 1;
				}
				flytxt.instance.fly("召唤兽已出战", 0, default(Color), null);
				bool flag31 = a3_herohead.instance;
				if (flag31)
				{
					a3_herohead.instance.refresh_sumbar();
				}
				base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_CHUZHAN, this, data, false));
				break;
			}
			case 10:
			{
				num2 = data["summon_id"];
				ModelBase<A3_SummonModel>.getInstance().nowShowAttackID = 0u;
				bool flag32 = ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(num2);
				if (flag32)
				{
					ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2].summondata.status = 0;
				}
				bool flag33 = data.ContainsKey("summon_life");
				if (flag33)
				{
					bool flag34 = ModelBase<A3_SummonModel>.getInstance().GetSummons().ContainsKey(num2);
					if (flag34)
					{
						a3_BagItemData a3_BagItemData3 = ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2];
						int lifespan2 = a3_BagItemData3.summondata.lifespan;
						a3_BagItemData3.summondata.lifespan = data["summon_life"];
						ModelBase<A3_SummonModel>.getInstance().GetSummons().Remove(num2);
						ModelBase<A3_SummonModel>.getInstance().GetSummons()[num2] = a3_BagItemData3;
						ModelBase<A3_SummonModel>.getInstance().SortSummon();
						bool flag35 = lifespan2 > data["summon_life"] && data.ContainsKey("die_timelist");
						if (flag35)
						{
							ModelBase<A3_SummonModel>.getInstance().addSumCD((int)num2, (float)(data["die_timelist"][0]._int - muNetCleint.instance.CurServerTimeStamp));
							debug.Log("time" + (data["die_timelist"][0]._int - muNetCleint.instance.CurServerTimeStamp));
							bool flag36 = a3_herohead.instance;
							if (flag36)
							{
								ModelBase<A3_SummonModel>.getInstance().lastatkID = num2;
								a3_herohead.instance.refresh_sumbar();
								a3_herohead.instance.refresh_sumHp(0, 1);
							}
						}
					}
				}
				else
				{
					bool flag37 = a3_herohead.instance;
					if (flag37)
					{
						ModelBase<A3_SummonModel>.getInstance().lastatkID = 0u;
						a3_herohead.instance.refresh_sumbar();
						a3_herohead.instance.refresh_sumHp(0, 1);
					}
				}
				bool flag38 = !data.ContainsKey("summon_life") || data["summon_life"] > 0;
				if (flag38)
				{
					flytxt.instance.fly("召唤兽已休息", 0, default(Color), null);
				}
				base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_XIUXI, this, data, false));
				break;
			}
			case 11:
				base.dispatchEvent(GameEvent.Create(A3_SummonProxy.EVENT_SUMINFO, this, data, false));
				break;
			}
		}
	}
}
