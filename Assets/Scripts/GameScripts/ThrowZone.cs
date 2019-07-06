using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowZone : MonoBehaviour
{
    public Transform pointToThrowTowards;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerPawn>() != null)
        {
            if (other.gameObject.GetComponent<PlayerPawn>().isCarryingPickup == true)
            {
                Destroy(other.gameObject.GetComponent<PlayerPawn>().pickupItem);
                other.gameObject.GetComponent<PlayerPawn>().isCarryingPickup = false;
                other.gameObject.GetComponent<PlayerPawn>().myController.incrementScore();
            } 
        }
    }
}
