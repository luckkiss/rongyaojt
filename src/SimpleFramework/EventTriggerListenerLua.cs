using LuaInterface;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleFramework
{
	public class EventTriggerListenerLua : EventTrigger
	{
		public LuaFunction onClick;

		public LuaFunction onPointClick;

		public LuaFunction onDown;

		public LuaFunction onEnter;

		public LuaFunction onExit;

		public LuaFunction onUp;

		public LuaFunction onMove;

		public LuaFunction onSelect;

		public LuaFunction onUpdateSelect;

		public LuaFunction onDragIn;

		public LuaFunction onDrag;

		public LuaFunction onDragOut;

		public LuaFunction onDragEnd;

		public LuaFunction onInPoDrag;

		public static EventTriggerListenerLua Get(GameObject go)
		{
			if (go == null)
			{
				Debug.LogError("EventTriggerListener_go_is_NULL");
				return null;
			}
			EventTriggerListenerLua eventTriggerListenerLua = go.GetComponent<EventTriggerListenerLua>();
			if (eventTriggerListenerLua == null)
			{
				eventTriggerListenerLua = go.AddComponent<EventTriggerListenerLua>();
			}
			return eventTriggerListenerLua;
		}

		public override void OnBeginDrag(PointerEventData eventData)
		{
			if (this.onDragIn != null)
			{
				this.onDragIn.Call(new object[]
				{
					base.gameObject
				});
			}
		}

		public override void OnInitializePotentialDrag(PointerEventData eventData)
		{
			if (this.onInPoDrag != null)
			{
				this.onInPoDrag.Call(new object[]
				{
					base.gameObject,
					eventData.position
				});
			}
		}

		public override void OnDrag(PointerEventData eventData)
		{
			if (this.onDrag != null)
			{
				this.onDrag.Call(new object[]
				{
					base.gameObject,
					eventData.delta
				});
			}
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			if (this.onDragOut != null)
			{
				this.onDragOut.Call(new object[]
				{
					base.gameObject
				});
			}
			if (this.onDragEnd != null)
			{
				this.onDragEnd.Call(new object[]
				{
					base.gameObject,
					eventData.position
				});
			}
		}

		public override void OnPointerClick(PointerEventData eventData)
		{
			if (this.onClick != null)
			{
				this.onClick.Call(new object[]
				{
					base.gameObject
				});
			}
			if (this.onPointClick != null)
			{
				this.onPointClick.Call(new object[]
				{
					base.gameObject,
					eventData.position
				});
			}
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			if (this.onDown != null)
			{
				this.onDown.Call(new object[]
				{
					base.gameObject
				});
			}
		}

		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (this.onEnter != null)
			{
				this.onEnter.Call(new object[]
				{
					base.gameObject
				});
			}
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			if (this.onExit != null)
			{
				this.onExit.Call(new object[]
				{
					base.gameObject
				});
			}
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			if (this.onUp != null)
			{
				this.onUp.Call(new object[]
				{
					base.gameObject
				});
			}
		}

		public override void OnSelect(BaseEventData eventData)
		{
			if (this.onSelect != null)
			{
				this.onSelect.Call(new object[]
				{
					base.gameObject
				});
			}
		}

		public override void OnUpdateSelected(BaseEventData eventData)
		{
			if (this.onUpdateSelect != null)
			{
				this.onUpdateSelect.Call(new object[]
				{
					base.gameObject
				});
			}
		}

		public override void OnMove(AxisEventData eventData)
		{
			if (this.onMove != null)
			{
				this.onMove.Call(new object[]
				{
					base.gameObject,
					eventData.moveVector
				});
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
}
