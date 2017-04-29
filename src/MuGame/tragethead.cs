using DG.Tweening;
using GameFramework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class tragethead : FloatUi
	{
		public GameObject iconPhy;

		public GameObject iconMagic;

		public GameObject iconJy;

		public GameObject iconWar;

		public GameObject iconMage;

		public GameObject iconAss;

		public GameObject bossBg;

		public GameObject bg;

		public GameObject otherHP;

		public GameObject bosshp1;

		public GameObject fgim;

		public GameObject hp1;

		public GameObject type1;

		public GameObject pro;

		public GameObject fg2;

		public GameObject bossHead;

		public GameObject fg3;

		public GameObject bosshps;

		public Transform transHP;

		public Transform transHP1;

		public Transform bossHpBg;

		public Transform bossHP;

		public Text txtname;

		public Text txtLv;

		public Text bossName;

		public Text bossHPCount;

		public BaseRole role;

		private int bosscurhp = 0;

		public int curhp = 0;

		public int maxhp = 0;

		public int count = 0;

		public int max = 0;

		public GameObject golevel;

		private TargetMenu menu;

		public bool inFB = false;

		public static tragethead instan;

		private bool dead = false;

		private Tween tween = null;

		private Tween tween1 = null;

		private Tween tween2 = null;

		public bool full = false;

		private BaseRole trole;

		private bool isCan = true;

		private bool isboss = false;

		private bool b_useing = false;

		public override void init()
		{
			tragethead.instan = this;
			this.iconPhy = base.getGameObjectByPath("type/phy");
			this.iconMagic = base.getGameObjectByPath("type/magic");
			this.iconJy = base.getGameObjectByPath("type/jy");
			this.iconWar = base.getGameObjectByPath("pro/war");
			this.iconMage = base.getGameObjectByPath("pro/mage");
			this.iconAss = base.getGameObjectByPath("pro/ass");
			this.bossBg = base.getGameObjectByPath("BOSS1");
			this.bg = base.getGameObjectByPath("bg");
			this.otherHP = base.getGameObjectByPath("hp");
			this.bosshp1 = base.getGameObjectByPath("boss_hp");
			this.fgim = base.getGameObjectByPath("fgImage");
			this.hp1 = base.getGameObjectByPath("hp1");
			this.type1 = base.getGameObjectByPath("type");
			this.pro = base.getGameObjectByPath("pro");
			this.fg2 = base.getGameObjectByPath("fg2");
			this.fg3 = base.getGameObjectByPath("bossHead/fg3");
			this.bosshps = base.getGameObjectByPath("boss_hps");
			EventTriggerListener.Get(this.iconWar).onClick = new EventTriggerListener.VoidDelegate(this.onHeadClick);
			EventTriggerListener.Get(this.iconMage).onClick = new EventTriggerListener.VoidDelegate(this.onHeadClick);
			EventTriggerListener.Get(this.iconAss).onClick = new EventTriggerListener.VoidDelegate(this.onHeadClick);
			this.transHP = base.getTransformByPath("hp");
			this.txtLv = base.getComponentByPath<Text>("lv");
			this.txtname = base.getComponentByPath<Text>("txt");
			this.bossName = base.getComponentByPath<Text>("txt2");
			this.bossHPCount = base.getComponentByPath<Text>("txt3");
			this.transHP1 = base.getTransformByPath("hp1");
			this.bossHpBg = base.getTransformByPath("boss_hps/boss_hp0");
			base.transform.localScale = Vector3.zero;
			this.bossHP = base.getTransformByPath("boss_hp");
			this.bossHead = base.getGameObjectByPath("bossHead/1");
			this.golevel = base.getGameObjectByPath("level");
			this.menu = new TargetMenu(base.getTransformByPath("menu"));
		}

		public void Update()
		{
			bool flag = SelfRole._inst == null;
			if (!flag)
			{
				bool flag2 = this.isboss;
				if (flag2)
				{
					this.curhp = 0;
					this.maxhp = 0;
				}
				bool flag3 = this.trole != null && this.trole.isDead && this.transHP1.localScale.x != 0f;
				if (flag3)
				{
					bool flag4 = this.isboss;
					if (flag4)
					{
						bool isDead = this.role.isDead;
						if (isDead)
						{
							this.BossHP();
							this.DeadHp();
						}
						base.StartCoroutine(this.Wait1());
					}
					this.role = null;
				}
				else
				{
					this.trole = SelfRole._inst.m_LockRole;
					bool flag5 = this.trole != null && this.trole.isDead;
					if (flag5)
					{
						this.trole = null;
					}
				}
				bool flag6 = this.trole == null || this.trole.disposed || (this.trole.isDead && this.transHP1.localScale.x == 0f);
				if (flag6)
				{
					bool flag7 = this.role != null;
					if (flag7)
					{
						bool flag8 = this.isboss;
						if (flag8)
						{
							this.dead = this.role.isDead;
							bool flag9 = this.dead;
							if (flag9)
							{
								this.BossHP();
								this.DeadHp();
							}
							base.StartCoroutine(this.Wait1());
						}
						else
						{
							base.transform.localScale = new Vector3(0f, 1f, 0f);
						}
						this.role = null;
					}
					bool flag10 = this.b_useing;
					if (flag10)
					{
						InterfaceMgr.doCommandByLua("a3_litemap.setactive_btn", "ui/interfaces/floatui/a3_litemap", new object[]
						{
							true
						});
						this.b_useing = false;
					}
				}
				else
				{
					bool flag11 = this.role == null && this.trole != null && !this.trole.isDead;
					if (flag11)
					{
						base.transform.localScale = Vector3.one;
						this.refreshInfo();
					}
					else
					{
						bool flag12 = SelfRole._inst.m_LockRole != this.role && this.isCan;
						if (flag12)
						{
							this.refreshInfo();
						}
						else
						{
							bool flag13 = this.role == null && this.trole != null && this.trole.isDead;
							if (flag13)
							{
								this.trole = null;
								base.transform.localScale = new Vector3(0f, 1f, 0f);
							}
						}
					}
					bool flag14 = this.role != null && (this.curhp != this.role.curhp || this.maxhp != this.role.maxHp);
					if (flag14)
					{
						bool isDead2 = this.trole.isDead;
						if (isDead2)
						{
							bool flag15 = !this.isboss;
							if (flag15)
							{
								this.tween2 = this.transHP.DOScaleX(0f, 0.2f);
								base.StartCoroutine(this.Wait());
							}
						}
						float num = (float)this.role.curhp / (float)this.role.maxHp;
						Vector3 one = new Vector3(num, 1f, 1f);
						bool flag16 = this.curhp < this.role.curhp;
						if (flag16)
						{
							bool flag17 = !this.isboss;
							if (flag17)
							{
								this.transHP1.DOKill(false);
								this.transHP1.localScale = one;
								this.bossHP.gameObject.SetActive(false);
							}
							else
							{
								bool flag18 = this.isboss;
								if (flag18)
								{
									this.bossHP.gameObject.SetActive(true);
								}
								this.bossHP.DOKill(false);
								this.bossHpBg.DOKill(false);
								this.bossHP.localScale = new Vector3(1f, 1f, 1f);
							}
							this.BossHP();
						}
						else
						{
							this.tween1 = this.transHP1.DOScaleX(num, 0.2f);
							this.isCan = false;
						}
						this.tween.OnComplete(delegate
						{
							this.refTrole();
						});
						this.tween1.OnComplete(delegate
						{
							this.refTrole();
						});
						this.tween2.OnComplete(delegate
						{
							this.refTrole();
						});
						bool flag19 = num > 1f;
						if (flag19)
						{
							one = Vector3.one;
						}
						bool flag20 = !this.isboss;
						if (flag20)
						{
							this.curhp = this.role.curhp;
							this.maxhp = this.role.maxHp;
							this.transHP.localScale = one;
							this.tween2 = this.transHP.DOScaleX(one.x, 0.2f);
						}
					}
				}
			}
		}

		private void refTrole()
		{
			this.isCan = true;
		}

		private void BossHP()
		{
			bool flag = this.isboss;
			if (flag)
			{
				float num = (float)this.role.curhp / (float)this.role.maxHp;
				int @int = this.role.tempXMl.getInt("head");
				int int2 = this.role.tempXMl.getInt("hp_count");
				GameObject[] array = new GameObject[int2 + 1];
				int num2 = this.role.maxHp / int2;
				int num3 = (this.role.maxHp - this.role.curhp) / num2;
				int num4 = int2 - num3;
				int num5 = int2;
				bool flag2 = this.role.curhp != this.bosscurhp;
				if (flag2)
				{
					this.bossName.text = this.role.roleName;
					this.bossHPCount.text = "X" + num4;
					this.bossHead.GetComponent<Image>().overrideSprite = (Resources.Load("icon/bossHead/" + @int, typeof(Sprite)) as Sprite);
					base.transform.FindChild("boss_hps/boss_hp0").gameObject.SetActive(true);
					array[0] = base.transform.FindChild("boss_hps/boss_hp0").gameObject;
					bool flag3 = this.count != num5;
					if (flag3)
					{
						this.full = false;
					}
					bool flag4 = !this.full;
					if (flag4)
					{
						bool flag5 = int2 >= 10;
						if (flag5)
						{
							bool flag6 = this.count > num5;
							if (flag6)
							{
								for (int i = num5 + 1; i <= this.count; i++)
								{
									base.transform.FindChild("boss_hps/boss_hp" + i).gameObject.SetActive(false);
								}
							}
							else
							{
								bool flag7 = this.count == 0;
								if (flag7)
								{
									for (int j = 0; j < (int2 - 5) / 5; j++)
									{
										int num6 = int2 % 5;
										int num7 = j + 1;
										for (int k = 1; k <= 5; k++)
										{
											base.transform.FindChild("boss_hps/boss_hp" + k).gameObject.SetActive(true);
											array[k] = base.transform.FindChild("boss_hps/boss_hp" + k).gameObject;
											GameObject gameObject = base.transform.FindChild("boss_hps/boss_hp" + k).gameObject;
											array[k + 5 * num7] = UnityEngine.Object.Instantiate<GameObject>(gameObject);
											array[k + 5 * num7].transform.SetParent(base.transform.FindChild("boss_hps"));
											array[k + 5 * num7].transform.localPosition = array[k].transform.localPosition;
											array[k + 5 * num7].transform.localScale = Vector3.one;
											array[k + 5 * num7].name = "boss_hp" + (k + 5 * num7);
											bool flag8 = k + 5 * num7 == int2 - num6;
											if (flag8)
											{
												for (int l = 1; l <= num6; l++)
												{
													base.transform.FindChild("boss_hps/boss_hp" + l).gameObject.SetActive(true);
													array[l] = base.transform.FindChild("boss_hps/boss_hp" + l).gameObject;
													GameObject gameObject2 = base.transform.FindChild("boss_hps/boss_hp" + l).gameObject;
													array[k + 5 * num7 + l] = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
													array[k + 5 * num7 + l].transform.SetParent(base.transform.FindChild("boss_hps"));
													array[k + 5 * num7 + l].transform.localPosition = array[l].transform.localPosition;
													array[k + 5 * num7 + l].transform.localScale = Vector3.one;
													array[k + 5 * num7 + l].name = "boss_hp" + (k + 5 * num7 + l);
												}
											}
											bool flag9 = k + 5 * num7 == int2 || k + 5 * num7 + num6 == int2;
											if (flag9)
											{
												this.full = true;
											}
										}
									}
								}
								else
								{
									bool flag10 = this.max >= num5;
									bool flag11 = !flag10;
									if (flag11)
									{
										for (int m = this.count + 1; m <= num5; m++)
										{
											bool flag12 = m % 5 > 0;
											if (flag12)
											{
												base.transform.FindChild("boss_hps/boss_hp" + m % 5).gameObject.SetActive(true);
												array[m % 5] = base.transform.FindChild("boss_hps/boss_hp" + m % 5).gameObject;
												GameObject gameObject3 = base.transform.FindChild("boss_hps/boss_hp" + m % 5).gameObject;
												array[m] = UnityEngine.Object.Instantiate<GameObject>(gameObject3);
												array[m].transform.SetParent(base.transform.FindChild("boss_hps"));
												array[m].transform.localPosition = array[m % 5].transform.localPosition;
												array[m].name = "boss_hp" + m;
												array[m].transform.localScale = new Vector3(1f, 1f, 0f);
											}
											else
											{
												base.transform.FindChild("boss_hps/boss_hp" + 5).gameObject.SetActive(true);
												array[5] = base.transform.FindChild("boss_hps/boss_hp" + 5).gameObject;
												GameObject gameObject4 = base.transform.FindChild("boss_hps/boss_hp" + 5).gameObject;
												array[m] = UnityEngine.Object.Instantiate<GameObject>(gameObject4);
												array[m].transform.SetParent(base.transform.FindChild("boss_hps"));
												array[m].transform.localPosition = array[5].transform.localPosition;
												array[m].transform.localScale = new Vector3(1f, 1f, 0f);
												array[m].name = "boss_hp" + m;
											}
											bool flag13 = m == int2;
											if (flag13)
											{
												this.full = true;
											}
										}
									}
									this.max = ((this.max >= this.count) ? this.max : this.count);
								}
							}
							this.count = int2;
							this.max = ((this.max >= this.count) ? this.max : this.count);
						}
						else
						{
							bool flag14 = this.count > num5;
							if (flag14)
							{
								for (int n = num5 + 1; n <= this.count; n++)
								{
									base.transform.FindChild("boss_hps/boss_hp" + n).gameObject.SetActive(false);
								}
							}
							else
							{
								bool flag15 = this.count == 0;
								if (flag15)
								{
									bool flag16 = int2 <= 5;
									if (flag16)
									{
										for (int num8 = 1; num8 <= int2; num8++)
										{
											base.transform.FindChild("boss_hps/boss_hp" + num8).gameObject.SetActive(true);
											array[num8] = base.transform.FindChild("boss_hps/boss_hp" + num8).gameObject;
										}
									}
									else
									{
										int num9 = int2 % 5;
										for (int num10 = 1; num10 <= num9; num10++)
										{
											base.transform.FindChild("boss_hps/boss_hp" + num10).gameObject.SetActive(true);
											array[num10] = base.transform.FindChild("boss_hps/boss_hp" + num10).gameObject;
											GameObject gameObject5 = base.transform.FindChild("boss_hps/boss_hp" + num10).gameObject;
											array[num10 + 5] = UnityEngine.Object.Instantiate<GameObject>(gameObject5);
											array[num10 + 5].transform.SetParent(base.transform.FindChild("boss_hps"));
											array[num10 + 5].transform.localPosition = array[num10].transform.localPosition;
											array[num10 + 5].transform.localScale = new Vector3(1f, 1f, 0f);
											array[num10 + 5].name = "boss_hp" + (num10 + 5);
											bool flag17 = num10 + 5 == int2;
											if (flag17)
											{
												this.full = true;
											}
										}
									}
								}
								else
								{
									bool flag18 = this.max >= num5;
									bool flag19 = !flag18;
									if (flag19)
									{
										for (int num11 = this.count + 1; num11 <= num5; num11++)
										{
											bool flag20 = num11 % 5 > 0;
											if (flag20)
											{
												base.transform.FindChild("boss_hps/boss_hp" + num11 % 5).gameObject.SetActive(true);
												array[num11 % 5] = base.transform.FindChild("boss_hps/boss_hp" + num11 % 5).gameObject;
												GameObject gameObject6 = base.transform.FindChild("boss_hps/boss_hp" + num11 % 5).gameObject;
												array[num11] = UnityEngine.Object.Instantiate<GameObject>(gameObject6);
												array[num11].transform.SetParent(base.transform.FindChild("boss_hps"));
												array[num11].transform.localPosition = array[num11 % 5].transform.localPosition;
												array[num11].transform.localScale = new Vector3(1f, 1f, 0f);
												array[num11].name = "boss_hp" + num11;
											}
											else
											{
												base.transform.FindChild("boss_hps/boss_hp" + 5).gameObject.SetActive(true);
												array[5] = base.transform.FindChild("boss_hps/boss_hp" + 5).gameObject;
												GameObject gameObject7 = base.transform.FindChild("boss_hps/boss_hp" + 5).gameObject;
												array[num11] = UnityEngine.Object.Instantiate<GameObject>(gameObject7);
												array[num11].transform.SetParent(base.transform.FindChild("boss_hps"));
												array[num11].transform.localPosition = array[5].transform.localPosition;
												array[num11].transform.localScale = new Vector3(1f, 1f, 0f);
												array[num11].name = "boss_hp" + num11;
											}
											bool flag21 = num11 == int2;
											if (flag21)
											{
												this.full = true;
											}
										}
									}
								}
							}
							this.count = int2;
							this.max = ((this.max >= this.count) ? this.max : this.count);
						}
					}
					for (int num12 = 1; num12 <= int2; num12++)
					{
						base.transform.FindChild("boss_hps/boss_hp" + num12).gameObject.SetActive(true);
						array[num12] = base.transform.FindChild("boss_hps/boss_hp" + num12).gameObject;
					}
					for (int num13 = int2 + 1; num13 <= 200; num13++)
					{
						bool flag22 = base.transform.FindChild("boss_hps/boss_hp" + num13) != null;
						if (flag22)
						{
							base.transform.FindChild("boss_hps/boss_hp" + num13).gameObject.SetActive(false);
						}
					}
					for (int num14 = 1; num14 < num4; num14++)
					{
						array[num14].transform.localScale = new Vector3(1f, 1f, 0f);
					}
					bool flag23 = num4 <= 1;
					if (flag23)
					{
						array[0].transform.SetAsFirstSibling();
					}
					else
					{
						array[0].transform.SetSiblingIndex(array[num4].transform.GetSiblingIndex() - 1);
					}
					this.tween1 = array[num4].transform.DOScaleX((float)int2 * num - (float)num4 + 1f, 0.2f);
					for (int num15 = num4 + 1; num15 <= int2; num15++)
					{
						this.tween2 = array[num15].transform.DOScaleX(0f, 0.2f);
					}
				}
				this.tween = this.bossHpBg.DOScaleX((float)int2 * num - (float)num4 + 1f, 0.3f);
				this.bosscurhp = this.role.curhp;
			}
		}

		private void DeadHp()
		{
			int @int = this.role.tempXMl.getInt("hp_count");
			GameObject[] array = new GameObject[@int + 1];
			int num = this.role.maxHp / @int;
			int num2 = (this.role.maxHp - this.role.curhp) / num;
			int num3 = @int - num2;
			for (int i = @int; i >= 1; i--)
			{
				array[i] = base.transform.FindChild("boss_hps/boss_hp" + i).gameObject;
				this.tween2 = array[i].transform.DOScaleX(0f, 0.2f);
			}
			this.tween1 = this.bossHpBg.DOScaleX(0f, 0.003f);
			this.dead = false;
		}

		private IEnumerator Wait()
		{
			yield return new WaitForSeconds(0.05f);
			this.transform.localScale = Vector3.zero;
			yield break;
		}

		private IEnumerator Wait1()
		{
			yield return new WaitForSeconds(0.1f);
			this.transform.localScale = new Vector3(0f, 1f, 0f);
			yield break;
		}

		public void onHeadClick(GameObject go)
		{
			bool flag = !this.inFB;
			if (flag)
			{
				bool visiable = this.menu.visiable;
				if (visiable)
				{
					this.menu.hide();
				}
				else
				{
					this.menu.show();
				}
			}
			else
			{
				flytxt.instance.fly("副本中不支持此操作！", 0, default(Color), null);
			}
		}

		public void refreshInfo()
		{
			this.role = SelfRole._inst.m_LockRole;
			this.menu.hide();
			this.iconMagic.SetActive(false);
			this.iconJy.SetActive(false);
			this.iconPhy.SetActive(false);
			this.iconWar.SetActive(false);
			this.iconAss.SetActive(false);
			this.iconMage.SetActive(false);
			this.bossBg.SetActive(false);
			this.bg.SetActive(true);
			this.fgim.SetActive(true);
			this.hp1.SetActive(true);
			this.golevel.SetActive(true);
			this.otherHP.SetActive(true);
			this.type1.SetActive(true);
			this.pro.SetActive(true);
			this.fg2.SetActive(true);
			this.fg3.SetActive(false);
			this.bossHead.SetActive(false);
			this.bosshps.SetActive(false);
			this.bosshp1.SetActive(false);
			bool flag = this.b_useing;
			if (flag)
			{
				InterfaceMgr.doCommandByLua("a3_litemap.setactive_btn", "ui/interfaces/floatui/a3_litemap", new object[]
				{
					true
				});
				this.b_useing = false;
			}
			bool flag2 = this.role is CollectRole;
			if (flag2)
			{
				this.golevel.SetActive(false);
				this.txtLv.text = "";
				this.bossName.text = "";
				this.bossHPCount.text = "";
				this.txtname.text = this.role.roleName;
				this.isboss = false;
			}
			else
			{
				bool flag3 = this.role is MonsterRole;
				if (flag3)
				{
					bool flag4 = this.role.tempXMl != null && this.role.tempXMl.getInt("boss_hp") == 1;
					if (flag4)
					{
						this.txtname.gameObject.SetActive(false);
						this.bossName.gameObject.SetActive(true);
						this.bossHPCount.gameObject.SetActive(true);
						this.isboss = true;
					}
					else
					{
						this.bossName.gameObject.SetActive(false);
						this.bossHPCount.gameObject.SetActive(false);
						this.txtname.gameObject.SetActive(true);
						this.isboss = false;
					}
					bool flag5 = this.role.tempXMl != null && this.role.tempXMl.getInt("atktp") == 1;
					if (flag5)
					{
						this.iconPhy.SetActive(true);
					}
					else
					{
						bool flag6 = this.role.tempXMl != null;
						if (flag6)
						{
							this.iconMagic.SetActive(true);
						}
					}
					bool flag7 = this.isboss;
					if (flag7)
					{
						this.bossBg.SetActive(true);
						this.bg.SetActive(false);
						this.fgim.SetActive(false);
						this.otherHP.SetActive(false);
						this.hp1.SetActive(false);
						this.bosshp1.SetActive(true);
						this.type1.SetActive(false);
						this.pro.SetActive(false);
						this.fg2.SetActive(false);
						this.fg3.SetActive(true);
						this.bossHead.SetActive(true);
						this.bosshps.SetActive(true);
						bool flag8 = !this.b_useing;
						if (flag8)
						{
							InterfaceMgr.doCommandByLua("a3_litemap.setactive_btn", "ui/interfaces/floatui/a3_litemap", new object[]
							{
								false
							});
							this.b_useing = true;
						}
					}
					bool flag9 = this.role is P2Warrior && !this.iconWar.activeSelf;
					if (flag9)
					{
						this.iconWar.SetActive(true);
					}
					else
					{
						bool flag10 = this.role is P3Mage && !this.iconMage.activeSelf;
						if (flag10)
						{
							this.iconMage.SetActive(true);
						}
						else
						{
							bool flag11 = this.role is P5Assassin && !this.iconAss.activeSelf;
							if (flag11)
							{
								this.iconAss.SetActive(true);
							}
						}
					}
					bool flag12 = MapModel.getInstance().curLevelId > 0u;
					if (flag12)
					{
						this.txtLv.text = MapModel.getInstance().show_instanceLvl(MapModel.getInstance().curLevelId).ToString();
						this.txtname.text = this.role.roleName;
					}
					else
					{
						this.txtLv.text = this.role.tempXMl.getString("lv");
						this.txtname.text = this.role.roleName;
					}
				}
				else
				{
					this.bossName.gameObject.SetActive(false);
					this.bossHPCount.gameObject.SetActive(false);
					this.txtname.gameObject.SetActive(true);
					this.bosshp1.SetActive(false);
					bool flag13 = this.role is ProfessionRole;
					if (flag13)
					{
						this.txtLv.text = (this.role as ProfessionRole).lvl.ToString();
						bool flag14 = this.role.roleName != this.txtname.text;
						if (flag14)
						{
							this.txtname.text = this.role.roleName;
						}
						this.bossBg.SetActive(false);
						this.bg.SetActive(true);
						this.fgim.SetActive(true);
						this.otherHP.SetActive(true);
						this.hp1.SetActive(true);
						this.bosshp1.SetActive(false);
						this.type1.SetActive(true);
						this.pro.SetActive(true);
						this.fg2.SetActive(true);
						this.fg3.SetActive(false);
						this.bossHead.SetActive(false);
						this.bosshps.SetActive(false);
						this.bossHP.gameObject.SetActive(false);
						this.isboss = false;
						bool flag15 = this.b_useing;
						if (flag15)
						{
							InterfaceMgr.doCommandByLua("a3_litemap.setactive_btn", "ui/interfaces/floatui/a3_litemap", new object[]
							{
								true
							});
							this.b_useing = false;
						}
					}
					bool flag16 = this.role is P2Warrior && !this.iconWar.activeSelf;
					if (flag16)
					{
						this.iconWar.SetActive(true);
					}
					else
					{
						bool flag17 = this.role is P3Mage && !this.iconMage.activeSelf;
						if (flag17)
						{
							this.iconMage.SetActive(true);
						}
						else
						{
							bool flag18 = this.role is P5Assassin && !this.iconAss.activeSelf;
							if (flag18)
							{
								this.iconAss.SetActive(true);
							}
						}
					}
				}
			}
		}
	}
}
