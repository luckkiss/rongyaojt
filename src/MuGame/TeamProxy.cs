using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class TeamProxy : BaseProxy<TeamProxy>
	{
		private enum TeamCmd
		{
			CreateTeam = 1,
			GetMapTeam,
			WatchTeamInfo,
			SyncTeamBlood,
			CurrentMapTeamPos,
			ApplyJoinTeam,
			AffirmApply,
			LeaveTeam,
			Invite,
			AffirmInvite,
			KickOut,
			ChangeCaptain,
			Dissolve,
			EditorInfo,
			ChangeObject,
			Ready,
			Get_curPageTeam,
			NoticeCaptainNewInfo = 20,
			NoticeApplyBeRefuse,
			NoticeHaveMemberLeave,
			NoticeInvite,
			NewMemberJoin,
			NoticeInviteBeRefuse,
			NoticeOnlineStateChanged
		}

		public static uint EVENT_CREATETEAM = 1201u;

		public static uint EVENT_DISSOLVETEAM = 1202u;

		public static uint EVENT_TEAMLISTINFO = 1203u;

		public static uint EVENT_AFFIRMINVITE = 1205u;

		public static uint EVENT_NEWMEMBERJOIN = 1206u;

		public static uint EVENT_KICKOUT = 1207u;

		public static uint EVENT_CHANGETEAMINFO = 1208u;

		public static uint EVENT_NOTICEHAVEMEMBERLEAVE = 1209u;

		public static uint EVENT_LEAVETEAM = 1210u;

		public static uint EVENT_CHANGECAPTAIN = 1211u;

		public static uint EVENT_SYNCTEAMBLOOD = 1212u;

		public static uint EVENT_NOTICEONLINESTATECHANGE = 1213u;

		public static uint EVENT_NOTICEINVITE = 1214u;

		public static uint EVENT_TEAMOBJECT_CHANGE = 1215u;

		public static uint EVENT_TEAM_READY = 1216u;

		public static uint EVENT_CURPAGE_TEAM = 1217u;

		public bool joinedTeam = false;

		public static uint wantedWatchTeamId;

		public static uint WatchTeamId_limited;

		public ItemTeamMemberData mapItemTeamData;

		public ItemTeamMemberData pageItemTeamData;

		public ItemTeamMemberData MyTeamData;

		private Dictionary<uint, float> InvitedDic = new Dictionary<uint, float>();

		private Dictionary<uint, float> ApplyDic = new Dictionary<uint, float>();

		public List<TeamPosition> teamlist_position = new List<TeamPosition>();

		public List<ItemTeamData> teamMemberposData = new List<ItemTeamData>();

		public uint trage_cid;

		public TeamProxy()
		{
			this.InvitedDic.Clear();
			this.addProxyListener(120u, new Action<Variant>(this.OnTeamInfo));
		}

		public void OnTeamInfo(Variant data)
		{
			debug.Log("组队信息:" + data.dump());
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				bool flag2 = num == -1309;
				if (flag2)
				{
					this.SendCreateTeam(0);
					BaseProxy<TeamProxy>.getInstance().SendInvite(this.trage_cid);
				}
				else
				{
					Globle.err_output(num);
				}
			}
			else
			{
				switch (num)
				{
				case 1:
					this.SetCreateTeam(data);
					break;
				case 2:
					this.SetMapTeam(data);
					break;
				case 3:
					this.SetWatchTeamInfo(data);
					break;
				case 4:
					this.SetSyncTeamBlood(data);
					break;
				case 5:
					this.GetTeamPos(data);
					break;
				case 6:
					this.SetApplyJoinTeam(data);
					break;
				case 8:
					this.SetLeaveTeam(data);
					break;
				case 10:
					this.SetAffirmInvite(data);
					break;
				case 11:
					this.SetKickOut(data);
					break;
				case 12:
					this.SetChangeCaptain(data);
					break;
				case 13:
					this.SetDissolve(data);
					break;
				case 14:
					this.SetChangeTeamInfo(data);
					break;
				case 15:
					this.SetTeamobject_Change(data);
					break;
				case 16:
					this.SetTeamReady(data);
					break;
				case 17:
					this.Get_curPageTeam_info(data);
					break;
				case 20:
					this.SetNoticeCaptainNewInfo(data);
					break;
				case 22:
					this.SetNoticeHaveMemberLeave(data);
					break;
				case 23:
					this.SetNoticeInvite(data);
					break;
				case 24:
					this.SetNewMemberJoin(data);
					break;
				case 25:
					this.SetNoticeInviteBeRefuse(data);
					break;
				case 26:
					this.SetNoticeOnlineStateChange(data);
					break;
				}
			}
		}

		private void Get_curPageTeam_info(Variant data)
		{
			this.pageItemTeamData = new ItemTeamMemberData();
			List<ItemTeamData> list = new List<ItemTeamData>();
			list.Clear();
			uint totalCount = data["total_cnt"];
			uint idxBegin = data["idx_begin"];
			this.pageItemTeamData.totalCount = totalCount;
			this.pageItemTeamData.idxBegin = idxBegin;
			List<Variant> arr = data["info"]._arr;
			foreach (Variant current in arr)
			{
				ItemTeamData itemTeamData = new ItemTeamData();
				itemTeamData.teamId = current["tid"];
				itemTeamData.curcnt = current["curcnt"];
				itemTeamData.maxcnt = current["maxcnt"];
				itemTeamData.name = current["lname"];
				itemTeamData.carr = current["lcarr"];
				itemTeamData.lvl = current["llevel"];
				itemTeamData.zhuan = current["lzhuan"];
				itemTeamData.mapId = current["lmapid"];
				itemTeamData.ltpid = current["ltpid"];
				itemTeamData.ldiff = current["ldiff"];
				itemTeamData.members = current["members"]._arr;
				bool flag = string.IsNullOrEmpty(current["lclname"]);
				if (flag)
				{
					itemTeamData.knightage = "无";
				}
				else
				{
					itemTeamData.knightage = current["lclname"];
				}
				list.Add(itemTeamData);
			}
			this.pageItemTeamData.itemTeamDataList = list;
			a3_SpeedTeam expr_209 = a3_SpeedTeam.instance;
			if (expr_209 != null)
			{
				expr_209.GetTeam_info(this.pageItemTeamData);
			}
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_CURPAGE_TEAM, this, list, false));
		}

		public void SendCreateTeam(int v)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 1u;
			variant["ltpid"] = v;
			variant["ldiff"] = 0;
			this.sendRPC(120u, variant);
		}

		public void sendobject_change(int v)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 15u;
			variant["ltpid"] = v;
			variant["ldiff"] = 0;
			this.sendRPC(120u, variant);
		}

		public void SendGetMapTeam(uint begin, uint end)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 2u;
			variant["idx_begin"] = begin;
			variant["idx_end"] = end;
			this.sendRPC(120u, variant);
		}

		public void SendGetPageTeam(uint ltpid, uint begin, uint end)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 17u;
			variant["ltpid"] = ltpid;
			variant["idx_begin"] = begin;
			variant["idx_end"] = end;
			this.sendRPC(120u, variant);
		}

		public void SendWatchTeamInfo(uint tid)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 3u;
			variant["tid"] = tid;
			TeamProxy.wantedWatchTeamId = tid;
			this.sendRPC(120u, variant);
		}

		public void SendSyncTeamBlood()
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 4u;
			this.sendRPC(120u, variant);
		}

		public void SendCurrentMapTeamPos()
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 5u;
			this.sendRPC(120u, variant);
		}

		public void SendApplyJoinTeam(uint tid)
		{
			bool flag = this.ApplyDic.ContainsKey(tid);
			if (flag)
			{
				bool flag2 = Time.time - this.ApplyDic[tid] > 10f;
				bool flag3 = flag2;
				if (!flag3)
				{
					flytxt.instance.fly("对同一只队伍的加入申请,每10秒只能发送一次", 0, default(Color), null);
					return;
				}
				this.ApplyDic[tid] = Time.time;
			}
			else
			{
				this.ApplyDic[tid] = Time.time;
			}
			Variant variant = new Variant();
			variant["team_cmd"] = 6u;
			variant["team_id"] = tid;
			this.sendRPC(120u, variant);
		}

		public void SendAffirmApply(uint cid, bool approved)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 7u;
			variant["cid"] = cid;
			variant["approved"] = approved;
			this.sendRPC(120u, variant);
		}

		public void SendLeaveTeam(uint tid)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 8u;
			variant["team_id"] = tid;
			this.sendRPC(120u, variant);
		}

		public void SendTEAM(uint cid)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 14;
			variant["cid"] = cid;
			this.sendRPC(170u, variant);
		}

		public void SendInvite(uint cid)
		{
			bool flag = this.MyTeamData != null;
			if (flag)
			{
				bool flag2 = this.MyTeamData.itemTeamDataList.Count == 5;
				if (flag2)
				{
					flytxt.instance.fly("队伍人数已满.", 0, default(Color), null);
				}
				else
				{
					flytxt.instance.fly("发送成功", 0, default(Color), null);
					bool flag3 = this.InvitedDic.ContainsKey(cid);
					if (flag3)
					{
						bool flag4 = Time.time - this.InvitedDic[cid] > 10f;
						bool flag5 = flag4;
						if (!flag5)
						{
							flytxt.instance.fly("10秒对同一个人的组队邀请只能发出一次", 0, default(Color), null);
							return;
						}
						this.InvitedDic[cid] = Time.time;
					}
					else
					{
						this.InvitedDic[cid] = Time.time;
					}
					debug.Log("cid" + cid);
					Variant variant = new Variant();
					variant["team_cmd"] = 9u;
					variant["cid"] = cid;
					this.sendRPC(120u, variant);
				}
			}
			else
			{
				flytxt.instance.fly("请先创建队伍", 0, default(Color), null);
			}
		}

		public void SendAffirmInvite(uint cid, uint tid, bool cofirmed)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 10u;
			variant["cid"] = cid;
			variant["tid"] = tid;
			variant["cofirmed"] = cofirmed;
			this.sendRPC(120u, variant);
		}

		public void SendKickOut(uint cid)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 11u;
			variant["cid"] = cid;
			this.sendRPC(120u, variant);
		}

		public void SendChangeCaptain(uint cid)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 12u;
			variant["cid"] = cid;
			this.sendRPC(120u, variant);
		}

		public void SendDissolve(uint teamId = 0u)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 13u;
			this.sendRPC(120u, variant);
			bool flag = teamId > 0u;
			if (flag)
			{
				Variant variant2 = new Variant();
				variant2["teamid"] = teamId;
				base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_DISSOLVETEAM, this, variant2, false));
			}
		}

		public void SendEditorInfoDirJoin(bool dirJoin)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 14u;
			variant["dir_join"] = dirJoin;
			this.sendRPC(120u, variant);
		}

		public void SendEditorInfoMembInv(bool membInv)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 14u;
			variant["memb_inv"] = membInv;
			this.sendRPC(120u, variant);
		}

		public void SendReady(bool ready, uint ltpid, uint ldiff)
		{
			Variant variant = new Variant();
			variant["team_cmd"] = 16u;
			variant["ready"] = ready;
			variant["ltpid"] = ltpid;
			variant["ldiff"] = ldiff;
			this.sendRPC(120u, variant);
		}

		private void SetCreateTeam(Variant data)
		{
			bool flag = this.MyTeamData != null;
			if (flag)
			{
				this.MyTeamData = null;
				this.MyTeamData = new ItemTeamMemberData();
			}
			else
			{
				this.MyTeamData = new ItemTeamMemberData();
			}
			this.joinedTeam = true;
			uint teamId = data["teamid"];
			ItemTeamData itemTeamData = new ItemTeamData();
			itemTeamData.name = ModelBase<PlayerModel>.getInstance().name;
			itemTeamData.lvl = ModelBase<PlayerModel>.getInstance().lvl;
			itemTeamData.knightage = ModelBase<PlayerModel>.getInstance().clanid.ToString();
			itemTeamData.mapId = ModelBase<PlayerModel>.getInstance().mapid;
			itemTeamData.MembCount = 1;
			itemTeamData.cid = ModelBase<PlayerModel>.getInstance().cid;
			itemTeamData.zhuan = ModelBase<PlayerModel>.getInstance().up_lvl;
			itemTeamData.combpt = ModelBase<PlayerModel>.getInstance().combpt;
			itemTeamData.teamId = teamId;
			itemTeamData.isCaptain = true;
			itemTeamData.showRemoveMemberBtn = false;
			itemTeamData.online = true;
			this.MyTeamData.teamId = teamId;
			this.MyTeamData.dirJoin = true;
			this.MyTeamData.membInv = false;
			this.MyTeamData.leaderCid = itemTeamData.cid;
			this.MyTeamData.meIsCaptain = true;
			this.MyTeamData.ltpid = data["ltpid"];
			this.MyTeamData.itemTeamDataList.Add(itemTeamData);
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_CREATETEAM, this, data, false));
			this.SendEditorInfoDirJoin(true);
			bool flag2 = !ModelBase<A3_TeamModel>.getInstance().cidNameElse.ContainsKey(itemTeamData.cid);
			if (flag2)
			{
				ModelBase<A3_TeamModel>.getInstance().cidNameElse.Add(itemTeamData.cid, itemTeamData.name);
			}
		}

		private void SetMapTeam(Variant data)
		{
			this.mapItemTeamData = new ItemTeamMemberData();
			List<ItemTeamData> list = new List<ItemTeamData>();
			uint totalCount = data["total_cnt"];
			uint idxBegin = data["idx_begin"];
			this.mapItemTeamData.totalCount = totalCount;
			this.mapItemTeamData.idxBegin = idxBegin;
			List<Variant> arr = data["info"]._arr;
			foreach (Variant current in arr)
			{
				ItemTeamData itemTeamData = new ItemTeamData();
				itemTeamData.teamId = current["tid"];
				itemTeamData.curcnt = current["curcnt"];
				itemTeamData.maxcnt = current["maxcnt"];
				itemTeamData.name = current["lname"];
				itemTeamData.carr = current["lcarr"];
				itemTeamData.lvl = current["llevel"];
				itemTeamData.zhuan = current["lzhuan"];
				itemTeamData.mapId = current["lmapid"];
				itemTeamData.ltpid = current["ltpid"];
				itemTeamData.ldiff = current["ldiff"];
				itemTeamData.members = current["members"]._arr;
				bool flag = string.IsNullOrEmpty(current["lclname"]);
				if (flag)
				{
					itemTeamData.knightage = "无";
				}
				else
				{
					itemTeamData.knightage = current["lclname"];
				}
				list.Add(itemTeamData);
			}
			this.mapItemTeamData.itemTeamDataList = list;
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_TEAMLISTINFO, this, this.mapItemTeamData, false));
		}

		private void SetWatchTeamInfo(Variant dt)
		{
			List<ItemTeamData> list = new List<ItemTeamData>();
			list.Clear();
			this.teamMemberposData.Clear();
			foreach (Variant current in dt["members"]._arr)
			{
				uint cid = current["cid"];
				string name = current["name"];
				uint lvl = current["lvl"];
				uint zhuan = current["zhuan"];
				uint combpt = current["combpt"];
				uint carr = current["carr"];
				uint mapId = current["mapid"];
				bool online = current["online"];
				string text = current["clname"];
				bool flag = string.IsNullOrEmpty(text);
				if (flag)
				{
					text = "无";
				}
				ItemTeamData itemTeamData = new ItemTeamData();
				itemTeamData.cid = cid;
				itemTeamData.name = name;
				itemTeamData.lvl = lvl;
				itemTeamData.zhuan = zhuan;
				itemTeamData.combpt = (int)combpt;
				itemTeamData.carr = carr;
				itemTeamData.mapId = mapId;
				itemTeamData.online = online;
				itemTeamData.knightage = text;
				list.Add(itemTeamData);
				this.teamMemberposData.Add(itemTeamData);
			}
			uint num = dt["lcid"];
			for (int i = 0; i < list.Count; i++)
			{
				bool flag2 = list[i].cid == num;
				if (flag2)
				{
					list[i].isCaptain = true;
				}
				else
				{
					list[i].isCaptain = false;
				}
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(list);
			bool flag3 = !worldmap.getmapid;
			if (flag3)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_TEAMMEMBERLIST, arrayList, false);
			}
		}

		private void SetSyncTeamBlood(Variant data)
		{
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_SYNCTEAMBLOOD, this, data, false));
		}

		private void GetTeamPos(Variant data)
		{
			this.teamlist_position.Clear();
			bool flag = data != null;
			if (flag)
			{
				List<Variant> arr = data["mempos"]._arr;
				foreach (Variant current in arr)
				{
					TeamPosition teamPosition = new TeamPosition();
					teamPosition.cid = current["cid"];
					teamPosition.x = (uint)(current["x"] / 53.3f);
					teamPosition.y = (uint)(current["y"] / 53.3f);
					this.teamlist_position.Add(teamPosition);
				}
			}
		}

		private void SetApplyJoinTeam(Variant data)
		{
			flytxt.instance.fly("申请已发送!", 0, default(Color), null);
		}

		private void SetLeaveTeam(Variant data)
		{
			this.MyTeamData = null;
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_LEAVETEAM, this, null, false));
			ModelBase<A3_TeamModel>.getInstance().cidNameElse.Clear();
			MonsterMgr._inst.HideInvaildMonster();
			flytxt.instance.fly("您已离开队伍", 0, default(Color), null);
		}

		private void SetAffirmInvite(Variant data)
		{
			ItemTeamMemberData itemTeamMemberData = new ItemTeamMemberData();
			bool flag = data.ContainsKey("cofirmed");
			if (flag)
			{
				bool cofirmed = data["cofirmed"];
				itemTeamMemberData.cofirmed = cofirmed;
			}
			bool flag2 = data.ContainsKey("tid");
			if (flag2)
			{
				uint teamId = data["tid"];
				itemTeamMemberData.teamId = teamId;
				List<ItemTeamData> list = new List<ItemTeamData>();
				List<Variant> arr = data["plys"]._arr;
				uint num = data["leader_cid"];
				bool dirJoin = data["dir_join"];
				bool membInv = data["memb_inv"];
				uint num2 = data["ltpid"];
				itemTeamMemberData.ltpid = num2;
				itemTeamMemberData.leaderCid = num;
				itemTeamMemberData.dirJoin = dirJoin;
				itemTeamMemberData.membInv = membInv;
				bool flag3 = this.MyTeamData == null;
				if (flag3)
				{
					this.MyTeamData = new ItemTeamMemberData();
				}
				this.MyTeamData.teamId = teamId;
				this.MyTeamData.leaderCid = num;
				this.MyTeamData.dirJoin = dirJoin;
				this.MyTeamData.membInv = membInv;
				this.MyTeamData.ltpid = num2;
				foreach (Variant current in arr)
				{
					uint num3 = current["cid"];
					string text = current["name"];
					uint lvl = current["lvl"];
					uint zhuan = current["zhuan"];
					uint combpt = current["combpt"];
					uint carr = current["carr"];
					bool online = current["online"];
					ItemTeamData itemTeamData = new ItemTeamData();
					itemTeamData.cid = num3;
					itemTeamData.name = text;
					itemTeamData.lvl = lvl;
					itemTeamData.zhuan = zhuan;
					itemTeamData.combpt = (int)combpt;
					itemTeamData.carr = carr;
					itemTeamData.online = online;
					itemTeamData.isCaptain = (num == num3);
					itemTeamData.showRemoveMemberBtn = false;
					list.Add(itemTeamData);
					this.MyTeamData.itemTeamDataList.Add(itemTeamData);
					bool flag4 = (num2 == 108u || num2 == 109u || num2 == 110u || num2 == 111u) && !ModelBase<A3_TeamModel>.getInstance().cidName.ContainsKey(num3);
					if (flag4)
					{
						ModelBase<A3_TeamModel>.getInstance().cidName.Add(num3, text);
					}
					bool flag5 = !ModelBase<A3_TeamModel>.getInstance().cidNameElse.ContainsKey(num3);
					if (flag5)
					{
						ModelBase<A3_TeamModel>.getInstance().cidNameElse.Add(num3, text);
					}
				}
				bool flag6 = num2 == 108u || num2 == 109u || num2 == 110u || num2 == 111u;
				if (flag6)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(1);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, arrayList, false);
					a3_counterpart expr_33B = a3_counterpart.instance;
					if (expr_33B != null)
					{
						expr_33B.transform.SetAsLastSibling();
					}
					a3_counterpart expr_351 = a3_counterpart.instance;
					if (expr_351 != null)
					{
						expr_351.getGameObjectByPath("currentTeam").SetActive(true);
					}
					ModelBase<A3_TeamModel>.getInstance().bein = true;
					ModelBase<A3_TeamModel>.getInstance().ltpids = num2;
				}
				ItemTeamData itemTeamData2 = new ItemTeamData();
				itemTeamData2.cid = ModelBase<PlayerModel>.getInstance().cid;
				itemTeamData2.name = ModelBase<PlayerModel>.getInstance().name;
				itemTeamData2.lvl = ModelBase<PlayerModel>.getInstance().lvl;
				itemTeamData2.zhuan = ModelBase<PlayerModel>.getInstance().up_lvl;
				itemTeamData2.combpt = ModelBase<PlayerModel>.getInstance().combpt;
				itemTeamData2.carr = (uint)ModelBase<PlayerModel>.getInstance().profession;
				itemTeamData2.online = true;
				itemTeamData2.isCaptain = false;
				itemTeamData2.showRemoveMemberBtn = false;
				itemTeamData2.ltpid = data["ltpid"];
				list.Add(itemTeamData2);
				itemTeamMemberData.itemTeamDataList = list;
				this.MyTeamData.itemTeamDataList.Add(itemTeamData2);
				ModelBase<A3_TeamModel>.getInstance().AffirmInviteData = itemTeamMemberData;
				base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_AFFIRMINVITE, this, data, false));
				this.joinedTeam = true;
				MonsterMgr._inst.RefreshVaildMonster();
			}
		}

		private void SetKickOut(Variant data)
		{
			uint num = data["cid"];
			int count = this.MyTeamData.itemTeamDataList.Count;
			for (int i = 0; i < count; i++)
			{
				bool flag = this.MyTeamData.itemTeamDataList[i].cid == num;
				if (flag)
				{
					this.MyTeamData.itemTeamDataList.Remove(this.MyTeamData.itemTeamDataList[i]);
					this.MyTeamData.removedIndex = (uint)i;
					base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_KICKOUT, this, data, false));
					break;
				}
			}
			bool flag2 = ModelBase<A3_TeamModel>.getInstance().cidNameElse.ContainsKey(num);
			if (flag2)
			{
				ModelBase<A3_TeamModel>.getInstance().cidNameElse.Remove(num);
			}
		}

		private void SetChangeCaptain(Variant data)
		{
			for (int i = 0; i < this.MyTeamData.itemTeamDataList.Count; i++)
			{
				bool flag = this.MyTeamData.leaderCid == this.MyTeamData.itemTeamDataList[i].cid;
				if (flag)
				{
					this.MyTeamData.itemTeamDataList[i].isCaptain = false;
					this.MyTeamData.itemTeamDataList[i].showRemoveMemberBtn = true;
					break;
				}
			}
			uint num = data["cid"];
			this.MyTeamData.leaderCid = num;
			bool flag2 = num == ModelBase<PlayerModel>.getInstance().cid;
			if (flag2)
			{
				this.MyTeamData.meIsCaptain = true;
			}
			for (int j = 0; j < this.MyTeamData.itemTeamDataList.Count; j++)
			{
				bool flag3 = this.MyTeamData.itemTeamDataList[j].cid == num;
				if (flag3)
				{
					this.MyTeamData.itemTeamDataList[j].isCaptain = true;
					bool flag4 = this.MyTeamData.meIsCaptain && this.MyTeamData.itemTeamDataList[j].cid != ModelBase<PlayerModel>.getInstance().cid;
					if (flag4)
					{
						this.MyTeamData.itemTeamDataList[j].showRemoveMemberBtn = true;
					}
					else
					{
						this.MyTeamData.itemTeamDataList[j].showRemoveMemberBtn = false;
					}
					bool flag5 = num == ModelBase<PlayerModel>.getInstance().cid;
					if (flag5)
					{
						this.MyTeamData.meIsCaptain = true;
					}
					else
					{
						this.MyTeamData.meIsCaptain = false;
					}
				}
				else
				{
					this.MyTeamData.itemTeamDataList[j].isCaptain = false;
				}
			}
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_CHANGECAPTAIN, this, null, false));
		}

		private void SetDissolve(Variant data)
		{
			ModelBase<A3_TeamModel>.getInstance().cidNameElse.Clear();
			this.joinedTeam = false;
			this.MyTeamData = null;
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_DISSOLVETEAM, this, data, false));
		}

		private void SetChangeTeamInfo(Variant data)
		{
			bool flag = data.ContainsKey("dir_join");
			if (flag)
			{
				bool dirJoin = data["dir_join"];
				this.MyTeamData.dirJoin = dirJoin;
			}
			bool flag2 = data.ContainsKey("memb_inv");
			if (flag2)
			{
				bool membInv = data["memb_inv"];
				this.MyTeamData.membInv = membInv;
			}
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_CHANGETEAMINFO, this, data, false));
		}

		private void SetNoticeCaptainNewInfo(Variant data)
		{
			uint cid = data["cid"];
			string name = data["name"];
			uint lvl = data["lvl"];
			uint zhuan = data["zhuan"];
			uint carr = data["carr"];
			uint combpt = data["combpt"];
			ItemTeamData itemTeamData = new ItemTeamData();
			itemTeamData.cid = cid;
			itemTeamData.name = name;
			itemTeamData.lvl = lvl;
			itemTeamData.zhuan = zhuan;
			itemTeamData.carr = carr;
			itemTeamData.combpt = (int)combpt;
			itemTeamData.showRemoveMemberBtn = true;
			bool flag = !ModelBase<A3_TeamModel>.getInstance().cidNameElse.ContainsKey(itemTeamData.cid);
			if (flag)
			{
				ModelBase<A3_TeamModel>.getInstance().cidNameElse.Add(itemTeamData.cid, itemTeamData.name);
			}
			a3_teamApplyPanel.mInstance.Show(itemTeamData);
		}

		private void SetTeamReady(Variant data)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, null, false);
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_TEAM_READY, this, data, false));
		}

		private void SetNoticeHaveMemberLeave(Variant data)
		{
			uint num = data["cid"];
			int count = this.MyTeamData.itemTeamDataList.Count;
			for (int i = 0; i < count; i++)
			{
				bool flag = this.MyTeamData.itemTeamDataList[i].cid == num;
				if (flag)
				{
					this.MyTeamData.itemTeamDataList.Remove(this.MyTeamData.itemTeamDataList[i]);
					this.MyTeamData.removedIndex = (uint)i;
					base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_NOTICEHAVEMEMBERLEAVE, this, data, false));
					break;
				}
			}
			bool flag2 = ModelBase<A3_TeamModel>.getInstance().cidNameElse.ContainsKey(num);
			if (flag2)
			{
				ModelBase<A3_TeamModel>.getInstance().cidNameElse.Remove(num);
			}
		}

		private void SetNoticeInvite(Variant data)
		{
			bool rEFUSE_TEAM_INVITE = GlobleSetting.REFUSE_TEAM_INVITE;
			if (!rEFUSE_TEAM_INVITE)
			{
				uint teamId = data["tid"];
				uint cid = data["cid"];
				string name = data["name"];
				uint lvl = data["lvl"];
				uint zhuan = data["zhuan"];
				uint carr = data["carr"];
				uint combpt = data["combpt"];
				ItemTeamData itemTeamData = new ItemTeamData();
				itemTeamData.teamId = teamId;
				itemTeamData.cid = cid;
				itemTeamData.name = name;
				itemTeamData.lvl = lvl;
				itemTeamData.zhuan = zhuan;
				itemTeamData.carr = carr;
				itemTeamData.combpt = (int)combpt;
				bool flag = this.MyTeamData == null;
				if (flag)
				{
					base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_NOTICEINVITE, this, data, false));
				}
			}
		}

		private void SetNewMemberJoin(Variant data)
		{
			uint num = data["cid"];
			string name = data["name"];
			uint lvl = data["lvl"];
			uint zhuan = data["zhuan"];
			uint carr = data["carr"];
			uint combpt = data["combpt"];
			ItemTeamData itemTeamData = new ItemTeamData();
			itemTeamData.cid = num;
			itemTeamData.name = name;
			itemTeamData.lvl = lvl;
			itemTeamData.zhuan = zhuan;
			itemTeamData.carr = carr;
			itemTeamData.combpt = (int)combpt;
			itemTeamData.isCaptain = false;
			itemTeamData.online = true;
			bool meIsCaptain = this.MyTeamData.meIsCaptain;
			if (meIsCaptain)
			{
				itemTeamData.showRemoveMemberBtn = true;
			}
			else
			{
				itemTeamData.showRemoveMemberBtn = false;
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(itemTeamData);
			bool flag = true;
			for (int i = 0; i < this.MyTeamData.itemTeamDataList.Count; i++)
			{
				bool flag2 = this.MyTeamData.itemTeamDataList[i].cid == num;
				if (flag2)
				{
					flag = false;
					this.MyTeamData.itemTeamDataList[i].online = true;
					base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_NOTICEONLINESTATECHANGE, this, data, false));
					break;
				}
			}
			bool flag3 = flag;
			if (flag3)
			{
				this.MyTeamData.itemTeamDataList.Add(itemTeamData);
				flytxt.instance.fly(itemTeamData.name + "加入队伍", 0, default(Color), null);
				base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_NEWMEMBERJOIN, this, data, false));
			}
			bool flag4 = !ModelBase<A3_TeamModel>.getInstance().cidNameElse.ContainsKey(itemTeamData.cid);
			if (flag4)
			{
				ModelBase<A3_TeamModel>.getInstance().cidNameElse.Add(itemTeamData.cid, itemTeamData.name);
			}
		}

		private void SetNoticeInviteBeRefuse(Variant data)
		{
			uint num = data["cid"];
			string text = data["name"];
		}

		private void SetTeamobject_Change(Variant data)
		{
			uint num = data["ltpid"];
			bool flag = num == 108u || num == 109u || num == 110u || num == 111u;
			if (flag)
			{
				ModelBase<A3_TeamModel>.getInstance().cidName = ModelBase<A3_TeamModel>.getInstance().cidNameElse;
				ArrayList arrayList = new ArrayList();
				arrayList.Add(1);
				BaseProxy<TeamProxy>.getInstance().MyTeamData.ltpid = num;
				ModelBase<A3_TeamModel>.getInstance().bein = true;
				ModelBase<A3_TeamModel>.getInstance().ltpids = num;
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, arrayList, false);
				a3_counterpart expr_95 = a3_counterpart.instance;
				if (expr_95 != null)
				{
					expr_95.transform.SetAsLastSibling();
				}
				a3_counterpart expr_AB = a3_counterpart.instance;
				if (expr_AB != null)
				{
					expr_AB.changePos();
				}
				a3_counterpart expr_BC = a3_counterpart.instance;
				if (expr_BC != null)
				{
					expr_BC.getGameObjectByPath("currentTeam").SetActive(true);
				}
			}
			else
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_COUNTERPART);
				a3_counterpart expr_EC = a3_counterpart.instance;
				if (expr_EC != null)
				{
					expr_EC.getGameObjectByPath("currentTeam").SetActive(false);
				}
				a3_counterpart expr_108 = a3_counterpart.instance;
				if (expr_108 != null)
				{
					expr_108.getGameObjectByPath("haoyou").SetActive(false);
				}
				a3_counterpart expr_124 = a3_counterpart.instance;
				if (expr_124 != null)
				{
					expr_124.getGameObjectByPath("yaoqing").SetActive(false);
				}
			}
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_TEAMOBJECT_CHANGE, this, data, false));
		}

		private void SetNoticeOnlineStateChange(Variant data)
		{
			uint num = data["cid"];
			bool flag = data["online"];
			int count = this.MyTeamData.itemTeamDataList.Count;
			int num2 = -1;
			for (int i = 0; i < count; i++)
			{
				bool flag2 = !this.MyTeamData.itemTeamDataList[i].online && num2 == -1;
				if (flag2)
				{
					num2 = i;
				}
				bool flag3 = this.MyTeamData.itemTeamDataList[i].cid == num;
				if (flag3)
				{
					bool flag4 = flag;
					if (flag4)
					{
						ItemTeamData itemTeamData = this.MyTeamData.itemTeamDataList[i];
						this.MyTeamData.itemTeamDataList.RemoveAt(i);
						itemTeamData.online = flag;
						this.MyTeamData.itemTeamDataList.Insert(num2, itemTeamData);
					}
					else
					{
						ItemTeamData itemTeamData2 = this.MyTeamData.itemTeamDataList[i];
						this.MyTeamData.itemTeamDataList.RemoveAt(i);
						itemTeamData2.online = flag;
						this.MyTeamData.itemTeamDataList.Add(itemTeamData2);
					}
					break;
				}
			}
			base.dispatchEvent(GameEvent.Create(TeamProxy.EVENT_NOTICEONLINESTATECHANGE, this, data, false));
		}

		public void SetTeamPanelInfo()
		{
			bool flag = this.MyTeamData != null;
			if (flag)
			{
				a3_currentTeamPanel._instance.gameObject.SetActive(true);
				a3_teamPanel._instance.gameObject.SetActive(false);
				a3_currentTeamPanel._instance.Show(this.MyTeamData);
			}
			else
			{
				a3_teamPanel._instance.gameObject.SetActive(true);
				a3_currentTeamPanel._instance.gameObject.SetActive(false);
			}
		}
	}
}
