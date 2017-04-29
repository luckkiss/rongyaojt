using MuGame;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpdateScrollbar : MonoBehaviour
{
	public int max = -1;

	public int cur = -1;

	public int state;

	private Image m_scroll_bar;

	private GameObject line;

	private Action m_funcCallBack;

	private bool m_fOver;

	public Action clickcomfirmHandle;

	public Action clickHandle;

	public GameObject comfirmBox;

	public GameObject msgBox1;

	public Text msgTxt;

	private Text txt;

	private Text lineTxt;

	public bool realT;

	public int tick;

	private int tempTick = 1;

	public void init()
	{
		this.line = base.transform.FindChild("line").gameObject;
		this.m_scroll_bar = base.transform.FindChild("line/Image").gameObject.GetComponent<Image>();
		this.m_scroll_bar.fillAmount = 0f;
		this.lineTxt = base.transform.FindChild("line/Text").GetComponent<Text>();
		this.lineTxt.text = string.Empty;
		this.msgBox1 = base.transform.Find("msgbox").gameObject;
		this.msgTxt = base.transform.Find("msgbox/Text").GetComponent<Text>();
		this.msgBox1.SetActive(false);
		this.comfirmBox = base.transform.Find("confirm").gameObject;
		this.comfirmBox.SetActive(false);
		base.transform.FindChild("confirm/bt").GetComponent<Button>().onClick.AddListener(new UnityAction(this.oncomfirmClick));
		this.txt = base.transform.FindChild("confirm/Text").GetComponent<Text>();
	}

	private void onClick()
	{
		this.comfirmBox.SetActive(false);
		this.clickHandle();
	}

	public void showMsg(string s)
	{
		this.msgTxt.text = s;
		this.msgBox1.SetActive(true);
	}

	public void hideMsg()
	{
		this.msgBox1.SetActive(false);
	}

	public void showComfirm(string desc, Action comfirhandle = null)
	{
		Debug.Log("显示更新框！！！");
		this.txt.text = desc;
		if (comfirhandle == null)
		{
			base.transform.FindChild("confirm/bt").gameObject.SetActive(false);
		}
		else
		{
			base.transform.FindChild("confirm/bt").gameObject.SetActive(true);
			this.clickcomfirmHandle = comfirhandle;
			this.comfirmBox.SetActive(true);
		}
	}

	private void oncomfirmClick()
	{
		this.clickcomfirmHandle();
	}

	private void Update()
	{
		if (this.cur < 0)
		{
			this.line.transform.localScale = Vector3.zero;
			return;
		}
		this.line.transform.localScale = Vector3.one;
		if (this.m_fOver)
		{
			if (this.m_scroll_bar.fillAmount >= 1f)
			{
				this.m_funcCallBack();
				UnityEngine.Object.Destroy(base.gameObject);
			}
			this.m_scroll_bar.fillAmount = this.m_scroll_bar.fillAmount + Time.deltaTime * 3f;
			if (this.m_scroll_bar.fillAmount >= 1f)
			{
				this.m_scroll_bar.fillAmount = 1f;
			}
		}
		else if (this.realT)
		{
			float fillAmount = (float)this.cur / (float)this.max;
			this.m_scroll_bar.fillAmount = fillAmount;
		}
		else
		{
			float num = (float)this.cur / (float)this.max;
			if (this.m_scroll_bar.fillAmount < num)
			{
				this.m_scroll_bar.fillAmount += Time.deltaTime * 0.2f;
			}
			if (this.m_scroll_bar.fillAmount > num)
			{
				this.m_scroll_bar.fillAmount = num;
			}
			if (this.m_scroll_bar.fillAmount >= 0.9f)
			{
				this.m_scroll_bar.fillAmount = 0.9f;
			}
		}
		this.tick++;
		if (this.tick > 3)
		{
			this.tick = 0;
			if (this.state != 0)
			{
				if (this.state == 1)
				{
					if (this.tempTick > 3)
					{
						this.tempTick = 1;
					}
					this.lineTxt.text = ContMgr.getOutGameCont("init" + this.tempTick, new string[]
					{
						(int)((float)this.cur / (float)this.max * 100f) + string.Empty
					});
					this.tempTick++;
				}
				else if (this.state == 2)
				{
					if (this.tempTick > 3)
					{
						this.tempTick = 1;
					}
					this.lineTxt.text = ContMgr.getOutGameCont("loading" + this.tempTick, new string[]
					{
						(int)((float)this.cur / (float)this.max * 100f) + string.Empty
					});
					Debug.Log("load::" + (int)(this.m_scroll_bar.fillAmount * 100f));
					this.tempTick++;
				}
			}
		}
	}

	public void loadCompleted(Action cb)
	{
		this.m_fOver = true;
		this.m_funcCallBack = cb;
	}

	public void onfail(string error)
	{
		this.m_scroll_bar.fillAmount = 0f;
		this.showComfirm(ContMgr.getOutGameCont("error", new string[0]), new Action(this.onClick));
	}
}
