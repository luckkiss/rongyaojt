using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	public class WdsyOpenDoor : MonoBehaviour
	{
		public static WdsyOpenDoor instance;

		public int triggerTimes = 1;

		private int km_count;

		private int mid;

		private int lastkillall = 0;

		public int doorkill;

		private Dictionary<int, int> allkill = new Dictionary<int, int>();

		private Dictionary<int, Variant> phase = new Dictionary<int, Variant>();

		private Dictionary<int, Dictionary<string, string>> phaseChild = new Dictionary<int, Dictionary<string, string>>();

		private Variant data;

		private Transform wall0;

		private Transform wall1;

		private Transform wall2;

		private Transform wall3;

		private int level = 2;

		private BoxCollider box;

		private Transform idtext;

		private Dictionary<int, int> killsome = new Dictionary<int, int>();

		private void Start()
		{
			WdsyOpenDoor.instance = this;
			this.box = base.gameObject.GetComponent<BoxCollider>();
			bool flag = this.box == null;
			if (flag)
			{
				this.box = base.gameObject.AddComponent<BoxCollider>();
			}
			this.box.isTrigger = true;
			base.gameObject.layer = EnumLayer.LM_PT;
			this.wall0 = base.gameObject.transform.parent.parent.FindChild("FX_common_door00");
			this.wall1 = base.gameObject.transform.parent.parent.FindChild("FX_common_door01");
			this.wall2 = base.gameObject.transform.parent.parent.FindChild("FX_common_door02");
			this.wall3 = base.gameObject.transform.parent.parent.FindChild("FX_common_door03");
			this.data = SvrLevelConfig.instacne.get_level_data(108u);
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

		public void OnTriggerEnter(Collider other)
		{
			int num = 0;
			int num2 = int.Parse(this.box.name);
			int num3 = this.data["diff_lvl"][this.doorkill]["map"][0]["trigger"][num2]["km"][0]["kmcnt"];
			int key = this.data["diff_lvl"][this.doorkill]["map"][0]["trigger"][num2]["km"][0]["mid"];
			this.idtext = a3_insideui_fb.instance.GetNowTran().FindChild("info/info_killnums/mid");
			Transform transform = a3_insideui_fb.instance.GetNowTran().FindChild("info/info_killnums/Text");
			bool flag = this.box.name == "0" && this.allkill.ContainsKey(key) && this.allkill[key] >= num3;
			if (flag)
			{
				this.level = 3;
				this.wall0.gameObject.SetActive(false);
				a3_insideui_fb.instance.needkill -= this.killsome[num2 + 1];
				a3_insideui_fb.instance.doors = 1;
				bool flag2 = this.idtext != null;
				if (flag2)
				{
					this.idtext.GetComponent<Text>().text = ContMgr.getCont("fb_info_9", new string[]
					{
						this.phaseChild[a3_insideui_fb.instance.doors]["des"]
					});
				}
				bool flag3 = transform != null;
				if (flag3)
				{
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
					{
						0.ToString()
					}) + "/" + this.killsome[a3_insideui_fb.instance.doors + 1];
				}
				UnityEngine.Object.Destroy(this.box.gameObject);
			}
			else
			{
				bool flag4 = this.box.name == "0";
				if (flag4)
				{
					this.level = 2;
				}
			}
			bool flag5 = this.box.name == "1" && this.allkill.ContainsKey(key) && this.allkill[key] >= num3;
			if (flag5)
			{
				this.level = 4;
				this.lastkillall = this.killsome[num2 + 1];
				a3_insideui_fb.instance.needkill -= this.lastkillall;
				a3_insideui_fb.instance.doors = 2;
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
				this.wall1.gameObject.SetActive(false);
				UnityEngine.Object.Destroy(this.box.gameObject);
			}
			else
			{
				bool flag8 = this.box.name == "1";
				if (flag8)
				{
					this.level = 3;
				}
			}
			bool flag9 = this.box.name == "2" && this.allkill.ContainsKey(key) && this.allkill[key] >= num3;
			if (flag9)
			{
				this.level = 5;
				a3_insideui_fb.instance.needkill -= this.killsome[num2 + 1];
				a3_insideui_fb.instance.doors = 3;
				bool flag10 = this.idtext != null;
				if (flag10)
				{
					this.idtext.GetComponent<Text>().text = ContMgr.getCont("fb_info_9", new string[]
					{
						this.phaseChild[a3_insideui_fb.instance.doors - 1]["des"]
					});
				}
				bool flag11 = transform != null;
				if (flag11)
				{
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
					{
						(a3_insideui_fb.instance.needkill - this.lastkillall + this.killsome[a3_insideui_fb.instance.doors]).ToString()
					}) + "/" + this.killsome[a3_insideui_fb.instance.doors];
				}
				this.wall2.gameObject.SetActive(false);
				UnityEngine.Object.Destroy(this.box.gameObject);
			}
			else
			{
				bool flag12 = this.box.name == "2";
				if (flag12)
				{
					this.level = 4;
				}
			}
			bool flag13 = this.box.name == "3" && this.allkill.ContainsKey(key) && this.allkill[key] >= num3;
			if (flag13)
			{
				this.level = 6;
				a3_insideui_fb.instance.needkill -= this.killsome[num2 + 1];
				a3_insideui_fb.instance.doors = 4;
				bool flag14 = this.idtext != null;
				if (flag14)
				{
					this.idtext.GetComponent<Text>().text = ContMgr.getCont("fb_info_9", new string[]
					{
						this.phaseChild[a3_insideui_fb.instance.doors - 1]["des"]
					});
				}
				bool flag15 = transform != null;
				if (flag15)
				{
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
					{
						(a3_insideui_fb.instance.needkill - this.lastkillall + this.killsome[a3_insideui_fb.instance.doors]).ToString()
					}) + "/" + this.killsome[4];
				}
				this.wall3.gameObject.SetActive(false);
				UnityEngine.Object.Destroy(this.box.gameObject);
			}
			else
			{
				bool flag16 = this.box.name == "3";
				if (flag16)
				{
					this.level = 5;
				}
			}
			for (int i = 0; i < this.level; i++)
			{
				num += NavmeshUtils.listARE[i];
			}
			bool flag17 = num == 0;
			if (!flag17)
			{
				SelfRole._inst.setNavLay(num);
			}
		}

		public void killNum(GameEvent e)
		{
			debug.Log(e.data.dump());
			a3_insideui_fb.instance.needkill = e.data["total_count"];
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
			this.lastkillall = 0;
			bool flag2 = a3_insideui_fb.instance.doors == 0;
			if (flag2)
			{
				bool flag3 = transform != null;
				if (flag3)
				{
					transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
					{
						a3_insideui_fb.instance.needkill.ToString()
					}) + "/" + this.killsome[1];
				}
			}
			else
			{
				for (int i = 1; i < a3_insideui_fb.instance.doors + 1; i++)
				{
					this.lastkillall += this.killsome[i];
				}
				bool flag4 = !this.allkill.ContainsKey(3110);
				if (flag4)
				{
					bool flag5 = a3_insideui_fb.instance.doors != 4 && a3_insideui_fb.instance.doors != 3;
					if (flag5)
					{
						bool flag6 = transform != null;
						if (flag6)
						{
							transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
							{
								(a3_insideui_fb.instance.needkill - this.lastkillall).ToString()
							}) + "/" + this.killsome[a3_insideui_fb.instance.doors + 1];
						}
					}
					else
					{
						bool flag7 = a3_insideui_fb.instance.doors == 3;
						if (flag7)
						{
							bool flag8 = transform != null;
							if (flag8)
							{
								transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
								{
									(a3_insideui_fb.instance.needkill - this.lastkillall + this.killsome[a3_insideui_fb.instance.doors]).ToString()
								}) + "/" + this.killsome[3];
							}
						}
						else
						{
							bool flag9 = a3_insideui_fb.instance.doors == 4;
							if (flag9)
							{
								bool flag10 = transform != null;
								if (flag10)
								{
									transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
									{
										(a3_insideui_fb.instance.needkill - this.lastkillall + this.killsome[a3_insideui_fb.instance.doors]).ToString()
									}) + "/" + this.killsome[4];
								}
							}
						}
					}
				}
				else
				{
					bool flag11 = a3_insideui_fb.instance.doors != 4 && a3_insideui_fb.instance.doors != 3;
					if (flag11)
					{
						bool flag12 = transform != null;
						if (flag12)
						{
							transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
							{
								(a3_insideui_fb.instance.needkill - this.lastkillall - this.allkill[3110]).ToString()
							}) + "/" + this.killsome[a3_insideui_fb.instance.doors + 1];
						}
					}
					else
					{
						bool flag13 = a3_insideui_fb.instance.doors == 3;
						if (flag13)
						{
							bool flag14 = transform != null;
							if (flag14)
							{
								transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
								{
									(a3_insideui_fb.instance.needkill - this.lastkillall + this.killsome[a3_insideui_fb.instance.doors] - this.allkill[3110]).ToString()
								}) + "/" + this.killsome[3];
							}
						}
						else
						{
							bool flag15 = a3_insideui_fb.instance.doors == 4;
							if (flag15)
							{
								bool flag16 = transform != null;
								if (flag16)
								{
									transform.GetComponent<Text>().text = ContMgr.getCont("fb_info_8", new string[]
									{
										(a3_insideui_fb.instance.needkill - this.lastkillall + this.killsome[a3_insideui_fb.instance.doors] - this.allkill[3110]).ToString()
									}) + "/" + this.killsome[4];
								}
							}
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
