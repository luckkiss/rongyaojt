using Cross;
using MuGame.role;
using System;
using System.Collections;
using UnityEngine;

namespace MuGame
{
	public class MonsterPlayer : MonsterRole
	{
		public string m_strAvatarPath;

		public bool m_bUserSelf = false;

		public static bool ismyself = false;

		private PetBird m_myPetBird = null;

		private int m_petID = -1;

		private int m_petStage = -1;

		public int zhuan = 0;

		public int Pk_state;

		public int m_nKeepSkillCount = 0;

		protected ProfessionAvatar m_proAvatar;

		private AnimationState m_Wing_Animstate;

		private float m_fWingTime = 0f;

		public int lvl = 0;

		public int combpt = 0;

		public string clanName;

		public void Init(string prefab_path, int layer, Vector3 pos, bool isUser = false)
		{
			base.Init(prefab_path, layer, pos, 0f, isUser);
			this.m_unLegionID = 2u;
			bool bUserSelf = this.m_bUserSelf;
			if (bUserSelf)
			{
				MonsterPlayer.ismyself = true;
			}
			else
			{
				MonsterPlayer.ismyself = false;
			}
			PlayerNameUIMgr.getInstance().show(this);
			base.curhp = (base.maxHp = 2000);
			this.m_proAvatar = new ProfessionAvatar();
			this.m_proAvatar.Init(this.m_strAvatarPath, "l_", this.m_curGameObj.layer, EnumMaterial.EMT_EQUIP_L, this.m_curModel, "");
			bool flag = this.m_isMain && this.viewType != BaseRole.VIEW_TYPE_ALL;
			if (flag)
			{
				base.refreshViewType(BaseRole.VIEW_TYPE_ALL);
			}
		}

