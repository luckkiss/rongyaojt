using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class broadcasting : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly broadcasting.<>c <>9 = new broadcasting.<>c();

			public static Action<GameObject> <>9__27_0;

			public static Action<GameObject> <>9__27_1;

			public static Action<GameObject> <>9__27_2;

			public static Action<GameObject> <>9__27_3;

			public static Action<GameObject> <>9__27_4;

			public static Action<GameObject> <>9__27_5;

			public static Action<GameObject> <>9__27_6;

			internal void <getNotice>b__27_0(GameObject o)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_LOTTERY, null, false);
			}

			internal void <getNotice>b__27_1(GameObject GameObject)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(ELITE_MONSTER_PAGE_IDX.BOSSPAGE);
				bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.GLOBA_BOSS, false);
				if (flag)
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList, false);
				}
				else
				{
					flytxt.instance.fly(ContMgr.getCont("func_limit_16", null), 0, default(Color), null);
				}
			}

			internal void <getNotice>b__27_2(GameObject GameObject)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(ELITE_MONSTER_PAGE_IDX.BOSSPAGE);
				bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.GLOBA_BOSS, false);
				if (flag)
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_ELITEMON, arrayList, false);
				}
				else
				{
					flytxt.instance.fly(ContMgr.getCont("func_limit_16", null), 0, default(Color), null);
				}
			}

			internal void <getNotice>b__27_3(GameObject GameObject)
			{
				bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.DEVIL_HUNTER, false);
				if (flag)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add("mlzd");
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList, false);
				}
				else
				{
					flytxt.instance.fly(ContMgr.getCont("func_limit_12", null), 0, default(Color), null);
				}
			}

			internal void <getNotice>b__27_4(GameObject GameObject)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.WORLD_MAP, null, false);
			}

			internal void <getNotice>b__27_5(GameObject GameObject)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add("forchest");
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList, false);
			}

			internal void <getNotice>b__27_6(GameObject GameObject)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add("forchest");
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_ACTIVE, arrayList, false);
			}
		}

		public static List<BroadcastingData> talkList = new List<BroadcastingData>();

		private List<BroadcastingData> system_msg = new List<BroadcastingData>();

		private BroadcastingData _data = null;

		private float hight = 31f;

		private GameObject Text;

		private GameObject openBtn;

		private GameObject currentText;

		private RectTransform currentRect;

		private RectTransform bg;

		private bool isBuilded = false;

		private GameObject pastText = null;

		private RectTransform pastRect;

		private float timer = 0f;

		private Text numMark;

		private int num = 0;

		private bool canGetNext = true;

		private float _length = 0f;

		public static float OutTimer;

		public static bool isOn;

		public static broadcasting instance;

		private BaseButton bsBtnOpen;

		private GameObject child;

		private GameObject mask;

		private List<iconActionData> iconAction = new List<iconActionData>();

		public override void init()
		{
			base.init();
			broadcasting.instance = this;
			BaseProxy<ChatProxy>.getInstance();
			this.Text = base.transform.FindChild("Text").gameObject;
			this.bg = base.transform.FindChild("bg").GetComponent<RectTransform>();
			InterfaceMgr.setUntouchable(this.bg.gameObject);
			this.bg.GetComponent<CanvasGroup>().blocksRaycasts = false;
			this.numMark = base.transform.FindChild("openBtn/mark/Text").GetComponent<Text>();
			this.openBtn = base.transform.FindChild("openBtn").gameObject;
			this.bsBtnOpen = new BaseButton(this.openBtn.transform, 0, 1);
			this.bsBtnOpen.onClick = new Action<GameObject>(this.onClickOpen);
			this.mask = base.getGameObjectByPath("mask");
			this.mask.SetActive(false);
			this.showMark();
			BaseProxy<ChatProxy>.getInstance().addEventListener(ChatProxy.lis_sys_notice, new Action<GameEvent>(this.getNotice));
			RectTransform cemaraRectTran = Baselayer.cemaraRectTran;
			RectTransform component = base.gameObject.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(cemaraRectTran.rect.width, cemaraRectTran.rect.height);
			this.child = base.getGameObjectByPath("child");
		}

		public override void onShowed()
		{
			base.onShowed();
			this.bg.sizeDelta = new Vector2(this.bg.sizeDelta.x, 0f);
		}

		private void onOfflineEXP()
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.OFFLINEEXP, null, false);
		}

		private void getNotice(GameEvent e)
		{
			debug.Log("广播信息:" + e.data.dump());
			bool flag = e.data.ContainsKey("tp");
			if (flag)
			{
				BroadcastingData broadcastingData = new BroadcastingData();
				iconActionData iconActionData = new iconActionData();
				broadcastingData.msg = e.data["msg"];
				string[] array = broadcastingData.msg.Split(new char[]
				{
					':'
				});
				bool flag2 = e.data["tp"] == 1;
				if (flag2)
				{
					a3_ItemData a3_ItemData = default(a3_ItemData);
					a3_ItemData = ModelBase<a3_BagModel>.getInstance().getItemDataById(uint.Parse(array[1]));
					broadcastingData.msg = ContMgr.getCont("lottery_roll", new List<string>
					{
						array[0],
						Globle.getColorStrByQuality(a3_ItemData.item_name, a3_ItemData.quality) + "x" + array[2]
					});
					this.system_msg.Add(broadcastingData);
					iconActionData.iconOpen = false;
					iconActionData.action = null;
					this.iconAction.Add(iconActionData);
				}
				else
				{
					bool flag3 = e.data["tp"] == 2;
					if (flag3)
					{
						EquipConf equipConf = default(EquipConf);
						equipConf = ModelBase<EquipModel>.getInstance().getEquipDataById(int.Parse(array[1]));
						EquipConf equipConf2 = default(EquipConf);
						equipConf2 = ModelBase<EquipModel>.getInstance().getEquipDataById(int.Parse(array[2]));
						broadcastingData.msg = ContMgr.getCont("equip_strengthen", new List<string>
						{
							array[0],
							Globle.getColorStrByQuality(equipConf.name, equipConf.quality),
							Globle.getColorStrByQuality(equipConf2.name, equipConf2.quality)
						});
						this.system_msg.Add(broadcastingData);
						iconActionData.iconOpen = false;
						iconActionData.action = null;
						this.iconAction.Add(iconActionData);
					}
					else
					{
						bool flag4 = e.data["tp"] == 3;
						if (flag4)
						{
							a3_ItemData a3_ItemData2 = default(a3_ItemData);
							a3_ItemData2 = ModelBase<a3_BagModel>.getInstance().getItemDataById(uint.Parse(array[1]));
							broadcastingData.msg = ContMgr.getCont("active_buying", new List<string>
							{
								array[0],
								Globle.getColorStrByQuality(a3_ItemData2.item_name, a3_ItemData2.quality)
							});
							this.system_msg.Add(broadcastingData);
							iconActionData.iconOpen = false;
							iconActionData.action = null;
							this.iconAction.Add(iconActionData);
						}
						else
						{
							bool flag5 = e.data["tp"] == 4;
							if (flag5)
							{
								bool flag6 = int.Parse(array[2]) > 1;
								string msg;
								if (flag6)
								{
									msg = string.Concat(new string[]
									{
										array[0],
										" 击杀了 ",
										array[1],
										" (",
										array[2],
										"连杀)"
									});
								}
								else
								{
									msg = array[0] + " 击杀了 " + array[1];
								}
								broadcastingData.msg = msg;
								this.system_msg.Add(broadcastingData);
								iconActionData.iconOpen = false;
								iconActionData.action = null;
								this.iconAction.Add(iconActionData);
							}
							else
							{
								bool flag7 = e.data["tp"] == 5;
								if (!flag7)
								{
									bool flag8 = e.data["tp"] == 6;
									if (flag8)
									{
										a3_ItemData a3_ItemData3 = default(a3_ItemData);
										a3_ItemData3 = ModelBase<a3_BagModel>.getInstance().getItemDataById(uint.Parse(array[1]));
										string text = "<color=#FF0000>" + array[0] + "</color>使用魔盒时被幸运女神击中,抽中了";
										text += Globle.getColorStrByQuality(a3_ItemData3.item_name, a3_ItemData3.quality);
										broadcastingData.msg = text;
										this.system_msg.Add(broadcastingData);
										Variant variant = new Variant();
										variant["name"] = array[0];
										variant["itemId"] = array[1];
										variant["cnt"] = array[2];
										variant["stage"] = ((array.Length > 3) ? array[3] : "0");
										variant["intensify"] = ((array.Length > 4) ? array[4] : "0");
										iconActionData.iconOpen = true;
										iconActionData arg_4B5_0 = iconActionData;
										Action<GameObject> arg_4B5_1;
										if ((arg_4B5_1 = broadcasting.<>c.<>9__27_0) == null)
										{
											arg_4B5_1 = (broadcasting.<>c.<>9__27_0 = new Action<GameObject>(broadcasting.<>c.<>9.<getNotice>b__27_0));
										}
										arg_4B5_0.action = arg_4B5_1;
										this.iconAction.Add(iconActionData);
										bool flag9 = text == null;
										if (flag9)
										{
											broadcasting.isOn = false;
										}
										else
										{
											broadcasting.isOn = true;
										}
										UIClient.instance.dispatchEvent(GameEvent.Create(19001u, this, variant, false));
									}
									else
									{
										bool flag10 = e.data["tp"] == 7;
										if (flag10)
										{
											int num = -1;
											bool flag11 = !int.TryParse(array[0], out num);
											if (!flag11)
											{
												switch (num)
												{
												case 1:
												{
													broadcastingData.msg = ContMgr.getCont("mwsl_bc_" + array[1], new List<string>
													{
														array[2]
													});
													this.system_msg.Add(broadcastingData);
													iconActionData.iconOpen = true;
													iconActionData arg_5B5_0 = iconActionData;
													Action<GameObject> arg_5B5_1;
													if ((arg_5B5_1 = broadcasting.<>c.<>9__27_1) == null)
													{
														arg_5B5_1 = (broadcasting.<>c.<>9__27_1 = new Action<GameObject>(broadcasting.<>c.<>9.<getNotice>b__27_1));
													}
													arg_5B5_0.action = arg_5B5_1;
													this.iconAction.Add(iconActionData);
													break;
												}
												case 2:
												{
													broadcastingData.msg = ContMgr.getCont("mwsl_ltbc_" + array[1], null);
													this.system_msg.Add(broadcastingData);
													iconActionData.iconOpen = true;
													iconActionData arg_619_0 = iconActionData;
													Action<GameObject> arg_619_1;
													if ((arg_619_1 = broadcasting.<>c.<>9__27_2) == null)
													{
														arg_619_1 = (broadcasting.<>c.<>9__27_2 = new Action<GameObject>(broadcasting.<>c.<>9.<getNotice>b__27_2));
													}
													arg_619_0.action = arg_619_1;
													this.iconAction.Add(iconActionData);
													break;
												}
												case 3:
												{
													SXML sXML = XMLMgr.instance.GetSXML("monsters", "");
													SXML node = sXML.GetNode("monsters", "id==" + array[1]);
													string item = "";
													bool flag12 = node != null;
													if (flag12)
													{
														item = node.getString("name");
													}
													broadcastingData.msg = ContMgr.getCont("mwsl_killed", new List<string>
													{
														item,
														array[2]
													});
													this.system_msg.Add(broadcastingData);
													iconActionData.iconOpen = false;
													iconActionData.action = null;
													this.iconAction.Add(iconActionData);
													BaseProxy<A3_ActiveProxy>.getInstance().SendGetBossInfo();
													break;
												}
												}
											}
										}
										else
										{
											bool flag13 = e.data["tp"] == 10;
											if (flag13)
											{
												bool flag14 = a3_insideui_fb.instance != null;
												if (flag14)
												{
													a3_insideui_fb.instance.SetBroadCast(e.data);
												}
											}
											else
											{
												bool flag15 = e.data["tp"] == 11;
												if (flag15)
												{
													string a = e.data["msg"];
													bool flag16 = a == "2:";
													if (flag16)
													{
														Variant variant2 = new Variant();
														variant2["tp"] = 6;
														variant2["msg"] = ContMgr.getCont("sjbt_over", null);
														broadcastingData.msg = variant2["msg"];
														this.system_msg.Add(broadcastingData);
														iconActionData.iconOpen = false;
														iconActionData.action = null;
														this.iconAction.Add(iconActionData);
														bool flag17 = a3_chatroom._instance != null;
														if (flag17)
														{
															a3_chatroom._instance.otherSays(variant2);
														}
													}
													else
													{
														Variant variant3 = new Variant();
														variant3["tp"] = 6;
														variant3["msg"] = ContMgr.getCont("sjbt_start", new List<string>
														{
															array[1],
															array[2],
															array[3]
														});
														broadcastingData.msg = variant3["msg"];
														this.system_msg.Add(broadcastingData);
														iconActionData.iconOpen = false;
														iconActionData.action = null;
														this.iconAction.Add(iconActionData);
														bool flag18 = a3_chatroom._instance != null;
														if (flag18)
														{
															a3_chatroom._instance.otherSays(variant3);
														}
													}
												}
												else
												{
													bool flag19 = e.data["tp"] == 12;
													if (flag19)
													{
														a3_ItemData a3_ItemData4 = default(a3_ItemData);
														a3_ItemData4 = ModelBase<a3_BagModel>.getInstance().getItemDataById(uint.Parse(array[2]));
														string str = "";
														int quality = a3_ItemData4.quality;
														if (quality != 4)
														{
															if (quality == 5)
															{
																str = "<color=#FF7F0A>" + a3_ItemData4.item_name + "</color>";
															}
														}
														else
														{
															str = "<color=#FF00FF>" + a3_ItemData4.item_name + "</color>";
														}
														Variant singleMapConf = SvrMapConfig.instance.getSingleMapConf(uint.Parse(array[1]));
														string str2 = singleMapConf["map_name"];
														bool flag20 = a3_ItemData4.quality >= 4;
														if (flag20)
														{
															string text2 = "<color=#FF0000>" + array[0] + "</color>在";
															text2 = text2 + "<color=#E3B36B>" + str2 + "</color>";
															text2 = text2 + "拾取了" + str;
															broadcastingData.msg = text2;
															this.system_msg.Add(broadcastingData);
															bool flag21 = int.Parse(array[1]) > 3338;
															if (flag21)
															{
																iconActionData.iconOpen = true;
																iconActionData arg_A39_0 = iconActionData;
																Action<GameObject> arg_A39_1;
																if ((arg_A39_1 = broadcasting.<>c.<>9__27_3) == null)
																{
																	arg_A39_1 = (broadcasting.<>c.<>9__27_3 = new Action<GameObject>(broadcasting.<>c.<>9.<getNotice>b__27_3));
																}
																arg_A39_0.action = arg_A39_1;
																this.iconAction.Add(iconActionData);
															}
														}
													}
													else
													{
														bool flag22 = e.data["tp"] == 13;
														if (flag22)
														{
															bool flag23 = int.Parse(array[0]) == 1;
															if (flag23)
															{
																Variant singleMapConf2 = SvrMapConfig.instance.getSingleMapConf(uint.Parse(array[1]));
																string str3 = singleMapConf2["map_name"];
																int num2 = int.Parse(array[2]);
																string text3 = "<color=#FF0000>宝箱将" + num2 + "</color>分钟后在";
																text3 = text3 + "<color=#E3B36B>" + str3 + "</color>";
																text3 += "刷新";
																broadcastingData.msg = text3;
																this.system_msg.Add(broadcastingData);
																iconActionData.iconOpen = true;
																iconActionData arg_B49_0 = iconActionData;
																Action<GameObject> arg_B49_1;
																if ((arg_B49_1 = broadcasting.<>c.<>9__27_4) == null)
																{
																	arg_B49_1 = (broadcasting.<>c.<>9__27_4 = new Action<GameObject>(broadcasting.<>c.<>9.<getNotice>b__27_4));
																}
																arg_B49_0.action = arg_B49_1;
																this.iconAction.Add(iconActionData);
															}
															bool flag24 = int.Parse(array[0]) == 2;
															if (flag24)
															{
																int num2 = int.Parse(array[1]);
																bool flag25 = num2 < 5;
																string text3;
																if (flag25)
																{
																	text3 = "第 <color=#FF0000>" + num2 + "</color> 波宝箱已经刷新";
																}
																else
																{
																	text3 = "最后一波宝箱已经刷新";
																}
																broadcastingData.msg = text3;
																this.system_msg.Add(broadcastingData);
																iconActionData.iconOpen = true;
																iconActionData arg_BE7_0 = iconActionData;
																Action<GameObject> arg_BE7_1;
																if ((arg_BE7_1 = broadcasting.<>c.<>9__27_5) == null)
																{
																	arg_BE7_1 = (broadcasting.<>c.<>9__27_5 = new Action<GameObject>(broadcasting.<>c.<>9.<getNotice>b__27_5));
																}
																arg_BE7_0.action = arg_BE7_1;
																this.iconAction.Add(iconActionData);
															}
															bool flag26 = int.Parse(array[0]) == 3;
															if (flag26)
															{
																a3_ItemData a3_ItemData5 = default(a3_ItemData);
																a3_ItemData5 = ModelBase<a3_BagModel>.getInstance().getItemDataById(uint.Parse(array[2]));
																string str4 = "";
																int quality2 = a3_ItemData5.quality;
																if (quality2 != 4)
																{
																	if (quality2 == 5)
																	{
																		str4 = "<color=#FF7F0A>" + a3_ItemData5.item_name + "</color>";
																	}
																}
																else
																{
																	str4 = "<color=#FF00FF>" + a3_ItemData5.item_name + "</color>";
																}
																bool flag27 = a3_ItemData5.quality >= 4;
																if (flag27)
																{
																	string text3 = "<color=#FF0000>" + array[1] + "</color>在宝箱中";
																	text3 = text3 + "得到了" + str4;
																	broadcastingData.msg = text3;
																	this.system_msg.Add(broadcastingData);
																	iconActionData.iconOpen = true;
																	iconActionData arg_CF3_0 = iconActionData;
																	Action<GameObject> arg_CF3_1;
																	if ((arg_CF3_1 = broadcasting.<>c.<>9__27_6) == null)
																	{
																		arg_CF3_1 = (broadcasting.<>c.<>9__27_6 = new Action<GameObject>(broadcasting.<>c.<>9.<getNotice>b__27_6));
																	}
																	arg_CF3_0.action = arg_CF3_1;
																	this.iconAction.Add(iconActionData);
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
				}
			}
		}

		public void addMsg(string msg)
		{
			BroadcastingData broadcastingData = new BroadcastingData();
			broadcastingData.msg = msg;
			this.system_msg.Add(broadcastingData);
		}

		public void on_off(bool isOn)
		{
			if (!isOn)
			{
				this.openBtn.SetActive(false);
				broadcasting.talkList.Clear();
			}
		}

		private void onClickOpen(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_CHATROOM, null, false);
			this.num = 0;
			this.showMark();
		}

		private void showMark()
		{
			bool flag = this.num == 0;
			if (flag)
			{
				this.numMark.transform.parent.gameObject.SetActive(false);
			}
			else
			{
				this.numMark.text = this.num.ToString();
				this.numMark.transform.parent.gameObject.SetActive(true);
				InterfaceMgr.setUntouchable(this.numMark.transform.parent.gameObject);
				this.numMark.transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = false;
				bool flag2 = this.num > 99;
				if (flag2)
				{
					this.num = 99;
				}
			}
		}

		private void Update()
		{
			bool flag = broadcasting.talkList.Count != 0 && this._data == null;
			if (flag)
			{
				this._data = broadcasting.talkList[0];
			}
			bool flag2 = this._data != null && !this.isBuilded;
			if (flag2)
			{
				this.bg.sizeDelta = new Vector2(this.bg.sizeDelta.x, 40f);
				this.currentText = UnityEngine.Object.Instantiate<GameObject>(this.Text);
				this.currentText.transform.SetParent(this.bg);
				this.currentText.transform.localScale = Vector3.one;
				this.currentText.SetActive(true);
				this.currentText.GetComponent<Text>().text = this._data.name + ":" + KeyWord.filter(this._data.msg);
				this.currentRect = this.currentText.GetComponent<RectTransform>();
				this.currentRect.anchoredPosition = new Vector3(10f, 31f, 0f);
				this.isBuilded = true;
			}
			bool flag3 = this.currentText != null;
			if (flag3)
			{
				bool flag4 = this.hight > 0f;
				if (flag4)
				{
					this.hight -= Time.deltaTime * 31f;
					this.currentRect.anchoredPosition = new Vector3(10f, this.currentRect.anchoredPosition.y - Time.deltaTime * 31f, 0f);
				}
				else
				{
					bool flag5 = broadcasting.talkList.Count != 0;
					if (flag5)
					{
						broadcasting.talkList.Remove(broadcasting.talkList[0]);
					}
					this._data = null;
					this.pastText = this.currentText;
					this.pastRect = this.currentRect;
					this.currentText = null;
					this.currentRect = null;
					this.hight = 31f;
					this.isBuilded = false;
				}
			}
			bool flag6 = this.pastText != null;
			if (flag6)
			{
				bool flag7 = broadcasting.talkList.Count == 0;
				if (flag7)
				{
					this.timer += Time.deltaTime;
					bool flag8 = this.timer >= 3f;
					if (flag8)
					{
						this.bg.sizeDelta = new Vector2(this.bg.sizeDelta.x, this.bg.sizeDelta.y - Time.deltaTime * 46f);
						bool flag9 = this.bg.sizeDelta.y <= 0f;
						if (flag9)
						{
							this.pastText.SetActive(false);
						}
						bool flag10 = this.bg.sizeDelta.y <= 0f;
						if (flag10)
						{
							UnityEngine.Object.Destroy(this.pastText);
							this.pastText = null;
							this.timer = 0f;
						}
					}
				}
				else
				{
					this.timer = 0f;
					this.pastRect.anchoredPosition = new Vector3(10f, this.pastRect.anchoredPosition.y - Time.deltaTime * 46f, 0f);
					bool flag11 = this.pastRect.anchoredPosition.y <= -20f;
					if (flag11)
					{
						UnityEngine.Object.Destroy(this.pastText);
						this.pastText = null;
					}
				}
			}
			bool flag12 = this.system_msg.Count != 0 && this.canGetNext;
			if (flag12)
			{
				bool flag13 = !this.mask.activeSelf;
				if (flag13)
				{
					this.mask.SetActive(true);
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.child);
				gameObject.transform.FindChild("msg_system/Text").GetComponent<Text>().text = this.system_msg[0].msg;
				gameObject.transform.SetParent(base.getTransformByPath("mask"));
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.SetActive(true);
				bool flag14 = this.iconAction.Count > 0 && this.iconAction[0].iconOpen;
				if (flag14)
				{
					gameObject.transform.FindChild("icon").gameObject.SetActive(true);
					new BaseButton(gameObject.transform.FindChild("icon"), 1, 1).onClick = this.iconAction[0].action;
				}
				else
				{
					gameObject.transform.FindChild("icon").gameObject.SetActive(false);
				}
				this.canGetNext = false;
				UnityEngine.Object.Destroy(gameObject, 4f);
				base.StartCoroutine(this.waitShowNextChild());
			}
			bool flag15 = this.system_msg.Count <= 0 && this.mask.activeSelf;
			if (flag15)
			{
				this.mask.SetActive(false);
			}
			bool flag16 = broadcasting.OutTimer > 0f;
			if (flag16)
			{
				broadcasting.OutTimer -= Time.deltaTime;
			}
		}

		private IEnumerator waitShowNextChild()
		{
			yield return new WaitForSeconds(4f);
			this.system_msg.Remove(this.system_msg[0]);
			bool flag = this.iconAction.Count > 0;
			if (flag)
			{
				this.iconAction.Remove(this.iconAction[0]);
			}
			this.canGetNext = true;
			yield break;
		}
	}
}
