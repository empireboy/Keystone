using CM.Collections;
using System;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool[] _gameObjectPools;

    public void AddReusable(string name, GameObject reusable)
    {
        GetGameObjectPool(name).AddReusable(reusable);
    }

    public GameObject GetReusable(string name)
    {
        return GetGameObjectPool(name).GetReusable();
    }

    public GameObject GetReusable(Enum name)
    {
        return GetReusable(name.ToString());
    }

    private GameObjectPool GetGameObjectPool(string name)
    {
        foreach (GameObjectPool gameObjectPool in _gameObjectPools)
        {
            if (name.EqualsWithoutSpaces(gameObjectPool.DefaultObject.name))
            {
                return gameObjectPool;
            }
        }

        return null;
    }
}