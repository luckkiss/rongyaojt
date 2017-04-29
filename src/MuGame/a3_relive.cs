using Cross;
using GameFramework;
using MuGame.Qsmy.model;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_relive : Window
	{
		public static int backtown_end_tm = 0;

		private int origin_tm = 3;

		private float timer = 0f;

		private Button btn_gld;

		private Button btn_stone;

		private Button btn_backleft;

		private Button btn_backmid;

		private Text info;

		private GameObject recharge;

		private BaseRole one;

		public static a3_relive instans;

		public GameObject FX;

		public override void init()
		{
			this.btn_gld = base.getComponentByPath<Button>("btn_gld");
			BaseButton baseButton = new BaseButton(this.btn_gld.transform, 1, 1);
			baseButton.onClick = new Action<GameObject>(this.OnGoldRespawn);
			this.btn_backleft = base.getComponentByPath<Button>("btn_backleft");
			BaseButton baseButton2 = new BaseButton(this.btn_backleft.transform, 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.OnBackRespawn);
			this.btn_stone = base.getComponentByPath<Button>("btn_stone");
			BaseButton baseButton3 = new BaseButton(this.btn_stone.transform, 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.OnStoneRespawn);
			this.btn_backmid = base.getComponentByPath<Button>("btn_backmid");
			BaseButton baseButton4 = new BaseButton(this.btn_backmid.transform, 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.OnBackRespawn);
			this.info = base.getComponentByPath<Text>("dialog");
			this.recharge = base.transform.FindChild("recharge").gameObject;
			this.FX = base.transform.FindChild("FX_").gameObject;
			new BaseButton(this.recharge.transform.FindChild("no"), 1, 1).onClick = delegate(GameObject go)
			{
				this.recharge.SetActive(false);
			};
			new BaseButton(this.recharge.transform.FindChild("yes"), 1, 1).onClick = delegate(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
				this.recharge.SetActive(false);
			};
		}

		public override void onShowed()
		{
			this.closeWindow();
			a3_relive.instans = this;
			this.recharge.SetActive(false);
			InterfaceMgr.getInstance().floatUI.localScale = Vector3.zero;
			bool flag = GameObject.Find("GAME_CAMERA/myCamera");
			if (flag)
			{
				GameObject gameObject = GameObject.Find("GAME_CAMERA/myCamera");
				bool flag2 = !gameObject.GetComponent<DeathShader>();
				if (flag2)
				{
					gameObject.AddComponent<DeathShader>();
				}
				else
				{
					gameObject.GetComponent<DeathShader>().enabled = true;
				}
			}
			this.timer = 0f;
			this.origin_tm = 3;
			this.btn_gld.gameObject.SetActive(false);
			this.btn_stone.gameObject.SetActive(false);
			this.btn_backleft.gameObject.SetActive(false);
			this.btn_backmid.gameObject.SetActive(false);
			this.btn_stone.interactable = false;
			this.btn_gld.interactable = false;
			this.btn_backmid.interactable = false;
			this.btn_backleft.interactable = false;
			this.RefreshBackTownBtn();
			this.RefreshOriginBtn();
			bool flag3 = !this.CanReviveOrigin();
			if (flag3)
			{
				this.btn_backmid.gameObject.SetActive(true);
			}
			else
			{
				bool flag4 = this.HasRespawnStone();
				if (flag4)
				{
					this.btn_stone.gameObject.SetActive(true);
					this.btn_backleft.gameObject.SetActive(true);
				}
				else
				{
					this.btn_gld.gameObject.SetActive(true);
					this.btn_backleft.gameObject.SetActive(true);
				}
			}
			bool flag5 = this.uiData != null;
			if (flag5)
			{
				this.one = (BaseRole)this.uiData[0];
			}
			this.refInfo();
			InterfaceMgr.getInstance().closeAllWin(InterfaceMgr.A3_RELIVE);
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().floatUI.localScale = Vector3.one;
			bool flag = GameObject.Find("GAME_CAMERA/myCamera");
			if (flag)
			{
				GameObject gameObject = GameObject.Find("GAME_CAMERA/myCamera");
				bool flag2 = gameObject.GetComponent<DeathShader>();
				if (flag2)
				{
					gameObject.GetComponent<DeathShader>().enabled = false;
				}
			}
			a3_relive.instans = null;
		}

		public void Update()
		{
			this.timer += Time.deltaTime;
			bool flag = this.timer > 1f;
			if (flag)
			{
				this.timer -= 1f;
				this.origin_tm--;
				a3_relive.backtown_end_tm--;
				this.RefreshBackTownBtn();
				this.RefreshOriginBtn();
				this.AutoRespawn();
			}
		}

		private void closeWindow()
		{
			int i = 0;
			while (i < base.transform.parent.childCount)
			{
				bool activeSelf = base.transform.parent.GetChild(i).gameObject.activeSelf;
				if (activeSelf)
				{
					bool flag = base.transform.parent.GetChild(i).gameObject == base.gameObject;
					if (!flag)
					{
						base.transform.parent.GetChild(i).gameObject.SetActive(false);
						GRMap.GAME_CAMERA.SetActive(true);
					}
				}
				IL_79:
				i++;
				continue;
				goto IL_79;
			}
		}

		private void AutoRespawn()
		{
			bool flag = !SelfRole.fsm.Autofighting;
			if (!flag)
			{
				bool flag2 = this.origin_tm == 0;
				if (flag2)
				{
					AutoPlayModel instance = ModelBase<AutoPlayModel>.getInstance();
					bool flag3 = instance.StoneRespawn == 0 || !this.CanReviveOrigin() || (instance.RespawnLimit > 0 && StateInit.Instance.RespawnTimes <= 0);
					if (flag3)
					{
						SelfRole.fsm.Stop();
					}
					else
					{
						bool flag4 = this.HasRespawnStone();
						if (flag4)
						{
							this.OnStoneRespawn(null);
							StateInit.Instance.RespawnTimes--;
						}
						else
						{
							bool flag5 = instance.GoldRespawn > 0 && ModelBase<PlayerModel>.getInstance().gold >= 20u;
							if (flag5)
							{
								this.OnGoldRespawn(null);
								StateInit.Instance.RespawnTimes--;
							}
							else
							{
								SelfRole.fsm.Stop();
							}
						}
					}
				}
			}
		}

		private void RefreshOriginBtn()
		{
			bool flag = this.origin_tm == 0;
			if (flag)
			{
				this.btn_stone.interactable = true;
				this.btn_gld.interactable = true;
			}
			bool flag2 = this.origin_tm < 0;
			if (!flag2)
			{
				this.btn_stone.transform.FindChild("Text").GetComponent<Text>().text = this.GetOriText();
				this.btn_gld.transform.FindChild("Text").GetComponent<Text>().text = this.GetOriText();
			}
		}

		private void RefreshBackTownBtn()
		{
			bool flag = a3_relive.backtown_end_tm <= 0;
			if (flag)
			{
				this.btn_backmid.interactable = true;
				this.btn_backleft.interactable = true;
				a3_relive.backtown_end_tm = 0;
			}
			this.btn_backleft.transform.FindChild("Text").GetComponent<Text>().text = this.GetBackText();
			this.btn_backmid.transform.FindChild("Text").GetComponent<Text>().text = this.GetBackText();
		}

		private void OnClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_RELIVE);
		}

		private void OnGoldRespawn(GameObject go)
		{
			bool flag = ModelBase<PlayerModel>.getInstance().gold < 20u;
			if (flag)
			{
				flytxt.instance.fly("钻石不足无法复活", 0, default(Color), null);
				this.recharge.SetActive(true);
			}
			else
			{
				BaseProxy<MapProxy>.getInstance().sendRespawn(true);
			}
		}

		private void OnBackRespawn(GameObject go)
		{
			BaseProxy<MapProxy>.getInstance().sendRespawn(false);
		}

		private void OnStoneRespawn(GameObject go)
		{
			BaseProxy<MapProxy>.getInstance().sendRespawn(true);
		}

		private string GetOriText()
		{
			string text = "原地复活";
			bool flag = this.origin_tm == 0;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				result = string.Concat(new object[]
				{
					text,
					"(",
					this.origin_tm,
					")"
				});
			}
			return result;
		}

		private string GetBackText()
		{
			string text = "回城复活";
			bool flag = a3_relive.backtown_end_tm == 0;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				result = string.Concat(new object[]
				{
					text,
					"(",
					a3_relive.backtown_end_tm,
					")"
				});
			}
			return result;
		}

		private void refInfo()
		{
			string text = "";
			bool flag = this.one != null;
			if (flag)
			{
				text = "你被" + this.one.roleName + "杀死了";
			}
			this.info.text = text;
		}

		private bool CanReviveOrigin()
		{
			uint nCurMapID = (uint)GRMap.instance.m_nCurMapID;
			Variant singleMapConf = SvrMapConfig.instance.getSingleMapConf(nCurMapID);
			bool flag = !singleMapConf.ContainsKey("revive");
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int num = singleMapConf["revive"];
				result = (num == 1);
			}
			return result;
		}

		private bool HasRespawnStone()
		{
			int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(1508u);
			return itemNumByTpid >= 1;
		}
	}
}
