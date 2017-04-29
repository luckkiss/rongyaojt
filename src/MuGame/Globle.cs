using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class Globle
	{
		public static bool A3_DEMO = false;

		public static bool isHardDemo = false;

		public static bool inGame = false;

		public static string Lan = "";

		public static gameST game_CrossMono;

		public static string DATA_PATH;

		public static int DebugMode = 2;

		public static ENUM_QSMY_PLATFORM QSMY_Platform_Index = ENUM_QSMY_PLATFORM.QSPF_None;

		public static ENUM_SDK_PLATFORM QSMY_SDK_Index = ENUM_SDK_PLATFORM.QISJ_QUICK;

		public static int QSMY_CLIENT_VER = 2;

		public static int QSMY_CLIENT_LOW_VER = 0;

		public static bool UNCLOSE_BEGINLOADING = true;

		public static string YR_srvlists__platform = "nil";

		public static string YR_srvlists__platuid = "1";

		public static string YR_srvlists__sign = "nil";

		public static string YR_srvlists__slurl = "nil";

		public static string QSMY_game_ver = "nil";

		public static string WebUrl = "http://cdn.qsmy.hulai.com/qsmy/";

		public static int VER;

		public static int m_nTestMonsterID;

		public static List<ServerData> lServer;

		public static Dictionary<int, ServerData> dServer;

		public static ServerData curServerD;

		public static int defServerId = 0;

		private static int lastUnFocusTimer = 0;

		public static Color COLOR_YELLOW = new Color(1f, 1f, 0.01f);

		public static Color COLOR_WHITE = new Color(1f, 1f, 1f);

		public static Color COLOR_GREEN = new Color(0f, 1f, 0f);

		public static Color COLOR_BLUE = new Color(0.4f, 1f, 1f);

		public static Color COLOR_PURPLE = new Color(1f, 0f, 1f);

		public static Color COLOR_GOLD = new Color(1f, 0.5f, 0.04f);

		public static Color COLOR_RED = new Color(0.97f, 0.05f, 0.05f);

		public static Dictionary<string, int> AttNameIDDic = new Dictionary<string, int>
		{
			{
				"strength",
				1
			},
			{
				"agiligty",
				2
			},
			{
				"constituion",
				3
			},
			{
				"intelligence",
				4
			},
			{
				"wisdom",
				34
			},
			{
				"max_attack",
				5
			},
			{
				"physics_def",
				6
			},
			{
				"magic_def",
				7
			},
			{
				"fire_att",
				8
			},
			{
				"ice_att",
				9
			},
			{
				"light_att",
				10
			},
			{
				"fire_def",
				11
			},
			{
				"ice_def",
				12
			},
			{
				"light_def",
				13
			},
			{
				"max_hp",
				14
			},
			{
				"max_mp",
				15
			},
			{
				"crime",
				16
			},
			{
				"mp_abate",
				17
			},
			{
				"hp_suck",
				18
			},
			{
				"physics_dmg_red",
				19
			},
			{
				"magic_dm_red",
				20
			},
			{
				"skill_damage",
				21
			},
			{
				"fatal_att",
				22
			},
			{
				"fatal_dodge",
				23
			},
			{
				"max_hp_add",
				24
			},
			{
				"max_mp_add",
				25
			},
			{
				"hp_recovery",
				26
			},
			{
				"mp_recovery",
				27
			},
			{
				"mp_suck",
				28
			},
			{
				"magic_shield",
				29
			},
			{
				"exp_add",
				30
			},
			{
				"blessing",
				31
			},
			{
				"knowledge_add",
				32
			},
			{
				"fatal_damage",
				33
			},
			{
				"fire_def_add",
				35
			},
			{
				"ice_def_add",
				36
			},
			{
				"light_def_add",
				37
			},
			{
				"min_attack",
				38
			},
			{
				"double_damage_rate",
				39
			},
			{
				"reflect_damage_rate",
				40
			},
			{
				"ignore_crit_rate",
				41
			},
			{
				"crit_add_hp",
				42
			},
			{
				"hit",
				43
			},
			{
				"dodge",
				44
			}
		};

		public static void Init_DEFAULT()
		{
			U3DAPI.Init_DEFAULT();
		}

		public static void initServer(List<Variant> d)
		{
			Globle.lServer = new List<ServerData>();
			Globle.dServer = new Dictionary<int, ServerData>();
			foreach (Variant current in d)
			{
				ServerData serverData = new ServerData();
				serverData.sid = current["sid"];
				serverData.server_name = StringUtils.unicodeToStr(current["server_name"]);
				serverData.close = (current["close"]._int == 1);
				serverData.combine_sid = current["combine_sid"];
				serverData.def = (current["def"]._int == 1);
				serverData.recomm = (current["recomm"]._int == 1);
				serverData.srvnew = (current["srvnew"]._int == 1);
				serverData.login_url = current["login_url"];
				serverData.do_url = current["do_url"];
				Globle.lServer.Add(serverData);
				Globle.dServer[serverData.sid] = serverData;
				bool def = serverData.def;
				if (def)
				{
					Globle.defServerId = serverData.sid;
				}
			}
			bool flag = Globle.defServerId == 0 && Globle.lServer.Count > 0;
			if (flag)
			{
				Globle.defServerId = Globle.lServer[Globle.lServer.Count - 1].sid;
			}
		}

		public static void setDebugServerD(uint sid, string login_url)
		{
			bool flag = Globle.DebugMode != 2;
			if (!flag)
			{
				Globle.curServerD = new ServerData
				{
					sid = (int)sid,
					login_url = login_url,
					do_url = login_url
				};
			}
		}

		public static void OnApplicationFocus(bool isFocus)
		{
			bool flag = !Globle.inGame;
			if (!flag)
			{
				bool flag2 = Globle.DebugMode == 2;
				if (!flag2)
				{
					debug.Log("连接状态：：" + NetClient.instance.isConnect.ToString());
					bool flag3 = !NetClient.instance.isConnect;
					if (flag3)
					{
						InterfaceMgr.getInstance().open(InterfaceMgr.DISCONECT, null, false);
					}
				}
			}
		}

		public static string getStrJob(int id)
		{
			return ContMgr.getCont("comm_job" + id, null);
		}

		public static Color getColorByQuality(int quality)
		{
			bool flag = quality == 1;
			Color result;
			if (flag)
			{
				result = Globle.COLOR_WHITE;
			}
			else
			{
				bool flag2 = quality == 2;
				if (flag2)
				{
					result = Globle.COLOR_GREEN;
				}
				else
				{
					bool flag3 = quality == 3;
					if (flag3)
					{
						result = Globle.COLOR_BLUE;
					}
					else
					{
						bool flag4 = quality == 4;
						if (flag4)
						{
							result = Globle.COLOR_PURPLE;
						}
						else
						{
							bool flag5 = quality == 5;
							if (flag5)
							{
								result = Globle.COLOR_GOLD;
							}
							else
							{
								bool flag6 = quality == 6;
								if (flag6)
								{
									result = Globle.COLOR_RED;
								}
								else
								{
									bool flag7 = quality == 7;
									if (flag7)
									{
										result = Globle.COLOR_RED;
									}
									else
									{
										result = Globle.COLOR_WHITE;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		public static string getColorStrByQuality(string name, int quality)
		{
			bool flag = quality == 1;
			string result;
			if (flag)
			{
				result = "<color=#ffffff>" + name + "</color>";
			}
			else
			{
				bool flag2 = quality == 2;
				if (flag2)
				{
					result = "<color=#00FF00>" + name + "</color>";
				}
				else
				{
					bool flag3 = quality == 3;
					if (flag3)
					{
						result = "<color=#66FFFF>" + name + "</color>";
					}
					else
					{
						bool flag4 = quality == 4;
						if (flag4)
						{
							result = "<color=#FF00FF>" + name + "</color>";
						}
						else
						{
							bool flag5 = quality == 5;
							if (flag5)
							{
								result = "<color=#f7790a>" + name + "</color>";
							}
							else
							{
								bool flag6 = quality == 6;
								if (flag6)
								{
									result = "<color=#f90e0e>" + name + "</color>";
								}
								else
								{
									bool flag7 = quality == 7;
									if (flag7)
									{
										result = "<color=#f90e0e>" + name + "</color>";
									}
									else
									{
										result = "<color=#ffffff>" + name + "</color>";
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		public static string formatTime(int second, bool showhour = true)
		{
			int num = second / 3600;
			second %= 3600;
			int num2 = second / 60;
			second %= 60;
			string text = num2.ToString();
			string text2 = second.ToString();
			string text3 = num.ToString();
			bool flag = text.Length == 1;
			if (flag)
			{
				text = "0" + text;
			}
			bool flag2 = text2.Length == 1;
			if (flag2)
			{
				text2 = "0" + text2;
			}
			string str = "";
			bool flag3 = num > 0;
			if (flag3)
			{
				bool flag4 = showhour && text3.Length == 1;
				if (flag4)
				{
					str = "0" + num + ":";
				}
				else
				{
					str = num + ":";
				}
			}
			else if (showhour)
			{
				str = "00:";
			}
			return str + text + ":" + text2;
		}

		public static string getEquipTextByType(int type_id)
		{
			return ContMgr.getCont("globle_equip" + type_id, null);
		}

		public static string getValueTextById(int id)
		{
			return ContMgr.getCont("globle_value" + id, null);
		}

		public static string getAttrAddById(int id, int value, bool add = true)
		{
			string text = Globle.getAttrNameById(id);
			bool flag = id == 19 || id == 20 || id == 17;
			if (flag)
			{
				add = false;
			}
			bool flag2 = id == 16;
			if (flag2)
			{
				text = text + ":" + value;
			}
			else
			{
				bool flag3 = id == 17 || id == 19 || id == 20 || id == 24 || id == 25 || id == 29 || id == 30 || id == 31 || id == 32 || id == 33 || id == 35 || id == 36 || id == 37 || id == 39 || id == 40 || id == 17 || id == 41;
				if (flag3)
				{
					bool flag4 = add;
					if (flag4)
					{
						text = string.Concat(new object[]
						{
							text,
							"+",
							(float)value / 10f,
							"%"
						});
					}
					else
					{
						text = string.Concat(new object[]
						{
							text,
							"-",
							(float)value / 10f,
							"%"
						});
					}
				}
				else
				{
					bool flag5 = add;
					if (flag5)
					{
						text = text + "+" + value;
					}
					else
					{
						text = text + "-" + value;
					}
				}
			}
			return text;
		}

		public static string getAttrAddById_value(int id, int value, bool add = true)
		{
			string text = "";
			bool flag = id == 19 || id == 20 || id == 17;
			if (flag)
			{
				add = false;
			}
			bool flag2 = id == 16;
			if (flag2)
			{
				text = text + ":" + value;
			}
			else
			{
				bool flag3 = id == 17 || id == 19 || id == 20 || id == 24 || id == 25 || id == 29 || id == 30 || id == 31 || id == 32 || id == 33 || id == 35 || id == 36 || id == 37 || id == 39 || id == 40 || id == 17 || id == 41;
				if (flag3)
				{
					bool flag4 = add;
					if (flag4)
					{
						text = string.Concat(new object[]
						{
							text,
							"+",
							(float)value / 10f,
							"%"
						});
					}
					else
					{
						text = string.Concat(new object[]
						{
							text,
							"-",
							(float)value / 10f,
							"%"
						});
					}
				}
				else
				{
					bool flag5 = add;
					if (flag5)
					{
						text = text + "+" + value;
					}
					else
					{
						text = text + "-" + value;
					}
				}
			}
			return text;
		}

		public static string getAttrNameById(int id)
		{
			return ContMgr.getCont("globle_attr" + id, null);
		}

		public static void setTimeScale(float scale)
		{
			Time.timeScale = scale;
			Time.fixedDeltaTime = scale * Time.timeScale;
		}

		public static string getBigText(uint num)
		{
			int num2 = (int)Math.Floor(num / 10000.0);
			bool flag = num2 > 0;
			string result;
			if (flag)
			{
				result = num2 + ContMgr.getCont("globle_money", null);
			}
			else
			{
				result = num.ToString();
			}
			return result;
		}

		public static string getStrTime(int _time = 0, bool showYear = false, bool showmine = true)
		{
			bool flag = _time == 0;
			if (flag)
			{
				_time = muNetCleint.instance.CurServerTimeStamp;
			}
			int num = _time;
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long ticks = long.Parse(num + "0000000");
			TimeSpan value = new TimeSpan(ticks);
			DateTime dateTime2 = dateTime.Add(value);
			string text = dateTime2.ToShortDateString().ToString();
			string text2 = dateTime2.ToLongTimeString().ToString();
			string[] array = text.Split(new char[]
			{
				'/'
			});
			string[] array2 = text2.Split(new char[]
			{
				':'
			});
			string[] array3 = text2.Split(new char[]
			{
				' '
			});
			string result = "";
			bool flag2 = array2.Length == 3;
			if (flag2)
			{
				if (showYear)
				{
					result = ContMgr.getCont("comm_date1", new List<string>
					{
						array[2],
						array[0],
						array[1],
						array2[0],
						array2[1]
					});
				}
				else
				{
					result = ContMgr.getCont("comm_date2", new List<string>
					{
						array[0],
						array[1],
						array2[0],
						array2[1]
					});
				}
				bool flag3 = !showmine;
				if (flag3)
				{
					result = ContMgr.getCont("comm_date3", new List<string>
					{
						array[0],
						array[1],
						array2[0]
					});
				}
			}
			else
			{
				bool flag4 = array2.Length == 4;
				if (flag4)
				{
					int num2 = (array3[1] == "PM") ? 12 : 0;
					if (showYear)
					{
						result = ContMgr.getCont("comm_date1", new List<string>
						{
							array[2],
							array[0],
							array[1],
							(int.Parse(array2[0]) + num2).ToString(),
							array2[1]
						});
					}
					else
					{
						result = ContMgr.getCont("comm_date2", new List<string>
						{
							array[0],
							array[1],
							(int.Parse(array2[0]) + num2).ToString(),
							array2[1]
						});
					}
					bool flag5 = !showmine;
					if (flag5)
					{
						result = ContMgr.getCont("comm_date3", new List<string>
						{
							array[0],
							array[1],
							(int.Parse(array2[0]) + num2).ToString()
						});
					}
				}
			}
			return result;
		}

		public static float getParticleSystemLength(Transform transform)
		{
			ParticleSystem[] componentsInChildren = transform.GetComponentsInChildren<ParticleSystem>();
			float num = 0f;
			ParticleSystem[] array = componentsInChildren;
			float result;
			for (int i = 0; i < array.Length; i++)
			{
				ParticleSystem particleSystem = array[i];
				bool enableEmission = particleSystem.enableEmission;
				if (enableEmission)
				{
					bool loop = particleSystem.loop;
					if (loop)
					{
						result = -1f;
						return result;
					}
					bool flag = particleSystem.emissionRate <= 0f;
					float num2;
					if (flag)
					{
						num2 = particleSystem.startDelay + particleSystem.startLifetime;
					}
					else
					{
						num2 = particleSystem.startDelay + Mathf.Max(particleSystem.duration, particleSystem.startLifetime);
					}
					bool flag2 = num2 > num;
					if (flag2)
					{
						num = num2;
					}
				}
			}
			Animator[] componentsInChildren2 = transform.GetComponentsInChildren<Animator>();
			float num3 = 0f;
			Animator[] array2 = componentsInChildren2;
			for (int j = 0; j < array2.Length; j++)
			{
				Animator animator = array2[j];
				float length = animator.GetCurrentAnimatorStateInfo(0).length;
				bool flag3 = length > num3;
				if (flag3)
				{
					num3 = length;
				}
			}
			result = num;
			return result;
		}

		public static void err_output(int res)
		{
			bool flag = res == -5012;
			if (!flag)
			{
				string text = err_string.get_Err_String(res);
				bool flag2 = flytxt.instance != null;
				if (flag2)
				{
					flytxt.instance.fly(text, 0, default(Color), null);
				}
				else
				{
					Debug.LogError(text);
				}
			}
		}
	}
}
