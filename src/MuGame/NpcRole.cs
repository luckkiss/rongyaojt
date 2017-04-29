using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class NpcRole : MonoBehaviour, INameObj
	{
		public static GameObject GO_TASK_FINISH;

		public static GameObject GO_TASK_UNFINISH;

		public static GameObject GO_TASK_GET;

		public int id;

		public new string name;

		public string openid;

		public bool nav = true;

		public List<string> lDesc;

		public List<string> newDesc;

		public Action handle;

		public List<int> listTaskId;

		public Vector3 talkOffset;

		public Vector3 talkScale;

		public Transform headNub;

		public Vector3 headOffset;

		private int _curhp = 100;

		private int _maxHp = 100;

		public bool isDead = false;

		public bool canbehurt = false;

		private int _title_id = 0;

		private int _rednm = 0;

		public uint _hidbacktime = 0u;

		private bool _isactive = true;

		public int lastHeadPosTick = 0;

		public Vector3 lastHeadPos = Vector3.zero;

		private Animator anim;

		private GameObject taskIcon;

		public string roleName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public int curhp
		{
			get
			{
				return this._curhp;
			}
			set
			{
				this._curhp = value;
			}
		}

		public int maxHp
		{
			get
			{
				return this._maxHp;
			}
			set
			{
				this._maxHp = value;
			}
		}

		public int title_id
		{
			get
			{
				return this._title_id;
			}
			set
			{
				this._title_id = value;
			}
		}

		public int rednm
		{
			get
			{
				return this._rednm;
			}
			set
			{
				this._rednm = value;
			}
		}

		public uint hidbacktime
		{
			get
			{
				return this._hidbacktime;
			}
			set
			{
				this._hidbacktime = value;
			}
		}

		public bool isactive
		{
			get
			{
				return this.isactive;
			}
			set
			{
				this._isactive = value;
				bool flag = !this._isactive;
				if (flag)
				{
					this._title_id = 0;
				}
			}
		}

		public Vector3 getHeadPos()
		{
			bool flag = SceneCamera.m_curCamera == null;
			Vector3 result;
			if (flag)
			{
				result = Vector3.zero;
			}
			else
			{
				bool flag2 = SelfRole._inst.m_curPhy.position.x > base.transform.position.x;
				float num;
				if (flag2)
				{
					num = SelfRole._inst.m_curPhy.position.x - base.transform.position.x;
				}
				else
				{
					num = base.transform.position.x - SelfRole._inst.m_curPhy.position.x;
				}
				bool flag3 = SelfRole._inst.m_curPhy.position.y > base.transform.position.y;
				float num2;
				if (flag3)
				{
					num2 = SelfRole._inst.m_curPhy.position.y - base.transform.position.y;
				}
				else
				{
					num2 = base.transform.position.y - SelfRole._inst.m_curPhy.position.y;
				}
				float num3 = num + num2;
				bool flag4 = num3 > 9f;
				if (flag4)
				{
					result = Vector3.zero;
				}
				else
				{
					int tickNum = TickMgr.tickNum;
					bool flag5 = this.lastHeadPosTick == tickNum;
					if (flag5)
					{
						result = this.lastHeadPos;
					}
					else
					{
						this.lastHeadPosTick = tickNum;
						bool flag6 = this.headNub != null;
						Vector3 vector;
						if (flag6)
						{
							vector = this.headNub.position;
						}
						else
						{
							vector = base.transform.position + this.headOffset;
						}
						vector = SceneCamera.m_curCamera.WorldToScreenPoint(vector);
						vector *= SceneCamera.m_fGameScreenPow;
						vector.z = 0f;
						this.lastHeadPos = vector;
						result = vector;
					}
				}
			}
			return result;
		}

		private void Start()
		{
			BoxCollider boxCollider = base.GetComponent<BoxCollider>();
			bool flag = boxCollider == null;
			if (flag)
			{
				boxCollider = base.gameObject.AddComponent<BoxCollider>();
				boxCollider.center = new Vector3(0f, 1.2f, 0f);
				boxCollider.size = new Vector3(1f, 2f, 1f);
			}
			this.headOffset = boxCollider.center;
			this.headOffset.y = this.headOffset.y + boxCollider.size.y / 2f;
			bool flag2 = this.nav;
			if (flag2)
			{
				NavMeshAgent navMeshAgent = base.GetComponent<NavMeshAgent>();
				bool flag3 = navMeshAgent == null;
				if (flag3)
				{
					navMeshAgent = base.gameObject.AddComponent<NavMeshAgent>();
				}
				navMeshAgent.speed = 0.1f;
				navMeshAgent.avoidancePriority = 0;
				navMeshAgent.walkableMask = NavmeshUtils.allARE;
			}
			this.anim = base.GetComponent<Animator>();
			base.gameObject.layer = EnumLayer.LM_NPC;
			NpcMgr.instance.addRole(this);
			this.headNub = base.transform.Find("headnub");
			bool flag4 = this.headNub == null;
			if (flag4)
			{
				this.headNub = base.transform.Find("Bip001 HeadNub");
			}
			PlayerNameUIMgr.getInstance().show(this);
		}

		public void refreshTaskIcon(NpcTaskState state)
		{
			this.clearTaskIcon();
			switch (state)
			{
			case NpcTaskState.UNREACHED:
				NpcRole.GO_TASK_GET = Resources.Load<GameObject>("FX/comFX/fx_renwu/FX_com_tanhao");
				this.taskIcon = UnityEngine.Object.Instantiate<GameObject>(NpcRole.GO_TASK_GET);
				this.taskIcon.transform.SetParent(base.transform, false);
				break;
			case NpcTaskState.REACHED:
				NpcRole.GO_TASK_UNFINISH = Resources.Load<GameObject>("FX/comFX/fx_renwu/FX_com_wenhao_red");
				this.taskIcon = UnityEngine.Object.Instantiate<GameObject>(NpcRole.GO_TASK_UNFINISH);
				this.taskIcon.transform.SetParent(base.transform, false);
				break;
			case NpcTaskState.UNFINISHED:
				NpcRole.GO_TASK_UNFINISH = Resources.Load<GameObject>("FX/comFX/fx_renwu/FX_com_wenhao_red");
				this.taskIcon = UnityEngine.Object.Instantiate<GameObject>(NpcRole.GO_TASK_UNFINISH);
				this.taskIcon.transform.SetParent(base.transform, false);
				break;
			case NpcTaskState.FINISHED:
				NpcRole.GO_TASK_FINISH = Resources.Load<GameObject>("FX/comFX/fx_renwu/FX_com_wenhao_yellow");
				this.taskIcon = UnityEngine.Object.Instantiate<GameObject>(NpcRole.GO_TASK_FINISH);
				this.taskIcon.transform.SetParent(base.transform, false);
				break;
			}
			bool flag = this.taskIcon != null;
			if (flag)
			{
				Vector3 vector = this.headOffset;
				vector.y += 1f;
				this.taskIcon.transform.localPosition = vector;
			}
		}

		public void clearTaskIcon()
		{
			bool flag = this.taskIcon != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.taskIcon);
			}
			this.taskIcon = null;
		}

		public void dispose()
		{
			PlayerNameUIMgr.getInstance().hide(this);
		}

		public void onClick()
		{
			bool flag = !NpcMgr.instance.can_touch;
			if (!flag)
			{
				this.anim.SetTrigger("talk");
				bool flag2 = this.handle != null;
				if (flag2)
				{
					Action action = this.handle;
					dialog.showTalk(this.newDesc, action, this, false);
					this.handle = null;
				}
				else
				{
					bool flag3 = this.listTaskId != null || this.openid != "";
					if (flag3)
					{
						List<string> desc = new List<string>
						{
							"-1:你不要来点什么嘛?"
						};
						dialog.showTalk(desc, null, this, false);
					}
					else
					{
						dialog.showTalk(this.lDesc, null, this, false);
					}
					skillbar.canClick = false;
				}
			}
		}

		public void OnRefreshTitle()
		{
			PlayerNameUIMgr.getInstance().refreshTitlelv(this, this.title_id);
		}

		public void playSkill()
		{
			this.anim.SetTrigger("skill");
		}
	}
}
