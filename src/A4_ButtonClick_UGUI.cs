using LuaInterface;
using System;
using UnityEngine;
using UnityEngine.UI;

public class A4_ButtonClick_UGUI : MonoBehaviour
{
	public Button button;

	private string script = "                  \n        function doClick()\n            print('Button Click:>>>')\n        end\n\n        function TestClick(button)    \n            button.onClick:AddListener(doClick);\n        end\n    ";

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		luaScriptMgr.DoString(this.script);
		LuaFunction luaFunction = luaScriptMgr.GetLuaFunction("TestClick");
		luaFunction.Call(new object[]
		{
			this.button
		});
	}

	private void Update()
	{
	}
}
