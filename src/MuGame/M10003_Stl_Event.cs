using System;
using UnityEngine;

namespace MuGame
{
	internal class M10003_Stl_Event : Monster_Base_Event
	{
		public M10003 m_StlRole;

		public void onBullet(int id)
		{
			bool flag = id == 1;
			if (flag)
			{
				GameObject original = Resources.Load<GameObject>("bullet/10003/bt1/b" + id.ToString());
				Vector3 position = base.transform.position;
				GameObject gameObject = UnityEngine.Object.Instantiate(original, position, base.transform.rotation) as GameObject;
				gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
				Transform transform = gameObject.transform.FindChild("t/b/bt0");
				bool flag2 = transform != null;
				if (flag2)
				{
					HitData hitData = transform.gameObject.AddComponent<HitData>();
					hitData.m_CastRole = this.m_StlRole;
					hitData.m_vBornerPos = this.m_StlRole.m_curModel.position;
					hitData.m_ePK_Type = PK_TYPE.PK_LEGION;
					hitData.m_unPK_Param = this.m_StlRole.m_unLegionID;
					hitData.m_nDamage = 1888;
					hitData.m_nHitType = id;
					hitData.m_bOnlyHit = false;
					transform.gameObject.layer = EnumLayer.LM_BT_FIGHT;
				}
				for (int i = 1; i < 7; i++)
				{
					Transform transform2 = gameObject.transform.FindChild("t/b/bt" + i.ToString());
					bool flag3 = transform2 != null;
					if (flag3)
					{
						HitData hitData2 = transform2.gameObject.AddComponent<HitData>();
						hitData2.m_CastRole = this.m_StlRole;
						hitData2.m_vBornerPos = this.m_StlRole.m_curModel.position;
						hitData2.m_ePK_Type = PK_TYPE.PK_LEGION;
						hitData2.m_unPK_Param = this.m_monRole.m_unLegionID;
						hitData2.m_nDamage = 488;
						hitData2.m_nHitType = id;
						hitData2.m_bOnlyHit = false;
						transform2.gameObject.layer = EnumLayer.LM_BT_FIGHT;
					}
				}
				UnityEngine.Object.Destroy(gameObject, 4f);
			}
		}

		private void onSFX(int id)
		{
			bool flag = 1 == id;
			if (flag)
			{
				this.m_StlRole.PlayFire();
			}
		}
	}
}
