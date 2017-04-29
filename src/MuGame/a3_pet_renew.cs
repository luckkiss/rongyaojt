using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	internal class a3_pet_renew : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_pet_renew.<>c <>9 = new a3_pet_renew.<>c();

			public static Action<GameObject> <>9__4_0;

			public static Action<GameObject> <>9__4_1;

			internal void <init>b__4_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_PET_RENEW);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_NEW_PET, null, false);
				A3_PetModel.showbuy = true;
			}

			internal void <init>b__4_1(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_PET_RENEW);
			}
		}

		public static a3_pet_renew instance;

		private GameObject m_SelfObj;

		private GameObject scene_Camera;

		private GameObject bird = null;

		public override void init()
		{
			a3_pet_renew.instance = this;
			BaseButton arg_38_0 = new BaseButton(base.getTransformByPath("gorenew"), 1, 1);
			Action<GameObject> arg_38_1;
			if ((arg_38_1 = a3_pet_renew.<>c.<>9__4_0) == null)
			{
				arg_38_1 = (a3_pet_renew.<>c.<>9__4_0 = new Action<GameObject>(a3_pet_renew.<>c.<>9.<init>b__4_0));
			}
			arg_38_0.onClick = arg_38_1;
			BaseButton arg_6F_0 = new BaseButton(base.getTransformByPath("bg/dark"), 1, 1);
			Action<GameObject> arg_6F_1;
			if ((arg_6F_1 = a3_pet_renew.<>c.<>9__4_1) == null)
			{
				arg_6F_1 = (a3_pet_renew.<>c.<>9__4_1 = new Action<GameObject>(a3_pet_renew.<>c.<>9.<init>b__4_1));
			}
			arg_6F_0.onClick = arg_6F_1;
			base.transform.SetAsLastSibling();
		}

		public override void onShowed()
		{
			this.creatAvatar();
		}

		public override void onClosed()
		{
			this.disposeAvatar();
		}

		private void creatAvatar()
		{
			string str = "";
			switch (A3_PetModel.curPetid)
			{
			case 2u:
				str = "eagle";
				break;
			case 3u:
				str = "yaque";
				break;
			case 5u:
				str = "yingwu";
				break;
			}
			GameObject gameObject = Resources.Load<GameObject>("profession/" + str);
			this.m_SelfObj = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-153.92f, 0.89f, 0f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
			this.bird = this.m_SelfObj;
			this.bird.GetComponent<Animator>().applyRootMotion = false;
			Transform[] componentsInChildren = this.m_SelfObj.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = EnumLayer.LM_FX;
			}
			bool flag = gameObject == null;
			if (!flag)
			{
				this.bird.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
				bool flag2 = this.m_SelfObj != null;
				if (flag2)
				{
					GameObject original = Resources.Load<GameObject>("profession/avatar_ui/roleinfo_ui_camera");
					this.scene_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
				}
				UnityEngine.Object.DontDestroyOnLoad(this.m_SelfObj);
				UnityEngine.Object.DontDestroyOnLoad(this.scene_Camera);
			}
		}

		public void disposeAvatar()
		{
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_SelfObj);
			}
			bool flag2 = this.scene_Camera != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.scene_Camera);
			}
		}
	}
}
