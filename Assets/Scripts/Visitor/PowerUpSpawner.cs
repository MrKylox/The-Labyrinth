using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpSpawner : MonoBehaviour
{

    [SerializeField] List<GameObject> PowerUps = new List<GameObject>();

    private int numberOfPowerUp = 50;

    public Vector3 GetRandomSpawnPosition()
    {
        // Choose the cell at cell position (X, Y) by randomizing X and Y
        // The maze is a 50 by 50 maze

        int cellX = Random.Range(1, 51);
        int cellZ = Random.Range(1, 51);

        // 
        float spawnX = (cellX - 1.0f) * 25f;
        float spawnZ = (cellZ - 1.0f) * 25f;

        return new Vector3(spawnX, 3f, spawnZ);
    }


    public void SpawnPowerUps()
    {
        // For looP
        for (int i = 0; i < PowerUps.Count; i++)
        {
            for (int j = 0; j < numberOfPowerUp; j++)
            {
                GameObject instance = Instantiate(PowerUps[i], GetRandomSpawnPosition(), Quaternion.identity);
            }
        }
    }
}
