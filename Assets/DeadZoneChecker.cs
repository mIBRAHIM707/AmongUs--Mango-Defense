using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
public class DeadZoneChecker : MonoBehaviour
{
    public ShakeData explosionShakeData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider that entered has the tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Perform your desired action here
            CameraShakerHandler.Shake(explosionShakeData);
            Debug.Log("Enemy has entered the dead zone!");
        }
    }

}
