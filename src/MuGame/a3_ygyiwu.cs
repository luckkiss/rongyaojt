using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_ygyiwu : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_ygyiwu.<>c <>9 = new a3_ygyiwu.<>c();

			public static Action<GameObject> <>9__11_1;

			internal void <init>b__11_1(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_44", null), 0, default(Color), null);
			}
		}

		public static a3_ygyiwu instan;

		private int CloseIndex = 0;

		private int index = 0;

		private Dictionary<int, GameObject> God_obj = new Dictionary<int, GameObject>();

		private Dictionary<int, GameObject> Pre_obj = new Dictionary<int, GameObject>();

		private Image studybar;

		private GameObject plane;

		private GameObject scene_Camera;

		private GameObject scene_Obj;

		private GameObject m_SelfObj;

		private GameObject isthis;

		private bool isCanStudy = true;

		public override void init()
		{
			a3_ygyiwu.instan = this;
			this.plane = base.transform.FindChild("tab_01/info").gameObject;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("over"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onClose);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("btn/God_btn"), 1, 1);
			baseButton2.onClick = delegate(GameObject go)
			{
				this.index = 0;
				this.ref_topText();
			};
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.JL_SL, false);
			if (flag)
			{
				base.getGameObjectByPath("btn/lockp").SetActive(false);
			}
			else
			{
				base.getGameObjectByPath("btn/lockp").SetActive(true);
			}
			BaseButton arg_E7_0 = new BaseButton(base.getTransformByPath("btn/lockp"), 1, 1);
			Action<GameObject> arg_E7_1;
			if ((arg_E7_1 = a3_ygyiwu.<>c.<>9__11_1) == null)
			{
				arg_E7_1 = (a3_ygyiwu.<>c.<>9__11_1 = new Action<GameObject>(a3_ygyiwu.<>c.<>9.<init>b__11_1));
			}
			arg_E7_0.onClick = arg_E7_1;
			this.isthis = base.transform.FindChild("btn/this").gameObject;
			this.isthis.SetActive(false);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("btn/per_btn"), 1, 1);
			baseButton3.onClick = delegate(GameObject go)
			{
				this.index = 1;
				this.ref_topText();
			};
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("tab_02/go"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onStudy);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("tab_01/info/tach"), 1, 1);
			BaseButton baseButton6 = new BaseButton(base.transform.FindChild("tab_01/info/back"), 1, 1);
			new BaseButton(base.transform.FindChild("tab_01/God/help_God"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("tab_01/God/help_text").gameObject.SetActive(true);
			};
			new BaseButton(base.transform.FindChild("tab_01/per/help_Pre"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("tab_01/per/help_text").gameObject.SetActive(true);
			};
			new BaseButton(base.transform.FindChild("tab_01/God/help_text/close"), 1, 1).onClick = (new BaseButton(base.transform.FindChild("tab_01/God/help_text/tach"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("tab_01/God/help_text").gameObject.SetActive(false);
			});
			new BaseButton(base.transform.FindChild("tab_01/per/help_text/close"), 1, 1).onClick = (new BaseButton(base.transform.FindChild("tab_01/per/help_text/tach"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("tab_01/per/help_text").gameObject.SetActive(false);
			});
			baseButton6.onClick = (baseButton5.onClick = delegate(GameObject go)
			{
				bool flag2 = this.index == 0;
				if (flag2)
				{
					base.transform.FindChild("tab_01/God").gameObject.SetActive(true);
				}
				else
				{
					bool flag3 = this.index == 1;
					if (flag3)
					{
						base.transform.FindChild("tab_01/per").gameObject.SetActive(true);
					}
				}
				base.transform.FindChild("tab_01/info").gameObject.SetActive(false);
				base.transform.FindChild("btn").gameObject.SetActive(true);
			});
			BaseButton baseButton7 = new BaseButton(base.transform.FindChild("btn/doup"), 1, 1);
			baseButton7.onClick = delegate(GameObject go)
			{
				this.CloseIndex = 1;
				base.transform.FindChild("tab_01").gameObject.SetActive(false);
				base.transform.FindChild("btn").gameObject.SetActive(false);
				base.transform.FindChild("tab_02").gameObject.SetActive(true);
			};
			base.getEventTrigerByPath("tach").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			this.studybar = base.transform.FindChild("tab_02/bar").GetComponent<Image>();
			this.into_God();
			this.into_per();
			BaseProxy<A3_ygyiwuProxy>.getInstance().SendYGinfo(2u);
			this.refreshinfo(null);
		}

		public override void onShowed()
		{
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.JL_SL, false);
			if (flag)
			{
				base.getGameObjectByPath("btn/lockp").SetActive(false);
			}
			else
			{
				base.getGameObjectByPath("btn/lockp").SetActive(true);
			}
			BaseProxy<A3_ygyiwuProxy>.getInstance().addEventListener(A3_ygyiwuProxy.EVENT_YWINFO, new Action<GameEvent>(this.refreshinfo));
			BaseProxy<A3_ygyiwuProxy>.getInstance().addEventListener(A3_ygyiwuProxy.EVENT_ZHISHIINFO, new Action<GameEvent>(this.refoMy_yiwu));
			BaseProxy<A3_ygyiwuProxy>.getInstance().SendYGinfo(1u);
			this.CloseIndex = 0;
			bool flag2 = skill_a3._instance;
			if (flag2)
			{
				bool toygyw = skill_a3._instance.toygyw;
				if (toygyw)
				{
					this.index = 1;
					this.ref_topText();
					skill_a3._instance.toygyw = false;
				}
			}
			GRMap.GAME_CAMERA.SetActive(false);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			base.transform.FindChild("ig_bg_bg").gameObject.SetActive(false);
			this.index = 0;
			this.ref_topText();
			this.createAvatar();
		}

		public override void onClosed()
		{
			this.disposeAvatar();
			base.transform.FindChild("tab_01").gameObject.SetActive(true);
			base.transform.FindChild("btn").gameObject.SetActive(true);
			base.transform.FindChild("tab_02").gameObject.SetActive(false);
			base.transform.FindChild("tab_01/info").gameObject.SetActive(false);
			BaseProxy<A3_ygyiwuProxy>.getInstance().removeEventListener(A3_ygyiwuProxy.EVENT_YWINFO, new Action<GameEvent>(this.refreshinfo));
			BaseProxy<A3_ygyiwuProxy>.getInstance().removeEventListener(A3_ygyiwuProxy.EVENT_ZHISHIINFO, new Action<GameEvent>(this.refoMy_yiwu));
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
		}

		private void ref_topText()
		{
			base.transform.FindChild("tab_01/God/help_text").gameObject.SetActive(false);
			base.transform.FindChild("tab_01/per/help_text").gameObject.SetActive(false);
			bool flag = this.index == 0;
			if (flag)
			{
				base.transform.FindChild("tab_01/per").gameObject.SetActive(false);
				base.transform.FindChild("tab_01/God").gameObject.SetActive(true);
				this.isthis.SetActive(true);
				this.isthis.transform.SetParent(base.transform.FindChild("btn/God_btn"), false);
				this.isthis.transform.localPosition = Vector2.zero;
			}
			else
			{
				bool flag2 = this.index == 1;
				if (flag2)
				{
					base.transform.FindChild("tab_01/per").gameObject.SetActive(true);
					base.transform.FindChild("tab_01/God").gameObject.SetActive(false);
					this.isthis.SetActive(true);
					this.isthis.transform.SetParent(base.transform.FindChild("btn/per_btn"), false);
					this.isthis.transform.localPosition = Vector2.zero;
				}
			}
			this.disboosAvatar();
			SXML sXML = XMLMgr.instance.GetSXML("accent_relic.mod", "type==" + (this.index + 1));
			int @int = sXML.getInt("id");
			float @float = sXML.getFloat("scale");
			this.createAvatar_body(@int, @float);
		}

		private void onClose(GameObject go)
		{
			bool flag = this.CloseIndex == 0;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_YGYIWU);
			}
			else
			{
				base.transform.FindChild("tab_01").gameObject.SetActive(true);
				base.transform.FindChild("btn").gameObject.SetActive(true);
				base.transform.FindChild("tab_02").gameObject.SetActive(false);
				base.transform.FindChild("tab_01/info").gameObject.SetActive(false);
				this.ref_topText();
				this.CloseIndex = 0;
			}
		}

		private void into_God()
		{
			bool flag = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God.Count > 0;
			if (flag)
			{
				GameObject gameObject = base.transform.FindChild("tab_01/God/view/item").gameObject;
				RectTransform component = base.transform.FindChild("tab_01/God/view/con").GetComponent<RectTransform>();
				bool flag2 = component.childCount > 0;
				if (flag2)
				{
					for (int i = 0; i < component.childCount; i++)
					{
						UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
					}
				}
				for (int j = 1; j <= ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God.Count; j++)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.SetActive(true);
					gameObject2.transform.SetParent(component, false);
					gameObject2.transform.FindChild("name").gameObject.GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[j].name;
					gameObject2.transform.FindChild("icon").GetComponent<Image>().sprite = (Resources.Load("icon/ar/" + ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[j].iconid, typeof(Sprite)) as Sprite);
					int id = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[j].id;
					gameObject2.name = id.ToString();
					bool flag3 = !this.God_obj.ContainsKey(id);
					if (flag3)
					{
						this.God_obj[id] = gameObject2;
					}
					new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject go)
					{
						base.transform.FindChild("tab_01/God").gameObject.SetActive(false);
						this.plane.SetActive(true);
						base.transform.FindChild("btn").gameObject.SetActive(false);
						base.transform.FindChild("tab_01/info/info_text").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[int.Parse(go.name)].des;
						string name = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[int.Parse(go.name)].name;
						string place = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[int.Parse(go.name)].place;
						string awardName = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[int.Parse(go.name)].awardName;
						string awardDesc = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[int.Parse(go.name)].awardDesc;
						int iconid = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[int.Parse(go.name)].iconid;
						this.Click_info(0, name, int.Parse(go.name), awardDesc, iconid);
					};
				}
			}
		}

		private void Click_info(int type, string name, int index, string awardDesc, int iconid)
		{
			this.plane.transform.FindChild("get_text").GetComponent<Text>().text = name;
			this.plane.transform.FindChild("getinfo").GetComponent<Text>().text = awardDesc;
			this.plane.transform.FindChild("icon").GetComponent<Image>().sprite = (Resources.Load("icon/ar/" + iconid, typeof(Sprite)) as Sprite);
			bool flag = type == 0;
			if (flag)
			{
				bool flag2 = ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id > index;
				if (flag2)
				{
					this.plane.transform.FindChild("isGet").gameObject.SetActive(true);
				}
				else
				{
					this.plane.transform.FindChild("isGet").gameObject.SetActive(false);
				}
			}
			bool flag3 = type == 1;
			if (flag3)
			{
				bool flag4 = ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id > index;
				if (flag4)
				{
					this.plane.transform.FindChild("isGet").gameObject.SetActive(true);
				}
				else
				{
					this.plane.transform.FindChild("isGet").gameObject.SetActive(false);
				}
			}
		}

		public void refreshGod()
		{
			foreach (int current in this.God_obj.Keys)
			{
				this.God_obj[current].transform.FindChild("t1").gameObject.SetActive(false);
				this.God_obj[current].transform.FindChild("t2").gameObject.SetActive(false);
				this.God_obj[current].transform.FindChild("t3").gameObject.SetActive(false);
				bool flag = current < ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id;
				if (flag)
				{
					this.God_obj[current].transform.FindChild("t1").gameObject.SetActive(true);
					this.God_obj[current].transform.FindChild("t1/skilldec").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[current].awardDesc;
				}
				else
				{
					bool flag2 = current == ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id;
					if (flag2)
					{
						this.God_obj[current].transform.FindChild("t2").gameObject.SetActive(true);
						int needexp = ModelBase<a3_ygyiwuModel>.getInstance().GetYiWu_God(ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id).needexp;
						float num = (float)ModelBase<PlayerModel>.getInstance().accent_exp / (float)needexp;
						this.God_obj[current].transform.FindChild("t2/jindu").GetComponent<Text>().text = num * 100f + "%";
						this.God_obj[current].transform.FindChild("t2/barbg/bar").GetComponent<Image>().fillAmount = (float)ModelBase<PlayerModel>.getInstance().accent_exp / (float)needexp;
						this.God_obj[current].transform.FindChild("t2/zhanli").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_God[current].need_zdl.ToString();
						this.God_obj[current].transform.FindChild("t2/go").gameObject.SetActive(false);
						this.God_obj[current].transform.FindChild("t2/goo").gameObject.SetActive(false);
						bool flag3 = ModelBase<PlayerModel>.getInstance().accent_exp >= needexp;
						if (flag3)
						{
							this.God_obj[current].transform.FindChild("t2/go").gameObject.SetActive(true);
							new BaseButton(this.God_obj[current].transform.FindChild("t2/go").transform, 1, 1).onClick = delegate(GameObject go)
							{
								this.onInGodFB();
							};
						}
						else
						{
							this.God_obj[current].transform.FindChild("t2/goo").gameObject.SetActive(true);
						}
					}
					else
					{
						bool flag4 = current > ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id;
						if (flag4)
						{
							this.God_obj[current].transform.FindChild("t3").gameObject.SetActive(true);
						}
					}
				}
			}
		}

		public void refreshPre()
		{
			foreach (int current in this.Pre_obj.Keys)
			{
				this.Pre_obj[current].transform.FindChild("t1").gameObject.SetActive(false);
				this.Pre_obj[current].transform.FindChild("t2").gameObject.SetActive(false);
				this.Pre_obj[current].transform.FindChild("t3").gameObject.SetActive(false);
				bool flag = current < ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id;
				if (flag)
				{
					this.Pre_obj[current].transform.FindChild("t1").gameObject.SetActive(true);
					this.Pre_obj[current].transform.FindChild("t1/skilldec").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[current].awardDesc;
				}
				else
				{
					bool flag2 = current == ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id;
					if (flag2)
					{
						bool flag3 = this.canToNowPre();
						if (flag3)
						{
							this.Pre_obj[current].transform.FindChild("t2").gameObject.SetActive(true);
							this.Pre_obj[current].transform.FindChild("t2/zhanli").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[current].need_zdl.ToString();
							new BaseButton(this.Pre_obj[current].transform.FindChild("t2/go").transform, 1, 1).onClick = delegate(GameObject go)
							{
								this.onInPreFB();
							};
						}
						else
						{
							this.Pre_obj[current].transform.FindChild("t3").gameObject.SetActive(true);
							this.Pre_obj[current].transform.FindChild("t3/openlvl").GetComponent<Text>().text = string.Concat(new object[]
							{
								ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[current].needuplvl,
								"转",
								ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[current].needlvl,
								"级开启"
							});
						}
					}
					else
					{
						bool flag4 = current > ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id;
						if (flag4)
						{
							this.Pre_obj[current].transform.FindChild("t3").gameObject.SetActive(true);
							this.Pre_obj[current].transform.FindChild("t3/openlvl").GetComponent<Text>().text = string.Concat(new object[]
							{
								ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[current].needuplvl,
								"转",
								ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[current].needlvl,
								"级开启"
							});
						}
					}
				}
			}
		}

		private bool canToNowPre()
		{
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl < (ulong)((long)ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id].needuplvl);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id].needuplvl);
				if (flag2)
				{
					bool flag3 = (ulong)ModelBase<PlayerModel>.getInstance().lvl < (ulong)((long)ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id].needlvl);
					result = !flag3;
				}
				else
				{
					bool flag4 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id].needuplvl);
					result = flag4;
				}
			}
			return result;
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				this.m_SelfObj.transform.Rotate(Vector3.up, -delta.x);
			}
		}

		private void createAvatar()
		{
			bool flag = this.scene_Obj == null;
			if (flag)
			{
				GameObject original = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera");
				this.scene_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
				original = Resources.Load<GameObject>("profession/avatar_ui/show_scene");
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
				this.scene_Obj.transform.FindChild("scene_ta").localPosition = new Vector3(-2.2f, -0.112f, 0.166f);
				this.scene_Obj.transform.FindChild("sc_tz_lg").localPosition = new Vector3(-2.2f, -0.112f, 0.166f);
				this.scene_Obj.transform.FindChild("fx_sc").localPosition = new Vector3(-2.21f, 0f, 0f);
			}
		}

		private void createAvatar_body(int objid, float size)
		{
			GameObject original = Resources.Load<GameObject>("monster/" + objid);
			this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-75.26f, -0.561f, 14.91f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
			Transform[] componentsInChildren = this.m_SelfObj.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
			}
			Transform transform2 = this.m_SelfObj.transform.FindChild("model");
			bool flag = objid == 10070;
			if (flag)
			{
				transform2.FindChild("body/FX_idle").gameObject.SetActive(false);
			}
			transform2.localScale = new Vector3(size, size, size);
			transform2.gameObject.AddComponent<Summon_Base_Event>();
			bool flag2 = transform2 != null;
			if (flag2)
			{
				Animator component = transform2.GetComponent<Animator>();
				bool flag3 = component != null;
				if (flag3)
				{
					component.Rebind();
				}
				component.cullingMode = AnimatorCullingMode.AlwaysAnimate;
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

		public void disboosAvatar()
		{
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_SelfObj);
			}
		}

		private void onInGodFB()
		{
			int needexp = ModelBase<a3_ygyiwuModel>.getInstance().GetYiWu_God(ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id).needexp;
			bool flag = ModelBase<PlayerModel>.getInstance().accent_exp >= needexp;
			if (flag)
			{
				Debug.Log("Enter");
				Variant variant = new Variant();
				variant["mapid"] = 3334;
				variant["npcid"] = 0;
				variant["ltpid"] = ModelBase<a3_ygyiwuModel>.getInstance().nowGodFB_id;
				variant["diff_lvl"] = 1;
				BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
			}
			else
			{
				flytxt.instance.fly("不满足挑战条件!", 0, default(Color), null);
			}
		}

		private void onInPreFB()
		{
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl < (ulong)((long)ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id].needuplvl);
			if (flag)
			{
				flytxt.instance.fly("不满足挑战条件!", 0, default(Color), null);
			}
			else
			{
				bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id].needuplvl);
				if (flag2)
				{
					bool flag3 = (ulong)ModelBase<PlayerModel>.getInstance().lvl < (ulong)((long)ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id].needlvl);
					if (flag3)
					{
						flytxt.instance.fly("不满足挑战条件!", 0, default(Color), null);
						return;
					}
				}
				Debug.Log("Enter");
				Variant variant = new Variant();
				variant["mapid"] = 3334;
				variant["npcid"] = 0;
				variant["ltpid"] = ModelBase<a3_ygyiwuModel>.getInstance().nowPreFB_id;
				variant["diff_lvl"] = 1;
				BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant);
			}
		}

		private void refreshinfo(GameEvent e)
		{
			this.refreshGod();
			this.refreshPre();
		}

		private void into_per()
		{
			bool flag = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre.Count > 0;
			if (flag)
			{
				GameObject gameObject = base.transform.FindChild("tab_01/per/view/item").gameObject;
				RectTransform component = base.transform.FindChild("tab_01/per/view/con").GetComponent<RectTransform>();
				bool flag2 = component.childCount > 0;
				if (flag2)
				{
					for (int i = 0; i < component.childCount; i++)
					{
						UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
					}
				}
				for (int j = 1; j <= ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre.Count; j++)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.SetActive(true);
					gameObject2.transform.SetParent(component, false);
					gameObject2.transform.FindChild("name").gameObject.GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[j].name;
					gameObject2.transform.FindChild("icon").GetComponent<Image>().sprite = (Resources.Load("icon/ar/" + ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[j].iconid, typeof(Sprite)) as Sprite);
					int id = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[j].id;
					gameObject2.name = id.ToString();
					bool flag3 = !this.Pre_obj.ContainsKey(id);
					if (flag3)
					{
						this.Pre_obj[id] = gameObject2;
					}
					new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject go)
					{
						base.transform.FindChild("tab_01/per").gameObject.SetActive(false);
						GameObject gameObject3 = base.transform.FindChild("tab_01/info").gameObject;
						gameObject3.SetActive(true);
						base.transform.FindChild("btn").gameObject.SetActive(false);
						base.transform.FindChild("tab_01/info/info_text").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[int.Parse(go.name)].des;
						string name = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[int.Parse(go.name)].name;
						string awardDesc = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[int.Parse(go.name)].awardDesc;
						int iconid = ModelBase<a3_ygyiwuModel>.getInstance().Allywlist_Pre[int.Parse(go.name)].iconid;
						this.Click_info(1, name, int.Parse(go.name), awardDesc, iconid);
					};
				}
			}
		}

		private void into_My_yiwu()
		{
			Transform transform = base.transform.FindChild("tab_02/scrollview/con");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
				}
			}
			GameObject gameObject = base.transform.FindChild("tab_02/scrollview/item").gameObject;
			foreach (int current in ModelBase<a3_ygyiwuModel>.getInstance().yiwuList_Pre.Keys)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.SetActive(true);
				gameObject2.name = current.ToString();
				gameObject2.transform.SetParent(transform, false);
				gameObject2.transform.FindChild("name").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().yiwuList_Pre[current].name;
				gameObject2.transform.FindChild("info").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().yiwuList_Pre[current].eff;
			}
			foreach (int current2 in ModelBase<a3_ygyiwuModel>.getInstance().yiwuList_God.Keys)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject3.SetActive(true);
				gameObject3.name = current2.ToString();
				gameObject3.transform.SetParent(transform, false);
				gameObject3.transform.FindChild("name").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().yiwuList_God[current2].name;
				gameObject3.transform.FindChild("info").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().yiwuList_God[current2].eff;
			}
		}

		public void refoMy_yiwu(GameEvent e)
		{
			base.transform.FindChild("tab_02/lvl/tex").GetComponent<Text>().text = ModelBase<a3_ygyiwuModel>.getInstance().yiwuLvl + "级";
			this.ref_StudyBtn();
			bool flag = ModelBase<a3_ygyiwuModel>.getInstance().studyTime > 0u;
			if (flag)
			{
				base.InvokeRepeating("timess", 0f, 1f);
			}
		}

		private void timess()
		{
			ModelBase<a3_ygyiwuModel>.getInstance().studyTime -= 1u;
			bool flag = ModelBase<a3_ygyiwuModel>.getInstance().studyTime <= 0u;
			if (flag)
			{
				this.studybar.fillAmount = 1f;
				base.CancelInvoke("timess");
			}
			else
			{
				this.studybar.fillAmount = (float)((long)ModelBase<a3_ygyiwuModel>.getInstance().GetZTime((int)ModelBase<a3_ygyiwuModel>.getInstance().yiwuLvl) - (long)((ulong)ModelBase<a3_ygyiwuModel>.getInstance().studyTime)) / (float)ModelBase<a3_ygyiwuModel>.getInstance().GetZTime((int)ModelBase<a3_ygyiwuModel>.getInstance().yiwuLvl);
			}
		}

		public void ref_StudyBtn()
		{
			bool flag = ModelBase<a3_ygyiwuModel>.getInstance().studyTime > 0u;
			if (flag)
			{
				this.isCanStudy = false;
				base.transform.FindChild("tab_02/go/Text1").gameObject.SetActive(false);
				base.transform.FindChild("tab_02/go/Text2").gameObject.SetActive(true);
				base.InvokeRepeating("timess", 0f, 1f);
			}
			else
			{
				this.isCanStudy = true;
				base.transform.FindChild("tab_02/go/Text1").gameObject.SetActive(true);
				base.transform.FindChild("tab_02/go/Text2").gameObject.SetActive(false);
				base.CancelInvoke("timess");
				this.studybar.fillAmount = 1f;
			}
		}

		private void onStudy(GameObject go)
		{
			bool flag = this.isCanStudy;
			if (flag)
			{
				BaseProxy<A3_ygyiwuProxy>.getInstance().SendYGinfo(3u);
			}
			else
			{
				BaseProxy<A3_ygyiwuProxy>.getInstance().SendYGinfo(4u);
			}
		}
	}
}
