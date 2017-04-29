using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using UnityEngine;

namespace Cross
{
	public class SysInterfaceImpl : ISysInterface
	{
		protected Dictionary<Define.EventType, Action<Event>> m_eventFunc = new Dictionary<Define.EventType, Action<Event>>();

		protected float m_fps;

		protected ILocalStorage m_localStorage;

		private float mFrameCount = 0f;

		private float mLastFrameTime = 0f;

		public ILocalStorage loscalStorage
		{
			get
			{
				return this.m_localStorage;
			}
		}

		public int windowWidth
		{
			get
			{
				return osImpl.singleton.screenWidth;
			}
		}

		public int windowHeight
		{
			get
			{
				return osImpl.singleton.screenHeight;
			}
		}

		public float fps
		{
			get
			{
				return this.m_fps;
			}
		}

		public float mouseX
		{
			get
			{
				return 0f;
			}
		}

		public float mouseY
		{
			get
			{
				return 0f;
			}
		}

		public SysInterfaceImpl()
		{
			this.m_localStorage = new LocalStorageImpl();
		}

		public void parseXMLFromFile(string path, Action<Variant> onFin)
		{
			new URLReqImpl
			{
				dataFormat = "binary",
				url = path
			}.load(delegate(IURLReq r, object ret)
			{
				Variant obj = this.parseXML(ret as byte[], path);
				onFin(obj);
			}, null, null);
		}

		public void parseXMLFromData(byte[] data, Action<Variant> onFin)
		{
			onFin(this.parseXML(data, ""));
		}

