using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_active_mwlr_kill
	{
		public Transform Bar_Mon;

		public List<Transform> Mon_Icons = new List<Transform>();

		public Text Text_DoubleTime;

		private static a3_active_mwlr_kill instance;

		private bool? b_clear;

		private bool? b_reset;

		private bool b_endPassed;

		public List<int> listKilled;

		public Animator MHAnimator
		{
			get;
			set;
		}

		public Transform MHInfo
		{
			get;
			set;
		}

		public int Count
		{
			get;
			set;
		}

		public static Variant initLoginData
		{
			get;
			set;
		}

		public static a3_active_mwlr_kill Instance
		{
			get
			{
				return a3_active_mwlr_kill.instance = (a3_active_mwlr_kill.instance ?? new a3_active_mwlr_kill());
			}
		}

		public bool NewAction
		{
			get;
			set;
		}

		public bool isReset
		{
			get
			{
				return this.b_reset.HasValue;
			}
		}

		private a3_active_mwlr_kill()
		{
			this.Init();
		}

		public void Init()
		{
			a3_active_mwlr_kill.instance = this;
			this.listKilled = new List<int>();
			this.NewAction = false;
			this.MHInfo = a3_expbar.instance.transform.FindChild("mh_tips");
			this.MHAnimator = this.MHInfo.GetComponent<Animator>();
			this.Bar_Mon = a3_expbar.instance.transform.FindChild("mh_tips/a3_mhfloatBorder");
			this.Text_DoubleTime = this.Bar_Mon.FindChild("Text").GetComponent<Text>();
			for (int i = 0; i < this.Bar_Mon.childCount; i++)
			{
				Transform transform = this.Bar_Mon.FindChild("hunt_icon_" + i.ToString());
				bool flag = transform != null;
				if (flag)
				{
					this.Mon_Icons.Add(transform);
				}
			}
			this.b_clear = !(this.b_reset = new bool?(true));
		}

		public void Update()
		{
			bool flag = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id.Count == 0 && !this.Bar_Mon.gameObject.activeSelf;
			if (!flag)
			{
				bool flag2 = this.Text_DoubleTime != null && this.Text_DoubleTime.gameObject.activeSelf;
				if (flag2)
				{
					TimeSpan timeSpan = new TimeSpan(0, 0, Mathf.Max(0, ModelBase<A3_ActiveModel>.getInstance().mwlr_doubletime - muNetCleint.instance.CurServerTimeStamp));
					this.Text_DoubleTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
					bool flag3 = timeSpan.Hours == 0 && timeSpan.Minutes == 0 && timeSpan.Seconds == 0;
					if (flag3)
					{
						this.Text_DoubleTime.gameObject.SetActive(false);
					}
				}
				bool flag4 = this.b_clear.HasValue && this.b_reset.HasValue;
				if (flag4)
				{
					Animator expr_119 = this.MHAnimator;
					bool flag5 = expr_119 != null && expr_119.GetCurrentAnimatorStateInfo(0).IsName("mh_init");
					if (flag5)
					{
						bool valueOrDefault = this.b_clear.GetValueOrDefault();
						if (valueOrDefault)
						{
							this.Bar_Mon.gameObject.SetActive(false);
							bool flag6 = this.b_endPassed;
							if (flag6)
							{
								this.MHInfo.gameObject.SetActive(false);
								this.b_clear = null;
								this.b_endPassed = false;
								return;
							}
						}
						bool valueOrDefault2 = this.b_reset.GetValueOrDefault();
						if (valueOrDefault2)
						{
							bool flag7 = this.Count > 0;
							if (flag7)
							{
								this.Bar_Mon.gameObject.SetActive(true);
							}
							this.MHInfo.gameObject.SetActive(true);
							this.b_reset = null;
						}
					}
					Animator expr_1F3 = this.MHAnimator;
					bool flag8 = expr_1F3 != null && expr_1F3.GetCurrentAnimatorStateInfo(0).IsName("mh_end");
					if (flag8)
					{
						this.b_endPassed = true;
					}
				}
			}
		}

		public void Reset()
		{
			this.b_clear = !(this.b_reset = new bool?(true));
			a3_expbar.instance.HoldTip();
			this.Text_DoubleTime.gameObject.SetActive(true);
			for (int i = 0; i < this.Count; i++)
			{
				this.Mon_Icons[i].GetChild(0).gameObject.SetActive(false);
				bool flag = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id.Count > i;
				if (flag)
				{
					Variant variant = SvrMapConfig.instance.mapConfs[(uint)ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id[i]];
					bool flag2 = variant.ContainsKey("map_name");
					if (flag2)
					{
						this.Mon_Icons[i].GetChild(1).GetComponent<Text>().text = variant["map_name"];
					}
					else
					{
						this.Mon_Icons[i].GetChild(1).GetComponent<Text>().text = "";
					}
					this.Mon_Icons[i].gameObject.SetActive(true);
				}
			}
			bool flag3 = this.Count == 0;
			if (flag3)
			{
				ModelBase<A3_ActiveModel>.getInstance().mwlr_mon_killed = 0;
			}
			bool flag4 = ModelBase<A3_ActiveModel>.getInstance().mwlr_mon_killed > 0;
			if (flag4)
			{
				this.Refresh();
			}
		}

		public void Refresh()
		{
			for (int i = 0; i < ModelBase<A3_ActiveModel>.getInstance().mwlr_mon_killed; i++)
			{
				this.Mon_Icons[ModelBase<A3_ActiveModel>.getInstance().listKilled[i]].transform.GetChild(0).gameObject.SetActive(true);
			}
		}

		public void Clear()
		{
			bool activeSelf = this.Bar_Mon.gameObject.activeSelf;
			if (activeSelf)
			{
				Animator expr_1B = this.MHAnimator;
				if (expr_1B != null)
				{
					expr_1B.SetTrigger("end");
				}
			}
			this.Count = 0;
			for (int i = 0; i < this.Mon_Icons.Count; i++)
			{
				Transform expr_44 = this.Mon_Icons[i];
				if (expr_44 != null)
				{
					expr_44.gameObject.SetActive(false);
				}
			}
			this.b_clear = !(this.b_reset = new bool?(false));
			ModelBase<A3_ActiveModel>.getInstance().listKilled.Clear();
		}

		public void ReloadData(Variant mapInfo)
		{
			this.Clear();
			bool flag = mapInfo.Count == 0;
			if (!flag)
			{
				ModelBase<A3_ActiveModel>.getInstance().mwlr_mon_killed = 0;
				this.Count = mapInfo.Count;
				for (int i = 0; i < this.Count; i++)
				{
					bool @bool = mapInfo[i]["kill"]._bool;
					if (@bool)
					{
						A3_ActiveModel expr_57 = ModelBase<A3_ActiveModel>.getInstance();
						int mwlr_mon_killed = expr_57.mwlr_mon_killed;
						expr_57.mwlr_mon_killed = mwlr_mon_killed + 1;
						bool flag2 = !ModelBase<A3_ActiveModel>.getInstance().listKilled.Contains(i);
						if (flag2)
						{
							ModelBase<A3_ActiveModel>.getInstance().listKilled.Add(i);
						}
					}
				}
				this.Reset();
				this.MHInfo.gameObject.SetActive(true);
			}
		}
	}
}
