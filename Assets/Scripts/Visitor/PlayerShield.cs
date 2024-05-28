using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour, IPlayerElement
{
    public bool isShieldActive= false;

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
        Debug.Log("Shield Visited");
    }

    public void ActivateShield(bool newShield)
    {
        isShieldActive = newShield;
    }

    public bool ShieldState => isShieldActive;

    public void DestroyShield()
    {
        isShieldActive=false;
    }
}
