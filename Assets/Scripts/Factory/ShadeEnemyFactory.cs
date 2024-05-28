using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ShadeEnemyFactory : EnemyFactory
{
    public string prefabName = "Shade";

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
        ShadeEnemy newEnemy = instance.GetComponent<ShadeEnemy>();
        if (newEnemy == null) {
            // If no, add the sniper enemy script
            newEnemy = instance.AddComponent<ShadeEnemy>();
        }

        newEnemy.Initialize();

        return newEnemy;
    }

    


}
