using System;

namespace Cross
{
	public interface IPHWorld
	{
		string id
		{
			get;
		}

		IPHMap createMap(string id);

		IPHMap getMap(string id);

		void deleteMap(string id);

		IPHEntity createEntity(Define.PHEntityType type, string id);

		IPHEntity createEntity(Define.PHEntityType type);

		IPHEntity getEntity(string id);

		void deleteEntity(string id);

		void deleteEntity(IPHEntity ent);
	}
}
