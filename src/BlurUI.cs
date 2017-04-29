using System;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/BlurUI")]
public class BlurUI : BaseMeshEffect
{
	[SerializeField]
	private float blurX = 1f;

	[SerializeField]
	private float blurY = 1f;

	private int nShader_X;

	private int nShader_Y;

	private Material matBlurUI;

	public override void ModifyMesh(VertexHelper vh)
	{
		if (!this.IsActive())
		{
			return;
		}
	}

	private new void Start()
	{
		this.nShader_X = Shader.PropertyToID("_X");
		this.nShader_Y = Shader.PropertyToID("_Y");
		this.matBlurUI = Resources.Load<Material>("uifx/ui_blur");
		Image component = base.GetComponent<Image>();
		component.material = this.matBlurUI;
	}

	private void Update()
	{
		Image component = base.GetComponent<Image>();
		component.material.SetFloat(this.nShader_X, this.blurX);
		component.material.SetFloat(this.nShader_Y, this.blurY);
	}
}
