using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_summoninfo : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_summoninfo.<>c <>9 = new a3_summoninfo.<>c();

			public static Action<GameObject> <>9__4_0;

			internal void <init>b__4_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SUMMONINFO);
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

		private int tpid = 0;

		public override void init()
		{
			this.stars = base.transform.FindChild("stars");
			BaseButton arg_48_0 = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			Action<GameObject> arg_48_1;
			if ((arg_48_1 = a3_summoninfo.<>c.<>9__4_0) == null)
			{
				arg_48_1 = (a3_summoninfo.<>c.<>9__4_0 = new Action<GameObject>(a3_summoninfo.<>c.<>9.<init>b__4_0));
			}
			arg_48_0.onClick = arg_48_1;
			base.getEventTrigerByPath("avatar_touch").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
		}

		public override void onShowed()
		{
			BaseProxy<A3_SummonProxy>.getInstance().addEventListener(A3_SummonProxy.EVENT_SUMINFO, new Action<GameEvent>(this.GetInfo));
			bool flag = this.uiData != null && this.uiData.Count > 0;
			uint id;
			if (flag)
			{
				id = (uint)this.uiData[0];
			}
			else
			{
				id = SelfRole._inst.m_LockRole.m_unCID;
			}
			BaseProxy<A3_SummonProxy>.getInstance().getsummonInfo(id);
			base.transform.SetAsLastSibling();
			GRMap.GAME_CAMERA.SetActive(false);
			base.transform.FindChild("ig_bg_bg").gameObject.SetActive(false);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
		}

		public override void onClosed()
		{
			this.disposeAvatar();
			BaseProxy<A3_SummonProxy>.getInstance().removeEventListener(A3_SummonProxy.EVENT_SUMINFO, new Action<GameEvent>(this.GetInfo));
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
		}

		private void GetInfo(GameEvent e)
		{
			Variant data = e.data;
			this.tpid = data["summon"]["tpid"];
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + this.tpid);
			base.transform.FindChild("fighting/value").GetComponent<Text>().text = data["summon"]["combpt"];
			base.transform.FindChild("playerInfo/panel_attr/name").GetComponent<Text>().text = sXML.getString("item_name");
			base.transform.FindChild("playerInfo/panel_attr/lv").GetComponent<Text>().text = "LV " + data["summon"]["level"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/longlife/Text").GetComponent<Text>().text = data["summon"]["life"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/luck/Text").GetComponent<Text>().text = data["summon"]["luckly"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/blood/Text").GetComponent<Text>().text = ((data["summon"]["bloodline"] > 1) ? "光" : "暗");
			base.transform.FindChild("playerInfo/panel_attr/right/up/name/Text").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().IntNaturalToStr(data["summon"]["speciality"]);
			base.transform.FindChild("playerInfo/panel_attr/right/up/att/Text").GetComponent<Text>().text = data["summon"]["att"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/def/Text").GetComponent<Text>().text = data["summon"]["def"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/agi/Text").GetComponent<Text>().text = data["summon"]["agi"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/con/Text").GetComponent<Text>().text = data["summon"]["con"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/life/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["max_hp"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/phyatk/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["min_attack"] + "-" + data["summon"]["battleAttrs"]["max_attack"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/phydef/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["physics_def"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/manadef/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["magic_def"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/hit/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["physics_dmg_red"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/manaatk/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["magic_dmg_red"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/crit/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["fatal_att"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/dodge/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["dodge"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/fatal_damage/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["fatal_damage"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/hitit/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["hit"];
			base.transform.FindChild("playerInfo/panel_attr/right/up/reflect_crit_rate/Text").GetComponent<Text>().text = data["summon"]["battleAttrs"]["fatal_dodge"];
			this.createAvatar();
			this.setStar(data["summon"]["talent"]);
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Variant variant = data["summon"]["skills"];
			for (int i = 0; i < variant.Count; i++)
			{
				dictionary[variant[i]["index"]] = variant[i]["skill_id"];
			}
			this.setSkills(data["summon"]["skill_num"], dictionary);
		}

		private void setStar(int starlvl)
		{
			for (int i = 0; i < 5; i++)
			{
				this.stars.GetChild(i).FindChild("b").gameObject.SetActive(false);
			}
			for (int j = 0; j < starlvl; j++)
			{
				this.stars.GetChild(j).FindChild("b").gameObject.SetActive(true);
			}
		}

		private void setSkills(int skillnum, Dictionary<int, int> skills)
		{
			Transform transform = base.transform.FindChild("skills");
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).FindChild("lock").gameObject.SetActive(true);
				transform.GetChild(i).FindChild("icon").gameObject.SetActive(false);
				bool flag = skills.ContainsKey(i + 1);
				if (flag)
				{
					SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + skills[i + 1]);
					transform.GetChild(i).FindChild("icon/icon").GetComponent<Image>().sprite = (Resources.Load("icon/smskill/" + sXML.getInt("icon"), typeof(Sprite)) as Sprite);
					transform.GetChild(i).FindChild("icon").gameObject.SetActive(true);
				}
			}
			for (int j = 0; j < skillnum; j++)
			{
				transform.GetChild(j).FindChild("lock").gameObject.SetActive(false);
			}
		}

		public void createAvatar()
		{
			GameObject original = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera");
			this.scene_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
			original = Resources.Load<GameObject>("profession/avatar_ui/show_scene");
			this.scene_Obj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.49f, 15.1f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
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
			SXML sXML = XMLMgr.instance.GetSXML("callbeast", "");
			SXML node = sXML.GetNode("callbeast", "id==" + this.tpid);
			int @int = node.getInt("mid");
			SXML sXML2 = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + @int);
			int int2 = sXML2.getInt("obj");
			original = Resources.Load<GameObject>("monster/" + int2);
			this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(-77.38f, -0.46f, 14.92f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
			Transform[] componentsInChildren2 = this.m_SelfObj.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform2 = componentsInChildren2[j];
				transform2.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
			}
			Transform transform3 = this.m_SelfObj.transform.FindChild("model");
			transform3.gameObject.AddComponent<Summon_Base_Event>();
			transform3.Rotate(Vector3.up, (float)(90 - sXML2.getInt("smshow_face")));
			transform3.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			Animator component = transform3.GetComponent<Animator>();
			component.cullingMode = AnimatorCullingMode.AlwaysAnimate;
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
