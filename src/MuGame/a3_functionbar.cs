using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_functionbar : Window
	{
		private GameObject m_SelfObj;

		private GameObject m_Self_Camera;

		private ProfessionAvatar m_proAvatar;

		private int m_testAvatar = 0;

		public override void init()
		{
			this.OpenAni = false;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onclose);
			base.getEventTrigerByPath("TouchDrag").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			base.transform.FindChild("Panel_right/dress").GetComponent<Button>().onClick.AddListener(delegate
			{
				this.m_testAvatar++;
			});
		}

		private void Update()
		{
			bool flag = this.m_proAvatar != null;
			if (flag)
			{
				this.m_proAvatar.FrameMove();
			}
		}

		public void createAvatar()
		{
			bool flag = this.m_SelfObj == null;
			if (flag)
			{
				bool flag2 = SelfRole._inst is P2Warrior;
				GameObject original;
				if (flag2)
				{
					original = Resources.Load<GameObject>("profession/avatar_ui/warrior_avatar");
				}
				else
				{
					bool flag3 = SelfRole._inst is P3Mage;
					if (flag3)
					{
						original = Resources.Load<GameObject>("profession/avatar_ui/mage_avatar");
					}
					else
					{
						bool flag4 = SelfRole._inst is P5Assassin;
						if (!flag4)
						{
							return;
						}
						original = Resources.Load<GameObject>("profession/avatar_ui/assa_avatar");
					}
				}
				this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-128f, 0f, 0f), Quaternion.identity) as GameObject);
				Transform[] componentsInChildren = this.m_SelfObj.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					transform.gameObject.layer = EnumLayer.LM_FX;
				}
				Transform transform2 = this.m_SelfObj.transform.FindChild("model");
				bool flag5 = SelfRole._inst is P3Mage;
				if (flag5)
				{
					Transform parent = transform2.FindChild("R_Finger1");
					original = Resources.Load<GameObject>("profession/avatar_ui/mage_r_finger_fire");
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					gameObject.transform.SetParent(parent, false);
				}
				this.m_proAvatar = new ProfessionAvatar();
				this.m_proAvatar.Init(SelfRole._inst.m_strAvatarPath, "h_", EnumLayer.LM_FX, EnumMaterial.EMT_EQUIP_H, transform2, SelfRole._inst.m_strEquipEffPath);
				this.m_proAvatar.set_body(SelfRole._inst.get_bodyid(), SelfRole._inst.get_bodyfxid());
				this.m_proAvatar.set_weaponl(SelfRole._inst.get_weaponl_id(), SelfRole._inst.get_weaponl_fxid());
				this.m_proAvatar.set_weaponr(SelfRole._inst.get_weaponr_id(), SelfRole._inst.get_weaponr_fxid());
				this.m_proAvatar.set_wing(SelfRole._inst.get_wingid(), SelfRole._inst.get_windfxid());
				original = Resources.Load<GameObject>("profession/avatar_ui/avatar_ui_camera");
				this.m_Self_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
				transform2.Rotate(Vector3.up, 90f);
			}
		}

		public void disposeAvatar()
		{
			this.m_proAvatar = null;
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_SelfObj);
			}
			bool flag2 = this.m_Self_Camera != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.m_Self_Camera);
			}
		}

		public override void onShowed()
		{
			this.createAvatar();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			base.transform.FindChild("ig_bg_bg").gameObject.SetActive(false);
			U3DAPI.functionbar_ChangeTo();
			SceneCamera.m_curGameObj.SetActive(false);
			a3_liteMinimap.instance.miniMapActive = false;
		}

		public override void onClosed()
		{
			this.disposeAvatar();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			U3DAPI.functionbar_BackFrom();
			SceneCamera.m_curGameObj.SetActive(true);
			a3_liteMinimap.instance.miniMapActive = true;
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				this.m_SelfObj.transform.Rotate(Vector3.up, -delta.x);
			}
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_FUNCTIONBAR);
		}
	}
}
