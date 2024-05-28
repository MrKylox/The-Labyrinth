using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoost : MonoBehaviour, IPlayerElement
{
    public PlayerController playerController;
    public bool isBoostActive = false;

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
        Debug.Log("Boost Visited");
    }

    public void ActivateBoost(float boostDuration, float boostAmount)
    {
        isBoostActive = true;
        playerController.IncreaseSpeedBoost(boostAmount);
        StartCoroutine(BoostTimer(boostDuration, boostAmount));
    }

    private IEnumerator BoostTimer(float boostDuration, float boostAmount)
    {
        yield return new WaitForSeconds(boostDuration);
        playerController.DecreaseSpeedBoost(boostAmount); // Reset boost speed after duration
        isBoostActive = false;
    }


}
