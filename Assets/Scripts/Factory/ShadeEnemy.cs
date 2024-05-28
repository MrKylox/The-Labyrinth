using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShadeEnemy: MonoBehaviour, IEnemy
{
    [SerializeField] private string enemyName = "Shade";
    [SerializeField] private float enemyHealth = 100f;
    [SerializeField] private float enemyDamage = 25f;


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


    public void Initialize()
    {
        Debug.Log("Shade Spawned");

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
