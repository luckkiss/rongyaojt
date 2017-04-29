using GameFramework;
using MuGame;
using System;
using UnityEngine;

internal class MDC000 : MonsterRole
{
	public string escort_name = string.Empty;

	private Animator ani = new Animator();

	private bool show = false;

	public override void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f)
	{
		base.Init(prefab_path, layer, pos, roatate);
		M00000_Default_Event m00000_Default_Event = this.m_curModel.gameObject.AddComponent<M00000_Default_Event>();
		m00000_Default_Event.m_monRole = this;
		this.ani = this.m_curModel.gameObject.GetComponent<Animator>();
		AnimatorStateInfo currentAnimatorStateInfo = this.ani.GetCurrentAnimatorStateInfo(0);
		BaseProxy<a3_dartproxy>.getInstance().addEventListener(4u, new Action<GameEvent>(PlayerNameUIMgr.getInstance().carinfo));
	}

	public override void FrameMove(float delta_time)
	{
		base.FrameMove(delta_time);
		bool flag = !this.show;
		if (flag)
		{
			SXML sXML = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + this.dartid);
			this.ani.speed = sXML.GetNodeList("att", "")[0].getFloat("speed_run");
			this.show = true;
		}
	}
}
