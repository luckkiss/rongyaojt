using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class IconHintMgr
	{
		public static int TYPE_ROLE = 1;

		public static int TYPE_SHEJIAO = 2;

		public static int TYPE_ACHIEVEMENT = 3;

		public static int TYPE_SKILL = 4;

		public static int TYPE_YGYUWU = 5;

		public static int TYPE_EQUIP = 6;

		public static int TYPE_WING_SKIN = 7;

		public static int TYPE_BAG = 8;

		public static int TYPE_MAIL = 9;

		public static int TYPE_QIANGHUA = 10;

		public static int TYPE_JINGLIAN = 11;

		public static int TYPE_ZHUIJIA = 12;

		public static int TYPE_XIANGQIAN = 13;

		private Dictionary<int, GameObject> dic_hints = new Dictionary<int, GameObject>();

		private Dictionary<int, List<GameObject>> dic_hints_havenum = new Dictionary<int, List<GameObject>>();

		public bool inituiisok = false;

		public List<hintinfo> lst_info = new List<hintinfo>();

		public static IconHintMgr _instance;

		public void addHint(Transform tsm, int type)
		{
			GameObject gameObject = new GameObject("hide");
			string path = "icon/hint/tips";
			this.common(tsm, gameObject, path, -22f, -22f, 1f);
			this.dic_hints[type] = gameObject;
		}

		public void addHint_havenum(Transform tsm, int type)
		{
			List<GameObject> list = new List<GameObject>();
			GameObject gameObject = new GameObject("hide");
			string path = "icon/hint/sign_bagremind";
			this.common(tsm, gameObject, path, -22f, -20f, 0.7f);
			list.Add(gameObject);
			GameObject gameObject2 = new GameObject("num");
			Text text = gameObject2.AddComponent<Text>();
			gameObject2.AddComponent<Shadow>();
			RectTransform component = gameObject2.transform.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(28f, 25.7f);
			component.localPosition = new Vector3(0f, 0f, 0f);
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			text.font = (Resources.Load("icon/hint/MicrosoftYH22438", typeof(Font)) as Font);
			text.fontStyle = FontStyle.Normal;
			text.fontSize = 18;
			text.alignment = TextAnchor.MiddleCenter;
			text.color = new Color(1f, 0.92f, 0.54f, 1f);
			gameObject2.transform.SetParent(gameObject.transform, false);
			GameObject gameObject3 = new GameObject("hide");
			string path2 = "icon/hint/sign_bagremind3";
			this.common(tsm, gameObject3, path2, -22f, -20f, 0.7f);
			list.Add(gameObject3);
			this.dic_hints_havenum[type] = list;
		}

		public void addHint_equip(Transform tsm, int type)
		{
			GameObject gameObject = new GameObject("hide");
			string path = "icon/hint/tips";
			this.common(tsm, gameObject, path, -125f, -38f, 1f);
			this.dic_hints[type] = gameObject;
		}

		private void common(Transform obj_tsm, GameObject obj, string path, float x, float y, float scale)
		{
			Image image = obj.AddComponent<Image>();
			RectTransform component = image.transform.GetComponent<RectTransform>();
			component.anchorMax = new Vector2(1f, 1f);
			component.anchorMin = new Vector2(1f, 1f);
			component.localPosition = new Vector3(x, y, 0f);
			component.transform.localScale = new Vector3(scale, scale, scale);
			image.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			image.transform.SetParent(obj_tsm, false);
			image.SetNativeSize();
			obj.SetActive(false);
		}

		public void showHint(int type, int FunctionOpenMgr_id = -1, int num = -1, bool isman = false)
		{
			bool flag = !this.inituiisok;
			if (flag)
			{
				hintinfo item = default(hintinfo);
				item.type = type;
				item.FunctionOpenMgr_id = FunctionOpenMgr_id;
				item.num = num;
				item.isman = isman;
				this.lst_info.Add(item);
			}
			else
			{
				bool flag2 = FunctionOpenMgr_id > 0 && !FunctionOpenMgr.instance.Check(FunctionOpenMgr_id, false);
				if (!flag2)
				{
					bool flag3 = num > -1;
					if (flag3)
					{
						bool flag4 = this.dic_hints_havenum.ContainsKey(type);
						if (flag4)
						{
							this.dic_hints_havenum[type][1].SetActive(isman);
							this.dic_hints_havenum[type][0].SetActive(!isman);
							bool flag5 = !isman;
							if (flag5)
							{
								this.dic_hints_havenum[type][0].transform.GetChild(0).GetComponent<Text>().text = num.ToString();
							}
						}
					}
					else
					{
						bool flag6 = this.dic_hints.ContainsKey(type);
						if (flag6)
						{
							this.dic_hints[type].SetActive(true);
						}
					}
				}
			}
		}

		public void closeHint(int type, bool num = false, bool isman = false)
		{
			if (num)
			{
				bool flag = this.dic_hints_havenum.ContainsKey(type);
				if (flag)
				{
					this.dic_hints_havenum[type][0].SetActive(false);
				}
			}
			if (isman)
			{
				bool flag2 = this.dic_hints_havenum.ContainsKey(type);
				if (flag2)
				{
					this.dic_hints_havenum[type][1].SetActive(false);
				}
			}
			bool flag3 = !num && !isman;
			if (flag3)
			{
				bool flag4 = this.dic_hints.ContainsKey(type);
				if (flag4)
				{
					this.dic_hints[type].SetActive(false);
				}
			}
		}

		public void initui()
		{
			bool flag = this.lst_info.Count <= 0;
			if (!flag)
			{
				for (int i = 0; i < this.lst_info.Count; i++)
				{
					this.showHint(this.lst_info[i].type, this.lst_info[i].FunctionOpenMgr_id, this.lst_info[i].num, this.lst_info[i].isman);
				}
			}
		}

		public static IconHintMgr getInsatnce()
		{
			bool flag = IconHintMgr._instance == null;
			if (flag)
			{
				IconHintMgr._instance = new IconHintMgr();
			}
			return IconHintMgr._instance;
		}
	}
}
