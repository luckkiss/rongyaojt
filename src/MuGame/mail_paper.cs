using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class mail_paper : Window
	{
		private Text _titel;

		private InputField msgInput;

		private Text textNum;

		private BaseButton cloBtn;

		private BaseButton sendBtn;

		private bool isSend = false;

		public override void init()
		{
			base.init();
			this.cloBtn = new BaseButton(base.transform.FindChild("closeBtn"), 1, 1);
			this.cloBtn.onClick = new Action<GameObject>(this.onClickClose);
			this.sendBtn = new BaseButton(base.transform.FindChild("sendBtn"), 1, 1);
			this.sendBtn.onClick = new Action<GameObject>(this.onClickSend);
			this._titel = base.transform.FindChild("titel/bg/text").GetComponent<Text>();
			this.textNum = base.transform.FindChild("msg/bg/num/Text").GetComponent<Text>();
			this.msgInput = base.transform.FindChild("msg/bg/InputField").GetComponent<InputField>();
		}

		public override void onShowed()
		{
			BaseProxy<E_mailProxy>.getInstance().addEventListener(E_mailProxy.lis_sendMail_res, new Action<GameEvent>(this.sendMailRes));
			base.onShowed();
			this.cloBtn.addEvent();
			this.sendBtn.addEvent();
			base.transform.SetAsLastSibling();
			int num = (int)this.uiData[0];
			if (num != 1)
			{
				if (num == 4)
				{
					this._titel.text = ContMgr.getCont("mail_send", null) + this.uiData[2].ToString();
				}
			}
			else
			{
				this._titel.text = ContMgr.getCont("mail_send_fam", null);
			}
		}

		public override void onClosed()
		{
			base.onClosed();
			this.cloBtn.removeAllListener();
			this.sendBtn.removeAllListener();
			BaseProxy<E_mailProxy>.getInstance().removeEventListener(E_mailProxy.lis_sendMail_res, new Action<GameEvent>(this.sendMailRes));
			this.msgInput.text = "";
		}

		private void onClickClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.MAILPAPER);
			this.isSend = false;
		}

		private void onClickSend(GameObject go)
		{
			bool flag = this.msgInput.text == "" || this.isSend;
			if (!flag)
			{
				int num = (int)this.uiData[0];
				if (num != 1)
				{
					if (num == 4)
					{
						BaseProxy<E_mailProxy>.getInstance().sendNewMail(4, this.msgInput.text, (uint)((int)this.uiData[1]));
					}
				}
				else
				{
					BaseProxy<E_mailProxy>.getInstance().sendNewMail(1, this.msgInput.text, 0u);
				}
				this.isSend = true;
			}
		}

		private void sendMailRes(GameEvent e)
		{
			bool flag = base.gameObject.activeSelf && e.data.ContainsKey("res");
			if (flag)
			{
				bool flag2 = e.data["res"] > 0;
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getCont("mail_send_suc", null), 1, default(Color), null);
					this.isSend = false;
					bool flag3 = (int)this.uiData[0] == 4;
					if (flag3)
					{
						mailData mailData = new mailData();
						mailData.frmcid = (int)this.uiData[1];
						mailData.msg = this.msgInput.text;
						mailData.frmname = this.uiData[2].ToString();
						mailData.time = BaseProxy<E_mailProxy>.getInstance().getTime(e.data["tm"]);
						mailData.seconds = e.data["tm"]._int32;
						mailData.cid = (int)ModelBase<PlayerModel>.getInstance().cid;
						bool flag4 = ModelBase<E_mailModel>.getInstance().personalMailDic.ContainsKey((int)this.uiData[1]);
						if (flag4)
						{
							ModelBase<E_mailModel>.getInstance().personalMailDic[(int)this.uiData[1]].Add(mailData);
						}
						else
						{
							ModelBase<E_mailModel>.getInstance().personalMailDic[mailData.frmcid] = new List<mailData>();
							ModelBase<E_mailModel>.getInstance().personalMailDic[mailData.frmcid].Add(mailData);
						}
						string str = string.Concat(new object[]
						{
							mailData.frmcid.ToString(),
							"#!#&",
							mailData.frmsex.ToString(),
							"#!#&",
							mailData.cid.ToString(),
							"#!#&",
							mailData.time,
							"#!#&",
							mailData.frmname,
							"#!#&",
							mailData.msg,
							"#!#&",
							mailData.seconds,
							"#!#&",
							mailData.clanc,
							"#)#&"
						});
						mailData.str = str;
						ModelBase<E_mailModel>.getInstance().perLocalStr.Add(mailData);
						ModelBase<E_mailModel>.getInstance().saveLocalData(ModelBase<E_mailModel>.getInstance().perLocalStr, 4);
					}
					InterfaceMgr.getInstance().close(InterfaceMgr.MAILPAPER);
				}
			}
			bool flag5 = base.gameObject.activeSelf && e.data.ContainsKey("tp");
			if (flag5)
			{
				flytxt.instance.fly(ContMgr.getCont("mail_send_suc", null), 1, default(Color), null);
				InterfaceMgr.getInstance().close(InterfaceMgr.MAILPAPER);
			}
		}
	}
}
