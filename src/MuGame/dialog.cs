using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class dialog : Window
	{
		public static List<Vector3> lAvPos = new List<Vector3>
		{
			new Vector3(-127.7f, -1.7f, -128f),
			new Vector3(-131.4f, -4.25f, -128f)
		};

		private ProfessionAvatar m_proAvatar;

		private static Action m_handle;

		private static List<string> m_desc;

		public static NpcRole m_npc;

		public static GameObject go_npc;

		private int curIdx = 0;

		public static NpcRole fake_npc;

		public static string fake_desc;

		public static dialog instance;

		public GameObject bg;

		private Animator m_anim_npc;

		private Animator m_anim_usr;

		private GameObject m_npc_camera;

		private GameObject m_player_camera;

		private GameObject m_selfObj;

		public static string curType;

		public static string curDesc;

		public static bool isLastDesc = false;

		public static bool continuedo = false;

		public override bool showBG
		{
			get
			{
				return false;
			}
		}

		public static void showTalk(List<string> desc, Action handle, NpcRole npc, bool isfake = false)
		{
			bool flag = desc == null;
			if (!flag)
			{
				if (isfake)
				{
					dialog.fake_npc = npc;
					dialog.fake_desc = desc[0];
				}
				else
				{
					dialog.m_handle = handle;
					dialog.m_desc = desc;
					dialog.m_npc = npc;
				}
				InterfaceMgr.getInstance().open(InterfaceMgr.DIALOG, null, false);
			}
		}

		public override void init()
		{
			base.alain();
			this.bg = base.getGameObjectByPath("bg");
			this.bg.GetComponent<RectTransform>().sizeDelta = new Vector2(Baselayer.uiWidth * 1.5f, Baselayer.uiHeight * 1.5f);
			base.init();
		}

		public void InitPlayerCam(bool active = false)
		{
			bool flag = this.m_player_camera == null;
			if (flag)
			{
				this.m_player_camera = UnityEngine.Object.Instantiate<GameObject>(U3DAPI.U3DResLoad<GameObject>("camera/player_camera"));
			}
			this.m_player_camera.SetActive(active);
		}

		public void InitNPCCam(bool active = true)
		{
			bool flag = this.m_npc_camera == null;
			if (flag)
			{
				this.m_npc_camera = UnityEngine.Object.Instantiate<GameObject>(U3DAPI.U3DResLoad<GameObject>("camera/npc_camera"));
			}
			this.m_npc_camera.SetActive(active);
		}

		public void initAvatar()
		{
			bool flag = this.m_anim_usr == null;
			if (flag)
			{
				this.m_selfObj = null;
				bool flag2 = SelfRole._inst is P2Warrior;
				if (flag2)
				{
					GameObject original = Resources.Load<GameObject>("profession/avatar_ui/warrior_avatar");
					this.m_selfObj = (UnityEngine.Object.Instantiate(original, new Vector3(500f, 0f, 1f), new Quaternion(0f, 0f, 0f, 0f)) as GameObject);
					this.m_selfObj.transform.FindChild("model").localRotation = Quaternion.Euler(new Vector3(0f, -20f, 0f));
				}
				else
				{
					bool flag3 = SelfRole._inst is P3Mage;
					if (flag3)
					{
						GameObject original = Resources.Load<GameObject>("profession/avatar_ui/mage_avatar");
						this.m_selfObj = (UnityEngine.Object.Instantiate(original, new Vector3(500f, 0f, 1f), new Quaternion(0f, 0f, 0f, 0f)) as GameObject);
						this.m_selfObj.transform.FindChild("model").localRotation = Quaternion.Euler(new Vector3(0f, -20f, 0f));
					}
					else
					{
						bool flag4 = SelfRole._inst is P5Assassin;
						if (flag4)
						{
							GameObject original = Resources.Load<GameObject>("profession/avatar_ui/assa_avatar");
							this.m_selfObj = (UnityEngine.Object.Instantiate(original, new Vector3(500f, 0f, 1f), new Quaternion(0f, 0f, 0f, 0f)) as GameObject);
							this.m_selfObj.transform.FindChild("model").localRotation = Quaternion.Euler(new Vector3(0f, -20f, 0f));
						}
					}
				}
				Transform transform = this.m_selfObj.transform.FindChild("model");
				transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
				ProfessionAvatar professionAvatar = new ProfessionAvatar();
				professionAvatar.Init(SelfRole._inst.m_strAvatarPath, "h_", EnumLayer.LM_FX, EnumMaterial.EMT_EQUIP_H, transform, "");
				this.m_proAvatar = professionAvatar;
				bool flag5 = ModelBase<a3_EquipModel>.getInstance().active_eqp.Count >= 10;
				if (flag5)
				{
					this.m_proAvatar.set_equip_eff(ModelBase<a3_EquipModel>.getInstance().GetEqpIdbyType(3), true);
				}
				professionAvatar.set_body(SelfRole._inst.get_bodyid(), SelfRole._inst.get_bodyfxid());
				professionAvatar.set_weaponl(SelfRole._inst.get_weaponl_id(), SelfRole._inst.get_weaponl_fxid());
				professionAvatar.set_weaponr(SelfRole._inst.get_weaponr_id(), SelfRole._inst.get_weaponr_fxid());
				professionAvatar.set_wing(SelfRole._inst.get_wingid(), SelfRole._inst.get_windfxid());
				professionAvatar.set_equip_color(SelfRole._inst.get_equip_colorid());
				this.m_anim_usr = transform.GetComponent<Animator>();
				Transform[] componentsInChildren = this.m_selfObj.GetComponentsInChildren<Transform>();
				Transform[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					Transform transform2 = array[i];
					transform2.gameObject.layer = EnumLayer.LM_DEFAULT;
				}
				bool flag6 = SelfRole._inst is P3Mage;
				if (flag6)
				{
					Vector3 center = professionAvatar.m_BodySkin.localBounds.center;
					center.y = 1.3f;
					Bounds localBounds = professionAvatar.m_BodySkin.localBounds;
					Vector3 size = localBounds.size;
					SkinnedMeshRenderer arg_3BE_0 = professionAvatar.m_ShoulderSkin;
					SkinnedMeshRenderer arg_3B6_0 = professionAvatar.m_BodySkin;
					localBounds = new Bounds(center, size);
					arg_3B6_0.localBounds = localBounds;
					arg_3BE_0.localBounds = localBounds;
				}
			}
			NpcRole npc = dialog.m_npc;
			bool flag7 = dialog.fake_npc != null;
			if (flag7)
			{
				npc = dialog.fake_npc;
			}
			bool flag8 = this.m_anim_npc == null && npc != null;
			if (flag8)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(npc.gameObject);
				this.m_anim_npc = gameObject.GetComponent<Animator>();
				UnityEngine.Object.Destroy(gameObject.GetComponent<NavMeshAgent>());
				gameObject.transform.localPosition = npc.talkOffset;
				gameObject.transform.localScale = npc.talkScale;
				gameObject.transform.eulerAngles = Vector3.zero;
				debug.Log(string.Concat(new object[]
				{
					":::::::::::::::::::::::::::::::::::::::::::::",
					gameObject.transform.position,
					" ",
					gameObject.gameObject.name
				}));
				Transform[] componentsInChildren2 = gameObject.GetComponentsInChildren<Transform>();
				Transform[] array2 = componentsInChildren2;
				for (int j = 0; j < array2.Length; j++)
				{
					Transform transform3 = array2[j];
					transform3.gameObject.layer = EnumLayer.LM_DEFAULT;
				}
				NpcRole component = gameObject.GetComponent<NpcRole>();
				bool flag9 = component != null;
				if (flag9)
				{
					UnityEngine.Object.Destroy(component);
				}
			}
		}

		private void Update()
		{
			bool flag = this.m_proAvatar != null;
			if (flag)
			{
				this.m_proAvatar.FrameMove();
			}
		}

		public void showDesc(string desc)
		{
			int num = desc.IndexOf(":");
			bool flag = num < 0;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.DIALOG);
			}
			else
			{
				dialog.curType = desc.Substring(0, num);
				dialog.curDesc = desc.Substring(num + 1, desc.Length - num - 1);
				bool flag2 = dialog.curType == "0" || dialog.curType == "1";
				if (flag2)
				{
					this.OnShowAvatar(true);
					InterfaceMgr.getInstance().open(InterfaceMgr.NPC_TALK, null, false);
				}
				else
				{
					bool flag3 = dialog.curType == "2" || dialog.curType == "3";
					if (flag3)
					{
						this.OnShowAvatar(false);
						InterfaceMgr.getInstance().open(InterfaceMgr.NPC_TASK_TALK, null, false);
					}
					else
					{
						bool flag4 = dialog.curType == "-1";
						if (flag4)
						{
							this.OnShowAvatar(false);
							InterfaceMgr.getInstance().open(InterfaceMgr.NPC_TASK_TALK, null, false);
						}
						else
						{
							bool flag5 = dialog.curType == "newbie";
							if (flag5)
							{
								dialog.continuedo = !dialog.isLastDesc;
								InterfaceMgr.getInstance().close(InterfaceMgr.DIALOG);
								NewbieTeachMgr.getInstance().add(dialog.curDesc, -1);
							}
						}
					}
				}
			}
		}

		private void OnShowAvatar(bool show)
		{
			if (show)
			{
				bool flag = this.m_npc_camera == null;
				if (flag)
				{
					this.m_npc_camera = UnityEngine.Object.Instantiate<GameObject>(U3DAPI.U3DResLoad<GameObject>("camera/npc_camera"));
				}
			}
			this.m_anim_usr.gameObject.SetActive(show);
			bool flag2 = this.m_anim_npc != null;
			if (flag2)
			{
				GameObject expr_5A = this.m_anim_npc.gameObject;
				if (expr_5A != null)
				{
					expr_5A.SetActive(show);
				}
			}
		}

		public void closeSubWins()
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.NPC_TALK);
			InterfaceMgr.getInstance().close(InterfaceMgr.NPC_TASK_TALK);
		}

		public void showRole(bool usr)
		{
			if (usr)
			{
				this.m_anim_usr.transform.position = dialog.lAvPos[0];
				bool flag = this.m_anim_npc != null;
				if (flag)
				{
					this.m_anim_npc.transform.position = new Vector3(65535f, 1f, 1f);
					this.m_anim_npc.transform.localRotation = Quaternion.Euler(new Vector3(0f, -20f, 0f));
				}
			}
			else
			{
				bool flag2 = this.m_anim_usr != null;
				if (flag2)
				{
					this.m_anim_usr.transform.position = new Vector3(500f, 1f, 1f);
				}
				bool flag3 = this.m_anim_npc != null;
				if (flag3)
				{
					this.m_anim_npc.transform.localRotation = Quaternion.Euler(new Vector3(0f, 20f, 0f));
				}
				bool flag4 = this.m_anim_npc != null;
				if (flag4)
				{
					bool flag5 = dialog.fake_npc != null;
					if (flag5)
					{
						this.m_anim_npc.transform.position = dialog.fake_npc.talkOffset;
					}
					else
					{
						this.m_anim_npc.transform.position = dialog.m_npc.talkOffset;
					}
					this.m_anim_npc.SetTrigger("talk");
				}
			}
		}

		public static void next()
		{
			bool flag = dialog.instance != null;
			if (flag)
			{
				dialog.instance.doNext();
			}
		}

		public void doNext()
		{
			bool flag = dialog.fake_npc != null;
			if (flag)
			{
				dialog.fake_npc = null;
				InterfaceMgr.getInstance().close(InterfaceMgr.DIALOG);
			}
			else
			{
				this.closeSubWins();
				bool flag2 = this.curIdx + 1 >= dialog.m_desc.Count;
				if (flag2)
				{
					dialog.isLastDesc = true;
				}
				else
				{
					dialog.isLastDesc = false;
				}
				bool flag3 = this.curIdx >= dialog.m_desc.Count;
				if (flag3)
				{
					bool flag4 = dialog.m_handle != null;
					if (flag4)
					{
						dialog.m_handle();
					}
					InterfaceMgr.getInstance().close(InterfaceMgr.DIALOG);
				}
				else
				{
					this.showDesc(dialog.m_desc[this.curIdx]);
					this.curIdx++;
				}
			}
		}

		public override void onShowed()
		{
			bool flag = joystick.instance != null;
			if (flag)
			{
				joystick.instance.OnDragOut(null);
			}
			dialog.instance = this;
			bool flag2 = dialog.fake_npc != null;
			if (flag2)
			{
				InterfaceMgr.setUntouchable(this.bg);
				this.initAvatar();
				dialog.curDesc = dialog.fake_desc;
				this.OnShowAvatar(true);
				InterfaceMgr.getInstance().open(InterfaceMgr.NPC_TALK, null, false);
			}
			else
			{
				bool flag3 = !dialog.continuedo;
				if (flag3)
				{
					this.curIdx = 0;
				}
				dialog.continuedo = false;
				InterfaceMgr.setUntouchable(this.bg);
				this.initAvatar();
				this.doNext();
			}
			base.onShowed();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_STORY);
			bool flag4 = a3_chapter_hint.instance != null && npctalk.instance != null;
			if (flag4)
			{
				npctalk.instance.MinOrMax(false);
			}
		}

		public override void onClosed()
		{
			this.closeSubWins();
			dialog.m_handle = null;
			dialog.instance = null;
			bool flag = this.m_player_camera != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_player_camera);
				this.m_player_camera = null;
			}
			bool flag2 = this.m_npc_camera != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.m_npc_camera);
				this.m_npc_camera = null;
			}
			bool flag3 = this.m_anim_usr != null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.m_selfObj);
				this.m_selfObj = null;
				this.m_anim_usr = null;
			}
			bool flag4 = this.m_anim_npc != null;
			if (flag4)
			{
				UnityEngine.Object.Destroy(this.m_anim_npc.gameObject);
				this.m_anim_npc = null;
			}
			EventTriggerListener.Get(base.gameObject).clearAllListener();
			base.onClosed();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
		}

		public void GetNPCCamRdy()
		{
			bool flag = this.m_player_camera != null;
			if (flag)
			{
				this.m_player_camera.SetActive(false);
			}
			else
			{
				dialog.instance.InitPlayerCam(false);
			}
			bool flag2 = this.m_npc_camera != null;
			if (flag2)
			{
				this.m_npc_camera.SetActive(true);
			}
			else
			{
				dialog.instance.InitNPCCam(true);
			}
		}

		public void GetPlayerCamRdy()
		{
			bool flag = this.m_player_camera != null;
			if (flag)
			{
				this.m_player_camera.SetActive(true);
			}
			else
			{
				dialog.instance.InitPlayerCam(false);
			}
			bool flag2 = this.m_npc_camera != null;
			if (flag2)
			{
				this.m_npc_camera.SetActive(false);
			}
			else
			{
				dialog.instance.InitNPCCam(false);
			}
		}
	}
}
