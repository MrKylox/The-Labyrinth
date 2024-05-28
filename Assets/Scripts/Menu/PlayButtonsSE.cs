using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonsSE : MonoBehaviour
{
    public GameObject anAudioObject;

    public void DropAudio()
    {
        Instantiate(anAudioObject, transform.position, transform.rotation);
    }
}
