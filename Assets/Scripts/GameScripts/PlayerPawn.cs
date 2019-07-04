using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    private Animator anim;
    private Transform tf;
    private Rigidbody rb;
    private AudioSource soundMaker;
    [HideInInspector]
    public PlayerController myController;

    public GameObject attackObject;
    public Transform attackPoint;

    public Transform carryPoint;

    private float attackTimerCurrent;
    [SerializeField]
    private float attackTimerMax;

    public float movementSpeed;
    public float dashSpeed;

    public float timeBeforeRespawn;
    public float timeToStun;
    public float timeToDashCurrent;
    public float timeToDashMax;

    public GameObject pickupItem;

    private Vector2 directionToDash;

    [HideInInspector]
    public bool isStunned;
    [HideInInspector]
    public bool isCarryingPickup;
    [HideInInspector]
    public bool isAttacking;
    [HideInInspector]
    public bool isDashing;
    [HideInInspector]
    public bool isDead;

    // Use this for initialization
    void Start()
    {
        attackTimerCurrent = 0;
        anim = GetComponent<Animator>();
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        soundMaker = GetComponent<AudioSource>();
        // myController is set when pawn is spawned
    }

    private void Update()
    {
        timeManager();
        if (isDashing)
        {
            doDash();
        }
    }

    private void timeManager()
    {
        if (attackTimerCurrent > 0)
        {
            attackTimerCurrent -= Time.deltaTime;
        }
    }

    public void move(Vector2 movementVector)
    {
        if (!isDashing && !isDead && !isStunned)
        {
            // Think about reducing speed while carrying the tea!
            if (movementVector.x == 0 && movementVector.y == 0)
            {
                // Idle Animation
            }
            else
            {
                // Walking Animation
            }
            if (movementVector.x == 0 && movementVector.y != 0)
            {
                tf.Translate(movementVector * (movementSpeed * Time.deltaTime));
            }
            else if (movementVector.x > 0)
            {
                // Flip Right
                tf.rotation = Quaternion.Euler(0, 0, 0);
                tf.Translate(movementVector * (movementSpeed * Time.deltaTime));
                directionToDash = movementVector;
            }
            else
            {
                // Flip Left
                tf.rotation = Quaternion.Euler(0, 180, 0);
                tf.Translate(new Vector2(-movementVector.x * (movementSpeed * Time.deltaTime), (movementVector.y * (movementSpeed * Time.deltaTime))));
                directionToDash = new Vector2(-movementVector.x, movementVector.y);
            }
        }
    }

    public void attack()
    {
        if (attackTimerCurrent <= 0 && !isDead && !isStunned && !isDashing && !isCarryingPickup)
        {
            // attack noise
            // attack animation
            attackTimerCurrent = attackTimerMax;
            GameObject myAttack = Instantiate(attackObject, attackPoint);
            myAttack.GetComponent<Attack>().owner = this.gameObject;
        }
    }

    public void dash(Vector2 movementVector)
    {
        // dash animation
        // dash noise
        if (!isDashing && !isDead && !isStunned && !isCarryingPickup)
        {
            isDashing = true;
            timeToDashCurrent = timeToDashMax;
        }
    }

    public void takeDamage(float damageToTake)
    {
        // takeDamage noise
        // take damage flashy
        if (isCarryingPickup)
        {
            dropPickup();
        }
    }

    public void dropPickup()
    {
        tf.position = myController.spawnPoint.position;
        Destroy(pickupItem);
        isCarryingPickup = false;
    }

    public void doDash()
    {
        transform.Translate(directionToDash * dashSpeed * Time.deltaTime);
        timeToDashCurrent -= Time.deltaTime;
        if (timeToDashCurrent < 0)
        {
            isDashing = false;
        }
    }
}
