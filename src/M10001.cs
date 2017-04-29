using System;
using UnityEngine;

public class M10001 : MonsterRole
{
	public override void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f)
	{
		this.m_fNavStoppingDis = 2f;
		base.Init(prefab_path, layer, pos, roatate);
		M10001_Dyzhs_Event m10001_Dyzhs_Event = this.m_curModel.gameObject.AddComponent<M10001_Dyzhs_Event>();
		m10001_Dyzhs_Event.m_monRole = this;
		base.maxHp = (base.curhp = 400);
	}
}
