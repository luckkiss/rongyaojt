using System;
using UnityEngine;
using UnityEngine.UI;

public class LMovieClip : MonoBehaviour
{
	public float fps = 15f;

	public bool isPlayOnwake = false;

	public string path;

	protected Image _comImage;

	protected float _time;

	protected int _frameLenght;

	protected bool _isPlaying = false;

	protected int _currentIndex = 0;

	protected Sprite[] _spriteArr;

	private void Start()
	{
		this._comImage = base.gameObject.GetComponent<Image>();
		bool flag = this.isPlayOnwake;
		if (flag)
		{
			this.loadTexture();
			this.play();
		}
	}

	public void loadTexture()
	{
		this._spriteArr = Resources.LoadAll<Sprite>(this.path);
		this._frameLenght = this._spriteArr.Length;
	}

	private void OnGUI()
	{
		bool isPlaying = this._isPlaying;
		if (isPlaying)
		{
			this.drawAnimation();
		}
	}

	protected void drawAnimation()
	{
		this._comImage.sprite = this._spriteArr[this._currentIndex];
		bool flag = this._currentIndex < this._frameLenght;
		if (flag)
		{
			this._time += Time.deltaTime;
			bool flag2 = this._time >= 1f / this.fps;
			if (flag2)
			{
				this._currentIndex++;
				this._time = 0f;
				bool flag3 = this._currentIndex == this._frameLenght;
				if (flag3)
				{
					this._currentIndex = 0;
				}
			}
		}
	}

	public void play()
	{
		this._isPlaying = true;
	}

	public void stop()
	{
		this._isPlaying = false;
		this._currentIndex = 0;
		this._comImage.sprite = this._spriteArr[0];
	}

	public void pause()
	{
		this._isPlaying = false;
	}
}
