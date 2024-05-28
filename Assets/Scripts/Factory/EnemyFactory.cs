using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{
    public abstract IEnemy CreateEnemy(List<GameObject> enemyPrefabs, Vector3 position);

    public string GetLog(IEnemy enemy)
    {
        string msg = "Factory: created enemy " + enemy.EnemyName;
        return msg;
    }
}