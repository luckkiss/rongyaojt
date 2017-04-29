using Cross;
using DG.Tweening;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class worldmap : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly worldmap.<>c <>9 = new worldmap.<>c();

			public static Action <>9__30_0;

			internal void <onGotoCity>b__30_0()
			{
				BaseProxy<MapProxy>.getInstance().sendBeginChangeMap(0, true, false);
			}
		}

		public static GameObject EFFECT_CHUANSONG1;

		public static GameObject EFFECT_CHUANSONG2;

		private BaseButton btclose;

		public TabControl tab;

		private GameObject m_goWorldmap;

		public GameObject m_goMapcon;

		private MiniMapItem curMiniMap;

		private GameObject goP;

		private GameObject goP1;

		private GameObject goP22;

		private GameObject sg;

		private ItemList npcList;

		private ItemList monsterList;

		private ItemList wayList;

		public static int mapid = 0;

		public static float ggnum = 1f;

		public static worldmap instance;

		private BaseButton btGoCity;

		private TickItem tick;

		private GameObject goTabrole;

		private GameObject temp1;

		private Vector3 vecBegin;

		private Vector3 vecEnd;

		private a3_mapChangeLine mapChangeLine;

		public Quaternion mapRotation;

		public static float tempH;

		public List<GameObject> ssg = new List<GameObject>();

		public static bool getmapid = false;

		private bool firstRun = true;

		private List<MapBt> lMap = new List<MapBt>();

		private Text curMap;

		private Text ditu;

		private static Dictionary<int, MapLinkItem> dMapLink;

		private List<Vector2> mapImgPath = new List<Vector2>();

		private static List<GameObject> mapImgpb = new List<GameObject>();

		private static GameObject plane;

		private static GameObject mapimg;

		public override void init()
		{
			this.ditu = base.getComponentByPath<Text>("shijieditu");
			this.m_goWorldmap = base.getGameObjectByPath("worldmap");
			this.m_goMapcon = base.getGameObjectByPath("mapcon");
			this.btclose = new BaseButton(base.getTransformByPath("btclose"), 1, 1);
			this.btclose.onClick = new Action<GameObject>(this.onClose);
			this.btGoCity = new BaseButton(base.getTransformByPath("tabrole/gotomain"), 1, 1);
			this.btGoCity.onClick = new Action<GameObject>(this.onGotoCity);
			GameObject gameObjectByPath = base.getGameObjectByPath("tabrole/temp");
			gameObjectByPath.SetActive(false);
			worldmap.tempH = gameObjectByPath.GetComponent<RectTransform>().sizeDelta.y;
			this.npcList = new ItemList(base.getTransformByPath("tabrole/npcmain"), 0, gameObjectByPath, new Action(this.onNpcListClick), new Action(this.onMonsterListClick), new Action(this.onWayListClick));
			this.monsterList = new ItemList(base.getTransformByPath("tabrole/monstermain"), 1, gameObjectByPath, new Action(this.onNpcListClick), new Action(this.onMonsterListClick), new Action(this.onWayListClick));
			this.wayList = new ItemList(base.getTransformByPath("tabrole/waymain"), 2, gameObjectByPath, new Action(this.onNpcListClick), new Action(this.onMonsterListClick), new Action(this.onWayListClick));
			this.goTabrole = base.getGameObjectByPath("tabrole");
			this.tick = new TickItem(new Action<float>(this.onUpdate));
			this.tab = new TabControl();
			this.tab.onClickHanle = new Action<TabControl>(this.ontab);
			this.tab.create(base.getGameObjectByPath("tab"), base.gameObject, 0, 0, false);
			this.goP = base.getGameObjectByPath("mapcon/p");
			this.goP1 = base.getGameObjectByPath("mapcon/icon");
			this.goP22 = base.transform.FindChild("worldmap/icon").gameObject;
			BaseButton baseButton = new BaseButton(base.getTransformByPath("tabrole/btnChangeLine"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnChangeLineClick);
			this.mapChangeLine = new a3_mapChangeLine(base.getTransformByPath("linePanel"));
			this.initWorldMap();
			bool flag = this.m_goMapcon.activeInHierarchy && BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag)
			{
				this.teampos();
			}
			this.mapRotation = Quaternion.Euler(0f, 0f, 0f);
		}

		public void teamWorldPic()
		{
			bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag)
			{
				worldmap.getmapid = true;
				BaseProxy<TeamProxy>.getInstance().SendWatchTeamInfo(BaseProxy<TeamProxy>.getInstance().MyTeamData.teamId);
			}
			Transform[] componentsInChildren = this.goP22.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag2 = transform.parent == this.goP22.transform;
				if (flag2)
				{
					transform.gameObject.SetActive(false);
				}
			}
			bool flag3 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null && BaseProxy<TeamProxy>.getInstance().teamMemberposData != null;
			if (flag3)
			{
				for (int j = 0; j < BaseProxy<TeamProxy>.getInstance().teamMemberposData.Count; j++)
				{
					uint mapId = BaseProxy<TeamProxy>.getInstance().teamMemberposData[j].mapId;
					bool isCaptain = BaseProxy<TeamProxy>.getInstance().teamMemberposData[j].isCaptain;
					if (isCaptain)
					{
						this.temp1 = this.goP22.transform.FindChild("signalteam").gameObject;
						this.temp1.SetActive(true);
					}
					else
					{
						this.temp1 = this.goP22.transform.FindChild("signal" + j).gameObject;
						this.temp1.SetActive(true);
					}
					bool flag4 = !BaseProxy<TeamProxy>.getInstance().teamMemberposData[j].online;
					if (flag4)
					{
						this.temp1.SetActive(false);
					}
					this.temp1.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.25f, 0.25f, 0.25f);
					float x = base.transform.FindChild("worldmap/map/" + mapId).localPosition.x;
					float y = base.transform.FindChild("worldmap/map/" + mapId).localPosition.y;
					this.temp1.transform.localPosition = new Vector3(x, y, 0f);
				}
			}
		}

		public void onGotoCity(GameObject go)
		{
			bool flag = !ModelBase<FindBestoModel>.getInstance().Canfly;
			if (flag)
			{
				flytxt.instance.fly(ModelBase<FindBestoModel>.getInstance().nofly_txt, 0, default(Color), null);
				InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP);
			}
			else
			{
				cd.updateHandle = new Action<cd>(worldmapsubwin.onCD);
				GameObject goeff = UnityEngine.Object.Instantiate<GameObject>(worldmap.EFFECT_CHUANSONG1);
				goeff.transform.SetParent(SelfRole._inst.m_curModel, false);
				Action arg_C5_0;
				if ((arg_C5_0 = worldmap.<>c.<>9__30_0) == null)
				{
					arg_C5_0 = (worldmap.<>c.<>9__30_0 = new Action(worldmap.<>c.<>9.<onGotoCity>b__30_0));
				}
				cd.show(arg_C5_0, 2.8f, false, delegate
				{
					UnityEngine.Object.Destroy(goeff);
				}, default(Vector3));
				InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP);
			}
		}

		public void teampos()
		{
			bool flag = this.m_goMapcon.activeInHierarchy && BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag)
			{
				this.resfreshTeamPos();
			}
		}

		public void worldteampos()
		{
			bool flag = this.m_goWorldmap.activeInHierarchy && BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag)
			{
				this.teamWorldPic();
			}
		}

		public void onUpdate(float s)
		{
			this.refreshPos();
		}

		public void planePic()
		{
			bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag)
			{
				bool flag2 = BaseProxy<TeamProxy>.getInstance().teamlist_position.Count != 0 && !BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
				if (flag2)
				{
					this.goP.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/pic/team_member");
					this.goP.GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.65f, 0.75f);
				}
				bool flag3 = BaseProxy<TeamProxy>.getInstance().teamlist_position.Count != 0 && BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain;
				if (flag3)
				{
					this.goP.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/pic/team_captain");
					this.goP.GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.65f, 0.75f);
				}
			}
			else
			{
				this.goP.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/pic/player");
			}
		}

		public void resfreshTeamPos()
		{
			BaseProxy<TeamProxy>.getInstance().SendCurrentMapTeamPos();
			Transform[] componentsInChildren = this.goP1.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent == this.goP1.transform;
				if (flag)
				{
					transform.gameObject.SetActive(false);
				}
			}
			bool flag2 = BaseProxy<TeamProxy>.getInstance().teamlist_position != null && BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
			if (flag2)
			{
				for (int j = 0; j < BaseProxy<TeamProxy>.getInstance().teamlist_position.Count; j++)
				{
					bool flag3 = !BaseProxy<TeamProxy>.getInstance().MyTeamData.meIsCaptain && BaseProxy<TeamProxy>.getInstance().MyTeamData.leaderCid == BaseProxy<TeamProxy>.getInstance().teamlist_position[j].cid;
					if (flag3)
					{
						this.temp1 = this.goP1.transform.FindChild("signalteam").gameObject;
						this.temp1.SetActive(true);
						this.temp1.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.25f, 0.25f, 0.25f);
					}
					else
					{
						this.temp1 = this.goP1.transform.FindChild("signal" + j).gameObject;
						this.temp1.SetActive(true);
						this.temp1.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.25f, 0.25f, 0.25f);
					}
					int x = (int)BaseProxy<TeamProxy>.getInstance().teamlist_position[j].x;
					int y = (int)BaseProxy<TeamProxy>.getInstance().teamlist_position[j].y;
					Vector3 vector = SceneCamera.getTeamPosOnMinMap(x, y, this.curMiniMap.mapScale);
					vector = this.mapRotation * vector;
					this.temp1.transform.localPosition = vector;
				}
			}
			this.planePic();
		}

		public void onNpcListClick()
		{
			this.onRoleTab(0);
		}

		public void onMonsterListClick()
		{
			this.onRoleTab(1);
		}

		public void onWayListClick()
		{
			this.onRoleTab(2);
		}

		public override void onShowed()
		{
			worldmap.instance = this;
			this.firstRun = true;
			this.tab.setSelectedIndex(1, true);
			TickMgr.instance.addTick(this.tick);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			GRMap.GAME_CAMERA.SetActive(false);
			this.m_goWorldmap.transform.FindChild("map/box_dxc").gameObject.SetActive(false);
			this.m_goWorldmap.transform.FindChild("map/box_ahsd").gameObject.SetActive(false);
			bool worldmap = InterfaceMgr.getInstance().worldmap;
			if (worldmap)
			{
				this.tab.setSelectedIndex(0, false);
				InterfaceMgr.getInstance().worldmap = false;
			}
		}

		public override void onClosed()
		{
			bool flag = a3_active.instance != null;
			if (flag)
			{
				bool map_light = a3_active.instance.map_light;
				if (map_light)
				{
					a3_active.instance.map_light = false;
				}
			}
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
			this.m_goWorldmap.transform.DOKill(false);
			TickMgr.instance.removeTick(this.tick);
			worldmap.instance = null;
			worldmap.mapid = 0;
			this.mapChangeLine.Show(false);
			base.onClosed();
		}

		public void onRoleTab(int type)
		{
			bool flag = this.tab == null || this.tab.getSeletedIndex() == 0 || this.curMiniMap == null;
			if (!flag)
			{
				Transform transform = this.curMiniMap.__mainTrans.Find("way");
				Transform transform2 = this.curMiniMap.__mainTrans.Find("npc");
				Transform transform3 = this.curMiniMap.__mainTrans.Find("monster");
				switch (type)
				{
				case 0:
				{
					bool flag2 = transform2;
					if (flag2)
					{
						transform2.gameObject.SetActive(true);
					}
					bool flag3 = transform3;
					if (flag3)
					{
						transform3.gameObject.SetActive(false);
					}
					bool flag4 = transform;
					if (flag4)
					{
						transform.gameObject.SetActive(false);
					}
					this.monsterList.visiable = false;
					this.npcList.visiable = true;
					this.wayList.visiable = false;
					break;
				}
				case 1:
				{
					bool flag5 = transform2;
					if (flag5)
					{
						transform2.gameObject.SetActive(false);
					}
					bool flag6 = transform3;
					if (flag6)
					{
						transform3.gameObject.SetActive(true);
					}
					bool flag7 = transform;
					if (flag7)
					{
						transform.gameObject.SetActive(false);
					}
					this.monsterList.visiable = true;
					this.npcList.visiable = false;
					this.wayList.visiable = false;
					break;
				}
				case 2:
				{
					bool flag8 = transform2;
					if (flag8)
					{
						transform2.gameObject.SetActive(false);
					}
					bool flag9 = transform3;
					if (flag9)
					{
						transform3.gameObject.SetActive(false);
					}
					bool flag10 = transform;
					if (flag10)
					{
						transform.gameObject.SetActive(true);
					}
					this.monsterList.visiable = false;
					this.npcList.visiable = false;
					this.wayList.visiable = true;
					break;
				}
				}
			}
		}

		public void initWorldMap()
		{
			Transform transformByPath = base.getTransformByPath("worldmap/bts");
			Transform transformByPath2 = base.getTransformByPath("worldmap/map");
			for (int i = 0; i < transformByPath.childCount; i++)
			{
				Transform child = transformByPath.GetChild(i);
				Transform transMap = transformByPath2.FindChild(child.gameObject.name);
				this.lMap.Add(new MapBt(child, transMap));
			}
		}

		public void changetab(int idx)
		{
			bool flag = idx == 0;
			if (flag)
			{
				this.m_goWorldmap.SetActive(true);
				bool flag2 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
				if (flag2)
				{
					base.InvokeRepeating("teampos", 0f, 0.5f);
				}
				this.m_goMapcon.SetActive(true);
				this.goTabrole.SetActive(false);
				this.monsterList.visiable = false;
				this.npcList.visiable = false;
				bool flag3 = a3_active.instance != null;
				if (flag3)
				{
					bool map_light = a3_active.instance.map_light;
					if (map_light)
					{
						this.m_goWorldmap.transform.FindChild("map/box_dxc").gameObject.SetActive(true);
						this.m_goWorldmap.transform.FindChild("map/box_ahsd").gameObject.SetActive(true);
					}
				}
				for (int i = 0; i < this.lMap.Count; i++)
				{
					this.lMap[i].refresh();
				}
				bool flag4 = this.curMiniMap != null;
				if (flag4)
				{
					this.curMap = this.curMiniMap.transform.FindChild("a/mainbt/Text").GetComponent<Text>();
					this.curMap.gameObject.SetActive(false);
					this.ditu.gameObject.SetActive(true);
				}
			}
			else
			{
				bool flag5 = this.curMiniMap != null;
				if (flag5)
				{
					this.ditu.gameObject.SetActive(false);
					this.curMap = this.curMiniMap.transform.FindChild("a/mainbt/Text").GetComponent<Text>();
					this.curMap.gameObject.SetActive(true);
				}
				this.m_goWorldmap.SetActive(false);
				base.InvokeRepeating("worldteampos", 0f, 5f);
				this.m_goMapcon.SetActive(true);
				this.goTabrole.SetActive(true);
				this.onRoleTab(2);
				bool flag6 = worldmap.mapid == 0;
				if (flag6)
				{
					worldmap.mapid = GRMap.instance.m_nCurMapID;
				}
				bool flag7 = this.curMiniMap == null || this.curMiniMap.id != worldmap.mapid;
				if (flag7)
				{
					this.refreshMiniMap();
				}
				this.refreshPos();
				SXML sXML = XMLMgr.instance.GetSXML("mappoint", "");
				List<SXML> nodeList = sXML.GetNodeList("p", "mapid==" + worldmap.mapid);
				this.npcList.refresh(nodeList);
				this.wayList.refresh(nodeList);
				this.monsterList.refresh(nodeList);
			}
			this.firstRun = false;
		}

		public void refreshMiniMap()
		{
			bool flag = this.curMiniMap != null;
			if (flag)
			{
				this.curMiniMap.dispose();
				this.curMiniMap = null;
			}
			GameObject gameObject = Resources.Load("map/prefab/map" + worldmap.mapid) as GameObject;
			bool flag2 = gameObject == null;
			if (!flag2)
			{
				SXML sXML = XMLMgr.instance.GetSXML("mappoint.trans_remind", "map_id==" + worldmap.mapid);
				bool flag3 = sXML.getInt("rotation") != -1;
				if (flag3)
				{
					this.mapRotation = (this.mapRotation = Quaternion.Euler(0f, 0f, (float)sXML.getInt("rotation")));
				}
				else
				{
					this.mapRotation = Quaternion.Euler(0f, 0f, 0f);
				}
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.transform.SetParent(this.m_goMapcon.transform, false);
				this.curMiniMap = new MiniMapItem(gameObject2.transform, this.mapRotation);
				this.curMiniMap.id = worldmap.mapid;
				Transform transform = this.curMiniMap.__mainTrans.Find("none");
				bool flag4 = transform != null;
				if (flag4)
				{
					transform.gameObject.SetActive(false);
				}
				gameObject2.transform.SetSiblingIndex(1);
			}
		}

		public void refreshPos()
		{
			this.goP.SetActive(worldmap.mapid == GRMap.instance.m_nCurMapID || worldmap.mapid == 0);
			bool flag = this.curMiniMap == null || !this.goP.active;
			if (!flag)
			{
				Vector3 vector = SceneCamera.getPosOnMiniMap(this.curMiniMap.mapScale);
				vector = this.mapRotation * vector;
				this.goP.transform.localPosition = vector;
				vector = SelfRole._inst.m_curModel.eulerAngles;
				vector.y = -vector.y;
				this.goP.transform.localEulerAngles = new Vector3(0f, 0f, 180f - SelfRole._inst.m_curModel.eulerAngles.y + this.mapRotation.eulerAngles.z);
			}
		}

		public void ontab(TabControl t)
		{
			int seletedIndex = t.getSeletedIndex();
			this.changetab(seletedIndex);
			bool flag = seletedIndex == 1;
			if (flag)
			{
				this.onRoleTab(2);
			}
		}

		public void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP);
			bool flag = a3_active.instance != null;
			if (flag)
			{
				bool map_light = a3_active.instance.map_light;
				if (map_light)
				{
					a3_active.instance.map_light = false;
				}
			}
		}

		private static void initLink()
		{
			worldmap.dMapLink = new Dictionary<int, MapLinkItem>();
			Dictionary<uint, Variant> mapConfs = SvrMapConfig.instance._mapConfs;
			foreach (Variant current in mapConfs.Values)
			{
				MapLinkItem mapLinkItem = new MapLinkItem();
				mapLinkItem.mapid = current["id"];
				bool flag = !current.ContainsKey("l");
				if (!flag)
				{
					List<Variant> arr = current["l"]._arr;
					for (int i = 0; i < arr.Count; i++)
					{
						Variant variant = arr[i];
						bool flag2 = variant.ContainsKey("trans") && variant["trans"] == 1;
						if (flag2)
						{
							MapLinkInfo mapLinkInfo = new MapLinkInfo();
							mapLinkInfo.id = variant["id"];
							mapLinkInfo.mapid = mapLinkItem.mapid;
							mapLinkInfo.toMap = variant["id"];
							mapLinkInfo.id = variant["gto"];
							mapLinkInfo.x = variant["ux"];
							mapLinkInfo.y = variant["uz"];
							mapLinkItem.lLink.Add(mapLinkInfo);
						}
					}
					worldmap.dMapLink[mapLinkItem.mapid] = mapLinkItem;
				}
			}
		}

		public static bool getMapLine(int begin, int end, List<MapLinkInfo> line, Dictionary<int, int> dfinded = null)
		{
			bool flag = worldmap.dMapLink == null;
			if (flag)
			{
				worldmap.initLink();
			}
			bool flag2 = dfinded == null;
			if (flag2)
			{
				dfinded = new Dictionary<int, int>();
			}
			bool flag3 = begin == end;
			bool result;
			if (flag3)
			{
				result = true;
			}
			else
			{
				bool flag4 = !worldmap.dMapLink.ContainsKey(begin);
				if (flag4)
				{
					result = false;
				}
				else
				{
					MapLinkItem mapLinkItem = worldmap.dMapLink[begin];
					dfinded[begin] = 1;
					for (int i = 0; i < mapLinkItem.lLink.Count; i++)
					{
						int id = mapLinkItem.lLink[i].id;
						bool flag5 = dfinded.ContainsKey(id);
						if (!flag5)
						{
							bool flag6 = id == end;
							if (flag6)
							{
								line.Insert(0, mapLinkItem.lLink[i]);
								result = true;
								return result;
							}
							bool mapLine = worldmap.getMapLine(id, end, line, dfinded);
							if (mapLine)
							{
								line.Insert(0, mapLinkItem.lLink[i]);
								result = true;
								return result;
							}
						}
					}
					result = false;
				}
			}
			return result;
		}

		private void onBtnChangeLineClick(GameObject go)
		{
			this.mapChangeLine.Show(true);
		}

		public void DrawMapImage(NavMeshPath path)
		{
			bool flag = worldmap.plane != null;
			if (flag)
			{
				worldmap.Desmapimg();
			}
			else
			{
				worldmap.mapimg = Resources.Load<GameObject>("icon/scene/mappathIamge");
				worldmap.plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
				worldmap.plane.name = "NmaImageList";
				worldmap.plane.transform.SetParent(base.transform.Find("mapcon").transform);
				worldmap.plane.transform.localPosition = Vector3.zero;
				worldmap.plane.transform.localScale = new Vector3(1f, 1f, 1f);
				worldmap.plane.transform.SetSiblingIndex(base.transform.Find("mapcon/p").transform.GetSiblingIndex());
			}
			this.mapImgPath.Clear();
			for (int i = 0; i < path.corners.Length; i++)
			{
				bool flag2 = i != path.corners.Length - 1;
				if (flag2)
				{
					float num = Vector3.Distance(path.corners[i + 1], path.corners[i]);
					bool flag3 = num > 5f;
					if (flag3)
					{
						int num2 = (int)(num / 5f);
						Vector3 a = path.corners[i + 1] - path.corners[i];
						Vector3 b = a / (float)(num2 + 1);
						Vector3 vector = path.corners[i];
						for (int j = 0; j < num2; j++)
						{
							vector += b;
							this.mapImgPath.Add(SceneCamera.getPosOnMiniMapNMA(vector, 0.45f));
						}
						this.mapImgPath.Add(SceneCamera.getPosOnMiniMapNMA(path.corners[i + 1], 0.45f));
					}
					else
					{
						bool flag4 = num > 2f;
						if (flag4)
						{
							this.mapImgPath.Add(SceneCamera.getPosOnMiniMapNMA(path.corners[i + 1], 0.45f));
						}
					}
				}
			}
			for (int k = 0; k < this.mapImgPath.Count; k++)
			{
				bool flag5 = this.mapImgPath.Count > worldmap.mapImgpb.Count;
				if (flag5)
				{
					for (int l = 0; l < this.mapImgPath.Count - worldmap.mapImgpb.Count; l++)
					{
						worldmap.mapImgpb.Add(UnityEngine.Object.Instantiate<GameObject>(worldmap.mapimg));
					}
				}
				worldmap.mapImgpb[k].transform.SetParent(worldmap.plane.transform);
				worldmap.mapImgpb[k].transform.localPosition = this.mapRotation * this.mapImgPath[k];
			}
		}

		public static void Desmapimg()
		{
			bool flag = worldmap.mapImgpb.Count != 0;
			if (flag)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
				gameObject.name = "GC";
				for (int i = 0; i < worldmap.mapImgpb.Count; i++)
				{
					worldmap.mapImgpb[i].transform.SetParent(gameObject.transform);
					worldmap.mapImgpb[i].SetActive(false);
				}
				worldmap.mapImgpb.Clear();
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}
}
