using CM.Collections;
using System;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool[] _gameObjectPools;

    public GameObject GetReusable(string name)
    {
        foreach (GameObjectPool gameObjectPool in _gameObjectPools)
        {
            if (name.EqualsWithoutSpaces(gameObjectPool.DefaultObject.name))
            {
                return gameObjectPool.GetReusable();
            }
        }

        return null;
    }

    public GameObject GetReusable(Enum name)
    {
        return GetReusable(name.ToString());
    }
}