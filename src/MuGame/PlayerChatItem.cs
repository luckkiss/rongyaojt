using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class PlayerChatItem : Skin
	{
		public INameObj _avatar;

		public Text txtChat;

		private Image bg;

		private Image bgImage;

		private bool chatshowed = false;

		private Image titleIcon;

		private float expandWidth;

		private float expandHeight;

		private TickItem showtime;

		private float times = 0f;

		private int i;

		private bool isself = false;

		public PlayerChatItem(Transform trans) : base(trans)
		{
			this.initUI();
		}

		public void clear()
		{
			this._avatar = null;
		}

		private void initUI()
		{
			this.bg = base.getComponentByPath<Image>("bg");
			this.bgImage = base.getComponentByPath<Image>("bgImage");
			this.expandWidth = this.bgImage.rectTransform.sizeDelta.x;
			this.expandHeight = this.bgImage.rectTransform.sizeDelta.y;
			this.txtChat = base.getComponentByPath<Text>("bgImage.uchat");
			this.titleIcon = base.getComponentByPath<Image>("title");
			this.bg.gameObject.SetActive(false);
			this.titleIcon.gameObject.SetActive(false);
			this.bgImage.gameObject.SetActive(false);
		}

		public void refresh(INameObj avatar, string msg)
		{
			this._avatar = avatar;
			bool flag = avatar is ProfessionRole;
			if (!flag)
			{
				this.txtChat = null;
			}
			bool flag2 = this.txtChat;
			if (flag2)
			{
				this.txtChat.text = msg;
				this.txtChat.gameObject.SetActive(true);
				this.bgImage.gameObject.SetActive(true);
				this.bgImage.rectTransform.sizeDelta = new Vector2(this.txtChat.preferredWidth + this.expandWidth, this.txtChat.preferredHeight + this.expandHeight);
			}
		}

		private void onUpdates(float s)
		{
			this.times += s;
			bool flag = this.times >= 1f;
			if (flag)
			{
				this.i--;
				bool flag2 = this.i == 0;
				if (flag2)
				{
					this.i = 0;
					this.bgImage.gameObject.SetActive(false);
					this.txtChat.gameObject.SetActive(false);
					TickMgr.instance.removeTick(this.showtime);
					this.showtime = null;
				}
				this.times = 0f;
			}
		}

		public void refresShowChat(int time, bool ismyself = false)
		{
			bool flag = time <= 0;
			if (flag)
			{
				this.bgImage.gameObject.SetActive(false);
			}
			else
			{
				this.bgImage.gameObject.SetActive(true);
				bool flag2 = this.bgImage != null;
				if (flag2)
				{
					this.showtime = new TickItem(new Action<float>(this.onUpdates));
					TickMgr.instance.addTick(this.showtime);
				}
				this.i = time;
				if (ismyself)
				{
					this.isself = true;
				}
				else
				{
					this.isself = false;
				}
			}
		}

		public void update()
		{
			base.pos = this._avatar.getHeadPos();
		}
	}
}
