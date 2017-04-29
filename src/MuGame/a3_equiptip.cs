using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_equiptip : Window
	{
		private bool is_sell_sure = false;

		private uint curid;

		private bool is_put_in = false;

		private Vector3 local_pos;

		private GameObject infoclone = null;

		private equip_tip_type tiptype = equip_tip_type.Bag_tip;

		private List<uint> dic_leftAllid = new List<uint>();

		private Toggle isMark;

		private a3_BagItemData equip_data1;

		public static a3_equiptip instans;

		private GameObject fristMark;

		private uint inputeqp_id = 0u;

		private uint output_id = 0u;

		private List<Transform> txetClon = new List<Transform>();

		private Transform AttCon;

		private BaseButton btn_do;

		private BaseButton btn_back;

		private BaseButton btn_sell;

		private BaseButton btn_put;

		private BaseButton btn_fenjie;

		private BaseButton btn_duanzao;

		private BaseButton btn_out;

		private BaseButton btn_out_bag;

		private BaseButton btn_buy;

		private BaseButton btn_buy_out;

		private BaseButton btn_close;

		private int need1;

		private int need2;

		private string[] list_need1;

		private string[] list_need2;

		private new int type = 0;

		public override void init()
		{
			a3_equiptip.instans = this;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("yes_or_no/yes"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onYes);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("yes_or_no/no"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onNo);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("auto_addPiont/yes"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onAddYes);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("auto_addPiont/no"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onAddNo);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("touch"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onclose);
			this.btn_do = new BaseButton(base.transform.FindChild("info/btns/contain/do"), 1, 1);
			this.btn_do.onClick = new Action<GameObject>(this.ondo);
			this.btn_back = new BaseButton(base.transform.FindChild("info/btns/contain/back"), 1, 1);
			this.btn_back.onClick = new Action<GameObject>(this.onback);
			this.btn_sell = new BaseButton(base.transform.FindChild("info/btns/contain/sell"), 1, 1);
			this.btn_sell.onClick = new Action<GameObject>(this.onsell);
			this.btn_put = new BaseButton(base.transform.FindChild("info/btns/contain/put"), 1, 1);
			this.btn_put.onClick = new Action<GameObject>(this.onput);
			this.btn_fenjie = new BaseButton(base.transform.FindChild("info/btns/contain/fenjie"), 1, 1);
			this.btn_fenjie.onClick = new Action<GameObject>(this.onequipsell);
			this.btn_duanzao = new BaseButton(base.transform.FindChild("info/btns/contain/input"), 1, 1);
			this.btn_duanzao.onClick = new Action<GameObject>(this.onduanzao);
			this.btn_out = new BaseButton(base.transform.FindChild("info/btns/contain/output"), 1, 1);
			this.btn_out.onClick = new Action<GameObject>(this.onOutput);
			this.btn_out_bag = new BaseButton(base.transform.FindChild("info/btns/contain/output_bagfenjie"), 1, 1);
			this.btn_out_bag.onClick = new Action<GameObject>(this.onOutput_bag);
			BaseButton baseButton6 = new BaseButton(base.transform.FindChild("info/isMark"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onIsMark);
			this.btn_buy = new BaseButton(base.transform.FindChild("info/btns/contain/buy"), 1, 1);
			this.btn_buy.onClick = new Action<GameObject>(this.onBuy);
			this.btn_buy_out = new BaseButton(base.transform.FindChild("info/btns/contain/putitem"), 1, 1);
			this.btn_buy_out.onClick = new Action<GameObject>(this.onputitem);
			this.btn_close = new BaseButton(base.transform.FindChild("info/btns/contain/closetip"), 1, 1);
			this.btn_close.onClick = new Action<GameObject>(this.onclose);
			this.fristMark = base.transform.FindChild("info/isFirstMark").gameObject;
			this.local_pos = base.transform.FindChild("info").localPosition;
			for (int i = 0; i <= 6; i++)
			{
				this.txetClon.Add(base.transform.FindChild("info/attr_scroll/scroll/text" + i));
			}
		}

		public override void onShowed()
		{
			this.tiptype = equip_tip_type.Bag_tip;
			base.transform.SetAsLastSibling();
			base.transform.FindChild("yes_or_no").gameObject.SetActive(false);
			base.transform.FindChild("auto_addPiont").gameObject.SetActive(false);
			bool flag = this.uiData == null;
			if (!flag)
			{
				bool flag2 = this.uiData.Count != 0;
				if (flag2)
				{
					this.equip_data1 = (a3_BagItemData)this.uiData[0];
					this.tiptype = (equip_tip_type)this.uiData[1];
					this.curid = this.equip_data1.id;
					this.inputeqp_id = this.curid;
				}
				this.mark();
				this.IsfirstMark();
				base.transform.FindChild("info/ig_up").gameObject.SetActive(false);
				base.transform.FindChild("info/ig_down").gameObject.SetActive(false);
				base.transform.FindChild("info/ig_equal").gameObject.SetActive(false);
				base.transform.FindChild("info/isequiped").gameObject.SetActive(false);
				this.btn_sell.gameObject.SetActive(false);
				this.btn_do.gameObject.SetActive(false);
				this.btn_back.gameObject.SetActive(false);
				this.btn_put.gameObject.SetActive(false);
				this.btn_fenjie.gameObject.SetActive(false);
				this.btn_duanzao.gameObject.SetActive(false);
				this.btn_out.gameObject.SetActive(false);
				this.btn_out_bag.gameObject.SetActive(false);
				this.btn_close.gameObject.SetActive(false);
				this.btn_buy.gameObject.SetActive(false);
				this.btn_buy_out.gameObject.SetActive(false);
				bool flag3 = this.tiptype == equip_tip_type.HouseOut_tip;
				if (flag3)
				{
					this.btn_put.transform.FindChild("Text").GetComponent<Text>().text = "取出";
					this.btn_put.gameObject.SetActive(true);
					this.is_put_in = false;
				}
				else
				{
					bool flag4 = this.tiptype == equip_tip_type.HouseIn_tip;
					if (flag4)
					{
						this.btn_put.transform.FindChild("Text").GetComponent<Text>().text = "放入";
						this.btn_put.gameObject.SetActive(true);
						this.is_put_in = true;
					}
					else
					{
						bool flag5 = this.tiptype == equip_tip_type.Bag_tip;
						if (flag5)
						{
							base.transform.FindChild("info/isequiped").gameObject.SetActive(true);
							this.btn_fenjie.transform.FindChild("Text").GetComponent<Text>().text = "卸下";
							this.btn_put.transform.FindChild("Text").GetComponent<Text>().text = "锻造";
							this.btn_fenjie.gameObject.SetActive(true);
							this.btn_put.gameObject.SetActive(true);
						}
						else
						{
							bool flag6 = this.tiptype == equip_tip_type.BagPick_tip;
							if (flag6)
							{
								this.btn_do.transform.FindChild("Text").GetComponent<Text>().text = "装备";
								this.btn_sell.gameObject.SetActive(true);
								this.btn_do.gameObject.SetActive(true);
								bool flag7 = FunctionOpenMgr.instance.Check(FunctionOpenMgr.EQP, false);
								if (flag7)
								{
									this.btn_back.gameObject.SetActive(true);
								}
								this.btn_fenjie.transform.FindChild("Text").GetComponent<Text>().text = "分解";
								this.btn_fenjie.gameObject.SetActive(true);
								base.transform.FindChild("info/isMark").gameObject.SetActive(true);
								bool isFirstMark = ModelBase<a3_BagModel>.getInstance().isFirstMark;
								if (isFirstMark)
								{
									base.transform.FindChild("info/isFirstMark").gameObject.SetActive(true);
								}
								bool flag8 = ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(this.equip_data1.confdata.equip_type);
								if (flag8)
								{
									a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquipsByType()[this.equip_data1.confdata.equip_type];
									this.output_id = a3_BagItemData.id;
									GameObject gameObject = base.transform.FindChild("info").gameObject;
									this.infoclone = UnityEngine.Object.Instantiate<GameObject>(gameObject);
									this.infoclone.transform.SetParent(base.transform.FindChild("info_clon"), false);
									this.infoclone.transform.localPosition = new Vector3(this.local_pos.x - 450f, this.local_pos.y, this.local_pos.z);
									this.infoclone.transform.FindChild("isequiped").gameObject.SetActive(true);
									bool flag9 = this.equip_data1.equipdata.combpt > a3_BagItemData.equipdata.combpt;
									if (flag9)
									{
										gameObject.transform.FindChild("ig_up").gameObject.SetActive(true);
									}
									else
									{
										bool flag10 = this.equip_data1.equipdata.combpt < a3_BagItemData.equipdata.combpt;
										if (flag10)
										{
											gameObject.transform.FindChild("ig_down").gameObject.SetActive(true);
										}
										else
										{
											gameObject.transform.FindChild("ig_equal").gameObject.SetActive(true);
										}
									}
									gameObject.transform.localPosition = new Vector3(this.local_pos.x + 50f, this.local_pos.y, this.local_pos.z);
									this.infoclone.transform.FindChild("btns").gameObject.SetActive(false);
									this.infoclone.transform.FindChild("isFirstMark").gameObject.SetActive(false);
									this.infoclone.transform.FindChild("isMark").gameObject.SetActive(false);
									this.initEquipInfo(this.infoclone.transform, a3_BagItemData);
								}
							}
							else
							{
								bool flag11 = this.tiptype == equip_tip_type.SellIn_tip;
								if (flag11)
								{
									this.btn_fenjie.transform.FindChild("Text").GetComponent<Text>().text = "分解";
									this.btn_fenjie.gameObject.SetActive(true);
									this.is_sell_sure = true;
								}
								else
								{
									bool flag12 = this.tiptype == equip_tip_type.SellOut_tip;
									if (flag12)
									{
										this.btn_fenjie.transform.FindChild("Text").GetComponent<Text>().text = "取出";
										this.btn_fenjie.gameObject.SetActive(true);
										this.is_sell_sure = false;
									}
									else
									{
										bool flag13 = this.tiptype == equip_tip_type.SellNo_tip;
										if (flag13)
										{
											this.btn_fenjie.transform.FindChild("Text").GetComponent<Text>().text = "";
											this.btn_fenjie.gameObject.SetActive(false);
										}
										else
										{
											bool flag14 = this.tiptype == equip_tip_type.Equip_tip;
											if (flag14)
											{
												bool flag15 = a3_equip.instance;
												if (flag15)
												{
													bool flag16 = a3_equip.instance.tabIndex == 7;
													if (flag16)
													{
														bool flag17 = a3_equip.instance.curInheritId3 == this.curid;
														if (flag17)
														{
															this.btn_out.gameObject.SetActive(true);
														}
														else
														{
															this.btn_duanzao.gameObject.SetActive(true);
														}
													}
													else
													{
														this.btn_duanzao.gameObject.SetActive(true);
													}
												}
											}
											else
											{
												bool flag18 = this.tiptype == equip_tip_type.tip_ForLook;
												if (flag18)
												{
													this.btn_close.gameObject.SetActive(true);
												}
												else
												{
													bool flag19 = this.tiptype == equip_tip_type.tip_forfenjie;
													if (flag19)
													{
														this.btn_out_bag.gameObject.SetActive(true);
													}
													else
													{
														bool flag20 = this.tiptype == equip_tip_type.tip_forAuc_buy;
														if (flag20)
														{
															this.btn_buy.gameObject.SetActive(true);
															this.btn_close.gameObject.SetActive(true);
														}
														else
														{
															bool flag21 = this.tiptype == equip_tip_type.tip_forAuc_put;
															if (flag21)
															{
																this.btn_buy_out.gameObject.SetActive(true);
																this.btn_close.gameObject.SetActive(true);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				Transform info = base.transform.FindChild("info");
				this.initEquipInfo(info, this.equip_data1);
				bool flag22 = a3_bag.isshow;
				if (flag22)
				{
					bool activeSelf = a3_bag.isshow.eqpView.activeSelf;
					if (activeSelf)
					{
						a3_bag.isshow.eqpView.SetActive(false);
					}
				}
			}
		}

		public override void onClosed()
		{
			bool flag = this.infoclone != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.infoclone);
			}
			this.infoclone = null;
			base.transform.FindChild("info/isMark").gameObject.SetActive(false);
			base.transform.FindChild("info/isFirstMark").gameObject.SetActive(false);
			base.transform.FindChild("info").localPosition = this.local_pos;
			bool flag2 = a3_bag.isshow;
			if (flag2)
			{
				bool flag3 = !a3_bag.isshow.eqpView.activeSelf;
				if (flag3)
				{
					a3_bag.isshow.eqpView.SetActive(true);
				}
			}
		}

		private void initEquipInfo(Transform info, a3_BagItemData equip_data)
		{
			info.FindChild("name").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getEquipNameInfo(equip_data);
			for (int i = 1; i <= 5; i++)
			{
				bool flag = i == equip_data.confdata.quality;
				if (flag)
				{
					info.FindChild("ig_bg/ig_" + i).gameObject.SetActive(true);
				}
				else
				{
					info.FindChild("ig_bg/ig_" + i).gameObject.SetActive(false);
				}
			}
			info.FindChild("money").GetComponent<Text>().text = "售价 " + equip_data.confdata.value.ToString();
			info.FindChild("txt_value").GetComponent<Text>().text = equip_data.equipdata.combpt.ToString();
			info.FindChild("lv").GetComponent<Text>().text = equip_data.confdata.equip_level + "阶·" + Globle.getEquipTextByType(equip_data.confdata.equip_type);
			bool flag2 = ModelBase<a3_BagModel>.getInstance().isWorked(equip_data);
			if (flag2)
			{
				info.FindChild("transaction").GetComponent<Text>().text = "可交易";
			}
			else
			{
				info.FindChild("transaction").GetComponent<Text>().text = "不可交易";
			}
			info.FindChild("Refine_text").GetComponent<Text>().text = equip_data.equipdata.stage + "/10";
			string text = "";
			switch (equip_data.confdata.job_limit)
			{
			case 1:
				text = "全职业";
				break;
			case 2:
				text = "狂战士";
				break;
			case 3:
				text = "法师";
				break;
			case 5:
				text = "暗影";
				break;
			}
			bool flag3 = !ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(equip_data.confdata);
			if (flag3)
			{
				info.FindChild("profession").GetComponent<Text>().text = "<color=#f90e0e>" + text + "</color>";
			}
			else
			{
				info.FindChild("profession").GetComponent<Text>().text = text;
			}
			int stage = equip_data.equipdata.stage;
			bool flag4 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl >= (ulong)((long)stage);
			if (flag4)
			{
				info.FindChild("lvl_need/text").GetComponent<Text>().text = stage + "转";
			}
			else
			{
				info.FindChild("lvl_need/text").GetComponent<Text>().text = "<color=#f90e0e>" + stage + "转</color>";
			}
			Transform transform = info.FindChild("icon");
			bool flag5 = transform.childCount > 0;
			if (flag5)
			{
				UnityEngine.Object.Destroy(transform.GetChild(0).gameObject);
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3EquipIcon(equip_data, 1f, false);
			gameObject.transform.FindChild("iconborder/equip_self").gameObject.SetActive(false);
			gameObject.transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(false);
			gameObject.transform.FindChild("iconborder/is_upequip").gameObject.SetActive(false);
			gameObject.transform.FindChild("iconborder/is_new").gameObject.SetActive(false);
			gameObject.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			GameObject gameObject2 = gameObject.transform.FindChild("iconborder/taoshu").gameObject;
			gameObject2.SetActive(true);
			int equip_level = equip_data.confdata.equip_level;
			string text2 = "第" + equip_level + "套";
			switch (equip_data.confdata.quality)
			{
			case 1:
				text2 = "<color=#FFFFFF>第" + equip_level + "套</color>";
				break;
			case 2:
				text2 = "<color=#00FF00>第" + equip_level + "套</color>";
				break;
			case 3:
				text2 = "<color=#00BFFF>第" + equip_level + "套</color>";
				break;
			case 4:
				text2 = "<color=#FF00FF>第" + equip_level + "套</color>";
				break;
			case 5:
				text2 = "<color=#FFA500>第" + equip_level + "套</color>";
				break;
			}
			gameObject2.GetComponent<Text>().text = text2;
			gameObject.transform.SetParent(transform, false);
			for (int j = 1; j <= 10; j++)
			{
				bool flag6 = j <= equip_data.equipdata.stage;
				if (flag6)
				{
					info.FindChild("stars/star" + j).gameObject.SetActive(true);
				}
				else
				{
					info.FindChild("stars/star" + j).gameObject.SetActive(false);
				}
			}
			info.FindChild("zhufu/title").GetComponent<Text>().text = string.Concat(new object[]
			{
				"祝福",
				equip_data.equipdata.blessing_lv,
				"（需求-",
				3 * equip_data.equipdata.blessing_lv,
				"%）"
			});
			this.initAtt(info, equip_data);
		}

		private void initAtt(Transform info, a3_BagItemData equip_data)
		{
			int num = 0;
			this.AttCon = info.transform.FindChild("attr_scroll/scroll/contain");
			for (int i = 0; i < this.AttCon.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.AttCon.GetChild(i).gameObject);
			}
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + equip_data.tpid);
			SXML node = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + equip_data.equipdata.stage).GetNode("stage_info", "itemid==" + equip_data.tpid);
			this.list_need1 = node.getString("equip_limit1").Split(new char[]
			{
				','
			});
			this.list_need2 = node.getString("equip_limit2").Split(new char[]
			{
				','
			});
			this.need1 = int.Parse(this.list_need1[1]) * (100 - 3 * equip_data.equipdata.blessing_lv) / 100;
			this.need2 = int.Parse(this.list_need2[1]) * (100 - 3 * equip_data.equipdata.blessing_lv) / 100;
			bool flag = this.need1 <= ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need1[0])];
			string text;
			if (flag)
			{
				bool flag2 = this.need1 <= 0;
				if (flag2)
				{
					text = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(this.list_need1[0])),
						" <color=#00FF00>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need1[0])],
						"</color>/-"
					});
				}
				else
				{
					text = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(this.list_need1[0])),
						" <color=#00FF00>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need1[0])],
						"</color>/",
						this.need1
					});
				}
			}
			else
			{
				bool flag3 = this.need1 <= 0;
				if (flag3)
				{
					text = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(this.list_need1[0])),
						" <color=#f90e0e>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need1[0])],
						"</color>/-"
					});
				}
				else
				{
					text = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(this.list_need1[0])),
						" <color=#f90e0e>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need1[0])],
						"</color>/",
						this.need1
					});
				}
			}
			bool flag4 = this.need2 <= ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need2[0])];
			string text2;
			if (flag4)
			{
				bool flag5 = this.need2 <= 0;
				if (flag5)
				{
					text2 = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(this.list_need2[0])),
						" <color=#00FF00>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need2[0])],
						"</color>/-"
					});
				}
				else
				{
					text2 = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(this.list_need2[0])),
						" <color=#00FF00>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need2[0])],
						"</color>/",
						this.need2
					});
				}
			}
			else
			{
				bool flag6 = this.need2 <= 0;
				if (flag6)
				{
					text2 = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(this.list_need2[0])),
						" <color=#f90e0e>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need2[0])],
						"</color>/-"
					});
				}
				else
				{
					text2 = string.Concat(new object[]
					{
						Globle.getAttrNameById(int.Parse(this.list_need2[0])),
						" <color=#f90e0e>",
						ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need2[0])],
						"</color>/",
						this.need2
					});
				}
			}
			info.FindChild("zhufu/text1").GetComponent<Text>().text = text;
			info.FindChild("zhufu/text2").GetComponent<Text>().text = text2;
			string[] array = XMLMgr.instance.GetSXML("item.stage", "stage_level==0").GetNode("stage_info", "itemid==" + equip_data.tpid).getString("basic_att").Split(new char[]
			{
				','
			});
			string[] array2 = node.getString("basic_att").Split(new char[]
			{
				','
			});
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.txetClon[0]).transform;
			transform.SetParent(this.AttCon, false);
			transform.gameObject.SetActive(true);
			transform.GetComponent<Text>().text = "[基础属性]";
			num++;
			Transform transform2 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[1]).transform;
			transform2.SetParent(this.AttCon, false);
			transform2.gameObject.SetActive(true);
			num++;
			bool flag7 = array.Length > 1;
			if (flag7)
			{
				transform2.FindChild("Text").GetComponent<Text>().text = "攻击力 " + array[0] + "-" + array[1];
			}
			else
			{
				transform2.FindChild("Text").GetComponent<Text>().text = Globle.getAttrNameById(sXML.getInt("att_type")) + " " + array[0];
			}
			bool flag8 = equip_data.equipdata.intensify_lv > 0;
			if (flag8)
			{
				Transform transform3 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[1]).transform;
				transform3.SetParent(this.AttCon, false);
				transform3.gameObject.SetActive(true);
				num++;
				SXML node2 = XMLMgr.instance.GetSXML("item.intensify", "intensify_level==" + equip_data.equipdata.intensify_lv).GetNode("intensify_info", "itemid==" + equip_data.tpid);
				string[] array3 = node2.getString("intensify_att").Split(new char[]
				{
					','
				});
				int @int = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + equip_data.equipdata.stage).getInt("extra");
				int num2 = 0;
				int num3 = 0;
				for (int j = 1; j <= equip_data.equipdata.intensify_lv; j++)
				{
					SXML node3 = XMLMgr.instance.GetSXML("item.intensify", "intensify_level==" + j).GetNode("intensify_info", "itemid==" + equip_data.tpid);
					bool flag9 = array3.Length > 1;
					if (flag9)
					{
						num2 += int.Parse(node2.getString("intensify_att").Split(new char[]
						{
							','
						})[0]) * @int / 100;
						num3 += int.Parse(node2.getString("intensify_att").Split(new char[]
						{
							','
						})[1]) * @int / 100;
					}
					else
					{
						num2 += int.Parse(node2.getString("intensify_att").Split(new char[]
						{
							','
						})[0]) * @int / 100;
					}
				}
				bool flag10 = array3.Length > 1;
				if (flag10)
				{
					transform3.FindChild("Text").GetComponent<Text>().text = string.Concat(new object[]
					{
						"强化加成 ",
						num2,
						"-",
						num3
					});
				}
				else
				{
					transform3.FindChild("Text").GetComponent<Text>().text = "强化加成 " + num2;
				}
			}
			bool flag11 = equip_data.equipdata.stage > 0;
			if (flag11)
			{
				Transform transform4 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[1]).transform;
				transform4.SetParent(this.AttCon, false);
				transform4.gameObject.SetActive(true);
				num++;
				bool flag12 = array.Length > 1;
				if (flag12)
				{
					int num4 = int.Parse(array2[0]) - int.Parse(array[0]);
					int num5 = int.Parse(array2[1]) - int.Parse(array[1]);
					transform4.FindChild("Text").GetComponent<Text>().text = string.Concat(new object[]
					{
						"精炼加成 ",
						num4,
						"-",
						num5
					});
				}
				else
				{
					int num6 = int.Parse(array2[0]) - int.Parse(array[0]);
					transform4.FindChild("Text").GetComponent<Text>().text = "精炼加成 " + num6;
				}
			}
			Transform transform5 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[0]).transform;
			transform5.SetParent(this.AttCon, false);
			transform5.gameObject.SetActive(true);
			transform5.GetComponent<Text>().text = "[附加属性]";
			transform5.FindChild("count").GetComponent<Text>().text = equip_data.equipdata.subjoin_att.Count + "/5";
			num++;
			int num7 = 0;
			SXML sXML2 = XMLMgr.instance.GetSXML("item.subjoin_att", "equip_level==" + equip_data.confdata.equip_level);
			foreach (int current in equip_data.equipdata.subjoin_att.Keys)
			{
				num7++;
				Transform transform6 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[2]).transform;
				transform6.SetParent(this.AttCon, false);
				transform6.gameObject.SetActive(true);
				num++;
				Text component = transform6.FindChild("Text").GetComponent<Text>();
				component.text = Globle.getAttrAddById(current, equip_data.equipdata.subjoin_att[current], true);
				SXML node4 = sXML2.GetNode("subjoin_att_info", "att_type==" + current);
				bool flag13 = equip_data.equipdata.subjoin_att[current] >= node4.getInt("max");
				if (flag13)
				{
					transform6.FindChild("max").gameObject.SetActive(true);
				}
				else
				{
					transform6.FindChild("max").gameObject.SetActive(false);
				}
			}
			Transform transform7 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[0]).transform;
			transform7.SetParent(this.AttCon, false);
			transform7.gameObject.SetActive(true);
			transform7.GetComponent<Text>().text = "[灵魂属性]";
			num++;
			Transform transform8 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[6]).transform;
			transform8.SetParent(this.AttCon, false);
			transform8.gameObject.SetActive(true);
			num++;
			bool flag14 = a3_targetinfo.isshow;
			Dictionary<int, a3_BagItemData> active_eqp;
			if (flag14)
			{
				active_eqp = a3_targetinfo.isshow.active_eqp;
			}
			else
			{
				active_eqp = ModelBase<a3_EquipModel>.getInstance().active_eqp;
			}
			bool flag15 = equip_data.equipdata.att_type > 0;
			string text3;
			if (flag15)
			{
				text3 = Globle.getAttrAddById(equip_data.equipdata.att_type, equip_data.equipdata.att_value, true);
				bool flag16 = active_eqp.ContainsKey(equip_data.confdata.equip_type);
				if (flag16)
				{
					bool flag17 = active_eqp[equip_data.confdata.equip_type].id == equip_data.id;
					if (flag17)
					{
						text3 = string.Concat(new string[]
						{
							"<color=#0cf373>",
							text3,
							"（穿戴",
							this.Getcizhui(ModelBase<a3_EquipModel>.getInstance().eqp_att_act[equip_data.equipdata.attribute]),
							"属性",
							Globle.getEquipTextByType(ModelBase<a3_EquipModel>.getInstance().eqp_type_act[equip_data.confdata.equip_type]),
							"激活）</color>"
						});
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
			transform8.FindChild("Text").GetComponent<Text>().text = text3;
			bool flag18 = equip_data.equipdata.baoshi != null;
			if (flag18)
			{
				Transform transform9 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[0]).transform;
				transform9.SetParent(this.AttCon, false);
				transform9.gameObject.SetActive(true);
				transform9.GetComponent<Text>().text = "[宝石镶嵌]";
				transform9.FindChild("count").GetComponent<Text>().text = equip_data.equipdata.baoshi.Count + "孔";
				num++;
				foreach (int current2 in equip_data.equipdata.baoshi.Keys)
				{
					Transform transform10 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[3]).transform;
					transform10.SetParent(this.AttCon, false);
					transform10.gameObject.SetActive(true);
					Image component2 = transform10.transform.FindChild("icon").GetComponent<Image>();
					Text component3 = transform10.FindChild("Text").GetComponent<Text>();
					bool flag19 = equip_data.equipdata.baoshi[current2] <= 0;
					if (flag19)
					{
						component2.gameObject.SetActive(false);
						component3.text = "可镶嵌";
					}
					else
					{
						SXML sXML3 = XMLMgr.instance.GetSXML("item", "");
						SXML node5 = sXML3.GetNode("item", "id==" + equip_data.equipdata.baoshi[current2]);
						string path = "icon/item/" + node5.getString("icon_file");
						component2.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
						SXML node6 = sXML3.GetNode("gem_info", "item_id==" + equip_data.equipdata.baoshi[current2]);
						List<SXML> nodeList = node6.GetNodeList("gem_add", "");
						int id = 0;
						int value = 0;
						foreach (SXML current3 in nodeList)
						{
							bool flag20 = current3.getInt("equip_type") == equip_data.confdata.equip_type;
							if (flag20)
							{
								id = current3.getInt("att_type");
								value = current3.getInt("att_value");
								break;
							}
						}
						component3.text = node5.getString("item_name") + " " + Globle.getAttrAddById(id, value, true);
					}
					num++;
				}
			}
			Transform transform11 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[0]).transform;
			transform11.SetParent(this.AttCon, false);
			transform11.gameObject.SetActive(true);
			transform11.GetComponent<Text>().text = "[追加属性]";
			transform11.FindChild("count").GetComponent<Text>().text = equip_data.equipdata.add_level + "/" + equip_data.confdata.add_basiclevel * equip_data.confdata.equip_level * equip_data.equipdata.stage;
			num++;
			Transform transform12 = UnityEngine.Object.Instantiate<Transform>(this.txetClon[1]).transform;
			transform12.SetParent(this.AttCon, false);
			transform12.gameObject.SetActive(true);
			num++;
			SXML sXML4 = XMLMgr.instance.GetSXML("item.item", "id==" + equip_data.tpid);
			int id2 = int.Parse(sXML4.getString("add_atttype").Split(new char[]
			{
				','
			})[0]);
			int value2 = int.Parse(sXML4.getString("add_atttype").Split(new char[]
			{
				','
			})[1]) * equip_data.equipdata.add_level;
			transform12.FindChild("Text").GetComponent<Text>().text = Globle.getAttrAddById(id2, value2, true);
			float y = this.AttCon.GetComponent<GridLayoutGroup>().cellSize.y;
			float y2 = this.AttCon.GetComponent<GridLayoutGroup>().spacing.y;
			Vector2 sizeDelta = new Vector2(this.AttCon.GetComponent<RectTransform>().sizeDelta.x, (float)num * (y + y2) + y2);
			this.AttCon.GetComponent<RectTransform>().sizeDelta = sizeDelta;
		}

		private void onBuy(GameObject go)
		{
			bool flag = this.curid != 0u && ModelBase<A3_AuctionModel>.getInstance().GetItems()[this.curid].confdata.equip_type > 0;
			if (flag)
			{
				uint cid = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this.curid].auctiondata.cid;
				int num = ModelBase<A3_AuctionModel>.getInstance().GetItems()[this.curid].num;
				BaseProxy<A3_AuctionProxy>.getInstance().SendBuyMsg(this.curid, cid, (uint)num);
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
		}

		private void onputitem(GameObject go)
		{
			bool flag = ModelBase<a3_BagModel>.getInstance().getItems(false)[this.curid].confdata.equip_type > 0;
			if (flag)
			{
				bool flag2 = this.curid != 0u && ModelBase<a3_BagModel>.getInstance().isWorked(ModelBase<a3_BagModel>.getInstance().getItems(false)[this.curid]);
				if (flag2)
				{
					a3_BagItemData info = ModelBase<a3_BagModel>.getInstance().getItems(false)[this.curid];
					bool flag3 = a3_auction_sell.instans != null;
					if (flag3)
					{
						a3_auction_sell.instans.SetInfo(info);
					}
				}
				else
				{
					flytxt.instance.fly("该物品不可交易！", 0, default(Color), null);
				}
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
		}

		private void zhufu()
		{
		}

		private void onAddYes(GameObject go)
		{
			int num = ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need2[0])];
			int num2 = ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need1[0])];
			int type_id = this.GetType_id(Globle.getAttrNameById(int.Parse(this.list_need1[0])));
			int type_id2 = this.GetType_id(Globle.getAttrNameById(int.Parse(this.list_need2[0])));
			bool flag = this.need1 > num2 && this.need2 <= num;
			if (flag)
			{
				int num3 = this.need1 - ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need1[0])];
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				bool flag2 = num3 <= ModelBase<PlayerModel>.getInstance().pt_att;
				if (flag2)
				{
					dictionary.Add(type_id, num3);
				}
				else
				{
					dictionary.Add(type_id, ModelBase<PlayerModel>.getInstance().pt_att);
				}
				BaseProxy<PlayerInfoProxy>.getInstance().sendAddPoint(0, dictionary);
			}
			bool flag3 = this.need2 > num && this.need1 <= num2;
			if (flag3)
			{
				int num4 = this.need2 - ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need2[0])];
				Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
				bool flag4 = num4 <= ModelBase<PlayerModel>.getInstance().pt_att;
				if (flag4)
				{
					dictionary2.Add(type_id2, num4);
				}
				else
				{
					dictionary2.Add(type_id2, ModelBase<PlayerModel>.getInstance().pt_att);
				}
				BaseProxy<PlayerInfoProxy>.getInstance().sendAddPoint(0, dictionary2);
			}
			bool flag5 = this.need2 > num && this.need1 > num2;
			if (flag5)
			{
				int num5 = this.need1 - ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need1[0])];
				int num6 = this.need2 - ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(this.list_need2[0])];
				Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
				bool flag6 = ModelBase<PlayerModel>.getInstance().pt_att >= num5 + num6;
				if (flag6)
				{
					dictionary3.Add(type_id, num5);
					dictionary3.Add(type_id2, num6);
				}
				bool flag7 = ModelBase<PlayerModel>.getInstance().pt_att >= num5 && ModelBase<PlayerModel>.getInstance().pt_att < num5 + num6;
				if (flag7)
				{
					int num7 = ModelBase<PlayerModel>.getInstance().pt_att - num5;
					dictionary3.Add(type_id, num5);
					bool flag8 = num7 != 0;
					if (flag8)
					{
						dictionary3.Add(type_id2, num7);
					}
				}
				bool flag9 = ModelBase<PlayerModel>.getInstance().pt_att < num5;
				if (flag9)
				{
					dictionary3.Add(type_id, num5);
				}
				BaseProxy<PlayerInfoProxy>.getInstance().sendAddPoint(0, dictionary3);
			}
			BaseProxy<EquipProxy>.getInstance().sendChangeEquip(this.curid);
			base.transform.FindChild("auto_addPiont").gameObject.SetActive(false);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
		}

		private int GetType_id(string tpye)
		{
			int result = 0;
			if (!(tpye == "力量"))
			{
				if (!(tpye == "敏捷"))
				{
					if (!(tpye == "体力"))
					{
						if (!(tpye == "魔力"))
						{
							if (tpye == "智慧")
							{
								result = 5;
							}
						}
						else
						{
							result = 2;
						}
					}
					else
					{
						result = 4;
					}
				}
				else
				{
					result = 3;
				}
			}
			else
			{
				result = 1;
			}
			return result;
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

		private void onAddNo(GameObject go)
		{
			base.transform.FindChild("auto_addPiont").gameObject.SetActive(false);
		}

		public void mark()
		{
			bool ismark = this.equip_data1.ismark;
			if (ismark)
			{
				base.getComponentByPath<Image>("info/isMark/Image").gameObject.SetActive(true);
			}
			else
			{
				base.getComponentByPath<Image>("info/isMark").GetComponent<Image>().enabled = true;
				base.getComponentByPath<Image>("info/isMark/Image").gameObject.SetActive(false);
			}
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
		}

		private void ondo(GameObject go)
		{
			bool flag = go.transform.FindChild("Text").GetComponent<Text>().text == "装备";
			if (flag)
			{
				bool flag2 = !ModelBase<a3_EquipModel>.getInstance().checkCanEquip(this.equip_data1.confdata, this.equip_data1.equipdata.stage, this.equip_data1.equipdata.blessing_lv) && ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(this.equip_data1.confdata);
				if (flag2)
				{
					bool flag3 = ModelBase<PlayerModel>.getInstance().pt_att > 0;
					if (flag3)
					{
						base.transform.FindChild("auto_addPiont").gameObject.SetActive(true);
					}
				}
			}
			BaseProxy<EquipProxy>.getInstance().sendChangeEquip(this.curid);
		}

		private void onback(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_BAG);
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.curid);
			arrayList.Add(true);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIP, arrayList, false);
		}

		private void onsell(GameObject go)
		{
			bool flag = this.equip_data1.confdata.quality != 5 && ModelBase<a3_BagModel>.getInstance().isWorked(this.equip_data1);
			if (flag)
			{
				this.type = 1;
				this.onYes(go);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
			}
			else
			{
				this.type = 1;
				base.transform.FindChild("yes_or_no").gameObject.SetActive(true);
				this.setGetmoney();
			}
		}

		private void onOutput(GameObject go)
		{
			bool flag = a3_equip.instance;
			if (flag)
			{
				a3_equip.instance.onOutEqp();
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
		}

		private void onOutput_bag(GameObject go)
		{
			bool flag = a3_bag.isshow;
			if (flag)
			{
				a3_bag.isshow.outItemCon_fenjie(-1, this.curid);
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
		}

		private void onput(GameObject go)
		{
			bool flag = go.transform.FindChild("Text").GetComponent<Text>().text == "锻造";
			if (flag)
			{
				this.onback(go);
			}
			else
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
				bool flag2 = this.is_put_in;
				if (flag2)
				{
					BaseProxy<BagProxy>.getInstance().sendRoomItems(true, this.equip_data1.id, this.equip_data1.num);
				}
				else
				{
					BaseProxy<BagProxy>.getInstance().sendRoomItems(false, this.equip_data1.id, this.equip_data1.num);
				}
			}
		}

		public void IsfirstMark()
		{
			bool activeSelf = base.transform.FindChild("info/isMark").gameObject.activeSelf;
			if (activeSelf)
			{
				bool isFirstMark = ModelBase<a3_BagModel>.getInstance().isFirstMark;
				if (isFirstMark)
				{
					this.fristMark.SetActive(true);
				}
				else
				{
					this.fristMark.SetActive(false);
				}
			}
		}

		private void onYes(GameObject go)
		{
			bool flag = this.type == 0;
			if (flag)
			{
				bool activeSelf = base.getComponentByPath<Image>("info/isMark/Image").gameObject.activeSelf;
				if (activeSelf)
				{
					base.transform.FindChild("yes_or_no").gameObject.SetActive(false);
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
					flytxt.instance.fly("装备已被锁定！", 0, default(Color), null);
				}
				else
				{
					BaseProxy<EquipProxy>.getInstance().sendsell_one(this.curid);
					this.getItemNum();
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
				}
			}
			else
			{
				bool flag2 = this.type == 1;
				if (flag2)
				{
					bool activeSelf2 = base.getComponentByPath<Image>("info/isMark/Image").gameObject.activeSelf;
					if (activeSelf2)
					{
						base.transform.FindChild("yes_or_no").gameObject.SetActive(false);
						InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
						flytxt.instance.fly("装备已被锁定！", 0, default(Color), null);
					}
					else
					{
						BaseProxy<BagProxy>.getInstance().sendSellItems(this.curid, 1);
						InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
					}
				}
			}
		}

		private void getItemNum()
		{
			bool flag = a3_bag.indtans != null;
			if (flag)
			{
				SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + this.equip_data1.tpid);
				List<SXML> nodeList = sXML.GetNodeList("decompose", "");
				foreach (SXML current in nodeList)
				{
					switch (current.getInt("item"))
					{
					case 1540:
						a3_bag.indtans.mojing_num = current.getInt("num");
						break;
					case 1541:
						a3_bag.indtans.shengguanghuiji_num = current.getInt("num");
						break;
					case 1542:
						a3_bag.indtans.mifageli_num = current.getInt("num");
						break;
					}
				}
			}
		}

		private void onNo(GameObject go)
		{
			base.transform.FindChild("yes_or_no").gameObject.SetActive(false);
		}

		private void onequipsell(GameObject go)
		{
			bool flag = go.transform.FindChild("Text").GetComponent<Text>().text == "卸下";
			if (flag)
			{
				this.ondo(go);
			}
			else
			{
				bool flag2 = this.equip_data1.confdata.quality != 5 && ModelBase<a3_BagModel>.getInstance().isWorked(this.equip_data1);
				if (flag2)
				{
					this.type = 0;
					this.onYes(go);
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
				}
				else
				{
					this.type = 0;
					base.transform.FindChild("yes_or_no").gameObject.SetActive(true);
					this.setfjts();
				}
			}
		}

		private void setfjts()
		{
			base.transform.FindChild("yes_or_no/toptext").GetComponent<Text>().text = "是否分解" + ModelBase<a3_BagModel>.getInstance().getEquipNameInfo(this.equip_data1);
			string text = "分解可获得";
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + this.equip_data1.tpid);
			List<SXML> nodeList = sXML.GetNodeList("decompose", "");
			bool flag = true;
			foreach (SXML current in nodeList)
			{
				SXML sXML2 = XMLMgr.instance.GetSXML("item.item", "id==" + current.getInt("item"));
				bool flag2 = flag;
				if (flag2)
				{
					text = string.Concat(new object[]
					{
						text,
						"<color=#00FF00>",
						current.getInt("num"),
						"</color>",
						sXML2.getString("item_name")
					});
					flag = false;
				}
				else
				{
					text = string.Concat(new object[]
					{
						text,
						"和<color=#00FF00>",
						current.getInt("num"),
						"</color>",
						sXML2.getString("item_name")
					});
				}
			}
			base.transform.FindChild("yes_or_no/canget").GetComponent<Text>().text = text;
		}

		private void setGetmoney()
		{
			base.transform.FindChild("yes_or_no/toptext").GetComponent<Text>().text = "是否出售" + ModelBase<a3_BagModel>.getInstance().getEquipNameInfo(this.equip_data1);
			string text = "出售可获得";
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + this.equip_data1.tpid);
			text = string.Concat(new object[]
			{
				text,
				"<color=#FFD700>",
				sXML.getInt("value"),
				"</color>金币"
			});
			base.transform.FindChild("yes_or_no/canget").GetComponent<Text>().text = text;
		}

		private void onIsMark(GameObject go)
		{
			BaseProxy<BagProxy>.getInstance().sendMark(this.equip_data1.id);
			bool activeSelf = base.getComponentByPath<Image>("info/isMark/Image").gameObject.activeSelf;
			if (activeSelf)
			{
				go.GetComponent<Image>().enabled = true;
				base.getComponentByPath<Image>("info/isMark/Image").gameObject.SetActive(false);
			}
			else
			{
				go.GetComponent<Image>().enabled = false;
				base.getComponentByPath<Image>("info/isMark/Image").gameObject.SetActive(true);
				flytxt.instance.fly("被锁定的装备不会被分解", 0, default(Color), null);
			}
		}

		private void onduanzao(GameObject go)
		{
			bool flag = a3_equip.instance != null;
			if (flag)
			{
				bool flag2 = this.uiData != null;
				if (flag2)
				{
					uint id = this.curid;
					a3_equip.instance.onClickEquip(go, id);
				}
				else
				{
					bool flag3 = a3_equip.instance.equipicon.Count > 0;
					if (flag3)
					{
						a3_equip.instance.onClickEquip(go, a3_equip.instance.equipicon.Keys.First<uint>());
					}
				}
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPTIP);
		}
	}
}
