using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowZone : MonoBehaviour
{
    public Transform pointToThrowTowards;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerPawn>().isCarryingPickup)
        {
            Destroy(other.gameObject.GetComponent<PlayerPawn>().pickupItem);
            other.gameObject.GetComponent<PlayerPawn>().isCarryingPickup = false;
        }
    }
}
