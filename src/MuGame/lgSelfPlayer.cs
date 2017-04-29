using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class lgSelfPlayer : LGAvatarGameInst
	{
		public delegate bool boolDelegate(string id);

		public static lgSelfPlayer instance;

		public int[] m_nDress_PartID = new int[4];

		public float m_fCameraNearXZ = 0.517f;

		public float m_fCameraNearH = 3.6f;

		public bool m_bMounting = false;

		private uint _castSkilid = 0u;

		private bool _smartSkil = false;

		private bool _singleAttackFlag = false;

		private bool _attackFlag = false;

		private int _nextSkillTurn = 0;

		public lgSelfPlayer.boolDelegate needAutoNextAttackHanle;

		private Variant _actAchives = new Variant();

		private long _lastSendtm = 0L;

		private int idx = 0;

		private Variant moveTOAttackTarget = null;

		private SkillData jumpSkilld;

		private Dictionary<uint, int> dSkillPreload = new Dictionary<uint, int>();

		private float _castTm = 0f;

		private Dictionary<uint, Variant> _skillCDs = new Dictionary<uint, Variant>();

		private int autoMovingTick = 0;

		private int m_nrefreshWpRibbon = 0;

		private checks _checks = null;

		public override int roleType
		{
			get
			{
				return LGAvatarBase.ROLE_TYPE_USER;
			}
		}

		public override string processName
		{
			get
			{
				return "lgSelfPlayer";
			}
			set
			{
				this._processName = value;
			}
		}

		public int nextSkillTurn
		{
			get
			{
				return this._nextSkillTurn;
			}
			set
			{
				this._nextSkillTurn = value;
			}
		}

		public override Variant viewInfo
		{
			get
			{
				return base.selfInfo.mainPlayerInfo;
			}
		}

		public override Variant data
		{
			get
			{
				return base.selfInfo.m_data;
			}
		}

		public override float x
		{
			get
			{
				return this.viewInfo["x"];
			}
		}

		public override float y
		{
			get
			{
				return this.viewInfo["y"];
			}
		}

		public int cid
		{
			get
			{
				return this.data["cid"]._int;
			}
		}

		public LGAvatarGameInst selectTarget
		{
			get
			{
				return this._selectLgAvatar;
			}
			set
			{
				this._selectLgAvatar = value;
			}
		}

		private LGSkill lgskill
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).getObject("LG_SKILL") as LGSkill;
			}
		}

		private LGIUIOGLoading ogLoading
		{
			get
			{
				return null;
			}
		}

		public lgSelfPlayer(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgSelfPlayer(m as gameManager);
		}

		public override void onInit()
		{
			lgSelfPlayer.instance = this;
			this.g_mgr.g_gameM.addEventListenerCL("LG_JOIN_WORLD", 3034u, new Action<GameEvent>(this.onJoinWorld));
			this.g_mgr.g_gameM.addEventListenerCL("LG_MAP", 2160u, new Action<GameEvent>(this.onChangeMap));
			this.g_mgr.g_netM.addEventListener(40u, new Action<GameEvent>(this.onDetailInfoChangeRes));
			this.g_mgr.g_netM.addEventListener(32u, new Action<GameEvent>(this.onSelfAttChange));
			this.g_mgr.g_netM.addEventListener(61u, new Action<GameEvent>(this.onModExpRes));
		}

		public void RefreshPlayerAvatar()
		{
			this.m_nrefreshWpRibbon = 6;
			this.grAvatar.m_char.removeAvatar("RightHand");
			this.grAvatar.m_char.removeAvatar("body");
			this.grAvatar.m_char.removeAvatar("hair");
			this.grAvatar.m_char.removeAvatar("Bip01 Spine1");
			string text = this.data["sex"];
			for (int i = 0; i < this.m_nDress_PartID.Length; i++)
			{
				int num = this.m_nDress_PartID[i];
				bool flag = num > 0;
				if (flag)
				{
					bool flag2 = GraphManager.singleton.getAvatarConf(text, num.ToString() + text) != null;
					if (flag2)
					{
						this.grAvatar.m_char.applyAvatar(GraphManager.singleton.getAvatarConf(text, num.ToString() + text), null);
					}
					else
					{
						bool flag3 = i == 0;
						if (flag3)
						{
							this.grAvatar.m_char.applyAvatar(GraphManager.singleton.getAvatarConf(text, "1000" + text), null);
						}
					}
				}
				else
				{
					bool flag4 = i == 0;
					if (flag4)
					{
						this.grAvatar.m_char.applyAvatar(GraphManager.singleton.getAvatarConf(text, "1000" + text), null);
					}
				}
			}
		}

		public override bool onAniEnd()
		{
			string ani = this.m_ani;
			bool flag = base.IsDie();
			bool result;
			if (flag)
			{
				this.m_visible = false;
				base.refreshAni();
				result = false;
			}
			else
			{
				bool flag2 = this.needAutoNextAttackHanle == null || !this.needAutoNextAttackHanle(ani);
				if (flag2)
				{
					bool flag3 = base.IsMoving();
					if (flag3)
					{
						base.setTag(10);
					}
					else
					{
						base.setTag(20);
					}
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		public void startMount(string id)
		{
			bool flag = this.grAvatar != null;
			if (flag)
			{
				this.m_fCameraNearH = 5.57f;
				this.m_fCameraNearXZ = 0.95f;
				MouseClickMgr.instance.m_UpdateNearCamNow = true;
				this.grAvatar.startMount(id);
				this.m_bMounting = true;
			}
		}

		public void disMount()
		{
			bool flag = this.grAvatar != null;
			if (flag)
			{
				this.m_fCameraNearH = 3.6f;
				this.m_fCameraNearXZ = 0.517f;
				MouseClickMgr.instance.m_UpdateNearCamNow = true;
				this.grAvatar.disMount();
				this.m_bMounting = false;
			}
		}

		public void on_self_attchange(Variant data)
		{
		}

		public void updateNetData(Variant data)
		{
		}

		public void moveToNpc(int npcid)
		{
			LGAvatarNpc npc = LGNpcs.instance.getNpc(npcid);
			bool flag = npc != null;
			if (flag)
			{
				this.onSelectNpc(npc);
			}
		}

		public void onSelectNpc(LGAvatarNpc npc)
		{
			bool flag = base.IsDie();
			if (!flag)
			{
				npc.faceToAvatar(this);
				bool flag2 = Vector3.Distance(npc.pGameobject.transform.position, base.pGameobject.transform.position) <= 1f;
				if (flag2)
				{
					base.RotationToPt(npc.x, npc.y);
					this._reach_to_npc(npc);
				}
				else
				{
					base.movePosSingle(npc.x, npc.y, delegate(Variant info)
					{
						this._reach_to_npc(npc);
					}, null, 1f, npc);
				}
			}
		}

		public void onSelectOther(LGAvatarOther other)
		{
		}

		private void _reach_to_npc(LGAvatar npc)
		{
			base.stop();
			int num = 0;
			bool flag = num == 2;
			if (!flag)
			{
				bool flag2 = num == 0;
				if (flag2)
				{
					bool flag3 = npc != null;
					if (flag3)
					{
						MouseClickMgr.instance.onSelectNpc(npc as LGAvatarNpc);
					}
				}
			}
		}

		private void onJoinWorld(GameEvent e)
		{
			this.initData();
		}

		private void onSelfAttChange(GameEvent e)
		{
			base.selfInfo.onSelfAttChange(e.data);
		}

		private void onDetailInfoChangeRes(GameEvent e)
		{
			Variant data = e.data;
			this.on_detail_info_change(data);
		}

		private void onModExpRes(GameEvent e)
		{
			Variant data = e.data;
			this.on_mod_exp(data);
		}

		public void on_mod_exp(Variant info)
		{
			this.data["exp"]._int += info["mod_exp"];
			int num = info["mod_exp"];
			string languageText = LanguagePack.getLanguageText("LGUIItemImpl", "getExp");
			string languageText2 = LanguagePack.getLanguageText("LGUIItemImpl", "loseExp");
			bool flag = num > 0;
			string val;
			if (flag)
			{
				val = DebugTrace.Printf(languageText, new string[]
				{
					num.ToString()
				});
			}
			else
			{
				val = DebugTrace.Printf(languageText2, new string[]
				{
					Math.Abs(num).ToString()
				});
			}
			Variant variant = new Variant();
			variant.pushBack(val);
		}

		public void on_lvl_up(Variant info)
		{
		}

		public void SetResetName()
		{
		}

		protected override void onModMp(int add)
		{
		}

		protected override void onModDp(int add)
		{
		}

		public Variant GetActAchives()
		{
			return this._actAchives;
		}

		public void modAttPt(Variant value)
		{
			bool flag = this.data != null;
			if (flag)
			{
				Variant variant = new Variant();
				bool flag2 = value.ContainsKey("strpt");
				if (flag2)
				{
					variant["strpt"] = this.data["strpt"]._int + value["strpt"]._int;
					this.data["str"] = this.data["str"]._int + value["strpt"]._int;
				}
				bool flag3 = value.ContainsKey("conpt");
				if (flag3)
				{
					variant["conpt"] = this.data["conpt"]._int + value["conpt"]._int;
					this.data["con"] = this.data["con"]._int + value["conpt"]._int;
				}
				bool flag4 = value.ContainsKey("intept");
				if (flag4)
				{
					variant["intept"] = this.data["intept"]._int + value["intept"]._int;
					this.data["inte"] = this.data["inte"]._int + value["intept"]._int;
				}
				bool flag5 = value.ContainsKey("agipt");
				if (flag5)
				{
					variant["agipt"] = this.data["agipt"]._int + value["agipt"]._int;
					this.data["agi"] = this.data["agi"]._int + value["agipt"]._int;
				}
				bool flag6 = value.ContainsKey("wispt");
				if (flag6)
				{
					variant["wispt"] = this.data["wispt"]._int + value["wispt"]._int;
				}
			}
			bool flag7 = value.ContainsKey("att_pt");
			if (flag7)
			{
				this.on_detail_info_change(GameTools.createGroup(new Variant[]
				{
					"att_pt",
					value["att_pt"]
				}));
			}
		}

		public void on_detail_info_change(Variant data1)
		{
		}

		private int skillatkChange(int inteBefore, int inteAfter)
		{
			int carr = this.data["carr"];
			int skillDmg = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.GetSkillDmg(carr, inteBefore);
			int skillDmg2 = (this.g_mgr.g_gameConfM as muCLientConfig).svrGeneralConf.GetSkillDmg(carr, inteAfter);
			return skillDmg2 - skillDmg;
		}

		public void SetnNobName()
		{
		}

		public void modAttPt(int data)
		{
		}

		public void SetCloseInfo(Variant data)
		{
		}

		private void initData()
		{
			base.playerInfos.get_player_detailinfo(this.getCid(), new Action<Variant>(this.onGetDetialInfo));
		}

		private void onGetDetialInfo(Variant info)
		{
			base.selfInfo.updataDetial(info);
			bool destroy = base.destroy;
			if (!destroy)
			{
				this.g_mgr.g_sceneM.dispatchEvent(GameEvent.Createimmedi(2183u, this, null, false));
				this.hp = (this.viewInfo["hp"] = ModelBase<PlayerModel>.getInstance().hp);
				this.max_hp = (this.viewInfo["max_hp"] = ModelBase<PlayerModel>.getInstance().max_hp);
				base.dispatchEvent(GameEvent.Create(2100u, this, this.viewInfo, false));
			}
		}

		public override uint getIid()
		{
			return this.data["iid"]._uint;
		}

		public override uint getCid()
		{
			return this.data["cid"]._uint;
		}

		public override void setPos(float x, float y)
		{
			base.selfInfo.setPos((double)x, (double)y);
		}

		public bool canMove()
		{
			bool flag = base.IsDie();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = loading_cloud.instance != null && loading_cloud.instance.showed;
				result = (!flag2 && (this.m_ani == "idle" || this.m_ani == "run"));
			}
			return result;
		}

		public bool canAttack()
		{
			bool flag = base.IsDie();
			return !flag && (this.m_ani == "idle" || this.m_ani == "run");
		}

		public bool canAutoAttack()
		{
			bool flag = base.IsDie();
			return !flag && this.m_ani == "idle";
		}

		public void onJoystickMove(float orgdata)
		{
		}

		public void onJoystickEnd(bool force = false)
		{
			long curServerTimeStampMS = this.g_mgr.g_netM.CurServerTimeStampMS;
			try
			{
				float num = base.pGameobject.transform.position.x * 53.333f;
				float num2 = base.pGameobject.transform.position.z * 53.333f;
				Vector3 vector = new Vector3(num, base.pGameobject.transform.position.y, num2);
				LGCamera.instance.updateMainPlayerPos(false);
				BaseProxy<MoveProxy>.getInstance().sendstop((uint)num, (uint)num2, 1u, (float)curServerTimeStampMS, force);
			}
			catch (Exception var_4_8D)
			{
			}
			base.stop();
			this.onMoveLinkCheck();
		}

		public void onSelectMonster(LGAvatarMonster lga)
		{
		}

		private void onAttackReach(Variant info)
		{
			this.moveTOAttackTarget = null;
		}

		public override bool attack(LGAvatarGameInst lga, bool smartSkill = false, uint skilid = 0u)
		{
			bool flag = lga == null;
			bool result;
			if (flag)
			{
				bool flag2 = skilid > 0u;
				if (flag2)
				{
					this.castSkill(skilid, false);
				}
				else
				{
					base.play_animation("a16", 1);
				}
				result = false;
			}
			else
			{
				bool flag3 = !smartSkill && skilid <= 0u;
				if (flag3)
				{
					skilid = this.getCurDefSkill();
				}
				SkillData skillData = ModelBase<SkillModel>.getInstance().getSkillData(skilid);
				bool needjump = false;
				bool flag4 = skillData.xml.target_type == 1 && !base.isInSkillRange(lga, skillData);
				if (flag4)
				{
					int num = (int)lga.x - (int)this.x;
					int num2 = (int)lga.y - (int)this.y;
				}
				bool flag5 = this.CanAttack(lga);
				result = (flag5 && this.attackEnemy(lga, smartSkill, skilid, needjump));
			}
			return result;
		}

		private bool isInJumpRange(LGAvatarGameInst avatar)
		{
			bool flag = this.jumpSkilld == null;
			if (flag)
			{
				this.jumpSkilld = ModelBase<SkillModel>.getInstance().getSkillData(1008u);
			}
			bool flag2 = this.jumpSkilld == null;
			return !flag2 && base.IsInRange(avatar, this.jumpSkilld.range, false);
		}

		private bool attackFriend(LGAvatarGameInst lga, uint skilid)
		{
			base.operationLgAvatarSet(lga);
			bool flag = skilid > 0u;
			bool result;
			if (flag)
			{
				result = this.castSkill(skilid, false);
			}
			else
			{
				this._singleAttackFlag = true;
				uint @uint = this.data["iid"]._uint;
				base.play_animation("a16", 2);
				result = true;
			}
			return result;
		}

		private bool attackEnemy(LGAvatarGameInst lga, bool smartSkill = false, uint skilid = 0u, bool needjump = false)
		{
			Variant data = lga.data;
			bool flag = !data.ContainsKey("iid");
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				base.operationLgAvatarSet(lga);
				this._castSkilid = skilid;
				this._smartSkil = smartSkill;
				if (smartSkill)
				{
					skilid = this.getSkilSmart();
				}
				bool flag2 = skilid > 0u;
				if (flag2)
				{
					result = this.castSkill(skilid, needjump);
				}
				else
				{
					this._singleAttackFlag = true;
					uint @uint = data["iid"]._uint;
					base.play_animation("a16", 2);
					result = true;
				}
			}
			return result;
		}

		private void stopAttact()
		{
			this.moveTOAttackTarget = null;
			base.setStand();
			this._attackFlag = false;
			this.clearCastSkil();
			bool singleAttackFlag = this._singleAttackFlag;
			if (singleAttackFlag)
			{
				this._singleAttackFlag = false;
			}
		}

		protected override void onStop()
		{
			base.onStop();
			this.moveTOAttackTarget = null;
			base.operationLgAvatarClear();
		}

		private void clearCastSkil()
		{
			this._castSkilid = 0u;
			this._smartSkil = false;
		}

		protected override void onOperationLgaClear()
		{
			SKillCdBt.needAutoNextAttack = false;
		}

		public void doSkillPreload()
		{
			Dictionary<uint, SkillData> skillDatas = ModelBase<SkillModel>.getInstance().skillDatas;
			foreach (SkillData current in skillDatas.Values)
			{
				bool flag = this.dSkillPreload.ContainsKey(current.id);
				if (!flag)
				{
					this.dSkillPreload[current.id] = 1;
					base.play_animation(current.id.ToString(), 1);
					bool flag2 = current.eff != "null";
					if (flag2)
					{
						MapEffMgr.getInstance().play(current.eff, Vector3.zero, Vector3.zero, 0.1f);
					}
				}
			}
		}

		protected override void playSkill(uint sid, int sex, uint toIID, float x = 0f, float y = 0f)
		{
		}

		private bool isFriend(LGAvatarGameInst lga)
		{
			return lga.getIid() == this.getIid();
		}

		private bool CanAttack(LGAvatarGameInst lga)
		{
			bool flag = lga == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = base.IsDie() || lga.IsDie();
				if (flag2)
				{
					result = false;
				}
				else
				{
					Variant data = this.data;
					Variant data2 = lga.data;
					uint @uint = data["cid"]._uint;
					bool flag3 = lga is LGAvatarMonster;
					if (flag3)
					{
						bool flag4 = lga.IsCollect();
						if (flag4)
						{
							result = false;
							return result;
						}
						bool flag5 = data2.ContainsKey("owner_cid");
						if (flag5)
						{
							uint uint2 = data2["owner_cid"]._uint;
							bool flag6 = uint2 == @uint;
							if (flag6)
							{
								result = false;
								return result;
							}
							LGAvatarGameInst lGAvatarGameInst = base.lgOthers.get_player_by_cid(uint2);
							bool flag7 = lGAvatarGameInst != null;
							if (!flag7)
							{
								result = false;
								return result;
							}
							data2 = lGAvatarGameInst.data;
							bool flag8 = data2 == null || data2["level"]._uint < 50u;
							if (flag8)
							{
								result = false;
								return result;
							}
						}
					}
					result = true;
				}
			}
			return result;
		}

		private bool isSkilCanCast(uint sid)
		{
			bool flag = base.IsDie();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.isSkilCD(sid);
				result = !flag2;
			}
			return result;
		}

		public bool isSkilCD(uint sid)
		{
			float num = (float)NetClient.instance.CurServerTimeStampMS;
			bool flag = this._skillCDs.ContainsKey(sid);
			bool result;
			if (flag)
			{
				result = (this._skillCDs[sid]["start_tm"]._float + this._skillCDs[sid]["cd_tm"]._float * 100f > num || this._castTm + this._skillcd * 100f > num);
			}
			else
			{
				result = (this._castTm + this._skillcd * 100f > num);
			}
			return result;
		}

		private uint getSkilSmart()
		{
			bool flag = this._castSkilid > 0u;
			uint result;
			if (flag)
			{
				bool flag2 = base.genConf.IsDefSkill(this.data["carr"]._int, this._castSkilid);
				if (flag2)
				{
					result = this.getCurDefSkill();
					return result;
				}
				bool flag3 = this.isSkilCanCast(this._castSkilid);
				if (flag3)
				{
					result = this._castSkilid;
					return result;
				}
			}
			bool smartSkil = this._smartSkil;
			if (smartSkil)
			{
			}
			result = 0u;
			return result;
		}

		public uint getCurDefSkill()
		{
			Variant carrDefSkill = base.genConf.GetCarrDefSkill(this.data["carr"]._int);
			bool flag = carrDefSkill != null;
			uint result;
			if (flag)
			{
				result = carrDefSkill[base.lgMainPlayer.nextSkillTurn]._uint;
			}
			else
			{
				result = 0u;
			}
			return result;
		}

		public bool castSkill(uint sid, bool needJump = false)
		{
			return false;
		}

		private void onChangeMap(GameEvent e)
		{
			this.x += 0.1f;
			this.y += 0.1f;
		}

		public void onMoveLinkCheck()
		{
			Variant variant = base.lgMap.get_map_link(base.lgMap.pixelToGridSize(this.x), base.lgMap.pixelToGridSize(this.y));
			bool flag = variant == null;
			if (!flag)
			{
				base.lgMap.beginChangeMap(variant["gto"]._uint);
			}
		}

		public LGAvatarMonster getSelectTarget()
		{
			return this._selectLgAvatar as LGAvatarMonster;
		}

		private void actSkill()
		{
			uint skilSmart = this.getSkilSmart();
			bool flag = skilSmart <= 0u;
			if (!flag)
			{
				this.castSkill(skilSmart, false);
			}
		}

		public override void updateProcess(float tmSlice)
		{
		}

		public void AddEquips(Variant addEqps)
		{
		}

		public Variant get_value(string attname)
		{
			bool flag = this.data != null && this.data.ContainsKey(attname);
			Variant result;
			if (flag)
			{
				Variant variant = this.data[attname];
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static bool check_carr(int carr, Variant atts)
		{
			int num = atts["carrlvl"];
			int num2 = lgSelfPlayer.check_only_carr(carr, atts);
			bool flag = num2 != -1;
			bool result;
			if (flag)
			{
				int num3 = carr >> num2 * 4 + 1 & 7;
				bool flag2 = num >= num3;
				if (flag2)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public static int check_only_carr(int carr, Variant atts)
		{
			int num = atts["carr"];
			int num2 = 8;
			int result;
			for (int i = 0; i < num2; i++)
			{
				bool flag = (carr >> i * 4 & 1) != 0;
				if (flag)
				{
					int num3 = i + 1;
					bool flag2 = num == num3;
					if (flag2)
					{
						result = i;
						return result;
					}
				}
			}
			result = -1;
			return result;
		}

		public override void Die(Variant data)
		{
			base.Die(data);
			bool flag = MapModel.getInstance().curLevelId == 0u;
			if (flag)
			{
				InterfaceMgr.getInstance().closeAllWin("");
				InterfaceMgr.getInstance().open(InterfaceMgr.RELIVE, null, false);
			}
		}

		protected override void respawn(Variant data)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.RELIVE);
			base.respawn(data);
			base.setStand();
			base.setMeshAni("idle", 0);
		}

		public bool check(Variant data)
		{
			bool isArr = data.isArr;
			bool result;
			if (isArr)
			{
				bool flag = data.Count == 0;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			bool flag2 = this._checks == null;
			if (flag2)
			{
				this._checks = new checks();
			}
			foreach (string current in data.Keys)
			{
				bool flag3 = !this._checks.isPropertyMethod(current);
				if (!flag3)
				{
					bool isInteger = data[current].isInteger;
					if (isInteger)
					{
						bool flag4 = !this._checks.getCheckMethod(current)(this, data[current], this.g_mgr);
						if (flag4)
						{
							result = false;
							return result;
						}
					}
					else
					{
						foreach (Variant current2 in data[current]._arr)
						{
							bool flag5 = !this._checks.getCheckMethod(current)(this, current2, this.g_mgr);
							if (flag5)
							{
								result = false;
								return result;
							}
						}
					}
				}
			}
			result = true;
			return result;
		}
	}
}
