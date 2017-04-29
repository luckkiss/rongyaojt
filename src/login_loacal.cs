using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class login_loacal : MonoBehaviour
{
	private InputField uid;

	private InputField tkn;

	private Button bt;

	private Button btn_serverSelect;

	private ToggleGroup tg;

	private Text textServer;

	private GameObject serverInfo;

	private Transform servers;

	private Button zhanghao;

	private List<serverData> serverList;

	private void Start()
	{
		this.uid = base.transform.FindChild("uid").GetComponent<InputField>();
		this.tkn = base.transform.FindChild("tkn").GetComponent<InputField>();
		this.bt = base.transform.FindChild("bt").GetComponent<Button>();
		this.serverInfo = base.transform.FindChild("serverPanel").gameObject;
		this.servers = base.transform.FindChild("serverPanel/servers");
		this.btn_serverSelect = base.transform.FindChild("serverInfo/btn_selectServer").GetComponent<Button>();
		this.textServer = base.transform.FindChild("serverInfo/txt_server").GetComponent<Text>();
		this.tg = base.transform.FindChild("s").GetComponent<ToggleGroup>();
		this.bt.onClick.AddListener(new UnityAction(this.onCLick));
		this.btn_serverSelect.onClick.AddListener(new UnityAction(this.onServerSlectClick));
		this.zhanghao = base.transform.FindChild("idbtn").GetComponent<Button>();
		this.zhanghao.onClick.AddListener(new UnityAction(this.onZhanghao));
		this.serverList = new List<serverData>();
		serverData serverData = new serverData();
		serverData.ip = "10.1.8.76";
		serverData.port = 64999u;
		serverData.sid = 1u;
		serverData.clnt = 0u;
		this.serverList.Add(serverData);
		serverData = new serverData();
		serverData.ip = "10.1.8.76";
		serverData.port = 63999u;
		serverData.sid = 2u;
		serverData.clnt = 0u;
		this.serverList.Add(serverData);
		serverData = new serverData();
		serverData.ip = "10.1.8.76";
		serverData.port = 62999u;
		serverData.sid = 3u;
		serverData.clnt = 0u;
		this.serverList.Add(serverData);
		serverData = new serverData();
		serverData.ip = "10.1.8.76";
		serverData.port = 61999u;
		serverData.sid = 4u;
		serverData.clnt = 0u;
		this.serverList.Add(serverData);
		serverData = new serverData();
		serverData.ip = "120.132.13.141";
		serverData.port = 63999u;
		serverData.sid = 1u;
		serverData.clnt = 0u;
		this.serverList.Add(serverData);
		serverData = new serverData();
		serverData.ip = "10.1.6.181";
		serverData.port = 54999u;
		serverData.sid = 1u;
		serverData.clnt = 0u;
		this.serverList.Add(serverData);
		serverData = new serverData();
		serverData.ip = "a3.test.utogame.com";
		serverData.port = 65019u;
		serverData.sid = 1u;
		serverData.clnt = 0u;
		this.serverList.Add(serverData);
		serverData = new serverData();
		serverData.ip = "10.1.6.200";
		serverData.port = 64999u;
		serverData.sid = 1u;
		serverData.clnt = 0u;
		this.serverList.Add(serverData);
		serverData = new serverData();
		serverData.ip = "10.1.8.7";
		serverData.port = 64999u;
		serverData.sid = 1u;
		serverData.clnt = 0u;
		this.serverList.Add(serverData);
		serverData = new serverData();
		serverData.ip = "10.1.6.29";
		serverData.port = 64999u;
		serverData.sid = 1u;
		serverData.clnt = 0u;
		this.serverList.Add(serverData);
		this.initInfo();
		this.setToggleEvent();
	}

	private void initInfo()
	{
		if (PlayeLocalInfo.checkKey(PlayeLocalInfo.DEBUG_TKN))
		{
			this.tkn.text = PlayeLocalInfo.loadString(PlayeLocalInfo.DEBUG_TKN);
		}
		if (PlayeLocalInfo.checkKey(PlayeLocalInfo.DEBUG_UID))
		{
			this.uid.text = PlayeLocalInfo.loadString(PlayeLocalInfo.DEBUG_UID);
		}
		if (this.tkn.text == string.Empty && this.uid.text == string.Empty)
		{
			this.tkn.gameObject.SetActive(true);
			this.uid.gameObject.SetActive(true);
		}
		else if (this.tkn.text != string.Empty && this.uid.text != string.Empty)
		{
			this.tkn.gameObject.SetActive(false);
			this.uid.gameObject.SetActive(false);
		}
		if (PlayeLocalInfo.checkKey(PlayeLocalInfo.DEBUG_SELECTED))
		{
			Toggle component = base.transform.FindChild("serverPanel/servers/s" + PlayeLocalInfo.loadInt(PlayeLocalInfo.DEBUG_SELECTED)).GetComponent<Toggle>();
			component.isOn = true;
			this.textServer.text = component.transform.FindChild("Label").GetComponent<Text>().text;
		}
		else
		{
			Toggle component2 = base.transform.FindChild("serverPanel/servers/s7").GetComponent<Toggle>();
			component2.isOn = true;
			this.textServer.text = component2.transform.FindChild("Label").GetComponent<Text>().text;
		}
	}

	private void setToggleEvent()
	{
		int childCount = this.servers.childCount;
		for (int i = 0; i < childCount; i++)
		{
			this.servers.transform.GetChild(i).gameObject.GetComponent<Toggle>().onValueChanged.AddListener(new UnityAction<bool>(this.onToggleClick));
		}
	}

	public void onToggleClick(bool isON)
	{
		foreach (Toggle current in this.tg.ActiveToggles())
		{
			if (current.isOn)
			{
				this.textServer.text = current.transform.FindChild("Label").GetComponent<Text>().text;
				if (this.serverInfo.activeSelf)
				{
					this.serverInfo.SetActive(false);
				}
				break;
			}
		}
	}

	private void onCLick()
	{
		PlayeLocalInfo.saveString(PlayeLocalInfo.DEBUG_TKN, this.tkn.text);
		PlayeLocalInfo.saveString(PlayeLocalInfo.DEBUG_UID, this.uid.text);
		int index = 0;
		for (int i = 0; i < this.serverList.Count; i++)
		{
			Toggle component = base.transform.FindChild("serverPanel/servers/s" + (i + 1)).GetComponent<Toggle>();
			if (component.isOn)
			{
				index = i;
				PlayeLocalInfo.saveInt(PlayeLocalInfo.DEBUG_SELECTED, i + 1);
				break;
			}
		}
		Main.instance.initParam(uint.Parse(this.uid.text), this.tkn.text, this.serverList[index]);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void onZhanghao()
	{
		if (!this.tkn.gameObject.activeSelf || !this.uid.gameObject.activeSelf)
		{
			this.tkn.gameObject.SetActive(true);
			this.uid.gameObject.SetActive(true);
		}
	}

	private void onServerSlectClick()
	{
		if (!this.serverInfo.activeSelf)
		{
			this.serverInfo.SetActive(true);
		}
	}

	private void Update()
	{
	}
}
