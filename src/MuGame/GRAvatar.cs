using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class GRAvatar : GRBaseImpls, IObjectPlugin
	{
		public int m_nSex = -1;

		public int[] m_nOtherDress = new int[4];

		private bool _isMain = false;

		private bool _isCreateName = false;

		public string _name;

		public string _id;

		public string _fileName;

		private List<IUIImageNum> _imgNums = new List<IUIImageNum>();

		private float _speed = 3f;

		private GRCharacter3D m_skMount;

		private bool m_blinkToMount = false;

		private Transform m_tfChar;

		private Transform m_tfMount;

		private float m_ftargetRot_Y = 0f;

		public LGAvatarGameInst lgAvatar;

		private GameObject m_rib_Wp;

		private bool m_rib_Active;

		public Variant _charInfo;

		public GameObject m_charShadow;

		private Dictionary<uint, List<avatarInfo>> _avatarMap = new Dictionary<uint, List<avatarInfo>>();

		private Dictionary<uint, Variant> m_conf = new Dictionary<uint, Variant>();

		private Dictionary<uint, bool> m_bool = new Dictionary<uint, bool>();

		public bool inited = false;

		private float m_fhitFlash_time = 0f;

		private bool m_bhitFlashGoUp = false;

		private bool m_bDoHitFlash = false;

		private Camera cameraText;

		private Vector3 lastHeadpos;

		private Vector3 lastNamePlayerPos;

		private Vector3 headoffset;

		private Vector3 middleoffset;

		private Transform _pTrans;

		public int curHp;

		public int MaxHp;

		private int m_nBornStep = 0;

		private float m_fBornAlpha = 0f;

		private float _hurtTm = 0f;

		private float time;

		protected bool _isInitDp = false;

		protected bool _isLoad = false;

		protected Variant _dpConf;

		private Variant _dpShowInfo;

		private string avatarid
		{
			get
			{
				return this._charInfo["avatarid"]._str;
			}
		}

		public GRCharacter3D m_char
		{
			get
			{
				return this.m_gr as GRCharacter3D;
			}
		}

		public LGCamera lgcam
		{
			get
			{
				return this.g_mgr.g_gameM.getObject("LG_CAMERA") as LGCamera;
			}
		}

		public ClientItemsConf itmesConf
		{
			get
			{
				return this.g_mgr.g_gameConfM.getObject("items") as ClientItemsConf;
			}
		}

		public Transform pTrans
		{
			get
			{
				bool flag = this._pTrans == null;
				Transform result;
				if (flag)
				{
					bool flag2 = this.m_char == null || this.m_char.gameObject == null;
					if (flag2)
					{
						result = null;
						return result;
					}
					this._pTrans = this.m_char.gameObject.transform.parent.transform;
				}
				result = this._pTrans;
				return result;
			}
		}

		public GRAvatar(muGRClient ctrl) : base(ctrl)
		{
			this._charInfo = new Variant();
			this._charInfo["x"] = 0;
			this._charInfo["y"] = 0;
			this._charInfo["name"] = "";
			this._charInfo["ani"] = "idle";
			this._charInfo["ori"] = 0;
			this._charInfo["loop"] = false;
			this._charInfo["avatarid"] = "";
			this._charInfo["equip"] = new Variant();
		}

		public override void initLg(lgGDBase lgbase)
		{
			bool flag = lgbase is LGAvatarGameInst;
			if (flag)
			{
				this.lgAvatar = (lgbase as LGAvatarGameInst);
			}
		}

		public void startMount(string id)
		{
			bool flag = this.m_skMount == null;
			if (flag)
			{
				this.m_skMount = (GRClient.instance.world.createEntity(Define.GREntityType.CHARACTER) as GRCharacter3D);
				this.m_skMount.load(GraphManager.singleton.getCharacterConf(id), null, null);
				this.m_tfChar = this.m_char.gameObject.transform.parent;
				this.m_tfMount = this.m_skMount.gameObject.transform.parent;
				this.m_blinkToMount = true;
				this.m_char.playAnimation("mount_idle", 0);
			}
		}

		public void disMount()
		{
			bool flag = this.m_skMount != null;
			if (flag)
			{
				bool flag2 = !this.m_blinkToMount;
				if (flag2)
				{
					this.m_char.gameObject.transform.SetParent(this.m_tfChar, false);
					this.m_skMount.gameObject.transform.SetParent(this.m_tfMount, false);
				}
				this.m_skMount.dispose();
				this.m_skMount = null;
				this.m_char.playAnimation("idle", 0);
			}
		}

		private Transform getMountBone(Transform root)
		{
			bool flag = root.name.Equals("mount");
			Transform result;
			if (flag)
			{
				result = root;
			}
			else
			{
				for (int i = 0; i < root.childCount; i++)
				{
					Transform mountBone = this.getMountBone(root.GetChild(i));
					bool flag2 = mountBone != null;
					if (flag2)
					{
						result = mountBone;
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		private Transform getWpRibbon(Transform root)
		{
			bool flag = root.name.Equals("RightHand");
			Transform result;
			if (flag)
			{
				result = root;
			}
			else
			{
				for (int i = 0; i < root.childCount; i++)
				{
					Transform wpRibbon = this.getWpRibbon(root.GetChild(i));
					bool flag2 = wpRibbon != null;
					if (flag2)
					{
						result = wpRibbon;
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public void RefreshOtherAvatar()
		{
			bool flag = this.m_char == null || this.m_nSex < 0;
			if (!flag)
			{
				this.m_char.removeAvatar("RightHand");
				this.m_char.removeAvatar("body");
				this.m_char.removeAvatar("hair");
				this.m_char.removeAvatar("Bip01 Spine1");
				string text = this.m_nSex.ToString();
				for (int i = 0; i < this.m_nOtherDress.Length; i++)
				{
					int num = this.m_nOtherDress[i];
					bool flag2 = num > 0;
					if (flag2)
					{
						bool flag3 = GraphManager.singleton.getAvatarConf(text, num.ToString() + text) != null;
						if (flag3)
						{
							this.m_char.applyAvatar(GraphManager.singleton.getAvatarConf(text, num.ToString() + text), null);
						}
						else
						{
							bool flag4 = i == 0;
							if (flag4)
							{
								this.m_char.applyAvatar(GraphManager.singleton.getAvatarConf(text, "1000" + text), null);
							}
						}
					}
					else
					{
						bool flag5 = i == 0;
						if (flag5)
						{
							this.m_char.applyAvatar(GraphManager.singleton.getAvatarConf(text, "1000" + text), null);
						}
					}
				}
			}
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new GRAvatar(m as muGRClient);
		}

		public override void init()
		{
			this.initChaSprHost(140f, 10f, 160f, 150f, 130f);
		}

		protected override void onSetSceneCtrl()
		{
			this.m_ctrl.addEventListener(2205u, new Action<GameEvent>(this.ondispose));
			this.m_ctrl.addEventListener(2100u, new Action<GameEvent>(this.setData));
		}

		private void onDie(GameEvent e)
		{
		}

		protected void initChaSprHost(float titleHeight, float groundHeight, float dynamicHeight, float hpHeight, float dpHeight)
		{
		}

		protected override void onSetGraphImpl()
		{
			this.m_char.addEventListener(Define.EventType.RAYCASTED, new Action<Cross.Event>(this.onMouseDown));
			this.m_charShadow = UnityEngine.Object.Instantiate<GameObject>(gameST.SHADOW_PREFAB);
			this.m_charShadow.transform.SetParent(this.m_char.gameObject.transform.parent, false);
			this.RefreshOtherAvatar();
		}

		private void onMouseDown(Cross.Event e)
		{
			base.dispatchEvent(GameEvent.Createimmedi(2280u, this, null, false));
		}

		private void removeAvatar(Variant eqp)
		{
			uint @uint = eqp["id"]._uint;
			List<Variant> arr = this._charInfo["equip"]._arr;
			for (int i = arr.Count - 1; i >= 0; i--)
			{
				Variant variant = arr[i];
				bool flag = variant["id"]._uint == eqp["id"]._uint;
				if (flag)
				{
					arr.RemoveAt(i);
				}
			}
			this.posMapRmv(eqp["tpid"]._uint, @uint);
		}

		private void posMapAdd(avatarInfo ainfo)
		{
			bool flag = ainfo.pos == 6;
			if (flag)
			{
				bool flag2 = this.m_bool.Count > 0;
				if (flag2)
				{
					foreach (bool current in this.m_bool.Values)
					{
						bool flag3 = current;
						if (flag3)
						{
							ainfo.isLeft = false;
						}
						else
						{
							ainfo.isLeft = true;
						}
					}
				}
				bool flag4 = !this.m_bool.ContainsKey(ainfo.iid);
				if (flag4)
				{
					this.m_bool.Add(ainfo.iid, ainfo.isLeft);
				}
			}
			Variant avatarConf = this.g_mgr.getAvatarConf(this.avatarid, ainfo.avatarid, ainfo.isLeft);
			bool flag5 = avatarConf == null;
			if (!flag5)
			{
				string text = "";
				bool flag6 = ainfo.flvl > 0;
				if (flag6)
				{
					text = this.itmesConf.GetItemFlvlmat(ainfo.tpid, ainfo.flvl);
				}
				bool flag7 = text != "";
				if (flag7)
				{
					this.m_char.applyAvatar(avatarConf, text);
				}
				else
				{
					this.m_char.applyAvatar(avatarConf, null);
				}
				bool flag8 = !this.m_conf.ContainsKey(ainfo.iid);
				if (flag8)
				{
					this.m_conf.Add(ainfo.iid, avatarConf);
				}
			}
		}

		private void posMapRmv(uint tpid, uint iid)
		{
			bool flag = this.m_conf.ContainsKey(iid);
			if (flag)
			{
				Variant variant = this.m_conf[iid];
				this.m_char.removeAvatar(variant["part"]._str);
				this.m_conf.Remove(iid);
				bool flag2 = this.m_bool.ContainsKey(iid);
				if (flag2)
				{
					this.m_bool.Remove(iid);
				}
			}
		}

		public void setOri(float f_rot)
		{
			this._charInfo["ori"] = f_rot;
			float num = 90f - this._charInfo["ori"]._float;
			bool flag = num > 360f;
			if (flag)
			{
				num -= 360f;
			}
			bool flag2 = num < 0f;
			if (flag2)
			{
				num += 360f;
			}
			this.m_ftargetRot_Y = num;
		}

		public void setAni(string anim, int loop)
		{
			bool flag = this.m_char == null;
			if (!flag)
			{
				bool flag2 = this.m_skMount != null;
				if (flag2)
				{
					bool flag3 = loop == 0;
					if (flag3)
					{
						this.m_skMount.playAnimation(anim, 0);
						this.m_char.playAnimation("mount_" + anim, 0);
					}
				}
				else
				{
					this.m_char.playAnimation(anim, loop);
				}
			}
		}

		private void setAni(GameEvent e)
		{
		}

		private void setAni(Variant data)
		{
		}

		private void setData(GameEvent e)
		{
			Variant data = e.data;
			Variant variant = data["avatarConf"];
			bool flag = data.ContainsKey("isCreateName");
			if (flag)
			{
				this._isCreateName = data["isCreateName"]._bool;
			}
			bool flag2 = data.ContainsKey("isMain");
			if (flag2)
			{
				this._isMain = data["isMain"]._bool;
				this._isCreateName = true;
			}
			bool flag3 = variant == null;
			if (!flag3)
			{
				bool flag4 = data.ContainsKey("name");
				if (flag4)
				{
					this._name = data["name"]._str;
					this._charInfo["name"] = this._name;
				}
				this._charInfo["avatarid"] = variant["id"]._str;
				this._id = this._charInfo["avatarid"];
				string str = variant["file"]._str;
				string[] array = str.Split(new char[]
				{
					'/'
				});
				this._fileName = array[array.Length - 1];
				bool flag5 = data.ContainsKey("hp") && data.ContainsKey("max_hp");
				if (flag5)
				{
					this.curHp = (this._charInfo["hp"] = data["hp"]);
					this.MaxHp = (this._charInfo["max_hp"] = data["max_hp"]);
				}
			}
		}

		protected virtual bool charactPlayOver()
		{
			return this.lgAvatar.onAniEnd();
		}

		protected virtual void onLoadFin()
		{
			bool flag = !this.lgAvatar.destroy;
			if (flag)
			{
				this.lgAvatar.onGrLoaded(this);
			}
		}

		private void upDateView(GameEvent e)
		{
			Vec3 poss = this.m_ctrl.getPoss();
			this.updateXYZ(poss.x, poss.y, poss.z);
		}

		public bool updateY()
		{
			return false;
		}

		private void updateXYZ(float unity_x, float unity_y, float unity_z)
		{
			bool flag = this.m_char == null;
			if (!flag)
			{
				bool flag2 = this.pTrans == null;
				if (!flag2)
				{
					this.pTrans.position = new Vector3(unity_x, unity_y, unity_z);
					bool isMain = this._isMain;
					if (isMain)
					{
						this.lgcam.updateMainPlayerPos(unity_x, unity_y, unity_z);
					}
					bool flag3 = !this._isMain && this._isCreateName;
					if (flag3)
					{
						this.changeGroundSprPos();
					}
				}
			}
		}

		public Vector3 getHeadPos()
		{
			bool flag = this.m_char == null;
			Vector3 result;
			if (flag)
			{
				result = Vector3.zero;
			}
			else
			{
				Vector3 vector = this.pTrans.position + this.headoffset;
				bool flag2 = GRMap.GAME_CAM_CAMERA == null;
				if (flag2)
				{
					result = Vector3.zero;
				}
				else
				{
					bool flag3 = InterfaceMgr.ui_Camera_cam == null;
					if (flag3)
					{
						result = Vector3.zero;
					}
					else
					{
						bool flag4 = InterfaceMgr.ui_Camera_cam != null && !InterfaceMgr.ui_Camera_cam.gameObject.active;
						if (flag4)
						{
							InterfaceMgr.ui_Camera_cam.gameObject.SetActive(true);
						}
						vector = GRMap.GAME_CAM_CAMERA.WorldToScreenPoint(vector);
						vector.z = 0f;
						this.lastHeadpos = vector;
						result = vector;
					}
				}
			}
			return result;
		}

		public void onHurt(Variant d)
		{
			Vector3 headPos = this.getHeadPos();
			bool flag = d.ContainsKey("ft");
			if (flag)
			{
				bool flag2 = d.ContainsKey("criatk");
				if (flag2)
				{
					FightText.play(d["ft"], headPos, d["dmg"], d["criatk"], -1);
				}
				else
				{
					FightText.play(d["ft"], headPos, d["dmg"], false, -1);
				}
			}
			bool flag3 = d.ContainsKey("hurteff");
			if (flag3)
			{
				bool flag4 = this.middleoffset == Vector3.zero;
				if (flag4)
				{
					this.middleoffset = new Vector3(0f, this.m_char.chaHeight * 0.5f, 0f);
				}
				bool flag5 = this.pTrans == null;
				if (!flag5)
				{
					bool flag6 = !this.lgAvatar.IsDie() && !this.lgAvatar.m_bSinging;
					if (flag6)
					{
						bool flag7 = this.m_char.curAnimName == "idle" || this.m_char.curAnimName == "run" || this.m_char.curAnimName == "hurt";
						if (flag7)
						{
							this.setAni("hurt", 1);
						}
					}
					uint id = 1001u;
					bool flag8 = d.ContainsKey("skill_id");
					if (flag8)
					{
						id = d["skill_id"];
					}
					SkillHitedXml hitxml = ModelBase<SkillModel>.getInstance().getSkillXml(id).hitxml;
					bool flag9 = hitxml != null;
					if (flag9)
					{
						this.m_char.setMtlColor(gameST.HIT_Rim_Color_nameID, hitxml.rimColor);
						this.m_char.setMtlColor(gameST.HIT_Main_Color_nameID, hitxml.mainColor);
						this.m_bhitFlashGoUp = true;
						this.m_bDoHitFlash = true;
					}
					MapEffMgr.getInstance().play("hurt", this.pTrans.position + this.middleoffset, this.pTrans.eulerAngles, 0f);
				}
			}
		}

		public void playSong(uint skillid)
		{
			bool flag = this.pTrans == null;
			if (!flag)
			{
				SkillXmlData skillXml = ModelBase<SkillModel>.getInstance().getSkillXml(skillid);
				MapEffMgr.getInstance().play(skillXml.eff + "_song", this.pTrans, UnityEngine.Random.Range(0f, 359f), 0f);
			}
		}

		public void playDrop_jinbi_fx()
		{
			bool flag = this.m_char != null && this.m_char.gameObject != null;
			if (flag)
			{
				Transform transform = this.m_char.gameObject.transform;
				bool flag2 = transform.childCount > 0;
				if (flag2)
				{
					Transform child = transform.GetChild(0);
					bool flag3 = child.childCount > 0;
					if (flag3)
					{
						Transform child2 = child.GetChild(0);
						Vector3 pos = child2.localToWorldMatrix.MultiplyPoint(Vector3.zero);
						MapEffMgr.getInstance().play("drop_jinbin", pos, Quaternion.identity, 0f, null);
					}
				}
			}
		}

		public void setUIHp(int cur, int max)
		{
			this.curHp = cur;
			this.MaxHp = max;
		}

		public void onAddEff(Variant d)
		{
			string str = d["effid"]._str;
			bool flag = d.ContainsKey("loop");
			if (flag)
			{
				bool @bool = d["loop"]._bool;
			}
			bool flag2 = d.ContainsKey("angle");
			if (flag2)
			{
				float @float = d["angle"]._float;
			}
			bool flag3 = d.ContainsKey("single");
			if (flag3)
			{
				bool bool2 = d["single"]._bool;
			}
		}

		private void _setProgressData()
		{
		}

		private void creatImgNum(int state, int num)
		{
		}

		private void _imgNumMove(float tmSlice)
		{
			for (int i = 0; i < this._imgNums.Count; i++)
			{
				this._imgNums[i].y -= this._speed * tmSlice * 60f;
				this._imgNums[i].movedistance += this._speed * tmSlice * 60f;
				bool flag = this._imgNums[i].movedistance > 100f;
				if (flag)
				{
					this._imgNums[i].dispose();
					this._imgNums.RemoveAt(i);
					i--;
				}
			}
		}

		private void _disposeImgNum()
		{
			for (int i = 0; i < this._imgNums.Count; i++)
			{
				this._imgNums[i].dispose();
				this._imgNums.RemoveAt(i);
				i--;
			}
		}

		private void ondispose(GameEvent e)
		{
			this.dispose();
		}

		public override void dispose()
		{
			this.disMount();
			this._disposeImgNum();
			bool flag = this.m_char != null;
			if (flag)
			{
				this.g_mgr.deleteEntity(this.m_char);
			}
		}

		public void showWpRibbon()
		{
		}

		public void hideWpRibbon()
		{
		}

		public bool linkWpRibbon()
		{
			bool flag = this.m_rib_Wp != null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Transform wpRibbon = this.getWpRibbon(this.m_char.gameObject.transform);
				bool flag2 = wpRibbon != null;
				if (flag2)
				{
					GameObject original = U3DAPI.U3DResLoad<GameObject>("ribbon/rib_1");
					this.m_rib_Wp = UnityEngine.Object.Instantiate<GameObject>(original);
					this.m_rib_Wp.transform.SetParent(wpRibbon, false);
					this.m_rib_Active = true;
				}
				result = true;
			}
			return result;
		}

		public override void updateProcess(float tmSlice)
		{
			bool flag = this.m_blinkToMount && this.m_skMount != null;
			if (flag)
			{
				bool flag2 = this.m_skMount.gameObject.transform.childCount > 0;
				if (flag2)
				{
					this.m_blinkToMount = false;
					this.m_skMount.gameObject.transform.SetParent(this.m_char.gameObject.transform.parent.transform, false);
					Transform mountBone = this.getMountBone(this.m_skMount.gameObject.transform);
					bool flag3 = mountBone != null;
					if (flag3)
					{
						debug.Log("坐骑的位置 绑定成功");
						GameObject gameObject = new GameObject();
						gameObject.transform.eulerAngles = new Vector3(90f, 270f, 0f);
						gameObject.transform.SetParent(mountBone, false);
						this.m_char.gameObject.transform.SetParent(gameObject.transform, false);
					}
				}
			}
			bool flag4 = this.m_bDoHitFlash && this.m_nBornStep > 1;
			if (flag4)
			{
				bool bhitFlashGoUp = this.m_bhitFlashGoUp;
				if (bhitFlashGoUp)
				{
					this.m_fhitFlash_time += tmSlice * 10f;
					bool flag5 = this.m_fhitFlash_time > 0.25f;
					if (flag5)
					{
						this.m_fhitFlash_time = 0.25f;
						this.m_bhitFlashGoUp = false;
					}
					this.m_char.setMtlFloat(gameST.HIT_Rim_Width_nameID, this.m_fhitFlash_time * 6f);
				}
				else
				{
					this.m_fhitFlash_time -= tmSlice;
					bool flag6 = this.m_fhitFlash_time <= 0f;
					if (flag6)
					{
						this.m_fhitFlash_time = 0f;
						this.m_bDoHitFlash = false;
						this.m_char.setMtlColor(gameST.HIT_Rim_Color_nameID, new Color(0f, 0f, 0f));
						this.m_char.setMtlColor(gameST.HIT_Main_Color_nameID, new Color(1f, 1f, 1f));
					}
					else
					{
						this.m_char.setMtlFloat(gameST.HIT_Rim_Width_nameID, this.m_fhitFlash_time * 6f);
					}
				}
			}
			bool flag7 = this.m_char != null && this.m_char.rotY != this.m_ftargetRot_Y;
			if (flag7)
			{
				float num = this.m_ftargetRot_Y - this.m_char.rotY;
				float num2 = Mathf.Abs(num);
				bool flag8 = num2 < 180f;
				if (flag8)
				{
					bool flag9 = num2 < 4f;
					if (flag9)
					{
						this.m_char.rotY = this.m_ftargetRot_Y;
					}
					else
					{
						this.m_char.rotY = this.m_char.rotY + num * 0.25f;
					}
				}
				else
				{
					bool flag10 = this.m_char.rotY > 180f;
					if (flag10)
					{
						this.m_char.rotY += 16f;
						bool flag11 = this.m_char.rotY >= 360f;
						if (flag11)
						{
							this.m_char.rotY = 1f;
						}
					}
					else
					{
						this.m_char.rotY -= 16f;
						bool flag12 = this.m_char.rotY < 0f;
						if (flag12)
						{
							this.m_char.rotY = 359f;
						}
					}
				}
			}
			float num3 = (float)this.g_mgr.g_netM.CurServerTimeStampMS;
			this._imgNumMove(tmSlice);
			this.time += tmSlice;
		}

		protected void onAddTitle(GameEvent e)
		{
		}

		public void ShowChaDp(bool flag)
		{
		}

		protected void initDpFin()
		{
		}

		public void SetDpShowInfo(Variant info)
		{
		}

		protected void changeGroundSprPos()
		{
		}
	}
}
