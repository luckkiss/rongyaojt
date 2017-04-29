using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_Winginfo : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_Winginfo.<>c <>9 = new a3_Winginfo.<>c();

			public static Action<GameObject> <>9__4_0;

			internal void <init>b__4_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_WINGINFO);
				bool flag = a3_ranking.isshow && a3_ranking.isshow.Toback;
				if (flag)
				{
					a3_ranking.isshow.Toback.SetActive(true);
					a3_ranking.isshow.Toback = null;
				}
			}
		}

		private GameObject m_SelfObj;

		private GameObject scene_Camera;

		private GameObject scene_Obj;

		private Transform stars;

		private int carr = 0;

		private int stage = 0;

		private int lvl = 0;

		public override void init()
		{
			this.stars = base.transform.FindChild("playerInfo/panel_attr/stars");
			BaseButton arg_48_0 = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			Action<GameObject> arg_48_1;
			if ((arg_48_1 = a3_Winginfo.<>c.<>9__4_0) == null)
			{
				arg_48_1 = (a3_Winginfo.<>c.<>9__4_0 = new Action<GameObject>(a3_Winginfo.<>c.<>9.<init>b__4_0));
			}
			arg_48_0.onClick = arg_48_1;
			base.getEventTrigerByPath("avatar_touch").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
		}

		public override void onShowed()
		{
			bool flag = this.uiData != null && this.uiData.Count > 0;
			if (flag)
			{
				this.carr = (int)this.uiData[0];
				this.stage = (int)this.uiData[1];
				this.lvl = (int)this.uiData[2];
			}
			else
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_WINGINFO);
				bool flag2 = a3_ranking.isshow && a3_ranking.isshow.Toback;
				if (flag2)
				{
					a3_ranking.isshow.Toback.SetActive(true);
					a3_ranking.isshow.Toback = null;
				}
				flytxt.instance.fly("查询失败", 0, default(Color), null);
			}
			this.createAvatar(this.carr, this.stage);
			this.RefreshAtt(this.carr, this.stage, this.lvl);
			this.setStars(this.lvl);
			base.transform.SetAsLastSibling();
			GRMap.GAME_CAMERA.SetActive(false);
			base.transform.FindChild("ig_bg_bg").gameObject.SetActive(false);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
			this.disposeAvatar();
		}

		private void setStars(int lvl)
		{
			for (int i = 0; i < 10; i++)
			{
				this.stars.GetChild(i).FindChild("b").gameObject.SetActive(false);
			}
			for (int j = 0; j < lvl; j++)
			{
				this.stars.GetChild(j).FindChild("b").gameObject.SetActive(true);
			}
		}

		private void RefreshAtt(int carr, int stage, int lvl)
		{
			GameObject gameObject = base.transform.FindChild("playerInfo/panel_attr/att/att_temp").gameObject;
			SXML sXML = XMLMgr.instance.GetSXML("wings.wing", "career==" + carr);
			SXML node = sXML.GetNode("wing_stage", "stage_id==" + stage);
			SXML node2 = node.GetNode("wing_level", "level_id==" + lvl);
			List<SXML> nodeList = node2.GetNodeList("att", null);
			base.transform.FindChild("playerInfo/panel_attr/name").GetComponent<Text>().text = node.getString("name");
			base.transform.FindChild("playerInfo/panel_attr/lv").GetComponent<Text>().text = string.Concat(new object[]
			{
				"LV",
				lvl,
				"(",
				stage,
				"阶)"
			});
			Transform transform = base.transform.FindChild("playerInfo/panel_attr/att/grid");
			for (int i = 0; i < transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
			}
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			string[] array = new string[2];
			for (int j = 0; j < nodeList.Count; j++)
			{
				int @int = nodeList[j].getInt("att_type");
				float @float = nodeList[j].getFloat("att_value");
				bool flag = @int == 5;
				if (flag)
				{
					array[1] = @float.ToString();
					dictionary.Add(@int, "");
				}
				else
				{
					bool flag2 = @int == 38;
					if (flag2)
					{
						array[0] = @float.ToString();
					}
					else
					{
						dictionary.Add(@int, @float.ToString());
					}
				}
			}
			bool flag3 = dictionary.ContainsKey(5);
			if (flag3)
			{
				dictionary[5] = array[0] + "-" + array[1];
			}
			int childCount = transform.childCount;
			foreach (int current in dictionary.Keys)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.transform.SetParent(transform, false);
				gameObject2.gameObject.SetActive(true);
				Text component = gameObject2.transform.FindChild("text_name").GetComponent<Text>();
				Text component2 = gameObject2.transform.FindChild("text_value").GetComponent<Text>();
				component.text = Globle.getAttrNameById(current);
				component2.text = dictionary[current];
			}
		}

		public void createAvatar(int carr, int stage)
		{
			GameObject gameObject = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera");
			this.scene_Camera = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject = Resources.Load<GameObject>("profession/avatar_ui/show_scene");
			this.scene_Obj = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-77.38f, -0.49f, 15.1f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
			Transform[] componentsInChildren = this.scene_Obj.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.gameObject.name == "scene_ta";
				if (flag)
				{
					transform.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
				}
				else
				{
					transform.gameObject.layer = EnumLayer.LM_FX;
				}
			}
			string path = "";
			switch (carr)
			{
			case 2:
				path = "profession/warrior/wing_l_" + stage;
				break;
			case 3:
				path = "profession/mage/wing_l_" + stage;
				break;
			case 5:
				path = "profession/assa/wing_l_" + stage;
				break;
			}
			gameObject = Resources.Load<GameObject>(path);
			bool flag2 = gameObject != null;
			if (flag2)
			{
				this.m_SelfObj = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-77.34f, 0.45f, 15.028f), Quaternion.AngleAxis(180f, Vector3.up)) as GameObject);
				this.m_SelfObj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
				Transform[] componentsInChildren2 = this.m_SelfObj.GetComponentsInChildren<Transform>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Transform transform2 = componentsInChildren2[j];
					transform2.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
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

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				this.m_SelfObj.transform.Rotate(Vector3.up, -delta.x);
			}
		}
	}
}
