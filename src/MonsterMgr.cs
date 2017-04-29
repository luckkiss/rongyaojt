using Cross;
using GameFramework;
using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMgr : GameEventDispatcher
{
	public delegate Type typeDelegate(string str);

	public static uint EVENT_MONSTER_ADD = 1u;

	public static uint EVENT_MONSTER_REMOVED = 2u;

	public static uint EVENT_ROLE_BORN = 3u;

	public static MonsterMgr _inst = new MonsterMgr();

	public TaskMonId taskMonId;

	public List<Variant> cacheProxy = new List<Variant>();

	public List<Variant> cacheProxy_pvp = new List<Variant>();

	public List<MonsterRole> m_listMonster = new List<MonsterRole>();

	public Dictionary<uint, MonsterRole> m_mapMonster = new Dictionary<uint, MonsterRole>();

	public Dictionary<uint, MonsterRole> m_mapFakeMonster = new Dictionary<uint, MonsterRole>();

	private uint idIdx = 1u;

	public static MonsterMgr.typeDelegate getTypeHandle;

	private Dictionary<int, SXML> dMon;

	public Dictionary<string, MonEffData> dMonEff;

	private List<MonsterRole> need_remove_list = new List<MonsterRole>();

	public void init()
	{
		bool flag = this.dMon != null;
		if (!flag)
		{
			this.dMon = new Dictionary<int, SXML>();
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("monsters.monsters", "");
			foreach (SXML current in sXMLList)
			{
				this.dMon[current.getInt("id")] = current;
			}
			bool flag2 = this.dMonEff != null;
			if (!flag2)
			{
				this.dMonEff = new Dictionary<string, MonEffData>();
				List<SXML> sXMLList2 = XMLMgr.instance.GetSXMLList("effect.eff", "");
				foreach (SXML current2 in sXMLList2)
				{
					MonEffData monEffData = default(MonEffData);
					monEffData.id = current2.getString("id");
					monEffData.romote = (current2.getInt("romote") == 1);
					monEffData.Lockpos = (current2.getInt("romote") == 2);
					monEffData.file = current2.getString("file");
					monEffData.y = current2.getFloat("y");
					monEffData.f = current2.getFloat("f");
					monEffData.sound = current2.getString("sound");
					monEffData.rotation = current2.getFloat("rotation");
					bool flag3 = current2.getFloat("speed") == -1f;
					if (flag3)
					{
						monEffData.speed = 1f;
					}
					else
					{
						monEffData.speed = current2.getFloat("speed");
					}
					this.dMonEff[monEffData.id] = monEffData;
				}
			}
		}
	}

	public MonsterRole AddMonster(Variant m, bool invisible = true)
	{
		bool loading = GRMap.loading;
		MonsterRole result;
		if (loading)
		{
			this.cacheProxy.Add(m);
			result = null;
		}
		else
		{
			int boset_num = 0;
			bool flag = m.ContainsKey("boset_num");
			if (flag)
			{
				boset_num = m["boset_num"];
			}
			Vector3 pos = new Vector3(m["x"] / 53.333f, 0f, m["y"] / 53.333f);
			string name = null;
			bool flag2 = m.ContainsKey("owner_name");
			if (flag2)
			{
				name = m["owner_name"];
			}
			MonsterRole monsterRole = MonsterMgr._inst.AddMonster(m["mid"], pos, m["iid"], m["face"], boset_num, 0, name);
			bool flag3 = monsterRole != null;
			if (flag3)
			{
				monsterRole.curhp = m["hp"];
				monsterRole.maxHp = m["battleAttrs"]["max_hp"];
				monsterRole.m_curModel.parent.gameObject.SetActive(invisible & monsterRole.m_curModel.parent.gameObject.activeSelf);
			}
			bool flag4 = m.ContainsKey("sprite_flag");
			if (flag4)
			{
				uint num = m["sprite_flag"];
				uint iid = m["iid"];
				MonsterRole monster = MonsterMgr._inst.getMonster(iid);
				bool flag5 = monster != null;
				if (flag5)
				{
					Transform transform = monster.m_curModel.FindChild("body");
					bool flag6 = transform != null;
					if (flag6)
					{
						SkinnedMeshRenderer component = transform.GetComponent<SkinnedMeshRenderer>();
						bool flag7 = component != null;
						if (flag7)
						{
							uint num2 = num;
							if (num2 != 0u)
							{
								if (num2 != 1u)
								{
								}
							}
							else
							{
								Material[] sharedMaterials = component.sharedMaterials;
								for (int i = 0; i < sharedMaterials.Length; i++)
								{
									Material material = sharedMaterials[i];
								}
							}
						}
					}
				}
			}
			bool flag8 = m.ContainsKey("moving");
			if (flag8)
			{
				uint @uint = m["iid"]._uint;
				MonsterRole monster2 = MonsterMgr._inst.getMonster(@uint);
				float @float = m["moving"]["to_x"]._float;
				float float2 = m["moving"]["to_y"]._float;
				Vector3 sourcePosition = new Vector3(@float * 32f / 53.333f, 0f, float2 * 32f / 53.333f);
				NavMeshHit navMeshHit;
				NavMesh.SamplePosition(sourcePosition, out navMeshHit, 100f, monster2.m_layer);
				monster2.SetDestPos(navMeshHit.position);
			}
			result = monsterRole;
		}
		return result;
	}

	public void AddMonster_PVP(Variant p)
	{
		this.init();
		bool loading = GRMap.loading;
		if (loading)
		{
			this.cacheProxy.Add(p);
		}
		else
		{
			uint @uint = p["iid"]._uint;
			uint num = 0u;
			int num2 = p["carr"];
			bool flag = p.ContainsKey("cid");
			if (flag)
			{
				num = p["cid"]._uint;
			}
			string name = p["name"];
			Vector3 vector = new Vector3(p["x"] / 53.333f, 0f, p["y"] / 53.333f);
			MonsterPlayer monsterPlayer = null;
			bool flag2 = 2 == num2;
			if (flag2)
			{
				monsterPlayer = new ohterP2Warrior();
				(monsterPlayer as ohterP2Warrior).Init(name, EnumLayer.LM_OTHERPLAYER, vector, false);
			}
			else
			{
				bool flag3 = 3 == num2;
				if (flag3)
				{
					monsterPlayer = new ohterP3Mage();
					(monsterPlayer as ohterP3Mage).Init(name, EnumLayer.LM_OTHERPLAYER, vector, false);
				}
				else
				{
					bool flag4 = 5 == num2;
					if (flag4)
					{
						monsterPlayer = new ohterP5Assassin();
						(monsterPlayer as ohterP5Assassin).Init(name, EnumLayer.LM_OTHERPLAYER, vector, false);
					}
				}
			}
			monsterPlayer.m_curModel.position = vector;
			bool flag5 = p.ContainsKey("face");
			if (flag5)
			{
				monsterPlayer.m_curModel.rotation = new Quaternion(monsterPlayer.m_curModel.rotation.x, p["face"], monsterPlayer.m_curModel.rotation.z, 0f);
			}
			bool flag6 = monsterPlayer == null;
			if (!flag6)
			{
				monsterPlayer.setPos(vector);
				monsterPlayer.SetDestPos(vector);
				monsterPlayer.m_unIID = @uint;
				monsterPlayer.m_unCID = num;
				monsterPlayer.m_unPK_Param = num;
				monsterPlayer.maxHp = p["battleAttrs"]["max_hp"];
				monsterPlayer.curhp = p["hp"];
				bool flag7 = p.ContainsKey("zhuan");
				if (flag7)
				{
					monsterPlayer.zhuan = p["zhuan"];
				}
				bool flag8 = p.ContainsKey("pk_state");
				if (flag8)
				{
					monsterPlayer.Pk_state = p["pk_state"];
				}
				switch (monsterPlayer.Pk_state)
				{
				case 0:
					monsterPlayer.m_ePK_Type = PK_TYPE.PK_PEACE;
					break;
				case 1:
					monsterPlayer.m_ePK_Type = PK_TYPE.PK_PKALL;
					break;
				case 2:
					monsterPlayer.m_ePK_Type = PK_TYPE.PK_TEAM;
					break;
				case 3:
					monsterPlayer.m_ePK_Type = PK_TYPE.PK_LEGION;
					break;
				case 4:
					monsterPlayer.m_ePK_Type = PK_TYPE.PK_PEACE;
					break;
				}
				bool flag9 = p.ContainsKey("teamid");
				if (flag9)
				{
					monsterPlayer.m_unTeamID = p["teamid"];
				}
				bool flag10 = p.ContainsKey("clanid");
				if (flag10)
				{
					monsterPlayer.m_unLegionID = p["clanid"];
				}
				monsterPlayer.creatPetAvatar(num2);
				monsterPlayer.roleName = p["name"];
				bool flag11 = !GRMap.loading;
				if (flag11)
				{
					monsterPlayer.refreshViewType(1);
				}
				bool flag12 = @uint > 0u;
				if (flag12)
				{
					monsterPlayer.m_unIID = @uint;
					this.m_mapMonster.Add(@uint, monsterPlayer);
				}
				else
				{
					monsterPlayer.isfake = true;
					monsterPlayer.m_unIID = this.idIdx;
					this.m_mapFakeMonster.Add(this.idIdx, monsterPlayer);
					this.idIdx += 1u;
				}
				monsterPlayer.refreshViewData1(p);
			}
		}
	}

	public MonsterRole AddSummon(Variant m)
	{
		this.init();
		bool loading = GRMap.loading;
		MonsterRole result;
		if (loading)
		{
			this.cacheProxy.Add(m);
			result = null;
		}
		else
		{
			Vector3 pos = new Vector3(m["x"] / 53.333f, 0f, m["y"] / 53.333f);
			int num = m["mid"];
			uint num2 = m["iid"];
			bool flag = this.m_mapMonster.ContainsKey(num2);
			if (flag)
			{
				this.RemoveMonster(num2);
			}
			SXML sXML = this.dMon[num];
			int num3 = sXML.getInt("obj");
			float @float = sXML.getFloat("scale");
			string @string = sXML.getString("name");
			bool flag2 = num2 <= 0u;
			if (flag2)
			{
				bool flag3 = Globle.m_nTestMonsterID > 0;
				if (flag3)
				{
					num3 = Globle.m_nTestMonsterID;
				}
			}
			MS0000 mS = new MS0000();
			mS.tempXMl = sXML;
			mS.isBoos = (sXML.getInt("boss") == 1);
			bool flag4 = @float > 0f;
			if (flag4)
			{
				mS.scale = @float;
			}
			bool flag5 = mS != null;
			if (flag5)
			{
				mS.Init("monster/" + num3, EnumLayer.LM_MONSTER, pos, 0f);
				PlayerNameUIMgr.getInstance().show(mS);
				PlayerNameUIMgr.getInstance().setName(mS, @string, m["owner_name"] + "的小伙伴");
				mS.roleName = @string;
				mS.monsterid = num;
				bool flag6 = num2 > 0u;
				if (flag6)
				{
					mS.m_unIID = num2;
					this.m_mapMonster.Add(num2, mS);
				}
				else
				{
					mS.isfake = true;
					mS.m_unIID = this.idIdx;
					this.m_mapFakeMonster.Add(this.idIdx, mS);
					this.idIdx += 1u;
				}
				bool flag7 = !GRMap.loading;
				if (flag7)
				{
					mS.refreshViewType(2);
				}
				mS.issummon = true;
				mS.summonid = num;
				mS.masterid = m["owner_cid"];
				bool flag8 = mS.masterid == ModelBase<PlayerModel>.getInstance().cid;
				if (flag8)
				{
					bool flag9 = a3_herohead.instance;
					if (flag9)
					{
						ModelBase<A3_SummonModel>.getInstance().lastatkID = 0u;
						a3_herohead.instance.refresh_sumHp(m["hp"], m["battleAttrs"]["max_hp"]);
						a3_herohead.instance.refresh_sumbar();
						a3_herohead.instance.do_sum_CD = false;
					}
				}
			}
			this.m_listMonster.Add(mS);
			bool flag10 = mS != null;
			if (flag10)
			{
				base.dispatchEvent(GameEvent.Create(MonsterMgr.EVENT_MONSTER_ADD, this, mS, false));
			}
			bool flag11 = mS != null;
			if (flag11)
			{
				mS.curhp = m["hp"];
				mS.maxHp = m["battleAttrs"]["max_hp"];
				mS.owner_cid = m["owner_cid"];
			}
			result = mS;
		}
		return result;
	}

	public MonsterRole AddDartCar(Variant d)
	{
		this.init();
		bool loading = GRMap.loading;
		MonsterRole result;
		if (loading)
		{
			this.cacheProxy.Add(d);
			result = null;
		}
		else
		{
			Vector3 pos = new Vector3(d["x"] / 53.333f, 0f, d["y"] / 53.333f);
			int num = d["mid"];
			uint num2 = d["iid"];
			bool flag = this.m_mapMonster.ContainsKey(num2);
			if (flag)
			{
				this.RemoveMonster(num2);
			}
			SXML sXML = this.dMon[num];
			int num3 = sXML.getInt("obj");
			float @float = sXML.getFloat("scale");
			string empty = string.Empty;
			bool flag2 = num2 <= 0u;
			if (flag2)
			{
				bool flag3 = Globle.m_nTestMonsterID > 0;
				if (flag3)
				{
					num3 = Globle.m_nTestMonsterID;
				}
			}
			MDC000 mDC = new MDC000();
			mDC.tempXMl = sXML;
			mDC.isBoos = (sXML.getInt("boss") == 1);
			bool flag4 = @float > 0f;
			if (flag4)
			{
				mDC.scale = @float;
			}
			bool flag5 = mDC != null;
			if (flag5)
			{
				mDC.Init("monster/" + num3, EnumLayer.LM_MONSTER, pos, 0f);
				mDC.curhp = d["hp"];
				mDC.maxHp = d["battleAttrs"]["max_hp"];
				mDC.escort_name = d["escort_name"];
				PlayerNameUIMgr.getInstance().show(mDC);
				PlayerNameUIMgr.getInstance().setDartName(mDC, d["escort_name"] + "军团的镖车");
				mDC.roleName = d["escort_name"] + "军团的镖车";
				mDC.monsterid = num;
				bool flag6 = mDC.roleName == ModelBase<A3_LegionModel>.getInstance().myLegion.name;
				if (flag6)
				{
					mDC.m_isMarked = false;
				}
				bool flag7 = num2 > 0u;
				if (flag7)
				{
					mDC.m_unIID = num2;
					this.m_mapMonster.Add(num2, mDC);
				}
				else
				{
					mDC.isfake = true;
					mDC.m_unIID = this.idIdx;
					this.m_mapFakeMonster.Add(this.idIdx, mDC);
					this.idIdx += 1u;
				}
				bool flag8 = !GRMap.loading;
				if (flag8)
				{
					mDC.refreshViewType(2);
				}
				mDC.dartid = num;
				mDC.isDart = true;
			}
			this.m_listMonster.Add(mDC);
			bool flag9 = mDC != null;
			if (flag9)
			{
				base.dispatchEvent(GameEvent.Create(MonsterMgr.EVENT_MONSTER_ADD, this, mDC, false));
			}
			bool flag10 = d.ContainsKey("moving");
			if (flag10)
			{
				uint @uint = d["iid"]._uint;
				MonsterRole monster = MonsterMgr._inst.getMonster(@uint);
				float float2 = d["moving"]["to_x"]._float;
				float float3 = d["moving"]["to_y"]._float;
				Vector3 sourcePosition = new Vector3(float2 * 32f / 53.333f, 0f, float3 * 32f / 53.333f);
				NavMeshHit navMeshHit;
				NavMesh.SamplePosition(sourcePosition, out navMeshHit, 100f, monster.m_layer);
				monster.SetDestPos(navMeshHit.position);
			}
			result = mDC;
		}
		return result;
	}

	public MonsterRole AddMonster(int id, Vector3 pos, uint serverid = 0u, float roatate = 0f, int boset_num = 0, int carr = 0, string name = null)
	{
		this.init();
		bool flag = this.m_mapMonster.ContainsKey(serverid);
		MonsterRole result;
		if (flag)
		{
			result = null;
		}
		else
		{
			SXML sXML = this.dMon[id];
			int num = sXML.getInt("obj");
			float @float = sXML.getFloat("scale");
			bool flag2 = serverid <= 0u;
			if (flag2)
			{
				bool flag3 = Globle.m_nTestMonsterID > 0;
				if (flag3)
				{
					num = Globle.m_nTestMonsterID;
				}
			}
			bool flag4 = sXML.getInt("collect_tar") > 0;
			bool isHardDemo = Globle.isHardDemo;
			Type type;
			if (isHardDemo)
			{
				type = MonsterMgr.getTypeHandle("Md" + num);
			}
			else
			{
				bool flag5 = flag4;
				if (flag5)
				{
					bool flag6 = num == 122;
					if (flag6)
					{
						type = Type.GetType("CollectBox");
					}
					else
					{
						type = Type.GetType("CollectRole");
					}
				}
				else
				{
					type = Type.GetType("M" + num);
				}
			}
			bool flag7 = id == 4002 || carr == 2;
			if (flag7)
			{
				type = Type.GetType("M000P2");
			}
			else
			{
				bool flag8 = id == 4003 || carr == 3;
				if (flag8)
				{
					type = Type.GetType("M000P3");
				}
				else
				{
					bool flag9 = id == 4005 || carr == 5;
					if (flag9)
					{
						type = Type.GetType("M000P5");
					}
				}
			}
			bool flag10 = type == null;
			if (flag10)
			{
				type = Type.GetType("M00000");
			}
			MonsterRole monsterRole = (MonsterRole)Activator.CreateInstance(type);
			bool flag11 = name != null;
			if (flag11)
			{
				monsterRole.ownerName = name;
			}
			bool flag12 = carr == 0;
			if (flag12)
			{
				monsterRole.roleName = sXML.getString("name");
			}
			monsterRole.tempXMl = sXML;
			monsterRole.m_circle_type = sXML.getInt("boss_circle");
			bool flag13 = sXML.getFloat("boss_circle_scale") == -1f;
			if (flag13)
			{
				monsterRole.m_circle_scale = 1f;
			}
			else
			{
				monsterRole.m_circle_scale = sXML.getFloat("boss_circle_scale");
			}
			monsterRole.isBoos = (sXML.getInt("boss") == 1);
			bool flag14 = @float > 0f;
			if (flag14)
			{
				monsterRole.scale = @float;
			}
			bool flag15 = monsterRole != null;
			if (flag15)
			{
				bool flag16 = id == 4002 || carr == 2;
				if (flag16)
				{
					monsterRole.Init("profession/warrior_inst", EnumLayer.LM_MONSTER, pos, roatate);
				}
				else
				{
					bool flag17 = id == 4003 || carr == 3;
					if (flag17)
					{
						monsterRole.Init("profession/mage_inst", EnumLayer.LM_MONSTER, pos, roatate);
					}
					else
					{
						bool flag18 = id == 4005 || carr == 5;
						if (flag18)
						{
							monsterRole.Init("profession/assa_inst", EnumLayer.LM_MONSTER, pos, roatate);
						}
						else
						{
							bool flag19 = flag4;
							if (flag19)
							{
								monsterRole.Init("npc/" + num, EnumLayer.LM_MONSTER, pos, roatate);
							}
							else
							{
								monsterRole.Init("monster/" + num, EnumLayer.LM_MONSTER, pos, roatate);
							}
						}
					}
				}
				monsterRole.monsterid = id;
				bool flag20 = boset_num > 0;
				if (flag20)
				{
					PlayerNameUIMgr.getInstance().show(monsterRole);
					PlayerNameUIMgr.getInstance().seticon_forDaobao(monsterRole, boset_num);
				}
				bool flag21 = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info.Count > 0;
				if (flag21)
				{
					PlayerNameUIMgr.getInstance().seticon_forMonsterHunter(monsterRole, ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info[0]["target_mid"] != id);
				}
				bool flag22 = serverid > 0u;
				if (flag22)
				{
					monsterRole.m_unIID = serverid;
					this.m_mapMonster.Add(serverid, monsterRole);
				}
				else
				{
					monsterRole.isfake = true;
					monsterRole.m_unIID = this.idIdx;
					this.m_mapFakeMonster.Add(this.idIdx, monsterRole);
					this.idIdx += 1u;
				}
				bool flag23 = !GRMap.loading;
				if (flag23)
				{
					monsterRole.refreshViewType(2);
				}
			}
			this.m_listMonster.Add(monsterRole);
			bool flag24 = monsterRole != null;
			if (flag24)
			{
				base.dispatchEvent(GameEvent.Create(MonsterMgr.EVENT_MONSTER_ADD, this, monsterRole, false));
			}
			result = monsterRole;
		}
		return result;
	}

	public void RemoveMonster(MonsterRole role)
	{
		bool flag = !role.isfake;
		if (flag)
		{
			bool flag2 = !this.m_mapMonster.ContainsKey(role.m_unIID);
			if (flag2)
			{
				return;
			}
			MonsterRole monsterRole = this.m_mapMonster[role.m_unIID];
			monsterRole.dispose();
			this.m_mapMonster.Remove(role.m_unIID);
		}
		else
		{
			bool flag3 = !this.m_mapFakeMonster.ContainsKey(role.m_unIID);
			if (flag3)
			{
				return;
			}
			MonsterRole monsterRole2 = this.m_mapFakeMonster[role.m_unIID];
			monsterRole2.dispose();
			this.m_mapFakeMonster.Remove(role.m_unIID);
		}
		bool flag4 = a3_liteMinimap.instance != null;
		if (flag4)
		{
			a3_liteMinimap.instance.removeRoleInMiniMap(role.strIID);
		}
		this.m_listMonster.Remove(role);
		base.dispatchEvent(GameEvent.Create(MonsterMgr.EVENT_MONSTER_REMOVED, this, role, false));
	}

	public void HideInvaildMonster()
	{
		foreach (MonsterRole current in this.m_listMonster)
		{
			bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData == null;
			if (flag)
			{
				bool flag2 = current.ownerName != null && !current.ownerName.Equals(ModelBase<PlayerModel>.getInstance().name);
				if (flag2)
				{
					current.m_curModel.parent.gameObject.SetActive(false);
				}
			}
			bool flag3 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag3)
			{
				bool flag4 = current.ownerName != null && !BaseProxy<TeamProxy>.getInstance().MyTeamData.IsInMyTeam(current.ownerName);
				if (flag4)
				{
					current.m_curModel.parent.gameObject.SetActive(false);
				}
			}
		}
	}

	public void RefreshVaildMonster()
	{
		bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
		if (flag)
		{
			foreach (MonsterRole current in this.m_listMonster)
			{
				bool flag2 = current.ownerName != null && BaseProxy<TeamProxy>.getInstance().MyTeamData.IsInMyTeam(current.ownerName);
				if (flag2)
				{
					current.m_curModel.parent.gameObject.SetActive(true);
				}
			}
		}
	}

	public void RemoveMonster(uint iid)
	{
		bool flag = this.m_mapMonster.ContainsKey(iid);
		if (flag)
		{
			this.RemoveMonster(this.m_mapMonster[iid]);
		}
	}

	public void onMapLoaded()
	{
		foreach (MonsterRole current in this.m_mapMonster.Values)
		{
			current.refreshViewType(2);
		}
		foreach (Variant current2 in this.cacheProxy)
		{
			bool flag = current2.ContainsKey("carr");
			if (flag)
			{
				this.AddMonster_PVP(current2);
			}
			else
			{
				bool flag2 = current2.ContainsKey("owner_cid");
				if (flag2)
				{
					this.AddSummon(current2);
				}
				else
				{
					bool flag3 = current2.ContainsKey("escort_name");
					if (flag3)
					{
						bool flag4 = ModelBase<PlayerModel>.getInstance().up_lvl >= 1u;
						if (flag4)
						{
							this.AddDartCar(current2);
						}
					}
					else
					{
						this.AddMonster(current2, true);
					}
				}
			}
		}
		foreach (Variant current3 in this.cacheProxy_pvp)
		{
		}
		this.cacheProxy.Clear();
		this.cacheProxy_pvp.Clear();
	}

	public MonsterRole FindWhoPhy(Transform phy, bool includeCollect = false)
	{
		MonsterRole result;
		foreach (MonsterRole current in this.m_mapMonster.Values)
		{
			bool flag = !includeCollect && current is CollectRole;
			if (!flag)
			{
				bool isDead = current.isDead;
				if (!isDead)
				{
					bool flag2 = current.m_curPhy == phy;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
		}
		foreach (MonsterRole current2 in this.m_mapFakeMonster.Values)
		{
			bool flag3 = current2.isDead || current2 is CollectRole;
			if (!flag3)
			{
				bool flag4 = current2.m_curPhy == phy;
				if (flag4)
				{
					result = current2;
					return result;
				}
			}
		}
		result = null;
		return result;
	}

	public MonsterRole getServerMonster(uint iid)
	{
		bool flag = this.m_mapMonster.ContainsKey(iid);
		MonsterRole result;
		if (flag)
		{
			result = this.m_mapMonster[iid];
		}
		else
		{
			result = null;
		}
		return result;
	}

	public MonsterRole getFakeMonster(uint iid)
	{
		bool flag = this.m_mapFakeMonster.ContainsKey(iid);
		MonsterRole result;
		if (flag)
		{
			result = this.m_mapFakeMonster[iid];
		}
		else
		{
			result = null;
		}
		return result;
	}

	public MonsterRole getMonster(uint iid)
	{
		MonsterRole serverMonster = this.getServerMonster(iid);
		bool flag = serverMonster != null;
		MonsterRole result;
		if (flag)
		{
			result = serverMonster;
		}
		else
		{
			result = this.getFakeMonster(iid);
		}
		return result;
	}

	public bool HasMonInView()
	{
		bool result = false;
		Dictionary<uint, MonsterRole>.Enumerator enumerator = this.m_mapMonster.GetEnumerator();
		while (enumerator.MoveNext())
		{
			KeyValuePair<uint, MonsterRole> current = enumerator.Current;
			bool arg_42_0;
			if (!current.Value.isDead)
			{
				current = enumerator.Current;
				arg_42_0 = (current.Value is CollectRole);
			}
			else
			{
				arg_42_0 = true;
			}
			bool flag = arg_42_0;
			if (!flag)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public MonsterRole FindNearestMonster(Vector3 pos, Func<MonsterRole, bool> handle = null, bool useMark = false, PK_TYPE pkState = PK_TYPE.PK_PEACE, bool onTask = false)
	{
		float num = 9999999f;
		MonsterRole monsterRole = null;
		RoleMgr.ClearMark(!useMark, pkState, (BaseRole m) => (m.m_curPhy.position - pos).magnitude < (SelfRole.fsm.Autofighting ? StateInit.Instance.Distance : SelfRole._inst.m_LockDis));
		foreach (MonsterRole current in this.m_mapMonster.Values)
		{
			bool flag = current.isDead || current is CollectRole || (current is MS0000 && (long)((MS0000)current).owner_cid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid)) || (current is MDC000 && ((MDC000)current).escort_name == ModelBase<A3_LegionModel>.getInstance().myLegion.clname);
			if (!flag)
			{
				bool flag2 = current is MDC000 && (int)((float)((MDC000)current).curhp / (float)((MDC000)current).maxHp * 100f) <= 20;
				if (!flag2)
				{
					TaskMonId expr_104 = this.taskMonId;
					bool flag3 = expr_104 != null && expr_104.applied && this.taskMonId.value != current.monsterid;
					if (!flag3)
					{
						bool flag4 = handle != null && handle(current);
						if (!flag4)
						{
							bool flag5 = onTask && ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.ContainsKey(ModelBase<A3_TaskModel>.getInstance().main_task_id) && current.monsterid != ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack[ModelBase<A3_TaskModel>.getInstance().main_task_id];
							if (!flag5)
							{
								bool flag6 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
								if (flag6)
								{
									bool mwlr_on = ModelBase<A3_ActiveModel>.getInstance().mwlr_on;
									if (mwlr_on)
									{
										bool flag7 = ModelBase<A3_ActiveModel>.getInstance().mwlr_target_monId != current.monsterid;
										if (flag7)
										{
											continue;
										}
									}
									bool flag8 = current.ownerName != null && !BaseProxy<TeamProxy>.getInstance().MyTeamData.IsInMyTeam(current.ownerName);
									if (flag8)
									{
										continue;
									}
								}
								else
								{
									bool flag9 = current.ownerName != null && current.ownerName != ModelBase<PlayerModel>.getInstance().name;
									if (flag9)
									{
										continue;
									}
								}
								bool flag10 = SelfRole.fsm.Autofighting && (current.m_curPhy.position - StateInit.Instance.Origin).magnitude > StateInit.Instance.Distance;
								if (!flag10)
								{
									bool flag11 = pkState != PK_TYPE.PK_PKALL && current is MS0000 && ((MS0000)current).owner_cid != 0;
									if (!flag11)
									{
										float magnitude = (current.m_curPhy.position - pos).magnitude;
										bool flag12 = magnitude < (SelfRole.fsm.Autofighting ? Mathf.Min(SelfRole._inst.m_LockDis, StateInit.Instance.Distance) : SelfRole._inst.m_LockDis) && magnitude < num;
										if (flag12)
										{
											num = magnitude;
											monsterRole = current;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		bool flag13 = monsterRole != null & useMark;
		if (flag13)
		{
			monsterRole.m_isMarked = true;
		}
		return monsterRole;
	}

	public MonsterRole FindNearestSummon(Vector3 pos)
	{
		MonsterRole result = null;
		float num = 3.40282347E+38f;
		foreach (MonsterRole current in this.m_mapMonster.Values)
		{
			int ownerCid = 0;
			bool flag = current is MS0000;
			if (flag)
			{
				ownerCid = ((MS0000)current).owner_cid;
			}
			bool isMarked = current.m_isMarked;
			if (!isMarked)
			{
				bool flag2 = ownerCid == 0;
				if (!flag2)
				{
					bool flag3 = ModelBase<PlayerModel>.getInstance().pk_state > PK_TYPE.PK_PEACE;
					if (flag3)
					{
						bool flag4 = (long)ownerCid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid);
						if (flag4)
						{
							continue;
						}
						bool flag5 = ModelBase<PlayerModel>.getInstance().pk_state == PK_TYPE.PK_TEAM;
						if (flag5)
						{
							bool arg_102_0;
							if (BaseProxy<TeamProxy>.getInstance().MyTeamData != null)
							{
								List<ItemTeamData> expr_E6 = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList;
								arg_102_0 = (expr_E6 != null && expr_E6.Exists((ItemTeamData member) => (ulong)member.cid == (ulong)((long)ownerCid)));
							}
							else
							{
								arg_102_0 = false;
							}
							bool flag6 = arg_102_0;
							if (flag6)
							{
								continue;
							}
							bool flag7 = (long)ownerCid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid);
							if (flag7)
							{
								continue;
							}
						}
					}
					float magnitude = (current.m_curPhy.position - pos).magnitude;
					bool flag8 = magnitude < (SelfRole.fsm.Autofighting ? Mathf.Min(SelfRole._inst.m_LockDis, StateInit.Instance.Distance) : SelfRole._inst.m_LockDis) && magnitude < num;
					if (flag8)
					{
						num = magnitude;
						result = current;
					}
				}
			}
		}
		return result;
	}

	public MonsterRole FindNearestFakeMonster(Vector3 pos)
	{
		float num = 9999999f;
		MonsterRole result = null;
		foreach (MonsterRole current in this.m_mapFakeMonster.Values)
		{
			bool flag = current.isDead || current is CollectRole || (current is MS0000 && (long)((MS0000)current).owner_cid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid)) || (current is MDC000 && ((MDC000)current).escort_name == ModelBase<A3_LegionModel>.getInstance().myLegion.clname);
			if (!flag)
			{
				bool flag2 = current is MDC000 && (int)((float)((MDC000)current).curhp / (float)((MDC000)current).maxHp * 100f) <= 20;
				if (!flag2)
				{
					float magnitude = (current.m_curPhy.position - pos).magnitude;
					bool flag3 = magnitude < SelfRole._inst.m_LockDis && magnitude < num;
					if (flag3)
					{
						num = magnitude;
						result = current;
					}
				}
			}
		}
		return result;
	}

	public Vector3 FindNearestMonsterPos(Vector3 me)
	{
		MonsterRole monsterRole = this.FindNearestMonster(me, null, false, PK_TYPE.PK_PEACE, false);
		bool flag = monsterRole != null;
		Vector3 result;
		if (flag)
		{
			result = monsterRole.m_curPhy.position;
		}
		else
		{
			result = Vector3.zero;
		}
		return result;
	}

	public void clear()
	{
		foreach (MonsterRole current in this.m_mapMonster.Values)
		{
			current.dispose();
			bool flag = current is MonsterPlayer;
			if (flag)
			{
				current.dispose();
			}
		}
		foreach (MonsterRole current2 in this.m_mapFakeMonster.Values)
		{
			current2.dispose();
		}
		this.m_mapMonster.Clear();
		this.m_mapFakeMonster.Clear();
	}

	public void FrameMove(float fdt)
	{
		foreach (MonsterRole current in this.m_mapMonster.Values)
		{
			current.FrameMove(fdt);
		}
		this.need_remove_list.Clear();
		foreach (MonsterRole current2 in this.m_mapFakeMonster.Values)
		{
			current2.FrameMove(fdt);
			bool remove_after_dead = current2.m_remove_after_dead;
			if (remove_after_dead)
			{
				this.need_remove_list.Add(current2);
			}
		}
		foreach (MonsterRole current3 in this.need_remove_list)
		{
			this.RemoveMonster(current3);
		}
	}

	public float GetTraceRange(int mid)
	{
		bool flag = this.dMon.ContainsKey(mid);
		float result;
		if (flag)
		{
			SXML expr_1D = this.dMon[mid];
			float? arg_59_0;
			if (expr_1D == null)
			{
				arg_59_0 = null;
			}
			else
			{
				SXML expr_3B = expr_1D.GetNode("ai", "");
				arg_59_0 = ((expr_3B != null) ? new float?(expr_3B.getFloat("tracerang")) : null);
			}
			result = (arg_59_0 ?? 0f) / 53.333f;
		}
		else
		{
			result = 0f;
		}
		return result;
	}
}
