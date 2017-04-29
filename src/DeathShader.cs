using System;
using UnityEngine;

[ExecuteInEditMode]
public class DeathShader : MonoBehaviour
{
	private Shader curShader;

	private float grayScaleAmount = 1f;

	private Material curMaterial;

	public Material material
	{
		get
		{
			bool flag = this.curMaterial == null;
			if (flag)
			{
				this.curMaterial = new Material(this.curShader);
				this.curMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.curMaterial;
		}
	}

	private void Start()
	{
		this.curShader = (Resources.Load("CameraShaders/DeathShader") as Shader);
		bool flag = !SystemInfo.supportsImageEffects;
		if (flag)
		{
			base.enabled = false;
		}
		else
		{
			bool flag2 = this.curShader != null && !this.curShader.isSupported;
			if (flag2)
			{
				base.enabled = false;
			}
		}
	}

	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		bool flag = this.curShader != null;
		if (flag)
		{
			this.material.SetFloat("_LuminosityAmount", this.grayScaleAmount);
			Graphics.Blit(sourceTexture, destTexture, this.material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}

	private void Update()
	{
		this.grayScaleAmount = Mathf.Clamp(this.grayScaleAmount, 0f, 1f);
	}

	private void OnDisable()
	{
		bool flag = this.curMaterial != null;
		if (flag)
		{
			UnityEngine.Object.DestroyImmediate(this.curMaterial);
		}
	}
}
