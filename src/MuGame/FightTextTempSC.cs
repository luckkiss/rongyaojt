using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	[AddComponentMenu("QSMY/FightText")]
	public class FightTextTempSC : MonoBehaviour
	{
		public static int TYPE_TEXT = 0;

		public static int TYPE_ANI = 1;

		public static Vector3 vec_two = new Vector3(2f, 2f, 2f);

		internal Animator ani;

		public List<FightTextTempSC> pool;

		public List<FightTextTempSC> playingPool;

		public Text txt;

		internal int _type;

		public float timer = 0f;

		public int idx = 0;

		internal void init(int type)
		{
			this._type = type;
			this.ani = base.GetComponent<Animator>();
			bool flag = this._type == FightTextTempSC.TYPE_TEXT;
			if (flag)
			{
				this.txt = base.transform.FindChild("Text").GetComponent<Text>();
			}
		}

		public void onAniOver()
		{
			base.gameObject.SetActive(false);
			this.playingPool.Remove(this);
			this.pool.Add(this);
		}

		internal void setActive(bool b)
		{
			base.gameObject.SetActive(b);
		}

		internal void play(Vector3 pos, int num, bool criatk)
		{
			bool flag = this._type == FightTextTempSC.TYPE_TEXT;
			if (flag)
			{
				int num2 = num;
				bool flag2 = num2 == 0;
				if (flag2)
				{
					num2 = 1;
				}
				bool flag3 = num2 >= 99999999;
				if (flag3)
				{
					num2 = 99999999;
				}
				this.txt.text = (criatk ? "暴" : "") + num2;
				if (criatk)
				{
					this.txt.GetComponent<RectTransform>().localScale = FightTextTempSC.vec_two;
				}
				else
				{
					this.txt.GetComponent<RectTransform>().localScale = Vector3.one;
				}
			}
			RectTransform component = base.GetComponent<RectTransform>();
			component.position = pos;
		}
	}
}
