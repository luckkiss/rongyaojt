using MuGame;
using System;
using UnityEngine;

public class BaseHurt : MonoBehaviour
{
	protected bool CanHited(BaseRole br, HitData hd)
	{
		bool result = false;
		bool flag = hd.m_CastRole is ProfessionRole && hd.m_CastRole.m_isMain;
		bool flag2;
		if (flag)
		{
			hd.m_ePK_Type = ModelBase<PlayerModel>.getInstance().pk_state;
			flag2 = true;
		}
		else
		{
			flag2 = false;
		}
		switch (hd.m_ePK_Type)
		{
		case PK_TYPE.PK_PEACE:
		{
			bool flag3 = br is MonsterRole;
			if (flag3)
			{
				result = true;
			}
			break;
		}
		case PK_TYPE.PK_PKALL:
		{
			bool flag4 = br is MonsterRole;
			if (flag4)
			{
				result = true;
			}
			else
			{
				bool flag5 = br is ProfessionRole;
				if (flag5)
				{
					bool flag6 = flag2;
					if (flag6)
					{
						bool flag7 = br.m_unCID > 0u;
						if (flag7)
						{
							result = true;
						}
					}
					else
					{
						bool flag8 = br.m_unCID != hd.m_unPK_Param;
						if (flag8)
						{
							result = true;
						}
					}
				}
			}
			break;
		}
		case PK_TYPE.PK_TEAM:
		{
			bool flag9 = br is MonsterRole;
			if (flag9)
			{
				result = true;
			}
			else
			{
				bool flag10 = br is ProfessionRole;
				if (flag10)
				{
					bool flag11 = flag2;
					if (flag11)
					{
						bool flag12 = br.m_unCID != 0u && br.m_unCID != ModelBase<PlayerModel>.getInstance().m_unPK_Param && br.m_unCID != ModelBase<PlayerModel>.getInstance().m_unPK_Param2;
						if (flag12)
						{
							result = true;
						}
					}
					else
					{
						bool flag13 = br.m_unCID != hd.m_unPK_Param;
						if (flag13)
						{
							result = true;
						}
					}
				}
			}
			break;
		}
		case PK_TYPE.PK_LEGION:
		{
			bool flag14 = br is MonsterRole;
			if (flag14)
			{
				result = true;
			}
			else
			{
				bool flag15 = br is ProfessionRole;
				if (flag15)
				{
					bool flag16 = flag2;
					if (flag16)
					{
						bool flag17 = br.m_unCID != 0u && br.m_unCID != ModelBase<PlayerModel>.getInstance().m_unPK_Param && br.m_unCID != ModelBase<PlayerModel>.getInstance().m_unPK_Param2;
						if (flag17)
						{
							result = true;
						}
					}
					else
					{
						bool flag18 = br.m_unCID != hd.m_unPK_Param;
						if (flag18)
						{
							result = true;
						}
					}
				}
			}
			break;
		}
		case PK_TYPE.PK_HERO:
		{
			bool flag19 = br is MonsterRole;
			if (flag19)
			{
				result = true;
			}
			else
			{
				bool flag20 = br is ProfessionRole;
				if (flag20)
				{
					bool flag21 = br.rednm > 0;
					result = flag21;
				}
			}
			break;
		}
		}
		return result;
	}
}
