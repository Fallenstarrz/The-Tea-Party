using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject spawnablePawn;
    private PlayerPawn myPawn;
    public Transform spawnPoint;

    public Image myScoreArea;

    public int myScore;

    private string myControllerNumber;
    public enum playerNumber
    {
        player1,
        player2,
        player3,
        player4
    }
    public playerNumber myPlayerNumber;
    
    // Set up scores
    // if score is > 2, this player wins
    // score UI

    // Use this for initialization
    public void BeginPlay()
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
        myScoreArea = myPawn.fillyBar;
    }

    // Update is called once per frame
    void Update()
    {
        inputHandler();
    }

    private void inputHandler()
    {
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

    public void incrementScore()
    {
        if (GameManager.instance.isGameOver == false)
        {
            myScore++;
            checkVictory();
            myScoreArea.fillAmount = (float)myScore / 3;
            Debug.Log(myScoreArea.fillAmount);
        }
    }

    public void checkVictory()
    {
        if (myScore >= 3)
        {
            GameManager.instance.victoryText.GetComponentInChildren<Text>().text = "Game Over " + myPlayerNumber.ToString() + " Wins";
            GameManager.instance.victoryText.SetActive(true);
            GameManager.instance.resetGame();
        }
    }
}
