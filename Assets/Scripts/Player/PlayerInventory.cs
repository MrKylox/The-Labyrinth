using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfRunes {  get; private set; }

    public void RunesCollected()
    {
        NumberOfRunes++;
    }
}
