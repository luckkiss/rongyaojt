using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_mail : Window
	{
		public static int expshowid;

		private readonly List<A3_MailSimple> sortedList = new List<A3_MailSimple>();

		private readonly Dictionary<uint, GameObject> title_gos = new Dictionary<uint, GameObject>();

		private int read_id = -1;

		private Text mailtitle;

		private Text mailcontent;

		private Text fajianren2;

		private Text time2;

		private Text hint;

		private Text tx_infos;

		private Button del1;

		private Button get;

		private Button del2;

		private GridLayoutGroup itmGrid;

		private Text cnt;

		private GridLayoutGroup coinGrid;

		public Vector3 containPos = default(Vector3);

		public override void init()
		{
			new BaseButton(base.getTransformByPath("closeBtn"), 1, 1).onClick = new Action<GameObject>(this.OnClose);
			this.cnt = base.getComponentByPath<Text>("left/cnt");
			this.mailtitle = base.getComponentByPath<Text>("right1/mailtitle");
			this.mailcontent = base.getComponentByPath<Text>("right1/mailcontent");
			this.fajianren2 = base.getComponentByPath<Text>("right1/fajianren2");
			this.time2 = base.getComponentByPath<Text>("right1/time2");
			this.hint = base.getComponentByPath<Text>("right1/hint");
			this.del1 = base.getComponentByPath<Button>("right2/del1");
			this.get = base.getComponentByPath<Button>("right2/get");
			this.del2 = base.getComponentByPath<Button>("right2/del2");
			this.itmGrid = base.getComponentByPath<GridLayoutGroup>("right2/scroll_view/contain");
			this.coinGrid = base.getComponentByPath<GridLayoutGroup>("right2/grid");
			this.containPos = base.getTransformByPath("right2/scroll_view/contain").localPosition;
			this.tx_infos = base.getComponentByPath<Text>("right2/tx_infos");
			this.get.onClick.AddListener(new UnityAction(this.OnGetBtnClick));
			this.del1.onClick.AddListener(new UnityAction(this.OnRemoveClick));
			this.del2.onClick.AddListener(new UnityAction(this.OnRemoveClick));
			base.getComponentByPath<Button>("left/allget").onClick.AddListener(new UnityAction(this.OnAllGetClick));
			base.getComponentByPath<Button>("left/alldel").onClick.AddListener(new UnityAction(this.OnDeleteAllClick));
		}

		public override void onShowed()
		{
			BaseProxy<A3_MailProxy>.getInstance().addEventListener(A3_MailProxy.MAIL_NEW_MAIL, new Action<GameEvent>(this.OnGetNewMail));
			BaseProxy<A3_MailProxy>.getInstance().addEventListener(A3_MailProxy.MAIL_NEW_MAIL_CONTENT, new Action<GameEvent>(this.OnGetMailContent));
			BaseProxy<A3_MailProxy>.getInstance().addEventListener(A3_MailProxy.MAIL_GET_ATTACHMENT, new Action<GameEvent>(this.OnGetAttachment));
			BaseProxy<A3_MailProxy>.getInstance().addEventListener(A3_MailProxy.MAIL_REMOVE_ONE, new Action<GameEvent>(this.OnRemoveOne));
			BaseProxy<A3_MailProxy>.getInstance().addEventListener(A3_MailProxy.MAIL_GET_ALL, new Action<GameEvent>(this.OnGetAll));
			BaseProxy<A3_MailProxy>.getInstance().addEventListener(A3_MailProxy.MAIL_DELETE_ALL, new Action<GameEvent>(this.OnDeleteAll));
			this.RefreshMailList();
			bool flag = this.sortedList != null && this.sortedList.Count > 0;
			if (flag)
			{
				this.OnMailTitleClick(this.title_gos[this.sortedList[0].id]);
			}
			else
			{
				this.RefreshMailContent(this.read_id);
			}
			bool flag2 = this.uiData != null && this.uiData.Count > 0;
			if (flag2)
			{
				this.OnMailTitleClick(this.title_gos[(uint)a3_mail.expshowid]);
			}
			else
			{
				this.RefreshMailContent(this.read_id);
			}
			GRMap.GAME_CAMERA.SetActive(false);
		}

		public override void onClosed()
		{
			BaseProxy<A3_MailProxy>.getInstance().removeEventListener(A3_MailProxy.MAIL_NEW_MAIL, new Action<GameEvent>(this.OnGetNewMail));
			BaseProxy<A3_MailProxy>.getInstance().removeEventListener(A3_MailProxy.MAIL_NEW_MAIL_CONTENT, new Action<GameEvent>(this.OnGetMailContent));
			BaseProxy<A3_MailProxy>.getInstance().removeEventListener(A3_MailProxy.MAIL_GET_ATTACHMENT, new Action<GameEvent>(this.OnGetAttachment));
			BaseProxy<A3_MailProxy>.getInstance().removeEventListener(A3_MailProxy.MAIL_REMOVE_ONE, new Action<GameEvent>(this.OnRemoveOne));
			BaseProxy<A3_MailProxy>.getInstance().removeEventListener(A3_MailProxy.MAIL_GET_ALL, new Action<GameEvent>(this.OnGetAll));
			BaseProxy<A3_MailProxy>.getInstance().removeEventListener(A3_MailProxy.MAIL_DELETE_ALL, new Action<GameEvent>(this.OnDeleteAll));
			GRMap.GAME_CAMERA.SetActive(true);
		}

		private void OnClose(GameObject go)
		{
			a3_expbar.instance.HideMailHint();
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_MAIL);
			Dictionary<uint, GameObject>.Enumerator enumerator = this.title_gos.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<uint, GameObject> current = enumerator.Current;
				UnityEngine.Object.Destroy(current.Value);
			}
		}

		private void SortMailList()
		{
			this.sortedList.Clear();
			A3_MailModel instance = ModelBase<A3_MailModel>.getInstance();
			foreach (KeyValuePair<uint, A3_MailSimple> current in instance.mail_simple)
			{
				this.sortedList.Add(current.Value);
			}
			int count = this.sortedList.Count;
			for (int i = 0; i < count; i++)
			{
				for (int j = i + 1; j < count; j++)
				{
					bool flag = this.sortedList[i].tm < this.sortedList[j].tm;
					if (flag)
					{
						A3_MailSimple value = this.sortedList[i];
						this.sortedList[i] = this.sortedList[j];
						this.sortedList[j] = value;
					}
				}
			}
		}

		private void RefreshMailList()
		{
			this.SortMailList();
			GameObject original = Resources.Load<GameObject>("prefab/a3_mail_title");
			GameObject gameObject = base.transform.FindChild("left/scroll_view/contain").gameObject;
			gameObject.transform.DetachChildren();
			for (int i = 0; i < this.sortedList.Count; i++)
			{
				uint id = this.sortedList[i].id;
				GameObject gameObject2 = null;
				this.title_gos.TryGetValue(id, out gameObject2);
				bool flag = gameObject2 == null;
				if (flag)
				{
					gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original);
					this.title_gos[id] = gameObject2;
					BaseButton baseButton = new BaseButton(gameObject2.transform, 1, 1);
					baseButton.onClick = new Action<GameObject>(this.OnMailTitleClick);
				}
				gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject2.name = id.ToString();
				gameObject2.transform.SetParent(gameObject.transform, false);
				this.RefreshMailTitle(id);
			}
			this.RefreshMailCnt();
		}

		private void RefreshMailCnt()
		{
			this.cnt.text = ContMgr.getCont("mail_cnt", new List<string>
			{
				ModelBase<A3_MailModel>.getInstance().mail_simple.Count.ToString()
			});
		}

		private void RefreshMailTitle(uint id)
		{
			GameObject gameObject = null;
			this.title_gos.TryGetValue(id, out gameObject);
			bool flag = gameObject == null;
			if (!flag)
			{
				Image component = gameObject.transform.FindChild("icon").GetComponent<Image>();
				Image component2 = gameObject.transform.FindChild("icon").GetComponent<Image>();
				Text component3 = gameObject.transform.FindChild("title").GetComponent<Text>();
				Text component4 = gameObject.transform.FindChild("from").GetComponent<Text>();
				A3_MailSimple a3_MailSimple = ModelBase<A3_MailModel>.getInstance().mail_simple[id];
				bool has_itm = a3_MailSimple.has_itm;
				if (has_itm)
				{
					bool got_itm = a3_MailSimple.got_itm;
					if (got_itm)
					{
						component.sprite = (Resources.Load("icon/itemborder/b039_03", typeof(Sprite)) as Sprite);
						component2.sprite = (Resources.Load("icon/mail/mail_sign2", typeof(Sprite)) as Sprite);
					}
					else
					{
						component.sprite = (Resources.Load("icon/itemborder/b039_04", typeof(Sprite)) as Sprite);
						component2.sprite = (Resources.Load("icon/mail/mail_sign1", typeof(Sprite)) as Sprite);
					}
				}
				else
				{
					component.sprite = (Resources.Load("icon/itemborder/b039_01", typeof(Sprite)) as Sprite);
				}
				bool flag2 = a3_MailSimple.flag;
				if (flag2)
				{
					component3.text = "<color=#808080>" + a3_MailSimple.title + "</color>";
					component4.text = "<color=#808080>" + a3_MailSimple.tp + "</color>";
				}
				else
				{
					component3.text = "<color=#FFFFFF>" + a3_MailSimple.title + "</color>";
					component4.text = "<color=#FFFFFF>" + a3_MailSimple.tp + "</color>";
				}
			}
		}

		private void RefreshMailContent(int id)
		{
			this.mailtitle.gameObject.SetActive(false);
			this.mailcontent.gameObject.SetActive(false);
			this.fajianren2.gameObject.SetActive(false);
			this.time2.gameObject.SetActive(false);
			this.hint.gameObject.SetActive(false);
			this.del1.gameObject.SetActive(false);
			this.get.gameObject.SetActive(false);
			this.del2.gameObject.SetActive(false);
			this.tx_infos.gameObject.SetActive(false);
			for (int i = 0; i < this.itmGrid.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.itmGrid.transform.GetChild(i).gameObject);
			}
			for (int j = 0; j < this.coinGrid.transform.childCount; j++)
			{
				UnityEngine.Object.Destroy(this.coinGrid.transform.GetChild(j).gameObject);
			}
			bool flag = ModelBase<A3_MailModel>.getInstance().mail_simple.Count == 0;
			if (flag)
			{
				this.hint.gameObject.SetActive(true);
				this.hint.text = ContMgr.getCont("mail_hint_1", null);
			}
			else
			{
				bool flag2 = id == -1 || !ModelBase<A3_MailModel>.getInstance().mail_details.ContainsKey((uint)id);
				if (flag2)
				{
					this.hint.gameObject.SetActive(true);
					this.hint.text = ContMgr.getCont("mail_hint_2", null);
				}
				else
				{
					A3_MailDetail a3_MailDetail = ModelBase<A3_MailModel>.getInstance().mail_details[(uint)id];
					this.mailtitle.gameObject.SetActive(true);
					this.mailtitle.text = a3_MailDetail.ms.title;
					this.fajianren2.gameObject.SetActive(true);
					this.fajianren2.text = a3_MailDetail.ms.tp;
					this.time2.gameObject.SetActive(true);
					string strTime = Globle.getStrTime((int)a3_MailDetail.ms.tm, false, true);
					this.time2.text = strTime;
					this.mailcontent.gameObject.SetActive(true);
					this.mailcontent.text = a3_MailDetail.msg;
					bool flag3 = !a3_MailDetail.ms.has_itm;
					if (flag3)
					{
						this.del2.gameObject.SetActive(true);
					}
					else
					{
						bool flag4 = a3_MailDetail.money > 0u;
						if (flag4)
						{
							this.CreateCoinIcon("coin1", a3_MailDetail.money);
						}
						bool flag5 = a3_MailDetail.yb > 0u;
						if (flag5)
						{
							this.CreateCoinIcon("coin2", a3_MailDetail.yb);
						}
						bool flag6 = a3_MailDetail.bndyb > 0u;
						if (flag6)
						{
							this.CreateCoinIcon("coin3", a3_MailDetail.bndyb);
						}
						for (int k = 0; k < a3_MailDetail.itms.Count; k++)
						{
							a3_BagItemData data = a3_MailDetail.itms[k];
							GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data, true, data.num, 1f, false);
							gameObject.transform.SetParent(this.itmGrid.transform, false);
							bool flag7 = data.num <= 1;
							if (flag7)
							{
								gameObject.transform.FindChild("num").gameObject.SetActive(false);
							}
							BaseButton baseButton = new BaseButton(gameObject.transform, 1, 1);
							baseButton.onClick = delegate(GameObject go)
							{
								this.onMailItemClick(data);
							};
						}
						bool got_itm = a3_MailDetail.ms.got_itm;
						if (got_itm)
						{
							this.del2.gameObject.SetActive(true);
							this.tx_infos.gameObject.SetActive(true);
							this.tx_infos.text = ContMgr.getCont("mail_hint_0", null);
							for (int l = 0; l < this.itmGrid.transform.childCount; l++)
							{
								UnityEngine.Object.Destroy(this.itmGrid.transform.GetChild(l).gameObject);
							}
							for (int m = 0; m < this.coinGrid.transform.childCount; m++)
							{
								UnityEngine.Object.Destroy(this.coinGrid.transform.GetChild(m).gameObject);
							}
						}
						else
						{
							this.del1.gameObject.SetActive(true);
							this.get.gameObject.SetActive(true);
						}
					}
				}
			}
		}

		private void onMailItemClick(a3_BagItemData itmdata)
		{
			bool isEquip = itmdata.isEquip;
			if (isEquip)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(itmdata);
				arrayList.Add(equip_tip_type.Comon_tip);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
			}
			else
			{
				bool isSummon = itmdata.isSummon;
				if (isSummon)
				{
					ArrayList arrayList2 = new ArrayList();
					arrayList2.Add(itmdata);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3TIPS_SUMMON, arrayList2, false);
				}
				else
				{
					ArrayList arrayList3 = new ArrayList();
					arrayList3.Add(itmdata);
					arrayList3.Add(equip_tip_type.Comon_tip);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMTIP, arrayList3, false);
				}
			}
		}

		private void OnMailTitleClick(GameObject evt_go)
		{
			bool flag = this.read_id != -1;
			GameObject gameObject;
			if (flag)
			{
				gameObject = null;
				this.title_gos.TryGetValue((uint)this.read_id, out gameObject);
				bool flag2 = gameObject != null;
				if (flag2)
				{
					gameObject.transform.FindChild("select").gameObject.SetActive(false);
				}
			}
			this.read_id = int.Parse(evt_go.name);
			gameObject = null;
			this.title_gos.TryGetValue((uint)this.read_id, out gameObject);
			bool flag3 = gameObject != null;
			if (flag3)
			{
				gameObject.transform.FindChild("select").gameObject.SetActive(true);
				base.getTransformByPath("right2/scroll_view/contain").localPosition = this.containPos;
			}
			bool flag4 = ModelBase<A3_MailModel>.getInstance().mail_details.ContainsKey((uint)this.read_id);
			if (flag4)
			{
				this.RefreshMailContent(this.read_id);
			}
			else
			{
				BaseProxy<A3_MailProxy>.getInstance().GetMailContent((uint)this.read_id);
			}
		}

		private void OnGetBtnClick()
		{
			BaseProxy<A3_MailProxy>.getInstance().GetMailAttachment((uint)this.read_id);
		}

		private void OnAllGetClick()
		{
			BaseProxy<A3_MailProxy>.getInstance().GetAllAttachment();
		}

		private void OnDeleteAllClick()
		{
			bool flag = ModelBase<A3_MailModel>.getInstance().HasItemInMails();
			if (flag)
			{
				MsgBoxMgr.getInstance().showConfirm(ContMgr.getCont("mail_hint_3", null), new UnityAction(this.ConfirmDeleteAll), null, 0);
			}
			else
			{
				this.ConfirmDeleteAll();
			}
		}

		private void ConfirmDeleteAll()
		{
			BaseProxy<A3_MailProxy>.getInstance().DeleteAll();
		}

		private void OnRemoveClick()
		{
			bool flag = ModelBase<A3_MailModel>.getInstance().HasItemInMail((uint)this.read_id);
			if (flag)
			{
				MsgBoxMgr.getInstance().showConfirm(ContMgr.getCont("mail_hint_4", null), new UnityAction(this.ConfirmDelete), null, 0);
			}
			else
			{
				this.ConfirmDelete();
			}
		}

		private void ConfirmDelete()
		{
			BaseProxy<A3_MailProxy>.getInstance().RemoveMail((uint)this.read_id);
		}

		private void OnGetNewMail(GameEvent e)
		{
			this.RefreshMailList();
		}

		private void OnGetMailContent(GameEvent e)
		{
			uint id = (uint)e.orgdata;
			this.RefreshMailContent((int)id);
			this.RefreshMailTitle(id);
		}

		private void OnGetAttachment(GameEvent e)
		{
			flytxt.instance.fly(ContMgr.getCont("mail_hint_5", null), 0, default(Color), null);
			uint id = (uint)e.orgdata;
			this.RefreshMailContent((int)id);
			this.RefreshMailTitle(id);
		}

		private void OnRemoveOne(GameEvent e)
		{
			uint num = (uint)e.orgdata;
			GameObject gameObject = null;
			this.title_gos.TryGetValue(num, out gameObject);
			bool flag = gameObject != null && (long)this.read_id == (long)((ulong)num);
			if (flag)
			{
				this.title_gos.Remove(num);
				UnityEngine.Object.Destroy(gameObject);
				this.RefreshMailContent(this.read_id);
			}
			this.RefreshMailCnt();
			uint key = 0u;
			bool flag2 = num == this.sortedList[this.sortedList.Count - 1].id;
			if (flag2)
			{
				this.sortedList.Remove(this.sortedList[this.sortedList.Count - 1]);
				bool flag3 = this.sortedList != null && this.sortedList.Count > 0;
				if (flag3)
				{
					key = this.sortedList[0].id;
				}
				else
				{
					this.RefreshMailContent(this.read_id);
				}
			}
			else
			{
				for (int i = 0; i < this.sortedList.Count; i++)
				{
					bool flag4 = num == this.sortedList[i].id;
					if (flag4)
					{
						key = this.sortedList[i + 1].id;
						this.sortedList.RemoveAt(i);
						break;
					}
				}
			}
			GameObject gameObject2 = null;
			this.title_gos.TryGetValue(key, out gameObject2);
			bool flag5 = gameObject2 != null;
			if (flag5)
			{
				this.OnMailTitleClick(gameObject2);
			}
			flytxt.instance.fly(ContMgr.getCont("mail_hint_6", null), 0, default(Color), null);
		}

		private void OnGetAll(GameEvent e)
		{
			this.RefreshMailList();
			this.RefreshMailContent(this.read_id);
		}

		private void OnDeleteAll(GameEvent e)
		{
			Variant data = e.data;
			Variant variant = data["ids"];
			bool flag = variant._arr.Count > 0;
			if (flag)
			{
				flytxt.instance.fly(ContMgr.getCont("mail_hint_9", null), 0, default(Color), null);
			}
			else
			{
				flytxt.instance.fly(ContMgr.getCont("mail_hint_8", null), 0, default(Color), null);
			}
			this.RefreshMailList();
			this.RefreshMailContent(this.read_id);
		}

		private void CreateCoinIcon(string coin_icon, uint num)
		{
			GameObject original = Resources.Load<GameObject>("prefab/a3_mail_coin");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject.transform.localScale = Vector3.one;
			Image component = gameObject.transform.FindChild("icon").GetComponent<Image>();
			component.sprite = (Resources.Load("icon/coin/" + coin_icon, typeof(Sprite)) as Sprite);
			Text component2 = gameObject.transform.FindChild("num").GetComponent<Text>();
			component2.text = num.ToString();
			gameObject.transform.SetParent(this.coinGrid.transform);
			gameObject.transform.localScale = Vector3.one;
		}
	}
}
