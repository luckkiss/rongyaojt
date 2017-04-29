using Cross;
using System;

namespace GameFramework
{
	public class DispAttAni
	{
		private IUIBaseControl _baseControl = null;

		private AttAni _attAni = null;

		private bool _playing = false;

		private Action<DispAttAni> _endFun;

		public Variant userdata
		{
			get
			{
				return this._attAni.userdata;
			}
			set
			{
				this._attAni.userdata = value;
			}
		}

		public IUIBaseControl baseControl
		{
			get
			{
				return this._baseControl;
			}
		}

		public bool IsPlaying
		{
			get
			{
				return this._playing;
			}
		}

		public DispAttAni(IUIBaseControl baseCtrl)
		{
			this._baseControl = baseCtrl;
			this._attAni = new AttAni(new Variant
			{
				_val = this._baseControl
			});
		}

		public void SetFinishFun(Action<DispAttAni> fin)
		{
			this._endFun = fin;
		}

		public void Play()
		{
			bool flag = !this._playing;
			if (flag)
			{
				this._playing = true;
				AttAniManager.singleton.AddAttAni(this._attAni);
				this._attAni.SetFinFun(new Action<AttAni>(this.onAniStop));
			}
		}

		public void Stop()
		{
			bool playing = this._playing;
			if (playing)
			{
				this._playing = false;
				AttAniManager.singleton.RemoveAttAni(this._attAni);
			}
		}

		public void Release()
		{
			this.Stop();
			this._attAni.Release();
			this._baseControl = null;
		}

		public void AddMoveAni(Func<Variant, double, double> tweenFun, Vec2 b, Vec2 e, float tm)
		{
			bool flag = b.x != e.x;
			if (flag)
			{
				Variant aniAtt = GameTools.createGroup(new Variant[]
				{
					"begin",
					b.x,
					"change",
					e.x - b.x,
					"duration",
					tm
				});
				this._attAni.AddAni(tweenFun, "x", aniAtt, 1, 0f);
			}
			bool flag2 = b.y != e.y;
			if (flag2)
			{
				Variant aniAtt = GameTools.createGroup(new Variant[]
				{
					"begin",
					b.y,
					"change",
					e.y - b.y,
					"duration",
					tm
				});
				this._attAni.AddAni(tweenFun, "y", aniAtt, 1, 0f);
			}
		}

		public void AddSizeAni(Func<Variant, double, double> tweenFun, float b, float e, float tm, int cnt = 1)
		{
			bool flag = b != e;
			if (flag)
			{
				Variant aniAtt = GameTools.createGroup(new Variant[]
				{
					"begin",
					b,
					"change",
					e - b,
					"duration",
					tm
				});
				this._attAni.AddAni(tweenFun, "scaleX", aniAtt, cnt, 0f);
				aniAtt = GameTools.createGroup(new Variant[]
				{
					"begin",
					b,
					"change",
					e - b,
					"duration",
					tm
				});
				this._attAni.AddAni(tweenFun, "scaleY", aniAtt, cnt, 0f);
			}
		}

		public void AddAttAni(Func<Variant, double, double> tweenFun, string attnm, float b, float e, float tm, int cnt = 1)
		{
			bool flag = b != e;
			if (flag)
			{
				Variant aniAtt = GameTools.createGroup(new Variant[]
				{
					"begin",
					b,
					"change",
					e - b,
					"duration",
					tm
				});
				this._attAni.AddAni(tweenFun, attnm, aniAtt, cnt, 0f);
			}
		}

		public void AdjustAniAtt(string attnm, Variant att)
		{
			this._attAni.AdjustAniAtt(attnm, att);
		}

		public void RemoveAttAni(string attnm)
		{
			this._attAni.RemoveAniAtt(attnm);
		}

		private void onAniStop(AttAni ani)
		{
			this.Stop();
			bool flag = this._endFun != null;
			if (flag)
			{
				this._endFun(this);
			}
		}
	}
}
