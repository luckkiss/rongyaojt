using System;
using UnityEngine;

public static class EnumShader
{
	public static int SPI_COLOR = Shader.PropertyToID("_Color");

	public static int SPI_RIMCOLOR = Shader.PropertyToID("_RimColor");

	public static int SPI_RIMWIDTH = Shader.PropertyToID("_RimWidth");

	public static int SPI_TINT_COLOR = Shader.PropertyToID("_TintColor");

	public static int SPI_MAINTEX = Shader.PropertyToID("_MainTex");

	public static int SPI_SUBTEX = Shader.PropertyToID("_SubTex");

	public static int SPI_SPLCOLOR = Shader.PropertyToID("_SplColor");

	public static int SPI_STRCOLOR = Shader.PropertyToID("_StrColor");

	public static int SPI_CHANGECOLOR = Shader.PropertyToID("_ChangeColor");

	public static int SPI_SPLRIM = Shader.PropertyToID("_SplRim");

	public static int SPI_STRANIM = Shader.PropertyToID("_StrAnim");
}
