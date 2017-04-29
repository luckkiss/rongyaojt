using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class PkmodelAdmin
	{
		private static int pkstat = -1;

		private static MeshRenderer skin = null;

		private static BaseRole br = null;

		public static void RefreshList(List<uint> oldList, List<uint> isProfession)
		{
			switch (ModelBase<PlayerModel>.getInstance().now_pkState)
			{
			case 0:
				for (int i = 0; i < isProfession.Count; i++)
				{
					bool flag = oldList.Contains(isProfession[i]);
					if (flag)
					{
						oldList.Remove(isProfession[i]);
					}
				}
				break;
			case 4:
				for (int j = 0; j < isProfession.Count; j++)
				{
					bool flag2 = OtherPlayerMgr._inst != null && OtherPlayerMgr._inst.m_mapOtherPlayer.Count > 0;
					if (flag2)
					{
						bool flag3 = OtherPlayerMgr._inst.m_mapOtherPlayer.ContainsKey(isProfession[j]);
						if (flag3)
						{
							bool flag4 = OtherPlayerMgr._inst.m_mapOtherPlayer[isProfession[j]].rednm <= 0;
							if (flag4)
							{
								oldList.Remove(isProfession[j]);
							}
						}
					}
				}
				break;
			}
		}

		public static void RefreshShow(BaseRole LockRole, bool havepeopleLv = false, bool havpeoplerednam = false)
		{
			bool flag = PkmodelAdmin.br == LockRole && ModelBase<PlayerModel>.getInstance().now_pkState == PkmodelAdmin.pkstat && PkmodelAdmin.skin != null && !havepeopleLv && !havpeoplerednam;
			if (!flag)
			{
				PkmodelAdmin.br = LockRole;
				PkmodelAdmin.skin = SelfRole.s_LockFX.gameObject.GetComponent<MeshRenderer>();
				bool flag2 = LockRole is ProfessionRole;
				if (flag2)
				{
					bool flag3 = OtherPlayerMgr._inst.m_mapOtherPlayer.ContainsKey(LockRole.m_unIID) && OtherPlayerMgr._inst.m_mapOtherPlayer[LockRole.m_unIID].zhuan < 1;
					if (flag3)
					{
						PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(0f, 1f, 0f, 1f));
						PkmodelAdmin.pkstat = ModelBase<PlayerModel>.getInstance().now_pkState;
						return;
					}
					bool flag4 = !OtherPlayerMgr._inst.m_mapOtherPlayer.ContainsKey(LockRole.m_unIID);
					if (flag4)
					{
						SelfRole._inst.m_LockRole = null;
					}
				}
				else
				{
					bool flag5 = LockRole is MS0000;
					if (flag5)
					{
						bool flag6 = (long)((MS0000)LockRole).owner_cid == (long)((ulong)ModelBase<PlayerModel>.getInstance().cid);
						if (flag6)
						{
							PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(0f, 1f, 0f, 1f));
						}
						else
						{
							PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(1f, 0f, 0f, 1f));
						}
						return;
					}
					bool flag7 = LockRole is MDC000;
					if (flag7)
					{
						bool flag8 = ((MDC000)LockRole).escort_name == ModelBase<A3_LegionModel>.getInstance().myLegion.clname;
						if (flag8)
						{
							PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(0f, 1f, 0f, 1f));
						}
						else
						{
							bool flag9 = (float)LockRole.curhp / (float)LockRole.maxHp * 100f <= 20f;
							if (flag9)
							{
								PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(0f, 1f, 0f, 1f));
							}
							else
							{
								PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(1f, 0f, 0f, 1f));
							}
						}
						return;
					}
					bool flag10 = LockRole is MonsterRole;
					if (flag10)
					{
						PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(1f, 0f, 0f, 1f));
						PkmodelAdmin.pkstat = ModelBase<PlayerModel>.getInstance().now_pkState;
						return;
					}
				}
				switch (ModelBase<PlayerModel>.getInstance().now_pkState)
				{
				case 0:
				{
					bool flag11 = LockRole is ProfessionRole;
					if (flag11)
					{
						PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(0f, 1f, 0f, 1f));
					}
					else
					{
						bool flag12 = LockRole == null;
						if (flag12)
						{
							PkmodelAdmin.skin = null;
						}
					}
					break;
				}
				case 1:
					PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(1f, 0f, 0f, 1f));
					break;
				case 4:
				{
					bool flag13 = LockRole == null;
					if (flag13)
					{
						PkmodelAdmin.skin = null;
					}
					else
					{
						bool flag14 = LockRole.rednm > 0;
						if (flag14)
						{
							PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(1f, 0f, 0f, 1f));
						}
						else
						{
							PkmodelAdmin.skin.material.SetColor(EnumShader.SPI_TINT_COLOR, new Color(0f, 1f, 0f, 1f));
						}
					}
					break;
				}
				}
				PkmodelAdmin.pkstat = ModelBase<PlayerModel>.getInstance().now_pkState;
			}
		}

		public static BaseRole RefreshLockRoleTransform(BaseRole LockRole)
		{
			bool flag = LockRole is MonsterRole;
			BaseRole result;
			if (flag)
			{
				result = LockRole;
			}
			else
			{
				bool flag2 = LockRole.isDead || LockRole == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					switch (ModelBase<PlayerModel>.getInstance().now_pkState)
					{
					case 0:
						result = null;
						return result;
					case 1:
					{
						bool flag3 = LockRole is ProfessionRole;
						if (!flag3)
						{
							result = LockRole;
							return result;
						}
						bool flag4 = OtherPlayerMgr._inst.m_mapOtherPlayer[LockRole.m_unIID].zhuan < 1;
						if (flag4)
						{
							result = null;
							return result;
						}
						break;
					}
					case 4:
					{
						bool flag5 = LockRole is ProfessionRole && LockRole.rednm > 0;
						if (flag5)
						{
							result = LockRole;
							return result;
						}
						result = null;
						return result;
					}
					}
					result = LockRole;
				}
			}
			return result;
		}

		public static bool RefreshLockSkill(BaseRole LockRole)
		{
			bool flag = LockRole.isDead || LockRole == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = LockRole is MonsterRole;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = OtherPlayerMgr._inst.m_mapOtherPlayer[LockRole.m_unIID].zhuan < 1;
					if (flag3)
					{
						result = false;
					}
					else
					{
						switch (ModelBase<PlayerModel>.getInstance().pk_state)
						{
						case PK_TYPE.PK_PEACE:
							result = false;
							return result;
						case PK_TYPE.PK_PKALL:
							result = true;
							return result;
						case PK_TYPE.PK_TEAM:
						{
							bool arg_E3_0;
							if (LockRole is ProfessionRole)
							{
								ItemTeamMemberData expr_C3 = BaseProxy<TeamProxy>.getInstance().MyTeamData;
								arg_E3_0 = (expr_C3 == null || expr_C3.itemTeamDataList.Exists((ItemTeamData m) => m.cid == LockRole.m_unCID));
							}
							else
							{
								arg_E3_0 = false;
							}
							result = arg_E3_0;
							return result;
						}
						case PK_TYPE.PK_HERO:
						{
							bool flag4 = LockRole.rednm > 0;
							result = flag4;
							return result;
						}
						}
						result = false;
					}
				}
			}
			return result;
		}

		public static BaseRole RefreshLockRole()
		{
			BaseRole result;
			switch (ModelBase<PlayerModel>.getInstance().now_pkState)
			{
			case 0:
				result = null;
				return result;
			case 1:
				result = OtherPlayerMgr._inst.FindNearestEnemyOne(SelfRole._inst.m_curModel.position, false, null, false, PK_TYPE.PK_PEACE);
				return result;
			case 4:
				result = OtherPlayerMgr._inst.FindNearestEnemyOne(SelfRole._inst.m_curModel.position, true, null, false, PK_TYPE.PK_PEACE);
				return result;
			}
			result = null;
			return result;
		}
	}
}
