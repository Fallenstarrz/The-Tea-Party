using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PauseButton"))
        {
            GameManager.instance.togglePaused();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.togglePaused();
        }
    }
}
