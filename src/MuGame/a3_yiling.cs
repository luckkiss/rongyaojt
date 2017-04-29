using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class a3_yiling : Window
	{
		private TabControl tab;

		private Transform con;

		private BaseYiling current = null;

		private BaseYiling pet = null;

		private BaseYiling wing = null;

		public static a3_yiling instance;

		public override void init()
		{
			this.con = base.getTransformByPath("con");
			this.tab = new TabControl();
			this.tab.onClickHanle = new Action<TabControl>(this.OnSwitch);
			this.tab.create(base.getGameObjectByPath("tab"), base.gameObject, 0, 0, false);
			BaseButton baseButton = new BaseButton(base.getTransformByPath("close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.OnClose);
			this.CheckLock();
			a3_yiling.instance = this;
		}

		public override void onShowed()
		{
			bool flag = this.current != null;
			if (flag)
			{
				this.current.onShowed();
			}
			else
			{
				this.tab.setSelectedIndex(0, false);
				this.OnSwitch(this.tab);
			}
			GRMap.GAME_CAMERA.SetActive(false);
		}

		public override void onClosed()
		{
			bool flag = this.current != null;
			if (flag)
			{
				this.current.onClose();
			}
			GRMap.GAME_CAMERA.SetActive(true);
		}

		private void OnSwitch(TabControl t)
		{
			int seletedIndex = t.getSeletedIndex();
			bool flag = this.current != null;
			if (flag)
			{
				this.current.onClose();
				this.current.gameObject.SetActive(false);
			}
			bool flag2 = seletedIndex == 0;
			if (flag2)
			{
				bool flag3 = ModelBase<A3_PetModel>.getInstance().hasPet();
				if (flag3)
				{
					bool flag4 = this.pet == null;
					if (flag4)
					{
						GameObject original = Resources.Load<GameObject>("prefab/a3_pet_skin");
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					}
					bool flag5 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.PET, false);
					if (flag5)
					{
						this.current = this.pet;
					}
				}
				else
				{
					flytxt.instance.fly("宠物未获得", 0, default(Color), null);
					this.current = null;
				}
			}
			else
			{
				bool flag6 = seletedIndex == 1;
				if (flag6)
				{
					bool flag7 = this.wing == null;
					if (flag7)
					{
						GameObject original2 = Resources.Load<GameObject>("prefab/a3_wing_skin");
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original2);
					}
					bool flag8 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.PET_SWING, false);
					if (flag8)
					{
						this.current = this.wing;
					}
				}
			}
			bool flag9 = this.current != null;
			if (flag9)
			{
				this.current.onShowed();
				this.current.visiable = true;
				bool flag10 = !this.current.__mainTrans.gameObject.activeSelf;
				if (flag10)
				{
					this.current.__mainTrans.gameObject.SetActive(true);
				}
			}
		}

		private void OnClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_YILING);
		}

		public void CheckLock()
		{
			base.transform.FindChild("tab/pet").gameObject.SetActive(false);
			base.transform.FindChild("tab/wing").gameObject.SetActive(false);
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.PET, false);
			if (flag)
			{
			}
			bool flag2 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.PET_SWING, false);
			if (flag2)
			{
				this.OpenWing();
			}
		}

		public void OpenPet()
		{
			Transform transform = base.transform.FindChild("tab/pet");
			bool flag = transform;
			if (flag)
			{
				transform.gameObject.SetActive(true);
			}
			BaseProxy<A3_PetProxy>.getInstance().GetPets();
		}

		public void OpenWing()
		{
			Transform transform = base.transform.FindChild("tab/wing");
			bool flag = transform;
			if (flag)
			{
				transform.gameObject.SetActive(true);
			}
			BaseProxy<A3_WingProxy>.getInstance().GetWings();
		}
	}
}
