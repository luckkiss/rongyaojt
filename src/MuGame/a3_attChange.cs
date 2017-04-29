using DG.Tweening;
using DG.Tweening.Core;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_attChange : LoadingUI
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_attChange.<>c <>9 = new a3_attChange.<>c();

			public static DOSetter<float> <>9__16_1;

			public static DOSetter<float> <>9__17_1;

			internal void <setbg_over>b__16_1(float s)
			{
			}

			internal void <wite>b__17_1(float s)
			{
			}
		}

		private List<Dictionary<uint, int>> change_att = new List<Dictionary<uint, int>>();

		private GameObject item;

		private Transform con;

		private GameObject bg;

		private GameObject veiw;

		private bool canshow = false;

		private float itemsize;

		public static a3_attChange instans;

		private bool onshow = false;

		private uint curtype = 0u;

		private float viewhight = 0f;

		private float waiteTime = 0.5f;

		private float time_over = 0f;

		private float waiteTime_h = 0.8f;

		private bool show_bg = true;

		private bool onOver = false;

		public override void init()
		{
			a3_attChange.instans = this;
			this.bg = base.transform.FindChild("bg").gameObject;
			base.gameObject.SetActive(false);
			this.item = base.transform.FindChild("scrollview/item").gameObject;
			this.con = base.transform.FindChild("scrollview/con");
			this.veiw = base.transform.FindChild("scrollview").gameObject;
			this.itemsize = this.con.GetComponent<GridLayoutGroup>().cellSize.y;
			this.bg.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
		}

		public override void onShowed()
		{
			for (int i = 0; i < this.con.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.con.GetChild(i).gameObject);
			}
		}

		public void runTxt(Dictionary<uint, int> l)
		{
			bool flag = !ModelBase<a3_EquipModel>.getInstance().Attchange_wite;
			if (flag)
			{
				bool flag2 = !this.onshow;
				if (flag2)
				{
					this.canshow = true;
					this.show_bg = true;
					this.time_over = this.waiteTime_h;
					base.gameObject.SetActive(true);
				}
				this.onshow = true;
			}
			bool flag3 = l != null;
			if (flag3)
			{
				this.change_att.Add(l);
				bool flag4 = this.change_att.Count >= 3;
				if (flag4)
				{
					this.change_att.Remove(this.change_att[1]);
				}
			}
		}

		private void showAtt()
		{
			using (Dictionary<uint, int>.KeyCollection.Enumerator enumerator = this.change_att[0].Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					a3_attChange.<>c__DisplayClass14_1 <>c__DisplayClass14_ = new a3_attChange.<>c__DisplayClass14_1();
					<>c__DisplayClass14_.type = enumerator.Current;
					this.curtype = <>c__DisplayClass14_.type;
					GameObject clon = UnityEngine.Object.Instantiate<GameObject>(this.item);
					clon.SetActive(true);
					int num = ModelBase<PlayerModel>.getInstance().attr_list[<>c__DisplayClass14_.type];
					clon.transform.FindChild("attValue").GetComponent<Text>().text = Globle.getAttrAddById((int)<>c__DisplayClass14_.type, num - this.change_att[0][<>c__DisplayClass14_.type], true);
					int ss = 0;
					DOTween.To(() => (float)ss, delegate(float s)
					{
						ss = (int)s;
						clon.transform.FindChild("addValue").GetComponent<Text>().text = Globle.getAttrAddById_value((int)<>c__DisplayClass14_.type, ss, true);
					}, (float)num, 1f);
					clon.transform.SetParent(this.con, false);
				}
			}
			this.wite();
		}

		private void setbg()
		{
			int ss = 0;
			this.viewhight = (float)this.change_att[0].Count * this.itemsize;
			Tweener t = DOTween.To(() => (float)ss, delegate(float s)
			{
				ss = (int)s;
				this.bg.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)ss);
			}, (float)this.change_att[0].Count * this.itemsize + 20f, 0.5f);
			this.veiw.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this.change_att[0].Count * this.itemsize);
			t.OnComplete(delegate
			{
				this.showAtt();
			});
		}

		private void setbg_over()
		{
			int ss = 0;
			DOGetter<float> arg_43_0 = () => (float)ss;
			DOSetter<float> arg_43_1;
			if ((arg_43_1 = a3_attChange.<>c.<>9__16_1) == null)
			{
				arg_43_1 = (a3_attChange.<>c.<>9__16_1 = new DOSetter<float>(a3_attChange.<>c.<>9.<setbg_over>b__16_1));
			}
			Tweener t = DOTween.To(arg_43_0, arg_43_1, 0f, 0.5f);
			t.OnComplete(delegate
			{
				this.bg.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
				for (int i = 0; i < this.con.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.con.GetChild(i).gameObject);
				}
				bool flag = this.change_att.Count <= 0;
				if (flag)
				{
					base.gameObject.SetActive(false);
					this.onshow = false;
					this.canshow = false;
					this.show_bg = false;
				}
				else
				{
					this.canshow = true;
					this.show_bg = true;
				}
			});
		}

		private void wite()
		{
			this.canshow = false;
			int ss = 0;
			DOGetter<float> arg_4A_0 = () => (float)ss;
			DOSetter<float> arg_4A_1;
			if ((arg_4A_1 = a3_attChange.<>c.<>9__17_1) == null)
			{
				arg_4A_1 = (a3_attChange.<>c.<>9__17_1 = new DOSetter<float>(a3_attChange.<>c.<>9.<wite>b__17_1));
			}
			Tweener t = DOTween.To(arg_4A_0, arg_4A_1, 0f, 1.5f);
			t.OnComplete(delegate
			{
				for (int i = 0; i < this.con.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.con.GetChild(i).gameObject);
				}
				this.bg.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
				this.change_att.Remove(this.change_att[0]);
				bool flag = this.change_att.Count > 0;
				if (flag)
				{
					this.show_bg = true;
					this.canshow = true;
				}
				else
				{
					bool flag2 = this.change_att == null || this.change_att.Count <= 0;
					if (flag2)
					{
						base.gameObject.SetActive(false);
						this.onshow = false;
						this.canshow = false;
						this.show_bg = false;
					}
				}
			});
		}

		private void Update()
		{
			bool flag = !this.canshow;
			if (!flag)
			{
				bool flag2 = this.show_bg;
				if (flag2)
				{
					this.setbg();
					this.show_bg = false;
				}
			}
		}

		public override void onClosed()
		{
		}
	}
}
