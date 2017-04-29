using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class MiniMapItem : Skin
	{
		public int id = 0;

		private Transform map;

		private List<GameObject> lNpc = new List<GameObject>();

		private List<GameObject> lMonster = new List<GameObject>();

		private Quaternion mapRotation;

		private GameObject goCon;

		public float mapScale
		{
			get
			{
				return this.map.localScale.x;
			}
		}

		public MiniMapItem(Transform trans, Quaternion rotation) : base(trans)
		{
			this.mapRotation = rotation;
			SXML sXML = XMLMgr.instance.GetSXML("mappoint", "");
			int num = worldmap.mapid * 100;
			Transform transformByPath = base.getTransformByPath("npc");
			transformByPath.localRotation = rotation;
			for (int i = 0; i < transformByPath.childCount; i++)
			{
				Transform child = transformByPath.GetChild(i);
				child.localRotation = Quaternion.Inverse(rotation);
				this.lNpc.Add(child.gameObject);
			}
			Transform transformByPath2 = base.getTransformByPath("way");
			transformByPath2.localRotation = rotation;
			for (int j = 0; j < transformByPath2.childCount; j++)
			{
				Transform child2 = transformByPath2.GetChild(j);
				child2.localRotation = Quaternion.Inverse(rotation);
			}
			Transform transformByPath3 = base.getTransformByPath("monster");
			transformByPath3.localRotation = rotation;
			for (int k = 0; k < transformByPath3.childCount; k++)
			{
				Transform child3 = transformByPath3.GetChild(k);
				child3.localRotation = Quaternion.Inverse(rotation);
				this.lMonster.Add(child3.gameObject);
				Transform transform = child3.FindChild("lv");
				bool flag = transform == null;
				if (!flag)
				{
					Text component = transform.GetComponent<Text>();
					SXML node = sXML.GetNode("p", "id==" + (num + int.Parse(child3.gameObject.name)));
					int @int = node.getInt("lv_up");
					int int2 = node.getInt("lv");
					component.text = ContMgr.getCont("worldmap_lv", new string[]
					{
						@int.ToString(),
						int2.ToString()
					});
				}
			}
			this.map = base.getTransformByPath("map");
			this.map.localRotation = rotation;
			EventTriggerListener.Get(base.gameObject).onPointClick = new EventTriggerListener.VectorDelegate(this.onMapClick);
			Transform transform2 = base.transform.FindChild("namebt");
			bool flag2 = transform2 != null;
			if (flag2)
			{
				Transform transform3 = transform2.FindChild("con");
				bool flag3 = transform3 == null;
				if (!flag3)
				{
					this.goCon = transform3.gameObject;
					BaseButton baseButton = new BaseButton(transform2.FindChild("mainbt"), 1, 1);
					baseButton.onClick = delegate(GameObject go)
					{
						this.goCon.SetActive(!this.goCon.active);
					};
					this.goCon.SetActive(false);
				}
			}
		}

		private void onMapClick(GameObject go, Vector2 mappos)
		{
			Vector2 vector;
			bool flag = RectTransformUtility.ScreenPointToLocalPointInRectangle(worldmap.instance.m_goMapcon.GetComponent<RectTransform>(), mappos, InterfaceMgr.ui_Camera_cam, out vector);
			if (flag)
			{
				vector = Quaternion.Inverse(this.mapRotation) * vector;
				Vector3 posByMiniMap = SceneCamera.getPosByMiniMap(vector, this.mapScale, 20f);
				bool flag2 = posByMiniMap.x == float.PositiveInfinity;
				if (!flag2)
				{
					bool autofighting = SelfRole.fsm.Autofighting;
					if (autofighting)
					{
						SelfRole.fsm.Stop();
					}
					SelfRole.moveto(posByMiniMap, null, false, 0.3f, true);
				}
			}
		}

		private void onmapClick(GameObject go)
		{
			worldmap.mapid = int.Parse(go.name);
			worldmap.instance.refreshMiniMap();
			worldmap.instance.refreshPos();
		}

		public void dispose()
		{
			base.destoryGo();
		}
	}
}
