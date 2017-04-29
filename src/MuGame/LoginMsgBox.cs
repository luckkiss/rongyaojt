using DG.Tweening;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	public class LoginMsgBox : Skin
	{
		private Text txt;

		private Action _handle;

		public LoginMsgBox(Transform trans) : base(trans)
		{
			this.txt = base.getComponentByPath<Text>("Text");
			base.getComponentByPath<Button>("bt").onClick.AddListener(new UnityAction(this.onClick));
			this.visiable = false;
		}

		public void show(bool b, string str, Action handle = null)
		{
			this._handle = handle;
			if (b)
			{
				this.txt.text = str;
				this.visiable = true;
				this.__mainTrans.localScale = Vector3.one;
				this.__mainTrans.DOScale(1f, 0.3f).From<Tweener>();
			}
			else
			{
				this.txt.text = "";
				this._handle = null;
				this.visiable = false;
			}
		}

		public void onClick()
		{
			bool flag = this._handle != null;
			if (flag)
			{
				this._handle();
			}
			this.visiable = false;
		}
	}
}
