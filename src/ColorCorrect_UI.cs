using System;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/ColorCorrect_UI")]
public class ColorCorrect_UI : BaseMeshEffect
{
	[SerializeField]
	private float Brightness = 1f;

	[SerializeField]
	private float Saturation = 1f;

	[SerializeField]
	private float Contrast = 1f;

	private int nShader_B;

	private int nShader_S;

	private int nShader_C;

	private Material matCCUI;

	public override void ModifyMesh(VertexHelper vh)
	{
		if (!this.IsActive())
		{
			return;
		}
	}

	private new void Start()
	{
		this.nShader_B = Shader.PropertyToID("_BrightnessAmount");
		this.nShader_S = Shader.PropertyToID("_SaturationAmount");
		this.nShader_C = Shader.PropertyToID("_ContrastAmount");
		this.matCCUI = Resources.Load<Material>("uifx/ui_color_correct");
		Image component = base.GetComponent<Image>();
		component.material = this.matCCUI;
	}

	private void Update()
	{
		Image component = base.GetComponent<Image>();
		component.material.SetFloat(this.nShader_B, this.Brightness);
		component.material.SetFloat(this.nShader_S, this.Saturation);
		component.material.SetFloat(this.nShader_C, this.Contrast);
	}
}
