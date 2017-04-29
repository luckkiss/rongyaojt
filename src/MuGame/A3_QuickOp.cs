using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	internal class A3_QuickOp : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly A3_QuickOp.<>c <>9 = new A3_QuickOp.<>c();

			public static Action<GameObject> <>9__7_0;

			internal void <init>b__7_0(GameObject go)
			{
				SelfRole.moveToNPc((int)ModelBase<PlayerModel>.getInstance().mapid, 1003, null, null);
			}
		}

		private Transform tfSkillbarCombat;

		private GameObject goUpBtn;

		private GameObject goBeStronger;

		private A3_QuickOp instance;

		private A3_QuickOp Instance
		{
			get
			{
				bool flag = this.instance != null;
				A3_QuickOp result;
				if (flag)
				{
					result = this.instance;
				}
				else
				{
					(this.instance = new A3_QuickOp()).init();
					result = this.instance;
				}
				return result;
			}
			set
			{
				this.instance = value;
			}
		}

		public override void init()
		{
			this.Instance = this;
			joinWorldInfo.instance.g_mgr.addEventListener(58u, new Action<GameEvent>(this.OnSceneChange));
			this.tfSkillbarCombat = skillbar.instance.transform.FindChild("combat");
			this.goBeStronger = A3_BeStronger.Instance.gameObject;
			this.goUpBtn = this.goBeStronger.transform.FindChild("upbtn").gameObject;
			new BaseButton(base.transform.FindChild("openAuction"), 1, 1).onClick = new Action<GameObject>(this.OpenAuction);
			new BaseButton(base.transform.FindChild("go2warehouse"), 1, 1).onClick = new Action<GameObject>(this.MoveToWarehouseNPC);
			new BaseButton(base.transform.FindChild("go2treasureMap"), 1, 1).onClick = new Action<GameObject>(this.MoveToTreasureMapNPC);
			BaseButton arg_11D_0 = new BaseButton(base.getTransformByPath("gotoDart"), 1, 1);
			Action<GameObject> arg_11D_1;
			if ((arg_11D_1 = A3_QuickOp.<>c.<>9__7_0) == null)
			{
				arg_11D_1 = (A3_QuickOp.<>c.<>9__7_0 = new Action<GameObject>(A3_QuickOp.<>c.<>9.<init>b__7_0));
			}
			arg_11D_0.onClick = arg_11D_1;
			bool flag = ModelBase<PlayerModel>.getInstance().mapid == 10u;
			if (flag)
			{
				A3_BeStronger.Instance.Owner = this.Instance.transform;
				bool flag2 = !this.goBeStronger.activeSelf;
				if (flag2)
				{
					this.goBeStronger.SetActive(true);
					this.goUpBtn.SetActive(A3_BeStronger.Instance.CheckUpItem());
				}
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}

		public override void onShowed()
		{
			base.onShowed();
		}

		public void OnSceneChange(GameEvent e)
		{
			bool flag = e.data["mpid"]._uint == 10u;
			if (flag)
			{
				bool flag2 = !A3_BeStronger.Instance.Owner.Equals(this.Instance.transform);
				if (flag2)
				{
					A3_BeStronger.Instance.Owner = this.Instance.transform;
					bool flag3 = !this.goBeStronger.activeSelf;
					if (flag3)
					{
						this.goBeStronger.SetActive(true);
						this.goUpBtn.SetActive(A3_BeStronger.Instance.CheckUpItem());
					}
				}
				bool flag4 = !base.gameObject.activeSelf;
				if (flag4)
				{
					base.gameObject.SetActive(true);
				}
			}
			else
			{
				bool flag5 = !A3_BeStronger.Instance.Owner.Equals(this.tfSkillbarCombat);
				if (flag5)
				{
					A3_BeStronger.Instance.Owner = this.tfSkillbarCombat;
				}
				bool activeSelf = base.gameObject.activeSelf;
				if (activeSelf)
				{
					base.gameObject.SetActive(false);
				}
			}
		}

		public void OpenAuction(GameObject go)
		{
			SelfRole.fsm.ChangeState(StateIdle.Instance);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_AUCTION, null, false);
		}

		public void MoveToWarehouseNPC(GameObject go)
		{
			SelfRole.moveToNPc((int)ModelBase<PlayerModel>.getInstance().mapid, 1010, null, null);
		}

		public void MoveToTreasureMapNPC(GameObject go)
		{
			SelfRole.moveToNPc((int)ModelBase<PlayerModel>.getInstance().mapid, 1001, null, null);
		}
	}
}
