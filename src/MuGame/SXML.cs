using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class SXML
	{
		public static List<string> lStr;

		public bool hasFound;

		public ByteArray m_root = new ByteArray();

		private string m_strNodeName = "";

		public Dictionary<string, SXMLAttr> m_dAtttr = new Dictionary<string, SXMLAttr>();

		public Dictionary<string, List<SXML>> m_dNode = new Dictionary<string, List<SXML>>();

		public void checkInited()
		{
			bool flag = this.m_strNodeName != "";
			if (!flag)
			{
				this.m_root.position = 0;
				int index = this.m_root.readInt();
				this.m_strNodeName = SXML.lStr[index];
				int num = 0;
				while (this.m_root.bytesAvailable > 4)
				{
					int num2 = this.m_root.readInt();
					bool flag2 = num2 == -1;
					if (flag2)
					{
						num++;
					}
					else
					{
						bool flag3 = num == 0;
						if (flag3)
						{
							this.m_dAtttr[SXML.lStr[num2]] = this.getAttr(this.m_root);
						}
						else
						{
							bool flag4 = num == 1;
							if (flag4)
							{
								SXML sXML = new SXML();
								string key = SXML.lStr[num2];
								bool flag5 = !this.m_dNode.ContainsKey(key);
								if (flag5)
								{
									this.m_dNode[key] = new List<SXML>();
								}
								this.m_dNode[key].Add(sXML);
								int len = this.m_root.readInt();
								this.m_root.readBytes(sXML.m_root, this.m_root.position, len);
							}
						}
					}
				}
			}
		}

		private SXMLAttr getAttr(ByteArray ba)
		{
			SXMLAttr sXMLAttr = new SXMLAttr();
			int num = (int)ba.readByte();
			bool flag = num == 1;
			if (flag)
			{
				sXMLAttr.intvalue = (int)ba.readByte();
				sXMLAttr.uintvalue = (uint)sXMLAttr.intvalue;
				sXMLAttr.floatvalue = (float)sXMLAttr.intvalue;
				sXMLAttr.str = sXMLAttr.intvalue.ToString();
			}
			else
			{
				bool flag2 = num == 2;
				if (flag2)
				{
					sXMLAttr.intvalue = (int)ba.readShort();
					sXMLAttr.uintvalue = (uint)sXMLAttr.intvalue;
					sXMLAttr.floatvalue = (float)sXMLAttr.intvalue;
					sXMLAttr.str = sXMLAttr.intvalue.ToString();
				}
				else
				{
					bool flag3 = num == 3;
					if (flag3)
					{
						sXMLAttr.intvalue = ba.readInt();
						sXMLAttr.str = sXMLAttr.intvalue.ToString();
						sXMLAttr.uintvalue = (uint)sXMLAttr.intvalue;
						sXMLAttr.floatvalue = (float)sXMLAttr.intvalue;
					}
					else
					{
						bool flag4 = num == 4;
						if (flag4)
						{
							sXMLAttr.str = SXML.lStr[ba.readInt()];
						}
						else
						{
							bool flag5 = num == 5;
							if (flag5)
							{
								sXMLAttr.uintvalue = ba.readUnsignedInt();
								sXMLAttr.intvalue = 0;
								sXMLAttr.floatvalue = 0f;
								sXMLAttr.str = sXMLAttr.uintvalue.ToString();
							}
							else
							{
								bool flag6 = num == 6;
								if (flag6)
								{
									sXMLAttr.floatvalue = ba.readFloat();
									sXMLAttr.uintvalue = 0u;
									sXMLAttr.intvalue = 0;
									sXMLAttr.str = sXMLAttr.floatvalue.ToString();
								}
							}
						}
					}
				}
			}
			return sXMLAttr;
		}

		public void forEach(Action<SXML> handle)
		{
			this.checkInited();
			foreach (List<SXML> current in this.m_dNode.Values)
			{
				foreach (SXML current2 in current)
				{
					handle(current2);
				}
			}
		}

		public static List<SXML> Filer(List<SXML> list, string filter)
		{
			bool flag = filter == "" || filter == null;
			List<SXML> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				int num = 0;
				string text = "";
				string text2 = "";
				int num2 = 0;
				List<SXML> list2 = new List<SXML>();
				int num3 = 0;
				char[] array = filter.ToCharArray();
				for (int i = 0; i < array.Length; i++)
				{
					char c = array[i];
					char c2 = c;
					if (c2 != '!')
					{
						switch (c2)
						{
						case '<':
						{
							num3 = 1;
							bool flag2 = '=' == array[i + 1];
							if (flag2)
							{
								i++;
								num = 6;
							}
							else
							{
								num = 4;
							}
							break;
						}
						case '=':
							num3 = 1;
							i++;
							num = 1;
							break;
						case '>':
						{
							num3 = 1;
							bool flag3 = '=' == array[i + 1];
							if (flag3)
							{
								i++;
								num = 5;
							}
							else
							{
								num = 3;
							}
							break;
						}
						default:
						{
							bool flag4 = 1 == num3;
							if (flag4)
							{
								text2 += c.ToString();
							}
							else
							{
								text += c.ToString();
							}
							break;
						}
						}
					}
					else
					{
						num3 = 1;
						i++;
						num = 2;
					}
				}
				bool flag5 = num >= 3 && num <= 6;
				if (flag5)
				{
					num2 = int.Parse(text2);
				}
				foreach (SXML current in list)
				{
					switch (num)
					{
					case 1:
					{
						bool flag6 = current.getString(text).Equals(text2);
						if (flag6)
						{
							list2.Add(current);
						}
						break;
					}
					case 2:
					{
						bool flag7 = !current.getString(text).Equals(text2);
						if (flag7)
						{
							list2.Add(current);
						}
						break;
					}
					case 3:
					{
						int @int = current.getInt(text);
						bool flag8 = @int > num2;
						if (flag8)
						{
							list2.Add(current);
						}
						break;
					}
					case 4:
					{
						int @int = current.getInt(text);
						bool flag9 = @int < num2;
						if (flag9)
						{
							list2.Add(current);
						}
						break;
					}
					case 5:
					{
						int @int = current.getInt(text);
						bool flag10 = @int >= num2;
						if (flag10)
						{
							list2.Add(current);
						}
						break;
					}
					case 6:
					{
						int @int = current.getInt(text);
						bool flag11 = @int <= num2;
						if (flag11)
						{
							list2.Add(current);
						}
						break;
					}
					}
				}
				result = list2;
			}
			return result;
		}

		public bool nextOne()
		{
			return false;
		}

		public SXML GetNode(string command, string filter = "")
		{
			List<SXML> nodeList = this.GetNodeList(command, filter);
			bool flag = nodeList == null;
			SXML result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = nodeList.Count > 0;
				if (flag2)
				{
					result = nodeList[0];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public List<SXML> GetNodeList(string command, string filter = "")
		{
			this.checkInited();
			string[] array = command.Split(new char[]
			{
				'.'
			});
			bool flag = array.Length < 1;
			List<SXML> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SXML sXML = this;
				List<SXML> list = null;
				for (int i = 0; i < array.Length; i++)
				{
					bool flag2 = sXML.m_dNode.ContainsKey(array[i]);
					if (!flag2)
					{
						result = null;
						return result;
					}
					list = sXML.m_dNode[array[i]];
					sXML = list[0];
					sXML.checkInited();
				}
				result = SXML.Filer(list, filter);
			}
			return result;
		}

		public string getString(string key)
		{
			this.checkInited();
			bool flag = this.m_dAtttr.ContainsKey(key);
			string result;
			if (flag)
			{
				result = this.m_dAtttr[key].str;
			}
			else
			{
				result = "null";
			}
			return result;
		}

		public float getFloat(string key)
		{
			this.checkInited();
			bool flag = this.m_dAtttr.ContainsKey(key);
			float result;
			if (flag)
			{
				result = this.m_dAtttr[key].floatvalue;
			}
			else
			{
				result = -1f;
			}
			return result;
		}

		public int getInt(string key)
		{
			this.checkInited();
			bool flag = this.m_dAtttr.ContainsKey(key);
			int result;
			if (flag)
			{
				result = this.m_dAtttr[key].intvalue;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		public uint getUint(string key)
		{
			this.checkInited();
			bool flag = this.m_dAtttr.ContainsKey(key);
			uint result;
			if (flag)
			{
				result = this.m_dAtttr[key].uintvalue;
			}
			else
			{
				result = 0u;
			}
			return result;
		}

		public bool hasValue(string key)
		{
			return this.m_dAtttr.ContainsKey(key);
		}
	}
}
