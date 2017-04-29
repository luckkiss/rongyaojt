using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class LGAvatarGameInst : LGAvatar
	{
		public const uint SKILL_TAR_SELF = 1u;

		public const uint SKILL_TAR_AILY = 2u;

		public const uint SKILL_TAR_ENEMY = 4u;

		public const uint SKILL_TAR_OTHER = 8u;

		protected const uint SKILL_INTERVAL = 1u;

		protected LGAvatarGameInst _selectLgAvatar;

		private bool _showSkill = true;

		private bool _only_self_skill_effect = true;

		protected Variant _states = new Variant();

		protected float _skillcd = 5f;

		protected bool _isCastingSkill = false;

		protected float _castingTm = 1000f;

		protected float _curCastingTm = 0f;

		public int hp;

		public int max_hp;

		private Variant _viewInfo;

		private GameObject _pGameobject;

		private GameObject _gameObj;

		public bool needRefreshY = true;

		private int _countToSend = 0;

		private Action<Variant> _onMoveReach;

		private Dictionary<string, int> dEffing = new Dictionary<string, int>();

		public Dictionary<string, string> dCacheEffect = new Dictionary<string, string>();

		public Action<LGAvatarGameInst> grLoadedhandle;

		private float m_dead_Burn = 0f;

		private bool m_bchangeDeadMtl = false;

		public override Variant viewInfo
		{
			get
			{
				return this._viewInfo;
			}
			set
			{
				bool flag = value.ContainsKey("hp");
				if (flag)
				{
					this.hp = value["hp"];
					bool flag2 = value.ContainsKey("max_hp");
					if (flag2)
					{
						this.max_hp = value["max_hp"];
					}
					else
					{
						bool flag3 = value.ContainsKey("battleAttrs");
						if (flag3)
						{
							value["max_hp"] = (this.max_hp = value["battleAttrs"]["max_hp"]);
						}
					}
				}
				this._viewInfo = value;
			}
		}

		public override Variant data
		{
			get
			{
				return this._viewInfo;
			}
			set
			{
				this._viewInfo = value;
			}
		}

		public GameObject pGameobject
		{
			get
			{
				bool flag = this._pGameobject == null;
				GameObject result;
				if (flag)
				{
					bool flag2 = this.grAvatar == null || this.grAvatar.m_char == null || this.grAvatar.m_char.gameObject == null;
					if (flag2)
					{
						result = null;
						return result;
					}
					this._pGameobject = this.grAvatar.m_char.gameObject.transform.parent.gameObject;
				}
				result = this._pGameobject;
				return result;
			}
		}

		public GameObject gameObj
		{
			get
			{
				bool flag = this._gameObj == null;
				if (flag)
				{
					this._gameObj = this.grAvatar.m_char.gameObject;
				}
				return this._gameObj;
			}
		}

		public Vector3 unityPos
		{
			get
			{
				bool flag = this.pGameobject == null;
				Vector3 result;
				if (flag)
				{
					result = Vector3.zero;
				}
				else
				{
					result = this.pGameobject.transform.position;
				}
				return result;
			}
		}

		public override float x
		{
			get
			{
				bool flag = this.pGameobject != null;
				float result;
				if (flag)
				{
					result = this.pGameobject.transform.position.x;
				}
				else
				{
					result = this.viewInfo["x"]._float;
				}
				return result;
			}
		}

		public override float y
		{
			get
			{
				bool flag = this.pGameobject != null;
				float result;
				if (flag)
				{
					result = this.pGameobject.transform.position.z;
				}
				else
				{
					result = this.viewInfo["y"]._float;
				}
				return result;
			}
		}

		public int max_dp
		{
			get
			{
				return this.data["max_dp"];
			}
		}

		public int mp
		{
			get
			{
				bool flag = !this.data.ContainsKey("mp");
				int result;
				if (flag)
				{
					result = 1000;
				}
				else
				{
					result = this.data["mp"];
				}
				return result;
			}
		}

		public int max_mp
		{
			get
			{
				bool flag = !this.data.ContainsKey("max_mp");
				int result;
				if (flag)
				{
					result = 1000;
				}
				else
				{
					result = this.data["max_mp"];
				}
				return result;
			}
		}

		public int dp
		{
			get
			{
				bool flag = !this.data.ContainsKey("dp");
				int result;
				if (flag)
				{
					result = 1000;
				}
				else
				{
					result = this.data["dp"];
				}
				return result;
			}
		}

		public float skillcd
		{
			get
			{
				return this._skillcd;
			}
		}

		public bool in_pczone
		{
			get
			{
				return this.data["in_pczone"]._bool;
			}
			set
			{
				this.data["in_pczone"] = value;
			}
		}

		public bool follow
		{
			get
			{
				return this.data["follow"]._bool;
			}
			set
			{
				this.data["follow"] = value;
			}
		}

		public bool ghost
		{
			get
			{
				return this.data["ghost"]._bool;
			}
			set
			{
				this.data["ghost"] = value;
			}
		}

		public uint ride_mon
		{
			get
			{
				return this.data["ride_mon"]._uint;
			}
			set
			{
				this.data["ride_mon"] = value;
			}
		}

		public uint iid
		{
			get
			{
				bool flag = this.data.ContainsKey("iid") && this.data["iid"] != null;
				uint result;
				if (flag)
				{
					result = this.data["iid"];
				}
				else
				{
					result = 0u;
				}
				return result;
			}
		}

		public LGAvatarGameInst(gameManager m) : base(m)
		{
		}

		public override void init()
		{
			base.addEventListener(2280u, new Action<GameEvent>(this.onClick));
			base.init();
		}

		private void rmvEventListener()
		{
			base.removeEventListener(2280u, new Action<GameEvent>(this.onClick));
			this.onRmoveEventListener();
		}

		protected virtual void onRmoveEventListener()
		{
		}

		public void refreshY()
		{
			this.grAvatar.updateY();
		}

		public void tryRefrershY()
		{
			bool flag = !this.needRefreshY;
			if (!flag)
			{
				bool flag2 = this.grAvatar == null;
				if (!flag2)
				{
					this.needRefreshY = !this.grAvatar.updateY();
				}
			}
		}

		public Variant getStates()
		{
			return this._states;
		}

		public virtual int attackRange(Variant skilconf = null)
		{
			int skillCastRang = this.getSkillCastRang(skilconf);
			return 1 + skillCastRang;
		}

		public bool isInAttackRange(LGAvatar lga)
		{
			return base.IsInRange(lga, (float)this.attackRange(null), false);
		}

		public bool isInSkillRange(LGAvatar lga, SkillData sdta)
		{
			return base.IsInRange(lga, sdta.range, false);
		}

		public bool isInSkillRange(LGAvatar lga, Variant skilconf)
		{
			int skillCastRang = this.getSkillCastRang(skilconf);
			int num = 1 + skillCastRang;
			return base.IsInRange(lga, (float)num, false);
		}

		public bool isInSkillRange(float x, float y, Variant skilconf)
		{
			int skillCastRang = this.getSkillCastRang(skilconf);
			int num = 1 + skillCastRang;
			return base.IsInRange(x, y, (float)num, false);
		}

		public int getSkillCastRang(Variant skilconf)
		{
			return (skilconf != null && skilconf.ContainsKey("range")) ? skilconf["range"]._int : 0;
		}

		public void _cast_success(Variant data)
		{
			Variant data2 = this.data;
			uint @uint = data["sid"]._uint;
			int sex = 0;
			bool flag = data2.ContainsKey("sex");
			if (flag)
			{
				sex = data2["sex"]._int;
			}
			bool flag2 = data.ContainsKey("x");
			if (flag2)
			{
				this.playSkill(@uint, sex, data.ContainsKey("to_iid") ? data["to_iid"]._uint : 0u, data["x"], data["y"]);
			}
			else
			{
				this.playSkill(@uint, sex, data.ContainsKey("to_iid") ? data["to_iid"]._uint : 0u, 0f, 0f);
			}
		}

		protected virtual void playSkill(uint sid, int sex, uint toIID, float tx = 0f, float ty = 0f)
		{
			bool destroy = base.destroy;
			if (!destroy)
			{
				SkillXmlData skillXml = ModelBase<SkillModel>.getInstance().getSkillXml(sid);
				bool flag = skillXml.target_type == 1;
				if (flag)
				{
					LGAvatarGameInst lGAvatarGameInst = base.get_Character_by_iid(toIID);
					bool useJump = skillXml.useJump;
					if (useJump)
					{
						bool flag2 = lGAvatarGameInst != null;
						if (flag2)
						{
							this.jump(skillXml, lGAvatarGameInst);
						}
					}
					else
					{
						bool flag3 = toIID > 0u;
						if (flag3)
						{
							bool flag4 = lGAvatarGameInst != null && lGAvatarGameInst != this;
							if (flag4)
							{
								base.RotationToPt(lGAvatarGameInst.x, lGAvatarGameInst.y);
							}
						}
						this.playSkillAction(sid, (sex == 0) ? skillXml.eff : skillXml.eff_female, lGAvatarGameInst);
					}
				}
				bool flag5 = skillXml.target_type == 2;
				if (flag5)
				{
					bool useJump2 = skillXml.useJump;
					if (useJump2)
					{
						this.jump(skillXml, tx, ty);
						this.playSkillAction(sid, null, null);
					}
					else
					{
						base.RotationToPt(tx, ty);
						this.playSkillAction(sid, (sex == 0) ? skillXml.eff : skillXml.eff_female, null);
					}
				}
			}
		}

		public void playSkillAction(uint sid, string eff = null, LGAvatarGameInst to_lc = null)
		{
			bool destroy = base.destroy;
			if (!destroy)
			{
				this._isCastingSkill = true;
				this._curCastingTm = (float)this.g_mgr.g_netM.CurServerTimeStampMS;
				bool flag = base.IsMoving();
				if (flag)
				{
					base.stop();
				}
				bool flag2 = to_lc != null;
				if (flag2)
				{
					bool flag3 = to_lc != this;
					if (flag3)
					{
						base.RotationToPt(to_lc.x, to_lc.y);
					}
				}
				Variant data = this.data;
				base.play_animation(string.Concat(sid), 1);
				bool flag4 = eff != null;
				if (flag4)
				{
					MapEffMgr.getInstance().play(eff, this.pGameobject.transform, 450f - this.lg_ori_angle, 0f);
				}
			}
		}

		protected virtual void onClick(GameEvent e)
		{
			this.operationLgAvatarSet(this);
		}

		protected void operationLgAvatarSet(LGAvatarGameInst lga)
		{
			bool flag = base.lgMainPlayer.selectTarget != null;
			if (flag)
			{
			}
			bool flag2 = !base.isMainPlayer();
			if (flag2)
			{
				base.lgMainPlayer.selectTarget = lga;
			}
			bool flag3 = this._selectLgAvatar != null;
			if (flag3)
			{
				this._selectLgAvatar.removeEventListener(2205u, new Action<GameEvent>(this.operationLgaDie));
				this._selectLgAvatar.removeEventListener(2285u, new Action<GameEvent>(this.operationLgaDie));
			}
			this._selectLgAvatar = lga;
			bool flag4 = this._selectLgAvatar == null;
			if (!flag4)
			{
				this._selectLgAvatar.addEventListener(2205u, new Action<GameEvent>(this.operationLgaDie));
				this._selectLgAvatar.addEventListener(2285u, new Action<GameEvent>(this.operationLgaDie));
				bool flag5 = !this._selectLgAvatar.isMainPlayer();
				if (flag5)
				{
				}
			}
		}

		protected virtual void onOperationLgaClear()
		{
		}

		private void operationLgaDie(GameEvent e)
		{
			this.operationLgAvatarClear();
		}

		protected void operationLgAvatarClear()
		{
			bool flag = this._selectLgAvatar == null;
			if (!flag)
			{
				this.onOperationLgaClear();
				this._selectLgAvatar.removeEventListener(2285u, new Action<GameEvent>(this.operationLgaDie));
				this._selectLgAvatar = null;
			}
		}

		public void onDie(Variant data)
		{
			this.Die(data);
			this.grAvatar.curHp = 0;
		}

		private void onAttack(GameEvent e)
		{
			uint @uint = e.data["frm_iid"]._uint;
			bool flag = @uint != this.getIid();
			if (!flag)
			{
				this.addAttack(e.data["to_iid"]._uint, e.data);
			}
		}

		protected void addAttack(uint tar_iid, Variant data = null)
		{
		}

		public void onRespawn(Variant info)
		{
			this.Respawn(info);
			this.respawn(info);
		}

		protected virtual void respawn(Variant data)
		{
		}

		protected bool isTrangFan(Variant skilconf)
		{
			return false;
		}

		public void jump(SkillXmlData jumpDta, LGAvatarGameInst to_lc)
		{
			base.stop();
			base.RotationToPt(to_lc.x, to_lc.y);
			Transform transform = to_lc.gameObj.transform;
			this.pGameobject.transform.position = transform.position;
			this.setPos(to_lc.x, to_lc.y);
			Vector3 position = this.gameObj.transform.position;
			Vector3 position2 = transform.position;
			float num = Vector3.Distance(position, position2);
			Vector3 end = Vector3.Lerp(position, position2, (float)((double)num - 1.5) / num);
			bool flag = jumpDta.jump_canying != "null";
			if (flag)
			{
				MapEffMgr.getInstance().playMoveto(jumpDta.jump_canying, position, end, 0.4f);
			}
			bool flag2 = jumpDta.eff != "null";
			if (flag2)
			{
				MapEffMgr.getInstance().play(jumpDta.eff, transform.position, transform.rotation, 0f, null);
			}
		}

		public void jump(SkillXmlData jumpDta, float gezi_distance)
		{
			base.stop();
			LGMap lGMap = GRClient.instance.g_gameM.getObject("LG_MAP") as LGMap;
			Vec2 farthestGPosByOri = lGMap.getFarthestGPosByOri(this.x, this.y, this.lg_ori_angle * 3.14159274f / 180f, gezi_distance);
			this.setPos(farthestGPosByOri.x * 32f, farthestGPosByOri.y * 32f);
		}

		public void jump(SkillXmlData jumpDta, float tx, float ty)
		{
			base.stop();
			Vector3 position = this.gameObj.transform.position;
			Vector3 vector = new Vector3(tx / 53.333f, GRClient.instance.getZ(tx, ty), ty / 53.333f);
			float num = Vector3.Distance(position, vector);
			Vector3 end = Vector3.Lerp(position, vector, (float)((double)num - 1.5) / num);
			bool flag = jumpDta.jump_canying != "null";
			if (flag)
			{
				MapEffMgr.getInstance().playMoveto(jumpDta.jump_canying, position, end, 0.4f);
			}
			bool flag2 = jumpDta.eff != "null";
			if (flag2)
			{
				MapEffMgr.getInstance().play(jumpDta.eff, vector, this.gameObj.transform.rotation, 0f, null);
			}
			this.setPos(tx, ty);
		}

		public float getEffAngle(Variant rotate, LGAvatarGameInst frm_lc, LGAvatarGameInst to_lc)
		{
			bool flag = rotate == null;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float num = 0f;
				int @int = rotate["tp"]._int;
				bool flag2 = 1 == @int;
				if (flag2)
				{
					bool flag3 = frm_lc == null || to_lc == null;
					if (flag3)
					{
						result = num;
						return result;
					}
					Vec2 vec = new Vec2(to_lc.x - frm_lc.x, to_lc.y - frm_lc.y);
					num = (float)vec.angleBetween(GameConstant.EFF_DEF_ORI);
					bool flag4 = to_lc.x < frm_lc.x;
					if (flag4)
					{
						num = -num;
					}
				}
				else
				{
					bool flag5 = frm_lc == null;
					if (flag5)
					{
						result = num;
						return result;
					}
					num = (float)(1.5707963267948966 - (double)frm_lc.lg_ori_angle * 3.1415926535897931 / 180.0);
				}
				result = num;
			}
			return result;
		}

		public bool can_play_skill_effect(uint from_iid, uint sid = 0u)
		{
			bool flag = !this._showSkill;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool only_self_skill_effect = this._only_self_skill_effect;
				result = (!only_self_skill_effect || from_iid == base.lgMainPlayer.getIid());
			}
			return result;
		}

		public void OnHurt(Variant data, LGAvatarGameInst frm_c)
		{
			bool flag = base.isMainPlayer();
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = frm_c != null;
			if (flag4)
			{
				flag2 = frm_c.isMainPlayer();
				flag3 = (frm_c is LGAvatarHero && ((LGAvatarHero)frm_c).isUserOwnHero);
			}
			int id = data.ContainsKey("skill_id") ? data["skill_id"]._int : 0;
			bool flag5 = data.ContainsKey("isdie") && data["isdie"]._bool;
			bool flag6 = data.ContainsKey("dmg") && data["dmg"]._int != 0;
			if (flag6)
			{
				int num = -data["dmg"]._int;
				int num2 = 0;
				bool flag7 = data.ContainsKey("hprest");
				if (flag7)
				{
					num2 = data["hprest"];
				}
				bool flag8 = flag;
				if (flag8)
				{
					ModelBase<PlayerModel>.getInstance().modHp(num2);
					SkillXmlData skillXml = ModelBase<SkillModel>.getInstance().getSkillXml((uint)id);
					bool hitfall = skillXml.hitfall;
					if (hitfall)
					{
						base.setMeshAni("hitfall", 1);
					}
				}
				else
				{
					bool flag9 = this is LGAvatarHero;
					if (flag9)
					{
						uint mid = this.getMid();
					}
				}
				this.modHp(num2);
			}
			else
			{
				bool flag10 = data.ContainsKey("hpadd");
				if (flag10)
				{
					int @int = data["hpadd"]._int;
					int hprest = data["hprest"];
					this.modHp(data["hprest"]);
					bool flag11 = flag;
					if (flag11)
					{
						ModelBase<PlayerModel>.getInstance().modHp(hprest);
					}
				}
			}
			bool flag12 = flag5;
			if (flag12)
			{
				bool flag13 = !flag2 && !flag3;
				if (flag13)
				{
					this.Die(data);
				}
			}
			bool flag14 = this._isCastingSkill && this._curCastingTm + this._castingTm < (float)this.g_mgr.g_netM.CurServerTimeStampMS;
			if (flag14)
			{
				this._isCastingSkill = false;
			}
			bool flag15 = !flag && (flag2 | flag3);
			if (flag15)
			{
				data["hurteff"] = true;
			}
		}

		public void modSpeed(int v)
		{
			this.data["speed"]._int = v;
		}

		public void modHp(int curhp)
		{
			int num = this.hp;
			this.hp = curhp;
			bool flag = this.hp > this.max_hp;
			if (flag)
			{
				this.hp = this.max_hp;
			}
			bool flag2 = this.grAvatar != null;
			if (flag2)
			{
				this.grAvatar.setUIHp(this.hp, this.max_hp);
			}
		}

		public void modMp(int add)
		{
			int num = this.data["mp"]._int + add;
			bool flag = num > this.data["max_mp"]._int;
			if (flag)
			{
				num = this.data["max_mp"]._int;
			}
			this.data["mp"]._int = num;
			this.onModMp(add);
		}

		protected virtual void onModMp(int add)
		{
		}

		public void modDp(int add)
		{
			int num = this.data["dp"]._int + add;
			bool flag = num > this.data["max_dp"]._int;
			if (flag)
			{
				num = this.data["max_dp"]._int;
			}
			this.onModDp(add);
			this.data["dp"]._int = num;
		}

		protected virtual void onModDp(int add)
		{
		}

		public void modMaxDp(int max_v)
		{
			float num = (float)(this.dp / this.max_dp);
			this.data["max_dp"] = max_v;
			this.data["dp"] = (int)((float)max_v * num);
		}

		public void modMaxHp(int max_v, int cur_v = -1)
		{
			int num = (cur_v < 0) ? this.hp : cur_v;
			this.data["max_hp"] = max_v;
			this.max_hp = max_v;
			InterfaceMgr.doCommandByLua("PlayerModel:getInstance().modHp", "model/PlayerModel", new object[]
			{
				num,
				ModelBase<PlayerModel>.getInstance().max_hp
			});
		}

		public virtual void Die(Variant data)
		{
			this.removeAllEff();
			base.setDie();
			base.stop();
			this.operationLgAvatarClear();
			this.rmvEventListener();
			bool flag = this.grAvatar.m_charShadow != null;
			if (flag)
			{
				this.grAvatar.m_charShadow.SetActive(false);
			}
			base.dispatchEvent(GameEvent.Createimmedi(2285u, this, null, false));
		}

		public void addMoving(Variant data)
		{
			Variant moveInfo = new Variant();
			bool flag = data == null || !data.ContainsKey("to_x");
			if (flag)
			{
				GameTools.PrintError("addMoving err!");
			}
			else
			{
				float num = data["to_x"]._float / 53.333f;
				float num2 = data["to_y"]._float / 53.333f;
				Debug.Log(string.Concat(new object[]
				{
					"收到移动消息 ",
					num,
					"  ",
					num2
				}));
				base.setMoveInfo(moveInfo);
			}
		}

		private void navPlayAni(string ani, bool loop)
		{
			base.setMeshAni(ani, loop ? 0 : 1);
		}

		protected override void onStop()
		{
		}

		public void move_byOri(float direction, float maxDis)
		{
		}

		public void movePos(float tx, float ty)
		{
			Variant variant = new Variant();
		}

		public void movePosSingle(float tx, float ty, Action<Variant> onMoveReach, Variant userData = null, float range = 0f, LGAvatarGameInst target = null)
		{
		}

		public void addBuffer(int buff)
		{
			string text = "buff_" + buff;
			this.addEffect(text, text, false);
		}

		public void removeBuffer(int buff)
		{
			string id = "buff_" + buff;
			this.removeEffect(id);
		}

		public void removeAllEff()
		{
			foreach (string current in this.dEffing.Keys)
			{
				this.removeEffect(current);
			}
			this.dEffing.Clear();
		}

		public void addEffect(string id, string eff, bool needCache = false)
		{
			bool destroy = base.destroy;
			if (!destroy)
			{
				bool flag = this.grAvatar != null && this.grAvatar.inited && this.data != null;
				if (flag)
				{
					string text = this.data["iid"]._str + id;
					EffectItem effectItem = MapEffMgr.getInstance().addEffItem(text, eff, MapEffMgr.TYPE_AUTO, this.pGameobject.transform);
					effectItem.scale = this.grAvatar.m_char.scale.x;
					bool flag2 = !effectItem.isAutoRemove;
					if (flag2)
					{
						this.dEffing[text] = 1;
					}
				}
				else if (needCache)
				{
					this.dCacheEffect[id] = eff;
				}
			}
		}

		public void removeEffect(string id)
		{
			bool flag = this.data == null;
			if (!flag)
			{
				string text = this.data["iid"]._str + id;
				this.dEffing.Remove(text);
				MapEffMgr.getInstance().removeEffItem(text);
			}
		}

		public void onGrLoaded(GRAvatar gr)
		{
			foreach (string current in this.dCacheEffect.Keys)
			{
				this.addEffect(current, this.dCacheEffect[current], false);
			}
			this.dCacheEffect.Clear();
			bool flag = this.grLoadedhandle != null && !base.destroy;
			if (flag)
			{
				this.grLoadedhandle(this);
				this.grLoadedhandle = null;
			}
		}

		protected virtual void onMovePosSingleReach(GameEvent e)
		{
			base.stop();
			bool flag = this._onMoveReach != null;
			if (flag)
			{
				this._onMoveReach(e.data);
			}
		}

		public void Respawn(Variant data)
		{
			this.m_visible = true;
			bool bchangeDeadMtl = this.m_bchangeDeadMtl;
			if (bchangeDeadMtl)
			{
				(this as LGAvatarMonster).grAvatar.m_char.changeMtl(gameST.CHAR_MTL);
				this.m_bchangeDeadMtl = false;
			}
			base.setStand();
			this.setPos(data["x"]._float, data["y"]._float);
		}

		public virtual bool onAniEnd()
		{
			bool flag = base.IsDie();
			bool result;
			if (flag)
			{
				bool flag2 = this is LGAvatarMonster;
				if (flag2)
				{
					this.grAvatar.playDrop_jinbi_fx();
					this.grAvatar.m_char.changeMtl(gameST.DEAD_MTL);
					this.m_bchangeDeadMtl = true;
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		protected override void onDispose()
		{
			this.grLoadedhandle = null;
			this.dCacheEffect.Clear();
			foreach (string current in this.dEffing.Keys)
			{
				MapEffMgr.getInstance().removeEffItem(current);
			}
			this.dEffing.Clear();
			base.onDispose();
		}

		public override void updateProcess(float tmSlice)
		{
			this.tryRefrershY();
			bool bchangeDeadMtl = this.m_bchangeDeadMtl;
			if (bchangeDeadMtl)
			{
				bool flag = this is LGAvatarMonster && this.m_dead_Burn < 1f;
				if (flag)
				{
					this.m_dead_Burn += tmSlice * 0.75f;
					this.grAvatar.m_char.setMtlFloat(gameST.DEAD_MT_AMOUNT, this.m_dead_Burn);
				}
			}
		}
	}
}
