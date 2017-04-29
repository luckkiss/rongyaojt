using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
	public class ScrollControler
	{
		private ScrollRect m_scrollR;

		private float m_fSRDragedY;

		private int m_nSRDragCancel = 0;

		private int m_srDragNum;

		public void create(ScrollRect scroll, int srDragNum = 4)
		{
			this.m_scrollR = scroll;
			this.m_srDragNum = srDragNum;
			EventTriggerListener.Get(this.m_scrollR.gameObject).onDrag = new EventTriggerListener.VectorDelegate(this.onDragSR);
			EventTriggerListener.Get(this.m_scrollR.gameObject).onDragEnd = new EventTriggerListener.VectorDelegate(this.onDragSRend);
			EventTriggerListener.Get(this.m_scrollR.gameObject).onInPoDrag = new EventTriggerListener.VectorDelegate(this.onInPoDragSR);
		}

		private void onDragSR(GameObject go, Vector2 delta)
		{
			this.m_nSRDragCancel--;
		}

		private void onDragSRend(GameObject go, Vector2 pos)
		{
			bool flag = this.m_nSRDragCancel > 0;
			if (flag)
			{
				float value = (pos.y - this.m_fSRDragedY) * 5f;
				bool flag2 = Math.Abs(value) > 2f;
				if (flag2)
				{
					Vector2 normalizedPosition = this.m_scrollR.normalizedPosition;
					bool flag3 = normalizedPosition.y > 0.98f;
					if (flag3)
					{
						normalizedPosition.y = 0.98f;
					}
					bool flag4 = normalizedPosition.y < 0.02f;
					if (flag4)
					{
						normalizedPosition.y = 0.02f;
					}
					this.m_scrollR.normalizedPosition = normalizedPosition;
					Vector2 velocity = this.m_scrollR.velocity;
					velocity.y += (pos.y - this.m_fSRDragedY) * 5f;
					this.m_scrollR.velocity = velocity;
				}
			}
		}

		private void onInPoDragSR(GameObject go, Vector2 pos)
		{
			this.m_fSRDragedY = pos.y;
			this.m_nSRDragCancel = 4;
		}
	}
}
