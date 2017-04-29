using System;
using UnityEngine;

namespace MuGame
{
	internal class petGO : MonsterRole
	{
		private GameObject m_SelfObj;

		private GameObject scene_Camera;

		private GameObject scene_Obj;

		private GameObject bird = null;

		public bool canshow = true;

		public void creatPetAvatar(int carr)
		{
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_SelfObj.gameObject);
				this.m_SelfObj = null;
			}
			Transform transform = SelfRole._inst.m_curModel.FindChild("birdstop");
			ProfessionAvatar professionAvatar = new ProfessionAvatar();
			string str = "";
			switch (carr)
			{
			case 2:
				str = "eagle";
				break;
			case 3:
				str = "yaque";
				break;
			case 5:
				str = "yingwu";
				break;
			}
			GameObject gameObject = Resources.Load<GameObject>("profession/" + str);
			this.m_SelfObj = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-76.38f, 0.3f, 14.934f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
			this.bird = this.m_SelfObj;
			a3_new_pet.instance.pet_avater = this.m_SelfObj;
			Transform[] componentsInChildren = this.m_SelfObj.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform2 = componentsInChildren[i];
				transform2.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
			}
			Transform transform3 = this.m_SelfObj.transform.FindChild("model");
			bool flag2 = gameObject == null;
			if (!flag2)
			{
				bool flag3 = this.bird == null;
				if (!flag3)
				{
					this.bird.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
				}
			}
		}

		public void createAvatar()
		{
			bool flag = this.m_SelfObj == null;
			if (flag)
			{
				GameObject original = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera");
				this.scene_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
				original = Resources.Load<GameObject>("profession/avatar_ui/petShow_scene");
				this.scene_Obj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.49f, 15.1f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
				Transform[] componentsInChildren = this.scene_Obj.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					bool flag2 = transform.gameObject.name == "scene_ta";
					if (flag2)
					{
						transform.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
					}
					else
					{
						transform.gameObject.layer = EnumLayer.LM_FX;
					}
				}
			}
		}

		public void disposeAvatar()
		{
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_SelfObj);
			}
			bool flag2 = this.scene_Obj != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.scene_Obj);
			}
			bool flag3 = this.scene_Camera != null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.scene_Camera);
			}
		}
	}
}
