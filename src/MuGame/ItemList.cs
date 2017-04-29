using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class ItemList : Skin
	{
		private Button btWay;

		private Button btNpc;

		private Button btMonster;

		private Transform transCon;

		private ScrollControler scrollControler;

		private GameObject itemListView;

		private GridLayoutGroup item_Parent;

		private int mapid = 0;

		private int type;

		private GameObject tempItem;

		private Action npcHandle;

		private Action monHandle;

		private Action wayHandle;

		private int childCount;

		private int itemnum;

		public ItemList(Transform trans, int t, GameObject temp, Action npcClick, Action monClick, Action wayClick) : base(trans)
		{
			this.wayHandle = wayClick;
			this.npcHandle = npcClick;
			this.monHandle = monClick;
			this.tempItem = temp;
			this.type = t;
			this.initUI();
		}

		private void initUI()
		{
			this.btWay = base.getComponentByPath<Button>("way");
			this.btNpc = base.getComponentByPath<Button>("npc");
			this.btMonster = base.getComponentByPath<Button>("monster");
			this.transCon = base.getTransformByPath("con/con");
			bool flag = this.type == 0;
			if (flag)
			{
				this.btNpc.interactable = false;
			}
			else
			{
				bool flag2 = this.type == 1;
				if (flag2)
				{
					this.btMonster.interactable = false;
				}
				else
				{
					bool flag3 = this.type == 2;
					if (flag3)
					{
						this.btWay.interactable = false;
					}
				}
			}
			this.btMonster.onClick.AddListener(new UnityAction(this.onMonsterClick));
			this.btNpc.onClick.AddListener(new UnityAction(this.onNpcClick));
			this.btWay.onClick.AddListener(new UnityAction(this.onWayClick));
			this.itemListView = base.transform.FindChild("con/con").gameObject;
			this.item_Parent = this.itemListView.GetComponent<GridLayoutGroup>();
			this.scrollControler = new ScrollControler();
			ScrollRect component = base.transform.FindChild("con").GetComponent<ScrollRect>();
			this.scrollControler.create(component, 4);
		}

		protected void refreshScrollRect()
		{
			int num = this.itemListView.transform.childCount;
			bool flag = num <= 0;
			if (!flag)
			{
				RectTransform component = this.itemListView.GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(0f, (float)this.itemnum * (worldmap.tempH + 2f));
			}
		}

		public void clear()
		{
			Transform transform = this.transCon.transform;
			for (int i = 0; i < this.transCon.GetChildCount(); i++)
			{
				UnityEngine.Object.Destroy(this.transCon.GetChild(i).gameObject);
			}
		}

		public void refresh(List<SXML> list)
		{
			this.clear();
			bool flag = list.Count == 0;
			if (!flag)
			{
				this.itemnum = 0;
				for (int i = 0; i < list.Count; i++)
				{
					SXML xml = list[i];
					bool flag2 = xml.getInt("type") != this.type;
					if (!flag2)
					{
						this.itemnum++;
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tempItem);
						gameObject.SetActive(true);
						gameObject.transform.SetParent(this.transCon, false);
						gameObject.transform.FindChild("Text").GetComponent<Text>().text = xml.getString("name");
						gameObject.name = xml.getString(xml.getString("id"));
						Button component = gameObject.GetComponent<Button>();
						component.onClick.AddListener(delegate
						{
							bool flag3 = worldmap.mapid == GRMap.instance.m_nCurMapID;
							if (flag3)
							{
								Vector3 sourcePosition = new Vector3(xml.getFloat("ux"), 0f, xml.getFloat("uy"));
								NavMeshHit navMeshHit;
								NavMesh.SamplePosition(sourcePosition, out navMeshHit, 20f, NavmeshUtils.allARE);
								SelfRole.moveto(navMeshHit.position, null, false, 0.3f, true);
							}
							else
							{
								Vector3 pos = new Vector3(xml.getFloat("ux"), 0f, xml.getFloat("uy"));
								SelfRole.moveto(worldmap.mapid, pos, null, 0.3f);
							}
						});
					}
				}
				this.refreshScrollRect();
			}
		}

		private void onMonsterClick()
		{
			this.monHandle();
		}

		private void onNpcClick()
		{
			this.npcHandle();
		}

		private void onWayClick()
		{
			this.wayHandle();
		}
	}
}
