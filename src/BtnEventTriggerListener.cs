using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnEventTriggerListener : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
	public delegate void VoidDelegate(GameObject go);

	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	private ArrayList m_arr;

	public BtnEventTriggerListener.VoidDelegate onClick;

	public BtnEventTriggerListener.VoidDelegate onDown;

	public BtnEventTriggerListener.VoidDelegate onExit;

	public BtnEventTriggerListener.VoidDelegate onUp;

	public static BtnEventTriggerListener Get(GameObject go)
	{
		bool flag = go == null;
		BtnEventTriggerListener result;
		if (flag)
		{
			Debug.LogError("BtnEventTriggerListener_go_is_NULL");
			result = null;
		}
		else
		{
			BtnEventTriggerListener btnEventTriggerListener = go.GetComponent<BtnEventTriggerListener>();
			bool flag2 = btnEventTriggerListener == null;
			if (flag2)
			{
				btnEventTriggerListener = go.AddComponent<BtnEventTriggerListener>();
			}
			result = btnEventTriggerListener;
		}
		return result;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		bool flag = this.onClick != null;
		if (flag)
		{
			this.onClick(base.gameObject);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		bool flag = this.onDown != null;
		if (flag)
		{
			this.onDown(base.gameObject);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		bool flag = this.onExit != null;
		if (flag)
		{
			this.onExit(base.gameObject);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		bool flag = this.onUp != null;
		if (flag)
		{
			this.onUp(base.gameObject);
		}
	}

	public void clearAllListener()
	{
		this.onClick = null;
		this.onDown = null;
		this.onExit = null;
		this.onUp = null;
		UnityEngine.Object.Destroy(base.gameObject.GetComponent<BtnEventTriggerListener>());
	}
}
