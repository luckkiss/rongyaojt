using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
	public abstract class MediaClientBase
	{
		protected IAudio _curMusic;

		protected IAudio _curVoice;

		public string _curMusicUrl = "";

		protected Dictionary<string, IAudio> _sounds;

		protected float _musicVolume = 1f;

		public float _soundVolume = 1f;

		protected bool _isPlaySound = true;

		protected bool _isPlayMusic = true;

		private bool _enableLoopPlayMusic = true;

		public bool _pause = false;

		public bool enableLoopPlayMusic
		{
			get
			{
				return this._enableLoopPlayMusic;
			}
			set
			{
				this._enableLoopPlayMusic = value;
			}
		}

		public bool isPlaySound
		{
			get
			{
				return this._isPlaySound;
			}
			set
			{
				bool flag = this._isPlaySound != value;
				this._isPlaySound = value;
				bool flag2 = flag;
				if (!flag2)
				{
					bool flag3 = !this._isPlaySound;
					if (flag3)
					{
						this.StopSoundUrls(null);
					}
				}
			}
		}

		public bool isPlayMusic
		{
			get
			{
				return this._isPlayMusic;
			}
			set
			{
				bool flag = this._isPlayMusic == value;
				this._isPlayMusic = value;
				bool flag2 = flag;
				if (!flag2)
				{
					bool flag3 = !this._isPlayMusic;
					if (flag3)
					{
						this.StopMusic();
					}
					else
					{
						this.PlayMusicUrl(this._curMusicUrl, null, true);
					}
				}
			}
		}

		public MediaClientBase()
		{
			this._sounds = new Dictionary<string, IAudio>();
		}

		public void pause(bool b)
		{
			this._pause = b;
		}

		public void StopMusic()
		{
			bool flag = this._curMusic != null;
			if (flag)
			{
				this._curMusic.stop();
			}
		}

		public virtual void clearMusic()
		{
			this._sounds.Clear();
			this._curMusic = null;
		}

		public virtual void PlayVoiceUrl(string url, Action<IAsset> handle)
		{
			bool flag = url == "";
			if (flag)
			{
				bool flag2 = this._curVoice != null;
				if (flag2)
				{
					this._curVoice.stop();
				}
			}
			else
			{
				bool flag3 = this._curVoice != null;
				if (flag3)
				{
					this._curVoice.stop();
				}
				bool flag4 = this._curVoice == null;
				if (flag4)
				{
					this._curVoice = os.media.createAudio();
				}
				Debug.Log("::PlayVoiceUrl::" + url);
				this._curVoice.asset = os.asset.getAsset<IAssetAudio>(url, handle, null, null);
				this._curVoice.volume = this._musicVolume;
				this._curVoice.loop = false;
				this._curVoice.play();
			}
		}

		public virtual void PlayMusicUrl(string url, Action finFun = null, bool force = false)
		{
			bool flag = url == "";
			if (flag)
			{
				bool flag2 = this._curMusic != null;
				if (flag2)
				{
					this._curMusic.stop();
				}
			}
			else
			{
				bool flag3 = this._curMusicUrl == url && !force;
				if (!flag3)
				{
					this._curMusicUrl = url;
					bool flag4 = !this._isPlayMusic;
					if (!flag4)
					{
						bool flag5 = this._curMusic != null;
						if (flag5)
						{
							this._curMusic.stop();
						}
						bool flag6 = this._curMusic == null;
						if (flag6)
						{
							this._curMusic = os.media.createAudio();
						}
						this._curMusic.asset = os.asset.getAsset<IAssetAudio>(url, new Action<IAsset>(this.onMusicLoaded), null, null);
						this._curMusic.volume = this._musicVolume;
						this._curMusic.loop = true;
						this._curMusic.play();
					}
				}
			}
		}

		protected virtual void onMusicLoaded(IAsset ast)
		{
		}

		public void PlaySoundUrl(string url, bool loop, Action finFun)
		{
			bool pause = this._pause;
			if (!pause)
			{
				bool flag = url == null || url == "";
				if (!flag)
				{
					bool flag2 = !this._isPlaySound;
					if (!flag2)
					{
						bool flag3 = this._sounds.ContainsKey(url);
						if (flag3)
						{
							IAudio audio = this._sounds[url];
							audio.stop();
							audio.play();
						}
						else
						{
							IAudio audio = os.media.createAudio();
							audio.asset = os.asset.getAsset<IAssetAudio>(url);
							this._sounds[url] = audio;
							audio.volume = this._soundVolume;
							audio.loop = loop;
							audio.play();
						}
					}
				}
			}
		}

		public void setSoundVolume(float v = 1f)
		{
			bool flag = v > 1f;
			if (flag)
			{
				v = 1f;
			}
			bool flag2 = v < 0f;
			if (flag2)
			{
				v = 0f;
			}
			this._soundVolume = v;
			foreach (string current in this._sounds.Keys)
			{
				IAudio audio = this._sounds[current];
				audio.volume = this._soundVolume;
			}
			bool flag3 = this._soundVolume <= 0f;
			if (flag3)
			{
				this._isPlaySound = false;
			}
			else
			{
				this._isPlaySound = true;
			}
		}

		public float getSoundVolume()
		{
			return this._soundVolume;
		}

		public float getMusicVolume()
		{
			return this._musicVolume;
		}

		public void setMusicVolume(float v = 1f)
		{
			bool flag = v > 1f;
			if (flag)
			{
				v = 1f;
			}
			bool flag2 = v < 0f;
			if (flag2)
			{
				v = 0f;
			}
			this._musicVolume = v;
			bool flag3 = this._curMusic != null;
			if (flag3)
			{
				this._curMusic.volume = this._musicVolume;
			}
			else
			{
				this._curMusic = os.media.createAudio();
				this._curMusic.asset = os.asset.getAsset<IAssetAudio>(this._curMusicUrl, new Action<IAsset>(this.onMusicLoaded), null, null);
			}
			bool flag4 = this._musicVolume <= 0f;
			if (flag4)
			{
				this._isPlayMusic = false;
				bool flag5 = this._curMusic != null;
				if (flag5)
				{
					this._curMusic.stop();
				}
			}
			else
			{
				this._isPlayMusic = true;
				bool flag6 = this._curMusic != null;
				if (flag6)
				{
					this._curMusic.play();
				}
			}
		}

		public void StopSoundUrls(Variant urlArr = null)
		{
			bool flag = urlArr == null;
			if (flag)
			{
				foreach (IAudio current in this._sounds.Values)
				{
					bool flag2 = current != null;
					if (flag2)
					{
						current.stop();
					}
				}
			}
			else
			{
				for (int i = 0; i < urlArr.Count; i++)
				{
					string key = urlArr[i];
					bool flag3 = !this._sounds.ContainsKey(key);
					if (!flag3)
					{
						IAudio audio = this._sounds[key];
						bool flag4 = audio != null;
						if (flag4)
						{
							audio.stop();
						}
					}
				}
			}
		}
	}
}
