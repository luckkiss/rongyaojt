using System;
using UnityEngine;

namespace Cross
{
	public class OsBehaviour : MonoBehaviour
	{
		protected float m_lastMouseX;

		protected float m_lastMouseY;

		private float m_stateClickTime = 0f;

		private float m_endClickTime = 0f;

		private Vec2 m_stateClickMoveLength = new Vec2(0f, 0f);

		protected Vec2 m_endClickMoveLength = new Vec2(0f, 0f);

		private Collider2D[] m_collider2D = new Collider2D[1];

		private int m_colliderNum = 0;

		private Vector2 m_pos = new Vector2(0f, 0f);

		private Vec2 m_ptW = new Vec2(0f, 0f);

		private Vec2 m_downW = new Vec2(0f, 0f);

		private Vector2 m_startPoint = new Vector2(0f, 0f);

		private Vector2 m_endPoint = new Vector2(0f, 0f);

		private Vec2 m_GlobalPoint = new Vec2(0f, 0f);

		private void Start()
		{
			this.m_lastMouseX = -1f;
			this.m_lastMouseY = -1f;
		}

		private bool isPickObj(Vector2 pos)
		{
			return false;
		}

		private GameObject isClickObj(Vector2 pos)
		{
			return null;
		}

		private void Update()
		{
			osImpl singleton = osImpl.singleton;
			singleton.onProcess(Time.deltaTime);
			singleton.onRender(Time.deltaTime);
		}
	}
}
