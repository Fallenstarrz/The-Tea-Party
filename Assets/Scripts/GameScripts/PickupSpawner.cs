using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform tf;

    private GameObject pickupSpawned;

    // Use this for initialization
    void Start()
    {
        tf = GetComponent<Transform>();

        pickupSpawned = null;

        spawnNewPickup();
    }

    private void Update()
    {
        spawnNewPickup();
    }

    private void spawnNewPickup()
    {
        if (pickupSpawned == null)
        {
            pickupSpawned = Instantiate(objectToSpawn, tf);
        }
    }
}
