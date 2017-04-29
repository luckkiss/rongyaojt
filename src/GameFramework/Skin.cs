using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
	public class Skin
	{
		public object data;

		public Transform __mainTrans;

		public RectTransform recTransform;

		protected bool __visiable = true;

		public Vector3 pos
		{
			get
			{
				return this.recTransform.position;
			}
			set
			{
				this.recTransform.position = value;
			}
		}

		public GameObject gameObject
		{
			get
			{
				return this.__mainTrans.gameObject;
			}
		}

		public Transform transform
		{
			get
			{
				return this.__mainTrans.transform;
			}
		}

		public virtual bool visiable
		{
			get
			{
				return this.__visiable;
			}
			set
			{
				bool flag = this.__mainTrans.gameObject.active == value;
				if (!flag)
				{
					this.__visiable = value;
					this.__mainTrans.gameObject.SetActive(value);
				}
			}
		}

		public Skin(Transform trans)
		{
			this.__mainTrans = trans;
			this.recTransform = trans.GetComponent<RectTransform>();
		}

		public Transform getTransformByPath(string path)
		{
			string[] array = path.Split(new char[]
			{
				'.'
			});
			Transform transform = this.__mainTrans;
			for (int i = 0; i < array.Length; i++)
			{
				transform = transform.FindChild(array[i]);
			}
			return transform;
		}

		public GameObject getGameObjectByPath(string path)
		{
			return this.getTransformByPath(path).gameObject;
		}

		public Button getButtonByPath(string path)
		{
			return this.getComponentByPath<Button>(path);
		}

		public void setPerent(Transform p)
		{
			this.__mainTrans.transform.SetParent(p, false);
		}

		public T getComponentByPath<T>(string path) where T : Component
		{
			Transform transform = this.__mainTrans;
			string[] array = path.Split(new char[]
			{
				'.'
			});
			for (int i = 0; i < array.Length; i++)
			{
				transform = transform.Find(array[i]);
			}
			return transform.GetComponent<T>();
		}

		public EventTriggerListener getEventTrigerByPath(string path = "")
		{
			bool flag = path == "";
			EventTriggerListener result;
			if (flag)
			{
				result = EventTriggerListener.Get(this.__mainTrans.gameObject);
			}
			else
			{
				result = EventTriggerListener.Get(this.getGameObjectByPath(path));
			}
			return result;
		}

		public void clearListenersPath(string path = "")
		{
			bool flag = path == "";
			GameObject go;
			if (flag)
			{
				go = this.__mainTrans.gameObject;
			}
			else
			{
				go = this.getGameObjectByPath(path);
			}
			EventTriggerListener.Get(go).clearAllListener();
		}

		public void destoryGo()
		{
			UnityEngine.Object.Destroy(this.__mainTrans.gameObject);
			this.__mainTrans = null;
			this.recTransform = null;
		}
	}
}
