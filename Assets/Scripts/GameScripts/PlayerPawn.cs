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

    private float healthCurrent;
    [SerializeField]
    private float healthMax;
    private float staminaCurrent;
    [SerializeField]
    private float staminaMax;
    [SerializeField]
    private float staminaRegen;
    private float attackTimerCurrent;
    [SerializeField]
    private float attackTimerMax;

    public float movementSpeed;
    public float dashSpeed;

    public float timeBeforeRespawn;
    public float timeToStun;
    public float timeToDash;

    public GameObject pickupItem;

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
        healthCurrent = healthMax;
        staminaCurrent = staminaMax;
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
    }

    private void timeManager()
    {
        if (attackTimerCurrent > 0)
        {
            attackTimerCurrent -= Time.deltaTime;
        }
        if (staminaMax <= 0)
        {
            staminaCurrent += (Time.deltaTime * staminaRegen);
        }
    }

    public void move(Vector2 movementVector)
    {
        if (!isDashing && !isDead && !isStunned)
        {
            // Think about reducing speed while carrying the tea!
            tf.Translate(movementVector * (movementSpeed * Time.deltaTime));
            if (movementVector.x == 0 && movementVector.y == 0)
            {
                // Idle Animation
            }
            else
            {
                // Walking Animation
            }

            if (movementVector.x > 0)
            {
                // Flip Right
                tf.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                // Flip Left
                tf.rotation = Quaternion.Euler(0, 180, 0);
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
            StartCoroutine(doDash(movementVector));
        }
    }

    public void takeDamage(float damageToTake)
    {
        // takeDamage noise
        // take damage flashy
        healthCurrent -= damageToTake;
        if (isCarryingPickup)
        {
            dropPickup();
            isStunned = true;
            StartCoroutine(beStunned());
        }
        if (healthCurrent <= 0)
        {
            die();
        }
    }

    public void dropPickup()
    {
        // throw the gameobject randomly somewhere around the player.
        // is carryingPickup = false;
        // BETTER IDEA!
        // Reset tea to middle!!
    }

    public void die()
    {
        // death noise
        // death animation
        isDead = true;
        StartCoroutine(respawnTimer());
    }

    public IEnumerator doDash(Vector2 directionToDash)
    {
        tf.Translate(directionToDash * (dashSpeed * Time.deltaTime));
        yield return new WaitForSeconds(timeToDash);
        isDashing = false;
    }

    public IEnumerator beStunned()
    {
        yield return new WaitForSeconds(timeToStun);
        isStunned = false;
    }

    public IEnumerator respawnTimer()
    {
        yield return new WaitForSeconds(timeBeforeRespawn);
        myController.createNewPawn();
        Destroy(this.gameObject);
    }
}
