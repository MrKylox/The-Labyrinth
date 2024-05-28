using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    // Create a list of GameObjects that stores prefabs
    // Will manual drag and drop the prefabs
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();

    private int numberOfEnemy = 100;

    // Get random spawn position for the enemy
    public Vector3 GetRandomPosition()
    {
        // Choose the cell at cell position (X, Y) by randomizing X and Y
        // The maze is a 50 by 50 maze

        int cellX = Random.Range(1, 51);
        int cellZ = Random.Range(1, 51);

        // 
        float spawnX = (cellX - 1.0f) * 25f;
        float spawnZ = (cellZ - 1.0f) * 25f;

        return new Vector3(spawnX, 0.5f, spawnZ);
    }



    public EnemyFactory GetSpawnType()
    {
        // Set defualt.
        EnemyFactory output = new TrollEnemyFactory();

        //int aRandomNumber = Random.Range(1, 3);

        //if (aRandomNumber == 1)
        //{
        //    output = new ShadeEnemyFactory();

        //} else if(aRandomNumber == 2)
        //{
        //    output = new TrollEnemyFactory();
        //}

        return output;
    }

    public void SpawnEnemies()
    {
        // For looP
        for (int i = 0; i < numberOfEnemy; i++)
        {
            Vector3 newSpawnPosition = GetRandomPosition();
            GetSpawnType().CreateEnemy(prefabs, newSpawnPosition);
        }
    }

    





}
