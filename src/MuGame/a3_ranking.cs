using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_ranking : Window
	{
		private TabControl tabCtrl1;

		private int tabIdx;

		private GameObject[] rank_tabs;

		private List<GameObject> tab = new List<GameObject>();

		private Transform zhanliCon;

		private Transform lvlCon;

		private Transform chiBangCon;

		private Transform junTuanCon;

		private Transform summonCon;

		private Text time_text;

		private GameObject isthis;

		public GameObject Toback = null;

		private GameObject tip1;

		public static a3_ranking instan;

		public static a3_ranking isshow;

		private Vector3 v = default(Vector3);

		private uint cuiId = 0u;

		private string cuiName = "";

		private int cuicarr = 0;

		private int cuiwingstage = 0;

		private int cuiwinglvl = 0;

		public override void init()
		{
			a3_ranking.instan = this;
			this.rank_tabs = new GameObject[5];
			this.rank_tabs[0] = base.transform.FindChild("rank_tab1").gameObject;
			this.rank_tabs[1] = base.transform.FindChild("rank_tab2").gameObject;
			this.rank_tabs[2] = base.transform.FindChild("rank_tab3").gameObject;
			this.rank_tabs[3] = base.transform.FindChild("rank_tab4").gameObject;
			this.rank_tabs[4] = base.transform.FindChild("rank_tab5").gameObject;
			this.zhanliCon = this.rank_tabs[0].transform.FindChild("panel/scroll_rect/contain");
			this.lvlCon = this.rank_tabs[1].transform.FindChild("panel/scroll_rect/contain");
			this.chiBangCon = this.rank_tabs[2].transform.FindChild("panel/scroll_rect/contain");
			this.junTuanCon = this.rank_tabs[3].transform.FindChild("panel/scroll_rect/contain");
			this.summonCon = this.rank_tabs[4].transform.FindChild("panel/scroll_rect/contain");
			this.tip1 = base.transform.FindChild("tip").gameObject;
			new BaseButton(this.tip1.transform.FindChild("close"), 1, 1).onClick = delegate(GameObject go)
			{
				this.tip1.SetActive(false);
			};
			this.time_text = base.transform.FindChild("time").GetComponent<Text>();
			this.isthis = base.transform.FindChild("this").gameObject;
			for (int i = 0; i < base.transform.FindChild("panelTab2/con").childCount; i++)
			{
				this.tab.Add(base.transform.FindChild("panelTab2/con").GetChild(i).gameObject);
			}
			for (int j = 0; j < this.tab.Count; j++)
			{
				int tag = j;
				new BaseButton(this.tab[j].transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.onTab(tag);
				};
			}
			new BaseButton(base.transform.FindChild("btn_close"), 1, 1).onClick = new Action<GameObject>(this.onClose);
			this.setTip_1_btn();
		}

		public override void onShowed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			GRMap.GAME_CAMERA.SetActive(false);
			this.onTab(0);
			a3_ranking.isshow = this;
			this.tip1.SetActive(false);
			BaseProxy<a3_rankingProxy>.getInstance().getTime();
			this.isOpen();
		}

		private void isOpen()
		{
			base.transform.FindChild("panelTab2/con/4").gameObject.SetActive(false);
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.SUMMON_MONSTER, false);
			if (flag)
			{
				base.transform.FindChild("panelTab2/con/4").gameObject.SetActive(true);
			}
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
			a3_ranking.isshow = null;
		}

		private void onTab(int tag)
		{
			this.tabIdx = tag;
			this.isthis.SetActive(false);
			this.isthis.transform.SetParent(base.transform, false);
			this.rank_tabs[0].SetActive(false);
			this.rank_tabs[1].SetActive(false);
			this.rank_tabs[2].SetActive(false);
			this.rank_tabs[3].SetActive(false);
			this.rank_tabs[4].SetActive(false);
			for (int i = 0; i < this.tab.Count; i++)
			{
				this.tab[i].GetComponent<Button>().interactable = true;
			}
			this.tab[this.tabIdx].GetComponent<Button>().interactable = false;
			this.rank_tabs[this.tabIdx].SetActive(true);
			switch (this.tabIdx)
			{
			case 0:
			{
				bool zhanli_frist = ModelBase<a3_rankingModel>.getInstance().zhanli_frist;
				if (zhanli_frist)
				{
					for (int j = 0; j < this.zhanliCon.childCount; j++)
					{
						UnityEngine.Object.Destroy(this.zhanliCon.GetChild(j).gameObject);
					}
					for (int k = 1; k <= 5; k++)
					{
						BaseProxy<a3_rankingProxy>.getInstance().send_Getinfo(1u, (uint)k, 1u);
					}
					ModelBase<a3_rankingModel>.getInstance().zhanli_frist = false;
				}
				else
				{
					this.zhanliCon.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.zhanliCon.GetComponent<RectTransform>().anchoredPosition.x, 0f);
				}
				break;
			}
			case 1:
			{
				bool lvl_frist = ModelBase<a3_rankingModel>.getInstance().lvl_frist;
				if (lvl_frist)
				{
					for (int l = 0; l < this.lvlCon.childCount; l++)
					{
						UnityEngine.Object.Destroy(this.lvlCon.GetChild(l).gameObject);
					}
					for (int m = 1; m <= 5; m++)
					{
						BaseProxy<a3_rankingProxy>.getInstance().send_Getinfo(2u, (uint)m, 1u);
					}
					ModelBase<a3_rankingModel>.getInstance().lvl_frist = false;
				}
				else
				{
					this.lvlCon.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.lvlCon.GetComponent<RectTransform>().anchoredPosition.x, 0f);
				}
				break;
			}
			case 2:
			{
				bool chibang_frist = ModelBase<a3_rankingModel>.getInstance().chibang_frist;
				if (chibang_frist)
				{
					for (int n = 0; n < this.chiBangCon.childCount; n++)
					{
						UnityEngine.Object.Destroy(this.chiBangCon.GetChild(n).gameObject);
					}
					for (int num = 1; num <= 5; num++)
					{
						BaseProxy<a3_rankingProxy>.getInstance().send_Getinfo(3u, (uint)num, 1u);
					}
					ModelBase<a3_rankingModel>.getInstance().chibang_frist = false;
				}
				else
				{
					this.chiBangCon.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.chiBangCon.GetComponent<RectTransform>().anchoredPosition.x, 0f);
				}
				break;
			}
			case 3:
			{
				bool juntuan_frist = ModelBase<a3_rankingModel>.getInstance().juntuan_frist;
				if (juntuan_frist)
				{
					for (int num2 = 0; num2 < this.junTuanCon.childCount; num2++)
					{
						UnityEngine.Object.Destroy(this.junTuanCon.GetChild(num2).gameObject);
					}
					for (int num3 = 1; num3 <= 5; num3++)
					{
						BaseProxy<a3_rankingProxy>.getInstance().send_Getinfo(4u, (uint)num3, 1u);
					}
					ModelBase<a3_rankingModel>.getInstance().juntuan_frist = false;
				}
				else
				{
					this.junTuanCon.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.junTuanCon.GetComponent<RectTransform>().anchoredPosition.x, 0f);
				}
				break;
			}
			case 4:
			{
				bool summon_frist = ModelBase<a3_rankingModel>.getInstance().summon_frist;
				if (summon_frist)
				{
					for (int num4 = 0; num4 < this.summonCon.childCount; num4++)
					{
						UnityEngine.Object.Destroy(this.summonCon.GetChild(num4).gameObject);
					}
					for (int num5 = 1; num5 <= 5; num5++)
					{
						BaseProxy<a3_rankingProxy>.getInstance().send_Getinfo(5u, (uint)num5, 1u);
					}
					ModelBase<a3_rankingModel>.getInstance().summon_frist = false;
				}
				else
				{
					this.summonCon.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.summonCon.GetComponent<RectTransform>().anchoredPosition.x, 0f);
				}
				break;
			}
			}
		}

		public void Getinfo_panel(List<RankingData> l, int type)
		{
			switch (type)
			{
			case 1:
				this.addrank_zhanli(l);
				break;
			case 2:
				this.addrank_lvl(l);
				break;
			case 3:
				this.addrank_chibang(l);
				break;
			case 4:
				this.addrank_juntuan(l);
				break;
			case 5:
				this.addrank_summon(l);
				break;
			}
		}

		public void refresh_myRank(int type, int rank)
		{
			switch (type)
			{
			case 1:
				this.rank_tabs[type - 1].transform.FindChild("myrank").GetComponent<Text>().text = "我的排名: " + rank;
				break;
			case 2:
			case 3:
			case 4:
			case 5:
			{
				bool flag = rank <= 0 || rank > 100;
				if (flag)
				{
					this.rank_tabs[type - 1].transform.FindChild("myrank").GetComponent<Text>().text = "未上榜";
				}
				else
				{
					this.rank_tabs[type - 1].transform.FindChild("myrank").GetComponent<Text>().text = "我的排名: " + rank;
				}
				break;
			}
			}
		}

		private void addrank_zhanli(List<RankingData> data)
		{
			GameObject gameObject = this.rank_tabs[0].transform.FindChild("panel/scroll_rect/item_zhanli").gameObject;
			foreach (RankingData current in data)
			{
				GameObject clon = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				clon.SetActive(true);
				clon.transform.SetParent(this.zhanliCon, false);
				clon.transform.FindChild("1/Text").GetComponent<Text>().text = "第" + current.rank + "名";
				clon.transform.FindChild("2/Text").GetComponent<Text>().text = current.name;
				bool flag = current.viplvl <= 0;
				if (flag)
				{
					clon.transform.FindChild("2/vip").gameObject.SetActive(false);
				}
				else
				{
					clon.transform.FindChild("2/vip").gameObject.SetActive(true);
					clon.transform.FindChild("2/vip").GetComponent<Image>().sprite = (Resources.Load("icon/vip/" + current.viplvl, typeof(Sprite)) as Sprite);
				}
				clon.transform.FindChild("3/Text").GetComponent<Text>().text = string.Concat(new object[]
				{
					current.zhuan,
					"转",
					current.lvl,
					"级"
				});
				Text arg_1F3_0 = clon.transform.FindChild("4/Text").GetComponent<Text>();
				uint combpt = current.combpt;
				arg_1F3_0.text = combpt.ToString();
				clon.transform.SetAsFirstSibling();
				uint cid = current.cid;
				string n = current.name;
				new BaseButton(clon.transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.isthis.SetActive(true);
					this.isthis.transform.SetParent(clon.transform, false);
					this.v = Input.mousePosition;
					this.tip1.SetActive(true);
					this.tip1.transform.FindChild("look").gameObject.SetActive(true);
					this.tip1.transform.FindChild("looksum").gameObject.SetActive(false);
					this.tip1.transform.FindChild("lookWing").gameObject.SetActive(false);
					this.tip1.transform.position = new Vector3(this.tip1.transform.position.x, this.v.y, this.tip1.transform.position.z);
					this.cuiId = cid;
					this.cuiName = n;
				};
			}
			this.zhanliCon.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.zhanliCon.GetComponent<RectTransform>().anchoredPosition.x, 0f);
		}

		private void addrank_lvl(List<RankingData> data)
		{
			GameObject gameObject = this.rank_tabs[1].transform.FindChild("panel/scroll_rect/item_lvl").gameObject;
			foreach (RankingData current in data)
			{
				GameObject clon = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				clon.SetActive(true);
				clon.transform.SetParent(this.lvlCon, false);
				clon.transform.FindChild("1/Text").GetComponent<Text>().text = "第" + current.rank + "名";
				clon.transform.FindChild("2/Text").GetComponent<Text>().text = current.name;
				bool flag = current.viplvl <= 0;
				if (flag)
				{
					clon.transform.FindChild("2/vip").gameObject.SetActive(false);
				}
				else
				{
					clon.transform.FindChild("2/vip").gameObject.SetActive(true);
					clon.transform.FindChild("2/vip").GetComponent<Image>().sprite = (Resources.Load("icon/vip/" + current.viplvl, typeof(Sprite)) as Sprite);
				}
				clon.transform.FindChild("3/Text").GetComponent<Text>().text = string.Concat(new object[]
				{
					current.zhuan,
					"转",
					current.lvl,
					"级"
				});
				Text arg_1F3_0 = clon.transform.FindChild("4/Text").GetComponent<Text>();
				uint combpt = current.combpt;
				arg_1F3_0.text = combpt.ToString();
				clon.transform.SetAsFirstSibling();
				uint cid = current.cid;
				string n = current.name;
				new BaseButton(clon.transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.isthis.SetActive(true);
					this.isthis.transform.SetParent(clon.transform, false);
					this.v = Input.mousePosition;
					this.tip1.SetActive(true);
					this.tip1.transform.FindChild("look").gameObject.SetActive(true);
					this.tip1.transform.FindChild("looksum").gameObject.SetActive(false);
					this.tip1.transform.FindChild("lookWing").gameObject.SetActive(false);
					this.tip1.transform.position = new Vector3(this.tip1.transform.position.x, this.v.y, this.tip1.transform.position.z);
					this.cuiId = cid;
					this.cuiName = n;
				};
			}
			this.lvlCon.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.lvlCon.GetComponent<RectTransform>().anchoredPosition.x, 0f);
		}

		private void addrank_chibang(List<RankingData> data)
		{
			GameObject gameObject = this.rank_tabs[2].transform.FindChild("panel/scroll_rect/item_chibang").gameObject;
			foreach (RankingData current in data)
			{
				GameObject clon = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				clon.SetActive(true);
				clon.transform.SetParent(this.chiBangCon, false);
				clon.transform.FindChild("1/Text").GetComponent<Text>().text = "第" + current.rank + "名";
				clon.transform.FindChild("2/Text").GetComponent<Text>().text = current.name;
				bool flag = current.viplvl <= 0;
				if (flag)
				{
					clon.transform.FindChild("2/vip").gameObject.SetActive(false);
				}
				else
				{
					clon.transform.FindChild("2/vip").gameObject.SetActive(true);
					clon.transform.FindChild("2/vip").GetComponent<Image>().sprite = (Resources.Load("icon/vip/" + current.viplvl, typeof(Sprite)) as Sprite);
				}
				SXML sXML = XMLMgr.instance.GetSXML("wings.wing", "career==" + current.carr);
				SXML node = sXML.GetNode("wing_stage", "stage_id==" + current.stage);
				clon.transform.FindChild("3/Text").GetComponent<Text>().text = node.getString("name");
				clon.transform.FindChild("4/Text").GetComponent<Text>().text = string.Concat(new object[]
				{
					current.stage,
					"阶",
					current.flylvl,
					"星"
				});
				clon.transform.SetAsFirstSibling();
				uint carr = current.carr;
				int wingstage = current.stage;
				int winglvl = current.flylvl;
				new BaseButton(clon.transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.isthis.SetActive(true);
					this.isthis.transform.SetParent(clon.transform, false);
					this.v = Input.mousePosition;
					this.tip1.SetActive(true);
					this.tip1.transform.FindChild("look").gameObject.SetActive(false);
					this.tip1.transform.FindChild("looksum").gameObject.SetActive(false);
					this.tip1.transform.FindChild("lookWing").gameObject.SetActive(true);
					this.tip1.transform.position = new Vector3(this.tip1.transform.position.x, this.v.y, this.tip1.transform.position.z);
					this.cuicarr = (int)carr;
					this.cuiwingstage = wingstage;
					this.cuiwinglvl = winglvl;
				};
			}
			this.chiBangCon.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.chiBangCon.GetComponent<RectTransform>().anchoredPosition.x, 0f);
		}

		private void addrank_juntuan(List<RankingData> data)
		{
			GameObject gameObject = this.rank_tabs[3].transform.FindChild("panel/scroll_rect/item_juntuan").gameObject;
			foreach (RankingData current in data)
			{
				GameObject clon = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				clon.SetActive(true);
				clon.transform.SetParent(this.junTuanCon, false);
				clon.transform.FindChild("1/Text").GetComponent<Text>().text = "第" + current.rank + "名";
				clon.transform.FindChild("2/Text").GetComponent<Text>().text = current.jt_name;
				Text arg_FB_0 = clon.transform.FindChild("3/Text").GetComponent<Text>();
				int jt_combpt = current.jt_combpt;
				arg_FB_0.text = jt_combpt.ToString();
				clon.transform.FindChild("4/Text").GetComponent<Text>().text = current.jt_lvl + "级";
				clon.transform.SetAsFirstSibling();
				new BaseButton(clon.transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.isthis.SetActive(true);
					this.isthis.transform.SetParent(clon.transform, false);
				};
			}
			this.junTuanCon.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.junTuanCon.GetComponent<RectTransform>().anchoredPosition.x, 0f);
		}

		private void addrank_summon(List<RankingData> data)
		{
			GameObject gameObject = this.rank_tabs[4].transform.FindChild("panel/scroll_rect/item_summon").gameObject;
			foreach (RankingData current in data)
			{
				GameObject clon = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				clon.SetActive(true);
				clon.transform.SetParent(this.summonCon, false);
				clon.transform.FindChild("1/Text").GetComponent<Text>().text = "第" + current.rank + "名";
				clon.transform.FindChild("2/Text").GetComponent<Text>().text = current.name;
				bool flag = current.viplvl <= 0;
				if (flag)
				{
					clon.transform.FindChild("2/vip").gameObject.SetActive(false);
				}
				else
				{
					clon.transform.FindChild("2/vip").gameObject.SetActive(true);
					clon.transform.FindChild("2/vip").GetComponent<Image>().sprite = (Resources.Load("icon/vip/" + current.viplvl, typeof(Sprite)) as Sprite);
				}
				SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + current.zhs_tpid);
				clon.transform.FindChild("3/Text").GetComponent<Text>().text = sXML.getString("item_name");
				clon.transform.FindChild("4/Text").GetComponent<Text>().text = current.zhs_lvl + "级";
				Text arg_223_0 = clon.transform.FindChild("5/Text").GetComponent<Text>();
				int zhs_combpt = current.zhs_combpt;
				arg_223_0.text = zhs_combpt.ToString();
				clon.transform.SetAsFirstSibling();
				uint cid = (uint)current.zhs_id;
				string n = current.name;
				new BaseButton(clon.transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.isthis.SetActive(true);
					this.isthis.transform.SetParent(clon.transform, false);
					this.v = Input.mousePosition;
					this.tip1.SetActive(true);
					this.tip1.transform.FindChild("look").gameObject.SetActive(false);
					this.tip1.transform.FindChild("looksum").gameObject.SetActive(true);
					this.tip1.transform.FindChild("lookWing").gameObject.SetActive(false);
					this.tip1.transform.position = new Vector3(this.tip1.transform.position.x, this.v.y, this.tip1.transform.position.z);
					this.cuiId = cid;
					this.cuiName = n;
				};
			}
			this.summonCon.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.summonCon.GetComponent<RectTransform>().anchoredPosition.x, 0f);
		}

		private void setTip_1_btn()
		{
			new BaseButton(this.tip1.transform.FindChild("looksum"), 1, 1).onClick = delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.cuiId);
				this.Toback = base.gameObject;
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SUMMONINFO, arrayList, false);
				this.tip1.SetActive(false);
				base.gameObject.SetActive(false);
			};
			new BaseButton(this.tip1.transform.FindChild("look"), 1, 1).onClick = delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.cuiId);
				this.Toback = base.gameObject;
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_TARGETINFO, arrayList, false);
				this.tip1.SetActive(false);
				base.gameObject.SetActive(false);
			};
			new BaseButton(this.tip1.transform.FindChild("lookWing"), 1, 1).onClick = delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.cuicarr);
				arrayList.Add(this.cuiwingstage);
				arrayList.Add(this.cuiwinglvl);
				this.Toback = base.gameObject;
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_WINGINFO, arrayList, false);
				this.tip1.SetActive(false);
				base.gameObject.SetActive(false);
			};
		}

		public void setTime(float time)
		{
			TimeSpan timeSpan = new TimeSpan(0, 0, (int)time);
			this.time_text.text = string.Concat(new object[]
			{
				timeSpan.Hours,
				"小时",
				timeSpan.Minutes,
				"分钟",
				timeSpan.Seconds,
				"秒"
			});
		}

		private void Update()
		{
		}

		private void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_RANKING);
		}
	}
}
