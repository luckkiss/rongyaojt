using LuaInterface;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TextWrap
{
	private static Type classType = typeof(Text);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("FontTextureChanged", new LuaCSFunction(TextWrap.FontTextureChanged)),
			new LuaMethod("GetGenerationSettings", new LuaCSFunction(TextWrap.GetGenerationSettings)),
			new LuaMethod("GetTextAnchorPivot", new LuaCSFunction(TextWrap.GetTextAnchorPivot)),
			new LuaMethod("CalculateLayoutInputHorizontal", new LuaCSFunction(TextWrap.CalculateLayoutInputHorizontal)),
			new LuaMethod("CalculateLayoutInputVertical", new LuaCSFunction(TextWrap.CalculateLayoutInputVertical)),
			new LuaMethod("New", new LuaCSFunction(TextWrap._CreateText)),
			new LuaMethod("GetClassType", new LuaCSFunction(TextWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(TextWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("cachedTextGenerator", new LuaCSFunction(TextWrap.get_cachedTextGenerator), null),
			new LuaField("cachedTextGeneratorForLayout", new LuaCSFunction(TextWrap.get_cachedTextGeneratorForLayout), null),
			new LuaField("mainTexture", new LuaCSFunction(TextWrap.get_mainTexture), null),
			new LuaField("font", new LuaCSFunction(TextWrap.get_font), new LuaCSFunction(TextWrap.set_font)),
			new LuaField("text", new LuaCSFunction(TextWrap.get_text), new LuaCSFunction(TextWrap.set_text)),
			new LuaField("supportRichText", new LuaCSFunction(TextWrap.get_supportRichText), new LuaCSFunction(TextWrap.set_supportRichText)),
			new LuaField("resizeTextForBestFit", new LuaCSFunction(TextWrap.get_resizeTextForBestFit), new LuaCSFunction(TextWrap.set_resizeTextForBestFit)),
			new LuaField("resizeTextMinSize", new LuaCSFunction(TextWrap.get_resizeTextMinSize), new LuaCSFunction(TextWrap.set_resizeTextMinSize)),
			new LuaField("resizeTextMaxSize", new LuaCSFunction(TextWrap.get_resizeTextMaxSize), new LuaCSFunction(TextWrap.set_resizeTextMaxSize)),
			new LuaField("alignment", new LuaCSFunction(TextWrap.get_alignment), new LuaCSFunction(TextWrap.set_alignment)),
			new LuaField("alignByGeometry", new LuaCSFunction(TextWrap.get_alignByGeometry), new LuaCSFunction(TextWrap.set_alignByGeometry)),
			new LuaField("fontSize", new LuaCSFunction(TextWrap.get_fontSize), new LuaCSFunction(TextWrap.set_fontSize)),
			new LuaField("horizontalOverflow", new LuaCSFunction(TextWrap.get_horizontalOverflow), new LuaCSFunction(TextWrap.set_horizontalOverflow)),
			new LuaField("verticalOverflow", new LuaCSFunction(TextWrap.get_verticalOverflow), new LuaCSFunction(TextWrap.set_verticalOverflow)),
			new LuaField("lineSpacing", new LuaCSFunction(TextWrap.get_lineSpacing), new LuaCSFunction(TextWrap.set_lineSpacing)),
			new LuaField("fontStyle", new LuaCSFunction(TextWrap.get_fontStyle), new LuaCSFunction(TextWrap.set_fontStyle)),
			new LuaField("pixelsPerUnit", new LuaCSFunction(TextWrap.get_pixelsPerUnit), null),
			new LuaField("minWidth", new LuaCSFunction(TextWrap.get_minWidth), null),
			new LuaField("preferredWidth", new LuaCSFunction(TextWrap.get_preferredWidth), null),
			new LuaField("flexibleWidth", new LuaCSFunction(TextWrap.get_flexibleWidth), null),
			new LuaField("minHeight", new LuaCSFunction(TextWrap.get_minHeight), null),
			new LuaField("preferredHeight", new LuaCSFunction(TextWrap.get_preferredHeight), null),
			new LuaField("flexibleHeight", new LuaCSFunction(TextWrap.get_flexibleHeight), null),
			new LuaField("layoutPriority", new LuaCSFunction(TextWrap.get_layoutPriority), null)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.UI.Text", typeof(Text), regs, fields, typeof(MaskableGraphic));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateText(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Text class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, TextWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cachedTextGenerator(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cachedTextGenerator");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cachedTextGenerator on a nil value");
			}
		}
		LuaScriptMgr.PushObject(L, text.cachedTextGenerator);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cachedTextGeneratorForLayout(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cachedTextGeneratorForLayout");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cachedTextGeneratorForLayout on a nil value");
			}
		}
		LuaScriptMgr.PushObject(L, text.cachedTextGeneratorForLayout);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mainTexture(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainTexture");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainTexture on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.mainTexture);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_font(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name font");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index font on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.font);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_text(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name text");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index text on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.text);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_supportRichText(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name supportRichText");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index supportRichText on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.supportRichText);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_resizeTextForBestFit(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name resizeTextForBestFit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index resizeTextForBestFit on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.resizeTextForBestFit);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_resizeTextMinSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name resizeTextMinSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index resizeTextMinSize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.resizeTextMinSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_resizeTextMaxSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name resizeTextMaxSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index resizeTextMaxSize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.resizeTextMaxSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_alignment(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alignment");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alignment on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.alignment);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_alignByGeometry(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alignByGeometry");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alignByGeometry on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.alignByGeometry);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fontSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fontSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fontSize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.fontSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_horizontalOverflow(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalOverflow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalOverflow on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.horizontalOverflow);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_verticalOverflow(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalOverflow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalOverflow on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.verticalOverflow);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lineSpacing(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lineSpacing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lineSpacing on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.lineSpacing);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fontStyle(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fontStyle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fontStyle on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.fontStyle);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pixelsPerUnit(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelsPerUnit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelsPerUnit on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.pixelsPerUnit);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_minWidth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minWidth on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.minWidth);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_preferredWidth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preferredWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preferredWidth on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.preferredWidth);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_flexibleWidth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name flexibleWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index flexibleWidth on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.flexibleWidth);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_minHeight(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minHeight on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.minHeight);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_preferredHeight(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preferredHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preferredHeight on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.preferredHeight);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_flexibleHeight(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name flexibleHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index flexibleHeight on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.flexibleHeight);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_layoutPriority(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layoutPriority");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layoutPriority on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.layoutPriority);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_font(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name font");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index font on a nil value");
			}
		}
		text.font = (Font)LuaScriptMgr.GetUnityObject(L, 3, typeof(Font));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_text(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name text");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index text on a nil value");
			}
		}
		text.text = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_supportRichText(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name supportRichText");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index supportRichText on a nil value");
			}
		}
		text.supportRichText = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_resizeTextForBestFit(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name resizeTextForBestFit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index resizeTextForBestFit on a nil value");
			}
		}
		text.resizeTextForBestFit = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_resizeTextMinSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name resizeTextMinSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index resizeTextMinSize on a nil value");
			}
		}
		text.resizeTextMinSize = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_resizeTextMaxSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name resizeTextMaxSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index resizeTextMaxSize on a nil value");
			}
		}
		text.resizeTextMaxSize = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_alignment(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alignment");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alignment on a nil value");
			}
		}
		text.alignment = (TextAnchor)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(TextAnchor)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_alignByGeometry(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alignByGeometry");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alignByGeometry on a nil value");
			}
		}
		text.alignByGeometry = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fontSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fontSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fontSize on a nil value");
			}
		}
		text.fontSize = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_horizontalOverflow(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalOverflow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalOverflow on a nil value");
			}
		}
		text.horizontalOverflow = (HorizontalWrapMode)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(HorizontalWrapMode)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_verticalOverflow(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalOverflow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalOverflow on a nil value");
			}
		}
		text.verticalOverflow = (VerticalWrapMode)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(VerticalWrapMode)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lineSpacing(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lineSpacing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lineSpacing on a nil value");
			}
		}
		text.lineSpacing = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fontStyle(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Text text = (Text)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fontStyle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fontStyle on a nil value");
			}
		}
		text.fontStyle = (FontStyle)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(FontStyle)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int FontTextureChanged(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Text text = (Text)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Text");
		text.FontTextureChanged();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetGenerationSettings(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Text text = (Text)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Text");
		Vector2 vector = LuaScriptMgr.GetVector2(L, 2);
		TextGenerationSettings generationSettings = text.GetGenerationSettings(vector);
		LuaScriptMgr.PushValue(L, generationSettings);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTextAnchorPivot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TextAnchor anchor = (TextAnchor)((int)LuaScriptMgr.GetNetObject(L, 1, typeof(TextAnchor)));
		Vector2 textAnchorPivot = Text.GetTextAnchorPivot(anchor);
		LuaScriptMgr.Push(L, textAnchorPivot);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CalculateLayoutInputHorizontal(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Text text = (Text)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Text");
		text.CalculateLayoutInputHorizontal();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CalculateLayoutInputVertical(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Text text = (Text)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Text");
		text.CalculateLayoutInputVertical();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEngine.Object x = LuaScriptMgr.GetLuaObject(L, 1) as UnityEngine.Object;
		UnityEngine.Object y = LuaScriptMgr.GetLuaObject(L, 2) as UnityEngine.Object;
		bool b = x == y;
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
