using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class npctalk : StoryUI
	{
		protected Text txtName;

		protected Text txtDesc;

		protected GameObject fg;

		public List<Transform> lUiPos = new List<Transform>();

		private Transform transTalk;

		private GameObject teach;

		private Transform bg;

		private new Transform name;

		private Animator aniTalk;

		public static npctalk instance;

		public override void init()
		{
			this.transTalk = base.getTransformByPath("talk");
			this.aniTalk = this.transTalk.GetComponent<Animator>();
			this.teach = base.getGameObjectByPath("talk/teach");
			this.txtName = base.getComponentByPath<Text>("talk/txtname");
			this.txtDesc = base.getComponentByPath<Text>("talk/txtdesc");
			this.fg = base.getGameObjectByPath("talk/fg");
			this.bg = base.getTransformByPath("talk/bg");
			this.name = base.getTransformByPath("talk/namebg");
			this.fg.GetComponent<RectTransform>().sizeDelta = new Vector2(Baselayer.uiWidth * 1.5f, Baselayer.uiHeight * 1.5f);
			this.lUiPos.Add(base.getTransformByPath("con1"));
			this.lUiPos.Add(base.getTransformByPath("con0"));
			this.OnInit();
			base.gameObject.SetActive(false);
			base.CancelInvoke("showui_phone");
			base.Invoke("showui_phone", 0.1f);
		}

		public override void onShowed()
		{
			npctalk.instance = this;
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_STORY);
			EventTriggerListener.Get(this.fg).onClick = new EventTriggerListener.VoidDelegate(this.onClick);
			this.showDesc();
			this.transTalk.gameObject.SetActive(true);
		}

		public void MinOrMax(bool b = true)
		{
			if (b)
			{
				base.transform.localScale = Vector3.one;
			}
			else
			{
				base.transform.localScale = Vector3.zero;
			}
		}

		public void showui_phone()
		{
			base.transform.localScale = Vector3.one;
			base.gameObject.SetActive(true);
			this.refreshPos();
		}

		public override void onClosed()
		{
			npctalk.instance = null;
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			this.OnClosedProcess();
		}

		public virtual void OnClosedProcess()
		{
		}

		public virtual void OnInit()
		{
		}

		public virtual void onClick(GameObject go)
		{
			dialog.next();
		}

		public virtual void refreshPos()
		{
			bool flag = dialog.curType == "0";
			if (flag)
			{
				this.txtName.text = ModelBase<PlayerModel>.getInstance().name;
				dialog.instance.GetPlayerCamRdy();
				dialog.instance.showRole(true);
				this.transTalk.position = this.lUiPos[0].position;
				this.bg.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
				this.name.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
				this.txtName.alignment = TextAnchor.LowerLeft;
				Transform transform = base.transform.FindChild("talk/txtdesc");
				transform.position = new Vector3(this.lUiPos[0].position.x, transform.position.y, 0f);
				Animator expr_102 = this.aniTalk;
				if (expr_102 != null)
				{
					expr_102.SetTrigger("left");
				}
			}
			else
			{
				bool flag2 = dialog.curType == "1";
				if (flag2)
				{
					this.txtName.text = dialog.m_npc.name;
					dialog.instance.GetNPCCamRdy();
					dialog.instance.showRole(false);
					this.transTalk.position = this.lUiPos[1].position;
					this.bg.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
					this.name.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
					this.txtName.alignment = TextAnchor.LowerRight;
					Transform transform2 = base.transform.FindChild("talk/txtdesc");
					transform2.position = new Vector3(this.lUiPos[1].position.x, transform2.position.y, 0f);
					Animator expr_21A = this.aniTalk;
					if (expr_21A != null)
					{
						expr_21A.SetTrigger("right");
					}
				}
			}
		}

		public void showDesc()
		{
			this.refreshPos();
			this.refreshView();
		}

		public virtual void refreshView()
		{
			this.txtDesc.text = dialog.curDesc;
		}
	}
}
