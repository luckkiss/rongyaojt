using Cross;
using GameFramework;
using MuGame;
using System;
using UnityEngine;

public class demo_main : MonoBehaviour
{
	private static osImpl m_os;

	private void Start()
	{
		Debug.Log("开始Demo");
		Debug.Log("开始....加载xml数据...............");
		new URLReqImpl
		{
			dataFormat = "binary",
			url = "staticdata/staticdata.dat"
		}.load(delegate(IURLReq url_req, object ret)
		{
			Debug.Log("demo 加载数据xml...............");
			byte[] d = ret as byte[];
			ByteArray byteArray = new ByteArray(d);
			byteArray.uncompress();
			XMLMgr.instance.init(byteArray);
		}, null, delegate(IURLReq url_req, string err)
		{
			Debug.Log("加载staticdata 失败。。。。。。。。。。。。" + url_req.url);
		});
		Globle.Init_DEFAULT();
		Globle.A3_DEMO = true;
		Application.targetFrameRate = 60;
		new CrossApp(true);
		CrossApp.singleton.regPlugin(new gameEventDelegate());
		CrossApp.singleton.regPlugin(new processManager());
		Screen.SetResolution(Screen.width, Screen.height, true);
		SceneCamera.Init();
		SelfRole.Init();
		Variant variant = new Variant();
		variant["id"] = 1;
		GRMap.changeMapTimeSt = 7;
		GRMap.loading = false;
		GameRoomMgr.getInstance().onChangeLevel(variant, null);
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		SceneCamera.FrameMove();
		MonsterMgr._inst.FrameMove(deltaTime);
		SelfRole.FrameMove(deltaTime);
		TickMgr.instance.update(deltaTime);
		(CrossApp.singleton.getPlugin("gameEventDelegate") as gameEventDelegate).onProcess(deltaTime);
	}

	public void loadXml()
	{
		new URLReqImpl
		{
			dataFormat = "binary",
			url = "staticdata/staticdata.dat"
		}.load(delegate(IURLReq url_req, object ret)
		{
			byte[] d = ret as byte[];
			ByteArray byteArray = new ByteArray(d);
			byteArray.uncompress();
			while (byteArray.bytesAvailable > 4)
			{
				int len = byteArray.readInt();
				string text = byteArray.readUTF8Bytes(len);
				int num = byteArray.readInt();
				string text2 = byteArray.readUTF8Bytes(num);
				Debug.Log(string.Concat(new object[]
				{
					"处理表 ",
					text,
					" 大小=",
					num
				}));
				XMLMgr.instance.AddXmlData(text, ref text2);
				text2 = null;
			}
		}, null, delegate(IURLReq url_req, string err)
		{
			Debug.Log("加载staticdata 失败。。。。。。。。。。。。" + url_req.url);
		});
	}
}
