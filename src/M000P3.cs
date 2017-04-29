using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class M000P3 : MonsterRole
{
	private GameObject m_SFX1;

	private GameObject m_SFX2;

	private GameObject m_SFX3;

	private string[] lFirstName;

	private string[] lLastName0;

	private string[] lLastName1;

	private TickItem process_3003;

	private float m_skill3003_time = 4f;

	private float m_cur3003_time = 0f;

	private int m_skill3003_num = 20;

	private int m_cur3003_num = 0;

	private Vector3 m_3003_pos;

	private Quaternion m_3003_rotation;

	private TickItem process_3008;

	private float m_skill3008_time = 30f;

	private float m_cur3008_time = 0f;

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
				id = 3001;
			}
			bool flag3 = 3003 == id;
			if (flag3)
			{
				this.runSkill_3003();
			}
			bool flag4 = 3008 == id;
			if (flag4)
			{
				this.runSkill_3008();
			}
			bool isMain = this.m_isMain;
			if (isMain)
			{
				this.m_moveAgent.avoidancePriority = 50;
			}
			this.m_curSkillId = id;
			this.m_curAni.SetInteger(EnumAni.ANI_I_SKILL, id);
			this.m_fAttackCount = 1f;
		}
	}

	private void runSkill_3003()
	{
		bool flag = this.process_3003 == null;
		if (flag)
		{
			this.process_3003 = new TickItem(new Action<float>(this.onUpdate_3003));
			TickMgr.instance.addTick(this.process_3003);
		}
		this.m_cur3003_time = 0f;
		this.m_cur3003_num = 0;
		this.m_3003_pos = this.m_curModel.transform.position;
		this.m_3003_rotation = this.m_curModel.transform.rotation;
		bool flag2 = this.m_SFX2 == null;
		if (flag2)
		{
			this.m_SFX2 = (UnityEngine.Object.Instantiate(P3Mage.P3MAGE_SFX2, this.m_3003_pos, this.m_3003_rotation) as GameObject);
			this.m_SFX2.transform.SetParent(U3DAPI.FX_POOL_TF, false);
		}
	}

	private void onUpdate_3003(float s)
	{
		bool flag = this.m_curModel == null;
		if (flag)
		{
			TickMgr.instance.removeTick(this.process_3003);
			this.process_3003 = null;
			this.m_SFX2.transform.FindChild("f").GetComponent<Animator>().SetTrigger(EnumAni.ANI_T_FXDEAD);
			UnityEngine.Object.Destroy(this.m_SFX2, 2f);
			this.m_SFX2 = null;
		}
		else
		{
			this.m_cur3003_time += s;
			float num = 0.5f + (float)this.m_cur3003_num * (this.m_skill3003_time - 1f) / (float)this.m_skill3003_num;
			bool flag2 = this.m_cur3003_time > num;
			if (flag2)
			{
				this.onBullet_3003(this.m_3003_pos, this.m_3003_rotation);
				this.m_cur3003_num++;
			}
			bool flag3 = this.m_cur3003_time > this.m_skill3003_time;
			if (flag3)
			{
				this.m_cur3003_time = 0f;
				this.m_cur3003_num = 0;
				TickMgr.instance.removeTick(this.process_3003);
				this.process_3003 = null;
				this.m_SFX2.transform.FindChild("f").GetComponent<Animator>().SetTrigger(EnumAni.ANI_T_FXDEAD);
				UnityEngine.Object.Destroy(this.m_SFX2, 2f);
				this.m_SFX2 = null;
			}
		}
	}

	public void onBullet_3003(Vector3 pos, Quaternion rotation)
	{
		pos.z += UnityEngine.Random.Range(-3f, 3f);
		pos.x += UnityEngine.Random.Range(-3f, 3f);
		pos.y = 16f;
		GameObject gameObject = UnityEngine.Object.Instantiate(P3Mage_Event.MAGE_B3, pos, rotation) as GameObject;
		gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
		Transform transform = gameObject.transform.FindChild("t");
		bool flag = transform != null;
		if (flag)
		{
			HitData hitData = this.Link_PRBullet(3003u, 2f, gameObject, transform);
			hitData.m_nHurtSP_type = 11;
			hitData.m_nHurtSP_pow = 1;
			hitData.m_nDamage = 177;
			hitData.m_Color_Main = Color.blue;
			hitData.m_Color_Rim = Color.white;
			hitData.m_aniTrack = transform.GetComponent<Animator>();
			transform.gameObject.layer = EnumLayer.LM_BT_FIGHT;
			Transform transform2 = transform.FindChild("f");
			bool flag2 = transform2 != null;
			if (flag2)
			{
				hitData.m_aniFx = transform2.GetComponent<Animator>();
			}
		}
	}

	public new HitData Link_PRBullet(uint skillid, float t, GameObject root, Transform linker)
	{
		HitData hitData = linker.gameObject.AddComponent<HitData>();
		hitData.m_hdRootObj = root;
		hitData.m_CastRole = this;
		hitData.m_vBornerPos = this.m_curModel.position;
		hitData.m_ePK_Type = this.m_ePK_Type;
		switch (this.m_ePK_Type)
		{
		case PK_TYPE.PK_PKALL:
			hitData.m_unPK_Param = this.m_unCID;
			break;
		case PK_TYPE.PK_TEAM:
			hitData.m_unPK_Param = this.m_unTeamID;
			break;
		case PK_TYPE.PK_LEGION:
			hitData.m_unPK_Param = this.m_unLegionID;
			break;
		}
		hitData.m_unSkillID = skillid;
		hitData.m_nDamage = 100;
		hitData.m_nHitType = 0;
		linker.gameObject.layer = EnumLayer.LM_BT_FIGHT;
		UnityEngine.Object.Destroy(root, t);
		bool flag = this.m_fDisposeTime < t;
		if (flag)
		{
			this.m_fDisposeTime = t;
		}
		return hitData;
	}

	public void runSkill_3008()
	{
		bool flag = this.m_SFX1 == null;
		if (flag)
		{
			this.m_SFX1 = UnityEngine.Object.Instantiate<GameObject>(P3Mage.P3MAGE_SFX1);
			this.m_SFX1.transform.SetParent(this.m_curModel, false);
			this.m_SFX1.SetActive(false);
		}
		bool flag2 = this.process_3008 == null;
		if (flag2)
		{
			this.process_3008 = new TickItem(new Action<float>(this.onUpdate_3008));
			TickMgr.instance.addTick(this.process_3008);
		}
		this.m_cur3008_time = 0f;
	}

	private void onUpdate_3008(float s)
	{
		bool flag = this.m_curModel == null;
		if (flag)
		{
			TickMgr.instance.removeTick(this.process_3008);
		}
		else
		{
			this.m_cur3008_time += s;
			bool flag2 = this.m_cur3008_time > 0.6f;
			if (flag2)
			{
				this.m_SFX1.SetActive(true);
			}
			bool flag3 = this.m_cur3008_time > this.m_skill3008_time;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.m_SFX1);
				this.m_cur3008_time = 0f;
				TickMgr.instance.removeTick(this.process_3008);
				this.process_3008 = null;
				this.m_SFX1 = null;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(P3Mage.P3MAGE_SFX3);
				gameObject.transform.SetParent(this.m_curModel, false);
				UnityEngine.Object.Destroy(gameObject, 1f);
			}
		}
	}

	public void SetSkin()
	{
		SXML sXML = XMLMgr.instance.GetSXML("mlzd", "");
		int num = SelfRole._inst.zhuan * 10 + SelfRole._inst.lvl;
		int num2 = 2;
		int num3 = 2;
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
		string avatar_path = "profession/mage/";
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
