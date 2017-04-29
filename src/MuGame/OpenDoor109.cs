using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	public class OpenDoor109 : MonoBehaviour
	{
		public int triggerTimes = 1;

		private int km_count;

		private int mid;

		public int doorkill;

		private Dictionary<int, int> allkill = new Dictionary<int, int>();

		private Dictionary<int, Variant> phase = new Dictionary<int, Variant>();

		private Dictionary<int, Dictionary<string, string>> phaseChild = new Dictionary<int, Dictionary<string, string>>();

		private Variant data;

		private int level = 2;

		private BoxCollider box;

		private Transform idtext;

		private Dictionary<int, int> killsome = new Dictionary<int, int>();

		private void Start()
		{
			this.box = base.gameObject.GetComponent<BoxCollider>();
			bool flag = this.box == null;
			if (flag)
			{
				this.box = base.gameObject.AddComponent<BoxCollider>();
			}
			this.box.isTrigger = true;
			base.gameObject.layer = EnumLayer.LM_PT;
			this.data = SvrLevelConfig.instacne.get_level_data(109u);
			this.doorkill = a3_counterpart.lvl;
			this.read();
			BaseProxy<TeamProxy>.getInstance().addEventListener(237u, new Action<GameEvent>(this.killNum));
		}

		private void read()
		{
			this.killsome.Clear();
			this.phase.Clear();
			this.phaseChild.Clear();
			for (int i = 0; i < this.data["diff_lvl"][this.doorkill]["phase"]._arr.Count; i++)
			{
				bool flag = !this.phase.ContainsKey(i);
				if (flag)
				{
					this.phase.Add(i, this.data["diff_lvl"][this.doorkill]["phase"][i]);
				}
			}
			foreach (KeyValuePair<int, Variant> current in this.phase)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("p", current.Value["p"]._str);
				dictionary.Add("des", current.Value["des"]._str);
				dictionary.Add("target", current.Value["target"]._str);
				dictionary.Add("num", current.Value["num"]._str);
				bool flag2 = !this.phaseChild.ContainsKey(current.Key);
				if (flag2)
				{
					this.phaseChild.Add(current.Key, dictionary);
				}
				bool flag3 = !this.killsome.ContainsKey(current.Value["p"]._int);
				if (flag3)
				{
					this.killsome.Add(current.Value["p"]._int, current.Value["num"]._int);
				}
			}
		}

		private void killNum(GameEvent e)
		{
			debug.Log(e.data.dump());
			this.idtext = a3_insideui_fb.instance.GetNowTran().FindChild("info/info_killnums/mid");
			this.km_count = e.data["km_count"];
			this.mid = e.data["m_id"]._int;
			bool flag = this.allkill.ContainsKey(this.mid);
			if (flag)
			{
				this.allkill[this.mid] = this.km_count;
			}
			else
			{
				this.allkill.Add(this.mid, this.km_count);
			}
			Transform transform = a3_insideui_fb.instance.GetNowTran().FindChild("info/info_killnums/Text");
			bool flag2 = !this.killsome.ContainsKey(a3_insideui_fb.instance.doors + 1);
			if (!flag2)
			{
				bool flag3 = transform != null;
				if (flag3)
				{
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
					{
						this.km_count.ToString()
					}) + "/" + this.killsome[a3_insideui_fb.instance.doors + 1];
				}
				bool flag4 = this.km_count == this.killsome[a3_insideui_fb.instance.doors + 1];
				if (flag4)
				{
					a3_insideui_fb.instance.doors++;
					bool flag5 = !this.killsome.ContainsKey(a3_insideui_fb.instance.doors + 1);
					if (!flag5)
					{
						bool flag6 = this.idtext != null;
						if (flag6)
						{
							this.idtext.GetComponent<Text>().text = ContMgr.getCont("fb_info_9", new string[]
							{
								this.phaseChild[a3_insideui_fb.instance.doors]["des"]
							});
						}
						bool flag7 = transform != null;
						if (flag7)
						{
							transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
							{
								0.ToString()
							}) + "/" + this.killsome[a3_insideui_fb.instance.doors + 1];
						}
					}
				}
			}
		}

		private void OnDestroy()
		{
			BaseProxy<TeamProxy>.getInstance().removeEventListener(237u, new Action<GameEvent>(this.killNum));
		}
	}
}
