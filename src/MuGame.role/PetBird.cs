using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using UnityEngine;

namespace MuGame.role
{
	public class PetBird : MonoBehaviour
	{
		private GameObject _path;

		private Transform _curPath;

		private Tween _curPathTween;

		private Vector3 _ownerLastPos;

		private float _thinkTimer;

		private float _showTimer;

		private bool _mustFollow;

		private bool _canShow;

		private Animator _anim;

		private readonly float _thinkInterval = 2f;

		public GameObject Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		public PetBird()
		{
			this.Path = null;
			this._thinkTimer = 0f;
			this._showTimer = 0f;
			this._curPath = null;
			this._curPathTween = null;
			this._ownerLastPos = new Vector3(0f, 0f, 0f);
			this._mustFollow = true;
			this._canShow = false;
		}

		private void Start()
		{
			base.transform.position = this.Path.transform.position;
			this._anim = base.GetComponent<Animator>();
		}

		private void Update()
		{
			this._thinkTimer += Time.deltaTime;
			bool flag = this._thinkTimer >= this._thinkInterval;
			if (flag)
			{
				this._thinkTimer = 0f;
				this.Think();
			}
			this.DoAction();
		}

		private void FlyPath()
		{
			bool flag = !this.Path;
			if (!flag)
			{
				Transform transform = this.Path.transform;
				int childCount = transform.childCount;
				bool flag2 = childCount <= 0;
				if (!flag2)
				{
					System.Random random = new System.Random();
					int index = random.Next(childCount);
					this._curPath = transform.GetChild(index);
					childCount = this._curPath.childCount;
					bool flag3 = childCount <= 0;
					if (!flag3)
					{
						Vector3[] array = new Vector3[childCount];
						for (int i = 0; i < childCount; i++)
						{
							Transform child = this._curPath.GetChild(i);
							array[i] = child.position;
						}
						this._curPathTween = base.transform.DOPath(array, 0.5f, PathType.CatmullRom, PathMode.Full3D, 5, null).OnWaypointChange(new TweenCallback<int>(this.OnWayPointChange)).OnStart(new TweenCallback(this.OnPathStart)).OnComplete(new TweenCallback(this.OnPathComplete)).OnPause(new TweenCallback(this.OnPathPause)).OnKill(new TweenCallback(this.OnPathKill)).SetLookAt(0.01f, null, null).SetEase(Ease.Linear).SetOptions(false, AxisConstraint.None, AxisConstraint.X | AxisConstraint.Z).SetSpeedBased<TweenerCore<Vector3, Path, PathOptions>>();
						this._anim.SetBool("stop", false);
					}
				}
			}
		}

		private void Think()
		{
			Vector3 position = this.Path.transform.position;
			bool flag = position.Equals(this._ownerLastPos);
			if (flag)
			{
				this._mustFollow = false;
				this._showTimer += this._thinkInterval;
				bool flag2 = this._showTimer >= 35f;
				if (flag2)
				{
					this._canShow = true;
					this._showTimer = 0f;
				}
			}
			else
			{
				this._mustFollow = true;
				this._canShow = false;
				this._showTimer = 0f;
				this._ownerLastPos.Set(position.x, position.y, position.z);
			}
		}

		private void DoAction()
		{
			bool mustFollow = this._mustFollow;
			if (mustFollow)
			{
				bool flag = this._curPathTween != null;
				if (flag)
				{
					this._curPathTween.Kill(false);
					this._curPathTween = null;
				}
				Vector3 b = this.Path.transform.position - base.transform.position;
				float magnitude = b.magnitude;
				this._anim.SetBool("stop", magnitude < 1f);
				this._anim.SetBool("fly", true);
				b.y = 0f;
				base.transform.forward = Vector3.Lerp(base.transform.forward, b, Time.deltaTime * 8f);
				base.transform.position = Vector3.Lerp(base.transform.position, this.Path.transform.position, Time.deltaTime * 1f);
			}
			else
			{
				bool canShow = this._canShow;
				if (canShow)
				{
					this.FlyPath();
					this._canShow = false;
				}
			}
		}

		private void OnWayPointChange(int wpIdx)
		{
			int num = wpIdx - 1;
			bool flag = num >= this._curPath.childCount || num < 0;
			if (!flag)
			{
				Transform child = this._curPath.GetChild(num);
				string name = child.name;
				bool flag2 = name == null || name.Length <= 0;
				if (!flag2)
				{
					string[] array = name.Split(new char[]
					{
						'_'
					});
					bool flag3 = array[0] == "stop";
					if (flag3)
					{
						this._anim.SetBool("stop", true);
						base.StartCoroutine(this.OnPathPauseCoroutine(float.Parse(array[1])));
					}
					else
					{
						bool flag4 = array[0] == "land";
						if (flag4)
						{
							this._anim.SetBool("fly", false);
							base.StartCoroutine(this.OnPathPauseCoroutine(float.Parse(array[1])));
						}
						else
						{
							bool flag5 = array[0] == "fly";
							if (flag5)
							{
								this._anim.SetBool("fly", true);
							}
						}
					}
				}
			}
		}

		private IEnumerator OnPathPauseCoroutine(float pauseTm)
		{
			this._curPathTween.Pause<Tween>();
			yield return new WaitForSeconds(pauseTm);
			this._anim.SetBool("stop", false);
			this._curPathTween.Play<Tween>();
			yield break;
		}

		private void OnPathStart()
		{
		}

		private void OnPathComplete()
		{
			this._anim.SetBool("stop", true);
			this._curPathTween = null;
			this._curPath = null;
		}

		private void OnPathPause()
		{
		}

		private void OnPathKill()
		{
			this._curPathTween = null;
			this._curPath = null;
		}
	}
}
