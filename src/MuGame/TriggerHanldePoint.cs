using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class TriggerHanldePoint : MonoBehaviour
	{
		public int type = 0;

		public List<int> paramInts = new List<int>();

		public List<float> paramFloat = new List<float>();

		public List<GameObject> paramGo = new List<GameObject>();

		public List<string> paramStr = new List<string>();

		public bool paramBool;

		public static List<GameObject> lGo;

		public virtual void onTriggerHanlde()
		{
			bool flag = this.type == 1;
			if (flag)
			{
				bool flag2 = this.paramInts.Count == 0;
				if (!flag2)
				{
					MonsterMgr._inst.AddMonster(this.paramInts[0], base.transform.position, 0u, base.transform.localEulerAngles.y, 0, 0, null);
				}
			}
			else
			{
				bool flag3 = this.type == 2;
				if (flag3)
				{
					bool flag4 = this.paramGo.Count == 0;
					if (!flag4)
					{
						foreach (GameObject current in this.paramGo)
						{
							current.SetActive(true);
						}
					}
				}
				else
				{
					bool flag5 = this.type == 3;
					if (flag5)
					{
						bool flag6 = this.paramGo.Count == 0;
						if (!flag6)
						{
							foreach (GameObject current2 in this.paramGo)
							{
								HiddenItem component = current2.GetComponent<HiddenItem>();
								bool flag7 = component != null;
								if (flag7)
								{
									component.hide();
								}
								else
								{
									current2.SetActive(false);
								}
							}
						}
					}
					else
					{
						bool flag8 = this.type == 4;
						if (flag8)
						{
							bool flag9 = this.paramInts.Count == 0;
							if (!flag9)
							{
								int num = 0;
								for (int i = 0; i < this.paramInts.Count; i++)
								{
									num += NavmeshUtils.listARE[this.paramInts[i]];
								}
								bool flag10 = num == 0;
								if (!flag10)
								{
									SelfRole._inst.setNavLay(num);
								}
							}
						}
						else
						{
							bool flag11 = this.type == 5;
							if (flag11)
							{
								bool flag12 = this.paramGo.Count == 0;
								if (!flag12)
								{
									Transform transform = this.paramGo[0].transform;
									for (int j = 0; j < transform.childCount; j++)
									{
										GameObject gameObject = transform.GetChild(j).gameObject;
										gameObject.AddComponent<BrokenIten>();
									}
								}
							}
							else
							{
								bool flag13 = this.type == 6;
								if (flag13)
								{
									bool flag14 = this.paramGo.Count == 0;
									if (!flag14)
									{
										bool flag15 = this.paramFloat.Count == 0;
										if (!flag15)
										{
											bool flag16 = this.paramBool;
											if (flag16)
											{
												InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_HIDE_ALL);
												joystick.instance.stopDrag();
											}
											this.paramGo[1].SetActive(true);
											SceneCamera.changeAniCamera(this.paramGo[0], this.paramFloat[0]);
										}
									}
								}
								else
								{
									bool flag17 = this.type == 8;
									if (flag17)
									{
										bool flag18 = this.paramGo.Count == 0;
										if (!flag18)
										{
											this.paramGo[0].SetActive(true);
											HiddenItem component2 = this.paramGo[0].GetComponent<HiddenItem>();
											bool flag19 = component2 != null;
											if (flag19)
											{
												component2.hide();
											}
											else
											{
												this.paramGo[0].SetActive(false);
											}
										}
									}
									else
									{
										bool flag20 = this.type == 9;
										if (flag20)
										{
											bool flag21 = this.paramFloat.Count < 2;
											if (!flag21)
											{
												bool flag22 = this.paramInts.Count < 1;
												if (!flag22)
												{
													SceneCamera.cameraShake(this.paramFloat[0], this.paramInts[0], this.paramFloat[1]);
												}
											}
										}
										else
										{
											bool flag23 = this.type == 10;
											if (flag23)
											{
												bool flag24 = this.paramFloat.Count < 1;
												if (!flag24)
												{
													bool flag25 = this.paramGo.Count < 1;
													if (!flag25)
													{
														GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.paramGo[0]);
														gameObject2.transform.SetParent(SelfRole._inst.m_curModel, false);
														UnityEngine.Object.Destroy(gameObject2, this.paramFloat[0]);
													}
												}
											}
											else
											{
												bool flag26 = this.type == 11;
												if (flag26)
												{
													bool flag27 = this.paramStr.Count < 1;
													if (!flag27)
													{
														List<string> ldialog = this.paramStr;
														NpcRole npc = null;
														bool flag28 = this.paramGo != null && this.paramGo.Count > 0 && this.paramGo[0] != null;
														if (flag28)
														{
															npc = this.paramGo[0].GetComponent<NpcRole>();
														}
														DoAfterMgr.instacne.addAfterRender(delegate
														{
															dialog.showTalk(ldialog, null, npc, false);
														});
													}
												}
												else
												{
													bool flag29 = this.type == 12;
													if (flag29)
													{
														bool flag30 = this.paramStr.Count < 1;
														if (!flag30)
														{
															bool flag31 = this.paramGo == null || this.paramGo.Count == 0 || this.paramGo[0] == null;
															if (flag31)
															{
																SelfRole._inst.m_curAni.SetTrigger(this.paramStr[0]);
															}
															else
															{
																Animator component3 = this.paramGo[0].GetComponent<Animator>();
																bool flag32 = component3 != null;
																if (flag32)
																{
																	component3.SetTrigger(this.paramStr[0]);
																}
															}
														}
													}
													else
													{
														bool flag33 = this.type == 13;
														if (flag33)
														{
															bool flag34 = this.paramStr.Count < 1;
															if (!flag34)
															{
																NewbieTeachMgr.getInstance().add(this.paramStr, -1);
															}
														}
														else
														{
															bool flag35 = this.type == 14;
															if (flag35)
															{
																bool flag36 = !this.paramBool;
																if (flag36)
																{
																	InterfaceMgr.getInstance().floatUI.transform.localScale = Vector3.zero;
																	bool flag37 = joystick.instance != null;
																	if (flag37)
																	{
																		joystick.instance.OnDragOut(null);
																	}
																}
																else
																{
																	InterfaceMgr.getInstance().floatUI.transform.localScale = Vector3.one;
																}
															}
															else
															{
																bool flag38 = this.type == 15;
																if (flag38)
																{
																	TriggerHanldePoint.lGo = this.paramGo;
																}
																else
																{
																	bool flag39 = this.type == 16;
																	if (flag39)
																	{
																		bool flag40 = this.paramGo.Count == 0;
																		if (!flag40)
																		{
																			foreach (GameObject current3 in this.paramGo)
																			{
																				TriggerHanldePoint component4 = current3.GetComponent<TriggerHanldePoint>();
																				bool flag41 = component4 != null;
																				if (flag41)
																				{
																					component4.onTriggerHanlde();
																				}
																			}
																		}
																	}
																	else
																	{
																		bool flag42 = this.type == 17;
																		if (flag42)
																		{
																			bool flag43 = maploading.instance != null;
																			if (flag43)
																			{
																				maploading.instance.closeLoadWait(0.5f);
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
				}
			}
		}
	}
}
