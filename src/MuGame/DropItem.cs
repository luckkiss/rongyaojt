using GameFramework;
using MuGame.Qsmy.model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class DropItem : RoomObj, INameObj
	{
		public bool canPick = true;

		public static List<Color> lColor = new List<Color>
		{
			new Color(1f, 0.84f, 0f, 0.2f),
			new Color(1f, 1f, 1f, 0.1f),
			new Color(0f, 1f, 0f, 0.2f),
			new Color(0f, 0f, 1f, 0.2f),
			new Color(1f, 0f, 1f, 0.2f),
			new Color(1f, 0.94f, 0.23f, 0.2f)
		};

		public static List<string> leqType = new List<string>
		{
			"",
			"Item/diaoluo_helmet",
			"Item/diaoluo_shoulder",
			"Item/diaoluo_armour",
			"Item/diaoluo_trousers",
			"Item/diaoluo_shoe",
			"Item/box"
		};

		private int _curhp = 100;

		private int _maxHp = 100;

		private int _title_id = 0;

		private bool _isactive = true;

		private int _rednm = 0;

		public uint _hidbacktime = 0u;

		public Vector3 lastHeadPos = Vector3.zero;

		public static GameObject tempEffect;

		public static GameObject tempGolden;

		public Vector3 headOffset = Vector3.zero;

		public bool isFake = false;

		public static Transform dropItemCon;

		public DropItemdta itemdta;

		public int lastGetTimer = 0;

		public static long cantGetTimer = 0L;

		public static List<Vector3> weaponDorpOffset = new List<Vector3>
		{
			Vector3.zero,
			Vector3.zero,
			Vector3.zero,
			Vector3.zero,
			Vector3.zero,
			new Vector3(0f, 0f, -0.4f)
		};

		public static List<Vector3> weaponDorpScale = new List<Vector3>
		{
			Vector3.zero,
			Vector3.zero,
			new Vector3(1f, 1f, 1f),
			new Vector3(0.7f, 0.7f, 0.7f),
			Vector3.zero,
			new Vector3(0.6f, 0.6f, 0.6f)
		};

		public static List<Vector3> weaponDorpRot = new List<Vector3>
		{
			Vector3.zero,
			Vector3.zero,
			new Vector3(0f, 0f, 90f),
			new Vector3(0f, 0f, 90f),
			Vector3.zero,
			new Vector3(90f, 90f, 0f)
		};

		public static List<string> weaponDorpPath = new List<string>
		{
			"",
			"",
			"profession/warrior/weaponr_l_",
			"profession/mage/weaponl_l_",
			"",
			"profession/assa/weaponl_l_"
		};

		public string roleName
		{
			get
			{
				return this.itemdta.getName();
			}
			set
			{
			}
		}

		public int curhp
		{
			get
			{
				return this._curhp;
			}
			set
			{
				this._curhp = value;
			}
		}

		public int maxHp
		{
			get
			{
				return this._maxHp;
			}
			set
			{
				this._maxHp = value;
			}
		}

		public int title_id
		{
			get
			{
				return this._title_id;
			}
			set
			{
				this._title_id = value;
			}
		}

		public bool isactive
		{
			get
			{
				return this.isactive;
			}
			set
			{
				this._isactive = value;
				bool flag = !this._isactive;
				if (flag)
				{
					this._title_id = 0;
				}
			}
		}

		public int rednm
		{
			get
			{
				return this._rednm;
			}
			set
			{
				this._rednm = value;
			}
		}

		public uint hidbacktime
		{
			get
			{
				return this._hidbacktime;
			}
			set
			{
				this._hidbacktime = value;
			}
		}

		public Vector3 getHeadPos()
		{
			bool flag = SceneCamera.m_curCamera == null;
			Vector3 result;
			if (flag)
			{
				result = Vector3.zero;
			}
			else
			{
				bool flag2 = this == null || base.gameObject == null || this.disposed;
				if (flag2)
				{
					this.dispose();
					result = Vector3.zero;
				}
				else
				{
					Vector3 vector = base.transform.position + this.headOffset;
					vector = SceneCamera.m_curCamera.WorldToScreenPoint(vector);
					vector *= SceneCamera.m_fGameScreenPow;
					vector.z = 0f;
					this.lastHeadPos = vector;
					result = vector;
				}
			}
			return result;
		}

		public static DropItem create(DropItemdta item)
		{
			GameObject gameObject = new GameObject();
			DropItem dropItem = gameObject.AddComponent<DropItem>();
			dropItem.itemdta = item;
			dropItem.init();
			return dropItem;
		}

		public override void init()
		{
			base.init();
			bool flag = DropItem.dropItemCon == null;
			if (flag)
			{
				GameObject gameObject = new GameObject();
				DropItem.dropItemCon = gameObject.transform;
				DropItem.dropItemCon.name = "DROP_CON";
			}
			base.transform.SetParent(DropItem.dropItemCon, false);
			this.initItem();
		}

		public void initItem()
		{
			Vector3 one = Vector3.one;
			bool flag = this.itemdta.tpid == 0;
			GameObject gameObject;
			if (flag)
			{
				bool flag2 = DropItem.tempGolden == null;
				if (flag2)
				{
					DropItem.tempGolden = (Resources.Load("Item/golenCoin") as GameObject);
				}
				gameObject = UnityEngine.Object.Instantiate<GameObject>(DropItem.tempGolden);
			}
			else
			{
				bool flag3 = this.itemdta.itemXml.getInt("item_type") == 2;
				if (flag3)
				{
					int @int = this.itemdta.itemXml.getInt("equip_type");
					bool flag4 = @int == 6;
					if (flag4)
					{
						int int2 = this.itemdta.itemXml.getInt("job_limit");
						gameObject = this.getWeapom(this.itemdta);
						bool flag5 = int2 == 3;
						if (flag5)
						{
							one = new Vector3(0.3f, 2f, 1f);
						}
						else
						{
							bool flag6 = int2 == 2;
							if (flag6)
							{
								one = new Vector3(0.3f, 2f, 1f);
							}
						}
					}
					else
					{
						bool flag7 = @int >= DropItem.leqType.Count;
						GameObject original;
						if (flag7)
						{
							original = (Resources.Load("Item/box") as GameObject);
						}
						else
						{
							original = (Resources.Load(DropItem.leqType[@int]) as GameObject);
						}
						gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					}
				}
				else
				{
					GameObject original2 = Resources.Load("Item/box") as GameObject;
					gameObject = UnityEngine.Object.Instantiate<GameObject>(original2);
				}
			}
			bool flag8 = gameObject == null;
			if (!flag8)
			{
				bool flag9 = this.itemdta.tpid != 0;
				if (flag9)
				{
					bool flag10 = DropItem.tempEffect == null;
					if (flag10)
					{
						DropItem.tempEffect = (Resources.Load("FX/comFX/fx_diaoluo_guang") as GameObject);
					}
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(DropItem.tempEffect);
					gameObject2.transform.localScale = one;
					gameObject2.transform.SetParent(base.transform, false);
					MeshRenderer component = gameObject2.transform.GetChild(0).GetComponent<MeshRenderer>();
					bool flag11 = this.itemdta.tpid == 0;
					if (flag11)
					{
						component.material.SetColor(EnumShader.SPI_TINT_COLOR, DropItem.lColor[0]);
					}
					else
					{
						component.material.SetColor(EnumShader.SPI_TINT_COLOR, DropItem.lColor[this.itemdta.itemXml.getInt("quality")]);
					}
				}
				gameObject.transform.SetParent(base.transform, false);
				gameObject.gameObject.layer = EnumLayer.LM_MAP_ITEM;
				this.headOffset.y = 0.5f;
			}
		}

		public new void OnTriggerEnter(Collider other)
		{
			this.PickUpItem();
		}

		public void PickUpItem()
		{
			bool flag = NetClient.instance == null;
			if (flag)
			{
				this.dispose();
			}
			else
			{
				long curServerTimeStampMS = NetClient.instance.CurServerTimeStampMS;
				bool flag2 = curServerTimeStampMS < DropItem.cantGetTimer;
				if (!flag2)
				{
					bool flag3 = NetClient.instance != null;
					if (flag3)
					{
						bool flag4 = curServerTimeStampMS - this.itemdta.initedTimer < 100L;
						if (flag4)
						{
							return;
						}
					}
					bool flag5 = this.isFake;
					if (flag5)
					{
						FightText.play(FightText.MONEY_TEXT, SelfRole._inst.getHeadPos(), this.itemdta.count, false, -1);
						this.dispose();
					}
					else
					{
						bool flag6 = SelfRole.fsm.Autofighting && !ModelBase<AutoPlayModel>.getInstance().WillPick((uint)this.itemdta.tpid);
						if (!flag6)
						{
							BaseProxy<MapProxy>.getInstance().sendPickUpItem(this.itemdta.dpid);
						}
					}
				}
			}
		}

		public void update(long curTimer)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				bool flag = this.checkOverTime(curTimer);
				if (flag)
				{
					BaseRoomItem.instance.removeDropItm(this.itemdta.dpid, this.isFake);
				}
				bool flag2 = this.checkOwnerTime();
				if (flag2)
				{
					this.itemdta.ownerId = 0u;
				}
			}
		}

		private GameObject getWeapom(DropItemdta item)
		{
			GameObject gameObject = new GameObject();
			int @int = item.itemXml.getInt("job_limit");
			uint @uint = item.itemXml.getUint("obj");
			GameObject gameObject2 = Resources.Load<GameObject>(DropItem.weaponDorpPath[@int] + @uint);
			bool flag = gameObject2 == null;
			GameObject result;
			if (flag)
			{
				result = null;
			}
			else
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
				Transform transform = gameObject3.transform.FindChild("eff");
				bool flag2 = transform != null;
				if (flag2)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
				bool flag3 = @int == 5;
				if (flag3)
				{
					GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
					gameObject4.transform.SetParent(gameObject.transform, false);
					gameObject4.transform.eulerAngles = DropItem.weaponDorpRot[@int] * 1.4f;
					gameObject4.transform.position = DropItem.weaponDorpOffset[@int];
					gameObject4.gameObject.layer = EnumLayer.LM_MAP_ITEM;
					Transform[] componentsInChildren = gameObject4.GetComponentsInChildren<Transform>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						Transform transform2 = componentsInChildren[i];
						transform2.gameObject.layer = EnumLayer.LM_MAP_ITEM;
					}
				}
				gameObject3.gameObject.layer = EnumLayer.LM_MAP_ITEM;
				Transform[] componentsInChildren2 = gameObject3.GetComponentsInChildren<Transform>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Transform transform3 = componentsInChildren2[j];
					transform3.gameObject.layer = EnumLayer.LM_MAP_ITEM;
				}
				gameObject3.transform.SetParent(gameObject.transform, false);
				gameObject3.transform.eulerAngles = DropItem.weaponDorpRot[@int];
				gameObject.transform.localScale = Vector3.one;
				gameObject3.transform.position = DropItem.weaponDorpOffset[@int];
				result = gameObject;
			}
			return result;
		}

		public bool checkOverTime(long curTimer)
		{
			return curTimer >= this.itemdta.left_tm;
		}

		public bool checkOwnerTime()
		{
			return this.itemdta.owner_tm < (uint)NetClient.instance.CurServerTimeStamp;
		}
	}
}
