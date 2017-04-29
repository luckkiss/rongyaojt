using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_legion_application : a3BaseLegion
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_legion_application.<>c <>9 = new a3_legion_application.<>c();

			public static UnityAction<bool> <>9__3_1;

			internal void <init>b__3_1(bool b)
			{
				BaseProxy<A3_LegionProxy>.getInstance().SendChangeApplyMode(b);
			}
		}

		private Transform content;

		private Toggle tt;

		public a3_legion_application(BaseShejiao win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			this.content = base.getTransformByPath("cells/scroll/content");
			this.tt = base.transform.FindChild("Toggle").GetComponent<Toggle>();
			this.tt.isOn = ModelBase<A3_LegionModel>.getInstance().CanAutoApply;
			new BaseButton(base.transform.FindChild("touch_toggle"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				bool flag = ModelBase<A3_LegionModel>.getInstance().members != null;
				if (flag)
				{
					foreach (int current in ModelBase<A3_LegionModel>.getInstance().members.Keys)
					{
						bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().cid == (ulong)((long)ModelBase<A3_LegionModel>.getInstance().members[current].cid);
						if (flag2)
						{
							bool flag3 = ModelBase<A3_LegionModel>.getInstance().members[current].clanc > 1;
							if (!flag3)
							{
								flytxt.flyUseContId("clan_8", null, 0);
								break;
							}
							BaseProxy<A3_LegionProxy>.getInstance().SendChangeApplyMode(!this.tt.isOn);
						}
					}
				}
			};
			UnityEvent<bool> arg_96_0 = this.tt.onValueChanged;
			UnityAction<bool> arg_96_1;
			if ((arg_96_1 = a3_legion_application.<>c.<>9__3_1) == null)
			{
				arg_96_1 = (a3_legion_application.<>c.<>9__3_1 = new UnityAction<bool>(a3_legion_application.<>c.<>9.<init>b__3_1));
			}
			arg_96_0.AddListener(arg_96_1);
		}

		public override void onShowed()
		{
			foreach (int current in ModelBase<A3_LegionModel>.getInstance().members.Keys)
			{
				bool flag = (ulong)ModelBase<PlayerModel>.getInstance().cid == (ulong)((long)ModelBase<A3_LegionModel>.getInstance().members[current].cid);
				if (flag)
				{
					bool flag2 = ModelBase<A3_LegionModel>.getInstance().members[current].clanc > 1;
					if (!flag2)
					{
						break;
					}
					BaseProxy<A3_LegionProxy>.getInstance().addEventListener(11u, new Action<GameEvent>(this.OnChangeApplyMode));
					BaseProxy<A3_LegionProxy>.getInstance().addEventListener(16u, new Action<GameEvent>(this.OnGetApplicant));
					BaseProxy<A3_LegionProxy>.getInstance().addEventListener(4u, new Action<GameEvent>(this.OnApproveOrReject));
					BaseProxy<A3_LegionProxy>.getInstance().SendGetApplicant();
				}
			}
		}

		public override void onClose()
		{
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(11u, new Action<GameEvent>(this.OnChangeApplyMode));
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(16u, new Action<GameEvent>(this.OnGetApplicant));
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(4u, new Action<GameEvent>(this.OnApproveOrReject));
			Transform transformByPath = base.getTransformByPath("cells/scroll/content");
			Transform transformByPath2 = base.getTransformByPath("cells/scroll/0");
			Transform[] componentsInChildren = transformByPath.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent == transformByPath;
				if (flag)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
		}

		private void OnChangeApplyMode(GameEvent e)
		{
			this.tt.isOn = ModelBase<A3_LegionModel>.getInstance().CanAutoApply;
		}

		private void OnGetApplicant(GameEvent e)
		{
			Transform transformByPath = base.getTransformByPath("cells/scroll/0");
			Transform[] componentsInChildren = this.content.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent == this.content;
				if (flag)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			using (Dictionary<int, A3_LegionMember>.ValueCollection.Enumerator enumerator = ModelBase<A3_LegionModel>.getInstance().applicant.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					A3_LegionMember v = enumerator.Current;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transformByPath.gameObject);
					gameObject.transform.SetParent(this.content);
					gameObject.transform.FindChild("name").GetComponent<Text>().text = v.name;
					gameObject.transform.FindChild("zy").GetComponent<Text>().text = A3_LegionModel.GetCarr(v.carr);
					Text arg_126_0 = gameObject.transform.FindChild("dj").GetComponent<Text>();
					int num = v.lvl;
					arg_126_0.text = num.ToString();
					Text arg_157_0 = gameObject.transform.FindChild("zdl").GetComponent<Text>();
					num = v.combpt;
					arg_157_0.text = num.ToString();
					gameObject.transform.localScale = Vector3.one;
					UnityEngine.Object arg_186_0 = gameObject;
					num = v.cid;
					arg_186_0.name = num.ToString();
					gameObject.SetActive(true);
					List<A3_LegionMember> list = new List<A3_LegionMember>();
					list.Add(v);
					new BaseButton(gameObject.transform.FindChild("yes"), 1, 1).onClick = delegate(GameObject g)
					{
						BaseProxy<A3_LegionProxy>.getInstance().SendYN((uint)v.cid, true);
					};
					new BaseButton(gameObject.transform.FindChild("no"), 1, 1).onClick = delegate(GameObject g)
					{
						BaseProxy<A3_LegionProxy>.getInstance().SendYN((uint)v.cid, false);
					};
				}
			}
			this.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, this.content.GetComponent<GridLayoutGroup>().cellSize.y * (float)ModelBase<A3_LegionModel>.getInstance().list.Count);
		}

		private void OnOpApplicant(GameEvent e)
		{
			Variant data = e.data;
			uint num = data["cid"];
			bool flag = data["approved"];
			Transform transform = this.content.FindChild(num.ToString());
			bool flag2 = transform != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}

		private void OnApproveOrReject(GameEvent e)
		{
			Variant data = e.data;
			uint num = data["cid"];
			bool flag = data["approved"];
			Transform transform = this.content.FindChild(num.ToString());
			bool flag2 = transform != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}
}
