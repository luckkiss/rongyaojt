using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class A3_LegionProxy : BaseProxy<A3_LegionProxy>
	{
		public const int EVENT_GETINFO = 1;

		public const int EVENT_CREATE = 2;

		public const int EVENT_APPLYFOR = 3;

		public const int EVENT_APPROVEORREJECT = 4;

		public const int EVENT_LVUP = 5;

		public const int EVENT_DONATE = 6;

		public const int EVENT_PRODE = 7;

		public const int EVENT_BELEADER = 8;

		public const int EVENT_QUIT = 9;

		public const int EVENT_REMOVE = 10;

		public const int EVENT_APPLYMODE = 11;

		public const int EVENT_CHANGENOTICE = 12;

		public const int EVENT_GETMEMBER = 14;

		public const int EVENT_GETDIARY = 15;

		public const int EVENT_GETAPPLICANT = 16;

		public const int EVENT_LOADLIST = 17;

		public const int EVENT_INVITE = 18;

		public const int EVENT_ACCEPTAINVITE = 19;

		public const int EVENT_DIRECT = 21;

		public const int EVENT_DELETECLAN = 22;

		public const int EVENT_CHECKNAME = 24;

		public const int EVENT_TASKREWARD = 32;

		public const int EVENT_BEINVITE = 33;

		public const int EVENT_APPLYSUCCESSFUL = 34;

		public const int EVENT_GETQUIT = 36;

		public const int EVENT_GETDIN = 37;

		public const int EVENT_UPBUFF = 25;

		public const int EVENT_REPAIR = 26;

		public int gold;

		public int lvl;

		public bool join_legion;

		public A3_LegionProxy()
		{
			this.addProxyListener(210u, new Action<Variant>(this.OnLegion));
		}

		public void SendGetMember()
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 14;
			this.sendRPC(210u, variant);
		}

		public void SendLegion_Repair()
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 26;
			this.sendRPC(210u, variant);
		}

		public void SendGetInfo()
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 1;
			this.sendRPC(210u, variant);
		}

		public void SendApplyFor(uint clid)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 3;
			variant["clid"] = clid;
			this.sendRPC(210u, variant);
		}

		public void SendUp_Buff()
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 25;
			this.sendRPC(210u, variant);
		}

		public void SendYN(uint cid, bool yes)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			variant["clan_cmd"] = 4;
			variant["approved"] = yes;
			this.sendRPC(210u, variant);
		}

		public void SendLVUP()
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 5;
			this.sendRPC(210u, variant);
		}

		public void SendDonate(uint money)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 6;
			variant["money"] = money;
			this.sendRPC(210u, variant);
		}

		public void SendBeLeader(uint cid)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 8;
			variant["cid"] = cid;
			this.sendRPC(210u, variant);
		}

		public void PromotionOrDemotion(uint cid, uint type)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 7;
			variant["cid"] = cid;
			variant["tp"] = type;
			this.sendRPC(210u, variant);
		}

		public void SendChangeApplyMode(bool b)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 11;
			variant["direct_join"] = (b ? 1 : 0);
			this.sendRPC(210u, variant);
		}

		public void SendChangeToggleMode(bool b)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 21;
			variant["direct_join"] = (b ? 1 : 0);
			this.sendRPC(210u, variant);
		}

		public void SendQuit()
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 9;
			this.sendRPC(210u, variant);
		}

		public void SendInvite(uint cid)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 18;
			variant["cid"] = cid;
			this.sendRPC(210u, variant);
		}

		public void SendRemove(uint cid)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 10;
			variant["cid"] = cid;
			this.sendRPC(210u, variant);
		}

		public void SendGetApplicant()
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 16;
			this.sendRPC(210u, variant);
		}

		public void SendGetDiary()
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 15;
			bool flag = ModelBase<A3_LegionModel>.getInstance().logdata != null && ModelBase<A3_LegionModel>.getInstance().logdata.ContainsKey("clanlog_list") && ModelBase<A3_LegionModel>.getInstance().logdata["clanlog_list"]._arr.Count > 0;
			if (flag)
			{
				Variant variant2 = ModelBase<A3_LegionModel>.getInstance().logdata["clanlog_list"];
				int num = 0;
				foreach (Variant current in variant2._arr)
				{
					int num2 = current["id"];
					bool flag2 = num2 > num;
					if (flag2)
					{
						num = num2;
					}
				}
				variant["id"] = num;
			}
			this.sendRPC(210u, variant);
		}

		public void SendGetList()
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 17;
			this.sendRPC(210u, variant);
		}

		public void SendChangeNotice(string notice)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 12;
			variant["notice"] = notice;
			this.sendRPC(210u, variant);
		}

		public void SendCreateLegion(string name)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 2;
			variant["clname"] = name;
			this.sendRPC(210u, variant);
		}

		public void SendDeleteLegion()
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 22;
			this.sendRPC(210u, variant);
		}

		public void SendCheckName(string name)
		{
			Variant variant = new Variant();
			variant["clan_cmd"] = 24;
			variant["clname"] = name;
			this.sendRPC(210u, variant);
		}

		public void SendAcceptInvite(uint clanid, bool approved)
		{
			if (approved)
			{
				this.join_legion = true;
			}
			Variant variant = new Variant();
			variant["clan_cmd"] = 19;
			variant["clanid"] = clanid;
			variant["approved"] = approved;
			this.sendRPC(210u, variant);
		}

		private void OnLegion(Variant data)
		{
			int num = data["res"];
			debug.Log("军团消息" + data.dump());
			Variant variant = new Variant();
			switch (num)
			{
			case 1:
			{
				bool flag = data.ContainsKey("id");
				if (flag)
				{
					A3_LegionData a3_LegionData = default(A3_LegionData);
					a3_LegionData.id = data["id"];
					a3_LegionData.lvl = data["lvl"];
					a3_LegionData.clname = data["clname"];
					a3_LegionData.name = data["name"];
					a3_LegionData.notice = data["notice"];
					a3_LegionData.gold = data["money"];
					a3_LegionData.plycnt = data["plycnt"];
					a3_LegionData.exp = data["clan_pt"];
					bool flag2 = data.ContainsKey("ol_cnt");
					if (flag2)
					{
						a3_LegionData.ol_cnt = data["ol_cnt"];
					}
					bool flag3 = data.ContainsKey("combpt");
					if (flag3)
					{
						a3_LegionData.combpt = data["combpt"];
					}
					bool flag4 = data.ContainsKey("rankidx");
					if (flag4)
					{
						a3_LegionData.rankidx = data["rankidx"];
					}
					bool flag5 = data.ContainsKey("clanc");
					if (flag5)
					{
						a3_LegionData.clanc = data["clanc"];
					}
					a3_LegionData.anabasis_tm = data["anabasis_tm"];
					ModelBase<A3_LegionModel>.getInstance().myLegion = a3_LegionData;
					int num2 = data["direct_join"];
					ModelBase<A3_LegionModel>.getInstance().CanAutoApply = (num2 == 1);
					ModelBase<A3_LegionModel>.getInstance().SetMyLegion(a3_LegionData.lvl);
					ModelBase<A3_LegionModel>.getInstance().donate = data["donate"];
					this.gold = a3_LegionData.gold;
					this.lvl = a3_LegionData.lvl;
				}
				else
				{
					ModelBase<A3_LegionModel>.getInstance().myLegion = default(A3_LegionData);
				}
				base.dispatchEvent(GameEvent.Create(1u, this, data, false));
				break;
			}
			case 2:
				base.dispatchEvent(GameEvent.Create(2u, this, data, false));
				base.removeEventListener(24u, new Action<GameEvent>(a3_legion.mInstance.SetCheckName));
				break;
			case 3:
			{
				int num3 = data["clid"];
				base.dispatchEvent(GameEvent.Create(3u, this, data, false));
				flytxt.instance.fly("申请成功！", 0, default(Color), null);
				break;
			}
			case 4:
				base.dispatchEvent(GameEvent.Create(4u, this, data, false));
				break;
			case 5:
				ModelBase<A3_LegionModel>.getInstance().myLegion.gold = data["money"];
				ModelBase<A3_LegionModel>.getInstance().myLegion.lvl = data["lvl"];
				ModelBase<A3_LegionModel>.getInstance().myLegion.exp = 0;
				ModelBase<A3_LegionModel>.getInstance().SetMyLegion(ModelBase<A3_LegionModel>.getInstance().myLegion.lvl);
				base.dispatchEvent(GameEvent.Create(1u, this, data, false));
				flytxt.instance.fly("升级骑士团成功！", 0, default(Color), null);
				break;
			case 6:
			{
				int num4 = data["money"];
				flytxt.instance.fly("获得了" + num4 / 1000 + "点贡献度", 0, default(Color), null);
				this.SendGetInfo();
				break;
			}
			case 8:
				this.SendGetMember();
				break;
			case 9:
			{
				ModelBase<A3_LegionModel>.getInstance().myLegion = default(A3_LegionData);
				base.dispatchEvent(GameEvent.Create(9u, this, data, false));
				TaskData expr_4C9 = a3_task_auto.instance.executeTask;
				bool flag6 = expr_4C9 != null && expr_4C9.taskT == TaskType.CLAN;
				if (flag6)
				{
					SelfRole.fsm.Stop();
					flytxt.instance.fly("你已退出军团无法继续进行任务", 0, default(Color), null);
				}
				else
				{
					flytxt.instance.fly("您已成功退出骑士团！", 0, default(Color), null);
				}
				break;
			}
			case 10:
			{
				int key = data["cid"];
				bool flag7 = ModelBase<A3_LegionModel>.getInstance().members.ContainsKey(key);
				if (flag7)
				{
					ModelBase<A3_LegionModel>.getInstance().members.Remove(key);
				}
				base.dispatchEvent(GameEvent.Create(14u, this, data, false));
				break;
			}
			case 11:
			{
				int num5 = data["direct_join"];
				ModelBase<A3_LegionModel>.getInstance().CanAutoApply = (num5 == 1);
				base.dispatchEvent(GameEvent.Create(11u, this, data, false));
				break;
			}
			case 12:
				base.dispatchEvent(GameEvent.Create(12u, this, data, false));
				break;
			case 14:
				variant = data["pls"];
				ModelBase<A3_LegionModel>.getInstance().members.Clear();
				foreach (Variant current in variant._arr)
				{
					ModelBase<A3_LegionModel>.getInstance().AddMember(current);
				}
				base.dispatchEvent(GameEvent.Create(14u, this, data, false));
				break;
			case 15:
			{
				bool flag8 = data != null && data.ContainsKey("clanlog_list") && data["clanlog_list"]._arr.Count > 0;
				if (flag8)
				{
					ModelBase<A3_LegionModel>.getInstance().AddLog(data);
				}
				base.dispatchEvent(GameEvent.Create(15u, this, data, false));
				break;
			}
			case 16:
				ModelBase<A3_LegionModel>.getInstance().RefreshApplicant(data);
				base.dispatchEvent(GameEvent.Create(16u, this, data, false));
				break;
			case 17:
				ModelBase<A3_LegionModel>.getInstance().list.Clear();
				ModelBase<A3_LegionModel>.getInstance().list2.Clear();
				variant = data["info"];
				foreach (Variant current2 in variant._arr)
				{
					A3_LegionData item = default(A3_LegionData);
					item.id = current2["id"];
					item.clname = current2["clname"];
					item.combpt = current2["combpt"];
					item.lvl = current2["lvl"];
					item.name = current2["name"];
					item.plycnt = current2["plycnt"];
					item.direct_join = current2["direct_join"];
					item.huoyue = current2["last_active"];
					ModelBase<A3_LegionModel>.getInstance().list.Add(item);
					ModelBase<A3_LegionModel>.getInstance().list2.Add(item);
				}
				base.dispatchEvent(GameEvent.Create(17u, this, data, false));
				break;
			case 18:
				base.dispatchEvent(GameEvent.Create(18u, this, data, false));
				break;
			case 22:
				this.SendGetInfo();
				base.dispatchEvent(GameEvent.Create(22u, this, data, false));
				flytxt.instance.fly("您已成功解散骑士团！", 0, default(Color), null);
				break;
			case 24:
				base.dispatchEvent(GameEvent.Create(24u, this, data, false));
				break;
			case 25:
				flytxt.instance.fly("军团buff提升成功", 0, default(Color), null);
				break;
			case 26:
				base.dispatchEvent(GameEvent.Create(26u, this, data, false));
				break;
			case 32:
			{
				bool flag9 = ModelBase<A3_LegionModel>.getInstance().myLegion.id != 0;
				if (flag9)
				{
					bool flag10 = ModelBase<A3_TaskModel>.getInstance() == null || ModelBase<A3_TaskModel>.getInstance().GetClanTask() == null;
					int taskCount;
					if (flag10)
					{
						taskCount = 9;
					}
					else
					{
						taskCount = ModelBase<A3_TaskModel>.getInstance().GetClanTask().taskCount;
					}
					Dictionary<uint, int> clanRewardDic = ModelBase<A3_TaskModel>.getInstance().GetClanRewardDic(taskCount);
					flytxt.instance.StopDelayFly();
					bool flag11 = data.ContainsKey("money");
					if (flag11)
					{
						bool flag12 = clanRewardDic.ContainsKey((uint)A3_TaskModel.REWARD_CLAN_MONEY);
						if (flag12)
						{
							flytxt.instance.AddDelayFlytxt("军团资金+" + clanRewardDic[(uint)A3_TaskModel.REWARD_CLAN_MONEY]);
						}
						ModelBase<A3_LegionModel>.getInstance().myLegion.gold = data["money"]._int;
					}
					bool flag13 = data.ContainsKey("clan_pt");
					if (flag13)
					{
						bool flag14 = clanRewardDic.ContainsKey((uint)A3_TaskModel.REWARD_CLAN_EXP);
						if (flag14)
						{
							flytxt.instance.AddDelayFlytxt("军团经验+" + clanRewardDic[(uint)A3_TaskModel.REWARD_CLAN_EXP]);
						}
						ModelBase<A3_LegionModel>.getInstance().myLegion.clan_pt = data["clan_pt"]._int;
					}
					bool flag15 = data.ContainsKey("donate");
					if (flag15)
					{
						bool flag16 = clanRewardDic.ContainsKey((uint)A3_TaskModel.REWARD_CLAN_DONATE);
						if (flag16)
						{
							flytxt.instance.AddDelayFlytxt("军团贡献+" + clanRewardDic[(uint)A3_TaskModel.REWARD_CLAN_DONATE]);
						}
						ModelBase<A3_LegionModel>.getInstance().donate = data["donate"]._int;
					}
					bool flag17 = data.ContainsKey("active");
					if (flag17)
					{
						ModelBase<A3_LegionModel>.getInstance().myLegion.huoyue = data["active"]._int;
					}
					flytxt.instance.StartDelayFly(0f, 0.2f);
				}
				break;
			}
			case 33:
			{
				uint clanid = data["clanid"];
				string text = data["name"];
				string text2 = data["clan_name"];
				int num6 = data["clan_lvl"];
				bool isOn = a3_legion.mInstance.dic0.isOn;
				if (isOn)
				{
					this.SendAcceptInvite(clanid, true);
					flytxt.instance.fly("您已接受军团邀请！", 0, default(Color), null);
					BaseProxy<a3_dartproxy>.getInstance().sendDartGo();
					base.dispatchEvent(GameEvent.Create(19u, this, data, false));
				}
				else
				{
					MsgBoxMgr.getInstance().showConfirm(string.Concat(new object[]
					{
						text,
						" 邀请你加入 ",
						num6,
						"级骑士团 ",
						text2
					}), delegate
					{
						this.SendAcceptInvite(clanid, true);
					}, delegate
					{
						this.SendAcceptInvite(clanid, false);
					}, 0);
				}
				base.dispatchEvent(GameEvent.Create(33u, this, data, false));
				break;
			}
			case 34:
			{
				bool flag18 = data["approved"];
				bool flag19 = flag18;
				if (flag19)
				{
					BaseProxy<A3_LegionProxy>.getInstance().SendGetInfo();
				}
				BaseProxy<a3_dartproxy>.getInstance().sendDartGo();
				base.dispatchEvent(GameEvent.Create(34u, this, data, false));
				break;
			}
			case 35:
			{
				BaseProxy<A3_LegionProxy>.getInstance().SendGetMember();
				int num7 = data["cid"];
				int num8 = data["clanc"];
				int num9 = data["oldclanc"];
				string str = data["name"];
				string str2 = string.Empty;
				bool flag20 = num7 == (int)ModelBase<PlayerModel>.getInstance().cid;
				if (flag20)
				{
					str = "您";
				}
				bool flag21 = num9 > num8;
				if (flag21)
				{
					str2 = "的军团职位被降为：";
				}
				else
				{
					str2 = "的军团职位被升为：";
				}
				flytxt.instance.fly(str + str2 + ModelBase<A3_LegionModel>.getInstance().GetClancToName(num8), 0, default(Color), null);
				break;
			}
			case 36:
			{
				BaseProxy<A3_LegionProxy>.getInstance().SendGetInfo();
				bool flag22 = a3_buff.instance != null;
				if (flag22)
				{
					a3_buff.instance.Quited();
				}
				break;
			}
			case 37:
				flytxt.instance.fly("您已成功加入骑士团！", 0, default(Color), null);
				BaseProxy<a3_dartproxy>.getInstance().sendDartGo();
				base.dispatchEvent(GameEvent.Create(2u, this, data, false));
				break;
			}
		}
	}
}
