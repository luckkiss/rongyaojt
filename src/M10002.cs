using MuGame;
using System;
using UnityEngine;

public class M10002 : MonsterRole
{
	public override void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f)
	{
		this.m_fNavStoppingDis = 1.5f;
		base.Init(prefab_path, layer, pos, roatate);
		M10002_Srg_Event m10002_Srg_Event = this.m_curModel.gameObject.AddComponent<M10002_Srg_Event>();
		m10002_Srg_Event.m_monRole = this;
		base.maxHp = (base.curhp = 200);
	}
}
