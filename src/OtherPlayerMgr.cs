using Cross;
using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerMgr
{
	public static OtherPlayerMgr _inst = new OtherPlayerMgr();

	public Dictionary<uint, ProfessionRole> m_mapOtherPlayer = new Dictionary<uint, ProfessionRole>();

	public Dictionary<uint, ProfessionRole> m_mapOtherPlayerSee = new Dictionary<uint, ProfessionRole>();

	public Dictionary<uint, ProfessionRole> m_mapOtherPlayerUnsee = new Dictionary<uint, ProfessionRole>();

	public List<Variant> cacheProxy = new List<Variant>();

	private int _VIEW_PLAYER_TYPE = 1;

	public int VIEW_PLAYER_TYPE
	{
		get
		{
			return this._VIEW_PLAYER_TYPE;
		}
		set
		{
			bool flag = this._VIEW_PLAYER_TYPE == value;
			if (!flag)
			{
				this._VIEW_PLAYER_TYPE = value;
				bool flag2 = this._VIEW_PLAYER_TYPE == 0;
				if (flag2)
				{
					foreach (ProfessionRole current in this.m_mapOtherPlayerSee.Values)
					{
						bool flag3 = !GRMap.loading;
						if (flag3)
						{
							current.refreshViewType(1);
						}
						else
						{
							current.refreshViewType(0);
						}
						this.m_mapOtherPlayerUnsee.Add(current.m_unIID, current);
					}
					this.m_mapOtherPlayerSee.Clear();
				}
			}
		}
	}

	public void AddOtherPlayer(Variant p)
	{
		bool loading = GRMap.loading;
		if (loading)
		{
			this.cacheProxy.Add(p);
		}
		else
		{
			uint @uint = p["iid"]._uint;
			bool flag = this.m_mapOtherPlayer.ContainsKey(@uint);
			if (!flag)
			{
				uint num = 0u;
				int num2 = p["carr"];
				bool flag2 = p.ContainsKey("cid");
				if (flag2)
				{
					num = p["cid"]._uint;
				}
				string name = p["name"];
				Vector3 vector = new Vector3(p["x"] / 53.333f, 0f, p["y"] / 53.333f);
				bool flag3 = 2 == num2;
				ProfessionRole professionRole;
				if (flag3)
				{
					professionRole = new P2Warrior();
					(professionRole as P2Warrior).Init(name, EnumLayer.LM_OTHERPLAYER, vector, false);
				}
				else
				{
					bool flag4 = 3 == num2;
					if (flag4)
					{
						professionRole = new P3Mage();
						(professionRole as P3Mage).Init(name, EnumLayer.LM_OTHERPLAYER, vector, false);
					}
					else
					{
						bool flag5 = 5 == num2;
						if (flag5)
						{
							professionRole = new P5Assassin();
							(professionRole as P5Assassin).Init(name, EnumLayer.LM_OTHERPLAYER, vector, false);
						}
						else
						{
							professionRole = new P3Mage();
							(professionRole as P3Mage).Init(name, EnumLayer.LM_OTHERPLAYER, vector, false);
						}
					}
				}
				bool flag6 = GameRoomMgr.getInstance().curRoom == GameRoomMgr.getInstance().dRooms[3342u];
				if (flag6)
				{
					professionRole.setNavLay(NavmeshUtils.allARE);
				}
				professionRole.m_curModel.position = vector;
				bool flag7 = p.ContainsKey("face");
				if (flag7)
				{
					professionRole.m_curModel.rotation = new Quaternion(professionRole.m_curModel.rotation.x, p["face"], professionRole.m_curModel.rotation.z, 0f);
				}
				bool flag8 = professionRole == null;
				if (!flag8)
				{
					this.m_mapOtherPlayer.Add(@uint, professionRole);
					this.m_mapOtherPlayerUnsee.Add(@uint, professionRole);
					professionRole.setPos(vector);
					professionRole.SetDestPos(vector);
					professionRole.m_unIID = @uint;
					professionRole.m_unCID = num;
					professionRole.m_unPK_Param = num;
					professionRole.maxHp = p["battleAttrs"]["max_hp"];
					professionRole.curhp = p["hp"];
					bool flag9 = p.ContainsKey("zhuan");
					if (flag9)
					{
						professionRole.zhuan = p["zhuan"];
					}
					bool flag10 = p.ContainsKey("pk_state");
					if (flag10)
					{
						professionRole.Pk_state = p["pk_state"];
					}
					switch (professionRole.Pk_state)
					{
					case 0:
						professionRole.m_ePK_Type = PK_TYPE.PK_PEACE;
						break;
					case 1:
						professionRole.m_ePK_Type = PK_TYPE.PK_PKALL;
						break;
					case 2:
						professionRole.m_ePK_Type = PK_TYPE.PK_TEAM;
						break;
					case 3:
						professionRole.m_ePK_Type = PK_TYPE.PK_LEGION;
						break;
					case 4:
						professionRole.m_ePK_Type = PK_TYPE.PK_PEACE;
						break;
					}
					bool flag11 = p.ContainsKey("teamid");
					if (flag11)
					{
						professionRole.m_unTeamID = p["teamid"];
					}
					bool flag12 = p.ContainsKey("clanid");
					if (flag12)
					{
						professionRole.m_unLegionID = p["clanid"];
					}
					bool flag13 = p.ContainsKey("pet");
					if (flag13)
					{
						professionRole.ChangePetAvatar(p["pet"]["id"], 0);
					}
					bool flag14 = p.ContainsKey("treasure_num");
					if (flag14)
					{
						professionRole.mapCount = p["treasure_num"];
						debug.Log("对方宝图" + p["treasure_num"]);
						professionRole.refreshmapCount(p["treasure_num"]);
					}
					bool flag15 = p.ContainsKey("serial_kp");
					if (flag15)
					{
						professionRole.serial = p["serial_kp"];
						professionRole.refreshserialCount(p["serial_kp"]);
					}
					bool flag16 = p.ContainsKey("vip");
					if (flag16)
					{
						professionRole.refreshVipLvl(p["vip"]);
					}
					bool flag17 = !GRMap.loading;
					if (flag17)
					{
						professionRole.refreshViewType(1);
					}
				}
			}
		}
	}

	public void refreshPlayerInfo(Variant v)
	{
		uint key = v["iid"];
		bool flag = this.m_mapOtherPlayer.ContainsKey(key);
		if (flag)
		{
			this.m_mapOtherPlayer[key].refreshViewData(v);
		}
	}

	public ProfessionRole GetOtherPlayer(uint iid)
	{
		bool flag = !this.m_mapOtherPlayer.ContainsKey(iid);
		ProfessionRole result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = this.m_mapOtherPlayer[iid];
		}
		return result;
	}

	public void clear()
	{
		foreach (ProfessionRole current in this.m_mapOtherPlayer.Values)
		{
			current.dispose();
		}
		this.m_mapOtherPlayer.Clear();
		this.m_mapOtherPlayerSee.Clear();
		this.m_mapOtherPlayerUnsee.Clear();
	}

	public void RemoveOtherPlayer(uint iid)
	{
		bool flag = !this.m_mapOtherPlayer.ContainsKey(iid);
		if (!flag)
		{
			ProfessionRole professionRole = this.m_mapOtherPlayer[iid];
			bool flag2 = this.m_mapOtherPlayerUnsee.ContainsKey(iid);
			if (flag2)
			{
				this.m_mapOtherPlayerUnsee.Remove(iid);
			}
			bool flag3 = this.m_mapOtherPlayerSee.ContainsKey(iid);
			if (flag3)
			{
				this.m_mapOtherPlayerSee.Remove(iid);
			}
			BaseProxy<FriendProxy>.getInstance().removeNearyByLeave(professionRole.m_unCID);
			this.m_mapOtherPlayer.Remove(iid);
			bool flag4 = a3_liteMinimap.instance != null;
			if (flag4)
			{
				a3_liteMinimap.instance.removeRoleInMiniMap(professionRole.strIID);
			}
			professionRole.dispose();
			bool flag5 = SelfRole._inst.m_LockRole != null && SelfRole._inst.m_LockRole.m_unIID == iid;
			if (flag5)
			{
				SelfRole._inst.m_LockRole = null;
			}
		}
	}

	public ProfessionRole FindWhoPhy(Transform phy)
	{
		ProfessionRole result;
		foreach (ProfessionRole current in this.m_mapOtherPlayer.Values)
		{
			bool flag = current.isDead || current.invisible;
			if (!flag)
			{
				bool flag2 = current.m_curPhy == phy;
				if (flag2)
				{
					result = current;
					return result;
				}
			}
		}
		result = null;
		return result;
	}

	public void onMapLoaded()
	{
		foreach (ProfessionRole current in this.m_mapOtherPlayer.Values)
		{
			current.refreshViewType(1);
		}
		foreach (Variant current2 in this.cacheProxy)
		{
			this.AddOtherPlayer(current2);
		}
		this.cacheProxy.Clear();
	}

	public BaseRole FindNearestEnemyOne(Vector3 pos, bool isredname = false, Func<ProfessionRole, bool> selector = null, bool useMark = false, PK_TYPE pkState = PK_TYPE.PK_PEACE)
	{
		float num = 9999999f;
		BaseRole baseRole = null;
		switch (pkState)
		{
		case PK_TYPE.PK_PEACE:
		case PK_TYPE.PK_PKALL:
		{
			IL_2F:
			Func<BaseRole, bool> filterHandle = (BaseRole p) => !p.isDead && (p.m_curPhy.position - pos).magnitude < (SelfRole.fsm.Autofighting ? StateInit.Instance.Distance : SelfRole._inst.m_LockDis);
			goto IL_4D;
		}
		case PK_TYPE.PK_TEAM:
		{
			Func<BaseRole, bool> filterHandle = delegate(BaseRole p)
			{
				bool arg_C1_0;
				if (!p.isDead && (p.m_curPhy.position - pos).magnitude < (SelfRole.fsm.Autofighting ? StateInit.Instance.Distance : SelfRole._inst.m_LockDis))
				{
					ItemTeamMemberData expr_6E = BaseProxy<TeamProxy>.getInstance().MyTeamData;
					bool? arg_AA_0;
					if (expr_6E == null)
					{
						arg_AA_0 = null;
					}
					else
					{
						List<ItemTeamData> expr_82 = expr_6E.itemTeamDataList;
						arg_AA_0 = ((expr_82 != null) ? new bool?(!expr_82.Exists((ItemTeamData m) => m.cid == p.m_unCID)) : null);
					}
					arg_C1_0 = (arg_AA_0 ?? true);
				}
				else
				{
					arg_C1_0 = false;
				}
				return arg_C1_0;
			};
			goto IL_4D;
		}
		}
		goto IL_2F;
		IL_4D:
		bool flag = false;
		while (true)
		{
			foreach (ProfessionRole current in this.m_mapOtherPlayer.Values)
			{
				bool flag2 = OtherPlayerMgr._inst.m_mapOtherPlayer[current.m_unIID].zhuan >= 1;
				if (flag2)
				{
					bool flag3 = current.isDead || current.invisible;
					if (!flag3)
					{
						bool isMarked = current.m_isMarked;
						if (!isMarked)
						{
							if (isredname)
							{
								bool flag4 = current.rednm <= 0;
								if (flag4)
								{
									continue;
								}
							}
							bool flag5 = selector != null && selector(current);
							if (!flag5)
							{
								float magnitude = (current.m_curModel.position - pos).magnitude;
								bool flag6 = magnitude < (SelfRole.fsm.Autofighting ? StateInit.Instance.Distance : SelfRole._inst.m_LockDis) && magnitude < num;
								if (flag6)
								{
									num = magnitude;
									baseRole = current;
								}
							}
						}
					}
				}
			}
			bool flag7 = baseRole == null;
			if (!flag7)
			{
				break;
			}
			baseRole = MonsterMgr._inst.FindNearestSummon(pos);
			bool flag8 = baseRole == null;
			if (!flag8)
			{
				goto IL_1C9;
			}
			Func<BaseRole, bool> filterHandle;
			RoleMgr.ClearMark(!useMark, pkState, filterHandle);
			bool flag9 = !flag;
			if (!flag9)
			{
				break;
			}
			flag = true;
		}
		goto IL_1D9;
		IL_1C9:
		if (useMark)
		{
			baseRole.m_isMarked = true;
		}
		IL_1D9:
		bool flag10 = baseRole != null & useMark;
		if (flag10)
		{
			baseRole.m_isMarked = true;
		}
		return baseRole;
	}

	public void FrameMove(float fdt)
	{
		foreach (ProfessionRole current in this.m_mapOtherPlayer.Values)
		{
			current.FrameMove(fdt);
		}
		bool flag = this.VIEW_PLAYER_TYPE == 0;
		if (!flag)
		{
			bool loading = GRMap.loading;
			if (!loading)
			{
				using (Dictionary<uint, ProfessionRole>.ValueCollection.Enumerator enumerator2 = this.m_mapOtherPlayerUnsee.Values.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						ProfessionRole current2 = enumerator2.Current;
						this.m_mapOtherPlayerUnsee.Remove(current2.m_unIID);
						this.m_mapOtherPlayerSee.Add(current2.m_unIID, current2);
						BaseProxy<FriendProxy>.getInstance().addNearyByPeople(current2.m_unIID);
						BaseProxy<PlayerInfoProxy>.getInstance().sendLoadPlayerDetailInfo(current2.m_unCID);
					}
				}
			}
		}
	}

	public void PlayPetAvatarChange(uint iid, int petid, int stage)
	{
		ProfessionRole professionRole = null;
		this.m_mapOtherPlayer.TryGetValue(iid, out professionRole);
		bool flag = professionRole == null;
		if (!flag)
		{
			professionRole.ChangePetAvatar(petid, 0);
		}
	}

	public void otherPlayPetAvatarChange(uint iid, int petid, int stage)
	{
		ProfessionRole professionRole = null;
		this.m_mapOtherPlayer.TryGetValue(iid, out professionRole);
		bool flag = professionRole == null;
		if (!flag)
		{
			professionRole.ChangePetAvatar(petid, 0);
		}
	}

	public void playerMapCountChange(uint iid, int count)
	{
		ProfessionRole professionRole = null;
		this.m_mapOtherPlayer.TryGetValue(iid, out professionRole);
		bool flag = professionRole == null;
		if (!flag)
		{
			professionRole.mapCount = count;
			debug.Log("对方宝图" + count + "个");
			professionRole.refreshmapCount(count);
		}
	}

	public void otherserial_pk(uint iid, int count)
	{
		ProfessionRole professionRole = null;
		this.m_mapOtherPlayer.TryGetValue(iid, out professionRole);
		bool flag = professionRole == null;
		if (!flag)
		{
			professionRole.serial = count;
			professionRole.refreshserialCount(count);
		}
	}
}
