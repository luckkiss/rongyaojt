using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class M000P5 : MonsterRole
{
	private string[] lFirstName;

	private string[] lLastName0;

	private string[] lLastName1;

	public override void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f)
	{
		this.m_fNavStoppingDis = 2f;
		base.roleName = this.RandomName();
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
				id = 5001;
			}
			bool flag3 = 5005 == id;
			if (flag3)
			{
				this.runSkill_5005();
			}
			bool flag4 = 5010 == id;
			if (flag4)
			{
				this.runSkill_5010();
			}
			bool flag5 = 5003 == this.m_curAni.GetInteger(EnumAni.ANI_I_SKILL);
			if (!flag5)
			{
				this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, id);
				this.m_curSkillId = id;
				bool isMain = this.m_isMain;
				if (isMain)
				{
					this.m_moveAgent.avoidancePriority = 50;
				}
				bool flag6 = 5003 == id;
				if (flag6)
				{
					this.m_fAttackCount = 2.5f;
					bool flag7 = !this.m_isMain;
					if (flag7)
					{
						this.m_moveAgent.updateRotation = true;
						this.m_moveAgent.updatePosition = true;
						this.m_fSkillShowTime = 2.5f;
						this.m_moveAgent.speed = 5f;
					}
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P5Assassin.ASSASSIN_SFX1);
					UnityEngine.Object.Destroy(gameObject, 2.5f);
					gameObject.transform.SetParent(this.m_curModel, false);
				}
				else
				{
					this.m_fAttackCount = 1.5f;
				}
			}
		}
	}

	public void runSkill_5005()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P5Assassin.ASSASSIN_SFX2);
		gameObject.transform.SetParent(this.m_curModel, false);
		UnityEngine.Object.Destroy(gameObject, 10f);
	}

	public void runSkill_5010()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P5Assassin.ASSASSIN_SFX3);
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(P5Assassin.ASSASSIN_SFX3);
		gameObject.transform.SetParent(this.m_curModel.transform.FindChild("Weapon_L"), false);
		gameObject2.transform.SetParent(this.m_curModel.transform.FindChild("Weapon_R"), false);
		UnityEngine.Object.Destroy(gameObject, 60f);
		UnityEngine.Object.Destroy(gameObject2, 60f);
	}

	public void SetSkin()
	{
		SXML sXML = XMLMgr.instance.GetSXML("mlzd", "");
		int num = SelfRole._inst.zhuan * 10 + SelfRole._inst.lvl;
		int num2 = 2;
		int num3 = 3;
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
		string avatar_path = "profession/assa/";
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
