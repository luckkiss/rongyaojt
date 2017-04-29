using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GameFramework
{
	public class JSONNode
	{
		public virtual JSONNode this[int aIndex]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public virtual JSONNode this[string aKey]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public virtual string Value
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		public virtual IEnumerable<JSONNode> Childs
		{
			get
			{
				yield break;
			}
		}

		public IEnumerable<JSONNode> DeepChilds
		{
			get
			{
				foreach (JSONNode jSONNode in this.Childs)
				{
					foreach (JSONNode jSONNode2 in jSONNode.DeepChilds)
					{
						yield return jSONNode2;
						jSONNode2 = null;
					}
					IEnumerator<JSONNode> enumerator2 = null;
					jSONNode = null;
				}
				IEnumerator<JSONNode> enumerator = null;
				yield break;
				yield break;
			}
		}

		public virtual int AsInt
		{
			get
			{
				int num = 0;
				bool flag = int.TryParse(this.Value, out num);
				int result;
				if (flag)
				{
					result = num;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		public virtual float AsFloat
		{
			get
			{
				float num = 0f;
				bool flag = float.TryParse(this.Value, out num);
				float result;
				if (flag)
				{
					result = num;
				}
				else
				{
					result = 0f;
				}
				return result;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		public virtual double AsDouble
		{
			get
			{
				double num = 0.0;
				bool flag = double.TryParse(this.Value, out num);
				double result;
				if (flag)
				{
					result = num;
				}
				else
				{
					result = 0.0;
				}
				return result;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		public virtual bool AsBool
		{
			get
			{
				bool flag = false;
				bool flag2 = bool.TryParse(this.Value, out flag);
				bool result;
				if (flag2)
				{
					result = flag;
				}
				else
				{
					result = !string.IsNullOrEmpty(this.Value);
				}
				return result;
			}
			set
			{
				this.Value = (value ? "true" : "false");
			}
		}

		public virtual JSONArray AsArray
		{
			get
			{
				return this as JSONArray;
			}
		}

		public virtual JSONClass AsObject
		{
			get
			{
				return this as JSONClass;
			}
		}

		public virtual void Add(string aKey, JSONNode aItem)
		{
		}

		public virtual void Add(JSONNode aItem)
		{
			this.Add("", aItem);
		}

		public virtual JSONNode Remove(string aKey)
		{
			return null;
		}

		public virtual JSONNode Remove(int aIndex)
		{
			return null;
		}

		public virtual JSONNode Remove(JSONNode aNode)
		{
			return aNode;
		}

		public override string ToString()
		{
			return "JSONNode";
		}

		public virtual string ToString(string aPrefix)
		{
			return "JSONNode";
		}

		public static implicit operator JSONNode(string s)
		{
			return new JSONData(s);
		}

		public static implicit operator string(JSONNode d)
		{
			return (d == null) ? null : d.Value;
		}

		public static bool operator ==(JSONNode a, object b)
		{
			bool flag = b == null && a is JSONLazyCreator;
			return flag || a == b;
		}

		public static bool operator !=(JSONNode a, object b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			return this == obj;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		internal static string Escape(string aText)
		{
			string text = "";
			int i = 0;
			while (i < aText.Length)
			{
				char c = aText[i];
				char c2 = c;
				switch (c2)
				{
				case '\b':
					text += "\\b";
					break;
				case '\t':
					text += "\\t";
					break;
				case '\n':
					text += "\\n";
					break;
				case '\v':
					goto IL_B2;
				case '\f':
					text += "\\f";
					break;
				case '\r':
					text += "\\r";
					break;
				default:
					if (c2 != '"')
					{
						if (c2 != '\\')
						{
							goto IL_B2;
						}
						text += "\\\\";
					}
					else
					{
						text += "\\\"";
					}
					break;
				}
				IL_C2:
				i++;
				continue;
				IL_B2:
				text += c.ToString();
				goto IL_C2;
			}
			return text;
		}

		public static JSONNode Parse(string aJSON)
		{
			Stack<JSONNode> stack = new Stack<JSONNode>();
			JSONNode jSONNode = null;
			int i = 0;
			string text = "";
			string text2 = "";
			bool flag = false;
			while (i < aJSON.Length)
			{
				char c = aJSON[i];
				if (c <= ',')
				{
					if (c <= ' ')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
						case '\r':
							goto IL_4B7;
						case '\v':
						case '\f':
							goto IL_49E;
						default:
							if (c != ' ')
							{
								goto IL_49E;
							}
							break;
						}
						bool flag2 = flag;
						if (flag2)
						{
							text += aJSON[i].ToString();
						}
					}
					else if (c != '"')
					{
						if (c != ',')
						{
							goto IL_49E;
						}
						bool flag3 = flag;
						if (flag3)
						{
							text += aJSON[i].ToString();
						}
						else
						{
							bool flag4 = text != "";
							if (flag4)
							{
								bool flag5 = jSONNode is JSONArray;
								if (flag5)
								{
									jSONNode.Add(text);
								}
								else
								{
									bool flag6 = text2 != "";
									if (flag6)
									{
										jSONNode.Add(text2, text);
									}
								}
							}
							text2 = "";
							text = "";
						}
					}
					else
					{
						flag = !flag;
					}
				}
				else
				{
					if (c <= ']')
					{
						if (c != ':')
						{
							switch (c)
							{
							case '[':
							{
								bool flag7 = flag;
								if (flag7)
								{
									text += aJSON[i].ToString();
									goto IL_4B7;
								}
								stack.Push(new JSONArray());
								bool flag8 = jSONNode != null;
								if (flag8)
								{
									text2 = text2.Trim();
									bool flag9 = jSONNode is JSONArray;
									if (flag9)
									{
										jSONNode.Add(stack.Peek());
									}
									else
									{
										bool flag10 = text2 != "";
										if (flag10)
										{
											jSONNode.Add(text2, stack.Peek());
										}
									}
								}
								text2 = "";
								text = "";
								jSONNode = stack.Peek();
								goto IL_4B7;
							}
							case '\\':
							{
								i++;
								bool flag11 = flag;
								if (flag11)
								{
									char c2 = aJSON[i];
									char c3 = c2;
									if (c3 <= 'f')
									{
										if (c3 != 'b')
										{
											if (c3 != 'f')
											{
												goto IL_48B;
											}
											text += "\f";
										}
										else
										{
											text += "\b";
										}
									}
									else if (c3 != 'n')
									{
										switch (c3)
										{
										case 'r':
											text += "\r";
											break;
										case 's':
											goto IL_48B;
										case 't':
											text += "\t";
											break;
										case 'u':
										{
											string s = aJSON.Substring(i + 1, 4);
											text += ((char)int.Parse(s, NumberStyles.AllowHexSpecifier)).ToString();
											i += 4;
											break;
										}
										default:
											goto IL_48B;
										}
									}
									else
									{
										text += "\n";
									}
									goto IL_49C;
									IL_48B:
									text += c2.ToString();
								}
								IL_49C:
								goto IL_4B7;
							}
							case ']':
								break;
							default:
								goto IL_49E;
							}
						}
						else
						{
							bool flag12 = flag;
							if (flag12)
							{
								text += aJSON[i].ToString();
								goto IL_4B7;
							}
							text2 = text;
							text = "";
							goto IL_4B7;
						}
					}
					else if (c != '{')
					{
						if (c != '}')
						{
							goto IL_49E;
						}
					}
					else
					{
						bool flag13 = flag;
						if (flag13)
						{
							text += aJSON[i].ToString();
							goto IL_4B7;
						}
						stack.Push(new JSONClass());
						bool flag14 = jSONNode != null;
						if (flag14)
						{
							text2 = text2.Trim();
							bool flag15 = jSONNode is JSONArray;
							if (flag15)
							{
								jSONNode.Add(stack.Peek());
							}
							else
							{
								bool flag16 = text2 != "";
								if (flag16)
								{
									jSONNode.Add(text2, stack.Peek());
								}
							}
						}
						text2 = "";
						text = "";
						jSONNode = stack.Peek();
						goto IL_4B7;
					}
					bool flag17 = flag;
					if (flag17)
					{
						text += aJSON[i].ToString();
					}
					else
					{
						bool flag18 = stack.Count == 0;
						if (flag18)
						{
							throw new Exception("JSON Parse: Too many closing brackets");
						}
						stack.Pop();
						bool flag19 = text != "";
						if (flag19)
						{
							text2 = text2.Trim();
							bool flag20 = jSONNode is JSONArray;
							if (flag20)
							{
								jSONNode.Add(text);
							}
							else
							{
								bool flag21 = text2 != "";
								if (flag21)
								{
									jSONNode.Add(text2, text);
								}
							}
						}
						text2 = "";
						text = "";
						bool flag22 = stack.Count > 0;
						if (flag22)
						{
							jSONNode = stack.Peek();
						}
					}
				}
				IL_4B7:
				i++;
				continue;
				IL_49E:
				text += aJSON[i].ToString();
				goto IL_4B7;
			}
			bool flag23 = flag;
			if (flag23)
			{
				throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
			}
			return jSONNode;
		}

		public virtual void Serialize(BinaryWriter aWriter)
		{
		}

		public void SaveToStream(Stream aData)
		{
			BinaryWriter aWriter = new BinaryWriter(aData);
			this.Serialize(aWriter);
		}

		public void SaveToCompressedStream(Stream aData)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public void SaveToCompressedFile(string aFileName)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public string SaveToCompressedBase64()
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public void SaveToFile(string aFileName)
		{
			Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
			using (FileStream fileStream = File.OpenWrite(aFileName))
			{
				this.SaveToStream(fileStream);
			}
		}

		public string SaveToBase64()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.SaveToStream(memoryStream);
				memoryStream.Position = 0L;
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		public static JSONNode Deserialize(BinaryReader aReader)
		{
			JSONBinaryTag jSONBinaryTag = (JSONBinaryTag)aReader.ReadByte();
			JSONNode result;
			switch (jSONBinaryTag)
			{
			case JSONBinaryTag.Array:
			{
				int num = aReader.ReadInt32();
				JSONArray jSONArray = new JSONArray();
				for (int i = 0; i < num; i++)
				{
					jSONArray.Add(JSONNode.Deserialize(aReader));
				}
				result = jSONArray;
				break;
			}
			case JSONBinaryTag.Class:
			{
				int num2 = aReader.ReadInt32();
				JSONClass jSONClass = new JSONClass();
				for (int j = 0; j < num2; j++)
				{
					string aKey = aReader.ReadString();
					JSONNode aItem = JSONNode.Deserialize(aReader);
					jSONClass.Add(aKey, aItem);
				}
				result = jSONClass;
				break;
			}
			case JSONBinaryTag.Value:
				result = new JSONData(aReader.ReadString());
				break;
			case JSONBinaryTag.IntValue:
				result = new JSONData(aReader.ReadInt32());
				break;
			case JSONBinaryTag.DoubleValue:
				result = new JSONData(aReader.ReadDouble());
				break;
			case JSONBinaryTag.BoolValue:
				result = new JSONData(aReader.ReadBoolean());
				break;
			case JSONBinaryTag.FloatValue:
				result = new JSONData(aReader.ReadSingle());
				break;
			default:
				throw new Exception("Error deserializing JSON. Unknown tag: " + jSONBinaryTag);
			}
			return result;
		}

		public static JSONNode LoadFromCompressedFile(string aFileName)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public static JSONNode LoadFromCompressedStream(Stream aData)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public static JSONNode LoadFromCompressedBase64(string aBase64)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		public static JSONNode LoadFromStream(Stream aData)
		{
			JSONNode result;
			using (BinaryReader binaryReader = new BinaryReader(aData))
			{
				result = JSONNode.Deserialize(binaryReader);
			}
			return result;
		}

		public static JSONNode LoadFromFile(string aFileName)
		{
			JSONNode result;
			using (FileStream fileStream = File.OpenRead(aFileName))
			{
				result = JSONNode.LoadFromStream(fileStream);
			}
			return result;
		}

		public static JSONNode LoadFromBase64(string aBase64)
		{
			byte[] buffer = Convert.FromBase64String(aBase64);
			return JSONNode.LoadFromStream(new MemoryStream(buffer)
			{
				Position = 0L
			});
		}
	}
}
