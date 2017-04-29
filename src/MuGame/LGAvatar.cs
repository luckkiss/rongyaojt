using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class LGAvatar : LGAvatarBase, IProcess
	{
		public LGAvatarGameInst lockTarget;

		protected float m_ori = 0.0001f;

		protected string m_ani = "idle";

		protected bool m_loopFlag = false;

		protected bool m_visible = true;

		protected int m_stateTag = 20;

		protected Variant m_conf;

		public GRAvatar grAvatar;

		public LGGRAvatar lggrAvatar;

		public bool m_bSinging = false;

		private bool m_bsong_c = false;

		private bool _pause = false;

		private bool _destory = false;

		protected string _processName = "";

		public virtual Variant viewInfo
		{
			get;
			set;
		}

		public virtual Variant data
		{
			get;
			set;
		}

		public virtual Variant conf
		{
			get
			{
				return null;
			}
		}

		public override float lg_ori_angle
		{
			get
			{
				return this.m_ori;
			}
			set
			{
				bool flag = this.m_ori == value;
				if (!flag)
				{
					this.m_ori = value;
					bool flag2 = this.grAvatar != null;
					if (flag2)
					{
						(this as LGAvatarGameInst).grAvatar.setOri(this.m_ori);
					}
				}
			}
		}

		public float speed
		{
			get
			{
				return this.viewInfo["speed"]._float;
			}
		}

		public int stateTag
		{
			get
			{
				return this.m_stateTag;
			}
		}

		protected ClientSkillConf skillConf
		{
			get
			{
				return (this.g_mgr.g_gameConfM as muCLientConfig).getObject("skill") as ClientSkillConf;
			}
		}

		protected ClientGeneralConf genConf
		{
			get
			{
				return (this.g_mgr.g_gameConfM as muCLientConfig).getObject("general") as ClientGeneralConf;
			}
		}

		protected SvrSkillConfig svrSkillConf
		{
			get
			{
				return (this.g_mgr.g_gameConfM as muCLientConfig).getObject("SvrSkill") as SvrSkillConfig;
			}
		}

		protected LGMap lgMap
		{
			get
			{
				return this.g_mgr.getObject("LG_MAP") as LGMap;
			}
		}

		protected lgSelfPlayer selfPlayer
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_selfPlayer;
			}
		}

		protected joinWorldInfo selfInfo
		{
			get
			{
				return this.g_mgr.g_netM.getObject("DATA_JOIN_WORLD") as joinWorldInfo;
			}
		}

		protected LGOthers lgOthers
		{
			get
			{
				return this.g_mgr.g_gameM.getObject("LG_OTHER_PLAYERS") as LGOthers;
			}
		}

		protected LGMonsters lgMonsters
		{
			get
			{
				return this.g_mgr.g_gameM.getObject("LG_MONSTERS") as LGMonsters;
			}
		}

		protected lgSelfPlayer lgMainPlayer
		{
			get
			{
				return this.g_mgr.g_gameM.getObject("LG_MAIN_PLAY") as lgSelfPlayer;
			}
		}

		protected MgrPlayerInfo playerInfos
		{
			get
			{
				return this.g_mgr.getObject("MgrPlayerInfo") as MgrPlayerInfo;
			}
		}

		public bool destroy
		{
			get
			{
				return this._destory;
			}
			set
			{
				this._destory = value;
			}
		}

		public virtual string processName
		{
			get
			{
				return "LGAvatar";
			}
			set
			{
				this._processName = value;
			}
		}

		public bool pause
		{
			get
			{
				return this._pause;
			}
			set
			{
				this._pause = value;
			}
		}

		public LGAvatar(gameManager m) : base(m)
		{
		}

		public override void init()
		{
			this.onInit();
		}

		public virtual void onInit()
		{
		}

		protected void setMoveInfo(Variant moveinfo)
		{
			this.data["moving"] = moveinfo;
		}

		protected void clearMoveInfo()
		{
			bool flag = !this.data.ContainsKey("moving");
			if (!flag)
			{
				this.data.RemoveKey("moving");
			}
		}

		public override void initGr(GRBaseImpls grBase, LGGRBaseImpls sctrl)
		{
			bool flag = grBase is GRAvatar;
			if (flag)
			{
				this.grAvatar = (grBase as GRAvatar);
			}
			bool flag2 = sctrl is LGGRAvatar;
			if (flag2)
			{
				this.lggrAvatar = (sctrl as LGGRAvatar);
			}
		}

		public bool isMainPlayer()
		{
			return this.roleType == LGAvatarBase.ROLE_TYPE_USER;
		}

		public bool IsCollect()
		{
			bool flag = this.m_conf != null && this.m_conf.ContainsKey("collect_tar");
			return flag && this.m_conf["collect_tar"]._int > 0;
		}

		public void setConfig(Variant conf)
		{
			this.m_conf = conf;
		}

		public string getAni()
		{
			return this.m_ani;
		}

		public bool IsRunningAni()
		{
			bool flag = this.grAvatar != null;
			bool result;
			if (flag)
			{
				GRCharacter3D @char = (this as LGAvatarGameInst).grAvatar.m_char;
				bool flag2 = @char != null;
				if (flag2)
				{
					result = @char.IsRunningAnim();
					return result;
				}
			}
			result = false;
			return result;
		}

		public void refreshIdle()
		{
			this.setMeshAni("run", 0);
			this.setMeshAni("idle", 0);
		}

		protected void setMeshAni(string ani, int loop)
		{
			bool flag = this is LGAvatarGameInst && (this as LGAvatarGameInst).grAvatar != null;
			if (flag)
			{
				this.m_ani = ani;
				this.refreshAniLoopflag();
				bool loopFlag = this.m_loopFlag;
				if (loopFlag)
				{
					loop = 0;
				}
				(this as LGAvatarGameInst).grAvatar.setAni(ani, loop);
			}
		}

		public string getMoveAni()
		{
			return "a5";
		}

		public bool visibleFlag()
		{
			return this.m_visible;
		}

		public virtual void setPos(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public bool IsDie()
		{
			return this.m_stateTag == 50;
		}

		public bool IsMoving()
		{
			return this.m_stateTag == 10;
		}

		public void setAttack()
		{
			this.setTag(40);
		}

		public void setStand()
		{
			this.setTag(20);
		}

		public void setDie()
		{
			this.setTag(50);
			this.playDeadSound();
		}

		protected virtual void playDeadSound()
		{
		}

		protected void setTag(int tag)
		{
			bool flag = this.m_stateTag == 50 && !(this is lgSelfPlayer);
			if (!flag)
			{
				this.m_stateTag = tag;
				this.refreshAni();
			}
		}

		public void UpRide(bool b)
		{
		}

		public virtual bool attack(LGAvatarGameInst lg, bool smartSkill = false, uint skillid = 0u)
		{
			return false;
		}

		public Variant GetStates()
		{
			return null;
		}

		public void stop()
		{
			this.clearMoveInfo();
			bool flag = !this.IsDie();
			if (flag)
			{
				this.setStand();
			}
			this.onStop();
		}

		protected virtual void onStop()
		{
		}

		public void addShowEff(string effid)
		{
			Variant effectConf = this.g_mgr.g_sceneM.getEffectConf(effid);
			base.dispatchEvent(GameEvent.Create(2210u, this, effectConf, false));
		}

		protected void rmvCharEquip(Variant eqp, Variant eqpConf)
		{
		}

		protected void refreshAni()
		{
			int stateTag = this.m_stateTag;
			if (stateTag <= 20)
			{
				if (stateTag == 10)
				{
					this.setMeshAni("run", 0);
					return;
				}
				if (stateTag == 20)
				{
					this.setMeshAni("idle", 0);
					return;
				}
			}
			else
			{
				if (stateTag == 40)
				{
					this.setMeshAni("attack", 1);
					return;
				}
				if (stateTag == 50)
				{
					this.setMeshAni("dead", 1);
					return;
				}
			}
			this.setMeshAni("idle", 0);
		}

		private void refreshAniLoopflag()
		{
			this.m_loopFlag = false;
			bool flag = this.m_ani == "idle" || this.m_ani == "run";
			if (flag)
			{
				this.m_loopFlag = true;
			}
		}

		public void play_animation(string ani, int flag = 0)
		{
			this.setMeshAni(ani, 0);
		}

		public void playAniOnMoveReach()
		{
			this.setTag(20);
		}

		public void dispose()
		{
			this.onDispose();
			this.destroy = true;
			base.dispatchEvent(GameEvent.Create(2205u, this, null, false));
		}

		protected virtual void onDispose()
		{
		}

		public List<GridST> findPath(float fx, float fy, float tx, float ty)
		{
			List<GridST> list = this.lgMap.findPath(this.lgMap.getGPosByPPos(fx, fy), this.lgMap.getGPosByPPos(tx, ty));
			bool flag = list == null;
			List<GridST> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = list;
			}
			return result;
		}

		public virtual uint getIid()
		{
			return 0u;
		}

		public virtual uint getCid()
		{
			return 0u;
		}

		public virtual uint getMid()
		{
			return 0u;
		}

		public virtual uint getNid()
		{
			return 0u;
		}

		public float RotationToPt(float tx, float ty)
		{
			float num = (float)(Math.Atan2((double)(ty - this.grAvatar.pTrans.position.z), (double)(tx - this.grAvatar.pTrans.position.x)) * 57.295779513082323);
			this.lg_ori_angle = num;
			return num;
		}

		public void faceToAvatar(LGAvatar avatar)
		{
			this.RotationToPt(avatar.grAvatar.pTrans.position.x, avatar.grAvatar.pTrans.position.z);
		}

		public void OnSong(Variant vd)
		{
			this.m_bSinging = true;
			this.setMeshAni("song", 255);
			bool flag = this.grAvatar != null && vd.ContainsKey("skid");
			if (flag)
			{
				this.grAvatar.playSong(vd["skid"]._uint);
			}
		}

		public void OnAttack(Variant data, LGAvatar toChar)
		{
			bool flag = this.IsDie();
			if (!flag)
			{
				bool flag2 = this.isMainPlayer();
				if (!flag2)
				{
					bool flag3 = toChar == null;
					if (!flag3)
					{
						this.RotationToPt(toChar.x, toChar.y);
						this.playAttackAni(false);
					}
				}
			}
		}

		protected virtual void playAttackAni(bool criatk = false)
		{
			this.setAttack();
		}

		public LGAvatarGameInst get_Character_by_iid(uint iid)
		{
			bool flag = iid == this.selfInfo.mainPlayerInfo["iid"]._uint;
			LGAvatarGameInst result;
			if (flag)
			{
				result = this.lgMainPlayer;
			}
			else
			{
				LGAvatarGameInst lGAvatarGameInst = this.lgMonsters.get_mon_by_iid(iid);
				bool flag2 = lGAvatarGameInst == null;
				if (flag2)
				{
					lGAvatarGameInst = this.lgOthers.get_player_by_iid(iid);
				}
				result = lGAvatarGameInst;
			}
			return result;
		}

		public bool IsInRange(LGAvatar lga, float rangePixel, bool iscell = true)
		{
			bool result;
			if (iscell)
			{
				result = this.IsInRange(lga.x, lga.y, rangePixel, iscell);
			}
			else
			{
				float num = Vector3.Distance(lga.grAvatar.pTrans.position, this.grAvatar.pTrans.position);
				result = (num <= rangePixel);
			}
			return result;
		}

		public bool IsInRange(float _x, float _y, float range, bool iscell = true)
		{
			float num;
			float num2;
			if (iscell)
			{
				num = (float)((int)(_x - this.x)) / 1.666f;
				num2 = (float)((int)(_y - this.y)) / 1.666f;
			}
			else
			{
				num = (float)((int)_x - (int)this.x);
				num2 = (float)((int)_y - (int)this.y);
			}
			return num * num + num2 * num2 < range * range;
		}

		protected Variant getTitleConf(string tp, int showtp = 0, Variant showInfo = null)
		{
			return GameTools.createGroup(new Variant[]
			{
				"tp",
				tp,
				"showtp",
				showtp,
				"showInfo",
				showInfo
			});
		}

		public virtual void updateProcess(float tmSlice)
		{
		}
	}
}
