using System;
using UnityEngine;

public static class EnumLayer
{
	public static int LM_ROLE_INVISIBLE = LayerMask.NameToLayer("role_invisible");

	public static int LM_TERRAIN_CLICK = LayerMask.NameToLayer("terrain_click");

	public static int LM_SELFROLE = LayerMask.NameToLayer("selfrole");

	public static int LM_OTHERPLAYER = LayerMask.NameToLayer("otherplayer");

	public static int LM_MONSTER = LayerMask.NameToLayer("monster");

	public static int LM_COLLECT = LayerMask.NameToLayer("collect");

	public static int LM_PET = LayerMask.NameToLayer("pet");

	public static int LM_SCENE_NORMAL = LayerMask.NameToLayer("scene_normal");

	public static int LM_SCENE_LIGHT = LayerMask.NameToLayer("scene_light");

	public static int LM_SCENE_SHADOW = LayerMask.NameToLayer("scene_shadow");

	public static int LM_FX = LayerMask.NameToLayer("fx");

	public static int LM_PT = LayerMask.NameToLayer("pt");

	public static int LM_BT_SELF = LayerMask.NameToLayer("bt_self");

	public static int LM_BT_FIGHT = LayerMask.NameToLayer("bt_fight");

	public static int LM_MAP_ITEM = LayerMask.NameToLayer("mapitem");

	public static int LM_NPC = LayerMask.NameToLayer("npc");

	public static int LM_DEFAULT = LayerMask.NameToLayer("Default");
}
