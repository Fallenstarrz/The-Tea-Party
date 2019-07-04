using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject spawnablePawn;
    public PlayerPawn myPawn;
    public Transform spawnPoint;

    public enum playerNumber
    {
        player1,
        player2,
        player3,
        player4
    }
    public playerNumber myPlayerNumber;

    private string myControllerNumber;

    // Use this for initialization
    void Start()
    {
        switch (myPlayerNumber)
        {
            case playerNumber.player1:
                myControllerNumber = "C1";
                spawnPoint = GameManager.instance.spawnPoints[0];
                break;
            case playerNumber.player2:
                myControllerNumber = "C2";
                spawnPoint = GameManager.instance.spawnPoints[1];
                break;
            case playerNumber.player3:
                myControllerNumber = "C3";
                spawnPoint = GameManager.instance.spawnPoints[2];
                break;
            case playerNumber.player4:
                myControllerNumber = "C4";
                spawnPoint = GameManager.instance.spawnPoints[3];
                break;
            default:
                break;
        }
        createNewPawn();
    }

    // Update is called once per frame
    void Update()
    {
        inputHandler();
    }

    private void inputHandler()
    {
        if (Input.GetButtonDown("PauseButton"))
        {
            GameManager.instance.togglePaused();
        }
        if (!GameManager.instance.isPaused && myPawn != null)
        {
            Vector2 myVector = new Vector2(Input.GetAxis(myControllerNumber + "_Horizontal"), Input.GetAxis(myControllerNumber + "_Vertical"));
            myPawn.move(myVector);
            if (Input.GetButtonDown(myControllerNumber + "_Dash"))
            {
                myPawn.dash(myVector);
            }
            if (Input.GetButtonDown(myControllerNumber + "_Attack"))
            {
                myPawn.attack();
            } 
        }
    }

    public void createNewPawn()
    {
        myPawn = Instantiate(spawnablePawn, spawnPoint).GetComponent<PlayerPawn>();
        myPawn.myController = this;
    }
}
