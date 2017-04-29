using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class RoleMgr
	{
		public static RoleMgr _instance = new RoleMgr();

		private GameObject healEff;

		public BaseRole getRole(uint iid)
		{
			bool flag = SelfRole._inst != null && iid == SelfRole._inst.m_unIID;
			BaseRole result;
			if (flag)
			{
				result = SelfRole._inst;
			}
			else
			{
				BaseRole baseRole = OtherPlayerMgr._inst.GetOtherPlayer(iid);
				bool flag2 = baseRole == null;
				if (flag2)
				{
					baseRole = MonsterMgr._inst.getServerMonster(iid);
				}
				result = baseRole;
			}
			return result;
		}

		public void onMonsterHate(uint iid, uint hateiid = 0u)
		{
		}

		public void clear()
		{
			LGMonsters.instacne.clear();
		}

		public void AddStates(Variant states)
		{
		}

		public void onAttchange(Variant msgData)
		{
			debug.Log("onAttchange::" + msgData.dump());
			BaseRole role = this.getRole(msgData["iid"]);
			bool flag = role == null;
			if (!flag)
			{
				bool isMain = role.m_isMain;
				bool flag2 = msgData.ContainsKey("hpchange");
				if (flag2)
				{
					Variant variant = msgData["hpchange"];
					int num = variant["hpchange"];
					int num2 = variant["hp_left"];
					Variant variant2 = new Variant();
					bool flag3 = isMain;
					if (flag3)
					{
						ModelBase<PlayerModel>.getInstance().modHp(num2);
					}
					bool flag4 = num > 0;
					if (flag4)
					{
						role.modHp(num2);
						bool flag5 = isMain;
						if (flag5)
						{
							FightText.play(FightText.HEAL_TEXT, role.getHeadPos(), num, false, -1);
						}
						bool flag6 = this.healEff == null;
						if (flag6)
						{
							this.healEff = Resources.Load<GameObject>("FX/comFX/fuwenFX/FX_fuwen_chuyong");
						}
						bool flag7 = this.healEff != null && role is ProfessionRole;
						if (flag7)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.healEff);
							UnityEngine.Object.Destroy(gameObject, 1f);
							gameObject.transform.SetParent(role.m_curModel, false);
						}
						bool flag8 = msgData.ContainsKey("rune_ids");
						if (flag8)
						{
							List<Variant> arr = msgData["rune_ids"]._arr;
							foreach (Variant current in arr)
							{
								FightText.play(FightText.ADD_IMG_TEXT, role.getHeadPos(), num, false, current);
							}
						}
					}
					else
					{
						bool flag9 = num < 0;
						if (flag9)
						{
							uint iid = variant["frm_iid"];
							BaseRole role2 = RoleMgr._instance.getRole(iid);
							Variant variant3 = msgData["rune_ids"];
							int isCrit = variant3[0];
							bool flag10 = msgData.ContainsKey("rune_ids");
							if (flag10)
							{
								List<Variant> arr2 = msgData["rune_ids"]._arr;
								foreach (Variant current2 in arr2)
								{
									role.onServerHurt(-num, num2, variant["die"], role2, isCrit, false, false);
								}
							}
						}
					}
				}
				bool flag11 = msgData.ContainsKey("mpchange");
				if (flag11)
				{
					Variant variant4 = msgData["mpchange"];
					int num3 = variant4["mpchange"];
					int mprest = variant4["mp_left"];
					bool flag12 = isMain;
					if (flag12)
					{
						ModelBase<PlayerModel>.getInstance().modMp(mprest);
					}
				}
				bool flag13 = msgData.ContainsKey("pk_state");
				if (flag13)
				{
					switch (msgData["pk_state"])
					{
					case 0:
						role.m_ePK_Type = PK_TYPE.PK_PEACE;
						break;
					case 1:
						role.m_ePK_Type = PK_TYPE.PK_PKALL;
						break;
					case 2:
						role.m_ePK_Type = PK_TYPE.PK_TEAM;
						break;
					case 3:
						role.m_ePK_Type = PK_TYPE.PK_LEGION;
						break;
					case 4:
						role.m_ePK_Type = PK_TYPE.PK_HERO;
						break;
					}
				}
				bool flag14 = msgData.ContainsKey("clanid");
				if (flag14)
				{
					role.m_unLegionID = msgData["clanid"];
				}
				bool flag15 = msgData.ContainsKey("teamid");
				if (flag15)
				{
					role.m_unTeamID = msgData["teamid"];
				}
				bool flag16 = msgData.ContainsKey("rune_ids");
				if (flag16)
				{
					List<Variant> arr3 = msgData["rune_ids"]._arr;
					foreach (Variant current3 in arr3)
					{
						GameObject runeEff = EffMgr.getRuneEff(current3._int);
						bool flag17 = runeEff != null;
						if (flag17)
						{
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(runeEff);
							UnityEngine.Object.Destroy(gameObject2, 2f);
							gameObject2.transform.SetParent(role.m_curModel, false);
						}
					}
				}
				bool flag18 = msgData.ContainsKey("sprite_flag");
				if (flag18)
				{
					uint num4 = msgData["sprite_flag"];
					uint iid2 = msgData["iid"];
					MonsterRole monster = MonsterMgr._inst.getMonster(iid2);
					bool flag19 = monster != null;
					if (flag19)
					{
						SkinnedMeshRenderer component = monster.m_curModel.FindChild("body").GetComponent<SkinnedMeshRenderer>();
						uint num5 = num4;
						if (num5 != 0u)
						{
							if (num5 == 1u)
							{
								component.sharedMaterial = Resources.Load<Material>("default/monster_1021_heite_gold");
							}
						}
						else
						{
							Material[] sharedMaterials = component.sharedMaterials;
							for (int i = 0; i < sharedMaterials.Length; i++)
							{
								Material material = sharedMaterials[i];
								material.shader = Shader.Find("A3/A3_Char_Streamer_H");
								material.SetColor("_RimColor", new Color(0f, 0f, 0f, 0f));
								material.SetFloat("_RimWidth", 0f);
							}
						}
					}
				}
			}
		}

		public static void ClearMark(bool clearAnyway = false, PK_TYPE pkState = PK_TYPE.PK_PEACE, Func<BaseRole, bool> filterHandle = null)
		{
			int i = 0;
			Dictionary<uint, MonsterRole> mapMonster = MonsterMgr._inst.m_mapMonster;
			Dictionary<uint, ProfessionRole> mapOtherPlayer = OtherPlayerMgr._inst.m_mapOtherPlayer;
			switch (pkState)
			{
			case PK_TYPE.PK_PKALL:
			{
				List<uint> list = new List<uint>(mapOtherPlayer.Keys);
				while (i < list.Count)
				{
					bool flag = i == list.Count - 1 | clearAnyway;
					if (flag)
					{
						for (int j = 0; j < list.Count; j++)
						{
							mapOtherPlayer[list[j]].m_isMarked = false;
						}
						break;
					}
					bool flag2 = !mapOtherPlayer[list[i]].m_isMarked && (filterHandle == null || filterHandle(mapOtherPlayer[list[i]]));
					if (flag2)
					{
						break;
					}
					i++;
				}
				break;
			}
			case PK_TYPE.PK_TEAM:
			{
				List<uint> list2 = new List<uint>(mapOtherPlayer.Keys);
				while (i < list2.Count)
				{
					bool flag3 = i == list2.Count - 1 | clearAnyway;
					if (flag3)
					{
						for (int k = 0; k < list2.Count; k++)
						{
							mapOtherPlayer[list2[k]].m_isMarked = false;
						}
						break;
					}
					bool flag4 = !mapOtherPlayer[list2[i]].m_isMarked && (filterHandle == null || filterHandle(mapOtherPlayer[list2[i]]));
					if (flag4)
					{
						break;
					}
					i++;
				}
				break;
			}
			}
			i = 0;
			List<uint> list3 = new List<uint>(mapMonster.Keys);
			while (i < list3.Count)
			{
				bool flag5 = i == list3.Count - 1 | clearAnyway;
				if (flag5)
				{
					for (int l = 0; l < list3.Count; l++)
					{
						mapMonster[list3[l]].m_isMarked = false;
					}
					break;
				}
				bool flag6 = !mapMonster[list3[i]].m_isMarked && (filterHandle == null || filterHandle(mapMonster[list3[i]]));
				if (flag6)
				{
					break;
				}
				i++;
			}
		}
	}
}
