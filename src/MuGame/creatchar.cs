using Cross;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class creatchar : FloatUi
	{
		private InputField input;

		private Button btrename;

		private Button btcreate;

		private GameObject m_theScene;

		protected GRCharacter3D m_skmesh_cc_male;

		protected GRCharacter3D m_skmesh_cc_female;

		private bool m_bMale_BodyHair_Loaded = false;

		private bool m_bMale_Weapon_Loaded = false;

		private bool m_bFemale_BodyHair_Loaded = false;

		private bool m_bFemale_Weapon_Loaded = false;

		protected GRCharacter3D curSK;

		protected TabControl tab;

		private uint sex;

		protected Variant firstname;

		protected Variant lastname;

		public override void init()
		{
			this.initMesh();
			this.initUI();
			this.initListener();
		}

		private void Update()
		{
			bool uNCLOSE_BEGINLOADING = Globle.UNCLOSE_BEGINLOADING;
			if (uNCLOSE_BEGINLOADING)
			{
				bool ms_HasAllLoaded = LoaderBehavior.ms_HasAllLoaded;
				if (ms_HasAllLoaded)
				{
					Globle.UNCLOSE_BEGINLOADING = false;
					bool flag = login.instance;
					if (!flag)
					{
						InterfaceMgr.getInstance().destory(InterfaceMgr.BEGIN_LOADING);
					}
					Debug.Log("关闭开始的加载界面");
				}
			}
			bool flag2 = !this.m_bMale_BodyHair_Loaded && this.m_skmesh_cc_male != null;
			if (flag2)
			{
				SkinnedMeshRenderer[] componentsInChildren = this.m_skmesh_cc_male.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					bool flag3 = "body" == componentsInChildren[i].name;
					if (flag3)
					{
						componentsInChildren[i].gameObject.GetComponent<Renderer>().material.SetColor(gameST.HIT_Rim_Color_nameID, new Color(0.9372549f, 0.709803939f, 0.360784322f));
						componentsInChildren[i].gameObject.GetComponent<Renderer>().material.SetFloat(gameST.HIT_Rim_Width_nameID, 0.68f);
					}
					bool flag4 = "hair" == componentsInChildren[i].name;
					if (flag4)
					{
						componentsInChildren[i].gameObject.GetComponent<Renderer>().material.SetColor(gameST.HIT_Rim_Color_nameID, new Color(0.407843143f, 0.454901963f, 0.7294118f));
						componentsInChildren[i].gameObject.GetComponent<Renderer>().material.SetFloat(gameST.HIT_Rim_Width_nameID, 0.68f);
					}
					this.m_bMale_BodyHair_Loaded = true;
				}
			}
			bool flag5 = !this.m_bMale_Weapon_Loaded;
			if (flag5)
			{
				GameObject gameObject = GameObject.Find("3DScene/Container3D/SkMesh/player(Clone)/RightHand/Container3D/SkMesh/1000(Clone)/Wp_male_jian");
				bool flag6 = gameObject != null;
				if (flag6)
				{
					debug.Log("更新男的武器上的光感");
					gameObject.name = "light_male_wp";
					gameObject.GetComponent<Renderer>().material.SetColor(gameST.HIT_Rim_Color_nameID, new Color(0.945098042f, 1f, 0f));
					gameObject.GetComponent<Renderer>().material.SetFloat(gameST.HIT_Rim_Width_nameID, 0.31f);
					this.m_bMale_Weapon_Loaded = true;
				}
			}
			bool flag7 = !this.m_bFemale_BodyHair_Loaded && this.m_skmesh_cc_female != null;
			if (flag7)
			{
				SkinnedMeshRenderer[] componentsInChildren2 = this.m_skmesh_cc_female.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					bool flag8 = "body" == componentsInChildren2[j].name;
					if (flag8)
					{
						componentsInChildren2[j].gameObject.GetComponent<Renderer>().material.SetColor(gameST.HIT_Rim_Color_nameID, new Color(0.9137255f, 0.68235296f, 0.3372549f));
						componentsInChildren2[j].gameObject.GetComponent<Renderer>().material.SetFloat(gameST.HIT_Rim_Width_nameID, 0.37f);
					}
					bool flag9 = "hair" == componentsInChildren2[j].name;
					if (flag9)
					{
						componentsInChildren2[j].gameObject.GetComponent<Renderer>().material.SetColor(gameST.HIT_Rim_Color_nameID, new Color(0.8901961f, 0.6784314f, 0.31764707f));
						componentsInChildren2[j].gameObject.GetComponent<Renderer>().material.SetFloat(gameST.HIT_Rim_Width_nameID, 0.43f);
					}
					this.m_bFemale_BodyHair_Loaded = true;
				}
			}
			bool flag10 = !this.m_bFemale_Weapon_Loaded;
			if (flag10)
			{
				GameObject gameObject2 = GameObject.Find("3DScene/Container3D/SkMesh/player(Clone)/RightHand/Container3D/SkMesh/1000(Clone)/Wp_male_jian");
				bool flag11 = gameObject2 != null;
				if (flag11)
				{
					gameObject2.name = "light_female_wp";
					debug.Log("更新女的武器上的光感");
					gameObject2.GetComponent<Renderer>().material.SetColor(gameST.HIT_Rim_Color_nameID, new Color(0.945098042f, 1f, 0f));
					gameObject2.GetComponent<Renderer>().material.SetFloat(gameST.HIT_Rim_Width_nameID, 0.31f);
					this.m_bFemale_Weapon_Loaded = true;
				}
			}
			bool flag12 = this.curSK != null;
			if (flag12)
			{
				this.curSK.anim_Speed(0.5f);
			}
		}

		private void initUI()
		{
			this.input = base.getComponentByPath<InputField>("InputField");
			this.btrename = base.getComponentByPath<Button>("btrename");
			this.btcreate = base.getComponentByPath<Button>("btcreate");
			this.input.text = this.randomname();
			base.getEventTrigerByPath("btrename").onClick = new EventTriggerListener.VoidDelegate(this.onRenameClick);
			base.getEventTrigerByPath("btcreate").onClick = new EventTriggerListener.VoidDelegate(this.onCreateClick);
			base.getEventTrigerByPath("mousebg").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			MediaClient.instance.Play("media/mapmusic/creat_player", true, null);
			this.tab = new TabControl();
			this.tab.onClickHanle = new Action<TabControl>(this.onTab);
			this.tab.create(base.getGameObjectByPath("btchoose"), base.gameObject, 0, 0, false);
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			this.curSK.rotY -= delta.x;
		}

		private void onTab(TabControl t)
		{
			bool flag = t.getSeletedIndex() == 0;
			if (flag)
			{
				this.curSK = this.m_skmesh_cc_male;
				this.m_skmesh_cc_male.pos = new Vec3(0.1f, -100f, 1.5f);
				this.m_skmesh_cc_female.pos = new Vec3(0.1f, -100f, 1.5f);
				this.sex = 0u;
			}
			else
			{
				this.curSK = this.m_skmesh_cc_female;
				this.m_skmesh_cc_male.pos = new Vec3(0.1f, -100f, 1.5f);
				this.m_skmesh_cc_female.pos = new Vec3(0.1f, -100f, 1.5f);
				this.sex = 1u;
			}
			bool flag2 = this.sex == 0u;
			if (flag2)
			{
				this.curSK.pos = new Vec3(0.1f, -0.15f, 1.5f);
			}
			else
			{
				this.curSK.pos = new Vec3(0.1f, 0f, 1.5f);
			}
			this.curSK.playAnimation("idle", 0);
		}

		protected void initMesh()
		{
			GameObject gameObject = U3DAPI.U3DResLoad<GameObject>("creatchar/cc_scene");
			bool flag = gameObject != null;
			if (flag)
			{
				this.m_theScene = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			}
			this.m_skmesh_cc_male = (GRClient.instance.world.createEntity(Define.GREntityType.CHARACTER) as GRCharacter3D);
			this.m_skmesh_cc_male.load(GraphManager.singleton.getCharacterConf("0"), null, null);
			this.m_skmesh_cc_male.applyAvatar(GraphManager.singleton.getAvatarConf("0", "10000"), null);
			this.m_skmesh_cc_male.pos = new Vec3(0.1f, -100f, 1.5f);
			this.m_skmesh_cc_male.rotY = 317f;
			this.m_skmesh_cc_female = (GRClient.instance.world.createEntity(Define.GREntityType.CHARACTER) as GRCharacter3D);
			this.m_skmesh_cc_female.load(GraphManager.singleton.getCharacterConf("1"), null, null);
			this.m_skmesh_cc_female.applyAvatar(GraphManager.singleton.getAvatarConf("1", "10001"), null);
			this.m_skmesh_cc_female.pos = new Vec3(0.1f, -100f, 1.5f);
			this.m_skmesh_cc_female.rotY = 297f;
			GRClient.instance.getGraphCamera().visible = false;
			this.m_bMale_BodyHair_Loaded = false;
			this.m_bMale_Weapon_Loaded = false;
			this.m_bFemale_BodyHair_Loaded = false;
			this.m_bFemale_Weapon_Loaded = false;
		}

		private void initListener()
		{
			muNetCleint.instance.charsInfoInst.addEventListener(4031u, new Action<GameEvent>(this.onCreateChar));
			this.input.onValueChange.AddListener(new UnityAction<string>(this.onEditOver));
		}

		private void onEditOver(string str)
		{
			str = StringUtils.FilterSpecial(str);
			this.input.text = KeyWord.filter(str);
		}

		private void onCreateChar(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"]._int > 0;
			if (flag)
			{
				uint @uint = data["cha"]["cid"]._uint;
				ModelBase<PlayerModel>.getInstance().cid = @uint;
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
				InterfaceMgr.getInstance().destory(InterfaceMgr.CREATE_CHAR);
			}
		}

		public override void dispose()
		{
			MediaClient.instance.StopSoundUrls(null);
			bool flag = this.m_theScene != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_theScene);
				this.m_theScene = null;
			}
			this.m_skmesh_cc_male.dispose();
			this.m_skmesh_cc_male = null;
			this.m_skmesh_cc_female.dispose();
			this.m_skmesh_cc_female = null;
			this.curSK = null;
			this.tab.dispose();
			this.tab = null;
			base.getEventTrigerByPath("mousebg").clearAllListener();
			base.getEventTrigerByPath("btrename").clearAllListener();
			base.getEventTrigerByPath("btcreate").clearAllListener();
			this.input.onValueChange.RemoveAllListeners();
			base.dispose();
		}

		private void onRenameClick(GameObject obj)
		{
			this.input.text = this.randomname();
		}

		private void onCreateClick(GameObject obj)
		{
			muNetCleint.instance.outGameMsgsInst.createCha(this.input.text, 1u, this.sex);
		}

		private string randomname()
		{
			System.Random random = new System.Random();
			this.firstname = muCLientConfig.instance.localOutGame.getFirstNameArr();
			this.lastname = muCLientConfig.instance.localOutGame.getLastNameArr(-1, 0);
			int idx = random.Next(0, this.firstname.Count);
			int idx2 = random.Next(0, this.lastname.Count);
			return this.firstname[idx]._str + this.lastname[idx2];
		}
	}
}
