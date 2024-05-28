using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PowerUp powerUp;

    void OnTriggerEnter(Collider other)
    {
        var visitable = other.GetComponent<IPlayerElement>();
        if (visitable != null)
        {
            visitable.Accept(powerUp);
            Destroy(gameObject);
        }
    }
}
