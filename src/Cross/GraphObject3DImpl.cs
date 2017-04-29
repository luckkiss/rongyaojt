using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class GraphObject3DImpl : GraphObjectImpl, IGraphObject3D, IGraphObject
	{
		protected string m_id = null;

		protected GameObject m_u3dObj = null;

		protected Container3DImpl m_parent = null;

		protected int m_layer = 0;

		protected Vector3 m_u3dPos = default(Vector3);

		protected Vector3 m_u3dRot = default(Vector3);

		protected Vector3 m_u3dScale = new Vector3(1f, 1f, 1f);

		protected BoundBox m_boxCollider = null;

		protected Dictionary<Define.EventType, Action<Event>> m_eventFunc = new Dictionary<Define.EventType, Action<Event>>();

		protected CharacterController m_character = null;

		protected bool m_characterColliderbool = false;

		protected Dictionary<string, object> m_helper = new Dictionary<string, object>();

		protected bool m_visible;

		protected bool m_realVisible;

		public string id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		public Dictionary<string, object> helper
		{
			get
			{
				return this.m_helper;
			}
		}

		public Mat44 matrix
		{
			get
			{
				Matrix4x4 localToWorldMatrix = this.m_u3dObj.transform.localToWorldMatrix;
				Mat44 mat = new Mat44();
				mat.val[0][0] = localToWorldMatrix.m00;
				mat.val[0][1] = localToWorldMatrix.m01;
				mat.val[0][2] = localToWorldMatrix.m02;
				mat.val[0][3] = localToWorldMatrix.m03;
				mat.val[1][0] = localToWorldMatrix.m00;
				mat.val[1][1] = localToWorldMatrix.m01;
				mat.val[1][2] = localToWorldMatrix.m02;
				mat.val[1][3] = localToWorldMatrix.m03;
				mat.val[2][0] = localToWorldMatrix.m00;
				mat.val[2][1] = localToWorldMatrix.m01;
				mat.val[2][2] = localToWorldMatrix.m02;
				mat.val[2][3] = localToWorldMatrix.m03;
				mat.val[3][0] = localToWorldMatrix.m00;
				mat.val[3][1] = localToWorldMatrix.m01;
				mat.val[3][2] = localToWorldMatrix.m02;
				mat.val[3][3] = localToWorldMatrix.m03;
				return mat;
			}
		}

		public BoundBox boxCollider
		{
			get
			{
				return this.m_boxCollider;
			}
			set
			{
				this.m_boxCollider = value;
				bool flag = this.m_u3dObj == null;
				if (!flag)
				{
					bool flag2 = this.m_boxCollider != null;
					if (flag2)
					{
						BoxCollider boxCollider = this.m_u3dObj.GetComponent<BoxCollider>();
						bool flag3 = boxCollider == null;
						if (flag3)
						{
							boxCollider = this.m_u3dObj.AddComponent<BoxCollider>();
						}
						Vec3 center = this.m_boxCollider.center;
						Vec3 extent = this.m_boxCollider.extent;
						boxCollider.center = new Vector3(center.x, center.y, center.z);
						boxCollider.size = new Vector3(extent.x, extent.y, extent.z);
					}
					else
					{
						BoxCollider component = this.m_u3dObj.GetComponent<BoxCollider>();
						bool flag4 = component != null;
						if (flag4)
						{
							UnityEngine.Object.Destroy(component);
						}
					}
				}
			}
		}

		public Vec3 pos
		{
			get
			{
				return new Vec3(this.m_u3dPos.x, this.m_u3dPos.y, this.m_u3dPos.z);
			}
			set
			{
				bool flag = value == null;
				if (!flag)
				{
					this.m_u3dPos.Set(value.x, value.y, value.z);
					bool flag2 = this.m_u3dObj == null;
					if (!flag2)
					{
						this.m_u3dObj.transform.localPosition = this.m_u3dPos;
					}
				}
			}
		}

		public Vec3 globalPos
		{
			get
			{
				return new Vec3(this.m_u3dObj.transform.position.x, this.m_u3dObj.transform.position.y, this.m_u3dObj.transform.position.z);
			}
		}

		public float x
		{
			get
			{
				return this.m_u3dPos.x;
			}
			set
			{
				bool flag = float.IsNaN(value);
				if (!flag)
				{
					this.m_u3dPos.x = value;
					bool flag2 = this.m_u3dObj == null;
					if (!flag2)
					{
						this.m_u3dObj.transform.localPosition = this.m_u3dPos;
					}
				}
			}
		}

		public float y
		{
			get
			{
				return this.m_u3dPos.y;
			}
			set
			{
				bool flag = float.IsNaN(value);
				if (!flag)
				{
					this.m_u3dPos.y = value;
					bool flag2 = this.m_u3dObj == null;
					if (!flag2)
					{
						this.m_u3dObj.transform.localPosition = this.m_u3dPos;
					}
				}
			}
		}

		public float z
		{
			get
			{
				return this.m_u3dPos.z;
			}
			set
			{
				bool flag = float.IsNaN(value);
				if (!flag)
				{
					this.m_u3dPos.z = value;
					bool flag2 = this.m_u3dObj == null;
					if (!flag2)
					{
						this.m_u3dObj.transform.localPosition = this.m_u3dPos;
					}
				}
			}
		}

		public Vec3 rot
		{
			get
			{
				return new Vec3(this.m_u3dRot.x, this.m_u3dRot.y, this.m_u3dRot.z);
			}
			set
			{
				this.m_u3dRot.Set(value.x, value.y, value.z);
				bool flag = this.m_u3dObj == null;
				if (!flag)
				{
					this.m_u3dObj.transform.localEulerAngles = this.m_u3dRot;
				}
			}
		}

		public float rotX
		{
			get
			{
				return this.m_u3dRot.x;
			}
			set
			{
				this.m_u3dRot.x = value;
				bool flag = this.m_u3dObj == null;
				if (!flag)
				{
					this.m_u3dObj.transform.localEulerAngles = this.m_u3dRot;
				}
			}
		}

		public float rotY
		{
			get
			{
				return this.m_u3dRot.y;
			}
			set
			{
				this.m_u3dRot.y = value;
				bool flag = this.m_u3dObj == null;
				if (!flag)
				{
					bool flag2 = float.IsNaN(this.m_u3dRot.y);
					if (!flag2)
					{
						this.m_u3dObj.transform.localEulerAngles = this.m_u3dRot;
					}
				}
			}
		}

		public float rotZ
		{
			get
			{
				return this.m_u3dRot.z;
			}
			set
			{
				this.m_u3dRot.z = value;
				bool flag = this.m_u3dObj == null;
				if (!flag)
				{
					this.m_u3dObj.transform.localEulerAngles = this.m_u3dRot;
				}
			}
		}

		public Vec3 scale
		{
			get
			{
				return new Vec3(this.m_u3dScale.x, this.m_u3dScale.y, this.m_u3dScale.z);
			}
			set
			{
				bool flag = value == null;
				if (!flag)
				{
					this.m_u3dScale.Set(value.x, value.y, value.z);
					bool flag2 = this.m_u3dObj == null;
					if (!flag2)
					{
						this.m_u3dObj.transform.localScale = this.m_u3dScale;
					}
				}
			}
		}

		public float scaleX
		{
			get
			{
				return this.m_u3dScale.x;
			}
			set
			{
				bool flag = float.IsNaN(value);
				if (!flag)
				{
					this.m_u3dScale.x = value;
					bool flag2 = this.m_u3dObj == null;
					if (!flag2)
					{
						this.m_u3dObj.transform.localScale = this.m_u3dScale;
					}
				}
			}
		}

		public string name
		{
			get
			{
				return this.m_u3dObj.name;
			}
			set
			{
				bool flag = this.m_u3dObj == null;
				if (!flag)
				{
					this.m_u3dObj.name = value;
				}
			}
		}

		public float scaleY
		{
			get
			{
				return this.m_u3dScale.y;
			}
			set
			{
				bool flag = float.IsNaN(value);
				if (!flag)
				{
					this.m_u3dScale.y = value;
					bool flag2 = this.m_u3dObj == null;
					if (!flag2)
					{
						this.m_u3dObj.transform.localScale = this.m_u3dScale;
					}
				}
			}
		}

		public float scaleZ
		{
			get
			{
				return this.m_u3dScale.z;
			}
			set
			{
				bool flag = float.IsNaN(value);
				if (!flag)
				{
					this.m_u3dScale.z = value;
					bool flag2 = this.m_u3dObj == null;
					if (!flag2)
					{
						this.m_u3dObj.transform.localScale = this.m_u3dScale;
					}
				}
			}
		}

		public Vec3 axisX
		{
			get
			{
				Vector3 right = this.m_u3dObj.transform.right;
				return new Vec3(right.x, right.y, right.z);
			}
		}

		public Vec3 axisY
		{
			get
			{
				Vector3 up = this.m_u3dObj.transform.up;
				return new Vec3(up.x, up.y, up.z);
			}
		}

		public Vec3 axisZ
		{
			get
			{
				Vector3 forward = this.m_u3dObj.transform.forward;
				return new Vec3(forward.x, forward.y, forward.z);
			}
		}

		public virtual bool visible
		{
			get
			{
				return this.m_visible;
			}
			set
			{
				bool flag = this.m_u3dObj == null;
				if (!flag)
				{
					this.m_visible = value;
					this.m_u3dObj.SetActive(value);
					this._updateRealVisible();
				}
			}
		}

		public IContainer3D parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = (value as Container3DImpl);
				bool flag = this.m_u3dObj == null;
				if (!flag)
				{
					bool flag2 = this.m_parent != null;
					if (flag2)
					{
						this.m_u3dObj.transform.parent = this.m_parent.m_u3dObj.transform;
					}
					else
					{
						this.m_u3dObj.transform.parent = null;
					}
					this.m_u3dObj.transform.localPosition = this.m_u3dPos;
					this.m_u3dObj.transform.localEulerAngles = this.m_u3dRot;
					this.m_u3dObj.transform.localScale = this.m_u3dScale;
				}
			}
		}

		public virtual int layer
		{
			get
			{
				return this.m_layer;
			}
			set
			{
				this.m_layer = value;
				this.setLayer(this.m_u3dObj, value);
			}
		}

		public bool characterColliderbool
		{
			get
			{
				return this.m_characterColliderbool;
			}
			set
			{
				this.m_characterColliderbool = value;
				bool flag = this.m_u3dObj == null;
				if (!flag)
				{
					bool characterColliderbool = this.m_characterColliderbool;
					if (characterColliderbool)
					{
						bool flag2 = this.m_character == null;
						if (flag2)
						{
							this.m_character = this.m_u3dObj.AddComponent<CharacterController>();
						}
					}
				}
			}
		}

		public float slopeLimit
		{
			get
			{
				return this.m_character.slopeLimit;
			}
			set
			{
				bool flag = float.IsNaN(value);
				if (!flag)
				{
					this.m_character.slopeLimit = value;
				}
			}
		}

		public float stepOffset
		{
			get
			{
				return this.m_character.stepOffset;
			}
			set
			{
				bool flag = float.IsNaN(value);
				if (!flag)
				{
					this.m_character.stepOffset = value;
				}
			}
		}

		public float radius
		{
			get
			{
				return this.m_character.radius;
			}
			set
			{
				bool flag = float.IsNaN(value);
				if (!flag)
				{
					this.m_character.radius = value;
				}
			}
		}

		public float height
		{
			get
			{
				return this.m_character.height;
			}
			set
			{
				bool flag = float.IsNaN(value);
				if (!flag)
				{
					this.m_character.height = value;
				}
			}
		}

		public Vec3 center
		{
			get
			{
				return new Vec3(this.m_character.center.x, this.m_character.center.y, this.m_character.center.z);
			}
			set
			{
				bool flag = value == null;
				if (!flag)
				{
					this.m_character.center = new Vector3(value.x, value.y, value.z);
				}
			}
		}

		public GameObject u3dObject
		{
			get
			{
				return this.m_u3dObj;
			}
		}

		public CharacterController u3dCharacter
		{
			get
			{
				return this.m_character;
			}
		}

		public GraphObject3DImpl()
		{
			this.m_u3dObj = new GameObject();
			Object3DBehaviour object3DBehaviour = this.m_u3dObj.AddComponent<Object3DBehaviour>();
			object3DBehaviour.obj = this;
			this.layer = 0;
			this.m_u3dPos = this.m_u3dObj.transform.localPosition;
			this.m_u3dRot = this.m_u3dObj.transform.localEulerAngles;
			this.m_u3dScale = this.m_u3dObj.transform.localScale;
		}

		~GraphObject3DImpl()
		{
			this.dispose();
		}

		public virtual void dispose()
		{
			bool flag = this.m_u3dObj != null;
			if (flag)
			{
				bool flag2 = this.parent != null;
				if (flag2)
				{
					this.parent.removeChild(this);
				}
				UnityEngine.Object.Destroy(this.m_u3dObj);
				this.m_u3dObj = null;
			}
		}

		public virtual void onPreRender()
		{
		}

		public virtual void _updateRealVisible()
		{
			bool flag = this.m_parent != null;
			if (flag)
			{
				this.m_realVisible = (this.m_parent.m_realVisible && this.m_visible);
			}
			else
			{
				this.m_realVisible = this.m_visible;
			}
		}

		public Vec3 characterPos(Vec3 pos)
		{
			this.m_character.Move(new Vector3(pos.x, pos.y, pos.z));
			return new Vec3(this.m_character.transform.position.x, this.m_character.transform.position.y, this.m_character.transform.position.z);
		}

		protected void setLayer(GameObject obj, int layer)
		{
			bool flag = obj == null;
			if (!flag)
			{
				bool flag2 = obj.transform.childCount > 0;
				if (flag2)
				{
					for (int i = 0; i < obj.transform.childCount; i++)
					{
						this.setLayer(obj.transform.GetChild(i).gameObject, layer);
					}
				}
			}
		}

		public virtual void addEventListener(Define.EventType eventType, Action<Event> cbFunc)
		{
			bool flag = cbFunc == null;
			if (!flag)
			{
				bool flag2 = this.m_eventFunc.ContainsKey(eventType);
				if (flag2)
				{
					Dictionary<Define.EventType, Action<Event>> eventFunc = this.m_eventFunc;
					eventFunc[eventType] = (Action<Event>)Delegate.Combine(eventFunc[eventType], cbFunc);
				}
				else
				{
					this.m_eventFunc[eventType] = cbFunc;
				}
			}
		}

		public virtual void removeEventListener(Define.EventType eventType, Action<Event> cbFunc)
		{
			bool flag = cbFunc == null;
			if (!flag)
			{
				bool flag2 = this.m_eventFunc.ContainsKey(eventType);
				if (flag2)
				{
					Dictionary<Define.EventType, Action<Event>> eventFunc = this.m_eventFunc;
					eventFunc[eventType] = (Action<Event>)Delegate.Remove(eventFunc[eventType], cbFunc);
				}
			}
		}

		public virtual void clearAllEventListeners()
		{
			this.m_eventFunc.Clear();
		}

		public virtual void onProcess(float tmSlice)
		{
		}

		public void attachTo(GameObject dest)
		{
			bool flag = dest == null;
			if (!flag)
			{
				bool flag2 = this.m_parent != null;
				if (flag2)
				{
					this.m_parent.removeChild(this);
				}
				this.m_u3dObj.transform.parent = ((dest != null) ? dest.transform : null);
				this.m_u3dObj.transform.localPosition = this.m_u3dPos;
				this.m_u3dObj.transform.localEulerAngles = this.m_u3dRot;
				this.m_u3dObj.transform.localScale = this.m_u3dScale;
			}
		}

		public void addScript(string objName, string ScriptName)
		{
			bool flag = this.m_u3dObj == null;
			if (!flag)
			{
				this.m_u3dObj.GetComponent<Object3DBehaviour>().addScript(this.m_u3dObj, objName, ScriptName);
			}
		}

		public void addevent(float tim, Action act)
		{
			bool flag = this.m_u3dObj == null;
			if (!flag)
			{
				this.m_u3dObj.GetComponent<Object3DBehaviour>().addevent(tim, act);
			}
		}
	}
}
