using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isBeingCarried;
    private PlayerPawn myCourier;
    private Transform tf;

    private void Start()
    {
        tf = GetComponent<Transform>();
        isBeingCarried = false;
        myCourier = null;
    }

    private void Update()
    {
        if (myCourier != null)
        {
            tf.position = myCourier.carryPoint.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBeingCarried)
        {
            if (other.gameObject.GetComponent<PlayerPawn>())
            {
                isBeingCarried = true;
                myCourier = other.gameObject.GetComponent<PlayerPawn>();
                myCourier.pickupItem = this.gameObject;
                myCourier.isCarryingPickup = true;
            }
        }
    }
}
