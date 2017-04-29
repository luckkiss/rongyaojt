using Cross;
using GameFramework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_createcha : FloatUi
	{
		private TabControl chooseTab;

		private InputField nameInput;

		private Text descText;

		private Text _txtKeyWordWaring;

		private Slider[] attSliders;

		private string[] lFirstName;

		private string[] lLastName0;

		private string[] lLastName1;

		private GameObject fg;

		private AsyncOperation async;

		private GameObject cameras = null;

		private SXML createChaSxml = null;

		private GameObject att;

		protected Variant firstname = null;

		protected Variant lastname = null;

		public override void init()
		{
			bool flag = this.createChaSxml == null;
			if (flag)
			{
				this.createChaSxml = XMLMgr.instance.GetSXML("creat_character", "");
			}
			this.fg = base.getGameObjectByPath("fg");
			this.fg.SetActive(true);
			this.fg.GetComponent<RectTransform>().sizeDelta = new Vector2(Baselayer.uiWidth, Baselayer.uiHeight);
			this.InitUI();
		}

		private IEnumerator LoadScene()
		{
			Debug.Log("加载创角场景");
			this.async = SceneManager.LoadSceneAsync("cr_scene");
			yield return this.async;
			this.fg.SetActive(false);
			yield break;
		}

		public override void onShowed()
		{
			GRMap.DontDestroyBaseGameObject();
			base.StartCoroutine(this.LoadScene());
			muNetCleint.instance.charsInfoInst.addEventListener(4031u, new Action<GameEvent>(this.OnCreateChar));
		}

		public override void onClosed()
		{
			this.cameras = null;
			this.async = null;
			muNetCleint.instance.charsInfoInst.removeEventListener(4031u, new Action<GameEvent>(this.OnCreateChar));
		}

		private void Update()
		{
			bool flag = this.async != null && this.async.isDone;
			if (flag)
			{
				this.async = null;
				this.cameras = GameObject.Find("cameras");
				this.RefreshAfterSelect();
			}
		}

		private void OnDestroy()
		{
		}

		private void InitUI()
		{
			this.att = base.transform.FindChild("att_pic").gameObject;
			this.nameInput = base.getComponentByPath<InputField>("nameInput");
			this.descText = base.getComponentByPath<Text>("descText");
			this._txtKeyWordWaring = base.getComponentByPath<Text>("txtKeyWordWaring");
			this._txtKeyWordWaring.gameObject.SetActive(false);
			base.getEventTrigerByPath("returnBtn").onClick = new EventTriggerListener.VoidDelegate(this.OnReturnClick);
			base.getEventTrigerByPath("createBtn").onClick = new EventTriggerListener.VoidDelegate(this.OnCreateClick);
			base.getEventTrigerByPath("randomBtn").onClick = new EventTriggerListener.VoidDelegate(this.OnNameRandomClick);
			this.chooseTab = new TabControl();
			this.chooseTab.onClickHanle = new Action<TabControl>(this.onChooseCha);
			this.chooseTab.create(base.getGameObjectByPath("btnChoose"), base.gameObject, 0, 0, false);
		}

		private void onChooseCha(TabControl t)
		{
			this.RefreshAfterSelect();
		}

		private void OnReturnClick(GameObject go)
		{
			MediaClient.instance.clearMusic();
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_CREATECHA);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_SELECTCHA, null, false);
		}

		private void OnCreateChar(GameEvent e)
		{
			Variant data = e.data;
			Debug.Log(data["res"]._int);
			Debug.Log(data.dump());
			bool flag = data["res"]._int == -153;
			if (flag)
			{
				flytxt.instance.fly("已存在同名角色", 0, default(Color), null);
			}
			bool flag2 = data["res"]._int > 0;
			if (flag2)
			{
				uint @uint = data["cha"]["cid"]._uint;
				ModelBase<PlayerModel>.getInstance().cid = @uint;
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_CREATECHA);
				InterfaceMgr.getInstance().destory(InterfaceMgr.A3_CREATECHA);
				ModelBase<PlayerModel>.getInstance().profession = this.chooseTab.getSeletedIndex() + 2;
				ModelBase<PlayerModel>.getInstance().name = this.nameInput.text;
				GameSdkMgr.record_createRole(data["cha"]);
				SelfRole.s_bStandaloneScene = true;
				InterfaceMgr.getInstance().open(InterfaceMgr.MAP_LOADING, null, false);
				InterfaceMgr.getInstance().open("joystick", null, false);
				InterfaceMgr.getInstance().open("skillbar", null, false);
				LGMap lGMap = GRClient.instance.g_gameM.getObject("LG_MAP") as LGMap;
				lGMap.EnterStandalone();
				MediaClient.instance.StopMusic();
				MediaClient.instance.clearMusic();
			}
		}

		private void OnCreateClick(GameObject go)
		{
			bool flag = KeyWord.isContain(this.nameInput.text);
			this._txtKeyWordWaring.gameObject.SetActive(flag);
			bool flag2 = flag;
			if (!flag2)
			{
				int carr = this.chooseTab.getSeletedIndex() + 2;
				muNetCleint.instance.outGameMsgsInst.createCha(this.nameInput.text, (uint)carr, 0u);
			}
		}

		private void OnNameRandomClick(GameObject go)
		{
			this.nameInput.text = this.RandomName();
		}

		private string RandomName()
		{
			bool flag = this.lFirstName == null;
			if (flag)
			{
				this.lFirstName = XMLMgr.instance.GetSXML("comm.ranName.firstName", "").getString("value").Split(new char[]
				{
					','
				});
				this.lLastName0 = XMLMgr.instance.GetSXML("comm.ranName.lastName", "sex==0").getString("value").Split(new char[]
				{
					','
				});
				this.lLastName1 = XMLMgr.instance.GetSXML("comm.ranName.lastName", "sex==1").getString("value").Split(new char[]
				{
					','
				});
			}
			System.Random random = new System.Random();
			int num = random.Next(0, this.lFirstName.Length);
			int num2 = random.Next(0, this.lLastName0.Length);
			return this.lFirstName[num] + this.lFirstName[num2];
		}

		private void ShowDescAndAtt()
		{
			int seletedIndex = this.chooseTab.getSeletedIndex();
			for (int i = 0; i < this.att.transform.childCount; i++)
			{
				this.att.transform.GetChild(i).gameObject.SetActive(false);
			}
			this.att.transform.FindChild("cre" + seletedIndex).gameObject.SetActive(true);
			SXML node = this.createChaSxml.GetNode("character", "job_type==" + (seletedIndex + 2));
			this.descText.text = node.getString("desc");
		}

		private void RefreshAfterSelect()
		{
			this.ShowCameraAnim();
			this.ShowDescAndAtt();
			this.OnNameRandomClick(null);
		}

		private void ShowBtenIamg(int a)
		{
			for (int i = 0; i < 4; i++)
			{
				bool flag = i == a;
				if (flag)
				{
					base.transform.FindChild("btnChoose").GetChild(i).FindChild("isthis").gameObject.SetActive(true);
					base.transform.FindChild("btnChoose").GetChild(i).FindChild("isnull").gameObject.SetActive(false);
				}
				else
				{
					base.transform.FindChild("btnChoose").GetChild(i).FindChild("isthis").gameObject.SetActive(false);
					base.transform.FindChild("btnChoose").GetChild(i).FindChild("isnull").gameObject.SetActive(true);
				}
			}
		}

		private void ShowCameraAnim()
		{
			bool flag = this.cameras == null;
			if (!flag)
			{
				int seletedIndex = this.chooseTab.getSeletedIndex();
				Transform transform = this.cameras.transform.FindChild("assa_Camera");
				Transform transform2 = this.cameras.transform.FindChild("mage_Camera");
				Transform transform3 = this.cameras.transform.FindChild("warrior_Camera");
				bool flag2 = transform == null || transform2 == null || transform3 == null;
				if (flag2)
				{
					Debug.Log("some camera is missing in create role scene");
				}
				else
				{
					transform.gameObject.SetActive(false);
					transform2.gameObject.SetActive(false);
					transform3.gameObject.SetActive(false);
					switch (seletedIndex)
					{
					case 0:
						transform3.gameObject.SetActive(true);
						this.ShowBtenIamg(0);
						break;
					case 1:
						transform2.gameObject.SetActive(true);
						this.ShowBtenIamg(1);
						break;
					case 3:
						transform.gameObject.SetActive(true);
						this.ShowBtenIamg(3);
						break;
					}
				}
			}
		}
	}
}
