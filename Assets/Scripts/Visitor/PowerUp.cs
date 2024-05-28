using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]

public class PowerUp : ScriptableObject, IVisitor
{

    public int HealthBonus = 25;
    public bool PShield = true;

    public float PCoin = 1f;

    public float Pboost = 15f;
    private float boostDuration = 10.0f;


    public void Visit(PlayerBoost playerBoost)
    {
        playerBoost.ActivateBoost(boostDuration, Pboost);
    }

    public void Visit(PlayerShield playerShield)
    {
        playerShield.ActivateShield(PShield);
    }

    public void Visit(PlayerCoin playerCoin)
    {
        playerCoin.points += PCoin;
    }

    public void Visit(PlayerHealth playerHealth)
    {
        playerHealth.playerController.RestoreHealth(HealthBonus);
    }

}


