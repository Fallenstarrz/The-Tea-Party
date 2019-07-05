using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointConnector : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private GameObject playerController;

    // Use this for initialization
    void Start()
    {
        GameManager.instance.spawnPoints.Clear();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameManager.instance.spawnPoints.Add(spawnPoints[i]);
        }
        GameManager.instance.playerController = playerController;
        GameManager.instance.startGame();
    }
}
