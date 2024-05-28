using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ObserveHealth : Observer
{

    public void DamageOverlay(float durationTimer, Image overlay)
    {
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

  

    public override void Notify(Subject aSubject)
    {
        if (aSubject is PlayerController)
        {
            PlayerController player = aSubject as PlayerController;
            //DebuggingGame(player.PlayerHealth);
            DamageOverlay(player.OverlayDurationTimer,player.OverlayScreen);
            player.UpdateHealthUI(); // Update the health UI whenever the health changes
            Debug.Log(player.PlayerHealth);
        }
    }
}
