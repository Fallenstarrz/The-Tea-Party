using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPawn : MonoBehaviour
{
    private Animator anim;
    private Transform tf;
    private Rigidbody rb;
    private AudioSource soundMaker;
    [HideInInspector]
    public PlayerController myController;
    public Image fillyBar;

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
        if (attackTimerCurrent >= 0)
        {
            attackTimerCurrent -= Time.deltaTime;
        }
    }

    bool isFacingRight = true;
    public void move(Vector2 movementVector)
    {
        if (!isDashing && !isDead && !isStunned)
        {
            tf.Translate(movementVector * (movementSpeed * Time.deltaTime));
            directionToDash = movementVector;
            // Think about reducing speed while carrying the tea!
            if (movementVector.x == 0 && movementVector.y == 0)
            {
                // Idle Animation
            }
            else
            {
                // Walking Animation
            }
            if (isFacingRight == false && movementVector.x > 0)
            {
                flipActor();
            }
            else if (isFacingRight == true && movementVector.x < 0)
            {
                flipActor();
            }
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void flipActor()
    {
        isFacingRight = !isFacingRight;
        Vector3 scalar = transform.localScale;
        scalar.x *= -1;
        transform.localScale = scalar;
    }

    public void attack()
    {
        if (attackTimerCurrent <= 0 && !isDead && !isStunned && !isDashing && !isCarryingPickup)
        {
            // attack noise
            // attack animation
            Debug.Log("Attack Pressed");
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

    public void doPauseGame()
    {
        GameManager.instance.togglePaused();
    }
}
