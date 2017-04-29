using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	public class ActiveChatItem : Skin
	{
		private Animator ani;

		private GRAvatar _avatar;

		public ActiveChatItem(Transform trans) : base(trans)
		{
			this.initUI();
		}

		private void initUI()
		{
			this.ani = this.__mainTrans.GetComponent<Animator>();
		}

		public void clear()
		{
			this._avatar = null;
		}

		public bool update()
		{
			base.pos = this._avatar.getHeadPos();
			bool flag = this.ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
			bool result;
			if (flag)
			{
				this.visiable = false;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}
	}
}
