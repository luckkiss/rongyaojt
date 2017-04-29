using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_selectcha : FloatUi
	{
		private Button returnBtn;

		private Button enterGameBtn;

		private BaseButton[] chooseBtns;

		private Dictionary<uint, GameObject> avatars = new Dictionary<uint, GameObject>();

		private Dictionary<uint, GameObject> avatars_mon = new Dictionary<uint, GameObject>();

		private int chaSelectedIndex = -1;

		private uint chaCid = 0u;

		private GameObject chaAvatar = null;

		private GameObject chaAvatar_mon = null;

		private AsyncOperation async = null;

		private GameObject standPoint = null;

		private ProfessionAvatar m_curProAvatar = null;

		private SXML itemXML = null;

		private IEnumerator LoadScene()
		{
			this.async = SceneManager.LoadSceneAsync("select_role_scene");
			yield return this.async;
			bool flag = MediaClient.getInstance()._curMusicUrl != "audio/map/music/0";
			if (flag)
			{
				MediaClient.getInstance().PlayMusicUrl("audio/map/music/0", null, true);
				Application.DontDestroyOnLoad(GameObject.Find("Audio"));
			}
			yield break;
		}

		public override void init()
		{
			this.returnBtn = base.getComponentByPath<Button>("returnBtn");
			this.enterGameBtn = base.getComponentByPath<Button>("enterGameBtn");
			this.chooseBtns = new BaseButton[]
			{
				new BaseButton(base.getTransformByPath("btnChoose/chaBtn0"), 1, 1),
				new BaseButton(base.getTransformByPath("btnChoose/chaBtn1"), 1, 1),
				new BaseButton(base.getTransformByPath("btnChoose/chaBtn2"), 1, 1),
				new BaseButton(base.getTransformByPath("btnChoose/chaBtn3"), 1, 1)
			};
			base.getEventTrigerByPath("returnBtn").onClick = new EventTriggerListener.VoidDelegate(this.onReturnClick);
			base.getEventTrigerByPath("enterGameBtn").onClick = new EventTriggerListener.VoidDelegate(this.onEnterGameClick);
			base.getEventTrigerByPath("TouchDrag").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			base.getEventTrigerByPath("deleteBtn").onClick = new EventTriggerListener.VoidDelegate(this.onDeleteChaClick);
			muNetCleint.instance.charsInfoInst.addEventListener(4032u, new Action<GameEvent>(this.onDeleteChar));
		}

		private void Update()
		{
			bool flag = this.async != null && this.async.isDone;
			if (flag)
			{
				this.async = null;
				this.standPoint = GameObject.Find("standpoint");
				Variant chas = muNetCleint.instance.charsInfoInst.getChas();
				bool flag2 = chas.Count > 0;
				if (flag2)
				{
					this.onChooseCha(this.chooseBtns[0].gameObject);
				}
			}
			bool flag3 = this.m_curProAvatar != null;
			if (flag3)
			{
				this.m_curProAvatar.FrameMove();
			}
		}

		public override void onShowed()
		{
			GRMap.DontDestroyBaseGameObject();
			base.StartCoroutine(this.LoadScene());
			this.refreshChooseBtns();
		}

		private void refreshChooseBtns()
		{
			Variant chas = muNetCleint.instance.charsInfoInst.getChas();
			for (int i = 0; i < this.chooseBtns.Length; i++)
			{
				Text component = this.chooseBtns[i].transform.FindChild("Text").GetComponent<Text>();
				Text component2 = this.chooseBtns[i].transform.FindChild("infoTxt").GetComponent<Text>();
				GameObject gameObject = this.chooseBtns[i].transform.FindChild("ig_bg").gameObject;
				bool flag = i < chas.Count;
				if (flag)
				{
					component.text = chas[i]["name"];
					component2.gameObject.SetActive(true);
					gameObject.SetActive(true);
					string path = "";
					switch (chas[i]["carr"])
					{
					case 2:
						path = "icon/job_icon/selectcha_zs_1";
						break;
					case 3:
						path = "icon/job_icon/selectcha_fs_1";
						break;
					case 4:
						path = "icon/job_icon/h4";
						break;
					case 5:
						path = "icon/job_icon/selectcha_ck_1";
						break;
					}
					gameObject.transform.FindChild("ig_icon").GetComponent<Image>().sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
					gameObject.transform.FindChild("ig_icon_null").GetComponent<Image>().sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
					string text = string.Empty;
					bool flag2 = chas[i]["zhua"] != null;
					if (flag2)
					{
						text = chas[i]["zhua"]._int.ToString();
					}
					component2.text = string.Concat(new object[]
					{
						text,
						" 转",
						chas[i]["lvl"]._int,
						"级"
					});
					this.chooseBtns[i].onClick = new Action<GameObject>(this.onChooseCha);
					this.chooseBtns[i].transform.FindChild("chuangjian").gameObject.SetActive(false);
					this.chooseBtns[i].transform.FindChild("Text").gameObject.SetActive(true);
					this.chooseBtns[i].transform.FindChild("infoTxt").gameObject.SetActive(true);
					gameObject.transform.FindChild("ig_icon").gameObject.SetActive(false);
				}
				else
				{
					component.gameObject.SetActive(false);
					component2.gameObject.SetActive(false);
					gameObject.SetActive(false);
					this.chooseBtns[i].transform.FindChild("chuangjian").gameObject.SetActive(true);
					this.chooseBtns[i].transform.FindChild("Text").gameObject.SetActive(false);
					this.chooseBtns[i].transform.FindChild("infoTxt").gameObject.SetActive(false);
					this.chooseBtns[i].onClick = new Action<GameObject>(this.onCreateCha);
				}
			}
		}

		private void refreshAvatar()
		{
			bool flag = this.standPoint == null;
			if (!flag)
			{
				foreach (GameObject current in this.avatars.Values)
				{
					current.SetActive(false);
				}
				foreach (GameObject current2 in this.avatars_mon.Values)
				{
					bool flag2 = current2 != null;
					if (flag2)
					{
						current2.SetActive(false);
					}
				}
				GameObject gameObject = null;
				GameObject value = null;
				this.avatars.TryGetValue(this.chaCid, out gameObject);
				this.avatars_mon.TryGetValue(this.chaCid, out value);
				bool flag3 = gameObject == null;
				if (flag3)
				{
					gameObject = this.createAvatar(this.chaSelectedIndex);
					value = this.createAvata_mon(this.chaSelectedIndex);
					bool flag4 = gameObject == null;
					if (flag4)
					{
						return;
					}
					this.avatars[this.chaCid] = gameObject;
					this.avatars_mon[this.chaCid] = value;
				}
				this.chaAvatar = gameObject;
				this.chaAvatar_mon = value;
				bool flag5 = this.chaAvatar_mon != null;
				if (flag5)
				{
					this.chaAvatar_mon.SetActive(true);
				}
				this.chaAvatar.SetActive(true);
			}
		}

		private void onChooseCha(GameObject go)
		{
			for (int i = 0; i < 4; i++)
			{
				bool flag = go == this.chooseBtns[i].gameObject;
				if (flag)
				{
					this.chaSelectedIndex = i;
					this.chooseBtns[i].transform.FindChild("Image").gameObject.SetActive(true);
					this.chooseBtns[i].transform.FindChild("ig_bg/ig_icon").gameObject.SetActive(true);
					this.chooseBtns[i].transform.FindChild("ig_bg/ig_icon_null").gameObject.SetActive(false);
				}
				else
				{
					this.chooseBtns[i].transform.FindChild("Image").gameObject.SetActive(false);
					this.chooseBtns[i].transform.FindChild("ig_bg/ig_icon").gameObject.SetActive(false);
					this.chooseBtns[i].transform.FindChild("ig_bg/ig_icon_null").gameObject.SetActive(true);
				}
			}
			Variant chas = muNetCleint.instance.charsInfoInst.getChas();
			this.chaCid = chas[this.chaSelectedIndex]["cid"];
			switch (chas[this.chaSelectedIndex]["carr"])
			{
			case 2:
				MediaClient.instance.StopSoundUrls(null);
				MediaClient.instance.PlaySoundUrl("audio/common/zhanshi", false, null);
				break;
			case 3:
				MediaClient.instance.StopSoundUrls(null);
				MediaClient.instance.PlaySoundUrl("audio/common/fashi", false, null);
				break;
			case 5:
				MediaClient.instance.StopSoundUrls(null);
				MediaClient.instance.PlaySoundUrl("audio/common/cike", false, null);
				break;
			}
			this.refreshAvatar();
		}

		private GameObject createMagaAvatar(ChaOutInfo info)
		{
			GameObject original = Resources.Load<GameObject>("profession/avatar_ui/mage_avatar");
			GameObject gameObject = UnityEngine.Object.Instantiate(original, this.standPoint.transform.position, Quaternion.identity) as GameObject;
			Transform transform = gameObject.transform.FindChild("model");
			ProfessionAvatar professionAvatar = new ProfessionAvatar();
			professionAvatar.Init("profession/mage/", "h_", EnumLayer.LM_FX, EnumMaterial.EMT_EQUIP_H, transform.transform, "Fx/armourFX/mage/");
			professionAvatar.set_body(info.bodyID, info.bodyInte);
			professionAvatar.set_weaponl(info.weaponLID, info.weaponLInte);
			professionAvatar.set_weaponr(info.weaponRID, info.weaponRInte);
			professionAvatar.set_wing(info.wingID, info.wingInte);
			professionAvatar.set_equip_color((uint)info.colorID);
			bool addeff = info.addeff;
			if (addeff)
			{
				professionAvatar.set_equip_eff(info.bodyID, true);
			}
			this.m_curProAvatar = professionAvatar;
			Transform parent = gameObject.transform.FindChild("model/R_Finger1");
			original = Resources.Load<GameObject>("profession/avatar_ui/mage_r_finger_fire");
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject2.transform.SetParent(parent, false);
			return gameObject;
		}

		private GameObject createWarriorAvatar(ChaOutInfo info)
		{
			GameObject original = Resources.Load<GameObject>("profession/avatar_ui/warrior_avatar");
			GameObject gameObject = UnityEngine.Object.Instantiate(original, this.standPoint.transform.position, Quaternion.identity) as GameObject;
			Transform cur_model = gameObject.transform.FindChild("model");
			ProfessionAvatar professionAvatar = new ProfessionAvatar();
			professionAvatar.Init("profession/warrior/", "h_", EnumLayer.LM_FX, EnumMaterial.EMT_EQUIP_H, cur_model, "Fx/armourFX/warrior/");
			professionAvatar.set_body(info.bodyID, info.bodyInte);
			professionAvatar.set_weaponl(info.weaponLID, info.weaponLInte);
			professionAvatar.set_weaponr(info.weaponRID, info.weaponRInte);
			professionAvatar.set_wing(info.wingID, info.wingInte);
			professionAvatar.set_equip_color((uint)info.colorID);
			bool addeff = info.addeff;
			if (addeff)
			{
				professionAvatar.set_equip_eff(info.bodyID, true);
			}
			this.m_curProAvatar = professionAvatar;
			return gameObject;
		}

		private GameObject createArcherAvatar(ChaOutInfo info)
		{
			return null;
		}

		private GameObject createAssassinAvatar(ChaOutInfo info)
		{
			GameObject original = Resources.Load<GameObject>("profession/avatar_ui/assa_avatar");
			GameObject gameObject = UnityEngine.Object.Instantiate(original, this.standPoint.transform.position, Quaternion.identity) as GameObject;
			Transform cur_model = gameObject.transform.FindChild("model");
			ProfessionAvatar professionAvatar = new ProfessionAvatar();
			professionAvatar.Init("profession/assa/", "h_", EnumLayer.LM_FX, EnumMaterial.EMT_EQUIP_H, cur_model, "Fx/armourFX/assa/");
			professionAvatar.set_body(info.bodyID, info.bodyInte);
			professionAvatar.set_weaponl(info.weaponLID, info.weaponLInte);
			professionAvatar.set_weaponr(info.weaponRID, info.weaponRInte);
			professionAvatar.set_wing(info.wingID, info.wingInte);
			professionAvatar.set_equip_color((uint)info.colorID);
			bool addeff = info.addeff;
			if (addeff)
			{
				professionAvatar.set_equip_eff(info.bodyID, true);
			}
			this.m_curProAvatar = professionAvatar;
			return gameObject;
		}

		private GameObject createAssassinAvatar_mon(int tpid)
		{
			SXML sXML = XMLMgr.instance.GetSXML("callbeast", "");
			SXML node = sXML.GetNode("callbeast", "id==" + tpid);
			int @int = node.getInt("mid");
			SXML sXML2 = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + @int);
			int int2 = sXML2.getInt("obj");
			GameObject original = Resources.Load<GameObject>("monster/" + int2);
			GameObject gameObject = UnityEngine.Object.Instantiate(original, new Vector3(this.standPoint.transform.position.x - 1.4f, this.standPoint.transform.position.y, this.standPoint.transform.position.z - 1f), Quaternion.identity) as GameObject;
			Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = EnumLayer.LM_FX;
			}
			Transform transform2 = gameObject.transform.FindChild("model");
			Animator component = transform2.GetComponent<Animator>();
			component.cullingMode = AnimatorCullingMode.AlwaysAnimate;
			transform2.gameObject.AddComponent<Summon_Base_Event>();
			transform2.Rotate(Vector3.up, (float)(90 - sXML2.getInt("smshow_face")));
			transform2.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			return gameObject;
		}

		private GameObject createAvatar(int chaIdx)
		{
			bool flag = chaIdx < 0;
			GameObject result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant chas = muNetCleint.instance.charsInfoInst.getChas();
				debug.Log("III" + chas.dump());
				Variant variant = chas[chaIdx];
				ChaOutInfo info = default(ChaOutInfo);
				this.GetChaEqpInfo(variant, 3, out info.bodyID, out info.bodyInte);
				this.GetChaWingInfo(variant, out info.wingID, out info.wingInte);
				this.GetColorInfo(variant, out info.colorID);
				this.GetEqpEff(variant, out info.addeff);
				int num = variant["carr"];
				GameObject gameObject = null;
				switch (num)
				{
				case 2:
					this.GetChaEqpInfo(variant, 6, out info.weaponRID, out info.weaponRInte);
					gameObject = this.createWarriorAvatar(info);
					break;
				case 3:
					this.GetChaEqpInfo(variant, 6, out info.weaponLID, out info.weaponLInte);
					gameObject = this.createMagaAvatar(info);
					break;
				case 4:
					gameObject = this.createArcherAvatar(info);
					break;
				case 5:
					this.GetChaEqpInfo(variant, 6, out info.weaponRID, out info.weaponRInte);
					this.GetChaEqpInfo(variant, 6, out info.weaponLID, out info.weaponLInte);
					gameObject = this.createAssassinAvatar(info);
					break;
				}
				result = gameObject;
			}
			return result;
		}

		private GameObject createAvata_mon(int chaIdx)
		{
			bool flag = chaIdx < 0;
			GameObject result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant chas = muNetCleint.instance.charsInfoInst.getChas();
				Variant variant = chas[chaIdx];
				bool flag2 = !variant.ContainsKey("smon");
				if (flag2)
				{
					result = null;
				}
				else
				{
					int num = variant["smon"];
					bool flag3 = num <= 0;
					if (flag3)
					{
						result = null;
					}
					else
					{
						GameObject gameObject = this.createAssassinAvatar_mon(num);
						result = gameObject;
					}
				}
			}
			return result;
		}

		private void GetChaEqpInfo(Variant chaInfo, int tp, out int id, out int intens)
		{
			id = 0;
			intens = 0;
			bool flag = chaInfo == null;
			if (!flag)
			{
				Variant variant = chaInfo["eqp"];
				bool flag2 = variant == null;
				if (!flag2)
				{
					bool flag3 = this.itemXML == null;
					if (flag3)
					{
						this.itemXML = XMLMgr.instance.GetSXML("item", "");
					}
					foreach (Variant current in variant._arr)
					{
						int num = current["tpid"];
						int num2 = current["stag"];
						int @int = this.itemXML.GetNode("item", "id==" + num).getInt("equip_type");
						bool flag4 = @int == tp;
						if (flag4)
						{
							id = num;
							intens = num2;
							break;
						}
					}
				}
			}
		}

		private void GetChaWingInfo(Variant chaInfo, out int id, out int intens)
		{
			id = 0;
			intens = 0;
			bool flag = chaInfo == null;
			if (!flag)
			{
				bool flag2 = !chaInfo.ContainsKey("wing");
				if (!flag2)
				{
					Variant variant = chaInfo["wing"];
					id = variant["show"];
				}
			}
		}

		private void GetColorInfo(Variant chaInfo, out int colorID)
		{
			ModelBase<a3_EquipModel>.getInstance();
			colorID = 0;
			bool flag = chaInfo == null;
			if (!flag)
			{
				Variant variant = chaInfo["eqp"];
				bool flag2 = variant == null;
				if (!flag2)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag3 = current.ContainsKey("colo");
						if (flag3)
						{
							colorID = current["colo"];
							break;
						}
					}
				}
			}
		}

		private void GetEqpEff(Variant chaInfo, out bool addEff)
		{
			addEff = false;
			bool flag = chaInfo == null;
			if (!flag)
			{
				bool flag2 = !chaInfo.ContainsKey("acti");
				if (!flag2)
				{
					bool flag3 = chaInfo["acti"] >= 10;
					if (flag3)
					{
						addEff = true;
					}
				}
			}
		}

		private void onCreateCha(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.FLYTXT, null, false);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SELECTCHA);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_CREATECHA, null, false);
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.chaAvatar != null;
			if (flag)
			{
				this.chaAvatar.transform.Rotate(Vector3.up, -delta.x);
			}
		}

		private void onReturnClick(GameObject go)
		{
		}

		private void onDeleteChar(GameEvent e)
		{
			this.refreshChooseBtns();
			this.chaSelectedIndex = -1;
			this.chaCid = 0u;
			this.chaAvatar = null;
			this.chaAvatar_mon = null;
			foreach (GameObject current in this.avatars.Values)
			{
				current.SetActive(false);
			}
			foreach (GameObject current2 in this.avatars_mon.Values)
			{
				bool flag = current2 != null;
				if (flag)
				{
					current2.SetActive(false);
				}
			}
			Variant chas = muNetCleint.instance.charsInfoInst.getChas();
			bool flag2 = chas.Count > 0;
			if (flag2)
			{
				this.onChooseCha(this.chooseBtns[0].gameObject);
			}
		}

		private void onDeleteChaClick(GameObject go)
		{
			bool flag = this.chaCid > 0u;
			if (flag)
			{
				Variant chas = muNetCleint.instance.charsInfoInst.getChas();
				confirmtext.showDeleChar(chas[this.chaSelectedIndex]);
			}
		}

		private void onEnterGameClick(GameObject go)
		{
			MediaClient.instance.StopSoundUrls(null);
			Variant chas = muNetCleint.instance.charsInfoInst.getChas();
			bool flag = this.chaSelectedIndex < 0 || this.chaSelectedIndex >= chas.Count;
			if (!flag)
			{
				uint @uint = chas[this.chaSelectedIndex]["cid"]._uint;
				UIClient.instance.dispatchEvent(GameEvent.Create(4033u, this, GameTools.createGroup(new Variant[]
				{
					"cid",
					@uint
				}), false));
				UIClient.instance.dispatchEvent(GameEvent.Create(4034u, this, GameTools.createGroup(new Variant[]
				{
					"cid",
					@uint
				}), false));
				GameObject obj = GameObject.Find("Audio");
				UnityEngine.Object.Destroy(obj);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SELECTCHA);
				InterfaceMgr.getInstance().destory(InterfaceMgr.A3_SELECTCHA);
			}
		}

		private void disposeAvatar()
		{
			foreach (GameObject current in this.avatars.Values)
			{
				UnityEngine.Object.Destroy(current);
			}
			foreach (GameObject current2 in this.avatars_mon.Values)
			{
				bool flag = current2 != null;
				if (flag)
				{
					current2.SetActive(false);
				}
			}
			this.avatars.Clear();
			this.avatars_mon.Clear();
			this.m_curProAvatar = null;
		}

		public override void onClosed()
		{
			this.async = null;
			this.standPoint = null;
			this.itemXML = null;
			this.disposeAvatar();
		}
	}
}
