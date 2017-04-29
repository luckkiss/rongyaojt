using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_legion_diary : a3BaseLegion
	{
		public a3_legion_diary(BaseShejiao win, string pathStr) : base(win, pathStr)
		{
		}

		public override void onShowed()
		{
			base.onShowed();
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(15u, new Action<GameEvent>(this.RefreshDiary));
			BaseProxy<A3_LegionProxy>.getInstance().SendGetDiary();
		}

		public override void onClose()
		{
			base.onClose();
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(15u, new Action<GameEvent>(this.RefreshDiary));
		}

		private void RefreshDiary(GameEvent e)
		{
			GameObject gameObjectByPath = base.getGameObjectByPath("cells/scroll/0");
			Transform transformByPath = base.getTransformByPath("cells/scroll/content");
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
			Variant logdata = ModelBase<A3_LegionModel>.getInstance().logdata;
			bool flag2 = logdata == null || !logdata.ContainsKey("clanlog_list");
			if (!flag2)
			{
				Variant variant = logdata["clanlog_list"];
				List<Variant> list = new List<Variant>(variant._arr);
				list.Reverse();
				foreach (Variant current in list)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(gameObjectByPath);
					gameObject.SetActive(true);
					gameObject.transform.SetParent(transformByPath);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
					Text component = gameObject.transform.FindChild("text").GetComponent<Text>();
					int num = current["logtp"];
					Variant variant2 = current["log"];
					string item = string.Empty;
					string item2 = string.Empty;
					string item3 = string.Empty;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					int num7 = 0;
					bool flag3 = variant2.ContainsKey("name");
					if (flag3)
					{
						item = variant2["name"];
					}
					bool flag4 = variant2.ContainsKey("tar_name");
					if (flag4)
					{
						item3 = variant2["tar_name"];
					}
					bool flag5 = variant2.ContainsKey("clanc");
					if (flag5)
					{
						num4 = variant2["clanc"];
					}
					bool flag6 = variant2.ContainsKey("oldclanc");
					if (flag6)
					{
						num3 = variant2["oldclanc"];
					}
					bool flag7 = variant2.ContainsKey("money");
					if (flag7)
					{
						num5 = variant2["money"];
					}
					bool flag8 = variant2.ContainsKey("name");
					if (flag8)
					{
						item2 = variant2["name"];
					}
					bool flag9 = variant2.ContainsKey("guard_time");
					if (flag9)
					{
						num2 = variant2["guard_time"];
					}
					bool flag10 = variant2.ContainsKey("repair_cost");
					if (flag10)
					{
						num7 = variant2["repair_cost"];
					}
					bool flag11 = variant2.ContainsKey("clan_lvl");
					if (flag11)
					{
						num6 = variant2["clan_lvl"];
					}
					switch (num)
					{
					case 1:
					{
						string item4 = string.Empty;
						bool flag12 = num4 < num3;
						if (flag12)
						{
							item4 = "降级";
						}
						else
						{
							item4 = "升级";
						}
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							item3,
							item,
							item4,
							ModelBase<A3_LegionModel>.getInstance().GetClancToName(num4)
						});
						break;
					}
					case 2:
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							item,
							ModelBase<A3_LegionModel>.getInstance().myLegion.lvl.ToString()
						});
						break;
					case 3:
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							A3_LegionModel.GetCarr(ModelBase<PlayerModel>.getInstance().profession),
							item,
							num5.ToString()
						});
						break;
					case 4:
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							item2
						});
						break;
					case 5:
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							item2
						});
						break;
					case 6:
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							item3,
							item
						});
						break;
					case 7:
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							item
						});
						break;
					case 8:
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							item
						});
						break;
					case 9:
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							item3,
							item
						});
						break;
					case 10:
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							item,
							item3
						});
						break;
					case 11:
					{
						bool flag13 = num6 <= 1;
						if (flag13)
						{
							component.text = ContMgr.getCont("clan_log_" + num, new List<string>
							{
								(4 - num2).ToString()
							});
						}
						else
						{
							component.text = ContMgr.getCont("clan_log_12", new List<string>
							{
								(4 - num2).ToString(),
								(num6 - 1).ToString()
							});
						}
						break;
					}
					case 12:
						component.text = ContMgr.getCont("clan_log_14", new List<string>
						{
							num6.ToString()
						});
						break;
					case 13:
						component.text = ContMgr.getCont("clan_log_" + num, new List<string>
						{
							num7.ToString()
						});
						break;
					}
				}
				transformByPath.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, transformByPath.GetComponent<GridLayoutGroup>().cellSize.y * (float)variant._arr.Count);
			}
		}
	}
}
