using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : MonoBehaviour, IPlayerElement
{
    
    public float points = 0f;

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
        Debug.Log("Coin Visited");
    }
}
