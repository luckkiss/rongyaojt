using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public interface IGraphObject3D : IGraphObject
	{
		int layer
		{
			get;
			set;
		}

		Dictionary<string, object> helper
		{
			get;
		}

		Mat44 matrix
		{
			get;
		}

		BoundBox boxCollider
		{
			get;
			set;
		}

		bool characterColliderbool
		{
			get;
			set;
		}

		float slopeLimit
		{
			get;
			set;
		}

		float stepOffset
		{
			get;
			set;
		}

		float radius
		{
			get;
			set;
		}

		float height
		{
			get;
			set;
		}

		Vec3 center
		{
			get;
			set;
		}

		Vec3 pos
		{
			get;
			set;
		}

		float x
		{
			get;
			set;
		}

		float y
		{
			get;
			set;
		}

		float z
		{
			get;
			set;
		}

		Vec3 rot
		{
			get;
			set;
		}

		float rotX
		{
			get;
			set;
		}

		float rotY
		{
			get;
			set;
		}

		float rotZ
		{
			get;
			set;
		}

		string name
		{
			get;
			set;
		}

		Vec3 scale
		{
			get;
			set;
		}

		float scaleX
		{
			get;
			set;
		}

		float scaleY
		{
			get;
			set;
		}

		float scaleZ
		{
			get;
			set;
		}

		Vec3 axisX
		{
			get;
		}

		Vec3 axisY
		{
			get;
		}

		Vec3 axisZ
		{
			get;
		}

		bool visible
		{
			get;
			set;
		}

		GameObject u3dObject
		{
			get;
		}

		IContainer3D parent
		{
			get;
		}

		void dispose();

		void addEventListener(Define.EventType eventType, Action<Event> cbFunc);

		void removeEventListener(Define.EventType eventType, Action<Event> cbFunc);

		void clearAllEventListeners();

		void addScript(string objName, string ScriptName);

		void addevent(float tim, Action act);

		Vec3 characterPos(Vec3 v);
	}
}