		private Variant _UnpackFullTypePackage(ByteArray d)
		{
			Variant variant = null;
			bool flag = d.bytesAvailable < 1;
			Variant result;
			if (flag)
			{
				result = variant;
			}
			else
			{
				uint num = (uint)d.readUnsignedByte();
				uint num2 = num & 15u;
				bool flag2 = num2 == 0u;
				if (flag2)
				{
					variant = new Variant((num & 16u) > 0u);
				}
				else
				{
					bool flag3 = num2 == 1u;
					if (flag3)
					{
						bool flag4 = (num & 16u) > 0u;
						uint num3 = num >> 5;
						bool flag5 = flag4;
						if (flag5)
						{
							bool flag6 = num3 == 2u;
							if (flag6)
							{
								variant = new Variant(d.readInt());
							}
							else
							{
								bool flag7 = num3 == 1u;
								if (flag7)
								{
									variant = new Variant(d.readShort());
								}
								else
								{
									bool flag8 = num3 == 0u;
									if (flag8)
									{
										variant = new Variant(d.readByte());
									}
									else
									{
										bool flag9 = num3 == 3u;
										if (flag9)
										{
											variant = new Variant((long)((ulong)d.readUnsignedInt() + (ulong)((long)d.readInt() * 4294967296L)));
										}
									}
								}
							}
						}
						else
						{
							bool flag10 = num3 == 2u;
							if (flag10)
							{
								variant = new Variant(d.readUnsignedInt());
							}
							else
							{
								bool flag11 = num3 == 1u;
								if (flag11)
								{
									variant = new Variant(d.readUnsignedShort());
								}
								else
								{
									bool flag12 = num3 == 0u;
									if (flag12)
									{
										variant = new Variant(d.readUnsignedByte());
									}
									else
									{
										bool flag13 = num3 == 3u;
										if (flag13)
										{
											variant = new Variant((long)((ulong)d.readUnsignedInt() + (ulong)((long)d.readInt() * 4294967296L)));
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag14 = num2 == 2u;
						if (flag14)
						{
							uint num3 = num >> 5;
							uint num4 = num3;
							if (num4 != 2u)
							{
								if (num4 == 3u)
								{
									variant = new Variant(d.readDouble());
								}
							}
							else
							{
								variant = new Variant(d.readFloat());
							}
						}
						else
						{
							bool flag15 = num2 == 3u;
							if (flag15)
							{
								uint num5 = (uint)d.readUnsignedByte();
								uint num6 = (uint)d.readUnsignedShort();
								int num7 = (int)((num6 << 16 | num5 << 8 | num) >> 4);
								variant = new Variant(d.readUTF8Bytes(num7));
							}
							else
							{
								bool flag16 = num2 == 4u;
								if (flag16)
								{
									uint num5 = (uint)d.readUnsignedByte();
									uint num6 = (uint)d.readUnsignedShort();
									int num7 = (int)((num6 << 16 | num5 << 8 | num) >> 4);
									variant = new Variant();
									variant.setToDct();
									for (int i = 0; i < num7; i++)
									{
										int len = (int)d.readUnsignedByte();
										string text = d.readUTF8Bytes(len);
										int num8 = text.LastIndexOf("\0");
										bool flag17 = num8 != -1;
										if (flag17)
										{
											text = text.Substring(0, num8);
										}
										variant[text] = this._UnpackFullTypePackage(d);
										bool isStr = variant[text].isStr;
										if (isStr)
										{
											int num9 = variant[text]._str.LastIndexOf("\0");
											bool flag18 = num9 != -1;
											if (flag18)
											{
												string text2 = variant[text]._str.Substring(0, num9);
												variant[text] = variant[text]._str.Substring(0, num9);
											}
										}
									}
								}
								else
								{
									bool flag19 = num2 == 5u;
									if (flag19)
									{
										uint num5 = (uint)d.readUnsignedByte();
										uint num6 = (uint)d.readUnsignedShort();
										int num7 = (int)((num6 << 16 | num5 << 8 | num) >> 4);
										variant = new Variant();
										variant.setToArray();
										for (int j = 0; j < num7; j++)
										{
											variant.pushBack(this._UnpackFullTypePackage(d));
										}
									}
									else
									{
										bool flag20 = num2 == 6u;
										if (flag20)
										{
											uint num5 = (uint)d.readUnsignedByte();
											uint num6 = (uint)d.readUnsignedShort();
											int num7 = (int)((num6 << 16 | num5 << 8 | num) >> 4);
											ByteArray byteArray = new ByteArray();
											d.readBytes(byteArray, 0, num7);
											variant = new Variant(byteArray);
										}
										else
										{
											bool flag21 = num2 == 7u;
											if (flag21)
											{
												uint num5 = (uint)d.readUnsignedByte();
												uint num6 = (uint)d.readUnsignedShort();
												int num7 = (int)((num6 << 16 | num5 << 8 | num) >> 4);
												variant = new Variant();
												variant.setToIntkeyDct();
												for (int k = 0; k < num7; k++)
												{
													int idx = d.readInt();
													variant[idx] = this._UnpackFullTypePackage(d);
												}
											}
										}
									}
								}
							}
						}
					}
				}
				result = variant;
			}
			return result;
		}

		public Variant parseXML(byte[] bytes, string path)
		{
			Variant variant = null;
			bool flag = bytes[0] == 78 && bytes[1] == 105 && bytes[2] == 99;
			Variant result;
			if (flag)
			{
				byte[] array = new byte[bytes.Length - 14];
				for (int i = 14; i < bytes.Length; i++)
				{
					array[i - 14] = bytes[i];
				}
				ByteArray byteArray = new ByteArray(array);
				byteArray.uncompress();
				variant = this._UnpackFullTypePackage(byteArray);
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream(bytes);
				XmlReader xmlReader = XmlReader.Create(memoryStream, new XmlReaderSettings
				{
					IgnoreComments = true,
					IgnoreProcessingInstructions = true,
					IgnoreWhitespace = true,
					ValidationType = ValidationType.None,
					ValidationFlags = XmlSchemaValidationFlags.None,
					ProhibitDtd = true,
					ConformanceLevel = ConformanceLevel.Document
				});
				Stack<Variant> stack = new Stack<Variant>();
				try
				{
					while (xmlReader.Read())
					{
						bool flag2 = xmlReader.NodeType == XmlNodeType.Element;
						if (flag2)
						{
							Variant variant2 = Variant.alloc();
							bool flag3 = stack.Count > 0;
							if (flag3)
							{
								Variant variant3 = stack.Peek();
								bool flag4 = !variant3.ContainsKey(xmlReader.Name);
								if (flag4)
								{
									variant3[xmlReader.Name] = Variant.alloc();
								}
								variant3[xmlReader.Name].pushBack(variant2);
							}
							else
							{
								variant = variant2;
							}
							bool flag5 = !xmlReader.IsEmptyElement;
							if (flag5)
							{
								stack.Push(variant2);
							}
							while (xmlReader.MoveToNextAttribute())
							{
								variant2[xmlReader.Name] = new Variant(xmlReader.Value);
							}
						}
						else
						{
							bool flag6 = xmlReader.NodeType == XmlNodeType.EndElement;
							if (flag6)
							{
								stack.Pop();
							}
						}
					}
				}
				catch (Exception var_19_1CC)
				{
					DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
					{
						"[Parse XML error] File: ",
						path,
						" Line: ",
						xmlReader.Settings.LineNumberOffset,
						" Col: ",
						xmlReader.Settings.LinePositionOffset
					}));
					result = null;
					return result;
				}
				xmlReader.Close();
				memoryStream.Close();
			}
			result = variant;
			return result;
		}

		public void addGlobalEventListener(Define.EventType eventType, Action<Event> cbFunc)
		{
			bool flag = cbFunc == null;
			if (!flag)
			{
				bool flag2 = this.m_eventFunc.ContainsKey(eventType);
				if (flag2)
				{
					Dictionary<Define.EventType, Action<Event>> eventFunc = this.m_eventFunc;
					eventFunc[eventType] = (Action<Event>)Delegate.Combine(eventFunc[eventType], cbFunc);
				}
				else
				{
					this.m_eventFunc[eventType] = cbFunc;
				}
			}
		}

		public void removeGlobalEventListener(Define.EventType eventType, Action<Event> cbFunc)
		{
			bool flag = cbFunc == null;
			if (!flag)
			{
				bool flag2 = this.m_eventFunc.ContainsKey(eventType);
				if (flag2)
				{
					Dictionary<Define.EventType, Action<Event>> eventFunc = this.m_eventFunc;
					eventFunc[eventType] = (Action<Event>)Delegate.Remove(eventFunc[eventType], cbFunc);
				}
			}
		}

		public void onProcess(float tmSlice)
		{
			this._getFps(tmSlice);
			bool flag = !this.m_eventFunc.ContainsKey(Define.EventType.PROCESSS);
			if (!flag)
			{
				this.m_eventFunc[Define.EventType.PROCESSS](null);
			}
		}

		public void onMouseEvent(Define.EventType evtType, int id, Define.MouseButton button, Vector3 pt, bool execute)
		{
		}

		public void exit()
		{
			Process.GetCurrentProcess().Kill();
		}

		private void _getFps(float tmSlice)
		{
			this.mFrameCount += 1f;
			this.mLastFrameTime += tmSlice;
			bool flag = this.mLastFrameTime > 1f;
			if (flag)
			{
				this.m_fps = this.mFrameCount;
				this.mFrameCount = 0f;
				this.mLastFrameTime = 0f;
			}
		}
	}
}
