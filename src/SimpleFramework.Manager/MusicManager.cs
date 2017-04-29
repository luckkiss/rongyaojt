using System;
using System.Collections;
using UnityEngine;

namespace SimpleFramework.Manager
{
	public class MusicManager : View
	{
		private AudioSource audio;

		private Hashtable sounds = new Hashtable();

		private void Awake()
		{
			this.audio = base.GetComponent<AudioSource>();
		}

		private void Add(string key, AudioClip value)
		{
			if (this.sounds[key] != null || value == null)
			{
				return;
			}
			this.sounds.Add(key, value);
		}

		private AudioClip Get(string key)
		{
			if (this.sounds[key] == null)
			{
				return null;
			}
			return this.sounds[key] as AudioClip;
		}

		public AudioClip LoadAudioClip(string path)
		{
			AudioClip audioClip = this.Get(path);
			if (audioClip == null)
			{
				audioClip = (AudioClip)Resources.Load(path, typeof(AudioClip));
				this.Add(path, audioClip);
			}
			return audioClip;
		}

		public bool CanPlayBackSound()
		{
			string key = AppConst.AppPrefix + "BackSound";
			int @int = PlayerPrefs.GetInt(key, 1);
			return @int == 1;
		}

		public void PlayBacksound(string name, bool canPlay)
		{
			if (this.audio.clip != null && name.IndexOf(this.audio.clip.name) > -1)
			{
				if (!canPlay)
				{
					this.audio.Stop();
					this.audio.clip = null;
					Util.ClearMemory();
				}
				return;
			}
			if (canPlay)
			{
				this.audio.loop = true;
				this.audio.clip = this.LoadAudioClip(name);
				this.audio.Play();
			}
			else
			{
				this.audio.Stop();
				this.audio.clip = null;
				Util.ClearMemory();
			}
		}

		public bool CanPlaySoundEffect()
		{
			string key = AppConst.AppPrefix + "SoundEffect";
			int @int = PlayerPrefs.GetInt(key, 1);
			return @int == 1;
		}

		public void Play(AudioClip clip, Vector3 position)
		{
			if (!this.CanPlaySoundEffect())
			{
				return;
			}
			AudioSource.PlayClipAtPoint(clip, position);
		}
	}
}
