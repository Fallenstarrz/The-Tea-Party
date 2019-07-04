using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointConnector : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;

    // Use this for initialization
    void Start()
    {
        GameManager.instance.spawnPoints.Clear();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameManager.instance.spawnPoints.Add(spawnPoints[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
