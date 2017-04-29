using System;
using UnityEngine;

namespace Cross
{
	public class AudioImpl : MediaObjectImpl, IAudio, IMediaObject
	{
		protected AssetAudioImpl m_asset;

		protected AudioSource m_audiosource;

		protected float m_volume = 1f;

		protected bool m_loop = false;

		public IAssetAudio asset
		{
			get
			{
				return this.m_asset;
			}
			set
			{
				this.m_asset = (value as AssetAudioImpl);
				bool isReady = this.m_asset.isReady;
				if (isReady)
				{
					this.m_audiosource.clip = this.m_asset.audioClip;
				}
				else
				{
					this.m_asset.addCallbacks(delegate(IAsset ast)
					{
						this.m_audiosource.clip = this.m_asset.audioClip;
						this.play();
					}, null, null);
				}
			}
		}

		public float volume
		{
			get
			{
				return this.m_volume;
			}
			set
			{
				this.m_volume = value;
			}
		}

		public bool loop
		{
			get
			{
				return this.m_loop;
			}
			set
			{
				this.m_loop = value;
			}
		}

		public AudioImpl()
		{
			this.m_obj.name = "Audio";
			this.m_audiosource = this.m_obj.AddComponent<AudioSource>();
		}

		public void play()
		{
			this.m_audiosource.loop = this.m_loop;
			this.m_audiosource.volume = this.m_volume;
			bool flag = !this.m_audiosource.isPlaying;
			if (flag)
			{
				this.m_audiosource.Play();
			}
		}

		public void stop()
		{
			bool isPlaying = this.m_audiosource.isPlaying;
			if (isPlaying)
			{
				this.m_audiosource.Stop();
			}
		}

		public void pause()
		{
			bool isPlaying = this.m_audiosource.isPlaying;
			if (isPlaying)
			{
				this.m_audiosource.Pause();
			}
		}
	}
}
