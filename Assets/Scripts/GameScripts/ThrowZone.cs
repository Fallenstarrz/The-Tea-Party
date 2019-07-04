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
            // Think about delaying before we throw it
            // Throw towards point
        }
    }
}
