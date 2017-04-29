using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_buff : FloatUi
	{
		public static a3_buff instance;

		public GameObject buff_tp;

		public GameObject buff_info;

		public Transform buff_num;

		public Transform contain;

		public GameObject pre;

		private bool legion_bf;

		private bool pet_bf;

		public int num;

		public long endCD_exp;

		public Transform exp_pos;

		public string name_exp;

		public bool timego;

		private Transform bless_pos;

		private string name_bless;

		private long endCD_bless;

		private uint[] skill_id = new uint[11];

		private Transform fuwen_pos;

		private string name_fuwen;

		private long endCD_fuwen;

		private Transform[] skill_pos = new Transform[11];

		private string[] name_skill = new string[11];

		private long[] endCD_skill = new long[11];

		public override void init()
		{
			a3_buff.instance = this;
			this.buff_tp = base.transform.FindChild("view_buff").gameObject;
			this.buff_info = base.transform.FindChild("buff_info").gameObject;
			this.buff_num = this.buff_tp.transform.FindChild("con/text");
			this.contain = this.buff_info.transform.FindChild("view/con");
			this.pre = this.buff_info.transform.FindChild("view/temp").gameObject;
			this.buff_info.SetActive(false);
			this.buff_tp.SetActive(false);
			new BaseButton(this.buff_tp.transform, 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = !this.buff_info.activeInHierarchy;
				if (flag)
				{
					this.buff_info.SetActive(true);
				}
				else
				{
					this.buff_info.SetActive(false);
				}
			};
			this.legion_pet_buff();
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(9u, new Action<GameEvent>(this.Quit));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(this.Join));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(22u, new Action<GameEvent>(this.Deleteclan));
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_HAVE_PET, new Action<GameEvent>(this.closePet));
			BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_GET_LAST_TIME, new Action<GameEvent>(this.get_pettime));
			BaseProxy<BattleProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_GET_LAST_TIME, new Action<GameEvent>(this.reshbuff));
		}

		private void reshbuff(GameEvent obj)
		{
			this.resh_buff();
		}

		private void get_pettime(GameEvent e)
		{
			this.pet_bf = true;
			this.num++;
			this.resh_buff();
		}

		private void closePet(GameEvent e)
		{
			this.pet_bf = false;
			this.num--;
			this.resh_buff();
		}

		private void OpenPet(GameEvent e)
		{
			this.pet_bf = true;
			this.num++;
			this.resh_buff();
		}

		private void Deleteclan(GameEvent e)
		{
			bool flag = ModelBase<PlayerModel>.getInstance().clan_buff_lvl <= 0;
			if (!flag)
			{
				this.legion_bf = false;
				this.num--;
				this.resh_buff();
			}
		}

		private void Join(GameEvent e)
		{
			bool flag = ModelBase<PlayerModel>.getInstance().clan_buff_lvl <= 0;
			if (!flag)
			{
				this.legion_bf = true;
				this.num++;
				this.resh_buff();
			}
		}

		private void Quit(GameEvent e)
		{
			bool flag = ModelBase<PlayerModel>.getInstance().clan_buff_lvl <= 0;
			if (!flag)
			{
				this.legion_bf = false;
				this.num--;
				this.resh_buff();
			}
		}

		public void Quited()
		{
			bool flag = ModelBase<PlayerModel>.getInstance().clan_buff_lvl <= 0;
			if (!flag)
			{
				this.legion_bf = false;
				this.num--;
				this.resh_buff();
			}
		}

		private void legion_pet_buff()
		{
			bool flag = ModelBase<PlayerModel>.getInstance().clanid != 0u && ModelBase<PlayerModel>.getInstance().clan_buff_lvl > 0;
			if (flag)
			{
				this.legion_bf = true;
				this.num++;
			}
			bool flag2 = ModelBase<PlayerModel>.getInstance().havePet && ModelBase<PlayerModel>.getInstance().last_time != 0;
			if (flag2)
			{
				this.pet_bf = true;
				this.num++;
			}
		}

		public void legion_buf()
		{
			this.legion_bf = true;
			this.num++;
			this.resh_buff();
		}

		public override void onShowed()
		{
			this.resh_buff();
		}

		public void resh_buff()
		{
			ProfessionRole expr_06 = SelfRole._inst;
			bool flag = expr_06 != null && expr_06.isDead;
			if (!flag)
			{
				this.Clear_con();
				Dictionary<uint, BuffInfo> buffCd = ModelBase<A3_BuffModel>.getInstance().BuffCd;
				this.Set_cancel();
				bool flag2 = buffCd.Count == 0 && this.num == 0;
				if (flag2)
				{
					this.buff_tp.SetActive(false);
					this.buff_info.SetActive(false);
				}
				else
				{
					this.buff_tp.SetActive(true);
					bool flag3 = this.num < 0;
					if (flag3)
					{
						this.num = 0;
					}
					this.buff_num.GetComponent<Text>().text = "buff * " + (buffCd.Count + this.num);
					this.contain.GetComponent<RectTransform>().sizeDelta = new Vector2(this.pre.GetComponent<RectTransform>().sizeDelta.x, this.pre.GetComponent<RectTransform>().sizeDelta.y * (float)(buffCd.Count + this.num));
					foreach (uint current in ModelBase<A3_BuffModel>.getInstance().BuffCd.Keys)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pre);
						gameObject.transform.SetParent(this.contain);
						gameObject.transform.localScale = Vector3.one;
						gameObject.SetActive(true);
						this.Set_Line(gameObject.transform, buffCd[current]);
					}
					bool flag4 = this.num != 0;
					if (flag4)
					{
						bool flag5 = this.legion_bf && ModelBase<PlayerModel>.getInstance().clan_buff_lvl > 0;
						if (flag5)
						{
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.pre);
							gameObject2.transform.SetParent(this.contain);
							gameObject2.transform.localScale = Vector3.one;
							gameObject2.SetActive(true);
							gameObject2.transform.FindChild("item_text").GetComponent<Text>().text = "荣耀与共";
							SXML sXML = XMLMgr.instance.GetSXML("clan.clan_buff", "lvl==" + ModelBase<PlayerModel>.getInstance().clan_buff_lvl);
							gameObject2.transform.FindChild("Text").GetComponent<Text>().text = sXML.getString("buff_dc");
						}
						bool flag6 = this.pet_bf;
						if (flag6)
						{
							GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.pre);
							gameObject3.transform.SetParent(this.contain);
							gameObject3.transform.localScale = Vector3.one;
							gameObject3.SetActive(true);
							gameObject3.transform.FindChild("item_text").GetComponent<Text>().text = "宠物";
							bool flag7 = A3_PetModel.curPetid == 0u;
							if (flag7)
							{
								A3_PetModel.curPetid = 2u;
							}
							SXML sXML2 = XMLMgr.instance.GetSXML("newpet.pet", "id==" + A3_PetModel.curPetid);
							gameObject3.transform.FindChild("Text").GetComponent<Text>().text = sXML2.getString("buff_dc");
						}
					}
				}
			}
		}

		private void Set_cancel()
		{
			for (int i = 0; i < 10; i++)
			{
				bool flag = !ModelBase<A3_BuffModel>.getInstance().BuffCd.ContainsKey(this.skill_id[i]);
				if (flag)
				{
					bool flag2 = base.IsInvoking("do_skillCD_" + i);
					if (flag2)
					{
						base.CancelInvoke("do_skillCD_" + i);
					}
				}
			}
		}

		private void Clear_con()
		{
			bool flag = this.contain.childCount == 0;
			if (!flag)
			{
				for (int i = 0; i < this.contain.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.contain.GetChild(i).gameObject);
				}
			}
		}

		private void Set_Line(Transform go, BuffInfo v)
		{
			XMLMgr expr_06 = XMLMgr.instance;
			SXML sXML = (expr_06 != null) ? expr_06.GetSXML("skill.state", "id==" + v.id) : null;
			bool flag = v.id == 10001u;
			if (flag)
			{
				this.exp_pos = go.transform.FindChild("item_text");
				this.name_exp = sXML.getString("name");
				bool flag2 = base.IsInvoking("do_expCD");
				if (flag2)
				{
					this.exp_pos = go.transform.FindChild("item_text");
					this.endCD_exp = (long)((ulong)((v.end_time - v.start_time) / 1000u));
					base.CancelInvoke("do_expCD");
				}
				else
				{
					this.endCD_exp = (long)((ulong)((v.end_time - v.start_time) / 1000u));
				}
				base.InvokeRepeating("do_expCD", 0f, 1f);
				go.transform.FindChild("Text").GetComponent<Text>().text = sXML.getString("desc");
			}
			else
			{
				bool flag3 = v.id == 99996u || v.id == 99997u || v.id == 99998u || v.id == 99999u;
				if (flag3)
				{
					go.transform.FindChild("item_text").GetComponent<Text>().text = sXML.getString("name");
					go.transform.FindChild("Text").GetComponent<Text>().text = sXML.getString("desc");
				}
				else
				{
					bool flag4 = v.id == 10000u;
					if (flag4)
					{
						this.bless_pos = go.transform.FindChild("item_text");
						this.name_bless = sXML.getString("name");
						bool flag5 = base.IsInvoking("do_blessCD");
						if (flag5)
						{
							this.bless_pos = go.transform.FindChild("item_text");
							base.CancelInvoke("do_blessCD");
						}
						else
						{
							this.endCD_bless = (long)((ulong)((v.end_time - v.start_time) / 1000u));
						}
						base.InvokeRepeating("do_blessCD", 0f, 1f);
						go.transform.FindChild("Text").GetComponent<Text>().text = sXML.getString("desc");
					}
					else
					{
						bool flag6 = (v.id > 0u && v.id < 203u) || (v.id > 3000u && v.id < 3101u);
						if (flag6)
						{
							this.fuwen_pos = go.transform.FindChild("item_text");
							this.name_fuwen = sXML.getString("name");
							bool flag7 = base.IsInvoking("do_fuwenCD");
							if (flag7)
							{
								this.fuwen_pos = go.transform.FindChild("item_text");
								base.CancelInvoke("do_fuwenCD");
							}
							else
							{
								this.endCD_fuwen = (long)((ulong)((v.end_time - v.start_time) / 1000u));
							}
							base.InvokeRepeating("do_fuwenCD", 0f, 1f);
							go.transform.FindChild("Text").GetComponent<Text>().text = sXML.getString("desc");
						}
						else
						{
							bool flag8 = v.id >= 300u && v.id < 6050u;
							if (flag8)
							{
								XMLMgr expr_39F = XMLMgr.instance;
								SXML sXML2 = (expr_39F != null) ? expr_39F.GetSXML("skill.skill", "id==" + sXML.getInt("skill_id")) : null;
								List<SXML> nodeList = sXML2.GetNodeList("skill_att", "");
								for (int i = 0; i < nodeList.Count; i++)
								{
									SXML node = nodeList[i].GetNode("sres", "");
									bool flag9 = node == null;
									if (flag9)
									{
										node = nodeList[i].GetNode("tres", "");
										bool flag10 = (long)node.getInt("tar_state") == (long)((ulong)v.id);
										if (flag10)
										{
											go.transform.FindChild("Text").GetComponent<Text>().text = nodeList[i].getString("descr3");
										}
									}
									else
									{
										bool flag11 = (long)node.getInt("tar_state") == (long)((ulong)v.id);
										if (flag11)
										{
											go.transform.FindChild("Text").GetComponent<Text>().text = nodeList[i].getString("descr2");
										}
									}
								}
								int @int = sXML.getInt("skill_id");
								if (@int <= 2010)
								{
									if (@int == 2005)
									{
										this.skill_id[1] = v.id;
										this.skill_pos[1] = go.transform.FindChild("item_text");
										this.name_skill[1] = sXML.getString("name");
										bool flag12 = base.IsInvoking("do_skillCD_1");
										if (flag12)
										{
											this.skill_pos[1] = go.transform.FindChild("item_text");
											base.CancelInvoke("do_skillCD_1");
										}
										else
										{
											this.endCD_skill[1] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
										}
										base.InvokeRepeating("do_skillCD_1", 0f, 1f);
										goto IL_D13;
									}
									if (@int == 2008)
									{
										this.skill_id[8] = v.id;
										this.skill_pos[8] = go.transform.FindChild("item_text");
										this.name_skill[8] = sXML.getString("name");
										bool flag13 = base.IsInvoking("do_skillCD_8");
										if (flag13)
										{
											this.skill_pos[8] = go.transform.FindChild("item_text");
											base.CancelInvoke("do_skillCD_8");
										}
										else
										{
											this.endCD_skill[8] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
										}
										base.InvokeRepeating("do_skillCD_8", 0f, 1f);
										goto IL_D13;
									}
									if (@int == 2010)
									{
										this.skill_id[2] = v.id;
										this.skill_pos[2] = go.transform.FindChild("item_text");
										this.name_skill[2] = sXML.getString("name");
										bool flag14 = base.IsInvoking("do_skillCD_2");
										if (flag14)
										{
											this.skill_pos[2] = go.transform.FindChild("item_text");
											base.CancelInvoke("do_skillCD_2");
										}
										else
										{
											this.endCD_skill[2] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
										}
										base.InvokeRepeating("do_skillCD_2", 0f, 1f);
										goto IL_D13;
									}
								}
								else
								{
									switch (@int)
									{
									case 3008:
									{
										this.skill_id[3] = v.id;
										this.skill_pos[3] = go.transform.FindChild("item_text");
										this.name_skill[3] = sXML.getString("name");
										bool flag15 = base.IsInvoking("do_skillCD_3");
										if (flag15)
										{
											this.skill_pos[3] = go.transform.FindChild("item_text");
											base.CancelInvoke("do_skillCD_3");
										}
										else
										{
											this.endCD_skill[3] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
										}
										base.InvokeRepeating("do_skillCD_3", 0f, 1f);
										goto IL_D13;
									}
									case 3009:
									{
										this.skill_id[9] = v.id;
										this.skill_pos[9] = go.transform.FindChild("item_text");
										this.name_skill[9] = sXML.getString("name");
										bool flag16 = base.IsInvoking("do_skillCD_9");
										if (flag16)
										{
											this.skill_pos[3] = go.transform.FindChild("item_text");
											base.CancelInvoke("do_skillCD_9");
										}
										else
										{
											this.endCD_skill[9] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
										}
										base.InvokeRepeating("do_skillCD_9", 0f, 1f);
										goto IL_D13;
									}
									case 3010:
									{
										this.skill_id[4] = v.id;
										this.skill_pos[4] = go.transform.FindChild("item_text");
										this.name_skill[4] = sXML.getString("name");
										bool flag17 = base.IsInvoking("do_skillCD_4");
										if (flag17)
										{
											this.skill_pos[4] = go.transform.FindChild("item_text");
											base.CancelInvoke("do_skillCD_4");
										}
										else
										{
											this.endCD_skill[4] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
										}
										base.InvokeRepeating("do_skillCD_4", 0f, 1f);
										goto IL_D13;
									}
									default:
										switch (@int)
										{
										case 5005:
										{
											this.skill_id[5] = v.id;
											this.skill_pos[5] = go.transform.FindChild("item_text");
											this.name_skill[5] = sXML.getString("name");
											bool flag18 = base.IsInvoking("do_skillCD_5");
											if (flag18)
											{
												this.skill_pos[5] = go.transform.FindChild("item_text");
												base.CancelInvoke("do_skillCD_5");
											}
											else
											{
												this.endCD_skill[5] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
											}
											base.InvokeRepeating("do_skillCD_5", 0f, 1f);
											goto IL_D13;
										}
										case 5006:
										case 5007:
											break;
										case 5008:
										{
											this.skill_id[0] = v.id;
											this.skill_pos[0] = go.transform.FindChild("item_text");
											this.name_skill[0] = sXML.getString("name");
											bool flag19 = base.IsInvoking("do_skillCD_0");
											if (flag19)
											{
												this.skill_pos[0] = go.transform.FindChild("item_text");
												base.CancelInvoke("do_skillCD_0");
											}
											else
											{
												this.endCD_skill[0] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
											}
											base.InvokeRepeating("do_skillCD_0", 0f, 1f);
											goto IL_D13;
										}
										case 5009:
										{
											this.skill_id[6] = v.id;
											this.skill_pos[6] = go.transform.FindChild("item_text");
											this.name_skill[6] = sXML.getString("name");
											bool flag20 = base.IsInvoking("do_skillCD_6");
											if (flag20)
											{
												this.skill_pos[6] = go.transform.FindChild("item_text");
												base.CancelInvoke("do_skillCD_6");
											}
											else
											{
												this.endCD_skill[6] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
											}
											base.InvokeRepeating("do_skillCD_6", 0f, 1f);
											goto IL_D13;
										}
										case 5010:
										{
											this.skill_id[7] = v.id;
											this.skill_pos[7] = go.transform.FindChild("item_text");
											this.name_skill[7] = sXML.getString("name");
											bool flag21 = base.IsInvoking("do_skillCD_7");
											if (flag21)
											{
												this.skill_pos[7] = go.transform.FindChild("item_text");
												base.CancelInvoke("do_skillCD_7");
											}
											else
											{
												this.endCD_skill[7] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
											}
											base.InvokeRepeating("do_skillCD_7", 0f, 1f);
											goto IL_D13;
										}
										default:
											if (@int == 6053)
											{
												this.skill_id[10] = v.id;
												this.skill_pos[10] = go.transform.FindChild("item_text");
												this.name_skill[10] = sXML.getString("name");
												bool flag22 = base.IsInvoking("do_skillCD_10");
												if (flag22)
												{
													this.skill_pos[10] = go.transform.FindChild("item_text");
													base.CancelInvoke("do_skillCD_10");
												}
												else
												{
													this.endCD_skill[10] = (long)((ulong)((v.end_time - v.start_time) / 1000u));
												}
												base.InvokeRepeating("do_skillCD_10", 0f, 1f);
												goto IL_D13;
											}
											break;
										}
										break;
									}
								}
								go.transform.FindChild("item_text").GetComponent<Text>().text = "额外buff";
								go.transform.FindChild("Text").GetComponent<Text>().text = "额外的buff属性，提供攻击防御属性";
								IL_D13:;
							}
							else
							{
								go.transform.FindChild("item_text").GetComponent<Text>().text = "额外buff";
								go.transform.FindChild("Text").GetComponent<Text>().text = "额外的buff属性，提供攻击防御属性";
							}
						}
					}
				}
			}
		}

		public void do_skillCD_0()
		{
			this.endCD_skill[0] -= 1L;
			bool flag = this.endCD_skill[0] <= 0L;
			if (flag)
			{
				this.endCD_skill[0] = 0L;
				base.CancelInvoke("do_skillCD_0");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[0]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[0] % 3600L / 60L, this.endCD_skill[0] % 60L);
				bool flag2 = this.skill_pos[0];
				if (flag2)
				{
					this.skill_pos[0].GetComponent<Text>().text = this.name_skill[0] + "(" + str + ")";
				}
			}
		}

		public void do_skillCD_1()
		{
			this.endCD_skill[1] -= 1L;
			bool flag = this.endCD_skill[1] <= 0L;
			if (flag)
			{
				this.endCD_skill[1] = 0L;
				base.CancelInvoke("do_skillCD_1");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[1]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[1] % 3600L / 60L, this.endCD_skill[1] % 60L);
				bool flag2 = this.skill_pos[1];
				if (flag2)
				{
					this.skill_pos[1].GetComponent<Text>().text = this.name_skill[1] + "(" + str + ")";
				}
			}
		}

		public void do_skillCD_2()
		{
			this.endCD_skill[2] -= 1L;
			bool flag = this.endCD_skill[2] <= 0L;
			if (flag)
			{
				this.endCD_skill[2] = 0L;
				base.CancelInvoke("do_skillCD_2");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[2]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[2] % 3600L / 60L, this.endCD_skill[2] % 60L);
				bool flag2 = this.skill_pos[2];
				if (flag2)
				{
					this.skill_pos[2].GetComponent<Text>().text = this.name_skill[2] + "(" + str + ")";
				}
			}
		}

		public void do_skillCD_3()
		{
			this.endCD_skill[3] -= 1L;
			bool flag = this.endCD_skill[3] <= 0L;
			if (flag)
			{
				this.endCD_skill[3] = 0L;
				base.CancelInvoke("do_skillCD_3");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[3]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[3] % 3600L / 60L, this.endCD_skill[3] % 60L);
				bool flag2 = this.skill_pos[3];
				if (flag2)
				{
					this.skill_pos[3].GetComponent<Text>().text = this.name_skill[3] + "(" + str + ")";
				}
			}
		}

		public void do_skillCD_4()
		{
			this.endCD_skill[4] -= 1L;
			bool flag = this.endCD_skill[4] <= 0L;
			if (flag)
			{
				this.endCD_skill[4] = 0L;
				base.CancelInvoke("do_skillCD_4");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[4]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[4] % 3600L / 60L, this.endCD_skill[4] % 60L);
				bool flag2 = this.skill_pos[4];
				if (flag2)
				{
					this.skill_pos[4].GetComponent<Text>().text = this.name_skill[4] + "(" + str + ")";
				}
			}
		}

		public void do_skillCD_5()
		{
			this.endCD_skill[5] -= 1L;
			bool flag = this.endCD_skill[5] <= 0L;
			if (flag)
			{
				this.endCD_skill[5] = 0L;
				base.CancelInvoke("do_skillCD_5");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[5]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[5] % 3600L / 60L, this.endCD_skill[5] % 60L);
				bool flag2 = this.skill_pos[5];
				if (flag2)
				{
					this.skill_pos[5].GetComponent<Text>().text = this.name_skill[5] + "(" + str + ")";
				}
			}
		}

		public void do_skillCD_6()
		{
			this.endCD_skill[6] -= 1L;
			bool flag = this.endCD_skill[6] <= 0L;
			if (flag)
			{
				this.endCD_skill[6] = 0L;
				base.CancelInvoke("do_skillCD_6");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[6]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[6] % 3600L / 60L, this.endCD_skill[6] % 60L);
				bool flag2 = this.skill_pos[6];
				if (flag2)
				{
					this.skill_pos[6].GetComponent<Text>().text = this.name_skill[6] + "(" + str + ")";
				}
			}
		}

		public void do_skillCD_7()
		{
			this.endCD_skill[7] -= 1L;
			bool flag = this.endCD_skill[7] <= 0L;
			if (flag)
			{
				this.endCD_skill[7] = 0L;
				base.CancelInvoke("do_skillCD_7");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[7]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[7] % 3600L / 60L, this.endCD_skill[7] % 60L);
				bool flag2 = this.skill_pos[7];
				if (flag2)
				{
					this.skill_pos[7].GetComponent<Text>().text = this.name_skill[7] + "(" + str + ")";
				}
			}
		}

		public void do_skillCD_8()
		{
			this.endCD_skill[8] -= 1L;
			bool flag = this.endCD_skill[8] <= 0L;
			if (flag)
			{
				this.endCD_skill[8] = 0L;
				base.CancelInvoke("do_skillCD_8");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[8]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[8] % 3600L / 60L, this.endCD_skill[8] % 60L);
				bool flag2 = this.skill_pos[8];
				if (flag2)
				{
					this.skill_pos[8].GetComponent<Text>().text = this.name_skill[8] + "(" + str + ")";
				}
			}
		}

		public void do_skillCD_9()
		{
			this.endCD_skill[9] -= 1L;
			bool flag = this.endCD_skill[9] <= 0L;
			if (flag)
			{
				this.endCD_skill[9] = 0L;
				base.CancelInvoke("do_skillCD_9");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[9]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[9] % 3600L / 60L, this.endCD_skill[9] % 60L);
				bool flag2 = this.skill_pos[9];
				if (flag2)
				{
					this.skill_pos[9].GetComponent<Text>().text = this.name_skill[9] + "(" + str + ")";
				}
			}
		}

		public void do_skillCD_10()
		{
			this.endCD_skill[10] -= 1L;
			bool flag = this.endCD_skill[10] <= 0L;
			if (flag)
			{
				this.endCD_skill[10] = 0L;
				base.CancelInvoke("do_skillCD_10");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(this.skill_id[10]);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_skill[10] % 3600L / 60L, this.endCD_skill[10] % 60L);
				bool flag2 = this.skill_pos[10];
				if (flag2)
				{
					this.skill_pos[10].GetComponent<Text>().text = this.name_skill[10] + "(" + str + ")";
				}
			}
		}

		public void do_expCD()
		{
			this.endCD_exp -= 1L;
			bool flag = this.endCD_exp <= 0L;
			if (flag)
			{
				this.endCD_exp = 0L;
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(10001u);
				base.CancelInvoke("do_expCD");
			}
			else
			{
				bool flag2 = this.endCD_exp > 3600L;
				string str;
				if (flag2)
				{
					str = string.Format("{0:D2}:{1:D2}:{2:D2}", this.endCD_exp / 3600L, this.endCD_exp % 3600L / 60L, this.endCD_exp % 60L);
				}
				else
				{
					str = string.Format("{0:D2}:{1:D2}", this.endCD_exp % 3600L / 60L, this.endCD_exp % 60L);
				}
				bool flag3 = this.exp_pos;
				if (flag3)
				{
					this.exp_pos.GetComponent<Text>().text = this.name_exp + "(" + str + ")";
				}
			}
		}

		public void do_fuwenCD()
		{
			this.endCD_fuwen -= 1L;
			bool flag = this.endCD_fuwen <= 0L;
			if (flag)
			{
				this.endCD_fuwen = 0L;
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_fuwen % 3600L / 60L, this.endCD_fuwen % 60L);
				bool flag2 = this.fuwen_pos;
				if (flag2)
				{
					this.fuwen_pos.GetComponent<Text>().text = this.name_fuwen + "(" + str + ")";
				}
			}
		}

		public void do_blessCD()
		{
			this.endCD_bless -= 1L;
			bool flag = this.endCD_bless <= 0L;
			if (flag)
			{
				this.endCD_bless = 0L;
				base.CancelInvoke("do_blessCD");
				ModelBase<A3_BuffModel>.getInstance().RemoveBuff(10000u);
			}
			else
			{
				string str = string.Format("{0:D2}:{1:D2}", this.endCD_bless % 3600L / 60L, this.endCD_bless % 60L);
				bool flag2 = this.bless_pos;
				if (flag2)
				{
					this.bless_pos.GetComponent<Text>().text = this.name_bless + "(" + str + ")";
				}
			}
		}
	}
}
