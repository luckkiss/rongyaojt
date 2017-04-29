using System;

namespace Cross
{
	public interface ISysInterface
	{
		int windowWidth
		{
			get;
		}

		int windowHeight
		{
			get;
		}

		float mouseX
		{
			get;
		}

		float mouseY
		{
			get;
		}

		float fps
		{
			get;
		}

		ILocalStorage loscalStorage
		{
			get;
		}

		void parseXMLFromFile(string path, Action<Variant> onFin);

		void parseXMLFromData(byte[] data, Action<Variant> onFin);

		Variant parseXML(byte[] bytes, string path);

		void addGlobalEventListener(Define.EventType eventType, Action<Event> cbFunc);

		void removeGlobalEventListener(Define.EventType eventType, Action<Event> cbFunc);

		void exit();
	}
}
