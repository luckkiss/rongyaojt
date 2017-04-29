using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class A3_RankProxy : BaseProxy<A3_RankProxy>
	{
		public const uint ON_ACHIEVEMENT_CHANGE = 0u;

		public const uint ON_GET_ACHIEVEMENT_PRIZE = 1u;

		public const uint ON_REACH_ACHIEVEMENT = 2u;

		public const uint C2S_GET_ACHIEVEMENT_PRIZE = 4u;

		public static uint RANKADDLV = 5u;

		public static uint RANKREFRESH = 6u;

		public A3_RankProxy()
		{
			this.addProxyListener(34u, new Action<Variant>(this.onLoadinfos));
		}

		public void GetAchievementPrize(uint achievementID)
		{
			debug.Log("34_get_achievement_cmd_4_id_::" + achievementID);
			Variant variant = new Variant();
			variant["ach_cmd"] = 4u;
			variant["ach_id"] = achievementID;
			this.sendRPC(34u, variant);
		}

		public void sendProxy(uint ach_cmd, int add_type = -1, bool display = false)
		{
			Variant variant = new Variant();
			switch (ach_cmd)
			{
			case 6u:
				variant["display"] = display;
				break;
			}
			debug.Log("get_achievement_cmd_34_::" + ach_cmd);
			variant["ach_cmd"] = ach_cmd;
			this.sendRPC(34u, variant);
		}

		public void onLoadinfos(Variant data)
		{
			debug.Log("s2c_rank_achievement_::" + data.dump());
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			else
			{
				switch (num)
				{
				case 1:
					ModelBase<A3_AchievementModel>.getInstance().SyncAchievementDataByServer(data);
					base.dispatchEvent(GameEvent.Create(A3_RankProxy.RANKREFRESH, this, data, false));
					break;
				case 2:
					ModelBase<A3_AchievementModel>.getInstance().OnAchievementChangeFromServer(data);
					base.dispatchEvent(GameEvent.Create(0u, this, data, false));
					break;
				case 3:
					break;
				case 4:
				{
					ModelBase<A3_AchievementModel>.getInstance().OnGetAchievePrize(data);
					bool flag2 = data.ContainsKey("ach_point");
					if (flag2)
					{
						ModelBase<PlayerModel>.getInstance().ach_point = data["ach_point"];
						a3_RankModel.nowexp = data["ach_point"];
						ModelBase<A3_AchievementModel>.getInstance().AchievementPoint = data["ach_point"];
						base.dispatchEvent(GameEvent.Create(A3_RankProxy.RANKREFRESH, this, data, false));
					}
					base.dispatchEvent(GameEvent.Create(1u, this, data, false));
					break;
				}
				case 5:
				{
					debug.Log("升级成功：" + data["title"]);
					bool flag3 = data.ContainsKey("title");
					if (flag3)
					{
						ModelBase<PlayerModel>.getInstance().titileChange(data);
						ModelBase<a3_RankModel>.getInstance().refreinfo(data["title"], a3_RankModel.nowexp, true);
						bool flag4 = data.ContainsKey("ach_point");
						if (flag4)
						{
							ModelBase<PlayerModel>.getInstance().ach_point = data["ach_point"];
							a3_RankModel.nowexp = data["ach_point"];
							ModelBase<A3_AchievementModel>.getInstance().AchievementPoint = data["ach_point"];
							base.dispatchEvent(GameEvent.Create(A3_RankProxy.RANKREFRESH, this, data, false));
						}
						base.dispatchEvent(GameEvent.Create(A3_RankProxy.RANKADDLV, this, data, false));
					}
					break;
				}
				case 6:
					debug.Log("显示或者隐藏：" + data["title_display"]._bool.ToString());
					ModelBase<PlayerModel>.getInstance().titleShoworHide(data);
					break;
				case 7:
					ModelBase<A3_AchievementModel>.getInstance().OnAchievementReachChange(data);
					base.dispatchEvent(GameEvent.Create(2u, this, data, false));
					break;
				default:
					Globle.err_output(data["res"]);
					break;
				}
				bool flag5 = data.ContainsKey("ach_point");
				if (flag5)
				{
					ModelBase<PlayerModel>.getInstance().ach_point = data["ach_point"];
					ModelBase<A3_AchievementModel>.getInstance().AchievementPoint = data["ach_point"];
					a3_RankModel.nowexp = data["ach_point"];
				}
			}
		}
	}
}
