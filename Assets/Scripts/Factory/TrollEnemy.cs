using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] private string enemyName = "Troll";
    [SerializeField] private float enemyHealth = 200f;
    [SerializeField] private float enemyDamage = 50f;

    public string EnemyName
    {
        get { return enemyName; }
        set { enemyName = value; }
    }
    public float EnemyHealth
    {
        get { return enemyHealth; }
        set { enemyHealth = value; }
    }

    public float EnemyDamage
    {
        get { return enemyDamage; }
        set { enemyDamage = value; }
    }

    public float EnemySight
    {
        get { return enemyDamage; }
        set { enemyDamage = value; }
    }

    public void Initialize()
    {
        Debug.Log("Troll Spawned");

        // Set state as roaming
        // Start looking for players
    }

    private void OnCollisionEnter(Collision collision)
    {

        PlayerController health = collision.gameObject.GetComponent<PlayerController>();
        if (health != null)
        {
            health.TakeDamage(EnemyDamage);
        }

    }
}