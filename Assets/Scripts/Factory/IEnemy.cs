using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public string EnemyName { get; set; }
    public float EnemyHealth { get; set; }

    public float EnemyDamage { get; set; }

    public void Initialize();


}
