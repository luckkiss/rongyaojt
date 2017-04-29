using Cross;
using MuGame;
using System;
using UnityEngine;

internal class MS0000 : MonsterRole
{
	public int owner_cid = 0;

	public bool ismapEffect = false;

	public Vector3 effectVec;

	public override void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f)
	{
		this.m_fNavStoppingDis = 2f;
		base.Init(prefab_path, layer, pos, roatate);
		MS0000_Default_Event mS0000_Default_Event = this.m_curModel.gameObject.AddComponent<MS0000_Default_Event>();
		mS0000_Default_Event.m_monRole = this;
		base.maxHp = (base.curhp = 1000);
		Variant variant = new Variant();
		variant["cur"] = 100;
		variant["max"] = 100;
		PlayerNameUIMgr.getInstance().refreshHp(this, variant);
	}

	public override void PlaySkill(int id)
	{
		Debug.Log("PlaySkill:   " + id);
		MS0000_Default_Event component = this.m_curModel.gameObject.GetComponent<MS0000_Default_Event>();
		component.effect = null;
		bool flag = id != 1;
		if (flag)
		{
			bool flag2 = this.ismapEffect;
			if (flag2)
			{
				component.vec = this.effectVec;
				this.ismapEffect = false;
			}
			component.effect = id.ToString();
		}
		else
		{
			this.selfFx(id);
		}
		bool flag3 = id == 1;
		int num;
		if (flag3)
		{
			num = 1;
		}
		else
		{
			SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + id);
			bool flag4 = sXML != null;
			if (flag4)
			{
				num = sXML.getInt("action_tp");
			}
			else
			{
				num = 1;
			}
		}
		this.m_fAttackCount = 0.5f;
		bool flag5 = num == 1;
		if (flag5)
		{
			base.OtherSkillShow();
			this.EnterAttackState();
		}
		else
		{
			this.m_fSkillShowTime = 5f;
			this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, num);
			this.m_curAni.SetBool(EnumAni.ANI_ATTACK, false);
		}
	}

	private void selfFx(int id)
	{
		MS0000_Default_Event component = this.m_curModel.gameObject.GetComponent<MS0000_Default_Event>();
		SXML sXML = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + this.summonid);
		bool flag = sXML != null && sXML.getString("skillid_1") != "";
		if (flag)
		{
			component.effect = sXML.getString("skillid_" + id);
		}
	}
}
