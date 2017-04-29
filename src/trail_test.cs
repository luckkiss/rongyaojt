using System;
using UnityEngine;

public class trail_test : MonoBehaviour
{
	public int vertexCount = 6;

	public float startPhase;

	public float length = 10f;

	public float Anispeed = 5f;

	public float ani_offset = 0.5f;

	public AnimationCurve ac;

	private float ani_fudu = 0.2f;

	private float a;

	private float a_cycle;

	private Transform helper;

	private LineRenderer lineR;

	private bool ishaveLineR;

	private void Start()
	{
		this.ishaveLineR = (base.GetComponent<LineRenderer>() != null);
		if (this.ishaveLineR)
		{
			this.lineR = base.gameObject.GetComponent<LineRenderer>();
			this.lineR.SetVertexCount(this.vertexCount + 1);
		}
		else
		{
			Debug.Log(base.name + "需要添加一个<LineRenderer>组件！！！");
		}
		if (base.transform.FindChild("helper"))
		{
			this.helper = base.transform.FindChild("helper").transform;
			this.helper.localPosition = Vector3.zero;
			this.helper.rotation = base.transform.rotation;
		}
		else
		{
			this.helper = new GameObject
			{
				name = "helper",
				transform = 
				{
					parent = base.transform,
					position = base.transform.position,
					rotation = base.transform.rotation
				}
			}.transform;
		}
		this.startPhase %= 360f;
		this.a = this.startPhase;
		this.a_cycle = 11309.7334f;
	}

	private void Update()
	{
		if (this.ishaveLineR)
		{
			this.lineR_Ani();
		}
	}

	private void lineR_Ani()
	{
		for (int i = 0; i < this.vertexCount + 1; i++)
		{
			this.a += Time.deltaTime * this.Anispeed / ((float)i + 2f);
			if (this.a > this.a_cycle)
			{
				this.a = 0f;
			}
			this.ani_fudu = this.ac.Evaluate((float)i * 1f / (float)(this.vertexCount - 1));
			Vector3 localPosition = new Vector3(0f, Mathf.Sin(this.a - (float)i * this.ani_offset) * this.ani_fudu, (float)i * (this.length / (float)this.vertexCount));
			this.helper.localPosition = localPosition;
			this.lineR.SetPosition(i, this.helper.position);
		}
	}
}
