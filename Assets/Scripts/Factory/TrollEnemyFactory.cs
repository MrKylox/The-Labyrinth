using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollEnemyFactory : EnemyFactory
{
    public string prefabName = "Troll";
    public override IEnemy CreateEnemy(List<GameObject> enemyPrefabs, Vector3 position)
    {
        // Set default
        GameObject objectToSpawn = enemyPrefabs[0];

        foreach (GameObject enemy in enemyPrefabs)
        {
            if (enemy.name == prefabName)
            {
                objectToSpawn = enemy;
            }
        }

        // Spawns the game object
        GameObject instance = Instantiate(objectToSpawn, position, Quaternion.identity);

        // Check if there is a sniper enemy script attatched
        TrollEnemy newEnemy = instance.GetComponent<TrollEnemy>();
        if (newEnemy == null)
        {
            // If no, add the sniper enemy script
            newEnemy = instance.AddComponent<TrollEnemy>();
        }

        newEnemy.Initialize();

        return newEnemy;
    }
}
