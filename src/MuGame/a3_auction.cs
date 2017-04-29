using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_auction : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_auction.<>c <>9 = new a3_auction.<>c();

			public static Action<GameObject> <>9__11_0;

			internal void <SetCop>b__11_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_AUCTION);
			}
		}

		private a3BaseAuction _currentAuction = null;

		private Dictionary<string, a3BaseAuction> _activies = new Dictionary<string, a3BaseAuction>();

		private TabControl _tabCtl;

		public Transform etra;

		public Transform djtip;

		private List<Transform> txetClon = new List<Transform>();

		public override void init()
		{
			this.SetCop();
			this._activies["buy"] = new a3_auction_buy(this, "contents/buy");
			this._activies["sell"] = new a3_auction_sell(this, "contents/sell");
			this._activies["rack"] = new a3_auction_rack(this, "contents/rack");
			this._activies["get"] = new a3_auction_get(this, "contents/get");
			this._tabCtl = this.InitLayout();
			for (int i = 0; i <= 6; i++)
			{
				this.txetClon.Add(base.transform.FindChild("eqtip/info/attr_scroll/scroll/text" + i));
			}
		}

		public override void onShowed()
		{
			bool flag = this._currentAuction != null;
			if (flag)
			{
				this._currentAuction.onShowed();
			}
			bool flag2 = this.uiData != null;
			if (flag2)
			{
				int index = (int)this.uiData[0];
				this._tabCtl.setSelectedIndex(index, false);
			}
			BaseProxy<A3_AuctionProxy>.getInstance().SendSearchMsg(0u, 0u, 0u, 0u, 0u, 0u, null);
			GRMap.GAME_CAMERA.SetActive(false);
		}

		public override void onClosed()
		{
			bool flag = this._currentAuction != null;
			if (flag)
			{
				this._currentAuction.onClose();
			}
			GRMap.GAME_CAMERA.SetActive(true);
		}

		public void ShowDJTip(a3_BagItemData data, bool puton = true, bool buy = false)
		{
			Transform transform = this.djtip.FindChild("info");
			if (puton)
			{
				transform.FindChild("put").gameObject.SetActive(true);
			}
			else
			{
				transform.FindChild("put").gameObject.SetActive(false);
			}
			if (buy)
			{
				transform.FindChild("buy").gameObject.SetActive(true);
			}
			else
			{
				transform.FindChild("buy").gameObject.SetActive(false);
			}
			for (int i = 1; i <= 5; i++)
			{
				bool flag = i == data.confdata.quality;
				if (flag)
				{
					transform.FindChild("ig_bg/ig_" + i).gameObject.SetActive(true);
				}
				else
				{
					transform.FindChild("ig_bg/ig_" + i).gameObject.SetActive(false);
				}
			}
			transform.FindChild("money").GetComponent<Text>().text = "价值：" + data.confdata.value;
			transform.FindChild("name").GetComponent<Text>().text = data.confdata.item_name;
			transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(data.confdata.quality);
			transform.FindChild("transaction").GetComponent<Text>().text = "可交易";
			transform.FindChild("profession").GetComponent<Text>().text = "材料";
			SXML itemXml = ModelBase<a3_BagModel>.getInstance().getItemXml((int)data.tpid);
			transform.FindChild("desc").GetComponent<Text>().text = StringUtils.formatText(itemXml.getString("desc"));
			Transform transform2 = transform.FindChild("icon");
			bool flag2 = transform2.childCount > 0;
			if (flag2)
			{
				UnityEngine.Object.Destroy(transform2.GetChild(0).gameObject);
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data.tpid, false, -1, 1f, false, -1, 0, false, false, false, false);
			gameObject.transform.SetParent(transform2, false);
		}

		public void ShowEPTip(a3_BagItemData data, bool puton = true, bool buy = false)
		{
			Transform transform = this.etra.FindChild("info");
			if (puton)
			{
				transform.FindChild("put").gameObject.SetActive(true);
			}
			else
			{
				transform.FindChild("put").gameObject.SetActive(false);
			}
			if (buy)
			{
				transform.FindChild("buy").gameObject.SetActive(true);
			}
			else
			{
				transform.FindChild("buy").gameObject.SetActive(false);
			}
			for (int i = 1; i <= 5; i++)
			{
				bool flag = i == data.confdata.quality;
				if (flag)
				{
					transform.FindChild("ig_bg/ig_" + i).gameObject.SetActive(true);
				}
				else
				{
					transform.FindChild("ig_bg/ig_" + i).gameObject.SetActive(false);
				}
			}
			transform.FindChild("money").GetComponent<Text>().text = "价值：" + data.confdata.value;
			transform.FindChild("name").GetComponent<Text>().text = data.confdata.item_name;
			transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(data.confdata.quality);
			transform.FindChild("txt_value").GetComponent<Text>().text = data.equipdata.combpt.ToString();
			transform.FindChild("lv").GetComponent<Text>().text = data.equipdata.stage.ToString() + "阶" + Globle.getEquipTextByType(data.confdata.equip_type);
			bool flag2 = data.confdata.equip_type < 1;
			if (flag2)
			{
				transform.FindChild("transaction").GetComponent<Text>().text = "可交易";
			}
			else
			{
				bool flag3 = ModelBase<a3_BagModel>.getInstance().isWorked(data);
				if (flag3)
				{
					transform.FindChild("transaction").GetComponent<Text>().text = "可交易";
				}
				else
				{
					transform.FindChild("transaction").GetComponent<Text>().text = "不可交易";
				}
			}
			string text = "";
			switch (data.confdata.job_limit)
			{
			case 1:
				text = "职业：全职业";
				break;
			case 2:
				text = "职业：狂战士";
				break;
			case 3:
				text = "职业：法师";
				break;
			case 5:
				text = "职业：暗影";
				break;
			}
			transform.FindChild("profession").GetComponent<Text>().text = text;
			Transform transform2 = transform.FindChild("icon");
			bool flag4 = transform2.childCount > 0;
			if (flag4)
			{
				UnityEngine.Object.Destroy(transform2.GetChild(0).gameObject);
			}
			bool flag5 = data.confdata.equip_type > 0;
			GameObject gameObject;
			if (flag5)
			{
				gameObject = IconImageMgr.getInstance().createA3EquipIcon(data, 1f, false);
			}
			else
			{
				gameObject = IconImageMgr.getInstance().createA3ItemIcon(data.tpid, false, -1, 1f, false, -1, 0, false, false, false, false);
			}
			gameObject.transform.SetParent(transform2, false);
			gameObject.transform.FindChild("iconborder/is_upequip").gameObject.SetActive(false);
			gameObject.transform.FindChild("iconborder/is_new").gameObject.SetActive(false);
			gameObject.transform.FindChild("iconborder/equip_self").gameObject.SetActive(false);
			gameObject.transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(false);
			this.SetStar(this.etra.FindChild("info/stars"), data.equipdata.intensify_lv);
			transform.FindChild("zhufu/title").GetComponent<Text>().text = string.Concat(new object[]
			{
				"祝福",
				data.equipdata.blessing_lv,
				"（需求-",
				3 * data.equipdata.blessing_lv,
				"%）"
			});
			bool flag6 = data.confdata.equip_type <= 0;
			if (flag6)
			{
				transform.FindChild("zhufu/title").GetComponent<Text>().text = "祝福0";
				transform.FindChild("zhufu/text1").GetComponent<Text>().text = "力量0";
				transform.FindChild("zhufu/text2").GetComponent<Text>().text = "敏捷0";
				this.SetStar(this.etra.FindChild("info/stars"), 0);
				transform.FindChild("name").GetComponent<Text>().text = "";
				transform.FindChild("txt_value").GetComponent<Text>().text = "";
				transform.FindChild("lv").GetComponent<Text>().text = "0阶材料";
				transform.FindChild("profession").GetComponent<Text>().text = "材料";
				transform.FindChild("attr_scroll").gameObject.SetActive(false);
			}
			else
			{
				transform.FindChild("attr_scroll").gameObject.SetActive(true);
				this.initAtt(transform, data);
			}
		}

		private void SetCop()
		{
			BaseButton arg_32_0 = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			Action<GameObject> arg_32_1;
			if ((arg_32_1 = a3_auction.<>c.<>9__11_0) == null)
			{
				arg_32_1 = (a3_auction.<>c.<>9__11_0 = new Action<GameObject>(a3_auction.<>c.<>9.<SetCop>b__11_0));
			}
			arg_32_0.onClick = arg_32_1;
			this.etra = base.transform.FindChild("eqtip");
			this.djtip = base.transform.FindChild("djtip");
			new BaseButton(this.etra.transform.FindChild("touch"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.etra.gameObject.SetActive(false);
			};
			new BaseButton(this.etra.transform.FindChild("info/cancel"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.etra.gameObject.SetActive(false);
			};
			new BaseButton(this.djtip.transform.FindChild("touch"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.djtip.gameObject.SetActive(false);
			};
			new BaseButton(this.djtip.transform.FindChild("info/cancel"), 1, 1).onClick = delegate(GameObject GameObject)
			{
				this.djtip.gameObject.SetActive(false);
			};
		}

		private TabControl InitLayout()
		{
			TabControl tabControl = new TabControl();
			List<Transform> contents = new List<Transform>();
			Transform transform = base.getGameObjectByPath("contents").transform;
			Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform2 = componentsInChildren[i];
				bool flag = transform2.parent == transform;
				if (flag)
				{
					transform2.gameObject.SetActive(false);
					contents.Add(transform2);
				}
			}
			TabControl expr_85 = tabControl;
			expr_85.onClickHanle = (Action<TabControl>)Delegate.Combine(expr_85.onClickHanle, new Action<TabControl>(delegate(TabControl tb)
			{
				bool flag2 = !this._activies.ContainsKey(contents[tb.getSeletedIndex()].name);
				if (flag2)
				{
					Debug.Log("没有界面配置");
					bool flag3 = this._activies.Count > 0;
					if (flag3)
					{
						tb.setSelectedIndex(tb.getIndexByName(new List<a3BaseAuction>(this._activies.Values)[0].pathStrName), false);
					}
				}
				else
				{
					for (int j = 0; j < contents.Count; j++)
					{
						bool flag4 = j != tb.getSeletedIndex();
						if (flag4)
						{
							contents[j].gameObject.SetActive(false);
						}
						else
						{
							contents[j].gameObject.SetActive(true);
						}
					}
					bool flag5 = this._currentAuction != null;
					if (flag5)
					{
						this._currentAuction.onClose();
					}
					bool flag6 = this._currentAuction != null && this._activies.ContainsKey(contents[tb.getSeletedIndex()].name);
					if (flag6)
					{
						this._currentAuction = this._activies[contents[tb.getSeletedIndex()].name];
						this._currentAuction.onShowed();
					}
					else
					{
						this._currentAuction = this._activies[contents[tb.getSeletedIndex()].name];
					}
				}
			}));
			tabControl.create(base.getGameObjectByPath("tabs"), base.gameObject, 0, 0, false);
			return tabControl;
		}

		private void initAtt(Transform info, a3_BagItemData equip_data)
		{
			int num = 0;
			Transform transform = info.transform.FindChild("attr_scroll/scroll/contain");
			for (int i = 0; i < transform.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
			}
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + equip_data.tpid);
			SXML node = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + equip_data.equipdata.stage).GetNode("stage_info", "itemid==" + equip_data.tpid);
			string[] array = node.getString("equip_limit1").Split(new char[]
			{
				','
			});
			string[] array2 = node.getString("equip_limit2").Split(new char[]
			{
				','
			});
			int num2 = int.Parse(array[1]) * (100 - 3 * equip_data.equipdata.blessing_lv) / 100;
			int num3 = int.Parse(array2[1]) * (100 - 3 * equip_data.equipdata.blessing_lv) / 100;
			bool flag = num2 <= ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array[0])];
			string text;
			if (flag)
			{
				bool flag2 = num2 <= 0;
				if (flag2)
				{
					text = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(array[0])),
						" <color=#00FF00>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array[0])],
						"</color>/-"
					});
				}
				else
				{
					text = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(array[0])),
						" <color=#00FF00>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array[0])],
						"</color>/",
						num2
					});
				}
			}
			else
			{
				bool flag3 = num2 <= 0;
				if (flag3)
				{
					text = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(array[0])),
						" <color=#f90e0e>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array[0])],
						"</color>/-"
					});
				}
				else
				{
					text = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(array[0])),
						" <color=#f90e0e>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array[0])],
						"</color>/",
						num2
					});
				}
			}
			bool flag4 = num3 <= ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array2[0])];
			string text2;
			if (flag4)
			{
				bool flag5 = num3 <= 0;
				if (flag5)
				{
					text2 = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(array2[0])),
						" <color=#00FF00>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array2[0])],
						"</color>/-"
					});
				}
				else
				{
					text2 = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(array2[0])),
						" <color=#00FF00>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array2[0])],
						"</color>/",
						num3
					});
				}
			}
			else
			{
				bool flag6 = num3 <= 0;
				if (flag6)
				{
					text2 = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(array2[0])),
						" <color=#f90e0e>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array2[0])],
						"</color>/-"
					});
				}
				else
				{
					text2 = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(array2[0])),
						" <color=#f90e0e>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array2[0])],
						"</color>/",
						num3
					});
				}
			}
			info.FindChild("zhufu/text1").GetComponent<Text>().text = text;
			info.FindChild("zhufu/text2").GetComponent<Text>().text = text2;
			string[] array3 = XMLMgr.instance.GetSXML("item.stage", "stage_level==0").GetNode("stage_info", "itemid==" + equip_data.tpid).getString("basic_att").Split(new char[]
			{
				','
			});
			string[] array4 = node.getString("basic_att").Split(new char[]
			{
				','
			});
			Transform transform2 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[0]).transform;
			transform2.SetParent(transform, false);
			transform2.gameObject.SetActive(true);
			transform2.GetComponent<Text>().text = "[基础属性]";
			num++;
			Transform transform3 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[1]).transform;
			transform3.SetParent(transform, false);
			transform3.gameObject.SetActive(true);
			num++;
			bool flag7 = equip_data.equipdata.intensify_lv > 0;
			if (flag7)
			{
				SXML node2 = XMLMgr.instance.GetSXML("item.intensify", "intensify_level==" + equip_data.equipdata.intensify_lv).GetNode("intensify_info", "itemid==" + equip_data.tpid);
				string[] array5 = node2.getString("intensify_att").Split(new char[]
				{
					','
				});
				bool flag8 = array5.Length > 1;
				if (flag8)
				{
					int num4 = int.Parse(array5[0]) * equip_data.equipdata.intensify_lv + (int.Parse(array4[0]) - int.Parse(array3[0]));
					int num5 = int.Parse(array5[1]) * equip_data.equipdata.intensify_lv + (int.Parse(array4[1]) - int.Parse(array3[1]));
					transform3.FindChild("Text").GetComponent<Text>().text = string.Concat(new object[]
					{
						Globle.getAttrNameById(sXML.getInt("att_type")),
						" ",
						array3[0],
						"<color=#00FF00>（+",
						num4,
						"）</color>-",
						array3[1],
						"<color=#00FF00>（+",
						num5,
						"）</color>"
					});
				}
				else
				{
					int num6 = int.Parse(array5[0]) * equip_data.equipdata.intensify_lv + (int.Parse(array4[0]) - int.Parse(array3[0]));
					transform3.FindChild("Text").GetComponent<Text>().text = string.Concat(new object[]
					{
						Globle.getAttrNameById(sXML.getInt("att_type")),
						" ",
						array3[0],
						"<color=#00FF00>（+",
						num6,
						"）</color>"
					});
				}
			}
			else
			{
				bool flag9 = equip_data.equipdata.stage > 0;
				if (flag9)
				{
					bool flag10 = array3.Length > 1;
					if (flag10)
					{
						int num7 = int.Parse(array4[0]) - int.Parse(array3[0]);
						int num8 = int.Parse(array4[1]) - int.Parse(array3[1]);
						transform3.FindChild("Text").GetComponent<Text>().text = string.Concat(new object[]
						{
							Globle.getAttrNameById(sXML.getInt("att_type")),
							" ",
							array3[0],
							"<color=#00FF00>（+",
							num7,
							"）</color>-",
							array3[1],
							"<color=#00FF00>（+",
							num8,
							"）</color>"
						});
					}
					else
					{
						int num9 = int.Parse(array4[0]) - int.Parse(array3[0]);
						transform3.FindChild("Text").GetComponent<Text>().text = string.Concat(new object[]
						{
							Globle.getAttrNameById(sXML.getInt("att_type")),
							" ",
							array3[0],
							"<color=#00FF00>（+",
							num9,
							"）</color>"
						});
					}
				}
				else
				{
					bool flag11 = array3.Length > 1;
					if (flag11)
					{
						transform3.FindChild("Text").GetComponent<Text>().text = string.Concat(new string[]
						{
							Globle.getAttrNameById(sXML.getInt("att_type")),
							" ",
							array3[0],
							"-",
							array3[1]
						});
					}
					else
					{
						transform3.FindChild("Text").GetComponent<Text>().text = Globle.getAttrNameById(sXML.getInt("att_type")) + " " + array3[0];
					}
				}
			}
			Transform transform4 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[0]).transform;
			transform4.SetParent(transform, false);
			transform4.gameObject.SetActive(true);
			transform4.GetComponent<Text>().text = "[附加属性]";
			num++;
			int num10 = 0;
			SXML sXML2 = XMLMgr.instance.GetSXML("item.subjoin_att", "equip_level==" + equip_data.confdata.equip_level);
			foreach (int current in equip_data.equipdata.subjoin_att.Keys)
			{
				num10++;
				Transform transform5 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[2]).transform;
				transform5.SetParent(transform, false);
				transform5.gameObject.SetActive(true);
				num++;
				SXML node3 = sXML2.GetNode("subjoin_att_info", "att_type==" + current);
				Text component = transform5.FindChild("Text").GetComponent<Text>();
				component.text = Globle.getAttrAddById(current, equip_data.equipdata.subjoin_att[current], true);
				bool flag12 = equip_data.equipdata.subjoin_att[current] >= node3.getInt("max");
				if (flag12)
				{
					transform5.FindChild("max").gameObject.SetActive(true);
				}
				else
				{
					transform5.FindChild("max").gameObject.SetActive(false);
				}
			}
			Transform transform6 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[0]).transform;
			transform6.SetParent(transform, false);
			transform6.gameObject.SetActive(true);
			transform6.GetComponent<Text>().text = "[灵魂属性]";
			num++;
			Transform transform7 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[6]).transform;
			transform7.SetParent(transform, false);
			transform7.gameObject.SetActive(true);
			num++;
			bool flag13 = equip_data.equipdata.att_type > 0;
			string text3;
			if (flag13)
			{
				text3 = Globle.getAttrAddById(equip_data.equipdata.att_type, equip_data.equipdata.att_value, true);
				bool flag14 = ModelBase<a3_EquipModel>.getInstance().active_eqp.ContainsKey(equip_data.confdata.equip_type);
				if (flag14)
				{
					bool flag15 = ModelBase<a3_EquipModel>.getInstance().active_eqp[equip_data.confdata.equip_type].id == equip_data.id;
					if (flag15)
					{
						text3 = "<color=#0cf373>" + text3 + "</color>";
					}
					else
					{
						text3 = string.Concat(new string[]
						{
							"<color=#4d3d3d>",
							text3,
							"（穿戴",
							this.Getcizhui(ModelBase<a3_EquipModel>.getInstance().eqp_att_act[equip_data.equipdata.attribute]),
							"属性",
							Globle.getEquipTextByType(ModelBase<a3_EquipModel>.getInstance().eqp_type_act[equip_data.confdata.equip_type]),
							"激活）</color>"
						});
					}
				}
				else
				{
					text3 = string.Concat(new string[]
					{
						"<color=#4d3d3d>",
						text3,
						"（穿戴",
						this.Getcizhui(ModelBase<a3_EquipModel>.getInstance().eqp_att_act[equip_data.equipdata.attribute]),
						"属性",
						Globle.getEquipTextByType(ModelBase<a3_EquipModel>.getInstance().eqp_type_act[equip_data.confdata.equip_type]),
						"激活）</color>"
					});
				}
			}
			else
			{
				text3 = "<color=#4d3d3d>无</color>";
			}
			transform7.FindChild("Text").GetComponent<Text>().text = text3;
			bool flag16 = equip_data.equipdata.baoshi != null;
			if (flag16)
			{
				Transform transform8 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[0]).transform;
				transform8.SetParent(transform, false);
				transform8.gameObject.SetActive(true);
				transform8.GetComponent<Text>().text = "[宝石镶嵌]";
				num++;
				foreach (int current2 in equip_data.equipdata.baoshi.Keys)
				{
					Transform transform9 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[3]).transform;
					transform9.SetParent(transform, false);
					transform9.gameObject.SetActive(true);
					Image component2 = transform9.transform.FindChild("icon").GetComponent<Image>();
					Text component3 = transform9.FindChild("Text").GetComponent<Text>();
					bool flag17 = equip_data.equipdata.baoshi[current2] <= 0;
					if (flag17)
					{
						component2.gameObject.SetActive(false);
						component3.text = "可镶嵌";
					}
					else
					{
						SXML sXML3 = XMLMgr.instance.GetSXML("item", "");
						SXML node4 = sXML3.GetNode("item", "id==" + equip_data.equipdata.baoshi[current2]);
						string path = "icon/item/" + node4.getString("icon_file");
						component2.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
						SXML node5 = sXML3.GetNode("gem_info", "item_id==" + equip_data.equipdata.baoshi[current2]);
						List<SXML> nodeList = node5.GetNodeList("gem_add", "");
						int id = 0;
						int value = 0;
						foreach (SXML current3 in nodeList)
						{
							bool flag18 = current3.getInt("equip_type") == equip_data.confdata.equip_type;
							if (flag18)
							{
								id = current3.getInt("att_type");
								value = current3.getInt("att_value");
								break;
							}
						}
						component3.text = Globle.getAttrAddById(id, value, true);
					}
					num++;
				}
			}
			Transform transform10 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[0]).transform;
			transform10.SetParent(transform, false);
			transform10.gameObject.SetActive(true);
			transform10.GetComponent<Text>().text = "[追加属性]";
			num++;
			Transform transform11 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[1]).transform;
			transform11.SetParent(transform, false);
			transform11.gameObject.SetActive(true);
			num++;
			SXML sXML4 = XMLMgr.instance.GetSXML("item.add_att", "add_level==" + (equip_data.equipdata.add_level + 1));
			SXML sXML5 = XMLMgr.instance.GetSXML("item.item", "id==" + equip_data.tpid);
			int id2 = int.Parse(sXML5.getString("add_atttype").Split(new char[]
			{
				','
			})[0]);
			int value2 = int.Parse(sXML5.getString("add_atttype").Split(new char[]
			{
				','
			})[1]) * equip_data.equipdata.add_level;
			transform11.FindChild("Text").GetComponent<Text>().text = Globle.getAttrAddById(id2, value2, true);
			float y = transform.GetComponent<GridLayoutGroup>().cellSize.y;
			float y2 = transform.GetComponent<GridLayoutGroup>().spacing.y;
			Vector2 sizeDelta = new Vector2(transform.GetComponent<RectTransform>().sizeDelta.x, (float)num * (y + y2) + y2);
			transform.GetComponent<RectTransform>().sizeDelta = sizeDelta;
		}

		private void SetStar(Transform starRoot, int num)
		{
			int num2 = 0;
			Transform[] componentsInChildren = starRoot.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent != null && transform.parent.parent == starRoot.transform;
				if (flag)
				{
					bool flag2 = num2 < num;
					if (flag2)
					{
						transform.gameObject.SetActive(true);
						num2++;
					}
					else
					{
						transform.gameObject.SetActive(false);
					}
				}
			}
		}

		private string Getcizhui(int type)
		{
			string result = "";
			switch (type)
			{
			case 1:
				result = "风";
				break;
			case 2:
				result = "火";
				break;
			case 3:
				result = "光";
				break;
			case 4:
				result = "雷";
				break;
			case 5:
				result = "冰";
				break;
			}
			return result;
		}
	}
}
