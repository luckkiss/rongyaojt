using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerListener : EventTrigger
{
	public delegate void VoidDelegate(GameObject go);

	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	public EventTriggerListener.VoidDelegate onClick;

	public EventTriggerListener.VectorDelegate onPointClick;

	public EventTriggerListener.VoidDelegate onDown;

	public EventTriggerListener.VoidDelegate onEnter;

	public EventTriggerListener.VoidDelegate onExit;

	public EventTriggerListener.VoidDelegate onUp;

	public EventTriggerListener.VectorDelegate onMove;

	public EventTriggerListener.VoidDelegate onSelect;

	public EventTriggerListener.VoidDelegate onUpdateSelect;

	public EventTriggerListener.VoidDelegate onDragIn;

	public EventTriggerListener.VectorDelegate onDrag;

	public EventTriggerListener.VoidDelegate onDragOut;

	public EventTriggerListener.VectorDelegate onDragEnd;

	public EventTriggerListener.VectorDelegate onInPoDrag;

	public static EventTriggerListener Get(GameObject go)
	{
		bool flag = go == null;
		EventTriggerListener result;
		if (flag)
		{
			Debug.LogError("EventTriggerListener_go_is_NULL");
			result = null;
		}
		else
		{
			EventTriggerListener eventTriggerListener = go.GetComponent<EventTriggerListener>();
			bool flag2 = eventTriggerListener == null;
			if (flag2)
			{
				eventTriggerListener = go.AddComponent<EventTriggerListener>();
			}
			result = eventTriggerListener;
		}
		return result;
	}

	public override void OnBeginDrag(PointerEventData eventData)
	{
		bool flag = this.onDragIn != null;
		if (flag)
		{
			this.onDragIn(base.gameObject);
		}
	}

	public override void OnInitializePotentialDrag(PointerEventData eventData)
	{
		bool flag = this.onInPoDrag != null;
		if (flag)
		{
			this.onInPoDrag(base.gameObject, eventData.position);
		}
	}

	public override void OnDrag(PointerEventData eventData)
	{
		bool flag = this.onDrag != null;
		if (flag)
		{
			this.onDrag(base.gameObject, eventData.delta);
		}
	}

	public override void OnEndDrag(PointerEventData eventData)
	{
		bool flag = this.onDragOut != null;
		if (flag)
		{
			this.onDragOut(base.gameObject);
		}
		bool flag2 = this.onDragEnd != null;
		if (flag2)
		{
			this.onDragEnd(base.gameObject, eventData.position);
		}
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
		bool flag = this.onClick != null;
		if (flag)
		{
			this.onClick(base.gameObject);
		}
		bool flag2 = this.onPointClick != null;
		if (flag2)
		{
			this.onPointClick(base.gameObject, eventData.position);
		}
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		bool flag = this.onDown != null;
		if (flag)
		{
			this.onDown(base.gameObject);
		}
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		bool flag = this.onEnter != null;
		if (flag)
		{
			this.onEnter(base.gameObject);
		}
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		bool flag = this.onExit != null;
		if (flag)
		{
			this.onExit(base.gameObject);
		}
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		bool flag = this.onUp != null;
		if (flag)
		{
			this.onUp(base.gameObject);
		}
	}

	public override void OnSelect(BaseEventData eventData)
	{
		bool flag = this.onSelect != null;
		if (flag)
		{
			this.onSelect(base.gameObject);
		}
	}

	public override void OnUpdateSelected(BaseEventData eventData)
	{
		bool flag = this.onUpdateSelect != null;
		if (flag)
		{
			this.onUpdateSelect(base.gameObject);
		}
	}

	public override void OnMove(AxisEventData eventData)
	{
		bool flag = this.onMove != null;
		if (flag)
		{
			this.onMove(base.gameObject, eventData.moveVector);
		}
	}

	public void clearAllListener()
	{
		this.onClick = null;
		this.onDown = null;
		this.onEnter = null;
		this.onExit = null;
		this.onUp = null;
		this.onSelect = null;
		this.onUpdateSelect = null;
		this.onDrag = null;
		this.onDragOut = null;
		this.onDragIn = null;
		this.onMove = null;
		this.onInPoDrag = null;
		this.onDragEnd = null;
		UnityEngine.Object.Destroy(base.gameObject.GetComponent<EventTriggerListener>());
	}
}
