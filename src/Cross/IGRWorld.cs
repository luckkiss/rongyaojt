using System;

namespace Cross
{
	public interface IGRWorld
	{
		IGRMap curMap
		{
			get;
			set;
		}

		IGRMap createMap(string id);

		IGRMap getMap(string id);

		bool deleteMap(string id);

		bool deleteMap(IGRMap map);

		IGREntity createEntity(Define.GREntityType type, string id);

		IGREntity createEntity(Define.GREntityType type);

		IGREntity getEntity(string id);

		bool deleteEntity(string id);

		bool deleteEntity(IGREntity ent);
	}
}