		public void creatPetAvatar(int carr)
		{
			bool flag = this.m_myPetBird != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_myPetBird.gameObject);
				this.m_myPetBird = null;
			}
			Transform transform = this.m_curModel.FindChild("birdstop");
			GameObject gameObject = null;
			GameObject gameObject2 = null;
			bool flag2 = carr == 2;
			if (flag2)
			{
				GameObject gameObject3 = Resources.Load<GameObject>("profession/eagle");
				GameObject gameObject4 = Resources.Load<GameObject>("profession/birdpath");
				bool flag3 = gameObject3 == null || gameObject4 == null;
				if (flag3)
				{
					return;
				}
				gameObject2 = (UnityEngine.Object.Instantiate(gameObject3, transform.position, Quaternion.identity) as GameObject);
				gameObject = (UnityEngine.Object.Instantiate(gameObject4, transform.position, Quaternion.identity) as GameObject);
			}
			else
			{
				bool flag4 = carr == 3;
				if (flag4)
				{
					GameObject gameObject5 = Resources.Load<GameObject>("profession/yaque");
					GameObject gameObject6 = Resources.Load<GameObject>("profession/birdpath");
					bool flag5 = gameObject5 == null || gameObject6 == null;
					if (flag5)
					{
						return;
					}
					gameObject2 = (UnityEngine.Object.Instantiate(gameObject5, transform.position, Quaternion.identity) as GameObject);
					gameObject = (UnityEngine.Object.Instantiate(gameObject6, transform.position, Quaternion.identity) as GameObject);
				}
				else
				{
					bool flag6 = carr == 5;
					if (flag6)
					{
						GameObject gameObject7 = Resources.Load<GameObject>("profession/yingwu");
						GameObject gameObject8 = Resources.Load<GameObject>("profession/birdpath");
						bool flag7 = gameObject7 == null || gameObject8 == null;
						if (flag7)
						{
							return;
						}
						gameObject2 = (UnityEngine.Object.Instantiate(gameObject7, transform.position, Quaternion.identity) as GameObject);
						gameObject = (UnityEngine.Object.Instantiate(gameObject8, transform.position, Quaternion.identity) as GameObject);
					}
				}
			}
			bool flag8 = gameObject2 == null || gameObject == null;
			if (!flag8)
			{
				gameObject.transform.parent = transform;
				gameObject2.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
				this.m_myPetBird = gameObject2.AddComponent<PetBird>();
				this.m_myPetBird.Path = gameObject;
			}
		}

		public void ChangePetAvatar(int petID, int petStage)
		{
			bool flag = petID == this.m_petID && petStage == this.m_petStage;
			if (!flag)
			{
				this.m_petID = petID;
				this.m_petStage = petStage;
				bool flag2 = this.m_myPetBird != null;
				if (flag2)
				{
					UnityEngine.Object.Destroy(this.m_myPetBird.gameObject);
					this.m_myPetBird = null;
				}
				Transform transform = this.m_curModel.FindChild("birdstop");
				string petAvatar = ModelBase<A3_PetModel>.getInstance().GetPetAvatar(this.m_petID, 0);
				bool flag3 = petAvatar == "";
				if (!flag3)
				{
					GameObject gameObject = Resources.Load<GameObject>("profession/" + petAvatar);
					GameObject gameObject2 = Resources.Load<GameObject>("profession/birdpath");
					bool flag4 = gameObject == null || gameObject2 == null;
					if (!flag4)
					{
						GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject, transform.position, Quaternion.identity) as GameObject;
						GameObject gameObject4 = UnityEngine.Object.Instantiate(gameObject2, transform.position, Quaternion.identity) as GameObject;
						bool flag5 = gameObject3 == null || gameObject4 == null;
						if (!flag5)
						{
							gameObject4.transform.parent = transform;
							gameObject3.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
							this.m_myPetBird = gameObject3.AddComponent<PetBird>();
							this.m_myPetBird.Path = gameObject4;
						}
					}
				}
			}
		}

		protected override void onRefreshViewType()
		{
			bool flag = !this.m_isMain && this.m_moveAgent != null && this.m_moveAgent.enabled;
			if (flag)
			{
				bool flag2 = this.viewType == BaseRole.VIEW_TYPE_ALL;
				if (flag2)
				{
					this.set_weaponl(this.m_roleDta.m_Weapon_LID, this.m_roleDta.m_Weapon_LFXID);
					this.set_weaponr(this.m_roleDta.m_Weapon_RID, this.m_roleDta.m_Weapon_RFXID);
					this.set_wing(this.m_roleDta.m_WindID, this.m_roleDta.m_WingFXID);
					this.set_body(this.m_roleDta.m_BodyID, this.m_roleDta.m_BodyFXID);
					this.rebind_ani();
					this.set_equip_color(this.m_roleDta.m_EquipColorID);
				}
				else
				{
					bool flag3 = this.viewType == BaseRole.VIEW_TYPE_NAV;
					if (flag3)
					{
						this.m_proAvatar.set_weaponl(-1, 0);
						this.m_proAvatar.set_weaponr(-1, 0);
						this.m_proAvatar.set_wing(-1, 0);
						this.m_proAvatar.set_body(-1, 0);
						this.m_proAvatar.set_equip_color(this.m_roleDta.m_EquipColorID);
					}
				}
			}
		}

		public void set_weaponl(int id, int fxlevel)
		{
			bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
			if (flag)
			{
				this.m_roleDta.m_Weapon_LID = id;
				this.m_roleDta.m_Weapon_LFXID = fxlevel;
			}
			else
			{
				this.m_proAvatar.set_weaponl(id, fxlevel);
			}
		}

		public void set_weaponr(int id, int fxlevel)
		{
			bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
			if (flag)
			{
				this.m_roleDta.m_Weapon_RID = id;
				this.m_roleDta.m_Weapon_RFXID = fxlevel;
			}
			else
			{
				this.m_proAvatar.set_weaponr(id, fxlevel);
			}
		}

		public void set_wing(int id, int fxlevel)
		{
			bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
			if (flag)
			{
				this.m_roleDta.m_WindID = id;
				this.m_roleDta.m_WingFXID = fxlevel;
			}
			else
			{
				this.m_proAvatar.set_wing(id, fxlevel);
				bool flag2 = this.m_proAvatar.m_WindID > 0 && this.m_proAvatar.m_WingObj != null;
				if (flag2)
				{
					Animation component = this.m_proAvatar.m_WingObj.GetComponent<Animation>();
					bool flag3 = component != null;
					if (flag3)
					{
						this.m_Wing_Animstate = component["wg"];
					}
					this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 1f);
				}
				else
				{
					this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 0f);
				}
			}
		}

		public void set_body(int id, int fxlevel)
		{
			bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
			if (flag)
			{
				this.m_roleDta.m_BodyID = id;
				this.m_roleDta.m_BodyFXID = fxlevel;
			}
			else
			{
				this.m_proAvatar.set_body(id, fxlevel);
			}
		}

		public void rebind_ani()
		{
			this.m_curAni.Rebind();
			bool flag = this.m_roleDta.m_WindID > 0;
			if (flag)
			{
				this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 1f);
			}
			else
			{
				this.m_curAni.SetFloat(EnumAni.ANI_F_FLY, 0f);
			}
		}

		public void set_equip_color(uint id)
		{
			bool flag = this.viewType != BaseRole.VIEW_TYPE_ALL;
			if (flag)
			{
				this.m_roleDta.m_EquipColorID = id;
			}
			else
			{
				this.m_proAvatar.set_equip_color(id);
			}
		}

		public void FlyWing(float time)
		{
			bool flag = this.m_Wing_Animstate != null;
			if (flag)
			{
				this.m_Wing_Animstate.speed = 4f;
				this.m_fWingTime = time;
			}
		}

		public override void dispose()
		{
			base.dispose();
			UnityEngine.Object.Destroy(this.m_curGameObj);
			bool flag = this.m_myPetBird != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_myPetBird.gameObject);
			}
		}

		public void refreshViewData1(Variant v)
		{
			int num = v["carr"];
			bool flag = v.ContainsKey("eqp");
			if (flag)
			{
				this.m_roleDta.m_BodyID = 0;
				this.m_roleDta.m_BodyFXID = 0;
				this.m_roleDta.m_EquipColorID = 0u;
				this.m_roleDta.m_Weapon_LID = 0;
				this.m_roleDta.m_Weapon_LFXID = 0;
				this.m_roleDta.m_Weapon_RID = 0;
				this.m_roleDta.m_Weapon_RFXID = 0;
				foreach (Variant current in v["eqp"]._arr)
				{
					a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(current["tpid"]);
					bool flag2 = itemDataById.equip_type == 3;
					if (flag2)
					{
						int tpid = (int)itemDataById.tpid;
						int bodyFXID = current["intensify"];
						this.m_roleDta.m_BodyID = tpid;
						this.m_roleDta.m_BodyFXID = bodyFXID;
						uint equipColorID = 0u;
						bool flag3 = current.ContainsKey("colour");
						if (flag3)
						{
							equipColorID = current["colour"];
						}
						this.m_roleDta.m_EquipColorID = equipColorID;
					}
					bool flag4 = itemDataById.equip_type == 6;
					if (flag4)
					{
						int tpid2 = (int)itemDataById.tpid;
						int num2 = current["intensify"];
						switch (num)
						{
						case 2:
							this.m_roleDta.m_Weapon_RID = tpid2;
							this.m_roleDta.m_Weapon_RFXID = num2;
							break;
						case 3:
							this.m_roleDta.m_Weapon_LID = tpid2;
							this.m_roleDta.m_Weapon_LFXID = num2;
							break;
						case 5:
							this.m_roleDta.m_Weapon_LID = tpid2;
							this.m_roleDta.m_Weapon_LFXID = num2;
							this.m_roleDta.m_Weapon_RID = tpid2;
							this.m_roleDta.m_Weapon_RFXID = num2;
							break;
						}
					}
				}
			}
			bool flag5 = v.ContainsKey("wing");
			if (flag5)
			{
				this.m_roleDta.m_WindID = v["wing"];
				this.m_roleDta.m_WingFXID = v["wing"];
			}
			bool flag6 = v.ContainsKey("ach_title");
			if (flag6)
			{
				base.title_id = v["ach_title"];
				base.isactive = v["title_display"]._bool;
				PlayerNameUIMgr.getInstance().refreshTitlelv(this, base.title_id);
			}
			bool flag7 = v.ContainsKey("lvl");
			if (flag7)
			{
				this.lvl = v["lvl"];
			}
			bool flag8 = v.ContainsKey("combpt");
			if (flag8)
			{
				this.combpt = v["combpt"];
			}
			bool flag9 = v.ContainsKey("clname");
			if (flag9)
			{
				this.clanName = v["clname"];
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.m_unCID);
			arrayList.Add(this.combpt);
			bool flag10 = BaseProxy<FriendProxy>.getInstance() != null;
			if (flag10)
			{
				BaseProxy<FriendProxy>.getInstance().reFreshProfessionInfo(arrayList);
			}
			base.refreshViewType(BaseRole.VIEW_TYPE_ALL);
			this.onRefreshViewType();
		}
	}
}
