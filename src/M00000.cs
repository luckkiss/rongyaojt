using MuGame;
using System;
using UnityEngine;

public class M00000 : MonsterRole
{
	public bool isCollect = false;

	public override void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f)
	{
		this.m_fNavStoppingDis = 2f;
		base.Init(prefab_path, layer, pos, roatate);
		M00000_Default_Event m00000_Default_Event = this.m_curModel.gameObject.AddComponent<M00000_Default_Event>();
		PlayerNameUIMgr.getInstance().show(this);
		m00000_Default_Event.m_monRole = this;
		base.maxHp = (base.curhp = 1000);
		bool flag = this.isCollect;
		if (flag)
		{
			this.m_curPhy.gameObject.layer = EnumLayer.LM_COLLECT;
		}
	}
}
