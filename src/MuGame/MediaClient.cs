using GameFramework;
using System;

namespace MuGame
{
	public class MediaClient : MediaClientBase
	{
		public static MediaClient instance;

		private TickItem tick;

		private bool m_fadeinMusic = false;

		public MediaClient()
		{
			MediaClient.instance = this;
			this.tick = new TickItem(new Action<float>(this.onTick));
		}

		private void onTick(float s)
		{
			bool flag = !this.m_fadeinMusic;
			if (!flag)
			{
				bool flag2 = this._curMusic == null;
				if (flag2)
				{
					this.m_fadeinMusic = false;
					TickMgr.instance.removeTick(this.tick);
				}
				else
				{
					bool flag3 = this._curMusic.volume >= 1f;
					if (flag3)
					{
						this.m_fadeinMusic = false;
						base.setMusicVolume(1f);
						TickMgr.instance.removeTick(this.tick);
					}
					else
					{
						base.setMusicVolume(this._musicVolume + 0.005f);
					}
				}
			}
		}

		public static MediaClient getInstance()
		{
			bool flag = MediaClient.instance == null;
			if (flag)
			{
				MediaClient.instance = new MediaClient();
			}
			return MediaClient.instance;
		}

		public void fadeInMusic()
		{
			base.setMusicVolume(0.1f);
			this.m_fadeinMusic = true;
			TickMgr.instance.addTick(this.tick);
		}

		public void Play(string url, bool loop, Action finFun = null)
		{
			base.PlaySoundUrl(url, loop, finFun);
		}
	}
}
