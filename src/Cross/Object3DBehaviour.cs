using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class Object3DBehaviour : MonoBehaviour
	{
		protected GraphObject3DImpl m_obj = null;

		protected Dictionary<float, Action> m_event = new Dictionary<float, Action>();

		protected List<float> m_deleevent = new List<float>();

		protected bool m_addscript = false;

		protected string m_objname;

		protected string m_scriptName;

		protected GameObject m_addScobj;

		protected Dictionary<string, GameObject> m_addscri = new Dictionary<string, GameObject>();

		protected Dictionary<string, bool> m_script = new Dictionary<string, bool>();

		private float m_time;

		protected bool m_enable = true;

		public GraphObject3DImpl obj
		{
			get
			{
				return this.m_obj;
			}
			set
			{
				this.m_obj = value;
			}
		}

		private void Start()
		{
		}

		private void OnBecameInvisible()
		{
			this.m_enable = false;
		}

		private void OnBecameVisible()
		{
			this.m_enable = true;
		}

		private void Update()
		{
			bool flag = !this.m_enable;
			if (!flag)
			{
				this.m_time = Time.time;
				foreach (string current in this.m_addscri.Keys)
				{
					bool flag2 = this.m_script[current];
					if (flag2)
					{
						bool flag3 = this.m_addscri[current].transform.Find(this.m_objname);
						if (flag3)
						{
							this.m_addscri[current].transform.Find(this.m_objname).gameObject.AddComponent(this.m_scriptName);
							this.m_addscript = false;
							this.m_script[current] = this.m_addscript;
						}
					}
				}
				foreach (float current2 in this.m_event.Keys)
				{
					bool flag4 = this.m_time > current2;
					if (flag4)
					{
						this.m_event[current2]();
						this.m_deleevent.Add(current2);
					}
				}
				for (int i = 0; i < this.m_deleevent.Count; i++)
				{
					this.m_event.Remove(this.m_deleevent[i]);
				}
				this.m_deleevent.Clear();
				this.m_obj.onProcess(Time.deltaTime);
			}
		}

		private void OnRenderObject()
		{
			bool flag = this.m_obj == null;
			if (!flag)
			{
				this.m_obj.onPreRender();
			}
		}

		public void addScript(GameObject obj, string objName, string ScriptName)
		{
			this.m_addscript = true;
			this.m_addScobj = obj;
			this.m_objname = objName;
			this.m_scriptName = ScriptName;
			this.m_addscri[objName] = obj;
			this.m_script[objName] = this.m_addscript;
		}

		public void addevent(float tim, Action act)
		{
			tim += Time.time;
			this.m_event[tim] = act;
		}
	}
}
