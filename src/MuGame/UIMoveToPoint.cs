using DG.Tweening;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class UIMoveToPoint : MonoBehaviour
	{
		public Transform targetUI;

		private Baselayer uiLayer;

		private GameObject clone;

		private Tweener tw;

		private Tweener ss;

		[SerializeField]
		public Ease ease;

		[SerializeField]
		public float dutime;

		private Action onend;

		public Vector3 endscale = Vector3.one;

		public static UIMoveToPoint Get(GameObject go)
		{
			bool flag = go == null;
			UIMoveToPoint result;
			if (flag)
			{
				Debug.LogError("EventTriggerListener_go_is_NULL");
				result = null;
			}
			else
			{
				UIMoveToPoint uIMoveToPoint = go.GetComponent<UIMoveToPoint>();
				bool flag2 = uIMoveToPoint == null;
				if (flag2)
				{
					uIMoveToPoint = go.AddComponent<UIMoveToPoint>();
				}
				result = uIMoveToPoint;
			}
			return result;
		}

		public void Move(Transform target, Action endevent = null)
		{
			this.targetUI = target;
			this.onend = endevent;
			this.Move();
		}

		[ContextMenu("Move")]
		public void Move()
		{
			bool flag = this.targetUI == null;
			if (!flag)
			{
				this.Kill();
				this.clone = UnityEngine.Object.Instantiate<GameObject>(base.gameObject);
				this.clone.transform.SetParent(this.uiLayer.transform);
				this.clone.transform.position = base.transform.position;
				this.clone.transform.localScale = Vector3.one;
				this.clone.transform.SetAsLastSibling();
				this.tw = this.clone.transform.DOMove(this.targetUI.position, this.dutime, false);
				bool flag2 = this.endscale != Vector3.one;
				if (flag2)
				{
					this.ss = this.clone.transform.DOScale(this.endscale, this.dutime);
					this.ss.SetEase(Ease.OutCubic);
				}
				this.tw.SetEase(this.ease);
				this.tw.OnKill(delegate
				{
					bool flag3 = this.clone != null;
					if (flag3)
					{
						UnityEngine.Object.Destroy(this.clone);
					}
				});
				this.tw.OnComplete(delegate
				{
					bool flag3 = this.onend != null;
					if (flag3)
					{
						this.onend();
					}
				});
			}
		}

		public void Kill()
		{
			bool flag = this.tw != null;
			if (flag)
			{
				this.tw.Kill(true);
			}
			bool flag2 = this.ss != null;
			if (flag2)
			{
				this.ss.Kill(false);
			}
		}

		private void Awake()
		{
			this.uiLayer = base.transform.GetComponentInParent<Baselayer>();
		}

		private void OnDisable()
		{
			this.Kill();
		}
	}
}
