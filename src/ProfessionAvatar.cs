using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProfessionAvatar
{
	private string m_strAvatarPath;

	private string m_strEquipEffPath;

	private string m_strLow_or_High;

	private int m_nLayer;

	private Material m_fxLvMtl;

	private int m_nCurFXID;

	private Transform m_Weapon_LBone;

	private GameObject m_Weapon_LObj;

	private Renderer m_Weapon_LDraw;

	private Material m_Weapon_LMtl;

	public int m_Weapon_LID = -1;

	public int m_Weapon_LFXLV = -1;

	private Transform m_Weapon_RBone;

	private GameObject m_Weapon_RObj;

	private Renderer m_Weapon_RDraw;

	private Material m_Weapon_RMtl;

	public int m_Weapon_RID = -1;

	public int m_Weapon_RFXLV = -1;

	private Transform m_WingBone;

	public GameObject m_WingObj;

	private Renderer m_Wing_Draw;

	private Material m_Wing_Mtl;

	public int m_WindID = -1;

	public int m_Wing_FXLV = -1;

	private List<GameObject> m_Euiqp_Eff = new List<GameObject>();

	public int m_Equip_Eff_id = -1;

	public SkinnedMeshRenderer m_BodySkin;

	public SkinnedMeshRenderer m_ShoulderSkin;

	private Material m_Body_Mtl;

	private Material m_Shoulder_Mtl;

	public int m_BodyID = -1;

	public int m_BodyFXLV = -1;

	public uint m_EquipColorID = 0u;

	public Transform m_curModel;

	private Transform m_L_UpperArm;

	private Transform m_R_UpperArm;

	private Transform m_Pelvis;

	private Transform m_Spine;

	public void Init(string avatar_path, string lh, int layer, Material fxlv, Transform cur_model, string equipEff_path = "")
	{
		this.m_strAvatarPath = avatar_path;
		this.m_strEquipEffPath = equipEff_path;
		this.m_strLow_or_High = lh;
		this.m_nLayer = layer;
		this.m_fxLvMtl = fxlv;
		this.m_Weapon_LBone = cur_model.FindChild("Weapon_L");
		this.m_Weapon_RBone = cur_model.FindChild("Weapon_R");
		this.m_WingBone = cur_model.FindChild("Plus_B");
		this.m_BodySkin = cur_model.FindChild("body").GetComponent<SkinnedMeshRenderer>();
		this.m_ShoulderSkin = cur_model.FindChild("shoulder").GetComponent<SkinnedMeshRenderer>();
		this.m_BodyID = -1;
		this.m_Weapon_LID = -1;
		this.m_Weapon_RID = -1;
		this.m_WindID = -1;
		this.m_L_UpperArm = cur_model.FindChild("L_UpperArm");
		this.m_R_UpperArm = cur_model.FindChild("R_UpperArm");
		this.m_Pelvis = cur_model.FindChild("Pelvis");
		this.m_Spine = cur_model.FindChild("Spine");
		this.m_curModel = cur_model;
	}

	public void FrameMove()
	{
		bool flag = this.m_curModel != null;
		if (flag)
		{
			Animator component = this.m_curModel.GetComponent<Animator>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.Rebind();
			}
			this.m_curModel = null;
		}
	}

	public void push_fx(int id, bool mustdo = false)
	{
		bool flag = this.m_nCurFXID <= 0 | mustdo;
		if (flag)
		{
			this.m_nCurFXID = id;
			Material eMT_SKILL_HIDE = EnumMaterial.EMT_SKILL_HIDE;
			bool flag2 = this.m_Body_Mtl != null;
			if (flag2)
			{
				this.m_BodySkin.material = eMT_SKILL_HIDE;
			}
			bool flag3 = this.m_Shoulder_Mtl != null;
			if (flag3)
			{
				this.m_ShoulderSkin.material = eMT_SKILL_HIDE;
			}
			bool flag4 = this.m_Weapon_LDraw != null;
			if (flag4)
			{
				this.m_Weapon_LDraw.material = eMT_SKILL_HIDE;
			}
			bool flag5 = this.m_Weapon_RDraw != null;
			if (flag5)
			{
				this.m_Weapon_RDraw.material = eMT_SKILL_HIDE;
			}
			bool flag6 = this.m_Wing_Draw != null;
			if (flag6)
			{
				this.m_Wing_Draw.material = eMT_SKILL_HIDE;
			}
			bool flag7 = a3_lowblood.instance != null;
			if (flag7)
			{
				a3_lowblood.instance.begin_assassin_fx();
			}
		}
	}

	public void pop_fx()
	{
		bool flag = this.m_nCurFXID > 0;
		if (flag)
		{
			this.m_nCurFXID = 0;
			bool flag2 = this.m_Body_Mtl != null;
			if (flag2)
			{
				this.m_BodySkin.material = this.m_Body_Mtl;
			}
			bool flag3 = this.m_Shoulder_Mtl != null;
			if (flag3)
			{
				this.m_ShoulderSkin.material = this.m_Shoulder_Mtl;
			}
			bool flag4 = this.m_Weapon_LDraw != null;
			if (flag4)
			{
				this.m_Weapon_LDraw.material = this.m_Weapon_LMtl;
			}
			bool flag5 = this.m_Weapon_RDraw != null;
			if (flag5)
			{
				this.m_Weapon_RDraw.material = this.m_Weapon_RMtl;
			}
			bool flag6 = this.m_Wing_Draw != null;
			if (flag6)
			{
				this.m_Wing_Draw.material = this.m_Wing_Mtl;
			}
			bool flag7 = a3_lowblood.instance != null;
			if (flag7)
			{
				a3_lowblood.instance.end_assassin_fx();
			}
		}
	}

	public void set_weaponl(int id, int fxlevel)
	{
		bool flag = this.m_Weapon_LID == id && this.m_Weapon_LFXLV == fxlevel;
		if (!flag)
		{
			this.m_Weapon_LID = id;
			this.m_Weapon_LFXLV = fxlevel;
			bool flag2 = this.m_Weapon_LID == -1;
			if (flag2)
			{
				bool flag3 = this.m_Weapon_LObj != null;
				if (flag3)
				{
					UnityEngine.Object.Destroy(this.m_Weapon_LObj);
				}
				this.m_Weapon_LObj = null;
			}
			else
			{
				bool flag4 = this.m_Weapon_LObj != null;
				if (flag4)
				{
					UnityEngine.Object.Destroy(this.m_Weapon_LObj);
					this.m_Weapon_LObj = null;
				}
				bool flag5 = this.m_Weapon_LID >= 0;
				if (flag5)
				{
					GameObject gameObject = Resources.Load<GameObject>(this.m_strAvatarPath + "weaponl_" + this.m_strLow_or_High + this.m_Weapon_LID.ToString());
					bool flag6 = gameObject == null;
					if (!flag6)
					{
						this.m_Weapon_LObj = UnityEngine.Object.Instantiate<GameObject>(gameObject);
						this.m_Weapon_LObj.transform.SetParent(this.m_Weapon_LBone, false);
						Transform[] componentsInChildren = this.m_Weapon_LObj.GetComponentsInChildren<Transform>();
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							Transform transform = componentsInChildren[i];
							transform.gameObject.layer = this.m_nLayer;
						}
						Transform transform2 = this.m_Weapon_LObj.transform.FindChild("weapon");
						bool flag7 = transform2 != null;
						if (flag7)
						{
							SkinnedMeshRenderer component = transform2.GetComponent<SkinnedMeshRenderer>();
							bool flag8 = component != null;
							if (flag8)
							{
								bool flag9 = fxlevel > 0;
								if (flag9)
								{
									Material material = component.material;
									component.material = this.set_strength_shader(material, id, fxlevel);
								}
								this.m_Weapon_LDraw = component;
								this.m_Weapon_LMtl = component.material;
							}
							else
							{
								MeshRenderer component2 = transform2.GetComponent<MeshRenderer>();
								bool flag10 = component2 != null;
								if (flag10)
								{
									bool flag11 = fxlevel > 0;
									if (flag11)
									{
										Material material2 = component2.material;
										component2.material = this.set_strength_shader(material2, id, fxlevel);
									}
									this.m_Weapon_LDraw = component2;
									this.m_Weapon_LMtl = component2.material;
								}
							}
						}
					}
				}
			}
		}
	}

	public void set_weaponr(int id, int fxlevel)
	{
		bool flag = this.m_Weapon_RID == id && this.m_Weapon_RFXLV == fxlevel;
		if (!flag)
		{
			this.m_Weapon_RID = id;
			this.m_Weapon_RFXLV = fxlevel;
			bool flag2 = this.m_Weapon_RID == -1;
			if (flag2)
			{
				bool flag3 = this.m_Weapon_RObj != null;
				if (flag3)
				{
					UnityEngine.Object.Destroy(this.m_Weapon_RObj);
				}
				this.m_Weapon_RObj = null;
			}
			else
			{
				bool flag4 = this.m_Weapon_RObj != null;
				if (flag4)
				{
					UnityEngine.Object.Destroy(this.m_Weapon_RObj);
					this.m_Weapon_RObj = null;
				}
				bool flag5 = this.m_Weapon_RID >= 0;
				if (flag5)
				{
					GameObject gameObject = Resources.Load<GameObject>(this.m_strAvatarPath + "weaponr_" + this.m_strLow_or_High + this.m_Weapon_RID.ToString());
					bool flag6 = gameObject == null;
					if (!flag6)
					{
						this.m_Weapon_RObj = UnityEngine.Object.Instantiate<GameObject>(gameObject);
						this.m_Weapon_RObj.transform.SetParent(this.m_Weapon_RBone, false);
						Transform[] componentsInChildren = this.m_Weapon_RObj.GetComponentsInChildren<Transform>();
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							Transform transform = componentsInChildren[i];
							transform.gameObject.layer = this.m_nLayer;
						}
						Transform transform2 = this.m_Weapon_RObj.transform.FindChild("weapon");
						bool flag7 = transform2 != null;
						if (flag7)
						{
							SkinnedMeshRenderer component = transform2.GetComponent<SkinnedMeshRenderer>();
							bool flag8 = component != null;
							if (flag8)
							{
								bool flag9 = fxlevel > 0;
								if (flag9)
								{
									Material material = component.material;
									component.material = this.set_strength_shader(material, id, fxlevel);
								}
								this.m_Weapon_RDraw = component;
								this.m_Weapon_RMtl = component.material;
							}
							else
							{
								MeshRenderer component2 = transform2.GetComponent<MeshRenderer>();
								bool flag10 = component2 != null;
								if (flag10)
								{
									bool flag11 = fxlevel > 0;
									if (flag11)
									{
										Material material2 = component2.material;
										component2.material = this.set_strength_shader(material2, id, fxlevel);
									}
									this.m_Weapon_RDraw = component2;
									this.m_Weapon_RMtl = component2.material;
								}
							}
						}
					}
				}
			}
		}
	}

	public void set_body(int id, int fxlevel)
	{
		bool flag = this.m_BodyID == id && this.m_BodyFXLV == fxlevel;
		if (!flag)
		{
			this.m_BodyID = id;
			this.m_BodyFXLV = fxlevel;
			bool flag2 = this.m_BodyID == -1;
			if (flag2)
			{
				bool flag3 = this.m_BodySkin != null;
				if (flag3)
				{
					this.m_BodySkin.sharedMesh = null;
					this.m_ShoulderSkin.sharedMesh = null;
				}
			}
			else
			{
				GameObject gameObject = Resources.Load<GameObject>(this.m_strAvatarPath + "body_" + this.m_strLow_or_High + this.m_BodyID.ToString());
				bool flag4 = gameObject == null || this.m_BodySkin == null;
				if (!flag4)
				{
					SkinnedMeshRenderer component = gameObject.GetComponent<SkinnedMeshRenderer>();
					this.m_BodySkin.sharedMesh = component.sharedMesh;
					this.m_BodySkin.sharedMaterials = component.sharedMaterials;
					bool flag5 = fxlevel > 0;
					if (flag5)
					{
						Material material = this.m_BodySkin.material;
						this.m_BodySkin.material = this.set_strength_shader(material, id, fxlevel);
					}
					this.m_Body_Mtl = this.m_BodySkin.material;
					GameObject gameObject2 = Resources.Load<GameObject>(this.m_strAvatarPath + "shoulder_" + this.m_strLow_or_High + this.m_BodyID.ToString());
					bool flag6 = gameObject2 == null;
					if (!flag6)
					{
						SkinnedMeshRenderer component2 = gameObject2.GetComponent<SkinnedMeshRenderer>();
						this.m_ShoulderSkin.sharedMesh = component2.sharedMesh;
						this.m_ShoulderSkin.sharedMaterials = component2.sharedMaterials;
						bool flag7 = fxlevel > 0;
						if (flag7)
						{
							Material material2 = this.m_ShoulderSkin.material;
							this.m_ShoulderSkin.material = this.set_strength_shader(material2, id, fxlevel);
						}
						this.m_Shoulder_Mtl = this.m_ShoulderSkin.material;
					}
				}
			}
		}
	}

	public void clear_eff()
	{
		foreach (GameObject current in this.m_Euiqp_Eff)
		{
			UnityEngine.Object.Destroy(current);
		}
		this.m_Euiqp_Eff.Clear();
	}

	public void set_equip_eff(int id, bool high)
	{
		foreach (GameObject current in this.m_Euiqp_Eff)
		{
			UnityEngine.Object.Destroy(current);
		}
		this.m_Euiqp_Eff.Clear();
		this.m_Equip_Eff_id = id;
		bool flag = this.m_Equip_Eff_id > 0;
		if (flag)
		{
			GameObject gameObject = Resources.Load<GameObject>(string.Concat(new object[]
			{
				this.m_strEquipEffPath,
				id,
				"/FX_L_UpperArm_",
				id
			}));
			GameObject gameObject2 = Resources.Load<GameObject>(string.Concat(new object[]
			{
				this.m_strEquipEffPath,
				id,
				"/FX_Pelvis_",
				id
			}));
			GameObject gameObject3 = Resources.Load<GameObject>(string.Concat(new object[]
			{
				this.m_strEquipEffPath,
				id,
				"/FX_R_UpperArm_",
				id
			}));
			GameObject gameObject4 = Resources.Load<GameObject>(string.Concat(new object[]
			{
				this.m_strEquipEffPath,
				id,
				"/FX_Spine_",
				id
			}));
			bool flag2 = gameObject != null;
			if (flag2)
			{
				this.add_equip_eff(gameObject, this.m_L_UpperArm, high);
			}
			bool flag3 = gameObject2 != null;
			if (flag3)
			{
				this.add_equip_eff(gameObject2, this.m_Pelvis, high);
			}
			bool flag4 = gameObject3 != null;
			if (flag4)
			{
				this.add_equip_eff(gameObject3, this.m_R_UpperArm, high);
			}
			bool flag5 = gameObject4 != null;
			if (flag5)
			{
				this.add_equip_eff(gameObject4, this.m_Spine, high);
			}
		}
	}

	private void add_equip_eff(GameObject child, Transform parent, bool high)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(child);
		if (high)
		{
			Transform transform = gameObject.transform.FindChild("hide");
			bool flag = transform != null;
			if (flag)
			{
				transform.gameObject.SetActive(true);
			}
		}
		gameObject.transform.SetParent(parent, false);
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform2 = componentsInChildren[i];
			transform2.gameObject.layer = this.m_nLayer;
		}
		this.m_Euiqp_Eff.Add(gameObject);
	}

	public void set_wing(int id, int fxlevel)
	{
		bool flag = this.m_WindID == id && this.m_Wing_FXLV == fxlevel;
		if (!flag)
		{
			this.m_WindID = id;
			this.m_Wing_FXLV = fxlevel;
			bool flag2 = this.m_WindID == -1;
			if (flag2)
			{
				bool flag3 = this.m_WingObj != null;
				if (flag3)
				{
					UnityEngine.Object.Destroy(this.m_WingObj);
				}
				this.m_WingObj = null;
			}
			else
			{
				bool flag4 = this.m_WingObj != null;
				if (flag4)
				{
					UnityEngine.Object.Destroy(this.m_WingObj);
					this.m_WingObj = null;
				}
				bool flag5 = this.m_WindID >= 0;
				if (flag5)
				{
					GameObject gameObject = Resources.Load<GameObject>(this.m_strAvatarPath + "wing_" + this.m_strLow_or_High + this.m_WindID.ToString());
					bool flag6 = gameObject == null;
					if (!flag6)
					{
						this.m_WingObj = UnityEngine.Object.Instantiate<GameObject>(gameObject);
						this.m_WingObj.transform.SetParent(this.m_WingBone, false);
						Transform[] componentsInChildren = this.m_WingObj.GetComponentsInChildren<Transform>();
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							Transform transform = componentsInChildren[i];
							transform.gameObject.layer = this.m_nLayer;
						}
						Transform transform2 = this.m_WingObj.transform.FindChild("wing");
						bool flag7 = transform2 != null;
						if (flag7)
						{
							SkinnedMeshRenderer component = transform2.GetComponent<SkinnedMeshRenderer>();
							bool flag8 = component != null;
							if (flag8)
							{
								this.m_Wing_Draw = component;
								this.m_Wing_Mtl = component.material;
							}
							else
							{
								MeshRenderer component2 = transform2.GetComponent<MeshRenderer>();
								bool flag9 = component2 != null;
								if (flag9)
								{
									this.m_Wing_Draw = component2;
									this.m_Wing_Mtl = component2.material;
								}
							}
						}
					}
				}
			}
		}
	}

	public Material set_strength_shader(Material ml, int id, int fxlevel)
	{
		Material material = UnityEngine.Object.Instantiate<Material>(this.m_fxLvMtl);
		Texture texture = ml.GetTexture(EnumShader.SPI_MAINTEX);
		material.SetTexture(EnumShader.SPI_MAINTEX, texture);
		string str = texture.name.Substring(0, texture.name.Length - 6) + "q";
		debug.Log("材质是" + str);
		Texture texture2 = Resources.Load("mtl/" + str) as Texture2D;
		bool flag = texture2 != null;
		if (flag)
		{
			material.SetTexture(EnumShader.SPI_SUBTEX, texture2);
		}
		SXML sXML = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + fxlevel);
		bool flag2 = sXML == null;
		Material result;
		if (flag2)
		{
			result = material;
		}
		else
		{
			sXML = sXML.GetNode("stage_info", "itemid==" + id);
			bool flag3 = sXML == null;
			if (flag3)
			{
				result = material;
			}
			else
			{
				sXML = sXML.GetNode("intensify_light", null);
				bool flag4 = sXML == null;
				if (flag4)
				{
					result = material;
				}
				else
				{
					string[] array = sXML.getString("high_light").Split(new char[]
					{
						','
					});
					string[] array2 = sXML.getString("strength_light").Split(new char[]
					{
						','
					});
					string[] array3 = sXML.getString("color").Split(new char[]
					{
						','
					});
					Vector4 vector = new Vector4(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
					Vector4 vector2 = new Vector4(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]), float.Parse(array2[3]));
					Color color = new Color(float.Parse(array3[0]) / 255f, float.Parse(array3[1]) / 255f, float.Parse(array3[2]) / 255f);
					material.SetVector(EnumShader.SPI_SPLRIM, vector);
					material.SetVector(EnumShader.SPI_STRANIM, vector2);
					material.SetColor(EnumShader.SPI_STRCOLOR, color);
					result = material;
				}
			}
		}
		return result;
	}

	public void set_equip_color(uint id)
	{
		this.m_EquipColorID = id;
		bool flag = this.m_BodyID == -1;
		if (!flag)
		{
			bool flag2 = id == 0u;
			if (flag2)
			{
				this.m_BodySkin.material.SetColor(EnumShader.SPI_CHANGECOLOR, new Color(1f, 1f, 1f, 0f));
				this.m_ShoulderSkin.material.SetColor(EnumShader.SPI_CHANGECOLOR, new Color(1f, 1f, 1f, 0f));
			}
			else
			{
				Color color = a3_EquipModel.EQUIP_COLOR[id];
				this.m_BodySkin.material.SetColor(EnumShader.SPI_CHANGECOLOR, color);
				this.m_ShoulderSkin.material.SetColor(EnumShader.SPI_CHANGECOLOR, color);
			}
		}
	}
}
