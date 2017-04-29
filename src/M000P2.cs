using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class M000P2 : MonsterRole
{
	private GameObject m_SFX1;

	private GameObject m_SFX2;

	private string[] lFirstName;

	private string[] lLastName0;

	private string[] lLastName1;

	private TickItem process_2005;

	private float m_skill2005_time = 10f;

	private float m_cur2005_time = 0f;

	private TickItem process_2010;

	private float m_skill2010_time = 10f;

	private float m_cur2010_time = 0f;

	public override void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f)
	{
		base.roleName = this.RandomName();
		this.m_fNavStoppingDis = 2f;
		base.Init(prefab_path, layer, pos, roatate);
		M0x000_Role_Event m0x000_Role_Event = this.m_curModel.gameObject.AddComponent<M0x000_Role_Event>();
		m0x000_Role_Event.m_monRole = this;
		base.maxHp = (base.curhp = 1000);
		this.SetSkin();
	}

	public override void PlaySkill(int id)
	{
		bool flag = this.m_curSkillId != 0;
		if (!flag)
		{
			bool flag2 = id == 1;
			if (flag2)
			{
				id = 2001;
			}
			bool isMain = this.m_isMain;
			if (isMain)
			{
				this.m_moveAgent.avoidancePriority = 50;
			}
			this.m_curSkillId = id;
			bool flag3 = 2003 == this.m_curAni.GetInteger(EnumAni.ANI_I_SKILL);
			if (!flag3)
			{
				bool flag4 = 2005 == id;
				if (flag4)
				{
					this.runSkill_2005();
				}
				bool flag5 = 2010 == id;
				if (flag5)
				{
					this.runSkill_2010();
				}
				bool flag6 = 2009 == id;
				if (flag6)
				{
					this.runSkill_2009();
				}
				this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, id);
				bool flag7 = 2003 == id;
				if (flag7)
				{
					this.m_fAttackCount = 3.5f;
					bool flag8 = this.m_moveAgent;
					if (flag8)
					{
						this.m_moveAgent.updateRotation = true;
						this.m_moveAgent.updatePosition = true;
						this.m_fSkillShowTime = 3.5f;
						this.m_moveAgent.speed = 3f;
					}
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX1);
					UnityEngine.Object.Destroy(gameObject, 3.5f);
					gameObject.transform.SetParent(this.m_curModel, false);
				}
				else
				{
					this.m_fAttackCount = 1.5f;
				}
			}
		}
	}

	public void runSkill_2005()
	{
		bool flag = this.m_SFX1 == null;
		if (flag)
		{
			this.m_SFX1 = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX2);
			this.m_SFX1.transform.SetParent(this.m_curModel, false);
			this.m_SFX1.SetActive(false);
		}
		bool flag2 = this.process_2005 == null;
		if (flag2)
		{
			this.process_2005 = new TickItem(new Action<float>(this.onUpdate_2005));
			TickMgr.instance.addTick(this.process_2005);
		}
		this.m_cur2005_time = 0f;
	}

	private void onUpdate_2005(float s)
	{
		bool flag = this.m_curModel == null;
		if (flag)
		{
			TickMgr.instance.removeTick(this.process_2005);
		}
		else
		{
			this.m_cur2005_time += s;
			bool flag2 = this.m_cur2005_time > 1f;
			if (flag2)
			{
				this.m_SFX1.SetActive(true);
			}
			bool flag3 = this.m_cur2005_time > this.m_skill2005_time;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.m_SFX1);
				this.m_cur2005_time = 0f;
				TickMgr.instance.removeTick(this.process_2005);
				this.process_2005 = null;
				this.m_SFX1 = null;
			}
		}
	}

	public void runSkill_2010()
	{
		bool flag = this.m_SFX2 == null;
		if (flag)
		{
			this.m_SFX2 = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX2);
			this.m_SFX2.transform.SetParent(this.m_curModel, false);
			this.m_SFX2.SetActive(false);
		}
		bool flag2 = this.process_2010 == null;
		if (flag2)
		{
			this.process_2010 = new TickItem(new Action<float>(this.onUpdate_2010));
			TickMgr.instance.addTick(this.process_2010);
		}
		this.m_cur2010_time = 0f;
	}

	private void onUpdate_2010(float s)
	{
		bool flag = this.m_curModel == null;
		if (flag)
		{
			TickMgr.instance.removeTick(this.process_2010);
		}
		else
		{
			this.m_cur2010_time += s;
			bool flag2 = this.m_cur2010_time > 1f;
			if (flag2)
			{
				this.m_SFX2.SetActive(true);
			}
			bool flag3 = this.m_cur2010_time > this.m_skill2010_time;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.m_SFX2);
				this.m_cur2010_time = 0f;
				TickMgr.instance.removeTick(this.process_2010);
				this.process_2010 = null;
				this.m_SFX2 = null;
			}
		}
	}

	public void runSkill_2009()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX3);
		UnityEngine.Object.Destroy(gameObject, 3.5f);
		gameObject.transform.SetParent(this.m_curModel, false);
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(P2Warrior.WARRIOR_SFX4);
		UnityEngine.Object.Destroy(gameObject2, 3.5f);
		bool flag = this.m_curModel.FindChild("Spine") != null;
		if (flag)
		{
			gameObject2.transform.SetParent(this.m_curModel.FindChild("Spine"), false);
		}
	}

	public void SetSkin()
	{
		SXML sXML = XMLMgr.instance.GetSXML("mlzd", "");
		int num = SelfRole._inst.zhuan * 10 + SelfRole._inst.lvl;
		int num2 = 2;
		int num3 = 1;
		List<SXML> nodeList = sXML.GetNodeList("stage", "");
		foreach (SXML current in nodeList)
		{
			string @string = current.getString("lvl");
			int num4 = int.Parse(@string.Split(new char[]
			{
				','
			})[0]);
			int num5 = int.Parse(@string.Split(new char[]
			{
				','
			})[1]);
			bool flag = num <= num4 * 10 + num5;
			if (flag)
			{
				num2 = current.getInt("waiguan");
				break;
			}
		}
		GameObject gameObject = this.m_curModel.parent.gameObject;
		string avatar_path = "profession/warrior/";
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = EnumLayer.LM_SELFROLE;
		}
		Transform transform2 = gameObject.transform.FindChild("model");
		ProfessionAvatar professionAvatar = new ProfessionAvatar();
		professionAvatar.Init(avatar_path, "l_", this.m_curGameObj.layer, EnumMaterial.EMT_EQUIP_L, this.m_curModel, "");
		professionAvatar.set_body(num3 * 100 + num2 * 10 + 3, 0);
		professionAvatar.set_weaponl(num3 * 100 + num2 * 10 + 6, 0);
		professionAvatar.set_weaponr(num3 * 100 + num2 * 10 + 6, 0);
		professionAvatar.set_wing(0, 0);
		this.m_curPhy = this.m_curModel.transform.FindChild("physics");
		try
		{
			this.m_curPhy.gameObject.layer = EnumLayer.LM_BT_FIGHT;
		}
		catch (Exception var_18_1F5)
		{
		}
	}

	private string RandomName()
	{
		bool flag = this.lFirstName == null;
		if (flag)
		{
			this.lFirstName = XMLMgr.instance.GetSXML("comm.ranName.firstName", "").getString("value").Split(new char[]
			{
				','
			});
			this.lLastName0 = XMLMgr.instance.GetSXML("comm.ranName.lastName", "sex==0").getString("value").Split(new char[]
			{
				','
			});
			this.lLastName1 = XMLMgr.instance.GetSXML("comm.ranName.lastName", "sex==1").getString("value").Split(new char[]
			{
				','
			});
		}
		System.Random random = new System.Random();
		int num = random.Next(0, this.lFirstName.Length);
		int num2 = random.Next(0, this.lLastName0.Length);
		return this.lFirstName[num] + this.lFirstName[num2];
	}
}
