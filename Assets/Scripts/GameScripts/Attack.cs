using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public float timeBeforeDestruction;
    public GameObject owner;

    private void Start()
    {
        Destroy(this.gameObject, timeBeforeDestruction);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisionObject = other.gameObject;
        if (collisionObject.GetComponent<PlayerPawn>() && collisionObject != owner)
        {
            dealDamage(collisionObject);
        }
    }

    private void dealDamage(GameObject objectToTakeDamage)
    {
        objectToTakeDamage.GetComponent<PlayerPawn>().takeDamage(damage);
    }
}
