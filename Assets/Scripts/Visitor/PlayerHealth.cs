using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IPlayerElement
{
    public PlayerController playerController;
    public float maxHealth = 100f;

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
        Debug.Log("Health Visited");
    }
}
