using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_lowblood : FloatUi
	{
		public static a3_lowblood instance;

		private Image ig_blood;

		private Animator m_ani;

		private GameObject assassin_fx;

		private bool m_hurt = false;

		public override void init()
		{
			this.ig_blood = base.transform.FindChild("blood").GetComponent<Image>();
			this.m_ani = base.transform.GetComponent<Animator>();
			this.assassin_fx = base.transform.FindChild("FX_yingsheng").gameObject;
			a3_lowblood.instance = this;
			base.gameObject.SetActive(false);
			this.assassin_fx.SetActive(false);
			this.ig_blood.gameObject.SetActive(false);
		}

		public void begin_assassin_fx()
		{
			base.gameObject.SetActive(true);
			this.assassin_fx.SetActive(true);
		}

		public void end_assassin_fx()
		{
			base.gameObject.SetActive(false);
			this.assassin_fx.SetActive(false);
		}

		public void begin()
		{
			bool hurt = this.m_hurt;
			if (!hurt)
			{
				base.transform.SetAsLastSibling();
				this.ig_blood.transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
				this.m_ani.enabled = true;
				this.ig_blood.color = Color.red;
				this.ig_blood.gameObject.SetActive(true);
				base.gameObject.SetActive(true);
				this.m_hurt = true;
				base.CancelInvoke("onshowend");
				base.Invoke("onshowend", 5f);
				base.CancelInvoke("onhurt");
				base.Invoke("onhurt", 10f);
			}
		}

		public void end()
		{
			this.m_ani.enabled = false;
			base.gameObject.SetActive(false);
			this.ig_blood.gameObject.SetActive(false);
		}

		private void onhurt()
		{
			this.m_hurt = false;
		}

		private void onshowend()
		{
			this.end();
		}
	}
}
