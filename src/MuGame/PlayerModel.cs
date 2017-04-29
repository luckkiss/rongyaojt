using Cross;
using GameFramework;
using MuGame.Qsmy.model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class PlayerModel : ModelBase<PlayerModel>
	{
		public static uint ON_ATTR_CHANGE = 0u;

		public static uint ON_LEVEL_CHANGED = 1u;

		public int profession;

		public uint cid;

		public uint uid;

		public uint iid;

		public uint crttm;

		public string name = "nil";

		public uint vip = 0u;

		public bool _isvipActive = false;

		private bool _inFb;

		public bool showFriend = false;

		public int clan_buff_lvl;

		public bool _istitleActive = false;

		public Vector3 enter_map_pos = default(Vector3);

		public int now_pkState = 0;

		public PK_TYPE pk_state = PK_TYPE.PK_PEACE;

		public uint m_unPK_Param = 0u;

		public uint m_unPK_Param2 = 33333333u;

		public int now_nameState = 0;

		public REDNAME_TYPE name_state = REDNAME_TYPE.RNT_NORMAL;

		public uint sinsNub;

		public uint hitBack = 0u;

		public uint clanid;

		public uint teamid;

		public uint exp_time;

		private Dictionary<int, int> cur_att_pt = new Dictionary<int, int>();

		private uint _money;

		public uint gift;

		public uint ach_point;

		public int nobpt;

		public uint gold;

		public uint _mapid;

		public uint _lvl;

		public uint treasure_num;

		public int serial;

		public uint up_lvl = 0u;

		public uint exp;

		public int accent_exp;

		public int hp;

		public int mp;

		public int combpt;

		public bool _pkState = false;

		private int _max_hp;

		public float mapBeginX = 0f;

		public float mapBeginY = 0f;

		public float mapBeginroatate = 0f;

		public uint strength;

		public uint agility;

		public uint constitution;

		public uint intelligence;

		public uint max_attack;

		public uint physics_def;

		public uint magic_def;

		public uint fire_att;

		public uint ice_att;

		public uint light_att;

		public uint fire_def;

		public uint ice_def;

		public uint light_def;

		public uint max_mp;

		public uint crime;

		public uint mp_abate;

		public uint hp_suck;

		public uint physics_dmg_red;

		public uint magic_dmg_red;

		public uint skill_damage;

		public uint fatal_att;

		public uint fatal_dodge;

		public uint max_hp_add;

		public uint max_mp_add;

		public uint hp_recovery;

		public uint mp_recovery;

		public uint mp_suck;

		public uint magic_shield;

		public uint exp_add;

		public uint blessing;

		public uint knowledge_add;

		public uint fatal_damage;

		public uint fire_def_add;

		public uint ice_def_add;

		public uint light_def_add;

		public uint wisdom;

		public uint min_attack;

		public uint double_damage_rate;

		public uint reflect_crit_rate;

		public uint ignore_crit_rate;

		public uint crit_add_hp;

		public uint hit;

		public uint dodge;

		public uint ignore_defense_damage;

		public uint stagger;

		private int _pt_att;

		public int pt_strpt;

		public int pt_conpt;

		public int pt_intept;

		public int pt_wispt;

		public int pt_agipt;

		public Dictionary<int, int> task_monsterId = new Dictionary<int, int>();

		public Dictionary<int, int> task_monsterIdOnAttack = new Dictionary<int, int>();

		public Dictionary<uint, int> attr_list = new Dictionary<uint, int>();

		public Dictionary<uint, int> attChange_eqp = new Dictionary<uint, int>();

		public bool inDefendArea = true;

		public bool isFirstRechange = false;

		public bool hasKaifuActive = true;

		public int last_time;

		public bool first;

		public bool havePet;

		public int selfPetTime;

		public bool showBaotu_ui = false;

		public uint oldLv = 1u;

		public uint olduplvl = 0u;

		public int oldCombpt = 0;

		private bool isFirstVipLoad = true;

		public bool inFb
		{
			get
			{
				return this._inFb;
			}
			set
			{
				this._inFb = value;
				if (value)
				{
					A3_BeStronger expr_14 = A3_BeStronger.Instance;
					if (expr_14 != null)
					{
						expr_14.gameObject.SetActive(false);
					}
				}
				else
				{
					A3_BeStronger expr_2D = A3_BeStronger.Instance;
					if (expr_2D != null)
					{
						expr_2D.CheckUpItem();
					}
				}
			}
		}

		public bool isvipActive
		{
			get
			{
				return this._isvipActive;
			}
			set
			{
				this._isvipActive = value;
				bool flag = !this._isvipActive;
				if (flag)
				{
				}
			}
		}

		public bool istitleActive
		{
			get
			{
				return this._istitleActive;
			}
			set
			{
				this._istitleActive = value;
				bool flag = !this._istitleActive;
				if (flag)
				{
					bool flag2 = SelfRole._inst != null;
					if (flag2)
					{
						SelfRole._inst.refreshtitle(0);
					}
				}
			}
		}

		public uint money
		{
			get
			{
				return this._money;
			}
			set
			{
				this._money = value;
				a3_BagModel expr_0D = ModelBase<a3_BagModel>.getInstance();
				if (expr_0D != null)
				{
					expr_0D.OnMoneyChange();
				}
			}
		}

		public uint mapid
		{
			get
			{
				return this._mapid;
			}
			set
			{
				this._mapid = value;
			}
		}

		public uint lvl
		{
			get
			{
				return this._lvl;
			}
			set
			{
				this._lvl = value;
			}
		}

		public int max_hp
		{
			get
			{
				return this._max_hp;
			}
			set
			{
				bool flag = this._max_hp == value;
				if (!flag)
				{
					this._max_hp = value;
					bool flag2 = lgSelfPlayer.instance != null;
					if (flag2)
					{
						lgSelfPlayer.instance.modMaxHp(value, this.hp);
					}
				}
			}
		}

		public int pt_att
		{
			get
			{
				return this._pt_att;
			}
			set
			{
				this._pt_att = value;
				bool flag = ModelBase<PlayerModel>.getInstance().up_lvl == 0u && ModelBase<PlayerModel>.getInstance().lvl <= 80u;
				if (flag)
				{
					this.autoAddPrint();
				}
				A3_BeStronger expr_3A = A3_BeStronger.Instance;
				if (expr_3A != null)
				{
					expr_3A.CheckUpItem();
				}
			}
		}

		public bool pkState
		{
			get
			{
				return this._pkState;
			}
			set
			{
				bool flag = this._pkState == value;
				if (!flag)
				{
					this._pkState = value;
				}
			}
		}

		public bool IsCaptainOrAlone
		{
			get
			{
				ItemTeamMemberData expr_0A = BaseProxy<TeamProxy>.getInstance().MyTeamData;
				return expr_0A == null || expr_0A.meIsCaptain;
			}
		}

		public bool IsInATeam
		{
			get
			{
				return BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			}
		}

		public void modHp(int hprest)
		{
			this.hp = hprest;
			InterfaceMgr.doCommandByLua("PlayerModel:getInstance().modHp", "model/PlayerModel", new object[]
			{
				this.hp,
				this.max_hp
			});
		}

		public void modMp(int mprest)
		{
			this.mp = mprest;
			InterfaceMgr.doCommandByLua("PlayerModel:getInstance().modMp", "model/PlayerModel", new object[]
			{
				this.mp,
				this.max_mp
			});
		}

		public void init(Variant data)
		{
			Globle.inGame = true;
			bool flag = !data.ContainsKey("battleAttrs");
			if (!flag)
			{
				this.profession = data["carr"];
				this.cid = data["cid"];
				this.uid = data["uid"];
				this.iid = data["iid"];
				this.name = data["name"];
				this.clan_buff_lvl = data["clan_buff_lvl"];
				this.enter_map_pos.x = data["x"] / 53.333f;
				this.enter_map_pos.y = 0f;
				this.enter_map_pos.z = data["y"] / 53.333f;
				Debug.Log("玩家出生的坐标为" + this.enter_map_pos);
				bool flag2 = data.ContainsKey("crttm");
				if (flag2)
				{
					this.crttm = data["crttm"];
				}
				bool flag3 = data.ContainsKey("yb");
				if (flag3)
				{
					this.gold = data["yb"];
				}
				bool flag4 = data.ContainsKey("ach_point");
				if (flag4)
				{
					this.ach_point = data["ach_point"];
				}
				bool flag5 = data.ContainsKey("money");
				if (flag5)
				{
					this.money = data["money"];
				}
				bool flag6 = data.ContainsKey("bndyb");
				if (flag6)
				{
					this.gift = data["bndyb"];
				}
				bool flag7 = data.ContainsKey("zhuan");
				if (flag7)
				{
					this.up_lvl = data["zhuan"];
				}
				bool flag8 = data.ContainsKey("pet_food_last_time");
				if (flag8)
				{
					this.last_time = data["pet_food_last_time"];
					a3_expbar.feedTime = this.last_time;
				}
				bool flag9 = data.ContainsKey("first_pet_food");
				if (flag9)
				{
					this.first = data["first_pet_food"];
				}
				bool flag10 = data.ContainsKey("pet");
				if (flag10)
				{
					bool flag11 = data["pet"]["id"] > 0;
					if (flag11)
					{
						this.havePet = true;
					}
					else
					{
						this.havePet = false;
					}
					Variant variant = data["pet"];
					A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
					instance.Tpid = variant["id"];
					A3_PetModel.curPetid = variant["id"];
					ModelBase<A3_PetModel>.getInstance().petId = data["pet"]["id_arr"]._arr;
				}
				bool flag12 = data.ContainsKey("treasure_num");
				if (flag12)
				{
					this.treasure_num = data["treasure_num"];
					bool flag13 = this.treasure_num >= 50u;
					if (flag13)
					{
						this.showBaotu_ui = true;
					}
				}
				bool flag14 = data.ContainsKey("serial_kp");
				if (flag14)
				{
					this.serial = data["serial_kp"];
				}
				this.mapid = data["mpid"];
				this.lvl = data["lvl"];
				this.oldLv = this.lvl;
				this.exp = data["exp"];
				bool flag15 = data.ContainsKey("remains_exp");
				if (flag15)
				{
					this.accent_exp = data["remains_exp"];
				}
				this.hp = data["hp"];
				this.combpt = data["combpt"];
				this.oldCombpt = this.combpt;
				this.inDefendArea = !data["in_pczone"]._bool;
				this.clanid = data["clanid"];
				bool flag16 = data.ContainsKey("teamid");
				if (flag16)
				{
					this.teamid = data["teamid"];
				}
				this.pkState = (data["pk_state"] == 1);
				bool flag17 = data.ContainsKey("pk_state");
				if (flag17)
				{
					this.now_pkState = data["pk_state"];
				}
				switch (this.now_pkState)
				{
				case 0:
					this.pk_state = PK_TYPE.PK_PEACE;
					break;
				case 1:
					this.pk_state = PK_TYPE.PK_PKALL;
					this.m_unPK_Param = this.cid;
					this.m_unPK_Param2 = this.cid;
					break;
				case 2:
					this.pk_state = PK_TYPE.PK_TEAM;
					this.m_unPK_Param = this.teamid;
					this.m_unPK_Param2 = this.clanid;
					break;
				case 3:
					this.pk_state = PK_TYPE.PK_LEGION;
					this.m_unPK_Param = this.clanid;
					break;
				case 4:
					this.pk_state = PK_TYPE.PK_HERO;
					break;
				}
				bool flag18 = data.ContainsKey("rednm");
				if (flag18)
				{
					this.now_nameState = data["rednm"];
				}
				bool flag19 = data.ContainsKey("pk_v");
				if (flag19)
				{
					this.sinsNub = data["pk_v"];
					this.crime = data["pk_v"];
					this.attr_list[16u] = data["pk_v"];
					debug.Log("罪恶值：" + this.sinsNub);
				}
				bool flag20 = data.ContainsKey("strike_back_tm");
				if (flag20)
				{
					this.hitBack = data["strike_back_tm"] - (uint)NetClient.instance.CurServerTimeStamp;
				}
				switch (this.now_nameState)
				{
				case 0:
					this.name_state = REDNAME_TYPE.RNT_NORMAL;
					break;
				case 1:
					this.name_state = REDNAME_TYPE.RNT_RASCAL;
					break;
				case 2:
					this.name_state = REDNAME_TYPE.RNT_EVIL;
					break;
				case 3:
					this.name_state = REDNAME_TYPE.RNT_DEVIL;
					break;
				}
				this.pt_att = data["att_pt"];
				this.pt_strpt = data["strpt"];
				this.pt_conpt = data["conpt"];
				this.pt_intept = data["intept"];
				this.pt_wispt = data["wispt"];
				this.pt_agipt = data["agipt"];
				bool flag21 = data.ContainsKey("battleAttrs");
				if (flag21)
				{
					this.attrChange(data);
				}
				bool flag22 = data.ContainsKey("nobpt");
				if (flag22)
				{
					ModelBase<PlayerModel>.getInstance().nobpt = data["nobpt"];
				}
				bool flag23 = data.ContainsKey("curpets");
				if (flag23)
				{
				}
				ModelBase<SkillModel>.getInstance().initSkillList(data["skills"]._arr);
				ModelBase<Skill_a3Model>.getInstance().initSkillList(data["skills"]._arr);
				ModelBase<Skill_a3Model>.getInstance().skillGroups(data["skill_groups"]._arr);
				debug.Log("技能信息：" + data["skills"].dump());
				ModelBase<Skill_a3Model>.getInstance().skills = data["skills"];
				bool flag24 = data["skills"].Length > 0;
				if (flag24)
				{
					foreach (Variant current in data["skills"]._arr)
					{
						ModelBase<Skill_a3Model>.getInstance().skillinfos(current["skill_id"], current["skill_level"]);
					}
				}
				bool flag25 = data.ContainsKey("items");
				if (flag25)
				{
					ModelBase<a3_BagModel>.getInstance().initItemList(data["items"]._arr);
				}
				bool flag26 = data.ContainsKey("equipments2");
				if (flag26)
				{
					ModelBase<a3_EquipModel>.getInstance().initEquipList(data["equipments2"]._arr);
				}
				bool flag27 = data.ContainsKey("eqp_stones2");
				if (flag27)
				{
					ModelBase<A3_RuneStoneModel>.getInstance().initDressupInfos(data["eqp_stones2"]._arr);
				}
				bool flag28 = data.ContainsKey("dressments");
				if (flag28)
				{
					debug.Log("有要更新的时装数据");
					List<Variant> arr = data["dressments"]._arr;
					foreach (Variant current2 in arr)
					{
						int @int = current2["dressid"]._int;
						SXML sXML = XMLMgr.instance.GetSXML("dress.dress_info", "id==" + @int);
						int int2 = sXML.getInt("dress_type");
						bool flag29 = int2 >= 0 && int2 < lgSelfPlayer.instance.m_nDress_PartID.Length;
						if (flag29)
						{
							lgSelfPlayer.instance.m_nDress_PartID[int2] = @int;
						}
						debug.Log("时装的ID " + current2["dressid"]);
					}
				}
				bool flag30 = data.ContainsKey("has_kaifuactivity");
				if (flag30)
				{
					this.hasKaifuActive = data["has_kaifuactivity"];
				}
				ModelBase<AutoPlayModel>.getInstance().Init();
				bool flag31 = data.ContainsKey("wing");
				if (flag31)
				{
					int num = data["wing"]["show_stage"];
					A3_WingModel instance2 = ModelBase<A3_WingModel>.getInstance();
					bool flag32 = num > 0;
					if (flag32)
					{
						instance2.ShowStage = num;
					}
					else
					{
						instance2.ShowStage = 0;
					}
				}
				bool flag33 = data.ContainsKey("ach_point");
				if (flag33)
				{
					this.istitleActive = (data["ach_title"] > 0);
					this.istitleActive = data["title_display"]._bool;
					ModelBase<a3_RankModel>.getInstance().refreinfo(data["ach_title"], data["ach_point"], data["title_display"]._bool);
				}
				FunctionOpenMgr.instance.onLvUp((int)this.up_lvl, (int)this.lvl, false);
				this.initProxy();
				InterfaceMgr.doCommandByLua("PlayerModel:getInstance().initInfo", "model/PlayerModel", new object[]
				{
					data
				});
				GameSdkMgr.record_login();
			}
		}

		public void attrChange(Variant data)
		{
			Debug.Log("XXXX" + data.dump());
			Variant variant = data["battleAttrs"];
			this.strength = variant["strength"];
			this.agility = variant["agility"];
			this.constitution = variant["constitution"];
			this.intelligence = variant["intelligence"];
			this.max_attack = variant["max_attack"];
			this.physics_def = variant["physics_def"];
			this.magic_def = variant["magic_def"];
			this.fire_att = variant["fire_att"];
			this.ice_att = variant["ice_att"];
			this.light_att = variant["light_att"];
			this.fire_def = variant["fire_def"];
			this.ice_def = variant["ice_def"];
			this.light_def = variant["light_def"];
			this.max_hp = variant["max_hp"];
			this.max_mp = variant["max_mp"];
			this.mp_abate = variant["mp_abate"];
			this.hp_suck = variant["hp_suck"];
			this.physics_dmg_red = variant["physics_dmg_red"];
			this.magic_dmg_red = variant["magic_dmg_red"];
			this.skill_damage = variant["skill_damage"];
			this.fatal_att = variant["fatal_att"];
			this.fatal_dodge = variant["fatal_dodge"];
			this.max_hp_add = variant["max_hp_add"];
			this.max_mp_add = variant["max_mp_add"];
			this.hp_recovery = variant["hp_recovery"];
			this.mp_recovery = variant["mp_recovery"];
			this.mp_suck = variant["mp_suck"];
			this.magic_shield = variant["magic_shield"];
			this.exp_add = variant["exp_add"];
			this.blessing = variant["blessing"];
			this.knowledge_add = variant["knowledge_add"];
			this.fatal_damage = variant["fatal_damage"];
			this.fire_def_add = variant["fire_def_add"];
			this.ice_def_add = variant["ice_def_add"];
			this.light_def_add = variant["light_def_add"];
			this.wisdom = variant["wisdom"];
			this.min_attack = variant["min_attack"];
			bool flag = variant.ContainsKey("hit");
			if (flag)
			{
				this.double_damage_rate = variant["double_damage_rate"];
				this.reflect_crit_rate = variant["reflect_crit_rate"];
				this.ignore_crit_rate = variant["ignore_crit_rate"];
				this.crit_add_hp = variant["crit_add_hp"];
				this.hit = variant["hit"];
				this.dodge = variant["dodge"];
				this.ignore_defense_damage = variant["ignore_defense_damage"];
				this.stagger = variant["stagger"];
			}
			this.attr_list[1u] = variant["strength"];
			this.attr_list[2u] = variant["agility"];
			this.attr_list[3u] = variant["constitution"];
			this.attr_list[4u] = variant["intelligence"];
			this.attr_list[5u] = variant["max_attack"];
			this.attr_list[6u] = variant["physics_def"];
			this.attr_list[7u] = variant["magic_def"];
			this.attr_list[8u] = variant["fire_att"];
			this.attr_list[9u] = variant["ice_att"];
			this.attr_list[10u] = variant["light_att"];
			this.attr_list[11u] = variant["fire_def"];
			this.attr_list[12u] = variant["ice_def"];
			this.attr_list[13u] = variant["light_def"];
			this.attr_list[14u] = variant["max_hp"];
			this.attr_list[15u] = variant["max_mp"];
			this.attr_list[17u] = variant["mp_abate"];
			this.attr_list[18u] = variant["hp_suck"];
			this.attr_list[19u] = variant["physics_dmg_red"];
			this.attr_list[20u] = variant["magic_dmg_red"];
			this.attr_list[21u] = variant["skill_damage"];
			this.attr_list[22u] = variant["fatal_att"];
			this.attr_list[23u] = variant["fatal_dodge"];
			this.attr_list[24u] = variant["max_hp_add"];
			this.attr_list[25u] = variant["max_mp_add"];
			this.attr_list[26u] = variant["hp_recovery"];
			this.attr_list[27u] = variant["mp_recovery"];
			this.attr_list[28u] = variant["mp_suck"];
			this.attr_list[29u] = variant["magic_shield"];
			this.attr_list[30u] = variant["exp_add"];
			this.attr_list[31u] = variant["blessing"];
			this.attr_list[32u] = variant["knowledge_add"];
			this.attr_list[33u] = variant["fatal_damage"];
			this.attr_list[34u] = variant["wisdom"];
			this.attr_list[35u] = variant["fire_def_add"];
			this.attr_list[36u] = variant["ice_def_add"];
			this.attr_list[37u] = variant["light_def_add"];
			this.attr_list[38u] = variant["min_attack"];
			bool flag2 = variant.ContainsKey("hit");
			if (flag2)
			{
				this.attr_list[39u] = variant["double_damage_rate"];
				this.attr_list[40u] = variant["reflect_crit_rate"];
				this.attr_list[41u] = variant["ignore_crit_rate"];
				this.attr_list[42u] = variant["crit_add_hp"];
				this.attr_list[43u] = variant["hit"];
				this.attr_list[44u] = variant["dodge"];
				this.attr_list[45u] = variant["ignore_defense_damage"];
				this.attr_list[46u] = variant["stagger"];
			}
		}

		public void attrChangeCheck(Variant att)
		{
			bool flag = false;
			this.attChange_eqp.Clear();
			bool flag2 = att.ContainsKey("strength");
			if (flag2)
			{
				this.attChange_eqp[1u] = (int)(att["strength"] - this.strength);
				this.strength = att["strength"];
				this.attr_list[1u] = att["strength"];
				flag = true;
			}
			bool flag3 = att.ContainsKey("agility");
			if (flag3)
			{
				this.attChange_eqp[2u] = (int)(att["agility"] - this.agility);
				this.agility = att["agility"];
				this.attr_list[2u] = att["agility"];
				flag = true;
			}
			bool flag4 = att.ContainsKey("constitution");
			if (flag4)
			{
				this.attChange_eqp[3u] = (int)(att["constitution"] - this.constitution);
				this.constitution = att["constitution"];
				this.attr_list[3u] = att["constitution"];
				flag = true;
			}
			bool flag5 = att.ContainsKey("intelligence");
			if (flag5)
			{
				this.attChange_eqp[4u] = (int)(att["intelligence"] - this.intelligence);
				this.intelligence = att["intelligence"];
				this.attr_list[4u] = att["intelligence"];
				flag = true;
			}
			bool flag6 = att.ContainsKey("max_attack");
			if (flag6)
			{
				this.attChange_eqp[5u] = (int)(att["max_attack"] - this.max_attack);
				this.max_attack = att["max_attack"];
				this.attr_list[5u] = att["max_attack"];
				flag = true;
			}
			bool flag7 = att.ContainsKey("physics_def");
			if (flag7)
			{
				this.attChange_eqp[6u] = (int)(att["physics_def"] - this.physics_def);
				this.physics_def = att["physics_def"];
				this.attr_list[6u] = att["physics_def"];
				flag = true;
			}
			bool flag8 = att.ContainsKey("magic_def");
			if (flag8)
			{
				this.attChange_eqp[7u] = (int)(att["magic_def"] - this.magic_def);
				this.magic_def = att["magic_def"];
				this.attr_list[7u] = att["magic_def"];
				flag = true;
			}
			bool flag9 = att.ContainsKey("fire_att");
			if (flag9)
			{
				this.attChange_eqp[8u] = (int)(att["fire_att"] - this.fire_att);
				this.fire_att = att["fire_att"];
				this.attr_list[8u] = att["fire_att"];
				flag = true;
			}
			bool flag10 = att.ContainsKey("ice_att");
			if (flag10)
			{
				this.attChange_eqp[9u] = (int)(att["ice_att"] - this.ice_att);
				this.ice_att = att["ice_att"];
				this.attr_list[9u] = att["ice_att"];
				flag = true;
			}
			bool flag11 = att.ContainsKey("light_att");
			if (flag11)
			{
				this.attChange_eqp[10u] = (int)(att["light_att"] - this.light_att);
				this.light_att = att["light_att"];
				this.attr_list[10u] = att["light_att"];
				flag = true;
			}
			bool flag12 = att.ContainsKey("fire_def");
			if (flag12)
			{
				this.attChange_eqp[11u] = (int)(att["fire_def"] - this.fire_def);
				this.fire_def = att["fire_def"];
				this.attr_list[11u] = att["fire_def"];
				flag = true;
			}
			bool flag13 = att.ContainsKey("ice_def");
			if (flag13)
			{
				this.attChange_eqp[12u] = (int)(att["ice_def"] - this.ice_def);
				this.ice_def = att["ice_def"];
				this.attr_list[12u] = att["ice_def"];
				flag = true;
			}
			bool flag14 = att.ContainsKey("light_def");
			if (flag14)
			{
				this.attChange_eqp[13u] = (int)(att["light_def"] - this.light_def);
				this.light_def = att["light_def"];
				this.attr_list[13u] = att["light_def"];
				flag = true;
			}
			bool flag15 = att.ContainsKey("max_hp");
			if (flag15)
			{
				this.attChange_eqp[14u] = att["max_hp"] - this.max_hp;
				this.max_hp = att["max_hp"];
				this.attr_list[14u] = att["max_hp"];
				flag = true;
			}
			bool flag16 = att.ContainsKey("max_mp");
			if (flag16)
			{
				this.attChange_eqp[15u] = (int)(att["max_mp"] - this.max_mp);
				this.max_mp = att["max_mp"];
				this.attr_list[15u] = att["max_mp"];
				flag = true;
			}
			bool flag17 = att.ContainsKey("mp_abate");
			if (flag17)
			{
				this.attChange_eqp[17u] = (int)(att["mp_abate"] - this.mp_abate);
				this.mp_abate = att["mp_abate"];
				this.attr_list[17u] = att["mp_abate"];
				flag = true;
			}
			bool flag18 = att.ContainsKey("hp_suck");
			if (flag18)
			{
				this.attChange_eqp[18u] = (int)(att["hp_suck"] - this.hp_suck);
				this.hp_suck = att["hp_suck"];
				this.attr_list[18u] = att["hp_suck"];
				flag = true;
			}
			bool flag19 = att.ContainsKey("physics_dmg_red");
			if (flag19)
			{
				this.attChange_eqp[19u] = (int)(att["physics_dmg_red"] - this.physics_dmg_red);
				this.physics_dmg_red = att["physics_dmg_red"];
				this.attr_list[19u] = att["physics_dmg_red"];
				flag = true;
			}
			bool flag20 = att.ContainsKey("magic_dmg_red");
			if (flag20)
			{
				this.attChange_eqp[20u] = (int)(att["magic_dmg_red"] - this.magic_dmg_red);
				this.magic_dmg_red = att["magic_dmg_red"];
				this.attr_list[20u] = att["magic_dmg_red"];
				flag = true;
			}
			bool flag21 = att.ContainsKey("skill_damage");
			if (flag21)
			{
				this.attChange_eqp[21u] = (int)(att["skill_damage"] - this.skill_damage);
				this.skill_damage = att["skill_damage"];
				this.attr_list[21u] = att["skill_damage"];
				flag = true;
			}
			bool flag22 = att.ContainsKey("fatal_att");
			if (flag22)
			{
				this.attChange_eqp[22u] = (int)(att["fatal_att"] - this.fatal_att);
				this.fatal_att = att["fatal_att"];
				this.attr_list[22u] = att["fatal_att"];
				flag = true;
			}
			bool flag23 = att.ContainsKey("fatal_dodge");
			if (flag23)
			{
				this.attChange_eqp[23u] = (int)(att["fatal_dodge"] - this.fatal_dodge);
				this.fatal_dodge = att["fatal_dodge"];
				this.attr_list[23u] = att["fatal_dodge"];
				flag = true;
			}
			bool flag24 = att.ContainsKey("max_hp_add");
			if (flag24)
			{
				this.attChange_eqp[24u] = (int)(att["max_hp_add"] - this.max_hp_add);
				this.max_hp_add = att["max_hp_add"];
				this.attr_list[24u] = att["max_hp_add"];
				flag = true;
			}
			bool flag25 = att.ContainsKey("max_mp_add");
			if (flag25)
			{
				this.attChange_eqp[25u] = (int)(att["max_mp_add"] - this.max_mp_add);
				this.max_mp_add = att["max_mp_add"];
				this.attr_list[25u] = att["max_mp_add"];
				flag = true;
			}
			bool flag26 = att.ContainsKey("hp_recovery");
			if (flag26)
			{
				this.attChange_eqp[26u] = (int)(att["hp_recovery"] - this.hp_recovery);
				this.hp_recovery = att["hp_recovery"];
				this.attr_list[26u] = att["hp_recovery"];
				flag = true;
			}
			bool flag27 = att.ContainsKey("mp_recovery");
			if (flag27)
			{
				this.attChange_eqp[27u] = (int)(att["mp_recovery"] - this.mp_recovery);
				this.mp_recovery = att["mp_recovery"];
				this.attr_list[27u] = att["mp_recovery"];
				flag = true;
			}
			bool flag28 = att.ContainsKey("mp_suck");
			if (flag28)
			{
				this.attChange_eqp[28u] = (int)(att["mp_suck"] - this.mp_suck);
				this.mp_suck = att["mp_suck"];
				this.attr_list[28u] = att["mp_suck"];
				flag = true;
			}
			bool flag29 = att.ContainsKey("stremagic_shieldngth");
			if (flag29)
			{
				this.attChange_eqp[29u] = (int)(att["magic_shield"] - this.magic_shield);
				this.magic_shield = att["magic_shield"];
				this.attr_list[29u] = att["magic_shield"];
				flag = true;
			}
			bool flag30 = att.ContainsKey("exp_add");
			if (flag30)
			{
				this.attChange_eqp[30u] = (int)(att["exp_add"] - this.exp_add);
				this.exp_add = att["exp_add"];
				this.attr_list[30u] = att["exp_add"];
				flag = true;
			}
			bool flag31 = att.ContainsKey("blessing");
			if (flag31)
			{
				this.attChange_eqp[31u] = (int)(att["blessing"] - this.blessing);
				this.blessing = att["blessing"];
				this.attr_list[31u] = att["blessing"];
				flag = true;
			}
			bool flag32 = att.ContainsKey("knowledge_add");
			if (flag32)
			{
				this.attChange_eqp[32u] = (int)(att["knowledge_add"] - this.knowledge_add);
				this.knowledge_add = att["knowledge_add"];
				this.attr_list[32u] = att["knowledge_add"];
				flag = true;
			}
			bool flag33 = att.ContainsKey("fatal_damage");
			if (flag33)
			{
				this.attChange_eqp[33u] = (int)(att["fatal_damage"] - this.fatal_damage);
				this.fatal_damage = att["fatal_damage"];
				this.attr_list[33u] = att["fatal_damage"];
				flag = true;
			}
			bool flag34 = att.ContainsKey("wisdom");
			if (flag34)
			{
				this.attChange_eqp[34u] = (int)(att["wisdom"] - this.wisdom);
				this.wisdom = att["wisdom"];
				this.attr_list[34u] = att["wisdom"];
				flag = true;
			}
			bool flag35 = att.ContainsKey("fire_def_add");
			if (flag35)
			{
				this.attChange_eqp[35u] = (int)(att["fire_def_add"] - this.fire_def_add);
				this.fire_def_add = att["fire_def_add"];
				this.attr_list[35u] = att["fire_def_add"];
				flag = true;
			}
			bool flag36 = att.ContainsKey("ice_def_add");
			if (flag36)
			{
				this.attChange_eqp[36u] = (int)(att["ice_def_add"] - this.ice_def_add);
				this.ice_def_add = att["ice_def_add"];
				this.attr_list[36u] = att["ice_def_add"];
				flag = true;
			}
			bool flag37 = att.ContainsKey("light_def_add");
			if (flag37)
			{
				this.attChange_eqp[37u] = (int)(att["light_def_add"] - this.light_def_add);
				this.light_def_add = att["light_def_add"];
				this.attr_list[37u] = att["light_def_add"];
				flag = true;
			}
			bool flag38 = att.ContainsKey("min_attack");
			if (flag38)
			{
				this.attChange_eqp[38u] = (int)(att["min_attack"] - this.min_attack);
				this.min_attack = att["min_attack"];
				this.attr_list[38u] = att["min_attack"];
				flag = true;
			}
			bool flag39 = att.ContainsKey("double_damage_rate");
			if (flag39)
			{
				this.attChange_eqp[39u] = (int)(att["double_damage_rate"] - this.double_damage_rate);
				this.double_damage_rate = att["double_damage_rate"];
				this.attr_list[39u] = att["double_damage_rate"];
				flag = true;
			}
			bool flag40 = att.ContainsKey("reflect_crit_rate");
			if (flag40)
			{
				this.attChange_eqp[40u] = (int)(att["reflect_crit_rate"] - this.reflect_crit_rate);
				this.reflect_crit_rate = att["reflect_crit_rate"];
				this.attr_list[40u] = att["reflect_crit_rate"];
				flag = true;
			}
			bool flag41 = att.ContainsKey("ignore_crit_rate");
			if (flag41)
			{
				this.attChange_eqp[41u] = (int)(att["ignore_crit_rate"] - this.ignore_crit_rate);
				this.ignore_crit_rate = att["ignore_crit_rate"];
				this.attr_list[41u] = att["ignore_crit_rate"];
				flag = true;
			}
			bool flag42 = att.ContainsKey("crit_add_hp");
			if (flag42)
			{
				this.attChange_eqp[42u] = (int)(att["crit_add_hp"] - this.crit_add_hp);
				this.crit_add_hp = att["crit_add_hp"];
				this.attr_list[42u] = att["crit_add_hp"];
				flag = true;
			}
			bool flag43 = att.ContainsKey("hit");
			if (flag43)
			{
				this.attChange_eqp[43u] = (int)(att["hit"] - this.hit);
				this.hit = att["hit"];
				this.attr_list[43u] = att["hit"];
				flag = true;
			}
			bool flag44 = att.ContainsKey("dodge");
			if (flag44)
			{
				this.attChange_eqp[44u] = (int)(att["dodge"] - this.dodge);
				this.dodge = att["dodge"];
				this.attr_list[44u] = att["dodge"];
				flag = true;
			}
			bool flag45 = att.ContainsKey("ignore_defense_damage");
			if (flag45)
			{
				this.attChange_eqp[45u] = (int)(att["ignore_defense_damage"] - this.ignore_defense_damage);
				this.ignore_defense_damage = att["ignore_defense_damage"];
				this.attr_list[45u] = att["ignore_defense_damage"];
				flag = true;
			}
			bool flag46 = att.ContainsKey("stagger");
			if (flag46)
			{
				this.attChange_eqp[46u] = (int)(att["stagger"] - this.stagger);
				this.stagger = att["stagger"];
				this.attr_list[46u] = att["stagger"];
				flag = true;
			}
			bool flag47 = att.ContainsKey("pk_v");
			if (flag47)
			{
				this.sinsNub = att["pk_v"];
				this.crime = this.sinsNub;
				this.attr_list[16u] = (int)this.sinsNub;
				flag = true;
			}
			bool flag48 = att.ContainsKey("pk_v");
			if (flag48)
			{
				debug.Log("更新罪恶值：" + this.sinsNub);
				bool flag49 = a3_expbar.instance;
				if (flag49)
				{
					a3_expbar.instance.ShowWashRed();
					bool flag50 = a3_washredname._instance;
					if (flag50)
					{
						a3_washredname._instance.point();
					}
				}
			}
			bool flag51 = att.ContainsKey("equip");
			if (flag51)
			{
				bool flag52 = att["equip"];
				if (flag52)
				{
					Dictionary<uint, int> dictionary = new Dictionary<uint, int>();
					foreach (uint current in this.attChange_eqp.Keys)
					{
						bool flag53 = this.attChange_eqp[current] > 0;
						if (flag53)
						{
							dictionary[current] = this.attChange_eqp[current];
						}
					}
					bool flag54 = dictionary.Count > 0;
					if (flag54)
					{
						a3_attChange.instans.runTxt(dictionary);
					}
				}
			}
			bool flag55 = flag;
			if (flag55)
			{
				base.dispatchEvent(GameEvent.Create(PlayerModel.ON_ATTR_CHANGE, this, null, false));
			}
		}

		public void attPointCheck(Variant att)
		{
			debug.Log(att.dump());
			bool flag = att.ContainsKey("att_pt");
			if (flag)
			{
				this.pt_att = att["att_pt"];
			}
			bool flag2 = att.ContainsKey("strpt");
			if (flag2)
			{
				this.pt_strpt = att["strpt"];
			}
			bool flag3 = att.ContainsKey("conpt");
			if (flag3)
			{
				this.pt_conpt = att["conpt"];
			}
			bool flag4 = att.ContainsKey("intept");
			if (flag4)
			{
				this.pt_intept = att["intept"];
			}
			bool flag5 = att.ContainsKey("wispt");
			if (flag5)
			{
				this.pt_wispt = att["wispt"];
			}
			bool flag6 = att.ContainsKey("agipt");
			if (flag6)
			{
				this.pt_agipt = att["agipt"];
			}
			bool flag7 = att.ContainsKey("rednm");
			if (flag7)
			{
				this.now_nameState = att["rednm"];
			}
			switch (this.now_nameState)
			{
			case 0:
				this.name_state = REDNAME_TYPE.RNT_NORMAL;
				break;
			case 1:
				this.name_state = REDNAME_TYPE.RNT_RASCAL;
				break;
			case 2:
				this.name_state = REDNAME_TYPE.RNT_EVIL;
				break;
			case 3:
				this.name_state = REDNAME_TYPE.RNT_DEVIL;
				break;
			}
			bool flag8 = SelfRole._inst != null;
			if (flag8)
			{
				SelfRole._inst.refreshnamecolor(this.now_nameState);
			}
		}

		public void LeaveStandalone_CreateChar()
		{
			SelfRole.s_bStandaloneScene = false;
			skillbar expr_0C = skillbar.instance;
			if (expr_0C != null)
			{
				GameObject expr_1C = expr_0C.getGameObjectByPath("combat/apbtn");
				if (expr_1C != null)
				{
					expr_1C.SetActive(true);
				}
			}
			UIClient.instance.dispatchEvent(GameEvent.Create(4033u, this, GameTools.createGroup(new Variant[]
			{
				"cid",
				this.cid
			}), false));
			UIClient.instance.dispatchEvent(GameEvent.Create(4034u, this, GameTools.createGroup(new Variant[]
			{
				"cid",
				this.cid
			}), false));
		}

		public void lvUp(Variant data)
		{
			debug.Log(":::::" + data.dump());
			bool flag = data.ContainsKey("zhuan");
			if (flag)
			{
				this.olduplvl = this.up_lvl;
				this.up_lvl = data["zhuan"];
				welfareProxy.b_zhuan = ModelBase<WelfareModel>.getInstance().for_dengjilibao(BaseProxy<welfareProxy>.getInstance().dengjijiangli);
				BaseProxy<welfareProxy>.getInstance().showIconLight();
				BaseProxy<EliteMonsterProxy>.getInstance().SendProxy();
			}
			bool flag2 = data.ContainsKey("lvl");
			if (flag2)
			{
				BaseProxy<EliteMonsterProxy>.getInstance().SendProxy();
				bool flag3 = data["lvl"] > this.lvl;
				if (flag3)
				{
					this.lvl = data["lvl"];
					this.oldLv = this.lvl;
					bool flag4 = SelfRole._inst != null;
					if (flag4)
					{
						SelfRole._inst.ShowLvUpFx();
					}
					bool flag5 = data["zhuan"] <= this.olduplvl;
					if (flag5)
					{
						FunctionOpenMgr.instance.onLvUp((int)this.up_lvl, (int)this.lvl, true);
					}
					bool flag6 = a3_funcopen.instance != null;
					if (flag6)
					{
						bool flag7 = !a3_funcopen.instance.is_show;
						if (flag7)
						{
							bool flag8 = a3_lvup.instance != null;
							if (flag8)
							{
								a3_lvup.instance.refreshInfo(this.lvl);
							}
						}
					}
					base.dispatchEvent(GameEvent.Create(PlayerModel.ON_LEVEL_CHANGED, this, this.lvl, false));
				}
				else
				{
					this.lvl = data["lvl"];
				}
				MediaClient.instance.PlaySoundUrl("audio/common/levelup", false, null);
				GameSdkMgr.record_LvlUp();
			}
			this.olduplvl = this.up_lvl;
			bool flag9 = data.ContainsKey("combpt");
			if (flag9)
			{
				this.combpt = data["combpt"];
				base.dispatchEvent(GameEvent.Create(PlayerModel.ON_ATTR_CHANGE, this, null, false));
			}
			bool flag10 = data.ContainsKey("pinfo");
			if (flag10)
			{
				Variant variant = data["pinfo"];
				this.exp = variant["exp"];
				this.hp = variant["hp"];
				this.combpt = variant["combpt"];
				bool flag11 = variant.ContainsKey("battleAttrs");
				if (flag11)
				{
					ModelBase<PlayerModel>.getInstance().attrChange(variant);
					base.dispatchEvent(GameEvent.Create(PlayerModel.ON_ATTR_CHANGE, this, null, false));
				}
			}
			InterfaceMgr.doCommandByLua("PlayerModel:getInstance().modInfo", "model/PlayerModel", new object[]
			{
				data
			});
		}

		private void autoAddPrint()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			bool flag = ModelBase<PlayerModel>.getInstance().profession == 2;
			if (flag)
			{
				int[] addtype = new int[]
				{
					4,
					3,
					1,
					5
				};
				int[] tem = new int[]
				{
					2,
					2,
					1,
					1
				};
				this.addPointAuto(this._pt_att, addtype, tem);
			}
			bool flag2 = ModelBase<PlayerModel>.getInstance().profession == 3;
			if (flag2)
			{
				int[] addtype2 = new int[]
				{
					2,
					1,
					4,
					5
				};
				int[] tem2 = new int[]
				{
					2,
					1,
					2,
					1
				};
				this.addPointAuto(this._pt_att, addtype2, tem2);
			}
			bool flag3 = ModelBase<PlayerModel>.getInstance().profession == 5;
			if (flag3)
			{
				int[] addtype3 = new int[]
				{
					3,
					4,
					1,
					5
				};
				int[] tem3 = new int[]
				{
					2,
					2,
					1,
					1
				};
				this.addPointAuto(this._pt_att, addtype3, tem3);
			}
			foreach (int current in this.cur_att_pt.Keys)
			{
				bool flag4 = this.cur_att_pt[current] > 0;
				if (flag4)
				{
					dictionary[current] = this.cur_att_pt[current];
				}
			}
			bool flag5 = dictionary.Count > 0;
			if (flag5)
			{
				BaseProxy<PlayerInfoProxy>.getInstance().sendAddPoint(0, dictionary);
			}
		}

		private void addPointAuto(int left_num, int[] addtype, int[] tem)
		{
			int[] array = new int[4];
			int num = 0;
			for (int i = 0; i < tem.Length; i++)
			{
				num += tem[i];
			}
			int num2 = (int)Math.Floor((double)left_num / (double)num);
			int num3 = left_num % num;
			for (int j = 0; j < 4; j++)
			{
				array[j] = tem[j] * num2;
			}
			bool flag = num3 > 0;
			if (flag)
			{
				int num4 = num3;
				for (int k = 0; k < 4; k++)
				{
					bool flag2 = num4 >= tem[k];
					if (flag2)
					{
						array[k] += tem[k];
						num4 -= tem[k];
					}
					else
					{
						array[k] += num4;
						num4 = 0;
					}
					bool flag3 = num4 <= 0;
					if (flag3)
					{
						break;
					}
				}
			}
			for (int l = 0; l < 4; l++)
			{
				this.cur_att_pt[addtype[l]] = array[l];
			}
		}

		public void isShowVipUpLayer()
		{
			bool flag = this.lvl > this.oldLv;
			if (flag)
			{
				bool flag2 = muLGClient.instance.g_mapCT.curMapId == 1u;
				if (flag2)
				{
					this.oldLv = this.lvl;
					InterfaceMgr.getInstance().open(InterfaceMgr.UPLEVEL, null, false);
				}
			}
		}

		public void vipChange(Variant data)
		{
		}

		public void titileChange(Variant data)
		{
			bool flag = data["title"] > 0;
			if (flag)
			{
				this.istitleActive = true;
			}
			else
			{
				this.istitleActive = false;
			}
			bool flag2 = SelfRole._inst != null;
			if (flag2)
			{
				SelfRole._inst.refreshtitle(data["title"]);
			}
		}

		public void titleShoworHide(Variant data)
		{
			this.istitleActive = data["title_display"]._bool;
			bool @bool = data["title_display"]._bool;
			if (@bool)
			{
				bool flag = a3_RankModel.now_id > 0;
				if (flag)
				{
					bool flag2 = SelfRole._inst != null;
					if (flag2)
					{
						SelfRole._inst.refreshtitle(a3_RankModel.now_id);
					}
				}
				else
				{
					this.istitleActive = false;
				}
			}
		}

		private void initProxy()
		{
			BaseProxy<a3_RuneStoneProxy>.getInstance().sendporxy(5, 0, 0, null);
			BaseProxy<A3_BeStrongerProxy>.getInstance();
			BaseProxy<A3_LegionProxy>.getInstance().SendGetInfo();
			BaseProxy<A3_TaskProxy>.getInstance().SendGetTask();
			BaseProxy<A3_RankProxy>.getInstance().sendProxy(1u, -1, false);
			BaseProxy<A3_WingProxy>.getInstance().GetWings();
			BaseProxy<A3_MailProxy>.getInstance().GetMails();
			BaseProxy<A3_NPCShopProxy>.getInstance().sendShowAll();
			BaseProxy<a3_dartproxy>.getInstance().sendDartGo();
			BaseProxy<A3_ygyiwuProxy>.getInstance().SendYGinfo(1u);
			BaseProxy<EliteMonsterProxy>.getInstance().SendProxy();
			BaseProxy<A3_SmithyProxy>.getInstance();
			BaseProxy<BagProxy>.getInstance().sendLoadItems(0);
			BaseProxy<BagProxy>.getInstance().sendLoadItems(1);
			BaseProxy<SkillProxy>.getInstance();
			BaseProxy<GeneralProxy>.getInstance();
			BaseProxy<FindBestoProxy>.getInstance();
			BaseProxy<Skill_a3Proxy>.getInstance();
			BaseProxy<A3_ygyiwuProxy>.getInstance();
			BaseProxy<a3_PkmodelProxy>.getInstance();
			BaseProxy<A3_VipProxy>.getInstance().GetVip();
			BaseProxy<A3_HudunProxy>.getInstance().sendinfo(0);
			BaseProxy<a3_activeDegreeProxy>.getInstance().SendGetPoint(1);
			BaseProxy<TeamProxy>.getInstance();
			ContMgr.init();
			BaseProxy<OffLineExpProxy>.getInstance();
			BaseProxy<LevelProxy>.getInstance().sendGet_lvl_cnt_info(1, 0, 0);
			BaseProxy<A3_SummonProxy>.getInstance().sendLoadSummons();
			BaseProxy<A3_ActiveProxy>.getInstance().SendGetHuntInfo();
			bool flag = HttpAppMgr.instance == null && Globle.DebugMode == 2;
			if (flag)
			{
				HttpAppMgr.init();
			}
			ModelBase<E_mailModel>.getInstance().init();
			bool flag2 = HttpAppMgr.instance != null;
			if (flag2)
			{
				HttpAppMgr.instance.initGift();
			}
		}

		public bool checkPK()
		{
			return this._pkState && !this.inDefendArea;
		}

		internal void refreshByChangeMap(Variant msgData)
		{
			this.modHp(msgData["hp"]);
			bool flag = msgData.ContainsKey("mp");
			if (flag)
			{
				this.modMp(msgData["mp"]);
			}
			this.iid = msgData["iid"];
			bool flag2 = msgData.ContainsKey("face");
			if (flag2)
			{
				this.mapBeginroatate = msgData["face"];
			}
			this.mapBeginX = msgData["x"] / 53.333f;
			this.mapBeginY = msgData["y"] / 53.333f;
			bool flag3 = msgData.ContainsKey("mpid");
			if (flag3)
			{
				this.mapid = msgData["mpid"];
			}
		}

		public uint GetNeedExp(uint a_zhuan, uint a_lvl, uint a_exp, uint b_zhuan, uint b_lvl, uint b_exp)
		{
			SXML sXML = XMLMgr.instance.GetSXML("carrlvl", "");
			uint num = 0u;
			for (uint num2 = a_zhuan; num2 < b_zhuan; num2 += 1u)
			{
				List<SXML> nodeList = sXML.GetNode("carr", "carr==" + this.profession).GetNode("zhuanshen", "zhuan==" + num2).GetNodeList("carr", "");
				foreach (SXML current in nodeList)
				{
					int @int = current.getInt("lvl");
					bool flag = (long)@int >= (long)((ulong)a_lvl);
					if (flag)
					{
						num += current.getUint("exp");
					}
				}
			}
			bool flag2 = b_zhuan > a_zhuan;
			if (flag2)
			{
				List<SXML> nodeList2 = sXML.GetNode("carr", "carr==" + this.profession).GetNode("zhuanshen", "zhuan==" + b_zhuan).GetNodeList("carr", "");
				foreach (SXML current2 in nodeList2)
				{
					int int2 = current2.getInt("lvl");
					bool flag3 = (long)int2 < (long)((ulong)a_lvl);
					if (flag3)
					{
						num += current2.getUint("exp");
					}
				}
			}
			num -= a_exp;
			num += b_exp;
			return (num > 0u) ? num : 0u;
		}
	}
}
