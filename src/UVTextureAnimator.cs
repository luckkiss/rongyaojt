using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

internal class UVTextureAnimator : MonoBehaviour
{
	public Material[] AnimatedMaterialsNotInstance;

	public int Rows = 4;

	public int Columns = 4;

	public float Fps = 20f;

	public int OffsetMat;

	public Vector2 SelfTiling = default(Vector2);

	public bool IsLoop = true;

	public bool IsReverse;

	public bool IsRandomOffsetForInctance;

	public bool IsBump;

	public bool IsHeight;

	private bool isInizialised;

	private int index;

	private int count;

	private int allCount;

	private float deltaFps;

	private bool isVisible;

	private bool isCorutineStarted;

	private Renderer currentRenderer;

	private Material instanceMaterial;

	private void Awake()
	{
		this.InitDefaultVariables();
		this.isInizialised = true;
	}

	public void SetInstanceMaterial(Material mat, Vector2 offsetMat)
	{
		this.instanceMaterial = mat;
		this.InitDefaultVariables();
	}

	private void InitDefaultVariables()
	{
		this.currentRenderer = base.GetComponent<Renderer>();
		this.allCount = 0;
		this.deltaFps = 1f / this.Fps;
		this.count = this.Rows * this.Columns;
		this.index = this.Columns - 1;
		Vector2 offset = new Vector2((float)this.index / (float)this.Columns - (float)(this.index / this.Columns), 1f - (float)(this.index / this.Columns) / (float)this.Rows);
		this.OffsetMat = (this.IsRandomOffsetForInctance ? UnityEngine.Random.Range(0, this.count) : (this.OffsetMat - this.OffsetMat / this.count * this.count));
		Vector2 scale = (!(this.SelfTiling == Vector2.zero)) ? this.SelfTiling : new Vector2(1f / (float)this.Columns, 1f / (float)this.Rows);
		if (this.AnimatedMaterialsNotInstance.Length > 0)
		{
			Material[] animatedMaterialsNotInstance = this.AnimatedMaterialsNotInstance;
			for (int i = 0; i < animatedMaterialsNotInstance.Length; i++)
			{
				Material material = animatedMaterialsNotInstance[i];
				material.SetTextureScale("_MainTex", scale);
				material.SetTextureOffset("_MainTex", Vector2.zero);
				if (this.IsBump)
				{
					material.SetTextureScale("_BumpMap", scale);
					material.SetTextureOffset("_BumpMap", Vector2.zero);
				}
				if (this.IsHeight)
				{
					material.SetTextureScale("_HeightMap", scale);
					material.SetTextureOffset("_HeightMap", Vector2.zero);
				}
			}
		}
		else if (this.instanceMaterial != null)
		{
			this.instanceMaterial.SetTextureScale("_MainTex", scale);
			this.instanceMaterial.SetTextureOffset("_MainTex", offset);
			if (this.IsBump)
			{
				this.instanceMaterial.SetTextureScale("_BumpMap", scale);
				this.instanceMaterial.SetTextureOffset("_BumpMap", offset);
			}
			if (this.IsBump)
			{
				this.instanceMaterial.SetTextureScale("_HeightMap", scale);
				this.instanceMaterial.SetTextureOffset("_HeightMap", offset);
			}
		}
		else if (this.currentRenderer != null)
		{
			this.currentRenderer.material.SetTextureScale("_MainTex", scale);
			this.currentRenderer.material.SetTextureOffset("_MainTex", offset);
			if (this.IsBump)
			{
				this.currentRenderer.material.SetTextureScale("_BumpMap", scale);
				this.currentRenderer.material.SetTextureOffset("_BumpMap", offset);
			}
			if (this.IsBump)
			{
				this.currentRenderer.material.SetTextureScale("_HeightMap", scale);
				this.currentRenderer.material.SetTextureOffset("_HeightMap", offset);
			}
		}
	}

	private void OnEnable()
	{
		if (this.isInizialised)
		{
			this.InitDefaultVariables();
		}
		this.isVisible = true;
		if (!this.isCorutineStarted)
		{
			base.StartCoroutine(this.UpdateCorutine());
		}
	}

	private void OnDisable()
	{
		this.isCorutineStarted = false;
		this.isVisible = false;
		base.StopAllCoroutines();
	}

	private void OnBecameVisible()
	{
		this.isVisible = true;
		if (!this.isCorutineStarted)
		{
			base.StartCoroutine(this.UpdateCorutine());
		}
	}

	private void OnBecameInvisible()
	{
		this.isVisible = false;
	}

	[DebuggerHidden]
	private IEnumerator UpdateCorutine()
	{
		UVTextureAnimator.<UpdateCorutine>c__Iterator4 <UpdateCorutine>c__Iterator = new UVTextureAnimator.<UpdateCorutine>c__Iterator4();
		<UpdateCorutine>c__Iterator.<>f__this = this;
		return <UpdateCorutine>c__Iterator;
	}

	private void UpdateCorutineFrame()
	{
		if (this.currentRenderer == null && this.instanceMaterial == null && this.AnimatedMaterialsNotInstance.Length == 0)
		{
			return;
		}
		this.allCount++;
		if (this.IsReverse)
		{
			this.index--;
		}
		else
		{
			this.index++;
		}
		if (this.index >= this.count)
		{
			this.index = 0;
		}
		if (this.AnimatedMaterialsNotInstance.Length > 0)
		{
			for (int i = 0; i < this.AnimatedMaterialsNotInstance.Length; i++)
			{
				int num = i * this.OffsetMat + this.index;
				num -= num / this.count * this.count;
				Vector2 offset = new Vector2((float)num / (float)this.Columns - (float)(num / this.Columns), 1f - (float)(num / this.Columns) / (float)this.Rows);
				this.AnimatedMaterialsNotInstance[i].SetTextureOffset("_MainTex", offset);
				if (this.IsBump)
				{
					this.AnimatedMaterialsNotInstance[i].SetTextureOffset("_BumpMap", offset);
				}
				if (this.IsHeight)
				{
					this.AnimatedMaterialsNotInstance[i].SetTextureOffset("_HeightMap", offset);
				}
			}
		}
		else
		{
			Vector2 offset2;
			if (this.IsRandomOffsetForInctance)
			{
				int num2 = this.index + this.OffsetMat;
				offset2 = new Vector2((float)num2 / (float)this.Columns - (float)(num2 / this.Columns), 1f - (float)(num2 / this.Columns) / (float)this.Rows);
			}
			else
			{
				offset2 = new Vector2((float)this.index / (float)this.Columns - (float)(this.index / this.Columns), 1f - (float)(this.index / this.Columns) / (float)this.Rows);
			}
			if (this.instanceMaterial != null)
			{
				this.instanceMaterial.SetTextureOffset("_MainTex", offset2);
				if (this.IsBump)
				{
					this.instanceMaterial.SetTextureOffset("_BumpMap", offset2);
				}
				if (this.IsHeight)
				{
					this.instanceMaterial.SetTextureOffset("_HeightMap", offset2);
				}
			}
			else if (this.currentRenderer != null)
			{
				this.currentRenderer.material.SetTextureOffset("_MainTex", offset2);
				if (this.IsBump)
				{
					this.currentRenderer.material.SetTextureOffset("_BumpMap", offset2);
				}
				if (this.IsHeight)
				{
					this.currentRenderer.material.SetTextureOffset("_HeightMap", offset2);
				}
			}
		}
	}
}
