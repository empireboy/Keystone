using System;
using UnityEngine;

public class EntityFactory
{
    private ObjectPoolManager _objectPoolManager;

    public EntityFactory(ObjectPoolManager objectPoolManager)
    {
        _objectPoolManager = objectPoolManager;
    }
    
    public GameObject Create(string name)
    {
        GameObject entity = _objectPoolManager.GetReusable(name);

        entity.SetActive(true);

        return entity;
    }

    public GameObject Create(Enum name)
    {
        return Create(name.ToString());
    }
}