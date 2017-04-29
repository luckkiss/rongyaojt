using System;

namespace Cross
{
	public class Define
	{
		public enum EventType
		{
			UNKNOWN,
			MOUSE_DOWN,
			MOUSE_MOVE,
			MOUSE_UP,
			MOUSE_CLICK,
			MOUSE_OVER,
			MOUSE_LEAVE,
			UI_MOUSE_DOWN,
			UI_MOUSE_UP,
			UI_MOUSE_MOVE,
			JOYSTICK_BEGIN,
			JOYSTICK_MOVE,
			JOYSTICK_END,
			SKANIM_BEGIN,
			SKANIM_END,
			PROCESSS,
			RAYCASTED,
			SLIDERCHANGE
		}

		public enum MouseButton
		{
			None,
			Left,
			Middle,
			Right
		}

		public enum TextAlign
		{
			LEFT,
			MIDDLE,
			RIGHT
		}

		public enum Ccm_status
		{
			NONE,
			LOADING_MAINFILE,
			LOADING_CONF,
			FORMAT_CONF,
			LOADED,
			ERR
		}

		public enum ButtonState
		{
			NORMAL,
			UP,
			DOWN,
			DISABLE,
			SELECTNO,
			SELECTYES,
			SELECTOVER
		}

		public enum UIDirection
		{
			HORIZONTAL,
			VERTICAL,
			BOTH
		}

		public enum Movement
		{
			NONE,
			HORIZONTAL,
			VERTICAL
		}

		public enum ProgressDirection
		{
			HORIZAL,
			VERTICAL
		}

		public enum SkAnimState
		{
			BEGIN,
			END
		}

		public enum Endian
		{
			BIG_ENDIAN,
			LITTLE_ENDIAN
		}

		public enum GREntityType
		{
			STATIC_MESH,
			CHARACTER,
			EFFECT_PARTICLE,
			CAMERA,
			LIGHTDIR,
			LIGHTPOINT,
			BILLBOARD,
			EFFECT_KNIFELIGHT
		}

		public enum PHEntityType
		{
			HEIGHTMAP,
			COLLIDER_MESH
		}

		public enum DebugTrace
		{
			DTT_NONE,
			DTT_SYS,
			DTT_ERR,
			DTT_DTL
		}

		public static int SLIDER_CHANGE = 1101;

		public static int SLIDER_THUMBDRAG = 1102;

		public static int SLIDER_THUMBPRESS = 1103;

		public static int SLIDER_THUMBRELEASE = 1104;

		public static uint COMBOBOX_CHANGE = 1140u;

		public static uint COMBOBOX_CLOSE = 1141u;

		public static uint COMBOBOX_OPEN = 1142u;

		public static uint COMBOBOX_SCROLL = 1143u;

		public static uint COMBOBOX_ITEMROLLOVER = 1144u;

		public static uint COMBOBOX_ITEMROLLOUT = 1145u;

		public static uint SUPERTEXT_ELEMENTCLICK = 1180u;

		public static uint SUPERTEXT_ELEMENTOVER = 1181u;

		public static uint SUPERTEXT_ELEMENTOUT = 1182u;

		public static Define.EventType convertToEventType(string str)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(str);
			Define.EventType result;
			if (num <= 2035911400u)
			{
				if (num <= 931768077u)
				{
					if (num != 13560078u)
					{
						if (num != 597938448u)
						{
							if (num != 931768077u)
							{
								goto IL_251;
							}
							if (!(str == "inputclick"))
							{
								goto IL_251;
							}
						}
						else
						{
							if (!(str == "mousedown"))
							{
								goto IL_251;
							}
							goto IL_22F;
						}
					}
					else
					{
						if (!(str == "inputup"))
						{
							goto IL_251;
						}
						goto IL_237;
					}
				}
				else if (num <= 1551804527u)
				{
					if (num != 1233071654u)
					{
						if (num != 1551804527u)
						{
							goto IL_251;
						}
						if (!(str == "click"))
						{
							goto IL_251;
						}
					}
					else
					{
						if (!(str == "joystickmove"))
						{
							goto IL_251;
						}
						result = Define.EventType.JOYSTICK_MOVE;
						return result;
					}
				}
				else if (num != 1681120112u)
				{
					if (num != 2035911400u)
					{
						goto IL_251;
					}
					if (!(str == "mouseout"))
					{
						goto IL_251;
					}
					result = Define.EventType.MOUSE_LEAVE;
					return result;
				}
				else
				{
					if (!(str == "joystickbegin"))
					{
						goto IL_251;
					}
					result = Define.EventType.JOYSTICK_BEGIN;
					return result;
				}
				result = Define.EventType.MOUSE_CLICK;
				return result;
			}
			if (num > 2826333372u)
			{
				if (num <= 3611423958u)
				{
					if (num != 3260449410u)
					{
						if (num != 3611423958u)
						{
							goto IL_251;
						}
						if (!(str == "mouseover"))
						{
							goto IL_251;
						}
						result = Define.EventType.MOUSE_OVER;
						return result;
					}
					else if (!(str == "inputmove"))
					{
						goto IL_251;
					}
				}
				else if (num != 3923426769u)
				{
					if (num != 4035692073u)
					{
						goto IL_251;
					}
					if (!(str == "mouseup"))
					{
						goto IL_251;
					}
					goto IL_237;
				}
				else if (!(str == "mousemove"))
				{
					goto IL_251;
				}
				result = Define.EventType.MOUSE_MOVE;
				return result;
			}
			if (num <= 2632535418u)
			{
				if (num != 2161338732u)
				{
					if (num != 2632535418u)
					{
						goto IL_251;
					}
					if (!(str == "process"))
					{
						goto IL_251;
					}
					result = Define.EventType.PROCESSS;
					return result;
				}
				else
				{
					if (!(str == "sliderchange"))
					{
						goto IL_251;
					}
					result = Define.EventType.SLIDERCHANGE;
					return result;
				}
			}
			else if (num != 2689894135u)
			{
				if (num != 2826333372u)
				{
					goto IL_251;
				}
				if (!(str == "joystickend"))
				{
					goto IL_251;
				}
				result = Define.EventType.JOYSTICK_END;
				return result;
			}
			else if (!(str == "inputdown"))
			{
				goto IL_251;
			}
			IL_22F:
			result = Define.EventType.MOUSE_DOWN;
			return result;
			IL_237:
			result = Define.EventType.MOUSE_UP;
			return result;
			IL_251:
			result = Define.EventType.UNKNOWN;
			return result;
		}
	}
}
