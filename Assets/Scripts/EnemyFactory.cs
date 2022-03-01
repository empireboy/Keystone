using System;
using UnityEngine;

public class EnemyFactory
{
    private ObjectPoolManager _objectPoolManager;

    public EnemyFactory(ObjectPoolManager objectPoolManager)
    {
        _objectPoolManager = objectPoolManager;
    }
    
    public GameObject Create(string enemy)
    {
        GameObject enemyObject = _objectPoolManager.GetReusable(enemy);

        enemyObject.SetActive(true);

        return enemyObject;
    }

    public GameObject Create(Enum enemy)
    {
        return Create(enemy.ToString());
    }
}